Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnImpresion_productos_barras

    Public Shared Sub Cargar(ByRef oBeImpresion_productos_barras As clsBeImpresion_productos_barras, ByRef dr As DataRow)
        Try
            With oBeImpresion_productos_barras
                .IdProductoBarra = IIf(IsDBNull(dr.Item("IdProductoBarra")), 0, dr.Item("IdProductoBarra"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Cantidad_impresiones = IIf(IsDBNull(dr.Item("cantidad_impresiones")), 0, dr.Item("cantidad_impresiones"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .UnidadMedida = IIf(IsDBNull(dr.Item("UnidadMedida")), "", dr.Item("UnidadMedida"))
                .Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                .Camas_Por_Tarima = IIf(IsDBNull(dr.Item("Camas_Por_Tarima")), 0, dr.Item("Camas_Por_Tarima"))
                .Cajas_Por_Cama = IIf(IsDBNull(dr.Item("Cajas_Por_Cama")), 0, dr.Item("Cajas_Por_Cama"))
                .Cantidad_Presentacion = IIf(IsDBNull(dr.Item("Cantidad_Presentacion")), 0.0, dr.Item("Cantidad_Presentacion"))
                .Factor = IIf(IsDBNull(dr.Item("Factor")), 0.0, dr.Item("Factor"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), Nothing, dr.Item("Fecha_Ingreso"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), Nothing, dr.Item("Fecha_Vence"))
                .Fecha_agr = IIf(IsDBNull(dr.Item("fecha_agr")), Date.Now, dr.Item("fecha_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Impreso = IIf(IsDBNull(dr.Item("impreso")), False, dr.Item("impreso"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdImpresora = IIf(IsDBNull(dr.Item("IdImpresora")), 0, dr.Item("IdImpresora"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeImpresion_productos_barras As clsBeImpresion_productos_barras, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresion_productos_barras")
            Ins.Add("idproductobarra", "@idproductobarra", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("cantidad_impresiones", "@cantidad_impresiones", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("unidadmedida", "@unidadmedida", DataType.Parametro)
            Ins.Add("presentacion", "@presentacion", DataType.Parametro)
            Ins.Add("camas_por_tarima", "@camas_por_tarima", DataType.Parametro)
            Ins.Add("cajas_por_cama", "@cajas_por_cama", DataType.Parametro)
            Ins.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("impreso", "@impreso", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("IdImpresora", "@IdImpresora", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBARRA", oBeImpresion_productos_barras.IdProductoBarra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresion_productos_barras.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresion_productos_barras.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeImpresion_productos_barras.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_IMPRESIONES", oBeImpresion_productos_barras.Cantidad_impresiones))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeImpresion_productos_barras.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeImpresion_productos_barras.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@UNIDADMEDIDA", oBeImpresion_productos_barras.UnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeImpresion_productos_barras.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CAMAS_POR_TARIMA", oBeImpresion_productos_barras.Camas_Por_Tarima))
            cmd.Parameters.Add(New SqlParameter("@CAJAS_POR_CAMA", oBeImpresion_productos_barras.Cajas_Por_Cama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeImpresion_productos_barras.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeImpresion_productos_barras.Factor))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeImpresion_productos_barras.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeImpresion_productos_barras.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeImpresion_productos_barras.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeImpresion_productos_barras.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresion_productos_barras.User_agr))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeImpresion_productos_barras.Impreso))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresion_productos_barras.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresion_productos_barras.IdImpresora))

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

    Public Shared Function Actualizar(ByRef oBeImpresion_productos_barras As clsBeImpresion_productos_barras, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresion_productos_barras")
            Upd.Add("idproductobarra", "@idproductobarra", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("cantidad_impresiones", "@cantidad_impresiones", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("unidadmedida", "@unidadmedida", DataType.Parametro)
            Upd.Add("presentacion", "@presentacion", DataType.Parametro)
            Upd.Add("camas_por_tarima", "@camas_por_tarima", DataType.Parametro)
            Upd.Add("cajas_por_cama", "@cajas_por_cama", DataType.Parametro)
            Upd.Add("cantidad_presentacion", "@cantidad_presentacion", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("impreso", "@impreso", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("IdImpresora", "@IdImpresora", DataType.Parametro)
            Upd.Where("IdProductoBarra = @IdProductoBarra")

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBARRA", oBeImpresion_productos_barras.IdProductoBarra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeImpresion_productos_barras.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresion_productos_barras.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeImpresion_productos_barras.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_IMPRESIONES", oBeImpresion_productos_barras.Cantidad_impresiones))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeImpresion_productos_barras.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeImpresion_productos_barras.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@UNIDADMEDIDA", oBeImpresion_productos_barras.UnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeImpresion_productos_barras.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@CAMAS_POR_TARIMA", oBeImpresion_productos_barras.Camas_Por_Tarima))
            cmd.Parameters.Add(New SqlParameter("@CAJAS_POR_CAMA", oBeImpresion_productos_barras.Cajas_Por_Cama))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_PRESENTACION", oBeImpresion_productos_barras.Cantidad_Presentacion))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeImpresion_productos_barras.Factor))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeImpresion_productos_barras.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", oBeImpresion_productos_barras.Fecha_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeImpresion_productos_barras.Fecha_Vence))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeImpresion_productos_barras.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresion_productos_barras.User_agr))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeImpresion_productos_barras.Impreso))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresion_productos_barras.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresion_productos_barras.IdImpresora))


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


    Public Shared Function Eliminar(ByRef oBeImpresion_productos_barras As clsBeImpresion_productos_barras, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresion_productos_barras" &
             "  Where(IdProductoBarra = @IdProductoBarra)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBARRA", oBeImpresion_productos_barras.IdProductoBarra))

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

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresion_productos_barras"
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

            Const sp As String = "SELECT * FROM Impresion_productos_barras"
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
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

    Public Shared Function Obtener(ByRef oBeImpresion_productos_barras As clsBeImpresion_productos_barras) As Boolean

        Try

            Const sp As String = "SELECT * FROM Impresion_productos_barras" &
            " Where(IdProductoBarra = @IdProductoBarra)"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBARRA", oBeImpresion_productos_barras.IdProductoBarra))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeImpresion_productos_barras, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeImpresion_productos_barras)

        Try

            Dim lReturnList As New List(Of clsBeImpresion_productos_barras)
            Const sp As String = "SELECT * FROM Impresion_productos_barras WHERE activo=1 AND impreso = 0 "
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeImpresion_productos_barras As New clsBeImpresion_productos_barras

            For Each dr As DataRow In dt.Rows
                vBeImpresion_productos_barras = New clsBeImpresion_productos_barras
                Cargar(vBeImpresion_productos_barras, dr)
                lReturnList.Add(vBeImpresion_productos_barras)
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

    Public Shared Function GetSingle(ByRef pBeImpresion_productos_barras As clsBeImpresion_productos_barras)

        Try

            Const sp As String = "SELECT * FROM Impresion_productos_barras" &
            " Where(IdProductoBarra = @IdProductoBarra)"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBARRA", pBeImpresion_productos_barras.IdProductoBarra))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeImpresion_productos_barras, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoBarra),0) FROM Impresion_productos_barras"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Barra(ByRef BeImpresion As clsBeImpresion_productos_barras) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            BeImpresion.IdProductoBarra = MaxID(lConnection, lTransaction) + 1

            Insertar(BeImpresion, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
