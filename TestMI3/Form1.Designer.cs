namespace TestMI3
{
    partial class frmInicio
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
            this.dgrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bodegaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getByCodigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barrasPalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transaccionesOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedorBodegaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDespachosPendientesDeProcesarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajustesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.propietarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testRecepcionHHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getSingleMI3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservaCaso1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrid
            // 
            this.dgrid.BackgroundColor = System.Drawing.Color.Cornsilk;
            this.dgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgrid.Location = new System.Drawing.Point(0, 308);
            this.dgrid.Margin = new System.Windows.Forms.Padding(4);
            this.dgrid.Name = "dgrid";
            this.dgrid.RowHeadersWidth = 51;
            this.dgrid.Size = new System.Drawing.Size(1067, 246);
            this.dgrid.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.qAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bodegaToolStripMenuItem,
            this.getByCodigoToolStripMenuItem,
            this.pedidoCompraToolStripMenuItem,
            this.barrasPalletToolStripMenuItem,
            this.pedidoClienteToolStripMenuItem,
            this.proveedorToolStripMenuItem,
            this.transaccionesOutToolStripMenuItem,
            this.proveedorBodegaToolStripMenuItem,
            this.getDespachosPendientesDeProcesarToolStripMenuItem,
            this.ajustesToolStripMenuItem,
            this.usuarioToolStripMenuItem,
            this.tsmiReportes,
            this.propietarioToolStripMenuItem,
            this.testRecepcionHHToolStripMenuItem,
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem,
            this.getSingleMI3ToolStripMenuItem,
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem,
            this.productoToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // bodegaToolStripMenuItem
            // 
            this.bodegaToolStripMenuItem.Name = "bodegaToolStripMenuItem";
            this.bodegaToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.bodegaToolStripMenuItem.Text = "Bodega";
            this.bodegaToolStripMenuItem.Click += new System.EventHandler(this.bodegaToolStripMenuItem_Click);
            // 
            // getByCodigoToolStripMenuItem
            // 
            this.getByCodigoToolStripMenuItem.Name = "getByCodigoToolStripMenuItem";
            this.getByCodigoToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.getByCodigoToolStripMenuItem.Text = "Get_By_Codigo";
            this.getByCodigoToolStripMenuItem.Click += new System.EventHandler(this.getByCodigoToolStripMenuItem_Click);
            // 
            // pedidoCompraToolStripMenuItem
            // 
            this.pedidoCompraToolStripMenuItem.Name = "pedidoCompraToolStripMenuItem";
            this.pedidoCompraToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.pedidoCompraToolStripMenuItem.Text = "Pedido Compra";
            this.pedidoCompraToolStripMenuItem.Click += new System.EventHandler(this.pedidoCompraToolStripMenuItem_Click);
            // 
            // barrasPalletToolStripMenuItem
            // 
            this.barrasPalletToolStripMenuItem.Name = "barrasPalletToolStripMenuItem";
            this.barrasPalletToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.barrasPalletToolStripMenuItem.Text = "Barras_Pallet";
            this.barrasPalletToolStripMenuItem.Click += new System.EventHandler(this.barrasPalletToolStripMenuItem_Click);
            // 
            // pedidoClienteToolStripMenuItem
            // 
            this.pedidoClienteToolStripMenuItem.Name = "pedidoClienteToolStripMenuItem";
            this.pedidoClienteToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.pedidoClienteToolStripMenuItem.Text = "Pedido Cliente";
            this.pedidoClienteToolStripMenuItem.Click += new System.EventHandler(this.pedidoClienteToolStripMenuItem_Click);
            // 
            // proveedorToolStripMenuItem
            // 
            this.proveedorToolStripMenuItem.Name = "proveedorToolStripMenuItem";
            this.proveedorToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.proveedorToolStripMenuItem.Text = "Proveedor";
            this.proveedorToolStripMenuItem.Click += new System.EventHandler(this.proveedorToolStripMenuItem_Click);
            // 
            // transaccionesOutToolStripMenuItem
            // 
            this.transaccionesOutToolStripMenuItem.Name = "transaccionesOutToolStripMenuItem";
            this.transaccionesOutToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.transaccionesOutToolStripMenuItem.Text = "Transacciones_Out";
            this.transaccionesOutToolStripMenuItem.Click += new System.EventHandler(this.transaccionesOutToolStripMenuItem_Click);
            // 
            // proveedorBodegaToolStripMenuItem
            // 
            this.proveedorBodegaToolStripMenuItem.Name = "proveedorBodegaToolStripMenuItem";
            this.proveedorBodegaToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.proveedorBodegaToolStripMenuItem.Text = "ProveedorBodega";
            this.proveedorBodegaToolStripMenuItem.Click += new System.EventHandler(this.proveedorBodegaToolStripMenuItem_Click);
            // 
            // getDespachosPendientesDeProcesarToolStripMenuItem
            // 
            this.getDespachosPendientesDeProcesarToolStripMenuItem.Name = "getDespachosPendientesDeProcesarToolStripMenuItem";
            this.getDespachosPendientesDeProcesarToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.getDespachosPendientesDeProcesarToolStripMenuItem.Text = "Get_Despachos_Pendientes_De_Procesar";
            this.getDespachosPendientesDeProcesarToolStripMenuItem.Click += new System.EventHandler(this.getDespachosPendientesDeProcesarToolStripMenuItem_Click);
            // 
            // ajustesToolStripMenuItem
            // 
            this.ajustesToolStripMenuItem.Name = "ajustesToolStripMenuItem";
            this.ajustesToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.ajustesToolStripMenuItem.Text = "Ajustes";
            this.ajustesToolStripMenuItem.Click += new System.EventHandler(this.ajustesToolStripMenuItem_Click);
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            this.usuarioToolStripMenuItem.Click += new System.EventHandler(this.usuarioToolStripMenuItem_Click);
            // 
            // tsmiReportes
            // 
            this.tsmiReportes.Name = "tsmiReportes";
            this.tsmiReportes.Size = new System.Drawing.Size(555, 26);
            this.tsmiReportes.Text = "Reportes";
            this.tsmiReportes.Click += new System.EventHandler(this.tsmiReportes_Click);
            // 
            // propietarioToolStripMenuItem
            // 
            this.propietarioToolStripMenuItem.Name = "propietarioToolStripMenuItem";
            this.propietarioToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.propietarioToolStripMenuItem.Text = "Propietario";
            this.propietarioToolStripMenuItem.Click += new System.EventHandler(this.propietarioToolStripMenuItem_Click);
            // 
            // testRecepcionHHToolStripMenuItem
            // 
            this.testRecepcionHHToolStripMenuItem.Name = "testRecepcionHHToolStripMenuItem";
            this.testRecepcionHHToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.testRecepcionHHToolStripMenuItem.Text = "Test Recepcion HH";
            this.testRecepcionHHToolStripMenuItem.Click += new System.EventHandler(this.testRecepcionHHToolStripMenuItem_Click);
            // 
            // getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem
            // 
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem.Name = "getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem";
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem.Text = "Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas";
            this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem.Click += new System.EventHandler(this.getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem_Click);
            // 
            // getSingleMI3ToolStripMenuItem
            // 
            this.getSingleMI3ToolStripMenuItem.Name = "getSingleMI3ToolStripMenuItem";
            this.getSingleMI3ToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.getSingleMI3ToolStripMenuItem.Text = "Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto";
            this.getSingleMI3ToolStripMenuItem.Click += new System.EventHandler(this.getSingleMI3ToolStripMenuItem_Click);
            // 
            // ubicacionesDeLosLotesDeLasOPToolStripMenuItem
            // 
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem.Name = "ubicacionesDeLosLotesDeLasOPToolStripMenuItem";
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem.Text = "Registrar Lote Documento de Ingreso";
            this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem.Click += new System.EventHandler(this.ubicacionesDeLosLotesDeLasOPToolStripMenuItem_Click);
            // 
            // productoToolStripMenuItem
            // 
            this.productoToolStripMenuItem.Name = "productoToolStripMenuItem";
            this.productoToolStripMenuItem.Size = new System.Drawing.Size(555, 26);
            this.productoToolStripMenuItem.Text = "Producto";
            this.productoToolStripMenuItem.Click += new System.EventHandler(this.productoToolStripMenuItem_Click);
            // 
            // qAToolStripMenuItem
            // 
            this.qAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reservaCaso1ToolStripMenuItem});
            this.qAToolStripMenuItem.Name = "qAToolStripMenuItem";
            this.qAToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.qAToolStripMenuItem.Text = "QA";
            // 
            // reservaCaso1ToolStripMenuItem
            // 
            this.reservaCaso1ToolStripMenuItem.Name = "reservaCaso1ToolStripMenuItem";
            this.reservaCaso1ToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.reservaCaso1ToolStripMenuItem.Text = "Reserva Caso 1 - IDEAL_20231002011101";
            this.reservaCaso1ToolStripMenuItem.ToolTipText = "IDEAL_20231002011101";
            this.reservaCaso1ToolStripMenuItem.Click += new System.EventHandler(this.reservaCaso1ToolStripMenuItem_Click);
            // 
            // txtResult
            // 
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtResult.Location = new System.Drawing.Point(0, 191);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(1067, 117);
            this.txtResult.TabIndex = 3;
            this.txtResult.Text = "";
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.dgrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bodegaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getByCodigoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidoCompraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barrasPalletToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.ToolStripMenuItem pedidoClienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transaccionesOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedorBodegaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDespachosPendientesDeProcesarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajustesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiReportes;
        private System.Windows.Forms.ToolStripMenuItem propietarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testRecepcionHHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAlmacenajeHistoricoByIdPropietarioAndRangoFechasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getSingleMI3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ubicacionesDeLosLotesDeLasOPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservaCaso1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productoToolStripMenuItem;
    }
}

