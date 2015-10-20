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
    public partial class confirmaNumeroDeDiario : Form
    {
        public String carpetaGlobal { get; set; }
    
        public confirmaNumeroDeDiario(String carpeta)
        {
            InitializeComponent();
            carpetaGlobal = carpeta;
        }

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            if(!diarioText.Text.Trim().Equals(""))
            {
                String diario = diarioText.Text.Trim();
                String query1 = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] set JRNAL_NO = " + diario + " WHERE JRNAL_SOURCE = '" + Login.sourceGlobal + "' AND JRNAL_NO = -1";
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query1, connection);
                        cmd.ExecuteNonQuery();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void confirmaNumeroDeDiario_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(carpetaGlobal);
        
        }
    }
}
