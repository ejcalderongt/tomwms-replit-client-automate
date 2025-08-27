Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock_se

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdStockse),0) FROM stock_se"

            Using lCommand As New SqlCommand(vSQL, pConnection)
                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdStockse),0) FROM stock_se"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

            Return lMax

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Serie_By_IdStock(ByVal pIdStock As Integer) As List(Of clsBeStock_se)

        Dim lReturnList As New List(Of clsBeStock_se)

        Try

            Dim vSQL As String = "SELECT * FROM stock_se WHERE IdStock = @IdStock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock_se

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeStock_se
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Public Shared Function Insertar_Stock_Serializado_Recepcion(ByVal BeStockRec As clsBeStock_rec,
                                                                ByVal IdStock As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Integer


        Insertar_Stock_Serializado_Recepcion = 0

        Dim vRegistros As Integer = 0

        Try

            Dim lMaxSE As Integer = MaxID(lConnection, lTransaction)
            Dim lStockSerializadoRec As New List(Of clsBeStock_se_rec)

            lStockSerializadoRec = clsLnStock_se_rec.GetAllSerieByIdStockRec(BeStockRec.IdStockRec,
                                                                             lConnection,
                                                                             lTransaction)

            If Not lStockSerializadoRec Is Nothing Then

                If lStockSerializadoRec.Count > 0 Then

                    For Each Stock_Se_Rec As clsBeStock_se_rec In lStockSerializadoRec

                        Dim ObjS As New clsBeStock_se()

                        clsPublic.CopyObject(Stock_Se_Rec, ObjS)

                        lMaxSE += 1

                        ObjS.IdStock = IdStock
                        ObjS.IdStockSe = lMaxSE

                        Stock_Se_Rec.Regularizado = True
                        Stock_Se_Rec.Fecha_regularizacion = Now

                        vRegistros += Insertar(ObjS, lConnection, lTransaction)

                    Next

                    Insertar_Stock_Serializado_Recepcion = vRegistros

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
