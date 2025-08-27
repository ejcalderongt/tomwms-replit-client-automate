Imports System.Reflection
Imports System.Data.SqlClient

Partial Public Class clsLnTrans_picking_op


    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdOperadorPicking),0) FROM trans_picking_op"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdPickingEnc(ByVal pIdPicking As Integer) As List(Of clsBeTrans_picking_op)

        Dim lReturnList As New List(Of clsBeTrans_picking_op)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim vSQL As String = "SELECT op.* 
                                      FROM trans_picking_op AS op INNER JOIN 
                                           operador_bodega AS b ON op.IdOperadorBodega = b.IdOperadorBodega
                                      WHERE op.IdPickingEnc=@IdPickingEnc ORDER BY b.IdOperadorBodega "

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPicking)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_op

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_op

                                Cargar(Obj, lRow)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT09062025: con transaccion para envio a la nube
    Public Shared Function Get_All_By_IdPickingEnc(ByVal pIdPicking As Integer, ByVal lConnection As SqlConnection,
                                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_op)

        Dim lReturnList As New List(Of clsBeTrans_picking_op)

        Try

            Dim vSQL As String = "SELECT op.* 
                                      FROM trans_picking_op AS op INNER JOIN 
                                           operador_bodega AS b ON op.IdOperadorBodega = b.IdOperadorBodega
                                      WHERE op.IdPickingEnc=@IdPickingEnc ORDER BY b.IdOperadorBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPicking)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_op

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_op

                        Cargar(Obj, lRow)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



    Public Shared Function Delete(ByVal pIdOperadorPicking As Integer) As Integer

        Delete = 0

        Try

            Dim sp As String = String.Format("DELETE FROM trans_picking_op WHERE IdOperadorPicking={0}", pIdOperadorPicking)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        Delete = lCommand.ExecuteNonQuery()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub Guarda_Trans_picking_operador(ByVal IdPickingEnc As Integer,
                                                ByVal pListObjPickingOpe As List(Of clsBeTrans_picking_op),
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)               

        Try

            Dim lMaxIdDet As Integer = MaxID(lConnection, lTransaction)

            For Each Obj As clsBeTrans_picking_op In pListObjPickingOpe

                If Obj.IsNew Then
                    lMaxIdDet += 1
                    Obj.IdPickingEnc = IdPickingEnc
                    Obj.IdOperadorPicking = lMaxIdDet
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If                

            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub


End Class
