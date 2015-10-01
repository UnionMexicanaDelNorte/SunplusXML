namespace AdministradorXML
{
    partial class eligeBanco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(eligeBanco));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bancoCombo = new System.Windows.Forms.ComboBox();
            this.cuentaBancariaText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Banco:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(265, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cuenta bancaria:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(372, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 62);
            this.button1.TabIndex = 2;
            this.button1.Text = "Añadir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bancoCombo
            // 
            this.bancoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.93194F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bancoCombo.FormattingEnabled = true;
            this.bancoCombo.Location = new System.Drawing.Point(352, 50);
            this.bancoCombo.Name = "bancoCombo";
            this.bancoCombo.Size = new System.Drawing.Size(431, 41);
            this.bancoCombo.TabIndex = 3;
            // 
            // cuentaBancariaText
            // 
            this.cuentaBancariaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuentaBancariaText.Location = new System.Drawing.Point(352, 141);
            this.cuentaBancariaText.MaxLength = 50;
            this.cuentaBancariaText.Name = "cuentaBancariaText";
            this.cuentaBancariaText.Size = new System.Drawing.Size(437, 44);
            this.cuentaBancariaText.TabIndex = 4;
            // 
            // eligeBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(987, 338);
            this.Controls.Add(this.cuentaBancariaText);
            this.Controls.Add(this.bancoCombo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "eligeBanco";
            this.Text = "Elige banco y cuenta bancaria:";
            this.Load += new System.EventHandler(this.eligeBanco_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox bancoCombo;
        private System.Windows.Forms.TextBox cuentaBancariaText;
    }
}