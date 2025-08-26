<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpresa
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If Empresa IsNot Nothing Then
                Empresa.Dispose()
                Empresa = Nothing
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
        Dim ActivoLabel As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Anulaciones_por_supervisorLabel As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Control_presentacionesLabel As System.Windows.Forms.Label
        Dim DireccionLabel As System.Windows.Forms.Label
        Dim Operador_logisticoLabel As System.Windows.Forms.Label
        Dim TelefonoLabel As System.Windows.Forms.Label
        Dim ClienteRapidoLabel As System.Windows.Forms.Label
        Dim EmailLabel As System.Windows.Forms.Label
        Dim Razon_socialLabel As System.Windows.Forms.Label
        Dim RepresentanteLabel As System.Windows.Forms.Label
        Dim Corr_cod_barraLabel As System.Windows.Forms.Label
        Dim Path_printerLabel As System.Windows.Forms.Label
        Dim ImagenLabel As System.Windows.Forms.Label
        Dim Puerto_escanerLabel As System.Windows.Forms.Label
        Dim CodigoLabel As System.Windows.Forms.Label
        Dim ClaveLabel As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblCodigosAuto As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpresa))
        Dim lblVersionBD As System.Windows.Forms.Label
        Dim lblAWSToken As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkBuscarActualizacionHH = New DevExpress.XtraEditors.CheckEdit()
        Me.cbxMotivosAjuste = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkCodigoAutomatico = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.chkMostrarContraseña = New System.Windows.Forms.CheckBox()
        Me.ClaveTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.CodigoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.IdEmpresaSpinEdit = New DevExpress.XtraEditors.TextEdit()
        Me.btnExaminar = New DevExpress.XtraEditors.SimpleButton()
        Me.picFoto = New System.Windows.Forms.PictureBox()
        Me.chkAnulacionesporsupervisor = New DevExpress.XtraEditors.CheckEdit()
        Me.chkControlpresentaciones = New DevExpress.XtraEditors.CheckEdit()
        Me.NombreTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.chkOperadorlogistico = New DevExpress.XtraEditors.CheckEdit()
        Me.DireccionTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.chkClienteRapido = New DevExpress.XtraEditors.CheckEdit()
        Me.TelefonoTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.EmailTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Razon_socialTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.RepresentanteTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Corr_cod_barraSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.Path_printerTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Puerto_escanerSpinEdit = New DevExpress.XtraEditors.SpinEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.chkExigirPoliticaDeContraseña = New DevExpress.XtraEditors.CheckEdit()
        Me.SpinEditIntentos = New DevExpress.XtraEditors.SpinEdit()
        Me.SpinEditDuracionTemporal = New DevExpress.XtraEditors.SpinEdit()
        Me.SpinEditDuracion = New DevExpress.XtraEditors.SpinEdit()
        Me.cbxBloqueo = New DevExpress.XtraEditors.CheckEdit()
        Me.cbxCaducidad = New DevExpress.XtraEditors.CheckEdit()
        Me.TabControlEmpresa = New DevExpress.XtraTab.XtraTabControl()
        Me.General = New DevExpress.XtraTab.XtraTabPage()
        Me.Seguridad = New DevExpress.XtraTab.XtraTabPage()
        Me.dkEmpresa = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtVersionBD = New DevExpress.XtraEditors.TextEdit()
        Me.txtAWSToken = New DevExpress.XtraEditors.TextEdit()
        ActivoLabel = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Anulaciones_por_supervisorLabel = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Control_presentacionesLabel = New System.Windows.Forms.Label()
        DireccionLabel = New System.Windows.Forms.Label()
        Operador_logisticoLabel = New System.Windows.Forms.Label()
        TelefonoLabel = New System.Windows.Forms.Label()
        ClienteRapidoLabel = New System.Windows.Forms.Label()
        EmailLabel = New System.Windows.Forms.Label()
        Razon_socialLabel = New System.Windows.Forms.Label()
        RepresentanteLabel = New System.Windows.Forms.Label()
        Corr_cod_barraLabel = New System.Windows.Forms.Label()
        Path_printerLabel = New System.Windows.Forms.Label()
        ImagenLabel = New System.Windows.Forms.Label()
        Puerto_escanerLabel = New System.Windows.Forms.Label()
        CodigoLabel = New System.Windows.Forms.Label()
        ClaveLabel = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblCodigosAuto = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        lblVersionBD = New System.Windows.Forms.Label()
        lblAWSToken = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkBuscarActualizacionHH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxMotivosAjuste.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCodigoAutomatico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.ClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IdEmpresaSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAnulacionesporsupervisor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlpresentaciones.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOperadorlogistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkClienteRapido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Razon_socialTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepresentanteTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Corr_cod_barraSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Path_printerTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Puerto_escanerSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.chkExigirPoliticaDeContraseña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinEditIntentos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinEditDuracionTemporal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpinEditDuracion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxBloqueo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxCaducidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabControlEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlEmpresa.SuspendLayout()
        Me.General.SuspendLayout()
        Me.Seguridad.SuspendLayout()
        CType(Me.dkEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.txtVersionBD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAWSToken.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ActivoLabel
        '
        ActivoLabel.AutoSize = True
        ActivoLabel.Location = New System.Drawing.Point(435, 319)
        ActivoLabel.Name = "ActivoLabel"
        ActivoLabel.Size = New System.Drawing.Size(46, 16)
        ActivoLabel.TabIndex = 33
        ActivoLabel.Text = "Activo:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.Location = New System.Drawing.Point(27, 34)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(51, 16)
        IdEmpresaLabel.TabIndex = 0
        IdEmpresaLabel.Text = "Código:"
        '
        'Anulaciones_por_supervisorLabel
        '
        Anulaciones_por_supervisorLabel.AutoSize = True
        Anulaciones_por_supervisorLabel.Location = New System.Drawing.Point(435, 226)
        Anulaciones_por_supervisorLabel.Name = "Anulaciones_por_supervisorLabel"
        Anulaciones_por_supervisorLabel.Size = New System.Drawing.Size(166, 16)
        Anulaciones_por_supervisorLabel.TabIndex = 26
        Anulaciones_por_supervisorLabel.Text = "Anulaciones por supervisor:"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(26, 65)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 4
        NombreLabel.Text = "Nombre:"
        '
        'Control_presentacionesLabel
        '
        Control_presentacionesLabel.AutoSize = True
        Control_presentacionesLabel.Location = New System.Drawing.Point(435, 196)
        Control_presentacionesLabel.Name = "Control_presentacionesLabel"
        Control_presentacionesLabel.Size = New System.Drawing.Size(143, 16)
        Control_presentacionesLabel.TabIndex = 22
        Control_presentacionesLabel.Text = "Control Presentaciones:"
        '
        'DireccionLabel
        '
        DireccionLabel.AutoSize = True
        DireccionLabel.Location = New System.Drawing.Point(28, 96)
        DireccionLabel.Name = "DireccionLabel"
        DireccionLabel.Size = New System.Drawing.Size(64, 16)
        DireccionLabel.TabIndex = 8
        DireccionLabel.Text = "Dirección:"
        '
        'Operador_logisticoLabel
        '
        Operador_logisticoLabel.AutoSize = True
        Operador_logisticoLabel.Location = New System.Drawing.Point(435, 257)
        Operador_logisticoLabel.Name = "Operador_logisticoLabel"
        Operador_logisticoLabel.Size = New System.Drawing.Size(119, 16)
        Operador_logisticoLabel.TabIndex = 29
        Operador_logisticoLabel.Text = "Operador Logistico:"
        '
        'TelefonoLabel
        '
        TelefonoLabel.AutoSize = True
        TelefonoLabel.Location = New System.Drawing.Point(435, 34)
        TelefonoLabel.Name = "TelefonoLabel"
        TelefonoLabel.Size = New System.Drawing.Size(62, 16)
        TelefonoLabel.TabIndex = 2
        TelefonoLabel.Text = "Teléfono:"
        '
        'ClienteRapidoLabel
        '
        ClienteRapidoLabel.AutoSize = True
        ClienteRapidoLabel.Location = New System.Drawing.Point(435, 164)
        ClienteRapidoLabel.Name = "ClienteRapidoLabel"
        ClienteRapidoLabel.Size = New System.Drawing.Size(94, 16)
        ClienteRapidoLabel.TabIndex = 18
        ClienteRapidoLabel.Text = "Cliente Rapido:"
        '
        'EmailLabel
        '
        EmailLabel.AutoSize = True
        EmailLabel.Location = New System.Drawing.Point(28, 127)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New System.Drawing.Size(43, 16)
        EmailLabel.TabIndex = 12
        EmailLabel.Text = "Email:"
        '
        'Razon_socialLabel
        '
        Razon_socialLabel.AutoSize = True
        Razon_socialLabel.Location = New System.Drawing.Point(435, 65)
        Razon_socialLabel.Name = "Razon_socialLabel"
        Razon_socialLabel.Size = New System.Drawing.Size(85, 16)
        Razon_socialLabel.TabIndex = 6
        Razon_socialLabel.Text = "Razon Social:"
        '
        'RepresentanteLabel
        '
        RepresentanteLabel.AutoSize = True
        RepresentanteLabel.Location = New System.Drawing.Point(28, 187)
        RepresentanteLabel.Name = "RepresentanteLabel"
        RepresentanteLabel.Size = New System.Drawing.Size(95, 16)
        RepresentanteLabel.TabIndex = 20
        RepresentanteLabel.Text = "Representante:"
        '
        'Corr_cod_barraLabel
        '
        Corr_cod_barraLabel.AutoSize = True
        Corr_cod_barraLabel.Location = New System.Drawing.Point(435, 127)
        Corr_cod_barraLabel.Name = "Corr_cod_barraLabel"
        Corr_cod_barraLabel.Size = New System.Drawing.Size(86, 16)
        Corr_cod_barraLabel.TabIndex = 14
        Corr_cod_barraLabel.Text = "Código Barra:"
        Corr_cod_barraLabel.Visible = False
        '
        'Path_printerLabel
        '
        Path_printerLabel.AutoSize = True
        Path_printerLabel.Location = New System.Drawing.Point(28, 156)
        Path_printerLabel.Name = "Path_printerLabel"
        Path_printerLabel.Size = New System.Drawing.Size(79, 16)
        Path_printerLabel.TabIndex = 16
        Path_printerLabel.Text = "Path Printer:"
        '
        'ImagenLabel
        '
        ImagenLabel.AutoSize = True
        ImagenLabel.Location = New System.Drawing.Point(27, 246)
        ImagenLabel.Name = "ImagenLabel"
        ImagenLabel.Size = New System.Drawing.Size(55, 16)
        ImagenLabel.TabIndex = 28
        ImagenLabel.Text = "Imagen:"
        '
        'Puerto_escanerLabel
        '
        Puerto_escanerLabel.AutoSize = True
        Puerto_escanerLabel.Location = New System.Drawing.Point(27, 218)
        Puerto_escanerLabel.Name = "Puerto_escanerLabel"
        Puerto_escanerLabel.Size = New System.Drawing.Size(98, 16)
        Puerto_escanerLabel.TabIndex = 24
        Puerto_escanerLabel.Text = "Puerto Escáner:"
        '
        'CodigoLabel
        '
        CodigoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CodigoLabel.AutoSize = True
        CodigoLabel.Location = New System.Drawing.Point(24, 37)
        CodigoLabel.Name = "CodigoLabel"
        CodigoLabel.Size = New System.Drawing.Size(51, 16)
        CodigoLabel.TabIndex = 0
        CodigoLabel.Text = "Código:"
        '
        'ClaveLabel
        '
        ClaveLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ClaveLabel.AutoSize = True
        ClaveLabel.Location = New System.Drawing.Point(24, 69)
        ClaveLabel.Name = "ClaveLabel"
        ClaveLabel.Size = New System.Drawing.Size(43, 16)
        ClaveLabel.TabIndex = 2
        ClaveLabel.Text = "Clave:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(24, 150)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(119, 16)
        Label5.TabIndex = 5
        Label5.Text = "Ingresa despues de"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(22, 114)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(142, 16)
        Label4.TabIndex = 2
        Label4.Text = "Contraseña expira cada"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(227, 150)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(186, 16)
        Label3.TabIndex = 7
        Label3.Text = "dias de expirada la contraseña."
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(227, 114)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(34, 16)
        Label2.TabIndex = 4
        Label2.Text = "dias."
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(285, 190)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(52, 16)
        Label1.TabIndex = 10
        Label1.Text = "intentos"
        '
        'lblCodigosAuto
        '
        lblCodigosAuto.AutoSize = True
        lblCodigosAuto.Location = New System.Drawing.Point(435, 288)
        lblCodigosAuto.Name = "lblCodigosAuto"
        lblCodigosAuto.Size = New System.Drawing.Size(130, 16)
        lblCodigosAuto.TabIndex = 31
        lblCodigosAuto.Text = "Códigos automáticos:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(68, 46)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(68, 14)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(552, 46)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(552, 14)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(435, 96)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(113, 16)
        Label6.TabIndex = 11
        Label6.Text = "Motivos de Ajuste:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(435, 350)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(147, 16)
        Label7.TabIndex = 82
        Label7.Text = "Buscar actualizacion HH:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1071, 193)
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
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Mantenimiento de Empresa"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(170, 42)
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
        Me.User_agrTextEdit.Location = New System.Drawing.Point(170, 10)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(654, 42)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(654, 10)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupControl1
        '
        Me.GroupControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupControl1.Controls.Add(Me.chkBuscarActualizacionHH)
        Me.GroupControl1.Controls.Add(Label7)
        Me.GroupControl1.Controls.Add(Me.cbxMotivosAjuste)
        Me.GroupControl1.Controls.Add(Label6)
        Me.GroupControl1.Controls.Add(Me.chkCodigoAutomatico)
        Me.GroupControl1.Controls.Add(lblCodigosAuto)
        Me.GroupControl1.Controls.Add(Me.GroupControl5)
        Me.GroupControl1.Controls.Add(Me.IdEmpresaSpinEdit)
        Me.GroupControl1.Controls.Add(Me.btnExaminar)
        Me.GroupControl1.Controls.Add(Me.picFoto)
        Me.GroupControl1.Controls.Add(ActivoLabel)
        Me.GroupControl1.Controls.Add(Me.chkAnulacionesporsupervisor)
        Me.GroupControl1.Controls.Add(IdEmpresaLabel)
        Me.GroupControl1.Controls.Add(Anulaciones_por_supervisorLabel)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.chkControlpresentaciones)
        Me.GroupControl1.Controls.Add(Me.NombreTextEdit)
        Me.GroupControl1.Controls.Add(Control_presentacionesLabel)
        Me.GroupControl1.Controls.Add(DireccionLabel)
        Me.GroupControl1.Controls.Add(Me.chkOperadorlogistico)
        Me.GroupControl1.Controls.Add(Me.DireccionTextEdit)
        Me.GroupControl1.Controls.Add(Operador_logisticoLabel)
        Me.GroupControl1.Controls.Add(TelefonoLabel)
        Me.GroupControl1.Controls.Add(Me.chkClienteRapido)
        Me.GroupControl1.Controls.Add(Me.TelefonoTextEdit)
        Me.GroupControl1.Controls.Add(ClienteRapidoLabel)
        Me.GroupControl1.Controls.Add(EmailLabel)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.EmailTextEdit)
        Me.GroupControl1.Controls.Add(Razon_socialLabel)
        Me.GroupControl1.Controls.Add(Me.Razon_socialTextEdit)
        Me.GroupControl1.Controls.Add(RepresentanteLabel)
        Me.GroupControl1.Controls.Add(Me.RepresentanteTextEdit)
        Me.GroupControl1.Controls.Add(Corr_cod_barraLabel)
        Me.GroupControl1.Controls.Add(Me.Corr_cod_barraSpinEdit)
        Me.GroupControl1.Controls.Add(Path_printerLabel)
        Me.GroupControl1.Controls.Add(Me.Path_printerTextEdit)
        Me.GroupControl1.Controls.Add(ImagenLabel)
        Me.GroupControl1.Controls.Add(Puerto_escanerLabel)
        Me.GroupControl1.Controls.Add(Me.Puerto_escanerSpinEdit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1069, 595)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Datos Empresa"
        '
        'chkBuscarActualizacionHH
        '
        Me.chkBuscarActualizacionHH.Location = New System.Drawing.Point(608, 346)
        Me.chkBuscarActualizacionHH.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkBuscarActualizacionHH.MenuManager = Me.RibbonControl
        Me.chkBuscarActualizacionHH.Name = "chkBuscarActualizacionHH"
        Me.chkBuscarActualizacionHH.Properties.Caption = ""
        Me.chkBuscarActualizacionHH.Size = New System.Drawing.Size(42, 24)
        Me.chkBuscarActualizacionHH.TabIndex = 83
        '
        'cbxMotivosAjuste
        '
        Me.cbxMotivosAjuste.Location = New System.Drawing.Point(555, 91)
        Me.cbxMotivosAjuste.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxMotivosAjuste.MenuManager = Me.RibbonControl
        Me.cbxMotivosAjuste.Name = "cbxMotivosAjuste"
        Me.cbxMotivosAjuste.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbxMotivosAjuste.Size = New System.Drawing.Size(220, 22)
        Me.cbxMotivosAjuste.TabIndex = 9
        '
        'chkCodigoAutomatico
        '
        Me.chkCodigoAutomatico.EditValue = True
        Me.chkCodigoAutomatico.Location = New System.Drawing.Point(608, 284)
        Me.chkCodigoAutomatico.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkCodigoAutomatico.MenuManager = Me.RibbonControl
        Me.chkCodigoAutomatico.Name = "chkCodigoAutomatico"
        Me.chkCodigoAutomatico.Properties.Caption = ""
        Me.chkCodigoAutomatico.Size = New System.Drawing.Size(42, 24)
        Me.chkCodigoAutomatico.TabIndex = 32
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.chkMostrarContraseña)
        Me.GroupControl5.Controls.Add(Me.ClaveTextEdit)
        Me.GroupControl5.Controls.Add(ClaveLabel)
        Me.GroupControl5.Controls.Add(CodigoLabel)
        Me.GroupControl5.Controls.Add(Me.CodigoTextEdit)
        Me.GroupControl5.Location = New System.Drawing.Point(438, 393)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(338, 135)
        Me.GroupControl5.TabIndex = 36
        Me.GroupControl5.Text = "Datos sincronización con HH"
        '
        'chkMostrarContraseña
        '
        Me.chkMostrarContraseña.AutoSize = True
        Me.chkMostrarContraseña.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkMostrarContraseña.Location = New System.Drawing.Point(21, 97)
        Me.chkMostrarContraseña.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkMostrarContraseña.Name = "chkMostrarContraseña"
        Me.chkMostrarContraseña.Size = New System.Drawing.Size(109, 20)
        Me.chkMostrarContraseña.TabIndex = 4
        Me.chkMostrarContraseña.Text = "Mostrar         "
        Me.chkMostrarContraseña.UseVisualStyleBackColor = True
        '
        'ClaveTextEdit
        '
        Me.ClaveTextEdit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClaveTextEdit.Location = New System.Drawing.Point(108, 65)
        Me.ClaveTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ClaveTextEdit.MenuManager = Me.RibbonControl
        Me.ClaveTextEdit.Name = "ClaveTextEdit"
        Me.ClaveTextEdit.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ClaveTextEdit.Size = New System.Drawing.Size(199, 22)
        Me.ClaveTextEdit.TabIndex = 3
        '
        'CodigoTextEdit
        '
        Me.CodigoTextEdit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CodigoTextEdit.Location = New System.Drawing.Point(108, 33)
        Me.CodigoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CodigoTextEdit.MenuManager = Me.RibbonControl
        Me.CodigoTextEdit.Name = "CodigoTextEdit"
        Me.CodigoTextEdit.Size = New System.Drawing.Size(199, 22)
        Me.CodigoTextEdit.TabIndex = 1
        '
        'IdEmpresaSpinEdit
        '
        Me.IdEmpresaSpinEdit.Enabled = False
        Me.IdEmpresaSpinEdit.Location = New System.Drawing.Point(133, 31)
        Me.IdEmpresaSpinEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IdEmpresaSpinEdit.MenuManager = Me.RibbonControl
        Me.IdEmpresaSpinEdit.Name = "IdEmpresaSpinEdit"
        Me.IdEmpresaSpinEdit.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.IdEmpresaSpinEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.IdEmpresaSpinEdit.Size = New System.Drawing.Size(245, 22)
        Me.IdEmpresaSpinEdit.TabIndex = 1
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(133, 436)
        Me.btnExaminar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(245, 31)
        Me.btnExaminar.TabIndex = 35
        Me.btnExaminar.Text = "Examinar..."
        '
        'picFoto
        '
        Me.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picFoto.Location = New System.Drawing.Point(133, 251)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Size = New System.Drawing.Size(245, 177)
        Me.picFoto.TabIndex = 81
        Me.picFoto.TabStop = False
        '
        'chkAnulacionesporsupervisor
        '
        Me.chkAnulacionesporsupervisor.EditValue = True
        Me.chkAnulacionesporsupervisor.Location = New System.Drawing.Point(608, 223)
        Me.chkAnulacionesporsupervisor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkAnulacionesporsupervisor.MenuManager = Me.RibbonControl
        Me.chkAnulacionesporsupervisor.Name = "chkAnulacionesporsupervisor"
        Me.chkAnulacionesporsupervisor.Properties.Caption = ""
        Me.chkAnulacionesporsupervisor.Size = New System.Drawing.Size(42, 24)
        Me.chkAnulacionesporsupervisor.TabIndex = 27
        '
        'chkControlpresentaciones
        '
        Me.chkControlpresentaciones.Location = New System.Drawing.Point(608, 192)
        Me.chkControlpresentaciones.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkControlpresentaciones.MenuManager = Me.RibbonControl
        Me.chkControlpresentaciones.Name = "chkControlpresentaciones"
        Me.chkControlpresentaciones.Properties.Caption = ""
        Me.chkControlpresentaciones.Size = New System.Drawing.Size(42, 24)
        Me.chkControlpresentaciones.TabIndex = 23
        '
        'NombreTextEdit
        '
        Me.NombreTextEdit.Location = New System.Drawing.Point(133, 62)
        Me.NombreTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.NombreTextEdit.MenuManager = Me.RibbonControl
        Me.NombreTextEdit.Name = "NombreTextEdit"
        Me.NombreTextEdit.Size = New System.Drawing.Size(245, 22)
        Me.NombreTextEdit.TabIndex = 5
        '
        'chkOperadorlogistico
        '
        Me.chkOperadorlogistico.EditValue = True
        Me.chkOperadorlogistico.Location = New System.Drawing.Point(608, 254)
        Me.chkOperadorlogistico.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkOperadorlogistico.MenuManager = Me.RibbonControl
        Me.chkOperadorlogistico.Name = "chkOperadorlogistico"
        Me.chkOperadorlogistico.Properties.Caption = ""
        Me.chkOperadorlogistico.Size = New System.Drawing.Size(42, 24)
        Me.chkOperadorlogistico.TabIndex = 30
        '
        'DireccionTextEdit
        '
        Me.DireccionTextEdit.Location = New System.Drawing.Point(133, 92)
        Me.DireccionTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DireccionTextEdit.MenuManager = Me.RibbonControl
        Me.DireccionTextEdit.Name = "DireccionTextEdit"
        Me.DireccionTextEdit.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.DireccionTextEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.DireccionTextEdit.Size = New System.Drawing.Size(245, 22)
        Me.DireccionTextEdit.TabIndex = 10
        '
        'chkClienteRapido
        '
        Me.chkClienteRapido.Location = New System.Drawing.Point(608, 160)
        Me.chkClienteRapido.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkClienteRapido.MenuManager = Me.RibbonControl
        Me.chkClienteRapido.Name = "chkClienteRapido"
        Me.chkClienteRapido.Properties.Caption = ""
        Me.chkClienteRapido.Size = New System.Drawing.Size(42, 24)
        Me.chkClienteRapido.TabIndex = 19
        '
        'TelefonoTextEdit
        '
        Me.TelefonoTextEdit.Location = New System.Drawing.Point(555, 31)
        Me.TelefonoTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TelefonoTextEdit.MenuManager = Me.RibbonControl
        Me.TelefonoTextEdit.Name = "TelefonoTextEdit"
        Me.TelefonoTextEdit.Size = New System.Drawing.Size(220, 22)
        Me.TelefonoTextEdit.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(608, 315)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(42, 24)
        Me.chkActivo.TabIndex = 34
        '
        'EmailTextEdit
        '
        Me.EmailTextEdit.Location = New System.Drawing.Point(133, 123)
        Me.EmailTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EmailTextEdit.MenuManager = Me.RibbonControl
        Me.EmailTextEdit.Name = "EmailTextEdit"
        Me.EmailTextEdit.Properties.Mask.EditMask = "[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
        Me.EmailTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.EmailTextEdit.Size = New System.Drawing.Size(245, 22)
        Me.EmailTextEdit.TabIndex = 13
        '
        'Razon_socialTextEdit
        '
        Me.Razon_socialTextEdit.Location = New System.Drawing.Point(555, 62)
        Me.Razon_socialTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Razon_socialTextEdit.MenuManager = Me.RibbonControl
        Me.Razon_socialTextEdit.Name = "Razon_socialTextEdit"
        Me.Razon_socialTextEdit.Size = New System.Drawing.Size(220, 22)
        Me.Razon_socialTextEdit.TabIndex = 7
        '
        'RepresentanteTextEdit
        '
        Me.RepresentanteTextEdit.Location = New System.Drawing.Point(133, 183)
        Me.RepresentanteTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RepresentanteTextEdit.MenuManager = Me.RibbonControl
        Me.RepresentanteTextEdit.Name = "RepresentanteTextEdit"
        Me.RepresentanteTextEdit.Size = New System.Drawing.Size(245, 22)
        Me.RepresentanteTextEdit.TabIndex = 21
        '
        'Corr_cod_barraSpinEdit
        '
        Me.Corr_cod_barraSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Corr_cod_barraSpinEdit.Location = New System.Drawing.Point(555, 123)
        Me.Corr_cod_barraSpinEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Corr_cod_barraSpinEdit.MenuManager = Me.RibbonControl
        Me.Corr_cod_barraSpinEdit.Name = "Corr_cod_barraSpinEdit"
        Me.Corr_cod_barraSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Corr_cod_barraSpinEdit.Size = New System.Drawing.Size(220, 24)
        Me.Corr_cod_barraSpinEdit.TabIndex = 15
        Me.Corr_cod_barraSpinEdit.Visible = False
        '
        'Path_printerTextEdit
        '
        Me.Path_printerTextEdit.Location = New System.Drawing.Point(133, 153)
        Me.Path_printerTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Path_printerTextEdit.MenuManager = Me.RibbonControl
        Me.Path_printerTextEdit.Name = "Path_printerTextEdit"
        Me.Path_printerTextEdit.Size = New System.Drawing.Size(245, 22)
        Me.Path_printerTextEdit.TabIndex = 17
        '
        'Puerto_escanerSpinEdit
        '
        Me.Puerto_escanerSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.Puerto_escanerSpinEdit.Location = New System.Drawing.Point(133, 214)
        Me.Puerto_escanerSpinEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Puerto_escanerSpinEdit.MenuManager = Me.RibbonControl
        Me.Puerto_escanerSpinEdit.Name = "Puerto_escanerSpinEdit"
        Me.Puerto_escanerSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Puerto_escanerSpinEdit.Size = New System.Drawing.Size(245, 24)
        Me.Puerto_escanerSpinEdit.TabIndex = 25
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.chkExigirPoliticaDeContraseña)
        Me.GroupControl3.Controls.Add(Me.SpinEditIntentos)
        Me.GroupControl3.Controls.Add(Me.SpinEditDuracionTemporal)
        Me.GroupControl3.Controls.Add(Me.SpinEditDuracion)
        Me.GroupControl3.Controls.Add(Label5)
        Me.GroupControl3.Controls.Add(Label4)
        Me.GroupControl3.Controls.Add(Label3)
        Me.GroupControl3.Controls.Add(Label2)
        Me.GroupControl3.Controls.Add(Label1)
        Me.GroupControl3.Controls.Add(Me.cbxBloqueo)
        Me.GroupControl3.Controls.Add(Me.cbxCaducidad)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1069, 595)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Aplicar Politica de Seguridad a usuarios de esta empresa"
        '
        'chkExigirPoliticaDeContraseña
        '
        Me.chkExigirPoliticaDeContraseña.Location = New System.Drawing.Point(28, 46)
        Me.chkExigirPoliticaDeContraseña.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkExigirPoliticaDeContraseña.MenuManager = Me.RibbonControl
        Me.chkExigirPoliticaDeContraseña.Name = "chkExigirPoliticaDeContraseña"
        Me.chkExigirPoliticaDeContraseña.Properties.Caption = "Exigir política de contraseñas"
        Me.chkExigirPoliticaDeContraseña.Size = New System.Drawing.Size(219, 24)
        Me.chkExigirPoliticaDeContraseña.TabIndex = 0
        '
        'SpinEditIntentos
        '
        Me.SpinEditIntentos.EditValue = New Decimal(New Integer() {3, 0, 0, 0})
        Me.SpinEditIntentos.Location = New System.Drawing.Point(225, 186)
        Me.SpinEditIntentos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SpinEditIntentos.MenuManager = Me.RibbonControl
        Me.SpinEditIntentos.Name = "SpinEditIntentos"
        Me.SpinEditIntentos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SpinEditIntentos.Size = New System.Drawing.Size(52, 24)
        Me.SpinEditIntentos.TabIndex = 9
        '
        'SpinEditDuracionTemporal
        '
        Me.SpinEditDuracionTemporal.EditValue = New Decimal(New Integer() {7, 0, 0, 0})
        Me.SpinEditDuracionTemporal.Location = New System.Drawing.Point(166, 146)
        Me.SpinEditDuracionTemporal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SpinEditDuracionTemporal.MenuManager = Me.RibbonControl
        Me.SpinEditDuracionTemporal.Name = "SpinEditDuracionTemporal"
        Me.SpinEditDuracionTemporal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SpinEditDuracionTemporal.Size = New System.Drawing.Size(52, 24)
        Me.SpinEditDuracionTemporal.TabIndex = 6
        '
        'SpinEditDuracion
        '
        Me.SpinEditDuracion.EditValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.SpinEditDuracion.Location = New System.Drawing.Point(166, 108)
        Me.SpinEditDuracion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SpinEditDuracion.MenuManager = Me.RibbonControl
        Me.SpinEditDuracion.Name = "SpinEditDuracion"
        Me.SpinEditDuracion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SpinEditDuracion.Size = New System.Drawing.Size(52, 24)
        Me.SpinEditDuracion.TabIndex = 3
        '
        'cbxBloqueo
        '
        Me.cbxBloqueo.EditValue = True
        Me.cbxBloqueo.Location = New System.Drawing.Point(28, 186)
        Me.cbxBloqueo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxBloqueo.MenuManager = Me.RibbonControl
        Me.cbxBloqueo.Name = "cbxBloqueo"
        Me.cbxBloqueo.Properties.Caption = "Bloquear usuario despues de "
        Me.cbxBloqueo.Size = New System.Drawing.Size(190, 24)
        Me.cbxBloqueo.TabIndex = 8
        '
        'cbxCaducidad
        '
        Me.cbxCaducidad.Location = New System.Drawing.Point(28, 76)
        Me.cbxCaducidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxCaducidad.MenuManager = Me.RibbonControl
        Me.cbxCaducidad.Name = "cbxCaducidad"
        Me.cbxCaducidad.Properties.Caption = "Contraseña nunca expira"
        Me.cbxCaducidad.Size = New System.Drawing.Size(180, 24)
        Me.cbxCaducidad.TabIndex = 1
        '
        'TabControlEmpresa
        '
        Me.TabControlEmpresa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlEmpresa.Location = New System.Drawing.Point(0, 193)
        Me.TabControlEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabControlEmpresa.Name = "TabControlEmpresa"
        Me.TabControlEmpresa.SelectedTabPage = Me.General
        Me.TabControlEmpresa.Size = New System.Drawing.Size(1071, 625)
        Me.TabControlEmpresa.TabIndex = 0
        Me.TabControlEmpresa.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.General, Me.Seguridad, Me.XtraTabPage1})
        '
        'General
        '
        Me.General.Controls.Add(Me.GroupControl1)
        Me.General.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.General.Name = "General"
        Me.General.Size = New System.Drawing.Size(1069, 595)
        Me.General.Text = "General"
        '
        'Seguridad
        '
        Me.Seguridad.Controls.Add(Me.GroupControl3)
        Me.Seguridad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Seguridad.Name = "Seguridad"
        Me.Seguridad.Size = New System.Drawing.Size(1069, 595)
        Me.Seguridad.Text = "Seguridad"
        '
        'dkEmpresa
        '
        Me.dkEmpresa.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkEmpresa.Form = Me
        Me.dkEmpresa.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 818)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1071, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("e1682e0c-507d-4372-8c61-317c8a9af873")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 93)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(1071, 114)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 28)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1062, 81)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.txtAWSToken)
        Me.XtraTabPage1.Controls.Add(lblAWSToken)
        Me.XtraTabPage1.Controls.Add(Me.txtVersionBD)
        Me.XtraTabPage1.Controls.Add(lblVersionBD)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1069, 595)
        Me.XtraTabPage1.Text = "AWS_Config"
        '
        'txtVersionBD
        '
        Me.txtVersionBD.Enabled = False
        Me.txtVersionBD.Location = New System.Drawing.Point(154, 47)
        Me.txtVersionBD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVersionBD.MenuManager = Me.RibbonControl
        Me.txtVersionBD.Name = "txtVersionBD"
        Me.txtVersionBD.Properties.Appearance.BackColor = System.Drawing.Color.Gainsboro
        Me.txtVersionBD.Properties.Appearance.Options.UseBackColor = True
        Me.txtVersionBD.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtVersionBD.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtVersionBD.Size = New System.Drawing.Size(245, 22)
        Me.txtVersionBD.TabIndex = 3
        '
        'lblVersionBD
        '
        lblVersionBD.AutoSize = True
        lblVersionBD.Location = New System.Drawing.Point(48, 50)
        lblVersionBD.Name = "lblVersionBD"
        lblVersionBD.Size = New System.Drawing.Size(92, 16)
        lblVersionBD.TabIndex = 2
        lblVersionBD.Text = "Versión de BD:"
        '
        'txtAWSToken
        '
        Me.txtAWSToken.Enabled = False
        Me.txtAWSToken.Location = New System.Drawing.Point(154, 89)
        Me.txtAWSToken.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAWSToken.MenuManager = Me.RibbonControl
        Me.txtAWSToken.Name = "txtAWSToken"
        Me.txtAWSToken.Properties.Appearance.BackColor = System.Drawing.Color.Gainsboro
        Me.txtAWSToken.Properties.Appearance.Options.UseBackColor = True
        Me.txtAWSToken.Properties.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.txtAWSToken.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtAWSToken.Size = New System.Drawing.Size(245, 22)
        Me.txtAWSToken.TabIndex = 5
        '
        'lblAWSToken
        '
        lblAWSToken.AutoSize = True
        lblAWSToken.Location = New System.Drawing.Point(48, 92)
        lblAWSToken.Name = "lblAWSToken"
        lblAWSToken.Size = New System.Drawing.Size(79, 16)
        lblAWSToken.TabIndex = 4
        lblAWSToken.Text = "AWS Token:"
        '
        'frmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1071, 844)
        Me.Controls.Add(Me.TabControlEmpresa)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmEmpresa"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de empresa"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkBuscarActualizacionHH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxMotivosAjuste.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCodigoAutomatico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.ClaveTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CodigoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IdEmpresaSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAnulacionesporsupervisor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlpresentaciones.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NombreTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOperadorlogistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DireccionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkClienteRapido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TelefonoTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmailTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Razon_socialTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepresentanteTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Corr_cod_barraSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Path_printerTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Puerto_escanerSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.chkExigirPoliticaDeContraseña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinEditIntentos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinEditDuracionTemporal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpinEditDuracion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxBloqueo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxCaducidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabControlEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlEmpresa.ResumeLayout(False)
        Me.General.ResumeLayout(False)
        Me.Seguridad.ResumeLayout(False)
        CType(Me.dkEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        CType(Me.txtVersionBD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAWSToken.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents IdEmpresaSpinEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnExaminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents picFoto As System.Windows.Forms.PictureBox
    Friend WithEvents chkAnulacionesporsupervisor As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkControlpresentaciones As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents NombreTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkOperadorlogistico As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents DireccionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkClienteRapido As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TelefonoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents EmailTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Razon_socialTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RepresentanteTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Corr_cod_barraSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents Path_printerTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Puerto_escanerSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents CodigoTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ClaveTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SpinEditIntentos As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SpinEditDuracionTemporal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SpinEditDuracion As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cbxBloqueo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cbxCaducidad As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkCodigoAutomatico As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkMostrarContraseña As CheckBox
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TabControlEmpresa As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents General As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Seguridad As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dkEmpresa As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents chkExigirPoliticaDeContraseña As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cbxMotivosAjuste As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkBuscarActualizacionHH As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtAWSToken As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtVersionBD As DevExpress.XtraEditors.TextEdit
End Class
