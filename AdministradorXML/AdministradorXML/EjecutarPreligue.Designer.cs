namespace AdministradorXML
{
    partial class EjecutarPreligue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EjecutarPreligue));
            this.label1 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modoFlojo = new System.Windows.Forms.RadioButton();
            this.modoAlfa = new System.Windows.Forms.RadioButton();
            this.Ligar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo donde será ejecutado el preligue:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(718, 51);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(363, 50);
            this.periodosCombo.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modoAlfa);
            this.groupBox1.Controls.Add(this.modoFlojo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(46, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 285);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modo de preligue";
            // 
            // modoFlojo
            // 
            this.modoFlojo.AutoSize = true;
            this.modoFlojo.Location = new System.Drawing.Point(37, 76);
            this.modoFlojo.Name = "modoFlojo";
            this.modoFlojo.Size = new System.Drawing.Size(343, 42);
            this.modoFlojo.TabIndex = 0;
            this.modoFlojo.Text = "Modo: Contador flojo";
            this.modoFlojo.UseVisualStyleBackColor = true;
            // 
            // modoAlfa
            // 
            this.modoAlfa.AutoSize = true;
            this.modoAlfa.Checked = true;
            this.modoAlfa.Location = new System.Drawing.Point(37, 169);
            this.modoAlfa.Name = "modoAlfa";
            this.modoAlfa.Size = new System.Drawing.Size(475, 42);
            this.modoAlfa.TabIndex = 1;
            this.modoAlfa.TabStop = true;
            this.modoAlfa.Text = "Modo: Contador Responsable";
            this.modoAlfa.UseVisualStyleBackColor = true;
            // 
            // Ligar
            // 
            this.Ligar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ligar.Location = new System.Drawing.Point(423, 485);
            this.Ligar.Name = "Ligar";
            this.Ligar.Size = new System.Drawing.Size(248, 66);
            this.Ligar.TabIndex = 3;
            this.Ligar.Text = "Ligar";
            this.Ligar.UseVisualStyleBackColor = true;
            this.Ligar.Click += new System.EventHandler(this.Ligar_Click);
            // 
            // EjecutarPreligue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1121, 610);
            this.Controls.Add(this.Ligar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EjecutarPreligue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ejecutar preligue";
            this.Load += new System.EventHandler(this.EjecutarPreligue_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton modoAlfa;
        private System.Windows.Forms.RadioButton modoFlojo;
        private System.Windows.Forms.Button Ligar;
    }
}