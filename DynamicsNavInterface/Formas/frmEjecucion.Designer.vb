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
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidoCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirGrid1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirgrid2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteEjecuciones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuConversiones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarDatos = New DevExpress.XtraBars.BarSubItem()
        Me.mnuEnviarPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosVenta = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdEntidad = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSyncLotes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuRecibirPedidosTransfINgreso = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarAjustes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductosI = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuClientes = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuOrdenesProduccion = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuPedidosDeVenta = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnvios = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDevolucionVenta = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.mnuDatosMI3 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDatosPendPush = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarDatosPendientesPush = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCentrosCosto = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminarEnvio = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rpg1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpg2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpg3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpg4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.rpg5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.timerProducto = New System.Windows.Forms.Timer(Me.components)
        Me.BwProducto = New System.ComponentModel.BackgroundWorker()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblprgIngresos = New System.Windows.Forms.RichTextBox()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuBodegas, Me.mnuActualizarProveedores, Me.mnuPedidosTransferencia, Me.mnuPedidoCompra, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuConversiones, Me.mnuEnviarDatos, Me.mnuEnviarPedidosCompra, Me.mnuEnviarPedidosTransferencia, Me.cmdEntidad, Me.mnuSyncLotes, Me.mnuRecibirPedidosTransfINgreso, Me.mnuEnviarAjustes, Me.mnuProductosI, Me.mnuClientes, Me.mnuOrdenesProduccion, Me.mnuPedidosDeVenta, Me.mnuEnvios, Me.mnuDevolucionVenta, Me.BarSubItem1, Me.mnuDatosMI3, Me.mnuDatosPendPush, Me.mnuEnviarDatosPendientesPush, Me.mnuCentrosCosto, Me.mnuEnviarPedidosVenta, Me.mnuEliminarEnvio})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 46
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1554, 193)
        '
        'mnuBodegas
        '
        Me.mnuBodegas.Caption = "Bodegas"
        Me.mnuBodegas.Id = 1
        Me.mnuBodegas.ImageOptions.Image = CType(resources.GetObject("mnuBodegas.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuBodegas.ImageOptions.LargeImage = CType(resources.GetObject("mnuBodegas.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuBodegas.Name = "mnuBodegas"
        '
        'mnuActualizarProveedores
        '
        Me.mnuActualizarProveedores.Caption = "Proveedores"
        Me.mnuActualizarProveedores.Id = 2
        Me.mnuActualizarProveedores.ImageOptions.Image = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizarProveedores.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizarProveedores.Name = "mnuActualizarProveedores"
        '
        'mnuPedidosTransferencia
        '
        Me.mnuPedidosTransferencia.Caption = "Pedidos transferencia envío"
        Me.mnuPedidosTransferencia.Id = 4
        Me.mnuPedidosTransferencia.ImageOptions.Image = CType(resources.GetObject("mnuPedidosTransferencia.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuPedidosTransferencia.ImageOptions.LargeImage = CType(resources.GetObject("mnuPedidosTransferencia.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuPedidosTransferencia.Name = "mnuPedidosTransferencia"
        '
        'mnuPedidoCompra
        '
        Me.mnuPedidoCompra.Caption = "Pedidos de compra"
        Me.mnuPedidoCompra.Id = 5
        Me.mnuPedidoCompra.ImageOptions.Image = CType(resources.GetObject("mnuPedidoCompra.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuPedidoCompra.ImageOptions.LargeImage = CType(resources.GetObject("mnuPedidoCompra.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuPedidoCompra.Name = "mnuPedidoCompra"
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
        Me.mnuImprimir.ImageOptions.Image = CType(resources.GetObject("mnuImprimir.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuImprimir.ImageOptions.LargeImage = CType(resources.GetObject("mnuImprimir.ImageOptions.LargeImage"), System.Drawing.Image)
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
        'mnuConversiones
        '
        Me.mnuConversiones.Caption = "Conversiones"
        Me.mnuConversiones.Id = 22
        Me.mnuConversiones.ImageOptions.Image = CType(resources.GetObject("mnuConversiones.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuConversiones.ImageOptions.LargeImage = CType(resources.GetObject("mnuConversiones.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuConversiones.Name = "mnuConversiones"
        '
        'mnuEnviarDatos
        '
        Me.mnuEnviarDatos.Caption = "Enviar datos a Nav"
        Me.mnuEnviarDatos.Id = 24
        Me.mnuEnviarDatos.ImageOptions.Image = CType(resources.GetObject("mnuEnviarDatos.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEnviarDatos.ImageOptions.LargeImage = CType(resources.GetObject("mnuEnviarDatos.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEnviarDatos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarPedidosCompra), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarPedidosTransferencia), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarPedidosVenta)})
        Me.mnuEnviarDatos.Name = "mnuEnviarDatos"
        '
        'mnuEnviarPedidosCompra
        '
        Me.mnuEnviarPedidosCompra.Caption = "Pedidos de transferencia de ingreso"
        Me.mnuEnviarPedidosCompra.Id = 25
        Me.mnuEnviarPedidosCompra.Name = "mnuEnviarPedidosCompra"
        '
        'mnuEnviarPedidosTransferencia
        '
        Me.mnuEnviarPedidosTransferencia.Caption = "Pedidos de transferencia de salida"
        Me.mnuEnviarPedidosTransferencia.Id = 26
        Me.mnuEnviarPedidosTransferencia.Name = "mnuEnviarPedidosTransferencia"
        '
        'mnuEnviarPedidosVenta
        '
        Me.mnuEnviarPedidosVenta.Caption = "Pedidos de venta"
        Me.mnuEnviarPedidosVenta.Id = 44
        Me.mnuEnviarPedidosVenta.Name = "mnuEnviarPedidosVenta"
        '
        'cmdEntidad
        '
        Me.cmdEntidad.Caption = "Filtros de importación"
        Me.cmdEntidad.Id = 27
        Me.cmdEntidad.ImageOptions.Image = CType(resources.GetObject("cmdEntidad.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdEntidad.ImageOptions.LargeImage = CType(resources.GetObject("cmdEntidad.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdEntidad.Name = "cmdEntidad"
        '
        'mnuSyncLotes
        '
        Me.mnuSyncLotes.Caption = "Test Lotes"
        Me.mnuSyncLotes.Id = 29
        Me.mnuSyncLotes.ImageOptions.Image = CType(resources.GetObject("mnuSyncLotes.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuSyncLotes.ImageOptions.LargeImage = CType(resources.GetObject("mnuSyncLotes.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuSyncLotes.Name = "mnuSyncLotes"
        Me.mnuSyncLotes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuRecibirPedidosTransfINgreso
        '
        Me.mnuRecibirPedidosTransfINgreso.Caption = "Recibir pedidos de transferencia ingreso"
        Me.mnuRecibirPedidosTransfINgreso.Id = 30
        Me.mnuRecibirPedidosTransfINgreso.ImageOptions.Image = CType(resources.GetObject("mnuRecibirPedidosTransfINgreso.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuRecibirPedidosTransfINgreso.ImageOptions.LargeImage = CType(resources.GetObject("mnuRecibirPedidosTransfINgreso.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuRecibirPedidosTransfINgreso.Name = "mnuRecibirPedidosTransfINgreso"
        '
        'mnuEnviarAjustes
        '
        Me.mnuEnviarAjustes.Caption = "Enviar ajustes"
        Me.mnuEnviarAjustes.Id = 31
        Me.mnuEnviarAjustes.ImageOptions.Image = CType(resources.GetObject("mnuEnviarAjustes.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEnviarAjustes.ImageOptions.LargeImage = CType(resources.GetObject("mnuEnviarAjustes.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEnviarAjustes.Name = "mnuEnviarAjustes"
        '
        'mnuProductosI
        '
        Me.mnuProductosI.Caption = "Productos"
        Me.mnuProductosI.Id = 33
        Me.mnuProductosI.ImageOptions.Image = CType(resources.GetObject("mnuProductosI.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuProductosI.ImageOptions.LargeImage = CType(resources.GetObject("mnuProductosI.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuProductosI.Name = "mnuProductosI"
        '
        'mnuClientes
        '
        Me.mnuClientes.Caption = "Clientes"
        Me.mnuClientes.Id = 34
        Me.mnuClientes.ImageOptions.Image = CType(resources.GetObject("mnuClientes.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuClientes.ImageOptions.LargeImage = CType(resources.GetObject("mnuClientes.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuClientes.Name = "mnuClientes"
        '
        'mnuOrdenesProduccion
        '
        Me.mnuOrdenesProduccion.Caption = "Órdenes de Producción"
        Me.mnuOrdenesProduccion.Id = 35
        Me.mnuOrdenesProduccion.ImageOptions.Image = CType(resources.GetObject("mnuOrdenesProduccion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuOrdenesProduccion.ImageOptions.LargeImage = CType(resources.GetObject("mnuOrdenesProduccion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuOrdenesProduccion.Name = "mnuOrdenesProduccion"
        '
        'mnuPedidosDeVenta
        '
        Me.mnuPedidosDeVenta.Caption = "Pedidos de Venta"
        Me.mnuPedidosDeVenta.Id = 36
        Me.mnuPedidosDeVenta.ImageOptions.Image = CType(resources.GetObject("mnuPedidosDeVenta.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuPedidosDeVenta.ImageOptions.LargeImage = CType(resources.GetObject("mnuPedidosDeVenta.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuPedidosDeVenta.Name = "mnuPedidosDeVenta"
        '
        'mnuEnvios
        '
        Me.mnuEnvios.Caption = "Envíos Almacén"
        Me.mnuEnvios.Id = 37
        Me.mnuEnvios.ImageOptions.Image = CType(resources.GetObject("mnuEnvios.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuEnvios.ImageOptions.LargeImage = CType(resources.GetObject("mnuEnvios.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuEnvios.Name = "mnuEnvios"
        '
        'mnuDevolucionVenta
        '
        Me.mnuDevolucionVenta.Caption = "Devolución Venta"
        Me.mnuDevolucionVenta.Id = 38
        Me.mnuDevolucionVenta.ImageOptions.SvgImage = CType(resources.GetObject("mnuDevolucionVenta.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuDevolucionVenta.Name = "mnuDevolucionVenta"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Datos MI3"
        Me.BarSubItem1.Id = 39
        Me.BarSubItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarSubItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuDatosMI3), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuDatosPendPush)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'mnuDatosMI3
        '
        Me.mnuDatosMI3.Caption = "Datos MI3"
        Me.mnuDatosMI3.Id = 40
        Me.mnuDatosMI3.Name = "mnuDatosMI3"
        '
        'mnuDatosPendPush
        '
        Me.mnuDatosPendPush.Caption = "Datos pendientes de Push"
        Me.mnuDatosPendPush.Id = 41
        Me.mnuDatosPendPush.Name = "mnuDatosPendPush"
        '
        'mnuEnviarDatosPendientesPush
        '
        Me.mnuEnviarDatosPendientesPush.Caption = "Enviar datos pendientes de push"
        Me.mnuEnviarDatosPendientesPush.Id = 42
        Me.mnuEnviarDatosPendientesPush.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarDatosPendientesPush.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarDatosPendientesPush.Name = "mnuEnviarDatosPendientesPush"
        '
        'mnuCentrosCosto
        '
        Me.mnuCentrosCosto.Caption = "Centros de Costo"
        Me.mnuCentrosCosto.Id = 43
        Me.mnuCentrosCosto.ImageOptions.SvgImage = CType(resources.GetObject("mnuCentrosCosto.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCentrosCosto.Name = "mnuCentrosCosto"
        '
        'mnuEliminarEnvio
        '
        Me.mnuEliminarEnvio.Caption = "Eliminar Envío"
        Me.mnuEliminarEnvio.Id = 45
        Me.mnuEliminarEnvio.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminarEnvio.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminarEnvio.Name = "mnuEliminarEnvio"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpg1, Me.rpg2, Me.rpg3, Me.rpg4, Me.rpg5})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Interface"
        '
        'rpg1
        '
        Me.rpg1.ItemLinks.Add(Me.cmdEntidad)
        Me.rpg1.ItemLinks.Add(Me.mnuConversiones)
        Me.rpg1.ItemLinks.Add(Me.mnuImprimir)
        Me.rpg1.Name = "rpg1"
        '
        'rpg2
        '
        Me.rpg2.ItemLinks.Add(Me.mnuBodegas)
        Me.rpg2.ItemLinks.Add(Me.mnuActualizarProveedores)
        Me.rpg2.ItemLinks.Add(Me.mnuClientes)
        Me.rpg2.ItemLinks.Add(Me.mnuProductosI)
        Me.rpg2.ItemLinks.Add(Me.mnuCentrosCosto)
        Me.rpg2.Name = "rpg2"
        '
        'rpg3
        '
        Me.rpg3.ItemLinks.Add(Me.mnuPedidoCompra)
        Me.rpg3.ItemLinks.Add(Me.mnuOrdenesProduccion)
        Me.rpg3.ItemLinks.Add(Me.mnuDevolucionVenta)
        Me.rpg3.ItemLinks.Add(Me.mnuEnviarAjustes)
        Me.rpg3.ItemLinks.Add(Me.mnuEnvios)
        Me.rpg3.Name = "rpg3"
        '
        'rpg4
        '
        Me.rpg4.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.rpg4.ItemLinks.Add(Me.BarSubItem1)
        Me.rpg4.ItemLinks.Add(Me.mnuEnviarDatosPendientesPush)
        Me.rpg4.Name = "rpg4"
        '
        'rpg5
        '
        Me.rpg5.ItemLinks.Add(Me.mnuPedidosTransferencia)
        Me.rpg5.ItemLinks.Add(Me.mnuRecibirPedidosTransfINgreso)
        Me.rpg5.ItemLinks.Add(Me.mnuSyncLotes)
        Me.rpg5.ItemLinks.Add(Me.mnuPedidosDeVenta)
        Me.rpg5.ItemLinks.Add(Me.mnuEnviarDatos)
        Me.rpg5.ItemLinks.Add(Me.mnuEliminarEnvio)
        Me.rpg5.Name = "rpg5"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.lblprg.Location = New System.Drawing.Point(4, 4)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1546, 352)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 197)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1554, 3)
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
        Me.lblTLog.Size = New System.Drawing.Size(1554, 4)
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblprgIngresos, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblprg, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 200)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.07937!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.92064!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1554, 554)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'lblprgIngresos
        '
        Me.lblprgIngresos.BackColor = System.Drawing.Color.OldLace
        Me.lblprgIngresos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprgIngresos.Location = New System.Drawing.Point(4, 364)
        Me.lblprgIngresos.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprgIngresos.Name = "lblprgIngresos"
        Me.lblprgIngresos.Size = New System.Drawing.Size(1546, 186)
        Me.lblprgIngresos.TabIndex = 3
        Me.lblprgIngresos.Text = ""
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 754)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MI3_SYNC ->TOM, WMS (vrs 20210115)"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidoCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rpg2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImprimirGrid1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimirgrid2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As ProgressBar
    Friend WithEvents BwProducto As System.ComponentModel.BackgroundWorker
    Friend WithEvents timerProducto As Timer
    Friend WithEvents mnuReporteEjecuciones As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuConversiones As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarDatos As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuEnviarPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdEntidad As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpg4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuSyncLotes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuRecibirPedidosTransfINgreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarAjustes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductosI As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuClientes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuOrdenesProduccion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuPedidosDeVenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnvios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblprgIngresos As RichTextBox
    Friend WithEvents mnuDevolucionVenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuDatosMI3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuDatosPendPush As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarDatosPendientesPush As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpg1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rpg5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents rpg3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuCentrosCosto As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarPedidosVenta As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminarEnvio As DevExpress.XtraBars.BarButtonItem
End Class
