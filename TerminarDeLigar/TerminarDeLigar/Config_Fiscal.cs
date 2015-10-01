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
namespace TerminarDeLigar
{
    public partial class Config_Fiscal : Form
    {
        public Config_Fiscal()
        {
            InitializeComponent();
        }

        private void Config_Fiscal_Load(object sender, EventArgs e)
        {
            sunplusDatabase.Text = Properties.Settings.Default.sunDatabase;
            dataSourceText.Text = Properties.Settings.Default.datasource;
            unidadDeNegociosText.Text = Properties.Settings.Default.sunUnidadDeNegocio;
            user.Text = Properties.Settings.Default.user;
            pass.Text = Properties.Settings.Default.password;
            libroText.Text = Properties.Settings.Default.sunLibro;
            fiscalDatabase.Text = Properties.Settings.Default.databaseFiscal;
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void save()
        {
            Properties.Settings.Default.sunDatabase = sunplusDatabase.Text;
            Properties.Settings.Default.datasource = dataSourceText.Text;
            Properties.Settings.Default.sunUnidadDeNegocio = unidadDeNegociosText.Text;
            Properties.Settings.Default.user = user.Text;
            Properties.Settings.Default.password = pass.Text;
            Properties.Settings.Default.sunLibro = libroText.Text;
            Properties.Settings.Default.databaseFiscal = fiscalDatabase.Text;
            Properties.Settings.Default.Save();
        }
        private void grabarButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void revisarConexionButton_Click(object sender, EventArgs e)
        {
            save();
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
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
