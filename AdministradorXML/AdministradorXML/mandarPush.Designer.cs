namespace AdministradorXML
{
    partial class mandarPush
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mandarPush));
            this.label1 = new System.Windows.Forms.Label();
            this.personasCombo = new System.Windows.Forms.ComboBox();
            this.mensajeText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona:";
            // 
            // personasCombo
            // 
            this.personasCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personasCombo.FormattingEnabled = true;
            this.personasCombo.Location = new System.Drawing.Point(265, 58);
            this.personasCombo.Name = "personasCombo";
            this.personasCombo.Size = new System.Drawing.Size(339, 45);
            this.personasCombo.TabIndex = 1;
            // 
            // mensajeText
            // 
            this.mensajeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensajeText.Location = new System.Drawing.Point(60, 243);
            this.mensajeText.Name = "mensajeText";
            this.mensajeText.Size = new System.Drawing.Size(961, 44);
            this.mensajeText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Escribe tu mensaje:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1070, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 62);
            this.button1.TabIndex = 4;
            this.button1.Text = "Mandar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mandarPush
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1335, 344);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mensajeText);
            this.Controls.Add(this.personasCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mandarPush";
            this.Text = "Mandar push";
            this.Load += new System.EventHandler(this.mandarPush_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox personasCombo;
        private System.Windows.Forms.TextBox mensajeText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}