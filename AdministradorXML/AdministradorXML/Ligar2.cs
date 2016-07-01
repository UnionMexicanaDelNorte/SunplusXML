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
    public partial class Ligar2 : Form
    {
        public String lineaGlobal { get; set; }
        public String diarioGlobal { get; set; }
        public double lineaMaximo { get; set; }
        public double maximoFactura { get; set; }
        public int tipoDeContabilidad { get; set; }
        public List<Dictionary<string, object>> listaCandidatos { get; set; }
      
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.ContextMenu contextMenu2;

        public Ligar2()
        {
            InitializeComponent();
        }

        public Ligar2(String linea, String diario, double maximo, int tipo)
        {
            InitializeComponent();
            lineaGlobal = linea;
            diarioGlobal = diario;
            lineaMaximo = maximo;
            tipoDeContabilidad = tipo;
        }
        public void VerPDF(object sender, EventArgs e)
        {
            int cuantos = facturasPosiblesList.SelectedItems.Count;
            if(cuantos==0)
            {
                String pdf = facturasPosiblesList.SelectedItems[0].SubItems[4].Text;
                String ruta = facturasPosiblesList.SelectedItems[0].SubItems[5].Text;
                String nombre = ruta + (object)Path.DirectorySeparatorChar + pdf;
                System.Diagnostics.Process.Start(nombre);
            } 
        }
        private void llenaRFC()
        {
            String razonSocial = razonSocialText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidad == 1)
                    {
                        queryXML = "SELECT TOP 1 rfc FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' AND razonSocial = '" + razonSocial + "'";
                    }
                    else
                    {
                        if (tipoDeContabilidad == 2)
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
                                rfcText.Text = rfc;
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
            String RFC = rfcText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidad == 1)
                    {
                        queryXML = "SELECT TOP 1 razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' AND rfc = '" + RFC + "'";
                    }
                    else
                    {
                        if (tipoDeContabilidad == 2)
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
        private void rellena()
        {
            String RFC = rfcText.Text;
            String linea = lineaGlobal;
            String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
         
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidad == 1)
                    {
                        queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS =  '1' order by fechaExpedicion asc";
                    }
                    else
                    {
                        if (tipoDeContabilidad == 2)
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
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(0))), 2);
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

                            facturasPosiblesList.Clear();
                            facturasPosiblesList.View = View.Details;
                            facturasPosiblesList.GridLines = true;
                            facturasPosiblesList.FullRowSelect = true;
                            facturasPosiblesList.Columns.Add("Fecha", 80);
                            facturasPosiblesList.Columns.Add("RFC", 100);
                            facturasPosiblesList.Columns.Add("Cantidad", 90);
                            facturasPosiblesList.Columns.Add("Razon Social", 190);
                            facturasPosiblesList.Columns.Add("RutaPDF", 0);
                            facturasPosiblesList.Columns.Add("ruta", 0);
                            facturasPosiblesList.Columns.Add("folioFiscal", 0);
                            facturasPosiblesList.Columns.Add("nombreArchivoXML", 0);
                            facturasPosiblesList.Columns.Add("Folio", 100);

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
                                    facturasPosiblesList.Items.Add(itm);
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
        private void Ligar2_Load(object sender, EventArgs e)
        {
            maximoFactura = -1;
            if(tipoDeContabilidad==1)
            {
                infoLabel.Text = "La linea " + lineaGlobal + " del diario " + diarioGlobal + " puedes ligar hasta $ " + lineaMaximo+" del gasto";
            }
            else
            {
                infoLabel.Text = "La linea " + lineaGlobal + " del diario " + diarioGlobal + " puedes ligar hasta $ " + lineaMaximo + " del ingreso";
            }
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
          
            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1 });
            menuItem1.Index = 0;
            menuItem1.Text = "Ver PDF";
            menuItem1.Click += VerPDF;



            facturasPosiblesList.ContextMenu = contextMenu2;

            var source = new AutoCompleteStringCollection();
            var sourceRazonesSociales = new AutoCompleteStringCollection();

            List<String> rfcS = new List<String>();
            List<String> razonesSocialeS = new List<String>();

            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidad == 1)
                    {
                        queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE STATUS = '1' GROUP BY rfc,razonSocial order by rfc asc";
                    }
                    else
                    {
                        if (tipoDeContabilidad == 2)
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

            rfcText.AutoCompleteCustomSource = source;
            rfcText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            rfcText.AutoCompleteSource = AutoCompleteSource.CustomSource;


            razonSocialText.AutoCompleteCustomSource = sourceRazonesSociales;
            razonSocialText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            razonSocialText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
        }

        private void razonSocialText_TextChanged(object sender, EventArgs e)
        {
            //llenaRFC();
        }

        private void rfcText_TextChanged(object sender, EventArgs e)
        {
            llenaRazonSocial();
        }

        private void facturasPosiblesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = facturasPosiblesList.SelectedItems.Count;
            if (cuantos > 0)
            {
                maximoFactura = Math.Round(Convert.ToDouble(facturasPosiblesList.SelectedItems[0].SubItems[2].Text), 2);
               cantidadALigar.Text = maximoFactura.ToString();
            }
        }

        private void ligarButton_Click(object sender, EventArgs e)
        {
            int cuantos = facturasPosiblesList.SelectedItems.Count;
            if (cuantos == 0)
            {
                System.Windows.Forms.MessageBox.Show("Primero debes de seleccionar una factura antes de ligarlo a la linea", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            double maximo;
            if (maximoFactura > lineaMaximo)
            {
                maximo = lineaMaximo;
            }
            else
            {
                maximo = maximoFactura;
            }
            double cuantoFactura = Math.Round(Convert.ToDouble(cantidadALigar.Text), 2);
           
            if (cuantoFactura > maximo)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $" + maximo, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cuantos > 0)
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

                String cantidad = cantidadALigar.Text.Trim();
                int consecutivo = 0;
                String folioFiscalGlobal = facturasPosiblesList.SelectedItems[0].SubItems[6].Text.Trim();
                String queryCheck = "SELECT consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE BUNIT = '" + Login.unidadDeNegocioGlobal+ "' and JRNAL_NO = " + diarioGlobal + " and JRNAL_LINE = " + lineaGlobal + " order by consecutivo desc";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmdCheck = new SqlCommand(queryCheck, connection);
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                consecutivo = reader.GetInt32(0);
                            }
                        }
                        String xmlText = "";
                        consecutivo++;
                        String query = "";
                        if (tipoDeContabilidad == 1)
                        {
                            query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '1', '" + xmlText + "' , " + consecutivo + ",  '"+Login.sourceGlobal+"')";
                        }
                        else
                        {
                            if (tipoDeContabilidad == 2)
                            {
                                query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '2', '" + xmlText + "' , " + consecutivo + ",  '"+Login.sourceGlobal+"')";
                            }
                            else
                            {

                            }
                        }
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        this.Close();

                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
