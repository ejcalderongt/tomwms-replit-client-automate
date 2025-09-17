Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnLog_error_wms

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If lConnection.State = ConnectionState.Open Then

                Dim oBeLog_error_wms As New clsBeLog_error_wms()
                oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1

                If pMensajeExcepcion.Length > 2000 Then
                    pMensajeExcepcion = pMensajeExcepcion.Substring(1, 2000)
                End If

                oBeLog_error_wms.MensajeError = pMensajeExcepcion
                oBeLog_error_wms.Fecha = Now
                oBeLog_error_wms.IdEmpresa = 0
                oBeLog_error_wms.IdBodega = 0
                Insertar(oBeLog_error_wms)

            End If

            If Not lTransaction.Connection Is Nothing Then lTransaction.Commit()

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            If ex.Message.StartsWith("Insertar String or binary data would be truncated") Then
                Agregar_Error(vMsgError)
                Throw New Exception(pMensajeExcepcion)
            Else
                Throw ex
            End If

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Sub Agregar_Error(ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pMensajeExcepcion As String)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim oBeLog_error_wms As New clsBeLog_error_wms()
            oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1
            oBeLog_error_wms.MensajeError = pMensajeExcepcion
            oBeLog_error_wms.Fecha = Now
            oBeLog_error_wms.IdEmpresa = pIdEmpresa
            oBeLog_error_wms.IdBodega = pIdBodega
            Insertar(oBeLog_error_wms)

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            If ex.Message.StartsWith("Insertar String or binary data would be truncated") Then
                Agregar_Error(vMsgError)
                Throw New Exception(pMensajeExcepcion)
            Else
                Throw ex
            End If
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdError),0) FROM Log_error_wms"

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

    Public Shared Sub Agregar_Error(ByVal pMensajeExcepcion As String,
                                    ByVal lConnection As SqlConnection,
                                    ByVal lTransaction As SqlTransaction)

        Try

            Dim oBeLog_error_wms As New clsBeLog_error_wms()
            oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1
            oBeLog_error_wms.MensajeError = pMensajeExcepcion
            oBeLog_error_wms.Fecha = Now
            oBeLog_error_wms.IdEmpresa = 0
            oBeLog_error_wms.IdBodega = 0
            Insertar(oBeLog_error_wms)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            Throw ex

        End Try

    End Sub

    Public Shared Sub Agregar_Error(ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pMensajeExcepcion As String,
                                    ByVal pIdPedidoEnc As Integer,
                                    ByVal pIdPIckingEnc As Integer,
                                    ByVal pIdRecepcionEnc As Integer,
                                    ByVal pIdUsuarioAgr As Integer)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim oBeLog_error_wms As New clsBeLog_error_wms()
            oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1
            oBeLog_error_wms.MensajeError = pMensajeExcepcion
            oBeLog_error_wms.Fecha = Now
            oBeLog_error_wms.IdEmpresa = pIdEmpresa
            oBeLog_error_wms.IdBodega = pIdBodega
            oBeLog_error_wms.IdPedidoEnc = pIdPedidoEnc
            oBeLog_error_wms.IdPickingEnc = pIdPIckingEnc
            oBeLog_error_wms.IdRecepcionEnc = pIdRecepcionEnc
            oBeLog_error_wms.IdUsuarioAgr = pIdUsuarioAgr
            Insertar(oBeLog_error_wms)

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            If ex.Message.StartsWith("Insertar String or binary data would be truncated") Then
                Agregar_Error(vMsgError)
                Throw New Exception(pMensajeExcepcion)
            Else
                Throw ex
            End If
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub


    Public Shared Sub Agregar_Error(ByVal pIdEmpresa As Integer,
                                    ByVal pIdBodega As Integer,
                                    ByVal pMensajeExcepcion As String,
                                    ByVal pIdPedidoEnc As Integer,
                                    ByVal pIdPIckingEnc As Integer,
                                    ByVal pIdRecepcionEnc As Integer,
                                    ByVal pIdUsuarioAgr As Integer,
                                    ByVal lConnection As SqlConnection,
                                    ByVal lTransaction As SqlTransaction)

        Try

            Dim oBeLog_error_wms As New clsBeLog_error_wms()
            oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1
            oBeLog_error_wms.MensajeError = pMensajeExcepcion
            oBeLog_error_wms.Fecha = Now
            oBeLog_error_wms.IdEmpresa = pIdEmpresa
            oBeLog_error_wms.IdBodega = pIdBodega
            oBeLog_error_wms.IdPedidoEnc = pIdPedidoEnc
            oBeLog_error_wms.IdPickingEnc = pIdPIckingEnc
            oBeLog_error_wms.IdRecepcionEnc = pIdRecepcionEnc
            oBeLog_error_wms.IdUsuarioAgr = pIdUsuarioAgr
            Insertar(oBeLog_error_wms)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Agregar_Log_Pedido(ByVal pBeStockAReservar As clsBeStock_res,
                                         ByVal pNombreEscenario As String)

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If lConnection.State = ConnectionState.Open Then

                Dim oBeLog_error_wms As New clsBeLog_error_wms()
                oBeLog_error_wms.IdError = MaxID(lConnection, lTransaction) + 1
                oBeLog_error_wms.MensajeError = pNombreEscenario
                oBeLog_error_wms.Fecha = Now
                oBeLog_error_wms.IdEmpresa = 0
                oBeLog_error_wms.IdBodega = pBeStockAReservar.IdBodega
                oBeLog_error_wms.Cantidad = pBeStockAReservar.Cantidad
                oBeLog_error_wms.Variant_Code = pBeStockAReservar.IdPresentacion
                oBeLog_error_wms.IdUsuarioAgr = IIf(pBeStockAReservar.User_agr = "", 0, Val(pBeStockAReservar.User_agr))
                oBeLog_error_wms.IdRecepcionEnc = pBeStockAReservar.IdRecepcion
                oBeLog_error_wms.IdPedidoEnc = pBeStockAReservar.IdPedido
                oBeLog_error_wms.Item_No = pBeStockAReservar.IdProductoBodega
                oBeLog_error_wms.UmBas = pBeStockAReservar.IdUnidadMedida

                Insertar(oBeLog_error_wms)

            End If

            If Not lTransaction.Connection Is Nothing Then lTransaction.Commit()

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

End Class
