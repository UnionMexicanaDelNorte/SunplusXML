namespace AdministradorXML
{
    partial class XMLBalanza
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLBalanza));
            this.label1 = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tipoCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTime1 = new System.Windows.Forms.DateTimePicker();
            this.previewTree = new System.Windows.Forms.TreeView();
            this.generarButton = new System.Windows.Forms.Button();
            this.noGenerarPreview = new System.Windows.Forms.CheckBox();
            this.guardarButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(261, 53);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(374, 45);
            this.periodosCombo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(661, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo:";
            // 
            // tipoCombo
            // 
            this.tipoCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoCombo.FormattingEnabled = true;
            this.tipoCombo.Location = new System.Drawing.Point(793, 50);
            this.tipoCombo.Name = "tipoCombo";
            this.tipoCombo.Size = new System.Drawing.Size(309, 45);
            this.tipoCombo.TabIndex = 3;
            this.tipoCombo.SelectedIndexChanged += new System.EventHandler(this.tipoCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1190, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "Última modificación:";
            this.label3.Visible = false;
            // 
            // dateTime1
            // 
            this.dateTime1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime1.CustomFormat = "dd/MM/yyyy";
            this.dateTime1.Location = new System.Drawing.Point(1197, 60);
            this.dateTime1.Name = "dateTime1";
            this.dateTime1.Size = new System.Drawing.Size(353, 31);
            this.dateTime1.TabIndex = 5;
            this.dateTime1.Visible = false;
            // 
            // previewTree
            // 
            this.previewTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewTree.Location = new System.Drawing.Point(44, 304);
            this.previewTree.Name = "previewTree";
            this.previewTree.Size = new System.Drawing.Size(1545, 437);
            this.previewTree.TabIndex = 6;
            // 
            // generarButton
            // 
            this.generarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarButton.Location = new System.Drawing.Point(186, 129);
            this.generarButton.Name = "generarButton";
            this.generarButton.Size = new System.Drawing.Size(284, 84);
            this.generarButton.TabIndex = 7;
            this.generarButton.Text = "Generar";
            this.generarButton.UseVisualStyleBackColor = true;
            this.generarButton.Click += new System.EventHandler(this.generarButton_Click);
            // 
            // noGenerarPreview
            // 
            this.noGenerarPreview.AutoSize = true;
            this.noGenerarPreview.Checked = true;
            this.noGenerarPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noGenerarPreview.Location = new System.Drawing.Point(64, 235);
            this.noGenerarPreview.Name = "noGenerarPreview";
            this.noGenerarPreview.Size = new System.Drawing.Size(227, 30);
            this.noGenerarPreview.TabIndex = 8;
            this.noGenerarPreview.Text = "No generar preview";
            this.noGenerarPreview.UseVisualStyleBackColor = true;
            // 
            // guardarButton
            // 
            this.guardarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarButton.Location = new System.Drawing.Point(722, 129);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(314, 84);
            this.guardarButton.TabIndex = 9;
            this.guardarButton.Text = "Guardar";
            this.guardarButton.UseVisualStyleBackColor = true;
            this.guardarButton.Visible = false;
            this.guardarButton.Click += new System.EventHandler(this.guardarButton_Click);
            // 
            // XMLBalanza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1629, 780);
            this.Controls.Add(this.guardarButton);
            this.Controls.Add(this.noGenerarPreview);
            this.Controls.Add(this.generarButton);
            this.Controls.Add(this.previewTree);
            this.Controls.Add(this.dateTime1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tipoCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMLBalanza";
            this.Text = "Generar XML de Balanza";
            this.Load += new System.EventHandler(this.XMLBalanza_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipoCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTime1;
        private System.Windows.Forms.TreeView previewTree;
        private System.Windows.Forms.Button generarButton;
        private System.Windows.Forms.CheckBox noGenerarPreview;
        private System.Windows.Forms.Button guardarButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}