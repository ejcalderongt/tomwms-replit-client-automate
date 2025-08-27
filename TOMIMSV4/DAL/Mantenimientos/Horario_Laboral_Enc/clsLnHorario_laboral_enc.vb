Imports System.Data.SqlClient

Public Class clsLnHorario_laboral_enc

    Public Shared Sub Cargar(ByRef oBeHorario_laboral_enc As clsBeHorario_laboral_enc, ByRef dr As DataRow)
        Try
            With oBeHorario_laboral_enc
                .IdHorarioLaboralEnc = IIf(IsDBNull(dr.Item("IdHorarioLaboralEnc")), 0, dr.Item("IdHorarioLaboralEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdJornada = IIf(IsDBNull(dr.Item("IdJornada")), 0, dr.Item("IdJornada"))
                .IdTurno = IIf(IsDBNull(dr.Item("IdTurno")), 0, dr.Item("IdTurno"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeHorario_laboral_enc As clsBeHorario_laboral_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("horario_laboral_enc")
            Ins.Add("idhorariolaboralenc", "@idhorariolaboralenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idjornada", "@idjornada", DataType.Parametro)
            Ins.Add("idturno", "@idturno", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_enc.IdHorarioLaboralEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeHorario_laboral_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeHorario_laboral_enc.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDTURNO", oBeHorario_laboral_enc.IdTurno))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeHorario_laboral_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeHorario_laboral_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeHorario_laboral_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeHorario_laboral_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeHorario_laboral_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeHorario_laboral_enc.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeHorario_laboral_enc As clsBeHorario_laboral_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("horario_laboral_enc")
            Upd.Add("idhorariolaboralenc", "@idhorariolaboralenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idjornada", "@idjornada", DataType.Parametro)
            Upd.Add("idturno", "@idturno", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdHorarioLaboralEnc = @IdHorarioLaboralEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_enc.IdHorarioLaboralEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeHorario_laboral_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADA", oBeHorario_laboral_enc.IdJornada))
            cmd.Parameters.Add(New SqlParameter("@IDTURNO", oBeHorario_laboral_enc.IdTurno))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeHorario_laboral_enc.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeHorario_laboral_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeHorario_laboral_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeHorario_laboral_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeHorario_laboral_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeHorario_laboral_enc.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeHorario_laboral_enc As clsBeHorario_laboral_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Horario_laboral_enc" &
             "  Where(IdHorarioLaboralEnc = @IdHorarioLaboralEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_enc.IdHorarioLaboralEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try
    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Horario_laboral_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeHorario_laboral_enc As clsBeHorario_laboral_enc) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Horario_laboral_enc" &
            " Where(IdHorarioLaboralEnc = @IdHorarioLaboralEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_enc.IdHorarioLaboralEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeHorario_laboral_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
