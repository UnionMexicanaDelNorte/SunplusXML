namespace Liga
{
    partial class Seguro
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
            this.mensajeLabel = new System.Windows.Forms.Label();
            this.siButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mensajeLabel
            // 
            this.mensajeLabel.AutoSize = true;
            this.mensajeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensajeLabel.Location = new System.Drawing.Point(43, 52);
            this.mensajeLabel.Name = "mensajeLabel";
            this.mensajeLabel.Size = new System.Drawing.Size(103, 38);
            this.mensajeLabel.TabIndex = 0;
            this.mensajeLabel.Text = "label1";
            this.mensajeLabel.Click += new System.EventHandler(this.mensajeLabel_Click);
            // 
            // siButton
            // 
            this.siButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siButton.Location = new System.Drawing.Point(195, 298);
            this.siButton.Name = "siButton";
            this.siButton.Size = new System.Drawing.Size(105, 57);
            this.siButton.TabIndex = 1;
            this.siButton.Text = "Si";
            this.siButton.UseVisualStyleBackColor = true;
            // 
            // noButton
            // 
            this.noButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noButton.Location = new System.Drawing.Point(832, 281);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(314, 84);
            this.noButton.TabIndex = 2;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // Seguro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1285, 428);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.siButton);
            this.Controls.Add(this.mensajeLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Seguro";
            this.ShowIcon = false;
            this.Text = "¿Seguro?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mensajeLabel;
        private System.Windows.Forms.Button siButton;
        private System.Windows.Forms.Button noButton;
    }
}