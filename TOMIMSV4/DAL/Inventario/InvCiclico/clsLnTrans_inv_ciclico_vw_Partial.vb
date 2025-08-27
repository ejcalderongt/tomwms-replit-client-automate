Imports System.Data.SqlClient

Partial Public Class clsLnTrans_inv_ciclico_vw

    Public Shared Function GetAllByOperador(pidinventarioenc As Integer, pidoperador As Integer, ppendiente As Boolean) As List(Of clsBeTrans_inv_ciclico_vw)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_vw)
            Dim ubic As New clsBeBodega_ubicacion

            Dim sp As String = "SELECT trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, 
                         trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.EsNuevo, trans_inv_ciclico.lote_stock, 
                         trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.fecha_vence, 
                         sum(trans_inv_ciclico.cant_stock) as cant_stock, 
                         sum(trans_inv_ciclico.cantidad) as cantidad, 
                         sum(trans_inv_ciclico.cant_reconteo) as cant_reconteo, 
                         sum(trans_inv_ciclico.peso_stock) as peso_stock, 
                         sum(trans_inv_ciclico.peso) as peso, 
                         sum(trans_inv_ciclico.peso_reconteo) as peso_reconteo, 
                         trans_inv_ciclico.idoperador, trans_inv_ciclico.user_agr, trans_inv_ciclico.fec_agr, 
                         bodega_ubicacion.IdTramo, producto_estado.nombre AS estado_nombre, producto.nombre AS producto_nombre, 
                         bodega_ubicacion.descripcion AS ubic_nombre, producto_presentacion.nombre AS pres_nombre, unidad_medida.Nombre AS unid_nombre ,
                         producto.control_peso AS control_peso, producto.control_peso AS genera_lote, producto.control_peso AS control_vencimiento , 
                         trans_inv_ciclico.idPresentacion_nuevo , trans_inv_ciclico.IdProductoEst_nuevo , 0 as idreconteo, producto.codigo as codigo_producto,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x as Columna, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos as Posicion
                         FROM unidad_medida RIGHT OUTER JOIN
                         trans_inv_ciclico INNER JOIN
                         bodega_ubicacion ON trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                         producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                         producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                         producto ON producto_bodega.IdProducto = producto.IdProducto ON unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica 
                         inner join bodega_tramo on bodega_ubicacion.idtramo = bodega_tramo.idtramo LEFT OUTER JOIN
                         producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                         WHERE (trans_inv_ciclico.idinventarioenc=@idinventarioenc) AND ((trans_inv_ciclico.idoperador=@idoperador)) 
                         GROUP BY trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, 
                         trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.EsNuevo, trans_inv_ciclico.lote_stock, 
                         trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.fecha_vence, 
                         trans_inv_ciclico.idoperador, trans_inv_ciclico.user_agr, trans_inv_ciclico.fec_agr, 
                         bodega_ubicacion.IdTramo, producto_estado.nombre, producto.nombre, 
                         bodega_ubicacion.descripcion, producto_presentacion.nombre, unidad_medida.Nombre,
                         producto.control_peso, producto.control_peso, producto.control_peso, 
                         trans_inv_ciclico.idPresentacion_nuevo , trans_inv_ciclico.IdProductoEst_nuevo, producto.codigo,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos "
            'WHERE(trans_inv_ciclico.idinventarioenc =@idinventarioenc) And ((trans_inv_ciclico.idoperador=@idoperador) Or (trans_inv_ciclico.idoperador=0)) And (trans_inv_ciclico.esnuevo=0) "

            'If ppendiente Then sp &= " And (trans_inv_ciclico.user_agr ='') "

            sp &= " UNION "

            sp &= "SELECT trans_inv_reconteo.idinventarioenc, trans_inv_reconteo.IdProductoBodega, trans_inv_reconteo.IdProductoEstado, 
                         trans_inv_reconteo.IdPresentacion, trans_inv_reconteo.IdUbicacion, trans_inv_reconteo.EsNuevo, '' AS Lote_Stock, 
                         trans_inv_reconteo.lote, GETDATE() AS fecha_vence_stock, 
                         trans_inv_reconteo.fecha_vence, 0 AS cant_stock, 
                         SUM(trans_inv_reconteo.cantidad) AS Cantidad, 0 AS cant_reconteo, 0 AS peso_stock, 
                         SUM(trans_inv_reconteo.peso) AS Peso, 0 AS peso_reconteo, trans_inv_reconteo.IdOperador, 
                         trans_inv_reconteo.user_agr, trans_inv_reconteo.fec_agr, bodega_ubicacion.IdTramo, producto_estado.nombre AS estado_nombre, producto.nombre AS producto_nombre, 
                         bodega_ubicacion.descripcion AS ubic_nombre, producto_presentacion.nombre AS pres_nombre, unidad_medida.Nombre AS unid_nombre, producto.control_peso, producto.control_peso AS genera_lote, 
                         producto.control_peso AS control_vencimiento, 0 AS IdPresentacion_nuevo, 0 AS IdProductoEst_nuevo, trans_inv_enc_reconteo.reconteo AS idreconteo, producto.codigo as codigo_producto,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x as Columna, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos as Posicion
                         FROM  trans_inv_enc_reconteo INNER JOIN
                         trans_inv_reconteo INNER JOIN
                         bodega_ubicacion ON trans_inv_reconteo.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                         producto_estado ON trans_inv_reconteo.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                         producto_bodega ON trans_inv_reconteo.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                         producto ON producto_bodega.IdProducto = producto.IdProducto ON trans_inv_enc_reconteo.idinvencreconteo = trans_inv_reconteo.idreconteo LEFT OUTER JOIN
                         unidad_medida ON producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN 
                         producto_presentacion ON trans_inv_reconteo.IdPresentacion = producto_presentacion.IdPresentacion
                         WHERE (trans_inv_reconteo.idinventarioenc =@idinventarioenc) AND ((trans_inv_reconteo.idoperador=@idoperador))  
                         GROUP BY  trans_inv_reconteo.idinventarioenc, trans_inv_reconteo.IdProductoBodega, trans_inv_reconteo.IdProductoEstado, 
                         trans_inv_reconteo.IdPresentacion, trans_inv_reconteo.IdUbicacion, trans_inv_reconteo.EsNuevo, trans_inv_reconteo.lote, 
                         trans_inv_reconteo.fecha_vence, trans_inv_reconteo.cantidad, trans_inv_reconteo.peso, trans_inv_reconteo.IdOperador, 
                         trans_inv_reconteo.user_agr, trans_inv_reconteo.fec_agr, bodega_ubicacion.IdTramo, producto_estado.nombre,
                         producto.nombre, bodega_ubicacion.descripcion, producto_presentacion.nombre, unidad_medida.Nombre, producto.control_peso, 
                         producto.control_peso, producto.control_peso, trans_inv_enc_reconteo.reconteo, producto.codigo,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos"

            'If ppendiente Then sp &= " And (trans_inv_reconteo.user_agr='') "

            sp &= " UNION "

            sp &= "SELECT  trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, trans_inv_ciclico.IdPresentacion, 
                            trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.EsNuevo, trans_inv_ciclico.lote_stock, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.fecha_vence, 
                            trans_inv_ciclico.cant_stock, trans_inv_ciclico.cantidad, trans_inv_ciclico.cant_reconteo, trans_inv_ciclico.peso_stock, trans_inv_ciclico.peso, trans_inv_ciclico.peso_reconteo, 
                            trans_inv_ciclico.idoperador, trans_inv_ciclico.user_agr, trans_inv_ciclico.fec_agr, 1 AS idtramo, producto_estado.nombre AS estado_nombre, producto.nombre AS producto_nombre, 'ubic' AS ubic_nombre, 
                            producto_presentacion.nombre AS pres_nombre, unidad_medida.Nombre AS unid_nombre, producto.control_peso, producto.control_lote AS genera_lote, producto.control_vencimiento, 
                            trans_inv_ciclico.IdPresentacion_nuevo, trans_inv_ciclico.IdProductoEst_nuevo, 0 AS idreconteo, producto.codigo as codigo_producto,
						    0 AS IdTramo, 0 AS Columna, 0 AS nivel,'' as Posicion
                            FROM  trans_inv_ciclico INNER JOIN
                            producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                            producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                            producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                            unidad_medida ON producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                            producto_presentacion ON producto.IdProducto = producto_presentacion.IdProducto AND trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                            WHERE (trans_inv_ciclico.idinventarioenc=@idinventarioenc)  AND (trans_inv_ciclico.idoperador = @idoperador) AND (trans_inv_ciclico.IdStock = 0) 
                            GROUP BY trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, 
                            trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.EsNuevo, trans_inv_ciclico.lote_stock, 
                            trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.fecha_vence, 
                            trans_inv_ciclico.cant_stock, trans_inv_ciclico.cantidad, trans_inv_ciclico.cant_reconteo, trans_inv_ciclico.peso_stock, 
                            trans_inv_ciclico.peso, trans_inv_ciclico.peso_reconteo,  trans_inv_ciclico.idoperador, trans_inv_ciclico.user_agr, 
                            trans_inv_ciclico.fec_agr, producto_estado.nombre, producto.nombre, producto_presentacion.nombre, unidad_medida.Nombre, 
                            producto.control_peso, producto.control_lote, producto.control_vencimiento, 
                            trans_inv_ciclico.IdPresentacion_nuevo, trans_inv_ciclico.IdProductoEst_nuevo, producto.codigo"

            'If ppendiente Then
            '    sp &= " AND (dbo.trans_inv_ciclico.EsNuevo = 0) "
            'End If

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction()

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pidinventarioenc)
            dad.SelectCommand.Parameters.AddWithValue("@idoperador", pidoperador)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico_vw As New clsBeTrans_inv_ciclico_vw

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_ciclico_vw = New clsBeTrans_inv_ciclico_vw

                Cargar(vBeTrans_inv_ciclico_vw, dr)

                If vBeTrans_inv_ciclico_vw.Pres_nombre = "" Then vBeTrans_inv_ciclico_vw.Pres_nombre = vBeTrans_inv_ciclico_vw.Unid_nombre

                If vBeTrans_inv_ciclico_vw.IdUbicacion = 0 Then
                    vBeTrans_inv_ciclico_vw.Ubic_nombre = "0"
                Else
                    ubic = New clsBeBodega_ubicacion
                    ubic.IdUbicacion = vBeTrans_inv_ciclico_vw.IdUbicacion
                    vBeTrans_inv_ciclico_vw.Ubicacion.IdUbicacion = vBeTrans_inv_ciclico_vw.IdUbicacion
                    clsLnBodega_ubicacion.GetSingle(vBeTrans_inv_ciclico_vw.Ubicacion, lConnection, lTransaction)
                    vBeTrans_inv_ciclico_vw.Ubic_nombre = vBeTrans_inv_ciclico_vw.Ubicacion.NombreCompleto
                End If

                lReturnList.Add(vBeTrans_inv_ciclico_vw)

            Next

            lReturnList = lReturnList.OrderByDescending(Function(x) x.Ubicacion.Tramo.Descripcion).ThenBy(Function(x) x.Ubicacion.Indice_x).ThenBy(Function(x) x.Ubicacion.Nivel).ToList()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc_And_IdOperador(ByVal pIdInventarioenc As Integer,
                                                                     ByVal pIdOperador As Integer,
                                                                     ByVal pPendiente As Boolean) As DataTable

        Dim lconection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = ""

            If pPendiente Then
                sp += "SELECT *, (SELECT COUNT(t.IdProductoBodega) from 
                                             (SELECT IdProductoBodega
                                             FROM trans_inv_ciclico i
                                             WHERE i.idinventarioenc=@idinventarioenc AND i.idoperador=@idoperador 
                                             GROUP BY i.IdProductoBodega, i.IdProductoEstado, i.IdPresentacion, i.IdUbicacion, i.lote, CONVERT(DATE,i.fecha_vence)
                                             HAVING SUM(i.cantidad) = 0) as T) AS TOTAL 
                       FROM (SELECT "
            Else
                sp += "SELECT *, (SELECT COUNT(t.IdProductoBodega) from 
                                             (SELECT IdProductoBodega
                                             FROM trans_inv_ciclico i
                                             WHERE i.idinventarioenc=@idinventarioenc AND i.idoperador=@idoperador
                                             GROUP BY i.IdProductoBodega, i.IdProductoEstado, i.IdPresentacion, i.IdUbicacion, i.lote, CONVERT(DATE,i.fecha_vence)
                                             HAVING SUM(i.cantidad) > 0) as T) AS TOTAL 
                       FROM (SELECT "
            End If


            '*******************************************************************************************************************************************************
            'GT29122020 NUEVAS VALIDACIONES EN PRIMER QUERY PARA EVITAR AGREGAR PRODUCTO NUEVO CON EL CONTEO, ULTIMO QUERY VALIDA QUE SOLO DEVUELVA NUEVO PRODUCTO
            'GT01122021 Se agrega licence plate para ser consumida en la HH, durante el inventario ciclico. 

            sp += " trans_inv_ciclico.idinventarioenc,						
						'0' as idinvreconteo,
                        trans_inv_ciclico.IdProductoBodega, 
                        trans_inv_ciclico.IdProductoEstado,
                        trans_inv_ciclico.IdPresentacion, 
                        trans_inv_ciclico.IdUbicacion,
                        trans_inv_ciclico.EsNuevo, 
                        trans_inv_ciclico.lote_stock,
                        trans_inv_ciclico.lote, 
                        CONVERT(DATE,trans_inv_ciclico.fecha_vence_stock) AS fecha_vence_stock, 
                        CONVERT(DATE,trans_inv_ciclico.fecha_vence) AS fecha_vence,
                        sum(trans_inv_ciclico.cant_stock) as cant_stock,
                        sum(trans_inv_ciclico.cantidad) as cantidad,
                        sum(trans_inv_ciclico.cant_reconteo) as cant_reconteo,
                        sum(trans_inv_ciclico.peso_stock) as peso_stock,
                        sum(trans_inv_ciclico.peso) as peso,
                        sum(trans_inv_ciclico.peso_reconteo) as peso_reconteo,
                        trans_inv_ciclico.idoperador,
                        trans_inv_ciclico.lic_plate,    
                        trans_inv_ciclico.fec_agr, 
                        bodega_ubicacion.IdTramo, 
                        producto_estado.nombre AS estado_nombre,
                        producto.nombre AS producto_nombre, 
                        ISNULL(dbo.Nombre_Completo_Ubicacion (bodega_ubicacion.IdUbicacion,bodega_ubicacion.IdBodega),'') AS ubic_nombre, 
                        ISNULL(producto_presentacion.nombre,'N/A') AS pres_nombre, 
                        unidad_medida.Nombre AS unid_nombre ,
                        producto.control_peso AS control_peso, 
                        producto.control_lote AS control_lote, 
                        producto.control_vencimiento AS control_vencimiento , 
                        trans_inv_ciclico.idPresentacion_nuevo, 
                        0 as idreconteo, 
                        trans_inv_ciclico.IdProductoEst_nuevo,
                        producto.codigo as codigo_producto,
                        bodega_ubicacion.indice_x as Columna, 
                        bodega_ubicacion.nivel, 
                        bodega_ubicacion.orientacion_pos as Posicion,
                        ISNULL(producto_presentacion.factor,0) AS factor,
                        trans_inv_ciclico.IdUbicacion_nuevo,
                        ISNULL(e.nombre, '') as nuevo_estado,
	                    trans_inv_ciclico.IdStock,
                        trans_inv_ciclico.idinvciclico,
                        trans_inv_ciclico.fec_mod,
                        trans_inv_ciclico.contado
                        FROM unidad_medida RIGHT OUTER JOIN
                        trans_inv_ciclico INNER JOIN
                        bodega_ubicacion ON trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto ON 
						           unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica 
                        INNER JOIN bodega ON producto_bodega.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega 
                        LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                        LEFT JOIN producto_estado e on e.IdEstado = trans_inv_ciclico.IdProductoEst_nuevo
                        WHERE (trans_inv_ciclico.idinventarioenc=@idinventarioenc AND trans_inv_ciclico.idoperador=@idoperador and IdStock <>0) 
						GROUP BY trans_inv_ciclico.idinventarioenc,					    
                        trans_inv_ciclico.IdProductoBodega,
                        trans_inv_ciclico.IdProductoEstado,
                        trans_inv_ciclico.IdPresentacion,
                        trans_inv_ciclico.IdUbicacion,
                        trans_inv_ciclico.EsNuevo,
                        trans_inv_ciclico.lote_stock,
                        trans_inv_ciclico.lote,
                        CONVERT(DATE,trans_inv_ciclico.fecha_vence_stock),
                        CONVERT(DATE,trans_inv_ciclico.fecha_vence),
                        trans_inv_ciclico.idoperador,
                        bodega_ubicacion.IdTramo, 
                        producto_estado.nombre, 
                        producto.nombre,
                        bodega_ubicacion.descripcion, 
                        producto_presentacion.nombre, 
                        unidad_medida.Nombre,
                        producto.control_peso, 
                        producto.control_lote, 
                        producto.control_vencimiento,
                        trans_inv_ciclico.idPresentacion_nuevo, 
                        producto.codigo,
                        bodega_ubicacion.indice_x, 
                        bodega_ubicacion.nivel, 
                        bodega_ubicacion.orientacion_pos,
                        bodega_ubicacion.IdUbicacion,
                        IdProductoEst_nuevo,
                        producto_presentacion.factor,
                        bodega_ubicacion.IdBodega,
						trans_inv_ciclico.lic_plate,
                        trans_inv_ciclico.IdUbicacion_nuevo,
                        e.nombre,       
                        trans_inv_ciclico.IdStock,
                        trans_inv_ciclico.idinvciclico,
                        trans_inv_ciclico.fec_mod, 
                        trans_inv_ciclico.fec_agr,
                        trans_inv_ciclico.contado

            UNION

                        SELECT trans_inv_reconteo.idinventarioenc,
						trans_inv_reconteo.idinvreconteo,
                        trans_inv_reconteo.IdProductoBodega, 
                        trans_inv_reconteo.IdProductoEstado, 
                        trans_inv_reconteo.IdPresentacion, 
                        trans_inv_reconteo.IdUbicacion, 
                        trans_inv_reconteo.EsNuevo, 
                        '' AS lote_stock, 
                        trans_inv_reconteo.lote, 
                        CONVERT(DATE,GETDATE()) AS fecha_vence_stock, 
                        trans_inv_reconteo.fecha_vence, 
                        0 AS cant_stock, 
                        SUM(trans_inv_reconteo.cantidad) AS cantidad, 
                        0 AS cant_reconteo, 
                        0 AS peso_stock, 
                        0 AS peso, 
                        SUM(trans_inv_reconteo.peso) AS peso_reconteo,
                        trans_inv_reconteo.IdOperador, 
                        trans_inv_reconteo.lic_plate,
                        trans_inv_reconteo.fec_agr, 
                        bodega_ubicacion.IdTramo, 
                        producto_estado.nombre AS estado_nombre, 
                        producto.nombre AS producto_nombre, 
						dbo.Nombre_Completo_Ubicacion(trans_inv_reconteo.IdUbicacion,bodega_ubicacion.IdBodega ) AS ubic_nombre,
                        ISNULL(producto_presentacion.nombre,'N/A') AS pres_nombre, 
                        unidad_medida.Nombre AS unid_nombre, 
                        producto.control_peso, 
                        producto.control_lote AS control_lote, 
                        producto.control_vencimiento AS control_vencimiento, 
                        trans_inv_reconteo.idPresentacion, 
                        0 AS idreconteo, 
                        0 AS IdProductoEst_nuevo,                         
                        producto.codigo AS codigo_producto, 
                        bodega_ubicacion.indice_x AS Columna, 
                        bodega_ubicacion.nivel, 
                        bodega_ubicacion.orientacion_pos AS Posicion,
                        ISNULL(producto_presentacion.factor,0) AS factor,
                        0 as IdUbicacion_nuevo,
                        '' as nuevo_estado,
                        0 as IdStock,
                        0 as idinvciclico,
                        '19000101' fec_mod,
                        0 as contado
                        FROM  trans_inv_enc_reconteo INNER JOIN
                        trans_inv_reconteo INNER JOIN
                        bodega_ubicacion ON trans_inv_reconteo.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        producto_estado ON trans_inv_reconteo.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                        producto_bodega ON trans_inv_reconteo.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto ON 
                        trans_inv_enc_reconteo.idinvencreconteo = trans_inv_reconteo.idreconteo INNER JOIN
                        unidad_medida ON producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_reconteo.IdPresentacion = producto_presentacion.IdPresentacion 
                        WHERE (trans_inv_reconteo.idinventarioenc = @idinventarioenc AND trans_inv_reconteo.IdOperador = @idoperador)
						GROUP BY trans_inv_reconteo.idinventarioenc,  trans_inv_reconteo.idinvreconteo, trans_inv_reconteo.IdProductoBodega, trans_inv_reconteo.IdProductoEstado, 
                        trans_inv_reconteo.IdPresentacion, trans_inv_reconteo.IdUbicacion, trans_inv_reconteo.EsNuevo, trans_inv_reconteo.lote, 
                        trans_inv_reconteo.fecha_vence, trans_inv_reconteo.IdOperador, 
                        bodega_ubicacion.IdTramo, producto_estado.nombre, producto.nombre, 
                        bodega_ubicacion.descripcion, producto_presentacion.nombre, unidad_medida.Nombre, producto.control_peso, producto.control_lote, 
                        producto.control_vencimiento, trans_inv_enc_reconteo.reconteo, producto.codigo, bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x, 
                        bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos,bodega_ubicacion.IdUbicacion,
                        producto_presentacion.factor,trans_inv_reconteo.fec_agr,
                        trans_inv_reconteo.lic_plate,bodega_ubicacion.IdBodega

             UNION
                        SELECT trans_inv_ciclico.idinventarioenc,
						'0' as idinvreconteo, 
                        trans_inv_ciclico.IdProductoBodega, 
                        trans_inv_ciclico.IdProductoEstado,  
                        trans_inv_ciclico.IdPresentacion, 
                        trans_inv_ciclico.IdUbicacion,  
                        trans_inv_ciclico.EsNuevo,  
                        trans_inv_ciclico.lote_stock,  
                        trans_inv_ciclico.lote,  
                        trans_inv_ciclico.fecha_vence_stock,
                        CONVERT(DATE,trans_inv_ciclico.fecha_vence) AS fecha_vence, 
                        sum(trans_inv_ciclico.cant_stock) as cant_stock,
                        sum(trans_inv_ciclico.cantidad) as cantidad,
                        sum(trans_inv_ciclico.cant_reconteo) as cant_reconteo,
                        sum(trans_inv_ciclico.peso_stock) as peso_stock,
                        sum(trans_inv_ciclico.peso) as peso,
                        sum(trans_inv_ciclico.peso_reconteo) as peso_reconteo,
                        trans_inv_ciclico.idoperador,  
                        trans_inv_ciclico.lic_plate,
                        trans_inv_ciclico.fec_agr,  
                        1 AS idtramo,  
                        producto_estado.nombre AS estado_nombre,  
                        producto.nombre AS producto_nombre,  
						dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion,trans_inv_ciclico.IdBodega ) AS ubic_nombre,
                        ISNULL(producto_presentacion.nombre,'N/A') AS pres_nombre, 
                        unidad_medida.Nombre AS unid_nombre,  
                        producto.control_peso,  
                        producto.control_lote AS control_lote,  
                        producto.control_vencimiento, 
                        trans_inv_ciclico.IdPresentacion_nuevo,
                        0 AS idreconteo, 
                        trans_inv_ciclico.IdProductoEst_nuevo,                         
                        producto.codigo as codigo_producto,
                        0 AS Columna,  
                        0 AS nivel, 
                        '' as Posicion,
                        ISNULL(producto_presentacion.factor,0) AS factor,
                        trans_inv_ciclico.IdUbicacion_nuevo,
                        ISNULL(e.nombre, '') as nuevo_estado,
                        trans_inv_ciclico.IdStock,
                        trans_inv_ciclico.idinvciclico,
                        trans_inv_ciclico.fec_mod,
                        trans_inv_ciclico.contado
                        FROM  trans_inv_ciclico INNER JOIN
                        producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                        unidad_medida ON producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                        producto_presentacion ON producto.IdProducto = producto_presentacion.IdProducto AND trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
						INNER JOIN bodega_ubicacion ON trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        bodega ON producto_bodega.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega
                        LEFT JOIN producto_estado e on e.IdEstado = trans_inv_ciclico.IdProductoEst_nuevo
						WHERE trans_inv_ciclico.idinventarioenc=@idinventarioenc  AND trans_inv_ciclico.idoperador = @idoperador 
						AND	trans_inv_ciclico.IdStock = 0
						GROUP BY trans_inv_ciclico.idinventarioenc,trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, 
                        trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.EsNuevo, trans_inv_ciclico.lote_stock, 
                        trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.fecha_vence, 
                        trans_inv_ciclico.idoperador, producto_estado.nombre, producto.nombre, producto_presentacion.nombre, unidad_medida.Nombre, 
                        producto.control_peso, producto.control_lote, producto.control_vencimiento, 
                        trans_inv_ciclico.IdPresentacion_nuevo, trans_inv_ciclico.IdProductoEst_nuevo, producto.codigo,
                        producto_presentacion.factor,
						bodega_ubicacion.IdTramo, 
                        producto_estado.nombre, 
                        producto.nombre,
                        bodega_ubicacion.descripcion, 
                        producto_presentacion.nombre, 
                        trans_inv_ciclico.IdUbicacion,
                        trans_inv_ciclico.lic_plate,
						trans_inv_ciclico.IdBodega,
                        trans_inv_ciclico.IdUbicacion_nuevo,
                        e.nombre,
                        trans_inv_ciclico.IdStock,
                        trans_inv_ciclico.idinvciclico,
                        trans_inv_ciclico.fec_mod, 
                        trans_inv_ciclico.fec_agr,
                        trans_inv_ciclico.contado
						) AS T  "

            '#CKFK20250705 Agregué comparación por fecha
            If pPendiente Then
                'sp += "(T.cantidad = 0 AND CONVERT(varchar(19), T.fec_agr, 120) = CONVERT(varchar(19), T.fec_mod, 120))"
                sp += " WHERE (T.contado = 0) "
            Else
                'sp += " WHERE (T.cantidad > 0 or CONVERT(varchar(19), T.fec_agr, 120) <> CONVERT(varchar(19), T.fec_mod, 120)) "
                sp += " WHERE (T.contado = 1) "
            End If

            sp += " Order by T.IdTramo, T.Columna, T.Nivel, T.Posicion"

            lconection.Open()

            ltransaction = lconection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lconection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInventarioenc)
            dad.SelectCommand.Parameters.AddWithValue("@idoperador", pIdOperador)
            Dim dt As New DataTable("Inventario")
            dad.Fill(dt)

            ltransaction.Commit()

            Return dt

        Catch ex As Exception
            ltransaction.Rollback()
            Throw ex
        Finally
            If Not lconection Is Nothing AndAlso lconection.State = ConnectionState.Open Then lconection.Close()
        End Try

    End Function

    Public Shared Function GetAllByOperador_old2(pidinventarioenc As Integer, pidoperador As Integer, ppendiente As Boolean) As List(Of clsBeTrans_inv_ciclico_vw)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_vw)
            Dim ubic As New clsBeBodega_ubicacion

            Dim sp As String = "SELECT dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdStock, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.IdProductoEstado, 
                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.IdUbicacion, dbo.trans_inv_ciclico.EsNuevo, dbo.trans_inv_ciclico.lote_stock, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence_stock, 
                         dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.cant_reconteo, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.peso, 
                         dbo.trans_inv_ciclico.peso_reconteo, dbo.trans_inv_ciclico.idoperador, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.fec_agr, dbo.bodega_ubicacion.IdTramo, dbo.producto_estado.nombre AS estado_nombre, 
                         dbo.producto.nombre AS producto_nombre, dbo.bodega_ubicacion.descripcion AS ubic_nombre, dbo.producto_presentacion.nombre AS pres_nombre, dbo.unidad_medida.Nombre AS unid_nombre ,
                         dbo.producto.control_peso AS control_peso, dbo.producto.control_peso AS genera_lote, dbo.producto.control_peso AS control_vencimiento , 
                         dbo.trans_inv_ciclico.idPresentacion_nuevo , dbo.trans_inv_ciclico.IdProductoEst_nuevo , 0 as idreconteo, dbo.producto.codigo as codigo_producto,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x as Columna, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos as Posicion
                         FROM dbo.unidad_medida RIGHT OUTER JOIN
                         dbo.trans_inv_ciclico INNER JOIN
                         dbo.bodega_ubicacion ON dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                         dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica 
                         inner join bodega_tramo on bodega_ubicacion.idtramo = bodega_tramo.idtramo LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                         WHERE (dbo.trans_inv_ciclico.idinventarioenc=@idinventarioenc) AND ((dbo.trans_inv_ciclico.idoperador=@idoperador)) "

            sp &= " UNION "

            sp &= "SELECT  dbo.trans_inv_reconteo.idinvreconteo AS idinvciclico, dbo.trans_inv_reconteo.idinventarioenc, dbo.trans_inv_reconteo.IdStock, dbo.trans_inv_reconteo.IdProductoBodega, dbo.trans_inv_reconteo.IdProductoEstado, 
                         dbo.trans_inv_reconteo.IdPresentacion, dbo.trans_inv_reconteo.IdUbicacion, dbo.trans_inv_reconteo.EsNuevo, '' AS Lote_Stock, dbo.trans_inv_reconteo.lote, GETDATE() AS fecha_vence_stock, 
                         dbo.trans_inv_reconteo.fecha_vence, 0 AS cant_stock, dbo.trans_inv_reconteo.cantidad, 0 AS cant_reconteo, 0 AS peso_stock, dbo.trans_inv_reconteo.peso, 0 AS peso_reconteo, dbo.trans_inv_reconteo.IdOperador, 
                         dbo.trans_inv_reconteo.user_agr, dbo.trans_inv_reconteo.fec_agr, dbo.bodega_ubicacion.IdTramo, dbo.producto_estado.nombre AS estado_nombre, dbo.producto.nombre AS producto_nombre, 
                         dbo.bodega_ubicacion.descripcion AS ubic_nombre, dbo.producto_presentacion.nombre AS pres_nombre, dbo.unidad_medida.Nombre AS unid_nombre, dbo.producto.control_peso, dbo.producto.control_peso AS genera_lote, 
                         dbo.producto.control_peso AS control_vencimiento, 0 AS IdPresentacion_nuevo, 0 AS IdProductoEst_nuevo, dbo.trans_inv_enc_reconteo.reconteo AS idreconteo, dbo.producto.codigo as codigo_producto,
						 bodega_ubicacion.IdTramo, bodega_ubicacion.indice_x as Columna, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos as Posicion
                         FROM  dbo.trans_inv_enc_reconteo INNER JOIN
                         dbo.trans_inv_reconteo INNER JOIN
                         dbo.bodega_ubicacion ON dbo.trans_inv_reconteo.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                         dbo.producto_estado ON dbo.trans_inv_reconteo.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.producto_bodega ON dbo.trans_inv_reconteo.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.trans_inv_enc_reconteo.idinvencreconteo = dbo.trans_inv_reconteo.idreconteo LEFT OUTER JOIN
                         dbo.unidad_medida ON dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN 
                         dbo.producto_presentacion ON dbo.trans_inv_reconteo.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                         WHERE (dbo.trans_inv_reconteo.idinventarioenc =@idinventarioenc) AND ((dbo.trans_inv_reconteo.idoperador=@idoperador))  "

            sp &= " UNION "

            sp &= "SELECT  dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdStock, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.IdProductoEstado, dbo.trans_inv_ciclico.IdPresentacion, 
                            dbo.trans_inv_ciclico.IdUbicacion, dbo.trans_inv_ciclico.EsNuevo, dbo.trans_inv_ciclico.lote_stock, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.fecha_vence, 
                            dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.cant_reconteo, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.peso, dbo.trans_inv_ciclico.peso_reconteo, 
                            dbo.trans_inv_ciclico.idoperador, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.fec_agr, 1 AS idtramo, dbo.producto_estado.nombre AS estado_nombre, dbo.producto.nombre AS producto_nombre, 'ubic' AS ubic_nombre, 
                            dbo.producto_presentacion.nombre AS pres_nombre, dbo.unidad_medida.Nombre AS unid_nombre, dbo.producto.control_peso, dbo.producto.control_lote AS genera_lote, dbo.producto.control_vencimiento, 
                            dbo.trans_inv_ciclico.IdPresentacion_nuevo, dbo.trans_inv_ciclico.IdProductoEst_nuevo, 0 AS idreconteo, dbo.producto.codigo as codigo_producto,
						    0 AS IdTramo, 0 AS Columna, 0 AS nivel,'' as Posicion
                            FROM  dbo.trans_inv_ciclico INNER JOIN
                            dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                            dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                            dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                            dbo.unidad_medida ON dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                            dbo.producto_presentacion ON dbo.producto.IdProducto = dbo.producto_presentacion.IdProducto AND dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                            WHERE (dbo.trans_inv_ciclico.idinventarioenc=@idinventarioenc)  AND (dbo.trans_inv_ciclico.idoperador = @idoperador) AND (dbo.trans_inv_ciclico.IdStock = 0) "

            lConnection.Open()

            ltransaction = lConnection.BeginTransaction()

            Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pidinventarioenc)
            dad.SelectCommand.Parameters.AddWithValue("@idoperador", pidoperador)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico_vw As New clsBeTrans_inv_ciclico_vw

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_ciclico_vw = New clsBeTrans_inv_ciclico_vw

                Cargar(vBeTrans_inv_ciclico_vw, dr)

                If vBeTrans_inv_ciclico_vw.Pres_nombre = "" Then vBeTrans_inv_ciclico_vw.Pres_nombre = vBeTrans_inv_ciclico_vw.Unid_nombre

                If vBeTrans_inv_ciclico_vw.IdUbicacion = 0 Then
                    vBeTrans_inv_ciclico_vw.Ubic_nombre = "0"
                Else
                    ubic = New clsBeBodega_ubicacion
                    ubic.IdUbicacion = vBeTrans_inv_ciclico_vw.IdUbicacion
                    vBeTrans_inv_ciclico_vw.Ubicacion.IdUbicacion = vBeTrans_inv_ciclico_vw.IdUbicacion
                    clsLnBodega_ubicacion.GetSingle(vBeTrans_inv_ciclico_vw.Ubicacion, lConnection, ltransaction)
                    vBeTrans_inv_ciclico_vw.Ubic_nombre = vBeTrans_inv_ciclico_vw.Ubicacion.NombreCompleto
                End If

                lReturnList.Add(vBeTrans_inv_ciclico_vw)

            Next

            lReturnList = lReturnList.OrderByDescending(Function(x) x.Ubicacion.Tramo.Descripcion).ThenBy(Function(x) x.Ubicacion.Indice_x).ThenBy(Function(x) x.Ubicacion.Nivel).ToList()

            ltransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            ltransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetPaletByOperador(pidinventarioenc As Integer, pidoperador As Integer, ppendiente As Boolean, plicplate As String) As List(Of clsBeTrans_inv_ciclico_vw)

        Dim lconection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico_vw)
            Dim ubic As New clsBeBodega_ubicacion

            Dim sp As String = "SELECT dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdStock, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.IdProductoEstado, 
                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.IdUbicacion, dbo.trans_inv_ciclico.EsNuevo, dbo.trans_inv_ciclico.lote_stock, dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.fecha_vence_stock, 
                         dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.cant_reconteo, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.peso, 
                         dbo.trans_inv_ciclico.peso_reconteo, dbo.trans_inv_ciclico.idoperador, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.fec_agr, dbo.bodega_ubicacion.IdTramo, dbo.producto_estado.nombre AS estado_nombre, 
                         dbo.producto.nombre AS producto_nombre, dbo.bodega_ubicacion.descripcion AS ubic_nombre, dbo.producto_presentacion.nombre AS pres_nombre, dbo.unidad_medida.Nombre AS unid_nombre ,
                         dbo.producto.control_peso AS control_peso, dbo.producto.control_peso AS genera_lote, dbo.producto.control_peso AS control_vencimiento , 
                         dbo.trans_inv_ciclico.idPresentacion_nuevo , dbo.trans_inv_ciclico.IdProductoEst_nuevo , 0 as idreconteo,dbo.producto.codigo,bodega_ubicacion.IdBodega
                         FROM dbo.unidad_medida RIGHT OUTER JOIN
                         dbo.trans_inv_ciclico INNER JOIN
                         dbo.bodega_ubicacion ON dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                         dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.unidad_medida.IdUnidadMedida = dbo.producto.IdUnidadMedidaBasica LEFT OUTER JOIN
                         dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                         WHERE (dbo.trans_inv_ciclico.idinventarioenc=@idinventarioenc) AND (dbo.trans_inv_ciclico.idoperador=@idoperador) AND (trans_inv_ciclico.esnuevo=0) 
                         AND (dbo.trans_inv_ciclico.lic_plate=@licplate)   "

            If ppendiente Then sp &= " AND (trans_inv_ciclico.user_agr='') "

            sp &= " UNION "

            sp &= "SELECT  dbo.trans_inv_reconteo.idinvreconteo AS idinvciclico, dbo.trans_inv_reconteo.idinventarioenc, dbo.trans_inv_reconteo.IdStock, dbo.trans_inv_reconteo.IdProductoBodega, dbo.trans_inv_reconteo.IdProductoEstado, 
                         dbo.trans_inv_reconteo.IdPresentacion, dbo.trans_inv_reconteo.IdUbicacion, dbo.trans_inv_reconteo.EsNuevo, '' AS Lote_Stock, dbo.trans_inv_reconteo.lote, GETDATE() AS fecha_vence_stock, 
                         dbo.trans_inv_reconteo.fecha_vence, 0 AS cant_stock, dbo.trans_inv_reconteo.cantidad, 0 AS cant_reconteo, 0 AS peso_stock, dbo.trans_inv_reconteo.peso, 0 AS peso_reconteo, dbo.trans_inv_reconteo.IdOperador, 
                         dbo.trans_inv_reconteo.user_agr, dbo.trans_inv_reconteo.fec_agr, dbo.bodega_ubicacion.IdTramo, dbo.producto_estado.nombre AS estado_nombre, dbo.producto.nombre AS producto_nombre, 
                         dbo.bodega_ubicacion.descripcion AS ubic_nombre, dbo.producto_presentacion.nombre AS pres_nombre, dbo.unidad_medida.Nombre AS unid_nombre, dbo.producto.control_peso, dbo.producto.control_peso AS genera_lote, 
                         dbo.producto.control_peso AS control_vencimiento, 0 AS IdPresentacion_nuevo, 0 AS IdProductoEst_nuevo, dbo.trans_inv_enc_reconteo.reconteo AS idreconteo,dbo.producto.codigo,bodega_ubicacion.IdBodega
                         FROM  dbo.trans_inv_enc_reconteo INNER JOIN
                         dbo.trans_inv_reconteo INNER JOIN
                         dbo.bodega_ubicacion ON dbo.trans_inv_reconteo.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion INNER JOIN
                         dbo.producto_estado ON dbo.trans_inv_reconteo.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
                         dbo.producto_bodega ON dbo.trans_inv_reconteo.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto ON dbo.trans_inv_enc_reconteo.idinvencreconteo = dbo.trans_inv_reconteo.idreconteo LEFT OUTER JOIN
                         dbo.unidad_medida ON dbo.producto.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida LEFT OUTER JOIN 
                         dbo.producto_presentacion ON dbo.trans_inv_reconteo.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                         WHERE (dbo.trans_inv_reconteo.idinventarioenc =@idinventarioenc) And (dbo.trans_inv_reconteo.idoperador=@idoperador)   
                         AND (dbo.trans_inv_reconteo.lic_plate=@licplate)   "

            If ppendiente Then sp &= " And (trans_inv_reconteo.user_agr='') "

            lconection.Open()

            ltransaction = lconection.BeginTransaction()

            Dim cmd As New SqlCommand(sp, lconection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pidinventarioenc)
            dad.SelectCommand.Parameters.AddWithValue("@idoperador", pidoperador)
            dad.SelectCommand.Parameters.AddWithValue("@licplate", plicplate)

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_ciclico_vw As New clsBeTrans_inv_ciclico_vw

            For Each dr As DataRow In dt.Rows

                vBeTrans_inv_ciclico_vw = New clsBeTrans_inv_ciclico_vw

                Cargar(vBeTrans_inv_ciclico_vw, dr)
                vBeTrans_inv_ciclico_vw.IdBodega = CType(dr("IdBodega"), Int32)

                If vBeTrans_inv_ciclico_vw.Pres_nombre = "" Then vBeTrans_inv_ciclico_vw.Pres_nombre = vBeTrans_inv_ciclico_vw.Unid_nombre

                ubic.IdUbicacion = vBeTrans_inv_ciclico_vw.IdUbicacion
                'clsLnBodega_ubicacion.GetSingle(ubic)
                vBeTrans_inv_ciclico_vw.Ubic_nombre = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(ubic.IdUbicacion, vBeTrans_inv_ciclico_vw.IdBodega, lconection, ltransaction)

                lReturnList.Add(vBeTrans_inv_ciclico_vw)

            Next

            Return lReturnList

            ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If Not lconection Is Nothing AndAlso lconection.State = ConnectionState.Open Then lconection.Close()
        End Try

    End Function

End Class
