Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_config_ent

    Public Shared Function Exists(ByVal pIdNavEnt As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM i_nav_config_ent WHERE idnavent=@pIdNavEnt"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@pIdNavEnt", pIdNavEnt)
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

    Public Shared Function ListarEnt() As DataTable

        Try

            Dim vSQL As String = "SELECT i_nav_ent.*, ISNULL(i_nav_config_ent.idnavconfigent,0) AS IdNavConfigEnt, " &
                    " ISNULL(i_nav_config_ent.endpoint,'NotSet') AS EndPointConfig " &
                    " FROM i_nav_ent LEFT OUTER JOIN " &
                    " i_nav_config_ent ON i_nav_ent.idnavent = i_nav_config_ent.idnavent "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(VSQL, lConnection) With {.CommandType = CommandType.Text}
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

End Class
