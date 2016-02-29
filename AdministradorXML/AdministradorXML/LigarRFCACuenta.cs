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
namespace AdministradorXML
{
    public partial class LigarRFCACuenta : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        private void llenaRFC()
        {
            String razonSocial = razonSocialText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT TOP 1 rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in ('1','2') AND razonSocial = '" + razonSocial + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                rfcText.Text = rfc;
                            }
                           
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void llenaRazonSocial()
        {
            String RFC = rfcText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT TOP 1 razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') AND rfc = '" + RFC + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String razonSocial = reader.GetString(0);
                                razonSocialText.Text = razonSocial;
                            }
                           
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public LigarRFCACuenta()
        {
            InitializeComponent();
        }

        private void LigarRFCACuenta_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();

            var source = new AutoCompleteStringCollection();
            var source0 = new AutoCompleteStringCollection();
            var source1 = new AutoCompleteStringCollection();
            var source2 = new AutoCompleteStringCollection();
            var source3 = new AutoCompleteStringCollection();
            var source4 = new AutoCompleteStringCollection();
            var source5 = new AutoCompleteStringCollection();
            var source6 = new AutoCompleteStringCollection();
            var source7 = new AutoCompleteStringCollection();
            var source8 = new AutoCompleteStringCollection();
            var source9 = new AutoCompleteStringCollection();
            
            var sourceRazonesSociales = new AutoCompleteStringCollection();
            var sourceCuentas = new AutoCompleteStringCollection();

            List<String> anal0S = new List<String>();
            List<String> anal1S = new List<String>();
            List<String> anal2S = new List<String>();
            List<String> anal3S = new List<String>();
            List<String> anal4S = new List<String>();
            List<String> anal5S = new List<String>();
            List<String> anal6S = new List<String>();
            List<String> anal7S = new List<String>();
            List<String> anal8S = new List<String>();
            List<String> anal9S = new List<String>();
           
            List<String> rfcS = new List<String>();
            List<String> razonesSocialeS = new List<String>();
            List<String> cuentasS = new List<String>();

            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT rfc,SUM(total) as total,razonSocial, count(*) as cuantos FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE STATUS in('1', '2') GROUP BY rfc,razonSocial order by rfc asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                String razonSocial = reader.GetString(2);
                                rfcS.Add(rfc);
                                razonesSocialeS.Add(razonSocial);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            source.AddRange(rfcS.ToArray());


            sourceRazonesSociales.AddRange(razonesSocialeS.ToArray());

            rfcText.AutoCompleteCustomSource = source;
            rfcText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            rfcText.AutoCompleteSource = AutoCompleteSource.CustomSource;


            razonSocialText.AutoCompleteCustomSource = sourceRazonesSociales;
            razonSocialText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            razonSocialText.AutoCompleteSource = AutoCompleteSource.CustomSource;

