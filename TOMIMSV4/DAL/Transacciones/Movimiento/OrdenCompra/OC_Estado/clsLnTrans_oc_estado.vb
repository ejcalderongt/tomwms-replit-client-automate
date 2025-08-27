Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_oc_estado

    Public Shared Sub Cargar(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado, ByRef dr As DataRow)
        Try
            With oBeTrans_oc_estado
                .IdEstadoOC = IIf(IsDBNull(dr.Item("IdEstadoOC")), 0, dr.Item("IdEstadoOC"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
            End With
        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Ins.Init("trans_oc_estado")
            Ins.Add("idestadooc", "@idestadooc", "F")
            Ins.Add("nombre", "@nombre", "F")

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If EsTransaccional Then
                cmd = New SqlClient.SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlClient.SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))
            cmd.Parameters("@IDESTADOOC").Direction = ParameterDirection.Output
            cmd.Parameters.Add(New SqlClient.SqlParameter("@NOMBRE", oBeTrans_oc_estado.Nombre))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

            oBeTrans_oc_estado.IdEstadoOC = CInt(cmd.Parameters("@IDESTADOOC").Value)

        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Insertar: " & ex.Message)
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Upd.Init("trans_oc_estado")
            Upd.Add("idestadooc", "@idestadooc", "F")
            Upd.Add("nombre", "@nombre", "F")
            Upd.Where("IdEstadoOC = @IdEstadoOC")

            Dim sp As String = Upd.SQL()

            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}


            If EsTransaccional Then
                cmd = New SqlClient.SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlClient.SqlCommand(sp, cnn)
                cnn.Open()
            End If

            cmd.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))
            cmd.Parameters.Add(New SqlClient.SqlParameter("@NOMBRE", oBeTrans_oc_estado.Nombre))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Actualizar: " & ex.Message)
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try

    End Function


    Public Function Eliminar(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try


            Const sp As String = " Delete from Trans_oc_estado" &
             "  Where(IdEstadoOC = @IdEstadoOC)"


            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            If EsTransaccional Then

                cmd = New SqlClient.SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                cmd = New SqlClient.SqlCommand(sp, cnn)
                cnn.Open()

            End If


            cmd.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected


        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Eliminar: " & ex.Message)
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            Const sp As String = " Delete from Trans_oc_estado"

            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim EsTransaccional As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If EsTransaccional Then
                cmd = New SqlClient.SqlCommand(sp, pConection)
            Else
                cmd = New SqlClient.SqlCommand(sp, cnn)
                cnn.Open()

            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Eliminar: " & ex.Message)
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_oc_estado"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_oc_estado" &
            " Where(IdEstadoOC = @IdEstadoOC)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_estado, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    'Public Shared Function GetAll() As List(Of clsBeTrans_oc_estado)

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_oc_estado)
    '        Const sp As String = "SELECT * FROM Trans_oc_estado"
    '        Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '        Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)
    '        Dim dt As New DataTable

    '        dad.Fill(dt)

    '        Dim lnTrans_oc_estado As New clsLnTrans_oc_estado
    '        Dim vBeTrans_oc_estado As New clsBeTrans_oc_estado

    '        For Each dr As DataRow In dt.Rows
    '            vBeTrans_oc_estado = New clsBeTrans_oc_estado
    '            lnTrans_oc_estado.Cargar(vBeTrans_oc_estado, dr)
    '            lReturnList.Add(vBeTrans_oc_estado)
    '        Next

    '        Return lReturnList

    '        If cnn.State = ConnectionState.Open Then cnn.Close()
    '        cnn.Dispose()
    '        cmd.Dispose()
    '    Catch ex As Exception
    '        Throw New Exception("Trans_oc_estado_GetAll: " & ex.Message)
    '    End Try

    'End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_oc_estado)

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_estado)
            Const sp As String = "SELECT * FROM Trans_oc_estado"
            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            
            Dim vBeTrans_oc_estado As New clsBeTrans_oc_estado

            For Each dr As DataRow In dt.Rows
                vBeTrans_oc_estado = New clsBeTrans_oc_estado
                Cargar(vBeTrans_oc_estado, dr)
                lReturnList.Add(vBeTrans_oc_estado)
            Next

            Return lReturnList

            If cnn.State = ConnectionState.Open Then cnn.Close()
            cnn.Dispose()
            cmd.Dispose()
        Catch ex As Exception
            Throw New Exception("Trans_oc_estado_GetAll: " & ex.message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_oc_estado As clsBeTrans_oc_estado)

        Try

            Const sp As String = "SELECT * FROM Trans_oc_estado" & _
            " Where(IdEstadoOC = @IdEstadoOC)"

            Dim cnn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@IDESTADOOC", pBeTrans_oc_estado.IdEstadoOC))
            
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_oc_estado, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
