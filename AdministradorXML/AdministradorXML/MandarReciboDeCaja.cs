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
using PdfFileWriter;
using System.Net.Mail;
namespace AdministradorXML
{
    public partial class MandarReciboDeCaja : Form
    {
        public MandarReciboDeCaja()
        {
            InitializeComponent();
        }
        private void llenaDatos()
        {
            String ANL_CODE = iglesiaText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT TOP 1 nombre, correo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias] WHERE ANL_CODE = '"+ANL_CODE+"'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                String nombre = reader.GetString(0).Trim();
                                String correo = reader.GetString(1).Trim();
                                nombreLabel.Text = nombre;
                                correoLabel.Text = correo;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            var source = new AutoCompleteStringCollection();

            List<String> diarios = new List<String>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    if(Login.unidadDeNegocioGlobal.Equals("FOP"))
                    {
                        queryXML = "SELECT DISTINCT JRNAL_NO FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ANAL_T9 = '" + iglesiaText.Text + "'";
                    }
                    else
                    {
                        queryXML = "SELECT DISTINCT JRNAL_NO FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ANAL_T5 = '" + iglesiaText.Text + "'";
                    }
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String JRNAL_NO = Convert.ToString(reader.GetInt32(0)).Trim();
                                diarios.Add(JRNAL_NO);
                            }
                        }//if reader
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source.AddRange(diarios.ToArray());
            diarioText.AutoCompleteCustomSource = source;
            diarioText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            diarioText.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }
        private void MandarReciboDeCaja_Load(object sender, EventArgs e)
        {
            if(Login.unidadDeNegocioGlobal.Equals("FOP"))
            {
                label1.Text = "P:";
            }
            var source = new AutoCompleteStringCollection();
           
            List<String> iglesias = new List<String>();
            
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT ANL_CODE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias]";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ANL_CODE = reader.GetString(0).Trim();
                                iglesias.Add(ANL_CODE);
                            }
                        }//if reader
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source.AddRange(iglesias.ToArray());
            iglesiaText.AutoCompleteCustomSource = source;
            iglesiaText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            iglesiaText.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void iglesiaText_TextChanged(object sender, EventArgs e)
        {
            llenaDatos();
        }
        public PdfFont ArialNormal { get; set; }

        public PdfFont ArialBold { get; set; }
        public PdfFont ArialItalic { get; set; }
        public PdfFont ArialBoldItalic { get; set; }
        public PdfFont TimesNormal { get; set; }
        public PdfFont Comic { get; set; }
        public PdfDocument Document { get; set; }
        public PdfTilingPattern WaterMark { get; set; }
        private void DefineTilingPatternResource()
        {
            WaterMark = new PdfTilingPattern(Document);
            String Mark = "PdfFileWriter";
            Double FontSize = 18.0;
            Double TextWidth = ArialBold.TextWidth(FontSize, Mark);
            Double TextHeight = ArialBold.LineSpacing(FontSize);
            Double BaseLine = ArialBold.DescentPlusLeading(FontSize);
            Double BoxWidth = TextWidth + 2 * TextHeight;
            Double BoxHeight = 4 * TextHeight;
            WaterMark.SetTileBox(BoxWidth, BoxHeight);
            WaterMark.SaveGraphicsState();
            WaterMark.SetColorNonStroking(Color.FromArgb(230, 244, 255));
            WaterMark.DrawRectangle(0, 0, BoxWidth, BoxHeight, PaintOp.Fill);
            WaterMark.SetColorNonStroking(Color.White);
            WaterMark.DrawText(ArialBold, FontSize, BoxWidth / 2, BaseLine, TextJustify.Center, Mark);
            BaseLine += BoxHeight / 2;
            WaterMark.DrawText(ArialBold, FontSize, 0.0, BaseLine, TextJustify.Center, Mark);
            WaterMark.DrawText(ArialBold, FontSize, BoxWidth, BaseLine, TextJustify.Center, Mark);
            WaterMark.RestoreGraphicsState();
            return;
        }
        private void DefineFontResources()
        {
            String FontName1 = "Arial";
            String FontName2 = "Times New Roman";
            ArialNormal = new PdfFont(Document, FontName1, System.Drawing.FontStyle.Regular, true);
            ArialBold = new PdfFont(Document, FontName1, System.Drawing.FontStyle.Bold, true);
            ArialItalic = new PdfFont(Document, FontName1, System.Drawing.FontStyle.Italic, true);
            ArialBoldItalic = new PdfFont(Document, FontName1, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, true);
            TimesNormal = new PdfFont(Document, FontName2, System.Drawing.FontStyle.Regular, true);
            Comic = new PdfFont(Document, "Comic Sans MS", System.Drawing.FontStyle.Bold, true);
               return;
        }
        private void mandarButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
         
              String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
              try
              {

                  using (SqlConnection connection = new SqlConnection(connString))
                  {
                      connection.Open();
                      String queryXML = "";
                      if(Login.unidadDeNegocioGlobal.Equals("FOP"))
                      {
                          queryXML = "SELECT b.ADDR_LINE_1,b.ADDR_LINE_2,b.ADDR_LINE_3,b.ADDR_LINE_4,b.ADDR_LINE_5  , a.AMOUNT, a.TRANS_DATETIME,a.DESCRIPTN,a.JRNAL_SRCE, a.ANAL_T0,a.ACCNT_CODE,c.DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] a INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ADDR] b on a.ANAL_T2 = b.ADDR_CODE INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[FOP_ACNT] c on c.ACNT_CODE = a.ACCNT_CODE WHERE a.JRNAL_NO = " + diarioText.Text + " AND a.ANAL_T9 = '" + iglesiaText.Text + "'";
                      }
                      else
                      {
                          queryXML = "SELECT b.ADDR_LINE_1,b.ADDR_LINE_2,b.ADDR_LINE_3,b.ADDR_LINE_4,b.ADDR_LINE_5  , a.AMOUNT, a.TRANS_DATETIME,a.DESCRIPTN,a.JRNAL_SRCE, a.ANAL_T0,a.ACCNT_CODE,c.DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] a INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ADDR] b on a.ANAL_T2 = b.ADDR_CODE INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] c on c.ACNT_CODE = a.ACCNT_CODE WHERE a.JRNAL_NO = " + diarioText.Text + " AND a.ANAL_T5 = '" + iglesiaText.Text + "'";
                      }
                      using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                      {
                          String saveANAL="";
                          String saveFecha = "";    
                          
                          SqlDataReader reader = cmdCheck.ExecuteReader();
                          if (reader.HasRows)
                          {
                              bool first = true;
                              String FileName = "C:" + (object)Path.DirectorySeparatorChar + "recibos" + (object)Path.DirectorySeparatorChar + iglesiaText.Text + "_" + diarioText.Text + ".pdf";
                              string path = "C:" + (object)Path.DirectorySeparatorChar + "recibos";
                              if (!Directory.Exists(path))
                                  Directory.CreateDirectory(path);
                              if (File.Exists(FileName))
                              {
                                  try
                                  {
                                      File.Delete(FileName);
                                  }
                                  catch (IOException ex2)
                                  {
                                      System.Windows.Forms.MessageBox.Show(ex2.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                  }
                              }
                             
                              Document = new PdfDocument(PaperType.Letter, false, UnitOfMeasure.Inch, FileName);
                              DefineFontResources();
                              DefineTilingPatternResource();
                              PdfPage Page = new PdfPage(Document);
                              PdfContents Contents = new PdfContents(Page);
                              Contents.SaveGraphicsState();
                             
                              String fileImage = "C:" + (object)Path.DirectorySeparatorChar + "recibos" + (object)Path.DirectorySeparatorChar + "logo.jpg";
                              PdfImageControl ImageControl = new PdfImageControl();
                              ImageControl.Resolution = 300.0;
                              PdfImage Image = new PdfImage(Document, fileImage, ImageControl); //new PdfImage(Document, fileImage, 72.0, 50);
                              Contents.SaveGraphicsState();
                              double top = 9.9;
                              double left = 0.4;
                              int ancho = 1;
                              int largo = 1;
                             Contents.DrawImage(Image, left, top, ancho, largo);
                             Contents.DrawImage(Image, left, top-6.2, ancho, largo);
                            
                              Contents.RestoreGraphicsState();
                              Contents.SaveGraphicsState();
                            

                            






                              const Double Width = 8.15;
                              const Double Height = 10.65;
                              PdfFileWriter.TextBox Box = new PdfFileWriter.TextBox(Width, 0.0);
                              PdfFileWriter.TextBox Datos = new PdfFileWriter.TextBox(Width, 0.0);
                              PdfFileWriter.TextBox derecha = new PdfFileWriter.TextBox(Width, 0.0);
                              PdfFileWriter.TextBox fechaBox = new PdfFileWriter.TextBox(Width, 0.0);
                              PdfFileWriter.TextBox graciasBox = new PdfFileWriter.TextBox(Width, 0.0);
                              PdfFileWriter.TextBox copiaBox = new PdfFileWriter.TextBox(Width, 0.0);
                             
                              //double dif = 2.5;
                             // Contents.SaveGraphicsState();
                              //Contents.RestoreGraphicsState();
                              Contents.Translate(0.0, 0.0);
                              PdfTable Table = new PdfTable(Page, Contents, ArialNormal, 9.0);
                              PdfTable TableCopy = new PdfTable(Page, Contents, ArialNormal, 9.0);
                             
                             
                              Table.TableArea = new PdfRectangle(0.5, 1,8.0,7.7);
                              TableCopy.TableArea = new PdfRectangle(0.5, 1, 8.0, 1.75);


                              
                              Double MarginHor = 0.04;
                              double aver = ArialNormal.TextWidth(9.0, "9999.99") + 2.0 * MarginHor;
                              double aver2 = ArialNormal.TextWidth(9.0, "Qty") + 2.0 * MarginHor;
                              aver += 0.2;
                              Table.SetColumnWidth(new Double[] { aver2,aver , aver, aver2 });
                              Table.Borders.SetAllBorders(0.0);

                              Table.Header[0].Style = Table.HeaderStyle;
                              Table.Header[0].Style.Alignment = ContentAlignment.MiddleCenter;
                              Table.Header[1].Style = Table.HeaderStyle;
                              Table.Header[1].Style.Alignment = ContentAlignment.MiddleCenter;
                              Table.Header[2].Style = Table.HeaderStyle;
                              Table.Header[2].Style.Alignment = ContentAlignment.MiddleCenter;
                              Table.Header[3].Style = Table.HeaderStyle;
                              Table.Header[3].Style.Alignment = ContentAlignment.MiddleCenter;

                              Table.Header[0].Value = "Cuenta";
                              Table.Header[1].Value = "Descripción";
                              Table.Header[2].Value = "Concepto";
                              Table.Header[3].Value = "Total";


                              TableCopy.SetColumnWidth(new Double[] { aver2, aver, aver, aver2 });
                              TableCopy.Borders.SetAllBorders(0.0);

                              TableCopy.Header[0].Style = Table.HeaderStyle;
                              TableCopy.Header[0].Style.Alignment = ContentAlignment.MiddleCenter;
                              TableCopy.Header[1].Style = Table.HeaderStyle;
                              TableCopy.Header[1].Style.Alignment = ContentAlignment.MiddleCenter;
                              TableCopy.Header[2].Style = Table.HeaderStyle;
                              TableCopy.Header[2].Style.Alignment = ContentAlignment.MiddleCenter;
                              TableCopy.Header[3].Style = Table.HeaderStyle;
                              TableCopy.Header[3].Style.Alignment = ContentAlignment.MiddleCenter;

                              TableCopy.Header[0].Value = "Cuenta";
                              TableCopy.Header[1].Value = "Descripción";
                              TableCopy.Header[2].Value = "Concepto";
                              TableCopy.Header[3].Value = "Total";

                              StringBuilder cad = new StringBuilder("");
                              StringBuilder primeros = new StringBuilder("");
                              StringBuilder letrasGrandes = new StringBuilder("");
                              StringBuilder fechaCad = new StringBuilder("");
                              
                              StringBuilder tabla = new StringBuilder("");
                              double total=0;
                              int contadorTabla = 0;
                              while (reader.Read())
                              {
                                  String ADDR_LINE_1 = reader.GetString(0).Trim();
                                  String ADDR_LINE_2 = reader.GetString(1).Trim();
                                  String ADDR_LINE_3 = reader.GetString(2).Trim();
                                  String ADDR_LINE_4 = reader.GetString(3).Trim();
                                  String ADDR_LINE_5 = reader.GetString(4).Trim();
                                  String amount = Convert.ToString(reader.GetDecimal(5));
                                  String fecha = Convert.ToString(reader.GetDateTime(6)).Substring(0, 10);
                                 
                                  String DESCR = reader.GetString(7).Trim();
                                  if(DESCR.Length>32)
                                  {
                                      DESCR = DESCR.Substring(0, 32);
                                  }
                                  String JRNAL_SRCE = reader.GetString(8).Trim();
                                  String ANAL_T0 = reader.GetString(9).Trim();
                                  saveANAL=ANAL_T0;
                                  String ACNT_CODE = reader.GetString(10).Trim();
                                  String descrCuenta = reader.GetString(11).Trim();
                                  if (descrCuenta.Length > 36)
                                  {
                                      descrCuenta = descrCuenta.Substring(0, 36);
                                  }
                                  total+=Convert.ToDouble(amount);
                                  if(first)
                                  {
                                      first = false;
                                      primeros.Append( ADDR_LINE_1 + "\n" +
                                         ADDR_LINE_2 + "\n" +
                                      ADDR_LINE_3 + "\n" +
                                      ADDR_LINE_4 + "\n" +
                                      ADDR_LINE_5 + "\n\n");
                                      fechaCad.Append(" Fecha de recepción:\n" + fecha);

                                      letrasGrandes.Append("      Recibido de " + DESCR + "\n");
                                      saveFecha = fecha;
                                  }
                                  Table.Cell[0].Value = ACNT_CODE;
                                  Table.Cell[1].Value = descrCuenta;
                                  Table.Cell[2].Value = DESCR;
                                   Table.Cell[3].Value = "$"+String.Format("{0:n}", Convert.ToDouble(amount));
                                  Table.DrawRow();
                                  TableCopy.Cell[0].Value = ACNT_CODE;
                                  TableCopy.Cell[1].Value = descrCuenta;
                                  TableCopy.Cell[2].Value = DESCR;
                                  TableCopy.Cell[3].Value = "$" + String.Format("{0:n}", Convert.ToDouble(amount));
                                  TableCopy.DrawRow();


                                  contadorTabla++;

                                //  tabla.Append("\n"+DESCR+"         "+ACNT_CODE+"        "+JRNAL_SRCE+"        "+  String.Format("{0:n}", Convert.ToDouble(amount)));
                              }//while
                              Table.Close();
                              TableCopy.Close();
                              Contents.SaveGraphicsState();
                              Contents.RestoreGraphicsState();
                                
                              letrasGrandes.Append("      ***** " + Conversiones.NumeroALetras(total.ToString()) + " *****\n");

                              cad.Append(" Número de recibo original: " + saveANAL + "\n Diario: " + diarioText.Text + "\n" +
                                      " Monto recibido\n ***** "+String.Format("{0:n}", Convert.ToDouble(total))+" *****\n\n");
                              //cad.Append();
                              try{
                                 
                                  Box.AddText(ArialNormal, 14.0,
                                    primeros.ToString()  );
                                  Datos.AddText(ArialNormal, 14.0,
                                letrasGrandes.ToString() );
                                  copiaBox.AddText(ArialBold, 50.0,
                              "     COPY");
                                  graciasBox.AddText(ArialItalic, 30.0,
                              "                           Gracias\n");
                                  fechaBox.AddText(ArialNormal, 10.0,
                                 fechaCad.ToString());
                               derecha.AddText(ArialNormal, 12.0,
                                 cad.ToString());
                                  Double PosY = 8.5;
                                  Double PosYDatos = 9.0;
                                  Double auxY = Height;
                                  Double auxFecha = Height-0.5;
                                  double dif = 6.0;
                                  Double auxYCopia = Height-dif;
                                  Double auxFechaCopia = Height - 0.5-dif;
                                  Double PosYCopia = 8.5-dif;
                                  Double copiaCopia = 8.5 - dif;
                                 
                                  Double PosYDatosCopia = 9.0-dif;
                                 
                                 
                                  Contents.Translate(0.0, 10.0);

                                 // Contents.Translate(0.0, -9.9);//-9.9
                              
                                 Contents.Translate(0.0, -9.9);


                                  Contents.DrawText(0.0, ref auxY, 9.0, 0, 0.0, 0.0, TextBoxJustify.Center, Box);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();
                                  //la copia
                                  Contents.DrawText(0.0, ref auxYCopia, 1.0, 0, 0.0, 0.0, TextBoxJustify.Center, Box);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();
                                 // Contents.Translate(0.0, -9.9);
                                  Contents.DrawText(0, ref PosYDatos, 8.5, 0, 0.00, 0.00, TextBoxJustify.Left, Datos);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref PosYDatosCopia, 1.0, 0, 0.00, 0.00, TextBoxJustify.Left, Datos);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                 // Contents.Translate(0.0, -9.9);
                                  Contents.DrawText(0, ref PosY, 7.0, 0, 0.00, 0.00, TextBoxJustify.Right, derecha);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref PosYDatos, 7.0, 0, 0.00, 0.00, TextBoxJustify.Left, graciasBox);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref PosYCopia, 1.0, 0, 0.00, 0.00, TextBoxJustify.Right, derecha);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref PosYDatosCopia, 1.0, 0, 0.00, 0.00, TextBoxJustify.Left, graciasBox);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref copiaCopia, 1.0, 0, 0.00, 0.00, TextBoxJustify.Left, copiaBox);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  //Contents.Translate(0.0, -9.9);
                                  Contents.DrawText(0, ref auxFecha, 9.5, 0, 0.00, 0.00, TextBoxJustify.Right, fechaBox);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();

                                  Contents.DrawText(0, ref auxFechaCopia, 1.0, 0, 0.00, 0.00, TextBoxJustify.Right, fechaBox);
                                  Contents.RestoreGraphicsState();
                                  Contents.SaveGraphicsState();
                                 
                                 
                                Document.CreateFile();

                                try
                                {
                                    MailMessage mail2 = new MailMessage();
                                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                    mail2.From = new MailAddress(Properties.Settings.Default.correoEmisor);
                                    mail2.To.Add(correoLabel.Text);

                                    mail2.CC.Add(Properties.Settings.Default.correoReceptor);
                                    DateTime now = DateTime.Now;
                                    int year = now.Year;
                                    int month = now.Month;
                                    int day = now.Day;
                                    String mes = month.ToString();
                                    if (month < 10)
                                    {
                                        mes = "0" + month;
                                    }
                                    String dia = day.ToString();
                                    if (day < 10)
                                    {
                                        dia = "0" + day;
                                    }
                                    if(Login.unidadDeNegocioGlobal.Equals("FOP"))
                                    {
                                        mail2.Subject = "Recibo del dia: " + saveFecha + " del diario "+diarioText.Text+" de FORPOUMN " + iglesiaText.Text ;
                                        mail2.Body = "Hola " + nombreLabel.Text + ", por este medio le envio el recibo de FOPROUMN correspondiente al diario "+diarioText.Text+". Dios lo bendiga. ";
                                    }
                                    else
                                    {
                                        mail2.Subject = "Recibo de caja del dia: " + saveFecha + " de la iglesia " + iglesiaText.Text; 
                                        mail2.Body = "Hola " + nombreLabel.Text + ", por este medio le envio el recibo de caja de la iglesia " + iglesiaText.Text;
                                    }
                                    Attachment pdf = new Attachment(FileName);
                                    mail2.Attachments.Add(pdf);
                                    SmtpServer.Port = 587;
                                    SmtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.correoEmisor, Properties.Settings.Default.passEmisor);
                                    SmtpServer.EnableSsl = true;
                                    SmtpServer.Send(mail2);
                                    this.Cursor = System.Windows.Forms.Cursors.Arrow;
         
                                    System.Windows.Forms.MessageBox.Show("Ya se mando el correo", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         

                                }
                                catch (Exception ex3)
                                {
                                    System.Windows.Forms.MessageBox.Show(ex3.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }



                                   }//try
                              catch(Exception ex1)
                              {
                                          System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        
                              }
                          }
                      }
                  }
              }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
              this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        
    }
}
