<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresionRecepcion
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
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim lblIdArea As System.Windows.Forms.Label
        Dim lblCodigoArea As System.Windows.Forms.Label
        Dim lblDescripcionAreaBodega As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresionRecepcion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.cmdPrintLicencia = New System.Windows.Forms.Button()
        Me.cmdPrintBarra = New System.Windows.Forms.Button()
        Me.txtTiempoActualizacionP = New System.Windows.Forms.NumericUpDown()
        Me.XtraScrollableControl = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdImpresionLicencia = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdImpresionBarra = New DevExpress.XtraEditors.SimpleButton()
        Me.cmbPrinterLicencia = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPrinterBarra = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtLicencia = New DevExpress.XtraEditors.TextEdit()
        Me.txtBarra = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantidadLicencias = New System.Windows.Forms.NumericUpDown()
        Me.txtCantidadBarras = New System.Windows.Forms.NumericUpDown()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        lblIdArea = New System.Windows.Forms.Label()
        lblCodigoArea = New System.Windows.Forms.Label()
        lblDescripcionAreaBodega = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraScrollableControl.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbPrinterLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPrinterBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadLicencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadBarras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(13, 57)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(136, 16)
        Label5.TabIndex = 9
        Label5.Text = "Cantidad Impresiones:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(48, 226)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(81, 21)
        Label6.TabIndex = 4
        Label6.Text = "Producto:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(48, 287)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(69, 21)
        Label1.TabIndex = 18
        Label1.Text = "Licencia"
        '
        'lblIdArea
        '
        lblIdArea.AutoSize = True
        lblIdArea.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblIdArea.Location = New System.Drawing.Point(48, 55)
        lblIdArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblIdArea.Name = "lblIdArea"
        lblIdArea.Size = New System.Drawing.Size(66, 21)
        lblIdArea.TabIndex = 24
        lblIdArea.Text = "Código:"
        '
        'lblCodigoArea
        '
        lblCodigoArea.AutoSize = True
        lblCodigoArea.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblCodigoArea.Location = New System.Drawing.Point(48, 88)
        lblCodigoArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoArea.Name = "lblCodigoArea"
        lblCodigoArea.Size = New System.Drawing.Size(102, 21)
        lblCodigoArea.TabIndex = 20
        lblCodigoArea.Text = "Descripción:"
        '
        'lblDescripcionAreaBodega
        '
        lblDescripcionAreaBodega.AutoSize = True
        lblDescripcionAreaBodega.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblDescripcionAreaBodega.Location = New System.Drawing.Point(48, 121)
        lblDescripcionAreaBodega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblDescripcionAreaBodega.Name = "lblDescripcionAreaBodega"
        lblDescripcionAreaBodega.Size = New System.Drawing.Size(48, 21)
        lblDescripcionAreaBodega.TabIndex = 22
        lblDescripcionAreaBodega.Text = "Lote:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(48, 154)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(75, 21)
        Label2.TabIndex = 26
        Label2.Text = "Licencia:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(150, 200)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(81, 21)
        Label3.TabIndex = 30
        Label3.Text = "Cantidad:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(239, 200)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(92, 21)
        Label4.TabIndex = 33
        Label4.Text = "Impresora:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 1
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(637, 40)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 512)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(637, 30)
        '
        'cmdPrintLicencia
        '
        Me.cmdPrintLicencia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrintLicencia.Location = New System.Drawing.Point(364, 48)
        Me.cmdPrintLicencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmdPrintLicencia.Name = "cmdPrintLicencia"
        Me.cmdPrintLicencia.Size = New System.Drawing.Size(170, 35)
        Me.cmdPrintLicencia.TabIndex = 6
        Me.cmdPrintLicencia.Text = "Impresión Licencia"
        Me.cmdPrintLicencia.UseVisualStyleBackColor = True
        '
        'cmdPrintBarra
        '
        Me.cmdPrintBarra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrintBarra.Location = New System.Drawing.Point(603, 48)
        Me.cmdPrintBarra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmdPrintBarra.Name = "cmdPrintBarra"
        Me.cmdPrintBarra.Size = New System.Drawing.Size(170, 35)
        Me.cmdPrintBarra.TabIndex = 7
        Me.cmdPrintBarra.Text = "Impresion barra"
        Me.cmdPrintBarra.UseVisualStyleBackColor = True
        '
        'txtTiempoActualizacionP
        '
        Me.txtTiempoActualizacionP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTiempoActualizacionP.Location = New System.Drawing.Point(166, 55)
        Me.txtTiempoActualizacionP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTiempoActualizacionP.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtTiempoActualizacionP.Name = "txtTiempoActualizacionP"
        Me.txtTiempoActualizacionP.Size = New System.Drawing.Size(96, 22)
        Me.txtTiempoActualizacionP.TabIndex = 8
        Me.txtTiempoActualizacionP.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'XtraScrollableControl
        '
        Me.XtraScrollableControl.Controls.Add(Me.GroupControl4)
        Me.XtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl.Location = New System.Drawing.Point(0, 40)
        Me.XtraScrollableControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.XtraScrollableControl.Name = "XtraScrollableControl"
        Me.XtraScrollableControl.Size = New System.Drawing.Size(637, 472)
        Me.XtraScrollableControl.TabIndex = 5
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Label4)
        Me.GroupControl4.Controls.Add(Me.cmdImpresionLicencia)
        Me.GroupControl4.Controls.Add(Me.cmdImpresionBarra)
        Me.GroupControl4.Controls.Add(Label3)
        Me.GroupControl4.Controls.Add(Me.cmbPrinterLicencia)
        Me.GroupControl4.Controls.Add(Me.cmbPrinterBarra)
        Me.GroupControl4.Controls.Add(Label2)
        Me.GroupControl4.Controls.Add(Me.txtLicencia)
        Me.GroupControl4.Controls.Add(lblIdArea)
        Me.GroupControl4.Controls.Add(Me.txtBarra)
        Me.GroupControl4.Controls.Add(lblCodigoArea)
        Me.GroupControl4.Controls.Add(Me.txtDescripcion)
        Me.GroupControl4.Controls.Add(lblDescripcionAreaBodega)
        Me.GroupControl4.Controls.Add(Me.txtLote)
        Me.GroupControl4.Controls.Add(Me.txtCantidadLicencias)
        Me.GroupControl4.Controls.Add(Label1)
        Me.GroupControl4.Controls.Add(Me.txtCantidadBarras)
        Me.GroupControl4.Controls.Add(Label6)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(637, 472)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Datos para impresión"
        '
        'cmdImpresionLicencia
        '
        Me.cmdImpresionLicencia.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresionLicencia.Appearance.Options.UseFont = True
        Me.cmdImpresionLicencia.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresionLicencia.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresionLicencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresionLicencia.Location = New System.Drawing.Point(436, 355)
        Me.cmdImpresionLicencia.Name = "cmdImpresionLicencia"
        Me.cmdImpresionLicencia.Size = New System.Drawing.Size(163, 73)
        Me.cmdImpresionLicencia.TabIndex = 32
        Me.cmdImpresionLicencia.Text = "&Licencia"
        '
        'cmdImpresionBarra
        '
        Me.cmdImpresionBarra.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresionBarra.Appearance.Options.UseFont = True
        Me.cmdImpresionBarra.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresionBarra.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresionBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresionBarra.Location = New System.Drawing.Point(154, 355)
        Me.cmdImpresionBarra.Name = "cmdImpresionBarra"
        Me.cmdImpresionBarra.Size = New System.Drawing.Size(163, 73)
        Me.cmdImpresionBarra.TabIndex = 31
        Me.cmdImpresionBarra.Text = "&Producto"
        '
        'cmbPrinterLicencia
        '
        Me.cmbPrinterLicencia.Location = New System.Drawing.Point(243, 288)
        Me.cmbPrinterLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPrinterLicencia.MenuManager = Me.RibbonControl
        Me.cmbPrinterLicencia.Name = "cmbPrinterLicencia"
        Me.cmbPrinterLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterLicencia.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterLicencia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterLicencia.Properties.NullText = ""
        Me.cmbPrinterLicencia.Size = New System.Drawing.Size(345, 28)
        Me.cmbPrinterLicencia.TabIndex = 29
        '
        'cmbPrinterBarra
        '
        Me.cmbPrinterBarra.Location = New System.Drawing.Point(243, 227)
        Me.cmbPrinterBarra.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPrinterBarra.MenuManager = Me.RibbonControl
        Me.cmbPrinterBarra.Name = "cmbPrinterBarra"
        Me.cmbPrinterBarra.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterBarra.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterBarra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterBarra.Properties.NullText = ""
        Me.cmbPrinterBarra.Size = New System.Drawing.Size(345, 28)
        Me.cmbPrinterBarra.TabIndex = 28
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(154, 151)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicencia.Properties.Appearance.Options.UseFont = True
        Me.txtLicencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLicencia.Properties.MaxLength = 50
        Me.txtLicencia.Size = New System.Drawing.Size(434, 28)
        Me.txtLicencia.TabIndex = 27
        '
        'txtBarra
        '
        Me.txtBarra.Location = New System.Drawing.Point(154, 49)
        Me.txtBarra.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBarra.Name = "txtBarra"
        Me.txtBarra.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarra.Properties.Appearance.Options.UseFont = True
        Me.txtBarra.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtBarra.Properties.MaxLength = 50
        Me.txtBarra.Properties.ReadOnly = True
        Me.txtBarra.Size = New System.Drawing.Size(434, 28)
        Me.txtBarra.TabIndex = 25
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(154, 84)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcion.Properties.MaxLength = 50
        Me.txtDescripcion.Size = New System.Drawing.Size(434, 28)
        Me.txtDescripcion.TabIndex = 21
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(154, 118)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLote.Properties.MaxLength = 50
        Me.txtLote.Size = New System.Drawing.Size(434, 28)
        Me.txtLote.TabIndex = 23
        '
        'txtCantidadLicencias
        '
        Me.txtCantidadLicencias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadLicencias.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadLicencias.Location = New System.Drawing.Point(154, 287)
        Me.txtCantidadLicencias.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidadLicencias.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadLicencias.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadLicencias.Name = "txtCantidadLicencias"
        Me.txtCantidadLicencias.Size = New System.Drawing.Size(82, 28)
        Me.txtCantidadLicencias.TabIndex = 19
        Me.txtCantidadLicencias.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCantidadBarras
        '
        Me.txtCantidadBarras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadBarras.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadBarras.Location = New System.Drawing.Point(154, 226)
        Me.txtCantidadBarras.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidadBarras.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadBarras.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadBarras.Name = "txtCantidadBarras"
        Me.txtCantidadBarras.Size = New System.Drawing.Size(82, 28)
        Me.txtCantidadBarras.TabIndex = 5
        Me.txtCantidadBarras.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'frmImpresionRecepcion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 542)
        Me.Controls.Add(Me.XtraScrollableControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Name = "frmImpresionRecepcion"
        Me.Ribbon = Me.RibbonControl
        Me.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Hidden
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresión BOF"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraScrollableControl.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbPrinterLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPrinterBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadLicencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadBarras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdPrintBarra As Button
    Friend WithEvents cmdPrintLicencia As Button
    Friend WithEvents txtTiempoActualizacionP As NumericUpDown
    Friend WithEvents XtraScrollableControl As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCantidadBarras As NumericUpDown
    Friend WithEvents txtCantidadLicencias As NumericUpDown
    Friend WithEvents txtBarra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLicencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbPrinterLicencia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPrinterBarra As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdImpresionLicencia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdImpresionBarra As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
End Class