namespace AdministradorXML
{
    partial class asignarPROJaWHO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(asignarPROJaWHO));
            this.label1 = new System.Windows.Forms.Label();
            this.personaCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.proyectoCombo = new System.Windows.Forms.ComboBox();
            this.relacionList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.asignarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona:";
            // 
            // personaCombo
            // 
            this.personaCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personaCombo.FormattingEnabled = true;
            this.personaCombo.Location = new System.Drawing.Point(259, 55);
            this.personaCombo.Name = "personaCombo";
            this.personaCombo.Size = new System.Drawing.Size(289, 45);
            this.personaCombo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(673, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proj:";
            // 
            // proyectoCombo
            // 
            this.proyectoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proyectoCombo.FormattingEnabled = true;
            this.proyectoCombo.Location = new System.Drawing.Point(840, 52);
            this.proyectoCombo.Name = "proyectoCombo";
            this.proyectoCombo.Size = new System.Drawing.Size(373, 45);
            this.proyectoCombo.TabIndex = 3;
            // 
            // relacionList
            // 
            this.relacionList.Location = new System.Drawing.Point(55, 186);
            this.relacionList.Name = "relacionList";
            this.relacionList.Size = new System.Drawing.Size(1482, 597);
            this.relacionList.TabIndex = 4;
            this.relacionList.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1560, 791);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = ".";
            // 
            // asignarButton
            // 
            this.asignarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asignarButton.Location = new System.Drawing.Point(1294, 55);
            this.asignarButton.Name = "asignarButton";
            this.asignarButton.Size = new System.Drawing.Size(212, 65);
            this.asignarButton.TabIndex = 6;
            this.asignarButton.Text = "Asignar";
            this.asignarButton.UseVisualStyleBackColor = true;
            this.asignarButton.Click += new System.EventHandler(this.asignarButton_Click);
            // 
            // asignarPROJaWHO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1590, 826);
            this.Controls.Add(this.asignarButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.relacionList);
            this.Controls.Add(this.proyectoCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.personaCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "asignarPROJaWHO";
            this.Text = "Asignar PROJ a WHO";
            this.Load += new System.EventHandler(this.asignarPROJaWHO_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox personaCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox proyectoCombo;
        private System.Windows.Forms.ListView relacionList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button asignarButton;
    }
}