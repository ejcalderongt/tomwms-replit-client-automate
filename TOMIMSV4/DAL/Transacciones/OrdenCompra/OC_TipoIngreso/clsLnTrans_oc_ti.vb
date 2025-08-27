Imports System.Data.SqlClient

Public Class clsLnTrans_oc_ti

    Public Shared Sub Cargar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, ByRef dr As DataRow)

        Try

            With oBeTrans_oc_ti

                .IdTipoIngresoOC = IIf(IsDBNull(dr.Item("IdTipoIngresoOC")), 0, dr.Item("IdTipoIngresoOC"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Es_devolucion = IIf(IsDBNull(dr.Item("es_devolucion")), False, dr.Item("es_devolucion"))
                .Control_Poliza = IIf(IsDBNull(dr.Item("Control_Poliza")), False, dr.Item("Control_Poliza"))
                .Requerir_Documento_Ref = IIf(IsDBNull(dr.Item("Requerir_Documento_Ref")), False, dr.Item("Requerir_Documento_Ref"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Es_Poliza_Consolidada = IIf(IsDBNull(dr.Item("Es_Poliza_Consolidada")), False, dr.Item("Es_Poliza_Consolidada"))
                .Genera_Tarea_Ingreso = IIf(IsDBNull(dr.Item("Genera_Tarea_Ingreso")), False, dr.Item("Genera_Tarea_Ingreso"))
                .Requerir_Proveedor_Es_Bodega_WMS = IIf(IsDBNull(dr.Item("Requerir_Proveedor_Es_Bodega_WMS")), False, dr.Item("Requerir_Proveedor_Es_Bodega_WMS"))
                .Requerir_Documento_Ref_WMS = IIf(IsDBNull(dr.Item("Requerir_Documento_Ref_WMS")), False, dr.Item("Requerir_Documento_Ref_WMS"))
                .Requerir_Ubic_Rec_Ingreso = IIf(IsDBNull(dr.Item("Requerir_Ubic_Rec_Ingreso")), False, dr.Item("Requerir_Ubic_Rec_Ingreso"))
                .Exigir_Campo_Referencia = IIf(IsDBNull(dr.Item("Exigir_Campo_Referencia")), False, dr.Item("Exigir_Campo_Referencia"))
                .Marcar_Registros_Enviados_MI3 = IIf(IsDBNull(dr.Item("Marcar_Registros_Enviados_MI3")), False, dr.Item("Marcar_Registros_Enviados_MI3"))
                .Preguntar_En_BackOrder = IIf(IsDBNull(dr.Item("Preguntar_En_BackOrder")), False, dr.Item("Preguntar_En_BackOrder"))
                .Bloquear_Lotes = IIf(IsDBNull(dr.Item("Bloquear_Lotes")), False, dr.Item("Bloquear_Lotes"))
                .Permitir_Excedente_Lotes = IIf(IsDBNull(dr.Item("Permitir_Excedente_Lotes")), False, dr.Item("Permitir_Excedente_Lotes"))
                .Es_Importacion = IIf(IsDBNull(dr.Item("Es_Importacion")), False, dr.Item("Es_Importacion"))
                .Permitir_Vencido_Ingreso = IIf(IsDBNull(dr.Item("permitir_vencido_ingreso")), False, dr.Item("permitir_vencido_ingreso"))
                '#GT25032025: campos para gestion del estado general de una recepci�n
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_oc_ti")
            Ins.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("es_devolucion", "@es_devolucion", DataType.Parametro)
            Ins.Add("control_poliza", "@control_poliza", DataType.Parametro)
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("es_poliza_consolidada", "@es_poliza_consolidada", DataType.Parametro)
            Ins.Add("genera_tarea_ingreso", "@genera_tarea_ingreso", DataType.Parametro)
            Ins.Add("requerir_proveedor_es_bodega_wms", "@requerir_proveedor_es_bodega_wms", DataType.Parametro)
            Ins.Add("requerir_documento_ref_wms", "@requerir_documento_ref_wms", DataType.Parametro)
            Ins.Add("requerir_ubic_rec_ingreso", "@requerir_ubic_rec_ingreso", DataType.Parametro)
            Ins.Add("exigir_campo_referencia", "@exigir_campo_referencia", DataType.Parametro)
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", DataType.Parametro)
            Ins.Add("preguntar_en_backorder", "@preguntar_en_backorder", DataType.Parametro)
            Ins.Add("bloquear_lotes", "@bloquear_lotes", DataType.Parametro)
            Ins.Add("permitir_excedente_lotes", "@permitir_excedente_lotes", DataType.Parametro)
            Ins.Add("es_importacion", "@es_importacion", DataType.Parametro)
            Ins.Add("permitir_vencido_ingreso", "@permitir_vencido_ingreso", DataType.Parametro)
            '#GT25032025: campos para gestionar el estado de la recepci�n 
            Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Ins.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_ti.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_oc_ti.Es_devolucion))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_oc_ti.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF", oBeTrans_oc_ti.Requerir_Documento_Ref))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_ti.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_ti.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_ti.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_ti.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_ti.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_POLIZA_CONSOLIDADA", oBeTrans_oc_ti.Es_Poliza_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@GENERA_TAREA_INGRESO", oBeTrans_oc_ti.Genera_Tarea_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_PROVEEDOR_ES_BODEGA_WMS", oBeTrans_oc_ti.Requerir_Proveedor_Es_Bodega_WMS))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF_WMS", oBeTrans_oc_ti.Requerir_Documento_Ref_WMS))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_UBIC_REC_INGRESO", oBeTrans_oc_ti.Requerir_Ubic_Rec_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@EXIGIR_CAMPO_REFERENCIA", oBeTrans_oc_ti.Exigir_Campo_Referencia))
            cmd.Parameters.Add(New SqlParameter("@MARCAR_REGISTROS_ENVIADOS_MI3", oBeTrans_oc_ti.Marcar_Registros_Enviados_MI3))
            cmd.Parameters.Add(New SqlParameter("@PREGUNTAR_EN_BACKORDER", oBeTrans_oc_ti.Preguntar_En_BackOrder))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEAR_LOTES", oBeTrans_oc_ti.Bloquear_Lotes))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_EXCEDENTE_LOTES", oBeTrans_oc_ti.Permitir_Excedente_Lotes))
            cmd.Parameters.Add(New SqlParameter("@ES_IMPORTACION", oBeTrans_oc_ti.Es_Importacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_VENCIDO_INGRESO", oBeTrans_oc_ti.Permitir_Vencido_Ingreso))
            '#GT25032025: campos para gestionar el estado de la recepcion desde interface
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_oc_ti.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_oc_ti.IdProductoEstado))

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

    Public Shared Function Actualizar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_ti")
            Upd.Add("idtipoingresooc", "@idtipoingresooc", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("es_devolucion", "@es_devolucion", DataType.Parametro)
            Upd.Add("control_poliza", "@control_poliza", DataType.Parametro)
            Upd.Add("requerir_documento_ref", "@requerir_documento_ref", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("es_poliza_consolidada", "@es_poliza_consolidada", DataType.Parametro)
            Upd.Add("genera_tarea_ingreso", "@genera_tarea_ingreso", DataType.Parametro)
            Upd.Add("requerir_proveedor_es_bodega_wms", "@requerir_proveedor_es_bodega_wms", DataType.Parametro)
            Upd.Add("requerir_documento_ref_wms", "@requerir_documento_ref_wms", DataType.Parametro)
            Upd.Add("requerir_ubic_rec_ingreso", "@requerir_ubic_rec_ingreso", DataType.Parametro)
            Upd.Add("exigir_campo_referencia", "@exigir_campo_referencia", DataType.Parametro)
            Upd.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", DataType.Parametro)
            Upd.Add("preguntar_en_backorder", "@preguntar_en_backorder", DataType.Parametro)
            Upd.Add("bloquear_lotes", "@bloquear_lotes", DataType.Parametro)
            Upd.Add("permitir_excedente_lotes", "@permitir_excedente_lotes", DataType.Parametro)
            Upd.Add("es_importacion", "@es_importacion", DataType.Parametro)
            Upd.Add("permitir_vencido_ingreso", "@permitir_vencido_ingreso", DataType.Parametro)

            Upd.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Upd.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)

            Upd.Where("IdTipoIngresoOC = @IdTipoIngresoOC")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_oc_ti.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_oc_ti.Es_devolucion))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_oc_ti.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF", oBeTrans_oc_ti.Requerir_Documento_Ref))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_ti.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_ti.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_ti.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_ti.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_ti.Activo))
            cmd.Parameters.Add(New SqlParameter("@ES_POLIZA_CONSOLIDADA", oBeTrans_oc_ti.Es_Poliza_Consolidada))
            cmd.Parameters.Add(New SqlParameter("@GENERA_TAREA_INGRESO", oBeTrans_oc_ti.Genera_Tarea_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_PROVEEDOR_ES_BODEGA_WMS", oBeTrans_oc_ti.Requerir_Proveedor_Es_Bodega_WMS))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF_WMS", oBeTrans_oc_ti.Requerir_Documento_Ref_WMS))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_UBIC_REC_INGRESO", oBeTrans_oc_ti.Requerir_Ubic_Rec_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@EXIGIR_CAMPO_REFERENCIA", oBeTrans_oc_ti.Exigir_Campo_Referencia))
            cmd.Parameters.Add(New SqlParameter("@MARCAR_REGISTROS_ENVIADOS_MI3", oBeTrans_oc_ti.Marcar_Registros_Enviados_MI3))
            cmd.Parameters.Add(New SqlParameter("@PREGUNTAR_EN_BACKORDER", oBeTrans_oc_ti.Preguntar_En_BackOrder))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEAR_LOTES", oBeTrans_oc_ti.Bloquear_Lotes))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_EXCEDENTE_LOTES", oBeTrans_oc_ti.Permitir_Excedente_Lotes))
            cmd.Parameters.Add(New SqlParameter("@ES_IMPORTACION", oBeTrans_oc_ti.Es_Importacion))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_VENCIDO_INGRESO", oBeTrans_oc_ti.Permitir_Vencido_Ingreso))

            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_oc_ti.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_oc_ti.IdProductoEstado))

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

    Public Shared Function Eliminar(ByRef oBeTrans_oc_ti As clsBeTrans_oc_ti, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " DELETE FROM Trans_oc_ti 
                                 WHERE(IdTipoIngresoOC = @IdTipoIngresoOC)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_oc_ti.IdTipoIngresoOC))

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

End Class