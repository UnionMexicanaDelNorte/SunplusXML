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
    public partial class ImpuestosDetalle2 : Form
    {
        public String periodo { get; set; }
        public String impuesto { get; set; }
        public int tipo { get; set; }
        public String rfc { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
       
        System.Windows.Forms.ContextMenu contextMenu2;

        public ImpuestosDetalle2()
        {
            InitializeComponent();
        }
         public ImpuestosDetalle2(String p, String i, int t,String r)
        {
            InitializeComponent();
            periodo = p;
            impuesto = i;
            tipo = t;
            rfc = r;
        }
         public void VerPDF(object sender, EventArgs e)
         {
             int cuantos = facturasList.SelectedItems.Count;
             if (cuantos > 0)
             {
                 String pdf = facturasList.SelectedItems[0].SubItems[2].Text;
                 String ruta = facturasList.SelectedItems[0].SubItems[1].Text;
                 String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                 System.Diagnostics.Process.Start(nombre);
             }
         }
         public void VerXML(object sender, EventArgs e)
         {
             int cuantos = facturasList.SelectedItems.Count;
             if (cuantos > 0)
             {
                 String pdf = facturasList.SelectedItems[0].SubItems[3].Text;
                 String ruta = facturasList.SelectedItems[0].SubItems[1].Text;
                 String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                 System.Diagnostics.Process.Start(nombre);
             }
         }
        private void ImpuestosDetalle2_Load(object sender, EventArgs e)
        {
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2, menuItem1 });
            menuItem1.Index = 1;
            menuItem1.Text = "Ver PDF";
            menuItem1.Click += VerPDF;
            menuItem2.Index = 0;
            menuItem2.Text = "Ver XML";
            menuItem2.Click += VerXML;
            facturasList.ContextMenu = contextMenu2;

            listaFinal = new List<Dictionary<string, object>>();
            if (tipo == 1)
            {
                cambiarLabel.Text = "" + impuesto + " del periodo: " + periodo + " del gasto del RFC: "+rfc;
            }
            else
            {
                cambiarLabel.Text = "" + impuesto + " del periodo: " + periodo + " de ingreso del RFC: " + rfc;
            }
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT f.folioFiscal,f.ruta,f.nombreArchivoPDF,f.nombreArchivoXML,f.folio,f.fechaExpedicion,i.impuesto,i.importe FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[impuestos] i on i.folioFiscal = f.folioFiscal WHERE f.STATUS = '"+tipo+"' AND SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(10)),1,"+periodo.Length+") = '"+periodo+"' AND i.impuesto = '"+impuesto+"' AND f.rfc = '"+rfc+"' order by f.fechaExpedicion asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String folioFiscal = reader.GetString(0);
                                String ruta = reader.GetString(1);
                                String nombreArchivoPDF = reader.GetString(2);
                                String nombreArchivoXML = reader.GetString(3);
                                String folio = reader.GetString(4);
                                String fechaExpedicion = Convert.ToString( reader.GetDateTime(5)).Substring(0,10);
                                String impuesto1 = reader.GetString(6);
                                double importe = reader.GetDouble(7);

                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("folioFiscal", folioFiscal);
                                dictionary.Add("ruta", ruta);
                                dictionary.Add("nombreArchivoPDF", nombreArchivoPDF);
                                dictionary.Add("nombreArchivoXML", nombreArchivoXML);
                                dictionary.Add("folio", folio);
                                dictionary.Add("fechaExpedicion", fechaExpedicion);
                                dictionary.Add("impuesto1", impuesto1);
                                dictionary.Add("importe", importe);
                                listaFinal.Add(dictionary);
                            }
                            facturasList.View = View.Details;
                            facturasList.GridLines = true;
                            facturasList.FullRowSelect = true;
                            facturasList.Columns.Add("folioFiscal", 0);
                            facturasList.Columns.Add("ruta", 0);
                            facturasList.Columns.Add("nombreArchivoPDF", 0);
                            facturasList.Columns.Add("nombreArchivoXML", 0);
                            facturasList.Columns.Add("Folio", 100);
                            facturasList.Columns.Add("Fecha expedicion", 150);
                            facturasList.Columns.Add("Impuesto", 130);
                            facturasList.Columns.Add("Importe", 250);
                        

                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("folioFiscal"))
                                {
                                    string[] arr = new string[12];
                                    ListViewItem itm3;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["folioFiscal"]);
                                    arr[1] = Convert.ToString(dic["ruta"]);
                                    arr[2] = Convert.ToString(dic["nombreArchivoPDF"]);
                                    arr[3] = Convert.ToString(dic["nombreArchivoXML"]);
                                    arr[4] = Convert.ToString(dic["folio"]);
                                    arr[5] = Convert.ToString(dic["fechaExpedicion"]);
                                    arr[6] = Convert.ToString(dic["impuesto1"]);
                                    arr[7] = String.Format("{0:n}", Convert.ToDouble(dic["importe"]));
                                    itm3 = new ListViewItem(arr);
                                    facturasList.Items.Add(itm3);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
