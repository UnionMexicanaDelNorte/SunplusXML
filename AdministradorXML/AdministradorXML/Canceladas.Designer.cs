namespace AdministradorXML
{
    partial class Canceladas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Canceladas));
            this.label1 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.canceladasList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.tipoCombo = new System.Windows.Forms.ComboBox();
            this.totalCanceladasLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(202, 35);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(395, 45);
            this.periodosCombo.TabIndex = 1;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // canceladasList
            // 
            this.canceladasList.Location = new System.Drawing.Point(37, 156);
            this.canceladasList.Name = "canceladasList";
            this.canceladasList.Size = new System.Drawing.Size(121, 97);
            this.canceladasList.TabIndex = 2;
            this.canceladasList.UseCompatibleStateImageBehavior = false;
            this.canceladasList.SelectedIndexChanged += new System.EventHandler(this.canceladasList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(635, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo:";
            // 
            // tipoCombo
            // 
            this.tipoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoCombo.FormattingEnabled = true;
            this.tipoCombo.Location = new System.Drawing.Point(746, 38);
            this.tipoCombo.Name = "tipoCombo";
            this.tipoCombo.Size = new System.Drawing.Size(302, 45);
            this.tipoCombo.TabIndex = 4;
            this.tipoCombo.SelectedIndexChanged += new System.EventHandler(this.tipoCombo_SelectedIndexChanged);
            // 
            // totalCanceladasLabel
            // 
            this.totalCanceladasLabel.AutoSize = true;
            this.totalCanceladasLabel.Location = new System.Drawing.Point(539, 285);
            this.totalCanceladasLabel.Name = "totalCanceladasLabel";
            this.totalCanceladasLabel.Size = new System.Drawing.Size(0, 26);
            this.totalCanceladasLabel.TabIndex = 5;
            // 
            // Canceladas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1107, 628);
            this.Controls.Add(this.totalCanceladasLabel);
            this.Controls.Add(this.tipoCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.canceladasList);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Canceladas";
            this.Text = "Facturas canceladas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Canceladas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.ListView canceladasList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipoCombo;
        private System.Windows.Forms.Label totalCanceladasLabel;
    }
}