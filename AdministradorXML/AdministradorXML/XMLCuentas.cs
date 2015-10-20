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
using System.IO.Compression;
using System.Xml;
namespace AdministradorXML
{
    public partial class XMLCuentas : Form
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
        public XMLCuentas()
        {
            InitializeComponent();
        }

        private void XMLCuentas_Load(object sender, EventArgs e)
        {
            anioBox.Items.Add(new Item("2015", 2015, "2015"));
            anioBox.Items.Add(new Item("2016", 2016, "2016"));
            anioBox.Items.Add(new Item("2017", 2017, "2017"));
            anioBox.Items.Add(new Item("2018", 2018, "2018"));
            anioBox.Items.Add(new Item("2019", 2019, "2019"));
            anioBox.Items.Add(new Item("2020", 2020, "2020"));
            anioBox.Items.Add(new Item("2021", 2021, "2021"));
            anioBox.Items.Add(new Item("2022", 2022, "2022"));
            anioBox.Items.Add(new Item("2023", 2023, "2023"));
            anioBox.SelectedIndex = 0;
     
            mesBox.Items.Add(new Item("Enero", 1, "01"));
            mesBox.Items.Add(new Item("Febrero", 2, "02"));
            mesBox.Items.Add(new Item("Marzo", 3, "03"));
            mesBox.Items.Add(new Item("Abril", 4, "04"));
            mesBox.Items.Add(new Item("Mayo", 5, "05"));
            mesBox.Items.Add(new Item("Junio", 6, "06"));
            mesBox.Items.Add(new Item("Julio", 7, "07"));
            mesBox.Items.Add(new Item("Agosto", 8, "08"));
            mesBox.Items.Add(new Item("Septiembre", 9, "09"));
            mesBox.Items.Add(new Item("Octubre", 10, "10"));
            mesBox.Items.Add(new Item("Noviembre", 11, "11"));
            mesBox.Items.Add(new Item("Diciembre", 12, "12"));
            mesBox.SelectedIndex = 0;
     

         
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

        private void guardarButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
           
            Item itm = (Item)anioBox.SelectedItem;
            Item itm2 = (Item)mesBox.SelectedItem;

            String mes = itm2.Extra;
            String anio = itm.Extra;
          
            doc = new XmlDocument();
            cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"+
                "<catalogocuentas:Catalogo xsi:schemaLocation=\"www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas/CatalogoCuentas_1_1.xsd\"" +
               "  Version=\"1.1\" RFC=\"" + Properties.Settings.Default.rfcGlobal + "\""+
                    " Mes=\""+mes+"\" Anio=\""+anio+"\" xmlns:catalogocuentas=\"www.sat.gob.mx/esquemas/ContabilidadE/1_1/CatalogoCuentas\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                 String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
          int nivel = 1;
             try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT c.codigoAgrupador, a.ACNT_CODE , a.DESCR, b.ANL_CODE as sub, bb.ANL_CODE as natur FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_ACNT] a INNER JOIN  [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT_ANL_CAT] b on b.ACNT_CODE = a.ACNT_CODE INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[codigoAgrupadorCuentaSunplus] c on c.ACNT_CODE COLLATE SQL_Latin1_General_CP1_CI_AS  = a.ACNT_CODE INNER JOIN  [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_ACNT_ANL_CAT] bb on bb.ACNT_CODE = a.ACNT_CODE WHERE b.ANL_CAT_ID = 12 AND bb.ANL_CAT_ID = 14";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String codigoAgrupador = reader.GetString(0).Trim();
                                String ACNT_CODE = reader.GetString(1).Trim();
                                //hardcode
                                if(ACNT_CODE.Length<6)
                                {
                                    nivel = 1;
                                }
                                else
                                {
                                    nivel = 2;
                                }
                                String DESCR = reader.GetString(2).Trim();
                                String sub = reader.GetString(3).Trim();
                                String natur = reader.GetString(4).Trim();
                                cad.Append("<catalogocuentas:Ctas CodAgrup=\"" + codigoAgrupador + "\" NumCta=\"" + ACNT_CODE + "\" Desc=\"" + DESCR + "\" ");
                                if(!sub.Equals(""))
                                {
                                    cad.Append("SubCtaDe=\""+sub+"\" ");
                                }
                                cad.Append("Nivel=\"" + nivel + "\" Natur=\""+natur+"\" />");
                            }
                        }
                    }
                }
             }
            catch(Exception ex)
             {
                 ex.ToString();
            }


             cad.Append("</catalogocuentas:Catalogo>");


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
            if(!desactivarPreview.Checked)
            {
                ConvertXmlNodeToTreeNode(doc, cuentasTree.Nodes);
            }
            //cuentasTree.Nodes[0].ExpandAll();
            guardarAhoraSiButton.Visible = true;
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
           
        }

        private void guardarAhoraSiButton_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Zip File|*.zip";
            saveFileDialog1.Title = "Guarda el xml del catalogo de cuentas";
            Item itm = (Item)anioBox.SelectedItem;
            Item itm2 = (Item)mesBox.SelectedItem;

            String mes = itm2.Extra;
            String anio = itm.Extra;

            saveFileDialog1.FileName = Properties.Settings.Default.rfcGlobal + anio + mes + "CT.zip";
            saveFileDialog1.ShowDialog();


            String path = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName) + (object)Path.DirectorySeparatorChar;
            doc.Save(path + Properties.Settings.Default.rfcGlobal + anio + mes + "CT.xml");
            using (ZipArchive archive = ZipFile.Open(path + Properties.Settings.Default.rfcGlobal + anio + mes + "CT.zip", ZipArchiveMode.Create))
            {

                archive.CreateEntryFromFile(path + Properties.Settings.Default.rfcGlobal + anio + mes + "CT.xml", Properties.Settings.Default.rfcGlobal + anio + mes + "CT.xml");
            }
            File.Delete(path + Properties.Settings.Default.rfcGlobal + anio + mes + "CT.xml");
            System.Windows.Forms.MessageBox.Show("Se ha generado el archivo: " + path + Properties.Settings.Default.rfcGlobal + anio + mes + "CT.zip  recuerde que unicamente las cuentas a las que le puso el codigo agrupador del SAT estan en el XML.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            this.Close();
            


         
           
        }
    }
}
