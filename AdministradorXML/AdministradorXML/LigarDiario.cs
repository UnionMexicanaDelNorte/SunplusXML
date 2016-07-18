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
    public partial class LigarDiario : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public double lineaMaximo { get; set; }
        public double maximoFactura { get; set; }
        public int tipoDeContabilidadGlobal { get; set; }
        public List<Dictionary<string, object>> listaCandidatos { get; set; }
      
     
        public LigarDiario()
        {
            InitializeComponent();
        }

        private void diarioText_TextChanged(object sender, EventArgs e)
        {
            leeDiario();
        }

        private void leeDiario()
        {
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            listaFinal.Clear();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT s.JRNAL_NO, s.JRNAL_LINE,s.AMOUNT,s.DESCRIPTN,s.TRANS_DATETIME , s.ACCNT_CODE, s.D_C FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s WHERE s.JRNAL_NO = '" + diariosText.Text + "' AND s.ALLOCATION != 'C' AND s.JRNAL_NO = '" + diariosText.Text.Trim() + "'";

                   
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
                                String TRANS_DATETIME = Convert.ToString(reader.GetDateTime(4));
                                String ACCNT_CODE = reader.GetString(5);
                                String D_C = reader.GetString(6);

                                //cuanto esta enlazado de esa linea
                                String queryFISCAL = "SELECT f.FOLIO_FISCAL,f.BUNIT,f.JRNAL_NO,f.JRNAL_LINE,f.AMOUNT,f.consecutivo , x.rfc,x.nombreArchivoPDF, x.ruta, x.razonSocial, s.DESCRIPTN FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE f.JRNAL_NO = " + JRNAL_NO1 + " AND f.JRNAL_LINE = " + JRNAL_LINE1;
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


                                            double amount = Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(4))),2);



                                            total = total - amount;
                                        }

                                    }

                                    if(total<0.01)
                                    {
                                        total = 0.0;
                                    }

                                    if (total > 0)
                                    {

                                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                        dictionary.Add("JRNAL_NO1", JRNAL_NO1);
                                        dictionary.Add("JRNAL_LINE1", JRNAL_LINE1);
                                        dictionary.Add("total", total);
                                        dictionary.Add("DESCRIPTN1", DESCRIPTN1);
                                        dictionary.Add("TRANS_DATETIME", TRANS_DATETIME);
                                        dictionary.Add("ACCNT_CODE", ACCNT_CODE);
                                        dictionary.Add("D_C", D_C);
                                        listaFinal.Add(dictionary);
                                    }
                                }//using
                            }//while


                            lineasList.Clear();
                            lineasList.View = View.Details;
                            lineasList.GridLines = true;
                            lineasList.FullRowSelect = true;
                            lineasList.Columns.Add("Diario", 80);
                            lineasList.Columns.Add("Linea", 80);
                            lineasList.Columns.Add("Cantidad", 100);
                            lineasList.Columns.Add("Descripción", 250);
                            lineasList.Columns.Add("Fecha", 200);
                            lineasList.Columns.Add("Cuenta", 110);
                            lineasList.Columns.Add("D_C", 50);


                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("JRNAL_NO1"))
                                {
                                    string[] arr = new string[8];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["JRNAL_NO1"]);
                                    arr[1] = Convert.ToString(dic["JRNAL_LINE1"]);
                                    arr[2] = Convert.ToString(dic["total"]);
                                    arr[3] = Convert.ToString(dic["DESCRIPTN1"]);
                                    arr[4] = Convert.ToString(dic["TRANS_DATETIME"]);
                                    arr[5] = Convert.ToString(dic["ACCNT_CODE"]);
                                    arr[6] = Convert.ToString(dic["D_C"]);
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

        private void LigarDiario_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
            listaCandidatos = new List<Dictionary<string, object>>();
        }

        private void lineasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = lineasList.SelectedItems.Count;
            if (cuantos > 0)
            {
                if(checarSiLacuentaEsDeIngresoODeEgreso(lineasList.SelectedItems[0].SubItems[5].Text.Trim()))
                {
                    lineaMaximo = Math.Round(Convert.ToDouble(lineasList.SelectedItems[0].SubItems[2].Text), 2);
                    cantidadDeLaLineaText.Text = lineaMaximo.ToString();
                    cantidadDeLaLineaText.Enabled = true;

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
                            if (tipoDeContabilidadGlobal == 1)
                            {
                                queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '1' GROUP BY rfc,razonSocial order by rfc asc";
                            }
                            else
                            {
                                if (tipoDeContabilidadGlobal == 2)
                                {
                                    queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '2' GROUP BY rfc,razonSocial order by rfc asc";
                                }
                                else
                                {
                                    queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') GROUP BY rfc,razonSocial order by rfc asc";
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
                else
                {
                    cantidadDeLaLineaText.Text = "0";
                    cantidadDeLaLineaText.Enabled = false;
                }
                //detectar si la cuenta es de ingreso o de egreso
                //cargar rfc y razones
            }
        }
        private void rellena()
        {
            String RFC = rfcText.Text;
            //String linea = lineaGlobal;
            String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            listaCandidatos.Clear();
                  
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    if (tipoDeContabilidadGlobal == 1)
                    {
                        queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS =  '1' order by fechaExpedicion asc";
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal == 2)
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS =  '2' order by fechaExpedicion asc";
                        }
                        else
                        {
                            queryXML = "SELECT total, folioFiscal, rfc, razonSocial, nombreArchivoPDF, fechaExpedicion ,ruta,nombreArchivoXML,folio FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE rfc = '" + RFC + "' AND STATUS in('1','2') order by fechaExpedicion asc";
                        }
                    }
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
                                if (!reader.IsDBNull(4))
                                {
                                    nombreArchivoPDF = reader.GetString(4);
                                }
                                String nombreArchivoXML = "";
                                if (!reader.IsDBNull(7))
                                {
                                    nombreArchivoXML = reader.GetString(7);
                                }
                                String razonSocial = "";
                                if (!reader.IsDBNull(3))
                                {
                                    razonSocial = reader.GetString(3);
                                }
                                String fechaExpedicion = Convert.ToString(reader.GetDateTime(5));
                                String ruta = "";
                                if (!reader.IsDBNull(6))
                                {
                                    ruta = reader.GetString(6);
                                
                                }
                                String folio = "no pudimos leer el folio, perdón";
                                if (!reader.IsDBNull(8))
                                {
                                    folio = reader.GetString(8);
                                }

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
                    if (tipoDeContabilidadGlobal == 1)
                    {
                        queryXML = "SELECT TOP 1 rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '1' AND razonSocial = '" + razonSocial + "'";
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal == 2)
                        {
                            queryXML = "SELECT TOP 1 rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '2' AND razonSocial = '" + razonSocial + "'";
                        }
                        else
                        {
                            queryXML = "SELECT TOP 1 rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in ('1','2') AND razonSocial = '" + razonSocial + "'";
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
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (tipoDeContabilidadGlobal == 1)
                    {
                        queryXML = "SELECT TOP 1 razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '1' AND rfc = '" + RFC + "'";
                    }
                    else
                    {
                        if (tipoDeContabilidadGlobal == 2)
                        {
                            queryXML = "SELECT TOP 1 razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS = '2' AND rfc = '" + RFC + "'";
                        }
                        else
                        {
                            queryXML = "SELECT TOP 1 razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') AND rfc = '" + RFC + "'";
                        }
                    }
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    String razonSocial = reader.GetString(0);
                                    if (!razonSocial.Equals(""))
                                    {
                                        razonSocialText.Text = razonSocial;
                                    }
                                }
                                rellena();
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
        private bool checarSiLacuentaEsDeIngresoODeEgreso(String cuentaGlobal)
        {
            String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "SELECT tipoDeContabilidad, DESCR FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE ACNT_CODE = '" + cuentaGlobal + "' AND unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                String descr = reader.GetString(1);
                                tipoDeContabilidadGlobal = reader.GetInt32(0);
                                this.Cursor = System.Windows.Forms.Cursors.Arrow;
                                return true;
                            }
                        }
                        else
                        {
                            this.Cursor = System.Windows.Forms.Cursors.Arrow;
                            System.Windows.Forms.MessageBox.Show("Se detecto que la cuenta: " + cuentaGlobal + " no esta registrada ni como gasto, ni como ingreso, tiene que ir asignarla en el programa de administrador.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                System.Windows.Forms.MessageBox.Show(ex1.ToString() + "ERROR", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            return false;
        }

        private void rfcText_TextChanged(object sender, EventArgs e)
        {
            llenaRazonSocial();
        }

        private void razonSocialText_TextChanged(object sender, EventArgs e)
        {
      //      llenaRFC();
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

        private void cantidadALigar_TextChanged(object sender, EventArgs e)
        {

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

            double cuantoLinea = Math.Round(Convert.ToDouble( cantidadDeLaLineaText.Text ), 2);

            if (cuantoLinea > maximo)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $" + maximo, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cuantos > 0)
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                String diarioGlobal = diariosText.Text.Trim();
                String lineaGlobal = lineasList.SelectedItems[0].SubItems[1].Text.Trim();
                String cantidad = cantidadALigar.Text.Trim();
                int consecutivo = 0;
                String folioFiscalGlobal = facturasPosiblesList.SelectedItems[0].SubItems[6].Text.Trim();
                String queryCheck = "SELECT consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE BUNIT = '" + Login.unidadDeNegocioGlobal + "' and JRNAL_NO = " + diarioGlobal + " and JRNAL_LINE = " + lineaGlobal + " order by consecutivo desc";

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
                        if (tipoDeContabilidadGlobal == 1)
                        {
                            query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '1', '" + xmlText + "' , " + consecutivo + ",  '" + Login.sourceGlobal + "')";
                        }
                        else
                        {
                            if (tipoDeContabilidadGlobal == 2)
                            {
                                query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '2', '" + xmlText + "' , " + consecutivo + ",  '" + Login.sourceGlobal + "')";
                            }
                            else
                            {
                                String d_c = lineasList.SelectedItems[0].SubItems[6].Text.Trim();
                                if(d_c.Equals("C"))//ingreso, de balanza
                                {
                                    query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '2', '" + xmlText + "' , " + consecutivo + ",  '" + Login.sourceGlobal + "')";
                                }
                                else
                                {
                                    query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diarioGlobal + ", " + lineaGlobal + ", '" + folioFiscalGlobal + "', " + cantidad + ", '1', '" + xmlText + "' , " + consecutivo + ",  '" + Login.sourceGlobal + "')";
                                }
                            }
                        }
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        leeDiario();
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
