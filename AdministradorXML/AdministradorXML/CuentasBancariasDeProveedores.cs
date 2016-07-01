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
    public partial class CuentasBancariasDeProveedores : Form
    {
        public List<Dictionary<string, object>> listaCuentasBancarias { get; set; }
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.ContextMenu contextMenu2;
        public String connStringSun { get; set; }
        public int idProveedorGlobal { get; set; }
        public CuentasBancariasDeProveedores()
        {
            InitializeComponent();
            this.connStringSun = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
        }
        private void rellena()
        {
            String RFC = rfcText.Text;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "SELECT b.nombreCorto, c.cuentaBancaria,p.idProveedor FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] p INNER JOIN  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[cuentasBancarias] c on c.idProveedor = p.idProveedor INNER JOIN  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[bancos] b on b.clave = c.banco WHERE p.rfc = '" + RFC + "'";
                    listaCuentasBancarias = new List<Dictionary<string, object>>();
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String nombreCorto = reader.GetString(0);
                                String cuentasBancaria = reader.GetString(1);
                                idProveedorGlobal = reader.GetInt32(2);
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("nombreCorto", nombreCorto);
                                dictionary.Add("cuentasBancaria", cuentasBancaria);
                                listaCuentasBancarias.Add(dictionary);
                            }//while
                        }
                        cuentasBancariasList.Clear();
                        cuentasBancariasList.View = View.Details;
                        cuentasBancariasList.GridLines = true;
                        cuentasBancariasList.FullRowSelect = true;
                        cuentasBancariasList.Columns.Add("Banco", 180);
                        cuentasBancariasList.Columns.Add("Cuenta Bancaria", 180);
                        foreach (Dictionary<string, object> dic in listaCuentasBancarias)
                        {
                            if (dic.ContainsKey("nombreCorto"))
                            {
                                string[] arr = new string[3];
                                ListViewItem itm;
                                //add items to ListView
                                arr[0] = Convert.ToString(dic["nombreCorto"]);
                                arr[1] = Convert.ToString(dic["cuentasBancaria"]);
                                itm = new ListViewItem(arr);
                                cuentasBancariasList.Items.Add(itm);
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
        public void eliminarCuentaBancariaAsociada(object sender, EventArgs e)
        {
            String banco = cuentasBancariasList.SelectedItems[0].SubItems[0].Text;
            String cuenta = cuentasBancariasList.SelectedItems[0].SubItems[1].Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[cuentasBancarias] WHERE idProveedor = " + idProveedorGlobal + " AND cuentaBancaria = '" + cuenta + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    rellena();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        private void CuentasBancariasDeProveedores_Load(object sender, EventArgs e)
        {
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
           
            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1 });
            menuItem1.Index = 0;
            menuItem1.Text = "Eliminar";
            menuItem1.Click += eliminarCuentaBancariaAsociada;
            cuentasBancariasList.ContextMenu = contextMenu2;
            
            listaCuentasBancarias = new List<Dictionary<string, object>>();
            var source = new AutoCompleteStringCollection();
            var sourceRazonesSociales = new AutoCompleteStringCollection();

            List<String> rfcS = new List<String>();
            List<String> razonesSocialeS = new List<String>();

            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT rfc,razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] order by rfc asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                String razonSocial = reader.GetString(1);
                                rfcS.Add(rfc);
                                razonesSocialeS.Add(razonSocial);
                            }
                        }//if reader
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            source.AddRange(rfcS.ToArray());
            sourceRazonesSociales.AddRange(razonesSocialeS.ToArray());
            rfcText.AutoCompleteCustomSource = source;
            rfcText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            rfcText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            razonSocialText.AutoCompleteCustomSource = sourceRazonesSociales;
            razonSocialText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            razonSocialText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            rellena();
          
        }
        private void llenaRFC()
        {
            String razonSocial = razonSocialText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT TOP 1 rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] WHERE razonSocial = '" + razonSocial + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                rfcText.Text = rfc;
                            }
                            rellena();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void llenaRazonSocial()
        {
            String RFC = rfcText.Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT TOP 1 razonSocial FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] WHERE rfc = '" + RFC + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String razonSocial = reader.GetString(0);
                                razonSocialText.Text = razonSocial;
                            }
                            rellena();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void cuentasBancariasList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rfcText_TextChanged(object sender, EventArgs e)
        {
            llenaRazonSocial();
        }

        private void razonSocialText_TextChanged(object sender, EventArgs e)
        {
            llenaRFC();
        }

        private void anadirButton_Click(object sender, EventArgs e)
        {
            eligeBanco eligeBancoForm = new eligeBanco();
            eligeBancoForm.rfcGlobal = rfcText.Text.Trim();
            eligeBancoForm.ShowDialog();
            rellena();
        }
    }
}
