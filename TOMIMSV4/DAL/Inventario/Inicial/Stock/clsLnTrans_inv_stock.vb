Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_stock

    Public Shared Sub Cargar(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, ByRef dr As DataRow)
        Try
            With oBeTrans_inv_stock
                .Idinventario = IIf(IsDBNull(dr.Item("idinventario")), 0, dr.Item("idinventario"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Nothing, dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0.0, dr.Item("uds_lic_plate"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), "", dr.Item("no_bulto"))
                .Fecha_manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                .fecha_copia = IIf(IsDBNull(dr.Item("fecha_copia")), Date.Now, dr.Item("fecha_copia"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Cantidad_Reservada_UMBas = IIf(IsDBNull(dr.Item("Cantidad_Reservada_UMBas")), 0, dr.Item("Cantidad_Reservada_UMBas"))

            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_stock")
            Ins.Add("idinventario", "@idinventario", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idubicacion_anterior", "@idubicacion_anterior", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("temperatura", "@temperatura", DataType.Parametro)
            Ins.Add("fecha_copia", "@fecha_copia", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_inv_stock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_stock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_stock.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_stock.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_stock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeTrans_inv_stock.IdUbicacion_anterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_inv_stock.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_inv_stock.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_inv_stock.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_inv_stock.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_inv_stock.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_stock.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_stock.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeTrans_inv_stock.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_stock.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeTrans_inv_stock.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeTrans_inv_stock.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeTrans_inv_stock.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeTrans_inv_stock.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeTrans_inv_stock.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_stock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_stock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_stock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_stock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_inv_stock.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_stock.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeTrans_inv_stock.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@FECHA_COPIA", oBeTrans_inv_stock.fecha_copia))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_stock.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_inv_stock.Idinventario = CInt(cmd.Parameters("@IDINVENTARIO").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_stock")
            Upd.Add("idinventario", "@idinventario", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idubicacion_anterior", "@idubicacion_anterior", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Upd.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("añada", "@añada", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("temperatura", "@temperatura", DataType.Parametro)
            Upd.Add("fecha_copia", "@fecha_copia", DataType.Parametro)
            Upd.Where("idinventario = @idinventario" &
                      " And IdStock = @IdStock And IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_inv_stock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_stock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_stock.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_stock.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_stock.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_stock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeTrans_inv_stock.IdUbicacion_anterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_inv_stock.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_inv_stock.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_inv_stock.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_inv_stock.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_inv_stock.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_stock.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_stock.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeTrans_inv_stock.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_stock.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeTrans_inv_stock.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeTrans_inv_stock.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeTrans_inv_stock.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeTrans_inv_stock.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeTrans_inv_stock.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_stock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_stock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_stock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_stock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_inv_stock.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_stock.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeTrans_inv_stock.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@FECHA_COPIA", oBeTrans_inv_stock.fecha_copia))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_stock.IdBodega))

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

    Public Shared Function Actualizar_Fecha_Vencimiento(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_stock")
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Where("idinventario = @idinventario" &
                " AND IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))


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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_stock" &
             "  Where(idinventario = @idinventario)" &
             "  AND (IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_stock"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)

            End If

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

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock" &
            " Where(idinventario = @idinventario)" &
            " AND (IdStock = @IdStock)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_stock, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)
            Const sp As String = "SELECT * FROM Trans_inv_stock"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByInv(ByVal IdInventario As Integer, ByVal IdProductoBodega As Integer, ByVal Lote As String) As List(Of clsBeTrans_inv_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * FROM Trans_inv_stock "

            vSQL += "Where idinventario=@idinventario And IdProductoBodega=@IdProductoBodega And lote=@lote "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@idinventario", IdInventario)
            dad.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
            dad.SelectCommand.Parameters.AddWithValue("@lote", Lote)

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByInv(ByVal IdInventario As Integer,
                                       ByVal IdProductoBodega As Integer,
                                       ByVal Lote As String,
                                       ByRef lConnection As SqlConnection,
                                       ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * FROM Trans_inv_stock "

            vSQL += "Where idinventario=@idinventario And IdProductoBodega=@IdProductoBodega And lote=@lote "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@idinventario", IdInventario)
            dad.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
            dad.SelectCommand.Parameters.AddWithValue("@lote", Lote)

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_stock As clsBeTrans_inv_stock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock" &
            " Where(idinventario = @idinventario)" &
            " AND (IdStock = @IdStock)"

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIO", pBeTrans_inv_stock.IDINVENTARIO))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pBeTrans_inv_stock.IDSTOCK))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_stock, dt.Rows(0))
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then
                lTransaction.Rollback()
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinventario),0) FROM Trans_inv_stock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
