<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpresa_Transporte_Piloto
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
        Dim lblFechaExpiracionCarnet As System.Windows.Forms.Label
        Dim lblNoCarnet As System.Windows.Forms.Label
        Dim lblCorreo As System.Windows.Forms.Label
        Dim lblTelefono As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim ImagenLabel As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim IdEmpresaLabel As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpresa_Transporte_Piloto))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImprimirCarnet = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanBitacora = New DevExpress.XtraEditors.GroupControl()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.PanDatosMotivoAnulacion = New DevExpress.XtraEditors.GroupControl()
        Me.dtmFechaExpiracionLicencia = New DevExpress.XtraEditors.DateEdit()
        Me.dtmFechaExpiracionCarnet = New DevExpress.XtraEditors.DateEdit()
        Me.dtmFechaSalida = New DevExpress.XtraEditors.DateEdit()
        Me.dtmFechaInicio = New DevExpress.XtraEditors.DateEdit()
        Me.dtmFechaNacimiento = New DevExpress.XtraEditors.DateEdit()
        Me.cmbEmpresaT = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNoCarnet = New DevExpress.XtraEditors.TextEdit()
        Me.txtCorreo = New DevExpress.XtraEditors.TextEdit()
        Me.txtTelefono = New DevExpress.XtraEditors.TextEdit()
        Me.txtApellidos = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombres = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.btnExaminar = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDPI = New DevExpress.XtraEditors.TextEdit()
        Me.picFoto = New System.Windows.Forms.PictureBox()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoBarra = New DevExpress.XtraEditors.TextEdit()
        Me.txtLicencia = New DevExpress.XtraEditors.TextEdit()
        Me.cmbTipoLicencia = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        lblFechaExpiracionCarnet = New System.Windows.Forms.Label()
        lblNoCarnet = New System.Windows.Forms.Label()
        lblCorreo = New System.Windows.Forms.Label()
        lblTelefono = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        ImagenLabel = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        IdEmpresaLabel = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanBitacora.SuspendLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanDatosMotivoAnulacion.SuspendLayout()
        CType(Me.dtmFechaExpiracionLicencia.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaExpiracionLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaExpiracionCarnet.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaExpiracionCarnet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaSalida.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaNacimiento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtmFechaNacimiento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmpresaT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoCarnet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApellidos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombres.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFechaExpiracionCarnet
        '
        lblFechaExpiracionCarnet.AutoSize = True
        lblFechaExpiracionCarnet.Location = New System.Drawing.Point(25, 330)
        lblFechaExpiracionCarnet.Name = "lblFechaExpiracionCarnet"
        lblFechaExpiracionCarnet.Size = New System.Drawing.Size(121, 17)
        lblFechaExpiracionCarnet.TabIndex = 18
        lblFechaExpiracionCarnet.Text = "Expiración Carnet:"
        '
        'lblNoCarnet
        '
        lblNoCarnet.AutoSize = True
        lblNoCarnet.Location = New System.Drawing.Point(25, 300)
        lblNoCarnet.Name = "lblNoCarnet"
        lblNoCarnet.Size = New System.Drawing.Size(79, 17)
        lblNoCarnet.TabIndex = 16
        lblNoCarnet.Text = "No. Carnet:"
        '
        'lblCorreo
        '
        lblCorreo.AutoSize = True
        lblCorreo.Location = New System.Drawing.Point(25, 268)
        lblCorreo.Name = "lblCorreo"
        lblCorreo.Size = New System.Drawing.Size(126, 17)
        lblCorreo.TabIndex = 14
        lblCorreo.Text = "Correo Electrónico:"
        '
        'lblTelefono
        '
        lblTelefono.AutoSize = True
        lblTelefono.Location = New System.Drawing.Point(25, 236)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New System.Drawing.Size(60, 17)
        lblTelefono.TabIndex = 12
        lblTelefono.Text = "Teléfono"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Label10.Location = New System.Drawing.Point(25, 142)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(64, 17)
        Label10.TabIndex = 6
        Label10.Text = "Apellidos:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Label9.Location = New System.Drawing.Point(25, 108)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(68, 17)
        Label9.TabIndex = 4
        Label9.Text = "Nombres:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(25, 44)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(51, 17)
        Label8.TabIndex = 0
        Label8.Text = "Código"
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        NombreLabel.Location = New System.Drawing.Point(25, 201)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(60, 17)
        NombreLabel.TabIndex = 10
        NombreLabel.Text = "No. DPI:"
        '
        'ImagenLabel
        '
        ImagenLabel.AutoSize = True
        ImagenLabel.Location = New System.Drawing.Point(459, 300)
        ImagenLabel.Name = "ImagenLabel"
        ImagenLabel.Size = New System.Drawing.Size(36, 17)
        ImagenLabel.TabIndex = 36
        ImagenLabel.Text = "Foto"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(459, 74)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(90, 17)
        Label7.TabIndex = 22
        Label7.Text = "Tipo Licencia:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(459, 269)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(87, 17)
        Label6.TabIndex = 34
        Label6.Text = "Fecha Salida:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(459, 235)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(99, 17)
        Label4.TabIndex = 32
        Label4.Text = "Fecha Ingreso:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(459, 202)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(120, 17)
        Label3.TabIndex = 30
        Label3.Text = "Fecha Nacimiento:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(459, 171)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(69, 17)
        Label5.TabIndex = 28
        Label5.Text = "Dirección:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(459, 139)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(111, 17)
        Label2.TabIndex = 26
        Label2.Text = "Código de Barra:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Label1.Location = New System.Drawing.Point(25, 171)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(85, 17)
        Label1.TabIndex = 8
        Label1.Text = "No. Licencia:"
        '
        'IdEmpresaLabel
        '
        IdEmpresaLabel.AutoSize = True
        IdEmpresaLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        IdEmpresaLabel.Location = New System.Drawing.Point(25, 75)
        IdEmpresaLabel.Name = "IdEmpresaLabel"
        IdEmpresaLabel.Size = New System.Drawing.Size(137, 17)
        IdEmpresaLabel.TabIndex = 2
        IdEmpresaLabel.Text = "Empresa Transporte:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(459, 109)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(127, 17)
        Label11.TabIndex = 24
        Label11.Text = "Expiración Licencia:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(25, 401)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(51, 17)
        Label18.TabIndex = 20
        Label18.Text = "Activo:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(48, 78)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(97, 17)
        Fec_agrLabel.TabIndex = 2
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(48, 46)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(106, 17)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(532, 78)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(102, 17)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(532, 46)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(111, 17)
        User_modLabel.TabIndex = 4
        User_modLabel.Text = "Usuario Modificó:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.BarButtonItem1, Me.cmdImprimirCarnet})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(981, 193)
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
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Imprimir"
        Me.BarButtonItem1.Id = 5
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdImprimirCarnet
        '
        Me.cmdImprimirCarnet.Caption = "Imprimir Carnet"
        Me.cmdImprimirCarnet.Id = 6
        Me.cmdImprimirCarnet.ImageOptions.SvgImage = CType(resources.GetObject("cmdImprimirCarnet.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImprimirCarnet.Name = "cmdImprimirCarnet"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Empresa Transporte Piloto"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImprimirCarnet)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 810)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(981, 30)
        '
        'PanBitacora
        '
        Me.PanBitacora.Controls.Add(Me.Fec_agrDateEdit)
        Me.PanBitacora.Controls.Add(Fec_agrLabel)
        Me.PanBitacora.Controls.Add(Me.User_agrTextEdit)
        Me.PanBitacora.Controls.Add(User_agrLabel)
        Me.PanBitacora.Controls.Add(Me.Fec_modDateEdit)
        Me.PanBitacora.Controls.Add(Fec_modLabel)
        Me.PanBitacora.Controls.Add(Me.User_modTextEdit)
        Me.PanBitacora.Controls.Add(User_modLabel)
        Me.PanBitacora.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanBitacora.Location = New System.Drawing.Point(0, 687)
        Me.PanBitacora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanBitacora.Name = "PanBitacora"
        Me.PanBitacora.Size = New System.Drawing.Size(981, 123)
        Me.PanBitacora.TabIndex = 1
        Me.PanBitacora.Text = "Bitácora"
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(150, 74)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 3
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(150, 42)
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
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(635, 74)
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
        Me.User_modTextEdit.Location = New System.Drawing.Point(635, 42)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 5
        '
        'PanDatosMotivoAnulacion
        '
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtmFechaExpiracionLicencia)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtmFechaExpiracionCarnet)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtmFechaSalida)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtLicencia)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label1)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtmFechaInicio)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.dtmFechaNacimiento)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.cmbEmpresaT)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label18)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.chkActivo)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label11)
        Me.PanDatosMotivoAnulacion.Controls.Add(lblFechaExpiracionCarnet)
        Me.PanDatosMotivoAnulacion.Controls.Add(lblNoCarnet)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtNoCarnet)
        Me.PanDatosMotivoAnulacion.Controls.Add(lblCorreo)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtCorreo)
        Me.PanDatosMotivoAnulacion.Controls.Add(lblTelefono)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtTelefono)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label10)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtApellidos)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label9)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtNombres)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.lblCodigo)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label8)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.btnExaminar)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtDPI)
        Me.PanDatosMotivoAnulacion.Controls.Add(NombreLabel)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.picFoto)
        Me.PanDatosMotivoAnulacion.Controls.Add(ImagenLabel)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label7)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label6)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label4)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label3)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label5)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtDireccion)
        Me.PanDatosMotivoAnulacion.Controls.Add(Label2)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.txtCodigoBarra)
        Me.PanDatosMotivoAnulacion.Controls.Add(IdEmpresaLabel)
        Me.PanDatosMotivoAnulacion.Controls.Add(Me.cmbTipoLicencia)
        Me.PanDatosMotivoAnulacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanDatosMotivoAnulacion.Location = New System.Drawing.Point(0, 193)
        Me.PanDatosMotivoAnulacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanDatosMotivoAnulacion.Name = "PanDatosMotivoAnulacion"
        Me.PanDatosMotivoAnulacion.Size = New System.Drawing.Size(981, 494)
        Me.PanDatosMotivoAnulacion.TabIndex = 0
        Me.PanDatosMotivoAnulacion.Text = "Datos Empresa Transporte Piloto"
        '
        'dtmFechaExpiracionLicencia
        '
        Me.dtmFechaExpiracionLicencia.EditValue = New Date(2017, 11, 23, 10, 12, 16, 716)
        Me.dtmFechaExpiracionLicencia.Location = New System.Drawing.Point(596, 107)
        Me.dtmFechaExpiracionLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmFechaExpiracionLicencia.MenuManager = Me.RibbonControl
        Me.dtmFechaExpiracionLicencia.Name = "dtmFechaExpiracionLicencia"
        Me.dtmFechaExpiracionLicencia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaExpiracionLicencia.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaExpiracionLicencia.Size = New System.Drawing.Size(241, 22)
        Me.dtmFechaExpiracionLicencia.TabIndex = 25
        '
        'dtmFechaExpiracionCarnet
        '
        Me.dtmFechaExpiracionCarnet.EditValue = New Date(2017, 11, 23, 9, 44, 55, 502)
        Me.dtmFechaExpiracionCarnet.Location = New System.Drawing.Point(182, 328)
        Me.dtmFechaExpiracionCarnet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmFechaExpiracionCarnet.MenuManager = Me.RibbonControl
        Me.dtmFechaExpiracionCarnet.Name = "dtmFechaExpiracionCarnet"
        Me.dtmFechaExpiracionCarnet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaExpiracionCarnet.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaExpiracionCarnet.Size = New System.Drawing.Size(240, 22)
        Me.dtmFechaExpiracionCarnet.TabIndex = 19
        '
        'dtmFechaSalida
        '
        Me.dtmFechaSalida.EditValue = New Date(2017, 11, 20, 10, 26, 39, 243)
        Me.dtmFechaSalida.Location = New System.Drawing.Point(596, 265)
        Me.dtmFechaSalida.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmFechaSalida.MenuManager = Me.RibbonControl
        Me.dtmFechaSalida.Name = "dtmFechaSalida"
        Me.dtmFechaSalida.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaSalida.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaSalida.Size = New System.Drawing.Size(240, 22)
        Me.dtmFechaSalida.TabIndex = 35
        '
        'dtmFechaInicio
        '
        Me.dtmFechaInicio.EditValue = New Date(2017, 11, 20, 10, 25, 53, 370)
        Me.dtmFechaInicio.Location = New System.Drawing.Point(596, 232)
        Me.dtmFechaInicio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmFechaInicio.MenuManager = Me.RibbonControl
        Me.dtmFechaInicio.Name = "dtmFechaInicio"
        Me.dtmFechaInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaInicio.Size = New System.Drawing.Size(240, 22)
        Me.dtmFechaInicio.TabIndex = 33
        '
        'dtmFechaNacimiento
        '
        Me.dtmFechaNacimiento.EditValue = New Date(2017, 11, 20, 10, 25, 5, 211)
        Me.dtmFechaNacimiento.Location = New System.Drawing.Point(596, 198)
        Me.dtmFechaNacimiento.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtmFechaNacimiento.MenuManager = Me.RibbonControl
        Me.dtmFechaNacimiento.Name = "dtmFechaNacimiento"
        Me.dtmFechaNacimiento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaNacimiento.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtmFechaNacimiento.Size = New System.Drawing.Size(240, 22)
        Me.dtmFechaNacimiento.TabIndex = 31
        '
        'cmbEmpresaT
        '
        Me.cmbEmpresaT.Location = New System.Drawing.Point(182, 72)
        Me.cmbEmpresaT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEmpresaT.MenuManager = Me.RibbonControl
        Me.cmbEmpresaT.Name = "cmbEmpresaT"
        Me.cmbEmpresaT.Properties.Appearance.BackColor = System.Drawing.Color.LavenderBlush
        Me.cmbEmpresaT.Properties.Appearance.Options.UseBackColor = True
        Me.cmbEmpresaT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEmpresaT.Properties.NullText = ""
        Me.cmbEmpresaT.Size = New System.Drawing.Size(242, 22)
        Me.cmbEmpresaT.TabIndex = 3
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(182, 398)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(84, 24)
        Me.chkActivo.TabIndex = 21
        '
        'txtNoCarnet
        '
        Me.txtNoCarnet.Location = New System.Drawing.Point(182, 296)
        Me.txtNoCarnet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoCarnet.Name = "txtNoCarnet"
        Me.txtNoCarnet.Size = New System.Drawing.Size(241, 22)
        Me.txtNoCarnet.TabIndex = 17
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(182, 264)
        Me.txtCorreo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(241, 22)
        Me.txtCorreo.TabIndex = 15
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(182, 232)
        Me.txtTelefono.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(241, 22)
        Me.txtTelefono.TabIndex = 13
        '
        'txtApellidos
        '
        Me.txtApellidos.Location = New System.Drawing.Point(182, 138)
        Me.txtApellidos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtApellidos.Name = "txtApellidos"
        Me.txtApellidos.Properties.Appearance.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtApellidos.Properties.Appearance.Options.UseBackColor = True
        Me.txtApellidos.Size = New System.Drawing.Size(241, 22)
        Me.txtApellidos.TabIndex = 7
        '
        'txtNombres
        '
        Me.txtNombres.Location = New System.Drawing.Point(182, 105)
        Me.txtNombres.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombres.Name = "txtNombres"
        Me.txtNombres.Properties.Appearance.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtNombres.Properties.Appearance.Options.UseBackColor = True
        Me.txtNombres.Size = New System.Drawing.Size(241, 22)
        Me.txtNombres.TabIndex = 5
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(180, 44)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(0, 17)
        Me.lblCodigo.TabIndex = 1
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(596, 434)
        Me.btnExaminar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(87, 28)
        Me.btnExaminar.TabIndex = 37
        Me.btnExaminar.Text = "Examinar..."
        '
        'txtDPI
        '
        Me.txtDPI.Location = New System.Drawing.Point(182, 198)
        Me.txtDPI.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDPI.Name = "txtDPI"
        Me.txtDPI.Properties.Appearance.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtDPI.Properties.Appearance.Options.UseBackColor = True
        Me.txtDPI.Size = New System.Drawing.Size(241, 22)
        Me.txtDPI.TabIndex = 11
        '
        'picFoto
        '
        Me.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picFoto.Location = New System.Drawing.Point(596, 295)
        Me.picFoto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picFoto.Name = "picFoto"
        Me.picFoto.Size = New System.Drawing.Size(241, 132)
        Me.picFoto.TabIndex = 54
        Me.picFoto.TabStop = False
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(596, 168)
        Me.txtDireccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(241, 22)
        Me.txtDireccion.TabIndex = 29
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.Location = New System.Drawing.Point(596, 136)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Size = New System.Drawing.Size(241, 22)
        Me.txtCodigoBarra.TabIndex = 27
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(182, 168)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.Appearance.BackColor = System.Drawing.Color.LavenderBlush
        Me.txtLicencia.Properties.Appearance.Options.UseBackColor = True
        Me.txtLicencia.Size = New System.Drawing.Size(241, 22)
        Me.txtLicencia.TabIndex = 9
        '
        'cmbTipoLicencia
        '
        Me.cmbTipoLicencia.Location = New System.Drawing.Point(596, 72)
        Me.cmbTipoLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbTipoLicencia.MenuManager = Me.RibbonControl
        Me.cmbTipoLicencia.Name = "cmbTipoLicencia"
        Me.cmbTipoLicencia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoLicencia.Properties.ImmediatePopup = True
        Me.cmbTipoLicencia.Properties.NullText = ""
        Me.cmbTipoLicencia.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbTipoLicencia.Size = New System.Drawing.Size(241, 22)
        Me.cmbTipoLicencia.TabIndex = 23
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 13
        Me.cmdImprimir.ImageOptions.Image = CType(resources.GetObject("cmdImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImprimir.ImageOptions.LargeImage = CType(resources.GetObject("cmdImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'frmEmpresa_Transporte_Piloto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(981, 840)
        Me.Controls.Add(Me.PanDatosMotivoAnulacion)
        Me.Controls.Add(Me.PanBitacora)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmEmpresa_Transporte_Piloto"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Empresa Transporte Piloto"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanBitacora.ResumeLayout(False)
        Me.PanBitacora.PerformLayout()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanDatosMotivoAnulacion.ResumeLayout(False)
        Me.PanDatosMotivoAnulacion.PerformLayout()
        CType(Me.dtmFechaExpiracionLicencia.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaExpiracionLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaExpiracionCarnet.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaExpiracionCarnet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaSalida.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaNacimiento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtmFechaNacimiento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmpresaT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoCarnet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApellidos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombres.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PanBitacora As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PanDatosMotivoAnulacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNoCarnet As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCorreo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTelefono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtApellidos As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombres As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents btnExaminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtDPI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents picFoto As System.Windows.Forms.PictureBox
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoBarra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLicencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImprimirCarnet As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbEmpresaT As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoLicencia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dtmFechaNacimiento As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaInicio As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaSalida As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaExpiracionCarnet As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtmFechaExpiracionLicencia As DevExpress.XtraEditors.DateEdit
End Class
