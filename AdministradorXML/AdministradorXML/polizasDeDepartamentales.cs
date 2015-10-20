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
using System.Xml;
using System.Collections.Specialized;
using System.Net;
namespace AdministradorXML
{
    public partial class polizasDeDepartamentales : Form
    {
        System.Windows.Forms.MenuItem menuItem1;
        System.Windows.Forms.MenuItem menuItem2;
        System.Windows.Forms.MenuItem menuItem3;
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
        public polizasDeDepartamentales()
        {
            InitializeComponent();
        }
        private void MarcarComoReembolso(object sender, EventArgs e)
        {
            if (prepolizaList.SelectedItems.Count > 0)
            {
                String r = prepolizaList.SelectedItems[0].SubItems[17].Text.Trim();
                if(r.Equals("0"))
                {
                    prepolizaList.SelectedItems[0].SubItems[17].Text = "1";
                }
                else
                {
                    prepolizaList.SelectedItems[0].SubItems[17].Text = "0";
                }
                
            }
        }
        private void EliminarLinea(object sender, EventArgs e)
        {
            if (prepolizaList.SelectedItems.Count > 0)
            {
                prepolizaList.Items.Remove(prepolizaList.SelectedItems[0]);
            }
        }
        private void CambiaLinea(object sender, EventArgs e)
        {
            if(prepolizaList.SelectedItems.Count>0)
            {
                String ACNT_CODE = prepolizaList.SelectedItems[0].SubItems[0].Text.Trim();
                String D_C = prepolizaList.SelectedItems[0].SubItems[1].Text.Trim();
                String PERIOD = prepolizaList.SelectedItems[0].SubItems[2].Text.Trim();
                String TREFERENCE = prepolizaList.SelectedItems[0].SubItems[3].Text.Trim();
                String ANAL_T0 = prepolizaList.SelectedItems[0].SubItems[5].Text.Trim();
                String ANAL_T1 = prepolizaList.SelectedItems[0].SubItems[6].Text.Trim();
                String ANAL_T2 = prepolizaList.SelectedItems[0].SubItems[7].Text.Trim();
                String ANAL_T3 = prepolizaList.SelectedItems[0].SubItems[8].Text.Trim();
                String ANAL_T4 = prepolizaList.SelectedItems[0].SubItems[9].Text.Trim();
                String ANAL_T5 = prepolizaList.SelectedItems[0].SubItems[10].Text.Trim();
                String ANAL_T6 = prepolizaList.SelectedItems[0].SubItems[11].Text.Trim();
                String ANAL_T7 = prepolizaList.SelectedItems[0].SubItems[12].Text.Trim();
                String ANAL_T8 = prepolizaList.SelectedItems[0].SubItems[13].Text.Trim();
                String ANAL_T9 = prepolizaList.SelectedItems[0].SubItems[14].Text.Trim();


                CambiaLinea linea = new CambiaLinea(D_C, ANAL_T0, ANAL_T1, ANAL_T2, ANAL_T3, ANAL_T4, ANAL_T5, ANAL_T6, ANAL_T7, ANAL_T8, ANAL_T9,TREFERENCE,ACNT_CODE,PERIOD);
                linea.ShowDialog();
                prepolizaList.SelectedItems[0].SubItems[0].Text = linea.ACNT_CODE;
                prepolizaList.SelectedItems[0].SubItems[1].Text = linea.D_C;
                prepolizaList.SelectedItems[0].SubItems[2].Text = linea.PERIOD;
                prepolizaList.SelectedItems[0].SubItems[3].Text = linea.TREFERENCE;
                prepolizaList.SelectedItems[0].SubItems[5].Text = linea.ANAL_T0;
                prepolizaList.SelectedItems[0].SubItems[6].Text = linea.ANAL_T1;
                prepolizaList.SelectedItems[0].SubItems[7].Text = linea.ANAL_T2;
                prepolizaList.SelectedItems[0].SubItems[8].Text = linea.ANAL_T3;
                prepolizaList.SelectedItems[0].SubItems[9].Text = linea.ANAL_T4;
                prepolizaList.SelectedItems[0].SubItems[10].Text = linea.ANAL_T5;
                prepolizaList.SelectedItems[0].SubItems[11].Text = linea.ANAL_T6;
                prepolizaList.SelectedItems[0].SubItems[12].Text = linea.ANAL_T7;
                prepolizaList.SelectedItems[0].SubItems[13].Text = linea.ANAL_T8;
                prepolizaList.SelectedItems[0].SubItems[14].Text = linea.ANAL_T9;
                
            }
        }

