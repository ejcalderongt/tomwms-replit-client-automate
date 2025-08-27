Imports System.Data.SqlClient

Public Class clsLnHorario_laboral_det

    Public Sub Cargar(ByRef oBeHorario_laboral_det As clsBeHorario_laboral_det, ByRef dr As DataRow)
        Try
            With oBeHorario_laboral_det
                .IdHorarioLaboralDet = IIf(IsDBNull(dr.Item("IdHorarioLaboralDet")), 0, dr.Item("IdHorarioLaboralDet"))
                .IdHorarioLaboralEnc = IIf(IsDBNull(dr.Item("IdHorarioLaboralEnc")), 0, dr.Item("IdHorarioLaboralEnc"))
                .Dia = IIf(IsDBNull(dr.Item("dia")), 0, dr.Item("dia"))
                .Hora_inicio = IIf(IsDBNull(dr.Item("hora_inicio")), Date.Now, dr.Item("hora_inicio"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .Minimo_min_hora_ingreso = IIf(IsDBNull(dr.Item("minimo_min_hora_ingreso")), 0, dr.Item("minimo_min_hora_ingreso"))
                .Maximo_min_hora_ingreso = IIf(IsDBNull(dr.Item("maximo_min_hora_ingreso")), 0, dr.Item("maximo_min_hora_ingreso"))
                .Minimo_min_hora_salida = IIf(IsDBNull(dr.Item("minimo_min_hora_salida")), 0, dr.Item("minimo_min_hora_salida"))
                .Maximo_min_hora_salida = IIf(IsDBNull(dr.Item("maximo_min_hora_salida")), 0, dr.Item("maximo_min_hora_salida"))
                .Tiempo_retraso_permitido = IIf(IsDBNull(dr.Item("tiempo_retraso_permitido")), 0, dr.Item("tiempo_retraso_permitido"))
                .Horas_extras = IIf(IsDBNull(dr.Item("horas_extras")), False, dr.Item("horas_extras"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Fecha_baja = IIf(IsDBNull(dr.Item("fecha_baja")), Date.Now, dr.Item("fecha_baja"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeHorario_laboral_det As clsBeHorario_laboral_det, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("horario_laboral_det")
            Ins.Add("idhorariolaboraldet", "@idhorariolaboraldet", DataType.Parametro)
            Ins.Add("idhorariolaboralenc", "@idhorariolaboralenc", DataType.Parametro)
            Ins.Add("dia", "@dia", DataType.Parametro)
            Ins.Add("hora_inicio", "@hora_inicio", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("minimo_min_hora_ingreso", "@minimo_min_hora_ingreso", DataType.Parametro)
            Ins.Add("maximo_min_hora_ingreso", "@maximo_min_hora_ingreso", DataType.Parametro)
            Ins.Add("minimo_min_hora_salida", "@minimo_min_hora_salida", DataType.Parametro)
            Ins.Add("maximo_min_hora_salida", "@maximo_min_hora_salida", DataType.Parametro)
            Ins.Add("tiempo_retraso_permitido", "@tiempo_retraso_permitido", DataType.Parametro)
            Ins.Add("horas_extras", "@horas_extras", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("fecha_baja", "@fecha_baja", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALDET", oBeHorario_laboral_det.IdHorarioLaboralDet))
            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_det.IdHorarioLaboralEnc))
            cmd.Parameters.Add(New SqlParameter("@DIA", oBeHorario_laboral_det.Dia))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO", oBeHorario_laboral_det.Hora_inicio))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeHorario_laboral_det.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@MINIMO_MIN_HORA_INGRESO", oBeHorario_laboral_det.Minimo_min_hora_ingreso))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO_MIN_HORA_INGRESO", oBeHorario_laboral_det.Maximo_min_hora_ingreso))
            cmd.Parameters.Add(New SqlParameter("@MINIMO_MIN_HORA_SALIDA", oBeHorario_laboral_det.Minimo_min_hora_salida))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO_MIN_HORA_SALIDA", oBeHorario_laboral_det.Maximo_min_hora_salida))
            cmd.Parameters.Add(New SqlParameter("@TIEMPO_RETRASO_PERMITIDO", oBeHorario_laboral_det.Tiempo_retraso_permitido))
            cmd.Parameters.Add(New SqlParameter("@HORAS_EXTRAS", oBeHorario_laboral_det.Horas_extras))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeHorario_laboral_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeHorario_laboral_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeHorario_laboral_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeHorario_laboral_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_BAJA", oBeHorario_laboral_det.Fecha_baja))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeHorario_laboral_det.Activo))

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

    Public Shared Function Actualizar(ByRef oBeHorario_laboral_det As clsBeHorario_laboral_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("horario_laboral_det")
            Upd.Add("idhorariolaboraldet", "@idhorariolaboraldet", DataType.Parametro)
            Upd.Add("idhorariolaboralenc", "@idhorariolaboralenc", DataType.Parametro)
            Upd.Add("dia", "@dia", DataType.Parametro)
            Upd.Add("hora_inicio", "@hora_inicio", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("minimo_min_hora_ingreso", "@minimo_min_hora_ingreso", DataType.Parametro)
            Upd.Add("maximo_min_hora_ingreso", "@maximo_min_hora_ingreso", DataType.Parametro)
            Upd.Add("minimo_min_hora_salida", "@minimo_min_hora_salida", DataType.Parametro)
            Upd.Add("maximo_min_hora_salida", "@maximo_min_hora_salida", DataType.Parametro)
            Upd.Add("tiempo_retraso_permitido", "@tiempo_retraso_permitido", DataType.Parametro)
            Upd.Add("horas_extras", "@horas_extras", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("fecha_baja", "@fecha_baja", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdHorarioLaboralDet = @IdHorarioLaboralDet")

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

            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALDET", oBeHorario_laboral_det.IdHorarioLaboralDet))
            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALENC", oBeHorario_laboral_det.IdHorarioLaboralEnc))
            cmd.Parameters.Add(New SqlParameter("@DIA", oBeHorario_laboral_det.Dia))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO", oBeHorario_laboral_det.Hora_inicio))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeHorario_laboral_det.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@MINIMO_MIN_HORA_INGRESO", oBeHorario_laboral_det.Minimo_min_hora_ingreso))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO_MIN_HORA_INGRESO", oBeHorario_laboral_det.Maximo_min_hora_ingreso))
            cmd.Parameters.Add(New SqlParameter("@MINIMO_MIN_HORA_SALIDA", oBeHorario_laboral_det.Minimo_min_hora_salida))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO_MIN_HORA_SALIDA", oBeHorario_laboral_det.Maximo_min_hora_salida))
            cmd.Parameters.Add(New SqlParameter("@TIEMPO_RETRASO_PERMITIDO", oBeHorario_laboral_det.Tiempo_retraso_permitido))
            cmd.Parameters.Add(New SqlParameter("@HORAS_EXTRAS", oBeHorario_laboral_det.Horas_extras))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeHorario_laboral_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeHorario_laboral_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeHorario_laboral_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeHorario_laboral_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_BAJA", oBeHorario_laboral_det.Fecha_baja))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeHorario_laboral_det.Activo))

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


    Public Shared Function Eliminar(ByRef oBeHorario_laboral_det As clsBeHorario_laboral_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Horario_laboral_det" &
             "  Where(IdHorarioLaboralDet = @IdHorarioLaboralDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDHORARIOLABORALDET", oBeHorario_laboral_det.IdHorarioLaboralDet))

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

            Const sp As String = "SELECT * FROM Horario_laboral_det"
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

    Public Function Obtener(ByRef oBeHorario_laboral_det As clsBeHorario_laboral_det) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Horario_laboral_det" &
            " Where(IdHorarioLaboralDet = @IdHorarioLaboralDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDHORARIOLABORALDET", oBeHorario_laboral_det.IdHorarioLaboralDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeHorario_laboral_det, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
