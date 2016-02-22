namespace AdministradorXML
{
    partial class LigarRFCACuenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LigarRFCACuenta));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listaPreferencias = new System.Windows.Forms.ListView();
            this.rfcText = new System.Windows.Forms.TextBox();
            this.razonSocialText = new System.Windows.Forms.TextBox();
            this.cuentaSunPlusText = new System.Windows.Forms.TextBox();
            this.asociar = new System.Windows.Forms.Button();
            this.desasociar = new System.Windows.Forms.Button();
            this.subir = new System.Windows.Forms.Button();
            this.ver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "RFC:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Razón social:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cuenta sunplus:";
            // 
            // listaPreferencias
            // 
            this.listaPreferencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaPreferencias.Location = new System.Drawing.Point(52, 486);
            this.listaPreferencias.MultiSelect = false;
            this.listaPreferencias.Name = "listaPreferencias";
            this.listaPreferencias.Size = new System.Drawing.Size(1005, 355);
            this.listaPreferencias.TabIndex = 3;
            this.listaPreferencias.UseCompatibleStateImageBehavior = false;
            // 
            // rfcText
            // 
            this.rfcText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfcText.Location = new System.Drawing.Point(52, 100);
            this.rfcText.Name = "rfcText";
            this.rfcText.Size = new System.Drawing.Size(721, 49);
            this.rfcText.TabIndex = 4;
            this.rfcText.TextChanged += new System.EventHandler(this.rfcText_TextChanged);
            // 
            // razonSocialText
            // 
            this.razonSocialText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.razonSocialText.Location = new System.Drawing.Point(52, 239);
            this.razonSocialText.Name = "razonSocialText";
            this.razonSocialText.Size = new System.Drawing.Size(721, 49);
            this.razonSocialText.TabIndex = 5;
            this.razonSocialText.TextChanged += new System.EventHandler(this.razonSocialText_TextChanged);
            // 
            // cuentaSunPlusText
            // 
            this.cuentaSunPlusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuentaSunPlusText.Location = new System.Drawing.Point(52, 373);
            this.cuentaSunPlusText.Name = "cuentaSunPlusText";
            this.cuentaSunPlusText.Size = new System.Drawing.Size(721, 49);
            this.cuentaSunPlusText.TabIndex = 6;
            // 
            // asociar
            // 
            this.asociar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asociar.Location = new System.Drawing.Point(1138, 486);
            this.asociar.Name = "asociar";
            this.asociar.Size = new System.Drawing.Size(225, 61);
            this.asociar.TabIndex = 7;
            this.asociar.Text = "Asociar";
            this.asociar.UseVisualStyleBackColor = true;
            this.asociar.Click += new System.EventHandler(this.asociar_Click);
            // 
            // desasociar
            // 
            this.desasociar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desasociar.Location = new System.Drawing.Point(1138, 580);
            this.desasociar.Name = "desasociar";
            this.desasociar.Size = new System.Drawing.Size(225, 61);
            this.desasociar.TabIndex = 8;
            this.desasociar.Text = "Desasociar";
            this.desasociar.UseVisualStyleBackColor = true;
            this.desasociar.Click += new System.EventHandler(this.desasociar_Click);
            // 
            // subir
            // 
            this.subir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subir.Location = new System.Drawing.Point(1138, 681);
            this.subir.Name = "subir";
            this.subir.Size = new System.Drawing.Size(225, 61);
            this.subir.TabIndex = 9;
            this.subir.Text = "Subir";
            this.subir.UseVisualStyleBackColor = true;
            this.subir.Click += new System.EventHandler(this.subir_Click);
            // 
            // ver
            // 
            this.ver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ver.Location = new System.Drawing.Point(814, 373);
            this.ver.Name = "ver";
            this.ver.Size = new System.Drawing.Size(551, 61);
            this.ver.TabIndex = 11;
            this.ver.Text = "Ver Asociados";
            this.ver.UseVisualStyleBackColor = true;
            this.ver.Click += new System.EventHandler(this.ver_Click);
            // 
            // LigarRFCACuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1417, 890);
            this.Controls.Add(this.ver);
            this.Controls.Add(this.subir);
            this.Controls.Add(this.desasociar);
            this.Controls.Add(this.asociar);
            this.Controls.Add(this.cuentaSunPlusText);
            this.Controls.Add(this.razonSocialText);
            this.Controls.Add(this.rfcText);
            this.Controls.Add(this.listaPreferencias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LigarRFCACuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ligar RFC a cuenta sunplus";
            this.Load += new System.EventHandler(this.LigarRFCACuenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listaPreferencias;
        private System.Windows.Forms.TextBox rfcText;
        private System.Windows.Forms.TextBox razonSocialText;
        private System.Windows.Forms.TextBox cuentaSunPlusText;
        private System.Windows.Forms.Button asociar;
        private System.Windows.Forms.Button desasociar;
        private System.Windows.Forms.Button subir;
        private System.Windows.Forms.Button ver;
    }
}