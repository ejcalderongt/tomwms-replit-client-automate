<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMotivo_Anulacion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If DTMotivosAnulacion IsNot Nothing Then
                DTMotivosAnulacion.Dispose()
                DTMotivosAnulacion = Nothing
            End If
            If pBeMotivoAnulacionBodega IsNot Nothing Then
                pBeMotivoAnulacionBodega.Dispose()
                pBeMotivoAnulacionBodega = Nothing
            End If
            If pBeMotivoAnulacion IsNot Nothing Then
                pBeMotivoAnulacion.Dispose()
                pBeMotivoAnulacion = Nothing
            End If
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
        Me.components = New System.ComponentModel.Container()
        Dim NombreLabel As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMotivo_Anulacion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GrpEmpresaTB = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMotivoAnulacion = New TOMWMS.DsMotivoAnulacion()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdMotivoAnulacionBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.NombreTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkMotivo_Anulacion = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.TabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.TabMotivoAnulacionBodega = New DevExpress.XtraTab.XtraTabPage()
        NombreLabel = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmpresaTB.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMotivoAnulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkMotivo_Anulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDatos.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.TabMotivoAnulacionBodega.SuspendLayout()
        Me.SuspendLayout()
        '
        'NombreLabel
        '
        NombreLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(26, 108)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(62, 17)
        NombreLabel.TabIndex = 4
        NombreLabel.Text = "Nombre:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(26, 75)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(66, 17)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(51, 17)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(26, 140)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(51, 17)
        Label2.TabIndex = 6
        Label2.Text = "Activo:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(51, 39)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(51, 7)
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
        Fec_modLabel.Location = New System.Drawing.Point(535, 39)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(535, 7)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(953, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
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
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú motivo anulación"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 536)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(953, 30)
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(154, 36)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(154, 4)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(638, 36)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(638, 4)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'GrpEmpresaTB
        '
        Me.GrpEmpresaTB.Controls.Add(Me.GroupControl3)
        Me.GrpEmpresaTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpEmpresaTB.Location = New System.Drawing.Point(0, 0)
        Me.GrpEmpresaTB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpEmpresaTB.Name = "GrpEmpresaTB"
        Me.GrpEmpresaTB.Size = New System.Drawing.Size(951, 299)
        Me.GrpEmpresaTB.TabIndex = 0
        Me.GrpEmpresaTB.Tag = ""
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Grid)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(947, 269)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Bodega"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.Grid.Size = New System.Drawing.Size(943, 239)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsMotivoAnulacion
        '
        'DsMotivoAnulacion
        '
        Me.DsMotivoAnulacion.DataSetName = "DsMotivoAnulacion"
        Me.DsMotivoAnulacion.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colIdBodega, Me.IdMotivoAnulacionBodega, Me.colBodega, Me.IdInterno})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Grid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion
        '
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "Asignar"
        Me.colSeleccion.MinWidth = 23
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        Me.colSeleccion.Width = 87
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 23
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        Me.colIdBodega.Width = 87
        '
        'IdMotivoAnulacionBodega
        '
        Me.IdMotivoAnulacionBodega.Caption = "Asignación"
        Me.IdMotivoAnulacionBodega.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.IdMotivoAnulacionBodega.FieldName = "IdMotivoAnulacionBodega"
        Me.IdMotivoAnulacionBodega.MinWidth = 23
        Me.IdMotivoAnulacionBodega.Name = "IdMotivoAnulacionBodega"
        Me.IdMotivoAnulacionBodega.OptionsColumn.ReadOnly = True
        Me.IdMotivoAnulacionBodega.Width = 87
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 23
        Me.colBodega.Name = "colBodega"
        Me.colBodega.OptionsColumn.ReadOnly = True
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        Me.colBodega.Width = 87
        '
        'IdInterno
        '
        Me.IdInterno.Caption = "IdInterno"
        Me.IdInterno.FieldName = "IdInterno"
        Me.IdInterno.MinWidth = 23
        Me.IdInterno.Name = "IdInterno"
        Me.IdInterno.OptionsColumn.ReadOnly = True
        Me.IdInterno.Width = 87
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'GroupControl1
        '
        Me.GroupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.NombreTextEdit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(951, 287)
        Me.GroupControl1.TabIndex = 0
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEmpresa.Location = New System.Drawing.Point(150, 71)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(587, 22)
        Me.cmbEmpresa.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(150, 137)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(241, 24)
        Me.chkActivo.TabIndex = 7
        '
        'lblCodigo
        '
        Me.lblCodigo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(152, 44)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'NombreTextEdit
        '
        Me.NombreTextEdit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NombreTextEdit.Location = New System.Drawing.Point(150, 105)
        Me.NombreTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NombreTextEdit.MenuManager = Me.RibbonControl
        Me.NombreTextEdit.Name = "NombreTextEdit"
        Me.NombreTextEdit.Size = New System.Drawing.Size(587, 22)
        Me.NombreTextEdit.TabIndex = 5
        '
        'dkMotivo_Anulacion
        '
        Me.dkMotivo_Anulacion.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkMotivo_Anulacion.Form = Me
        Me.dkMotivo_Anulacion.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 510)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(953, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("ea8e9e1b-bebd-4f58-b922-b4eab968baa3")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 90)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 111)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 28)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(944, 78)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'TabDatos
        '
        Me.TabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDatos.Location = New System.Drawing.Point(0, 193)
        Me.TabDatos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedTabPage = Me.XtraTabPage1
        Me.TabDatos.Size = New System.Drawing.Size(953, 317)
        Me.TabDatos.TabIndex = 0
        Me.TabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.TabMotivoAnulacionBodega})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(951, 287)
        Me.XtraTabPage1.Text = "Datos generales"
        '
        'TabMotivoAnulacionBodega
        '
        Me.TabMotivoAnulacionBodega.Controls.Add(Me.GrpEmpresaTB)
        Me.TabMotivoAnulacionBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabMotivoAnulacionBodega.Name = "TabMotivoAnulacionBodega"
        Me.TabMotivoAnulacionBodega.Size = New System.Drawing.Size(951, 299)
        Me.TabMotivoAnulacionBodega.Text = "Asignación por bodega"
        '
        'frmMotivo_Anulacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 566)
        Me.Controls.Add(Me.TabDatos)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmMotivo_Anulacion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Motivo Anulación"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GrpEmpresaTB,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpEmpresaTB.ResumeLayout(false)
        CType(Me.GroupControl3,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl3.ResumeLayout(false)
        CType(Me.Grid,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DataBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DsMotivoAnulacion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        CType(Me.cmbEmpresa.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkActivo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NombreTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dkMotivo_Anulacion,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerBottom.ResumeLayout(false)
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        Me.DockPanel1_Container.PerformLayout
        CType(Me.TabDatos,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabDatos.ResumeLayout(false)
        Me.XtraTabPage1.ResumeLayout(false)
        Me.TabMotivoAnulacionBodega.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GrpEmpresaTB As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdMotivoAnulacionBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents NombreTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsMotivoAnulacion As TOMWMS.DsMotivoAnulacion
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dkMotivo_Anulacion As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents TabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabMotivoAnulacionBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
End Class
