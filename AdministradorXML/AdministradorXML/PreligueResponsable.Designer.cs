namespace AdministradorXML
{
    partial class PreligueResponsable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreligueResponsable));
            this.listaPreview = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.verXML = new System.Windows.Forms.Button();
            this.verPDF = new System.Windows.Forms.Button();
            this.verPoliza = new System.Windows.Forms.Button();
            this.verMovimiento = new System.Windows.Forms.Button();
            this.rechazar = new System.Windows.Forms.Button();
            this.ligar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listaPreview
            // 
            this.listaPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaPreview.Location = new System.Drawing.Point(1, 122);
            this.listaPreview.MultiSelect = false;
            this.listaPreview.Name = "listaPreview";
            this.listaPreview.Size = new System.Drawing.Size(1931, 824);
            this.listaPreview.TabIndex = 0;
            this.listaPreview.UseCompatibleStateImageBehavior = false;
            this.listaPreview.SelectedIndexChanged += new System.EventHandler(this.listaPreview_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1846, 97);
            this.label1.TabIndex = 1;
            this.label1.Text = "Instrucciones: Puedes usar la tecla \"x\" para ver el XML, \"p\" para ver el PDF, \"o\"" +
    " para ver la Poliza, \"m\" para ver el movimiento, \"r\" para rechazar y \"l\" para li" +
    "gar.";
            // 
            // verXML
            // 
            this.verXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verXML.Location = new System.Drawing.Point(83, 962);
            this.verXML.Name = "verXML";
            this.verXML.Size = new System.Drawing.Size(258, 80);
            this.verXML.TabIndex = 2;
            this.verXML.Text = "Ver XML";
            this.verXML.UseVisualStyleBackColor = true;
            this.verXML.Click += new System.EventHandler(this.verXML_Click);
            // 
            // verPDF
            // 
            this.verPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verPDF.Location = new System.Drawing.Point(419, 962);
            this.verPDF.Name = "verPDF";
            this.verPDF.Size = new System.Drawing.Size(258, 80);
            this.verPDF.TabIndex = 3;
            this.verPDF.Text = "Ver PDF";
            this.verPDF.UseVisualStyleBackColor = true;
            this.verPDF.Click += new System.EventHandler(this.verPDF_Click);
            // 
            // verPoliza
            // 
            this.verPoliza.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verPoliza.Location = new System.Drawing.Point(749, 962);
            this.verPoliza.Name = "verPoliza";
            this.verPoliza.Size = new System.Drawing.Size(271, 80);
            this.verPoliza.TabIndex = 4;
            this.verPoliza.Text = "Ver Poliza";
            this.verPoliza.UseVisualStyleBackColor = true;
            this.verPoliza.Click += new System.EventHandler(this.verPoliza_Click);
            // 
            // verMovimiento
            // 
            this.verMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verMovimiento.Location = new System.Drawing.Point(1070, 962);
            this.verMovimiento.Name = "verMovimiento";
            this.verMovimiento.Size = new System.Drawing.Size(334, 80);
            this.verMovimiento.TabIndex = 5;
            this.verMovimiento.Text = "Ver Movimiento";
            this.verMovimiento.UseVisualStyleBackColor = true;
            this.verMovimiento.Click += new System.EventHandler(this.verMovimiento_Click);
            // 
            // rechazar
            // 
            this.rechazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rechazar.Location = new System.Drawing.Point(1444, 962);
            this.rechazar.Name = "rechazar";
            this.rechazar.Size = new System.Drawing.Size(238, 80);
            this.rechazar.TabIndex = 6;
            this.rechazar.Text = "Rechazar";
            this.rechazar.UseVisualStyleBackColor = true;
            this.rechazar.Click += new System.EventHandler(this.rechazar_Click);
            // 
            // ligar
            // 
            this.ligar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligar.Location = new System.Drawing.Point(1723, 962);
            this.ligar.Name = "ligar";
            this.ligar.Size = new System.Drawing.Size(190, 80);
            this.ligar.TabIndex = 7;
            this.ligar.Text = "Ligar";
            this.ligar.UseVisualStyleBackColor = true;
            this.ligar.Click += new System.EventHandler(this.ligar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1910, 1032);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = ".";
            // 
            // PreligueResponsable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1940, 1067);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ligar);
            this.Controls.Add(this.rechazar);
            this.Controls.Add(this.verMovimiento);
            this.Controls.Add(this.verPoliza);
            this.Controls.Add(this.verPDF);
            this.Controls.Add(this.verXML);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreligueResponsable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preligue responsable";
            this.Load += new System.EventHandler(this.PreligueResponsable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listaPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button verXML;
        private System.Windows.Forms.Button verPDF;
        private System.Windows.Forms.Button verPoliza;
        private System.Windows.Forms.Button verMovimiento;
        private System.Windows.Forms.Button rechazar;
        private System.Windows.Forms.Button ligar;
        private System.Windows.Forms.Label label2;
    }
}