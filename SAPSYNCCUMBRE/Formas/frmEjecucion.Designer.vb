<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEjecucion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecucion))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.rbMain = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizarProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirGrid1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirgrid2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteEjecuciones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRptTransac = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductosI = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSolicitudTraslado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnvioPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarAjustes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTestConexion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarCodigosBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCentrosCosto = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPresentaciones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosMercancia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTrasladoDesdeSolicitud = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSolicitudTrasladoEntrada = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDevolucionProveedor = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarSolicitudDevolucion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuClientes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteComparativoWMSvrsERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCerrarDocumentoSAP = New DevExpress.XtraBars.BarButtonItem()
        Me.rpInterfaceSAP = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgDatosMaestros = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgIngresosInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgSalidasInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgEnviosInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgResumen = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mnuReportesI = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.timerProducto = New System.Windows.Forms.Timer(Me.components)
        Me.BwProducto = New System.ComponentModel.BackgroundWorker()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbMain
        '
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.mnuActualizarProveedores, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuEnviarPedidosCompra, Me.mnuEnviarPedidosTransferencia, Me.cmdRptTransac, Me.mnuProductosI, Me.BarButtonItem1, Me.mnuPedidosCompra, Me.mnuBodegas, Me.mnuSolicitudTraslado, Me.mnuEnvioPedidosCompra, Me.mnuEnviarAjustes, Me.mnuTestConexion, Me.mnuActualizarCodigosBarra, Me.mnuCentrosCosto, Me.mnuPresentaciones, Me.mnuEnviarTrasladosMercancia, Me.mnuTrasladoDesdeSolicitud, Me.mnuSolicitudTrasladoEntrada, Me.BarButtonItem4, Me.mnuDevolucionProveedor, Me.mnuEnviarSolicitudDevolucion, Me.mnuClientes, Me.mnuReporteComparativoWMSvrsERP, Me.mnuCerrarDocumentoSAP})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(4)
        Me.rbMain.MaxItemId = 65
        Me.rbMain.Name = "rbMain"
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rpInterfaceSAP, Me.RibbonPage1})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(1554, 198)
        '
        'mnuActualizarProveedores
        '
        Me.mnuActualizarProveedores.Caption = "Proveedores"
        Me.mnuActualizarProveedores.Id = 2
        Me.mnuActualizarProveedores.ImageOptions.Image = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizarProveedores.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizarProveedores.Name = "mnuActualizarProveedores"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.ActAsDropDown = True
        Me.BarButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 10
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 18
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirGrid1), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirgrid2)})
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'mnuImprimirGrid1
        '
        Me.mnuImprimirGrid1.Caption = "Imprimir_Grid_1"
        Me.mnuImprimirGrid1.Id = 19
        Me.mnuImprimirGrid1.Name = "mnuImprimirGrid1"
        '
        'mnuImprimirgrid2
        '
        Me.mnuImprimirgrid2.Caption = "Imprimir_Grid_2"
        Me.mnuImprimirgrid2.Id = 20
        Me.mnuImprimirgrid2.Name = "mnuImprimirgrid2"
        '
        'mnuReporteEjecuciones
        '
        Me.mnuReporteEjecuciones.Caption = "Reporte de Ejecuciones"
        Me.mnuReporteEjecuciones.Id = 21
        Me.mnuReporteEjecuciones.ImageOptions.Image = CType(resources.GetObject("mnuReporteEjecuciones.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuReporteEjecuciones.ImageOptions.LargeImage = CType(resources.GetObject("mnuReporteEjecuciones.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuReporteEjecuciones.Name = "mnuReporteEjecuciones"
        '
        'mnuEnviarPedidosCompra
        '
        Me.mnuEnviarPedidosCompra.Caption = "Pedidos de compra"
        Me.mnuEnviarPedidosCompra.Id = 25
        Me.mnuEnviarPedidosCompra.Name = "mnuEnviarPedidosCompra"
        '
        'mnuEnviarPedidosTransferencia
        '
        Me.mnuEnviarPedidosTransferencia.Caption = "Pedidos de transferencia"
        Me.mnuEnviarPedidosTransferencia.Id = 26
        Me.mnuEnviarPedidosTransferencia.Name = "mnuEnviarPedidosTransferencia"
        '
        'cmdRptTransac
        '
        Me.cmdRptTransac.Caption = "Datos MI3"
        Me.cmdRptTransac.Id = 32
        Me.cmdRptTransac.ImageOptions.Image = CType(resources.GetObject("cmdRptTransac.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdRptTransac.ImageOptions.LargeImage = CType(resources.GetObject("cmdRptTransac.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdRptTransac.Name = "cmdRptTransac"
        Me.cmdRptTransac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuProductosI
        '
        Me.mnuProductosI.Caption = "Productos"
        Me.mnuProductosI.Id = 33
        Me.mnuProductosI.ImageOptions.Image = CType(resources.GetObject("mnuProductosI.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuProductosI.ImageOptions.LargeImage = CType(resources.GetObject("mnuProductosI.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuProductosI.Name = "mnuProductosI"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Ajustes"
        Me.BarButtonItem1.Id = 34
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuPedidosCompra
        '
        Me.mnuPedidosCompra.Caption = "Pedidos de Compra"
        Me.mnuPedidosCompra.Id = 40
        Me.mnuPedidosCompra.ImageOptions.SvgImage = CType(resources.GetObject("mnuPedidosCompra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPedidosCompra.Name = "mnuPedidosCompra"
        '
        'mnuBodegas
        '
        Me.mnuBodegas.Caption = "Bodegas"
        Me.mnuBodegas.Id = 41
        Me.mnuBodegas.ImageOptions.SvgImage = CType(resources.GetObject("mnuBodegas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBodegas.Name = "mnuBodegas"
        '
        'mnuSolicitudTraslado
        '
        Me.mnuSolicitudTraslado.Caption = "Solicitud de Traslado (S)"
        Me.mnuSolicitudTraslado.Id = 42
        Me.mnuSolicitudTraslado.ImageOptions.SvgImage = CType(resources.GetObject("mnuSolicitudTraslado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSolicitudTraslado.Name = "mnuSolicitudTraslado"
        '
        'mnuEnvioPedidosCompra
        '
        Me.mnuEnvioPedidosCompra.Caption = "Enviar documentos de Ingreso"
        Me.mnuEnvioPedidosCompra.Id = 48
        Me.mnuEnvioPedidosCompra.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnvioPedidosCompra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnvioPedidosCompra.Name = "mnuEnvioPedidosCompra"
        '
        'mnuEnviarAjustes
        '
        Me.mnuEnviarAjustes.Caption = "Enviar ajustes"
        Me.mnuEnviarAjustes.Id = 50
        Me.mnuEnviarAjustes.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarAjustes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarAjustes.Name = "mnuEnviarAjustes"
        '
        'mnuTestConexion
        '
        Me.mnuTestConexion.Caption = "Test Conexión"
        Me.mnuTestConexion.Id = 51
        Me.mnuTestConexion.ImageOptions.SvgImage = CType(resources.GetObject("mnuTestConexion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTestConexion.Name = "mnuTestConexion"
        '
        'mnuActualizarCodigosBarra
        '
        Me.mnuActualizarCodigosBarra.Caption = "Códigos de barra"
        Me.mnuActualizarCodigosBarra.Id = 53
        Me.mnuActualizarCodigosBarra.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarCodigosBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarCodigosBarra.Name = "mnuActualizarCodigosBarra"
        '
        'mnuCentrosCosto
        '
        Me.mnuCentrosCosto.Caption = "Centros de Costo"
        Me.mnuCentrosCosto.Id = 54
        Me.mnuCentrosCosto.ImageOptions.SvgImage = CType(resources.GetObject("mnuCentrosCosto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCentrosCosto.Name = "mnuCentrosCosto"
        '
        'mnuPresentaciones
        '
        Me.mnuPresentaciones.Caption = "Presentaciones"
        Me.mnuPresentaciones.Id = 55
        Me.mnuPresentaciones.ImageOptions.SvgImage = CType(resources.GetObject("mnuPresentaciones.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPresentaciones.Name = "mnuPresentaciones"
        '
        'mnuEnviarTrasladosMercancia
        '
        Me.mnuEnviarTrasladosMercancia.Caption = "Enviar Traslados"
        Me.mnuEnviarTrasladosMercancia.Id = 56
        Me.mnuEnviarTrasladosMercancia.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarTrasladosMercancia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarTrasladosMercancia.Name = "mnuEnviarTrasladosMercancia"
        '
        'mnuTrasladoDesdeSolicitud
        '
        Me.mnuTrasladoDesdeSolicitud.Caption = "Traslados desde Solicitud (89 a 05)"
        Me.mnuTrasladoDesdeSolicitud.Id = 57
        Me.mnuTrasladoDesdeSolicitud.ImageOptions.SvgImage = CType(resources.GetObject("mnuTrasladoDesdeSolicitud.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTrasladoDesdeSolicitud.Name = "mnuTrasladoDesdeSolicitud"
        '
        'mnuSolicitudTrasladoEntrada
        '
        Me.mnuSolicitudTrasladoEntrada.Caption = "Solicitud de Traslados (E)"
        Me.mnuSolicitudTrasladoEntrada.Id = 58
        Me.mnuSolicitudTrasladoEntrada.ImageOptions.SvgImage = CType(resources.GetObject("mnuSolicitudTrasladoEntrada.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSolicitudTrasladoEntrada.Name = "mnuSolicitudTrasladoEntrada"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 59
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'mnuDevolucionProveedor
        '
        Me.mnuDevolucionProveedor.Caption = "Solicitud de devolución a proveedor"
        Me.mnuDevolucionProveedor.Id = 60
        Me.mnuDevolucionProveedor.ImageOptions.SvgImage = CType(resources.GetObject("mnuDevolucionProveedor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDevolucionProveedor.Name = "mnuDevolucionProveedor"
        Me.mnuDevolucionProveedor.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuEnviarSolicitudDevolucion
        '
        Me.mnuEnviarSolicitudDevolucion.Caption = "Devolución de proveedor"
        Me.mnuEnviarSolicitudDevolucion.Id = 61
        Me.mnuEnviarSolicitudDevolucion.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarSolicitudDevolucion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarSolicitudDevolucion.Name = "mnuEnviarSolicitudDevolucion"
        '
        'mnuClientes
        '
        Me.mnuClientes.Caption = "Clientes"
        Me.mnuClientes.Id = 62
        Me.mnuClientes.ImageOptions.SvgImage = CType(resources.GetObject("mnuClientes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuClientes.Name = "mnuClientes"
        '
        'mnuReporteComparativoWMSvrsERP
        '
        Me.mnuReporteComparativoWMSvrsERP.Caption = "Existencias comparativo WMS - SAP"
        Me.mnuReporteComparativoWMSvrsERP.Id = 63
        Me.mnuReporteComparativoWMSvrsERP.ImageOptions.SvgImage = CType(resources.GetObject("mnuReporteComparativoWMSvrsERP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReporteComparativoWMSvrsERP.Name = "mnuReporteComparativoWMSvrsERP"
        '
        'mnuCerrarDocumentoSAP
        '
        Me.mnuCerrarDocumentoSAP.Caption = "Cerrar documento SAP"
        Me.mnuCerrarDocumentoSAP.Id = 64
        Me.mnuCerrarDocumentoSAP.ImageOptions.SvgImage = CType(resources.GetObject("mnuCerrarDocumentoSAP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCerrarDocumentoSAP.Name = "mnuCerrarDocumentoSAP"
        ToolTipItem1.Text = "Se utiliza para cerrar las solicitudes de traslado en SAP"
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.mnuCerrarDocumentoSAP.SuperTip = SuperToolTip1
        '
        'rpInterfaceSAP
        '
        Me.rpInterfaceSAP.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgDatosMaestros, Me.rpgIngresosInterface, Me.rpgSalidasInterface, Me.rpgEnviosInterface, Me.rpgResumen})
        Me.rpInterfaceSAP.Name = "rpInterfaceSAP"
        Me.rpInterfaceSAP.Text = "Interface SAP"
        '
        'rpgDatosMaestros
        '
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuBodegas)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuActualizarProveedores)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuClientes)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuProductosI)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuActualizarCodigosBarra)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuCentrosCosto)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuPresentaciones)
        Me.rpgDatosMaestros.Name = "rpgDatosMaestros"
        Me.rpgDatosMaestros.Text = "Datos Maestros"
        '
        'rpgIngresosInterface
        '
        Me.rpgIngresosInterface.ItemLinks.Add(Me.mnuPedidosCompra)
        Me.rpgIngresosInterface.ItemLinks.Add(Me.mnuDevolucionProveedor)
        Me.rpgIngresosInterface.ItemLinks.Add(Me.mnuSolicitudTrasladoEntrada)
        Me.rpgIngresosInterface.Name = "rpgIngresosInterface"
        Me.rpgIngresosInterface.Text = "Ingreso"
        '
        'rpgSalidasInterface
        '
        Me.rpgSalidasInterface.ItemLinks.Add(Me.mnuSolicitudTraslado)
        Me.rpgSalidasInterface.Name = "rpgSalidasInterface"
        Me.rpgSalidasInterface.Text = "Salida"
        '
        'rpgEnviosInterface
        '
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuEnvioPedidosCompra)
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuEnviarAjustes)
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuEnviarTrasladosMercancia)
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuTrasladoDesdeSolicitud)
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuEnviarSolicitudDevolucion)
        Me.rpgEnviosInterface.ItemLinks.Add(Me.mnuCerrarDocumentoSAP)
        Me.rpgEnviosInterface.Name = "rpgEnviosInterface"
        Me.rpgEnviosInterface.Text = "Envìo"
        '
        'rpgResumen
        '
        Me.rpgResumen.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.rpgResumen.ItemLinks.Add(Me.cmdRptTransac)
        Me.rpgResumen.ItemLinks.Add(Me.mnuImprimir)
        Me.rpgResumen.ItemLinks.Add(Me.mnuTestConexion)
        Me.rpgResumen.Name = "rpgResumen"
        Me.rpgResumen.Text = "Resumen"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.mnuReportesI})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Reportes"
        '
        'mnuReportesI
        '
        Me.mnuReportesI.ItemLinks.Add(Me.mnuReporteComparativoWMSvrsERP)
        Me.mnuReportesI.Name = "mnuReportesI"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprg.Location = New System.Drawing.Point(0, 253)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1554, 501)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 225)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1554, 28)
        Me.prg.TabIndex = 1
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(0, 198)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(1554, 27)
        Me.lblTLog.TabIndex = 0
        Me.lblTLog.Text = "Log"
        '
        'timerProducto
        '
        Me.timerProducto.Interval = 1000
        '
        'BwProducto
        '
        Me.BwProducto.WorkerReportsProgress = True
        Me.BwProducto.WorkerSupportsCancellation = True
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Entrada Mercancía"
        Me.BarButtonItem3.Id = 35
        Me.BarButtonItem3.ImageOptions.Image = CType(resources.GetObject("BarButtonItem3.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem3.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 754)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.rbMain)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.rbMain
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MI3_SYNC ->TOM, WMS (vrs 20180614)"
        CType(Me.rbMain,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents rbMain As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuActualizarProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpInterfaceSAP As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpgDatosMaestros As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImprimirGrid1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimirgrid2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As ProgressBar
    Friend WithEvents BwProducto As System.ComponentModel.BackgroundWorker
    Friend WithEvents timerProducto As Timer
    Friend WithEvents mnuReporteEjecuciones As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgResumen As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdRptTransac As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductosI As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgIngresosInterface As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSolicitudTraslado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgSalidasInterface As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnvioPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgEnviosInterface As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnviarAjustes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTestConexion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarCodigosBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCentrosCosto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPresentaciones As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosMercancia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTrasladoDesdeSolicitud As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSolicitudTrasladoEntrada As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDevolucionProveedor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarSolicitudDevolucion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuClientes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents mnuReportesI As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuReporteComparativoWMSvrsERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCerrarDocumentoSAP As DevExpress.XtraBars.BarButtonItem
End Class
