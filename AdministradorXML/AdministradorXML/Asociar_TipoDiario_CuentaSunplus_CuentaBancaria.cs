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
    public partial class Asociar_TipoDiario_CuentaSunplus_CuentaBancaria : Form
    {
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.ContextMenu contextMenu2;
        public List<Dictionary<string, object>> listaTipoDeDiario { get; set; }
        public List<Dictionary<string, object>> listaCuentasSunplus { get; set; }
        public List<Dictionary<string, object>> listaCuentasBancarias { get; set; }
        public List<Dictionary<string, object>> listaAsociaciones { get; set; }
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

        public int tipoGlobal { get; set; }
      
        public Asociar_TipoDiario_CuentaSunplus_CuentaBancaria()
        {
            InitializeComponent();
        }
        private void llenaTodasLasTablasConDelTipoDeContabilidad(int tipo)
        {
            tipoGlobal = tipo;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            listaTipoDeDiario.Clear();
            listaCuentasSunplus.Clear();
            listaCuentasBancarias.Clear();
            listaAsociaciones.Clear();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String tipoS = "";
            if(tipoGlobal==1)
            {
                tipoS = "Cheques";
            }
            else
            {
                tipoS = "Transferencias";
            }
            // primero obtengo la lista de lo que ya hay, para no agregarlo a las 3 listas
            String queryLoQueTengo = "SELECT p.JRNAL_TYPE, p.ACNT_CODE, b.nombreCorto, p.cuentaBancaria FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[predefinidosDeChequeYTransferencia] p INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[bancos] b on b.clave = p.banco WHERE p.BUNIT = '"+Properties.Settings.Default.sunUnidadDeNegocio+"' AND p.tipo = "+tipoGlobal;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryLoQueTengo, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String JRNAL_TYPE = reader.GetString(0);
                            String ACNT_CODE = reader.GetString(1);
                            String nombreCorto = reader.GetString(2);
                            String cuentaBancaria = reader.GetString(3);
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            dictionary.Add("tipo", tipoS);
                            dictionary.Add("unidadDeNegocio", Properties.Settings.Default.sunUnidadDeNegocio);
                            dictionary.Add("JRNAL_TYPE", JRNAL_TYPE);
                            dictionary.Add("ACNT_CODE", ACNT_CODE);
                            dictionary.Add("nombreCorto", nombreCorto);
                            dictionary.Add("cuentaBancaria", cuentaBancaria);
                            listaAsociaciones.Add(dictionary);
                        }
                        asociacionesList.Clear();
                        asociacionesList.View = View.Details;
                        asociacionesList.GridLines = true;
                        asociacionesList.FullRowSelect = true;
                        asociacionesList.Columns.Add("Tipo", 100);
                        asociacionesList.Columns.Add("Unidad de negocio", 200);
                        asociacionesList.Columns.Add("Tipo de diario", 200);
                        asociacionesList.Columns.Add("Cuenta sunplus", 200);
                        asociacionesList.Columns.Add("Banco", 140);
                        asociacionesList.Columns.Add("Cuenta Bancaria", 200);
                        foreach (Dictionary<string, object> dic in listaAsociaciones)
                        {
                            if (dic.ContainsKey("ACNT_CODE"))
                            {
                                string[] arr = new string[7];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["tipo"]);
                                arr[1] = Convert.ToString(dic["unidadDeNegocio"]);
                                arr[2] = Convert.ToString(dic["JRNAL_TYPE"]);
                                arr[3] = Convert.ToString(dic["ACNT_CODE"]);
                                arr[4] = Convert.ToString(dic["nombreCorto"]);
                                arr[5] = Convert.ToString(dic["cuentaBancaria"]);
                                itm2 = new ListViewItem(arr);
                                asociacionesList.Items.Add(itm2);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }



            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            //para tipos de diarios
            String queryTiposDediarios = "SELECT JOURNAL_TYPE, JOURNAL_NAME FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_JNL_DEFN]";
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryTiposDediarios, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String JOURNAL_TYPE = reader.GetString(0);
                            String JOURNAL_NAME = reader.GetString(1);
                            bool metelo = true; ;
                            foreach (Dictionary<string, object> dic in listaAsociaciones)
                            {
                                if (Convert.ToString(dic["JRNAL_TYPE"]).Equals(JOURNAL_TYPE))
                                {
                                    metelo = false;
                                    break;
                                }
                            }
                            if (metelo)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("JOURNAL_TYPE", JOURNAL_TYPE);
                                dictionary.Add("JOURNAL_NAME", JOURNAL_NAME);
                                listaTipoDeDiario.Add(dictionary);
                            }
                        }
                        tipoDeDiarioList.Clear();
                        tipoDeDiarioList.View = View.Details;
                        tipoDeDiarioList.GridLines = true;
                        tipoDeDiarioList.FullRowSelect = true;
                        tipoDeDiarioList.Columns.Add("Código", 90);
                        tipoDeDiarioList.Columns.Add("Descripción", 200);
                        foreach (Dictionary<string, object> dic in listaTipoDeDiario)
                        {
                            if (dic.ContainsKey("JOURNAL_TYPE"))
                            {
                                string[] arr = new string[2];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["JOURNAL_TYPE"]);
                                arr[1] = Convert.ToString(dic["JOURNAL_NAME"]);
                                itm2 = new ListViewItem(arr);
                                tipoDeDiarioList.Items.Add(itm2);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen tipo de diarios para la unidad de negocios: " + Properties.Settings.Default.sunUnidadDeNegocio + ", favor de verificar.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex1)
            {
                System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //para cuentas sunplus
            String queryPeriodos = "SELECT ACNT_CODE, DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ACNT]";
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
                            bool metelo = true; ;
                            foreach (Dictionary<string, object> dic in listaAsociaciones)
                            {
                                if(Convert.ToString(dic["ACNT_CODE"]).Equals(ACNT_CODE))
                                {
                                    metelo = false;
                                    break;
                                }
                            }
                            if(metelo)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("ACNT_CODE", ACNT_CODE);
                                dictionary.Add("DESCR", DESCR);
                                listaCuentasSunplus.Add(dictionary);
                            }
                        }
                        cuentasSunplusList.Clear();
                        cuentasSunplusList.View = View.Details;
                        cuentasSunplusList.GridLines = true;
                        cuentasSunplusList.FullRowSelect = true;
                        cuentasSunplusList.Columns.Add("Cuenta", 90);
                        cuentasSunplusList.Columns.Add("Descripción", 200);
                        foreach (Dictionary<string, object> dic in listaCuentasSunplus)
                        {
                            if (dic.ContainsKey("ACNT_CODE"))
                            {
                                string[] arr = new string[2];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["ACNT_CODE"]);
                                arr[1] = Convert.ToString(dic["DESCR"]);
                                itm2 = new ListViewItem(arr);
                                cuentasSunplusList.Items.Add(itm2);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Cuentas para la unidad de negocios: " + Properties.Settings.Default.sunUnidadDeNegocio + ", favor de verificar.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex1)
            {
                System.Windows.Forms.MessageBox.Show(ex1.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //para cuentasBancarias
            String queryCuentasBancarias = "SELECT b.nombreCorto, c.cuentaBancaria, b.clave, p.idProveedor FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[proveedor] p INNER JOIN  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[cuentasBancarias] c on c.idProveedor = p.idProveedor INNER JOIN  [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[bancos] b on b.clave = c.banco WHERE p.rfc = '" + Properties.Settings.Default.rfcGlobal + "'";     
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentasBancarias, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String nombreCorto = reader.GetString(0);
                            String cuentaBancaria = reader.GetString(1);
                            String clave = reader.GetString(2);
                            
                            bool metelo = true; ;
                            foreach (Dictionary<string, object> dic in listaAsociaciones)
                            {
                                if (Convert.ToString(dic["nombreCorto"]).Equals(nombreCorto) && Convert.ToString(dic["cuentaBancaria"]).Equals(cuentaBancaria))
                                {
                                    metelo = false;
                                    break;
                                }
                            }
                            if (metelo)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("nombreCorto", nombreCorto);
                                dictionary.Add("cuentaBancaria", cuentaBancaria);
                                dictionary.Add("clave", clave);
                          
                                listaCuentasBancarias.Add(dictionary);
                            }
                        }
                        cuentasBancariasList.Clear();
                        cuentasBancariasList.View = View.Details;
                        cuentasBancariasList.GridLines = true;
                        cuentasBancariasList.FullRowSelect = true;
                        cuentasBancariasList.Columns.Add("Banco", 130);
                        cuentasBancariasList.Columns.Add("Cuenta Bancaria", 200);
                        cuentasBancariasList.Columns.Add("clave", 0);
                 
                        foreach (Dictionary<string, object> dic in listaCuentasBancarias)
                        {
                            if (dic.ContainsKey("nombreCorto"))
                            {
                                string[] arr = new string[3];
                                ListViewItem itm2;
                                arr[0] = Convert.ToString(dic["nombreCorto"]);
                                arr[1] = Convert.ToString(dic["cuentaBancaria"]);
                                arr[2] = Convert.ToString(dic["clave"]);
                                itm2 = new ListViewItem(arr);
                                cuentasBancariasList.Items.Add(itm2);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Revisar: "+queryCuentasBancarias+" - No existen cuentas bancarias asociadas al RFC: " + Properties.Settings.Default.rfcGlobal + ", favor de verificar.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            llenaTodasLasTablasConDelTipoDeContabilidad(tipo);
        }
        public void eliminarAsociada(object sender, EventArgs e)
        {
            String tipo = asociacionesList.SelectedItems[0].SubItems[0].Text;

            String unidadDeNegocio = asociacionesList.SelectedItems[0].SubItems[1].Text;
            String JRNAL_TYPE = asociacionesList.SelectedItems[0].SubItems[2].Text;
            String ACNT_CODE = asociacionesList.SelectedItems[0].SubItems[3].Text;
            String banco = asociacionesList.SelectedItems[0].SubItems[4].Text;
            String cuentaBancaria = asociacionesList.SelectedItems[0].SubItems[5].Text;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[predefinidosDeChequeYTransferencia] WHERE tipo = " + tipoGlobal + " AND BUNIT = '" + unidadDeNegocio + "' AND JRNAL_TYPE = '" + JRNAL_TYPE + "' AND ACNT_CODE = '" + ACNT_CODE + "' AND cuentaBancaria = '" + cuentaBancaria + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    asociacionesList.Items.Remove(asociacionesList.SelectedItems[0]);
                    llenaConTipoDeContabilidad(tipoGlobal);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private void Asociar_TipoDiario_CuentaSunplus_CuentaBancaria_Load(object sender, EventArgs e)
        {
            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1 });
            menuItem1.Index = 0;
            menuItem1.Text = "Eliminar";
            menuItem1.Click += eliminarAsociada;
            asociacionesList.ContextMenu = contextMenu2;
            
            listaTipoDeDiario = new List<Dictionary<string, object>>();
            listaCuentasSunplus = new List<Dictionary<string, object>>();
            listaCuentasBancarias = new List<Dictionary<string, object>>();
            listaAsociaciones = new List<Dictionary<string, object>>();


            tipoContabilidad.Items.Add(new Item("Cheques", 1));
            tipoContabilidad.Items.Add(new Item("Transferencias", 2));
            tipoContabilidad.SelectedIndex = 0;
            llenaConTipoDeContabilidad(1);//Gastos
        }

        private void anadirButtom_Click(object sender, EventArgs e)
        {
            int cuantosTiposDeDiarios = tipoDeDiarioList.SelectedItems.Count;
            int cuantosCuentasSunplus = cuentasSunplusList.SelectedItems.Count;
            int cuantosCuentasBancarias = cuentasBancariasList.SelectedItems.Count;
            if(cuantosCuentasBancarias == 0 || cuantosCuentasSunplus == 0 || cuantosTiposDeDiarios ==0)
            {
                System.Windows.Forms.MessageBox.Show("Debes de seleccionar un tipo de diario, una cuenta de sunplus y una cuenta bancaria.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                String JOURNAL_TYPE = tipoDeDiarioList.SelectedItems[0].SubItems[0].Text.Trim();
                String ACNT_CODE = cuentasSunplusList.SelectedItems[0].SubItems[0].Text.Trim();
                String clave = cuentasBancariasList.SelectedItems[0].SubItems[2].Text.Trim();
                String cuentaBancaria = cuentasBancariasList.SelectedItems[0].SubItems[1].Text.Trim();
                String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[predefinidosDeChequeYTransferencia] (tipo,BUNIT,JRNAL_TYPE,ACNT_CODE,banco,cuentaBancaria) VALUES ("+tipoGlobal+", '" + Properties.Settings.Default.sunUnidadDeNegocio + "', '" + JOURNAL_TYPE + "', '" + ACNT_CODE + "', '"+clave+"', '"+cuentaBancaria+"')";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        tipoDeDiarioList.Items.Remove(tipoDeDiarioList.SelectedItems[0]);
                        cuentasSunplusList.Items.Remove(cuentasSunplusList.SelectedItems[0]);
                        cuentasBancariasList.Items.Remove(cuentasBancariasList.SelectedItems[0]);
                        llenaConTipoDeContabilidad(tipoGlobal);
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Revisar: "+query+" error: "+ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);             
                }
            }
        }

        private void tipoContabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)tipoContabilidad.SelectedItem;
            llenaConTipoDeContabilidad(itm.Value);
        }
    }
}
