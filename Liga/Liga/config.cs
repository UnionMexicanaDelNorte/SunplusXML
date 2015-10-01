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
namespace Liga
{
    public partial class config : Form
    {
        public config()
        {
            InitializeComponent();
        }

        private void config_Load(object sender, EventArgs e)
        {
            baseDeDatos.Text = Properties.Settings.Default.sunDatabase;
            datasource.Text = Properties.Settings.Default.sunDatasource;
            usuario.Text = Properties.Settings.Default.user;
            password.Text = Properties.Settings.Default.password;
            unidadDeNegocio.Text = Properties.Settings.Default.sunUnidadDeNegocio;
            libro.Text = Properties.Settings.Default.sunLibro;
            barraDeFiscal.Text = Properties.Settings.Default.databaseFiscal;
        }

        private void Grabar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sunDatabase = baseDeDatos.Text;
            Properties.Settings.Default.sunDatasource = datasource.Text;
            Properties.Settings.Default.user = usuario.Text;
            Properties.Settings.Default.password = password.Text ;
            Properties.Settings.Default.sunUnidadDeNegocio=unidadDeNegocio.Text;
            Properties.Settings.Default.sunLibro = libro.Text;
            Properties.Settings.Default.databaseFiscal = barraDeFiscal.Text;
            Properties.Settings.Default.Save();
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void probar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sunDatabase = baseDeDatos.Text;
            Properties.Settings.Default.sunDatasource = datasource.Text;
            Properties.Settings.Default.user = usuario.Text;
            Properties.Settings.Default.password = password.Text;
            Properties.Settings.Default.sunUnidadDeNegocio = unidadDeNegocio.Text;
            Properties.Settings.Default.sunLibro = libro.Text;
            Properties.Settings.Default.databaseFiscal = barraDeFiscal.Text;
            Properties.Settings.Default.Save();
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
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
