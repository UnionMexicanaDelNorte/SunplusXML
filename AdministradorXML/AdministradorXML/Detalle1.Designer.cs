namespace AdministradorXML
{
    partial class Detalle1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalle1));
            this.tipoDeFacturasLabel = new System.Windows.Forms.Label();
            this.facturasList = new System.Windows.Forms.ListView();
            this.ligadasList = new System.Windows.Forms.ListView();
            this.tipoDeEnlazadosLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tipoDeFacturasLabel
            // 
            this.tipoDeFacturasLabel.AutoSize = true;
            this.tipoDeFacturasLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoDeFacturasLabel.Location = new System.Drawing.Point(44, 58);
            this.tipoDeFacturasLabel.Name = "tipoDeFacturasLabel";
            this.tipoDeFacturasLabel.Size = new System.Drawing.Size(103, 38);
            this.tipoDeFacturasLabel.TabIndex = 0;
            this.tipoDeFacturasLabel.Text = "label1";
            // 
            // facturasList
            // 
            this.facturasList.Location = new System.Drawing.Point(51, 116);
            this.facturasList.MultiSelect = false;
            this.facturasList.Name = "facturasList";
            this.facturasList.Size = new System.Drawing.Size(1625, 638);
            this.facturasList.TabIndex = 1;
            this.facturasList.UseCompatibleStateImageBehavior = false;
            this.facturasList.SelectedIndexChanged += new System.EventHandler(this.facturasList_SelectedIndexChanged);
            // 
            // ligadasList
            // 
            this.ligadasList.Location = new System.Drawing.Point(51, 804);
            this.ligadasList.MultiSelect = false;
            this.ligadasList.Name = "ligadasList";
            this.ligadasList.Size = new System.Drawing.Size(1625, 477);
            this.ligadasList.TabIndex = 2;
            this.ligadasList.UseCompatibleStateImageBehavior = false;
            this.ligadasList.SelectedIndexChanged += new System.EventHandler(this.ligadasList_SelectedIndexChanged);
            // 
            // tipoDeEnlazadosLabel
            // 
            this.tipoDeEnlazadosLabel.AutoSize = true;
            this.tipoDeEnlazadosLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoDeEnlazadosLabel.Location = new System.Drawing.Point(53, 757);
            this.tipoDeEnlazadosLabel.Name = "tipoDeEnlazadosLabel";
            this.tipoDeEnlazadosLabel.Size = new System.Drawing.Size(103, 38);
            this.tipoDeEnlazadosLabel.TabIndex = 3;
            this.tipoDeEnlazadosLabel.Text = "label1";
            // 
            // Detalle1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1609, 876);
            this.Controls.Add(this.tipoDeEnlazadosLabel);
            this.Controls.Add(this.ligadasList);
            this.Controls.Add(this.facturasList);
            this.Controls.Add(this.tipoDeFacturasLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Detalle1";
            this.Text = "Detalle 1";
            this.Load += new System.EventHandler(this.Detalle1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tipoDeFacturasLabel;
        private System.Windows.Forms.ListView facturasList;
        private System.Windows.Forms.ListView ligadasList;
        private System.Windows.Forms.Label tipoDeEnlazadosLabel;
    }
}