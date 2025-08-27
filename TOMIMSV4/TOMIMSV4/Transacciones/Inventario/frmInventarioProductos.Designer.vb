<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInventarioProductos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioProductos))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdAgregar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarStaticItem()
        Me.lblRegistrosSeleccionados = New DevExpress.XtraBars.BarStaticItem()
        Me.twExistencias = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RepositoryItemToggleSwitch1 = New DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpProductos = New DevExpress.XtraEditors.GroupControl()
        Me.pgbProductos = New DevExpress.XtraEditors.ProgressBarControl()
        Me.btsMarcarRegistros = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.gvProductos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemToggleSwitch1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProductos.SuspendLayout()
        CType(Me.pgbProductos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.cmdAgregar, Me.mnuMarcarTodos, Me.lblRegistros, Me.lblRegistrosSeleccionados, Me.twExistencias})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 9
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemToggleSwitch1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1114, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Caption = "Agregar"
        Me.cmdAgregar.Id = 1
        Me.cmdAgregar.ImageOptions.SvgImage = CType(resources.GetObject("cmdAgregar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAgregar.Name = "cmdAgregar"
        '
        'mnuMarcarTodos
        '
        Me.mnuMarcarTodos.Caption = "Marcar/Desmarcar todos"
        Me.mnuMarcarTodos.Id = 5
        Me.mnuMarcarTodos.Name = "mnuMarcarTodos"
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 6
        Me.lblRegistros.Name = "lblRegistros"
        '
        'lblRegistrosSeleccionados
        '
        Me.lblRegistrosSeleccionados.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.lblRegistrosSeleccionados.Caption = "Reg. Seleccionados: 0"
        Me.lblRegistrosSeleccionados.Id = 7
        Me.lblRegistrosSeleccionados.Name = "lblRegistrosSeleccionados"
        '
        'twExistencias
        '
        Me.twExistencias.BindableChecked = True
        Me.twExistencias.Caption = "Con existencias"
        Me.twExistencias.Checked = True
        Me.twExistencias.Id = 8
        Me.twExistencias.Name = "twExistencias"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Listado de Productos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdAgregar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuMarcarTodos)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.twExistencias)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RepositoryItemToggleSwitch1
        '
        Me.RepositoryItemToggleSwitch1.AutoHeight = False
        Me.RepositoryItemToggleSwitch1.Name = "RepositoryItemToggleSwitch1"
        Me.RepositoryItemToggleSwitch1.OffText = "Off"
        Me.RepositoryItemToggleSwitch1.OnText = "On"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistrosSeleccionados)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 665)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1114, 30)
        '
        'grpProductos
        '
        Me.grpProductos.Controls.Add(Me.pgbProductos)
        Me.grpProductos.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpProductos.Location = New System.Drawing.Point(0, 193)
        Me.grpProductos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grpProductos.Name = "grpProductos"
        Me.grpProductos.Size = New System.Drawing.Size(1114, 53)
        Me.grpProductos.TabIndex = 0
        '
        'pgbProductos
        '
        Me.pgbProductos.Dock = System.Windows.Forms.DockStyle.Top
        Me.pgbProductos.Location = New System.Drawing.Point(2, 28)
        Me.pgbProductos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pgbProductos.MenuManager = Me.RibbonControl
        Me.pgbProductos.Name = "pgbProductos"
        Me.pgbProductos.Properties.ShowTitle = True
        Me.pgbProductos.Size = New System.Drawing.Size(1110, 22)
        Me.pgbProductos.TabIndex = 0
        Me.pgbProductos.Visible = False
        '
        'BarToggleSwitchItem1
        '
        Me.btsMarcarRegistros.Caption = "Marcar/Desmarcar todos"
        Me.btsMarcarRegistros.Id = 5
        Me.btsMarcarRegistros.Name = "BarToggleSwitchItem1"
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Location = New System.Drawing.Point(0, 246)
        Me.Dgrid.MainView = Me.gvProductos
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit1})
        Me.Dgrid.Size = New System.Drawing.Size(1114, 419)
        Me.Dgrid.TabIndex = 1
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvProductos})
        '
        'gvProductos
        '
        Me.gvProductos.CustomizationFormBounds = New System.Drawing.Rectangle(590, 414, 252, 234)
        Me.gvProductos.DetailHeight = 431
        Me.gvProductos.GridControl = Me.Dgrid
        Me.gvProductos.Name = "gvProductos"
        Me.gvProductos.OptionsFind.AlwaysVisible = True
        Me.gvProductos.OptionsView.ColumnAutoWidth = False
        Me.gvProductos.OptionsView.ShowAutoFilterRow = True
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'frmInventarioProductos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1114, 695)
        Me.Controls.Add(Me.Dgrid)
        Me.Controls.Add(Me.grpProductos)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmInventarioProductos"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Listado de Productos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemToggleSwitch1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProductos.ResumeLayout(False)
        CType(Me.pgbProductos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProductos,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpProductos As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdAgregar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemToggleSwitch1 As DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgbProductos As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents btsMarcarRegistros As DevExpress.XtraBars.BarToggleSwitchItem
    Private WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Private WithEvents gvProductos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents lblRegistrosSeleccionados As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents twExistencias As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
