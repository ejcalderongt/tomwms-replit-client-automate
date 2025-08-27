<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmResumenStock
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResumenStock))
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdResumenStock = New DevExpress.XtraGrid.GridControl()
        Me.EncabezadoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResumenStockBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResumenStock = New TOMWMS.DsResumenStock()
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
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.btnActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DetalleBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdResumenStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResumenStockBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResumenStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.grdResumenStock
        Me.GridView2.Name = "GridView2"
        '
        'grdResumenStock
        '
        Me.grdResumenStock.DataSource = Me.EncabezadoBindingSource
        Me.grdResumenStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdResumenStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.LevelTemplate = Me.GridView2
        GridLevelNode1.RelationName = "detalle_resumen"
        Me.grdResumenStock.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdResumenStock.Location = New System.Drawing.Point(353, 193)
        Me.grdResumenStock.MainView = Me.GridView1
        Me.grdResumenStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdResumenStock.MenuManager = Me.RibbonControl1
        Me.grdResumenStock.Name = "grdResumenStock"
        Me.grdResumenStock.Size = New System.Drawing.Size(935, 567)
        Me.grdResumenStock.TabIndex = 1
        Me.grdResumenStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'EncabezadoBindingSource
        '
        Me.EncabezadoBindingSource.DataMember = "Encabezado"
        Me.EncabezadoBindingSource.DataSource = Me.DsResumenStockBindingSource
        '
        'DsResumenStockBindingSource
        '
        Me.DsResumenStockBindingSource.DataSource = Me.DsResumenStock
        Me.DsResumenStockBindingSource.Position = 0
        '
        'DsResumenStock
        '
        Me.DsResumenStock.DataSetName = "DsResumenStock"
        Me.DsResumenStock.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colIdProducto, Me.colPropietario, Me.colCódigo, Me.colProducto, Me.colCódigo_Barra, Me.colPresentación, Me.colUM_Bas, Me.colCantidadUMBas, Me.colCantidadPresentación})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdResumenStock
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsFind.FindNullPrompt = "Ingrese texto para buscar..."
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'colIdProducto
        '
        Me.colIdProducto.FieldName = "IdProducto"
        Me.colIdProducto.MinWidth = 23
        Me.colIdProducto.Name = "colIdProducto"
        Me.colIdProducto.OptionsColumn.ReadOnly = True
        Me.colIdProducto.Width = 87
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.MinWidth = 23
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.OptionsColumn.ReadOnly = True
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 0
        Me.colPropietario.Width = 87
        '
        'colCódigo
        '
        Me.colCódigo.FieldName = "Código"
        Me.colCódigo.MinWidth = 23
        Me.colCódigo.Name = "colCódigo"
        Me.colCódigo.OptionsColumn.ReadOnly = True
        Me.colCódigo.Visible = True
        Me.colCódigo.VisibleIndex = 1
        Me.colCódigo.Width = 87
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.MinWidth = 23
        Me.colProducto.Name = "colProducto"
        Me.colProducto.OptionsColumn.ReadOnly = True
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 2
        Me.colProducto.Width = 87
        '
        'colCódigo_Barra
        '
        Me.colCódigo_Barra.FieldName = "Código_Barra"
        Me.colCódigo_Barra.MinWidth = 23
        Me.colCódigo_Barra.Name = "colCódigo_Barra"
        Me.colCódigo_Barra.OptionsColumn.ReadOnly = True
        Me.colCódigo_Barra.Visible = True
        Me.colCódigo_Barra.VisibleIndex = 3
        Me.colCódigo_Barra.Width = 87
        '
        'colPresentación
        '
        Me.colPresentación.FieldName = "Presentación"
        Me.colPresentación.MinWidth = 23
        Me.colPresentación.Name = "colPresentación"
        Me.colPresentación.OptionsColumn.ReadOnly = True
        Me.colPresentación.Visible = True
        Me.colPresentación.VisibleIndex = 4
        Me.colPresentación.Width = 87
        '
        'colUM_Bas
        '
        Me.colUM_Bas.FieldName = "UM_Bas"
        Me.colUM_Bas.MinWidth = 23
        Me.colUM_Bas.Name = "colUM_Bas"
        Me.colUM_Bas.OptionsColumn.ReadOnly = True
        Me.colUM_Bas.Visible = True
        Me.colUM_Bas.VisibleIndex = 5
        Me.colUM_Bas.Width = 87
        '
        'colCantidadUMBas
        '
        Me.colCantidadUMBas.FieldName = "CantidadUMBas"
        Me.colCantidadUMBas.MinWidth = 23
        Me.colCantidadUMBas.Name = "colCantidadUMBas"
        Me.colCantidadUMBas.OptionsColumn.ReadOnly = True
        Me.colCantidadUMBas.Visible = True
        Me.colCantidadUMBas.VisibleIndex = 6
        Me.colCantidadUMBas.Width = 87
        '
        'colCantidadPresentación
        '
        Me.colCantidadPresentación.FieldName = "CantidadPresentación"
        Me.colCantidadPresentación.MinWidth = 23
        Me.colCantidadPresentación.Name = "colCantidadPresentación"
        Me.colCantidadPresentación.OptionsColumn.ReadOnly = True
        Me.colCantidadPresentación.Visible = True
        Me.colCantidadPresentación.VisibleIndex = 7
        Me.colCantidadPresentación.Width = 87
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.RibbonControl1.SearchEditItem, Me.btnImprimir, Me.btnActualizar, Me.btnSalir, Me.lblRegs})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl1.MaxItemId = 5
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl1.Size = New System.Drawing.Size(1288, 193)
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
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Resumen de Stock"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 760)
        Me.RibbonStatusBar1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1288, 30)
        '
        'DetalleBindingSource
        '
        Me.DetalleBindingSource.DataMember = "Detalle"
        Me.DetalleBindingSource.DataSource = Me.DsResumenStockBindingSource
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
        Me.GroupBox1.Size = New System.Drawing.Size(353, 567)
        Me.GroupBox1.TabIndex = 0
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
        Me.cmbPropietario.Size = New System.Drawing.Size(325, 22)
        Me.cmbPropietario.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Propietario:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bodega:"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(7, 58)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(325, 22)
        Me.cmbBodega.TabIndex = 1
        '
        'frmResumenStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1288, 790)
        Me.Controls.Add(Me.grdResumenStock)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmResumenStock"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Resumen de Stock"
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdResumenStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncabezadoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResumenStockBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResumenStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetalleBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents grdResumenStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colIdProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCódigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUM_Bas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadUMBas As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidadPresentación As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents EncabezadoBindingSource As BindingSource
    Friend WithEvents DsResumenStockBindingSource As BindingSource
    Friend WithEvents DsResumenStock As DsResumenStock
    Friend WithEvents DetalleBindingSource As BindingSource
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
End Class
