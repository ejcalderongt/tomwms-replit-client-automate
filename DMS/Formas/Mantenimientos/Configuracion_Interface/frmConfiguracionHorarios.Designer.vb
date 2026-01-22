<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConfiguracionHorarios
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
        Dim User_modLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim lblEstadoProductoPedidos As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim lblDiasImportacion As System.Windows.Forms.Label
        Dim Label28 As System.Windows.Forms.Label
        Dim Label29 As System.Windows.Forms.Label
        Dim Label30 As System.Windows.Forms.Label
        Dim Label31 As System.Windows.Forms.Label
        Dim Label32 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfiguracionHorarios))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim Label33 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.pnlEncabezado = New DevExpress.XtraEditors.PanelControl()
        Me.txtCodigoBodegaProrrateo1 = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBodegaProrrateo = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBodegaFacturacion = New DevExpress.XtraEditors.TextEdit()
        Me.chkExcluirRececpionPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.chkValidaSoloCodigo = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRechazarBonificacionIncompleta = New DevExpress.XtraEditors.CheckEdit()
        Me.chkInferirBonificacionPedidoSAP = New DevExpress.XtraEditors.CheckEdit()
        Me.nudRangoDiasImportacion = New System.Windows.Forms.NumericUpDown()
        Me.cmbIndiceRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbIdEstadoProductoNC = New System.Windows.Forms.ComboBox()
        Me.dtpVenceDefectoNC = New System.Windows.Forms.DateTimePicker()
        Me.txtLoteDefectoEntradaNC = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBodegaERPNC = New DevExpress.XtraEditors.TextEdit()
        Me.chkConsiderar_Paletizado_En_Reabasto = New DevExpress.XtraEditors.CheckEdit()
        Me.chkExcluirUbicacionesReabasto = New DevExpress.XtraEditors.CheckEdit()
        Me.chkRecepcionGeneraHistorico = New DevExpress.XtraEditors.CheckEdit()
        Me.chkmantener_zona_picking_clavaud = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTipoRotacion = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkEjecutarEnDespachoAuotmaticamente = New DevExpress.XtraEditors.CheckEdit()
        Me.chkExplosionAutomaticaInterface = New DevExpress.XtraEditors.CheckEdit()
        Me.chkImplosionAutomaticaEnInterface = New DevExpress.XtraEditors.CheckEdit()
        Me.chkpush_ingreso_nav_desde_hh = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCrearRecTransfNAV = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCrearRecCompraNAV = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbEtiqueta = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.txtIdAcuerdoEnc = New DevExpress.XtraEditors.TextEdit()
        Me.chkControlPeso = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoProvProd = New DevExpress.XtraEditors.TextEdit()
        Me.chkGenerarRecAutoBD = New DevExpress.XtraEditors.CheckEdit()
        Me.chkGenerarPedidoIngresoBD = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlLote = New DevExpress.XtraEditors.CheckEdit()
        Me.txtArchivo = New DevExpress.XtraEditors.ButtonEdit()
        Me.chkControlFechaVencimiento = New DevExpress.XtraEditors.CheckEdit()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.chkGeneraLP = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbClasificación = New System.Windows.Forms.ComboBox()
        Me.cmbTipoProducto = New System.Windows.Forms.ComboBox()
        Me.cmbMarca = New System.Windows.Forms.ComboBox()
        Me.cmbFamilia = New System.Windows.Forms.ComboBox()
        Me.cmbProductoEstado = New System.Windows.Forms.ComboBox()
        Me.cmbUsuario = New System.Windows.Forms.ComboBox()
        Me.cmbPropietario = New System.Windows.Forms.ComboBox()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.cmbEmpresa = New System.Windows.Forms.ComboBox()
        Me.cmbBodega = New System.Windows.Forms.ComboBox()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.pnlEntidades = New DevExpress.XtraEditors.PanelControl()
        Me.cmbInterfaces = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEntidades = New DevExpress.XtraEditors.LookUpEdit()
        Me.checBoxDomingo = New System.Windows.Forms.CheckBox()
        Me.checBoxSabado = New System.Windows.Forms.CheckBox()
        Me.checBoxViernes = New System.Windows.Forms.CheckBox()
        Me.checBoxJueves = New System.Windows.Forms.CheckBox()
        Me.checBoxMiercoles = New System.Windows.Forms.CheckBox()
        Me.checBoxMartes = New System.Windows.Forms.CheckBox()
        Me.checBoxLunes = New System.Windows.Forms.CheckBox()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.cmdNuevo = New System.Windows.Forms.ToolStripButton()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdActualizar = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.txtEntidad = New DevExpress.XtraEditors.TextEdit()
        Me.txtHoraFin = New DevExpress.XtraEditors.TimeEdit()
        Me.txtFrecuencia = New System.Windows.Forms.NumericUpDown()
        Me.txtEndpoint = New DevExpress.XtraEditors.TextEdit()
        Me.txtHoraInicio = New DevExpress.XtraEditors.TimeEdit()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtdias_vida_defecto_perecederos = New DevExpress.XtraEditors.SpinEdit()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto = New DevExpress.XtraEditors.CheckEdit()
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto = New System.Windows.Forms.Label()
        Me.txtNivelMaximoExplosionAuto = New DevExpress.XtraEditors.SpinEdit()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.chkExplosionAutomaticaUbicacionPicking = New DevExpress.XtraEditors.CheckEdit()
        Me.lblexplosion_automatica_desde_ubicacion_picking = New System.Windows.Forms.Label()
        Me.seConvertirDecUMB = New DevExpress.XtraEditors.SpinEdit()
        Me.seDespacharExiParc = New DevExpress.XtraEditors.SpinEdit()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.chkRechazarPedidoIncompleto = New DevExpress.XtraEditors.CheckEdit()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.chkSAP_Control_Draft_Traslados = New DevExpress.XtraEditors.CheckEdit()
        Me.chkSAP_Control_Draft_Ajustes = New DevExpress.XtraEditors.CheckEdit()
        Me.chkInterfaceSAP = New DevExpress.XtraEditors.CheckEdit()
        Me.docBitacora = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        User_modLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        lblEstadoProductoPedidos = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        lblDiasImportacion = New System.Windows.Forms.Label()
        Label28 = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        Label30 = New System.Windows.Forms.Label()
        Label31 = New System.Windows.Forms.Label()
        Label32 = New System.Windows.Forms.Label()
        Label33 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.pnlEncabezado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEncabezado.SuspendLayout()
        CType(Me.txtCodigoBodegaProrrateo1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaProrrateo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaFacturacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcluirRececpionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkValidaSoloCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRechazarBonificacionIncompleta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInferirBonificacionPedidoSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRangoDiasImportacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoteDefectoEntradaNC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBodegaERPNC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConsiderar_Paletizado_En_Reabasto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcluirUbicacionesReabasto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecepcionGeneraHistorico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmantener_zona_picking_clavaud.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEjecutarEnDespachoAuotmaticamente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExplosionAutomaticaInterface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkImplosionAutomaticaEnInterface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkpush_ingreso_nav_desde_hh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCrearRecTransfNAV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCrearRecCompraNAV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdAcuerdoEnc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPeso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoProvProd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenerarRecAutoBD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenerarPedidoIngresoBD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArchivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlFechaVencimiento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraLP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlEntidades, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEntidades.SuspendLayout()
        CType(Me.cmbInterfaces.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEntidades.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        CType(Me.txtEntidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHoraFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFrecuencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndpoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHoraInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.txtdias_vida_defecto_perecederos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNivelMaximoExplosionAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExplosionAutomaticaUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seConvertirDecUMB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seDespacharExiParc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRechazarPedidoIncompleto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage4.SuspendLayout()
        CType(Me.chkSAP_Control_Draft_Traslados.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSAP_Control_Draft_Ajustes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInterfaceSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.docBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(608, 23)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 0
        User_modLabel.Text = "Usuario Modificó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(75, 27)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 2
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(825, 23)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 1
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(292, 27)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 3
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(68, 50)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(62, 16)
        IdEmpresaLabel.TabIndex = 0
        IdEmpresaLabel.Text = "Empresa:"
        '
        'Label1
        '
        Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(68, 83)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(54, 16)
        Label1.TabIndex = 4
        Label1.Text = "Bodega:"
        '
        'Label2
        '
        Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(68, 17)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(57, 16)
        Label2.TabIndex = 10
        Label2.Text = "Nombre:"
        '
        'Label7
        '
        Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(68, 116)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(74, 16)
        Label7.TabIndex = 6
        Label7.Text = "Propietario:"
        '
        'Label8
        '
        Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(68, 149)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(55, 16)
        Label8.TabIndex = 8
        Label8.Text = "Usuario:"
        '
        'lblEstadoProductoPedidos
        '
        lblEstadoProductoPedidos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblEstadoProductoPedidos.AutoSize = True
        lblEstadoProductoPedidos.Location = New System.Drawing.Point(68, 182)
        lblEstadoProductoPedidos.Name = "lblEstadoProductoPedidos"
        lblEstadoProductoPedidos.Size = New System.Drawing.Size(200, 16)
        lblEstadoProductoPedidos.TabIndex = 1
        lblEstadoProductoPedidos.Text = "Estado de producto para pedidos:"
        '
        'Label10
        '
        Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(68, 245)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(53, 16)
        Label10.TabIndex = 12
        Label10.Text = "Familia:"
        '
        'Label11
        '
        Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(68, 311)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(47, 16)
        Label11.TabIndex = 14
        Label11.Text = "Marca:"
        '
        'Label12
        '
        Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(68, 344)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(91, 16)
        Label12.TabIndex = 16
        Label12.Text = "Tipo Producto:"
        '
        'Label13
        '
        Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(68, 278)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(82, 16)
        Label13.TabIndex = 18
        Label13.Text = "Clasificación:"
        '
        'Label9
        '
        Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(25, 229)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(60, 16)
        Label9.TabIndex = 6
        Label9.Text = "Hora Fin:"
        '
        'Label6
        '
        Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(25, 131)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(71, 16)
        Label6.TabIndex = 0
        Label6.Text = "Ejecutable:"
        '
        'Label5
        '
        Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(25, 165)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(65, 16)
        Label5.TabIndex = 2
        Label5.Text = "End Point:"
        '
        'Label4
        '
        Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(25, 260)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(98, 16)
        Label4.TabIndex = 8
        Label4.Text = "Frecuencia Min:"
        '
        'Label3
        '
        Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(25, 197)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(73, 16)
        Label3.TabIndex = 4
        Label3.Text = "Hora Inicio:"
        '
        'Label18
        '
        Label18.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(749, 79)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(179, 16)
        Label18.TabIndex = 29
        Label18.Text = "Código proveedor producción:"
        '
        'Label19
        '
        Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(1174, 75)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(85, 16)
        Label19.TabIndex = 35
        Label19.Text = "IdAcuerdoEnc"
        '
        'Label23
        '
        Label23.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label23.AutoSize = True
        Label23.Location = New System.Drawing.Point(68, 374)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(143, 16)
        Label23.TabIndex = 52
        Label23.Text = "Código bodega ERP NC:"
        '
        'Label24
        '
        Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(68, 404)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(150, 16)
        Label24.TabIndex = 54
        Label24.Text = "Lote defecto entrada NC:"
        '
        'Label25
        '
        Label25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label25.AutoSize = True
        Label25.Location = New System.Drawing.Point(749, 23)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(149, 16)
        Label25.TabIndex = 56
        Label25.Text = "Fecha vence defecto NC:"
        '
        'Label26
        '
        Label26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(68, 214)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(224, 16)
        Label26.TabIndex = 58
        Label26.Text = "Estado de producto para nota crédito:"
        '
        'lblDiasImportacion
        '
        lblDiasImportacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblDiasImportacion.AutoSize = True
        lblDiasImportacion.Location = New System.Drawing.Point(750, 109)
        lblDiasImportacion.Name = "lblDiasImportacion"
        lblDiasImportacion.Size = New System.Drawing.Size(146, 16)
        lblDiasImportacion.TabIndex = 63
        lblDiasImportacion.Text = "Rango días importación:"
        '
        'Label28
        '
        Label28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label28.AutoSize = True
        Label28.Location = New System.Drawing.Point(68, 464)
        Label28.Name = "Label28"
        Label28.Size = New System.Drawing.Size(164, 16)
        Label28.TabIndex = 68
        Label28.Text = "Código bodega facturación:"
        '
        'Label29
        '
        Label29.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label29.AutoSize = True
        Label29.Location = New System.Drawing.Point(68, 494)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(155, 16)
        Label29.TabIndex = 70
        Label29.Text = "Código bodega prorrateo:"
        '
        'Label30
        '
        Label30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label30.AutoSize = True
        Label30.Location = New System.Drawing.Point(68, 524)
        Label30.Name = "Label30"
        Label30.Size = New System.Drawing.Size(162, 16)
        Label30.TabIndex = 72
        Label30.Text = "Código bodega prorrateo1:"
        '
        'Label31
        '
        Label31.AutoSize = True
        Label31.Location = New System.Drawing.Point(25, 54)
        Label31.Name = "Label31"
        Label31.Size = New System.Drawing.Size(30, 16)
        Label31.TabIndex = 49
        Label31.Text = "Dia:"
        '
        'Label32
        '
        Label32.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label32.AutoSize = True
        Label32.Location = New System.Drawing.Point(25, 96)
        Label32.Name = "Label32"
        Label32.Size = New System.Drawing.Size(63, 16)
        Label32.TabIndex = 52
        Label32.Text = "Interface:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1509, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 838)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1509, 30)
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(611, 42)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_modTextEdit.TabIndex = 4
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(78, 47)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.User_agrTextEdit.TabIndex = 6
        '
        'Fec_modTextEdit
        '
        Me.Fec_modTextEdit.Enabled = False
        Me.Fec_modTextEdit.Location = New System.Drawing.Point(824, 42)
        Me.Fec_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modTextEdit.Name = "Fec_modTextEdit"
        Me.Fec_modTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_modTextEdit.TabIndex = 5
        '
        'Fec_agrTextEdit
        '
        Me.Fec_agrTextEdit.Enabled = False
        Me.Fec_agrTextEdit.Location = New System.Drawing.Point(292, 47)
        Me.Fec_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrTextEdit.Name = "Fec_agrTextEdit"
        Me.Fec_agrTextEdit.Size = New System.Drawing.Size(205, 22)
        Me.Fec_agrTextEdit.TabIndex = 7
        '
        'XtraTabControl
        '
        Me.XtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabControl.Name = "XtraTabControl"
        Me.XtraTabControl.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl.Size = New System.Drawing.Size(1509, 619)
        Me.XtraTabControl.TabIndex = 0
        Me.XtraTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2, Me.XtraTabPage3, Me.XtraTabPage4})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.pnlEncabezado)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1507, 589)
        Me.XtraTabPage1.Text = "Configuración"
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.AllowTouchScroll = True
        Me.pnlEncabezado.AutoSize = True
        Me.pnlEncabezado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlEncabezado.Controls.Add(Me.txtCodigoBodegaProrrateo1)
        Me.pnlEncabezado.Controls.Add(Label30)
        Me.pnlEncabezado.Controls.Add(Me.txtCodigoBodegaProrrateo)
        Me.pnlEncabezado.Controls.Add(Label29)
        Me.pnlEncabezado.Controls.Add(Me.txtCodigoBodegaFacturacion)
        Me.pnlEncabezado.Controls.Add(Label28)
        Me.pnlEncabezado.Controls.Add(Me.chkExcluirRececpionPicking)
        Me.pnlEncabezado.Controls.Add(Me.chkValidaSoloCodigo)
        Me.pnlEncabezado.Controls.Add(Me.chkRechazarBonificacionIncompleta)
        Me.pnlEncabezado.Controls.Add(Me.chkInferirBonificacionPedidoSAP)
        Me.pnlEncabezado.Controls.Add(lblDiasImportacion)
        Me.pnlEncabezado.Controls.Add(Me.nudRangoDiasImportacion)
        Me.pnlEncabezado.Controls.Add(Me.cmbIndiceRotacion)
        Me.pnlEncabezado.Controls.Add(Me.Label27)
        Me.pnlEncabezado.Controls.Add(Label26)
        Me.pnlEncabezado.Controls.Add(Me.cmbIdEstadoProductoNC)
        Me.pnlEncabezado.Controls.Add(Me.dtpVenceDefectoNC)
        Me.pnlEncabezado.Controls.Add(Label25)
        Me.pnlEncabezado.Controls.Add(Me.txtLoteDefectoEntradaNC)
        Me.pnlEncabezado.Controls.Add(Label24)
        Me.pnlEncabezado.Controls.Add(Me.txtCodigoBodegaERPNC)
        Me.pnlEncabezado.Controls.Add(Label23)
        Me.pnlEncabezado.Controls.Add(Me.chkConsiderar_Paletizado_En_Reabasto)
        Me.pnlEncabezado.Controls.Add(Me.chkExcluirUbicacionesReabasto)
        Me.pnlEncabezado.Controls.Add(Me.chkRecepcionGeneraHistorico)
        Me.pnlEncabezado.Controls.Add(Me.chkmantener_zona_picking_clavaud)
        Me.pnlEncabezado.Controls.Add(Me.cmbTipoRotacion)
        Me.pnlEncabezado.Controls.Add(Me.Label20)
        Me.pnlEncabezado.Controls.Add(Me.chkEjecutarEnDespachoAuotmaticamente)
        Me.pnlEncabezado.Controls.Add(Me.chkExplosionAutomaticaInterface)
        Me.pnlEncabezado.Controls.Add(Me.chkImplosionAutomaticaEnInterface)
        Me.pnlEncabezado.Controls.Add(Me.chkpush_ingreso_nav_desde_hh)
        Me.pnlEncabezado.Controls.Add(Me.chkCrearRecTransfNAV)
        Me.pnlEncabezado.Controls.Add(Me.chkCrearRecCompraNAV)
        Me.pnlEncabezado.Controls.Add(Me.cmbEtiqueta)
        Me.pnlEncabezado.Controls.Add(Me.lblEtiqueta)
        Me.pnlEncabezado.Controls.Add(Me.txtIdAcuerdoEnc)
        Me.pnlEncabezado.Controls.Add(Label19)
        Me.pnlEncabezado.Controls.Add(Me.chkControlPeso)
        Me.pnlEncabezado.Controls.Add(Me.txtCodigoProvProd)
        Me.pnlEncabezado.Controls.Add(Label18)
        Me.pnlEncabezado.Controls.Add(Me.chkGenerarRecAutoBD)
        Me.pnlEncabezado.Controls.Add(Me.chkGenerarPedidoIngresoBD)
        Me.pnlEncabezado.Controls.Add(Me.chkControlLote)
        Me.pnlEncabezado.Controls.Add(Me.txtArchivo)
        Me.pnlEncabezado.Controls.Add(Me.chkControlFechaVencimiento)
        Me.pnlEncabezado.Controls.Add(Me.Label14)
        Me.pnlEncabezado.Controls.Add(Me.chkGeneraLP)
        Me.pnlEncabezado.Controls.Add(Label13)
        Me.pnlEncabezado.Controls.Add(Me.cmbClasificación)
        Me.pnlEncabezado.Controls.Add(Label12)
        Me.pnlEncabezado.Controls.Add(Me.cmbTipoProducto)
        Me.pnlEncabezado.Controls.Add(Label11)
        Me.pnlEncabezado.Controls.Add(Me.cmbMarca)
        Me.pnlEncabezado.Controls.Add(Label10)
        Me.pnlEncabezado.Controls.Add(Me.cmbFamilia)
        Me.pnlEncabezado.Controls.Add(lblEstadoProductoPedidos)
        Me.pnlEncabezado.Controls.Add(Me.cmbProductoEstado)
        Me.pnlEncabezado.Controls.Add(Label8)
        Me.pnlEncabezado.Controls.Add(Me.cmbUsuario)
        Me.pnlEncabezado.Controls.Add(Label7)
        Me.pnlEncabezado.Controls.Add(Me.cmbPropietario)
        Me.pnlEncabezado.Controls.Add(Me.txtNombre)
        Me.pnlEncabezado.Controls.Add(Label2)
        Me.pnlEncabezado.Controls.Add(Label1)
        Me.pnlEncabezado.Controls.Add(IdEmpresaLabel)
        Me.pnlEncabezado.Controls.Add(Me.cmbEmpresa)
        Me.pnlEncabezado.Controls.Add(Me.cmbBodega)
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(1507, 589)
        Me.pnlEncabezado.TabIndex = 0
        '
        'txtCodigoBodegaProrrateo1
        '
        Me.txtCodigoBodegaProrrateo1.EditValue = ""
        Me.txtCodigoBodegaProrrateo1.Location = New System.Drawing.Point(300, 520)
        Me.txtCodigoBodegaProrrateo1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBodegaProrrateo1.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaProrrateo1.Name = "txtCodigoBodegaProrrateo1"
        Me.txtCodigoBodegaProrrateo1.Properties.MaxLength = 256
        Me.txtCodigoBodegaProrrateo1.Size = New System.Drawing.Size(431, 22)
        Me.txtCodigoBodegaProrrateo1.TabIndex = 73
        '
        'txtCodigoBodegaProrrateo
        '
        Me.txtCodigoBodegaProrrateo.EditValue = ""
        Me.txtCodigoBodegaProrrateo.Location = New System.Drawing.Point(300, 490)
        Me.txtCodigoBodegaProrrateo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBodegaProrrateo.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaProrrateo.Name = "txtCodigoBodegaProrrateo"
        Me.txtCodigoBodegaProrrateo.Properties.MaxLength = 256
        Me.txtCodigoBodegaProrrateo.Size = New System.Drawing.Size(431, 22)
        Me.txtCodigoBodegaProrrateo.TabIndex = 71
        '
        'txtCodigoBodegaFacturacion
        '
        Me.txtCodigoBodegaFacturacion.EditValue = ""
        Me.txtCodigoBodegaFacturacion.Location = New System.Drawing.Point(300, 460)
        Me.txtCodigoBodegaFacturacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBodegaFacturacion.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaFacturacion.Name = "txtCodigoBodegaFacturacion"
        Me.txtCodigoBodegaFacturacion.Properties.MaxLength = 256
        Me.txtCodigoBodegaFacturacion.Size = New System.Drawing.Size(431, 22)
        Me.txtCodigoBodegaFacturacion.TabIndex = 69
        '
        'chkExcluirRececpionPicking
        '
        Me.chkExcluirRececpionPicking.Location = New System.Drawing.Point(1122, 420)
        Me.chkExcluirRececpionPicking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkExcluirRececpionPicking.MenuManager = Me.RibbonControl
        Me.chkExcluirRececpionPicking.Name = "chkExcluirRececpionPicking"
        Me.chkExcluirRececpionPicking.Properties.Caption = "Excluir recepción de picking"
        Me.chkExcluirRececpionPicking.Size = New System.Drawing.Size(256, 24)
        Me.chkExcluirRececpionPicking.TabIndex = 67
        '
        'chkValidaSoloCodigo
        '
        Me.chkValidaSoloCodigo.Location = New System.Drawing.Point(1122, 387)
        Me.chkValidaSoloCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkValidaSoloCodigo.MenuManager = Me.RibbonControl
        Me.chkValidaSoloCodigo.Name = "chkValidaSoloCodigo"
        Me.chkValidaSoloCodigo.Properties.Caption = "Valida sólo código"
        Me.chkValidaSoloCodigo.Size = New System.Drawing.Size(256, 24)
        Me.chkValidaSoloCodigo.TabIndex = 66
        '
        'chkRechazarBonificacionIncompleta
        '
        Me.chkRechazarBonificacionIncompleta.Location = New System.Drawing.Point(1122, 354)
        Me.chkRechazarBonificacionIncompleta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRechazarBonificacionIncompleta.MenuManager = Me.RibbonControl
        Me.chkRechazarBonificacionIncompleta.Name = "chkRechazarBonificacionIncompleta"
        Me.chkRechazarBonificacionIncompleta.Properties.Caption = "Rechazar bonificación incompleta"
        Me.chkRechazarBonificacionIncompleta.Size = New System.Drawing.Size(256, 24)
        Me.chkRechazarBonificacionIncompleta.TabIndex = 65
        '
        'chkInferirBonificacionPedidoSAP
        '
        Me.chkInferirBonificacionPedidoSAP.Location = New System.Drawing.Point(1122, 321)
        Me.chkInferirBonificacionPedidoSAP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkInferirBonificacionPedidoSAP.MenuManager = Me.RibbonControl
        Me.chkInferirBonificacionPedidoSAP.Name = "chkInferirBonificacionPedidoSAP"
        Me.chkInferirBonificacionPedidoSAP.Properties.Caption = "Inferir bonificación pedido SAP"
        Me.chkInferirBonificacionPedidoSAP.Size = New System.Drawing.Size(256, 24)
        Me.chkInferirBonificacionPedidoSAP.TabIndex = 64
        '
        'nudRangoDiasImportacion
        '
        Me.nudRangoDiasImportacion.Location = New System.Drawing.Point(981, 109)
        Me.nudRangoDiasImportacion.Name = "nudRangoDiasImportacion"
        Me.nudRangoDiasImportacion.Size = New System.Drawing.Size(135, 23)
        Me.nudRangoDiasImportacion.TabIndex = 62
        '
        'cmbIndiceRotacion
        '
        Me.cmbIndiceRotacion.Location = New System.Drawing.Point(981, 51)
        Me.cmbIndiceRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbIndiceRotacion.MenuManager = Me.RibbonControl
        Me.cmbIndiceRotacion.Name = "cmbIndiceRotacion"
        Me.cmbIndiceRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbIndiceRotacion.Properties.NullText = ""
        Me.cmbIndiceRotacion.Size = New System.Drawing.Size(135, 22)
        Me.cmbIndiceRotacion.TabIndex = 61
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(750, 55)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(109, 16)
        Me.Label27.TabIndex = 60
        Me.Label27.Text = "Índice de rotación"
        '
        'cmbIdEstadoProductoNC
        '
        Me.cmbIdEstadoProductoNC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdEstadoProductoNC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbIdEstadoProductoNC.ForeColor = System.Drawing.Color.Navy
        Me.cmbIdEstadoProductoNC.Location = New System.Drawing.Point(300, 207)
        Me.cmbIdEstadoProductoNC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbIdEstadoProductoNC.Name = "cmbIdEstadoProductoNC"
        Me.cmbIdEstadoProductoNC.Size = New System.Drawing.Size(431, 25)
        Me.cmbIdEstadoProductoNC.TabIndex = 59
        '
        'dtpVenceDefectoNC
        '
        Me.dtpVenceDefectoNC.CustomFormat = "dd/MM/yyyy"
        Me.dtpVenceDefectoNC.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVenceDefectoNC.Location = New System.Drawing.Point(980, 21)
        Me.dtpVenceDefectoNC.Name = "dtpVenceDefectoNC"
        Me.dtpVenceDefectoNC.Size = New System.Drawing.Size(136, 23)
        Me.dtpVenceDefectoNC.TabIndex = 57
        '
        'txtLoteDefectoEntradaNC
        '
        Me.txtLoteDefectoEntradaNC.EditValue = ""
        Me.txtLoteDefectoEntradaNC.Location = New System.Drawing.Point(300, 400)
        Me.txtLoteDefectoEntradaNC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLoteDefectoEntradaNC.MenuManager = Me.RibbonControl
        Me.txtLoteDefectoEntradaNC.Name = "txtLoteDefectoEntradaNC"
        Me.txtLoteDefectoEntradaNC.Properties.MaxLength = 256
        Me.txtLoteDefectoEntradaNC.Size = New System.Drawing.Size(431, 22)
        Me.txtLoteDefectoEntradaNC.TabIndex = 55
        '
        'txtCodigoBodegaERPNC
        '
        Me.txtCodigoBodegaERPNC.EditValue = ""
        Me.txtCodigoBodegaERPNC.Location = New System.Drawing.Point(300, 370)
        Me.txtCodigoBodegaERPNC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBodegaERPNC.MenuManager = Me.RibbonControl
        Me.txtCodigoBodegaERPNC.Name = "txtCodigoBodegaERPNC"
        Me.txtCodigoBodegaERPNC.Properties.MaxLength = 256
        Me.txtCodigoBodegaERPNC.Size = New System.Drawing.Size(431, 22)
        Me.txtCodigoBodegaERPNC.TabIndex = 53
        '
        'chkConsiderar_Paletizado_En_Reabasto
        '
        Me.chkConsiderar_Paletizado_En_Reabasto.Location = New System.Drawing.Point(1122, 289)
        Me.chkConsiderar_Paletizado_En_Reabasto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkConsiderar_Paletizado_En_Reabasto.MenuManager = Me.RibbonControl
        Me.chkConsiderar_Paletizado_En_Reabasto.Name = "chkConsiderar_Paletizado_En_Reabasto"
        Me.chkConsiderar_Paletizado_En_Reabasto.Properties.Caption = "Considerar paletizado en reabasto"
        Me.chkConsiderar_Paletizado_En_Reabasto.Size = New System.Drawing.Size(256, 24)
        Me.chkConsiderar_Paletizado_En_Reabasto.TabIndex = 51
        '
        'chkExcluirUbicacionesReabasto
        '
        Me.chkExcluirUbicacionesReabasto.Location = New System.Drawing.Point(1122, 255)
        Me.chkExcluirUbicacionesReabasto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkExcluirUbicacionesReabasto.MenuManager = Me.RibbonControl
        Me.chkExcluirUbicacionesReabasto.Name = "chkExcluirUbicacionesReabasto"
        Me.chkExcluirUbicacionesReabasto.Properties.Caption = "Excluir ubicaciones de reabasto"
        Me.chkExcluirUbicacionesReabasto.Size = New System.Drawing.Size(226, 24)
        Me.chkExcluirUbicacionesReabasto.TabIndex = 50
        '
        'chkRecepcionGeneraHistorico
        '
        Me.chkRecepcionGeneraHistorico.Location = New System.Drawing.Point(1122, 219)
        Me.chkRecepcionGeneraHistorico.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRecepcionGeneraHistorico.MenuManager = Me.RibbonControl
        Me.chkRecepcionGeneraHistorico.Name = "chkRecepcionGeneraHistorico"
        Me.chkRecepcionGeneraHistorico.Properties.Caption = "Recepción genera histórico"
        Me.chkRecepcionGeneraHistorico.Size = New System.Drawing.Size(196, 24)
        Me.chkRecepcionGeneraHistorico.TabIndex = 49
        '
        'chkmantener_zona_picking_clavaud
        '
        Me.chkmantener_zona_picking_clavaud.Location = New System.Drawing.Point(753, 370)
        Me.chkmantener_zona_picking_clavaud.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkmantener_zona_picking_clavaud.MenuManager = Me.RibbonControl
        Me.chkmantener_zona_picking_clavaud.Name = "chkmantener_zona_picking_clavaud"
        Me.chkmantener_zona_picking_clavaud.Properties.Caption = "Conservar Zona de Picking (Modelo Clavaud)"
        Me.chkmantener_zona_picking_clavaud.Size = New System.Drawing.Size(298, 24)
        Me.chkmantener_zona_picking_clavaud.TabIndex = 48
        Me.chkmantener_zona_picking_clavaud.ToolTip = resources.GetString("chkmantener_zona_picking_clavaud.ToolTip")
        '
        'cmbTipoRotacion
        '
        Me.cmbTipoRotacion.Location = New System.Drawing.Point(1285, 44)
        Me.cmbTipoRotacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTipoRotacion.MenuManager = Me.RibbonControl
        Me.cmbTipoRotacion.Name = "cmbTipoRotacion"
        Me.cmbTipoRotacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoRotacion.Properties.NullText = ""
        Me.cmbTipoRotacion.Size = New System.Drawing.Size(135, 22)
        Me.cmbTipoRotacion.TabIndex = 47
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(1174, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(82, 16)
        Me.Label20.TabIndex = 46
        Me.Label20.Text = "Tipo rotación"
        '
        'chkEjecutarEnDespachoAuotmaticamente
        '
        Me.chkEjecutarEnDespachoAuotmaticamente.Location = New System.Drawing.Point(753, 338)
        Me.chkEjecutarEnDespachoAuotmaticamente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEjecutarEnDespachoAuotmaticamente.MenuManager = Me.RibbonControl
        Me.chkEjecutarEnDespachoAuotmaticamente.Name = "chkEjecutarEnDespachoAuotmaticamente"
        Me.chkEjecutarEnDespachoAuotmaticamente.Properties.Caption = "Ejecutar en despacho automáticamente"
        Me.chkEjecutarEnDespachoAuotmaticamente.Size = New System.Drawing.Size(374, 24)
        Me.chkEjecutarEnDespachoAuotmaticamente.TabIndex = 45
        Me.chkEjecutarEnDespachoAuotmaticamente.ToolTip = "#EJC20220307: Si está habilitado y se tiene un Id de  configuración de interface " &
    "válido, entonces la interface correspondiente configurada se ejecuta después de " &
    "cada transacción de despaacho. "
        '
        'chkExplosionAutomaticaInterface
        '
        Me.chkExplosionAutomaticaInterface.Location = New System.Drawing.Point(1122, 178)
        Me.chkExplosionAutomaticaInterface.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkExplosionAutomaticaInterface.MenuManager = Me.RibbonControl
        Me.chkExplosionAutomaticaInterface.Name = "chkExplosionAutomaticaInterface"
        Me.chkExplosionAutomaticaInterface.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExplosionAutomaticaInterface.Properties.Appearance.Options.UseFont = True
        Me.chkExplosionAutomaticaInterface.Properties.Caption = "Explosión automática en interface (Presentación a UMBas)"
        Me.chkExplosionAutomaticaInterface.Properties.ReadOnly = True
        Me.chkExplosionAutomaticaInterface.Size = New System.Drawing.Size(374, 24)
        Me.chkExplosionAutomaticaInterface.TabIndex = 43
        Me.chkExplosionAutomaticaInterface.ToolTip = "Determina si cuando el producto se solicita en umbas y solo hay disponible en pre" &
    "sentación "
        '
        'chkImplosionAutomaticaEnInterface
        '
        Me.chkImplosionAutomaticaEnInterface.Location = New System.Drawing.Point(1122, 141)
        Me.chkImplosionAutomaticaEnInterface.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkImplosionAutomaticaEnInterface.MenuManager = Me.RibbonControl
        Me.chkImplosionAutomaticaEnInterface.Name = "chkImplosionAutomaticaEnInterface"
        Me.chkImplosionAutomaticaEnInterface.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImplosionAutomaticaEnInterface.Properties.Appearance.Options.UseFont = True
        Me.chkImplosionAutomaticaEnInterface.Properties.Caption = "Implosión automática en interface (Umbas - > Presentación)"
        Me.chkImplosionAutomaticaEnInterface.Properties.ReadOnly = True
        Me.chkImplosionAutomaticaEnInterface.Size = New System.Drawing.Size(374, 24)
        Me.chkImplosionAutomaticaEnInterface.TabIndex = 42
        Me.chkImplosionAutomaticaEnInterface.ToolTip = "Determina si cuando el producto se solicita en presentación y solo hay umbas y se" &
    " corresponde con el factor."
        '
        'chkpush_ingreso_nav_desde_hh
        '
        Me.chkpush_ingreso_nav_desde_hh.Location = New System.Drawing.Point(753, 305)
        Me.chkpush_ingreso_nav_desde_hh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkpush_ingreso_nav_desde_hh.MenuManager = Me.RibbonControl
        Me.chkpush_ingreso_nav_desde_hh.Name = "chkpush_ingreso_nav_desde_hh"
        Me.chkpush_ingreso_nav_desde_hh.Properties.Caption = "Push Ingreso NAV Desde HH"
        Me.chkpush_ingreso_nav_desde_hh.Size = New System.Drawing.Size(257, 24)
        Me.chkpush_ingreso_nav_desde_hh.TabIndex = 41
        '
        'chkCrearRecTransfNAV
        '
        Me.chkCrearRecTransfNAV.Location = New System.Drawing.Point(753, 272)
        Me.chkCrearRecTransfNAV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkCrearRecTransfNAV.MenuManager = Me.RibbonControl
        Me.chkCrearRecTransfNAV.Name = "chkCrearRecTransfNAV"
        Me.chkCrearRecTransfNAV.Properties.Caption = "Crear recepcion_De_Transferencia_NAV"
        Me.chkCrearRecTransfNAV.Size = New System.Drawing.Size(257, 24)
        Me.chkCrearRecTransfNAV.TabIndex = 40
        '
        'chkCrearRecCompraNAV
        '
        Me.chkCrearRecCompraNAV.Location = New System.Drawing.Point(753, 240)
        Me.chkCrearRecCompraNAV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkCrearRecCompraNAV.MenuManager = Me.RibbonControl
        Me.chkCrearRecCompraNAV.Name = "chkCrearRecCompraNAV"
        Me.chkCrearRecCompraNAV.Properties.Caption = "Crear recepción automática para documentos de ingreso"
        Me.chkCrearRecCompraNAV.Size = New System.Drawing.Size(374, 24)
        Me.chkCrearRecCompraNAV.TabIndex = 39
        '
        'cmbEtiqueta
        '
        Me.cmbEtiqueta.Location = New System.Drawing.Point(1285, 14)
        Me.cmbEtiqueta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEtiqueta.MenuManager = Me.RibbonControl
        Me.cmbEtiqueta.Name = "cmbEtiqueta"
        Me.cmbEtiqueta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEtiqueta.Properties.NullText = ""
        Me.cmbEtiqueta.Size = New System.Drawing.Size(135, 22)
        Me.cmbEtiqueta.TabIndex = 38
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.AutoSize = True
        Me.lblEtiqueta.Location = New System.Drawing.Point(1174, 20)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(53, 16)
        Me.lblEtiqueta.TabIndex = 37
        Me.lblEtiqueta.Text = "Etiqueta"
        '
        'txtIdAcuerdoEnc
        '
        Me.txtIdAcuerdoEnc.EditValue = ""
        Me.txtIdAcuerdoEnc.Location = New System.Drawing.Point(1285, 74)
        Me.txtIdAcuerdoEnc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdAcuerdoEnc.MenuManager = Me.RibbonControl
        Me.txtIdAcuerdoEnc.Name = "txtIdAcuerdoEnc"
        Me.txtIdAcuerdoEnc.Properties.MaxLength = 50
        Me.txtIdAcuerdoEnc.Size = New System.Drawing.Size(135, 22)
        Me.txtIdAcuerdoEnc.TabIndex = 36
        '
        'chkControlPeso
        '
        Me.chkControlPeso.Location = New System.Drawing.Point(753, 208)
        Me.chkControlPeso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkControlPeso.MenuManager = Me.RibbonControl
        Me.chkControlPeso.Name = "chkControlPeso"
        Me.chkControlPeso.Properties.Caption = "Control peso"
        Me.chkControlPeso.Size = New System.Drawing.Size(107, 24)
        Me.chkControlPeso.TabIndex = 34
        '
        'txtCodigoProvProd
        '
        Me.txtCodigoProvProd.EditValue = ""
        Me.txtCodigoProvProd.Location = New System.Drawing.Point(981, 80)
        Me.txtCodigoProvProd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoProvProd.MenuManager = Me.RibbonControl
        Me.txtCodigoProvProd.Name = "txtCodigoProvProd"
        Me.txtCodigoProvProd.Properties.MaxLength = 50
        Me.txtCodigoProvProd.Size = New System.Drawing.Size(135, 22)
        Me.txtCodigoProvProd.TabIndex = 33
        '
        'chkGenerarRecAutoBD
        '
        Me.chkGenerarRecAutoBD.Location = New System.Drawing.Point(753, 175)
        Me.chkGenerarRecAutoBD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGenerarRecAutoBD.MenuManager = Me.RibbonControl
        Me.chkGenerarRecAutoBD.Name = "chkGenerarRecAutoBD"
        Me.chkGenerarRecAutoBD.Properties.Caption = "Generar recepcion auto Bodega Destino"
        Me.chkGenerarRecAutoBD.Size = New System.Drawing.Size(274, 24)
        Me.chkGenerarRecAutoBD.TabIndex = 24
        '
        'chkGenerarPedidoIngresoBD
        '
        Me.chkGenerarPedidoIngresoBD.Location = New System.Drawing.Point(753, 143)
        Me.chkGenerarPedidoIngresoBD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGenerarPedidoIngresoBD.MenuManager = Me.RibbonControl
        Me.chkGenerarPedidoIngresoBD.Name = "chkGenerarPedidoIngresoBD"
        Me.chkGenerarPedidoIngresoBD.Properties.Caption = "Generar pedido ingreso Bodega Destino"
        Me.chkGenerarPedidoIngresoBD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGenerarPedidoIngresoBD.Size = New System.Drawing.Size(257, 24)
        Me.chkGenerarPedidoIngresoBD.TabIndex = 23
        '
        'chkControlLote
        '
        Me.chkControlLote.Location = New System.Drawing.Point(753, 402)
        Me.chkControlLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkControlLote.MenuManager = Me.RibbonControl
        Me.chkControlLote.Name = "chkControlLote"
        Me.chkControlLote.Properties.Caption = "Control por Lote"
        Me.chkControlLote.Size = New System.Drawing.Size(128, 24)
        Me.chkControlLote.TabIndex = 18
        '
        'txtArchivo
        '
        Me.txtArchivo.Location = New System.Drawing.Point(300, 430)
        Me.txtArchivo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtArchivo.Name = "txtArchivo"
        Me.txtArchivo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, True, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.txtArchivo.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtArchivo.Size = New System.Drawing.Size(431, 22)
        Me.txtArchivo.TabIndex = 22
        '
        'chkControlFechaVencimiento
        '
        Me.chkControlFechaVencimiento.Location = New System.Drawing.Point(753, 434)
        Me.chkControlFechaVencimiento.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkControlFechaVencimiento.MenuManager = Me.RibbonControl
        Me.chkControlFechaVencimiento.Name = "chkControlFechaVencimiento"
        Me.chkControlFechaVencimiento.Properties.Caption = "Controlar Vencimientos"
        Me.chkControlFechaVencimiento.Size = New System.Drawing.Size(196, 24)
        Me.chkControlFechaVencimiento.TabIndex = 19
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(68, 436)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 16)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Archivo"
        '
        'chkGeneraLP
        '
        Me.chkGeneraLP.Location = New System.Drawing.Point(753, 466)
        Me.chkGeneraLP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGeneraLP.MenuManager = Me.RibbonControl
        Me.chkGeneraLP.Name = "chkGeneraLP"
        Me.chkGeneraLP.Properties.Caption = "Generar Licencia (Automáticamente)"
        Me.chkGeneraLP.Size = New System.Drawing.Size(221, 24)
        Me.chkGeneraLP.TabIndex = 20
        '
        'cmbClasificación
        '
        Me.cmbClasificación.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificación.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClasificación.ForeColor = System.Drawing.Color.Navy
        Me.cmbClasificación.Location = New System.Drawing.Point(300, 271)
        Me.cmbClasificación.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbClasificación.Name = "cmbClasificación"
        Me.cmbClasificación.Size = New System.Drawing.Size(431, 25)
        Me.cmbClasificación.TabIndex = 19
        '
        'cmbTipoProducto
        '
        Me.cmbTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoProducto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoProducto.ForeColor = System.Drawing.Color.Navy
        Me.cmbTipoProducto.Location = New System.Drawing.Point(300, 337)
        Me.cmbTipoProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTipoProducto.Name = "cmbTipoProducto"
        Me.cmbTipoProducto.Size = New System.Drawing.Size(431, 25)
        Me.cmbTipoProducto.TabIndex = 17
        '
        'cmbMarca
        '
        Me.cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMarca.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMarca.ForeColor = System.Drawing.Color.Navy
        Me.cmbMarca.Location = New System.Drawing.Point(300, 304)
        Me.cmbMarca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbMarca.Name = "cmbMarca"
        Me.cmbMarca.Size = New System.Drawing.Size(431, 25)
        Me.cmbMarca.TabIndex = 15
        '
        'cmbFamilia
        '
        Me.cmbFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFamilia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFamilia.ForeColor = System.Drawing.Color.Navy
        Me.cmbFamilia.Location = New System.Drawing.Point(300, 238)
        Me.cmbFamilia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbFamilia.Name = "cmbFamilia"
        Me.cmbFamilia.Size = New System.Drawing.Size(431, 25)
        Me.cmbFamilia.TabIndex = 13
        '
        'cmbProductoEstado
        '
        Me.cmbProductoEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProductoEstado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProductoEstado.ForeColor = System.Drawing.Color.Navy
        Me.cmbProductoEstado.Location = New System.Drawing.Point(300, 175)
        Me.cmbProductoEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbProductoEstado.Name = "cmbProductoEstado"
        Me.cmbProductoEstado.Size = New System.Drawing.Size(431, 25)
        Me.cmbProductoEstado.TabIndex = 2
        '
        'cmbUsuario
        '
        Me.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsuario.ForeColor = System.Drawing.Color.Navy
        Me.cmbUsuario.Location = New System.Drawing.Point(300, 142)
        Me.cmbUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbUsuario.Name = "cmbUsuario"
        Me.cmbUsuario.Size = New System.Drawing.Size(431, 25)
        Me.cmbUsuario.TabIndex = 9
        '
        'cmbPropietario
        '
        Me.cmbPropietario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPropietario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPropietario.ForeColor = System.Drawing.Color.Navy
        Me.cmbPropietario.Location = New System.Drawing.Point(300, 109)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Size = New System.Drawing.Size(431, 25)
        Me.cmbPropietario.TabIndex = 7
        '
        'txtNombre
        '
        Me.txtNombre.EditValue = ""
        Me.txtNombre.Location = New System.Drawing.Point(300, 13)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Properties.MaxLength = 256
        Me.txtNombre.Size = New System.Drawing.Size(253, 22)
        Me.txtNombre.TabIndex = 11
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpresa.ForeColor = System.Drawing.Color.Navy
        Me.cmbEmpresa.Location = New System.Drawing.Point(300, 43)
        Me.cmbEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Size = New System.Drawing.Size(431, 25)
        Me.cmbEmpresa.TabIndex = 3
        '
        'cmbBodega
        '
        Me.cmbBodega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBodega.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodega.ForeColor = System.Drawing.Color.Navy
        Me.cmbBodega.Location = New System.Drawing.Point(300, 76)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Size = New System.Drawing.Size(431, 25)
        Me.cmbBodega.TabIndex = 5
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.Dgrid)
        Me.XtraTabPage2.Controls.Add(Me.pnlEntidades)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1507, 589)
        Me.XtraTabPage2.Text = "programación semanal"
        '
        'Dgrid
        '
        Me.Dgrid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.Location = New System.Drawing.Point(0, 337)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dgrid.MenuManager = Me.RibbonControl
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(1507, 252)
        Me.Dgrid.TabIndex = 50
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'pnlEntidades
        '
        Me.pnlEntidades.Controls.Add(Label33)
        Me.pnlEntidades.Controls.Add(Me.chkActivo)
        Me.pnlEntidades.Controls.Add(Label32)
        Me.pnlEntidades.Controls.Add(Me.cmbInterfaces)
        Me.pnlEntidades.Controls.Add(Me.cmbEntidades)
        Me.pnlEntidades.Controls.Add(Me.checBoxDomingo)
        Me.pnlEntidades.Controls.Add(Me.checBoxSabado)
        Me.pnlEntidades.Controls.Add(Me.checBoxViernes)
        Me.pnlEntidades.Controls.Add(Me.checBoxJueves)
        Me.pnlEntidades.Controls.Add(Me.checBoxMiercoles)
        Me.pnlEntidades.Controls.Add(Me.checBoxMartes)
        Me.pnlEntidades.Controls.Add(Me.checBoxLunes)
        Me.pnlEntidades.Controls.Add(Label31)
        Me.pnlEntidades.Controls.Add(Me.ToolStrip)
        Me.pnlEntidades.Controls.Add(Me.txtEntidad)
        Me.pnlEntidades.Controls.Add(Me.txtHoraFin)
        Me.pnlEntidades.Controls.Add(Label9)
        Me.pnlEntidades.Controls.Add(Label6)
        Me.pnlEntidades.Controls.Add(Me.txtFrecuencia)
        Me.pnlEntidades.Controls.Add(Me.txtEndpoint)
        Me.pnlEntidades.Controls.Add(Label5)
        Me.pnlEntidades.Controls.Add(Label4)
        Me.pnlEntidades.Controls.Add(Me.txtHoraInicio)
        Me.pnlEntidades.Controls.Add(Label3)
        Me.pnlEntidades.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntidades.Location = New System.Drawing.Point(0, 0)
        Me.pnlEntidades.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlEntidades.Name = "pnlEntidades"
        Me.pnlEntidades.Size = New System.Drawing.Size(1507, 337)
        Me.pnlEntidades.TabIndex = 3
        '
        'cmbInterfaces
        '
        Me.cmbInterfaces.Location = New System.Drawing.Point(180, 93)
        Me.cmbInterfaces.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbInterfaces.MenuManager = Me.RibbonControl
        Me.cmbInterfaces.Name = "cmbInterfaces"
        Me.cmbInterfaces.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbInterfaces.Properties.NullText = ""
        Me.cmbInterfaces.Size = New System.Drawing.Size(295, 22)
        Me.cmbInterfaces.TabIndex = 51
        '
        'cmbEntidades
        '
        Me.cmbEntidades.Location = New System.Drawing.Point(180, 128)
        Me.cmbEntidades.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEntidades.MenuManager = Me.RibbonControl
        Me.cmbEntidades.Name = "cmbEntidades"
        Me.cmbEntidades.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEntidades.Properties.NullText = ""
        Me.cmbEntidades.Size = New System.Drawing.Size(295, 22)
        Me.cmbEntidades.TabIndex = 50
        '
        'checBoxDomingo
        '
        Me.checBoxDomingo.AutoSize = True
        Me.checBoxDomingo.Location = New System.Drawing.Point(460, 53)
        Me.checBoxDomingo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxDomingo.Name = "checBoxDomingo"
        Me.checBoxDomingo.Size = New System.Drawing.Size(37, 20)
        Me.checBoxDomingo.TabIndex = 48
        Me.checBoxDomingo.Tag = "7"
        Me.checBoxDomingo.Text = "D"
        Me.checBoxDomingo.UseVisualStyleBackColor = True
        '
        'checBoxSabado
        '
        Me.checBoxSabado.AutoSize = True
        Me.checBoxSabado.Location = New System.Drawing.Point(409, 53)
        Me.checBoxSabado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxSabado.Name = "checBoxSabado"
        Me.checBoxSabado.Size = New System.Drawing.Size(37, 20)
        Me.checBoxSabado.TabIndex = 47
        Me.checBoxSabado.Tag = "6"
        Me.checBoxSabado.Text = "S"
        Me.checBoxSabado.UseVisualStyleBackColor = True
        '
        'checBoxViernes
        '
        Me.checBoxViernes.AutoSize = True
        Me.checBoxViernes.Location = New System.Drawing.Point(364, 53)
        Me.checBoxViernes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxViernes.Name = "checBoxViernes"
        Me.checBoxViernes.Size = New System.Drawing.Size(37, 20)
        Me.checBoxViernes.TabIndex = 46
        Me.checBoxViernes.Tag = "5"
        Me.checBoxViernes.Text = "V"
        Me.checBoxViernes.UseVisualStyleBackColor = True
        '
        'checBoxJueves
        '
        Me.checBoxJueves.AutoSize = True
        Me.checBoxJueves.Location = New System.Drawing.Point(319, 53)
        Me.checBoxJueves.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxJueves.Name = "checBoxJueves"
        Me.checBoxJueves.Size = New System.Drawing.Size(34, 20)
        Me.checBoxJueves.TabIndex = 45
        Me.checBoxJueves.Tag = "4"
        Me.checBoxJueves.Text = "J"
        Me.checBoxJueves.UseVisualStyleBackColor = True
        '
        'checBoxMiercoles
        '
        Me.checBoxMiercoles.AutoSize = True
        Me.checBoxMiercoles.Location = New System.Drawing.Point(270, 53)
        Me.checBoxMiercoles.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxMiercoles.Name = "checBoxMiercoles"
        Me.checBoxMiercoles.Size = New System.Drawing.Size(39, 20)
        Me.checBoxMiercoles.TabIndex = 44
        Me.checBoxMiercoles.Tag = "3"
        Me.checBoxMiercoles.Text = "M"
        Me.checBoxMiercoles.UseVisualStyleBackColor = True
        '
        'checBoxMartes
        '
        Me.checBoxMartes.AutoSize = True
        Me.checBoxMartes.Location = New System.Drawing.Point(223, 53)
        Me.checBoxMartes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxMartes.Name = "checBoxMartes"
        Me.checBoxMartes.Size = New System.Drawing.Size(39, 20)
        Me.checBoxMartes.TabIndex = 43
        Me.checBoxMartes.Tag = "2"
        Me.checBoxMartes.Text = "M"
        Me.checBoxMartes.UseVisualStyleBackColor = True
        '
        'checBoxLunes
        '
        Me.checBoxLunes.AutoSize = True
        Me.checBoxLunes.Location = New System.Drawing.Point(180, 53)
        Me.checBoxLunes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checBoxLunes.Name = "checBoxLunes"
        Me.checBoxLunes.Size = New System.Drawing.Size(35, 20)
        Me.checBoxLunes.TabIndex = 42
        Me.checBoxLunes.Tag = "1"
        Me.checBoxLunes.Text = "L"
        Me.checBoxLunes.UseVisualStyleBackColor = True
        '
        'ToolStrip
        '
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdNuevo, Me.cmdAdd, Me.cmdActualizar, Me.cmdDelete})
        Me.ToolStrip.Location = New System.Drawing.Point(2, 2)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1503, 27)
        Me.ToolStrip.TabIndex = 41
        Me.ToolStrip.Text = "ToolStrip4"
        '
        'cmdNuevo
        '
        Me.cmdNuevo.Image = CType(resources.GetObject("cmdNuevo.Image"), System.Drawing.Image)
        Me.cmdNuevo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNuevo.Name = "cmdNuevo"
        Me.cmdNuevo.Size = New System.Drawing.Size(76, 24)
        Me.cmdNuevo.Text = "Nuevo"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(86, 24)
        Me.cmdAdd.Text = "Guardar"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Image = CType(resources.GetObject("cmdActualizar.Image"), System.Drawing.Image)
        Me.cmdActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdActualizar.Name = "cmdActualizar"
        Me.cmdActualizar.Size = New System.Drawing.Size(99, 24)
        Me.cmdActualizar.Text = "Actualizar"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(102, 24)
        Me.cmdDelete.Text = "Desactivar"
        Me.cmdDelete.Visible = False
        '
        'txtEntidad
        '
        Me.txtEntidad.EditValue = ""
        Me.txtEntidad.Location = New System.Drawing.Point(759, 85)
        Me.txtEntidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEntidad.MenuManager = Me.RibbonControl
        Me.txtEntidad.Name = "txtEntidad"
        Me.txtEntidad.Size = New System.Drawing.Size(157, 22)
        Me.txtEntidad.TabIndex = 40
        Me.txtEntidad.Visible = False
        '
        'txtHoraFin
        '
        Me.txtHoraFin.EditValue = New Date(2017, 9, 8, 0, 0, 0, 0)
        Me.txtHoraFin.Location = New System.Drawing.Point(178, 226)
        Me.txtHoraFin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtHoraFin.MenuManager = Me.RibbonControl
        Me.txtHoraFin.Name = "txtHoraFin"
        Me.txtHoraFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtHoraFin.Size = New System.Drawing.Size(297, 24)
        Me.txtHoraFin.TabIndex = 7
        '
        'txtFrecuencia
        '
        Me.txtFrecuencia.Location = New System.Drawing.Point(178, 258)
        Me.txtFrecuencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFrecuencia.Name = "txtFrecuencia"
        Me.txtFrecuencia.Size = New System.Drawing.Size(297, 23)
        Me.txtFrecuencia.TabIndex = 9
        '
        'txtEndpoint
        '
        Me.txtEndpoint.EditValue = ""
        Me.txtEndpoint.Location = New System.Drawing.Point(178, 162)
        Me.txtEndpoint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEndpoint.MenuManager = Me.RibbonControl
        Me.txtEndpoint.Name = "txtEndpoint"
        Me.txtEndpoint.Size = New System.Drawing.Size(297, 22)
        Me.txtEndpoint.TabIndex = 3
        '
        'txtHoraInicio
        '
        Me.txtHoraInicio.EditValue = New Date(2017, 9, 8, 0, 0, 0, 0)
        Me.txtHoraInicio.Location = New System.Drawing.Point(178, 194)
        Me.txtHoraInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtHoraInicio.MenuManager = Me.RibbonControl
        Me.txtHoraInicio.Name = "txtHoraInicio"
        Me.txtHoraInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtHoraInicio.Size = New System.Drawing.Size(297, 24)
        Me.txtHoraInicio.TabIndex = 5
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.txtdias_vida_defecto_perecederos)
        Me.XtraTabPage3.Controls.Add(Me.Label22)
        Me.XtraTabPage3.Controls.Add(Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto)
        Me.XtraTabPage3.Controls.Add(Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto)
        Me.XtraTabPage3.Controls.Add(Me.txtNivelMaximoExplosionAuto)
        Me.XtraTabPage3.Controls.Add(Me.Label21)
        Me.XtraTabPage3.Controls.Add(Me.chkExplosionAutomaticaUbicacionPicking)
        Me.XtraTabPage3.Controls.Add(Me.lblexplosion_automatica_desde_ubicacion_picking)
        Me.XtraTabPage3.Controls.Add(Me.seConvertirDecUMB)
        Me.XtraTabPage3.Controls.Add(Me.seDespacharExiParc)
        Me.XtraTabPage3.Controls.Add(Me.Label17)
        Me.XtraTabPage3.Controls.Add(Me.chkRechazarPedidoIncompleto)
        Me.XtraTabPage3.Controls.Add(Me.Label16)
        Me.XtraTabPage3.Controls.Add(Me.Label15)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(1507, 589)
        Me.XtraTabPage3.Text = "Control de existencias"
        '
        'txtdias_vida_defecto_perecederos
        '
        Me.txtdias_vida_defecto_perecederos.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtdias_vida_defecto_perecederos.Location = New System.Drawing.Point(154, 143)
        Me.txtdias_vida_defecto_perecederos.MenuManager = Me.RibbonControl
        Me.txtdias_vida_defecto_perecederos.Name = "txtdias_vida_defecto_perecederos"
        Me.txtdias_vida_defecto_perecederos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtdias_vida_defecto_perecederos.Properties.IsFloatValue = False
        Me.txtdias_vida_defecto_perecederos.Properties.MaskSettings.Set("mask", "N00")
        Me.txtdias_vida_defecto_perecederos.Properties.MaxValue = New Decimal(New Integer() {365, 0, 0, 0})
        Me.txtdias_vida_defecto_perecederos.Size = New System.Drawing.Size(69, 24)
        Me.txtdias_vida_defecto_perecederos.TabIndex = 52
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(26, 352)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(179, 16)
        Me.Label22.TabIndex = 51
        Me.Label22.Text = "Días vida defecto perecederos"
        '
        'chkConsiderar_Disponibilidad_Ubicacion_Reabasto
        '
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Location = New System.Drawing.Point(154, 127)
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.MenuManager = Me.RibbonControl
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Name = "chkConsiderar_Disponibilidad_Ubicacion_Reabasto"
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Properties.Caption = ""
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Size = New System.Drawing.Size(69, 24)
        Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.TabIndex = 50
        '
        'lblConsiderar_Disponibilidad_Ubicacion_Reabasto
        '
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.AutoSize = True
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.Location = New System.Drawing.Point(33, 388)
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.Name = "lblConsiderar_Disponibilidad_Ubicacion_Reabasto"
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.Size = New System.Drawing.Size(275, 16)
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.TabIndex = 49
        Me.lblConsiderar_Disponibilidad_Ubicacion_Reabasto.Text = "Considerar_Disponibilidad_Ubicacion_Reabasto"
        '
        'txtNivelMaximoExplosionAuto
        '
        Me.txtNivelMaximoExplosionAuto.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtNivelMaximoExplosionAuto.Location = New System.Drawing.Point(154, 109)
        Me.txtNivelMaximoExplosionAuto.MenuManager = Me.RibbonControl
        Me.txtNivelMaximoExplosionAuto.Name = "txtNivelMaximoExplosionAuto"
        Me.txtNivelMaximoExplosionAuto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNivelMaximoExplosionAuto.Properties.IsFloatValue = False
        Me.txtNivelMaximoExplosionAuto.Properties.MaskSettings.Set("mask", "N00")
        Me.txtNivelMaximoExplosionAuto.Properties.MaxValue = New Decimal(New Integer() {10, 0, 0, 0})
        Me.txtNivelMaximoExplosionAuto.Size = New System.Drawing.Size(69, 24)
        Me.txtNivelMaximoExplosionAuto.TabIndex = 48
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(33, 339)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(207, 16)
        Me.Label21.TabIndex = 47
        Me.Label21.Text = "Explosion Automática Nivel Máximo"
        '
        'chkExplosionAutomaticaUbicacionPicking
        '
        Me.chkExplosionAutomaticaUbicacionPicking.Location = New System.Drawing.Point(154, 93)
        Me.chkExplosionAutomaticaUbicacionPicking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkExplosionAutomaticaUbicacionPicking.MenuManager = Me.RibbonControl
        Me.chkExplosionAutomaticaUbicacionPicking.Name = "chkExplosionAutomaticaUbicacionPicking"
        Me.chkExplosionAutomaticaUbicacionPicking.Properties.Caption = ""
        Me.chkExplosionAutomaticaUbicacionPicking.Size = New System.Drawing.Size(69, 24)
        Me.chkExplosionAutomaticaUbicacionPicking.TabIndex = 46
        '
        'lblexplosion_automatica_desde_ubicacion_picking
        '
        Me.lblexplosion_automatica_desde_ubicacion_picking.AutoSize = True
        Me.lblexplosion_automatica_desde_ubicacion_picking.Location = New System.Drawing.Point(33, 288)
        Me.lblexplosion_automatica_desde_ubicacion_picking.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblexplosion_automatica_desde_ubicacion_picking.Name = "lblexplosion_automatica_desde_ubicacion_picking"
        Me.lblexplosion_automatica_desde_ubicacion_picking.Size = New System.Drawing.Size(272, 16)
        Me.lblexplosion_automatica_desde_ubicacion_picking.TabIndex = 45
        Me.lblexplosion_automatica_desde_ubicacion_picking.Text = "Explosión Automatica Desde Ubicacion  Picking"
        '
        'seConvertirDecUMB
        '
        Me.seConvertirDecUMB.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seConvertirDecUMB.Location = New System.Drawing.Point(154, 72)
        Me.seConvertirDecUMB.MenuManager = Me.RibbonControl
        Me.seConvertirDecUMB.Name = "seConvertirDecUMB"
        Me.seConvertirDecUMB.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seConvertirDecUMB.Properties.IsFloatValue = False
        Me.seConvertirDecUMB.Properties.Mask.EditMask = "N00"
        Me.seConvertirDecUMB.Properties.MaxValue = New Decimal(New Integer() {3, 0, 0, 0})
        Me.seConvertirDecUMB.Size = New System.Drawing.Size(69, 24)
        Me.seConvertirDecUMB.TabIndex = 32
        '
        'seDespacharExiParc
        '
        Me.seDespacharExiParc.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seDespacharExiParc.Location = New System.Drawing.Point(154, 53)
        Me.seDespacharExiParc.MenuManager = Me.RibbonControl
        Me.seDespacharExiParc.Name = "seDespacharExiParc"
        Me.seDespacharExiParc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.seDespacharExiParc.Properties.IsFloatValue = False
        Me.seDespacharExiParc.Properties.MaskSettings.Set("mask", "N00")
        Me.seDespacharExiParc.Properties.MaxValue = New Decimal(New Integer() {3, 0, 0, 0})
        Me.seDespacharExiParc.Size = New System.Drawing.Size(69, 24)
        Me.seDespacharExiParc.TabIndex = 31
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(26, 180)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(205, 16)
        Me.Label17.TabIndex = 28
        Me.Label17.Text = "Convertir decimales en UM básica:"
        '
        'chkRechazarPedidoIncompleto
        '
        Me.chkRechazarPedidoIncompleto.Location = New System.Drawing.Point(154, 35)
        Me.chkRechazarPedidoIncompleto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRechazarPedidoIncompleto.MenuManager = Me.RibbonControl
        Me.chkRechazarPedidoIncompleto.Name = "chkRechazarPedidoIncompleto"
        Me.chkRechazarPedidoIncompleto.Properties.Caption = ""
        Me.chkRechazarPedidoIncompleto.Size = New System.Drawing.Size(69, 24)
        Me.chkRechazarPedidoIncompleto.TabIndex = 44
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(33, 168)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(174, 16)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "Despachar existencia parcial:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(26, 88)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(168, 16)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Rechazar pedido incompleto"
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Controls.Add(Me.chkSAP_Control_Draft_Traslados)
        Me.XtraTabPage4.Controls.Add(Me.chkSAP_Control_Draft_Ajustes)
        Me.XtraTabPage4.Controls.Add(Me.chkInterfaceSAP)
        Me.XtraTabPage4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(1507, 589)
        Me.XtraTabPage4.Text = "Configuración SAP"
        '
        'chkSAP_Control_Draft_Traslados
        '
        Me.chkSAP_Control_Draft_Traslados.Location = New System.Drawing.Point(23, 64)
        Me.chkSAP_Control_Draft_Traslados.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSAP_Control_Draft_Traslados.MenuManager = Me.RibbonControl
        Me.chkSAP_Control_Draft_Traslados.Name = "chkSAP_Control_Draft_Traslados"
        Me.chkSAP_Control_Draft_Traslados.Properties.Caption = "SAP_Control_Draft_Traslados"
        Me.chkSAP_Control_Draft_Traslados.Size = New System.Drawing.Size(98, 24)
        Me.chkSAP_Control_Draft_Traslados.TabIndex = 65
        '
        'chkSAP_Control_Draft_Ajustes
        '
        Me.chkSAP_Control_Draft_Ajustes.Location = New System.Drawing.Point(23, 48)
        Me.chkSAP_Control_Draft_Ajustes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSAP_Control_Draft_Ajustes.MenuManager = Me.RibbonControl
        Me.chkSAP_Control_Draft_Ajustes.Name = "chkSAP_Control_Draft_Ajustes"
        Me.chkSAP_Control_Draft_Ajustes.Properties.Caption = "SAP_Control_Draft_Ajustes"
        Me.chkSAP_Control_Draft_Ajustes.Size = New System.Drawing.Size(98, 24)
        Me.chkSAP_Control_Draft_Ajustes.TabIndex = 64
        '
        'chkInterfaceSAP
        '
        Me.chkInterfaceSAP.Location = New System.Drawing.Point(23, 32)
        Me.chkInterfaceSAP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkInterfaceSAP.MenuManager = Me.RibbonControl
        Me.chkInterfaceSAP.Name = "chkInterfaceSAP"
        Me.chkInterfaceSAP.Properties.Caption = "Interface SAP"
        Me.chkInterfaceSAP.Size = New System.Drawing.Size(55, 24)
        Me.chkInterfaceSAP.TabIndex = 63
        '
        'docBitacora
        '
        Me.docBitacora.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.docBitacora.Form = Me
        Me.docBitacora.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 812)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1509, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("4958cf5a-b6eb-42ec-8e5c-c27b3b609845")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 660)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 89)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1108, 110)
        Me.DockPanel1.Text = "Bitacora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modTextEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1101, 75)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(178, 292)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(94, 24)
        Me.chkActivo.TabIndex = 53
        '
        'Label33
        '
        Label33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label33.AutoSize = True
        Label33.Location = New System.Drawing.Point(25, 296)
        Label33.Name = "Label33"
        Label33.Size = New System.Drawing.Size(46, 16)
        Label33.TabIndex = 54
        Label33.Text = "Activo:"
        '
        'frmConfiguracionHorarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1509, 868)
        Me.Controls.Add(Me.XtraTabControl)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmConfiguracionHorarios"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Configuración de Ejecución"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        CType(Me.pnlEncabezado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        CType(Me.txtCodigoBodegaProrrateo1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaProrrateo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaFacturacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcluirRececpionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkValidaSoloCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRechazarBonificacionIncompleta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInferirBonificacionPedidoSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRangoDiasImportacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbIndiceRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoteDefectoEntradaNC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBodegaERPNC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConsiderar_Paletizado_En_Reabasto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcluirUbicacionesReabasto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRecepcionGeneraHistorico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmantener_zona_picking_clavaud.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoRotacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEjecutarEnDespachoAuotmaticamente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExplosionAutomaticaInterface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkImplosionAutomaticaEnInterface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkpush_ingreso_nav_desde_hh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCrearRecTransfNAV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCrearRecCompraNAV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdAcuerdoEnc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPeso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoProvProd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenerarRecAutoBD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenerarPedidoIngresoBD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArchivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlFechaVencimiento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraLP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlEntidades, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEntidades.ResumeLayout(False)
        Me.pnlEntidades.PerformLayout()
        CType(Me.cmbInterfaces.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEntidades.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        CType(Me.txtEntidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHoraFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFrecuencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndpoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHoraInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        Me.XtraTabPage3.PerformLayout()
        CType(Me.txtdias_vida_defecto_perecederos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConsiderar_Disponibilidad_Ubicacion_Reabasto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNivelMaximoExplosionAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExplosionAutomaticaUbicacionPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seConvertirDecUMB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seDespacharExiParc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRechazarPedidoIncompleto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage4.ResumeLayout(False)
        CType(Me.chkSAP_Control_Draft_Traslados.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSAP_Control_Draft_Ajustes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInterfaceSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.docBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents docBitacora As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents pnlEncabezado As DevExpress.XtraEditors.PanelControl
    Friend WithEvents seConvertirDecUMB As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents seDespacharExiParc As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents chkGenerarRecAutoBD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkGenerarPedidoIngresoBD As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlLote As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtArchivo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents chkControlFechaVencimiento As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label14 As Label
    Friend WithEvents chkGeneraLP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbClasificación As ComboBox
    Friend WithEvents cmbTipoProducto As ComboBox
    Friend WithEvents cmbMarca As ComboBox
    Friend WithEvents cmbFamilia As ComboBox
    Friend WithEvents cmbProductoEstado As ComboBox
    Friend WithEvents cmbUsuario As ComboBox
    Friend WithEvents cmbPropietario As ComboBox
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbEmpresa As ComboBox
    Friend WithEvents cmbBodega As ComboBox
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pnlEntidades As DevExpress.XtraEditors.PanelControl
    Friend WithEvents txtHoraFin As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents txtFrecuencia As NumericUpDown
    Friend WithEvents txtEndpoint As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHoraInicio As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents txtCodigoProvProd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkControlPeso As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtIdAcuerdoEnc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents HelpProvider1 As HelpProvider
    Friend WithEvents cmbEtiqueta As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEtiqueta As Label
    Friend WithEvents chkCrearRecCompraNAV As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCrearRecTransfNAV As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkpush_ingreso_nav_desde_hh As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkExplosionAutomaticaInterface As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkImplosionAutomaticaEnInterface As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRechazarPedidoIncompleto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkEjecutarEnDespachoAuotmaticamente As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbTipoRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label20 As Label
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtNivelMaximoExplosionAuto As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label21 As Label
    Friend WithEvents chkExplosionAutomaticaUbicacionPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblexplosion_automatica_desde_ubicacion_picking As Label
    Friend WithEvents chkmantener_zona_picking_clavaud As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRecepcionGeneraHistorico As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkExcluirUbicacionesReabasto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkConsiderar_Paletizado_En_Reabasto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkConsiderar_Disponibilidad_Ubicacion_Reabasto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblConsiderar_Disponibilidad_Ubicacion_Reabasto As Label
    Friend WithEvents txtdias_vida_defecto_perecederos As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Label22 As Label
    Friend WithEvents txtCodigoBodegaERPNC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtpVenceDefectoNC As DateTimePicker
    Friend WithEvents txtLoteDefectoEntradaNC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbIdEstadoProductoNC As ComboBox
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents chkSAP_Control_Draft_Traslados As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkSAP_Control_Draft_Ajustes As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkInterfaceSAP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbIndiceRotacion As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label27 As Label
    Friend WithEvents nudRangoDiasImportacion As NumericUpDown
    Friend WithEvents chkRechazarBonificacionIncompleta As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkInferirBonificacionPedidoSAP As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkValidaSoloCodigo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkExcluirRececpionPicking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCodigoBodegaProrrateo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBodegaFacturacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBodegaProrrateo1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEntidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents cmdNuevo As ToolStripButton
    Friend WithEvents cmdAdd As ToolStripButton
    Friend WithEvents cmdActualizar As ToolStripButton
    Friend WithEvents cmdDelete As ToolStripButton
    Friend WithEvents checBoxDomingo As CheckBox
    Friend WithEvents checBoxSabado As CheckBox
    Friend WithEvents checBoxViernes As CheckBox
    Friend WithEvents checBoxJueves As CheckBox
    Friend WithEvents checBoxMiercoles As CheckBox
    Friend WithEvents checBoxMartes As CheckBox
    Friend WithEvents checBoxLunes As CheckBox
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbEntidades As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbInterfaces As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
End Class
