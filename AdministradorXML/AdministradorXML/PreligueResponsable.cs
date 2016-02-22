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
    public partial class PreligueResponsable : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
       
        public PreligueResponsable()
        {
            InitializeComponent();
        }

        public PreligueResponsable(List<Dictionary<string, object>> auxLista)
        {
            InitializeComponent();
            listaFinal = auxLista;
        }

        private void listaPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void PreligueResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('p') || e.KeyChar.Equals('P'))
            {
                verPDFA();
            }
            if (e.KeyChar.Equals('o') || e.KeyChar.Equals('O'))
            {
                vePoliza();
            }
            if (e.KeyChar.Equals('x') || e.KeyChar.Equals('X'))
            {
                verXMLA();
            }
            if (e.KeyChar.Equals('r') || e.KeyChar.Equals('R'))
            {
                rechazarA();
            }
            if (e.KeyChar.Equals('l') || e.KeyChar.Equals('L'))
            {
                ligarA();
            }
            if (e.KeyChar.Equals('m') || e.KeyChar.Equals('M'))
            {
                veMovimiento();
            }
        }
        private void listaPreview_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void PreligueResponsable_Load(object sender, EventArgs e)
        {
            listaPreview.Clear();
            listaPreview.View = View.Details;
            listaPreview.GridLines = true;
            listaPreview.FullRowSelect = true;
            listaPreview.Columns.Add("Cuenta", 120);
            listaPreview.Columns.Add("Diario", 120);
            listaPreview.Columns.Add("Linea", 120);
            listaPreview.Columns.Add("Folio fiscal", 0);
            listaPreview.Columns.Add("Cantidad", 120);
            listaPreview.Columns.Add("timestamp", 0);
            listaPreview.Columns.Add("cantidadSinFormato", 0);
            listaPreview.Columns.Add("RFC", 120);
            foreach (Dictionary<string, object> dic in listaFinal)
            {
                if (dic.ContainsKey("cuenta"))
                {
                    string[] arr = new string[9];
                    ListViewItem itm;
                    arr[0] = Convert.ToString(dic["cuenta"]);
                    arr[1] = Convert.ToString(dic["diario"]);
                    arr[2] = Convert.ToString(dic["linea"]);
                    arr[3] = Convert.ToString(dic["folioFiscal"]);
                    arr[4] = String.Format("{0:n}", Convert.ToDouble(dic["cuantoVoyALigarRealmente"]));
                    arr[5] = Convert.ToString(dic["timestamp"]);
                    arr[6] = Convert.ToString(dic["cuantoVoyALigarRealmente"]);
                    arr[7] = Convert.ToString(dic["rfc"]);
                    itm = new ListViewItem(arr);
                    listaPreview.Items.Add(itm);
                }
            }
            this.KeyPress+=PreligueResponsable_KeyPress;
            listaPreview.Items[0].Selected = true;
        }

        public void verPDFA()
        {
            int cuantos = listaPreview.SelectedItems.Count;
            if(cuantos>0)
            {
                String folioFiscal = listaPreview.SelectedItems[0].SubItems[3].Text.ToString().Trim();
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "'";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    String ruta = reader.GetString(0).Trim().ToUpper();
                                    String nombre = ruta + (object)Path.DirectorySeparatorChar + folioFiscal + ".pdf";
                                    System.Diagnostics.Process.Start(nombre);
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        private void vePoliza()
        {
             int cuantos = listaPreview.SelectedItems.Count;
             if (cuantos > 0)
             {
                 String diario = listaPreview.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                 VerPolizas pol = new VerPolizas(Convert.ToInt32(diario));
                 pol.Show();
             }
        }

        private void veMovimiento()
        {
            int cuantos = listaPreview.SelectedItems.Count;
            if (cuantos > 0)
            {
                String diario = listaPreview.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                String linea = listaPreview.SelectedItems[0].SubItems[2].Text.ToString().Trim();
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ACCNT_CODE, PERIOD, TRANS_DATETIME, AMOUNT,D_C,JRNAL_TYPE,JRNAL_SRCE,TREFERENCE,DESCRIPTN, ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9 FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = " + diario + " AND JRNAL_LINE = " + linea;
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    String cuenta = reader.GetString(0).Trim().ToUpper();
                                    int periodo = reader.GetInt32(1);
                                    String fecha = reader.GetDateTime(2).ToString().Substring(0, 10);
                                    double cantidad = Math.Round(Math.Abs(Convert.ToDouble(reader.GetDecimal(3))), 2);
                                    String D_C = reader.GetString(4);
                                    String tipoX = "Debito";
                                    if (D_C.Equals("C"))
                                    {
                                        tipoX = "Credito";
                                    }
                                    String tipoDeDiario = reader.GetString(5);
                                    String source = reader.GetString(6);
                                    String referencia = reader.GetString(7);
                                    String descripcion = reader.GetString(8);
                                    String ANAL_T0 = reader.GetString(9);
                                    String ANAL_T1 = reader.GetString(10);
                                    String ANAL_T2 = reader.GetString(11);
                                    String ANAL_T3 = reader.GetString(12);
                                    String ANAL_T4 = reader.GetString(13);
                                    String ANAL_T5 = reader.GetString(14);
                                    String ANAL_T6 = reader.GetString(15);
                                    String ANAL_T7 = reader.GetString(16);
                                    String ANAL_T8 = reader.GetString(17);
                                    String ANAL_T9 = reader.GetString(18);
                                    StringBuilder cad = new StringBuilder("");
                                    cad.Append("Cuenta: " + cuenta + "      " + descripcion);
                                    cad.Append("\nDiario: " + diario + " Linea: " + linea);
                                    cad.Append("\nPeriodo: " + periodo + " Fecha T: " + fecha);
                                    cad.Append("\nCantidad: $" + String.Format("{0:n}", cantidad) + "   " + tipoX);
                                    cad.Append("\nTipo de diario: " + tipoDeDiario + " Quien lo hizo: " + source);
                                    cad.Append("\nReferencia: " + referencia + " Ref2: " + ANAL_T0);
                                    cad.Append("\n " + ANAL_T1 + " ");
                                    cad.Append("\n Fondo: " + ANAL_T2 + " Función: " + ANAL_T3 + " Restricción: " + ANAL_T4);
                                    cad.Append("\n ORGID: " + ANAL_T5 + " WHO: " + ANAL_T6 + " FLAG: " + ANAL_T7);
                                    cad.Append("\n Proyecto: " + ANAL_T8 + " Detalle: " + ANAL_T9 + " ");
                                    System.Windows.Forms.MessageBox.Show(cad.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void ligarA()
        {
            int cuantos = listaPreview.SelectedItems.Count;
            if (cuantos > 0)
            {
                String diario = listaPreview.SelectedItems[0].SubItems[1].Text.ToString().Trim();
                String linea = listaPreview.SelectedItems[0].SubItems[2].Text.ToString().Trim();
                String folioFiscal = listaPreview.SelectedItems[0].SubItems[3].Text.ToString().Trim();

                String timestamp = listaPreview.SelectedItems[0].SubItems[5].Text.ToString().Trim();
                String cuantoVoyALigarRealmente = listaPreview.SelectedItems[0].SubItems[6].Text.ToString().Trim();
               
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
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
                            listaPreview.Items.RemoveAt(listaPreview.SelectedItems[0].Index);
                            if(listaPreview.Items.Count==0)
                            {
                                System.Windows.Forms.MessageBox.Show("Ya terminaste con la lista, gracias", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else 
                            {
                                listaPreview.Items[0].Selected = true;
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        public void verXMLA()
        {
            int cuantos = listaPreview.SelectedItems.Count;
            if (cuantos > 0)
            {
                String folioFiscal = listaPreview.SelectedItems[0].SubItems[3].Text.ToString().Trim();
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "'";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    String ruta = reader.GetString(0).Trim().ToUpper();
                                    String nombre = ruta + (object)Path.DirectorySeparatorChar + folioFiscal + ".xml";
                                    System.Diagnostics.Process.Start(nombre);
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void verPDF_Click(object sender, EventArgs e)
        {
            verPDFA();
        }

        private void verXML_Click(object sender, EventArgs e)
        {
            verXMLA();
        }

        private void verPoliza_Click(object sender, EventArgs e)
        {
            vePoliza();
        }

        private void verMovimiento_Click(object sender, EventArgs e)
        {
            veMovimiento();
        }
        private void rechazarA()
        {
            int cuantos = listaPreview.SelectedItems.Count;
            if(cuantos>0)
            {
                listaPreview.Items.RemoveAt(listaPreview.SelectedItems[0].Index);
                if(listaPreview.Items.Count>0)
                {
                    listaPreview.Items[0].Selected = true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Ya terminaste con la lista, gracias", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void rechazar_Click(object sender, EventArgs e)
        {
            rechazarA();
        }

        private void ligar_Click(object sender, EventArgs e)
        {
            ligarA();
        }
    }
}
