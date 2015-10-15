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
            sourceText.Text = Properties.Settings.Default.source;
            passwordText.Text = Properties.Settings.Default.password;
            presupuestoText.Text = Properties.Settings.Default.sunLibroPresupuesto;
            databaseFiscalText.Text = Properties.Settings.Default.databaseFiscal;
            unidadDeNegocioText.Text = Properties.Settings.Default.sunUnidadDeNegocio;
        }
        private void save()
        {
            Properties.Settings.Default.sunDatabase = sunDatabaseText.Text.Trim();
            Properties.Settings.Default.datasource = datasourceText.Text.Trim();
            Properties.Settings.Default.sunLibro = sunLibroText.Text.Trim();
            Properties.Settings.Default.user = userText.Text.Trim();
            Properties.Settings.Default.source = sourceText.Text.Trim();
            Properties.Settings.Default.sunLibroPresupuesto = presupuestoText.Text.Trim();
            Properties.Settings.Default.password = passwordText.Text;
            Properties.Settings.Default.databaseFiscal = databaseFiscalText.Text.Trim();
            Properties.Settings.Default.sunUnidadDeNegocio = unidadDeNegocioText.Text.Trim();
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
                        System.Windows.Forms.MessageBox.Show("Sin conexión", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Sin conexión", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Las variables que llenes aqui serán usadas en todo el sistema:\n hola", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
