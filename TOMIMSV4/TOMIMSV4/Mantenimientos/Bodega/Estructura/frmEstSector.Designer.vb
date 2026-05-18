<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEstSector
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEstSector))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGuardarDatosGrid = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.xtabConfig = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdIzq = New System.Windows.Forms.DataGridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnInsIzq = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPegarIzq = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBorIzq = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLimpiarIzq = New DevExpress.XtraEditors.SimpleButton()
        Me.grdDer = New System.Windows.Forms.DataGridView()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.btnInsDer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLimpiarDer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPegarDer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBorDer = New DevExpress.XtraEditors.SimpleButton()
        Me.xtabRawDATA = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridTramo = New DevExpress.XtraGrid.GridControl()
        Me.DetTramoByBodega1 = New DetTramoByBodega()
        Me.gvTramo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.coldescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colalto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.collargo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colancho = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colpos_x = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colpos_y = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colhorizontal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdSector = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.Bodega_sectorTableAdapter1 = New DetTramoByBodegaTableAdapters.bodega_sectorTableAdapter()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chkHorizontal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chkHorizontalR = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.xtabConfig.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdIzq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdDer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.xtabRawDATA.SuspendLayout()
        CType(Me.dgridTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetTramoByBodega1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTramo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(30, 44)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(101, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(445, 18)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(117, 17)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(30, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(111, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(445, 44)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(107, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(811, 76)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(118, 41)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuActualizar, Me.mnuAsignacion, Me.mnuGuardarDatosGrid})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1940, 193)
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Guardar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        Me.mnuAsignacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuGuardarDatosGrid
        '
        Me.mnuGuardarDatosGrid.Caption = "Guardar datos grid"
        Me.mnuGuardarDatosGrid.Id = 6
        Me.mnuGuardarDatosGrid.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardarDatosGrid.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardarDatosGrid.Name = "mnuGuardarDatosGrid"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardarDatosGrid)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(118, 15)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(533, 15)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(224, 20)
        Me.User_modTextEdit.TabIndex = 2
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(533, 41)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(224, 20)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 427)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(769, 19)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("ca6eb4a3-b2c9-4288-83b5-71379d0e78fa")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 104)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(817, 104)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1940, 805)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Estructura de tramos"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(2, 28)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.xtabConfig
        Me.XtraTabControl1.Size = New System.Drawing.Size(1936, 775)
        Me.XtraTabControl1.TabIndex = 11
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtabConfig, Me.xtabRawDATA})
        '
        'xtabConfig
        '
        Me.xtabConfig.Appearance.Header.Options.UseBackColor = True
        Me.xtabConfig.Appearance.HeaderHotTracked.Options.UseBackColor = True
        Me.xtabConfig.Appearance.PageClient.BackColor = System.Drawing.Color.LightCoral
        Me.xtabConfig.Appearance.PageClient.Options.UseBackColor = True
        Me.xtabConfig.Controls.Add(Me.SplitContainer1)
        Me.xtabConfig.Name = "xtabConfig"
        Me.xtabConfig.Size = New System.Drawing.Size(1934, 745)
        Me.xtabConfig.Text = "Configuración"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdIzq)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PanelControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdDer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelControl2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1934, 745)
        Me.SplitContainer1.SplitterDistance = 772
        Me.SplitContainer1.TabIndex = 10
        '
        'grdIzq
        '
        Me.grdIzq.AllowUserToResizeColumns = False
        Me.grdIzq.AllowUserToResizeRows = False
        Me.grdIzq.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdIzq.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdIzq.ColumnHeadersHeight = 44
        Me.grdIzq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdIzq.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column6, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column7, Me.chkHorizontal, Me.Column8})
        Me.grdIzq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdIzq.EnableHeadersVisualStyles = False
        Me.grdIzq.Location = New System.Drawing.Point(0, 150)
        Me.grdIzq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdIzq.MultiSelect = False
        Me.grdIzq.Name = "grdIzq"
        Me.grdIzq.RowHeadersVisible = False
        Me.grdIzq.RowHeadersWidth = 51
        Me.grdIzq.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdIzq.ShowCellErrors = False
        Me.grdIzq.ShowCellToolTips = False
        Me.grdIzq.ShowRowErrors = False
        Me.grdIzq.Size = New System.Drawing.Size(772, 595)
        Me.grdIzq.TabIndex = 8
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnInsIzq)
        Me.PanelControl1.Controls.Add(Me.btnPegarIzq)
        Me.PanelControl1.Controls.Add(Me.btnBorIzq)
        Me.PanelControl1.Controls.Add(Me.btnLimpiarIzq)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(772, 150)
        Me.PanelControl1.TabIndex = 9
        '
        'btnInsIzq
        '
        Me.btnInsIzq.ImageOptions.Image = CType(resources.GetObject("btnInsIzq.ImageOptions.Image"), System.Drawing.Image)
        Me.btnInsIzq.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnInsIzq.Location = New System.Drawing.Point(65, 16)
        Me.btnInsIzq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnInsIzq.Name = "btnInsIzq"
        Me.btnInsIzq.Size = New System.Drawing.Size(126, 50)
        Me.btnInsIzq.TabIndex = 0
        Me.btnInsIzq.Text = "Insertar"
        '
        'btnPegarIzq
        '
        Me.btnPegarIzq.ImageOptions.Image = CType(resources.GetObject("btnPegarIzq.ImageOptions.Image"), System.Drawing.Image)
        Me.btnPegarIzq.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnPegarIzq.Location = New System.Drawing.Point(484, 16)
        Me.btnPegarIzq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPegarIzq.Name = "btnPegarIzq"
        Me.btnPegarIzq.Size = New System.Drawing.Size(132, 50)
        Me.btnPegarIzq.TabIndex = 3
        Me.btnPegarIzq.Text = "Pegar"
        Me.btnPegarIzq.Visible = False
        '
        'btnBorIzq
        '
        Me.btnBorIzq.ImageOptions.Image = CType(resources.GetObject("btnBorIzq.ImageOptions.Image"), System.Drawing.Image)
        Me.btnBorIzq.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnBorIzq.Location = New System.Drawing.Point(200, 16)
        Me.btnBorIzq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBorIzq.Name = "btnBorIzq"
        Me.btnBorIzq.Size = New System.Drawing.Size(128, 50)
        Me.btnBorIzq.TabIndex = 1
        Me.btnBorIzq.Text = "Eliminar"
        '
        'btnLimpiarIzq
        '
        Me.btnLimpiarIzq.ImageOptions.Image = CType(resources.GetObject("btnLimpiarIzq.ImageOptions.Image"), System.Drawing.Image)
        Me.btnLimpiarIzq.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnLimpiarIzq.Location = New System.Drawing.Point(337, 16)
        Me.btnLimpiarIzq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnLimpiarIzq.Name = "btnLimpiarIzq"
        Me.btnLimpiarIzq.Size = New System.Drawing.Size(138, 50)
        Me.btnLimpiarIzq.TabIndex = 2
        Me.btnLimpiarIzq.Text = "Eliminar todo"
        '
        'grdDer
        '
        Me.grdDer.AllowUserToResizeColumns = False
        Me.grdDer.AllowUserToResizeRows = False
        Me.grdDer.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 7.8!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdDer.ColumnHeadersHeight = 44
        Me.grdDer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdDer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewCheckBoxColumn1, Me.chkHorizontalR, Me.Column9})
        Me.grdDer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDer.EnableHeadersVisualStyles = False
        Me.grdDer.Location = New System.Drawing.Point(0, 150)
        Me.grdDer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDer.MultiSelect = False
        Me.grdDer.Name = "grdDer"
        Me.grdDer.RowHeadersVisible = False
        Me.grdDer.RowHeadersWidth = 51
        Me.grdDer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdDer.ShowCellErrors = False
        Me.grdDer.ShowCellToolTips = False
        Me.grdDer.ShowRowErrors = False
        Me.grdDer.Size = New System.Drawing.Size(1158, 595)
        Me.grdDer.TabIndex = 9
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.btnInsDer)
        Me.PanelControl2.Controls.Add(Me.btnLimpiarDer)
        Me.PanelControl2.Controls.Add(Me.btnPegarDer)
        Me.PanelControl2.Controls.Add(Me.btnBorDer)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1158, 150)
        Me.PanelControl2.TabIndex = 10
        '
        'btnInsDer
        '
        Me.btnInsDer.ImageOptions.Image = CType(resources.GetObject("btnInsDer.ImageOptions.Image"), System.Drawing.Image)
        Me.btnInsDer.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnInsDer.Location = New System.Drawing.Point(52, 16)
        Me.btnInsDer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnInsDer.Name = "btnInsDer"
        Me.btnInsDer.Size = New System.Drawing.Size(126, 50)
        Me.btnInsDer.TabIndex = 4
        Me.btnInsDer.Text = "Insertar"
        '
        'btnLimpiarDer
        '
        Me.btnLimpiarDer.ImageOptions.Image = CType(resources.GetObject("btnLimpiarDer.ImageOptions.Image"), System.Drawing.Image)
        Me.btnLimpiarDer.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnLimpiarDer.Location = New System.Drawing.Point(324, 16)
        Me.btnLimpiarDer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnLimpiarDer.Name = "btnLimpiarDer"
        Me.btnLimpiarDer.Size = New System.Drawing.Size(148, 50)
        Me.btnLimpiarDer.TabIndex = 6
        Me.btnLimpiarDer.Text = "Eliminar todo"
        '
        'btnPegarDer
        '
        Me.btnPegarDer.ImageOptions.Image = CType(resources.GetObject("btnPegarDer.ImageOptions.Image"), System.Drawing.Image)
        Me.btnPegarDer.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnPegarDer.Location = New System.Drawing.Point(481, 16)
        Me.btnPegarDer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPegarDer.Name = "btnPegarDer"
        Me.btnPegarDer.Size = New System.Drawing.Size(134, 50)
        Me.btnPegarDer.TabIndex = 7
        Me.btnPegarDer.Text = "Pegar"
        Me.btnPegarDer.Visible = False
        '
        'btnBorDer
        '
        Me.btnBorDer.ImageOptions.Image = CType(resources.GetObject("btnBorDer.ImageOptions.Image"), System.Drawing.Image)
        Me.btnBorDer.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnBorDer.Location = New System.Drawing.Point(187, 16)
        Me.btnBorDer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBorDer.Name = "btnBorDer"
        Me.btnBorDer.Size = New System.Drawing.Size(128, 50)
        Me.btnBorDer.TabIndex = 5
        Me.btnBorDer.Text = "Eliminar"
        '
        'xtabRawDATA
        '
        Me.xtabRawDATA.Controls.Add(Me.dgridTramo)
        Me.xtabRawDATA.Name = "xtabRawDATA"
        Me.xtabRawDATA.Size = New System.Drawing.Size(1934, 745)
        Me.xtabRawDATA.Text = "Data"
        '
        'dgridTramo
        '
        Me.dgridTramo.DataMember = "bodega_sector"
        Me.dgridTramo.DataSource = Me.DetTramoByBodega1
        Me.dgridTramo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridTramo.Location = New System.Drawing.Point(0, 0)
        Me.dgridTramo.MainView = Me.gvTramo
        Me.dgridTramo.MenuManager = Me.RibbonControl
        Me.dgridTramo.Name = "dgridTramo"
        Me.dgridTramo.Size = New System.Drawing.Size(1934, 745)
        Me.dgridTramo.TabIndex = 0
        Me.dgridTramo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTramo})
        '
        'DetTramoByBodega1
        '
        Me.DetTramoByBodega1.DataSetName = "DetTramoByBodega"
        Me.DetTramoByBodega1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gvTramo
        '
        Me.gvTramo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCodigo, Me.coldescripcion, Me.colalto, Me.collargo, Me.colancho, Me.colpos_x, Me.colpos_y, Me.colhorizontal, Me.colIdBodega, Me.colIdSector})
        Me.gvTramo.GridControl = Me.dgridTramo
        Me.gvTramo.Name = "gvTramo"
        Me.gvTramo.OptionsView.ShowFooter = True
        '
        'colCodigo
        '
        Me.colCodigo.FieldName = "Codigo"
        Me.colCodigo.MinWidth = 25
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.OptionsColumn.AllowEdit = False
        Me.colCodigo.Visible = True
        Me.colCodigo.VisibleIndex = 0
        Me.colCodigo.Width = 94
        '
        'coldescripcion
        '
        Me.coldescripcion.FieldName = "descripcion"
        Me.coldescripcion.MinWidth = 25
        Me.coldescripcion.Name = "coldescripcion"
        Me.coldescripcion.Visible = True
        Me.coldescripcion.VisibleIndex = 1
        Me.coldescripcion.Width = 94
        '
        'colalto
        '
        Me.colalto.FieldName = "alto"
        Me.colalto.MinWidth = 25
        Me.colalto.Name = "colalto"
        Me.colalto.Visible = True
        Me.colalto.VisibleIndex = 2
        Me.colalto.Width = 94
        '
        'collargo
        '
        Me.collargo.FieldName = "largo"
        Me.collargo.MinWidth = 25
        Me.collargo.Name = "collargo"
        Me.collargo.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "largo", "SUM={0:0.##}")})
        Me.collargo.Visible = True
        Me.collargo.VisibleIndex = 3
        Me.collargo.Width = 94
        '
        'colancho
        '
        Me.colancho.FieldName = "ancho"
        Me.colancho.MinWidth = 25
        Me.colancho.Name = "colancho"
        Me.colancho.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ancho", "SUM={0:0.##}")})
        Me.colancho.Visible = True
        Me.colancho.VisibleIndex = 4
        Me.colancho.Width = 94
        '
        'colpos_x
        '
        Me.colpos_x.FieldName = "pos_x"
        Me.colpos_x.MinWidth = 25
        Me.colpos_x.Name = "colpos_x"
        Me.colpos_x.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "pos_x", "SUM={0:0.##}")})
        Me.colpos_x.Visible = True
        Me.colpos_x.VisibleIndex = 5
        Me.colpos_x.Width = 94
        '
        'colpos_y
        '
        Me.colpos_y.FieldName = "pos_y"
        Me.colpos_y.MinWidth = 25
        Me.colpos_y.Name = "colpos_y"
        Me.colpos_y.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "pos_y", "SUM={0:0.##}")})
        Me.colpos_y.Visible = True
        Me.colpos_y.VisibleIndex = 6
        Me.colpos_y.Width = 94
        '
        'colhorizontal
        '
        Me.colhorizontal.FieldName = "horizontal"
        Me.colhorizontal.MinWidth = 25
        Me.colhorizontal.Name = "colhorizontal"
        Me.colhorizontal.Visible = True
        Me.colhorizontal.VisibleIndex = 7
        Me.colhorizontal.Width = 94
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 25
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.AllowEdit = False
        Me.colIdBodega.Visible = True
        Me.colIdBodega.VisibleIndex = 8
        Me.colIdBodega.Width = 94
        '
        'colIdSector
        '
        Me.colIdSector.FieldName = "IdSector"
        Me.colIdSector.MinWidth = 25
        Me.colIdSector.Name = "colIdSector"
        Me.colIdSector.OptionsColumn.AllowEdit = False
        Me.colIdSector.Visible = True
        Me.colIdSector.VisibleIndex = 9
        Me.colIdSector.Width = 94
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        '
        'Bodega_sectorTableAdapter1
        '
        Me.Bodega_sectorTableAdapter1.ClearBeforeFill = True
        '
        'Column6
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column6.HeaderText = "Codigo"
        Me.Column6.MinimumWidth = 6
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column6.Width = 80
        '
        'Column1
        '
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Nombre R00-AB"
        Me.Column1.MinimumWidth = 6
        Me.Column1.Name = "Column1"
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 80
        '
        'Column2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column2.HeaderText = "Largo"
        Me.Column2.MinimumWidth = 6
        Me.Column2.Name = "Column2"
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 80
        '
        'Column3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column3.HeaderText = "Profunidad Ubic."
        Me.Column3.MinimumWidth = 6
        Me.Column3.Name = "Column3"
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 80
        '
        'Column4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column4.HeaderText = "Margen Izq"
        Me.Column4.MinimumWidth = 6
        Me.Column4.Name = "Column4"
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 80
        '
        'Column5
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column5.HeaderText = "Margen Frontal"
        Me.Column5.MinimumWidth = 6
        Me.Column5.Name = "Column5"
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Width = 80
        '
        'Column7
        '
        Me.Column7.HeaderText = "Ubic. de Piso"
        Me.Column7.MinimumWidth = 6
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 80
        '
        'chkHorizontal
        '
        Me.chkHorizontal.HeaderText = "Horizontal"
        Me.chkHorizontal.MinimumWidth = 6
        Me.chkHorizontal.Name = "chkHorizontal"
        Me.chkHorizontal.Width = 80
        '
        'Column8
        '
        Me.Column8.HeaderText = "Descendente"
        Me.Column8.MinimumWidth = 6
        Me.Column8.Name = "Column8"
        Me.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column8.Width = 125
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn1.HeaderText = "Codigo"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn2.HeaderText = "Nombre R00-AB"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn3.HeaderText = "Largo"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn4.HeaderText = "Profunidad Ubic."
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N2"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn5.HeaderText = "Margen Izq"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N2"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn6.HeaderText = "Margen Frontal"
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Ubic. de Piso"
        Me.DataGridViewCheckBoxColumn1.MinimumWidth = 6
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Width = 80
        '
        'chkHorizontalR
        '
        Me.chkHorizontalR.HeaderText = "Horizontal"
        Me.chkHorizontalR.MinimumWidth = 6
        Me.chkHorizontalR.Name = "chkHorizontalR"
        Me.chkHorizontalR.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkHorizontalR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkHorizontalR.Width = 80
        '
        'Column9
        '
        Me.Column9.HeaderText = "Descendente"
        Me.Column9.MinimumWidth = 6
        Me.Column9.Name = "Column9"
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column9.Width = 125
        '
        'frmEstSector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1940, 998)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmEstSector"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración de estructura de sector"
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.xtabConfig.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdIzq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdDer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.xtabRawDATA.ResumeLayout(False)
        CType(Me.dgridTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetTramoByBodega1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTramo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdIzq As DataGridView
    Friend WithEvents grdDer As DataGridView
    Friend WithEvents btnPegarDer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPegarIzq As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLimpiarIzq As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBorIzq As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnInsIzq As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLimpiarDer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBorDer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnInsDer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtabConfig As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents xtabRawDATA As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents mnuGuardarDatosGrid As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents dgridTramo As DevExpress.XtraGrid.GridControl
    Friend WithEvents DetTramoByBodega1 As DetTramoByBodega
    Friend WithEvents gvTramo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colCodigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents coldescripcion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colalto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents collargo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colancho As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colpos_x As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colpos_y As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colhorizontal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdSector As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Bodega_sectorTableAdapter1 As DetTramoByBodegaTableAdapters.bodega_sectorTableAdapter
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewCheckBoxColumn
    Friend WithEvents chkHorizontal As DataGridViewCheckBoxColumn
    Friend WithEvents Column8 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents chkHorizontalR As DataGridViewCheckBoxColumn
    Friend WithEvents Column9 As DataGridViewCheckBoxColumn
End Class
