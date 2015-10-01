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
    public partial class eligeBanco : Form
    {
        public String rfcGlobal { get; set; }
        private class Item
        {
            public string Name;
            public int Value;
            public string Extra;
            public Item(string name, int value, String extra)
            {
                Name = name; Value = value;
                Extra = extra;
            }
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }
        public eligeBanco()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cuentaBancariaText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe tu cuenta bancaria", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                String queryCheck = "SELECT idProveedor FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] WHERE rfc = '" + rfcGlobal + "'";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                         SqlCommand cmdCheck = new SqlCommand(queryCheck, connection);
                         SqlDataReader reader = cmdCheck.ExecuteReader();
                         if (reader.HasRows)
                         {
                             if(reader.Read())
                             {
                                 int idProveedor = reader.GetInt32(0);
                                 Item itm = (Item)bancoCombo.SelectedItem;
                                 String clave = itm.Value.ToString();
                                 String cuenta = cuentaBancariaText.Text;
                                 String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[cuentasBancarias] (banco,idProveedor,cuentaBancaria) VALUES ('" + clave + "', " + idProveedor + ", '" + cuenta + "')";
                                 SqlCommand cmd = new SqlCommand(query, connection);
                                 cmd.ExecuteNonQuery();
                                 this.Close();
                             }
                         }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    //  Logs.Escribir("Error en download complete : " + ex.ToString());
                }
            }
        }

        private void eligeBanco_Load(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT clave, nombreCorto FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[bancos]";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int clave = Convert.ToInt32 (reader.GetString(0));
                            String nombreCorto = reader.GetString(1);
                            bancoCombo.Items.Add(new Item(nombreCorto, clave));
                        }
                        bancoCombo.SelectedIndex = 0;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen bancos en la base de datos, favor de verificar.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
