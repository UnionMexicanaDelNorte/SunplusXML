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
    public partial class misDatos : Form
    {
        public bool existeRegistro { get; set; }
        public String rfcGlobal { get; set; }
        public misDatos()
        {
            InitializeComponent();
        }

        private void misDatos_Load(object sender, EventArgs e)
        {
             this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
             existeRegistro = false;
             String connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "SELECT rfc,razonSocial ,calle ,ne ,ni ,colonia ,ciudad ,estado ,cp FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[misDatos]";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                existeRegistro = true;
                                String rfc = reader.GetString(0);
                                rfcGlobal = rfc;
                                String razonSocial = reader.GetString(1);
                                String calle = reader.GetString(2);
                                String ne = reader.GetString(3);
                                String ni = reader.GetString(4);
                                String colonia = reader.GetString(5);
                                String ciudad = reader.GetString(6);
                                String estado = reader.GetString(7);
                                String cp = reader.GetString(8);
                                rfcText.Text = rfc;
                                razonSocialText.Text = razonSocial;
                                calleText.Text = calle;
                                neText.Text = ne;
                                niText.Text = ni;
                                coloniaText.Text = colonia;
                                cdText.Text = ciudad;
                                esText.Text = estado;
                                cpText.Text = cp;
                            }
                        }
                        else
                        {
                            existeRegistro = false;
                        }
                    }
                }
              }
            catch(Exception ex)
            {
                ex.ToString();
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
          
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            if(rfcText.Text.Trim().Equals("") || razonSocialText.Text.Trim().Equals("") || calleText.Text.Trim().Equals("") || neText.Text.Trim().Equals("") || niText.Text.Trim().Equals("") || coloniaText.Text.Trim().Equals("") || cdText.Text.Trim().Equals("") || esText.Text.Trim().Equals("")|| cpText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero llena todos los datos", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                String query = "";
                if (existeRegistro)
                {
                    query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[misDatos] SET rfc = '" + rfcText.Text.Trim() + "' , razonSocial = '" + razonSocialText.Text.Trim() + "',calle = '" + calleText.Text.Trim() + "',ne = '" + neText.Text.Trim() + "',ni = '" + niText.Text.Trim() + "',colonia ='" + coloniaText.Text.Trim() + "',ciudad = '" + cdText.Text.Trim() + "',estado = '" + esText.Text.Trim() + "',cp = '" + cpText.Text.Trim() + "'";
                }
                else
                {
                    query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[misDatos] (rfc,razonSocial,calle,ne,ni,colonia,ciudad,estado,cp) VALUES ('" + rfcText.Text.Trim() + "', '" + razonSocialText.Text.Trim() + "', '" + calleText.Text.Trim() + "'  , '" + neText.Text.Trim() + "' , '" + niText.Text.Trim() + "' , '" + coloniaText.Text.Trim() + "', '" + cdText.Text.Trim() + "' , '" + esText.Text.Trim() + "' , '" + cpText.Text.Trim() + "')";
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        String query2 = "";
                        if(existeRegistro)
                        {
                            query2 = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] SET rfc = '" + rfcText.Text.Trim() + "',razonSocial = '" + razonSocialText.Text.Trim() + "' WHERE rfc = '" + rfcGlobal + "'";
                        }
                        else
                        {
                            query2 = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] (rfc,razonSocial) VALUES ('" + rfcText.Text.Trim() + "', '" + razonSocialText.Text.Trim() + "')";
                        }
                        SqlCommand cmd1 = new SqlCommand(query2, connection);
                        cmd1.ExecuteNonQuery();
                        Properties.Settings.Default.rfcGlobal = rfcText.Text.Trim();
                        Properties.Settings.Default.Save();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            } 
        }
    }
}
