<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmExistenciasConReserva
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExistenciasConReserva))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.rbExistencias = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.lblProgress = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grdExistenciasStock = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsExistenciasConReservaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsExistenciasConReserva = New DsExistenciasConReserva()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colIdProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUM_Bas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidadUMBas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidadPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad_Reservada = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDisponible_UMBas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDisponiblePresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFactor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colClasificacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPropietarioBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.rbExistencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdExistenciasStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsExistenciasConReservaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsExistenciasConReserva, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbExistencias
        '
        Me.rbExistencias.ExpandCollapseItem.Id = 0
        Me.rbExistencias.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbExistencias.ExpandCollapseItem, Me.rbExistencias.SearchEditItem, Me.cmdImprimir, Me.cmdActualizar, Me.cmdSalir, Me.lblRegs, Me.lblProgress, Me.BarButtonItem1})
        Me.rbExistencias.Location = New System.Drawing.Point(0, 0)
        Me.rbExistencias.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.rbExistencias.MaxItemId = 7
        Me.rbExistencias.Name = "rbExistencias"
        Me.rbExistencias.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.rbExistencias.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbExistencias.Size = New System.Drawing.Size(1229, 193)
        Me.rbExistencias.StatusBar = Me.RibbonStatusBar
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
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'lblProgress
        '
        Me.lblProgress.Id = 5
        Me.lblProgress.ImageOptions.Image = CType(resources.GetObject("lblProgress.ImageOptions.Image"), System.Drawing.Image)
        Me.lblProgress.ImageOptions.LargeImage = CType(resources.GetObject("lblProgress.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblProgress.Name = "lblProgress"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Exportar a Excel "
        Me.BarButtonItem1.Id = 6
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Detalle de Existencias"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblProgress)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 695)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.rbExistencias
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1229, 30)
        '
        'grdExistenciasStock
        '
        Me.grdExistenciasStock.DataSource = Me.EncabezadoBindingSource
        Me.grdExistenciasStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdExistenciasStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Encabezado_Detalle"
        Me.grdExistenciasStock.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdExistenciasStock.Location = New System.Drawing.Point(369, 193)
        Me.grdExistenciasStock.MainView = Me.GridView1
        Me.grdExistenciasStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdExistenciasStock.MenuManager = Me.rbExistencias
        Me.grdExistenciasStock.Name = "grdExistenciasStock"
        Me.grdExistenciasStock.Size = New System.Drawing.Size(860, 502)
        Me.grdExistenciasStock.TabIndex = 0
        Me.grdExistenciasStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsExistenciasConReservaBindingSource
        '
        'DsExistenciasConReservaBindingSource
        '
        Me.DsExistenciasConReservaBindingSource.DataSource = Me.DsExistenciasConReserva
        Me.DsExistenciasConReservaBindingSource.Position = 0
        '
        'DsExistenciasConReserva
        '
        Me.DsExistenciasConReserva.DataSetName = "DsExistenciasConReserva"
        Me.DsExistenciasConReserva.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdProducto, Me.colPropietario, Me.colCódigo, Me.colProducto, Me.colCódigo_Barra, Me.colPresentación, Me.colUM_Bas, Me.colCantidadUMBas, Me.colCantidadPresentación, Me.colCantidad_Reservada, Me.colDisponible_UMBas, Me.colDisponiblePresentación, Me.colFactor, Me.colClasificacion, Me.colArea})
        Me.GridView1.GridControl = Me.grdExistenciasStock
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsFind.FindNullPrompt = "Ingrese texto para buscar..."
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'colIdProducto
        '
        Me.colIdProducto.FieldName = "IdProducto"
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.Visible = True
        Me.colIdProducto.VisibleIndex = 0
        Me.colIdProducto.Width = 82
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 1
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 2
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.Name = "colProducto"
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 3
        '
        'colCódigo_Barra
        '
        Me.colCódigo_Barra.FieldName = "Código_Barra"
        Me.colCódigo_Barra.Name = "colCódigo_Barra"
        Me.colCódigo_Barra.Visible = True
        Me.colCódigo_Barra.VisibleIndex = 4
        '
        'colPresentación
        '
        Me.colPresentación.FieldName = "Presentación"
        Me.colPresentación.Name = "colPresentación"
        Me.colPresentación.Visible = True
        Me.colPresentación.VisibleIndex = 5
        '
        'colUM_Bas
        '
        Me.colUM_Bas.FieldName = "UM_Bas"
        Me.colUM_Bas.Name = "colUM_Bas"
        Me.colUM_Bas.Visible = True
        Me.colUM_Bas.VisibleIndex = 6
        '
        'colCantidadUMBas
        '
        Me.colCantidadUMBas.FieldName = "CantidadUMBas"
        Me.colCantidadUMBas.Name = "colCantidadUMBas"
        Me.colCantidadUMBas.Visible = True
        Me.colCantidadUMBas.VisibleIndex = 7
        '
        'colCantidadPresentación
        '
        Me.colCantidadPresentación.FieldName = "CantidadPresentación"
        Me.colCantidadPresentación.Name = "colCantidadPresentación"
        Me.colCantidadPresentación.Visible = True
        Me.colCantidadPresentación.VisibleIndex = 8
        '
        'colCantidad_Reservada
        '
        Me.colCantidad_Reservada.FieldName = "Cantidad_Reservada"
        Me.colCantidad_Reservada.Name = "colCantidad_Reservada"
        Me.colCantidad_Reservada.Visible = True
        Me.colCantidad_Reservada.VisibleIndex = 9
        '
        'colDisponible_UMBas
        '
        Me.colDisponible_UMBas.FieldName = "Disponible_UMBas"
        Me.colDisponible_UMBas.Name = "colDisponible_UMBas"
        Me.colDisponible_UMBas.UnboundExpression = "Iif([CantidadUMBas] > 0, [CantidadUMBas] - [Cantidad_Reservada], 0)"
        Me.colDisponible_UMBas.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colDisponible_UMBas.Visible = True
        Me.colDisponible_UMBas.VisibleIndex = 10
        '
        'colDisponiblePresentación
        '
        Me.colDisponiblePresentación.DisplayFormat.FormatString = "{0:n2}"
        Me.colDisponiblePresentación.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDisponiblePresentación.FieldName = "Disponible Presentación"
        Me.colDisponiblePresentación.Name = "colDisponiblePresentación"
        Me.colDisponiblePresentación.UnboundExpression = "Iif([CantidadPresentación] > 0, [Disponible_UMBas] / [], 0)"
        Me.colDisponiblePresentación.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.colDisponiblePresentación.Visible = True
        Me.colDisponiblePresentación.VisibleIndex = 11
        '
        'colFactor
        '
        Me.colFactor.Caption = "Factor"
        Me.colFactor.Name = "colFactor"
        '
        'colClasificacion
        '
        Me.colClasificacion.Caption = "Clasificacion"
        Me.colClasificacion.FieldName = "Clasificacion"
        Me.colClasificacion.MinWidth = 25
        Me.colClasificacion.Name = "colClasificacion"
        Me.colClasificacion.Visible = True
        Me.colClasificacion.VisibleIndex = 12
        Me.colClasificacion.Width = 94
        '
        'colArea
        '
        Me.colArea.Caption = "Area"
        Me.colArea.FieldName = "Area"
        Me.colArea.MinWidth = 25
        Me.colArea.Name = "colArea"
        Me.colArea.Visible = True
        Me.colArea.VisibleIndex = 13
        Me.colArea.Width = 94
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DsExistenciasConReservaBindingSource
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.cmbPropietarioBodega)
        Me.PanelControl1.Controls.Add(Me.cmbBodega)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(369, 502)
        Me.PanelControl1.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Propietario:"
        '
        'cmbPropietarioBodega
        '
        Me.cmbPropietarioBodega.Location = New System.Drawing.Point(9, 134)
        Me.cmbPropietarioBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietarioBodega.Name = "cmbPropietarioBodega"
        Me.cmbPropietarioBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietarioBodega.Properties.NullText = ""
        Me.cmbPropietarioBodega.Size = New System.Drawing.Size(343, 22)
        Me.cmbPropietarioBodega.TabIndex = 11
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(6, 53)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(347, 22)
        Me.cmbBodega.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Bodega:"
        '
        'frmExistenciasConReserva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1229, 725)
        Me.Controls.Add(Me.grdExistenciasStock)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.rbExistencias)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmExistenciasConReserva"
        Me.Ribbon = Me.rbExistencias
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Detalle de existencias"
        CType(Me.rbExistencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdExistenciasStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsExistenciasConReservaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsExistenciasConReserva, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cmbPropietarioBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents rbExistencias As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents grdExistenciasStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsExistenciasConReservaBindingSource As BindingSource
    Friend WithEvents DsExistenciasConReserva As DsExistenciasConReserva
    Friend WithEvents colIdProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUM_Bas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadUMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad_Reservada As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDisponible_UMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDisponiblePresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetalleBindingSource As BindingSource
    Friend WithEvents lblProgress As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents colFactor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbPropietarioBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents colClasificacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArea As DevExpress.XtraGrid.Columns.GridColumn
End Class
