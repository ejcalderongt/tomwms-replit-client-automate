Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_kit_composicion

    Public Shared Sub Cargar(ByRef oBeProducto_kit_composicion As clsBeProducto_kit_composicion, ByRef dr As DataRow)
        Try
            With oBeProducto_kit_composicion
                .IdProductoKitComposicion = IIf(IsDBNull(dr.Item("IdProductoKitComposicion")), 0, dr.Item("IdProductoKitComposicion"))
                .IdProductoPadre = IIf(IsDBNull(dr.Item("IdProductoPadre")), 0, dr.Item("IdProductoPadre"))
                .IdProductoHijo = IIf(IsDBNull(dr.Item("IdProductoHijo")), 0, dr.Item("IdProductoHijo"))
                .IdUnidadMedidaBasicaPadre = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasicaPadre")), 0, dr.Item("IdUnidadMedidaBasicaPadre"))
                .IdUnidadMedidaBasicaHijo = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasicaHijo")), 0, dr.Item("IdUnidadMedidaBasicaHijo"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Fecha_agr = IIf(IsDBNull(dr.Item("fecha_agr")), Date.Now, dr.Item("fecha_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fecha_mod = IIf(IsDBNull(dr.Item("fecha_mod")), Date.Now, dr.Item("fecha_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_kit_composicion As clsBeProducto_kit_composicion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_kit_composicion")
            Ins.Add("idproductokitcomposicion", "@idproductokitcomposicion", DataType.Parametro)
            Ins.Add("idproductopadre", "@idproductopadre", DataType.Parametro)
            Ins.Add("idproductohijo", "@idproductohijo", DataType.Parametro)
            Ins.Add("idunidadmedidabasicapadre", "@idunidadmedidabasicapadre", DataType.Parametro)
            Ins.Add("idunidadmedidabasicahijo", "@idunidadmedidabasicahijo", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fecha_mod", "@fecha_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOKITCOMPOSICION", oBeProducto_kit_composicion.IdProductoKitComposicion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPADRE", oBeProducto_kit_composicion.IdProductoPadre))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOHIJO", oBeProducto_kit_composicion.IdProductoHijo))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICAPADRE", oBeProducto_kit_composicion.IdUnidadMedidaBasicaPadre))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICAHIJO", oBeProducto_kit_composicion.IdUnidadMedidaBasicaHijo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_kit_composicion.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeProducto_kit_composicion.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_kit_composicion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MOD", oBeProducto_kit_composicion.Fecha_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_kit_composicion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_kit_composicion.IdBodega))

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

    Public Shared Function Actualizar(ByRef oBeProducto_kit_composicion As clsBeProducto_kit_composicion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_kit_composicion")
            Upd.Add("idproductokitcomposicion", "@idproductokitcomposicion", DataType.Parametro)
            Upd.Add("idproductopadre", "@idproductopadre", DataType.Parametro)
            Upd.Add("idproductohijo", "@idproductohijo", DataType.Parametro)
            Upd.Add("idunidadmedidabasicapadre", "@idunidadmedidabasicapadre", DataType.Parametro)
            Upd.Add("idunidadmedidabasicahijo", "@idunidadmedidabasicahijo", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("fecha_agr", "@fecha_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fecha_mod", "@fecha_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Where("IdProductoKitComposicion = @IdProductoKitComposicion")

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOKITCOMPOSICION", oBeProducto_kit_composicion.IdProductoKitComposicion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPADRE", oBeProducto_kit_composicion.IdProductoPadre))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOHIJO", oBeProducto_kit_composicion.IdProductoHijo))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICAPADRE", oBeProducto_kit_composicion.IdUnidadMedidaBasicaPadre))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICAHIJO", oBeProducto_kit_composicion.IdUnidadMedidaBasicaHijo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_kit_composicion.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FECHA_AGR", oBeProducto_kit_composicion.Fecha_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_kit_composicion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MOD", oBeProducto_kit_composicion.Fecha_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_kit_composicion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_kit_composicion.IdBodega))

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


    Public Shared Function Eliminar(ByRef oBeProducto_kit_composicion As clsBeProducto_kit_composicion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_kit_composicion" &
             "  Where(IdProductoKitComposicion = @IdProductoKitComposicion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOKITCOMPOSICION", oBeProducto_kit_composicion.IdProductoKitComposicion))

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

            Const sp As String = " Delete from Producto_kit_composicion"
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

            Const sp As String = "SELECT * FROM Producto_kit_composicion"
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

    Public Shared Function Obtener(ByRef oBeProducto_kit_composicion As clsBeProducto_kit_composicion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_kit_composicion" &
            " Where(IdProductoKitComposicion = @IdProductoKitComposicion)"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOKITCOMPOSICION", oBeProducto_kit_composicion.IdProductoKitComposicion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_kit_composicion, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeProducto_kit_composicion)

        Try

            Dim lReturnList As New List(Of clsBeProducto_kit_composicion)
            Const sp As String = "SELECT * FROM Producto_kit_composicion"
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_kit_composicion As New clsBeProducto_kit_composicion

            For Each dr As DataRow In dt.Rows
                vBeProducto_kit_composicion = New clsBeProducto_kit_composicion
                Cargar(vBeProducto_kit_composicion, dr)
                lReturnList.Add(vBeProducto_kit_composicion)
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

    Public Shared Function GetSingle(ByRef pBeProducto_kit_composicion As clsBeProducto_kit_composicion)

        Try

            Const sp As String = "SELECT * FROM Producto_kit_composicion" &
            " Where(IdProductoKitComposicion = @IdProductoKitComposicion)"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOKITCOMPOSICION", pBeProducto_kit_composicion.IDPRODUCTOKITCOMPOSICION))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_kit_composicion, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoKitComposicion),0) FROM Producto_kit_composicion"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoKitComposicion),0) FROM Producto_kit_composicion"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using


            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
