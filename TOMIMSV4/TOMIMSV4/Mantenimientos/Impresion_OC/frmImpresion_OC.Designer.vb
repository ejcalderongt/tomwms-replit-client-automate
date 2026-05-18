<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImpresionRecepcion_OC
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
        Dim Label5 As System.Windows.Forms.Label
        Dim lblLicenciaCnt As System.Windows.Forms.Label
        Dim lblProducto As System.Windows.Forms.Label
        Dim lblLote As System.Windows.Forms.Label
        Dim lblLicencia As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim lblVencimiento As System.Windows.Forms.Label
        Dim lblPresentacion As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresionRecepcion_OC))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.cmdPrintLicencia = New System.Windows.Forms.Button()
        Me.cmdPrintBarra = New System.Windows.Forms.Button()
        Me.txtTiempoActualizacionP = New System.Windows.Forms.NumericUpDown()
        Me.XtraScrollableControl = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabImpresion = New System.Windows.Forms.TabPage()
        Me.chkLicenciaBulto = New System.Windows.Forms.RadioButton()
        Me.txtCantidadBarras = New System.Windows.Forms.NumericUpDown()
        Me.chkSoloLicencia = New System.Windows.Forms.RadioButton()
        Me.cmdUnidad = New DevExpress.XtraEditors.SimpleButton()
        Me.txtCantidadLicencias = New System.Windows.Forms.NumericUpDown()
        Me.lblUmbasCant = New DevExpress.XtraEditors.LabelControl()
        Me.cmdPrinterUmbas = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtLicencia = New DevExpress.XtraEditors.TextEdit()
        Me.txtCantUmBas = New System.Windows.Forms.NumericUpDown()
        Me.lblProductoPres = New DevExpress.XtraEditors.LabelControl()
        Me.cmbPrinterBarra = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbPrinterLicencia = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtCopias = New System.Windows.Forms.NumericUpDown()
        Me.lblEtiquetas = New DevExpress.XtraEditors.LabelControl()
        Me.cmdImpresionBarra = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblPesoTotal = New DevExpress.XtraEditors.LabelControl()
        Me.txtPesoTotal = New System.Windows.Forms.NumericUpDown()
        Me.lblPesoTarima = New DevExpress.XtraEditors.LabelControl()
        Me.txtPesoTarima = New System.Windows.Forms.NumericUpDown()
        Me.txtCajaPorCama = New System.Windows.Forms.NumericUpDown()
        Me.txtCamaPorTarima = New System.Windows.Forms.NumericUpDown()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtFactor = New DevExpress.XtraEditors.TextEdit()
        Me.cmdImpresionLicencia = New DevExpress.XtraEditors.SimpleButton()
        Me.txtVencimiento = New DevExpress.XtraEditors.TextEdit()
        Me.cmbLote = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbProducto = New DevExpress.XtraEditors.LookUpEdit()
        Me.tabLicencias = New System.Windows.Forms.TabPage()
        Me.dgridBarrasPallet = New DevExpress.XtraGrid.GridControl()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.Cliente_direccionTableAdapter1 = New TOMWMS.cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter()
        Label5 = New System.Windows.Forms.Label()
        lblLicenciaCnt = New System.Windows.Forms.Label()
        lblProducto = New System.Windows.Forms.Label()
        lblLote = New System.Windows.Forms.Label()
        lblLicencia = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        lblVencimiento = New System.Windows.Forms.Label()
        lblPresentacion = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraScrollableControl.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabImpresion.SuspendLayout()
        CType(Me.txtCantidadBarras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadLicencias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdPrinterUmbas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantUmBas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPrinterBarra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPrinterLicencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCopias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtPesoTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoTarima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCajaPorCama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCamaPorTarima, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFactor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVencimiento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLicencias.SuspendLayout()
        CType(Me.dgridBarrasPallet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'lblLicenciaCnt
        '
        lblLicenciaCnt.AutoSize = True
        lblLicenciaCnt.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLicenciaCnt.Location = New System.Drawing.Point(30, 299)
        lblLicenciaCnt.Name = "lblLicenciaCnt"
        lblLicenciaCnt.Size = New System.Drawing.Size(55, 17)
        lblLicenciaCnt.TabIndex = 18
        lblLicenciaCnt.Text = "Licencia"
        '
        'lblProducto
        '
        lblProducto.AutoSize = True
        lblProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblProducto.Location = New System.Drawing.Point(25, 63)
        lblProducto.Name = "lblProducto"
        lblProducto.Size = New System.Drawing.Size(70, 17)
        lblProducto.TabIndex = 24
        lblProducto.Text = "Producto:"
        '
        'lblLote
        '
        lblLote.AutoSize = True
        lblLote.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLote.Location = New System.Drawing.Point(25, 93)
        lblLote.Name = "lblLote"
        lblLote.Size = New System.Drawing.Size(40, 17)
        lblLote.TabIndex = 22
        lblLote.Text = "Lote:"
        '
        'lblLicencia
        '
        lblLicencia.AutoSize = True
        lblLicencia.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLicencia.Location = New System.Drawing.Point(25, 162)
        lblLicencia.Name = "lblLicencia"
        lblLicencia.Size = New System.Drawing.Size(60, 17)
        lblLicencia.TabIndex = 26
        lblLicencia.Text = "Licencia:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(201, 280)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(67, 17)
        Label3.TabIndex = 30
        Label3.Text = "Cantidad:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(278, 280)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(75, 17)
        Label4.TabIndex = 33
        Label4.Text = "Impresora:"
        '
        'lblVencimiento
        '
        lblVencimiento.AutoSize = True
        lblVencimiento.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblVencimiento.Location = New System.Drawing.Point(25, 129)
        lblVencimiento.Name = "lblVencimiento"
        lblVencimiento.Size = New System.Drawing.Size(87, 17)
        lblVencimiento.TabIndex = 37
        lblVencimiento.Text = "Vencimiento:"
        '
        'lblPresentacion
        '
        lblPresentacion.AutoSize = True
        lblPresentacion.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblPresentacion.Location = New System.Drawing.Point(13, 34)
        lblPresentacion.Name = "lblPresentacion"
        lblPresentacion.Size = New System.Drawing.Size(91, 17)
        lblPresentacion.TabIndex = 38
        lblPresentacion.Text = "Presentación:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(13, 64)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(52, 17)
        Label2.TabIndex = 41
        Label2.Text = "Factor:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.Location = New System.Drawing.Point(13, 96)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(111, 17)
        Label7.TabIndex = 42
        Label7.Text = "Camas x Tarima:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(13, 124)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(97, 17)
        Label8.TabIndex = 43
        Label8.Text = "Cajas x Cama:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.Location = New System.Drawing.Point(598, 280)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(53, 17)
        Label9.TabIndex = 45
        Label9.Text = "Copias:"
        '
        'RibbonControl
        '
        Me.RibbonControl.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RibbonControl.MaxItemId = 1
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(964, 32)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 574)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(964, 24)
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
        Me.txtTiempoActualizacionP.Size = New System.Drawing.Size(96, 20)
        Me.txtTiempoActualizacionP.TabIndex = 8
        Me.txtTiempoActualizacionP.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'XtraScrollableControl
        '
        Me.XtraScrollableControl.Controls.Add(Me.GroupControl4)
        Me.XtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl.Location = New System.Drawing.Point(0, 32)
        Me.XtraScrollableControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraScrollableControl.Name = "XtraScrollableControl"
        Me.XtraScrollableControl.Size = New System.Drawing.Size(964, 542)
        Me.XtraScrollableControl.TabIndex = 5
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.TabControl1)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(964, 543)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Datos para impresión"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabImpresion)
        Me.TabControl1.Controls.Add(Me.tabLicencias)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(2, 23)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(768, 414)
        Me.TabControl1.TabIndex = 53
        '
        'tabImpresion
        '
        Me.tabImpresion.Controls.Add(lblProducto)
        Me.tabImpresion.Controls.Add(Me.chkLicenciaBulto)
        Me.tabImpresion.Controls.Add(Me.txtCantidadBarras)
        Me.tabImpresion.Controls.Add(Me.chkSoloLicencia)
        Me.tabImpresion.Controls.Add(lblLicenciaCnt)
        Me.tabImpresion.Controls.Add(Me.cmdUnidad)
        Me.tabImpresion.Controls.Add(Me.txtCantidadLicencias)
        Me.tabImpresion.Controls.Add(Me.lblUmbasCant)
        Me.tabImpresion.Controls.Add(lblLote)
        Me.tabImpresion.Controls.Add(Me.cmdPrinterUmbas)
        Me.tabImpresion.Controls.Add(Me.txtLicencia)
        Me.tabImpresion.Controls.Add(Me.txtCantUmBas)
        Me.tabImpresion.Controls.Add(lblLicencia)
        Me.tabImpresion.Controls.Add(Me.lblProductoPres)
        Me.tabImpresion.Controls.Add(Me.cmbPrinterBarra)
        Me.tabImpresion.Controls.Add(Label9)
        Me.tabImpresion.Controls.Add(Me.cmbPrinterLicencia)
        Me.tabImpresion.Controls.Add(Me.txtCopias)
        Me.tabImpresion.Controls.Add(Label3)
        Me.tabImpresion.Controls.Add(Me.lblEtiquetas)
        Me.tabImpresion.Controls.Add(Me.cmdImpresionBarra)
        Me.tabImpresion.Controls.Add(Me.GroupControl1)
        Me.tabImpresion.Controls.Add(Me.cmdImpresionLicencia)
        Me.tabImpresion.Controls.Add(lblVencimiento)
        Me.tabImpresion.Controls.Add(Label4)
        Me.tabImpresion.Controls.Add(Me.txtVencimiento)
        Me.tabImpresion.Controls.Add(Me.cmbLote)
        Me.tabImpresion.Controls.Add(Me.cmbProducto)
        Me.tabImpresion.Location = New System.Drawing.Point(4, 22)
        Me.tabImpresion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tabImpresion.Name = "tabImpresion"
        Me.tabImpresion.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tabImpresion.Size = New System.Drawing.Size(760, 388)
        Me.tabImpresion.TabIndex = 0
        Me.tabImpresion.Text = "Impresión"
        Me.tabImpresion.UseVisualStyleBackColor = True
        '
        'chkLicenciaBulto
        '
        Me.chkLicenciaBulto.AutoSize = True
        Me.chkLicenciaBulto.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.chkLicenciaBulto.Location = New System.Drawing.Point(205, 240)
        Me.chkLicenciaBulto.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkLicenciaBulto.Name = "chkLicenciaBulto"
        Me.chkLicenciaBulto.Size = New System.Drawing.Size(173, 21)
        Me.chkLicenciaBulto.TabIndex = 52
        Me.chkLicenciaBulto.Text = "Licencias y Presentacion"
        Me.chkLicenciaBulto.UseVisualStyleBackColor = True
        '
        'txtCantidadBarras
        '
        Me.txtCantidadBarras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadBarras.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadBarras.Location = New System.Drawing.Point(205, 328)
        Me.txtCantidadBarras.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidadBarras.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadBarras.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadBarras.Name = "txtCantidadBarras"
        Me.txtCantidadBarras.Size = New System.Drawing.Size(70, 24)
        Me.txtCantidadBarras.TabIndex = 5
        Me.txtCantidadBarras.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkSoloLicencia
        '
        Me.chkSoloLicencia.AutoSize = True
        Me.chkSoloLicencia.Checked = True
        Me.chkSoloLicencia.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.chkSoloLicencia.Location = New System.Drawing.Point(205, 219)
        Me.chkSoloLicencia.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkSoloLicencia.Name = "chkSoloLicencia"
        Me.chkSoloLicencia.Size = New System.Drawing.Size(109, 21)
        Me.chkSoloLicencia.TabIndex = 51
        Me.chkSoloLicencia.TabStop = True
        Me.chkSoloLicencia.Text = "Solo Licencias"
        Me.chkSoloLicencia.UseVisualStyleBackColor = True
        '
        'cmdUnidad
        '
        Me.cmdUnidad.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUnidad.Appearance.Options.UseFont = True
        Me.cmdUnidad.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdUnidad.ImageOptions.SvgImage = CType(resources.GetObject("cmdUnidad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdUnidad.Location = New System.Drawing.Point(494, 388)
        Me.cmdUnidad.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdUnidad.Name = "cmdUnidad"
        Me.cmdUnidad.Size = New System.Drawing.Size(83, 59)
        Me.cmdUnidad.TabIndex = 50
        Me.cmdUnidad.Text = "&Unidad"
        '
        'txtCantidadLicencias
        '
        Me.txtCantidadLicencias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadLicencias.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadLicencias.Location = New System.Drawing.Point(205, 299)
        Me.txtCantidadLicencias.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidadLicencias.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadLicencias.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadLicencias.Name = "txtCantidadLicencias"
        Me.txtCantidadLicencias.Size = New System.Drawing.Size(70, 24)
        Me.txtCantidadLicencias.TabIndex = 19
        Me.txtCantidadLicencias.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblUmbasCant
        '
        Me.lblUmbasCant.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblUmbasCant.Appearance.Options.UseFont = True
        Me.lblUmbasCant.Location = New System.Drawing.Point(33, 361)
        Me.lblUmbasCant.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblUmbasCant.Name = "lblUmbasCant"
        Me.lblUmbasCant.Size = New System.Drawing.Size(109, 17)
        Me.lblUmbasCant.TabIndex = 49
        Me.lblUmbasCant.Text = "Producto UMBAS:"
        '
        'cmdPrinterUmbas
        '
        Me.cmdPrinterUmbas.Location = New System.Drawing.Point(281, 360)
        Me.cmdPrinterUmbas.MenuManager = Me.RibbonControl
        Me.cmdPrinterUmbas.Name = "cmdPrinterUmbas"
        Me.cmdPrinterUmbas.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrinterUmbas.Properties.Appearance.Options.UseFont = True
        Me.cmdPrinterUmbas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmdPrinterUmbas.Properties.NullText = ""
        Me.cmdPrinterUmbas.Size = New System.Drawing.Size(296, 24)
        Me.cmdPrinterUmbas.TabIndex = 48
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(129, 156)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicencia.Properties.Appearance.Options.UseBackColor = True
        Me.txtLicencia.Properties.Appearance.Options.UseFont = True
        Me.txtLicencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLicencia.Properties.MaxLength = 50
        Me.txtLicencia.Size = New System.Drawing.Size(372, 24)
        Me.txtLicencia.TabIndex = 27
        '
        'txtCantUmBas
        '
        Me.txtCantUmBas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantUmBas.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantUmBas.Location = New System.Drawing.Point(205, 359)
        Me.txtCantUmBas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantUmBas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantUmBas.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantUmBas.Name = "txtCantUmBas"
        Me.txtCantUmBas.Size = New System.Drawing.Size(70, 24)
        Me.txtCantUmBas.TabIndex = 47
        Me.txtCantUmBas.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblProductoPres
        '
        Me.lblProductoPres.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblProductoPres.Appearance.Options.UseFont = True
        Me.lblProductoPres.Location = New System.Drawing.Point(33, 330)
        Me.lblProductoPres.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblProductoPres.Name = "lblProductoPres"
        Me.lblProductoPres.Size = New System.Drawing.Size(92, 17)
        Me.lblProductoPres.TabIndex = 46
        Me.lblProductoPres.Text = "Producto Pres:"
        '
        'cmbPrinterBarra
        '
        Me.cmbPrinterBarra.Location = New System.Drawing.Point(281, 329)
        Me.cmbPrinterBarra.MenuManager = Me.RibbonControl
        Me.cmbPrinterBarra.Name = "cmbPrinterBarra"
        Me.cmbPrinterBarra.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterBarra.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterBarra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterBarra.Properties.NullText = ""
        Me.cmbPrinterBarra.Size = New System.Drawing.Size(296, 24)
        Me.cmbPrinterBarra.TabIndex = 28
        '
        'cmbPrinterLicencia
        '
        Me.cmbPrinterLicencia.Location = New System.Drawing.Point(281, 300)
        Me.cmbPrinterLicencia.MenuManager = Me.RibbonControl
        Me.cmbPrinterLicencia.Name = "cmbPrinterLicencia"
        Me.cmbPrinterLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterLicencia.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterLicencia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterLicencia.Properties.NullText = ""
        Me.cmbPrinterLicencia.Size = New System.Drawing.Size(296, 24)
        Me.cmbPrinterLicencia.TabIndex = 29
        '
        'txtCopias
        '
        Me.txtCopias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopias.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopias.Location = New System.Drawing.Point(597, 297)
        Me.txtCopias.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCopias.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCopias.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCopias.Name = "txtCopias"
        Me.txtCopias.Size = New System.Drawing.Size(70, 24)
        Me.txtCopias.TabIndex = 44
        Me.txtCopias.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblEtiquetas
        '
        Me.lblEtiquetas.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblEtiquetas.Appearance.Options.UseFont = True
        Me.lblEtiquetas.Location = New System.Drawing.Point(597, 323)
        Me.lblEtiquetas.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblEtiquetas.Name = "lblEtiquetas"
        Me.lblEtiquetas.Size = New System.Drawing.Size(136, 17)
        Me.lblEtiquetas.TabIndex = 43
        Me.lblEtiquetas.Text = "Etiquetas a imprimir: 0"
        Me.lblEtiquetas.Visible = False
        '
        'cmdImpresionBarra
        '
        Me.cmdImpresionBarra.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresionBarra.Appearance.Options.UseFont = True
        Me.cmdImpresionBarra.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresionBarra.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresionBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresionBarra.Location = New System.Drawing.Point(387, 388)
        Me.cmdImpresionBarra.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdImpresionBarra.Name = "cmdImpresionBarra"
        Me.cmdImpresionBarra.Size = New System.Drawing.Size(83, 59)
        Me.cmdImpresionBarra.TabIndex = 31
        Me.cmdImpresionBarra.Text = "&Fardo"
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.GroupControl1.Appearance.Options.UseBackColor = True
        Me.GroupControl1.Controls.Add(Me.lblPesoTotal)
        Me.GroupControl1.Controls.Add(Me.txtPesoTotal)
        Me.GroupControl1.Controls.Add(Me.lblPesoTarima)
        Me.GroupControl1.Controls.Add(Me.txtPesoTarima)
        Me.GroupControl1.Controls.Add(Me.txtCajaPorCama)
        Me.GroupControl1.Controls.Add(Me.txtCamaPorTarima)
        Me.GroupControl1.Controls.Add(Label8)
        Me.GroupControl1.Controls.Add(Label7)
        Me.GroupControl1.Controls.Add(lblPresentacion)
        Me.GroupControl1.Controls.Add(Label2)
        Me.GroupControl1.Controls.Add(Me.txtPresentacion)
        Me.GroupControl1.Controls.Add(Me.txtFactor)
        Me.GroupControl1.Location = New System.Drawing.Point(544, 21)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(374, 240)
        Me.GroupControl1.TabIndex = 42
        Me.GroupControl1.Text = "Atributos de producto"
        '
        'lblPesoTotal
        '
        Me.lblPesoTotal.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblPesoTotal.Appearance.Options.UseFont = True
        Me.lblPesoTotal.Location = New System.Drawing.Point(18, 185)
        Me.lblPesoTotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblPesoTotal.Name = "lblPesoTotal"
        Me.lblPesoTotal.Size = New System.Drawing.Size(68, 17)
        Me.lblPesoTotal.TabIndex = 50
        Me.lblPesoTotal.Text = "Peso Total:"
        '
        'txtPesoTotal
        '
        Me.txtPesoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPesoTotal.DecimalPlaces = 6
        Me.txtPesoTotal.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoTotal.Location = New System.Drawing.Point(131, 181)
        Me.txtPesoTotal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPesoTotal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoTotal.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtPesoTotal.Name = "txtPesoTotal"
        Me.txtPesoTotal.Size = New System.Drawing.Size(214, 24)
        Me.txtPesoTotal.TabIndex = 49
        Me.txtPesoTotal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblPesoTarima
        '
        Me.lblPesoTarima.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblPesoTarima.Appearance.Options.UseFont = True
        Me.lblPesoTarima.Location = New System.Drawing.Point(18, 153)
        Me.lblPesoTarima.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblPesoTarima.Name = "lblPesoTarima"
        Me.lblPesoTarima.Size = New System.Drawing.Size(86, 17)
        Me.lblPesoTarima.TabIndex = 48
        Me.lblPesoTarima.Text = "Peso x Tarima"
        '
        'txtPesoTarima
        '
        Me.txtPesoTarima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPesoTarima.DecimalPlaces = 6
        Me.txtPesoTarima.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoTarima.Location = New System.Drawing.Point(131, 149)
        Me.txtPesoTarima.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPesoTarima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoTarima.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtPesoTarima.Name = "txtPesoTarima"
        Me.txtPesoTarima.Size = New System.Drawing.Size(214, 24)
        Me.txtPesoTarima.TabIndex = 47
        Me.txtPesoTarima.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCajaPorCama
        '
        Me.txtCajaPorCama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCajaPorCama.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaPorCama.Location = New System.Drawing.Point(131, 118)
        Me.txtCajaPorCama.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCajaPorCama.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCajaPorCama.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCajaPorCama.Name = "txtCajaPorCama"
        Me.txtCajaPorCama.Size = New System.Drawing.Size(214, 24)
        Me.txtCajaPorCama.TabIndex = 45
        Me.txtCajaPorCama.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCamaPorTarima
        '
        Me.txtCamaPorTarima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCamaPorTarima.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCamaPorTarima.Location = New System.Drawing.Point(131, 91)
        Me.txtCamaPorTarima.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCamaPorTarima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCamaPorTarima.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCamaPorTarima.Name = "txtCamaPorTarima"
        Me.txtCamaPorTarima.Size = New System.Drawing.Size(214, 24)
        Me.txtCamaPorTarima.TabIndex = 44
        Me.txtCamaPorTarima.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(131, 32)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPresentacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentacion.Properties.Appearance.Options.UseBackColor = True
        Me.txtPresentacion.Properties.Appearance.Options.UseFont = True
        Me.txtPresentacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtPresentacion.Properties.MaxLength = 50
        Me.txtPresentacion.Size = New System.Drawing.Size(214, 24)
        Me.txtPresentacion.TabIndex = 39
        '
        'txtFactor
        '
        Me.txtFactor.Location = New System.Drawing.Point(131, 60)
        Me.txtFactor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFactor.Name = "txtFactor"
        Me.txtFactor.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtFactor.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFactor.Properties.Appearance.Options.UseBackColor = True
        Me.txtFactor.Properties.Appearance.Options.UseFont = True
        Me.txtFactor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtFactor.Properties.MaxLength = 50
        Me.txtFactor.Size = New System.Drawing.Size(214, 24)
        Me.txtFactor.TabIndex = 40
        '
        'cmdImpresionLicencia
        '
        Me.cmdImpresionLicencia.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresionLicencia.Appearance.Options.UseFont = True
        Me.cmdImpresionLicencia.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresionLicencia.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresionLicencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresionLicencia.Location = New System.Drawing.Point(281, 388)
        Me.cmdImpresionLicencia.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdImpresionLicencia.Name = "cmdImpresionLicencia"
        Me.cmdImpresionLicencia.Size = New System.Drawing.Size(87, 59)
        Me.cmdImpresionLicencia.TabIndex = 32
        Me.cmdImpresionLicencia.Text = "&Licencia"
        '
        'txtVencimiento
        '
        Me.txtVencimiento.Location = New System.Drawing.Point(129, 124)
        Me.txtVencimiento.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVencimiento.Name = "txtVencimiento"
        Me.txtVencimiento.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVencimiento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVencimiento.Properties.Appearance.Options.UseBackColor = True
        Me.txtVencimiento.Properties.Appearance.Options.UseFont = True
        Me.txtVencimiento.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtVencimiento.Properties.MaxLength = 50
        Me.txtVencimiento.Size = New System.Drawing.Size(372, 24)
        Me.txtVencimiento.TabIndex = 36
        '
        'cmbLote
        '
        Me.cmbLote.Location = New System.Drawing.Point(129, 91)
        Me.cmbLote.MenuManager = Me.RibbonControl
        Me.cmbLote.Name = "cmbLote"
        Me.cmbLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLote.Properties.Appearance.Options.UseFont = True
        Me.cmbLote.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLote.Properties.NullText = ""
        Me.cmbLote.Size = New System.Drawing.Size(372, 24)
        Me.cmbLote.TabIndex = 34
        '
        'cmbProducto
        '
        Me.cmbProducto.Location = New System.Drawing.Point(129, 60)
        Me.cmbProducto.MenuManager = Me.RibbonControl
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.Properties.Appearance.Options.UseFont = True
        Me.cmbProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProducto.Properties.NullText = ""
        Me.cmbProducto.Size = New System.Drawing.Size(372, 24)
        Me.cmbProducto.TabIndex = 35
        '
        'tabLicencias
        '
        Me.tabLicencias.Controls.Add(Me.dgridBarrasPallet)
        Me.tabLicencias.Location = New System.Drawing.Point(4, 22)
        Me.tabLicencias.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tabLicencias.Name = "tabLicencias"
        Me.tabLicencias.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tabLicencias.Size = New System.Drawing.Size(952, 491)
        Me.tabLicencias.TabIndex = 1
        Me.tabLicencias.Text = "Licencias"
        Me.tabLicencias.UseVisualStyleBackColor = True
        '
        'dgridBarrasPallet
        '
        Me.dgridBarrasPallet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridBarrasPallet.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridBarrasPallet.Location = New System.Drawing.Point(3, 2)
        Me.dgridBarrasPallet.MainView = Me.GridView5
        Me.dgridBarrasPallet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgridBarrasPallet.Name = "dgridBarrasPallet"
        Me.dgridBarrasPallet.Size = New System.Drawing.Size(946, 487)
        Me.dgridBarrasPallet.TabIndex = 1
        Me.dgridBarrasPallet.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView5})
        '
        'GridView5
        '
        Me.GridView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.DetailHeight = 355
        Me.GridView5.GridControl = Me.dgridBarrasPallet
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsBehavior.ReadOnly = True
        Me.GridView5.OptionsEditForm.PopupEditFormWidth = 686
        Me.GridView5.OptionsFind.AlwaysVisible = True
        Me.GridView5.OptionsView.ShowFooter = True
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'Cliente_direccionTableAdapter1
        '
        Me.Cliente_direccionTableAdapter1.ClearBeforeFill = True
        '
        'frmImpresionRecepcion_OC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 598)
        Me.Controls.Add(Me.XtraScrollableControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmImpresionRecepcion_OC"
        Me.Ribbon = Me.RibbonControl
        Me.RibbonVisibility = DevExpress.XtraBars.Ribbon.RibbonVisibility.Hidden
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresión BOF OC"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTiempoActualizacionP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraScrollableControl.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabImpresion.ResumeLayout(False)
        Me.tabImpresion.PerformLayout()
        CType(Me.txtCantidadBarras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadLicencias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdPrinterUmbas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantUmBas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPrinterBarra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPrinterLicencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCopias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtPesoTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPesoTarima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCajaPorCama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCamaPorTarima, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFactor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVencimiento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLicencias.ResumeLayout(False)
        CType(Me.dgridBarrasPallet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtLicencia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmbPrinterLicencia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPrinterBarra As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmdImpresionLicencia As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdImpresionBarra As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents cmbLote As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbProducto As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtVencimiento As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtFactor As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCajaPorCama As NumericUpDown
    Friend WithEvents txtCamaPorTarima As NumericUpDown
    Friend WithEvents lblEtiquetas As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCopias As NumericUpDown
    Friend WithEvents lblProductoPres As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUmbasCant As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdPrinterUmbas As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtCantUmBas As NumericUpDown
    Friend WithEvents cmdUnidad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkLicenciaBulto As RadioButton
    Friend WithEvents chkSoloLicencia As RadioButton
    Friend WithEvents txtPesoTarima As NumericUpDown
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabImpresion As TabPage
    Friend WithEvents tabLicencias As TabPage
    Friend WithEvents Cliente_direccionTableAdapter1 As cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents dgridBarrasPallet As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblPesoTarima As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPesoTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPesoTotal As NumericUpDown
End Class