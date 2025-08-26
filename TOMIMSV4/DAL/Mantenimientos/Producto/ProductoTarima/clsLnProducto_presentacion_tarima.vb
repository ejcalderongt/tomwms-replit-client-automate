Imports System.Data.SqlClient

Public Class clsLnProducto_presentacion_tarima

    Public Shared Sub Cargar(ByRef oBeProducto_presentacion_tarima As clsBeProducto_presentacion_tarima, ByRef dr As DataRow)
        Try
            With oBeProducto_presentacion_tarima
                .IdPresentacionTarima = IIf(IsDBNull(dr.Item("IdPresentacionTarima")), 0, dr.Item("IdPresentacionTarima"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdTipoTarima = IIf(IsDBNull(dr.Item("IdTipoTarima")), 0, dr.Item("IdTipoTarima"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .CantidadPorCama = IIf(IsDBNull(dr.Item("CantidadPorCama")), 0.0, dr.Item("CantidadPorCama"))
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

    Public Shared Function Insertar(ByRef oBeProducto_presentacion_tarima As clsBeProducto_presentacion_tarima, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("producto_presentacion_tarima")
            Ins.Add("idpresentaciontarima", "@idpresentaciontarima", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idtipotarima", "@idtipotarima", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("cantidadporcama", "@cantidadporcama", DataType.Parametro)
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


            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONTARIMA", oBeProducto_presentacion_tarima.IdPresentacionTarima))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion_tarima.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeProducto_presentacion_tarima.IdTipoTarima))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_presentacion_tarima.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADPORCAMA", oBeProducto_presentacion_tarima.CantidadPorCama))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentacion_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentacion_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion_tarima.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion_tarima.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBeProducto_presentacion_tarima.IdPresentacionTarima = CInt(cmd.Parameters("@IDPRESENTACIONTARIMA").Value)

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

    Public Shared Function Actualizar(ByRef oBeProducto_presentacion_tarima As clsBeProducto_presentacion_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("producto_presentacion_tarima")
            Upd.Add("idpresentaciontarima", "@idpresentaciontarima", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idtipotarima", "@idtipotarima", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cantidadporcama", "@cantidadporcama", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdPresentacionTarima = @IdPresentacionTarima")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONTARIMA", oBeProducto_presentacion_tarima.IdPresentacionTarima))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion_tarima.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTARIMA", oBeProducto_presentacion_tarima.IdTipoTarima))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeProducto_presentacion_tarima.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDADPORCAMA", oBeProducto_presentacion_tarima.CantidadPorCama))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentacion_tarima.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentacion_tarima.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion_tarima.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion_tarima.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion_tarima.Activo))

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


    Public Function Eliminar(ByRef oBeProducto_presentacion_tarima As clsBeProducto_presentacion_tarima, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Producto_presentacion_tarima" &
             "  Where(IdPresentacionTarima = @IdPresentacionTarima)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONTARIMA", oBeProducto_presentacion_tarima.IdPresentacionTarima))

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

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_presentacion_tarima"
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
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeProducto_presentacion_tarima As clsBeProducto_presentacion_tarima) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Producto_presentacion_tarima" &
            " Where(IdPresentacionTarima = @IdPresentacionTarima)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACIONTARIMA", oBeProducto_presentacion_tarima.IdPresentacionTarima))
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_presentacion_tarima, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
