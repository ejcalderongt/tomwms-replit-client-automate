<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRptStock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptStock))
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdSeries = New DevExpress.XtraGrid.GridControl()
        Me.StockBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SeriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Series = New Series()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSerial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUM_Bas = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha_Ingreso = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colFecha_Vence = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colRecepción = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colStockId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCódigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCant_Presentación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUbicación = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.codigo_poliza = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.numero_orden = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GalleryDropDown1 = New DevExpress.XtraBars.Ribbon.GalleryDropDown(Me.components)
        Me.StockseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.colTalla = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colColor = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StockBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Series, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GalleryDropDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StockseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.grdSeries
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 686
        '
        'grdSeries
        '
        Me.grdSeries.DataSource = Me.StockBindingSource
        Me.grdSeries.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.RelationName = "Stock_stock_se"
        Me.grdSeries.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdSeries.Location = New System.Drawing.Point(204, 158)
        Me.grdSeries.MainView = Me.GridView1
        Me.grdSeries.Name = "grdSeries"
        Me.grdSeries.Size = New System.Drawing.Size(882, 372)
        Me.grdSeries.TabIndex = 10
        Me.grdSeries.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'StockBindingSource
        '
        Me.StockBindingSource.DataMember = "Stock"
        Me.StockBindingSource.DataSource = Me.SeriesBindingSource
        '
        'SeriesBindingSource
        '
        Me.SeriesBindingSource.DataSource = Me.Series
        Me.SeriesBindingSource.Position = 0
        '
        'Series
        '
        Me.Series.DataSetName = "Series"
        Me.Series.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPropietario, Me.colProducto, Me.colEstado, Me.colLote, Me.colSerial, Me.colCantidad, Me.colCódigo_Barra, Me.colUM_Bas, Me.colFecha_Ingreso, Me.colFecha_Vence, Me.colRecepción, Me.colStockId, Me.colCódigo, Me.colPresentación, Me.colCant_Presentación, Me.colUbicación, Me.codigo_poliza, Me.numero_orden, Me.colTalla, Me.colColor})
        Me.GridView1.GridControl = Me.grdSeries
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.ReadOnly = True
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsFind.FindNullPrompt = "Ingrese texto para buscar..."
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 1
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.Name = "colProducto"
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 2
        '
        'colEstado
        '
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.Name = "colEstado"
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 3
        '
        'colLote
        '
        Me.colLote.FieldName = "Lote"
        Me.colLote.Name = "colLote"
        Me.colLote.Visible = True
        Me.colLote.VisibleIndex = 4
        '
        'colSerial
        '
        Me.colSerial.FieldName = "Serial"
        Me.colSerial.Name = "colSerial"
        Me.colSerial.Visible = True
        Me.colSerial.VisibleIndex = 5
        '
        'colCantidad
        '
        Me.colCantidad.FieldName = "Cantidad"
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.Visible = True
        Me.colCantidad.VisibleIndex = 6
        '
        'colCódigo_Barra
        '
        Me.colCódigo_Barra.FieldName = "Código_Barra"
        Me.colCódigo_Barra.Name = "colCódigo_Barra"
        Me.colCódigo_Barra.Visible = True
        Me.colCódigo_Barra.VisibleIndex = 7
        '
        'colUM_Bas
        '
        Me.colUM_Bas.FieldName = "UM_Bas"
        Me.colUM_Bas.Name = "colUM_Bas"
        Me.colUM_Bas.Visible = True
        Me.colUM_Bas.VisibleIndex = 8
        '
        'colFecha_Ingreso
        '
        Me.colFecha_Ingreso.FieldName = "Fecha_Ingreso"
        Me.colFecha_Ingreso.Name = "colFecha_Ingreso"
        Me.colFecha_Ingreso.Visible = True
        Me.colFecha_Ingreso.VisibleIndex = 9
        '
        'colFecha_Vence
        '
        Me.colFecha_Vence.FieldName = "Fecha_Vence"
        Me.colFecha_Vence.Name = "colFecha_Vence"
        Me.colFecha_Vence.Visible = True
        Me.colFecha_Vence.VisibleIndex = 10
        '
        'colRecepción
        '
        Me.colRecepción.FieldName = "Recepción"
        Me.colRecepción.Name = "colRecepción"
        Me.colRecepción.Visible = True
        Me.colRecepción.VisibleIndex = 11
        '
        'colStockId
        '
        Me.colStockId.FieldName = "Stock Id"
        Me.colStockId.Name = "colStockId"
        Me.colStockId.Visible = True
        Me.colStockId.VisibleIndex = 0
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 12
        '
        'colPresentación
        '
        Me.colPresentación.FieldName = "Presentación"
        Me.colPresentación.Name = "colPresentación"
        Me.colPresentación.Visible = True
        Me.colPresentación.VisibleIndex = 13
        '
        'colCant_Presentación
        '
        Me.colCant_Presentación.FieldName = "Cant_Presentación"
        Me.colCant_Presentación.Name = "colCant_Presentación"
        Me.colCant_Presentación.Visible = True
        Me.colCant_Presentación.VisibleIndex = 14
        '
        'colUbicación
        '
        Me.colUbicación.FieldName = "Ubicación"
        Me.colUbicación.Name = "colUbicación"
        Me.colUbicación.Visible = True
        Me.colUbicación.VisibleIndex = 15
        '
        'codigo_poliza
        '
        Me.codigo_poliza.Caption = "codigo poliza"
        Me.codigo_poliza.FieldName = "codigo_poliza"
        Me.codigo_poliza.MinWidth = 21
        Me.codigo_poliza.Name = "codigo_poliza"
        Me.codigo_poliza.Visible = True
        Me.codigo_poliza.VisibleIndex = 16
        Me.codigo_poliza.Width = 81
        '
        'numero_orden
        '
        Me.numero_orden.Caption = "número orden"
        Me.numero_orden.FieldName = "numero_orden"
        Me.numero_orden.MinWidth = 21
        Me.numero_orden.Name = "numero_orden"
        Me.numero_orden.Visible = True
        Me.numero_orden.VisibleIndex = 17
        Me.numero_orden.Width = 81
        '
        'RibbonControl1
        '
        Me.RibbonControl1.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.btnActualizar, Me.btnImprimir, Me.btnSalir, Me.lblRegs, Me.mnuEliminarLayoutGrid, Me.mnuGuardarLayoutGrid})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 10
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.OptionsMenuMinWidth = 283
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.Size = New System.Drawing.Size(1086, 158)
        Me.RibbonControl1.StatusBar = Me.RibbonStatusBar1
        '
        'btnActualizar
        '
        Me.btnActualizar.Caption = "Actualizar"
        Me.btnActualizar.Id = 4
        Me.btnActualizar.ImageOptions.SvgImage = CType(resources.GetObject("btnActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnActualizar.Name = "btnActualizar"
        '
        'btnImprimir
        '
        Me.btnImprimir.Caption = "Imprimir"
        Me.btnImprimir.Id = 5
        Me.btnImprimir.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimir.Name = "btnImprimir"
        '
        'btnSalir
        '
        Me.btnSalir.Caption = "Salir"
        Me.btnSalir.Id = 6
        Me.btnSalir.ImageOptions.SvgImage = CType(resources.GetObject("btnSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnSalir.Name = "btnSalir"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 7
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño de grid"
        Me.mnuEliminarLayoutGrid.Id = 8
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'mnuGuardarLayoutGrid
        '
        Me.mnuGuardarLayoutGrid.Caption = "Guardar diseño de grid"
        Me.mnuGuardarLayoutGrid.Id = 9
        Me.mnuGuardarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarLayoutGrid.Name = "mnuGuardarLayoutGrid"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Reporte Series"
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
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 530)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1086, 24)
        '
        'GalleryDropDown1
        '
        Me.GalleryDropDown1.Name = "GalleryDropDown1"
        Me.GalleryDropDown1.Ribbon = Me.RibbonControl1
        '
        'StockseBindingSource
        '
        Me.StockseBindingSource.DataMember = "stock_se"
        Me.StockseBindingSource.DataSource = Me.SeriesBindingSource
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPropietario)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbBodega)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 158)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(204, 372)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtros"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(6, 100)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(192, 20)
        Me.cmbPropietario.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Propietario:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(6, 47)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(192, 20)
        Me.cmbBodega.TabIndex = 6
        '
        'colTalla
        '
        Me.colTalla.Caption = "Talla"
        Me.colTalla.FieldName = "Talla"
        Me.colTalla.Name = "colTalla"
        Me.colTalla.Visible = True
        Me.colTalla.VisibleIndex = 18
        Me.colTalla.Width = 64
        '
        'colColor
        '
        Me.colColor.Caption = "Color"
        Me.colColor.FieldName = "Color"
        Me.colColor.Name = "colColor"
        Me.colColor.Visible = True
        Me.colColor.VisibleIndex = 19
        Me.colColor.Width = 64
        '
        'frmRptStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 554)
        Me.Controls.Add(Me.grdSeries)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Name = "frmRptStock"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Stock Series"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StockBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Series, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GalleryDropDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StockseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdSeries As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GalleryDropDown1 As DevExpress.XtraBars.Ribbon.GalleryDropDown
    Friend WithEvents btnActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents StockBindingSource As BindingSource
    Friend WithEvents SeriesBindingSource As BindingSource
    Friend WithEvents Series As Series
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSerial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUM_Bas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha_Ingreso As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFecha_Vence As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRecepción As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStockId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCant_Presentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUbicación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StockseBindingSource As BindingSource
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGuardarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents codigo_poliza As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents numero_orden As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTalla As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colColor As DevExpress.XtraGrid.Columns.GridColumn
End Class
