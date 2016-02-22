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
    public partial class XMLFacturas : Form
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
        public XMLFacturas()
        {
            InitializeComponent();
        }

        private void XMLFacturas_Load(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL' order by SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) asc";
            //String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";

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
                        System.Windows.Forms.MessageBox.Show("No existen Periodos, primero descarga xml del buzon tributario.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 1;
        }

        private void generarButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Item itm = (Item)periodosCombo.SelectedItem;
            String periodo = itm.Name;
            String year = periodo.Substring(0, 4);
            String month = periodo.Substring(5, 2);
            doc = new XmlDocument();
         
             cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<FAC:Facturas xmlns:FAC=\"http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion\" Version=\"1.1\" RFC=\"" + Properties.Settings.Default.rfcGlobal + "\"" +
                   " Mes=\"" + month + "\" Anio=\"" + year + "\"  >");

            //obtener todas las facturas del periodo que no esten canceladas !
            //tipo 1 y 2
            //cada factura tendra:
            //UUID, amount, tipo,, rfc, razon social

              String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
              try
              {
                  using (SqlConnection connection = new SqlConnection(connString))
                  {
                      connection.Open();
                      String queryXML = "SELECT rfc, razonSocial, total, folioFiscal, fechaExpedicion,STATUS,ISNULL(ruta,'') as ruta FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '"+periodo+"'";
                      using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                      {
                          SqlDataReader reader = cmdCheck.ExecuteReader();
                          if (reader.HasRows)
                          {
                              while (reader.Read())
                              {
                                  String rfc = reader.GetString(0).Trim();
                                  rfc = rfc.Replace(",", "");
                                  rfc = rfc.Replace("&", "&amp;");
                                  rfc = rfc.Replace("\"", "");
                              
                                  String razonSocial = reader.GetString(1).Trim();
                                  razonSocial = razonSocial.Replace(",", "");
                                  razonSocial = razonSocial.Replace("&", "&amp;");
                                  razonSocial = razonSocial.Replace("\"", "");
                              
                                  String total = Math.Round(Convert.ToDouble(reader.GetDecimal(2)), 2).ToString().Trim();
                                  String folioFiscal = reader.GetString(3).Trim();
                                  String fecha = reader.GetDateTime(4).ToString().Substring(0, 10);
                                  String STATUS = reader.GetString(5).Trim();
                                  String ruta = reader.GetString(6).Trim();

                                  int donativos = 0;
                                  
                                  if(!ruta.Equals(""))
                                  {
                                      XmlDocument doc1 = new XmlDocument();
                                      String rutaArchivo = ruta + "\\" + folioFiscal + ".xml";
                                      try
                                      {
                                          doc1.Load(rutaArchivo);
                                      }
                                      catch (Exception err)
                                      {
                                          Clipboard.SetText(rutaArchivo);
                                          MessageBox.Show(err.Message);
                                          return;
                                      }
                                      XmlNodeList titles = doc1.GetElementsByTagName("donat:Donatarias");
                                      if (titles.Count > 0)
                                      {
                                          donativos = 1;
                                          /* XmlNode obj = titles.Item(0);
                                           String noCertificadoSAT = "";
                                           bool isNoCertificado = obj.Attributes["noCertificadoSAT"] != null;
                                           if (isNoCertificado)
                                           {
                                               noCertificadoSAT = obj.Attributes["noCertificadoSAT"].InnerText;
                                           }*/
                                      }
                                  }


                                  
                                  



                                  cad.Append("<FAC:Factura STATUS=\"" + STATUS + "\" donativos=\"" + donativos + "\"  rfc=\"" + rfc + "\" razonSocial=\"" + razonSocial + "\" total=\"" + total + "\" folioFiscal=\"" + folioFiscal + "\" fecha=\"" + fecha + "\" />");
                              }
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  Clipboard.SetText(cad.ToString());
                  ex.ToString();
              }
             cad.Append("</FAC:Facturas>");
             try
             {
                 doc.LoadXml(cad.ToString());
             }
             catch (Exception err)
             {
                 Clipboard.SetText(cad.ToString());
                 MessageBox.Show(err.Message);
                 return;
             }

             saveFileDialog1.Filter = "Zip File|*.zip";
             saveFileDialog1.Title = "Guarda el xml del catalogo de cuentas";
             saveFileDialog1.FileName = Properties.Settings.Default.rfcGlobal + year + month + "FC.zip";
             saveFileDialog1.ShowDialog();


             String path = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName) + (object)Path.DirectorySeparatorChar;
             doc.Save(path + Properties.Settings.Default.rfcGlobal + year + month + "FC.xml");
             using (ZipArchive archive = ZipFile.Open(path + Properties.Settings.Default.rfcGlobal + year + month + "FC.zip", ZipArchiveMode.Create))
             {

                 archive.CreateEntryFromFile(path + Properties.Settings.Default.rfcGlobal + year + month + "FC.xml", Properties.Settings.Default.rfcGlobal + year + month + "FC.xml");
             }
             File.Delete(path + Properties.Settings.Default.rfcGlobal + year + month + "FC.xml");
             System.Windows.Forms.MessageBox.Show("Se ha generado el archivo de balanza: " + path + Properties.Settings.Default.rfcGlobal + year + month + "FC.zip  recuerde que unicamente las cuentas que tienen movimientos estan en el XML.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
             this.Close();


         

        }
    }
}
