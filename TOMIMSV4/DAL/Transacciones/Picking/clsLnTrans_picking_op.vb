Imports System.Data.SqlClient

Public Class clsLnTrans_picking_op

    Public Shared Sub Cargar(ByRef oBeTrans_picking_op As clsBeTrans_picking_op, ByRef dr As DataRow)
        Try
            With oBeTrans_picking_op
                .IdOperadorPicking = IIf(IsDBNull(dr.Item("IdOperadorPicking")), 0, dr.Item("IdOperadorPicking"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw New Exception("Trans_picking_op_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_op As clsBeTrans_picking_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_picking_op")
            Ins.Add("idoperadorpicking", "@idoperadorpicking", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDOPERADORPICKING", oBeTrans_picking_op.IdOperadorPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_op.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_picking_op.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_op.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_picking_op As clsBeTrans_picking_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_picking_op")
            Upd.Add("idoperadorpicking", "@idoperadorpicking", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdOperadorPicking = @IdOperadorPicking")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADORPICKING", oBeTrans_picking_op.IdOperadorPicking))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_op.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_picking_op.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_op.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_picking_op As clsBeTrans_picking_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_picking_op" &
             "  Where(IdOperadorPicking = @IdOperadorPicking)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADORPICKING", oBeTrans_picking_op.IdOperadorPicking))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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
