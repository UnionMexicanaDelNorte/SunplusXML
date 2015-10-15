namespace AdministradorXML
{
    partial class confirmaNumeroDeDiario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(confirmaNumeroDeDiario));
            this.label1 = new System.Windows.Forms.Label();
            this.diarioText = new System.Windows.Forms.TextBox();
            this.confirmarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(811, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Confirmar número de diario que le dio el Transfer Desk";
            // 
            // diarioText
            // 
            this.diarioText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diarioText.Location = new System.Drawing.Point(85, 129);
            this.diarioText.Name = "diarioText";
            this.diarioText.Size = new System.Drawing.Size(303, 44);
            this.diarioText.TabIndex = 1;
            // 
            // confirmarButton
            // 
            this.confirmarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmarButton.Location = new System.Drawing.Point(575, 129);
            this.confirmarButton.Name = "confirmarButton";
            this.confirmarButton.Size = new System.Drawing.Size(239, 63);
            this.confirmarButton.TabIndex = 2;
            this.confirmarButton.Text = "Confirmar";
            this.confirmarButton.UseVisualStyleBackColor = true;
            this.confirmarButton.Click += new System.EventHandler(this.confirmarButton_Click);
            // 
            // confirmaNumeroDeDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(984, 281);
            this.ControlBox = false;
            this.Controls.Add(this.confirmarButton);
            this.Controls.Add(this.diarioText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "confirmaNumeroDeDiario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmar";
            this.Load += new System.EventHandler(this.confirmaNumeroDeDiario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox diarioText;
        private System.Windows.Forms.Button confirmarButton;
    }
}