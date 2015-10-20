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
    public partial class presupuestoDeDepartamentales : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }

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
                return Name;
            }
        }
        public presupuestoDeDepartamentales()
        {
            InitializeComponent();
        }

        private void presupuestoDeDepartamentales_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryCuentas = "SELECT ANL_CODE,LOOKUP FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_ANL_CODE] WHERE ANL_CAT_ID= '07' AND SUBSTRING( ANL_CODE,1,2) = 'ER' order by ANL_CODE asc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentas, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int empiezo1 = 1;
                        while (reader.Read())
                        {
                            String ACNT_CODE = reader.GetString(0);
                            personaCombo.Items.Add(new Item(ACNT_CODE, empiezo1, ACNT_CODE));
                            empiezo1++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            personaCombo.SelectedIndex = 0;
            anioRadio.Checked = true;
            
      
        }
        private void actualizaPresupuesto()
        {
            if(periodosCombo.Items.Count>0)
            {
                listaFinal.Clear();
                presupuestoList.Items.Clear();
                //get function
                 String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                 String queryPresupuesto = "";
              Item itm1 = (Item)periodosCombo.SelectedItem;
                String periodo = itm1.Name.ToString().Trim();

                Item itm = (Item)personaCombo.SelectedItem;
                String WHO = itm.Name.ToString().Trim();
                String FNCT = "";
                String query = "SELECT FNCT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_FNCTyWHO] WHERE WHO = '"+WHO+"'";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmdCheck = new SqlCommand(query, connection);
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FNCT = Convert.ToString(reader.GetString(0)).Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
                queryPresupuesto = "SELECT DISTINCT b.ACCNT_CODE, SUM( b.AMOUNT) as amount, MAX(c.DESCR) as DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibroPresupuesto + "_SALFLDG] b INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] c on c.ACNT_CODE = b.ACCNT_CODE WHERE b.ANAL_T3 = '" + FNCT + "' AND b.ANAL_T6 = '" + WHO + "' AND SUBSTRING( CAST(PERIOD AS NVARCHAR(10)),1," + periodo.Length + ") = '" + periodo + "'  GROUP BY  b.ACCNT_CODE";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmdCheck = new SqlCommand(queryPresupuesto, connection);
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ACCNT_CODE = reader.GetString(0).Trim();
                                double PRESUPUESTO = Math.Round(Math.Abs( Convert.ToDouble(reader.GetDecimal(1))), 2);
                                String DESCR = reader.GetString(2).Trim();
                                double gastado = 0;
                                String queryGastado = "SELECT ACCNT_CODE, AMOUNT, DESCRIPTN, TREFERENCE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ANAL_T3 = '"+FNCT+"' AND ANAL_T6 = '"+WHO+"' AND  SUBSTRING( CAST(PERIOD AS NVARCHAR(10)),1,"+periodo.Length+") = '"+periodo+"' AND ACCNT_CODE = '"+ACCNT_CODE+"'";
                                using (SqlCommand cmdCheck1 = new SqlCommand(queryGastado, connection))
                                {
                                    SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                    if (reader1.HasRows)
                                    {
                                        while (reader1.Read())
                                        {
                                            gastado += Math.Round(Convert.ToDouble(Math.Abs(reader1.GetDecimal(1))), 2); 
                                        }
                                    }
                                }
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("ACCNT_CODE", ACCNT_CODE);
                                dictionary.Add("PRESUPUESTO", PRESUPUESTO);
                                dictionary.Add("DESCR", DESCR);
                                dictionary.Add("gastado", gastado);
                                listaFinal.Add(dictionary);
                            }
                            presupuestoList.Clear();
                            presupuestoList.Items.Clear();
                            presupuestoList.View = View.Details;
                            presupuestoList.GridLines = true;
                            presupuestoList.FullRowSelect = true;
                            presupuestoList.Columns.Add("Cuenta", 180);
                            presupuestoList.Columns.Add("Descripción", 200);
                            presupuestoList.Columns.Add("Presupuestado", 150);
                            presupuestoList.Columns.Add("Gastado", 150);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("ACCNT_CODE"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["ACCNT_CODE"]);
                                    arr[1] = Convert.ToString(dic["DESCR"]);
                                    arr[2] = Convert.ToString(dic["PRESUPUESTO"]);
                                    arr[3] = Convert.ToString(dic["gastado"]);
                                    itm2 = new ListViewItem(arr);
                                    presupuestoList.Items.Add(itm2);
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }

        }
        private void actualizaPeriodos(int limite)
        {
            periodosCombo.Items.Clear();
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(PERIOD AS NVARCHAR(10)) ,1,"+limite+") FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibroPresupuesto + "_SALFLDG] order by SUBSTRING( CAST(PERIOD AS NVARCHAR(10)) ,1,"+limite+") asc";
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
                            String periodo = Convert.ToString(reader.GetString(0));
                            periodosCombo.Items.Add(new Item(periodo, empiezo));
                            empiezo++;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen registros de Periodos en el libro "+Properties.Settings.Default.sunLibroPresupuesto, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 1;
        }
        private void anioRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(anioRadio.Checked)
            {
                actualizaPeriodos(4);
            }
        }

        private void mesRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (mesRadio.Checked)
            {
                actualizaPeriodos(7);
            }
        }

        private void personaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizaPresupuesto();
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizaPresupuesto();
        }
    }
}
