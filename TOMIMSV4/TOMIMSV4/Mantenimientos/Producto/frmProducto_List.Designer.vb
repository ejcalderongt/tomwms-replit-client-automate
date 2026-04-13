<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProductoList
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pObjProducto IsNot Nothing Then
                pObjProducto.Dispose()
                pObjProducto = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductoList))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.mnuEliminarLayoutGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportarReabasto = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirCodigoBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdPlantilla = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarControlVencimiento = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarIndicesRotacion = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RbgActivo = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Grd = New DevExpress.XtraGrid.GridControl()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.mnuImagenesAWS = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.cmdImprimir, Me.cmdImportarExcel, Me.BarButtonItem1, Me.lblRegs, Me.mnuEliminarLayoutGrid, Me.cmdImportarReabasto, Me.cmdImprimirCodigoBarra, Me.cmdPlantilla, Me.mnuActualizarControlVencimiento, Me.BarButtonItem2, Me.mnuImportarIndicesRotacion, Me.mnuImagenesAWS})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 18
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1580, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.SvgImage = CType(resources.GetObject("mnuNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuNuevo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 4
        Me.chkActivos.Name = "chkActivos"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 6
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdImportarExcel
        '
        Me.cmdImportarExcel.Caption = "Importar Excel"
        Me.cmdImportarExcel.Id = 7
        Me.cmdImportarExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdImportarExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImportarExcel.Name = "cmdImportarExcel"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.BarButtonItem1.Caption = "WCF"
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 9
        Me.lblRegs.Name = "lblRegs"
        '
        'mnuEliminarLayoutGrid
        '
        Me.mnuEliminarLayoutGrid.Caption = "Eliminar diseño de grid"
        Me.mnuEliminarLayoutGrid.Id = 10
        Me.mnuEliminarLayoutGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarLayoutGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarLayoutGrid.Name = "mnuEliminarLayoutGrid"
        '
        'cmdImportarReabasto
        '
        Me.cmdImportarReabasto.Caption = "Importar Reabasto"
        Me.cmdImportarReabasto.Id = 11
        Me.cmdImportarReabasto.ImageOptions.SvgImage = CType(resources.GetObject("cmdImportarReabasto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImportarReabasto.Name = "cmdImportarReabasto"
        '
        'cmdImprimirCodigoBarra
        '
        Me.cmdImprimirCodigoBarra.Caption = "Imprimir código de barra"
        Me.cmdImprimirCodigoBarra.Id = 12
        Me.cmdImprimirCodigoBarra.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimirCodigoBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimirCodigoBarra.Name = "cmdImprimirCodigoBarra"
        '
        'cmdPlantilla
        '
        Me.cmdPlantilla.Caption = "Descargar plantilla"
        Me.cmdPlantilla.Id = 13
        Me.cmdPlantilla.ImageOptions.SvgImage = CType(resources.GetObject("cmdPlantilla.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPlantilla.Name = "cmdPlantilla"
        '
        'mnuActualizarControlVencimiento
        '
        Me.mnuActualizarControlVencimiento.Caption = "Actualizar control lote"
        Me.mnuActualizarControlVencimiento.Id = 14
        Me.mnuActualizarControlVencimiento.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarControlVencimiento.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarControlVencimiento.Name = "mnuActualizarControlVencimiento"
        Me.mnuActualizarControlVencimiento.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Plantilla indices de rotación"
        Me.BarButtonItem2.Id = 15
        Me.BarButtonItem2.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'mnuImportarIndicesRotacion
        '
        Me.mnuImportarIndicesRotacion.Caption = "Importar indices de rotación"
        Me.mnuImportarIndicesRotacion.Id = 16
        Me.mnuImportarIndicesRotacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarIndicesRotacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarIndicesRotacion.Name = "mnuImportarIndicesRotacion"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RbgActivo})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista de Producto"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImportarExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminarLayoutGrid)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImportarReabasto)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimirCodigoBarra)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdPlantilla)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizarControlVencimiento)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImportarIndicesRotacion)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImagenesAWS)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RbgActivo
        '
        Me.RbgActivo.ItemLinks.Add(Me.chkActivos)
        Me.RbgActivo.Name = "RbgActivo"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 679)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1580, 30)
        '
        'Grd
        '
        Me.Grd.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.Grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.Grd.Location = New System.Drawing.Point(0, 193)
        Me.Grd.MainView = Me.GridView
        Me.Grd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grd.MenuManager = Me.RibbonControl
        Me.Grd.Name = "Grd"
        Me.Grd.Size = New System.Drawing.Size(1580, 486)
        Me.Grd.TabIndex = 0
        Me.Grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'GridView
        '
        Me.GridView.DetailHeight = 431
        Me.GridView.GridControl = Me.Grd
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsFind.AlwaysVisible = True
        Me.GridView.OptionsView.ColumnAutoWidth = False
        Me.GridView.OptionsView.ShowAutoFilterRow = True
        '
        'mnuImagenesAWS
        '
        Me.mnuImagenesAWS.Caption = "Enviar imágenes a AWS"
        Me.mnuImagenesAWS.Id = 17
        Me.mnuImagenesAWS.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImagenesAWS.Name = "mnuImagenesAWS"
        '
        'frmProductoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1580, 709)
        Me.Controls.Add(Me.Grd)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmProductoList"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Lista de Productos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents RbgActivo As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents mnuEliminarLayoutGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportarReabasto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirCodigoBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdPlantilla As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarControlVencimiento As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImportarIndicesRotacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImagenesAWS As DevExpress.XtraBars.BarButtonItem
End Class