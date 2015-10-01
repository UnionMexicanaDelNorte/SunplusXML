namespace AdministradorXML
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impuestosDelMesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturasCanceladasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLDeCuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLDeBalanzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLDePolizasDelMesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.misDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permisosDeCuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentasBancariasDeProveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingresosSATList = new System.Windows.Forms.ListView();
            this.ingresosSunPlusList = new System.Windows.Forms.ListView();
            this.gastosSATList = new System.Windows.Forms.ListView();
            this.gastosSunplusList = new System.Windows.Forms.ListView();
            this.periodoLabel = new System.Windows.Forms.Label();
            this.periodosCombo = new System.Windows.Forms.ComboBox();
            this.totalIngresoSATLabel = new System.Windows.Forms.Label();
            this.totalEgresosSATLabel = new System.Windows.Forms.Label();
            this.totalIngresosSunplusLabel = new System.Windows.Forms.Label();
            this.totalEgresosSunplusLabel = new System.Windows.Forms.Label();
            this.xMLDeCuentasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLDeBalanzaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bDToolStripMenuItem,
            this.verToolStripMenuItem,
            this.generarToolStripMenuItem,
            this.administrarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1590, 43);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bDToolStripMenuItem
            // 
            this.bDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variablesToolStripMenuItem});
            this.bDToolStripMenuItem.Name = "bDToolStripMenuItem";
            this.bDToolStripMenuItem.Size = new System.Drawing.Size(59, 39);
            this.bDToolStripMenuItem.Text = "BD";
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(243, 40);
            this.variablesToolStripMenuItem.Text = "Variables";
            this.variablesToolStripMenuItem.Click += new System.EventHandler(this.variablesToolStripMenuItem_Click);
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.impuestosDelMesToolStripMenuItem,
            this.facturasCanceladasToolStripMenuItem});
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(65, 39);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // impuestosDelMesToolStripMenuItem
            // 
            this.impuestosDelMesToolStripMenuItem.Name = "impuestosDelMesToolStripMenuItem";
            this.impuestosDelMesToolStripMenuItem.Size = new System.Drawing.Size(314, 40);
            this.impuestosDelMesToolStripMenuItem.Text = "Impuestos del Mes";
            this.impuestosDelMesToolStripMenuItem.Click += new System.EventHandler(this.impuestosDelMesToolStripMenuItem_Click);
            // 
            // facturasCanceladasToolStripMenuItem
            // 
            this.facturasCanceladasToolStripMenuItem.Name = "facturasCanceladasToolStripMenuItem";
            this.facturasCanceladasToolStripMenuItem.Size = new System.Drawing.Size(314, 40);
            this.facturasCanceladasToolStripMenuItem.Text = "Facturas Canceladas";
            this.facturasCanceladasToolStripMenuItem.Click += new System.EventHandler(this.facturasCanceladasToolStripMenuItem_Click);
            // 
            // generarToolStripMenuItem
            // 
            this.generarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLDeCuentasToolStripMenuItem,
            this.xMLDeBalanzaToolStripMenuItem,
            this.xMLDePolizasDelMesToolStripMenuItem});
            this.generarToolStripMenuItem.Name = "generarToolStripMenuItem";
            this.generarToolStripMenuItem.Size = new System.Drawing.Size(115, 39);
            this.generarToolStripMenuItem.Text = "Generar";
            // 
            // xMLDeCuentasToolStripMenuItem
            // 
            this.xMLDeCuentasToolStripMenuItem.Name = "xMLDeCuentasToolStripMenuItem";
            this.xMLDeCuentasToolStripMenuItem.Size = new System.Drawing.Size(351, 40);
            this.xMLDeCuentasToolStripMenuItem.Text = "XML de Cuentas";
            this.xMLDeCuentasToolStripMenuItem.Click += new System.EventHandler(this.xMLDeCuentasToolStripMenuItem_Click);
            // 
            // xMLDeBalanzaToolStripMenuItem
            // 
            this.xMLDeBalanzaToolStripMenuItem.Name = "xMLDeBalanzaToolStripMenuItem";
            this.xMLDeBalanzaToolStripMenuItem.Size = new System.Drawing.Size(351, 40);
            this.xMLDeBalanzaToolStripMenuItem.Text = "XML de Balanza";
            this.xMLDeBalanzaToolStripMenuItem.Click += new System.EventHandler(this.xMLDeBalanzaToolStripMenuItem_Click);
            // 
            // xMLDePolizasDelMesToolStripMenuItem
            // 
            this.xMLDePolizasDelMesToolStripMenuItem.Name = "xMLDePolizasDelMesToolStripMenuItem";
            this.xMLDePolizasDelMesToolStripMenuItem.Size = new System.Drawing.Size(351, 40);
            this.xMLDePolizasDelMesToolStripMenuItem.Text = "XML de Polizas del Mes";
            this.xMLDePolizasDelMesToolStripMenuItem.Click += new System.EventHandler(this.xMLDePolizasDelMesToolStripMenuItem_Click);
            // 
            // administrarToolStripMenuItem
            // 
            this.administrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.misDatosToolStripMenuItem,
            this.permisosDeCuentasToolStripMenuItem,
            this.cuentasBancariasDeProveedoresToolStripMenuItem,
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem,
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem});
            this.administrarToolStripMenuItem.Name = "administrarToolStripMenuItem";
            this.administrarToolStripMenuItem.Size = new System.Drawing.Size(156, 39);
            this.administrarToolStripMenuItem.Text = "Administrar";
            // 
            // misDatosToolStripMenuItem
            // 
            this.misDatosToolStripMenuItem.Name = "misDatosToolStripMenuItem";
            this.misDatosToolStripMenuItem.Size = new System.Drawing.Size(734, 40);
            this.misDatosToolStripMenuItem.Text = "Mis datos";
            this.misDatosToolStripMenuItem.Click += new System.EventHandler(this.misDatosToolStripMenuItem_Click);
            // 
            // permisosDeCuentasToolStripMenuItem
            // 
            this.permisosDeCuentasToolStripMenuItem.Name = "permisosDeCuentasToolStripMenuItem";
            this.permisosDeCuentasToolStripMenuItem.Size = new System.Drawing.Size(734, 40);
            this.permisosDeCuentasToolStripMenuItem.Text = "Permisos de cuentas";
            this.permisosDeCuentasToolStripMenuItem.Click += new System.EventHandler(this.permisosDeCuentasToolStripMenuItem_Click);
            // 
            // cuentasBancariasDeProveedoresToolStripMenuItem
            // 
            this.cuentasBancariasDeProveedoresToolStripMenuItem.Name = "cuentasBancariasDeProveedoresToolStripMenuItem";
            this.cuentasBancariasDeProveedoresToolStripMenuItem.Size = new System.Drawing.Size(734, 40);
            this.cuentasBancariasDeProveedoresToolStripMenuItem.Text = "Cuentas bancarias de proveedores";
            this.cuentasBancariasDeProveedoresToolStripMenuItem.Click += new System.EventHandler(this.cuentasBancariasDeProveedoresToolStripMenuItem_Click);
            // 
            // asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem
            // 
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem.Name = "asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem";
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem.Size = new System.Drawing.Size(734, 40);
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem.Text = "Asociar Tipo de Diario - Cuenta Sunplus - Cuenta Bancaria";
            this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem.Click += new System.EventHandler(this.asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem_Click);
            // 
            // asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem
            // 
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem.Name = "asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem";
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem.Size = new System.Drawing.Size(734, 40);
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem.Text = "Asociar código agrupador del SAT con cuenta Sunplus";
            this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem.Click += new System.EventHandler(this.asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLDeCuentasToolStripMenuItem1,
            this.xMLDeBalanzaToolStripMenuItem1,
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(97, 39);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(269, 40);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // ingresosSATList
            // 
            this.ingresosSATList.Location = new System.Drawing.Point(50, 100);
            this.ingresosSATList.Name = "ingresosSATList";
            this.ingresosSATList.Size = new System.Drawing.Size(542, 405);
            this.ingresosSATList.TabIndex = 1;
            this.ingresosSATList.UseCompatibleStateImageBehavior = false;
            this.ingresosSATList.SelectedIndexChanged += new System.EventHandler(this.ingresosSATList_SelectedIndexChanged);
            // 
            // ingresosSunPlusList
            // 
            this.ingresosSunPlusList.Location = new System.Drawing.Point(663, 113);
            this.ingresosSunPlusList.Name = "ingresosSunPlusList";
            this.ingresosSunPlusList.Size = new System.Drawing.Size(542, 405);
            this.ingresosSunPlusList.TabIndex = 2;
            this.ingresosSunPlusList.UseCompatibleStateImageBehavior = false;
            this.ingresosSunPlusList.SelectedIndexChanged += new System.EventHandler(this.ingresosSunPlusList_SelectedIndexChanged);
            // 
            // gastosSATList
            // 
            this.gastosSATList.Location = new System.Drawing.Point(36, 579);
            this.gastosSATList.Name = "gastosSATList";
            this.gastosSATList.Size = new System.Drawing.Size(542, 442);
            this.gastosSATList.TabIndex = 3;
            this.gastosSATList.UseCompatibleStateImageBehavior = false;
            this.gastosSATList.SelectedIndexChanged += new System.EventHandler(this.gastosSATList_SelectedIndexChanged);
            // 
            // gastosSunplusList
            // 
            this.gastosSunplusList.Location = new System.Drawing.Point(663, 579);
            this.gastosSunplusList.Name = "gastosSunplusList";
            this.gastosSunplusList.Size = new System.Drawing.Size(542, 442);
            this.gastosSunplusList.TabIndex = 4;
            this.gastosSunplusList.UseCompatibleStateImageBehavior = false;
            this.gastosSunplusList.SelectedIndexChanged += new System.EventHandler(this.gastosSunplusList_SelectedIndexChanged);
            // 
            // periodoLabel
            // 
            this.periodoLabel.AutoSize = true;
            this.periodoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodoLabel.Location = new System.Drawing.Point(12, 59);
            this.periodoLabel.Name = "periodoLabel";
            this.periodoLabel.Size = new System.Drawing.Size(138, 38);
            this.periodoLabel.TabIndex = 5;
            this.periodoLabel.Text = "Periodo:";
            // 
            // periodosCombo
            // 
            this.periodosCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.periodosCombo.FormattingEnabled = true;
            this.periodosCombo.Location = new System.Drawing.Point(262, 56);
            this.periodosCombo.Name = "periodosCombo";
            this.periodosCombo.Size = new System.Drawing.Size(193, 45);
            this.periodosCombo.TabIndex = 6;
            this.periodosCombo.SelectedIndexChanged += new System.EventHandler(this.periodosCombo_SelectedIndexChanged);
            // 
            // totalIngresoSATLabel
            // 
            this.totalIngresoSATLabel.AutoSize = true;
            this.totalIngresoSATLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalIngresoSATLabel.Location = new System.Drawing.Point(50, 528);
            this.totalIngresoSATLabel.Name = "totalIngresoSATLabel";
            this.totalIngresoSATLabel.Size = new System.Drawing.Size(103, 38);
            this.totalIngresoSATLabel.TabIndex = 7;
            this.totalIngresoSATLabel.Text = "label1";
            // 
            // totalEgresosSATLabel
            // 
            this.totalEgresosSATLabel.AutoSize = true;
            this.totalEgresosSATLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalEgresosSATLabel.Location = new System.Drawing.Point(52, 1033);
            this.totalEgresosSATLabel.Name = "totalEgresosSATLabel";
            this.totalEgresosSATLabel.Size = new System.Drawing.Size(103, 38);
            this.totalEgresosSATLabel.TabIndex = 8;
            this.totalEgresosSATLabel.Text = "label1";
            // 
            // totalIngresosSunplusLabel
            // 
            this.totalIngresosSunplusLabel.AutoSize = true;
            this.totalIngresosSunplusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalIngresosSunplusLabel.Location = new System.Drawing.Point(663, 547);
            this.totalIngresosSunplusLabel.Name = "totalIngresosSunplusLabel";
            this.totalIngresosSunplusLabel.Size = new System.Drawing.Size(103, 38);
            this.totalIngresosSunplusLabel.TabIndex = 9;
            this.totalIngresosSunplusLabel.Text = "label1";
            // 
            // totalEgresosSunplusLabel
            // 
            this.totalEgresosSunplusLabel.AutoSize = true;
            this.totalEgresosSunplusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.06283F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalEgresosSunplusLabel.Location = new System.Drawing.Point(1226, 665);
            this.totalEgresosSunplusLabel.Name = "totalEgresosSunplusLabel";
            this.totalEgresosSunplusLabel.Size = new System.Drawing.Size(103, 38);
            this.totalEgresosSunplusLabel.TabIndex = 10;
            this.totalEgresosSunplusLabel.Text = "label1";
            // 
            // xMLDeCuentasToolStripMenuItem1
            // 
            this.xMLDeCuentasToolStripMenuItem1.Name = "xMLDeCuentasToolStripMenuItem1";
            this.xMLDeCuentasToolStripMenuItem1.Size = new System.Drawing.Size(269, 40);
            this.xMLDeCuentasToolStripMenuItem1.Text = "XML de Cuentas";
            this.xMLDeCuentasToolStripMenuItem1.Click += new System.EventHandler(this.xMLDeCuentasToolStripMenuItem1_Click);
            // 
            // xMLDeBalanzaToolStripMenuItem1
            // 
            this.xMLDeBalanzaToolStripMenuItem1.Name = "xMLDeBalanzaToolStripMenuItem1";
            this.xMLDeBalanzaToolStripMenuItem1.Size = new System.Drawing.Size(269, 40);
            this.xMLDeBalanzaToolStripMenuItem1.Text = "XML de Balanza";
            this.xMLDeBalanzaToolStripMenuItem1.Click += new System.EventHandler(this.xMLDeBalanzaToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1624, 1068);
            this.Controls.Add(this.totalEgresosSunplusLabel);
            this.Controls.Add(this.totalIngresosSunplusLabel);
            this.Controls.Add(this.totalEgresosSATLabel);
            this.Controls.Add(this.totalIngresoSATLabel);
            this.Controls.Add(this.periodosCombo);
            this.Controls.Add(this.periodoLabel);
            this.Controls.Add(this.gastosSunplusList);
            this.Controls.Add(this.gastosSATList);
            this.Controls.Add(this.ingresosSunPlusList);
            this.Controls.Add(this.ingresosSATList);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Administrador de Sunplusito XML";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permisosDeCuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem misDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuentasBancariasDeProveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asociarTipoDeDiarioCuentaSunplusCuentaBancariaToolStripMenuItem;
        private System.Windows.Forms.ListView ingresosSATList;
        private System.Windows.Forms.ListView ingresosSunPlusList;
        private System.Windows.Forms.ListView gastosSATList;
        private System.Windows.Forms.ListView gastosSunplusList;
        private System.Windows.Forms.Label periodoLabel;
        private System.Windows.Forms.ComboBox periodosCombo;
        private System.Windows.Forms.Label totalIngresoSATLabel;
        private System.Windows.Forms.Label totalEgresosSATLabel;
        private System.Windows.Forms.Label totalIngresosSunplusLabel;
        private System.Windows.Forms.Label totalEgresosSunplusLabel;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impuestosDelMesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturasCanceladasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLDeCuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLDeBalanzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLDePolizasDelMesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asociarCódigoAgrupadorDelSATConCuentaSunplusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLDeCuentasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xMLDeBalanzaToolStripMenuItem1;
    }
}

