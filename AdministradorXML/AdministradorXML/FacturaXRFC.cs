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
    public partial class FacturaXRFC : Form
    {
        public List<Dictionary<string, object>> listaFinal { get; set; }
      
        public FacturaXRFC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String anio = anoText.Text.Trim();
            String rfc = rfcText.Text.Trim();
            String connStringSun = "Database=" + Properties.Settings.Default.sunDatabase + ";Data Source=" + Properties.Settings.Default.datasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
            listaFinal.Clear();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                using (SqlConnection connection = new SqlConnection(connStringSun))
                {
                    connection.Open();
                    String queryXML = "";
                    queryXML = "SELECT SUM(total) as total, STATUS  FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE rfc = '"+rfc+"' AND SUBSTRING( CAST(fechaExpedicion AS NVARCHAR(11)),1,4) = '"+anio+"' GROUP BY STATUS";
                    using (SqlCommand cmdCheck = new SqlCommand(queryXML, connection))
                    {
                        SqlDataReader reader = cmdCheck.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double total = Math.Round(Convert.ToDouble(Math.Abs(reader.GetDecimal(0))), 2);
                                String STATUS = reader.GetString(1);
                                String palabra = "";
                                if (STATUS.Equals("0"))
                                {
                                    palabra = "Cancelada de Gastos";
                                }
                                if(STATUS.Equals("1"))
                                {
                                    palabra = "Gastos";
                                }
                                if (STATUS.Equals("2"))
                                {
                                    palabra = "Ingresos";
                                }
                                if (STATUS.Equals("3"))
                                {
                                    palabra = "Cancelado de Ingresos";
                                }
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                                dictionary.Add("STATUS", palabra);
                                dictionary.Add("total", total);
                                listaFinal.Add(dictionary);
                            }//while


                            lineasList.Clear();
                            lineasList.View = View.Details;
                            lineasList.GridLines = true;
                            lineasList.FullRowSelect = true;
                            lineasList.Columns.Add("STATUS", 200);
                            lineasList.Columns.Add("Cantidad", 200);
                            foreach (Dictionary<string, object> dic in listaFinal)
                            {
                                if (dic.ContainsKey("STATUS"))
                                {
                                    string[] arr = new string[3];
                                    ListViewItem itm;
                                    //add items to ListView
                                    arr[0] = Convert.ToString(dic["STATUS"]);
                                    arr[1] = String.Format("{0:n}", Convert.ToDouble(dic["total"]));
                                    itm = new ListViewItem(arr);
                                    lineasList.Items.Add(itm);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FacturaXRFC_Load(object sender, EventArgs e)
        {
            listaFinal = new List<Dictionary<string, object>>();
        }

        private void lineasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cuantos = lineasList.SelectedItems.Count;
            if (cuantos > 0)
            {
                String rfc = rfcText.Text;
                String anio = anoText.Text;
                String STATUS = lineasList.SelectedItems[0].SubItems[0].Text.Trim();
                
                Detalle3 form = new Detalle3(rfc, STATUS, anio);
                form.ShowDialog();
                // actualiza();
            }
        }
    }
}
