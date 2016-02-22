namespace AdministradorXML
{
    partial class polizasDeDepartamentales
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(polizasDeDepartamentales));
            this.label1 = new System.Windows.Forms.Label();
            this.personaCombo = new System.Windows.Forms.ComboBox();
            this.prepolizaList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.contabilizarButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nuevaLinea = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.conceptoText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona:";
            // 
            // personaCombo
            // 
            this.personaCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personaCombo.FormattingEnabled = true;
            this.personaCombo.Location = new System.Drawing.Point(258, 62);
            this.personaCombo.Name = "personaCombo";
            this.personaCombo.Size = new System.Drawing.Size(359, 45);
            this.personaCombo.TabIndex = 1;
            this.personaCombo.SelectedIndexChanged += new System.EventHandler(this.personaCombo_SelectedIndexChanged);
            // 
            // prepolizaList
            // 
            this.prepolizaList.Location = new System.Drawing.Point(50, 147);
            this.prepolizaList.Name = "prepolizaList";
            this.prepolizaList.Size = new System.Drawing.Size(2310, 656);
            this.prepolizaList.TabIndex = 2;
            this.prepolizaList.UseCompatibleStateImageBehavior = false;
            this.prepolizaList.SelectedIndexChanged += new System.EventHandler(this.prepolizaList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(681, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Periodo:";
            this.label2.Visible = false;
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(867, 65);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(329, 45);
            this.periodosCombo.TabIndex = 4;
            this.periodosCombo.Visible = false;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // contabilizarButton
            // 
            this.contabilizarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contabilizarButton.Location = new System.Drawing.Point(75, 934);
            this.contabilizarButton.Name = "contabilizarButton";
            this.contabilizarButton.Size = new System.Drawing.Size(562, 65);
            this.contabilizarButton.TabIndex = 5;
            this.contabilizarButton.Text = "Generar XML para Transfer Desk";
            this.toolTip1.SetToolTip(this.contabilizarButton, "Revisa bien la poliza antes de presionar este botón");
            this.contabilizarButton.UseVisualStyleBackColor = true;
            this.contabilizarButton.Click += new System.EventHandler(this.contabilizarButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2378, 924);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = ".";
            // 
            // nuevaLinea
            // 
            this.nuevaLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nuevaLinea.Location = new System.Drawing.Point(777, 934);
            this.nuevaLinea.Name = "nuevaLinea";
            this.nuevaLinea.Size = new System.Drawing.Size(278, 65);
            this.nuevaLinea.TabIndex = 7;
            this.nuevaLinea.Text = "Nueva Linea";
            this.nuevaLinea.UseVisualStyleBackColor = true;
            this.nuevaLinea.Click += new System.EventHandler(this.nuevaLinea_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1162, 934);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(702, 38);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cambiar periodo y fecha para toda la prepoliza:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(1914, 934);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(446, 44);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 847);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(476, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Concepto de poliza para facturación electrónica:";
            // 
            // conceptoText
            // 
            this.conceptoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conceptoText.Location = new System.Drawing.Point(572, 847);
            this.conceptoText.MaxLength = 300;
            this.conceptoText.Name = "conceptoText";
            this.conceptoText.Size = new System.Drawing.Size(1788, 44);
            this.conceptoText.TabIndex = 11;
            // 
            // polizasDeDepartamentales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2358, 1041);
            this.Controls.Add(this.conceptoText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nuevaLinea);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.contabilizarButton);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prepolizaList);
            this.Controls.Add(this.personaCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "polizasDeDepartamentales";
            this.Text = "Polizas y prepolizas de departamentales";
            this.Load += new System.EventHandler(this.polizasDeDepartamentales_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox personaCombo;
        private System.Windows.Forms.ListView prepolizaList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Button contabilizarButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button nuevaLinea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox conceptoText;
    }
}