Imports System.Data.SqlClient

Public Class clsLnTrans_reabastecimiento_log

    Public Shared Sub Cargar(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log, ByRef dr As DataRow)

        Try

            With oBeTrans_reabastecimiento_log

                .IdRellenado = IIf(IsDBNull(dr.Item("IdRellenado")), 0, dr.Item("IdRellenado"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo_Producto")), "", dr.Item("Codigo_Producto"))
                .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre_Producto")), "", dr.Item("Nombre_Producto"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .NombreUmBas = IIf(IsDBNull(dr.Item("NombreUmBas")), "", dr.Item("NombreUmBas"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                .Minimo = IIf(IsDBNull(dr.Item("Minimo")), 0.0, dr.Item("Minimo"))
                .Maximo = IIf(IsDBNull(dr.Item("Maximo")), 0.0, dr.Item("Maximo"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Estado = IIf(IsDBNull(dr.Item("Estado")), "", dr.Item("Estado"))
                .StockUMBas = IIf(IsDBNull(dr.Item("StockUMBas")), 0.0, dr.Item("StockUMBas"))
                If dr.Table.Columns.Contains("ReservadoUmBas") Then .ReservadoUmBas = IIf(IsDBNull(dr.Item("ReservadoUmBas")), 0.0, dr.Item("ReservadoUmBas"))
                If dr.Table.Columns.Contains("DisponibleUMBas") Then .DisponibleUMBas = IIf(IsDBNull(dr.Item("DisponibleUMBas")), 0.0, dr.Item("DisponibleUMBas"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                If dr.Table.Columns.Contains("FactorAbastecerCon") Then .FactorAbastecerCon = IIf(IsDBNull(dr.Item("factorabastecercon")), 0.0, dr.Item("factorabastecercon"))
                If dr.Table.Columns.Contains("StockPres") Then .StockPres = IIf(IsDBNull(dr.Item("StockPres")), 0.0, dr.Item("StockPres"))
                If dr.Table.Columns.Contains("ReservadoPres") Then .ReservadoPres = IIf(IsDBNull(dr.Item("ReservadoPres")), 0.0, dr.Item("ReservadoPres"))
                If dr.Table.Columns.Contains("DisponiblePres") Then .DisponiblePres = IIf(IsDBNull(dr.Item("DisponiblePres")), 0.0, dr.Item("DisponiblePres"))
                .Ubicacion = IIf(IsDBNull(dr.Item("Ubicacion")), "", dr.Item("Ubicacion"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTipoAccion = IIf(IsDBNull(dr.Item("IdTipoAccion")), 0, dr.Item("IdTipoAccion"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Nombre_Propietario = IIf(IsDBNull(dr.Item("Nombre_Propietario")), "", dr.Item("Nombre_Propietario"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdUmBasAbastercerCon = IIf(IsDBNull(dr.Item("IdUmBasAbastercerCon")), 0, dr.Item("IdUmBasAbastercerCon"))
                .IdPresentacionAbastercerCon = IIf(IsDBNull(dr.Item("IdPresentacionAbastercerCon")), 0, dr.Item("IdPresentacionAbastercerCon"))
                .NombrePresentacionAbastecerCon = IIf(IsDBNull(dr.Item("NombrePresentacionAbastecerCon")), "", dr.Item("NombrePresentacionAbastecerCon"))

                '#EJC20210303: Este cargar se utiliza tambión para llenar a partir de una vista
                'En la vista estos valores no existe, por eso se agrega la validación.
                'Yo sí, feo, pero disminuye código y unifica.
                If dr.Table.Columns.Contains("IdReabastecimientoLog") Then .IdReabastecimientoLog = IIf(IsDBNull(dr.Item("IdReabastecimientoLog")), 0, dr.Item("IdReabastecimientoLog"))
                If dr.Table.Columns.Contains("Enviado") Then .Enviado = IIf(IsDBNull(dr.Item("Enviado")), False, dr.Item("Enviado"))
                If dr.Table.Columns.Contains("Cancelado") Then .Cancelado = IIf(IsDBNull(dr.Item("Cancelado")), False, dr.Item("Cancelado"))
                If dr.Table.Columns.Contains("Fecha_Procesamiento_BOF") Then .Fecha_Procesamiento_BOF = IIf(IsDBNull(dr.Item("Fecha_Procesamiento_BOF")), New Date(1900, 1, 1), dr.Item("Fecha_Procesamiento_BOF"))
                If dr.Table.Columns.Contains("Hora_Procesamiento_BOF") Then .Hora_Procesamiento_BOF = IIf(IsDBNull(dr.Item("Hora_Procesamiento_BOF")), New Date(1900, 1, 1), dr.Item("Hora_Procesamiento_BOF"))
                If dr.Table.Columns.Contains("IdOperadorBodega") Then .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0, dr.Item("IdOperadorBodega"))
                If dr.Table.Columns.Contains("Procesado_HH") Then .Procesado_HH = IIf(IsDBNull(dr.Item("Procesado_HH")), False, dr.Item("Procesado_HH"))
                If dr.Table.Columns.Contains("Fecha_Procesamiento_HH") Then .Fecha_Procesamiento_HH = IIf(IsDBNull(dr.Item("Fecha_Procesamiento_HH")), Date.Now, dr.Item("Fecha_Procesamiento_HH"))
                If dr.Table.Columns.Contains("Stock_Ubicacion") Then .Stock_Ubicacion = IIf(IsDBNull(dr.Item("Stock_Ubicacion")), 0, dr.Item("Stock_Ubicacion"))
                If dr.Table.Columns.Contains("Cantidad_A_Ubicar") Then .Cantidad_A_Ubicar = IIf(IsDBNull(dr.Item("Cantidad_A_Ubicar")), 0, dr.Item("Cantidad_A_Ubicar"))
                If dr.Table.Columns.Contains("Stock_Inferior") Then .Stock_Inferior = IIf(IsDBNull(dr.Item("Stock_Inferior")), 0, dr.Item("Stock_Inferior"))
                If dr.Table.Columns.Contains("Stock_Disponible") Then .Stock_Disponible = IIf(IsDBNull(dr.Item("Stock_Disponible")), 0, dr.Item("Stock_Disponible"))
                If dr.Table.Columns.Contains("IdTareaUbicacionEnc") Then .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                If dr.Table.Columns.Contains("IdOperadorBodega") Then .IdOperadorBodega = IIf(IsDBNull(dr.Item("IdOperadorBodega")), 0.0, dr.Item("IdOperadorBodega"))
                If dr.Table.Columns.Contains("CantidadSFUbicDestino") Then .CantidadSFUbicDestino = IIf(IsDBNull(dr.Item("CantidadSFUbicDestino")), 0.0, dr.Item("CantidadSFUbicDestino"))
                If dr.Table.Columns.Contains("CantidadPresUbicDestino") Then .CantidadPresUbicDestino = IIf(IsDBNull(dr.Item("CantidadPresUbicDestino")), 0.0, dr.Item("CantidadPresUbicDestino"))
                If dr.Table.Columns.Contains("CantidadReservadaUbicDestino") Then .CantidadReservadaUbicDestino = IIf(IsDBNull(dr.Item("CantidadReservadaUbicDestino")), 0.0, dr.Item("CantidadReservadaUbicDestino"))

                If dr.Table.Columns.Contains("CantidadSFDispo") Then .CantidadSFDispo = IIf(IsDBNull(dr.Item("CantidadSFDispo")), 0.0, dr.Item("CantidadSFDispo"))
                If dr.Table.Columns.Contains("CantidadPresDispo") Then .CantidadPresDispo = IIf(IsDBNull(dr.Item("CantidadPresDispo")), 0.0, dr.Item("CantidadPresDispo"))
                If dr.Table.Columns.Contains("CantidadReservadaDispo") Then .CantidadReservadaDispo = IIf(IsDBNull(dr.Item("CantidadReservadaDispo")), 0.0, dr.Item("CantidadReservadaDispo"))

                If dr.Table.Columns.Contains("IdOperadorDefecto") Then .IdOperadorDefecto = IIf(IsDBNull(dr.Item("IdOperadorDefecto")), 0, dr.Item("IdOperadorDefecto"))
                If dr.Table.Columns.Contains("Operador") Then .Operador = IIf(IsDBNull(dr.Item("Operador")), "", dr.Item("Operador"))

                If .Codigo_Producto = "40292" Then
                    Debug.WriteLine("Cargar - Codigo_Producto: " & oBeTrans_reabastecimiento_log.Codigo_Producto & " Dispo: " & oBeTrans_reabastecimiento_log.CantidadSFDispo)
                End If

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_reabastecimiento_log")
            Ins.Add("idreabastecimientolog", "@idreabastecimientolog", DataType.Parametro)
            Ins.Add("idrellenado", "@idrellenado", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("Codigo_Producto", "@Codigo_Producto", DataType.Parametro)
            Ins.Add("Nombre_Producto", "@Nombre_Producto", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("nombreumbas", "@nombreumbas", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("presentacion", "@presentacion", DataType.Parametro)
            Ins.Add("minimo", "@minimo", DataType.Parametro)
            Ins.Add("maximo", "@maximo", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("stockumbas", "@stockumbas", DataType.Parametro)
            Ins.Add("reservadoumbas", "@reservadoumbas", DataType.Parametro)
            Ins.Add("disponibleumbas", "@disponibleumbas", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("stockpres", "@stockpres", DataType.Parametro)
            Ins.Add("reservadopres", "@reservadopres", DataType.Parametro)
            Ins.Add("disponiblepres", "@disponiblepres", DataType.Parametro)
            Ins.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtipoaccion", "@idtipoaccion", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("idumbasabastercercon", "@idumbasabastercercon", DataType.Parametro)
            Ins.Add("idpresentacionabastercercon", "@idpresentacionabastercercon", DataType.Parametro)
            Ins.Add("nombrepresentacionabastecercon", "@nombrepresentacionabastecercon", DataType.Parametro)
            Ins.Add("enviado", "@enviado", DataType.Parametro)
            Ins.Add("cancelado", "@cancelado", DataType.Parametro)
            Ins.Add("fecha_generacion_inexistencia", "@fecha_generacion_inexistencia", DataType.Parametro)
            Ins.Add("hora_generacion_inexistencia", "@Hora_generacion_inexistencia", DataType.Parametro)
            Ins.Add("fecha_procesamiento_bof", "@fecha_procesamiento_bof", DataType.Parametro)
            Ins.Add("hora_procesamiento_bof", "@hora_procesamiento_bof", DataType.Parametro)
            Ins.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Ins.Add("procesado_hh", "@procesado_hh", DataType.Parametro)
            Ins.Add("fecha_procesamiento_hh", "@fecha_procesamiento_hh", DataType.Parametro)
            Ins.Add("Stock_Ubicacion", "@Stock_Ubicacion", DataType.Parametro)
            Ins.Add("Cantidad_A_Ubicar", "@Cantidad_A_Ubicar", DataType.Parametro)
            Ins.Add("Stock_Inferior", "@Stock_Inferior", DataType.Parametro)
            Ins.Add("Stock_Disponible", "@Stock_Disponible", DataType.Parametro)
            Ins.Add("IdTareaUbicacionEnc", "@IdTareaUbicacionEnc", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeTrans_reabastecimiento_log.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_reabastecimiento_log.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_reabastecimiento_log.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_reabastecimiento_log.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_reabastecimiento_log.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_reabastecimiento_log.Nombre_Producto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_reabastecimiento_log.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@NOMBREUMBAS", oBeTrans_reabastecimiento_log.NombreUmBas))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_reabastecimiento_log.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeTrans_reabastecimiento_log.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@MINIMO", oBeTrans_reabastecimiento_log.Minimo))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO", oBeTrans_reabastecimiento_log.Maximo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_reabastecimiento_log.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_reabastecimiento_log.Estado))
            cmd.Parameters.Add(New SqlParameter("@STOCKUMBAS", oBeTrans_reabastecimiento_log.StockUMBas))
            cmd.Parameters.Add(New SqlParameter("@RESERVADOUMBAS", oBeTrans_reabastecimiento_log.ReservadoUmBas))
            cmd.Parameters.Add(New SqlParameter("@DISPONIBLEUMBAS", oBeTrans_reabastecimiento_log.DisponibleUMBas))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeTrans_reabastecimiento_log.Factor))
            cmd.Parameters.Add(New SqlParameter("@STOCKPRES", oBeTrans_reabastecimiento_log.StockPres))
            cmd.Parameters.Add(New SqlParameter("@RESERVADOPRES", oBeTrans_reabastecimiento_log.ReservadoPres))
            cmd.Parameters.Add(New SqlParameter("@DISPONIBLEPRES", oBeTrans_reabastecimiento_log.DisponiblePres))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_reabastecimiento_log.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_reabastecimiento_log.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_reabastecimiento_log.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOACCION", oBeTrans_reabastecimiento_log.IdTipoAccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_reabastecimiento_log.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_reabastecimiento_log.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeTrans_reabastecimiento_log.Nombre_Propietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_reabastecimiento_log.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_reabastecimiento_log.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_reabastecimiento_log.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_reabastecimiento_log.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDUMBASABASTERCERCON", oBeTrans_reabastecimiento_log.IdUmBasAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONABASTERCERCON", oBeTrans_reabastecimiento_log.IdPresentacionAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRESENTACIONABASTECERCON", oBeTrans_reabastecimiento_log.NombrePresentacionAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_reabastecimiento_log.Enviado))
            cmd.Parameters.Add(New SqlParameter("@CANCELADO", oBeTrans_reabastecimiento_log.Cancelado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_GENERACION_INEXISTENCIA", oBeTrans_reabastecimiento_log.Fecha_Generacion_Inexistencia))
            cmd.Parameters.Add(New SqlParameter("@HORA_GENERACION_INEXISTENCIA", oBeTrans_reabastecimiento_log.Hora_Generacion_Inexistencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@HORA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Hora_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_reabastecimiento_log.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_HH", oBeTrans_reabastecimiento_log.Procesado_HH))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_HH", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_HH))
            cmd.Parameters.Add(New SqlParameter("@STOCK_UBICACION", oBeTrans_reabastecimiento_log.Stock_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_A_UBICAR", oBeTrans_reabastecimiento_log.Cantidad_A_Ubicar))
            cmd.Parameters.Add(New SqlParameter("@STOCK_INFERIOR", oBeTrans_reabastecimiento_log.Stock_Inferior))
            cmd.Parameters.Add(New SqlParameter("@STOCK_DISPONIBLE", oBeTrans_reabastecimiento_log.Stock_Disponible))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_reabastecimiento_log.IdTareaUbicacionEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_reabastecimiento_log")
            Upd.Add("idreabastecimientolog", "@idreabastecimientolog", DataType.Parametro)
            Upd.Add("idrellenado", "@idrellenado", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("Codigo_Producto", "@Codigo_Producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("nombreumbas", "@nombreumbas", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("presentacion", "@presentacion", DataType.Parametro)
            Upd.Add("minimo", "@minimo", DataType.Parametro)
            Upd.Add("maximo", "@maximo", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("stockumbas", "@stockumbas", DataType.Parametro)
            Upd.Add("reservadoumbas", "@reservadoumbas", DataType.Parametro)
            Upd.Add("disponibleumbas", "@disponibleumbas", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("stockpres", "@stockpres", DataType.Parametro)
            Upd.Add("reservadopres", "@reservadopres", DataType.Parametro)
            Upd.Add("disponiblepres", "@disponiblepres", DataType.Parametro)
            Upd.Add("ubicacion", "@ubicacion", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idtipoaccion", "@idtipoaccion", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("idumbasabastercercon", "@idumbasabastercercon", DataType.Parametro)
            Upd.Add("idpresentacionabastercercon", "@idpresentacionabastercercon", DataType.Parametro)
            Upd.Add("nombrepresentacionabastecercon", "@nombrepresentacionabastecercon", DataType.Parametro)
            Upd.Add("enviado", "@enviado", DataType.Parametro)
            Upd.Add("cancelado", "@cancelado", DataType.Parametro)
            Upd.Add("fecha_generacion_inexistencia", "@fecha_generacion_inexistencia", DataType.Parametro)
            Upd.Add("hora_generacion_inexistencia", "@Hora_generacion_inexistencia", DataType.Parametro)
            Upd.Add("fecha_procesamiento_bof", "@fecha_procesamiento_bof", DataType.Parametro)
            Upd.Add("hora_procesamiento_bof", "@hora_procesamiento_bof", DataType.Parametro)
            Upd.Add("idoperadorbodega", "@idoperadorbodega", DataType.Parametro)
            Upd.Add("procesado_hh", "@procesado_hh", DataType.Parametro)
            Upd.Add("fecha_procesamiento_hh", "@fecha_procesamiento_hh", DataType.Parametro)
            Upd.Add("Stock_Ubicacion", "@Stock_Ubicacion", DataType.Parametro)
            Upd.Add("Cantidad_A_Ubicar", "@Cantidad_A_Ubicar", DataType.Parametro)
            Upd.Add("Stock_Inferior", "@Stock_Inferior", DataType.Parametro)
            Upd.Add("Stock_Disponible", "@Stock_Disponible", DataType.Parametro)
            Upd.Add("IdTareaUbicacionEnc", "@IdTareaUbicacionEnc", DataType.Parametro)
            Upd.Where("IdReabastecimientoLog = @IdReabastecimientoLog")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeTrans_reabastecimiento_log.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_reabastecimiento_log.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_reabastecimiento_log.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeTrans_reabastecimiento_log.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_reabastecimiento_log.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", oBeTrans_reabastecimiento_log.Nombre_Producto))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_reabastecimiento_log.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@NOMBREUMBAS", oBeTrans_reabastecimiento_log.NombreUmBas))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_reabastecimiento_log.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@PRESENTACION", oBeTrans_reabastecimiento_log.Presentacion))
            cmd.Parameters.Add(New SqlParameter("@MINIMO", oBeTrans_reabastecimiento_log.Minimo))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO", oBeTrans_reabastecimiento_log.Maximo))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_reabastecimiento_log.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_reabastecimiento_log.Estado))
            cmd.Parameters.Add(New SqlParameter("@STOCKUMBAS", oBeTrans_reabastecimiento_log.StockUMBas))
            cmd.Parameters.Add(New SqlParameter("@RESERVADOUMBAS", oBeTrans_reabastecimiento_log.ReservadoUmBas))
            cmd.Parameters.Add(New SqlParameter("@DISPONIBLEUMBAS", oBeTrans_reabastecimiento_log.DisponibleUMBas))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeTrans_reabastecimiento_log.Factor))
            cmd.Parameters.Add(New SqlParameter("@STOCKPRES", oBeTrans_reabastecimiento_log.StockPres))
            cmd.Parameters.Add(New SqlParameter("@RESERVADOPRES", oBeTrans_reabastecimiento_log.ReservadoPres))
            cmd.Parameters.Add(New SqlParameter("@DISPONIBLEPRES", oBeTrans_reabastecimiento_log.DisponiblePres))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_reabastecimiento_log.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeTrans_reabastecimiento_log.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_reabastecimiento_log.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOACCION", oBeTrans_reabastecimiento_log.IdTipoAccion))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTrans_reabastecimiento_log.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeTrans_reabastecimiento_log.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeTrans_reabastecimiento_log.Nombre_Propietario))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_reabastecimiento_log.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_reabastecimiento_log.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_reabastecimiento_log.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_reabastecimiento_log.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@IDUMBASABASTERCERCON", oBeTrans_reabastecimiento_log.IdUmBasAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONABASTERCERCON", oBeTrans_reabastecimiento_log.IdPresentacionAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@NOMBREPRESENTACIONABASTECERCON", oBeTrans_reabastecimiento_log.NombrePresentacionAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@ENVIADO", oBeTrans_reabastecimiento_log.Enviado))
            cmd.Parameters.Add(New SqlParameter("@CANCELADO", oBeTrans_reabastecimiento_log.Cancelado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_GENERACION_INEXISTENCIA", oBeTrans_reabastecimiento_log.Fecha_Generacion_Inexistencia))
            cmd.Parameters.Add(New SqlParameter("@HORA_GENERACION_INEXISTENCIA", oBeTrans_reabastecimiento_log.Hora_Generacion_Inexistencia))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@HORA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Hora_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORBODEGA", oBeTrans_reabastecimiento_log.IdOperadorBodega))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_HH", oBeTrans_reabastecimiento_log.Procesado_HH))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_HH", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_HH))
            cmd.Parameters.Add(New SqlParameter("@STOCK_UBICACION", oBeTrans_reabastecimiento_log.Stock_Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_A_UBICAR", oBeTrans_reabastecimiento_log.Cantidad_A_Ubicar))
            cmd.Parameters.Add(New SqlParameter("@STOCK_INFERIOR", oBeTrans_reabastecimiento_log.Stock_Inferior))
            cmd.Parameters.Add(New SqlParameter("@STOCK_DISPONIBLE", oBeTrans_reabastecimiento_log.Stock_Disponible))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_reabastecimiento_log.IdTareaUbicacionEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_reabastecimiento_log" &
             "  Where(IdReabastecimientoLog = @IdReabastecimientoLog)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeTrans_reabastecimiento_log)

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                            Cargar(vBeTrans_reabastecimiento_log, dr)
                            lReturnList.Add(vBeTrans_reabastecimiento_log)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log)

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log" &
            " Where(IdReabastecimientoLog = @IdReabastecimientoLog)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdReabastecimientoLog", pBeTrans_reabastecimiento_log.IdReabastecimientoLog)

                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_reabastecimiento_log, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReabastecimientoLog),0) FROM Trans_reabastecimiento_log "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Existe_IdReabastecimientoLog(ByVal pIdReabastecimientoLog As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT IdReabastecimientoLog FROM Trans_reabastecimiento_log WHERE IdReabastecimientoLog = @IdReabastecimientoLog"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdReabastecimientoLog", pIdReabastecimientoLog)

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

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Existe_IdReabastecimientoLog(ByVal pIdReabastecimientoLog As Integer,
                                                        ByVal pIdRellenado As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Boolean

        Existe_IdReabastecimientoLog = False

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT IdReabastecimientoLog FROM Trans_reabastecimiento_log 
                                   WHERE IdReabastecimientoLog = @IdReabastecimientoLog AND IdRellenado = @IdRellenado "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdReabastecimientoLog", pIdReabastecimientoLog)
                lCommand.Parameters.AddWithValue("@IdRellenado", pIdRellenado)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Existe_IdReabastecimientoLog = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_IdReabastecimientoLog(ByVal pIdReabastecimientoLog As Integer,
                                                        ByVal pIdRellenado As Integer,
                                                        ByVal pfecha_generacion_inexistencia As Date,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Boolean

        Existe_IdReabastecimientoLog = False

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT IdReabastecimientoLog FROM Trans_reabastecimiento_log 
                                   WHERE IdReabastecimientoLog = @IdReabastecimientoLog AND IdRellenado = @IdRellenado AND fecha_generacion_inexistencia = @fecha_generacion_inexistencia "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim vFechaGeneracionInexistencia As String = FormatoFechas.tFecha(pfecha_generacion_inexistencia)

                lCommand.Parameters.AddWithValue("@IdReabastecimientoLog", pIdReabastecimientoLog)
                lCommand.Parameters.AddWithValue("@IdRellenado", pIdRellenado)
                lCommand.Parameters.AddWithValue("@fecha_generacion_inexistencia", pfecha_generacion_inexistencia.Date)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Existe_IdReabastecimientoLog = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
