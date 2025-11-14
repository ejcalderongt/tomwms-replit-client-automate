<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPreFactura
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
        Dim Label12 As System.Windows.Forms.Label
        Dim lblFechaDocumento As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreFactura))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdProcesar = New DevExpress.XtraBars.BarButtonItem()
        Me.chkConsolidados = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.cmdImpresionPrecuenta = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImpresionDetallePrecuenta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPrefactura = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCorreccionPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.chkControlPesoBruto = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkVarianteCobro = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkAgruparPorProducto = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkMantenerDolares = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.chkEnviarDolares = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.cmdAnularPrefactura = New DevExpress.XtraBars.BarButtonItem()
        Me.chkEstimacionCobro = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.cmdListadoIngresos = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup6 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GrpTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.lbDecimales = New DevExpress.XtraEditors.LabelControl()
        Me.txtDecimales = New DevExpress.XtraEditors.SpinEdit()
        Me.gbErrores = New System.Windows.Forms.GroupBox()
        Me.lblPrg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.lblScanPoliza = New DevExpress.XtraEditors.LabelControl()
        Me.txtScanPoliza = New DevExpress.XtraEditors.TextEdit()
        Me.lblTipoCambioOrigen = New DevExpress.XtraEditors.LabelControl()
        Me.lblObservacion = New DevExpress.XtraEditors.LabelControl()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.cmbPolizasPE = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbPolizasOC = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.pnValores = New DevExpress.XtraEditors.PanelControl()
        Me.lblCobroMoneda = New DevExpress.XtraEditors.LabelControl()
        Me.dtFechaIngreso = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.dtFechaSalida = New DevExpress.XtraEditors.DateEdit()
        Me.txtPesoTotal = New DevExpress.XtraEditors.SpinEdit()
        Me.txtValorAduana = New DevExpress.XtraEditors.SpinEdit()
        Me.txtValorGeneral = New DevExpress.XtraEditors.SpinEdit()
        Me.txtDiasAlmacenaje = New DevExpress.XtraEditors.SpinEdit()
        Me.txtAlmacenajeDiario = New DevExpress.XtraEditors.SpinEdit()
        Me.txtTotalFacturacion = New DevExpress.XtraEditors.SpinEdit()
        Me.lblFechaFinal = New DevExpress.XtraEditors.LabelControl()
        Me.lblTipoCambio = New DevExpress.XtraEditors.LabelControl()
        Me.lblFechaInicial = New DevExpress.XtraEditors.LabelControl()
        Me.dtpfechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.dtpFechaDocumento = New DevExpress.XtraEditors.DateEdit()
        Me.lblPolizaSalida = New DevExpress.XtraEditors.LabelControl()
        Me.lblCliente = New DevExpress.XtraEditors.LabelControl()
        Me.lblConsolidador = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoDocumento = New System.Windows.Forms.Label()
        Me.lblPolizaIngreso = New DevExpress.XtraEditors.LabelControl()
        Me.cmbCliente = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbPropietario = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.txtTipoCambio = New DevExpress.XtraEditors.SpinEdit()
        Me.dgridServiciosAsociados = New DevExpress.XtraGrid.GridControl()
        Me.gvDetalleServicios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtratabPrecuenta = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgriDetallePreCuenta = New DevExpress.XtraGrid.GridControl()
        Me.gvdetalleprecuenta = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Label12 = New System.Windows.Forms.Label()
        lblFechaDocumento = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTransaccion.SuspendLayout()
        CType(Me.txtDecimales.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbErrores.SuspendLayout()
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPolizasPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPolizasOC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnValores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnValores.SuspendLayout()
        CType(Me.dtFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaIngreso.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaSalida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFechaSalida.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPesoTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorAduana.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValorGeneral.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiasAlmacenaje.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlmacenajeDiario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalFacturacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaDocumento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaDocumento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCliente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoCambio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtratabPrecuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtratabPrecuenta.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.dgriDetallePreCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvdetalleprecuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(8, 50)
        Label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(71, 16)
        Label12.TabIndex = 0
        Label12.Text = "Documento"
        '
        'lblFechaDocumento
        '
        lblFechaDocumento.AutoSize = True
        lblFechaDocumento.Location = New System.Drawing.Point(8, 87)
        lblFechaDocumento.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        lblFechaDocumento.Name = "lblFechaDocumento"
        lblFechaDocumento.Size = New System.Drawing.Size(114, 16)
        lblFechaDocumento.TabIndex = 7
        lblFechaDocumento.Text = "Fecha Documento:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Location = New System.Drawing.Point(9, 123)
        IdPropietarioLabel.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(54, 16)
        IdPropietarioLabel.TabIndex = 9
        IdPropietarioLabel.Text = "Bodega:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(34, 18)
        Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(93, 16)
        Label2.TabIndex = 38
        Label2.Text = "Fecha Ingreso:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(34, 98)
        Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(85, 16)
        Label3.TabIndex = 42
        Label3.Text = "Fecha Salida:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(34, 142)
        Label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(89, 16)
        Label5.TabIndex = 47
        Label5.Text = "Valor Aduana:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(34, 185)
        Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(89, 16)
        Label6.TabIndex = 49
        Label6.Text = "Total General:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(34, 58)
        Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(116, 16)
        Label7.TabIndex = 51
        Label7.Text = "Total Peso (bruto):"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(34, 224)
        Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(107, 16)
        Label4.TabIndex = 56
        Label4.Text = "Dias Almacenaje:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(34, 265)
        Label9.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(86, 16)
        Label9.TabIndex = 60
        Label9.Text = "Valor por día:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(34, 308)
        Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(104, 16)
        Label1.TabIndex = 62
        Label1.Text = "Total a Facturar:"
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.cmdProcesar, Me.chkConsolidados, Me.BarSubItem1, Me.cmdImpresionPrecuenta, Me.cmdImpresionDetallePrecuenta, Me.BarButtonItem1, Me.cmdCorreccionPoliza, Me.chkControlPesoBruto, Me.chkVarianteCobro, Me.chkAgruparPorProducto, Me.chkMantenerDolares, Me.chkEnviarDolares, Me.mnuPrefactura, Me.cmdAnularPrefactura, Me.chkEstimacionCobro, Me.cmdListadoIngresos})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.RibbonControl.MaxItemId = 24
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 412
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1598, 193)
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'cmdProcesar
        '
        Me.cmdProcesar.Caption = "Calcular Cobro"
        Me.cmdProcesar.Id = 2
        Me.cmdProcesar.ImageOptions.SvgImage = CType(resources.GetObject("cmdProcesar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdProcesar.Name = "cmdProcesar"
        '
        'chkConsolidados
        '
        Me.chkConsolidados.Caption = "Consolidadores"
        Me.chkConsolidados.Id = 3
        Me.chkConsolidados.Name = "chkConsolidados"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Imprimir"
        Me.BarSubItem1.Id = 5
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImpresionPrecuenta), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImpresionDetallePrecuenta), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuPrefactura)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'cmdImpresionPrecuenta
        '
        Me.cmdImpresionPrecuenta.Caption = "Precuenta"
        Me.cmdImpresionPrecuenta.Id = 6
        Me.cmdImpresionPrecuenta.Name = "cmdImpresionPrecuenta"
        '
        'cmdImpresionDetallePrecuenta
        '
        Me.cmdImpresionDetallePrecuenta.Caption = "Detalle Precuenta"
        Me.cmdImpresionDetallePrecuenta.Id = 7
        Me.cmdImpresionDetallePrecuenta.Name = "cmdImpresionDetallePrecuenta"
        '
        'mnuPrefactura
        '
        Me.mnuPrefactura.Caption = "Prefactura"
        Me.mnuPrefactura.Id = 18
        Me.mnuPrefactura.Name = "mnuPrefactura"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdCorreccionPoliza
        '
        Me.cmdCorreccionPoliza.Caption = "Corregir Póliza Salida"
        Me.cmdCorreccionPoliza.Id = 9
        Me.cmdCorreccionPoliza.ImageOptions.SvgImage = CType(resources.GetObject("cmdCorreccionPoliza.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCorreccionPoliza.Name = "cmdCorreccionPoliza"
        '
        'chkControlPesoBruto
        '
        Me.chkControlPesoBruto.BindableChecked = True
        Me.chkControlPesoBruto.Caption = "Cobro con peso bruto"
        Me.chkControlPesoBruto.Checked = True
        Me.chkControlPesoBruto.Id = 11
        Me.chkControlPesoBruto.Name = "chkControlPesoBruto"
        '
        'chkVarianteCobro
        '
        Me.chkVarianteCobro.Caption = "Calcular con variante"
        Me.chkVarianteCobro.Hint = "Aplica si el cobro es por unidad (mt2, mt3 o posición)"
        Me.chkVarianteCobro.Id = 12
        Me.chkVarianteCobro.Name = "chkVarianteCobro"
        '
        'chkAgruparPorProducto
        '
        Me.chkAgruparPorProducto.Caption = "Agrupar por producto"
        Me.chkAgruparPorProducto.Id = 14
        Me.chkAgruparPorProducto.Name = "chkAgruparPorProducto"
        '
        'chkMantenerDolares
        '
        Me.chkMantenerDolares.Caption = "Mantener calculo en dólares"
        Me.chkMantenerDolares.Hint = "Aplica si acuerdo esta en dolares y se debe mantener dolares."
        Me.chkMantenerDolares.Id = 16
        Me.chkMantenerDolares.Name = "chkMantenerDolares"
        '
        'chkEnviarDolares
        '
        Me.chkEnviarDolares.Caption = "Enviar dólares hacia el ERP"
        Me.chkEnviarDolares.Enabled = False
        Me.chkEnviarDolares.Hint = "Aplica si el cálculo esta en dolares y debe facturar dolares."
        Me.chkEnviarDolares.Id = 17
        Me.chkEnviarDolares.Name = "chkEnviarDolares"
        Me.chkEnviarDolares.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'cmdAnularPrefactura
        '
        Me.cmdAnularPrefactura.Caption = "Anular Prefactura"
        Me.cmdAnularPrefactura.Id = 19
        Me.cmdAnularPrefactura.ImageOptions.SvgImage = CType(resources.GetObject("cmdAnularPrefactura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdAnularPrefactura.Name = "cmdAnularPrefactura"
        '
        'chkEstimacionCobro
        '
        Me.chkEstimacionCobro.Caption = "Cotizar cobro"
        Me.chkEstimacionCobro.Id = 21
        Me.chkEstimacionCobro.ImageOptions.SvgImage = CType(resources.GetObject("chkEstimacionCobro.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.chkEstimacionCobro.Name = "chkEstimacionCobro"
        '
        'cmdListadoIngresos
        '
        Me.cmdListadoIngresos.Caption = "Lista Polizas de Ingreso"
        Me.cmdListadoIngresos.Id = 23
        Me.cmdListadoIngresos.ImageOptions.SvgImage = CType(resources.GetObject("cmdListadoIngresos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdListadoIngresos.Name = "cmdListadoIngresos"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup4, Me.RibbonPageGroup5, Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.RibbonPageGroup6})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "PreFacturación"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarSubItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdProcesar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkConsolidados)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkMantenerDolares)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.chkEnviarDolares)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkControlPesoBruto)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.chkVarianteCobro)
        Me.RibbonPageGroup5.ItemLinks.Add(Me.cmdListadoIngresos)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkAgruparPorProducto)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.chkEstimacionCobro)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.cmdCorreccionPoliza, "2.1.1.2")
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup6
        '
        Me.RibbonPageGroup6.ItemLinks.Add(Me.cmdAnularPrefactura, "2.1.1.2")
        Me.RibbonPageGroup6.Name = "RibbonPageGroup6"
        '
        'GrpTransaccion
        '
        Me.GrpTransaccion.Controls.Add(Me.lbDecimales)
        Me.GrpTransaccion.Controls.Add(Me.txtDecimales)
        Me.GrpTransaccion.Controls.Add(Me.gbErrores)
        Me.GrpTransaccion.Controls.Add(Me.lblScanPoliza)
        Me.GrpTransaccion.Controls.Add(Me.txtScanPoliza)
        Me.GrpTransaccion.Controls.Add(Me.lblTipoCambioOrigen)
        Me.GrpTransaccion.Controls.Add(Me.lblObservacion)
        Me.GrpTransaccion.Controls.Add(Me.txtObservacion)
        Me.GrpTransaccion.Controls.Add(Me.cmbPolizasPE)
        Me.GrpTransaccion.Controls.Add(Me.cmbPolizasOC)
        Me.GrpTransaccion.Controls.Add(Me.pnValores)
        Me.GrpTransaccion.Controls.Add(Me.lblFechaFinal)
        Me.GrpTransaccion.Controls.Add(Me.lblTipoCambio)
        Me.GrpTransaccion.Controls.Add(Me.lblFechaInicial)
        Me.GrpTransaccion.Controls.Add(Me.dtpfechaHasta)
        Me.GrpTransaccion.Controls.Add(Me.dtpFechaDesde)
        Me.GrpTransaccion.Controls.Add(Me.cmbBodega)
        Me.GrpTransaccion.Controls.Add(Me.dtpFechaDocumento)
        Me.GrpTransaccion.Controls.Add(Me.lblPolizaSalida)
        Me.GrpTransaccion.Controls.Add(Label12)
        Me.GrpTransaccion.Controls.Add(Me.lblCliente)
        Me.GrpTransaccion.Controls.Add(lblFechaDocumento)
        Me.GrpTransaccion.Controls.Add(Me.lblConsolidador)
        Me.GrpTransaccion.Controls.Add(Me.txtNoDocumento)
        Me.GrpTransaccion.Controls.Add(IdPropietarioLabel)
        Me.GrpTransaccion.Controls.Add(Me.lblPolizaIngreso)
        Me.GrpTransaccion.Controls.Add(Me.cmbCliente)
        Me.GrpTransaccion.Controls.Add(Me.cmbPropietario)
        Me.GrpTransaccion.Controls.Add(Me.txtTipoCambio)
        Me.GrpTransaccion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrpTransaccion.Location = New System.Drawing.Point(0, 193)
        Me.GrpTransaccion.Margin = New System.Windows.Forms.Padding(5)
        Me.GrpTransaccion.Name = "GrpTransaccion"
        Me.GrpTransaccion.Size = New System.Drawing.Size(1598, 456)
        Me.GrpTransaccion.TabIndex = 2
        Me.GrpTransaccion.Text = "Encabezado"
        '
        'lbDecimales
        '
        Me.lbDecimales.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lbDecimales.Appearance.Options.UseFont = True
        Me.lbDecimales.Location = New System.Drawing.Point(503, 48)
        Me.lbDecimales.Margin = New System.Windows.Forms.Padding(5)
        Me.lbDecimales.Name = "lbDecimales"
        Me.lbDecimales.Size = New System.Drawing.Size(63, 16)
        Me.lbDecimales.TabIndex = 94
        Me.lbDecimales.Text = "Decimales:"
        '
        'txtDecimales
        '
        Me.txtDecimales.EditValue = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtDecimales.Location = New System.Drawing.Point(625, 46)
        Me.txtDecimales.Margin = New System.Windows.Forms.Padding(5)
        Me.txtDecimales.Name = "txtDecimales"
        Me.txtDecimales.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDecimales.Properties.Appearance.Options.UseFont = True
        Me.txtDecimales.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDecimales.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtDecimales.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtDecimales.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtDecimales.Properties.MaxValue = New Decimal(New Integer() {6, 0, 0, 0})
        Me.txtDecimales.Properties.MinValue = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtDecimales.Size = New System.Drawing.Size(239, 24)
        Me.txtDecimales.TabIndex = 6
        '
        'gbErrores
        '
        Me.gbErrores.Controls.Add(Me.lblPrg)
        Me.gbErrores.Controls.Add(Me.prg)
        Me.gbErrores.Controls.Add(Me.lblTLog)
        Me.gbErrores.Location = New System.Drawing.Point(625, 274)
        Me.gbErrores.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Name = "gbErrores"
        Me.gbErrores.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.gbErrores.Size = New System.Drawing.Size(239, 162)
        Me.gbErrores.TabIndex = 92
        Me.gbErrores.TabStop = False
        Me.gbErrores.Text = "Ingreso sin histórico."
        '
        'lblPrg
        '
        Me.lblPrg.BackColor = System.Drawing.Color.OldLace
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrg.Location = New System.Drawing.Point(4, 73)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(231, 87)
        Me.lblPrg.TabIndex = 5
        Me.lblPrg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(4, 45)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(231, 28)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(4, 18)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(231, 27)
        Me.lblTLog.TabIndex = 3
        Me.lblTLog.Text = "Log"
        '
        'lblScanPoliza
        '
        Me.lblScanPoliza.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblScanPoliza.Appearance.Options.UseFont = True
        Me.lblScanPoliza.Location = New System.Drawing.Point(503, 239)
        Me.lblScanPoliza.Margin = New System.Windows.Forms.Padding(4)
        Me.lblScanPoliza.Name = "lblScanPoliza"
        Me.lblScanPoliza.Size = New System.Drawing.Size(89, 16)
        Me.lblScanPoliza.TabIndex = 91
        Me.lblScanPoliza.Text = "Escanée Poliza:"
        '
        'txtScanPoliza
        '
        Me.txtScanPoliza.Location = New System.Drawing.Point(625, 234)
        Me.txtScanPoliza.Margin = New System.Windows.Forms.Padding(4)
        Me.txtScanPoliza.Name = "txtScanPoliza"
        Me.txtScanPoliza.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.txtScanPoliza.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanPoliza.Properties.Appearance.Options.UseBackColor = True
        Me.txtScanPoliza.Properties.Appearance.Options.UseFont = True
        Me.txtScanPoliza.Size = New System.Drawing.Size(239, 24)
        Me.txtScanPoliza.TabIndex = 90
        '
        'lblTipoCambioOrigen
        '
        Me.lblTipoCambioOrigen.Location = New System.Drawing.Point(467, 308)
        Me.lblTipoCambioOrigen.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTipoCambioOrigen.Name = "lblTipoCambioOrigen"
        Me.lblTipoCambioOrigen.Size = New System.Drawing.Size(72, 16)
        Me.lblTipoCambioOrigen.TabIndex = 87
        Me.lblTipoCambioOrigen.Text = "Tipo Cambio"
        Me.lblTipoCambioOrigen.Visible = False
        '
        'lblObservacion
        '
        Me.lblObservacion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblObservacion.Appearance.Options.UseFont = True
        Me.lblObservacion.Location = New System.Drawing.Point(10, 345)
        Me.lblObservacion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblObservacion.Name = "lblObservacion"
        Me.lblObservacion.Size = New System.Drawing.Size(75, 16)
        Me.lblObservacion.TabIndex = 86
        Me.lblObservacion.Text = "Observación:"
        '
        'txtObservacion
        '
        Me.txtObservacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservacion.Location = New System.Drawing.Point(158, 343)
        Me.txtObservacion.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.txtObservacion.Multiline = True
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.Size = New System.Drawing.Size(301, 93)
        Me.txtObservacion.TabIndex = 85
        '
        'cmbPolizasPE
        '
        Me.cmbPolizasPE.Location = New System.Drawing.Point(625, 194)
        Me.cmbPolizasPE.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbPolizasPE.Name = "cmbPolizasPE"
        Me.cmbPolizasPE.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPolizasPE.Properties.Appearance.Options.UseFont = True
        Me.cmbPolizasPE.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPolizasPE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPolizasPE.Properties.NullText = ""
        Me.cmbPolizasPE.Properties.PopupView = Me.GridView2
        Me.cmbPolizasPE.Size = New System.Drawing.Size(239, 24)
        Me.cmbPolizasPE.TabIndex = 8
        '
        'GridView2
        '
        Me.GridView2.DetailHeight = 437
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'cmbPolizasOC
        '
        Me.cmbPolizasOC.Location = New System.Drawing.Point(625, 158)
        Me.cmbPolizasOC.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbPolizasOC.Name = "cmbPolizasOC"
        Me.cmbPolizasOC.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPolizasOC.Properties.Appearance.Options.UseFont = True
        Me.cmbPolizasOC.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPolizasOC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPolizasOC.Properties.NullText = ""
        Me.cmbPolizasOC.Properties.PopupView = Me.GridView1
        Me.cmbPolizasOC.Size = New System.Drawing.Size(239, 24)
        Me.cmbPolizasOC.TabIndex = 7
        '
        'GridView1
        '
        Me.GridView1.DetailHeight = 437
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'pnValores
        '
        Me.pnValores.Controls.Add(Me.lblCobroMoneda)
        Me.pnValores.Controls.Add(Label1)
        Me.pnValores.Controls.Add(Label2)
        Me.pnValores.Controls.Add(Me.dtFechaIngreso)
        Me.pnValores.Controls.Add(Me.LabelControl1)
        Me.pnValores.Controls.Add(Label3)
        Me.pnValores.Controls.Add(Label9)
        Me.pnValores.Controls.Add(Me.dtFechaSalida)
        Me.pnValores.Controls.Add(Label5)
        Me.pnValores.Controls.Add(Label4)
        Me.pnValores.Controls.Add(Label6)
        Me.pnValores.Controls.Add(Label7)
        Me.pnValores.Controls.Add(Me.txtPesoTotal)
        Me.pnValores.Controls.Add(Me.txtValorAduana)
        Me.pnValores.Controls.Add(Me.txtValorGeneral)
        Me.pnValores.Controls.Add(Me.txtDiasAlmacenaje)
        Me.pnValores.Controls.Add(Me.txtAlmacenajeDiario)
        Me.pnValores.Controls.Add(Me.txtTotalFacturacion)
        Me.pnValores.Location = New System.Drawing.Point(889, 48)
        Me.pnValores.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.pnValores.Name = "pnValores"
        Me.pnValores.Size = New System.Drawing.Size(631, 354)
        Me.pnValores.TabIndex = 68
        '
        'lblCobroMoneda
        '
        Me.lblCobroMoneda.Appearance.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCobroMoneda.Appearance.Options.UseFont = True
        Me.lblCobroMoneda.Location = New System.Drawing.Point(574, 134)
        Me.lblCobroMoneda.Margin = New System.Windows.Forms.Padding(4)
        Me.lblCobroMoneda.Name = "lblCobroMoneda"
        Me.lblCobroMoneda.Size = New System.Drawing.Size(210, 28)
        Me.lblCobroMoneda.TabIndex = 88
        Me.lblCobroMoneda.Text = "Enviar Dólares a ERP"
        Me.lblCobroMoneda.Visible = False
        '
        'dtFechaIngreso
        '
        Me.dtFechaIngreso.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtFechaIngreso.Location = New System.Drawing.Point(192, 12)
        Me.dtFechaIngreso.Margin = New System.Windows.Forms.Padding(5)
        Me.dtFechaIngreso.MenuManager = Me.RibbonControl
        Me.dtFechaIngreso.Name = "dtFechaIngreso"
        Me.dtFechaIngreso.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.dtFechaIngreso.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaIngreso.Properties.Appearance.Options.UseBackColor = True
        Me.dtFechaIngreso.Properties.Appearance.Options.UseFont = True
        Me.dtFechaIngreso.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtFechaIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaIngreso.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaIngreso.Properties.ReadOnly = True
        Me.dtFechaIngreso.Size = New System.Drawing.Size(248, 24)
        Me.dtFechaIngreso.TabIndex = 39
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseImage = True
        Me.LabelControl1.Location = New System.Drawing.Point(184, 101)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(0, 18)
        Me.LabelControl1.TabIndex = 41
        '
        'dtFechaSalida
        '
        Me.dtFechaSalida.EditValue = New Date(2017, 11, 20, 10, 7, 9, 549)
        Me.dtFechaSalida.Location = New System.Drawing.Point(190, 98)
        Me.dtFechaSalida.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.dtFechaSalida.Name = "dtFechaSalida"
        Me.dtFechaSalida.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaSalida.Properties.Appearance.Options.UseFont = True
        Me.dtFechaSalida.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.dtFechaSalida.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaSalida.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtFechaSalida.Size = New System.Drawing.Size(251, 24)
        Me.dtFechaSalida.TabIndex = 43
        '
        'txtPesoTotal
        '
        Me.txtPesoTotal.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtPesoTotal.Location = New System.Drawing.Point(190, 52)
        Me.txtPesoTotal.Margin = New System.Windows.Forms.Padding(5)
        Me.txtPesoTotal.Name = "txtPesoTotal"
        Me.txtPesoTotal.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtPesoTotal.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoTotal.Properties.Appearance.Options.UseBackColor = True
        Me.txtPesoTotal.Properties.Appearance.Options.UseFont = True
        Me.txtPesoTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtPesoTotal.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtPesoTotal.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtPesoTotal.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtPesoTotal.Properties.MaskSettings.Set("mask", "f")
        Me.txtPesoTotal.Size = New System.Drawing.Size(251, 24)
        Me.txtPesoTotal.TabIndex = 52
        '
        'txtValorAduana
        '
        Me.txtValorAduana.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtValorAduana.Location = New System.Drawing.Point(190, 138)
        Me.txtValorAduana.Margin = New System.Windows.Forms.Padding(5)
        Me.txtValorAduana.Name = "txtValorAduana"
        Me.txtValorAduana.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtValorAduana.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorAduana.Properties.Appearance.Options.UseBackColor = True
        Me.txtValorAduana.Properties.Appearance.Options.UseFont = True
        Me.txtValorAduana.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtValorAduana.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtValorAduana.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtValorAduana.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtValorAduana.Properties.MaskSettings.Set("mask", "f")
        Me.txtValorAduana.Size = New System.Drawing.Size(251, 24)
        Me.txtValorAduana.TabIndex = 48
        '
        'txtValorGeneral
        '
        Me.txtValorGeneral.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtValorGeneral.Location = New System.Drawing.Point(190, 181)
        Me.txtValorGeneral.Margin = New System.Windows.Forms.Padding(5)
        Me.txtValorGeneral.Name = "txtValorGeneral"
        Me.txtValorGeneral.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtValorGeneral.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorGeneral.Properties.Appearance.Options.UseBackColor = True
        Me.txtValorGeneral.Properties.Appearance.Options.UseFont = True
        Me.txtValorGeneral.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtValorGeneral.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtValorGeneral.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtValorGeneral.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtValorGeneral.Properties.MaskSettings.Set("mask", "f")
        Me.txtValorGeneral.Size = New System.Drawing.Size(251, 24)
        Me.txtValorGeneral.TabIndex = 50
        '
        'txtDiasAlmacenaje
        '
        Me.txtDiasAlmacenaje.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtDiasAlmacenaje.Location = New System.Drawing.Point(190, 220)
        Me.txtDiasAlmacenaje.Margin = New System.Windows.Forms.Padding(5)
        Me.txtDiasAlmacenaje.Name = "txtDiasAlmacenaje"
        Me.txtDiasAlmacenaje.Properties.Appearance.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtDiasAlmacenaje.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiasAlmacenaje.Properties.Appearance.Options.UseBackColor = True
        Me.txtDiasAlmacenaje.Properties.Appearance.Options.UseFont = True
        Me.txtDiasAlmacenaje.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDiasAlmacenaje.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtDiasAlmacenaje.Size = New System.Drawing.Size(251, 24)
        Me.txtDiasAlmacenaje.TabIndex = 44
        '
        'txtAlmacenajeDiario
        '
        Me.txtAlmacenajeDiario.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtAlmacenajeDiario.Location = New System.Drawing.Point(189, 261)
        Me.txtAlmacenajeDiario.Margin = New System.Windows.Forms.Padding(5)
        Me.txtAlmacenajeDiario.Name = "txtAlmacenajeDiario"
        Me.txtAlmacenajeDiario.Properties.Appearance.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtAlmacenajeDiario.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlmacenajeDiario.Properties.Appearance.Options.UseBackColor = True
        Me.txtAlmacenajeDiario.Properties.Appearance.Options.UseFont = True
        Me.txtAlmacenajeDiario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtAlmacenajeDiario.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtAlmacenajeDiario.Size = New System.Drawing.Size(251, 24)
        Me.txtAlmacenajeDiario.TabIndex = 57
        '
        'txtTotalFacturacion
        '
        Me.txtTotalFacturacion.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtTotalFacturacion.Location = New System.Drawing.Point(189, 304)
        Me.txtTotalFacturacion.Margin = New System.Windows.Forms.Padding(5)
        Me.txtTotalFacturacion.Name = "txtTotalFacturacion"
        Me.txtTotalFacturacion.Properties.Appearance.BackColor = System.Drawing.Color.AntiqueWhite
        Me.txtTotalFacturacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalFacturacion.Properties.Appearance.Options.UseBackColor = True
        Me.txtTotalFacturacion.Properties.Appearance.Options.UseFont = True
        Me.txtTotalFacturacion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTotalFacturacion.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtTotalFacturacion.Properties.MaskSettings.Set("mask", "f")
        Me.txtTotalFacturacion.Size = New System.Drawing.Size(251, 24)
        Me.txtTotalFacturacion.TabIndex = 61
        '
        'lblFechaFinal
        '
        Me.lblFechaFinal.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblFechaFinal.Appearance.Options.UseFont = True
        Me.lblFechaFinal.Location = New System.Drawing.Point(10, 268)
        Me.lblFechaFinal.Margin = New System.Windows.Forms.Padding(5)
        Me.lblFechaFinal.Name = "lblFechaFinal"
        Me.lblFechaFinal.Size = New System.Drawing.Size(75, 16)
        Me.lblFechaFinal.TabIndex = 75
        Me.lblFechaFinal.Text = "Fecha Hasta:"
        '
        'lblTipoCambio
        '
        Me.lblTipoCambio.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblTipoCambio.Appearance.Options.UseFont = True
        Me.lblTipoCambio.Location = New System.Drawing.Point(10, 308)
        Me.lblTipoCambio.Margin = New System.Windows.Forms.Padding(5)
        Me.lblTipoCambio.Name = "lblTipoCambio"
        Me.lblTipoCambio.Size = New System.Drawing.Size(115, 16)
        Me.lblTipoCambio.TabIndex = 79
        Me.lblTipoCambio.Text = "Tipo Cambio (USD):"
        '
        'lblFechaInicial
        '
        Me.lblFechaInicial.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblFechaInicial.Appearance.Options.UseFont = True
        Me.lblFechaInicial.Location = New System.Drawing.Point(10, 231)
        Me.lblFechaInicial.Margin = New System.Windows.Forms.Padding(5)
        Me.lblFechaInicial.Name = "lblFechaInicial"
        Me.lblFechaInicial.Size = New System.Drawing.Size(78, 16)
        Me.lblFechaInicial.TabIndex = 74
        Me.lblFechaInicial.Text = "Fecha Desde:"
        '
        'dtpfechaHasta
        '
        Me.dtpfechaHasta.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfechaHasta.Location = New System.Drawing.Point(159, 266)
        Me.dtpfechaHasta.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpfechaHasta.Name = "dtpfechaHasta"
        Me.dtpfechaHasta.Size = New System.Drawing.Size(299, 26)
        Me.dtpfechaHasta.TabIndex = 4
        '
        'dtpFechaDesde
        '
        Me.dtpFechaDesde.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDesde.Location = New System.Drawing.Point(159, 228)
        Me.dtpFechaDesde.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpFechaDesde.Name = "dtpFechaDesde"
        Me.dtpFechaDesde.Size = New System.Drawing.Size(299, 26)
        Me.dtpFechaDesde.TabIndex = 3
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(162, 119)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBodega.Properties.Appearance.Options.UseFont = True
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Size = New System.Drawing.Size(298, 24)
        Me.cmbBodega.TabIndex = 31
        '
        'dtpFechaDocumento
        '
        Me.dtpFechaDocumento.EditValue = New Date(2017, 11, 19, 11, 13, 59, 0)
        Me.dtpFechaDocumento.Location = New System.Drawing.Point(162, 82)
        Me.dtpFechaDocumento.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpFechaDocumento.MenuManager = Me.RibbonControl
        Me.dtpFechaDocumento.Name = "dtpFechaDocumento"
        Me.dtpFechaDocumento.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaDocumento.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaDocumento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDocumento.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaDocumento.Properties.ReadOnly = True
        Me.dtpFechaDocumento.Size = New System.Drawing.Size(298, 24)
        Me.dtpFechaDocumento.TabIndex = 8
        '
        'lblPolizaSalida
        '
        Me.lblPolizaSalida.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblPolizaSalida.Appearance.Options.UseFont = True
        Me.lblPolizaSalida.Location = New System.Drawing.Point(503, 198)
        Me.lblPolizaSalida.Margin = New System.Windows.Forms.Padding(5)
        Me.lblPolizaSalida.Name = "lblPolizaSalida"
        Me.lblPolizaSalida.Size = New System.Drawing.Size(83, 16)
        Me.lblPolizaSalida.TabIndex = 67
        Me.lblPolizaSalida.Text = "Pólizas Salida:"
        '
        'lblCliente
        '
        Me.lblCliente.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblCliente.Appearance.Options.UseFont = True
        Me.lblCliente.Location = New System.Drawing.Point(12, 197)
        Me.lblCliente.Margin = New System.Windows.Forms.Padding(5)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(64, 16)
        Me.lblCliente.TabIndex = 66
        Me.lblCliente.Text = "Facturar a:"
        '
        'lblConsolidador
        '
        Me.lblConsolidador.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblConsolidador.Appearance.Options.UseFont = True
        Me.lblConsolidador.Location = New System.Drawing.Point(10, 160)
        Me.lblConsolidador.Margin = New System.Windows.Forms.Padding(5)
        Me.lblConsolidador.Name = "lblConsolidador"
        Me.lblConsolidador.Size = New System.Drawing.Size(79, 16)
        Me.lblConsolidador.TabIndex = 65
        Me.lblConsolidador.Text = "Consolidador:"
        '
        'txtNoDocumento
        '
        Me.txtNoDocumento.BackColor = System.Drawing.Color.Linen
        Me.txtNoDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoDocumento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoDocumento.Location = New System.Drawing.Point(162, 48)
        Me.txtNoDocumento.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.txtNoDocumento.Name = "txtNoDocumento"
        Me.txtNoDocumento.Size = New System.Drawing.Size(296, 25)
        Me.txtNoDocumento.TabIndex = 1
        '
        'lblPolizaIngreso
        '
        Me.lblPolizaIngreso.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblPolizaIngreso.Appearance.Options.UseFont = True
        Me.lblPolizaIngreso.Location = New System.Drawing.Point(503, 161)
        Me.lblPolizaIngreso.Margin = New System.Windows.Forms.Padding(5)
        Me.lblPolizaIngreso.Name = "lblPolizaIngreso"
        Me.lblPolizaIngreso.Size = New System.Drawing.Size(90, 16)
        Me.lblPolizaIngreso.TabIndex = 0
        Me.lblPolizaIngreso.Text = "Pólizas ingreso:"
        '
        'cmbCliente
        '
        Me.cmbCliente.Location = New System.Drawing.Point(159, 193)
        Me.cmbCliente.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCliente.Properties.Appearance.Options.UseFont = True
        Me.cmbCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbCliente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbCliente.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.cmbCliente.Size = New System.Drawing.Size(300, 24)
        Me.cmbCliente.TabIndex = 2
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.DetailHeight = 437
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 1000
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(159, 156)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPropietario.Properties.Appearance.Options.UseFont = True
        Me.cmbPropietario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.PopupView = Me.GridLookUpEdit1View
        Me.cmbPropietario.Size = New System.Drawing.Size(300, 24)
        Me.cmbPropietario.TabIndex = 1
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.DetailHeight = 437
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 1000
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtTipoCambio.Location = New System.Drawing.Point(158, 304)
        Me.txtTipoCambio.Margin = New System.Windows.Forms.Padding(5)
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.Properties.Appearance.Options.UseFont = True
        Me.txtTipoCambio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTipoCambio.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtTipoCambio.Properties.MaskSettings.Set("MaskManagerType", GetType(DevExpress.Data.Mask.NumericMaskManager))
        Me.txtTipoCambio.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False")
        Me.txtTipoCambio.Size = New System.Drawing.Size(301, 24)
        Me.txtTipoCambio.TabIndex = 5
        '
        'dgridServiciosAsociados
        '
        Me.dgridServiciosAsociados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridServiciosAsociados.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.dgridServiciosAsociados.Font = New System.Drawing.Font("Tahoma", 6.0!)
        Me.dgridServiciosAsociados.Location = New System.Drawing.Point(0, 0)
        Me.dgridServiciosAsociados.MainView = Me.gvDetalleServicios
        Me.dgridServiciosAsociados.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.dgridServiciosAsociados.Name = "dgridServiciosAsociados"
        Me.dgridServiciosAsociados.Size = New System.Drawing.Size(1592, 262)
        Me.dgridServiciosAsociados.TabIndex = 32
        Me.dgridServiciosAsociados.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDetalleServicios})
        '
        'gvDetalleServicios
        '
        Me.gvDetalleServicios.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.gvDetalleServicios.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.gvDetalleServicios.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleServicios.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDetalleServicios.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDetalleServicios.Appearance.Row.Options.UseFont = True
        Me.gvDetalleServicios.DetailHeight = 437
        Me.gvDetalleServicios.GridControl = Me.dgridServiciosAsociados
        Me.gvDetalleServicios.Name = "gvDetalleServicios"
        Me.gvDetalleServicios.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvDetalleServicios.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvDetalleServicios.OptionsView.ColumnAutoWidth = False
        Me.gvDetalleServicios.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gvDetalleServicios.OptionsView.ShowFooter = True
        Me.gvDetalleServicios.OptionsView.ShowGroupPanel = False
        '
        'xtratabPrecuenta
        '
        Me.xtratabPrecuenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtratabPrecuenta.Location = New System.Drawing.Point(2, 2)
        Me.xtratabPrecuenta.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.xtratabPrecuenta.Name = "xtratabPrecuenta"
        Me.xtratabPrecuenta.SelectedTabPage = Me.XtraTabPage1
        Me.xtratabPrecuenta.Size = New System.Drawing.Size(1594, 292)
        Me.xtratabPrecuenta.TabIndex = 64
        Me.xtratabPrecuenta.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.dgridServiciosAsociados)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1592, 262)
        Me.XtraTabPage1.Text = "Precuenta"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.dgriDetallePreCuenta)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1592, 262)
        Me.XtraTabPage2.Text = "Detalle de precuenta"
        '
        'dgriDetallePreCuenta
        '
        Me.dgriDetallePreCuenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgriDetallePreCuenta.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.dgriDetallePreCuenta.Location = New System.Drawing.Point(0, 0)
        Me.dgriDetallePreCuenta.MainView = Me.gvdetalleprecuenta
        Me.dgriDetallePreCuenta.Margin = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.dgriDetallePreCuenta.Name = "dgriDetallePreCuenta"
        Me.dgriDetallePreCuenta.Size = New System.Drawing.Size(1592, 262)
        Me.dgriDetallePreCuenta.TabIndex = 33
        Me.dgriDetallePreCuenta.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvdetalleprecuenta})
        '
        'gvdetalleprecuenta
        '
        Me.gvdetalleprecuenta.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvdetalleprecuenta.Appearance.FocusedRow.Options.UseFont = True
        Me.gvdetalleprecuenta.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvdetalleprecuenta.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvdetalleprecuenta.DetailHeight = 437
        Me.gvdetalleprecuenta.GridControl = Me.dgriDetallePreCuenta
        Me.gvdetalleprecuenta.Name = "gvdetalleprecuenta"
        Me.gvdetalleprecuenta.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvdetalleprecuenta.OptionsEditForm.PopupEditFormWidth = 1000
        Me.gvdetalleprecuenta.OptionsView.ColumnAutoWidth = False
        Me.gvdetalleprecuenta.OptionsView.ShowFooter = True
        Me.gvdetalleprecuenta.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.xtratabPrecuenta)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 649)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1598, 296)
        Me.PanelControl1.TabIndex = 68
        '
        'frmPreFactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1598, 945)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GrpTransaccion)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Name = "frmPreFactura"
        Me.Ribbon = Me.RibbonControl
        Me.Text = "Prefactura"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpTransaccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTransaccion.ResumeLayout(False)
        Me.GrpTransaccion.PerformLayout()
        CType(Me.txtDecimales.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbErrores.ResumeLayout(False)
        CType(Me.txtScanPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPolizasPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPolizasOC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnValores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnValores.ResumeLayout(False)
        Me.pnValores.PerformLayout()
        CType(Me.dtFechaIngreso.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaSalida.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFechaSalida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPesoTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorAduana.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValorGeneral.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiasAlmacenaje.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlmacenajeDiario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalFacturacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaDocumento.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaDocumento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCliente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoCambio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridServiciosAsociados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetalleServicios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtratabPrecuenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtratabPrecuenta.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.dgriDetallePreCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvdetalleprecuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GrpTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtpFechaDocumento As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtNoDocumento As Label
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dgridServiciosAsociados As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDetalleServicios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblPolizaIngreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtFechaIngreso As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtFechaSalida As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbCliente As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtratabPrecuenta As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgriDetallePreCuenta As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvdetalleprecuenta As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblPolizaSalida As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCliente As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblConsolidador As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pnValores As DevExpress.XtraEditors.PanelControl
    Friend WithEvents dtpfechaHasta As DateTimePicker
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents lblFechaFinal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaInicial As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdProcesar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblTipoCambio As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkConsolidados As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmbPolizasOC As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbPolizasPE As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtPesoTotal As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtValorAduana As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtValorGeneral As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtDiasAlmacenaje As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtAlmacenajeDiario As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtTotalFacturacion As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtTipoCambio As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents cmdImpresionPrecuenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImpresionDetallePrecuenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtObservacion As TextBox
    Friend WithEvents lblObservacion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCorreccionPoliza As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblTipoCambioOrigen As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblCobroMoneda As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkControlPesoBruto As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkVarianteCobro As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkAgruparPorProducto As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkMantenerDolares As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents chkEnviarDolares As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents mnuPrefactura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdAnularPrefactura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup6 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkEstimacionCobro As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents txtScanPoliza As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblScanPoliza As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gbErrores As GroupBox
    Friend WithEvents lblPrg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdListadoIngresos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lbDecimales As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDecimales As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
