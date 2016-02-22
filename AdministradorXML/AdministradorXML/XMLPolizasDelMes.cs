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
using System.IO.Compression;
namespace AdministradorXML
{
    public partial class XMLPolizasDelMes : Form
    {
        public StringBuilder cad { get; set; }
        public XmlDocument doc { get; set; }
       
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
        public XMLPolizasDelMes()
        {
            InitializeComponent();
        }

        private void XMLPolizasDelMes_Load(object sender, EventArgs e)
        {
            doc = new XmlDocument();
          
            tipoSolicitudCombo.Items.Add(new Item("Acto de Fiscalización", 0,"AF"));
            tipoSolicitudCombo.Items.Add(new Item("Fiscalización Compulsa", 1, "FC"));
            tipoSolicitudCombo.Items.Add(new Item("Devolución", 2, "DE"));
            tipoSolicitudCombo.Items.Add(new Item("Compensación", 3, "CO"));
            tipoSolicitudCombo.SelectedIndex = 0;
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT PERIOD FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by PERIOD asc";
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
                            String periodo = Convert.ToString(reader.GetInt32(0));
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
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 2;
/*
                         
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML]";
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
 * */
        }
        private void actualiza()
        {
 
        }
        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }
        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode,
     TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                      xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    newTreeNode.Text = xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }

        private void generarButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Item itm = (Item)periodosCombo.SelectedItem;
            Item itm2 = (Item)tipoSolicitudCombo.SelectedItem;

            String periodo = itm.Name;
            String tipoDeSolicitud = itm2.Extra;
            String NumOrdenT = "", NumTramiteT = "";
            String year = periodo.Substring(0, 4);
            String month = periodo.Substring(5, 2);
            String periodoParaQuery = year + "0" + month;



            cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<PLZ:Polizas xsi:schemaLocation=\"www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo/PolizasPeriodo_1_1.xsd\"" +
              "  Version=\"1.1\" RFC=\"" + Properties.Settings.Default.rfcGlobal + "\"" +
                   " Mes=\"" + month + "\" Anio=\"" + year + "\" TipoSolicitud=\"" + tipoDeSolicitud + "\" ");




            cad.Append(" xmlns:PLZ=\"http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
        
            if (tipoDeSolicitud.Equals("AF") || tipoDeSolicitud.Equals("FC"))
            {
                NumOrdenT = NumOrden.Text.Trim();
                if (NumOrdenT.Length != 13)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Arrow;
           
                    System.Windows.Forms.MessageBox.Show("Debes de escribir el numero de orden y este debe de tener 13 caracteres", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    cad.Append("NumOrden=\"" + NumOrdenT + "\"");
                }
            }
            if (tipoDeSolicitud.Equals("DE") || tipoDeSolicitud.Equals("CO"))
            {
                NumTramiteT = NumTramite.Text.Trim();
                if (NumTramiteT.Length != 10)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Arrow;
           
                    System.Windows.Forms.MessageBox.Show("Debes de escribir el numero de tramite y este debe de tener 10 caracteres", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    cad.Append("NumTramite=\"" + NumTramiteT + "\"");
                }
            }

            cad.Append(">");
            //seleciono todas las polizas de sunplus ordenadas por numero de diario, inner join fiscal_xml, inner join nombre cuenta
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //String queryXML = "SELECT c.ACCNT_CODE, a.DESCR, c.JRNAL_NO, c.JRNAL_LINE, c.TRANS_DATETIME,c.TREFERENCE, c.DESCRIPTN, f.FOLIO_FISCAL, f.CONCEPTO, f.AMOUNT, ff.rfc, f.BUNIT, c.D_C, c.AMOUNT, c.ANAL_T0, c.ANAL_T1, c.ANAL_T2, c.ANAL_T3, c.ANAL_T4, c.ANAL_T5, c.ANAL_T6, c.ANAL_T7, c.ANAL_T8, c.ANAL_T9 FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] c INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = c.ACCNT_CODE LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f on f.JRNAL_NO = c.JRNAL_NO AND f.JRNAL_LINE = c.JRNAL_LINE LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] ff on ff.folioFiscal = f.FOLIO_FISCAL WHERE c.PERIOD = '" + periodo + "' order by c.JRNAL_NO asc, c.JRNAL_LINE asc";

                    String queryXML = "SELECT c.ACCNT_CODE, a.DESCR, c.JRNAL_NO, c.JRNAL_LINE, c.TRANS_DATETIME,c.TREFERENCE, c.DESCRIPTN, f.FOLIO_FISCAL, f.CONCEPTO, f.AMOUNT, ff.rfc, f.BUNIT, c.D_C, c.AMOUNT, c.ANAL_T0, c.ANAL_T1, c.ANAL_T2, c.ANAL_T3, c.ANAL_T4, c.ANAL_T5, c.ANAL_T6, c.ANAL_T7, c.ANAL_T8, c.ANAL_T9, ISNULL(aa.tipoDeContabilidad,0) as tipoDeContabilidad FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] c INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = c.ACCNT_CODE LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] aa on aa.ACNT_CODE COLLATE SQL_Latin1_General_CP1_CI_AS = c.ACCNT_CODE LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f on f.JRNAL_NO = c.JRNAL_NO AND f.JRNAL_LINE = c.JRNAL_LINE LEFT JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] ff on ff.folioFiscal = f.FOLIO_FISCAL WHERE c.PERIOD = '" + periodo + "' AND aa.unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "' order by c.JRNAL_NO asc, c.JRNAL_LINE asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            bool first = true;
                            double debe = 0, haber = 0;
                            int diarioActual = 0, diarioAnterior = 0;
                            while (reader.Read())
                            {
                                String ACNT_CODE = ""; 
                                if (!reader.IsDBNull(0))
                                {
                                    ACNT_CODE = reader.GetString(0).Trim();
                                }
                                String DESCR_ACNT_CODE = "";
                                if (!reader.IsDBNull(1))
                                {
                                    DESCR_ACNT_CODE = reader.GetString(1).Trim();
                                }
                                DESCR_ACNT_CODE = DESCR_ACNT_CODE.Replace("&", "&amp;");
                                DESCR_ACNT_CODE = DESCR_ACNT_CODE.Replace("\"", "");

                                DESCR_ACNT_CODE = DESCR_ACNT_CODE.Replace("<", "");
                                DESCR_ACNT_CODE = DESCR_ACNT_CODE.Replace(">", "");


                                String JRNAL_NO = "";
                                if (!reader.IsDBNull(2))
                                {
                                    JRNAL_NO = Convert.ToString(reader.GetInt32(2)).Trim();
                                }

                                String JRNAL_LINE = "";
                                if (!reader.IsDBNull(3))
                                {
                                    JRNAL_LINE = Convert.ToString(reader.GetInt32(3)).Trim();
                                }
                                String TRANS_DATETIME = "";
                                if (!reader.IsDBNull(4))
                                {
                                    TRANS_DATETIME = reader.GetDateTime(4).ToString().Substring(0, 10);
                                }
                                String ano = TRANS_DATETIME.Substring(6, 4);
                                String dia = TRANS_DATETIME.Substring(0, 2);
                                String mes = TRANS_DATETIME.Substring(3, 2);
                                TRANS_DATETIME = ano + "-" + mes + "-" + dia;
                                //TRANS_DATETIME = TRANS_DATETIME.Replace("/", "-");


                                String DESCRIPTN = "";
                                if (!reader.IsDBNull(6))
                                {
                                    DESCRIPTN = reader.GetString(6).Trim();
                                }
                                String TREFERENCE = "";
                                if (!reader.IsDBNull(5))
                                {
                                    TREFERENCE = reader.GetString(5).Trim();
                                }
                                else
                                {
                                    TREFERENCE = DESCRIPTN;
                                }
                                DESCRIPTN = DESCRIPTN.Replace("<", "");
                                DESCRIPTN = DESCRIPTN.Replace(">", "");
                              
                                DESCRIPTN = DESCRIPTN.Replace("&", "&amp;");
                                TREFERENCE = TREFERENCE.Replace("&", "&amp;");
                                DESCRIPTN = DESCRIPTN.Replace("\"", "");
                                TREFERENCE = TREFERENCE.Replace("\"", "");
                                TREFERENCE = TREFERENCE.Replace("<", "");
                                TREFERENCE = TREFERENCE.Replace(">", "");
                              

                                String FOLIO_FISCAL = "";
                                String CONCEPTO = "";
                                String rfc = "";
                                String BUNIT = "";
                                String AMOUNT_FISCAL = "";
                                if (!reader.IsDBNull(7))
                                {
                                     FOLIO_FISCAL = reader.GetString(7).Trim();
                                     if (!reader.IsDBNull(8))
                                     {
                                         CONCEPTO = reader.GetString(8).Trim();
                                     }
                                     if (!reader.IsDBNull(10))
                                     {
                                         rfc = reader.GetString(10).Trim();
                                     }
                                     if (!reader.IsDBNull(11))
                                     {
                                         BUNIT = reader.GetString(11).Trim();
                                     }
                                     if (!reader.IsDBNull(9))
                                     {
                                         AMOUNT_FISCAL = Convert.ToString(reader.GetDecimal(9));
                                     }
                                }

                                String D_C = "";
                                if (!reader.IsDBNull(12))
                                {
                                    D_C = reader.GetString(12).Trim();
                                }
                                String AMOUNT_SUNPLUS = "";
                                if (!reader.IsDBNull(13))
                                {
                                    AMOUNT_SUNPLUS = Convert.ToString(reader.GetDecimal(13)).Trim();
                                }
                                String ANAL_T0 = "";
                                String ANAL_T1 = "";
                                String ANAL_T2 = "";
                                String ANAL_T3 = "";
                                String ANAL_T4 = "";
                                String ANAL_T5 = "";
                                String ANAL_T6 = "";
                                String ANAL_T7 = "";
                                String ANAL_T8 = "";
                                String ANAL_T9 = "";
                                String tipoDeContabilidad = "0";
                               
                                
                                if (!reader.IsDBNull(14))
                                {
                                    ANAL_T0 = reader.GetString(14).Trim();
                                }

                                if (!reader.IsDBNull(15))
                                {
                                    ANAL_T1 = reader.GetString(15).Trim();
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    ANAL_T2 = reader.GetString(16).Trim();
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    ANAL_T3 = reader.GetString(17).Trim();
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    ANAL_T4 = reader.GetString(18).Trim();
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    ANAL_T5 = reader.GetString(19).Trim();
                                }
                                if (!reader.IsDBNull(20))
                                {
                                    ANAL_T6 = reader.GetString(20).Trim();
                                }
                                if (!reader.IsDBNull(21))
                                {
                                    ANAL_T7 = reader.GetString(21).Trim();
                                }
                                if (!reader.IsDBNull(22))
                                {
                                    ANAL_T8 = reader.GetString(22).Trim();
                                }
                                if (!reader.IsDBNull(23))
                                {
                                    ANAL_T9 = reader.GetString(23).Trim();
                                }
                                if (!reader.IsDBNull(24))
                                {
                                    tipoDeContabilidad = Convert.ToString( reader.GetInt32(24));
                                }
                                 
                               
                                
                                if (first)
                                {
                                    first = false;
                                    diarioAnterior = Convert.ToInt32(JRNAL_NO);
                                    diarioActual = diarioAnterior;
                                    //primer diario
                                    String cualConceptoTomo = "";
                                    if (CONCEPTO == null || CONCEPTO.Equals(""))
                                    {
                                        cualConceptoTomo = TREFERENCE;
                                    }
                                    else
                                    {
                                        cualConceptoTomo = CONCEPTO;
                                    }
                                    cualConceptoTomo = cualConceptoTomo.Replace("&", "&amp;");
                                    cualConceptoTomo = cualConceptoTomo.Replace("\"", "");

                                    cualConceptoTomo = cualConceptoTomo.Replace("<", "");
                                    cualConceptoTomo = cualConceptoTomo.Replace(">", "");
                              

                                
                                    cad.Append("<PLZ:Poliza NumUnIdenPol=\"" + JRNAL_NO + "\" Fecha=\"" + TRANS_DATETIME + "\"  Concepto=\"" + cualConceptoTomo + "\" >");
                                }
                                else
                                {
                                    diarioActual = Convert.ToInt32(JRNAL_NO);
                                }

                                if (diarioActual != diarioAnterior)
                                {
                                    //diario nuevo!!
                                    //cierro diario anterior y abro uno nuevo
                                    String cualConceptoTomo = "";
                                    if (CONCEPTO == null || CONCEPTO.Equals(""))
                                    {
                                        cualConceptoTomo = TREFERENCE;
                                    }
                                    else
                                    {
                                        cualConceptoTomo = CONCEPTO;
                                    }
                                    cualConceptoTomo = cualConceptoTomo.Replace("&", "&amp;");
                                    cualConceptoTomo = cualConceptoTomo.Replace("\"", "");
                                    cualConceptoTomo = cualConceptoTomo.Replace("<", "");
                                    cualConceptoTomo = cualConceptoTomo.Replace(">", "");
                              
                                    cad.Append("</PLZ:Poliza><PLZ:Poliza NumUnIdenPol=\"" + JRNAL_NO + "\" Fecha=\"" + TRANS_DATETIME + "\"  Concepto=\"" + cualConceptoTomo + "\" >");

                                }




                                diarioAnterior = diarioActual;
                                //agregar lineas!
                                debe = 0.0;
                                haber = 0.0;
                                if (D_C.Equals("D"))//debito
                                {
                                    debe = Math.Abs(Convert.ToDouble(AMOUNT_SUNPLUS));
                                }
                                else
                                {
                                    haber = Math.Abs(Convert.ToDouble(AMOUNT_SUNPLUS));
                                }
                                if(paraSatsito.Checked)
                                {
                                    cad.Append("<PLZ:Transaccion NumCta=\"" + ACNT_CODE + "\" DesCta=\"" + DESCR_ACNT_CODE + "\" Concepto=\"" + DESCRIPTN + "\" Debe=\"" + debe + "\" Haber=\"" + haber + "\" ANAL_T0=\"" + ANAL_T0 + "\" ANAL_T1=\"" + ANAL_T1 + "\" ANAL_T2=\"" + ANAL_T2 + "\" ANAL_T3=\"" + ANAL_T3 + "\" ANAL_T4=\"" + ANAL_T4 + "\" ANAL_T5=\"" + ANAL_T5 + "\"  ANAL_T6=\"" + ANAL_T6 + "\" ANAL_T7=\"" + ANAL_T7 + "\" ANAL_T8=\"" + ANAL_T8 + "\" ANAL_T9=\"" + ANAL_T9 + "\"  tipoDeContabilidad=\"" + tipoDeContabilidad + "\" >");
                                }
                                else
                                {
                                    cad.Append("<PLZ:Transaccion NumCta=\"" + ACNT_CODE + "\" DesCta=\"" + DESCR_ACNT_CODE + "\" Concepto=\"" + DESCRIPTN + "\" Debe=\"" + debe + "\" Haber=\"" + haber + "\"  >");
                                }
                                
                                //comprobantes
                                if (FOLIO_FISCAL != null && !FOLIO_FISCAL.Equals(""))
                                {
                                    cad.Append("<PLZ:CompNal UUID_CFDI=\"" + FOLIO_FISCAL + "\" RFC=\"" + rfc + "\" MontoTotal=\"" + AMOUNT_FISCAL + "\" ></PLZ:CompNal>");
                                }
                                cad.Append("</PLZ:Transaccion>");
                            }//while
                            cad.Append("</PLZ:Poliza>");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
           
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            cad.Append("</PLZ:Polizas>");

            String aver = cad.ToString();

            try
            {
                doc.LoadXml(cad.ToString());
            }
            catch (Exception err)
            {
                Clipboard.SetText(cad.ToString());
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
           
                MessageBox.Show(err.Message);
                return;
            }
            guardarButton.Visible = true;
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
              
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            Item itm = (Item)periodosCombo.SelectedItem;
            String periodo = itm.Name;
            String anio = periodo.Substring(0, 4);
            String mes = periodo.Substring(5, 2);

            saveFileDialog1.Filter = "Zip File|*.zip";
            saveFileDialog1.Title = "Guarda el xml del catalogo de cuentas";
            saveFileDialog1.FileName = Properties.Settings.Default.rfcGlobal + anio + mes + "PL.zip";
            saveFileDialog1.ShowDialog();


            String path = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName) + (object)Path.DirectorySeparatorChar;
            doc.Save(path + Properties.Settings.Default.rfcGlobal + anio + mes + "PL.xml");
            using (ZipArchive archive = ZipFile.Open(path + Properties.Settings.Default.rfcGlobal + anio + mes + "PL.zip", ZipArchiveMode.Create))
            {

                archive.CreateEntryFromFile(path + Properties.Settings.Default.rfcGlobal + anio + mes + "PL.xml", Properties.Settings.Default.rfcGlobal + anio + mes + "PL.xml");
            }
            File.Delete(path + Properties.Settings.Default.rfcGlobal + anio + mes + "PL.xml");
            System.Windows.Forms.MessageBox.Show("Se ha generado el archivo de polizas del periodo: " + path + Properties.Settings.Default.rfcGlobal + anio + mes + "PL.zip  recuerde que unicamente las cuentas que tienen movimientos estan en el XML.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            this.Close();
        }
    }
}
