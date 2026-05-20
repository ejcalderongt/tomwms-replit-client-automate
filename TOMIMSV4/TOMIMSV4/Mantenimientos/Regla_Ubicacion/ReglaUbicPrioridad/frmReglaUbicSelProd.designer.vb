<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReglaUbicSelProd
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReglaUbicSelProd))
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.grdvProductos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DgridProductos = New DevExpress.XtraGrid.GridControl()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.colNombre1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSeleccion1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.grdvPropietarios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DgridPropietarios = New DevExpress.XtraGrid.GridControl()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.lblRegistro = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.prg = New DevExpress.XtraEditors.ProgressBarControl()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPropietarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgridPropietarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.prg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'grdvProductos
        '
        Me.grdvProductos.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvProductos.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvProductos.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdvProductos.Appearance.SelectedRow.BackColor = System.Drawing.Color.SteelBlue
        Me.grdvProductos.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdvProductos.DetailHeight = 431
        Me.grdvProductos.GridControl = Me.DgridProductos
        Me.grdvProductos.Name = "grdvProductos"
        Me.grdvProductos.OptionsFind.AlwaysVisible = True
        Me.grdvProductos.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvProductos.OptionsView.ColumnAutoWidth = False
        Me.grdvProductos.OptionsView.ShowGroupPanel = False
        '
        'DgridProductos
        '
        Me.DgridProductos.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridProductos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridProductos.Location = New System.Drawing.Point(0, 0)
        Me.DgridProductos.MainView = Me.grdvProductos
        Me.DgridProductos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridProductos.Name = "DgridProductos"
        Me.DgridProductos.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4})
        Me.DgridProductos.Size = New System.Drawing.Size(574, 427)
        Me.DgridProductos.TabIndex = 0
        Me.DgridProductos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvProductos})
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(clsBeProducto_SelectionList)
        '
        'colNombre1
        '
        Me.colNombre1.FieldName = "Nombre"
        Me.colNombre1.Name = "colNombre1"
        '
        'colSeleccion1
        '
        Me.colSeleccion1.FieldName = "Seleccion"
        Me.colSeleccion1.Name = "colSeleccion1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'grdvPropietarios
        '
        Me.grdvPropietarios.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvPropietarios.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.grdvPropietarios.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdvPropietarios.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvPropietarios.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvPropietarios.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Red
        Me.grdvPropietarios.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grdvPropietarios.Appearance.SelectedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grdvPropietarios.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdvPropietarios.Appearance.SelectedRow.Options.UseBorderColor = True
        Me.grdvPropietarios.Appearance.SelectedRow.Options.UseFont = True
        Me.grdvPropietarios.DetailHeight = 431
        Me.grdvPropietarios.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.grdvPropietarios.GridControl = Me.DgridPropietarios
        Me.grdvPropietarios.Name = "grdvPropietarios"
        Me.grdvPropietarios.OptionsBehavior.Editable = False
        Me.grdvPropietarios.OptionsFind.AlwaysVisible = True
        Me.grdvPropietarios.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvPropietarios.OptionsSelection.EnableAppearanceHideSelection = False
        Me.grdvPropietarios.OptionsView.ShowGroupPanel = False
        '
        'DgridPropietarios
        '
        Me.DgridPropietarios.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridPropietarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridPropietarios.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPropietarios.Location = New System.Drawing.Point(0, 0)
        Me.DgridPropietarios.MainView = Me.grdvPropietarios
        Me.DgridPropietarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPropietarios.MenuManager = Me.RibbonControl
        Me.DgridPropietarios.Name = "DgridPropietarios"
        Me.DgridPropietarios.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.DgridPropietarios.Size = New System.Drawing.Size(418, 427)
        Me.DgridPropietarios.TabIndex = 0
        Me.DgridPropietarios.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPropietarios})
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.chkActivos, Me.lblRegistro, Me.cmdImprimir, Me.mnuMarcarTodos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(997, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Nuevo"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.Image = CType(resources.GetObject("mnuNuevo.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuNuevo.ImageOptions.LargeImage = CType(resources.GetObject("mnuNuevo.ImageOptions.LargeImage"), System.Drawing.Image)
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
        Me.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'lblRegistro
        '
        Me.lblRegistro.Caption = "Registros:"
        Me.lblRegistro.Id = 5
        Me.lblRegistro.Name = "lblRegistro"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 6
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'mnuMarcarTodos
        '
        Me.mnuMarcarTodos.Caption = "Marcar/Desmarcar todos"
        Me.mnuMarcarTodos.Id = 9
        Me.mnuMarcarTodos.Name = "mnuMarcarTodos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuMarcarTodos)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistro)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 620)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(997, 30)
        '
        'GridColumn3
        '
        Me.GridColumn3.FieldName = "Nombre"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn2
        '
        Me.GridColumn2.FieldName = "ID"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn1
        '
        Me.GridColumn1.FieldName = "Seleccion"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DgridPropietarios)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DgridProductos)
        Me.SplitContainer1.Size = New System.Drawing.Size(997, 427)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 1
        '
        'prg
        '
        Me.prg.Location = New System.Drawing.Point(465, 96)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.MenuManager = Me.RibbonControl
        Me.prg.Name = "prg"
        Me.prg.Properties.ShowTitle = True
        Me.prg.Size = New System.Drawing.Size(488, 22)
        Me.prg.TabIndex = 0
        Me.prg.Visible = False
        '
        'frmReglaUbicSelProd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 650)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmReglaUbicSelProd"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Regla Seleccion - Productos"
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgridProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvPropietarios,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DgridPropietarios,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        CType(Me.prg.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents grdvProductos As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents DgridProductos As DevExpress.XtraGrid.GridControl
    Friend WithEvents colNombre1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSeleccion1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents grdvPropietarios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DgridPropietarios As DevExpress.XtraGrid.GridControl
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuNuevo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents lblRegistro As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents prg As DevExpress.XtraEditors.ProgressBarControl
End Class
