Imports System
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.Data.Linq.Helpers

Partial Public Class clsLnTrans_pe_enc

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPedidoEnc),0) FROM trans_pe_enc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20200125: Se utiliza en I_Nav_Transacciones_Out para interface MI3.
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function GetSingle(ByVal pIdPedidoEnc As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_enc

        GetSingle = Nothing

        Try

            Dim vSQ As String = ""

            vSQ = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    Cargar(vPedidoEnc, lRow)

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If


                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega =
                    clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                        lConnection,
                                                                        lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lRow("IdMotivoDevolucion")), 0, lRow("IdMotivoDevolucion"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, lConnection, lTransaction)
                    End If

                    For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle

                        PeDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(PeDet.IdPedidoDet,
                                                                                    PeDet.IdPedidoEnc,
                                                                                    lConnection,
                                                                                    lTransaction)

                        If Not vPedidoEnc.Picking Is Nothing Then
                            PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = PeDet.IdPedidoDet)
                        End If

                    Next

                    Return vPedidoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Documento_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As Boolean

        Existe_Documento_By_IdPedidoEnc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM VW_Get_Single_Pedido WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Existe_Documento_By_IdPedidoEnc = True

                End If

            End Using

            lTransaction.Commit()


        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM VW_Get_Single_Pedido WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(vPedidoEnc, lRow)

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, lRow("IdCliente"))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, lRow("IdCliente"))
                    vPedidoEnc.Cliente.Codigo = IIf(IsDBNull(lRow("Codigo_Cliente")), "", lRow("Codigo_Cliente"))
                    vPedidoEnc.Cliente.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Cliente")), "", lRow("Nombre_Cliente"))
                    vPedidoEnc.Cliente.Es_bodega_recepcion = IIf(IsDBNull(lRow("Es_bodega_recepcion")), False, lRow("Es_bodega_recepcion"))
                    vPedidoEnc.Cliente.Es_Bodega_Traslado = IIf(IsDBNull(lRow("Es_Bodega_Traslado")), False, lRow("Es_Bodega_Traslado"))
                    vPedidoEnc.Cliente.Control_Ultimo_Lote = IIf(IsDBNull(lRow("control_ultimo_lote_cliente")), False, lRow("control_ultimo_lote_cliente"))
                    vPedidoEnc.Cliente.IdUbicacionVirtual = IIf(IsDBNull(lRow("IdUbicacionVirtual")), False, lRow("IdUbicacionVirtual"))
                    vPedidoEnc.Cliente.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.Cliente.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, lRow("IdPropietarioBodega"))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, lRow("IdPropietarioBodega"))
                    vPedidoEnc.PropietarioBodega.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.PropietarioBodega.Propietario.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Propietario")), "", lRow("Nombre_Propietario"))

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = IIf(IsDBNull(lRow("IdTipoPedido")), 1, lRow("IdTipoPedido"))
                        vPedidoEnc.IdTipoPedido = IIf(IsDBNull(lRow("IdTipoPedido")), 1, lRow("IdTipoPedido"))
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        vPedidoEnc.TipoPedido = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(vPedidoEnc.TipoPedido.IdTipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.RoadIdRutaDespacho = IIf(IsDBNull(lRow("RoadIdRutaDespacho")), 0, CType(lRow("RoadIdRutaDespacho"), Integer))
                    vPedidoEnc.RoadIdVendedorDespacho = IIf(IsDBNull(lRow("RoadIdVendedorDespacho")), 0, CType(lRow("RoadIdVendedorDespacho"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.HoraEntregaDesde = IIf(IsDBNull(lRow("HoraEntregaDesde")), Now, lRow("HoraEntregaDesde"))
                    vPedidoEnc.HoraEntregaHasta = IIf(IsDBNull(lRow("HoraEntregaHasta")), Now, lRow("HoraEntregaHasta"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))
                    vPedidoEnc.Observacion = IIf(IsDBNull(lRow("Observacion")), "", lRow("Observacion"))
                    vPedidoEnc.Fecha_Preparacion = IIf(IsDBNull(lRow("Fecha_Preparacion")), Now, lRow("Fecha_Preparacion"))
                    vPedidoEnc.IdTipoManufactura = IIf(IsDBNull(lRow("IdTipoManufactura")), 0, lRow("IdTipoManufactura"))
                    vPedidoEnc.Bodega_Origen = IIf(IsDBNull(lRow("Bodega_Origen")), "", lRow("Bodega_Origen"))
                    vPedidoEnc.Bodega_Destino = IIf(IsDBNull(lRow("Bodega_Destino")), "", lRow("Bodega_Destino"))
                    vPedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = IIf(IsDBNull(lRow("Referencia_Documento_Ingreso_Bodega_Destino")), "", lRow("Referencia_Documento_Ingreso_Bodega_Destino"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lRow("IdMotivoDevolucion")), 0, lRow("IdMotivoDevolucion"))

                    vPedidoEnc.Guia_Transporte = IIf(IsDBNull(lRow("Guia_Transporte")), "", lRow("Guia_Transporte"))
                    vPedidoEnc.IdEmpresaTransporte = IIf(IsDBNull(lRow("IdEmpresaTransporte")), 0, lRow("IdEmpresaTransporte"))
                    vPedidoEnc.IdPiloto = IIf(IsDBNull(lRow("IdPiloto")), 0, lRow("IdPiloto"))

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, lConnection, lTransaction)
                    End If

                    Dim ListaStockRes = clsLnStock_res.fGet_All_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    If vPedidoEnc.Detalle.Count > 20 Then
                        ' Procesamiento paralelo para listas grandes
                        Parallel.ForEach(vPedidoEnc.Detalle, Sub(PeDet)
                                                                 PeDet.ListaStockRes = ListaStockRes.
                                                                     Where(Function(x) x.IdPedido = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                                                     ToList()

                                                                 If vPedidoEnc.Picking IsNot Nothing Then
                                                                     PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.
                                                                         Where(Function(x) x.IdPedidoEnc = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                                                         ToList()
                                                                 End If
                                                             End Sub)
                    Else
                        ' Procesamiento secuencial para listas pequeñas
                        For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle
                            PeDet.ListaStockRes = ListaStockRes.
                                Where(Function(x) x.IdPedido = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                ToList()

                            If vPedidoEnc.Picking IsNot Nothing Then
                                PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.
                                    Where(Function(x) x.IdPedidoEnc = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                    ToList()
                            End If
                        Next
                    End If


                End If

                GetSingle = vPedidoEnc

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC20200125: Utilizado en la HH, se cambió a vista para mejorar el rendimiento.
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc

        Get_Single_By_IdPedidoEnc = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT * FROM VW_Get_Single_Pedido WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Cliente")), "", lRow("Nombre_Cliente"))

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, CType(lRow("IdPropietario"), Integer))
                    vPedidoEnc.PropietarioBodega.Propietario.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Propietario")), "", lRow("Nombre_Propietario"))

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        vPedidoEnc.TipoPedido.Nombre = IIf(IsDBNull(lRow("Nombre_Tipo_Pedido")), "", ("Nombre_Tipo_Pedido"))
                        vPedidoEnc.TipoPedido.Permitir_Despacho_Parcial = IIf(IsDBNull(lRow("Permitir_Despacho_Parcial")), False, lRow("Permitir_Despacho_Parcial"))
                        vPedidoEnc.TipoPedido.Permitir_Despacho_Multiple = IIf(IsDBNull(lRow("Permitir_Despacho_Multiple")), False, lRow("Permitir_Despacho_Multiple"))
                        vPedidoEnc.TipoPedido.Empaque_Tarima = IIf(IsDBNull(lRow("Empaque_Tarima")), False, lRow("Empaque_Tarima"))
                        vPedidoEnc.TipoPedido.Asignar_Todos_Operadores = IIf(IsDBNull(lRow("Asignar_Todos_Operadores")), False, lRow("Asignar_Todos_Operadores"))
                        vPedidoEnc.TipoPedido.Verificar = IIf(IsDBNull(lRow("Verificar")), False, lRow("Verificar"))
                        vPedidoEnc.TipoPedido.Generar_Picking_Auto = IIf(IsDBNull(lRow("Generar_Picking_Auto")), False, lRow("Generar_Picking_Auto"))
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))
                    vPedidoEnc.Bodega_Destino = IIf(IsDBNull(lRow("Bodega_Destino")), "", lRow("Bodega_Destino"))
                    vPedidoEnc.Guia_Transporte = IIf(IsDBNull(lRow("Guia_Transporte")), "", lRow("Guia_Transporte"))
                    vPedidoEnc.IdEmpresaTransporte = IIf(IsDBNull(lRow("IdEmpresaTransporte")), 0, lRow("IdEmpresaTransporte"))
                    vPedidoEnc.IdPiloto = IIf(IsDBNull(lRow("IdPiloto")), 0, lRow("IdPiloto"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingleHH_By_IdPedidoEnc(vPedidoEnc.IdPickingEnc,
                                                                                               vPedidoEnc.IdPedidoEnc,
                                                                                               lConnection,
                                                                                               lTransaction)
                    End If

                    Return vPedidoEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_IdPicking_By_IdPedido(ByVal pIdPedidoEnc As Integer) As Integer

        Get_IdPicking_By_IdPedido = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT IdPickingEnc FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_IdPicking_By_IdPedido = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPedidoEnc As Integer,
                                     ByVal InfoPicking As Boolean) As clsBeTrans_pe_enc

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc() With {.IdPedidoEnc = lRow("IdPedidoEnc"),
                        .IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer)),
                        .IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))}

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))

                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))

                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))

                    '#EJC20190214_0108PM: Enviar estado para cargar detalle de picking en transacción..
                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                      vPedidoEnc.Estado,
                                                                                      0,
                                                                                      lConnection,
                                                                                      lTransaction)

                    Application.DoEvents()

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    '#EJC20171021_0526PM: Obtener el picking y el detalle.
                    If vPedidoEnc.IdPickingEnc <> 0 Then

                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc

                        '#CKFK20171026_05204PM_REF: Si parámetro InfoPicking es true ya no se vuelve a cargar la información del picking
                        If Not InfoPicking Then
                            '#CKFK20250227 Cambié el GetSingle por el GetSingleHH
                            'vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc,
                            '                                                      lConnection,
                            '                                                      lTransaction)
                            vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingleHH(vPedidoEnc.IdPickingEnc,
                                                                                    lConnection,
                                                                                    lTransaction)
                        End If

                    End If

                    Return vPedidoEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function
    Public Shared Function GetSingle_By_IdDespachoEnc(ByVal pIdPedidoEnc As Integer,
                                                      ByVal InfoPicking As Boolean,
                                                      ByVal IdDespachoEnc As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_enc

        GetSingle_By_IdDespachoEnc = Nothing

        Dim vSQ As String = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

        Try

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc() With {.IdPedidoEnc = lRow("IdPedidoEnc"),
                        .IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer)),
                        .IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))}

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))

                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))

                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))

                    '#EJC20190214_0108PM: Enviar estado para cargar detalle de picking en transacción..
                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                      vPedidoEnc.Estado,
                                                                                      IdDespachoEnc,
                                                                                      lConnection,
                                                                                      lTransaction)

                    Application.DoEvents()

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    '#EJC20171021_0526PM: Obtener el picking y el detalle.
                    If vPedidoEnc.IdPickingEnc <> 0 Then

                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc

                        '#CKFK20171026_05204PM_REF: Si parámetro InfoPicking es true ya no se vuelve a cargar la información del picking
                        If InfoPicking Then
                            vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc,
                                                                                  lConnection,
                                                                                  lTransaction)

                            If Not vPedidoEnc.Picking Is Nothing Then

                                For Each Det In vPedidoEnc.Detalle
                                    Det.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.FindAll(Function(x) x.IdPedidoEnc = pIdPedidoEnc AndAlso x.IdPedidoDet = Det.IdPedidoDet)
                                Next



                            End If
                        End If

                    End If

                    Return vPedidoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function GetAll(ByVal pActivo As Boolean,
                                  ByVal pFechaDel As Date,
                                  ByVal pFechaAl As Date,
                                  Optional ByVal pAnulado As Boolean = False,
                                  Optional ByVal IdBodega As Integer = 0,
                                  Optional ByVal pDespachado As Boolean = False,
                                  Optional ByVal pSinExistenciasWMS As Boolean = False,
                                  Optional ByVal pSinExistenciasERP As Boolean = False) As DataTable


        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = " SELECT * FROM VW_PEDIDOS_LIST WHERE IDBODEGA = @IDBODEGA "

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            If pAnulado And pDespachado Then
                vSQL += " AND (Estado = 'Anulado' Or Estado = 'Despachado') "
            ElseIf Not pAnulado And Not pDespachado Then
                vSQL += " AND Estado <> 'Anulado' AND Estado <> 'Despachado'"
            ElseIf pAnulado Then
                vSQL += " AND Estado = 'Anulado' "
            ElseIf Not pAnulado Then
                vSQL += " AND Estado <> 'Anulado' "
            ElseIf Not pDespachado Then
                vSQL += " AND Estado <> 'Despachado' "
            Else
                vSQL += " AND Estado = 'Despachado' "
            End If

            If pSinExistenciasERP Then
                vSQL += " AND (referencia IN (SELECT distinct no_pedido 
                                  FROM I_nav_transacciones_out
                                  WHERE auditar =1 And cantidad_pendiente > 0)) "
            End If

            If pSinExistenciasWMS Then
                vSQL += " And (referencia In (Select distinct enc.[No] 
                                  FROM i_nav_ped_traslado_det det inner join 
                                  i_nav_ped_traslado_enc enc on enc.No = det.NoEnc 
                                  WHERE det.Process_Result <> 'Ok' )) "
            End If


            vSQL += " AND cast(Fecha_Pedido AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180518 11:53 PM Agregué la función Enviado_A_ERP para poder saber si el pedido fue enviado a no al ERP
    Public Shared Function Get_Estado_Enviado_A_ERP(ByVal Referencia As String) As Boolean

        Get_Estado_Enviado_A_ERP = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT Enviado_A_ERP FROM Trans_pe_enc 
             Where(Referencia = @Referencia)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Estado_Enviado_A_ERP = IIf(IsDBNull(dt.Rows(0).Item("Enviado_A_ERP")), False, dt.Rows(0).Item("Enviado_A_ERP"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#CKFK 20180518 11:53 PM Agregué la función Actualizar_Estado_Enviado_A_ERP para poder actualizar el campo de si fue enviado el pedido o no al ERP transaccional
    Public Shared Sub Actualizar_Estado_Enviado_A_ERP(ByVal pIdPedidoEnc As Integer,
                                                      ByVal pEnviado_A_ERP As Boolean,
                                                      ByVal pUserMod As Integer,
                                                      ByVal pConnection As SqlConnection,
                                                      ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_pe_enc SET Enviado_A_ERP=@Enviado_A_ERP, user_mod = @user_mod, fec_mod = GetDate()
                                  WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.Parameters.AddWithValue("@user_mod", pUserMod)
                lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pUsrAgr:=pUserMod, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub
    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdPedidoEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean,
                                                           ByVal pUserMod As Integer,
                                                           Optional ByVal MarcarRegistrosNoEnviados As Boolean = True) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            Dim vSQL As String = "UPDATE trans_pe_enc SET Enviado_A_ERP=@Enviado_A_ERP, user_mod = @user_mod, fec_mod = GetDate()
                                  WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lCommand.Parameters.AddWithValue("@user_mod", pUserMod)
                        Actualizar_Estado_Enviado_A_ERP = lCommand.ExecuteNonQuery()

                        If MarcarRegistrosNoEnviados Then
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                                   pEnviado_A_ERP,
                                                                                                   pUserMod,
                                                                                                   lConnection,
                                                                                                   lTransaction)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pUsrAgr:=pUserMod, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pendiente_Registro_MI3() As List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeTrans_pe_enc)
            Const sp As String = "SELECT * FROM Trans_pe_enc WHERE ISNULL(Enviado_A_ERP,0) = 0  AND estado IN  ('Despachado','Verificado')  "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_pe_enc As New clsBeTrans_pe_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_pe_enc = New clsBeTrans_pe_enc
                Cargar(vBeTrans_pe_enc, dr)
                lReturnList.Add(vBeTrans_pe_enc)
            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function
    Public Shared Function Get_All_ActivosGet_All_Activos(ByVal pFechaDel As Date,
                                           ByVal pFechaAl As Date,
                                           ByVal ConPicking As Boolean,
                                           ByVal NoIncluir As String,
                                           Optional ByVal IdBodega As Integer = 0) As DataTable

        Dim lTable As New DataTable("Result")

        Dim vSQL As String = ""

        Try

            If ConPicking Then
                vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, SUM(Cantidad_Despachada) AS Cantidad_Despachada 
                                      FROM VW_Pedido WHERE IdPickingEnc > 0 {0} {1}",
                                     NoIncluir, " AND EstadoPedido Not In ('Despachado','Nuevo')
                                     AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0) ")
            Else
                vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             SUM(Cantidad_Verificada) AS Cantidad_Verificada, SUM(Cantidad_Despachada) AS Cantidad_Despachada
                                       FROM VW_Pedido WHERE IdPickingEnc = 0 {0}", NoIncluir)
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " AND IdBodega=@IdBodega "

            vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, Producto, Presentacion, UnidadMedida, Estado, Fecha, IdProducto, IdPickingEnc, EstadoPedido, IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Activos(ByVal pFechaDel As Date,
                                           ByVal pFechaAl As Date,
                                           ByVal ConPicking As Boolean,
                                           ByVal NoIncluir As String,
                                           ByVal IdBodega As Integer,
                                           ByVal EstadoDespachado As Boolean,
                                           ByVal IdPropietarioBodega As Integer,
                                           ByRef pIdUsuario As Integer,
                                           ByVal pActivo As Boolean) As DataTable

        Dim lTable As New DataTable("Result")
        Dim vSQL As String = ""

        Try

            Dim BeBodega As New clsBeBodega()
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(IdBodega)

            If ConPicking Then

                '#EJC20220328: Config CEALSA, mostrar LP en listado.
                If BeBodega.Mostrar_Area_En_HH Then

                    vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, CodigoCliente, NombreCliente,
                                             Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, 
                                             SUM(Cantidad_Despachada) AS Cantidad_Despachada, pr_clas.nombre as Clasificacion,
                                             Licencia, lote as Lote, fecha_vence as Vence
                                             FROM VW_Pedido vw
											 inner join producto pr on vw.Código = pr.codigo
											 left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo
                                             WHERE IdPickingEnc > 0 {0} {1}",
                                             NoIncluir, " AND EstadoPedido Not In ('Despachado','Nuevo')
                                             AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0)")

                Else

                    vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, 
                                             CodigoCliente, NombreCliente,Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, 
                                             SUM(Cantidad_Despachada) AS Cantidad_Despachada, pr_clas.nombre as Clasificacion
                                             FROM VW_Pedido vw
											 inner join producto pr on vw.Código = pr.codigo
											 left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo
                                             WHERE IdPickingEnc > 0 {0} {1}",
                                             NoIncluir, " AND EstadoPedido Not In ('Despachado','Nuevo')
                                             AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0)")

                End If

            Else
                '#CKFK20221013 Agregué el campo NombreCliente
                vSQL = "SELECT Pedido, referencia, IdPedidoDet, Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             SUM(Cantidad_Verificada) AS Cantidad_Verificada, SUM(Cantidad_Despachada) AS Cantidad_Despachada, 
                                             pr_clas.nombre as Clasificacion, NombreCliente
                                             FROM VW_Pedido vw
											 inner join producto pr on vw.Código = pr.codigo
											 left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo "

                If Not EstadoDespachado Then
                    vSQL += String.Format("WHERE IdPickingEnc = 0 {0}", NoIncluir)
                Else
                    vSQL += "WHERE 1 > 0"
                End If

            End If

            vSQL += String.Format(" And cast(Fecha As Date) BETWEEN {0} And {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " AND IdBodega=@IdBodega "

            If IdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            If EstadoDespachado Then
                vSQL += " And EstadoPedido='Despachado' "
            End If

            If pIdUsuario <> 0 Then
                vSQL += " And vw.user_agr=@IdUsuarioAgrego"
            End If

            If Not pActivo Then
                vSQL += " And EstadoPedido <> 'Anulado' "
            End If

            If BeBodega.Mostrar_Area_En_HH Then
                vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, 
                               Producto, Presentacion, UnidadMedida, 
                               Estado, Fecha, vw.IdProducto, IdPickingEnc, 
                               EstadoPedido, IdBodega, pr_clas.nombre, licencia, lote, fecha_vence, CodigoCliente, NombreCliente "
            Else
                vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, 
                               Producto, Presentacion, UnidadMedida, 
                               Estado, Fecha, vw.IdProducto, IdPickingEnc, 
                               EstadoPedido, IdBodega, pr_clas.nombre, CodigoCliente, NombreCliente "
            End If


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        If pIdUsuario <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUsuarioAgrego", pIdUsuario)
                        End If

                        If IdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        End If

                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Detalle(ByVal IdPedidoEnc As Integer) As Boolean

        Try

            Dim vSQL As String = "SELECT TOP(1) IdPedidoDet FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        Tiene_Detalle = lDT.Rows.Count > 0

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Eliminar_Encabezado_Pedido(ByVal IdPedidoEnc As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_pe_enc Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Eliminar_Encabezado_Pedido(ByVal IdPedidoEnc As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As Integer

        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete FROM Trans_pe_enc Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Anular_Pedido(ByVal IdPedidoEnc As Integer,
                                         ByVal pIdMotivoAnulacionBodega As Integer) As Boolean

        Anular_Pedido = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing

        Try

            lConnection.Open() : ltrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If clsLnStock_res.Eliminar_Stock_Res_By_IdPedidoEnc(IdPedidoEnc, lConnection, ltrans) Then
                Set_Estado_Anulado_Rec(IdPedidoEnc, pIdMotivoAnulacionBodega, lConnection, ltrans)
            Else
                Set_Estado_Anulado_Rec(IdPedidoEnc, pIdMotivoAnulacionBodega, lConnection, ltrans)
            End If

            ltrans.Commit()

            Anular_Pedido = True

        Catch ex As Exception
            If Not ltrans Is Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If Not ltrans Is Nothing Then ltrans.Dispose()
        End Try

    End Function
    Public Shared Sub Set_Estado_Anulado_Rec(ByVal pIdPedidoEnc As Integer,
                                             ByVal pIdMotivoAnulacionBodega As Integer,
                                             ByRef pConnection As SqlConnection,
                                             ByRef pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_pe_enc " &
                                 " SET estado='Anulado'," &
                                 " IdMotivoAnulacionBodega = @IdMotivoAnulacionBodega " &
                                 " WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@estado", "Anulado")
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacionBodega", pIdMotivoAnulacionBodega)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Eliminar_Pedido(ByVal IdPedidoEnc As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()
        Dim rowsAffected As Integer = 0

        Eliminar_Pedido = rowsAffected

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Elimina el detalle
            Dim sp As String = " Delete from Trans_pe_det " &
             "  Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected = cmd.ExecuteNonQuery()

            'Elimina el encabezado
            sp = " Delete from Trans_pe_enc" &
             "  Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            'Elimina el stock reservado
            sp = " Delete from stock_res" &
             "  Where(IdPedido = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            'Elimina la manufactura
            sp = " Delete from trans_manufactura_det" &
                 " Where IdManufacturaEnc IN (SELECT IdManufacturaEnc FROM trans_manufactura_enc WHERE IdPedidoEnc =  @IdPedidoEnc);
                   DELETE FROM trans_manufactura_enc WHERE IdPedidoEnc =  @IdPedidoEnc;"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
            lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Eliminar_Detalle_Pedido(ByVal IdPedidoEnc As Integer,
                                                   ByVal IdPedidoDet As Integer,
                                                   ByVal IdPickingEnc As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim rowsAffected As Integer = 0

        Dim lPickingDet As New List(Of clsBeTrans_picking_det)
        Dim lPickingDetFilterResult As New List(Of clsBeTrans_picking_det)
        Dim lBeStockPickeado As New List(Of clsBeStock)

        Dim BeStockPickeado As clsBeStock = Nothing
        Dim vIdUbicacionPicking As Integer = 0

        Eliminar_Detalle_Pedido = rowsAffected

        Dim pPickingEnc As New clsBeTrans_picking_enc()

        Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)

        Try

            '#20171015_1241AM: Hoy vine a la oficina y es un día sábado, ya es casi la 1 am y ya me voy jaja.
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If IdPickingEnc = 0 Then

                '#EJC20210811: Esto ocurre si anulan el picking y luego quieren liberar la línea del pedido.
                Dim vIdPickingEnc As Integer = clsLnTrans_picking_det.Get_IdPicking_Enc_By_IdPedidoDet(IdPedidoDet,
                                                                                                       IdPedidoEnc,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                IdPickingEnc = vIdPickingEnc

            Else
                pPickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)
            End If

            '#EJC20171022_0439PM: Aquí validé que si el pedido tiene picking asociado se elimina y restaura el stock para 
            'Poder ser reubicado posteriormente.
            If IdPickingEnc <> 0 Then

                If Not pPickingEnc Is Nothing Then

                    lPickingDet = pPickingEnc.ListaPickingDet.Where(Function(x) x.IdPedidoDet = IdPedidoDet AndAlso x.IdPedidoEnc = IdPedidoEnc).ToList()
                    ListaDetalleUbicacion = pPickingEnc.ListaPickingUbic.Where(Function(x) x.IdPedidoDet = IdPedidoDet AndAlso x.IdPedidoEnc = IdPedidoEnc).ToList()

                    vIdUbicacionPicking = clsLnTrans_picking_enc.Get_IdUbicacion_Picking(IdPickingEnc,
                                                                                         lConnection,
                                                                                         lTransaction)



                    If Not lPickingDet Is Nothing Then

                        lPickingDetFilterResult = lPickingDet.FindAll(Function(x) x.Cantidad_recibida > 0)

                        If Not lPickingDetFilterResult Is Nothing Then

                            If lPickingDetFilterResult.Count > 0 Then

                                For Each BePickingDet In lPickingDetFilterResult

                                    If BePickingDet.Cantidad_recibida > 0 Then

                                        If Not ListaDetalleUbicacion Is Nothing Then

                                            For Each StockPicking In ListaDetalleUbicacion.Where(Function(x) x.IdPickingDet = BePickingDet.IdPickingDet _
                                                                                                 AndAlso x.IdPickingEnc = BePickingDet.IdPickingEnc)

                                                Console.WriteLine(StockPicking.IdStock)

                                                BeStockPickeado = New clsBeStock
                                                BeStockPickeado.IdStock = StockPicking.IdStock
                                                BeStockPickeado = clsLnStock.GetSingle(BeStockPickeado.IdStock, lConnection, lTransaction)
                                                BeStockPickeado.Cantidad = StockPicking.Cantidad_Recibida
                                                BeStockPickeado.Peso = StockPicking.Peso_recibido
                                                BeStockPickeado.IdPedidoEnc = 0
                                                BeStockPickeado.IdPickingEnc = 0
                                                BeStockPickeado.IdUbicacion_anterior = BeStockPickeado.IdUbicacion ' Ubicación anterior guardar histórico de donde estaba.
                                                BeStockPickeado.IdUbicacion = vIdUbicacionPicking 'Nueva ubicación
                                                lBeStockPickeado.Add(BeStockPickeado)

                                            Next

                                        End If

                                    End If

                                    rowsAffected += clsLnTrans_picking_ubic.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                                    rowsAffected += clsLnTrans_picking_det.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                Next

                            End If

                        End If

                    End If

                Else
                    Throw New Exception("ERROR_202211022255: No se pudo obtener el picking: " & IdPickingEnc)
                End If

                rowsAffected += clsLnStock.Actualizar_Stock_Por_Productos_Pickeados(lBeStockPickeado,
                                                                                    lConnection,
                                                                                    lTransaction)

            End If

            rowsAffected += clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(IdPedidoEnc,
                                                                                                   IdPedidoDet,
                                                                                                   lConnection,
                                                                                                   lTransaction)

            '#CKFK20240820 Eliminamos el IdPedidoDet de la manufactura
            rowsAffected += clsLnTrans_manufactura_det.Eliminar_By_IdPedidoDet(IdPedidoDet,
                                                                              lConnection,
                                                                              lTransaction)

            rowsAffected += clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(IdPedidoEnc,
                                                                              IdPedidoDet,
                                                                              lConnection,
                                                                              lTransaction)

            '#EJC202207181029: Agregar aquí en el log que se eliminó una línea del pedido.
            If Not pPickingEnc Is Nothing Then
                '#MECR15102025: Se agrego bitacora de logs para pedidos
                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(pPickingEnc.IdBodega, lConnection, lTransaction)
                'clsLnLog_error_wms.Agregar_Error("PED_DEL_DET: Se eliminó el Idpedido: " & IdPedidoEnc & " con IdDetalle: " & IdPedidoDet & " con empresa: " & vIdEmpresa & " bodega: " & pPickingEnc.IdBodega)
                Dim vMsgDelete As String = "PED_DEL_DET: Se eliminó el Idpedido: " & IdPedidoEnc & " con IdDetalle: " & IdPedidoDet & " con empresa: " & vIdEmpresa & " bodega: " & pPickingEnc.IdBodega
                clsLnLog_error_wms_pe.Agregar_Error(vMsgDelete,
                                                    pIdEmpresa:=vIdEmpresa,
                                                    pIdBodega:=pPickingEnc.IdBodega,
                                                    pIdPedidoEnc:=IdPedidoEnc,
                                                    pCodigoProducto:=IdPedidoDet,
                                                    pConection:=lConnection,
                                                    pTransaction:=lTransaction)

            End If

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Sub ActualizarIdPickingEnc(ByVal pIdPickingEnc As Integer,
                                             ByVal pIdPedidoEnc As Integer,
                                             ByVal pConnection As SqlConnection,
                                             ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE Trans_pe_enc SET IdPickingEnc=@IdPickingEnc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    Public Shared Sub DespacharPedido(ByVal pIdPedidoEnc As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction)

        Try

            Dim sp As String = String.Format("UPDATE trans_pe_enc SET Estado='DESPACHADO' WHERE IdPedidoEnc={0}", pIdPedidoEnc)

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                lCommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Function Inserta_Encabezado(ByRef pPedido As clsBeTrans_pe_enc) As Boolean

        Inserta_Encabezado = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim ResultadoInsert As Integer = 0

            pPedido.IdPedidoEnc = MaxID(lConnection, lTransaction)

            pPedido.No_documento = Right("PE000000" & pPedido.IdPedidoEnc, 7)

            If pPedido.Bodega_Destino IsNot Nothing AndAlso
                pPedido.Bodega_Destino.Length > 0 AndAlso
                pPedido.Bodega_Destino.Contains("-") Then
                Dim pBodDestino As String() = pPedido.Bodega_Destino.Split("-")
                pPedido.Bodega_Destino = pBodDestino.GetValue(0)
            End If

            ResultadoInsert = Insertar(pPedido, lConnection, lTransaction)

            lTransaction.Commit()

            Inserta_Encabezado = ResultadoInsert > 0

        Catch ex1 As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function Inserta_Encabezado(ByRef pPedido As clsBeTrans_pe_enc,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Boolean

        Inserta_Encabezado = False

        Try

            Dim ResultadoInsert As Integer = 0

            pPedido.IdPedidoEnc = MaxID(lConnection, lTransaction)
            pPedido.No_documento = Right("PE000000" & pPedido.IdPedidoEnc, 7)

            If pPedido.Bodega_Destino IsNot Nothing AndAlso
                pPedido.Bodega_Destino.Length > 0 AndAlso
                pPedido.Bodega_Destino.Contains("-") Then
                Dim pBodDestino As String() = pPedido.Bodega_Destino.Split("-")
                pPedido.Bodega_Destino = pBodDestino.GetValue(0)
            End If

            ResultadoInsert = Insertar(pPedido, lConnection, lTransaction)
            Inserta_Encabezado = ResultadoInsert > 0

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function
    Public Shared Function Actualizar_Datos(ByVal pBeTransPeEnc As clsBeTrans_pe_enc,
                                            ByVal pListObjTD As List(Of clsBeTrans_pe_det),
                                            ByVal pBeTransPePol As clsBeTrans_pe_pol,
                                            ByVal pListObjServ As List(Of clsBeTrans_pe_servicios)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeTransPeEnc.IsNew Then
                pBeTransPeEnc.IdPedidoEnc = MaxID(lConnection, lTransaction)
                If pBeTransPeEnc.No_documento.ToString = "" Then pBeTransPeEnc.No_documento = Right("PE000000" & pBeTransPeEnc.IdPedidoEnc, 8)

                If pBeTransPeEnc.Bodega_Destino IsNot Nothing AndAlso
                   pBeTransPeEnc.Bodega_Destino.Length > 0 AndAlso
                   pBeTransPeEnc.Bodega_Destino.Contains("-") Then
                    Dim pBodDestino As String() = pBeTransPeEnc.Bodega_Destino.Split("-")
                    pBeTransPeEnc.Bodega_Destino = pBodDestino.GetValue(0)
                End If

                Insertar(pBeTransPeEnc, lConnection, lTransaction)
            Else
                If pBeTransPeEnc.Bodega_Destino IsNot Nothing AndAlso
                   pBeTransPeEnc.Bodega_Destino.Length > 0 AndAlso
                   pBeTransPeEnc.Bodega_Destino.Contains("-") Then
                    Dim pBodDestino As String() = pBeTransPeEnc.Bodega_Destino.Split("-")
                    pBeTransPeEnc.Bodega_Destino = pBodDestino.GetValue(0)
                End If
                Actualizar(pBeTransPeEnc, lConnection, lTransaction)
            End If

            For Each BeTransPeDet As clsBeTrans_pe_det In pListObjTD
                clsLnTrans_pe_det.Actualizar(BeTransPeDet, lConnection, lTransaction)
            Next

            '#CKFK20220703 Agregué esta validación para evitar stock reservados sin picking ubic
            If Tiene_Picking(pBeTransPeEnc.IdPedidoEnc, pBeTransPeEnc.IdPickingEnc, lConnection, lTransaction) Then
                If Not clsLnStock_res.Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado(pBeTransPeEnc.IdPedidoEnc, lConnection, lTransaction) Then
                    Throw New Exception("ERROR_20220701_0658A: La asociación de picking para stock reservado falló y esto generaría un picking incompleto en base a la reserva.")
                End If
            End If

            Guarda_Trans_pe_pol(pBeTransPeEnc.IdPedidoEnc, pBeTransPePol, lConnection, lTransaction)

            Guarda_Trans_pe_servicio(pListObjServ, lConnection, lTransaction)

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            lTransaction.Dispose()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function
    Private Shared Sub Guarda_Trans_pe_pol(ByVal IdPedidoEnc As Integer,
                                           ByVal BePoliza As clsBeTrans_pe_pol,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction)

        Try


            If BePoliza IsNot Nothing Then

                Dim ObjP As New clsLnTrans_pe_pol()
                Dim BePolizaExistente As New clsBeTrans_pe_pol()


                BePolizaExistente = clsLnTrans_pe_pol.GetSingleId(IdPedidoEnc,
                                                                  lConnection,
                                                                  lTransaction)

                If Not BePolizaExistente Is Nothing Then
                    If BePolizaExistente.numero_orden = "" Then
                        Throw New Exception("ERROR_20230818: El numero de orden está vacío en la poliza")
                    Else
                        If BePoliza.IsNew Then
                            'BePoliza.IsNew = False
                        End If
                    End If
                Else
                    If BePoliza.numero_orden = "" Then
                        Throw New Exception("ERROR_20230818: El numero de orden está vacío en la poliza")
                    End If
                End If

                If BePoliza.IsNew Then
                    BePoliza.IdOrdenPedidoPol = clsLnTrans_pe_pol.MaxID(IdPedidoEnc, lConnection, lTransaction)
                    BePoliza.IdOrdenPedidoEnc = IdPedidoEnc
                    clsLnTrans_pe_pol.Insertar(BePoliza, lConnection, lTransaction)
                Else
                    clsLnTrans_pe_pol.Actualizar(BePoliza, lConnection, lTransaction)
                End If

            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=IdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub
    Private Shared Sub Guarda_Trans_pe_servicio(ByVal pListObjServicio As List(Of clsBeTrans_pe_servicios),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If pListObjServicio IsNot Nothing Then

                Dim BeTransPeDetExistente As New clsBeTrans_pe_servicios

                Dim lMax As Integer = clsLnTrans_pe_servicios.MaxID(lConnection, lTransaction)

                For Each BeTransPeServicio As clsBeTrans_pe_servicios In pListObjServicio

                    If Not clsLnTrans_pe_servicios.Exist(BeTransPeServicio.IdServicio, BeTransPeServicio.IdOrdenPedidoEnc, lConnection, lTransaction) Then

                        lMax += 1

                        BeTransPeServicio.IdOrdenPedidoServicio = lMax

                        clsLnTrans_pe_servicios.Insertar(BeTransPeServicio, lConnection, lTransaction)

                    Else

                        clsLnTrans_pe_servicios.Actualizar_By_PE_IdServicio(BeTransPeServicio, lConnection, lTransaction)

                    End If

                Next

            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Single_By_Referencia(ByRef pBeTrans_pe_enc As clsBeTrans_pe_enc,
                                                    ByVal pConection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Get_Single_By_Referencia = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_enc " &
            " Where(Referencia = @Referencia AND IdTipoPedido = @IdTipoPedido)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", pBeTrans_pe_enc.Referencia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoPedido", pBeTrans_pe_enc.IdTipoPedido))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then
                Dim ObjUM As New clsBeTrans_pe_enc()
                Cargar(ObjUM, dt.Rows(0))
                Return ObjUM
            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pBeTrans_pe_enc.IdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pedido_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                          ByRef pConection As SqlConnection,
                                                          ByRef pTransaction As SqlTransaction) As List(Of clsBeTrans_pe_enc)

        Dim lReturnList As New List(Of clsBeTrans_pe_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_enc Where( IdPickingEnc = @IdPickingEnc)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

            Dim Ped As clsBeTrans_pe_enc

            Dim dt As New DataTable
            dad.Fill(dt)

            For Each dr In dt.Rows

                Ped = New clsBeTrans_pe_enc
                Cargar(Ped, dr)
                lReturnList.Add(Ped)

            Next

            Return lReturnList

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPedidoEnc),0) FROM trans_pe_enc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdCliente(ByVal pIdPedidoEnc As Integer,
                                        ByRef pConnection As SqlConnection,
                                        ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim lCliente As Integer = 0

            Const sp As String = "SELECT IdCliente FROM trans_pe_enc WHERE IdPedidoEnc = @pIdPedidoEnc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdPedidoEnc", pIdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCliente = lReturnValue
                End If

            End Using

            Return lCliente

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Picking_Asociado(ByRef IdPedidoEnc As Integer,
                                                  Optional ByRef pConnection As SqlConnection = Nothing,
                                                  Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing
        Dim Resultado As String = ""

        Try

            Dim lCommand As New SqlCommand
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim Idpic As String = ("Select IdPickingEnc 
                                    from trans_picking_det 
                                    where IdPedidoDet in 
                                        (Select IdPedidoDet 
                                        from trans_pe_det where IdPedidoEnc=@IdPedidoEnc) 
                                    AND IdPickingEnc NOT IN 
                                        (SELECT IdPickingEnc 
                                         FROM trans_picking_enc WHERE estado = 'Anulado') ")

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : ltrans = lConnection.BeginTransaction
                lCommand = New SqlCommand(Idpic, lConnection, ltrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Resultado = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return Resultado <> ""

        Catch ex As Exception
            If ltrans IsNot Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function

    Public Shared Function Tiene_Manufactura_Asociada_Sin_Finalizar(ByRef IdPedidoEnc As Integer,
                                                                    Optional ByRef pConnection As SqlConnection = Nothing,
                                                                    Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing
        Dim Resultado As Integer = 0

        Try

            Dim lCommand As New SqlCommand
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim Idpic As String = "SELECT IdManufacturaEnc 
                                   FROM trans_manufactura_enc
                                   WHERE IdPedidoEnc = @IdPedidoEnc AND estado NOT IN ('Anulado','Cerrado') "

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : ltrans = lConnection.BeginTransaction
                lCommand = New SqlCommand(Idpic, lConnection, ltrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Resultado = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return Resultado <> 0

        Catch ex As Exception
            If ltrans IsNot Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function

    Public Shared Function Tiene_Picking(ByRef IdPedidoEnc As Integer,
                                         ByRef IdPicking As Integer,
                                         Optional ByRef pConnection As SqlConnection = Nothing,
                                         Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        IdPicking = 0

        Tiene_Picking = False

        Try

            Dim lCommand As New SqlCommand

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim Idpic As String = ("Select IdPickingEnc 
                                    from trans_pe_enc 
                                    where IdPedidoEnc=@IdPedidoEnc")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(Idpic, lConnection, ltransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            IdPicking = lCommand.ExecuteScalar()

            Tiene_Picking = (IdPicking <> 0)

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Pedidos_A_Verificar_By_IdBodega(ByVal pIdBodega As Integer,
                                                                   ByVal pIdOperadorBodega As Integer,
                                                                   Optional ByRef pConection As SqlConnection = Nothing,
                                                                   Optional ByRef pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_pe_enc)

        Dim lReturnList As New List(Of clsBeTrans_pe_enc)

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : ltrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Dim sp As String = "SELECT DISTINCT pe.*, r.nombre AS NombreRutaDespacho FROM trans_pe_enc pe 
            '                    INNER JOIN trans_picking_op op on op.IdPickingEnc = pe.IdPickingEnc
            '                    LEFT JOIN road_ruta r on r.IdRuta = pe.RoadIdDespacho
            '                    WHERE pe.IdBodega = @IdBodega AND pe.estado in('Pickeado','Pendiente')"


            '#GT01122025: se agrega inner join al tipo pedido para saber si aplica verificar por imagen para no mostrar las tareas en la HH
            Dim sp As String = "SELECT DISTINCT pe.*, r.nombre AS NombreRutaDespacho FROM trans_pe_enc pe 
                                INNER JOIN trans_picking_op op on op.IdPickingEnc = pe.IdPickingEnc
                                LEFT JOIN road_ruta r on r.IdRuta = pe.RoadIdDespacho
                                INNER JOIN trans_pe_tipo pe_tipo on pe.IdTipoPedido=pe_tipo.IdTipoPedido
                                WHERE pe.IdBodega = @IdBodega AND pe.estado in('Pickeado','Pendiente') and pe_tipo.verificar_con_imagen=0"

            If pIdOperadorBodega <> 0 Then
                sp += " AND op.IdOperadorBodega = @IdOperadorBodega "
            End If

            sp += " And EXISTS 
                    (SELECT pd.*
                    FROM trans_pe_det pd INNER JOIN 
                    trans_picking_det pkd ON pd.IdPedidoDet = pkd.IdPedidoDet 
                    WHERE pkd.cantidad_recibida > 0 AND pd.IdPedidoEnc = pe.IdPedidoEnc)
                          AND pe.IdPedidoEnc IN (SELECT DISTINCT IdPedido FROM stock_res)                          
						  AND pe.IdPedidoEnc NOT IN (SELECT IdPedidoEnc FROM trans_manufactura_enc WHERE estado <> 'Cerrado')"

            Dim cmd As New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConection, lConnection)) With {.CommandType = CommandType.Text, .Transaction = IIf(Es_Transaccion_Remota, pTransaction, ltrans)}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            If pIdOperadorBodega <> 0 Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
            End If

            Dim Ped As clsBeTrans_pe_enc

            Dim dt As New DataTable
            dad.Fill(dt)

            For Each dr In dt.Rows

                Ped = New clsBeTrans_pe_enc

                Cargar(Ped, dr)

                Ped.Picking.IdPrioridadPicking = clsLnTrans_picking_enc.Get_IdPrioridadPicking(Ped.IdPickingEnc, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltrans))

                If dr("IdCliente") IsNot Nothing Then

                    Ped.Cliente.IdCliente = IIf(IsDBNull(dr("IdCliente")), "0", dr("IdCliente"))

                    If Ped.Cliente.IdCliente = 0 Then
                        Throw New Exception("#ERR20200325_1940: No se puede verificar el pedido, fue modificado o anulado en el BOF y la referencia del cliente no es válida.")
                    Else
                        clsLnCliente.Obtener(Ped.Cliente, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltrans))
                    End If

                End If

                If dr("IdPropietarioBodega") IsNot Nothing Then
                    Ped.PropietarioBodega.IdPropietarioBodega = dr("IdPropietarioBodega")
                    clsLnPropietario_bodega.Obtener(Ped.PropietarioBodega, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltrans))
                End If

                lReturnList.Add(Ped)

            Next

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return lReturnList

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then
                If ltrans IsNot Nothing Then ltrans.Rollback()
            End If
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function
    Public Shared Function Get_Pedido_A_Verificar_By_LP(ByVal pLP As String,
                                                        Optional ByRef pConection As SqlConnection = Nothing,
                                                        Optional ByRef pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_pe_enc)
        Dim lReturnList As New List(Of clsBeTrans_pe_enc)
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing

        Try
            If Not Es_Transaccion_Remota Then lConnection.Open() : ltrans = lConnection.BeginTransaction

            Dim sp As String = "SELECT *  FROM trans_pe_enc pe 
                                  WHERE IdPedidoEnc IN 
                                  (SELECT  trans_pe_det.IdPedidoEnc FROM trans_pe_det 
                                   INNER JOIN trans_picking_ubic ON trans_pe_det.IdPedidoDet=trans_picking_ubic.IdPedidoDet
                                   WHERE  (trans_picking_ubic.cantidad_recibida>0) AND (trans_picking_ubic.lic_plate = '" & pLP & " '))"

            Dim cmd As New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConection, lConnection)) With {.CommandType = CommandType.Text, .Transaction = IIf(Es_Transaccion_Remota, pTransaction, ltrans)}
            Dim dad As New SqlDataAdapter(cmd)
            Dim Ped As clsBeTrans_pe_enc
            Dim dt As New DataTable

            dad.Fill(dt)

            For Each dr In dt.Rows

                Ped = New clsBeTrans_pe_enc

                Cargar(Ped, dr)

                If dr("IdCliente") IsNot Nothing Then

                    Ped.Cliente.IdCliente = IIf(IsDBNull(dr("IdCliente")), "0", dr("IdCliente"))

                    If Ped.Cliente.IdCliente = 0 Then
                        Throw New Exception("#ERR20200325_1940: No se puede verificar el pedido, fue modificado o anulado en el BOF y la referencia del cliente no es válida.")
                    Else
                        clsLnCliente.Obtener(Ped.Cliente, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltrans))
                    End If

                End If

                If dr("IdPropietarioBodega") IsNot Nothing Then
                    Ped.PropietarioBodega.IdPropietarioBodega = dr("IdPropietarioBodega")
                    clsLnPropietario_bodega.Obtener(Ped.PropietarioBodega, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltrans))
                End If

                lReturnList.Add(Ped)

            Next

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return lReturnList

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand


        '#EJC20191205: Agregué hora_fin.

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            If oBeTrans_pe_enc.Estado = "Verificado" Then Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc AND estado NOT IN ('Anulado','Despachado')")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_pe_enc.Estado))
            If oBeTrans_pe_enc.Estado = "Verificado" Then cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    '#CKFK 20211121 Agregué esta función para actualizar el número de Picking
    Public Shared Function Actualizar_No_Picking_ERP(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("No_Picking_ERP", "@No_Picking_ERP", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_PICKING_ERP", oBeTrans_pe_enc.No_Picking_ERP))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Verificado(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        '#EJC20191205: Agregué hora_fin.

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("Enviado_A_ERP", "@Enviado_A_ERP", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc AND Estado <> 'Despachado'")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Verificado"))
            cmd.Parameters.Add(New SqlParameter("@Enviado_A_ERP", False))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado(ByVal IdPedidoEnc As Integer,
                                             ByVal Estado As String,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", Estado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Pendiente(ByVal IdPedidoEnc As Integer,
                                                       ByVal Estado As String,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc AND estado = 'Nuevo' ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", Estado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Liberar_De_Picking(ByVal IdPedidoEnc As Integer,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdUsuarioAnulo As Integer,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As Integer

        Dim rowsAffected As Integer = 0

        Liberar_De_Picking = rowsAffected

        Try

            '#CKFK20231211 HAY QUE REHACER
            Upd.Init("Trans_Pe_Enc")
            Upd.Add("IdPickingEnc", "0", DataType.Parametro)
            Upd.Add("Estado", "@Estado", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc " &
                      " AND IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@Estado", "Incompleto"))
            cmd.Parameters.Add(New SqlParameter("@user_mod", IdUsuarioAnulo))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Now))

            rowsAffected += cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(ByVal pIdPedidoEnc As String,
                                                                          ByVal pEnviado_A_ERP As Boolean,
                                                                          ByVal pUserMod As Integer) As Integer

        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " UPDATE trans_pe_enc 
                                           SET Enviado_A_ERP=@Enviado_A_ERP, user_mod = @user_mod, fec_mod = GetDate() 
                                           WHERE IdPedidoEnc=@IdPedidoEnc "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lCommand.Parameters.AddWithValue("@user_mod", pUserMod)
                        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc = lCommand.ExecuteNonQuery()

                        '#EJC20191227: Marcar los registros en la tabla intermedia.
                        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc += clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                                                                                 pEnviado_A_ERP,
                                                                                                                                                 pUserMod,
                                                                                                                                                 lConnection,
                                                                                                                                                 lTransaction)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pUsrAgr:=pUserMod, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(ByVal pIdPedidoEnc As String,
                                                                          ByVal pEnviado_A_ERP As Boolean,
                                                                          ByVal pUserMod As Integer,
                                                                          ByVal lConnection As SqlConnection,
                                                                          ByVal lTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = " UPDATE trans_pe_enc 
                                   SET Enviado_A_ERP=@Enviado_A_ERP, user_mod = @user_mod, fec_mod = GetDate()                  
                                   WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.Parameters.AddWithValue("@user_mod", pUserMod)
                Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc = lCommand.ExecuteNonQuery()

                '#EJC20191227: Marcar los registros en la tabla intermedia.
                Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc += clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                                                                         pEnviado_A_ERP,
                                                                                                                                         pUserMod,
                                                                                                                                         lConnection,
                                                                                                                                         lTransaction)

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pUsrAgr:=pUserMod, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(ByVal pReferencia As String,
                                                                         ByVal pEnviado_A_ERP As Boolean) As Integer

        Actualizar_Estado_Enviado_A_ERP_By_Referencia = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " UPDATE trans_pe_enc 
                                           SET Enviado_A_ERP=@Enviado_A_ERP                                           
                                           WHERE Referencia=@pReferencia "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommand.Parameters.AddWithValue("@pReferencia", pReferencia)
                        Actualizar_Estado_Enviado_A_ERP_By_Referencia = lCommand.ExecuteNonQuery()

                        '#EJC20191227: Marcar los registros en la tabla intermedia.
                        clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_No_Pedido(pReferencia, pEnviado_A_ERP, lConnection, lTransaction)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Actualiza el número de documento del ERP en un pedido despachado desde WMS 
    ''' Se utiliza cuando el pedido fue creado en WMS y procesado posteriormente en el ERP.
    ''' Actualiza también los registros en la tabla I_NAV_TRANSACCIONES_OUT
    ''' </summary>
    ''' <param name="pReferencia"></param>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Referencia_By_IdPedidoEnc(ByVal pReferencia As String,
                                                                ByVal pIdPedidoEnc As Integer) As Integer

        Actualizar_Referencia_By_IdPedidoEnc = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = " UPDATE trans_pe_enc 
                                            SET referencia=@Referencia 
                                            WHERE IdPedidoEnc = @IdPedidoEnc "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Referencia", pReferencia)
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        Actualizar_Referencia_By_IdPedidoEnc = lCommand.ExecuteNonQuery()
                    End Using

                    vSQL = " UPDATE i_nav_transacciones_out 
                                SET no_pedido =@Referencia 
                                WHERE IdPedidoEnc = @IdPedidoEnc "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@Referencia", pReferencia)
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        Actualizar_Referencia_By_IdPedidoEnc += lCommand.ExecuteNonQuery()
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Single_Pedido_For_Despacho(ByVal IdPedidoEnc As Integer,
                                                          ByVal IdPickingEnc As Integer,
                                                          ByVal IdBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")
        Dim vSQL As String = ""

        Try

            vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, Código, Producto, Presentacion, 
                                  UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, IdProducto, 
                                  IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                  (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, SUM(Cantidad_Despachada) AS Cantidad_Despachada 
                                  FROM VW_Pedido 
                                  WHERE IdPickingEnc = @IdPickingEnc 
                                  AND Pedido =@IdPedidoEnc
                                  AND IdBodega = @IdBodega
                                  AND EstadoPedido Not In ('Despachado','Nuevo')
                                  AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0)")

            vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, 
                               Producto, Presentacion, UnidadMedida, 
                               Estado, Fecha, IdProducto, IdPickingEnc, 
                               EstadoPedido, IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                                                                         ByVal pEliminar As Boolean,
                                                                         ByVal pIdUsuario As Integer,
                                                                         Optional pIdMotivoAnulacionBodega As Integer = 0) As Boolean

        Eliminar_Pedido_By_IdPedidoEnc_And_Referencia = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If clsLnTrans_manufactura_enc.Eliminar_Tarea_Manufactura_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                    lConnection,
                                                                                    lTransaction) Then
            End If

            For Each det In pBePedidoEnc.Detalle

                Eliminar_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                        det.IdPedidoDet,
                                        pBePedidoEnc.IdPickingEnc,
                                        lConnection,
                                        lTransaction)
            Next

            If clsLnStock_res.Eliminar_Stock_Res_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                lConnection,
                                                                lTransaction) Then
            End If

            If pEliminar Then
                Dim rowsCount As Integer = Eliminar_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                           lConnection,
                                                           lTransaction)
            Else
                Set_Estado_Anulado_Rec(pBePedidoEnc.IdPedidoEnc, pIdMotivoAnulacionBodega, lConnection, lTransaction)
            End If

            If clsLnI_nav_ped_traslado_enc.Eliminar_Pedido_Tabla_Intermedia_By_NoEnc(pBePedidoEnc.Referencia,
                                                                                     lConnection,
                                                                                     lTransaction) Then
            End If


            '#EJC20220718:Dejar log de eliminación de pedido.
            Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(pBePedidoEnc.IdBodega,
                                                                              lConnection,
                                                                              lTransaction)


            '#GT27062024: anular la póliza si tuviera una asociada.
            If pBePedidoEnc.ObjPoliza IsNot Nothing Then
                pBePedidoEnc.ObjPoliza.activo = 0
                clsLnTrans_pe_pol.Anular_poliza(pBePedidoEnc.ObjPoliza, lConnection, lTransaction)
            End If

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            '#EJC202303032014: Log mejorado
            'Dim BeLogErrorWMS As New clsBeLog_error_wms
            'BeLogErrorWMS.IdError = clsLnLog_error_wms.MaxID() + 1
            'BeLogErrorWMS.IdEmpresa = vIdEmpresa
            'BeLogErrorWMS.IdBodega = pBePedidoEnc.IdBodega
            'BeLogErrorWMS.Fecha = Now
            'BeLogErrorWMS.MensajeError = "PED_DEL: Se eliminó el IdPedido: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia
            'BeLogErrorWMS.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
            'BeLogErrorWMS.IdPickingEnc = pBePedidoEnc.IdPickingEnc
            'BeLogErrorWMS.IdUsuarioAgr = pIdUsuario
            'clsLnLog_error_wms.Insertar(BeLogErrorWMS)

            Dim msgError As String = "PED_DEL: Se eliminó el IdPedido: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia
            clsLnLog_error_wms_pe.Agregar_Error(msgError,
                                                pUsrAgr:=pIdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pConection:=lConnection,
                                                pTransaction:=lTransaction)

            lTransaction.Commit()

            Eliminar_Pedido_By_IdPedidoEnc_And_Referencia = True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Eliminar_Pedido(ByVal IdPedidoEnc As Integer,
                                           ByVal lConnection As SqlConnection,
                                           ByVal lTransaction As SqlTransaction) As Boolean

        Dim cmd As New SqlCommand()
        Dim rowsAffected As Integer = 0

        Eliminar_Pedido = False

        Try

            'Elimina el detalle
            Dim sp As String = " Delete from Trans_pe_det " &
             "  Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected = cmd.ExecuteNonQuery()

            'Elimina la historica
            sp = " Delete from stock_hist" &
                 "  Where(IdPedidoEnc = @IdPedidoEnc)"
            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            'Elimina el encabezado
            sp = " Delete from Trans_pe_enc" &
             "  Where(IdPedidoEnc = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            'Elimina el stock reservado
            sp = " Delete from stock_res" &
             "  Where(IdPedido = @IdPedidoEnc)"

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            rowsAffected += cmd.ExecuteNonQuery()

            Eliminar_Pedido = (rowsAffected > 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Eliminar_Detalle_Pedido(ByVal IdPedidoEnc As Integer,
                                                   ByVal IdPedidoDet As Integer,
                                                   ByVal IdPickingEnc As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As Integer

        Dim rowsAffected As Integer = 0

        Dim lPickingDet As New List(Of clsBeTrans_picking_det)
        Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
        Dim lBeStockPickeado As New List(Of clsBeStock)

        Dim BeStockPickeado As clsBeStock = Nothing
        Dim vIdUbicacionPicking As Integer = 0

        Eliminar_Detalle_Pedido = rowsAffected

        Dim BePickingEnc As New clsBeTrans_picking_enc

        Try

            If IdPickingEnc = 0 Then

                '#EJC20210811: Esto ocurre si anulan el picking y luego quieren liberar la línea del pedido.
                Dim vIdPickingEnc As Integer = clsLnTrans_picking_det.Get_IdPicking_Enc_By_IdPedidoDet(IdPedidoDet,
                                                                                                       IdPedidoEnc,
                                                                                                       lConnection,
                                                                                                       lTransaction)
                IdPickingEnc = vIdPickingEnc

            Else
                BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)
            End If

            '#EJC20171022_0439PM: Aquí validé que si el pedidio tiene picking asociado se elimina y restaura el stock para 
            'Poder ser reubicado posteriormente.
            If IdPickingEnc <> 0 Then

                lPickingDet = BePickingEnc.ListaPickingDet
                ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic

                vIdUbicacionPicking = clsLnTrans_picking_enc.Get_IdUbicacion_Picking(IdPickingEnc,
                                                                                     lConnection,
                                                                                     lTransaction)

                If Not lPickingDet Is Nothing Then

                    For Each PD In lPickingDet

                        If PD.Cantidad_recibida > 0 Then

                            If Not ListaDetalleUbicacion Is Nothing Then

                                For Each StockPicking In ListaDetalleUbicacion

                                    Console.WriteLine(StockPicking.IdStock)

                                    BeStockPickeado = New clsBeStock
                                    BeStockPickeado.IdStock = StockPicking.IdStock
                                    BeStockPickeado = clsLnStock.GetSingle(BeStockPickeado.IdStock,
                                                                           lConnection,
                                                                           lTransaction)
                                    BeStockPickeado.Cantidad = StockPicking.Cantidad_Recibida
                                    BeStockPickeado.Peso = StockPicking.Peso_recibido
                                    BeStockPickeado.IdPedidoEnc = 0
                                    BeStockPickeado.IdPickingEnc = 0
                                    BeStockPickeado.IdUbicacion_anterior = BeStockPickeado.IdUbicacion ' Ubicación anterior guardar histórico de donde estaba.
                                    BeStockPickeado.IdUbicacion = vIdUbicacionPicking 'Nueva ubicación
                                    lBeStockPickeado.Add(BeStockPickeado)

                                Next

                            End If

                        End If

                        rowsAffected += clsLnTrans_picking_ubic.Eliminar_By_IdPickingDet(PD.IdPickingDet,
                                                                                         lConnection,
                                                                                         lTransaction)

                        rowsAffected += clsLnTrans_picking_det.Eliminar_By_IdPickingDet(PD.IdPickingDet,
                                                                                        lConnection,
                                                                                        lTransaction)

                    Next

                End If

                rowsAffected += clsLnStock.Actualizar_Stock_Por_Productos_Pickeados(lBeStockPickeado,
                                                                                    lConnection,
                                                                                    lTransaction)

            End If

            rowsAffected += clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(IdPedidoEnc,
                                                                                                   IdPedidoDet,
                                                                                                   lConnection,
                                                                                                   lTransaction)

            rowsAffected += clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(IdPedidoEnc,
                                                                              IdPedidoDet,
                                                                              lConnection,
                                                                              lTransaction)

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Hoja_Verificacion(ByVal pIdPedidoEnc As Integer,
                                                 ByVal pIdBodega As Integer,
                                                 ByRef pConection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction) As DataTable


        Get_Hoja_Verificacion = Nothing

        Try

            Const sp As String = "SELECT IdPedido, Codigo as Codigo, producto as Nombre, Estado, Cantidad, Cantidad_Presentacion, 
                                  UMBas, Presentacion, Referencia
                                  FROM VW_Stock_Verificacion_By_IdPedidoEnc 
                                  Where(IdPedidoEnc= @IdPedidoEnc AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Get_Hoja_Verificacion = dt

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdBodega:=pIdBodega, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Single_Header(ByVal pIdPedidoEnc As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Get_Single_Header = Nothing

        Try

            Dim vSQ As String = ""

            vSQ = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))


                    Return vPedidoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20220707 Creé este método Get_Single_Header sin conexión ni transacción como parámetros
    Public Shared Function Get_Single_Header(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc

        Get_Single_Header = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = ""

            vSQ = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    Return vPedidoEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function Get_Single_By_IdPedidoEnc_And_IdBodega(ByVal IdPedidoEnc As Integer,
                                                                  ByVal IdBodega As Integer,
                                                                  ByVal pConection As SqlConnection,
                                                                  ByVal pTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Get_Single_By_IdPedidoEnc_And_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_enc" &
            " Where(IdPedidoEnc = @IdPedidoEnc AND IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then
                Dim ObjUM As New clsBeTrans_pe_enc()
                Cargar(ObjUM, dt.Rows(0))
                Return ObjUM
            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdBodega:=IdBodega, pIdPedidoEnc:=IdPedidoEnc, pStackTrace:=ex.StackTrace)

            Throw ex
        End Try

    End Function

    '#CKFK20221128 Obtener el IdPedido teniendo el IdPickingEnc
    Public Shared Function Get_IdPedido_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPedido_By_IdPickingEnc = 0

        Try

            Dim vSQL As String = "SELECT IdPedidoEnc
                                  FROM trans_pe_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdPedidoEnc") IsNot DBNull.Value AndAlso lRow("IdPedidoEnc") IsNot Nothing Then

                        Get_IdPedido_By_IdPickingEnc = CType(lRow("IdPedidoEnc"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Fecha_Inicio_Preparacion(ByVal IdPedidoEnc As Integer,
                                                               Optional ByVal pConection As SqlConnection = Nothing,
                                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("fecha_preparacion", "@fecha_preparacion", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PREPARACION", Now))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", Now))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar_Fecha_Fin_Preparacion(ByVal IdPedidoEnc As Integer,
                                                            ByVal IdDespachoEnc As Integer,
                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("no_despacho", "@no_despacho", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DESPACHO", IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Get_All_Pedidos_Sin_Poliza() As List(Of clsBeTrans_pe_enc)

        Dim lReturnList As New List(Of clsBeTrans_pe_enc)
        Dim vSQL As String = ""

        Try

            vSQL += "select * from trans_pe_enc where IdPedidoEnc not in (select IdOrdenPedidoEnc from trans_pe_pol where fec_agr BETWEEN '20230411' AND  '20230811' )
                    and fec_agr BETWEEN '20230411 00:00:00' AND  '20230811 23:59:59' and IdBodega = 2 and no_documento_externo = ''"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lDT As New DataTable

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            For Each lRow In lDT.Rows

                                Dim vPedidoEnc As New clsBeTrans_pe_enc()

                                Cargar(vPedidoEnc, lRow)

                                vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                                lReturnList.Add(vPedidoEnc)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_No_Documento_Externo(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("no_documento_externo", "@no_documento_externo", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@no_documento_externo", oBeTrans_pe_enc.No_Documento_Externo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_No_Documento_Externo(ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Integer

        Dim vSQL As String
        Dim vRegistrosActualizados As Integer = 0

        Try

            '#Actualizar trans_pe_enc con los registros encontrados manualmente 
            vSQL = "UPDATE trans_pe_enc set no_documento_externo = ISNULL((SELECT TOP(1) p.numero_orden 
                                                                           FROM polizas_con_noorden p 
                                                                           WHERE p.IdPedidoEnc = trans_pe_enc.IdPedidoEnc AND p.numero_orden <>''),'')
                    WHERE no_documento_externo = '' OR no_documento_externo = '0' OR no_documento_externo is null "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vRegistrosActualizados = CInt(lReturnValue)
                End If

            End Using

            Return vRegistrosActualizados

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pedidos_Sin_Poliza(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_enc)

        Dim lReturnList As New List(Of clsBeTrans_pe_enc)
        Dim vSQL As String = ""

        Try

            vSQL += "select * from trans_pe_enc where IdPedidoEnc not in (select IdOrdenPedidoEnc from trans_pe_pol where fecha_poliza BETWEEN '20230411' AND  '20230811' )
                    and fec_agr BETWEEN '20230411 00:00:00' AND  '20230811 23:59:59' and IdBodega = 2 and no_documento_externo = ''"


            Dim lDT As New DataTable

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    For Each lRow In lDT.Rows

                        Dim vPedidoEnc As New clsBeTrans_pe_enc()

                        Cargar(vPedidoEnc, lRow)

                        vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                          lConnection,
                                                                                          lTransaction)

                        lReturnList.Add(vPedidoEnc)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pedidos_Con_NumOrden_SinPoliza(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_enc)

        Dim lReturnList As New List(Of clsBeTrans_pe_enc)
        Dim vSQL As String = ""

        Try

            vSQL += "select * from trans_pe_enc where IdPedidoEnc not in (select IdOrdenPedidoEnc from trans_pe_pol where fecha_poliza BETWEEN '20230411' AND  '20230811' )
                    and fec_agr BETWEEN '20230411 00:00:00' AND  '20230811 23:59:59' and IdBodega = 2 and no_documento_externo <> '' "


            Dim lDT As New DataTable

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    For Each lRow In lDT.Rows

                        Dim vPedidoEnc As New clsBeTrans_pe_enc()

                        Cargar(vPedidoEnc, lRow)

                        vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                          lConnection,
                                                                                          lTransaction)

                        lReturnList.Add(vPedidoEnc)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Pedido_Sin_Poliza_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Dim vReturn As clsBeTrans_pe_enc = Nothing
        Dim vSQL As String = ""

        Try

            vSQL += "select * from trans_pe_enc where IdPedidoEnc = @IdPedidoEnc"

            Dim lDT As New DataTable

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                lDataAdapter.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    Cargar(vPedidoEnc, lDT.Rows(0))

                    vReturn = vPedidoEnc

                End If

            End Using

            Return vReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Single_By_NoDocumentoExterno(ByVal pNoDocumentoExterno As String,
                                                            ByVal pConection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Get_Single_By_NoDocumentoExterno = Nothing

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc 
                                   WHERE (no_documento_externo = @no_documento_externo)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@no_documento_externo", pNoDocumentoExterno))

            Dim dt As New DataTable()
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then
                Dim BeTransPeEnc As New clsBeTrans_pe_enc()
                Cargar(BeTransPeEnc, dt.Rows(0))
                Return BeTransPeEnc
            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_NoDocumentoExterno(ByVal pNoDocumentoExterno As String,
                                                         ByVal pConection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_pe_enc)

        Get_All_By_NoDocumentoExterno = Nothing

        Dim lReturn As New List(Of clsBeTrans_pe_enc)

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc 
                                   WHERE (no_documento_externo = @no_documento_externo)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@no_documento_externo", pNoDocumentoExterno))

            Dim dt As New DataTable()
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                For Each pedido In dt.Rows
                    Dim BeTransPeEnc As New clsBeTrans_pe_enc()
                    Cargar(BeTransPeEnc, pedido)
                    lReturn.Add(BeTransPeEnc)
                Next

            End If

            Return lReturn

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_Without_Picking(ByVal pIdPedidoEnc As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction,
                                                   Optional ByVal IdDespachoEnc As Integer = 0) As clsBeTrans_pe_enc

        Get_Single_Without_Picking = Nothing

        Try

            Dim vSQ As String = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc() With {.IdPedidoEnc = lRow("IdPedidoEnc"),
                        .IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer)),
                        .IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))}

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))

                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))

                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))
                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))
                    '#GT26032024: campo para identificar si aplica manufactura ligera
                    vPedidoEnc.IdTipoManufactura = IIf(IsDBNull(lRow("idtipomanufactura")), 0, lRow("idtipomanufactura"))

                    '#EJC20190214_0108PM: Enviar estado para cargar detalle de picking en transacción..
                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc,
                                                                                      vPedidoEnc.Estado,
                                                                                      IdDespachoEnc,
                                                                                      lConnection,
                                                                                      lTransaction)

                    Application.DoEvents()

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    '#EJC20171021_0526PM: Obtener el picking y el detalle.
                    If vPedidoEnc.IdPickingEnc <> 0 Then

                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc

                        If vPedidoEnc.IdPickingEnc <> 0 Then
                            vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc,
                                                                                  lConnection,
                                                                                  lTransaction)
                        End If

                    End If

                    Return vPedidoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxNoDocumento(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(no_documento),0)  as MaxNoDocumento FROM trans_pe_enc "

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function MaxNoDocumento() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(no_documento),0)  as MaxNoDocumento FROM trans_pe_enc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pedido_By_IdBodega_DT(ByVal IdBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = " SELECT * FROM VW_PEDIDOS_LIST WHERE IDBODEGA = @IDBODEGA "
            vSQL += " AND Activo=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20231008 Creé esta función para obtener un datatable del pedido
    Public Shared Function Get_Pedido_By_IdPedido_DT(ByVal IdPedidoEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = " SELECT * FROM VW_PEDIDOS_LIST WHERE Correlativo = @IdPedidoEnc "
            vSQL += " AND Activo=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20231008 Obtener el IdPedido teniendo la referencia
    Public Shared Function Get_IdPedido_By_Referencia(ByVal pReferencia As String) As Integer

        Get_IdPedido_By_Referencia = 0

        Try

            Dim vSQL As String = "SELECT IdPedidoEnc
                                  FROM trans_pe_enc 
                                  WHERE referencia=@referencia "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@referencia", pReferencia)

                        Dim lDT As New DataTable()

                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("IdPedidoEnc") IsNot DBNull.Value AndAlso lRow("IdPedidoEnc") IsNot Nothing Then

                                Get_IdPedido_By_Referencia = CType(lRow("IdPedidoEnc"), Integer)

                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Tiene_Detalle(ByVal IdPedidoEnc As Integer,
                                         ByVal lConnection As SqlConnection,
                                         ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim vSQL As String = "SELECT TOP(1) IdPedidoDet FROM trans_pe_det WHERE IdPedidoEnc = @IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                Tiene_Detalle = lDT.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Single_By_Referencia(ByVal Referencia As String) As clsBeTrans_pe_enc

        Get_Single_By_Referencia = Nothing

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc " &
                                 " Where(Referencia = @Referencia)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count >= 1 Then
                        Dim BePedidoEnc As New clsBeTrans_pe_enc()
                        Cargar(BePedidoEnc, dt.Rows(0))
                        Get_Single_By_Referencia = BePedidoEnc
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    '#GT23022024: cargar pedidos con estado pickeado sin bodega
    Public Shared Function Get_All_Activos_By_Manufactura(ByVal pFechaDel As Date,
                                           ByVal pFechaAl As Date,
                                           ByVal ConPicking As Boolean,
                                           ByVal NoIncluir As String,
                                           ByVal IdBodega As Integer,
                                           ByVal EstadoDespachado As Boolean,
                                           ByVal IdPropietarioBodega As Integer,
                                           ByRef pIdUsuario As Integer,
                                           ByVal Es_Manufactura As Boolean) As DataTable

        Dim lTable As New DataTable("Result")
        Dim vSQL As String = ""

        Try

            Dim BeBodega As New clsBeBodega()
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(IdBodega)

            If ConPicking Then

                '#EJC20220328: Config CEALSA, mostrar LP en listado.
                If BeBodega.Mostrar_Area_En_HH Then

                    vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, CodigoCliente, NombreCliente,
                                             Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, 
                                             SUM(Cantidad_Despachada) AS Cantidad_Despachada, pr_clas.nombre as Clasificacion,
                                             Licencia, lote as Lote, fecha_vence as Vence
                                             FROM VW_Pedido vw
											 inner join producto pr on vw.Código = pr.codigo
											 left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo
                                             WHERE IdPickingEnc > 0 {0} {1}",
                                             NoIncluir, " AND EstadoPedido Not In ('Despachado','Nuevo')
                                             AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0)")

                Else

                    vSQL = String.Format("SELECT Pedido, referencia, IdPedidoDet, 
                                             CodigoCliente, NombreCliente,Código, Producto, Presentacion, 
                                             UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                                             IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                                             (SUM(Cantidad_Verificada) - SUM(Cantidad_Despachada)) AS Cantidad_Verificada, 
                                             SUM(Cantidad_Despachada) AS Cantidad_Despachada, pr_clas.nombre as Clasificacion
                                             FROM VW_Pedido vw
											 inner join producto pr on vw.Código = pr.codigo
											 left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo
                                             WHERE IdPickingEnc > 0 {0} {1}",
                                             NoIncluir, " AND EstadoPedido Not In ('Despachado','Nuevo')
                                             AND (Cantidad_Despachada < Cantidad_Verificada AND Cantidad_Verificada > 0)")

                End If

            Else
                '#CKFK20221013 Agregué el campo NombreCliente
                vSQL = " SELECT Pedido, referencia,mfg2.nombre as manufactura_pedido, IdPedidoDet, Código, Producto, Presentacion, 
                         UnidadMedida, Estado, SUM(Cantidad) AS Cantidad , Fecha, vw.IdProducto, 
                         IdPickingEnc, EstadoPedido, IdBodega, SUM(Cantidad_Pickeada) AS Cantidad_Pickeada, 
                         SUM(Cantidad_Verificada) AS Cantidad_Verificada, SUM(Cantidad_Despachada) AS Cantidad_Despachada, 
                         pr_clas.nombre as Clasificacion, NombreCliente,
                         mfg.nombre AS manufactura_producto
                         FROM VW_Pedido vw
			             inner join producto pr on vw.Código = pr.codigo
			             left outer join producto_clasificacion pr_clas on pr.codigo = pr_clas.codigo 
                         inner join trans_manufactura_tipo mfg on pr.idtipomanufactura=mfg.idtipomanufactura
	                     inner join trans_manufactura_tipo mfg2 on vw.idtipomanufactura=mfg2.idtipomanufactura "

            End If

            vSQL += "WHERE 1 > 0"

            vSQL += String.Format(" And cast(Fecha As Date) BETWEEN {0} And {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            If IdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            If Es_Manufactura Then
                vSQL += " And vw.EstadoPedido='Pickeado' and mfg.IdTipoManufactura<>0  "
            End If

            If pIdUsuario <> 0 Then
                vSQL += " And vw.user_agr=@IdUsuarioAgrego"
            End If

            If BeBodega.Mostrar_Area_En_HH Then
                vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, 
                               Producto, Presentacion, UnidadMedida, 
                               Estado, Fecha, vw.IdProducto, IdPickingEnc, 
                               EstadoPedido, IdBodega, pr_clas.nombre, licencia, lote, fecha_vence, CodigoCliente, NombreCliente,
                               pr.idtipomanufactura,mfg.nombre,mfg2.nombre"
            Else
                vSQL += " Group By Pedido, referencia, IdPedidoDet, Código, 
                               Producto, Presentacion, UnidadMedida, 
                               Estado, Fecha, vw.IdProducto, IdPickingEnc, 
                               EstadoPedido, IdBodega, pr_clas.nombre, CodigoCliente, NombreCliente,
                               pr.idtipomanufactura,mfg.nombre,mfg2.nombre"
            End If


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        If pIdUsuario <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUsuarioAgrego", pIdUsuario)
                        End If

                        If IdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        End If

                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20240522: Obtiene el número de contenedor en base al IdRecepcionEnc del primer IdStockRes
    ''' Realizado para cuando se transfiera producto de la bodega origen de WMS, para la bodega destino también de WMS
    ''' Y se mapee el No. de contenedor origen en la bodega destino para no perder la trazabilidad.
    ''' </summary>
    ''' <param name="BePedidoEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_No_Contenedor_Ingreso(BePedidoEnc As clsBeTrans_pe_enc, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_No_Contenedor_Ingreso = ""

        Try

            Dim vIdRecepcion As Integer = 0
            Dim registrosFiltrados = BePedidoEnc.Detalle.
                                    Where(Function(d) d.ListaStockRes IsNot Nothing AndAlso d.ListaStockRes.Any()).
                                    ToList()

            '#CKFK20260324 Agregue esta validación para que no de error de object not reference
            If registrosFiltrados IsNot Nothing AndAlso registrosFiltrados.Count > 0 Then

                vIdRecepcion = registrosFiltrados.FirstOrDefault.ListaStockRes.FirstOrDefault.IdRecepcion

                Get_No_Contenedor_Ingreso = clsLnTrans_re_enc.Get_No_Contenedor_By_IdRecepcionEnc(vIdRecepcion, lConnection, lTransaction)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#CKFK20240528 Obtener el IdTipoPedido 
    Public Shared Function Get_IdTipoPedido_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As Integer

        Get_IdTipoPedido_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = "SELECT IdTipoPedido
                                  FROM trans_pe_enc 
                                  WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdTipoPedido") IsNot DBNull.Value AndAlso lRow("IdTipoPedido") IsNot Nothing Then

                        Get_IdTipoPedido_By_IdPedidoEnc = CType(lRow("IdTipoPedido"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Bodega_Destino_By_IdPedido(ByVal pIdPedidoEnc As Integer) As Integer

        Get_Bodega_Destino_By_IdPedido = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT bodega_destino FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_Bodega_Destino_By_IdPedido = IIf(IsDBNull(lRow("bodega_destino")), 0, lRow("bodega_destino"))

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Bodega_Destino_By_IdPedido(ByVal pIdPedidoEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_Bodega_Destino_By_IdPedido = 0

        Try

            Dim vSQ As String = "SELECT bodega_destino FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_Bodega_Destino_By_IdPedido = IIf(IsDBNull(lRow("bodega_destino")), 0, lRow("bodega_destino"))

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_IdMuelle_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                              ByVal IdMuelle As Integer,
                                                              Optional ByVal pConection As SqlConnection = Nothing,
                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("IdMuelle", "@IdMuelle", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", IdMuelle))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Shared Function get_cantidad_pedidos_activos(ByVal idBodega As Integer,
                                                 ByVal FechaDesde As Date,
                                                 ByVal FechaHasta As Date) As Integer

        get_cantidad_pedidos_activos = 0

        Try

            Dim vSQL As String = "SELECT COUNT(distinct Pedido) Cant_Pedido 
                                 FROM VW_Pedido 
                                 WHERE fecha between @FechaDesde and @FechaHasta and EstadoPedido <> 'Anulado'
                                 AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", FechaDesde)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", FechaHasta)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            get_cantidad_pedidos_activos = IIf(IsDBNull(lRow("Cant_Pedido")), 0, lRow("Cant_Pedido"))

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT14082024: guardar el acuerdo comercial al asociar un servicio, en caso no se haya hecho al crear el pedido
    Public Shared Function Actualizar_AcuerdoComercial_By_IdPedidoEnc(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("idacuerdocomercial", "@idacuerdocomercial", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_enc.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDACUERDOCOMERCIAL", oBeTrans_pe_enc.IdAcuerdoComercial))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_pe_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_pe_enc.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As Boolean

        Get_Estado_Enviado_A_ERP_By_IdPedidoEnc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT Enviado_A_ERP FROM Trans_pe_enc 
             Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("Enviado_A_ERP")), False, dt.Rows(0).Item("Enviado_A_ERP"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Pedido_Tienen_Verificacion_Parcial(ByVal BePedidoEnc As clsBeTrans_pe_enc, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        '#CKFK20220624 Inicialicé la función en False
        Pedido_Tienen_Verificacion_Parcial = False
        Dim vListaPedidosVerificados As New List(Of Integer)

        Try

            If Not BePedidoEnc Is Nothing Then

                Dim BeTipoDocumento As New clsBeTrans_pe_tipo()

                BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedidoEnc.IdTipoPedido,
                                                                                lConnection,
                                                                                lTransaction)

                If Not BeTipoDocumento.Permitir_Despacho_Parcial Then

                    For Each Det In BePedidoEnc.Detalle

                        If Not Det.ListaPickingUbic Is Nothing Then

                            If Not Det.ListaPickingUbic Is Nothing Then

                                For Each Pu In Det.ListaPickingUbic

                                    If (Pu.Cantidad_Solicitada - Pu.Cantidad_Verificada) <> 0 Then

                                        '#CKFK20220705 No es necesario inicializar esto en true
                                        'Pedidos_Tienen_Verificacion_Parcial = True

                                        Dim vMensaje As String = "La configuración del tipo de documento: " &
                                                                    BeTipoDocumento.Descripcion & " para el IdPedido: " &
                                                                    BePedidoEnc.IdPedidoEnc & " Ref.: " & BePedidoEnc.Referencia &
                                                                    " No permite despachos parciales. " & "Producto: " & Det.Codigo_Producto &
                                                                    " Solicitado: " & Det.Cantidad & " Verificado: " & Pu.Cantidad_Verificada

                                        If Not vListaPedidosVerificados.Contains(BePedidoEnc.IdPedidoEnc) Then
                                            Pedido_Tienen_Verificacion_Parcial = True
                                            Throw New Exception(vMensaje)
                                            Exit For
                                        End If

                                    End If

                                Next

                            Else
                                'El pedido no tiene picking_ubic?, no se ha asociado picking al pedido?
                            End If

                        End If

                    Next

                Else
                    '#EJC20220618_1125AM.
                    Pedido_Tienen_Verificacion_Parcial = False
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Pedidos_Permiten_Despacho_Multiple(ByVal BePedido As clsBeTrans_pe_enc,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As Boolean

        Pedidos_Permiten_Despacho_Multiple = False
        Dim ListDespacho As New List(Of clsBeTrans_despacho_det)
        Dim vListaBePedidoidosVerificados As New List(Of Integer)
        Dim BeTipoDocumento As New clsBeTrans_pe_tipo

        Try

            BeTipoDocumento = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedido.IdTipoPedido)

            If Not BeTipoDocumento.Permitir_Despacho_Multiple Then

                If Not vListaBePedidoidosVerificados.Contains(BePedido.IdPedidoEnc) Then

                    ListDespacho = clsLnTrans_despacho_det.Get_All_By_IdPedidoEnc(BePedido.IdPedidoEnc,
                                                                                  lConnection,
                                                                                  lTransaction)

                    If Not ListDespacho Is Nothing Then

                        Dim vMensaje As String = "La configuración del tipo de documento: " &
                                                          BeTipoDocumento.Descripcion & " para el IdBePedidoido: " &
                                                          BePedido.IdPedidoEnc & " Ref.: " & BePedido.Referencia &
                                                          " No permite despachos múltiples y el BePedidoido ya reporta un despacho. "

                        Pedidos_Permiten_Despacho_Multiple = False
                        Throw New Exception(vMensaje)
                    Else
                        Pedidos_Permiten_Despacho_Multiple = True
                    End If

                End If

            Else
                '#EJC20220618_1125AM.
                Pedidos_Permiten_Despacho_Multiple = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(ByVal Referencia As String,
                                                                          ByVal pIdTipoDocumento As Integer) As Boolean

        Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT Enviado_A_ERP FROM Trans_pe_enc 
             Where(Referencia = @Referencia) AND (IdTipoPedido = @IdTipoPedido)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoPedido", pIdTipoDocumento))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("Enviado_A_ERP")), False, dt.Rows(0).Item("Enviado_A_ERP"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc_Single(ByVal pIdPedidoEnc As String,
                                                                                 ByVal pEnviado_A_ERP As Boolean,
                                                                                 ByVal pUserMod As Integer,
                                                                                 ByVal lConnection As SqlConnection,
                                                                                 ByVal lTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc_Single = 0

        Try

            Dim vSQL As String = " UPDATE trans_pe_enc 
                                   SET Enviado_A_ERP=@Enviado_A_ERP, user_mod = @user_mod, fec_mod = GetDate()                  
                                   WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.Parameters.AddWithValue("@user_mod", pUserMod)
                Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc_Single = lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Packing(ByRef IdPedidoEnc As Integer,
                                         Optional ByRef pConnection As SqlConnection = Nothing,
                                         Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Dim pIdPedidoEnc As Integer = 0

        Tiene_Packing = False

        Try

            Dim lCommand As New SqlCommand

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim Idpic As String = ("Select IdPedidoEnc 
                                    from trans_packing_enc 
                                    where IdPedidoEnc=@IdPedidoEnc")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(Idpic, lConnection, ltransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            pIdPedidoEnc = lCommand.ExecuteScalar()

            Tiene_Packing = (pIdPedidoEnc <> 0)

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    Public Shared Function Packing_Finalizado(ByRef IdPedidoEnc As Integer,
                                              Optional ByRef pConnection As SqlConnection = Nothing,
                                              Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Dim pIdPedidoEnc As Integer = 0

        Packing_Finalizado = False

        Try

            Dim lCommand As New SqlCommand

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim Idpic As String = ("Select finalizado 
                                    from trans_packing_enc 
                                    where IdPedidoEnc=@IdPedidoEnc and iddespachoenc = 0 ")

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(Idpic, lConnection, ltransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Packing_Finalizado = lReturnValue
            End If

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    '#CKFK 20240928 Agregué la función Get_Estado_Pedido_By_IdPedidoEnc para poder saber el estado del pedido 
    Public Shared Function Get_Estado_Pedido_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                            Optional ByRef pConnection As SqlConnection = Nothing,
                                                            Optional ByRef pTransaction As SqlTransaction = Nothing) As String

        Get_Estado_Pedido_By_IdPedidoEnc = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT estado FROM Trans_pe_enc 
                                  Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand
            Dim dad As New SqlDataAdapter()

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConnection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            dad = New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("estado")), "", dt.Rows(0).Item("estado"))
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_No_Documento(ByVal No_Documento As String) As clsBeTrans_pe_enc

        Get_Single_By_No_Documento = Nothing

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc " &
                                 " Where(no_documento = @No_Documento)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Documento", No_Documento))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count >= 1 Then
                        Dim BePedidoEnc As New clsBeTrans_pe_enc()
                        Cargar(BePedidoEnc, dt.Rows(0))
                        Get_Single_By_No_Documento = BePedidoEnc
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Sub GetBodegas_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                ByRef BodegaOrigen As String,
                                                ByRef BodegaDestino As String)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT b.Codigo BodegaOrigen, c.codigo BodegaDestino
                                  FROM trans_pe_enc e INNER JOIN 
                                       cliente c ON e.IdCliente = c.IdCliente INNER JOIN
	                                   bodega b ON b.IdBodega = e.IdBodega
                                  WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    BodegaOrigen = lDT.Rows(0).Item("BodegaOrigen")
                    BodegaDestino = lDT.Rows(0).Item("BodegaDestino")
                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_Company_SAP_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Get_Company_SAP_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = "SELECT Codigo_Empresa_ERP 
                                  FROM trans_pe_enc 
                                  WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("Codigo_Empresa_ERP") IsNot DBNull.Value AndAlso lRow("Codigo_Empresa_ERP") IsNot Nothing Then

                        Get_Company_SAP_By_IdPedidoEnc = CType(lRow("Codigo_Empresa_ERP"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Company_SAP_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As String

        Get_Company_SAP_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = "SELECT Codigo_Empresa_ERP 
                                  FROM trans_pe_enc 
                                  WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDT As New DataTable()

                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("Codigo_Empresa_ERP") IsNot DBNull.Value AndAlso lRow("Codigo_Empresa_ERP") IsNot Nothing Then

                                Get_Company_SAP_By_IdPedidoEnc = CType(lRow("Codigo_Empresa_ERP"), String)

                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Empaque_Tarima_By_IdPickingEnc(ByRef IdPickingEnc As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lCommand As New SqlCommand
            Dim vSQL As String = ("Select top(1) empaque_tarima 
                                   From trans_pe_enc p INNER JOIN trans_pe_tipo t ON p.IdTipoPedido = t.IdTipoPedido
                                   Where IdPickingEnc =@IdPickingEnc")

            lCommand.CommandType = CommandType.Text
            lCommand = New SqlCommand(vSQL, lConnection, ltransaction)

            lCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPickingEnc))

            Return lCommand.ExecuteScalar()

            ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    Public Shared Function Tiene_Empaque_Tarima_By_IdPedidoEnc(ByRef IdPedidoEnc As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lCommand As New SqlCommand
            Dim vSQL As String = ("Select empaque_tarima 
                                   From trans_pe_enc p INNER JOIN trans_pe_tipo t ON p.IdTipoPedido = t.IdTipoPedido
                                   Where IdPedidoEnc =@IdPedidoEnc")

            lCommand.CommandType = CommandType.Text
            lCommand = New SqlCommand(vSQL, lConnection, ltransaction)

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Return lCommand.ExecuteScalar()

            ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_Referencia_Documento(ByVal No_Documento As String) As clsBeTrans_pe_enc

        Get_Single_By_Referencia_Documento = Nothing

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc " &
                                 " Where (referencia = @No_Documento)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Documento", No_Documento))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count >= 1 Then
                        Dim BePedidoEnc As New clsBeTrans_pe_enc()
                        Cargar(BePedidoEnc, dt.Rows(0))
                        Get_Single_By_Referencia_Documento = BePedidoEnc
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' #CKFK20250225: Utilizado en la HH, se cambió para filtrar solo por el pedido y el producto
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_IdPedidoEnc_And_Codigo_Producto(ByVal pIdPedidoEnc As Integer,
                                                                         ByVal pCodigo_Producto As String) As clsBeTrans_pe_enc

        Get_Single_By_IdPedidoEnc_And_Codigo_Producto = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT * FROM VW_Get_Single_Pedido WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Cliente")), "", lRow("Nombre_Cliente"))

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, CType(lRow("IdPropietario"), Integer))
                    vPedidoEnc.PropietarioBodega.Propietario.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Propietario")), "", lRow("Nombre_Propietario"))

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        vPedidoEnc.TipoPedido.Nombre = IIf(IsDBNull(lRow("Nombre_Tipo_Pedido")), "", ("Nombre_Tipo_Pedido"))
                        vPedidoEnc.TipoPedido.Permitir_Despacho_Parcial = IIf(IsDBNull(lRow("Permitir_Despacho_Parcial")), False, lRow("Permitir_Despacho_Parcial"))
                        vPedidoEnc.TipoPedido.Permitir_Despacho_Multiple = IIf(IsDBNull(lRow("Permitir_Despacho_Multiple")), False, lRow("Permitir_Despacho_Multiple"))
                        vPedidoEnc.TipoPedido.Empaque_Tarima = IIf(IsDBNull(lRow("Empaque_Tarima")), False, lRow("Empaque_Tarima"))
                        vPedidoEnc.TipoPedido.Asignar_Todos_Operadores = IIf(IsDBNull(lRow("Asignar_Todos_Operadores")), False, lRow("Asignar_Todos_Operadores"))
                        vPedidoEnc.TipoPedido.Verificar = IIf(IsDBNull(lRow("Verificar")), False, lRow("Verificar"))
                        vPedidoEnc.TipoPedido.Generar_Picking_Auto = IIf(IsDBNull(lRow("Generar_Picking_Auto")), False, lRow("Generar_Picking_Auto"))
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))
                    vPedidoEnc.Bodega_Destino = IIf(IsDBNull(lRow("Bodega_Destino")), "", lRow("Bodega_Destino"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then

                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingleHH_By_IdPicking_IdPedido_CodProd(vPedidoEnc.IdPickingEnc,
                                                                                                              pIdPedidoEnc,
                                                                                                              pCodigo_Producto,
                                                                                                              lConnection,
                                                                                                              lTransaction)
                    End If

                    Return vPedidoEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK20250227 Creé esta función para devolver los pedidos sin picking
    Public Shared Function Get_Single_Sin_Picking(ByVal pIdPedidoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction,
                                                  Optional ByVal IdDespachoEnc As Integer = 0) As clsBeTrans_pe_enc

        Get_Single_Sin_Picking = Nothing

        Try

            Dim vSQ As String = "SELECT * FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Dim vPedidoEnc As New clsBeTrans_pe_enc() With {.IdPedidoEnc = lRow("IdPedidoEnc"),
                        .IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer)),
                        .IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))}

                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, CType(lRow("IdCliente"), Integer))
                    clsLnCliente.Obtener(vPedidoEnc.Cliente, lConnection, lTransaction)

                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))

                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))

                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, CType(lRow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction)

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lRow("IdTipoPedido"), Integer)
                    End If

                    'If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                    '    clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    'End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))
                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))
                    '#GT26032024: campo para identificar si aplica manufactura ligera
                    vPedidoEnc.IdTipoManufactura = IIf(IsDBNull(lRow("idtipomanufactura")), 0, lRow("idtipomanufactura"))

                    vPedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = IIf(IsDBNull(lRow("Referencia_Documento_Ingreso_Bodega_Destino")), "", lRow("Referencia_Documento_Ingreso_Bodega_Destino"))


                    '#CKFK20250227: Obtener detalle sin el picking
                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc_Sin_Picking(vPedidoEnc.IdPedidoEnc,
                                                                                                  vPedidoEnc.Estado,
                                                                                                  IdDespachoEnc,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                    Application.DoEvents()

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    Return vPedidoEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_StockRes_By_IdPedido(ByVal pIdPedidoEnc As Integer) As Integer

        Get_StockRes_By_IdPedido = 0

        Try

            Dim vCantStock As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(count(IdStockRes),0) cant FROM stock_res WHERE IdPedido=@IdPedidoEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            vCantStock = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return vCantStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Pendiente(ByVal IdPedidoEnc As Integer,
                                                       ByVal Usuario As Integer,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", Usuario))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Pendiente"))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_TipoPedido_By_IdPickingEnc(ByVal pIdPickingEnc As Integer) As clsBeTrans_pe_tipo


        Get_TipoPedido_By_IdPickingEnc = Nothing

        Try

            Dim vSQL As String = "select pe_tipo.IdTipoPedido from trans_picking_enc pick_enc 
                                       inner join trans_pe_enc pe_enc on pick_enc.IdPickingEnc=pe_enc.IdPickingEnc
                                       inner join trans_pe_tipo pe_tipo on pe_enc.IdTipoPedido=pe_tipo.IdTipoPedido
                                where pick_enc.IdPickingEnc=@pIdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {
                        .CommandType = CommandType.Text
                    }

                    cmd.Parameters.Add(New SqlParameter("@pIdPickingEnc", pIdPickingEnc))

                    Dim result As Object = cmd.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then

                        Dim idPedido As Integer = Convert.ToInt32(result)

                        If clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(idPedido) IsNot Nothing Then
                            '#GT15042025: me aseguro que no sea nothing, para devolver un objeto o devolver nada
                            Get_TipoPedido_By_IdPickingEnc = New clsBeTrans_pe_tipo()
                            Get_TipoPedido_By_IdPickingEnc = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(idPedido)
                        End If
                    End If

                    lTransaction.Commit()
                End Using

                lConnection.Close()
            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPickingEnc, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Mover_Producto_A_Muelle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As Boolean

        Mover_Producto_A_Muelle_By_IdPedidoEnc = False

        Try

            Dim vSQL As String = "SELECT b.mover_producto_zona_muelle
                                  FROM trans_pe_enc a
                                  INNER JOIN trans_pe_tipo b
                                  ON a.IdTipoPedido = b.IdTipoPedido
                                  WHERE a.IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("mover_producto_zona_muelle") IsNot DBNull.Value AndAlso lRow("mover_producto_zona_muelle") IsNot Nothing Then

                        Mover_Producto_A_Muelle_By_IdPedidoEnc = CType(lRow("mover_producto_zona_muelle"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Empresa_By_IdPedidoEnc(IdPedidoEnc As Integer) As String

        Get_Empresa_By_IdPedidoEnc = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT Codigo_Empresa_ERP FROM Trans_pe_enc 
             Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Empresa_By_IdPedidoEnc = IIf(IsDBNull(dt.Rows(0).Item("Codigo_Empresa_ERP")), "", dt.Rows(0).Item("Codigo_Empresa_ERP"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#GT20052025: obtener los pedidos cerrados, activos, no anulados y según lista propietario_bodega para enviar a la nube
    Public Shared Function GetAll_By_CDC(ByVal pUltimaFechaSincro As Date, ByVal pListaPropietariosBodega As List(Of clsBePropietario_bodega),
                                                                           ByVal listaPedidosPendientes As List(Of Integer),
                                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetAll_By_CDC = New List(Of clsBeTrans_pe_enc)
        Dim Es_Transaccion_Remota As Boolean
        Dim vSQL As String = ""
        Try

            vSQL = "SELECT * FROM trans_pe_enc WHERE (activo=1 and anulado=0 and estado='Despachado') and hora_ini >=@PULTIMAFECHASINCRO "

            If pListaPropietariosBodega IsNot Nothing AndAlso pListaPropietariosBodega.Count > 0 Then
                Dim propietarioIds As String = String.Join(",", pListaPropietariosBodega.Select(Function(p) p.IdPropietarioBodega.ToString()))
                vSQL &= " AND idPropietarioBodega IN (" & propietarioIds & ")"
            End If

            If listaPedidosPendientes IsNot Nothing AndAlso listaPedidosPendientes.Count > 0 Then
                Dim IdPedidoIds As String = String.Join(",", listaPedidosPendientes)
                vSQL &= " And IdPedidoEnc Not In (" & IdPedidoIds & ")"
            End If

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
            End If

            Dim pConexion = If(Es_Transaccion_Remota, pConection, lConnection)
            Dim pTransaccion = If(Es_Transaccion_Remota, pTransaction, lTransaction)

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@PULTIMAFECHASINCRO", pUltimaFechaSincro)


            'If pListaPropietariosBodega > 0 Then
            '    lDTA.SelectCommand.Parameters.AddWithValue("@pPropietariosBodega", pListaPropietariosBodega)
            'End If

            Dim lDT As New DataTable()

            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                '#GT20052025: iterar cada row que equivale a un pedido
                For Each lrow As DataRow In lDT.Rows

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    Cargar(vPedidoEnc, lrow)

                    With vPedidoEnc
                        .IdPedidoEnc = lrow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lrow("estado")), "", lrow("estado"))
                        .Ubicacion = IIf(IsDBNull(lrow("Ubicacion")), "", lrow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lrow("IdBodega")), 0, CType(lrow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lrow("IdMuelle")), 0, lrow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))

                    clsLnCliente.Obtener(vPedidoEnc.Cliente, pConexion, pTransaccion)
                    vPedidoEnc.PropietarioBodega =
                        clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                            pConexion,
                                                                            pTransaccion)

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, pConexion, pTransaccion)
                    End If


                    If Not IsDBNull(lrow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lrow("IdTipoPedido"), Integer)
                    End If

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, pConexion, pTransaccion)
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lrow("IdPickingEnc")), 0, lrow("IdPickingEnc"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lrow("IdMotivoDevolucion")), 0, lrow("IdMotivoDevolucion"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, pConexion, pTransaccion)
                    End If

                    For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle

                        PeDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(PeDet.IdPedidoDet,
                                                                                            PeDet.IdPedidoEnc,
                                                                                            pConexion,
                                                                                            pTransaccion)

                        PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = PeDet.IdPedidoDet)

                    Next

                    GetAll_By_CDC.Add(vPedidoEnc)

                Next
            Else
                GetAll_By_CDC = Nothing
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Function

    '#GT01072025: metodo para obtener pedido con transacción.
    Public Shared Function Get_Single_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer, ByRef pConexion As SqlConnection, ByRef pTransaccion As SqlTransaction) As clsBeTrans_pe_enc
        Get_Single_By_IdPedidoEnc = New clsBeTrans_pe_enc
        Dim lDTA As New SqlDataAdapter
        Try

            Dim vSQ As String = "SELECT * FROM trans_pe_enc WHERE (activo=1 and anulado=0 and estado='Despachado' and IdPedidoEnc = @pIePedidoEnc)  "

            'pConexion.Open() : pTransaccion = pConexion.BeginTransaction(IsolationLevel.ReadUncommitted)
            lDTA = New SqlDataAdapter(vSQ, pConexion)

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Transaction = pTransaccion
            lDTA.SelectCommand.Parameters.AddWithValue("@pIePedidoEnc", IdPedidoEnc)

            Dim lDT As New DataTable()

            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                '#GT20052025: iterar cada row que equivale a un pedido
                For Each lrow As DataRow In lDT.Rows

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()
                    Cargar(vPedidoEnc, lrow)

                    With vPedidoEnc
                        .IdPedidoEnc = lrow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lrow("estado")), "", lrow("estado"))
                        .Ubicacion = IIf(IsDBNull(lrow("Ubicacion")), "", lrow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lrow("IdBodega")), 0, CType(lrow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lrow("IdMuelle")), 0, lrow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))

                    clsLnCliente.Obtener(vPedidoEnc.Cliente, pConexion, pTransaccion)
                    vPedidoEnc.PropietarioBodega =
                        clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                            pConexion,
                                                                            pTransaccion)

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, pConexion, pTransaccion)
                    End If


                    If Not IsDBNull(lrow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lrow("IdTipoPedido"), Integer)
                    End If

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, pConexion, pTransaccion)
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lrow("IdPickingEnc")), 0, lrow("IdPickingEnc"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lrow("IdMotivoDevolucion")), 0, lrow("IdMotivoDevolucion"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, pConexion, pTransaccion)
                    End If

                    For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle

                        PeDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(PeDet.IdPedidoDet,
                                                                                            PeDet.IdPedidoEnc,
                                                                                            pConexion,
                                                                                            pTransaccion)

                        PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = PeDet.IdPedidoDet)



                    Next

                    Get_Single_By_IdPedidoEnc = vPedidoEnc

                Next
            Else
                Get_Single_By_IdPedidoEnc = Nothing
            End If


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                                   ByVal lConnection As SqlConnection,
                                                                   ByVal lTransaction As SqlTransaction) As Boolean

        Get_Estado_Enviado_A_ERP_By_IdPedidoEnc = False

        Try

            Const sp As String = "SELECT Enviado_A_ERP FROM Trans_pe_enc 
                                  Where (IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("Enviado_A_ERP")), False, dt.Rows(0).Item("Enviado_A_ERP"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Pedidos_By_IdPickinEnc(ByVal IdPickingEnc As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As DataTable

        Dim lTable As New DataTable("Result")
        Dim vSQL As String = ""

        Try

            vSQL = "SELECT *
                    FROM VW_Pedidos_IdPickingEnc 
                    WHERE IdPickingEnc = @IdPickingEnc"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)
                lDataAdapter.Fill(lTable)

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Pedido_Picking(ByVal pIdPedidoEnc As Integer,
                                                   ByVal pIdPickingEnc As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction,
                                                   ByVal pUsuario As Integer) As Boolean
        Try

            Dim cmd As New SqlCommand() With {
            .Connection = lConnection,
            .Transaction = lTransaction,
            .CommandType = CommandType.Text
        }

            ' Parámetros compartidos
            cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

            ' Eliminar detalle de ubicaciones
            cmd.CommandText = "DELETE FROM Trans_picking_ubic WHERE IdPedidoEnc = @IdPedidoEnc AND IdPickingEnc = @IdPickingEnc"
            If cmd.ExecuteNonQuery() = 0 Then
                Throw New ApplicationException("No se pudo eliminar de Trans_picking_ubic.")
            End If

            ' Eliminar detalle picking
            cmd.CommandText = "DELETE FROM Trans_picking_det WHERE IdPedidoEnc = @IdPedidoEnc AND IdPickingEnc = @IdPickingEnc"
            If cmd.ExecuteNonQuery() = 0 Then
                Throw New ApplicationException("No se pudo eliminar de Trans_picking_det.")
            End If

            ' Actualizar encabezado de pedido
            cmd.CommandText = "UPDATE trans_pe_enc SET IdPickingEnc = 0, estado = 'Pendiente', fec_mod = getdate() WHERE IdPedidoEnc = @IdPedidoEnc AND IdPickingEnc = @IdPickingEnc"
            If cmd.ExecuteNonQuery() = 0 Then
                Throw New ApplicationException("No se pudo actualizar trans_pe_enc.")
            End If

            Dim vPedidosByIdPickingEnc As Integer = Get_Count_Pedidos_By_IdPickingEnc(pIdPickingEnc, lConnection, lTransaction)

            If vPedidosByIdPickingEnc = 0 Then
                ' Eliminar detalle de operadores
                cmd.CommandText = "DELETE FROM Trans_picking_op WHERE IdPickingEnc = @IdPickingEnc"
                If cmd.ExecuteNonQuery() = 0 Then
                    Throw New ApplicationException("No se pudo eliminar de Trans_picking_op.")
                End If

                ' Eliminar picking_enc
                cmd.CommandText = "DELETE FROM Trans_picking_enc WHERE IdPickingEnc = @IdPickingEnc"
                If cmd.ExecuteNonQuery() = 0 Then
                    Throw New ApplicationException("No se pudo eliminar de Trans_picking_enc.")
                End If

            End If

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            'clsLnLog_error_wms.Agregar_Error(1, 1, "Se eliminó el picking para el pedido", pIdPedidoEnc, pIdPickingEnc, 0, pUsuario)
            Dim vMsgDelete As String = $"Se eliminó el picking: {pIdPickingEnc} para el pedido: {pIdPedidoEnc}"
            clsLnLog_error_wms_pe.Agregar_Error(vMsgDelete, pIdPedidoEnc:=pIdPedidoEnc, pConection:=lConnection, pTransaction:=lTransaction)

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT20052025: obtener los pedidos cerrados, activos y no anulados para enviar a la nube
    Public Shared Function GetAll_By_CDC(ByVal pUltimaFechaSincro As Date, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetAll_By_CDC = New List(Of clsBeTrans_pe_enc)
        Dim Es_Transaccion_Remota As Boolean
        Try

            Dim vSQ As String = ""

            vSQ = "SELECT * FROM trans_pe_enc WHERE (activo=1 and anulado=0 and estado='Despachado') and hora_ini >=@PULTIMAFECHASINCRO "

            Es_Transaccion_Remota = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQ, pConection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQ, lConnection)
            End If

            Dim pConexion = If(Es_Transaccion_Remota, pConection, lConnection)
            Dim pTransaccion = If(Es_Transaccion_Remota, pTransaction, lTransaction)

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@PULTIMAFECHASINCRO", pUltimaFechaSincro)

            Dim lDT As New DataTable()

            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                '#GT20052025: iterar cada row que equivale a un pedido
                For Each lrow As DataRow In lDT.Rows

                    Dim vPedidoEnc As New clsBeTrans_pe_enc()

                    Cargar(vPedidoEnc, lrow)

                    With vPedidoEnc
                        .IdPedidoEnc = lrow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lrow("estado")), "", lrow("estado"))
                        .Ubicacion = IIf(IsDBNull(lrow("Ubicacion")), "", lrow("Ubicacion"))
                    End With

                    If vPedidoEnc.Estado = "NUEVO" AndAlso vPedidoEnc.Ubicacion = "TMP" Then
                        Throw New Exception("El pedido seleccionado no es candidato para picking, se creó de forma temporal en el WMS y no se concluyó: Ubicación = TMP")
                    End If

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lrow("IdBodega")), 0, CType(lrow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lrow("IdCliente")), 0, CType(lrow("IdCliente"), Integer))
                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lrow("IdMuelle")), 0, lrow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lrow("IdPropietarioBodega")), 0, CType(lrow("IdPropietarioBodega"), Integer))

                    clsLnCliente.Obtener(vPedidoEnc.Cliente, pConexion, pTransaccion)
                    vPedidoEnc.PropietarioBodega =
                        clsLnPropietario_bodega.Get_Single_With_Propietario(vPedidoEnc.PropietarioBodega.IdPropietarioBodega,
                                                                            pConexion,
                                                                            pTransaccion)

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, pConexion, pTransaccion)
                    End If


                    If Not IsDBNull(lrow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = CType(lrow("IdTipoPedido"), Integer)
                    End If

                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, pConexion, pTransaccion)
                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lrow("IdPickingEnc")), 0, lrow("IdPickingEnc"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lrow("IdMotivoDevolucion")), 0, lrow("IdMotivoDevolucion"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, pConexion, pTransaccion)
                    End If

                    For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle

                        PeDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(PeDet.IdPedidoDet,
                                                                                            PeDet.IdPedidoEnc,
                                                                                            pConexion,
                                                                                            pTransaccion)

                        PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = PeDet.IdPedidoDet)

                    Next

                    GetAll_By_CDC.Add(vPedidoEnc)

                Next
            Else
                GetAll_By_CDC = Nothing
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Function
    Public Shared Function Get_Empresa_By_IdPedidoEnc(IdPedidoEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_Empresa_By_IdPedidoEnc = ""

        Try

            Const sp As String = "SELECT Codigo_Empresa_ERP FROM Trans_pe_enc 
             Where(IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("Codigo_Empresa_ERP")), "", dt.Rows(0).Item("Codigo_Empresa_ERP"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Pedidos_No_Despachados_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                                      ByRef pConection As SqlConnection,
                                                                      ByRef pTransaction As SqlTransaction) As List(Of Integer)

        Dim lReturnList As New List(Of Integer)

        Try
            Const sp As String = "SELECT IdPedidoEnc FROM Trans_pe_enc WHERE IdPickingEnc = @IdPickingEnc AND estado <> 'Despachado'"

            Using cmd As New SqlCommand(sp, pConection, pTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        If Not reader.IsDBNull(0) Then
                            lReturnList.Add(reader.GetInt32(0))
                        End If
                    End While
                End Using
            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        End Try

    End Function
    Public Shared Function Tiene_Productos_Pickeados(ByVal pIdPedidoEnc As Integer,
                                                     ByVal pIdPickingEnc As Integer,
                                                     ByVal pConnection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lPickeados As Integer = 0

            Const sp As String = "SELECT COUNT(IdPickingUbic) cant 
                                  FROM trans_picking_ubic  
                                  WHERE cantidad_recibida >0 AND dañado_picking = 0 AND  
                                        dañado_verificacion = 0 AND  
                                        no_encontrado = 0 AND  
                                        IdPedidoEnc = @IdPedidoEnc AND
                                        IdPickingEnc = @IdPickingEnc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lPickeados = CInt(lReturnValue)
                End If

            End Using

            Return lPickeados > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Pedidos_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                             ByRef pConnection As SqlConnection,
                                                             ByRef pTransaction As SqlTransaction) As Integer

        Get_Count_Pedidos_By_IdPickingEnc = 0

        Try

            Const query As String = "SELECT COUNT(IdPickingEnc) FROM Trans_pe_enc WHERE IdPickingEnc = @IdPickingEnc"

            Using cmd As New SqlCommand(query, pConnection, pTransaction)

                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
                    Get_Count_Pedidos_By_IdPickingEnc = Convert.ToInt32(result)
                End If

            End Using

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw
        End Try

    End Function
    Public Shared Function Actualizar_Estado_Picking(ByVal IdPedidoEnc As Integer,
                                                     ByVal Estado As String,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            If Estado = "Verificado" Then Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc AND estado NOT IN ('Anulado','Despachado')")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", Estado))
            If Estado = "Verificado" Then cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar_IdCliente_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                               ByVal IdCliente As Integer,
                                                               Optional ByVal pConection As SqlConnection = Nothing,
                                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_pe_enc")
            Upd.Add("IdCliente", "@IdCliente", DataType.Parametro)
            Upd.Where("IdPedidoEnc = @IdPedidoEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", IdCliente))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function
    Public Shared Function Get_Estado_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As String
        Dim estado As String = ""

        Try
            Dim sSQL As String = "SELECT Estado FROM trans_pe_enc WHERE IdPedidoEnc = @IdPedidoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("Estado") IsNot DBNull.Value Then
                                estado = lRow("Estado").ToString()
                            End If
                        End If
                    End Using

                    lTransaction.Commit()
                End Using

                lConnection.Close()
            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return estado
    End Function

    Public Shared Function Despachados_En_Otro(ByVal IdPedidoEnc As Integer,
                                          ByVal IdDespachoEnc As Integer,
                                          Optional ByRef pConnection As SqlConnection = Nothing,
                                          Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Dim vDespachadosEnOtros As Integer = 0

        Despachados_En_Otro = False

        Try

            Dim lCommand As New SqlCommand

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim vSQL As String = ";WITH elegibles AS (
                                        SELECT 
                                            u.IdPedidoEnc,
                                            u.IdPedidoDet,
                                            u.IdPickingUbic,
                                            u.cantidad_verificada
                                        FROM trans_picking_ubic u
                                        WHERE u.IdPedidoEnc = @IdPedidoEnc
                                          AND u.cantidad_verificada > 0
                                          AND u.dañado_picking = 0
                                          AND u.dañado_verificacion = 0
                                          AND u.no_encontrado = 0
                                    ),
                                    despachos_otros AS (
                                        SELECT 
                                            d.IdPedidoEnc,
                                            d.IdPedidoDet,
                                            d.IdPickingUbic,
                                            SUM(d.CantidadDespachada) AS cant_despachada_otros
                                        FROM trans_despacho_det d
                                        WHERE d.IdPedidoEnc = @IdPedidoEnc
                                          AND d.IdDespachoEnc <> @IdDespachoEnc     -- excluir el despacho actual
                                        GROUP BY d.IdPedidoEnc, d.IdPedidoDet, d.IdPickingUbic
                                    )
                                    -- Resultado booleano (1 = todos ya despachados en otro despacho, 0 = falta algo):
                                    SELECT CASE 
                                             WHEN NOT EXISTS (
                                                  SELECT 1
                                                  FROM elegibles e
                                                  LEFT JOIN despachos_otros o
                                                    ON  o.IdPedidoEnc   = e.IdPedidoEnc
                                                    AND o.IdPedidoDet   = e.IdPedidoDet
                                                    AND o.IdPickingUbic = e.IdPickingUbic
                                                  WHERE ISNULL(o.cant_despachada_otros, 0) < e.cantidad_verificada
                                             )
                                             THEN 1 ELSE 0
                                           END AS TodosDespachadosEnOtro;"

            lCommand.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(vSQL, pConnection)
                lCommand.Transaction = pTransaction
            Else
                lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lCommand = New SqlCommand(vSQL, lConnection, ltransaction)
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdDespachoEnc", IdDespachoEnc))

            vDespachadosEnOtros = lCommand.ExecuteScalar()

            Despachados_En_Otro = (vDespachadosEnOtros <> 0)

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetSingle_For_Pedido(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc

        GetSingle_For_Pedido = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM VW_Get_Single_Pedido WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Cargar(vPedidoEnc, lRow)

                    With vPedidoEnc
                        .IdPedidoEnc = lRow("IdPedidoEnc")
                        .Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                        .Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    End With

                    vPedidoEnc.IdBodega = IIf(IsDBNull(lRow("IdBodega")), 0, CType(lRow("IdBodega"), Integer))
                    vPedidoEnc.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, lRow("IdCliente"))
                    vPedidoEnc.Cliente.IdCliente = IIf(IsDBNull(lRow("IdCliente")), 0, lRow("IdCliente"))
                    vPedidoEnc.Cliente.Codigo = IIf(IsDBNull(lRow("Codigo_Cliente")), "", lRow("Codigo_Cliente"))
                    vPedidoEnc.Cliente.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Cliente")), "", lRow("Nombre_Cliente"))
                    vPedidoEnc.Cliente.Es_bodega_recepcion = IIf(IsDBNull(lRow("Es_bodega_recepcion")), False, lRow("Es_bodega_recepcion"))
                    vPedidoEnc.Cliente.Es_Bodega_Traslado = IIf(IsDBNull(lRow("Es_Bodega_Traslado")), False, lRow("Es_Bodega_Traslado"))
                    vPedidoEnc.Cliente.Control_Ultimo_Lote = IIf(IsDBNull(lRow("control_ultimo_lote_cliente")), False, lRow("control_ultimo_lote_cliente"))
                    vPedidoEnc.Cliente.IdUbicacionVirtual = IIf(IsDBNull(lRow("IdUbicacionVirtual")), False, lRow("IdUbicacionVirtual"))
                    vPedidoEnc.Cliente.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.Cliente.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.IdMuelle = IIf(IsDBNull(lRow("IdMuelle")), 0, lRow("IdMuelle"))
                    vPedidoEnc.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, lRow("IdPropietarioBodega"))
                    vPedidoEnc.PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(lRow("IdPropietarioBodega")), 0, lRow("IdPropietarioBodega"))
                    vPedidoEnc.PropietarioBodega.Propietario.IdPropietario = IIf(IsDBNull(lRow("IdPropietario")), 0, lRow("IdPropietario"))
                    vPedidoEnc.PropietarioBodega.Propietario.Nombre_comercial = IIf(IsDBNull(lRow("Nombre_Propietario")), "", lRow("Nombre_Propietario"))

                    If Not IsDBNull(lRow("IdTipoPedido")) Then
                        vPedidoEnc.TipoPedido.IdTipoPedido = IIf(IsDBNull(lRow("IdTipoPedido")), 1, lRow("IdTipoPedido"))
                        vPedidoEnc.IdTipoPedido = IIf(IsDBNull(lRow("IdTipoPedido")), 1, lRow("IdTipoPedido"))
                    End If

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        vPedidoEnc.TipoPedido = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(vPedidoEnc.TipoPedido.IdTipoPedido, lConnection, lTransaction)
                    End If

                    vPedidoEnc.RoadIdRuta = IIf(IsDBNull(lRow("RoadIdRuta")), 0, CType(lRow("RoadIdRuta"), Integer))
                    vPedidoEnc.RoadIdVendedor = IIf(IsDBNull(lRow("RoadIdVendedor")), 0, CType(lRow("RoadIdVendedor"), Integer))

                    vPedidoEnc.RoadIdRutaDespacho = IIf(IsDBNull(lRow("RoadIdRutaDespacho")), 0, CType(lRow("RoadIdRutaDespacho"), Integer))
                    vPedidoEnc.RoadIdVendedorDespacho = IIf(IsDBNull(lRow("RoadIdVendedorDespacho")), 0, CType(lRow("RoadIdVendedorDespacho"), Integer))

                    vPedidoEnc.Fecha_Pedido = IIf(IsDBNull(lRow("Fecha_Pedido")), Now, lRow("Fecha_Pedido"))
                    vPedidoEnc.Hora_ini = IIf(IsDBNull(lRow("Hora_ini")), Now, lRow("Hora_ini"))
                    vPedidoEnc.Hora_fin = IIf(IsDBNull(lRow("Hora_fin")), Now, lRow("Hora_fin"))
                    vPedidoEnc.Ubicacion = IIf(IsDBNull(lRow("Ubicacion")), "", lRow("Ubicacion"))
                    vPedidoEnc.Estado = IIf(IsDBNull(lRow("estado")), "", lRow("estado"))
                    vPedidoEnc.No_despacho = IIf(IsDBNull(lRow("No_despacho")), "0", lRow("No_despacho"))
                    vPedidoEnc.Activo = IIf(IsDBNull(lRow("activo")), True, lRow("activo"))
                    vPedidoEnc.User_agr = IIf(IsDBNull(lRow("user_agr")), "", lRow("user_agr"))
                    vPedidoEnc.Fec_agr = IIf(IsDBNull(lRow("fec_agr")), Now, lRow("fec_agr"))
                    vPedidoEnc.User_mod = IIf(IsDBNull(lRow("user_mod")), "", lRow("user_mod"))
                    vPedidoEnc.Fec_mod = IIf(IsDBNull(lRow("fec_mod")), "", lRow("fec_mod"))
                    vPedidoEnc.No_documento = IIf(IsDBNull(lRow("no_documento")), "", lRow("no_documento"))
                    vPedidoEnc.Local = IIf(IsDBNull(lRow("local")), False, lRow("local"))
                    vPedidoEnc.Pallet_primero = IIf(IsDBNull(lRow("pallet_primero")), False, lRow("pallet_primero"))
                    vPedidoEnc.Dias_cliente = IIf(IsDBNull(lRow("dias_cliente")), "0", lRow("dias_cliente"))
                    vPedidoEnc.Anulado = IIf(IsDBNull(lRow("anulado")), False, lRow("anulado"))
                    vPedidoEnc.RoadKilometraje = IIf(IsDBNull(lRow("RoadKilometraje")), "0", lRow("RoadKilometraje"))
                    vPedidoEnc.RoadFechaEntr = IIf(IsDBNull(lRow("RoadFechaEntr")), Now, lRow("RoadFechaEntr"))
                    vPedidoEnc.HoraEntregaDesde = IIf(IsDBNull(lRow("HoraEntregaDesde")), Now, lRow("HoraEntregaDesde"))
                    vPedidoEnc.HoraEntregaHasta = IIf(IsDBNull(lRow("HoraEntregaHasta")), Now, lRow("HoraEntregaHasta"))
                    vPedidoEnc.RoadDirEntrega = IIf(IsDBNull(lRow("RoadDirEntrega")), "", lRow("RoadDirEntrega"))
                    vPedidoEnc.RoadTotal = IIf(IsDBNull(lRow("RoadTotal")), "0", lRow("RoadTotal"))
                    vPedidoEnc.RoadDesMonto = IIf(IsDBNull(lRow("RoadDesMonto")), "0", lRow("RoadDesMonto"))
                    vPedidoEnc.RoadImpMonto = IIf(IsDBNull(lRow("RoadImpMonto")), "0", lRow("RoadImpMonto"))
                    vPedidoEnc.RoadPeso = IIf(IsDBNull(lRow("RoadPeso")), "0", lRow("RoadPeso"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadBandera = IIf(IsDBNull(lRow("RoadBandera")), "", lRow("RoadBandera"))
                    vPedidoEnc.RoadStatCom = IIf(IsDBNull(lRow("RoadStatCom")), "", lRow("RoadStatCom"))
                    vPedidoEnc.RoadCalcoBJ = IIf(IsDBNull(lRow("RoadCalcoBJ")), "", lRow("RoadCalcoBJ"))
                    vPedidoEnc.RoadImpres = IIf(IsDBNull(lRow("RoadImpres")), "0", lRow("RoadImpres"))
                    vPedidoEnc.RoadADD1 = IIf(IsDBNull(lRow("RoadADD1")), "", lRow("RoadADD1"))
                    vPedidoEnc.RoadADD2 = IIf(IsDBNull(lRow("RoadADD2")), "", lRow("RoadADD2"))
                    vPedidoEnc.RoadADD3 = IIf(IsDBNull(lRow("RoadADD3")), "", lRow("RoadADD3"))
                    vPedidoEnc.RoadStatProc = IIf(IsDBNull(lRow("RoadStatProc")), "", lRow("RoadStatProc"))
                    vPedidoEnc.RoadRechazado = IIf(IsDBNull(lRow("RoadRechazado")), "", lRow("RoadRechazado"))
                    vPedidoEnc.RoadRazon_Rechazado = IIf(IsDBNull(lRow("RoadRazon_Rechazado")), "", lRow("RoadRazon_Rechazado"))
                    vPedidoEnc.RoadInformado = IIf(IsDBNull(lRow("RoadInformado")), False, lRow("RoadInformado"))
                    vPedidoEnc.RoadSucursal = IIf(IsDBNull(lRow("RoadSucursal")), False, lRow("RoadSucursal"))
                    vPedidoEnc.RoadIdDespacho = IIf(IsDBNull(lRow("RoadIdDespacho")), False, lRow("RoadIdDespacho"))
                    vPedidoEnc.RoadIdFacturacion = IIf(IsDBNull(lRow("RoadIdFacturacion")), False, lRow("RoadIdFacturacion"))
                    vPedidoEnc.Referencia = IIf(IsDBNull(lRow("referencia")), "", lRow("referencia"))
                    vPedidoEnc.Enviado_A_ERP = IIf(IsDBNull(lRow("Enviado_A_ERP")), False, lRow("Enviado_A_ERP"))
                    vPedidoEnc.No_Picking_ERP = IIf(IsDBNull(lRow("No_Picking_ERP")), "", lRow("No_Picking_ERP"))
                    vPedidoEnc.Observacion = IIf(IsDBNull(lRow("Observacion")), "", lRow("Observacion"))
                    vPedidoEnc.Fecha_Preparacion = IIf(IsDBNull(lRow("Fecha_Preparacion")), Now, lRow("Fecha_Preparacion"))
                    vPedidoEnc.IdTipoManufactura = IIf(IsDBNull(lRow("IdTipoManufactura")), 0, lRow("IdTipoManufactura"))
                    vPedidoEnc.Bodega_Origen = IIf(IsDBNull(lRow("Bodega_Origen")), "", lRow("Bodega_Origen"))
                    vPedidoEnc.Bodega_Destino = IIf(IsDBNull(lRow("Bodega_Destino")), "", lRow("Bodega_Destino"))
                    vPedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = IIf(IsDBNull(lRow("Referencia_Documento_Ingreso_Bodega_Destino")), "", lRow("Referencia_Documento_Ingreso_Bodega_Destino"))
                    vPedidoEnc.IdMotivoDevolucion = IIf(IsDBNull(lRow("IdMotivoDevolucion")), 0, lRow("IdMotivoDevolucion"))

                    '#GT17092025: método exclusivo para cargar detalle de pedido que no filtra por linea con stock_liberado.
                    'el detalle del pedido debe filtrarse unicamente en el despacho cuando se aplica parcial.
                    vPedidoEnc.Detalle = clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc_For_Pedido(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    vPedidoEnc.IdPickingEnc = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))

                    If vPedidoEnc.IdPickingEnc <> 0 Then
                        vPedidoEnc.Picking.IdPickingEnc = vPedidoEnc.IdPickingEnc
                        vPedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(vPedidoEnc.IdPickingEnc, lConnection, lTransaction)
                    End If

                    Dim ListaStockRes = clsLnStock_res.fGet_All_By_IdPedidoEnc(vPedidoEnc.IdPedidoEnc, lConnection, lTransaction)

                    If vPedidoEnc.Detalle.Count > 20 Then
                        ' Procesamiento paralelo para listas grandes
                        Parallel.ForEach(vPedidoEnc.Detalle, Sub(PeDet)
                                                                 PeDet.ListaStockRes = ListaStockRes.
                                                                     Where(Function(x) x.IdPedido = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                                                     ToList()

                                                                 If vPedidoEnc.Picking IsNot Nothing Then
                                                                     PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.
                                                                         Where(Function(x) x.IdPedidoEnc = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                                                         ToList()
                                                                 End If
                                                             End Sub)
                    Else
                        ' Procesamiento secuencial para listas pequeñas
                        For Each PeDet As clsBeTrans_pe_det In vPedidoEnc.Detalle
                            PeDet.ListaStockRes = ListaStockRes.
                                Where(Function(x) x.IdPedido = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                ToList()

                            If vPedidoEnc.Picking IsNot Nothing Then
                                PeDet.ListaPickingUbic = vPedidoEnc.Picking.ListaPickingUbic.
                                    Where(Function(x) x.IdPedidoEnc = PeDet.IdPedidoEnc AndAlso x.IdPedidoDet = PeDet.IdPedidoDet).
                                    ToList()
                            End If
                        Next
                    End If


                End If

                GetSingle_For_Pedido = vPedidoEnc

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                Try
                    lTransaction.Rollback()
                Catch ex1 As Exception
                    Debug.Print(ex1.Message)
                End Try
            End If
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_No_Picking_ERP(ByVal pIdPedidoEnc As Integer,
                                              ByVal pNo_Despacho As String) As String

        Get_No_Picking_ERP = ""

        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            Dim vSQL As String = "SELECT No_Picking_ERP FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc and no_despacho = @no_despacho "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text

                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lCommand.Parameters.AddWithValue("@no_despacho", pNo_Despacho)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_No_Picking_ERP = lReturnValue
                        End If

                    End Using

                    lTransaction.Commit()

            End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Entrega_By_IdDespachoEnc(ByVal pIdDespachoEnc As Integer) As String

        Existe_Entrega_By_IdDespachoEnc = ""

        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            Dim vSQL As String = "SELECT no_pase FROM trans_despacho_enc WHERE IdDespachoEnc=@IdDespachoEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text

                        lCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Existe_Entrega_By_IdDespachoEnc = lReturnValue
            End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function GetIdPropietarioBodega(ByVal pIdPedidoEnc As Integer,
                                                  ByRef pConnection As SqlConnection,
                                                  ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim lCliente As Integer = 0

            Const sp As String = "SELECT IdPropietarioBodega FROM trans_pe_enc WHERE IdPedidoEnc = @pIdPedidoEnc"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdPedidoEnc", pIdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCliente = lReturnValue
                End If

            End Using

            Return lCliente

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Referencia_And_Company(ByRef pBeTrans_pe_enc As clsBeTrans_pe_enc,
                                                                ByVal pConection As SqlConnection,
                                                                ByVal pTransaction As SqlTransaction) As clsBeTrans_pe_enc

        Get_Single_By_Referencia_And_Company = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_enc " &
            " Where(Referencia = @Referencia AND IdTipoPedido = @IdTipoPedido and Codigo_Empresa_ERP = @Codigo_Empresa_ERP) "

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", pBeTrans_pe_enc.Referencia))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdTipoPedido", pBeTrans_pe_enc.IdTipoPedido))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Codigo_Empresa_ERP", pBeTrans_pe_enc.Codigo_Empresa_ERP))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then
                Dim ObjUM As New clsBeTrans_pe_enc()
                Cargar(ObjUM, dt.Rows(0))
                Return ObjUM
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    '#GT16102025: cargar el pedido en funcion del despacho por transferencia entre bodegas para cealsa    
    Public Shared Function GetPedido_By_IdDespachoEnc(ByVal pIdDespachoEnc As Integer) As clsBeTrans_pe_enc

        GetPedido_By_IdDespachoEnc = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQ As String = "SELECT top 1 * FROM trans_pe_enc WHERE (no_despacho=@pIdDespachoEnc) "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdDespachoEnc", pIdDespachoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                Dim vPedidoEnc As New clsBeTrans_pe_enc()

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    vPedidoEnc = New clsBeTrans_pe_enc()

                    Cargar(vPedidoEnc, lRow)

                    If vPedidoEnc.TipoPedido.IdTipoPedido > 0 Then
                        clsLnTrans_pe_tipo.Obtener(vPedidoEnc.TipoPedido, lConnection, lTransaction)
                    End If

                    GetPedido_By_IdDespachoEnc = vPedidoEnc

                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function GetAll_Tmp(ByVal pIdBodega As Integer,
                                      ByVal pFechaDel As Date,
                                      ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = " SELECT * FROM VW_PEDIDOS_LIST_TMP WHERE IDBODEGA = @IDBODEGA AND UBICACION='TMP' "

            vSQL += " AND cast(Fecha_Pedido AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_No_Picking_ERP_By_IdPedidoEnc_And_NoDespacho(ByVal pIdPedidoEnc As Integer,
                                                                            ByVal pNo_Despacho As String,
                                                                            ByVal lConnection As SqlConnection,
                                                                            ByVal lTransaction As SqlTransaction) As String

        Get_No_Picking_ERP_By_IdPedidoEnc_And_NoDespacho = ""

        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            Dim vSQL As String = "SELECT No_Picking_ERP FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc and no_despacho = @no_despacho "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text

                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lCommand.Parameters.AddWithValue("@no_despacho", pNo_Despacho)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_No_Picking_ERP_By_IdPedidoEnc_And_NoDespacho = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_No_Picking_ERP_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As String

        Get_No_Picking_ERP_By_IdPedidoEnc = ""

        Dim vPedidoEnc As New clsBeTrans_pe_enc()

        Try

            Dim vSQL As String = "SELECT No_Picking_ERP FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_No_Picking_ERP_By_IdPedidoEnc = lReturnValue
                    End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPicking_By_IdPedido(ByVal pIdPedidoEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Get_IdPicking_By_IdPedido = 0

        Try

            Dim vSQ As String = "SELECT ISNULL(IdPickingEnc,0) AS IdPickingEnc FROM trans_pe_enc WHERE IdPedidoEnc=@IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQ, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Get_IdPicking_By_IdPedido = IIf(IsDBNull(lRow("IdPickingEnc")), 0, lRow("IdPickingEnc"))
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdCliente_And_IdPedidoEnc_By_IdPickingUbic(ByVal pIdPickingUbic As Integer, ByVal pIdPickingEnc As Integer) As Tuple(Of Integer, Integer)

        Dim vIdCliente As Integer = 0
        Dim vIdPedidoEnc As Integer = 0

        Try

            Dim vSQL As String = "SELECT penc.IdCliente, penc.IdPedidoEnc
                              FROM trans_pe_enc penc
                              JOIN trans_picking_ubic pu ON penc.IdPedidoEnc = pu.IdPedidoEnc
                              WHERE pu.IdPickingUbic=@IdPickingUbic AND pu.IdPickingEnc=@IdPickingEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCmd As New SqlCommand(vSQL, lConnection, lTransaction)
                        lCmd.Parameters.AddWithValue("@IdPickingUbic", pIdPickingUbic)
                        lCmd.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Using lReader As SqlDataReader = lCmd.ExecuteReader()

                            If lReader.Read() Then

                                If Not IsDBNull(lReader("IdCliente")) Then
                                    vIdCliente = CInt(lReader("IdCliente"))
                                End If

                                If Not IsDBNull(lReader("IdPedidoEnc")) Then
                                    vIdPedidoEnc = CInt(lReader("IdPedidoEnc"))
                                End If

                            End If

                End Using

                    End Using

                    lTransaction.Commit()
                End Using
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        End Try

        Return Tuple.Create(vIdCliente, vIdPedidoEnc)

    End Function

    Public Shared Function Get_Estado_Enviado_A_ERP(ByVal Referencia As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As Boolean

        Get_Estado_Enviado_A_ERP = False

        Try

            Const sp As String = "SELECT Enviado_A_ERP FROM Trans_pe_enc 
             Where(Referencia = @Referencia)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Referencia", Referencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("Enviado_A_ERP")), False, dt.Rows(0).Item("Enviado_A_ERP"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT25112025: cargar lista de pedidos pickeados exclusivo para verificacion_bof
    Public Shared Function GetAll_By_VerificacionBOF(ByVal pActivo As Boolean,
                                                     ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal pIdUsuario As Integer) As DataTable




        Dim lTable As New DataTable("Result")

        Try

            '#GT11122025: mostrar los pedidos asociados al usuario para no filtrar por cada idbodega
            Dim vSQL As String = " SELECT * " &
                     " FROM VW_PEDIDOS_LIST " &
                     " WHERE IdBodega IN ( " &
                     "     SELECT ub.IdBodega " &
                     "     FROM Usuario_Bodega ub " &
                     "     WHERE ub.IdUsuario = @pIdUsuario " &
                     " ) " &
                     " AND verificar_con_imagen = 1 " &
                     " AND estado = 'Pickeado' "

            If pActivo = True Then
                vSQL += " AND Activo = 1 "
            Else
                vSQL += " AND Activo = 0 "
            End If

            vSQL += " AND CAST(Fecha_Pedido AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) & " AND " & FormatoFechas.fFecha(pFechaAl)

            vSQL += " ORDER BY CONVERT(date, Fecha_Pedido) ASC, Correlativo ASC "


            'Dim vSQL As String = " SELECT * FROM VW_PEDIDOS_LIST WHERE IDBODEGA = @IDBODEGA and verificar_con_imagen=1 and estado='Pickeado' "

            'If pActivo = True Then
            '    vSQL += " AND Activo=1 "
            'Else
            '    vSQL += " AND Activo=0"
            'End If

            'vSQL += " AND cast(Fecha_Pedido AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
            '       " AND " & FormatoFechas.fFecha(pFechaAl)

            'vSQL += " ORDER BY CONVERT(date, Fecha_Pedido) ASC, Correlativo ASC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdUsuario", pIdUsuario)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function GetAll_By_Guia_Transporte(ByVal guia_Transporte As String) As DataTable

        GetAll_By_Guia_Transporte = Nothing

        Try

            Const sp As String =
                "SELECT IdPedidoEnc AS Correlativo " &
                "FROM Trans_pe_enc " &
                "WHERE guia_transporte = @guia_transporte"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@guia_transporte", guia_Transporte))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    lTransaction.Commit()

                    GetAll_By_Guia_Transporte = dt

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_Single_By_NoGuia(ByVal NoGuia As String) As clsBeTrans_pe_enc

        Get_Single_By_NoGuia = Nothing

        Try

            Const sp As String = " SELECT * FROM Trans_pe_enc " &
                                 " Where(guia_transporte = @guia_transporte)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@guia_transporte", NoGuia))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count >= 1 Then
                        Dim BePedidoEnc As New clsBeTrans_pe_enc()
                        Cargar(BePedidoEnc, dt.Rows(0))
                        Get_Single_By_NoGuia = BePedidoEnc
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class