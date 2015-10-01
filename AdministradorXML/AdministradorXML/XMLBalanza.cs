using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Data.SqlClient;
namespace AdministradorXML
{
    public partial class XMLBalanza : Form
    {
        public StringBuilder cad { get; set; }
        public XmlDocument doc { get; set; }
        public List<String> todosLosPeriodos { get; set; }
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
        public XMLBalanza()
        {
            InitializeComponent();
        }

        private void XMLBalanza_Load(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT PERIOD FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by PERIOD asc";
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
                        todosLosPeriodos = new List<String>();
                        while (reader.Read())
                        {
                            String periodo = Convert.ToString(reader.GetInt32(0));
                            periodosCombo.Items.Add(new Item(periodo, empiezo));
                            empiezo++;
                            todosLosPeriodos.Add(periodo);
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

            tipoCombo.Items.Add(new Item("Normal", 1,"N"));
            tipoCombo.Items.Add(new Item("Complementario", 2,"C"));
            tipoCombo.SelectedIndex = 0;
     
        }
        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes)
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
                    newTreeNode.Text = " " + xmlNode.Name;
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

        private void tipoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)tipoCombo.SelectedItem;
            if( itm.Value==1)
            {
                label3.Visible = false;
                dateTime1.Visible = false;
            }
            else
            {
                 label3.Visible = true;
                 dateTime1.Visible = true;
            }
        }

        private void generarButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Item itm = (Item)periodosCombo.SelectedItem;
            String periodo = itm.Name;
            StringBuilder periodosAnteriores = new StringBuilder("");

             bool first = true;
            foreach(String p in todosLosPeriodos)
            {
                if(p.Equals(periodo))
                {
                    break;
                }
                if(first)
                {
                    first = false;
                    periodosAnteriores.Append("" + p + "");
                }
                else
                {
                    periodosAnteriores.Append("," + p + "");
                }
             }
            String hastaAlPeriodoActual = periodosAnteriores.ToString()+","+periodo+"";
         

            String anio = periodo.Substring(0, 4);
            String mes = periodo.Substring(5, 2);
            Item itm2 = (Item)tipoCombo.SelectedItem;
            String tipo = itm2.Extra;
            String fecha = "";
            if(tipo.Equals("C"))
            {
                fecha = dateTime1.Value.Date.ToShortDateString().Substring(0, 10);
            }
            doc = new XmlDocument();
            cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<BCE:Balanza xsi:schemaLocation=\"www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion/BalanzaComprobacion_1_1.xsd\"" +
               "  Version=\"1.1\" RFC=\"" + Properties.Settings.Default.rfcGlobal + "\"" +
                    " Mes=\"" + mes + "\" Anio=\"" + anio + "\" TipoEnvio=\""+tipo+"\" ");
            if (tipo.Equals("C"))
            {
                cad.Append(" FechaModBal = \""+fecha+"\" ");
            }
            cad.Append(" xmlns:BCE=\"http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT ACNT_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ACNT] order by ACNT_CODE asc";
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
                                String queryFISCAL = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '"+ACNT_CODE+"' AND PERIOD in (" + periodosAnteriores.ToString() + ")";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        if (readerFISCAL.Read())
                                        {
                                            int cuantos = readerFISCAL.GetInt32(0);
                                            if(cuantos>0)
                                            {
                                                saldoInicial = Convert.ToString( Math.Round( Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(1))),2));
                                            }
                                        }
                                    }
                                }

                                //saldo final
                                String queryFISCAL1 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND PERIOD in (" + hastaAlPeriodoActual + ")";
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
                                String queryFISCAL2 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '"+ACNT_CODE+"' AND D_C='D' AND PERIOD in ('"+periodo+"')";
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
                                String queryFISCAL3 = " SELECT COUNT(*) as cuantos, SUM(AMOUNT) as suma FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '" + ACNT_CODE + "' AND D_C='C' AND PERIOD in ('" + periodo + "')";
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
                                if(saldoInicial.Equals("0") && saldoFinal.Equals("0")    &&saldoDebe.Equals("0")     &&saldoHaber.Equals("0"))
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
         
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            Item itm = (Item)periodosCombo.SelectedItem;
            String periodo = itm.Name;
            String anio = periodo.Substring(0, 4);
            String mes = periodo.Substring(5, 2);
        
            saveFileDialog1.Filter = "Zip File|*.zip";
            saveFileDialog1.Title = "Guarda el xml del catalogo de cuentas";
            saveFileDialog1.FileName = Properties.Settings.Default.rfcGlobal + anio + mes + "BN.zip";
            saveFileDialog1.ShowDialog();


            String path = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName) + (object)Path.DirectorySeparatorChar;
            doc.Save(path + Properties.Settings.Default.rfcGlobal + anio + mes + "BN.xml");
            using (ZipArchive archive = ZipFile.Open(path + Properties.Settings.Default.rfcGlobal + anio + mes + "BN.zip", ZipArchiveMode.Create))
            {

                archive.CreateEntryFromFile(path + Properties.Settings.Default.rfcGlobal + anio + mes + "BN.xml", Properties.Settings.Default.rfcGlobal + anio + mes + "BN.xml");
            }
            File.Delete(path + Properties.Settings.Default.rfcGlobal + anio + mes + "BN.xml");
            System.Windows.Forms.MessageBox.Show("Se ha generado el archivo de balanza: " + path + Properties.Settings.Default.rfcGlobal + anio + mes + "BN.zip  recuerde que unicamente las cuentas que tienen movimientos estan en el XML.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            this.Close();
        }
    }
}
