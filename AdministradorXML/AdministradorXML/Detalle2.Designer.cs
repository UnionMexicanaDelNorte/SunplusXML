namespace AdministradorXML
{
    partial class Detalle2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalle2));
            this.tipoDeCuentasLabel = new System.Windows.Forms.Label();
            this.diariosList = new System.Windows.Forms.ListView();
            this.enlazadosLabel = new System.Windows.Forms.Label();
            this.enlazadosList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // tipoDeCuentasLabel
            // 
            this.tipoDeCuentasLabel.AutoSize = true;
            this.tipoDeCuentasLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipoDeCuentasLabel.Location = new System.Drawing.Point(48, 40);
            this.tipoDeCuentasLabel.Name = "tipoDeCuentasLabel";
            this.tipoDeCuentasLabel.Size = new System.Drawing.Size(103, 38);
            this.tipoDeCuentasLabel.TabIndex = 0;
            this.tipoDeCuentasLabel.Text = "label1";
            // 
            // diariosList
            // 
            this.diariosList.Location = new System.Drawing.Point(55, 121);
            this.diariosList.Name = "diariosList";
            this.diariosList.Size = new System.Drawing.Size(1663, 507);
            this.diariosList.TabIndex = 1;
            this.diariosList.UseCompatibleStateImageBehavior = false;
            this.diariosList.SelectedIndexChanged += new System.EventHandler(this.diariosList_SelectedIndexChanged);
            // 
            // enlazadosLabel
            // 
            this.enlazadosLabel.AutoSize = true;
            this.enlazadosLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enlazadosLabel.Location = new System.Drawing.Point(48, 654);
            this.enlazadosLabel.Name = "enlazadosLabel";
            this.enlazadosLabel.Size = new System.Drawing.Size(103, 38);
            this.enlazadosLabel.TabIndex = 2;
            this.enlazadosLabel.Text = "label1";
            // 
            // enlazadosList
            // 
            this.enlazadosList.Location = new System.Drawing.Point(55, 716);
            this.enlazadosList.Name = "enlazadosList";
            this.enlazadosList.Size = new System.Drawing.Size(1663, 550);
            this.enlazadosList.TabIndex = 3;
            this.enlazadosList.UseCompatibleStateImageBehavior = false;
            this.enlazadosList.SelectedIndexChanged += new System.EventHandler(this.enlazadosList_SelectedIndexChanged);
            // 
            // Detalle2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1824, 1245);
            this.Controls.Add(this.enlazadosList);
            this.Controls.Add(this.enlazadosLabel);
            this.Controls.Add(this.diariosList);
            this.Controls.Add(this.tipoDeCuentasLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Detalle2";
            this.Text = "Detalle de cuentas";
            this.Load += new System.EventHandler(this.Detalle2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tipoDeCuentasLabel;
        private System.Windows.Forms.ListView diariosList;
        private System.Windows.Forms.Label enlazadosLabel;
        private System.Windows.Forms.ListView enlazadosList;
    }
}