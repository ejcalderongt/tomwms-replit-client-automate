<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEjecuciones
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecuciones))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdEjecuciones = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.panelFechas = New DevExpress.XtraEditors.PanelControl()
        Me.lblFechas = New System.Windows.Forms.Label()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.fchAl = New System.Windows.Forms.DateTimePicker()
        Me.fchDel = New System.Windows.Forms.DateTimePicker()
        Me.DSEjecuciones = New DSEjecuciones()
        Me.DSEjecucionesBindingSource = New System.Windows.Forms.BindingSource()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource()
        Me.colIdEjecución = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpresa = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colConfiguración = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colInterface = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdEjecuciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelFechas.SuspendLayout()
        CType(Me.DSEjecuciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSEjecucionesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdImprimir, Me.cmdActualizar, Me.cmdSalir})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(985, 143)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Glyph = CType(resources.GetObject("cmdImprimir.Glyph"), System.Drawing.Image)
        Me.cmdImprimir.Id = 1
        Me.cmdImprimir.LargeGlyph = CType(resources.GetObject("cmdImprimir.LargeGlyph"), System.Drawing.Image)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Glyph = CType(resources.GetObject("cmdActualizar.Glyph"), System.Drawing.Image)
        Me.cmdActualizar.Id = 2
        Me.cmdActualizar.LargeGlyph = CType(resources.GetObject("cmdActualizar.LargeGlyph"), System.Drawing.Image)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Glyph = My.Resources.Resources.button_power_basic_blue
        Me.cmdSalir.Id = 3
        Me.cmdSalir.LargeGlyph = My.Resources.Resources.button_power_basic_blue
        Me.cmdSalir.Name = "cmdSalir"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 622)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(985, 31)
        '
        'grdEjecuciones
        '
        Me.grdEjecuciones.DataSource = Me.EncabezadoBindingSource
        Me.grdEjecuciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEjecuciones.Location = New System.Drawing.Point(215, 143)
        Me.grdEjecuciones.MainView = Me.GridView1
        Me.grdEjecuciones.MenuManager = Me.RibbonControl
        Me.grdEjecuciones.Name = "grdEjecuciones"
        Me.grdEjecuciones.Size = New System.Drawing.Size(770, 479)
        Me.grdEjecuciones.TabIndex = 2
        Me.grdEjecuciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdEjecución, Me.colEmpresa, Me.colBodega, Me.colPropietario, Me.colConfiguración, Me.colInterface, Me.colFecha})
        Me.GridView1.GridControl = Me.grdEjecuciones
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'panelFechas
        '
        Me.panelFechas.Controls.Add(Me.lblFechas)
        Me.panelFechas.Controls.Add(Me.lblAl)
        Me.panelFechas.Controls.Add(Me.lblDel)
        Me.panelFechas.Controls.Add(Me.fchAl)
        Me.panelFechas.Controls.Add(Me.fchDel)
        Me.panelFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelFechas.Location = New System.Drawing.Point(0, 143)
        Me.panelFechas.Name = "panelFechas"
        Me.panelFechas.Size = New System.Drawing.Size(215, 479)
        Me.panelFechas.TabIndex = 3
        '
        'lblFechas
        '
        Me.lblFechas.AutoSize = True
        Me.lblFechas.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechas.Location = New System.Drawing.Point(47, 3)
        Me.lblFechas.Name = "lblFechas"
        Me.lblFechas.Size = New System.Drawing.Size(146, 19)
        Me.lblFechas.TabIndex = 4
        Me.lblFechas.Text = "Rango de Fechas"
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(12, 186)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(20, 13)
        Me.lblAl.TabIndex = 4
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(6, 155)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(26, 13)
        Me.lblDel.TabIndex = 4
        Me.lblDel.Text = "Del:"
        '
        'fchAl
        '
        Me.fchAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.fchAl.Location = New System.Drawing.Point(38, 186)
        Me.fchAl.Name = "fchAl"
        Me.fchAl.Size = New System.Drawing.Size(172, 21)
        Me.fchAl.TabIndex = 4
        '
        'fchDel
        '
        Me.fchDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.fchDel.Location = New System.Drawing.Point(38, 149)
        Me.fchDel.Name = "fchDel"
        Me.fchDel.Size = New System.Drawing.Size(172, 21)
        Me.fchDel.TabIndex = 4
        '
        'DSEjecuciones
        '
        Me.DSEjecuciones.DataSetName = "DSEjecuciones"
        Me.DSEjecuciones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DSEjecucionesBindingSource
        '
        Me.DSEjecucionesBindingSource.DataSource = Me.DSEjecuciones
        Me.DSEjecucionesBindingSource.Position = 0
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DSEjecucionesBindingSource
        '
        'colIdEjecución
        '
        Me.colIdEjecución.FieldName = "IdEjecución"
        Me.colIdEjecución.Name = "colIdEjecución"
        Me.colIdEjecución.Visible = True
        Me.colIdEjecución.VisibleIndex = 0
        '
        'colEmpresa
        '
        Me.colEmpresa.FieldName = "Empresa"
        Me.colEmpresa.Name = "colEmpresa"
        Me.colEmpresa.Visible = True
        Me.colEmpresa.VisibleIndex = 1
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.Name = "colBodega"
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 2
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 3
        '
        'colConfiguración
        '
        Me.colConfiguración.FieldName = "Configuración"
        Me.colConfiguración.Name = "colConfiguración"
        Me.colConfiguración.Visible = True
        Me.colConfiguración.VisibleIndex = 4
        '
        'colInterface
        '
        Me.colInterface.FieldName = "Interface"
        Me.colInterface.Name = "colInterface"
        Me.colInterface.Visible = True
        Me.colInterface.VisibleIndex = 5
        '
        'colFecha
        '
        Me.colFecha.FieldName = "Fecha"
        Me.colFecha.Name = "colFecha"
        Me.colFecha.Visible = True
        Me.colFecha.VisibleIndex = 6
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DSEjecucionesBindingSource
        '
        'frmEjecuciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 653)
        Me.Controls.Add(Me.grdEjecuciones)
        Me.Controls.Add(Me.panelFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmEjecuciones"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Reporte de Ejecuciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdEjecuciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelFechas.ResumeLayout(False)
        Me.panelFechas.PerformLayout()
        CType(Me.DSEjecuciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSEjecucionesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grdEjecuciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents panelFechas As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblFechas As Label
    Friend WithEvents lblAl As Label
    Friend WithEvents lblDel As Label
    Friend WithEvents fchAl As DateTimePicker
    Friend WithEvents fchDel As DateTimePicker
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DSEjecucionesBindingSource As BindingSource
    Friend WithEvents DSEjecuciones As DSEjecuciones
    Friend WithEvents colIdEjecución As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEmpresa As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colConfiguración As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colInterface As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource As BindingSource
End Class
