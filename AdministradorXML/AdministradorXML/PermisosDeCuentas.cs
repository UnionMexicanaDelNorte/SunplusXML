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
    public partial class PermisosDeCuentas : Form
    {
        public List<Dictionary<string, object>> listaCuentasSinDarPermisos { get; set; }
        public List<Dictionary<string, object>> listaCuentasConPermisos { get; set; }
        public int tipoGlobal { get; set; }
      
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
        public PermisosDeCuentas()
        {
            InitializeComponent();
        }
        private void llenaTablaDeTodasLasCuentasExceptoConDelTipoDeContabilidad(int tipo)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            listaCuentasConPermisos.Clear();
            listaCuentasSinDarPermisos.Clear();
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT ACNT_CODE, DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_ACNT]";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String ACNT_CODE = reader.GetString(0);
                            String DESCR = reader.GetString(1);
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            dictionary.Add("ACNT_CODE", ACNT_CODE);
                            dictionary.Add("DESCR", DESCR);
                            String queryFISCAL = "SELECT ACNT_CODE,DESCR FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE unidadDeNegocio = '"+Login.unidadDeNegocioGlobal+"' AND tipoDeContabilidad = "+tipo+" AND ACNT_CODE = '"+ACNT_CODE+"'";
                            using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                            {
                                SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                if (readerFISCAL.HasRows)
                                {
                                    listaCuentasConPermisos.Add(dictionary);
                                }
                                else
                                {
                                    listaCuentasSinDarPermisos.Add(dictionary);
                                }
                            }
                        }
                        todasLasCuentasList.Clear();
                        todasLasCuentasList.View = View.Details;
                        todasLasCuentasList.GridLines = true;
                        todasLasCuentasList.FullRowSelect = true;
                        todasLasCuentasList.Columns.Add("Cuenta", 90);
                        todasLasCuentasList.Columns.Add("Descripción", 200);
                        cuentasConPermisosList.Clear();
                        cuentasConPermisosList.View = View.Details;
                        cuentasConPermisosList.GridLines = true;
                        cuentasConPermisosList.FullRowSelect = true;
                        cuentasConPermisosList.Columns.Add("Cuenta", 90);
                        cuentasConPermisosList.Columns.Add("Descripción", 200);
                        foreach (Dictionary<string, object> dic in listaCuentasSinDarPermisos)
                        {
                            if (dic.ContainsKey("ACNT_CODE"))
                            {
                                string[] arr = new string[2];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["ACNT_CODE"]);
                                arr[1] = Convert.ToString(dic["DESCR"]);
                                itm2 = new ListViewItem(arr);
                                todasLasCuentasList.Items.Add(itm2);
                            }
                        }
                        foreach (Dictionary<string, object> dic in listaCuentasConPermisos)
                        {
                            if (dic.ContainsKey("ACNT_CODE"))
                            {
                                string[] arr = new string[2];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["ACNT_CODE"]);
                                arr[1] = Convert.ToString(dic["DESCR"]);
                                itm2 = new ListViewItem(arr);
                                cuentasConPermisosList.Items.Add(itm2);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Cuentas para la unidad de negocios: "+Login.unidadDeNegocioGlobal+", favor de verificar.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex1)
            {
                System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }
        private void llenaConTipoDeContabilidad(int tipo)
        {
            tipoGlobal = tipo;
            llenaTablaDeTodasLasCuentasExceptoConDelTipoDeContabilidad(tipo);
        }
        private void PermisosDeCuentas_Load(object sender, EventArgs e)
        {
            listaCuentasSinDarPermisos = new List<Dictionary<string, object>>();
            listaCuentasConPermisos = new List<Dictionary<string, object>>();
          
            tipoDePermisosCombo.Items.Add(new Item("Gastos", 1));
            tipoDePermisosCombo.Items.Add(new Item("Ingresos", 2));
            tipoDePermisosCombo.Items.Add(new Item("Balanza", 3));
       
          //  tipoDePermisosCombo.Items.Add(new Item("Cheques y transferencias", 3));
            tipoDePermisosCombo.SelectedIndex = 0;
            llenaConTipoDeContabilidad(1);//Gastos
        }

        private void tipoDePermisosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)tipoDePermisosCombo.SelectedItem;
            llenaConTipoDeContabilidad(itm.Value);
        }

        private void darPermisoAUnoButton_Click(object sender, EventArgs e)
        {
            int cuantos = todasLasCuentasList.SelectedItems.Count;
            if(cuantos>0)
            {
                String connStringFiscal = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringFiscal))
                    {
                        connection.Open();
                        foreach(ListViewItem cuenta in todasLasCuentasList.SelectedItems)
                        {
                            String ACNT_CODE = cuenta.SubItems[0].Text.Trim();
                            String DESCR = cuenta.SubItems[1].Text.Trim();
                            String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] (ACNT_CODE,DESCR,tipoDeContabilidad,unidadDeNegocio) VALUES ('" + ACNT_CODE + "', '"+DESCR+"', " + tipoGlobal + ", '" + Login.unidadDeNegocioGlobal + "')";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            ListViewItem clon = (ListViewItem)cuenta.Clone();
                            cuentasConPermisosList.Items.Add(clon);
                            todasLasCuentasList.Items.Remove(cuenta);
                        }
                     }
                }
                catch (Exception ex1)
                {
                     System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Primero selecciona 1 o varias cuentas", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void quitarPermisoAUnoButton_Click(object sender, EventArgs e)
        {
            int cuantos = cuentasConPermisosList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String connStringFiscal = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringFiscal))
                    {
                        connection.Open();
                        foreach (ListViewItem cuenta in cuentasConPermisosList.SelectedItems)
                        {
                            String ACNT_CODE = cuenta.SubItems[0].Text.Trim();
                            String DESCR = cuenta.SubItems[1].Text.Trim();
                            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE ACNT_CODE ='" + ACNT_CODE + "' AND tipoDeContabilidad = " + tipoGlobal + " AND unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "'";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            ListViewItem clon = (ListViewItem)cuenta.Clone();
                            todasLasCuentasList.Items.Add(clon);
                            cuentasConPermisosList.Items.Remove(cuenta);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Primero selecciona 1 o varias cuentas de las cuentas con permiso", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void quitarPermisoATodosButton_Click(object sender, EventArgs e)
        {
           
                String connStringFiscal = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connStringFiscal))
                    {
                        connection.Open();
                        foreach (ListViewItem cuenta in cuentasConPermisosList.Items)
                        {
                            String ACNT_CODE = cuenta.SubItems[0].Text.Trim();
                            String DESCR = cuenta.SubItems[1].Text.Trim();
                            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[permisos_cuentas] WHERE ACNT_CODE ='" + ACNT_CODE + "' AND tipoDeContabilidad = " + tipoGlobal + " AND unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "'";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            ListViewItem clon = (ListViewItem)cuenta.Clone();
                            todasLasCuentasList.Items.Add(clon);
                            cuentasConPermisosList.Items.Remove(cuenta);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }        
        }
    }
}
