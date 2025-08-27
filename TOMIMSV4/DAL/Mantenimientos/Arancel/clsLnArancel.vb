Imports System.Data.SqlClient

''' <summary>
''' Class clsLnArancel. Logic of Arancel
''' </summary>
Public Class clsLnArancel
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeArancel As clsBeArancel, ByRef dr As DataRow)
        Try
            With oBeArancel
                .IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Porcentaje = IIf(IsDBNull(dr.Item("porcentaje")), 0.0, dr.Item("porcentaje"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Function Insertar(ByRef oBeArancel As clsBeArancel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("arancel")
            Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeArancel.IdArancel))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeArancel.Nombre))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeArancel.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeArancel.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeArancel.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeArancel.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeArancel.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeArancel.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBeArancel.IdArancel = CInt(cmd.Parameters("@IDARANCEL").Value)

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
    Public Shared Function Actualizar(ByRef oBeArancel As clsBeArancel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("arancel")
            Upd.Add("idarancel", "@idarancel", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("porcentaje", "@porcentaje", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdArancel = @IdArancel")

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


            cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeArancel.IdArancel))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeArancel.Nombre))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE", oBeArancel.Porcentaje))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeArancel.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeArancel.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeArancel.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeArancel.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeArancel.Activo))

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

    ''' <summary>
    ''' Eliminars the specified o be arancel.
    ''' </summary>
    ''' <param name="oBeArancel">The o be arancel.</param>
    ''' <param name="pConection">The p conection.</param>
    ''' <param name="pTransaction">The p transaction.</param>
    ''' <returns>Number of rows afected</returns>
    ''' <exception cref="Exception"></exception>
    Public Shared Function Eliminar(ByRef oBeArancel As clsBeArancel, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Arancel" &
             "  Where(IdArancel = @IdArancel)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeArancel.IdArancel))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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
    Public Shared Function Obtener(ByRef oBeArancel As clsBeArancel) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Arancel" &
            " Where(IdArancel = @IdArancel)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDARANCEL", oBeArancel.IdArancel))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeArancel, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
