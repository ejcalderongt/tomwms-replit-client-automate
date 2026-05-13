<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeleccionUbicacionesInv
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSeleccionUbicacionesInv))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkUbicexist = New DevExpress.XtraBars.BarCheckItem()
        Me.mnuMarcarTodos = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpUbic = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.grpSelectAll = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Cliente_direccionTableAdapter1 = New cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter()
        Me.xtrBodegaSelUbic = New DevExpress.XtraTab.XtraTabControl()
        Me.TodasUbicaciones = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtFiltroUbic = New DevExpress.XtraEditors.TextEdit()
        Me.btnFiltLimpia = New DevExpress.XtraEditors.SimpleButton()
        Me.tlUbicacionesTodas = New DevExpress.XtraTreeList.TreeList()
        Me.pgrUbic = New DevExpress.XtraEditors.ProgressBarControl()
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.xtrBodegaSelUbic,System.ComponentModel.ISupportInitialize).BeginInit
        Me.xtrBodegaSelUbic.SuspendLayout
        Me.TodasUbicaciones.SuspendLayout
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl1.SuspendLayout
        CType(Me.txtFiltroUbic.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.tlUbicacionesTodas,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pgrUbic.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.chkUbicexist, Me.mnuMarcarTodos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1074, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'chkUbicexist
        '
        Me.chkUbicexist.BindableChecked = True
        Me.chkUbicexist.Caption = "Ubicaciones con existencias"
        Me.chkUbicexist.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.AfterText
        Me.chkUbicexist.Checked = True
        Me.chkUbicexist.Id = 3
        Me.chkUbicexist.Name = "chkUbicexist"
        '
        'mnuMarcarTodos
        '
        Me.mnuMarcarTodos.Caption = "Marcar/Desmarcar todos"
        Me.mnuMarcarTodos.Id = 4
        Me.mnuMarcarTodos.Name = "mnuMarcarTodos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.grpUbic, Me.grpSelectAll})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Selección de ubicaciones"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'grpUbic
        '
        Me.grpUbic.ItemLinks.Add(Me.chkUbicexist)
        Me.grpUbic.Name = "grpUbic"
        '
        'grpSelectAll
        '
        Me.grpSelectAll.ItemLinks.Add(Me.mnuMarcarTodos)
        Me.grpSelectAll.Name = "grpSelectAll"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 760)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1074, 30)
        '
        'Cliente_direccionTableAdapter1
        '
        Me.Cliente_direccionTableAdapter1.ClearBeforeFill = True
        '
        'xtrBodegaSelUbic
        '
        Me.xtrBodegaSelUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrBodegaSelUbic.Location = New System.Drawing.Point(0, 193)
        Me.xtrBodegaSelUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtrBodegaSelUbic.Name = "xtrBodegaSelUbic"
        Me.xtrBodegaSelUbic.SelectedTabPage = Me.TodasUbicaciones
        Me.xtrBodegaSelUbic.Size = New System.Drawing.Size(1074, 567)
        Me.xtrBodegaSelUbic.TabIndex = 1
        Me.xtrBodegaSelUbic.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TodasUbicaciones})
        '
        'TodasUbicaciones
        '
        Me.TodasUbicaciones.Controls.Add(Me.GroupControl1)
        Me.TodasUbicaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TodasUbicaciones.Name = "TodasUbicaciones"
        Me.TodasUbicaciones.Size = New System.Drawing.Size(1072, 537)
        Me.TodasUbicaciones.Text = "Todas las ubicaciones"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtFiltroUbic)
        Me.GroupControl1.Controls.Add(Me.btnFiltLimpia)
        Me.GroupControl1.Controls.Add(Me.tlUbicacionesTodas)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1072, 537)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Bodega -> Area -> Sector -> Tramo -> Ubicación"
        '
        'txtFiltroUbic
        '
        Me.txtFiltroUbic.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[True]
        Me.txtFiltroUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFiltroUbic.Location = New System.Drawing.Point(2, 28)
        Me.txtFiltroUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFiltroUbic.MenuManager = Me.RibbonControl
        Me.txtFiltroUbic.Name = "txtFiltroUbic"
        Me.txtFiltroUbic.Properties.Appearance.BackColor = System.Drawing.Color.AliceBlue
        Me.txtFiltroUbic.Properties.Appearance.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiltroUbic.Properties.Appearance.Options.UseBackColor = True
        Me.txtFiltroUbic.Properties.Appearance.Options.UseFont = True
        Me.txtFiltroUbic.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.txtFiltroUbic.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFiltroUbic.Size = New System.Drawing.Size(1039, 30)
        Me.txtFiltroUbic.TabIndex = 0
        Me.txtFiltroUbic.ToolTip = resources.GetString("txtFiltroUbic.ToolTip")
        Me.txtFiltroUbic.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.txtFiltroUbic.ToolTipTitle = "Filtro de búsqueda"
        Me.txtFiltroUbic.Visible = False
        '
        'btnFiltLimpia
        '
        Me.btnFiltLimpia.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFiltLimpia.ImageOptions.Image = CType(resources.GetObject("btnFiltLimpia.ImageOptions.Image"), System.Drawing.Image)
        Me.btnFiltLimpia.Location = New System.Drawing.Point(1041, 28)
        Me.btnFiltLimpia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnFiltLimpia.Name = "btnFiltLimpia"
        Me.btnFiltLimpia.Size = New System.Drawing.Size(29, 58)
        Me.btnFiltLimpia.TabIndex = 1
        Me.btnFiltLimpia.Visible = False
        '
        'tlUbicacionesTodas
        '
        Me.tlUbicacionesTodas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlUbicacionesTodas.Location = New System.Drawing.Point(2, 86)
        Me.tlUbicacionesTodas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tlUbicacionesTodas.MinWidth = 23
        Me.tlUbicacionesTodas.Name = "tlUbicacionesTodas"
        Me.tlUbicacionesTodas.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = True
        Me.tlUbicacionesTodas.OptionsBehavior.Editable = False
        Me.tlUbicacionesTodas.OptionsBehavior.ReadOnly = True
        Me.tlUbicacionesTodas.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.tlUbicacionesTodas.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check
        Me.tlUbicacionesTodas.OptionsView.ShowAutoFilterRow = True
        Me.tlUbicacionesTodas.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.ShowAlways
        Me.tlUbicacionesTodas.Size = New System.Drawing.Size(1068, 449)
        Me.tlUbicacionesTodas.TabIndex = 2
        Me.tlUbicacionesTodas.TreeLevelWidth = 21
        '
        'pgrUbic
        '
        Me.pgrUbic.Location = New System.Drawing.Point(615, 100)
        Me.pgrUbic.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pgrUbic.MenuManager = Me.RibbonControl
        Me.pgrUbic.Name = "pgrUbic"
        Me.pgrUbic.Properties.ShowTitle = True
        Me.pgrUbic.Size = New System.Drawing.Size(405, 20)
        Me.pgrUbic.TabIndex = 0
        Me.pgrUbic.Visible = False
        '
        'frmSeleccionUbicacionesInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 790)
        Me.Controls.Add(Me.pgrUbic)
        Me.Controls.Add(Me.xtrBodegaSelUbic)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSeleccionUbicacionesInv"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Selección de Ubicaciones"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.xtrBodegaSelUbic,System.ComponentModel.ISupportInitialize).EndInit
        Me.xtrBodegaSelUbic.ResumeLayout(false)
        Me.TodasUbicaciones.ResumeLayout(false)
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        CType(Me.txtFiltroUbic.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.tlUbicacionesTodas,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pgrUbic.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents Cliente_direccionTableAdapter1 As cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents xtrBodegaSelUbic As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents TodasUbicaciones As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtFiltroUbic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnFiltLimpia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tlUbicacionesTodas As DevExpress.XtraTreeList.TreeList
    Friend WithEvents grpUbic As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkUbicexist As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents mnuMarcarTodos As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents grpSelectAll As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents pgrUbic As DevExpress.XtraEditors.ProgressBarControl
End Class
