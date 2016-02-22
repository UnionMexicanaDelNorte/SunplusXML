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
    public partial class VerRelacionRFCCuenta : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
       
        public VerRelacionRFCCuenta()
        {
            InitializeComponent();
        }
        public void verFuncion()
        {
            listaFinal.Clear();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT r.ACNT_CODE,r.prioridad,r.rfc , p.razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] r  INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] p on p.rfc=r.rfc WHERE r.BUNIT = '" + Login.unidadDeNegocioGlobal + "' order by r.rfc asc, r.prioridad asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String cuenta = reader.GetString(0).Trim();
                                int prioridad = reader.GetInt32(1);
                                String rfc = reader.GetString(2).Trim();
                                String razonSocial = reader.GetString(3).Trim();
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("razonSocial", razonSocial);
                                dictionary.Add("cuenta", cuenta);
                                dictionary.Add("prioridad", prioridad);
                                listaFinal.Add(dictionary);
                            }
                            listaDeAsociados.Clear();
                            listaDeAsociados.View = View.Details;
                            listaDeAsociados.GridLines = true;
                            listaDeAsociados.FullRowSelect = true;
                            listaDeAsociados.Columns.Add("RFC", 200);
                            listaDeAsociados.Columns.Add("Razon Social", 500);
                            listaDeAsociados.Columns.Add("Cuenta", 200);
                            listaDeAsociados.Columns.Add("Prioridad", 80);


                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("cuenta"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["rfc"]);
                                    arr[1] = Convert.ToString(dic["razonSocial"]);
                                    arr[2] = Convert.ToString(dic["cuenta"]);
                                    arr[3] = Convert.ToString(dic["prioridad"]);
                                    itm = new ListViewItem(arr);
                                    listaDeAsociados.Items.Add(itm);
                                }
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }


        private void VerRelacionRFCCuenta_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
            verFuncion();
        }
    }
}