             try
            {
                using (SqlConnection connection1 = new SqlConnection(connString))
                {

                    String queryXML1 = "SELECT DISTINCT ACNT_CODE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas]  order by ACNT_CODE asc";
                    using (SqlCommand cmdCheck1 = new SqlCommand(queryXML1, connection1))
                    {
                        connection1.Open();
                    
                        SqlDataReader reader1=null;
                        try
                        {
                            reader1 = cmdCheck1.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }



                       if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                String ACNT_CODE = reader1.GetString(0).Trim();
                                cuentasS.Add(ACNT_CODE);
                            }
                        }//if reader
                    }
                    connection1.Close();
                }
            }
             catch (SqlException ex)
             {
                 System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             }
            
           
            sourceCuentas.AddRange(cuentasS.ToArray());


            cuentaSunPlusText.AutoCompleteCustomSource = sourceCuentas;
            cuentaSunPlusText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cuentaSunPlusText.AutoCompleteSource = AutoCompleteSource.CustomSource;

            String dimension = "";
            dimension = "ANAL_T0";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal0S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source0.AddRange(anal0S.ToArray());



            dimension = "ANAL_T1";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim(); 
                                anal1S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source1.AddRange(anal1S.ToArray());

            dimension = "ANAL_T2";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal2S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source2.AddRange(anal2S.ToArray());

            dimension = "ANAL_T3";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal3S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source3.AddRange(anal3S.ToArray());

            dimension = "ANAL_T4";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal4S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source4.AddRange(anal4S.ToArray());

            dimension = "ANAL_T5";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal5S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source5.AddRange(anal5S.ToArray());

            dimension = "ANAL_T6";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal6S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source6.AddRange(anal6S.ToArray());

            dimension = "ANAL_T7";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal7S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source7.AddRange(anal7S.ToArray());

            dimension = "ANAL_T8";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal8S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source8.AddRange(anal8S.ToArray());

            dimension = "ANAL_T9";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT DISTINCT " + dimension + " FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] order by " + dimension + " asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String aux0 = reader.GetString(0).Trim();
                                anal9S.Add(aux0);
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source9.AddRange(anal9S.ToArray());


            ANAL_T0.AutoCompleteCustomSource = source0;
            ANAL_T0.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T0.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T1.AutoCompleteCustomSource = source1;
            ANAL_T1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T2.AutoCompleteCustomSource = source2;
            ANAL_T2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T3.AutoCompleteCustomSource = source3;
            ANAL_T3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T3.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T4.AutoCompleteCustomSource = source4;
            ANAL_T4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T4.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T5.AutoCompleteCustomSource = source5;
            ANAL_T5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T5.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T6.AutoCompleteCustomSource = source6;
            ANAL_T6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T6.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T7.AutoCompleteCustomSource = source7;
            ANAL_T7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T7.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T8.AutoCompleteCustomSource = source8;
            ANAL_T8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T8.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ANAL_T9.AutoCompleteCustomSource = source9;
            ANAL_T9.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ANAL_T9.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void rfcText_TextChanged(object sender, EventArgs e)
        {
            llenaRazonSocial();
        }

        private void razonSocialText_TextChanged(object sender, EventArgs e)
        {
            llenaRFC();
        }
        public void verFuncion()
        {
            listaFinal.Clear();
            String rfc = rfcText.Text.Trim();
            if (rfc.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe el rfc", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT ACNT_CODE,prioridad, ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9 FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' order by prioridad asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String cuenta = reader.GetString(0).Trim();
                                int prioridad = reader.GetInt32(1);

                                String a0 = reader.GetString(2).Trim();
                                String a1 = reader.GetString(3).Trim();
                                String a2 = reader.GetString(4).Trim();
                                String a3 = reader.GetString(5).Trim();
                                String a4 = reader.GetString(6).Trim();
                                String a5 = reader.GetString(7).Trim();
                                String a6 = reader.GetString(8).Trim();
                                String a7 = reader.GetString(9).Trim();
                                String a8 = reader.GetString(10).Trim();
                                String a9 = reader.GetString(11).Trim();
                                
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("cuenta", cuenta);
                                dictionary.Add("prioridad", prioridad);
                                dictionary.Add("a0", a0);
                                dictionary.Add("a1", a1);
                                dictionary.Add("a2", a2);
                                dictionary.Add("a3", a3);
                                dictionary.Add("a4", a4);
                                dictionary.Add("a5", a5);
                                dictionary.Add("a6", a6);
                                dictionary.Add("a7", a7);
                                dictionary.Add("a8", a8);
                                dictionary.Add("a9", a9);
                                listaFinal.Add(dictionary);
                            }
                            listaPreferencias.Clear();
                            listaPreferencias.View = View.Details;
                            listaPreferencias.GridLines = true;
                            listaPreferencias.FullRowSelect = true;
                            listaPreferencias.Columns.Add("Cuenta", 200);
                            listaPreferencias.Columns.Add("Prioridad", 80);
                            listaPreferencias.Columns.Add("Ref2", 80);
                            listaPreferencias.Columns.Add("TFWW", 80);
                            listaPreferencias.Columns.Add("Fondo", 80);
                            listaPreferencias.Columns.Add("Función", 80);
                            listaPreferencias.Columns.Add("Reestriccion", 80);
                            listaPreferencias.Columns.Add("OrgId", 80);
                            listaPreferencias.Columns.Add("Who", 80);
                            listaPreferencias.Columns.Add("Flag", 80);
                            listaPreferencias.Columns.Add("Proj", 80);
                            listaPreferencias.Columns.Add("Detalle", 80);
                           

                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("cuenta"))
                                {
                                    string[] arr = new string[13];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["cuenta"]);
                                    arr[1] = Convert.ToString(dic["prioridad"]);
                                    arr[2] = Convert.ToString(dic["a0"]);
                                    arr[3] = Convert.ToString(dic["a1"]);
                                    arr[4] = Convert.ToString(dic["a2"]);
                                    arr[5] = Convert.ToString(dic["a3"]);
                                    arr[6] = Convert.ToString(dic["a4"]);
                                    arr[7] = Convert.ToString(dic["a5"]);
                                    arr[8] = Convert.ToString(dic["a6"]);
                                    arr[9] = Convert.ToString(dic["a7"]);
                                    arr[10] = Convert.ToString(dic["a8"]);
                                    arr[11] = Convert.ToString(dic["a9"]);
                                    itm = new ListViewItem(arr);
                                    listaPreferencias.Items.Add(itm);
                                }
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void ver_Click(object sender, EventArgs e)
        {
            verFuncion();
        }

        private void asociar_Click(object sender, EventArgs e)
        {
            String rfc = rfcText.Text.Trim();
            String cuenta = cuentaSunPlusText.Text.Trim();
            String a0 = ANAL_T0.Text.Trim().ToUpper();
            String a1 = ANAL_T1.Text.Trim().ToUpper();
            String a2 = ANAL_T2.Text.Trim().ToUpper();
            String a3 = ANAL_T3.Text.Trim().ToUpper();
            String a4 = ANAL_T4.Text.Trim().ToUpper();
            String a5 = ANAL_T5.Text.Trim().ToUpper();
            String a6 = ANAL_T6.Text.Trim().ToUpper();
            String a7 = ANAL_T7.Text.Trim().ToUpper();
            String a8 = ANAL_T8.Text.Trim().ToUpper();
            String a9 = ANAL_T9.Text.Trim().ToUpper();


            if(rfc.Equals("") || cuenta.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe el rfc y la cuenta sunplus", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //determina cuantos tiene, si no tiene, pone 1
            int prioridad = 1;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT ISNULL(MAX(prioridad),0) as prioridad FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                prioridad = reader.GetInt32(0) + 1;
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //guarda asociacion
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] (rfc,ACNT_CODE, prioridad,BUNIT,ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9) VALUES ('" + rfc + "','" + cuenta + "', " + prioridad + " ,'" + Login.unidadDeNegocioGlobal + "','" + a0 + "','" + a1 + "','" + a2 + "','" + a3 + "','" + a4 + "','" + a5 + "','" + a6 + "','" + a7 + "','" + a8 + "','" + a9 + "')";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        cmdCheck.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            verFuncion();


        }

        private void desasociar_Click(object sender, EventArgs e)
        {
            int cuantos = listaPreferencias.SelectedItems.Count;
            int prioridad = 99999;
                     
            if (cuantos > 0)
            {
                prioridad = Convert.ToInt32(listaPreferencias.SelectedItems[0].SubItems[1].Text.Trim());
            
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Primero tienes que seleccionar un elemento de la tabla", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //saco una lista como verFuncion
            //si es menor a la prioridad que tengo, no hago nada
            //si es igual a la prioridad que tengo, la elimino
            //si es mayor a la prioridad que tengo, le hago update -1
            String rfc = rfcText.Text.Trim();
            if (rfc.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe el rfc", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT prioridad FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' order by prioridad asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int prioridadARevisar = reader.GetInt32(0);
                                if(prioridadARevisar<prioridad)
                                {
                                    
                                }
                                else 
                                {
                                    if(prioridadARevisar==prioridad)//delete
                                    {
                                         String queryDELETE = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' AND prioridad = "+prioridad;
                                         using (SqlCommand cmdDelete = new SqlCommand(queryDELETE, connection))
                                         {
                                             cmdDelete.ExecuteNonQuery();
                                         }
                                    }
                                    else 
                                    {
                                        if(prioridadARevisar>prioridad)//update
                                        {
                                            String queryUPDATE = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] SET prioridad = "+(prioridadARevisar-1)+" WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' AND prioridad = " + prioridadARevisar;
                                            using (SqlCommand cmdUPDATE = new SqlCommand(queryUPDATE, connection))
                                            {
                                                cmdUPDATE.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }//if reader
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            verFuncion();


        }

        private void subir_Click(object sender, EventArgs e)
        {
            int cuantos = listaPreferencias.SelectedItems.Count;
            int prioridad = 99999;
            if (cuantos > 0)
            {
                prioridad = Convert.ToInt32(listaPreferencias.SelectedItems[0].SubItems[1].Text.Trim());
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Primero tienes que seleccionar un elemento de la tabla", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(prioridad==1)
            {
                return;
            }
            String rfc = rfcText.Text.Trim();
            if (rfc.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe el rfc", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    String queryUPDATE1 = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] SET prioridad = -1 WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' AND prioridad = " + (prioridad-1);
                    using (SqlCommand cmdUPDATE1 = new SqlCommand(queryUPDATE1, connection))
                    {
                        cmdUPDATE1.ExecuteNonQuery();
                    }

                    String queryUPDATE = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] SET prioridad = " + (prioridad - 1) + " WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' AND prioridad = " + prioridad;
                    using (SqlCommand cmdUPDATE = new SqlCommand(queryUPDATE, connection))
                    {
                        cmdUPDATE.ExecuteNonQuery();
                    }

                    String queryUPDATE2 = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] SET prioridad = " + prioridad + " WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' AND prioridad = -1";
                    using (SqlCommand cmdUPDATE2 = new SqlCommand(queryUPDATE2, connection))
                    {
                        cmdUPDATE2.ExecuteNonQuery();
                    }


                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            verFuncion();



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
