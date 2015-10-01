namespace Liga
{
    partial class config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(config));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.baseDeDatos = new System.Windows.Forms.TextBox();
            this.datasource = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.unidadDeNegocio = new System.Windows.Forms.TextBox();
            this.libro = new System.Windows.Forms.TextBox();
            this.barraDeFiscal = new System.Windows.Forms.TextBox();
            this.cerrar = new System.Windows.Forms.Button();
            this.probar = new System.Windows.Forms.Button();
            this.Grabar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base de datos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Datasource:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 26);
            this.label5.TabIndex = 4;
            this.label5.Text = "Unidad de negocio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 477);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "Libro:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 561);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 26);
            this.label7.TabIndex = 6;
            this.label7.Text = "Base de Fiscal:";
            // 
            // baseDeDatos
            // 
            this.baseDeDatos.Location = new System.Drawing.Point(262, 36);
            this.baseDeDatos.Name = "baseDeDatos";
            this.baseDeDatos.Size = new System.Drawing.Size(170, 31);
            this.baseDeDatos.TabIndex = 7;
            // 
            // datasource
            // 
            this.datasource.Location = new System.Drawing.Point(262, 120);
            this.datasource.Name = "datasource";
            this.datasource.Size = new System.Drawing.Size(170, 31);
            this.datasource.TabIndex = 8;
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(262, 217);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(170, 31);
            this.usuario.TabIndex = 9;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(262, 304);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(170, 31);
            this.password.TabIndex = 10;
            // 
            // unidadDeNegocio
            // 
            this.unidadDeNegocio.Location = new System.Drawing.Point(262, 384);
            this.unidadDeNegocio.Name = "unidadDeNegocio";
            this.unidadDeNegocio.Size = new System.Drawing.Size(170, 31);
            this.unidadDeNegocio.TabIndex = 11;
            // 
            // libro
            // 
            this.libro.Location = new System.Drawing.Point(262, 477);
            this.libro.Name = "libro";
            this.libro.Size = new System.Drawing.Size(170, 31);
            this.libro.TabIndex = 12;
            // 
            // barraDeFiscal
            // 
            this.barraDeFiscal.Location = new System.Drawing.Point(262, 555);
            this.barraDeFiscal.Name = "barraDeFiscal";
            this.barraDeFiscal.Size = new System.Drawing.Size(170, 31);
            this.barraDeFiscal.TabIndex = 13;
            // 
            // cerrar
            // 
            this.cerrar.Location = new System.Drawing.Point(595, 545);
            this.cerrar.Name = "cerrar";
            this.cerrar.Size = new System.Drawing.Size(234, 51);
            this.cerrar.TabIndex = 14;
            this.cerrar.Text = "Cerrar";
            this.cerrar.UseVisualStyleBackColor = true;
            this.cerrar.Click += new System.EventHandler(this.cerrar_Click);
            // 
            // probar
            // 
            this.probar.Location = new System.Drawing.Point(595, 447);
            this.probar.Name = "probar";
            this.probar.Size = new System.Drawing.Size(234, 61);
            this.probar.TabIndex = 15;
            this.probar.Text = "Probar Conexión";
            this.probar.UseVisualStyleBackColor = true;
            this.probar.Click += new System.EventHandler(this.probar_Click);
            // 
            // Grabar
            // 
            this.Grabar.Location = new System.Drawing.Point(595, 358);
            this.Grabar.Name = "Grabar";
            this.Grabar.Size = new System.Drawing.Size(234, 58);
            this.Grabar.TabIndex = 16;
            this.Grabar.Text = "Grabar";
            this.Grabar.UseVisualStyleBackColor = true;
            this.Grabar.Click += new System.EventHandler(this.Grabar_Click);
            // 
            // config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 676);
            this.Controls.Add(this.Grabar);
            this.Controls.Add(this.probar);
            this.Controls.Add(this.cerrar);
            this.Controls.Add(this.barraDeFiscal);
            this.Controls.Add(this.libro);
            this.Controls.Add(this.unidadDeNegocio);
            this.Controls.Add(this.password);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.datasource);
            this.Controls.Add(this.baseDeDatos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "config";
            this.Text = "Variables de configuración";
            this.Load += new System.EventHandler(this.config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox baseDeDatos;
        private System.Windows.Forms.TextBox datasource;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox unidadDeNegocio;
        private System.Windows.Forms.TextBox libro;
        private System.Windows.Forms.TextBox barraDeFiscal;
        private System.Windows.Forms.Button cerrar;
        private System.Windows.Forms.Button probar;
        private System.Windows.Forms.Button Grabar;
    }
}