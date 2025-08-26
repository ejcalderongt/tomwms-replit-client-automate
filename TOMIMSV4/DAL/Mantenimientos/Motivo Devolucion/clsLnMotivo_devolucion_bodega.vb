
Imports System.Data.SqlClient

Public Class clsLnMotivo_devolucion_bodega

    Public Shared Sub Cargar(ByRef oBeMotivo_devolucion_bodega As clsBeMotivo_devolucion_bodega, ByRef dr As DataRow)
        Try
            With oBeMotivo_devolucion_bodega
                .IdMotivoDevolucionBodega = IIf(IsDBNull(dr.Item("IdMotivoDevolucionBodega")), 0, dr.Item("IdMotivoDevolucionBodega"))
                .IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Insertar(ByRef oBeMotivo_devolucion_bodega As clsBeMotivo_devolucion_bodega, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("motivo_devolucion_bodega")
            Ins.Add("idmotivodevolucionbodega", "@idmotivodevolucionbodega", DataType.Parametro)
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCIONBODEGA", oBeMotivo_devolucion_bodega.IdMotivoDevolucionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion_bodega.IdMotivoDevolucion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMotivo_devolucion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_devolucion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_devolucion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_devolucion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_devolucion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_devolucion_bodega.Fec_mod))

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

    Public Function Actualizar(ByRef oBeMotivo_devolucion_bodega As clsBeMotivo_devolucion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("motivo_devolucion_bodega")
            Upd.Add("idmotivodevolucionbodega", "@idmotivodevolucionbodega", DataType.Parametro)
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMotivoDevolucionBodega = @IdMotivoDevolucionBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCIONBODEGA", oBeMotivo_devolucion_bodega.IdMotivoDevolucionBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", oBeMotivo_devolucion_bodega.IdMotivoDevolucion))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeMotivo_devolucion_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeMotivo_devolucion_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeMotivo_devolucion_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeMotivo_devolucion_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeMotivo_devolucion_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeMotivo_devolucion_bodega.Fec_mod))

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

    Public Function Eliminar(ByRef oBeMotivo_devolucion_bodega As clsBeMotivo_devolucion_bodega, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Motivo_devolucion_bodega" &
             "  Where(IdMotivoDevolucionBodega = @IdMotivoDevolucionBodega)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCIONBODEGA", oBeMotivo_devolucion_bodega.IdMotivoDevolucionBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            lConnection.Dispose
            cmd.Dispose
        End Try
    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion_bodega"
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

    Public Function Obtener(ByRef oBeMotivo_devolucion_bodega As clsBeMotivo_devolucion_bodega) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Motivo_devolucion_bodega" &
            " Where(IdMotivoDevolucionBodega = @IdMotivoDevolucionBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCIONBODEGA", oBeMotivo_devolucion_bodega.IdMotivoDevolucionBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMotivo_devolucion_bodega, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
