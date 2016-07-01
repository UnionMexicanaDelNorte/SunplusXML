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
    public partial class TotalesCFDIxAno : Form
    {
        public TotalesCFDIxAno()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String anio = textBox1.Text.Trim();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos SAT
                    //GROUP BY rfc,razonSocial order by rfc asc
                    String queryXML = "SELECT SUM(total) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '" + anio + "' AND STATUS = '1'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                label1.Text = " Gastos: $" + String.Format("{0:n}", total);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //egresos SAT
                    //GROUP BY rfc,razonSocial order by rfc asc
                    String queryXML = "SELECT SUM(total) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '" + anio + "' AND STATUS = '2'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                label2.Text = " Ingresos: $" + String.Format("{0:n}", total);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //canceladas egresos SAT
                    //GROUP BY rfc,razonSocial order by rfc asc
                    String queryXML = "SELECT SUM(total) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '" + anio + "' AND STATUS = '0'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                label3.Text = " Canceladas Gastos: $" + String.Format("{0:n}", total);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //canceladas ingresos SAT
                    //GROUP BY rfc,razonSocial order by rfc asc
                    String queryXML = "SELECT SUM(total) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '" + anio + "' AND STATUS = '3'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                label4.Text =   " Canceladas Ingresos: $" + String.Format("{0:n}", total);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //label1.Text = cadena;
        }
    }
}
