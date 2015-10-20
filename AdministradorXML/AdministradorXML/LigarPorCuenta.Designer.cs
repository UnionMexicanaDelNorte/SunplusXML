namespace AdministradorXML
{
    partial class LigarPorCuenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LigarPorCuenta));
            this.label1 = new System.Windows.Forms.Label();
            this.cantidadALigar = new System.Windows.Forms.TextBox();
            this.modificarCantidadCheck = new System.Windows.Forms.CheckBox();
            this.maximoFacturaLabel = new System.Windows.Forms.Label();
            this.maxFactura = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cuentaText = new System.Windows.Forms.TextBox();
            this.diariosList = new System.Windows.Forms.ListView();
            this.ligarButton = new System.Windows.Forms.Button();
            this.lineasList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maximoLineaLabel = new System.Windows.Forms.Label();
            this.cantidadDeLaLineaText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad a ligar de la factura:";
            // 
            // cantidadALigar
            // 
            this.cantidadALigar.Enabled = false;
            this.cantidadALigar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadALigar.Location = new System.Drawing.Point(520, 52);
            this.cantidadALigar.Name = "cantidadALigar";
            this.cantidadALigar.Size = new System.Drawing.Size(290, 44);
            this.cantidadALigar.TabIndex = 1;
            this.cantidadALigar.TextChanged += new System.EventHandler(this.cantidadALigar_TextChanged);
            // 
            // modificarCantidadCheck
            // 
            this.modificarCantidadCheck.AutoSize = true;
            this.modificarCantidadCheck.Location = new System.Drawing.Point(50, 108);
            this.modificarCantidadCheck.Name = "modificarCantidadCheck";
            this.modificarCantidadCheck.Size = new System.Drawing.Size(339, 30);
            this.modificarCantidadCheck.TabIndex = 2;
            this.modificarCantidadCheck.Text = "Modificar cantidad de la factura";
            this.modificarCantidadCheck.UseVisualStyleBackColor = true;
            this.modificarCantidadCheck.CheckedChanged += new System.EventHandler(this.modificarCantidadCheck_CheckedChanged);
            // 
            // maximoFacturaLabel
            // 
            this.maximoFacturaLabel.AutoSize = true;
            this.maximoFacturaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximoFacturaLabel.Location = new System.Drawing.Point(891, 52);
            this.maximoFacturaLabel.Name = "maximoFacturaLabel";
            this.maximoFacturaLabel.Size = new System.Drawing.Size(103, 38);
            this.maximoFacturaLabel.TabIndex = 3;
            this.maximoFacturaLabel.Text = "label2";
            // 
            // maxFactura
            // 
            this.maxFactura.AutoSize = true;
            this.maxFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxFactura.Location = new System.Drawing.Point(470, 108);
            this.maxFactura.Name = "maxFactura";
            this.maxFactura.Size = new System.Drawing.Size(103, 38);
            this.maxFactura.TabIndex = 4;
            this.maxFactura.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 38);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cuenta sunplus:";
            // 
            // cuentaText
            // 
            this.cuentaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuentaText.Location = new System.Drawing.Point(343, 186);
            this.cuentaText.Name = "cuentaText";
            this.cuentaText.Size = new System.Drawing.Size(467, 44);
            this.cuentaText.TabIndex = 6;
            this.cuentaText.TextChanged += new System.EventHandler(this.cuentaText_TextChanged);
            // 
            // diariosList
            // 
            this.diariosList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diariosList.Location = new System.Drawing.Point(50, 272);
            this.diariosList.Name = "diariosList";
            this.diariosList.Size = new System.Drawing.Size(1300, 307);
            this.diariosList.TabIndex = 7;
            this.diariosList.UseCompatibleStateImageBehavior = false;
            this.diariosList.SelectedIndexChanged += new System.EventHandler(this.diariosList_SelectedIndexChanged);
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(1157, 120);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(193, 71);
            this.ligarButton.TabIndex = 8;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // lineasList
            // 
            this.lineasList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineasList.Location = new System.Drawing.Point(50, 658);
            this.lineasList.Name = "lineasList";
            this.lineasList.Size = new System.Drawing.Size(1306, 349);
            this.lineasList.TabIndex = 9;
            this.lineasList.UseCompatibleStateImageBehavior = false;
            this.lineasList.SelectedIndexChanged += new System.EventHandler(this.lineasList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 600);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 38);
            this.label3.TabIndex = 10;
            this.label3.Text = "Monto a ligar:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1363, 1002);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = ".";
            // 
            // maximoLineaLabel
            // 
            this.maximoLineaLabel.AutoSize = true;
            this.maximoLineaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximoLineaLabel.Location = new System.Drawing.Point(804, 600);
            this.maximoLineaLabel.Name = "maximoLineaLabel";
            this.maximoLineaLabel.Size = new System.Drawing.Size(103, 38);
            this.maximoLineaLabel.TabIndex = 12;
            this.maximoLineaLabel.Text = "label5";
            // 
            // cantidadDeLaLineaText
            // 
            this.cantidadDeLaLineaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadDeLaLineaText.Location = new System.Drawing.Point(289, 594);
            this.cantidadDeLaLineaText.Name = "cantidadDeLaLineaText";
            this.cantidadDeLaLineaText.Size = new System.Drawing.Size(345, 44);
            this.cantidadDeLaLineaText.TabIndex = 13;
            // 
            // LigarPorCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1401, 1040);
            this.Controls.Add(this.cantidadDeLaLineaText);
            this.Controls.Add(this.maximoLineaLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lineasList);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.diariosList);
            this.Controls.Add(this.cuentaText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxFactura);
            this.Controls.Add(this.maximoFacturaLabel);
            this.Controls.Add(this.modificarCantidadCheck);
            this.Controls.Add(this.cantidadALigar);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LigarPorCuenta";
            this.Text = "Ligar por cuenta";
            this.Load += new System.EventHandler(this.LigarPorCuenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cantidadALigar;
        private System.Windows.Forms.CheckBox modificarCantidadCheck;
        private System.Windows.Forms.Label maximoFacturaLabel;
        private System.Windows.Forms.Label maxFactura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cuentaText;
        private System.Windows.Forms.ListView diariosList;
        private System.Windows.Forms.Button ligarButton;
        private System.Windows.Forms.ListView lineasList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label maximoLineaLabel;
        private System.Windows.Forms.TextBox cantidadDeLaLineaText;
    }
}