<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManufacturaPicking
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
        Me.components = New System.ComponentModel.Container()
        Dim Label4 As System.Windows.Forms.Label
        Dim lblScan As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim lblDescripcionProducto As System.Windows.Forms.Label
        Dim lblBarraProducto As System.Windows.Forms.Label
        Dim lblCodigoBarraProducto As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.lblRegistros = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GrpAsignacionTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.txtDescripcionProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtBarraProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigoProducto = New DevExpress.XtraEditors.TextEdit()
        Me.grpManual = New DevExpress.XtraEditors.GroupControl()
        Me.txtCantidadRegistrada = New System.Windows.Forms.LinkLabel()
        Me.txtCantidadEsperada = New System.Windows.Forms.LinkLabel()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtReferenciaSAP = New DevExpress.XtraEditors.TextEdit()
        Me.txtCliente = New DevExpress.XtraEditors.TextEdit()
        Me.txtPedido = New DevExpress.XtraEditors.TextEdit()
        Me.grpScan = New DevExpress.XtraEditors.GroupControl()
        Me.txtScanner = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidad = New DevExpress.XtraEditors.TextEdit()
        Me.GrpFactura = New DevExpress.XtraEditors.GroupControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Label4 = New System.Windows.Forms.Label()
        lblScan = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        lblDescripcionProducto = New System.Windows.Forms.Label()
        lblBarraProducto = New System.Windows.Forms.Label()
        lblCodigoBarraProducto = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAsignacionTransaccion.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarraProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpManual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpManual.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtReferenciaSAP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScan.SuspendLayout()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpFactura.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Label4.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(164, 33)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(31, 59)
        Label4.TabIndex = 39
        Label4.Text = "/"
        '
        'lblScan
        '
        lblScan.AutoSize = True
        lblScan.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblScan.Location = New System.Drawing.Point(35, 62)
        lblScan.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblScan.Name = "lblScan"
        lblScan.Size = New System.Drawing.Size(66, 21)
        lblScan.TabIndex = 43
        lblScan.Text = "Código:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(498, 33)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(81, 21)
        Label1.TabIndex = 49
        Label1.Text = "Cantidad:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(6, 45)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(66, 21)
        Label2.TabIndex = 43
        Label2.Text = "Código:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(6, 83)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(67, 21)
        Label3.TabIndex = 45
        Label3.Text = "Cliente:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(6, 121)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(95, 21)
        Label5.TabIndex = 47
        Label5.Text = "Referencia:"
        '
        'lblDescripcionProducto
        '
        lblDescripcionProducto.AutoSize = True
        lblDescripcionProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblDescripcionProducto.Location = New System.Drawing.Point(6, 121)
        lblDescripcionProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblDescripcionProducto.Name = "lblDescripcionProducto"
        lblDescripcionProducto.Size = New System.Drawing.Size(102, 21)
        lblDescripcionProducto.TabIndex = 47
        lblDescripcionProducto.Text = "Descripción:"
        '
        'lblBarraProducto
        '
        lblBarraProducto.AutoSize = True
        lblBarraProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblBarraProducto.Location = New System.Drawing.Point(6, 83)
        lblBarraProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBarraProducto.Name = "lblBarraProducto"
        lblBarraProducto.Size = New System.Drawing.Size(56, 21)
        lblBarraProducto.TabIndex = 45
        lblBarraProducto.Text = "Barra:"
        '
        'lblCodigoBarraProducto
        '
        lblCodigoBarraProducto.AutoSize = True
        lblCodigoBarraProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblCodigoBarraProducto.Location = New System.Drawing.Point(6, 45)
        lblCodigoBarraProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoBarraProducto.Name = "lblCodigoBarraProducto"
        lblCodigoBarraProducto.Size = New System.Drawing.Size(66, 21)
        lblCodigoBarraProducto.TabIndex = 43
        lblCodigoBarraProducto.Text = "Código:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.lblRegistros})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1220, 40)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 5
        Me.lblRegistros.Name = "lblRegistros"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 950)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1220, 30)
        '
        'GrpAsignacionTransaccion
        '
        Me.GrpAsignacionTransaccion.Controls.Add(Me.GroupControl2)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.GroupControl1)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.grpScan)
        Me.GrpAsignacionTransaccion.Controls.Add(Me.grpManual)
        Me.GrpAsignacionTransaccion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpAsignacionTransaccion.Location = New System.Drawing.Point(0, 40)
        Me.GrpAsignacionTransaccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpAsignacionTransaccion.Name = "GrpAsignacionTransaccion"
        Me.GrpAsignacionTransaccion.Size = New System.Drawing.Size(1220, 233)
        Me.GrpAsignacionTransaccion.TabIndex = 41
        Me.GrpAsignacionTransaccion.Text = "Detalle de Transacción"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.GroupControl2.AppearanceCaption.Options.UseBackColor = True
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.txtDescripcionProducto)
        Me.GroupControl2.Controls.Add(lblDescripcionProducto)
        Me.GroupControl2.Controls.Add(Me.txtBarraProducto)
        Me.GroupControl2.Controls.Add(lblBarraProducto)
        Me.GroupControl2.Controls.Add(Me.txtCodigoProducto)
        Me.GroupControl2.Controls.Add(lblCodigoBarraProducto)
        Me.GroupControl2.Location = New System.Drawing.Point(659, 41)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(548, 170)
        Me.GroupControl2.TabIndex = 53
        Me.GroupControl2.Text = "Información del producto"
        '
        'txtDescripcionProducto
        '
        Me.txtDescripcionProducto.Location = New System.Drawing.Point(104, 118)
        Me.txtDescripcionProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcionProducto.Name = "txtDescripcionProducto"
        Me.txtDescripcionProducto.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtDescripcionProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtDescripcionProducto.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcionProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcionProducto.Properties.MaxLength = 50
        Me.txtDescripcionProducto.Size = New System.Drawing.Size(385, 28)
        Me.txtDescripcionProducto.TabIndex = 46
        '
        'txtBarraProducto
        '
        Me.txtBarraProducto.Location = New System.Drawing.Point(104, 80)
        Me.txtBarraProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBarraProducto.Name = "txtBarraProducto"
        Me.txtBarraProducto.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtBarraProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarraProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtBarraProducto.Properties.Appearance.Options.UseFont = True
        Me.txtBarraProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtBarraProducto.Properties.MaxLength = 50
        Me.txtBarraProducto.Size = New System.Drawing.Size(385, 28)
        Me.txtBarraProducto.TabIndex = 44
        '
        'txtCodigoProducto
        '
        Me.txtCodigoProducto.Location = New System.Drawing.Point(104, 42)
        Me.txtCodigoProducto.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCodigoProducto.Name = "txtCodigoProducto"
        Me.txtCodigoProducto.Properties.Appearance.BackColor = System.Drawing.Color.Wheat
        Me.txtCodigoProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoProducto.Properties.Appearance.Options.UseBackColor = True
        Me.txtCodigoProducto.Properties.Appearance.Options.UseFont = True
        Me.txtCodigoProducto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCodigoProducto.Properties.MaxLength = 50
        Me.txtCodigoProducto.Size = New System.Drawing.Size(385, 28)
        Me.txtCodigoProducto.TabIndex = 42
        '
        'grpManual
        '
        Me.grpManual.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpManual.AppearanceCaption.Options.UseBackColor = True
        Me.grpManual.AppearanceCaption.Options.UseTextOptions = True
        Me.grpManual.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpManual.Controls.Add(Me.txtCantidadRegistrada)
        Me.grpManual.Controls.Add(Label4)
        Me.grpManual.Controls.Add(Me.txtCantidadEsperada)
        Me.grpManual.Location = New System.Drawing.Point(796, 41)
        Me.grpManual.Name = "grpManual"
        Me.grpManual.Size = New System.Drawing.Size(385, 113)
        Me.grpManual.TabIndex = 51
        Me.grpManual.Visible = False
        '
        'txtCantidadRegistrada
        '
        Me.txtCantidadRegistrada.BackColor = System.Drawing.Color.Red
        Me.txtCantidadRegistrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadRegistrada.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadRegistrada.LinkColor = System.Drawing.Color.LightGray
        Me.txtCantidadRegistrada.Location = New System.Drawing.Point(57, 44)
        Me.txtCantidadRegistrada.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtCantidadRegistrada.Name = "txtCantidadRegistrada"
        Me.txtCantidadRegistrada.Size = New System.Drawing.Size(111, 45)
        Me.txtCantidadRegistrada.TabIndex = 8
        Me.txtCantidadRegistrada.TabStop = True
        Me.txtCantidadRegistrada.Text = "0"
        Me.txtCantidadRegistrada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCantidadEsperada
        '
        Me.txtCantidadEsperada.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCantidadEsperada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadEsperada.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadEsperada.LinkColor = System.Drawing.Color.LightGray
        Me.txtCantidadEsperada.Location = New System.Drawing.Point(196, 45)
        Me.txtCantidadEsperada.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.txtCantidadEsperada.Name = "txtCantidadEsperada"
        Me.txtCantidadEsperada.Size = New System.Drawing.Size(111, 45)
        Me.txtCantidadEsperada.TabIndex = 44
        Me.txtCantidadEsperada.TabStop = True
        Me.txtCantidadEsperada.Text = "0"
        Me.txtCantidadEsperada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.GroupControl1.AppearanceCaption.Options.UseBackColor = True
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.txtReferenciaSAP)
        Me.GroupControl1.Controls.Add(Label5)
        Me.GroupControl1.Controls.Add(Me.txtCliente)
        Me.GroupControl1.Controls.Add(Label3)
        Me.GroupControl1.Controls.Add(Me.txtPedido)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Location = New System.Drawing.Point(62, 41)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(573, 170)
        Me.GroupControl1.TabIndex = 52
        Me.GroupControl1.Text = "Información del pedido"
        '
        'txtReferenciaSAP
        '
        Me.txtReferenciaSAP.Location = New System.Drawing.Point(104, 118)
        Me.txtReferenciaSAP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReferenciaSAP.Name = "txtReferenciaSAP"
        Me.txtReferenciaSAP.Properties.Appearance.BackColor = System.Drawing.Color.Linen
        Me.txtReferenciaSAP.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReferenciaSAP.Properties.Appearance.Options.UseBackColor = True
        Me.txtReferenciaSAP.Properties.Appearance.Options.UseFont = True
        Me.txtReferenciaSAP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtReferenciaSAP.Properties.MaxLength = 50
        Me.txtReferenciaSAP.Size = New System.Drawing.Size(385, 28)
        Me.txtReferenciaSAP.TabIndex = 46
        '
        'txtCliente
        '
        Me.txtCliente.Location = New System.Drawing.Point(104, 80)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Properties.Appearance.BackColor = System.Drawing.Color.Linen
        Me.txtCliente.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Properties.Appearance.Options.UseBackColor = True
        Me.txtCliente.Properties.Appearance.Options.UseFont = True
        Me.txtCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCliente.Properties.MaxLength = 50
        Me.txtCliente.Size = New System.Drawing.Size(385, 28)
        Me.txtCliente.TabIndex = 44
        '
        'txtPedido
        '
        Me.txtPedido.Location = New System.Drawing.Point(104, 42)
        Me.txtPedido.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPedido.Name = "txtPedido"
        Me.txtPedido.Properties.Appearance.BackColor = System.Drawing.Color.Linen
        Me.txtPedido.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPedido.Properties.Appearance.Options.UseBackColor = True
        Me.txtPedido.Properties.Appearance.Options.UseFont = True
        Me.txtPedido.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtPedido.Properties.MaxLength = 50
        Me.txtPedido.Size = New System.Drawing.Size(385, 28)
        Me.txtPedido.TabIndex = 42
        '
        'grpScan
        '
        Me.grpScan.AppearanceCaption.BackColor = System.Drawing.Color.SteelBlue
        Me.grpScan.AppearanceCaption.Options.UseBackColor = True
        Me.grpScan.AppearanceCaption.Options.UseTextOptions = True
        Me.grpScan.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.grpScan.Controls.Add(Label1)
        Me.grpScan.Controls.Add(Me.txtScanner)
        Me.grpScan.Controls.Add(Me.txtCantidad)
        Me.grpScan.Controls.Add(lblScan)
        Me.grpScan.Location = New System.Drawing.Point(103, 41)
        Me.grpScan.Name = "grpScan"
        Me.grpScan.Size = New System.Drawing.Size(408, 113)
        Me.grpScan.TabIndex = 50
        Me.grpScan.Visible = False
        '
        'txtScanner
        '
        Me.txtScanner.Location = New System.Drawing.Point(109, 59)
        Me.txtScanner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtScanner.Name = "txtScanner"
        Me.txtScanner.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanner.Properties.Appearance.Options.UseFont = True
        Me.txtScanner.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtScanner.Properties.MaxLength = 50
        Me.txtScanner.Size = New System.Drawing.Size(380, 28)
        Me.txtScanner.TabIndex = 42
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(497, 59)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Properties.Appearance.Options.UseFont = True
        Me.txtCantidad.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCantidad.Properties.MaxLength = 50
        Me.txtCantidad.Size = New System.Drawing.Size(119, 28)
        Me.txtCantidad.TabIndex = 48
        '
        'GrpFactura
        '
        Me.GrpFactura.Controls.Add(Me.GridControl1)
        Me.GrpFactura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpFactura.Location = New System.Drawing.Point(0, 273)
        Me.GrpFactura.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpFactura.Name = "GrpFactura"
        Me.GrpFactura.Size = New System.Drawing.Size(1220, 677)
        Me.GrpFactura.TabIndex = 44
        Me.GrpFactura.Text = "Registros"
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(2, 28)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.MenuManager = Me.RibbonControl
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1216, 647)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'frmManufacturaPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 980)
        Me.Controls.Add(Me.GrpFactura)
        Me.Controls.Add(Me.GrpAsignacionTransaccion)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmManufacturaPicking"
        Me.Ribbon = Me.RibbonControl
        Me.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Hidden
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Detalle de manufactura"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpAsignacionTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAsignacionTransaccion.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtDescripcionProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarraProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpManual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpManual.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtReferenciaSAP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScan.ResumeLayout(False)
        Me.grpScan.PerformLayout()
        CType(Me.txtScanner.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpFactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpFactura.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GrpAsignacionTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpFactura As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtScanner As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCantidadRegistrada As LinkLabel
    Friend WithEvents txtCantidadEsperada As LinkLabel
    Friend WithEvents grpScan As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grpManual As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCantidad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDescripcionProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBarraProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigoProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtReferenciaSAP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCliente As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPedido As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarButtonItem
End Class