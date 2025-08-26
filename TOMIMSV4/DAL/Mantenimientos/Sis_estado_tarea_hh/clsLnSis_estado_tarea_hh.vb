Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnSis_estado_tarea_hh

    Public Shared Sub Cargar(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh, ByRef dr As DataRow)

        Try

            With oBeSis_estado_tarea_hh
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
            End With

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Function Insertar(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("sis_estado_tarea_hh")
            Ins.Add("idestado", "@idestado", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeSis_estado_tarea_hh.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeSis_estado_tarea_hh.Descripcion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeSis_estado_tarea_hh.IdEstado = CInt(cmd.Parameters("@IDESTADO").Value)

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("sis_estado_tarea_hh")
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Where("IdEstado = @IdEstado")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeSis_estado_tarea_hh.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeSis_estado_tarea_hh.Descripcion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " Delete from Sis_estado_tarea_hh" &
             "  Where(IdEstado = @IdEstado)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If


            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeSis_estado_tarea_hh.IdEstado))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Sis_estado_tarea_hh"

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
            Throw New Exception("Sis_estado_tarea_hh_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Sis_estado_tarea_hh"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh) As Boolean

        Try

            Const sp As String = "SELECT * FROM Sis_estado_tarea_hh" &
            " Where(IdEstado = @IdEstado)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADO", oBeSis_estado_tarea_hh.IdEstado))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeSis_estado_tarea_hh, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeSis_estado_tarea_hh)

        Try

            Dim lReturnList As New List(Of clsBeSis_estado_tarea_hh)
            Const sp As String = "SELECT * FROM Sis_estado_tarea_hh"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeSis_estado_tarea_hh As New clsBeSis_estado_tarea_hh

            For Each dr As DataRow In dt.Rows

                vBeSis_estado_tarea_hh = New clsBeSis_estado_tarea_hh
                Cargar(vBeSis_estado_tarea_hh, dr)
                lReturnList.Add(vBeSis_estado_tarea_hh)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Sis_estado_tarea_hh_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeSis_estado_tarea_hh As clsBeSis_estado_tarea_hh)

        Try

            Const sp As String = "SELECT * FROM Sis_estado_tarea_hh" &
            " Where(IdEstado = @IdEstado)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADO", pBeSis_estado_tarea_hh.IdEstado))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeSis_estado_tarea_hh, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
