namespace TerminarDeLigar
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
            this.label3 = new System.Windows.Forms.Label();
            this.conceptoText = new System.Windows.Forms.TextBox();
            this.mensajeLabel = new System.Windows.Forms.Label();
            this.ligarAhoraSiButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 38);
            this.label3.TabIndex = 10;
            this.label3.Text = "Concepto:";
            // 
            // conceptoText
            // 
            this.conceptoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conceptoText.Location = new System.Drawing.Point(228, 173);
            this.conceptoText.MaxLength = 300;
            this.conceptoText.Name = "conceptoText";
            this.conceptoText.Size = new System.Drawing.Size(1297, 44);
            this.conceptoText.TabIndex = 11;
            // 
            // mensajeLabel
            // 
            this.mensajeLabel.AutoSize = true;
            this.mensajeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensajeLabel.Location = new System.Drawing.Point(61, 40);
            this.mensajeLabel.Name = "mensajeLabel";
            this.mensajeLabel.Size = new System.Drawing.Size(122, 44);
            this.mensajeLabel.TabIndex = 12;
            this.mensajeLabel.Text = "label1";
            // 
            // ligarAhoraSiButton
            // 
            this.ligarAhoraSiButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarAhoraSiButton.Location = new System.Drawing.Point(1132, 246);
            this.ligarAhoraSiButton.Name = "ligarAhoraSiButton";
            this.ligarAhoraSiButton.Size = new System.Drawing.Size(393, 70);
            this.ligarAhoraSiButton.TabIndex = 13;
            this.ligarAhoraSiButton.Text = "Terminar de ligar";
            this.ligarAhoraSiButton.UseVisualStyleBackColor = true;
            this.ligarAhoraSiButton.Click += new System.EventHandler(this.ligarAhoraSiButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1576, 367);
            this.Controls.Add(this.ligarAhoraSiButton);
            this.Controls.Add(this.mensajeLabel);
            this.Controls.Add(this.conceptoText);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Terminar de Ligar";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox conceptoText;
        private System.Windows.Forms.Label mensajeLabel;
        private System.Windows.Forms.Button ligarAhoraSiButton;
    }
}

