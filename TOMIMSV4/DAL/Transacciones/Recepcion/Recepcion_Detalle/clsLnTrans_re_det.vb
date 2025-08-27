Imports System.Data.SqlClient

Public Class clsLnTrans_re_det

    '#EJC20180113: Agregué atributo_variante_1 en clase de clsLnTrans_re_det
    Public Shared Sub Cargar(ByRef oBeTrans_re_det As clsBeTrans_re_det, ByRef dr As DataRow)

        Try

            With oBeTrans_re_det

                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IDPRODUCTOBODEGA")), 0, dr.Item("IDPRODUCTOBODEGA"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado")) '#CKFK 20181114_0337PM Agregué este dato porque no lo estaba llenando
                .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                .MotivoDevolucion.IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
                .cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Nombre_presentacion = IIf(IsDBNull(dr.Item("nombre_presentacion")), "", dr.Item("nombre_presentacion"))
                .Nombre_unidad_medida = IIf(IsDBNull(dr.Item("nombre_unidad_medida")), "", dr.Item("nombre_unidad_medida"))
                .Nombre_producto_estado = IIf(IsDBNull(dr.Item("nombre_producto_estado")), "", dr.Item("nombre_producto_estado"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Peso_Estadistico = IIf(IsDBNull(dr.Item("peso_estadistico")), 0.0, dr.Item("peso_estadistico"))
                .Peso_Minimo = IIf(IsDBNull(dr.Item("peso_minimo")), 0.0, dr.Item("peso_minimo"))
                .Peso_Maximo = IIf(IsDBNull(dr.Item("peso_maximo")), 0.0, dr.Item("peso_maximo"))
                .peso_unitario = IIf(IsDBNull(dr.Item("peso_unitario")), 0.0, dr.Item("peso_unitario"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Observacion = IIf(IsDBNull(dr.Item("observacion")), "", dr.Item("observacion"))
                .Aniada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                .Costo_Oc = IIf(IsDBNull(dr.Item("costo_oc")), 0.0, dr.Item("costo_oc"))
                .Costo_Estadistico = IIf(IsDBNull(dr.Item("costo_estadistico")), 0.0, dr.Item("costo_estadistico"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), 0.0, dr.Item("codigo_producto"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Pallet_No_Estandar = IIf(IsDBNull(dr.Item("pallet_no_estandar")), False, dr.Item("pallet_no_estandar"))
                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdOrdenCompraDet = IIf(IsDBNull(dr.Item("IdOrdenCompraDet")), 0, dr.Item("IdOrdenCompraDet"))
                .IdJornadaSistema = IIf(IsDBNull(dr.Item("IdJornadaSistema")), 0, dr.Item("IdJornadaSistema"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_re_det As clsBeTrans_re_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("trans_re_det")
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("nombre_presentacion", "@nombre_presentacion", DataType.Parametro)
            Ins.Add("nombre_unidad_medida", "@nombre_unidad_medida", DataType.Parametro)
            Ins.Add("nombre_producto_estado", "@nombre_producto_estado", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("peso_estadistico", "@peso_estadistico", DataType.Parametro)
            Ins.Add("peso_minimo", "@peso_minimo", DataType.Parametro)
            Ins.Add("peso_maximo", "@peso_maximo", DataType.Parametro)
            Ins.Add("peso_unitario", "@peso_unitario", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("observacion", "@observacion", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("costo_oc", "@costo_oc", DataType.Parametro)
            Ins.Add("costo_estadistico", "@costo_estadistico", DataType.Parametro)
            Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Ins.Add("IdOrdenCompraEnc", "@IdOrdenCompraEnc", DataType.Parametro)
            Ins.Add("IdOrdenCompraDet", "@IdOrdenCompraDet", DataType.Parametro)
            Ins.Add("IdJornadaSistema", "@IdJornadaSistema", DataType.Parametro)
            Ins.Add("Host", "@Host", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_re_det.IdPresentacion = 0, DBNull.Value, oBeTrans_re_det.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTrans_re_det.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_re_det.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeTrans_re_det.ProductoEstado.IdEstado = 0, DBNull.Value, oBeTrans_re_det.ProductoEstado.IdEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeTrans_re_det.IdOperadorBodega = 0, DBNull.Value, oBeTrans_re_det.IdOperadorBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_re_det.MotivoDevolucion.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_re_det.MotivoDevolucion.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_re_det.No_Linea))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_re_det.cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", IIf(oBeTrans_re_det.Nombre_producto Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRESENTACION", IIf(oBeTrans_re_det.Nombre_presentacion Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_presentacion)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD_MEDIDA", IIf(oBeTrans_re_det.Nombre_unidad_medida Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_unidad_medida)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO_ESTADO", IIf(oBeTrans_re_det.Nombre_producto_estado Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_producto_estado)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_re_det.Lote Is Nothing, DBNull.Value, oBeTrans_re_det.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_re_det.Fecha_vence = Nothing, DBNull.Value, oBeTrans_re_det.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeTrans_re_det.Fecha_ingreso = Nothing, DBNull.Value, oBeTrans_re_det.Fecha_ingreso)))
            cmd.Parameters.Add(New SqlParameter("@PESO", IIf(oBeTrans_re_det.Peso = Nothing, DBNull.Value, oBeTrans_re_det.Peso)))
            cmd.Parameters.Add(New SqlParameter("@PESO_ESTADISTICO", IIf(oBeTrans_re_det.Peso_Estadistico = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Estadistico)))
            cmd.Parameters.Add(New SqlParameter("@PESO_MINIMO", IIf(oBeTrans_re_det.Peso_Minimo = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Minimo)))
            cmd.Parameters.Add(New SqlParameter("@PESO_MAXIMO", IIf(oBeTrans_re_det.Peso_Maximo = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Maximo)))
            cmd.Parameters.Add(New SqlParameter("@PESO_UNITARIO", IIf(oBeTrans_re_det.peso_unitario = Nothing, DBNull.Value, oBeTrans_re_det.peso_unitario)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", IIf(oBeTrans_re_det.Observacion Is Nothing, DBNull.Value, oBeTrans_re_det.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@añada", IIf(oBeTrans_re_det.Aniada = Nothing, DBNull.Value, oBeTrans_re_det.Aniada)))
            cmd.Parameters.Add(New SqlParameter("@COSTO", IIf(oBeTrans_re_det.Costo = Nothing, DBNull.Value, oBeTrans_re_det.Costo)))
            cmd.Parameters.Add(New SqlParameter("@COSTO_OC", IIf(oBeTrans_re_det.Costo_Oc = Nothing, DBNull.Value, oBeTrans_re_det.Costo_Oc)))
            cmd.Parameters.Add(New SqlParameter("@COSTO_ESTADISTICO", IIf(oBeTrans_re_det.Costo_Estadistico = Nothing, DBNull.Value, oBeTrans_re_det.Costo_Estadistico)))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", IIf(oBeTrans_re_det.Atributo_Variante_1 = String.Empty, DBNull.Value, oBeTrans_re_det.Atributo_Variante_1)))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_re_det.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_re_det.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeTrans_re_det.Pallet_No_Estandar))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_re_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_re_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADASISTEMA", oBeTrans_re_det.IdJornadaSistema))
            cmd.Parameters.Add(New SqlParameter("@HOST", oBeTrans_re_det.Host))

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

    Public Shared Function Actualizar(ByRef oBeTrans_re_det As clsBeTrans_re_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_det")
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("IDPRODUCTOBODEGA", "@IDPRODUCTOBODEGA", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("nombre_presentacion", "@nombre_presentacion", DataType.Parametro)
            Upd.Add("nombre_unidad_medida", "@nombre_unidad_medida", DataType.Parametro)
            Upd.Add("nombre_producto_estado", "@nombre_producto_estado", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("peso_unitario", "@peso_unitario", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("observacion", "@observacion", DataType.Parametro)
            Upd.Add("añada", "@añada", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("costo_oc", "@costo_oc", DataType.Parametro)
            Upd.Add("costo_estadistico", "@costo_estadistico", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Upd.Add("IdOrdenCompraEnc", "@IdOrdenCompraEnc", DataType.Parametro)
            Upd.Add("IdOrdenCompraDet", "@IdOrdenCompraDet", DataType.Parametro)
            Upd.Add("IdJornadaSistema", "@IdJornadaSistema", DataType.Parametro)
            Upd.Where("IdRecepcionDet = @IdRecepcionDet " &
                "AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_re_det.IdProductoBodega))
            'cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_re_det.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_re_det.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_re_det.IdPresentacion = 0, DBNull.Value, oBeTrans_re_det.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeTrans_re_det.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_re_det.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeTrans_re_det.ProductoEstado.IdEstado = 0, DBNull.Value, oBeTrans_re_det.ProductoEstado.IdEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", IIf(oBeTrans_re_det.IdOperadorBodega = 0, DBNull.Value, oBeTrans_re_det.IdOperadorBodega)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_re_det.MotivoDevolucion.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_re_det.MotivoDevolucion.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_re_det.No_Linea))
            cmd.Parameters.Add(New SqlParameter("@cantidad_recibida", oBeTrans_re_det.cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", IIf(oBeTrans_re_det.Nombre_producto Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRESENTACION", IIf(oBeTrans_re_det.Nombre_presentacion Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_presentacion)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD_MEDIDA", IIf(oBeTrans_re_det.Nombre_unidad_medida Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_unidad_medida)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO_ESTADO", IIf(oBeTrans_re_det.Nombre_producto_estado Is Nothing, DBNull.Value, oBeTrans_re_det.Nombre_producto_estado)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_re_det.Lote Is Nothing, DBNull.Value, oBeTrans_re_det.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_re_det.Fecha_vence = Nothing, DBNull.Value, oBeTrans_re_det.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeTrans_re_det.Fecha_ingreso = Nothing, DBNull.Value, oBeTrans_re_det.Fecha_ingreso)))
            cmd.Parameters.Add(New SqlParameter("@PESO", IIf(oBeTrans_re_det.Peso = Nothing, DBNull.Value, oBeTrans_re_det.Peso)))
            cmd.Parameters.Add(New SqlParameter("@PESO_UNITARIO", IIf(oBeTrans_re_det.peso_unitario = Nothing, DBNull.Value, oBeTrans_re_det.peso_unitario)))
            cmd.Parameters.Add(New SqlParameter("@PESO_ESTADISTICO", IIf(oBeTrans_re_det.Peso_Estadistico = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Estadistico)))
            cmd.Parameters.Add(New SqlParameter("@PESO_MINIMO", IIf(oBeTrans_re_det.Peso_Minimo = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Minimo)))
            cmd.Parameters.Add(New SqlParameter("@PESO_MAXIMO", IIf(oBeTrans_re_det.Peso_Maximo = Nothing, DBNull.Value, oBeTrans_re_det.Peso_Maximo)))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_re_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_re_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@OBSERVACION", IIf(oBeTrans_re_det.Observacion Is Nothing, DBNull.Value, oBeTrans_re_det.Observacion)))
            cmd.Parameters.Add(New SqlParameter("@añada", IIf(oBeTrans_re_det.Aniada = Nothing, DBNull.Value, oBeTrans_re_det.Aniada)))
            cmd.Parameters.Add(New SqlParameter("@COSTO", IIf(oBeTrans_re_det.Costo = Nothing, DBNull.Value, oBeTrans_re_det.Costo)))
            cmd.Parameters.Add(New SqlParameter("@COSTO_OC", IIf(oBeTrans_re_det.Costo_Oc = Nothing, DBNull.Value, oBeTrans_re_det.Costo_Oc)))
            cmd.Parameters.Add(New SqlParameter("@COSTO_ESTADISTICO", IIf(oBeTrans_re_det.Costo_Estadistico = Nothing, DBNull.Value, oBeTrans_re_det.Costo_Estadistico)))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", IIf(oBeTrans_re_det.Atributo_Variante_1 = String.Empty, DBNull.Value, oBeTrans_re_det.Atributo_Variante_1)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_re_det.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeTrans_re_det.Pallet_No_Estandar))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_re_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_re_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDJORNADASISTEMA", oBeTrans_re_det.IdJornadaSistema))

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

    Public Shared Function Eliminar(ByRef oBeTrans_re_det As clsBeTrans_re_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Trans_re_det" &
             "  Where(IdRecepcionDet = @IdRecepcionDet) " &
             "  AND (IdRecepcionEnc = @IdRecepcionEnc)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))

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

    Public Shared Function Obtener(ByRef oBeTrans_re_det As clsBeTrans_re_det) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Trans_re_det 
            Where(IdRecepcionDet = @IdRecepcionDet) 
            AND (IdRecepcionEnc = @IdRecepcionEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_det, dt.Rows(0))
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

    '#CKFK 20181114_0147PM creó esta función transaccional
    Public Shared Function Obtener(ByRef oBeTrans_re_det As clsBeTrans_re_det,
                                   ByRef pConnection As SqlConnection,
                                   ByRef pTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = "SELECT * FROM Trans_re_det Where(IdRecepcionDet = @IdRecepcionDet) AND (IdRecepcionEnc = @IdRecepcionEnc)"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_re_det, dt.Rows(0))
                Obtener = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByRef oBeTrans_re_det As clsBeTrans_re_det,
                                  ByVal pConection As SqlConnection,
                                  ByVal pTransaction As SqlTransaction) As Boolean


        Existe = False

        Try

            Dim sp As String = "SELECT * FROM Trans_re_det 
                                WHERE(IdRecepcionDet = @IdRecepcionDet) 
                                AND (IdRecepcionEnc = @IdRecepcionEnc) AND (IdOperadorBodega=@IdOperadorBodega ) "

            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                sp += " AND LIC_PLATE = @LIC_PLATE "
            End If

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_re_det.IdOperadorBodega))
            If Not oBeTrans_re_det.Lic_plate.Trim = "" Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_re_det.Lic_plate))
            End If

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Existe = (dt.Rows.Count = 1)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteLP(ByVal IdRecepcionEnc As Integer, ByVal LicPlate As String, ByVal IdRecepcionDet As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        ExisteLP = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = " SELECT lic_plate FROM stock_rec  
                                 WHERE (idrecepcionenc = @idrecepcionenc
                                 And lic_plate = @lic_plate And idrecepciondet <> @idrecepciondet)
                                 UNION 
                                 Select lic_plate FROM stock 
                                 WHERE (idrecepcionenc = @idrecepcionenc)
                                 AND lic_plate = @lic_plate  AND idrecepciondet <> @idrecepciondet "



            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@idrecepcionenc", IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idrecepciondet", IdRecepcionDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lic_plate", LicPlate))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            ExisteLP = (dt.Rows.Count = 1)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByRef oBeTrans_re_det As clsBeTrans_re_det,
                                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_re_det")
            Upd.Add("IdOrdenCompraEnc", "@IdOrdenCompraEnc", DataType.Parametro)
            Upd.Add("IdOrdenCompraDet", "@IdOrdenCompraDet", DataType.Parametro)
            Upd.Where("IdRecepcionDet = @IdRecepcionDet " &
                      "AND IdRecepcionEnc = @IdRecepcionEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", oBeTrans_re_det.IdRecepcionDet))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", oBeTrans_re_det.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_re_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_re_det.IdOrdenCompraDet))

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



    Public Shared Function Licencia_Procesada_Stock_Jornada(ByVal pLicencia As String,
                                                            ByVal pIdRecepcionEnc As Integer,
                                                            ByVal pIdRecepcionDet As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Boolean

        Licencia_Procesada_Stock_Jornada = False

        Try

            Const sp As String = "SELECT * FROM Trans_re_det 
                                  WHERE(Lic_Plate = @Lic_Plate AND IdRecepcionEnc = @IdRecepcionEnc 
                                                               AND IdRecepcionDet = @IdRecepcionDet 
                                                               AND IdJornadaSistema > 0) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", pLicencia)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pIdRecepcionDet)


                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Licencia_Procesada_Stock_Jornada = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Actualizar_Licencia_Procesada_Por_Stock_Jornada(ByVal pLicenciaJornada As clsBeLicenciaJornada,
                                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_re_det")
            Upd.Add("IdJornadaSistema", "@IdJornadaSistema", DataType.Parametro)
            Upd.Where("lic_plate = @lic_plate AND IdRecepcionEnc = @IdRecepcionEnc AND IdRecepcionDet = @IdRecepcionDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdJornadaSistema", pLicenciaJornada.IdJornadaSistema))
            cmd.Parameters.Add(New SqlParameter("@lic_plate", pLicenciaJornada.Licencia))
            cmd.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pLicenciaJornada.IdRecepcionEnc))
            cmd.Parameters.Add(New SqlParameter("@IdRecepcionDet", pLicenciaJornada.IdRecepcionDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
