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
    public partial class VerImpuestos : Form
    {
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;

        System.Windows.Forms.ContextMenu contextMenu2;

        public Dictionary<string, double> contadoresImpuestos { get; set; }
       
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public double totalImpuestosSAT { get; set; }
        public double totalContabilizadoImpuestosSAT { get; set; }
        public bool siguiente { get; set; }
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
        public VerImpuestos()
        {
            InitializeComponent();
        }
        public void VerPDF(object sender, EventArgs e)
        {
            int cuantos = impuestosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = impuestosList.SelectedItems[0].SubItems[2].Text;
                String ruta = impuestosList.SelectedItems[0].SubItems[0].Text;        
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                System.Diagnostics.Process.Start(nombre);
            }
        }

        public void VerXML(object sender, EventArgs e)
        {
            int cuantos = impuestosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = impuestosList.SelectedItems[0].SubItems[1].Text;
                String ruta = impuestosList.SelectedItems[0].SubItems[0].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                System.Diagnostics.Process.Start(nombre);
            }
        }
        private void VerImpuestos_Load(object sender, EventArgs e)
        {
            contadoresImpuestos= new Dictionary<string,double>();
            
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            impuestosList.Location = new Point(50, 75);
            impuestosList.Size = new Size(width - 150, height - 150);
            listaFinal = new List<Dictionary<string, object>>();
            totalImpuestosLabel.Location = new Point(50, height - 100);
            tipoCombo.Items.Add(new Item("Gasto", 1));
            tipoCombo.Items.Add(new Item("Ingreso", 2));
            tipoCombo.SelectedIndex = 0;
            
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2, menuItem1 });
            menuItem1.Index = 1;
            menuItem1.Text = "Ver xml";
            menuItem1.Click += VerXML;
            menuItem2.Index = 0;
            menuItem2.Text = "Ver pdf";
            menuItem2.Click += VerPDF;
            impuestosList.ContextMenu = contextMenu2;

            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";
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
            String queryPeriodos1 = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";
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

            impuestoCombo.Items.Add(new Item("Traslados", 1));
            impuestoCombo.Items.Add(new Item("Retenciones", 2));
            impuestoCombo.SelectedIndex = 0;
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 1;
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        private void tipoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        private void actualiza()
        {
            if (tipoCombo.SelectedIndex != -1 && periodosCombo.SelectedIndex != -1 && impuestoCombo.SelectedIndex != -1)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Item itm = (Item)periodosCombo.SelectedItem;
                Item itm2 = (Item)tipoCombo.SelectedItem;
                int tipo = itm2.Value;
                String periodo = itm.Name;
                Item itm4 = (Item)impuestoCombo.SelectedItem;
                int tipoImpuesto = itm4.Value;
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                impuestosList.Clear();
                listaFinal.Clear();
                
                totalImpuestosSAT = 0;
                totalContabilizadoImpuestosSAT = 0;
                //selecciona distintos impuestos que existen en la tabla impuestos y ponlos a 0 - YA
                //selecciona todas las facturas activas segun el periodo y segun el tipo 1 o 2
                //si el folio de la factura activa coincide con el tipo de impuesto lo voy sumando
                // alfinal lo muestro en la tabla
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT DISTINCT impuesto FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[impuestos]";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    contadoresImpuestos[reader.GetString(0)] = 0.0;
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
                        String queryXML = "SELECT folioFiscal FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(10)),1," + periodo.Length + ") = '" + periodo + "' AND STATUS = '"+tipo+"'";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {             
                                    String folioFiscal = reader.GetString(0);
                                    String query2 = "SELECT impuesto, importe FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[impuestos] WHERE folioFiscal = '" + folioFiscal + "' AND tipo = " + tipoImpuesto;
                                    SqlCommand cmdCheck1 = new SqlCommand(query2, connection);
                                    SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                    if (reader1.HasRows)
                                    {
                                        while (reader1.Read())
                                        {
                                            String impuesto = reader1.GetString(0).Trim();
                                            double importe = reader1.GetDouble(1);
                                            contadoresImpuestos[impuesto] += importe;
                                        }
                                    }
                                }  
                            }//if reader
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                foreach(KeyValuePair<string,double> impuesto in contadoresImpuestos)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("impuesto", impuesto.Key);
                    dictionary.Add("importe", impuesto.Value);
                    listaFinal.Add(dictionary);
                }
                impuestosList.View = View.Details;
                impuestosList.GridLines = true;
                impuestosList.FullRowSelect = true;
                impuestosList.Columns.Add("Impuesto",250);
                impuestosList.Columns.Add("Importe", 250);
                foreach (Dictionary<string, object> dic in listaFinal)
                {
                    if (dic.ContainsKey("impuesto"))
                    {
                        string[] arr = new string[12];
                        ListViewItem itm3;
                        //add items to ListView
                        arr[0] = Convert.ToString(dic["impuesto"]);
                        arr[1] = String.Format("{0:n}", Convert.ToDouble(dic["importe"]));
                        itm3 = new ListViewItem(arr);
                        impuestosList.Items.Add(itm3);
                    }
                }
   ///             totalImpuestosLabel.Text = "Total de impuestos pagados en el SAT: $" + Math.Round(totalImpuestosSAT,2).ToString() + " En el periodo: " + periodo;
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

        private void impuestosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(impuestosList.SelectedItems.Count>0)
            {
                //obtengo el tipo de impuesto, periodo y tipo 1 o 2
                //selecciono todas las facturas por periodo y tipo
                Item itm = (Item)periodosCombo.SelectedItem;
                Item itm2 = (Item)tipoCombo.SelectedItem;
                int tipo = itm2.Value;
                String periodo = itm.Name;
                String impuesto = impuestosList.SelectedItems[0].SubItems[0].Text.Trim();
                Item itm3 = (Item)impuestoCombo.SelectedItem;
                int tipoImpuesto = itm3.Value;
                ImpuestosDetalle1 form = new ImpuestosDetalle1(periodo, impuesto, tipo, tipoImpuesto);
                form.ShowDialog();
            }
        }

        private void impuestoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

    }
}
