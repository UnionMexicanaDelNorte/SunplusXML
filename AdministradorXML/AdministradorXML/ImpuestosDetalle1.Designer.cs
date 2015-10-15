namespace AdministradorXML
{
    partial class ImpuestosDetalle1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpuestosDetalle1));
            this.cambiarLabel = new System.Windows.Forms.Label();
            this.rfcImpuestosList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cambiarLabel
            // 
            this.cambiarLabel.AutoSize = true;
            this.cambiarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cambiarLabel.Location = new System.Drawing.Point(48, 35);
            this.cambiarLabel.Name = "cambiarLabel";
            this.cambiarLabel.Size = new System.Drawing.Size(103, 38);
            this.cambiarLabel.TabIndex = 0;
            this.cambiarLabel.Text = "label1";
            // 
            // rfcImpuestosList
            // 
            this.rfcImpuestosList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfcImpuestosList.Location = new System.Drawing.Point(55, 122);
            this.rfcImpuestosList.Name = "rfcImpuestosList";
            this.rfcImpuestosList.Size = new System.Drawing.Size(1611, 741);
            this.rfcImpuestosList.TabIndex = 1;
            this.rfcImpuestosList.UseCompatibleStateImageBehavior = false;
            this.rfcImpuestosList.SelectedIndexChanged += new System.EventHandler(this.rfcImpuestosList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1682, 867);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(384, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecciona un RFC para ver su detalle";
            // 
            // ImpuestosDetalle1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1712, 902);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rfcImpuestosList);
            this.Controls.Add(this.cambiarLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImpuestosDetalle1";
            this.Text = "Impuestos detalle por RFC";
            this.Load += new System.EventHandler(this.ImpuestosDetalle1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cambiarLabel;
        private System.Windows.Forms.ListView rfcImpuestosList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}