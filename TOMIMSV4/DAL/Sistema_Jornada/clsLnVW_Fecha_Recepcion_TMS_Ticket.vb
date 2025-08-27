Imports System.Data.SqlClient

Public Class clsLnVW_Fecha_Recepcion_TMS_Ticket

    Public Shared Sub Cargar(ByRef oBeTMP_VW_Fecha_Recepcion_TMS_Ticket As clsBeVW_Fecha_Recepcion_TMS_Ticket, ByRef dr As DataRow)

        Try

            With oBeTMP_VW_Fecha_Recepcion_TMS_Ticket
                .IdTicket = IIf(IsDBNull(dr.Item("IdTicket")), 0, dr.Item("IdTicket"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("Fecha_Ingreso")), Date.Now, dr.Item("Fecha_Ingreso"))
                .Fecha_Creacion = IIf(IsDBNull(dr.Item("Fecha_Creacion")), Date.Now, dr.Item("Fecha_Creacion"))
                .Fecha_Recepcion = IIf(IsDBNull(dr.Item("Fecha_Recepcion")), Date.Now, dr.Item("Fecha_Recepcion"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All() As List(Of clsBeVW_Fecha_Recepcion_TMS_Ticket)

        Dim lReturnList As New List(Of clsBeVW_Fecha_Recepcion_TMS_Ticket)

        Try

            Const sp As String = "SELECT * FROM TMP_VW_Fecha_Recepcion_TMS_Ticket "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTMP_VW_Fecha_Recepcion_TMS_Ticket As New clsBeVW_Fecha_Recepcion_TMS_Ticket

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTMP_VW_Fecha_Recepcion_TMS_Ticket = New clsBeVW_Fecha_Recepcion_TMS_Ticket()
                            Cargar(vBeTMP_VW_Fecha_Recepcion_TMS_Ticket, dr)
                            lReturnList.Add(vBeTMP_VW_Fecha_Recepcion_TMS_Ticket)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTMP_VW_Fecha_Recepcion_TMS_Ticket As clsBeVW_Fecha_Recepcion_TMS_Ticket)

        Try

            Const sp As String = "SELECT * FROM TMP_VW_Fecha_Recepcion_TMS_Ticket"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTMP_VW_Fecha_Recepcion_TMS_Ticket As New clsBeVW_Fecha_Recepcion_TMS_Ticket

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTMP_VW_Fecha_Recepcion_TMS_Ticket, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
