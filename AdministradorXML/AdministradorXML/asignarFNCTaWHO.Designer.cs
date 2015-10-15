namespace AdministradorXML
{
    partial class asignarFNCTaWHO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(asignarFNCTaWHO));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.personaCombo = new System.Windows.Forms.ComboBox();
            this.funcionCombo = new System.Windows.Forms.ComboBox();
            this.relacionList = new System.Windows.Forms.ListView();
            this.asignarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(84, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Persona (who):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(852, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "FNCT:";
            // 
            // personaCombo
            // 
            this.personaCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personaCombo.FormattingEnabled = true;
            this.personaCombo.Location = new System.Drawing.Point(375, 58);
            this.personaCombo.Name = "personaCombo";
            this.personaCombo.Size = new System.Drawing.Size(377, 45);
            this.personaCombo.TabIndex = 2;
            // 
            // funcionCombo
            // 
            this.funcionCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.funcionCombo.FormattingEnabled = true;
            this.funcionCombo.Location = new System.Drawing.Point(1007, 54);
            this.funcionCombo.Name = "funcionCombo";
            this.funcionCombo.Size = new System.Drawing.Size(405, 45);
            this.funcionCombo.TabIndex = 3;
            // 
            // relacionList
            // 
            this.relacionList.Location = new System.Drawing.Point(91, 201);
            this.relacionList.Name = "relacionList";
            this.relacionList.Size = new System.Drawing.Size(1321, 527);
            this.relacionList.TabIndex = 4;
            this.relacionList.UseCompatibleStateImageBehavior = false;
            // 
            // asignarButton
            // 
            this.asignarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asignarButton.Location = new System.Drawing.Point(1183, 125);
            this.asignarButton.Name = "asignarButton";
            this.asignarButton.Size = new System.Drawing.Size(229, 51);
            this.asignarButton.TabIndex = 5;
            this.asignarButton.Text = "Asignar";
            this.asignarButton.UseVisualStyleBackColor = true;
            this.asignarButton.Click += new System.EventHandler(this.asignarButton_Click);
            // 
            // asignarFNCTaWHO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1554, 778);
            this.Controls.Add(this.asignarButton);
            this.Controls.Add(this.relacionList);
            this.Controls.Add(this.funcionCombo);
            this.Controls.Add(this.personaCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "asignarFNCTaWHO";
            this.Text = "Asignar FNCT a WHO";
            this.Load += new System.EventHandler(this.asignarFNCTaWHO_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox personaCombo;
        private System.Windows.Forms.ComboBox funcionCombo;
        private System.Windows.Forms.ListView relacionList;
        private System.Windows.Forms.Button asignarButton;
    }
}