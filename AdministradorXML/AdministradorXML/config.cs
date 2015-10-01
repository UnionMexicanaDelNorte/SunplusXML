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
namespace AdministradorXML
{
    public partial class config : Form
    {
        public String connString { get; set; }
     
        public config()
        {
            InitializeComponent();
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void config_Load(object sender, EventArgs e)
        {
            this.connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            sunDatabaseText.Text = Properties.Settings.Default.sunDatabase;
            datasourceText.Text = Properties.Settings.Default.datasource;
            sunLibroText.Text = Properties.Settings.Default.sunLibro;
            userText.Text = Properties.Settings.Default.user;
            passwordText.Text = Properties.Settings.Default.password;
            databaseFiscalText.Text = Properties.Settings.Default.databaseFiscal;
            unidadDeNegocioText.Text = Properties.Settings.Default.sunUnidadDeNegocio;
        }
        private void save()
        {
            Properties.Settings.Default.sunDatabase = sunDatabaseText.Text;
            Properties.Settings.Default.datasource = datasourceText.Text;
            Properties.Settings.Default.sunLibro = sunLibroText.Text;
            Properties.Settings.Default.user = userText.Text;
            Properties.Settings.Default.password = passwordText.Text;
            Properties.Settings.Default.databaseFiscal = databaseFiscalText.Text;
            Properties.Settings.Default.sunUnidadDeNegocio = unidadDeNegocioText.Text;
            Properties.Settings.Default.Save();
        }
        private void grabarButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void revisarConexionButton_Click(object sender, EventArgs e)
        {
            save();
            this.connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            String queryCheck = "USE [" + Properties.Settings.Default.sunDatabase + "] SELECT name FROM sys.tables";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    SqlCommand cmdCheck = new SqlCommand(queryCheck, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        System.Windows.Forms.MessageBox.Show("Conexión Establecida satisfactoriamente", "SunPlusXML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Sin conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Sin conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
