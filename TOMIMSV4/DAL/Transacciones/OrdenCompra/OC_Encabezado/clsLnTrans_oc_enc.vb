Imports System.Data.SqlClient

Public Class clsLnTrans_oc_enc

    Public Shared Sub Cargar(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_oc_enc

                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProveedorBodega = IIf(IsDBNull(dr.Item("IdProveedorBodega")), 0, dr.Item("IdProveedorBodega"))
                .IdTipoIngresoOC = IIf(IsDBNull(dr.Item("IdTipoIngresoOC")), 0, dr.Item("IdTipoIngresoOC"))
                .TipoIngreso.IdTipoIngresoOC = .IdTipoIngresoOC
                .IdEstadoOC = IIf(IsDBNull(dr.Item("IdEstadoOC")), 0, dr.Item("IdEstadoOC"))
                .EstadoOC = New clsBeTrans_oc_estado()
                .EstadoOC.IdEstadoOC = .IdEstadoOC
                .IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .Fecha_Creacion = IIf(IsDBNull(dr.Item("Fecha_Creacion")), Date.Now, dr.Item("Fecha_Creacion"))
                .Hora_Creacion = IIf(IsDBNull(dr.Item("Hora_Creacion")), Date.Now, dr.Item("Hora_Creacion"))
                .No_Documento = IIf(IsDBNull(dr.Item("No_Documento")), "", dr.Item("No_Documento"))
                .User_Agr = IIf(IsDBNull(dr.Item("User_Agr")), "", dr.Item("User_Agr"))
                .Fec_Agr = IIf(IsDBNull(dr.Item("Fec_Agr")), Date.Now, dr.Item("Fec_Agr"))
                .User_Mod = IIf(IsDBNull(dr.Item("User_Mod")), "", dr.Item("User_Mod"))
                .Fec_Mod = IIf(IsDBNull(dr.Item("Fec_Mod")), Date.Now, dr.Item("Fec_Mod"))
                .Procedencia = IIf(IsDBNull(dr.Item("Procedencia")), "", dr.Item("Procedencia"))
                .No_Marchamo = IIf(IsDBNull(dr.Item("No_Marchamo")), "", dr.Item("No_Marchamo"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Control_Poliza = IIf(IsDBNull(dr.Item("Control_Poliza")), False, dr.Item("Control_Poliza"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .Fecha_Recepcion = IIf(IsDBNull(dr.Item("Fecha_Recepcion")), Date.Now, dr.Item("Fecha_Recepcion"))
                .Hora_Inicio_Recepcion = IIf(IsDBNull(dr.Item("Hora_Inicio_Recepcion")), Date.Now, dr.Item("Hora_Inicio_Recepcion"))
                .Hora_Fin_Recepcion = IIf(IsDBNull(dr.Item("Hora_Fin_Recepcion")), Date.Now, dr.Item("Hora_Fin_Recepcion"))
                .IdMuelleRecepcion = IIf(IsDBNull(dr.Item("IdMuelleRecepcion")), 0, dr.Item("IdMuelleRecepcion"))
                .Programar_Recepcion = IIf(IsDBNull(dr.Item("Programar_Recepcion")), False, dr.Item("Programar_Recepcion"))
                .IdMotivoAnulacionBodega = IIf(IsDBNull(dr.Item("IdMotivoAnulacionBodega")), 0, dr.Item("IdMotivoAnulacionBodega"))
                .Enviado_A_ERP = IIf(IsDBNull(dr.Item("Enviado_A_ERP")), False, dr.Item("Enviado_A_ERP"))
                .Serie = IIf(IsDBNull(dr.Item("Serie")), "", dr.Item("Serie"))
                .Correlativo = IIf(IsDBNull(dr.Item("Correlativo")), "", dr.Item("Correlativo"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), "0", dr.Item("IdDespachoEnc"))
                .No_Ticket_TMS = IIf(IsDBNull(dr.Item("No_Ticket_TMS")), "0", dr.Item("No_Ticket_TMS"))
                .IdNoDocumentoRef = IIf(IsDBNull(dr.Item("IdNoDocumentoRef")), "0", dr.Item("IdNoDocumentoRef"))
                .IdAcuerdoComercial = IIf(IsDBNull(dr.Item("IdAcuerdoComercial")), "0", dr.Item("IdAcuerdoComercial"))
                .IdOperadorBodegaDefecto = IIf(IsDBNull(dr.Item("IdOperadorBodegaDefecto")), "0", dr.Item("IdOperadorBodegaDefecto"))
                .No_Documento_Recepcion_ERP = IIf(IsDBNull(dr.Item("No_Documento_Recepcion_ERP")), "", dr.Item("No_Documento_Recepcion_ERP"))
                .No_Documento_Devolucion = IIf(IsDBNull(dr.Item("No_Documento_Devolucion")), "", dr.Item("No_Documento_Devolucion"))
                .IdPedidoEncDevolucion = IIf(IsDBNull(dr.Item("IdPedidoEncDevolucion")), "0", dr.Item("IdPedidoEncDevolucion"))
                .Push_To_NAV = IIf(IsDBNull(dr.Item("Push_To_NAV")), False, dr.Item("Push_To_NAV"))
                .No_Documento_Ubicacion_ERP = IIf(IsDBNull(dr.Item("No_Documento_Ubicacion_ERP")), "", dr.Item("No_Documento_Ubicacion_ERP"))
                .PutAway_Registrado = IIf(IsDBNull(dr.Item("PutAway_Registrado")), False, dr.Item("PutAway_Registrado"))
                .Codigo_Empresa_ERP = IIf(IsDBNull(dr.Item("Codigo_Empresa_ERP")), 0, dr.Item("Codigo_Empresa_ERP"))
                .IdCampaña = IIf(IsDBNull(dr.Item("IdCampaña")), 0, dr.Item("IdCampaña"))
                .usr_documento = IIf(IsDBNull(dr.Item("usr_documento")), "", dr.Item("usr_documento"))
                .comentarios = IIf(IsDBNull(dr.Item("comentarios")), "", dr.Item("comentarios"))

            End With

        Catch ex As Exception
            Throw New Exception("Trans_oc_enc_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("trans_oc_enc")
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproveedorbodega", "@idproveedorbodega", DataType.Parametro)
            Ins.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
            Ins.Add("idestadooc", "@idestadooc", DataType.Parametro)
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Ins.Add("fecha_creacion", "@fecha_creacion", DataType.Parametro)
            Ins.Add("hora_creacion", "@hora_creacion", DataType.Parametro)
            Ins.Add("no_documento", "@no_documento", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("procedencia", "@procedencia", DataType.Parametro)
            Ins.Add("no_marchamo", "@no_marchamo", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("control_poliza", "@control_poliza", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("fecha_recepcion", "@fecha_recepcion", DataType.Parametro)
            Ins.Add("hora_inicio_recepcion", "@hora_inicio_recepcion", DataType.Parametro)
            Ins.Add("hora_fin_recepcion", "@hora_fin_recepcion", DataType.Parametro)
            Ins.Add("idmuellerecepcion", "@idmuellerecepcion", DataType.Parametro)
            Ins.Add("programar_recepcion", "@programar_recepcion", DataType.Parametro)
            Ins.Add("enviado_a_erp", "@Enviado_A_ERP", DataType.Parametro)
            Ins.Add("serie", "@serie", DataType.Parametro)
            Ins.Add("correlativo", "@correlativo", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("No_Documento_Devolucion", "@No_Documento_Devolucion", DataType.Parametro)
            Ins.Add("IdPedidoEncDevolucion", "@IdPedidoEncDevolucion", DataType.Parametro)
            Ins.Add("push_to_nav", "@push_to_nav", DataType.Parametro)
            Ins.Add("Codigo_Empresa_ERP", "@Codigo_Empresa_ERP", DataType.Parametro)

            If oBeTrans_oc_enc.IdNoDocumentoRef <> 0 Then Ins.Add("idnodocumentoref", "@idnodocumentoref", DataType.Parametro)
            If oBeTrans_oc_enc.IdAcuerdoComercial <> 0 Then Ins.Add("idacuerdocomercial", "@idacuerdocomercial", DataType.Parametro)
            If oBeTrans_oc_enc.IdOperadorBodegaDefecto <> 0 Then Ins.Add("idoperadorbodegadefecto", "@idoperadorbodegadefecto", DataType.Parametro)
            If oBeTrans_oc_enc.No_Ticket_TMS IsNot Nothing Then Ins.Add("no_ticket_tms", "@no_ticket_tms", DataType.Parametro)
            If oBeTrans_oc_enc.No_Documento_Recepcion_ERP.Trim <> "" Then Ins.Add("no_documento_recepcion_erp", "@no_documento_recepcion_erp", DataType.Parametro)
            If oBeTrans_oc_enc.No_Documento_Ubicacion_ERP.Trim <> "" Then Ins.Add("no_documento_ubicacion_erp", "@no_documento_ubicacion_erp", DataType.Parametro)
            If oBeTrans_oc_enc.IdCampaña <> 0 Then Ins.Add("IdCampaña", "@IdCampaña", DataType.Parametro)
            If oBeTrans_oc_enc.Usr_Documento.Trim <> "" Then Ins.Add("usr_documento", "@usr_documento", DataType.Parametro)
            If oBeTrans_oc_enc.Comentarios.Trim <> "" Then Ins.Add("comentarios", "@comentarios", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", IIf(oBeTrans_oc_enc.PropietarioBodega.IdPropietarioBodega = 0, DBNull.Value, oBeTrans_oc_enc.PropietarioBodega.IdPropietarioBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDORBODEGA", IIf(oBeTrans_oc_enc.IdProveedorBodega = 0, DBNull.Value, oBeTrans_oc_enc.IdProveedorBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", IIf(oBeTrans_oc_enc.IdTipoIngresoOC = 0, DBNull.Value, oBeTrans_oc_enc.IdTipoIngresoOC)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOOC", IIf(oBeTrans_oc_enc.IdEstadoOC = 0, DBNull.Value, oBeTrans_oc_enc.IdEstadoOC)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_oc_enc.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_oc_enc.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CREACION", oBeTrans_oc_enc.Fecha_Creacion))
            cmd.Parameters.Add(New SqlParameter("@HORA_CREACION", oBeTrans_oc_enc.Hora_Creacion))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_oc_enc.No_Documento))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_enc.User_Agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_enc.Fec_Agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_enc.User_Mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_enc.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@PROCEDENCIA", oBeTrans_oc_enc.Procedencia))
            cmd.Parameters.Add(New SqlParameter("@NO_MARCHAMO", oBeTrans_oc_enc.No_Marchamo))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_oc_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_oc_enc.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_oc_enc.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@FECHA_RECEPCION", IIf(oBeTrans_oc_enc.Fecha_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Fecha_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@HORA_INICIO_RECEPCION", IIf(oBeTrans_oc_enc.Hora_Inicio_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Hora_Inicio_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN_RECEPCION", IIf(oBeTrans_oc_enc.Hora_Fin_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Hora_Fin_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLERECEPCION", IIf(oBeTrans_oc_enc.IdMuelleRecepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.IdMuelleRecepcion)))
            cmd.Parameters.Add(New SqlParameter("@PROGRAMAR_RECEPCION", IIf(oBeTrans_oc_enc.Programar_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Programar_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO_A_ERP", IIf(oBeTrans_oc_enc.Enviado_A_ERP = Nothing, False, oBeTrans_oc_enc.Enviado_A_ERP)))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_oc_enc.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTrans_oc_enc.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_oc_enc.IdDespachoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_DEVOLUCION", oBeTrans_oc_enc.No_Documento_Devolucion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCDEVOLUCION", oBeTrans_oc_enc.IdPedidoEncDevolucion))
            cmd.Parameters.Add(New SqlParameter("@PUSH_TO_NAV", oBeTrans_oc_enc.Push_To_NAV))

            If oBeTrans_oc_enc.IdNoDocumentoRef <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDNODOCUMENTOREF ", oBeTrans_oc_enc.IdNoDocumentoRef))
            If oBeTrans_oc_enc.IdAcuerdoComercial <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDACUERDOCOMERCIAL", oBeTrans_oc_enc.IdAcuerdoComercial))
            If oBeTrans_oc_enc.No_Ticket_TMS IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO_TICKET_TMS", oBeTrans_oc_enc.No_Ticket_TMS))
            If oBeTrans_oc_enc.IdOperadorBodegaDefecto <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGADEFECTO", oBeTrans_oc_enc.IdOperadorBodegaDefecto))
            If oBeTrans_oc_enc.No_Documento_Recepcion_ERP.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_RECEPCION_ERP", oBeTrans_oc_enc.No_Documento_Recepcion_ERP))
            If oBeTrans_oc_enc.No_Documento_Ubicacion_ERP.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_UBICACION_ERP", oBeTrans_oc_enc.No_Documento_Ubicacion_ERP))
            If oBeTrans_oc_enc.IdCampaña <> 0 Then cmd.Parameters.Add(New SqlParameter("@IdCampaña", oBeTrans_oc_enc.IdCampaña))
            If oBeTrans_oc_enc.Usr_Documento.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@Usr_Documento", oBeTrans_oc_enc.Usr_Documento))
            If oBeTrans_oc_enc.Comentarios.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@Comentarios", oBeTrans_oc_enc.Comentarios))

            cmd.Parameters.Add(New SqlParameter("@PUTAWAY_REGISTRADO", oBeTrans_oc_enc.PutAway_Registrado))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeTrans_oc_enc.Codigo_Empresa_ERP))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("trans_oc_enc")
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproveedorbodega", "@idproveedorbodega", DataType.Parametro)
            Upd.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
            Upd.Add("idestadooc", "@idestadooc", DataType.Parametro)
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("fecha_creacion", "@fecha_creacion", DataType.Parametro)
            Upd.Add("hora_creacion", "@hora_creacion", DataType.Parametro)
            Upd.Add("no_documento", "@no_documento", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("procedencia", "@procedencia", DataType.Parametro)
            Upd.Add("no_marchamo", "@no_marchamo", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("control_poliza", "@control_poliza", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idmuellerecepcion", "@idmuellerecepcion", DataType.Parametro)
            Upd.Add("programar_recepcion", "@programar_recepcion", DataType.Parametro)
            Upd.Add("Enviado_A_ERP", "@Enviado_A_ERP", DataType.Parametro)
            Upd.Add("serie", "@serie", DataType.Parametro)
            Upd.Add("correlativo", "@correlativo", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("No_Documento_Devolucion", "@No_Documento_Devolucion", DataType.Parametro)
            Upd.Add("IdPedidoEncDevolucion", "@IdPedidoEncDevolucion", DataType.Parametro)
            Upd.Add("push_to_nav", "@push_to_nav", DataType.Parametro)
            Upd.Add("Codigo_Empresa_ERP", "@Codigo_Empresa_ERP", DataType.Parametro)

            If oBeTrans_oc_enc.IdNoDocumentoRef <> 0 Then Upd.Add("idnodocumentoref", "@idnodocumentoref", DataType.Parametro)
            If oBeTrans_oc_enc.No_Ticket_TMS IsNot Nothing Then Upd.Add("no_ticket_tms", "@no_ticket_tms", DataType.Parametro)
            If oBeTrans_oc_enc.IdAcuerdoComercial <> 0 Then Upd.Add("idacuerdocomercial", "@idacuerdocomercial", DataType.Parametro)
            If oBeTrans_oc_enc.IdOperadorBodegaDefecto <> 0 Then Upd.Add("idoperadorbodegadefecto", "@IdOperadorBodegaDefecto", DataType.Parametro)
            If oBeTrans_oc_enc.No_Documento_Recepcion_ERP.Trim <> "" Then Upd.Add("no_documento_recepcion_erp", "@no_documento_recepcion_erp", DataType.Parametro)
            If oBeTrans_oc_enc.No_Documento_Ubicacion_ERP.Trim <> "" Then Upd.Add("no_documento_ubicacion_erp", "@no_documento_ubicacion_erp", DataType.Parametro)
            If oBeTrans_oc_enc.IdCampaña <> 0 Then Upd.Add("IdCampaña", "@IdCampaña", DataType.Parametro)
            If oBeTrans_oc_enc.Usr_Documento.Trim <> "" Then Upd.Add("usr_documento", "@usr_documento", DataType.Parametro)
            If oBeTrans_oc_enc.Comentarios.Trim <> "" Then Upd.Add("comentarios", "@comentarios", DataType.Parametro)

            Upd.Add("putaway_registrado", "@putaway_registrado", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc")

            Dim sp As String = Upd.SQL()

            '#EJC20191205: Trans_Ref02
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_oc_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", IIf(oBeTrans_oc_enc.IdPropietarioBodega = 0, DBNull.Value, oBeTrans_oc_enc.IdPropietarioBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDORBODEGA", IIf(oBeTrans_oc_enc.IdProveedorBodega = 0, DBNull.Value, oBeTrans_oc_enc.IdProveedorBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", IIf(oBeTrans_oc_enc.IdTipoIngresoOC = 0, DBNull.Value, oBeTrans_oc_enc.IdTipoIngresoOC)))
            cmd.Parameters.Add(New SqlParameter("@IDESTADOOC", IIf(oBeTrans_oc_enc.IdEstadoOC = 0, DBNull.Value, oBeTrans_oc_enc.IdEstadoOC)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_oc_enc.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_oc_enc.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_CREACION", oBeTrans_oc_enc.Fecha_Creacion))
            cmd.Parameters.Add(New SqlParameter("@HORA_CREACION", oBeTrans_oc_enc.Hora_Creacion))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", oBeTrans_oc_enc.No_Documento))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_enc.User_Mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_enc.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@PROCEDENCIA", oBeTrans_oc_enc.Procedencia))
            cmd.Parameters.Add(New SqlParameter("@NO_MARCHAMO", oBeTrans_oc_enc.No_Marchamo))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeTrans_oc_enc.Referencia))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_oc_enc.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_oc_enc.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDMUELLERECEPCION", IIf(oBeTrans_oc_enc.IdMuelleRecepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.IdMuelleRecepcion)))
            cmd.Parameters.Add(New SqlParameter("@PROGRAMAR_RECEPCION", IIf(oBeTrans_oc_enc.Programar_Recepcion = Nothing, DBNull.Value, oBeTrans_oc_enc.Programar_Recepcion)))
            cmd.Parameters.Add(New SqlParameter("@Enviado_A_ERP", IIf(oBeTrans_oc_enc.Enviado_A_ERP = Nothing, DBNull.Value, oBeTrans_oc_enc.Enviado_A_ERP)))
            cmd.Parameters.Add(New SqlParameter("@SERIE", oBeTrans_oc_enc.Serie))
            cmd.Parameters.Add(New SqlParameter("@CORRELATIVO", oBeTrans_oc_enc.Correlativo))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", oBeTrans_oc_enc.IdDespachoEnc))
            If oBeTrans_oc_enc.IdNoDocumentoRef <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDNODOCUMENTOREF ", oBeTrans_oc_enc.IdNoDocumentoRef))
            If oBeTrans_oc_enc.No_Ticket_TMS IsNot Nothing Then cmd.Parameters.Add(New SqlParameter("@NO_TICKET_TMS", oBeTrans_oc_enc.No_Ticket_TMS))
            If oBeTrans_oc_enc.IdAcuerdoComercial <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDACUERDOCOMERCIAL", oBeTrans_oc_enc.IdAcuerdoComercial))
            If oBeTrans_oc_enc.IdOperadorBodegaDefecto <> 0 Then cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGADEFECTO", oBeTrans_oc_enc.IdOperadorBodegaDefecto))
            If oBeTrans_oc_enc.No_Documento_Recepcion_ERP.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_RECEPCION_ERP", oBeTrans_oc_enc.No_Documento_Recepcion_ERP))
            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_DEVOLUCION", oBeTrans_oc_enc.No_Documento_Devolucion))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENCDEVOLUCION", oBeTrans_oc_enc.IdPedidoEncDevolucion))
            cmd.Parameters.Add(New SqlParameter("@PUSH_TO_NAV", oBeTrans_oc_enc.Push_To_NAV))
            If oBeTrans_oc_enc.No_Documento_Ubicacion_ERP.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO_UBICACION_ERP", oBeTrans_oc_enc.No_Documento_Ubicacion_ERP))
            cmd.Parameters.Add(New SqlParameter("@PUTAWAY_REGISTRADO", oBeTrans_oc_enc.PutAway_Registrado))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeTrans_oc_enc.Codigo_Empresa_ERP))
            If oBeTrans_oc_enc.IdCampaña <> 0 Then cmd.Parameters.Add(New SqlParameter("@IdCampaña", oBeTrans_oc_enc.IdCampaña))
            If oBeTrans_oc_enc.Usr_Documento.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@Usr_Documento", oBeTrans_oc_enc.Usr_Documento))
            If oBeTrans_oc_enc.Comentarios.Trim <> "" Then cmd.Parameters.Add(New SqlParameter("@Comentarios", oBeTrans_oc_enc.Comentarios))

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

    Public Shared Function Eliminar(ByRef oBeTrans_oc_enc As clsBeTrans_oc_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_enc" &
             "  Where(IdOrdenCompraEnc = @IdOrdenCompraEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_enc.IdOrdenCompraEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_oc_enc "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Pendiente_Registro_MI3() As List(Of clsBeTrans_oc_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeTrans_oc_enc)
            Const sp As String = "SELECT * FROM Trans_oc_enc WHERE ISNULL(Enviado_A_ERP,0) = 0"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_oc_enc As New clsBeTrans_oc_enc

            For Each dr As DataRow In dt.Rows

                vBeTrans_oc_enc = New clsBeTrans_oc_enc
                Cargar(vBeTrans_oc_enc, dr)
                lReturnList.Add(vBeTrans_oc_enc)

            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function get_cantidad_documentos_activos(idBodega As Integer, date1 As Date, date2 As Date) As Integer
        get_cantidad_documentos_activos = 0

        Try

            Dim vSQL As String = "SELECT count(distinct Código) Cant_Ped_Compra
                                  FROM VW_OrdenCompra 
                                  WHERE fecha between @FechaDesde AND @FechaHasta and estado <> 'ANULADA' 
                                  AND IdBodega=@IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", idBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", date1)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", date2)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            get_cantidad_documentos_activos = IIf(IsDBNull(lRow("Cant_Ped_Compra")), 0, lRow("Cant_Ped_Compra"))

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_IdOrdenCompraEnc_By_IdPedidoEnc(IdPedidoEnc As Integer) As Integer

        Get_IdOrdenCompraEnc_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = "SELECT trans_oc_enc.IdOrdenCompraEnc 
                              FROM trans_oc_enc 
                              LEFT OUTER JOIN
                              trans_despacho_det ON trans_oc_enc.IdDespachoEnc = trans_despacho_det.IdDespachoEnc 
                              WHERE trans_despacho_det.IdPedidoEnc = @IdPedidoEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Get_IdOrdenCompraEnc_By_IdPedidoEnc = IIf(IsDBNull(lRow("IdOrdenCompraEnc")), 0, lRow("IdOrdenCompraEnc"))

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_IdOrdenCompraEnc_By_IdPedidoEnc(IdPedidoEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Get_IdOrdenCompraEnc_By_IdPedidoEnc = 0

        Try

            Dim vSQL As String = "SELECT trans_oc_enc.IdOrdenCompraEnc 
                              FROM trans_oc_enc 
                              LEFT OUTER JOIN
                              trans_despacho_det ON trans_oc_enc.IdDespachoEnc = trans_despacho_det.IdDespachoEnc 
                              WHERE trans_despacho_det.IdPedidoEnc = @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Get_IdOrdenCompraEnc_By_IdPedidoEnc = IIf(IsDBNull(lRow("IdOrdenCompraEnc")), 0, lRow("IdOrdenCompraEnc"))

                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

End Class
