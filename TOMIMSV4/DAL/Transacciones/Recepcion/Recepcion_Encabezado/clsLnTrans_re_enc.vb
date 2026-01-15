Imports System.Data.SqlClient

Public Class clsLnTrans_re_enc

    Public Shared Sub Cargar(ByRef oBeTrans_re_enc As clsBeTrans_re_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_re_enc

                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .PropietarioBodega.IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .PropietarioBodega.IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdMuelle = IIf(IsDBNull(dr.Item("IdMuelle")), 0, dr.Item("IdMuelle"))
                .IdUbicacionRecepcion = IIf(IsDBNull(dr.Item("IdUbicacionRecepcion")), 0, dr.Item("IdUbicacionRecepcion"))
                .IdTipoTransaccion = IIf(IsDBNull(dr.Item("IdTipoTransaccion")), "", dr.Item("IdTipoTransaccion"))
                .Fecha_recepcion = IIf(IsDBNull(dr.Item("fecha_recepcion")), Date.Now, dr.Item("fecha_recepcion"))
                .Hora_ini_pc = IIf(IsDBNull(dr.Item("hora_ini_pc")), Date.Now, dr.Item("hora_ini_pc"))
                .Hora_fin_pc = IIf(IsDBNull(dr.Item("hora_fin_pc")), Date.Now, dr.Item("hora_fin_pc"))
                .Muestra_precio = IIf(IsDBNull(dr.Item("muestra_precio")), False, dr.Item("muestra_precio"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Fecha_tarea = IIf(IsDBNull(dr.Item("fecha_tarea")), Date.Now, dr.Item("fecha_tarea"))
                .Tomar_fotos = IIf(IsDBNull(dr.Item("tomar_fotos")), False, dr.Item("tomar_fotos"))
                .Escanear_rec_ubic = IIf(IsDBNull(dr.Item("escanear_rec_ubic")), False, dr.Item("escanear_rec_ubic"))
                .Para_por_codigo = IIf(IsDBNull(dr.Item("para_por_codigo")), False, dr.Item("para_por_codigo"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Firma_piloto = IIf(IsDBNull(dr.Item("firma_piloto")), Nothing, dr.Item("firma_piloto"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .NoGuia = IIf(IsDBNull(dr.Item("NoGuia")), "", dr.Item("NoGuia"))
                .CorreoEnviado = IIf(IsDBNull(dr.Item("CorreoEnviado")), False, dr.Item("CorreoEnviado"))
                .Revision_Inconsistencia = IIf(IsDBNull(dr.Item("Revision_Inconsistencia")), False, dr.Item("Revision_Inconsistencia"))
                .bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .bloqueada_por = IIf(IsDBNull(dr.Item("bloqueada_por")), "", dr.Item("bloqueada_por"))
                .IdUsuarioBloqueo = IIf(IsDBNull(dr.Item("IdUsuarioBloqueo")), 0, dr.Item("IdUsuarioBloqueo"))
                .IdMotivoAnulacionBodega = IIf(IsDBNull(dr.Item("IdMotivoAnulacionBodega")), 0, dr.Item("IdMotivoAnulacionBodega"))
                .Habilitar_Stock = IIf(IsDBNull(dr.Item("Habilitar_Stock")), False, dr.Item("Habilitar_Stock"))
                .IdVehiculo = IIf(IsDBNull(dr.Item("idvehiculo")), 0, dr.Item("idvehiculo"))
                .IdPiloto = IIf(IsDBNull(dr.Item("idpiloto")), 0, dr.Item("idpiloto"))
                .No_Marchamo = IIf(IsDBNull(dr.Item("No_Marchamo")), "", dr.Item("No_Marchamo"))
                .Mostrar_Cantidad_Esperada = IIf(IsDBNull(dr.Item("Mostrar_Cantidad_Esperada")), False, dr.Item("Mostrar_Cantidad_Esperada"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Carta_Cupo = IIf(IsDBNull(dr.Item("carta_cupo")), "", dr.Item("carta_cupo"))
                .No_Contenedor = IIf(IsDBNull(dr.Item("no_contenedor")), "", dr.Item("no_contenedor"))
                .IdEstado_Defecto_Recepcion = IIf(IsDBNull(dr.Item("IdEstado_Defecto_Recepcion")), 0, dr.Item("IdEstado_Defecto_Recepcion"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_re_enc As clsBeTrans_re_enc,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_enc")
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            If Not oBeTrans_re_enc.IdMuelle = 0 Then Ins.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Ins.Add("IdUbicacionRecepcion", "@IdUbicacionRecepcion", DataType.Parametro)
            Ins.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Ins.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Ins.Add("hora_ini_pc", "@hora_ini_pc", DataType.Parametro)
            Ins.Add("hora_fin_pc", "@hora_fin_pc", DataType.Parametro)
            Ins.Add("muestra_precio", "@muestra_precio", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("fecha_tarea", "@fecha_tarea", DataType.Parametro)
            Ins.Add("tomar_fotos", "@tomar_fotos", DataType.Parametro)
            Ins.Add("escanear_rec_ubic", "@escanear_rec_ubic", DataType.Parametro)
            Ins.Add("para_por_codigo", "@para_por_codigo", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("firma_piloto", "@firma_piloto", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("NoGuia", "@NoGuia", DataType.Parametro)
            Ins.Add("CorreoEnviado", "@CorreoEnviado", DataType.Parametro)
            Ins.Add("revision_inconsistencia", "@revision_inconsistencia", DataType.Parametro)
            Ins.Add("bloqueada_por", "@bloqueada_por", DataType.Parametro)
            Ins.Add("IdUsuarioBloqueo", "@IdUsuarioBloqueo", DataType.Parametro)
            Ins.Add("Habilitar_Stock", "@Habilitar_Stock", DataType.Parametro)
            Ins.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Ins.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Ins.Add("no_marchamo", "@no_marchamo", DataType.Parametro)
            Ins.Add("mostrar_cantidad_esperada", "@mostrar_cantidad_esperada", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("carta_cupo", "@carta_cupo", DataType.Parametro)
            Ins.Add("no_contenedor", "@no_contenedor", DataType.Parametro)
            Ins.Add("IdEstado_Defecto_Recepcion", "@IdEstado_Defecto_Recepcion", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_enc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_re_enc.PropietarioBodega.IdPropietarioBodega))
            If Not oBeTrans_re_enc.IdMuelle = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeTrans_re_enc.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacionRecepcion", oBeTrans_re_enc.IdUbicacionRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", IIf(oBeTrans_re_enc.IdTipoTransaccion = "", DBNull.Value, oBeTrans_re_enc.IdTipoTransaccion)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeTrans_re_enc.Fecha_recepcion))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI_PC", oBeTrans_re_enc.Hora_ini_pc))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_PC", oBeTrans_re_enc.Hora_fin_pc))
            cmd.Parameters.Add(New SqlParameter("@MUESTRA_PRECIO", oBeTrans_re_enc.Muestra_precio))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_re_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_TAREA", oBeTrans_re_enc.Fecha_tarea))
            cmd.Parameters.Add(New SqlParameter("@TOMAR_FOTOS", oBeTrans_re_enc.Tomar_fotos))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_REC_UBIC", oBeTrans_re_enc.Escanear_rec_ubic))
            cmd.Parameters.Add(New SqlParameter("@PARA_POR_CODIGO", oBeTrans_re_enc.Para_por_codigo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_re_enc.Observacion))

            If oBeTrans_re_enc.Firma_piloto IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@firma_piloto", oBeTrans_re_enc.Firma_piloto))
            Else
                cmd.Parameters.Add(New SqlParameter("@firma_piloto", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@NoGuia", IIf(oBeTrans_re_enc.NoGuia Is Nothing, DBNull.Value, oBeTrans_re_enc.NoGuia)))
            cmd.Parameters.Add(New SqlParameter("@CorreoEnviado", oBeTrans_re_enc.CorreoEnviado))
            cmd.Parameters.Add(New SqlParameter("@REVISION_INCONSISTENCIA", oBeTrans_re_enc.Revision_Inconsistencia))
            cmd.Parameters.Add(New SqlParameter("@bloqueada_por", oBeTrans_re_enc.bloqueada_por))
            cmd.Parameters.Add(New SqlParameter("@IdUsuarioBloqueo", oBeTrans_re_enc.IdUsuarioBloqueo))
            cmd.Parameters.Add(New SqlParameter("@Habilitar_Stock", oBeTrans_re_enc.Habilitar_Stock))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_re_enc.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_re_enc.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@NO_MARCHAMO", oBeTrans_re_enc.No_Marchamo))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_CANTIDAD_ESPERADA", oBeTrans_re_enc.Mostrar_Cantidad_Esperada))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_re_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CARTA_CUPO", oBeTrans_re_enc.Carta_Cupo))
            cmd.Parameters.Add(New SqlParameter("@NO_CONTENEDOR", oBeTrans_re_enc.No_Contenedor))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO_DEFECTO_RECEPCION", oBeTrans_re_enc.IdEstado_Defecto_Recepcion))

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

    Public Shared Function Actualizar(ByRef oBeTrans_re_enc As clsBeTrans_re_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_enc")
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Upd.Add("IdUbicacionRecepcion", "@IdUbicacionRecepcion", DataType.Parametro)
            Upd.Add("idtipotransaccion", "@idtipotransaccion", DataType.Parametro)
            Upd.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Upd.Add("hora_ini_pc", "@hora_ini_pc", DataType.Parametro)
            Upd.Add("hora_fin_pc", "@hora_fin_pc", DataType.Parametro)
            Upd.Add("muestra_precio", "@muestra_precio", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("fecha_tarea", "@fecha_tarea", DataType.Parametro)
            Upd.Add("tomar_fotos", "@tomar_fotos", DataType.Parametro)
            Upd.Add("escanear_rec_ubic", "@escanear_rec_ubic", DataType.Parametro)
            Upd.Add("para_por_codigo", "@para_por_codigo", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("firma_piloto", "@firma_piloto", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("NoGuia", "@NoGuia", DataType.Parametro)
            Upd.Add("CorreoEnviado", "@CorreoEnviado", DataType.Parametro)
            Upd.Add("revision_inconsistencia", "@revision_inconsistencia", DataType.Parametro)
            Upd.Add("bloqueada_por", "@bloqueada_por", DataType.Parametro)
            Upd.Add("IdUsuarioBloqueo", "@IdUsuarioBloqueo", DataType.Parametro)
            Upd.Add("Habilitar_Stock", "@Habilitar_Stock", DataType.Parametro)
            Upd.Add("idvehiculo", "@idvehiculo", DataType.Parametro)
            Upd.Add("idpiloto", "@idpiloto", DataType.Parametro)
            Upd.Add("no_marchamo", "@no_marchamo", DataType.Parametro)
            Upd.Add("mostrar_cantidad_esperada", "@mostrar_cantidad_esperada", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("carta_cupo", "@carta_cupo", DataType.Parametro)
            Upd.Add("no_contenedor", "@no_contenedor", DataType.Parametro)
            Upd.Add("IdEstado_Defecto_Recepcion", "@IdEstado_Defecto_Recepcion", DataType.Parametro)
            Upd.Where("IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_enc.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_re_enc.PropietarioBodega.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeTrans_re_enc.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacionRecepcion", oBeTrans_re_enc.IdUbicacionRecepcion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTRANSACCION", IIf(oBeTrans_re_enc.IdTipoTransaccion = "", DBNull.Value, oBeTrans_re_enc.IdTipoTransaccion)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", oBeTrans_re_enc.Fecha_recepcion))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI_PC", oBeTrans_re_enc.Hora_ini_pc))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_PC", oBeTrans_re_enc.Hora_fin_pc))
            cmd.Parameters.Add(New SqlParameter("@MUESTRA_PRECIO", oBeTrans_re_enc.Muestra_precio))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_re_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_re_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_re_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@FECHA_TAREA", oBeTrans_re_enc.Fecha_tarea))
            cmd.Parameters.Add(New SqlParameter("@TOMAR_FOTOS", oBeTrans_re_enc.Tomar_fotos))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_REC_UBIC", oBeTrans_re_enc.Escanear_rec_ubic))
            cmd.Parameters.Add(New SqlParameter("@PARA_POR_CODIGO", oBeTrans_re_enc.Para_por_codigo))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", oBeTrans_re_enc.Observacion))

            If oBeTrans_re_enc.Firma_piloto IsNot Nothing Then
                cmd.Parameters.Add(New SqlParameter("@firma_piloto", oBeTrans_re_enc.Firma_piloto))
            Else
                cmd.Parameters.Add(New SqlParameter("@firma_piloto", SqlDbType.Image)).Value = DBNull.Value
            End If

            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_re_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@NOGUIA", IIf(oBeTrans_re_enc.NoGuia Is Nothing, DBNull.Value, oBeTrans_re_enc.NoGuia)))
            cmd.Parameters.Add(New SqlParameter("@CORREOENVIADO", oBeTrans_re_enc.CorreoEnviado))
            cmd.Parameters.Add(New SqlParameter("@REVISION_INCONSISTENCIA", oBeTrans_re_enc.Revision_Inconsistencia))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA_POR", oBeTrans_re_enc.bloqueada_por))
            cmd.Parameters.Add(New SqlParameter("@IDUSUARIOBLOQUEO", oBeTrans_re_enc.IdUsuarioBloqueo))
            cmd.Parameters.Add(New SqlParameter("@HABILITAR_STOCK", oBeTrans_re_enc.Habilitar_Stock))
            cmd.Parameters.Add(New SqlParameter("@IDVEHICULO", oBeTrans_re_enc.IdVehiculo))
            cmd.Parameters.Add(New SqlParameter("@IDPILOTO", oBeTrans_re_enc.IdPiloto))
            cmd.Parameters.Add(New SqlParameter("@NO_MARCHAMO", oBeTrans_re_enc.No_Marchamo))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_CANTIDAD_ESPERADA", oBeTrans_re_enc.Mostrar_Cantidad_Esperada))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_re_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CARTA_CUPO", oBeTrans_re_enc.Carta_Cupo))
            cmd.Parameters.Add(New SqlParameter("@NO_CONTENEDOR", oBeTrans_re_enc.No_Contenedor))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO_DEFECTO_RECEPCION", oBeTrans_re_enc.IdEstado_Defecto_Recepcion))

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

    Public Shared Function Eliminar(ByRef oBeTrans_re_enc As clsBeTrans_re_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text


            Dim sp As String = " Delete from Trans_re_enc" &
             "  Where(IdRecepcionEnc = @IdRecepcionEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_enc.IdRecepcionEnc))

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

    Public Shared Function Obtener(ByRef oBeTrans_re_enc As clsBeTrans_re_enc) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_re_enc" &
            " Where(IdRecepcionEnc = @IdRecepcionEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_enc.IdRecepcionEnc))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_enc, dt.Rows(0))
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
