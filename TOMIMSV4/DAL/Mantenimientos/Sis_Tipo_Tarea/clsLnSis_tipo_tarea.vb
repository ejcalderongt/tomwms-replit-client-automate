Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnSis_tipo_tarea

    Public Shared Sub Cargar(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea, ByRef dr As DataRow)
        Try
            With oBeSis_tipo_tarea
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Contabilizar = IIf(IsDBNull(dr.Item("Contabilizar")), False, dr.Item("Contabilizar"))
            End With
        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("sis_tipo_tarea")
            Ins.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("contabilizar", "@contabilizar", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeSis_tipo_tarea.IdTipoTarea))
            cmd.Parameters("@IDTIPOTAREA").Direction = ParameterDirection.Output
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeSis_tipo_tarea.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CONTABILIZAR", oBeSis_tipo_tarea.Contabilizar))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeSis_tipo_tarea.IdTipoTarea = CInt(cmd.Parameters("@IDTIPOTAREA").Value)

        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("sis_tipo_tarea")
            Upd.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("contabilizar", "@contabilizar", DataType.Parametro)
            Upd.Where("IdTipoTarea = @IdTipoTarea")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeSis_tipo_tarea.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeSis_tipo_tarea.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CONTABILIZAR", oBeSis_tipo_tarea.Contabilizar))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Sis_tipo_tarea" &
             "  Where(IdTipoTarea = @IdTipoTarea)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeSis_tipo_tarea.IdTipoTarea))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Sis_tipo_tarea"

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
            Throw New Exception("Sis_tipo_tarea_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Sis_tipo_tarea"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeSis_tipo_tarea As clsBeSis_tipo_tarea) As Boolean

        Try

            Const sp As String = "SELECT * FROM Sis_tipo_tarea" &
            " Where(IdTipoTarea = @IdTipoTarea)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeSis_tipo_tarea.IdTipoTarea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeSis_tipo_tarea, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeSis_tipo_tarea)

        Try

            Dim lReturnList As New List(Of clsBeSis_tipo_tarea)
            Const sp As String = "SELECT * FROM Sis_tipo_tarea"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeSis_tipo_tarea As New clsBeSis_tipo_tarea

            For Each dr As DataRow In dt.Rows

                vBeSis_tipo_tarea = New clsBeSis_tipo_tarea
                Cargar(vBeSis_tipo_tarea, dr)
                lReturnList.Add(vBeSis_tipo_tarea)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Sis_tipo_tarea_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeSis_tipo_tarea As clsBeSis_tipo_tarea)

        Try

            Const sp As String = "SELECT * FROM Sis_tipo_tarea" &
            " Where(IdTipoTarea = @IdTipoTarea)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOTAREA", pBeSis_tipo_tarea.IdTipoTarea))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeSis_tipo_tarea, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo() As DataTable

        Try

            Const sp As String = "SELECT IdTipoTarea,Nombre FROM sis_tipo_tarea"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
