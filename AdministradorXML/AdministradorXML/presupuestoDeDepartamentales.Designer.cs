namespace AdministradorXML
{
    partial class presupuestoDeDepartamentales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(presupuestoDeDepartamentales));
            this.label1 = new System.Windows.Forms.Label();
            this.personaCombo = new System.Windows.Forms.ComboBox();
            this.anioRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mesRadio = new System.Windows.Forms.RadioButton();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.presupuestoList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona:";
            // 
            // personaCombo
            // 
            this.personaCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personaCombo.FormattingEnabled = true;
            this.personaCombo.Location = new System.Drawing.Point(204, 48);
            this.personaCombo.Name = "personaCombo";
            this.personaCombo.Size = new System.Drawing.Size(324, 45);
            this.personaCombo.TabIndex = 1;
            this.personaCombo.SelectedIndexChanged += new System.EventHandler(this.personaCombo_SelectedIndexChanged);
            // 
            // anioRadio
            // 
            this.anioRadio.AutoSize = true;
            this.anioRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anioRadio.Location = new System.Drawing.Point(42, 45);
            this.anioRadio.Name = "anioRadio";
            this.anioRadio.Size = new System.Drawing.Size(100, 42);
            this.anioRadio.TabIndex = 2;
            this.anioRadio.TabStop = true;
            this.anioRadio.Text = "Año";
            this.anioRadio.UseVisualStyleBackColor = true;
            this.anioRadio.CheckedChanged += new System.EventHandler(this.anioRadio_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mesRadio);
            this.groupBox1.Controls.Add(this.anioRadio);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(551, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ver presupuesto por:";
            // 
            // mesRadio
            // 
            this.mesRadio.AutoSize = true;
            this.mesRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesRadio.Location = new System.Drawing.Point(382, 45);
            this.mesRadio.Name = "mesRadio";
            this.mesRadio.Size = new System.Drawing.Size(104, 42);
            this.mesRadio.TabIndex = 3;
            this.mesRadio.TabStop = true;
            this.mesRadio.Text = "Mes";
            this.mesRadio.UseVisualStyleBackColor = true;
            this.mesRadio.CheckedChanged += new System.EventHandler(this.mesRadio_CheckedChanged);
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(1293, 94);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(282, 45);
            this.periodosCombo.TabIndex = 4;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1293, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 38);
            this.label2.TabIndex = 5;
            this.label2.Text = "Periodo:";
            // 
            // presupuestoList
            // 
            this.presupuestoList.Location = new System.Drawing.Point(57, 205);
            this.presupuestoList.Name = "presupuestoList";
            this.presupuestoList.Size = new System.Drawing.Size(1518, 681);
            this.presupuestoList.TabIndex = 6;
            this.presupuestoList.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1592, 894);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = ".";
            // 
            // presupuestoDeDepartamentales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1661, 932);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.presupuestoList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.personaCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "presupuestoDeDepartamentales";
            this.Text = "Presupuesto de departamentales";
            this.Load += new System.EventHandler(this.presupuestoDeDepartamentales_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox personaCombo;
        private System.Windows.Forms.RadioButton anioRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mesRadio;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView presupuestoList;
        private System.Windows.Forms.Label label3;
    }
}