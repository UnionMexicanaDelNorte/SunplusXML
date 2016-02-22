namespace AdministradorXML
{
    partial class HistorialPreligues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistorialPreligues));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.eliminarPreligue = new System.Windows.Forms.Button();
            this.verMovimiento = new System.Windows.Forms.Button();
            this.verPoliza = new System.Windows.Forms.Button();
            this.verPDF = new System.Windows.Forms.Button();
            this.verXML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(-2, 1);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(2308, 856);
            this.treeView1.TabIndex = 0;
            // 
            // eliminarPreligue
            // 
            this.eliminarPreligue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eliminarPreligue.Location = new System.Drawing.Point(1690, 921);
            this.eliminarPreligue.Name = "eliminarPreligue";
            this.eliminarPreligue.Size = new System.Drawing.Size(356, 98);
            this.eliminarPreligue.TabIndex = 1;
            this.eliminarPreligue.Text = "Eliminar preligue";
            this.eliminarPreligue.UseVisualStyleBackColor = true;
            this.eliminarPreligue.Click += new System.EventHandler(this.eliminarPreligue_Click);
            // 
            // verMovimiento
            // 
            this.verMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verMovimiento.Location = new System.Drawing.Point(1128, 921);
            this.verMovimiento.Name = "verMovimiento";
            this.verMovimiento.Size = new System.Drawing.Size(441, 98);
            this.verMovimiento.TabIndex = 2;
            this.verMovimiento.Text = "Ver movimiento";
            this.verMovimiento.UseVisualStyleBackColor = true;
            this.verMovimiento.Click += new System.EventHandler(this.verMovimiento_Click);
            // 
            // verPoliza
            // 
            this.verPoliza.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verPoliza.Location = new System.Drawing.Point(610, 921);
            this.verPoliza.Name = "verPoliza";
            this.verPoliza.Size = new System.Drawing.Size(441, 98);
            this.verPoliza.TabIndex = 3;
            this.verPoliza.Text = "Ver Poliza";
            this.verPoliza.UseVisualStyleBackColor = true;
            this.verPoliza.Click += new System.EventHandler(this.verPoliza_Click);
            // 
            // verPDF
            // 
            this.verPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verPDF.Location = new System.Drawing.Point(355, 921);
            this.verPDF.Name = "verPDF";
            this.verPDF.Size = new System.Drawing.Size(220, 98);
            this.verPDF.TabIndex = 4;
            this.verPDF.Text = "Ver PDF";
            this.verPDF.UseVisualStyleBackColor = true;
            this.verPDF.Click += new System.EventHandler(this.verPDF_Click);
            // 
            // verXML
            // 
            this.verXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.94764F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verXML.Location = new System.Drawing.Point(74, 921);
            this.verXML.Name = "verXML";
            this.verXML.Size = new System.Drawing.Size(216, 98);
            this.verXML.TabIndex = 5;
            this.verXML.Text = "Ver XML";
            this.verXML.UseVisualStyleBackColor = true;
            this.verXML.Click += new System.EventHandler(this.verXML_Click);
            // 
            // HistorialPreligues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2311, 1070);
            this.Controls.Add(this.verXML);
            this.Controls.Add(this.verPDF);
            this.Controls.Add(this.verPoliza);
            this.Controls.Add(this.verMovimiento);
            this.Controls.Add(this.eliminarPreligue);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HistorialPreligues";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de preligues";
            this.Load += new System.EventHandler(this.HistorialPreligues_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button eliminarPreligue;
        private System.Windows.Forms.Button verMovimiento;
        private System.Windows.Forms.Button verPoliza;
        private System.Windows.Forms.Button verPDF;
        private System.Windows.Forms.Button verXML;
    }
}