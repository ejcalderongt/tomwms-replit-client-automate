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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecuciones))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdEjecuciones = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSEjecucionesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DSEjecuciones = New TOMWMS.DSEjecuciones()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdEjecución = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmpresa = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colConfiguración = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colInterface = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.panelFechas = New DevExpress.XtraEditors.PanelControl()
        Me.lblFechas = New System.Windows.Forms.Label()
        Me.lblAl = New System.Windows.Forms.Label()
        Me.lblDel = New System.Windows.Forms.Label()
        Me.fchAl = New System.Windows.Forms.DateTimePicker()
        Me.fchDel = New System.Windows.Forms.DateTimePicker()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdEjecuciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSEjecucionesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DSEjecuciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelFechas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelFechas.SuspendLayout()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdImprimir, Me.cmdActualizar, Me.cmdSalir})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 385
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1149, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 1
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 2
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 3
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 774)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1149, 30)
        '
        'grdEjecuciones
        '
        Me.grdEjecuciones.DataSource = Me.EncabezadoBindingSource
        Me.grdEjecuciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEjecuciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdEjecuciones.Location = New System.Drawing.Point(251, 193)
        Me.grdEjecuciones.MainView = Me.GridView1
        Me.grdEjecuciones.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdEjecuciones.MenuManager = Me.RibbonControl
        Me.grdEjecuciones.Name = "grdEjecuciones"
        Me.grdEjecuciones.Size = New System.Drawing.Size(898, 581)
        Me.grdEjecuciones.TabIndex = 2
        Me.grdEjecuciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DSEjecucionesBindingSource
        '
        'DSEjecucionesBindingSource
        '
        Me.DSEjecucionesBindingSource.DataSource = Me.DSEjecuciones
        Me.DSEjecucionesBindingSource.Position = 0
        '
        'DSEjecuciones
        '
        Me.DSEjecuciones.DataSetName = "DSEjecuciones"
        Me.DSEjecuciones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdEjecución, Me.colEmpresa, Me.colBodega, Me.colPropietario, Me.colConfiguración, Me.colInterface, Me.colFecha})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdEjecuciones
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 933
        Me.GridView1.OptionsFind.AlwaysVisible = True
        '
        'colIdEjecución
        '
        Me.colIdEjecución.FieldName = "IdEjecución"
        Me.colIdEjecución.MinWidth = 23
        Me.colIdEjecución.Name = "colIdEjecución"
        Me.colIdEjecución.Visible = True
        Me.colIdEjecución.VisibleIndex = 0
        Me.colIdEjecución.Width = 87
        '
        'colEmpresa
        '
        Me.colEmpresa.FieldName = "Empresa"
        Me.colEmpresa.MinWidth = 23
        Me.colEmpresa.Name = "colEmpresa"
        Me.colEmpresa.Visible = True
        Me.colEmpresa.VisibleIndex = 1
        Me.colEmpresa.Width = 87
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 23
        Me.colBodega.Name = "colBodega"
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 2
        Me.colBodega.Width = 87
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.MinWidth = 23
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 3
        Me.colPropietario.Width = 87
        '
        'colConfiguración
        '
        Me.colConfiguración.FieldName = "Configuración"
        Me.colConfiguración.MinWidth = 23
        Me.colConfiguración.Name = "colConfiguración"
        Me.colConfiguración.Visible = True
        Me.colConfiguración.VisibleIndex = 4
        Me.colConfiguración.Width = 87
        '
        'colInterface
        '
        Me.colInterface.FieldName = "Interface"
        Me.colInterface.MinWidth = 23
        Me.colInterface.Name = "colInterface"
        Me.colInterface.Visible = True
        Me.colInterface.VisibleIndex = 5
        Me.colInterface.Width = 87
        '
        'colFecha
        '
        Me.colFecha.FieldName = "Fecha"
        Me.colFecha.MinWidth = 23
        Me.colFecha.Name = "colFecha"
        Me.colFecha.Visible = True
        Me.colFecha.VisibleIndex = 6
        Me.colFecha.Width = 87
        '
        'panelFechas
        '
        Me.panelFechas.Controls.Add(Me.lblFechas)
        Me.panelFechas.Controls.Add(Me.lblAl)
        Me.panelFechas.Controls.Add(Me.lblDel)
        Me.panelFechas.Controls.Add(Me.fchAl)
        Me.panelFechas.Controls.Add(Me.fchDel)
        Me.panelFechas.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelFechas.Location = New System.Drawing.Point(0, 193)
        Me.panelFechas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.panelFechas.Name = "panelFechas"
        Me.panelFechas.Size = New System.Drawing.Size(251, 581)
        Me.panelFechas.TabIndex = 3
        '
        'lblFechas
        '
        Me.lblFechas.AutoSize = True
        Me.lblFechas.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechas.Location = New System.Drawing.Point(55, 4)
        Me.lblFechas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechas.Name = "lblFechas"
        Me.lblFechas.Size = New System.Drawing.Size(182, 24)
        Me.lblFechas.TabIndex = 4
        Me.lblFechas.Text = "Rango de Fechas"
        '
        'lblAl
        '
        Me.lblAl.AutoSize = True
        Me.lblAl.Location = New System.Drawing.Point(14, 229)
        Me.lblAl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAl.Name = "lblAl"
        Me.lblAl.Size = New System.Drawing.Size(23, 16)
        Me.lblAl.TabIndex = 4
        Me.lblAl.Text = "Al:"
        '
        'lblDel
        '
        Me.lblDel.AutoSize = True
        Me.lblDel.Location = New System.Drawing.Point(7, 191)
        Me.lblDel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDel.Name = "lblDel"
        Me.lblDel.Size = New System.Drawing.Size(30, 16)
        Me.lblDel.TabIndex = 4
        Me.lblDel.Text = "Del:"
        '
        'fchAl
        '
        Me.fchAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.fchAl.Location = New System.Drawing.Point(44, 229)
        Me.fchAl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.fchAl.Name = "fchAl"
        Me.fchAl.Size = New System.Drawing.Size(200, 23)
        Me.fchAl.TabIndex = 4
        '
        'fchDel
        '
        Me.fchDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.fchDel.Location = New System.Drawing.Point(44, 183)
        Me.fchDel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.fchDel.Name = "fchDel"
        Me.fchDel.Size = New System.Drawing.Size(200, 23)
        Me.fchDel.TabIndex = 4
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DSEjecucionesBindingSource
        '
        'frmEjecuciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1149, 804)
        Me.Controls.Add(Me.grdEjecuciones)
        Me.Controls.Add(Me.panelFechas)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmEjecuciones"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Reporte de Ejecuciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdEjecuciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSEjecucionesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DSEjecuciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelFechas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelFechas.ResumeLayout(False)
        Me.panelFechas.PerformLayout()
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
