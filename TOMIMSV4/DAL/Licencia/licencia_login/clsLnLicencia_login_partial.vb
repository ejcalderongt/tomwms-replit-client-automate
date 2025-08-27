Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLicencia_login

    Public Shared Function Exist(ByVal pIdDisp As String)

        Exist = False

        Try

            Const sp As String = "SELECT * FROM Licencia_login" &
                                 " Where(idDisp = @idDisp)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idDisp", pIdDisp))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal pIdDisp As String,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction)

        Exist = False

        Try

            Const sp As String = "SELECT * FROM Licencia_login " &
                                 " Where(idDisp = @idDisp)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idDisp", pIdDisp))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
