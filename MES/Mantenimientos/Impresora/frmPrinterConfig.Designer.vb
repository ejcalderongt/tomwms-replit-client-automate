<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrinterConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrinterConfig))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdTest = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblImpresora = New DevExpress.XtraEditors.LabelControl()
        Me.txtIp = New DevExpress.XtraEditors.TextEdit()
        Me.lblIP = New DevExpress.XtraEditors.LabelControl()
        Me.txtNombreImpresora = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.chkPredeterminada = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lblCorrelativo = New DevExpress.XtraEditors.LabelControl()
        Me.cmbConexionImpresora = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.cmbReintentos = New DevExpress.XtraEditors.SpinEdit()
        Me.cmbVelocidad = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbFormatoImpresion = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreImpresora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPredeterminada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbConexionImpresora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbReintentos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVelocidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFormatoImpresion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdTest, Me.BarButtonItem2, Me.cmdGuardar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1095, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdTest
        '
        Me.cmdTest.Caption = "Test comunicacion"
        Me.cmdTest.Id = 3
        Me.cmdTest.ImageOptions.SvgImage = CType(resources.GetObject("cmdTest.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdTest.Name = "cmdTest"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 4
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 5
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Impresora"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.cmdTest)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 828)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1095, 30)
        '
        'lblImpresora
        '
        Me.lblImpresora.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImpresora.Appearance.Options.UseFont = True
        Me.lblImpresora.Location = New System.Drawing.Point(17, 100)
        Me.lblImpresora.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblImpresora.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblImpresora.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblImpresora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImpresora.Name = "lblImpresora"
        Me.lblImpresora.Size = New System.Drawing.Size(120, 29)
        Me.lblImpresora.TabIndex = 40
        Me.lblImpresora.Text = "Impresora:"
        '
        'txtIp
        '
        Me.txtIp.EditValue = ""
        Me.txtIp.Location = New System.Drawing.Point(295, 207)
        Me.txtIp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIp.MenuManager = Me.RibbonControl
        Me.txtIp.Name = "txtIp"
        Me.txtIp.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIp.Properties.Appearance.Options.UseFont = True
        Me.txtIp.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtIp.Size = New System.Drawing.Size(388, 38)
        Me.txtIp.TabIndex = 3
        '
        'lblIP
        '
        Me.lblIP.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIP.Appearance.Options.UseFont = True
        Me.lblIP.Location = New System.Drawing.Point(17, 216)
        Me.lblIP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(30, 29)
        Me.lblIP.TabIndex = 39
        Me.lblIP.Text = "IP:"
        '
        'txtNombreImpresora
        '
        Me.txtNombreImpresora.EditValue = ""
        Me.txtNombreImpresora.Location = New System.Drawing.Point(294, 91)
        Me.txtNombreImpresora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreImpresora.MenuManager = Me.RibbonControl
        Me.txtNombreImpresora.Name = "txtNombreImpresora"
        Me.txtNombreImpresora.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreImpresora.Properties.Appearance.Options.UseFont = True
        Me.txtNombreImpresora.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtNombreImpresora.Size = New System.Drawing.Size(388, 38)
        Me.txtNombreImpresora.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(18, 507)
        Me.LabelControl1.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(187, 29)
        Me.LabelControl1.TabIndex = 51
        Me.LabelControl1.Text = "Predeterminada:"
        '
        'chkPredeterminada
        '
        Me.chkPredeterminada.EditValue = True
        Me.chkPredeterminada.Location = New System.Drawing.Point(296, 512)
        Me.chkPredeterminada.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPredeterminada.Name = "chkPredeterminada"
        Me.chkPredeterminada.Properties.Caption = ""
        Me.chkPredeterminada.Size = New System.Drawing.Size(124, 24)
        Me.chkPredeterminada.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(18, 571)
        Me.LabelControl2.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(77, 29)
        Me.LabelControl2.TabIndex = 53
        Me.LabelControl2.Text = "Activa:"
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Location = New System.Drawing.Point(296, 567)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(124, 24)
        Me.chkActivo.TabIndex = 5
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(699, 93)
        Me.LabelControl3.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(87, 20)
        Me.LabelControl3.TabIndex = 54
        Me.LabelControl3.Text = "*Obligatorio"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(410, 571)
        Me.LabelControl4.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl4.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(87, 20)
        Me.LabelControl4.TabIndex = 55
        Me.LabelControl4.Text = "*Obligatorio"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(17, 48)
        Me.LabelControl5.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl5.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(129, 29)
        Me.LabelControl5.TabIndex = 56
        Me.LabelControl5.Text = "Correlativo:"
        '
        'lblCorrelativo
        '
        Me.lblCorrelativo.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCorrelativo.Appearance.Options.UseFont = True
        Me.lblCorrelativo.Location = New System.Drawing.Point(294, 48)
        Me.lblCorrelativo.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.lblCorrelativo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.lblCorrelativo.LookAndFeel.UseDefaultLookAndFeel = False
        Me.lblCorrelativo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblCorrelativo.Name = "lblCorrelativo"
        Me.lblCorrelativo.Size = New System.Drawing.Size(14, 29)
        Me.lblCorrelativo.TabIndex = 57
        Me.lblCorrelativo.Text = "0"
        '
        'cmbConexionImpresora
        '
        Me.cmbConexionImpresora.Location = New System.Drawing.Point(294, 150)
        Me.cmbConexionImpresora.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbConexionImpresora.MenuManager = Me.RibbonControl
        Me.cmbConexionImpresora.Name = "cmbConexionImpresora"
        Me.cmbConexionImpresora.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbConexionImpresora.Properties.Appearance.Options.UseFont = True
        Me.cmbConexionImpresora.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbConexionImpresora.Properties.NullText = ""
        Me.cmbConexionImpresora.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbConexionImpresora.Properties.PopupWidth = 100
        Me.cmbConexionImpresora.Size = New System.Drawing.Size(388, 36)
        Me.cmbConexionImpresora.TabIndex = 2
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(17, 157)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(113, 29)
        Me.LabelControl6.TabIndex = 61
        Me.LabelControl6.Text = "Conexión:"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Location = New System.Drawing.Point(699, 166)
        Me.LabelControl7.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl7.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(87, 20)
        Me.LabelControl7.TabIndex = 62
        Me.LabelControl7.Text = "*Obligatorio"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl11)
        Me.GroupControl1.Controls.Add(Me.LabelControl12)
        Me.GroupControl1.Controls.Add(Me.cmbFormatoImpresion)
        Me.GroupControl1.Controls.Add(Me.cmbDelay)
        Me.GroupControl1.Controls.Add(Me.cmbReintentos)
        Me.GroupControl1.Controls.Add(Me.cmbVelocidad)
        Me.GroupControl1.Controls.Add(Me.LabelControl10)
        Me.GroupControl1.Controls.Add(Me.LabelControl9)
        Me.GroupControl1.Controls.Add(Me.LabelControl8)
        Me.GroupControl1.Controls.Add(Me.LabelControl5)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.lblIP)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Controls.Add(Me.txtIp)
        Me.GroupControl1.Controls.Add(Me.cmbConexionImpresora)
        Me.GroupControl1.Controls.Add(Me.lblImpresora)
        Me.GroupControl1.Controls.Add(Me.lblCorrelativo)
        Me.GroupControl1.Controls.Add(Me.txtNombreImpresora)
        Me.GroupControl1.Controls.Add(Me.chkPredeterminada)
        Me.GroupControl1.Controls.Add(Me.LabelControl4)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1095, 635)
        Me.GroupControl1.TabIndex = 65
        Me.GroupControl1.Text = "GroupControl1"
        '
        'cmbDelay
        '
        Me.cmbDelay.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.cmbDelay.Location = New System.Drawing.Point(295, 447)
        Me.cmbDelay.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbDelay.Name = "cmbDelay"
        Me.cmbDelay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cmbDelay.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDelay.Properties.Appearance.Options.UseBackColor = True
        Me.cmbDelay.Properties.Appearance.Options.UseFont = True
        Me.cmbDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDelay.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.cmbDelay.Properties.IsFloatValue = False
        Me.cmbDelay.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.cmbDelay.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.cmbDelay.Properties.MaskSettings.Set("mask", "d")
        Me.cmbDelay.Size = New System.Drawing.Size(125, 36)
        Me.cmbDelay.TabIndex = 71
        '
        'cmbReintentos
        '
        Me.cmbReintentos.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.cmbReintentos.Location = New System.Drawing.Point(296, 387)
        Me.cmbReintentos.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbReintentos.Name = "cmbReintentos"
        Me.cmbReintentos.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cmbReintentos.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbReintentos.Properties.Appearance.Options.UseBackColor = True
        Me.cmbReintentos.Properties.Appearance.Options.UseFont = True
        Me.cmbReintentos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbReintentos.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.cmbReintentos.Properties.IsFloatValue = False
        Me.cmbReintentos.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.cmbReintentos.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.cmbReintentos.Properties.MaskSettings.Set("mask", "d")
        Me.cmbReintentos.Size = New System.Drawing.Size(124, 36)
        Me.cmbReintentos.TabIndex = 70
        '
        'cmbVelocidad
        '
        Me.cmbVelocidad.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.cmbVelocidad.Location = New System.Drawing.Point(296, 329)
        Me.cmbVelocidad.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbVelocidad.Name = "cmbVelocidad"
        Me.cmbVelocidad.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cmbVelocidad.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbVelocidad.Properties.Appearance.Options.UseBackColor = True
        Me.cmbVelocidad.Properties.Appearance.Options.UseFont = True
        Me.cmbVelocidad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbVelocidad.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.cmbVelocidad.Properties.IsFloatValue = False
        Me.cmbVelocidad.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.cmbVelocidad.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.cmbVelocidad.Properties.MaskSettings.Set("mask", "d")
        Me.cmbVelocidad.Size = New System.Drawing.Size(125, 36)
        Me.cmbVelocidad.TabIndex = 69
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Location = New System.Drawing.Point(18, 454)
        Me.LabelControl10.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl10.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl10.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(72, 29)
        Me.LabelControl10.TabIndex = 68
        Me.LabelControl10.Text = "Delay:"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Appearance.Options.UseFont = True
        Me.LabelControl9.Location = New System.Drawing.Point(18, 394)
        Me.LabelControl9.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl9.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(128, 29)
        Me.LabelControl9.TabIndex = 66
        Me.LabelControl9.Text = "Reintentos:"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Location = New System.Drawing.Point(18, 332)
        Me.LabelControl8.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl8.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(119, 29)
        Me.LabelControl8.TabIndex = 65
        Me.LabelControl8.Text = "Velocidad:"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Location = New System.Drawing.Point(700, 285)
        Me.LabelControl11.LookAndFeel.SkinName = "DevExpress Dark Style"
        Me.LabelControl11.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.LabelControl11.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(87, 20)
        Me.LabelControl11.TabIndex = 74
        Me.LabelControl11.Text = "*Obligatorio"
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Location = New System.Drawing.Point(18, 276)
        Me.LabelControl12.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(219, 29)
        Me.LabelControl12.TabIndex = 73
        Me.LabelControl12.Text = "Formato Impresión:"
        '
        'cmbFormatoImpresion
        '
        Me.cmbFormatoImpresion.Location = New System.Drawing.Point(295, 269)
        Me.cmbFormatoImpresion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbFormatoImpresion.MenuManager = Me.RibbonControl
        Me.cmbFormatoImpresion.Name = "cmbFormatoImpresion"
        Me.cmbFormatoImpresion.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFormatoImpresion.Properties.Appearance.Options.UseFont = True
        Me.cmbFormatoImpresion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbFormatoImpresion.Properties.NullText = ""
        Me.cmbFormatoImpresion.Properties.PopupFormMinSize = New System.Drawing.Size(600, 200)
        Me.cmbFormatoImpresion.Properties.PopupWidth = 100
        Me.cmbFormatoImpresion.Size = New System.Drawing.Size(388, 36)
        Me.cmbFormatoImpresion.TabIndex = 72
        '
        'frmPrinterConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1095, 858)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmPrinterConfig"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresora"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreImpresora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPredeterminada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbConexionImpresora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbReintentos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVelocidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFormatoImpresion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents lblImpresora As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtIp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblIP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNombreImpresora As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdTest As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkPredeterminada As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCorrelativo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbConexionImpresora As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDelay As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cmbReintentos As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cmbVelocidad As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbFormatoImpresion As DevExpress.XtraEditors.LookUpEdit
End Class
