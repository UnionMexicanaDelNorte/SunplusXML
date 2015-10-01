using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liga
{
    public partial class cantidadALigarXML : Form
    {
        private double cantidadDisponible { get; set; }
        public double cantidadLigada { get; set; }
      
        public cantidadALigarXML()
        {
            InitializeComponent();
            cantidadDisponible = 0;
        }

        public cantidadALigarXML(double x)
        {
            InitializeComponent();
            cantidadDisponible = x;
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
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void ligarButton_Click(object sender, EventArgs e)
        {
            ligar();
        }
    }
}
