Imports System.Data.SqlClient

Public Class clsLnPropietario_reglas_det

    Public Shared Sub Cargar(ByRef oBePropietario_reglas_det As clsBePropietario_reglas_det, ByRef dr As DataRow)

        Try

            With oBePropietario_reglas_det
                .IdReglaPropietarioDet = IIf(IsDBNull(dr.Item("IdReglaPropietarioDet")), 0, dr.Item("IdReglaPropietarioDet"))
                .IdReglaPropietarioEnc = IIf(IsDBNull(dr.Item("IdReglaPropietarioEnc")), 0, dr.Item("IdReglaPropietarioEnc"))
                .IdDestinatarioPropietario = IIf(IsDBNull(dr.Item("IdDestinatarioPropietario")), 0, dr.Item("IdDestinatarioPropietario"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_det_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Function Insertar(ByRef oBePropietario_reglas_det As clsBePropietario_reglas_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("propietario_reglas_det")
            Ins.Add("idreglapropietariodet", "@idreglapropietariodet", DataType.Parametro)
            Ins.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Ins.Add("iddestinatariopropietario", "@iddestinatariopropietario", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIODET", oBePropietario_reglas_det.IdReglaPropietarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_det.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_reglas_det.IdDestinatarioPropietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietario_reglas_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietario_reglas_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietario_reglas_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietario_reglas_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_reglas_det.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBePropietario_reglas_det.IdReglaPropietarioDet = CInt(cmd.Parameters("@IDREGLAPROPIETARIODET").Value)

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_det_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBePropietario_reglas_det As clsBePropietario_reglas_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("propietario_reglas_det")
            Upd.Add("idreglapropietariodet", "@idreglapropietariodet", DataType.Parametro)
            Upd.Add("idreglapropietarioenc", "@idreglapropietarioenc", DataType.Parametro)
            Upd.Add("iddestinatariopropietario", "@iddestinatariopropietario", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdReglaPropietarioDet = @IdReglaPropietarioDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIODET", oBePropietario_reglas_det.IdReglaPropietarioDet))
            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIOENC", oBePropietario_reglas_det.IdReglaPropietarioEnc))
            cmd.Parameters.Add(New SqlParameter("@IDDESTINATARIOPROPIETARIO", oBePropietario_reglas_det.IdDestinatarioPropietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBePropietario_reglas_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBePropietario_reglas_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBePropietario_reglas_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBePropietario_reglas_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBePropietario_reglas_det.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_det_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBePropietario_reglas_det As clsBePropietario_reglas_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Propietario_reglas_det" &
             "  Where(IdReglaPropietarioDet = @IdReglaPropietarioDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIODET", oBePropietario_reglas_det.IdReglaPropietarioDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Propietario_reglas_det_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Propietario_reglas_det"

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
            Throw New Exception("Propietario_reglas_det_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBePropietario_reglas_det As clsBePropietario_reglas_det) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Propietario_reglas_det" &
            " Where(IdReglaPropietarioDet = @IdReglaPropietarioDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAPROPIETARIODET", oBePropietario_reglas_det.IdReglaPropietarioDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBePropietario_reglas_det, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
