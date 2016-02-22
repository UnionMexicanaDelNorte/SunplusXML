using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using PdfFileWriter;
using System.IO;
namespace AdministradorXML
{
    public partial class EstadoDeCuenta : Form
    {
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
        public EstadoDeCuenta()
        {
            InitializeComponent();
        }

        private void generarButton_Click(object sender, EventArgs e)
        {
            Item itm = (Item)periodosCombo.SelectedItem;
            int periodo = Convert.ToInt32(itm.Name);
            String cuenta = cuentaText.Text.Trim();

            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT JRNAL_NO, JRNAL_LINE, AMOUNT, PERIOD, JRNAL_SRCE, TRANS_DATETIME, ALLOCATION, D_C, JRNAL_TYPE, ANAL_T0, ANAL_T1, ANAL_T2, ANAL_T3, ANAL_T4, ANAL_T5, ANAL_T6, ANAL_T7, ANAL_T8, ANAL_T9 FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE ACCNT_CODE = '"+cuenta+"' AND PERIOD <= "+periodo+" order by PERIOD asc, JRNAL_NO asc, JRNAL_LINE desc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        String FileName = cuentaText.Text + "_" + periodo + ".pdf";
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
                        PdfPage Page0 = new PdfPage(Document);
                        PdfContents Contents0 = new PdfContents(Page0);

                        PdfPage Page1 = null;
                        PdfContents Contents1 = null;
                        PdfPage Page2 = null;
                        PdfContents Contents2 = null;
                        PdfPage Page3 = null;
                        PdfContents Contents3 = null;
                        PdfPage Page4 = null;
                        PdfContents Contents4 = null;
                        PdfPage Page5 = null;
                        PdfContents Contents5 = null;
                        PdfPage Page6 = null;
                        PdfContents Contents6 = null;
                        PdfPage Page7 = null;
                        PdfContents Contents7 = null;
                        PdfPage Page8 = null;
                        PdfContents Contents8 = null;
                        PdfPage Page9 = null;
                        PdfContents Contents9 = null;
                        PdfPage Page10 = null;
                        PdfContents Contents10 = null;
                        PdfPage Page11 = null;
                        PdfContents Contents11 = null;
                        PdfPage Page12 = null;
                        PdfContents Contents12 = null;
                        PdfPage Page13 = null;
                        PdfContents Contents13 = null;
                        PdfPage Page14 = null;
                        PdfContents Contents14 = null;
                        PdfPage Page15 = null;
                        PdfPage Page16 = null;
                        PdfPage Page17 = null;
                        PdfPage Page18 = null;
                        PdfPage Page19 = null;
                        PdfPage Page20 = null;
                        PdfPage Page21 = null;
                       /* PdfPage Page22 = null;
                        PdfPage Page23 = null;
                        PdfPage Page24 = null;
                        PdfPage Page25 = null;
                        PdfPage Page26 = null;
                        PdfPage Page27 = null;
                        PdfPage Page28 = null;
                        PdfPage Page29 = null;
                        PdfPage Page30 = null;
                        PdfPage Page31 = null;
                        PdfPage Page32 = null;
                        PdfPage Page33 = null;
                        PdfPage Page34 = null;
                        PdfPage Page35 = null;
                        PdfPage Page36 = null;
                        PdfPage Page37 = null;
                        PdfPage Page38 = null;
                        PdfPage Page39 = null;
                        PdfPage Page40 = null;
                        PdfPage Page41 = null;
                        */
                        PdfContents Contents15 = null;
                        PdfContents Contents16 = null;
                        PdfContents Contents17 = null;
                        PdfContents Contents18 = null;
                        PdfContents Contents19 = null;
                        PdfContents Contents20 = null;
                        PdfContents Contents21 = null;
                       /* PdfContents Contents22 = null;
                        PdfContents Contents23 = null;
                        PdfContents Contents24 = null;
                        PdfContents Contents25 = null;
                        PdfContents Contents26 = null;
                        PdfContents Contents27 = null;
                        PdfContents Contents28 = null;
                        PdfContents Contents29 = null;
                        PdfContents Contents30 = null;
                        PdfContents Contents31 = null;
                        PdfContents Contents32 = null;
                        PdfContents Contents33 = null;
                        PdfContents Contents34 = null;
                        PdfContents Contents35 = null;
                        PdfContents Contents36 = null;
                        PdfContents Contents37 = null;
                        PdfContents Contents38 = null;
                        PdfContents Contents39 = null;
                        PdfContents Contents40 = null;
                        PdfContents Contents41 = null;
                       */






                        Contents0.SaveGraphicsState();


                        const Double Width = 8.15;
                        const Double Height = 10.65;
                        //const Double FontSize = 12.0;
                        PdfFileWriter.TextBox Box0 = new PdfFileWriter.TextBox(Width, 0.25);
                        StringBuilder lineas = new StringBuilder("");

                        PdfFileWriter.TextBox Box1 = null;
                        PdfFileWriter.TextBox Box2 = null;
                        PdfFileWriter.TextBox Box3 = null;
                        PdfFileWriter.TextBox Box4 = null;
                        PdfFileWriter.TextBox Box5 = null;
                        PdfFileWriter.TextBox Box6 = null;
                        PdfFileWriter.TextBox Box7 = null;
                        PdfFileWriter.TextBox Box8 = null;
                        PdfFileWriter.TextBox Box9 = null;
                        PdfFileWriter.TextBox Box10 = null;
                        PdfFileWriter.TextBox Box11 = null;
                        PdfFileWriter.TextBox Box12 = null;
                        PdfFileWriter.TextBox Box13 = null;
                        PdfFileWriter.TextBox Box14 = null;
                        PdfFileWriter.TextBox Box15 = null;
                        PdfFileWriter.TextBox Box16 = null;
                        PdfFileWriter.TextBox Box17 = null;
                        PdfFileWriter.TextBox Box18 = null;
                        PdfFileWriter.TextBox Box19 = null;
                        PdfFileWriter.TextBox Box20 = null;
                        PdfFileWriter.TextBox Box21 = null;
                       /* PdfFileWriter.TextBox Box22 = null;
                        PdfFileWriter.TextBox Box23 = null;
                        PdfFileWriter.TextBox Box24 = null;
                        PdfFileWriter.TextBox Box25 = null;
                        PdfFileWriter.TextBox Box26 = null;
                        PdfFileWriter.TextBox Box27 = null;
                        PdfFileWriter.TextBox Box28 = null;
                        PdfFileWriter.TextBox Box29 = null;
                        PdfFileWriter.TextBox Box30 = null;
                        PdfFileWriter.TextBox Box31 = null;
                        PdfFileWriter.TextBox Box32 = null;
                        PdfFileWriter.TextBox Box33 = null;
                        PdfFileWriter.TextBox Box34 = null;
                        PdfFileWriter.TextBox Box35 = null;
                        PdfFileWriter.TextBox Box36 = null;
                        PdfFileWriter.TextBox Box37 = null;
                        PdfFileWriter.TextBox Box38 = null;
                        PdfFileWriter.TextBox Box39 = null;
                        PdfFileWriter.TextBox Box40 = null;
                        PdfFileWriter.TextBox Box41 = null;
                        */


                        double total = 0;
                        int cuantosPorPagina = 30;
                        int cuantosVamos = 0;
                        int numeroDePagina = 0;
                        while (reader.Read())
                        {
                            String JRNAL_NO = Convert.ToString(reader.GetInt32(0)).Trim();
                            String JRNAL_LINE = Convert.ToString(reader.GetInt32(1)).Trim();
                            String TRANS_DATETIME = reader.GetDateTime(5).ToString().Substring(0, 10);

                            String PERIOD = Convert.ToString(reader.GetInt32(3));

                            String JRNAL_SRCE = reader.GetString(4).Trim();

                            String ALLOCATION = reader.GetString(6).Trim();
                           
                           
                            String D_C = reader.GetString(7).Trim();
                            String AMOUNT_SUNPLUS = Convert.ToString( Math.Abs(Convert.ToDouble(reader.GetDecimal(2))) ).Trim();
                            if(D_C.Equals("D"))//debito
                            {
                                total += Math.Abs(Convert.ToDouble(reader.GetDecimal(2)));
                            }
                            else//creditos
                            {
                                total -= Math.Abs(Convert.ToDouble(reader.GetDecimal(2)));
                            }
                            String ANAL_T0 = reader.GetString(9).Trim();
                            String ANAL_T1 = reader.GetString(10).Trim();
                            String ANAL_T2 = reader.GetString(11).Trim();
                            String ANAL_T3 = reader.GetString(12).Trim();
                            String ANAL_T4 = reader.GetString(13).Trim();
                            String ANAL_T5 = reader.GetString(14).Trim();
                            String ANAL_T6 = reader.GetString(15).Trim();
                            String ANAL_T7 = reader.GetString(16).Trim();
                            String ANAL_T8 = reader.GetString(17).Trim();
                            String ANAL_T9 = reader.GetString(18).Trim();
                            if (cuantosVamos<cuantosPorPagina)
                            {
                                cuantosVamos++;
                            }
                            else
                            {
                                Double PosY = Height;
                                numeroDePagina++;
                               // Document.Add

                                switch(numeroDePagina)
                                {
                                    case 1:
                                        Box0.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents0.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box0);
                                        Contents0.SaveGraphicsState();
                                        Contents0.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page1 = new PdfPage(Document);
                                        Contents1 = new PdfContents(Page1);
                                        Box1 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 2:
                                        Box1.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents1.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box1);
                                        Contents1.SaveGraphicsState();
                                        Contents1.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page2 = new PdfPage(Document);
                                        Contents2 = new PdfContents(Page2);
                                        Box2 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 3:
                                        Box2.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents2.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box2);
                                        Contents2.SaveGraphicsState();
                                        Contents2.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page3 = new PdfPage(Document);
                                        Contents3 = new PdfContents(Page3);
                                        Box3 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 4:
                                        Box3.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents3.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box3);
                                        Contents3.SaveGraphicsState();
                                        Contents3.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page4 = new PdfPage(Document);
                                        Contents4 = new PdfContents(Page4);
                                        Box4 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 5:
                                        Box4.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents4.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box4);
                                        Contents4.SaveGraphicsState();
                                        Contents4.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page5 = new PdfPage(Document);
                                        Contents5 = new PdfContents(Page5);
                                        Box5 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 6:
                                        Box5.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents5.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box5);
                                        Contents5.SaveGraphicsState();
                                        Contents5.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page6 = new PdfPage(Document);
                                        Contents6 = new PdfContents(Page6);
                                        Box6 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 7:
                                        Box6.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents6.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box6);
                                        Contents6.SaveGraphicsState();
                                        Contents6.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page7 = new PdfPage(Document);
                                        Contents7 = new PdfContents(Page7);
                                        Box7 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 8:
                                        Box7.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents7.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box7);
                                        Contents7.SaveGraphicsState();
                                        Contents7.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page8 = new PdfPage(Document);
                                        Contents8 = new PdfContents(Page8);
                                        Box8 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 9:
                                        Box8.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents8.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box8);
                                        Contents8.SaveGraphicsState();
                                        Contents8.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page9 = new PdfPage(Document);
                                        Contents9 = new PdfContents(Page9);
                                        Box9 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 10:
                                        Box9.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents9.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box9);
                                        Contents9.SaveGraphicsState();
                                        Contents9.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page10 = new PdfPage(Document);
                                        Contents10 = new PdfContents(Page10);
                                        Box10 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 11:
                                        Box10.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents10.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box10);
                                        Contents10.SaveGraphicsState();
                                        Contents10.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page11 = new PdfPage(Document);
                                        Contents11 = new PdfContents(Page11);
                                        Box11 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 12:
                                        Box11.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents11.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box11);
                                        Contents11.SaveGraphicsState();
                                        Contents11.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page12 = new PdfPage(Document);
                                        Contents12 = new PdfContents(Page12);
                                        Box12 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 13:
                                        Box12.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents12.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box12);
                                        Contents12.SaveGraphicsState();
                                        Contents12.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page13 = new PdfPage(Document);
                                        Contents13 = new PdfContents(Page13);
                                        Box13 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 14:
                                        Box13.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents13.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box13);
                                        Contents13.SaveGraphicsState();
                                        Contents13.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page14 = new PdfPage(Document);
                                        Contents14 = new PdfContents(Page14);
                                        Box14 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 15:
                                        Box14.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents14.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box14);
                                        Contents14.SaveGraphicsState();
                                        Contents14.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page15 = new PdfPage(Document);
                                        Contents15 = new PdfContents(Page15);
                                        Box15 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 16:
                                        Box15.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents15.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box15);
                                        Contents15.SaveGraphicsState();
                                        Contents15.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page16 = new PdfPage(Document);
                                        Contents16 = new PdfContents(Page16);
                                        Box16 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 17:
                                        Box16.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents16.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box16);
                                        Contents16.SaveGraphicsState();
                                        Contents16.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page17 = new PdfPage(Document);
                                        Contents17 = new PdfContents(Page17);
                                        Box17 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 18:
                                        Box17.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents17.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box17);
                                        Contents17.SaveGraphicsState();
                                        Contents17.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page18 = new PdfPage(Document);
                                        Contents18 = new PdfContents(Page18);
                                        Box18 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 19:
                                        Box18.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents18.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box18);
                                        Contents18.SaveGraphicsState();
                                        Contents18.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page19 = new PdfPage(Document);
                                        Contents19 = new PdfContents(Page19);
                                        Box19 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 20:
                                        Box19.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents19.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box19);
                                        Contents19.SaveGraphicsState();
                                        Contents19.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page20 = new PdfPage(Document);
                                        Contents20 = new PdfContents(Page20);
                                        Box20 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;
                                    case 21:
                                        Box20.AddText(ArialNormal, 14.0,
                                        lineas.ToString() + "\n");
                                        Contents20.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box20);
                                        Contents20.SaveGraphicsState();
                                        Contents20.RestoreGraphicsState();
                                        lineas = new StringBuilder("");
                                        cuantosVamos = 0;
                                        Page21 = new PdfPage(Document);
                                        Contents21 = new PdfContents(Page21);
                                        Box21 = new PdfFileWriter.TextBox(Width, 0.25);
                                    break;


                                
                                }

                                
                               
                                

                            }
                            lineas.Append("" + PERIOD + "-" + JRNAL_NO + "-" + JRNAL_LINE + "-" + TRANS_DATETIME + " $" + AMOUNT_SUNPLUS + " - " + JRNAL_SRCE + "\n");
                        }

                      


                        Document.CreateFile();
                        System.Diagnostics.Process.Start(FileName);

                        System.Windows.Forms.MessageBox.Show("al parecer, todo bien", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    


                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void EstadoDeCuenta_Load(object sender, EventArgs e)
        {
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
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 1;

            var source = new AutoCompleteStringCollection();
          
            List<String> cuentas = new List<String>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                   
                    queryXML = "SELECT ACNT_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].["+Login.unidadDeNegocioGlobal+"_ACNT]";
                       
                    
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ACNT_CODE = reader.GetString(0).Trim();
                                cuentas.Add(ACNT_CODE);
                            }
                        }//if reader
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            source.AddRange(cuentas.ToArray());
            cuentaText.AutoCompleteCustomSource = source;
            cuentaText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cuentaText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String FileName = "testmac2.pdf";
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


            const Double Width = 8.15;
            const Double Height = 10.65;
            //const Double FontSize = 12.0;
            PdfFileWriter.TextBox Box = new PdfFileWriter.TextBox(Width, 0.25);
            StringBuilder lineas = new StringBuilder("hola 2");

            Box.AddText(ArialNormal, 14.0,
                                  lineas.ToString() + "\n");
            Double PosY = Height;
            Contents.DrawText(0.0, ref PosY, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, Box);
            Contents.RestoreGraphicsState();


            Document.CreateFile();
            
            System.Windows.Forms.MessageBox.Show("al parecer, todo bien tambien", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    
        }
    }
}
