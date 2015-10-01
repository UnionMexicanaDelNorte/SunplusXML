namespace AdministradorXML
{
    partial class codigoAgrupador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(codigoAgrupador));
            this.sunplusList = new System.Windows.Forms.ListView();
            this.agrupadoresList = new System.Windows.Forms.ListView();
            this.enlazadosList = new System.Windows.Forms.ListView();
            this.ligarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sunplusList
            // 
            this.sunplusList.Location = new System.Drawing.Point(871, -1);
            this.sunplusList.Name = "sunplusList";
            this.sunplusList.Size = new System.Drawing.Size(931, 675);
            this.sunplusList.TabIndex = 0;
            this.sunplusList.UseCompatibleStateImageBehavior = false;
            // 
            // agrupadoresList
            // 
            this.agrupadoresList.Location = new System.Drawing.Point(871, 680);
            this.agrupadoresList.MultiSelect = false;
            this.agrupadoresList.Name = "agrupadoresList";
            this.agrupadoresList.Size = new System.Drawing.Size(916, 414);
            this.agrupadoresList.TabIndex = 1;
            this.agrupadoresList.UseCompatibleStateImageBehavior = false;
            // 
            // enlazadosList
            // 
            this.enlazadosList.Location = new System.Drawing.Point(-2, -1);
            this.enlazadosList.MultiSelect = false;
            this.enlazadosList.Name = "enlazadosList";
            this.enlazadosList.Size = new System.Drawing.Size(882, 1148);
            this.enlazadosList.TabIndex = 2;
            this.enlazadosList.UseCompatibleStateImageBehavior = false;
            // 
            // ligarButton
            // 
            this.ligarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ligarButton.Location = new System.Drawing.Point(1479, 1100);
            this.ligarButton.Name = "ligarButton";
            this.ligarButton.Size = new System.Drawing.Size(308, 56);
            this.ligarButton.TabIndex = 3;
            this.ligarButton.Text = "Ligar";
            this.ligarButton.UseVisualStyleBackColor = true;
            this.ligarButton.Click += new System.EventHandler(this.ligarButton_Click);
            // 
            // codigoAgrupador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1861, 1163);
            this.Controls.Add(this.ligarButton);
            this.Controls.Add(this.enlazadosList);
            this.Controls.Add(this.agrupadoresList);
            this.Controls.Add(this.sunplusList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "codigoAgrupador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Asociar código agrupador del SAT con cuenta sunplus";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.codigoAgrupador_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView sunplusList;
        private System.Windows.Forms.ListView agrupadoresList;
        private System.Windows.Forms.ListView enlazadosList;
        private System.Windows.Forms.Button ligarButton;
    }
}