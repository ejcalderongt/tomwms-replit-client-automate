Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnEmpresa_transporte

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Try

            Dim vSQL As String = "SELECT IdEmpresa,IdEmpresaTransporte AS Código, Nombre FROM empresa_transporte WHERE 1 > 0 "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            If Not String.IsNullOrEmpty(pFiltro) Then
                vSQL += String.Format(" AND (IdEmpresaTransporte LIKE '%{0}%'", pFiltro)
                vSQL += String.Format(" OR Nombre LIKE '%{0}%')", pFiltro)
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByIdEmpresa(ByVal pIdEmpresa As Integer) As List(Of clsBeEmpresa_transporte)

        Try

            Dim lReturnList As New List(Of clsBeEmpresa_transporte)
            Const sp As String = "SELECT * FROM Empresa_transporte WHERE IdEmpresa = @IdEmpresa AND Activo = 1 "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEmpresa_transporte As New clsBeEmpresa_transporte

            For Each dr As DataRow In dt.Rows

                vBeEmpresa_transporte = New clsBeEmpresa_transporte
                Cargar(vBeEmpresa_transporte, dr)
                lReturnList.Add(vBeEmpresa_transporte)

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

    Public Shared Function Get_All_For_Combo(ByVal pIdEmpresa As Integer) As DataTable

        Try

            Const sp As String = "SELECT IdEmpresaTransporte, nombre as Nombre FROM Empresa_transporte WHERE IdEmpresa = @IdEmpresa AND Activo = 1 "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
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

End Class
