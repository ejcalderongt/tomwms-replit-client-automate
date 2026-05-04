Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_resumen

    Public Shared Function GetAll(pIdinventarioenct As Integer, pIdtramo As Integer, pIdProducto As Integer) As List(Of clsBeTrans_inv_resumen)
        Dim lReturnList As New List(Of clsBeTrans_inv_resumen)

        Try
            lReturnList = GetAll().FindAll(Function(x) x.Idinventarioenct = pIdinventarioenct).OrderBy(Function(Y) Y.Idtramo).ThenBy(Function(z) z.Nom_producto).ToList()

            If (pIdtramo <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idtramo = pIdtramo).ToList()
            If (pIdProducto <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idproducto = pIdProducto).ToList()

            Return lReturnList
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllGridView() As List(Of clsBeTrans_inv_resumen)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_resumen)
            Const sp As String = "SELECT * FROM Trans_inv_resumen"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_resumen

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_resumen
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
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

    Public Shared Function GetProductosTramo(pIdinventarioenct As Integer, pIdtramo As Integer) As List(Of clsBeProductoCombo)
        Dim lReturnList As New List(Of clsBeProductoCombo)
        Dim prodList As New List(Of clsBeProductoCombo)
        Dim itemList As New List(Of clsBeTrans_inv_resumen)
        Dim prod As clsBeProductoCombo

        Try
            itemList = GetAll(pIdinventarioenct, pIdtramo, 0)

            For Each item As clsBeTrans_inv_resumen In itemList
                prod = New clsBeProductoCombo
                prod.IdProducto = item.Idproducto
                prod.Nombre = item.Idproducto & " - " & item.Nom_producto
                prodList.Add(prod)
            Next

            lReturnList = prodList.GroupBy(Function(x) x.IdProducto).Select(Function(x) x.First).OrderBy(Function(Y) Y.Nombre).ToList

            Return lReturnList
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleByInvRes(ByVal pIdInvRes As Integer) As clsBeTrans_inv_resumen

        Try

            Dim vSQL As String = "SELECT * FROM Trans_inv_resumen Where idinventariores = @idinventariores"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventariores", pIdInvRes)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim ObjProducto As New clsBeTrans_inv_resumen()

                        Cargar(ObjProducto, lRow)

                        Return ObjProducto

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingleByPresentacionResumen(ByVal pIdInv As Integer, ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As clsBeTrans_inv_enc

        Try

            Dim vSQL As String = "SELECT idinventarioenct AS IdInventario,
                    producto.IdProducto,
                    producto_presentacion.IdPresentacion,
                    producto.codigo,  
                    producto.nombre AS Producto,
                    ISNULL(producto_presentacion.nombre,'') AS Presentacion,
                    SUM(cantidad) As Resumen
                    FROM trans_inv_resumen  INNER JOIN 
                    producto ON trans_inv_resumen.idproducto = producto.IdProducto LEFT OUTER JOIN 
                    producto_presentacion ON trans_inv_resumen.IdPresentacion = producto_presentacion.IdPresentacion 
                    WHERE idinventarioenct = @idinventarioenct and producto.IdProducto=@IdProducto and producto_presentacion.IdPresentacion=@IdPresentacion
                    GROUP BY idinventarioenct,producto.codigo,producto_presentacion.IdPresentacion,  
                    producto.nombre,producto_presentacion.nombre,producto.IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenct", pIdInv)
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

                            If lRow("Resumen") IsNot DBNull.Value AndAlso lRow("Resumen") IsNot Nothing Then
                                Obj.Resumen = CType(lRow("Resumen"), Double)
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

    Public Shared Function Get_CantidadInvVer_By_Producto(pIdUbicacion As Integer,
                                                          pIdProducto As Integer,
                                                          pIdBodega As Integer,
                                                          pIdPresentacion As Integer,
                                                          pIdInventarioEnc As Integer) As clsBeTrans_inv_resumen

        Dim lReturnValue As New clsBeTrans_inv_resumen

        Try

            Dim vSQL As String = "select 0 as idinventariores,
                                    idinventarioenct,
                                    idtramo,
                                    idproducto,
                                    idoperador,
                                    IdUnidadMedida,
                                    0 as idpresentacion,
                                    0 as idproductoestado,
                                    sum(cantidad) as Cantidad,
                                    '1900-01-01' as fecha_captura,
                                    host,
                                    nom_producto,
                                    nom_operador,
                                    idubicacion 
                                    from trans_inv_resumen
                                    where idubicacion = @idubicacion
                                    and idproducto =  @idproducto
                                    and idinventarioenct = @idinventarioenct"

            If pIdPresentacion <> 0 Then
                vSQL += " And idpresentacion = @idpresentacion"
            Else
                vSQL += " And idpresentacion = 0"
            End If

            vSQL += " group by idinventarioenct,idtramo,idproducto,idoperador,IdUnidadMedida,idpresentacion,host,nom_producto,nom_operador,idubicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idubicacion", pIdUbicacion)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idproducto", pIdProducto)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenct", pIdInventarioEnc)

                        If pIdPresentacion <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@idpresentacion", pIdPresentacion)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_resunen As New clsBeTrans_inv_resumen

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_resunen = New clsBeTrans_inv_resumen
                            Cargar(vBeTrans_inv_resunen, dr)

                            lReturnValue = vBeTrans_inv_resunen
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnValue

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Verificacion_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                          ByVal pIdBodega As Integer,
                                                          ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_resumen)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_resumen)

            Dim vSQL As String = "select * from trans_inv_resumen
                                where idubicacion = @idubicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idubicacion", pIdUbicacion)
                        'lDataAdapter.SelectCommand.Parameters.AddWithValue("@idproducto", pIdProducto)
                        'lDataAdapter.SelectCommand.Parameters.AddWithValue("@idbodega", pIdBodega)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_resumen As New clsBeTrans_inv_resumen

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_resumen = New clsBeTrans_inv_resumen
                            Cargar(vBeTrans_inv_resumen, dr)
                            lReturnList.Add(vBeTrans_inv_resumen)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Tiempos_Tramos_Verif_DT(pIdInventarioEnc As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As DataTable

        Dim DT As New DataTable

        Dim SQL As String = "SELECT  t.descripcion Tramo,LTRIM(RTRIM(
                                        SUBSTRING(
                                            dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                            CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                            CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                        )
                                    )) AS Nivel,LTRIM(RTRIM(
                                        SUBSTRING(
                                            dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                            CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                            CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                        )
                                    )) AS Columna,min(i.fecha_captura) Fecha_Inicio, max(i.fecha_captura) Fecha_Fin, 
                                DATEDIFF(MINUTE,min(i.fecha_captura),max(i.fecha_captura)) Tiempo_Conteo,
                                COUNT(DISTINCT i.IdUbicacion) UbiContadas
                                FROM trans_inv_resumen i inner join bodega_tramo t on i.idtramo = t.IdTramo and i.idbodega = t.IdBodega
                                WHERE idinventarioenct = @IdInventarioEnc
                                GROUP BY t.descripcion, LTRIM(RTRIM(
                                        SUBSTRING(
                                            dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                            CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                            CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                                CHARINDEX('N', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                        )
                                    )),LTRIM(RTRIM(
                                SUBSTRING(
                                    dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega),
                                    CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega)),
                                    CHARINDEX(' - ', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega) + ' - ', 
			                        CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))) - 
			                        CHARINDEX('C', dbo.Nombre_Completo_Ubicacion(i.idubicacion, i.idbodega))
                                )))
                                UNION
                                select t.descripcion Tramo, '' Nivel, '' Columna, '19000101' Fecha_Inicio, '19000101' Fecha_Fin, 
                                0 Diferencia, 0 UbiContadas
                                from trans_inv_tramo i inner join bodega_tramo t on i.idtramo = t.IdTramo and i.idbodega = t.IdBodega
                                where idinventario = @IdInventarioEnc and i.idtramo not in (SELECT i.idtramo
                                FROM trans_inv_resumen i inner join bodega_tramo t on i.idtramo = t.IdTramo and i.idbodega = t.IdBodega
                                WHERE idinventarioenct = @IdInventarioEnc)"

        Using da As New SqlDataAdapter(SQL, lConnection)
            da.SelectCommand.Transaction = lTransaction
            da.SelectCommand.CommandType = CommandType.Text

            da.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)

            da.Fill(DT)
        End Using
        Return DT
    End Function

End Class
