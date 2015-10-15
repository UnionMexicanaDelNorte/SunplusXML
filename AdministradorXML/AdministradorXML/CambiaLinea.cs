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
    public partial class CambiaLinea : Form
    {
        public String D_C { get; set; }
        public String ANAL_T0 { get; set; }
        public String ANAL_T1 { get; set; }
        public String ANAL_T2 { get; set; }
        public String ANAL_T3 { get; set; }
        public String ANAL_T4 { get; set; }
        public String ANAL_T5 { get; set; }
        public String ANAL_T6 { get; set; }
        public String ANAL_T7 { get; set; }
        public String ANAL_T8 { get; set; }
        public String ANAL_T9 { get; set; }
        
        public String nombre { get; set; }
        public String TREFERENCE { get; set; }
        public String ACNT_CODE { get; set; }
        public String PERIOD { get; set; }
    
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
        public CambiaLinea()
        {
            InitializeComponent();
        }

        public CambiaLinea(String dc, String anal0, String anal1, String anal2, String anal3, String anal4, String anal5, String anal6, String anal7, String anal8, String anal9, String tref, String cuenta, String per)
        {
            InitializeComponent();
            D_C = dc;
            ANAL_T0 = anal0;
            ANAL_T1 = anal1;
            ANAL_T2 = anal2;
            ANAL_T3 = anal3;
            ANAL_T4 = anal4;
            ANAL_T5 = anal5;
            ANAL_T6 = anal6;
            ANAL_T7 = anal7;
            ANAL_T8 = anal8;
            ANAL_T9 = anal9;
            TREFERENCE = tref;
            ACNT_CODE = cuenta;
            PERIOD = per;
        }

        private void modificarButton_Click(object sender, EventArgs e)
        {
            if (periodoText.Text.Trim().Equals("") || treferenceText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Primero escribe tu cuenta bancaria", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
               
                        Item itm = (Item)cuentaBox.SelectedItem;
                         ACNT_CODE = itm.Extra.ToString();

                       

                        Item itm1 = (Item)d1combo.SelectedItem;
                        ANAL_T0 = itm1.Name.ToString();

                        Item itm2 = (Item)d2combo.SelectedItem;
                         ANAL_T1 = itm2.Name.ToString();

                        Item itm3 = (Item)d3combo.SelectedItem;
                         ANAL_T2 = itm3.Name.ToString();

                        Item itm4 = (Item)d4combo.SelectedItem;
                         ANAL_T3 = itm4.Name.ToString();

                        Item itm5 = (Item)d5combo.SelectedItem;
                         ANAL_T4 = itm5.Name.ToString();

                        Item itm6 = (Item)d6combo.SelectedItem;
                         ANAL_T5 = itm6.Name.ToString();

                        Item itm7 = (Item)d7combo.SelectedItem;
                         ANAL_T6 = itm7.Name.ToString();

                        Item itm8 = (Item)d8combo.SelectedItem;
                         ANAL_T7 = itm8.Name.ToString();

                        Item itm9 = (Item)d9combo.SelectedItem;
                         ANAL_T8 = itm9.Name.ToString();

                        Item itm10 = (Item)d10combo.SelectedItem;
                         ANAL_T9 = itm10.Name.ToString();

                        TREFERENCE = treferenceText.Text.Trim();
                         PERIOD = periodoText.Text.Trim();
                        
                        Item itmX = (Item)tipoCombo.SelectedItem;
                        int tipo = itmX.Value;
                        if(tipo==1)
                        {
                            D_C = "D";
                        }
                        else
                        { 
                            D_C = "C";
                        }
                        this.Close();
            }
        }

        private void CambiaLinea_Load(object sender, EventArgs e)
        {
            periodoText.Text = PERIOD;
            tipoCombo.Items.Add(new Item("Gasto", 1));
            tipoCombo.Items.Add(new Item("Forma de pago", 2));
            tipoCombo.SelectedIndex = 0;

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
                            if (empiezo == 1)
                            {
                                d1label.Text = LOOKUP;
                            }
                            String queryANL = "SELECT ANL_CODE FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Properties.Settings.Default.sunUnidadDeNegocio + "_ANL_CODE] WHERE ANL_CAT_ID = '" + ANL_CAT_ID + "' order by ANL_CODE asc";

                            using (SqlCommand cmdCheck1 = new SqlCommand(queryANL, connection))
                            {
                                SqlDataReader reader1 = cmdCheck1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        String ANL_CODE = reader1.GetString(0).Trim();
                                        switch (empiezo)
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
                            String ACNT_CODE1 = reader.GetString(0).Trim();
                            cuentaBox.Items.Add(new Item(ACNT_CODE1, empiezo1, ACNT_CODE1));
                            empiezo1++;
                        }
                    }
                }
            }
            catch (Exception ex)
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

            treferenceText.Text = TREFERENCE;
            if (D_C.Equals("D"))
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
    }
}
