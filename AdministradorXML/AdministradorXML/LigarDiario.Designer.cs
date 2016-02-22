namespace AdministradorXML
{
    partial class LigarDiario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LigarDiario));
            this.label1 = new System.Windows.Forms.Label();
            this.diariosText = new System.Windows.Forms.TextBox();
            this.lineasList = new System.Windows.Forms.ListView();
            this.facturasPosiblesList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.rfcText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.razonSocialText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cantidadALigar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cantidadDeLaLineaText = new System.Windows.Forms.TextBox();
            this.ligarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Diario:";
            // 
            // diariosText
            // 
            this.diariosText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diariosText.Location = new System.Drawing.Point(178, 30);
            this.diariosText.Name = "diariosText";
            this.diariosText.Size = new System.Drawing.Size(289, 44);
            this.diariosText.TabIndex = 1;
            this.diariosText.TextChanged += new System.EventHandler(this.diarioText_TextChanged);
            // 
            // lineasList
            // 
            this.lineasList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineasList.Location = new System.Drawing.Point(36, 95);
            this.lineasList.MultiSelect = false;
            this.lineasList.Name = "lineasList";
            this.lineasList.Size = new System.Drawing.Size(1873, 506);
            this.lineasList.TabIndex = 2;
            this.lineasList.UseCompatibleStateImageBehavior = false;
            this.lineasList.SelectedIndexChanged += new System.EventHandler(this.lineasList_SelectedIndexChanged);
            // 
            // facturasPosiblesList
            // 
            this.facturasPosiblesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.facturasPosiblesList.Location = new System.Drawing.Point(36, 698);
            this.facturasPosiblesList.MultiSelect = false;
            this.facturasPosiblesList.Name = "facturasPosiblesList";
            this.facturasPosiblesList.Size = new System.Drawing.Size(1873, 562);
            this.facturasPosiblesList.TabIndex = 3;
            this.facturasPosiblesList.UseCompatibleStateImageBehavior = false;
            this.facturasPosiblesList.SelectedIndexChanged += new System.EventHandler(this.facturasPosiblesList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 632);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "RFC:";
            // 
            // rfcText
            // 
            this.rfcText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfcText.Location = new System.Drawing.Point(154, 629);
            this.rfcText.Name = "rfcText";
            this.rfcText.Size = new System.Drawing.Size(351, 44);
            this.rfcText.TabIndex = 5;
            this.rfcText.TextChanged += new System.EventHandler(this.rfcText_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(549, 632);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 38);
            this.label3.TabIndex = 6;
            this.label3.Text = "Razón social:";
            // 
            // razonSocialText
            // 
            this.razonSocialText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.razonSocialText.Location = new System.Drawing.Point(805, 632);
            this.razonSocialText.Name = "razonSocialText";
            this.razonSocialText.Size = new System.Drawing.Size(1104, 44);
            this.razonSocialText.TabIndex = 7;
            this.razonSocialText.TextChanged += new System.EventHandler(this.razonSocialText_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(518, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 38);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cantidad factura:";
            // 
            // cantidadALigar
            // 
            this.cantidadALigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadALigar.Location = new System.Drawing.Point(805, 30);
            this.cantidadALigar.Name = "cantidadALigar";
            this.cantidadALigar.Size = new System.Drawing.Size(325, 44);
            this.cantidadALigar.TabIndex = 9;
            this.cantidadALigar.TextChanged += new System.EventHandler(this.cantidadALigar_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1916, 1264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1163, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 38);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cantidad linea:";
            // 
            // cantidadDeLaLineaText
            // 
            this.cantidadDeLaLineaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadDeLaLineaText.Location = new System.Drawing.Point(1402, 30);
            this.cantidadDeLaLineaText.Name = "cantidadDeLaLineaText";
            this.cantidadDeLaLineaText.Size = new System.Drawing.Size(306, 44);
            this.cantidadDeLaLineaText.TabIndex = 12;
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(1750, 12);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(172, 63);
            this.ligarButton.TabIndex = 13;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // LigarDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1984, 1245);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.cantidadDeLaLineaText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cantidadALigar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.razonSocialText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rfcText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.facturasPosiblesList);
            this.Controls.Add(this.lineasList);
            this.Controls.Add(this.diariosText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LigarDiario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ligar diario";
            this.Load += new System.EventHandler(this.LigarDiario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox diariosText;
        private System.Windows.Forms.ListView lineasList;
        private System.Windows.Forms.ListView facturasPosiblesList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rfcText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox razonSocialText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cantidadALigar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cantidadDeLaLineaText;
        private System.Windows.Forms.Button ligarButton;
    }
}