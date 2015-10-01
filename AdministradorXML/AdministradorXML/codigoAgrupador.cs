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
    public partial class codigoAgrupador : Form
    {
        public List<Dictionary<string, object>> listaSunplus { get; set; }
        public List<Dictionary<string, object>> listaSAT { get; set; }
        public List<Dictionary<string, object>> listaEnlazados { get; set; }
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.ContextMenu contextMenu2;

        public codigoAgrupador()
        {
            InitializeComponent();
        }
        public void rellena()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            agrupadoresList.Clear();
            listaSAT.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT nivel, codigoAgrupador, nombre FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[codigoAgrupadorSAT] WHERE nivel != '0'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String nivel = Convert.ToString(reader.GetInt32(0)).Trim();
                                String codigoAgrupador = reader.GetString(1).Trim();
                                String nombre = reader.GetString(2).Trim();
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("nivel", nivel);
                                dictionary.Add("codigoAgrupador", codigoAgrupador);
                                dictionary.Add("nombre", nombre);
                                listaSAT.Add(dictionary);
                            }
                            agrupadoresList.View = View.Details;
                            agrupadoresList.GridLines = true;
                            agrupadoresList.FullRowSelect = true;
                            agrupadoresList.Columns.Add("Nivel", 80);
                            agrupadoresList.Columns.Add("Código Agrupador", 200);
                            agrupadoresList.Columns.Add("Descripción", 300);
                            foreach (Dictionary<string, object> dic in listaSAT)
                            {
                                if (dic.ContainsKey("nivel"))
                                {
                                    string[] arr = new string[4];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["nivel"]);
                                    arr[1] = Convert.ToString(dic["codigoAgrupador"]);
                                    arr[2] = Convert.ToString(dic["nombre"]);
                                    itm2 = new ListViewItem(arr);
                                    agrupadoresList.Items.Add(itm2);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            enlazadosList.Clear();
            sunplusList.Clear();
            listaSunplus.Clear();
            listaEnlazados.Clear();
            //aqui empiezan las cuentas de sunplus y las que ya estan enlazadas !!
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryPeriodos = "SELECT a.ACNT_CODE, a.DESCR, b.ANL_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ACNT] a INNER JOIN  [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ACNT_ANL_CAT] b on b.ACNT_CODE = a.ACNT_CODE WHERE b.ANL_CAT_ID = 14 order by a.ACNT_CODE asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String ACNT_CODE = reader.GetString(0).Trim();
                                String DESCR = reader.GetString(1).Trim();
                                String ENTER_ANL_10 = reader.GetString(2).Trim();
                                //si esta enlazado de esa cuenta
                                String queryFISCAL = "SELECT codigoAgrupador,ACNT_CODE, BUNIT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[codigoAgrupadorCuentaSunplus] WHERE ACNT_CODE = '"+ACNT_CODE+"' AND BUNIT = '"+Properties.Settings.Default.sunUnidadDeNegocio+"'";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            String codigoAgrupador = readerFISCAL.GetString(0);
                                            String ACNT_CODE2 = readerFISCAL.GetString(1);
                                            String BUNIT = readerFISCAL.GetString(2);
                                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                            dictionary.Add("codigoAgrupador", codigoAgrupador);
                                            dictionary.Add("ACNT_CODE2", ACNT_CODE2);
                                            dictionary.Add("BUNIT", BUNIT);
                                            listaEnlazados.Add(dictionary);
                                        }
                                    }
                                    else
                                    {
                                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                        dictionary.Add("ACNT_CODE", ACNT_CODE);
                                        dictionary.Add("DESCR", DESCR);
                                        dictionary.Add("ENTER_ANL_10", ENTER_ANL_10);
                                        listaSunplus.Add(dictionary);
                                    }
                                    
                                    
                                }//using
                            }//while

                            enlazadosList.Clear();
                            enlazadosList.View = View.Details;
                            enlazadosList.GridLines = true;
                            enlazadosList.FullRowSelect = true;
                            enlazadosList.Columns.Add("Código Agrupador", 200);
                            enlazadosList.Columns.Add("Cuenta Sunplus", 200);
                            enlazadosList.Columns.Add("Unidad", 90);



                            foreach (Dictionary<string, object> dic in listaEnlazados)
                            {
                                if (dic.ContainsKey("codigoAgrupador"))
                                {
                                    string[] arr = new string[4];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["codigoAgrupador"]);
                                    arr[1] = Convert.ToString(dic["ACNT_CODE2"]);
                                    arr[2] = Convert.ToString(dic["BUNIT"]);
                                 
                                    itm = new ListViewItem(arr);
                                    enlazadosList.Items.Add(itm);
                                }
                            }

                            sunplusList.Clear();
                            sunplusList.View = View.Details;
                            sunplusList.GridLines = true;
                            sunplusList.FullRowSelect = true;
                            sunplusList.Columns.Add("Cuenta", 90);
                            sunplusList.Columns.Add("Descripción", 200);
                            sunplusList.Columns.Add("Naturaleza", 100);

                            foreach (Dictionary<string, object> dic in listaSunplus)
                            {
                                if (dic.ContainsKey("ACNT_CODE"))
                                {
                                    string[] arr = new string[4];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["ACNT_CODE"]);
                                    arr[1] = Convert.ToString(dic["DESCR"]);
                                    arr[2] = Convert.ToString(dic["ENTER_ANL_10"]);
                                    itm = new ListViewItem(arr);
                                    sunplusList.Items.Add(itm);
                                }
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
        public void DesligarFactura(object sender, EventArgs e)
        {
            String ACNT_CODE = enlazadosList.SelectedItems[0].SubItems[1].Text;
            String BUNIT = enlazadosList.SelectedItems[0].SubItems[2].Text;
            String codigoAgrupador = enlazadosList.SelectedItems[0].SubItems[0].Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[codigoAgrupadorCuentaSunplus] WHERE codigoAgrupador = '" + codigoAgrupador + "' AND BUNIT = '" + BUNIT + "' AND ACNT_CODE = '" + ACNT_CODE + "'";
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
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void codigoAgrupador_Load(object sender, EventArgs e)
        {
            int height = Screen.PrimaryScreen.Bounds.Height-70;
            int width = Screen.PrimaryScreen.Bounds.Width;

            sunplusList.Location = new Point(0, 0);
            sunplusList.Size = new Size(width / 3, height );

            agrupadoresList.Location = new Point(width / 3, 0);
            agrupadoresList.Size = new Size(width / 3, height-70);

            enlazadosList.Location = new Point(2*(width / 3), 0);
            enlazadosList.Size = new Size(width / 3, height);
            ligarButton.Location = new Point(width / 2, height-60);
          
           // this.Size = new Size(900, 900);
            listaSAT = new List<Dictionary<string, object>>();
            listaSunplus = new List<Dictionary<string, object>>();
            listaEnlazados = new List<Dictionary<string, object>>();
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2 });

            menuItem2.Index = 0;
            menuItem2.Text = "Desligar cuenta";
            menuItem2.Click += DesligarFactura;
            enlazadosList.ContextMenu = contextMenu2;
            rellena();
        }

        private void ligarButton_Click(object sender, EventArgs e)
        {
            int cuantos = sunplusList.SelectedItems.Count;
            if (cuantos > 0)
            {
                if(agrupadoresList.SelectedItems.Count>0)
                {
                    String connStringFiscal = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connStringFiscal))
                        {
                            connection.Open();
                            foreach(ListViewItem listItem in sunplusList.SelectedItems)
                            {
                                String ACNT_CODE = listItem.SubItems[0].Text.Trim();
                                String codigoAgrupador = agrupadoresList.SelectedItems[0].SubItems[1].Text.Trim();
                                String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[codigoAgrupadorCuentaSunplus] (codigoAgrupador, ACNT_CODE,BUNIT) VALUES ('" + codigoAgrupador + "', '" + ACNT_CODE + "','" + Properties.Settings.Default.sunUnidadDeNegocio + "')";
                                SqlCommand cmd = new SqlCommand(query, connection);
                                cmd.ExecuteNonQuery();
                            }
                            rellena();
                            //sunplusList.Items.Remove(sunplusList.SelectedItems[0]);
                        }
                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Primero selecciona una cuenta agrupadora", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Primero selecciona una cuenta de sunplus", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
