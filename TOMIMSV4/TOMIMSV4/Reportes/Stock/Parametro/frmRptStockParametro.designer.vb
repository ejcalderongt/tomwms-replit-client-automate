<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRptStockParametro
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptStockParametro))
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdParametros = New DevExpress.XtraGrid.GridControl()
        Me.StockBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ParametrosBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Parametros = New TOMWMS.Parametros()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colStockId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLicPlate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCant_Presentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUbicación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSerial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUM_Bas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha_Ingreso = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha_Vence = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecepción = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.btnActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.StockparametroBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.colTalla = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colColor = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StockBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ParametrosBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Parametros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StockparametroBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.grdParametros
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridView2.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        '
        'grdParametros
        '
        Me.grdParametros.DataSource = Me.StockBindingSource
        Me.grdParametros.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdParametros.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.LevelTemplate = Me.GridView2
        GridLevelNode1.RelationName = "stock_stock_parametro"
        Me.grdParametros.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdParametros.Location = New System.Drawing.Point(326, 193)
        Me.grdParametros.MainView = Me.GridView1
        Me.grdParametros.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdParametros.Name = "grdParametros"
        Me.grdParametros.Size = New System.Drawing.Size(968, 490)
        Me.grdParametros.TabIndex = 5
        Me.grdParametros.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'StockBindingSource
        '
        Me.StockBindingSource.DataMember = "Stock"
        Me.StockBindingSource.DataSource = Me.ParametrosBindingSource
        '
        'ParametrosBindingSource
        '
        Me.ParametrosBindingSource.DataSource = Me.Parametros
        Me.ParametrosBindingSource.Position = 0
        '
        'Parametros
        '
        Me.Parametros.DataSetName = "Parametros"
        Me.Parametros.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colStockId, Me.colPropietario, Me.colCódigo, Me.colProducto, Me.colLicPlate, Me.colCódigo_Barra, Me.colPresentación, Me.colCant_Presentación, Me.colEstado, Me.colUbicación, Me.colLote, Me.colSerial, Me.colCantidad, Me.colUM_Bas, Me.colFecha_Ingreso, Me.colFecha_Vence, Me.colRecepción})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdParametros
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'colStockId
        '
        Me.colStockId.FieldName = "Stock Id"
        Me.colStockId.MinWidth = 23
        Me.colStockId.Name = "colStockId"
        Me.colStockId.Visible = True
        Me.colStockId.VisibleIndex = 0
        Me.colStockId.Width = 87
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.MinWidth = 23
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 1
        Me.colPropietario.Width = 87
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.MinWidth = 23
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 2
        Me.colCódigo.Width = 87
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.MinWidth = 23
        Me.colProducto.Name = "colProducto"
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 3
        Me.colProducto.Width = 87
        '
        'colLicPlate
        '
        Me.colLicPlate.Caption = "Licencia"
        Me.colLicPlate.FieldName = "Licencia"
        Me.colLicPlate.MinWidth = 25
        Me.colLicPlate.Name = "colLicPlate"
        Me.colLicPlate.Visible = True
        Me.colLicPlate.VisibleIndex = 16
        Me.colLicPlate.Width = 94
        '
        'colCódigo_Barra
        '
        Me.colCódigo_Barra.FieldName = "Código_Barra"
        Me.colCódigo_Barra.MinWidth = 23
        Me.colCódigo_Barra.Name = "colCódigo_Barra"
        Me.colCódigo_Barra.Visible = True
        Me.colCódigo_Barra.VisibleIndex = 4
        Me.colCódigo_Barra.Width = 87
        '
        'colPresentación
        '
        Me.colPresentación.FieldName = "Presentación"
        Me.colPresentación.MinWidth = 23
        Me.colPresentación.Name = "colPresentación"
        Me.colPresentación.Visible = True
        Me.colPresentación.VisibleIndex = 5
        Me.colPresentación.Width = 87
        '
        'colCant_Presentación
        '
        Me.colCant_Presentación.FieldName = "Disponible_Presentación"
        Me.colCant_Presentación.MinWidth = 23
        Me.colCant_Presentación.Name = "colCant_Presentación"
        Me.colCant_Presentación.Visible = True
        Me.colCant_Presentación.VisibleIndex = 6
        Me.colCant_Presentación.Width = 87
        '
        'colEstado
        '
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.MinWidth = 23
        Me.colEstado.Name = "colEstado"
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 7
        Me.colEstado.Width = 87
        '
        'colUbicación
        '
        Me.colUbicación.FieldName = "Ubicación"
        Me.colUbicación.MinWidth = 23
        Me.colUbicación.Name = "colUbicación"
        Me.colUbicación.Visible = True
        Me.colUbicación.VisibleIndex = 8
        Me.colUbicación.Width = 87
        '
        'colLote
        '
        Me.colLote.FieldName = "Lote"
        Me.colLote.MinWidth = 23
        Me.colLote.Name = "colLote"
        Me.colLote.Visible = True
        Me.colLote.VisibleIndex = 9
        Me.colLote.Width = 87
        '
        'colSerial
        '
        Me.colSerial.FieldName = "Serial"
        Me.colSerial.MinWidth = 23
        Me.colSerial.Name = "colSerial"
        Me.colSerial.Visible = True
        Me.colSerial.VisibleIndex = 10
        Me.colSerial.Width = 87
        '
        'colCantidad
        '
        Me.colCantidad.FieldName = "Cantidad"
        Me.colCantidad.MinWidth = 23
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.Visible = True
        Me.colCantidad.VisibleIndex = 11
        Me.colCantidad.Width = 87
        '
        'colUM_Bas
        '
        Me.colUM_Bas.FieldName = "UM_Bas"
        Me.colUM_Bas.MinWidth = 23
        Me.colUM_Bas.Name = "colUM_Bas"
        Me.colUM_Bas.Visible = True
        Me.colUM_Bas.VisibleIndex = 12
        Me.colUM_Bas.Width = 87
        '
        'colFecha_Ingreso
        '
        Me.colFecha_Ingreso.FieldName = "Fecha_Ingreso"
        Me.colFecha_Ingreso.MinWidth = 23
        Me.colFecha_Ingreso.Name = "colFecha_Ingreso"
        Me.colFecha_Ingreso.Visible = True
        Me.colFecha_Ingreso.VisibleIndex = 13
        Me.colFecha_Ingreso.Width = 87
        '
        'colFecha_Vence
        '
        Me.colFecha_Vence.FieldName = "Fecha_Vence"
        Me.colFecha_Vence.MinWidth = 23
        Me.colFecha_Vence.Name = "colFecha_Vence"
        Me.colFecha_Vence.Visible = True
        Me.colFecha_Vence.VisibleIndex = 14
        Me.colFecha_Vence.Width = 87
        '
        'colRecepción
        '
        Me.colRecepción.FieldName = "Recepción"
        Me.colRecepción.MinWidth = 23
        Me.colRecepción.Name = "colRecepción"
        Me.colRecepción.Visible = True
        Me.colRecepción.VisibleIndex = 15
        Me.colRecepción.Width = 87
        '
        'RibbonControl1
        '
        Me.RibbonControl1.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.RibbonControl1.SearchEditItem, Me.btnImprimir, Me.btnActualizar, Me.btnSalir, Me.lblRegs, Me.mnuGuardarLayoutGrid, Me.mnuEliminarLayoutGrid})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl1.MaxItemId = 7
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.OptionsMenuMinWidth = 283
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.Size = New System.Drawing.Size(1294, 193)
        Me.RibbonControl1.StatusBar = Me.RibbonStatusBar1
        '
        'btnImprimir
        '
        Me.btnImprimir.Caption = "Imprimir"
        Me.btnImprimir.Id = 1
        Me.btnImprimir.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimir.Name = "btnImprimir"
        '
        'btnActualizar
        '
        Me.btnActualizar.Caption = "Actualizar"
        Me.btnActualizar.Id = 2
        Me.btnActualizar.ImageOptions.SvgImage = CType(resources.GetObject("btnActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnSalir
        '
        Me.btnSalir.Caption = "Salir"
        Me.btnSalir.Id = 3
        Me.btnSalir.ImageOptions.SvgImage = CType(resources.GetObject("btnSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSalir.Name = "btnSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 4
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 5
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño de grid"
        Me.mnuEliminarLayoutGrid.Id = 6
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Reporte Parametros"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 683)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1294, 30)
        '
        'StockparametroBindingSource
        '
        Me.StockparametroBindingSource.DataMember = "Stock_parametro"
        Me.StockparametroBindingSource.DataSource = Me.ParametrosBindingSource
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPropietario)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbBodega)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(326, 490)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtros"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(7, 123)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(295, 22)
        Me.cmbPropietario.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Propietario:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(7, 58)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(295, 22)
        Me.cmbBodega.TabIndex = 6
        '
        'colTalla
        '
        Me.colTalla.Caption = "Talla"
        Me.colTalla.FieldName = "Talla"
        Me.colTalla.Name = "colTalla"
        Me.colTalla.Visible = True
        Me.colTalla.VisibleIndex = 17
        Me.colTalla.Width = 64
        '
        'colColor
        '
        Me.colColor.Caption = "Color"
        Me.colColor.FieldName = "Color"
        Me.colColor.Name = "colColor"
        Me.colColor.Visible = True
        Me.colColor.VisibleIndex = 18
        Me.colColor.Width = 64
        '
        'frmRptStockParametro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1294, 713)
        Me.Controls.Add(Me.grdParametros)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmRptStockParametro"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Stock Parametros"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdParametros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StockBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ParametrosBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Parametros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StockparametroBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdParametros As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents StockBindingSource As BindingSource
    Friend WithEvents ParametrosBindingSource As BindingSource
    Friend WithEvents Parametros As Parametros
    Friend WithEvents colStockId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCant_Presentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUbicación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSerial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUM_Bas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha_Ingreso As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha_Vence As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRecepción As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StockparametroBindingSource As BindingSource
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents colLicPlate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTalla As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colColor As DevExpress.XtraGrid.Columns.GridColumn
End Class
