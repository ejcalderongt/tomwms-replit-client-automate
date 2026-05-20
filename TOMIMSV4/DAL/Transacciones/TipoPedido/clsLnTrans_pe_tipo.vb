Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsLnTrans_pe_tipo

    Public Shared Sub Cargar(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo, ByRef dr As DataRow)

        Try

            With oBeTrans_pe_tipo

                .IdTipoPedido = IIf(IsDBNull(dr.Item("IdTipoPedido")), 0, dr.Item("IdTipoPedido"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Descripcion = IIf(IsDBNull(dr.Item("Descripcion")), "", dr.Item("Descripcion"))
                .Preparar = IIf(IsDBNull(dr.Item("Preparar")), False, dr.Item("Preparar"))
                .Verificar = IIf(IsDBNull(dr.Item("Verificar")), False, dr.Item("Verificar"))
                .ReservaStock = IIf(IsDBNull(dr.Item("ReservaStock")), False, dr.Item("ReservaStock"))
                .ImprimeBarrasPicking = IIf(IsDBNull(dr.Item("ImprimeBarrasPicking")), False, dr.Item("ImprimeBarrasPicking"))
                .ImprimeBarrasPacking = IIf(IsDBNull(dr.Item("ImprimeBarrasPacking")), False, dr.Item("ImprimeBarrasPacking"))
                .Control_Poliza = IIf(IsDBNull(dr.Item("Control_Poliza")), False, dr.Item("Control_Poliza"))
                .Requerir_Documento_Ref = IIf(IsDBNull(dr.Item("requerir_documento_ref")), False, dr.Item("requerir_documento_ref"))
                .Generar_pedido_ingreso_bodega_destino = IIf(IsDBNull(dr.Item("Generar_pedido_ingreso_bodega_destino")), False, dr.Item("Generar_pedido_ingreso_bodega_destino"))
                .IdTipoIngresoOC = IIf(IsDBNull(dr.Item("IdTipoIngresoOC")), 0, dr.Item("IdTipoIngresoOC"))
                .Trasladar_Lotes_Doc_Ingreso = IIf(IsDBNull(dr.Item("Trasladar_Lotes_Doc_Ingreso")), False, dr.Item("Trasladar_Lotes_Doc_Ingreso"))
                .Requerir_Cliente_Es_Bodega_WMS = IIf(IsDBNull(dr.Item("Requerir_Cliente_Es_Bodega_WMS")), False, dr.Item("Requerir_Cliente_Es_Bodega_WMS"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .Marcar_Registros_Enviados_MI3 = IIf(IsDBNull(dr.Item("Marcar_Registros_Enviados_MI3")), False, dr.Item("Marcar_Registros_Enviados_MI3"))
                .Generar_Recepcion_Auto_Bodega_Destino = IIf(IsDBNull(dr.Item("generar_recepcion_auto_bodega_destino")), False, dr.Item("generar_recepcion_auto_bodega_destino"))
                .Recibir_Producto_Auto_Bodega_Destino = IIf(IsDBNull(dr.Item("recibir_producto_auto_bodega_destino")), False, dr.Item("recibir_producto_auto_bodega_destino"))
                .Control_Cliente_En_Detalle = IIf(IsDBNull(dr.Item("Control_Cliente_En_Detalle")), False, dr.Item("Control_Cliente_En_Detalle"))
                .Permitir_Despacho_Parcial = IIf(IsDBNull(dr.Item("Permitir_Despacho_Parcial")), False, dr.Item("Permitir_Despacho_Parcial"))
                .Permitir_Despacho_Multiple = IIf(IsDBNull(dr.Item("Permitir_Despacho_Multiple")), False, dr.Item("Permitir_Despacho_Multiple"))
                .Fotografia_Verificacion = IIf(IsDBNull(dr.Item("Fotografia_Verificacion")), Date.Now, dr.Item("Fotografia_Verificacion"))
                .Es_Devolucion = IIf(IsDBNull(dr.Item("Es_Devolucion")), False, dr.Item("Es_Devolucion"))
                .Empaque_Tarima = IIf(IsDBNull(dr.Item("Empaque_Tarima")), False, dr.Item("Empaque_Tarima"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Mover_Producto_Zona_Muelle = IIf(IsDBNull(dr.Item("mover_producto_zona_muelle")), False, dr.Item("mover_producto_zona_muelle"))
                .Escanear_Muelle_Picking = IIf(IsDBNull(dr.Item("escanear_muelle_picking")), False, dr.Item("escanear_muelle_picking"))
                .Transferir_Ubicacion = IIf(IsDBNull(dr.Item("transferir_ubicacion")), False, dr.Item("transferir_ubicacion"))
                .Verificar_con_imagen = IIf(IsDBNull(dr.Item("verificar_con_imagen")), False, dr.Item("verificar_con_imagen"))
                .Genera_Guia_Remision = IIf(IsDBNull(dr.Item("genera_guia_remision")), False, dr.Item("genera_guia_remision"))
                .Generar_Picking_Auto = IIf(IsDBNull(dr.Item("generar_picking_auto")), False, dr.Item("generar_picking_auto"))
                .Asignar_Todos_Operadores = IIf(IsDBNull(dr.Item("asignar_todos_operadores")), False, dr.Item("asignar_todos_operadores"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_tipo")
            Ins.Add("idtipopedido", "@idtipopedido", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("preparar", "@preparar", DataType.Parametro)
            Ins.Add("verificar", "@verificar", DataType.Parametro)
            Ins.Add("reservastock", "@reservastock", DataType.Parametro)
            Ins.Add("imprimebarraspicking", "@imprimebarraspicking", DataType.Parametro)
            Ins.Add("imprimebarraspacking", "@imprimebarraspacking", DataType.Parametro)
            Ins.Add("Control_Poliza", "@Control_Poliza", DataType.Parametro)
            Ins.Add("requerir_documento_ref", "@requerir_documento_ref", DataType.Parametro)
            Ins.Add("Generar_pedido_ingreso_bodega_destino", "@Generar_pedido_ingreso_bodega_destino", DataType.Parametro)
            Ins.Add("IdTipoIngresoOC", "@IdTipoIngresoOC", DataType.Parametro)
            Ins.Add("trasladar_lotes_doc_ingreso", "@trasladar_lotes_doc_ingreso", DataType.Parametro)
            Ins.Add("requerir_cliente_es_bodega_wms", "@requerir_cliente_es_bodega_wms", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", DataType.Parametro)
            Ins.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", DataType.Parametro)
            Ins.Add("recibir_producto_auto_bodega_destino", "@recibir_producto_auto_bodega_destino", DataType.Parametro)
            Ins.Add("control_cliente_en_detalle", "@control_cliente_en_detalle", DataType.Parametro)
            Ins.Add("permitir_despacho_parcial", "@permitir_despacho_parcial", DataType.Parametro)
            Ins.Add("permitir_despacho_multiple", "@permitir_despacho_multiple", DataType.Parametro)
            Ins.Add("fotografia_verificacion", "@fotografia_verificacion", DataType.Parametro)
            Ins.Add("Es_Devolucion", "@Es_Devolucion", DataType.Parametro)
            Ins.Add("empaque_tarima", "@empaque_tarima", DataType.Parametro)

            Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Ins.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)

            Ins.Add("mover_producto_zona_muelle", "@mover_producto_zona_muelle", DataType.Parametro)
            Ins.Add("escanear_muelle_picking", "@escanear_muelle_picking", DataType.Parametro)
            Ins.Add("Transferir_Ubicacion", "@Transferir_Ubicacion", DataType.Parametro)
            Ins.Add("Genera_Guia_Remision", "@Genera_Guia_Remision", DataType.Parametro)
            Ins.Add("Verificar_con_imagen", "@Verificar_con_imagen", DataType.Parametro)
            Ins.Add("Asignar_Todos_Operadores", "@Asignar_Todos_Operadores", DataType.Parametro)
            Ins.Add("Generar_Picking_Auto", "@Generar_Picking_Auto", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IdTipoPedido))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_pe_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_pe_tipo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PREPARAR", oBeTrans_pe_tipo.Preparar))
            cmd.Parameters.Add(New SqlParameter("@VERIFICAR", oBeTrans_pe_tipo.Verificar))
            cmd.Parameters.Add(New SqlParameter("@RESERVASTOCK", oBeTrans_pe_tipo.ReservaStock))
            cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPICKING", oBeTrans_pe_tipo.ImprimeBarrasPicking))
            cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPACKING", oBeTrans_pe_tipo.ImprimeBarrasPacking))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_pe_tipo.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF", oBeTrans_pe_tipo.Requerir_Documento_Ref))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PEDIDO_INGRESO_BODEGA_DESTINO", oBeTrans_pe_tipo.Generar_pedido_ingreso_bodega_destino))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_pe_tipo.IdTipoIngresoOC))
            cmd.Parameters.Add(New SqlParameter("@TRASLADAR_LOTES_DOC_INGRESO", oBeTrans_pe_tipo.Trasladar_Lotes_Doc_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_CLIENTE_ES_BODEGA_WMS", oBeTrans_pe_tipo.Requerir_Cliente_Es_Bodega_WMS))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_tipo.Activo))
            cmd.Parameters.Add(New SqlParameter("@MARCAR_REGISTROS_ENVIADOS_MI3", oBeTrans_pe_tipo.Marcar_Registros_Enviados_MI3))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_RECEPCION_AUTO_BODEGA_DESTINO", oBeTrans_pe_tipo.Generar_Recepcion_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@RECIBIR_PRODUCTO_AUTO_BODEGA_DESTINO", oBeTrans_pe_tipo.Recibir_Producto_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_CLIENTE_EN_DETALLE", oBeTrans_pe_tipo.Control_Cliente_En_Detalle))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DESPACHO_PARCIAL", oBeTrans_pe_tipo.Permitir_Despacho_Parcial))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DESPACHO_MULTIPLE", oBeTrans_pe_tipo.Permitir_Despacho_Multiple))
            cmd.Parameters.Add(New SqlParameter("@FOTOGRAFIA_VERIFICACION", oBeTrans_pe_tipo.Fotografia_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_pe_tipo.Es_Devolucion))
            cmd.Parameters.Add(New SqlParameter("@EMPAQUE_TARIMA", oBeTrans_pe_tipo.Empaque_Tarima))

            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_pe_tipo.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_pe_tipo.IdProductoEstado))

            cmd.Parameters.Add(New SqlParameter("@MOVER_PRODUCTO_ZONA_MUELLE", oBeTrans_pe_tipo.Mover_Producto_Zona_Muelle))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_MUELLE_PICKING", oBeTrans_pe_tipo.Escanear_Muelle_Picking))
            cmd.Parameters.Add(New SqlParameter("@TRANSFERIR_UBICACION", oBeTrans_pe_tipo.Transferir_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@GENERA_GUIA_REMISION", oBeTrans_pe_tipo.Genera_Guia_Remision))
            cmd.Parameters.Add(New SqlParameter("@VERIFICAR_CON_IMAGEN", oBeTrans_pe_tipo.Verificar_con_imagen))
            cmd.Parameters.Add(New SqlParameter("@ASIGNAR_TODOS_OPERADORES", oBeTrans_pe_tipo.Asignar_Todos_Operadores))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PICKING_AUTO", oBeTrans_pe_tipo.Generar_Picking_Auto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_tipo")
            Upd.Add("idtipopedido", "@idtipopedido", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("preparar", "@preparar", DataType.Parametro)
            Upd.Add("verificar", "@verificar", DataType.Parametro)
            Upd.Add("reservastock", "@reservastock", DataType.Parametro)
            Upd.Add("imprimebarraspicking", "@imprimebarraspicking", DataType.Parametro)
            Upd.Add("imprimebarraspacking", "@imprimebarraspacking", DataType.Parametro)
            Upd.Add("requerir_documento_ref", "@requerir_documento_ref", DataType.Parametro)
            Upd.Add("Generar_pedido_ingreso_bodega_destino", "@Generar_pedido_ingreso_bodega_destino", DataType.Parametro)
            Upd.Add("IdTipoIngresoOC", "@IdTipoIngresoOC", DataType.Parametro)
            Upd.Add("trasladar_lotes_doc_ingreso", "@trasladar_lotes_doc_ingreso", DataType.Parametro)
            Upd.Add("requerir_cliente_es_bodega_wms", "@requerir_cliente_es_bodega_wms", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("marcar_registros_enviados_mi3", "@marcar_registros_enviados_mi3", DataType.Parametro)
            Upd.Add("generar_recepcion_auto_bodega_destino", "@generar_recepcion_auto_bodega_destino", DataType.Parametro)
            Upd.Add("recibir_producto_auto_bodega_destino", "@recibir_producto_auto_bodega_destino", DataType.Parametro)
            Upd.Add("control_cliente_en_detalle", "@control_cliente_en_detalle", DataType.Parametro)
            Upd.Add("permitir_despacho_parcial", "@permitir_despacho_parcial", DataType.Parametro)
            Upd.Add("permitir_despacho_multiple", "@permitir_despacho_multiple", DataType.Parametro)
            Upd.Add("fotografia_verificacion", "@fotografia_verificacion", DataType.Parametro)
            Upd.Add("es_devolucion", "@es_devolucion", DataType.Parametro)
            Upd.Add("empaque_tarima", "@empaque_tarima", DataType.Parametro)

            Upd.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Upd.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)

            Upd.Add("mover_producto_zona_muelle", "@mover_producto_zona_muelle", DataType.Parametro)
            Upd.Add("escanear_muelle_picking", "@escanear_muelle_picking", DataType.Parametro)

            Upd.Add("transferir_ubicacion", "@transferir_ubicacion", DataType.Parametro)
            Upd.Add("genera_guia_remision", "@Genera_Guia_Remision", DataType.Parametro)
            Upd.Add("Verificar_con_imagen", "@Verificar_con_imagen", DataType.Parametro)
            Upd.Add("Asignar_Todos_Operadores", "@Asignar_Todos_Operadores", DataType.Parametro)
            Upd.Add("Generar_Picking_Auto", "@Generar_Picking_Auto", DataType.Parametro)

            Upd.Where("IdTipoPedido = @IdTipoPedido")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IdTipoPedido))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTrans_pe_tipo.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeTrans_pe_tipo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@PREPARAR", oBeTrans_pe_tipo.Preparar))
            cmd.Parameters.Add(New SqlParameter("@VERIFICAR", oBeTrans_pe_tipo.Verificar))
            cmd.Parameters.Add(New SqlParameter("@RESERVASTOCK", oBeTrans_pe_tipo.ReservaStock))
            cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPICKING", oBeTrans_pe_tipo.ImprimeBarrasPicking))
            cmd.Parameters.Add(New SqlParameter("@IMPRIMEBARRASPACKING", oBeTrans_pe_tipo.ImprimeBarrasPacking))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_POLIZA", oBeTrans_pe_tipo.Control_Poliza))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_DOCUMENTO_REF", oBeTrans_pe_tipo.Requerir_Documento_Ref))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PEDIDO_INGRESO_BODEGA_DESTINO", oBeTrans_pe_tipo.Generar_pedido_ingreso_bodega_destino))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINGRESOOC", oBeTrans_pe_tipo.IdTipoIngresoOC))
            cmd.Parameters.Add(New SqlParameter("@TRASLADAR_LOTES_DOC_INGRESO", oBeTrans_pe_tipo.Trasladar_Lotes_Doc_Ingreso))
            cmd.Parameters.Add(New SqlParameter("@REQUERIR_CLIENTE_ES_BODEGA_WMS", oBeTrans_pe_tipo.Requerir_Cliente_Es_Bodega_WMS))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_pe_tipo.Activo))
            cmd.Parameters.Add(New SqlParameter("@MARCAR_REGISTROS_ENVIADOS_MI3", oBeTrans_pe_tipo.Marcar_Registros_Enviados_MI3))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_RECEPCION_AUTO_BODEGA_DESTINO", oBeTrans_pe_tipo.Generar_Recepcion_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@RECIBIR_PRODUCTO_AUTO_BODEGA_DESTINO", oBeTrans_pe_tipo.Recibir_Producto_Auto_Bodega_Destino))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_CLIENTE_EN_DETALLE", oBeTrans_pe_tipo.Control_Cliente_En_Detalle))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DESPACHO_PARCIAL", oBeTrans_pe_tipo.Permitir_Despacho_Parcial))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_DESPACHO_MULTIPLE", oBeTrans_pe_tipo.Permitir_Despacho_Multiple))
            cmd.Parameters.Add(New SqlParameter("@FOTOGRAFIA_VERIFICACION", oBeTrans_pe_tipo.Fotografia_Verificacion))
            cmd.Parameters.Add(New SqlParameter("@ES_DEVOLUCION", oBeTrans_pe_tipo.Es_Devolucion))
            cmd.Parameters.Add(New SqlParameter("@EMPAQUE_TARIMA", oBeTrans_pe_tipo.Empaque_Tarima))

            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_pe_tipo.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_pe_tipo.IdProductoEstado))

            cmd.Parameters.Add(New SqlParameter("@MOVER_PRODUCTO_ZONA_MUELLE", oBeTrans_pe_tipo.Mover_Producto_Zona_Muelle))
            cmd.Parameters.Add(New SqlParameter("@ESCANEAR_MUELLE_PICKING", oBeTrans_pe_tipo.Escanear_Muelle_Picking))
            cmd.Parameters.Add(New SqlParameter("@TRANSFERIR_UBICACION", oBeTrans_pe_tipo.Transferir_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@GENERA_GUIA_REMISION", oBeTrans_pe_tipo.Genera_Guia_Remision))
            cmd.Parameters.Add(New SqlParameter("@VERIFICAR_CON_IMAGEN", oBeTrans_pe_tipo.Verificar_con_imagen))
            cmd.Parameters.Add(New SqlParameter("@ASIGNAR_TODOS_OPERADORES", oBeTrans_pe_tipo.Asignar_Todos_Operadores))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_PICKING_AUTO", oBeTrans_pe_tipo.Generar_Picking_Auto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_tipo Where(IdTipoPedido = @IdTipoPedido)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IdTipoPedido))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_tipo "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_pe_tipo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_pe_tipo" &
            " Where(IdTipoPedido = @IdTipoPedido)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IDTIPOPEDIDO))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_pe_tipo, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_pe_tipo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_pe_tipo)
            Const sp As String = "SELECT * FROM Trans_pe_tipo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_pe_tipo As New clsBeTrans_pe_tipo

            For Each dr As DataRow In dt.Rows

                vBeTrans_pe_tipo = New clsBeTrans_pe_tipo
                Cargar(vBeTrans_pe_tipo, dr)
                lReturnList.Add(vBeTrans_pe_tipo)

            Next

            lConnection.Dispose()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_pe_tipo As clsBeTrans_pe_tipo) As Boolean


        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_pe_tipo Where(IdTipoPedido = @IdTipoPedido)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", pBeTrans_pe_tipo.IdTipoPedido))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_pe_tipo, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoPedido),0) FROM Trans_pe_tipo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class