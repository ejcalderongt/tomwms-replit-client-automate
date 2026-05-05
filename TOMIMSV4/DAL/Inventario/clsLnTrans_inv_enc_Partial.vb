Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Partial Public Class clsLnTrans_inv_enc

    '#CKFK 20180625 10:35 PM Corregì la condición del operador = 1, porque debe er el operador del parámetro
    Public Shared Function Get_All_Pendientes_By_IdBodega_And_IdOperador(pIdBodega As Integer,
                                                                         pIdOperador As Integer,
                                                                         pIdTarea As Integer) As List(Of clsBeTrans_inv_enc)

        Get_All_Pendientes_By_IdBodega_And_IdOperador = Nothing

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT DISTINCT
                        trans_inv_enc.idinventarioenc, trans_inv_enc.idpropietario, trans_inv_enc.idbodega, trans_inv_enc.idtipoinventario, 
                        trans_inv_enc.tipo_conteo_producto, trans_inv_enc.doble_verificacion,
                        trans_inv_enc.fecha, trans_inv_enc.estado, trans_inv_enc.inicial, trans_inv_enc.activo, trans_inv_enc.regularizado, 
                        trans_inv_enc.hora_ini, trans_inv_enc.hora_fin, trans_inv_enc.user_agr,
                        trans_inv_enc.fec_agr, trans_inv_enc.user_mod, trans_inv_enc.fec_mod, 
                        trans_inv_enc.EsSistema, trans_inv_enc.cambia_ubicacion, trans_inv_enc.Fecha_Ultimo_Inventario, 
                        trans_inv_enc.mostrar_cantidad_teorica_hh, trans_inv_enc.IdProductoFamilia, trans_inv_enc.IdBodegaVirtual,trans_inv_enc.capturar_no_existente,
                        trans_inv_enc.multi_propietario,
						trans_inv_enc.IdCentroCosto, 0 as Tipo_Asignacion,
                        trans_inv_enc.Capturar_No_Asignados From trans_inv_enc Where (activo = 1 And inicial = 1) "

                    If (pIdTarea > 0) Then vSQL &= "AND (idinventarioenc=@IdInventario) AND estado <> 'Finalizado' "

                    vSQL &= " And (IdBodega = @IdBodega)
                        UNION
                        Select  DISTINCT
                        trans_inv_enc.idinventarioenc, trans_inv_enc.idpropietario, trans_inv_enc.idbodega, trans_inv_enc.idtipoinventario, 
                        trans_inv_enc.tipo_conteo_producto, trans_inv_enc.doble_verificacion,
                        trans_inv_enc.fecha, trans_inv_enc.estado, trans_inv_enc.inicial, trans_inv_enc.activo, trans_inv_enc.regularizado, 
                        trans_inv_enc.hora_ini, trans_inv_enc.hora_fin, trans_inv_enc.user_agr,
                        trans_inv_enc.fec_agr, trans_inv_enc.user_mod, trans_inv_enc.fec_mod, 
                        trans_inv_enc.EsSistema, trans_inv_enc.cambia_ubicacion, trans_inv_enc.Fecha_Ultimo_Inventario, 
                        trans_inv_enc.mostrar_cantidad_teorica_hh, trans_inv_enc.IdProductoFamilia, trans_inv_enc.IdBodegaVirtual,trans_inv_enc.capturar_no_existente,
                        trans_inv_enc.multi_propietario,
						trans_inv_enc.IdCentroCosto, 0 as Tipo_Asignacion,
                        trans_inv_enc.Capturar_No_Asignados
                        From trans_inv_enc
                        Where (trans_inv_enc.activo = 1) And (trans_inv_enc.idbodega =@IdBodega)
                        And (trans_inv_enc.inicial = 0) AND trans_inv_enc.estado <> 'Finalizado' "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                        If (pIdTarea > 0) Then lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdTarea)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_inv_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeTrans_inv_enc
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next
                        End If

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

    Public Shared Function Get_All_By_Rango_Fechas(ByVal pFechaInicio As Date,
                                                   ByVal pFechaFin As Date,
                                                   ByVal IdBodega As Integer,
                                                   ByVal pActivos As Boolean) As List(Of clsBeTrans_inv_enc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_enc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#GT28042026: si tipo inventario es RFID omitir de la lista clasica de inventarios.
                Dim vSQL As String = String.Format("SELECT trans_inv_enc.idinventarioenc, trans_inv_enc.idpropietario, trans_inv_enc.idbodega, trans_inv_enc.idtipoinventario, 
                         trans_inv_enc.tipo_conteo_producto, trans_inv_enc.doble_verificacion, trans_inv_enc.fecha, trans_inv_enc.estado, trans_inv_enc.inicial, 
                         trans_inv_enc.activo, trans_inv_enc.regularizado, trans_inv_enc.hora_ini, trans_inv_enc.hora_fin, trans_inv_enc.user_agr, 
                         trans_inv_enc.fec_agr, trans_inv_enc.user_mod, trans_inv_enc.fec_mod, trans_inv_enc.fecha_ultimo_inventario, propietarios.nombre_comercial AS Propietario, 
                         bodega.nombre AS Bodega, TipoConteo.Descripcion AS Conteo, TipoInventario.Descripcion AS Inventario,trans_inv_enc.EsSistema, trans_inv_enc.cambia_ubicacion,
                         trans_inv_enc.mostrar_cantidad_teorica_hh,trans_inv_enc.IdProductoFamilia,trans_inv_enc.IdBodegaVirtual,trans_inv_enc.capturar_no_existente,trans_inv_enc.multi_propietario,
                         trans_inv_enc.IdCentroCosto, trans_inv_enc.Tipo_Asignacion, trans_inv_enc.Capturar_No_Asignados
                         FROM TipoInventario RIGHT OUTER JOIN
                         trans_inv_enc ON TipoInventario.IdTipoInv = trans_inv_enc.idtipoinventario LEFT OUTER JOIN
                         TipoConteo ON trans_inv_enc.tipo_conteo_producto = TipoConteo.IdTipoConteo LEFT OUTER JOIN
                         bodega ON trans_inv_enc.idbodega = bodega.idbodega  LEFT OUTER JOIN
                         propietarios ON trans_inv_enc.idpropietario = propietarios.IdPropietario
                         WHERE trans_inv_enc.idbodega = {0}  AND ISNULL(TipoInventario.Es_RFID, 0) <> 1
                         AND cast(trans_inv_enc.Fecha as Date) >={1} and cast(trans_inv_enc.Fecha as Date) <={2} ",
                                                   IdBodega, FormatoFechas.fFecha(pFechaInicio), FormatoFechas.fFecha(pFechaFin))

                If pActivos Then
                    vSQL += " AND trans_inv_enc.activo = 1 "
                Else
                    vSQL += " AND trans_inv_enc.activo = 0 "
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeTrans_inv_enc As New clsBeTrans_inv_enc

                    For Each dr As DataRow In lDataTable.Rows

                        vBeTrans_inv_enc = New clsBeTrans_inv_enc

                        Cargar(vBeTrans_inv_enc, dr)

                        If dr("IdBodega") IsNot DBNull.Value AndAlso dr("IdBodega") IsNot Nothing Then
                            vBeTrans_inv_enc.Bodega = New clsBeBodega
                            vBeTrans_inv_enc.Bodega.IdBodega = CType(dr("IdBodega"), Integer)
                            vBeTrans_inv_enc.Bodega.Nombre = CType(dr("Bodega"), String)
                        End If

                        If dr("IdPropietario") IsNot DBNull.Value AndAlso dr("IdPropietario") IsNot Nothing Then

                            If dr("IdPropietario") = 0 Then
                                'vBeTrans_inv_enc.Propietario = clsLnPropietarios.GetSingle(dr("IdPropietario"))
                                vBeTrans_inv_enc.Propietario.Nombre_comercial = "INVENTARIO MULTI-PROPIETARIO"

                            Else
                                vBeTrans_inv_enc.Propietario = New clsBePropietarios
                                vBeTrans_inv_enc.Propietario = clsLnPropietarios.GetSingle(dr("IdPropietario"))
                                vBeTrans_inv_enc.Propietario.Nombre_comercial = vBeTrans_inv_enc.Propietario.Nombre_comercial
                            End If

                        End If

                        If dr("idtipoinventario") IsNot DBNull.Value AndAlso dr("idtipoinventario") IsNot Nothing Then
                            vBeTrans_inv_enc.TipoInv = New clsBeTipoInventario
                            vBeTrans_inv_enc.TipoInv.IdTipoInv = CType(dr("idtipoinventario"), Integer)
                            vBeTrans_inv_enc.TipoInv.Descripcion = If(IsDBNull(dr("Inventario")), String.Empty, CType(dr("Inventario"), String))
                        End If

                        If dr("tipo_conteo_producto") > 0 AndAlso dr("tipo_conteo_producto") IsNot Nothing Then
                            vBeTrans_inv_enc.TipoConteo = New clsBeTipoConteo
                            vBeTrans_inv_enc.TipoConteo.IdTipoConteo = CType(dr("tipo_conteo_producto"), Integer)
                            vBeTrans_inv_enc.TipoConteo.Descripcion = If(IsDBNull(dr("Conteo")), String.Empty, CType(dr("Conteo"), String))
                        End If

                        lReturnList.Add(vBeTrans_inv_enc)
                    Next

                    Return lReturnList

                End Using
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(idinventarioenc),0) FROM trans_inv_enc"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForComboTipoInv() As DataTable

        Try

            Const sp As String = "Select IdTipoInv,Descripcion from TipoInventario "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForComboTipoConteo() As DataTable

        Try

            Const sp As String = "Select IdTipoConteo,Descripcion from TipoConteo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer) As clsBeTrans_inv_enc

        Try

            Dim vSQL As String = "SELECT * FROM Trans_inv_enc WHERE idinventarioenc=@idinventarioenc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInventarioEnc)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)

                            Dim Obj As New clsBeTrans_inv_enc()

                            Cargar(Obj, lRow)

                            Return Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Presentacion_TemporalTable(ByVal pIdInv As Integer, ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As clsBeTrans_inv_enc

        Try

            Dim vSQL As String = "SELECT * from ##tempComparacionStock where IdProducto=@IdProducto AND Inventario=@inventario AND IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@inventario", pIdInv)
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim Obj As New clsBeTrans_inv_enc()

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("Inventario") IsNot DBNull.Value AndAlso lRow("Inventario") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("Inventario"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("Detalle") IsNot DBNull.Value AndAlso lRow("Detalle") IsNot Nothing Then
                                Obj.Detalle = CType(lRow("Detalle"), Double)
                            End If

                            If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                                Obj.Resumen = CType(lRow("Resumen"), Double)
                            End If

                            If lRow("Stock") IsNot DBNull.Value AndAlso lRow("Stock") IsNot Nothing Then
                                Obj.Stock = CType(lRow("Stock"), Double)
                            End If

                            If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                Obj.Peso = CType(lRow("Peso"), Double)
                            End If

                            Return Obj
                        Next

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Res(ByVal pIdInv As Integer) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                        SUM(cantidad) As Detalle,0 AS Resumen,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso
                        FROM trans_inv_detalle  
                        INNER JOIN
                        producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                        WHERE idinventarioenc =@idinventario  
                        GROUP BY idinventarioenc,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,
                        producto_presentacion.IdPresentacion,producto.IdProducto
                        UNION
                        SELECT idinventarioenct AS IdInventario,producto.codigo,producto.IdProducto,   
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion,0 AS Detalle,SUM(cantidad) AS Resumen,0 AS Stock,0 AS Peso 
                        FROM trans_inv_resumen INNER JOIN 
                        producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_resumen.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE idinventarioenct =@idinventario
                        GROUP BY idinventarioenct,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto 
                        UNION
                        SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,0 AS Detalle,0 AS Resumen,SUM(cant) AS Stock,0 AS Peso 
                        FROM trans_inv_stock_prod INNER JOIN 
                        producto ON trans_inv_stock_prod.idproducto = producto.IdProducto LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE idinventario =@idinventario
                        GROUP BY idinventario,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto
                        UNION
                        SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,0 AS Detalle,0 AS Resumen,0 AS Stock,SUM(trans_inv_stock_prod.peso) AS Peso 
                        FROM trans_inv_stock_prod INNER JOIN 
                        producto ON trans_inv_stock_prod.idproducto = producto.IdProducto LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE idinventario =@idinventario
                        GROUP BY idinventario,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto"

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim Obj As New clsBeTrans_inv_enc()

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("IdInventario") IsNot DBNull.Value AndAlso lRow("IdInventario") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("IdInventario"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Detalle") IsNot DBNull.Value AndAlso lRow("Detalle") IsNot Nothing Then
                                Obj.Detalle = CType(lRow("Detalle"), Double)
                            End If

                            If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                                Obj.Resumen = CType(lRow("Resumen"), Double)
                            End If

                            If lRow("Stock") IsNot DBNull.Value AndAlso lRow("Stock") IsNot Nothing Then
                                Obj.Stock = CType(lRow("Stock"), Double)
                            End If

                            If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                Obj.Peso = CType(lRow("Peso"), Double)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_Teorico_WMS(ByVal pIdInv As Integer) As DataTable

        Get_Inventario_Vrs_Stock_Det_Teorico_WMS = Nothing

        Try

            Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, t.Producto as Nombre, t.UMBas,
                                                SUM(t.Inventario) AS Stock_WMS , 
                                                SUM(t.Stock) AS Teorico_ERP, 
                                                ROUND(SUM(t.Inventario) - SUM(t.Stock),6) AS Dif_ERP,
                                                SUM(t.Conteo) AS Conteo, 
                                                ROUND(SUM(t.Conteo) - SUM(t.Stock),6) AS Dif_Conteo,
                                                t.lote AS Lote, t.fecha_vence AS Fecha_Vence,T.ubicacion
                                                FROM (
                                                SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                SUM(trans_inv_stock.cantidad) AS Inventario, 
                                                0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                                                producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas,
						                         dbo.Nombre_Completo_Ubicacion(trans_inv_stock.idubicacion,trans_inv_stock.idbodega)  as ubicacion
                                                FROM trans_inv_stock INNER JOIN
                                                producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida
                                                WHERE (trans_inv_stock.idinventario = @IdInventarioEnc )
                                                GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock.idubicacion,trans_inv_stock.idbodega
                                        UNION                        
                                                SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                                                AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,
						                         dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion
                                                FROM  trans_inv_stock_prod INNER JOIN
                                                producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida 
                                                WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc )
                                                GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega
                                        UNION                        
                                                SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                 0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                                                0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,'' as ubicacion
                                                FROM  trans_inv_ciclico INNER JOIN
                                                producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						                        producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida
                                                WHERE     (trans_inv_ciclico.idinventarioenc =  @IdInventarioEnc)
						                        GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                                                trans_inv_ciclico.lote, 
						                        trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre) AS T
                                          GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto, t.UMBas,T.ubicacion
                                          ORDER BY T.codigo  "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.CommandTimeout = 0
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Get_Inventario_Vrs_Stock_Det_Teorico_WMS = lDataTable

                    End If

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Teorico_Conteo_Costos(ByVal pIdInv As Integer) As DataTable

        Get_Teorico_Conteo_Costos = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, t.Producto as Nombre, t.UMBas,
                        SUM(t.Inventario) AS Stock_WMS , 
                        SUM(t.Stock) AS Teorico_ERP, 
                        ROUND(SUM(t.Inventario) - SUM(t.Stock),6) AS Dif_ERP,
                        SUM(t.Conteo) AS Conteo, 
                        ROUND(SUM(t.Conteo) - SUM(t.Stock),6) AS Dif_Conteo,
                        t.lote AS Lote, t.fecha_vence AS Fecha_Vence, 
                        ROUND(SUM(t.Stock) * SUM(T.costo),6) AS Costo_Nav,ROUND(SUM(t.Conteo) * SUM(T.costo),6) AS Costo_Conteo,
                        (ROUND(SUM(t.Stock) * SUM(T.costo),6) - ROUND(SUM(t.Conteo) * SUM(T.costo),6)) as Dif_Costo
                        FROM (
                        SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                        SUM(trans_inv_stock.cantidad) AS Inventario, 
                        0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                        producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas, producto.costo
                        FROM trans_inv_stock INNER JOIN
                        producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE (trans_inv_stock.idinventario = @IdInventarioEnc)
                        GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, producto_presentacion.IdPresentacion, 
                        producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo
                UNION                        
                        SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                        0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                        AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                        unidad_medida.Nombre AS UMBas, producto.costo
                        FROM  trans_inv_stock_prod INNER JOIN
                        producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_stock_prod.idPresentacion = producto_presentacion.IdPresentacion
                        WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc)
                        GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                        producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo
                UNION                        
                        SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                         0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                        0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                        unidad_medida.Nombre AS UMBas, producto.costo
                        FROM  trans_inv_ciclico INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_ciclico.idPresentacion = producto_presentacion.IdPresentacion
                        WHERE     (trans_inv_ciclico.idinventarioenc = @IdInventarioEnc)
						GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                        producto_presentacion.nombre,trans_inv_ciclico.lote, 
						trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo) AS T
                  GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto, t.UMBas
                  ORDER BY T.codigo "

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.CommandTimeout = 0
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Return lDataTable

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_WMS(ByVal pIdInv As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pCoincidencias As Boolean) As DataTable

        Get_Inventario_Vrs_Stock_Det_WMS = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            '#EJC20220505
            'dbo.Nombre_Completo_Ubicacion(" & vIdUbicacionRecepcion & "," & pIdBodega & ") as ubicacion

            If Not pCoincidencias Then
                vIdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pIdBodega)
            End If

            Dim vSQL As String = "SELECT t.codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                     SUM(t.Stock) AS Stock, 
                                     SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,t.ubicacion,
                                     t.UMBas,t.Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
                                     IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia
                                     FROM (
                                     SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                     SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, trans_inv_detalle.Lote, 
                                     CONVERT(date,trans_inv_detalle.Fecha_Vence) AS Fecha_Vence, "

            If pCoincidencias Then
                vSQL += " dbo.Nombre_Completo_Ubicacion(trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega) As ubicacion, "
            Else
                vSQL += " dbo.Nombre_Completo_Ubicacion(" & vIdUbicacionRecepcion & "," & pIdBodega & ") as ubicacion, "
            End If

            vSQL += "unidad_medida.Nombre UMBas, producto_presentacion.factor, trans_inv_detalle.Lic_Plate AS Licencia
                     FROM trans_inv_detalle
                        INNER JOIN
                        producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
	                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                    unidad_medida.IdUnidadMedida = trans_inv_detalle.idunidadmedida LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                        WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc = @IdInventarioEnc "

            '#GT28112022_1000: si es ubic por defecto de la bodega
            If Not pCoincidencias Then
                vSQL += " AND trans_inv_detalle.IdUbicacion = @vIdUbicacionRecepcion AND trans_inv_detalle.idbodega = @IdBodega "
            End If

            vSQL += "GROUP BY idinventarioenc,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,
                        producto_presentacion.IdPresentacion,producto.IdProducto, trans_inv_detalle.Lote, trans_inv_detalle.Fecha_Vence,
                        trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega, unidad_medida.Nombre, producto_presentacion.factor,
                        trans_inv_detalle.Lic_Plate
                        UNION ALL                     
                        SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
                        0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
	                    dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion,
	                    unidad_medida.Nombre UMBas, producto_presentacion.factor, Lic_Plate AS Licencia
                        FROM trans_inv_stock_prod INNER JOIN 
                        producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
	                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                    unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE idinventario = @IdInventarioEnc
                        AND TipoTeoricoImportacion =0 --#EJC20240724: WMS
                        GROUP BY idinventario,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
                        trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.idubicacion,
                        trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor,trans_inv_stock_prod.Lic_Plate) AS T                                     
                        GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion, t.UMBas, t.Presentacion,t.factor, t.Licencia
                        ORDER BY T.codigo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                        If Not pCoincidencias Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@vIdUbicacionRecepcion", vIdUbicacionRecepcion)
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Vrs_Stock_Det_WMS = lDataTable
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

    Public Shared Function Get_Inventario_Vrs_Stock_Det_Original(ByVal pIdInv As Integer,
                                                                 ByVal pIdBodega As Integer,
                                                                 ByVal pCoincidencias As Boolean) As DataTable

        Get_Inventario_Vrs_Stock_Det_Original = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            '#EJC20220505
            'dbo.Nombre_Completo_Ubicacion(" & vIdUbicacionRecepcion & "," & pIdBodega & ") as ubicacion

            If Not pCoincidencias Then
                vIdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pIdBodega)
            End If

            Dim vSQL As String = "SELECT t.codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                     SUM(t.Stock) AS Stock, 
                                     SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,t.ubicacion
                                     FROM (
                                     SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                     SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, trans_inv_detalle.Lote, 
                                     CONVERT(date,trans_inv_detalle.Fecha_Vence) AS Fecha_Vence,"

            If pCoincidencias Then
                vSQL += " dbo.Nombre_Completo_Ubicacion(trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega) As ubicacion "
            Else
                vSQL += " dbo.Nombre_Completo_Ubicacion(" & vIdUbicacionRecepcion & "," & pIdBodega & ") as ubicacion "
            End If

            vSQL += "FROM trans_inv_detalle
                                     INNER JOIN
                                     producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN 
                                     producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                     WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc =@IdInventarioEnc
                                     GROUP BY idinventarioenc,producto.codigo,  
                                     producto.nombre,producto_presentacion.nombre,
                                     producto_presentacion.IdPresentacion,producto.IdProducto, trans_inv_detalle.Lote, trans_inv_detalle.Fecha_Vence,
                                     trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega
                                     UNION                        
                                     SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
                                     0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
						             dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion
                                     FROM trans_inv_stock_prod INNER JOIN 
                                     producto ON trans_inv_stock_prod.idproducto = producto.IdProducto LEFT OUTER JOIN 
                                     producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                     WHERE idinventario =@IdInventarioEnc
                                     GROUP BY idinventario,producto.codigo,  
                                     producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
                                     trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega) AS T                                     
                                     GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion
                                     ORDER BY T.codigo "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Vrs_Stock_Det_Original = lDataTable
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

    Public Shared Function Tiene_Conteos(ByVal pIdInv As Integer) As Boolean

        Tiene_Conteos = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT COUNT(IdInvCiclico) AS Registros_Contados
                         FROM trans_inv_ciclico
                         WHERE idinventarioenc =@IdInventarioEnc
                         AND cantidad > 0"

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                        Tiene_Conteos = (IIf(IsDBNull(lDataTable.Rows(0).Item("Registros_Contados")), 0, lDataTable.Rows(0).Item("Registros_Contados")) > 0)
                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer, ByVal ConUbicacion As Boolean) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Dim vSQL As String = ""

            If Not ConUbicacion Then

                vSQL = "SELECT T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, SUM(T.DETALLE) AS DETALLE, SUM(T.RESUMEN) AS RESUMEN, 
	                    T.IDPRODUCTO, T.PRODUCTO,T.CODIGO as Codigo, tit.det_estado as EstadoConteo, tit.res_estado as EstadoResumen, T.IdPropietario,T.UMBas
	                    FROM (SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,
	                    SUM(trans_inv_detalle.cantidad) AS Detalle,0 AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_detalle.idoperador,trans_inv_enc.idbodega
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc AND 
	                    trans_inv_tramo.idtramo = trans_inv_detalle.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_detalle.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega
	                    WHERE (trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_detalle.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_detalle.idoperador,trans_inv_enc.idbodega
	                    UNION
	                    SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,0 As Detalle,
	                    SUM(trans_inv_resumen.cantidad) AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_resumen.idoperador,trans_inv_enc.idbodega
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_resumen ON trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct AND 
	                    trans_inv_tramo.idtramo = trans_inv_resumen.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica  INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc  AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega
	                    WHERE(trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_resumen.idoperador,trans_inv_enc.idbodega ) AS T
	                    LEFT JOIN trans_inv_tramo tit ON T.idtramo = tit.idtramo and T.idinventario=tit.idinventario
						inner join trans_inv_enc enc on enc.idinventarioenc = tit.idinventario  
						inner join bodega_tramo bt on bt.IdTramo = tit.idtramo and bt.IdBodega = enc.idbodega and tit.idbodega=enc.idbodega
						
						GROUP BY T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, T.IDPRODUCTO, T.PRODUCTO,
	                    tit.det_estado, tit.res_estado, T.CODIGO,T.IdPropietario,T.UMBas"

            Else

                vSQL = "SELECT T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, SUM(T.DETALLE) AS DETALLE, SUM(T.RESUMEN) AS RESUMEN, 
	                    T.IDPRODUCTO, T.PRODUCTO,T.CODIGO as Codigo, tit.det_estado as EstadoConteo, tit.res_estado as EstadoResumen,T.IdPropietario,T.UMBas,t.Ubicacion_Conteo
	                    FROM (SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,
	                    SUM(trans_inv_detalle.cantidad) AS Detalle,0 AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        dbo.Nombre_Completo_Ubicacion(trans_inv_detalle.IdUbicacion,trans_inv_enc.idbodega) as Ubicacion_Conteo
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc AND 
	                    trans_inv_tramo.idtramo = trans_inv_detalle.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_detalle.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega
	                    WHERE (trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_detalle.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        trans_inv_detalle.IdUbicacion
	                    UNION
	                    SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,0 As Detalle,
	                    SUM(trans_inv_resumen.cantidad) AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,
                        dbo.Nombre_Completo_Ubicacion(trans_inv_resumen.IdUbicacion,trans_inv_enc.idbodega) as Ubicacion_Conteo
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_resumen ON trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct AND 
	                    trans_inv_tramo.idtramo = trans_inv_resumen.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica  INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc  AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega
	                    WHERE(trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,
                        trans_inv_resumen.IdUbicacion) AS T
	                    LEFT JOIN trans_inv_tramo tit ON T.idtramo = tit.idtramo and T.idinventario=tit.idinventario
						inner join trans_inv_enc enc on enc.idinventarioenc = tit.idinventario  
						inner join bodega_tramo bt on bt.IdTramo = tit.idtramo and bt.IdBodega = enc.idbodega and tit.idbodega=enc.idbodega
						
						GROUP BY T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, T.IDPRODUCTO, T.PRODUCTO,
	                    tit.det_estado, tit.res_estado,T.CODIGO,T.IdPropietario,T.UMBas,t.Ubicacion_Conteo"

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.CommandTimeout = 0
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim Obj As New clsBeTrans_inv_enc()

                            If lRow("IDINVENTARIO") IsNot DBNull.Value AndAlso lRow("IDINVENTARIO") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("IDINVENTARIO"), Integer)
                            End If

                            If lRow("IDTRAMO") IsNot DBNull.Value AndAlso lRow("IDTRAMO") IsNot Nothing Then
                                Obj.IdTramo = CType(lRow("IDTRAMO"), Integer)
                            End If

                            If lRow("IDPRESENTACION") IsNot DBNull.Value AndAlso lRow("IDPRESENTACION") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IDPRESENTACION"), Integer)
                            End If

                            If lRow("TRAMO") IsNot DBNull.Value AndAlso lRow("TRAMO") IsNot Nothing Then
                                Obj.Tramo = CType(lRow("TRAMO"), String)
                            End If

                            If lRow("DETALLE") IsNot DBNull.Value AndAlso lRow("DETALLE") IsNot Nothing Then
                                Obj.Detalle = CType(lRow("DETALLE"), Double)
                            End If

                            If lRow("RESUMEN") IsNot DBNull.Value AndAlso lRow("RESUMEN") IsNot Nothing Then
                                Obj.Resumen = CType(lRow("RESUMEN"), Double)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("IDPRODUCTO") IsNot DBNull.Value AndAlso lRow("IDPRODUCTO") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IDPRODUCTO"), Integer)
                            End If

                            If lRow("PRODUCTO") IsNot DBNull.Value AndAlso lRow("PRODUCTO") IsNot Nothing Then
                                Obj.Producto = CType(lRow("PRODUCTO"), String)
                            End If

                            If lRow("PRESENTACION") IsNot DBNull.Value AndAlso lRow("PRESENTACION") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("PRESENTACION"), String)
                            End If

                            If lRow("EstadoConteo") IsNot DBNull.Value AndAlso lRow("EstadoConteo") IsNot Nothing Then
                                Obj.EstadoDetalle = CType(lRow("EstadoConteo"), String)
                            End If

                            If lRow("EstadoResumen") IsNot DBNull.Value AndAlso lRow("EstadoResumen") IsNot Nothing Then
                                Obj.EstadoResumen = CType(lRow("EstadoResumen"), String)
                            End If

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.Idpropietario = CType(lRow("IdPropietario"), Integer)
                            End If

                            If lRow("UMBas") IsNot DBNull.Value AndAlso lRow("UMBas") IsNot Nothing Then
                                Obj.UMBas = CType(lRow("UMBas"), String)
                            End If

                            If ConUbicacion Then
                                If lRow("Ubicacion_Conteo") IsNot DBNull.Value AndAlso lRow("Ubicacion_Conteo") IsNot Nothing Then
                                    Obj.UbicacionCompleta = CType(lRow("Ubicacion_Conteo"), String)
                                End If
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInv As Integer,
                                                      Optional ByVal UbicacionCompleta As Boolean = False) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Dim vSQL As String = "SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion, 
                         SUM(trans_inv_detalle.cantidad) AS Detalle, trans_inv_tramo.det_estado AS EstadoDetalle, trans_inv_detalle.nom_operador AS OperadorConteo, 
                         trans_inv_detalle.fecha_vence, trans_inv_detalle.lote, trans_inv_detalle.IdUbicacion, trans_inv_detalle.fecha_captura, producto.IdProducto, 
                         producto.nombre AS Producto, producto.codigo, producto.IdPropietario, trans_inv_detalle.idinventariodet,
                         dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion,bodega_ubicacion.IdBodega) AS Nombre_Completo,
                         trans_inv_detalle.lic_plate as Licencia
                        FROM trans_inv_tramo INNER JOIN
                            trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc AND 
                            trans_inv_tramo.idtramo = trans_inv_detalle.idtramo INNER JOIN
                            bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
                            producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
                            bodega_ubicacion ON trans_inv_detalle.IdUbicacion = bodega_ubicacion.IdUbicacion AND 
                            trans_inv_detalle.idtramo = bodega_ubicacion.IdTramo INNER JOIN
                            trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc AND 
                            trans_inv_detalle.idinventarioenc = trans_inv_enc.idinventarioenc AND bodega_ubicacion.IdBodega = trans_inv_enc.idbodega AND 
                            bodega_tramo.IdBodega = trans_inv_enc.idbodega LEFT OUTER JOIN
                            producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE (trans_inv_tramo.idinventario = @idinventario)
                        GROUP BY trans_inv_tramo.idtramo, trans_inv_tramo.idinventario, bodega_tramo.descripcion, producto_presentacion.nombre, 
                                                 trans_inv_tramo.det_estado, trans_inv_detalle.nom_operador, trans_inv_detalle.fecha_vence, trans_inv_detalle.lote, 
                                                 trans_inv_detalle.IdUbicacion, trans_inv_detalle.fecha_captura, producto.IdProducto, producto.nombre, producto.codigo, 
                                                 producto.IdPropietario, trans_inv_detalle.idinventariodet, bodega_tramo.es_rack, bodega_tramo.Indice_x, bodega_ubicacion.nivel, 
                                                 bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion, bodega_ubicacion.indice_x,bodega_ubicacion.IdBodega, trans_inv_detalle.lic_plate "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = ltransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Dim BeTransInvEnc As New clsBeTrans_inv_enc
                                Dim ObjU As clsBeBodega_ubicacion

                                If lRow("idinventario") IsNot DBNull.Value AndAlso lRow("idinventario") IsNot Nothing Then
                                    BeTransInvEnc.Idinventarioenc = CType(lRow("idinventario"), Integer)
                                End If

                                If lRow("idinventariodet") IsNot DBNull.Value AndAlso lRow("idinventariodet") IsNot Nothing Then
                                    BeTransInvEnc.IdInventarioDet = CType(lRow("idinventariodet"), Integer)
                                End If

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    BeTransInvEnc.Idpropietario = CType(lRow("IdPropietario"), Integer)
                                End If

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    BeTransInvEnc.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("idtramo") IsNot DBNull.Value AndAlso lRow("idtramo") IsNot Nothing Then
                                    BeTransInvEnc.IdTramo = CType(lRow("idtramo"), Integer)
                                End If

                                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                    BeTransInvEnc.Tramo = CType(lRow("Tramo"), String)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    ObjU = New clsBeBodega_ubicacion
                                    BeTransInvEnc.Ubicacion.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                    If UbicacionCompleta Then
                                        clsLnBodega_ubicacion.ObtenerWithTramo(BeTransInvEnc.Ubicacion, lConnection, ltransaction)
                                    End If
                                End If

                                If lRow("Nombre_Completo") IsNot DBNull.Value AndAlso lRow("Nombre_Completo") IsNot Nothing Then
                                    BeTransInvEnc.Ubicacion.Descripcion = CType(lRow("Nombre_Completo"), String)
                                End If

                                If lRow("Detalle") IsNot DBNull.Value AndAlso lRow("Detalle") IsNot Nothing Then
                                    BeTransInvEnc.Detalle = CType(lRow("Detalle"), Double)
                                End If

                                If lRow("EstadoDetalle") IsNot DBNull.Value AndAlso lRow("EstadoDetalle") IsNot Nothing Then
                                    BeTransInvEnc.EstadoDetalle = CType(lRow("EstadoDetalle"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    BeTransInvEnc.Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("OperadorConteo") IsNot DBNull.Value AndAlso lRow("OperadorConteo") IsNot Nothing Then
                                    BeTransInvEnc.OperadorConteo = CType(lRow("OperadorConteo"), String)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    BeTransInvEnc.FechaVence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    BeTransInvEnc.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    BeTransInvEnc.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    BeTransInvEnc.Codigo = CType(lRow("codigo"), String)
                                End If

                                If lRow("fecha_captura") IsNot DBNull.Value AndAlso lRow("fecha_captura") IsNot Nothing Then
                                    BeTransInvEnc.Fecha = CType(lRow("fecha_captura"), Date)
                                End If

                                If lRow("Licencia") IsNot DBNull.Value AndAlso lRow("Licencia") IsNot Nothing Then
                                    BeTransInvEnc.Licencia = CType(lRow("Licencia"), String)
                                End If

                                lReturnList.Add(BeTransInvEnc)

                            Next

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_ConteoEnc_Veri(ByVal pIdInv As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion, 
                                    SUM(trans_inv_resumen.cantidad) AS Resumen, trans_inv_tramo.res_estado AS EstadoResumen, 
                                    trans_inv_resumen.nom_operador AS OperadorVerifica, producto.IdProducto, producto.IdPropietario, producto.codigo, 
                                    producto.nombre AS Producto, trans_inv_resumen.fecha_captura,trans_inv_resumen.idinventariores,producto_presentacion.IdPresentacion, "


                vSQL += " dbo.Nombre_Completo_Ubicacion(trans_inv_resumen.idubicacion, " & pIdBodega & " ) as Ubicacion "

                vSQL += " FROM trans_inv_tramo 
                                    INNER JOIN
                                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo 
                                    INNER JOIN
                                    trans_inv_resumen ON trans_inv_tramo.idtramo = trans_inv_resumen.idtramo 
                                    AND 
                                    trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct 
                                    INNER JOIN
                                    producto ON trans_inv_resumen.idproducto = producto.IdProducto INNER JOIN
                                     dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                                     dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega 
                                    LEFT OUTER JOIN
                                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion
                                    WHERE (trans_inv_tramo.idinventario = @idinventario)
                                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
                                    trans_inv_tramo.res_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.IdPropietario, producto.codigo, 
                                    producto.nombre, trans_inv_resumen.fecha_captura,trans_inv_resumen.idinventariores,producto_presentacion.IdPresentacion,trans_inv_resumen.idubicacion "

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim BeTransInvEnc As New clsBeTrans_inv_enc()

                            If lRow("idinventario") IsNot DBNull.Value AndAlso lRow("idinventario") IsNot Nothing Then
                                BeTransInvEnc.Idinventarioenc = CType(lRow("idinventario"), Integer)
                            End If

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                BeTransInvEnc.Idpropietario = CType(lRow("IdPropietario"), Integer)
                            End If

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                BeTransInvEnc.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                BeTransInvEnc.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("idinventariores") IsNot DBNull.Value AndAlso lRow("idinventariores") IsNot Nothing Then
                                BeTransInvEnc.IdInventarioRes = CType(lRow("idinventariores"), Integer)
                            End If

                            If lRow("idtramo") IsNot DBNull.Value AndAlso lRow("idtramo") IsNot Nothing Then
                                BeTransInvEnc.IdTramo = CType(lRow("idtramo"), Integer)
                            End If

                            If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                BeTransInvEnc.Tramo = CType(lRow("Tramo"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                BeTransInvEnc.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("EstadoResumen") IsNot DBNull.Value AndAlso lRow("EstadoResumen") IsNot Nothing Then
                                BeTransInvEnc.EstadoResumen = CType(lRow("EstadoResumen"), String)
                            End If

                            If lRow("OperadorVerifica") IsNot DBNull.Value AndAlso lRow("OperadorVerifica") IsNot Nothing Then
                                BeTransInvEnc.OperadorVerifica = CType(lRow("OperadorVerifica"), String)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                BeTransInvEnc.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                BeTransInvEnc.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("fecha_captura") IsNot DBNull.Value AndAlso lRow("fecha_captura") IsNot Nothing Then
                                BeTransInvEnc.Fecha = CType(lRow("fecha_captura"), Date)
                            End If

                            If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                                BeTransInvEnc.Resumen = CType(lRow("Resumen"), Double)
                            End If

                            If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                BeTransInvEnc.UbicacionCompleta = CType(lRow("Ubicacion"), String)
                            End If

                            lReturnList.Add(BeTransInvEnc)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar(ByVal pBeInventarioEnc As clsBeTrans_inv_enc,
                                   ByVal pObjTareaHH As clsBeTarea_hh) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction

            'Inventario Encabezado
            Guarda_Trans_Inv_Enc(pBeInventarioEnc, lConnection, lTransaction)

            'Crea Tarea para HH
            pObjTareaHH.IdTransaccion = pBeInventarioEnc.Idinventarioenc
            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH, lConnection, lTransaction)

            'Crea copia de stock cuando es inventario ciclico. 
            'clsLnTrans_inv_stock.Guarda_Copia_Stock(pBeInventarioEnc, lConnection, lTransaction)
            clsLnTrans_inv_stock.Generar_Invenatario_Congelado(pBeInventarioEnc.Idinventarioenc, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Guardar(ByVal pBeInventarioEnc As clsBeTrans_inv_enc,
                                   ByVal pObjTareaHH As clsBeTarea_hh,
                                   ByRef prg As Windows.Forms.ProgressBar,
                                   ByRef lblprg As Label) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            lblprg.Visible = True

            lblprg.Text = "Guardando cabecera de inventario"
            lblprg.Refresh()

            'Inventario Encabezado
            Guarda_Trans_Inv_Enc(pBeInventarioEnc, lConnection, lTransaction)

            lblprg.Text = "Guardando tarea de inventario"
            lblprg.Refresh()

            'Crea Tarea para HH
            pObjTareaHH.IdTransaccion = pBeInventarioEnc.Idinventarioenc

            clsLnTarea_hh.Guardar_Tarea_Recepcion_HH(pObjTareaHH, lConnection, lTransaction)

            lblprg.Text = "Generando imagen de de inventario"
            lblprg.Refresh()

            'Crea copia de stock cuando es inventario ciclico. 
            clsLnTrans_inv_stock.Generar_Invenatario_Congelado(pBeInventarioEnc.Idinventarioenc, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Visible = False
            lblprg.Visible = False
        End Try

    End Function

    Public Shared Function Guardar(ByVal pBeInventarioEnc As clsBeTrans_inv_enc) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction

            'Inventario Encabezado
            Guarda_Trans_Inv_Enc(pBeInventarioEnc, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Guarda_Trans_Inv_Enc(ByRef pObjE As clsBeTrans_inv_enc,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If pObjE.IsNew Then
                pObjE.Idinventarioenc = MaxID(lConnection, lTransaction)
                Insertar(pObjE, lConnection, lTransaction)
            Else
                Actualizar(pObjE, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function GetComparacionStock(ByVal pIdInv As Integer) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM ##tempComparacionStock WHERE Inventario=@Inventario"

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@Inventario", pIdInv)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim Obj As New clsBeTrans_inv_enc()

                            If lRow("Inventario") IsNot DBNull.Value AndAlso lRow("Inventario") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("Inventario"), Integer)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Detalle") IsNot DBNull.Value AndAlso lRow("Detalle") IsNot Nothing Then
                                Obj.Detalle = CType(lRow("Detalle"), Double)
                            End If

                            If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                                Obj.Resumen = CType(lRow("Resumen"), Double)
                            End If

                            If lRow("Stock") IsNot DBNull.Value AndAlso lRow("Stock") IsNot Nothing Then
                                Obj.Stock = CType(lRow("Stock"), Double)
                            End If

                            If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                Obj.Peso = CType(lRow("Peso"), Double)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Crea_Tabla_Temporal() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL = "IF OBJECT_ID('##tempComparacionStock', 'U') IS NOT NULL
                                DELETE  ##tempComparacionStock
                    ELSE
                             CREATE TABLE ##tempComparacionStock
                             (  
                             IdProducto   INT,
                                Inventario   INT,
                             IdPresentacion INT,  
                                Codigo   NVARCHAR(100) ,
                             Producto NVARCHAR(100),
                             Presentacion NVARCHAR(100),
                             Detalle Float ,
                             Resumen Float , 
                             Stock Float,
                                Peso Float,
                             Primary Key(IdProducto,Inventario,IdPresentacion)
                             )"

            Dim sp As String = vSQL
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction)


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        End Try

    End Function

    'Public Shared Function creaTablaTemporalComparacionInventario() As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        vSQL = "IF OBJECT_ID('##tempComparacionInventario', 'U') IS NOT NULL
    '                            DELETE  ##tempComparacionInventario
    '                ELSE
    '                         CREATE TABLE ##tempComparacionInventario
    '                         (  
    '                         IdInventario   INT,
    '                            IdProducto   INT,
    '                            IdPresentacion INT,
    '                            IdTramo INT,
    '                            Tramo NVARCHAR(50),
    '                            Codigo   NVARCHAR(100) ,
    '                         Producto NVARCHAR(100),
    '                         Presentacion NVARCHAR(100),
    '                         Detalle Float ,
    '                         Resumen Float , 
    '                            EstadoDetalle NVARCHAR(50),
    '                            EstadoResumen NVARCHAR(50),
    '                            OperadorDetalle NVARCHAR(50),
    '                            OperadorResumen NVARCHAR(50),
    '                            UMBas NVARCHAR(50)
    '                         )"

    '        Dim sp As String = vSQL
    '        Dim cmd As New SqlCommand(sp, lConnection)

    '        cmd = New SqlCommand(sp, lConnection, lTransaction)

    '        lConnection.Open()

    '        
    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        cmd.Dispose()

    '        If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '        Return rowsAffected

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function GetSingleByPresentacionStock(ByVal pIdInv As Integer,
                                                        ByVal pIdProducto As Integer,
                                                        ByVal pIdPresentacion As Integer) As clsBeTrans_inv_enc

        Try

            Dim vSQL As String = "SELECT idinventario AS IdInventario,
                    producto.IdProducto,
                    producto_presentacion.IdPresentacion,
                    producto.codigo,  
                    producto.nombre AS Producto,
                    ISNULL(producto_presentacion.nombre,'') AS Presentacion,
                    SUM(cant) As Stock
                    FROM trans_inv_stock_prod  INNER JOIN 
                    producto ON trans_inv_stock_prod.idproducto = producto.IdProducto LEFT OUTER JOIN 
                    producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion 
                    WHERE idinventario = @idinventario and producto.IdProducto=@IdProducto and producto_presentacion.IdPresentacion=@IdPresentacion
                    GROUP BY idinventario,producto.codigo,producto_presentacion.IdPresentacion,  
                    producto.nombre,producto_presentacion.nombre,producto.IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Dim Obj As New clsBeTrans_inv_enc()

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("IdInventario") IsNot DBNull.Value AndAlso lRow("IdInventario") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("IdInventario"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Stock") IsNot DBNull.Value AndAlso lRow("Stock") IsNot Nothing Then
                                Obj.Stock = CType(lRow("Stock"), Double)
                            End If

                            Return Obj
                        Next

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Elimina_Tabla_Temporal_ComparacionStock() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim vSQL As String = "DROP TABLE  ##tempComparacionStock"
            Dim sp As String = vSQL
            Dim cmd As New SqlCommand(sp, lConnection)

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            cmd = New SqlCommand(sp, lConnection, lTransaction)


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Inv_Inicial(ByVal pIdBodega As Integer) As Boolean

        Try

            Dim vSQL As String = "SELECT * FROM Trans_inv_enc WHERE idbodega=@idbodega AND Inicial=1 AND regularizado =0  AND estado <> 'Anulado' "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idbodega", pIdBodega)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    Try
                        Return lDataTable.Rows.Count > 0
                    Catch ex As Exception
                        Return False
                    End Try

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Fecha_Ultimo_Inventario() As Date

        Try

            Dim UltInv As Date = Now.Date

            Const sp As String = "SELECT ISNULL(Max(FECHA),GETDATE()) FROM Trans_inv_enc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        UltInv = lReturnValue
                    End If
                End Using
            End Using

            Return UltInv

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#AT20241209 Agregue el parametro pIdResolucion
    Public Shared Function Guarda_ProductoNuevo_AgregaInventario(ByVal gBeProducto As clsBeProducto,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdInventario As Integer,
                                                                 ByVal EsCiclico As Boolean,
                                                                 ByVal BeInvCiclico As clsBeTrans_inv_ciclico,
                                                                 ByVal BeInvInicial As clsBeTrans_inv_detalle,
                                                                 ByVal pIdResolucion As Integer) As Boolean

        Guarda_ProductoNuevo_AgregaInventario = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            '#Inserta Producto 
            gBeProducto.IdProducto = clsLnProducto.MaxID(lConnection, lTransaction) + 1
            clsLnProducto.Insertar(gBeProducto, lConnection, lTransaction)

            '#Inserta Producto Bodega
            Dim BeProductoBodega As New clsBeProducto_bodega
            BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1
            BeProductoBodega.IdProducto = gBeProducto.IdProducto
            BeProductoBodega.IdBodega = IdBodega
            BeProductoBodega.Activo = True
            BeProductoBodega.User_agr = gBeProducto.User_agr
            BeProductoBodega.Fec_agr = gBeProducto.Fec_agr
            BeProductoBodega.User_mod = gBeProducto.User_mod
            BeProductoBodega.Fec_mod = gBeProducto.Fec_mod

            clsLnProducto_bodega.Insertar(BeProductoBodega, lConnection, lTransaction)

            'Inserta Producto en inventario no encontrado
            Dim BeInvNe As New clsBeTrans_inv_ne
            BeInvNe.Idinventarione = clsLnTrans_inv_ne.MaxID(lConnection, lTransaction) + 1
            BeInvNe.Idinventarioenc = IdInventario
            BeInvNe.Idproducto = gBeProducto.IdProducto
            BeInvNe.Codigo = gBeProducto.Codigo
            BeInvNe.Nombre = gBeProducto.Nombre
            BeInvNe.Fec_agr = gBeProducto.Fec_agr
            BeInvNe.Usr_agr = gBeProducto.User_agr
            BeInvNe.Cantidad = 0

            clsLnTrans_inv_ne.Insertar(BeInvNe, lConnection, lTransaction)

            '#EJC20211201: Agregué IdBodega
            BeInvCiclico.IdBodega = IdBodega

            'Inserta Producto en detalle de inventario. 
            If EsCiclico Then

                BeInvCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(lConnection, lTransaction)
                BeInvCiclico.IdProductoBodega = BeProductoBodega.IdProductoBodega

                clsLnTrans_inv_ciclico.Insertar(BeInvCiclico, lConnection, lTransaction)

                '#AT20241209 Actualizar el correlativo actual en las resoluciones
                If pIdResolucion <> 0 Then
                    Dim BeResolLp As New clsBeResolucion_lp_operador()
                    BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucion, lConnection, lTransaction)

                    If Not BeResolLp Is Nothing Then
                        BeResolLp.Correlativo_Actual += 1
                        clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                  lConnection,
                                                                                  lTransaction)
                    End If
                End If

            Else

                BeInvInicial.Idinventariodet = clsLnTrans_inv_detalle.MaxID(lConnection, lTransaction)
                BeInvInicial.Idproducto = gBeProducto.IdProducto
                clsLnTrans_inv_detalle.InsertarSinID(BeInvInicial, lConnection, lTransaction)

            End If

            lTransaction.Commit()

            Guarda_ProductoNuevo_AgregaInventario = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Regularizar_Inventario(ByVal pIdInventarioEnc As Integer,
                                             ByVal pIdEmpresa As Integer)

        Dim stock As New List(Of clsBeTrans_inv_detalle)
        Dim BeInventarioEnc As New clsBeTrans_inv_enc
        Dim lPresentaciones As New List(Of clsBeProducto_Presentacion)
        Dim items As New List(Of clsBeStock)
        Dim movs As New List(Of clsBeTrans_movimientos)
        Dim item As New clsBeStock
        Dim prod As New clsBeProducto
        Dim mov As New clsBeTrans_movimientos
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim IdxPres As Integer = 0

        Try

            BeInventarioEnc.Idinventarioenc = pIdInventarioEnc
            GetSingle(BeInventarioEnc)

            If Not BeInventarioEnc Is Nothing Then

                stock = clsLnTrans_inv_detalle.Get_All_By_IdInventarioEnc(pIdInventarioEnc)

                If stock IsNot Nothing And stock.Count > 0 Then

                    For Each st As clsBeTrans_inv_detalle In stock

                        item = New clsBeStock

                        With item

                            If BeInventarioEnc.multi_propietario Then
                                .IdPropietarioBodega = st.IdPropietarioBodega
                            Else
                                .IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeInventarioEnc.IdBodega, BeInventarioEnc.Idpropietario)
                            End If

                            .IdStock = 0
                            .IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(st.Idproducto, BeInventarioEnc.IdBodega)
                            .IdProductoEstado = st.Idproductoestado
                            .ProductoEstado.IdEstado = st.Idproductoestado
                            .IdPresentacion = st.IdPresentacion
                            .Presentacion.IdPresentacion = st.IdPresentacion
                            .IdUnidadMedida = st.Idunidadmedida
                            .IdUbicacion = st.IdUbicacion
                            .IdUbicacion_anterior = st.IdUbicacion
                            .IdRecepcionEnc = 0
                            .IdRecepcionDet = 0
                            .IdPedidoEnc = 0
                            .IdPickingEnc = 0
                            .IdDespachoEnc = 0
                            .Lote = st.Lote
                            .Lic_plate = st.License_plate
                            .Serial = ""
                            .Cantidad = st.Cantidad
                            .Fecha_Ingreso = st.Fecha_captura
                            .Fecha_vence = st.Fecha_vence
                            .Uds_lic_plate = 0
                            .No_bulto = 0
                            .Fecha_Manufactura = Date.Now
                            .Añada = 0
                            .User_agr = BeInventarioEnc.User_agr
                            .Fec_agr = Date.Now
                            .User_mod = BeInventarioEnc.User_mod
                            .Fec_mod = Date.Now
                            .Activo = True
                            .Peso = st.Peso
                            .Temperatura = 0
                            .Atributo_Variante_1 = st.Codigo_variante

                        End With

                        If st.IdPresentacion <> 0 Then

                            IdxPres = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = st.IdPresentacion)

                            If IdxPres = -1 Then
                                BePresentacion = New clsBeProducto_Presentacion
                                BePresentacion.IdPresentacion = st.IdPresentacion
                                clsLnProducto_presentacion.GetSingle(BePresentacion)
                                lPresentaciones.Add(BePresentacion)
                            Else
                                BePresentacion = lPresentaciones(IdxPres)
                            End If

                            item.Cantidad = item.Cantidad * BePresentacion.Factor

                        End If

                        items.Add(item)

                        mov = New clsBeTrans_movimientos

                        mov.IdMovimiento = 0
                        mov.IdEmpresa = pIdEmpresa
                        mov.IdBodegaOrigen = BeInventarioEnc.IdBodega
                        mov.IdTransaccion = BeInventarioEnc.Idinventarioenc
                        mov.IdPropietarioBodega = item.IdPropietarioBodega
                        mov.IdProductoBodega = item.IdProductoBodega
                        mov.IdUbicacionOrigen = item.IdUbicacion
                        mov.IdUbicacionDestino = item.IdUbicacion
                        mov.IdPresentacion = item.IdPresentacion
                        mov.IdEstadoOrigen = item.IdProductoEstado
                        mov.IdEstadoDestino = item.IdProductoEstado
                        mov.IdUnidadMedida = item.IdUnidadMedida
                        mov.IdTipoTarea = 6
                        mov.IdBodegaDestino = BeInventarioEnc.IdBodega
                        mov.IdRecepcion = 0
                        mov.IdRecepcionDet = 0
                        mov.Serie = ""
                        mov.Lote = item.Lote
                        mov.Fecha_vence = item.Fecha_vence
                        mov.Fecha = Now
                        mov.Barra_pallet = ""
                        mov.Hora_ini = Now
                        mov.Hora_fin = Now
                        mov.Fecha_agr = Now
                        mov.Usuario_agr = BeInventarioEnc.User_agr
                        mov.Cantidad = item.Cantidad
                        mov.Cantidad_hist = 0
                        mov.Peso = item.Peso
                        mov.Peso_hist = 0

                        movs.Add(mov)

                        Application.DoEvents()

                    Next

                    clsLnStock.Importar_Inventario(BeInventarioEnc, items, movs)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), "Regularizar inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Shared Function Get_Inventario_Vrs_Stock_Det_ERP(ByVal pIdInv As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pCoincidencias As Boolean) As DataTable

        Get_Inventario_Vrs_Stock_Det_ERP = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            Dim vSQL As String = "SELECT t.ubicacion Codigo_Area, t.Codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                     SUM(t.Stock) AS Stock, 
                                     SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,
                                     t.UMBas,t.Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
                                     IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia
                                     FROM (
                                     SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                     SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, trans_inv_detalle.Lote, 
                                     CONVERT(date,trans_inv_detalle.Fecha_Vence) AS Fecha_Vence, 
                                     dbo.Get_Codigo_Area_By_IdUbicacion(trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega) As ubicacion, 
                                     unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                 FROM trans_inv_detalle INNER JOIN
                                    producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
	                                unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                unidad_medida.IdUnidadMedida = trans_inv_detalle.idunidadmedida LEFT OUTER JOIN 
                                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                    WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc = @IdInventarioEnc 
                                GROUP BY idinventarioenc,producto.codigo,  
                                    producto.nombre,producto_presentacion.nombre,
                                    producto_presentacion.IdPresentacion,producto.IdProducto, trans_inv_detalle.Lote, trans_inv_detalle.Fecha_Vence,
                                    trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega, unidad_medida.Nombre, producto_presentacion.factor
                                UNION ALL                     
                                SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
	                                    producto.nombre AS Producto,
	                                    ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
	                                    0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
	                                    trans_inv_stock_prod.codigo_area  as ubicacion,
	                                    unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                FROM trans_inv_stock_prod INNER JOIN 
	                                    producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
	                                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                    unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
	                                    producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                WHERE idinventario = @IdInventarioEnc
                                        AND TipoTeoricoImportacion =1 --#EJC20240724: ERP
                                GROUP BY idinventario,producto.codigo,  
		                                    producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
		                                    trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.codigo_area,
		                                    trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T                                     
                                GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion, t.UMBas, t.Presentacion,t.factor, t.Licencia
                                ORDER BY T.ubicacion, T.codigo "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                        'If Not pCoincidencias Then
                        '    lDataAdapter.SelectCommand.Parameters.AddWithValue("@vIdUbicacionRecepcion", vIdUbicacionRecepcion)
                        '    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        'End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Vrs_Stock_Det_ERP = lDataTable
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

    Public Shared Function Get_Inventario_Vrs_Stock_Det_Teorico_ERP(ByVal pIdInv As Integer) As DataTable

        Get_Inventario_Vrs_Stock_Det_Teorico_ERP = Nothing

        Try

            Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, t.Producto as Nombre, t.UMBas,
                                                SUM(t.Inventario) AS Stock_WMS , 
                                                SUM(t.Stock) AS Teorico_ERP, 
                                                ROUND(SUM(t.Inventario) - SUM(t.Stock),6) AS Dif_ERP,
                                                SUM(t.Conteo) AS Conteo, 
                                                ROUND(SUM(t.Conteo) - SUM(t.Stock),6) AS Dif_Conteo,
                                                t.lote AS Lote, t.fecha_vence AS Fecha_Vence,T.ubicacion
                                                FROM (
                                                SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                SUM(trans_inv_stock.cantidad) AS Inventario, 
                                                0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                                                producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas,
						                         dbo.Nombre_Completo_Ubicacion(trans_inv_stock.idubicacion,trans_inv_stock.idbodega)  as ubicacion
                                                FROM trans_inv_stock INNER JOIN
                                                producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida
                                                WHERE (trans_inv_stock.idinventario = @IdInventarioEnc)
                                                GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock.idubicacion,trans_inv_stock.idbodega
                                        UNION                        
                                                SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                                                AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,
						                         dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion
                                                FROM  trans_inv_stock_prod INNER JOIN
                                                producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida 
                                                WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc AND TipoTeoricoImportacion = 1)
                                                GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega
                                        UNION                        
                                                SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                 0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                                                0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,'' as ubicacion
                                                FROM  trans_inv_ciclico INNER JOIN
                                                producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						                        producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida
                                                WHERE     (trans_inv_ciclico.idinventarioenc =  @IdInventarioEnc)
						                        GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                                                trans_inv_ciclico.lote, 
						                        trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre) AS T
                                          GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto, t.UMBas,T.ubicacion
                                          ORDER BY T.codigo  "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.CommandTimeout = 0
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Inventario_Vrs_Stock_Det_Teorico_ERP = lDataTable

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

    Public Shared Function Get_All_By_ConteoEnc_Veri(ByVal pIdInv As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Dim vSQL As String = "SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion, 
                                    SUM(trans_inv_resumen.cantidad) AS Resumen, trans_inv_tramo.res_estado AS EstadoResumen, 
                                    trans_inv_resumen.nom_operador AS OperadorVerifica, producto.IdProducto, producto.IdPropietario, producto.codigo, 
                                    producto.nombre AS Producto, trans_inv_resumen.fecha_captura,trans_inv_resumen.idinventariores,producto_presentacion.IdPresentacion, 
                                    talla.Codigo AS Codigo_Talla, color.Codigo + ' - ' + color.Nombre AS Codigo_Color, "


            vSQL += " dbo.Nombre_Completo_Ubicacion(trans_inv_resumen.idubicacion, " & pIdBodega & " ) as Ubicacion,trans_inv_resumen.idubicacion,trans_inv_resumen.lic_plate Licencia  "

            vSQL += " FROM trans_inv_tramo 
                                    INNER JOIN
                                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo 
                                    INNER JOIN
                                    trans_inv_resumen ON trans_inv_tramo.idtramo = trans_inv_resumen.idtramo 
                                    AND 
                                    trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct 
                                    INNER JOIN
                                    producto ON trans_inv_resumen.idproducto = producto.IdProducto INNER JOIN
                                     dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                                     dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega 
                                    LEFT OUTER JOIN
                                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion
                                    LEFT JOIN producto_talla_color ON trans_inv_resumen.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor
                                    LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla
                                    LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor
                                    WHERE (trans_inv_tramo.idinventario = @idinventario)
                                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
                                    trans_inv_tramo.res_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.IdPropietario, producto.codigo, 
                                    producto.nombre, trans_inv_resumen.fecha_captura,trans_inv_resumen.idinventariores,producto_presentacion.IdPresentacion,
                                    trans_inv_resumen.idubicacion,trans_inv_resumen.lic_plate, talla.Codigo, color.Codigo, color.Nombre "

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim BeTransInvEnc As New clsBeTrans_inv_enc()

                        If lRow("idinventario") IsNot DBNull.Value AndAlso lRow("idinventario") IsNot Nothing Then
                            BeTransInvEnc.Idinventarioenc = CType(lRow("idinventario"), Integer)
                        End If

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            BeTransInvEnc.Idpropietario = CType(lRow("IdPropietario"), Integer)
                        End If

                        If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                            BeTransInvEnc.IdProducto = CType(lRow("IdProducto"), Integer)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransInvEnc.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                        End If

                        If lRow("idinventariores") IsNot DBNull.Value AndAlso lRow("idinventariores") IsNot Nothing Then
                            BeTransInvEnc.IdInventarioRes = CType(lRow("idinventariores"), Integer)
                        End If

                        If lRow("idtramo") IsNot DBNull.Value AndAlso lRow("idtramo") IsNot Nothing Then
                            BeTransInvEnc.IdTramo = CType(lRow("idtramo"), Integer)
                        End If

                        If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                            BeTransInvEnc.Tramo = CType(lRow("Tramo"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            BeTransInvEnc.Presentacion = CType(lRow("Presentacion"), String)
                        End If

                        If lRow("EstadoResumen") IsNot DBNull.Value AndAlso lRow("EstadoResumen") IsNot Nothing Then
                            BeTransInvEnc.EstadoResumen = CType(lRow("EstadoResumen"), String)
                        End If

                        If lRow("OperadorVerifica") IsNot DBNull.Value AndAlso lRow("OperadorVerifica") IsNot Nothing Then
                            BeTransInvEnc.OperadorVerifica = CType(lRow("OperadorVerifica"), String)
                        End If

                        If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                            BeTransInvEnc.Codigo = CType(lRow("codigo"), String)
                        End If

                        If lRow("Licencia") IsNot DBNull.Value AndAlso lRow("Licencia") IsNot Nothing Then
                            BeTransInvEnc.Licencia = CType(lRow("Licencia"), String)
                        End If

                        If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                            BeTransInvEnc.Producto = CType(lRow("Producto"), String)
                        End If

                        If lRow("fecha_captura") IsNot DBNull.Value AndAlso lRow("fecha_captura") IsNot Nothing Then
                            BeTransInvEnc.Fecha = CType(lRow("fecha_captura"), Date)
                        End If

                        If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                            BeTransInvEnc.Resumen = CType(lRow("Resumen"), Double)
                        End If

                        If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                            BeTransInvEnc.UbicacionCompleta = CType(lRow("Ubicacion"), String)
                        End If

                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                            Dim ObjU As New clsBeBodega_ubicacion
                            BeTransInvEnc.Ubicacion.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                            'clsLnBodega_ubicacion.ObtenerWithTramo(BeTransInvEnc.Ubicacion, lConnection, lTransaction)
                        End If

                        If lRow("Codigo_Talla") IsNot DBNull.Value AndAlso lRow("Codigo_Talla") IsNot Nothing Then
                            BeTransInvEnc.Codigo_Talla = CType(lRow("Codigo_Talla"), String)
                        End If

                        If lRow("Codigo_Color") IsNot DBNull.Value AndAlso lRow("Codigo_Color") IsNot Nothing Then
                            BeTransInvEnc.Codigo_Color = CType(lRow("Codigo_Color"), String)
                        End If

                        lReturnList.Add(BeTransInvEnc)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInv As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Dim vsql As String = "
                            SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion, 
                                   SUM(trans_inv_detalle.cantidad) AS Detalle, trans_inv_tramo.det_estado AS EstadoDetalle, trans_inv_detalle.nom_operador AS OperadorConteo, 
                                   trans_inv_detalle.fecha_vence, trans_inv_detalle.lote, trans_inv_detalle.IdUbicacion, trans_inv_detalle.fecha_captura, 
                                   producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario, trans_inv_detalle.idinventariodet,
                                   dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Nombre_Completo, trans_inv_detalle.lic_plate AS Licencia,
                                   trans_inv_detalle.cantidad, talla.Codigo AS Codigo_Talla, color.Codigo + ' - ' + color.Nombre AS Codigo_Color
                            FROM trans_inv_tramo 
                            INNER JOIN trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc 
                                                        AND trans_inv_tramo.idtramo = trans_inv_detalle.idtramo 
                            INNER JOIN bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo 
                                                   AND trans_inv_tramo.IdBodega = bodega_tramo.IdBodega 
                            INNER JOIN producto ON trans_inv_detalle.idproducto = producto.IdProducto 
                            INNER JOIN trans_inv_enc ON trans_inv_tramo.idinventario = trans_inv_enc.idinventarioenc 
                                                    AND trans_inv_detalle.idinventarioenc = trans_inv_enc.idinventarioenc 
                            LEFT OUTER JOIN bodega_ubicacion ON trans_inv_detalle.IdUbicacion = bodega_ubicacion.IdUbicacion 
                                                           AND trans_inv_detalle.idtramo = bodega_ubicacion.IdTramo 
                                                           AND trans_inv_detalle.idbodega = bodega_ubicacion.IdBodega 
                            LEFT OUTER JOIN producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion
                            LEFT JOIN producto_talla_color ON trans_inv_detalle.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor
                            LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla
                            LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor
                            WHERE trans_inv_tramo.idinventario = @idinventario
                            GROUP BY trans_inv_tramo.idtramo, trans_inv_tramo.idinventario, bodega_tramo.descripcion, producto_presentacion.nombre, trans_inv_tramo.det_estado, 
                                     trans_inv_detalle.nom_operador, trans_inv_detalle.fecha_vence, trans_inv_detalle.lote, trans_inv_detalle.IdUbicacion, trans_inv_detalle.fecha_captura, 
                                     producto.IdProducto, producto.nombre, producto.codigo, producto.IdPropietario, trans_inv_detalle.idinventariodet, bodega_tramo.es_rack, 
                                     bodega_tramo.Indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, bodega_ubicacion.IdUbicacion, bodega_ubicacion.indice_x, 
                                     bodega_ubicacion.IdBodega, trans_inv_detalle.lic_plate, trans_inv_detalle.cantidad, talla.Codigo, color.Codigo, color.Nombre"


            Using lDataAdapter As New SqlDataAdapter(vsql, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim BeTransInvEnc As New clsBeTrans_inv_enc
                        Dim ObjU As clsBeBodega_ubicacion

                        If lRow("idinventario") IsNot DBNull.Value AndAlso lRow("idinventario") IsNot Nothing Then
                            BeTransInvEnc.Idinventarioenc = CType(lRow("idinventario"), Integer)
                        End If

                        If lRow("idinventariodet") IsNot DBNull.Value AndAlso lRow("idinventariodet") IsNot Nothing Then
                            BeTransInvEnc.IdInventarioDet = CType(lRow("idinventariodet"), Integer)
                        End If

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            BeTransInvEnc.Idpropietario = CType(lRow("IdPropietario"), Integer)
                        End If

                        If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                            BeTransInvEnc.IdProducto = CType(lRow("IdProducto"), Integer)
                        End If

                        If lRow("idtramo") IsNot DBNull.Value AndAlso lRow("idtramo") IsNot Nothing Then
                            BeTransInvEnc.IdTramo = CType(lRow("idtramo"), Integer)
                        End If

                        If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                            BeTransInvEnc.Tramo = CType(lRow("Tramo"), String)
                        End If

                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                            ObjU = New clsBeBodega_ubicacion
                            BeTransInvEnc.Ubicacion.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                            'clsLnBodega_ubicacion.ObtenerWithTramo(BeTransInvEnc.Ubicacion, lConnection, lTransaction)
                        End If

                        If lRow("Nombre_Completo") IsNot DBNull.Value AndAlso lRow("Nombre_Completo") IsNot Nothing Then
                            BeTransInvEnc.Ubicacion.Descripcion = CType(lRow("Nombre_Completo"), String)
                        End If

                        If lRow("Detalle") IsNot DBNull.Value AndAlso lRow("Detalle") IsNot Nothing Then
                            BeTransInvEnc.Detalle = CType(lRow("Detalle"), Double)
                        End If

                        If lRow("EstadoDetalle") IsNot DBNull.Value AndAlso lRow("EstadoDetalle") IsNot Nothing Then
                            BeTransInvEnc.EstadoDetalle = CType(lRow("EstadoDetalle"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            BeTransInvEnc.Presentacion = CType(lRow("Presentacion"), String)
                        End If

                        If lRow("OperadorConteo") IsNot DBNull.Value AndAlso lRow("OperadorConteo") IsNot Nothing Then
                            BeTransInvEnc.OperadorConteo = CType(lRow("OperadorConteo"), String)
                        End If

                        If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                            BeTransInvEnc.FechaVence = CType(lRow("fecha_vence"), Date)
                        End If

                        If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                            BeTransInvEnc.Lote = CType(lRow("lote"), String)
                        End If

                        If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                            BeTransInvEnc.Producto = CType(lRow("Producto"), String)
                        End If

                        If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                            BeTransInvEnc.Codigo = CType(lRow("codigo"), String)
                        End If

                        If lRow("fecha_captura") IsNot DBNull.Value AndAlso lRow("fecha_captura") IsNot Nothing Then
                            BeTransInvEnc.Fecha = CType(lRow("fecha_captura"), Date)
                        End If

                        If lRow("Licencia") IsNot DBNull.Value AndAlso lRow("Licencia") IsNot Nothing Then
                            BeTransInvEnc.Licencia = CType(lRow("Licencia"), String)
                        End If

                        If lRow("Codigo_Talla") IsNot DBNull.Value AndAlso lRow("Codigo_Talla") IsNot Nothing Then
                            BeTransInvEnc.Codigo_Talla = CType(lRow("Codigo_Talla"), String)
                        End If

                        If lRow("Codigo_Color") IsNot DBNull.Value AndAlso lRow("Codigo_Color") IsNot Nothing Then
                            BeTransInvEnc.Codigo_Color = CType(lRow("Codigo_Color"), String)
                        End If

                        lReturnList.Add(BeTransInvEnc)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_WMS(ByVal pIdInv As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pCoincidencias As Boolean,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As DataTable

        Get_Inventario_Vrs_Stock_Det_WMS = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0

        Try

            If Not pCoincidencias Then
                vIdUbicacionRecepcion = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pIdBodega, lConnection, lTransaction)
            End If

            Dim vSQL As String = "SELECT t.codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                     SUM(t.Stock) AS Stock, 
                                     SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,t.ubicacion,
                                     Case
                                                WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                                THEN t.UMBas
                                                ELSE t.Presentacion
                                                END AS Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
                                     IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia, t.Codigo_Talla, t.Codigo_Color
                                     FROM (
                                     SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                     SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, trans_inv_detalle.Lote, 
                                     CONVERT(date,trans_inv_detalle.Fecha_Vence) AS Fecha_Vence, "

            If pCoincidencias Then
                vSQL += " dbo.Nombre_Completo_Ubicacion(trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega) As ubicacion, "
            Else
                vSQL += " dbo.Nombre_Completo_Ubicacion(" & vIdUbicacionRecepcion & "," & pIdBodega & ") as ubicacion, "
            End If

            vSQL += "unidad_medida.Nombre UMBas, producto_presentacion.factor, trans_inv_detalle.Lic_Plate AS Licencia, '' Codigo_Talla, '' Codigo_Color
                     FROM trans_inv_detalle
                        INNER JOIN
                        producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
	                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                    unidad_medida.IdUnidadMedida = trans_inv_detalle.idunidadmedida LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                        WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc = @IdInventarioEnc "

            '#GT28112022_1000: si es ubic por defecto de la bodega
            If Not pCoincidencias Then
                vSQL += " AND trans_inv_detalle.IdUbicacion = @vIdUbicacionRecepcion AND trans_inv_detalle.idbodega = @IdBodega "
            End If

            vSQL += "GROUP BY idinventarioenc,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,
                        producto_presentacion.IdPresentacion,producto.IdProducto, trans_inv_detalle.Lote, trans_inv_detalle.Fecha_Vence,
                        trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega, unidad_medida.Nombre, producto_presentacion.factor,
                        trans_inv_detalle.Lic_Plate
                        UNION ALL                     
                        SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
                        producto.nombre AS Producto,
                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
                        0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
	                    dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion,
	                    unidad_medida.Nombre UMBas, producto_presentacion.factor, Lic_Plate AS Licencia, Codigo_Talla, Codigo_Color
                        FROM trans_inv_stock_prod INNER JOIN 
                        producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
	                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                    unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
                        producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE idinventario = @IdInventarioEnc
                        AND TipoTeoricoImportacion =0 --#EJC20240724: WMS
                        GROUP BY idinventario,producto.codigo,  
                        producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
                        trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.idubicacion,
                        trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor,trans_inv_stock_prod.Lic_Plate, 
                        trans_inv_stock_prod.codigo_talla, trans_inv_stock_prod.codigo_color) AS T                                     
                        GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion, Case
                                                WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                                THEN t.UMBas
                                                ELSE t.Presentacion
                                                END,t.factor, t.Licencia, t.Codigo_Talla, t.Codigo_Color
                        ORDER BY T.codigo "

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                If Not pCoincidencias Then
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@vIdUbicacionRecepcion", vIdUbicacionRecepcion)
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                End If

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_Inventario_Vrs_Stock_Det_WMS = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_Teorico_WMS(ByVal pIdInv As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As DataTable

        Get_Inventario_Vrs_Stock_Det_Teorico_WMS = Nothing

        Try

            Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, t.Producto as Nombre, 
                                                Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END AS Presentacion,
                                                SUM(IIF(t.Factor <> 0, t.Inventario / t.Factor, 0)) AS Stock_WMS_Pres,
                                                SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)) AS Teorico_ERP_Pres,
                                                ROUND(SUM(IIF(t.Factor <> 0, t.Inventario / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_ERP_Pres,
                                                SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) AS Conteo_Pres,
                                                ROUND(SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_Conteo_Pres,
                                                t.lote AS Lote, t.fecha_vence AS Fecha_Vence,T.ubicacion, t.Color, t.Talla
                                                FROM (
                                                SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                SUM(trans_inv_stock.cantidad) AS Inventario, 
                                                0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                                                producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas,
						                        dbo.Nombre_Completo_Ubicacion(trans_inv_stock.idubicacion,trans_inv_stock.idbodega)  as ubicacion, '' Color, '' Talla,
                                                producto_presentacion.nombre AS NombrePresentacion,
	                                            ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM trans_inv_stock INNER JOIN
                                                producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida
                                                LEFT JOIN producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE (trans_inv_stock.idinventario = @IdInventarioEnc )
                                                GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock.idubicacion,trans_inv_stock.idbodega, producto_presentacion.nombre, producto_presentacion.factor
                                        UNION                        
                                                SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                                                AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,
						                        dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion, '' Color, '' Talla,
                                                producto_presentacion.nombre AS NombrePresentacion,
		                                        ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM  trans_inv_stock_prod INNER JOIN
                                                producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida 
                                                LEFT JOIN producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc )
                                                GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega, producto_presentacion.nombre, producto_presentacion.factor
                                        UNION                        
                                                SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                 0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                                                0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,'' as ubicacion, color.nombre Color, talla.codigo Talla, 
                                                producto_presentacion.nombre AS NombrePresentacion,
	                                            ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM  trans_inv_ciclico INNER JOIN
                                                producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						                        producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida
                                                LEFT JOIN producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_ciclico.IdProductoTallaColor
                                                LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor
                                                LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla
                                                LEFT JOIN producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE     (trans_inv_ciclico.idinventarioenc =  @IdInventarioEnc)
						                        GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                                                trans_inv_ciclico.lote, 
						                        trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, color.nombre, talla.codigo, producto_presentacion.factor, producto_presentacion.nombre) AS T
                                          GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto,T.ubicacion, t.Talla, t.Color, Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END, t.Factor
                                          ORDER BY T.codigo  "


            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandTimeout = 0
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_Inventario_Vrs_Stock_Det_Teorico_WMS = lDataTable

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_ERP(ByVal pIdInv As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIncluyeUbic As Boolean,
                                                            ByVal pIncluyeLoteVence As Boolean,
                                                            lConnection As SqlConnection,
                                                            lTransaction As SqlTransaction) As DataTable

        Get_Inventario_Vrs_Stock_Det_ERP = Nothing

        Dim vIdUbicacionRecepcion As Integer = 0
        Dim vSQL As String = ""

        Try

            If pIncluyeUbic And pIncluyeLoteVence Then

                vSQL = "SELECT t.ubicacion Codigo_Area, t.Codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                     SUM(t.Stock) AS Stock, 
                                     SUM(t.Inventario) - SUM(t.Stock) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,
                                     Case
                                        WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                        THEN t.UMBas
                                        ELSE t.Presentacion
                                     END AS Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
                                     IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia
                                     FROM (
                                     SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                     producto.nombre AS Producto,
                                     ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                     SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, trans_inv_detalle.Lote, 
                                     CONVERT(date,trans_inv_detalle.Fecha_Vence) AS Fecha_Vence, 
                                     dbo.Get_Codigo_Area_By_IdUbicacion(trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega) As ubicacion, 
                                     unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                 FROM trans_inv_detalle INNER JOIN
                                    producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
	                                unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                unidad_medida.IdUnidadMedida = trans_inv_detalle.idunidadmedida LEFT OUTER JOIN 
                                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                    WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc = @IdInventarioEnc 
                                GROUP BY idinventarioenc,producto.codigo,  
                                    producto.nombre,producto_presentacion.nombre,
                                    producto_presentacion.IdPresentacion,producto.IdProducto, trans_inv_detalle.Lote, trans_inv_detalle.Fecha_Vence,
                                    trans_inv_detalle.idubicacion, trans_inv_detalle.idbodega, unidad_medida.Nombre, producto_presentacion.factor
                                UNION ALL                     
                                SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
	                                    producto.nombre AS Producto,
	                                    ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
	                                    0 AS Detalle,SUM(cant) AS Stock,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
	                                    trans_inv_stock_prod.codigo_area  as ubicacion,
	                                    unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                FROM trans_inv_stock_prod INNER JOIN 
	                                    producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
	                                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                    unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
	                                    producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                WHERE idinventario = @IdInventarioEnc
                                        AND TipoTeoricoImportacion =1 --#EJC20240724: ERP
                                GROUP BY idinventario,producto.codigo,  
		                                    producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
		                                    trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.codigo_area,
		                                    trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T                                     
                                GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion, Case
                                                WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                                THEN  t.UMBas
                                                ELSE t.Presentacion
                                                END,t.factor, t.Licencia
                                ORDER BY T.ubicacion, T.codigo "

            ElseIf Not pIncluyeUbic AndAlso Not pIncluyeLoteVence Then

                vSQL = "SELECT t.Codigo, t.Producto as Nombre, SUM(t.Inventario) AS Inv , 
                                SUM(t.Stock) AS Stock, 
                                SUM(t.Inventario) - SUM(t.Stock) AS Dif, 
                                Case
                                                WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                                THEN t.UMBas
                                                ELSE t.Presentacion
                                                END AS Presentacion, t.factor,IIF(t.factor>0,t.factor*SUM(t.Inventario),sum(t.Inventario)) as Inv_UM,
                                IIF(t.factor>0,t.factor*SUM(t.Stock),sum(t.Stock)) as Stock_UM, t.Licencia
                        FROM (SELECT idinventarioenc AS IdInventario,producto.codigo,producto.IdProducto,  
                                        producto.nombre AS Producto,
                                        ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion, 
                                        SUM(cantidad) As Inventario,0 AS Stock,SUM(trans_inv_detalle.peso) AS Peso, 
                                        unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                FROM trans_inv_detalle INNER JOIN
                                    producto ON trans_inv_detalle.idproducto = producto.IdProducto INNER JOIN
	                                unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                unidad_medida.IdUnidadMedida = trans_inv_detalle.idunidadmedida LEFT OUTER JOIN 
                                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion 
                                    WHERE trans_inv_detalle.IdpropietarioBodega <> 0 and idinventarioenc = @IdInventarioEnc 
                                GROUP BY idinventarioenc,producto.codigo,  
                                    producto.nombre,producto_presentacion.nombre,
                                    producto_presentacion.IdPresentacion,producto.IdProducto, 
                                    trans_inv_detalle.idbodega, unidad_medida.Nombre, producto_presentacion.factor
                                UNION ALL                     
                                SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
	                                    producto.nombre AS Producto,
	                                    ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
	                                    0 AS Detalle,SUM(cant) AS Stock,0 AS Peso,
	                                    unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
                                FROM trans_inv_stock_prod INNER JOIN 
	                                    producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
	                                    unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
	                                    unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
	                                    producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                WHERE idinventario = @IdInventarioEnc
                                        AND TipoTeoricoImportacion =1 
                                GROUP BY idinventario,producto.codigo,  
		                                producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
		                                trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.codigo_area,
		                                trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T                                     
                        GROUP BY t.codigo, t.Producto,Case
                                                WHEN t.Presentacion IS NULL OR t.Presentacion = ''
                                                THEN t.UMBas
                                                ELSE t.Presentacion
                                                END,t.factor, t.Licencia
                        ORDER BY T.codigo "
            End If

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_Inventario_Vrs_Stock_Det_ERP = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Vrs_Stock_Det_Teorico_ERP(ByVal pIdInv As Integer,
                                                                    lConnection As SqlConnection,
                                                                    lTransaction As SqlTransaction) As DataTable

        Get_Inventario_Vrs_Stock_Det_Teorico_ERP = Nothing

        Try

            Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, 
                                                t.Producto as Nombre, 
                                                Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END AS Presentacion,
                                                SUM(IiF(t.Factor <> 0, t.Inventario / t.Factor, 0)) AS Stock_WMS_Pres,
                                                SUM(IiF(t.Factor <> 0, t.Stock / t.Factor, 0)) AS Teorico_ERP_Pres,
                                                ROUND(SUM(IiF(t.Factor <> 0, t.Inventario / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_ERP_Pres,
                                                SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) AS Conteo_Pres,
                                                ROUND(SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_Conteo_Pres,
                                                t.lote AS Lote, t.fecha_vence AS Fecha_Vence,T.ubicacion, t.Talla, t.Color
                                                FROM (
                                                SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                SUM(trans_inv_stock.cantidad) AS Inventario, 
                                                0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                                                producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas,
						                        dbo.Nombre_Completo_Ubicacion(trans_inv_stock.idubicacion,trans_inv_stock.idbodega)  as ubicacion, '' Talla, '' Color,
                                                producto_presentacion.nombre AS NombrePresentacion,
                                                ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM trans_inv_stock INNER JOIN
                                                producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida
                                                LEFT JOIN producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE (trans_inv_stock.idinventario = @IdInventarioEnc)
                                                GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock.idubicacion,trans_inv_stock.idbodega, producto_presentacion.nombre, producto_presentacion.factor
                                        UNION                        
                                                SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                                                AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,
						                        dbo.Nombre_Completo_Ubicacion(trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega)  as ubicacion,'' Talla, '' Color,
                                                producto_presentacion.nombre AS NombrePresentacion,
	                                            ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM  trans_inv_stock_prod INNER JOIN
                                                producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida
                                                LEFT JOIN producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc AND TipoTeoricoImportacion = 1)
                                                GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                                                producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre,
						                        trans_inv_stock_prod.idubicacion,trans_inv_stock_prod.idbodega, producto_presentacion.nombre, producto_presentacion.factor
                                        UNION                        
                                                SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                                                 0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                                                0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                                                unidad_medida.Nombre AS UMBas,'' as ubicacion, color.nombre Color, talla.codigo Talla,
                                                producto_presentacion.nombre AS NombrePresentacion,
                                                ISNULL(producto_presentacion.factor, 0) AS Factor
                                                FROM  trans_inv_ciclico INNER JOIN
                                                producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						                        producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                                                producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                                                unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida
                                                LEFT JOIN producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_ciclico.IdProductoTallaColor
                                                LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor
                                                LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla
                                                LEFT JOIN producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                                                WHERE     (trans_inv_ciclico.idinventarioenc =  @IdInventarioEnc)
						                        GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                                                trans_inv_ciclico.lote, 
						                        trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, color.nombre, talla.codigo, producto_presentacion.factor, producto_presentacion.nombre) AS T
                                          GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto,T.ubicacion, t.Color, t.Talla, t.Factor, Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END
                                          ORDER BY T.codigo  "


            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.CommandTimeout = 0
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_Inventario_Vrs_Stock_Det_Teorico_ERP = lDataTable

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Teorico_Conteo_Costos(ByVal pIdInv As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As DataTable

        Get_Teorico_Conteo_Costos = Nothing

        Try

            Dim vSQL As String = "SELECT t.TipoProducto as Tipo, t.codigo as Codigo, t.Producto as Nombre,  Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END AS Presentacion, t.Color, t.Talla, 
                        SUM(IIF(t.Factor <> 0, t.Inventario / t.Factor, 0)) AS Stock_WMS_Pres,
	                    SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)) AS Teorico_ERP_Pres,
	                    ROUND(SUM(IIF(t.Factor <> 0, t.Inventario / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_ERP_Pres,
	                    SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) AS Conteo_Pres,
	                    ROUND(SUM(IIF(t.Factor <> 0, t.Conteo / t.Factor, 0)) - SUM(IIF(t.Factor <> 0, t.Stock / t.Factor, 0)), 6) AS Dif_Conteo_Pres,
                        t.lote AS Lote, t.fecha_vence AS Fecha_Vence, 
                        ROUND(SUM(t.Stock) * SUM(T.costo),6) AS Costo_Nav,ROUND(SUM(t.Conteo) * SUM(T.costo),6) AS Costo_Conteo,
                        (ROUND(SUM(t.Stock) * SUM(T.costo),6) - ROUND(SUM(t.Conteo) * SUM(T.costo),6)) as Dif_Costo
                        FROM (
                        SELECT trans_inv_stock.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                        SUM(trans_inv_stock.cantidad) AS Inventario, 
                        0 AS Stock, 0 as Conteo, SUM(trans_inv_stock.peso) AS Peso, trans_inv_stock.lote, CONVERT(date, trans_inv_stock.fecha_vence) AS fecha_vence, 
                        producto_tipo.NombreTipoProducto AS TipoProducto, unidad_medida.Nombre AS UMBas, producto.costo, '' Color, '' Talla,
                        producto_presentacion.nombre AS NombrePresentacion,
                        ISNULL(producto_presentacion.factor, 0) AS Factor
                        FROM trans_inv_stock INNER JOIN
                        producto ON trans_inv_stock.IdProductoBodega = producto.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON trans_inv_stock.IdUnidadMedida = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE (trans_inv_stock.idinventario = @IdInventarioEnc)
                        GROUP BY trans_inv_stock.idinventario, producto.codigo, producto.nombre, producto_presentacion.IdPresentacion, 
                        producto.IdProducto, trans_inv_stock.lote, trans_inv_stock.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo,
                        producto_presentacion.nombre, producto_presentacion.factor
                UNION                        
                        SELECT     trans_inv_stock_prod.idinventario AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                        0 AS Detalle, SUM(trans_inv_stock_prod.cant) 
                        AS Stock, 0 as Conteo, 0 AS Peso, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                        unidad_medida.Nombre AS UMBas, producto.costo, '' Color, '' Talla,
                        producto_presentacion.nombre AS NombrePresentacion,
		                ISNULL(producto_presentacion.factor, 0) AS Factor
                        FROM  trans_inv_stock_prod INNER JOIN
                        producto ON trans_inv_stock_prod.idProducto = producto.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON trans_inv_stock_prod.idUnidadMedida = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_stock_prod.idPresentacion = producto_presentacion.IdPresentacion
                        WHERE     (trans_inv_stock_prod.idinventario = @IdInventarioEnc)
                        GROUP BY trans_inv_stock_prod.idinventario, producto.codigo, producto.nombre, 
                        producto.IdProducto, trans_inv_stock_prod.lote, trans_inv_stock_prod.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo,
                        producto_presentacion.nombre,
		                producto_presentacion.factor
                UNION                        
                        SELECT     trans_inv_ciclico.idinventarioenc AS IdInventario, producto.codigo, producto.IdProducto, producto.nombre AS Producto, 
                         0 AS Detalle, 0 as stock,Sum(trans_inv_ciclico.cantidad) as Conteo, 
                        0 AS Peso, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto AS TipoProducto, 
                        unidad_medida.Nombre AS UMBas, producto.costo, color.nombre Color, talla.codigo Talla,
                        producto_presentacion.nombre AS NombrePresentacion,
		                ISNULL(producto_presentacion.factor, 0) AS Factor
                        FROM  trans_inv_ciclico INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
						producto ON  producto.IdProducto =  producto_bodega.IdProducto INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        unidad_medida ON PRODUCTO.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_ciclico.idPresentacion = producto_presentacion.IdPresentacion
                        LEFT JOIN producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_ciclico.IdProductoTallaColor
                        LEFT JOIN color ON color.IdColor = producto_talla_color.IdColor
                        LEFT JOIN talla ON talla.IdTalla = producto_talla_color.IdTalla
                        WHERE     (trans_inv_ciclico.idinventarioenc = @IdInventarioEnc)
						GROUP BY trans_inv_ciclico.idinventarioenc, producto.codigo, producto.IdProducto, producto.nombre, 
                        producto_presentacion.nombre,trans_inv_ciclico.lote, 
						trans_inv_ciclico.fecha_vence, producto_tipo.NombreTipoProducto, unidad_medida.Nombre, producto.costo, color.nombre, talla.codigo, producto_presentacion.factor) AS T
                  GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.TipoProducto, t.Color, t.Talla,  Case
                                                WHEN t.NombrePresentacion IS NULL OR t.NombrePresentacion = ''
                                                THEN t.UMBas
                                                ELSE t.NombrePresentacion
                                                END, t.Factor
                  ORDER BY T.codigo "

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.CommandTimeout = 0
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Return lDataTable

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer, ByVal ConUbicacion As Boolean, ByVal lConnection As SqlConnection, ByVal ltransaction As SqlTransaction) As List(Of clsBeTrans_inv_enc)

        Dim lReturnList As New List(Of clsBeTrans_inv_enc)

        Try

            Dim vSQL As String = ""

            If Not ConUbicacion Then

                vSQL = "SELECT T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, SUM(T.DETALLE) AS DETALLE, SUM(T.RESUMEN) AS RESUMEN, 
	                    T.IDPRODUCTO, T.PRODUCTO,T.CODIGO as Codigo, tit.det_estado as EstadoConteo, tit.res_estado as EstadoResumen, T.IdPropietario,T.UMBas, T.Codigo_Talla, T.Codigo_Color
	                    FROM (SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,
	                    SUM(trans_inv_detalle.cantidad) AS Detalle,0 AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        talla.Codigo as Codigo_Talla, color.Codigo +' - '+ color.Nombre as Codigo_Color 
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc AND 
	                    trans_inv_tramo.idtramo = trans_inv_detalle.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica INNER JOIN
                        dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                        dbo.trans_inv_detalle.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega LEFT JOIN
                        producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_detalle.IdProductoTallaColor LEFT JOIN
						talla ON talla.IdTalla = producto_talla_color.IdTalla LEFT JOIN
						color ON color.IdColor = producto_talla_color.IdColor
	                    WHERE (trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_detalle.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        talla.Codigo, color.Nombre, color.Codigo
	                    UNION
	                    SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,0 As Detalle,
	                    SUM(trans_inv_resumen.cantidad) AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,
                        talla.Codigo as Codigo_Talla, color.Codigo +' - '+ color.Nombre as Codigo_Color 
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_resumen ON trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct AND 
	                    trans_inv_tramo.idtramo = trans_inv_resumen.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica  INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc  AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega LEFT JOIN
                        producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_resumen.IdProductoTallaColor LEFT JOIN
						talla ON talla.IdTalla = producto_talla_color.IdTalla LEFT JOIN
						color ON color.IdColor = producto_talla_color.IdColor
	                    WHERE(trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,talla.Codigo, color.Nombre, color.Codigo ) AS T
	                    LEFT JOIN trans_inv_tramo tit ON T.idtramo = tit.idtramo and T.idinventario=tit.idinventario
						inner join trans_inv_enc enc on enc.idinventarioenc = tit.idinventario  
						inner join bodega_tramo bt on bt.IdTramo = tit.idtramo and bt.IdBodega = enc.idbodega and tit.idbodega=enc.idbodega
						
						GROUP BY T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, T.IDPRODUCTO, T.PRODUCTO,
	                    tit.det_estado, tit.res_estado, T.CODIGO,T.IdPropietario,T.UMBas, T.Codigo_Talla, T.Codigo_Color"

            Else

                vSQL = "SELECT T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, SUM(T.DETALLE) AS DETALLE, SUM(T.RESUMEN) AS RESUMEN, 
	                    T.IDPRODUCTO, T.PRODUCTO,T.CODIGO as Codigo, tit.det_estado as EstadoConteo, tit.res_estado as EstadoResumen,T.IdPropietario,T.UMBas,t.Ubicacion_Conteo, t.IdUbicacion,T.Codigo_Talla, T.Codigo_Color 
	                    FROM (SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,
	                    SUM(trans_inv_detalle.cantidad) AS Detalle,0 AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        dbo.Nombre_Completo_Ubicacion(trans_inv_detalle.IdUbicacion,trans_inv_enc.idbodega) as Ubicacion_Conteo,trans_inv_detalle.IdUbicacion, talla.Codigo as Codigo_Talla, color.Codigo +' - '+ color.Nombre as Codigo_Color 
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_detalle ON trans_inv_tramo.idinventario = trans_inv_detalle.idinventarioenc AND 
	                    trans_inv_tramo.idtramo = trans_inv_detalle.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_detalle.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_detalle.IdPresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_detalle.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega LEFT JOIN
                        producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_detalle.IdProductoTallaColor LEFT JOIN
                        talla ON talla.IdTalla = producto_talla_color.IdTalla LEFT JOIN
                        color ON color.IdColor = producto_talla_color.IdColor
	                    WHERE (trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_detalle.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_detalle.idoperador,trans_inv_enc.idbodega,
                        trans_inv_detalle.IdUbicacion, talla.Codigo, color.Nombre, color.Codigo
	                    UNION
	                    SELECT trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion AS Tramo, producto_presentacion.nombre AS Presentacion,producto_presentacion.IdPresentacion,0 As Detalle,
	                    SUM(trans_inv_resumen.cantidad) AS Resumen, 
	                    producto.IdProducto, producto.nombre AS Producto, producto.codigo, producto.IdPropietario,unidad_medida.Nombre as UMBas, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,
                        dbo.Nombre_Completo_Ubicacion(trans_inv_resumen.IdUbicacion,trans_inv_enc.idbodega) as Ubicacion_Conteo,trans_inv_resumen.IdUbicacion, talla.Codigo as Codigo_Talla, color.Codigo +' - '+ color.Nombre as Codigo_Color 
	                    FROM trans_inv_tramo INNER JOIN
	                    trans_inv_resumen ON trans_inv_tramo.idinventario = trans_inv_resumen.idinventarioenct AND 
	                    trans_inv_tramo.idtramo = trans_inv_resumen.idtramo INNER JOIN
	                    bodega_tramo ON trans_inv_tramo.idtramo = bodega_tramo.IdTramo INNER JOIN
	                    producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN
	                    producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion INNER JOIN
	                    unidad_medida On  unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica  INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_tramo.idinventario = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc  AND 
                         dbo.trans_inv_resumen.idinventarioenct = dbo.trans_inv_enc.idinventarioenc AND dbo.bodega_tramo.IdBodega = dbo.trans_inv_enc.idbodega LEFT JOIN
                        producto_talla_color ON producto_talla_color.IdProductoTallaColor = trans_inv_resumen.IdProductoTallaColor LEFT JOIN
                        talla ON talla.IdTalla = producto_talla_color.IdTalla LEFT JOIN
                        color ON color.IdColor = producto_talla_color.IdColor
	                    WHERE(trans_inv_tramo.idinventario = @idinventario)
	                    GROUP BY trans_inv_tramo.idinventario, trans_inv_tramo.idtramo, bodega_tramo.descripcion, producto_presentacion.nombre, 
	                    trans_inv_tramo.det_estado, trans_inv_resumen.nom_operador, producto.IdProducto, producto.nombre, producto.codigo, 
	                    producto.IdPropietario,producto_presentacion.IdPresentacion,unidad_medida.Nombre, trans_inv_resumen.idoperador,trans_inv_enc.idbodega,
                        trans_inv_resumen.IdUbicacion, talla.Codigo, color.Nombre, color.Codigo) AS T
	                    LEFT JOIN trans_inv_tramo tit ON T.idtramo = tit.idtramo and T.idinventario=tit.idinventario
						inner join trans_inv_enc enc on enc.idinventarioenc = tit.idinventario  
						inner join bodega_tramo bt on bt.IdTramo = tit.idtramo and bt.IdBodega = enc.idbodega and tit.idbodega=enc.idbodega
						
						GROUP BY T.IDINVENTARIO, T.IDTRAMO, T.TRAMO, T.PRESENTACION, T.IDPRESENTACION, T.IDPRODUCTO, T.PRODUCTO,
	                    tit.det_estado, tit.res_estado,T.CODIGO,T.IdPropietario,T.UMBas,t.Ubicacion_Conteo, t.IdUbicacion, T.Codigo_Talla, T.Codigo_Color "

            End If

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = ltransaction
                lDataAdapter.SelectCommand.CommandTimeout = 0
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim Obj As New clsBeTrans_inv_enc()

                        If lRow("IDINVENTARIO") IsNot DBNull.Value AndAlso lRow("IDINVENTARIO") IsNot Nothing Then
                            Obj.Idinventarioenc = CType(lRow("IDINVENTARIO"), Integer)
                        End If

                        If lRow("IDTRAMO") IsNot DBNull.Value AndAlso lRow("IDTRAMO") IsNot Nothing Then
                            Obj.IdTramo = CType(lRow("IDTRAMO"), Integer)
                        End If

                        If lRow("IDPRESENTACION") IsNot DBNull.Value AndAlso lRow("IDPRESENTACION") IsNot Nothing Then
                            Obj.IdPresentacion = CType(lRow("IDPRESENTACION"), Integer)
                        End If

                        If lRow("TRAMO") IsNot DBNull.Value AndAlso lRow("TRAMO") IsNot Nothing Then
                            Obj.Tramo = CType(lRow("TRAMO"), String)
                        End If

                        If lRow("DETALLE") IsNot DBNull.Value AndAlso lRow("DETALLE") IsNot Nothing Then
                            Obj.Detalle = CType(lRow("DETALLE"), Double)
                        End If

                        If lRow("RESUMEN") IsNot DBNull.Value AndAlso lRow("RESUMEN") IsNot Nothing Then
                            Obj.Resumen = CType(lRow("RESUMEN"), Double)
                        End If

                        If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                            Obj.Codigo = CType(lRow("Codigo"), String)
                        End If

                        If lRow("IDPRODUCTO") IsNot DBNull.Value AndAlso lRow("IDPRODUCTO") IsNot Nothing Then
                            Obj.IdProducto = CType(lRow("IDPRODUCTO"), Integer)
                        End If

                        If lRow("PRODUCTO") IsNot DBNull.Value AndAlso lRow("PRODUCTO") IsNot Nothing Then
                            Obj.Producto = CType(lRow("PRODUCTO"), String)
                        End If

                        If lRow("PRESENTACION") IsNot DBNull.Value AndAlso lRow("PRESENTACION") IsNot Nothing Then
                            Obj.Presentacion = CType(lRow("PRESENTACION"), String)
                        End If

                        If lRow("EstadoConteo") IsNot DBNull.Value AndAlso lRow("EstadoConteo") IsNot Nothing Then
                            Obj.EstadoDetalle = CType(lRow("EstadoConteo"), String)
                        End If

                        If lRow("EstadoResumen") IsNot DBNull.Value AndAlso lRow("EstadoResumen") IsNot Nothing Then
                            Obj.EstadoResumen = CType(lRow("EstadoResumen"), String)
                        End If

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            Obj.Idpropietario = CType(lRow("IdPropietario"), Integer)
                        End If

                        If lRow("UMBas") IsNot DBNull.Value AndAlso lRow("UMBas") IsNot Nothing Then
                            Obj.UMBas = CType(lRow("UMBas"), String)
                        End If

                        If lRow("Codigo_Talla") IsNot DBNull.Value AndAlso lRow("Codigo_Talla") IsNot Nothing Then
                            Obj.Codigo_Talla = CType(lRow("Codigo_Talla"), String)
                        End If

                        If lRow("Codigo_Color") IsNot DBNull.Value AndAlso lRow("Codigo_Color") IsNot Nothing Then
                            Obj.Codigo_Color = CType(lRow("Codigo_Color"), String)
                        End If

                        If ConUbicacion Then
                            If lRow("Ubicacion_Conteo") IsNot DBNull.Value AndAlso lRow("Ubicacion_Conteo") IsNot Nothing Then
                                Obj.UbicacionCompleta = CType(lRow("Ubicacion_Conteo"), String)
                            End If

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                Dim ObjU As New clsBeBodega_ubicacion
                                Obj.Ubicacion.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                clsLnBodega_ubicacion.ObtenerWithTramo(Obj.Ubicacion, lConnection, ltransaction)
                            End If

                        End If

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_ForCombo_TipoInv(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "Select IdTipoInv,Descripcion from TipoInventario "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForComboTipoInv(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "Select IdTipoInv,Descripcion from TipoInventario "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForComboTipoConteo(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Const sp As String = "Select IdTipoConteo,Descripcion from TipoConteo "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT28042026: listar inventarios que sean de tipo_inventario RFID=1
    Public Shared Function Listar_Inventarios_RFID_By_Rango_Fechas(ByVal pFechaInicio As Date,
                                                                   ByVal pFechaFin As Date,
                                                                   ByVal IdBodega As Integer,
                                                                   ByVal pActivos As Boolean) As List(Of clsBeTrans_inv_enc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_enc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#GT28042026: si tipo inventario es RFID omitir de la lista clasica de inventarios.
                Dim vSQL As String = String.Format("SELECT trans_inv_enc.idinventarioenc, trans_inv_enc.idpropietario, trans_inv_enc.idbodega, trans_inv_enc.idtipoinventario, 
                         trans_inv_enc.tipo_conteo_producto, trans_inv_enc.doble_verificacion, trans_inv_enc.fecha, trans_inv_enc.estado, trans_inv_enc.inicial, 
                         trans_inv_enc.activo, trans_inv_enc.regularizado, trans_inv_enc.hora_ini, trans_inv_enc.hora_fin, trans_inv_enc.user_agr, 
                         trans_inv_enc.fec_agr, trans_inv_enc.user_mod, trans_inv_enc.fec_mod, trans_inv_enc.fecha_ultimo_inventario, propietarios.nombre_comercial AS Propietario, 
                         bodega.nombre AS Bodega, TipoConteo.Descripcion AS Conteo, TipoInventario.Descripcion AS Inventario,trans_inv_enc.EsSistema, trans_inv_enc.cambia_ubicacion,
                         trans_inv_enc.mostrar_cantidad_teorica_hh,trans_inv_enc.IdProductoFamilia,trans_inv_enc.IdBodegaVirtual,trans_inv_enc.capturar_no_existente,trans_inv_enc.multi_propietario,
                         trans_inv_enc.IdCentroCosto, trans_inv_enc.Tipo_Asignacion, trans_inv_enc.Capturar_No_Asignados
                         FROM TipoInventario RIGHT OUTER JOIN
                         trans_inv_enc ON TipoInventario.IdTipoInv = trans_inv_enc.idtipoinventario LEFT OUTER JOIN
                         TipoConteo ON trans_inv_enc.tipo_conteo_producto = TipoConteo.IdTipoConteo LEFT OUTER JOIN
                         bodega ON trans_inv_enc.idbodega = bodega.idbodega  LEFT OUTER JOIN
                         propietarios ON trans_inv_enc.idpropietario = propietarios.IdPropietario
                         WHERE trans_inv_enc.idbodega = {0}  AND TipoInventario.Es_RFID = 1
                         AND cast(trans_inv_enc.Fecha as Date) >={1} and cast(trans_inv_enc.Fecha as Date) <={2} ",
                                                   IdBodega, FormatoFechas.fFecha(pFechaInicio), FormatoFechas.fFecha(pFechaFin))

                If pActivos Then
                    vSQL += " AND trans_inv_enc.activo = 1 "
                Else
                    vSQL += " AND trans_inv_enc.activo = 0 "
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeTrans_inv_enc As New clsBeTrans_inv_enc

                    For Each dr As DataRow In lDataTable.Rows

                        vBeTrans_inv_enc = New clsBeTrans_inv_enc

                        Cargar(vBeTrans_inv_enc, dr)

                        If dr("IdBodega") IsNot DBNull.Value AndAlso dr("IdBodega") IsNot Nothing Then
                            vBeTrans_inv_enc.Bodega = New clsBeBodega
                            vBeTrans_inv_enc.Bodega.IdBodega = CType(dr("IdBodega"), Integer)
                            vBeTrans_inv_enc.Bodega.Nombre = CType(dr("Bodega"), String)
                        End If

                        If dr("IdPropietario") IsNot DBNull.Value AndAlso dr("IdPropietario") IsNot Nothing Then

                            If dr("IdPropietario") = 0 Then
                                'vBeTrans_inv_enc.Propietario = clsLnPropietarios.GetSingle(dr("IdPropietario"))
                                vBeTrans_inv_enc.Propietario.Nombre_comercial = "INVENTARIO MULTI-PROPIETARIO"

                            Else
                                vBeTrans_inv_enc.Propietario = New clsBePropietarios
                                vBeTrans_inv_enc.Propietario = clsLnPropietarios.GetSingle(dr("IdPropietario"))
                                vBeTrans_inv_enc.Propietario.Nombre_comercial = vBeTrans_inv_enc.Propietario.Nombre_comercial
                            End If

                        End If

                        If dr("idtipoinventario") IsNot DBNull.Value AndAlso dr("idtipoinventario") IsNot Nothing Then
                            vBeTrans_inv_enc.TipoInv = New clsBeTipoInventario
                            vBeTrans_inv_enc.TipoInv.IdTipoInv = CType(dr("idtipoinventario"), Integer)
                            vBeTrans_inv_enc.TipoInv.Descripcion = If(IsDBNull(dr("Inventario")), String.Empty, CType(dr("Inventario"), String))
                        End If

                        If dr("tipo_conteo_producto") > 0 AndAlso dr("tipo_conteo_producto") IsNot Nothing Then
                            vBeTrans_inv_enc.TipoConteo = New clsBeTipoConteo
                            vBeTrans_inv_enc.TipoConteo.IdTipoConteo = CType(dr("tipo_conteo_producto"), Integer)
                            vBeTrans_inv_enc.TipoConteo.Descripcion = If(IsDBNull(dr("Conteo")), String.Empty, CType(dr("Conteo"), String))
                        End If

                        lReturnList.Add(vBeTrans_inv_enc)
                    Next

                    Return lReturnList

                End Using
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_TeoricoWMS_Vrs_TeoricoERP(ByVal pIdInv As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       lConnection As SqlConnection,
                                                       lTransaction As SqlTransaction) As DataTable

        Get_TeoricoWMS_Vrs_TeoricoERP = Nothing

        Dim vSQL As String = ""

        Try

            vSQL = "SELECT t.Codigo, t.Producto as Nombre, SUM(t.StockERP) AS StockERP , 
                           SUM(t.StockWMS) AS StockWMS, 
                           SUM(t.StockERP) - SUM(t.StockWMS) AS Dif, t.lote AS Lote, t.fecha_vence AS Fecha_Vence,
                           t.UMBas,t.Presentacion
                    FROM (
                    SELECT  idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
			                producto.nombre AS Producto,
				            ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
				            0 AS StockERP,SUM(cant) AS StockWMS,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
				            trans_inv_stock_prod.codigo_area  as ubicacion,
				            unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
		            FROM trans_inv_stock_prod INNER JOIN 
				            producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
				            unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
				            unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
				            producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
		            WHERE idinventario = @IdInventarioEnc
				            AND TipoTeoricoImportacion =0 --#EJC20240724: WMS
		            GROUP BY idinventario,producto.codigo,  
					            producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
					            trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.codigo_area,
					            trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor
		            UNION ALL                     
		            SELECT idinventario AS IdInventario,producto.codigo,producto.IdProducto,   
				            producto.nombre AS Producto,
				            ISNULL(producto_presentacion.nombre,'') AS Presentacion,producto_presentacion.IdPresentacion ,
				            SUM(cant) AS StockERP, 0 StockWMS,0 AS Peso, trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence,
				            trans_inv_stock_prod.codigo_area  as ubicacion,
				            unidad_medida.Nombre UMBas, producto_presentacion.factor, '' AS Licencia
		            FROM trans_inv_stock_prod INNER JOIN 
				            producto ON trans_inv_stock_prod.idproducto = producto.IdProducto INNER JOIN
				            unidad_medida ON producto.IdUnidadMedidaBasica = producto.IdUnidadMedidaBasica AND 
				            unidad_medida.IdUnidadMedida = trans_inv_stock_prod.idunidadmedida LEFT OUTER JOIN 
				            producto_presentacion ON trans_inv_stock_prod.IdPresentacion = producto_presentacion.IdPresentacion
		            WHERE idinventario = @IdInventarioEnc
				            AND TipoTeoricoImportacion =1 --#EJC20240724: ERP
		            GROUP BY idinventario,producto.codigo,  
					            producto.nombre,producto_presentacion.nombre,producto_presentacion.IdPresentacion,producto.IdProducto,
					            trans_inv_stock_prod.Lote, trans_inv_stock_prod.Fecha_Vence, trans_inv_stock_prod.codigo_area,
					            trans_inv_stock_prod.idbodega,unidad_medida.Nombre, producto_presentacion.factor) AS T                                     
            GROUP BY t.lote, t.codigo, t.Producto, t.fecha_vence,t.ubicacion, t.UMBas, t.Presentacion
            ORDER BY T.ubicacion, T.codigo"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_TeoricoWMS_Vrs_TeoricoERP = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class