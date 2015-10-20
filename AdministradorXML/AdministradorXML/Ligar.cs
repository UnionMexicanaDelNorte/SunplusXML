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
    public partial class Ligar : Form
    {
        public String folioFiscalGlobal { get; set; }
        public String original { get; set; }
        public double maximoActual { get; set; }
      
        public double maximoGlobal { get; set; }
        public double maximoTemporal { get; set; }
        public int tipoDeContabilidadGlobal { get; set; }
        public bool firstTime { get; set; }
     
      
        public double lineaMaximo { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public Ligar(String folio, double maximo, int tipo, String o)
        {
            InitializeComponent();
            maximoGlobal = maximo;
            folioFiscalGlobal = folio;
            tipoDeContabilidadGlobal = tipo;
            original = o;
        }
              
        public Ligar()
        {
            InitializeComponent();
        }

        private void modificarCantidadCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(modificarCantidadCheck.Checked)
            {
                cantidadALigar.Enabled = true;
            }
            else
            {
                cantidadALigar.Enabled = false;
            }
        }

        private void Ligar_Load(object sender, EventArgs e)
        {
            maximoActual = maximoGlobal;
            lineaMaximo = -1;
            firstTime = true;
            maxFactura.Text = "Cantidad original: $"+original;
            maximoFacturaLabel.Text = "Máximo factura: $" + maximoGlobal.ToString();
            cantidadALigar.Text = maximoGlobal.ToString();
            //this.Size = new Size(887,639);
        }
        
        private void leeDiario()
        {
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
           
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT s.JRNAL_NO, s.JRNAL_LINE,s.AMOUNT,s.DESCRIPTN,s.TRANS_DATETIME , s.ACCNT_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s WHERE s.JRNAL_NO = '" + diariosText.Text + "' AND s.ALLOCATION != 'C' AND s.JRNAL_NO = '" + diariosText.Text.Trim() + "'";

                    listaFinal = new List<Dictionary<string, object>>();

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


                                            double amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(4)));


                                           
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
                                        dictionary.Add("ACCNT_CODE", ACCNT_CODE);
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


                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("JRNAL_NO1"))
                                {
                                    string[] arr = new string[7];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["JRNAL_NO1"]);
                                    arr[1] = Convert.ToString(dic["JRNAL_LINE1"]);
                                    arr[2] = Convert.ToString(dic["total"]);
                                    arr[3] = Convert.ToString(dic["DESCRIPTN1"]);
                                    arr[4] = Convert.ToString(dic["TRANS_DATETIME"]);
                                    arr[5] = Convert.ToString(dic["ACCNT_CODE"]);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            leeDiario();
        }

        private void cantidadALigar_TextChanged(object sender, EventArgs e)
        {
            double test = Convert.ToDouble(cantidadALigar.Text.Trim());
            if (test > maximoGlobal)
            {
                System.Windows.Forms.MessageBox.Show("No puedes ligar más de " + maximoGlobal, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                maximoActual = test;
            }
           
        }

        private void lineasList_SelectedIndexChanged(object sender, EventArgs e)
        {
             int cuantos = lineasList.SelectedItems.Count;
             if (cuantos > 0)
             {
                 lineaMaximo = Math.Round(Convert.ToDouble(lineasList.SelectedItems[0].SubItems[2].Text), 2);
                 maximoLineaLabel.Text = "Máximo linea: $" + lineaMaximo.ToString();
                 double cuantoFactura = Math.Round(Convert.ToDouble(cantidadALigar.Text), 2);

                 double cuanto = lineaMaximo;
                 cantidadDeLaLineaText.Text = cuanto.ToString();
             }
        }

        private void modificarCantidadLinea_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cantidadDeLaLineaText_TextChanged(object sender, EventArgs e)
        {
          /* double cuantoFactura = Math.Round(Convert.ToDouble(cantidadALigar.Text), 2);
            if (cuanto > lineaMaximo)
            {
                cuanto = lineaMaximo;
            }
            if(cuantoFactura>cuanto)
            {
                maximoTemporal = cuanto;
                cantidadALigar.TextChanged -= cantidadALigar_TextChanged;
                cantidadDeLaLineaText.TextChanged -= cantidadDeLaLineaText_TextChanged;

                cantidadALigar.Text = maximoTemporal.ToString();
                cantidadDeLaLineaText.Text = maximoTemporal.ToString();

                cantidadALigar.TextChanged += cantidadALigar_TextChanged;
                cantidadDeLaLineaText.TextChanged += cantidadDeLaLineaText_TextChanged;

            }
            else
            {
                maximoTemporal = cuantoFactura;
                cantidadALigar.TextChanged -= cantidadALigar_TextChanged;
                cantidadDeLaLineaText.TextChanged -= cantidadDeLaLineaText_TextChanged;

                cantidadDeLaLineaText.Text = maximoTemporal.ToString();
                cantidadALigar.Text = maximoTemporal.ToString();

                cantidadALigar.TextChanged += cantidadALigar_TextChanged;
                cantidadDeLaLineaText.TextChanged += cantidadDeLaLineaText_TextChanged;
            }
           * */
            //informacionLabel.Text = "La cantidad $" + cantidadDeLaLineaText.Text + " de la linea " + lineasList.SelectedItems[0].SubItems[1].Text + " del diario " + lineasList.SelectedItems[0].SubItems[0].Text + " sera ligada a la factura por la cantidad de $" + cantidadALigar.Text;
           
        }

        private void ligarButton_Click(object sender, EventArgs e)
        {
            int cuantos = lineasList.SelectedItems.Count;
            if (cuantos == 0)
            {
                System.Windows.Forms.MessageBox.Show("Primero debes de seleccionar una linea del diario antes de ligarlo a la factura", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            String cuenta = lineasList.SelectedItems[0].SubItems[5].Text.Trim();
            //checa si la cuenta corresponde al tipo de contabilidad
             String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
           
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT tipoDeContabilidad FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE ACNT_CODE = '" + cuenta+ "' AND unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "'";

                  
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                int tipo = reader.GetInt32(0);
                                if(tipo!=tipoDeContabilidadGlobal && tipo!=3)
                                {
                                    System.Windows.Forms.MessageBox.Show("La cuenta "+cuenta+" no corresponde al tipo de factura (ingreso o egreso) seleccionada.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("La cuenta " + cuenta + " no corresponde al tipo de factura (ingreso o egreso) seleccionada.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

          

            double maximo;
            if(maximoGlobal>lineaMaximo)
            {
                maximo = lineaMaximo;
            }
            else
            {
                maximo = maximoGlobal;
            }

            double cuantoFactura = Math.Round(Convert.ToDouble(cantidadALigar.Text), 2);
            double cuanto = Math.Round(Convert.ToDouble(cantidadDeLaLineaText.Text), 2);
            if(cuantoFactura>maximoGlobal)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $"+maximoGlobal+" de la factura", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cuanto > lineaMaximo)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $" + lineaMaximo + " de la linea", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cuanto > maximoActual)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $" + lineaMaximo + " de la linea, debido a que la cantidad que se va a ligar a la factura, es " + maximoActual, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(cuantoFactura>maximo)
            {
                System.Windows.Forms.MessageBox.Show("Solo puedes ligar $" + maximo, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cuantos > 0)
             {
                 String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

                 String cantidad = cantidadALigar.Text.Trim();
                 String linea = lineasList.SelectedItems[0].SubItems[1].Text.Trim();
                 int consecutivo = 0;
                 String diario = lineasList.SelectedItems[0].SubItems[0].Text.Trim();

                 String queryCheck = "SELECT consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE BUNIT = '" + Login.unidadDeNegocioGlobal + "' and JRNAL_NO = " + diario + " and JRNAL_LINE = " + linea + " order by consecutivo desc";
             
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
                                 query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', "+diario+", " + linea + ", '" + folioFiscalGlobal + "', " + cantidad + ", '1', '" + xmlText + "' , " + consecutivo + ",  '"+Login.sourceGlobal+"')";
                             }
                             else
                             {
                                 if (tipoDeContabilidadGlobal == 2)
                                 {
                                     query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diario + ", " + linea + ", '" + folioFiscalGlobal + "', " + cantidad + ", '2', '" + xmlText + "' , " + consecutivo + ",  '"+Login.sourceGlobal+"')";
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
                     System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                     //  Logs.Escribir("Error en download complete : " + ex.ToString());
                 }
             }
           
        }
    }
}
