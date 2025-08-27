Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnPais_municipio

    Public Shared Function GetAllMunicipios() As DataTable

        Try

            Dim sp As String = "SELECT a.IdMunicipio as Codigo, a.Nombre as Nombre, " &
                " b.Nombre as Departamento, b.IdDepartamento  " &
                " FROM Pais_Municipio a, Pais_Departamento B " &
                " Where a.IdDepartamento = b.IdDepartamento "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdDepartamentoByIdMunicipio(ByVal IdMunicipio As Integer)

        GetIdDepartamentoByIdMunicipio = 0

        Try

            Const sp As String = "SELECT IdDepartamento FROM Pais_municipio" &
            " Where(IdMunicipio = @IdMunicipio)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUNICIPIO", IdMunicipio))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetIdDepartamentoByIdMunicipio = dt.Rows(0).Item("IdDepartamento")
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByIdDepartamento(ByVal pIdDepartamento As Integer) As List(Of clsBePais_municipio)

        Try

            Dim lReturnList As New List(Of clsBePais_municipio)
            Const sp As String = "SELECT * FROM Pais_municipio WHERE IdDepartamento = @IdDepartamento"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdDepartamento", pIdDepartamento)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePais_municipio As New clsBePais_municipio

            For Each dr As DataRow In dt.Rows

                vBePais_municipio = New clsBePais_municipio
                Cargar(vBePais_municipio, dr)
                lReturnList.Add(vBePais_municipio)

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

    Public Shared Function GetAllForCombo(ByVal pIdDepartamento As Integer) As DataTable

        Try

            Const sp As String = "SELECT IdMunicipio, Nombre FROM Pais_municipio WHERE IdDepartamento = @IdDepartamento"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdDepartamento", pIdDepartamento)
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

    Public Shared Function Exists(ByVal pIdMuni As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM pais_municipio WHERE IdMunicipio=@IdMunicipio"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdMunicipio", pIdMuni)

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
