<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIndicadorAjusteProveedor
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdAplicar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdLimpiar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdExportar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.lblFiltrosActivos = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanelMain = New DevExpress.XtraEditors.PanelControl()
        Me.PanelFiltros = New DevExpress.XtraEditors.GroupControl()
        Me.dtpFechaDel = New DevExpress.XtraEditors.DateEdit()
        Me.dtpFechaAl = New DevExpress.XtraEditors.DateEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProveedor = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoProducto = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNoContenedor = New DevExpress.XtraEditors.TextEdit()
        Me.lblFechaDel = New DevExpress.XtraEditors.LabelControl()
        Me.lblFechaAl = New DevExpress.XtraEditors.LabelControl()
        Me.lblBodega = New DevExpress.XtraEditors.LabelControl()
        Me.lblProveedor = New DevExpress.XtraEditors.LabelControl()
        Me.lblTipoProducto = New DevExpress.XtraEditors.LabelControl()
        Me.lblNoContenedor = New DevExpress.XtraEditors.LabelControl()
        Me.btnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLimpiar = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelContenido = New DevExpress.XtraEditors.PanelControl()
        Me.PanelKPI = New DevExpress.XtraEditors.PanelControl()
        Me.TabIndicadores = New DevExpress.XtraTab.XtraTabControl()
        Me.tabProveedor = New DevExpress.XtraTab.XtraTabPage()
        Me.tabTipoProducto = New DevExpress.XtraTab.XtraTabPage()
        Me.tabTendencia = New DevExpress.XtraTab.XtraTabPage()
        Me.tabLote = New DevExpress.XtraTab.XtraTabPage()
        Me.tabVencimiento = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelMain.SuspendLayout()
        CType(Me.PanelFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFiltros.SuspendLayout()
        CType(Me.dtpFechaDel.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaDel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProveedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoContenedor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelContenido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContenido.SuspendLayout()
        CType(Me.PanelKPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabIndicadores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabIndicadores.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdAplicar, Me.cmdLimpiar, Me.cmdExportar, Me.cmdImprimir, Me.cmdSalir, Me.lblRegs, Me.lblFiltrosActivos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 8
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1400, 158)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdAplicar
        '
        Me.cmdAplicar.Caption = "Aplicar Filtros"
        Me.cmdAplicar.Id = 1
        Me.cmdAplicar.Name = "cmdAplicar"
        '
        'cmdLimpiar
        '
        Me.cmdLimpiar.Caption = "Limpiar"
        Me.cmdLimpiar.Id = 2
        Me.cmdLimpiar.Name = "cmdLimpiar"
        '
        'cmdExportar
        '
        Me.cmdExportar.Caption = "Exportar Excel"
        Me.cmdExportar.Id = 3
        Me.cmdExportar.Name = "cmdExportar"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 4
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 5
        Me.cmdSalir.Name = "cmdSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 6
        Me.lblRegs.Name = "lblRegs"
        '
        'lblFiltrosActivos
        '
        Me.lblFiltrosActivos.Caption = "Sin filtros activos"
        Me.lblFiltrosActivos.Id = 7
        Me.lblFiltrosActivos.Name = "lblFiltrosActivos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Indicadores"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdAplicar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdLimpiar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdExportar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblFiltrosActivos)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 770)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1400, 30)
        '
        'PanelMain
        '
        Me.PanelMain.Controls.Add(Me.PanelContenido)
        Me.PanelMain.Controls.Add(Me.PanelFiltros)
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.Location = New System.Drawing.Point(0, 158)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(1400, 612)
        Me.PanelMain.TabIndex = 1
        '
        'PanelFiltros
        '
        Me.PanelFiltros.Controls.Add(Me.btnLimpiar)
        Me.PanelFiltros.Controls.Add(Me.btnAplicar)
        Me.PanelFiltros.Controls.Add(Me.txtNoContenedor)
        Me.PanelFiltros.Controls.Add(Me.lblNoContenedor)
        Me.PanelFiltros.Controls.Add(Me.cmbTipoProducto)
        Me.PanelFiltros.Controls.Add(Me.lblTipoProducto)
        Me.PanelFiltros.Controls.Add(Me.cmbProveedor)
        Me.PanelFiltros.Controls.Add(Me.lblProveedor)
        Me.PanelFiltros.Controls.Add(Me.cmbBodega)
        Me.PanelFiltros.Controls.Add(Me.lblBodega)
        Me.PanelFiltros.Controls.Add(Me.dtpFechaAl)
        Me.PanelFiltros.Controls.Add(Me.lblFechaAl)
        Me.PanelFiltros.Controls.Add(Me.dtpFechaDel)
        Me.PanelFiltros.Controls.Add(Me.lblFechaDel)
        Me.PanelFiltros.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelFiltros.Location = New System.Drawing.Point(2, 2)
        Me.PanelFiltros.Name = "PanelFiltros"
        Me.PanelFiltros.Size = New System.Drawing.Size(290, 608)
        Me.PanelFiltros.TabIndex = 0
        Me.PanelFiltros.Text = "Filtros"
        '
        'lblFechaDel
        '
        Me.lblFechaDel.Location = New System.Drawing.Point(12, 35)
        Me.lblFechaDel.Name = "lblFechaDel"
        Me.lblFechaDel.Size = New System.Drawing.Size(53, 16)
        Me.lblFechaDel.Text = "Fecha del:"
        '
        'dtpFechaDel
        '
        Me.dtpFechaDel.EditValue = Nothing
        Me.dtpFechaDel.Location = New System.Drawing.Point(12, 55)
        Me.dtpFechaDel.Name = "dtpFechaDel"
        Me.dtpFechaDel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDel.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDel.Size = New System.Drawing.Size(266, 22)
        Me.dtpFechaDel.TabIndex = 0
        '
        'lblFechaAl
        '
        Me.lblFechaAl.Location = New System.Drawing.Point(12, 85)
        Me.lblFechaAl.Name = "lblFechaAl"
        Me.lblFechaAl.Size = New System.Drawing.Size(46, 16)
        Me.lblFechaAl.Text = "Fecha al:"
        '
        'dtpFechaAl
        '
        Me.dtpFechaAl.EditValue = Nothing
        Me.dtpFechaAl.Location = New System.Drawing.Point(12, 105)
        Me.dtpFechaAl.Name = "dtpFechaAl"
        Me.dtpFechaAl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAl.Size = New System.Drawing.Size(266, 22)
        Me.dtpFechaAl.TabIndex = 1
        '
        'lblBodega
        '
        Me.lblBodega.Location = New System.Drawing.Point(12, 140)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(43, 16)
        Me.lblBodega.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(12, 160)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = "(Todas)"
        Me.cmbBodega.Size = New System.Drawing.Size(266, 22)
        Me.cmbBodega.TabIndex = 2
        '
        'lblProveedor
        '
        Me.lblProveedor.Location = New System.Drawing.Point(12, 195)
        Me.lblProveedor.Name = "lblProveedor"
        Me.lblProveedor.Size = New System.Drawing.Size(54, 16)
        Me.lblProveedor.Text = "Proveedor:"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.Location = New System.Drawing.Point(12, 215)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProveedor.Properties.NullText = "(Todos)"
        Me.cmbProveedor.Size = New System.Drawing.Size(266, 22)
        Me.cmbProveedor.TabIndex = 3
        '
        'lblTipoProducto
        '
        Me.lblTipoProducto.Location = New System.Drawing.Point(12, 250)
        Me.lblTipoProducto.Name = "lblTipoProducto"
        Me.lblTipoProducto.Size = New System.Drawing.Size(78, 16)
        Me.lblTipoProducto.Text = "Tipo Producto:"
        '
        'cmbTipoProducto
        '
        Me.cmbTipoProducto.Location = New System.Drawing.Point(12, 270)
        Me.cmbTipoProducto.Name = "cmbTipoProducto"
        Me.cmbTipoProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoProducto.Properties.NullText = "(Todos)"
        Me.cmbTipoProducto.Size = New System.Drawing.Size(266, 22)
        Me.cmbTipoProducto.TabIndex = 4
        '
        'lblNoContenedor
        '
        Me.lblNoContenedor.Location = New System.Drawing.Point(12, 305)
        Me.lblNoContenedor.Name = "lblNoContenedor"
        Me.lblNoContenedor.Size = New System.Drawing.Size(73, 16)
        Me.lblNoContenedor.Text = "No. Contenedor:"
        '
        'txtNoContenedor
        '
        Me.txtNoContenedor.Location = New System.Drawing.Point(12, 325)
        Me.txtNoContenedor.Name = "txtNoContenedor"
        Me.txtNoContenedor.Properties.NullValuePrompt = "(Cualquiera)"
        Me.txtNoContenedor.Size = New System.Drawing.Size(266, 22)
        Me.txtNoContenedor.TabIndex = 5
        '
        'btnAplicar
        '
        Me.btnAplicar.Location = New System.Drawing.Point(12, 370)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(127, 35)
        Me.btnAplicar.TabIndex = 6
        Me.btnAplicar.Text = "Aplicar"
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Location = New System.Drawing.Point(151, 370)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(127, 35)
        Me.btnLimpiar.TabIndex = 7
        Me.btnLimpiar.Text = "Limpiar"
        '
        'PanelContenido
        '
        Me.PanelContenido.Controls.Add(Me.TabIndicadores)
        Me.PanelContenido.Controls.Add(Me.PanelKPI)
        Me.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContenido.Location = New System.Drawing.Point(292, 2)
        Me.PanelContenido.Name = "PanelContenido"
        Me.PanelContenido.Size = New System.Drawing.Size(1106, 608)
        Me.PanelContenido.TabIndex = 1
        '
        'PanelKPI
        '
        Me.PanelKPI.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelKPI.Location = New System.Drawing.Point(2, 2)
        Me.PanelKPI.Name = "PanelKPI"
        Me.PanelKPI.Size = New System.Drawing.Size(1102, 110)
        Me.PanelKPI.TabIndex = 0
        '
        'TabIndicadores
        '
        Me.TabIndicadores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabIndicadores.Location = New System.Drawing.Point(2, 112)
        Me.TabIndicadores.Name = "TabIndicadores"
        Me.TabIndicadores.SelectedTabPage = Me.tabProveedor
        Me.TabIndicadores.Size = New System.Drawing.Size(1102, 494)
        Me.TabIndicadores.TabIndex = 1
        Me.TabIndicadores.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabProveedor, Me.tabTipoProducto, Me.tabTendencia, Me.tabLote, Me.tabVencimiento})
        '
        'tabProveedor
        '
        Me.tabProveedor.Name = "tabProveedor"
        Me.tabProveedor.Size = New System.Drawing.Size(1096, 466)
        Me.tabProveedor.Text = "Por Proveedor / Contenedor"
        '
        'tabTipoProducto
        '
        Me.tabTipoProducto.Name = "tabTipoProducto"
        Me.tabTipoProducto.Size = New System.Drawing.Size(1096, 466)
        Me.tabTipoProducto.Text = "Por Tipo Producto"
        '
        'tabTendencia
        '
        Me.tabTendencia.Name = "tabTendencia"
        Me.tabTendencia.Size = New System.Drawing.Size(1096, 466)
        Me.tabTendencia.Text = "Tendencia Diaria"
        '
        'tabLote
        '
        Me.tabLote.Name = "tabLote"
        Me.tabLote.Size = New System.Drawing.Size(1096, 466)
        Me.tabLote.Text = "Por Lote"
        '
        'tabVencimiento
        '
        Me.tabVencimiento.Name = "tabVencimiento"
        Me.tabVencimiento.Size = New System.Drawing.Size(1096, 466)
        Me.tabVencimiento.Text = "Por Vencimiento"
        '
        'frmIndicadorAjusteProveedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1400, 800)
        Me.Controls.Add(Me.PanelMain)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmIndicadorAjusteProveedor"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Indicadores de Ajustes por Proveedor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelMain.ResumeLayout(False)
        CType(Me.PanelFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFiltros.ResumeLayout(False)
        Me.PanelFiltros.PerformLayout()
        CType(Me.dtpFechaDel.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaDel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProveedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoContenedor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelContenido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContenido.ResumeLayout(False)
        CType(Me.PanelKPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabIndicadores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabIndicadores.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents cmdAplicar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdLimpiar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdExportar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblFiltrosActivos As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents PanelMain As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelFiltros As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblFechaDel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpFechaDel As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblFechaAl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpFechaAl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblBodega As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblProveedor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbProveedor As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblTipoProducto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTipoProducto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblNoContenedor As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoContenedor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLimpiar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelContenido As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelKPI As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TabIndicadores As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabProveedor As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabTipoProducto As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabTendencia As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabLote As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabVencimiento As DevExpress.XtraTab.XtraTabPage

End Class
