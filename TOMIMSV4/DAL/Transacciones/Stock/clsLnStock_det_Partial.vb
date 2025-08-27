Imports System.Data.SqlClient

Partial Public Class clsLnStock_det

    Public Shared Function GetSingle(ByVal IdStock As Integer,
                                      ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeStock_det

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Stock_det Where(IdStock = @IdStock)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_det As New clsBeStock_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeStock_det, lDataTable.Rows(0))
                    GetSingle = vBeStock_det
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
