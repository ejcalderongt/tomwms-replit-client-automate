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
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizarProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirGrid1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirgrid2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteEjecuciones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRptTransac = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductos = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidoCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuNotasCreditoCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDevolucionMercancia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEntradaMerncacia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnvioPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviaPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarAjustes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTestConexion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarAjustes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarInvTeoricoSAP = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteExistenciasComparativoWMSSAP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPrefactura = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSolicitudDevolucion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarSolicitudDevolucionDraft = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarDevolucion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosMercancia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteComparativo = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rgpInterfaceEjecucion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbpgEnvio = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.timerProducto = New System.Windows.Forms.Timer(Me.components)
        Me.BwProducto = New System.ComponentModel.BackgroundWorker()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 24, 26, 24)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizarProveedores, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuEnviarPedidosCompra, Me.mnuEnviarPedidosTransferencia, Me.cmdRptTransac, Me.mnuProductos, Me.BarButtonItem1, Me.mnuPedidoCliente, Me.mnuPedidosCompra, Me.mnuBodegas, Me.mnuPedidosTransferencia, Me.mnuNotasCreditoCliente, Me.mnuDevolucionMercancia, Me.mnuEntradaMerncacia, Me.mnuEnvioPedidosCompra, Me.mnuEnviaPedidosTransferencia, Me.mnuEnviarAjustes, Me.mnuTestConexion, Me.mnuImportarAjustes, Me.mnuImportarInvTeoricoSAP, Me.BarButtonItem4, Me.mnuReporteExistenciasComparativoWMSSAP, Me.mnuPrefactura, Me.mnuSolicitudDevolucion, Me.mnuEnviarSolicitudDevolucionDraft, Me.mnuEnviarDevolucion, Me.mnuEnviarTrasladosMercancia, Me.mnuReporteComparativo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 63
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 283
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1535, 158)
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
        'mnuProductos
        '
        Me.mnuProductos.Caption = "Productos"
        Me.mnuProductos.Id = 33
        Me.mnuProductos.ImageOptions.Image = CType(resources.GetObject("mnuProductos.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuProductos.ImageOptions.LargeImage = CType(resources.GetObject("mnuProductos.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuProductos.Name = "mnuProductos"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Ajustes"
        Me.BarButtonItem1.Id = 34
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mnuPedidoCliente
        '
        Me.mnuPedidoCliente.Caption = "Pedido Cliente"
        Me.mnuPedidoCliente.Id = 36
        Me.mnuPedidoCliente.ImageOptions.SvgImage = CType(resources.GetObject("mnuPedidoCliente.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPedidoCliente.Name = "mnuPedidoCliente"
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
        'mnuPedidosTransferencia
        '
        Me.mnuPedidosTransferencia.Caption = "Solicitud de Traslado"
        Me.mnuPedidosTransferencia.Id = 42
        Me.mnuPedidosTransferencia.ImageOptions.SvgImage = CType(resources.GetObject("mnuPedidosTransferencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPedidosTransferencia.Name = "mnuPedidosTransferencia"
        '
        'mnuNotasCreditoCliente
        '
        Me.mnuNotasCreditoCliente.Caption = "Notas de crédito cliente"
        Me.mnuNotasCreditoCliente.Id = 45
        Me.mnuNotasCreditoCliente.Name = "mnuNotasCreditoCliente"
        Me.mnuNotasCreditoCliente.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuDevolucionMercancia
        '
        Me.mnuDevolucionMercancia.Caption = "Devolución de Cliente"
        Me.mnuDevolucionMercancia.Id = 46
        Me.mnuDevolucionMercancia.ImageOptions.SvgImage = CType(resources.GetObject("mnuDevolucionMercancia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDevolucionMercancia.Name = "mnuDevolucionMercancia"
        '
        'mnuEntradaMerncacia
        '
        Me.mnuEntradaMerncacia.Caption = "Entrada de Mercancía"
        Me.mnuEntradaMerncacia.Id = 47
        Me.mnuEntradaMerncacia.ImageOptions.SvgImage = CType(resources.GetObject("mnuEntradaMerncacia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEntradaMerncacia.Name = "mnuEntradaMerncacia"
        Me.mnuEntradaMerncacia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuEnvioPedidosCompra
        '
        Me.mnuEnvioPedidosCompra.Caption = "Enviar documentos de Ingreso"
        Me.mnuEnvioPedidosCompra.Id = 48
        Me.mnuEnvioPedidosCompra.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnvioPedidosCompra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnvioPedidosCompra.Name = "mnuEnvioPedidosCompra"
        '
        'mnuEnviaPedidosTransferencia
        '
        Me.mnuEnviaPedidosTransferencia.Caption = "Enviar documentos de Salida"
        Me.mnuEnviaPedidosTransferencia.Id = 49
        Me.mnuEnviaPedidosTransferencia.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviaPedidosTransferencia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviaPedidosTransferencia.Name = "mnuEnviaPedidosTransferencia"
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
        'mnuImportarAjustes
        '
        Me.mnuImportarAjustes.Caption = "Importar Ajustes"
        Me.mnuImportarAjustes.Id = 52
        Me.mnuImportarAjustes.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarAjustes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarAjustes.Name = "mnuImportarAjustes"
        '
        'mnuImportarInvTeoricoSAP
        '
        Me.mnuImportarInvTeoricoSAP.Caption = "Importar Inv. Teórico"
        Me.mnuImportarInvTeoricoSAP.Id = 54
        Me.mnuImportarInvTeoricoSAP.ImageOptions.SvgImage = CType(resources.GetObject("mnuImportarInvTeoricoSAP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImportarInvTeoricoSAP.Name = "mnuImportarInvTeoricoSAP"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 55
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'mnuReporteExistenciasComparativoWMSSAP
        '
        Me.mnuReporteExistenciasComparativoWMSSAP.Caption = "Existencias comparativo WMS - SAP"
        Me.mnuReporteExistenciasComparativoWMSSAP.Id = 56
        Me.mnuReporteExistenciasComparativoWMSSAP.ImageOptions.SvgImage = CType(resources.GetObject("mnuReporteExistenciasComparativoWMSSAP.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReporteExistenciasComparativoWMSSAP.Name = "mnuReporteExistenciasComparativoWMSSAP"
        '
        'mnuPrefactura
        '
        Me.mnuPrefactura.Caption = "Factura Reserva"
        Me.mnuPrefactura.Id = 57
        Me.mnuPrefactura.ImageOptions.SvgImage = CType(resources.GetObject("mnuPrefactura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuPrefactura.Name = "mnuPrefactura"
        Me.mnuPrefactura.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuSolicitudDevolucion
        '
        Me.mnuSolicitudDevolucion.Caption = "Devolución de proveedor"
        Me.mnuSolicitudDevolucion.Id = 58
        Me.mnuSolicitudDevolucion.ImageOptions.SvgImage = CType(resources.GetObject("mnuSolicitudDevolucion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSolicitudDevolucion.Name = "mnuSolicitudDevolucion"
        '
        'mnuEnviarSolicitudDevolucionDraft
        '
        Me.mnuEnviarSolicitudDevolucionDraft.Caption = "Enviar Solicitud de devolución"
        Me.mnuEnviarSolicitudDevolucionDraft.Id = 59
        Me.mnuEnviarSolicitudDevolucionDraft.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarSolicitudDevolucionDraft.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarSolicitudDevolucionDraft.Name = "mnuEnviarSolicitudDevolucionDraft"
        '
        'mnuEnviarDevolucion
        '
        Me.mnuEnviarDevolucion.Caption = "Enviar Devolución proveedor"
        Me.mnuEnviarDevolucion.Id = 60
        Me.mnuEnviarDevolucion.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarDevolucion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarDevolucion.Name = "mnuEnviarDevolucion"
        '
        'mnuEnviarTrasladosMercancia
        '
        Me.mnuEnviarTrasladosMercancia.Caption = "Enviar Traslados"
        Me.mnuEnviarTrasladosMercancia.Id = 61
        Me.mnuEnviarTrasladosMercancia.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarTrasladosMercancia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarTrasladosMercancia.Name = "mnuEnviarTrasladosMercancia"
        '
        'mnuReporteComparativo
        '
        Me.mnuReporteComparativo.Caption = "Comparativo por área WMS"
        Me.mnuReporteComparativo.Id = 62
        Me.mnuReporteComparativo.ImageOptions.SvgImage = CType(resources.GetObject("mnuReporteComparativo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReporteComparativo.Name = "mnuReporteComparativo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rgpInterfaceEjecucion, Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.rbpgEnvio, Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Interface"
        '
        'rgpInterfaceEjecucion
        '
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuBodegas)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuActualizarProveedores)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuProductos)
        Me.rgpInterfaceEjecucion.Name = "rgpInterfaceEjecucion"
        Me.rgpInterfaceEjecucion.Text = "Datos Maestros"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuPedidosCompra)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuDevolucionMercancia)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuNotasCreditoCliente)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuEntradaMerncacia)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuImportarAjustes)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuPrefactura)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Ingreso"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuPedidoCliente)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuSolicitudDevolucion)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuPedidosTransferencia)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Salida"
        '
        'rbpgEnvio
        '
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnvioPedidosCompra)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviaPedidosTransferencia)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarAjustes)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarDevolucion)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarTrasladosMercancia)
        Me.rbpgEnvio.Name = "rbpgEnvio"
        Me.rbpgEnvio.Text = "Envìo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdRptTransac)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuTestConexion)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImportarInvTeoricoSAP)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup4})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Reportes"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuReporteExistenciasComparativoWMSSAP)
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuReporteComparativo)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprg.Location = New System.Drawing.Point(0, 203)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1535, 410)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 180)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1535, 23)
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
        Me.lblTLog.Location = New System.Drawing.Point(0, 158)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(1535, 22)
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1535, 613)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MI3_SYNC ->TOM, WMS (vrs 20180614)"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuActualizarProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rgpInterfaceEjecucion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
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
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdRptTransac As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidoCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuNotasCreditoCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDevolucionMercancia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEntradaMerncacia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnvioPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbpgEnvio As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnviaPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarAjustes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTestConexion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImportarAjustes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImportarInvTeoricoSAP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReporteExistenciasComparativoWMSSAP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuPrefactura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSolicitudDevolucion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarSolicitudDevolucionDraft As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarDevolucion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosMercancia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuReporteComparativo As DevExpress.XtraBars.BarButtonItem
End Class
