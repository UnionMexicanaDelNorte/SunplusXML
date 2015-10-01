namespace AdministradorXML
{
    partial class Ligar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ligar));
            this.label1 = new System.Windows.Forms.Label();
            this.cantidadALigar = new System.Windows.Forms.TextBox();
            this.modificarCantidadCheck = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.diariosText = new System.Windows.Forms.TextBox();
            this.informacionLabel = new System.Windows.Forms.Label();
            this.lineasList = new System.Windows.Forms.ListView();
            this.ligarButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cantidadDeLaLineaText = new System.Windows.Forms.TextBox();
            this.maximoFacturaLabel = new System.Windows.Forms.Label();
            this.maximoLineaLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad a ligar de la factura:";
            // 
            // cantidadALigar
            // 
            this.cantidadALigar.Enabled = false;
            this.cantidadALigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadALigar.Location = new System.Drawing.Point(542, 50);
            this.cantidadALigar.Name = "cantidadALigar";
            this.cantidadALigar.Size = new System.Drawing.Size(297, 44);
            this.cantidadALigar.TabIndex = 1;
            this.cantidadALigar.TextChanged += new System.EventHandler(this.cantidadALigar_TextChanged);
            // 
            // modificarCantidadCheck
            // 
            this.modificarCantidadCheck.AutoSize = true;
            this.modificarCantidadCheck.Location = new System.Drawing.Point(62, 117);
            this.modificarCantidadCheck.Name = "modificarCantidadCheck";
            this.modificarCantidadCheck.Size = new System.Drawing.Size(339, 30);
            this.modificarCantidadCheck.TabIndex = 2;
            this.modificarCantidadCheck.Text = "Modificar cantidad de la factura";
            this.modificarCantidadCheck.UseVisualStyleBackColor = true;
            this.modificarCantidadCheck.CheckedChanged += new System.EventHandler(this.modificarCantidadCheck_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "Diario:";
            // 
            // diariosText
            // 
            this.diariosText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diariosText.Location = new System.Drawing.Point(251, 197);
            this.diariosText.Name = "diariosText";
            this.diariosText.Size = new System.Drawing.Size(280, 44);
            this.diariosText.TabIndex = 4;
            this.diariosText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // informacionLabel
            // 
            this.informacionLabel.AutoSize = true;
            this.informacionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informacionLabel.Location = new System.Drawing.Point(55, 426);
            this.informacionLabel.Name = "informacionLabel";
            this.informacionLabel.Size = new System.Drawing.Size(0, 38);
            this.informacionLabel.TabIndex = 5;
            // 
            // lineasList
            // 
            this.lineasList.Location = new System.Drawing.Point(64, 511);
            this.lineasList.Name = "lineasList";
            this.lineasList.Size = new System.Drawing.Size(1559, 585);
            this.lineasList.TabIndex = 6;
            this.lineasList.UseCompatibleStateImageBehavior = false;
            this.lineasList.SelectedIndexChanged += new System.EventHandler(this.lineasList_SelectedIndexChanged);
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(1341, 155);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(222, 83);
            this.ligarButton.TabIndex = 7;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(409, 38);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cantidad a ligar de la linea:";
            // 
            // cantidadDeLaLineaText
            // 
            this.cantidadDeLaLineaText.Enabled = false;
            this.cantidadDeLaLineaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadDeLaLineaText.Location = new System.Drawing.Point(542, 317);
            this.cantidadDeLaLineaText.Name = "cantidadDeLaLineaText";
            this.cantidadDeLaLineaText.Size = new System.Drawing.Size(297, 44);
            this.cantidadDeLaLineaText.TabIndex = 9;
            this.cantidadDeLaLineaText.TextChanged += new System.EventHandler(this.cantidadDeLaLineaText_TextChanged);
            // 
            // maximoFacturaLabel
            // 
            this.maximoFacturaLabel.AutoSize = true;
            this.maximoFacturaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximoFacturaLabel.Location = new System.Drawing.Point(875, 44);
            this.maximoFacturaLabel.Name = "maximoFacturaLabel";
            this.maximoFacturaLabel.Size = new System.Drawing.Size(122, 44);
            this.maximoFacturaLabel.TabIndex = 11;
            this.maximoFacturaLabel.Text = "label4";
            // 
            // maximoLineaLabel
            // 
            this.maximoLineaLabel.AutoSize = true;
            this.maximoLineaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximoLineaLabel.Location = new System.Drawing.Point(883, 317);
            this.maximoLineaLabel.Name = "maximoLineaLabel";
            this.maximoLineaLabel.Size = new System.Drawing.Size(0, 44);
            this.maximoLineaLabel.TabIndex = 12;
            // 
            // Ligar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 1128);
            this.Controls.Add(this.maximoLineaLabel);
            this.Controls.Add(this.maximoFacturaLabel);
            this.Controls.Add(this.cantidadDeLaLineaText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.lineasList);
            this.Controls.Add(this.informacionLabel);
            this.Controls.Add(this.diariosText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modificarCantidadCheck);
            this.Controls.Add(this.cantidadALigar);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ligar";
            this.Text = "Ligar";
            this.Load += new System.EventHandler(this.Ligar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cantidadALigar;
        private System.Windows.Forms.CheckBox modificarCantidadCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox diariosText;
        private System.Windows.Forms.Label informacionLabel;
        private System.Windows.Forms.ListView lineasList;
        private System.Windows.Forms.Button ligarButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cantidadDeLaLineaText;
        private System.Windows.Forms.Label maximoFacturaLabel;
        private System.Windows.Forms.Label maximoLineaLabel;
    }
}