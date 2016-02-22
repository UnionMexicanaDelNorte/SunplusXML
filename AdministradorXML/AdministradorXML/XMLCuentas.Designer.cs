namespace AdministradorXML
{
    partial class XMLCuentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMLCuentas));
            this.cuentasTree = new System.Windows.Forms.TreeView();
            this.guardarButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.anioBox = new System.Windows.Forms.ComboBox();
            this.mesBox = new System.Windows.Forms.ComboBox();
            this.guardarAhoraSiButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.desactivarPreview = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sunplusitoRadio = new System.Windows.Forms.RadioButton();
            this.statutoryRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cuentasTree
            // 
            this.cuentasTree.Location = new System.Drawing.Point(94, 325);
            this.cuentasTree.Name = "cuentasTree";
            this.cuentasTree.Size = new System.Drawing.Size(1461, 742);
            this.cuentasTree.TabIndex = 0;
            // 
            // guardarButton
            // 
            this.guardarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarButton.Location = new System.Drawing.Point(900, 31);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(253, 73);
            this.guardarButton.TabIndex = 1;
            this.guardarButton.Text = "Generar";
            this.guardarButton.UseVisualStyleBackColor = true;
            this.guardarButton.Click += new System.EventHandler(this.guardarButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vigencia:";
            // 
            // anioBox
            // 
            this.anioBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.anioBox.FormattingEnabled = true;
            this.anioBox.Location = new System.Drawing.Point(251, 49);
            this.anioBox.Name = "anioBox";
            this.anioBox.Size = new System.Drawing.Size(175, 45);
            this.anioBox.TabIndex = 3;
            // 
            // mesBox
            // 
            this.mesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesBox.FormattingEnabled = true;
            this.mesBox.Location = new System.Drawing.Point(491, 46);
            this.mesBox.Name = "mesBox";
            this.mesBox.Size = new System.Drawing.Size(322, 45);
            this.mesBox.TabIndex = 5;
            // 
            // guardarAhoraSiButton
            // 
            this.guardarAhoraSiButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarAhoraSiButton.Location = new System.Drawing.Point(1249, 34);
            this.guardarAhoraSiButton.Name = "guardarAhoraSiButton";
            this.guardarAhoraSiButton.Size = new System.Drawing.Size(262, 73);
            this.guardarAhoraSiButton.TabIndex = 6;
            this.guardarAhoraSiButton.Text = "Guardar";
            this.guardarAhoraSiButton.UseVisualStyleBackColor = true;
            this.guardarAhoraSiButton.Visible = false;
            this.guardarAhoraSiButton.Click += new System.EventHandler(this.guardarAhoraSiButton_Click);
            // 
            // desactivarPreview
            // 
            this.desactivarPreview.AutoSize = true;
            this.desactivarPreview.Checked = true;
            this.desactivarPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.desactivarPreview.Location = new System.Drawing.Point(94, 280);
            this.desactivarPreview.Name = "desactivarPreview";
            this.desactivarPreview.Size = new System.Drawing.Size(225, 30);
            this.desactivarPreview.TabIndex = 7;
            this.desactivarPreview.Text = "Desactivar Preview";
            this.desactivarPreview.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sunplusitoRadio);
            this.groupBox1.Controls.Add(this.statutoryRadio);
            this.groupBox1.Location = new System.Drawing.Point(78, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(934, 107);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "De donde agarro el código agrupador";
            // 
            // sunplusitoRadio
            // 
            this.sunplusitoRadio.AutoSize = true;
            this.sunplusitoRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sunplusitoRadio.Location = new System.Drawing.Point(382, 42);
            this.sunplusitoRadio.Name = "sunplusitoRadio";
            this.sunplusitoRadio.Size = new System.Drawing.Size(247, 42);
            this.sunplusitoRadio.TabIndex = 1;
            this.sunplusitoRadio.TabStop = true;
            this.sunplusitoRadio.Text = "Del sunplusito";
            this.sunplusitoRadio.UseVisualStyleBackColor = true;
            // 
            // statutoryRadio
            // 
            this.statutoryRadio.AutoSize = true;
            this.statutoryRadio.Checked = true;
            this.statutoryRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statutoryRadio.Location = new System.Drawing.Point(27, 42);
            this.statutoryRadio.Name = "statutoryRadio";
            this.statutoryRadio.Size = new System.Drawing.Size(226, 42);
            this.statutoryRadio.TabIndex = 0;
            this.statutoryRadio.TabStop = true;
            this.statutoryRadio.Text = "Del statutory";
            this.statutoryRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1575, 1053);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 26);
            this.label2.TabIndex = 9;
            this.label2.Text = ".";
            // 
            // XMLCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1608, 1091);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.desactivarPreview);
            this.Controls.Add(this.guardarAhoraSiButton);
            this.Controls.Add(this.mesBox);
            this.Controls.Add(this.anioBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guardarButton);
            this.Controls.Add(this.cuentasTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMLCuentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar XML Catálogo de cuentas";
            this.Load += new System.EventHandler(this.XMLCuentas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView cuentasTree;
        private System.Windows.Forms.Button guardarButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox anioBox;
        private System.Windows.Forms.ComboBox mesBox;
        private System.Windows.Forms.Button guardarAhoraSiButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox desactivarPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton sunplusitoRadio;
        private System.Windows.Forms.RadioButton statutoryRadio;
        private System.Windows.Forms.Label label2;
    }
}