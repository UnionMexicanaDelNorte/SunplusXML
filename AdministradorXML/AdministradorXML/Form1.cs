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
    public partial class Form1 : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public List<Dictionary<string, object>> listaFinalEgresosSAT { get; set; }
        public List<Dictionary<string, object>> listaFinalEgresosSunPlus { get; set; }
        public List<Dictionary<string, object>> listaFinalIngresosSunplus { get; set; }
        public double totalIngresoSAT { get; set; }
        public double totalContabilizadoIngresoSAT { get; set; }
        public double totalEgresoSAT { get; set; }
        public double totalContabilizadoEgresoSAT { get; set; }
        public String periodoGlobal { get; set; }
        public StringBuilder todasLasCuentasDeIngresos { get; set; }
        public StringBuilder todasLasCuentasDeEgresos { get; set; }
        public StringBuilder todasLasCuentasDeBalanza { get; set; }

        public bool siguiente { get; set; }
      
        public double totalIngresoSunplus { get; set; }
        public double totalContabilizadoIngresoSunplus { get; set; }
        public double totalEgresoSunplus { get; set; }
        public double totalContabilizadoEgresoSunplus { get; set; }
    

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
        public Form1()
        {
            InitializeComponent();
            AdministradorXML.Login.sourceGlobal = "AOK";//borrar
            AdministradorXML.Login.unidadDeNegocioGlobal = "CEA";
        }

        public Form1(String s, String bunit)
        {
            InitializeComponent();
            AdministradorXML.Login.sourceGlobal = s;
            AdministradorXML.Login.unidadDeNegocioGlobal = bunit;
        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config form = new config();
            form.Show();
        }

        private void permisosDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PermisosDeCuentas form1 = new PermisosDeCuentas();
            form1.Show();
        }

        private void cuentasBancariasDeProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CuentasBancariasDeProveedores form = new CuentasBancariasDeProveedores();
            form.Show();

        }

        private void misDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            misDatos datos = new misDatos();
            datos.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Sunplusito® 0.1 Es un programa que se integra con SunSystems 5.4 Patch 22, supongo que funciona para patches superiores, no olvide actualizar el programa cuando se liberen actualizaciones.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Asociar_TipoDiario_CuentaSunplus_CuentaBancaria form = new Asociar_TipoDiario_CuentaSunplus_CuentaBancaria();
            form.Show();
        }
        private void arreglaAlgunosProblemillas()
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] set STATUS = '1' WHERE fechaCancelacion = '1900-01-01' AND STATUS = '0'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }  

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String query = "WITH cte AS (SELECT folioFiscal, row_number() OVER(PARTITION BY folioFiscal ORDER BY idXml) AS [rn] FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML]) DELETE cte WHERE [rn] > 1";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }  

             

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            if(AdministradorXML.Login.sourceGlobal.Equals("ERROR"))
            {
                System.Windows.Forms.MessageBox.Show("Sunplusito® se abre desde el formulario de Sunplus. No se detecto el operador.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
            logueadoComoToolStripMenuItem.Text = "Logueado como: " + AdministradorXML.Login.sourceGlobal+" en "+Login.unidadDeNegocioGlobal;
            siguiente = true;
            String connString2 = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 10";
            String queryCheck = "USE [" + Properties.Settings.Default.sunDatabase + "] SELECT name FROM sys.tables";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString2))
                {
                    connection.Open();

                    SqlCommand cmdCheck = new SqlCommand(queryCheck, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // System.Windows.Forms.MessageBox.Show("Conexión Establecida satisfactoriamente", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        config form = new config();
                        form.ShowDialog();
                        //  System.Windows.Forms.MessageBox.Show("Sin conexión", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                config form = new config();
                form.ShowDialog();
                //System.Windows.Forms.MessageBox.Show("Sin conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }/*
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("settings.txt"))
                {
                    String line = sr.ReadToEnd();
                    Properties.Settings.Default.datasource = line;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }*/

            arreglaAlgunosProblemillas();
            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            int posX = 50;
            int posY = 50;
            int dif = 20;
            totalIngresoSATLabel.Location = new Point(posX, posY + (height / 2) - (posY * 2) + 5);
            totalEgresosSATLabel.Location = new Point(posX, posY + (height / 2) - (posY * 2) + posY + (height / 2) - (posY * 2)+5);
            listaFinal = new List<Dictionary<string, object>>();
            listaFinalEgresosSAT = new List<Dictionary<string, object>>();
            listaFinalIngresosSunplus = new List<Dictionary<string, object>>();
            listaFinalEgresosSunPlus = new List<Dictionary<string, object>>();
               
            totalIngresosSunplusLabel.Location = new Point((width / 2) + (posX / 2), posY + (height / 2) - (posY * 2) + 5);
            totalEgresosSunplusLabel.Location = new Point((width / 2) + (posX / 2), posY + (height / 2) - (posY * 2) + posY + (height / 2) - (posY * 2) + 5);


            periodoLabel.Location = new Point(posX, posY-dif);
            periodosCombo.Location = new Point(periodosCombo.Location.X, posY - dif-7);
            ingresosSATList.Location = new Point(posX, posY);
            ingresosSATList.Size = new Size((width / 2) - (posX * 2), (height / 2) - (posY * 2));

            ingresosSunPlusList.Location = new Point((width / 2) + (posX / 2), posY);
            ingresosSunPlusList.Size = new Size((width / 2) - (posX * 2), (height / 2) - (posY * 2));

            gastosSATList.Location = new Point(posX, posY + (height / 2) - (posY * 2)+posY);
            gastosSATList.Size = new Size((width / 2) - (posX * 2), (height / 2) - (posY * 2));

            gastosSunplusList.Location = new Point((width / 2) + (posX / 2), posY + (height / 2) - (posY * 2) + posY);
            gastosSunplusList.Size = new Size((width / 2) - (posX * 2), (height / 2) - (posY * 2));
            //String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL'";
          
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String queryPeriodos = "SELECT DISTINCT SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) as periodos FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE CAST(fechaExpedicion AS NVARCHAR(11)) != 'NULL' order by SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) asc";
           
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    int empiezo = 1;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            String periodo = reader.GetString(0);
                            periodosCombo.Items.Add(new Item(periodo, empiezo));
                            empiezo++;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen Periodos, primero descarga xml del buzon tributario.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            periodosCombo.SelectedIndex = periodosCombo.Items.Count - 1;
        }
        
        private void actualiza()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Item itm = (Item)periodosCombo.SelectedItem;
            //1.- genero la tabla con los rfc del mes y sus cantidades de lo que tiene el sat
            //2.- para cada folio fiscal en la tabala FISCAL, hago un query con el folio fiscal y el periodo seleccionado, obtengo el rfc y la cantidad, y a la cantidad obtenida le sumo al diccionario de la lista
            //nota: usare una lista de diccionarios que llenare en el paso uno y modificare en el paso 2, y finalmente lleno la vista
            String periodo = itm.Name;
            periodoGlobal = periodo;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            ingresosSATList.Clear();
            listaFinal.Clear();

            totalIngresoSAT = 0;
            totalContabilizadoIngresoSAT = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos SAT
                    String queryXML = "SELECT rfc,SUM(total) as total,razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodo + "' AND STATUS = '2' GROUP BY rfc,razonSocial order by rfc asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(1)));
                                String rfc = reader.GetString(0);
                                String razonSocial = reader.GetString(2);


                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("maximo", total);
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("enlazado", 0);
                                dictionary.Add("razonSocial", razonSocial);

                                listaFinal.Add(dictionary);
                            }
                            double amount = 0;
                            String folioFiscal = "";
                            String queryFISCAL = "SELECT FOLIO_FISCAL,AMOUNT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml]";
                            using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                            {
                                SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                if (readerFISCAL.HasRows)
                                {
                                    while (readerFISCAL.Read())
                                    {
                                        amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(1)));
                                        folioFiscal = readerFISCAL.GetString(0);
                                        //ingresos
                                        String queryFISCAL2 = "SELECT rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7)  = '" + periodo + "' AND STATUS = '2'";
                                        using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                        {
                                            SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                            if (readerFISCAL2.HasRows)
                                            {
                                                while (readerFISCAL2.Read())
                                                {
                                                    String rfcAux = Convert.ToString(readerFISCAL2.GetString(0));
                                                    foreach (Dictionary<string, object> dic in listaFinal)
                                                    {
                                                        if (dic["rfc"].Equals(rfcAux))
                                                        {
                                                            dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }


                            ingresosSATList.View = View.Details;
                            ingresosSATList.GridLines = true;
                            ingresosSATList.FullRowSelect = true;
                            ingresosSATList.Columns.Add("Razon Social", 200);
                            ingresosSATList.Columns.Add("RFC", 150);
                            ingresosSATList.Columns.Add("En el SAT", 150);
                            ingresosSATList.Columns.Add("Enlazado", 150);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("rfc"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["razonSocial"]);
                                    arr[1] = Convert.ToString(dic["rfc"]);
                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["maximo"]));
                                    arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["enlazado"]));
                                    

                                    totalIngresoSAT += Convert.ToDouble(dic["maximo"]);
                                    totalContabilizadoIngresoSAT += Convert.ToDouble(dic["enlazado"]);
                                    itm2 = new ListViewItem(arr);
                                    ingresosSATList.Items.Add(itm2);
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
            //egresos SAT
            listaFinalEgresosSAT.Clear();
            gastosSATList.Clear();
            totalEgresoSAT = 0;
            totalContabilizadoEgresoSAT = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos SAT
                    String queryXML = "SELECT rfc,SUM(total) as total,razonSocial FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7) = '" + periodo + "' AND STATUS = '1' GROUP BY rfc,razonSocial order by rfc asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(1)));
                                String rfc = reader.GetString(0);
                                String razonSocial = reader.GetString(2);


                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("maximo", total);
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("enlazado", 0);
                                dictionary.Add("razonSocial", razonSocial);

                                listaFinalEgresosSAT.Add(dictionary);
                            }
                            double amount = 0;
                            String folioFiscal = "";
                            String queryFISCAL = "SELECT FOLIO_FISCAL,AMOUNT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml]";
                            using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                            {
                                SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                if (readerFISCAL.HasRows)
                                {
                                    while (readerFISCAL.Read())
                                    {
                                        amount = Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(1)));
                                        folioFiscal = readerFISCAL.GetString(0);
                                        //gastos
                                        String queryFISCAL2 = "SELECT rfc FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] WHERE folioFiscal = '" + folioFiscal + "' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,7)  = '" + periodo + "' AND STATUS = '1'";
                                        using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                        {
                                            SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                            if (readerFISCAL2.HasRows)
                                            {
                                                while (readerFISCAL2.Read())
                                                {
                                                    String rfcAux = Convert.ToString(readerFISCAL2.GetString(0));
                                                    foreach (Dictionary<string, object> dic in listaFinalEgresosSAT)
                                                    {
                                                        if (dic["rfc"].Equals(rfcAux))
                                                        {
                                                            dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            gastosSATList.View = View.Details;
                            gastosSATList.GridLines = true;
                            gastosSATList.FullRowSelect = true;
                            gastosSATList.Columns.Add("Razon Social", 250);
                            gastosSATList.Columns.Add("RFC", 150);
                            gastosSATList.Columns.Add("En el SAT", 150);
                            gastosSATList.Columns.Add("Enlazado", 150);
                            foreach (Dictionary<string, object> dic in listaFinalEgresosSAT)
                            {
                                if (dic.ContainsKey("rfc"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["razonSocial"]);
                                    arr[1] = Convert.ToString(dic["rfc"]);
                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["maximo"]));
                                    arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["enlazado"]));
                                    
                                    totalEgresoSAT += Convert.ToDouble(dic["maximo"]);
                                    totalContabilizadoEgresoSAT += Convert.ToDouble(dic["enlazado"]);
                                    itm2 = new ListViewItem(arr);
                                    gastosSATList.Items.Add(itm2);
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



            //ingresos Sunplus
            listaFinalIngresosSunplus.Clear();

            ingresosSunPlusList.Clear();
            totalIngresoSunplus = 0;
            totalContabilizadoIngresoSunplus = 0;

            String year = periodoGlobal.Substring(0, 4);
            String month = periodoGlobal.Substring(5, 2);

            String periodoParaQuery = year + "0" + month;
            todasLasCuentasDeEgresos = new StringBuilder("");
            //obtengo todas las cuentas de gastos!!
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos sunplus
                    String queryXML = "SELECT ACNT_CODE FROM [SU_FISCAL].[dbo].[permisos_cuentas] WHERE unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "' AND tipoDeContabilidad = '1'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        bool first = true;
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (first)
                                {
                                    first = false;
                                    todasLasCuentasDeEgresos.Append("'" + reader.GetString(0).Trim() + "'");
                                }
                                else
                                {
                                    todasLasCuentasDeEgresos.Append(",'" + reader.GetString(0).Trim() + "'");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Como estas " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //aqui ya tengo todas las cuentas de gastos
            todasLasCuentasDeIngresos = new StringBuilder("");
            //obtengo todas las cuentas de ingresos!!
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos sunplus
                    String queryXML = "SELECT ACNT_CODE FROM [SU_FISCAL].[dbo].[permisos_cuentas] WHERE unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "' AND tipoDeContabilidad = '2'";

                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        bool first = true;
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (first)
                                {
                                    first = false;
                                    todasLasCuentasDeIngresos.Append("'" + reader.GetString(0).Trim() + "'");
                                }
                                else
                                {
                                    todasLasCuentasDeIngresos.Append(",'" + reader.GetString(0).Trim() + "'");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Como estas " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //aqui ya tengo todas las cuentas de ingresos
            //obtengo todas las cuentas de balanza!!
            todasLasCuentasDeBalanza = new StringBuilder("");

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos sunplus
                    String queryXML = "SELECT ACNT_CODE FROM [SU_FISCAL].[dbo].[permisos_cuentas] WHERE unidadDeNegocio = '" + Login.unidadDeNegocioGlobal + "' AND tipoDeContabilidad = '3'";

                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        bool first = true;
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (first)
                                {
                                    first = false;
                                    todasLasCuentasDeBalanza.Append("'" + reader.GetString(0).Trim() + "'");
                                }
                                else
                                {
                                    todasLasCuentasDeBalanza.Append(",'" + reader.GetString(0).Trim() + "'");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Hola " + ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //hasta aqui tengo todas las cuentas de balanza!!
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //ingresos sunplus
                    if(todasLasCuentasDeIngresos.Length==0)
                    {
                        System.Windows.Forms.MessageBox.Show("Hola, te falta indicartus cuentas de Ingreso en sunplus, y probablemente las de egreso y balanza también.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    String queryXML = "SELECT s.ACCNT_CODE, ISNULL(SUM(s.AMOUNT),0)  as suma, MAX( a.DESCR) as DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = s.ACCNT_CODE WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.ACCNT_CODE in (" + todasLasCuentasDeIngresos.ToString() + ") GROUP BY s.ACCNT_CODE ORDER BY  s.ACCNT_CODE asc ";
                   
     
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            String ACCNT_CODE = "";
                            while (reader.Read())
                            {
                                ACCNT_CODE = reader.GetString(0).Trim();
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(1)));
                                String DESCR = reader.GetString(2);
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("ACCNT_CODE", ACCNT_CODE);
                                dictionary.Add("total", total);
                                dictionary.Add("DESCR", DESCR);
                                dictionary.Add("enlazado", 0);

                                listaFinalIngresosSunplus.Add(dictionary);

                                String queryFISCAL = "SELECT ISNULL( SUM(f.AMOUNT),0) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE s.ACCNT_CODE = '" + ACCNT_CODE + "' AND s.PERIOD = '" + periodoParaQuery + "'";

                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            double amount = Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(0))), 2);
                                            foreach (Dictionary<string, object> dic in listaFinalIngresosSunplus)
                                            {
                                                if (dic["ACCNT_CODE"].Equals(ACCNT_CODE))
                                                {
                                                    dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (todasLasCuentasDeBalanza.ToString().Length > 1)
                            {
                                String queryFISCAL1 = "SELECT  s.ACCNT_CODE, ISNULL(SUM(s.AMOUNT),0) as suma,MAX( a.DESCR) as DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = s.ACCNT_CODE WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.D_C = 'C' AND s.ACCNT_CODE in (" + todasLasCuentasDeBalanza.ToString() + ") GROUP BY s.ACCNT_CODE ORDER BY  s.ACCNT_CODE asc ";

                                using (SqlCommand cmdCheckFISCAL1 = new SqlCommand(queryFISCAL1, connection))
                                {
                                    SqlDataReader readerFISCAL1 = cmdCheckFISCAL1.ExecuteReader();
                                    if (readerFISCAL1.HasRows)
                                    {
                                        while (readerFISCAL1.Read())
                                        {
                                            ACCNT_CODE = readerFISCAL1.GetString(0).Trim();
                                            double total = Convert.ToDouble(Math.Abs(readerFISCAL1.GetDecimal(1)));
                                            String DESCR = readerFISCAL1.GetString(2);
                                            Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
                                            dictionary1.Add("ACCNT_CODE", ACCNT_CODE);
                                            dictionary1.Add("total", total);
                                            dictionary1.Add("DESCR", DESCR);
                                            dictionary1.Add("enlazado", 0);

                                            listaFinalIngresosSunplus.Add(dictionary1);
                                            String queryFISCAL2 = "SELECT ISNULL(SUM(f.AMOUNT),0) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE s.D_C= 'C' AND s.ACCNT_CODE = '" + ACCNT_CODE + "' AND s.PERIOD = '" + periodoParaQuery + "'";

                                            using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                            {
                                                SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                                if (readerFISCAL2.HasRows)
                                                {
                                                    while (readerFISCAL2.Read())
                                                    {
                                                        double amount = Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL2.GetDecimal(0))), 2);
                                                        foreach (Dictionary<string, object> dic in listaFinalIngresosSunplus)
                                                        {
                                                            if (dic["ACCNT_CODE"].Equals(ACCNT_CODE))
                                                            {
                                                                dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }






                            ingresosSunPlusList.View = View.Details;
                            ingresosSunPlusList.GridLines = true;
                            ingresosSunPlusList.FullRowSelect = true;
                            ingresosSunPlusList.Columns.Add("Cuenta", 100);
                            ingresosSunPlusList.Columns.Add("Descripción", 180);
                            ingresosSunPlusList.Columns.Add("En el sunplus", 200);
                            ingresosSunPlusList.Columns.Add("Enlazado", 200);
                            foreach (Dictionary<string, object> dic in listaFinalIngresosSunplus)
                            {
                                if (dic.ContainsKey("ACCNT_CODE"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["ACCNT_CODE"]);
                                    arr[1] = Convert.ToString(dic["DESCR"]);
                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["total"]));
                                 
                                    arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["enlazado"]));
                                    totalIngresoSunplus += Convert.ToDouble(dic["total"]);
                                    totalContabilizadoIngresoSunplus += Convert.ToDouble(dic["enlazado"]);
                                    itm2 = new ListViewItem(arr);
                                    ingresosSunPlusList.Items.Add(itm2);
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
            listaFinalEgresosSunPlus.Clear();

            gastosSunplusList.Clear();
            totalEgresoSunplus = 0;
            totalContabilizadoEgresoSunplus = 0;

            //aqui empiezo con los gastos sunplusito
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //gastos sunplus
                    String queryXML = "SELECT s.ACCNT_CODE, ISNULL(SUM(s.AMOUNT),0)  as suma,MAX( a.DESCR) as DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = s.ACCNT_CODE WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.ACCNT_CODE in (" + todasLasCuentasDeEgresos.ToString() + ") GROUP BY s.ACCNT_CODE ORDER BY  s.ACCNT_CODE asc ";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            String ACCNT_CODE = "";
                            while (reader.Read())
                            {
                                ACCNT_CODE = reader.GetString(0).Trim();
                                double total = Convert.ToDouble(Math.Abs(reader.GetDecimal(1)));
                                String DESCR = reader.GetString(2).Trim();
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("ACCNT_CODE", ACCNT_CODE);
                                dictionary.Add("total", total);
                                dictionary.Add("DESCR", DESCR);
                                dictionary.Add("enlazado", 0);

                                listaFinalEgresosSunPlus.Add(dictionary);
                                String queryFISCAL = "SELECT ISNULL( SUM(f.AMOUNT),0) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE s.ACCNT_CODE = '" + ACCNT_CODE + "' AND s.PERIOD = '" + periodoParaQuery + "'";
                                using (SqlCommand cmdCheckFISCAL = new SqlCommand(queryFISCAL, connection))
                                {
                                    SqlDataReader readerFISCAL = cmdCheckFISCAL.ExecuteReader();
                                    if (readerFISCAL.HasRows)
                                    {
                                        while (readerFISCAL.Read())
                                        {
                                            double amount = Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL.GetDecimal(0))), 2);
                                            foreach (Dictionary<string, object> dic in listaFinalEgresosSunPlus)
                                            {
                                                if (dic["ACCNT_CODE"].Equals(ACCNT_CODE))
                                                {
                                                    dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (todasLasCuentasDeBalanza.ToString().Length > 1)
                            {
                                String queryFISCAL1 = "SELECT s.ACCNT_CODE, ISNULL(SUM(s.AMOUNT),0) as suma,MAX( a.DESCR) as DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_ACNT] a on a.ACNT_CODE = s.ACCNT_CODE WHERE s.PERIOD = '" + periodoParaQuery + "' AND s.ALLOCATION != 'C' AND s.D_C = 'D' AND s.ACCNT_CODE in (" + todasLasCuentasDeBalanza.ToString() + ") GROUP BY s.ACCNT_CODE ORDER BY  s.ACCNT_CODE asc ";
                                using (SqlCommand cmdCheckFISCAL1 = new SqlCommand(queryFISCAL1, connection))
                                {
                                    SqlDataReader readerFISCAL1 = cmdCheckFISCAL1.ExecuteReader();
                                    if (readerFISCAL1.HasRows)
                                    {
                                        while (readerFISCAL1.Read())
                                        {
                                            ACCNT_CODE = readerFISCAL1.GetString(0).Trim();
                                            double total = Convert.ToDouble(Math.Abs(readerFISCAL1.GetDecimal(1)));
                                            String DESCR = readerFISCAL1.GetString(2).Trim();
                                            Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
                                            dictionary1.Add("ACCNT_CODE", ACCNT_CODE);
                                            dictionary1.Add("total", total);
                                            dictionary1.Add("DESCR", DESCR);
                                            dictionary1.Add("enlazado", 0);

                                            listaFinalEgresosSunPlus.Add(dictionary1);
                                            String queryFISCAL2 = "SELECT ISNULL(SUM(f.AMOUNT),0) as total FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] x on x.folioFiscal = f.FOLIO_FISCAL INNER JOIN [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal + "_" + Properties.Settings.Default.sunLibro + "_SALFLDG] s on s.JRNAL_NO = f.JRNAL_NO and s.JRNAL_LINE = f.JRNAL_LINE WHERE s.D_C = 'D' AND s.ACCNT_CODE = '" + ACCNT_CODE + "' AND s.PERIOD = '" + periodoParaQuery + "'";
                                         
                                            using (SqlCommand cmdCheckFISCAL2 = new SqlCommand(queryFISCAL2, connection))
                                            {
                                                SqlDataReader readerFISCAL2 = cmdCheckFISCAL2.ExecuteReader();
                                                if (readerFISCAL2.HasRows)
                                                {
                                                    while (readerFISCAL2.Read())
                                                    {
                                                        double amount = Math.Round(Convert.ToDouble(Math.Abs(readerFISCAL2.GetDecimal(0))), 2);
                                                        foreach (Dictionary<string, object> dic in listaFinalEgresosSunPlus)
                                                        {
                                                            if (dic["ACCNT_CODE"].Equals(ACCNT_CODE))
                                                            {                                               
                                                                dic["enlazado"] = Convert.ToString(Convert.ToDouble(dic["enlazado"]) + amount);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }



                                        }
                                    }
                                }
                            }//if


                            gastosSunplusList.View = View.Details;
                            gastosSunplusList.GridLines = true;
                            gastosSunplusList.FullRowSelect = true;
                            gastosSunplusList.Columns.Add("Cuenta", 100);
                            gastosSunplusList.Columns.Add("Descripción", 180);
                            gastosSunplusList.Columns.Add("En el sunplus", 200);
                            gastosSunplusList.Columns.Add("Enlazado", 200);
                            foreach (Dictionary<string, object> dic in listaFinalEgresosSunPlus)
                            {
                                if (dic.ContainsKey("ACCNT_CODE"))
                                {
                                    string[] arr = new string[5];
                                    ListViewItem itm2;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["ACCNT_CODE"]);
                                    arr[1] = Convert.ToString(dic["DESCR"]);

                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["total"]));
                                    arr[3] = String.Format("{0:n}", Convert.ToDouble(dic["enlazado"]));
                                    
                                    totalEgresoSunplus += Convert.ToDouble(dic["total"]);
                                    totalContabilizadoEgresoSunplus += Convert.ToDouble(dic["enlazado"]);
                                    itm2 = new ListViewItem(arr);
                                    gastosSunplusList.Items.Add(itm2);
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

            totalIngresoSATLabel.Text = "Ingreso en el SAT: $" + String.Format("{0:n}", totalIngresoSAT) + " Enlazado: $" + String.Format("{0:n}", totalContabilizadoIngresoSAT);
            totalEgresosSATLabel.Text = "Egreso en el SAT: $" + String.Format("{0:n}", totalEgresoSAT) + " Enlazado: $" + String.Format("{0:n}", totalContabilizadoEgresoSAT);
            totalIngresosSunplusLabel.Text = "Ingreso en el Sunplus: $" + String.Format("{0:n}", totalIngresoSunplus) + " Enlazado: $" + String.Format("{0:n}", totalContabilizadoIngresoSunplus);
            totalEgresosSunplusLabel.Text = "Egreso en el Sunplus: $" + String.Format("{0:n}", totalEgresoSunplus) + " Enlazado: $" + String.Format("{0:n}", totalContabilizadoEgresoSunplus);





            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }

        private void ingresosSATList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = ingresosSATList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String rfc = ingresosSATList.SelectedItems[0].SubItems[1].Text.Trim();
                int tipo = 2;//ingresos vigentes
                Item itm = (Item)periodosCombo.SelectedItem;
                String periodo = itm.Name;
                Detalle1 form = new Detalle1(rfc, tipo, periodo);
                form.ShowDialog();
               // actualiza();
            }
        }

        private void gastosSATList_SelectedIndexChanged(object sender, EventArgs e)
        {
             int cuantos = gastosSATList.SelectedItems.Count;
             if (cuantos > 0)
             {
                 String rfc = gastosSATList.SelectedItems[0].SubItems[1].Text.Trim();
                 int tipo = 1;//egresos vigentes
                 Item itm = (Item)periodosCombo.SelectedItem;
                 String periodo = itm.Name;
                 Detalle1 form = new Detalle1(rfc, tipo, periodo);
                 form.ShowDialog();
              //   actualiza();
             }
        }

        private void ingresosSunPlusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = ingresosSunPlusList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String cuenta = ingresosSunPlusList.SelectedItems[0].SubItems[0].Text.Trim();
                int tipo = 2;//ingresos vigentes
                Item itm = (Item)periodosCombo.SelectedItem;
                String periodo = itm.Name;
                Detalle2 form2 = new Detalle2(cuenta, tipo, periodo, todasLasCuentasDeIngresos.ToString(), todasLasCuentasDeEgresos.ToString(), todasLasCuentasDeBalanza.ToString());
                form2.ShowDialog();
                //actualiza(); 
            }
        }

        private void gastosSunplusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = gastosSunplusList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String cuenta = gastosSunplusList.SelectedItems[0].SubItems[0].Text.Trim();
                int tipo = 1;//gastos vigentes
                Item itm = (Item)periodosCombo.SelectedItem;
                String periodo = itm.Name;
                Detalle2 form2 = new Detalle2(cuenta, tipo, periodo, todasLasCuentasDeIngresos.ToString(), todasLasCuentasDeEgresos.ToString(), todasLasCuentasDeBalanza.ToString());
                form2.ShowDialog();
                //actualiza();
            }
        }

        private void xMLDePolizasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Próximamente... cuando se necesite, esperemos que nunca se necesite", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          
            XMLPolizasDelMes xmlPL = new XMLPolizasDelMes();
            xmlPL.ShowDialog();
        }

        private void asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codigoAgrupador ca = new codigoAgrupador();
            ca.ShowDialog();
        }

        private void xMLDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLCuentas form = new XMLCuentas();
            form.ShowDialog();
        }

        private void xMLDeBalanzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLBalanza bal = new XMLBalanza();
            bal.ShowDialog();
        }

        private void facturasCanceladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Canceladas form = new Canceladas();
            form.ShowDialog();
        }

        private void impuestosDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerImpuestos form = new VerImpuestos();
            form.ShowDialog();
        }

        private void xMLDeCuentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Hola, para generar el XML del Catalogo de cuentas, estamos suponiendo que cada cuenta en la dimension con id 14 \"TYPE\" tiene asignada una \"D\" o una \"A\", tambien estamos suponiendo que todas las cuentas de 6 o más digitos son de nivel 2, y las mas chicas son de nivel 1, si se llega a ocupar fiscalmente este XML, podrían cambiar estas suposiciones.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void xMLDeBalanzaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Hola, para generar el XML de balanza, se esta tomando en cuenta todos los diarios, inclusive aquellos cuyo ALLOCATION = C", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void configurarConceptosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configurarConceptos form = new configurarConceptos();
            form.ShowDialog();
        }

        private void asignarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            asignarComceptosaWHO form = new asignarComceptosaWHO();
            form.ShowDialog();
        }

        private void asignarFNCTAWHOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            asignarFNCTaWHO form = new asignarFNCTaWHO();
            form.ShowDialog();
        }

        private void polizasDeDepartamentalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polizasDeDepartamentales form = new polizasDeDepartamentales();
            form.ShowDialog();
        }

        private void presupuestoDeDepartamentalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presupuestoDeDepartamentales form = new presupuestoDeDepartamentales();
            form.ShowDialog();
        }

        private void asignarPROJAWHOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            asignarPROJaWHO form = new asignarPROJaWHO();
            form.ShowDialog();
        }

        private void mandarMensajeCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mandarPush mandar = new mandarPush();
            mandar.ShowDialog();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }

        private void facturasNoEnlazadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FacturasNoEnlazadas form = new FacturasNoEnlazadas();
            form.ShowDialog();
        }

        private void actualizarButton_Click(object sender, EventArgs e)
        {
            actualiza();
        }

         
            
        private void    borrarMisTemporalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[temporales] WHERE BUNIT ='" + Login.unidadDeNegocioGlobal + "' AND JRNAL_SOURCE = '" + Login.sourceGlobal + "'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("Temporales borrados para "+Login.sourceGlobal+" en "+Login.unidadDeNegocioGlobal, "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }         
        }

        private void accionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void ligarDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LigarDiario ligarDiario = new LigarDiario();
            ligarDiario.ShowDialog();
        }

        private void configurarDestinatariosDeIglesiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarDestinatariosIglesias form = new ConfigurarDestinatariosIglesias();
            form.ShowDialog();
        }

        private void mandarReciboDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MandarReciboDeCaja form = new MandarReciboDeCaja();
            form.ShowDialog();
        }

        private void facturasPorProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xMLDeFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLFacturas fac = new XMLFacturas();
            fac.ShowDialog();
        }

        private void estadoDeCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadoDeCuenta estado = new EstadoDeCuenta();
            estado.ShowDialog();
        }

        private void arreglarError30OctToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] set STATUS = '1' WHERE fechaCancelacion = '1900-01-01' AND STATUS = '0'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("Facturas arregladas... disculpe las molestias, estamos trabajando para usted.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }   
        }

        private void ligarRFCACuentaSunplusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LigarRFCACuenta form = new LigarRFCACuenta();
            form.Show();
        }

        private void ejecutarPreligueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EjecutarPreligue form = new EjecutarPreligue();
            form.Show();
        }

        private void verRelacionRfccuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerRelacionRFCCuenta form = new VerRelacionRFCCuenta();
            form.Show();
        }

        private void historialDePreliguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistorialPreligues form = new HistorialPreligues();
            form.Show();
        }
    }
}
