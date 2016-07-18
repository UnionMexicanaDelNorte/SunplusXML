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
    public partial class Detalle3 : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.MenuItem menuItem33;

        System.Windows.Forms.ContextMenu contextMenu2;
        public Detalle3()
        {
            InitializeComponent();
        }
        public String rfcGlobal;
        public int tipo;
        public String anioGlobal;
        public Detalle3(String rfc, String STATUS, String anio)
        {
            InitializeComponent();
            rfcGlobal = rfc;
            if (STATUS.Equals("Cancelada de Gastos"))
            {
                tipo = 0;
            }
            if (STATUS.Equals("Gastos"))
            {
                tipo = 1;
            }
            if (STATUS.Equals("Ingresos"))
            {
                tipo = 2;
            }
            if (STATUS.Equals("Cancelado de Ingresos"))
            {
                tipo = 3;
            }
            anioGlobal = anio;
        }

        private void Detalle3_Load(object sender, EventArgs e)
        {
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            lineasList.Location = new Point(0, 0);
            lineasList.Size = new Size(width, height);
            listaFinal = new List<Dictionary<string, object>>();
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            listaFinal.Clear();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT total,ruta,nombreArchivoPDF,nombreArchivoXML,fechaExpedicion, rfc,razonSocial,folio,folioFiscal FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE rfc = '"+rfcGlobal+"' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '"+anioGlobal+"' AND STATUS = '"+tipo+"'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(0))), 2);
                                String ruta = reader.GetString(1);
                                String nombreArchivoPDF = reader.GetString(2);
                                String nombreArchivoXML = reader.GetString(3);
                                String fechaExpedicion = Convert.ToString(reader.GetDateTime(4));
                                String rfc = reader.GetString(5);
                                String razon = reader.GetString(6);
                                String folio = reader.GetString(7);
                                String folioFiscal = reader.GetString(8);
                               
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("ruta", ruta);
                                dictionary.Add("nombreArchivoPDF", nombreArchivoPDF);
                                dictionary.Add("nombreArchivoXML", nombreArchivoXML);
                                dictionary.Add("fechaExpedicion", fechaExpedicion);
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("razon", razon);
                                dictionary.Add("folio", folio);
                                dictionary.Add("folioFiscal", folioFiscal);
                                dictionary.Add("total", total);
                                listaFinal.Add(dictionary);
                            }//while


                            lineasList.Clear();
                            lineasList.View = View.Details;
                            lineasList.GridLines = true;
                            lineasList.FullRowSelect = true;
                            lineasList.Columns.Add("ruta", 0);
                            lineasList.Columns.Add("nombreArchivoPDF", 0);
                            lineasList.Columns.Add("nombreArchivoXML", 0);
                            lineasList.Columns.Add("Folio", 70);
                            lineasList.Columns.Add("Fecha", 180);
                            lineasList.Columns.Add("Cantidad", 200);
                            lineasList.Columns.Add("Razon Social", 300);
                            lineasList.Columns.Add("Folio Fiscal", 300);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("ruta"))
                                {
                                    string[] arr = new string[9];
                                    ListViewItem itm;
                                    arr[0] = Convert.ToString(dic["ruta"]);
                                    arr[1] = Convert.ToString(dic["nombreArchivoPDF"]);
                                    arr[2] = Convert.ToString(dic["nombreArchivoXML"]);
                                    arr[3] = Convert.ToString(dic["folio"]);
                                    arr[4] = Convert.ToString(dic["fechaExpedicion"]);
                                    arr[5] = String.Format("{0:n}", Convert.ToDouble(dic["total"]));
                                    arr[6] = Convert.ToString(dic["razon"]);
                                    arr[7] = Convert.ToString(dic["folioFiscal"]);
                                    itm = new ListViewItem(arr);
                                    lineasList.Items.Add(itm);
                                }
                            }
                        }
                    }//using
                }
            }
            catch (SqlException ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            
        }
    }
}
