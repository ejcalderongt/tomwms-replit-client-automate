Imports System.Data.SqlClient

Partial Public Class clsLnTrans_inv_operador

    Public Shared Function GetAllByUbic(ByVal pIdUbicacion As Integer, ByVal pIdOperador As Integer, ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_operador)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_operador)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * from trans_inv_operador
                        Where idubic=@IdUbicacion AND idoperador=@IdOperador AND idinventarioenc=@IdInventario"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_operador

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each dr As DataRow In lDataTable.Rows
                            Obj = New clsBeTrans_inv_operador
                            Cargar(Obj, dr)
                            lReturnList.Add(Obj)
                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
