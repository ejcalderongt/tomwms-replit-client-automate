<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBodegaTramoList
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
        Dim lblBodega As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBodegaTramoList))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuNuevo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.BarCheckItem1 = New DevExpress.XtraBars.BarCheckItem()
        Me.chkActivos = New DevExpress.XtraBars.BarCheckItem()
        Me.BarToggleSwitchItem1 = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportaExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtNivelHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.txtNivelDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.txtColumnaHasta = New DevExpress.XtraEditors.SpinEdit()
        Me.txtColumnaDesde = New DevExpress.XtraEditors.SpinEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lblColumnaDesde = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lblColumnaHasta = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lblNivelMin = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        lblBodega = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtNivelHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNivelDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColumnaHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtColumnaDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblColumnaDesde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblColumnaHasta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNivelMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBodega
        '
        lblBodega.Dock = System.Windows.Forms.DockStyle.Top
        lblBodega.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblBodega.Location = New System.Drawing.Point(0, 193)
        lblBodega.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblBodega.Name = "lblBodega"
        lblBodega.Size = New System.Drawing.Size(1093, 30)
        lblBodega.TabIndex = 10
        lblBodega.Text = "Bodega:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuNuevo, Me.mnuActualizar, Me.mnuSalir, Me.BarCheckItem1, Me.chkActivos, Me.BarToggleSwitchItem1, Me.lblRegs, Me.cmdImprimir, Me.cmdImportaExcel})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1093, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Caption = "Seleccionar"
        Me.mnuNuevo.Id = 1
        Me.mnuNuevo.ImageOptions.SvgImage = CType(resources.GetObject("mnuNuevo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuNuevo.Name = "mnuNuevo"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ImageOptions.SvgImage = CType(resources.GetObject("mnuSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSalir.Name = "mnuSalir"
        '
        'BarCheckItem1
        '
        Me.BarCheckItem1.Caption = "BarCheckItem1"
        Me.BarCheckItem1.Id = 4
        Me.BarCheckItem1.Name = "BarCheckItem1"
        '
        'chkActivos
        '
        Me.chkActivos.BindableChecked = True
        Me.chkActivos.Caption = "Activos"
        Me.chkActivos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkActivos.Checked = True
        Me.chkActivos.Id = 5
        Me.chkActivos.Name = "chkActivos"
        '
        'BarToggleSwitchItem1
        '
        Me.BarToggleSwitchItem1.Caption = "BarToggleSwitchItem1"
        Me.BarToggleSwitchItem1.Id = 6
        Me.BarToggleSwitchItem1.Name = "BarToggleSwitchItem1"
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 7
        Me.lblRegs.Name = "lblRegs"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 8
        Me.cmdImprimir.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdImportaExcel
        '
        Me.cmdImportaExcel.Caption = "Importar Excel"
        Me.cmdImportaExcel.Id = 9
        Me.cmdImportaExcel.ImageOptions.SvgImage = CType(resources.GetObject("cmdImportaExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImportaExcel.Name = "cmdImportaExcel"
        Me.cmdImportaExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Lista de Tramos"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuNuevo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImportaExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivos)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 804)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1093, 30)
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Location = New System.Drawing.Point(0, 425)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.MenuManager = Me.RibbonControl
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1093, 379)
        Me.Dgrid.TabIndex = 0
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cmbBodega
        '
        Me.cmbBodega.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbBodega.Location = New System.Drawing.Point(0, 223)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodega.Properties.Appearance.Options.UseFont = True
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Size = New System.Drawing.Size(1093, 28)
        Me.cmbBodega.TabIndex = 11
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LayoutControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 251)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1093, 174)
        Me.GroupControl1.TabIndex = 14
        Me.GroupControl1.Text = "Parámetros"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtNivelHasta)
        Me.LayoutControl1.Controls.Add(Me.txtNivelDesde)
        Me.LayoutControl1.Controls.Add(Me.txtColumnaHasta)
        Me.LayoutControl1.Controls.Add(Me.txtColumnaDesde)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 28)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1089, 144)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtNivelHasta
        '
        Me.txtNivelHasta.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtNivelHasta.Location = New System.Drawing.Point(113, 96)
        Me.txtNivelHasta.MenuManager = Me.RibbonControl
        Me.txtNivelHasta.Name = "txtNivelHasta"
        Me.txtNivelHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNivelHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtNivelHasta.Properties.MaxValue = New Decimal(New Integer() {10, 0, 0, 0})
        Me.txtNivelHasta.Size = New System.Drawing.Size(964, 24)
        Me.txtNivelHasta.StyleController = Me.LayoutControl1
        Me.txtNivelHasta.TabIndex = 7
        '
        'txtNivelDesde
        '
        Me.txtNivelDesde.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtNivelDesde.Location = New System.Drawing.Point(113, 68)
        Me.txtNivelDesde.MenuManager = Me.RibbonControl
        Me.txtNivelDesde.Name = "txtNivelDesde"
        Me.txtNivelDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNivelDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtNivelDesde.Properties.MaxValue = New Decimal(New Integer() {10, 0, 0, 0})
        Me.txtNivelDesde.Size = New System.Drawing.Size(964, 24)
        Me.txtNivelDesde.StyleController = Me.LayoutControl1
        Me.txtNivelDesde.TabIndex = 6
        '
        'txtColumnaHasta
        '
        Me.txtColumnaHasta.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtColumnaHasta.Location = New System.Drawing.Point(113, 40)
        Me.txtColumnaHasta.MenuManager = Me.RibbonControl
        Me.txtColumnaHasta.Name = "txtColumnaHasta"
        Me.txtColumnaHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtColumnaHasta.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtColumnaHasta.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.txtColumnaHasta.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtColumnaHasta.Size = New System.Drawing.Size(964, 24)
        Me.txtColumnaHasta.StyleController = Me.LayoutControl1
        Me.txtColumnaHasta.TabIndex = 5
        '
        'txtColumnaDesde
        '
        Me.txtColumnaDesde.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtColumnaDesde.Location = New System.Drawing.Point(113, 12)
        Me.txtColumnaDesde.MenuManager = Me.RibbonControl
        Me.txtColumnaDesde.Name = "txtColumnaDesde"
        Me.txtColumnaDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtColumnaDesde.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtColumnaDesde.Properties.MaxValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtColumnaDesde.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtColumnaDesde.Size = New System.Drawing.Size(964, 24)
        Me.txtColumnaDesde.StyleController = Me.LayoutControl1
        Me.txtColumnaDesde.TabIndex = 4
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lblColumnaDesde, Me.lblColumnaHasta, Me.lblNivelMin, Me.LayoutControlItem4})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1089, 144)
        Me.Root.TextVisible = False
        '
        'lblColumnaDesde
        '
        Me.lblColumnaDesde.Control = Me.txtColumnaDesde
        Me.lblColumnaDesde.Location = New System.Drawing.Point(0, 0)
        Me.lblColumnaDesde.Name = "lblColumnaDesde"
        Me.lblColumnaDesde.Size = New System.Drawing.Size(1069, 28)
        Me.lblColumnaDesde.Text = "Columna Desde"
        Me.lblColumnaDesde.TextSize = New System.Drawing.Size(89, 16)
        '
        'lblColumnaHasta
        '
        Me.lblColumnaHasta.Control = Me.txtColumnaHasta
        Me.lblColumnaHasta.Location = New System.Drawing.Point(0, 28)
        Me.lblColumnaHasta.Name = "lblColumnaHasta"
        Me.lblColumnaHasta.Size = New System.Drawing.Size(1069, 28)
        Me.lblColumnaHasta.Text = "Columna Hasta"
        Me.lblColumnaHasta.TextSize = New System.Drawing.Size(89, 16)
        '
        'lblNivelMin
        '
        Me.lblNivelMin.Control = Me.txtNivelDesde
        Me.lblNivelMin.Location = New System.Drawing.Point(0, 56)
        Me.lblNivelMin.Name = "lblNivelMin"
        Me.lblNivelMin.Size = New System.Drawing.Size(1069, 28)
        Me.lblNivelMin.Text = "Nivel Desde"
        Me.lblNivelMin.TextSize = New System.Drawing.Size(89, 16)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtNivelHasta
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 84)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1069, 40)
        Me.LayoutControlItem4.Text = "Nivel Hasta"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(89, 16)
        '
        'frmBodegaTramoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1093, 834)
        Me.Controls.Add(Me.Dgrid)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.cmbBodega)
        Me.Controls.Add(lblBodega)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmBodegaTramoList"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Tramos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtNivelHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNivelDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColumnaHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtColumnaDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblColumnaDesde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblColumnaHasta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNivelMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents BarCheckItem1 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkActivos As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents BarToggleSwitchItem1 As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportaExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtNivelHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtNivelDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtColumnaHasta As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtColumnaDesde As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lblColumnaDesde As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lblColumnaHasta As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lblNivelMin As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
