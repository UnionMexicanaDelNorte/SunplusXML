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
    public partial class configurarConceptos : Form
    {
        System.Windows.Forms.MenuItem menuItem2;

        System.Windows.Forms.ContextMenu contextMenu2;
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public int idConceptoGlobal { get; set; }
    
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
        public configurarConceptos()
        {
            InitializeComponent();
        }
        private void BorrarConcepto(object sender, EventArgs e)
        {
            String idConcepto = conceptosList.SelectedItems[0].SubItems[0].Text.Trim();
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            String query = "DELETE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] WHERE idConcepto = " + idConcepto;
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
        private void configurarConceptos_Load(object sender, EventArgs e)
        {
            tipoCombo.Items.Add(new Item("Gasto", 1));
            tipoCombo.Items.Add(new Item("Forma de pago", 2));
            tipoCombo.SelectedIndex = 0;

            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2 });
             menuItem2.Index = 0;
            menuItem2.Text = "Borrar concepto";
            menuItem2.Click += BorrarConcepto;
            conceptosList.ContextMenu = contextMenu2;
            listaFinal = new List<Dictionary<string, object>>();
                String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
        
               String queryPeriodos = "SELECT TOP 10 ANL_CAT_ID, LOOKUP FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ANL_CAT] order by ANL_CAT_ID asc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryPeriodos, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int empiezo = 1;
                        d1combo.Items.Add(new Item("", 0));
                        d2combo.Items.Add(new Item("", 0));
                        d3combo.Items.Add(new Item("", 0));
                        d4combo.Items.Add(new Item("", 0));
                        d5combo.Items.Add(new Item("", 0));
                        d6combo.Items.Add(new Item("", 0));
                        d7combo.Items.Add(new Item("", 0));
                        d8combo.Items.Add(new Item("", 0));
                        d9combo.Items.Add(new Item("", 0));
                        d10combo.Items.Add(new Item("", 0));
                        while (reader.Read())
                        {
                            String ANL_CAT_ID = reader.GetString(0).Trim();
                            String LOOKUP = reader.GetString(1).Trim();
                            if(empiezo==1)
                            {
                                d1label.Text = LOOKUP;
                            }
                            String queryANL = "SELECT ANL_CODE FROM ["+Properties.Settings.Default.sunDatabase+"].[dbo].["+ Properties.Settings.Default.sunUnidadDeNegocio +"_ANL_CODE] WHERE ANL_CAT_ID = '"+ANL_CAT_ID+"' order by ANL_CODE asc";

                            using (SqlCommand cmdCheck1 = new SqlCommand(queryANL, connection))
                            {
                                SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        String ANL_CODE = reader1.GetString(0).Trim();
                                        switch(empiezo)
                                        {
                                            case 1:
                                                d1label.Text = LOOKUP;
                                                d1combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 2:
                                                d2label.Text = LOOKUP;
                                                d2combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 3:
                                                d3label.Text = LOOKUP;
                                                d3combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 4:
                                                d4label.Text = LOOKUP;
                                                d4combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 5:
                                                d5label.Text = LOOKUP;
                                                d5combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 6:
                                                d6label.Text = LOOKUP;
                                                d6combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 7:
                                                d7label.Text = LOOKUP;
                                                d7combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 8:
                                                d8label.Text = LOOKUP;
                                                d8combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 9:
                                                d9label.Text = LOOKUP;
                                                d9combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                            case 10:
                                                d10label.Text = LOOKUP;
                                                d10combo.Items.Add(new Item(ANL_CODE, empiezo));
                                            break;
                                        }
                           
                                    }
                                }
                            }
                            empiezo++;
                         
                    
                            
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No existen dimensiones", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            String queryCuentas = "SELECT ACNT_CODE, DESCR FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ACNT] order by ACNT_CODE asc";
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
                            String ACNT_CODE = reader.GetString(0).Trim();
                            cuentaBox.Items.Add(new Item(ACNT_CODE, empiezo1, ACNT_CODE));
                            empiezo1++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }





            d1combo.Items.Add(new Item("<who>", 1000));
            d2combo.Items.Add(new Item("<who>", 1000));
            d3combo.Items.Add(new Item("<who>", 1000));
            d4combo.Items.Add(new Item("<who>", 1000));
            d5combo.Items.Add(new Item("<who>", 1000));
            d6combo.Items.Add(new Item("<who>", 1000));
            d7combo.Items.Add(new Item("<who>", 1000));
            d8combo.Items.Add(new Item("<who>", 1000));
            d9combo.Items.Add(new Item("<who>", 1000));
            d10combo.Items.Add(new Item("<who>", 1000));

            d1combo.Items.Add(new Item("<fnct>", 1001));
            d2combo.Items.Add(new Item("<fnct>", 1002));
            d3combo.Items.Add(new Item("<fnct>", 1003));
            d4combo.Items.Add(new Item("<fnct>", 1004));
            d5combo.Items.Add(new Item("<fnct>", 1005));
            d6combo.Items.Add(new Item("<fnct>", 1006));
            d7combo.Items.Add(new Item("<fnct>", 1007));
            d8combo.Items.Add(new Item("<fnct>", 1008));
            d9combo.Items.Add(new Item("<fnct>", 1009));
            d10combo.Items.Add(new Item("<fnct>", 1010));


            d1combo.Items.Add(new Item("<dtls>", 1011));
            d2combo.Items.Add(new Item("<dtls>", 1012));
            d3combo.Items.Add(new Item("<dtls>", 1013));
            d4combo.Items.Add(new Item("<dtls>", 1014));
            d5combo.Items.Add(new Item("<dtls>", 1015));
            d6combo.Items.Add(new Item("<dtls>", 1016));
            d7combo.Items.Add(new Item("<dtls>", 1017));
            d8combo.Items.Add(new Item("<dtls>", 1018));
            d9combo.Items.Add(new Item("<dtls>", 1019));
            d10combo.Items.Add(new Item("<dtls>", 1020));

            d1combo.Items.Add(new Item("<proj>", 1021));
            d2combo.Items.Add(new Item("<proj>", 1022));
            d3combo.Items.Add(new Item("<proj>", 1023));
            d4combo.Items.Add(new Item("<proj>", 1024));
            d5combo.Items.Add(new Item("<proj>", 1025));
            d6combo.Items.Add(new Item("<proj>", 1026));
            d7combo.Items.Add(new Item("<proj>", 1027));
            d8combo.Items.Add(new Item("<proj>", 1028));
            d9combo.Items.Add(new Item("<proj>", 1029));
            d10combo.Items.Add(new Item("<proj>", 1030));


            d1combo.SelectedIndex = 0;
            d2combo.SelectedIndex = 0;
            d3combo.SelectedIndex = 0;
            d4combo.SelectedIndex = 0;
            d5combo.SelectedIndex = 0;
            d6combo.SelectedIndex = 0;
            d7combo.SelectedIndex = 0;
            d8combo.SelectedIndex = 0;
            d9combo.SelectedIndex = 0;
            d10combo.SelectedIndex = 0;
            actualiza();
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            if (nombreText.Text.Trim().Equals("")  || treferenceText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe tu cuenta bancaria", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                 try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        Item itm = (Item)cuentaBox.SelectedItem;
                        String cuenta = itm.Extra.ToString();

                        String ANAL_T0 = "";

                        Item itm1 = (Item)d1combo.SelectedItem;
                        ANAL_T0 = itm1.Name.ToString();

                        Item itm2 = (Item)d2combo.SelectedItem;
                        String ANAL_T1 = itm2.Name.ToString();

                        Item itm3 = (Item)d3combo.SelectedItem;
                        String ANAL_T2 = itm3.Name.ToString();

                        Item itm4 = (Item)d4combo.SelectedItem;
                        String ANAL_T3 = itm4.Name.ToString();

                        Item itm5 = (Item)d5combo.SelectedItem;
                        String ANAL_T4 = itm5.Name.ToString();

                        Item itm6 = (Item)d6combo.SelectedItem;
                        String ANAL_T5 = itm6.Name.ToString();

                        Item itm7 = (Item)d7combo.SelectedItem;
                        String ANAL_T6 = itm7.Name.ToString();

                        Item itm8 = (Item)d8combo.SelectedItem;
                        String ANAL_T7 = itm8.Name.ToString();

                        Item itm9 = (Item)d9combo.SelectedItem;
                        String ANAL_T8 = itm9.Name.ToString();

                        Item itm10 = (Item)d10combo.SelectedItem;
                        String ANAL_T9 = itm10.Name.ToString();

                        String treference = treferenceText.Text.Trim();
                        String nombre = nombreText.Text.Trim();

                        Item itmX = (Item)tipoCombo.SelectedItem;
                        String tipo = itmX.Value.ToString();


                        String query = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] (nombre,tipo,ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9,TREFERENCE,ACNT_CODE) VALUES ('" + nombre + "', " + tipo + ", '" + ANAL_T0 + "', '" + ANAL_T1 + "', '" + ANAL_T2 + "', '" + ANAL_T3 + "', '" + ANAL_T4 + "', '" + ANAL_T5 + "', '" + ANAL_T6 + "', '" + ANAL_T7 + "', '" + ANAL_T8 + "', '" + ANAL_T9 + "', '" + treference + "', '" + cuenta + "')";
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
        private void actualiza()
        {
            
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                conceptosList.Clear();
                listaFinal.Clear();
            
               
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        //conceptos 
                        String queryXML = "SELECT idConcepto,tipo,nombre,ANAL_T0,ANAL_T1,ANAL_T2,ANAL_T3,ANAL_T4,ANAL_T5,ANAL_T6,ANAL_T7,ANAL_T8,ANAL_T9,TREFERENCE,ACNT_CODE FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] order by idConcepto asc";
                        using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                        {
                            SqlDataReader reader = cmdCheck.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    String idConcepto = reader.GetInt32(0).ToString().Trim();
                                    String tipo = reader.GetInt32(1).ToString().Trim();
                                    String nombre = reader.GetString(2).Trim();
                                    String ANAL_T0 = reader.GetString(3).Trim();
                                    String ANAL_T1 = reader.GetString(4).Trim();
                                    String ANAL_T2 = reader.GetString(5).Trim();
                                    String ANAL_T3 = reader.GetString(6).Trim();
                                    String ANAL_T4 = reader.GetString(7).Trim();
                                    String ANAL_T5 = reader.GetString(8).Trim();
                                    String ANAL_T6 = reader.GetString(9).Trim();
                                    String ANAL_T7 = reader.GetString(10).Trim();
                                    String ANAL_T8 = reader.GetString(11).Trim();
                                    String ANAL_T9 = reader.GetString(12).Trim();
                                    String TREFERENCE = reader.GetString(13).Trim();
                                    String ACNT_CODE = reader.GetString(14).Trim();

                                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                    dictionary.Add("idConcepto", idConcepto);
                                    dictionary.Add("tipo", tipo);
                                    dictionary.Add("nombre", nombre);
                                    dictionary.Add("ANAL_T0", ANAL_T0);
                                    dictionary.Add("ANAL_T1", ANAL_T1);
                                    dictionary.Add("ANAL_T2", ANAL_T2);
                                    dictionary.Add("ANAL_T3", ANAL_T3);
                                    dictionary.Add("ANAL_T4", ANAL_T4);
                                    dictionary.Add("ANAL_T5", ANAL_T5);
                                    dictionary.Add("ANAL_T6", ANAL_T6);
                                    dictionary.Add("ANAL_T7", ANAL_T7);
                                    dictionary.Add("ANAL_T8", ANAL_T8);
                                    dictionary.Add("ANAL_T9", ANAL_T9);
                                    dictionary.Add("TREFERENCE", TREFERENCE);
                                    dictionary.Add("ACNT_CODE", ACNT_CODE);
                                    listaFinal.Add(dictionary);
                                }



                                conceptosList.View = View.Details;
                                conceptosList.GridLines = true;
                                conceptosList.FullRowSelect = true;
                                conceptosList.Columns.Add("idConcepto", 0);
                                conceptosList.Columns.Add("D_C", 40);
                                conceptosList.Columns.Add("nombre", 120);
                                conceptosList.Columns.Add("TREFERENCE", 120);
                                //hardcode
                                conceptosList.Columns.Add("REF2", 50);
                                conceptosList.Columns.Add("TFWW", 80);
                                conceptosList.Columns.Add("FUND", 80);
                                conceptosList.Columns.Add("FUNCTION", 80);
                                conceptosList.Columns.Add("RSTR", 50);
                                conceptosList.Columns.Add("ORGID", 80);
                                conceptosList.Columns.Add("WHO", 80);
                                conceptosList.Columns.Add("FLAG", 50);
                                conceptosList.Columns.Add("PROJ", 80);
                                conceptosList.Columns.Add("DTLS", 80);
                                conceptosList.Columns.Add("Cuenta", 150);

                                foreach (Dictionary<string, object> dic in listaFinal)
                                {
                                    if (dic.ContainsKey("idConcepto"))
                                    {
                                        string[] arr = new string[16];
                                        ListViewItem itm3;
                                        //add items to ListView
                                        arr[0] = Convert.ToString(dic["idConcepto"]);
                                        if (Convert.ToString(dic["tipo"]).Equals("1"))
                                        {
                                            arr[1] = "D";
                                        }
                                        else
                                        {
                                            arr[1] = "C";
                                        }
                                        arr[2] = Convert.ToString(dic["nombre"]);
                                        arr[3] = Convert.ToString(dic["TREFERENCE"]);
                                        arr[4] = Convert.ToString(dic["ANAL_T0"]);
                                        arr[5] = Convert.ToString(dic["ANAL_T1"]);
                                        arr[6] = Convert.ToString(dic["ANAL_T2"]);
                                        arr[7] = Convert.ToString(dic["ANAL_T3"]);
                                        arr[8] = Convert.ToString(dic["ANAL_T4"]);
                                        arr[9] = Convert.ToString(dic["ANAL_T5"]);
                                        arr[10] = Convert.ToString(dic["ANAL_T6"]);
                                        arr[11] = Convert.ToString(dic["ANAL_T7"]);
                                        arr[12] = Convert.ToString(dic["ANAL_T8"]);
                                        arr[13] = Convert.ToString(dic["ANAL_T9"]);
                                        arr[14] = Convert.ToString(dic["ACNT_CODE"]);
                                        itm3 = new ListViewItem(arr);
                                        conceptosList.Items.Add(itm3);
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

        private void conceptosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            idConceptoGlobal = Convert.ToInt32( conceptosList.SelectedItems[0].SubItems[0].Text.Trim());
            String D_C = conceptosList.SelectedItems[0].SubItems[1].Text.Trim();
            String nombre = conceptosList.SelectedItems[0].SubItems[2].Text.Trim();
            String TREFERENCE = conceptosList.SelectedItems[0].SubItems[3].Text.Trim();
            String ANAL_T0 = conceptosList.SelectedItems[0].SubItems[4].Text.Trim();
            String ANAL_T1 = conceptosList.SelectedItems[0].SubItems[5].Text.Trim();
            String ANAL_T2 = conceptosList.SelectedItems[0].SubItems[6].Text.Trim();
            String ANAL_T3 = conceptosList.SelectedItems[0].SubItems[7].Text.Trim();
            String ANAL_T4 = conceptosList.SelectedItems[0].SubItems[8].Text.Trim();
            String ANAL_T5 = conceptosList.SelectedItems[0].SubItems[9].Text.Trim();
            String ANAL_T6 = conceptosList.SelectedItems[0].SubItems[10].Text.Trim();
            String ANAL_T7 = conceptosList.SelectedItems[0].SubItems[11].Text.Trim();
            String ANAL_T8 = conceptosList.SelectedItems[0].SubItems[12].Text.Trim();
            String ANAL_T9 = conceptosList.SelectedItems[0].SubItems[13].Text.Trim();
            String ACNT_CODE = conceptosList.SelectedItems[0].SubItems[14].Text.Trim();
            nombreText.Text = nombre;
            treferenceText.Text = TREFERENCE;
            if(D_C.Equals("D"))
            {
                tipoCombo.SelectedIndex = 0;
            }
            else
            {
                tipoCombo.SelectedIndex = 1;
            }
            cuentaBox.SelectedIndex = cuentaBox.FindStringExact(ACNT_CODE);
            
            d1combo.SelectedIndex = d1combo.FindStringExact(ANAL_T0);
            d2combo.SelectedIndex = d2combo.FindStringExact(ANAL_T1);
            d3combo.SelectedIndex = d3combo.FindStringExact(ANAL_T2);
            d4combo.SelectedIndex = d4combo.FindStringExact(ANAL_T3);
            d5combo.SelectedIndex = d5combo.FindStringExact(ANAL_T4);
            d6combo.SelectedIndex = d6combo.FindStringExact(ANAL_T5);
            d7combo.SelectedIndex = d7combo.FindStringExact(ANAL_T6);
            d8combo.SelectedIndex = d8combo.FindStringExact(ANAL_T7);
            d9combo.SelectedIndex = d9combo.FindStringExact(ANAL_T8);
            d10combo.SelectedIndex = d10combo.FindStringExact(ANAL_T9);



        }

        private void modificarButton_Click(object sender, EventArgs e)
        {
            if (nombreText.Text.Trim().Equals("") || treferenceText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe tu cuenta bancaria", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        Item itm = (Item)cuentaBox.SelectedItem;
                        String cuenta = itm.Extra.ToString();

                        String ANAL_T0 = "";

                        Item itm1 = (Item)d1combo.SelectedItem;
                        ANAL_T0 = itm1.Name.ToString();

                        Item itm2 = (Item)d2combo.SelectedItem;
                        String ANAL_T1 = itm2.Name.ToString();

                        Item itm3 = (Item)d3combo.SelectedItem;
                        String ANAL_T2 = itm3.Name.ToString();

                        Item itm4 = (Item)d4combo.SelectedItem;
                        String ANAL_T3 = itm4.Name.ToString();

                        Item itm5 = (Item)d5combo.SelectedItem;
                        String ANAL_T4 = itm5.Name.ToString();

                        Item itm6 = (Item)d6combo.SelectedItem;
                        String ANAL_T5 = itm6.Name.ToString();

                        Item itm7 = (Item)d7combo.SelectedItem;
                        String ANAL_T6 = itm7.Name.ToString();

                        Item itm8 = (Item)d8combo.SelectedItem;
                        String ANAL_T7 = itm8.Name.ToString();

                        Item itm9 = (Item)d9combo.SelectedItem;
                        String ANAL_T8 = itm9.Name.ToString();

                        Item itm10 = (Item)d10combo.SelectedItem;
                        String ANAL_T9 = itm10.Name.ToString();

                        String treference = treferenceText.Text.Trim();
                        String nombre = nombreText.Text.Trim();

                        Item itmX = (Item)tipoCombo.SelectedItem;
                        String tipo = itmX.Value.ToString();


                        String query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] set nombre = '" + nombre + "',tipo = " + tipo + " ,ANAL_T0  = '" + ANAL_T0 + "',ANAL_T1 = '" + ANAL_T1 + "',ANAL_T2 = '" + ANAL_T2 + "',ANAL_T3 = '" + ANAL_T3 + "',ANAL_T4 = '" + ANAL_T4 + "',ANAL_T5 = '" + ANAL_T5 + "',ANAL_T6 = '" + ANAL_T6 + "',ANAL_T7 = '" + ANAL_T7 + "',ANAL_T8 = '" + ANAL_T8 + "',ANAL_T9 = '" + ANAL_T9 + "',TREFERENCE = '" + treference + "',ACNT_CODE = '" + cuenta + "' WHERE idConcepto = "+idConceptoGlobal;
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
}
