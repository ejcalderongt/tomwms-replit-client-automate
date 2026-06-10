Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_movimientos

    Private Shared Function IdMovimientoEsIdentity(ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As Boolean
        Const sql As String = "SELECT CONVERT(bit, ISNULL(COLUMNPROPERTY(OBJECT_ID('dbo.trans_movimientos'),'IdMovimiento','IsIdentity'),0))"

        Using cmd As New SqlCommand(sql, pConnection, pTransaction)
            cmd.CommandType = CommandType.Text
            Dim result As Object = cmd.ExecuteScalar()
            If result Is Nothing OrElse IsDBNull(result) Then Return False
            Return Convert.ToBoolean(result)
        End Using
    End Function

    Private Shared Function ObtenerSiguienteIdMovimiento(ByVal pConnection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction) As Integer
        Const sql As String = "SELECT ISNULL(MAX(IdMovimiento),0)+1 FROM trans_movimientos WITH (UPDLOCK, HOLDLOCK)"

        Using cmd As New SqlCommand(sql, pConnection, pTransaction)
            cmd.CommandType = CommandType.Text
            Dim result As Object = cmd.ExecuteScalar()
            If result Is Nothing OrElse IsDBNull(result) Then Return 1
            Return Convert.ToInt32(result)
        End Using
    End Function

    Public Shared Sub Cargar(ByRef oBeTrans_movimientos As clsBeTrans_movimientos, ByRef dr As DataRow)

        Try

            With oBeTrans_movimientos

                .IdMovimiento = IIf(IsDBNull(dr.Item("IdMovimiento")), 0, dr.Item("IdMovimiento"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdUbicacionOrigen = IIf(IsDBNull(dr.Item("IdUbicacionOrigen")), 0, dr.Item("IdUbicacionOrigen"))
                .IdUbicacionDestino = IIf(IsDBNull(dr.Item("IdUbicacionDestino")), 0, dr.Item("IdUbicacionDestino"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdEstadoOrigen = IIf(IsDBNull(dr.Item("IdEstadoOrigen")), 0, dr.Item("IdEstadoOrigen"))
                .IdEstadoDestino = IIf(IsDBNull(dr.Item("IdEstadoDestino")), 0, dr.Item("IdEstadoDestino"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .IdBodegaDestino = IIf(IsDBNull(dr.Item("IdBodegaDestino")), 0, dr.Item("IdBodegaDestino"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Serie = IIf(IsDBNull(dr.Item("serie")), "", dr.Item("serie"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
                .Barra_pallet = IIf(IsDBNull(dr.Item("barra_pallet")), "", dr.Item("barra_pallet"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Fecha_agr = IIf(IsDBNull(dr.Item("fecha_agr")), Date.Now, dr.Item("fecha_agr"))
                .Usuario_agr = IIf(IsDBNull(dr.Item("usuario_agr")), "", dr.Item("usuario_agr"))
                .Cantidad_hist = IIf(IsDBNull(dr.Item("cantidad_hist")), 0.0, dr.Item("cantidad_hist"))
                .Peso_hist = IIf(IsDBNull(dr.Item("peso_hist")), 0.0, dr.Item("peso_hist"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("idoperadorbodega")), 0, dr.Item("idoperadorbodega"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .IdDespachoDet = IIf(IsDBNull(dr.Item("IdDespachoDet")), 0, dr.Item("IdDespachoDet"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), 0, dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), 0, dr.Item("Color"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_movimientos As clsBeTrans_movimientos,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand
        Dim esIdMovimientoIdentity As Boolean = False

        Try
            Dim sp As String = ""

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand("", pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand("", lConnection, lTransaction)
            End If

            esIdMovimientoIdentity = IdMovimientoEsIdentity(cmd.Connection, cmd.Transaction)

            Ins.Init("trans_movimientos")
            If Not esIdMovimientoIdentity Then
                Ins.Add("idmovimiento", "@idmovimiento", DataType.Parametro)
            End If
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Ins.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idubicacionorigen", "@idubicacionorigen", DataType.Parametro)
            Ins.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idestadoorigen", "@idestadoorigen", DataType.Parametro)
            Ins.Add("idestadodestino", "@idestadodestino", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Ins.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Ins.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("barra_pallet", "@barra_pallet", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Ins.Add("usuario_agr", "@usuario_agr", DataType.Parametro)
            Ins.Add("cantidad_hist", "@cantidad_hist", DataType.Parametro)
            Ins.Add("peso_hist", "@peso_hist", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("IdRecepcionDet", "@IdRecepcionDet", DataType.Parametro)
            Ins.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro)
            Ins.Add("IdPedidoDet", "@IdPedidoDet", DataType.Parametro)
            Ins.Add("IdDespachoEnc", "@IdDespachoEnc", DataType.Parametro)
            Ins.Add("IdDespachoDet", "@IdDespachoDet", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Ins.Add("Talla", "@Talla", DataType.Parametro)
            Ins.Add("Color", "@Color", DataType.Parametro)

            sp = Ins.SQL()
            If esIdMovimientoIdentity Then
                sp &= "; SELECT CAST(SCOPE_IDENTITY() AS INT);"
            End If
            cmd.CommandText = sp

            If Not esIdMovimientoIdentity Then
                If oBeTrans_movimientos.IdMovimiento <= 0 Then
                    oBeTrans_movimientos.IdMovimiento = ObtenerSiguienteIdMovimiento(cmd.Connection, cmd.Transaction)
                End If
                cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_movimientos.IdMovimiento))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_movimientos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_movimientos.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", IIf(oBeTrans_movimientos.IdPropietarioBodega = 0, DBNull.Value, oBeTrans_movimientos.IdPropietarioBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", IIf(oBeTrans_movimientos.IdProductoBodega = 0, DBNull.Value, oBeTrans_movimientos.IdProductoBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONORIGEN", IIf(oBeTrans_movimientos.IdUbicacionOrigen = 0, DBNull.Value, oBeTrans_movimientos.IdUbicacionOrigen)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", IIf(oBeTrans_movimientos.IdUbicacionDestino = 0, DBNull.Value, oBeTrans_movimientos.IdUbicacionDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_movimientos.IdPresentacion = 0, DBNull.Value, oBeTrans_movimientos.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", IIf(oBeTrans_movimientos.IdEstadoOrigen = 0, DBNull.Value, oBeTrans_movimientos.IdEstadoOrigen)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", IIf(oBeTrans_movimientos.IdEstadoDestino = 0, DBNull.Value, oBeTrans_movimientos.IdEstadoDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTrans_movimientos.IdUnidadMedida = 0, DBNull.Value, oBeTrans_movimientos.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", IIf(oBeTrans_movimientos.IdTipoTarea = 0, DBNull.Value, oBeTrans_movimientos.IdTipoTarea)))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", IIf(oBeTrans_movimientos.IdBodegaDestino = 0, DBNull.Value, oBeTrans_movimientos.IdBodegaDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_movimientos.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_movimientos.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_movimientos.Serie))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_movimientos.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_movimientos.Lote = Nothing, "", oBeTrans_movimientos.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_movimientos.Fecha_vence = Nothing, DBNull.Value, oBeTrans_movimientos.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_movimientos.Fecha))
            cmd.Parameters.Add(New SqlParameter("@BARRA_PALLET", oBeTrans_movimientos.Barra_pallet))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_movimientos.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_movimientos.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeTrans_movimientos.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_AGR", oBeTrans_movimientos.Usuario_agr))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_HIST", oBeTrans_movimientos.Cantidad_hist))
            cmd.Parameters.Add(New SqlParameter("@PESO_HIST", oBeTrans_movimientos.Peso_hist))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_movimientos.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeTrans_movimientos.Lic_plate = Nothing, "", oBeTrans_movimientos.Lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_movimientos.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_movimientos.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_movimientos.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_movimientos.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_movimientos.IdDespachoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_movimientos.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_movimientos.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_movimientos.Color))

            Dim rowsAffected As Integer = 0

            If esIdMovimientoIdentity Then
                Dim idGeneradoObj As Object = cmd.ExecuteScalar()
                If idGeneradoObj IsNot Nothing AndAlso Not IsDBNull(idGeneradoObj) Then
                    oBeTrans_movimientos.IdMovimiento = Convert.ToInt32(idGeneradoObj)
                    rowsAffected = oBeTrans_movimientos.IdMovimiento
                End If
            Else
                rowsAffected = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    rowsAffected = oBeTrans_movimientos.IdMovimiento
                End If
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            '#GT21102022: aqui nunca falta el bug, mejor guardar lo que haya sido
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_LP_Por_Explosion(ByVal pIdMovimiento As Integer,
                                                       ByVal pLicPlate As String,
                                                       ByVal lConection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As Integer


        Try

            Upd.Init("trans_movimientos")
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Where("IdMovimiento = @IdMovimiento")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not lConection Is Nothing AndAlso Not lTransaction Is Nothing)

            Dim cmd As New SqlCommand(sp, lConection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", pIdMovimiento))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", pLicPlate))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            '#MECR25112025: Se agrego bitacora de logs para reabastecimiento
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_reab.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdMovimiento:=pIdMovimiento, pLic_Plate:=pLicPlate)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_movimientos As clsBeTrans_movimientos, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_movimientos")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Upd.Add("idtransaccion", "@idtransaccion", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idubicacionorigen", "@idubicacionorigen", DataType.Parametro)
            Upd.Add("idubicaciondestino", "@idubicaciondestino", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idestadoorigen", "@idestadoorigen", DataType.Parametro)
            Upd.Add("idestadodestino", "@idestadodestino", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Upd.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Upd.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("barra_pallet", "@barra_pallet", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Upd.Add("usuario_agr", "@usuario_agr", DataType.Parametro)
            Upd.Add("cantidad_hist", "@cantidad_hist", DataType.Parametro)
            Upd.Add("peso_hist", "@peso_hist", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("IdRecepcionDet", "@IdRecepcionDet", DataType.Parametro)
            Upd.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro)
            Upd.Add("IdPedidoDet", "@IdPedidoDet", DataType.Parametro)
            Upd.Add("IdDespachoEnc", "@IdDespachoEnc", DataType.Parametro)
            Upd.Add("IdDespachoDet", "@IdDespachoDet", DataType.Parametro)
            Upd.Add("Talla", "@Talla", DataType.Parametro)
            Upd.Add("Color", "@Color", DataType.Parametro)
            Upd.Where("IdMovimiento = @IdMovimiento " &
                "AND IdEmpresa = @IdEmpresa " &
                "AND IdBodegaOrigen = @IdBodegaOrigen " &
                "AND IdTransaccion = @IdTransaccion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_movimientos.IdMovimiento))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_movimientos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_movimientos.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", IIf(oBeTrans_movimientos.IdPropietarioBodega = 0, DBNull.Value, oBeTrans_movimientos.IdPropietarioBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", IIf(oBeTrans_movimientos.IdProductoBodega = 0, DBNull.Value, oBeTrans_movimientos.IdProductoBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONORIGEN", IIf(oBeTrans_movimientos.IdUbicacionOrigen = 0, DBNull.Value, oBeTrans_movimientos.IdUbicacionOrigen)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDESTINO", IIf(oBeTrans_movimientos.IdUbicacionDestino = 0, DBNull.Value, oBeTrans_movimientos.IdUbicacionDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_movimientos.IdPresentacion = 0, DBNull.Value, oBeTrans_movimientos.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOORIGEN", IIf(oBeTrans_movimientos.IdEstadoOrigen = 0, DBNull.Value, oBeTrans_movimientos.IdEstadoOrigen)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADODESTINO", IIf(oBeTrans_movimientos.IdEstadoDestino = 0, DBNull.Value, oBeTrans_movimientos.IdEstadoDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTrans_movimientos.IdUnidadMedida = 0, DBNull.Value, oBeTrans_movimientos.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", IIf(oBeTrans_movimientos.IdTipoTarea = 0, DBNull.Value, oBeTrans_movimientos.IdTipoTarea)))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", IIf(oBeTrans_movimientos.IdBodegaDestino = 0, DBNull.Value, oBeTrans_movimientos.IdBodegaDestino)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_movimientos.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_movimientos.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_movimientos.Serie))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_movimientos.Peso))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_movimientos.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_movimientos.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_movimientos.Fecha))
            cmd.Parameters.Add(New SqlParameter("@BARRA_PALLET", oBeTrans_movimientos.Barra_pallet))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_movimientos.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_movimientos.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeTrans_movimientos.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USUARIO_AGR", oBeTrans_movimientos.Usuario_agr))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_HIST", oBeTrans_movimientos.Cantidad_hist))
            cmd.Parameters.Add(New SqlParameter("@PESO_HIST", oBeTrans_movimientos.Peso_hist))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_movimientos.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_movimientos.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_movimientos.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_movimientos.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_movimientos.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHODET", oBeTrans_movimientos.IdDespachoDet))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_movimientos.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_movimientos.Color))

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

    Public Shared Function Eliminar(ByRef oBeTrans_movimientos As clsBeTrans_movimientos,
                                    ByVal pConection As SqlConnection,
                                    ByVal pTransaction As SqlTransaction) As Integer

        Try


            Dim sp As String = " Delete from Trans_movimientos
                                 Where(IdMovimiento = @IdMovimiento)
                                 AND (IdRecepcion=@IdRecepcion)
                                 AND (IdRecepcionDet=@IdRecepcionDet)
                                 AND (IdEmpresa = @IdEmpresa)  
                                 AND (IdBodegaOrigen = @IdBodegaOrigen)  
                                 AND (IdTransaccion = @IdTransaccion)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_movimientos.IdMovimiento))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_movimientos.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_movimientos.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_movimientos.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_movimientos.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_movimientos As clsBeTrans_movimientos) As Integer

        Try

            Dim sp As String = " Delete from Trans_movimientos
                                 Where(IdMovimiento = @IdMovimiento)
                                 AND (IdEmpresa = @IdEmpresa)  
                                 AND (IdBodegaOrigen = @IdBodegaOrigen)  
                                 AND (IdTransaccion = @IdTransaccion)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                    cmd.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_movimientos.IdMovimiento))
                    cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_movimientos.IdEmpresa))
                    cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_movimientos.IdBodegaOrigen))
                    cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    lTransaction.Commit()

                    Return rowsAffected

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_movimientos As clsBeTrans_movimientos) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_movimientos" &
            " Where(IdMovimiento = @IdMovimiento) " &
            "AND (IdEmpresa = @IdEmpresa) " &
            "AND (IdBodegaOrigen = @IdBodegaOrigen) " &
            "AND (IdTransaccion = @IdTransaccion)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOVIMIENTO", oBeTrans_movimientos.IdMovimiento))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTrans_movimientos.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeTrans_movimientos.IdBodegaOrigen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_movimientos, dt.Rows(0))
                Obtener = True
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

    Public Shared Function Eliminar_Recepcion_BOF(ByRef oBeTrans_movimientos As clsBeTrans_movimientos,
                                                  ByVal pConection As SqlConnection,
                                                  ByVal pTransaction As SqlTransaction) As Integer

        Try


            Dim sp As String = " Delete from Trans_movimientos
                                 Where (IdRecepcion=@IdRecepcion)
                                 AND (IdRecepcionDet=@IdRecepcionDet)
                                 AND (IdTransaccion = @IdTransaccion)
                                 AND IdTipoTarea=1 "

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_movimientos.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_movimientos.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCION", oBeTrans_movimientos.IdTransaccion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
