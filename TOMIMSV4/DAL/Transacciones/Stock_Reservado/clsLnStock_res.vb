Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_res

    Private Shared lPresentaciones As New List(Of clsBeProducto_Presentacion)

    Public Shared Sub Cargar(ByRef oBeStock_res As clsBeStock_res, ByRef dr As DataRow)

        Try

            With oBeStock_res

                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .Indicador = IIf(IsDBNull(dr.Item("Indicador")), "", dr.Item("Indicador"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("idproductoestado")), 0, dr.Item("idproductoestado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                .Ubicacion_ant = IIf(IsDBNull(dr.Item("ubicacion_ant")), "", dr.Item("ubicacion_ant"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), "", dr.Item("no_bulto"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .IdPicking = IIf(IsDBNull(dr.Item("IdPicking")), 0, dr.Item("IdPicking"))
                .IdPedido = IIf(IsDBNull(dr.Item("IdPedido")), 0, dr.Item("IdPedido"))
                .IdDespacho = IIf(IsDBNull(dr.Item("IdDespacho")), 0, dr.Item("IdDespacho"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Host = IIf(IsDBNull(dr.Item("host")), "", dr.Item("host"))
                .añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .Fecha_manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Pallet_no_estandar = IIf(IsDBNull(dr.Item("Pallet_no_estandar")), 0, dr.Item("Pallet_no_estandar"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK 20180329 12:43 PM Modifiqué el ByVal de la conexión y la transacción por ByRef
    Public Shared Function Insertar(ByRef oBeStock_res As clsBeStock_res, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20260520_RESERVA_BYB_FIX: evitar que un candidato agotado revierta reservas validas ya calculadas.
            If Math.Round(oBeStock_res.Cantidad, 6) <= 0 Then
                clsReservaMi3DebugTrace.EventoStockRes(clsReservaMi3DebugTrace.ObtenerActual(),
                                                       "stock_res_insertar_omitido_cantidad_no_positiva",
                                                       oBeStock_res,
                                                       "Cantidad", clsReservaMi3DebugTrace.Valor(oBeStock_res.Cantidad))
                Return 0
            End If

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            Dim lConnectionTrabajo As SqlConnection = If(Es_Transaccion_Remota, pConection, lConnection)
            Dim lTransactionTrabajo As SqlTransaction = If(Es_Transaccion_Remota, pTransaction, lTransaction)

            Ajustar_Cantidad_Maxima_PedidoDet(oBeStock_res, lConnectionTrabajo, lTransactionTrabajo)

            If Math.Round(oBeStock_res.Cantidad, 6) <= 0 Then
                clsReservaMi3DebugTrace.EventoStockRes(clsReservaMi3DebugTrace.ObtenerActual(),
                                                       "stock_res_insertar_omitido_sin_pendiente_pedido_det",
                                                       oBeStock_res,
                                                       "Cantidad", clsReservaMi3DebugTrace.Valor(oBeStock_res.Cantidad))
                If Not Es_Transaccion_Remota Then lTransaction.Commit()
                Return 0
            End If

            Ins.Init("stock_res")
            'Ins.Add("idstockres", "@idstockres", DataType.Parametro)
            Ins.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Ins.Add("indicador", "@indicador", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("ubicacion_ant", "@ubicacion_ant", DataType.Parametro)
            Ins.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Ins.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Ins.Add("idpicking", "@idpicking", DataType.Parametro)
            Ins.Add("idpedido", "@idpedido", DataType.Parametro)
            Ins.Add("iddespacho", "@iddespacho", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("host", "@host", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("Pallet_no_estandar", "@Pallet_no_estandar", DataType.Parametro)
            '#GT29082025: sin talla color, no insertar valor 0 para mantener consistencia
            If oBeStock_res.IdProductoTallaColor > 0 Then
                Ins.Add("Talla", "@Talla", DataType.Parametro)
                Ins.Add("Color", "@Color", DataType.Parametro)
                Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            End If

            Dim sp As String = Ins.SQLIdentity("IdStockRes")
            Dim cmd As SqlCommand

            cmd = New SqlCommand(sp, lConnectionTrabajo, lTransactionTrabajo) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeStock_res.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@INDICADOR", oBeStock_res.Indicador))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeStock_res.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_res.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_res.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_res.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_res.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_res.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_res.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_res.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_res.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_res.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_res.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_res.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_res.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_res.Peso))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeStock_res.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_res.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_res.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_res.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_ANT", oBeStock_res.Ubicacion_ant))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_res.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeStock_res.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPICKING", oBeStock_res.IdPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDO", oBeStock_res.IdPedido))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHO", oBeStock_res.IdDespacho))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_res.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_res.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_res.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_res.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeStock_res.Host))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_res.añada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_res.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_res.Pallet_no_estandar))
            If oBeStock_res.IdProductoTallaColor > 0 Then
                cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_res.Talla))
                cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_res.Color))
                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_res.IdProductoTallaColor))
            End If

            'Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeStock_res.IdStockRes = newId
            clsLnStock_res.Limpiar_Cache_StockReservado_MI3(clsReservaMi3DebugTrace.ObtenerActual())

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return 1

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#EJC20260520_RESERVA_BYB_FIX: defensa final para que una linea de pedido normalizada a UM base no quede sobre-reservada.
    Private Shared Sub Ajustar_Cantidad_Maxima_PedidoDet(ByRef oBeStock_res As clsBeStock_res,
                                                         ByRef pConnection As SqlConnection,
                                                         ByRef pTransaction As SqlTransaction)

        If oBeStock_res.IdPedidoDet <= 0 Then Exit Sub
        If Math.Round(oBeStock_res.Cantidad, 6) <= 0 Then Exit Sub

        Const sp As String = "SELECT " &
                              " CAST(ISNULL((SELECT TOP 1 d.Cantidad FROM trans_pe_det d WHERE d.IdPedidoDet = @IdPedidoDet), 0) AS FLOAT) AS CantidadPedido, " &
                             " CAST(ISNULL((SELECT TOP 1 ISNULL(d.IdPresentacion, 0) FROM trans_pe_det d WHERE d.IdPedidoDet = @IdPedidoDet), 0) AS INT) AS IdPresentacionPedido, " &
                             " CAST(ISNULL((SELECT SUM(sr.cantidad) FROM stock_res sr WHERE sr.IdPedidoDet = @IdPedidoDet AND sr.estado IN ('UNCOMMITED HH','UNCOMMITED')), 0) AS FLOAT) AS CantidadReservada "

        Using cmd As New SqlCommand(sp, pConnection, pTransaction)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDet", oBeStock_res.IdPedidoDet))

            Using dr As SqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    Dim vCantidadPedido As Double = IIf(IsDBNull(dr.Item("CantidadPedido")), 0, dr.Item("CantidadPedido"))
                    Dim vIdPresentacionPedido As Integer = IIf(IsDBNull(dr.Item("IdPresentacionPedido")), 0, dr.Item("IdPresentacionPedido"))
                    Dim vCantidadReservada As Double = IIf(IsDBNull(dr.Item("CantidadReservada")), 0, dr.Item("CantidadReservada"))

                    If vCantidadPedido <= 0 Then Exit Sub
                    'Las lineas con presentacion tienen semantica propia de conversion; no se deben topar contra UM base aqui.
                    If vIdPresentacionPedido > 0 Then Exit Sub

                    Dim vCantidadPendiente As Double = Math.Round(vCantidadPedido - vCantidadReservada, 6)

                    If vCantidadPendiente <= 0 Then
                        clsReservaMi3DebugTrace.EventoStockRes(clsReservaMi3DebugTrace.ObtenerActual(),
                                                               "stock_res_cantidad_ajustada_sin_pendiente",
                                                               oBeStock_res,
                                                               "CantidadPedido", clsReservaMi3DebugTrace.Valor(vCantidadPedido),
                                                               "CantidadReservada", clsReservaMi3DebugTrace.Valor(vCantidadReservada))
                        oBeStock_res.Cantidad = 0
                    ElseIf Math.Round(oBeStock_res.Cantidad, 6) > vCantidadPendiente Then
                        clsReservaMi3DebugTrace.EventoStockRes(clsReservaMi3DebugTrace.ObtenerActual(),
                                                               "stock_res_cantidad_ajustada_a_pendiente",
                                                               oBeStock_res,
                                                               "CantidadOriginal", clsReservaMi3DebugTrace.Valor(oBeStock_res.Cantidad),
                                                               "CantidadPedido", clsReservaMi3DebugTrace.Valor(vCantidadPedido),
                                                               "CantidadReservada", clsReservaMi3DebugTrace.Valor(vCantidadReservada),
                                                               "CantidadPendiente", clsReservaMi3DebugTrace.Valor(vCantidadPendiente))
                        oBeStock_res.Cantidad = vCantidadPendiente
                    End If
                End If
            End Using
        End Using

    End Sub

    Public Shared Function Actualizar(ByRef oBeStock_res As clsBeStock_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_res")
            Upd.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Upd.Add("indicador", "@indicador", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Upd.Add("ubicacion_ant", "@ubicacion_ant", DataType.Parametro)
            Upd.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Upd.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Upd.Add("idpicking", "@idpicking", DataType.Parametro)
            Upd.Add("idpedido", "@idpedido", DataType.Parametro)
            Upd.Add("iddespacho", "@iddespacho", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("host", "@host", DataType.Parametro)
            Upd.Add("añada", "@añada", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("Pallet_no_estandar", "@Pallet_no_estandar", DataType.Parametro)
            '#GT29082025: sin talla color, no insertar valor 0, para mantener consistencia
            If oBeStock_res.IdProductoTallaColor > 0 Then
                Upd.Add("Talla", "@Talla", DataType.Parametro)
                Upd.Add("Color", "@Color", DataType.Parametro)
                Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            End If

            Upd.Where("IdStockRes = @IdStockRes")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeStock_res.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@INDICADOR", oBeStock_res.Indicador))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeStock_res.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_res.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_res.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_res.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_res.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_res.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@idproductoestado", oBeStock_res.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_res.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_res.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_res.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_res.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_res.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_res.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_res.Peso))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeStock_res.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_res.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_res.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_res.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_ANT", oBeStock_res.Ubicacion_ant))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_res.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeStock_res.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPICKING", oBeStock_res.IdPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDO", oBeStock_res.IdPedido))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHO", oBeStock_res.IdDespacho))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_res.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_res.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_res.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_res.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeStock_res.Host))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_res.añada))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_res.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_res.Pallet_no_estandar))
            If oBeStock_res.IdProductoTallaColor > 0 Then
                cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_res.Talla))
                cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_res.Color))
                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_res.IdProductoTallaColor))
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeStock_res As clsBeStock_res, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_res  Where(IdStockRes = @IdStockRes)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeStock_res As clsBeStock_res) As Boolean

        Try

            Const sp As String = "SELECT * FROM Stock_res" &
            " Where(IdStockRes = @IdStockRes)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeStock_res, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeStock_res As clsBeStock_res) As Boolean

        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock_res " &
            " Where(IdStockRes = @IdStockRes) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", pBeStock_res.IdStockRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_res, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception

            If lConnection.State = ConnectionState.Open Then lConnection.Close()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex

        End Try

    End Function

    Public Shared Function Actualizar_IdPickingEnc(ByRef oBeStock_res As clsBeStock_res,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_res")
            Upd.Add("idpicking", "@idpicking", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Where("IdStockRes = @IdStockRes AND IdPedido=@IdPedido")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeStock_res.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDPICKING", oBeStock_res.IdPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDO", oBeStock_res.IdPedido))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_res.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_res.Fec_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Friend Shared Function Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado(ByVal IdPedidoEnc As Integer,
                                                                              ByVal lConnection As SqlConnection,
                                                                              ByVal lTransaction As SqlTransaction) As Boolean

        Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado = False

        Try


            Const sp As String = "SELECT * FROM Stock_res " &
                                 " Where(IdPedido = @IdPedidoEnc AND IdPicking =0  and Indicador  = 'PED')"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado = (dt.Rows.Count = 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdStockRes(ByVal pIdBodega As Integer,
                                                   ByVal pIdStockRes As Integer) As clsBeStock_res

        GetSingle_By_IdStockRes = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock_res 
                                  WHERE(IdBodega = @IdBodega AND IdStockRes = @IdStockRes ) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", pIdStockRes))


            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeStock_res As New clsBeStock_res
                Cargar(pBeStock_res, dt.Rows(0))
                GetSingle_By_IdStockRes = pBeStock_res
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdStockRes(ByVal pIdBodega As Integer,
                                                   ByVal pIdStockRes As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As clsBeStock_res

        GetSingle_By_IdStockRes = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_res 
                                  WHERE(IdBodega = @IdBodega AND IdStockRes = @IdStockRes ) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", pIdStockRes))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeStock_res As New clsBeStock_res
                Cargar(pBeStock_res, dt.Rows(0))
                GetSingle_By_IdStockRes = pBeStock_res
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdStockRes(ByRef pBeStock_res As clsBeStock_res, ByRef lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Get_Single_By_IdStockRes = False

        Try

            Const sp As String = "SELECT * FROM Stock_res " &
            " Where(IdStockRes = @IdStockRes) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", pBeStock_res.IdStockRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_res, dt.Rows(0))
                Get_Single_By_IdStockRes = True
            End If

        Catch ex1 As SqlException
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        End Try

    End Function

    Public Shared Function GetAll_By_IdStock_And_IdPickingEnc(ByVal pIdBodega As Integer,
                                                                ByVal pIdStock As Integer,
                                                                ByVal pIdPickingEnc As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_res)

        GetAll_By_IdStock_And_IdPickingEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_res 
                                  WHERE(IdBodega = @IdBodega AND IdStock = @IdStock AND IdPicking = @IdPicking ) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pIdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKING", pIdPickingEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then

                Dim lReturnList As New List(Of clsBeStock_res)

                For Each lRow As DataRow In dt.Rows

                    Dim pBeStock_res As New clsBeStock_res
                    Cargar(pBeStock_res, lRow)
                    lReturnList.Add(pBeStock_res)

                Next

                GetAll_By_IdStock_And_IdPickingEnc = lReturnList

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
