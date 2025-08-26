Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_enc

    Public Shared Sub Cargar(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_inv_enc

                .Idinventarioenc = IIf(IsDBNull(dr.Item("idinventarioenc")), 0, dr.Item("idinventarioenc"))
                .Idpropietario = IIf(IsDBNull(dr.Item("idpropietario")), 0, dr.Item("idpropietario"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .IdTipoInventario = IIf(IsDBNull(dr.Item("idtipoinventario")), 0, dr.Item("idtipoinventario"))
                .Tipo_Conteo_Producto = IIf(IsDBNull(dr.Item("tipo_conteo_producto")), 0, dr.Item("tipo_conteo_producto"))
                .Doble_verificacion = IIf(IsDBNull(dr.Item("doble_verificacion")), False, dr.Item("doble_verificacion"))
                .Fecha = IIf(IsDBNull(dr.Item("fecha")), Date.Now, dr.Item("fecha"))
                .Fecha_Ultimo_Inventario = IIf(IsDBNull(dr.Item("Fecha_ultimo_inventario")), Date.Now, dr.Item("Fecha_ultimo_inventario"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Inicial = IIf(IsDBNull(dr.Item("inicial")), False, dr.Item("inicial"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Regularizado = IIf(IsDBNull(dr.Item("regularizado")), False, dr.Item("regularizado"))
                .Hora_ini = IIf(IsDBNull(dr.Item("hora_ini")), Date.Now, dr.Item("hora_ini"))
                .Hora_fin = IIf(IsDBNull(dr.Item("hora_fin")), Date.Now, dr.Item("hora_fin"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .EsSistema = IIf(IsDBNull(dr.Item("EsSistema")), False, dr.Item("EsSistema"))
                .Cambia_Ubicacion = IIf(IsDBNull(dr.Item("cambia_ubicacion")), False, dr.Item("cambia_ubicacion"))
                .Mostrar_Cantidad_Teorica_hh = IIf(IsDBNull(dr.Item("mostrar_cantidad_teorica_hh")), False, dr.Item("mostrar_cantidad_teorica_hh"))
                .IdProductoFamilia = IIf(IsDBNull(dr.Item("IdProductoFamilia")), 0, dr.Item("IdProductoFamilia"))
                .IdBodegaVirtual = IIf(IsDBNull(dr.Item("IdBodegaVirtual")), 0, dr.Item("IdBodegaVirtual"))
                .Capturar_no_existente = IIf(IsDBNull(dr.Item("capturar_no_existente")), False, dr.Item("capturar_no_existente"))
                .multi_propietario = IIf(IsDBNull(dr.Item("multi_propietario")), False, dr.Item("multi_propietario"))
                .IdCentroCosto = IIf(IsDBNull(dr.Item("IdCentroCosto")), 0, dr.Item("IdCentroCosto"))
                .Tipo_Asignacion = IIf(IsDBNull(dr.Item("Tipo_Asignacion")), 2, dr.Item("Tipo_Asignacion"))
                .Capturar_No_Asignados = IIf(IsDBNull(dr.Item("Capturar_No_Asignados")), False, dr.Item("Capturar_No_Asignados"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_inv_enc")
            Ins.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idtipoinventario", "@idtipoinventario", DataType.Parametro)
            Ins.Add("tipo_conteo_producto", "@tipo_conteo_producto", DataType.Parametro)
            Ins.Add("doble_verificacion", "@doble_verificacion", DataType.Parametro)
            Ins.Add("fecha", "@fecha", DataType.Parametro)
            Ins.Add("fecha_ultimo_inventario", "@fecha_ultimo_inventario", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("inicial", "@inicial", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("regularizado", "@regularizado", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("essistema", "@essistema", DataType.Parametro)
            Ins.Add("cambia_ubicacion", "@cambia_ubicacion", DataType.Parametro)
            Ins.Add("mostrar_cantidad_teorica_hh", "@mostrar_cantidad_teorica_hh", DataType.Parametro)
            Ins.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            Ins.Add("IdBodegaVirtual", "@IdBodegaVirtual", DataType.Parametro)
            Ins.Add("capturar_no_existente", "@capturar_no_existente", DataType.Parametro)
            Ins.Add("multi_propietario", "@multi_propietario", DataType.Parametro)
            Ins.Add("IdCentroCosto", "@IdCentroCosto", DataType.Parametro)
            Ins.Add("Tipo_Asignacion", "@Tipo_Asignacion", DataType.Parametro)
            Ins.Add("capturar_no_asignados", "@capturar_no_asignados", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_enc.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_inv_enc.Idpropietario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINVENTARIO", oBeTrans_inv_enc.IdTipoInventario))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CONTEO_PRODUCTO", oBeTrans_inv_enc.Tipo_Conteo_Producto))
            cmd.Parameters.Add(New SqlParameter("@DOBLE_VERIFICACION", oBeTrans_inv_enc.Doble_verificacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_inv_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ULTIMO_INVENTARIO", oBeTrans_inv_enc.Fecha_Ultimo_Inventario))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_inv_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@INICIAL", oBeTrans_inv_enc.Inicial))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_inv_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", oBeTrans_inv_enc.Regularizado))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_inv_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_inv_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESSISTEMA", oBeTrans_inv_enc.EsSistema))
            cmd.Parameters.Add(New SqlParameter("@CAMBIA_UBICACION", oBeTrans_inv_enc.Cambia_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_CANTIDAD_TEORICA_HH", oBeTrans_inv_enc.Mostrar_Cantidad_Teorica_hh))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", oBeTrans_inv_enc.IdProductoFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAVIRTUAL", oBeTrans_inv_enc.IdBodegaVirtual))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_NO_EXISTENTE", oBeTrans_inv_enc.Capturar_no_existente))
            cmd.Parameters.Add(New SqlParameter("@MULTI_PROPIETARIO", oBeTrans_inv_enc.multi_propietario))
            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeTrans_inv_enc.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@TIPO_ASIGNACION", oBeTrans_inv_enc.Tipo_Asignacion))
            cmd.Parameters.Add(New SqlParameter("@capturar_no_asignados", oBeTrans_inv_enc.Capturar_No_Asignados))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeTrans_inv_enc.Idinventarioenc = CInt(cmd.Parameters("@IDINVENTARIOENC").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_enc")
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idtipoinventario", "@idtipoinventario", DataType.Parametro)
            Upd.Add("tipo_conteo_producto", "@tipo_conteo_producto", DataType.Parametro)
            Upd.Add("doble_verificacion", "@doble_verificacion", DataType.Parametro)
            Upd.Add("fecha", "@fecha", DataType.Parametro)
            Upd.Add("fecha_ultimo_inventario", "@fecha_ultimo_inventario", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("inicial", "@inicial", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("regularizado", "@regularizado", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("essistema", "@essistema", DataType.Parametro)
            Upd.Add("cambia_ubicacion", "@cambia_ubicacion", DataType.Parametro)
            Upd.Add("mostrar_cantidad_teorica_hh", "@mostrar_cantidad_teorica_hh", DataType.Parametro)
            Upd.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            Upd.Add("IdBodegaVirtual", "@IdBodegaVirtual", DataType.Parametro)
            Upd.Add("capturar_no_existente", "@capturar_no_existente", DataType.Parametro)
            Upd.Add("multi_propietario", "@multi_propietario", DataType.Parametro)
            Upd.Add("IdCentroCosto", "@IdCentroCosto", DataType.Parametro)
            Upd.Add("Tipo_Asignacion", "@Tipo_Asignacion", DataType.Parametro)
            Upd.Add("capturar_no_asignados", "@capturar_no_asignados", DataType.Parametro)
            Upd.Where("idinventarioenc = @idinventarioenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_enc.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_inv_enc.Idpropietario))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_inv_enc.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOINVENTARIO", oBeTrans_inv_enc.IdTipoInventario))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CONTEO_PRODUCTO", oBeTrans_inv_enc.Tipo_Conteo_Producto))
            cmd.Parameters.Add(New SqlParameter("@DOBLE_VERIFICACION", oBeTrans_inv_enc.Doble_verificacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA", oBeTrans_inv_enc.Fecha))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ULTIMO_INVENTARIO", oBeTrans_inv_enc.Fecha_Ultimo_Inventario))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_inv_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@INICIAL", oBeTrans_inv_enc.Inicial))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_inv_enc.Activo))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", oBeTrans_inv_enc.Regularizado))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeTrans_inv_enc.Hora_ini))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeTrans_inv_enc.Hora_fin))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_enc.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_enc.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_inv_enc.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ESSISTEMA", oBeTrans_inv_enc.EsSistema))
            cmd.Parameters.Add(New SqlParameter("@CAMBIA_UBICACION", oBeTrans_inv_enc.Cambia_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@MOSTRAR_CANTIDAD_TEORICA_HH", oBeTrans_inv_enc.Mostrar_Cantidad_Teorica_hh))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", oBeTrans_inv_enc.IdProductoFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAVIRTUAL", oBeTrans_inv_enc.IdBodegaVirtual))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_NO_EXISTENTE", oBeTrans_inv_enc.Capturar_no_existente))
            cmd.Parameters.Add(New SqlParameter("@MULTI_PROPIETARIO", oBeTrans_inv_enc.multi_propietario))
            cmd.Parameters.Add(New SqlParameter("@IDCENTROCOSTO", oBeTrans_inv_enc.IdCentroCosto))
            cmd.Parameters.Add(New SqlParameter("@TIPO_ASIGNACION", oBeTrans_inv_enc.Tipo_Asignacion))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_NO_ASIGNADOS", oBeTrans_inv_enc.Capturar_No_Asignados))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If oBeTrans_inv_enc.Estado = "Anulado" Then

                '#CKFK 20211129 No se actualiza por IdTarea sino por IdTransacción mas el Tipo de tarea
                ''#CKFK 20200115 Agreguó la funcionalidad de que actualice la tarea de inventario a anulada cuando se anule el inventario
                'Dim IdTareaHH As Integer = clsLnTarea_hh.GetIdTarea(oBeTrans_inv_enc.Idinventarioenc, 6, lConnection, lTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_inv_enc.Idinventarioenc, 6, 3, lConnection, lTransaction) 'El IdEstado 3 es Anulado

            End If

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

    Public Shared Function Actualizar_Comparacion_Stock(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("##tempComparacionStock")
            Upd.Add("IdProducto", "@IdProducto", DataType.Parametro)
            Upd.Add("Inventario", "@Inventario", DataType.Parametro)
            Upd.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Upd.Add("Codigo", "@Codigo", DataType.Parametro)
            Upd.Add("Producto", "@Producto", DataType.Parametro)
            Upd.Add("Presentacion", "@Presentacion", DataType.Parametro)
            Upd.Add("Detalle", "@Detalle", DataType.Parametro)
            Upd.Add("Resumen", "@Resumen", DataType.Parametro)
            Upd.Add("Stock", "@Stock", DataType.Parametro)
            Upd.Add("Peso", "@Peso", DataType.Parametro)
            Upd.Where("IdProducto = @IdProducto AND Inventario=@Inventario AND IdPresentacion = @IdPresentacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_enc.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@INVENTARIO", oBeTrans_inv_enc.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_enc.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", IIf(oBeTrans_inv_enc.Codigo Is Nothing, DBNull.Value, oBeTrans_inv_enc.Codigo)))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", IIf(oBeTrans_inv_enc.Producto Is Nothing, DBNull.Value, oBeTrans_inv_enc.Producto)))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", IIf(oBeTrans_inv_enc.Presentacion Is Nothing, DBNull.Value, oBeTrans_inv_enc.Presentacion)))
            cmd.Parameters.Add(New SqlParameter("@DETALLE", oBeTrans_inv_enc.Detalle))
            cmd.Parameters.Add(New SqlParameter("@RESUMEN", oBeTrans_inv_enc.Resumen))
            cmd.Parameters.Add(New SqlParameter("@STOCK", oBeTrans_inv_enc.Stock))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_enc.Peso))


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

    Public Shared Function ActualizarComparacionInventario(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("##tempComparacionInventario")
            Upd.Add("IdInventario ", "@IdInventario ", DataType.Parametro)
            Upd.Add("IdProducto", "@IdProducto", DataType.Parametro)
            Upd.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Upd.Add("IdTramo", "@IdTramo", DataType.Parametro)
            Upd.Add("Tramo", "@Tramo", DataType.Parametro)
            Upd.Add("Codigo", "@Codigo", DataType.Parametro)
            Upd.Add("Producto", "@Producto", DataType.Parametro)
            Upd.Add("Presentacion", "@Presentacion", DataType.Parametro)
            Upd.Add("Detalle", "@Detalle", DataType.Parametro)
            Upd.Add("Resumen", "@Resumen", DataType.Parametro)
            Upd.Add("EstadoDetalle", "@EstadoDetalle", DataType.Parametro)
            Upd.Add("EstadoResumen", "@EstadoResumen", DataType.Parametro)
            Upd.Add("OperadorDetalle", "@OperadorDetalle", DataType.Parametro)
            Upd.Add("OperadorResumen", "@OperadorResumen", DataType.Parametro)
            Upd.Where(" IdInventario=@IdInventario AND IdProducto = @IdProducto  AND IdPresentacion = @IdPresentacion AND IdTramo=@IdTramo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_enc.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_inv_enc.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_enc.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeTrans_inv_enc.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@TRAMO", IIf(oBeTrans_inv_enc.Tramo Is Nothing, DBNull.Value, oBeTrans_inv_enc.Tramo)))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", IIf(oBeTrans_inv_enc.Codigo Is Nothing, DBNull.Value, oBeTrans_inv_enc.Codigo)))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO", IIf(oBeTrans_inv_enc.Producto Is Nothing, DBNull.Value, oBeTrans_inv_enc.Producto)))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", IIf(oBeTrans_inv_enc.Presentacion Is Nothing, DBNull.Value, oBeTrans_inv_enc.Presentacion)))
            cmd.Parameters.Add(New SqlParameter("@DETALLE", oBeTrans_inv_enc.Detalle))
            cmd.Parameters.Add(New SqlParameter("@RESUMEN", oBeTrans_inv_enc.Resumen))
            cmd.Parameters.Add(New SqlParameter("@ESTADODETALLE", IIf(oBeTrans_inv_enc.EstadoDetalle Is Nothing, DBNull.Value, oBeTrans_inv_enc.EstadoDetalle)))
            cmd.Parameters.Add(New SqlParameter("@ESTADORESUMEN", IIf(oBeTrans_inv_enc.EstadoResumen Is Nothing, DBNull.Value, oBeTrans_inv_enc.EstadoResumen)))
            cmd.Parameters.Add(New SqlParameter("@OPERADORDETALLE", IIf(oBeTrans_inv_enc.OperadorConteo Is Nothing, DBNull.Value, oBeTrans_inv_enc.OperadorConteo)))
            cmd.Parameters.Add(New SqlParameter("@OPERADORRESUMEN", IIf(oBeTrans_inv_enc.OperadorVerifica Is Nothing, DBNull.Value, oBeTrans_inv_enc.OperadorVerifica)))


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

    Public Shared Function Eliminar(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_enc" &
             "  Where(idinventarioenc = @idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_enc.Idinventarioenc))

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

            Const sp As String = " Delete from Trans_inv_enc"
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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Trans_inv_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_inv_enc As clsBeTrans_inv_enc) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_enc" &
            " Where(idinventarioenc = @idinventarioenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_enc.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_enc, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_enc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_enc)
            Const sp As String = "SELECT * FROM Trans_inv_enc"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_enc As New clsBeTrans_inv_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_enc = New clsBeTrans_inv_enc
                Cargar(vBeTrans_inv_enc, dr)
                lReturnList.Add(vBeTrans_inv_enc)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_inv_enc As clsBeTrans_inv_enc)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_enc" &
            " Where(idinventarioenc = @idinventarioenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", pBeTrans_inv_enc.IDINVENTARIOENC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_enc, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idinventarioenc),0) FROM Trans_inv_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function



End Class
