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
namespace Liga
{
    public partial class cantidadALigarXML : Form
    {
        private double cantidadDisponible { get; set; }
        public double cantidadLigada { get; set; }
        public String folioFiscalGlobal { get; set; }
    
        public cantidadALigarXML()
        {
            InitializeComponent();
            cantidadDisponible = 0;
        }

        public cantidadALigarXML(double x, String folio)
        {
            InitializeComponent();
            cantidadDisponible = x;
            folioFiscalGlobal = folio;
        }
        private void cantidadALigarXML_Load(object sender, EventArgs e)
        {
            puedesLigar.Text = "Puedes ligar: $ " + cantidadDisponible;
            cantidadLigadaText.Text = cantidadDisponible.ToString();
            cantidadLigadaText.KeyPress+=cantidadLigadaText_KeyPress;
        }
        private void cantidadLigadaText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ligar();
            }
        }
        private void ligar ()
        {
            try
            {
                cantidadLigada = Math.Round(Convert.ToDouble(cantidadLigadaText.Text), 2);
                if (cantidadLigada > cantidadDisponible)
                {
                    System.Windows.Forms.MessageBox.Show("Error, solo falta de asignar a este movimiento: $ " + cantidadDisponible + " y estas tratando de asignar: $ " + cantidadLigada, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    this.cantidadLigada = cantidadLigada;
                    if(descartarCheck.Checked)
                    {
                           String connString = "Database=" + Properties.Settings.Default.databaseFiscal + ";Data Source=" + Properties.Settings.Default.sunDatasource + ";Integrated Security=False;MultipleActiveResultSets=true;User ID='" + Properties.Settings.Default.user + "';Password='" + Properties.Settings.Default.password + "';connect timeout = 60";
                           try
                           {
                               using (SqlConnection connection = new SqlConnection(connString))
                               {
                                   connection.Open();
                                   String query = "UPDATE [" + Properties.Settings.Default.databaseFiscal + "].[dbo].[facturacion_XML] set ocultaEnLigar = 1 WHERE folioFiscal= '"+folioFiscalGlobal+"'";
                                   SqlCommand cmd = new SqlCommand(query, connection);
                                   cmd.ExecuteNonQuery();
                               }
                           }
                           catch(Exception ex)
                           {
                               System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Sunplusito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void ligarButton_Click(object sender, EventArgs e)
        {
            ligar();
        }
    }
}
