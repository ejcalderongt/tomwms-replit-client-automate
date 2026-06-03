Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.Mvvm.Native
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmManufactura

    Public Delegate Sub Listar_Manufactura()
    Public Property InvokeListar_Manufactura As Listar_Manufactura

    Public BeManufacturaEnc As New clsBeTrans_manufactura_enc

    Private lPedidosPicking As New List(Of clsBeTrans_pe_enc)
    Private BeManufacturaDet As New clsBeTrans_manufactura_det
    Private ListBeManufacturaDet As New List(Of clsBeTrans_manufactura_det)
    Private listTransPeDet As New List(Of clsBeTrans_pe_det)
    Private pTransIdPedidoEnc As Integer = 0
    Private BeBodega As clsBeBodega
    Private BeManufacturaPicking As New clsBeTrans_manufactura_picking
    Private pPedidoEnc As New clsBeTrans_pe_enc

    Public BePickingEnc As clsBeTrans_picking_enc
    Private DTOperadores As DataTable
    Private DTStockRes As New DataTable("StockRes")
    Private DTManufacturaDet As New DataTable("ManufacturaDet")
    Public pListaPedidos As New List(Of Integer)

    Private BeListPickingDet As New List(Of clsBeTrans_picking_det)
    Private ReadOnly BeListPickingParam As New List(Of clsBeTrans_picking_det_parametros)
    Private BeListOp As New List(Of clsBeTrans_picking_op)
    Private pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)
    Private pListObjSP As New List(Of clsBeVW_stock_res)
    Private lStockP As New List(Of clsBeStock_parametro)
    Dim ObjBePickingDet As New clsBeTrans_picking_det

    Public Property IdBodega As Integer = 0
    Private lPropietarios As New List(Of clsBePropietario_bodega)
    Private lBePresentacion As New List(Of clsBeProducto_Presentacion)
    Private lBePedidoDet As New List(Of clsBeTrans_pe_det)
    Public Property Llamado_Desde_Pedido As Boolean = False

    Private Property Es_Manufactura As Boolean

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmManufactura_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            CheckForIllegalCrossThreadCalls = False

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Documento de Manufactura...")

            Es_Manufactura = True

            AP.Listar_Bodegas_By_Usuario(cmbBodegas)

            '#GT25032024: el tipo ya viene dado por el pedido desde SAP
            AP.Listar_Tipo_Manufactura(cmbTipoManufactura)

            '#CKFK20181001: Colocar bodega por defecto.
            cmbBodegas.EditValue = Integer.Parse(AP.IdBodega)
            cmbBodegas.RefreshEditValue()

            dgridPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgridDetalleManufactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            SetDatataTableManufactura()

            Select Case Modo

                Case TipoTrans.Nuevo

                    BeManufacturaEnc = New clsBeTrans_manufactura_enc()
                    listTransPeDet = New List(Of clsBeTrans_pe_det)
                    BeManufacturaEnc.IsNew = True

                    lblCodigo.Text = clsLnTrans_picking_enc.MaxID()
                    lblEstado.Text = "Nuevo"
                    cmdGuardar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    'cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    cmdImprimir.Enabled = False
                    dtmFechaManufactura.DateTime = Today
                    dtmFechaTarea.DateTime = Today

                    dtmHoraI.Value = Now
                    dtmHoraF.Value = Now.AddHours(1)

                    dtmFechaManufactura.ReadOnly = True
                    dtmFechaTarea.ReadOnly = True
                    dtmHoraI.Enabled = False
                    dtmHoraF.Enabled = False

                    XtraTabControl1.TabPages.Remove(XtraTabPageUbicacionPicking)

                Case TipoTrans.Editar

                    If Not BeManufacturaEnc Is Nothing Then

                        Cargar_Datos()

                        Set_Menu_Edicion()

                        Bloquea_Objetos(True)

                        If BeManufacturaEnc.Estado = "Nuevo" Then
                            XtraTabControl1.SelectedTabPage = XtratabPagePedido
                        ElseIf BeManufacturaEnc.Estado = "Proceso" Then
                            XtraTabControl1.SelectedTabPage = XtraTabPageUbicacionPicking
                            txtScanner.Focus()
                        ElseIf BeManufacturaEnc.Estado = "Cerrado" OrElse BeManufacturaEnc.Estado = "Anulado" Then
                            Bloquea_Objetos(False)
                        End If

                    End If

            End Select

            Validar_Operadores()

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Cargar_Datos()

        Dim clsTransaccion As New clsTransaccion

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Tarea Manufactura...")

            If BeManufacturaEnc IsNot Nothing Then

                Mostrar_Datos_Encabezado(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                clsTransaccion.Begin_Transaction()

                '#GT26022024: cargo detalle de la manufactura
                ListBeManufacturaDet = clsLnTrans_manufactura_det.Get_Detalle_By_IdManufacturaEnc(BeManufacturaEnc.IdManufacturaEnc,
                                                                                                  clsTransaccion.lConnection,
                                                                                                  clsTransaccion.lTransaction)

                If ListBeManufacturaDet IsNot Nothing Then

                    Dim pIdPedidoDet = ListBeManufacturaDet.Find(Function(x) x.IdPedidoDet).IdPedidoDet

                    Mostrar_Pedidos_Asociados(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    Dim pIdPickingEnc = clsLnTrans_picking_det.Get_IdPicking_Enc_By_IdPedidoDet(pIdPedidoDet,
                                                                                                BeManufacturaEnc.IdPedidoEnc,
                                                                                                clsTransaccion.lConnection,
                                                                                                clsTransaccion.lTransaction)

                    If pIdPickingEnc = 0 Then

                        Get_Manufactura_Det(ListBeManufacturaDet)

                        XtraTabControl1.TabPages.Remove(XtraTabPageUbicacionPicking)

                        SplashScreenManager.CloseForm(False)

                        XtraMessageBox.Show("El pedido aún no se ha pickeado, no se puede iniciar la tarea de manufactura")

                        'Throw New Exception("Error_GT26022024: No se encontró una tarea de picking asociada al pedido")
                    Else

                        BePickingEnc = clsLnTrans_picking_enc.GetSingle(pIdPickingEnc,
                                                                    clsTransaccion.lConnection,
                                                                    clsTransaccion.lTransaction)

                        If BePickingEnc IsNot Nothing Then

                            If BePickingEnc.ListaPickingDet IsNot Nothing AndAlso BePickingEnc.ListaPickingDet.Count > 0 Then

                                dgridPedidos.Rows.Clear()
                                DTStockRes.Rows.Clear()

                                Dim ListaPedidosPicking As List(Of Integer) = (From picking In BePickingEnc.ListaPickingDet Select picking.IdPedidoEnc).Distinct.ToList()

                                pListaPedidos = ListaPedidosPicking

                                Dim BePickingDet As New clsBeTrans_picking_det
                                Dim vPedido As New clsBeTrans_pe_enc()
                                Dim ie As Integer = 0
                                Dim TiempoCliente As New clsBeCliente_tiempos()

                                For Each IdPedidoEnc As Integer In ListaPedidosPicking

                                    BePickingDet = BePickingEnc.ListaPickingDet.ToList.Find(Function(b) b.IdPedidoEnc = IdPedidoEnc)

                                    If Not lPedidosPicking Is Nothing Then

                                        If Not lPedidosPicking.Count = 0 Then

                                            vPedido = lPedidosPicking.Find(Function(x) x.IdPedidoEnc = IdPedidoEnc)

                                            If vPedido Is Nothing Then
                                                vPedido = clsLnTrans_pe_enc.Get_Single_Without_Picking(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                            End If

                                        Else
                                            vPedido = clsLnTrans_pe_enc.Get_Single_Without_Picking(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                                        End If

                                        pPedidoEnc = vPedido

                                    End If

                                    ie = dgridPedidos.Rows.Add()

                                    dgridPedidos.Rows(ie).Cells("IdPedido").Value = BePickingDet.IdPedidoEnc
                                    dgridPedidos.Rows(ie).Cells("Referencia").Value = BePickingDet.Referencia
                                    dgridPedidos.Rows(ie).Cells("Bodega").Value = BePickingDet.Bodega
                                    dgridPedidos.Rows(ie).Cells("Cliente").Value = BePickingDet.Cliente
                                    dgridPedidos.Rows(ie).Cells("Propietario").Value = BePickingDet.Propietario
                                    dgridPedidos.Rows(ie).Cells("FechaPedido").Value = BePickingDet.FechaPedido
                                    dgridPedidos.Rows(ie).Cells("EstadoP").Value = vPedido.Estado

                                    Get_Manufactura_Det(ListBeManufacturaDet)

                                Next

                                '#GT28022024: aun no debo formatear porque ya no es picking sino manufactura
                                Set_Formato_Grid_Manufactura()

                                dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                                dgridPedidos.EndEdit()

                                BeListPickingDet = BePickingEnc.ListaPickingDet

                                Dim i As Integer = -1

                                dgridDetalleManufactura.CommitEdit(DataGridViewDataErrorContexts.Commit)
                                dgridDetalleManufactura.EndEdit()

                                Try
                                    dgridDetalleManufactura.Rows.Clear()
                                Catch ex As Exception
                                    Debug.Write("No se porque da error aquí a veces: " & ex.Message)
                                End Try

                                Dim ObjDP As clsBeTrans_picking_det_parametros
                                Dim ObjlU As New List(Of clsBeTrans_picking_ubic)
                                Dim PedidoEnc As New clsBeTrans_pe_enc

                                pListBePickingUbic = New List(Of clsBeTrans_picking_ubic)

                                For Each det As clsBeTrans_picking_det In BePickingEnc.ListaPickingDet

                                    i = dgridDetalleManufactura.Rows.Add(det.Producto.Codigo)

                                    dgridDetalleManufactura.Rows(i).Cells("IdPedidoEnc").Value = det.IdPedidoEnc
                                    dgridDetalleManufactura.Rows(i).Cells("IdPedidoDet").Value = det.IdPedidoDet
                                    dgridDetalleManufactura.Rows(i).Cells("Codigo").Value = det.Codigo '#CKFK 20200514 lo cambié por det.producto.codigo
                                    dgridDetalleManufactura.Rows(i).Cells("Producto").Value = det.NombreProducto '#CKFK 20200514 lo cambié por det.producto.nombre
                                    dgridDetalleManufactura.Rows(i).Cells("Presentacion").Value = det.Presentacion.Nombre
                                    dgridDetalleManufactura.Rows(i).Cells("UnidadMedida").Value = det.UnidadMedida.Nombre
                                    dgridDetalleManufactura.Rows(i).Cells("Estado").Value = det.ProductoEstado.Nombre
                                    dgridDetalleManufactura.Rows(i).Cells("Cantidad").Value = det.Cantidad

                                    '#EJC20171026_0523PM: Validación de días cliente en cargadatos picking.
                                    PedidoEnc.IdPedidoEnc = det.IdPedidoEnc

                                    TiempoCliente = clsLnCliente_tiempos.GetSingle(PedidoEnc.IdCliente,
                                                                               det.Producto.IdFamilia,
                                                                               det.Producto.IdClasificacion)

                                    If Not TiempoCliente Is Nothing Then
                                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = IIf(PedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                                    Else
                                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = -1
                                    End If

                                    dgridDetalleManufactura.Rows(i).Cells("CantidadRecibida").Value = det.Cantidad_recibida

                                    Cargar_Operador(i, det.IdOperadorBodega)

                                    ObjDP = det.ListaDetalleParametro.ToList.Find(Function(b) b.IdPickingDet = det.IdPickingDet)

                                    If ObjDP IsNot Nothing Then
                                        BeListPickingParam.Add(ObjDP)
                                    End If

                                    '#EJC20171026_1212PM: Anteriormente se buscaba solo un objeto, pero cada IdPIckingDet tiene N picking ubic asociados, por lo que se debe hacer un findAll
                                    ObjlU = BePickingEnc.ListaPickingUbic.FindAll(Function(b) b.IdPickingDet = det.IdPickingDet)

                                    If ObjlU IsNot Nothing Then
                                        '#EJC20171026_1212PM: Asociar a cada objeto de la lista el IdPedidoDet al que pertenece.
                                        ObjlU.ForEach(Sub(x) x.IdPedidoDet = det.IdPedidoDet)
                                        pListBePickingUbic.AddRange(ObjlU)
                                    End If

                                Next

                            End If

                        End If

                    End If

                End If

                clsTransaccion.Commit_Transaction()
                clsTransaccion.Close_Conection()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Mostrar_Datos_Encabezado(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            cmbBodegas.EditValue = BeManufacturaEnc.IdBodega
            lblCodigo.Text = BeManufacturaEnc.IdManufacturaEnc
            lblEstado.Text = BeManufacturaEnc.Estado

            cmbTipoManufactura.EditValue = BeManufacturaEnc.IdTipoManufactura
            cmbPropietario.EditValue = BeManufacturaEnc.IdPropietarioBodega

            If BeManufacturaEnc.Fecha_manufactura.Date = New Date(1900, 1, 1) Then
                dtmFechaManufactura.DateTime = Today
                dtmFechaTarea.DateTime = Today
            Else
                dtmFechaManufactura.EditValue = BeManufacturaEnc.Fecha_manufactura
                dtmFechaTarea.EditValue = BeManufacturaEnc.Fecha_manufactura
            End If

            dtmHoraI.Value = BeManufacturaEnc.Hora_ini
            dtmHoraF.Value = BeManufacturaEnc.Hora_fin

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Mostrar_Pedidos_Asociados(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If BePickingEnc IsNot Nothing Then

                Dim ListaPedidosPicking As List(Of Integer) = (From picking In BePickingEnc.ListaPickingDet Select picking.IdPedidoEnc).Distinct.ToList
                pListaPedidos = ListaPedidosPicking

                Dim BePickingDet As clsBeTrans_picking_det
                Dim BePedidoEnc As New clsBeTrans_pe_enc
                Dim ie As Integer = 0

                lPedidosPicking = New List(Of clsBeTrans_pe_enc)

                For Each IdPedidoEnc As Integer In ListaPedidosPicking

                    BePickingDet = BePickingEnc.ListaPickingDet.ToList.Find(Function(b) b.IdPedidoEnc = IdPedidoEnc)
                    BePedidoEnc = clsLnTrans_pe_enc.Get_Single_Without_Picking(IdPedidoEnc, lConnection, lTransaction)

                    ie = dgridPedidos.Rows.Add()

                    dgridPedidos.Rows(ie).Cells("IdPedido").Value = BePickingDet.IdPedidoEnc
                    dgridPedidos.Rows(ie).Cells("Referencia").Value = BePickingDet.Referencia
                    dgridPedidos.Rows(ie).Cells("Bodega").Value = BePickingDet.Bodega
                    dgridPedidos.Rows(ie).Cells("Cliente").Value = BePickingDet.Cliente
                    dgridPedidos.Rows(ie).Cells("Propietario").Value = BePickingDet.Propietario
                    dgridPedidos.Rows(ie).Cells("FechaPedido").Value = BePickingDet.FechaPedido
                    dgridPedidos.Rows(ie).Cells("EstadoP").Value = BePedidoEnc.Estado
                    lPedidosPicking.Add(BePedidoEnc)

                Next

                dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                dgridPedidos.EndEdit()

                If BeListPickingDet IsNot Nothing Then
                    BeListPickingDet = BePickingEnc.ListaPickingDet.ToList()
                End If

            Else

                Dim BePedidoEnc As New clsBeTrans_pe_enc
                BePedidoEnc = clsLnTrans_pe_enc.Get_Single_Without_Picking(BeManufacturaEnc.IdPedidoEnc, lConnection, lTransaction)

                dgridPedidos.Rows(0).Cells("IdPedido").Value = BePedidoEnc.IdPedidoEnc
                dgridPedidos.Rows(0).Cells("Referencia").Value = BePedidoEnc.Referencia
                dgridPedidos.Rows(0).Cells("Bodega").Value = AP.Bodega.Nombre
                dgridPedidos.Rows(0).Cells("Cliente").Value = BePedidoEnc.Cliente.Nombre_comercial
                dgridPedidos.Rows(0).Cells("Propietario").Value = BePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial
                dgridPedidos.Rows(0).Cells("FechaPedido").Value = BePedidoEnc.Fecha_Pedido
                dgridPedidos.Rows(0).Cells("EstadoP").Value = BePedidoEnc.Estado
                lPedidosPicking.Add(BePedidoEnc)

                dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                dgridPedidos.EndEdit()

                Try
                    dgridDetalleManufactura.Rows.Clear()
                Catch ex As Exception
                    Debug.Write("No se porque da error aquí a veces: " & ex.Message)
                End Try

                For Each det As clsBeTrans_pe_det In BePedidoEnc.Detalle

                    Dim i As Integer = dgridDetalleManufactura.Rows.Add(det.Producto.Codigo)

                    dgridDetalleManufactura.Rows(i).Cells("IdPedidoEnc").Value = det.IdPedidoEnc
                    dgridDetalleManufactura.Rows(i).Cells("IdPedidoDet").Value = det.IdPedidoDet
                    dgridDetalleManufactura.Rows(i).Cells("Codigo").Value = det.Codigo_Producto
                    dgridDetalleManufactura.Rows(i).Cells("Producto").Value = det.Nombre_producto
                    dgridDetalleManufactura.Rows(i).Cells("Presentacion").Value = det.Nom_presentacion
                    dgridDetalleManufactura.Rows(i).Cells("UnidadMedida").Value = det.Nom_unid_med
                    dgridDetalleManufactura.Rows(i).Cells("Estado").Value = det.ProductoEstado
                    dgridDetalleManufactura.Rows(i).Cells("Cantidad").Value = det.Cantidad

                    Dim TiempoCliente As New clsBeCliente_tiempos
                    TiempoCliente = clsLnCliente_tiempos.GetSingle(BePedidoEnc.IdCliente,
                                                                   det.Producto.IdFamilia,
                                                                   det.Producto.IdClasificacion)

                    If Not TiempoCliente Is Nothing Then
                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = IIf(BePedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                    Else
                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = -1
                    End If

                    dgridDetalleManufactura.Rows(i).Cells("CantidadRecibida").Value = det.Cantidad

                Next


            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Get_Manufactura_Det(ByVal pListManufacturaDet As List(Of clsBeTrans_manufactura_det))

        Dim Idx As Integer = 0

        Try

            If pListManufacturaDet.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det
                Dim BePropietario As New clsBePropietario_bodega
                '#GT26032024: calcular fecha ini y fecha fin
                Dim tbTiempos As New DataTable

                Dim hora_inicial As String = ""
                Dim hora_final As String = ""
                Dim minutos_transcurridos As Integer = 0

                Dim pTipoManufacturaProducto As clsDataContractDI.Manufacturing_Process = 0

                For Each BeManufacturaDet In pListManufacturaDet

                    Idx = lBePedidoDet.FindIndex(Function(x) x.IdPedidoDet = BeManufacturaDet.IdPedidoDet)

                    '#EJC20191121: Optimización por memoria.
                    If Idx = -1 Then
                        BePedidoDet.IdPedidoDet = BeManufacturaDet.IdPedidoDet
                        clsLnTrans_pe_det.GetSingle(BePedidoDet)
                        If Not lBePedidoDet.Contains(BePedidoDet) Then
                            lBePedidoDet.Add(BePedidoDet)
                        End If
                    Else
                        BePedidoDet = lBePedidoDet(Idx)
                    End If

                    Idx = lPropietarios.FindIndex(Function(x) x.IdPropietario = BeManufacturaDet.IdPropietarioBodega)

                    If Idx = -1 Then
                        BePropietario = clsLnPropietario_bodega.Get_Single_With_Propietario_By_IdPropietarioBodega(BeManufacturaDet.IdPropietarioBodega)
                        lPropietarios.Add(BePropietario)
                    Else
                        BePropietario = lPropietarios(Idx)
                    End If

                    If (BePedidoDet.IdPresentacion = 0) Then
                        vCantidadReservadaUMBas = BeManufacturaDet.Cantidad_esperada
                        vCantidadReservadaPres = 0
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0
                        vCantidadDespPres = 0
                    Else

                        '#EJC20191121_1456: Optimización por memoria.
                        Idx = lBePresentacion.FindIndex(Function(x) x.IdPresentacion = BePedidoDet.IdPresentacion)

                        If Idx = -1 Then
                            BePresentacion.IdPresentacion = BePedidoDet.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            lBePresentacion.Add(BePresentacion)
                        Else
                            BePresentacion = lBePresentacion(Idx)
                        End If

                        '#CM_20190206: Se cambio el orden en el que se realizaban las operaciones porque la cantidad
                        'cuando tiene presentación el picking la devuelve en presentación.
                        If Not BePresentacion Is Nothing Then

                            If (Modo = TipoTrans.Nuevo) Then

                                '#CKFK20221121 Puse esto en comentario AndAlso (BePedidoDet.IdPresentacion <> 0) para tomar como referencia la unidad de medida de la reserva
                                If (BePedidoDet.IdPresentacion <> 0) Then
                                    'vCantidadReservadaPres = objDetalle.Cantidad_esperada
                                    vCantidadReservadaUMBas = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                                    vCantidadRecPres = BeManufacturaDet.Cantidad_recibida
                                    BeManufacturaDet.Cantidad_recibida = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                                Else
                                    vCantidadReservadaPres = BeManufacturaDet.Cantidad_esperada
                                    vCantidadReservadaUMBas = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                                    vCantidadRecPres = BeManufacturaDet.Cantidad_recibida
                                    BeManufacturaDet.Cantidad_recibida = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                                End If

                            Else
                                'vCantidadReservadaPres = objDetalle.Cantidad_esperada
                                vCantidadReservadaUMBas = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                                vCantidadRecPres = BeManufacturaDet.Cantidad_recibida
                                BeManufacturaDet.Cantidad_recibida = Math.Round(BeManufacturaDet.Cantidad_recibida * BePresentacion.Factor, 6)
                            End If

                        End If

                    End If

                    tbTiempos.Clear()
                    tbTiempos = clsLnTrans_manufactura_picking.Get_Tiempos_By_IdManufacturaEnc_and_IdManufacturaDet(BeManufacturaDet.IdManufacturaEnc,
                                                                                                                    BeManufacturaDet.IdManufacturaDet)

                    hora_inicial = IIf(tbTiempos.Rows(0).Item("hora_ini").ToString <> "", tbTiempos.Rows(0).Item("hora_ini").ToString, "")
                    hora_final = IIf(tbTiempos.Rows(0).Item("hora_fin").ToString <> "", tbTiempos.Rows(0).Item("hora_fin").ToString, "")
                    minutos_transcurridos = IIf(tbTiempos.Rows(0).Item("duracion_minutos").ToString <> "", tbTiempos.Rows(0).Item("duracion_minutos").ToString, 0)

                    pTipoManufacturaProducto = clsLnProducto.Get_Tipo_Manufactura_By_CodigoProducto(BePedidoDet.Codigo_Producto)

                    'clsLnTrans_manufactura_tipo.Get_Nombre_Manufactura_By_IdTipoManufactura(pTipoManufacturaProducto)

                    If pTipoManufacturaProducto = 0 Then
                        pTipoManufacturaProducto = clsLnTrans_manufactura_enc.Get_TipoManufactura_By_IdPedidoDet(BeManufacturaDet.IdPedidoDet)
                    End If

                    Dim pManufactura As String = pTipoManufacturaProducto.ToString

                    DTManufacturaDet.Rows.Add(BeManufacturaDet.IdManufacturaDet,
                                              BeManufacturaDet.IdManufacturaEnc,
                                              BeManufacturaDet.IdPedidoDet,
                                              BeManufacturaDet.IdPropietarioBodega,
                                              BeManufacturaDet.IdProductoBodega,
                                              BeManufacturaDet.Codigo_producto,
                                              BeManufacturaDet.Nombre_producto,
                                              pManufactura,
                                              BeManufacturaDet.Cantidad_esperada,
                                              BeManufacturaDet.Cantidad_recibida,
                                              hora_inicial,
                                              hora_final,
                                              minutos_transcurridos)

                Next

            End If

            dgridPickingUbic.DataSource = DTManufacturaDet
            grdvPickingUbic.BestFitColumns()

            If grdvPickingUbic.RowCount > 0 Then
                grdvPickingUbic.Columns("IdPropietarioBodega").Visible = False
                grdvPickingUbic.Columns("IdProductoBodega").Visible = False
                grdvPickingUbic.Columns("IdManufacturaEnc").Visible = False
                grdvPickingUbic.Columns("IdManufacturaDet").Visible = False
                grdvPickingUbic.Columns("IdPedidoDet").Visible = False
                'grdvPickingUbic.Columns("IdPedidoDet").Caption = "Pedido Detalle"
                grdvPickingUbic.Columns("cantidad_esperada").Caption = "Cantidad Esperada"
                grdvPickingUbic.Columns("cantidad_recibida").Caption = "Cantidad Recibida"


                grdvPickingUbic.Columns("hora_inicial").Caption = "Hora Inicial"
                grdvPickingUbic.Columns("hora_final").Caption = "Hora Final"
                grdvPickingUbic.Columns("tiempo_transcurrido").Caption = "Duracion en Minutos"

                grdvPickingUbic.Columns("cantidad_esperada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("cantidad_esperada").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("cantidad_recibida").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvPickingUbic.Columns("cantidad_recibida").DisplayFormat.FormatString = "{0:n6}"

                grdvPickingUbic.Columns("cantidad_esperada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvPickingUbic.Columns("cantidad_esperada").SummaryItem.DisplayFormat = "{0:n6}"

                grdvPickingUbic.Columns("cantidad_recibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvPickingUbic.Columns("cantidad_recibida").SummaryItem.DisplayFormat = "{0:n6}"

                grdvPickingUbic.Columns("tiempo_transcurrido").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvPickingUbic.Columns("tiempo_transcurrido").SummaryItem.DisplayFormat = "{0:n6}"

                lblRegistros.Caption = "Registros: " & grdvPickingUbic.RowCount

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Validar_Operadores()

        Try

            DsOrdenCompraRecepcionOperador.Clear()

            '#CKFK20220703 Cambié el query para listar los operadores
            'DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodegas.EditValue)
            DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_For_Tarea_DT(cmbBodegas.EditValue,
                                                                                 clsDataContractDI.tTipoTarea.PICK)

            If BePickingEnc IsNot Nothing AndAlso BePickingEnc.IdPickingEnc > 0 Then
                BeListOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(BePickingEnc.IdPickingEnc).ToList
            Else
                BeListOp = New List(Of clsBeTrans_picking_op)
            End If

            'Listar_Operadores()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub lnkAgregarPedido_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles lnkAgregarPedido.ItemClick

        Try

            '#CKFK20171026_0721PM: Le envío a la forma los pedidos ya incluidos en el Picking
            Dim bo As New frmPedidoDetalleBuscador() With {.pListaPedidos = pListaPedidos,
                                                           .IdBodega = cmbBodegas.EditValue,
                                                           .Es_Manufactura = Es_Manufactura}
            bo.ShowDialog()

            If Not bo.pBePedidoEnc Is Nothing Then

                If bo.pBePedidoEnc.IdCliente <> 0 AndAlso bo.pBePedidoEnc.IdPropietarioBodega <> 0 Then

                    If bo.pBePedidoEnc.Detalle IsNot Nothing AndAlso bo.pBePedidoEnc.Detalle.Count > 0 Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Cargando Pedido...")

                        Cursor = Cursors.WaitCursor

                        Application.DoEvents()

                        pPedidoEnc = bo.pBePedidoEnc
                        pTransIdPedidoEnc = bo.pBePedidoEnc.IdPedidoEnc

                        Dim i As Integer = dgridPedidos.Rows.Add()

                        dgridPedidos.Rows(i).Cells("IdPedido").Value = bo.pBePedidoEnc.IdPedidoEnc
                        dgridPedidos.Rows(i).Cells("Referencia").Value = bo.pBePedidoEnc.Referencia
                        dgridPedidos.Rows(i).Cells("Bodega").Value = bo.pBePedidoEnc.IdBodega
                        dgridPedidos.Rows(i).Cells("Cliente").Value = bo.pBePedidoEnc.Cliente.Nombre_comercial
                        dgridPedidos.Rows(i).Cells("Propietario").Value = bo.pBePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial
                        dgridPedidos.Rows(i).Cells("FechaPedido").Value = bo.pBePedidoEnc.Fecha_Pedido
                        dgridPedidos.Rows(i).Cells("EstadoP").Value = bo.pBePedidoEnc.Estado

                        dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        dgridPedidos.EndEdit()

                        'Set_Stock_Res(bo.pBePedidoEnc.IdPedidoEnc)

                        '#CKFK20171026_0721PM: Agregué lista de pedidos donde se adicionan los pedidos ya incluidos en el Picking
                        pListaPedidos.Add(bo.pBePedidoEnc.IdPedidoEnc)

                        For Each BeTransPeDet As clsBeTrans_pe_det In bo.pBePedidoEnc.Detalle

                            Application.DoEvents()

                            '#GT26022024: obtengo la lista del pedido, para iterar cada producto
                            '#GT26032024: valido que el tipo manufactura del producto por linea, sea valido para agregarlo al detalle del pedido 
                            Dim pTipoManufacturaProducto = clsLnProducto.Get_Tipo_Manufactura_By_CodigoProducto(BeTransPeDet.Codigo_Producto)

                            If pTipoManufacturaProducto > 1 Then
                                listTransPeDet.Add(BeTransPeDet)
                                BeTransPeDet.ListaStockRes = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoDet(BeTransPeDet.IdPedidoDet, BeTransPeDet.IdPedidoEnc)
                                SetProducto(BeTransPeDet, bo.pBePedidoEnc)
                            End If

                        Next

                        XtraTabControl1.TabPages.Add(XtraTabPageUbicacionPicking)
                        XtraTabControl1.SelectedTabPage = XtraTabPageUbicacionPicking

                        Set_Formato_Grid_Manufactura()

                    End If

                Else

                    XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If

            Else

                '#EJC20220613: No se seleccinó ningún pedido, no complain, no mostrar mensaje.
                'XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

            SplashScreenManager.CloseForm(False)

            Cursor = Cursors.Default

            XtraTabControl1.SelectedTabPage = XtratabPagePedido

        Catch ex As Exception

            Cursor = Cursors.Default

            SplashScreenManager.CloseForm(False)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    '#CM20172310_1231PM: Se agregaron campos al GridView de ubicación picking. 
    Public Sub Set_Stock_Res(ByVal pIdPedidoEnc As Integer)

        Try

            pListObjSP = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoEnc_And_IdPickingEnc(pIdPedidoEnc,
                                                                                             BePickingEnc.IdPickingEnc,
                                                                                             False,
                                                                                             (Modo = TipoTrans.Nuevo),
                                                                                             True)

            If pListObjSP.Count > 0 Then

                Dim vCantidadReservadaUMBas As Double = 0
                Dim vCantidadReservadaPres As Double = 0
                Dim vPesoReservado As Double = 0
                Dim vCantidadRecPres As Double = 0
                Dim vCantidadVerPres As Double = 0
                Dim vCantidadDespPres As Double = 0
                Dim BePresentacion As New clsBeProducto_Presentacion
                Dim BePedidoDet As New clsBeTrans_pe_det
                Dim Idx As Integer = -1

                Dim BeUbicacion As New clsBeBodega_ubicacion()
                Dim vSingularidadTramoUbicacionEnZP As New List(Of clsBeZona_picking_tramo)
                Dim SingleBeZonaPIckingTramoOP As New clsBeOperador_zona_picking_tramo
                Dim BeOperadorBodega As New clsBeOperador_bodega
                Dim vNombreOperadorAsingadoAuto As String = ""

                '#EJC20190607: No limpiar, porque si se cargan más pedidos al picking se limpia el stock anterior del grid.
                If Modo = TipoTrans.Editar Then
                    'DTStockRes.Rows.Clear()
                End If

                For Each ObjStock As clsBeVW_stock_res In pListObjSP

                    Idx = lBePedidoDet.FindIndex(Function(x) x.IdPedidoDet = ObjStock.IdPedidoDet)
                    '#EJC20191121: Optimización por memoria.
                    If Idx = -1 Then
                        BePedidoDet.IdPedidoDet = ObjStock.IdPedidoDet
                        clsLnTrans_pe_det.GetSingle(BePedidoDet)
                        lBePedidoDet.Add(BePedidoDet.Clone())
                    Else
                        BePedidoDet = lBePedidoDet(Idx).Clone()
                    End If

                    '#CKFK20221121 Voy a quitar esta condición OrElse (BePedidoDet.IdPresentacion = 0)
                    If (ObjStock.IdPresentacion = 0) Then

                        vCantidadReservadaUMBas = ObjStock.CantidadReservadaUMBas
                        vCantidadReservadaPres = 0
                        vPesoReservado = ObjStock.Peso
                        '#CKFK 20210728 Coloqué en 0 estas variables cuando el producto no tiene presentacion vCantidadRecPres y vCantidadVerPres
                        vCantidadRecPres = 0
                        vCantidadVerPres = 0

                    Else

                        '#EJC20191121_1456: Optimización por memoria.
                        Idx = lBePresentacion.FindIndex(Function(x) x.IdPresentacion = ObjStock.IdPresentacion)

                        If Idx = -1 Then
                            BePresentacion = New clsBeProducto_Presentacion()
                            BePresentacion.IdPresentacion = ObjStock.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BePresentacion)
                            lBePresentacion.Add(BePresentacion.Clone())
                        Else
                            BePresentacion = New clsBeProducto_Presentacion()
                            BePresentacion = lBePresentacion(Idx).Clone()
                        End If

                        If Not BePresentacion Is Nothing Then

                            If (Modo = TipoTrans.Nuevo) Then

                                If (ObjStock.IdPresentacion <> 0) AndAlso (BePedidoDet.IdPresentacion <> 0) Then
                                    vCantidadReservadaPres = Math.Round(ObjStock.CantidadReservadaUMBas / BePresentacion.Factor, 6)
                                    vCantidadReservadaUMBas = ObjStock.CantidadReservadaUMBas
                                    ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada / BePresentacion.Factor, 6)
                                    vCantidadRecPres = Math.Round(ObjStock.Cantidad_Pickeada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Pickeada = ObjStock.Cantidad_Pickeada
                                    vCantidadVerPres = Math.Round(ObjStock.Cantidad_Verificada / BePresentacion.Factor, 6)
                                    ObjStock.Cantidad_Verificada = ObjStock.Cantidad_Verificada
                                    vCantidadDespPres = 0
                                End If

                            Else
                                '#CM_20190206: Se cambio el orden en el que se realizaban las operaciones porque la cantidad cuando tiene presentación el picking la devuelve en presentación.
                                '#CKFK20221121 Puse esto en comentario AndAlso (BePedidoDet.IdPresentacion <> 0) para que solo se guíe por la reserva
                                If (ObjStock.IdPresentacion <> 0) Then
                                    If BePedidoDet.IdStockEspecifico = 0 Then
                                        If (ObjStock.IdPresentacion <> 0) Then ' BePedidoDet.IdPresentacion
                                            vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                            vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                            vCantidadRecPres = ObjStock.Cantidad_Pickeada
                                            ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                            vCantidadVerPres = ObjStock.Cantidad_Verificada
                                            ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada * BePresentacion.Factor, 6)
                                            vCantidadDespPres = ObjStock.Cantidad_Despachada
                                            ObjStock.Cantidad_Despachada = Math.Round(ObjStock.Cantidad_Despachada * BePresentacion.Factor, 6)
                                        Else
                                            vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                            vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                            ObjStock.Cantidad_Pickeada = ObjStock.Cantidad_Pickeada
                                            ObjStock.Cantidad_Verificada = ObjStock.Cantidad_Verificada
                                        End If
                                    Else
                                        vCantidadReservadaPres = ObjStock.CantidadReservadaUMBas
                                        vCantidadReservadaUMBas = Math.Round(ObjStock.CantidadReservadaUMBas * BePresentacion.Factor, 6)
                                        '#CKFK 20210728 Llené la variable de la cantidad recibida con presentacion que no se estaba llenando
                                        'y en la cantidad pickeada asigné la cantidad en presentación multiplicada por el factor
                                        vCantidadRecPres = ObjStock.Cantidad_Pickeada
                                        ObjStock.Cantidad_Pickeada = Math.Round(ObjStock.Cantidad_Pickeada * BePresentacion.Factor, 6)
                                        '#CKFK 20210728 Llené la variable de la cantidad verificada con presentacion que no se estaba llenando
                                        'y en la cantidad verificad asigné la cantidad en presentación multiplicada por el factor
                                        vCantidadVerPres = ObjStock.Cantidad_Verificada
                                        ObjStock.Cantidad_Verificada = Math.Round(ObjStock.Cantidad_Verificada * BePresentacion.Factor, 6)
                                    End If
                                End If

                            End If

                        Else
                            Throw New Exception("No se pudo obtener la presentación con identificador: " & ObjStock.IdPresentacion)
                        End If

                        vPesoReservado = ObjStock.Peso

                    End If

                    '#CKFK20220704 Agregué esto para poder eliminar la fila y que no se inserte dos veces en el grid
                    If DTStockRes.Rows.Count > 0 Then
                        Dim vRow As DataRow

                        vRow = DTStockRes.Select("IdStockRes=" & ObjStock.IdStockRes).FirstOrDefault

                        If vRow IsNot Nothing Then
                            DTStockRes.Rows.Remove(vRow)
                        End If

                    End If

                    DTStockRes.Rows.Add(ObjStock.IdPedido,
                                        ObjStock.IdPicking,
                                        ObjStock.Codigo_Producto,
                                        ObjStock.Nombre_Producto,
                                        ObjStock.Nombre_Presentacion,
                                        ObjStock.NomEstado,
                                        ObjStock.UMBas,
                                        ObjStock.Propietario,
                                        ObjStock.Ubicacion_Nombre,
                                        ObjStock.Lote,
                                        ObjStock.Lic_plate,
                                        ObjStock.Fecha_Vence,
                                        ObjStock.Factor,
                                        vCantidadReservadaPres,
                                        vCantidadReservadaUMBas,
                                        vCantidadRecPres,
                                        ObjStock.Cantidad_Pickeada,
                                        vCantidadVerPres,
                                        ObjStock.Cantidad_Verificada,
                                        vCantidadDespPres,
                                        ObjStock.Cantidad_Despachada,
                                        ObjStock.peso_pickeado,
                                        ObjStock.peso_verificado,
                                        ObjStock.encontrado,
                                        ObjStock.acepto,
                                        ObjStock.Fecha_ingreso,
                                        ObjStock.IdStockRes,
                                        vNombreOperadorAsingadoAuto)

                Next

            End If

            If DTStockRes.Rows.Count > 0 Then

                If BePickingEnc.Estado <> "Despachado" Then
                    dgridPickingUbic.DataSource = DTStockRes
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub SetProducto(ByVal pBeTransPeDet As clsBeTrans_pe_det,
                           ByVal PedidoEnc As clsBeTrans_pe_enc)

        Dim vIndicador As Double = 0

        Try

            If PedidoEnc Is Nothing Then Exit Sub

            vIndicador = 1

            If BeListPickingDet IsNot Nothing AndAlso BeListPickingDet.Count > 0 Then

                Dim lIndexE As Integer = -1

                lIndexE = BeListPickingDet.FindIndex(Function(b) b.IdPedidoDet = pBeTransPeDet.IdPedidoDet)

                If lIndexE > -1 Then
                    'Throw New Exception(String.Format("El Producto {0} ya fue agregado.", pBeTransPeDet.NombreProducto))
                    Exit Sub
                End If

                ObjBePickingDet = New clsBeTrans_picking_det()
                ObjBePickingDet.IdPickingDet = BeListPickingDet.Max(Function(b) b.IdPickingDet) + 1

            Else

                If ObjBePickingDet Is Nothing Then
                    ObjBePickingDet = New clsBeTrans_picking_det()
                End If

                ObjBePickingDet.IdPickingDet = 1

                vIndicador = 2

            End If

            Dim i As Integer = dgridDetalleManufactura.Rows.Add()

            dgridDetalleManufactura.Rows(i).Cells("Codigo").Value = pBeTransPeDet.Producto.Codigo
            dgridDetalleManufactura.Rows(i).Cells("Producto").Value = pBeTransPeDet.Producto.Nombre
            dgridDetalleManufactura.Rows(i).Cells("Presentacion").Value = pBeTransPeDet.Nom_presentacion
            dgridDetalleManufactura.Rows(i).Cells("UnidadMedida").Value = pBeTransPeDet.Nom_unid_med
            dgridDetalleManufactura.Rows(i).Cells("Estado").Value = pBeTransPeDet.Nom_estado
            dgridDetalleManufactura.Rows(i).Cells("Cantidad").Value = pBeTransPeDet.Cantidad
            dgridDetalleManufactura.Rows(i).Cells("IdPedidoDet").Value = pBeTransPeDet.IdPedidoDet
            dgridDetalleManufactura.Rows(i).Cells("IdPedidoEnc").Value = pBeTransPeDet.IdPedidoEnc
            dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = 0

            vIndicador = 3

            Cargar_Operador(i)

            vIndicador = 4

            dgridDetalleManufactura.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgridDetalleManufactura.EndEdit()

            vIndicador = 5

            ObjBePickingDet.IdPedidoEnc = pBeTransPeDet.IdPedidoEnc
            ObjBePickingDet.IdPedidoDet = pBeTransPeDet.IdPedidoDet
            ObjBePickingDet.Cantidad = pBeTransPeDet.Cantidad
            ObjBePickingDet.User_agr = AP.UsuarioAp.IdUsuario
            ObjBePickingDet.Fec_agr = Now
            ObjBePickingDet.User_mod = AP.UsuarioAp.IdUsuario
            ObjBePickingDet.Fec_mod = Now
            ObjBePickingDet.Activo = True
            ObjBePickingDet.IsNew = True

            vIndicador = 6

            Dim ObjProducto As New clsBeProducto

            ObjBePickingDet.Presentacion = New clsBeProducto_Presentacion
            ObjBePickingDet.UnidadMedida = New clsBeUnidad_medida
            ObjBePickingDet.ProductoEstado = New clsBeProducto_estado

            If Not (pBeTransPeDet.Producto Is Nothing) Then
                If pBeTransPeDet.IdProductoBodega <> 0 Then
                    ObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransPeDet.IdProductoBodega)
                    If Not ObjProducto Is Nothing Then
                        pBeTransPeDet.Producto = ObjProducto
                    End If
                End If
            Else
                '#EJC20211220: En caso de que por alguna razón sea nothing... buscar
                If pBeTransPeDet.IdProductoBodega <> 0 Then
                    ObjProducto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransPeDet.IdProductoBodega)
                    If Not ObjProducto Is Nothing Then
                        pBeTransPeDet.Producto = ObjProducto
                    End If
                End If
                If pBeTransPeDet.Producto Is Nothing Then
                    Throw New Exception("#20211218: No Se encontró el objeto de producto asociado a la línea de detalle para la línea del pedido: " & pBeTransPeDet.No_linea & " y Código de producto: " & pBeTransPeDet.Codigo_Producto)
                End If
            End If

            vIndicador = 7

            If pBeTransPeDet.Producto.IdProducto <> 0 Then

                ObjProducto = clsLnProducto.Get_Single_By_IdProducto(pBeTransPeDet.Producto.IdProducto)

                vIndicador = 8.4

                If Not ObjProducto Is Nothing Then

                    ObjBePickingDet.Producto = New clsBeProducto()
                    ObjBePickingDet.Producto.Codigo = ObjProducto.Codigo
                    ObjBePickingDet.Producto.Nombre = pBeTransPeDet.Producto.Nombre
                    ObjBePickingDet.Codigo = ObjProducto.Codigo
                    ObjBePickingDet.NombreProducto = pBeTransPeDet.Producto.Nombre
                    ObjBePickingDet.Presentacion.IdPresentacion = pBeTransPeDet.IdPresentacion
                    ObjBePickingDet.Presentacion.Nombre = pBeTransPeDet.Nom_presentacion
                    ObjBePickingDet.UnidadMedida.IdUnidadMedida = pBeTransPeDet.IdUnidadMedidaBasica
                    ObjBePickingDet.UnidadMedida.Nombre = pBeTransPeDet.Nom_unid_med
                    ObjBePickingDet.ProductoEstado.IdEstado = pBeTransPeDet.IdEstado
                    ObjBePickingDet.ProductoEstado.Nombre = pBeTransPeDet.Nom_estado
                    ObjBePickingDet.Cantidad = pBeTransPeDet.Cantidad
                    ObjBePickingDet.Cantidad_recibida = 0
                    ObjBePickingDet.Cliente_dias = 0

                    BeListPickingDet.Add(ObjBePickingDet)

                    Dim TiempoCliente As New clsBeCliente_tiempos
                    TiempoCliente = clsLnCliente_tiempos.GetSingle(PedidoEnc.IdCliente, ObjProducto.IdFamilia, ObjProducto.IdClasificacion)

                    If Not TiempoCliente Is Nothing Then
                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = IIf(PedidoEnc.Local, TiempoCliente.Dias_Local, TiempoCliente.Dias_Exterior)
                    Else
                        dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value = -1
                    End If

                    If pBeTransPeDet.ListaStockRes IsNot Nothing AndAlso pBeTransPeDet.ListaStockRes.Count > 0 Then

                        Dim BePresentacion As New clsBeProducto_Presentacion

                        vIndicador = 8.3

                        Dim BeUbicacion As New clsBeBodega_ubicacion

                        '#EJC20220603: Se utiliza para filtrar el resultado de las zonas de picking identificadas acorde a la ubicación del producto.
                        Dim vSingularidadTramoUbicacionEnZP As New List(Of clsBeZona_picking_tramo)
                        Dim SingleBeZonaPIckingTramoOP As New clsBeOperador_zona_picking_tramo

                        For Each bo As clsBeStock_res In pBeTransPeDet.ListaStockRes

                            Dim ObjU As New clsBeTrans_picking_ubic()

                            With ObjU

                                If Val(bo.Cantidad) = 0 Then

                                    Dim vMensaje As String = String.Format("Una línea del picking es inconsistente, 
                                        éste es un error poco usual y estamos trabajando para resolverlo. 
                                        El IdStock: {0} reporta una cantidad: {1} aunque ésto puede no afectar
                                        se considera una línea no válida para el sistema, 
                                        de forma preventiva se ha restringido el picking de este documento,
                                        lo sentimos, EJC.", bo.IdStock, bo.Cantidad)

                                    '#EJC20180619: Punto de control agregado para evitar que se generen despachos sobre cantidad 0.                            
                                    Throw New Exception(vMensaje)

                                Else

                                    .IdPedidoEnc = bo.IdPedido
                                    .IdPedidoDet = bo.IdPedidoDet
                                    .IdStockRes = bo.IdStockRes
                                    .IdPickingDet = ObjBePickingDet.IdPickingDet
                                    .IdStock = bo.IdStock
                                    .IdPropietarioBodega = bo.IdPropietarioBodega
                                    .IdProductoBodega = bo.IdProductoBodega
                                    .IdProductoEstado = bo.IdProductoEstado
                                    .IdPresentacion = bo.IdPresentacion
                                    .IdUnidadMedida = bo.IdUnidadMedida
                                    .IdUbicacionAnterior = Val(bo.Ubicacion_ant)
                                    .IdRecepcion = bo.IdRecepcion
                                    .IdUbicacion = bo.IdUbicacion
                                    .Lote = bo.Lote
                                    .Fecha_Vence = bo.Fecha_vence
                                    .Serial = bo.Serial
                                    .Lic_plate = bo.Lic_plate
                                    .Peso_solicitado = bo.Peso
                                    .IdBodega = cmbBodegas.EditValue

                                    ''#EJC20180926: Insertar cantidad de presentación en picking_ubic 
                                    If bo.IdPresentacion = 0 Then
                                        .Cantidad_Solicitada = bo.Cantidad
                                    Else
                                        BePresentacion = clsLnProducto_presentacion.GetSingle(bo.IdPresentacion)
                                        If Not BePresentacion Is Nothing Then
                                            If BePresentacion.Factor = 0 Then BePresentacion.Factor = 1
                                            '#EJC20191122: Quité redondeo para obtener exactitud en la conversión.
                                            '.Cantidad_Solicitada = Math.Round(bo.Cantidad / BePresentacion.Factor, 6)
                                            .Cantidad_Solicitada = bo.Cantidad / BePresentacion.Factor
                                        Else
                                            Throw New Exception("No se obtuvo el detalle de la presentación para el código de presentación: " & bo.IdPresentacion)
                                        End If
                                    End If

                                    .Cantidad_Recibida = 0.0
                                    .Fecha_real_vence = bo.Fecha_vence
                                    .User_agr = AP.UsuarioAp.IdUsuario
                                    .Fec_agr = Now
                                    .User_mod = AP.UsuarioAp.IdUsuario
                                    .Fec_mod = Now
                                    .Activo = True
                                    .IsNew = True

                                End If

                            End With

                            vIndicador = 8.1

                            pListBePickingUbic.Add(ObjU)

                            If ObjProducto.IsNew = False AndAlso ObjProducto.IdProducto > 0 Then
                                Set_Stock_Parametro(bo.IdStock)
                            End If

                            vIndicador = 8.2

                        Next

                    End If

                End If

                vIndicador = 8

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message & " Indicador: " & vIndicador)
            clsLnLog_error_wms.Agregar_Error(vMsgError & " Indicador: " & vIndicador)
            Throw ex
        End Try

    End Sub

    Private Sub Set_Stock_Parametro(ByVal pIdStock As Integer)

        Try
            '#CKFK 20180208 09:00AM se puso esto en comentario porque los parámetros deben guardarse en la lista de parámetros BeListPickingParam
            'Dim lParametros As New List(Of clsBeTrans_picking_det_parametros)
            Dim BeParametros As New clsBeTrans_picking_det_parametros

            lStockP = clsLnStock_parametro.Get_All_By_IdStock(pIdStock)

            For Each Obj As clsBeStock_parametro In lStockP

                BeParametros = New clsBeTrans_picking_det_parametros
                'BeParametros.IdParametroPicking = clsLnTrans_picking_det_parametros.MaxID()
                BeParametros.IdPickingDet = ObjBePickingDet.IdPickingDet
                BeParametros.IdProductoParametro = Obj.IdProductoParametro
                BeParametros.Valor_texto = Obj.Valor_texto
                BeParametros.Valor_numerico = Obj.Valor_numerico
                BeParametros.Valor_logico = Obj.Valor_logico
                BeParametros.Valor_fecha = Obj.Valor_fecha
                BeParametros.Fec_agr = Now
                BeParametros.User_agr = AP.UsuarioAp.Nombres
                BeParametros.IsNew = True
                BeListPickingParam.Add(BeParametros)

            Next

            '#CKFK 20180208 09:00AM se puso esto en comentario porque los parámetros no se pueden guardar hasta que esté creado el picking
            'clsLnTrans_picking_det_parametros.Guardar(lParametros)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Cargar_Operador(ByVal pIndex As Integer, Optional ByVal pIdOperadorBodega As Integer = 0)

        Try

            Dim DT As DataTable = clsLnOperador_bodega.Get_All_By_IdBodega_DT(cmbBodegas.EditValue)

            If Not DT Is Nothing Then

                Dim DgCombo As New DataGridViewComboBoxCell()
                DgCombo = TryCast(dgridDetalleManufactura.Rows(pIndex).Cells("OperadorBodega"), DataGridViewComboBoxCell)

                DgCombo.DataSource = DT
                DgCombo.ValueMember = "IdOperadorBodega"
                DgCombo.DisplayMember = "Nombres"

                If pIdOperadorBodega > 0 Then
                    DgCombo.Value = pIdOperadorBodega
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Set_Formato_Grid_Manufactura()

        Try

            If grdvPickingUbic.Columns.Count > 0 Then

                If grdvPickingUbic.RowCount > 0 Then

                    'Create and setup the first summary item.
                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "cantidad_esperada",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("cantidad_esperada")}
                    grdvPickingUbic.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "cantidad_recibida",
                        .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}",
                        .ShowInGroupColumnFooter = grdvPickingUbic.Columns("cantidad_recibida")}
                    grdvPickingUbic.GroupSummary.Add(item1)

                    grdvPickingUbic.ExpandAllGroups()
                    grdvPickingUbic.BestFitColumns()

                    lblRegs.Caption = String.Format("Registros: {0}", grdvPickingUbic.RowCount)

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub grdPickingUbic_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles grdvPickingUbic.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        Try

            Dim View1 As GridView = sender

            If View1.Columns Is Nothing Then Exit Sub
            If View1.Columns.Count = 0 Then Exit Sub

            'Dim Operador_Asignado As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Operador_Asignado"))), "0", View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("Operador_Asignado")))
            Dim IdManufacturaEnc As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdManufacturaEnc"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("IdManufacturaEnc")))

            Dim vCantidadSolicitada As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("cantidad_esperada"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("cantidad_esperada")))
            Dim vCantidadManufacturada As Object = IIf(IsDBNull(View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("cantidad_recibida"))), 0, View1.GetRowCellDisplayText(e.RowHandle, View1.Columns("cantidad_recibida")))

            If IdManufacturaEnc <> "" Then

                If IdManufacturaEnc > 0 Then



                    If (Val(vCantidadSolicitada) > Val(vCantidadManufacturada) OrElse Val(vCantidadManufacturada) = 0) Then

                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                        e.Appearance.BackColor = Color.Salmon
                        e.Appearance.BackColor2 = Color.SeaShell
                        e.Appearance.ForeColor = Color.Black

                    ElseIf Val(vCantidadManufacturada) < Val(vCantidadSolicitada) Then

                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                        e.Appearance.BackColor = Color.Yellow
                        e.Appearance.BackColor2 = Color.White
                        e.Appearance.ForeColor = Color.Black

                    ElseIf Val(vCantidadManufacturada) = Val(vCantidadSolicitada) Then

                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                        e.Appearance.BackColor = Color.LimeGreen
                        e.Appearance.BackColor2 = Color.White
                        e.Appearance.ForeColor = Color.Black

                    Else

                        e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.BackColor2 = Color.White
                        e.Appearance.ForeColor = Color.Black

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                  Text,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Sub SetDatataTableManufactura()

        DTManufacturaDet.Columns.Add("IdManufacturaDet", GetType(Int32)).ReadOnly = True
        DTManufacturaDet.Columns.Add("IdManufacturaEnc", GetType(Int32)).ReadOnly = True
        DTManufacturaDet.Columns.Add("IdPedidoDet", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("IdPropietarioBodega", GetType(Int32)).ReadOnly = True
        DTManufacturaDet.Columns.Add("IdProductoBodega", GetType(Int32)).ReadOnly = True
        DTManufacturaDet.Columns.Add("codigo_producto", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("nombre_producto", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("Manufactura", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("cantidad_esperada", GetType(Double)).ReadOnly = True
        DTManufacturaDet.Columns.Add("cantidad_recibida", GetType(Double)).ReadOnly = True
        DTManufacturaDet.Columns.Add("hora_inicial", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("hora_final", GetType(String)).ReadOnly = True
        DTManufacturaDet.Columns.Add("tiempo_transcurrido", GetType(Integer)).ReadOnly = True


    End Sub

    Private Sub cmbBodegas_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodegas.EditValueChanged

        Try

            If cmbBodegas.EditValue > -1 Then

                If cmbBodegas.EditValue > 0 Then

                    cmbPropietario.Properties.DataSource = Nothing
                    IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodegas.EditValue)

                    '#EJC20220301: Set BeBodega en Nuevo.
                    Dim pIdBodega As Integer = cmbBodegas.EditValue
                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)
                    Validar_Operadores()

                End If

            End If

            Set_IdUbicacion_Picking()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            If cmbPropietario.ItemIndex > -1 Then
                If cmbBodegas.Text <> "" Then
                    cmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodegas.EditValue, cmbPropietario.EditValue)
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Set_IdUbicacion_Picking()

        Try

            Dim pIdBodega As Integer = cmbBodegas.EditValue
            Dim Obj = New clsBeBodega() With {.IdBodega = pIdBodega}
            clsLnBodega.Obtener(Obj)
            'txtIdUbicacion.Text = Obj.Ubic_picking
            'txtIdUbicacion_Validated(txtIdUbicacion, Nothing)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        cmdGuardar.Enabled = False
        Process_Guardar_Manufactura(True)
        cmdGuardar.Enabled = True
    End Sub

    Private Sub Process_Guardar_Manufactura(Optional ByVal Preguntar As Boolean = True)

        Try

            If Valida_Datos() Then

                If Preguntar Then

                    If Not XtraMessageBox.Show("¿Guardar Tarea Manufactura?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Exit Sub
                    End If

                End If

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                If Guardar_Manufactura() Then

                    Modo = TipoTrans.Editar

                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show("Se guardó la tarea de manufactura", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'Recargar_Detalle_Manufactura()

                    If InvokeListar_Manufactura IsNot Nothing Then
                        InvokeListar_Manufactura.Invoke
                    End If

                    If Modal Then
                        DialogResult = DialogResult.OK
                    Else
                        Close()
                    End If

                End If

            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Function Valida_Datos() As Boolean

        Valida_Datos = False

        Try

            dgridDetalleManufactura.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgridDetalleManufactura.EndEdit()

            If cmbBodegas.ItemIndex = -1 Then
                XtraTabControl1.SelectedTabPage = XtratabPageDato
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbPropietario.ItemIndex = -1 Then
                XtraTabControl1.SelectedTabPage = XtratabPageDato
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbTipoManufactura.ItemIndex = -1 Then
                XtraTabControl1.SelectedTabPage = XtratabPageDato
                XtraMessageBox.Show("Seleccione un tipo de manufactura.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf BeListPickingDet Is Nothing OrElse BeListPickingDet.Count > 0 = False Then
                XtraTabControl1.SelectedTabPage = XtratabPageDato
                XtraMessageBox.Show("Debe asociar un pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not ValidaFilas() Then
                XtraTabControl1.SelectedTabPage = XtratabPagePedido
            Else
                Valida_Datos = True
            End If

        Catch ex As Exception

            Valida_Datos = False

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function ValidaDetalleOperadores() As Boolean

        ValidaDetalleOperadores = True

        If dgridDetalleManufactura.Rows.Count > 0 Then
            For i As Integer = 0 To dgridDetalleManufactura.Rows.Count - 1
                If dgridDetalleManufactura.Rows(i).Cells("Producto").Value IsNot Nothing AndAlso dgridDetalleManufactura.Rows(i).Cells("OperadorBodega").Value Is Nothing Then
                    XtraTabControl1.SelectedTabPage = XtratabPagePedido
                    XtraMessageBox.Show(String.Format("Debe seleccionar Operador en la fila ", i + 1),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ValidaDetalleOperadores = False
                End If
            Next
        End If

    End Function

    Private Function ValidaFilas() As Boolean

        ValidaFilas = True

        Try

            For i As Integer = 0 To dgridDetalleManufactura.Rows.Count - 1
                If dgridDetalleManufactura.Rows(i).Cells("Producto").Value IsNot DBNull.Value AndAlso dgridDetalleManufactura.Rows(i).Cells("Producto").Value IsNot Nothing Then
                    If dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value Is DBNull.Value OrElse dgridDetalleManufactura.Rows(i).Cells("ClienteDias").Value Is Nothing Then
                        XtraTabControl1.SelectedTabPage = XtratabPagePedido
                        'Throw New Exception(String.Format("Ingrese días Cliente en fila {0}", i + 1))

                        XtraMessageBox.Show(String.Format("Ingrese días Cliente en fila {0}", i + 1),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        ValidaFilas = False
                    End If
                End If
            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Function Guardar_Manufactura() As Boolean

        Guardar_Manufactura = False

        Try

            '#EJC20220303: Prevent data loss if the HH has already pickings on progres.
            'Existen_Diferencias_Memoria_vs_BD(vContinuar)

            BeManufacturaEnc.IdPedidoEnc = pTransIdPedidoEnc
            BeManufacturaEnc.Fec_agr = Now
            BeManufacturaEnc.Fec_mod = Now
            BeManufacturaEnc.User_agr = AP.UsuarioAp.IdUsuario
            BeManufacturaEnc.User_mod = AP.UsuarioAp.IdUsuario
            BeManufacturaEnc.IdBodega = cmbBodegas.EditValue
            BeManufacturaEnc.IdPropietarioBodega = cmbPropietario.EditValue
            BeManufacturaEnc.Fecha_manufactura = Now
            BeManufacturaEnc.Hora_ini = dtmHoraIhh.Value
            BeManufacturaEnc.Hora_fin = dtmHoraFhh.Value
            BeManufacturaEnc.Escaneo = True
            BeManufacturaEnc.Activo = True
            'BeManufacturaEnc.IdTipoManufactura = cmbTipoManufactura.EditValue
            BeManufacturaEnc.IdTipoManufactura = pPedidoEnc.IdTipoManufactura
            BeManufacturaEnc.Estado = "Nuevo"

            If listTransPeDet.Count > 0 Then

                ListBeManufacturaDet = New List(Of clsBeTrans_manufactura_det)

                For Each pTransPeDet In listTransPeDet

                    Dim BeManufactaraDet = New clsBeTrans_manufactura_det
                    BeManufactaraDet.IsNew = True
                    BeManufactaraDet.IdPropietarioBodega = cmbPropietario.EditValue
                    BeManufactaraDet.User_agr = AP.UsuarioAp.IdUsuario
                    BeManufactaraDet.User_mod = AP.UsuarioAp.IdUsuario
                    BeManufactaraDet.Fec_agr = Now
                    BeManufactaraDet.Fec_mod = Now
                    BeManufactaraDet.IdPedidoDet = pTransPeDet.IdPedidoDet
                    BeManufactaraDet.IdProductoBodega = pTransPeDet.IdProductoBodega
                    BeManufactaraDet.Codigo_producto = pTransPeDet.Codigo_Producto
                    BeManufactaraDet.Nombre_producto = pTransPeDet.Nombre_producto
                    BeManufactaraDet.Cantidad_esperada = pTransPeDet.Cantidad
                    BeManufactaraDet.Cantidad_recibida = 0
                    ListBeManufacturaDet.Add(BeManufactaraDet)
                Next

                Guardar_Manufactura = clsLnTrans_manufactura_enc.Guardar(BeManufacturaEnc, ListBeManufacturaDet)

            End If

            If Not listTransPeDet.Count = 0 Then

                '#GT27022023: se guarda log de manufactura
                If BeManufacturaEnc.IsNew Then
                    clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdManufacturaEnc: " & BeManufacturaEnc.IdManufacturaEnc)
                Else
                    clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdManufacturaEnc: " & BeManufacturaEnc.IdManufacturaEnc)
                End If

                'Cargar_Datos()

            Else
                Throw New Exception("Al parecer el proceso de manufactura no tiene líneas, no se podrá guardar la transacción.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub Set_Menu_Edicion()

        cmdGuardar.Enabled = False
        cmdImprimir.Enabled = True

        If BePickingEnc IsNot Nothing Then
            mnuEliminar.Enabled = BePickingEnc.Estado <> "Despachado"
        End If

    End Sub

    Private Sub Bloquea_Objetos(ByVal bloquear As Boolean)

        cmbBodegas.ReadOnly = True
        cmbTipoManufactura.ReadOnly = True
        dtmFechaManufactura.ReadOnly = True
        cmbPropietario.ReadOnly = True
        dtmFechaTarea.ReadOnly = True
        dtmHoraI.Enabled = False
        dtmHoraF.Enabled = False
        lnkAgregarPedido.Enabled = False
        cmdGuardar.Enabled = False
        cmdCerrar.Enabled = bloquear
        txtScanner.Enabled = bloquear
        txtCantidad.Enabled = bloquear
        cmdEliminar.Enabled = bloquear

    End Sub

    Private Sub grdvPickingUbic_DoubleClick(sender As Object, e As EventArgs) Handles grdvPickingUbic.DoubleClick
        Procesar_manufactura()
    End Sub

    Private Sub Procesar_manufactura()

        Try
            Dim lBeManufacturaPicking As New List(Of clsBeTrans_manufactura_picking)

            If (grdvPickingUbic.RowCount > 0) Then

                Dim Dr As DataRowView = grdvPickingUbic.GetFocusedRow

                If Dr Is Nothing Then Return

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando...")

                Dim lSelectionIndex As Integer = grdvPickingUbic.FocusedRowHandle

                lBeManufacturaPicking = clsLnTrans_manufactura_picking.Get_All_By_IdPedidoDet_IdManufactura(Dr.Item("IdManufacturaEnc"),
                                                                                                            Dr.Item("IdPedidoDet"))

                BeManufacturaDet.IdManufacturaDet = Dr.Item("IdManufacturaDet")
                clsLnTrans_manufactura_det.GetSingle(BeManufacturaDet)

                If lBeManufacturaPicking IsNot Nothing Then
                    Dim ManufacturaPicking As New frmManufacturaPicking
                    ManufacturaPicking.pBeManufacturaEnc = BeManufacturaEnc
                    ManufacturaPicking.pBeManufacturaDet = BeManufacturaDet
                    ManufacturaPicking.pBePedidoEnc = pPedidoEnc
                    ManufacturaPicking.ShowDialog()
                    ManufacturaPicking.Dispose()
                Else
                    XtraMessageBox.Show("No se carg el producto para procesar.", "Manufactura", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim cantidad = contador + Val(txtCantidad.Text)

            If cantidad > Val(txtCantidadEsperada.Text) Then
                DxErrorProvider1.SetError(txtCantidad, "La cantidad supera lo esperado.")
            Else
                Procesar_Escaneo(Val(txtCantidadRegistrada.Text) = 0)
            End If

        End If

    End Sub

    Private Sub txtScanner_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanner.KeyDown, txtCantidad.KeyDown

        Try

            If e.KeyCode = Keys.Enter Then

                If txtScanner.Text.Trim() <> String.Empty Then
                    Scan_Barra()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                              Text,
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' #EF202403041451: Se busca el producto y se inserta por defecto con cantidad 1.
    ''' </summary>
    ''' 
    Dim vBeManufacturaDet As New clsBeTrans_manufactura_det
    Dim ProductoenPicking As New clsBeTrans_picking_ubic
    Dim contador As Integer = 0

    Private Sub Scan_Barra()

        Try

            Dim barra As String = txtScanner.Text.Replace("$", "")
            Dim vPendientes As Integer = 0

            vPendientes = clsLnTrans_manufactura_enc.Get_Pendientes_By_IdManufacturaEnc(lblCodigo.Text)

            If vPendientes = 0 Then

                If XtraMessageBox.Show("Ya no hay productos pendientes ¿Finalizar la tarea?",
                                                         Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Finalizar_Tarea(False)

                End If
            Else

                If String.IsNullOrEmpty(barra) Then
                    DxErrorProvider1.SetError(txtScanner, "Ingrese código/licencia de producto")
                Else

                    Dim ListaProductoenPicking = New List(Of clsBeTrans_picking_ubic)
                    ListaProductoenPicking = pListBePickingUbic.FindAll(Function(x) x.CodigoProducto = barra OrElse x.Lic_plate = barra)

                    For Each Prod In ListaProductoenPicking

                        '#GT25032024: buscamos el codigo/barra en el detalle del pedido
                        ProductoenPicking = New clsBeTrans_picking_ubic
                        ProductoenPicking = Prod

                        If ProductoenPicking IsNot Nothing Then

                            vBeManufacturaDet = New clsBeTrans_manufactura_det
                            vBeManufacturaDet = ListBeManufacturaDet.Find(Function(x) x.Codigo_producto = ProductoenPicking.CodigoProducto AndAlso
                                                                              x.Cantidad_esperada <> x.Cantidad_recibida)

                            If vBeManufacturaDet IsNot Nothing Then

                                Dim pManufaturaPicking As New clsBeTrans_manufactura_picking
                                pManufaturaPicking.IdManufacturaEnc = vBeManufacturaDet.IdManufacturaEnc
                                pManufaturaPicking.IdProductoBodega = vBeManufacturaDet.IdProductoBodega

                                Mostrar_Datos()

                                If BeManufacturaEnc.Escaneo Then
                                    txtCantidad.Text = 1
                                    txtCantidad.Enabled = False
                                    Procesar_Escaneo(Val(txtCantidadRegistrada.Text) = 0)
                                Else
                                    txtCantidad.Focus()
                                End If

                                Exit For

                            End If

                        Else
                            DxErrorProvider1.SetError(txtScanner, "La barra leída no pertenece a la tarea en proceso.")
                        End If

                    Next

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Dim pCantidad As Integer = 0

    Private Sub Procesar_Escaneo(ByVal EsPrimero As Boolean)

        Try

            If Not EsPrimero Then
                txtCantidad.Text = Val(txtCantidadEsperada.Text) - Val(txtCantidadRegistrada.Text)
            End If

            If Val(txtCantidad.Text) > 0 Then

                '#GT06032024: validamos que no se registre mas de lo esperado
                Dim pTotal = Val(txtCantidad.Text) + Val(txtCantidadRegistrada.Text)

                If pTotal <= Val(txtCantidadEsperada.Text) Then

                    pCantidad = Val(txtCantidad.Text)

                    Dim BeManufacturaPicking As New clsBeTrans_manufactura_picking
                    Dim pTransManufacturaDet As New clsBeTrans_manufactura_det
                    BeManufacturaPicking = New clsBeTrans_manufactura_picking
                    BeManufacturaPicking.IdManufacturaDet = vBeManufacturaDet.IdManufacturaDet
                    BeManufacturaPicking.IdManufacturaEnc = vBeManufacturaDet.IdManufacturaEnc
                    BeManufacturaPicking.IdPedidoDet = vBeManufacturaDet.IdPedidoDet
                    BeManufacturaPicking.IdProductoBodega = vBeManufacturaDet.IdProductoBodega
                    BeManufacturaPicking.Cantidad = txtCantidad.Text
                    BeManufacturaPicking.Licencia = ProductoenPicking.Lic_plate
                    BeManufacturaPicking.Licencia_manufactura = ""
                    BeManufacturaPicking.Codigo_barra = txtScanner.Text
                    BeManufacturaPicking.User_agr = AP.UsuarioAp.IdUsuario
                    BeManufacturaPicking.User_mod = AP.UsuarioAp.IdUsuario
                    BeManufacturaPicking.Fec_agr = Now
                    BeManufacturaPicking.Fec_mod = Now
                    pTransManufacturaDet.IdManufacturaEnc = vBeManufacturaDet.IdManufacturaEnc
                    pTransManufacturaDet.IdManufacturaDet = vBeManufacturaDet.IdManufacturaDet
                    pTransManufacturaDet.IdPedidoDet = vBeManufacturaDet.IdPedidoDet
                    pTransManufacturaDet.Cantidad_recibida = pCantidad + vBeManufacturaDet.Cantidad_recibida

                    clsLnTrans_manufactura_picking.Guardar(BeManufacturaEnc, BeManufacturaPicking, pTransManufacturaDet, pCantidad)

                    contador += pCantidad
                    txtCantidadRegistrada.Text = contador
                    txtCantidad.Text = "1"
                    txtScanner.Text = ""
                    txtScanner.Focus()

                    Recargar_Detalle_Manufactura()

                Else
                    DxErrorProvider1.SetError(txtScanner, "No es válido registrar mas de lo esperado.")
                End If

            Else
                DxErrorProvider1.SetError(txtCantidad, "Ingrese cantidad > 0.")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Mostrar_Datos()
        Try
            Dim pPedidoDet = New clsBeTrans_pe_det
            pPedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoDet(vBeManufacturaDet.IdPedidoDet)

            Dim pStockRes As List(Of clsBeStock_res) = clsLnStock_res.Get_All_Stock_Res_By_IdPedidoDet(vBeManufacturaDet.IdPedidoDet, pPedidoDet.IdPedidoEnc)

            If pStockRes IsNot Nothing Then

                txtDescripcionProducto.Text = vBeManufacturaDet.Nombre_producto
                txtCantidadEsperada.Text = vBeManufacturaDet.Cantidad_esperada
                txtCantidadRegistrada.Text = vBeManufacturaDet.Cantidad_recibida
                If pStockRes.Count > 0 Then
                    txtLote.Text = IIf(pStockRes.Count > 0, pStockRes.First.Lote, "")
                End If
                contador = vBeManufacturaDet.Cantidad_recibida

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Recargar_Detalle_Manufactura()

        Dim clsTransaccion As New clsTransaccion

        Try

            clsTransaccion.Begin_Transaction()

            '#GT26022024: cargo detalle de la manufactura
            ListBeManufacturaDet = clsLnTrans_manufactura_det.Get_Detalle_By_IdManufacturaEnc(BeManufacturaEnc.IdManufacturaEnc,
                                                                                                  clsTransaccion.lConnection,
                                                                                                  clsTransaccion.lTransaction)

            If ListBeManufacturaDet IsNot Nothing Then

                DTManufacturaDet.Clear()

                Get_Manufactura_Det(ListBeManufacturaDet)

            End If

            clsTransaccion.Commit_Transaction()
            clsTransaccion.Close_Conection()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdCerrar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCerrar.ItemClick
        cmdCerrar.Enabled = False
        Finalizar_Tarea()
        cmdCerrar.Enabled = True
    End Sub

    Private Sub Finalizar_Tarea(Optional ByVal Preguntar As Boolean = True)

        Dim vEjecutar As Boolean = True
        Dim vPendientes As Integer = 0

        Try

            vPendientes = clsLnTrans_manufactura_enc.Get_TipoManufactura_By_IdPedidoDet(lblCodigo.Text)

            If vPendientes > 0 Then

                If XtraMessageBox.Show("¿Hay productos pendientes de manufacturar, finalizar la tarea de todas formas?",
                                      Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then

                    Return

                End If

            End If

            If Preguntar Then

                If XtraMessageBox.Show(String.Format("¿Desea Cerrar la tarea de manufactura?") _
                           , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then


                    vEjecutar = True
                Else
                    txtScanner.Focus()
                    vEjecutar = False
                End If

            End If

            If vEjecutar Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Cerrando Tarea...")

                Cerrar()

                If InvokeListar_Manufactura IsNot Nothing Then
                    InvokeListar_Manufactura.Invoke
                End If

                If Modal Then
                    DialogResult = DialogResult.OK
                Else
                    Close()
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Cerrar()

        Dim clsTransaccion As New clsTransaccion

        Try

            clsLnTrans_manufactura_enc.Actualizar_Encabezado(BeManufacturaEnc.IdManufacturaEnc, "Cerrado",
                                                             clsTransaccion.lConnection,
                                                             clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()
            clsTransaccion.Close_Conection()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        cmdCerrar.Enabled = False
        Anular_Tarea()
        cmdCerrar.Enabled = True
    End Sub

    Private Sub Anular_Tarea()

        Try

            If XtraMessageBox.Show(String.Format("¿Desea Anular la tarea de manufactura?") _
                           , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then


                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Anulando Tarea...")

                Anular()

                If InvokeListar_Manufactura IsNot Nothing Then
                    InvokeListar_Manufactura.Invoke
                End If

                If Modal Then
                    DialogResult = DialogResult.OK
                Else
                    Close()
                End If

            Else
                txtScanner.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Anular()

        Dim clsTransaccion As New clsTransaccion

        Try

            clsLnTrans_manufactura_enc.Actualizar_Encabezado(BeManufacturaEnc.IdManufacturaEnc, "Anulado",
                                                             clsTransaccion.lConnection,
                                                             clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()
            clsTransaccion.Close_Conection()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
