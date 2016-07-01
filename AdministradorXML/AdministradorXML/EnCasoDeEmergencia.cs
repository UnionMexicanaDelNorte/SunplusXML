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
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace AdministradorXML
{
    public partial class EnCasoDeEmergencia : Form
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
        public EnCasoDeEmergencia()
        {
            InitializeComponent();

        }

        private void EnCasoDeEmergencia_Load(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT PERIOD FROM ["+Properties.Settings.Default.sunDatabase+"].[dbo].["+Login.unidadDeNegocioGlobal+"_"+Properties.Settings.Default.sunLibro+"_SALFLDG] order by PERIOD asc";
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
                            String periodo = Convert.ToString( reader.GetInt32(0));
                            periodosDel.Items.Add(new Item(periodo, empiezo));
                           // periodosAl.Items.Add(new Item(periodo, empiezo));
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
            //periodosAl.SelectedIndex = periodosAl.Items.Count - 1;
            periodosDel.SelectedIndex = periodosDel.Items.Count - 1;
        }

        private void generar_Click(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String mesLetra="",anoLetra="";
            Item itm = (Item)periodosDel.SelectedItem;
            String mesS="",anoS="";
            String periodo = itm.Name;
            anoLetra = periodo.Substring(0, 4);
            anoS = anoLetra.Substring(2);
            String mesI = "", mesString=periodo.Substring(5,2);
            int mes = Convert.ToInt16(periodo.Substring(5,2));
            String debeDeSer = anoLetra + "-" + periodo.Substring(5,2);
            switch(mes)
            {
                case 1:
                    mesLetra = "ENERO";
                    mesS = "ENE";
                    mesI = "Jan";
                break;
                case 2:
                    mesLetra = "FEBRERO";
                    mesS = "FEB";
                    mesI = "Feb";
                break;
                case 3:
                    mesLetra = "MARZO";
                    mesS = "MAR";
                    mesI = "Mar";
                break;
                case 4:
                    mesLetra = "ABRIL";
                    mesS = "ABR";
                    mesI = "Apr";
                break;
                case 5:
                    mesLetra = "MAYO";
                    mesS = "MAYO";
                    mesI = "May";
                break;
                case 6:
                    mesLetra = "JUNIO";
                    mesS = "JUN";
                    mesI = "Jun";
                break;
                case 7:
                    mesLetra = "JULIO";
                    mesS = "JUL";
                    mesI = "Jul";
                break;
                case 8:
                    mesLetra = "AGOSTO";
                    mesS = "AGO";
                    mesI = "Aug";
                break;
                case 9:
                    mesLetra = "SEPTIEMBRE";
                    mesS = "SEP";
                    mesI = "Sep";
                break;
                case 10:
                    mesLetra = "OCTUBRE";
                    mesS = "OCT";
                    mesI = "Oct";
                break;
                case 11:
                    mesLetra = "NOVIEMBRE";
                    mesS = "NOV";
                    mesI = "Nov";
                break;
                case 12:
                    mesLetra = "DICIEMBRE";
                    mesS = "DIC";
                    mesI = "Dec";
                break;
            }
            String posibles1 = mesLetra + " " + anoLetra;
            String posibles2 = mesLetra + " " + anoS;
            String posibles3 = mesS + " " + anoLetra;
            String posibles4 = mesS + " " + anoS;


            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            Range aRange = ws.get_Range("A5", "A5");
            Object[] args = new Object[1];
            args[0] = "FECHA";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);


            Range aRange2 = ws.get_Range("B5", "B5");
            Object[] args2 = new Object[1];
            args2[0] = "DESCRIPCIÓN";
            aRange2.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange2, args2);


            Range aRange3 = ws.get_Range("C5", "C5");
            Object[] args3 = new Object[1];
            args3[0] = "DEBITO";
            aRange3.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange3, args3);

            Range aRange4 = ws.get_Range("D5", "D5");
            Object[] args4 = new Object[1];
            args4[0] = "CREDITO";
            aRange4.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange4, args4);

            Range aRange5 = ws.get_Range("E5", "E5");
            Object[] args5 = new Object[1];
            args5[0] = "BANCO";
            aRange5.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange5, args5);

            Range aRange6 = ws.get_Range("F5", "F5");
            Object[] args6 = new Object[1];
            args6[0] = "Referencia";
            aRange6.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange6, args6);

            Range aRange7 = ws.get_Range("G5", "G5");
            Object[] args7 = new Object[1];
            args7[0] = "Diario";
            aRange7.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange7, args7);

            
            /*args[2] = "DEBITO";
            args[3] = "CREDITO";
            args[4] = "BANCO";
            args[5] = "CH/TRANS";
            
          */

            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
            }
            int empiezo = 6;
            double debito = 0, credito = 0;
                               
           // String queryPeriodos = "SELECT c.DESCR, a.D_C, a.AMOUNT, a.PERIOD, a.TRANS_DATETIME, a.DESCRIPTN FROM ["+Properties.Settings.Default.sunDatabase+"].[dbo].["+Login.unidadDeNegocioGlobal+"_"+Properties.Settings.Default.sunLibro+"_SALFLDG] a INNER JOIN ["+Properties.Settings.Default.sunDatabase+"].[dbo].["+Login.unidadDeNegocioGlobal+"_ACNT] c on c.ACNT_CODE = a.ACCNT_CODE WHERE SUBSTRING( CAST(ACCNT_CODE AS NVARCHAR(11)),1,3)  = '102' AND DESCRIPTN like '%"+mesLetra+"%' AND DESCRIPTN like '%"+anoLetra+"%' order by DESCRIPTN asc";          
            String queryPeriodos = "SELECT c.DESCR, a.D_C, a.AMOUNT, a.PERIOD, a.TRANS_DATETIME, a.DESCRIPTN, a.DUE_DATETIME, a.TREFERENCE, a.JRNAL_NO FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] a INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] c on c.ACNT_CODE = a.ACCNT_CODE WHERE SUBSTRING( CAST(ACCNT_CODE AS NVARCHAR(11)),1,3)  = '102' order by DUE_DATETIME asc, DESCRIPTN asc";          
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        String Descripcion = "", TREFERENCE = "",dia="",Cuenta="",D_C="",fechaCorrecta="",fecha="";
                        while (reader.Read())
                        {
                            Descripcion = reader.GetString(5).ToUpper();
                            bool deboPonerlo = false;
                            /*if (Descripcion.Contains(mesLetra) || Descripcion.Contains(mesS))
                            {
                                if (Descripcion.Contains(anoLetra) || Descripcion.Contains(anoS))
                                {
                                   
                                    TREFERENCE = reader.GetString(7);

                                    if (!TREFERENCE.Contains("NOMINA"))
                                    {
                                        deboPonerlo = true;
                                    }
                                }
                            }*/
                            if (Descripcion.Contains(posibles1) || Descripcion.Contains(posibles2) || Descripcion.Contains(posibles3) || Descripcion.Contains(posibles4))
                            {
                                TREFERENCE = reader.GetString(7);
                                if (!TREFERENCE.Contains("ABONO NOMINA"))
                                {
                                    deboPonerlo = true;
                                }
                            }
                            if(deboPonerlo)
                            {
                                dia = Descripcion.Substring(0, 2);
                                if (dia == "00") { dia = "01"; }
                                int n;
                                bool isNumeric = int.TryParse(dia, out n);
                                if(!isNumeric)
                                {
                                    int indexAux = Descripcion.IndexOf(mesS);
                                    if(indexAux>0)
                                    {
                                        indexAux -= 3;
                                        dia = Descripcion.Substring(indexAux, 2);
                                    }
                                }

                                fechaCorrecta = dia + "/" + mesString + "/" + anoLetra;
                                Cuenta = reader.GetString(0);
                                D_C = reader.GetString(1);
                                double cantidad = Math.Abs(Convert.ToDouble(reader.GetDecimal(2)));
                                fecha = Convert.ToString(reader.GetDateTime(4)).Substring(0, 10);

                                 int JRNAL_NO = reader.GetInt32(8);
                                
                                String fechaDue = "";
                                if (!reader.IsDBNull(6))
                                {
                                    fechaDue = Convert.ToString(reader.GetDateTime(6));
                                }

                                TREFERENCE = reader.GetString(7);


                                debito = 0; credito = 0;
                                if (D_C.Equals("D"))
                                {
                                    debito = cantidad;
                                    Descripcion = "DIEZMOS Y OFRENDAS";
                                }
                                else
                                {
                                    credito = cantidad;
                                    if (TREFERENCE.Contains("COMISIONES"))
                                    {
                                        Descripcion = "COMISIONES BANCARIAS";
                                    }
                                    else
                                    {
                                        if (TREFERENCE.Contains("INTERESES"))
                                        {
                                            Descripcion = "INTERESES BANCARIOS";
                                        }
                                        else
                                        {
                                            if (TREFERENCE.Contains("IMPUESTOS"))
                                            {
                                                Descripcion = "IMPUESTOS BANCARIOS";
                                            }
                                            else
                                            {
                                                if (TREFERENCE.Contains("RENDCOMSCOTIA"))
                                                {
                                                    Descripcion = "COMISIONES BANCARIAS";
                                                }
                                                else
                                                {
                                                    Descripcion = "NO SE QUE PONER AQUI";
                                                }
                                            }
                                        }
                                    }
                                }
                                Range aRange1 = ws.get_Range("A" + empiezo, "A" + empiezo);
                                Object[] args1 = new Object[1];
                                args1[0] = fechaCorrecta;
                                aRange1.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange1, args1);


                                Range aRangeB = ws.get_Range("B" + empiezo, "B" + empiezo);
                                Object[] argsB = new Object[1];
                                argsB[0] = Descripcion;
                                aRangeB.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeB, argsB);


                                Range aRangeCC = ws.get_Range("C" + empiezo, "C" + empiezo);
                                Object[] argsCC = new Object[1];
                                argsCC[0] = debito;
                                aRangeCC.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeCC, argsCC);

                                Range aRangeD = ws.get_Range("D" + empiezo, "D" + empiezo);
                                Object[] argsD = new Object[1];
                                argsD[0] = credito;
                                aRangeD.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeD, argsD);

                                Range aRangeE = ws.get_Range("E" + empiezo, "E" + empiezo);
                                Object[] argsE = new Object[1];
                                argsE[0] = Cuenta;
                                aRangeE.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeE, argsE);

                                Range aRangeF = ws.get_Range("F" + empiezo, "F" + empiezo);
                                Object[] argsF = new Object[1];
                                argsF[0] = TREFERENCE;
                                aRangeF.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeF, argsF);

                                Range aRangeG = ws.get_Range("G" + empiezo, "G" + empiezo);
                                Object[] argsG = new Object[1];
                                argsG[0] = JRNAL_NO;
                                aRangeG.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeG, argsG);
                               // System.Threading.Thread.Sleep(100);
                                empiezo++;
                            }
                           
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Periodos, primero descarga xml del buzon tributario.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    String queryGastos = "SELECT a.DUE_DATETIME, a.JRNAL_NO, a.D_C,a.AMOUNT,a.PERIOD,a.TRANS_DATETIME, a.DESCRIPTN, a.ACCNT_CODE, c.DESCR, a.TREFERENCE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] a INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] c on c.ACNT_CODE = a.ACCNT_CODE WHERE SUBSTRING( CAST(ACCNT_CODE AS NVARCHAR(11)),1,3)  = '102' AND D_C = 'C' order by DUE_DATETIME asc ";
                    SqlCommand cmdCheck1 = new SqlCommand(queryGastos, connection);
                    SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        int diario = -1;
                        String DescripcionBuena = "";
                        String fechaDue = "", fechaDueOriginal = "", Cuenta1 = "", D_C = "",TREFERENCE1="",Descripcion1="";
                        while (reader1.Read())
                        {
                            if (!reader1.IsDBNull(0))
                            {
                                fechaDueOriginal = Convert.ToString(reader1.GetDateTime(0)).Substring(0, 10);
                                String anio = fechaDueOriginal.Substring(6);
                                String dia = fechaDueOriginal.Substring(0, 2);
                                String mesX = fechaDueOriginal.Substring(3, 2);
                                fechaDue = anio + "-" + mesX;
                            }

                            TREFERENCE1 = reader1.GetString(9);

                            if (fechaDue.Equals(debeDeSer) && !TREFERENCE1.Contains("NOMINA"))
                            {
                                diario = reader1.GetInt32(1);
                                DescripcionBuena = reader1.GetString(6).ToUpper();

                                String verDESCR = "SELECT TREFERENCE, DESCRIPTN FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = "+diario+" AND TREFERENCE = '"+TREFERENCE1+"' AND D_C='D'";
                                SqlCommand cmdCheck2 = new SqlCommand(verDESCR, connection);
                                SqlDataReader reader2 = cmdCheck2.ExecuteReader();
                                if (reader2.HasRows)
                                {
                                    if(reader2.Read())
                                    {
                                        DescripcionBuena = reader2.GetString(1).Trim();
                                    }
                                }
                                Cuenta1 = reader1.GetString(8).ToUpper();
                                
                                 D_C = reader1.GetString(2);

                               
                                if (DescripcionBuena.Contains("GASOLINA"))
                                {
                                    DescripcionBuena = "COMBUSTIBLES";
                                }
                                else
                                {
                                    if(DescripcionBuena.Contains("COMISIONES"))
                                    {
                                        DescripcionBuena = "COMISIONES BANCARIAS";
                                    }
                                    else
                                    {
                                        if (DescripcionBuena.Contains("DIEZMOS"))
                                        {
                                            DescripcionBuena = "ENVIO DE REMESAS";
                                        }
                                        else
                                        {
                                            if (DescripcionBuena.Contains("JUBILACION"))
                                            {
                                                DescripcionBuena = "ENVIO DE REMESAS";
                                            }
                                            else
                                            {
                                                if (DescripcionBuena.Contains("OFRENDA"))
                                                {
                                                    DescripcionBuena = "OFRENDA ESPECIAL";
                                                }
                                                else
                                                {
                                                    if (DescripcionBuena.Contains("CANCELACION"))
                                                    {
                                                        DescripcionBuena = "ENVIO DE REMESAS";
                                                    }
                                                    else
                                                    {
                                                        if (DescripcionBuena.Contains("CAJA CHICA"))
                                                        {
                                                            DescripcionBuena = "CAJA CHICA";
                                                        }
                                                        else
                                                        {
                                                            if (DescripcionBuena.Contains("EDUCATIVO"))
                                                            {
                                                                DescripcionBuena = "MINISTERIO EDUCACION";
                                                            }
                                                            else
                                                            {
                                                                if (DescripcionBuena.Contains("CASA DE GOBIERNO") || DescripcionBuena.Contains("CASA GOBIERNO"))
                                                                {
                                                                    DescripcionBuena = "MANTENIMIENTO CASA DE GOBIERNO";
                                                                }
                                                                else
                                                                {
                                                                    if (DescripcionBuena.Contains("CAMBIO") && (DescripcionBuena.Contains("DLS") || DescripcionBuena.Contains("DLLS")))
                                                                    {
                                                                        DescripcionBuena = "CAMBIO DE DOLARES";
                                                                    }
                                                                    else
                                                                    {

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                double cantidad = Math.Abs(Convert.ToDouble(reader1.GetDecimal(3)));
                                debito = 0; credito = 0;
                                if (D_C.Equals("D"))
                                {
                                    debito = cantidad;
                                }
                                else
                                {
                                    credito = cantidad;
                                }
                                Range aRange1X = ws.get_Range("A" + empiezo, "A" + empiezo);
                                Object[] args1X = new Object[1];
                                args1X[0] = fechaDueOriginal;
                                aRange1X.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange1X, args1X);


                                Range aRangeB = ws.get_Range("B" + empiezo, "B" + empiezo);
                                Object[] argsB = new Object[1];
                                argsB[0] = DescripcionBuena;
                                aRangeB.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeB, argsB);


                                Range aRangeC = ws.get_Range("C" + empiezo, "C" + empiezo);
                                Object[] argsC = new Object[1];
                                argsC[0] = debito;
                                aRangeC.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeC, argsC);

                                Range aRangeD = ws.get_Range("D" + empiezo, "D" + empiezo);
                                Object[] argsD = new Object[1];
                                argsD[0] = credito;
                                aRangeD.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeD, argsD);

                                Range aRangeE = ws.get_Range("E" + empiezo, "E" + empiezo);
                                Object[] argsE = new Object[1];
                                argsE[0] = Cuenta1;
                                aRangeE.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeE, argsE);

                                Range aRangeFF = ws.get_Range("F" + empiezo, "F" + empiezo);
                                Object[] argsFF = new Object[1];
                                argsFF[0] = TREFERENCE1;
                                aRangeFF.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeFF, argsFF);

                                Range aRangeGG = ws.get_Range("G" + empiezo, "G" + empiezo);
                                Object[] argsGG = new Object[1];
                                argsGG[0] = diario;
                                aRangeGG.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeGG, argsGG);
                               // System.Threading.Thread.Sleep(100);
                            /*    Range aRangeH = ws.get_Range("H" + empiezo, "H" + empiezo);
                                Object[] argsH = new Object[1];
                                argsH[0] = fechaDueOriginal;
                                aRangeH.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeH, argsH);
                                */
                                empiezo++;



                           
                            }
                        }
                    }
                }
                

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            Range aRangeCCC = ws.get_Range("C" + empiezo, "C" + empiezo);
            Object[] argsCCC = new Object[1];
            argsCCC[0] = "=SUMA(C6:C"+(empiezo-1)+")";
            aRangeCCC.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeCCC, argsCCC);

            Range aRangeDDD = ws.get_Range("D" + empiezo, "D" + empiezo);
            Object[] argsDDD = new Object[1];
            argsDDD[0] = "=SUMA(D6:D" + (empiezo-1) + ")";
            aRangeDDD.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRangeDDD, argsDDD);

            // Change the cells in the C1 to C7 range of the worksheet to the number 8.
           // aRange.Value2 = 8;
        }
    }
}
