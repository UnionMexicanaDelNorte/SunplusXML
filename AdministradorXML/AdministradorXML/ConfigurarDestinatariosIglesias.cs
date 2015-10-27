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
    public partial class ConfigurarDestinatariosIglesias : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
       
        public ConfigurarDestinatariosIglesias()
        {
            InitializeComponent();
        }

        private void ConfigurarDestinatariosIglesias_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
            leerDatos();
        }
        private void leerDatos()
        {
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            listaFinal.Clear();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT ANL_CODE,NAME FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ANL_CODE] WHERE ANL_CAT_ID= '07' AND SUBSTRING( ANL_CODE,1,1) = 'T' order by ANL_CODE asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ANL_CODE = reader.GetString(0).Trim();
                                String NAME = reader.GetString(1).Trim();
                                //cuanto esta enlazado de esa linea
                                String queryFISCAL = "SELECT correo, nombre FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias] WHERE ANL_CODE = '" + ANL_CODE + "'";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    using(SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader())
                                    {
                                        if (readerFISCAL.HasRows)
                                        {
                                            if (readerFISCAL.Read())
                                            {
                                                String correo = readerFISCAL.GetString(0).Trim();
                                                String nombre = readerFISCAL.GetString(1).Trim();
                                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                                dictionary.Add("ANL_CODE", ANL_CODE);
                                                dictionary.Add("NAME", NAME);
                                                dictionary.Add("nombre", nombre);
                                                dictionary.Add("correo", correo);
                                                listaFinal.Add(dictionary);
                                            }
                                        }
                                        else
                                        {
                                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                            dictionary.Add("ANL_CODE", ANL_CODE);
                                            dictionary.Add("NAME", NAME);
                                            dictionary.Add("nombre", "");
                                            dictionary.Add("correo", "");
                                            listaFinal.Add(dictionary);
                                        }
                                    }
                                }//using
                            }//while


                            iglesiasList.Clear();
                            iglesiasList.View = View.Details;
                            iglesiasList.GridLines = true;
                            iglesiasList.FullRowSelect = true;
                            iglesiasList.Columns.Add("Codigo", 100);
                            iglesiasList.Columns.Add("Iglesia", 150);
                            iglesiasList.Columns.Add("Nombre", 200);
                            iglesiasList.Columns.Add("Correo", 200);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("ANL_CODE"))
                                {
                                    string[] arr = new string[8];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["ANL_CODE"]);
                                    arr[1] = Convert.ToString(dic["NAME"]);
                                    arr[2] = Convert.ToString(dic["nombre"]);
                                    arr[3] = Convert.ToString(dic["correo"]);
                                    itm = new ListViewItem(arr);
                                    iglesiasList.Items.Add(itm);
                                }
                            }
                        }
                    }//using
                }
            }
            catch (SqlException ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void iglesiasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(iglesiasList.SelectedItems.Count>0)
            {
                String nombre = iglesiasList.SelectedItems[0].SubItems[2].Text.Trim();
                String correo = iglesiasList.SelectedItems[0].SubItems[3].Text.Trim();
                String anl = iglesiasList.SelectedItems[0].SubItems[0].Text.Trim();
                nombreText.Text = nombre;
                correoText.Text = correo;
                ANL_CODE.Text = anl;
            }
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            if (nombreText.Text.Trim().Equals("") || correoText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe le nombre del destinatario y su correo electronico", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String nombre = nombreText.Text.Trim();
                        String correo = correoText.Text.Trim();



                          String queryCheck = "SELECT nombre,correo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias] WHERE ANL_CODE = '" + ANL_CODE.Text + "'";
                          using(SqlCommand cmdCheck = new SqlCommand(queryCheck, connection))
                          {
                               SqlDataReader reader = cmdCheck.ExecuteReader();
                               if (reader.HasRows)
                               {
                                   String query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias] set nombre = '" + nombre + "',correo ='" + correo + "'  WHERE ANL_CODE = '" + ANL_CODE.Text + "'";
                                   SqlCommand cmd = new SqlCommand(query, connection);
                                   cmd.ExecuteNonQuery();
                               }
                               else
                               {
                                   String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[__iglesias] (ANL_CODE, nombre,correo) VALUES('"+ANL_CODE.Text+"','" + nombre + "','" + correo + "')";
                                   SqlCommand cmd = new SqlCommand(query, connection);
                                   cmd.ExecuteNonQuery();
                               }
                          }
                                    
                       

                        
                        leerDatos();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
