Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnPais_departamento

    Public Shared Function GetIdPaisByIdDepartamento(ByVal IdDepartamento As Integer)

        GetIdPaisByIdDepartamento = 0

        Try

            Const sp As String = "SELECT IdPais FROM Pais_departamento" &
            " Where(IdDepartamento = @IdDepartamento)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDEPARTAMENTO", IdDepartamento))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetIdPaisByIdDepartamento = dt.Rows(0).Item("IdPais")
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByIdPais(ByVal IdPais As Integer) As List(Of clsBePais_departamento)

        Try

            Dim lReturnList As New List(Of clsBePais_departamento)
            Const sp As String = "SELECT * FROM Pais_departamento WHERE IdPais = @IdPais"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPais", IdPais)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePais_departamento As New clsBePais_departamento

            For Each dr As DataRow In dt.Rows

                vBePais_departamento = New clsBePais_departamento
                Cargar(vBePais_departamento, dr)
                lReturnList.Add(vBePais_departamento)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal IdPais As Integer) As DataTable

        Try

            Const sp As String = "SELECT IdDepartamento, Nombre from pais_departamento WHERE IdPais = @IdPais"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPais", IdPais)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdDept As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM pais_departamento WHERE IdDepartamento=@IdDepartamento"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdDepartamento", pIdDept)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
