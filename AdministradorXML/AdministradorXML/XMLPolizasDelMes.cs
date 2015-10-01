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
    }
}
