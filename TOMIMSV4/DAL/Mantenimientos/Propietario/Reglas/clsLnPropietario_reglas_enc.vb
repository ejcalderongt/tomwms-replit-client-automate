Imports System.Data.SqlClient

Public Class clsLnPropietario_reglas_enc

    Public Shared Sub Cargar(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc, ByRef dr As DataRow)

        Try

            With oBePropietario_reglas_enc

                .IdReglaPropietarioEnc = IIf(IsDBNull(dr.Item("IdReglaPropietarioEnc")), 0, dr.Item("IdReglaPropietarioEnc"))
                .IdReglaRecepcion = IIf(IsDBNull(dr.Item("IdReglaRecepcion")), 0, dr.Item("IdReglaRecepcion"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdMensajeRegla = IIf(IsDBNull(dr.Item("IdMensajeRegla")), 0, dr.Item("IdMensajeRegla"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))

            End With

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Function Insertar(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("propietario_reglas_enc")
            Ins.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Ins.Add("idreglarecepcion", "@idreglarecepcion", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idmensajeregla", "@idmensajeregla", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_enc.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBePropietario_reglas_enc.IdReglaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietario_reglas_enc.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDMENSAJEREGLA", oBePropietario_reglas_enc.IdMensajeRegla))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietario_reglas_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietario_reglas_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietario_reglas_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietario_reglas_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_reglas_enc.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBePropietario_reglas_enc.IdReglaPropietarioEnc = CInt(cmd.Parameters("@IDREGLAPROPIETARIOENC").Value)

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("propietario_reglas_enc")
            Upd.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Upd.Add("idreglarecepcion", "@idreglarecepcion", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idmensajeregla", "@idmensajeregla", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdReglaPropietarioEnc = @IdReglaPropietarioEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_enc.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDREGLARECEPCION", oBePropietario_reglas_enc.IdReglaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietario_reglas_enc.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDMENSAJEREGLA", oBePropietario_reglas_enc.IdMensajeRegla))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietario_reglas_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietario_reglas_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietario_reglas_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietario_reglas_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_reglas_enc.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Propietario_reglas_enc" &
             "  Where(IdReglaPropietarioEnc = @IdReglaPropietarioEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_enc.IdReglaPropietarioEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Propietario_reglas_enc"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_enc_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBePropietario_reglas_enc As clsBePropietario_reglas_enc) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Propietario_reglas_enc" &
            " Where(IdReglaPropietarioEnc = @IdReglaPropietarioEnc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_enc.IdReglaPropietarioEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBePropietario_reglas_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
