Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_stock

    Public Shared Sub Guarda_Copia_Stock(ByRef pObjE As clsBeTrans_inv_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)
        Dim Stock As New List(Of clsBeStock)
        Dim Copia As New clsBeTrans_inv_stock

        Try

            If pObjE.IsNew And pObjE.Inicial = False Then

                Stock = clsLnStock.GetAll()

                If Stock.Count > 0 Then

                    For Each Obj As clsBeStock In Stock

                        Copia = New clsBeTrans_inv_stock()
                        Copia.Idinventario = pObjE.Idinventarioenc
                        Copia.IdStock = Obj.IdStock
                        Copia.IdBodega = Obj.IdBodega
                        Copia.IdPropietarioBodega = Obj.IdPropietarioBodega
                        Copia.IdProductoBodega = Obj.IdProductoBodega
                        Copia.IdProductoEstado = Obj.IdProductoEstado
                        Copia.IdPresentacion = Obj.IdPresentacion
                        Copia.IdUnidadMedida = Obj.IdUnidadMedida
                        Copia.IdUbicacion = Obj.IdUbicacion
                        Copia.IdUbicacion_anterior = Obj.IdUbicacion_anterior
                        Copia.IdRecepcionEnc = Obj.IdRecepcionEnc
                        Copia.IdRecepcionDet = Obj.IdRecepcionDet
                        Copia.IdPedidoEnc = Obj.IdPedidoEnc
                        Copia.IdPickingEnc = Obj.IdPickingEnc
                        Copia.IdDespachoEnc = Obj.IdDespachoEnc
                        Copia.Lote = Obj.Lote
                        Copia.Lic_plate = Obj.Lic_plate
                        Copia.Serial = Obj.Serial
                        Copia.Cantidad = Obj.Cantidad
                        Copia.Fecha_ingreso = Obj.Fecha_Ingreso
                        Copia.Fecha_vence = Obj.Fecha_vence
                        Copia.Uds_lic_plate = Obj.Uds_lic_plate
                        Copia.No_bulto = Copia.No_bulto
                        Copia.Fecha_manufactura = Obj.Fecha_Manufactura
                        Copia.Añada = Obj.Añada
                        Copia.User_agr = Obj.User_agr
                        Copia.Fec_agr = Obj.Fec_agr
                        Copia.User_mod = Obj.User_mod
                        Copia.Fec_mod = Obj.Fec_mod
                        Copia.Activo = Obj.Activo
                        Copia.Peso = Obj.Peso
                        Copia.Temperatura = Obj.Temperatura
                        Copia.fecha_copia = pObjE.Fec_agr

                        Insertar(Copia, lConnection, lTransaction)

                    Next

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guarda_Copia_Stock(ByRef pObjE As clsBeTrans_inv_enc,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction,
                                         ByRef prg As ProgressBar,
                                         ByRef lblprg As Label)
        Dim Stock As New List(Of clsBeStock)
        Dim Copia As New clsBeTrans_inv_stock
        Dim vContador As Integer = 0

        Try

            lblprg.Visible = True
            prg.Visible = True

            If pObjE.IsNew And pObjE.Inicial = False Then

                lblprg.Text = "Obteniendo imagen de inventario actual"
                lblprg.Refresh()

                '#EJC20180628: Agruegué transacción a consulta para copia de inventario
                Stock = clsLnStock.GetAll(lConnection, lTransaction)

                If Stock.Count > 0 Then

                    prg.Maximum = Stock.Count

                    For Each Obj As clsBeStock In Stock

                        Copia = New clsBeTrans_inv_stock()
                        Copia.Idinventario = pObjE.Idinventarioenc
                        Copia.IdStock = Obj.IdStock
                        Copia.IdBodega = Obj.IdBodega
                        Copia.IdPropietarioBodega = Obj.IdPropietarioBodega
                        Copia.IdProductoBodega = Obj.IdProductoBodega
                        Copia.IdProductoEstado = Obj.IdProductoEstado
                        Copia.IdPresentacion = Obj.IdPresentacion
                        Copia.IdUnidadMedida = Obj.IdUnidadMedida
                        Copia.IdUbicacion = Obj.IdUbicacion
                        Copia.IdUbicacion_anterior = Obj.IdUbicacion_anterior
                        Copia.IdRecepcionEnc = Obj.IdRecepcionEnc
                        Copia.IdRecepcionDet = Obj.IdRecepcionDet
                        Copia.IdPedidoEnc = Obj.IdPedidoEnc
                        Copia.IdPickingEnc = Obj.IdPickingEnc
                        Copia.IdDespachoEnc = Obj.IdDespachoEnc
                        Copia.Lote = Obj.Lote
                        Copia.Lic_plate = Obj.Lic_plate
                        Copia.Serial = Obj.Serial
                        Copia.Cantidad = Obj.Cantidad
                        Copia.Fecha_ingreso = Obj.Fecha_Ingreso
                        Copia.Fecha_vence = Obj.Fecha_vence
                        Copia.Uds_lic_plate = Obj.Uds_lic_plate
                        Copia.No_bulto = Copia.No_bulto
                        Copia.Fecha_manufactura = Obj.Fecha_Manufactura
                        Copia.Añada = Obj.Añada
                        Copia.User_agr = Obj.User_agr
                        Copia.Fec_agr = Obj.Fec_agr
                        Copia.User_mod = Obj.User_mod
                        Copia.Fec_mod = Obj.Fec_mod
                        Copia.Activo = Obj.Activo
                        Copia.Peso = Obj.Peso
                        Copia.Temperatura = Obj.Temperatura
                        Copia.fecha_copia = pObjE.Fec_agr

                        Insertar(Copia, lConnection, lTransaction)

                        prg.Value = vContador
                        vContador += 1

                        lblprg.Text = "Generando imagen de inventario para Identificador: " & Obj.IdStock
                        lblprg.Refresh()

                    Next

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKFK 20180926 Modifiqué la función Generar_Invenatario_Congelado para que cuando el lote sea nulo guarde en el inventario congelado la cadena vacía.
    Public Shared Function Generar_Invenatario_Congelado(ByVal IdInventarioEnc As Integer,
                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "INSERT INTO trans_inv_stock
                                ([idinventario], 
                                [IdStock],
                                [IdPropietarioBodega],
                                [IdProductoBodega],
                                [IdProductoEstado],
                                [IdPresentacion],
                                [IdUnidadMedida],
                                [IdUbicacion],
                                [IdUbicacion_anterior],
                                [IdRecepcionEnc],
                                [IdRecepcionDet],
                                [IdPedidoEnc],
                                [IdPickingEnc],
                                [IdDespachoEnc],
                                [lote],
                                [lic_plate],
                                [serial],
                                [cantidad],
                                [fecha_ingreso],
                                [fecha_vence],
                                [uds_lic_plate],
                                [no_bulto],
                                [fecha_manufactura],
                                [añada],
                                [user_agr],
                                [fec_agr],
                                [user_mod],
                                [fec_mod],
                                [activo],
                                [peso],
                                [temperatura],
                                [fecha_copia],
                                [IdBodega],
                                [IdProductoTallaColor],
                                [cantidad_reservada_umbas])
                                Select @idinventario,
                                    s.[IdStock],
                                    s.[IdPropietarioBodega],
                                    s.[IdProductoBodega],
                                    s.[IdProductoEstado],
                                    s.[IdPresentacion],
                                    s.[IdUnidadMedida],
                                    s.[IdUbicacion],
                                    s.[IdUbicacion_anterior],
                                    s.[IdRecepcionEnc],
                                    s.[IdRecepcionDet],
                                    s.[IdPedidoEnc], [IdPickingEnc],
                                    s.[IdDespachoEnc],
                                    ISNULL(s.[lote], '') AS lote, 
                                    s.[lic_plate],
                                    s.[serial],
                                    s.[cantidad],
                                    s.[fecha_ingreso],
                                    ISNULL(s.[fecha_vence],'19000101') AS fecha_vence, 
                                    s.[uds_lic_plate],
                                    s.[no_bulto],
                                    s.[fecha_manufactura],
                                    s.[añada],
                                    s.[user_agr],
                                    s.[fec_agr],
                                    s.[user_mod],
                                    s.[fec_mod],
                                    s.[activo],
                                    s.[peso],
                                    s.[temperatura],
                                    GETDATE(),
                                    s.[IdBodega],
                                    s.IdProductoTallaColor,
                                    ISNULL(SUM(sr.cantidad), 0) AS cantidad_reservada_umbas 
                                    From stock s
                                    Left Join stock_res sr ON sr.IdStock = s.IdStock
                                    GROUP BY 
                                        s.[IdStock],
                                        s.[IdPropietarioBodega],
                                        s.[IdProductoBodega],
                                        s.[IdProductoEstado],
                                        s.[IdPresentacion],
                                        s.[IdUnidadMedida],
                                        s.[IdUbicacion],
                                        s.[IdUbicacion_anterior],
                                        s.[IdRecepcionEnc],
                                        s.[IdRecepcionDet],
                                        s.[IdPedidoEnc],
                                        s.[IdPickingEnc],
                                        s.[IdDespachoEnc],
                                        s.[lote],
                                        s.[lic_plate],
                                        s.[serial],
                                        s.[cantidad],
                                        s.[fecha_ingreso],
                                        s.[fecha_vence],
                                        s.[uds_lic_plate],
                                        s.[no_bulto],
                                        s.[fecha_manufactura],
                                        s.[añada],
                                        s.[user_agr],
                                        s.[fec_agr],
                                        s.[user_mod],
                                        s.[fec_mod],
                                        s.[activo],
                                        s.[peso],
                                        s.[temperatura],
                                        s.[IdBodega],
                                        s.[IdProductoTallaColor]"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", IdInventarioEnc))


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

    'Public Shared Function GetAllByInventario(ByVal IdInventario As Integer) As List(Of clsBeTrans_inv_stock)

    '    Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_inv_stock)

    '        '#CKFK 20180627 modifiqué la forma de obtener el nombre completo de la ubicacion
    '        Dim vSQL As String = "Select trans_inv_stock.IdStock, trans_inv_stock.IdUbicacion, trans_inv_stock.idinventario, trans_inv_stock.lote, trans_inv_stock.lic_plate, 
    '                     trans_inv_stock.cantidad, trans_inv_stock.fecha_ingreso, trans_inv_stock.fecha_vence, trans_inv_stock.peso, trans_inv_stock.fecha_copia, 
    '                     producto.codigo AS Codigo, producto.nombre AS Producto, unidad_medida.Nombre AS UMBas, 
    '                     producto_presentacion.nombre AS Presentacion, CASE WHEN T.es_rack = 1 
    '                               THEN
    '                                    'R' + RIGHT('00'+ SUBSTRING(t.descripcion,2,iif(CHARINDEX('-',t.descripcion,0)<0,1,CHARINDEX('-',t.descripcion,0)-2)),2) + ' - ' +
    '                                    'C' + RIGHT('00'+ CONVERT(NVARCHAR(10),u.indice_x),2 )+ ' - ' +
    '                                    'T' + SUBSTRING(t.descripcion,iif(CHARINDEX('-',t.descripcion,0)<0,0,CHARINDEX('-',t.descripcion,0)+1),1)+ ' - ' +
    '                                    'N' + RIGHT('00' + CONVERT(NVARCHAR(10),u.nivel),2 )+ ' - ' +
    '                                    'Pos' + u.orientacion_pos+ ' - ' +
    '                                    '#' + CONVERT(NVARCHAR(10),u.IdUbicacion)
    '                                ELSE
    '                                    T.descripcion END AS Nombre_Completo, pt.NombreTipoProducto AS TipoProducto
    '                FROM trans_inv_stock INNER JOIN
    '                     producto_bodega ON trans_inv_stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
    '                     producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
    '                     producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion LEFT OUTER JOIN
    '                     unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida 
    '                     inner join bodega_ubicacion u on trans_inv_stock.IdUbicacion = u.IdUbicacion  inner join bodega_tramo t on u.idtramo = t.idtramo
    '                     inner join producto_tipo pt on producto.IdTipoProducto = pt.IdTipoProducto "

    '        vSQL += "WHERE trans_inv_stock.IdInventario=@IdInventario"

    '        lConection.Open()

    '        lTransaction = lConection.BeginTransaction()

    '        Dim cmd As New SqlCommand(vSQL, lConection, lTransaction) With {.CommandType = CommandType.Text}
    '        Dim dad As New SqlDataAdapter(cmd)

    '        dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))

    '        Dim dt As New DataTable

    '        dad.Fill(dt)

    '        Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

    '        For Each dr As DataRow In dt.Rows

    '            vBeTrans_inv_stock = New clsBeTrans_inv_stock
    '            'Cargar(vBeTrans_inv_stock, dr)

    '            If dr("IdStock") IsNot DBNull.Value AndAlso dr("IdStock") IsNot Nothing Then
    '                vBeTrans_inv_stock.IdStock = CType(dr("IdStock"), Integer)
    '            End If

    '            If dr("IdUbicacion") IsNot DBNull.Value AndAlso dr("IdUbicacion") IsNot Nothing Then
    '                vBeTrans_inv_stock.IdUbicacion = CType(dr("IdUbicacion"), Integer)
    '                vBeTrans_inv_stock.Ubicacion = CType(dr("Nombre_Completo"), String)
    '                'clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vBeTrans_inv_stock.IdUbicacion, lConection, lTransaction) 
    '            End If

    '            If dr("idinventario") IsNot DBNull.Value AndAlso dr("idinventario") IsNot Nothing Then
    '                vBeTrans_inv_stock.Idinventario = CType(dr("idinventario"), Integer)
    '            End If

    '            If dr("lote") IsNot DBNull.Value AndAlso dr("lote") IsNot Nothing Then
    '                vBeTrans_inv_stock.Lote = CType(dr("lote"), String)
    '            End If

    '            If dr("fecha_vence") IsNot DBNull.Value AndAlso dr("fecha_vence") IsNot Nothing Then
    '                vBeTrans_inv_stock.Fecha_vence = CType(dr("fecha_vence"), Date)
    '            End If

    '            If dr("peso") IsNot DBNull.Value AndAlso dr("peso") IsNot Nothing Then
    '                vBeTrans_inv_stock.Peso = CType(dr("peso"), Double)
    '            End If

    '            If dr("cantidad") IsNot DBNull.Value AndAlso dr("cantidad") IsNot Nothing Then
    '                vBeTrans_inv_stock.Cantidad = CType(dr("cantidad"), Double)
    '            End If

    '            If dr("Codigo") IsNot DBNull.Value AndAlso dr("Codigo") IsNot Nothing Then
    '                vBeTrans_inv_stock.Codigo = CType(dr("Codigo"), String)
    '            End If

    '            If dr("Producto") IsNot DBNull.Value AndAlso dr("Producto") IsNot Nothing Then
    '                vBeTrans_inv_stock.Producto = CType(dr("Producto"), String)
    '            End If

    '            If dr("UMBas") IsNot DBNull.Value AndAlso dr("UMBas") IsNot Nothing Then
    '                vBeTrans_inv_stock.UMBas = CType(dr("UMBas"), String)
    '            End If

    '            If dr("Presentacion") IsNot DBNull.Value AndAlso dr("Presentacion") IsNot Nothing Then
    '                vBeTrans_inv_stock.Presentacion = CType(dr("Presentacion"), String)
    '            End If

    '            If dr("TipoProducto") IsNot DBNull.Value AndAlso dr("TipoProducto") IsNot Nothing Then
    '                vBeTrans_inv_stock.TipoProducto = CType(dr("TipoProducto"), String)
    '            End If

    '            lReturnList.Add(vBeTrans_inv_stock)

    '        Next

    '        lTransaction.Commit()

    '        Return lReturnList

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If Not lConection Is Nothing AndAlso lConection.State = ConnectionState.Open Then lConection.Close()
    '    End Try

    'End Function

    Public Shared Function Get_All_By_IdUbicacion(ByVal IdInventario As Integer,
                                                  ByVal IdUbicacion As Integer) As List(Of clsBeTrans_inv_stock)

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * from trans_inv_stock"

            vSQL += " WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdUbicacion=@IdUbicacion"

            lConection.Open()

            lTransaction = lConection.BeginTransaction()

            Dim cmd As New SqlCommand(vSQL, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)

            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConection Is Nothing AndAlso lConection.State = ConnectionState.Open Then lConection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal IdInventario As Integer, ByVal IdBodega As Integer) As DataTable

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#CKFK 20180627 modifiqué la forma de obtener el nombre completo de la ubicacion
            Dim vSQL As String = "SELECT producto.codigo AS Codigo, producto.nombre AS Producto, unidad_medida.Nombre AS UMBas, producto_presentacion.nombre AS Presentacion,
                                    producto_estado.nombre AS Estado, trans_inv_stock.Lote, trans_inv_stock.Fecha_vence, trans_inv_stock.Cantidad,  trans_inv_stock.Peso,
                                    dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion,trans_inv_stock.IdBodega) AS Ubicacion, trans_inv_stock.IdStock, pt.NombreTipoProducto AS TipoProducto, trans_inv_stock.IdUbicacion, trans_inv_stock.IdProductoBodega
                                    FROM trans_inv_stock INNER JOIN
                                    producto_bodega ON trans_inv_stock.IdProductoBodega = producto_bodega.IdProductoBodega 
                                    INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto 
                                    LEFT OUTER JOIN producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion LEFT OUTER JOIN
                                    unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida 
                                    inner join producto_tipo pt on producto.IdTipoProducto = pt.IdTipoProducto 
                                    inner join producto_estado on trans_inv_stock.IdProductoEstado = producto_estado.IdEstado "

            vSQL += "WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdBodega = @IdBodega"

            lConection.Open() : lTransaction = lConection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(vSQL, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable

            dad.Fill(dt)

            Return dt

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConection Is Nothing AndAlso lConection.State = ConnectionState.Open Then lConection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdProductoBodega(ByVal IdInventario As Integer, ByVal IdProductoBodega As Integer) As List(Of clsBeTrans_inv_stock)

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * from trans_inv_stock"

            vSQL += " WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdProductoBodega=@IdUbicacion"

            lConection.Open()

            lTransaction = lConection.BeginTransaction()

            Dim cmd As New SqlCommand(vSQL, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)

            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConection Is Nothing AndAlso lConection.State = ConnectionState.Open Then lConection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdProductoBodega(ByVal IdInventario As Integer,
                                                                           ByVal IdProductoBodega As Integer,
                                                                           ByRef lConnection As SqlConnection,
                                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * from trans_inv_stock "

            vSQL += " WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdProductoBodega=@IdProductoBodega"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Inventario_Congelado(ByVal DTProductos As DataTable,
                                                         ByVal IdInventarioEnc As Integer,
                                                         ByVal IdUsuarioAgrego As Integer,
                                                         ByVal IdOperadorAsignado As Integer) As Boolean

        Insertar_Inventario_Congelado = False

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lInvCongelado As New List(Of clsBeTrans_inv_stock)
        Dim gBeInventarioCiclico As New clsBeTrans_inv_ciclico
        Dim Operador As New clsBeTrans_inv_operador
        Dim Ubicacion As New clsBeTrans_inv_ciclico_ubic
        Dim inserto_inv As Boolean = False
        Dim cantReg As Integer = 0
        Dim cantProd As Integer = 0

        Try

            Dim ProductosSeleccionados = DTProductos.AsEnumerable().[Select](Function(row) New With {
                                                                                                    Key .IdProductoBodega = row.Field(Of Integer)("IdProductoBodega"),
                                                                                                    Key .Nombre = row.Field(Of String)("Nombre"),
                                                                                                    Key .Seleccionado = row.Field(Of Boolean)("Seleccionar")}).Where(Function(e) e.Seleccionado = True).Distinct().ToArray().OrderBy(Function(x) x.IdProductoBodega)

            If ProductosSeleccionados.Count > 0 Then

                lConection.Open() : lTransaction = lConection.BeginTransaction(IsolationLevel.ReadUncommitted)

                For Each ProdInv In ProductosSeleccionados

                    lInvCongelado = Get_All_By_IdInventarioEnc_And_IdProductoBodega(IdInventarioEnc,
                                                                                    ProdInv.IdProductoBodega,
                                                                                    lConection,
                                                                                    lTransaction)
                    cantReg = 0

                    'GT 02092021 1222: si hay existencia iterar
                    If lInvCongelado.Count > 0 Then

                        For Each StockCongelado In lInvCongelado

                            gBeInventarioCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(lConection, lTransaction)
                            gBeInventarioCiclico.Idinventarioenc = IdInventarioEnc
                            gBeInventarioCiclico.IdStock = StockCongelado.IdStock
                            gBeInventarioCiclico.IdProductoBodega = StockCongelado.IdProductoBodega
                            gBeInventarioCiclico.IdProductoEstado = StockCongelado.IdProductoEstado
                            gBeInventarioCiclico.IdProductoEst_nuevo = StockCongelado.IdProductoEstado
                            gBeInventarioCiclico.IdPresentacion = StockCongelado.IdPresentacion
                            gBeInventarioCiclico.IdUbicacion = StockCongelado.IdUbicacion
                            '#EJC20180924: Faltante, no sé porqupe, pero estos son de los detalles que a veces solo yo me puedo encontrar.
                            gBeInventarioCiclico.IdUnidadMedida = StockCongelado.IdUnidadMedida
                            gBeInventarioCiclico.Lote_stock = StockCongelado.Lote
                            gBeInventarioCiclico.Lote = StockCongelado.Lote
                            gBeInventarioCiclico.Fecha_vence_stock = StockCongelado.Fecha_vence
                            gBeInventarioCiclico.Fecha_vence = StockCongelado.Fecha_vence
                            gBeInventarioCiclico.Cant_stock = StockCongelado.Cantidad
                            gBeInventarioCiclico.Peso_stock = StockCongelado.Peso
                            gBeInventarioCiclico.EsNuevo = False
                            gBeInventarioCiclico.Idoperador = IdOperadorAsignado
                            gBeInventarioCiclico.User_agr = IdUsuarioAgrego
                            gBeInventarioCiclico.Fec_agr = Now
                            gBeInventarioCiclico.Cantidad = 0.0
                            gBeInventarioCiclico.EsPallet = False 'StockCongelado.IdPresentacion Is Pallet ? -> EJC20180807
                            gBeInventarioCiclico.lic_plate = StockCongelado.Lic_plate
                            gBeInventarioCiclico.IdBodega = StockCongelado.IdBodega
                            gBeInventarioCiclico.IdProductoTallaColor = StockCongelado.IdProductoTallaColor
                            gBeInventarioCiclico.IdProductoTallaColor_nuevo = StockCongelado.IdProductoTallaColor
                            gBeInventarioCiclico.Cantidad_Reservada_UMBas = StockCongelado.Cantidad_Reservada_UMBas

                            clsLnTrans_inv_ciclico.Insertar(gBeInventarioCiclico, lConection, lTransaction)

                            Operador = New clsBeTrans_inv_operador
                            Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(lConection, lTransaction)
                            Operador.Idinventarioenc = gBeInventarioCiclico.Idinventarioenc
                            Operador.Idinvencreconteo = 0
                            Operador.Idubic = StockCongelado.IdUbicacion
                            Operador.IdBodega = StockCongelado.IdBodega
                            Operador.Idoperador = IdOperadorAsignado

                            If Not clsLnTrans_inv_operador.Existe_Operador_By_IdUbicacion(Operador,
                                                                                          lConection,
                                                                                          lTransaction) Then
                                clsLnTrans_inv_operador.Insertar(Operador,
                                                                 lConection,
                                                                 lTransaction)
                            End If

                            If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(StockCongelado.IdUbicacion,
                                                                                IdInventarioEnc,
                                                                                lConection,
                                                                                lTransaction) Then
                                Ubicacion = New clsBeTrans_inv_ciclico_ubic
                                Ubicacion.Idubicacion = StockCongelado.IdUbicacion
                                Ubicacion.Idinventarioenc = IdInventarioEnc
                                Ubicacion.IdBodega = StockCongelado.IdBodega

                                clsLnTrans_inv_ciclico_ubic.Insertar(Ubicacion,
                                                                     lConection,
                                                                     lTransaction)

                            End If

                            cantReg += 1
                            Debug.Print("Registro " & cantReg & " Procesando interno IdStock: " & StockCongelado.IdStock)

                        Next

                        inserto_inv = True
                    Else

                        inserto_inv = False
                    End If

                    cantProd += 1
                    Debug.Print("Producto " & cantProd & " Procesando Externo Código: " & ProdInv.Nombre)

                Next

                lTransaction.Commit()

            Else
                Throw New Exception("No se marcó ningún registro para asignar al inventario")
            End If

            Insertar_Inventario_Congelado = inserto_inv

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConection.State = ConnectionState.Open Then lConection.Close()
            lConection.Dispose()
        End Try

    End Function

    Public Shared Function Insertar_Inventario_Congelado(ByVal DTProductos As DataTable,
                                                         ByVal IdInventarioEnc As Integer,
                                                         ByVal IdUsuarioAgrego As Integer,
                                                         ByVal IdOperadorAsignado As Integer,
                                                         ByVal lTransInvUbic As List(Of clsBeTrans_inv_ciclico_ubic)) As Boolean

        Insertar_Inventario_Congelado = False

        Dim lConection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lInvCongelado As New List(Of clsBeTrans_inv_stock)
        Dim gBeInventarioCiclico As New clsBeTrans_inv_ciclico
        Dim Operador As New clsBeTrans_inv_operador
        Dim Ubicacion As New clsBeTrans_inv_ciclico_ubic
        Dim vRegistrosProcesados As Integer = 0
        Dim lStockInsertados As New List(Of Integer)

        Try

            Dim ProductosSeleccionados = DTProductos.AsEnumerable().[Select](Function(row) New With {
                                                                                                    Key .IdProductoBodega = row.Field(Of Integer)("IdProductoBodega"),
                                                                                                    Key .Nombre = row.Field(Of String)("Nombre"),
                                                                                                    Key .Seleccionado = row.Field(Of Boolean)("Seleccionar")}).Where(Function(e) e.Seleccionado = True).Distinct().ToArray().OrderBy(Function(x) x.IdProductoBodega)

            If ProductosSeleccionados.Count > 0 Then

                lConection.Open() : lTransaction = lConection.BeginTransaction(IsolationLevel.ReadUncommitted)

                For Each ProdInv In ProductosSeleccionados

                    lInvCongelado = Get_All_By_IdInventarioEnc_And_IdProductoBodega(IdInventarioEnc,
                                                                                    ProdInv.IdProductoBodega,
                                                                                    lConection,
                                                                                    lTransaction)

                    lStockInsertados = New List(Of Integer)

                    If lInvCongelado.Count > 0 Then

                        For Each UbicInv In lTransInvUbic

                            '#EJC20220119:Mejora,Mercopan: Agregar al inventario los productos que están en las ubicaciones previamente definidas.                            
                            For Each StockCongelado In lInvCongelado.Where(Function(x) x.IdBodega = UbicInv.IdBodega)

                                If Not lStockInsertados.Contains(StockCongelado.IdStock) Then

                                    gBeInventarioCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(lConection, lTransaction)
                                    gBeInventarioCiclico.Idinventarioenc = IdInventarioEnc
                                    gBeInventarioCiclico.IdStock = StockCongelado.IdStock
                                    gBeInventarioCiclico.IdProductoBodega = StockCongelado.IdProductoBodega
                                    gBeInventarioCiclico.IdProductoEstado = StockCongelado.IdProductoEstado
                                    gBeInventarioCiclico.IdProductoEst_nuevo = StockCongelado.IdProductoEstado
                                    gBeInventarioCiclico.IdPresentacion = StockCongelado.IdPresentacion
                                    gBeInventarioCiclico.IdUbicacion = StockCongelado.IdUbicacion
                                    '#EJC20180924: Faltante, no sé porqupe, pero estos son de los detalles que a veces solo yo me puedo encontrar.
                                    gBeInventarioCiclico.IdUnidadMedida = StockCongelado.IdUnidadMedida
                                    gBeInventarioCiclico.Lote_stock = StockCongelado.Lote
                                    gBeInventarioCiclico.Lote = StockCongelado.Lote
                                    gBeInventarioCiclico.Fecha_vence_stock = StockCongelado.Fecha_vence
                                    gBeInventarioCiclico.Fecha_vence = StockCongelado.Fecha_vence
                                    gBeInventarioCiclico.Cant_stock = StockCongelado.Cantidad
                                    gBeInventarioCiclico.Peso_stock = StockCongelado.Peso
                                    gBeInventarioCiclico.EsNuevo = False
                                    gBeInventarioCiclico.Idoperador = IdOperadorAsignado
                                    gBeInventarioCiclico.User_agr = IdUsuarioAgrego
                                    gBeInventarioCiclico.Fec_agr = Now
                                    gBeInventarioCiclico.Cantidad = 0.0
                                    gBeInventarioCiclico.EsPallet = False 'StockCongelado.IdPresentacion Is Pallet ? -> EJC20180807
                                    gBeInventarioCiclico.lic_plate = StockCongelado.Lic_plate
                                    gBeInventarioCiclico.IdBodega = StockCongelado.IdBodega
                                    gBeInventarioCiclico.IdProductoTallaColor = StockCongelado.IdProductoTallaColor
                                    gBeInventarioCiclico.IdProductoTallaColor_nuevo = StockCongelado.IdProductoTallaColor
                                    gBeInventarioCiclico.Cantidad_Reservada_UMBas = StockCongelado.Cantidad_Reservada_UMBas

                                    clsLnTrans_inv_ciclico.Insertar(gBeInventarioCiclico, lConection, lTransaction)

                                    lStockInsertados.Add(gBeInventarioCiclico.IdStock)

                                    Operador = New clsBeTrans_inv_operador
                                    Operador.Idinvoperador = clsLnTrans_inv_operador.MaxID(lConection, lTransaction)
                                    Operador.Idinventarioenc = gBeInventarioCiclico.Idinventarioenc
                                    Operador.Idinvencreconteo = 0
                                    Operador.Idubic = StockCongelado.IdUbicacion
                                    Operador.IdBodega = StockCongelado.IdBodega
                                    Operador.Idoperador = IdOperadorAsignado

                                    If Not clsLnTrans_inv_operador.Existe_Operador_By_IdUbicacion(Operador,
                                                                                                  lConection,
                                                                                                  lTransaction) Then
                                        clsLnTrans_inv_operador.Insertar(Operador,
                                                                         lConection,
                                                                         lTransaction)
                                    End If

                                    If Not clsLnTrans_inv_ciclico_ubic.Existe_Ubicacion(StockCongelado.IdUbicacion,
                                                                                        IdInventarioEnc,
                                                                                        lConection,
                                                                                        lTransaction) Then
                                        Ubicacion = New clsBeTrans_inv_ciclico_ubic
                                        Ubicacion.Idubicacion = StockCongelado.IdUbicacion
                                        Ubicacion.Idinventarioenc = IdInventarioEnc
                                        Ubicacion.IdBodega = StockCongelado.IdBodega

                                        clsLnTrans_inv_ciclico_ubic.Insertar(Ubicacion,
                                                                             lConection,
                                                                             lTransaction)

                                    End If

                                    Debug.Print("Procesando interno IdStock: " & StockCongelado.IdStock)

                                    vRegistrosProcesados += 1

                                    lStockInsertados.Add(StockCongelado.IdStock)

                                End If

                            Next

                        Next

                        Insertar_Inventario_Congelado = (vRegistrosProcesados > 0)

                    End If

                    Debug.Print("Procesando Externo Código: " & ProdInv.Nombre)

                Next

                lTransaction.Commit()

            Else
                Throw New Exception("No se marcó ningún registro para asignar al inventario")
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConection.State = ConnectionState.Open Then lConection.Close()
            lConection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdProductoBodega(ByVal IdInventario As Integer,
                                                                           ByVal IdProductoBodega As Integer,
                                                                           ByVal IdUbicacion As Integer,
                                                                           ByRef lConnection As SqlConnection,
                                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_stock)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_stock)

            Dim vSQL As String = "SELECT * from trans_inv_stock "

            vSQL += " WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdProductoBodega=@IdProductoBodega AND trans_inv_stock.IdUbicacion=@IdUbicacion "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_stock As New clsBeTrans_inv_stock

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_stock = New clsBeTrans_inv_stock
                Cargar(vBeTrans_inv_stock, dr)
                lReturnList.Add(vBeTrans_inv_stock)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdInventarioEnc(ByVal IdInventario As Integer, ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try
            '#ejc, agruegué transacción: 241211
            '#CKFK 20180627 modifiqué la forma de obtener el nombre completo de la ubicacion
            Dim vSQL As String =
            "SELECT " &
            "    producto.codigo AS Codigo, " &
            "    producto.nombre AS Producto, " &
            " CASE " &
            " WHEN producto_presentacion.nombre IS NULL OR producto_presentacion.nombre = '' " &
            " THEN unidad_medida.Nombre " &
            " ELSE producto_presentacion.nombre " &
            " END AS Presentacion, " &
            "    producto_estado.nombre AS Estado, " &
            "    trans_inv_stock.Lote, " &
            "    trans_inv_stock.Fecha_vence, " &
            "    IIF(trans_inv_stock.IdPresentacion <> 0, trans_inv_stock.Cantidad / NULLIF(producto_presentacion.Factor,0), 0) AS Cantidad_Pres, " &
            "    trans_inv_stock.Peso, " &
            "    dbo.Nombre_Completo_Ubicacion(trans_inv_stock.IdUbicacion, trans_inv_stock.IdBodega) AS Ubicacion, " &
            "    trans_inv_stock.IdStock, " &
            "    trans_inv_stock.lic_plate AS Licencia, " &
            "    pt.NombreTipoProducto AS TipoProducto, " &
            "    trans_inv_stock.IdUbicacion, " &
            "    trans_inv_stock.IdProductoBodega, " &
            "   CASE " &
            "   WHEN producto_presentacion.nombre IS NULL OR producto_presentacion.nombre = '' " &
            "   THEN ISNULL(trans_inv_stock.cantidad_reservada_umbas, 0) " &
            "   WHEN producto_presentacion.Factor > 0 " &
            "   THEN ISNULL(trans_inv_stock.cantidad_reservada_umbas, 0) / producto_presentacion.Factor " &
            "   ELSE 0 " &
            "   END AS Cantidad_Reservada_Pres, " &
            "    color.nombre AS Color, " &
            "    talla.codigo AS Talla " &
            "FROM trans_inv_stock " &
            "INNER JOIN producto_bodega ON trans_inv_stock.IdProductoBodega = producto_bodega.IdProductoBodega " &
            "INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto " &
            "LEFT OUTER JOIN producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion " &
            "LEFT OUTER JOIN unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida " &
            "INNER JOIN producto_tipo pt ON producto.IdTipoProducto = pt.IdTipoProducto " &
            "INNER JOIN producto_estado ON trans_inv_stock.IdProductoEstado = producto_estado.IdEstado " &
            "LEFT JOIN producto_talla_color ON trans_inv_stock.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor " &
            "LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor " &
            "LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla "


            vSQL += "WHERE trans_inv_stock.IdInventario=@IdInventario AND trans_inv_stock.IdBodega = @IdBodega"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable

            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20241212 Obtener inventario congelado - inv ciclico
    Public Shared Function Get_Stock_Congelado_InvCiclico(ByVal pInvCiclico As clsBeTrans_inv_ciclico) As clsBeTrans_inv_ciclico

        Get_Stock_Congelado_InvCiclico = Nothing

        Try

            Const sp As String = "SELECT top(1) s.* 
                                  FROM trans_inv_stock s INNER JOIN 
                                       VW_ProductoSI pb ON s.IdProductoBodega = pb.IdProductoBodega
							      WHERE (s.Lic_plate = @Licencia OR pb.codigo = @Licencia Or 
								        pb.codigo_barra=@Licencia Or pb.codigo_barra_pcb =@Licencia Or 
										pb.codigo_barra_presentacion =@Licencia or pb.codigo_barra=@Licencia)
							            AND s.IdBodega = @IdBodega 
							            AND s.IdInventario = @IdInventario
							            AND s.IdUbicacion = @IdUbicacion "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Licencia", pInvCiclico.lic_plate)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pInvCiclico.IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pInvCiclico.Idinventarioenc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pInvCiclico.IdUbicacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            Dim dr As DataRow = lDataTable.Rows(0)
                            Dim Obj As New clsBeTrans_inv_stock
                            Cargar(Obj, dr)

                            Dim InvCiclico As New clsBeTrans_inv_ciclico
                            InvCiclico.IdStock = Obj.IdStock
                            InvCiclico.lic_plate = Obj.Lic_plate
                            InvCiclico.IdProductoBodega = Obj.IdProductoBodega
                            InvCiclico.IdProductoEstado = Obj.IdProductoEstado
                            InvCiclico.IdProductoEst_nuevo = Obj.IdProductoEstado
                            InvCiclico.Fecha_vence = Obj.Fecha_vence
                            InvCiclico.Fecha_vence_stock = Obj.Fecha_vence
                            InvCiclico.Lote = Obj.Lote
                            InvCiclico.Lote_stock = Obj.Lote
                            InvCiclico.IdPresentacion = Obj.IdPresentacion
                            InvCiclico.IdUnidadMedida = Obj.IdUnidadMedida
                            InvCiclico.Cant_stock = Obj.Cantidad
                            InvCiclico.IdUbicacion = Obj.IdUbicacion
                            InvCiclico.EstadoNuevo = ""

                            Get_Stock_Congelado_InvCiclico = InvCiclico
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

    Public Shared Function GetSingle(ByRef pBeTrans_inv_stock As clsBeTrans_inv_stock,
                                     ByVal pConnection As SqlConnection,
                                     ByVal pTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Trans_inv_stock 
                                  Where(idinventario = @idinventario) 
                                        AND (IdStock = @IdStock)"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIO", pBeTrans_inv_stock.Idinventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", pBeTrans_inv_stock.IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_inv_stock, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
