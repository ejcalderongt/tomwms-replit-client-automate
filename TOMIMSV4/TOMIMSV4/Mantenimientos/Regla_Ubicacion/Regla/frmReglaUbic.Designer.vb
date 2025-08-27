<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReglaUbic
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pBeReglaUbicEnc IsNot Nothing Then
                    pBeReglaUbicEnc.Dispose()
                    pBeReglaUbicEnc = Nothing
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label37 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReglaUbic))
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.mnuUbic = New DevExpress.XtraBars.BarButtonItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabPropietarios = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridPropietario = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvPropietario = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdPropietarioBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombreComercial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.tabTiposRotacion = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridTipoRotacion = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvTipoRotacion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colActivo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdTipoRotacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tabIndiceRotacion = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridIndiceRotacion = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource3 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvIndiceRotacion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colActivo1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdIndiceRotacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDescripcion1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tabTiposProductos = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridTipoProducto = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource8 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvTipoProducto = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colidEstado1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit10 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tabPresentaciones = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridPresentation = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource10 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvPresentation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colidPresentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProprietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit9 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit8 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tabEstados = New DevExpress.XtraTab.XtraTabPage()
        Me.DgridEstadoProducto = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource4 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grdvEstadoProducto = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colidEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPropietario = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lblCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.BindingSource9 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingSource5 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingSource6 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblRegs = New DevExpress.XtraBars.BarStaticItem()
        Me.BindingSource7 = New System.Windows.Forms.BindingSource(Me.components)
        Me.dkReglaUbicacion = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Label37 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabPropietarios.SuspendLayout()
        CType(Me.DgridPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTiposRotacion.SuspendLayout()
        CType(Me.DgridTipoRotacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvTipoRotacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabIndiceRotacion.SuspendLayout()
        CType(Me.DgridIndiceRotacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvIndiceRotacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTiposProductos.SuspendLayout()
        CType(Me.DgridTipoProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvTipoProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPresentaciones.SuspendLayout()
        CType(Me.DgridPresentation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPresentation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabEstados.SuspendLayout()
        CType(Me.DgridEstadoProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvEstadoProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkReglaUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label37
        '
        Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label37.AutoSize = True
        Label37.Location = New System.Drawing.Point(478, 27)
        Label37.Name = "Label37"
        Label37.Size = New System.Drawing.Size(46, 16)
        Label37.TabIndex = 4
        Label37.Text = "Activo:"
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(12, 26)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(145, 27)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(57, 16)
        Label1.TabIndex = 2
        Label1.Text = "Nombre:"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(44, 52)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(44, 20)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(441, 52)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(441, 20)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup4})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Regla Filtro"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
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
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuUbic)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'mnuUbic
        '
        Me.mnuUbic.Caption = "Ubicaciones"
        Me.mnuUbic.Id = 5
        Me.mnuUbic.ImageOptions.Image = CType(resources.GetObject("mnuUbic.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuUbic.ImageOptions.LargeImage = CType(resources.GetObject("mnuUbic.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuUbic.Name = "mnuUbic"
        Me.mnuUbic.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl1.Controls.Add(Me.PanelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(898, 647)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Regla"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(2, 96)
        Me.XtraTabControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabPropietarios
        Me.XtraTabControl1.Size = New System.Drawing.Size(894, 549)
        Me.XtraTabControl1.TabIndex = 1
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabPropietarios, Me.tabTiposRotacion, Me.tabIndiceRotacion, Me.tabTiposProductos, Me.tabPresentaciones, Me.tabEstados})
        '
        'tabPropietarios
        '
        Me.tabPropietarios.Controls.Add(Me.DgridPropietario)
        Me.tabPropietarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPropietarios.Name = "tabPropietarios"
        Me.tabPropietarios.Size = New System.Drawing.Size(892, 519)
        Me.tabPropietarios.Text = "Propietarios"
        '
        'DgridPropietario
        '
        Me.DgridPropietario.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridPropietario.DataSource = Me.BindingSource1
        Me.DgridPropietario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridPropietario.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPropietario.Location = New System.Drawing.Point(0, 0)
        Me.DgridPropietario.MainView = Me.grdvPropietario
        Me.DgridPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPropietario.MenuManager = Me.RibbonControl
        Me.DgridPropietario.Name = "DgridPropietario"
        Me.DgridPropietario.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.DgridPropietario.Size = New System.Drawing.Size(892, 519)
        Me.DgridPropietario.TabIndex = 0
        Me.DgridPropietario.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPropietario})
        '
        'grdvPropietario
        '
        Me.grdvPropietario.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colIdPropietarioBodega, Me.colNombreComercial})
        Me.grdvPropietario.DetailHeight = 431
        Me.grdvPropietario.GridControl = Me.DgridPropietario
        Me.grdvPropietario.Name = "grdvPropietario"
        Me.grdvPropietario.OptionsFind.AlwaysVisible = True
        Me.grdvPropietario.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvPropietario.OptionsView.ShowFooter = True
        Me.grdvPropietario.OptionsView.ShowGroupPanel = False
        Me.grdvPropietario.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colNombreComercial, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'colSeleccion
        '
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "Seleccion"
        Me.colSeleccion.MinWidth = 23
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        Me.colSeleccion.Width = 106
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdPropietarioBodega
        '
        Me.colIdPropietarioBodega.FieldName = "IdPropietarioBodega"
        Me.colIdPropietarioBodega.MinWidth = 23
        Me.colIdPropietarioBodega.Name = "colIdPropietarioBodega"
        Me.colIdPropietarioBodega.Visible = True
        Me.colIdPropietarioBodega.VisibleIndex = 1
        Me.colIdPropietarioBodega.Width = 237
        '
        'colNombreComercial
        '
        Me.colNombreComercial.FieldName = "NombreComercial"
        Me.colNombreComercial.MinWidth = 23
        Me.colNombreComercial.Name = "colNombreComercial"
        Me.colNombreComercial.Visible = True
        Me.colNombreComercial.VisibleIndex = 2
        Me.colNombreComercial.Width = 523
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuUbic})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(898, 193)
        '
        'tabTiposRotacion
        '
        Me.tabTiposRotacion.Controls.Add(Me.DgridTipoRotacion)
        Me.tabTiposRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabTiposRotacion.Name = "tabTiposRotacion"
        Me.tabTiposRotacion.Size = New System.Drawing.Size(892, 519)
        Me.tabTiposRotacion.Text = "Tipos de Rotacion"
        '
        'DgridTipoRotacion
        '
        Me.DgridTipoRotacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridTipoRotacion.DataSource = Me.BindingSource2
        Me.DgridTipoRotacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridTipoRotacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTipoRotacion.Location = New System.Drawing.Point(0, 0)
        Me.DgridTipoRotacion.MainView = Me.grdvTipoRotacion
        Me.DgridTipoRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTipoRotacion.Name = "DgridTipoRotacion"
        Me.DgridTipoRotacion.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.DgridTipoRotacion.Size = New System.Drawing.Size(892, 519)
        Me.DgridTipoRotacion.TabIndex = 0
        Me.DgridTipoRotacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvTipoRotacion})
        '
        'grdvTipoRotacion
        '
        Me.grdvTipoRotacion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colActivo, Me.colIdTipoRotacion, Me.colDescripcion})
        Me.grdvTipoRotacion.DetailHeight = 431
        Me.grdvTipoRotacion.GridControl = Me.DgridTipoRotacion
        Me.grdvTipoRotacion.Name = "grdvTipoRotacion"
        Me.grdvTipoRotacion.OptionsFind.AlwaysVisible = True
        Me.grdvTipoRotacion.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvTipoRotacion.OptionsView.ShowFooter = True
        '
        'colActivo
        '
        Me.colActivo.Caption = "Selección"
        Me.colActivo.FieldName = "Activo"
        Me.colActivo.MinWidth = 23
        Me.colActivo.Name = "colActivo"
        Me.colActivo.Visible = True
        Me.colActivo.VisibleIndex = 0
        Me.colActivo.Width = 72
        '
        'colIdTipoRotacion
        '
        Me.colIdTipoRotacion.FieldName = "IdTipoRotacion"
        Me.colIdTipoRotacion.MinWidth = 23
        Me.colIdTipoRotacion.Name = "colIdTipoRotacion"
        Me.colIdTipoRotacion.Visible = True
        Me.colIdTipoRotacion.VisibleIndex = 1
        Me.colIdTipoRotacion.Width = 145
        '
        'colDescripcion
        '
        Me.colDescripcion.FieldName = "Descripcion"
        Me.colDescripcion.MinWidth = 23
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.Visible = True
        Me.colDescripcion.VisibleIndex = 2
        Me.colDescripcion.Width = 649
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'tabIndiceRotacion
        '
        Me.tabIndiceRotacion.Controls.Add(Me.DgridIndiceRotacion)
        Me.tabIndiceRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabIndiceRotacion.Name = "tabIndiceRotacion"
        Me.tabIndiceRotacion.Size = New System.Drawing.Size(892, 519)
        Me.tabIndiceRotacion.Text = "Indice rotación"
        '
        'DgridIndiceRotacion
        '
        Me.DgridIndiceRotacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridIndiceRotacion.DataSource = Me.BindingSource3
        Me.DgridIndiceRotacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridIndiceRotacion.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridIndiceRotacion.Location = New System.Drawing.Point(0, 0)
        Me.DgridIndiceRotacion.MainView = Me.grdvIndiceRotacion
        Me.DgridIndiceRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridIndiceRotacion.Name = "DgridIndiceRotacion"
        Me.DgridIndiceRotacion.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3})
        Me.DgridIndiceRotacion.Size = New System.Drawing.Size(892, 519)
        Me.DgridIndiceRotacion.TabIndex = 0
        Me.DgridIndiceRotacion.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvIndiceRotacion})
        '
        'grdvIndiceRotacion
        '
        Me.grdvIndiceRotacion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colActivo1, Me.colIdIndiceRotacion, Me.colDescripcion1})
        Me.grdvIndiceRotacion.DetailHeight = 431
        Me.grdvIndiceRotacion.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.grdvIndiceRotacion.GridControl = Me.DgridIndiceRotacion
        Me.grdvIndiceRotacion.Name = "grdvIndiceRotacion"
        Me.grdvIndiceRotacion.OptionsFind.AlwaysVisible = True
        Me.grdvIndiceRotacion.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvIndiceRotacion.OptionsView.ShowFooter = True
        '
        'colActivo1
        '
        Me.colActivo1.Caption = "Seleccionar"
        Me.colActivo1.FieldName = "Activo"
        Me.colActivo1.MinWidth = 23
        Me.colActivo1.Name = "colActivo1"
        Me.colActivo1.Visible = True
        Me.colActivo1.VisibleIndex = 0
        Me.colActivo1.Width = 108
        '
        'colIdIndiceRotacion
        '
        Me.colIdIndiceRotacion.FieldName = "IdIndiceRotacion"
        Me.colIdIndiceRotacion.MinWidth = 23
        Me.colIdIndiceRotacion.Name = "colIdIndiceRotacion"
        Me.colIdIndiceRotacion.Visible = True
        Me.colIdIndiceRotacion.VisibleIndex = 1
        Me.colIdIndiceRotacion.Width = 135
        '
        'colDescripcion1
        '
        Me.colDescripcion1.FieldName = "Descripcion"
        Me.colDescripcion1.MinWidth = 23
        Me.colDescripcion1.Name = "colDescripcion1"
        Me.colDescripcion1.Visible = True
        Me.colDescripcion1.VisibleIndex = 2
        Me.colDescripcion1.Width = 622
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        '
        'tabTiposProductos
        '
        Me.tabTiposProductos.AllowDrop = True
        Me.tabTiposProductos.Controls.Add(Me.DgridTipoProducto)
        Me.tabTiposProductos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabTiposProductos.Name = "tabTiposProductos"
        Me.tabTiposProductos.Size = New System.Drawing.Size(892, 519)
        Me.tabTiposProductos.Text = "Tipos de producto"
        '
        'DgridTipoProducto
        '
        Me.DgridTipoProducto.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridTipoProducto.DataSource = Me.BindingSource8
        Me.DgridTipoProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridTipoProducto.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTipoProducto.Location = New System.Drawing.Point(0, 0)
        Me.DgridTipoProducto.MainView = Me.grdvTipoProducto
        Me.DgridTipoProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridTipoProducto.Name = "DgridTipoProducto"
        Me.DgridTipoProducto.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit7, Me.RepositoryItemCheckEdit6, Me.RepositoryItemCheckEdit10, Me.RepositoryItemCheckEdit11})
        Me.DgridTipoProducto.Size = New System.Drawing.Size(892, 519)
        Me.DgridTipoProducto.TabIndex = 0
        Me.DgridTipoProducto.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvTipoProducto})
        '
        'grdvTipoProducto
        '
        Me.grdvTipoProducto.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion2, Me.colidEstado1, Me.colNombre1, Me.colPropietario1})
        Me.grdvTipoProducto.DetailHeight = 431
        Me.grdvTipoProducto.GridControl = Me.DgridTipoProducto
        Me.grdvTipoProducto.Name = "grdvTipoProducto"
        Me.grdvTipoProducto.OptionsFind.AlwaysVisible = True
        Me.grdvTipoProducto.OptionsView.ShowFooter = True
        Me.grdvTipoProducto.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion2
        '
        Me.colSeleccion2.ColumnEdit = Me.RepositoryItemCheckEdit11
        Me.colSeleccion2.FieldName = "Seleccion"
        Me.colSeleccion2.MinWidth = 23
        Me.colSeleccion2.Name = "colSeleccion2"
        Me.colSeleccion2.Visible = True
        Me.colSeleccion2.VisibleIndex = 0
        Me.colSeleccion2.Width = 89
        '
        'RepositoryItemCheckEdit11
        '
        Me.RepositoryItemCheckEdit11.AutoHeight = False
        Me.RepositoryItemCheckEdit11.Name = "RepositoryItemCheckEdit11"
        '
        'colidEstado1
        '
        Me.colidEstado1.FieldName = "idTipoProducto"
        Me.colidEstado1.MinWidth = 23
        Me.colidEstado1.Name = "colidEstado1"
        Me.colidEstado1.Visible = True
        Me.colidEstado1.VisibleIndex = 1
        Me.colidEstado1.Width = 258
        '
        'colNombre1
        '
        Me.colNombre1.FieldName = "Nombre"
        Me.colNombre1.MinWidth = 23
        Me.colNombre1.Name = "colNombre1"
        Me.colNombre1.Visible = True
        Me.colNombre1.VisibleIndex = 2
        Me.colNombre1.Width = 258
        '
        'colPropietario1
        '
        Me.colPropietario1.FieldName = "Propietario"
        Me.colPropietario1.MinWidth = 23
        Me.colPropietario1.Name = "colPropietario1"
        Me.colPropietario1.Visible = True
        Me.colPropietario1.VisibleIndex = 3
        Me.colPropietario1.Width = 261
        '
        'RepositoryItemCheckEdit7
        '
        Me.RepositoryItemCheckEdit7.AutoHeight = False
        Me.RepositoryItemCheckEdit7.Name = "RepositoryItemCheckEdit7"
        '
        'RepositoryItemCheckEdit6
        '
        Me.RepositoryItemCheckEdit6.AutoHeight = False
        Me.RepositoryItemCheckEdit6.Name = "RepositoryItemCheckEdit6"
        '
        'RepositoryItemCheckEdit10
        '
        Me.RepositoryItemCheckEdit10.AutoHeight = False
        Me.RepositoryItemCheckEdit10.Name = "RepositoryItemCheckEdit10"
        '
        'tabPresentaciones
        '
        Me.tabPresentaciones.Controls.Add(Me.DgridPresentation)
        Me.tabPresentaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPresentaciones.Name = "tabPresentaciones"
        Me.tabPresentaciones.PageVisible = False
        Me.tabPresentaciones.Size = New System.Drawing.Size(892, 519)
        Me.tabPresentaciones.Text = "Presentaciones de producto"
        '
        'DgridPresentation
        '
        Me.DgridPresentation.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridPresentation.DataSource = Me.BindingSource10
        Me.DgridPresentation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridPresentation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPresentation.Location = New System.Drawing.Point(0, 0)
        Me.DgridPresentation.MainView = Me.grdvPresentation
        Me.DgridPresentation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridPresentation.Name = "DgridPresentation"
        Me.DgridPresentation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit9, Me.RepositoryItemCheckEdit8, Me.RepositoryItemCheckEdit12})
        Me.DgridPresentation.Size = New System.Drawing.Size(892, 519)
        Me.DgridPresentation.TabIndex = 0
        Me.DgridPresentation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPresentation})
        '
        'grdvPresentation
        '
        Me.grdvPresentation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion3, Me.colidPresentacion, Me.colPresentacion, Me.colProducto, Me.colProprietario})
        Me.grdvPresentation.DetailHeight = 431
        Me.grdvPresentation.GridControl = Me.DgridPresentation
        Me.grdvPresentation.Name = "grdvPresentation"
        Me.grdvPresentation.OptionsFind.AlwaysVisible = True
        Me.grdvPresentation.OptionsView.ShowFooter = True
        Me.grdvPresentation.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion3
        '
        Me.colSeleccion3.ColumnEdit = Me.RepositoryItemCheckEdit12
        Me.colSeleccion3.FieldName = "Seleccion"
        Me.colSeleccion3.MinWidth = 23
        Me.colSeleccion3.Name = "colSeleccion3"
        Me.colSeleccion3.Visible = True
        Me.colSeleccion3.VisibleIndex = 0
        Me.colSeleccion3.Width = 80
        '
        'RepositoryItemCheckEdit12
        '
        Me.RepositoryItemCheckEdit12.AutoHeight = False
        Me.RepositoryItemCheckEdit12.Name = "RepositoryItemCheckEdit12"
        '
        'colidPresentacion
        '
        Me.colidPresentacion.FieldName = "idPresentacion"
        Me.colidPresentacion.MinWidth = 23
        Me.colidPresentacion.Name = "colidPresentacion"
        Me.colidPresentacion.Visible = True
        Me.colidPresentacion.VisibleIndex = 1
        Me.colidPresentacion.Width = 195
        '
        'colPresentacion
        '
        Me.colPresentacion.FieldName = "Presentacion"
        Me.colPresentacion.MinWidth = 23
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.Visible = True
        Me.colPresentacion.VisibleIndex = 2
        Me.colPresentacion.Width = 195
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.MinWidth = 23
        Me.colProducto.Name = "colProducto"
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 3
        Me.colProducto.Width = 195
        '
        'colProprietario
        '
        Me.colProprietario.FieldName = "Proprietario"
        Me.colProprietario.MinWidth = 23
        Me.colProprietario.Name = "colProprietario"
        Me.colProprietario.Visible = True
        Me.colProprietario.VisibleIndex = 4
        Me.colProprietario.Width = 201
        '
        'RepositoryItemCheckEdit9
        '
        Me.RepositoryItemCheckEdit9.AutoHeight = False
        Me.RepositoryItemCheckEdit9.Name = "RepositoryItemCheckEdit9"
        '
        'RepositoryItemCheckEdit8
        '
        Me.RepositoryItemCheckEdit8.AutoHeight = False
        Me.RepositoryItemCheckEdit8.Name = "RepositoryItemCheckEdit8"
        '
        'tabEstados
        '
        Me.tabEstados.Controls.Add(Me.DgridEstadoProducto)
        Me.tabEstados.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabEstados.Name = "tabEstados"
        Me.tabEstados.Size = New System.Drawing.Size(892, 519)
        Me.tabEstados.Text = "Estados de producto"
        '
        'DgridEstadoProducto
        '
        Me.DgridEstadoProducto.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridEstadoProducto.DataSource = Me.BindingSource4
        Me.DgridEstadoProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridEstadoProducto.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridEstadoProducto.Location = New System.Drawing.Point(0, 0)
        Me.DgridEstadoProducto.MainView = Me.grdvEstadoProducto
        Me.DgridEstadoProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgridEstadoProducto.Name = "DgridEstadoProducto"
        Me.DgridEstadoProducto.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5})
        Me.DgridEstadoProducto.Size = New System.Drawing.Size(892, 519)
        Me.DgridEstadoProducto.TabIndex = 0
        Me.DgridEstadoProducto.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvEstadoProducto})
        '
        'grdvEstadoProducto
        '
        Me.grdvEstadoProducto.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion1, Me.colidEstado, Me.colNombre, Me.colPropietario})
        Me.grdvEstadoProducto.DetailHeight = 431
        Me.grdvEstadoProducto.GridControl = Me.DgridEstadoProducto
        Me.grdvEstadoProducto.Name = "grdvEstadoProducto"
        Me.grdvEstadoProducto.OptionsFind.AlwaysVisible = True
        Me.grdvEstadoProducto.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grdvEstadoProducto.OptionsView.ShowFooter = True
        Me.grdvEstadoProducto.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion1
        '
        Me.colSeleccion1.ColumnEdit = Me.RepositoryItemCheckEdit5
        Me.colSeleccion1.FieldName = "Seleccion"
        Me.colSeleccion1.MinWidth = 23
        Me.colSeleccion1.Name = "colSeleccion1"
        Me.colSeleccion1.Visible = True
        Me.colSeleccion1.VisibleIndex = 0
        Me.colSeleccion1.Width = 93
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        '
        'colidEstado
        '
        Me.colidEstado.FieldName = "idEstado"
        Me.colidEstado.MinWidth = 23
        Me.colidEstado.Name = "colidEstado"
        Me.colidEstado.Visible = True
        Me.colidEstado.VisibleIndex = 1
        Me.colidEstado.Width = 255
        '
        'colNombre
        '
        Me.colNombre.FieldName = "Nombre"
        Me.colNombre.MinWidth = 23
        Me.colNombre.Name = "colNombre"
        Me.colNombre.Visible = True
        Me.colNombre.VisibleIndex = 2
        Me.colNombre.Width = 255
        '
        'colPropietario
        '
        Me.colPropietario.FieldName = "Propietario"
        Me.colPropietario.MinWidth = 23
        Me.colPropietario.Name = "colPropietario"
        Me.colPropietario.Visible = True
        Me.colPropietario.VisibleIndex = 3
        Me.colPropietario.Width = 261
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.lblCodigo)
        Me.PanelControl1.Controls.Add(Me.txtNombre)
        Me.PanelControl1.Controls.Add(Label37)
        Me.PanelControl1.Controls.Add(Label1)
        Me.PanelControl1.Controls.Add(Me.chkActivo)
        Me.PanelControl1.Controls.Add(Label12)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(2, 28)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 68)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(894, 68)
        Me.PanelControl1.TabIndex = 0
        '
        'lblCodigo
        '
        Me.lblCodigo.Location = New System.Drawing.Point(69, 23)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblCodigo.MenuManager = Me.RibbonControl
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Properties.ReadOnly = True
        Me.lblCodigo.Size = New System.Drawing.Size(49, 22)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(202, 23)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(241, 22)
        Me.txtNombre.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(535, 23)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(26, 24)
        Me.chkActivo.TabIndex = 5
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(147, 48)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(147, 16)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(544, 48)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(544, 16)
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
        'dkReglaUbicacion
        '
        Me.dkReglaUbicacion.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkReglaUbicacion.Form = Me
        Me.dkReglaUbicacion.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 840)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(898, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("fbd11ab7-0b5d-44da-9f03-38e8d2d7a141")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 100)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(898, 123)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(891, 89)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmReglaUbic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 866)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(772, 650)
        Me.Name = "frmReglaUbic"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Regla Filtro"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        CType(Me.XtraTabControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.XtraTabControl1.ResumeLayout(false)
        Me.tabPropietarios.ResumeLayout(false)
        CType(Me.DgridPropietario,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvPropietario,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabTiposRotacion.ResumeLayout(false)
        CType(Me.DgridTipoRotacion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvTipoRotacion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit2,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabIndiceRotacion.ResumeLayout(false)
        CType(Me.DgridIndiceRotacion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvIndiceRotacion,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit3,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabTiposProductos.ResumeLayout(false)
        CType(Me.DgridTipoProducto,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource8,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvTipoProducto,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit11,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit10,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabPresentaciones.ResumeLayout(false)
        CType(Me.DgridPresentation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvPresentation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit12,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit8,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabEstados.ResumeLayout(false)
        CType(Me.DgridEstadoProducto,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdvEstadoProducto,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckEdit4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        Me.PanelControl1.PerformLayout
        CType(Me.lblCodigo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNombre.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.chkActivo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_agrDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_agrTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Fec_modDateEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.User_modTextEdit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BindingSource7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dkReglaUbicacion,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerBottom.ResumeLayout(false)
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        Me.DockPanel1_Container.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuUbic As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabPropietarios As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabTiposRotacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabTiposProductos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabIndiceRotacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabPresentaciones As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabEstados As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPropietarioBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombreComercial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Private WithEvents DgridTipoRotacion As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvTipoRotacion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource2 As BindingSource
    Friend WithEvents colIdTipoRotacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescripcion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActivo As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents DgridIndiceRotacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource3 As BindingSource
    Friend WithEvents colIdIndiceRotacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescripcion1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActivo1 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents DgridEstadoProducto As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvEstadoProducto As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource4 As BindingSource
    Friend WithEvents colSeleccion1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colidEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colPropietario As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents DgridTipoProducto As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvTipoProducto As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource5 As BindingSource
    Friend WithEvents BindingSource6 As BindingSource
    Friend WithEvents RepositoryItemCheckEdit10 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource7 As BindingSource
    Friend WithEvents BindingSource8 As BindingSource
    Friend WithEvents colSeleccion2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colidEstado1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNombre1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPropietario1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BindingSource9 As BindingSource
    Private WithEvents DgridPresentation As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvPresentation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit8 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit9 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BindingSource10 As BindingSource
    Friend WithEvents colSeleccion3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colidPresentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProprietario As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents grdvIndiceRotacion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DgridPropietario As DevExpress.XtraGrid.GridControl
    Private WithEvents grdvPropietario As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dkReglaUbicacion As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
End Class
