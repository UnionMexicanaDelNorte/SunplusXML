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
    public partial class Canceladas : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public double totalCanceladasSAT { get; set; }
        public double totalContabilizadoCanceladasSAT { get; set; }
        public bool noentres { get; set; }
       
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
        public Canceladas()
        {
            InitializeComponent();
        }
        public void actualiza()
        {
            if(tipoCombo.Items.Count > 0 && periodosCombo.Items.Count>0)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Item itm = (Item)periodosCombo.SelectedItem;
                Item itm2 = (Item)tipoCombo.SelectedItem;
                int tipo = itm2.Value;
                 String periodo = itm.Name;
                 String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                 canceladasList.Items.Clear();
                listaFinal.Clear();

                totalCanceladasSAT = 0;
                totalContabilizadoCanceladasSAT = 0;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        //canceladas SAT
                        String queryXML = "SELECT rfc,SUM(total) as total,razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodo + "' AND STATUS = '"+tipo+"' GROUP BY rfc,razonSocial order by rfc asc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(1)));
                                    String rfc = reader.GetString(0);
                                    String razonSocial = reader.GetString(2);


                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("maximo", total);
                                    dictionary.Add("rfc", rfc);
                                    dictionary.Add("enlazado", 0);
                                    dictionary.Add("razonSocial", razonSocial);

                                    listaFinal.Add(dictionary);
                                }
                                double amount = 0;
                                String folioFiscal = "";
                                String queryFISCAL = "SELECT FOLIO_FISCAL,AMOUNT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml]";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(1)));
                                            folioFiscal = readerFISCAL.GetString(0);
                                            //ingresos
                                            String queryFISCAL2 = "SELECT rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7)  = '" + periodo + "' AND STATUS = '"+tipo+"'";
                                            using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                            {
                                                SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                                if (readerFISCAL2.HasRows)
                                                {
                                                    while (readerFISCAL2.Read())
                                                    {
                                                        String rfcAux = Convert.ToString(readerFISCAL2.GetString(0));
                                                        foreach (Dictionary<string, object> dic in listaFinal)
                                                        {
                                                            if (dic["rfc"].Equals(rfcAux))
                                                            {
                                                                dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }


                                canceladasList.View = View.Details;
                                canceladasList.GridLines = true;
                                canceladasList.FullRowSelect = true;
                                canceladasList.Columns.Add("Razon Social", 350);
                                canceladasList.Columns.Add("RFC", 150);
                                canceladasList.Columns.Add("En el SAT", 150);
                                canceladasList.Columns.Add("Contabilizado", 150);
                                foreach (Dictionary<string, object> dic in listaFinal)
                                {
                                    if (dic.ContainsKey("rfc"))
                                    {
                                        string[] arr = new string[5];
                                        ListViewItem itm3;
                                        //add items to ListView
                                        arr[0] = Convert.ToString(dic["razonSocial"]);
                                        arr[1] = Convert.ToString(dic["rfc"]);
                                        arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["maximo"]));
                                        arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["enlazado"]));
                                        totalCanceladasSAT += Convert.ToDouble(dic["maximo"]);
                                        totalContabilizadoCanceladasSAT += Convert.ToDouble(dic["enlazado"]);
                                        itm3 = new ListViewItem(arr);
                                        canceladasList.Items.Add(itm3);
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
                totalCanceladasLabel.Text = "Cancelado en el SAT: $" + totalCanceladasSAT + " Contabilizado: $" + totalContabilizadoCanceladasSAT;
         
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
               
            }
        }
        private void Canceladas_Load(object sender, EventArgs e)
        {
            canceladasList.ItemSelectionChanged += canceladasList_ItemSelectionChanged;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            canceladasList.Location = new Point(50, 75);
            canceladasList.Size = new Size(width -150, height-175);
            listaFinal = new  List<Dictionary<string, object>> ();
            totalCanceladasLabel.Location = new Point(50, height - 100);
            tipoCombo.Items.Add(new Item("Gasto", 0));
            tipoCombo.Items.Add(new Item("Ingreso", 3));
            tipoCombo.SelectedIndex = 0;       
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML]";
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
        
        private void canceladasList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (canceladasList.SelectedItems.Count > 0 && e.IsSelected)
            {

                String rfc = canceladasList.SelectedItems[0].SubItems[1].Text.Trim();
                Item itm2 = (Item)tipoCombo.SelectedItem;
                int tipo = itm2.Value;
                Item itm = (Item)periodosCombo.SelectedItem;
                String periodo = itm.Name;

                Detalle1 form = new Detalle1(rfc, tipo, periodo);

               /* foreach (ListViewItem i in canceladasList.SelectedItems)
                {
                    i.Selected = false;
                }*/
                form.ShowDialog();
                //actualiza();
            }
        }

        private void tipoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        private void canceladasList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
        }
    }
}
