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
using System.Xml;
namespace Liga
{
    public partial class Form1 : Form
    {
        public String lineaGlobal { get; set; }
        public String sourceGlobal { get; set; }
        public String cantidadGlobal { get; set; }
        public String unidadDeNegociosGlobal { get; set; }
        public String cuentaGlobal { get; set; }
        public String debitCreditGlobal { get; set; }
        public String tipoDeDiarioGlobal { get; set; }
        public String referenciaGlobal { get; set; }
    
        public String nombreArchivoXMLSun { get; set; }
        private double cantidadQueFalta { get; set; }
        public bool noPermitas{ get; set; }
        public int tipoDeContabilidadGlobal { get; set; }
      
        public List<Dictionary<string, object>> listaUUIDEnlazados { get; set; }
       
        public String connStringSun { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public List<Dictionary<string, object>> listaCandidatos { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.MenuItem menuItem3;
        System.Windows.Forms.ContextMenu contextMenu2;

        System.Windows.Forms.MenuItem menuItem31;
        System.Windows.Forms.MenuItem menuItem32;
        System.Windows.Forms.MenuItem menuItem33;
        System.Windows.Forms.ContextMenu contextMenu3;
        public Form1()
        {
            InitializeComponent();
            lineaGlobal = "ERROR";
            sourceGlobal = "ERROR";
            cantidadGlobal = "ERROR";
            unidadDeNegociosGlobal = "ERROR";
            cuentaGlobal = "ERROR";
            debitCreditGlobal="ERROR";
            tipoDeDiarioGlobal = "ERROR";
            referenciaGlobal = "ERROR";
        }

        public Form1(String [] args)
        {
            InitializeComponent();
            lineaGlobal = args[0];
            sourceGlobal = args[1];
            cantidadGlobal = args[2];
            unidadDeNegociosGlobal = args[3];
            cuentaGlobal = args[4];
            debitCreditGlobal = args[5];
            tipoDeDiarioGlobal = args[6];
            int i;
            referenciaGlobal = "";
            for (i = 7; i < args.Length;i++ )
            {
                if (i == 7)
                {
                    referenciaGlobal = args[i];
                }
                else
                {
                    referenciaGlobal = referenciaGlobal +" "+ args[i];
                }
                 
            }
        }

        public Form1(String linea, String source, String cantidad, String unidadDeNegocio, String ACCNT_CODE, String debitCredit, String tipoDeDiario, String referencia)
        {
            InitializeComponent();
            lineaGlobal = linea;
            sourceGlobal = source;
            cantidadGlobal = cantidad;
            unidadDeNegociosGlobal = unidadDeNegocio;
            cuentaGlobal = ACCNT_CODE;
            debitCreditGlobal = debitCredit;
            tipoDeDiarioGlobal = tipoDeDiario;
            referenciaGlobal = referencia;
                         
        }
        private void rellena()
        {
            String RFC = rfcTextBox.Text;
            String linea = lineaGlobal;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidadGlobal==1)
                    {
                        queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS =  '1' order by fechaExpedicion asc";
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal==2)
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS =  '2' order by fechaExpedicion asc";
                        }
                        else
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS in('1','2') order by fechaExpedicion asc";
                        }
                    }
                    listaCandidatos = new List<Dictionary<string, object>>();
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(0))),2);
                                String folioFiscal = reader.GetString(1);
                                String rfc = reader.GetString(2);
                                String nombreArchivoPDF = reader.GetString(4);
                                String nombreArchivoXML = reader.GetString(7);
                                String razonSocial = reader.GetString(3);
                                String fechaExpedicion = Convert.ToString(reader.GetDateTime(5));
                                String ruta = reader.GetString(6);
                                String folio = reader.GetString(8);
                              
                                //cuanto esta enlazado de esa factura
                                String queryFISCAL = "SELECT FOLIO_FISCAL,BUNIT,JRNAL_NO,JRNAL_LINE,AMOUNT,consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '" + folioFiscal + "'";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            double amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(4)));
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
                            listaDeCandidatos.Clear();
                            listaDeCandidatos.View = View.Details;
                            listaDeCandidatos.GridLines = true;
                            listaDeCandidatos.FullRowSelect = true;
                            listaDeCandidatos.Columns.Add("Fecha", 80);
                            listaDeCandidatos.Columns.Add("RFC", 100);
                            listaDeCandidatos.Columns.Add("Cantidad", 90);
                            listaDeCandidatos.Columns.Add("Razon Social", 190);
                            listaDeCandidatos.Columns.Add("RutaPDF", 0);
                            listaDeCandidatos.Columns.Add("ruta", 0);
                            listaDeCandidatos.Columns.Add("folioFiscal", 0);
                            listaDeCandidatos.Columns.Add("nombreArchivoXML", 0);
                            listaDeCandidatos.Columns.Add("Folio", 100);
                         
                            foreach (Dictionary<string, object> dic in listaCandidatos)
                            {
                                if (dic.ContainsKey("folioFiscal"))
                                {
                                    string[] arr = new string[10];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["fechaExpedicion"]);
                                    arr[1] = Convert.ToString(dic["rfc"]);
                                    arr[2] = Convert.ToString(dic["maximo"]);
                                    arr[3] = Convert.ToString(dic["razonSocial"]);
                                    arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
                                    arr[5] = Convert.ToString(dic["ruta"]);
                                    arr[6] = Convert.ToString(dic["folioFiscal"]);
                                    arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
                                    arr[8] = Convert.ToString(dic["folio"]);
                                    itm = new ListViewItem(arr);
                                    listaDeCandidatos.Items.Add(itm);
                                }
                            }
                        }
                    }//using
                }
            }
            catch (SqlException ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;

                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        
        }
        public void eliminarFacturaDeLaLista(object sender, EventArgs e)
        {
            if(facturasAsignadasList.SelectedItems.Count >0)
            {
                String Fecha = facturasAsignadasList.SelectedItems[0].SubItems[0].Text;
                String RFC = facturasAsignadasList.SelectedItems[0].SubItems[1].Text;
                String cantidadAAsignar = facturasAsignadasList.SelectedItems[0].SubItems[2].Text;
                String razonSocial = facturasAsignadasList.SelectedItems[0].SubItems[3].Text;
                String RutaPDF = facturasAsignadasList.SelectedItems[0].SubItems[4].Text;
                String ruta = facturasAsignadasList.SelectedItems[0].SubItems[5].Text;
                String folioFiscal = facturasAsignadasList.SelectedItems[0].SubItems[6].Text;
                String nombreArchivoXML = facturasAsignadasList.SelectedItems[0].SubItems[7].Text;
                String folio = facturasAsignadasList.SelectedItems[0].SubItems[8].Text;
              
                //cambiar la cantidad!!
                String verdaderaCantidad = "";
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringSun))
                    {
                        connection.Open();
                        String queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "' order by fechaExpedicion asc";
                        listaCandidatos = new List<Dictionary<string, object>>();
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                    String queryFISCAL = "SELECT FOLIO_FISCAL,BUNIT,JRNAL_NO,JRNAL_LINE,AMOUNT,consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '" + folioFiscal + "'";
                                    using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                    {
                                        SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                        if (readerFISCAL.HasRows)
                                        {
                                            while (readerFISCAL.Read())
                                            {
                                                double amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(4)));
                                                total = total - amount;
                                            }
                                        }
                                        if (total > 0)
                                        {
                                            verdaderaCantidad = total.ToString();
                                        }
                                    }//using
                                }//while
                            }
                        }//using
                    }
                }
                catch (SqlException ex)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Arrow;

                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                this.Cursor = System.Windows.Forms.Cursors.Arrow;



                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("fechaExpedicion", Fecha);
                dic.Add("rfc", RFC);
                dic.Add("cantidadAAsignar", verdaderaCantidad);
                dic.Add("razonSocial", razonSocial);
                dic.Add("nombreArchivoPDF", RutaPDF);
                dic.Add("ruta", ruta);
                dic.Add("folioFiscal", folioFiscal);
                dic.Add("nombreArchivoXML", nombreArchivoXML);
                dic.Add("folio", folio);

                listaCandidatos.Add(dic);


                string[] arr = new string[10];
                ListViewItem itm;
                //add items to ListView
                arr[0] = Convert.ToString(dic["fechaExpedicion"]);
                arr[1] = Convert.ToString(dic["rfc"]);
                arr[2] = Convert.ToString(dic["cantidadAAsignar"]);
                arr[3] = Convert.ToString(dic["razonSocial"]);
                arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
                arr[5] = Convert.ToString(dic["ruta"]);
                arr[6] = Convert.ToString(dic["folioFiscal"]);
                arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
                arr[8] = Convert.ToString(dic["folio"]);

                itm = new ListViewItem(arr);
                listaDeCandidatos.Items.Add(itm);
                foreach (ListViewItem listViewItem in facturasAsignadasList.Items)
                {
                    if (listViewItem.SubItems[6].Text.Equals(folioFiscal))
                    {
                        facturasAsignadasList.Items.Remove(listViewItem);
                        break;
                    }
                }
                foreach (Dictionary<string, object> dic1 in listaUUIDEnlazados)
                {
                    if (dic1["folioFiscal"].ToString().Equals(folioFiscal))
                    {
                        listaUUIDEnlazados.Remove(dic1);
                        break;
                    }
                }
                double cantidadAQuitar = Math.Round(Convert.ToDouble(cantidadAAsignar), 2);
                cantidadQueFalta = Math.Round((cantidadQueFalta + cantidadAQuitar), 2);
                labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadQueFalta;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Selecciona una factura (que se vea la linea azul), antes de eliminar la asignación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             
            }
        }
        public void funcionVerPDF3(object sender, EventArgs e)
        {
            String pdf = facturasAsignadasList.SelectedItems[0].SubItems[4].Text;
            String ruta = facturasAsignadasList.SelectedItems[0].SubItems[5].Text;
            String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
            System.Diagnostics.Process.Start(nombre);
        }
        public void funcionVerXML3(object sender, EventArgs e)
        {
            String pdf = facturasAsignadasList.SelectedItems[0].SubItems[4].Text;
            String ruta = facturasAsignadasList.SelectedItems[0].SubItems[5].Text;
            nombreArchivoXMLSun = ruta + (object)Path.DirectorySeparatorChar + facturasAsignadasList.SelectedItems[0].SubItems[7].Text;
            System.Diagnostics.Process.Start(nombreArchivoXMLSun);
        }
        public void funcionVerPDF(object sender, EventArgs e)
        {
            String pdf = listaDeCandidatos.SelectedItems[0].SubItems[4].Text;
            String ruta = listaDeCandidatos.SelectedItems[0].SubItems[5].Text;

            String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
            System.Diagnostics.Process.Start(nombre);
        }
        public void funcionVerXML(object sender, EventArgs e)
        {
            String pdf = listaDeCandidatos.SelectedItems[0].SubItems[4].Text;
            String ruta = listaDeCandidatos.SelectedItems[0].SubItems[5].Text;

            nombreArchivoXMLSun = ruta + (object)Path.DirectorySeparatorChar + listaDeCandidatos.SelectedItems[0].SubItems[7].Text;
            System.Diagnostics.Process.Start(nombreArchivoXMLSun);
        }
        private void labelDiario4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void labelDiario4_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                String s = e.Data.GetData(DataFormats.Text) as String;
                if (!String.IsNullOrEmpty(s))
                {
                    double cantidadAAsignar = 0;
                    int posicionDelPuntero = s.IndexOf("|");
                    String Fecha = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String RFC = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    if (todaLaCantidadCheckBox.Checked)
                    {
                        cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
                    }
                    else
                    {
                        double maximo = 0;
                        cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
                        if(cantidadAAsignar<cantidadQueFalta)
                        {
                            maximo = cantidadAAsignar;
                        }
                        else
                        {
                            maximo = cantidadQueFalta;
                        }
                        cantidadALigarXML form1 = new cantidadALigarXML(maximo);
                        form1.ShowDialog();
                        cantidadAAsignar = form1.cantidadLigada;
                        if(cantidadAAsignar==0)
                        {
                            System.Windows.Forms.MessageBox.Show("Error: no puedes asignar $ 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String razonSocial = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String RutaPDF = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String ruta = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String folioFiscal = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String nombreArchivoXML = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String folio = s;
                  
                    if(cantidadAAsignar>cantidadQueFalta)
                    {
                        System.Windows.Forms.MessageBox.Show("Error, solo falta de asignar a este movimiento: $ "+cantidadQueFalta+" y estas tratando de asignar: $ "+cantidadAAsignar, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);     
                        return;
                    }

                    cantidadQueFalta = Math.Round((cantidadQueFalta - Math.Round(cantidadAAsignar, 2)), 2);

                    int cuantos = facturasAsignadasList.Items.Count;
                    if (cuantos == 0)
                    {
                        facturasAsignadasList.Clear();
                        facturasAsignadasList.View = View.Details;
                        facturasAsignadasList.GridLines = true;
                        facturasAsignadasList.FullRowSelect = true;
                        facturasAsignadasList.Columns.Add("Fecha", 80);
                        facturasAsignadasList.Columns.Add("RFC", 100);
                        facturasAsignadasList.Columns.Add("Cantidad", 90);
                        facturasAsignadasList.Columns.Add("Razon Social", 190);
                        facturasAsignadasList.Columns.Add("RutaPDF", 0);
                        facturasAsignadasList.Columns.Add("ruta", 0);
                        facturasAsignadasList.Columns.Add("folioFiscal", 0);
                        facturasAsignadasList.Columns.Add("nombreArchivoXML", 0);
                        facturasAsignadasList.Columns.Add("Folio", 100);
            
                    }

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("fechaExpedicion", Fecha);
                    dic.Add("rfc", RFC);
                    dic.Add("cantidadAAsignar", cantidadAAsignar);
                    dic.Add("razonSocial", razonSocial);
                    dic.Add("nombreArchivoPDF", RutaPDF);
                    dic.Add("ruta", ruta);
                    dic.Add("folioFiscal", folioFiscal);
                    dic.Add("nombreArchivoXML", nombreArchivoXML);
                    dic.Add("folio", folio);
                
                    listaUUIDEnlazados.Add(dic);


                    string[] arr = new string[10];
                    ListViewItem itm;
                    //add items to ListView
                    arr[0] = Convert.ToString(dic["fechaExpedicion"]);
                    arr[1] = Convert.ToString(dic["rfc"]);
                    arr[2] = Convert.ToString(dic["cantidadAAsignar"]);
                    arr[3] = Convert.ToString(dic["razonSocial"]);
                    arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
                    arr[5] = Convert.ToString(dic["ruta"]);
                    arr[6] = Convert.ToString(dic["folioFiscal"]);
                    arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
                    arr[8] = Convert.ToString(dic["folio"]);

                    itm = new ListViewItem(arr);
                    facturasAsignadasList.Items.Add(itm);
                    foreach(ListViewItem listViewItem in listaDeCandidatos.Items)
                    {
                        if(listViewItem.SubItems[6].Text.Equals(folioFiscal))
                        {
                            listaDeCandidatos.Items.Remove(listViewItem);
                            break;
                        }
                    }
                    foreach (Dictionary<string, object> dic1 in listaCandidatos)
                    {
                        if (dic1["folioFiscal"].ToString().Equals(folioFiscal))
                        {
                            listaCandidatos.Remove(dic1);
                            break;
                        }
                    }
                    labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadQueFalta;
                    noPermitas = true;
                }
                   
            }
            
        }
        private void listaDeCandidatos_MouseDown(object sender, MouseEventArgs e)
        {
            if (listaDeCandidatos.SelectedItems.Count > 0)
            {
                if (noPermitas)
                {
                    noPermitas = false;
                }
                else
                {
                    DoDragDrop(listaDeCandidatos.SelectedItems[0].SubItems[0].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[1].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[2].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[3].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[4].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[5].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[6].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[7].Text, DragDropEffects.Copy);
                }

            }
         }
        private void checarSiElSourceLeFaltoDeLigarElDiario()
        {
             this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
             //obtiene el ultimo diario
                String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                int ultimoDiarioAuxDeSunPlus=0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringSun))
                    {
                        connection.Open();
                        String queryXML = "SELECT TOP 1 JRNAL_NO as ultimo FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_SRCE = '" + sourceGlobal + "' order by JRNAL_NO desc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                   ultimoDiarioAuxDeSunPlus = reader.GetInt32(0); 
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //cerradura de ultimo diario
             try
             {
                 using (SqlConnection connection = new SqlConnection(connStringSun))
                 {
                     connection.Open();
                        //el tipo de diario no puede ser diferente, y el ultimo diario no debe de cambiar!!
                        // y el status tampoco debe de cambiar comparado con el tipodecontabilidad global!!
                        //el where debe de llevar el operador, la unidad de negocio
                     String queryXML = "SELECT ultimoDiarioAux,tipoDeDiario,STATUS FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '" + sourceGlobal + "' AND BUNIT = '" + unidadDeNegociosGlobal + "'";
                     listaCandidatos = new List<Dictionary<string, object>>();
                     using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                     {
                         SqlDataReader reader = cmdCheck.ExecuteReader();
                         if (reader.HasRows)
                         {
                             if(reader.Read())
                             {
                                 int ultimoDiarioAux = reader.GetInt32(0);
                                 String tipoDeDiario = reader.GetString(1).Trim();
                                 String STATUS = reader.GetString(2).Trim();
                                 if(ultimoDiarioAux!=ultimoDiarioAuxDeSunPlus)
                                 {
                                     System.Windows.Forms.MessageBox.Show("Se detecto que el operador " + sourceGlobal + " tiene pendiente por asignar un diario a las facturas previamente enlazadas a las lineas. Favor de enlazar el diario y luego venir a ligar facturas. Esta validación ocurrio porque el ultimo diario contabilizado por ti es el "+ultimoDiarioAuxDeSunPlus+" y debes de ligar algun diario que este despues de "+ultimoDiarioAux, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                     if (System.Windows.Forms.Application.MessageLoop)
                                     {
                                         System.Windows.Forms.Application.Exit();
                                     }
                                     else
                                     {
                                         System.Environment.Exit(1);
                                     }
                                 }
                                 if(!tipoDeDiario.Equals(tipoDeDiarioGlobal))
                                 {
                                     System.Windows.Forms.MessageBox.Show("Se detecto que el operador " + sourceGlobal + " tiene pendiente por asignar un diario a las facturas previamente enlazadas a las lineas. Favor de enlazar el diario y luego venir a ligar facturas. Esta validación ocurrio porque el diario que estas contabilizando es el tipo de diario " + tipoDeDiario + " y ya habias ligado facturas a un diario del tipo " + tipoDeDiarioGlobal, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                     if (System.Windows.Forms.Application.MessageLoop)
                                     {
                                         System.Windows.Forms.Application.Exit();
                                     }
                                     else
                                     {
                                         System.Environment.Exit(1);
                                     }
                                 }
                             }
                           
                         }
                     }
                 }
             }
            catch(Exception ex1)
            {
                 System.Windows.Forms.MessageBox.Show(ex1.ToString()+"ERROR", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        private void checarSiLacuentaEsDeIngresoODeEgreso()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "SELECT tipoDeContabilidad, DESCR FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE ACNT_CODE = '" + cuentaGlobal + "' AND unidadDeNegocio = '" + unidadDeNegociosGlobal + "'";
                       listaCandidatos = new List<Dictionary<string, object>>();
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if(reader.Read())
                            {
                                String descr = reader.GetString(1);
                                tipoDeContabilidadGlobal =  reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Se detecto que la cuenta: " + cuentaGlobal + " no esta registrada ni como gasto, ni como ingreso, tiene que ir asignarla en el programa de administrador.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (System.Windows.Forms.Application.MessageLoop)
                            {
                                System.Windows.Forms.Application.Exit();
                            }
                            else
                            {
                                System.Environment.Exit(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                System.Windows.Forms.MessageBox.Show(ex1.ToString() + "ERROR", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("settings.txt"))
                {
                    String line = sr.ReadToEnd();
                    Properties.Settings.Default.sunDatasource = line;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            this.connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

           checarSiElSourceLeFaltoDeLigarElDiario();
            checarSiLacuentaEsDeIngresoODeEgreso();
          //  listaDeCandidatos.MouseDoubleClick += listaDeCandidatos_MouseDoubleClick;


            if(tipoDeContabilidadGlobal==1)
            {
                detectLabel.Text = "Gastos";
            }
            else
            {
                if(tipoDeContabilidadGlobal==2)
                {
                    detectLabel.Text = "Ingresos";
                }
                else
                {
                    detectLabel.Text = "Balanza";
                }    
            }
            

            noPermitas = false;
            if(Properties.Settings.Default.predefinidoTodaLaCantidad.Equals("1"))
            {
                todaLaCantidadCheckBox.Checked = true;
            }
            else
            {
                todaLaCantidadCheckBox.Checked = false;
            }
            String decimalesC = cantidadGlobal.Substring(cantidadGlobal.Length - 3, 3);
            cantidadGlobal = cantidadGlobal.Substring(0, cantidadGlobal.Length - 3)+"."+decimalesC;

           
            labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadGlobal;
            cantidadQueFalta = Math.Round(Convert.ToDouble(cantidadGlobal), 2);
            if (cantidadQueFalta==0.0)
            {
                System.Windows.Forms.MessageBox.Show("ERROR: la cantidad no puede ser 0, trata de grabar esa linea y luego volver a dar click al boton de Ligar Facturas", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }  
            }

            labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadQueFalta;

            lineaLabel.Text = Convert.ToInt32(lineaGlobal).ToString();
            sourceLabel.Text = sourceGlobal;
            cuentaLabel.Text = cuentaGlobal;
            debitCreditLabel.Text = debitCreditGlobal;
            cantidadLabel.Text = Math.Round(Convert.ToDouble(cantidadGlobal), 2).ToString();
            unidadDeNegocioLabel.Text = unidadDeNegociosGlobal;
            

            listaDeCandidatos.MultiSelect = false;
            /*listaDeCandidatos.AllowDrop = true;
            listaDeCandidatos.MouseDown+=listaDeCandidatos_MouseDown;
            labelDiario4.AllowDrop = true;
            labelDiario4.DragEnter+=labelDiario4_DragEnter;
            labelDiario4.DragDrop+=labelDiario4_DragDrop;*/
            listaUUIDEnlazados = new List<Dictionary<string, object>>();
                
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem2 = new System.Windows.Forms.MenuItem();
            menuItem3 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1, menuItem2, menuItem3 });
            menuItem1.Index = 0;
            menuItem1.Text = "Ver PDF";
            menuItem2.Index = 1;
            menuItem2.Text = "Ver XML";
            menuItem1.Click += funcionVerPDF;// new SystemHandler(this.funcionVerPDF);
            menuItem2.Click += funcionVerXML;
            menuItem3.Index = 2;
            menuItem3.Text = "Asociar factura";
            menuItem3.Click += asociarFactura;
           
        
            listaDeCandidatos.ContextMenu = contextMenu2;



            contextMenu3 = new System.Windows.Forms.ContextMenu();
            menuItem31 = new System.Windows.Forms.MenuItem();
            menuItem32 = new System.Windows.Forms.MenuItem();
            menuItem33 = new System.Windows.Forms.MenuItem();

            contextMenu3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem31, menuItem32 ,menuItem33});
            menuItem31.Index = 0;
            menuItem31.Text = "Ver PDF";
            menuItem32.Index = 1;
            menuItem32.Text = "Ver XML";
            menuItem33.Index = 2;
            menuItem33.Text = "Eliminar de las facturas asignadas al asiento contable";

            menuItem31.Click += funcionVerPDF3;
            menuItem32.Click += funcionVerXML3;
            menuItem33.Click += eliminarFacturaDeLaLista;

            facturasAsignadasList.ContextMenu = contextMenu3;

       //!
            if(!lineaGlobal.Equals("ERROR"))
            {
                var source = new AutoCompleteStringCollection();
                var sourceRazonesSociales = new AutoCompleteStringCollection();
             
                List<String> rfcS = new List<String>();
                List<String> razonesSocialeS = new List<String>();
                
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "";
                        if(tipoDeContabilidadGlobal==1)
                        {
                            queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' GROUP BY rfc,razonSocial order by rfc asc";
                        }
                        else
                        {
                            if(tipoDeContabilidadGlobal==2)
                            {
                                 queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '2' GROUP BY rfc,razonSocial order by rfc asc";
                            }
                            else
                            {
                                queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') GROUP BY rfc,razonSocial order by rfc asc";
                            }
                        }
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
                                    rfcS.Add(rfc);
                                    razonesSocialeS.Add(razonSocial);
                                } 
                            }//if reader
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }



                
                source.AddRange(rfcS.ToArray());
                
                
                sourceRazonesSociales.AddRange(razonesSocialeS.ToArray());

                rfcTextBox.AutoCompleteCustomSource = source;
                rfcTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                rfcTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;


                razonSocialText.AutoCompleteCustomSource = sourceRazonesSociales;
                razonSocialText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                razonSocialText.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //    rfcTextBox.KeyPress += rfcTextBox_KeyPress;
               
            }
           
            
        }
        private void asociarFactura(object sender, EventArgs e)
        {
            if (listaDeCandidatos.SelectedItems.Count > 0)
            {
                String s = listaDeCandidatos.SelectedItems[0].SubItems[0].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[1].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[2].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[3].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[4].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[5].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[6].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[7].Text +
                   "|" + listaDeCandidatos.SelectedItems[0].SubItems[8].Text;
                if (!String.IsNullOrEmpty(s))
                {
                    double cantidadAAsignar = 0;
                    int posicionDelPuntero = s.IndexOf("|");
                    String Fecha = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String RFC = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    if (todaLaCantidadCheckBox.Checked)
                    {
                        cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
                    }
                    else
                    {
                        double maximo = 0;
                        cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
                        if (cantidadAAsignar < cantidadQueFalta)
                        {
                            maximo = cantidadAAsignar;
                        }
                        else
                        {
                            maximo = cantidadQueFalta;
                        }
                        cantidadALigarXML form1 = new cantidadALigarXML(maximo);
                        form1.ShowDialog();
                        cantidadAAsignar = form1.cantidadLigada;
                        if (cantidadAAsignar == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Error: no puedes asignar $ 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String razonSocial = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String RutaPDF = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String ruta = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String folioFiscal = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String nombreArchivoXML = s.Substring(0, posicionDelPuntero);
                    s = s.Substring(posicionDelPuntero + 1);
                    posicionDelPuntero = s.IndexOf("|");

                    String folio = s;

                    if (cantidadAAsignar > cantidadQueFalta)
                    {
                        System.Windows.Forms.MessageBox.Show("Error, solo falta de asignar a este movimiento: $ " + cantidadQueFalta + " y estas tratando de asignar: $ " + cantidadAAsignar, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    cantidadQueFalta = Math.Round((cantidadQueFalta - Math.Round(cantidadAAsignar, 2)), 2);

                    int cuantos = facturasAsignadasList.Items.Count;
                    if (cuantos == 0)
                    {
                        facturasAsignadasList.Clear();
                        facturasAsignadasList.View = View.Details;
                        facturasAsignadasList.GridLines = true;
                        facturasAsignadasList.FullRowSelect = true;
                        facturasAsignadasList.Columns.Add("Fecha", 80);
                        facturasAsignadasList.Columns.Add("RFC", 100);
                        facturasAsignadasList.Columns.Add("Cantidad", 90);
                        facturasAsignadasList.Columns.Add("Razon Social", 190);
                        facturasAsignadasList.Columns.Add("RutaPDF", 0);
                        facturasAsignadasList.Columns.Add("ruta", 0);
                        facturasAsignadasList.Columns.Add("folioFiscal", 0);
                        facturasAsignadasList.Columns.Add("nombreArchivoXML", 0);
                        facturasAsignadasList.Columns.Add("Folio", 100);

                    }

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("fechaExpedicion", Fecha);
                    dic.Add("rfc", RFC);
                    dic.Add("cantidadAAsignar", cantidadAAsignar);
                    dic.Add("razonSocial", razonSocial);
                    dic.Add("nombreArchivoPDF", RutaPDF);
                    dic.Add("ruta", ruta);
                    dic.Add("folioFiscal", folioFiscal);
                    dic.Add("nombreArchivoXML", nombreArchivoXML);
                    dic.Add("folio", folio);

                    listaUUIDEnlazados.Add(dic);


                    string[] arr = new string[10];
                    ListViewItem itm;
                    //add items to ListView
                    arr[0] = Convert.ToString(dic["fechaExpedicion"]);
                    arr[1] = Convert.ToString(dic["rfc"]);
                    arr[2] = Convert.ToString(dic["cantidadAAsignar"]);
                    arr[3] = Convert.ToString(dic["razonSocial"]);
                    arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
                    arr[5] = Convert.ToString(dic["ruta"]);
                    arr[6] = Convert.ToString(dic["folioFiscal"]);
                    arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
                    arr[8] = Convert.ToString(dic["folio"]);

                    itm = new ListViewItem(arr);
                    facturasAsignadasList.Items.Add(itm);
                    foreach (ListViewItem listViewItem in listaDeCandidatos.Items)
                    {
                        if (listViewItem.SubItems[6].Text.Equals(folioFiscal))
                        {
                            listaDeCandidatos.Items.Remove(listViewItem);
                            break;
                        }
                    }
                    foreach (Dictionary<string, object> dic1 in listaCandidatos)
                    {
                        if (dic1["folioFiscal"].ToString().Equals(folioFiscal))
                        {
                            listaCandidatos.Remove(dic1);
                            break;
                        }
                    }
                    labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadQueFalta;
                    noPermitas = true;
                }
            }
        }
        private void llenaRFC()
        {
            String razonSocial = razonSocialText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if(tipoDeContabilidadGlobal==1)
                    {
                        queryXML = "SELECT TOP 1 rfc FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' AND razonSocial = '" + razonSocial + "'";
                    }
                    else
                    {
                        if(tipoDeContabilidadGlobal==2)
                        {
                             queryXML = "SELECT TOP 1 rfc FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '2' AND razonSocial = '" + razonSocial + "'";
                        }
                        else
                        {
                             queryXML = "SELECT TOP 1 rfc FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS in ('1','2') AND razonSocial = '" + razonSocial + "'";
                        }
                    }
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                rfcTextBox.Text = rfc;
                            }
                            rellena();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void llenaRazonSocial()
        {
            String RFC = rfcTextBox.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if(tipoDeContabilidadGlobal==1)
                    {
                        queryXML = "SELECT TOP 1 razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' AND rfc = '" + RFC + "'";
                    }
                    else
                    {
                         if(tipoDeContabilidadGlobal==2)
                         {
                             queryXML = "SELECT TOP 1 razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '2' AND rfc = '" + RFC + "'";
                         }
                         else
                         {
                             queryXML = "SELECT TOP 1 razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') AND rfc = '" + RFC + "'";
                         }
                    }
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String razonSocial = reader.GetString(0);
                                razonSocialText.Text = razonSocial;
                            }
                            rellena();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void rfcTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
              

            }
            
        }
        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config form = new config();
           
            form.Show();
        }

        private void rfcTextBox_TextChanged(object sender, EventArgs e)
        {
            llenaRazonSocial();
        }

        private void razonSocialText_TextChanged(object sender, EventArgs e)
        {
            llenaRFC();
        }
        private void listaDeCandidatos_MouseDoubleClick(object sender, EventArgs e)
        {
            String s = listaDeCandidatos.SelectedItems[0].SubItems[0].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[1].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[2].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[3].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[4].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[5].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[6].Text +
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[7].Text+
                    "|" + listaDeCandidatos.SelectedItems[0].SubItems[8].Text;
            double cantidadAAsignar = 0;
            int posicionDelPuntero = s.IndexOf("|");
            String Fecha = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String RFC = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            if (todaLaCantidadCheckBox.Checked)
            {
                cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
            }
            else
            {
                double maximo = 0;
                cantidadAAsignar = Convert.ToDouble(s.Substring(0, posicionDelPuntero));
                if (cantidadAAsignar < cantidadQueFalta)
                {
                    maximo = cantidadAAsignar;
                }
                else
                {
                    maximo = cantidadQueFalta;
                }
                cantidadALigarXML form1 = new cantidadALigarXML(maximo);
                form1.ShowDialog();
                cantidadAAsignar = form1.cantidadLigada;
                if (cantidadAAsignar == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Error: no puedes asignar $ 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String razonSocial = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String RutaPDF = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String ruta = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String folioFiscal = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");


            String nombreArchivoXML = s.Substring(0, posicionDelPuntero);
            s = s.Substring(posicionDelPuntero + 1);
            posicionDelPuntero = s.IndexOf("|");

            String folio = s;

            if (cantidadAAsignar > cantidadQueFalta)
            {
                System.Windows.Forms.MessageBox.Show("Error, solo falta de asignar a este movimiento: $ " + cantidadQueFalta + " y estas tratando de asignar: $ " + cantidadAAsignar, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            cantidadQueFalta = Math.Round((cantidadQueFalta - Math.Round(cantidadAAsignar, 2)), 2);

            int cuantos = facturasAsignadasList.Items.Count;
            if (cuantos == 0)
            {
                facturasAsignadasList.Clear();
                facturasAsignadasList.View = View.Details;
                facturasAsignadasList.GridLines = true;
                facturasAsignadasList.FullRowSelect = true;
                facturasAsignadasList.Columns.Add("Fecha", 80);
                facturasAsignadasList.Columns.Add("RFC", 100);
                facturasAsignadasList.Columns.Add("Cantidad", 90);
                facturasAsignadasList.Columns.Add("Razon Social", 190);
                facturasAsignadasList.Columns.Add("RutaPDF", 0);
                facturasAsignadasList.Columns.Add("ruta", 0);
                facturasAsignadasList.Columns.Add("folioFiscal", 0);
                facturasAsignadasList.Columns.Add("nombreArchivoXML", 0);
                facturasAsignadasList.Columns.Add("Folio", 100);
            }

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("fechaExpedicion", Fecha);
            dic.Add("rfc", RFC);
            dic.Add("cantidadAAsignar", cantidadAAsignar);
            dic.Add("razonSocial", razonSocial);
            dic.Add("nombreArchivoPDF", RutaPDF);
            dic.Add("ruta", ruta);
            dic.Add("folioFiscal", folioFiscal);
            dic.Add("nombreArchivoXML", nombreArchivoXML);
            dic.Add("folio", folio);
            listaUUIDEnlazados.Add(dic);


            string[] arr = new string[10];
            ListViewItem itm;
            //add items to ListView
            arr[0] = Convert.ToString(dic["fechaExpedicion"]);
            arr[1] = Convert.ToString(dic["rfc"]);
            arr[2] = Convert.ToString(dic["cantidadAAsignar"]);
            arr[3] = Convert.ToString(dic["razonSocial"]);
            arr[4] = Convert.ToString(dic["nombreArchivoPDF"]);
            arr[5] = Convert.ToString(dic["ruta"]);
            arr[6] = Convert.ToString(dic["folioFiscal"]);
            arr[7] = Convert.ToString(dic["nombreArchivoXML"]);
            arr[8] = Convert.ToString(dic["folio"]);

            itm = new ListViewItem(arr);
            facturasAsignadasList.Items.Add(itm);
            foreach (ListViewItem listViewItem in listaDeCandidatos.Items)
            {
                if (listViewItem.SubItems[6].Text.Equals(folioFiscal))
                {
                    listaDeCandidatos.Items.Remove(listViewItem);
                    break;
                }
            }
            foreach (Dictionary<string, object> dic1 in listaCandidatos)
            {
                if (dic1["folioFiscal"].ToString().Equals(folioFiscal))
                {
                    listaCandidatos.Remove(dic1);
                    break;
                }
            }
            labelDiario4.Text = "Cantidad que falta a ligar: $ " + cantidadQueFalta;
            noPermitas = true;
        }
        private void listaDeCandidatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void facturasAsignadasList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void todaLaCantidadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(todaLaCantidadCheckBox.Checked)
            {
                Properties.Settings.Default.predefinidoTodaLaCantidad = "1";
            }
            else
            {
                Properties.Settings.Default.predefinidoTodaLaCantidad = "0";
            }
            Properties.Settings.Default.Save();
        }

        private void ligarButton_Click(object sender, EventArgs e)
        {
            if(cantidadQueFalta!=0.0)
            {
                System.Windows.Forms.MessageBox.Show("Todavia te falta por asignar: $ "+cantidadQueFalta+" al movimiento contable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //obtiene el ultimo diario
                String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                String ultimoDiarioAux = "0";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringSun))
                    {
                        connection.Open();
                        String queryXML = "SELECT TOP 1 JRNAL_NO as ultimo FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegociosGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_SRCE = '" + sourceGlobal + "' order by JRNAL_NO desc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                   ultimoDiarioAux = Convert.ToString(reader.GetInt32(0)); 
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //cerradura de ultimo diario
                String linea = lineaGlobal;
                int consecutivo = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(this.connStringSun))
                    {
                        connection.Open();
                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        foreach (Dictionary<string, object> dic1 in listaUUIDEnlazados)
                        {
                            nombreArchivoXMLSun = dic1["ruta"].ToString() + (object)Path.DirectorySeparatorChar + dic1["nombreArchivoXML"];         
                            consecutivo++;
                            String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] (TREFERENCE,consecutivo,JRNAL_SOURCE,consecutivoFacturas,UUID,amount,ultimoDiarioAux,tipoDeDiario,STATUS,timestamp,ACNT_CODE,BUNIT, D_C) VALUES ('" + referenciaGlobal + "', " + linea + ",  '" + sourceGlobal + "', " + consecutivo + ", '" + dic1["folioFiscal"].ToString() + "', " + dic1["cantidadAAsignar"].ToString() + ","+ultimoDiarioAux+",'"+tipoDeDiarioGlobal+"', '"+tipoDeContabilidadGlobal+"',"+unixTimestamp+",'"+cuentaGlobal+"', '"+unidadDeNegociosGlobal+"','"+debitCreditGlobal+"')";
                            
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                        }
                        System.Windows.Forms.MessageBox.Show("Linea ligada, recuerde dar click al botón \"Terminar de Ligar\" cuando termine esta poliza en sunplus.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (System.Windows.Forms.Application.MessageLoop)
                        {
                            System.Windows.Forms.Application.Exit();
                        }
                        else
                        {
                            System.Environment.Exit(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
