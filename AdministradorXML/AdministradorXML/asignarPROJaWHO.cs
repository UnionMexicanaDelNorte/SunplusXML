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
    public partial class asignarPROJaWHO : Form
    {
        System.Windows.Forms.MenuItem menuItem2;

        System.Windows.Forms.ContextMenu contextMenu2;
        public List<Dictionary<string, object>> listaFinal { get; set; }

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
                // Generates the text shown in the combo box
                return Name;
            }
        }
        private void BorrarRelacion(object sender, EventArgs e)
        {
            String WHO = relacionList.SelectedItems[0].SubItems[0].Text.Trim();
            String PROJ = relacionList.SelectedItems[0].SubItems[1].Text.Trim();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_PROJyWHO] WHERE WHO =  '" + WHO + "' AND PROJ = '" + PROJ + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    actualiza();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public asignarPROJaWHO()
        {
            InitializeComponent();
        }

        private void asignarPROJaWHO_Load(object sender, EventArgs e)
        {
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2 });
            menuItem2.Index = 0;
            menuItem2.Text = "Borrar relación";
            menuItem2.Click += BorrarRelacion;
            relacionList.ContextMenu = contextMenu2;
            listaFinal = new List<Dictionary<string, object>>();
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

            String queryCuentas = "SELECT ANL_CODE,LOOKUP FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ANL_CODE] WHERE ANL_CAT_ID= '07' AND SUBSTRING( ANL_CODE,1,2) = 'ER' order by ANL_CODE asc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentas, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int empiezo1 = 1;
                        while (reader.Read())
                        {
                            String ACNT_CODE = reader.GetString(0);
                            personaCombo.Items.Add(new Item(ACNT_CODE, empiezo1, ACNT_CODE));
                            empiezo1++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            String queryDP = "SELECT ANL_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ANL_CODE] WHERE ANL_CAT_ID= '09' order by ANL_CODE asc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryDP, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int empiezo2 = 1;
                        while (reader.Read())
                        {
                            String DP = reader.GetString(0);
                            proyectoCombo.Items.Add(new Item(DP, empiezo2, DP));
                            empiezo2++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            personaCombo.SelectedIndex = 0;
            proyectoCombo.SelectedIndex = 0;
            actualiza();
        }


        private void actualiza()
        {

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            relacionList.Clear();
            listaFinal.Clear();


            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //conceptos 
                    String queryXML = "SELECT WHO,PROJ FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_PROJyWHO] order by WHO asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String WHO = reader.GetString(0).Trim();
                                String PROJ = reader.GetString(1).ToString().Trim();


                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("WHO", WHO);
                                dictionary.Add("PROJ", PROJ);
                                listaFinal.Add(dictionary);
                            }



                            relacionList.View = View.Details;
                            relacionList.GridLines = true;
                            relacionList.FullRowSelect = true;
                            relacionList.Columns.Add("WHO", 150);
                            relacionList.Columns.Add("PROJ", 150);


                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("WHO"))
                                {
                                    string[] arr = new string[3];
                                    ListViewItem itm3;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["WHO"]);
                                    arr[1] = Convert.ToString(dic["PROJ"]);
                                    itm3 = new ListViewItem(arr);
                                    relacionList.Items.Add(itm3);
                                }
                            }
                        }//if reader
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void asignarButton_Click(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    Item itm = (Item)personaCombo.SelectedItem;
                    String WHO = itm.Extra.ToString();


                    Item itm1 = (Item)proyectoCombo.SelectedItem;
                    String PROJ = itm1.Name.ToString();

                    String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_PROJyWHO] (WHO,PROJ) VALUES ('" + WHO + "', '" + PROJ + "')";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    actualiza();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
