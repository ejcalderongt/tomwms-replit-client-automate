Imports System.Data.SqlClient

Partial Public Class clsLnTrans_series_doc

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean) As DataTable

        GetAllFiltro = Nothing

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_series_doc WHERE 1 > 0 "

                If pActivo Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)


                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Return lDataTable

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExistDocument(ByVal BeSeriesDoc As clsBeTrans_series_doc) As Boolean

        ExistDocument = False

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_series_doc where IdBodega=@IdBodega AND Tipo_Doc=@Tipo_Doc
                        And IdTipoTrans=@IdTipoTrans AND Inicial=@Inicial AND Final=@Final AND @Actual<@Final AND Activo=1"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", BeSeriesDoc.IdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Tipo_Doc", BeSeriesDoc.Tipo_Doc)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoTrans", BeSeriesDoc.IdTipoTrans)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Inicial", BeSeriesDoc.Inicial)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Final", BeSeriesDoc.Final)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Actual", BeSeriesDoc.Actual)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)


                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Return True

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