        private void polizasDeDepartamentales_Load(object sender, EventArgs e)
        {


            contextMenu2 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
             menuItem2 = new System.Windows.Forms.MenuItem();
             menuItem3 = new System.Windows.Forms.MenuItem();



             contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem2,menuItem3, menuItem1 });
            menuItem2.Index = 0;
            menuItem2.Text = "Cambiar linea";
            menuItem2.Click += CambiaLinea;

            menuItem3.Index = 1;
            menuItem3.Text = "Eliminar linea";
            menuItem3.Click += EliminarLinea;

            menuItem1.Index = 2;
            menuItem1.Text = "Marcar como reeembolso";
            menuItem1.Click += MarcarComoReembolso;
            prepolizaList.ContextMenu = contextMenu2;

            listaFinal = new List<Dictionary<string, object>>();
         
            String connString = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";

            String queryCuentas = "SELECT ANL_CODE,LOOKUP FROM [" + Properties.Settings.Default.sunDatabase + "].[dbo].[" + Login.unidadDeNegocioGlobal+ "_ANL_CODE] WHERE ANL_CAT_ID= '07' AND SUBSTRING( ANL_CODE,1,2) = 'ER' order by ANL_CODE asc";
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
            personaCombo.SelectedIndex = 0;
          
        }

        private void actualizaPeriodos()
        {

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            prepolizaList.Clear();
            listaFinal.Clear();
            Item itm = (Item)personaCombo.SelectedItem;
            String WHO = itm.Extra.ToString();


            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //conceptos 
                    String queryXML = "SELECT DISTINCT SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(11)),1,7) as periodo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_prepolizas] p INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f on f.folioFiscal = p.UUID WHERE p.WHO = '" + WHO + "' AND p.contabilizado = 0 order by periodo asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            periodosCombo.Items.Clear();
                            while (reader.Read())
                            {
                                String periodo = reader.GetString(0).Trim();
                                periodosCombo.Items.Add(new Item(periodo, 0, periodo));
                            }
                            periodosCombo.SelectedIndex = 0;
                        }//if reader
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Esa persona no tiene facturas informadas", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void personaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //actualizaPeriodos();
            actualiza();
        }

        private void periodosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualiza();
        }
        private void actualiza()
        {

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            prepolizaList.Clear();
            listaFinal.Clear();
            Item itm = (Item)personaCombo.SelectedItem;
            String WHO = itm.Extra.ToString();

              //Item itm1 = (Item)periodosCombo.SelectedItem;
            //String periodo = itm1.Extra.ToString();
           
            String FNCT = "";
            String PROJ = "";
            String DTLS = "";
            //obtengo el DTLS
            String theDate = dateTimePicker1.Value.ToShortDateString();
            String year = theDate.Substring(6, 4);
            String month = theDate.Substring(3, 2);
            String day = theDate.Substring(0, 2);
            String transdate = day + "-" + month + "-" + year;
            String periodoParaQuery = year + "0" + month;
            int m = Convert.ToInt32(month);
            

            switch(m)
            {
                case 1:
                    DTLS = "PENE";
                break;
                case 2:
                    DTLS = "PFEB";
                break;
                case 3:
                    DTLS = "PMAR";
                break;
                case 4:
                    DTLS = "PABR";
                break;
                case 5:
                    DTLS = "PMAY";
                break;
                case 6:
                    DTLS = "PJUN";
                break;
                case 7:
                    DTLS = "PJUL";
                break;
                case 8:
                    DTLS = "PAGO";
                break;
                case 9:
                    DTLS = "PSEP";
                break;
                case 10:
                    DTLS = "POCT";
                break;
                case 11:
                    DTLS = "PNOV";
                break;
                case 12:
                    DTLS = "PDIC";
                break;
            }

            

            //obtiene el fnct
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //conceptos 
                    String queryXML = "SELECT FNCT FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_FNCTyWHO] WHERE WHO = '"+WHO+"'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FNCT = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //obtiene el proj
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    //conceptos 
                    String queryXML = "SELECT PROJ FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_PROJyWHO] WHERE WHO = '" + WHO + "'";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PROJ = reader.GetString(0);
                            }
                        }
                    }
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
                    //prepoliza
                   // String queryXML = "SELECT  p.WHO,p.UUID,p.amount,p.idConceptoGasto,p.contabilizado,p.idConceptoRecurso,cg.tipo as tipog,cg.ACNT_CODE as cuentag,cg.TREFERENCE as treferenceg,cg.ANAL_T0 as d1g,cg.ANAL_T1 as d2g,cg.ANAL_T2 as d3g,cg.ANAL_T3 as d4g,cg.ANAL_T4 as d5g,cg.ANAL_T5 as d6g,cg.ANAL_T6 as d7g,cg.ANAL_T7 as d8g,cg.ANAL_T8 as d9g,cg.ANAL_T9 as d10g,cr.tipo as tipor,cr.ACNT_CODE as cuentar,cr.TREFERENCE as treferencer,cr.ANAL_T0 as d1r,cr.ANAL_T1 as d2r,cr.ANAL_T2 as d3r,cr.ANAL_T3 as d4r,cr.ANAL_T4 as d5r,cr.ANAL_T5 as d6r,cr.ANAL_T6 as d7r,cr.ANAL_T7 as d8r,cr.ANAL_T8 as d9r,cr.ANAL_T9 as d10r, SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(11)),1,7) as periodo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_prepolizas] p INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] cg on cg.idConcepto = p.idConceptoGasto INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] cr on cr.idConcepto = p.idConceptoRecurso INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f on f.folioFiscal = p.UUID WHERE p.WHO = '"+WHO+"' AND p.contabilizado = 0 AND SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(11)),1,7) = '"+periodo+"'";
                    String queryXML = "SELECT  p.WHO,p.UUID,p.amount,p.idConceptoGasto,p.contabilizado,p.idConceptoRecurso,cg.tipo as tipog,cg.ACNT_CODE as cuentag,cg.TREFERENCE as treferenceg,cg.ANAL_T0 as d1g,cg.ANAL_T1 as d2g,cg.ANAL_T2 as d3g,cg.ANAL_T3 as d4g,cg.ANAL_T4 as d5g,cg.ANAL_T5 as d6g,cg.ANAL_T6 as d7g,cg.ANAL_T7 as d8g,cg.ANAL_T8 as d9g,cg.ANAL_T9 as d10g,cr.tipo as tipor,cr.ACNT_CODE as cuentar,cr.TREFERENCE as treferencer,cr.ANAL_T0 as d1r,cr.ANAL_T1 as d2r,cr.ANAL_T2 as d3r,cr.ANAL_T3 as d4r,cr.ANAL_T4 as d5r,cr.ANAL_T5 as d6r,cr.ANAL_T6 as d7r,cr.ANAL_T7 as d8r,cr.ANAL_T8 as d9r,cr.ANAL_T9 as d10r, SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(11)),1,7) as periodo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_prepolizas] p INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] cg on cg.idConcepto = p.idConceptoGasto INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_conceptos] cr on cr.idConcepto = p.idConceptoRecurso INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f on f.folioFiscal = p.UUID WHERE p.WHO = '" + WHO + "' AND p.contabilizado = 0";
                    
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            bool first = true;
                            while (reader.Read())
                            {
                                String UUID = reader.GetString(1).ToString().Trim();
                               
                                double amount = Math.Abs( Convert.ToDouble( reader.GetDecimal(2)));
                                String D_Cg = "C";
                                int tipog = reader.GetInt32(6);
                                if(tipog==1)//gasto D
                                {
                                    D_Cg = "D";
                                }

                                String cuentag = reader.GetString(7).ToString().Trim();
                                String treferenceg = reader.GetString(8).ToString().Trim();
                                String d1g = reader.GetString(9).ToString().Trim();
                                String d2g = reader.GetString(10).ToString().Trim();
                                String d3g = reader.GetString(11).ToString().Trim();
                                String d4g = reader.GetString(12).ToString().Trim();
                                String d5g = reader.GetString(13).ToString().Trim();
                                String d6g = reader.GetString(14).ToString().Trim();
                                String d7g = reader.GetString(15).ToString().Trim();
                                String d8g = reader.GetString(16).ToString().Trim();
                                String d9g = reader.GetString(17).ToString().Trim();
                                String d10g = reader.GetString(18).ToString().Trim();
                                if(first)
                                {
                                    first = false;
                                    conceptoText.Text = treferenceg;
                                }
                              
                                if(d4g.Equals("<fnct>"))
                                {
                                    d4g = FNCT;
                                }
                                if (d7g.Equals("<who>"))
                                {
                                    d7g = WHO;
                                }
                                if (d9g.Equals("<proj>"))
                                {
                                    d9g = PROJ;
                                }
                                if (d10g.Equals("<dtls>"))
                                {
                                    d10g = DTLS;
                                }
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("Cuenta", cuentag);
                                dictionary.Add("D_C", D_Cg);
                                dictionary.Add("PERIOD", periodoParaQuery);
                                dictionary.Add("TREFERENCE", treferenceg);
                                dictionary.Add("Amount", amount);
                                dictionary.Add("REF2", d1g);
                                dictionary.Add("TFWW", d2g);
                                dictionary.Add("FUND", d3g);
                                dictionary.Add("FNCT", d4g);
                                dictionary.Add("RSTR", d5g);
                                dictionary.Add("ORGID", d6g);
                                dictionary.Add("WHO", d7g);
                                dictionary.Add("FLAG", d8g);
                                dictionary.Add("PROJ", d9g);
                                dictionary.Add("DTLS", d10g);
                                dictionary.Add("transdate", transdate);
                                dictionary.Add("UUID", UUID);
                                listaFinal.Add(dictionary);



                                String D_Cr = "C";
                                int tipor = reader.GetInt32(19);
                                if (tipor == 1)//gasto D
                                {
                                    D_Cr = "D";
                                }

                                String cuentar = reader.GetString(20).ToString().Trim();
                                String treferencer = reader.GetString(21).ToString().Trim();
                                String d1r = reader.GetString(22).ToString().Trim();
                                String d2r = reader.GetString(23).ToString().Trim();
                                String d3r = reader.GetString(24).ToString().Trim();
                                String d4r = reader.GetString(25).ToString().Trim();
                                String d5r = reader.GetString(26).ToString().Trim();
                                String d6r = reader.GetString(27).ToString().Trim();
                                String d7r = reader.GetString(28).ToString().Trim();
                                String d8r = reader.GetString(29).ToString().Trim();
                                String d9r = reader.GetString(30).ToString().Trim();
                                String d10r = reader.GetString(31).ToString().Trim();

                                if (d4r.Equals("<fnct>"))
                                {
                                    d4r = FNCT;
                                }
                                if (d7r.Equals("<who>"))
                                {
                                    d7r = WHO;
                                }
                                if (d9r.Equals("<proj>"))
                                {
                                    d9r = PROJ;
                                }
                                if (d10r.Equals("<dtls>"))
                                {
                                    d10r = DTLS;
                                }
                                Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
                                dictionary1.Add("Cuenta", cuentar);
                                dictionary1.Add("D_C", D_Cr);
                                dictionary1.Add("PERIOD", periodoParaQuery);
                                dictionary1.Add("TREFERENCE", treferencer);
                                dictionary1.Add("Amount", amount);
                                dictionary1.Add("REF2", d1r);
                                dictionary1.Add("TFWW", d2r);
                                dictionary1.Add("FUND", d3r);
                                dictionary1.Add("FNCT", d4r);
                                dictionary1.Add("RSTR", d5r);
                                dictionary1.Add("ORGID", d6r);
                                dictionary1.Add("WHO", d7r);
                                dictionary1.Add("FLAG", d8r);
                                dictionary1.Add("PROJ", d9r);
                                dictionary1.Add("DTLS", d10r);
                                dictionary1.Add("transdate", transdate);
                                dictionary1.Add("UUID", "");//los departamentales no reportan ingresos !!
                                listaFinal.Add(dictionary1);
                            }
                            prepolizaList.View = View.Details;
                            prepolizaList.GridLines = true;
                            prepolizaList.FullRowSelect = true;
                            prepolizaList.Columns.Add("Cuenta", 80);
                            prepolizaList.Columns.Add("D_C", 50);
                            prepolizaList.Columns.Add("PERIOD", 100);
                            prepolizaList.Columns.Add("TREFERENCE", 150);
                            prepolizaList.Columns.Add("Amount", 150);
                            prepolizaList.Columns.Add("REF2", 80);
                            prepolizaList.Columns.Add("TFWW", 80);
                            prepolizaList.Columns.Add("FUND", 80);
                            prepolizaList.Columns.Add("FNCT", 80);
                            prepolizaList.Columns.Add("RSTR", 80);
                            prepolizaList.Columns.Add("ORGID", 80);
                            prepolizaList.Columns.Add("WHO", 80);
                            prepolizaList.Columns.Add("FLAG", 80);
                            prepolizaList.Columns.Add("PROJ", 80);
                            prepolizaList.Columns.Add("DTLS", 80);
                            prepolizaList.Columns.Add("UUID", 0);
                            prepolizaList.Columns.Add("TRANSDATE", 100);
                            prepolizaList.Columns.Add("reembolso", 0);
                         
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("Cuenta"))
                                {
                                    string[] arr = new string[20];
                                    ListViewItem itm3;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["Cuenta"]);
                                    arr[1] = Convert.ToString(dic["D_C"]);
                                    arr[2] = Convert.ToString(dic["PERIOD"]);
                                    arr[3] = Convert.ToString(dic["TREFERENCE"]);
                                    arr[4] = Convert.ToString(dic["Amount"]);
                                    arr[5] = Convert.ToString(dic["REF2"]);
                                    arr[6] = Convert.ToString(dic["TFWW"]);
                                    arr[7] = Convert.ToString(dic["FUND"]);
                                    arr[8] = Convert.ToString(dic["FNCT"]);
                                    arr[9] = Convert.ToString(dic["RSTR"]);
                                    arr[10] = Convert.ToString(dic["ORGID"]);
                                    arr[11] = Convert.ToString(dic["WHO"]);
                                    arr[12] = Convert.ToString(dic["FLAG"]);
                                    arr[13] = Convert.ToString(dic["PROJ"]);
                                    arr[14] = Convert.ToString(dic["DTLS"]);
                                    arr[15] = Convert.ToString(dic["UUID"]);
                                    arr[16] = Convert.ToString(dic["transdate"]);
                                    arr[17] ="0";
                                    itm3 = new ListViewItem(arr);
                                    prepolizaList.Items.Add(itm3);
                                }
                            }
                        }//if reader
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Esa persona no tiene facturas informadas para ese periodo", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void prepolizaList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contabilizarButton_Click(object sender, EventArgs e)
        {
            StringBuilder cad = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><SSC><Payload>");
            cad.Append("<Ledger>");
            int numeroDeLinea = 0;
            Item itm = (Item)personaCombo.SelectedItem;
            String WHO = itm.Extra.ToString();
            double cantidadDeReembolso = 0.0;
            foreach (ListViewItem i in prepolizaList.Items)
            {
                numeroDeLinea++;
                String ACNT_CODE = i.SubItems[0].Text.Trim();
                String D_C = i.SubItems[1].Text.Trim();
                String PERIOD = i.SubItems[2].Text.Trim();
                String y1 = PERIOD.Substring(0, 4);
                String m1 = PERIOD.Substring(4, 3);
                String PERIOD1 = m1 + y1;
                String TREFERENCE = i.SubItems[3].Text.Trim();
                String amount = i.SubItems[4].Text.Trim();
                String ANAL_T0 = i.SubItems[5].Text.Trim();
                String ANAL_T1 = i.SubItems[6].Text.Trim();
                String ANAL_T2 = i.SubItems[7].Text.Trim();
                String ANAL_T3 = i.SubItems[8].Text.Trim();
                String ANAL_T4 = i.SubItems[9].Text.Trim();
                String ANAL_T5 = i.SubItems[10].Text.Trim();
                String ANAL_T6 = i.SubItems[11].Text.Trim();
                String ANAL_T7 = i.SubItems[12].Text.Trim();
                String ANAL_T8 = i.SubItems[13].Text.Trim();
                String ANAL_T9 = i.SubItems[14].Text.Trim();
                String UUID = i.SubItems[15].Text.Trim();
              

                String TRANSDATE = i.SubItems[16].Text.Trim();
                String d = TRANSDATE.Substring(0, 2);
                String m = TRANSDATE.Substring(3, 2);
                String y = TRANSDATE.Substring(6, 4);
                String DATE = d + m + y;
                String r = i.SubItems[17].Text.Trim();
                if(r.Equals("1"))
                {
                    cantidadDeReembolso += Math.Round(Convert.ToDouble(amount),2);
                }

                cad.Append("<Line><AccountCode>" + ACNT_CODE + "</AccountCode><AccountingPeriod>" + PERIOD1 + "</AccountingPeriod>");
                if(!ANAL_T0.Equals(""))
                {
                    cad.Append("<AnalysisCode1>" + ANAL_T0 + "</AnalysisCode1>");
                }
                else
                {
                    cad.Append("<AnalysisCode1></AnalysisCode1>");
                }
                if (!ANAL_T1.Equals(""))
                {
                    cad.Append("<AnalysisCode2>" + ANAL_T1 + "</AnalysisCode2>");
                }
                else
                {
                    cad.Append("<AnalysisCode2></AnalysisCode2>");
                }
                if (!ANAL_T2.Equals(""))
                {
                    cad.Append("<AnalysisCode3>" + ANAL_T2 + "</AnalysisCode3>");
                }
                else
                {
                    cad.Append("<AnalysisCode3></AnalysisCode3>");
                }
                if (!ANAL_T3.Equals(""))
                {
                    cad.Append("<AnalysisCode4>" + ANAL_T3 + "</AnalysisCode4>");
                }
                else
                {
                    cad.Append("<AnalysisCode4></AnalysisCode4>");
                }
                if (!ANAL_T4.Equals(""))
                {
                    cad.Append("<AnalysisCode5>" + ANAL_T4 + "</AnalysisCode5>");
                }
                else
                {
                    cad.Append("<AnalysisCode5></AnalysisCode5>");
                }
                if (!ANAL_T5.Equals(""))
                {
                    cad.Append("<AnalysisCode6>" + ANAL_T5 + "</AnalysisCode6>");
                }
                else
                {
                    cad.Append("<AnalysisCode6></AnalysisCode6>");
                }
                if (!ANAL_T6.Equals(""))
                {
                    cad.Append("<AnalysisCode7>" + ANAL_T6 + "</AnalysisCode7>");
                }
                else
                {
                    cad.Append("<AnalysisCode7></AnalysisCode7>");
                }
                if (!ANAL_T7.Equals(""))
                {
                    cad.Append("<AnalysisCode8>" + ANAL_T7 + "</AnalysisCode8>");
                }
                else
                {
                    cad.Append("<AnalysisCode8></AnalysisCode8>");
                }
                if (!ANAL_T8.Equals(""))
                {
                    cad.Append("<AnalysisCode9>" + ANAL_T8 + "</AnalysisCode9>");
                }
                else
                {
                    cad.Append("<AnalysisCode9></AnalysisCode9>");
                }
                if (!ANAL_T9.Equals(""))
                {
                    cad.Append("<AnalysisCode10>" + ANAL_T9 + "</AnalysisCode10>");
                }
                else
                {
                    cad.Append("<AnalysisCode10></AnalysisCode10>");
                }
                amount = String.Format("{0:n}", Convert.ToDouble(amount));
                      
                cad.Append("<DebitCredit>" + D_C + "</DebitCredit><Description>" + TREFERENCE + "</Description><DueDate>" + DATE + "</DueDate><JournalSource>" + AdministradorXML.Login.sourceGlobal + "</JournalSource><MemoAmount>"+amount+"</MemoAmount><TransactionDate>" + DATE + "</TransactionDate><TransactionReference>" + TREFERENCE + "</TransactionReference><Value4Amount>" + amount + "</Value4Amount><Value4CurrencyCode>MXP1</Value4CurrencyCode><CurrencyCode>MXP1</CurrencyCode><BaseCurrency>MXP1</BaseCurrency></Line>");
            }
            cad.Append("</Ledger></Payload></SSC>");
          //  XmlDocument doc = new XmlDocument();
            //doc.LoadXml(cad.ToString());

            saveFileDialog1.Filter = "XML File|*.xml";
            saveFileDialog1.Title = "Guarda el xml del diario";
            saveFileDialog1.FileName = "diarioParaTRD.xml";
            saveFileDialog1.ShowDialog();
             System.IO.File.WriteAllText(saveFileDialog1.FileName, cad.ToString());

            //doc.Save(saveFileDialog1.FileName);
            //genero el xml de TRD, lo guardo con un save dialog
            //despues inserto las facturas con diario -1
            //marco como contabilizado esas facturas
            System.Windows.Forms.MessageBox.Show("Se ha generado el archivo: " + saveFileDialog1.FileName + " debe de subir la poliza usando el TransferDesk (TRD) y despues venir aqui a capturar el diario resultante.", "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            String carpeta = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
            confirmaNumeroDeDiario form = new confirmaNumeroDeDiario(carpeta);
            form.ShowDialog();
            //ya puso un numero de diario
            numeroDeLinea = 0;
            foreach (ListViewItem i in prepolizaList.Items)
            {
                numeroDeLinea++;
                String amount = i.SubItems[4].Text.Trim();
            
                String UUID = i.SubItems[15].Text.Trim();
                if (!UUID.Equals(""))
                {
                    //insert
                    try
                    {
                        String query1 = "INSERT INTO [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[FISCAL_xml] ( BUNIT,JRNAL_NO,JRNAL_LINE,JRNAL_SOURCE,FOLIO_FISCAL,CONCEPTO,AMOUNT,STATUS,consecutivo) VALUES  ('" + Login.unidadDeNegocioGlobal + "', -1, " + numeroDeLinea + ",'" + Login.sourceGlobal + "','" + UUID + "','" + conceptoText.Text + "'," + amount + ",'1',1) ";/*siempre gasto!! y siempre 1 en el consecutivo*/
                        String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connString))
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand(query1, connection);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                        return;
                    }
                    //change to 1
                    try
                    {
                        String query1 = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_prepolizas] set contabilizado = 1 WHERE UUID = '" + UUID + "' AND WHO = '" + WHO + "'";
                        String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connString))
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand(query1, connection);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                        return;
                    }
                }
            }

            String mensajeParaMandar = "Tu informe ha sido contabilizado.";
            if (cantidadDeReembolso>0)
            {
                mensajeParaMandar=mensajeParaMandar+" Tu reembolso es de: $"+cantidadDeReembolso;
            }
            String connString1 = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
             String token = "";
            int tipo = -1;
            String queryCuentas = "SELECT token,tipo FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[_tokens] WHERE WHO= '" + WHO + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString1))
                {
                    connection.Open();
                    SqlCommand cmdCheck = new SqlCommand(queryCuentas, connection);
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tipo = reader.GetInt32(1);
                            token = reader.GetString(0).Trim();
                           
                            if (tipo == 1)
                            {//ios
                                token = token.Replace("<", "");
                                token = token.Replace(">", "");
                                token = token.Replace(" ", "");
                                token = token.Trim();

                                string URL = "http://unionnorte.org/push/push.php";
                                WebClient webClient = new WebClient();

                                NameValueCollection formData = new NameValueCollection();
                                formData["token"] = token;
                                formData["mensaje"] = mensajeParaMandar;

                                byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
                                string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                                Console.WriteLine(responsefromserver);
                                webClient.Dispose();
                                this.Close();
                            }
                            else
                            {//android
                                string SERVER_API_KEY = "AIzaSyDhONQRcdlOZVliuyhHxebPBSqWWyycVWE";
                                var SENDER_ID = "680915653090";
                                var value = mensajeParaMandar;
                                WebRequest tRequest;
                                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                                tRequest.Method = "post";
                                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + token + "";
                                Console.WriteLine(postData);
                                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                                tRequest.ContentLength = byteArray.Length;

                                Stream dataStream = tRequest.GetRequestStream();
                                dataStream.Write(byteArray, 0, byteArray.Length);
                                dataStream.Close();

                                WebResponse tResponse = tRequest.GetResponse();

                                dataStream = tResponse.GetResponseStream();

                                StreamReader tReader = new StreamReader(dataStream);

                                String sResponseFromServer = tReader.ReadToEnd();


                                tReader.Close();
                                dataStream.Close();
                                tResponse.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
            //y hago un showdialog de que escriba el numero de diario, al escribirlo lo actualizo y cierro las 2 ventanas
        }

        private void nuevaLinea_Click(object sender, EventArgs e)
        {
            Item itm = (Item)personaCombo.SelectedItem;
            String WHO = itm.Extra.ToString();

            string[] arr = new string[19];
            ListViewItem itm3;
            //add items to ListView
            arr[0] = "";
            arr[1] = "";
            arr[2] = "";
            arr[3] = "nueva linea";
            arr[4] = "";
            arr[5] = "";
            arr[6] = "";
            arr[7] = "";
            arr[8] = "";
            arr[9] = "";
            arr[10] = "";
            arr[11] = WHO;
            arr[12] = "";
            arr[13] = "";
            arr[14] = "";
            arr[15] = "";
            arr[16] = "";
            arr[17] = "0";
         
            itm3 = new ListViewItem(arr);
            prepolizaList.Items.Add(itm3);
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            String theDate = dateTimePicker1.Value.ToShortDateString();
            String year = theDate.Substring(6, 4);
            String month = theDate.Substring(3, 2);
            String day = theDate.Substring(0, 2);
            String transdatetime = day + "-" + month + "-" + year;
            String periodParaQuery = year + "0" + month;
            int m = Convert.ToInt32(month);

            String DTLS = "";
            switch (m)
            {
                case 1:
                    DTLS = "PENE";
                    break;
                case 2:
                    DTLS = "PFEB";
                    break;
                case 3:
                    DTLS = "PMAR";
                    break;
                case 4:
                    DTLS = "PABR";
                    break;
                case 5:
                    DTLS = "PMAY";
                    break;
                case 6:
                    DTLS = "PJUN";
                    break;
                case 7:
                    DTLS = "PJUL";
                    break;
                case 8:
                    DTLS = "PAGO";
                    break;
                case 9:
                    DTLS = "PSEP";
                    break;
                case 10:
                    DTLS = "POCT";
                    break;
                case 11:
                    DTLS = "PNOV";
                    break;
                case 12:
                    DTLS = "PDIC";
                    break;
            }
            foreach(ListViewItem i in prepolizaList.Items)
            {
                i.SubItems[2].Text = periodParaQuery;
                i.SubItems[16].Text = transdatetime;
                i.SubItems[14].Text = DTLS;

            }
        }
    }
}
