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
        Me.cmdRptTransac = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductosI = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidoCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSolicitudTraslado = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTransferenciaStock = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDevolucionMercancia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnvioPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviaPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarAjustes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTestConexion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductoPresentacion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDevolucionCliente = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosProrrateo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuTransferenciaIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarDevolProveedor = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarTrasladosDesdeSol = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarCodigosBarra = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSyncClientes = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rgpInterfaceEjecucion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rbpgEnvio = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
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
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuActualizarProveedores, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuEnviarPedidosCompra, Me.cmdRptTransac, Me.mnuProductosI, Me.BarButtonItem1, Me.mnuPedidoCliente, Me.mnuPedidosCompra, Me.mnuBodegas, Me.mnuSolicitudTraslado, Me.mnuTransferenciaStock, Me.mnuDevolucionMercancia, Me.mnuEnvioPedidosCompra, Me.mnuEnviaPedidosTransferencia, Me.mnuEnviarAjustes, Me.mnuTestConexion, Me.mnuProductoPresentacion, Me.mnuDevolucionCliente, Me.mnuEnviarTrasladosProrrateo, Me.mnuTransferenciaIngreso, Me.BarButtonItem4, Me.mnuEnviarDevolProveedor, Me.mnuEnviarTrasladosDesdeSol, Me.mnuActualizarCodigosBarra, Me.cmdSyncClientes})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 61
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1760, 193)
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
        'mnuSolicitudTraslado
        '
        Me.mnuSolicitudTraslado.Caption = "Solicitudes de Traslado"
        Me.mnuSolicitudTraslado.Id = 42
        Me.mnuSolicitudTraslado.ImageOptions.SvgImage = CType(resources.GetObject("mnuSolicitudTraslado.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuSolicitudTraslado.Name = "mnuSolicitudTraslado"
        '
        'mnuTransferenciaStock
        '
        Me.mnuTransferenciaStock.Caption = "Transferencia de Stock"
        Me.mnuTransferenciaStock.Id = 43
        Me.mnuTransferenciaStock.ImageOptions.SvgImage = CType(resources.GetObject("mnuTransferenciaStock.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTransferenciaStock.Name = "mnuTransferenciaStock"
        Me.mnuTransferenciaStock.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuDevolucionMercancia
        '
        Me.mnuDevolucionMercancia.Caption = "Devolución a Proveedor"
        Me.mnuDevolucionMercancia.Id = 46
        Me.mnuDevolucionMercancia.ImageOptions.SvgImage = CType(resources.GetObject("mnuDevolucionMercancia.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDevolucionMercancia.Name = "mnuDevolucionMercancia"
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
        'mnuProductoPresentacion
        '
        Me.mnuProductoPresentacion.Caption = "Presentacion"
        Me.mnuProductoPresentacion.Hint = "Factores de presentación y presentaciones"
        Me.mnuProductoPresentacion.Id = 52
        Me.mnuProductoPresentacion.ImageOptions.SvgImage = CType(resources.GetObject("mnuProductoPresentacion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProductoPresentacion.Name = "mnuProductoPresentacion"
        '
        'mnuDevolucionCliente
        '
        Me.mnuDevolucionCliente.Caption = "Devolución de cliente"
        Me.mnuDevolucionCliente.Id = 53
        Me.mnuDevolucionCliente.ImageOptions.SvgImage = CType(resources.GetObject("mnuDevolucionCliente.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDevolucionCliente.Name = "mnuDevolucionCliente"
        '
        'mnuEnviarTrasladosProrrateo
        '
        Me.mnuEnviarTrasladosProrrateo.Caption = "Enviar Traslados Prorrateo"
        Me.mnuEnviarTrasladosProrrateo.Id = 54
        Me.mnuEnviarTrasladosProrrateo.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarTrasladosProrrateo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarTrasladosProrrateo.Name = "mnuEnviarTrasladosProrrateo"
        '
        'mnuTransferenciaIngreso
        '
        Me.mnuTransferenciaIngreso.Caption = "Transferencias de ingreso"
        Me.mnuTransferenciaIngreso.Id = 55
        Me.mnuTransferenciaIngreso.ImageOptions.SvgImage = CType(resources.GetObject("mnuTransferenciaIngreso.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuTransferenciaIngreso.Name = "mnuTransferenciaIngreso"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 56
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'mnuEnviarDevolProveedor
        '
        Me.mnuEnviarDevolProveedor.Caption = "Enviar Devolución de Proveedor"
        Me.mnuEnviarDevolProveedor.Id = 57
        Me.mnuEnviarDevolProveedor.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarDevolProveedor.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarDevolProveedor.Name = "mnuEnviarDevolProveedor"
        '
        'mnuEnviarTrasladosDesdeSol
        '
        Me.mnuEnviarTrasladosDesdeSol.Caption = "Enviar Solicitudes de Traslado (BOD8)"
        Me.mnuEnviarTrasladosDesdeSol.Id = 58
        Me.mnuEnviarTrasladosDesdeSol.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarTrasladosDesdeSol.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarTrasladosDesdeSol.Name = "mnuEnviarTrasladosDesdeSol"
        '
        'mnuActualizarCodigosBarra
        '
        Me.mnuActualizarCodigosBarra.Caption = "Códigos de barra"
        Me.mnuActualizarCodigosBarra.Id = 59
        Me.mnuActualizarCodigosBarra.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarCodigosBarra.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarCodigosBarra.Name = "mnuActualizarCodigosBarra"
        '
        'cmdSyncClientes
        '
        Me.cmdSyncClientes.Caption = "Clientes"
        Me.cmdSyncClientes.Id = 60
        Me.cmdSyncClientes.ImageOptions.SvgImage = CType(resources.GetObject("cmdSyncClientes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSyncClientes.Name = "cmdSyncClientes"
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
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuProductosI)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuProductoPresentacion)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuActualizarCodigosBarra)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.cmdSyncClientes)
        Me.rgpInterfaceEjecucion.Name = "rgpInterfaceEjecucion"
        Me.rgpInterfaceEjecucion.Text = "Datos Maestros"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuPedidosCompra)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuDevolucionCliente)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuTransferenciaIngreso)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Ingreso"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuPedidoCliente)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuTransferenciaStock)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuSolicitudTraslado)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuDevolucionMercancia)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Salida"
        '
        'rbpgEnvio
        '
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnvioPedidosCompra)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviaPedidosTransferencia)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarAjustes)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarTrasladosProrrateo)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarDevolProveedor)
        Me.rbpgEnvio.ItemLinks.Add(Me.mnuEnviarTrasladosDesdeSol)
        Me.rbpgEnvio.Name = "rbpgEnvio"
        Me.rbpgEnvio.Text = "Envìo"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdRptTransac)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuImprimir)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuTestConexion)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprg.Location = New System.Drawing.Point(0, 262)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.ReadOnly = True
        Me.lblprg.Size = New System.Drawing.Size(1760, 492)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 244)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1760, 18)
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
        Me.lblTLog.Size = New System.Drawing.Size(1760, 51)
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
        Me.ClientSize = New System.Drawing.Size(1760, 754)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SAP ->TOMWMS (vrs 20180614) - Killio/Garesa"
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
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdRptTransac As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductosI As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidoCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSolicitudTraslado As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTransferenciaStock As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDevolucionMercancia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnvioPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rbpgEnvio As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuEnviaPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarAjustes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTestConexion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductoPresentacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDevolucionCliente As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosProrrateo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuTransferenciaIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarDevolProveedor As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarTrasladosDesdeSol As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarCodigosBarra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSyncClientes As DevExpress.XtraBars.BarButtonItem
End Class
