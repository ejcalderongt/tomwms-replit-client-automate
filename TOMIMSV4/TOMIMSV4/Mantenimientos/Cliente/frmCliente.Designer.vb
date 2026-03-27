<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCliente
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
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim Nombre_comercialLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim lblNit As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim lblContacto As System.Windows.Forms.Label
        Dim lblDireccion As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim IdDireccionLabel As System.Windows.Forms.Label
        Dim IdMunicipioLabel As System.Windows.Forms.Label
        Dim AvenidaLabel As System.Windows.Forms.Label
        Dim CalleLabel As System.Windows.Forms.Label
        Dim No_CasaLabel As System.Windows.Forms.Label
        Dim ZonaLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim RegionLabel As System.Windows.Forms.Label
        Dim ReferenciaLabel As System.Windows.Forms.Label
        Dim Coordenada_xLabel As System.Windows.Forms.Label
        Dim Coordenada_yLabel As System.Windows.Forms.Label
        Dim LocalLabel As System.Windows.Forms.Label
        Dim lblDepartamento As System.Windows.Forms.Label
        Dim lblPais As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim lblReferencia As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim lblIdClienteLote As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim lblchkBloquear As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCliente))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuMapa = New DevExpress.XtraBars.BarButtonItem()
        Me.chkEsBodRecep = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkControlUltimoLote = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkControlCalidad = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkEsBodegaTraslado = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkSistema = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkManufactura = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkEsProveedor = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GrpCliente = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdProductoEstadoDefecto = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbBodegaAreaSAP = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtIdUbicacionAbastecerCon = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lcmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbBodegaWMS = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblBodegaWMS = New System.Windows.Forms.Label()
        Me.txtReferenciaCliente = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTipoCliente = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEmpresa = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtContacto = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreComercial = New DevExpress.XtraEditors.TextEdit()
        Me.txtNit = New DevExpress.XtraEditors.TextEdit()
        Me.txtCorreo = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTiempoAceptacion = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdGuardarTodos = New System.Windows.Forms.Button()
        Me.cmdGuardar = New System.Windows.Forms.Button()
        Me.txtDiaExterior = New System.Windows.Forms.NumericUpDown()
        Me.txtDiaLocal = New System.Windows.Forms.NumericUpDown()
        Me.lnkFamilia = New System.Windows.Forms.LinkLabel()
        Me.txtIdFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreFamilia = New DevExpress.XtraEditors.TextEdit()
        Me.lnkClasificacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreClasificacion = New DevExpress.XtraEditors.TextEdit()
        Me.GrpTiempo = New DevExpress.XtraEditors.GroupControl()
        Me.GridTiempo = New DevExpress.XtraGrid.GridControl()
        Me.GridViewTiempo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GrpClienteBodega = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsCliente = New TOMWMS.DsCliente()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSelección = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdAsignacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdInterno = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdAreaDestino = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AreaBodegaGridLookUpEdit = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cmdAgregarDireccion = New System.Windows.Forms.Button()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.dgridDirecciones = New DevExpress.XtraGrid.GridControl()
        Me.gvDireccionesCli = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbRegion = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbMunicipio = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbDepartamento = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPais = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtDireccionEntrega = New System.Windows.Forms.TextBox()
        Me.chkLocal = New DevExpress.XtraEditors.CheckEdit()
        Me.txtIdDireccion = New DevExpress.XtraEditors.SpinEdit()
        Me.txtAvenida = New DevExpress.XtraEditors.TextEdit()
        Me.txtCalle = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoCasa = New DevExpress.XtraEditors.TextEdit()
        Me.txtZona = New DevExpress.XtraEditors.TextEdit()
        Me.txtReferenciaDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtCordX = New DevExpress.XtraEditors.TextEdit()
        Me.txtCordY = New DevExpress.XtraEditors.TextEdit()
        Me.Cliente_direccionTableAdapter = New TOMWMS.cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter()
        Me.TableAdapterManager = New TOMWMS.cliente_direccion_dsetTableAdapters.TableAdapterManager()
        Me.dkCliente = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtrtabClientes = New DevExpress.XtraTab.XtraTabControl()
        Me.datosCliente = New DevExpress.XtraTab.XtraTabPage()
        Me.tiempoaceptacion = New DevExpress.XtraTab.XtraTabPage()
        Me.clienteBodega = New DevExpress.XtraTab.XtraTabPage()
        Me.DireccionesEntrega = New DevExpress.XtraTab.XtraTabPage()
        Me.ToolStripPR = New System.Windows.Forms.ToolStrip()
        Me.cmdNewPR = New System.Windows.Forms.ToolStripButton()
        Me.cmdDesactivarPresentacion = New System.Windows.Forms.ToolStripButton()
        Me.tabLotes = New DevExpress.XtraTab.XtraTabPage()
        Me.grpMantLotes = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdClienteLote = New DevExpress.XtraEditors.TextEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.mnuNuevoLote = New System.Windows.Forms.ToolStripButton()
        Me.mnuGuardarLote = New System.Windows.Forms.ToolStripButton()
        Me.mnuEliminarLote = New System.Windows.Forms.ToolStripButton()
        Me.chkLoteActivo = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkBloquear = New DevExpress.XtraEditors.ToggleSwitch()
        Me.dtpFechaAgregoLote = New DevExpress.XtraEditors.DateEdit()
        Me.txtUsuarioAgregoLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtUsuarioModificoLote = New DevExpress.XtraEditors.TextEdit()
        Me.dtpFechaModificoLote = New DevExpress.XtraEditors.DateEdit()
        Me.cmbEstadoProducto = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbLote = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpLotesPermitidos = New DevExpress.XtraEditors.GroupControl()
        Me.dgridLotesPermitidos = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grpLotesBloqueados = New DevExpress.XtraEditors.GroupControl()
        Me.DgridLotesBloqueados = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        Nombre_comercialLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        lblNit = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        lblContacto = New System.Windows.Forms.Label()
        lblDireccion = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        IdDireccionLabel = New System.Windows.Forms.Label()
        IdMunicipioLabel = New System.Windows.Forms.Label()
        AvenidaLabel = New System.Windows.Forms.Label()
        CalleLabel = New System.Windows.Forms.Label()
        No_CasaLabel = New System.Windows.Forms.Label()
        ZonaLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        RegionLabel = New System.Windows.Forms.Label()
        ReferenciaLabel = New System.Windows.Forms.Label()
        Coordenada_xLabel = New System.Windows.Forms.Label()
        Coordenada_yLabel = New System.Windows.Forms.Label()
        LocalLabel = New System.Windows.Forms.Label()
        lblDepartamento = New System.Windows.Forms.Label()
        lblPais = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        lblReferencia = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        lblIdClienteLote = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        lblchkBloquear = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCliente.SuspendLayout()
        CType(Me.txtIdProductoEstadoDefecto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodegaAreaSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionAbastecerCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferenciaCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContacto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTiempoAceptacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTiempoAceptacion.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtDiaExterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiaLocal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreFamilia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreClasificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTiempo.SuspendLayout()
        CType(Me.GridTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridViewTiempo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpClienteBodega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpClienteBodega.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AreaBodegaGridLookUpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dgridDirecciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDireccionesCli, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbRegion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMunicipio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDepartamento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAvenida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCalle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoCasa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtZona.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferenciaDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCordX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCordY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtrtabClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtrtabClientes.SuspendLayout()
        Me.datosCliente.SuspendLayout()
        Me.tiempoaceptacion.SuspendLayout()
        Me.clienteBodega.SuspendLayout()
        Me.DireccionesEntrega.SuspendLayout()
        Me.ToolStripPR.SuspendLayout()
        Me.tabLotes.SuspendLayout()
        CType(Me.grpMantLotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMantLotes.SuspendLayout()
        CType(Me.txtIdClienteLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.chkLoteActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBloquear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAgregoLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaAgregoLote.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsuarioAgregoLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUsuarioModificoLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaModificoLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaModificoLote.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstadoProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLotesPermitidos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLotesPermitidos.SuspendLayout()
        CType(Me.dgridLotesPermitidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLotesBloqueados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLotesBloqueados.SuspendLayout()
        CType(Me.DgridLotesBloqueados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(38, 98)
        IdEmpresaLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa:"
        '
        'CodigoLabel
        '
        CodigoLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(38, 202)
        CodigoLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(51, 16)
        CodigoLabel.TabIndex = 13
        CodigoLabel.Text = "Código:"
        '
        'Nombre_comercialLabel
        '
        Nombre_comercialLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Nombre_comercialLabel.AutoSize = True
        Nombre_comercialLabel.Location = New System.Drawing.Point(38, 236)
        Nombre_comercialLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Nombre_comercialLabel.Name = "Nombre_comercialLabel"
        Nombre_comercialLabel.Size = New System.Drawing.Size(118, 16)
        Nombre_comercialLabel.TabIndex = 16
        Nombre_comercialLabel.Text = "Nombre Comercial:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(38, 270)
        TelefonoLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(62, 16)
        TelefonoLabel.TabIndex = 21
        TelefonoLabel.Text = "Telefono:"
        '
        'lblNit
        '
        lblNit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblNit.AutoSize = True
        lblNit.Location = New System.Drawing.Point(38, 372)
        lblNit.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblNit.Name = "lblNit"
        lblNit.Size = New System.Drawing.Size(27, 16)
        lblNit.TabIndex = 29
        lblNit.Text = "Nit:"
        '
        'EmailLabel
        '
        EmailLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(38, 406)
        EmailLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(117, 16)
        EmailLabel.TabIndex = 31
        EmailLabel.Text = "Correo Electrónico:"
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(38, 69)
        Label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(19, 16)
        Label12.TabIndex = 0
        Label12.Text = "ID"
        '
        'Label1
        '
        Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(38, 130)
        Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 16)
        Label1.TabIndex = 5
        Label1.Text = "Propietario:"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(38, 162)
        Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(80, 16)
        Label2.TabIndex = 8
        Label2.Text = "Tipo Cliente:"
        '
        'lblContacto
        '
        lblContacto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblContacto.AutoSize = True
        lblContacto.Location = New System.Drawing.Point(38, 338)
        lblContacto.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblContacto.Name = "lblContacto"
        lblContacto.Size = New System.Drawing.Size(62, 16)
        lblContacto.TabIndex = 27
        lblContacto.Text = "Contacto:"
        '
        'lblDireccion
        '
        lblDireccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblDireccion.AutoSize = True
        lblDireccion.Location = New System.Drawing.Point(38, 304)
        lblDireccion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblDireccion.Name = "lblDireccion"
        lblDireccion.Size = New System.Drawing.Size(64, 16)
        lblDireccion.TabIndex = 24
        lblDireccion.Text = "Dirección:"
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(90, 103)
        Label10.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(82, 16)
        Label10.TabIndex = 8
        Label10.Text = "Días Locales:"
        '
        'Label11
        '
        Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(90, 138)
        Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(97, 16)
        Label11.TabIndex = 11
        Label11.Text = "Días Exteriores:"
        '
        'IdDireccionLabel
        '
        IdDireccionLabel.AutoSize = True
        IdDireccionLabel.Location = New System.Drawing.Point(44, 39)
        IdDireccionLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        IdDireccionLabel.Name = "IdDireccionLabel"
        IdDireccionLabel.Size = New System.Drawing.Size(74, 16)
        IdDireccionLabel.TabIndex = 0
        IdDireccionLabel.Text = "Correlativo:"
        '
        'IdMunicipioLabel
        '
        IdMunicipioLabel.AutoSize = True
        IdMunicipioLabel.Location = New System.Drawing.Point(42, 144)
        IdMunicipioLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        IdMunicipioLabel.Name = "IdMunicipioLabel"
        IdMunicipioLabel.Size = New System.Drawing.Size(65, 16)
        IdMunicipioLabel.TabIndex = 6
        IdMunicipioLabel.Text = "Municipio:"
        '
        'AvenidaLabel
        '
        AvenidaLabel.AutoSize = True
        AvenidaLabel.Location = New System.Drawing.Point(42, 174)
        AvenidaLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        AvenidaLabel.Name = "AvenidaLabel"
        AvenidaLabel.Size = New System.Drawing.Size(57, 16)
        AvenidaLabel.TabIndex = 8
        AvenidaLabel.Text = "Avenida:"
        '
        'CalleLabel
        '
        CalleLabel.AutoSize = True
        CalleLabel.Location = New System.Drawing.Point(42, 207)
        CalleLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        CalleLabel.Name = "CalleLabel"
        CalleLabel.Size = New System.Drawing.Size(40, 16)
        CalleLabel.TabIndex = 10
        CalleLabel.Text = "Calle:"
        '
        'No_CasaLabel
        '
        No_CasaLabel.AutoSize = True
        No_CasaLabel.Location = New System.Drawing.Point(42, 241)
        No_CasaLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        No_CasaLabel.Name = "No_CasaLabel"
        No_CasaLabel.Size = New System.Drawing.Size(59, 16)
        No_CasaLabel.TabIndex = 12
        No_CasaLabel.Text = "No Casa:"
        '
        'ZonaLabel
        '
        ZonaLabel.AutoSize = True
        ZonaLabel.Location = New System.Drawing.Point(42, 274)
        ZonaLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        ZonaLabel.Name = "ZonaLabel"
        ZonaLabel.Size = New System.Drawing.Size(40, 16)
        ZonaLabel.TabIndex = 14
        ZonaLabel.Text = "Zona:"
        '
        'DireccionLabel
        '
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(44, 686)
        DireccionLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(64, 16)
        DireccionLabel.TabIndex = 27
        DireccionLabel.Text = "Direccion:"
        '
        'RegionLabel
        '
        RegionLabel.AutoSize = True
        RegionLabel.Location = New System.Drawing.Point(42, 309)
        RegionLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        RegionLabel.Name = "RegionLabel"
        RegionLabel.Size = New System.Drawing.Size(51, 16)
        RegionLabel.TabIndex = 16
        RegionLabel.Text = "Region:"
        '
        'ReferenciaLabel
        '
        ReferenciaLabel.AutoSize = True
        ReferenciaLabel.Location = New System.Drawing.Point(42, 343)
        ReferenciaLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        ReferenciaLabel.Name = "ReferenciaLabel"
        ReferenciaLabel.Size = New System.Drawing.Size(73, 16)
        ReferenciaLabel.TabIndex = 18
        ReferenciaLabel.Text = "Referencia:"
        '
        'Coordenada_xLabel
        '
        Coordenada_xLabel.AutoSize = True
        Coordenada_xLabel.Location = New System.Drawing.Point(42, 377)
        Coordenada_xLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Coordenada_xLabel.Name = "Coordenada_xLabel"
        Coordenada_xLabel.Size = New System.Drawing.Size(50, 16)
        Coordenada_xLabel.TabIndex = 20
        Coordenada_xLabel.Text = "Latitud:"
        '
        'Coordenada_yLabel
        '
        Coordenada_yLabel.AutoSize = True
        Coordenada_yLabel.Location = New System.Drawing.Point(42, 411)
        Coordenada_yLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Coordenada_yLabel.Name = "Coordenada_yLabel"
        Coordenada_yLabel.Size = New System.Drawing.Size(60, 16)
        Coordenada_yLabel.TabIndex = 22
        Coordenada_yLabel.Text = "Longitud:"
        '
        'LocalLabel
        '
        LocalLabel.AutoSize = True
        LocalLabel.Location = New System.Drawing.Point(43, 446)
        LocalLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        LocalLabel.Name = "LocalLabel"
        LocalLabel.Size = New System.Drawing.Size(41, 16)
        LocalLabel.TabIndex = 24
        LocalLabel.Text = "Local:"
        '
        'lblDepartamento
        '
        lblDepartamento.AutoSize = True
        lblDepartamento.Location = New System.Drawing.Point(44, 111)
        lblDepartamento.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblDepartamento.Name = "lblDepartamento"
        lblDepartamento.Size = New System.Drawing.Size(93, 16)
        lblDepartamento.TabIndex = 4
        lblDepartamento.Text = "Departamento:"
        '
        'lblPais
        '
        lblPais.AutoSize = True
        lblPais.Location = New System.Drawing.Point(44, 75)
        lblPais.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblPais.Name = "lblPais"
        lblPais.Size = New System.Drawing.Size(35, 16)
        lblPais.TabIndex = 2
        lblPais.Text = "País:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.Red
        Label3.Location = New System.Drawing.Point(14, 686)
        Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(21, 21)
        Label3.TabIndex = 26
        Label3.Text = "*"
        '
        'Label4
        '
        Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.Red
        Label4.Location = New System.Drawing.Point(239, 98)
        Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(21, 21)
        Label4.TabIndex = 3
        Label4.Text = "*"
        '
        'Label5
        '
        Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.Red
        Label5.Location = New System.Drawing.Point(239, 133)
        Label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(21, 21)
        Label5.TabIndex = 6
        Label5.Text = "*"
        '
        'Label6
        '
        Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.Red
        Label6.Location = New System.Drawing.Point(239, 164)
        Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(21, 21)
        Label6.TabIndex = 9
        Label6.Text = "*"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.Color.Red
        Label7.Location = New System.Drawing.Point(239, 199)
        Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(21, 21)
        Label7.TabIndex = 14
        Label7.Text = "*"
        '
        'Label8
        '
        Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.ForeColor = System.Drawing.Color.Red
        Label8.Location = New System.Drawing.Point(239, 234)
        Label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(21, 21)
        Label8.TabIndex = 17
        Label8.Text = "*"
        '
        'Label14
        '
        Label14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label14.AutoSize = True
        Label14.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label14.ForeColor = System.Drawing.Color.Red
        Label14.Location = New System.Drawing.Point(245, 32)
        Label14.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(21, 21)
        Label14.TabIndex = 1
        Label14.Text = "*"
        '
        'Label15
        '
        Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label15.ForeColor = System.Drawing.Color.Red
        Label15.Location = New System.Drawing.Point(245, 71)
        Label15.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(21, 21)
        Label15.TabIndex = 5
        Label15.Text = "*"
        '
        'Label16
        '
        Label16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label16.AutoSize = True
        Label16.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label16.ForeColor = System.Drawing.Color.Red
        Label16.Location = New System.Drawing.Point(245, 103)
        Label16.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(21, 21)
        Label16.TabIndex = 9
        Label16.Text = "*"
        '
        'Label17
        '
        Label17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label17.AutoSize = True
        Label17.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label17.ForeColor = System.Drawing.Color.Red
        Label17.Location = New System.Drawing.Point(245, 138)
        Label17.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(21, 21)
        Label17.TabIndex = 12
        Label17.Text = "*"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(28, 39)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(28, 7)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
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
        Fec_modLabel.Location = New System.Drawing.Point(504, 39)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(504, 7)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 2
        User_modLabel.Text = "Usuario Modificó:"
        '
        'lblReferencia
        '
        lblReferencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblReferencia.AutoSize = True
        lblReferencia.Location = New System.Drawing.Point(42, 105)
        lblReferencia.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblReferencia.Name = "lblReferencia"
        lblReferencia.Size = New System.Drawing.Size(99, 16)
        lblReferencia.TabIndex = 20
        lblReferencia.Text = "Referencia ERP:"
        '
        'Label9
        '
        Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(38, 473)
        Label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(174, 16)
        Label9.TabIndex = 81
        Label9.Text = "Ubicación de abastecimiento:"
        '
        'Label13
        '
        Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(38, 439)
        Label13.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(122, 16)
        Label13.TabIndex = 83
        Label13.Text = "Área (Bodega SAP):"
        '
        'lblIdClienteLote
        '
        lblIdClienteLote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblIdClienteLote.AutoSize = True
        lblIdClienteLote.Location = New System.Drawing.Point(39, 107)
        lblIdClienteLote.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblIdClienteLote.Name = "lblIdClienteLote"
        lblIdClienteLote.Size = New System.Drawing.Size(74, 16)
        lblIdClienteLote.TabIndex = 99
        lblIdClienteLote.Text = "Correlativo:"
        '
        'Label25
        '
        Label25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label25.AutoSize = True
        Label25.Location = New System.Drawing.Point(249, 217)
        Label25.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(46, 16)
        Label25.TabIndex = 97
        Label25.Text = "Activo:"
        '
        'lblchkBloquear
        '
        lblchkBloquear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblchkBloquear.AutoSize = True
        lblchkBloquear.Location = New System.Drawing.Point(249, 187)
        lblchkBloquear.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblchkBloquear.Name = "lblchkBloquear"
        lblchkBloquear.Size = New System.Drawing.Size(62, 16)
        lblchkBloquear.TabIndex = 95
        lblchkBloquear.Text = "Bloquear:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(39, 283)
        Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(91, 16)
        Label20.TabIndex = 90
        Label20.Text = "Fecha Agregó:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(39, 319)
        Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(106, 16)
        Label21.TabIndex = 88
        Label21.Text = "Usuario Modificó:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(39, 251)
        Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(100, 16)
        Label22.TabIndex = 86
        Label22.Text = "Usuario Agregó:"
        '
        'Label23
        '
        Label23.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label23.AutoSize = True
        Label23.Location = New System.Drawing.Point(39, 351)
        Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(97, 16)
        Label23.TabIndex = 92
        Label23.Text = "Fecha Modificó:"
        '
        'Label18
        '
        Label18.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(39, 133)
        Label18.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(36, 16)
        Label18.TabIndex = 83
        Label18.Text = "Lote:"
        '
        'Label19
        '
        Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(39, 159)
        Label19.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(50, 16)
        Label19.TabIndex = 85
        Label19.Text = "Estado:"
        '
        'Label24
        '
        Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(38, 507)
        Label24.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(212, 16)
        Label24.TabIndex = 85
        Label24.Text = "Estado producto de abastecimiento:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.mnuMapa, Me.chkEsBodRecep, Me.chkControlUltimoLote, Me.chkControlCalidad, Me.chkEsBodegaTraslado, Me.chkSistema, Me.chkManufactura, Me.chkActivo, Me.chkEsProveedor})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonControl.MaxItemId = 14
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1699, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'mnuMapa
        '
        Me.mnuMapa.Caption = "Posición"
        Me.mnuMapa.Id = 5
        Me.mnuMapa.ImageOptions.SvgImage = CType(resources.GetObject("mnuMapa.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuMapa.Name = "mnuMapa"
        '
        'chkEsBodRecep
        '
        Me.chkEsBodRecep.Caption = "Bodega Recepción:"
        Me.chkEsBodRecep.Id = 6
        Me.chkEsBodRecep.Name = "chkEsBodRecep"
        '
        'chkControlUltimoLote
        '
        Me.chkControlUltimoLote.Caption = "Control último lote:"
        Me.chkControlUltimoLote.Id = 7
        Me.chkControlUltimoLote.Name = "chkControlUltimoLote"
        '
        'chkControlCalidad
        '
        Me.chkControlCalidad.Caption = "Control Calidad:"
        Me.chkControlCalidad.Id = 8
        Me.chkControlCalidad.Name = "chkControlCalidad"
        '
        'chkEsBodegaTraslado
        '
        Me.chkEsBodegaTraslado.Caption = "Bodega Traslado:"
        Me.chkEsBodegaTraslado.Id = 9
        Me.chkEsBodegaTraslado.Name = "chkEsBodegaTraslado"
        '
        'chkSistema
        '
        Me.chkSistema.Caption = "Sistema"
        Me.chkSistema.Id = 10
        Me.chkSistema.Name = "chkSistema"
        '
        'chkManufactura
        '
        Me.chkManufactura.Caption = "Realiza Manufactura:"
        Me.chkManufactura.Id = 11
        Me.chkManufactura.Name = "chkManufactura"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 12
        Me.chkActivo.Name = "chkActivo"
        '
        'chkEsProveedor
        '
        Me.chkEsProveedor.Caption = "Es proveedor"
        Me.chkEsProveedor.Id = 13
        Me.chkEsProveedor.Name = "chkEsProveedor"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Cliente"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuMapa)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkSistema)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkEsBodRecep)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkEsBodegaTraslado)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkControlCalidad)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkControlUltimoLote)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkManufactura)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkEsProveedor)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Parámetros"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 852)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1699, 30)
        '
        'GrpCliente
        '
        Me.GrpCliente.Controls.Add(Label24)
        Me.GrpCliente.Controls.Add(Me.txtIdProductoEstadoDefecto)
        Me.GrpCliente.Controls.Add(Label13)
        Me.GrpCliente.Controls.Add(Me.cmbBodegaAreaSAP)
        Me.GrpCliente.Controls.Add(Label9)
        Me.GrpCliente.Controls.Add(Me.txtIdUbicacionAbastecerCon)
        Me.GrpCliente.Controls.Add(Me.lcmbPropietario)
        Me.GrpCliente.Controls.Add(Me.GroupBox3)
        Me.GrpCliente.Controls.Add(Me.lblCodigo)
        Me.GrpCliente.Controls.Add(Me.cmbTipoCliente)
        Me.GrpCliente.Controls.Add(Me.cmbEmpresa)
        Me.GrpCliente.Controls.Add(Label8)
        Me.GrpCliente.Controls.Add(Label7)
        Me.GrpCliente.Controls.Add(Label6)
        Me.GrpCliente.Controls.Add(Label5)
        Me.GrpCliente.Controls.Add(Label4)
        Me.GrpCliente.Controls.Add(lblDireccion)
        Me.GrpCliente.Controls.Add(Me.txtDireccion)
        Me.GrpCliente.Controls.Add(Me.txtContacto)
        Me.GrpCliente.Controls.Add(lblContacto)
        Me.GrpCliente.Controls.Add(Label2)
        Me.GrpCliente.Controls.Add(Label1)
        Me.GrpCliente.Controls.Add(Label12)
        Me.GrpCliente.Controls.Add(Me.txtTelefono)
        Me.GrpCliente.Controls.Add(IdEmpresaLabel)
        Me.GrpCliente.Controls.Add(CodigoLabel)
        Me.GrpCliente.Controls.Add(Me.txtCodigo)
        Me.GrpCliente.Controls.Add(Nombre_comercialLabel)
        Me.GrpCliente.Controls.Add(Me.txtNombreComercial)
        Me.GrpCliente.Controls.Add(TelefonoLabel)
        Me.GrpCliente.Controls.Add(lblNit)
        Me.GrpCliente.Controls.Add(Me.txtNit)
        Me.GrpCliente.Controls.Add(EmailLabel)
        Me.GrpCliente.Controls.Add(Me.txtCorreo)
        Me.GrpCliente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpCliente.Location = New System.Drawing.Point(0, 0)
        Me.GrpCliente.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpCliente.Name = "GrpCliente"
        Me.GrpCliente.Size = New System.Drawing.Size(1697, 603)
        Me.GrpCliente.TabIndex = 0
        '
        'txtIdProductoEstadoDefecto
        '
        Me.txtIdProductoEstadoDefecto.Location = New System.Drawing.Point(268, 504)
        Me.txtIdProductoEstadoDefecto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.txtIdProductoEstadoDefecto.MenuManager = Me.RibbonControl
        Me.txtIdProductoEstadoDefecto.Name = "txtIdProductoEstadoDefecto"
        Me.txtIdProductoEstadoDefecto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdProductoEstadoDefecto.Properties.PopupView = Me.GridView7
        Me.txtIdProductoEstadoDefecto.Size = New System.Drawing.Size(376, 22)
        Me.txtIdProductoEstadoDefecto.TabIndex = 84
        Me.txtIdProductoEstadoDefecto.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridView7
        '
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowAutoFilterRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'cmbBodegaAreaSAP
        '
        Me.cmbBodegaAreaSAP.Location = New System.Drawing.Point(268, 436)
        Me.cmbBodegaAreaSAP.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbBodegaAreaSAP.MenuManager = Me.RibbonControl
        Me.cmbBodegaAreaSAP.Name = "cmbBodegaAreaSAP"
        Me.cmbBodegaAreaSAP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaAreaSAP.Properties.PopupView = Me.GridView2
        Me.cmbBodegaAreaSAP.Size = New System.Drawing.Size(376, 22)
        Me.cmbBodegaAreaSAP.TabIndex = 82
        Me.cmbBodegaAreaSAP.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'txtIdUbicacionAbastecerCon
        '
        Me.txtIdUbicacionAbastecerCon.Location = New System.Drawing.Point(268, 470)
        Me.txtIdUbicacionAbastecerCon.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.txtIdUbicacionAbastecerCon.MenuManager = Me.RibbonControl
        Me.txtIdUbicacionAbastecerCon.Name = "txtIdUbicacionAbastecerCon"
        Me.txtIdUbicacionAbastecerCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdUbicacionAbastecerCon.Properties.PopupView = Me.GridLookUpEdit1View
        Me.txtIdUbicacionAbastecerCon.Size = New System.Drawing.Size(376, 22)
        Me.txtIdUbicacionAbastecerCon.TabIndex = 80
        Me.txtIdUbicacionAbastecerCon.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'lcmbPropietario
        '
        Me.lcmbPropietario.Location = New System.Drawing.Point(268, 130)
        Me.lcmbPropietario.Margin = New System.Windows.Forms.Padding(5)
        Me.lcmbPropietario.Name = "lcmbPropietario"
        Me.lcmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lcmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lcmbPropietario.Size = New System.Drawing.Size(376, 22)
        Me.lcmbPropietario.TabIndex = 67
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbBodegaWMS)
        Me.GroupBox3.Controls.Add(Me.lblBodegaWMS)
        Me.GroupBox3.Controls.Add(lblReferencia)
        Me.GroupBox3.Controls.Add(Me.txtReferenciaCliente)
        Me.GroupBox3.Location = New System.Drawing.Point(733, 66)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox3.Size = New System.Drawing.Size(578, 187)
        Me.GroupBox3.TabIndex = 43
        Me.GroupBox3.TabStop = False
        '
        'cmbBodegaWMS
        '
        Me.cmbBodegaWMS.Location = New System.Drawing.Point(46, 62)
        Me.cmbBodegaWMS.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbBodegaWMS.MenuManager = Me.RibbonControl
        Me.cmbBodegaWMS.Name = "cmbBodegaWMS"
        Me.cmbBodegaWMS.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbBodegaWMS.Properties.Appearance.Options.UseBackColor = True
        Me.cmbBodegaWMS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodegaWMS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodegaWMS.Properties.NullText = ""
        Me.cmbBodegaWMS.Size = New System.Drawing.Size(433, 22)
        Me.cmbBodegaWMS.TabIndex = 82
        '
        'lblBodegaWMS
        '
        Me.lblBodegaWMS.AutoSize = True
        Me.lblBodegaWMS.Location = New System.Drawing.Point(42, 30)
        Me.lblBodegaWMS.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblBodegaWMS.Name = "lblBodegaWMS"
        Me.lblBodegaWMS.Size = New System.Drawing.Size(152, 16)
        Me.lblBodegaWMS.TabIndex = 11
        Me.lblBodegaWMS.Text = "Bodega de ingreso WMS:"
        '
        'txtReferenciaCliente
        '
        Me.txtReferenciaCliente.Location = New System.Drawing.Point(42, 133)
        Me.txtReferenciaCliente.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtReferenciaCliente.MenuManager = Me.RibbonControl
        Me.txtReferenciaCliente.Name = "txtReferenciaCliente"
        Me.txtReferenciaCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferenciaCliente.Properties.MaxLength = 25
        Me.txtReferenciaCliente.Size = New System.Drawing.Size(436, 22)
        Me.txtReferenciaCliente.TabIndex = 18
        Me.txtReferenciaCliente.ToolTip = "Este campo puede ser utilizado por ejemplo en las transacciones de ajuste se toma" &
    " como serie para algunas bodegas"
        '
        'lblCodigo
        '
        Me.lblCodigo.Location = New System.Drawing.Point(268, 64)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lblCodigo.MenuManager = Me.RibbonControl
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.lblCodigo.Properties.Appearance.Options.UseBackColor = True
        Me.lblCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblCodigo.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.lblCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.lblCodigo.Size = New System.Drawing.Size(376, 22)
        Me.lblCodigo.TabIndex = 1
        '
        'cmbTipoCliente
        '
        Me.cmbTipoCliente.Location = New System.Drawing.Point(268, 164)
        Me.cmbTipoCliente.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbTipoCliente.MenuManager = Me.RibbonControl
        Me.cmbTipoCliente.Name = "cmbTipoCliente"
        Me.cmbTipoCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoCliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoCliente.Properties.NullText = ""
        Me.cmbTipoCliente.Size = New System.Drawing.Size(376, 22)
        Me.cmbTipoCliente.TabIndex = 10
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.Location = New System.Drawing.Point(268, 98)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbEmpresa.MenuManager = Me.RibbonControl
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresa.Properties.NullText = ""
        Me.cmbEmpresa.Size = New System.Drawing.Size(376, 22)
        Me.cmbEmpresa.TabIndex = 4
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(268, 300)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDireccion.MenuManager = Me.RibbonControl
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDireccion.Size = New System.Drawing.Size(376, 22)
        Me.txtDireccion.TabIndex = 25
        '
        'txtContacto
        '
        Me.txtContacto.Location = New System.Drawing.Point(268, 334)
        Me.txtContacto.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtContacto.MenuManager = Me.RibbonControl
        Me.txtContacto.Name = "txtContacto"
        Me.txtContacto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtContacto.Size = New System.Drawing.Size(376, 22)
        Me.txtContacto.TabIndex = 28
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(268, 266)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtTelefono.MenuManager = Me.RibbonControl
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtTelefono.Size = New System.Drawing.Size(376, 22)
        Me.txtTelefono.TabIndex = 23
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(268, 198)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigo.Size = New System.Drawing.Size(376, 22)
        Me.txtCodigo.TabIndex = 15
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.Location = New System.Drawing.Point(268, 231)
        Me.txtNombreComercial.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNombreComercial.MenuManager = Me.RibbonControl
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreComercial.Size = New System.Drawing.Size(376, 22)
        Me.txtNombreComercial.TabIndex = 19
        '
        'txtNit
        '
        Me.txtNit.Location = New System.Drawing.Point(268, 368)
        Me.txtNit.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNit.MenuManager = Me.RibbonControl
        Me.txtNit.Name = "txtNit"
        Me.txtNit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNit.Size = New System.Drawing.Size(376, 22)
        Me.txtNit.TabIndex = 30
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(268, 402)
        Me.txtCorreo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtCorreo.MenuManager = Me.RibbonControl
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCorreo.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.txtCorreo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtCorreo.Size = New System.Drawing.Size(376, 22)
        Me.txtCorreo.TabIndex = 32
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(131, 36)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(4)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(131, 4)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(615, 36)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(4)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(615, 4)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 3
        '
        'GrpTiempoAceptacion
        '
        Me.GrpTiempoAceptacion.Controls.Add(Me.GroupBox2)
        Me.GrpTiempoAceptacion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTiempoAceptacion.Location = New System.Drawing.Point(0, 0)
        Me.GrpTiempoAceptacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpTiempoAceptacion.Name = "GrpTiempoAceptacion"
        Me.GrpTiempoAceptacion.Size = New System.Drawing.Size(1697, 226)
        Me.GrpTiempoAceptacion.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdGuardarTodos)
        Me.GroupBox2.Controls.Add(Label17)
        Me.GroupBox2.Controls.Add(Label16)
        Me.GroupBox2.Controls.Add(Label15)
        Me.GroupBox2.Controls.Add(Label14)
        Me.GroupBox2.Controls.Add(Me.cmdGuardar)
        Me.GroupBox2.Controls.Add(Me.txtDiaExterior)
        Me.GroupBox2.Controls.Add(Me.txtDiaLocal)
        Me.GroupBox2.Controls.Add(Me.lnkFamilia)
        Me.GroupBox2.Controls.Add(Me.txtIdFamilia)
        Me.GroupBox2.Controls.Add(Me.txtNombreFamilia)
        Me.GroupBox2.Controls.Add(Me.lnkClasificacion)
        Me.GroupBox2.Controls.Add(Me.txtIdClasificacion)
        Me.GroupBox2.Controls.Add(Me.txtNombreClasificacion)
        Me.GroupBox2.Controls.Add(Label10)
        Me.GroupBox2.Controls.Add(Label11)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(2, 28)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox2.Size = New System.Drawing.Size(1693, 196)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cmdGuardarTodos
        '
        Me.cmdGuardarTodos.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGuardarTodos.Location = New System.Drawing.Point(1472, 115)
        Me.cmdGuardarTodos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmdGuardarTodos.Name = "cmdGuardarTodos"
        Me.cmdGuardarTodos.Size = New System.Drawing.Size(130, 44)
        Me.cmdGuardarTodos.TabIndex = 15
        Me.cmdGuardarTodos.Text = "Aplicar a Todos"
        Me.cmdGuardarTodos.UseVisualStyleBackColor = True
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGuardar.Location = New System.Drawing.Point(1287, 115)
        Me.cmdGuardar.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(130, 44)
        Me.cmdGuardar.TabIndex = 14
        Me.cmdGuardar.Text = "Guardar"
        Me.cmdGuardar.UseVisualStyleBackColor = True
        '
        'txtDiaExterior
        '
        Me.txtDiaExterior.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiaExterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiaExterior.Location = New System.Drawing.Point(286, 134)
        Me.txtDiaExterior.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDiaExterior.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtDiaExterior.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtDiaExterior.Name = "txtDiaExterior"
        Me.txtDiaExterior.Size = New System.Drawing.Size(969, 23)
        Me.txtDiaExterior.TabIndex = 13
        '
        'txtDiaLocal
        '
        Me.txtDiaLocal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiaLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiaLocal.Location = New System.Drawing.Point(286, 98)
        Me.txtDiaLocal.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDiaLocal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtDiaLocal.Minimum = New Decimal(New Integer() {1316134911, 2328, 0, -2147483648})
        Me.txtDiaLocal.Name = "txtDiaLocal"
        Me.txtDiaLocal.Size = New System.Drawing.Size(969, 23)
        Me.txtDiaLocal.TabIndex = 10
        '
        'lnkFamilia
        '
        Me.lnkFamilia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkFamilia.AutoSize = True
        Me.lnkFamilia.Location = New System.Drawing.Point(90, 71)
        Me.lnkFamilia.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lnkFamilia.Name = "lnkFamilia"
        Me.lnkFamilia.Size = New System.Drawing.Size(53, 16)
        Me.lnkFamilia.TabIndex = 4
        Me.lnkFamilia.TabStop = True
        Me.lnkFamilia.Text = "Familia:"
        '
        'txtIdFamilia
        '
        Me.txtIdFamilia.Location = New System.Drawing.Point(286, 65)
        Me.txtIdFamilia.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdFamilia.MenuManager = Me.RibbonControl
        Me.txtIdFamilia.Name = "txtIdFamilia"
        Me.txtIdFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdFamilia.Properties.Mask.EditMask = "n0"
        Me.txtIdFamilia.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdFamilia.Size = New System.Drawing.Size(281, 22)
        Me.txtIdFamilia.TabIndex = 6
        '
        'txtNombreFamilia
        '
        Me.txtNombreFamilia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreFamilia.Location = New System.Drawing.Point(578, 65)
        Me.txtNombreFamilia.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNombreFamilia.MenuManager = Me.RibbonControl
        Me.txtNombreFamilia.Name = "txtNombreFamilia"
        Me.txtNombreFamilia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreFamilia.Properties.ReadOnly = True
        Me.txtNombreFamilia.Size = New System.Drawing.Size(1024, 22)
        Me.txtNombreFamilia.TabIndex = 7
        '
        'lnkClasificacion
        '
        Me.lnkClasificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkClasificacion.AutoSize = True
        Me.lnkClasificacion.Location = New System.Drawing.Point(90, 36)
        Me.lnkClasificacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lnkClasificacion.Name = "lnkClasificacion"
        Me.lnkClasificacion.Size = New System.Drawing.Size(82, 16)
        Me.lnkClasificacion.TabIndex = 0
        Me.lnkClasificacion.TabStop = True
        Me.lnkClasificacion.Text = "Clasificación:"
        '
        'txtIdClasificacion
        '
        Me.txtIdClasificacion.Location = New System.Drawing.Point(286, 31)
        Me.txtIdClasificacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdClasificacion.MenuManager = Me.RibbonControl
        Me.txtIdClasificacion.Name = "txtIdClasificacion"
        Me.txtIdClasificacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdClasificacion.Properties.Mask.EditMask = "n0"
        Me.txtIdClasificacion.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIdClasificacion.Size = New System.Drawing.Size(281, 22)
        Me.txtIdClasificacion.TabIndex = 2
        '
        'txtNombreClasificacion
        '
        Me.txtNombreClasificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNombreClasificacion.Location = New System.Drawing.Point(578, 31)
        Me.txtNombreClasificacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNombreClasificacion.MenuManager = Me.RibbonControl
        Me.txtNombreClasificacion.Name = "txtNombreClasificacion"
        Me.txtNombreClasificacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNombreClasificacion.Properties.ReadOnly = True
        Me.txtNombreClasificacion.Size = New System.Drawing.Size(1024, 22)
        Me.txtNombreClasificacion.TabIndex = 3
        '
        'GrpTiempo
        '
        Me.GrpTiempo.Controls.Add(Me.GridTiempo)
        Me.GrpTiempo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpTiempo.Location = New System.Drawing.Point(0, 226)
        Me.GrpTiempo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpTiempo.Name = "GrpTiempo"
        Me.GrpTiempo.Size = New System.Drawing.Size(1697, 377)
        Me.GrpTiempo.TabIndex = 1
        Me.GrpTiempo.Text = "Tiempos Detalle"
        '
        'GridTiempo
        '
        Me.GridTiempo.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridTiempo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridTiempo.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridTiempo.Location = New System.Drawing.Point(2, 28)
        Me.GridTiempo.MainView = Me.GridViewTiempo
        Me.GridTiempo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GridTiempo.MenuManager = Me.RibbonControl
        Me.GridTiempo.Name = "GridTiempo"
        Me.GridTiempo.Size = New System.Drawing.Size(1693, 347)
        Me.GridTiempo.TabIndex = 0
        Me.GridTiempo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewTiempo})
        '
        'GridViewTiempo
        '
        Me.GridViewTiempo.DetailHeight = 674
        Me.GridViewTiempo.GridControl = Me.GridTiempo
        Me.GridViewTiempo.Name = "GridViewTiempo"
        Me.GridViewTiempo.OptionsBehavior.Editable = False
        Me.GridViewTiempo.OptionsView.ShowGroupPanel = False
        '
        'GrpClienteBodega
        '
        Me.GrpClienteBodega.Controls.Add(Me.GroupControl3)
        Me.GrpClienteBodega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpClienteBodega.Location = New System.Drawing.Point(0, 0)
        Me.GrpClienteBodega.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GrpClienteBodega.Name = "GrpClienteBodega"
        Me.GrpClienteBodega.Size = New System.Drawing.Size(1697, 603)
        Me.GrpClienteBodega.TabIndex = 0
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.Grid)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1693, 573)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Selección de Bodega"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.gridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.AreaBodegaGridLookUpEdit})
        Me.Grid.Size = New System.Drawing.Size(1689, 543)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsCliente
        '
        'DsCliente
        '
        Me.DsCliente.DataSetName = "DsCliente"
        Me.DsCliente.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSelección, Me.colIdBodega, Me.colIdAsignacion, Me.colBodega, Me.colIdInterno, Me.colIdAreaDestino})
        Me.gridView1.DetailHeight = 674
        Me.gridView1.GridControl = Me.Grid
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsView.ShowGroupPanel = False
        '
        'colSelección
        '
        Me.colSelección.FieldName = "Selección"
        Me.colSelección.MinWidth = 24
        Me.colSelección.Name = "colSelección"
        Me.colSelección.Visible = True
        Me.colSelección.VisibleIndex = 0
        Me.colSelección.Width = 94
        '
        'colIdBodega
        '
        Me.colIdBodega.FieldName = "IdBodega"
        Me.colIdBodega.MinWidth = 24
        Me.colIdBodega.Name = "colIdBodega"
        Me.colIdBodega.Visible = True
        Me.colIdBodega.VisibleIndex = 1
        Me.colIdBodega.Width = 94
        '
        'colIdAsignacion
        '
        Me.colIdAsignacion.FieldName = "IdAsignacion"
        Me.colIdAsignacion.MinWidth = 24
        Me.colIdAsignacion.Name = "colIdAsignacion"
        Me.colIdAsignacion.Visible = True
        Me.colIdAsignacion.VisibleIndex = 2
        Me.colIdAsignacion.Width = 94
        '
        'colBodega
        '
        Me.colBodega.FieldName = "Bodega"
        Me.colBodega.MinWidth = 24
        Me.colBodega.Name = "colBodega"
        Me.colBodega.Visible = True
        Me.colBodega.VisibleIndex = 3
        Me.colBodega.Width = 94
        '
        'colIdInterno
        '
        Me.colIdInterno.FieldName = "IdInterno"
        Me.colIdInterno.MinWidth = 24
        Me.colIdInterno.Name = "colIdInterno"
        Me.colIdInterno.Visible = True
        Me.colIdInterno.VisibleIndex = 4
        Me.colIdInterno.Width = 94
        '
        'colIdAreaDestino
        '
        Me.colIdAreaDestino.ColumnEdit = Me.AreaBodegaGridLookUpEdit
        Me.colIdAreaDestino.FieldName = "IdAreaDestino"
        Me.colIdAreaDestino.MinWidth = 24
        Me.colIdAreaDestino.Name = "colIdAreaDestino"
        Me.colIdAreaDestino.OptionsEditForm.Caption = "Área Destino"
        Me.colIdAreaDestino.Visible = True
        Me.colIdAreaDestino.VisibleIndex = 5
        Me.colIdAreaDestino.Width = 94
        '
        'AreaBodegaGridLookUpEdit
        '
        Me.AreaBodegaGridLookUpEdit.AutoHeight = False
        Me.AreaBodegaGridLookUpEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.AreaBodegaGridLookUpEdit.Name = "AreaBodegaGridLookUpEdit"
        Me.AreaBodegaGridLookUpEdit.PopupView = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'cmdAgregarDireccion
        '
        Me.cmdAgregarDireccion.Location = New System.Drawing.Point(758, 519)
        Me.cmdAgregarDireccion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmdAgregarDireccion.Name = "cmdAgregarDireccion"
        Me.cmdAgregarDireccion.Size = New System.Drawing.Size(142, 53)
        Me.cmdAgregarDireccion.TabIndex = 1
        Me.cmdAgregarDireccion.Text = ">>"
        Me.cmdAgregarDireccion.UseVisualStyleBackColor = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.dgridDirecciones)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControl1.Location = New System.Drawing.Point(1007, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(690, 603)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Tiempos Detalle"
        '
        'dgridDirecciones
        '
        Me.dgridDirecciones.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridDirecciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridDirecciones.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dgridDirecciones.Location = New System.Drawing.Point(2, 28)
        Me.dgridDirecciones.MainView = Me.gvDireccionesCli
        Me.dgridDirecciones.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dgridDirecciones.MenuManager = Me.RibbonControl
        Me.dgridDirecciones.Name = "dgridDirecciones"
        Me.dgridDirecciones.Size = New System.Drawing.Size(686, 573)
        Me.dgridDirecciones.TabIndex = 0
        Me.dgridDirecciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDireccionesCli})
        '
        'gvDireccionesCli
        '
        Me.gvDireccionesCli.DetailHeight = 674
        Me.gvDireccionesCli.GridControl = Me.dgridDirecciones
        Me.gvDireccionesCli.Name = "gvDireccionesCli"
        Me.gvDireccionesCli.OptionsBehavior.Editable = False
        Me.gvDireccionesCli.OptionsView.ColumnAutoWidth = False
        Me.gvDireccionesCli.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gvDireccionesCli.OptionsView.ShowGroupPanel = False
        Me.gvDireccionesCli.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[True]
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbRegion)
        Me.GroupBox1.Controls.Add(Me.cmbMunicipio)
        Me.GroupBox1.Controls.Add(Me.cmbDepartamento)
        Me.GroupBox1.Controls.Add(Me.cmbPais)
        Me.GroupBox1.Controls.Add(Label3)
        Me.GroupBox1.Controls.Add(Me.txtDireccionEntrega)
        Me.GroupBox1.Controls.Add(lblPais)
        Me.GroupBox1.Controls.Add(lblDepartamento)
        Me.GroupBox1.Controls.Add(DireccionLabel)
        Me.GroupBox1.Controls.Add(IdDireccionLabel)
        Me.GroupBox1.Controls.Add(Me.chkLocal)
        Me.GroupBox1.Controls.Add(Me.txtIdDireccion)
        Me.GroupBox1.Controls.Add(IdMunicipioLabel)
        Me.GroupBox1.Controls.Add(AvenidaLabel)
        Me.GroupBox1.Controls.Add(Me.txtAvenida)
        Me.GroupBox1.Controls.Add(CalleLabel)
        Me.GroupBox1.Controls.Add(Me.txtCalle)
        Me.GroupBox1.Controls.Add(No_CasaLabel)
        Me.GroupBox1.Controls.Add(Me.txtNoCasa)
        Me.GroupBox1.Controls.Add(ZonaLabel)
        Me.GroupBox1.Controls.Add(Me.txtZona)
        Me.GroupBox1.Controls.Add(RegionLabel)
        Me.GroupBox1.Controls.Add(ReferenciaLabel)
        Me.GroupBox1.Controls.Add(Me.txtReferenciaDireccion)
        Me.GroupBox1.Controls.Add(Coordenada_xLabel)
        Me.GroupBox1.Controls.Add(Me.txtCordX)
        Me.GroupBox1.Controls.Add(Coordenada_yLabel)
        Me.GroupBox1.Controls.Add(Me.txtCordY)
        Me.GroupBox1.Controls.Add(LocalLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 44)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.GroupBox1.Size = New System.Drawing.Size(590, 885)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbRegion
        '
        Me.cmbRegion.Location = New System.Drawing.Point(192, 309)
        Me.cmbRegion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbRegion.MenuManager = Me.RibbonControl
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbRegion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbRegion.Properties.NullText = ""
        Me.cmbRegion.Size = New System.Drawing.Size(376, 22)
        Me.cmbRegion.TabIndex = 17
        '
        'cmbMunicipio
        '
        Me.cmbMunicipio.Location = New System.Drawing.Point(192, 144)
        Me.cmbMunicipio.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbMunicipio.MenuManager = Me.RibbonControl
        Me.cmbMunicipio.Name = "cmbMunicipio"
        Me.cmbMunicipio.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbMunicipio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbMunicipio.Properties.NullText = ""
        Me.cmbMunicipio.Size = New System.Drawing.Size(376, 22)
        Me.cmbMunicipio.TabIndex = 7
        '
        'cmbDepartamento
        '
        Me.cmbDepartamento.Location = New System.Drawing.Point(192, 111)
        Me.cmbDepartamento.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbDepartamento.MenuManager = Me.RibbonControl
        Me.cmbDepartamento.Name = "cmbDepartamento"
        Me.cmbDepartamento.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbDepartamento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDepartamento.Properties.NullText = ""
        Me.cmbDepartamento.Size = New System.Drawing.Size(376, 22)
        Me.cmbDepartamento.TabIndex = 5
        '
        'cmbPais
        '
        Me.cmbPais.Location = New System.Drawing.Point(192, 75)
        Me.cmbPais.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.cmbPais.MenuManager = Me.RibbonControl
        Me.cmbPais.Name = "cmbPais"
        Me.cmbPais.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPais.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPais.Properties.NullText = ""
        Me.cmbPais.Size = New System.Drawing.Size(376, 22)
        Me.cmbPais.TabIndex = 3
        '
        'txtDireccionEntrega
        '
        Me.txtDireccionEntrega.Location = New System.Drawing.Point(195, 686)
        Me.txtDireccionEntrega.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtDireccionEntrega.Multiline = True
        Me.txtDireccionEntrega.Name = "txtDireccionEntrega"
        Me.txtDireccionEntrega.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDireccionEntrega.Size = New System.Drawing.Size(374, 126)
        Me.txtDireccionEntrega.TabIndex = 28
        '
        'chkLocal
        '
        Me.chkLocal.EditValue = True
        Me.chkLocal.Location = New System.Drawing.Point(192, 446)
        Me.chkLocal.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.chkLocal.MenuManager = Me.RibbonControl
        Me.chkLocal.Name = "chkLocal"
        Me.chkLocal.Properties.Caption = ""
        Me.chkLocal.Size = New System.Drawing.Size(376, 24)
        Me.chkLocal.TabIndex = 25
        '
        'txtIdDireccion
        '
        Me.txtIdDireccion.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtIdDireccion.Enabled = False
        Me.txtIdDireccion.Location = New System.Drawing.Point(192, 39)
        Me.txtIdDireccion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdDireccion.MenuManager = Me.RibbonControl
        Me.txtIdDireccion.Name = "txtIdDireccion"
        Me.txtIdDireccion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdDireccion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIdDireccion.Size = New System.Drawing.Size(376, 24)
        Me.txtIdDireccion.TabIndex = 1
        '
        'txtAvenida
        '
        Me.txtAvenida.Location = New System.Drawing.Point(192, 174)
        Me.txtAvenida.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtAvenida.MenuManager = Me.RibbonControl
        Me.txtAvenida.Name = "txtAvenida"
        Me.txtAvenida.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtAvenida.Size = New System.Drawing.Size(376, 22)
        Me.txtAvenida.TabIndex = 9
        '
        'txtCalle
        '
        Me.txtCalle.Location = New System.Drawing.Point(192, 207)
        Me.txtCalle.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtCalle.MenuManager = Me.RibbonControl
        Me.txtCalle.Name = "txtCalle"
        Me.txtCalle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCalle.Size = New System.Drawing.Size(376, 22)
        Me.txtCalle.TabIndex = 11
        '
        'txtNoCasa
        '
        Me.txtNoCasa.Location = New System.Drawing.Point(192, 241)
        Me.txtNoCasa.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtNoCasa.MenuManager = Me.RibbonControl
        Me.txtNoCasa.Name = "txtNoCasa"
        Me.txtNoCasa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtNoCasa.Size = New System.Drawing.Size(376, 22)
        Me.txtNoCasa.TabIndex = 13
        '
        'txtZona
        '
        Me.txtZona.Location = New System.Drawing.Point(192, 274)
        Me.txtZona.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtZona.MenuManager = Me.RibbonControl
        Me.txtZona.Name = "txtZona"
        Me.txtZona.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtZona.Size = New System.Drawing.Size(376, 22)
        Me.txtZona.TabIndex = 15
        '
        'txtReferenciaDireccion
        '
        Me.txtReferenciaDireccion.Location = New System.Drawing.Point(192, 343)
        Me.txtReferenciaDireccion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtReferenciaDireccion.MenuManager = Me.RibbonControl
        Me.txtReferenciaDireccion.Name = "txtReferenciaDireccion"
        Me.txtReferenciaDireccion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferenciaDireccion.Size = New System.Drawing.Size(376, 22)
        Me.txtReferenciaDireccion.TabIndex = 19
        '
        'txtCordX
        '
        Me.txtCordX.Location = New System.Drawing.Point(192, 377)
        Me.txtCordX.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtCordX.MenuManager = Me.RibbonControl
        Me.txtCordX.Name = "txtCordX"
        Me.txtCordX.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCordX.Size = New System.Drawing.Size(376, 22)
        Me.txtCordX.TabIndex = 21
        '
        'txtCordY
        '
        Me.txtCordY.Location = New System.Drawing.Point(192, 411)
        Me.txtCordY.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtCordY.MenuManager = Me.RibbonControl
        Me.txtCordY.Name = "txtCordY"
        Me.txtCordY.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCordY.Size = New System.Drawing.Size(376, 22)
        Me.txtCordY.TabIndex = 23
        '
        'Cliente_direccionTableAdapter
        '
        Me.Cliente_direccionTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.cliente_direccionTableAdapter = Me.Cliente_direccionTableAdapter
        Me.TableAdapterManager.UpdateOrder = TOMWMS.cliente_direccion_dsetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'dkCliente
        '
        Me.dkCliente.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkCliente.Form = Me
        Me.dkCliente.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 826)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1699, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("6a95e6d6-496f-4511-ba55-2e8a6590f626")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 694)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 106)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1699, 132)
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
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1691, 94)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtrtabClientes
        '
        Me.xtrtabClientes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrtabClientes.Location = New System.Drawing.Point(0, 193)
        Me.xtrtabClientes.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.xtrtabClientes.Name = "xtrtabClientes"
        Me.xtrtabClientes.SelectedTabPage = Me.datosCliente
        Me.xtrtabClientes.Size = New System.Drawing.Size(1699, 633)
        Me.xtrtabClientes.TabIndex = 0
        Me.xtrtabClientes.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.datosCliente, Me.tiempoaceptacion, Me.clienteBodega, Me.DireccionesEntrega, Me.tabLotes})
        '
        'datosCliente
        '
        Me.datosCliente.Controls.Add(Me.GrpCliente)
        Me.datosCliente.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.datosCliente.Name = "datosCliente"
        Me.datosCliente.Size = New System.Drawing.Size(1697, 603)
        Me.datosCliente.Text = "Datos Cliente"
        '
        'tiempoaceptacion
        '
        Me.tiempoaceptacion.Controls.Add(Me.GrpTiempo)
        Me.tiempoaceptacion.Controls.Add(Me.GrpTiempoAceptacion)
        Me.tiempoaceptacion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.tiempoaceptacion.Name = "tiempoaceptacion"
        Me.tiempoaceptacion.Size = New System.Drawing.Size(1697, 603)
        Me.tiempoaceptacion.Text = "Tiempos de Aceptación"
        '
        'clienteBodega
        '
        Me.clienteBodega.Controls.Add(Me.GrpClienteBodega)
        Me.clienteBodega.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.clienteBodega.Name = "clienteBodega"
        Me.clienteBodega.Size = New System.Drawing.Size(1697, 603)
        Me.clienteBodega.Text = "Cliente Bodega"
        '
        'DireccionesEntrega
        '
        Me.DireccionesEntrega.Controls.Add(Me.ToolStripPR)
        Me.DireccionesEntrega.Controls.Add(Me.GroupBox1)
        Me.DireccionesEntrega.Controls.Add(Me.cmdAgregarDireccion)
        Me.DireccionesEntrega.Controls.Add(Me.GroupControl1)
        Me.DireccionesEntrega.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DireccionesEntrega.Name = "DireccionesEntrega"
        Me.DireccionesEntrega.Size = New System.Drawing.Size(1697, 603)
        Me.DireccionesEntrega.Text = "Direcciones de Entrega"
        '
        'ToolStripPR
        '
        Me.ToolStripPR.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStripPR.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNewPR, Me.cmdDesactivarPresentacion})
        Me.ToolStripPR.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripPR.Name = "ToolStripPR"
        Me.ToolStripPR.Size = New System.Drawing.Size(1007, 31)
        Me.ToolStripPR.TabIndex = 3
        Me.ToolStripPR.Text = "ToolStrip2"
        '
        'cmdNewPR
        '
        Me.cmdNewPR.Image = CType(resources.GetObject("cmdNewPR.Image"), System.Drawing.Image)
        Me.cmdNewPR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNewPR.Name = "cmdNewPR"
        Me.cmdNewPR.Size = New System.Drawing.Size(76, 28)
        Me.cmdNewPR.Text = "Nuevo"
        '
        'cmdDesactivarPresentacion
        '
        Me.cmdDesactivarPresentacion.Image = CType(resources.GetObject("cmdDesactivarPresentacion.Image"), System.Drawing.Image)
        Me.cmdDesactivarPresentacion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesactivarPresentacion.Name = "cmdDesactivarPresentacion"
        Me.cmdDesactivarPresentacion.Size = New System.Drawing.Size(87, 28)
        Me.cmdDesactivarPresentacion.Text = "Eliminar"
        '
        'tabLotes
        '
        Me.tabLotes.Controls.Add(Me.grpMantLotes)
        Me.tabLotes.Controls.Add(Me.grpLotesPermitidos)
        Me.tabLotes.Controls.Add(Me.grpLotesBloqueados)
        Me.tabLotes.Name = "tabLotes"
        Me.tabLotes.Size = New System.Drawing.Size(1697, 603)
        Me.tabLotes.Text = "Gestión Lotes"
        '
        'grpMantLotes
        '
        Me.grpMantLotes.Controls.Add(Me.txtIdClienteLote)
        Me.grpMantLotes.Controls.Add(lblIdClienteLote)
        Me.grpMantLotes.Controls.Add(Me.ToolStrip1)
        Me.grpMantLotes.Controls.Add(Label25)
        Me.grpMantLotes.Controls.Add(Me.chkLoteActivo)
        Me.grpMantLotes.Controls.Add(lblchkBloquear)
        Me.grpMantLotes.Controls.Add(Me.chkBloquear)
        Me.grpMantLotes.Controls.Add(Me.dtpFechaAgregoLote)
        Me.grpMantLotes.Controls.Add(Me.txtUsuarioAgregoLote)
        Me.grpMantLotes.Controls.Add(Label20)
        Me.grpMantLotes.Controls.Add(Label21)
        Me.grpMantLotes.Controls.Add(Me.txtUsuarioModificoLote)
        Me.grpMantLotes.Controls.Add(Label22)
        Me.grpMantLotes.Controls.Add(Label23)
        Me.grpMantLotes.Controls.Add(Me.dtpFechaModificoLote)
        Me.grpMantLotes.Controls.Add(Label19)
        Me.grpMantLotes.Controls.Add(Me.cmbEstadoProducto)
        Me.grpMantLotes.Controls.Add(Label18)
        Me.grpMantLotes.Controls.Add(Me.cmbLote)
        Me.grpMantLotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMantLotes.Location = New System.Drawing.Point(0, 0)
        Me.grpMantLotes.Name = "grpMantLotes"
        Me.grpMantLotes.Size = New System.Drawing.Size(572, 603)
        Me.grpMantLotes.TabIndex = 87
        '
        'txtIdClienteLote
        '
        Me.txtIdClienteLote.Location = New System.Drawing.Point(148, 104)
        Me.txtIdClienteLote.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.txtIdClienteLote.MenuManager = Me.RibbonControl
        Me.txtIdClienteLote.Name = "txtIdClienteLote"
        Me.txtIdClienteLote.Properties.Appearance.BackColor = System.Drawing.Color.Lavender
        Me.txtIdClienteLote.Properties.Appearance.Options.UseBackColor = True
        Me.txtIdClienteLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdClienteLote.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.RegExpMaskManager))
        Me.txtIdClienteLote.Properties.MaskSettings.Set("allowBlankInput", True)
        Me.txtIdClienteLote.Properties.MaskSettings.Set("mask", "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")
        Me.txtIdClienteLote.Size = New System.Drawing.Size(261, 22)
        Me.txtIdClienteLote.TabIndex = 100
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNuevoLote, Me.mnuGuardarLote, Me.mnuEliminarLote})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 28)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(568, 27)
        Me.ToolStrip1.TabIndex = 98
        Me.ToolStrip1.Text = "ToolStrip2"
        '
        'mnuNuevoLote
        '
        Me.mnuNuevoLote.Image = CType(resources.GetObject("mnuNuevoLote.Image"), System.Drawing.Image)
        Me.mnuNuevoLote.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuNuevoLote.Name = "mnuNuevoLote"
        Me.mnuNuevoLote.Size = New System.Drawing.Size(76, 24)
        Me.mnuNuevoLote.Text = "Nuevo"
        '
        'mnuGuardarLote
        '
        Me.mnuGuardarLote.Image = CType(resources.GetObject("mnuGuardarLote.Image"), System.Drawing.Image)
        Me.mnuGuardarLote.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuGuardarLote.Name = "mnuGuardarLote"
        Me.mnuGuardarLote.Size = New System.Drawing.Size(86, 24)
        Me.mnuGuardarLote.Text = "Guardar"
        '
        'mnuEliminarLote
        '
        Me.mnuEliminarLote.Image = CType(resources.GetObject("mnuEliminarLote.Image"), System.Drawing.Image)
        Me.mnuEliminarLote.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEliminarLote.Name = "mnuEliminarLote"
        Me.mnuEliminarLote.Size = New System.Drawing.Size(87, 24)
        Me.mnuEliminarLote.Text = "Eliminar"
        '
        'chkLoteActivo
        '
        Me.chkLoteActivo.EditValue = True
        Me.chkLoteActivo.Location = New System.Drawing.Point(314, 213)
        Me.chkLoteActivo.MenuManager = Me.RibbonControl
        Me.chkLoteActivo.Name = "chkLoteActivo"
        Me.chkLoteActivo.Properties.OffText = ""
        Me.chkLoteActivo.Properties.OnText = "On"
        Me.chkLoteActivo.Size = New System.Drawing.Size(95, 24)
        Me.chkLoteActivo.TabIndex = 96
        '
        'chkBloquear
        '
        Me.chkBloquear.Location = New System.Drawing.Point(314, 183)
        Me.chkBloquear.MenuManager = Me.RibbonControl
        Me.chkBloquear.Name = "chkBloquear"
        Me.chkBloquear.Properties.OffText = ""
        Me.chkBloquear.Properties.OnText = "On"
        Me.chkBloquear.Size = New System.Drawing.Size(95, 24)
        Me.chkBloquear.TabIndex = 94
        '
        'dtpFechaAgregoLote
        '
        Me.dtpFechaAgregoLote.EditValue = Nothing
        Me.dtpFechaAgregoLote.Enabled = False
        Me.dtpFechaAgregoLote.Location = New System.Drawing.Point(148, 280)
        Me.dtpFechaAgregoLote.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaAgregoLote.MenuManager = Me.RibbonControl
        Me.dtpFechaAgregoLote.Name = "dtpFechaAgregoLote"
        Me.dtpFechaAgregoLote.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dtpFechaAgregoLote.Properties.Appearance.Options.UseBackColor = True
        Me.dtpFechaAgregoLote.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAgregoLote.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaAgregoLote.Size = New System.Drawing.Size(261, 22)
        Me.dtpFechaAgregoLote.TabIndex = 91
        '
        'txtUsuarioAgregoLote
        '
        Me.txtUsuarioAgregoLote.Enabled = False
        Me.txtUsuarioAgregoLote.Location = New System.Drawing.Point(148, 248)
        Me.txtUsuarioAgregoLote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsuarioAgregoLote.MenuManager = Me.RibbonControl
        Me.txtUsuarioAgregoLote.Name = "txtUsuarioAgregoLote"
        Me.txtUsuarioAgregoLote.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtUsuarioAgregoLote.Properties.Appearance.Options.UseBackColor = True
        Me.txtUsuarioAgregoLote.Size = New System.Drawing.Size(261, 22)
        Me.txtUsuarioAgregoLote.TabIndex = 87
        '
        'txtUsuarioModificoLote
        '
        Me.txtUsuarioModificoLote.Enabled = False
        Me.txtUsuarioModificoLote.Location = New System.Drawing.Point(148, 316)
        Me.txtUsuarioModificoLote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUsuarioModificoLote.MenuManager = Me.RibbonControl
        Me.txtUsuarioModificoLote.Name = "txtUsuarioModificoLote"
        Me.txtUsuarioModificoLote.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtUsuarioModificoLote.Properties.Appearance.Options.UseBackColor = True
        Me.txtUsuarioModificoLote.Size = New System.Drawing.Size(261, 22)
        Me.txtUsuarioModificoLote.TabIndex = 89
        '
        'dtpFechaModificoLote
        '
        Me.dtpFechaModificoLote.EditValue = Nothing
        Me.dtpFechaModificoLote.Enabled = False
        Me.dtpFechaModificoLote.Location = New System.Drawing.Point(148, 348)
        Me.dtpFechaModificoLote.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFechaModificoLote.MenuManager = Me.RibbonControl
        Me.dtpFechaModificoLote.Name = "dtpFechaModificoLote"
        Me.dtpFechaModificoLote.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dtpFechaModificoLote.Properties.Appearance.Options.UseBackColor = True
        Me.dtpFechaModificoLote.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaModificoLote.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaModificoLote.Size = New System.Drawing.Size(261, 22)
        Me.dtpFechaModificoLote.TabIndex = 93
        '
        'cmbEstadoProducto
        '
        Me.cmbEstadoProducto.Enabled = False
        Me.cmbEstadoProducto.Location = New System.Drawing.Point(148, 156)
        Me.cmbEstadoProducto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbEstadoProducto.MenuManager = Me.RibbonControl
        Me.cmbEstadoProducto.Name = "cmbEstadoProducto"
        Me.cmbEstadoProducto.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbEstadoProducto.Properties.Appearance.Options.UseBackColor = True
        Me.cmbEstadoProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstadoProducto.Properties.NullText = "(Auto) en base a lote"
        Me.cmbEstadoProducto.Properties.PopupView = Me.GridView6
        Me.cmbEstadoProducto.Size = New System.Drawing.Size(261, 22)
        Me.cmbEstadoProducto.TabIndex = 84
        Me.cmbEstadoProducto.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridView6
        '
        Me.GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView6.OptionsView.ShowAutoFilterRow = True
        Me.GridView6.OptionsView.ShowGroupPanel = False
        '
        'cmbLote
        '
        Me.cmbLote.Location = New System.Drawing.Point(148, 130)
        Me.cmbLote.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmbLote.MenuManager = Me.RibbonControl
        Me.cmbLote.Name = "cmbLote"
        Me.cmbLote.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLote.Properties.NullText = "Seleccione Lote"
        Me.cmbLote.Properties.PopupView = Me.GridView5
        Me.cmbLote.Size = New System.Drawing.Size(261, 22)
        Me.cmbLote.TabIndex = 82
        Me.cmbLote.ToolTip = "Config = Activo =1 Bloqueada = 0 Sistema = 0 Despacho = 1 "
        '
        'GridView5
        '
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'grpLotesPermitidos
        '
        Me.grpLotesPermitidos.Controls.Add(Me.dgridLotesPermitidos)
        Me.grpLotesPermitidos.Dock = System.Windows.Forms.DockStyle.Right
        Me.grpLotesPermitidos.Location = New System.Drawing.Point(871, 0)
        Me.grpLotesPermitidos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.grpLotesPermitidos.Name = "grpLotesPermitidos"
        Me.grpLotesPermitidos.Size = New System.Drawing.Size(625, 754)
        Me.grpLotesPermitidos.TabIndex = 86
        Me.grpLotesPermitidos.Text = "Lotes Permitidos"
        '
        'dgridLotesPermitidos
        '
        Me.dgridLotesPermitidos.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridLotesPermitidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridLotesPermitidos.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dgridLotesPermitidos.Location = New System.Drawing.Point(2, 28)
        Me.dgridLotesPermitidos.MainView = Me.GridView4
        Me.dgridLotesPermitidos.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dgridLotesPermitidos.MenuManager = Me.RibbonControl
        Me.dgridLotesPermitidos.Name = "dgridLotesPermitidos"
        Me.dgridLotesPermitidos.Size = New System.Drawing.Size(621, 724)
        Me.dgridLotesPermitidos.TabIndex = 0
        Me.dgridLotesPermitidos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.DetailHeight = 674
        Me.GridView4.GridControl = Me.dgridLotesPermitidos
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsBehavior.Editable = False
        Me.GridView4.OptionsView.ColumnAutoWidth = False
        Me.GridView4.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.GridView4.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[True]
        '
        'grpLotesBloqueados
        '
        Me.grpLotesBloqueados.Controls.Add(Me.DgridLotesBloqueados)
        Me.grpLotesBloqueados.Dock = System.Windows.Forms.DockStyle.Right
        Me.grpLotesBloqueados.Location = New System.Drawing.Point(1621, 0)
        Me.grpLotesBloqueados.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.grpLotesBloqueados.Name = "grpLotesBloqueados"
        Me.grpLotesBloqueados.Size = New System.Drawing.Size(500, 754)
        Me.grpLotesBloqueados.TabIndex = 85
        Me.grpLotesBloqueados.Text = "Lotes Bloqueados"
        '
        'DgridLotesBloqueados
        '
        Me.DgridLotesBloqueados.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridLotesBloqueados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridLotesBloqueados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DgridLotesBloqueados.Location = New System.Drawing.Point(2, 28)
        Me.DgridLotesBloqueados.MainView = Me.GridView3
        Me.DgridLotesBloqueados.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DgridLotesBloqueados.MenuManager = Me.RibbonControl
        Me.DgridLotesBloqueados.Name = "DgridLotesBloqueados"
        Me.DgridLotesBloqueados.Size = New System.Drawing.Size(496, 724)
        Me.DgridLotesBloqueados.TabIndex = 0
        Me.DgridLotesBloqueados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.DetailHeight = 674
        Me.GridView3.GridControl = Me.DgridLotesBloqueados
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsBehavior.Editable = False
        Me.GridView3.OptionsView.ColumnAutoWidth = False
        Me.GridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.GridView3.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[True]
        '
        'frmCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1699, 882)
        Me.Controls.Add(Me.xtrtabClientes)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCliente"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Cliente"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCliente.ResumeLayout(False)
        Me.GrpCliente.PerformLayout()
        CType(Me.txtIdProductoEstadoDefecto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodegaAreaSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionAbastecerCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferenciaCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContacto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreComercial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTiempoAceptacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTiempoAceptacion.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtDiaExterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiaLocal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreFamilia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreClasificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTiempo.ResumeLayout(False)
        CType(Me.GridTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridViewTiempo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpClienteBodega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpClienteBodega.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AreaBodegaGridLookUpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.dgridDirecciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDireccionesCli, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbRegion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMunicipio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDepartamento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAvenida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCalle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoCasa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtZona.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferenciaDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCordX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCordY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.xtrtabClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtrtabClientes.ResumeLayout(False)
        Me.datosCliente.ResumeLayout(False)
        Me.tiempoaceptacion.ResumeLayout(False)
        Me.clienteBodega.ResumeLayout(False)
        Me.DireccionesEntrega.ResumeLayout(False)
        Me.DireccionesEntrega.PerformLayout()
        Me.ToolStripPR.ResumeLayout(False)
        Me.ToolStripPR.PerformLayout()
        Me.tabLotes.ResumeLayout(False)
        CType(Me.grpMantLotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMantLotes.ResumeLayout(False)
        Me.grpMantLotes.PerformLayout()
        CType(Me.txtIdClienteLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.chkLoteActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBloquear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAgregoLote.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaAgregoLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsuarioAgregoLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUsuarioModificoLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaModificoLote.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaModificoLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstadoProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLotesPermitidos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLotesPermitidos.ResumeLayout(False)
        CType(Me.dgridLotesPermitidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLotesBloqueados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLotesBloqueados.ResumeLayout(False)
        CType(Me.DgridLotesBloqueados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GrpCliente As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreComercial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCorreo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtContacto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GrpTiempoAceptacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkFamilia As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreFamilia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkClasificacion As System.Windows.Forms.LinkLabel
    Friend WithEvents txtIdClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreClasificacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDiaExterior As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtDiaLocal As System.Windows.Forms.NumericUpDown
    Friend WithEvents GrpClienteBodega As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GrpTiempo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdGuardar As System.Windows.Forms.Button
    Friend WithEvents GridTiempo As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridViewTiempo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Cliente_direccionTableAdapter As TOMWMS.cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents TableAdapterManager As TOMWMS.cliente_direccion_dsetTableAdapters.TableAdapterManager
    Friend WithEvents txtIdDireccion As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtAvenida As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCalle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoCasa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtZona As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtReferenciaDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCordX As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCordY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkLocal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridDirecciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDireccionesCli As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAgregarDireccion As System.Windows.Forms.Button
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsCliente As TOMWMS.DsCliente
    Friend WithEvents txtDireccionEntrega As System.Windows.Forms.TextBox
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dkCliente As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtrtabClientes As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents datosCliente As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tiempoaceptacion As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents clienteBodega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DireccionesEntrega As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents cmbPais As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbDepartamento As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEmpresa As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbMunicipio As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbRegion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoCliente As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtReferenciaCliente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblBodegaWMS As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ToolStripPR As ToolStrip
    Friend WithEvents cmdNewPR As ToolStripButton
    Friend WithEvents cmdDesactivarPresentacion As ToolStripButton
    Friend WithEvents lcmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents mnuMapa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkEsBodRecep As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkControlUltimoLote As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkControlCalidad As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkEsBodegaTraslado As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkSistema As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkManufactura As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents txtIdUbicacionAbastecerCon As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbBodegaWMS As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents colSelección As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdAsignacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdInterno As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdAreaDestino As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AreaBodegaGridLookUpEdit As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbBodegaAreaSAP As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkEsProveedor As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents cmdGuardarTodos As Button
    Friend WithEvents tabLotes As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grpMantLotes As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdClienteLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents mnuNuevoLote As ToolStripButton
    Friend WithEvents mnuEliminarLote As ToolStripButton
    Friend WithEvents chkLoteActivo As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkBloquear As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents dtpFechaAgregoLote As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtUsuarioAgregoLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUsuarioModificoLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtpFechaModificoLote As DevExpress.XtraEditors.DateEdit
    Friend WithEvents grpLotesPermitidos As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgridLotesPermitidos As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpLotesBloqueados As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DgridLotesBloqueados As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbEstadoProducto As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbLote As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuGuardarLote As ToolStripButton
    Friend WithEvents txtIdProductoEstadoDefecto As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
