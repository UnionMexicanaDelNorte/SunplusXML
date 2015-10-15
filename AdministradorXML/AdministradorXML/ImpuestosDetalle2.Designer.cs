namespace AdministradorXML
{
    partial class ImpuestosDetalle2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpuestosDetalle2));
            this.cambiarLabel = new System.Windows.Forms.Label();
            this.facturasList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cambiarLabel
            // 
            this.cambiarLabel.AutoSize = true;
            this.cambiarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cambiarLabel.Location = new System.Drawing.Point(36, 33);
            this.cambiarLabel.Name = "cambiarLabel";
            this.cambiarLabel.Size = new System.Drawing.Size(103, 38);
            this.cambiarLabel.TabIndex = 0;
            this.cambiarLabel.Text = "label1";
            // 
            // facturasList
            // 
            this.facturasList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.facturasList.Location = new System.Drawing.Point(43, 109);
            this.facturasList.Name = "facturasList";
            this.facturasList.Size = new System.Drawing.Size(1823, 827);
            this.facturasList.TabIndex = 1;
            this.facturasList.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1868, 936);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(675, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecciona y da click derecho en una factura para ver su XML y PDF";
            // 
            // ImpuestosDetalle2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1898, 971);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.facturasList);
            this.Controls.Add(this.cambiarLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImpuestosDetalle2";
            this.Text = "Impuestos detalle por facturas";
            this.Load += new System.EventHandler(this.ImpuestosDetalle2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cambiarLabel;
        private System.Windows.Forms.ListView facturasList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}