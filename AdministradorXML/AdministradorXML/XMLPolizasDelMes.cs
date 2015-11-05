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
namespace AdministradorXML
{
    public partial class XMLPolizasDelMes : Form
    {

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

            tipoSolicitudCombo.Items.Add(new Item("Acto de Fiscalización", 0,"AF"));
            tipoSolicitudCombo.Items.Add(new Item("Fiscalización Compulsa", 1, "FC"));
            tipoSolicitudCombo.Items.Add(new Item("Devolución", 2, "DE"));
            tipoSolicitudCombo.Items.Add(new Item("Compensación", 3, "CO"));
            tipoSolicitudCombo.SelectedIndex = 0;
     
                         
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
        }
        private void actualiza()
        {
            XmlDocument doc = new XmlDocument();
         /*   XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Foo"));
            el.SetAttribute("Bar", "some & value");
            el.AppendChild(doc.CreateElement("Nested")).InnerText = "data";
            Console.WriteLine(doc.OuterXml);
            */
            Item itm = (Item)periodosCombo.SelectedItem;
             Item itm2 = (Item)tipoSolicitudCombo.SelectedItem;
           
            String periodo = itm.Name;
            String tipoDeSolicitud = itm2.Extra;
            String NumOrdenT="",NumTramiteT="";
            String year = periodo.Substring(0, 4);
            String month = periodo.Substring(5, 2);
            String periodoParaQuery = year + "0" + month;
           
            StringBuilder cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?> <Polizas " +
            "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" +
            "xsi:noNamespaceSchemaLocation=\"http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo/PolizasPeriodo_1_1.xsd\"" +
            " version=\"1.1\"  RFC=\"" + Properties.Settings.Default.rfcGlobal + "\" Mes = \"" + month + "\" Anio=\"" + year + "\" TipoSolicitud=\"" + tipoDeSolicitud + "\" " +
            "");
            if(tipoDeSolicitud.Equals("AF") || tipoDeSolicitud.Equals("FC") )
            {
                NumOrdenT = NumOrden.Text.Trim();
                if(NumOrdenT.Length!=13)
                {
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
                if (NumOrdenT.Length != 10)
                {
                    System.Windows.Forms.MessageBox.Show("Debes de escribir el numero de tramite y este debe de tener 10 caracteres", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    cad.Append("NumTramite=\"" + NumTramiteT + "\"");
                }
            }

            cad.Append(">");




            cad.Append("</Polizas>");

            
            
            try
            {
                doc.LoadXml("<books><A property='a'><B>text</B><C>textg</C><D>99999</D></A></books>");
                //doc.Load("");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                return;
            }
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
            /*
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Item itm = (Item)periodosCombo.SelectedItem;
            String periodo = itm.Name;
           

           

            String anio = periodo.Substring(0, 4);
            String mes = periodo.Substring(5, 2);
            Item itm2 = (Item)tipoSolicitudCombo.SelectedItem;
            String tipo = itm2.Extra;
            String fecha = "";
            if (tipo.Equals("C"))
            {
                fecha = dateTime1.Value.Date.ToShortDateString().Substring(0, 10);
            }
            doc = new XmlDocument();
            cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<BCE:Balanza xsi:schemaLocation=\"www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion/BalanzaComprobacion_1_1.xsd\"" +
               "  Version=\"1.1\" RFC=\"" + Properties.Settings.Default.rfcGlobal + "\"" +
                    " Mes=\"" + mes + "\" Anio=\"" + anio + "\" TipoEnvio=\"" + tipo + "\" ");
            if (tipo.Equals("C"))
            {
                cad.Append(" FechaModBal = \"" + fecha + "\" ");
            }
            cad.Append(" xmlns:BCE=\"http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT ACNT_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] order by ACNT_CODE asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ACNT_CODE = reader.GetString(0).Trim();
                                String saldoInicial = "0";
                                String saldoFinal = "0";
                                String saldoDebe = "0";
                                String saldoHaber = "0";
                                //saldo inicial
                                String queryFISCAL = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND ALLOCATION != 'C' AND PERIOD in (" + periodosAnteriores.ToString() + ")";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        if (readerFISCAL.Read())
                                        {
                                            int cuantos = readerFISCAL.GetInt32(0);
                                            if (cuantos > 0)
                                            {
                                                saldoInicial = Convert.ToString(Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(1))), 2));
                                            }
                                        }
                                    }
                                }

                                //saldo final
                                String queryFISCAL1 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND ALLOCATION != 'C' AND PERIOD in (" + hastaAlPeriodoActual + ")";
                                using (SqlCommand cmdCheckFISCAL1 = new SqlCommand(queryFISCAL1, connection))
                                {
                                    SqlDataReader readerFISCAL1 = cmdCheckFISCAL1.ExecuteReader();
                                    if (readerFISCAL1.HasRows)
                                    {
                                        if (readerFISCAL1.Read())
                                        {
                                            int cuantos = readerFISCAL1.GetInt32(0);
                                            if (cuantos > 0)
                                            {
                                                saldoFinal = Convert.ToString(Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL1.GetDecimal(1))), 2));
                                            }
                                        }
                                    }
                                }

                                //saldo debe
                                String queryFISCAL2 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND D_C='D'  AND ALLOCATION != 'C' AND PERIOD in ('" + periodo + "')";
                                using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                {
                                    SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                    if (readerFISCAL2.HasRows)
                                    {
                                        if (readerFISCAL2.Read())
                                        {
                                            int cuantos = readerFISCAL2.GetInt32(0);
                                            if (cuantos > 0)
                                            {
                                                saldoDebe = Convert.ToString(Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL2.GetDecimal(1))), 2));
                                            }
                                        }
                                    }
                                }

                                //saldo haber
                                String queryFISCAL3 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND D_C='C'  AND ALLOCATION != 'C' AND PERIOD in ('" + periodo + "')";
                                using (SqlCommand cmdCheckFISCAL3 = new SqlCommand(queryFISCAL3, connection))
                                {
                                    SqlDataReader readerFISCAL3 = cmdCheckFISCAL3.ExecuteReader();
                                    if (readerFISCAL3.HasRows)
                                    {
                                        if (readerFISCAL3.Read())
                                        {
                                            int cuantos = readerFISCAL3.GetInt32(0);
                                            if (cuantos > 0)
                                            {
                                                saldoHaber = Convert.ToString(Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL3.GetDecimal(1))), 2));
                                            }
                                        }
                                    }
                                }
                                if (saldoInicial.Equals("0") && saldoFinal.Equals("0") && saldoDebe.Equals("0") && saldoHaber.Equals("0"))
                                {

                                }
                                else
                                {
                                    cad.Append("<BCE:Ctas NumCta=\"" + ACNT_CODE + "\" SaldoIni=\"" + saldoInicial + "\" Debe=\"" + saldoDebe + "\" Haber=\"" + saldoHaber + "\" SaldoFin=\"" + saldoFinal + "\" />");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            cad.Append("</BCE:Balanza>");


            try
            {
                doc.LoadXml(cad.ToString());

                //doc.Load("");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                return;
            }

            if (!noGenerarPreview.Checked)
            {
                ConvertXmlNodeToTreeNode(doc, previewTree.Nodes);
            }
            //previewTree.Nodes[0].ExpandAll();
            guardarButton.Visible = true;
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
              */
        }
    }
}
