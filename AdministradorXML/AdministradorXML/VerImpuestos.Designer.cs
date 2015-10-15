namespace AdministradorXML
{
    partial class VerImpuestos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerImpuestos));
            this.label1 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tipoCombo = new System.Windows.Forms.ComboBox();
            this.impuestosList = new System.Windows.Forms.ListView();
            this.totalImpuestosLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(248, 47);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(342, 45);
            this.periodosCombo.TabIndex = 1;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(665, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo:";
            // 
            // tipoCombo
            // 
            this.tipoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoCombo.FormattingEnabled = true;
            this.tipoCombo.Location = new System.Drawing.Point(835, 50);
            this.tipoCombo.Name = "tipoCombo";
            this.tipoCombo.Size = new System.Drawing.Size(435, 45);
            this.tipoCombo.TabIndex = 3;
            this.tipoCombo.SelectedIndexChanged += new System.EventHandler(this.tipoCombo_SelectedIndexChanged);
            // 
            // impuestosList
            // 
            this.impuestosList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.impuestosList.Location = new System.Drawing.Point(50, 148);
            this.impuestosList.MultiSelect = false;
            this.impuestosList.Name = "impuestosList";
            this.impuestosList.Size = new System.Drawing.Size(1433, 634);
            this.impuestosList.TabIndex = 4;
            this.impuestosList.UseCompatibleStateImageBehavior = false;
            this.impuestosList.SelectedIndexChanged += new System.EventHandler(this.impuestosList_SelectedIndexChanged);
            // 
            // totalImpuestosLabel
            // 
            this.totalImpuestosLabel.AutoSize = true;
            this.totalImpuestosLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalImpuestosLabel.Location = new System.Drawing.Point(519, 107);
            this.totalImpuestosLabel.Name = "totalImpuestosLabel";
            this.totalImpuestosLabel.Size = new System.Drawing.Size(0, 38);
            this.totalImpuestosLabel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(428, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Selecciona un impuesto para ver su detalle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1513, 795);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = ".";
            // 
            // VerImpuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1547, 823);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.totalImpuestosLabel);
            this.Controls.Add(this.impuestosList);
            this.Controls.Add(this.tipoCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerImpuestos";
            this.Text = "Ver impuestos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VerImpuestos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipoCombo;
        private System.Windows.Forms.ListView impuestosList;
        private System.Windows.Forms.Label totalImpuestosLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}