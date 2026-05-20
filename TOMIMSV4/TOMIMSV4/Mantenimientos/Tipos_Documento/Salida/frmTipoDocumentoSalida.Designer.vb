<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTipoDocumentoSalida
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
        Dim NombreLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTipoDocumentoSalida))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControlComportamientoEnTransferenciasGeneralmente = New DevExpress.XtraEditors.GroupControl()
        Me.chkTransferirUbicacion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkGenerarRecepcionAutoBodegaDestino = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRecibirProductoAutoBodegaDestino = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkTrasladarLotesDocIngreso = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkGenerarPedidoIngresoBodegaDestino = New DevExpress.XtraEditors.ToggleSwitch()
        Me.cmbTipoDocumentoIngreso = New DevExpress.XtraEditors.LookUpEdit()
        Me.grpGenerales = New DevExpress.XtraEditors.GroupControl()
        Me.chkAsignarTodosOperadores = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkGeneraGuiaRemision = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkEmpaqueTarima = New DevExpress.XtraEditors.ToggleSwitch()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.cmbEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkVerificacionImagenBOF = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkFotografiaVerificacion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkVerificar = New DevExpress.XtraEditors.ToggleSwitch()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkMoverAMuelle = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkEscanearMuellePicking = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkEsDevolucion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkPermitirDespachoMultiple = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkpermitir_despacho_parcial = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkcontrol_cliente_en_detalle = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkMarcar_Registros_Enviados_MI3 = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirClienteEsBodegaWMS = New DevExpress.XtraEditors.ToggleSwitch()
        Me.txtObservacion = New DevExpress.XtraEditors.MemoEdit()
        Me.chkRequiereDocumentoRef = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkControlPoliza = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkPreparar = New DevExpress.XtraEditors.ToggleSwitch()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario = New DevExpress.XtraEditors.GroupControl()
        Me.chkImprimeBarrasPacking = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkImprimirBarrasPicking = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkReservaStock = New DevExpress.XtraEditors.ToggleSwitch()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkProducto_Tipo = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.chkGenerarPickingAuto = New DevExpress.XtraEditors.ToggleSwitch()
        NombreLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControlComportamientoEnTransferenciasGeneralmente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.SuspendLayout()
        CType(Me.chkTransferirUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenerarRecepcionAutoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRecibirProductoAutoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTrasladarLotesDocIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenerarPedidoIngresoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoDocumentoIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGenerales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGenerales.SuspendLayout()
        CType(Me.chkAsignarTodosOperadores.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraGuiaRemision.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEmpaqueTarima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chkVerificacionImagenBOF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFotografiaVerificacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVerificar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkMoverAMuelle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEscanearMuellePicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsDevolucion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermitirDespachoMultiple.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkpermitir_despacho_parcial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcontrol_cliente_en_detalle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMarcar_Registros_Enviados_MI3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirClienteEsBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObservacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequiereDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPreparar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.SuspendLayout()
        CType(Me.chkImprimeBarrasPacking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkImprimirBarrasPicking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReservaStock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkProducto_Tipo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.chkGenerarPickingAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(19, 86)
        NombreLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 4
        NombreLabel.Text = "Nombre:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(40, 20)
        User_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(40, 52)
        Fec_agrLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(524, 20)
        User_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(524, 52)
        Fec_modLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(19, 54)
        Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(19, 69)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(61, 16)
        IdPropietarioLabel.TabIndex = 14
        IdPropietarioLabel.Text = "Tipo D.I.:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.chkActivo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1239, 193)
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
        Me.mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'chkActivo
        '
        Me.chkActivo.BindableChecked = True
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.Id = 6
        Me.chkActivo.Name = "chkActivo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Tipo documento salida"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 854)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1239, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControlComportamientoEnTransferenciasGeneralmente)
        Me.GroupControl1.Controls.Add(Me.grpGenerales)
        Me.GroupControl1.Controls.Add(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1239, 635)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos tipo documento salida"
        '
        'GroupControlComportamientoEnTransferenciasGeneralmente
        '
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.chkTransferirUbicacion)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.chkGenerarRecepcionAutoBodegaDestino)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.chkRecibirProductoAutoBodegaDestino)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.chkTrasladarLotesDocIngreso)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.chkGenerarPedidoIngresoBodegaDestino)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(Me.cmbTipoDocumentoIngreso)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Controls.Add(IdPropietarioLabel)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Location = New System.Drawing.Point(2, 500)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Name = "GroupControlComportamientoEnTransferenciasGeneralmente"
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Size = New System.Drawing.Size(901, 218)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.TabIndex = 18
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.Text = "Comportamiento en transferencias (Generalmente)"
        '
        'chkTransferirUbicacion
        '
        Me.chkTransferirUbicacion.Location = New System.Drawing.Point(542, 90)
        Me.chkTransferirUbicacion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkTransferirUbicacion.MenuManager = Me.RibbonControl
        Me.chkTransferirUbicacion.Name = "chkTransferirUbicacion"
        Me.chkTransferirUbicacion.Properties.OffText = "No transferir ubicaciones"
        Me.chkTransferirUbicacion.Properties.OnText = "Transferir ubicaciones"
        Me.chkTransferirUbicacion.Size = New System.Drawing.Size(284, 24)
        Me.chkTransferirUbicacion.TabIndex = 33
        '
        'chkGenerarRecepcionAutoBodegaDestino
        '
        Me.chkGenerarRecepcionAutoBodegaDestino.Location = New System.Drawing.Point(542, 64)
        Me.chkGenerarRecepcionAutoBodegaDestino.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkGenerarRecepcionAutoBodegaDestino.MenuManager = Me.RibbonControl
        Me.chkGenerarRecepcionAutoBodegaDestino.Name = "chkGenerarRecepcionAutoBodegaDestino"
        Me.chkGenerarRecepcionAutoBodegaDestino.Properties.OffText = "No generar recepción auto bodega destino"
        Me.chkGenerarRecepcionAutoBodegaDestino.Properties.OnText = "Ggenerar recepción auto bodega destino"
        Me.chkGenerarRecepcionAutoBodegaDestino.Size = New System.Drawing.Size(331, 24)
        Me.chkGenerarRecepcionAutoBodegaDestino.TabIndex = 19
        '
        'chkRecibirProductoAutoBodegaDestino
        '
        Me.chkRecibirProductoAutoBodegaDestino.Location = New System.Drawing.Point(542, 35)
        Me.chkRecibirProductoAutoBodegaDestino.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkRecibirProductoAutoBodegaDestino.MenuManager = Me.RibbonControl
        Me.chkRecibirProductoAutoBodegaDestino.Name = "chkRecibirProductoAutoBodegaDestino"
        Me.chkRecibirProductoAutoBodegaDestino.Properties.OffText = "No recibir producto auto bodega destino"
        Me.chkRecibirProductoAutoBodegaDestino.Properties.OnText = "Recibir producto auto bodega destino"
        Me.chkRecibirProductoAutoBodegaDestino.Size = New System.Drawing.Size(331, 24)
        Me.chkRecibirProductoAutoBodegaDestino.TabIndex = 18
        '
        'chkTrasladarLotesDocIngreso
        '
        Me.chkTrasladarLotesDocIngreso.Location = New System.Drawing.Point(88, 95)
        Me.chkTrasladarLotesDocIngreso.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkTrasladarLotesDocIngreso.MenuManager = Me.RibbonControl
        Me.chkTrasladarLotesDocIngreso.Name = "chkTrasladarLotesDocIngreso"
        Me.chkTrasladarLotesDocIngreso.Properties.OffText = "No trasladar lotes en Doc. de ingreso Bod. Destino"
        Me.chkTrasladarLotesDocIngreso.Properties.OnText = "Trasladar lotes en Doc. de ingreso Bod. Destino"
        Me.chkTrasladarLotesDocIngreso.Size = New System.Drawing.Size(410, 24)
        Me.chkTrasladarLotesDocIngreso.TabIndex = 17
        '
        'chkGenerarPedidoIngresoBodegaDestino
        '
        Me.chkGenerarPedidoIngresoBodegaDestino.Location = New System.Drawing.Point(88, 35)
        Me.chkGenerarPedidoIngresoBodegaDestino.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkGenerarPedidoIngresoBodegaDestino.MenuManager = Me.RibbonControl
        Me.chkGenerarPedidoIngresoBodegaDestino.Name = "chkGenerarPedidoIngresoBodegaDestino"
        Me.chkGenerarPedidoIngresoBodegaDestino.Properties.OffText = "No genera Doc. Ingreso en bodega destino"
        Me.chkGenerarPedidoIngresoBodegaDestino.Properties.OnText = "Genera Doc. Ingreso en bodega destino"
        Me.chkGenerarPedidoIngresoBodegaDestino.Size = New System.Drawing.Size(314, 24)
        Me.chkGenerarPedidoIngresoBodegaDestino.TabIndex = 16
        '
        'cmbTipoDocumentoIngreso
        '
        Me.cmbTipoDocumentoIngreso.Location = New System.Drawing.Point(88, 66)
        Me.cmbTipoDocumentoIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoDocumentoIngreso.MenuManager = Me.RibbonControl
        Me.cmbTipoDocumentoIngreso.Name = "cmbTipoDocumentoIngreso"
        Me.cmbTipoDocumentoIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoDocumentoIngreso.Properties.NullText = ""
        Me.cmbTipoDocumentoIngreso.Size = New System.Drawing.Size(284, 22)
        Me.cmbTipoDocumentoIngreso.TabIndex = 15
        '
        'grpGenerales
        '
        Me.grpGenerales.Controls.Add(Me.chkAsignarTodosOperadores)
        Me.grpGenerales.Controls.Add(Me.chkGeneraGuiaRemision)
        Me.grpGenerales.Controls.Add(Me.chkEmpaqueTarima)
        Me.grpGenerales.Controls.Add(Me.GroupBox3)
        Me.grpGenerales.Controls.Add(Me.GroupBox2)
        Me.grpGenerales.Controls.Add(Me.GroupBox1)
        Me.grpGenerales.Controls.Add(Me.chkEsDevolucion)
        Me.grpGenerales.Controls.Add(Me.chkPermitirDespachoMultiple)
        Me.grpGenerales.Controls.Add(Me.chkpermitir_despacho_parcial)
        Me.grpGenerales.Controls.Add(Me.chkcontrol_cliente_en_detalle)
        Me.grpGenerales.Controls.Add(Me.chkMarcar_Registros_Enviados_MI3)
        Me.grpGenerales.Controls.Add(Me.chkRequerirClienteEsBodegaWMS)
        Me.grpGenerales.Controls.Add(Me.txtObservacion)
        Me.grpGenerales.Controls.Add(Me.chkRequiereDocumentoRef)
        Me.grpGenerales.Controls.Add(Me.chkControlPoliza)
        Me.grpGenerales.Controls.Add(Me.chkPreparar)
        Me.grpGenerales.Controls.Add(Me.lblCodigo)
        Me.grpGenerales.Controls.Add(Label12)
        Me.grpGenerales.Controls.Add(NombreLabel)
        Me.grpGenerales.Controls.Add(Me.txtNombre)
        Me.grpGenerales.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpGenerales.Location = New System.Drawing.Point(2, 28)
        Me.grpGenerales.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.grpGenerales.Name = "grpGenerales"
        Me.grpGenerales.Size = New System.Drawing.Size(901, 472)
        Me.grpGenerales.TabIndex = 21
        '
        'chkAsignarTodosOperadores
        '
        Me.chkAsignarTodosOperadores.Location = New System.Drawing.Point(85, 305)
        Me.chkAsignarTodosOperadores.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkAsignarTodosOperadores.MenuManager = Me.RibbonControl
        Me.chkAsignarTodosOperadores.Name = "chkAsignarTodosOperadores"
        Me.chkAsignarTodosOperadores.Properties.OffText = "No asignar todos los operadores"
        Me.chkAsignarTodosOperadores.Properties.OnText = "Asignar todos los operadores"
        Me.chkAsignarTodosOperadores.Size = New System.Drawing.Size(244, 24)
        Me.chkAsignarTodosOperadores.TabIndex = 34
        '
        'chkGeneraGuiaRemision
        '
        Me.chkGeneraGuiaRemision.Location = New System.Drawing.Point(85, 273)
        Me.chkGeneraGuiaRemision.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkGeneraGuiaRemision.MenuManager = Me.RibbonControl
        Me.chkGeneraGuiaRemision.Name = "chkGeneraGuiaRemision"
        Me.chkGeneraGuiaRemision.Properties.OffText = "No genera guía remisión"
        Me.chkGeneraGuiaRemision.Properties.OnText = "Genera guía remisión"
        Me.chkGeneraGuiaRemision.Size = New System.Drawing.Size(222, 24)
        Me.chkGeneraGuiaRemision.TabIndex = 33
        '
        'chkEmpaqueTarima
        '
        Me.chkEmpaqueTarima.Location = New System.Drawing.Point(392, 304)
        Me.chkEmpaqueTarima.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkEmpaqueTarima.MenuManager = Me.RibbonControl
        Me.chkEmpaqueTarima.Name = "chkEmpaqueTarima"
        Me.chkEmpaqueTarima.Properties.OffText = "No requiere empaque tarima"
        Me.chkEmpaqueTarima.Properties.OnText = "Requiere empaque tarima"
        Me.chkEmpaqueTarima.Size = New System.Drawing.Size(284, 24)
        Me.chkEmpaqueTarima.TabIndex = 32
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblPropietario)
        Me.GroupBox3.Controls.Add(Me.cmbPropietario)
        Me.GroupBox3.Controls.Add(Me.lblEstado)
        Me.GroupBox3.Controls.Add(Me.cmbEstado)
        Me.GroupBox3.Location = New System.Drawing.Point(563, 339)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(327, 121)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Estado default producto"
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(4, 35)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(74, 16)
        Me.lblPropietario.TabIndex = 33
        Me.lblPropietario.Text = "Propietario:"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPropietario.Location = New System.Drawing.Point(96, 23)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbPropietario.Properties.Appearance.Options.UseFont = True
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(209, 28)
        Me.cmbPropietario.TabIndex = 31
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(4, 72)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(50, 16)
        Me.lblEstado.TabIndex = 34
        Me.lblEstado.Text = "Estado:"
        '
        'cmbEstado
        '
        Me.cmbEstado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEstado.Location = New System.Drawing.Point(96, 60)
        Me.cmbEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEstado.MenuManager = Me.RibbonControl
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbEstado.Properties.Appearance.Options.UseFont = True
        Me.cmbEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstado.Properties.NullText = ""
        Me.cmbEstado.Size = New System.Drawing.Size(209, 28)
        Me.cmbEstado.TabIndex = 32
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkVerificacionImagenBOF)
        Me.GroupBox2.Controls.Add(Me.chkFotografiaVerificacion)
        Me.GroupBox2.Controls.Add(Me.chkVerificar)
        Me.GroupBox2.Location = New System.Drawing.Point(308, 339)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(249, 121)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Verificacion"
        '
        'chkVerificacionImagenBOF
        '
        Me.chkVerificacionImagenBOF.Location = New System.Drawing.Point(17, 92)
        Me.chkVerificacionImagenBOF.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkVerificacionImagenBOF.MenuManager = Me.RibbonControl
        Me.chkVerificacionImagenBOF.Name = "chkVerificacionImagenBOF"
        Me.chkVerificacionImagenBOF.Properties.OffText = "No verificar imagen en BOF"
        Me.chkVerificacionImagenBOF.Properties.OnText = "Verificar con imagen en BOF"
        Me.chkVerificacionImagenBOF.Size = New System.Drawing.Size(223, 24)
        Me.chkVerificacionImagenBOF.TabIndex = 13
        '
        'chkFotografiaVerificacion
        '
        Me.chkFotografiaVerificacion.Location = New System.Drawing.Point(17, 33)
        Me.chkFotografiaVerificacion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkFotografiaVerificacion.MenuManager = Me.RibbonControl
        Me.chkFotografiaVerificacion.Name = "chkFotografiaVerificacion"
        Me.chkFotografiaVerificacion.Properties.OffText = "Fotografía en verificación"
        Me.chkFotografiaVerificacion.Properties.OnText = "Fotografía en verificación"
        Me.chkFotografiaVerificacion.Size = New System.Drawing.Size(223, 24)
        Me.chkFotografiaVerificacion.TabIndex = 12
        '
        'chkVerificar
        '
        Me.chkVerificar.Location = New System.Drawing.Point(17, 61)
        Me.chkVerificar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkVerificar.MenuManager = Me.RibbonControl
        Me.chkVerificar.Name = "chkVerificar"
        Me.chkVerificar.Properties.OffText = "Requiere Verificación"
        Me.chkVerificar.Properties.OnText = "Sin Verificación"
        Me.chkVerificar.Size = New System.Drawing.Size(223, 24)
        Me.chkVerificar.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkGenerarPickingAuto)
        Me.GroupBox1.Controls.Add(Me.chkMoverAMuelle)
        Me.GroupBox1.Controls.Add(Me.chkEscanearMuellePicking)
        Me.GroupBox1.Location = New System.Drawing.Point(55, 339)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(243, 121)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Picking"
        '
        'chkMoverAMuelle
        '
        Me.chkMoverAMuelle.Location = New System.Drawing.Point(7, 32)
        Me.chkMoverAMuelle.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkMoverAMuelle.MenuManager = Me.RibbonControl
        Me.chkMoverAMuelle.Name = "chkMoverAMuelle"
        Me.chkMoverAMuelle.Properties.OffText = "Mover producto a muelle"
        Me.chkMoverAMuelle.Properties.OnText = "Mover producto a muelle"
        Me.chkMoverAMuelle.Size = New System.Drawing.Size(241, 24)
        Me.chkMoverAMuelle.TabIndex = 27
        '
        'chkEscanearMuellePicking
        '
        Me.chkEscanearMuellePicking.Location = New System.Drawing.Point(7, 60)
        Me.chkEscanearMuellePicking.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkEscanearMuellePicking.MenuManager = Me.RibbonControl
        Me.chkEscanearMuellePicking.Name = "chkEscanearMuellePicking"
        Me.chkEscanearMuellePicking.Properties.OffText = "Escanear muelle en picking"
        Me.chkEscanearMuellePicking.Properties.OnText = "Escanear muelle en picking"
        Me.chkEscanearMuellePicking.Size = New System.Drawing.Size(241, 24)
        Me.chkEscanearMuellePicking.TabIndex = 28
        '
        'chkEsDevolucion
        '
        Me.chkEsDevolucion.Location = New System.Drawing.Point(392, 273)
        Me.chkEsDevolucion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkEsDevolucion.MenuManager = Me.RibbonControl
        Me.chkEsDevolucion.Name = "chkEsDevolucion"
        Me.chkEsDevolucion.Properties.OffText = "Es devolución"
        Me.chkEsDevolucion.Properties.OnText = "Es devolución"
        Me.chkEsDevolucion.Size = New System.Drawing.Size(241, 24)
        Me.chkEsDevolucion.TabIndex = 26
        '
        'chkPermitirDespachoMultiple
        '
        Me.chkPermitirDespachoMultiple.EditValue = True
        Me.chkPermitirDespachoMultiple.Location = New System.Drawing.Point(392, 240)
        Me.chkPermitirDespachoMultiple.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkPermitirDespachoMultiple.MenuManager = Me.RibbonControl
        Me.chkPermitirDespachoMultiple.Name = "chkPermitirDespachoMultiple"
        Me.chkPermitirDespachoMultiple.Properties.OffText = "Permitir despacho múltiple"
        Me.chkPermitirDespachoMultiple.Properties.OnText = "Permitir despacho múltiple"
        Me.chkPermitirDespachoMultiple.Size = New System.Drawing.Size(364, 24)
        Me.chkPermitirDespachoMultiple.TabIndex = 25
        Me.chkPermitirDespachoMultiple.Tag = "MERCOSAL_MARECELO_20220627"
        Me.chkPermitirDespachoMultiple.ToolTip = "#EJC20220525: Si se habilita, en el proceso de despacho permite despachar N veces" &
    " el mismo pedido, si se desactiva solo se permite un despacho para un pedido."
        '
        'chkpermitir_despacho_parcial
        '
        Me.chkpermitir_despacho_parcial.EditValue = True
        Me.chkpermitir_despacho_parcial.Location = New System.Drawing.Point(392, 213)
        Me.chkpermitir_despacho_parcial.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkpermitir_despacho_parcial.MenuManager = Me.RibbonControl
        Me.chkpermitir_despacho_parcial.Name = "chkpermitir_despacho_parcial"
        Me.chkpermitir_despacho_parcial.Properties.OffText = "Permitir despacho parcial"
        Me.chkpermitir_despacho_parcial.Properties.OnText = "Permitir despacho parcial"
        Me.chkpermitir_despacho_parcial.Size = New System.Drawing.Size(364, 24)
        Me.chkpermitir_despacho_parcial.TabIndex = 24
        Me.chkpermitir_despacho_parcial.Tag = "MERCOSAL_MARECELO_20220525"
        Me.chkpermitir_despacho_parcial.ToolTip = "#EJC20220525: Si se habilita, en el proceso de despacho valida que el pedido esté" &
    " verificado en su totalidad en correlación con la cantidad solicitada para permi" &
    "tir el despacho"
        '
        'chkcontrol_cliente_en_detalle
        '
        Me.chkcontrol_cliente_en_detalle.Location = New System.Drawing.Point(392, 185)
        Me.chkcontrol_cliente_en_detalle.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkcontrol_cliente_en_detalle.MenuManager = Me.RibbonControl
        Me.chkcontrol_cliente_en_detalle.Name = "chkcontrol_cliente_en_detalle"
        Me.chkcontrol_cliente_en_detalle.Properties.OffText = "Control cliente en detalle (SSI MI3)"
        Me.chkcontrol_cliente_en_detalle.Properties.OnText = "control_cliente_en_detalle (SSI MI3)"
        Me.chkcontrol_cliente_en_detalle.Size = New System.Drawing.Size(364, 24)
        Me.chkcontrol_cliente_en_detalle.TabIndex = 23
        Me.chkcontrol_cliente_en_detalle.ToolTip = "#EJC20220512: Habilita cliente a nivel de detalle en documento de salida."
        '
        'chkMarcar_Registros_Enviados_MI3
        '
        Me.chkMarcar_Registros_Enviados_MI3.Location = New System.Drawing.Point(392, 158)
        Me.chkMarcar_Registros_Enviados_MI3.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkMarcar_Registros_Enviados_MI3.MenuManager = Me.RibbonControl
        Me.chkMarcar_Registros_Enviados_MI3.Name = "chkMarcar_Registros_Enviados_MI3"
        Me.chkMarcar_Registros_Enviados_MI3.Properties.OffText = "Marcar registros MI3 como enviados al despachar."
        Me.chkMarcar_Registros_Enviados_MI3.Properties.OnText = "Marcar registros MI3 como enviados al despachar."
        Me.chkMarcar_Registros_Enviados_MI3.Size = New System.Drawing.Size(364, 24)
        Me.chkMarcar_Registros_Enviados_MI3.TabIndex = 22
        '
        'chkRequerirClienteEsBodegaWMS
        '
        Me.chkRequerirClienteEsBodegaWMS.Location = New System.Drawing.Point(392, 128)
        Me.chkRequerirClienteEsBodegaWMS.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkRequerirClienteEsBodegaWMS.MenuManager = Me.RibbonControl
        Me.chkRequerirClienteEsBodegaWMS.Name = "chkRequerirClienteEsBodegaWMS"
        Me.chkRequerirClienteEsBodegaWMS.Properties.OffText = "Requiere cliente configurado como bodega de WMS"
        Me.chkRequerirClienteEsBodegaWMS.Properties.OnText = "Requiere cliente configurado como bodega de WMS"
        Me.chkRequerirClienteEsBodegaWMS.Size = New System.Drawing.Size(364, 24)
        Me.chkRequerirClienteEsBodegaWMS.TabIndex = 21
        '
        'txtObservacion
        '
        Me.txtObservacion.EditValue = ""
        Me.txtObservacion.Location = New System.Drawing.Point(85, 118)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.txtObservacion.MenuManager = Me.RibbonControl
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(284, 146)
        Me.txtObservacion.TabIndex = 20
        '
        'chkRequiereDocumentoRef
        '
        Me.chkRequiereDocumentoRef.Location = New System.Drawing.Point(392, 97)
        Me.chkRequiereDocumentoRef.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkRequiereDocumentoRef.MenuManager = Me.RibbonControl
        Me.chkRequiereDocumentoRef.Name = "chkRequiereDocumentoRef"
        Me.chkRequiereDocumentoRef.Properties.OffText = "No requiere documento referencia"
        Me.chkRequiereDocumentoRef.Properties.OnText = "Requiere documento referencia"
        Me.chkRequiereDocumentoRef.Size = New System.Drawing.Size(284, 24)
        Me.chkRequiereDocumentoRef.TabIndex = 12
        '
        'chkControlPoliza
        '
        Me.chkControlPoliza.Location = New System.Drawing.Point(392, 68)
        Me.chkControlPoliza.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkControlPoliza.MenuManager = Me.RibbonControl
        Me.chkControlPoliza.Name = "chkControlPoliza"
        Me.chkControlPoliza.Properties.OffText = "No requiere poliza"
        Me.chkControlPoliza.Properties.OnText = "Requiere poliza"
        Me.chkControlPoliza.Size = New System.Drawing.Size(284, 24)
        Me.chkControlPoliza.TabIndex = 10
        '
        'chkPreparar
        '
        Me.chkPreparar.Location = New System.Drawing.Point(392, 41)
        Me.chkPreparar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkPreparar.MenuManager = Me.RibbonControl
        Me.chkPreparar.Name = "chkPreparar"
        Me.chkPreparar.Properties.OffText = "Requiere Packing"
        Me.chkPreparar.Properties.OnText = "Requiere Packing"
        Me.chkPreparar.Size = New System.Drawing.Size(284, 24)
        Me.chkPreparar.TabIndex = 6
        '
        'lblCodigo
        '
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(85, 54)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(284, 23)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(85, 82)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(284, 22)
        Me.txtNombre.TabIndex = 5
        '
        'GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario
        '
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.chkImprimeBarrasPacking)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.chkImprimirBarrasPicking)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.chkReservaStock)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Location = New System.Drawing.Point(903, 28)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Name = "GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario"
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Size = New System.Drawing.Size(334, 605)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.TabIndex = 13
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Text = "Parámetros de Software (No personalizados por usuario)"
        '
        'chkImprimeBarrasPacking
        '
        Me.chkImprimeBarrasPacking.Enabled = False
        Me.chkImprimeBarrasPacking.Location = New System.Drawing.Point(77, 119)
        Me.chkImprimeBarrasPacking.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkImprimeBarrasPacking.MenuManager = Me.RibbonControl
        Me.chkImprimeBarrasPacking.Name = "chkImprimeBarrasPacking"
        Me.chkImprimeBarrasPacking.Properties.OffText = "No imprime licencia en packing"
        Me.chkImprimeBarrasPacking.Properties.OnText = "Imprimir licencia en packing"
        Me.chkImprimeBarrasPacking.Size = New System.Drawing.Size(241, 24)
        Me.chkImprimeBarrasPacking.TabIndex = 11
        '
        'chkImprimirBarrasPicking
        '
        Me.chkImprimirBarrasPicking.Location = New System.Drawing.Point(77, 89)
        Me.chkImprimirBarrasPicking.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkImprimirBarrasPicking.MenuManager = Me.RibbonControl
        Me.chkImprimirBarrasPicking.Name = "chkImprimirBarrasPicking"
        Me.chkImprimirBarrasPicking.Properties.OffText = "No imprime licencia en picking"
        Me.chkImprimirBarrasPicking.Properties.OnText = "Imprimir licencia en picking"
        Me.chkImprimirBarrasPicking.Size = New System.Drawing.Size(241, 24)
        Me.chkImprimirBarrasPicking.TabIndex = 9
        '
        'chkReservaStock
        '
        Me.chkReservaStock.EditValue = True
        Me.chkReservaStock.Enabled = False
        Me.chkReservaStock.Location = New System.Drawing.Point(77, 53)
        Me.chkReservaStock.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkReservaStock.MenuManager = Me.RibbonControl
        Me.chkReservaStock.Name = "chkReservaStock"
        Me.chkReservaStock.Properties.OffText = "Reserva stock"
        Me.chkReservaStock.Properties.OnText = "No reserva stock"
        Me.chkReservaStock.Size = New System.Drawing.Size(188, 24)
        Me.chkReservaStock.TabIndex = 8
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(142, 48)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(142, 16)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(626, 48)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(626, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkProducto_Tipo
        '
        Me.dkProducto_Tipo.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkProducto_Tipo.Form = Me
        Me.dkProducto_Tipo.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 828)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1239, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("b43ffebe-a0fb-42a7-b56f-f32caab34d5e")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 101)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 124)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(946, 90)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'chkGenerarPickingAuto
        '
        Me.chkGenerarPickingAuto.Location = New System.Drawing.Point(7, 88)
        Me.chkGenerarPickingAuto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkGenerarPickingAuto.MenuManager = Me.RibbonControl
        Me.chkGenerarPickingAuto.Name = "chkGenerarPickingAuto"
        Me.chkGenerarPickingAuto.Properties.OffText = "No generar picking auto"
        Me.chkGenerarPickingAuto.Properties.OnText = "Generar picking auto"
        Me.chkGenerarPickingAuto.Size = New System.Drawing.Size(241, 24)
        Me.chkGenerarPickingAuto.TabIndex = 29
        '
        'frmTipoDocumentoSalida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1239, 884)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmTipoDocumentoSalida"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Tipo documento salida"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControlComportamientoEnTransferenciasGeneralmente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.ResumeLayout(False)
        Me.GroupControlComportamientoEnTransferenciasGeneralmente.PerformLayout()
        CType(Me.chkTransferirUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenerarRecepcionAutoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRecibirProductoAutoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTrasladarLotesDocIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenerarPedidoIngresoBodegaDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoDocumentoIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGenerales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGenerales.ResumeLayout(False)
        Me.grpGenerales.PerformLayout()
        CType(Me.chkAsignarTodosOperadores.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraGuiaRemision.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEmpaqueTarima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.chkVerificacionImagenBOF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFotografiaVerificacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVerificar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.chkMoverAMuelle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEscanearMuellePicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsDevolucion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermitirDespachoMultiple.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkpermitir_despacho_parcial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcontrol_cliente_en_detalle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMarcar_Registros_Enviados_MI3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirClienteEsBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObservacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequiereDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPreparar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.ResumeLayout(False)
        CType(Me.chkImprimeBarrasPacking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkImprimirBarrasPicking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReservaStock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkProducto_Tipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.chkGenerarPickingAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents dkProducto_Tipo As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkRequiereDocumentoRef As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkImprimeBarrasPacking As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkControlPoliza As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkImprimirBarrasPicking As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkReservaStock As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkVerificar As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPreparar As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmbTipoDocumentoIngreso As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkGenerarPedidoIngresoBodegaDestino As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkTrasladarLotesDocIngreso As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents GroupControlComportamientoEnTransferenciasGeneralmente As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpGenerales As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtObservacion As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkRequerirClienteEsBodegaWMS As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkMarcar_Registros_Enviados_MI3 As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkcontrol_cliente_en_detalle As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkpermitir_despacho_parcial As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPermitirDespachoMultiple As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkFotografiaVerificacion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkEsDevolucion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents lblPropietario As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkEscanearMuellePicking As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkMoverAMuelle As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkGenerarRecepcionAutoBodegaDestino As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkRecibirProductoAutoBodegaDestino As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkEmpaqueTarima As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkTransferirUbicacion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkVerificacionImagenBOF As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkAsignarTodosOperadores As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkGeneraGuiaRemision As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkGenerarPickingAuto As DevExpress.XtraEditors.ToggleSwitch
End Class
