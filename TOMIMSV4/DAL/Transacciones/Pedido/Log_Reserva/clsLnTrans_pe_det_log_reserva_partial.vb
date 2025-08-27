Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_pe_det_log_reserva

    Public Shared Sub Agregar_Log_Reserva(ByVal pBeStockAReservar As clsBeStock_res,
                                          ByVal pNombreEscenario As String,
                                          ByVal pMensajeLog As String)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If lConnection.State = ConnectionState.Open Then

                Dim oBeLog_reserva_pedido As New clsBeTrans_pe_det_log_reserva()
                oBeLog_reserva_pedido.IdLogReserva = MaxID(lConnection, lTransaction) + 1
                oBeLog_reserva_pedido.MensajeLog = pMensajeLog
                oBeLog_reserva_pedido.Caso_Reserva = pNombreEscenario
                oBeLog_reserva_pedido.Fecha = Now
                oBeLog_reserva_pedido.IdBodega = pBeStockAReservar.IdBodega
                oBeLog_reserva_pedido.Cantidad = pBeStockAReservar.Cantidad
                oBeLog_reserva_pedido.Variant_Code = pBeStockAReservar.IdPresentacion
                oBeLog_reserva_pedido.IdPedidoEnc = pBeStockAReservar.IdPedido
                oBeLog_reserva_pedido.IdPedidoDet = pBeStockAReservar.IdPedidoDet
                oBeLog_reserva_pedido.Item_No = clsLnProducto.Get_Codigo_By_IdProductoBodega(pBeStockAReservar.IdProductoBodega, lConnection, lTransaction)
                oBeLog_reserva_pedido.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(pBeStockAReservar.IdUnidadMedida, lConnection, lTransaction)
                oBeLog_reserva_pedido.Fecha_Vence = pBeStockAReservar.Fecha_vence
                oBeLog_reserva_pedido.IdStock = pBeStockAReservar.IdStock
                oBeLog_reserva_pedido.IdStockRes = pBeStockAReservar.IdStockRes

                If Not Existe_By_Parametros(pBeStockAReservar.IdPedido,
                                            pBeStockAReservar.IdPedidoDet,
                                            pBeStockAReservar.IdStock,
                                            pBeStockAReservar.Cantidad,
                                            pNombreEscenario,
                                            lConnection,
                                            lTransaction) Then

                    Insertar(oBeLog_reserva_pedido, lConnection, lTransaction)

                End If

            End If

            If Not lTransaction Is Nothing Then lTransaction.Commit()

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw New Exception(vMsgError)

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Sub Agregar_Log_Reserva(ByVal pBeStockAReservar As clsBeStock_res,
                                          ByVal pNombreEscenario As String,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Try

            If lConnection.State = ConnectionState.Open Then

                Dim oBeLog_reserva_pedido As New clsBeTrans_pe_det_log_reserva()
                oBeLog_reserva_pedido.IdLogReserva = MaxID(lConnection, lTransaction) + 1
                oBeLog_reserva_pedido.MensajeLog = pNombreEscenario
                oBeLog_reserva_pedido.Fecha = Now
                oBeLog_reserva_pedido.IdBodega = pBeStockAReservar.IdBodega
                oBeLog_reserva_pedido.Cantidad = pBeStockAReservar.Cantidad
                oBeLog_reserva_pedido.Variant_Code = pBeStockAReservar.IdPresentacion
                oBeLog_reserva_pedido.IdPedidoEnc = pBeStockAReservar.IdPedido
                oBeLog_reserva_pedido.IdPedidoDet = pBeStockAReservar.IdPedidoDet
                oBeLog_reserva_pedido.Item_No = clsLnProducto.Get_Codigo_By_IdProductoBodega(pBeStockAReservar.IdProductoBodega, lConnection, lTransaction)
                oBeLog_reserva_pedido.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(pBeStockAReservar.IdUnidadMedida, lConnection, lTransaction)
                oBeLog_reserva_pedido.Fecha_Vence = pBeStockAReservar.Fecha_vence
                oBeLog_reserva_pedido.IdStock = pBeStockAReservar.IdStock
                oBeLog_reserva_pedido.IdStockRes = pBeStockAReservar.IdStockRes

                If Not Existe_By_Parametros(pBeStockAReservar.IdPedido,
                                            pBeStockAReservar.IdPedidoDet,
                                            pBeStockAReservar.IdStock,
                                            pBeStockAReservar.Cantidad,
                                            pNombreEscenario,
                                            lConnection,
                                            lTransaction) Then

                    Insertar(oBeLog_reserva_pedido, lConnection, lTransaction)

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw New Exception(vMsgError)
        End Try

    End Sub
    Public Shared Sub Existe_By_Parametros(ByVal IdPedidoEnc As Integer,
                                           ByVal IdPedidoDet As Integer,
                                           ByVal IdStock As Integer,
                                           ByVal Cantidad As Double)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva" &
                                 " Where(IdPedidoEnc = @IdPedidoEnc AND IdPedidoDet = @IdPedidoDet AND IdStock = @IdStock AND Cantidad = @Cantidad)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_pe_det_log_reserva As New clsBeTrans_pe_det_log_reserva

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_pe_det_log_reserva, lDataTable.Rows(0))
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
    Public Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdLogReserva),0) FROM trans_pe_det_log_reserva"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Existe_By_Parametros(ByVal IdPedidoEnc As Integer,
                                                ByVal IdPedidoDet As Integer,
                                                ByVal IdStock As Integer,
                                                ByVal Cantidad As Double,
                                                ByVal Caso_Reserva As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Boolean

        Existe_By_Parametros = False

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det_log_reserva" &
                                 " WHERE(IdPedidoEnc = @IdPedidoEnc 
                                   AND IdPedidoDet = @IdPedidoDet 
                                   AND IdStock = @IdStock AND Cantidad = @Cantidad AND Caso_Reserva = @Caso_Reserva)"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad)
                lDTA.SelectCommand.Parameters.AddWithValue("@Caso_Reserva", Caso_Reserva)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_By_Parametros = True
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
