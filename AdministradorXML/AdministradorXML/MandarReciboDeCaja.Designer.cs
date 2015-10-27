namespace AdministradorXML
{
    partial class MandarReciboDeCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MandarReciboDeCaja));
            this.label1 = new System.Windows.Forms.Label();
            this.iglesiaText = new System.Windows.Forms.TextBox();
            this.nombreLabel = new System.Windows.Forms.Label();
            this.correoLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.diarioText = new System.Windows.Forms.TextBox();
            this.mandarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iglesia:";
            // 
            // iglesiaText
            // 
            this.iglesiaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iglesiaText.Location = new System.Drawing.Point(214, 84);
            this.iglesiaText.Name = "iglesiaText";
            this.iglesiaText.Size = new System.Drawing.Size(359, 44);
            this.iglesiaText.TabIndex = 1;
            this.iglesiaText.TextChanged += new System.EventHandler(this.iglesiaText_TextChanged);
            // 
            // nombreLabel
            // 
            this.nombreLabel.AutoSize = true;
            this.nombreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreLabel.Location = new System.Drawing.Point(51, 166);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(0, 38);
            this.nombreLabel.TabIndex = 2;
            // 
            // correoLabel
            // 
            this.correoLabel.AutoSize = true;
            this.correoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correoLabel.Location = new System.Drawing.Point(51, 248);
            this.correoLabel.Name = "correoLabel";
            this.correoLabel.Size = new System.Drawing.Size(0, 38);
            this.correoLabel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "Diario:";
            // 
            // diarioText
            // 
            this.diarioText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diarioText.Location = new System.Drawing.Point(214, 316);
            this.diarioText.Name = "diarioText";
            this.diarioText.Size = new System.Drawing.Size(359, 44);
            this.diarioText.TabIndex = 5;
            // 
            // mandarButton
            // 
            this.mandarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mandarButton.Location = new System.Drawing.Point(178, 408);
            this.mandarButton.Name = "mandarButton";
            this.mandarButton.Size = new System.Drawing.Size(226, 65);
            this.mandarButton.TabIndex = 6;
            this.mandarButton.Text = "Mandar";
            this.mandarButton.UseVisualStyleBackColor = true;
            this.mandarButton.Click += new System.EventHandler(this.mandarButton_Click);
            // 
            // MandarReciboDeCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(672, 576);
            this.Controls.Add(this.mandarButton);
            this.Controls.Add(this.diarioText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.correoLabel);
            this.Controls.Add(this.nombreLabel);
            this.Controls.Add(this.iglesiaText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MandarReciboDeCaja";
            this.Text = "Mandar recibo de caja";
            this.Load += new System.EventHandler(this.MandarReciboDeCaja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iglesiaText;
        private System.Windows.Forms.Label nombreLabel;
        private System.Windows.Forms.Label correoLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox diarioText;
        private System.Windows.Forms.Button mandarButton;
    }
}