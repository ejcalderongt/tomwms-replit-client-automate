Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ajuste_det_doc

    Public Shared Sub Cargar(ByRef oBeTrans_ajuste_det_doc As clsBeTrans_ajuste_det_doc, ByRef dr As DataRow)
        Try
            With oBeTrans_ajuste_det_doc
                .Idajustedoc = IIf(IsDBNull(dr.Item("idajustedoc")), 0, dr.Item("idajustedoc"))
                .Idajusteenc = IIf(IsDBNull(dr.Item("idajusteenc")), 0, dr.Item("idajusteenc"))
                .Documento = IIf(IsDBNull(dr.Item("documento")), "", dr.Item("documento"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function Insertar(ByRef oBeTrans_ajuste_det_doc As clsBeTrans_ajuste_det_doc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_ajuste_det_doc")
            Ins.Add("idajustedoc", "@idajustedoc", DataType.Parametro)
            Ins.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Ins.Add("documento", "@documento", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDOC", oBeTrans_ajuste_det_doc.Idajustedoc))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det_doc.Idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO", oBeTrans_ajuste_det_doc.Documento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeTrans_ajuste_det_doc As clsBeTrans_ajuste_det_doc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_ajuste_det_doc")
            Upd.Add("idajustedoc", "@idajustedoc", DataType.Parametro)
            Upd.Add("idajusteenc", "@idajusteenc", DataType.Parametro)
            Upd.Add("documento", "@documento", DataType.Parametro)
            Upd.Where("idajustedoc = @idajustedoc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEDOC", oBeTrans_ajuste_det_doc.Idajustedoc))
            cmd.Parameters.Add(New SqlParameter("@IDAJUSTEENC", oBeTrans_ajuste_det_doc.Idajusteenc))
            cmd.Parameters.Add(New SqlParameter("@DOCUMENTO", oBeTrans_ajuste_det_doc.Documento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idajustedoc),0) FROM Trans_ajuste_det_doc"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idajustedoc),0) FROM Trans_ajuste_det_doc"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
