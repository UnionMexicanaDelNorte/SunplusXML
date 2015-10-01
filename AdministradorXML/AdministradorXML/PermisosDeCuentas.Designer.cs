namespace AdministradorXML
{
    partial class PermisosDeCuentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermisosDeCuentas));
            this.tipoDePermisosCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.todasLasCuentasList = new System.Windows.Forms.ListView();
            this.cuentasConPermisosList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.darPermisoAUnoButton = new System.Windows.Forms.Button();
            this.quitarPermisoATodosButton = new System.Windows.Forms.Button();
            this.quitarPermisoAUnoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tipoDePermisosCombo
            // 
            this.tipoDePermisosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoDePermisosCombo.FormattingEnabled = true;
            this.tipoDePermisosCombo.Location = new System.Drawing.Point(356, 64);
            this.tipoDePermisosCombo.Name = "tipoDePermisosCombo";
            this.tipoDePermisosCombo.Size = new System.Drawing.Size(485, 45);
            this.tipoDePermisosCombo.TabIndex = 0;
            this.tipoDePermisosCombo.SelectedIndexChanged += new System.EventHandler(this.tipoDePermisosCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo de Permisos:";
            // 
            // todasLasCuentasList
            // 
            this.todasLasCuentasList.Location = new System.Drawing.Point(39, 202);
            this.todasLasCuentasList.Name = "todasLasCuentasList";
            this.todasLasCuentasList.Size = new System.Drawing.Size(753, 534);
            this.todasLasCuentasList.TabIndex = 2;
            this.todasLasCuentasList.UseCompatibleStateImageBehavior = false;
            // 
            // cuentasConPermisosList
            // 
            this.cuentasConPermisosList.Location = new System.Drawing.Point(927, 202);
            this.cuentasConPermisosList.Name = "cuentasConPermisosList";
            this.cuentasConPermisosList.Size = new System.Drawing.Size(748, 534);
            this.cuentasConPermisosList.TabIndex = 3;
            this.cuentasConPermisosList.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "Todas las cuentas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(920, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(352, 38);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cuentas con permisos:";
            // 
            // darPermisoAUnoButton
            // 
            this.darPermisoAUnoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darPermisoAUnoButton.Location = new System.Drawing.Point(828, 232);
            this.darPermisoAUnoButton.Name = "darPermisoAUnoButton";
            this.darPermisoAUnoButton.Size = new System.Drawing.Size(66, 67);
            this.darPermisoAUnoButton.TabIndex = 6;
            this.darPermisoAUnoButton.Text = ">";
            this.darPermisoAUnoButton.UseVisualStyleBackColor = true;
            this.darPermisoAUnoButton.Click += new System.EventHandler(this.darPermisoAUnoButton_Click);
            // 
            // quitarPermisoATodosButton
            // 
            this.quitarPermisoATodosButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitarPermisoATodosButton.Location = new System.Drawing.Point(819, 491);
            this.quitarPermisoATodosButton.Name = "quitarPermisoATodosButton";
            this.quitarPermisoATodosButton.Size = new System.Drawing.Size(93, 64);
            this.quitarPermisoATodosButton.TabIndex = 7;
            this.quitarPermisoATodosButton.Text = "<<";
            this.quitarPermisoATodosButton.UseVisualStyleBackColor = true;
            this.quitarPermisoATodosButton.Click += new System.EventHandler(this.quitarPermisoATodosButton_Click);
            // 
            // quitarPermisoAUnoButton
            // 
            this.quitarPermisoAUnoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitarPermisoAUnoButton.Location = new System.Drawing.Point(828, 611);
            this.quitarPermisoAUnoButton.Name = "quitarPermisoAUnoButton";
            this.quitarPermisoAUnoButton.Size = new System.Drawing.Size(66, 70);
            this.quitarPermisoAUnoButton.TabIndex = 8;
            this.quitarPermisoAUnoButton.Text = "<";
            this.quitarPermisoAUnoButton.UseVisualStyleBackColor = true;
            this.quitarPermisoAUnoButton.Click += new System.EventHandler(this.quitarPermisoAUnoButton_Click);
            // 
            // PermisosDeCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1782, 879);
            this.Controls.Add(this.quitarPermisoAUnoButton);
            this.Controls.Add(this.quitarPermisoATodosButton);
            this.Controls.Add(this.darPermisoAUnoButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cuentasConPermisosList);
            this.Controls.Add(this.todasLasCuentasList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tipoDePermisosCombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermisosDeCuentas";
            this.Text = "Permisos de cuentas";
            this.Load += new System.EventHandler(this.PermisosDeCuentas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox tipoDePermisosCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView todasLasCuentasList;
        private System.Windows.Forms.ListView cuentasConPermisosList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button darPermisoAUnoButton;
        private System.Windows.Forms.Button quitarPermisoATodosButton;
        private System.Windows.Forms.Button quitarPermisoAUnoButton;
    }
}