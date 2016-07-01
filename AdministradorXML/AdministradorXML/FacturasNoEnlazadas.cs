using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace AdministradorXML
{
    public partial class FacturasNoEnlazadas : Form
    {
        public List<Dictionary<string, object>> listaFinalIngresos { get; set; }
        public List<Dictionary<string, object>> listaFinalGastos { get; set; }
        public List<String> listaIngresos { get; set; }
        public List<String> listaGastos { get; set; }



        private class Item
        {
            public string Name;
            public int Value;
            public string Extra;

            public Item(string name, int value, String extra)
            {
                Name = name; Value = value;
                Extra = extra;
            }
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }
        public FacturasNoEnlazadas()
        {
            InitializeComponent();
        }
        public String obtenRazonDelRFC(String rfc)
        {
            foreach (String todaLaLinea in listaGastos)
            {
                string[] parametros = todaLaLinea.Split('|');
                if (parametros[1].Equals(rfc))
                {
                    return parametros[2];
                }
            }
            return "";
        }
        public String obtenRazonDelRFC2(String rfc)
        {
            foreach (String todaLaLinea in listaIngresos)
            {
                string[] parametros = todaLaLinea.Split('|');
                if (parametros[1].Equals(rfc))
                {
                    return parametros[2];
                }
            }
            return "";
        }

        public void restaAmountAlFolio(String folioFiscal, double amount)
        {
            foreach (String todaLaLinea in listaGastos)
            {
                string[] parametros = todaLaLinea.Split('|');
                if(parametros[0].Equals(folioFiscal))
                {
                    //ya encontré la factura
                    double amountDeAhora = Convert.ToDouble(parametros[3]);
                    amountDeAhora -= amount;
                    String nuevaLinea = parametros[0] + "|" + parametros[1] + "|" + parametros[2] + "|" + amount + "|" + parametros[4] + "|" + parametros[5];
                    listaIngresos.Remove(todaLaLinea);
                    if(amount>0.0)
                    {
                        listaGastos.Add(nuevaLinea);
                    }
                    break;
                }
            }
        }
        public void restaAmountAlFolio2(String folioFiscal, double amount)
        {
            foreach (String todaLaLinea in listaIngresos)
            {
                string[] parametros = todaLaLinea.Split('|');
                if (parametros[0].Equals(folioFiscal))
                {
                    //ya encontré la factura
                    double amountDeAhora = Convert.ToDouble(parametros[3]);
                    amountDeAhora -= amount;
                    String nuevaLinea = parametros[0] + "|" + parametros[1] + "|" + parametros[2] + "|" + amount + "|" + parametros[4] + "|" + parametros[5];
                    listaIngresos.Remove(todaLaLinea);
                    if (amount > 0.0)
                    {
                        listaIngresos.Add(nuevaLinea);
                    }
                    break;
                }
            }
        }

        public void actualiza()
        {
            //obtengo todas las facturas del periodo y del tipo, hago una lista de string array
            //obtengo la lista de todas las facturas en fiscal_xml
            //hago un metodo para remover factura que tenga el foliofiscal, lo aplico para todos los elementos de la lista
            //los que me quedan hago un keyvalue para RFC, cantidad, y la razon social la obtengo con algun metodo custom !
            //repito lo mismo para ingreso y gasto

            if (periodosCombo.Items.Count > 0)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Item itm = (Item)periodosCombo.SelectedItem;
                String periodo = itm.Name;
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                ingresosList.Clear();
                listaIngresos.Clear();
                listaGastos.Clear();
                gastosList.Clear();
                //empiezo con las del gasto, tipo = 1
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT folioFiscal,rfc,razonSocial,total,folio,fechaExpedicion FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(10)),1," + periodo.Length + ") = '" + periodo + "' AND STATUS = '1' order by fechaExpedicion asc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    listaGastos.Add(reader.GetString(0).Trim() + "|" + reader.GetString(1).Trim() + "|" + reader.GetString(2).Trim() + "|" + reader.GetDecimal(3) + "|" + reader.GetString(4) + "|" + reader.GetDateTime(5).ToString().Substring(0, 10));
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT folioFiscal,rfc,razonSocial,total,folio,fechaExpedicion FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(10)),1," + periodo.Length + ") = '" + periodo + "' AND STATUS = '2' order by fechaExpedicion asc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    listaIngresos.Add(reader.GetString(0).Trim() + "|" + reader.GetString(1).Trim() + "|" + reader.GetString(2).Trim() + "|" + reader.GetDecimal(3) + "|" + reader.GetString(4) + "|" + reader.GetDateTime(5).ToString().Substring(0, 10));
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT FOLIO_FISCAL,AMOUNT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml]";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    String folioFiscal = reader.GetString(0);
                                    double amount = Convert.ToDouble(reader.GetDecimal(1));
                                    restaAmountAlFolio(folioFiscal, amount);
                                    restaAmountAlFolio2(folioFiscal, amount);
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                Dictionary<string, double> contadoresRFC = new Dictionary<string, double>();
                foreach (String todaLaLinea in listaGastos)
                {
                    string[] parametros = todaLaLinea.Split('|');
                    String rfc = parametros[1];
                    contadoresRFC[rfc] = 0.0;
                }
                foreach(String todaLaLinea in listaGastos)
                {
                    string[] parametros = todaLaLinea.Split('|');
                    String rfc = parametros[1];
                    double amount = Convert.ToDouble(parametros[3]);
                    contadoresRFC[rfc] += amount;
                }



                gastosList.View = View.Details;
                gastosList.GridLines = true;
                gastosList.FullRowSelect = true;
                gastosList.Columns.Add("RFC", 150);
                gastosList.Columns.Add("Razon Social", 350);
                gastosList.Columns.Add("Sin enlazar", 250);
                foreach (KeyValuePair<string, double> dosDatos in contadoresRFC)
                {
                    string[] arr = new string[4];
                    ListViewItem itm3;
                    arr[0] = Convert.ToString(dosDatos.Key);
                    arr[1] = obtenRazonDelRFC(dosDatos.Key);
                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dosDatos.Value));
                    itm3 = new ListViewItem(arr);
                    gastosList.Items.Add(itm3);
                }


                Dictionary<string, double> contadoresRFC2 = new Dictionary<string, double>();
                foreach (String todaLaLinea in listaIngresos)
                {
                    string[] parametros = todaLaLinea.Split('|');
                    String rfc = parametros[1];
                    contadoresRFC2[rfc] = 0.0;
                }
                foreach (String todaLaLinea in listaIngresos)
                {
                    string[] parametros = todaLaLinea.Split('|');
                    String rfc = parametros[1];
                    double amount = Convert.ToDouble(parametros[3]);
                    contadoresRFC2[rfc] += amount;
                }



                ingresosList.View = View.Details;
                ingresosList.GridLines = true;
                ingresosList.FullRowSelect = true;
                ingresosList.Columns.Add("RFC", 150);
                ingresosList.Columns.Add("Razon Social", 350);
                ingresosList.Columns.Add("Sin enlazar", 250);
                foreach (KeyValuePair<string, double> dosDatos in contadoresRFC2)
                {
                    string[] arr = new string[4];
                    ListViewItem itm3;
                    arr[0] = Convert.ToString(dosDatos.Key);
                    arr[1] = obtenRazonDelRFC2(dosDatos.Key);
                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dosDatos.Value));
                    itm3 = new ListViewItem(arr);
                    gastosList.Items.Add(itm3);
                }

                this.Cursor = System.Windows.Forms.Cursors.Arrow;
               


            }    

        }

        private void FacturasNoEnlazadas_Load(object sender, EventArgs e)
        {
            listaIngresos = new List<string>();
            listaGastos = new List<string>();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    int empiezo = 1;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String periodo = reader.GetString(0);
                            periodosCombo.Items.Add(new Item(periodo, empiezo));
                            empiezo++;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Periodos, primero descarga xml del buzon tributario.", "SunPlusXML", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            String queryPeriodos1 = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) as periodos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connString))
                {
                    connection1.Open();
                    SqlCommand cmdCheck1 = new SqlCommand(queryPeriodos1, connection1);
                    SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                    int empiezo1 = 1;
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            String periodo1 = reader1.GetString(0);
                            periodosCombo.Items.Add(new Item(periodo1, empiezo1));
                            empiezo1++;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Periodos, primero descarga xml del buzon tributario.", "SunPlusXML", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 2;
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }
    }
}
