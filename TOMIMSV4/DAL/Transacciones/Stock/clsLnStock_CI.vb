Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_CI

    Public Shared Sub Cargar(ByRef oBeStock As clsBeStock, ByRef dr As DataRow)
        Try
            With oBeStock
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior")) '#CKFK 20180208 09:34 Agregué el campo IdUbicacion_anterior que no se estaba llenando
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("stock")
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("Idubicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
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
            'If Not oBeStock.Atributo_Variante_1 Is Nothing Then Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeStock.ProductoEstado.IdEstado = 0, DBNull.Value, oBeStock.ProductoEstado.IdEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock.Presentacion.IdPresentacion = 0, DBNull.Value, oBeStock.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeStock.IdUnidadMedida = 0, DBNull.Value, oBeStock.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", IIf(oBeStock.IdUbicacion_anterior = 0, DBNull.Value, oBeStock.IdUbicacion_anterior)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeStock.IdRecepcionEnc = 0, DBNull.Value, oBeStock.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeStock.IdRecepcionDet = 0, DBNull.Value, oBeStock.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IIf(oBeStock.IdPedidoEnc = 0, DBNull.Value, oBeStock.IdPedidoEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IIf(oBeStock.IdPickingEnc = 0, DBNull.Value, oBeStock.IdPickingEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IIf(oBeStock.IdDespachoEnc = 0, DBNull.Value, oBeStock.IdDespachoEnc)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock.Lote = Nothing, "", oBeStock.Lote))) '#CKFK 20181011 0311PM Se quitó el DBNull.Value por ""
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeStock.Lic_plate = Nothing, DBNull.Value, oBeStock.Lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeStock.Serial = Nothing, DBNull.Value, oBeStock.Serial)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock.Cantidad = 0, DBNull.Value, oBeStock.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeStock.Fecha_Ingreso = Nothing, DBNull.Value, oBeStock.Fecha_Ingreso)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock.Fecha_vence = Nothing, DBNull.Value, oBeStock.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", IIf(oBeStock.Uds_lic_plate = Nothing, DBNull.Value, oBeStock.Uds_lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", IIf(oBeStock.No_bulto = 0, DBNull.Value, oBeStock.No_bulto)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", IIf(oBeStock.Fecha_Manufactura = Nothing, DBNull.Value, oBeStock.Fecha_Manufactura)))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock.Temperatura))
            'If Not oBeStock.Atributo_Variante_1 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock.Atributo_Variante_1))


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

    Public Shared Function Actualizar(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("IdUbicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
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
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

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

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeStock.ProductoEstado.IdEstado = 0, DBNull.Value, oBeStock.ProductoEstado.IdEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock.Presentacion.IdPresentacion = 0, DBNull.Value, oBeStock.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeStock.IdUnidadMedida = 0, DBNull.Value, oBeStock.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion_anterior", IIf(oBeStock.IdUbicacion_anterior = 0, DBNull.Value, oBeStock.IdUbicacion_anterior)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeStock.IdRecepcionEnc = 0, DBNull.Value, oBeStock.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeStock.IdRecepcionDet = 0, DBNull.Value, oBeStock.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IIf(oBeStock.IdPedidoEnc = 0, DBNull.Value, oBeStock.IdPedidoEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IIf(oBeStock.IdPickingEnc = 0, DBNull.Value, oBeStock.IdPickingEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IIf(oBeStock.IdDespachoEnc = 0, DBNull.Value, oBeStock.IdDespachoEnc)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock.Lote = Nothing, "", oBeStock.Lote)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeStock.Lic_plate = Nothing, DBNull.Value, oBeStock.Lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeStock.Serial = Nothing, DBNull.Value, oBeStock.Serial)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock.Cantidad = 0, DBNull.Value, oBeStock.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeStock.Fecha_Ingreso = Nothing, DBNull.Value, oBeStock.Fecha_Ingreso)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock.Fecha_vence = Nothing, DBNull.Value, oBeStock.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", IIf(oBeStock.Uds_lic_plate = Nothing, DBNull.Value, oBeStock.Uds_lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", IIf(oBeStock.No_bulto = 0, DBNull.Value, oBeStock.No_bulto)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", IIf(oBeStock.Fecha_Manufactura = Nothing, DBNull.Value, oBeStock.Fecha_Manufactura)))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock.Atributo_Variante_1))


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

    Public Shared Function Eliminar_By_IdProductoBodega(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock" &
             "  Where(IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))


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

    Public Shared Function Eliminar(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock" &
             "  Where(IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))


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

    Public Shared Function Eliminar_By_IdStock(ByVal IdStock As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock Where(IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", IdStock))


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

    Public Shared Function Eliminar_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal IdRecepcionEnc As Integer,
                                                                         ByVal IdRecepcionDet As Integer,
                                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock Where(IdRecepcionEnc = @IdRecepcionEnc AND IdRecepcionDet = @IdRecepcionDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IdRecepcionDet))


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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock"
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

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeStock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeStock)
            Const sp As String = "SELECT * FROM Stock "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStock As New clsBeStock

            For Each dr As DataRow In dt.Rows

                vBeStock = New clsBeStock
                Cargar(vBeStock, dr)
                lReturnList.Add(vBeStock)

            Next

            lTransaction.Commit()

            cmd.Dispose()

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

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                ByRef lTransaction As SqlTransaction) As List(Of clsBeStock)

        Try

            Dim lReturnList As New List(Of clsBeStock)
            Const sp As String = "SELECT * FROM Stock"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStock As New clsBeStock

            For Each dr As DataRow In dt.Rows

                vBeStock = New clsBeStock
                Cargar(vBeStock, dr)
                lReturnList.Add(vBeStock)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeStock As clsBeStock) As clsBeStock

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Stock" &
            " Where(IdStock = @IdStock)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pBeStock.IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock, dt.Rows(0))
                GetSingle = pBeStock
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStock),0) FROM Stock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If
                    End Using
                    lTransaction.Commit()
                End Using
                lConnection.Close()
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_LP(ByVal pLicPlate As String,
                                         ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res_CI)

        Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

        Try

            'JP20171027 - filtro por ubicacion y producto
            Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 = 1 "
            If pLicPlate <> "" Then vSQL &= " And lic_plate = @pLicPlate "
            If pIdBodega <> 0 Then vSQL &= " And IdBodega = @pIdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        If pLicPlate <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res_CI

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res_CI

                                clsLnVW_stock_res.Cargar_CI(Obj, lRow)

                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)

                                lReturnList.Add(Obj)

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

    '#CKFK20220518 Modifiqué la consulta para las existencias en la HH
    '#AT24032023 cambio de anderly
    Public Shared Function Get_All_By_LP_And_IdUbicacion(ByVal pIdUbicacion As String,
                                                     ByVal pLicPlate As String,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pNombre As String,
                                                     ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

        Try


            Dim vSQL As String


            If pDetallado Then

                '#AT20221221 Agregue IdUbicacion Stock_CI
                vSQL = "SELECT Codigo,
						   Nombre,
						   UM = UnidadMedida,
						   Estado = NomEstado,
						   IdPresentacion,
						   ExistUMBAs = SUM(ISNULL(CantidadSF,0)),
						   ExistPres = SUM(isnull(Cantidad_Presentacion,0)),
						   Pres = Presentacion,
						   ReservadoUMBAs = SUM(isnull(CantidadReservada,0)), 
						   ResPres = SUM(ISNULL(Cantidad_Reservada_Pres,0)),							      
						   DisponibleUMBas = SUM(ISNULL(Disponible_UMBas,0)), 
						   DispPres = SUM(ISNULL(Disponible_Presentacion,0)),
						   SUM(ISNULL(Peso,0)) Peso, 
						   Lote, 
						   LicPlate = lic_plate, 
						   Ingreso = CONVERT(DATE, Fecha_Ingreso),
						   Vence = Fecha_Vence, 
						   Ubic = Nombre_Completo,
						   codigo_poliza, 
						   Numero_poliza as numero_orden, 
						   ubicacion_picking, 
						   NombreArea = Area, 
						   Factor, 
						   IdTipoEtiqueta,  
						   Clasificacion,
						   IdProductoBodega, 
						   idUbic=IdUbicacion,
                           IdArea
					FROM VW_Stock_Res WHERE 1 > 0 "

                If pIdUbicacion <> 0 Then vSQL &= "And IdUbicacionActual = @IdUbicacion "
                If pLicPlate <> "0" Then vSQL &= "And (lic_plate = @pLicPlate) "
                If pIdBodega <> 0 Then vSQL &= "And IdBodega = @pIdBodega "

                '#AT20220404 Buscar por nombre de producto
                If pNombre <> "" Then
                    vSQL &= " And (nombre like '%" + pNombre + "%'"
                    vSQL &= " Or Lote like '%" + pNombre + "%')"
                End If

                vSQL += " Group by NomEstado, CONVERT(DATE, Fecha_Ingreso), Codigo,
					  Nombre, Presentacion, IdPresentacion, UnidadMedida, Peso, 
					  Lote, fecha_vence, Nombre_Completo, lic_plate,
					  Factor, codigo_poliza, Numero_poliza, CantidadReservada, Cantidad,
					  CantidadSF, ubicacion_picking, Area, Factor,IdTipoEtiqueta, Clasificacion, IdProductoBodega, IdUbicacion, IdArea "

                vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Else
                '#AT20221221 Agregue IdUbicacion Stock_CI
                '#AT20230320 Agregue Convert(Date, Fecha_Ingreso) al group by
                '#AT20230322 Sum de peso 
                vSQL = "SELECT Codigo,
						   Nombre,
						   UM = UnidadMedida,
						   Estado = NomEstado,
						   IdPresentacion,
						   ExistUMBAs = SUM(ISNULL(CantidadSF,0)),
						   ExistPres = SUM(isnull(Cantidad_Presentacion,0)),
						   Pres = Presentacion,
						   ReservadoUMBAs = SUM(isnull(CantidadReservada,0)), 
						   ResPres = SUM(ISNULL(Cantidad_Reservada_Pres,0)),						      
						   DisponibleUMBas = ROUND( SUM(isnull(CantidadSF,0)) -  SUM(isnull(CantidadReservada,0)),6), 
						   DispPres = SUM(ISNULL(Disponible_Presentacion,0)),
						   SUM(ISNULL(Peso,0)) Peso, 
						   Lote, 
						   LicPlate = lic_plate, 
						   Ingreso = Convert(Date, Fecha_Ingreso),
						   Vence = Fecha_Vence, 
						   Ubic = Nombre_Completo,
						   codigo_poliza, 
						   Numero_poliza as numero_orden, 
						   ubicacion_picking, 
						   NombreArea = Area, 
						   Factor, 
						   IdTipoEtiqueta,  
						   Clasificacion, 
						   IdProductoBodega,
						   idUbic=IdUbicacion, 
                           IdArea
					FROM VW_Stock_Res WHERE 1 > 0 "

                If pIdUbicacion <> 0 Then vSQL &= "And IdUbicacionActual = @IdUbicacion "
                If pLicPlate <> "0" Then vSQL &= "And  (lic_plate = @pLicPlate) "
                If pIdBodega <> 0 Then vSQL &= "And IdBodega = @pIdBodega "

                '#AT20220404 Buscar por nombre de producto
                If pNombre <> "" Then
                    vSQL &= " And (nombre like '%" + pNombre + "%'"
                    vSQL &= " Or Lote like '%" + pNombre + "%')"
                End If

                '#CKFK20221024 Aqui vamos a quitar el Group by por las cantidades  CantidadReservada, Cantidad,  CantidadSF,
                vSQL += " Group by NomEstado, Codigo, Convert(Date, Fecha_Ingreso),
					  Nombre, Presentacion, IdPresentacion, UnidadMedida, 
					  Lote, fecha_vence, Nombre_Completo, lic_plate,
					  Factor, codigo_poliza, Numero_poliza,
					  ubicacion_picking, Area, Factor,IdTipoEtiqueta, Clasificacion, IdProductoBodega, IdUbicacion, IdArea "

                vSQL += "ORDER BY Codigo, Nombre_Completo "

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> "0" Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pLicPlate <> "0" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res_CI

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res_CI

                                clsLnVW_stock_res.Cargar_CI(Obj, lRow)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                Return lReturnList

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer,
    '                                          ByVal pIdProducto As String,
    '                                          ByVal pIdBodega As Integer,
    '                                          ByVal pNombre As String,
    '                                          ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

    '    Dim result As New List(Of clsBeVW_stock_res_CI)
    '    Dim query As New Text.StringBuilder()

    '    Try

    '        If pDetallado Then
    '            query.AppendLine("SELECT IdStock, Codigo, Nombre, UnidadMedida AS UM, NomEstado AS Estado, IdPresentacion,")
    '            query.AppendLine("SUM(ISNULL(CantidadSF,0)) AS ExistUMBAs,")
    '            query.AppendLine("SUM(ISNULL(Cantidad_Presentacion,0)) AS ExistPres,")
    '            query.AppendLine("Presentacion AS Pres,")
    '            query.AppendLine("SUM(ISNULL(CantidadReservada,0)) AS ReservadoUMBAs,")
    '            query.AppendLine("SUM(ISNULL(Cantidad_Reservada_Pres,0)) AS ResPres,")
    '            query.AppendLine("SUM(ISNULL(Disponible_UMBas,0)) AS DisponibleUMBas,")
    '            query.AppendLine("SUM(ISNULL(Disponible_Presentacion,0)) AS DispPres,")
    '            query.AppendLine("SUM(ISNULL(Peso,0)) AS Peso, Lote, lic_plate AS LicPlate,")
    '            query.AppendLine("CONVERT(DATE, Fecha_Ingreso) AS ingreso, Fecha_Vence AS Vence, Nombre_Completo AS Ubic,")
    '            query.AppendLine("IdUbicacion AS idUbic, '0' AS Pedido, '0' AS Pick, codigo_poliza, Numero_poliza AS numero_orden,")
    '            query.AppendLine("ubicacion_picking, Area AS NombreArea, Factor, IdTipoEtiqueta, Clasificacion,")
    '            query.AppendLine("IdProductoBodega, IdArea")
    '            query.AppendLine("FROM VW_Stock_Res")
    '            query.AppendLine("WHERE 1 = 1")

    '        Else
    '            query.AppendLine("SELECT IdStock, Codigo, Nombre, UnidadMedida AS UM, NomEstado AS Estado, IdPresentacion,")
    '            query.AppendLine("SUM(ISNULL(CantidadSF,0)) AS ExistUMBAs,")
    '            query.AppendLine("SUM(ISNULL(Cantidad_Presentacion,0)) AS ExistPres,")
    '            query.AppendLine("Presentacion AS Pres,")
    '            query.AppendLine("SUM(ISNULL(CantidadReservada,0)) AS ReservadoUMBAs,")
    '            query.AppendLine("SUM(ISNULL(Cantidad_Reservada_Pres,0)) AS ResPres,")
    '            query.AppendLine("ROUND(SUM(ISNULL(CantidadSF,0)) - SUM(ISNULL(CantidadReservada,0)), 6) AS DisponibleUMBas,")
    '            query.AppendLine("SUM(ISNULL(Disponible_Presentacion,0)) AS DispPres,")
    '            query.AppendLine("SUM(ISNULL(Peso,0)) AS Peso, Lote, lic_plate AS LicPlate,")
    '            query.AppendLine("CONVERT(DATE, Fecha_Ingreso) AS ingreso, Fecha_Vence AS Vence, Nombre_Completo AS Ubic,")
    '            query.AppendLine("IdUbicacion AS idUbic, '0' AS Pedido, '0' AS Pick, codigo_poliza, Numero_poliza AS numero_orden,")
    '            query.AppendLine("ubicacion_picking, Area AS NombreArea, Factor, IdTipoEtiqueta, Clasificacion,")
    '            query.AppendLine("IdProductoBodega, IdArea")
    '            query.AppendLine("FROM VW_Stock_Res")
    '            query.AppendLine("WHERE 1 = 1")
    '        End If

    '        If pIdUbicacion <> 0 Then query.AppendLine("AND IdUbicacionActual = @IdUbicacion")
    '        If Not String.IsNullOrWhiteSpace(pIdProducto) Then
    '            query.AppendLine("AND IdProducto IN (")
    '            query.AppendLine("    SELECT TOP 1 IdProducto FROM (")
    '            query.AppendLine("        SELECT IdProducto FROM VW_Stock_Res WHERE Codigo = @pIdProducto")
    '            query.AppendLine("        UNION")
    '            query.AppendLine("        SELECT IdProducto FROM VW_Stock_Res WHERE codigo_barra = @pIdProducto")
    '            query.AppendLine("        UNION")
    '            query.AppendLine("        SELECT IdProducto FROM VW_Stock_Res WHERE lic_plate = @pIdProducto")
    '            query.AppendLine("        UNION")
    '            query.AppendLine("        SELECT IdProducto FROM producto_presentacion WHERE codigo_barra = @pIdProducto")
    '            query.AppendLine("    ) AS Productos")
    '            query.AppendLine(")")
    '        End If
    '        If pIdBodega <> 0 Then query.AppendLine("AND IdBodega = @pIdBodega")
    '        If Not String.IsNullOrWhiteSpace(pNombre) Then
    '            query.AppendLine("AND (nombre LIKE '%' + @pNombre + '%' OR Lote LIKE '%' + @pNombre + '%')")
    '        End If

    '        query.AppendLine("GROUP BY IdStock, Codigo, Nombre, Presentacion, IdPresentacion, UnidadMedida, NomEstado,")
    '        query.AppendLine("Lote, Fecha_Vence, CONVERT(DATE, Fecha_Ingreso), Nombre_Completo, lic_plate,")
    '        query.AppendLine("codigo_poliza, Numero_poliza, ubicacion_picking, Area, Factor, IdTipoEtiqueta, Clasificacion,")
    '        query.AppendLine("IdUbicacion, IdProductoBodega, IdArea")
    '        query.AppendLine("ORDER BY Codigo, Nombre_Completo")

    '        Using conn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '            conn.Open()
    '            Using cmd As New SqlCommand(query.ToString(), conn)
    '                cmd.CommandType = CommandType.Text
    '                If pIdUbicacion <> 0 Then cmd.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
    '                If Not String.IsNullOrWhiteSpace(pIdProducto) Then cmd.Parameters.AddWithValue("@pIdProducto", pIdProducto)
    '                If pIdBodega <> 0 Then cmd.Parameters.AddWithValue("@pIdBodega", pIdBodega)
    '                If Not String.IsNullOrWhiteSpace(pNombre) Then cmd.Parameters.AddWithValue("@pNombre", pNombre)

    '                Dim table As New DataTable()
    '                Using adapter As New SqlDataAdapter(cmd)
    '                    adapter.Fill(table)
    '                End Using

    '                For Each row As DataRow In table.Rows
    '                    Dim obj As New clsBeVW_stock_res_CI()
    '                    clsLnVW_stock_res.Cargar_CI(obj, row)
    '                    result.Add(obj)
    '                Next
    '            End Using
    '        End Using

    '        Return result

    '    Catch ex As Exception
    '        Throw
    '    End Try

    'End Function

    '#AT24032023 cambio de anderly
    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                              ByVal pIdProducto As String,
                                              ByVal pIdBodega As Integer,
                                              ByVal pNombre As String,
                                              ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

        Get_All_By_IdUbicacion = Nothing

        Try

            Dim vSQL As String

            If pDetallado Then
                '#AT20240805 Agregue el campo IdStock para la consultada detallada de existencias
                vSQL = "SELECT Codigo,
						   Nombre,
						   UM = UnidadMedida,
						   Estado = NomEstado,
						   IdPresentacion,
						   ExistUMBAs = SUM(ISNULL(CantidadSF,0)),
						   ExistPres = SUM(isnull(Cantidad_Presentacion,0)),
						   Pres = Presentacion,
						   ReservadoUMBAs = SUM(isnull(CantidadReservada,0)), 
						   ResPres = SUM(ISNULL(Cantidad_Reservada_Pres,0)),						      
						   DisponibleUMBas = SUM(ISNULL(Disponible_UMBas,0)),
						   DispPres = SUM(ISNULL(Disponible_Presentacion,0)),
						   SUM(ISNULL(Peso,0)) Peso, 
						   Lote, 
						   LicPlate = lic_plate, 
						   ingreso  = Convert(DATE,Fecha_Ingreso),
						   Vence = Fecha_Vence, 
						   Ubic = Nombre_Completo,
						   idUbic=IdUbicacion,
						   Pedido = '0',
						   Pick = '0',
						   codigo_poliza, 
						   Numero_poliza as numero_orden, 
						   ubicacion_picking, 
						   NombreArea = Area, 
						   Factor, 
						   IdTipoEtiqueta,  
						   Clasificacion,
						   IdProductoBodega,
                           IdArea,
                           IdStock,
                           Nombre_Talla,
                           Codigo_Talla,
                           Nombre_Color,
                           Codigo_Color,
                           CodigoSKU
					FROM VW_Stock_Res LEFT JOIN 
                        producto_codigos_barra pc ON VW_Stock_Res.IdProducto = pc.idproducto AND pc.codigo_barra = @pIdProducto WHERE 1 > 0 "

                If pIdUbicacion <> 0 Then vSQL &= " And IdUbicacionActual = @IdUbicacion "
                If pIdProducto <> "" Then vSQL &= " And  (VW_Stock_Res.Codigo = @pIdProducto Or VW_Stock_Res.codigo_barra = @pIdProducto Or 
                                                          VW_Stock_Res.lic_plate = @pIdProducto 
												OR VW_Stock_Res.IdProducto  IN (SELECT IdProducto 
																   FROM producto_presentacion 
																   WHERE codigo_barra=@pIdProducto)) "
                If pIdBodega <> 0 Then vSQL &= " And IdBodega = @pIdBodega "

                '#AT20220404 Buscar por nombre de producto
                If pNombre <> "" Then
                    vSQL &= " And (nombre like '%" + pNombre + "%'"
                    vSQL &= " Or Lote like '%" + pNombre + "%')"
                End If

                vSQL += " Group by NomEstado, Convert(DATE,Fecha_Ingreso), Codigo,
					  Nombre, Presentacion, IdPresentacion, UnidadMedida, Peso, 
					  Lote, fecha_vence, Nombre_Completo, lic_plate,
					  Factor, codigo_poliza, Numero_poliza, CantidadReservada, Cantidad,
					  CantidadSF, ubicacion_picking, Area, Factor,IdTipoEtiqueta, Clasificacion, IdUbicacion, IdProductoBodega,
                      IdRecepcionDet, IdRecepcionEnc, IdArea, IdStock, Nombre_Talla, Codigo_Talla, Nombre_Color, Codigo_Color, CodigoSKU "

                vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Else
                '#AT20230320 Agregue Convert(Date, Fecha_Ingreso) al group by
                '#AT20232203 Sum para peso
                vSQL = "SELECT Codigo,
						   Nombre,
						   UM = UnidadMedida,
						   Estado = NomEstado,
						   IdPresentacion,
						   ExistUMBAs = SUM(ISNULL(CantidadSF,0)),
						   ExistPres = SUM(isnull(Cantidad_Presentacion,0)),
						   Pres = Presentacion,
						   ReservadoUMBAs = SUM(isnull(CantidadReservada,0)), 
						   ResPres = SUM(ISNULL(Cantidad_Reservada_Pres,0)),						      
						   DisponibleUMBas = ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6), 
						   DispPres = SUM(ISNULL(Disponible_Presentacion,0)),
						   SUM(ISNULL(Peso,0)) Peso, 
						   Lote, 
						   LicPlate = lic_plate, 
						   ingreso = Convert(Date, Fecha_Ingreso),
						   Vence = Fecha_Vence, 
						   Ubic = Nombre_Completo,
						   idUbic = IdUbicacion,
						   Pedido = '0',
						   Pick = '0',
						   codigo_poliza, 
						   Numero_poliza numero_orden, 
						   ubicacion_picking, 
						   NombreArea = Area, 
						   Factor, 
						   IdTipoEtiqueta,  
						   Clasificacion, 
						   IdProductoBodega,
                           IdArea,
                           Nombre_Talla,
                           Codigo_Talla,
                           Nombre_Color,
                           Codigo_Color,
                           CodigoSKU
					FROM VW_Stock_Res LEFT JOIN 
                         producto_codigos_barra pc ON VW_Stock_Res.IdProducto = pc.idproducto and pc.codigo_barra = @pIdProducto
                    WHERE 1 > 0 "


                If pIdUbicacion <> 0 Then vSQL &= " And IdUbicacionActual = @IdUbicacion "
                If pIdProducto.Trim <> "" Then vSQL &= " And  (VW_Stock_Res.Codigo = @pIdProducto Or 
                                                               VW_Stock_Res.codigo_barra = @pIdProducto Or
													           VW_Stock_Res.lic_plate = @pIdProducto OR 
                                                               VW_Stock_Res.IdProducto  IN (SELECT IdProducto 
																								   FROM producto_presentacion 
																								   WHERE codigo_barra=@pIdProducto)) "
                If pIdBodega <> 0 Then vSQL &= " And IdBodega = @pIdBodega "


                '#AT20220404 Buscar por nombre de producto
                If pNombre.Trim <> "" Then
                    vSQL &= " And (nombre like '%" + pNombre + "%'"
                    vSQL &= " OR Lote like '%" + pNombre + "%')"
                End If

                '#CKFK20221024 Vamos a quitar el Group by por cantidades CantidadSF, CantidadReservada, Cantidad
                vSQL += " Group by NomEstado, Codigo, Convert(Date, Fecha_Ingreso),
					  Nombre, Presentacion, IdPresentacion, UnidadMedida, 
					  Lote, fecha_vence, Nombre_Completo, lic_plate,
					  Factor, codigo_poliza, Numero_poliza,
					  ubicacion_picking, Area, Factor,IdTipoEtiqueta, Clasificacion, IdUbicacion, IdProductoBodega, 
                      IdArea, Nombre_Talla, Codigo_Talla, Nombre_Color, Codigo_Color, CodigoSKU "

                vSQL += "ORDER BY Codigo, Nombre_Completo "

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> "0" Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeVWStockResCI As clsBeVW_stock_res_CI

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeVWStockResCI = New clsBeVW_stock_res_CI

                                clsLnVW_stock_res.Cargar_CI(BeVWStockResCI, lRow)

                                lReturnList.Add(BeVWStockResCI)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                Get_All_By_IdUbicacion = lReturnList

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_LP_And_IdUbicacion_Original(ByVal pIdUbicacion As String,
                                                                  ByVal pLicPlate As String,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal pNombre As String,
                                                                  ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

        Try


            Dim vSQL As String

            If pDetallado Then

                vSQL = "select codigo,
                               nombre,
                               um= UnidadMedida,
                               ExistUMBAs= SUM(isnull(Cantidad_UMBas,0)), 
							   ExistPres= SUM(isnull(Cantidad_UMBas/factor,0)), 
                               Pres = Presentacion,
                               DispPres= Disponible_UMBas/factor,
							   ResPres= CantidadReservadaUmBas/factor,
                               DisponibleUMBas=Disponible_UMBas,
                               ReservadoUMBAs=CantidadReservadaUmBas,
                               lote,
                               Vence= CONVERT(Date, Vence),
                               Estado,
                               Ubic=UbicacionCompleta,
                               idUbic=IdUbicacion,
                               Pedido = isnull(IdPedido,'0'),
                               Pick = isnull(IdPicking,'0'),
                               LicPlate=isnull(lic_plate,'0'),
                               IdProductoEstado,
                               IdProductoBodega,
                               factor,
                               CONVERT(Date, ingreso) as ingreso,
                               IdTipoEtiqueta, 
                               NombreArea,
                               Clasificacion
	                         from VW_Stock_Por_Producto_Ubicacion_CI WHERE 1 = 1 "

                If pIdUbicacion <> 0 Then vSQL &= "And IdUbicacionActual = @IdUbicacion "
                If pLicPlate <> "0" Then vSQL &= "And  (lic_plate = @pLicPlate) "
                If pIdBodega <> 0 Then vSQL &= "And IdBodega = @pIdBodega "

                '#AT20220404 Buscar por nombre de producto
                If pNombre <> "" Then
                    vSQL &= " And nombre like '%" + pNombre + "%'"
                End If

                vSQL &= "  group by codigo,nombre,UnidadMedida, Cantidad_UMBas,Presentacion,
                                           Disponible_UMBas,factor,
	                                       CantidadReservadaUmBas,Disponible_UMBas,lote,CONVERT(Date, Vence),
                                           Estado,UbicacionCompleta,IdUbicacion,
	                                       lic_plate,IdProductoEstado,IdProductoBodega,
                                           IdPedido,IdPicking,CONVERT(Date, ingreso), IdTipoEtiqueta, NombreArea, Clasificacion"
            Else
                vSQL = "select codigo,
                                       nombre,
	                                   um= UnidadMedida,
	                                   ExistUMBAs= isnull(Cantidad_UMBas,0), 
                                       ExistPres= isnull(Cantidad_UMBas/factor,0), 
	                                   Pres = Presentacion,
	                                   DispPres= SUM(Cantidad_UMBas/factor) -SUM(CantidadReservadaUmBas/factor) ,
                                       ResPres= SUM(CantidadReservadaUmBas/factor),
                                       DisponibleUMBas=SUM(Cantidad_UMBas) -SUM(CantidadReservadaUmBas),
	                                   ReservadoUMBAs=SUM(CantidadReservadaUmBas),
	                                   lote,
	                                   Vence= CONVERT(Date, Vence),
	                                   Estado,
	                                   Ubic=UbicacionCompleta,idUbic=IdUbicacion,
	                                   Pedido = 0,
	                                   Pick = 0,
	                                   LicPlate=isnull(lic_plate,'0'),
	                                   IdProductoEstado,IdProductoBodega,factor,
	                                   CONVERT(Date, '19000101') as ingreso,
	                                   IdTipoEtiqueta, 
                                       NombreArea,
                                       Clasificacion
                                from VW_Stock_Por_Producto_Ubicacion_CI WHERE 1=1 "


                If pIdUbicacion <> 0 Then vSQL &= "And IdUbicacionActual = @IdUbicacion "
                If pLicPlate <> "0" Then vSQL &= "And  (lic_plate = @pLicPlate) "
                If pIdBodega <> 0 Then vSQL &= "And IdBodega = @pIdBodega "

                '#AT20220404 Buscar por nombre de producto
                If pNombre <> "" Then
                    vSQL &= " And nombre like '%" + pNombre + "%'"
                End If

                vSQL &= "group by codigo,nombre,UnidadMedida,
                                          Cantidad_UMBas,
                                          Presentacion,
                                          factor,
                                          lote,
                                          CONVERT(Date, Vence),
                                          Estado,UbicacionCompleta,IdUbicacion,
                                          lic_plate,
                                          IdProductoEstado,IdProductoBodega, IdTipoEtiqueta, NombreArea, Clasificacion"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> "0" Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pLicPlate <> "0" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res_CI

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res_CI

                                clsLnVW_stock_res.Cargar_CI(Obj, lRow)

                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion,
                                '                                                                        Obj.IdBodega,
                                '                                                                        lConnection,
                                '                                                                        lTransaction)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                Return lReturnList

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdUbicacion_Original(ByVal pIdUbicacion As Integer,
                                                           ByVal pIdProducto As String,
                                                           ByVal pIdBodega As Integer,
                                                           ByVal pNombre As String,
                                                           ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'GT 30112020 - nuevo query agrupado para que en android solo se asignen los campos a cada label
                    Dim vSQL As String

                    If pDetallado Then

                        vSQL = "select codigo,
                                       nombre,
                                       um= UnidadMedida,
                                       ExistUMBAs= SUM(isnull(Cantidad_UMBas,0)), 
									   ExistPres= SUM(isnull(Cantidad_UMBas/factor,0)), 
                                       Pres = Presentacion,
                                       DispPres= Disponible_UMBas/factor,
									   ResPres= CantidadReservadaUmBas/factor,
                                       DisponibleUMBas=Disponible_UMBas,
                                       ReservadoUMBAs=CantidadReservadaUmBas,
                                       lote,
                                       Vence= CONVERT(Date, Vence),
                                       Estado,
                                       Ubic=UbicacionCompleta,
                                       idUbic=IdUbicacion,
                                       Pedido = isnull(IdPedido,'0'),
                                       Pick = isnull(IdPicking,'0'),
                                       LicPlate=isnull(lic_plate,'0'),
                                       IdProductoEstado,
                                       IdProductoBodega,
                                       factor,
                                       CONVERT(Date, ingreso) as ingreso,
                                       IdTipoEtiqueta, 
                                       NombreArea,
                                       Clasificacion,
                                       Ingreso 
	                         from VW_Stock_Por_Producto_Ubicacion_CI WHERE 1 = 1 "

                        If pIdUbicacion <> 0 Then vSQL &= " And IdUbicacionActual = @IdUbicacion "
                        If pIdProducto <> "" Then vSQL &= " And  (Codigo = @pIdProducto Or barra = @pIdProducto Or lic_plate = @pIdProducto) "
                        If pIdBodega <> 0 Then vSQL &= " And IdBodega = @pIdBodega "

                        '#AT20220404 Buscar por nombre de producto
                        If pNombre <> "" Then
                            vSQL &= " And nombre like '%" + pNombre + "%'"
                        End If

                        vSQL &= "  group by codigo,nombre,UnidadMedida, Cantidad_UMBas,Presentacion,
                                           Disponible_UMBas,factor,
	                                       CantidadReservadaUmBas,Disponible_UMBas,lote,CONVERT(Date, Vence),
                                           Estado,UbicacionCompleta,IdUbicacion,
	                                       lic_plate,IdProductoEstado,IdProductoBodega,
                                           IdPedido,IdPicking,CONVERT(Date, ingreso), IdTipoEtiqueta,NombreArea,Clasificacion, Ingreso"
                    Else
                        vSQL = "select codigo,
                                       nombre,
	                                   um= UnidadMedida,
	                                   ExistUMBAs= isnull(Cantidad_UMBas,0), 
                                       ExistPres= isnull(Cantidad_UMBas/factor,0), 
	                                   Pres = Presentacion,
	                                   DispPres= SUM(Cantidad_UMBas/factor) - SUM(CantidadReservadaUmBas/factor) ,
                                       ResPres= SUM(CantidadReservadaUmBas/factor),
                                       DisponibleUMBas=SUM(Cantidad_UMBas) - SUM(CantidadReservadaUmBas),
	                                   ReservadoUMBAs=SUM(CantidadReservadaUmBas),
	                                   lote,
	                                   Vence= CONVERT(Date, Vence),
	                                   Estado,
	                                   Ubic=UbicacionCompleta,idUbic=IdUbicacion,
	                                   Pedido = 0,
	                                   Pick = 0,
	                                   LicPlate=isnull(lic_plate,'0'),
	                                   IdProductoEstado,IdProductoBodega,factor,
	                                   CONVERT(Date, '19000101') as ingreso,
	                                   IdTipoEtiqueta, 
                                       NombreArea,
                                       Clasificacion
                                from VW_Stock_Por_Producto_Ubicacion_CI WHERE 1=1 "


                        If pIdUbicacion <> 0 Then vSQL &= "And IdUbicacionActual = @IdUbicacion "
                        If pIdProducto <> "" Then vSQL &= "And  (Codigo = @pIdProducto Or barra = @pIdProducto Or lic_plate = @pIdProducto) "
                        If pIdBodega <> 0 Then vSQL &= "And IdBodega = @pIdBodega "

                        '#AT20220404 Buscar por nombre de producto
                        If pNombre <> "" Then
                            vSQL &= " And nombre like '%" + pNombre + "%'"
                        End If

                        vSQL &= "group by codigo,nombre,UnidadMedida,
                                          Cantidad_UMBas,
                                          Presentacion,
                                          factor,
                                          lote,
                                          CONVERT(Date, Vence),
                                          Estado,UbicacionCompleta,IdUbicacion,
                                          lic_plate,
                                          IdProductoEstado,IdProductoBodega, IdTipoEtiqueta,NombreArea,Clasificacion"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProducto <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res_CI

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res_CI

                                clsLnVW_stock_res.Cargar_CI(Obj, lRow)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                Return lReturnList

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
