Imports System.Data.SqlClient

Public Class clsLnTrans_re_fact

    Public Shared Sub Cargar(ByRef oBeTrans_re_fact As clsBeTrans_re_fact, ByRef dr As DataRow)

        Try

            With oBeTrans_re_fact

                .IdFacturaRecepcion = IIf(IsDBNull(dr.Item("IdFacturaRecepcion")), 0, dr.Item("IdFacturaRecepcion"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .Orden = IIf(IsDBNull(dr.Item("Orden")), 0, dr.Item("Orden"))
                .NoFactura = IIf(IsDBNull(dr.Item("NoFactura")), "", dr.Item("NoFactura"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Completa = IIf(IsDBNull(dr.Item("Completa")), False, dr.Item("Completa"))

            End With

        Catch ex As Exception
            Throw New Exception("CargarTransReFact: " & ex.Message)
        End Try

    End Sub

    Public shared Function Insertar(ByRef oBeTrans_re_fact As clsBeTrans_re_fact, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_fact")
            Ins.Add("idfacturarecepcion", "@idfacturarecepcion", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("orden", "@orden", DataType.Parametro)
            Ins.Add("nofactura", "@nofactura", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("completa", "@completa", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDFACTURARECEPCION", oBeTrans_re_fact.IdFacturaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_fact.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", IIf(oBeTrans_re_fact.Orden = Nothing, DBNull.Value, oBeTrans_re_fact.Orden)))
            cmd.Parameters.Add(New SqlParameter("@NOFACTURA", IIf(oBeTrans_re_fact.NoFactura Is Nothing, DBNull.Value, oBeTrans_re_fact.NoFactura)))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", IIf(oBeTrans_re_fact.Observacion Is Nothing, DBNull.Value, oBeTrans_re_fact.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_fact.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_fact.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_fact.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_fact.User_mod))
            cmd.Parameters.Add(New SqlParameter("@COMPLETA", oBeTrans_re_fact.Completa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBeTrans_re_fact.IdFacturaRecepcion = CInt(cmd.Parameters("@IDFACTURARECEPCION").Value)

        Catch ex As Exception
            Throw New Exception("InsertarTransReFact: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_re_fact As clsBeTrans_re_fact, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_fact")
            Upd.Add("idfacturarecepcion", "@idfacturarecepcion", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("orden", "@orden", DataType.Parametro)
            Upd.Add("nofactura", "@nofactura", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("completa", "@completa", DataType.Parametro)
            Upd.Where("IdFacturaRecepcion = @IdFacturaRecepcion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDFACTURARECEPCION", oBeTrans_re_fact.IdFacturaRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_fact.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@ORDEN", IIf(oBeTrans_re_fact.Orden = Nothing, DBNull.Value, oBeTrans_re_fact.Orden)))
            cmd.Parameters.Add(New SqlParameter("@NOFACTURA", IIf(oBeTrans_re_fact.NoFactura Is Nothing, DBNull.Value, oBeTrans_re_fact.NoFactura)))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", IIf(oBeTrans_re_fact.Observacion Is Nothing, DBNull.Value, oBeTrans_re_fact.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_fact.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_fact.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_fact.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_fact.User_mod))
            cmd.Parameters.Add(New SqlParameter("@COMPLETA", oBeTrans_re_fact.Completa))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_re_fact As clsBeTrans_re_fact, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_re_fact" &
             "  Where(IdFacturaRecepcion = @IdFacturaRecepcion)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDFACTURARECEPCION", oBeTrans_re_fact.IdFacturaRecepcion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("EliminarTransReFact: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_re_fact"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("ListarTransReFact: " & ex.Message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_re_fact As clsBeTrans_re_fact) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Trans_re_fact" &
            " Where(IdFacturaRecepcion = @IdFacturaRecepcion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDFACTURARECEPCION", oBeTrans_re_fact.IdFacturaRecepcion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_fact, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw New Exception("ObtenerTransReFact: " & ex.Message)
        End Try

    End Function

End Class
