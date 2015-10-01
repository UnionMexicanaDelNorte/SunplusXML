namespace Liga
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lineaLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.cantidadLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.rfcTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.razonSocialText = new System.Windows.Forms.TextBox();
            this.listaDeCandidatos = new System.Windows.Forms.ListView();
            this.labelDiario4 = new System.Windows.Forms.Label();
            this.todaLaCantidadCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.facturasAsignadasList = new System.Windows.Forms.ListView();
            this.ligarButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.unidadDeNegocioLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cuentaLabel = new System.Windows.Forms.Label();
            this.detectLabel = new System.Windows.Forms.Label();
            this.debitCreditLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Linea:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cantidad:";
            // 
            // lineaLabel
            // 
            this.lineaLabel.AutoSize = true;
            this.lineaLabel.Location = new System.Drawing.Point(115, 49);
            this.lineaLabel.Name = "lineaLabel";
            this.lineaLabel.Size = new System.Drawing.Size(24, 26);
            this.lineaLabel.TabIndex = 3;
            this.lineaLabel.Text = "0";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(332, 49);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(24, 26);
            this.sourceLabel.TabIndex = 4;
            this.sourceLabel.Text = "0";
            // 
            // cantidadLabel
            // 
            this.cantidadLabel.AutoSize = true;
            this.cantidadLabel.Location = new System.Drawing.Point(529, 49);
            this.cantidadLabel.Name = "cantidadLabel";
            this.cantidadLabel.Size = new System.Drawing.Size(24, 26);
            this.cantidadLabel.TabIndex = 5;
            this.cantidadLabel.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1669, 43);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // bDToolStripMenuItem
            // 
            this.bDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem});
            this.bDToolStripMenuItem.Name = "bDToolStripMenuItem";
            this.bDToolStripMenuItem.Size = new System.Drawing.Size(59, 39);
            this.bDToolStripMenuItem.Text = "BD";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(246, 40);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuraciónToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "RFC:";
            // 
            // rfcTextBox
            // 
            this.rfcTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfcTextBox.Location = new System.Drawing.Point(149, 149);
            this.rfcTextBox.Name = "rfcTextBox";
            this.rfcTextBox.Size = new System.Drawing.Size(352, 44);
            this.rfcTextBox.TabIndex = 8;
            this.rfcTextBox.TextChanged += new System.EventHandler(this.rfcTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "Razón Social:";
            // 
            // razonSocialText
            // 
            this.razonSocialText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.razonSocialText.Location = new System.Drawing.Point(228, 244);
            this.razonSocialText.Name = "razonSocialText";
            this.razonSocialText.Size = new System.Drawing.Size(1149, 44);
            this.razonSocialText.TabIndex = 10;
            this.razonSocialText.TextChanged += new System.EventHandler(this.razonSocialText_TextChanged);
            // 
            // listaDeCandidatos
            // 
            this.listaDeCandidatos.Location = new System.Drawing.Point(43, 416);
            this.listaDeCandidatos.MultiSelect = false;
            this.listaDeCandidatos.Name = "listaDeCandidatos";
            this.listaDeCandidatos.Size = new System.Drawing.Size(1334, 486);
            this.listaDeCandidatos.TabIndex = 11;
            this.listaDeCandidatos.UseCompatibleStateImageBehavior = false;
            this.listaDeCandidatos.SelectedIndexChanged += new System.EventHandler(this.listaDeCandidatos_SelectedIndexChanged);
            // 
            // labelDiario4
            // 
            this.labelDiario4.AutoSize = true;
            this.labelDiario4.BackColor = System.Drawing.Color.White;
            this.labelDiario4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDiario4.Location = new System.Drawing.Point(564, 152);
            this.labelDiario4.Name = "labelDiario4";
            this.labelDiario4.Size = new System.Drawing.Size(413, 38);
            this.labelDiario4.TabIndex = 12;
            this.labelDiario4.Text = "Cantidad que falta a ligar: $";
            // 
            // todaLaCantidadCheckBox
            // 
            this.todaLaCantidadCheckBox.AutoSize = true;
            this.todaLaCantidadCheckBox.Location = new System.Drawing.Point(43, 315);
            this.todaLaCantidadCheckBox.Name = "todaLaCantidadCheckBox";
            this.todaLaCantidadCheckBox.Size = new System.Drawing.Size(525, 30);
            this.todaLaCantidadCheckBox.TabIndex = 13;
            this.todaLaCantidadCheckBox.Text = "Tomar toda la cantidad del CFDI automáticamente";
            this.todaLaCantidadCheckBox.UseVisualStyleBackColor = true;
            this.todaLaCantidadCheckBox.CheckedChanged += new System.EventHandler(this.todaLaCantidadCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 26);
            this.label6.TabIndex = 14;
            this.label6.Text = "Facturas Posibles:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 924);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 26);
            this.label7.TabIndex = 15;
            this.label7.Text = "Facturas Asignadas:";
            // 
            // facturasAsignadasList
            // 
            this.facturasAsignadasList.Location = new System.Drawing.Point(48, 978);
            this.facturasAsignadasList.MultiSelect = false;
            this.facturasAsignadasList.Name = "facturasAsignadasList";
            this.facturasAsignadasList.Size = new System.Drawing.Size(1329, 267);
            this.facturasAsignadasList.TabIndex = 16;
            this.facturasAsignadasList.UseCompatibleStateImageBehavior = false;
            this.facturasAsignadasList.SelectedIndexChanged += new System.EventHandler(this.facturasAsignadasList_SelectedIndexChanged);
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(715, 315);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(662, 70);
            this.ligarButton.TabIndex = 17;
            this.ligarButton.Text = "Ligar Factura(s) al movimiento contable";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(689, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 26);
            this.label8.TabIndex = 18;
            this.label8.Text = "Unidad de Negocio:";
            // 
            // unidadDeNegocioLabel
            // 
            this.unidadDeNegocioLabel.AutoSize = true;
            this.unidadDeNegocioLabel.Location = new System.Drawing.Point(907, 49);
            this.unidadDeNegocioLabel.Name = "unidadDeNegocioLabel";
            this.unidadDeNegocioLabel.Size = new System.Drawing.Size(70, 26);
            this.unidadDeNegocioLabel.TabIndex = 19;
            this.unidadDeNegocioLabel.Text = "label9";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(998, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 26);
            this.label9.TabIndex = 20;
            this.label9.Text = "Cuenta:";
            // 
            // cuentaLabel
            // 
            this.cuentaLabel.AutoSize = true;
            this.cuentaLabel.Location = new System.Drawing.Point(1102, 49);
            this.cuentaLabel.Name = "cuentaLabel";
            this.cuentaLabel.Size = new System.Drawing.Size(82, 26);
            this.cuentaLabel.TabIndex = 21;
            this.cuentaLabel.Text = "label10";
            // 
            // detectLabel
            // 
            this.detectLabel.AutoSize = true;
            this.detectLabel.Location = new System.Drawing.Point(1276, 49);
            this.detectLabel.Name = "detectLabel";
            this.detectLabel.Size = new System.Drawing.Size(82, 26);
            this.detectLabel.TabIndex = 22;
            this.detectLabel.Text = "label10";
            // 
            // debitCreditLabel
            // 
            this.debitCreditLabel.AutoSize = true;
            this.debitCreditLabel.Location = new System.Drawing.Point(1433, 49);
            this.debitCreditLabel.Name = "debitCreditLabel";
            this.debitCreditLabel.Size = new System.Drawing.Size(82, 26);
            this.debitCreditLabel.TabIndex = 23;
            this.debitCreditLabel.Text = "label10";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(685, 1252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 26);
            this.label10.TabIndex = 24;
            this.label10.Text = ".";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1669, 1297);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.debitCreditLabel);
            this.Controls.Add(this.detectLabel);
            this.Controls.Add(this.cuentaLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.unidadDeNegocioLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.facturasAsignadasList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.todaLaCantidadCheckBox);
            this.Controls.Add(this.labelDiario4);
            this.Controls.Add(this.listaDeCandidatos);
            this.Controls.Add(this.razonSocialText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rfcTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cantidadLabel);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.lineaLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Ligar movimiento a factura";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lineaLabel;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label cantidadLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rfcTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox razonSocialText;
        private System.Windows.Forms.ListView listaDeCandidatos;
        private System.Windows.Forms.Label labelDiario4;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.CheckBox todaLaCantidadCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView facturasAsignadasList;
        private System.Windows.Forms.Button ligarButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label unidadDeNegocioLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label cuentaLabel;
        private System.Windows.Forms.Label detectLabel;
        private System.Windows.Forms.Label debitCreditLabel;
        private System.Windows.Forms.Label label10;
    }
}

