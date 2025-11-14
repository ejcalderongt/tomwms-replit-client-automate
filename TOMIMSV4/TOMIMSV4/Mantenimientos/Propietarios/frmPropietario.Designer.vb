<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPropietario
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
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim ImagenLabel As System.Windows.Forms.Label
        Dim ContactoLabel As System.Windows.Forms.Label
        Dim Nombre_comercialLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim lblControlLote As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label54 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim lblCodigoAcceso As System.Windows.Forms.Label
        Dim lblClaveAcceso As System.Windows.Forms.Label
        Dim lblConfirmarClaveAcceso As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPropietario))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuStock = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMovimientos = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivarUX = New DevExpress.XtraEditors.CheckEdit()
        Me.txtConfirmarClave = New DevExpress.XtraEditors.TextEdit()
        Me.txtClaveAcceso = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoAcceso = New DevExpress.XtraEditors.TextEdit()
        Me.chkEsConsolidador = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNIT = New DevExpress.XtraEditors.TextEdit()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTipoActualizacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkActualizarPrecioOC = New DevExpress.XtraEditors.CheckEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.btnExaminar = New DevExpress.XtraEditors.SimpleButton()
        Me.picFoto = New System.Windows.Forms.PictureBox()
        Me.ContactoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Nombre_comercialTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.DireccionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GrpEmpresaTB = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsPropietario = New TOMWMS.DsPropietario()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdPropietarioBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.txtCorreoElectronico = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivoDest = New DevExpress.XtraEditors.CheckEdit()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPR = New System.Windows.Forms.ToolStripButton()
        Me.cmdGuardar = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDetalle = New DevExpress.XtraGrid.GridControl()
        Me.GridViewDet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkDestinatarioActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCargoDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtEmailDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtApellidoDestinatario = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono1 = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono2 = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigoD = New System.Windows.Forms.Label()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivoD = New DevExpress.XtraEditors.CheckEdit()
        Me.GridDestinatario = New DevExpress.XtraGrid.GridControl()
        Me.ViewDestinatario = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdAlertas = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdEliminar = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.chkActivoM = New DevExpress.XtraEditors.CheckEdit()
        Me.GridMensaje = New DevExpress.XtraGrid.GridControl()
        Me.ViewMensaje = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dkPropietario = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtraPropietario = New DevExpress.XtraTab.XtraTabControl()
        Me.DatosPropietario = New DevExpress.XtraTab.XtraTabPage()
        Me.PropietarioBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.Destinatarios = New DevExpress.XtraTab.XtraTabPage()
        Me.Reglas = New DevExpress.XtraTab.XtraTabPage()
        Me.tabUM = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridUM = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabEstados = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridEstados = New DevExpress.XtraGrid.GridControl()
        Me.gvEstados = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabProductos = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridProductos = New DevExpress.XtraGrid.GridControl()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabStock = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridStock = New DevExpress.XtraGrid.GridControl()
        Me.grdvStock = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView12 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tabMovimientos = New DevExpress.XtraTab.XtraTabPage()
        Me.dgridMovimientos = New DevExpress.XtraGrid.GridControl()
        Me.gviewMovimientos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView14 = New DevExpress.XtraGrid.Views.Grid.GridView()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        ImagenLabel = New System.Windows.Forms.Label()
        ContactoLabel = New System.Windows.Forms.Label()
        Nombre_comercialLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        lblControlLote = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label54 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        lblCodigoAcceso = New System.Windows.Forms.Label()
        lblClaveAcceso = New System.Windows.Forms.Label()
        lblConfirmarClaveAcceso = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.chkActivarUX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConfirmarClave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClaveAcceso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoAcceso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsConsolidador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNIT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoActualizacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActualizarPrecioOC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Nombre_comercialTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmpresaTB.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreoElectronico, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtCorreoElectronico.SuspendLayout()
        CType(Me.chkActivoDest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDestinatarioActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCargoDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApellidoDestinatario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.chkActivoD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridDestinatario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewDestinatario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.chkActivoM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridMensaje, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewMensaje, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtraPropietario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraPropietario.SuspendLayout()
        Me.DatosPropietario.SuspendLayout()
        Me.PropietarioBodega.SuspendLayout()
        Me.Destinatarios.SuspendLayout()
        Me.Reglas.SuspendLayout()
        Me.tabUM.SuspendLayout()
        CType(Me.dgridUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabEstados.SuspendLayout()
        CType(Me.dgridEstados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEstados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabProductos.SuspendLayout()
        CType(Me.dgridProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabStock.SuspendLayout()
        CType(Me.dgridStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMovimientos.SuspendLayout()
        CType(Me.dgridMovimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gviewMovimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(26, 102)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label12
        '
        Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 42)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(89, 16)
        Label12.TabIndex = 0
        Label12.Text = "Id Propietario:"
        '
        'ImagenLabel
        '
        ImagenLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ImagenLabel.AutoSize = True
        ImagenLabel.Location = New System.Drawing.Point(26, 411)
        ImagenLabel.Name = "ImagenLabel"
        ImagenLabel.Size = New System.Drawing.Size(55, 16)
        ImagenLabel.TabIndex = 19
        ImagenLabel.Text = "Imagen:"
        '
        'ContactoLabel
        '
        ContactoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ContactoLabel.AutoSize = True
        ContactoLabel.Location = New System.Drawing.Point(26, 134)
        ContactoLabel.Name = "ContactoLabel"
        ContactoLabel.Size = New System.Drawing.Size(62, 16)
        ContactoLabel.TabIndex = 4
        ContactoLabel.Text = "Contacto:"
        '
        'Nombre_comercialLabel
        '
        Nombre_comercialLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Nombre_comercialLabel.AutoSize = True
        Nombre_comercialLabel.Location = New System.Drawing.Point(26, 166)
        Nombre_comercialLabel.Name = "Nombre_comercialLabel"
        Nombre_comercialLabel.Size = New System.Drawing.Size(118, 16)
        Nombre_comercialLabel.TabIndex = 6
        Nombre_comercialLabel.Text = "Nombre Comercial:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(26, 198)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(62, 16)
        TelefonoLabel.TabIndex = 8
        TelefonoLabel.Text = "Telefono:"
        '
        'DireccionLabel
        '
        DireccionLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(26, 230)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(64, 16)
        DireccionLabel.TabIndex = 10
        DireccionLabel.Text = "Direccion:"
        '
        'EmailLabel
        '
        EmailLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(26, 262)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(43, 16)
        EmailLabel.TabIndex = 12
        EmailLabel.Text = "Email:"
        '
        'lblControlLote
        '
        lblControlLote.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblControlLote.AutoSize = True
        lblControlLote.Location = New System.Drawing.Point(26, 320)
        lblControlLote.Name = "lblControlLote"
        lblControlLote.Size = New System.Drawing.Size(123, 16)
        lblControlLote.TabIndex = 14
        lblControlLote.Text = "Actualiza Precio OC:"
        '
        'Label3
        '
        Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(26, 97)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(57, 16)
        Label3.TabIndex = 3
        Label3.Text = "Nombre:"
        '
        'Label4
        '
        Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(26, 126)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(57, 16)
        Label4.TabIndex = 5
        Label4.Text = "Apellido:"
        '
        'Label5
        '
        Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(28, 155)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(43, 16)
        Label5.TabIndex = 7
        Label5.Text = "Email:"
        '
        'Label6
        '
        Label6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(26, 185)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(62, 16)
        Label6.TabIndex = 9
        Label6.Text = "Teléfono:"
        '
        'Label7
        '
        Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(28, 214)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(73, 16)
        Label7.TabIndex = 11
        Label7.Text = "Teléfono 2:"
        '
        'Label9
        '
        Label9.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(26, 69)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(46, 16)
        Label9.TabIndex = 1
        Label9.Text = "Código"
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(28, 242)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(46, 16)
        Label1.TabIndex = 13
        Label1.Text = "Cargo:"
        '
        'Label2
        '
        Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(1252, 32)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(46, 16)
        Label2.TabIndex = 0
        Label2.Text = "Activo:"
        '
        'Label54
        '
        Label54.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label54.AutoSize = True
        Label54.BackColor = System.Drawing.Color.Transparent
        Label54.Location = New System.Drawing.Point(566, 31)
        Label54.Name = "Label54"
        Label54.Size = New System.Drawing.Size(52, 16)
        Label54.TabIndex = 0
        Label54.Text = "Activos:"
        '
        'Label10
        '
        Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.BackColor = System.Drawing.Color.Transparent
        Label10.Location = New System.Drawing.Point(850, 31)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(52, 16)
        Label10.TabIndex = 0
        Label10.Text = "Activos:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(36, 50)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(36, 18)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(520, 50)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(520, 18)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label8
        '
        Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(26, 384)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(46, 16)
        Label8.TabIndex = 17
        Label8.Text = "Activo:"
        '
        'Label11
        '
        Label11.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(28, 274)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(46, 16)
        Label11.TabIndex = 15
        Label11.Text = "Activo:"
        '
        'Label14
        '
        Label14.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(25, 294)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(32, 16)
        Label14.TabIndex = 84
        Label14.Text = "NIT:"
        '
        'Label15
        '
        Label15.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(458, 66)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(103, 16)
        Label15.TabIndex = 86
        Label15.Text = "Es Consolidador:"
        '
        'lblCodigoAcceso
        '
        lblCodigoAcceso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigoAcceso.AutoSize = True
        lblCodigoAcceso.Location = New System.Drawing.Point(34, 73)
        lblCodigoAcceso.Name = "lblCodigoAcceso"
        lblCodigoAcceso.Size = New System.Drawing.Size(94, 16)
        lblCodigoAcceso.TabIndex = 12
        lblCodigoAcceso.Text = "Código acceso:"
        '
        'lblClaveAcceso
        '
        lblClaveAcceso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblClaveAcceso.AutoSize = True
        lblClaveAcceso.Location = New System.Drawing.Point(34, 101)
        lblClaveAcceso.Name = "lblClaveAcceso"
        lblClaveAcceso.Size = New System.Drawing.Size(86, 16)
        lblClaveAcceso.TabIndex = 14
        lblClaveAcceso.Text = "Clave acceso:"
        '
        'lblConfirmarClaveAcceso
        '
        lblConfirmarClaveAcceso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblConfirmarClaveAcceso.AutoSize = True
        lblConfirmarClaveAcceso.Location = New System.Drawing.Point(34, 129)
        lblConfirmarClaveAcceso.Name = "lblConfirmarClaveAcceso"
        lblConfirmarClaveAcceso.Size = New System.Drawing.Size(104, 16)
        lblConfirmarClaveAcceso.TabIndex = 16
        lblConfirmarClaveAcceso.Text = "Confirmar Clave:"
        '
        'Label16
        '
        Label16.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(35, 44)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(71, 16)
        Label16.TabIndex = 89
        Label16.Text = "Activar UX:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuStock, Me.mnuMovimientos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1345, 193)
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
        Me.mnuEliminar.Caption = "Desactivar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuStock
        '
        Me.mnuStock.Caption = "Consultar Stock"
        Me.mnuStock.Id = 5
        Me.mnuStock.ImageOptions.SvgImage = CType(resources.GetObject("mnuStock.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuStock.Name = "mnuStock"
        '
        'mnuMovimientos
        '
        Me.mnuMovimientos.Caption = "Consultar Movimientos"
        Me.mnuMovimientos.Id = 6
        Me.mnuMovimientos.ImageOptions.SvgImage = CType(resources.GetObject("mnuMovimientos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuMovimientos.Name = "mnuMovimientos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Propietario"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuStock)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuMovimientos)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 856)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1345, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Label15)
        Me.GroupControl1.Controls.Add(Me.chkEsConsolidador)
        Me.GroupControl1.Controls.Add(Label14)
        Me.GroupControl1.Controls.Add(Me.txtNIT)
        Me.GroupControl1.Controls.Add(Me.Label13)
        Me.GroupControl1.Controls.Add(Me.txtCodigo)
        Me.GroupControl1.Controls.Add(Me.cmbTipoActualizacion)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(lblControlLote)
        Me.GroupControl1.Controls.Add(Me.chkActualizarPrecioOC)
        Me.GroupControl1.Controls.Add(Me.txtTelefono)
        Me.GroupControl1.Controls.Add(Me.btnExaminar)
        Me.GroupControl1.Controls.Add(Me.picFoto)
        Me.GroupControl1.Controls.Add(ImagenLabel)
        Me.GroupControl1.Controls.Add(ContactoLabel)
        Me.GroupControl1.Controls.Add(Me.ContactoTextEdit)
        Me.GroupControl1.Controls.Add(Nombre_comercialLabel)
        Me.GroupControl1.Controls.Add(Me.Nombre_comercialTextEdit)
        Me.GroupControl1.Controls.Add(TelefonoLabel)
        Me.GroupControl1.Controls.Add(DireccionLabel)
        Me.GroupControl1.Controls.Add(Me.DireccionTextEdit)
        Me.GroupControl1.Controls.Add(EmailLabel)
        Me.GroupControl1.Controls.Add(Me.EmailTextEdit)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1679, 759)
        Me.GroupControl1.TabIndex = 0
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Label16)
        Me.GroupControl2.Controls.Add(Me.chkActivarUX)
        Me.GroupControl2.Controls.Add(lblConfirmarClaveAcceso)
        Me.GroupControl2.Controls.Add(Me.txtConfirmarClave)
        Me.GroupControl2.Controls.Add(lblClaveAcceso)
        Me.GroupControl2.Controls.Add(Me.txtClaveAcceso)
        Me.GroupControl2.Controls.Add(lblCodigoAcceso)
        Me.GroupControl2.Controls.Add(Me.txtCodigoAcceso)
        Me.GroupControl2.Location = New System.Drawing.Point(461, 105)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(440, 207)
        Me.GroupControl2.TabIndex = 88
        Me.GroupControl2.Text = "WMSUX"
        '
        'chkActivarUX
        '
        Me.chkActivarUX.Location = New System.Drawing.Point(143, 40)
        Me.chkActivarUX.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivarUX.MenuManager = Me.RibbonControl
        Me.chkActivarUX.Name = "chkActivarUX"
        Me.chkActivarUX.Properties.Caption = ""
        Me.chkActivarUX.Size = New System.Drawing.Size(41, 24)
        Me.chkActivarUX.TabIndex = 90
        '
        'txtConfirmarClave
        '
        Me.txtConfirmarClave.Location = New System.Drawing.Point(143, 125)
        Me.txtConfirmarClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtConfirmarClave.MenuManager = Me.RibbonControl
        Me.txtConfirmarClave.Name = "txtConfirmarClave"
        Me.txtConfirmarClave.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmarClave.Size = New System.Drawing.Size(241, 22)
        Me.txtConfirmarClave.TabIndex = 17
        '
        'txtClaveAcceso
        '
        Me.txtClaveAcceso.Location = New System.Drawing.Point(143, 99)
        Me.txtClaveAcceso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtClaveAcceso.MenuManager = Me.RibbonControl
        Me.txtClaveAcceso.Name = "txtClaveAcceso"
        Me.txtClaveAcceso.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClaveAcceso.Size = New System.Drawing.Size(241, 22)
        Me.txtClaveAcceso.TabIndex = 15
        '
        'txtCodigoAcceso
        '
        Me.txtCodigoAcceso.Location = New System.Drawing.Point(143, 72)
        Me.txtCodigoAcceso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoAcceso.MenuManager = Me.RibbonControl
        Me.txtCodigoAcceso.Name = "txtCodigoAcceso"
        Me.txtCodigoAcceso.Size = New System.Drawing.Size(241, 22)
        Me.txtCodigoAcceso.TabIndex = 13
        '
        'chkEsConsolidador
        '
        Me.chkEsConsolidador.Location = New System.Drawing.Point(566, 62)
        Me.chkEsConsolidador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEsConsolidador.MenuManager = Me.RibbonControl
        Me.chkEsConsolidador.Name = "chkEsConsolidador"
        Me.chkEsConsolidador.Properties.Caption = ""
        Me.chkEsConsolidador.Size = New System.Drawing.Size(41, 24)
        Me.chkEsConsolidador.TabIndex = 87
        '
        'txtNIT
        '
        Me.txtNIT.Location = New System.Drawing.Point(153, 290)
        Me.txtNIT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNIT.MenuManager = Me.RibbonControl
        Me.txtNIT.Name = "txtNIT"
        Me.txtNIT.Size = New System.Drawing.Size(241, 22)
        Me.txtNIT.TabIndex = 85
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(26, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 16)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(154, 66)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(241, 22)
        Me.txtCodigo.TabIndex = 82
        '
        'cmbTipoActualizacion
        '
        Me.cmbTipoActualizacion.Location = New System.Drawing.Point(154, 348)
        Me.cmbTipoActualizacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTipoActualizacion.MenuManager = Me.RibbonControl
        Me.cmbTipoActualizacion.Name = "cmbTipoActualizacion"
        Me.cmbTipoActualizacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoActualizacion.Properties.NullText = ""
        Me.cmbTipoActualizacion.Size = New System.Drawing.Size(241, 22)
        Me.cmbTipoActualizacion.TabIndex = 81
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(154, 98)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(241, 22)
        Me.cmbEmpresa.TabIndex = 80
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(154, 380)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(41, 24)
        Me.chkActivo.TabIndex = 18
        '
        'chkActualizarPrecioOC
        '
        Me.chkActualizarPrecioOC.EditValue = True
        Me.chkActualizarPrecioOC.Location = New System.Drawing.Point(154, 316)
        Me.chkActualizarPrecioOC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActualizarPrecioOC.MenuManager = Me.RibbonControl
        Me.chkActualizarPrecioOC.Name = "chkActualizarPrecioOC"
        Me.chkActualizarPrecioOC.Properties.Caption = ""
        Me.chkActualizarPrecioOC.Size = New System.Drawing.Size(52, 24)
        Me.chkActualizarPrecioOC.TabIndex = 15
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(154, 194)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono.MenuManager = Me.RibbonControl
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono.TabIndex = 9
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(154, 528)
        Me.btnExaminar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(241, 30)
        Me.btnExaminar.TabIndex = 20
        Me.btnExaminar.Text = "Examinar..."
        '
        'picFoto
        '
        Me.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFoto.Location = New System.Drawing.Point(154, 411)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Size = New System.Drawing.Size(241, 110)
        Me.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picFoto.TabIndex = 79
        Me.picFoto.TabStop = False
        '
        'ContactoTextEdit
        '
        Me.ContactoTextEdit.Location = New System.Drawing.Point(154, 130)
        Me.ContactoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ContactoTextEdit.MenuManager = Me.RibbonControl
        Me.ContactoTextEdit.Name = "ContactoTextEdit"
        Me.ContactoTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.ContactoTextEdit.TabIndex = 5
        '
        'Nombre_comercialTextEdit
        '
        Me.Nombre_comercialTextEdit.Location = New System.Drawing.Point(154, 162)
        Me.Nombre_comercialTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Nombre_comercialTextEdit.MenuManager = Me.RibbonControl
        Me.Nombre_comercialTextEdit.Name = "Nombre_comercialTextEdit"
        Me.Nombre_comercialTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.Nombre_comercialTextEdit.TabIndex = 7
        '
        'DireccionTextEdit
        '
        Me.DireccionTextEdit.Location = New System.Drawing.Point(154, 226)
        Me.DireccionTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DireccionTextEdit.MenuManager = Me.RibbonControl
        Me.DireccionTextEdit.Name = "DireccionTextEdit"
        Me.DireccionTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.DireccionTextEdit.TabIndex = 11
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(154, 258)
        Me.EmailTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EmailTextEdit.MenuManager = Me.RibbonControl
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.EmailTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.EmailTextEdit.Size = New System.Drawing.Size(241, 22)
        Me.EmailTextEdit.TabIndex = 13
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(150, 42)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(139, 47)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(139, 15)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(623, 47)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(623, 15)
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
        Me.GrpEmpresaTB.Size = New System.Drawing.Size(1343, 607)
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
        Me.GroupControl3.Size = New System.Drawing.Size(1339, 577)
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
        Me.Grid.MainView = Me.gridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.Grid.Size = New System.Drawing.Size(1335, 547)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsPropietario
        '
        'DsPropietario
        '
        Me.DsPropietario.DataSetName = "DsPropietario"
        Me.DsPropietario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colIdBodega, Me.IdPropietarioBodega, Me.colBodega, Me.IdInterno})
        Me.gridView1.GridControl = Me.Grid
        Me.gridView1.Name = "gridView1"
        '
        'colSeleccion
        '
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "Selección"
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccion.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.OptionsColumn.ReadOnly = True
        '
        'IdPropietarioBodega
        '
        Me.IdPropietarioBodega.Caption = "Asignación"
        Me.IdPropietarioBodega.FieldName = "IdPropietarioBodega"
        Me.IdPropietarioBodega.Name = "IdPropietarioBodega"
        Me.IdPropietarioBodega.OptionsColumn.ReadOnly = True
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.Name = "colBodega"
        Me.colBodega.OptionsColumn.AllowEdit = False
        Me.colBodega.OptionsColumn.ReadOnly = True
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 1
        '
        'IdInterno
        '
        Me.IdInterno.Caption = "IdInterno"
        Me.IdInterno.FieldName = "IdInterno"
        Me.IdInterno.Name = "IdInterno"
        Me.IdInterno.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'txtCorreoElectronico
        '
        Me.txtCorreoElectronico.Controls.Add(Label11)
        Me.txtCorreoElectronico.Controls.Add(Me.chkActivoDest)
        Me.txtCorreoElectronico.Controls.Add(Me.ToolStrip)
        Me.txtCorreoElectronico.Controls.Add(Me.GroupControl4)
        Me.txtCorreoElectronico.Controls.Add(Label1)
        Me.txtCorreoElectronico.Controls.Add(Me.txtCargoDestinatario)
        Me.txtCorreoElectronico.Controls.Add(Me.txtEmailDestinatario)
        Me.txtCorreoElectronico.Controls.Add(Label3)
        Me.txtCorreoElectronico.Controls.Add(Me.txtNombreDestinatario)
        Me.txtCorreoElectronico.Controls.Add(Label4)
        Me.txtCorreoElectronico.Controls.Add(Me.txtApellidoDestinatario)
        Me.txtCorreoElectronico.Controls.Add(Label5)
        Me.txtCorreoElectronico.Controls.Add(Label6)
        Me.txtCorreoElectronico.Controls.Add(Me.txtTelefono1)
        Me.txtCorreoElectronico.Controls.Add(Label7)
        Me.txtCorreoElectronico.Controls.Add(Me.txtTelefono2)
        Me.txtCorreoElectronico.Controls.Add(Me.lblCodigoD)
        Me.txtCorreoElectronico.Controls.Add(Label9)
        Me.txtCorreoElectronico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCorreoElectronico.Location = New System.Drawing.Point(0, 0)
        Me.txtCorreoElectronico.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorreoElectronico.Name = "txtCorreoElectronico"
        Me.txtCorreoElectronico.Size = New System.Drawing.Size(1343, 607)
        Me.txtCorreoElectronico.TabIndex = 0
        '
        'chkActivoDest
        '
        Me.chkActivoDest.EditValue = True
        Me.chkActivoDest.Location = New System.Drawing.Point(156, 271)
        Me.chkActivoDest.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoDest.MenuManager = Me.RibbonControl
        Me.chkActivoDest.Name = "chkActivoDest"
        Me.chkActivoDest.Properties.Caption = ""
        Me.chkActivoDest.Size = New System.Drawing.Size(41, 24)
        Me.chkActivoDest.TabIndex = 16
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPR, Me.cmdGuardar, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1339, 27)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdNewPR
        '
        Me.cmdNewPR.Image = CType(resources.GetObject("cmdNewPR.Image"), System.Drawing.Image)
        Me.cmdNewPR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPR.Name = "cmdNewPR"
        Me.cmdNewPR.Size = New System.Drawing.Size(76, 24)
        Me.cmdNewPR.Text = "Nuevo"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGuardar.Image = Global.TOMWMS.My.Resources.Resources.greencheck
        Me.cmdGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(90, 24)
        Me.cmdGuardar.Text = "Guardar"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(90, 24)
        Me.cmdDelete.Text = "Eliminar"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.grdDetalle)
        Me.GroupControl4.Controls.Add(Label2)
        Me.GroupControl4.Controls.Add(Me.chkDestinatarioActivo)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl4.Location = New System.Drawing.Point(2, 347)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1339, 258)
        Me.GroupControl4.TabIndex = 17
        Me.GroupControl4.Text = "Destinatarios"
        '
        'grdDetalle
        '
        Me.grdDetalle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDetalle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDetalle.Location = New System.Drawing.Point(2, 59)
        Me.grdDetalle.MainView = Me.GridViewDet
        Me.grdDetalle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDetalle.MenuManager = Me.RibbonControl
        Me.grdDetalle.Name = "grdDetalle"
        Me.grdDetalle.Size = New System.Drawing.Size(1335, 197)
        Me.grdDetalle.TabIndex = 2
        Me.grdDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewDet, Me.GridView4})
        '
        'GridViewDet
        '
        Me.GridViewDet.GridControl = Me.grdDetalle
        Me.GridViewDet.Name = "GridViewDet"
        Me.GridViewDet.OptionsBehavior.Editable = False
        Me.GridViewDet.OptionsView.ShowGroupPanel = False
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.grdDetalle
        Me.GridView4.Name = "GridView4"
        '
        'chkDestinatarioActivo
        '
        Me.chkDestinatarioActivo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDestinatarioActivo.EditValue = True
        Me.chkDestinatarioActivo.Location = New System.Drawing.Point(1307, 28)
        Me.chkDestinatarioActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkDestinatarioActivo.MenuManager = Me.RibbonControl
        Me.chkDestinatarioActivo.Name = "chkDestinatarioActivo"
        Me.chkDestinatarioActivo.Properties.Caption = ""
        Me.chkDestinatarioActivo.Size = New System.Drawing.Size(27, 24)
        Me.chkDestinatarioActivo.TabIndex = 1
        '
        'txtCargoDestinatario
        '
        Me.txtCargoDestinatario.Location = New System.Drawing.Point(154, 239)
        Me.txtCargoDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCargoDestinatario.MenuManager = Me.RibbonControl
        Me.txtCargoDestinatario.Name = "txtCargoDestinatario"
        Me.txtCargoDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtCargoDestinatario.TabIndex = 14
        '
        'txtEmailDestinatario
        '
        Me.txtEmailDestinatario.Location = New System.Drawing.Point(154, 151)
        Me.txtEmailDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEmailDestinatario.MenuManager = Me.RibbonControl
        Me.txtEmailDestinatario.Name = "txtEmailDestinatario"
        Me.txtEmailDestinatario.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.txtEmailDestinatario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtEmailDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtEmailDestinatario.TabIndex = 8
        '
        'txtNombreDestinatario
        '
        Me.txtNombreDestinatario.Location = New System.Drawing.Point(154, 94)
        Me.txtNombreDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreDestinatario.MenuManager = Me.RibbonControl
        Me.txtNombreDestinatario.Name = "txtNombreDestinatario"
        Me.txtNombreDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtNombreDestinatario.TabIndex = 4
        '
        'txtApellidoDestinatario
        '
        Me.txtApellidoDestinatario.Location = New System.Drawing.Point(154, 122)
        Me.txtApellidoDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtApellidoDestinatario.MenuManager = Me.RibbonControl
        Me.txtApellidoDestinatario.Name = "txtApellidoDestinatario"
        Me.txtApellidoDestinatario.Size = New System.Drawing.Size(241, 22)
        Me.txtApellidoDestinatario.TabIndex = 6
        '
        'txtTelefono1
        '
        Me.txtTelefono1.Location = New System.Drawing.Point(154, 181)
        Me.txtTelefono1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono1.MenuManager = Me.RibbonControl
        Me.txtTelefono1.Name = "txtTelefono1"
        Me.txtTelefono1.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono1.TabIndex = 10
        '
        'txtTelefono2
        '
        Me.txtTelefono2.Location = New System.Drawing.Point(154, 210)
        Me.txtTelefono2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono2.MenuManager = Me.RibbonControl
        Me.txtTelefono2.Name = "txtTelefono2"
        Me.txtTelefono2.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono2.TabIndex = 12
        '
        'lblCodigoD
        '
        Me.lblCodigoD.AutoSize = True
        Me.lblCodigoD.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigoD.Location = New System.Drawing.Point(153, 69)
        Me.lblCodigoD.Name = "lblCodigoD"
        Me.lblCodigoD.Size = New System.Drawing.Size(14, 17)
        Me.lblCodigoD.TabIndex = 2
        Me.lblCodigoD.Text = "-"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.GroupControl7)
        Me.GroupControl5.Controls.Add(Me.Panel1)
        Me.GroupControl5.Controls.Add(Me.GroupControl6)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1679, 759)
        Me.GroupControl5.TabIndex = 0
        '
        'GroupControl7
        '
        Me.GroupControl7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl7.Controls.Add(Label10)
        Me.GroupControl7.Controls.Add(Me.chkActivoD)
        Me.GroupControl7.Controls.Add(Me.GridDestinatario)
        Me.GroupControl7.Location = New System.Drawing.Point(730, 28)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(943, 729)
        Me.GroupControl7.TabIndex = 2
        Me.GroupControl7.Text = "Destinatarios"
        '
        'chkActivoD
        '
        Me.chkActivoD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoD.EditValue = True
        Me.chkActivoD.Location = New System.Drawing.Point(910, 27)
        Me.chkActivoD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoD.MenuManager = Me.RibbonControl
        Me.chkActivoD.Name = "chkActivoD"
        Me.chkActivoD.Properties.Caption = ""
        Me.chkActivoD.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoD.TabIndex = 1
        '
        'GridDestinatario
        '
        Me.GridDestinatario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridDestinatario.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridDestinatario.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridDestinatario.Location = New System.Drawing.Point(2, 59)
        Me.GridDestinatario.MainView = Me.ViewDestinatario
        Me.GridDestinatario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridDestinatario.MenuManager = Me.RibbonControl
        Me.GridDestinatario.Name = "GridDestinatario"
        Me.GridDestinatario.Size = New System.Drawing.Size(941, 667)
        Me.GridDestinatario.TabIndex = 2
        Me.GridDestinatario.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewDestinatario, Me.GridView6})
        '
        'ViewDestinatario
        '
        Me.ViewDestinatario.GridControl = Me.GridDestinatario
        Me.ViewDestinatario.Name = "ViewDestinatario"
        Me.ViewDestinatario.OptionsBehavior.Editable = False
        Me.ViewDestinatario.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.GridDestinatario
        Me.GridView6.Name = "GridView6"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.cmdAlertas)
        Me.Panel1.Controls.Add(Me.cmdAgregar)
        Me.Panel1.Controls.Add(Me.cmdEliminar)
        Me.Panel1.Location = New System.Drawing.Point(667, 28)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(56, 731)
        Me.Panel1.TabIndex = 1
        '
        'cmdAlertas
        '
        Me.cmdAlertas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAlertas.ImageOptions.SvgImage = CType(resources.GetObject("cmdAlertas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAlertas.Location = New System.Drawing.Point(3, 79)
        Me.cmdAlertas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAlertas.Name = "cmdAlertas"
        Me.cmdAlertas.Size = New System.Drawing.Size(48, 48)
        Me.cmdAlertas.TabIndex = 2
        Me.cmdAlertas.ToolTip = "Envío de mensajes por proceso"
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAgregar.ImageOptions.SvgImage = CType(resources.GetObject("cmdAgregar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAgregar.Location = New System.Drawing.Point(5, 4)
        Me.cmdAgregar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(48, 48)
        Me.cmdAgregar.TabIndex = 0
        Me.cmdAgregar.ToolTip = "Envío de mensajes por excepción"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEliminar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEliminar.Location = New System.Drawing.Point(5, 147)
        Me.cmdEliminar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.Size = New System.Drawing.Size(48, 48)
        Me.cmdEliminar.TabIndex = 1
        '
        'GroupControl6
        '
        Me.GroupControl6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupControl6.Controls.Add(Label54)
        Me.GroupControl6.Controls.Add(Me.chkActivoM)
        Me.GroupControl6.Controls.Add(Me.GridMensaje)
        Me.GroupControl6.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(658, 735)
        Me.GroupControl6.TabIndex = 0
        Me.GroupControl6.Text = "Mensajes"
        '
        'chkActivoM
        '
        Me.chkActivoM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActivoM.EditValue = True
        Me.chkActivoM.Location = New System.Drawing.Point(626, 27)
        Me.chkActivoM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoM.MenuManager = Me.RibbonControl
        Me.chkActivoM.Name = "chkActivoM"
        Me.chkActivoM.Properties.Caption = ""
        Me.chkActivoM.Size = New System.Drawing.Size(21, 24)
        Me.chkActivoM.TabIndex = 1
        '
        'GridMensaje
        '
        Me.GridMensaje.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridMensaje.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridMensaje.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridMensaje.Location = New System.Drawing.Point(3, 59)
        Me.GridMensaje.MainView = Me.ViewMensaje
        Me.GridMensaje.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridMensaje.MenuManager = Me.RibbonControl
        Me.GridMensaje.Name = "GridMensaje"
        Me.GridMensaje.Size = New System.Drawing.Size(653, 682)
        Me.GridMensaje.TabIndex = 2
        Me.GridMensaje.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewMensaje, Me.GridView3})
        '
        'ViewMensaje
        '
        Me.ViewMensaje.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ViewMensaje.Appearance.Row.BackColor2 = System.Drawing.Color.Transparent
        Me.ViewMensaje.Appearance.Row.Options.UseBackColor = True
        Me.ViewMensaje.GridControl = Me.GridMensaje
        Me.ViewMensaje.Name = "ViewMensaje"
        Me.ViewMensaje.OptionsBehavior.Editable = False
        Me.ViewMensaje.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.GridMensaje
        Me.GridView3.Name = "GridView3"
        '
        'dkPropietario
        '
        Me.dkPropietario.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkPropietario.Form = Me
        Me.dkPropietario.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 830)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1345, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("6d41e5f7-f2b1-4761-8391-fc0ad59630be")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 709)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 97)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1345, 121)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 34)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1337, 83)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtraPropietario
        '
        Me.xtraPropietario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtraPropietario.Location = New System.Drawing.Point(0, 193)
        Me.xtraPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.xtraPropietario.Name = "xtraPropietario"
        Me.xtraPropietario.SelectedTabPage = Me.DatosPropietario
        Me.xtraPropietario.Size = New System.Drawing.Size(1345, 637)
        Me.xtraPropietario.TabIndex = 6
        Me.xtraPropietario.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.DatosPropietario, Me.PropietarioBodega, Me.Destinatarios, Me.Reglas, Me.tabUM, Me.TabEstados, Me.TabProductos, Me.TabStock, Me.tabMovimientos})
        '
        'DatosPropietario
        '
        Me.DatosPropietario.Controls.Add(Me.GroupControl1)
        Me.DatosPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DatosPropietario.Name = "DatosPropietario"
        Me.DatosPropietario.Size = New System.Drawing.Size(1343, 607)
        Me.DatosPropietario.Text = "Datos Propietario"
        '
        'PropietarioBodega
        '
        Me.PropietarioBodega.Controls.Add(Me.GrpEmpresaTB)
        Me.PropietarioBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PropietarioBodega.Name = "PropietarioBodega"
        Me.PropietarioBodega.Size = New System.Drawing.Size(1343, 607)
        Me.PropietarioBodega.Text = "Propietario Bodega"
        '
        'Destinatarios
        '
        Me.Destinatarios.Controls.Add(Me.txtCorreoElectronico)
        Me.Destinatarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Destinatarios.Name = "Destinatarios"
        Me.Destinatarios.Size = New System.Drawing.Size(1343, 607)
        Me.Destinatarios.Text = "Destinatarios"
        '
        'Reglas
        '
        Me.Reglas.Controls.Add(Me.GroupControl5)
        Me.Reglas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Reglas.Name = "Reglas"
        Me.Reglas.Size = New System.Drawing.Size(1343, 607)
        Me.Reglas.Text = "Reglas"
        '
        'tabUM
        '
        Me.tabUM.Controls.Add(Me.dgridUM)
        Me.tabUM.Name = "tabUM"
        Me.tabUM.Size = New System.Drawing.Size(1343, 607)
        Me.tabUM.Text = "Unidades de medida"
        '
        'dgridUM
        '
        Me.dgridUM.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridUM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridUM.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridUM.Location = New System.Drawing.Point(0, 0)
        Me.dgridUM.MainView = Me.GridView2
        Me.dgridUM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridUM.MenuManager = Me.RibbonControl
        Me.dgridUM.Name = "dgridUM"
        Me.dgridUM.Size = New System.Drawing.Size(1343, 607)
        Me.dgridUM.TabIndex = 3
        Me.dgridUM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.GridView5})
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.dgridUM
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowFooter = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.dgridUM
        Me.GridView5.Name = "GridView5"
        '
        'TabEstados
        '
        Me.TabEstados.Controls.Add(Me.dgridEstados)
        Me.TabEstados.Name = "TabEstados"
        Me.TabEstados.Size = New System.Drawing.Size(1343, 607)
        Me.TabEstados.Text = "Estados"
        '
        'dgridEstados
        '
        Me.dgridEstados.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridEstados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridEstados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridEstados.Location = New System.Drawing.Point(0, 0)
        Me.dgridEstados.MainView = Me.gvEstados
        Me.dgridEstados.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridEstados.MenuManager = Me.RibbonControl
        Me.dgridEstados.Name = "dgridEstados"
        Me.dgridEstados.Size = New System.Drawing.Size(1343, 607)
        Me.dgridEstados.TabIndex = 4
        Me.dgridEstados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvEstados, Me.GridView8})
        '
        'gvEstados
        '
        Me.gvEstados.GridControl = Me.dgridEstados
        Me.gvEstados.Name = "gvEstados"
        Me.gvEstados.OptionsBehavior.Editable = False
        Me.gvEstados.OptionsView.ShowAutoFilterRow = True
        Me.gvEstados.OptionsView.ShowFooter = True
        Me.gvEstados.OptionsView.ShowGroupPanel = False
        '
        'GridView8
        '
        Me.GridView8.GridControl = Me.dgridEstados
        Me.GridView8.Name = "GridView8"
        '
        'TabProductos
        '
        Me.TabProductos.Controls.Add(Me.dgridProductos)
        Me.TabProductos.Name = "TabProductos"
        Me.TabProductos.Size = New System.Drawing.Size(1343, 607)
        Me.TabProductos.Text = "Productos"
        '
        'dgridProductos
        '
        Me.dgridProductos.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridProductos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridProductos.Location = New System.Drawing.Point(0, 0)
        Me.dgridProductos.MainView = Me.GridView9
        Me.dgridProductos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridProductos.MenuManager = Me.RibbonControl
        Me.dgridProductos.Name = "dgridProductos"
        Me.dgridProductos.Size = New System.Drawing.Size(1343, 607)
        Me.dgridProductos.TabIndex = 5
        Me.dgridProductos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView9, Me.GridView10})
        '
        'GridView9
        '
        Me.GridView9.GridControl = Me.dgridProductos
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsBehavior.Editable = False
        Me.GridView9.OptionsView.ShowAutoFilterRow = True
        Me.GridView9.OptionsView.ShowFooter = True
        Me.GridView9.OptionsView.ShowGroupPanel = False
        '
        'GridView10
        '
        Me.GridView10.GridControl = Me.dgridProductos
        Me.GridView10.Name = "GridView10"
        '
        'TabStock
        '
        Me.TabStock.Controls.Add(Me.dgridStock)
        Me.TabStock.Name = "TabStock"
        Me.TabStock.Size = New System.Drawing.Size(1343, 607)
        Me.TabStock.Text = "Stock"
        '
        'dgridStock
        '
        Me.dgridStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridStock.Location = New System.Drawing.Point(0, 0)
        Me.dgridStock.MainView = Me.grdvStock
        Me.dgridStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridStock.MenuManager = Me.RibbonControl
        Me.dgridStock.Name = "dgridStock"
        Me.dgridStock.Size = New System.Drawing.Size(1343, 607)
        Me.dgridStock.TabIndex = 6
        Me.dgridStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvStock, Me.GridView12})
        '
        'grdvStock
        '
        Me.grdvStock.GridControl = Me.dgridStock
        Me.grdvStock.Name = "grdvStock"
        Me.grdvStock.OptionsBehavior.Editable = False
        Me.grdvStock.OptionsView.ColumnAutoWidth = False
        Me.grdvStock.OptionsView.ShowAutoFilterRow = True
        Me.grdvStock.OptionsView.ShowFooter = True
        Me.grdvStock.OptionsView.ShowGroupPanel = False
        '
        'GridView12
        '
        Me.GridView12.GridControl = Me.dgridStock
        Me.GridView12.Name = "GridView12"
        '
        'tabMovimientos
        '
        Me.tabMovimientos.Controls.Add(Me.dgridMovimientos)
        Me.tabMovimientos.Name = "tabMovimientos"
        Me.tabMovimientos.Size = New System.Drawing.Size(1343, 607)
        Me.tabMovimientos.Text = "Movimientos"
        '
        'dgridMovimientos
        '
        Me.dgridMovimientos.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridMovimientos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridMovimientos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridMovimientos.Location = New System.Drawing.Point(0, 0)
        Me.dgridMovimientos.MainView = Me.gviewMovimientos
        Me.dgridMovimientos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridMovimientos.MenuManager = Me.RibbonControl
        Me.dgridMovimientos.Name = "dgridMovimientos"
        Me.dgridMovimientos.Size = New System.Drawing.Size(1343, 607)
        Me.dgridMovimientos.TabIndex = 7
        Me.dgridMovimientos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewMovimientos, Me.GridView14})
        '
        'gviewMovimientos
        '
        Me.gviewMovimientos.GridControl = Me.dgridMovimientos
        Me.gviewMovimientos.Name = "gviewMovimientos"
        Me.gviewMovimientos.OptionsBehavior.Editable = False
        Me.gviewMovimientos.OptionsView.ColumnAutoWidth = False
        Me.gviewMovimientos.OptionsView.ShowAutoFilterRow = True
        Me.gviewMovimientos.OptionsView.ShowFooter = True
        Me.gviewMovimientos.OptionsView.ShowGroupPanel = False
        '
        'GridView14
        '
        Me.GridView14.GridControl = Me.dgridMovimientos
        Me.GridView14.Name = "GridView14"
        '
        'frmPropietario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1345, 886)
        Me.Controls.Add(Me.xtraPropietario)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmPropietario"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Propietario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.chkActivarUX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConfirmarClave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClaveAcceso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoAcceso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsConsolidador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNIT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoActualizacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActualizarPrecioOC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Nombre_comercialTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpEmpresaTB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEmpresaTB.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPropietario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreoElectronico, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtCorreoElectronico.ResumeLayout(False)
        Me.txtCorreoElectronico.PerformLayout()
        CType(Me.chkActivoDest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDestinatarioActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCargoDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApellidoDestinatario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        Me.GroupControl7.PerformLayout()
        CType(Me.chkActivoD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridDestinatario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewDestinatario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        Me.GroupControl6.PerformLayout()
        CType(Me.chkActivoM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridMensaje, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewMensaje, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkPropietario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtraPropietario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraPropietario.ResumeLayout(False)
        Me.DatosPropietario.ResumeLayout(False)
        Me.PropietarioBodega.ResumeLayout(False)
        Me.Destinatarios.ResumeLayout(False)
        Me.Reglas.ResumeLayout(False)
        Me.tabUM.ResumeLayout(False)
        CType(Me.dgridUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabEstados.ResumeLayout(False)
        CType(Me.dgridEstados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEstados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabProductos.ResumeLayout(False)
        CType(Me.dgridProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabStock.ResumeLayout(False)
        CType(Me.dgridStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMovimientos.ResumeLayout(False)
        CType(Me.dgridMovimientos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gviewMovimientos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents GrpEmpresaTB As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdPropietarioBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnExaminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContactoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Nombre_comercialTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DireccionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsPropietario As TOMWMS.DsPropietario
    Friend WithEvents chkActualizarPrecioOC As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents picFoto As System.Windows.Forms.PictureBox
    Friend WithEvents txtCorreoElectronico As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCargoDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEmailDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtApellidoDestinatario As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigoD As Label
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewDet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkDestinatarioActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridMensaje As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridDestinatario As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewDestinatario As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmdAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdEliminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkActivoM As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivoD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdGuardar As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents ViewMensaje As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkActivoDest As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dkPropietario As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtraPropietario As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents DatosPropietario As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PropietarioBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Destinatarios As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Reglas As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoActualizacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label13 As Label
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNIT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdNewPR As ToolStripButton
    Friend WithEvents tabUM As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabEstados As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabProductos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TabStock As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabMovimientos As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgridUM As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridEstados As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvEstados As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridProductos As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvStock As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView12 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dgridMovimientos As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewMovimientos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView14 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuStock As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuMovimientos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkEsConsolidador As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtConfirmarClave As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtClaveAcceso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoAcceso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivarUX As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmdAlertas As DevExpress.XtraEditors.SimpleButton
End Class
