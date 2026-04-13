<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAjusteStock_List
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAjusteStock_List))
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.lbldesde = New System.Windows.Forms.Label()
        Me.dtpFechaDel = New System.Windows.Forms.DateTimePicker()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImportarExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.dtpFechaAl = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Dgrid,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(191, 31)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(18, 16)
        Me.lblHasta.TabIndex = 2
        Me.lblHasta.Text = "Al"
        '
        'lbldesde
        '
        Me.lbldesde.AutoSize = True
        Me.lbldesde.Location = New System.Drawing.Point(14, 31)
        Me.lbldesde.Name = "lbldesde"
        Me.lbldesde.Size = New System.Drawing.Size(25, 16)
        Me.lbldesde.TabIndex = 0
        Me.lbldesde.Text = "Del"
        '
        'dtpFechaDel
        '
        Me.dtpFechaDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDel.Location = New System.Drawing.Point(47, 27)
        Me.dtpFechaDel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaDel.Name = "dtpFechaDel"
        Me.dtpFechaDel.Size = New System.Drawing.Size(123, 23)
        Me.dtpFechaDel.TabIndex = 1
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.Dgrid.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.Dgrid.Location = New System.Drawing.Point(0, 261)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.MenuManager = Me.RibbonControl
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1055, 444)
        Me.Dgrid.TabIndex = 18
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.lblRegs, Me.cmdImportarExcel, Me.BarButtonItem1, Me.cmdImprimir})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1055, 193)
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
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdImportarExcel
        '
        Me.cmdImportarExcel.Caption = "Importar Excel"
        Me.cmdImportarExcel.Id = 7
        Me.cmdImportarExcel.ImageOptions.LargeImage = Global.TOMWMS.My.Resources.Resources.excel_icon
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
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 9
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista Ajuste Stock"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 705)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1055, 30)
        '
        'dtpFechaAl
        '
        Me.dtpFechaAl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaAl.Location = New System.Drawing.Point(217, 27)
        Me.dtpFechaAl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFechaAl.Name = "dtpFechaAl"
        Me.dtpFechaAl.Size = New System.Drawing.Size(123, 23)
        Me.dtpFechaAl.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblHasta)
        Me.GroupBox1.Controls.Add(Me.lbldesde)
        Me.GroupBox1.Controls.Add(Me.dtpFechaAl)
        Me.GroupBox1.Controls.Add(Me.dtpFechaDel)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(1055, 68)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        Me.GroupBox1.Text = "Filtro por Fecha"
        '
        'frmAjusteStock_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1055, 735)
        Me.Controls.Add(Me.Dgrid)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmAjusteStock_List"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Listado de ajustes de stock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Dgrid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblHasta As Label
    Friend WithEvents lbldesde As Label
    Friend WithEvents dtpFechaDel As DateTimePicker
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImportarExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents dtpFechaAl As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
End Class
