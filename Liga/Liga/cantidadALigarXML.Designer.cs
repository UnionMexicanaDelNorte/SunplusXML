namespace Liga
{
    partial class cantidadALigarXML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cantidadALigarXML));
            this.puedesLigar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cantidadLigadaText = new System.Windows.Forms.TextBox();
            this.ligarButton = new System.Windows.Forms.Button();
            this.descartarCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // puedesLigar
            // 
            this.puedesLigar.AutoSize = true;
            this.puedesLigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.puedesLigar.Location = new System.Drawing.Point(51, 47);
            this.puedesLigar.Name = "puedesLigar";
            this.puedesLigar.Size = new System.Drawing.Size(288, 38);
            this.puedesLigar.TabIndex = 0;
            this.puedesLigar.Text = "Puedes ligar $0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cantidad a ligar:";
            // 
            // cantidadLigadaText
            // 
            this.cantidadLigadaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadLigadaText.Location = new System.Drawing.Point(359, 141);
            this.cantidadLigadaText.Name = "cantidadLigadaText";
            this.cantidadLigadaText.Size = new System.Drawing.Size(254, 44);
            this.cantidadLigadaText.TabIndex = 2;
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(680, 135);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(191, 55);
            this.ligarButton.TabIndex = 3;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // descartarCheck
            // 
            this.descartarCheck.AutoSize = true;
            this.descartarCheck.Location = new System.Drawing.Point(60, 256);
            this.descartarCheck.Name = "descartarCheck";
            this.descartarCheck.Size = new System.Drawing.Size(673, 30);
            this.descartarCheck.TabIndex = 4;
            this.descartarCheck.Text = "Descartar Factura (la factura ya no aparecerá en el menú de ligar)";
            this.descartarCheck.UseVisualStyleBackColor = true;
            // 
            // cantidadALigarXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 347);
            this.Controls.Add(this.descartarCheck);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.cantidadLigadaText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.puedesLigar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cantidadALigarXML";
            this.Text = "Cantidad a Ligar de la Factura XML";
            this.Load += new System.EventHandler(this.cantidadALigarXML_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label puedesLigar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cantidadLigadaText;
        private System.Windows.Forms.Button ligarButton;
        private System.Windows.Forms.CheckBox descartarCheck;
    }
}