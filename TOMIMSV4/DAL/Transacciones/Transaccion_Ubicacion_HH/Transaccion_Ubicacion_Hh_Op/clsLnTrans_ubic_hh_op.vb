Imports System.Data.SqlClient

Public Class clsLnTrans_ubic_hh_op

    Public Shared Sub Cargar(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op, ByRef dr As DataRow)
        Try
            With oBeTrans_ubic_hh_op
                .IdTransUbicHhOp = IIf(IsDBNull(dr.Item("IdTransUbicHhOp")), 0, dr.Item("IdTransUbicHhOp"))
                .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_ubic_hh_op")
            Ins.Add("idtransubichhop", "@idtransubichhop", DataType.Parametro)
            Ins.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSUBICHHOP", oBeTrans_ubic_hh_op.IdTransUbicHhOp))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_op.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_hh_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_hh_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_op.Fec_mod))

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
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_ubic_hh_op")
            Upd.Add("idtransubichhop", "@idtransubichhop", DataType.Parametro)
            Upd.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdTransUbicHhOp = @IdTransUbicHhOp " &
                      "AND IdTareaUbicacionEnc = @IdTareaUbicacionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSUBICHHOP", oBeTrans_ubic_hh_op.IdTransUbicHhOp))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_op.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_hh_op.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_hh_op.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_op.Fec_mod))

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
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_ubic_hh_op" &
             "  Where(IdTransUbicHhOp = @IdTransUbicHhOp)" &
             "  AND (IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSUBICHHOP", oBeTrans_ubic_hh_op.IdTransUbicHhOp))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTareaUbicacionEnc))

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
            cmd.Dispose()
        End Try

    End Function

    '#CKFK20230126 Agregué esta función para eliminar todos los operadores de una tarea de ubicación
    Public Shared Function Eliminar_Operadores_By_IdTareaUbicacionEnc(ByVal pIdTareaUbicacionEnc As Integer,
                                                               Optional ByVal pConection As SqlConnection = Nothing,
                                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_ubic_hh_op" &
             "  Where (IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", pIdTareaUbicacionEnc))

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
            cmd.Dispose()
        End Try

    End Function

    Public Function Obtener(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_ubic_hh_op" &
            " Where(IdTransUbicHhOp = @IdTransUbicHhOp)" &
            "AND (IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRANSUBICHHOP", oBeTrans_ubic_hh_op.IdTransUbicHhOp))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTransUbicHhOp))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_ubic_hh_op, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = 0

            Dim sp As String = "SELECT ISNULL(Max(IdTransUbicHhOp),0) FROM trans_ubic_hh_op"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
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

    Public Shared Function Actualizar_Operador(ByRef oBeTrans_ubic_hh_op As clsBeTrans_ubic_hh_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_ubic_hh_op")
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdTransUbicHhOp = @IdTransUbicHhOp " &
                      "AND IdTareaUbicacionEnc = @IdTareaUbicacionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSUBICHHOP", oBeTrans_ubic_hh_op.IdTransUbicHhOp))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_hh_op.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_ubic_hh_op.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_hh_op.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_hh_op.Fec_mod))

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
            cmd.Dispose()
        End Try

    End Function

End Class
