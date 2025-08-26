Imports System.Data.SqlClient
Partial Public Class clsLnTrans_manufactura_det

    Public Shared Function GetSingle_By_IdPedidoEnc_and_IdPedidoDet(ByVal pIdPedidoEnc As Integer,
                                                                    ByVal pIdPedidoDet As Integer) As clsBeTrans_manufactura_det

        Dim BeTransManufacturaDet As clsBeTrans_manufactura_det = Nothing

        Try

            Const sp As String = "SELECT d.* 
                                  FROM Trans_manufactura_det d INNER JOIN  
                                       trans_manufactura_enc e ON d.IdManufacturaEnc = e.IdManufacturaEnc
                                  Where(d.IdPedidoDet = @IdPedidoDet AND e.IdPedidoEnc = @IdPedidoEnc)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            BeTransManufacturaDet = New clsBeTrans_manufactura_det

                            Cargar(BeTransManufacturaDet, lDataTable.Rows(0))

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return BeTransManufacturaDet

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Eliminar_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_manufactura_det Where(IdPedidoDet = @pIdPedidoDet)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@pIdPedidoDet", pIdPedidoDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
