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
namespace TerminarDeLigar
{
    public partial class Form1 : Form
    {
        public String sourceGlobal { get; set; }
        public String unidadDeNegocioGlobal { get; set; }
      
        public String conceptoGlobal { get; set; }
        public List<Dictionary<string, object>> listaDiarios { get; set; }
        public List<Dictionary<string, object>> listaFacturas { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.ContextMenu contextMenu2;
         public int diarioGlobalEncontrado { get; set; }
         public List<String> TREFERENCE_temporales  { get; set; }
          
        public List<Dictionary<string, object>> listaDiariosReference { get; set; }
        public List<Dictionary<string, object>> listaTemporalesReference { get; set; }
       
        public Form1()
        {
            InitializeComponent();
            sourceGlobal = "ERROR";
        }
        public Form1(String source, String BUNIT)
        {
            InitializeComponent();
            sourceGlobal = source;
            unidadDeNegocioGlobal = BUNIT;
        }

        private void sUNPLUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config_Fiscal form = new Config_Fiscal();
            form.Show();
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("settings.txt"))
                {
                    String line = sr.ReadToEnd();
                    Properties.Settings.Default.datasource = line;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            
            //despues obtengo una lista de todas las diferentes TREFERENCE de los temporales para el source -YA
            //primero obtengo una lista de todos los diarios mayores al ultimoDiarioAux - YA

            //despues hago un ciclo para todos los diariosobtenidos de la lista 1, y checo si contienen todos los TREFERENCE de temporal en los TREFENCE del diario, - YA
            //si tienen todos los treferece, checo para cada treference del temporal que en el mismo numero de consecutivo contenga el mismo tipo D_C y tenga la misma cuenta, y el mismo tipo de diario y sume el mismo amount - YA !!
            // si cumple con la validacion lo inserto en FISCAL_xml y cierro el TerminarDeLigar !!
            //y antes de cerrar borro los temporales para el source !!
            if(sourceGlobal.Equals("ERROR"))
            {
                System.Windows.Forms.MessageBox.Show("Se detectó que no se leyó el operador o la unidad de negocio", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
            listaDiariosReference = new List<Dictionary<string, object>>();
            listaTemporalesReference = new List<Dictionary<string, object>>();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            TREFERENCE_temporales = new List<String>();
            //obtengo una lista de todas las diferentes TREFERENCE de los temporales
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT TREFERENCE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '" + sourceGlobal + "' AND BUNIT = '" + unidadDeNegocioGlobal+"'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TREFERENCE_temporales.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(" 1    "+ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            int ultimoDiarioAuxDeLosTemporales = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT TOP 1 ultimoDiarioAux FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '" + sourceGlobal + "' AND BUNIT = '" + unidadDeNegocioGlobal+"'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                ultimoDiarioAuxDeLosTemporales=reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(" 2    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //lista de diarios mayores al ultimoDiarioAux
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            List<int> diariosMayoresalUltimoDiarioAux = new List<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "SELECT JRNAL_NO FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_SRCE = '" + sourceGlobal + "' AND JRNAL_NO > " + ultimoDiarioAuxDeLosTemporales + " order by JRNAL_NO asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                diariosMayoresalUltimoDiarioAux.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(" 3    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            List<String> TREFERENCE_diarioCandidato = new List<String>();
            bool inserto = true;
            foreach(int diarioCandidato in diariosMayoresalUltimoDiarioAux)
            {
                inserto = true;
                TREFERENCE_diarioCandidato.Clear();
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringSun))
                    {
                        connection.Open();
                        String queryXML = "SELECT DISTINCT TREFERENCE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = " + diarioCandidato;
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    TREFERENCE_diarioCandidato.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(" 4    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                bool yanoSigasMas = false;
                foreach(String TREFERENCE in TREFERENCE_temporales)
                {
                    if(!TREFERENCE_diarioCandidato.Contains(TREFERENCE))
                    {
                        yanoSigasMas = true;
                    }
                }
                
                if(!yanoSigasMas)
                {
                    
                    foreach(String TREFERENCE in TREFERENCE_temporales)
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connStringSun))
                            {
                                connection.Open();
                                //aqui voy
                                String queryXML = "SELECT ACCNT_CODE,JRNAL_LINE,D_C,JRNAL_TYPE,AMOUNT FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = " + diarioCandidato + " AND TREFERENCE = '" + TREFERENCE + "' order by JRNAL_LINE asc";
                                using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                                {
                                    SqlDataReader reader = cmdCheck.ExecuteReader();
                                    if (reader.HasRows)
                                    {
                                        //obtengo los datos de sunplus
                                        listaDiariosReference.Clear();
                                        listaTemporalesReference.Clear();
                                        while (reader.Read())
                                        {
                                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                            dictionary.Add("ACNT_CODE", reader.GetString(0).Trim());
                                            dictionary.Add("JRNAL_LINE", Convert.ToString(reader.GetInt32(1)).Trim());
                                            dictionary.Add("D_C", reader.GetString(2).Trim());
                                            dictionary.Add("JRNAL_TYPE", reader.GetString(3).Trim());
                                            dictionary.Add("AMOUNT", Convert.ToString(reader.GetDecimal(4)).Trim());
                                            listaDiariosReference.Add(dictionary);
                                        }

                                        String queryXML1 = "SELECT  ACNT_CODE, consecutivo, D_C, tipoDeDiario, amount FROM  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '" + sourceGlobal + "' AND TREFERENCE = '" + TREFERENCE + "' AND BUNIT = '" + unidadDeNegocioGlobal + "' order by consecutivo asc";
                                        using (SqlCommand cmdCheck1 = new SqlCommand(queryXML1, connection))
                                        {
                                            SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                            if (reader1.HasRows)
                                            {
                                                while (reader1.Read())
                                                {
                                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                                    dictionary.Add("ACNT_CODE", reader1.GetString(0).Trim());
                                                    dictionary.Add("consecutivo", Convert.ToString(reader1.GetInt32(1)).Trim());
                                                    dictionary.Add("D_C", reader1.GetString(2).Trim());
                                                    dictionary.Add("tipoDeDiario", reader1.GetString(3).Trim());
                                                    dictionary.Add("amount", Convert.ToString(reader1.GetDecimal(4)).Trim());
                                                    listaTemporalesReference.Add(dictionary);
                                                }
                                            }
                                        }
                                        int consecutivoEnElQueEstoy = -1;
                                        int consecutivoActual = 0;
                                        double amountTemporales = 0;
                                        foreach(Dictionary<string, object> dic in listaTemporalesReference)
                                        {
                                            consecutivoActual = Convert.ToInt32(dic["consecutivo"]);
                                            if(consecutivoEnElQueEstoy==-1)//first time
                                            {
                                                consecutivoEnElQueEstoy = consecutivoActual;
                                            }
                                            if(consecutivoActual!=consecutivoEnElQueEstoy)
                                            {
                                                if (!(Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["D_C"]).Equals(dic["D_C"].ToString()) && Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["ACNT_CODE"]).Equals(dic["ACNT_CODE"].ToString()) && Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["JRNAL_TYPE"]).Equals(dic["tipoDeDiario"].ToString()) && Math.Round(Convert.ToDouble(Convert.ToDecimal(listaDiariosReference[consecutivoEnElQueEstoy - 1]["AMOUNT"].ToString())), 2) == amountTemporales))
                                                {
                                                      inserto = false;
                                                }
                                                consecutivoEnElQueEstoy = consecutivoActual;
                                                amountTemporales = 0;
                                            }
                                            else
                                            {
                                                amountTemporales += Math.Round(Convert.ToDouble( Convert.ToDecimal(dic["amount"].ToString())),2);
                                            }
                                        }
                                        //el ultimo consecutivo de los temporales
                                        Dictionary<string, object> dic1 = listaTemporalesReference[listaTemporalesReference.Count - 1];
                                        consecutivoEnElQueEstoy = consecutivoActual;
                                        if (!(Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["D_C"]).Equals(dic1["D_C"].ToString()) && Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["ACNT_CODE"]).Equals(dic1["ACNT_CODE"].ToString()) && Convert.ToString(listaDiariosReference[consecutivoEnElQueEstoy - 1]["JRNAL_TYPE"]).Equals(dic1["tipoDeDiario"].ToString()) && Math.Round(Convert.ToDouble(Convert.ToDecimal(listaDiariosReference[consecutivoEnElQueEstoy - 1]["AMOUNT"].ToString())), 2) == amountTemporales))
                                        {
                                            inserto = false;
                                        }



                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(" 5    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                if (inserto)
                {
                    diarioGlobalEncontrado = diarioCandidato;
                    String connStringSun2 = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
       
                    mensajeLabel.Text = "El diario " + diarioGlobalEncontrado + " será enlazado, confirme el concepto";
                    String queryUnico = "SELECT DESCRIPTN FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = " + diarioGlobalEncontrado + " AND JRNAL_LINE = 2";
                    try
                    {
                        using (SqlConnection connection2 = new SqlConnection(connStringSun2))
                        {
                            connection2.Open();
                            using (SqlCommand cmdChecku = new SqlCommand(queryUnico, connection2))
                            {
                                SqlDataReader readeru = cmdChecku.ExecuteReader();
                                if (readeru.HasRows)
                                {
                                    if (readeru.Read())
                                    {
                                        conceptoText.Text = readeru.GetString(0);
                                        this.Cursor = System.Windows.Forms.Cursors.Arrow;
                                        return;
                                    }
                                }
                            }
                            this.Cursor = System.Windows.Forms.Cursors.Arrow;
                            return;
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(" 6    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            System.Windows.Forms.MessageBox.Show("No se encontró ningún diario candidato, favor de borrar los temporales desde sunplusito y ligar las facturas desde ahí.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }

        private void ligarAhoraSiButton_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            String concepto = conceptoText.Text.Trim();
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
           
            foreach (String TREFERENCE in TREFERENCE_temporales)
            {
                //obtengo los datos del temporal


                //obtengo los datos que necesito para cada TREFERENCE
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringSun))
                    {
                        connection.Open();
                        listaTemporalesReference.Clear();

                        String queryXML1 = "SELECT  ACNT_CODE, consecutivo, D_C, tipoDeDiario, amount, UUID, consecutivoFacturas, STATUS FROM  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '" + sourceGlobal + "' AND TREFERENCE = '" + TREFERENCE + "' AND BUNIT = '" + unidadDeNegocioGlobal + "' order by consecutivo asc, consecutivoFacturas asc";
                        using (SqlCommand cmdCheck1 = new SqlCommand(queryXML1, connection))
                        {
                            SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("ACNT_CODE", reader1.GetString(0).Trim());
                                    dictionary.Add("consecutivo", Convert.ToString(reader1.GetInt32(1)).Trim());
                                    dictionary.Add("D_C", reader1.GetString(2).Trim());
                                    dictionary.Add("tipoDeDiario", reader1.GetString(3).Trim());
                                    dictionary.Add("amount", Convert.ToString(reader1.GetDecimal(4)).Trim());
                                    dictionary.Add("UUID", reader1.GetString(5).Trim());
                                    dictionary.Add("consecutivoFacturas", Convert.ToString(reader1.GetInt32(6)).Trim());
                                    dictionary.Add("STATUS", reader1.GetString(7).Trim());
                                    listaTemporalesReference.Add(dictionary);
                                }
                            }
                        }



                        String queryXML = "SELECT JRNAL_LINE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] WHERE JRNAL_NO = " + diarioGlobalEncontrado + " AND TREFERENCE = '" + TREFERENCE + "' order by JRNAL_LINE asc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                int consecutivoDelMayor = 0;
                                //obtengo los datos de sunplus
                                listaDiariosReference.Clear();
                                while (reader.Read())
                                {
                                    consecutivoDelMayor++;
                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("JRNAL_LINE", Convert.ToString(reader.GetInt32(0)).Trim());
                                    dictionary.Add("consecutivoDelMayor", consecutivoDelMayor.ToString());
                                    listaDiariosReference.Add(dictionary);
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(" 7    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //inserto lo que encontré
                foreach (Dictionary<string, object> dic in listaTemporalesReference)
                {
                    int consecutivoTemporal = Convert.ToInt32(dic["consecutivo"]);
                    String JRNAL_LINE = Convert.ToString(listaDiariosReference[consecutivoTemporal - 1]["JRNAL_LINE"]);
                    String STATUS = Convert.ToString(dic["STATUS"]);
                    String UUID = Convert.ToString(dic["UUID"]);
                    String consecutivoFacturas = Convert.ToString(dic["consecutivoFacturas"]);
                    String amount = Convert.ToString(dic["amount"]);
                  
                    String query1 = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] ( BUNIT,JRNAL_NO,JRNAL_LINE,JRNAL_SOURCE,FOLIO_FISCAL,CONCEPTO,AMOUNT,STATUS,consecutivo) VALUES  ('"+unidadDeNegocioGlobal+"', "+diarioGlobalEncontrado+","+JRNAL_LINE+",'"+sourceGlobal+"','"+UUID+"','"+conceptoText.Text+"',"+amount+",'"+STATUS+"',"+consecutivoFacturas+") ";
                    String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connString))
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand(query1, connection);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(" 8    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                //elimino temporales
                String query2 = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE JRNAL_SOURCE = '"+sourceGlobal+"' AND BUNIT = '"+unidadDeNegocioGlobal+"'";
                String connString2 = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString2))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query2, connection);
                        cmd.ExecuteNonQuery();
                        if (System.Windows.Forms.Application.MessageLoop)
                        {
                            System.Windows.Forms.Application.Exit();
                        }
                        else
                        {
                            System.Environment.Exit(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(" 9    " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                System.Windows.Forms.MessageBox.Show("Hubo un error, perdón", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
        }
    }
}
