Imports System.Data.SqlClient

Public Class clsLnStock_rec

    Public Shared Sub Cargar(ByRef oBeStock_rec As clsBeStock_rec, ByRef dr As DataRow)

        Try

            With oBeStock_rec

                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdStockRec = IIf(IsDBNull(dr.Item("IdStockRec")), 0, dr.Item("IdStockRec"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), "0", dr.Item("uds_lic_plate"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                .Regularizado = IIf(IsDBNull(dr.Item("regularizado")), False, dr.Item("regularizado"))
                .Fecha_regularizacion = IIf(IsDBNull(dr.Item("fecha_regularizacion")), Date.Now, dr.Item("fecha_regularizacion"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("Atributo_Variante_1")), "", dr.Item("Atributo_Variante_1"))
                .No_linea = IIf(IsDBNull(dr.Item("No_linea")), 0, dr.Item("No_linea"))
                .Pallet_No_Estandar = IIf(IsDBNull(dr.Item("pallet_no_estandar")), False, dr.Item("pallet_no_estandar"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeStock_rec As clsBeStock_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("stock_rec")
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idstockrec", "@idstockrec", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("IdUbicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
            Ins.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Ins.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Ins.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Ins.Add("lote", "@lote", DataType.Parametro)
            Ins.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Ins.Add("serial", "@serial", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Ins.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Ins.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Ins.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Ins.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Ins.Add("añada", "@añada", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("temperatura", "@temperatura", DataType.Parametro)
            Ins.Add("regularizado", "@regularizado", DataType.Parametro)
            Ins.Add("fecha_regularizacion", "@fecha_regularizacion", DataType.Parametro)
            Ins.Add("Atributo_Variante_1", "@Atributo_Variante_1", DataType.Parametro)
            Ins.Add("No_Linea", "@No_Linea", DataType.Parametro)
            Ins.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Ins.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            Ins.Add("Talla", "@Talla", DataType.Parametro)
            Ins.Add("Color", "@Color", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Bind(cmd, oBeStock_rec)

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

    Public Shared Function Actualizar(ByRef oBeStock_rec As clsBeStock_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("stock_rec")
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idstockrec", "@idstockrec", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("IdUbicacion_anterior", "@IdUbicacion_anterior", DataType.Parametro)
            Upd.Add("idrecepcionenc", "@idrecepcionenc", DataType.Parametro)
            Upd.Add("idrecepciondet", "@idrecepciondet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idpickingenc", "@idpickingenc", DataType.Parametro)
            Upd.Add("iddespachoenc", "@iddespachoenc", DataType.Parametro)
            Upd.Add("lote", "@lote", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Add("serial", "@serial", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            'Upd.Add("fecha_ingreso", "@fecha_ingreso", DataType.Parametro)
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("uds_lic_plate", "@uds_lic_plate", DataType.Parametro)
            Upd.Add("no_bulto", "@no_bulto", DataType.Parametro)
            Upd.Add("fecha_manufactura", "@fecha_manufactura", DataType.Parametro)
            Upd.Add("añada", "@añada", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("temperatura", "@temperatura", DataType.Parametro)
            Upd.Add("regularizado", "@regularizado", DataType.Parametro)
            Upd.Add("fecha_regularizacion", "@fecha_regularizacion", DataType.Parametro)
            Upd.Add("Atributo_Variante_1", "@Atributo_Variante_1", DataType.Parametro)
            Upd.Add("No_Linea", "@No_Linea", DataType.Parametro)
            Upd.Add("pallet_no_estandar", "@pallet_no_estandar", DataType.Parametro)
            Upd.Add("idproductotallacolor", "@idproductotallacolor", DataType.Parametro)
            Upd.Add("Talla", "@Talla", DataType.Parametro)
            Upd.Add("Color", "@Color", DataType.Parametro)
            Upd.Where("IdStockRec = @IdStockRec")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_rec.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_rec.IdStockRec))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_rec.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_rec.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeStock_rec.ProductoEstado.IdEstado = 0, DBNull.Value, oBeStock_rec.ProductoEstado.IdEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock_rec.IdPresentacion = 0, DBNull.Value, oBeStock_rec.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeStock_rec.IdUnidadMedida = 0, DBNull.Value, oBeStock_rec.IdUnidadMedida)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_rec.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion_anterior", IIf(oBeStock_rec.IdUbicacion_anterior = 0, DBNull.Value, oBeStock_rec.IdUbicacion_anterior)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeStock_rec.IdRecepcionEnc = 0, DBNull.Value, oBeStock_rec.IdRecepcionEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeStock_rec.IdRecepcionDet = 0, DBNull.Value, oBeStock_rec.IdRecepcionDet)))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IIf(oBeStock_rec.IdPedidoEnc = 0, DBNull.Value, oBeStock_rec.IdPedidoEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IIf(oBeStock_rec.IdPickingEnc = 0, DBNull.Value, oBeStock_rec.IdPickingEnc)))
            cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IIf(oBeStock_rec.IdDespachoEnc = 0, DBNull.Value, oBeStock_rec.IdDespachoEnc)))
            cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock_rec.Lote = Nothing, "", oBeStock_rec.Lote))) '#CKFK 20181011 0311PM Se quitó el DBNull.Value por "" 
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeStock_rec.Lic_plate = Nothing, DBNull.Value, oBeStock_rec.Lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeStock_rec.Serial = Nothing, DBNull.Value, oBeStock_rec.Serial)))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock_rec.Cantidad = 0, DBNull.Value, oBeStock_rec.Cantidad)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock_rec.Fecha_vence = Nothing, DBNull.Value, oBeStock_rec.Fecha_vence)))
            cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", IIf(oBeStock_rec.Uds_lic_plate = Nothing, DBNull.Value, oBeStock_rec.Uds_lic_plate)))
            cmd.Parameters.Add(New SqlParameter("@NO_BULTO", IIf(oBeStock_rec.No_bulto = 0, DBNull.Value, oBeStock_rec.No_bulto)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", IIf(oBeStock_rec.Fecha_Manufactura = Nothing, DBNull.Value, oBeStock_rec.Fecha_Manufactura)))
            cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_rec.Añada))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_rec.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_rec.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_rec.Activo))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_rec.Peso))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_rec.Temperatura))
            cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", IIf(oBeStock_rec.Regularizado = Nothing, DBNull.Value, oBeStock_rec.Regularizado)))
            cmd.Parameters.Add(New SqlParameter("@FECHA_REGULARIZACION", IIf(oBeStock_rec.Fecha_regularizacion = Nothing, DBNull.Value, oBeStock_rec.Fecha_regularizacion)))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", IIf(oBeStock_rec.Atributo_Variante_1 = String.Empty, DBNull.Value, oBeStock_rec.Atributo_Variante_1)))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeStock_rec.No_linea))
            cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_rec.Pallet_No_Estandar))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_rec.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_rec.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_rec.Color))

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

    Public Shared Function Eliminar(ByRef oBeStock_rec As clsBeStock_rec, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Stock_rec" &
             "  Where(IdStockRec = @IdStockRec)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_rec.IdStockRec))

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

    Public Shared Function Obtener(ByRef oBeStock_rec As clsBeStock_rec) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim sp As String = "SELECT * FROM Stock_rec
             Where(IdStockRec = @IdStockRec)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_rec.IdStockRec))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeStock_rec, dt.Rows(0))
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

    Public Shared Sub Bind(cmd As SqlCommand, oBeStock_rec As clsBeStock_rec)

        cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeStock_rec.IdBodega))
        cmd.Parameters.Add(New SqlParameter("@IDSTOCKREC", oBeStock_rec.IdStockRec))
        cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeStock_rec.IdPropietarioBodega))
        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock_rec.IdProductoBodega))
        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeStock_rec.ProductoEstado.IdEstado = 0, DBNull.Value, oBeStock_rec.ProductoEstado.IdEstado)))
        cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeStock_rec.IdPresentacion = 0, DBNull.Value, oBeStock_rec.IdPresentacion)))
        cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", IIf(oBeStock_rec.IdUnidadMedida = 0, DBNull.Value, oBeStock_rec.IdUnidadMedida)))
        cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock_rec.IdUbicacion))
        cmd.Parameters.Add(New SqlParameter("@IDUBICACION_ANTERIOR", IIf(oBeStock_rec.IdUbicacion_anterior = 0, DBNull.Value, oBeStock_rec.IdUbicacion_anterior)))
        cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONENC", IIf(oBeStock_rec.IdRecepcionEnc = 0, DBNull.Value, oBeStock_rec.IdRecepcionEnc)))
        cmd.Parameters.Add(New SqlParameter("@IDRECEPCIONDET", IIf(oBeStock_rec.IdRecepcionDet = 0, DBNull.Value, oBeStock_rec.IdRecepcionDet)))
        cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IIf(oBeStock_rec.IdPedidoEnc = 0, DBNull.Value, oBeStock_rec.IdPedidoEnc)))
        cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IIf(oBeStock_rec.IdPickingEnc = 0, DBNull.Value, oBeStock_rec.IdPickingEnc)))
        cmd.Parameters.Add(New SqlParameter("@IDDESPACHOENC", IIf(oBeStock_rec.IdDespachoEnc = 0, DBNull.Value, oBeStock_rec.IdDespachoEnc)))
        cmd.Parameters.Add(New SqlParameter("@LOTE", IIf(oBeStock_rec.Lote = Nothing, "", oBeStock_rec.Lote))) '#CKFK 20181011 0311PM Se quitó el DBNull.Value por "" 
        cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", IIf(oBeStock_rec.Lic_plate = Nothing, DBNull.Value, oBeStock_rec.Lic_plate)))
        cmd.Parameters.Add(New SqlParameter("@SERIAL", IIf(oBeStock_rec.Serial = Nothing, DBNull.Value, oBeStock_rec.Serial)))
        cmd.Parameters.Add(New SqlParameter("@CANTIDAD", IIf(oBeStock_rec.Cantidad = 0, DBNull.Value, oBeStock_rec.Cantidad)))
        cmd.Parameters.Add(New SqlParameter("@FECHA_INGRESO", IIf(oBeStock_rec.Fecha_Ingreso = Nothing, DBNull.Value, oBeStock_rec.Fecha_Ingreso)))
        cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", IIf(oBeStock_rec.Fecha_vence = Nothing, DBNull.Value, oBeStock_rec.Fecha_vence)))
        cmd.Parameters.Add(New SqlParameter("@UDS_LIC_PLATE", IIf(oBeStock_rec.Uds_lic_plate = Nothing, DBNull.Value, oBeStock_rec.Uds_lic_plate)))
        cmd.Parameters.Add(New SqlParameter("@NO_BULTO", oBeStock_rec.No_bulto))
        cmd.Parameters.Add(New SqlParameter("@FECHA_MANUFACTURA", IIf(oBeStock_rec.Fecha_Manufactura = Nothing, DBNull.Value, oBeStock_rec.Fecha_Manufactura)))
        cmd.Parameters.Add(New SqlParameter("@AÑADA", oBeStock_rec.Añada))
        cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeStock_rec.User_agr))
        cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeStock_rec.Fec_agr))
        cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeStock_rec.User_mod))
        cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeStock_rec.Fec_mod))
        cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeStock_rec.Activo))
        cmd.Parameters.Add(New SqlParameter("@PESO", oBeStock_rec.Peso))
        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA", oBeStock_rec.Temperatura))
        cmd.Parameters.Add(New SqlParameter("@REGULARIZADO", IIf(oBeStock_rec.Regularizado = Nothing, DBNull.Value, oBeStock_rec.Regularizado)))
        cmd.Parameters.Add(New SqlParameter("@FECHA_REGULARIZACION", IIf(oBeStock_rec.Fecha_regularizacion = Nothing, DBNull.Value, oBeStock_rec.Fecha_regularizacion)))
        cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", IIf(oBeStock_rec.Atributo_Variante_1 = String.Empty, DBNull.Value, oBeStock_rec.Atributo_Variante_1)))
        cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeStock_rec.No_linea))
        cmd.Parameters.Add(New SqlParameter("@PALLET_NO_ESTANDAR", oBeStock_rec.Pallet_No_Estandar))
        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeStock_rec.IdProductoTallaColor))
        cmd.Parameters.Add(New SqlParameter("@TALLA", oBeStock_rec.Talla))
        cmd.Parameters.Add(New SqlParameter("@COLOR", oBeStock_rec.Color))

    End Sub

End Class
