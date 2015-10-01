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
    public partial class Detalle2 : Form
    {
        public String todasLasCuentasDeIngresos { get; set; }
        public String todasLasCuentasDeEgresos { get; set; }
        public String todasLasCuentasDeBalanza { get; set; }
        public Timer tmr { get; set; }
        public String periodoGlobal { get; set; }
        public int tipoDeContabilidadGlobal { get; set; }
        public String cuentaGlobal { get; set; }
        public List<Dictionary<string, object>> listaCandidatos { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.ContextMenu contextMenu2;

        System.Windows.Forms.MenuItem menuItem11;
        System.Windows.Forms.MenuItem menuItem22;
        System.Windows.Forms.ContextMenu contextMenu22;

        public Detalle2()
        {
            InitializeComponent();
        }
        public Detalle2(String cuenta, int tipo, String periodo, String ingreso, String egreso, String balanza)
        {
            InitializeComponent();
            cuentaGlobal = cuenta;
            tipoDeContabilidadGlobal = tipo;
            periodoGlobal = periodo;
            todasLasCuentasDeIngresos = ingreso;
            todasLasCuentasDeEgresos = egreso;
            todasLasCuentasDeBalanza = balanza;
        }
        public void muestra(object sender, EventArgs e)
        {
            tmr.Stop();
            BringToFront();
        }
        public void VerPDF2(object sender, EventArgs e)
        {
            int cuantos = enlazadosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = enlazadosList.SelectedItems[0].SubItems[7].Text;
                String ruta = enlazadosList.SelectedItems[0].SubItems[8].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;

                System.Diagnostics.Process.Start(nombre);
            }
        }

        public void VerPDF(object sender, EventArgs e)
        {
            int cuantos = diariosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = diariosList.SelectedItems[0].SubItems[4].Text;
                String ruta = diariosList.SelectedItems[0].SubItems[5].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                System.Diagnostics.Process.Start(nombre);
            }
        }
        public void DesligarFactura(object sender, EventArgs e)
        {
            String FOLIO_FISCAL = enlazadosList.SelectedItems[0].SubItems[0].Text;
            String BUNIT = enlazadosList.SelectedItems[0].SubItems[1].Text;
            String JRNAL_NO = enlazadosList.SelectedItems[0].SubItems[2].Text;
            String JRNAL_LINE = enlazadosList.SelectedItems[0].SubItems[3].Text;
            String consecutivo = enlazadosList.SelectedItems[0].SubItems[5].Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '" + FOLIO_FISCAL + "' AND BUNIT = '" + BUNIT + "' AND JRNAL_NO = " + JRNAL_NO + " AND JRNAL_LINE = " + JRNAL_LINE + " AND consecutivo = " + consecutivo;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    rellena();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void LigarFactura(object sender, EventArgs e)
        {
            double maximo = Math.Round(Convert.ToDouble(diariosList.SelectedItems[0].SubItems[2].Text), 2);
            String linea = diariosList.SelectedItems[0].SubItems[1].Text;
            String diario = diariosList.SelectedItems[0].SubItems[0].Text;
            
            Ligar2 ligar = new Ligar2(linea, diario, maximo, tipoDeContabilidadGlobal);
            ligar.ShowDialog();
            ligar.TopMost = true;
            rellena();
        }
        private void Detalle2_Load(object sender, EventArgs e)
        {
            if (tipoDeContabilidadGlobal == 2)
            {
                tipoDeCuentasLabel.Text = "Diarios de Ingresos vigentes de la cuenta: "+cuentaGlobal+" del periodo: "+periodoGlobal;
                enlazadosLabel.Text = "Diarios de Ingresos ya enlazados de la cuenta: " + cuentaGlobal + " del periodo: " + periodoGlobal;
            }
            else
            {
                if (tipoDeContabilidadGlobal == 1)
                {
                    tipoDeCuentasLabel.Text = "Diarios de Gastos vigentes de la cuenta: " + cuentaGlobal + " del periodo: " + periodoGlobal;
                    enlazadosLabel.Text = "Diarios de Gastos ya enlazados de la cuenta: " + cuentaGlobal + " del periodo: " + periodoGlobal;
                }
            }
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem2 = new System.Windows.Forms.MenuItem();
           
            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {  menuItem2 });
        
            menuItem2.Index = 0;
            menuItem2.Text = "Ligar Factura";
            menuItem2.Click += LigarFactura;
            diariosList.ContextMenu = contextMenu2;

            contextMenu22 = new System.Windows.Forms.ContextMenu();
            menuItem11 = new System.Windows.Forms.MenuItem();
            menuItem22 = new System.Windows.Forms.MenuItem();

            contextMenu22.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem11, menuItem22 });
            menuItem11.Index = 0;
            menuItem11.Text = "Ver PDF";
            menuItem11.Click += VerPDF2;
            menuItem22.Index = 1;
            menuItem22.Text = "Desligar Factura";
            menuItem22.Click += DesligarFactura;

            enlazadosList.ContextMenu = contextMenu22;


            rellena();
            tmr = new Timer();
            tmr.Interval = 500;
            tmr.Tick += muestra;
         //   tmr.Start();
        }
        private void rellena()
        {
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String year = periodoGlobal.Substring(0, 4);
            String month = periodoGlobal.Substring(5, 2);

            String periodoParaQuery = year + "0" + month;
          
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    if(tipoDeContabilidadGlobal==1)//gastos
                    {
                        queryXML = "SELECT s.JRNAL_NO, s.JRNAL_LINE,s.AMOUNT,s.DESCRIPTN,s.TRANS_DATETIME FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.D_C = 'D' AND s.ACCNT_CODE = '" + cuentaGlobal + "'";
                    }
                    else
                    {//ingresos
                        queryXML = "SELECT s.JRNAL_NO, s.JRNAL_LINE,s.AMOUNT,s.DESCRIPTN,s.TRANS_DATETIME FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.D_C = 'C' AND s.ACCNT_CODE = '" + cuentaGlobal + "'";
                    }
                    
                    listaFinal = new List<Dictionary<string, object>>();

                    listaCandidatos = new List<Dictionary<string, object>>();
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String JRNAL_NO1 = Convert.ToString(reader.GetInt32(0));
                                String JRNAL_LINE1 = Convert.ToString(reader.GetInt32(1));
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(2))), 2);
                                String DESCRIPTN1 = reader.GetString(3);
                                String TRANS_DATETIME = Convert.ToString( reader.GetDateTime(4));
                             
                                //cuanto esta enlazado de esa linea
                                String queryFISCAL = "SELECT f.FOLIO_FISCAL,f.BUNIT,f.JRNAL_NO,f.JRNAL_LINE,f.AMOUNT,f.consecutivo , x.rfc,x.nombreArchivoPDF, x.ruta, x.razonSocial, s.DESCRIPTN FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE f.JRNAL_NO = " + JRNAL_NO1 + " AND f.JRNAL_LINE = "+JRNAL_LINE1;
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            String FOLIO_FISCAL = readerFISCAL.GetString(0);
                                            String BUNIT = readerFISCAL.GetString(1);
                                            String JRNAL_NO = Convert.ToString(readerFISCAL.GetInt32(2));
                                            String JRNAL_LINE = Convert.ToString(readerFISCAL.GetInt32(3));
                                            String consecutivo = Convert.ToString(readerFISCAL.GetInt32(5));
                                            String rfcDelMomento = readerFISCAL.GetString(6);
                                            String nombreArchivoPDF2 = readerFISCAL.GetString(7);
                                            String ruta2 = readerFISCAL.GetString(8);
                                            String razonSocial2 = readerFISCAL.GetString(9);
                                            String DESCRIPTN = readerFISCAL.GetString(10);


                                            double amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(4)));


                                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                            dictionary.Add("FOLIO_FISCAL", FOLIO_FISCAL);
                                            dictionary.Add("BUNIT", BUNIT);
                                            dictionary.Add("JRNAL_NO", JRNAL_NO);
                                            dictionary.Add("JRNAL_LINE", JRNAL_LINE);
                                            dictionary.Add("amount", amount);
                                            dictionary.Add("consecutivo", consecutivo);
                                            dictionary.Add("rfcDelMomento", rfcDelMomento);
                                            dictionary.Add("nombreArchivoPDF2", nombreArchivoPDF2);
                                            dictionary.Add("ruta2", ruta2);
                                            dictionary.Add("razonSocial2", razonSocial2);
                                            dictionary.Add("DESCRIPTN", DESCRIPTN);
                                            listaFinal.Add(dictionary);

                                            total = total - amount;
                                        }

                                    }
                                    if (total > 0)
                                    {
                                       
                                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                        dictionary.Add("JRNAL_NO1", JRNAL_NO1);
                                        dictionary.Add("JRNAL_LINE1", JRNAL_LINE1);
                                        dictionary.Add("total", total);
                                        dictionary.Add("DESCRIPTN1", DESCRIPTN1);
                                        dictionary.Add("TRANS_DATETIME", TRANS_DATETIME);
                                        listaCandidatos.Add(dictionary);
                                    }
                                }//using
                            }//while

                            enlazadosList.Clear();
                            enlazadosList.View = View.Details;
                            enlazadosList.GridLines = true;
                            enlazadosList.FullRowSelect = true;
                            enlazadosList.Columns.Add("FOLIO_FISCAL", 100);
                            enlazadosList.Columns.Add("Unidad de negocio", 100);
                            enlazadosList.Columns.Add("Diario", 90);
                            enlazadosList.Columns.Add("Linea", 90);
                            enlazadosList.Columns.Add("Cantidad", 100);
                            enlazadosList.Columns.Add("Consecutivo", 100);
                            enlazadosList.Columns.Add("RFC", 100);
                            enlazadosList.Columns.Add("nombreArchivoPDF2", 0);
                            enlazadosList.Columns.Add("ruta2", 0);
                            enlazadosList.Columns.Add("razonSocial2", 0);
                            enlazadosList.Columns.Add("DESCRIPTN", 150);


                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("FOLIO_FISCAL"))
                                {
                                    string[] arr = new string[12];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["FOLIO_FISCAL"]);
                                    arr[1] = Convert.ToString(dic["BUNIT"]);
                                    arr[2] = Convert.ToString(dic["JRNAL_NO"]);
                                    arr[3] = Convert.ToString(dic["JRNAL_LINE"]);
                                    arr[4] = Convert.ToString(dic["amount"]);
                                    arr[5] = Convert.ToString(dic["consecutivo"]);
                                    arr[6] = Convert.ToString(dic["rfcDelMomento"]);
                                    arr[7] = Convert.ToString(dic["nombreArchivoPDF2"]);
                                    arr[8] = Convert.ToString(dic["ruta2"]);
                                    arr[9] = Convert.ToString(dic["razonSocial2"]);
                                    arr[10] = Convert.ToString(dic["DESCRIPTN"]);
                                    itm = new ListViewItem(arr);
                                    enlazadosList.Items.Add(itm);
                                }
                            }

                            diariosList.Clear();
                            diariosList.View = View.Details;
                            diariosList.GridLines = true;
                            diariosList.FullRowSelect = true;
                            diariosList.Columns.Add("Diario", 80);
                            diariosList.Columns.Add("Linea", 80);
                            diariosList.Columns.Add("Cantidad", 100);
                            diariosList.Columns.Add("Descripción", 250);
                            diariosList.Columns.Add("Fecha", 200);
                            

                            foreach (Dictionary<string, object> dic in listaCandidatos)
                            {
                                if (dic.ContainsKey("JRNAL_NO1"))
                                {
                                    string[] arr = new string[6];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["JRNAL_NO1"]);
                                    arr[1] = Convert.ToString(dic["JRNAL_LINE1"]);
                                    arr[2] = Convert.ToString(dic["total"]);
                                    arr[3] = Convert.ToString(dic["DESCRIPTN1"]);
                                    arr[4] = Convert.ToString(dic["TRANS_DATETIME"]);
                                    itm = new ListViewItem(arr);
                                    diariosList.Items.Add(itm);
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

        private void enlazadosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*int cuantos = enlazadosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = enlazadosList.SelectedItems[0].SubItems[7].Text;
                String ruta = enlazadosList.SelectedItems[0].SubItems[8].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;

                System.Diagnostics.Process.Start(nombre);
            }*/
        }

        private void diariosList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
