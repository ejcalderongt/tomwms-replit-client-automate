Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock

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
                .Pallet_No_Estandar = IIf(IsDBNull(dr.Item("pallet_no_estandar")), False, dr.Item("pallet_no_estandar"))
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
    Public Shared Function Insertar(ByRef oBeStock As clsBeStock,
                                Optional ByVal pConection As SqlConnection = Nothing,
                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            Ins.Init("stock")
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
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
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)

            If oBeStock.IdProductoTallaColor > 0 Then
                Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            End If

            Dim sp As String = Ins.SQLIdentity("IdStock")

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            If oBeStock.ProductoEstado.IdEstado = 0 Then
                Throw New Exception("ERROR_202408250142: En la nueva versión de WMS no se permite el Idestado 0, revise el proceso por favor y notifique a desarrollo.")
            End If

            Bind(cmd, oBeStock)

            Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            oBeStock.IdStock = newId

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return newId

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then
                Try : lTransaction.Rollback() : Catch : End Try
            End If
            Throw
        Finally
            If cmd IsNot Nothing Then cmd.Dispose()
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
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
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Upd.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
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

            Bind(cmd, oBeStock)

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
    Public Shared Function Actualizar_Cantidad(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock.Cantidad = 0, DBNull.Value, oBeStock.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock.Peso))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock.Fec_agr))

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
    Public Shared Function Eliminar_By_IdStock(ByVal IdStock As Integer,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

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
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
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
    Public Shared Function GetSingle(ByRef pBeStock As clsBeStock, ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As clsBeStock

        GetSingle = Nothing

        Try

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

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Pallet_No_Standar(ByVal IdStock As Integer, pPallet_no_Standar As Boolean, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Update Stock" &
             " set pallet_no_estandar= @PALLET_NO_ESTANDAR  Where IdStock = @IdStock "

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
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", pPallet_no_Standar))


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
    Public Shared Function Get_All_By_NoLote(ByVal pCodigo As String, ByVal pLote As String) As List(Of clsBeStock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeStock)
            Const sp As String = "SELECT stock.*, producto.codigo AS Codigo
                                  FROM stock INNER JOIN
                                  producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
                                  AND stock.IdProductoBodega = producto_bodega.IdProductoBodega 
                                  INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto 
                                  WHERE producto.Codigo = @Codigo AND stock.lote = @Lote "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
            dad.SelectCommand.Parameters.AddWithValue("@Lote", pLote)

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
    Public Shared Function Get_Stock_By_LicensePlate_And_Codigo(ByVal pLicensePlate As String,
                                                                ByVal pCodigo As String,
                                                                ByVal IdBodega As Integer) As List(Of clsBeProducto)

        Get_Stock_By_LicensePlate_And_Codigo = Nothing

        Try

            Dim lObj As New List(Of clsBeProducto)
            Dim BeProducto As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Dim vSQL As String = "Select * from vw_stock_res 
                                  WHERE lic_plate=@lic_plate and codigo=@codigo 
                                  AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicensePlate)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(vStock, lRow, lConnection, lTransaction)
                                BeProducto = New clsBeProducto
                                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vStock.Codigo_Producto,
                                                                                    vStock.IdBodega,
                                                                                    lConnection,
                                                                                    lTransaction)

                                BeProducto.Stock = vStock
                                lObj.Add(BeProducto)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lObj

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Stock_Por_Ajuste(ByRef oBeStock As clsBeStock,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)

            If oBeStock.IdProductoTallaColor > 0 Then
                Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            End If

            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock.Lote = Nothing, "", oBeStock.Lote)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock.Cantidad = 0, DBNull.Value, oBeStock.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock.Fecha_vence = Nothing, DBNull.Value, oBeStock.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock.Peso))
            cmd.Parameters.Add(New SqlParameter("@user_mod", oBeStock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", oBeStock.Fec_mod))

            If oBeStock.IdProductoTallaColor > 0 Then
                cmd.Parameters.Add(New SqlParameter("@IdProductoTallaColor", oBeStock.IdProductoTallaColor))
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
    Public Shared Sub Bind(cmd As SqlCommand, oBeStock As clsBeStock)

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
        cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock.Pallet_No_Estandar))
        cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeStock.Atributo_Variante_1))
        '#GT08092025: aqui ya se valida que si no maneja talla color, el valor es null
        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", IIf(oBeStock.IdProductoTallaColor = 0, DBNull.Value, oBeStock.IdProductoTallaColor)))

    End Sub
    Public Shared Function Actualizar_Presentacion(ByRef oBeStock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock.IdPresentacion = 0, DBNull.Value, oBeStock.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))

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

    ''' <summary>
    ''' Obtiene un stock específico por producto bodega, ubicación y lote
    ''' </summary>
    Public Shared Function Get_Single_By_ProductoBodega_Ubicacion_Lote(
        idProductoBodega As Integer,
        idUbicacion As Integer,
        lote As String,
        idBodega As Integer) As clsBeStock

        Dim beStock As clsBeStock = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "
            SELECT TOP 1
                IdStock,
                IdBodega,
                IdPropietarioBodega,
                IdProductoBodega,
                IdProductoEstado,
                IdPresentacion,
                IdUnidadMedida,
                IdUbicacion,
                IdUbicacion_anterior,
                IdRecepcionEnc,
                IdPedido,
                IdPicking,
                lote,
                lic_plate,
                serial,
                CantidadSF AS Cantidad,
                CantidadReservada AS Cantidad_Reservada,
                factor,
                peso,
                fecha_ingreso,
                fecha_vence,
                añada,
                EstadoUtilizable,
                dañado AS Danado,
                activo,
                Pallet_No_Estandar,
                Codigo AS Codigo_Producto,
                Nombre AS Nombre_Producto,
                UnidadMedida AS UMBas,
                Presentacion AS Nombre_Presentacion,
                ubicacion_picking,
                Ubicacion_Nivel,
                Ubicacion_Nombre,
                Nombre_Completo,
                Alto AS AltoUbicacion,
                Largo AS LargoUbicacion,
                Ancho AS AnchoUbicacion,
                Alto_ubicacion,
                Largo_ubicacion,
                Ancho_ubicacion,
                NomEstado,
                CamasPorTarima,
                CajasPorCama,
                IdProductoTallaColor,
                Nombre_Talla,
                Nombre_Color,
                Atributo_variante_1,
                IdTramo,
                IdIndiceRotacion
            FROM vw_stock_res WITH (NOLOCK)
            WHERE IdProductoBodega = @IdProductoBodega
                AND IdUbicacion = @IdUbicacion
                AND Lote = @Lote
                AND IdBodega = @IdBodega
                AND activo = 1"

            Using cmd As New SqlCommand(vSQL, lConnection, lTransaction)
                cmd.Parameters.AddWithValue("@IdProductoBodega", idProductoBodega)
                cmd.Parameters.AddWithValue("@IdUbicacion", idUbicacion)
                cmd.Parameters.AddWithValue("@Lote", lote)
                cmd.Parameters.AddWithValue("@IdBodega", idBodega)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        beStock = New clsBeStock()

                        ' ─── IDs ─────────────────────────────────────────────────
                        beStock.IdStock = Convert.ToInt32(reader("IdStock"))
                        beStock.IdBodega = Convert.ToInt32(reader("IdBodega"))
                        beStock.IdPropietarioBodega = Convert.ToInt32(reader("IdPropietarioBodega"))
                        beStock.IdProductoBodega = Convert.ToInt32(reader("IdProductoBodega"))
                        beStock.IdProductoEstado = Convert.ToInt32(reader("IdProductoEstado"))

                        If reader("IdPresentacion") IsNot DBNull.Value Then
                            beStock.IdPresentacion = Convert.ToInt32(reader("IdPresentacion"))
                        End If

                        beStock.IdUnidadMedida = Convert.ToInt32(reader("IdUnidadMedida"))
                        beStock.IdUbicacion = Convert.ToInt32(reader("IdUbicacion"))

                        If reader("IdUbicacion_anterior") IsNot DBNull.Value Then
                            beStock.IdUbicacion_anterior = Convert.ToInt32(reader("IdUbicacion_anterior"))
                        End If

                        If reader("IdRecepcionEnc") IsNot DBNull.Value Then
                            beStock.IdRecepcionEnc = Convert.ToInt32(reader("IdRecepcionEnc"))
                        End If

                        If reader("IdPedido") IsNot DBNull.Value Then
                            beStock.IdPedidoEnc = Convert.ToInt32(reader("IdPedido"))
                        End If

                        If reader("IdPicking") IsNot DBNull.Value Then
                            beStock.IdPickingEnc = Convert.ToInt32(reader("IdPicking"))
                        End If

                        ' ─── Texto ──────────────────────────────────────────────
                        beStock.Lote = reader("lote").ToString()
                        beStock.Lic_plate = If(reader("lic_plate") IsNot DBNull.Value, reader("lic_plate").ToString(), "")
                        beStock.Serial = If(reader("serial") IsNot DBNull.Value, reader("serial").ToString(), "")
                        beStock.Atributo_Variante_1 = If(reader("Atributo_variante_1") IsNot DBNull.Value, reader("Atributo_variante_1").ToString(), "")

                        ' ─── Cantidades ─────────────────────────────────────────
                        beStock.Cantidad = Convert.ToDouble(reader("Cantidad"))
                        beStock.Cantidad_Reservada = Convert.ToDouble(reader("Cantidad_Reservada"))
                        beStock.Peso = Convert.ToDouble(reader("peso"))

                        ' ─── Fechas ────────────────────────────────────────────
                        If reader("fecha_ingreso") IsNot DBNull.Value Then
                            beStock.Fecha_Ingreso = Convert.ToDateTime(reader("fecha_ingreso"))
                        End If

                        If reader("fecha_vence") IsNot DBNull.Value Then
                            beStock.Fecha_vence = Convert.ToDateTime(reader("fecha_vence"))
                        End If

                        If reader("añada") IsNot DBNull.Value Then
                            beStock.Añada = Convert.ToInt32(reader("añada"))
                        End If

                        ' ─── Flags ─────────────────────────────────────────────
                        beStock.Activo = Convert.ToBoolean(reader("activo"))
                        beStock.Pallet_No_Estandar = Convert.ToBoolean(reader("Pallet_No_Estandar"))

                        ' ─── Segunda parte partial ─────────────────────────────
                        beStock.IsNew = False
                        beStock.ProductoValidado = True
                        beStock.UbicacionAnterior = If(reader("Ubicacion_Nombre") IsNot DBNull.Value, reader("Ubicacion_Nombre").ToString(), "")
                        beStock.UbicacionPicking = Convert.ToBoolean(reader("ubicacion_picking"))
                        beStock.UbicacionNivel = Convert.ToInt32(reader("Ubicacion_Nivel"))
                        beStock.Talla = If(reader("Nombre_Talla") IsNot DBNull.Value, reader("Nombre_Talla").ToString(), "")
                        beStock.Color = If(reader("Nombre_Color") IsNot DBNull.Value, reader("Nombre_Color").ToString(), "")

                        If reader("IdProductoTallaColor") IsNot DBNull.Value Then
                            beStock.IdProductoTallaColor = Convert.ToInt32(reader("IdProductoTallaColor"))
                        End If

                        ' ─── Objetos relacionados ──────────────────────────────

                        ' Presentación
                        beStock.Presentacion = New clsBeProducto_Presentacion()
                        beStock.Presentacion.IdPresentacion = beStock.IdPresentacion
                        beStock.Presentacion.Nombre = If(reader("Nombre_Presentacion") IsNot DBNull.Value, reader("Nombre_Presentacion").ToString(), "")
                        beStock.Presentacion.Factor = Convert.ToDouble(reader("factor"))
                        beStock.Presentacion.CajasPorCama = Convert.ToDouble(reader("CajasPorCama"))
                        beStock.Presentacion.CamasPorTarima = Convert.ToDouble(reader("CamasPorTarima"))

                        ' Producto
                        beStock.Producto = New clsBeProducto()
                        beStock.Producto.IdProducto = 0  ' No viene directamente, se puede obtener después
                        beStock.Producto.Codigo = reader("Codigo_Producto").ToString()
                        beStock.Producto.Nombre = reader("Nombre_Producto").ToString()
                        beStock.Producto.Alto = Convert.ToDouble(reader("AltoUbicacion"))
                        beStock.Producto.Largo = Convert.ToDouble(reader("LargoUbicacion"))
                        beStock.Producto.Ancho = Convert.ToDouble(reader("AnchoUbicacion"))

                        ' Producto Estado
                        beStock.ProductoEstado = New clsBeProducto_estado()
                        beStock.ProductoEstado.IdEstado = beStock.IdProductoEstado
                        beStock.ProductoEstado.Nombre = If(reader("NomEstado") IsNot DBNull.Value, reader("NomEstado").ToString(), "")
                        beStock.ProductoEstado.Utilizable = Convert.ToBoolean(reader("EstadoUtilizable"))
                        beStock.ProductoEstado.Dañado = Convert.ToBoolean(reader("Danado"))

                        ' Parámetros
                        beStock.Parametros = New List(Of clsBeStock_parametro)()
                    End If
                End Using
            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            clsLnLog_error_wms.Agregar_Error($"Error en Get_Single_By_ProductoBodega_Ubicacion_Lote: {ex.Message}")
            Throw
        Finally
            If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
        End Try

        Return beStock
    End Function

End Class