Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ped_compra_det_lote

    Public Shared Function Exist(ByRef pBeI_nav_ped_transf_det_lote As clsBeI_nav_ped_compra_det_lote,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction)

        Exist = False

        Try

            If pBeI_nav_ped_transf_det_lote.Lot_No IsNot Nothing Then

                Dim vSQL As String = "SELECT Lot_No 
                                      FROM I_nav_ped_compra_det_lote Where(NoEnc = @NoEnc
                                      AND Source_Prod_Order_Line = @Line_No
                                      AND Item_No = @Item_No AND Lot_No=@Lote_No) "

                Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_transf_det_lote.NoEnc))
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LINE_NO", pBeI_nav_ped_transf_det_lote.Source_Prod_Order_Line))
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@ITEM_NO", pBeI_nav_ped_transf_det_lote.Item_No))
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LOTE_NO", pBeI_nav_ped_transf_det_lote.Lot_No))
                    lDTA.SelectCommand.Transaction = pTransaction

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Exist = lDT.Rows.Count > 0

                End Using

            Else
                Throw New Exception(String.Format("La línea de detalle para la ordenden de compra:{0}, no tiene # de línea", pBeI_nav_ped_transf_det_lote.NoEnc))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction,
                                  ByVal pNoPedidoEnc As String) As List(Of clsBeI_nav_ped_compra_det_lote)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_det_lote)

            Const sp As String = "SELECT * FROM i_nav_ped_compra_det_lote 
                Where (NoEnc = @No) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@No", pNoPedidoEnc))

            dad.Fill(dt)

            Dim vBei_nav_ped_compra_det_lote As New clsBeI_nav_ped_compra_det_lote

            For Each dr As DataRow In dt.Rows
                vBei_nav_ped_compra_det_lote = New clsBeI_nav_ped_compra_det_lote
                Cargar(vBei_nav_ped_compra_det_lote, dr)
                lReturnList.Add(vBei_nav_ped_compra_det_lote)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_NoEnc(ByVal NoEnc As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Eliminar_By_NoEnc = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from i_nav_ped_compra_det_lote 
			                       Where(NoEnc = @NoEnc) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)

            End If

            cmd.Parameters.Add(New SqlParameter("@NOENC", NoEnc))

            cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
