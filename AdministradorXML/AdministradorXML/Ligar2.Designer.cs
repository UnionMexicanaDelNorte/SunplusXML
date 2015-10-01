namespace AdministradorXML
{
    partial class Ligar2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ligar2));
            this.infoLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rfcText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.razonSocialText = new System.Windows.Forms.TextBox();
            this.facturasPosiblesList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.cantidadALigar = new System.Windows.Forms.TextBox();
            this.ligarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(42, 49);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(103, 38);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "RFC:";
            // 
            // rfcText
            // 
            this.rfcText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfcText.Location = new System.Drawing.Point(232, 123);
            this.rfcText.Name = "rfcText";
            this.rfcText.Size = new System.Drawing.Size(345, 44);
            this.rfcText.TabIndex = 2;
            this.rfcText.TextChanged += new System.EventHandler(this.rfcText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Razon social:";
            // 
            // razonSocialText
            // 
            this.razonSocialText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.razonSocialText.Location = new System.Drawing.Point(308, 229);
            this.razonSocialText.Name = "razonSocialText";
            this.razonSocialText.Size = new System.Drawing.Size(745, 44);
            this.razonSocialText.TabIndex = 4;
            this.razonSocialText.TextChanged += new System.EventHandler(this.razonSocialText_TextChanged);
            // 
            // facturasPosiblesList
            // 
            this.facturasPosiblesList.Location = new System.Drawing.Point(49, 370);
            this.facturasPosiblesList.Name = "facturasPosiblesList";
            this.facturasPosiblesList.Size = new System.Drawing.Size(1250, 518);
            this.facturasPosiblesList.TabIndex = 5;
            this.facturasPosiblesList.UseCompatibleStateImageBehavior = false;
            this.facturasPosiblesList.SelectedIndexChanged += new System.EventHandler(this.facturasPosiblesList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(459, 38);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cantidad a ligar de la factura $";
            // 
            // cantidadALigar
            // 
            this.cantidadALigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadALigar.Location = new System.Drawing.Point(514, 298);
            this.cantidadALigar.Name = "cantidadALigar";
            this.cantidadALigar.Size = new System.Drawing.Size(245, 44);
            this.cantidadALigar.TabIndex = 7;
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(991, 84);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(283, 92);
            this.ligarButton.TabIndex = 8;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // Ligar2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 925);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.cantidadALigar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.facturasPosiblesList);
            this.Controls.Add(this.razonSocialText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rfcText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ligar2";
            this.Text = "Ligar factura al movimiento contable";
            this.Load += new System.EventHandler(this.Ligar2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rfcText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox razonSocialText;
        private System.Windows.Forms.ListView facturasPosiblesList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cantidadALigar;
        private System.Windows.Forms.Button ligarButton;
    }
}