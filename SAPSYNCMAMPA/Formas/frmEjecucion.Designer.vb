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
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarCodigosBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuClientes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteComparativoWMSvrsERP = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTallas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuColores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuFacturaReservaMenu = New DevExpress.XtraBars.BarSubItem()
        Me.mnuRecibirFacturaReservaProv = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarFacturaReservaIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuRecibirTrasladosCedis = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosCedis = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSolDevolProveedorMenu = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImportarSolDevolProv = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarSolDevolProv = New DevExpress.XtraBars.BarButtonItem()
        Me.rpInterfaceSAP = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpgDatosMaestros = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgIngresosInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgSalidasInterface = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpgResumen = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mnuReportesI = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.timerProducto = New System.Windows.Forms.Timer(Me.components)
        Me.BwProducto = New System.ComponentModel.BackgroundWorker()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuRecibirTrasladosTienda = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosTienda = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.rbMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbMain
        '
        Me.rbMain.ExpandCollapseItem.Id = 0
        Me.rbMain.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.rbMain.ExpandCollapseItem, Me.mnuActualizarProveedores, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuEnviarPedidosCompra, Me.mnuEnviarPedidosTransferencia, Me.cmdRptTransac, Me.mnuProductosI, Me.BarButtonItem1, Me.mnuBodegas, Me.mnuActualizarCodigosBarra, Me.BarButtonItem4, Me.mnuClientes, Me.mnuReporteComparativoWMSvrsERP, Me.mnuTallas, Me.mnuColores, Me.mnuFacturaReservaMenu, Me.mnuRecibirFacturaReservaProv, Me.mnuEnviarFacturaReservaIngreso, Me.BarSubItem1, Me.mnuRecibirTrasladosCedis, Me.mnuEnviarTrasladosCedis, Me.mnuSolDevolProveedorMenu, Me.mnuImportarSolDevolProv, Me.mnuEnviarSolDevolProv, Me.BarSubItem2, Me.mnuRecibirTrasladosTienda, Me.mnuEnviarTrasladosTienda})
        Me.rbMain.Location = New System.Drawing.Point(0, 0)
        Me.rbMain.Margin = New System.Windows.Forms.Padding(4)
        Me.rbMain.MaxItemId = 81
        Me.rbMain.Name = "rbMain"
        Me.rbMain.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rpInterfaceSAP, Me.RibbonPage1})
        Me.rbMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.rbMain.Size = New System.Drawing.Size(1749, 193)
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
        'mnuBodegas
        '
        Me.mnuBodegas.Caption = "Bodegas"
        Me.mnuBodegas.Id = 41
        Me.mnuBodegas.ImageOptions.SvgImage = CType(resources.GetObject("mnuBodegas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBodegas.Name = "mnuBodegas"
        '
        'mnuActualizarCodigosBarra
        '
        Me.mnuActualizarCodigosBarra.Caption = "Códigos de barra"
        Me.mnuActualizarCodigosBarra.Id = 53
        Me.mnuActualizarCodigosBarra.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarCodigosBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarCodigosBarra.Name = "mnuActualizarCodigosBarra"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 59
        Me.BarButtonItem4.Name = "BarButtonItem4"
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
        'mnuTallas
        '
        Me.mnuTallas.Caption = "Tallas"
        Me.mnuTallas.Id = 65
        Me.mnuTallas.ImageOptions.SvgImage = CType(resources.GetObject("mnuTallas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTallas.Name = "mnuTallas"
        '
        'mnuColores
        '
        Me.mnuColores.Caption = "Colores"
        Me.mnuColores.Id = 66
        Me.mnuColores.ImageOptions.SvgImage = CType(resources.GetObject("mnuColores.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuColores.Name = "mnuColores"
        '
        'mnuFacturaReservaMenu
        '
        Me.mnuFacturaReservaMenu.Caption = "Factura de Reserva (Ingreso)"
        Me.mnuFacturaReservaMenu.Id = 69
        Me.mnuFacturaReservaMenu.ImageOptions.SvgImage = CType(resources.GetObject("mnuFacturaReservaMenu.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuFacturaReservaMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRecibirFacturaReservaProv), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarFacturaReservaIngreso)})
        Me.mnuFacturaReservaMenu.Name = "mnuFacturaReservaMenu"
        '
        'mnuRecibirFacturaReservaProv
        '
        Me.mnuRecibirFacturaReservaProv.Caption = "Recibir"
        Me.mnuRecibirFacturaReservaProv.Id = 70
        Me.mnuRecibirFacturaReservaProv.Name = "mnuRecibirFacturaReservaProv"
        '
        'mnuEnviarFacturaReservaIngreso
        '
        Me.mnuEnviarFacturaReservaIngreso.Caption = "Enviar"
        Me.mnuEnviarFacturaReservaIngreso.Id = 71
        Me.mnuEnviarFacturaReservaIngreso.Name = "mnuEnviarFacturaReservaIngreso"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Traslados Cedis"
        Me.BarSubItem1.Id = 72
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRecibirTrasladosCedis), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarTrasladosCedis)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuRecibirTrasladosCedis
        '
        Me.mnuRecibirTrasladosCedis.Caption = "Recibir Traslados"
        Me.mnuRecibirTrasladosCedis.Id = 73
        Me.mnuRecibirTrasladosCedis.Name = "mnuRecibirTrasladosCedis"
        '
        'mnuEnviarTrasladosCedis
        '
        Me.mnuEnviarTrasladosCedis.Caption = "Enviar Traslados"
        Me.mnuEnviarTrasladosCedis.Id = 74
        Me.mnuEnviarTrasladosCedis.Name = "mnuEnviarTrasladosCedis"
        '
        'mnuSolDevolProveedorMenu
        '
        Me.mnuSolDevolProveedorMenu.Caption = "Sol. Devol. Proveedor"
        Me.mnuSolDevolProveedorMenu.Id = 75
        Me.mnuSolDevolProveedorMenu.ImageOptions.SvgImage = CType(resources.GetObject("mnuSolDevolProveedorMenu.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSolDevolProveedorMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImportarSolDevolProv), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarSolDevolProv)})
        Me.mnuSolDevolProveedorMenu.Name = "mnuSolDevolProveedorMenu"
        '
        'mnuImportarSolDevolProv
        '
        Me.mnuImportarSolDevolProv.Caption = "Improtar Sol. Devol. Prov"
        Me.mnuImportarSolDevolProv.Id = 76
        Me.mnuImportarSolDevolProv.Name = "mnuImportarSolDevolProv"
        '
        'mnuEnviarSolDevolProv
        '
        Me.mnuEnviarSolDevolProv.Caption = "Enviar Sol. Devol. Prov"
        Me.mnuEnviarSolDevolProv.Id = 77
        Me.mnuEnviarSolDevolProv.Name = "mnuEnviarSolDevolProv"
        '
        'rpInterfaceSAP
        '
        Me.rpInterfaceSAP.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgDatosMaestros, Me.rpgIngresosInterface, Me.rpgSalidasInterface, Me.rpgResumen})
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
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuTallas)
        Me.rpgDatosMaestros.ItemLinks.Add(Me.mnuColores)
        Me.rpgDatosMaestros.Name = "rpgDatosMaestros"
        Me.rpgDatosMaestros.Text = "Datos Maestros"
        '
        'rpgIngresosInterface
        '
        Me.rpgIngresosInterface.ItemLinks.Add(Me.mnuFacturaReservaMenu)
        Me.rpgIngresosInterface.Name = "rpgIngresosInterface"
        Me.rpgIngresosInterface.Text = "Ingreso"
        '
        'rpgSalidasInterface
        '
        Me.rpgSalidasInterface.ItemLinks.Add(Me.BarSubItem1)
        Me.rpgSalidasInterface.ItemLinks.Add(Me.mnuSolDevolProveedorMenu)
        Me.rpgSalidasInterface.ItemLinks.Add(Me.BarSubItem2)
        Me.rpgSalidasInterface.Name = "rpgSalidasInterface"
        Me.rpgSalidasInterface.Text = "Transacciones"
        '
        'rpgResumen
        '
        Me.rpgResumen.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.rpgResumen.ItemLinks.Add(Me.cmdRptTransac)
        Me.rpgResumen.ItemLinks.Add(Me.mnuImprimir)
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
        Me.lblprg.Location = New System.Drawing.Point(0, 248)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1749, 506)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 220)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1749, 28)
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
        Me.lblTLog.Location = New System.Drawing.Point(0, 193)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(1749, 27)
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
        'BarSubItem2
        '
        Me.BarSubItem2.Caption = "Traslados Tienda"
        Me.BarSubItem2.Id = 78
        Me.BarSubItem2.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuRecibirTrasladosTienda), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarTrasladosTienda)})
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'mnuRecibirTrasladosTienda
        '
        Me.mnuRecibirTrasladosTienda.Caption = "Recibir"
        Me.mnuRecibirTrasladosTienda.Id = 79
        Me.mnuRecibirTrasladosTienda.Name = "mnuRecibirTrasladosTienda"
        '
        'mnuEnviarTrasladosTienda
        '
        Me.mnuEnviarTrasladosTienda.Caption = "Enviar"
        Me.mnuEnviarTrasladosTienda.Id = 80
        Me.mnuEnviarTrasladosTienda.Name = "mnuEnviarTrasladosTienda"
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1749, 754)
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
    Friend WithEvents rpgIngresosInterface As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpgSalidasInterface As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuActualizarCodigosBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuClientes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents mnuReportesI As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuReporteComparativoWMSvrsERP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTallas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuColores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuFacturaReservaMenu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuRecibirFacturaReservaProv As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarFacturaReservaIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuRecibirTrasladosCedis As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosCedis As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSolDevolProveedorMenu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImportarSolDevolProv As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarSolDevolProv As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem2 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuRecibirTrasladosTienda As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosTienda As DevExpress.XtraBars.BarButtonItem
End Class
