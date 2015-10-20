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
    public partial class ImpuestosDetalle1 : Form
    {
        public String periodo { get; set; }
        public String impuesto { get; set; }
        public int tipo { get; set; }
        public List<Dictionary<string, object>> listaFinal { get; set; }
        public int tipoImpuesto { get; set; }
     
        public ImpuestosDetalle1()
        {
            InitializeComponent();
        }
        public ImpuestosDetalle1(String p, String i, int t, int tp)
        {
            InitializeComponent();
            periodo = p;
            impuesto = i;
            tipo = t;
            tipoImpuesto = tp;
        }

        private void ImpuestosDetalle1_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
            String append = "";
            if (tipoImpuesto == 1)
            {
                append = " del tipo de impuesto Traslados";
            }
            else
            {
                append = " del tipo de impuesto Retenciones";
            }
            if(tipo==1)
            {
                cambiarLabel.Text=""+impuesto+" del periodo: "+periodo+" del gasto"+append;
            }
            else
            {
                cambiarLabel.Text=""+impuesto+" del periodo: "+periodo+" de ingreso"+append;
            }
            String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    String queryXML = "SELECT MAX(f.rfc) as rfc, MAX(f.razonSocial) as razonSocial,SUM( i.importe) as importe FROM [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] f INNER JOIN [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[impuestos] i on i.folioFiscal = f.folioFiscal WHERE f.STATUS = '" + tipo + "' AND SUBSTRING( CAST(f.fechaExpedicion AS NVARCHAR(10)),1," + periodo.Length + ") = '" + periodo + "' AND i.impuesto = '" + impuesto + "' AND i.tipo = '" + tipoImpuesto + "' GROUP BY f.rfc order by f.rfc asc";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                String rfc = reader.GetString(0);
                                String razonSocial = reader.GetString(1);
                                double importe = reader.GetDouble(2);

                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("rfc", rfc);
                                dictionary.Add("razonSocial", razonSocial);
                                dictionary.Add("importe", importe);
                                listaFinal.Add(dictionary);
                            }
                            rfcImpuestosList.View = View.Details;
                            rfcImpuestosList.GridLines = true;
                            rfcImpuestosList.FullRowSelect = true;
                            rfcImpuestosList.Columns.Add("RFC", 150);
                            rfcImpuestosList.Columns.Add("Razon Social", 400);
                            rfcImpuestosList.Columns.Add("Importe", 200);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("rfc"))
                                {
                                    string[] arr = new string[12];
                                    ListViewItem itm3;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["rfc"]);
                                    arr[1] = Convert.ToString(dic["razonSocial"]);
                                    arr[2] = String.Format("{0:n}", Convert.ToDouble(dic["importe"]));
                                    itm3 = new ListViewItem(arr);
                                    rfcImpuestosList.Items.Add(itm3);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rfcImpuestosList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rfcImpuestosList.SelectedItems.Count>0)
            {
                String rfc = rfcImpuestosList.SelectedItems[0].SubItems[0].Text.Trim();
                ImpuestosDetalle2 form = new ImpuestosDetalle2(periodo, impuesto, tipo, rfc, tipoImpuesto);
                form.ShowDialog();
            }
        }
    }
}
