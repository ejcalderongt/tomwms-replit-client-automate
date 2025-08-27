Imports System.Data.SqlClient

Public Class clsLnTrans_re_img

    Public Shared Sub Cargar(ByRef oBeTrans_re_img As clsBeTrans_re_img, ByRef dr As DataRow)
        Try
            With oBeTrans_re_img
                .IdImagen = IIf(IsDBNull(dr.Item("IdImagen")), 0, dr.Item("IdImagen"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .Imagen = IIf(IsDBNull(dr.Item("Imagen")), Nothing, dr.Item("Imagen"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public shared Function Insertar(ByRef oBeTrans_re_img As clsBeTrans_re_img, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_img")
            Ins.Add("idimagen", "@idimagen", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMAGEN", oBeTrans_re_img.IdImagen))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_img.IdRecepcionEnc))

            If oBeTrans_re_img.Imagen IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeTrans_re_img.Imagen))
            Else
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_img.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_img.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_re_img.Observacion))

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

    Public Shared Function Actualizar(ByRef oBeTrans_re_img As clsBeTrans_re_img, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_img")
            Upd.Add("idimagen", "@idimagen", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Where("IdImagen = @IdImagen " &
                "AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMAGEN", oBeTrans_re_img.IdImagen))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_img.IdRecepcionEnc))

            If oBeTrans_re_img.Imagen IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeTrans_re_img.Imagen))
            Else
                cmd.Parameters.Add(New SqlParameter("@IMAGEN", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_img.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_img.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_re_img.Observacion))

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

    Public Shared Function Eliminar(ByRef oBeTrans_re_img As clsBeTrans_re_img, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_re_img" &
             "  Where(IdImagen = @IdImagen) " &
             "  AND (IdRecepcionEnc = @IdRecepcionEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMAGEN", oBeTrans_re_img.IdImagen))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_img.IdRecepcionEnc))

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

    Public Shared Function Obtener(ByRef oBeTrans_re_img As clsBeTrans_re_img) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_re_img" &
            " Where(IdImagen = @IdImagen) " &
            "AND (IdRecepcionEnc = @IdRecepcionEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMAGEN", oBeTrans_re_img.IdImagen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_img.IdRecepcionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_img, dt.Rows(0))
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
