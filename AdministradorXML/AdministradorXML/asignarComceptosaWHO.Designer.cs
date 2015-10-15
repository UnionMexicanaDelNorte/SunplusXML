namespace AdministradorXML
{
    partial class asignarComceptosaWHO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(asignarComceptosaWHO));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.personaCombo = new System.Windows.Forms.ComboBox();
            this.conceptoCombo = new System.Windows.Forms.ComboBox();
            this.relacionList = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.asignarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(627, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Concepto:";
            // 
            // personaCombo
            // 
            this.personaCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personaCombo.FormattingEnabled = true;
            this.personaCombo.Location = new System.Drawing.Point(223, 65);
            this.personaCombo.Name = "personaCombo";
            this.personaCombo.Size = new System.Drawing.Size(312, 45);
            this.personaCombo.TabIndex = 2;
            this.personaCombo.SelectedIndexChanged += new System.EventHandler(this.personaCombo_SelectedIndexChanged);
            // 
            // conceptoCombo
            // 
            this.conceptoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conceptoCombo.FormattingEnabled = true;
            this.conceptoCombo.Location = new System.Drawing.Point(850, 61);
            this.conceptoCombo.Name = "conceptoCombo";
            this.conceptoCombo.Size = new System.Drawing.Size(394, 45);
            this.conceptoCombo.TabIndex = 3;
            // 
            // relacionList
            // 
            this.relacionList.Location = new System.Drawing.Point(45, 149);
            this.relacionList.Name = "relacionList";
            this.relacionList.Size = new System.Drawing.Size(1435, 759);
            this.relacionList.TabIndex = 4;
            this.relacionList.UseCompatibleStateImageBehavior = false;
            this.relacionList.SelectedIndexChanged += new System.EventHandler(this.relacionList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1501, 885);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = ".";
            // 
            // asignarButton
            // 
            this.asignarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asignarButton.Location = new System.Drawing.Point(1287, 49);
            this.asignarButton.Name = "asignarButton";
            this.asignarButton.Size = new System.Drawing.Size(181, 66);
            this.asignarButton.TabIndex = 6;
            this.asignarButton.Text = "Asignar";
            this.asignarButton.UseVisualStyleBackColor = true;
            this.asignarButton.Click += new System.EventHandler(this.asignarButton_Click);
            // 
            // asignarComceptosaWHO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1546, 924);
            this.Controls.Add(this.asignarButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.relacionList);
            this.Controls.Add(this.conceptoCombo);
            this.Controls.Add(this.personaCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "asignarComceptosaWHO";
            this.Text = "Asignar conceptos a WHO";
            this.Load += new System.EventHandler(this.asignarComceptosaWHO_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox personaCombo;
        private System.Windows.Forms.ComboBox conceptoCombo;
        private System.Windows.Forms.ListView relacionList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button asignarButton;
    }
}