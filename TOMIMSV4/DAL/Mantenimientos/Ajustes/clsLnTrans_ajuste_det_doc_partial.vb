Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_ajuste_det_doc

    Public Shared Function GetAll_ByIdAjusteEnc(ByVal IdAjusteEnc As Integer) As DataTable

        GetAll_ByIdAjusteEnc = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_ajuste_det_doc where idajusteenc=@IdAjusteEnc"
            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc)

            Dim dt As New DataTable

            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
