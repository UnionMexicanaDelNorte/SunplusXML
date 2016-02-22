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
    public partial class EjecutarPreligue : Form
    {
        public Dictionary<string, List<string>> rfcCuentas { get; set; }
        public Dictionary<string, double> diarioLinea { get; set; }
        public Dictionary<string, double> totalDiarioLinea { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
      
    
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
        public EjecutarPreligue()
        {
            InitializeComponent();
        }

        private void EjecutarPreligue_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
                
            rfcCuentas = new Dictionary<string, List<string>>();
            diarioLinea = new Dictionary<string, double>();
            totalDiarioLinea = new Dictionary<string, double>();
          
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL' order by SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) asc";
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
                            String periodo = reader.GetString(0);
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
            String queryPeriodos1 = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]  WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL' order by SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) asc";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connString))
                {
                    connection1.Open();
                    SqlCommand cmdCheck1 = new SqlCommand(queryPeriodos1, connection1);
                    SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                    int empiezo1 = 1;
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            String periodo1 = reader1.GetString(0);
                            periodosCombo.Items.Add(new Item(periodo1, empiezo1));
                            empiezo1++;
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

        private void Ligar_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            int cuantosLigue = 0;
            rfcCuentas.Clear();
           Item itm = (Item)periodosCombo.SelectedItem;
            Int32 timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            String periodo = itm.Name;
            String periodoParaElLibroA = periodo.Replace('-', '0');
            //YA, primero, obtengo una lista de RFC de la tabla de preligues, con sus cuentas preferidas, 
            //YA, para cada RFC, obtengo las facturas SIN ligar que coincida con ese periodo, aun las facturas ligadas, cuya cantidad no sea 0
            //YA, para cada cuenta preferida, empezando con la prioridad, saco los diario-linea-cantidad que NO estan ligados
            //YA, empiezo a ligar, usando el timestamp
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT rfc, ACNT_CODE, prioridad FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE BUNIT = '" + Login.unidadDeNegocioGlobal + "' order by rfc asc, prioridad asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0).Trim().ToUpper();
                                String cuenta = reader.GetString(1).Trim().ToUpper();
                                if(!rfcCuentas.ContainsKey(rfc))
                                {
                                    rfcCuentas[rfc] = new List<string>();
                                }
                                rfcCuentas[rfc].Add(cuenta);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            int i = 0,j=0;
            String rfcEnTurno="";
            List<string> rfcList = new List<string>(rfcCuentas.Keys);
            for(i=0;i<rfcList.Count;i++)
            {
                rfcEnTurno = rfcList[i];
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        // AND ff.BUNIT = '"+Login.unidadDeNegocioGlobal+"'  DONT SUPPORT FOR MULTIPLE BUNITS!
                        String queryXML = "SELECT f.folioFiscal, f.STATUS,f.fechaExpedicion,f.total, ISNULL(ff.AMOUNT,0) as ligado FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] ff on ff.FOLIO_FISCAL = f.folioFiscal WHERE f.rfc = '"+rfcEnTurno+"' AND SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(10)),1,"+periodo.Length+") = '"+periodo+"' AND f.STATUS =1";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    String folioFiscal = reader.GetString(0).Trim().ToUpper();
                                    String fecha = reader.GetDateTime(2).ToString().Substring(0, 10);
                                    double totalDeLaFactura = Math.Abs( Math.Round( Convert.ToDouble(reader.GetDecimal(3)), 2));
                                    double ligadoDeLaFactura = Math.Abs(Math.Round(Convert.ToDouble(reader.GetDecimal(4)), 2));
                                    double diponibleFactura = Math.Round(totalDeLaFactura - ligadoDeLaFactura,2);
                                    double disponibleEnDiarios = 0;
                                    if(diponibleFactura>0)//si no hay disponible, no tomamos la factura
                                    {
                                        String anteriorDiarioLinea = "";
                                        int first = 1;
                                        if (modoFlojo.Checked)
                                        {//se revisa otra vez con lo ligado
                                            diarioLinea.Clear();
                                            totalDiarioLinea.Clear();
                                        }
                                        else//dejamos en memoria lo ligado
                                        {
                                            
                                        }
                                        for(j=0;j<rfcCuentas[rfcEnTurno].Count;j++)
                                        {
                                            String cuentaEnTurno = rfcCuentas[rfcEnTurno][j];
                                            //solo gastos D_C="debitos"
                                            String queryXML1 = "SELECT c.DESCRIPTN, c.JRNAL_NO , c.JRNAL_LINE,c.AMOUNT, ISNULL(ff.AMOUNT,0)as ligado, ISNULL(ff.FOLIO_FISCAL,'0') as folioFiscal FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] c LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] ff on ff.JRNAL_NO = c.JRNAL_NO AND ff.JRNAL_LINE=c.JRNAL_LINE WHERE c.ACCNT_CODE = '" + cuentaEnTurno + "' AND SUBSTRING( CAST(c.PERIOD AS NVARCHAR(10)),1," + periodoParaElLibroA.Length + ") = '" + periodoParaElLibroA + "' AND c.D_C = 'D' order by c.JRNAL_NO asc, c.JRNAL_LINE asc";
                                            using (SqlCommand cmdCheck1 = new SqlCommand(queryXML1, connection))
                                            {
                                                SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                                if (reader1.HasRows)
                                                {
                                                    while (reader1.Read())
                                                    {
                                                        String JRNAL_NO = Convert.ToString(reader1.GetInt32(1)).Trim();
                                                        String JRNAL_LINE = Convert.ToString(reader1.GetInt32(2)).Trim();
                                                        String diarioLineaString = JRNAL_NO + "-" + JRNAL_LINE;
                                                        if(first==1)
                                                        {
                                                            first = 0;
                                                            anteriorDiarioLinea = diarioLineaString;
                                                        }
                                                        if(!diarioLinea.ContainsKey(diarioLineaString))//si no contiene ya esa llave
                                                        {
                                                            diarioLinea[diarioLineaString] = 0.0;
                                                        }
                                                        double totalDelMovimiento = Math.Abs(Math.Round(Convert.ToDouble(reader1.GetDecimal(3)), 2));
                                                        totalDiarioLinea[diarioLineaString] = totalDelMovimiento;
                                                        String DESCRIPTN = reader1.GetString(0).Trim();
                                                        double ligadoDelMovimiento = Math.Abs(Math.Round(Convert.ToDouble(reader1.GetDecimal(4)), 2));
                                                        diarioLinea[diarioLineaString] = Math.Round(diarioLinea[diarioLineaString],2) +Math.Round(ligadoDelMovimiento,2);
                                                        if (!diarioLineaString.Equals(anteriorDiarioLinea))
                                                        {
                                                            disponibleEnDiarios += Math.Round((totalDiarioLinea[anteriorDiarioLinea]- diarioLinea[anteriorDiarioLinea]),2);
                                                            if(disponibleEnDiarios>=diponibleFactura)
                                                            {
                                                                j = rfcCuentas[rfcEnTurno].Count + 1;//salte del for tambien
                                                                break;//only while
                                                                
                                                            }
                                                        }
                                                        anteriorDiarioLinea = diarioLineaString;
                                                        //double diponibleDelMovimiento = totalDelMovimiento - ligadoDelMovimiento;
                                                    }
                                                    //si tatal disponible de
                                                    List<string> diariosLineasKeys = new List<string>(diarioLinea.Keys);
                                                    for (int l = 0; l < diariosLineasKeys.Count; l++)
                                                    {
                                                        if(diponibleFactura<0.01)
                                                        {
                                                            diponibleFactura = 0.000;
                                                            break;
                                                        }
                                                        String diarioLineaEnTurno = diariosLineasKeys[l];
                                                        String [] arrayAux = diarioLineaEnTurno.Split('-');
                                                        String diario = arrayAux[0];
                                                        String linea = arrayAux[1];
                                                        double disponibleEnEsteMomento = Math.Round(totalDiarioLinea[diarioLineaEnTurno] - diarioLinea[diarioLineaEnTurno], 2);
                                                        double cuantoVoyALigarRealmente = 0;
                                                        if (diponibleFactura >= disponibleEnEsteMomento)
                                                        {
                                                            cuantoVoyALigarRealmente = disponibleEnEsteMomento;
                                                        }
                                                        else
                                                        {
                                                            cuantoVoyALigarRealmente = diponibleFactura;
                                                        }
                                                        if (cuantoVoyALigarRealmente>0.009)
                                                        {
                                                            if (modoFlojo.Checked)
                                                            {
                                                                int consecutivo = 0;
                                                                String queryCheck = "SELECT consecutivo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE BUNIT = '" + Login.unidadDeNegocioGlobal + "' and JRNAL_NO = " + diario + " and JRNAL_LINE = " + linea + " order by consecutivo desc";
                                                                SqlCommand cmdCheckX = new SqlCommand(queryCheck, connection);
                                                                SqlDataReader readerX = cmdCheckX.ExecuteReader();
                                                                if (readerX.HasRows)
                                                                {
                                                                    if (readerX.Read())
                                                                    {
                                                                        consecutivo = readerX.GetInt32(0);
                                                                    }
                                                                }
                                                                consecutivo++;
                                                                String queryInsert = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] (BUNIT,JRNAL_NO,JRNAL_LINE,FOLIO_FISCAL,AMOUNT,STATUS,XML,consecutivo,JRNAL_SOURCE,autoligado) VALUES ('" + Login.unidadDeNegocioGlobal + "', " + diario + ", " + linea + ", '" + folioFiscal + "', " + cuantoVoyALigarRealmente + ", '1', '' , " + consecutivo + ",  '" + Login.sourceGlobal + "', " + timestamp + ")";
                                                                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, connection))
                                                                {
                                                                    cmdInsert.ExecuteNonQuery();
                                                                    cuantosLigue++;
                                                                    diponibleFactura -= Math.Round(cuantoVoyALigarRealmente, 2);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                diarioLinea[diarioLineaEnTurno] += cuantoVoyALigarRealmente;
                                                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                                                dictionary.Add("cuenta", cuentaEnTurno);
                                                                dictionary.Add("diario", diario);
                                                                dictionary.Add("linea", linea);
                                                                dictionary.Add("folioFiscal", folioFiscal);
                                                                dictionary.Add("cuantoVoyALigarRealmente", cuantoVoyALigarRealmente);
                                                                dictionary.Add("timestamp", timestamp);
                                                                dictionary.Add("rfc", rfcEnTurno);
                                                                listaFinal.Add(dictionary);
                                                                cuantosLigue++;
                                                                diponibleFactura -= cuantoVoyALigarRealmente;
                                                            }
                                                        }
                                                        //despues de hacer este insert.. debo de volver a leer las facturas pendientes por ligar?
                                                        //aunque solo leo los mov pendientes por ligar, parece que esta bien... uhm...
                                                    }                                                    
                                                }
                                            }
                                        }//for rfcCuentas
                                    }//id disponibleFactura >0
                                }
                            }
                        } 
                    }
                    //connection.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }//for rfcList
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            if(modoFlojo.Checked)
            {
                System.Windows.Forms.MessageBox.Show("Se realizaron " + cuantosLigue + " ligues. Si quiere deshacer algún ligue vaya al historial de preligues.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PreligueResponsable form = new PreligueResponsable(listaFinal);
                form.Show();
            }
            this.Close();
        }
    }
}
