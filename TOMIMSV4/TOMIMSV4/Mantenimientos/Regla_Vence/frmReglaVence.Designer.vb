<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmReglaVence
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReglaVence))
        Me.DgridReglaVencimiento = New DevExpress.XtraGrid.GridControl()
        Me.gvReglaVencimiento = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdReglaVencimiento = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombreRegla = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProductoFamilia = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProductoClasificacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTiempoVencimientoDias = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTipoNotificacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdPropietarioMercancia = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProveedor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdCliente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colActiva = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFechaCreacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUsuarioCreacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFechaModificacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUsuarioModificacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mainRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mainRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Dset_Regla_Vence = New Dset_Regla_Vence()
        Me.Regla_vencimientoTableAdapter = New Dset_Regla_VenceTableAdapters.regla_vencimientoTableAdapter()
        CType(Me.DgridReglaVencimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReglaVencimiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dset_Regla_Vence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgridReglaVencimiento
        '
        Me.DgridReglaVencimiento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridReglaVencimiento.Location = New System.Drawing.Point(0, 193)
        Me.DgridReglaVencimiento.MainView = Me.gvReglaVencimiento
        Me.DgridReglaVencimiento.MenuManager = Me.mainRibbonControl
        Me.DgridReglaVencimiento.Name = "DgridReglaVencimiento"
        Me.DgridReglaVencimiento.Size = New System.Drawing.Size(1303, 434)
        Me.DgridReglaVencimiento.TabIndex = 4
        Me.DgridReglaVencimiento.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvReglaVencimiento})
        '
        'gvReglaVencimiento
        '
        Me.gvReglaVencimiento.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdReglaVencimiento, Me.colNombreRegla, Me.colIdBodega, Me.colIdProductoFamilia, Me.colIdProductoClasificacion, Me.colTiempoVencimientoDias, Me.colTipoNotificacion, Me.colIdPropietarioMercancia, Me.colIdProveedor, Me.colIdCliente, Me.colActiva, Me.colFechaCreacion, Me.colUsuarioCreacion, Me.colFechaModificacion, Me.colUsuarioModificacion})
        Me.gvReglaVencimiento.GridControl = Me.DgridReglaVencimiento
        Me.gvReglaVencimiento.Name = "gvReglaVencimiento"
        Me.gvReglaVencimiento.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        Me.gvReglaVencimiento.OptionsView.ShowAutoFilterRow = True
        Me.gvReglaVencimiento.OptionsView.ShowFooter = True
        '
        'colIdReglaVencimiento
        '
        Me.colIdReglaVencimiento.FieldName = "IdReglaVencimiento"
        Me.colIdReglaVencimiento.MinWidth = 25
        Me.colIdReglaVencimiento.Name = "colIdReglaVencimiento"
        Me.colIdReglaVencimiento.Visible = True
        Me.colIdReglaVencimiento.VisibleIndex = 0
        Me.colIdReglaVencimiento.Width = 86
        '
        'colNombreRegla
        '
        Me.colNombreRegla.Caption = "Nombre"
        Me.colNombreRegla.FieldName = "NombreRegla"
        Me.colNombreRegla.MinWidth = 25
        Me.colNombreRegla.Name = "colNombreRegla"
        Me.colNombreRegla.Visible = True
        Me.colNombreRegla.VisibleIndex = 1
        Me.colNombreRegla.Width = 150
        '
        'colIdBodega
        '
        Me.colIdBodega.Caption = "Bodega"
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 25
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.Visible = True
        Me.colIdBodega.VisibleIndex = 2
        Me.colIdBodega.Width = 92
        '
        'colIdProductoFamilia
        '
        Me.colIdProductoFamilia.Caption = "Familia"
        Me.colIdProductoFamilia.FieldName = "IdProductoFamilia"
        Me.colIdProductoFamilia.MinWidth = 25
        Me.colIdProductoFamilia.Name = "colIdProductoFamilia"
        Me.colIdProductoFamilia.Visible = True
        Me.colIdProductoFamilia.VisibleIndex = 3
        Me.colIdProductoFamilia.Width = 141
        '
        'colIdProductoClasificacion
        '
        Me.colIdProductoClasificacion.Caption = "Clasificación"
        Me.colIdProductoClasificacion.FieldName = "IdProductoClasificacion"
        Me.colIdProductoClasificacion.MinWidth = 25
        Me.colIdProductoClasificacion.Name = "colIdProductoClasificacion"
        Me.colIdProductoClasificacion.Visible = True
        Me.colIdProductoClasificacion.VisibleIndex = 4
        Me.colIdProductoClasificacion.Width = 143
        '
        'colTiempoVencimientoDias
        '
        Me.colTiempoVencimientoDias.Caption = "Tiempo Vencimiento (Días)"
        Me.colTiempoVencimientoDias.FieldName = "TiempoVencimientoDias"
        Me.colTiempoVencimientoDias.MinWidth = 25
        Me.colTiempoVencimientoDias.Name = "colTiempoVencimientoDias"
        Me.colTiempoVencimientoDias.Visible = True
        Me.colTiempoVencimientoDias.VisibleIndex = 5
        Me.colTiempoVencimientoDias.Width = 125
        '
        'colTipoNotificacion
        '
        Me.colTipoNotificacion.FieldName = "TipoNotificacion"
        Me.colTipoNotificacion.MinWidth = 25
        Me.colTipoNotificacion.Name = "colTipoNotificacion"
        Me.colTipoNotificacion.Visible = True
        Me.colTipoNotificacion.VisibleIndex = 6
        Me.colTipoNotificacion.Width = 109
        '
        'colIdPropietarioMercancia
        '
        Me.colIdPropietarioMercancia.Caption = "Propietario"
        Me.colIdPropietarioMercancia.FieldName = "IdPropietarioMercancia"
        Me.colIdPropietarioMercancia.MinWidth = 25
        Me.colIdPropietarioMercancia.Name = "colIdPropietarioMercancia"
        Me.colIdPropietarioMercancia.Visible = True
        Me.colIdPropietarioMercancia.VisibleIndex = 7
        Me.colIdPropietarioMercancia.Width = 93
        '
        'colIdProveedor
        '
        Me.colIdProveedor.Caption = "Proveedor"
        Me.colIdProveedor.FieldName = "IdProveedor"
        Me.colIdProveedor.MinWidth = 25
        Me.colIdProveedor.Name = "colIdProveedor"
        Me.colIdProveedor.Visible = True
        Me.colIdProveedor.VisibleIndex = 8
        Me.colIdProveedor.Width = 116
        '
        'colIdCliente
        '
        Me.colIdCliente.Caption = "Cliente"
        Me.colIdCliente.FieldName = "IdCliente"
        Me.colIdCliente.MinWidth = 25
        Me.colIdCliente.Name = "colIdCliente"
        Me.colIdCliente.Visible = True
        Me.colIdCliente.VisibleIndex = 9
        Me.colIdCliente.Width = 83
        '
        'colActiva
        '
        Me.colActiva.Caption = "Activo"
        Me.colActiva.FieldName = "Activa"
        Me.colActiva.MinWidth = 25
        Me.colActiva.Name = "colActiva"
        Me.colActiva.Visible = True
        Me.colActiva.VisibleIndex = 10
        Me.colActiva.Width = 25
        '
        'colFechaCreacion
        '
        Me.colFechaCreacion.FieldName = "FechaCreacion"
        Me.colFechaCreacion.MinWidth = 25
        Me.colFechaCreacion.Name = "colFechaCreacion"
        Me.colFechaCreacion.Visible = True
        Me.colFechaCreacion.VisibleIndex = 11
        Me.colFechaCreacion.Width = 25
        '
        'colUsuarioCreacion
        '
        Me.colUsuarioCreacion.FieldName = "UsuarioCreacion"
        Me.colUsuarioCreacion.MinWidth = 25
        Me.colUsuarioCreacion.Name = "colUsuarioCreacion"
        Me.colUsuarioCreacion.Visible = True
        Me.colUsuarioCreacion.VisibleIndex = 12
        Me.colUsuarioCreacion.Width = 25
        '
        'colFechaModificacion
        '
        Me.colFechaModificacion.FieldName = "FechaModificacion"
        Me.colFechaModificacion.MinWidth = 25
        Me.colFechaModificacion.Name = "colFechaModificacion"
        Me.colFechaModificacion.Visible = True
        Me.colFechaModificacion.VisibleIndex = 13
        Me.colFechaModificacion.Width = 25
        '
        'colUsuarioModificacion
        '
        Me.colUsuarioModificacion.FieldName = "UsuarioModificacion"
        Me.colUsuarioModificacion.MinWidth = 25
        Me.colUsuarioModificacion.Name = "colUsuarioModificacion"
        Me.colUsuarioModificacion.Visible = True
        Me.colUsuarioModificacion.VisibleIndex = 14
        '
        'mainRibbonControl
        '
        Me.mainRibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.mainRibbonControl.ExpandCollapseItem.Id = 0
        Me.mainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mainRibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.lblRegs, Me.cmdImprimir})
        Me.mainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.mainRibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.mainRibbonControl.MaxItemId = 12
        Me.mainRibbonControl.Name = "mainRibbonControl"
        Me.mainRibbonControl.OptionsMenuMinWidth = 385
        Me.mainRibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.mainRibbonPage})
        Me.mainRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.mainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.mainRibbonControl.Size = New System.Drawing.Size(1303, 193)
        Me.mainRibbonControl.StatusBar = Me.RibbonStatusBar1
        Me.mainRibbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 2
        Me.mnuGuardar.ImageOptions.ImageUri.Uri = "Save"
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 3
        Me.mnuActualizar.ImageOptions.ImageUri.Uri = "SaveAndClose"
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 6
        Me.mnuEliminar.ImageOptions.ImageUri.Uri = "Delete"
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 10
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 11
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'mainRibbonPage
        '
        Me.mainRibbonPage.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.mainRibbonPageGroup})
        Me.mainRibbonPage.MergeOrder = 0
        Me.mainRibbonPage.Name = "mainRibbonPage"
        Me.mainRibbonPage.Text = "Menú Reglas de vencimiento"
        '
        'mainRibbonPageGroup
        '
        Me.mainRibbonPageGroup.AllowTextClipping = False
        Me.mainRibbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.mnuGuardar)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.mnuActualizar)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.mnuEliminar)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.cmdImprimir)
        Me.mainRibbonPageGroup.Name = "mainRibbonPageGroup"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 627)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.mainRibbonControl
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1303, 30)
        '
        'Dset_Regla_Vence
        '
        Me.Dset_Regla_Vence.DataSetName = "Dset_Regla_Vence"
        Me.Dset_Regla_Vence.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Regla_vencimientoTableAdapter
        '
        Me.Regla_vencimientoTableAdapter.ClearBeforeFill = True
        '
        'frmReglaVence
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(1303, 657)
        Me.Controls.Add(Me.DgridReglaVencimiento)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.mainRibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmReglaVence"
        Me.Ribbon = Me.mainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar1
        CType(Me.DgridReglaVencimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReglaVencimiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dset_Regla_Vence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private WithEvents mainRibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Private WithEvents mainRibbonPage As DevExpress.XtraBars.Ribbon.RibbonPage
    Private WithEvents mainRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Private WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Private WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DgridReglaVencimiento As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvReglaVencimiento As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Dset_Regla_Vence As Dset_Regla_Vence
    Friend WithEvents Regla_vencimientoTableAdapter As Dset_Regla_VenceTableAdapters.regla_vencimientoTableAdapter
    Friend WithEvents colIdReglaVencimiento As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombreRegla As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProductoFamilia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProductoClasificacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTiempoVencimientoDias As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTipoNotificacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPropietarioMercancia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProveedor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdCliente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActiva As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFechaCreacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUsuarioCreacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFechaModificacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUsuarioModificacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
End Class
