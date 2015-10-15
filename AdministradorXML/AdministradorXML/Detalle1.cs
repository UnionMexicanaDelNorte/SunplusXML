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
    public partial class Detalle1 : Form
    {
        public String periodoGlobal { get; set; }
        public int tipoDeContabilidadGlobal { get; set; }
        public String rfcGlobal { get; set; }
        public List<Dictionary<string, object>> listaCandidatos { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public Timer tmr { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.MenuItem menuItem33;
    
        System.Windows.Forms.ContextMenu contextMenu2;

        System.Windows.Forms.MenuItem menuItem11;
        System.Windows.Forms.MenuItem menuItem22;
       
        System.Windows.Forms.ContextMenu contextMenu22;


        public Detalle1()
        {
            InitializeComponent();
        }
        public Detalle1(String rfc, int tipo, String periodo)
        {
            InitializeComponent();
            rfcGlobal = rfc;
            tipoDeContabilidadGlobal = tipo;
            periodoGlobal = periodo;
        }
        public void ligarXML(object sender, EventArgs e)
        {
            double maximo = Math.Round( Convert.ToDouble( facturasList.SelectedItems[0].SubItems[2].Text),2);
            String folio_fiscal = facturasList.SelectedItems[0].SubItems[6].Text;
            Ligar ligar = new Ligar(folio_fiscal,maximo,tipoDeContabilidadGlobal);
            ligar.ShowDialog();
            ligar.TopMost = true;
            rellena();
        }
        public void VerPDF2(object sender, EventArgs e)
        {
            int cuantos = ligadasList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = ligadasList.SelectedItems[0].SubItems[7].Text;
                String ruta = ligadasList.SelectedItems[0].SubItems[8].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;

                System.Diagnostics.Process.Start(nombre);
            }
        }
        public void VerXML(object sender, EventArgs e)
        {
            int cuantos = facturasList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = facturasList.SelectedItems[0].SubItems[7].Text;
                String ruta = facturasList.SelectedItems[0].SubItems[5].Text;
                if (pdf.Equals("") || ruta.Equals(""))
                {

                }
                else
                {
                    String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                    System.Diagnostics.Process.Start(nombre);
                }

            }
        }
        public void VerPDF(object sender, EventArgs e)
        {
            int cuantos = facturasList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String pdf = facturasList.SelectedItems[0].SubItems[4].Text;
                String ruta = facturasList.SelectedItems[0].SubItems[5].Text;
                if(pdf.Equals("") || ruta.Equals(""))
                {

                }
                else
                {
                    String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                    System.Diagnostics.Process.Start(nombre);
                }
                
            }
        }
        public void desligarXML(object sender, EventArgs e)
        {
            String FOLIO_FISCAL = ligadasList.SelectedItems[0].SubItems[0].Text;
            String BUNIT = ligadasList.SelectedItems[0].SubItems[1].Text;
            String JRNAL_NO = ligadasList.SelectedItems[0].SubItems[2].Text;
            String JRNAL_LINE = ligadasList.SelectedItems[0].SubItems[3].Text;
            String consecutivo = ligadasList.SelectedItems[0].SubItems[5].Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
  
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '" + FOLIO_FISCAL + "' AND BUNIT = '" + BUNIT + "' AND JRNAL_NO = " + JRNAL_NO + " AND JRNAL_LINE = " + JRNAL_LINE + " AND consecutivo = " + consecutivo ;
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
        public void muestra(object sender, EventArgs e)
        {
            tmr.Stop();
            BringToFront();
        }
        private void Detalle1_Load(object sender, EventArgs e)
        {
            if (tipoDeContabilidadGlobal == 2)
            {
                tipoDeFacturasLabel.Text = "Facturas de Ingresos vigentes del RFC: "+rfcGlobal+" del periodo: "+periodoGlobal;
                tipoDeEnlazadosLabel.Text = "Facturas de Ingresos ya enlazadas del RFC: "+rfcGlobal+" del periodo: "+periodoGlobal;
            }
            else
            {
                if(tipoDeContabilidadGlobal==1)
                {
                    tipoDeFacturasLabel.Text = "Facturas de Gastos vigentes del RFC: "+rfcGlobal+" del periodo: "+periodoGlobal;
                    tipoDeEnlazadosLabel.Text = "Facturas de Gastos ya enlazadas del RFC: "+rfcGlobal+" del periodo: "+periodoGlobal;
                }
                else
                {
                    if (tipoDeContabilidadGlobal == 0)
                    {
                        tipoDeFacturasLabel.Text = "Facturas canceladas de Gastos  del RFC: " + rfcGlobal + " del periodo: " + periodoGlobal;
                        tipoDeEnlazadosLabel.Text = "Facturas  canceladas de Gastos ya enlazadas del RFC: " + rfcGlobal + " del periodo: " + periodoGlobal;
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal == 3)
                        {
                            tipoDeFacturasLabel.Text = "Facturas canceladas de Ingresos vigentes del RFC: " + rfcGlobal + " del periodo: " + periodoGlobal;
                            tipoDeEnlazadosLabel.Text = "Facturas canceladas de Ingresos ya enlazadas del RFC: " + rfcGlobal + " del periodo: " + periodoGlobal;
                        }
                    }

                }
            }
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem2 = new System.Windows.Forms.MenuItem();
      
            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2, menuItem1 });
            menuItem1.Index = 1;
            menuItem1.Text = "Desligar Factura";
            menuItem1.Click += desligarXML;
            menuItem2.Index = 0;
            menuItem2.Text = "Ver pdf";
            menuItem2.Click += VerPDF2;
            ligadasList.ContextMenu = contextMenu2;

           
                contextMenu22 = new System.Windows.Forms.ContextMenu();
                menuItem11 = new System.Windows.Forms.MenuItem();
                menuItem22 = new System.Windows.Forms.MenuItem();
                menuItem33 = new System.Windows.Forms.MenuItem();
             
                if (tipoDeContabilidadGlobal == 1 || tipoDeContabilidadGlobal == 2)
                {
                    contextMenu22.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem22, menuItem11, menuItem33 });
                    menuItem11.Index = 2;
                    menuItem11.Text = "Ligar XML";
                    menuItem11.Click += ligarXML;
                }
                else
                {
                    contextMenu22.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem22, menuItem33 });
                }
                menuItem22.Index = 0;
                menuItem22.Text = "Ver PDF";
                menuItem22.Click += VerPDF;
                menuItem33.Index = 1;
                menuItem33.Text = "Ver XML";
                menuItem33.Click += VerXML;
                facturasList.ContextMenu = contextMenu22;
            

           
            
            rellena();
           
        }
        private void rellena()
        {
            
            String RFC = rfcGlobal;
            String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
  
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidadGlobal == 1)
                    {
                        queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodoGlobal + "' AND rfc = '" + RFC + "' AND STATUS =  '1' order by fechaExpedicion asc";
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal == 2)
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodoGlobal + "' AND rfc = '" + RFC + "' AND STATUS =  '2' order by fechaExpedicion asc";
                        }
                        else
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, ISNULL(nombreArchivoPDF,'') as nombreArchivoPDF, fechaExpedicion ,ISNULL(ruta,'') as ruta,ISNULL(nombreArchivoXML,'') as nombreArchivoXML,ISNULL(folio,'') as folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodoGlobal + "' AND rfc = '" + RFC + "' AND STATUS in('" + tipoDeContabilidadGlobal + "') order by fechaExpedicion asc";
                        }
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
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(0))), 2);
                                String folioFiscal = reader.GetString(1);
                                String rfc = reader.GetString(2);
                                String nombreArchivoPDF = "";
                                String nombreArchivoXML = "";
                                String razonSocial = reader.GetString(3);
                                String fechaExpedicion = Convert.ToString(reader.GetDateTime(5));
                                String ruta = "";
                                String folio = "";
                             //   if(tipoDeContabilidadGlobal==1 || tipoDeContabilidadGlobal==2)
                               // {
                                    nombreArchivoPDF = reader.GetString(4);
                                    ruta = reader.GetString(6);
                                    folio = reader.GetString(8);
                                    nombreArchivoXML = reader.GetString(7);
                                //}
                              
                                //cuanto esta enlazado de esa factura
                                String queryFISCAL = "SELECT f.FOLIO_FISCAL,f.BUNIT,f.JRNAL_NO,f.JRNAL_LINE,f.AMOUNT,f.consecutivo , x.rfc,x.nombreArchivoPDF, x.ruta, x.razonSocial, s.DESCRIPTN FROM ["+Properties.Settings.Default.databaseFiscal+"].[dbo].[FISCAL_xml] f INNER JOIN ["+Properties.Settings.Default.databaseFiscal+"].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN ["+Properties.Settings.Default.sunDatabase+"].[dbo].["+Properties.Settings.Default.sunUnidadDeNegocio+"_"+Properties.Settings.Default.sunLibro+"_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE FOLIO_FISCAL = '"+folioFiscal+"'";
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
                                        dictionary.Add("maximo", total);
                                        dictionary.Add("folioFiscal", folioFiscal);
                                        dictionary.Add("rfc", rfc);
                                        dictionary.Add("razonSocial", razonSocial);
                                        dictionary.Add("nombreArchivoPDF", nombreArchivoPDF);
                                        dictionary.Add("nombreArchivoXML", nombreArchivoXML);
                                        dictionary.Add("fechaExpedicion", fechaExpedicion);
                                        dictionary.Add("ruta", ruta);
                                        dictionary.Add("folio", folio);
                                        listaCandidatos.Add(dictionary);
                                    }
                                }//using
                            }//while

                            ligadasList.Clear();
                            ligadasList.View = View.Details;
                            ligadasList.GridLines = true;
                            ligadasList.FullRowSelect = true;
                            ligadasList.Columns.Add("FOLIO_FISCAL", 100);
                            ligadasList.Columns.Add("Unidad de negocio", 100);
                            ligadasList.Columns.Add("Diario", 90);
                            ligadasList.Columns.Add("Linea", 90);
                            ligadasList.Columns.Add("Cantidad", 100);
                            ligadasList.Columns.Add("Consecutivo", 100);
                            ligadasList.Columns.Add("RFC", 100);
                            ligadasList.Columns.Add("nombreArchivoPDF2", 0);
                            ligadasList.Columns.Add("ruta2", 0);
                            ligadasList.Columns.Add("razonSocial2", 0);
                            ligadasList.Columns.Add("DESCRIPTN", 150);
                            

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
                                  //  arr[4] = Convert.ToString(dic["amount"]);
                                    arr[4] = String.Format("{0:n}", Convert.ToDouble(dic["amount"]));
                                
                                    arr[5] = Convert.ToString(dic["consecutivo"]);
                                    arr[6] = Convert.ToString(dic["rfcDelMomento"]);
                                    arr[7] = Convert.ToString(dic["nombreArchivoPDF2"]);
                                    arr[8] = Convert.ToString(dic["ruta2"]);
                                    arr[9] = Convert.ToString(dic["razonSocial2"]);
                                    arr[10] = Convert.ToString(dic["DESCRIPTN"]);
                                    itm = new ListViewItem(arr);
                                    ligadasList.Items.Add(itm);
                                }
                            }

                            facturasList.Clear();
                            facturasList.View = View.Details;
                            facturasList.GridLines = true;
                            facturasList.FullRowSelect = true;
                            facturasList.Columns.Add("Fecha", 100);
                            facturasList.Columns.Add("RFC", 100);
                            facturasList.Columns.Add("Cantidad", 100);
                            facturasList.Columns.Add("Razon Social", 350);
                            facturasList.Columns.Add("RutaPDF", 0);
                            facturasList.Columns.Add("ruta", 0);
                            facturasList.Columns.Add("folioFiscal", 130);
                            facturasList.Columns.Add("nombreArchivoXML", 0);
                            facturasList.Columns.Add("Folio", 100);

                            foreach (Dictionary<string, object> dic in listaCandidatos)
                            {
                                if (dic.ContainsKey("folioFiscal"))
                                {
                                    string[] arr = new string[10];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["fechaExpedicion"]);
                                    arr[1] = Convert.ToString(dic["rfc"]);
                                    //arr[2] = Convert.ToString(dic["maximo"]);
                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["maximo"]));
                                
                                    arr[3] = Convert.ToString(dic["razonSocial"]);
                                    arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
                                    arr[5] = Convert.ToString(dic["ruta"]);
                                    arr[6] = Convert.ToString(dic["folioFiscal"]);
                                    arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
                                    arr[8] = Convert.ToString(dic["folio"]);
                                    itm = new ListViewItem(arr);
                                    facturasList.Items.Add(itm);
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
        private void facturasList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void ligadasList_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
