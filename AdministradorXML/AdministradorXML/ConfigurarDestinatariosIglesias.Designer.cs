namespace AdministradorXML
{
    partial class ConfigurarDestinatariosIglesias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurarDestinatariosIglesias));
            this.iglesiasList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.nombreText = new System.Windows.Forms.TextBox();
            this.correoText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guardarButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ANL_CODE = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iglesiasList
            // 
            this.iglesiasList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iglesiasList.Location = new System.Drawing.Point(63, 209);
            this.iglesiasList.MultiSelect = false;
            this.iglesiasList.Name = "iglesiasList";
            this.iglesiasList.Size = new System.Drawing.Size(1689, 701);
            this.iglesiasList.TabIndex = 0;
            this.iglesiasList.UseCompatibleStateImageBehavior = false;
            this.iglesiasList.SelectedIndexChanged += new System.EventHandler(this.iglesiasList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // nombreText
            // 
            this.nombreText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreText.Location = new System.Drawing.Point(262, 55);
            this.nombreText.Name = "nombreText";
            this.nombreText.Size = new System.Drawing.Size(498, 44);
            this.nombreText.TabIndex = 2;
            // 
            // correoText
            // 
            this.correoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correoText.Location = new System.Drawing.Point(1009, 61);
            this.correoText.Name = "correoText";
            this.correoText.Size = new System.Drawing.Size(743, 44);
            this.correoText.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(819, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Correo:";
            // 
            // guardarButton
            // 
            this.guardarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarButton.Location = new System.Drawing.Point(1543, 128);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(209, 54);
            this.guardarButton.TabIndex = 5;
            this.guardarButton.Text = "Guardar";
            this.guardarButton.UseVisualStyleBackColor = true;
            this.guardarButton.Click += new System.EventHandler(this.guardarButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1763, 913);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = ".";
            // 
            // ANL_CODE
            // 
            this.ANL_CODE.AutoSize = true;
            this.ANL_CODE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ANL_CODE.Location = new System.Drawing.Point(101, 128);
            this.ANL_CODE.Name = "ANL_CODE";
            this.ANL_CODE.Size = new System.Drawing.Size(0, 38);
            this.ANL_CODE.TabIndex = 7;
            // 
            // ConfigurarDestinatariosIglesias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1805, 948);
            this.Controls.Add(this.ANL_CODE);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.guardarButton);
            this.Controls.Add(this.correoText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nombreText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iglesiasList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurarDestinatariosIglesias";
            this.Text = "Configurar destinatarios de iglesias";
            this.Load += new System.EventHandler(this.ConfigurarDestinatariosIglesias_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView iglesiasList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nombreText;
        private System.Windows.Forms.TextBox correoText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button guardarButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ANL_CODE;
    }
}