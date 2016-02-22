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
            var sourceRazonesSociales = new AutoCompleteStringCollection();
            var sourceCuentas = new AutoCompleteStringCollection();
           
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
                    String queryXML = "SELECT ACNT_CODE,prioridad FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] WHERE rfc = '" + rfc + "' AND BUNIT = '" + Login.unidadDeNegocioGlobal + "' order by prioridad asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String cuenta = reader.GetString(0).Trim();
                                int prioridad = reader.GetInt32(1);
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("cuenta", cuenta);
                                dictionary.Add("prioridad", prioridad);
                                listaFinal.Add(dictionary);
                            }
                            listaPreferencias.Clear();
                            listaPreferencias.View = View.Details;
                            listaPreferencias.GridLines = true;
                            listaPreferencias.FullRowSelect = true;
                            listaPreferencias.Columns.Add("Cuenta", 200);
                            listaPreferencias.Columns.Add("Prioridad", 80);
                           

                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("cuenta"))
                                {
                                    string[] arr = new string[3];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["cuenta"]);
                                    arr[1] = Convert.ToString(dic["prioridad"]);
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
                    String queryXML = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[rfcCuentasPreferidas] (rfc,ACNT_CODE, prioridad,BUNIT) VALUES ('" + rfc + "','" + cuenta + "', " + prioridad + " ,'" + Login.unidadDeNegocioGlobal + "')";
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
    }
}
