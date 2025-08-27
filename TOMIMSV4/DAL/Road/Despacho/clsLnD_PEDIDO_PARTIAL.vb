Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnD_PEDIDO

    Public Shared Function Get_All_Pendientes_Procesar_WMS() As List(Of clsBeD_PEDIDO)

        Dim lReturnList As New List(Of clsBeD_PEDIDO)

        Try

            Const sp As String = "SELECT * FROM D_PEDIDO WHERE STATCOM = 'N' "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeD_PEDIDO As New clsBeD_PEDIDO

                        For Each dr As DataRow In lDataTable.Rows
                            vBeD_PEDIDO = New clsBeD_PEDIDO()
                            Cargar(vBeD_PEDIDO, dr)
                            vBeD_PEDIDO.Detalle = clsLnD_PEDIDOD.Get_All_By_Corel(vBeD_PEDIDO.COREL, lConnection, lTransaction)
                            lReturnList.Add(vBeD_PEDIDO)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
