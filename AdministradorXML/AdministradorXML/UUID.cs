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
    public partial class UUID : Form
    {
        public UUID()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String UUID = textBox1.Text.Trim();
            if (UUID.Length != 36)
            {
                System.Windows.Forms.MessageBox.Show("El folio fiscal debe de tener 36 caracteres, contando los guiones, perdón.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT total, STATUS, fechaExpedicion FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + UUID + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(0)));
                                String STATUS = reader.GetString(1).ToString().Trim();
                                String fecha = Convert.ToString(reader.GetDateTime(2)).Substring(0, 10);
                                String sta = "";
                                if (STATUS.Equals("0"))
                                {
                                    sta = "GASTOS Cancelados";
                                }
                                if (STATUS.Equals("1"))
                                {
                                    sta = "GASTOS";
                                }
                                if (STATUS.Equals("2"))
                                {
                                    sta = "INGRESOS";
                                }
                                if (STATUS.Equals("3"))
                                {
                                    sta = "INGRESOS CANCELADOS";
                                }
                                label2.Text = " $" + String.Format("{0:n}", total) + " Fecha: " + fecha + " STATUS: " + sta;
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
                    String queryXML = "SELECT JRNAL_NO, JRNAL_LINE, BUNIT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '" + UUID + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                int diario = Convert.ToInt32(reader.GetInt32(0));
                                int linea = Convert.ToInt32(reader.GetInt32(1));
                                String BUNIT = reader.GetString(2).ToString().Trim();
                                label3.Text = " Diario: " + diario + " Linea: " + linea + " BUNIT: " + BUNIT;
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

        private void UUID_Load(object sender, EventArgs e)
        {
           

        }
    }
}
