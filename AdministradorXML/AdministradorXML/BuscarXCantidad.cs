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
    public partial class BuscarXCantidad : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
      
        public BuscarXCantidad()
        {
            InitializeComponent();
        }

        private void BuscarXCantidad_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
                
        }
        private void rellena()
        {

            String cantidadS = cantidad.Text;
            String queryXML = "";
                   
            String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    queryXML = "SELECT STATUS, fechaCancelacion, fechaExpedicion, total, rfc,razonSocial, folioFiscal FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE total = "+cantidadS+" order by fechaExpedicion desc";
                    listaFinal.Clear();

                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String STATUS = reader.GetString(0);
                                String fechaCancelacion = "";
                                if (!reader.IsDBNull(1))
                                {
                                    fechaCancelacion = Convert.ToString(reader.GetDateTime(1));
                                }
                                String fechaExpedicion = Convert.ToString(reader.GetDateTime(2));
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(3))), 2);
                              
                                String rfc = reader.GetString(4);
                                String razonSocial = reader.GetString(5);
                                String folioFiscal = reader.GetString(6);
                                String palabra = "";
                                if(STATUS.Equals("0"))
                                {
                                    palabra = "Cancelada Gasto";
                                }
                                if (STATUS.Equals("1"))
                                {
                                    palabra = "Gasto";
                                }
                                if (STATUS.Equals("2"))
                                {
                                    palabra = "Ingreso";
                                }
                                if (STATUS.Equals("3"))
                                {
                                    palabra = "Cancelada Ingreso";
                                }


                                
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("STATUS", palabra);
                                dictionary.Add("fechaCancelacion", fechaCancelacion);
                                dictionary.Add("fechaExpedicion", fechaExpedicion);
                                dictionary.Add("total", total);
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("razonSocial", razonSocial);
                                dictionary.Add("folioFiscal", folioFiscal);
                                listaFinal.Add(dictionary);
                            }//while

                            resultados.Clear();
                            resultados.View = View.Details;
                            resultados.GridLines = true;
                            resultados.FullRowSelect = true;
                            resultados.Columns.Add("Status", 100);
                            resultados.Columns.Add("Fecha cancelacion", 100);
                            resultados.Columns.Add("Fecha Expedicion", 100);
                            resultados.Columns.Add("Total", 100);
                            resultados.Columns.Add("RFC", 100);
                            resultados.Columns.Add("Razon Social", 100);
                            resultados.Columns.Add("Folio fiscal", 100);
                            

                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("STATUS"))
                                {
                                    string[] arr = new string[13];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["STATUS"]);
                                    arr[1] = Convert.ToString(dic["fechaCancelacion"]);
                                    arr[2] = Convert.ToString(dic["fechaExpedicion"]);
                                    arr[4] = Convert.ToString(dic["rfc"]);
                                    //  arr[4] = Convert.ToString(dicrfamount"]);
                                    arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["total"]));

                                    arr[5] = Convert.ToString(dic["razonSocial"]);
                                    arr[6] = Convert.ToString(dic["folioFiscal"]);
                                    
                                    itm = new ListViewItem(arr);
                                    resultados.Items.Add(itm);
                                }
                            }

                         
                        }
                    }//using
                }
            }
            catch (SqlException ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
                System.Windows.Forms.MessageBox.Show(queryXML+"-"+ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;

        }

        private void buscar_Click(object sender, EventArgs e)
        {
            rellena();
        }
    }
}
