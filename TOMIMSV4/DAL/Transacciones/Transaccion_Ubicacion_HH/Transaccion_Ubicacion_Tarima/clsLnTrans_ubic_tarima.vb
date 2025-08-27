Imports System.Data.SqlClient

Public Class clsLnTrans_ubic_tarima

    Public Shared Sub Cargar(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima, ByRef dr As DataRow)
        Try
            With oBeTrans_ubic_tarima
                .IdTarimaTareaUbic = IIf(IsDBNull(dr.Item("IdTarimaTareaUbic")), 0, dr.Item("IdTarimaTareaUbic"))
                .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                .IdTarima = IIf(IsDBNull(dr.Item("IdTarima")), 0, dr.Item("IdTarima"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Utilizada = IIf(IsDBNull(dr.Item("Utilizada")), False, dr.Item("Utilizada"))
                .FechaUtilizacion = IIf(IsDBNull(dr.Item("FechaUtilizacion")), Date.Now, dr.Item("FechaUtilizacion"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_ubic_tarima")
            Ins.Add("idtarimatareaubic", "@idtarimatareaubic", DataType.Parametro)
            Ins.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Ins.Add("idtarima", "@idtarima", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("utilizada", "@utilizada", DataType.Parametro)
            Ins.Add("fechautilizacion", "@fechautilizacion", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTARIMATAREAUBIC", oBeTrans_ubic_tarima.IdTarimaTareaUbic))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_tarima.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTARIMA", oBeTrans_ubic_tarima.IdTarima))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_ubic_tarima.Codigo))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTrans_ubic_tarima.Utilizada))
            cmd.Parameters.Add(New SqlParameter("@FECHAUTILIZACION", oBeTrans_ubic_tarima.FechaUtilizacion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_tarima.Fec_mod))

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

    Public Function Actualizar(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_ubic_tarima")
            Upd.Add("idtarimatareaubic", "@idtarimatareaubic", DataType.Parametro)
            Upd.Add("idtareaubicacionenc", "@idtareaubicacionenc", DataType.Parametro)
            Upd.Add("idtarima", "@idtarima", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("utilizada", "@utilizada", DataType.Parametro)
            Upd.Add("fechautilizacion", "@fechautilizacion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdTarimaTareaUbic = @IdTarimaTareaUbic")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTARIMATAREAUBIC", oBeTrans_ubic_tarima.IdTarimaTareaUbic))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_ubic_tarima.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDTARIMA", oBeTrans_ubic_tarima.IdTarima))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeTrans_ubic_tarima.Codigo))
            cmd.Parameters.Add(New SqlParameter("@UTILIZADA", oBeTrans_ubic_tarima.Utilizada))
            cmd.Parameters.Add(New SqlParameter("@FECHAUTILIZACION", oBeTrans_ubic_tarima.FechaUtilizacion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_ubic_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_ubic_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_ubic_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_ubic_tarima.Fec_mod))

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


    Public Function Eliminar(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_ubic_tarima" &
             "  Where(IdTarimaTareaUbic = @IdTarimaTareaUbic)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTARIMATAREAUBIC", oBeTrans_ubic_tarima.IdTarimaTareaUbic))

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

    Public Function Obtener(ByRef oBeTrans_ubic_tarima As clsBeTrans_ubic_tarima) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_ubic_tarima" &
            " Where(IdTarimaTareaUbic = @IdTarimaTareaUbic)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTARIMATAREAUBIC", oBeTrans_ubic_tarima.IdTarimaTareaUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_ubic_tarima, dt.Rows(0))
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


End Class
