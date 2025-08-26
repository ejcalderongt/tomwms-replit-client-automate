Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_presentaciones_conversiones

    Public Shared Sub Cargar(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones, ByRef dr As DataRow)
        Try
            With oBeProducto_presentaciones_conversiones
                .IdConversion = IIf(IsDBNull(dr.Item("IdConversion")), 0, dr.Item("IdConversion"))
                .IdPresentacionOrigen = IIf(IsDBNull(dr.Item("IdPresentacionOrigen")), 0, dr.Item("IdPresentacionOrigen"))
                .IdPresentacionDestino = IIf(IsDBNull(dr.Item("IdPresentacionDestino")), 0, dr.Item("IdPresentacionDestino"))
                .Factor = IIf(IsDBNull(dr.Item("Factor")), 0.0, dr.Item("Factor"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Inverso = IIf(IsDBNull(dr.Item("inverso")), False, dr.Item("inverso"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_presentaciones_conversiones")
            Ins.Add("idconversion", "@idconversion", DataType.Parametro)
            Ins.Add("idpresentacionorigen", "@idpresentacionorigen", DataType.Parametro)
            Ins.Add("idpresentaciondestino", "@idpresentaciondestino", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("inverso", "@inverso", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeProducto_presentaciones_conversiones.IdConversion))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONORIGEN", oBeProducto_presentaciones_conversiones.IdPresentacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONDESTINO", oBeProducto_presentaciones_conversiones.IdPresentacionDestino))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeProducto_presentaciones_conversiones.Factor))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentaciones_conversiones.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentaciones_conversiones.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentaciones_conversiones.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentaciones_conversiones.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentaciones_conversiones.User_agr))
            cmd.Parameters.Add(New SqlParameter("@INVERSO", oBeProducto_presentaciones_conversiones.Inverso))

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

    Public Shared Function Actualizar(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_presentaciones_conversiones")
            Upd.Add("idconversion", "@idconversion", DataType.Parametro)
            Upd.Add("idpresentacionorigen", "@idpresentacionorigen", DataType.Parametro)
            Upd.Add("idpresentaciondestino", "@idpresentaciondestino", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("inverso", "@inverso", DataType.Parametro)
            Upd.Where("IdConversion = @IdConversion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeProducto_presentaciones_conversiones.IdConversion))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONORIGEN", oBeProducto_presentaciones_conversiones.IdPresentacionOrigen))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONDESTINO", oBeProducto_presentaciones_conversiones.IdPresentacionDestino))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeProducto_presentaciones_conversiones.Factor))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentaciones_conversiones.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentaciones_conversiones.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentaciones_conversiones.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentaciones_conversiones.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentaciones_conversiones.User_agr))
            cmd.Parameters.Add(New SqlParameter("@INVERSO", oBeProducto_presentaciones_conversiones.Inverso))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_presentaciones_conversiones" &
             "  Where(IdConversion = @IdConversion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeProducto_presentaciones_conversiones.IdConversion))

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

            Const sp As String = " Delete from Producto_presentaciones_conversiones"
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

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones"
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

    Public Shared Function Obtener(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones" & _
            " Where(IdConversion = @IdConversion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONVERSION", oBeProducto_presentaciones_conversiones.IDCONVERSION))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_presentaciones_conversiones, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeProducto_presentaciones_conversiones)

        Try

            Dim lReturnList As New List(Of clsBeProducto_presentaciones_conversiones)
            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_presentaciones_conversiones As New clsBeProducto_presentaciones_conversiones

            For Each dr As DataRow In dt.Rows

                vBeProducto_presentaciones_conversiones = New clsBeProducto_presentaciones_conversiones
                Cargar(vBeProducto_presentaciones_conversiones, dr)
                lReturnList.Add(vBeProducto_presentaciones_conversiones)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones)

        Try

            Const sp As String = "SELECT * FROM Producto_presentaciones_conversiones" & _
            " Where(IdConversion = @IdConversion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONVERSION", pBeProducto_presentaciones_conversiones.IDCONVERSION))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_presentaciones_conversiones, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdConversion),0) FROM Producto_presentaciones_conversiones"

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


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
