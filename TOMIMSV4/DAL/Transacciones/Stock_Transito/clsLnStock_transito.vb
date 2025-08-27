Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnStock_transito

    Public Shared Sub Cargar(ByRef oBeStock_transito As clsBeStock_transito, ByRef dr As DataRow)

        Try

            With oBeStock_transito

                .IdStockTransito = IIf(IsDBNull(dr.Item("IdStockTransito")), 0, dr.Item("IdStockTransito"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                .IdBodegaDestino = IIf(IsDBNull(dr.Item("IdBodegaDestino")), 0, dr.Item("IdBodegaDestino"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdProductoBodegaDestino = IIf(IsDBNull(dr.Item("IdProductoBodegaDestino")), 0, dr.Item("IdProductoBodegaDestino"))
                .IdProductoBodegaOrigen = IIf(IsDBNull(dr.Item("IdProductoBodegaOrigen")), 0, dr.Item("IdProductoBodegaOrigen"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Lic_Plate = IIf(IsDBNull(dr.Item("Lic_Plate")), "", dr.Item("Lic_Plate"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), "01/01/1900", dr.Item("Fecha_Ingreso"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), "01/01/1900", dr.Item("Fecha_Vence"))
                .Fecha_Manufactura = IIf(IsDBNull(dr.Item("Fecha_Manufactura")), "01/01/1900", dr.Item("Fecha_Manufactura"))
                .Cantidad_Recibida = IIf(IsDBNull(dr.Item("Cantidad_Recibida")), 0.0, dr.Item("Cantidad_Recibida"))
                .Fecha_Agregado = IIf(IsDBNull(dr.Item("Fecha_Agregado")), "01/01/1900", dr.Item("Fecha_Agregado"))
                .IdOrdenCompraEnc_BodDest = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc_BodDest")), "0", dr.Item("IdOrdenCompraEnc_BodDest"))
                .IdRecepcionEnc_BodDest = IIf(IsDBNull(dr.Item("IdRecepcionEnc_BodDest")), "0", dr.Item("IdRecepcionEnc_BodDest"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeStock_transito As clsBeStock_transito, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("stock_transito")
            Ins.Add("idstocktransito", "@idstocktransito", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Ins.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Ins.Add("idstock", "@idstock", DataType.Parametro)
            Ins.Add("idproductobodegaorigen", "@idproductobodegaorigen", DataType.Parametro)
            Ins.Add("idproductobodegadestino", "@idproductobodegadestino", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Ins.Add("idordencompraenc_boddest", "@idordencompraenc_boddest", DataType.Parametro)
            Ins.Add("idrecepcionenc_boddest", "@idrecepcionenc_boddest", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", oBeStock_transito.IdStockTransito))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeStock_transito.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeStock_transito.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", oBeStock_transito.IdBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_transito.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGADESTINO", oBeStock_transito.IdProductoBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGAORIGEN", oBeStock_transito.IdProductoBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_transito.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_transito.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_transito.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_transito.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_transito.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_transito.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_transito.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_transito.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_transito.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeStock_transito.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeStock_transito.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_transito.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_transito.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_transito.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_transito.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_transito.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_transito.Fecha_Manufactura))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeStock_transito.Cantidad_Recibida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeStock_transito.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC_BODDEST", oBeStock_transito.IdOrdenCompraEnc_BodDest))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC_BODDEST", oBeStock_transito.IdRecepcionEnc_BodDest))

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

    Public Shared Function Actualizar(ByRef oBeStock_transito As clsBeStock_transito, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock_transito")
            Upd.Add("idstocktransito", "@idstocktransito", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idbodegaorigen", "@idbodegaorigen", DataType.Parametro)
            Upd.Add("idbodegadestino", "@idbodegadestino", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodegaorigen", "@idproductobodegaorigen", DataType.Parametro)
            Upd.Add("idproductobodegadestino", "@idproductobodegadestino", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("fecha_agregado", "@fecha_agregado", DataType.Parametro)
            Upd.Add("idordencompraenc_boddest", "@idordencompraenc_boddest", DataType.Parametro)
            Upd.Add("idrecepcionenc_boddest", "@idrecepcionenc_boddest", DataType.Parametro)
            Upd.Where("IdStockTransito = @IdStockTransito")

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

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", oBeStock_transito.IdStockTransito))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeStock_transito.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAORIGEN", oBeStock_transito.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGADESTINO", oBeStock_transito.IdBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeStock_transito.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGADESTINO", oBeStock_transito.IdProductoBodegaDestino))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGAORIGEN", oBeStock_transito.IdProductoBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeStock_transito.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock_transito.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeStock_transito.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_transito.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeStock_transito.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeStock_transito.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeStock_transito.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeStock_transito.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeStock_transito.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeStock_transito.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeStock_transito.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeStock_transito.Lote))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock_transito.Lic_Plate))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeStock_transito.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeStock_transito.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeStock_transito.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", oBeStock_transito.Fecha_Manufactura))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeStock_transito.Cantidad_Recibida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGREGADO", oBeStock_transito.Fecha_Agregado))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC_BODDEST", oBeStock_transito.IdOrdenCompraEnc_BodDest))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC_BODDEST", oBeStock_transito.IdRecepcionEnc_BodDest))

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


    Public Shared Function Eliminar(ByRef oBeStock_transito As clsBeStock_transito, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Stock_transito" &
             "  Where(IdStockTransito = @IdStockTransito)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", oBeStock_transito.IdStockTransito))

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

            Const sp As String = " Delete from Stock_transito"
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

            Const sp As String = "SELECT * FROM Stock_transito"
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

    Public Shared Function Obtener(ByRef oBeStock_transito As clsBeStock_transito) As Boolean

        Try

            Const sp As String = "SELECT * FROM Stock_transito" &
            " Where(IdStockTransito = @IdStockTransito)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", oBeStock_transito.IdStockTransito))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeStock_transito, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeStock_transito)

        Try

            Dim lReturnList As New List(Of clsBeStock_transito)
            Const sp As String = "SELECT * FROM Stock_transito"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStock_transito As New clsBeStock_transito

            For Each dr As DataRow In dt.Rows
                vBeStock_transito = New clsBeStock_transito
                Cargar(vBeStock_transito, dr)
                lReturnList.Add(vBeStock_transito)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeStock_transito As clsBeStock_transito)

        Try

            Const sp As String = "SELECT * FROM Stock_transito" &
            " Where(IdStockTransito = @IdStockTransito)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", pBeStock_transito.IDSTOCKTRANSITO))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_transito, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockTransito),0) FROM Stock_transito"

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
