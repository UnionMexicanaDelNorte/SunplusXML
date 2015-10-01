namespace AdministradorXML
{
    partial class Asociar_TipoDiario_CuentaSunplus_CuentaBancaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asociar_TipoDiario_CuentaSunplus_CuentaBancaria));
            this.label1 = new System.Windows.Forms.Label();
            this.tipoContabilidad = new System.Windows.Forms.ComboBox();
            this.tipoDeDiarioList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.cuentasSunplusList = new System.Windows.Forms.ListView();
            this.cuentasBancariasList = new System.Windows.Forms.ListView();
            this.anadirButtom = new System.Windows.Forms.Button();
            this.asociacionesList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo:";
            // 
            // tipoContabilidad
            // 
            this.tipoContabilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoContabilidad.FormattingEnabled = true;
            this.tipoContabilidad.Location = new System.Drawing.Point(189, 61);
            this.tipoContabilidad.Name = "tipoContabilidad";
            this.tipoContabilidad.Size = new System.Drawing.Size(273, 45);
            this.tipoContabilidad.TabIndex = 1;
            this.tipoContabilidad.SelectedIndexChanged += new System.EventHandler(this.tipoContabilidad_SelectedIndexChanged);
            // 
            // tipoDeDiarioList
            // 
            this.tipoDeDiarioList.Location = new System.Drawing.Point(40, 235);
            this.tipoDeDiarioList.MultiSelect = false;
            this.tipoDeDiarioList.Name = "tipoDeDiarioList";
            this.tipoDeDiarioList.Size = new System.Drawing.Size(529, 316);
            this.tipoDeDiarioList.TabIndex = 2;
            this.tipoDeDiarioList.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1046, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecciona un tipo de diario, una cuenta de sunplus y una cuenta bancaria, luego " +
    "da click al boton \"Añadir\".";
            // 
            // cuentasSunplusList
            // 
            this.cuentasSunplusList.Location = new System.Drawing.Point(621, 235);
            this.cuentasSunplusList.MultiSelect = false;
            this.cuentasSunplusList.Name = "cuentasSunplusList";
            this.cuentasSunplusList.Size = new System.Drawing.Size(523, 316);
            this.cuentasSunplusList.TabIndex = 4;
            this.cuentasSunplusList.UseCompatibleStateImageBehavior = false;
            // 
            // cuentasBancariasList
            // 
            this.cuentasBancariasList.Location = new System.Drawing.Point(1209, 234);
            this.cuentasBancariasList.MultiSelect = false;
            this.cuentasBancariasList.Name = "cuentasBancariasList";
            this.cuentasBancariasList.Size = new System.Drawing.Size(391, 317);
            this.cuentasBancariasList.TabIndex = 5;
            this.cuentasBancariasList.UseCompatibleStateImageBehavior = false;
            // 
            // anadirButtom
            // 
            this.anadirButtom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anadirButtom.Location = new System.Drawing.Point(1408, 61);
            this.anadirButtom.Name = "anadirButtom";
            this.anadirButtom.Size = new System.Drawing.Size(192, 53);
            this.anadirButtom.TabIndex = 6;
            this.anadirButtom.Text = "Añadir";
            this.anadirButtom.UseVisualStyleBackColor = true;
            this.anadirButtom.Click += new System.EventHandler(this.anadirButtom_Click);
            // 
            // asociacionesList
            // 
            this.asociacionesList.Location = new System.Drawing.Point(40, 677);
            this.asociacionesList.MultiSelect = false;
            this.asociacionesList.Name = "asociacionesList";
            this.asociacionesList.Size = new System.Drawing.Size(1560, 241);
            this.asociacionesList.TabIndex = 7;
            this.asociacionesList.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(320, 594);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1034, 38);
            this.label3.TabIndex = 8;
            this.label3.Text = "Lista de asociaciones tipo de diario - cuenta sunplus - cuenta bancaria";
            // 
            // Asociar_TipoDiario_CuentaSunplus_CuentaBancaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1684, 975);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.asociacionesList);
            this.Controls.Add(this.anadirButtom);
            this.Controls.Add(this.cuentasBancariasList);
            this.Controls.Add(this.cuentasSunplusList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tipoDeDiarioList);
            this.Controls.Add(this.tipoContabilidad);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Asociar_TipoDiario_CuentaSunplus_CuentaBancaria";
            this.Text = "Asociar tipo de diario con cuenta de sunplus y con cuenta bancaria";
            this.Load += new System.EventHandler(this.Asociar_TipoDiario_CuentaSunplus_CuentaBancaria_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tipoContabilidad;
        private System.Windows.Forms.ListView tipoDeDiarioList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView cuentasSunplusList;
        private System.Windows.Forms.ListView cuentasBancariasList;
        private System.Windows.Forms.Button anadirButtom;
        private System.Windows.Forms.ListView asociacionesList;
        private System.Windows.Forms.Label label3;
    }
}