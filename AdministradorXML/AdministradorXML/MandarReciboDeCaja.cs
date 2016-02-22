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
                    queryXML = "SELECT DISTINCT JRNAL_NO FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ANAL_T5 = '"+iglesiaText.Text+"'";
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
                      queryXML = "SELECT b.ADDR_LINE_1,b.ADDR_LINE_2,b.ADDR_LINE_3,b.ADDR_LINE_4,b.ADDR_LINE_5  , a.AMOUNT, a.TRANS_DATETIME,a.DESCRIPTN,a.JRNAL_SRCE, a.ANAL_T0,a.ACCNT_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_"+Properties.Settings.Default.sunLibro+"_SALFLDG] a INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].["+Login.unidadDeNegocioGlobal+"_ADDR] b on a.ANAL_T2 = b.ADDR_CODE WHERE a.JRNAL_NO = " + diarioText.Text + " AND a.ANAL_T5 = '" + iglesiaText.Text + "'";
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
                              Contents.Translate(0.1, 10.0);
                              PdfImage Image = null;// new PdfImage(Document, fileImage, 72.0, 50);
                              Contents.SaveGraphicsState();
                              int top = 0;
                              int left = 0;
                              int ancho = 1;
                              int largo = 1;
                            //  Contents.DrawImage(Image, left, top, ancho, largo);
                              Contents.RestoreGraphicsState();
                              Contents.SaveGraphicsState();
                              Contents.Translate(0.0, -9.9);


                            






                              const Double Width = 8.15;
                              const Double Height = 10.65;
                              const Double FontSize = 12.0;
                              PdfFileWriter.TextBox Box = new PdfFileWriter.TextBox(Width, 0.25);
                              StringBuilder cad = new StringBuilder("");
                              StringBuilder primeros = new StringBuilder("");
                              StringBuilder letrasGrandes = new StringBuilder("");
                             
                              StringBuilder tabla = new StringBuilder("");
                              double total=0;
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

                                  String JRNAL_SRCE = reader.GetString(8).Trim();
                                  String ANAL_T0 = reader.GetString(9).Trim();
                                  saveANAL=ANAL_T0;
                                  String ACNT_CODE = reader.GetString(10).Trim();
                                  total+=Convert.ToDouble(amount);
                                  if(first)
                                  {
                                      first = false;
                                      primeros.Append("                                          " + ADDR_LINE_1 + "\n" +
                                        "                                                    "+ADDR_LINE_2 + "\n" +
                                     "                                      "+ADDR_LINE_3 + "\n" +
                                     "                                                    "+ADDR_LINE_4 + "\n" +
                                     "                                          "+ADDR_LINE_5 + "\n\n");
                                      cad.Append(" Fecha de recepción: " + fecha + "\n");

                                      letrasGrandes.Append(" Recibido de " + DESCR + "\n");
                                      saveFecha = fecha;
                                  }
                                  tabla.Append("\n"+DESCR+"         "+ACNT_CODE+"        "+JRNAL_SRCE+"        "+  String.Format("{0:n}", Convert.ToDouble(amount)));
                              }//while
                              letrasGrandes.Append(" ***** " + Conversiones.NumeroALetras(total.ToString()) + " *****\n");

                              cad.Append(" Número de recibo original: " + saveANAL + "\n Diario: " + diarioText.Text + "\n" +
                                      " Monto recibido\n ***** "+String.Format("{0:n}", Convert.ToDouble(total))+" *****\n\n");
                              //cad.Append();
                              try{
                                 
                                  Box.AddText(ArialNormal, 14.0,
                                    primeros.ToString() + "\n" );
                                  Box.AddText(ArialNormal, 14.0,
                                letrasGrandes.ToString() );

                                  Box.AddText(ArialItalic, 30.0,
                               "             Thank you\n");

                               Box.AddText(ArialNormal, FontSize,
                                 cad.ToString()+"\n"+tabla.ToString());

                                  Box.AddText(ArialNormal, FontSize, "\n");
                                  Double PosY = Height;
                                  Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box);
                                  Contents.RestoreGraphicsState();

                                 
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
                                    mail2.Subject = "Recibo de caja del dia: " + saveFecha + " de la iglesia " + iglesiaText.Text; ;
                                    mail2.Body = "Hola " + nombreLabel.Text + ", por este medio le envio el recibo de caja de la iglesia " + iglesiaText.Text;
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
