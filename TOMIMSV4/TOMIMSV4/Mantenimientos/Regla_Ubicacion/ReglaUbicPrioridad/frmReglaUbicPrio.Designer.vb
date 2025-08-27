<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReglaUbicPrio
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If BeReglaUbicPrioEnc IsNot Nothing Then
                    BeReglaUbicPrioEnc.Dispose()
                    BeReglaUbicPrioEnc = Nothing
                End If
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
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label37 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReglaUbicPrio))
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnProd = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.xTabPDet = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridDet = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvDet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdReglaUbicPrioParam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colOrden = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.colTipo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colActivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xTabPProd = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridProd = New DevExpress.XtraGrid.GridControl()
        Me.grdvProductos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.lblCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.dkReglaUbicPrioridad = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label37 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.xTabPDet.SuspendLayout()
        CType(Me.DgridDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xTabPProd.SuspendLayout()
        CType(Me.DgridProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkReglaUbicPrioridad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(118, 54)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(118, 22)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(514, 54)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(514, 22)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label37
        '
        Label37.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(846, 50)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(51, 17)
        Label37.TabIndex = 2
        Label37.Text = "Activo:"
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(27, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(51, 17)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(27, 84)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(62, 17)
        Label1.TabIndex = 4
        Label1.Text = "Nombre:"
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(901, 48)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(66, 24)
        Me.chkActivo.TabIndex = 3
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.BarButtonItem2, Me.btnProd, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.[True]
        Me.RibbonControl.ShowItemCaptionsInPageHeader = True
        Me.RibbonControl.Size = New System.Drawing.Size(1076, 193)
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 5
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'btnProd
        '
        Me.btnProd.Caption = "Productos"
        Me.btnProd.Id = 6
        Me.btnProd.ImageOptions.SvgImage = CType(resources.GetObject("btnProd.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnProd.Name = "btnProd"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Prioridad regla de ubicación"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnProd)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(86, 80)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(241, 22)
        Me.txtNombre.TabIndex = 5
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(220, 50)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(220, 18)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(617, 50)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(617, 18)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 5
        Me.lblRegs.Name = "lblRegs"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl1.Controls.Add(Me.GroupControl3)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1076, 735)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Orden de Reglas de Selección de las Ubicaciones"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(2, 174)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.xTabPDet
        Me.XtraTabControl1.Size = New System.Drawing.Size(1072, 559)
        Me.XtraTabControl1.TabIndex = 1
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xTabPDet, Me.xTabPProd})
        '
        'xTabPDet
        '
        Me.xTabPDet.Controls.Add(Me.DgridDet)
        Me.xTabPDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xTabPDet.Name = "xTabPDet"
        Me.xTabPDet.Size = New System.Drawing.Size(1070, 529)
        Me.xTabPDet.Text = "Detalle de prioridades"
        '
        'DgridDet
        '
        Me.DgridDet.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridDet.DataSource = Me.BindingSource1
        Me.DgridDet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridDet.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridDet.Location = New System.Drawing.Point(0, 0)
        Me.DgridDet.MainView = Me.grdvDet
        Me.DgridDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridDet.Name = "DgridDet"
        Me.DgridDet.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemSpinEdit1})
        Me.DgridDet.Size = New System.Drawing.Size(1070, 529)
        Me.DgridDet.TabIndex = 0
        Me.DgridDet.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvDet})
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(TOMWMS.clsBeRegla_ubic_prio_param_SelectionList)
        '
        'grdvDet
        '
        Me.grdvDet.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colIdReglaUbicPrioParam, Me.colNombre, Me.colOrden, Me.colTipo, Me.colActivo})
        Me.grdvDet.DetailHeight = 431
        Me.grdvDet.GridControl = Me.DgridDet
        Me.grdvDet.Name = "grdvDet"
        Me.grdvDet.OptionsFind.AlwaysVisible = True
        Me.grdvDet.OptionsView.ShowFooter = True
        Me.grdvDet.OptionsView.ShowGroupPanel = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 23
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdReglaUbicPrioParam
        '
        Me.colIdReglaUbicPrioParam.Caption = "Código"
        Me.colIdReglaUbicPrioParam.FieldName = "IdReglaUbicPrioParam"
        Me.colIdReglaUbicPrioParam.MinWidth = 23
        Me.colIdReglaUbicPrioParam.Name = "colIdReglaUbicPrioParam"
        Me.colIdReglaUbicPrioParam.OptionsColumn.AllowEdit = False
        Me.colIdReglaUbicPrioParam.Visible = True
        Me.colIdReglaUbicPrioParam.VisibleIndex = 1
        Me.colIdReglaUbicPrioParam.Width = 87
        '
        'colNombre
        '
        Me.colNombre.FieldName = "Nombre"
        Me.colNombre.MinWidth = 23
        Me.colNombre.Name = "colNombre"
        Me.colNombre.OptionsColumn.AllowEdit = False
        Me.colNombre.Visible = True
        Me.colNombre.VisibleIndex = 2
        Me.colNombre.Width = 87
        '
        'colOrden
        '
        Me.colOrden.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.colOrden.FieldName = "Orden"
        Me.colOrden.MinWidth = 23
        Me.colOrden.Name = "colOrden"
        Me.colOrden.Visible = True
        Me.colOrden.VisibleIndex = 3
        Me.colOrden.Width = 87
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'colTipo
        '
        Me.colTipo.FieldName = "Tipo"
        Me.colTipo.MinWidth = 23
        Me.colTipo.Name = "colTipo"
        Me.colTipo.OptionsColumn.AllowEdit = False
        Me.colTipo.Visible = True
        Me.colTipo.VisibleIndex = 4
        Me.colTipo.Width = 87
        '
        'colActivo
        '
        Me.colActivo.FieldName = "Activo"
        Me.colActivo.MinWidth = 23
        Me.colActivo.Name = "colActivo"
        Me.colActivo.OptionsColumn.AllowEdit = False
        Me.colActivo.Visible = True
        Me.colActivo.VisibleIndex = 5
        Me.colActivo.Width = 87
        '
        'xTabPProd
        '
        Me.xTabPProd.Controls.Add(Me.DgridProd)
        Me.xTabPProd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xTabPProd.Name = "xTabPProd"
        Me.xTabPProd.Size = New System.Drawing.Size(1069, 541)
        Me.xTabPProd.Text = "Productos"
        '
        'DgridProd
        '
        Me.DgridProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridProd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridProd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridProd.Location = New System.Drawing.Point(0, 0)
        Me.DgridProd.MainView = Me.grdvProductos
        Me.DgridProd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridProd.Name = "DgridProd"
        Me.DgridProd.Size = New System.Drawing.Size(1069, 541)
        Me.DgridProd.TabIndex = 0
        Me.DgridProd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvProductos})
        '
        'grdvProductos
        '
        Me.grdvProductos.DetailHeight = 431
        Me.grdvProductos.GridControl = Me.DgridProd
        Me.grdvProductos.Name = "grdvProductos"
        Me.grdvProductos.OptionsBehavior.Editable = False
        Me.grdvProductos.OptionsFind.AlwaysVisible = True
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.lblCodigo)
        Me.GroupControl3.Controls.Add(Label37)
        Me.GroupControl3.Controls.Add(Me.chkActivo)
        Me.GroupControl3.Controls.Add(Label12)
        Me.GroupControl3.Controls.Add(Me.txtNombre)
        Me.GroupControl3.Controls.Add(Label1)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1072, 146)
        Me.GroupControl3.TabIndex = 0
        '
        'lblCodigo
        '
        Me.lblCodigo.Location = New System.Drawing.Point(86, 48)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblCodigo.MenuManager = Me.RibbonControl
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Properties.ReadOnly = True
        Me.lblCodigo.Size = New System.Drawing.Size(241, 22)
        Me.lblCodigo.TabIndex = 1
        '
        'dkReglaUbicPrioridad
        '
        Me.dkReglaUbicPrioridad.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkReglaUbicPrioridad.Form = Me
        Me.dkReglaUbicPrioridad.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 928)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1076, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("91345755-494e-48f4-88c4-bc42909fb28e")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 100)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1076, 123)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1069, 89)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmReglaUbicPrio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1076, 954)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmReglaUbicPrio"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prioridad regla de ubicación"
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.xTabPDet.ResumeLayout(False)
        CType(Me.DgridDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xTabPProd.ResumeLayout(False)
        CType(Me.DgridProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkReglaUbicPrioridad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnProd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xTabPDet As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DgridDet As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvDet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xTabPProd As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DgridProd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvProductos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdReglaUbicPrioParam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActivo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrden As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTipo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents dkReglaUbicPrioridad As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
End Class
