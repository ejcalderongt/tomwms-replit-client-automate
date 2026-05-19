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
        Me.tabImp = New System.Windows.Forms.TabControl()
        Me.tabImpresion = New System.Windows.Forms.TabPage()
        Me.txtIdPallet = New DevExpress.XtraEditors.TextEdit()
        Me.lblIdPallet = New DevExpress.XtraEditors.LabelControl()
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
        Me.gviewlicencias = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        Me.tabImp.SuspendLayout()
        Me.tabImpresion.SuspendLayout()
        CType(Me.txtIdPallet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.gviewlicencias, System.ComponentModel.ISupportInitialize).BeginInit()
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
        lblLicenciaCnt.Location = New System.Drawing.Point(35, 368)
        lblLicenciaCnt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLicenciaCnt.Name = "lblLicenciaCnt"
        lblLicenciaCnt.Size = New System.Drawing.Size(69, 21)
        lblLicenciaCnt.TabIndex = 18
        lblLicenciaCnt.Text = "Licencia"
        '
        'lblProducto
        '
        lblProducto.AutoSize = True
        lblProducto.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblProducto.Location = New System.Drawing.Point(29, 78)
        lblProducto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblProducto.Name = "lblProducto"
        lblProducto.Size = New System.Drawing.Size(81, 21)
        lblProducto.TabIndex = 24
        lblProducto.Text = "Producto:"
        '
        'lblLote
        '
        lblLote.AutoSize = True
        lblLote.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLote.Location = New System.Drawing.Point(29, 114)
        lblLote.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLote.Name = "lblLote"
        lblLote.Size = New System.Drawing.Size(48, 21)
        lblLote.TabIndex = 22
        lblLote.Text = "Lote:"
        '
        'lblLicencia
        '
        lblLicencia.AutoSize = True
        lblLicencia.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblLicencia.Location = New System.Drawing.Point(29, 199)
        lblLicencia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblLicencia.Name = "lblLicencia"
        lblLicencia.Size = New System.Drawing.Size(75, 21)
        lblLicencia.TabIndex = 26
        lblLicencia.Text = "Licencia:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(234, 345)
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
        Label4.Location = New System.Drawing.Point(324, 345)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(92, 21)
        Label4.TabIndex = 33
        Label4.Text = "Impresora:"
        '
        'lblVencimiento
        '
        lblVencimiento.AutoSize = True
        lblVencimiento.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblVencimiento.Location = New System.Drawing.Point(29, 159)
        lblVencimiento.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblVencimiento.Name = "lblVencimiento"
        lblVencimiento.Size = New System.Drawing.Size(107, 21)
        lblVencimiento.TabIndex = 37
        lblVencimiento.Text = "Vencimiento:"
        '
        'lblPresentacion
        '
        lblPresentacion.AutoSize = True
        lblPresentacion.BackColor = System.Drawing.Color.Transparent
        lblPresentacion.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblPresentacion.Location = New System.Drawing.Point(15, 42)
        lblPresentacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblPresentacion.Name = "lblPresentacion"
        lblPresentacion.Size = New System.Drawing.Size(111, 21)
        lblPresentacion.TabIndex = 38
        lblPresentacion.Text = "Presentación:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(15, 79)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(63, 21)
        Label2.TabIndex = 41
        Label2.Text = "Factor:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.BackColor = System.Drawing.Color.Transparent
        Label7.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.Location = New System.Drawing.Point(15, 118)
        Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(136, 21)
        Label7.TabIndex = 42
        Label7.Text = "Camas x Tarima:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.BackColor = System.Drawing.Color.Transparent
        Label8.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(15, 153)
        Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(117, 21)
        Label8.TabIndex = 43
        Label8.Text = "Cajas x Cama:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.Location = New System.Drawing.Point(698, 345)
        Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(65, 21)
        Label9.TabIndex = 45
        Label9.Text = "Copias:"
        '
        'RibbonControl
        '
        Me.RibbonControl.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonControl.MaxItemId = 1
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1125, 40)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 706)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1125, 30)
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
        Me.XtraScrollableControl.Location = New System.Drawing.Point(0, 40)
        Me.XtraScrollableControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.XtraScrollableControl.Name = "XtraScrollableControl"
        Me.XtraScrollableControl.Size = New System.Drawing.Size(1125, 666)
        Me.XtraScrollableControl.TabIndex = 5
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.tabImp)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1125, 666)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Datos para impresión"
        '
        'tabImp
        '
        Me.tabImp.Controls.Add(Me.tabImpresion)
        Me.tabImp.Controls.Add(Me.tabLicencias)
        Me.tabImp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabImp.Location = New System.Drawing.Point(2, 28)
        Me.tabImp.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabImp.Name = "tabImp"
        Me.tabImp.SelectedIndex = 0
        Me.tabImp.Size = New System.Drawing.Size(1121, 636)
        Me.tabImp.TabIndex = 53
        '
        'tabImpresion
        '
        Me.tabImpresion.Controls.Add(Me.txtIdPallet)
        Me.tabImpresion.Controls.Add(Me.lblIdPallet)
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
        Me.tabImpresion.Location = New System.Drawing.Point(4, 25)
        Me.tabImpresion.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabImpresion.Name = "tabImpresion"
        Me.tabImpresion.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabImpresion.Size = New System.Drawing.Size(1113, 607)
        Me.tabImpresion.TabIndex = 0
        Me.tabImpresion.Text = "Impresión"
        Me.tabImpresion.UseVisualStyleBackColor = True
        '
        'txtIdPallet
        '
        Me.txtIdPallet.Location = New System.Drawing.Point(150, 37)
        Me.txtIdPallet.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdPallet.Name = "txtIdPallet"
        Me.txtIdPallet.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.txtIdPallet.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdPallet.Properties.Appearance.Options.UseBackColor = True
        Me.txtIdPallet.Properties.Appearance.Options.UseFont = True
        Me.txtIdPallet.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtIdPallet.Properties.MaxLength = 50
        Me.txtIdPallet.Size = New System.Drawing.Size(434, 28)
        Me.txtIdPallet.TabIndex = 56
        Me.txtIdPallet.Visible = False
        '
        'lblIdPallet
        '
        Me.lblIdPallet.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblIdPallet.Appearance.ForeColor = System.Drawing.Color.Firebrick
        Me.lblIdPallet.Appearance.Options.UseFont = True
        Me.lblIdPallet.Appearance.Options.UseForeColor = True
        Me.lblIdPallet.Location = New System.Drawing.Point(33, 40)
        Me.lblIdPallet.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblIdPallet.Name = "lblIdPallet"
        Me.lblIdPallet.Size = New System.Drawing.Size(62, 21)
        Me.lblIdPallet.TabIndex = 55
        Me.lblIdPallet.Text = "IdPallet:"
        Me.lblIdPallet.Visible = False
        '
        'chkLicenciaBulto
        '
        Me.chkLicenciaBulto.AutoSize = True
        Me.chkLicenciaBulto.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.chkLicenciaBulto.Location = New System.Drawing.Point(239, 295)
        Me.chkLicenciaBulto.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkLicenciaBulto.Name = "chkLicenciaBulto"
        Me.chkLicenciaBulto.Size = New System.Drawing.Size(211, 25)
        Me.chkLicenciaBulto.TabIndex = 52
        Me.chkLicenciaBulto.Text = "Licencias y Presentacion"
        Me.chkLicenciaBulto.UseVisualStyleBackColor = True
        '
        'txtCantidadBarras
        '
        Me.txtCantidadBarras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadBarras.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadBarras.Location = New System.Drawing.Point(239, 404)
        Me.txtCantidadBarras.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidadBarras.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadBarras.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadBarras.Name = "txtCantidadBarras"
        Me.txtCantidadBarras.Size = New System.Drawing.Size(82, 28)
        Me.txtCantidadBarras.TabIndex = 5
        Me.txtCantidadBarras.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkSoloLicencia
        '
        Me.chkSoloLicencia.AutoSize = True
        Me.chkSoloLicencia.Checked = True
        Me.chkSoloLicencia.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.chkSoloLicencia.Location = New System.Drawing.Point(239, 270)
        Me.chkSoloLicencia.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.chkSoloLicencia.Name = "chkSoloLicencia"
        Me.chkSoloLicencia.Size = New System.Drawing.Size(134, 25)
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
        Me.cmdUnidad.Location = New System.Drawing.Point(576, 478)
        Me.cmdUnidad.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmdUnidad.Name = "cmdUnidad"
        Me.cmdUnidad.Size = New System.Drawing.Size(97, 73)
        Me.cmdUnidad.TabIndex = 50
        Me.cmdUnidad.Text = "&Unidad"
        '
        'txtCantidadLicencias
        '
        Me.txtCantidadLicencias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantidadLicencias.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadLicencias.Location = New System.Drawing.Point(239, 368)
        Me.txtCantidadLicencias.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantidadLicencias.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidadLicencias.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantidadLicencias.Name = "txtCantidadLicencias"
        Me.txtCantidadLicencias.Size = New System.Drawing.Size(82, 28)
        Me.txtCantidadLicencias.TabIndex = 19
        Me.txtCantidadLicencias.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblUmbasCant
        '
        Me.lblUmbasCant.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblUmbasCant.Appearance.Options.UseFont = True
        Me.lblUmbasCant.Location = New System.Drawing.Point(38, 444)
        Me.lblUmbasCant.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblUmbasCant.Name = "lblUmbasCant"
        Me.lblUmbasCant.Size = New System.Drawing.Size(130, 21)
        Me.lblUmbasCant.TabIndex = 49
        Me.lblUmbasCant.Text = "Producto UMBAS:"
        '
        'cmdPrinterUmbas
        '
        Me.cmdPrinterUmbas.Location = New System.Drawing.Point(328, 443)
        Me.cmdPrinterUmbas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdPrinterUmbas.MenuManager = Me.RibbonControl
        Me.cmdPrinterUmbas.Name = "cmdPrinterUmbas"
        Me.cmdPrinterUmbas.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrinterUmbas.Properties.Appearance.Options.UseFont = True
        Me.cmdPrinterUmbas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmdPrinterUmbas.Properties.NullText = ""
        Me.cmdPrinterUmbas.Size = New System.Drawing.Size(345, 28)
        Me.cmdPrinterUmbas.TabIndex = 48
        '
        'txtLicencia
        '
        Me.txtLicencia.Location = New System.Drawing.Point(150, 192)
        Me.txtLicencia.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLicencia.Name = "txtLicencia"
        Me.txtLicencia.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicencia.Properties.Appearance.Options.UseBackColor = True
        Me.txtLicencia.Properties.Appearance.Options.UseFont = True
        Me.txtLicencia.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtLicencia.Properties.MaxLength = 50
        Me.txtLicencia.Size = New System.Drawing.Size(434, 28)
        Me.txtLicencia.TabIndex = 27
        '
        'txtCantUmBas
        '
        Me.txtCantUmBas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCantUmBas.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantUmBas.Location = New System.Drawing.Point(239, 442)
        Me.txtCantUmBas.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCantUmBas.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantUmBas.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCantUmBas.Name = "txtCantUmBas"
        Me.txtCantUmBas.Size = New System.Drawing.Size(82, 28)
        Me.txtCantUmBas.TabIndex = 47
        Me.txtCantUmBas.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblProductoPres
        '
        Me.lblProductoPres.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblProductoPres.Appearance.Options.UseFont = True
        Me.lblProductoPres.Location = New System.Drawing.Point(38, 406)
        Me.lblProductoPres.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblProductoPres.Name = "lblProductoPres"
        Me.lblProductoPres.Size = New System.Drawing.Size(108, 21)
        Me.lblProductoPres.TabIndex = 46
        Me.lblProductoPres.Text = "Producto Pres:"
        '
        'cmbPrinterBarra
        '
        Me.cmbPrinterBarra.Location = New System.Drawing.Point(328, 405)
        Me.cmbPrinterBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPrinterBarra.MenuManager = Me.RibbonControl
        Me.cmbPrinterBarra.Name = "cmbPrinterBarra"
        Me.cmbPrinterBarra.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterBarra.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterBarra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterBarra.Properties.NullText = ""
        Me.cmbPrinterBarra.Size = New System.Drawing.Size(345, 28)
        Me.cmbPrinterBarra.TabIndex = 28
        '
        'cmbPrinterLicencia
        '
        Me.cmbPrinterLicencia.Location = New System.Drawing.Point(328, 369)
        Me.cmbPrinterLicencia.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPrinterLicencia.MenuManager = Me.RibbonControl
        Me.cmbPrinterLicencia.Name = "cmbPrinterLicencia"
        Me.cmbPrinterLicencia.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPrinterLicencia.Properties.Appearance.Options.UseFont = True
        Me.cmbPrinterLicencia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPrinterLicencia.Properties.NullText = ""
        Me.cmbPrinterLicencia.Size = New System.Drawing.Size(345, 28)
        Me.cmbPrinterLicencia.TabIndex = 29
        '
        'txtCopias
        '
        Me.txtCopias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopias.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopias.Location = New System.Drawing.Point(696, 366)
        Me.txtCopias.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCopias.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCopias.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCopias.Name = "txtCopias"
        Me.txtCopias.Size = New System.Drawing.Size(82, 28)
        Me.txtCopias.TabIndex = 44
        Me.txtCopias.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblEtiquetas
        '
        Me.lblEtiquetas.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblEtiquetas.Appearance.Options.UseFont = True
        Me.lblEtiquetas.Location = New System.Drawing.Point(696, 398)
        Me.lblEtiquetas.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblEtiquetas.Name = "lblEtiquetas"
        Me.lblEtiquetas.Size = New System.Drawing.Size(170, 21)
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
        Me.cmdImpresionBarra.Location = New System.Drawing.Point(451, 478)
        Me.cmdImpresionBarra.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmdImpresionBarra.Name = "cmdImpresionBarra"
        Me.cmdImpresionBarra.Size = New System.Drawing.Size(97, 73)
        Me.cmdImpresionBarra.TabIndex = 31
        Me.cmdImpresionBarra.Text = "&Fardo"
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.BackColor = System.Drawing.SystemColors.Info
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
        Me.GroupControl1.Location = New System.Drawing.Point(635, 26)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(436, 295)
        Me.GroupControl1.TabIndex = 42
        Me.GroupControl1.Text = "Atributos de producto"
        '
        'lblPesoTotal
        '
        Me.lblPesoTotal.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblPesoTotal.Appearance.Options.UseFont = True
        Me.lblPesoTotal.Location = New System.Drawing.Point(21, 228)
        Me.lblPesoTotal.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblPesoTotal.Name = "lblPesoTotal"
        Me.lblPesoTotal.Size = New System.Drawing.Size(84, 21)
        Me.lblPesoTotal.TabIndex = 50
        Me.lblPesoTotal.Text = "Peso Total:"
        '
        'txtPesoTotal
        '
        Me.txtPesoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPesoTotal.DecimalPlaces = 6
        Me.txtPesoTotal.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoTotal.Location = New System.Drawing.Point(153, 223)
        Me.txtPesoTotal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPesoTotal.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoTotal.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtPesoTotal.Name = "txtPesoTotal"
        Me.txtPesoTotal.Size = New System.Drawing.Size(250, 28)
        Me.txtPesoTotal.TabIndex = 49
        Me.txtPesoTotal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblPesoTarima
        '
        Me.lblPesoTarima.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.lblPesoTarima.Appearance.Options.UseFont = True
        Me.lblPesoTarima.Location = New System.Drawing.Point(21, 188)
        Me.lblPesoTarima.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblPesoTarima.Name = "lblPesoTarima"
        Me.lblPesoTarima.Size = New System.Drawing.Size(105, 21)
        Me.lblPesoTarima.TabIndex = 48
        Me.lblPesoTarima.Text = "Peso x Tarima"
        '
        'txtPesoTarima
        '
        Me.txtPesoTarima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPesoTarima.DecimalPlaces = 6
        Me.txtPesoTarima.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoTarima.Location = New System.Drawing.Point(153, 183)
        Me.txtPesoTarima.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPesoTarima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtPesoTarima.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtPesoTarima.Name = "txtPesoTarima"
        Me.txtPesoTarima.Size = New System.Drawing.Size(250, 28)
        Me.txtPesoTarima.TabIndex = 47
        Me.txtPesoTarima.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCajaPorCama
        '
        Me.txtCajaPorCama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCajaPorCama.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCajaPorCama.Location = New System.Drawing.Point(153, 145)
        Me.txtCajaPorCama.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCajaPorCama.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCajaPorCama.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCajaPorCama.Name = "txtCajaPorCama"
        Me.txtCajaPorCama.Size = New System.Drawing.Size(250, 28)
        Me.txtCajaPorCama.TabIndex = 45
        Me.txtCajaPorCama.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtCamaPorTarima
        '
        Me.txtCamaPorTarima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCamaPorTarima.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCamaPorTarima.Location = New System.Drawing.Point(153, 112)
        Me.txtCamaPorTarima.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCamaPorTarima.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCamaPorTarima.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtCamaPorTarima.Name = "txtCamaPorTarima"
        Me.txtCamaPorTarima.Size = New System.Drawing.Size(250, 28)
        Me.txtCamaPorTarima.TabIndex = 44
        Me.txtCamaPorTarima.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(153, 39)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPresentacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentacion.Properties.Appearance.Options.UseBackColor = True
        Me.txtPresentacion.Properties.Appearance.Options.UseFont = True
        Me.txtPresentacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtPresentacion.Properties.MaxLength = 50
        Me.txtPresentacion.Size = New System.Drawing.Size(250, 28)
        Me.txtPresentacion.TabIndex = 39
        '
        'txtFactor
        '
        Me.txtFactor.Location = New System.Drawing.Point(153, 74)
        Me.txtFactor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFactor.Name = "txtFactor"
        Me.txtFactor.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtFactor.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFactor.Properties.Appearance.Options.UseBackColor = True
        Me.txtFactor.Properties.Appearance.Options.UseFont = True
        Me.txtFactor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtFactor.Properties.MaxLength = 50
        Me.txtFactor.Size = New System.Drawing.Size(250, 28)
        Me.txtFactor.TabIndex = 40
        '
        'cmdImpresionLicencia
        '
        Me.cmdImpresionLicencia.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.cmdImpresionLicencia.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresionLicencia.Appearance.Options.UseBackColor = True
        Me.cmdImpresionLicencia.Appearance.Options.UseFont = True
        Me.cmdImpresionLicencia.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresionLicencia.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresionLicencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresionLicencia.Location = New System.Drawing.Point(328, 478)
        Me.cmdImpresionLicencia.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.cmdImpresionLicencia.Name = "cmdImpresionLicencia"
        Me.cmdImpresionLicencia.Size = New System.Drawing.Size(102, 73)
        Me.cmdImpresionLicencia.TabIndex = 32
        Me.cmdImpresionLicencia.Text = "&Licencia"
        '
        'txtVencimiento
        '
        Me.txtVencimiento.Location = New System.Drawing.Point(150, 153)
        Me.txtVencimiento.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtVencimiento.Name = "txtVencimiento"
        Me.txtVencimiento.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVencimiento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVencimiento.Properties.Appearance.Options.UseBackColor = True
        Me.txtVencimiento.Properties.Appearance.Options.UseFont = True
        Me.txtVencimiento.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtVencimiento.Properties.MaxLength = 50
        Me.txtVencimiento.Size = New System.Drawing.Size(434, 28)
        Me.txtVencimiento.TabIndex = 36
        '
        'cmbLote
        '
        Me.cmbLote.Location = New System.Drawing.Point(150, 112)
        Me.cmbLote.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbLote.MenuManager = Me.RibbonControl
        Me.cmbLote.Name = "cmbLote"
        Me.cmbLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLote.Properties.Appearance.Options.UseFont = True
        Me.cmbLote.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbLote.Properties.NullText = ""
        Me.cmbLote.Size = New System.Drawing.Size(434, 28)
        Me.cmbLote.TabIndex = 34
        '
        'cmbProducto
        '
        Me.cmbProducto.Location = New System.Drawing.Point(150, 74)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.MenuManager = Me.RibbonControl
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.Properties.Appearance.Options.UseFont = True
        Me.cmbProducto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbProducto.Properties.NullText = ""
        Me.cmbProducto.Size = New System.Drawing.Size(434, 28)
        Me.cmbProducto.TabIndex = 35
        '
        'tabLicencias
        '
        Me.tabLicencias.Controls.Add(Me.dgridBarrasPallet)
        Me.tabLicencias.Location = New System.Drawing.Point(4, 25)
        Me.tabLicencias.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabLicencias.Name = "tabLicencias"
        Me.tabLicencias.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.tabLicencias.Size = New System.Drawing.Size(1113, 607)
        Me.tabLicencias.TabIndex = 1
        Me.tabLicencias.Text = "Licencias"
        Me.tabLicencias.UseVisualStyleBackColor = True
        '
        'dgridBarrasPallet
        '
        Me.dgridBarrasPallet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridBarrasPallet.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgridBarrasPallet.Location = New System.Drawing.Point(4, 2)
        Me.dgridBarrasPallet.MainView = Me.gviewlicencias
        Me.dgridBarrasPallet.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgridBarrasPallet.Name = "dgridBarrasPallet"
        Me.dgridBarrasPallet.Size = New System.Drawing.Size(1105, 603)
        Me.dgridBarrasPallet.TabIndex = 1
        Me.dgridBarrasPallet.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gviewlicencias})
        '
        'gviewlicencias
        '
        Me.gviewlicencias.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gviewlicencias.Appearance.HeaderPanel.Options.UseFont = True
        Me.gviewlicencias.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gviewlicencias.Appearance.Row.Options.UseFont = True
        Me.gviewlicencias.DetailHeight = 437
        Me.gviewlicencias.GridControl = Me.dgridBarrasPallet
        Me.gviewlicencias.Name = "gviewlicencias"
        Me.gviewlicencias.OptionsBehavior.Editable = False
        Me.gviewlicencias.OptionsBehavior.ReadOnly = True
        Me.gviewlicencias.OptionsFind.AlwaysVisible = True
        Me.gviewlicencias.OptionsView.ShowFooter = True
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1125, 736)
        Me.Controls.Add(Me.XtraScrollableControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
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
        Me.tabImp.ResumeLayout(False)
        Me.tabImpresion.ResumeLayout(False)
        Me.tabImpresion.PerformLayout()
        CType(Me.txtIdPallet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.gviewlicencias, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tabImp As TabControl
    Friend WithEvents tabImpresion As TabPage
    Friend WithEvents tabLicencias As TabPage
    Friend WithEvents Cliente_direccionTableAdapter1 As cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents dgridBarrasPallet As DevExpress.XtraGrid.GridControl
    Friend WithEvents gviewlicencias As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblPesoTarima As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPesoTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPesoTotal As NumericUpDown
    Friend WithEvents lblIdPallet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtIdPallet As DevExpress.XtraEditors.TextEdit
End Class