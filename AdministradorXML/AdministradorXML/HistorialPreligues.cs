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
    public partial class HistorialPreligues : Form
    {
        public HistorialPreligues()
        {
            InitializeComponent();
        }
        private void rellena()
        {
            treeView1.Nodes.Clear();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT autoligado FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE autoligado>0 order by autoligado desc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Int32 timestamp = reader.GetInt32(0);
                            String fecha = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp).ToLocalTime().ToString();
                            TreeNode papa = new TreeNode();
                            papa.Text = fecha + "|"+timestamp;

                            String queryXML1 = "SELECT c.DESCRIPTN, c.ACCNT_CODE, ff.JRNAL_NO , ff.JRNAL_LINE, f.fechaExpedicion, c.PERIOD, ff.AMOUNT, c.AMOUNT, ff.FOLIO_FISCAL, f.rfc,f.razonSocial,f.ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] ff INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] c on ff.JRNAL_NO = c.JRNAL_NO AND ff.JRNAL_LINE=c.JRNAL_LINE INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f on f.folioFiscal = ff.FOLIO_FISCAL WHERE ff.autoligado = " + timestamp;
                            using (SqlCommand cmdCheck1 = new SqlCommand(queryXML1, connection))
                            {
                                SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        String DESCRIPTN = reader1.GetString(0).Trim();
                                        String cuenta = reader1.GetString(1).Trim();
                                        int diario = reader1.GetInt32(2);
                                        int linea = reader1.GetInt32(3);
                                        String fechaFactura = reader1.GetDateTime(4).ToString().Substring(0, 10);
                                        int periodo = reader1.GetInt32(5);
                                        double cantidadLigadaDeLaFactura = Math.Abs(Math.Round(Convert.ToDouble(reader1.GetDecimal(6)), 2));
                                        double cantidadEnLosLibros = Math.Abs(Math.Round(Convert.ToDouble(reader1.GetDecimal(7)), 2));
                                        String folioFiscal = reader1.GetString(8).Trim();
                                        String rfc = reader1.GetString(9).Trim();
                                        String razonSocial = reader1.GetString(10).Trim();
                                        String ruta = reader1.GetString(11).Trim();
                                        TreeNode hijo = new TreeNode();
                                        hijo.Text = "" + diario + "-" + linea + ",   $" + String.Format("{0:n}", cantidadEnLosLibros) + ",   " + DESCRIPTN + ",  " + periodo + ",   " + rfc + ",   " + razonSocial + ",  " + fechaFactura + ",   $" + String.Format("{0:n}", cantidadLigadaDeLaFactura)+" ,"+folioFiscal;
                                        papa.Nodes.Add(hijo);
                                    }
                                }
                            }
                            treeView1.Nodes.Add(papa);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen preligues, primero debes ligar con el preligue.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void HistorialPreligues_Load(object sender, EventArgs e)
        {
            rellena();
        }

        private void eliminarPreligue_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("¿Estas seguro de eliminar este preligue? Esta acción no se puede deshacer",
                                     "Confirmar",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                TreeNode talves = treeView1.SelectedNode;
                if(talves==null)
                {
                    System.Windows.Forms.MessageBox.Show("primero debes de seleccionar", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                String stringConQueTrabajar = talves.Text;
                if(stringConQueTrabajar.Contains('|'))
                {
                    String[] arrayAux = stringConQueTrabajar.Split('|');
                    String fechaParaMostrar = arrayAux[0];
                    String timeStampParaBorrar = arrayAux[1];
                    String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connString))
                        {
                            connection.Open();
                            String queryXML = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE autoligado = " + timeStampParaBorrar;
                            using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                            {
                                cmdCheck.ExecuteNonQuery();
                                System.Windows.Forms.MessageBox.Show("Ya se borró todo el autoligue realizado en: " + fechaParaMostrar, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rellena();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }                                 
                }
                else//borra una sola
                {
                    String[] arrayAux = stringConQueTrabajar.Split(',');
                    String diarioLinea = arrayAux[0];
                    String folioFiscalABorrar = arrayAux[arrayAux.Length - 1];
                    String[] arrayAuxDL = diarioLinea.Split('-');
                    String diario = arrayAuxDL[0];
                    String linea = arrayAuxDL[1];
                    String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connString))
                        {
                            connection.Open();
                            String queryXML = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE autoligado != 0 AND JRNAL_NO = " + diario+" AND JRNAL_LINE = "+linea+" AND FOLIO_FISCAL = '"+folioFiscalABorrar+"' AND BUNIT = '"+Login.unidadDeNegocioGlobal+"'";
                            using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                            {
                                cmdCheck.ExecuteNonQuery();
                                System.Windows.Forms.MessageBox.Show("Ya se borró lo autoligado de la factura: " + folioFiscalABorrar, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                rellena();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }          
                }
            }
            else
            {
                // If 'No', do something here.
            }
        }

        private void verMovimiento_Click(object sender, EventArgs e)
        {
            TreeNode talves = treeView1.SelectedNode;
            if(talves==null)
            {
                System.Windows.Forms.MessageBox.Show("primero debes de seleccionar", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            String stringConQueTrabajar = talves.Text;
            if (stringConQueTrabajar.Contains('|'))
            {
            }
            else//checa el mov
            {
                String[] arrayAux = stringConQueTrabajar.Split(',');
                String diarioLinea = arrayAux[0];
                String folioFiscalABorrar = arrayAux[arrayAux.Length - 1];
                String[] arrayAuxDL = diarioLinea.Split('-');
                String diario = arrayAuxDL[0];
                String linea = arrayAuxDL[1];
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ACCNT_CODE, PERIOD, TRANS_DATETIME, AMOUNT,D_C,JRNAL_TYPE,JRNAL_SRCE,TREFERENCE,DESCRIPTN, ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9 FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].["+Login.unidadDeNegocioGlobal+"_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = "+diario+" AND JRNAL_LINE = "+linea;
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
                                    if(D_C.Equals("C"))
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
                                    cad.Append("Cuenta: " + cuenta+"      "+descripcion);
                                    cad.Append("\nDiario: " + diario+" Linea: "+linea);
                                    cad.Append("\nPeriodo: " + periodo + " Fecha T: " + fecha);
                                    cad.Append("\nCantidad: $" + String.Format("{0:n}", cantidad) + "   " + tipoX);
                                    cad.Append("\nTipo de diario: " + tipoDeDiario + " Quien lo hizo: " + source);
                                    cad.Append("\nReferencia: " + referencia + " Ref2: " + ANAL_T0);
                                    cad.Append("\n " + ANAL_T1 + " ");
                                    cad.Append("\n Fondo: " + ANAL_T2 + " Función: "+ANAL_T3+" Restricción: "+ANAL_T4);
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

        private void verPoliza_Click(object sender, EventArgs e)
        {
            TreeNode talves = treeView1.SelectedNode;
            if (talves == null)
            {
                System.Windows.Forms.MessageBox.Show("primero debes de seleccionar", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            String stringConQueTrabajar = talves.Text;
            if (stringConQueTrabajar.Contains('|'))
            {
            }
            else//checa el mov
            {
                String[] arrayAux = stringConQueTrabajar.Split(',');
                String diarioLinea = arrayAux[0];
                String folioFiscalABorrar = arrayAux[arrayAux.Length - 1];
                String[] arrayAuxDL = diarioLinea.Split('-');
                String diario = arrayAuxDL[0];
                String linea = arrayAuxDL[1];
                VerPolizas pol = new VerPolizas(Convert.ToInt32(diario));
                pol.Show();
            }
        }

        private void verPDF_Click(object sender, EventArgs e)
        {
            TreeNode talves = treeView1.SelectedNode;
            if (talves == null)
            {
                System.Windows.Forms.MessageBox.Show("primero debes de seleccionar", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            String stringConQueTrabajar = talves.Text;
            if (stringConQueTrabajar.Contains('|'))
            {
            }
            else//checa el mov
            {
                String[] arrayAux = stringConQueTrabajar.Split(',');
                String diarioLinea = arrayAux[0];
                String folioFiscalABuscar = arrayAux[arrayAux.Length - 1];
                 String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                 try
                 {
                     using (SqlConnection connection = new SqlConnection(connString))
                     {
                         connection.Open();
                         String queryXML = "SELECT ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscalABuscar + "'";
                         using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                         {
                             SqlDataReader reader = cmdCheck.ExecuteReader();
                             if (reader.HasRows)
                             {
                                 if (reader.Read())
                                 {
                                     String ruta = reader.GetString(0).Trim().ToUpper();
                                     String nombre = ruta + (object)Path.DirectorySeparatorChar + folioFiscalABuscar+".pdf";
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

        private void verXML_Click(object sender, EventArgs e)
        {
            TreeNode talves = treeView1.SelectedNode;
            if (talves == null)
            {
                System.Windows.Forms.MessageBox.Show("primero debes de seleccionar", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            String stringConQueTrabajar = talves.Text;
            if (stringConQueTrabajar.Contains('|'))
            {
            }
            else//checa el mov
            {
                String[] arrayAux = stringConQueTrabajar.Split(',');
                String diarioLinea = arrayAux[0];
                String folioFiscalABuscar = arrayAux[arrayAux.Length - 1];
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String queryXML = "SELECT ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscalABuscar + "'";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    String ruta = reader.GetString(0).Trim().ToUpper();
                                    String nombre = ruta + (object)Path.DirectorySeparatorChar + folioFiscalABuscar + ".xml";
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
    }
}
