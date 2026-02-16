Imports System.Data.SqlClient

Public Class clsLnTrans_oc_det

    Public Shared Sub Cargar(ByRef oBeTrans_oc_det As clsBeTrans_oc_det,
                             ByRef dr As DataRow,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeTrans_oc_det

                .IdOrdenCompraEnc = IIf(IsDBNull(dr.Item("IdOrdenCompraEnc")), 0, dr.Item("IdOrdenCompraEnc"))
                .IdOrdenCompraDet = IIf(IsDBNull(dr.Item("IdOrdenCompraDet")), 0, dr.Item("IdOrdenCompraDet"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Arancel.IdArancel = IIf(IsDBNull(dr.Item("IdArancel")), 0, dr.Item("IdArancel"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .UnidadMedida.IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .IdMotivoDevolucion = IIf(IsDBNull(dr.Item("IdMotivoDevolucion")), 0, dr.Item("IdMotivoDevolucion"))
                .No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Nombre_presentacion = IIf(IsDBNull(dr.Item("nombre_presentacion")), "", dr.Item("nombre_presentacion"))
                .Nombre_arancel = IIf(IsDBNull(dr.Item("nombre_arancel")), "", dr.Item("nombre_arancel"))
                .Porcentaje_arancel = IIf(IsDBNull(dr.Item("porcentaje_arancel")), 0.0, dr.Item("porcentaje_arancel"))
                .Nombre_unidad_medida_basica = IIf(IsDBNull(dr.Item("nombre_unidad_medida_basica")), "", dr.Item("nombre_unidad_medida_basica"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Cantidad_recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                .Total_linea = IIf(IsDBNull(dr.Item("total_linea")), 0.0, dr.Item("total_linea"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Peso_Recibido = IIf(IsDBNull(dr.Item("Peso_Recibido")), 0.0, dr.Item("Peso_Recibido"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Atributo_variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), 0.0, dr.Item("codigo_producto"))

                If Not .Presentacion.IdPresentacion = Nothing Then
                    .Presentacion = clsLnProducto_presentacion.GetSingle(.Presentacion.IdPresentacion, lConnection, lTransaction)
                End If

                .valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0.0, dr.Item("valor_aduana"))
                .valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0.0, dr.Item("valor_fob"))
                .valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0.0, dr.Item("valor_iva"))
                .valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0.0, dr.Item("valor_dai"))
                .valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0.0, dr.Item("valor_seguro"))
                .valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0.0, dr.Item("valor_flete"))
                .Peso_Neto = IIf(IsDBNull(dr.Item("peso_neto")), 0.0, dr.Item("peso_neto"))
                .Peso_Bruto = IIf(IsDBNull(dr.Item("peso_bruto")), 0.0, dr.Item("peso_bruto"))

                '#EJC20210318_1540: 
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .Nombre_Propietario = IIf(IsDBNull(dr.Item("nombre_propietario")), "", dr.Item("nombre_propietario"))

                '#EJC20210403_1343PM
                .IdOrdenCompraDetPadre = IIf(IsDBNull(dr.Item("IdOrdenCompraDetPadre")), 0, dr.Item("IdOrdenCompraDetPadre"))

                '#EJC20220224_2144: Cealsa.
                .IdEmbarcador = IIf(IsDBNull(dr.Item("IdEmbarcador")), 0, dr.Item("IdEmbarcador"))

                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_oc_det As clsBeTrans_oc_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Insertar = 0

        Try

            Ins.Init("trans_oc_det")
            Ins.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Ins.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)

            'EJC_18092016
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            End If

            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("nombre_presentacion", "@nombre_presentacion", DataType.Parametro)
            Ins.Add("nombre_arancel", "@nombre_arancel", DataType.Parametro)
            Ins.Add("porcentaje_arancel", "@porcentaje_arancel", DataType.Parametro)
            Ins.Add("nombre_unidad_medida_basica", "@nombre_unidad_medida_basica", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Ins.Add("total_linea", "@total_linea", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            '#EJC20181121: Interface MI3 Idealsa
            If Not oBeTrans_oc_det.Atributo_variante_1 Is Nothing Then
                Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            End If

            '"#EJC20201228: Por detalle de Poliza Cealsa
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Ins.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Ins.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Ins.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Ins.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Ins.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Ins.Add("peso_bruto", "@peso_bruto", DataType.Parametro)

            '#EJC20210313: Agregado para el manejo de documento de ingreso de consolidadores para CEALSA.
            If Not oBeTrans_oc_det.IdPropietarioBodega = 0 Then Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            If Not oBeTrans_oc_det.Nombre_Propietario = "" Then Ins.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)

            '#EJC20210403: Agregado para el manejo de producto kit.
            If Not oBeTrans_oc_det.IdOrdenCompraDetPadre = 0 Then Ins.Add("IdOrdenCompraDetPadre", "@IdOrdenCompraDetPadre", DataType.Parametro)

            If Not oBeTrans_oc_det.IdEmbarcador = 0 Then Ins.Add("IdEmbarcador", "@IdEmbarcador", DataType.Parametro)
            If Not oBeTrans_oc_det.IdProductoTallaColor = 0 Then Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det.IdProductoBodega))

            'ejc_18092016: object_reference throws when nothing value is in arancel
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IDARANCEL", IIf(oBeTrans_oc_det.Arancel.IdArancel = 0, DBNull.Value, oBeTrans_oc_det.Arancel.IdArancel)))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_oc_det.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_oc_det.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeTrans_oc_det.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_oc_det.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_oc_det.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_oc_det.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", IIf(oBeTrans_oc_det.No_Linea = 0, 0, oBeTrans_oc_det.No_Linea)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", IIf(oBeTrans_oc_det.Nombre_producto Is Nothing, DBNull.Value, clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_oc_det.Nombre_producto))))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRESENTACION", IIf(oBeTrans_oc_det.Nombre_presentacion Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_presentacion)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_ARANCEL", IIf(oBeTrans_oc_det.Nombre_arancel Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_arancel)))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE_ARANCEL", oBeTrans_oc_det.Porcentaje_arancel))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD_MEDIDA_BASICA", IIf(oBeTrans_oc_det.Nombre_unidad_medida_basica Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_unidad_medida_basica)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_oc_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_oc_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_oc_det.Peso_Recibido))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_oc_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_det.Activo))
            '#EJC20201228: Agregado por Poliza CEALSA
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_oc_det.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeTrans_oc_det.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeTrans_oc_det.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeTrans_oc_det.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeTrans_oc_det.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeTrans_oc_det.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeTrans_oc_det.Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeTrans_oc_det.Peso_Bruto))

            If Not oBeTrans_oc_det.Atributo_variante_1 Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_oc_det.Atributo_variante_1))
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_det.Codigo_Producto))

            If Not oBeTrans_oc_det.IdPropietarioBodega = 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_det.IdPropietarioBodega))
                cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeTrans_oc_det.Nombre_Propietario))
            End If

            If Not oBeTrans_oc_det.IdOrdenCompraDetPadre = 0 Then cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETPADRE", oBeTrans_oc_det.IdOrdenCompraDetPadre))
            If Not oBeTrans_oc_det.IdEmbarcador = 0 Then cmd.Parameters.Add(New SqlParameter("@IDEMBARCADOR", oBeTrans_oc_det.IdEmbarcador))
            If Not oBeTrans_oc_det.IdProductoTallaColor = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_oc_det.IdProductoTallaColor))

            Debug.Print(oBeTrans_oc_det.Codigo_Producto)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_oc_det As clsBeTrans_oc_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_det")
            Upd.Add("idordencompraenc", "@idordencompraenc", DataType.Parametro)
            Upd.Add("idordencompradet", "@idordencompradet", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)

            'EJC_18092016
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            End If

            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("idmotivodevolucion", "@idmotivodevolucion", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("nombre_presentacion", "@nombre_presentacion", DataType.Parametro)
            Upd.Add("nombre_arancel", "@nombre_arancel", DataType.Parametro)
            Upd.Add("porcentaje_arancel", "@porcentaje_arancel", DataType.Parametro)
            Upd.Add("nombre_unidad_medida_basica", "@nombre_unidad_medida_basica", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("total_linea", "@total_linea", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)

            '"#EJC20201228: Por detalle de Poliza Cealsa
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Upd.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Upd.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Upd.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Upd.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Upd.Add("peso_neto", "@peso_neto", DataType.Parametro)
            Upd.Add("peso_bruto", "@peso_bruto", DataType.Parametro)

            '#EJC20210313: Agregado para el manejo de documento de ingreso de consolidadores para CEALSA.
            If Not oBeTrans_oc_det.IdPropietarioBodega = 0 Then Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            If Not oBeTrans_oc_det.Nombre_Propietario = "" Then Upd.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)

            '#EJC20210403: Agregado para el manejo de producto kit.
            If Not oBeTrans_oc_det.IdOrdenCompraDetPadre = 0 Then Upd.Add("IdOrdenCompraDetPadre", "@IdOrdenCompraDetPadre", DataType.Parametro)

            '#EJC20220224: Shipper o embarcador, agregado por CEALSA.
            If Not oBeTrans_oc_det.IdEmbarcador = 0 Then Ins.Add("IdEmbarcador", "@IdEmbarcador", DataType.Parametro)

            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc " &
                "AND IdOrdenCompraDet = @IdOrdenCompraDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_oc_det.IdProductoBodega))

            'ejc_18092016: object_reference throws when nothing value is in arancel
            If Not oBeTrans_oc_det.Arancel Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@IDARANCEL", IIf(oBeTrans_oc_det.Arancel.IdArancel = 0, DBNull.Value, oBeTrans_oc_det.Arancel.IdArancel)))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_oc_det.Presentacion.IdPresentacion = 0, DBNull.Value, oBeTrans_oc_det.Presentacion.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", IIf(oBeTrans_oc_det.UnidadMedida.IdUnidadMedida = 0, DBNull.Value, oBeTrans_oc_det.UnidadMedida.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVODEVOLUCION", IIf(oBeTrans_oc_det.IdMotivoDevolucion = 0, DBNull.Value, oBeTrans_oc_det.IdMotivoDevolucion)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", IIf(oBeTrans_oc_det.No_Linea = 0, DBNull.Value, oBeTrans_oc_det.No_Linea)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", IIf(oBeTrans_oc_det.Nombre_producto Is Nothing, DBNull.Value, clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_oc_det.Nombre_producto))))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRESENTACION", IIf(oBeTrans_oc_det.Nombre_presentacion Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_presentacion)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_ARANCEL", IIf(oBeTrans_oc_det.Nombre_arancel Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_arancel)))
            cmd.Parameters.Add(New SqlParameter("@PORCENTAJE_ARANCEL", IIf(oBeTrans_oc_det.Porcentaje_arancel = 0, DBNull.Value, oBeTrans_oc_det.Porcentaje_arancel)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_UNIDAD_MEDIDA_BASICA", IIf(oBeTrans_oc_det.Nombre_unidad_medida_basica Is Nothing, DBNull.Value, oBeTrans_oc_det.Nombre_unidad_medida_basica)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_oc_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_oc_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_oc_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_oc_det.Peso_Recibido))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_oc_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_oc_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_oc_det.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_oc_det.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_oc_det.Activo))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_oc_det.Atributo_variante_1))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_oc_det.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_oc_det.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeTrans_oc_det.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeTrans_oc_det.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeTrans_oc_det.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeTrans_oc_det.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeTrans_oc_det.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeTrans_oc_det.Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeTrans_oc_det.Peso_Bruto))

            If Not oBeTrans_oc_det.IdPropietarioBodega = 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_oc_det.IdPropietarioBodega))
                cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeTrans_oc_det.Nombre_Propietario))
            End If

            If Not oBeTrans_oc_det.IdOrdenCompraDetPadre = 0 Then cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETPADRE", oBeTrans_oc_det.IdOrdenCompraDetPadre))
            If Not oBeTrans_oc_det.IdEmbarcador = 0 Then cmd.Parameters.Add(New SqlParameter("@IDEMBARCADOR", oBeTrans_oc_det.IdEmbarcador))
            If Not oBeTrans_oc_det.IdProductoTallaColor = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_oc_det.IdProductoTallaColor))

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

    Public Shared Function Obtener(ByRef oBeTrans_oc_det As clsBeTrans_oc_det) As Boolean

        Obtener = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM Trans_oc_det 
                                 Where(IdOrdenCompraEnc = @IdOrdenCompraEnc) 
                                 AND (IdOrdenCompraDet = @IdOrdenCompraDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_det, dt.Rows(0), lConnection, lTransaction)
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

    ''' <summary>
    ''' #EJC20220314: Para byb, en la orden de producci�n sumar la cantidad a la l�nea en UMBAS.
    ''' </summary>
    ''' <param name="oBeTrans_oc_det"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Cantidad(ByRef oBeTrans_oc_det As clsBeTrans_oc_det,
                                               Optional ByVal pConection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_det")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc " &
                      "AND IdOrdenCompraDet = @IdOrdenCompraDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_oc_det.Cantidad))

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

    Public Shared Function Actualizar_Cantidad_Recibida(ByRef oBeTrans_oc_det As clsBeTrans_oc_det,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("trans_oc_det")
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc " &
                      "AND IdOrdenCompraDet = @IdOrdenCompraDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det.Cantidad_recibida))

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