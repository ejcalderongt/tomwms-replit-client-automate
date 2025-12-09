Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_hist

    Public Shared Sub Cargar(ByRef oBeStock_hist As clsBeStock_hist, ByRef dr As DataRow)

        Try

            With oBeStock_hist

                .IdStockHist = IIf(IsDBNull(dr.Item("IdStockHist")), 0, dr.Item("IdStockHist"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdNuevoStock = IIf(IsDBNull(dr.Item("IdNuevoStock")), 0, dr.Item("IdNuevoStock"))
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
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
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
                .Posiciones = IIf(IsDBNull(dr.Item("posiciones")), 0.0, dr.Item("posiciones"))
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

    Public Shared Function Insertar(ByRef oBeStock_hist As clsBeStock_hist, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("stock_hist")
            Ins.Add("idstockhist", "@idstockhist", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idnuevostock", "@idnuevostock", DataType.Parametro)
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
            Ins.Add("posiciones", "@posiciones", DataType.Parametro)
            Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKHIST", oBeStock_hist.IdStockHist))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_hist.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDNUEVOSTOCK", oBeStock_hist.IdNuevoStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_hist.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_hist.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeStock_hist.IdProductoEstado = 0, DBNull.Value, oBeStock_hist.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock_hist.IdPresentacion = 0, DBNull.Value, oBeStock_hist.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeStock_hist.IdUnidadMedida = 0, DBNull.Value, oBeStock_hist.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_hist.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", IIf(oBeStock_hist.IdUbicacion_anterior = 0, DBNull.Value, oBeStock_hist.IdUbicacion_anterior)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeStock_hist.IdRecepcionEnc = 0, DBNull.Value, oBeStock_hist.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeStock_hist.IdRecepcionDet = 0, DBNull.Value, oBeStock_hist.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IIf(oBeStock_hist.IdPedidoEnc = 0, DBNull.Value, oBeStock_hist.IdPedidoEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IIf(oBeStock_hist.IdPickingEnc = 0, DBNull.Value, oBeStock_hist.IdPickingEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IIf(oBeStock_hist.IdDespachoEnc = 0, DBNull.Value, oBeStock_hist.IdDespachoEnc)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock_hist.Lote = Nothing, DBNull.Value, oBeStock_hist.Lote)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeStock_hist.Lic_plate = Nothing, DBNull.Value, oBeStock_hist.Lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeStock_hist.Serial = Nothing, DBNull.Value, oBeStock_hist.Serial)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock_hist.Cantidad = 0, DBNull.Value, oBeStock_hist.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeStock_hist.Fecha_ingreso = Nothing, DBNull.Value, oBeStock_hist.Fecha_ingreso)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock_hist.Fecha_vence = Nothing, DBNull.Value, oBeStock_hist.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", IIf(oBeStock_hist.Uds_lic_plate = Nothing, DBNull.Value, oBeStock_hist.Uds_lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", IIf(oBeStock_hist.No_bulto = 0, DBNull.Value, oBeStock_hist.No_bulto)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", IIf(oBeStock_hist.Fecha_manufactura = Nothing, DBNull.Value, oBeStock_hist.Fecha_manufactura)))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_hist.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_hist.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_hist.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_hist.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_hist.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_hist.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_hist.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_hist.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeStock_hist.Posiciones))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_hist.IdProductoTallaColor))

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

    Public Shared Function Actualizar(ByRef oBeStock_hist As clsBeStock_hist, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_hist")
            Upd.Add("idstockhist", "@idstockhist", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idnuevostock", "@idnuevostock", DataType.Parametro)
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
            Upd.Add("posiciones", "@posiciones", DataType.Parametro)
            Upd.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            Upd.Where("IdStockHist = @IdStockHist")

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKHIST", oBeStock_hist.IdStockHist))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_hist.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDNUEVOSTOCK", oBeStock_hist.IdNuevoStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_hist.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_hist.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_hist.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_hist.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_hist.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_hist.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", oBeStock_hist.IdUbicacion_anterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_hist.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_hist.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_hist.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_hist.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_hist.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_hist.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_hist.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", oBeStock_hist.Serial))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_hist.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_hist.Fecha_ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_hist.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", oBeStock_hist.Uds_lic_plate))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_hist.No_bulto))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_hist.Fecha_manufactura))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_hist.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_hist.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_hist.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_hist.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_hist.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_hist.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_hist.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_hist.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@POSICIONES", oBeStock_hist.Posiciones))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_hist.IdProductoTallaColor))

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

    Public Shared Function Eliminar(ByRef oBeStock_hist As clsBeStock_hist, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_hist" &
             "  Where(IdStockHist = @IdStockHist)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKHIST", oBeStock_hist.IdStockHist))


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

            Const sp As String = " Delete from Stock_hist"
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Stock_hist"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
