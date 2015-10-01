namespace AdministradorXML
{
    partial class XMLPolizasDelMes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLPolizasDelMes));
            this.polizasTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tipoSolicitudCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumOrden = new System.Windows.Forms.TextBox();
            this.NumTramiteLalbe = new System.Windows.Forms.Label();
            this.NumTramite = new System.Windows.Forms.TextBox();
            this.generarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // polizasTree
            // 
            this.polizasTree.Location = new System.Drawing.Point(71, 242);
            this.polizasTree.Name = "polizasTree";
            this.polizasTree.Size = new System.Drawing.Size(1616, 848);
            this.polizasTree.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Periodo:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(257, 48);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(271, 45);
            this.periodosCombo.TabIndex = 2;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(585, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo de solicitud:";
            // 
            // tipoSolicitudCombo
            // 
            this.tipoSolicitudCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoSolicitudCombo.FormattingEnabled = true;
            this.tipoSolicitudCombo.Location = new System.Drawing.Point(895, 51);
            this.tipoSolicitudCombo.Name = "tipoSolicitudCombo";
            this.tipoSolicitudCombo.Size = new System.Drawing.Size(276, 45);
            this.tipoSolicitudCombo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 38);
            this.label3.TabIndex = 5;
            this.label3.Text = "# de orden:";
            // 
            // NumOrden
            // 
            this.NumOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumOrden.Location = new System.Drawing.Point(287, 155);
            this.NumOrden.MaxLength = 13;
            this.NumOrden.Name = "NumOrden";
            this.NumOrden.Size = new System.Drawing.Size(241, 44);
            this.NumOrden.TabIndex = 6;
            // 
            // NumTramiteLalbe
            // 
            this.NumTramiteLalbe.AutoSize = true;
            this.NumTramiteLalbe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTramiteLalbe.Location = new System.Drawing.Point(599, 161);
            this.NumTramiteLalbe.Name = "NumTramiteLalbe";
            this.NumTramiteLalbe.Size = new System.Drawing.Size(197, 38);
            this.NumTramiteLalbe.TabIndex = 7;
            this.NumTramiteLalbe.Text = "# de tramite:";
            // 
            // NumTramite
            // 
            this.NumTramite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumTramite.Location = new System.Drawing.Point(895, 161);
            this.NumTramite.MaxLength = 10;
            this.NumTramite.Name = "NumTramite";
            this.NumTramite.Size = new System.Drawing.Size(276, 44);
            this.NumTramite.TabIndex = 8;
            // 
            // generarButton
            // 
            this.generarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarButton.Location = new System.Drawing.Point(1343, 85);
            this.generarButton.Name = "generarButton";
            this.generarButton.Size = new System.Drawing.Size(223, 79);
            this.generarButton.TabIndex = 9;
            this.generarButton.Text = "Generar";
            this.generarButton.UseVisualStyleBackColor = true;
            // 
            // XMLPolizasDelMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1745, 1112);
            this.Controls.Add(this.generarButton);
            this.Controls.Add(this.NumTramite);
            this.Controls.Add(this.NumTramiteLalbe);
            this.Controls.Add(this.NumOrden);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tipoSolicitudCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.polizasTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMLPolizasDelMes";
            this.Text = "XML Polizas del mes";
            this.Load += new System.EventHandler(this.XMLPolizasDelMes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView polizasTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipoSolicitudCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NumOrden;
        private System.Windows.Forms.Label NumTramiteLalbe;
        private System.Windows.Forms.TextBox NumTramite;
        private System.Windows.Forms.Button generarButton;
    }
}