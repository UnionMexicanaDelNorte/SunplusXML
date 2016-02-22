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
    public partial class VerPolizas : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public int polizaFinal { get; set; }
       
        public VerPolizas()
        {
            InitializeComponent();
        }
        public VerPolizas(int pol)
        {
            InitializeComponent();
            polizaFinal = pol;
        }

        private void VerPolizas_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
                
             String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ACCNT_CODE, PERIOD, TRANS_DATETIME, AMOUNT,D_C,JRNAL_TYPE,JRNAL_SRCE,TREFERENCE,DESCRIPTN, ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9, JRNAL_LINE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].["+Login.unidadDeNegocioGlobal+"_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = "+polizaFinal+" order by JRNAL_LINE asc ";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    String cuenta = reader.GetString(0).Trim().ToUpper();
                                    int periodo = reader.GetInt32(1);
                                    String fecha = reader.GetDateTime(2).ToString().Substring(0, 10);
                                    double cantidad = Math.Round(Math.Abs(Convert.ToDouble(reader.GetDecimal(3))), 2);
                                    String D_C = reader.GetString(4);
                                    String tipoDeDiario = reader.GetString(5);
                                    String source = reader.GetString(6);
                                    String referencia = reader.GetString(7);
                                    String descripcion = reader.GetString(8);
                                    String ANAL_T0 = reader.GetString(9);
                                    String ANAL_T1 = reader.GetString(10);
                                    String ANAL_T2 = reader.GetString(11);
                                    String ANAL_T3 = reader.GetString(12);
                                    String ANAL_T4 = reader.GetString(13);
                                    String ANAL_T5 = reader.GetString(14);
                                    String ANAL_T6 = reader.GetString(15);
                                    String ANAL_T7 = reader.GetString(16);
                                    String ANAL_T8 = reader.GetString(17);
                                    String ANAL_T9 = reader.GetString(18);
                                    int linea = reader.GetInt32(19);
                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("cuenta", cuenta);
                                    dictionary.Add("periodo", periodo);
                                    dictionary.Add("JRNAL_NO", polizaFinal);
                                    dictionary.Add("JRNAL_LINE", linea);
                                    dictionary.Add("fecha", fecha);
                                    dictionary.Add("cantidad", cantidad);
                                    dictionary.Add("D_C", D_C);
                                    dictionary.Add("tipoDeDiario", tipoDeDiario);
                                    dictionary.Add("source", source);
                                    dictionary.Add("descripcion", descripcion);
                                    dictionary.Add("referencia", referencia);
                                    dictionary.Add("ANAL_T0", ANAL_T0);
                                    dictionary.Add("ANAL_T1", ANAL_T1);
                                    dictionary.Add("ANAL_T2", ANAL_T2);
                                    dictionary.Add("ANAL_T3", ANAL_T3);
                                    dictionary.Add("ANAL_T4", ANAL_T4);
                                    dictionary.Add("ANAL_T5", ANAL_T5);
                                    dictionary.Add("ANAL_T6", ANAL_T6);
                                    dictionary.Add("ANAL_T7", ANAL_T7);
                                    dictionary.Add("ANAL_T8", ANAL_T8);
                                    dictionary.Add("ANAL_T9", ANAL_T9);
                                    listaFinal.Add(dictionary);
                                }
                                listaPoliza.Clear();
                                listaPoliza.View = View.Details;
                                listaPoliza.GridLines = true;
                                listaPoliza.FullRowSelect = true;
                                listaPoliza.Columns.Add("Cuenta", 100);
                                listaPoliza.Columns.Add("Periodo", 100);
                                listaPoliza.Columns.Add("Diario", 90);
                                listaPoliza.Columns.Add("Linea", 90);
                                listaPoliza.Columns.Add("Debe", 100);
                                listaPoliza.Columns.Add("Haber", 100);
                                listaPoliza.Columns.Add("Tipo", 50);
                                listaPoliza.Columns.Add("Fecha T", 100);
                                listaPoliza.Columns.Add("Source", 50);
                                listaPoliza.Columns.Add("Descripción", 150);
                                listaPoliza.Columns.Add("Referencia", 100);
                                listaPoliza.Columns.Add("ANAL_T0", 60);
                                listaPoliza.Columns.Add("ANAL_T1", 60);
                                listaPoliza.Columns.Add("ANAL_T2", 60);
                                listaPoliza.Columns.Add("ANAL_T3", 60);
                                listaPoliza.Columns.Add("ANAL_T4", 60);
                                listaPoliza.Columns.Add("ANAL_T5", 60);
                                listaPoliza.Columns.Add("ANAL_T6", 60);
                                listaPoliza.Columns.Add("ANAL_T7", 60);
                                listaPoliza.Columns.Add("ANAL_T8", 60);
                                listaPoliza.Columns.Add("ANAL_T9", 60);
                                foreach (Dictionary<string, object> dic in listaFinal)
                                {
                                    if (dic.ContainsKey("cuenta"))
                                    {
                                        string[] arr = new string[22];
                                        ListViewItem itm;
                                        arr[0] = Convert.ToString(dic["cuenta"]);
                                        arr[1] = Convert.ToString(dic["periodo"]);
                                        arr[2] = Convert.ToString(dic["JRNAL_NO"]);
                                        arr[3] = Convert.ToString(dic["JRNAL_LINE"]);
                                        if (Convert.ToString(dic["D_C"]).Equals("D"))
                                        {
                                            arr[4] = String.Format("{0:n}", Convert.ToDouble(dic["cantidad"]));
                                            arr[5] = "$ 0";
                                        }
                                        else
                                        {
                                            arr[5] = String.Format("{0:n}", Convert.ToDouble(dic["cantidad"]));
                                            arr[4] = "$ 0";
                                        }

                                        arr[6] = Convert.ToString(dic["tipoDeDiario"]);
                                        arr[7] = Convert.ToString(dic["fecha"]);
                                        arr[8] = Convert.ToString(dic["source"]);
                                        arr[9] = Convert.ToString(dic["descripcion"]);
                                        arr[10] = Convert.ToString(dic["referencia"]);
                                        arr[11] = Convert.ToString(dic["ANAL_T0"]);
                                        arr[12] = Convert.ToString(dic["ANAL_T1"]);
                                        arr[13] = Convert.ToString(dic["ANAL_T2"]);
                                        arr[14] = Convert.ToString(dic["ANAL_T3"]);
                                        arr[15] = Convert.ToString(dic["ANAL_T4"]);
                                        arr[16] = Convert.ToString(dic["ANAL_T5"]);
                                        arr[17] = Convert.ToString(dic["ANAL_T6"]);
                                        arr[18] = Convert.ToString(dic["ANAL_T7"]);
                                        arr[19] = Convert.ToString(dic["ANAL_T8"]);
                                        arr[20] = Convert.ToString(dic["ANAL_T9"]);

                                        itm = new ListViewItem(arr);
                                        listaPoliza.Items.Add(itm);
                                    }
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
}
