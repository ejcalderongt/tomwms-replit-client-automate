Imports System.Data.SqlClient

'The test
'The other test
Public Class clsLnTransacciones_log

    Public Shared Sub Cargar(ByRef oBeTransacciones_log As clsBeTransacciones_log, ByRef dr As DataRow)
        Try
            With oBeTransacciones_log
                .IdTransaccionLog = IIf(IsDBNull(dr.Item("IdTransaccionLog")), 0, dr.Item("IdTransaccionLog"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdObservacion = IIf(IsDBNull(dr.Item("IdObservacion")), 0, dr.Item("IdObservacion"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .Cantidad_reabasto = IIf(IsDBNull(dr.Item("cantidad_reabasto")), 0.0, dr.Item("cantidad_reabasto"))
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

    Public Function Insertar(ByRef oBeTransacciones_log As clsBeTransacciones_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("transacciones_log")
            Ins.Add("idtransaccionlog", "@idtransaccionlog", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idobservacion", "@idobservacion", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("cantidad_reabasto", "@cantidad_reabasto", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONLOG", oBeTransacciones_log.IdTransaccionLog))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTransacciones_log.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTransacciones_log.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOBSERVACION", oBeTransacciones_log.IdObservacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTransacciones_log.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTransacciones_log.IdPresentacion = 0, DBNull.Value, oBeTransacciones_log.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeTransacciones_log.IdProductoEstado = 0, DBNull.Value, oBeTransacciones_log.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTransacciones_log.IdUnidadMedida = 0, DBNull.Value, oBeTransacciones_log.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTransacciones_log.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_REABASTO", oBeTransacciones_log.Cantidad_reabasto))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTransacciones_log.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTransacciones_log.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTransacciones_log.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTransacciones_log.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTransacciones_log.Activo))

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

    Public Function Actualizar(ByRef oBeTransacciones_log As clsBeTransacciones_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("transacciones_log")
            Upd.Add("idtransaccionlog", "@idtransaccionlog", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idobservacion", "@idobservacion", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("cantidad_reabasto", "@CANTIDAD_REABASTO", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdTransaccionLog = @IdTransaccionLog " &
                "AND IdEmpresa = @IdEmpresa " &
                "AND IdPropietarioBodega = @IdPropietarioBodega " &
                "AND IdObservacion = @IdObservacion")

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

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONLOG", oBeTransacciones_log.IdTransaccionLog))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTransacciones_log.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTransacciones_log.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOBSERVACION", oBeTransacciones_log.IdObservacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTransacciones_log.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTransacciones_log.IdPresentacion = 0, DBNull.Value, oBeTransacciones_log.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeTransacciones_log.IdProductoEstado = 0, DBNull.Value, oBeTransacciones_log.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTransacciones_log.IdUnidadMedida = 0, DBNull.Value, oBeTransacciones_log.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTransacciones_log.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_REABASTO", oBeTransacciones_log.Cantidad_reabasto))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTransacciones_log.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTransacciones_log.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTransacciones_log.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTransacciones_log.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTransacciones_log.Activo))

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


    Public Function Eliminar(ByRef oBeTransacciones_log As clsBeTransacciones_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Transacciones_log" &
             "  Where(IdTransaccionLog = @IdTransaccionLog)" &
             "  AND (IdEmpresa = @IdEmpresa)" &
             "  AND (IdPropietarioBodega = @IdPropietarioBodega)" &
             "  AND (IdObservacion = @IdObservacion)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#20191205_Trans_Ref: Transacción_Local_Agregada
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRANSACCIONLOG", oBeTransacciones_log.IdTransaccionLog))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTransacciones_log.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTransacciones_log.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOBSERVACION", oBeTransacciones_log.IdObservacion))

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


    Public Function Obtener(ByRef oBeTransacciones_log As clsBeTransacciones_log) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Transacciones_log" &
            " Where(IdTransaccionLog = @IdTransaccionLog)" &
            "AND (IdEmpresa = @IdEmpresa)" &
            "AND (IdPropietarioBodega = @IdPropietarioBodega)" &
            "AND (IdObservacion = @IdObservacion)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRANSACCIONLOG", oBeTransacciones_log.IdTransaccionLog))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeTransacciones_log.IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTransacciones_log.IdPropietarioBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOBSERVACION", oBeTransacciones_log.IdObservacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTransacciones_log, dt.Rows(0))
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
