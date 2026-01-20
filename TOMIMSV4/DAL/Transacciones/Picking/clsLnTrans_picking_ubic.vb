Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_picking_ubic

    Public Shared Sub Cargar(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, ByRef dr As DataRow)

        Try

            With oBeTrans_picking_ubic

                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet")) '#CKFK 20180331 Agregué el IdPedidoDet en el cargar porque se estaba quedando con valor 0
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacionAnterior = IIf(IsDBNull(dr.Item("IdUbicacionAnterior")), 0, dr.Item("IdUbicacionAnterior"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Fecha_minima = IIf(IsDBNull(dr.Item("fecha_minima")), Date.Now, dr.Item("fecha_minima"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Acepto = IIf(IsDBNull(dr.Item("acepto")), False, dr.Item("acepto"))
                .Peso_solicitado = IIf(IsDBNull(dr.Item("peso_solicitado")), 0.0, dr.Item("peso_solicitado"))
                .Peso_recibido = IIf(IsDBNull(dr.Item("peso_recibido")), 0.0, dr.Item("peso_recibido"))
                .Peso_verificado = IIf(IsDBNull(dr.Item("peso_verificado")), 0.0, dr.Item("peso_verificado"))
                .Peso_despachado = IIf(IsDBNull(dr.Item("peso_despachado")), 0.0, dr.Item("peso_despachado"))
                .Cantidad_Solicitada = IIf(IsDBNull(dr.Item("cantidad_solicitada")), 0.0, dr.Item("cantidad_solicitada"))
                .Cantidad_Recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Cantidad_Verificada = IIf(IsDBNull(dr.Item("cantidad_verificada")), 0.0, dr.Item("cantidad_verificada"))
                .Encontrado = IIf(IsDBNull(dr.Item("encontrado")), False, dr.Item("encontrado"))
                .Dañado_verificacion = IIf(IsDBNull(dr.Item("dañado_verificacion")), False, dr.Item("dañado_verificacion"))
                .Fecha_real_vence = IIf(IsDBNull(dr.Item("fecha_real_vence")), Date.Now, dr.Item("fecha_real_vence"))
                .No_packing = IIf(IsDBNull(dr.Item("no_packing")), "", dr.Item("no_packing"))
                .Fecha_picking = IIf(IsDBNull(dr.Item("fecha_picking")), Date.Now, dr.Item("fecha_picking"))
                .Fecha_verificado = IIf(IsDBNull(dr.Item("fecha_verificado")), Date.Now, dr.Item("fecha_verificado"))
                .Fecha_packing = IIf(IsDBNull(dr.Item("fecha_packing")), Date.Now, dr.Item("fecha_packing"))
                .Fecha_despachado = IIf(IsDBNull(dr.Item("fecha_despachado")), Date.Now, dr.Item("fecha_despachado"))
                .Cantidad_despachada = IIf(IsDBNull(dr.Item("cantidad_despachada")), 0.0, dr.Item("cantidad_despachada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Dañado_picking = IIf(IsDBNull(dr.Item("dañado_picking")), False, dr.Item("dañado_picking"))
                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes")) '#CKFK 20180402 06:37 PM Agregué el campo IdStockRes a la tabla para trazabilidad
                .Lic_plate_Reemplazo = IIf(IsDBNull(dr.Item("lic_plate_reemplazo")), "", dr.Item("lic_plate_reemplazo"))
                .IdUbicacion_reemplazo = IIf(IsDBNull(dr.Item("IdUbicacion_reemplazo")), "0", dr.Item("IdUbicacion_reemplazo"))
                .IdStock_reemplazo = IIf(IsDBNull(dr.Item("IdStock_reemplazo")), "0", dr.Item("IdStock_reemplazo"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), "0", dr.Item("IdBodega"))
                .IdOperadorBodega_Pickeo = IIf(IsDBNull(dr.Item("IdOperadorBodega_Pickeo")), "0", dr.Item("IdOperadorBodega_Pickeo"))
                .IdOperadorBodega_Verifico = IIf(IsDBNull(dr.Item("IdOperadorBodega_Verifico")), "0", dr.Item("IdOperadorBodega_Verifico"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdUbicacionTemporal = IIf(IsDBNull(dr.Item("IdUbicacionTemporal")), 0, dr.Item("IdUbicacionTemporal"))

                If dr.Table.Columns.Contains("No_encontrado") Then
                    .No_encontrado = IIf(IsDBNull(dr.Item("no_encontrado")), False, dr.Item("no_encontrado")) '#AT 20220118 Nuevo campo agreagado
                End If

                If .No_encontrado Then
                    Debug.Print(.Cantidad_Solicitada & " - " & .IdPickingUbic)
                End If

                '#CKFK 20211103 Agregué el campo Tarima
                If dr.Table.Columns.Contains("Tarima") Then
                    .Tarima = IIf(IsDBNull(dr.Item("Tarima")), "0", dr.Item("Tarima"))
                End If

                '#CKFK20220217 Agregué el campo NombreArea
                If dr.Table.Columns.Contains("NombreArea") Then
                    .NombreArea = IIf(IsDBNull(dr.Item("NombreArea")), "", dr.Item("NombreArea"))
                End If

                '#AT Agregué el campo NombreClasificacion
                If dr.Table.Columns.Contains("NombreClasificacion") Then
                    .NombreClasificacion = IIf(IsDBNull(dr.Item("NombreClasificacion")), "", dr.Item("NombreClasificacion"))
                End If

                '#AT20220330 Agregué el campo NombreClasificacion
                If dr.Table.Columns.Contains("NombreUbicacionTemporal") Then
                    .NombreUbicacionTemporal = IIf(IsDBNull(dr.Item("NombreUbicacionTemporal")), "", dr.Item("NombreUbicacionTemporal"))
                End If

                '#EJC20220603: AUP
                .IdOperadorBodega_Asignado = IIf(IsDBNull(dr.Item("IdOperadorBodega_Asignado")), "0", dr.Item("IdOperadorBodega_Asignado"))

                '#CKFK20250124 Agregué el campo Referencia
                If dr.Table.Columns.Contains("Referencia") Then
                    .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                End If

                If dr.Table.Columns.Contains("IdProductoTallaColor") Then
                    .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                End If

                If dr.Table.Columns.Contains("Codigo_Talla") Then
                    .Codigo_Talla = IIf(IsDBNull(dr.Item("Codigo_Talla")), "", dr.Item("Codigo_Talla"))
                End If

                If dr.Table.Columns.Contains("Nombre_Talla") Then
                    .Nombre_Talla = IIf(IsDBNull(dr.Item("Nombre_Talla")), "", dr.Item("Nombre_Talla"))
                End If

                If dr.Table.Columns.Contains("Codigo_Color") Then
                    .Codigo_Color = IIf(IsDBNull(dr.Item("Codigo_Color")), "", dr.Item("Codigo_Color"))
                End If

                If dr.Table.Columns.Contains("Nombre_Color") Then
                    .Nombre_Color = IIf(IsDBNull(dr.Item("Nombre_Color")), "", dr.Item("Nombre_Color"))
                End If

                If dr.Table.Columns.Contains("CodigoSKU") Then
                    .CodigoSKU = IIf(IsDBNull(dr.Item("CodigoSKU")), "", dr.Item("CodigoSKU"))
                End If

                If dr.Table.Columns.Contains("IdProductoTallaColor") Then
                    .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                End If

                If dr.Table.Columns.Contains("No_Linea") Then
                    .No_Linea = IIf(IsDBNull(dr.Item("No_Linea")), 0, dr.Item("No_Linea"))
                End If

                If dr.Table.Columns.Contains("IdTalla") Then
                    .IdTalla = IIf(IsDBNull(dr.Item("IdTalla")), 0, dr.Item("IdTalla"))
                End If

                If dr.Table.Columns.Contains("IdColor") Then
                    .IdColor = IIf(IsDBNull(dr.Item("IdColor")), 0, dr.Item("IdColor"))
                End If
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CantidadStockDestino As Double = 0

        Try

            Ins.Init("trans_picking_ubic")
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Ins.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("IdStock", "@IdStock", DataType.Parametro)
            Ins.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Ins.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            Ins.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Ins.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Ins.Add("IdUnidadMedida", "@IdUnidadMedida", DataType.Parametro)
            Ins.Add("IdUbicacionAnterior", "@IdUbicacionAnterior", DataType.Parametro)
            Ins.Add("IdRecepcion", "@IdRecepcion", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("fecha_minima", "@fecha_minima", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("acepto", "@acepto", DataType.Parametro)
            Ins.Add("peso_solicitado", "@peso_solicitado", DataType.Parametro)
            Ins.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Ins.Add("peso_verificado", "@peso_verificado", DataType.Parametro)
            Ins.Add("peso_despachado", "@peso_despachado", DataType.Parametro)
            Ins.Add("cantidad_solicitada", "@cantidad_solicitada", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Ins.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
            Ins.Add("encontrado", "@encontrado", DataType.Parametro)
            Ins.Add("dañado_verificacion", "@dañado_verificacion", DataType.Parametro)
            Ins.Add("fecha_real_vence", "@fecha_real_vence", DataType.Parametro)
            Ins.Add("no_packing", "@no_packing", DataType.Parametro)
            Ins.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Ins.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
            Ins.Add("fecha_packing", "@fecha_packing", DataType.Parametro)
            Ins.Add("fecha_despachado", "@fecha_despachado", DataType.Parametro)
            Ins.Add("cantidad_despachada", "@cantidad_despachada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("dañado_picking", "@dañado_picking", DataType.Parametro)
            Ins.Add("IdStockRes", "@IdStockRes", DataType.Parametro)
            Ins.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", DataType.Parametro)
            Ins.Add("IdUbicacion_reemplazo", "@IdUbicacion_reemplazo", DataType.Parametro)
            Ins.Add("IdStock_reemplazo", "@IdStock_reemplazo", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Ins.Add("IdOperadorBodega_Pickeo", "@IdOperadorBodega_Pickeo", DataType.Parametro)
            Ins.Add("IdOperadorBodega_Verifico", "@IdOperadorBodega_Verifico", DataType.Parametro)
            Ins.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro)
            Ins.Add("no_encontrado", "@no_encontrado", DataType.Parametro)
            Ins.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
            Ins.Add("IdOperadorBodega_Asignado", "@IdOperadorBodega_Asignado", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_ubic.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_ubic.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_picking_ubic.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_ubic.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_picking_ubic.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_picking_ubic.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_picking_ubic.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_picking_ubic.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_picking_ubic.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_picking_ubic.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_picking_ubic.Lote Is Nothing, DBNull.Value, oBeTrans_picking_ubic.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_picking_ubic.Fecha_Vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_Vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MINIMA", IIf(oBeTrans_picking_ubic.Fecha_minima = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_minima)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeTrans_picking_ubic.Serial = Nothing, DBNull.Value, oBeTrans_picking_ubic.Serial)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_picking_ubic.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@ACEPTO", oBeTrans_picking_ubic.Acepto))
            cmd.Parameters.Add(New SqlParameter("@PESO_SOLICITADO", oBeTrans_picking_ubic.Peso_solicitado))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_picking_ubic.Peso_recibido))
            cmd.Parameters.Add(New SqlParameter("@PESO_VERIFICADO", oBeTrans_picking_ubic.Peso_verificado))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_picking_ubic.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_SOLICITADA", oBeTrans_picking_ubic.Cantidad_Solicitada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic.Cantidad_Recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_picking_ubic.Cantidad_Verificada))
            cmd.Parameters.Add(New SqlParameter("@ENCONTRADO", oBeTrans_picking_ubic.Encontrado))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO_VERIFICACION", oBeTrans_picking_ubic.Dañado_verificacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REAL_VENCE", IIf(oBeTrans_picking_ubic.Fecha_real_vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_real_vence)))
            cmd.Parameters.Add(New SqlParameter("@NO_PACKING", IIf(oBeTrans_picking_ubic.No_packing = Nothing, DBNull.Value, oBeTrans_picking_ubic.No_packing)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", IIf(oBeTrans_picking_ubic.Fecha_picking = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_picking)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", IIf(oBeTrans_picking_ubic.Fecha_verificado = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_verificado)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PACKING", IIf(oBeTrans_picking_ubic.Fecha_packing = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_packing)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHADO", IIf(oBeTrans_picking_ubic.Fecha_despachado = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_despachado)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_DESPACHADA", oBeTrans_picking_ubic.Cantidad_despachada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_ubic.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_ubic.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_ubic.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_ubic.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_ubic.Activo))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO_PICKING", oBeTrans_picking_ubic.Dañado_picking))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE_REEMPLAZO", oBeTrans_picking_ubic.Lic_plate_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_REEMPLAZO", oBeTrans_picking_ubic.IdUbicacion_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK_REEMPLAZO", oBeTrans_picking_ubic.IdStock_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_picking_ubic.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_picking_ubic.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_picking_ubic.IdOperadorBodega_Verifico))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_ubic.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_ENCONTRADO", oBeTrans_picking_ubic.No_encontrado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic.IdUbicacionTemporal))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_picking_ubic.IdOperadorBodega_Asignado))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_picking_ubic.IdProductoTallaColor))

            CantidadStockDestino = oBeTrans_picking_ubic.Cantidad_Solicitada

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(oBeTrans_picking_ubic.IdBodega, pConection, pTransaction)
            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

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

    Public Shared Function Actualizar(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro) '#EJC20200125: Agregado el 24/01/2020
            Upd.Add("idpickingubic", "@idpickingubic", DataType.Parametro)
            Upd.Add("idpickingdet", "@idpickingdet", DataType.Parametro)
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("IdStock", "@IdStock", DataType.Parametro)
            Upd.Add("IdPropietarioBodega", "@IdPropietarioBodega", DataType.Parametro)
            Upd.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            Upd.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Upd.Add("IdPresentacion", "@IdPresentacion", DataType.Parametro)
            Upd.Add("IdUnidadMedida", "@IdUnidadMedida", DataType.Parametro)
            Upd.Add("IdUbicacionAnterior", "@IdUbicacionAnterior", DataType.Parametro)
            Upd.Add("IdRecepcion", "@IdRecepcion", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_minima", "@fecha_minima", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("acepto", "@acepto", DataType.Parametro)
            Upd.Add("peso_solicitado", "@peso_solicitado", DataType.Parametro)
            Upd.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Upd.Add("peso_verificado", "@peso_verificado", DataType.Parametro)
            Upd.Add("peso_despachado", "@peso_despachado", DataType.Parametro)
            Upd.Add("cantidad_solicitada", "@cantidad_solicitada", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
            Upd.Add("encontrado", "@encontrado", DataType.Parametro)
            Upd.Add("dañado_verificacion", "@dañado_verificacion", DataType.Parametro)
            Upd.Add("fecha_real_vence", "@fecha_real_vence", DataType.Parametro)
            Upd.Add("no_packing", "@no_packing", DataType.Parametro)
            Upd.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Upd.Add("fecha_verificado", "@fecha_verificado", DataType.Parametro)
            Upd.Add("fecha_packing", "@fecha_packing", DataType.Parametro)
            Upd.Add("fecha_despachado", "@fecha_despachado", DataType.Parametro)
            Upd.Add("cantidad_despachada", "@cantidad_despachada", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("dañado_picking", "@dañado_picking", DataType.Parametro)
            Upd.Add("IdStockRes", "@IdStockRes", DataType.Parametro)
            Upd.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", DataType.Parametro)
            Upd.Add("IdUbicacion_reemplazo", "@IdUbicacion_reemplazo", DataType.Parametro)
            Upd.Add("IdStock_reemplazo", "@IdStock_reemplazo", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Add("IdOperadorBodega_Pickeo", "@IdOperadorBodega_Pickeo", DataType.Parametro) '#EJC20200125: Agregado el 25/01/2020
            Upd.Add("IdOperadorBodega_Verifico", "@IdOperadorBodega_Verifico", DataType.Parametro) '#EJC20200125: Agregado el 25/01/2020
            Upd.Add("IdPedidoEnc", "@IdPedidoEnc", DataType.Parametro) '#EJC20220113: Agregado IdPedidoEnc en Actualizar trans_picking_ubic.
            Upd.Add("no_encontrado", "@no_encontrado", DataType.Parametro) '#AT 20220118 Nuevo campo agregado 
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro) '#AT 20220118 Nuevo campo agregado 
            Upd.Add("IdOperadorBodega_Asignado", "@IdOperadorBodega_Asignado", DataType.Parametro)
            Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", oBeTrans_picking_ubic.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_ubic.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_picking_ubic.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_picking_ubic.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_picking_ubic.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_picking_ubic.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_picking_ubic.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_picking_ubic.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_picking_ubic.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_picking_ubic.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_picking_ubic.Lote Is Nothing, DBNull.Value, oBeTrans_picking_ubic.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_picking_ubic.Fecha_Vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_Vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MINIMA", IIf(oBeTrans_picking_ubic.Fecha_minima = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_minima)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeTrans_picking_ubic.Serial = Nothing, DBNull.Value, oBeTrans_picking_ubic.Serial)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_picking_ubic.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@ACEPTO", oBeTrans_picking_ubic.Acepto))
            cmd.Parameters.Add(New SqlParameter("@PESO_SOLICITADO", oBeTrans_picking_ubic.Peso_solicitado))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_picking_ubic.Peso_recibido))
            cmd.Parameters.Add(New SqlParameter("@PESO_VERIFICADO", oBeTrans_picking_ubic.Peso_verificado))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_picking_ubic.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_SOLICITADA", oBeTrans_picking_ubic.Cantidad_Solicitada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic.Cantidad_Recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_picking_ubic.Cantidad_Verificada))
            cmd.Parameters.Add(New SqlParameter("@ENCONTRADO", oBeTrans_picking_ubic.Encontrado))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO_VERIFICACION", oBeTrans_picking_ubic.Dañado_verificacion))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REAL_VENCE", IIf(oBeTrans_picking_ubic.Fecha_real_vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_real_vence)))
            cmd.Parameters.Add(New SqlParameter("@NO_PACKING", IIf(oBeTrans_picking_ubic.No_packing = Nothing, DBNull.Value, oBeTrans_picking_ubic.No_packing)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", IIf(oBeTrans_picking_ubic.Fecha_picking = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_picking)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VERIFICADO", IIf(oBeTrans_picking_ubic.Fecha_verificado = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_verificado)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PACKING", IIf(oBeTrans_picking_ubic.Fecha_packing = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_packing)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_DESPACHADO", IIf(oBeTrans_picking_ubic.Fecha_despachado = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_despachado)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_DESPACHADA", oBeTrans_picking_ubic.Cantidad_despachada))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_picking_ubic.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_picking_ubic.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_ubic.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_ubic.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_picking_ubic.Activo))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO_PICKING", oBeTrans_picking_ubic.Dañado_picking))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE_REEMPLAZO", oBeTrans_picking_ubic.Lic_plate_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_REEMPLAZO", oBeTrans_picking_ubic.IdUbicacion_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK_REEMPLAZO", oBeTrans_picking_ubic.IdStock_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_picking_ubic.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_VERIFICO", oBeTrans_picking_ubic.IdOperadorBodega_Verifico))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_ubic.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@NO_ENCONTRADO", oBeTrans_picking_ubic.No_encontrado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic.IdUbicacionTemporal))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_ASIGNADO", oBeTrans_picking_ubic.IdOperadorBodega_Asignado))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_picking_ubic.IdProductoTallaColor))

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

    Public Shared Function Eliminar(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_picking_ubic" &
             "  Where(IdPickingUbic = @IdPickingUbic)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))


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

    Public Shared Function Eliminar_By_Params(ByVal IdPickingUbic As Integer,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdPickingDet As Integer,
                                              ByVal IdStock As Integer,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_picking_ubic 
                                   WHERE(IdPickingUbic = @IdPickingUbic 
                                   AND IdPickingEnc = @IdPickingEnc 
                                   AND IdPickingDet = @IdPickingDet 
                                   AND IdStock = @IdStock)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", IdStock))


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

    '#CKFK 20180220 10:36 PM Le agregué transaccionalidad al GetSingle al clsLnTrans_picking_ubic
    Public Shared Function GetSingle(ByRef pBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional pConnection As SqlConnection = Nothing, Optional pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic Where(IdPickingUbic = @IdPickingUbic)"

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction
            End If

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", pBeTrans_picking_ubic.IdPickingUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_picking_ubic, dt.Rows(0))
                GetSingle = True
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_By_Params(ByVal IdPickingUbic As Integer,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdPickingDet As Integer,
                                              ByVal IdStock As Integer,
                                              ByVal IdStockRes As Integer,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_picking_ubic" &
             "  Where(IdPickingUbic = @IdPickingUbic 
                AND IdPickingEnc = @IdPickingEnc 
                AND IdPickingDet = @IdPickingDet 
                AND IdStock = @IdStock
                AND IdStockRes = @IdStockRes)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", IdStockRes))


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

    Public Shared Function Actualizar_IdUbicacionTemporal(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic AND idpickingenc = @IdPickingEnc AND IdBodega = @IdBodega")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic.IdUbicacionTemporal))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic.IdBodega))

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

    Public Shared Function Eliminar_By_BePickingUbic(ByVal BePickingUbic As clsBeTrans_picking_ubic,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vDiferencia As Double = 0
        Dim rowsAffected As Integer = 0

        Eliminar_By_BePickingUbic = 0

        Try

            '#EJC20220412: Si recibió menos de lo que solicitaron (aun tiene pendiente por pickear)
            If BePickingUbic.Cantidad_Solicitada > BePickingUbic.Cantidad_Recibida Then

                vDiferencia = BePickingUbic.Cantidad_Solicitada - BePickingUbic.Cantidad_Recibida

                '#EJC20220412: No estoy seguro de que esto se deba borrar...
                'Pero solo lo vamos a eliminar si la cantidad total fue liberada del picking.
                If BePickingUbic.Cantidad_Solicitada = vDiferencia Then

                    Const sp As String = " DELETE FROM Trans_picking_ubic 
                                           WHERE(IdPickingUbic = @IdPickingUbic 
                                           AND IdPickingEnc = @IdPickingEnc 
                                           AND IdPickingDet = @IdPickingDet 
                                           AND IdStock = @IdStock
                                           AND IdStockRes = @IdStockRes)"

                    Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
                    Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

                    If Es_Transaccion_Remota Then
                        cmd = New SqlCommand(sp, pConection)
                        cmd.Transaction = pTransaction
                    Else
                        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                        cmd = New SqlCommand(sp, lConnection, lTransaction)
                    End If

                    cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", BePickingUbic.IdPickingUbic))
                    cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", BePickingUbic.IdPickingEnc))
                    cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", BePickingUbic.IdPickingDet))
                    cmd.Parameters.Add(New SqlParameter("@IDSTOCK", BePickingUbic.IdStock))
                    cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", BePickingUbic.IdStockRes))

                    rowsAffected = cmd.ExecuteNonQuery()

                    cmd.Dispose()

                    If Not Es_Transaccion_Remota Then lTransaction.Commit()

                Else
                    '#EJC20220412:No lo sé, pero me da la impresión de que debería dejar en el picking.
                    'la cantidad solicitada igual a la pickeada, te lo dejo aquí Erik
                    'del futuro para cuando alguien lo reporte mas adelante
                    BePickingUbic.Cantidad_Solicitada = BePickingUbic.Cantidad_Recibida
                    rowsAffected = Actualizar_Cantidad_Solicitada(BePickingUbic, lConnection, lTransaction)
                    Debug.Write("something is still missing ")
                End If

            End If


            Eliminar_By_BePickingUbic = rowsAffected

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

    Public Shared Function Actualizar_Cantidad_Solicitada(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("cantidad_solicitada", "@cantidad_solicitada", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic 
                       AND IdPickingEnc = @IdPickingEnc 
                       AND IdBodega = @IdBodega 
                       AND IdPedidoEnc = @IdPedidoEnc 
                       AND IdPedidoDet = @IdPedidoDet 
                       AND IdStockRes = @IdStockRes
                       AND IdStock = @IdStock")

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

            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_SOLICITADA", oBeTrans_picking_ubic.Cantidad_Solicitada))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_picking_ubic.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_picking_ubic.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_picking_ubic.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic.IdStock))

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

    Public Shared Function Actualizar_IdUbicacion_By_IdStockRes(ByVal IdUbicacion As Integer,
                                                                ByVal IdUbicacionAnterior As Integer,
                                                                ByVal IdBodega As Integer,
                                                                ByVal IdStock As Integer,
                                                                ByVal IdStockRes As Integer,
                                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Upd.Init("trans_picking_ubic")
            Upd.Add("idubicacion", "@IdUbicacion", DataType.Parametro)
            Upd.Add("IdUbicacionAnterior", "@IdUbicacionAnterior", DataType.Parametro)
            Upd.Where("IdBodega=@IdBodega 
                       AND IdStock=@IdStock 
                       AND IdStockRes=@IdStockRes ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacionAnterior", IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))

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

    Public Shared Function Actualizar_Cantidad_Recibida(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("IdProductoEstado", "@IdProductoEstado", DataType.Parametro)
            Upd.Add("IdUbicacionAnterior", "@IdUbicacionAnterior", DataType.Parametro)
            Upd.Add("IdRecepcion", "@IdRecepcion", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_minima", "@fecha_minima", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("acepto", "@acepto", DataType.Parametro)
            Upd.Add("peso_solicitado", "@peso_solicitado", DataType.Parametro)
            Upd.Add("peso_recibido", "@peso_recibido", DataType.Parametro)
            Upd.Add("peso_verificado", "@peso_verificado", DataType.Parametro)
            Upd.Add("cantidad_solicitada", "@cantidad_solicitada", DataType.Parametro)
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Add("cantidad_verificada", "@cantidad_verificada", DataType.Parametro)
            Upd.Add("encontrado", "@encontrado", DataType.Parametro)
            Upd.Add("fecha_real_vence", "@fecha_real_vence", DataType.Parametro)
            Upd.Add("fecha_picking", "@fecha_picking", DataType.Parametro)
            Upd.Add("dañado_picking", "@dañado_picking", DataType.Parametro)
            Upd.Add("lic_plate_reemplazo", "@lic_plate_reemplazo", DataType.Parametro)
            Upd.Add("IdUbicacion_reemplazo", "@IdUbicacion_reemplazo", DataType.Parametro)
            Upd.Add("IdStock_reemplazo", "@IdStock_reemplazo", DataType.Parametro)
            Upd.Add("IdOperadorBodega_Pickeo", "@IdOperadorBodega_Pickeo", DataType.Parametro)
            Upd.Add("no_encontrado", "@no_encontrado", DataType.Parametro)
            Upd.Add("idubicaciontemporal", "@idubicaciontemporal", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic AND IdPickingEnc=@IdPickingEnc")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_picking_ubic.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_picking_ubic.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONANTERIOR", oBeTrans_picking_ubic.IdUbicacionAnterior))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeTrans_picking_ubic.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeTrans_picking_ubic.Lote Is Nothing, DBNull.Value, oBeTrans_picking_ubic.Lote)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeTrans_picking_ubic.Fecha_Vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_Vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MINIMA", IIf(oBeTrans_picking_ubic.Fecha_minima = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_minima)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeTrans_picking_ubic.Serial = Nothing, DBNull.Value, oBeTrans_picking_ubic.Serial)))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_picking_ubic.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@ACEPTO", oBeTrans_picking_ubic.Acepto))
            cmd.Parameters.Add(New SqlParameter("@PESO_SOLICITADO", oBeTrans_picking_ubic.Peso_solicitado))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECIBIDO", oBeTrans_picking_ubic.Peso_recibido))
            cmd.Parameters.Add(New SqlParameter("@PESO_VERIFICADO", oBeTrans_picking_ubic.Peso_verificado))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_picking_ubic.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_SOLICITADA", oBeTrans_picking_ubic.Cantidad_Solicitada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic.Cantidad_Recibida))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_VERIFICADA", oBeTrans_picking_ubic.Cantidad_Verificada))
            cmd.Parameters.Add(New SqlParameter("@ENCONTRADO", oBeTrans_picking_ubic.Encontrado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REAL_VENCE", IIf(oBeTrans_picking_ubic.Fecha_real_vence = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_real_vence)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", IIf(oBeTrans_picking_ubic.Fecha_picking = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_picking)))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO_PICKING", oBeTrans_picking_ubic.Dañado_picking))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE_REEMPLAZO", oBeTrans_picking_ubic.Lic_plate_Reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_REEMPLAZO", oBeTrans_picking_ubic.IdUbicacion_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK_REEMPLAZO", oBeTrans_picking_ubic.IdStock_reemplazo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA_PICKEO", oBeTrans_picking_ubic.IdOperadorBodega_Pickeo))
            cmd.Parameters.Add(New SqlParameter("@NO_ENCONTRADO", oBeTrans_picking_ubic.No_encontrado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTEMPORAL", oBeTrans_picking_ubic.IdUbicacionTemporal))

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

    Public Shared Function Actualizar_FechaPacking(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("fecha_packing", "@fecha_packing", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic AND IdPickingEnc=@IdPickingEnc")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_ubic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PACKING", IIf(oBeTrans_picking_ubic.Fecha_packing = Nothing, DBNull.Value, oBeTrans_picking_ubic.Fecha_packing)))

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

    Public Shared Function Eliminar_By_IdPickingUbic(ByVal BePickingUbic As clsBeTrans_picking_ubic,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vDiferencia As Double = 0
        Dim rowsAffected As Integer = 0

        Eliminar_By_IdPickingUbic = 0

        Try

            Const sp As String = " DELETE FROM Trans_picking_ubic 
                                           WHERE(IdPickingUbic = @IdPickingUbic 
                                           AND IdPickingEnc = @IdPickingEnc 
                                           AND IdPickingDet = @IdPickingDet 
                                           AND IdStock = @IdStock
                                           AND IdStockRes = @IdStockRes)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", BePickingUbic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", BePickingUbic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGDET", BePickingUbic.IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", BePickingUbic.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", BePickingUbic.IdStockRes))

            rowsAffected = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Eliminar_By_IdPickingUbic = rowsAffected

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

End Class