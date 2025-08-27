Imports System.Data.SqlClient

Partial Public Class clsLnLog_importacion_excel

    Public Shared Function Existe(ByVal pHash As String) As Boolean

        Existe = False

        Try

            Const sp As String = "SELECT * FROM Log_importacion_excel " &
            " Where(hash_archivo = @hash_archivo) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@hash_archivo", pHash)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeLog_importacion_excel As New clsBeLog_importacion_excel

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Existe = True
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
