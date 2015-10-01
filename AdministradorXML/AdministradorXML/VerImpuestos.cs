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
            siguiente = true;
            
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            impuestosList.Location = new Point(50, 50);
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
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]";
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
                            var periodo = reader.GetString(0);
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
            if (tipoCombo.Items.Count > 0 && periodosCombo.Items.Count > 0)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Item itm = (Item)periodosCombo.SelectedItem;
                Item itm2 = (Item)tipoCombo.SelectedItem;
                int tipo = itm2.Value;
                String periodo = itm.Name;
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                impuestosList.Clear();
                listaFinal.Clear();

                totalImpuestosSAT = 0;
                totalContabilizadoImpuestosSAT = 0;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        //canceladas SAT
                        String queryXML = "SELECT f.ruta,f.nombreArchivoXML,f.nombreArchivoPDF,f.fechaExpedicion,f.rfc,f.razonSocial,f.total,f.folio,f.folioFiscal ,f.STATUS ,x.impuesto ,x.importe ,x.tasa FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[impuestos] x on x.folioFiscal = f.folioFiscal WHERE f.STATUS = '"+tipo+"' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '"+periodo+"' order by x.importe desc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    String ruta = reader.GetString(0);
                                    String nombreArchivoXML = reader.GetString(1);
                                    String nombreArchivoPDF = reader.GetString(2);
                                    String fechaExpedicion = reader.GetDateTime(3).ToString().Substring(0, 10);
                                    String rfc = reader.GetString(4);
                                    String razonSocial = reader.GetString(5);
                                   



                                    double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(6)));
                                    String folio = reader.GetString(7);
                                    String folioFiscal = reader.GetString(8);
                                    String impuesto = reader.GetString(10);
                                   
                                    double importe = Convert.ToDouble(Math.Abs(reader.GetDouble(11)));
                                    double tasa = Convert.ToDouble(Math.Abs(reader.GetDouble(12)));
                                 

                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("ruta", ruta);
                                    dictionary.Add("nombreArchivoXML", nombreArchivoXML);
                                    dictionary.Add("nombreArchivoPDF", nombreArchivoPDF);
                                    dictionary.Add("fechaExpedicion", fechaExpedicion);
                                    dictionary.Add("rfc", rfc);
                                    dictionary.Add("razonSocial", razonSocial);
                                  //  dictionary.Add("total", total);
                                    dictionary.Add("folio", folio);
                                    dictionary.Add("folioFiscal", folioFiscal);
                                    dictionary.Add("impuesto", impuesto);
                                    dictionary.Add("importe", importe);
                                    dictionary.Add("tasa", tasa);
                                    listaFinal.Add(dictionary);
                                }



                                impuestosList.View = View.Details;
                                impuestosList.GridLines = true;
                                impuestosList.FullRowSelect = true;
                                impuestosList.Columns.Add("ruta", 0);
                                impuestosList.Columns.Add("nombreArchivoXML", 0);
                                impuestosList.Columns.Add("nombreArchivoPDF", 0);
                                impuestosList.Columns.Add("Fecha", 100);

                                impuestosList.Columns.Add("RFC", 100);
                                impuestosList.Columns.Add("Razon Social", 250);
                                impuestosList.Columns.Add("Folio", 80);
                                impuestosList.Columns.Add("Folio Fiscal", 200);
                                impuestosList.Columns.Add("Impuesto", 120);
                                impuestosList.Columns.Add("Importe", 150);
                                impuestosList.Columns.Add("Tasa", 50);
                                foreach (Dictionary<string, object> dic in listaFinal)
                                {
                                    if (dic.ContainsKey("rfc"))
                                    {
                                        string[] arr = new string[12];
                                        ListViewItem itm3;
                                        //add items to ListView
                                        arr[0] = Convert.ToString(dic["ruta"]);
                                        arr[1] = Convert.ToString(dic["nombreArchivoXML"]);
                                        arr[2] = Convert.ToString(dic["nombreArchivoPDF"]);
                                        arr[3] = Convert.ToString(dic["fechaExpedicion"]);
                                        arr[4] = Convert.ToString(dic["rfc"]);
                                        arr[5] = Convert.ToString(dic["razonSocial"]);
                                        arr[6] = Convert.ToString(dic["folio"]);
                                        arr[7] = Convert.ToString(dic["folioFiscal"]);
                                        arr[8] = Convert.ToString(dic["impuesto"]);
                                        arr[9] = Convert.ToString(dic["importe"]);
                                        arr[10] = Convert.ToString(dic["tasa"]);


                                        totalImpuestosSAT += Convert.ToDouble(dic["importe"]);
                                        itm3 = new ListViewItem(arr);
                                        impuestosList.Items.Add(itm3);
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
                totalImpuestosLabel.Text = "Total de impuestos pagados en el SAT: $" + Math.Round(totalImpuestosSAT,2).ToString() + " En el periodo: " + periodo;
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

    }
}
