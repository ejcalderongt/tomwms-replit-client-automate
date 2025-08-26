<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Producto_Etiquetas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Producto_Etiquetas))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemToggleSwitch1 = New DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch()
        Me.mnuMarcarTodoss = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombreMarca = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreFam = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreClas = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombrePropietario = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdMarca = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdPropietario = New DevExpress.XtraEditors.TextEdit()
        Me.lnklblMarca = New System.Windows.Forms.LinkLabel()
        Me.lnklblClas = New System.Windows.Forms.LinkLabel()
        Me.lnklblFam = New System.Windows.Forms.LinkLabel()
        Me.lnklblProp = New System.Windows.Forms.LinkLabel()
        Me.grdPrds = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCodigo_Barra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pgbPrds = New DevExpress.XtraEditors.ProgressBarControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemToggleSwitch1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtNombreMarca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreFam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreClas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombrePropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdMarca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPrds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgbPrds.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.cmdPrint, Me.cmdActualizar, Me.cmdSalir, Me.mnuMarcarTodos, Me.mnuMarcarTodoss})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemToggleSwitch1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1050, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdPrint
        '
        Me.cmdPrint.Caption = "Imprimir"
        Me.cmdPrint.Id = 1
        Me.cmdPrint.ImageOptions.SvgImage = CType(resources.GetObject("cmdPrint.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdPrint.Name = "cmdPrint"
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
        'mnuMarcarTodos
        '
        Me.mnuMarcarTodos.Caption = "Marcar/Desmarcar todos"
        Me.mnuMarcarTodos.Edit = Me.RepositoryItemToggleSwitch1
        Me.mnuMarcarTodos.Id = 4
        Me.mnuMarcarTodos.Name = "mnuMarcarTodos"
        '
        'RepositoryItemToggleSwitch1
        '
        Me.RepositoryItemToggleSwitch1.AutoHeight = False
        Me.RepositoryItemToggleSwitch1.Name = "RepositoryItemToggleSwitch1"
        Me.RepositoryItemToggleSwitch1.OffText = "Off"
        Me.RepositoryItemToggleSwitch1.OnText = "On"
        '
        'mnuMarcarTodoss
        '
        Me.mnuMarcarTodoss.Caption = "Marcar / Desmarcar Todos"
        Me.mnuMarcarTodoss.Id = 5
        Me.mnuMarcarTodoss.Name = "mnuMarcarTodoss"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Etiquetas de productos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdPrint)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuMarcarTodoss)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 929)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1050, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtNombreMarca)
        Me.GroupControl1.Controls.Add(Me.txtNombreFam)
        Me.GroupControl1.Controls.Add(Me.txtNombreClas)
        Me.GroupControl1.Controls.Add(Me.txtNombrePropietario)
        Me.GroupControl1.Controls.Add(Me.txtIdMarca)
        Me.GroupControl1.Controls.Add(Me.txtIdFamilia)
        Me.GroupControl1.Controls.Add(Me.txtIdClasificacion)
        Me.GroupControl1.Controls.Add(Me.txtIdPropietario)
        Me.GroupControl1.Controls.Add(Me.lnklblMarca)
        Me.GroupControl1.Controls.Add(Me.lnklblClas)
        Me.GroupControl1.Controls.Add(Me.lnklblFam)
        Me.GroupControl1.Controls.Add(Me.lnklblProp)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1050, 123)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Filtros"
        '
        'txtNombreMarca
        '
        Me.txtNombreMarca.Location = New System.Drawing.Point(597, 81)
        Me.txtNombreMarca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreMarca.MenuManager = Me.RibbonControl
        Me.txtNombreMarca.Name = "txtNombreMarca"
        Me.txtNombreMarca.Size = New System.Drawing.Size(219, 22)
        Me.txtNombreMarca.TabIndex = 11
        '
        'txtNombreFam
        '
        Me.txtNombreFam.Location = New System.Drawing.Point(597, 41)
        Me.txtNombreFam.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreFam.MenuManager = Me.RibbonControl
        Me.txtNombreFam.Name = "txtNombreFam"
        Me.txtNombreFam.Size = New System.Drawing.Size(219, 22)
        Me.txtNombreFam.TabIndex = 10
        '
        'txtNombreClas
        '
        Me.txtNombreClas.Location = New System.Drawing.Point(205, 81)
        Me.txtNombreClas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreClas.MenuManager = Me.RibbonControl
        Me.txtNombreClas.Name = "txtNombreClas"
        Me.txtNombreClas.Size = New System.Drawing.Size(219, 22)
        Me.txtNombreClas.TabIndex = 9
        '
        'txtNombrePropietario
        '
        Me.txtNombrePropietario.Location = New System.Drawing.Point(205, 41)
        Me.txtNombrePropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombrePropietario.MenuManager = Me.RibbonControl
        Me.txtNombrePropietario.Name = "txtNombrePropietario"
        Me.txtNombrePropietario.Size = New System.Drawing.Size(219, 22)
        Me.txtNombrePropietario.TabIndex = 8
        '
        'txtIdMarca
        '
        Me.txtIdMarca.Location = New System.Drawing.Point(510, 81)
        Me.txtIdMarca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdMarca.MenuManager = Me.RibbonControl
        Me.txtIdMarca.Name = "txtIdMarca"
        Me.txtIdMarca.Size = New System.Drawing.Size(80, 22)
        Me.txtIdMarca.TabIndex = 7
        '
        'txtIdFamilia
        '
        Me.txtIdFamilia.Location = New System.Drawing.Point(510, 41)
        Me.txtIdFamilia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdFamilia.MenuManager = Me.RibbonControl
        Me.txtIdFamilia.Name = "txtIdFamilia"
        Me.txtIdFamilia.Size = New System.Drawing.Size(80, 22)
        Me.txtIdFamilia.TabIndex = 6
        '
        'txtIdClasificacion
        '
        Me.txtIdClasificacion.Location = New System.Drawing.Point(118, 81)
        Me.txtIdClasificacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdClasificacion.MenuManager = Me.RibbonControl
        Me.txtIdClasificacion.Name = "txtIdClasificacion"
        Me.txtIdClasificacion.Size = New System.Drawing.Size(80, 22)
        Me.txtIdClasificacion.TabIndex = 5
        '
        'txtIdPropietario
        '
        Me.txtIdPropietario.Location = New System.Drawing.Point(118, 41)
        Me.txtIdPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdPropietario.MenuManager = Me.RibbonControl
        Me.txtIdPropietario.Name = "txtIdPropietario"
        Me.txtIdPropietario.Size = New System.Drawing.Size(80, 22)
        Me.txtIdPropietario.TabIndex = 4
        '
        'lnklblMarca
        '
        Me.lnklblMarca.AutoSize = True
        Me.lnklblMarca.Location = New System.Drawing.Point(456, 85)
        Me.lnklblMarca.Name = "lnklblMarca"
        Me.lnklblMarca.Size = New System.Drawing.Size(49, 17)
        Me.lnklblMarca.TabIndex = 3
        Me.lnklblMarca.TabStop = True
        Me.lnklblMarca.Text = "Marca:"
        '
        'lnklblClas
        '
        Me.lnklblClas.AutoSize = True
        Me.lnklblClas.Location = New System.Drawing.Point(30, 85)
        Me.lnklblClas.Name = "lnklblClas"
        Me.lnklblClas.Size = New System.Drawing.Size(84, 17)
        Me.lnklblClas.TabIndex = 2
        Me.lnklblClas.TabStop = True
        Me.lnklblClas.Text = "Clasificación:"
        '
        'lnklblFam
        '
        Me.lnklblFam.AutoSize = True
        Me.lnklblFam.Location = New System.Drawing.Point(453, 44)
        Me.lnklblFam.Name = "lnklblFam"
        Me.lnklblFam.Size = New System.Drawing.Size(52, 17)
        Me.lnklblFam.TabIndex = 1
        Me.lnklblFam.TabStop = True
        Me.lnklblFam.Text = "Familia:"
        '
        'lnklblProp
        '
        Me.lnklblProp.AutoSize = True
        Me.lnklblProp.Location = New System.Drawing.Point(37, 44)
        Me.lnklblProp.Name = "lnklblProp"
        Me.lnklblProp.Size = New System.Drawing.Size(78, 17)
        Me.lnklblProp.TabIndex = 0
        Me.lnklblProp.TabStop = True
        Me.lnklblProp.Text = "Propietario:"
        '
        'grdPrds
        '
        Me.grdPrds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPrds.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdPrds.Location = New System.Drawing.Point(0, 316)
        Me.grdPrds.MainView = Me.GridView1
        Me.grdPrds.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdPrds.Name = "grdPrds"
        Me.grdPrds.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.grdPrds.Size = New System.Drawing.Size(1050, 613)
        Me.grdPrds.TabIndex = 3
        Me.grdPrds.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1, Me.GridView2})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colCodigo, Me.colDescripcion, Me.colCodigo_Barra})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.grdPrds
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 23
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Seleccionar", "{0}")})
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'colCodigo
        '
        Me.colCodigo.Caption = "Código"
        Me.colCodigo.FieldName = "Codigo"
        Me.colCodigo.MinWidth = 23
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.OptionsColumn.AllowEdit = False
        Me.colCodigo.Visible = True
        Me.colCodigo.VisibleIndex = 1
        Me.colCodigo.Width = 87
        '
        'colDescripcion
        '
        Me.colDescripcion.FieldName = "Descripcion"
        Me.colDescripcion.MinWidth = 23
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.OptionsColumn.AllowEdit = False
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 2
        Me.colDescripcion.Width = 87
        '
        'colCodigo_Barra
        '
        Me.colCodigo_Barra.FieldName = "Codigo_Barra"
        Me.colCodigo_Barra.MinWidth = 23
        Me.colCodigo_Barra.Name = "colCodigo_Barra"
        Me.colCodigo_Barra.OptionsColumn.AllowEdit = False
        Me.colCodigo_Barra.Visible = True
        Me.colCodigo_Barra.VisibleIndex = 3
        Me.colCodigo_Barra.Width = 87
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 431
        Me.GridView2.GridControl = Me.grdPrds
        Me.GridView2.Name = "GridView2"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'pgbPrds
        '
        Me.pgbPrds.Location = New System.Drawing.Point(669, 98)
        Me.pgbPrds.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pgbPrds.Name = "pgbPrds"
        Me.pgbPrds.Properties.ShowTitle = True
        Me.pgbPrds.Size = New System.Drawing.Size(345, 28)
        Me.pgbPrds.TabIndex = 7
        Me.pgbPrds.Visible = False
        '
        'Producto_Etiquetas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 959)
        Me.Controls.Add(Me.pgbPrds)
        Me.Controls.Add(Me.grdPrds)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Producto_Etiquetas"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Etiquetas de productos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemToggleSwitch1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtNombreMarca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreFam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreClas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombrePropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdMarca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPrds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgbPrds.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemToggleSwitch1 As DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdPrds As DevExpress.XtraGrid.GridControl
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colDescripcion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCodigo_Barra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtIdMarca As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdPropietario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnklblMarca As LinkLabel
    Friend WithEvents lnklblClas As LinkLabel
    Friend WithEvents lnklblFam As LinkLabel
    Friend WithEvents lnklblProp As LinkLabel
    Friend WithEvents txtNombreMarca As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreFam As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreClas As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombrePropietario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents pgbPrds As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents mnuMarcarTodoss As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents colCodigo As DevExpress.XtraGrid.Columns.GridColumn
End Class
