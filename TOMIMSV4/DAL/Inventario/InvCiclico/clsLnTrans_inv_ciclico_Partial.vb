Imports System.Data.SqlClient
Imports System.Reflection
Imports System.IO

Partial Public Class clsLnTrans_inv_ciclico

    Private Shared Sub InvRegularizacionTrace(ByVal pSesion As String,
                                              ByVal pPaso As String,
                                              ByVal pInicio As DateTime,
                                              Optional ByVal pExtra As String = "")
        Try
            Dim vDir As String = Path.Combine(Path.GetTempPath(), "TOMWMS")
            If Not Directory.Exists(vDir) Then Directory.CreateDirectory(vDir)

            Dim vLinea As String = String.Join("|", New String() {
                Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                "#EJC20260607_INV_REG_TRACE",
                "clsLnTrans_inv_ciclico",
                pSesion,
                pPaso,
                "DeltaMs=" & CLng((Date.Now - pInicio).TotalMilliseconds),
                pExtra
            })

            File.AppendAllText(Path.Combine(vDir, "inventario-regularizacion-trace.log"), vLinea & Environment.NewLine, System.Text.Encoding.UTF8)
        Catch
        End Try
    End Sub

    Public Shared Function listaPorOperador(pIdenc As Integer, pIdOperador As Integer) As List(Of clsBeTrans_inv_ciclico)
        Dim lReturnList As List(Of clsBeTrans_inv_ciclico)

        Try
            lReturnList = GetAll().FindAll(Function(x) x.Idinventarioenc = pIdenc).ToList
            lReturnList = GetAll().FindAll(Function(x) x.Idoperador = pIdOperador).ToList

            Return lReturnList
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

    Public Shared Function Get_All_By_Inventario(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT trans_inv_ciclico.IdStock, producto.nombre AS Producto, bodega_ubicacion.IdUbicacion, trans_inv_ciclico.IdProductoBodega, producto.codigo, 
                        trans_inv_ciclico.lote_stock, trans_inv_ciclico.fecha_vence_stock, producto_tipo.NombreTipoProducto AS TipoProducto, ISNULL(operador.nombres,'') + ' - ' +  
                        ISNULL(operador.apellidos,'') AS Operador,CASE WHEN bodega_tramo.es_rack = 1 THEN 
                        'R' + RIGHT('00'+ SUBSTRING(bodega_tramo.descripcion,2,iif(CHARINDEX('-',bodega_tramo.descripcion,0)<0,1,CHARINDEX('-',bodega_tramo.descripcion,0)-2)),2) + ' - ' +
                        'C' + RIGHT('00'+ CONVERT(NVARCHAR(10),bodega_ubicacion.indice_x),2 )+ ' - ' +
                        'T' + SUBSTRING(bodega_tramo.descripcion,iif(CHARINDEX('-',bodega_tramo.descripcion,0)<0,0,CHARINDEX('-',bodega_tramo.descripcion,0)+1),1)+ ' - ' +
                        'N' + RIGHT('00' + CONVERT(NVARCHAR(10),bodega_ubicacion.nivel),2 )+ ' - ' +
                        'Pos' + bodega_ubicacion.orientacion_pos+ ' - ' +
                        '#' + CONVERT(NVARCHAR(10),bodega_ubicacion.IdUbicacion)
                        ELSE bodega_tramo.descripcion END AS Ubicacion
                        FROM trans_inv_ciclico INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
                        bodega_ubicacion ON trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        bodega_tramo ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo INNER JOIN
                        producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto INNER JOIN
                        trans_inv_operador ON trans_inv_ciclico.idinventarioenc = trans_inv_operador.idinventarioenc AND 
                        trans_inv_ciclico.idoperador = trans_inv_operador.idoperador INNER JOIN
                        operador ON trans_inv_operador.idoperador = operador.IdOperador
                        WHERE (trans_inv_ciclico.idinventarioenc = @idinventario)
                        GROUP BY trans_inv_ciclico.IdStock, bodega_ubicacion.descripcion, producto.nombre, bodega_ubicacion.IdUbicacion, 
                        trans_inv_ciclico.IdProductoBodega, producto.codigo, trans_inv_ciclico.lote_stock, trans_inv_ciclico.fecha_vence_stock, bodega_tramo.es_rack, 
                        bodega_tramo.descripcion, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, 
                        producto_tipo.NombreTipoProducto, operador.nombres, operador.apellidos"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                Obj.Ubicacion = CType(lRow("Ubicacion"), String)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                Obj.IdStock = CType(lRow("IdStock"), Integer)
                            End If

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                                Obj.Lote_stock = CType(lRow("lote_stock"), String)
                            End If

                            If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                                Obj.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                            End If

                            If lRow("TipoProducto") IsNot DBNull.Value AndAlso lRow("TipoProducto") IsNot Nothing Then
                                Obj.TipoProducto = CType(lRow("TipoProducto"), String)
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

    Public Shared Function GetAllByInventarioEnc(ByVal pIdInventario As Integer, ByVal Fecha As Date) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT  trans_inv_ciclico.IdProductoBodega,trans_inv_ciclico.idinventarioenc,
                             (SELECT SUM(cantidad) AS Expr1
                               FROM  trans_movimientos
                               WHERE (IdTipoTarea = 1) AND (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND 
                                                         (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND (fecha > " + FormatoFechas.fFechaHora(Fecha) + ") and (fecha < trans_inv_ciclico.fec_agr)) AS Recepciones,
                             (SELECT SUM(cantidad) AS Expr1
                               FROM  trans_movimientos AS trans_movimientos_1
                               WHERE (IdTipoTarea = 5) AND (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND (fecha > " + FormatoFechas.fFechaHora(Fecha) + ") 
                    and (fecha < trans_inv_ciclico.fec_agr)) AS despachos, producto.codigo, 
                         producto.nombre
                    FROM  trans_inv_ciclico INNER JOIN
                         producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                         producto ON producto_bodega.IdProducto = producto.IdProducto
               WHERE     trans_inv_ciclico.idinventarioenc = @idinventario and  
                        (NOT ((SELECT SUM(cantidad) AS Expr1
                                 FROM trans_movimientos AS trans_movimientos_1
                                 WHERE (IdTipoTarea = 5) AND (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND 
                                                          (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND (fecha > " + FormatoFechas.fFechaHora(Fecha) + ") and (fecha < trans_inv_ciclico.fec_agr)) IS NULL)) OR
                         (NOT ((SELECT SUM(cantidad) AS Expr1
                                 FROM  trans_movimientos AS trans_movimientos_2
                                 WHERE (IdTipoTarea = 1) AND (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND 
                                 (IdProductoBodega = trans_inv_ciclico.IdProductoBodega) AND (fecha > " + FormatoFechas.fFechaHora(Fecha) + ")  and  (fecha < trans_inv_ciclico.fec_agr)) IS NULL)) "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                            End If

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("Recepciones") IsNot DBNull.Value AndAlso lRow("Recepciones") IsNot Nothing Then
                                Obj.Recepciones = CType(lRow("Recepciones"), Integer)
                            End If

                            If lRow("despachos") IsNot DBNull.Value AndAlso lRow("despachos") IsNot Nothing Then
                                Obj.Despachos = CType(lRow("despachos"), Integer)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                Obj.Producto = CType(lRow("nombre"), String)
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

    Public Shared Function Get_By_IdStock_And_IdInventarioEnc(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico Where(IdStock = @IdStock AND idinventarioenc=@idinventarioenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            ElseIf dt.Rows.Count > 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerByStockEliminar(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico 
                                  Where(IdStock = @IdStock AND idinventarioenc=@idinventarioenc AND IdProductoBodega=@IdProductoBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            ElseIf dt.Rows.Count > 1 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerByStockEliminarByIdOperador(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico 
                                  Where(idinventarioenc=@idinventarioenc AND IdProductoBodega=@IdProductoBodega and IdOperador=@IdOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function UpdateByStockEliminarByIdOperador(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "Update Trans_inv_ciclico
                                  Set IdOperador=0
                                  Where(idinventarioenc=@idinventarioenc AND IdProductoBodega=@IdProductoBodega and IdOperador=@IdOperador)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ObtenerByUbicacion(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico" &
            " Where(IdUbicacion = @IdUbicacion AND IdStock=@IdStock AND idinventarioenc=@idinventarioenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existen_Ubicaciones_Insertadas(ByVal IdUbicacion As Integer, ByVal IdInventario As Integer) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico" &
            " Where(IdUbicacion = @IdUbicacion AND idinventarioenc=@idinventarioenc)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count >= 1

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Ubic_Con_Otro_IdStock(ByVal IdUbicacion As Integer, ByVal IdInventario As Integer, ByVal IdStock As Integer) As Boolean

        Try

            Const sp As String = "SELECT * FROM Trans_inv_ciclico 
                                  WHERE IdUbicacion = @IdUbicacion AND idinventarioenc=@idinventarioenc
                                        AND idStock <> @idStock "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idStock", IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetOperadoresByInventario(ByVal pIdUbicacion As Integer,
                                                                 ByVal pIdInventario As Integer,
                                                                  ByVal pIdStock As Integer,
                                                                    ByVal pIdProductoBodega As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT operador.nombres AS Nombre, operador.apellidos, trans_inv_ciclico.idinvciclico, trans_inv_ciclico.idinventarioenc
                        FROM operador INNER JOIN
                         trans_inv_ciclico ON operador.IdOperador = trans_inv_ciclico.idoperador
                        WHERE trans_inv_ciclico.IdUbicacion =@IdUbicacion AND trans_inv_ciclico.idinventarioenc=@IdInventario AND trans_inv_ciclico.IdStock = @IdStock AND trans_inv_ciclico.IdProductoBodega=@IdProductoBodega"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventario", pIdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                    Dim lDataTable As New DataTable

                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal IdUbic As Integer, ByVal IdInventario As Integer, ByVal pIdSTock As Integer) As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT Distinct operador.IdOperador, 
                                             operador.nombres + ' ' + operador.apellidos Nombre
                                      FROM operador INNER JOIN
                                           trans_inv_ciclico ON operador.IdOperador = trans_inv_ciclico.idoperador
                                      WHERE trans_inv_ciclico.IdUbicacion=@idubic AND 
                                            trans_inv_ciclico.idinventarioenc=@idinv AND 
                                            trans_inv_ciclico.IdStock=@IdStock"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idubic", IdUbic)
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinv", IdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdSTock)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try
    End Function

    Public Shared Function EliminarConteoByIdStock(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Update Trans_inv_ciclico set cantidad = 0
                                   Where(IdStock = @IdStock AND 
                                         idinventarioenc=@idinventarioenc AND 
                                         IdProductoBodega=@IdProductoBodega And 
                                         IdOperador=@IdOperador And 
                                         IdInvCiclico=@IdInvCiclico)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))

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

    Public Shared Function EliminarByOperador(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico" &
             "  Where(idoperador = @idoperador AND idinventarioenc=@idinventarioenc AND IdStock=@IdStock AND IdProductoBodega=@IdProductoBodega )"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))

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

    Public Shared Function Get_All_By_InventarioStock(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '            Dim vSQL As String = "SELECT bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion AS Ubicacion, bodega_tramo.descripcion AS Tramo, 
                    '                        dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS NOMBRE_COMPLETO,
                    '                        trans_inv_ciclico.IdStock, producto.codigo AS Codigo, producto.nombre AS Producto, ISNULL(producto_presentacion.nombre, '') AS Presentacion, 
                    '                        trans_inv_ciclico.lote, trans_inv_ciclico.lote_stock, producto_estado.nombre AS Estado, trans_inv_ciclico.cantidad AS Cantidad_Ciclico, 
                    '                        trans_inv_ciclico.peso AS Peso_Ciclico, producto.IdPropietario, producto.IdClasificacion, producto.IdFamilia, producto_estado.IdEstado, 
                    '                        trans_inv_ciclico.EsNuevo, bodega_tramo.IdTramo, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.idinventarioenc, operador.IdOperador, 
                    '                        operador.nombres, trans_inv_ciclico.idinvciclico, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.user_agr, trans_inv_ciclico.EsPallet, 
                    '                        trans_inv_ciclico.lic_plate, trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.peso_stock AS Peso_Stock, 
                    '                        trans_inv_ciclico.fec_agr, trans_inv_ciclico.cant_stock AS Cantidad_Stock, trans_inv_ciclico.peso_reconteo, 
                    '                        producto_tipo.NombreTipoProducto
                    '                FROM    trans_inv_ciclico 
                    '      INNER JOIN producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega 
                    'INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto 
                    'INNER JOIN producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto 
                    'LEFT OUTER JOIN bodega_tramo 
                    'INNER JOIN bodega_ubicacion ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo AND 
                    '           bodega_tramo.IdBodega = bodega_ubicacion.IdBodega ON 
                    '                                   trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    'LEFT OUTER JOIN operador ON trans_inv_ciclico.idoperador = operador.IdOperador 
                    'LEFT OUTER JOIN producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado 
                    'LEFT OUTER JOIN producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                    '                WHERE   (trans_inv_ciclico.idinventarioenc = @idinventario)
                    '                GROUP BY bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion, bodega_tramo.descripcion, trans_inv_ciclico.IdStock, producto.codigo, 
                    '                        producto.nombre, producto_presentacion.nombre, trans_inv_ciclico.lote, producto_estado.nombre, trans_inv_ciclico.cantidad, 
                    '                        trans_inv_ciclico.peso, producto.IdPropietario, producto.IdClasificacion, producto.IdFamilia, producto_estado.IdEstado, 
                    '                        trans_inv_ciclico.EsNuevo, bodega_tramo.IdTramo, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.idinventarioenc, operador.IdOperador, 
                    '                        operador.nombres, trans_inv_ciclico.idinvciclico, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.user_agr, trans_inv_ciclico.EsPallet, 
                    '                        trans_inv_ciclico.lic_plate, trans_inv_ciclico.lote_stock, trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.fecha_vence_stock, 
                    '                        trans_inv_ciclico.peso_stock, trans_inv_ciclico.fec_agr, trans_inv_ciclico.cant_stock, trans_inv_ciclico.peso_reconteo, bodega_tramo.es_rack, 
                    '                        bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, producto_tipo.NombreTipoProducto,bodega_ubicacion.IdBodega
                    '                ORDER BY Tramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos"


                    'GT25072022_1530: Se cambia INNER por LEFT OUTER en producto_tipo y se añade IdBodega entre el inv y la bodega ubicación
                    'GT26072022_1500: Se agrega en where que cantidad sea =0 para incluir el producto en reconteo
                    Dim vSQL As String = "SELECT bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion AS Ubicacion, bodega_tramo.descripcion AS Tramo, 
                                dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS NOMBRE_COMPLETO,
                                trans_inv_ciclico.IdStock, producto.codigo AS Codigo, producto.nombre AS Producto, ISNULL(producto_presentacion.nombre, '') AS Presentacion, 
                                trans_inv_ciclico.lote, trans_inv_ciclico.lote_stock, producto_estado.nombre AS Estado, trans_inv_ciclico.cantidad AS Cantidad_Ciclico, 
                                trans_inv_ciclico.peso AS Peso_Ciclico, producto.IdPropietario, producto.IdClasificacion, producto.IdFamilia, producto_estado.IdEstado, 
                                trans_inv_ciclico.EsNuevo, bodega_tramo.IdTramo, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.idinventarioenc, operador.IdOperador, 
                                operador.nombres, trans_inv_ciclico.idinvciclico, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.user_agr, trans_inv_ciclico.EsPallet, 
                                trans_inv_ciclico.lic_plate, trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.peso_stock AS Peso_Stock, 
                                trans_inv_ciclico.fec_agr, trans_inv_ciclico.cant_stock AS Cantidad_Stock, trans_inv_ciclico.peso_reconteo, 
                                producto_tipo.NombreTipoProducto
                        FROM    trans_inv_ciclico 
						        INNER JOIN producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega 
								INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto 
								LEFT OUTER JOIN producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto 
								LEFT OUTER JOIN bodega_tramo 
								INNER JOIN bodega_ubicacion ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo AND 
								           bodega_tramo.IdBodega = bodega_ubicacion.IdBodega ON 
                                           trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion 
                                           AND trans_inv_ciclico.IdBodega = bodega_ubicacion.IdBodega
								LEFT OUTER JOIN operador ON trans_inv_ciclico.idoperador = operador.IdOperador 
								LEFT OUTER JOIN producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado 
								LEFT OUTER JOIN producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE   (trans_inv_ciclico.idinventarioenc = @idinventario)
                        and trans_inv_ciclico.cantidad = 0
                        GROUP BY bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion, bodega_tramo.descripcion, trans_inv_ciclico.IdStock, producto.codigo, 
                                producto.nombre, producto_presentacion.nombre, trans_inv_ciclico.lote, producto_estado.nombre, trans_inv_ciclico.cantidad, 
                                trans_inv_ciclico.peso, producto.IdPropietario, producto.IdClasificacion, producto.IdFamilia, producto_estado.IdEstado, 
                                trans_inv_ciclico.EsNuevo, bodega_tramo.IdTramo, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.idinventarioenc, operador.IdOperador, 
                                operador.nombres, trans_inv_ciclico.idinvciclico, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.user_agr, trans_inv_ciclico.EsPallet, 
                                trans_inv_ciclico.lic_plate, trans_inv_ciclico.lote_stock, trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.fecha_vence_stock, 
                                trans_inv_ciclico.peso_stock, trans_inv_ciclico.fec_agr, trans_inv_ciclico.cant_stock, trans_inv_ciclico.peso_reconteo, bodega_tramo.es_rack, 
                                bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, producto_tipo.NombreTipoProducto,bodega_ubicacion.IdBodega
                        ORDER BY Tramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_inv_ciclico

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_inv_ciclico()

                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.lic_plate = CType(lRow("lic_plate"), String)
                                End If

                                If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
                                    Obj.EsPallet = CType(lRow("EsPallet"), Boolean)
                                End If

                                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                    Obj.Ubicacion = CType(lRow("Ubicacion"), String)
                                End If

                                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                    Obj.User_agr = CType(lRow("user_agr"), String)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
                                    Obj.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                    Obj.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                                End If

                                If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
                                    Obj.Idoperador = CType(lRow("IdOperador"), Integer)
                                End If

                                If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
                                    Obj.Operador = CType(lRow("nombres"), String)
                                End If

                                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                    Obj.Tramo = CType(lRow("Tramo"), String)
                                End If

                                If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
                                    Obj.IdTramo = CType(lRow("IdTramo"), Integer)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("Codigo"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    Obj.Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("Lote"), String)
                                End If

                                If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                                    Obj.Lote_stock = CType(lRow("lote_stock"), String)
                                    Obj.Lote_stock = Obj.Lote_stock.Trim
                                End If

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("Estado"), String)
                                End If

                                If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                    Obj.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                                End If

                                If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                                    Obj.Peso_stock = CType(lRow("Peso_Stock"), Double)
                                End If

                                If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
                                End If

                                If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                                    Obj.Peso_stock = CType(lRow("peso_stock"), Double)
                                End If

                                If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("Peso_Ciclico"), Double)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                End If

                                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                                    Obj.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                                End If

                                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                                    Obj.IdFamilia = CType(lRow("IdFamilia"), Integer)
                                End If

                                If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                                    Obj.IdProductoEstado = CType(lRow("IdEstado"), Integer)
                                End If

                                If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
                                    Obj.EsNuevo = CType(lRow("EsNuevo"), Boolean)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                                    Obj.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                                End If

                                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                    Obj.Fec_agr = CType(lRow("fec_agr"), Date)
                                End If

                                Obj.Ubicacion = IIf(IsDBNull(lRow("NOMBRE_COMPLETO")), "", lRow("NOMBRE_COMPLETO"))

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

    'Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(ByVal pIdInventarioEnc As Integer,
    '                                                                               ByVal IdBodega As Integer,
    '                                                                               ByVal lConnection As SqlConnection,
    '                                                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

    '    Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar = Nothing

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

    '        Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
    '                                     dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
    '                                     dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
    '                                     dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
    '                                     dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
    '                                     dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
    '                                     trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              FROM dbo.trans_inv_ciclico INNER JOIN
    '                                     dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
    '                                     dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
    '                                     dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
    '                                     dbo.bodega_tramo INNER JOIN
    '                                     dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
    '                                     dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
    '                                     dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
    '                                     dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
    '                                     dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
    '                                     dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
    '                              WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
    '                              GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
    '                                     dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
    '                                     dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
    '                                     dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
    '                                     dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


    '        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '            lDTA.SelectCommand.CommandType = CommandType.Text
    '            lDTA.SelectCommand.Transaction = lTransaction
    '            lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
    '            lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

    '            Dim lDataTable As New DataTable
    '            lDTA.Fill(lDataTable)

    '            Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

    '            For Each lRow As DataRow In lDataTable.Rows

    '                BeTransInvCiclico = New clsBeTrans_inv_ciclico()

    '                BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

    '                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
    '                    BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
    '                End If

    '                If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
    '                    BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
    '                End If

    '                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
    '                End If

    '                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
    '                End If

    '                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
    '                End If

    '                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
    '                End If

    '                If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
    '                End If

    '                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
    '                End If

    '                If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
    '                    BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
    '                End If

    '                If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
    '                    BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
    '                End If

    '                If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
    '                    BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
    '                End If

    '                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
    '                    BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
    '                End If

    '                If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
    '                    BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
    '                End If

    '                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
    '                    BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
    '                End If

    '                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
    '                    BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
    '                End If

    '                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
    '                End If

    '                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
    '                End If

    '                If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
    '                    BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
    '                End If

    '                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
    '                    BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
    '                End If

    '                If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
    '                End If

    '                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
    '                    BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
    '                End If

    '                If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
    '                End If

    '                If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
    '                End If

    '                If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
    '                End If

    '                If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
    '                End If

    '                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
    '                    BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
    '                End If

    '                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
    '                End If

    '                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
    '                End If

    '                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
    '                    BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
    '                End If

    '                If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
    '                End If

    '                If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
    '                    BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
    '                End If

    '                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
    '                End If

    '                If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
    '                End If

    '                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
    '                End If

    '                BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
    '                BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
    '                BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
    '                BeTransInvCiclico.Operador = IIf(IsDBNull(lRow("nombres")), "", lRow("nombres"))

    '                lReturnList.Add(BeTransInvCiclico)

    '            Next

    '            Return lReturnList

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Actualizar_Fecha_Vencimiento(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Add("fecha_vence_stock", "@fecha_vence", DataType.Parametro)
            Upd.Where("idinventarioenc = @idinventario" &
                " AND IdStock = @IdStock")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))


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

    'Public Shared Function Actualizar_Operador(ByRef oBeTrans_inv_stock As clsBeTrans_inv_stock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


    '    Try

    '        Upd.Init("trans_inv_ciclico")
    '        Upd.Add("fecha_vence_stock", "@fecha_vence", DataType.Parametro)
    '        Upd.Where("idinventarioenc = @idinventario" &
    '            " AND IdStock = @IdStock")

    '        Dim sp As String = Upd.SQL()

    '        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
    '        Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

    '        If Es_Transaccion_Remota Then
    '            cmd = New SqlCommand(sp, pConection)
    '            cmd.Transaction = pTransaction
    '        Else
    '           cmd = New SqlCommand(sp, lConnection, lTransaction)

    '            lConnection.Open()
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@IDINVENTARIO", oBeTrans_inv_stock.Idinventario))
    '        cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
    '        cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))

    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        cmd.Dispose()

    '        Return rowsAffected

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        lConnection.Dispose()
    '    End Try

    'End Function

    Public Shared Function Get_All_By_IdStock_And_IdUbicacion(ByVal pIdInventario As Integer,
                                                ByVal pIdStock As Integer,
                                                ByVal pIdUbicacion As Integer,
                                                ByVal pIdProductoBodega As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_inv_ciclico
                WHERE    (trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdStock = @IdStock AND trans_inv_ciclico.IdUbicacion = @IdUbicacion AND IdProductoBodega=@IdProductoBodega)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()
                            Cargar(Obj, lRow)
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

    Public Shared Function Get_By_IdStock_And_IdProductoBodega(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico
                                  Where(IdStock = @IdStock AND 
                                        idinventarioenc=@idinventarioenc AND 
                                        IdProductoBodega=@IdProductoBodega AND 
                                        IdInvCiclico=@IdInvCiclico)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            ElseIf dt.Rows.Count > 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Inventario(ByVal IdInventarioEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Trans_Inv_Conteo WHERE idinventarioenc = @idinventarioenc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", IdInventarioEnc)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_InventarioByOperador(ByVal IdInventarioEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Inv_Conteo_Operador WHERE idinventarioenc = @idinventarioenc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", IdInventarioEnc)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByInventarioRegulariza(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT  producto.codigo, producto.nombre AS Producto, producto_estado.nombre AS Estado_Producto, producto_presentacion.nombre AS Presentacion, 
                                trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, SUM(trans_inv_ciclico.peso) AS Peso_Conteo, trans_inv_ciclico.IdStock, 
                                trans_inv_ciclico.cant_stock AS Cantidad_Stock, SUM(trans_inv_ciclico.cant_reconteo) AS Cantidad_Reconteo, trans_inv_ciclico.idinventarioenc, 
                                trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdUbicacion, 
                                SUM(trans_inv_ciclico.peso_reconteo) AS Peso_Reconteo, trans_inv_ciclico.peso_stock AS Peso_Stock, SUM(trans_inv_ciclico.cantidad) AS Cantidad_Conteo,
                                unidad_medida.Nombre AS UnidadMedida, unidad_medida.IdUnidadMedida
                        FROM    producto_estado INNER JOIN
                                trans_inv_ciclico INNER JOIN
                                producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                                producto ON producto.IdProducto = producto_bodega.IdProducto ON producto_estado.IdEstado = trans_inv_ciclico.IdProductoEstado INNER JOIN
                                unidad_medida ON producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                                producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE trans_inv_ciclico.idinventarioenc=@idinventario
                       GROUP BY producto.codigo, producto.nombre, producto_estado.nombre, producto_presentacion.nombre, trans_inv_ciclico.lote, 
                                trans_inv_ciclico.fecha_vence, trans_inv_ciclico.IdStock, trans_inv_ciclico.cant_stock, trans_inv_ciclico.cant_reconteo, 
                                trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, trans_inv_ciclico.IdProductoEstado, trans_inv_ciclico.IdPresentacion, 
                                trans_inv_ciclico.IdUbicacion, trans_inv_ciclico.peso_stock, trans_inv_ciclico.peso_reconteo, unidad_medida.Nombre, 
                                unidad_medida.IdUnidadMedida"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                Dim Ubic As New clsBeBodega_ubicacion
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                Ubic.IdUbicacion = Obj.IdUbicacion
                                clsLnBodega_ubicacion.GetSingle(Ubic)
                                Obj.Ubicacion = Ubic.NombreCompleto
                            End If

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("codigo"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                Obj.Lote = CType(lRow("lote"), String)
                            End If

                            If lRow("Estado_Producto") IsNot DBNull.Value AndAlso lRow("Estado_Producto") IsNot Nothing Then
                                Obj.Estado = CType(lRow("Estado_Producto"), String)
                            End If

                            If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                Obj.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                            End If

                            If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                                Obj.Peso_stock = CType(lRow("Peso_Stock"), Double)
                            End If

                            If lRow("Cantidad_Conteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Conteo") IsNot Nothing Then
                                Obj.Cantidad = CType(lRow("Cantidad_Conteo"), Double)
                            End If

                            If lRow("Peso_Conteo") IsNot DBNull.Value AndAlso lRow("Peso_Conteo") IsNot Nothing Then
                                Obj.Peso = CType(lRow("Peso_Conteo"), Double)
                            End If

                            If lRow("Cantidad_Reconteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Reconteo") IsNot Nothing Then
                                Obj.Cant_reconteo = CType(lRow("Cantidad_Reconteo"), Double)
                            End If

                            If lRow("Peso_Reconteo") IsNot DBNull.Value AndAlso lRow("Peso_Reconteo") IsNot Nothing Then
                                Obj.Peso_reconteo = CType(lRow("Peso_Reconteo"), Double)
                            End If

                            If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                Obj.IdStock = CType(lRow("IdStock"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                                Obj.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                            End If

                            If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                Obj.UnidadMedida = CType(lRow("UnidadMedida"), String)
                            End If

                            If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                            End If

                            If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                Obj.Fecha_vence = CType(lRow("fecha_vence"), Date)
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

    'Public Shared Function GetAllByInventarioReporte(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            Dim vSQL As String = "SELECT T.IdProductoBodega,T.Cantidad_Stock AS Cantidad_STock,T.Cantidad_Conteo As Cantidad_Conteo,T.Cantidad_Reconteo as Cantidad_Reconteo,T.Peso_Stock as Peso_Stock,T.Peso_Conteo As Peso_Conteo,T.Peso_Reconteo As Peso_Reconteo,
    '                    T.IdInventarioEnc,T.IdProducto,T.IdUnidadMedidaBasica,T.Codigo,T.Producto
    '                    from (SELECT  trans_inv_ciclico.IdProductoBodega, 
    '                    0 As Cantidad_Stock,SUM(trans_inv_ciclico.cantidad) AS Cantidad_Conteo, SUM(trans_inv_ciclico.cant_reconteo) 
    '                    AS Cantidad_Reconteo,0 As Peso_Stock,SUM(trans_inv_ciclico.peso) AS Peso_Conteo,SUM(trans_inv_ciclico.peso_reconteo) AS Peso_Reconteo, 
    '                    trans_inv_ciclico.idinventarioenc AS IdInventarioEnc, producto.IdProducto, producto.IdUnidadMedidaBasica, producto.codigo AS Codigo, 
    '                    producto.nombre AS Producto
    '                    FROM  trans_inv_ciclico INNER JOIN
    '                    producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
    '                    producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
    '                    producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado LEFT OUTER JOIN
    '                    producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
    '                    WHERE trans_inv_ciclico.idinventarioenc = @idinventario
    '                    GROUP BY  trans_inv_ciclico.IdProductoBodega,
    '                    trans_inv_ciclico.idinventarioenc, producto.IdProducto, producto.IdUnidadMedidaBasica,producto.codigo, producto.nombre
    '                    UNION
    '                   SELECT  dbo.trans_inv_stock.IdProductoBodega, dbo.trans_inv_stock.cantidad AS Cantidad_Stock, 0 AS Cantidad_Conteo, 0 AS Cantidad_Reconteo, 
    '                                               dbo.trans_inv_stock.peso AS Peso_Stock, 0 AS Peso_Conteo, 0 AS Peso_Reconteo, dbo.trans_inv_stock.idinventario AS IdInventarioEnc, 
    '                                               producto_1.IdProducto, producto_1.IdUnidadMedidaBasica, producto_1.codigo AS Codigo, producto_1.nombre AS Producto
    '                      FROM dbo.producto_bodega AS producto_bodega_1 INNER JOIN
    '                                               dbo.producto AS producto_1 ON producto_bodega_1.IdProducto = producto_1.IdProducto INNER JOIN
    '                                               dbo.trans_inv_stock ON producto_bodega_1.IdProductoBodega = dbo.trans_inv_stock.IdProductoBodega LEFT OUTER JOIN
    '                                               dbo.producto_estado AS producto_estado_1 ON dbo.trans_inv_stock.IdProductoEstado = producto_estado_1.IdEstado LEFT OUTER JOIN
    '                                               dbo.producto_presentacion AS producto_presentacion_1 ON dbo.trans_inv_stock.IdPresentacion = producto_presentacion_1.IdPresentacion
    '                      WHERE (dbo.trans_inv_stock.idinventario = @idinventario and trans_inv_stock.IdProductoBodega=producto_bodega_1.IdProductoBodega and trans_inv_stock.IdUbicacion in(select idubicacion from trans_inv_ciclico_ubic where idinventarioenc=@idinventario))
    '                      GROUP BY dbo.trans_inv_stock.IdProductoBodega, producto_1.IdProducto, producto_1.IdUnidadMedidaBasica, producto_1.codigo, producto_1.nombre, 
    '                                               dbo.trans_inv_stock.idinventario, dbo.trans_inv_stock.cantidad, dbo.trans_inv_stock.peso
    '                    ) As T
    '                    GROUP BY T.IdProductoBodega,
    '                    T.IdInventarioEnc,T.IdProducto,T.IdUnidadMedidaBasica,T.Codigo,T.Producto,T.Cantidad_Stock,T.Cantidad_Conteo,T.Cantidad_Reconteo,T.Peso_Stock,T.Peso_Conteo,T.Peso_Reconteo"

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text
    '                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

    '                Dim lDataTable As New DataTable
    '                lDTA.Fill(lDataTable)

    '                Dim Obj As clsBeTrans_inv_ciclico

    '                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

    '                    For Each lRow As DataRow In lDataTable.Rows

    '                        Obj = New clsBeTrans_inv_ciclico()

    '                        If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
    '                            Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
    '                        End If

    '                        If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
    '                            Obj.IdProducto = CType(lRow("IdProducto"), Integer)
    '                        End If

    '                        If lRow("IdInventarioEnc") IsNot DBNull.Value AndAlso lRow("IdInventarioEnc") IsNot Nothing Then
    '                            Obj.Idinventarioenc = CType(lRow("IdInventarioEnc"), Integer)
    '                        End If

    '                        If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
    '                            Obj.Producto = CType(lRow("Producto"), String)
    '                        End If

    '                        If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
    '                            Obj.Codigo = CType(lRow("Codigo"), String)
    '                        End If

    '                        If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
    '                            Obj.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
    '                        End If

    '                        If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
    '                            Obj.Peso_stock = CType(lRow("Peso_Stock"), Double)
    '                        End If

    '                        If lRow("Cantidad_Conteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Conteo") IsNot Nothing Then
    '                            Obj.Cantidad = CType(lRow("Cantidad_Conteo"), Double)
    '                        End If

    '                        If lRow("Peso_Conteo") IsNot DBNull.Value AndAlso lRow("Peso_Conteo") IsNot Nothing Then
    '                            Obj.Peso = CType(lRow("Peso_Conteo"), Double)
    '                        End If

    '                        If lRow("Cantidad_Reconteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Reconteo") IsNot Nothing Then
    '                            Obj.Cant_reconteo = CType(lRow("Cantidad_Reconteo"), Double)
    '                        End If

    '                        If lRow("Peso_Reconteo") IsNot DBNull.Value AndAlso lRow("Peso_Reconteo") IsNot Nothing Then
    '                            Obj.Peso_reconteo = CType(lRow("Peso_Reconteo"), Double)
    '                        End If

    '                        If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
    '                            Obj.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
    '                        End If

    '                        lReturnList.Add(Obj)

    '                    Next

    '                End If

    '            End Using

    '        End Using

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function GetAllByInventarioReporte(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'Dim vSQL As String = "SELECT dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdProductoBodega, SUM(dbo.trans_inv_ciclico.cant_stock) AS cantidad_stock, 
                '         SUM(dbo.trans_inv_ciclico.cantidad) AS cantidad_conteo, SUM(dbo.trans_inv_ciclico.cant_reconteo) AS cantidad_reconteo, SUM(dbo.trans_inv_ciclico.peso_stock) 
                '         AS peso_stock, SUM(dbo.trans_inv_ciclico.peso) AS peso_conteo, SUM(dbo.trans_inv_ciclico.peso_reconteo) AS peso_reconteo, dbo.producto.codigo AS Codigo, 
                '         dbo.producto.nombre AS Producto,MAX(trans_inv_ciclico.fec_agr) as FechaConteo
                '    FROM dbo.trans_inv_ciclico INNER JOIN
                '         dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND 
                '         dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
                '         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND 
                '         dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                '         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
                '  WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario)
                'GROUP BY dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdProductoBodega, dbo.producto.codigo, dbo.producto.nombre"

                Dim vSQL As String = "SELECT dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.cant_stock AS cantidad_stock, 
                         dbo.trans_inv_ciclico.cantidad AS cantidad_conteo, dbo.trans_inv_ciclico.cant_reconteo AS cantidad_reconteo, dbo.trans_inv_ciclico.peso_stock 
                         AS peso_stock, dbo.trans_inv_ciclico.peso AS peso_conteo, dbo.trans_inv_ciclico.peso_reconteo AS peso_reconteo, dbo.producto.codigo AS Codigo, 
                         dbo.producto.nombre AS Producto,trans_inv_ciclico.lote,trans_inv_ciclico.fecha_vence
                    FROM dbo.trans_inv_ciclico INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega AND 
                         dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
                  WHERE (dbo.trans_inv_ciclico.idinventarioenc =  @idinventario)"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("cantidad_stock") IsNot DBNull.Value AndAlso lRow("cantidad_stock") IsNot Nothing Then
                                Obj.Cant_stock = CType(lRow("cantidad_stock"), Double)
                            End If

                            If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                                Obj.Peso_stock = CType(lRow("peso_stock"), Double)
                            End If

                            If lRow("cantidad_conteo") IsNot DBNull.Value AndAlso lRow("cantidad_conteo") IsNot Nothing Then
                                Obj.Cantidad = CType(lRow("cantidad_conteo"), Double)
                            End If

                            If lRow("peso_conteo") IsNot DBNull.Value AndAlso lRow("peso_conteo") IsNot Nothing Then
                                Obj.Peso = CType(lRow("peso_conteo"), Double)
                            End If

                            If lRow("cantidad_reconteo") IsNot DBNull.Value AndAlso lRow("cantidad_reconteo") IsNot Nothing Then
                                Obj.Cant_reconteo = CType(lRow("cantidad_reconteo"), Double)
                            End If

                            If lRow("peso_reconteo") IsNot DBNull.Value AndAlso lRow("peso_reconteo") IsNot Nothing Then
                                Obj.Peso_reconteo = CType(lRow("peso_reconteo"), Double)
                            End If

                            If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                Obj.Lote = CType(lRow("lote"), String)
                            End If

                            If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                Obj.Fecha_vence = CType(lRow("fecha_vence"), Date)
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

    Public Shared Function GetAllByInventarioReporteConMovs(ByVal pIdInventario As Integer, ByVal Fecha As Date) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT  trans_inv_ciclico.IdProductoBodega, 
                        0 As Cantidad_Stock,trans_inv_ciclico.cantidad AS Cantidad_Conteo, trans_inv_ciclico.cant_reconteo 
                        AS Cantidad_Reconteo,0 As Peso_Stock, trans_inv_ciclico.peso AS Peso_Conteo, trans_inv_ciclico.peso_reconteo AS Peso_Reconteo, 
                        trans_inv_ciclico.idinventarioenc AS IdInventarioEnc, producto.IdProducto, producto.IdUnidadMedidaBasica, producto.codigo AS Codigo, 
                        producto.nombre AS Producto,
                        (SELECT Sum(Cantidad) AS Expr1
                        FROM  trans_movimientos
                        WHERE  DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(Fecha) + ",trans_movimientos.fecha_agr)>0 
                        AND trans_movimientos.IdProductoBodega = trans_inv_ciclico.IdProductoBodega 
                        AND trans_movimientos.IdTipoTarea = 1) AS Recepciones,
                        (SELECT  Sum(Cantidad) AS Expr1
                        FROM  trans_movimientos AS trans_movimientos_1
                        WHERE DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(Fecha) + ",trans_movimientos_1.fecha_agr)>0 
                        AND trans_movimientos_1.IdProductoBodega = trans_inv_ciclico.IdProductoBodega 
                        AND trans_movimientos_1.IdTipoTarea = 5) AS Despachos
                        FROM  trans_inv_ciclico INNER JOIN
                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
                        producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE trans_inv_ciclico.idinventarioenc = @idinventario
                        GROUP BY  trans_inv_ciclico.IdProductoBodega,
                        trans_inv_ciclico.idinventarioenc, producto.IdProducto, producto.IdUnidadMedidaBasica,producto.codigo, producto.nombre,trans_inv_ciclico.cantidad,
                        trans_inv_ciclico.cant_reconteo,trans_inv_ciclico.peso,trans_inv_ciclico.peso_reconteo
                        UNION
                        SELECT trans_inv_stock.IdProductoBodega,
                        trans_inv_stock.cantidad AS Cantidad_Stock,0 As Cantidad_Conteo,0 As Cantidad_Reconteo, trans_inv_stock.peso AS Peso_Stock, 
                        0 As Peso_Conteo, 0 As Peso_Reconteo,trans_inv_stock.idinventario as IdInventarioEnc, producto.IdProducto, producto.IdUnidadMedidaBasica, producto.codigo AS Codigo, 
                        producto.nombre AS Producto,
                        (SELECT Sum(Cantidad) AS Expr1
                        FROM  trans_movimientos
                        WHERE DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(Fecha) + ",trans_movimientos.fecha_agr)>0 
                        AND trans_movimientos.IdProductoBodega = trans_inv_stock.IdProductoBodega 
                        AND trans_movimientos.IdTipoTarea = 1 ) AS Recepciones,
                        (SELECT  Sum(Cantidad) AS Expr1
                        FROM  trans_movimientos AS trans_movimientos_1
                        WHERE DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(Fecha) + ",trans_movimientos_1.fecha_agr)>0 
                        AND trans_movimientos_1.IdProductoBodega = trans_inv_stock.IdProductoBodega 
                        AND trans_movimientos_1.IdTipoTarea = 5) AS Despachos
                        FROM  producto_bodega INNER JOIN
                        producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                        trans_inv_stock ON producto_bodega.IdProductoBodega = trans_inv_stock.IdProductoBodega LEFT OUTER JOIN
                        producto_estado ON trans_inv_stock.IdProductoEstado = producto_estado.IdEstado LEFT OUTER JOIN
                        producto_presentacion ON trans_inv_stock.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE trans_inv_stock.idinventario = @idinventario and trans_inv_stock.IdProductoBodega=producto_bodega.IdProductoBodega and trans_inv_stock.IdUbicacion in(select idubicacion from trans_inv_ciclico_ubic where idinventarioenc=@idinventario)
                        GROUP BY  trans_inv_stock.IdProductoBodega,  
                        producto.IdProducto, producto.IdUnidadMedidaBasica,
                        producto.codigo, producto.nombre, trans_inv_stock.idinventario,trans_inv_stock.cantidad,trans_inv_stock.peso"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("IdInventarioEnc") IsNot DBNull.Value AndAlso lRow("IdInventarioEnc") IsNot Nothing Then
                                Obj.Idinventarioenc = CType(lRow("IdInventarioEnc"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                Obj.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                            End If

                            If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                                Obj.Peso_stock = CType(lRow("Peso_Stock"), Double)
                            End If

                            If lRow("Cantidad_Conteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Conteo") IsNot Nothing Then
                                Obj.Cantidad = CType(lRow("Cantidad_Conteo"), Double)
                            End If

                            If lRow("Peso_Conteo") IsNot DBNull.Value AndAlso lRow("Peso_Conteo") IsNot Nothing Then
                                Obj.Peso = CType(lRow("Peso_Conteo"), Double)
                            End If

                            If lRow("Cantidad_Reconteo") IsNot DBNull.Value AndAlso lRow("Cantidad_Reconteo") IsNot Nothing Then
                                Obj.Cant_reconteo = CType(lRow("Cantidad_Reconteo"), Double)
                            End If

                            If lRow("Peso_Reconteo") IsNot DBNull.Value AndAlso lRow("Peso_Reconteo") IsNot Nothing Then
                                Obj.Peso_reconteo = CType(lRow("Peso_Reconteo"), Double)
                            End If

                            If lRow("IdUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("IdUnidadMedidaBasica") IsNot Nothing Then
                                Obj.IdUnidadMedida = CType(lRow("IdUnidadMedidaBasica"), Integer)
                            End If

                            If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                            End If

                            If lRow("Recepciones") IsNot DBNull.Value AndAlso lRow("Recepciones") IsNot Nothing Then
                                Obj.Recepciones = CType(lRow("Recepciones"), Double)
                            End If

                            If lRow("Despachos") IsNot DBNull.Value AndAlso lRow("Despachos") IsNot Nothing Then
                                Obj.Despachos = CType(lRow("Despachos"), Double)
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

    Public Shared Function Get_All_By_IdInventarioEnc(pIdInvEnc As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM Trans_inv_ciclico WHERE idinventarioenc=@idinventarioenc Order by IdProductoBodega"

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInvEnc)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    Dim vBeTrans_inv_detalle As New clsBeTrans_inv_ciclico

                    For Each dr As DataRow In lDataTable.Rows
                        vBeTrans_inv_detalle = New clsBeTrans_inv_ciclico
                        Cargar(vBeTrans_inv_detalle, dr)
                        lReturnList.Add(vBeTrans_inv_detalle)
                    Next

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Regularizar_Inventario(ByVal BeTransInvEnc As clsBeTrans_inv_enc,
                                             ByVal ListBeStockNuevo As IList(Of clsBeStock),
                                             ByVal ListBeMovimientos As IList(Of clsBeTrans_movimientos),
                                             ByVal Usuario As clsBeUsuario,
                                             ByVal ListBeAjusteDet As List(Of clsBeTrans_ajuste_det),
                                             ByRef lOperaciones As List(Of KeyValuePair(Of Integer, Integer)),
                                             ByVal pCodigoBodegaERP As String,
                                             ByVal pTallaColor As Boolean,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction)

        Dim IdStock As Integer
        Dim ListStock As New List(Of clsBeStock)
        Dim IdAjusteDet As Integer
        Dim objStockHist As New clsBeStock_hist()
        Dim vCantidadHist As Integer = 0
        Dim lIdStocksAEliminar As New List(Of Integer)
        Dim vSesionTrace As String = Date.Now.ToString("yyyyMMddHHmmssfff") & "-" & Guid.NewGuid().ToString("N").Substring(0, 8)
        Dim vInicioTrace As DateTime = Date.Now
        Dim vPasoTrace As DateTime = vInicioTrace
        Dim vMsGetStock As Long = 0
        Dim vMsExisteConteo As Long = 0
        Dim vMsInsertStock As Long = 0
        Dim vMsStockHist As Long = 0
        Dim vMsEliminarStock As Long = 0
        Dim vMsProcesarAjuste As Long = 0
        Dim vMsEliminarPendiente As Long = 0
        Dim vCntStockNuevo As Integer = 0
        Dim vCntAjustes As Integer = 0
        Dim vCntEliminarPendiente As Integer = 0

        Try
            InvRegularizacionTrace(vSesionTrace, "DAL_REG_START", vInicioTrace,
                                   "IdInventario=" & BeTransInvEnc.Idinventarioenc &
                                   ";Stocks=" & If(ListBeStockNuevo Is Nothing, 0, ListBeStockNuevo.Count) &
                                   ";Movs=" & If(ListBeMovimientos Is Nothing, 0, ListBeMovimientos.Count) &
                                   ";Ajustes=" & If(ListBeAjusteDet Is Nothing, 0, ListBeAjusteDet.Count))

            Dim vIdPropietarioBodega As Integer = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(BeTransInvEnc.Idpropietario,
                                                                                                                                BeTransInvEnc.IdBodega,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)


            BeTransInvEnc.Regularizado = True
            BeTransInvEnc.Estado = "Finalizado"
            BeTransInvEnc.Hora_fin = Now
            BeTransInvEnc.Activo = True

            clsLnTrans_inv_enc.Actualizar(BeTransInvEnc, lConnection, lTransaction)

            Dim vCantInv As Double = 0

            IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.

            BeTransInvEnc.IdStock = IdStock

            For Each pBeStock As clsBeStock In ListBeStockNuevo.OrderBy(Function(x) x.IdProductoBodega)
                vCntStockNuevo += 1

                vPasoTrace = Date.Now
                ListStock = Get_Existente_By_IdStock(pBeStock.IdStock, lConnection, lTransaction)
                vMsGetStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                If pBeStock.Cantidad = 0 Then
                    Debug.Write("Pausa")
                End If

                vCantInv = pBeStock.Cantidad

                If ListStock.Count > 0 Then

                    For Each BeStock As clsBeStock In ListStock

                        BeStock.ProductoEstado.IdEstado = pBeStock.IdProductoEstado
                        BeStock.IdProductoEstado = pBeStock.IdProductoEstado
                        BeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                        BeStock.Lote = pBeStock.Lote
                        BeStock.Fecha_vence = pBeStock.Fecha_vence
                        BeStock.IdUbicacion = pBeStock.IdUbicacion
                        BeStock.IdProductoTallaColor = pBeStock.IdProductoTallaColor

                        clsPublic.CopyObject(BeStock, pBeStock)

                        '#EJC20180830: Después del copy se restablece la cantidad con ajuste
                        'que se capturó en la variable vCantInv antes de perder el valor original
                        pBeStock.Cantidad = vCantInv

                        pBeStock.IdStock = BeStock.IdStock
                        vCantidadHist = BeStock.Cantidad

                        If pBeStock.Cantidad < 0 Then

                            If BeStock.Cantidad >= Math.Abs(vCantInv) Then
                                pBeStock.Cantidad = BeStock.Cantidad + pBeStock.Cantidad
                            Else
                                pBeStock.Cantidad = Math.Abs(pBeStock.Cantidad) - pBeStock.Cantidad
                            End If

                        ElseIf pBeStock.Cantidad > 0 Then

                            '#EJC20180830_0723PM: Se modificó suma por cambio en procedimiento en caliente de último inventario en BYB.
                            'pBeStock.Cantidad = BeStock.Cantidad + pBeStock.Cantidad
                            'EJC202408270855: Christian se mudó a la casa de Carolina. (todavía, todavía) risas.
                            pBeStock.Cantidad = pBeStock.Cantidad

                        End If

                        If pBeStock.Cantidad = 0 Then
                            '#EJC20180625: Mantener copia del stock original
                            clsPublic.CopyObject(pBeStock, objStockHist)
                            '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                        
                            vPasoTrace = Date.Now
                            objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                            objStockHist.IdNuevoStock = BeStock.IdStock
                            objStockHist.IdPedidoEnc = BeStock.IdPedidoEnc
                            objStockHist.IdPickingEnc = BeStock.IdPickingEnc
                            objStockHist.IdUbicacion_anterior = BeStock.IdUbicacion
                            objStockHist.IdUbicacion = BeStock.IdUbicacion
                            objStockHist.IdDespachoEnc = 0
                            objStockHist.Fec_agr = Now
                            objStockHist.Fec_mod = Now
                            objStockHist.Cantidad = vCantidadHist
                            objStockHist.IdProductoTallaColor = BeStock.IdProductoTallaColor
                            clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                            vMsStockHist += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                            vPasoTrace = Date.Now
                            clsLnStock.Eliminar(BeStock, lConnection, lTransaction)
                            vMsEliminarStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                        Else

                            '#CKFK20250130 Agregué esto para que cuando se cree un nuevo Stock se elimine el original,
                            'si no tiene conteos
                            vPasoTrace = Date.Now
                            If Not Existe_Conteo_By_IdStock_And_IdInvEnc_Mayor_0(pBeStock.IdStock,
                                                                                 BeTransInvEnc.Idinventarioenc,
                                                                                 lConnection,
                                                                                 lTransaction) Then
                                lIdStocksAEliminar.Add(BeStock.IdStock)
                            End If
                            vMsExisteConteo += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                            pBeStock.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                            pBeStock.Cantidad = Math.Abs(vCantInv)
                            pBeStock.ProductoEstado.IdEstado = pBeStock.IdProductoEstado
                            pBeStock.IdProductoEstado = pBeStock.IdProductoEstado
                            pBeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                            pBeStock.Lote = pBeStock.Lote
                            pBeStock.Fecha_vence = pBeStock.Fecha_vence
                            pBeStock.IdUbicacion = pBeStock.IdUbicacion
                            vPasoTrace = Date.Now
                            clsLnStock.Insertar(pBeStock, lConnection, lTransaction)
                            vMsInsertStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                            IdStock += 1

                        End If

                    Next

                Else
                    '#EJC20181212: 1621 de Erik para Erik :} debemos mejorar el análisis, si no se encuentra el IdStock (lista.count =0) y la cantidad es negativa.
                    'Quiere decir que ese producto no se contó en esa ubicación y que probablemente ya no exista allí, por lo que no se debe insertar.
                    If vCantInv > 0 Then

                        pBeStock.IdStock = IdStock
                        pBeStock.Cantidad = Math.Abs(vCantInv)
                        pBeStock.ProductoEstado.IdEstado = pBeStock.IdProductoEstado
                        pBeStock.IdProductoEstado = pBeStock.IdProductoEstado
                        pBeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                        pBeStock.Lote = pBeStock.Lote
                        pBeStock.Fecha_vence = pBeStock.Fecha_vence
                        pBeStock.IdUbicacion = pBeStock.IdUbicacion
                        vPasoTrace = Date.Now
                        clsLnStock.Insertar(pBeStock, lConnection, lTransaction)
                        vMsInsertStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                        IdStock += 1

                    Else

                        '#EJC20180625: Mantener copia del stock original
                        clsPublic.CopyObject(pBeStock, objStockHist)
                        '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                        
                        vPasoTrace = Date.Now
                        objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                        objStockHist.IdNuevoStock = 0
                        objStockHist.IdUbicacion_anterior = pBeStock.IdUbicacion
                        objStockHist.IdUbicacion = pBeStock.IdUbicacion
                        objStockHist.IdDespachoEnc = 0
                        objStockHist.Fec_agr = Now
                        objStockHist.Fec_mod = Now
                        objStockHist.Cantidad = vCantidadHist
                        objStockHist.IdProductoTallaColor = pBeStock.IdProductoTallaColor
                        clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                        vMsStockHist += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                        vPasoTrace = Date.Now
                        clsLnStock.Eliminar(pBeStock, lConnection, lTransaction)
                        vMsEliminarStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                    End If

                End If

                If vCntStockNuevo Mod 500 = 0 Then
                    InvRegularizacionTrace(vSesionTrace, "DAL_REG_STOCK_PROGRESS", vInicioTrace,
                                           "Procesados=" & vCntStockNuevo &
                                           ";MsGetStock=" & vMsGetStock &
                                           ";MsExisteConteo=" & vMsExisteConteo &
                                           ";MsInsertStock=" & vMsInsertStock &
                                           ";MsStockHist=" & vMsStockHist &
                                           ";MsEliminarStock=" & vMsEliminarStock &
                                           ";PendEliminar=" & lIdStocksAEliminar.Count)
                End If

            Next

            InvRegularizacionTrace(vSesionTrace, "DAL_REG_STOCK_END", vInicioTrace,
                                   "Procesados=" & vCntStockNuevo &
                                   ";MsGetStock=" & vMsGetStock &
                                   ";MsExisteConteo=" & vMsExisteConteo &
                                   ";MsInsertStock=" & vMsInsertStock &
                                   ";MsStockHist=" & vMsStockHist &
                                   ";MsEliminarStock=" & vMsEliminarStock &
                                   ";PendEliminar=" & lIdStocksAEliminar.Count)

            IdAjusteDet = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction) + 1

            For Each BeAjusteDet As clsBeTrans_ajuste_det In ListBeAjusteDet
                vCntAjustes += 1

                vPasoTrace = Date.Now
                ListStock = Get_Existente_By_IdStock(BeAjusteDet.IdStock, lConnection, lTransaction)
                vMsGetStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                If ListStock.Count > 0 Then
                    vPasoTrace = Date.Now
                    clsLnStock.Procesar_Ajuste(BeAjusteDet,
                                               Usuario,
                                               lConnection,
                                               lTransaction)
                    vMsProcesarAjuste += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                End If

                If vCntAjustes Mod 250 = 0 Then
                    InvRegularizacionTrace(vSesionTrace, "DAL_REG_AJUSTE_PROGRESS", vInicioTrace,
                                           "Procesados=" & vCntAjustes &
                                           ";MsGetStock=" & vMsGetStock &
                                           ";MsProcesarAjuste=" & vMsProcesarAjuste)
                End If

            Next

            InvRegularizacionTrace(vSesionTrace, "DAL_REG_AJUSTE_END", vInicioTrace,
                                   "Procesados=" & vCntAjustes &
                                   ";MsGetStock=" & vMsGetStock &
                                   ";MsProcesarAjuste=" & vMsProcesarAjuste)

            For Each IdStock In lIdStocksAEliminar
                vCntEliminarPendiente += 1

                vPasoTrace = Date.Now
                ListStock = Get_Existente_By_IdStock(IdStock, lConnection, lTransaction)
                vMsGetStock += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                If ListStock.Count > 0 Then
                    vPasoTrace = Date.Now
                    clsLnStock.Eliminar_By_IdStock(IdStock, lConnection, lTransaction)
                    vMsEliminarPendiente += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                End If

            Next

            InvRegularizacionTrace(vSesionTrace, "DAL_REG_ELIMINAR_PEND_END", vInicioTrace,
                                   "Procesados=" & vCntEliminarPendiente &
                                   ";MsGetStock=" & vMsGetStock &
                                   ";MsEliminarPendiente=" & vMsEliminarPendiente)

            clsLnTarea_hh.Actualiza_Estado_Tarea(BeTransInvEnc.Idinventarioenc,
                                                 6,
                                                 4,
                                                 lConnection,
                                                 lTransaction)

            Procesar_Ajustes_SAP(BeTransInvEnc,
                                 lConnection,
                                 lTransaction,
                                 vIdPropietarioBodega,
                                 Usuario,
                                 pCodigoBodegaERP,
                                 pTallaColor)

            InvRegularizacionTrace(vSesionTrace, "DAL_REG_FIN", vInicioTrace,
                                   "Stocks=" & vCntStockNuevo &
                                   ";Ajustes=" & vCntAjustes &
                                   ";EliminarPendiente=" & vCntEliminarPendiente &
                                   ";MsGetStock=" & vMsGetStock &
                                   ";MsExisteConteo=" & vMsExisteConteo &
                                   ";MsInsertStock=" & vMsInsertStock &
                                   ";MsStockHist=" & vMsStockHist &
                                   ";MsEliminarStock=" & vMsEliminarStock &
                                   ";MsProcesarAjuste=" & vMsProcesarAjuste &
                                   ";MsEliminarPendiente=" & vMsEliminarPendiente)

        Catch ex As Exception
            InvRegularizacionTrace(vSesionTrace, "DAL_REG_ERROR", vInicioTrace, ex.Message)
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Shared Function CrearStockHist(ByVal stock As clsBeStock, ByVal cantidadHist As Integer, ByVal conn As SqlConnection, ByVal tran As SqlTransaction) As clsBeStock_hist
        Dim hist As New clsBeStock_hist()
        clsPublic.CopyObject(stock, hist)
        hist.IdStockHist = clsLnStock_hist.MaxID(conn, tran) + 1
        hist.IdNuevoStock = If(stock.IdStock > 0, stock.IdStock, 0)
        hist.IdPedidoEnc = stock.IdPedidoEnc
        hist.IdPickingEnc = stock.IdPickingEnc
        hist.IdUbicacion_anterior = stock.IdUbicacion
        hist.IdUbicacion = stock.IdUbicacion
        hist.IdDespachoEnc = 0
        hist.Fec_agr = Now
        hist.Fec_mod = Now
        hist.Cantidad = cantidadHist
        clsLnStock_hist.Insertar(hist, conn, tran)
        Return hist
    End Function

    Private Shared Sub InsertarNuevoStock(ByRef stock As clsBeStock, ByVal cantidad As Integer, ByVal conn As SqlConnection, ByVal tran As SqlTransaction, ByVal idStock As Integer)
        stock.IdStock = idStock
        stock.Cantidad = cantidad
        stock.ProductoEstado.IdEstado = stock.IdProductoEstado
        stock.Presentacion.IdPresentacion = stock.IdPresentacion
        clsLnStock.Insertar(stock, conn, tran)
    End Sub

    Public Shared Sub Inserta_Encabezado_Ajuste(ByVal IdInventario As Integer,
                                                ByVal Usuario As clsBeUsuario,
                                                ByVal IdBodega As Integer,
                                                ByVal IdProductoFamilia As Integer,
                                                ByVal IdPropietario As Integer,
                                                ByRef pIdAjusteEnc As Integer,
                                                ByVal pAjuste_Por_Inventario As Integer,
                                                ByVal IdCentroCosto As Integer,
                                                Optional lConnection As SqlConnection = Nothing,
                                                Optional lTransaction As SqlTransaction = Nothing)

        Try

            Dim pBeTransAjustEnc As New clsBeTrans_ajuste_enc
            pBeTransAjustEnc.IdAjusteenc = clsLnTrans_ajuste_enc.MaxID() + 1
            pBeTransAjustEnc.Referencia = "Ajuste por inventario No. " & IdInventario
            pBeTransAjustEnc.Fecha = Date.Now
            pBeTransAjustEnc.Fec_agr = Date.Now
            pBeTransAjustEnc.Fec_mod = Date.Now
            pBeTransAjustEnc.Idusuario = Usuario.IdUsuario
            pBeTransAjustEnc.User_agr = Usuario.Nombres
            pBeTransAjustEnc.User_mod = Usuario.Nombres
            pBeTransAjustEnc.IdBodega = IdBodega
            pBeTransAjustEnc.IdProductoFamilia = IdProductoFamilia
            pBeTransAjustEnc.IdPropietarioBodega = IdPropietario

            '#EJC20240719: Por defecto dejar el ajuste como que ya se envió al ERP.
            pBeTransAjustEnc.Enviado_A_ERP = False
            pBeTransAjustEnc.Ajuste_Por_Inventario = pAjuste_Por_Inventario
            pBeTransAjustEnc.IdCentroCosto = IdCentroCosto
            clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc, lConnection, lTransaction)

            pIdAjusteEnc = pBeTransAjustEnc.IdAjusteenc

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_BuscaExistenteEnStock(pIdProductoBodega As Integer,
                                                     pIdUbicacion As Integer,
                                                     pIdPresentacion As Integer,
                                                     ByVal lote As String,
                                                     ByVal FechaVence As Date,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeStock)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeStock)

            If pIdPresentacion = 0 Then
                vSQL = "SELECT * FROM stock WHERE IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion and(lote=lote or lote is null) ORDER BY cantidad "
            Else
                vSQL = "SELECT * FROM stock WHERE IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion AND IdPresentacion=@IdPresentacion and (lote=lote or lote is null) and fecha_vence=@Fecha ORDER BY cantidad "
            End If

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@lote", lote)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@Fecha", FechaVence)
                lDataAdapter.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                Dim vBeTrans_inv_detalle As New clsBeStock

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_detalle = New clsBeStock
                    clsLnStock.Cargar(vBeTrans_inv_detalle, dr)
                    lReturnList.Add(vBeTrans_inv_detalle)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BuscaExistenteEnStockConLP(pIdProductoBodega As Integer,
                                                          pIdUbicacion As Integer,
                                                          lic_plate As String,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As List(Of clsBeStock)

        Try

            Dim lReturnList As New List(Of clsBeStock)

            Dim vSQL As String = "SELECT * FROM stock WHERE IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion AND lic_plate=@lic_plate"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@lic_plate", lic_plate)
                lDataAdapter.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                Dim vBeTrans_inv_detalle As New clsBeStock

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_detalle = New clsBeStock
                    clsLnStock.Cargar(vBeTrans_inv_detalle, dr)
                    lReturnList.Add(vBeTrans_inv_detalle)

                Next
            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener_ExistenciasEnStock(ByRef oBeStock As clsBeStock) As Boolean

        Dim vSQL As String = ""

        Try

            If oBeStock.IdPresentacion = 0 Then

                vSQL = "SELECT * FROM Stock" &
                    " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion)"

            ElseIf oBeStock.Lic_plate <> "" Then

                vSQL = "SELECT * FROM Stock" &
               " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion AND lic_plate=@lic_plate)"

            Else

                vSQL = "SELECT * FROM Stock" &
                " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacion=@IdUbicacion AND IdPresentacion=@IdPresentacion)"

            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeStock.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeStock.IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeStock.IdPresentacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeStock.Lic_plate))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return True
            ElseIf dt.Rows.Count > 1 Then
                Return True
            ElseIf dt.Rows.Count < 1 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener_ExistenciasEnMovimientos(ByRef oBeMovimientos As clsBeTrans_movimientos) As Boolean

        Dim vSQL As String = ""

        Try

            If oBeMovimientos.IdPresentacion = 0 Then

                vSQL = "SELECT * FROM Trans_movimientos" &
                    " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacionOrigen=@IdUbicacionOrigen)"

            ElseIf oBeMovimientos.Barra_pallet <> "" And oBeMovimientos.Barra_pallet <> 0 Then

                vSQL = "SELECT * FROM Trans_movimientos" &
               " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacionOrigen=@IdUbicacionOrigen AND barra_pallet=@barra_pallet)"

            Else
                vSQL = "SELECT * FROM Trans_movimientos" &
                " Where(IdProductoBodega=@IdProductoBodega AND IdUbicacionOrigen=@IdUbicacionOrigen AND IdPresentacion=@IdPresentacion)"

            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeMovimientos.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACIONORIGEN", oBeMovimientos.IdUbicacionOrigen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeMovimientos.IdPresentacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@BARRA_PALLET", oBeMovimientos.Barra_pallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return True
            ElseIf dt.Rows.Count > 1 Then
                Return True
            ElseIf dt.Rows.Count < 1 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20241202: Agregué transacción, lote, estado, ubicación.
    ''' </summary>
    ''' <param name="pIdInv"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer) As DataTable

        Get_All_By_Comparacion_Inventario = Nothing

        Try

            Dim vSQL As String = "SELECT codigo as Código,producto as Producto ,LoteOrigen as LoteOrigen, Lote, FechaVence,Licencia, 
                                        EstadoOrigen, EstadoDestino, UbicacionOrigen, UbicacionDestino,
                                        Cantidad_Stock as CantidadStock,
                                        Peso_Stock as PesoStock,Cantidad as CantidadConteo,Peso as PesoConteo,Entradas,Salidas,Entradas_Salidas,
                                        (Cantidad+Entradas_Salidas) as NuevoStock,(Cantidad-Cantidad_Stock+Entradas_Salidas) as DiferenciaCantidad, 
                                        (Peso_Stock - Peso) as DiferenciaPeso
                                        from tempComparacionInventario
                                        Where IdInventario=@idinventarioenc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable.Rows.Count > 0 Then
                            Return lDataTable
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

    Public Shared Function Get_Recepciones(ByVal IdProductoBodega As Integer, ByVal FechaCongelado As DateTime, ByVal FechaConteo As DateTime, ByVal IdInventarioEnc As Integer) As Double

        Try

            Dim lMax As Integer = 0
            Dim vSQL As String = "SELECT SUM(dbo.trans_movimientos.cantidad) AS ingreso,dbo.trans_inv_ciclico.IdProductoBodega
                                 FROM  dbo.trans_inv_ciclico INNER JOIN
                                        dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND 
                                        dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
                                        dbo.trans_movimientos ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.trans_movimientos.IdProductoBodega AND 
                                        dbo.trans_inv_ciclico.IdUbicacion = dbo.trans_movimientos.IdUbicacionDestino"
            vSQL += String.Format(" And cast(trans_movimientos.Fecha As Date) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(FechaCongelado), FormatoFechas.fFechaHora(FechaConteo))
            vSQL += "and trans_inv_ciclico.IdProductoBodega=@IdProductoBodega "
            vSQL += "GROUP BY dbo.trans_inv_ciclico.idinventarioenc, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_enc.fec_agr, dbo.trans_inv_ciclico.fec_agr, 
                                        dbo.trans_inv_ciclico.IdUbicacion
                                HAVING (dbo.trans_inv_ciclico.idinventarioenc = @IdInventario)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventarioEnc)
                    lCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Despachos(ByVal IdProductoBodega As Integer, ByVal FechaCongelado As DateTime, ByVal FechaConteo As DateTime, ByVal IdInventarioEnc As Integer) As Double

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT SUM(dbo.trans_movimientos.cantidad) AS ingreso , dbo.trans_inv_ciclico.IdProductoBodega
                         FROM            dbo.trans_inv_ciclico INNER JOIN
                         dbo.trans_inv_enc ON dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc AND 
                         dbo.trans_inv_ciclico.idinventarioenc = dbo.trans_inv_enc.idinventarioenc INNER JOIN
                         dbo.trans_movimientos ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.trans_movimientos.IdProductoBodega AND 
                         dbo.trans_inv_ciclico.IdUbicacion = dbo.trans_movimientos.IdUbicacionOrigen
                         WHERE (dbo.trans_inv_ciclico.idinventarioenc = @IdInventario)"

            vSQL += String.Format(" And cast(trans_movimientos.Fecha As Date) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(FechaCongelado), FormatoFechas.fFechaHora(FechaConteo))

            vSQL += "GROUP BY dbo.trans_inv_ciclico.IdProductoBodega
                         HAVING        (dbo.trans_inv_ciclico.IdProductoBodega = @IdProductoBodega)"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventarioEnc)
                    lCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_FechaMax(ByVal IdInventarioEnc As Integer, ByVal IdProductoBodega As Integer) As DateTime

        Try

            Dim lMax As DateTime

            Dim vSQL As String = "SELECT fec_agr from trans_inv_ciclico where idinventarioenc=@IdInventario and IdProductoBodega = @IdProductoBodega
                    Union
                    Select fec_agr from trans_inv_reconteo where idinventarioenc=@IdInventario and IdProductoBodega = @IdProductoBodega
                    Order by fec_agr desc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventarioEnc)
                    lCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CType(lReturnValue, DateTime)
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdUbicacionByIdStock(ByVal IdInventarioEnc As Integer, ByVal IdStock As Integer) As Integer

        Dim IdUbicacion As Integer = 0

        Try

            Dim vSQL As String = "SELECT IdUbicacion from trans_inv_ciclico where idinventarioenc=@IdInventario and IdStock=@IdStock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventarioEnc)
                    lCommand.Parameters.AddWithValue("@IdStock", IdStock)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        IdUbicacion = CType(lReturnValue, Integer)

                    End If

                End Using

            End Using

            Return IdUbicacion

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetIdOperadorByIdStock(ByVal IdInventarioEnc As Integer, ByVal IdStock As Integer) As Integer

        Dim IdOperador As Integer = 0

        Try

            Dim vSQL As String = "SELECT IdOperador from trans_inv_ciclico where idinventarioenc=@IdInventario and IdStock=@IdStock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdInventario", IdInventarioEnc)
                    lCommand.Parameters.AddWithValue("@IdStock", IdStock)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then

                        IdOperador = CType(lReturnValue, Integer)

                    End If

                End Using

            End Using

            Return IdOperador

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Conteo_Ciclico_HH(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico_vw,
                                                       ByRef Resultado As String) As Integer

        Dim ListaInvCiclico As New List(Of clsBeTrans_inv_ciclico)
        Dim ListaInvCiclicoRec As New List(Of clsBeTrans_inv_reconteo)
        Dim rslt As Integer = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vCantidadContada As Double = 0
        Dim vCantidadPendiente As Double = 0
        Dim vPesoContado As Double = 0
        Dim vPesoPendiente As Double = 0
        Dim vCantidadCompletada As Boolean = False
        Dim vCantPendLinea As Double = 0

        Dim pBeProducto As New clsBeProducto
        Dim pCampos(3) As clsBeProducto.ProdPropiedades

        Actualiza_Conteo_Ciclico_HH = Nothing

        Try

            If BeTransInvCiclico.IdReconteo = 0 Then

                Resultado = ""

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pCampos(2) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pBeProducto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                                       lConnection,
                                                                                       lTransaction)

                Resultado = "IdProducto es: " & pBeProducto.IdProducto & vbNewLine

                pBeProducto = clsLnProducto.GetSingle(pBeProducto.IdProducto, pCampos, lConnection, lTransaction)

                Resultado = "Se obtuvo el producto, control_vence es: " & pBeProducto.Control_vencimiento & vbNewLine

                Actualiza_Producto_Inventario(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)

                Resultado = "Se Actualiza_Producto_Inventario" & vbNewLine

                ListaInvCiclico = Get_All_By_Producto_Inventario(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)

                Resultado = "Se Obtuvo lista " & vbNewLine

                vCantidadContada = ListaInvCiclico.Sum(Function(x) x.Cantidad)

                Resultado = "Cantidad Contada " & vCantidadContada & vbNewLine

                vCantidadPendiente = BeTransInvCiclico.Cantidad - vCantidadContada

                Resultado = "Cantidad Pendiente " & vCantidadPendiente & vbNewLine

                vPesoContado = ListaInvCiclico.Sum(Function(x) x.Peso)
                vPesoPendiente = BeTransInvCiclico.Peso - vPesoContado

                For Each itemr In ListaInvCiclico

                    If vCantidadPendiente > 0 Then

                        vCantPendLinea = Math.Round((itemr.Cant_stock - itemr.Cantidad), 6)

                        If vCantidadPendiente >= vCantPendLinea Then 'CAmbie el itemr.cant_Stock por vCantPendLinea
                            If vCantPendLinea > 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                Else
                                    itemr.Cantidad += vCantPendLinea
                                    vCantidadPendiente -= vCantPendLinea
                                End If
                            ElseIf vCantPendLinea <= 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        ElseIf vCantidadPendiente < vCantPendLinea Then 'CAmbie el itemr.cant_Stock por vCantPendLinea
                            If vCantPendLinea > 0 Then
                                If vCantidadPendiente > 0 Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                ElseIf vCantidadPendiente < 0 Then
                                    If itemr Is ListaInvCiclico.LastOrDefault() Then
                                        itemr.Cantidad += vCantidadPendiente
                                        vCantidadPendiente -= vCantidadPendiente
                                    End If
                                End If
                            Else
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        End If

                    Else

                        vCantidadPendiente = Math.Abs(vCantidadPendiente)

                        If vCantidadPendiente <= itemr.Cantidad Then
                            itemr.Cantidad -= vCantidadPendiente
                            vCantidadPendiente -= vCantidadPendiente
                        ElseIf vCantidadPendiente > itemr.Cantidad And itemr.Cantidad > 0 Then
                            vCantidadPendiente -= itemr.Cantidad
                            itemr.Cantidad -= itemr.Cantidad
                        End If

                        vCantidadPendiente = -vCantidadPendiente

                    End If

                    If vPesoPendiente = itemr.Peso Then
                        itemr.Peso = itemr.Peso_stock
                        vPesoPendiente -= itemr.Peso_stock
                    ElseIf vPesoPendiente < itemr.Peso Then
                        vPesoPendiente = itemr.Peso
                        itemr.Peso = vPesoPendiente
                    ElseIf vPesoPendiente > itemr.Peso Then
                        itemr.Peso = itemr.Peso_stock
                        vPesoPendiente -= itemr.Peso_stock
                    End If

                    itemr.Cantidad = Math.Round(itemr.Cantidad, 6)
                    itemr.Peso = Math.Round(itemr.Peso, 6)

                    vCantidadPendiente = Math.Round(vCantidadPendiente, 6)
                    vPesoPendiente = Math.Round(vPesoPendiente, 6)
                    itemr.Fec_Mod = Now

                    '#CKFK20241119 Agregué una validación para que si cambia el lote o la fecha o el estado genere una nueva línea
                    If itemr.Fecha_vence <> itemr.Fecha_vence_stock OrElse
                       (itemr.IdProductoEstado <> itemr.IdProductoEst_nuevo AndAlso itemr.IdProductoEst_nuevo <> 0) OrElse
                       itemr.Lote <> itemr.Lote_stock Then

                        itemr.IdInvCiclico = MaxID(lConnection, lTransaction) + 1
                        Insertar(itemr, lConnection, lTransaction)

                    Else
                        rslt += Actualiza_Inventario_Ciclico(itemr, pBeProducto, lConnection, lTransaction)
                    End If

                    Resultado += " Actualiza_Inventario_Ciclico Ok " & vbNewLine

                    vCantidadCompletada = (vCantidadPendiente = 0)

                    If vCantidadCompletada Then
                        Exit For
                    End If

                Next

                Resultado += " 4. Después de actualizar las cantidades por IdStock: " & Date.Now

                lTransaction.Commit()

            Else

                lConnection.Open()

                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                ListaInvCiclicoRec = clsLnTrans_inv_reconteo.Get_All_By_Item_Inv(BeTransInvCiclico, lConnection, lTransaction)

                vCantidadContada = ListaInvCiclicoRec.Sum(Function(x) x.Cantidad)
                vCantidadPendiente = BeTransInvCiclico.Cantidad - vCantidadContada

                vPesoContado = ListaInvCiclico.Sum(Function(x) x.Peso)
                vPesoPendiente = BeTransInvCiclico.Peso - vPesoContado

                For Each itemr In ListaInvCiclicoRec

                    If vCantidadPendiente > 0 Then

                        vCantPendLinea = Math.Round((itemr.CantidadAnterior - itemr.Cantidad), 6)

                        If vCantidadPendiente >= itemr.CantidadAnterior Then
                            If vCantPendLinea > 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                Else
                                    itemr.Cantidad += vCantPendLinea
                                    vCantidadPendiente -= vCantPendLinea
                                End If
                            ElseIf vCantPendLinea < 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        ElseIf vCantidadPendiente < itemr.CantidadAnterior Then
                            If vCantidadPendiente > 0 Then
                                itemr.Cantidad += vCantidadPendiente
                                vCantidadPendiente -= vCantidadPendiente
                            ElseIf vCantidadPendiente < 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        End If

                    Else

                        vCantidadPendiente = Math.Abs(vCantidadPendiente)

                        If vCantidadPendiente <= itemr.Cantidad Then
                            itemr.Cantidad -= vCantidadPendiente
                            vCantidadPendiente -= vCantidadPendiente
                        ElseIf vCantidadPendiente > itemr.Cantidad And itemr.Cantidad > 0 Then
                            vCantidadPendiente -= itemr.Cantidad
                            itemr.Cantidad -= itemr.Cantidad
                        End If

                        vCantidadPendiente = -vCantidadPendiente

                    End If

                    If vPesoPendiente = itemr.Peso Then
                        itemr.Peso = itemr.PesoAnterior
                        vPesoPendiente -= itemr.PesoAnterior
                    ElseIf vPesoPendiente < itemr.Peso Then
                        vPesoPendiente = itemr.Peso
                        itemr.Peso = vPesoPendiente
                    ElseIf vPesoPendiente > itemr.Peso Then
                        itemr.Peso = itemr.PesoAnterior
                        vPesoPendiente -= itemr.PesoAnterior
                    End If

                    itemr.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    itemr.IdPresentacion = BeTransInvCiclico.idPresentacion_nuevo

                    itemr.Lote = BeTransInvCiclico.Lote
                    itemr.Fecha_vence = BeTransInvCiclico.Fecha_vence

                    rslt += clsLnTrans_inv_reconteo.Actualizar(itemr, lConnection, lTransaction)

                    vCantidadCompletada = (vCantidadPendiente = 0)

                    If vCantidadCompletada Then
                        Exit For
                    End If

                Next

                lTransaction.Commit()

                If rslt = 0 Then
                    Throw New Exception("Este es Grial, no se pudo actualizar el stock, dev#28")
                End If

            End If

            Return rslt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#AT 20220208 Copia de Actualiza_Conteo_Ciclico_HH
    Public Shared Function Actualiza_Conteo_Ciclico_HH_(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico,
                                                       ByVal pReconteo As Integer,
                                                       ByRef Resultado As String) As Integer

        Dim ListaInvCiclico As New List(Of clsBeTrans_inv_ciclico)
        Dim ListaInvCiclicoRec As New List(Of clsBeTrans_inv_reconteo)
        Dim rslt As Integer = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vCantidadContada As Double = 0
        Dim vCantidadPendiente As Double = 0
        Dim vPesoContado As Double = 0
        Dim vPesoPendiente As Double = 0
        Dim vCantidadCompletada As Boolean = False
        Dim vCantPendLinea As Double = 0

        Dim pBeProducto As New clsBeProducto
        Dim pCampos(3) As clsBeProducto.ProdPropiedades

        Actualiza_Conteo_Ciclico_HH_ = Nothing

        Try

            If pReconteo = 0 Then

                Resultado = ""

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pCampos(2) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pBeProducto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                                       lConnection,
                                                                                       lTransaction)

                Resultado = "IdProducto es: " & pBeProducto.IdProducto & vbNewLine

                pBeProducto = clsLnProducto.GetSingle(pBeProducto.IdProducto, pCampos, lConnection, lTransaction)

                Resultado = "Se obtuvo el producto, control_vence es: " & pBeProducto.Control_vencimiento & vbNewLine

                Actualiza_Producto_Inventario(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)

                Resultado = "Se Actualiza_Producto_Inventario" & vbNewLine

                ListaInvCiclico = Get_All_By_Producto_Inventario(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)

                Resultado = "Se Obtuvo lista " & vbNewLine

                vCantidadContada = ListaInvCiclico.Sum(Function(x) x.Cantidad)

                Resultado = "Cantidad Contada " & vCantidadContada & vbNewLine

                vCantidadPendiente = BeTransInvCiclico.Cantidad - vCantidadContada

                Resultado = "Cantidad Pendiente " & vCantidadPendiente & vbNewLine

                vPesoContado = ListaInvCiclico.Sum(Function(x) x.Peso)
                vPesoPendiente = BeTransInvCiclico.Peso - vPesoContado

                For Each itemr In ListaInvCiclico

                    If vCantidadPendiente > 0 Then

                        vCantPendLinea = Math.Round((itemr.Cant_stock - itemr.Cantidad), 6)

                        If vCantidadPendiente >= vCantPendLinea Then 'CAmbie el itemr.cant_Stock por vCantPendLinea
                            If vCantPendLinea > 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                Else
                                    itemr.Cantidad += vCantPendLinea
                                    vCantidadPendiente -= vCantPendLinea
                                End If
                            ElseIf vCantPendLinea <= 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        ElseIf vCantidadPendiente < vCantPendLinea Then 'CAmbie el itemr.cant_Stock por vCantPendLinea
                            If vCantPendLinea > 0 Then
                                If vCantidadPendiente > 0 Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                ElseIf vCantidadPendiente < 0 Then
                                    If itemr Is ListaInvCiclico.LastOrDefault() Then
                                        itemr.Cantidad += vCantidadPendiente
                                        vCantidadPendiente -= vCantidadPendiente
                                    End If
                                End If
                            Else
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        End If

                    Else

                        vCantidadPendiente = Math.Abs(vCantidadPendiente)

                        If vCantidadPendiente <= itemr.Cantidad Then
                            itemr.Cantidad -= vCantidadPendiente
                            vCantidadPendiente -= vCantidadPendiente
                        ElseIf vCantidadPendiente > itemr.Cantidad And itemr.Cantidad > 0 Then
                            vCantidadPendiente -= itemr.Cantidad
                            itemr.Cantidad -= itemr.Cantidad
                        End If

                        vCantidadPendiente = -vCantidadPendiente

                    End If

                    If vPesoPendiente = itemr.Peso Then
                        itemr.Peso = itemr.Peso_stock
                        vPesoPendiente -= itemr.Peso_stock
                    ElseIf vPesoPendiente < itemr.Peso Then
                        vPesoPendiente = itemr.Peso
                        itemr.Peso = vPesoPendiente
                    ElseIf vPesoPendiente > itemr.Peso Then
                        itemr.Peso = itemr.Peso_stock
                        vPesoPendiente -= itemr.Peso_stock
                    End If

                    itemr.Cantidad = Math.Round(itemr.Cantidad, 6)
                    itemr.Peso = Math.Round(itemr.Peso, 6)

                    vCantidadPendiente = Math.Round(vCantidadPendiente, 6)
                    vPesoPendiente = Math.Round(vPesoPendiente, 6)
                    itemr.Fec_Mod = Now

                    '#CKFK20241119 Agregué una validación para que si cambia el lote o la fecha o el estado genere una nueva línea
                    '#AT20241122 Agregué una validación para que si la ubicacion_nuevo es diferente a cero tambien genere una nueva línea
                    If itemr.Fecha_vence <> itemr.Fecha_vence_stock OrElse
                       (itemr.IdProductoEstado <> itemr.IdProductoEst_nuevo AndAlso itemr.IdProductoEst_nuevo <> 0) OrElse
                       itemr.Lote <> itemr.Lote_stock OrElse
                       itemr.IdUbicacion_nuevo <> 0 Then

                        itemr.IdInvCiclico = MaxID(lConnection, lTransaction) + 1
                        Insertar(itemr, lConnection, lTransaction)

                    Else
                        rslt += Actualiza_Inventario_Ciclico(itemr, pBeProducto, lConnection, lTransaction)
                    End If

                    Resultado += " Actualiza_Inventario_Ciclico Ok " & vbNewLine

                    vCantidadCompletada = (vCantidadPendiente = 0)

                    If vCantidadCompletada Then
                        Exit For
                    End If

                Next

                Resultado += " 4. Después de actualizar las cantidades por IdStock: " & Date.Now

                lTransaction.Commit()

            Else

                lConnection.Open()

                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                ListaInvCiclicoRec = clsLnTrans_inv_reconteo.Get_All_By_Item_Inv(BeTransInvCiclico, lConnection, lTransaction)

                vCantidadContada = ListaInvCiclicoRec.Sum(Function(x) x.Cantidad)
                vCantidadPendiente = BeTransInvCiclico.Cantidad - vCantidadContada

                vPesoContado = ListaInvCiclico.Sum(Function(x) x.Peso)
                vPesoPendiente = BeTransInvCiclico.Peso - vPesoContado

                For Each itemr In ListaInvCiclicoRec

                    If vCantidadPendiente > 0 Then

                        vCantPendLinea = Math.Round((itemr.CantidadAnterior - itemr.Cantidad), 6)

                        If vCantidadPendiente >= itemr.CantidadAnterior Then
                            If vCantPendLinea > 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                Else
                                    itemr.Cantidad += vCantPendLinea
                                    vCantidadPendiente -= vCantPendLinea
                                End If
                            ElseIf vCantPendLinea < 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        ElseIf vCantidadPendiente < itemr.CantidadAnterior Then
                            If vCantidadPendiente > 0 Then
                                itemr.Cantidad += vCantidadPendiente
                                vCantidadPendiente -= vCantidadPendiente
                            ElseIf vCantidadPendiente < 0 Then
                                If itemr Is ListaInvCiclico.LastOrDefault() Then
                                    itemr.Cantidad += vCantidadPendiente
                                    vCantidadPendiente -= vCantidadPendiente
                                End If
                            End If
                        End If

                    Else

                        vCantidadPendiente = Math.Abs(vCantidadPendiente)

                        If vCantidadPendiente <= itemr.Cantidad Then
                            itemr.Cantidad -= vCantidadPendiente
                            vCantidadPendiente -= vCantidadPendiente
                        ElseIf vCantidadPendiente > itemr.Cantidad And itemr.Cantidad > 0 Then
                            vCantidadPendiente -= itemr.Cantidad
                            itemr.Cantidad -= itemr.Cantidad
                        End If

                        vCantidadPendiente = -vCantidadPendiente

                    End If

                    If vPesoPendiente = itemr.Peso Then
                        itemr.Peso = itemr.PesoAnterior
                        vPesoPendiente -= itemr.PesoAnterior
                    ElseIf vPesoPendiente < itemr.Peso Then
                        vPesoPendiente = itemr.Peso
                        itemr.Peso = vPesoPendiente
                    ElseIf vPesoPendiente > itemr.Peso Then
                        itemr.Peso = itemr.PesoAnterior
                        vPesoPendiente -= itemr.PesoAnterior
                    End If

                    itemr.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    itemr.IdPresentacion = BeTransInvCiclico.IdPresentacion_nuevo

                    itemr.Lote = BeTransInvCiclico.Lote
                    itemr.Fecha_vence = BeTransInvCiclico.Fecha_vence

                    rslt += clsLnTrans_inv_reconteo.Actualizar(itemr, lConnection, lTransaction)

                    vCantidadCompletada = (vCantidadPendiente = 0)

                    If vCantidadCompletada Then
                        Exit For
                    End If

                Next

                lTransaction.Commit()

                If rslt = 0 Then
                    Throw New Exception("Este es Grial, no se pudo actualizar el stock, dev#28")
                End If

            End If

            Return rslt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#AT20241205 Nueva Funcion para manejar el conteo del inventario ciclico
    Public Shared Function Actualiza_Conteo_Ciclico_HH(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico,
                                                       ByVal pReconteo As Integer,
                                                       ByVal esOriginal As Boolean,
                                                       ByRef Resultado As String,
                                                       ByVal CrearTallaColor As Boolean,
                                                       ByVal ProductoTallaColor As clsBeProducto_talla_color) As Integer
        Dim rslt As Integer = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim pBeProducto As New clsBeProducto
        Dim pCampos(2) As clsBeProducto.ProdPropiedades
        Dim InvCiclico As clsBeTrans_inv_ciclico = Nothing

        Actualiza_Conteo_Ciclico_HH = Nothing

        Try

            If pReconteo = 0 Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pBeProducto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                                           lConnection,
                                                                                           lTransaction)

                pBeProducto = clsLnProducto.GetSingle(pBeProducto.IdProducto,
                                                      pCampos,
                                                      lConnection,
                                                      lTransaction)

                InvCiclico = Get_Inventario_Ciclico(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)


                If CrearTallaColor AndAlso ProductoTallaColor IsNot Nothing Then
                    ProductoTallaColor.IdProductoTallaColor = clsLnProducto_talla_color.MaxID() + 1

                    If clsLnProducto_talla_color.Insertar(ProductoTallaColor) > 0 Then
                        Dim PrdTallaColor = clsLnProducto_talla_color.Get_Single_By_IdColor_IdTalla(ProductoTallaColor.IdProducto,
                                                                                                    ProductoTallaColor.IdTalla,
                                                                                                    ProductoTallaColor.IdColor)

                        BeTransInvCiclico.IdProductoTallaColor_nuevo = PrdTallaColor.IdProductoTallaColor
                    End If
                End If

                'Si no viene talla/color nuevo, se asume que NO cambió.
                If BeTransInvCiclico.IdProductoTallaColor_nuevo = 0 Then
                    BeTransInvCiclico.IdProductoTallaColor_nuevo = BeTransInvCiclico.IdProductoTallaColor
                End If

                If esOriginal And (BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock OrElse
                       (BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo AndAlso BeTransInvCiclico.IdProductoEst_nuevo <> 0) OrElse
                       BeTransInvCiclico.Lote <> BeTransInvCiclico.Lote_stock OrElse
                       BeTransInvCiclico.IdUbicacion_nuevo <> 0 OrElse CrearTallaColor OrElse BeTransInvCiclico.IdProductoTallaColor_nuevo <> BeTransInvCiclico.IdProductoTallaColor) Then

                    BeTransInvCiclico.IdInvCiclico = MaxID(lConnection, lTransaction) + 1
                    BeTransInvCiclico.IdStock = InvCiclico.IdStock
                    BeTransInvCiclico.IdUnidadMedida = InvCiclico.IdUnidadMedida
                    BeTransInvCiclico.IdBodega = InvCiclico.IdBodega
                    BeTransInvCiclico.Cant_stock = 0
                    BeTransInvCiclico.Fec_agr = Now
                    BeTransInvCiclico.Fec_Mod = Now

                    rslt = Insertar(BeTransInvCiclico, lConnection, lTransaction)

                Else
                    BeTransInvCiclico.Fec_Mod = Now
                    rslt = Act_Inventario_Ciclico(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)
                End If

                lTransaction.Commit()

            End If

            Return rslt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAllByItemInv(ByVal pitem As clsBeTrans_inv_ciclico_vw) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT  *
                        FROM  trans_inv_ciclico 
                        WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                        AND Lote_Stock = @Lote  AND CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock)
                        AND IdInventarioEnc = @IdInventarioEnc "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()

                            Cargar(Obj, lRow)

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

    Public Shared Function Get_All_By_Producto_Inventario(ByVal pitem As clsBeTrans_inv_ciclico_vw,
                                                          ByVal pBeProducto As clsBeProducto,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT  *
                                  FROM  trans_inv_ciclico "

            vSQL += "WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion And IdInventarioEnc = @IdInventarioEnc "

            If pBeProducto.Control_lote Then
                vSQL += "And Lote_Stock = @Lote "
            End If

            If Not pBeProducto.Control_lote AndAlso pitem.Lote_stock <> "" Then
                vSQL += "And Lote_Stock = @Lote "
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += "And CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock)"
            End If

            vSQL += " And IdOperador = @IdOperador "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pitem.Idoperador)

                If pBeProducto.Control_lote Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)

                End If

                If Not pBeProducto.Control_lote AndAlso pitem.Lote_stock <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                End If

                If pBeProducto.Control_vencimiento Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_inv_ciclico

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeTrans_inv_ciclico()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20241205 Nueva Funcion para manejar el conteo del inventario ciclico
    Public Shared Function Actualiza_Conteo_Ciclico_HH(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico,
                                                       ByVal pReconteo As Integer,
                                                       ByVal esOriginal As Boolean,
                                                       ByRef Resultado As String) As Integer
        Dim rslt As Integer = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim pBeProducto As New clsBeProducto
        Dim pCampos(2) As clsBeProducto.ProdPropiedades
        Dim InvCiclico As clsBeTrans_inv_ciclico = Nothing

        Actualiza_Conteo_Ciclico_HH = Nothing

        Try

            If pReconteo = 0 Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento
                pBeProducto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                                           lConnection,
                                                                                           lTransaction)

                pBeProducto = clsLnProducto.GetSingle(pBeProducto.IdProducto,
                                                      pCampos,
                                                      lConnection,
                                                      lTransaction)

                InvCiclico = Get_Inventario_Ciclico(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)


                If esOriginal And (BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock OrElse
                       (BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo AndAlso BeTransInvCiclico.IdProductoEst_nuevo <> 0) OrElse
                       BeTransInvCiclico.Lote <> BeTransInvCiclico.Lote_stock OrElse
                       BeTransInvCiclico.IdUbicacion_nuevo <> 0) Then

                    BeTransInvCiclico.IdInvCiclico = MaxID(lConnection, lTransaction) + 1
                    BeTransInvCiclico.IdStock = InvCiclico.IdStock
                    BeTransInvCiclico.IdUnidadMedida = InvCiclico.IdUnidadMedida
                    BeTransInvCiclico.IdBodega = InvCiclico.IdBodega
                    BeTransInvCiclico.Cant_stock = 0
                    BeTransInvCiclico.Fec_agr = Now
                    BeTransInvCiclico.Fec_Mod = Now

                    rslt = Insertar(BeTransInvCiclico, lConnection, lTransaction)

                Else
                    BeTransInvCiclico.Fec_Mod = Now
                    rslt = Act_Inventario_Ciclico(BeTransInvCiclico, pBeProducto, lConnection, lTransaction)
                End If

                lTransaction.Commit()

            End If

            Return rslt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_Producto_Inventario(ByVal pitem As clsBeTrans_inv_ciclico,
                                                          ByVal pBeProducto As clsBeProducto,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT  *
                    FROM  trans_inv_ciclico "

            vSQL += "WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion And IdInventarioEnc = @IdInventarioEnc 
                           AND IdOperador = @IdOperador "

            If pBeProducto.Control_lote Then
                vSQL += "And Lote_Stock = @Lote "
            End If

            If Not pBeProducto.Control_lote AndAlso pitem.Lote_stock <> "" Then
                vSQL += "And Lote_Stock = @Lote "
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += "And CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock)"
            End If

            If pitem.lic_plate <> "" Then
                vSQL += "And lic_plate = @LicPlate "
            End If

            If pitem.IdPresentacion <> 0 Then
                vSQL += "And IdPresentacion = @IdPresentacion "
            Else
                vSQL += "And IdPresentacion = 0 "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pitem.Idoperador)

                If pBeProducto.Control_lote Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)

                End If

                If Not pBeProducto.Control_lote AndAlso pitem.Lote_stock <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                End If

                If pBeProducto.Control_vencimiento Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
                End If

                If pitem.lic_plate <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@LicPlate", pitem.lic_plate)
                End If

                If pitem.IdPresentacion Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pitem.IdPresentacion)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_inv_ciclico

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeTrans_inv_ciclico()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Inventario_Ciclico(ByVal pitem As clsBeTrans_inv_ciclico,
                                                  ByVal pBeProducto As clsBeProducto,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As clsBeTrans_inv_ciclico

        Try
            Dim Obj As New clsBeTrans_inv_ciclico

            Dim vSQL As String = "SELECT  *
                    FROM  trans_inv_ciclico "

            vSQL += "WHERE IdProductoBodega = @IdProductoBodega AND IdInvCiclico = @IdInvCiclico AND IdInventarioEnc = @IdInventarioEnc "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInvCiclico", pitem.IdInvCiclico)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDataTable.Rows(0)

                    Obj = New clsBeTrans_inv_ciclico()
                    Cargar(Obj, lRow)
                End If

            End Using

            Return Obj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'AT20240827 Devolver el total de los conteos realizados 
    Public Shared Function Get_Total_InvCiclico(ByVal pitem As clsBeTrans_inv_ciclico) As Double

        Try

            Get_Total_InvCiclico = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT  sum(cantidad) as cantidad_contada
                    FROM  trans_inv_ciclico 
                    WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                    AND lote_stock = @Lote  AND CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence)
                    AND IdPresentacion = @IdPresentacion
                    AND IdInventarioEnc = @IdInventarioEnc
                    AND IdStock = @IdStock "

                vSQL += " GROUP BY IdProductoBodega, IdProductoEstado, IdPresentacion, IdUbicacion, lote_stock, CONVERT(DATE,fecha_vence_stock) "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pitem.Fecha_vence_stock)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pitem.IdPresentacion)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pitem.IdStock)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As New clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Get_Total_InvCiclico = lDataTable.Rows(0)("cantidad_contada")

                    End If

                End Using

            End Using

            Return Get_Total_InvCiclico

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Producto_Inventario(ByVal pitem As clsBeTrans_inv_ciclico_vw,
                                                         ByVal pBeProducto As clsBeProducto,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim vSQL As String = "UPDATE trans_inv_ciclico SET  
                                         IdPresentacion_nuevo = @IdPresentacion_nuevo,
                                         IdProductoEst_nuevo = @IdProductoEst_nuevo"

            If pBeProducto.Control_lote Then
                vSQL += ",Lote = @Lote"
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += ",Fecha_vence = @Fecha_vence"
            End If

            vSQL += " WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion AND IdInventarioEnc = @IdInventarioEnc "

            If pBeProducto.Control_lote Then
                vSQL += "AND Lote_Stock = @LoteStock "
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += "AND CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock) "
            End If

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
            cmd.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)

            If pBeProducto.Control_lote Then
                cmd.Parameters.AddWithValue("@LoteStock", pitem.Lote_stock)
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
            End If

            cmd.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

            cmd.Parameters.AddWithValue("@IdPresentacion_nuevo", pitem.idPresentacion_nuevo)
            cmd.Parameters.AddWithValue("@IdProductoEst_nuevo", pitem.IdProductoEst_nuevo)

            If pBeProducto.Control_lote Then
                cmd.Parameters.AddWithValue("@Lote", pitem.Lote)
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.AddWithValue("@Fecha_vence", pitem.Fecha_vence)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Producto_Inventario(ByVal pitem As clsBeTrans_inv_ciclico,
                                                         ByVal pBeProducto As clsBeProducto,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim vSQL As String = "UPDATE trans_inv_ciclico SET  
                                                IdPresentacion_nuevo = @IdPresentacion_nuevo,
                                                IdProductoEst_nuevo = @IdProductoEst_nuevo"
            If pBeProducto.Control_lote Then
                vSQL += ",Lote = @Lote"
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += ",Fecha_vence = @Fecha_vence"
            End If

            If pitem.IdUbicacion_nuevo <> 0 Then
                vSQL += ",IdUbicacion_nuevo = @IdUbicacion_nuevo"
            End If

            vSQL += " WHERE IdProductoBodega = @IdProductoBodega AND 
                            IdUbicacion  = @IdUbicacion AND 
                            IdInventarioEnc = @IdInventarioEnc AND
                            IdOperador = @IdOperador "

            If pBeProducto.Control_lote Then
                vSQL += "AND Lote_Stock = @LoteStock "
            End If

            If pBeProducto.Control_vencimiento Then
                vSQL += "AND CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock) "
            End If

            If pitem.lic_plate <> "" Then
                vSQL += "AND lic_plate = @LicPlate "
            End If

            If pitem.IdPresentacion <> 0 Then
                vSQL += "And IdPresentacion = @IdPresentacion "
            Else
                vSQL += "And IdPresentacion = 0 "
            End If

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
            cmd.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)

            If pitem.IdUbicacion_nuevo <> 0 Then
                cmd.Parameters.AddWithValue("@IdUbicacion_nuevo", pitem.IdUbicacion_nuevo)
            End If

            If pBeProducto.Control_lote Then
                cmd.Parameters.AddWithValue("@LoteStock", pitem.Lote_stock)
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
            End If

            cmd.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

            cmd.Parameters.AddWithValue("@IdPresentacion_nuevo", pitem.IdPresentacion_nuevo)
            cmd.Parameters.AddWithValue("@IdProductoEst_nuevo", pitem.IdProductoEst_nuevo)

            If pBeProducto.Control_lote Then
                cmd.Parameters.AddWithValue("@Lote", pitem.Lote)
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.AddWithValue("@Fecha_vence", pitem.Fecha_vence)
            End If

            If pitem.lic_plate <> "" Then
                cmd.Parameters.AddWithValue("@LicPlate", pitem.lic_plate)
            End If

            If pitem.IdPresentacion Then
                cmd.Parameters.AddWithValue("@IdPresentacion", pitem.IdPresentacion)
            End If

            cmd.Parameters.AddWithValue("@IdOperador", pitem.Idoperador)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_ItemInv(ByVal pitem As clsBeTrans_inv_ciclico_vw,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT  *
                        FROM  trans_inv_ciclico 
                        WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                              AND Lote_Stock = @Lote  AND CONVERT(DATE, Fecha_Vence_Stock) = CONVERT(DATE, @Fecha_Vence_Stock)
                              AND IdInventarioEnc = @IdInventarioEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pitem.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pitem.Lote_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence_Stock", pitem.Fecha_vence_stock)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pitem.Idinventarioenc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_inv_ciclico

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeTrans_inv_ciclico()
                        Cargar(Obj, lRow)
                        Obj.Cantidad = 0
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function EliminarByStock(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico" &
             "  Where(idinvciclico = @idinvciclico)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))


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

    Public Shared Function Eliminar_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                        ByVal IdInventario As Integer,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico" &
             "  Where(IdProductoBodega = @IdProductoBodega and idinventarioenc=@idinventarioenc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))


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

    Public Shared Function Get_Ubicaciones_A_Eliminar_By_IdInventarioEnc_And_IdproductoBodega(ByVal IdInventario As Integer,
                                                                                              ByVal IdProductoBodega As Integer) As List(Of Integer)

        Dim ListIdUbicacion As New List(Of Integer)

        Get_Ubicaciones_A_Eliminar_By_IdInventarioEnc_And_IdproductoBodega = Nothing

        Try

            Dim vSQL As String = "select Distinct IdUbicacion from trans_inv_ciclico where idinventarioenc = @IdInventario and IdProductoBodega=@IdProductoBodega 
                    and IdUbicacion not in(select IdUbicacion 
                    from trans_inv_ciclico where idinventarioenc =@IdInventario and IdProductoBodega<>@IdProductoBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdInventario", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim IdUbicacion As Integer = 0

            If dt.Rows.Count > 0 Then

                IdUbicacion = 0

                For Each lRow As DataRow In dt.Rows

                    If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                        IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                    End If

                    ListIdUbicacion.Add(IdUbicacion)

                Next

                Return ListIdUbicacion

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Conteo_Por_Estrato_Tipo(ByVal IdInventarioEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Inventario_prg_por_tipo WHERE idinventarioenc = @idinventarioenc  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", IdInventarioEnc)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Get_All_Inventario_Regularizacion(ByVal pIdInventario As Integer,
                                                        ByVal prg As Windows.Forms.ProgressBar,
                                                        ByVal rdStockInventarioMovs As Boolean,
                                                        ByVal Fec_agr As Date,
                                                        Optional ByVal lConnection As SqlConnection = Nothing,
                                                        Optional ByVal lTransaction As SqlTransaction = Nothing)

        Try

            Dim Temp As New clsBeTempComparacionInventario
            Dim vCantidad As Double = 0.0
            Dim vPeso As Double = 0.0
            Dim vEntradasSalidas As Double = 0.0
            Dim ListaMovs As New List(Of clsBeVW_Movimientos)
            Dim TempListaMovs As New List(Of clsBeVW_Movimientos)
            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim tmpBeStocnEnFecha As New clsBeStockEnUnaFecha
            Dim lBeStockEnFecha As New List(Of clsBeStockEnUnaFecha)
            Dim lBeStockEnFechaFilter As New List(Of clsBeStockEnUnaFecha)

            Dim vSQL As String =
                                "SELECT " &
                                "   T.idinventarioenc, " &
                                "   T.IdProductoBodega, " &
                                "   MIN(T.idinvciclico) AS idinvciclico, " &
                                "   SUM(T.cantidad_stock) AS cantidad_stock, " &
                                "   SUM(T.cantidad_conteo) AS cantidad_conteo, " &
                                "   SUM(T.cantidad_reconteo) AS cantidad_reconteo, " &
                                "   SUM(T.peso_stock) AS peso_stock, " &
                                "   SUM(T.peso_conteo) AS peso_conteo, " &
                                "   SUM(T.peso_reconteo) AS peso_reconteo, " &
                                "   T.Codigo, " &
                                "   T.Producto, " &
                                "   T.LoteOrigen, " &
                                "   T.LoteDestino, " &
                                "   T.fecha_vence, " &
                                "   T.Licencia, " &
                                "   T.Estado, " &
                                "   T.EstadoDestino, " &
                                "   T.UbicacionOrigen, " &
                                "   T.UbicacionDestino, " &
                                "   T.IdUbicacion, " &
                                "   T.IdUnidadMedida, " &
                                "   MAX(T.Fec_Mod) AS Fec_Mod, " &
                                "   T.IdPresentacion, " &
                                "   T.IdProductoEstado, " &
                                "   T.IdProductoEst_nuevo, " &
                                "   T.fecha_vence_stock, " &
                                "   SUM(T.Cantidad_Reservada) AS Cantidad_Reservada, " &
                                "   T.IdProductoTallaColor, " &
                                "   T.IdProductoTallaColor_nuevo, " &
                                "   T.Talla, " &
                                "   T.Color, " &
                                "   T.TallaNueva, " &
                                "   T.ColorNuevo " &
                                "FROM ( " &
                                "   SELECT " &
                                "       ciclico.idinventarioenc, " &
                                "       ciclico.IdProductoBodega, " &
                                "       ciclico.idinvciclico, " &
                                "       MAX(ciclico.cant_stock) AS cantidad_stock, " &
                                "       SUM(ciclico.cantidad) AS cantidad_conteo, " &
                                "       SUM(ciclico.cant_reconteo) AS cantidad_reconteo, " &
                                "       MAX(ciclico.peso_stock) AS peso_stock, " &
                                "       SUM(ciclico.peso) AS peso_conteo, " &
                                "       SUM(ciclico.peso_reconteo) AS peso_reconteo, " &
                                "       p.codigo AS Codigo, " &
                                "       p.nombre AS Producto, " &
                                "       ciclico.lote_stock AS LoteOrigen, " &
                                "       ciclico.lote AS LoteDestino, " &
                                "       ciclico.fecha_vence, " &
                                "       ciclico.lic_plate AS Licencia, " &
                                "       est1.nombre AS Estado, " &
                                "       est2.nombre AS EstadoDestino, " &
                                "       dbo.Nombre_Completo_Ubicacion(ciclico.IdUbicacion, ciclico.IdBodega) AS UbicacionOrigen, " &
                                "       ISNULL(dbo.Nombre_Completo_Ubicacion(ciclico.IdUbicacion_nuevo, ciclico.IdBodega), dbo.Nombre_Completo_Ubicacion(ciclico.IdUbicacion, ciclico.IdBodega)) AS UbicacionDestino, " &
                                "       ciclico.IdUbicacion, " &
                                "       MAX(ciclico.fec_mod) AS Fec_Mod, " &
                                "       ciclico.IdPresentacion, " &
                                "       ciclico.IdProductoEstado, " &
                                "       ciclico.IdProductoEst_nuevo, " &
                                "       ciclico.fecha_vence_stock, " &
                                "       ciclico.IdUnidadMedida, " &
                                "       MAX(ISNULL(ciclico.cantidad_reservada_umbas,0)) AS Cantidad_Reservada, " &
                                "       ciclico.IdProductoTallaColor, " &
                                "       ciclico.IdProductoTallaColor_nuevo, " &
                                "       ISNULL(talla.codigo,'') AS Talla, " &
                                "       ISNULL(color.nombre,'') AS Color, " &
                                "       ISNULL(tn.codigo,'') AS TallaNueva, " &
                                "       ISNULL(cn.nombre,'') AS ColorNuevo " &
                                "   FROM trans_inv_ciclico ciclico " &
                                "       INNER JOIN trans_inv_enc enc ON ciclico.idinventarioenc = enc.idinventarioenc " &
                                "       INNER JOIN producto_bodega pb ON ciclico.IdProductoBodega = pb.IdProductoBodega " &
                                "       INNER JOIN producto p ON pb.IdProducto = p.IdProducto " &
                                "       INNER JOIN producto_estado est1 ON ciclico.IdProductoEstado = est1.IdEstado " &
                                "       INNER JOIN producto_estado est2 ON ciclico.IdProductoEst_nuevo = est2.IdEstado " &
                                "       LEFT JOIN producto_talla_color ptc ON ciclico.IdProductoTallaColor = ptc.IdProductoTallaColor " &
                                "       LEFT JOIN talla ON talla.IdTalla = ptc.IdTalla " &
                                "       LEFT JOIN color ON color.IdColor = ptc.IdColor " &
                                "       LEFT JOIN producto_talla_color ptcn ON ciclico.IdProductoTallaColor_nuevo = ptcn.IdProductoTallaColor " &
                                "       LEFT JOIN talla tn ON tn.IdTalla = ptcn.IdTalla " &
                                "       LEFT JOIN color cn ON cn.IdColor = ptcn.IdColor " &
                                "   WHERE ciclico.idinventarioenc = @IdInventario " &
                                "   GROUP BY " &
                                "       ciclico.idinventarioenc, ciclico.IdProductoBodega, ciclico.idinvciclico, " &
                                "       p.codigo, p.nombre, ciclico.lote, ciclico.fecha_vence, ciclico.lic_plate, " &
                                "       ciclico.IdProductoEstado, ciclico.IdProductoEst_nuevo, ciclico.IdUbicacion, ciclico.IdUbicacion_nuevo, " &
                                "       est1.nombre, est2.nombre, ciclico.lote_stock, ciclico.IdStock, ciclico.IdBodega, " &
                                "       ciclico.IdUnidadMedida, ciclico.IdPresentacion, ciclico.fecha_vence_stock, " &
                                "       ciclico.IdProductoTallaColor, ciclico.IdProductoTallaColor_nuevo, " &
                                "       talla.codigo, color.nombre, tn.codigo, cn.nombre " &
                                ") T " &
                                "GROUP BY " &
                                "   T.idinventarioenc, T.IdProductoBodega, " &
                                "   T.Codigo, T.Producto, T.LoteOrigen, T.LoteDestino, T.fecha_vence, T.Licencia, " &
                                "   T.Estado, T.EstadoDestino, T.UbicacionOrigen, T.UbicacionDestino, T.IdUbicacion, " &
                                "   T.IdUnidadMedida, T.IdPresentacion, T.IdProductoEstado, T.IdProductoEst_nuevo, T.fecha_vence_stock, " &
                                "   T.IdProductoTallaColor, T.IdProductoTallaColor_nuevo, T.Talla, T.Color, T.TallaNueva, T.ColorNuevo " &
                                "ORDER BY T.Codigo"



            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

            Dim lDataTable As New DataTable
            dad.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                Dim vContador As Integer = 0

                ListaMovs = clsLnTrans_movimientos.Get_All_Movimientos_By_Rango_Fechas(Fec_agr,
                                                                                       Now,
                                                                                       lConnection,
                                                                                       lTransaction)
                prg.Maximum = lDataTable.Rows.Count

                For Each lRow As DataRow In lDataTable.Rows

                    BeStockEnFecha = New clsBeStockEnUnaFecha

                    Temp = New clsBeTempComparacionInventario

                    Temp.IdInventario = lRow("idinventarioenc")
                    Temp.IdInvciclico = 0
                    Temp.IdProducto = clsLnProducto.Get_IdProducto_By_IdProductoBodega(lRow("IdProductoBodega"), lConnection, lTransaction)
                    Temp.Codigo = lRow("Codigo")
                    Temp.Producto = lRow("Producto")
                    Temp.IdProductoBodega = If(IsDBNull(lRow("IdProductoBodega")), 0, CInt(lRow("IdProductoBodega")))
                    Temp.IdUbicacion = If(IsDBNull(lRow("IdUbicacion")), 0, CInt(lRow("IdUbicacion")))
                    Temp.Fec_Mod = lRow("Fec_Mod")
                    Temp.Cantidad_Stock = lRow("cantidad_stock")
                    Temp.Peso_Stock = lRow("peso_stock")
                    Temp.IdProductoEstado = If(IsDBNull(lRow("IdProductoEstado")), 0, CInt(lRow("IdProductoEstado")))
                    Temp.IdProductoEst_nuevo = If(IsDBNull(lRow("IdProductoEst_nuevo")), 0, CInt(lRow("IdProductoEst_nuevo")))
                    Temp.IdPresentacion = If(IsDBNull(lRow("IdPresentacion")), 0, CInt(lRow("IdPresentacion")))
                    Temp.IdUnidadMedida = If(IsDBNull(lRow("IdUnidadMedida")), 0, CInt(lRow("IdUnidadMedida")))
                    Temp.IdProductoTallaColor = If(IsDBNull(lRow("IdProductoTallaColor")), 0, CInt(lRow("IdProductoTallaColor")))
                    Temp.IdProductoTallaColor_nuevo = If(IsDBNull(lRow("IdProductoTallaColor_nuevo")), 0, CInt(lRow("IdProductoTallaColor_nuevo")))


                    If lRow("cantidad_reconteo") > 0 Then
                        vCantidad = lRow("cantidad_reconteo")
                    Else
                        vCantidad = lRow("cantidad_conteo")
                    End If

                    Temp.Cantidad = vCantidad

                    If lRow("peso_reconteo") > 0 Then
                        vPeso = lRow("peso_reconteo")
                    Else
                        vPeso = lRow("peso_conteo")
                    End If

                    Temp.Peso = vPeso
                    Temp.LoteOrigen = lRow("LoteOrigen")
                    Temp.LoteDestino = lRow("LoteDestino")
                    Temp.EstadoOrigen = lRow("Estado")
                    Temp.EstadoDestino = lRow("EstadoDestino")
                    Temp.UbicacionOrigen = lRow("UbicacionOrigen")
                    Temp.UbicacionDestino = lRow("UbicacionDestino")
                    Temp.FechaVence = lRow("fecha_vence")
                    Temp.Licencia = lRow("Licencia")
                    Temp.TallaStock = lRow("Talla")
                    Temp.ColorStock = lRow("Color")
                    Temp.TallaNueva = lRow("TallaNueva")
                    Temp.ColorNuevo = lRow("ColorNuevo")
                    Temp.FechaVenceStock = lRow("fecha_vence_stock")
                    Temp.IdProductoEstado = lRow("IdProductoEstado")
                    Temp.IdUnidadMedida = lRow("IdUnidadMedida")
                    Temp.Cantidad_Reservada_UmBas = lRow("Cantidad_Reservada")

                    If Temp.Cantidad_Stock <> Temp.Cantidad Or rdStockInventarioMovs Then

                        If rdStockInventarioMovs Then

                            If ListaMovs.Count > 0 Then

                                vEntradasSalidas = 0

                                'cuando la ubicación es de picking se toma la ubicación origen
                                'cuando no es picking se toma la ubicación destino.
                                TempListaMovs = ListaMovs.FindAll(Function(x) x.IdProductoBodega = Temp.IdProductoBodega _
                                                                  And x.Lote = Temp.LoteDestino _
                                                                  And x.Fecha_Vence = Temp.FechaVence _
                                                                  And x.Lic_Plate = Temp.Licencia _
                                                                  And (((x.IdUbicacionOrigen = Temp.IdUbicacion) And
                                                                         x.IdTipoTarea <> clsDataContractDI.tTipoTarea.PIK And
                                                                         x.IdTipoTarea <> clsDataContractDI.tTipoTarea.DESP) _
                                                                    Or (((x.IdUbicacionOrigen = Temp.IdUbicacion AndAlso x.IdUbicacionOrigen = x.IdUbicacionDestino) OrElse
                                                                         (x.IdUbicacionDestino = Temp.IdUbicacion AndAlso x.IdUbicacionOrigen <> x.IdUbicacionDestino)) And
                                                                         x.IdTipoTarea = clsDataContractDI.tTipoTarea.DESP) _
                                                                    Or (x.IdUbicacionOrigen = Temp.IdUbicacion And x.IdTipoTarea = clsDataContractDI.tTipoTarea.PIK)))

                                lBeStockEnFecha.Clear()

                                For Each Mov In TempListaMovs

                                    BeStockEnFecha.Ingresos += Mov.Ingresos
                                    BeStockEnFecha.Salidas += Mov.Salidas
                                    BeStockEnFecha.Ajuste_Positivo += Mov.Ajustes_Positivos
                                    BeStockEnFecha.Ajuste_Negativo += Mov.Ajustes_Negativos
                                    BeStockEnFecha.EnMovimiento += Mov.EnMovimiento
                                    BeStockEnFecha.Fecha = Mov.Fecha
                                    BeStockEnFecha.Fecha_Conteo = Temp.Fec_Mod

                                    tmpBeStocnEnFecha = New clsBeStockEnUnaFecha
                                    clsPublic.CopyObject(Mov, tmpBeStocnEnFecha)

                                    lBeStockEnFecha.Add(tmpBeStocnEnFecha)

                                Next

                            End If

                            If lBeStockEnFecha.Count > 0 Then

                                lBeStockEnFechaFilter = lBeStockEnFecha.FindAll(Function(x) x.Fecha > x.Fecha_Conteo _
                                                                            AndAlso x.IdTipoTarea <> clsDataContractDI.tTipoTarea.VERI)

                                '#EJC20250515: Si lo contado es menor que el stock que debió haber existido, buscar si hubo un despacho o picking.
                                Dim lEntradas = lBeStockEnFechaFilter _
                                                   .Where(Function(x) (x.IdTipoTarea = clsDataContractDI.tTipoTarea.RECE OrElse x.IdTipoTarea = clsDataContractDI.tTipoTarea.PACK)) _
                                                   .Sum(Function(z) z.Ingresos)

                                Dim lAjustesPositivos = lBeStockEnFechaFilter _
                                                        .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTP) _
                                                        .Sum(Function(z) z.Ajustes_Positivos)

                                Dim lAjustesNegativos = lBeStockEnFechaFilter _
                                                        .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTN) _
                                                        .Sum(Function(z) z.Ajustes_Negativos)

                                Dim lDespachos = lBeStockEnFechaFilter _
                                                .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.DESP) _
                                                .Sum(Function(z) z.Salidas)

                                Dim lPickings = lBeStockEnFechaFilter _
                                                .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.PIK) _
                                                .Sum(Function(z) z.EnMovimiento)

                                Dim lCambioUbic = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.UBIC) _
                                                  .Sum(Function(z) z.EnMovimiento)

                                Dim lCambioUbicIng = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.UBIC) _
                                                  .Sum(Function(z) z.Ingresos)

                                Dim lCambioUbicSal = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.UBIC) _
                                                  .Sum(Function(z) z.Salidas)

                                Dim lCambioEst = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST) _
                                                  .Sum(Function(z) z.EnMovimiento)

                                Dim lCambioEstIng = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST) _
                                                  .Sum(Function(z) z.Ingresos)

                                Dim lCambioEstSal = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST) _
                                                  .Sum(Function(z) z.Salidas)

                                Dim lReemplazos = lBeStockEnFechaFilter _
                                                  .Where(Function(x) x.IdTipoTarea = clsDataContractDI.tTipoTarea.REEMP_BE_PICK _
                                                  OrElse x.IdTipoTarea = clsDataContractDI.tTipoTarea.REEMP_ME_PICK _
                                                  OrElse x.IdTipoTarea = clsDataContractDI.tTipoTarea.REEMP_NE_PICK) _
                                                  .Sum(Function(z) z.Salidas)

                                BeStockEnFecha.Ingresos = lEntradas + lCambioUbicIng + lCambioEstIng

                                If lDespachos > 0 Then
                                    lDespachos = lDespachos + lReemplazos + lCambioUbicSal + lCambioEstSal
                                    If Temp.Cantidad_Reservada_UmBas > 0 Then
                                        Temp.Cantidad_Reservada_UmBas = Temp.Cantidad_Reservada_UmBas - lDespachos
                                    End If
                                    BeStockEnFecha.Salidas = (lDespachos * -1)
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + (BeStockEnFecha.Salidas) + (BeStockEnFecha.EnMovimiento)) - (+BeStockEnFecha.Ajuste_Negativo)
                                ElseIf lPickings > 0 Then
                                    lPickings = lPickings + lReemplazos + lCambioUbicSal + lCambioEstSal
                                    If Temp.Cantidad_Reservada_UmBas > 0 Then
                                        Temp.Cantidad_Reservada_UmBas = Temp.Cantidad_Reservada_UmBas - lPickings
                                    End If
                                    BeStockEnFecha.Salidas = (lPickings * -1)
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + (BeStockEnFecha.Salidas)) - (BeStockEnFecha.Ajuste_Negativo) 'Quité (BeStockEnFecha.EnMovimiento * -1) y puse salidas
                                ElseIf lCambioUbic > 0 Then
                                    BeStockEnFecha.Salidas = lCambioUbic * -1
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + (BeStockEnFecha.EnMovimiento * -1)) - (+BeStockEnFecha.Ajuste_Negativo)
                                ElseIf lCambioUbicSal > 0 OrElse lCambioEstSal Then
                                    BeStockEnFecha.Salidas = (lCambioUbicSal * -1) + (lCambioEstSal * -1)
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + (BeStockEnFecha.Salidas)) - (+BeStockEnFecha.Ajuste_Negativo)
                                ElseIf lCambioEst > 0 Then
                                    BeStockEnFecha.Salidas = lCambioEst * -1
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + (BeStockEnFecha.EnMovimiento * -1)) - (+BeStockEnFecha.Ajuste_Negativo)
                                Else
                                    vEntradasSalidas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo + BeStockEnFecha.EnMovimiento) - (BeStockEnFecha.Salidas + BeStockEnFecha.Ajuste_Negativo)
                                End If

                            End If

                            Temp.Entradas = (BeStockEnFecha.Ingresos + BeStockEnFecha.Ajuste_Positivo)
                            Temp.Salidas = (BeStockEnFecha.Salidas + BeStockEnFecha.Ajuste_Negativo)

                        End If

                    Else
                        vEntradasSalidas = 0
                    End If

                    Temp.Entradas_Salidas = vEntradasSalidas

                    Dim vDiferencia As Double = ((Temp.Cantidad_Stock + Temp.Entradas_Salidas) - Temp.Cantidad)

                    If Temp.TieneReservaYConteoInsuficiente Then
                        Temp.Observacion = "Conteo menor que la cantidad reservada. No regularizar."
                    End If

                    If vDiferencia = 0 Then
                        Dim oBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico
                        Copiar_Temp(oBeTrans_inv_ciclico, Temp)
                        oBeTrans_inv_ciclico.Regularizar = False
                        Actualizar_Regularizar_By_IdInventarioEnc_And_IdInvCiclico(oBeTrans_inv_ciclico, lConnection, lTransaction)
                    Else
                        If Temp.Cantidad = Temp.Cantidad_Stock AndAlso Temp.Entradas_Salidas <> 0 Then
                            Dim oBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico
                            Copiar_Temp(oBeTrans_inv_ciclico, Temp)
                            oBeTrans_inv_ciclico.Nuevo_Stock = Math.Abs(Temp.Cantidad_Stock + Temp.Entradas_Salidas)
                            Actualizar_NuevoStock_By_IdInventarioEnc_And_IdInvCiclico(oBeTrans_inv_ciclico, Temp.Entradas_Salidas, lConnection, lTransaction)
                        ElseIf Temp.Entradas_Salidas <> 0 Then
                            Dim oBeTrans_inv_ciclico As New clsBeTrans_inv_ciclico
                            Copiar_Temp(oBeTrans_inv_ciclico, Temp)
                            oBeTrans_inv_ciclico.Nuevo_Stock = Math.Abs(Temp.Cantidad_Stock + Temp.Entradas_Salidas)
                            Actualizar_NuevoStock_By_IdInventarioEnc_And_IdInvCiclico(oBeTrans_inv_ciclico, Temp.Entradas_Salidas, lConnection, lTransaction)
                        End If
                    End If

                    If Temp.Cantidad_Reservada_UmBas > 0 AndAlso Temp.Cantidad < Temp.Cantidad_Reservada_UmBas Then
                        Temp.TieneReservaYConteoInsuficiente = True
                    Else
                        Temp.TieneReservaYConteoInsuficiente = False
                    End If

                    clsLnTempComparacionInventario.Insertar(Temp, lConnection, lTransaction)

                    prg.Value = vContador

                    vContador += 1

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Actualiza_Inventario_Ciclico(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, ByVal pBeProducto As clsBeProducto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")
            Upd.Add("idinvciclico", "@idinvciclico", DataType.Parametro)
            Upd.Add("idinventarioenc", "@idinventarioenc", DataType.Parametro)
            Upd.Add("idstock", "@idstock", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("esnuevo", "@esnuevo", DataType.Parametro)

            If pBeProducto.Control_lote Then
                Upd.Add("lote_stock", "@lote_stock", DataType.Parametro)
                Upd.Add("lote", "@lote", DataType.Parametro)
            End If

            If pBeProducto.Control_vencimiento Then
                Upd.Add("fecha_vence_stock", "@fecha_vence_stock", DataType.Parametro)
                Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            End If

            Upd.Add("cant_stock", "@cant_stock", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("cant_reconteo", "@cant_reconteo", DataType.Parametro)
            Upd.Add("peso_stock", "@peso_stock", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("peso_reconteo", "@peso_reconteo", DataType.Parametro)
            Upd.Add("idoperador", "@idoperador", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("IdProductoEst_nuevo", "@IdProductoEst_nuevo", DataType.Parametro)
            Upd.Add("IdPresentacion_nuevo", "@IdPresentacion_nuevo", DataType.Parametro)
            Upd.Add("IdUbicacion_nuevo", "@IdUbicacion_nuevo", DataType.Parametro)
            Upd.Add("espallet", "@espallet", DataType.Parametro)
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Where("idinvciclico = @idinvciclico")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", oBeTrans_inv_ciclico.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeTrans_inv_ciclico.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@ESNUEVO", oBeTrans_inv_ciclico.EsNuevo))

            If pBeProducto.Control_lote Then
                cmd.Parameters.Add(New SqlParameter("@LOTE_STOCK", IIf(oBeTrans_inv_ciclico.Lote_stock.Trim = "", "", oBeTrans_inv_ciclico.Lote_stock.Trim)))
                cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico.Lote))
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_STOCK", oBeTrans_inv_ciclico.Fecha_vence_stock))
                cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_ciclico.Fecha_vence))
            End If

            cmd.Parameters.Add(New SqlParameter("@CANT_STOCK", oBeTrans_inv_ciclico.Cant_stock))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@CANT_RECONTEO", oBeTrans_inv_ciclico.Cant_reconteo))
            cmd.Parameters.Add(New SqlParameter("@PESO_STOCK", oBeTrans_inv_ciclico.Peso_stock))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_inv_ciclico.Peso))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECONTEO", oBeTrans_inv_ciclico.Peso_reconteo))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_inv_ciclico.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_inv_ciclico.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOEST_NUEVO", IIf(oBeTrans_inv_ciclico.IdProductoEst_nuevo = 0, 0, oBeTrans_inv_ciclico.IdProductoEst_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdPresentacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdPresentacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdUbicacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdUbicacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeTrans_inv_ciclico.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_inv_ciclico.lic_plate))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeTrans_inv_ciclico.IdUnidadMedida))


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

    '#AT20241205 Actualizar inventario ciclico por idinvciclico 
    Public Shared Function Act_Inventario_Ciclico(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, ByVal pBeProducto As clsBeProducto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")

            If pBeProducto.Control_lote Then
                Upd.Add("lote_stock", "@lote_stock", DataType.Parametro)
                Upd.Add("lote", "@lote", DataType.Parametro)
            End If

            If pBeProducto.Control_vencimiento Then
                Upd.Add("fecha_vence_stock", "@fecha_vence_stock", DataType.Parametro)
                Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            End If

            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("IdPresentacion_nuevo", "@IdPresentacion_nuevo", DataType.Parametro)
            Upd.Add("IdProductoEst_nuevo", "@IdProductoEst_nuevo", DataType.Parametro)
            Upd.Add("IdUbicacion_nuevo", "@IdUbicacion_nuevo", DataType.Parametro)
            Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Upd.Add("IdProductoTallaColor_nuevo", "@IdProductoTallaColor_nuevo", DataType.Parametro)
            Upd.Add("gondola", "@gondola", DataType.Parametro)
            Upd.Add("lic_plate", "@licencia", DataType.Parametro)
            Upd.Add("contado", "@contado", DataType.Parametro)
            Upd.Where("idinvciclico = @idinvciclico")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))

            If pBeProducto.Control_lote Then
                cmd.Parameters.Add(New SqlParameter("@LOTE_STOCK", IIf(oBeTrans_inv_ciclico.Lote_stock.Trim = "", "", oBeTrans_inv_ciclico.Lote_stock.Trim)))
                cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_inv_ciclico.Lote))
            End If

            If pBeProducto.Control_vencimiento Then
                cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE_STOCK", oBeTrans_inv_ciclico.Fecha_vence_stock))
                cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_ciclico.Fecha_vence))
            End If

            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdPresentacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdPresentacion_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOEST_NUEVO", IIf(oBeTrans_inv_ciclico.IdProductoEst_nuevo = 0, 0, oBeTrans_inv_ciclico.IdProductoEst_nuevo)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION_NUEVO", IIf(oBeTrans_inv_ciclico.IdUbicacion_nuevo = 0, 0, oBeTrans_inv_ciclico.IdUbicacion_nuevo)))

            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@CONTADO", oBeTrans_inv_ciclico.Contado))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_inv_ciclico.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR_NUEVO", oBeTrans_inv_ciclico.IdProductoTallaColor_nuevo))
            cmd.Parameters.Add(New SqlParameter("@GONDOLA",
                If(oBeTrans_inv_ciclico.Gondola Is Nothing, "", oBeTrans_inv_ciclico.Gondola.Trim())
            ))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_inv_ciclico.lic_plate))

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


    Public Shared Function Actualiza_Inventario_Ciclico_Reconteo(ByVal idinvreconteo As Integer, ByVal pCantidad_Reconteo As Decimal, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Try

            Upd.Init("trans_inv_reconteo")
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Where("idinvreconteo = @idinvreconteo")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVRECONTEO", idinvreconteo))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", pCantidad_Reconteo))

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

    Public Shared Function Existe_Producto_By_IdOperador(ByVal IdOperador As Integer,
                                                         ByVal IdInventario As Integer,
                                                         ByVal IdProductoBodega As Integer) As Boolean

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico 
                                  WHERE idinventarioenc = @IdInventarioEnc AND 
                                        idoperador = @IdOperador AND IdProductoBodega = @IdProductoBodega "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProductoBodega_And_IdUbicacion(ByVal pIdInventarioEnc As Integer,
                                                                       ByVal pIdProductoBodega As Integer,
                                                                       ByVal pIdUbicacion As Integer) As List(Of clsBeTrans_inv_ciclico)

        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT  *
                                      FROM  trans_inv_ciclico 
                                      WHERE IdProductoBodega = @IdProductoBodega AND 
                                            IdInventarioEnc = @IdInventarioEnc AND
                                            IdUbicacion = @IdUbicacion"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransInvCiclico As clsBeTrans_inv_ciclico

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                BeTransInvCiclico = New clsBeTrans_inv_ciclico()
                                Cargar(BeTransInvCiclico, lRow)
                                lReturnList.Add(BeTransInvCiclico)
                            Next

                        End If


                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return lReturnList

    End Function

    Public Shared Function Eliminar_By_IdOperador_And_IdProductoBodega_And_IdUbicacion(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico,
                                                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico
                                   Where(idoperador = @idoperador AND 
                                         idinventarioenc=@idinventarioenc AND 
                                         IdUbicacion=@IdUbicacion AND 
                                         IdProductoBodega=@IdProductoBodega )"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))

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

    Public Shared Function Existe_Producto_By_IdOperador(ByVal IdOperador As Integer,
                                                         ByVal IdInventario As Integer,
                                                         ByVal IdProductoBodega As Integer,
                                                         lConnection As SqlConnection,
                                                         lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico 
                                  WHERE idinventarioenc = @IdInventarioEnc AND 
                                        idoperador = @IdOperador AND IdProductoBodega = @IdProductoBodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer,
                                                                        ByVal IdBodega As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Get_All_BeTransInvCiclico_By_IdInventarioEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT " &
                                    "  MAX(trans_inv_ciclico.Idinvciclico) as Idinvciclico, " &
                                    "  bodega_ubicacion.IdUbicacion, " &
                                    "  bodega_ubicacion.descripcion AS Ubicacion, " &
                                    "  bodega_tramo.descripcion AS Tramo, " &
                                    "  dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Nombre_Completo, " &
                                    "  trans_inv_ciclico.IdStock, " &
                                    "  producto.codigo AS Codigo, " &
                                    "  producto.nombre AS Producto, " &
                                    "  ISNULL(producto_presentacion.nombre, '') AS Presentacion, " &
                                    "  trans_inv_ciclico.lote, " &
                                    "  trans_inv_ciclico.lote_stock, " &
                                    "  producto_estado.nombre AS Estado, " &
                                    "  SUM(trans_inv_ciclico.cantidad) AS Cantidad_Ciclico, " &
                                    "  SUM(trans_inv_ciclico.peso) AS Peso_Ciclico, " &
                                    "  producto.IdPropietario, " &
                                    "  producto.IdClasificacion, " &
                                    "  producto.IdFamilia, " &
                                    "  producto_estado.IdEstado, " &
                                    "  trans_inv_ciclico.EsNuevo, " &
                                    "  bodega_tramo.IdTramo, " &
                                    "  trans_inv_ciclico.fecha_vence, " &
                                    "  trans_inv_ciclico.idinventarioenc, " &
                                    "  trans_inv_ciclico.IdProductoBodega, " &
                                    "  trans_inv_ciclico.EsPallet, " &
                                    "  trans_inv_ciclico.lic_plate, " &
                                    "  trans_inv_ciclico.IdPresentacion, " &
                                    "  trans_inv_ciclico.fecha_vence_stock, " &
                                    "  MAX(CASE WHEN trans_inv_ciclico.contado = 1 THEN ISNULL(trans_inv_ciclico.IdPresentacion_nuevo, 0) ELSE 0 END) AS IdPresentacion_nuevo, " &
                                    "  MAX(CASE WHEN trans_inv_ciclico.contado = 1 AND ISNULL(trans_inv_ciclico.IdPresentacion_nuevo, 0) = 0 THEN 1 ELSE 0 END) AS Conteo_En_Unidad, " &
                                    "  trans_inv_ciclico.peso_stock AS Peso_Stock, " &
                                    "  trans_inv_ciclico.cant_stock AS Cantidad_Stock, " &
                                    "  trans_inv_ciclico.peso_reconteo, " &
                                    "  producto_tipo.NombreTipoProducto, " &
                                    "  producto.IdProducto, " &
                                    "  ISNULL(producto_presentacion.factor, 1) AS Factor, " &
                                    "  dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS Nombre_Completo, " &
                                    "  dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.idubicacion_nuevo, bodega_ubicacion.IdBodega) AS Nombre_Completo_Destino, " &
                                    "  trans_inv_ciclico.IdProductoEst_nuevo, " &
                                    "  trans_inv_ciclico.lote, " &
                                    "  trans_inv_ciclico.fecha_vence, " &
                                    "  trans_inv_ciclico.IdUbicacion_nuevo, " &
                                    "  e1.Nombre as Estado_Nuevo, " &
                                    "  MAX(ISNULL(trans_inv_ciclico.Cantidad_Reservada_UMBas,0)) AS Cantidad_Reservada_UMBas, " &
                                    "  SUM(CASE WHEN trans_inv_ciclico.contado = 1 THEN 1 ELSE 0 END) AS Contado, " &
                                    "  ISNULL(um.Nombre,'') AS UmBas, " &
                                    "  ISNULL(color.nombre, '') AS Color, " &
                                    "  ISNULL(talla.codigo, '') AS Talla, " &
                                    "  ISNULL(cn.nombre, '') AS Color_Nuevo, " &
                                    "  ISNULL(tn.codigo, '') AS Talla_Nueva, " &
                                    "  MAX(ISNULL(trans_inv_ciclico.gondola, '')) AS Gondola " &
                                    "FROM trans_inv_ciclico " &
                                    "  INNER JOIN producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega " &
                                    "  INNER JOIN producto ON producto_bodega.IdProducto = producto.IdProducto " &
                                    "  INNER JOIN bodega ON producto_bodega.IdBodega = bodega.IdBodega " &
                                    "  LEFT OUTER JOIN bodega_tramo " &
                                    "     INNER JOIN bodega_ubicacion ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo " &
                                    "        AND bodega_tramo.IdBodega = bodega_ubicacion.IdBodega " &
                                    "        AND bodega_tramo.IdArea = bodega_ubicacion.IdArea " &
                                    "        AND bodega_tramo.IdSector = bodega_ubicacion.IdSector " &
                                    "     ON bodega.IdBodega = bodega_tramo.IdBodega " &
                                    "     AND trans_inv_ciclico.IdUbicacion = bodega_ubicacion.IdUbicacion " &
                                    "  LEFT OUTER JOIN producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto " &
                                    "  LEFT OUTER JOIN operador ON trans_inv_ciclico.idoperador = operador.IdOperador " &
                                    "  LEFT OUTER JOIN producto_estado ON trans_inv_ciclico.IdProductoEstado = producto_estado.IdEstado " &
                                    "  LEFT OUTER JOIN producto_estado e1 ON trans_inv_ciclico.IdProductoEst_Nuevo = e1.IdEstado " &
                                    "  LEFT OUTER JOIN producto_presentacion ON trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion " &
                                    "  LEFT JOIN unidad_medida um ON trans_inv_ciclico.IdUnidadMedida = um.IdUnidadMedida " &
                                    "  LEFT JOIN producto_talla_color ptc_act ON trans_inv_ciclico.IdProductoTallaColor = ptc_act.IdProductoTallaColor " &
                                    "  LEFT JOIN color ON color.IdColor = ptc_act.IdColor " &
                                    "  LEFT JOIN talla ON talla.IdTalla = ptc_act.IdTalla " &
                                    "  LEFT JOIN producto_talla_color ptc_new ON trans_inv_ciclico.IdProductoTallaColor_nuevo = ptc_new.IdProductoTallaColor " &
                                    "  LEFT JOIN color cn ON cn.IdColor = ptc_new.IdColor " &
                                    "  LEFT JOIN talla tn ON tn.IdTalla = ptc_new.IdTalla " &
                                    "WHERE (trans_inv_ciclico.idinventarioenc = @idinventario) " &
                                    "  AND (trans_inv_ciclico.IdBodega = @IdBodega) " &
                                    "GROUP BY " &
                                    "  bodega_ubicacion.IdUbicacion, bodega_ubicacion.descripcion, bodega_tramo.descripcion, trans_inv_ciclico.IdStock, " &
                                    "  producto.codigo, producto.nombre, producto_presentacion.nombre, trans_inv_ciclico.lote, producto_estado.nombre, e1.nombre, " &
                                    "  producto.IdPropietario, producto.IdClasificacion, producto.IdFamilia, producto_estado.IdEstado, trans_inv_ciclico.EsNuevo, " &
                                    "  bodega_tramo.IdTramo, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.idinventarioenc, trans_inv_ciclico.IdProductoBodega, " &
                                       "  trans_inv_ciclico.EsPallet, trans_inv_ciclico.lic_plate, trans_inv_ciclico.lote_stock, trans_inv_ciclico.IdPresentacion, " &
        "  trans_inv_ciclico.fecha_vence_stock, trans_inv_ciclico.peso_stock, trans_inv_ciclico.cant_stock, trans_inv_ciclico.peso_reconteo, " &
                                    "  bodega_tramo.es_rack, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, " &
                                    "  producto_tipo.NombreTipoProducto, producto.IdProducto, producto_presentacion.factor, bodega_ubicacion.IdBodega, " &
                                    "  trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.IdUbicacion_nuevo, " &
                                    "  color.nombre, talla.codigo, cn.nombre, tn.codigo, um.Nombre " &
                                    "ORDER BY Tramo, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)


                Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

                For Each lRow As DataRow In lDataTable.Rows

                    BeTransInvCiclico = New clsBeTrans_inv_ciclico()

                    BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

                    If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                        BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
                    End If

                    If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
                        BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
                    End If

                    If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                        BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
                    End If

                    If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                        BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                    End If

                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                        BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                    End If
                    If lRow("IdPresentacion_nuevo") IsNot DBNull.Value AndAlso lRow("IdPresentacion_nuevo") IsNot Nothing Then
                        BeTransInvCiclico.IdPresentacion_nuevo = CType(lRow("IdPresentacion_nuevo"), Integer)
                    End If


                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                        BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                    End If

                    If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                        BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
                    End If

                    If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
                        BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
                    End If

                    If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                        BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
                    End If

                    If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                        BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
                    End If

                    If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                        BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
                    End If

                    If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                        BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
                    End If

                    If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                        BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
                        BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
                    End If

                    If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                        BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
                    End If

                    If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                        BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                    End If

                    If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                        BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
                    End If

                    If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                        BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
                    End If

                    If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
                        BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
                    End If

                    If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                        BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
                    End If

                    If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
                        BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
                    End If

                    If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                        BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
                    End If

                    If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                        BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
                    End If

                    If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                        BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                    End If

                    If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                        BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
                    End If

                    If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                        BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
                    End If

                    If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
                        BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
                    End If

                    If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                        BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
                    End If

                    If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                        BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                    End If

                    If lRow("IdProductoEst_nuevo") IsNot DBNull.Value AndAlso lRow("IdProductoEst_nuevo") IsNot Nothing Then
                        BeTransInvCiclico.IdProductoEst_nuevo = CType(lRow("IdProductoEst_nuevo"), String)
                    End If

                    If lRow("IdUbicacion_nuevo") IsNot DBNull.Value AndAlso lRow("IdUbicacion_nuevo") IsNot Nothing Then
                        BeTransInvCiclico.IdUbicacion_nuevo = CType(lRow("IdUbicacion_nuevo"), String)
                    End If

                    If lRow("Color") IsNot DBNull.Value AndAlso lRow("Color") IsNot Nothing Then
                        BeTransInvCiclico.Color = CType(lRow("Color"), String)
                    End If

                    If lRow("Talla") IsNot DBNull.Value AndAlso lRow("Talla") IsNot Nothing Then
                        BeTransInvCiclico.Talla = CType(lRow("Talla"), String)
                    End If


                    If lRow("Gondola") IsNot DBNull.Value AndAlso lRow("Gondola") IsNot Nothing Then
                        BeTransInvCiclico.Gondola = CType(lRow("Gondola"), String)
                    End If

                    If lRow("UmBas") IsNot DBNull.Value AndAlso lRow("UmBas") IsNot Nothing Then
                        BeTransInvCiclico.UmBas = CType(lRow("UmBas"), String)
                    End If

                    BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
                    BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
                    BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
                    BeTransInvCiclico.Estado = IIf(IsDBNull(lRow("Estado")), "", lRow("Estado"))
                    BeTransInvCiclico.Ubicacion_Nueva = IIf(IsDBNull(lRow("Nombre_Completo_Destino")), "", lRow("Nombre_Completo_Destino"))
                    BeTransInvCiclico.EstadoNuevo = IIf(IsDBNull(lRow("Estado_Nuevo")), "", lRow("Estado_Nuevo"))
                    BeTransInvCiclico.Cantidad_Reservada_UMBas = IIf(IsDBNull(lRow("Cantidad_Reservada_UMBas")), 0, lRow("Cantidad_Reservada_UMBas"))
                    BeTransInvCiclico.Contado = IIf(IsDBNull(lRow("Contado")), False, lRow("Contado"))

                    lReturnList.Add(BeTransInvCiclico)

                Next

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT17012025: cargar inventario ciclico pero sin las agrupaciones de tipo, operador, producto, para la cumbre
    Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_Diferencias(ByVal pIdInventarioEnc As Integer,
                                                                                    ByVal IdBodega As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Get_All_BeTransInvCiclico_By_IdInventarioEnc_Diferencias = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)
            '#MA20280515 Agregar talla y color y eliminar columnas repetidas
            '#CKFK20250206 Por una mejora que se hizo en el ingreso de los datos, ya se pueden sumar la cantidades contadas
            'y las cantidades del stock, quité ambas del group by
            Dim vSQL As String = "SELECT t.idinventarioenc,
                                         t.Codigo,
                                         t.Producto,
                                         MAX(t.TipoProducto) As TipoProducto,
                                         SUM(t.Cantidad_Stock)  As Cantidad_Stock_UMBAS, 
                                         SUM(t.Cantidad_Ciclico) As Cantidad_Ciclico_UMBAS,
                                         t.IdProducto,
                                         t.IdProductoBodega, 
                                         SUM(t.Cantidad_Reservada) As Cantidad_Reservada,
                                         MAX(t.IdPresentacion) As IdPresentacion,
                                         MAX(t.Factor) As Factor,
                                         Case 
                                           WHEN MAX(ISNULL(t.IdPresentacion,0)) > 0 
                                             THEN MAX(t.Presentacion)
                                              Else MAX(t.UMBas)
                                          End As Presentacion,
                                          MAX(t.UMBas) As UMBas,
                                          MAX(t.Talla) As Talla,
                                          MAX(t.Color) As Color,
                                         SUM(t.Cantidad_Stock)   AS Cant_Stock_UMBAS,
                                         SUM(t.Cantidad_Ciclico) AS Cant_Conteo_UMBAS,
                                            ROUND(
                                                CASE 
                                                    WHEN MAX(t.IdPresentacion) > 0
                                                        THEN SUM(t.Cantidad_Stock / t.Factor)
                                                    ELSE SUM(t.Cantidad_Stock)
                                                END, 6
                                            ) AS Cant_Teorica_Presentacion,
                                            ROUND(
                                            CASE 
                                                WHEN MAX(t.IdPresentacion) > 0
                                                    THEN SUM(t.Cantidad_Ciclico / t.Factor)
                                                ELSE SUM(t.Cantidad_Ciclico)
                                            END, 6
                                        ) AS Cant_Conteo_Presentacion,
                                          
                                    SUM(
                                        ROUND(
                                            CASE 
                                                WHEN t.IdPresentacion > 0
                                                    THEN (t.Cantidad_Stock / t.Factor)
                                                         - (t.Cantidad_Ciclico / t.Factor)
                                                ELSE 
                                                    t.Cantidad_Stock - t.Cantidad_Ciclico
                                            END, 6
                                        )
                                    ) AS Dif_Cant_Presentacion
                                        FROM(
                                            SELECT 
                                                trans_inv_ciclico.idinventarioenc,
                                                producto.codigo As Codigo, 
                                                producto.nombre AS Producto,
                                                MAX(trans_inv_ciclico.cant_stock) As Cantidad_Stock, 
                                                SUM(trans_inv_ciclico.cantidad) As Cantidad_Ciclico, 
                                                producto.IdProducto,
                                                trans_inv_ciclico.IdProductoBodega,
                                                trans_inv_ciclico.IdStock,
                                                MAX(trans_inv_ciclico.cantidad_reservada_umbas) As Cantidad_Reservada,
                                                MAX(ISNULL(trans_inv_ciclico.IdPresentacion, 0)) As IdPresentacion,
                                                MAX(ISNULL(producto_presentacion.Factor, 1)) As Factor,
                                                MAX(unidad_medida.Nombre) As UMBas,
                                                MAX(talla.Nombre) As Talla,
                                                MAX(color.Nombre) As Color,
                                                MAX(producto_presentacion.nombre) As Presentacion,
                                                MAX(producto_tipo.NombreTipoProducto) As TipoProducto
                                            From trans_inv_ciclico 
                                            INNER Join producto_bodega 
                                                On trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega 
                                            INNER Join producto 
                                                On producto_bodega.IdProducto = producto.IdProducto 
                                            Left Join producto_tipo 
                                                On producto.IdTipoProducto = producto_tipo.IdTipoProducto 
                                            Left Join producto_presentacion 
                                                On trans_inv_ciclico.IdPresentacion = producto_presentacion.IdPresentacion 
                                            Left Join unidad_medida 
                                                On producto.IdUnidadMedidaBasica = unidad_medida.IdUnidadMedida
                                            LEFT JOIN producto_talla_color 
                                            ON trans_inv_ciclico.IdProductoTallaColor = producto_talla_color.IdProductoTallaColor
                                           LEFT JOIN talla 
                                            ON talla.IdTalla = producto_talla_color.IdTalla
                                           LEFT JOIN color 
                                            ON color.IdColor = producto_talla_color.IdColor
                                            WHERE trans_inv_ciclico.idinventarioenc = @idinventario 
                                              And trans_inv_ciclico.IdBodega = @IdBodega 
                                            GROUP BY 
                                                producto.codigo,
                                                trans_inv_ciclico.idinventarioenc,
                                                producto.nombre,
                                                producto.IdProducto,
                                                trans_inv_ciclico.IdProductoBodega,
                                                trans_inv_ciclico.IdStock) T
                                        GROUP BY 
                                            t.idinventarioenc,
                                            t.codigo,
                                            t.Producto,
                                            t.IdProducto,
                                            t.IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)


                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

                For Each lRow As DataRow In lDataTable.Rows

                    BeTransInvCiclico = New clsBeTrans_inv_ciclico()

                    'BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

                    If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                        BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                    End If

                    If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                        BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
                    End If

                    If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                        BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
                    End If

                    If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                        BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
                    End If

                    If lRow("UmBas") IsNot DBNull.Value AndAlso lRow("UmBas") IsNot Nothing Then
                        BeTransInvCiclico.UnidadMedida = CType(lRow("UMBas"), String)
                    End If

                    If lRow("Talla") IsNot DBNull.Value Then
                        BeTransInvCiclico.Talla = lRow("Talla").ToString()
                    End If

                    If lRow("Color") IsNot DBNull.Value Then
                        BeTransInvCiclico.Color = lRow("Color").ToString()
                    End If

                    If lRow("TipoProducto") IsNot DBNull.Value AndAlso lRow("TipoProducto") IsNot Nothing Then
                        BeTransInvCiclico.TipoProducto = CType(lRow("TipoProducto"), String)
                    End If

                    BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                    BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
                    BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
                    BeTransInvCiclico.Cantidad_Reservada_UMBas = IIf(IsDBNull(lRow("Cantidad_Reservada")), 0, lRow("Cantidad_Reservada"))
                    BeTransInvCiclico.Cant_stock = If(IsDBNull(lRow("Cant_Teorica_Presentacion")), 0, CDbl(lRow("Cant_Teorica_Presentacion")))
                    BeTransInvCiclico.Cantidad = If(IsDBNull(lRow("Cant_Conteo_Presentacion")), 0, CDbl(lRow("Cant_Conteo_Presentacion")))

                    lReturnList.Add(BeTransInvCiclico)

                Next

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProductoBodega_And_IdUbicacion(ByVal pIdInventarioEnc As Integer,
                                                                       ByVal pIdProductoBodega As Integer,
                                                                       ByVal pIdUbicacion As Integer,
                                                                       ByVal pIdOperador As Integer) As List(Of clsBeTrans_inv_ciclico)

        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT  *
                                      From  trans_inv_ciclico 
                                      WHERE IdProductoBodega = @IdProductoBodega AND 
                                            IdInventarioEnc = @IdInventarioEnc AND
                                            IdUbicacion = @IdUbicacion AND
                                            IdOperador = @IdOperador "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransInvCiclico As clsBeTrans_inv_ciclico

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                BeTransInvCiclico = New clsBeTrans_inv_ciclico()
                                Cargar(BeTransInvCiclico, lRow)
                                lReturnList.Add(BeTransInvCiclico)
                            Next

                        End If


                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return lReturnList

    End Function

    Public Shared Function Eliminar_By_IdOperador_And_IdUbicacion(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico,
                                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico
                                   Where(idoperador = @idoperador AND 
                                         idinventarioenc=@idinventarioenc AND 
                                         IdUbicacion=@IdUbicacion )"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDOPERADOR", oBeTrans_inv_ciclico.Idoperador))
            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))

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

    Public Shared Function Existe_Producto_By_IdOperador(ByVal IdOperador As Integer,
                                                         ByVal IdInventario As Integer,
                                                         ByVal IdProductoBodega As Integer,
                                                         ByVal IdUbicacion As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_Producto_By_IdOperador = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico 
                                  WHERE idinventarioenc = @IdInventarioEnc AND 
                                        idoperador = @IdOperador AND IdProductoBodega = @IdProductoBodega AND IdUbicacion = @IdUbicacion"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_Producto_By_IdOperador = dt.Rows.Count > 0

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_BeTransInvCiclico_By_IdUbicacion_And_IdOperador(ByVal pIdInventario As Integer,
                                                                                   ByVal pIdOperador As Integer,
                                                                                   ByVal pIdUbicacion As Integer) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM trans_inv_ciclico
                                      WHERE (trans_inv_ciclico.idinventarioenc = @idinventario AND
                                             trans_inv_ciclico.IdOperador = @IdOperador AND 
                                             trans_inv_ciclico.IdUbicacion = @IdUbicacion )"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_inv_ciclico

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_inv_ciclico()
                            Cargar(Obj, lRow)
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

    ''' <summary>
    ''' #EJC20240827: El día que teco temió
    ''' </summary>
    ''' <param name="pIdInvEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdInventarioEnc(pIdInvEnc As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT * FROM Trans_inv_ciclico WHERE idinventarioenc=@idinventarioenc Order by IdProductoBodega"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInvEnc)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                Dim vBeTrans_inv_detalle As New clsBeTrans_inv_ciclico

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_detalle = New clsBeTrans_inv_ciclico
                    Cargar(vBeTrans_inv_detalle, dr)
                    If vBeTrans_inv_detalle.Nuevo_Stock <> 0 Then
                        vBeTrans_inv_detalle.Cant_stock = vBeTrans_inv_detalle.Nuevo_Stock
                    End If
                    lReturnList.Add(vBeTrans_inv_detalle)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Producto_By_IdOperador(ByVal IdOperador As Integer,
                                                         ByVal IdInventario As Integer,
                                                         ByVal IdProductoBodega As Integer,
                                                         ByVal IdStock As Integer,
                                                         ByVal IdUbicacion As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Producto_By_IdOperador = False

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico 
                                  WHERE idinventarioenc = @IdInventarioEnc AND 
                                        idoperador = @IdOperador AND 
                                        IdProductoBodega = @IdProductoBodega AND 
                                        IdUbicacion = @IdUbicacion AND
                                        IdStock = @IdStock"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", IdUbicacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_Producto_By_IdOperador = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProductoBodega_And_IdUbicacion(ByVal pIdInventarioEnc As Integer,
                                                                       ByVal pIdProductoBodega As Integer,
                                                                       ByVal pIdUbicacion As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

        Try

            Dim vSQL As String = "SELECT  *
                                      FROM  trans_inv_ciclico 
                                      WHERE IdProductoBodega = @IdProductoBodega AND 
                                            IdInventarioEnc = @IdInventarioEnc AND
                                            IdUbicacion = @IdUbicacion"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioEnc", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransInvCiclico As clsBeTrans_inv_ciclico

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        BeTransInvCiclico = New clsBeTrans_inv_ciclico()
                        Cargar(BeTransInvCiclico, lRow)
                        lReturnList.Add(BeTransInvCiclico)
                    Next

                End If


            End Using


        Catch ex As Exception
            Throw ex
        End Try

        Return lReturnList

    End Function

    Public Shared Function Get_All_Conteos_By_IdInventarioEnc_And_Operador(ByVal IdInventarioEnc As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Conteos_By_IdInventarioEnc_And_Operador = Nothing

        Try
            '#MA20280515 Agregar talla y color y revisar productos duplicados
            '#MA20260204 agregar columnas de presentacion
            Dim vSQL As String = "SELECT DISTINCT
                                v.IdInventarioenc,
                                v.Ubicacion,
                                v.IdStock,
                                v.lic_plate,
                                v.Codigo,
                                v.CodigoBarra,
                                v.Nombre,
                                v.Operador,

                                Case 
                                    WHEN tic.IdPresentacion > 0 THEN pp.Nombre
                                    Else u.Nombre
                                End As Presentacion,

                                talla.Nombre AS Talla,
                                color.Nombre As Color,

                                Case 
                                    WHEN tic.IdPresentacion > 0 
                                        THEN v.Teorico / pp.factor
                                    Else v.Teorico
                                End As Teorico_Pres,

                                Case 
                                    WHEN tic.IdPresentacion > 0 
                                        THEN v.Conteo / pp.factor
                                    Else v.Conteo
                                End As Conteo_Pres

                            From VW_Conteo_By_Operador v

                            Left Join producto p
                                   On p.codigo = v.Codigo

                            Left Join unidad_medida u
                                   On p.IdUnidadMedidaBasica = u.IdUnidadMedida

                            Left Join trans_inv_ciclico tic
                                   On tic.IdStock = v.IdStock
                                  And tic.IdInventarioenc = v.IdInventarioenc

                            Left Join producto_presentacion pp
                                   On pp.IdPresentacion = tic.IdPresentacion
                                  And pp.IdProducto = p.IdProducto

                            Left Join producto_talla_color ptc
                                   On ptc.IdProductoTallaColor = tic.IdProductoTallaColor

                            Left Join talla
                                   On talla.IdTalla = ptc.IdTalla

                            Left Join color
                                   On color.IdColor = ptc.IdColor

                            WHERE v.IdInventarioenc = @IdInventarioenc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdInventarioenc", IdInventarioEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw New Exception("BodegaArea_getAllAreaByBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function Eliminar_By_IdUbicacion(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_inv_ciclico
                                   Where(idinventarioenc=@idinventarioenc 
                                   AND IdUbicacion=@IdUbicacion )"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeTrans_inv_ciclico.IdUbicacion))

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

    Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer,
                                                                        ByVal IdBodega As Integer) As List(Of clsBeTrans_inv_ciclico)

        Get_All_BeTransInvCiclico_By_IdInventarioEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT * from Vw_inv_ciclico
                                    WHERE  (idinventarioenc = @idinventario) AND (IdBodega = @IdBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

                        For Each lRow As DataRow In lDataTable.Rows

                            BeTransInvCiclico = New clsBeTrans_inv_ciclico()

                            BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

                            If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
                            End If

                            If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
                                BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
                            End If

                            If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
                            End If

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                            End If

                            If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
                            End If

                            If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
                                BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                                BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
                            End If

                            If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                                BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
                                BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
                            End If

                            If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
                            End If

                            If lRow("EstadoNuevo") IsNot DBNull.Value AndAlso lRow("EstadoNuevo") IsNot Nothing Then
                                BeTransInvCiclico.EstadoNuevo = CType(lRow("EstadoNuevo"), String)
                            End If

                            If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                            End If

                            If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
                            End If

                            If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                                BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
                            End If

                            If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
                                BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
                            End If

                            If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                                BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
                            End If

                            If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
                                BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
                            End If

                            If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
                            End If

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
                            End If

                            If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                                BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                            End If

                            If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                                BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
                            End If

                            If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                                BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
                            End If

                            If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
                                BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
                            End If

                            If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
                            End If

                            If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                                BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                            End If

                            If lRow("IdProductoEst_nuevo") IsNot DBNull.Value AndAlso lRow("IdProductoEst_nuevo") IsNot Nothing Then
                                BeTransInvCiclico.IdProductoEst_nuevo = CType(lRow("IdProductoEst_nuevo"), String)
                            End If

                            If lRow("IdUbicacion_nuevo") IsNot DBNull.Value AndAlso lRow("IdUbicacion_nuevo") IsNot Nothing Then
                                BeTransInvCiclico.IdUbicacion_nuevo = CType(lRow("IdUbicacion_nuevo"), String)
                            End If

                            BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
                            BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
                            BeTransInvCiclico.Ubicacion_Nueva = IIf(IsDBNull(lRow("Ubicacion_Nueva")), "", lRow("Ubicacion_Nueva"))
                            BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
                            lReturnList.Add(BeTransInvCiclico)

                        Next

                        Return lReturnList

                    End Using

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOperador(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico) As Boolean

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico
                                  Where(IdStock = @IdStock AND 
                                        idinventarioenc=@idinventarioenc AND 
                                        IdProductoBodega=@IdProductoBodega AND 
                                        IdOperador=@IdOperador AND 
                                        IdOperador=@IdOperador AND  
                                        --cantidad = @cantidad AND  
                                        lote = @lote AND  
                                        fecha_vence=@fecha_vence)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_ciclico.IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDINVENTARIOENC", oBeTrans_inv_ciclico.Idinventarioenc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_inv_ciclico.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", oBeTrans_inv_ciclico.Idoperador))
            'dad.SelectCommand.Parameters.Add(New SqlParameter("@cantidad", oBeTrans_inv_ciclico.Cantidad))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lote", oBeTrans_inv_ciclico.Lote))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@fecha_vence", oBeTrans_inv_ciclico.Fecha_vence))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            ElseIf dt.Rows.Count > 1 Then
                Cargar(oBeTrans_inv_ciclico, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function ExisteKeyValuePair(lOperaciones As List(Of KeyValuePair(Of Integer, Integer)), IdStock As Integer, IdOperacion As Integer) As Boolean
        Return lOperaciones.Any(Function(kv) kv.Key = IdStock AndAlso kv.Value = IdOperacion)
    End Function

    'Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(ByVal pIdInventarioEnc As Integer,
    '                                                                               ByVal IdBodega As Integer,
    '                                                                               ByVal lConnection As SqlConnection,
    '                                                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

    '    Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar = Nothing

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

    '        Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
    '                                     dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
    '                                     dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
    '                                     dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
    '                                     dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
    '                                     dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
    '                                     trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              FROM dbo.trans_inv_ciclico INNER JOIN
    '                                     dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
    '                                     dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
    '                                     dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
    '                                     dbo.bodega_tramo INNER JOIN
    '                                     dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
    '                                     dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
    '                                     dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
    '                                     dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
    '                                     dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
    '                                     dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
    '                              WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
    '                              GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
    '                                     dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
    '                                     dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
    '                                     dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
    '                                     dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


    '        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '            lDTA.SelectCommand.CommandType = CommandType.Text
    '            lDTA.SelectCommand.Transaction = lTransaction
    '            lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
    '            lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

    '            Dim lDataTable As New DataTable
    '            lDTA.Fill(lDataTable)

    '            Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

    '            For Each lRow As DataRow In lDataTable.Rows

    '                BeTransInvCiclico = New clsBeTrans_inv_ciclico()

    '                BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

    '                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
    '                    BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
    '                End If

    '                If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
    '                    BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
    '                End If

    '                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
    '                End If

    '                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
    '                End If

    '                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
    '                End If

    '                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
    '                End If

    '                If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
    '                End If

    '                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
    '                End If

    '                If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
    '                    BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
    '                End If

    '                If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
    '                    BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
    '                End If

    '                If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
    '                    BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
    '                End If

    '                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
    '                    BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
    '                End If

    '                If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
    '                    BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
    '                End If

    '                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
    '                    BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
    '                End If

    '                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
    '                    BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
    '                End If

    '                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
    '                End If

    '                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
    '                End If

    '                If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
    '                    BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
    '                End If

    '                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
    '                    BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
    '                End If

    '                If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
    '                End If

    '                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
    '                    BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
    '                End If

    '                If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
    '                End If

    '                If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
    '                End If

    '                If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
    '                End If

    '                If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
    '                End If

    '                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
    '                    BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
    '                End If

    '                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
    '                End If

    '                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
    '                End If

    '                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
    '                    BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
    '                End If

    '                If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
    '                End If

    '                If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
    '                    BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
    '                End If

    '                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
    '                End If

    '                If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
    '                End If

    '                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
    '                End If

    '                BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
    '                BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
    '                BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
    '                BeTransInvCiclico.Operador = IIf(IsDBNull(lRow("nombres")), "", lRow("nombres"))

    '                lReturnList.Add(BeTransInvCiclico)

    '            Next

    '            Return lReturnList

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(ByVal pIdInventarioEnc As Integer,
                                                                                   ByVal IdBodega As Integer,
                                                                                   ByVal lConnection As SqlConnection,
                                                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
                                         dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
                                         dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
                                         dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
                                         dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, 
                                         dbo.trans_inv_ciclico.user_agr, 
                                         dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
                                         dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
                                         dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
                                         trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  FROM dbo.trans_inv_ciclico INNER JOIN
                                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                                         dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                                         dbo.bodega_tramo INNER JOIN
                                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                                         dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                                         dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                                         dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
                                         dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                                         dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                                  WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
                                  GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
                                         dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
                                         dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
                                         dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, 
                                         dbo.trans_inv_ciclico.user_agr, 
                                         dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
                                         dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
                                         dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

                For Each lRow As DataRow In lDataTable.Rows

                    BeTransInvCiclico = New clsBeTrans_inv_ciclico()

                    BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

                    If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                        BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
                    End If

                    If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
                        BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
                    End If

                    If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                        BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
                    End If

                    If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                        BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
                    End If

                    If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                        BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                    End If

                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                        BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                    End If

                    If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
                        BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
                    End If

                    If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                        BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                    End If

                    If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                        BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                    End If

                    If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
                        BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
                    End If

                    If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
                        BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
                    End If

                    If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                        BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
                    End If

                    If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
                        BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
                    End If

                    If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                        BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
                    End If

                    If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                        BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
                    End If

                    If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                        BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
                    End If

                    If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                        BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
                    End If

                    If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                        BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
                        BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
                    End If

                    If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                        BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
                    End If

                    If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                        BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                    End If

                    If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                        BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
                    End If

                    If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                        BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
                    End If

                    If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
                        BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
                    End If

                    If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                        BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
                    End If

                    If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
                        BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
                    End If

                    If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                        BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
                    End If

                    If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                        BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
                    End If

                    If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                        BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                    End If

                    If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                        BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
                    End If

                    If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                        BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
                    End If

                    If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
                        BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
                    End If

                    If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                        BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
                    End If

                    If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                        BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                    End If

                    If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                        BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
                    End If

                    BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
                    BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
                    BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
                    BeTransInvCiclico.Operador = IIf(IsDBNull(lRow("nombres")), "", lRow("nombres"))

                    lReturnList.Add(BeTransInvCiclico)

                Next

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    'Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(ByVal pIdInventarioEnc As Integer,
    '                                                                               ByVal IdBodega As Integer,
    '                                                                               ByVal lConnection As SqlConnection,
    '                                                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

    '    Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar = Nothing

    '    Try

    '        Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

    '        Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
    '                                     dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
    '                                     dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
    '                                     dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
    '                                     dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
    '                                     CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
    '                                     dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
    '                                     trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              FROM dbo.trans_inv_ciclico INNER JOIN
    '                                     dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
    '                                     dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
    '                                     dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
    '                                     dbo.bodega_tramo INNER JOIN
    '                                     dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
    '                                     dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
    '                                     dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
    '                                     dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
    '                                     dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
    '                                     dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
    '                              WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
    '                              GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
    '                                     dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
    '                                     dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
    '                                     dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
    '                                     dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
    '                                     dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
    '                                     dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
    '                              ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


    '        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '            lDTA.SelectCommand.CommandType = CommandType.Text
    '            lDTA.SelectCommand.Transaction = lTransaction
    '            lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
    '            lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

    '            Dim lDataTable As New DataTable
    '            lDTA.Fill(lDataTable)

    '            Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

    '            For Each lRow As DataRow In lDataTable.Rows

    '                BeTransInvCiclico = New clsBeTrans_inv_ciclico()

    '                BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

    '                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
    '                    BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
    '                End If

    '                If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
    '                    BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
    '                End If

    '                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
    '                End If

    '                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
    '                End If

    '                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
    '                End If

    '                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
    '                End If

    '                If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
    '                End If

    '                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
    '                End If

    '                If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
    '                    BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
    '                End If

    '                If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
    '                    BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
    '                End If

    '                If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
    '                    BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
    '                End If

    '                If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
    '                    BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
    '                End If

    '                If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
    '                    BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
    '                End If

    '                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
    '                    BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
    '                End If

    '                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
    '                    BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
    '                End If

    '                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
    '                    BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
    '                End If

    '                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
    '                End If

    '                If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
    '                    BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
    '                End If

    '                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
    '                    BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
    '                End If

    '                If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
    '                End If

    '                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
    '                    BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
    '                End If

    '                If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
    '                End If

    '                If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
    '                End If

    '                If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
    '                End If

    '                If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
    '                    BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
    '                End If

    '                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
    '                    BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
    '                End If

    '                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
    '                    BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
    '                End If

    '                If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
    '                    BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
    '                End If

    '                If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
    '                    BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
    '                End If

    '                If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
    '                    BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
    '                End If

    '                If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
    '                    BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
    '                End If

    '                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
    '                End If

    '                If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
    '                    BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
    '                End If

    '                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
    '                    BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
    '                End If

    '                BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
    '                BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
    '                BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
    '                BeTransInvCiclico.Operador = IIf(IsDBNull(lRow("nombres")), "", lRow("nombres"))

    '                lReturnList.Add(BeTransInvCiclico)

    '            Next

    '            Return lReturnList

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar(ByVal pIdInventarioEnc As Integer, ByVal IdBodega As Integer) As List(Of clsBeTrans_inv_ciclico)

        Get_All_BeTransInvCiclico_By_IdInventarioEnc_SinAgrupar = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion AS Ubicacion, dbo.bodega_tramo.descripcion AS Tramo, dbo.Nombre_Completo_Ubicacion(dbo.bodega_ubicacion.IdUbicacion, 
                                         dbo.bodega_ubicacion.IdBodega) AS Nombre_Completo, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo AS Codigo, dbo.producto.nombre AS Producto, ISNULL(dbo.producto_presentacion.nombre, '') AS Presentacion, 
                                         dbo.trans_inv_ciclico.lote, dbo.trans_inv_ciclico.lote_stock, dbo.producto_estado.nombre AS Estado, dbo.trans_inv_ciclico.cantidad AS Cantidad_Ciclico, dbo.trans_inv_ciclico.peso AS Peso_Ciclico, dbo.producto.IdPropietario, 
                                         dbo.producto.IdClasificacion, dbo.producto.IdFamilia, dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, 
                                         dbo.operador.IdOperador, dbo.operador.nombres, dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock AS Peso_Stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock AS Cantidad_Stock, 
                                         dbo.trans_inv_ciclico.peso_reconteo, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, ISNULL(dbo.producto_presentacion.factor, 1) AS Factor, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.Cantidad * producto_presentacion.Factor ELSE trans_inv_ciclico.Cantidad END AS Expr1, 
                                         CASE WHEN trans_inv_ciclico.IdPresentacion > 0 THEN trans_inv_ciclico.cant_stock * producto_presentacion.Factor ELSE trans_inv_ciclico.cant_stock END AS Expr2,
                                         dbo.Nombre_Completo_Ubicacion( dbo.bodega_ubicacion.IdUbicacion,  dbo.bodega_ubicacion.IdBodega) as Ubicacion,
                                         trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  FROM dbo.trans_inv_ciclico INNER JOIN
                                         dbo.producto_bodega ON dbo.trans_inv_ciclico.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
                                         dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto INNER JOIN
                                         dbo.bodega ON dbo.producto_bodega.IdBodega = dbo.bodega.IdBodega LEFT OUTER JOIN
                                         dbo.bodega_tramo INNER JOIN
                                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega AND dbo.bodega_tramo.IdArea = dbo.bodega_ubicacion.IdArea AND 
                                         dbo.bodega_tramo.IdSector = dbo.bodega_ubicacion.IdSector ON dbo.bodega.IdBodega = dbo.bodega_tramo.IdBodega AND dbo.trans_inv_ciclico.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
                                         dbo.producto_tipo ON dbo.producto.IdTipoProducto = dbo.producto_tipo.IdTipoProducto LEFT OUTER JOIN
                                         dbo.operador ON dbo.trans_inv_ciclico.idoperador = dbo.operador.IdOperador LEFT OUTER JOIN
                                         dbo.producto_estado ON dbo.trans_inv_ciclico.IdProductoEstado = dbo.producto_estado.IdEstado LEFT OUTER JOIN
                                         dbo.producto_presentacion ON dbo.trans_inv_ciclico.IdPresentacion = dbo.producto_presentacion.IdPresentacion
                                  WHERE (dbo.trans_inv_ciclico.idinventarioenc = @idinventario AND trans_inv_ciclico.IdBodega = @IdBodega)
                                  GROUP BY dbo.bodega_ubicacion.IdUbicacion, dbo.bodega_ubicacion.descripcion, dbo.bodega_tramo.descripcion, dbo.trans_inv_ciclico.IdStock, dbo.producto.codigo, dbo.producto.nombre, dbo.producto_presentacion.nombre, 
                                         dbo.trans_inv_ciclico.lote, dbo.producto_estado.nombre, dbo.trans_inv_ciclico.cantidad, dbo.trans_inv_ciclico.peso, dbo.producto.IdPropietario, dbo.producto.IdClasificacion, dbo.producto.IdFamilia, 
                                         dbo.producto_estado.IdEstado, dbo.trans_inv_ciclico.EsNuevo, dbo.bodega_tramo.IdTramo, dbo.trans_inv_ciclico.fecha_vence, dbo.trans_inv_ciclico.idinventarioenc, dbo.operador.IdOperador, dbo.operador.nombres, 
                                         dbo.trans_inv_ciclico.idinvciclico, dbo.trans_inv_ciclico.IdProductoBodega, dbo.trans_inv_ciclico.user_agr, dbo.trans_inv_ciclico.EsPallet, dbo.trans_inv_ciclico.lic_plate, dbo.trans_inv_ciclico.lote_stock, 
                                         dbo.trans_inv_ciclico.IdPresentacion, dbo.trans_inv_ciclico.fecha_vence_stock, dbo.trans_inv_ciclico.peso_stock, dbo.trans_inv_ciclico.fec_agr, dbo.trans_inv_ciclico.cant_stock, dbo.trans_inv_ciclico.peso_reconteo, 
                                         dbo.bodega_tramo.es_rack, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos, dbo.producto_tipo.NombreTipoProducto, dbo.producto.IdProducto, 
                                         dbo.producto_presentacion.factor, dbo.bodega_ubicacion.IdBodega, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence
                                  ORDER BY Tramo, dbo.bodega_ubicacion.indice_x, dbo.bodega_ubicacion.nivel, dbo.bodega_ubicacion.orientacion_pos"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventarioEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransInvCiclico As New clsBeTrans_inv_ciclico

                        For Each lRow As DataRow In lDataTable.Rows

                            BeTransInvCiclico = New clsBeTrans_inv_ciclico()

                            BeTransInvCiclico.IdInvCiclico = lRow("Idinvciclico")

                            If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                BeTransInvCiclico.lic_plate = CType(lRow("lic_plate"), String)
                            End If

                            If lRow("EsPallet") IsNot DBNull.Value AndAlso lRow("EsPallet") IsNot Nothing Then
                                BeTransInvCiclico.EsPallet = CType(lRow("EsPallet"), Boolean)
                            End If

                            If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                BeTransInvCiclico.Ubicacion = CType(lRow("Ubicacion"), String)
                            End If

                            If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                BeTransInvCiclico.User_agr = CType(lRow("user_agr"), String)
                            End If

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                BeTransInvCiclico.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                            End If

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                BeTransInvCiclico.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            End If

                            If lRow("idinvciclico") IsNot DBNull.Value AndAlso lRow("idinvciclico") IsNot Nothing Then
                                BeTransInvCiclico.IdInvCiclico = CType(lRow("idinvciclico"), Integer)
                            End If

                            If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                BeTransInvCiclico.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                            End If

                            If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                BeTransInvCiclico.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                            End If

                            If lRow("IdOperador") IsNot DBNull.Value AndAlso lRow("IdOperador") IsNot Nothing Then
                                BeTransInvCiclico.Idoperador = CType(lRow("IdOperador"), Integer)
                            End If

                            If lRow("nombres") IsNot DBNull.Value AndAlso lRow("nombres") IsNot Nothing Then
                                BeTransInvCiclico.Operador = CType(lRow("nombres"), String)
                            End If

                            If lRow("Tramo") IsNot DBNull.Value AndAlso lRow("Tramo") IsNot Nothing Then
                                BeTransInvCiclico.Tramo = CType(lRow("Tramo"), String)
                            End If

                            If lRow("IdTramo") IsNot DBNull.Value AndAlso lRow("IdTramo") IsNot Nothing Then
                                BeTransInvCiclico.IdTramo = CType(lRow("IdTramo"), Integer)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                BeTransInvCiclico.Producto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                BeTransInvCiclico.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                BeTransInvCiclico.Presentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                                BeTransInvCiclico.Lote = CType(lRow("Lote"), String)
                            End If

                            If lRow("lote_stock") IsNot DBNull.Value AndAlso lRow("lote_stock") IsNot Nothing Then
                                BeTransInvCiclico.Lote_stock = CType(lRow("lote_stock"), String)
                                BeTransInvCiclico.Lote_stock = BeTransInvCiclico.Lote_stock.Trim
                            End If

                            If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                BeTransInvCiclico.Estado = CType(lRow("Estado"), String)
                            End If

                            If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                BeTransInvCiclico.Cant_stock = CType(lRow("Cantidad_Stock"), Double)
                            End If

                            If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                BeTransInvCiclico.Factor = CType(lRow("Factor"), Double)
                            End If

                            If lRow("Peso_Stock") IsNot DBNull.Value AndAlso lRow("Peso_Stock") IsNot Nothing Then
                                BeTransInvCiclico.Peso_stock = CType(lRow("Peso_Stock"), Double)
                            End If

                            If lRow("Cantidad_Ciclico") IsNot DBNull.Value AndAlso lRow("Cantidad_Ciclico") IsNot Nothing Then
                                BeTransInvCiclico.Cantidad = CType(lRow("Cantidad_Ciclico"), Double)
                            End If

                            If lRow("peso_stock") IsNot DBNull.Value AndAlso lRow("peso_stock") IsNot Nothing Then
                                BeTransInvCiclico.Peso_stock = CType(lRow("peso_stock"), Double)
                            End If

                            If lRow("Peso_Ciclico") IsNot DBNull.Value AndAlso lRow("Peso_Ciclico") IsNot Nothing Then
                                BeTransInvCiclico.Peso = CType(lRow("Peso_Ciclico"), Double)
                            End If

                            If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                BeTransInvCiclico.IdStock = CType(lRow("IdStock"), Integer)
                            End If

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                BeTransInvCiclico.IdPropietario = CType(lRow("IdPropietario"), Integer)
                            End If

                            If lRow("IdClasificacion") IsNot DBNull.Value AndAlso lRow("IdClasificacion") IsNot Nothing Then
                                BeTransInvCiclico.IdClasificacion = CType(lRow("IdClasificacion"), Integer)
                            End If

                            If lRow("IdFamilia") IsNot DBNull.Value AndAlso lRow("IdFamilia") IsNot Nothing Then
                                BeTransInvCiclico.IdFamilia = CType(lRow("IdFamilia"), Integer)
                            End If

                            If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                                BeTransInvCiclico.IdProductoEstado = CType(lRow("IdEstado"), Integer)
                            End If

                            If lRow("EsNuevo") IsNot DBNull.Value AndAlso lRow("EsNuevo") IsNot Nothing Then
                                BeTransInvCiclico.EsNuevo = CType(lRow("EsNuevo"), Boolean)
                            End If

                            If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                BeTransInvCiclico.Fecha_vence = CType(lRow("fecha_vence"), Date)
                            End If

                            If lRow("fecha_vence_stock") IsNot DBNull.Value AndAlso lRow("fecha_vence_stock") IsNot Nothing Then
                                BeTransInvCiclico.Fecha_vence_stock = CType(lRow("fecha_vence_stock"), Date)
                            End If

                            If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                BeTransInvCiclico.Fec_agr = CType(lRow("fec_agr"), Date)
                            End If

                            BeTransInvCiclico.TipoProducto = IIf(IsDBNull(lRow("NombreTipoProducto")), "", lRow("NombreTipoProducto"))
                            BeTransInvCiclico.Ubicacion = IIf(IsDBNull(lRow("Nombre_Completo")), "", lRow("Nombre_Completo"))
                            BeTransInvCiclico.IdProducto = IIf(IsDBNull(lRow("IdProducto")), "", lRow("IdProducto"))
                            BeTransInvCiclico.Operador = IIf(IsDBNull(lRow("nombres")), "", lRow("nombres"))

                            lReturnList.Add(BeTransInvCiclico)

                        Next

                        Return lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer) As DataTable

    '    Get_All_By_Comparacion_Inventario = Nothing

    '    Try

    '        Dim vSQL As String = "SELECT codigo as Código,producto as Producto ,LoteOrigen as LoteOrigen, Lote, FechaVence,Licencia, 
    '                                    EstadoOrigen, EstadoDestino, UbicacionOrigen, UbicacionDestino,
    '                                    Cantidad_Stock as CantidadStock,
    '                                    Peso_Stock as PesoStock,Cantidad as CantidadConteo,Peso as PesoConteo,Entradas,Salidas,Entradas_Salidas,
    '                                    (Cantidad+Entradas_Salidas) as NuevoStock,(Cantidad-Cantidad_Stock+Entradas_Salidas) as DiferenciaCantidad, 
    '                                    (Peso_Stock - Peso) as DiferenciaPeso
    '                                    from tempComparacionInventario
    '                                    Where IdInventario=@idinventarioenc"

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            lConnection.Open()

    '            Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

    '                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
    '                    lDataAdapter.SelectCommand.Transaction = lTransaction
    '                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

    '                    Dim lDataTable As New DataTable()
    '                    lDataAdapter.Fill(lDataTable)

    '                    If lDataTable.Rows.Count > 0 Then
    '                        Return lDataTable
    '                    End If

    '                End Using

    '                lTransaction.Commit()

    '            End Using

    '            lConnection.Close()

    '        End Using

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_All_By_Comparacion_Inventario(ByVal pIdInv As Integer, lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_Comparacion_Inventario = Nothing

        Try

            'Dim vSQL As String = "SELECT codigo as Código,producto as Producto ,LoteOrigen as LoteOrigen, Lote, FechaVence,Licencia, 
            '                            EstadoOrigen, EstadoDestino, UbicacionOrigen, UbicacionDestino,
            '                            Cantidad_Stock as CantidadStock,
            '                            Peso_Stock as PesoStock,Cantidad as CantidadConteo,Peso as PesoConteo,Entradas,Salidas,Entradas_Salidas,
            '                            (Cantidad+Entradas_Salidas) as NuevoStock,(Cantidad-Cantidad_Stock+Entradas_Salidas) as DiferenciaCantidad, 
            '                            (Peso_Stock - Peso) as DiferenciaPeso
            '                            from tempComparacionInventario
            '                            Where IdInventario=@idinventarioenc"

            Dim vSQL As String = "SELECT 
                    codigo AS Código,
                    producto AS Producto,
                    LoteOrigen AS LoteOrigen,
                    Lote,
                    FechaVence,
                    Licencia, 
                    EstadoOrigen,
                    EstadoDestino,
                    UbicacionOrigen,
                    UbicacionDestino,
                    Cantidad_Stock AS CantidadStock,
                    Peso_Stock AS PesoStock,
                    Cantidad AS CantidadConteo,
                    Peso AS PesoConteo,
                    Entradas,
                    Salidas,
                    Entradas_Salidas,

                    -- NuevoStock con múltiples condiciones
                    CASE 
                        WHEN Cantidad = (Cantidad_Stock + Entradas ) AND Salidas = 0  THEN Cantidad
                        WHEN Salidas IS NOT NULL AND Salidas < 0 THEN (Cantidad_Stock + Salidas)
                        ELSE (Cantidad + Entradas_Salidas + Salidas)
                    END AS NuevoStock,

                    -- DiferenciaCantidad con múltiples condiciones
                    CASE 
                        WHEN (Cantidad = (Cantidad_Stock + Entradas)) AND Salidas = 0 THEN (Cantidad_Stock + Entradas - Cantidad)
                        WHEN Salidas IS NOT NULL AND Salidas < 0 THEN  (Cantidad_Stock + Salidas - Cantidad) * -1
                        ELSE ((Cantidad_Stock + Entradas_Salidas) - Cantidad) * -1
                    END AS DiferenciaCantidad,

                    -- DiferenciaPeso se mantiene igual
                    (Peso_Stock - Peso) AS DiferenciaPeso,
                    Cantidad_Reservada_UmBas,
                    TieneReservaYConteoInsuficiente,  
                    Observacion
                FROM 
                    tempComparacionInventario
                WHERE 
                    IdInventario = @idinventarioenc;"


            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Return lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Conteo_By_IdStock_And_IdInvEnc_Mayor_0(ByVal pIdStock As Integer,
                                                                         ByVal pIdInventarioEnc As Integer,
                                                                         ByVal pConnection As SqlConnection,
                                                                         ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim ExisteO As Boolean = False
            Dim ExisteNO As Boolean = False
            Dim Existe As Boolean = False

            Const sp As String = "SELECT ISNULL(cantidad,0) CANT
                                  FROM Trans_inv_ciclico 
                                  WHERE(IdStock = @IdStock AND idinventarioenc=@idinventarioenc) AND 
                                       cantidad>0 AND lote = lote_stock AND
                                    fecha_vence = fecha_vence_stock AND
                                    (IdUbicacion = IdUbicacion_nuevo or IdUbicacion_nuevo = 0) AND
                                    IdProductoEstado = IdProductoEst_nuevo"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.Add(New SqlParameter("@IdStock", pIdStock))
                lCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventarioEnc))

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    ExisteO = CDbl(lReturnValue) > 1
                End If
            End Using

            Const sp1 As String = "SELECT ISNULL(cantidad,0) CANT
                                  FROM Trans_inv_ciclico 
                                  WHERE(IdStock = @IdStock AND idinventarioenc=@idinventarioenc) AND 
                                       cantidad>0 AND (lote <> lote_stock OR
	                                   fecha_vence <> fecha_vence_stock OR
	                                   (IdUbicacion <> IdUbicacion_nuevo AND IdUbicacion_nuevo <> 0) OR
	                                   IdProductoEstado <> IdProductoEst_nuevo)"

            Using lCommand As New SqlCommand(sp1, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.Add(New SqlParameter("@IdStock", pIdStock))
                lCommand.Parameters.Add(New SqlParameter("@idinventarioenc", pIdInventarioEnc))

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    ExisteNO = CDbl(lReturnValue) > 1
                End If
            End Using

            Return ExisteO AndAlso ExisteNO

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener_Existencia_Conteo(ByVal pIdStock As Integer,
                                                     ByVal pIdInventarioEnc As Integer,
                                                     ByVal pConnection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction) As (ExisteConteoSobreOriginal As Boolean, ConteoSobreOriginalConCambios As Boolean)
        Try
            Dim ExisteConteoSobreOriginal = EjecutarConteo(
            "SELECT ISNULL(cantidad,0) 
             FROM Trans_inv_ciclico 
             WHERE IdStock = @IdStock AND idinventarioenc = @idinventarioenc 
               AND cantidad > 0 
               AND lote = lote_stock 
               AND fecha_vence = fecha_vence_stock 
               AND (IdUbicacion = IdUbicacion_nuevo OR IdUbicacion_nuevo = 0) 
               AND IdProductoEstado = IdProductoEst_nuevo",
            pIdStock, pIdInventarioEnc, pConnection, pTransaction)

            Dim ConteoSobreOriginalConCambios = EjecutarConteo(
            "SELECT ISNULL(cantidad,0) 
             FROM Trans_inv_ciclico 
             WHERE IdStock = @IdStock AND idinventarioenc = @idinventarioenc 
               AND cantidad > 0 
               AND (lote <> lote_stock 
                    OR fecha_vence <> fecha_vence_stock 
                    OR (IdUbicacion <> IdUbicacion_nuevo AND IdUbicacion_nuevo <> 0) 
                    OR IdProductoEstado <> IdProductoEst_nuevo)",
            pIdStock, pIdInventarioEnc, pConnection, pTransaction)

            Return (ExisteConteoSobreOriginal, ConteoSobreOriginalConCambios)

        Catch ex As Exception
            Dim msg = $"{MethodBase.GetCurrentMethod.Name} {ex.Message}"
            clsLnLog_error_wms.Agregar_Error(msg)
            Throw
        End Try
    End Function

    Private Shared Function EjecutarConteo(ByVal sql As String,
                                           ByVal idStock As Integer,
                                           ByVal idInventarioEnc As Integer,
                                           ByVal conn As SqlConnection,
                                           ByVal tran As SqlTransaction) As Boolean
        Using cmd As New SqlCommand(sql, conn, tran) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdStock", idStock)
            cmd.Parameters.AddWithValue("@idinventarioenc", idInventarioEnc)
            Dim result = cmd.ExecuteScalar()
            Return result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToDouble(result) > 1
        End Using
    End Function

    Private Shared Sub Procesar_Ajustes_SAP(gBeInventario As clsBeTrans_inv_enc,
                                            pConnection As SqlConnection,
                                            pTransaction As SqlTransaction, vIdPropietarioBodega As Integer,
                                            pUsuario As clsBeUsuario,
                                            pCodigoBodegaERP As String,
                                            pTallaColor As Boolean)

        Dim vIdMotivoAjuste As Integer = 0
        Dim DT As New DataTable
        Dim ajustes As New List(Of clsBeTrans_ajuste_det)
        Dim pBeTransAjustEnc As New clsBeTrans_ajuste_enc()

        Try

            ajustes = clsLnTrans_ajuste_det.Get_All_Ajustes_By_IdInventarioEnc_For_SAP(gBeInventario.Idinventarioenc,
                                                                                       pTallaColor,
                                                                                       pConnection,
                                                                                       pTransaction)

            If ajustes IsNot Nothing Then

                If ajustes.Count > 0 Then

                    ' Crear encabezado para el ajuste
                    pBeTransAjustEnc.IdAjusteenc = clsLnTrans_ajuste_enc.MaxID(pConnection, pTransaction) + 1
                    pBeTransAjustEnc.IdBodega = gBeInventario.IdBodega
                    pBeTransAjustEnc.Idusuario = pUsuario.IdUsuario
                    pBeTransAjustEnc.Fecha = Date.Now
                    pBeTransAjustEnc.IdCentroCosto = gBeInventario.IdCentroCosto
                    pBeTransAjustEnc.IdPropietarioBodega = vIdPropietarioBodega
                    pBeTransAjustEnc.User_agr = pUsuario.Nombres
                    pBeTransAjustEnc.User_mod = pUsuario.Nombres
                    pBeTransAjustEnc.Referencia = $"Ajustes para SAP generado por inventario No. {gBeInventario.Idinventarioenc}"
                    pBeTransAjustEnc.Ajuste_Por_Inventario = gBeInventario.Idinventarioenc

                    DT = clsLnAjuste_motivo.Listar()

                    If DT IsNot Nothing Then
                        If DT.Rows.Count > 0 Then
                            vIdMotivoAjuste = DT.Rows(0).Item("IdMotivoAjuste")
                        End If
                        DT.Dispose()
                    End If

                    ' Insertar encabezado
                    clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc, pConnection, pTransaction)

                    ' Procesar los detalles de los ajustes
                    For Each ajuste In ajustes

                        ajuste.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(pConnection, pTransaction) + 1
                        ajuste.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
                        ajuste.IdPropietarioBodega = vIdPropietarioBodega

                        If ajuste.Cantidad_nueva - ajuste.Cantidad_original > 0 Then
                            ajuste.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo
                            ajuste.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTPI
                        Else
                            ajuste.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo
                            ajuste.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTNI
                        End If

                        ajuste.Enviado = False

                        ajuste.IdMotivoAjuste = vIdMotivoAjuste

                        Dim BodegaERP As Integer = clsLnCliente.Get_IdBodega_By_Codigo(Val(pCodigoBodegaERP),
                                                                                   pConnection,
                                                                                   pTransaction)

                        ajuste.IdBodegaERP = Val(BodegaERP)
                        clsLnTrans_ajuste_det.Insertar(ajuste, pConnection, pTransaction)

                    Next

                End If

            End If

        Catch ex As Exception
            Throw New Exception($"Error en Procesar_Ajustes_SAP: {ex.Message}")
        End Try

    End Sub

    Public Shared Function Agregar_Conteo(ByVal pBeTransInvCiclico As clsBeTrans_inv_ciclico,
                                          pIdResolucion As Integer,
                                          pTallaColor As clsBeProducto_talla_color,
                                          pCrearTallaColor As Boolean) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cnt As Integer = 0

        Try

            If Not pBeTransInvCiclico Is Nothing Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                '#AT20260529 Crear nueva combinacion de talla color si aplica
                If pTallaColor IsNot Nothing AndAlso pCrearTallaColor Then
                    pTallaColor.IdProductoTallaColor = clsLnProducto_talla_color.MaxID() + 1

                    If clsLnProducto_talla_color.Insertar(pTallaColor) > 0 Then
                        Dim PrdTallaColor = clsLnProducto_talla_color.Get_Single_By_IdColor_IdTalla(pTallaColor.IdProducto,
                                                                                                    pTallaColor.IdTalla,
                                                                                                    pTallaColor.IdColor)

                        pBeTransInvCiclico.IdProductoTallaColor_nuevo = PrdTallaColor.IdProductoTallaColor
                    End If
                End If

                If Insertar(pBeTransInvCiclico, lConnection, lTransaction) > 0 Then

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

                    lTransaction.Commit()

                    Return 1

                End If

            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Producto_By_IdOperador_And_IdStock(ByVal IdOperador As Integer,
                                                         ByVal IdInventario As Integer,
                                                         ByVal IdProductoBodega As Integer,
                                                         ByVal IdStock As Integer,
                                                         lConnection As SqlConnection,
                                                         lTransaction As SqlTransaction) As Boolean

        Try

            Const sp As String = "SELECT * 
                                  FROM Trans_inv_ciclico 
                                  WHERE idinventarioenc = @IdInventarioEnc AND 
                                        idoperador = @IdOperador AND 
                                        IdProductoBodega = @IdProductoBodega AND 
                                        IdStock = @IdStock "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@idinventarioenc", IdInventario))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOperador", IdOperador))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))

            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Copiar_Temp(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico, ByRef Temp As clsBeTempComparacionInventario)
        Try
            If oBeTrans_inv_ciclico Is Nothing Then
                oBeTrans_inv_ciclico = New clsBeTrans_inv_ciclico()
            End If

            With oBeTrans_inv_ciclico
                .Idinventarioenc = Temp.IdInventario
                .IdProductoBodega = Temp.IdProductoBodega
                .Cantidad = Temp.Cantidad
                .Cant_stock = Temp.Cantidad_Stock
                .IdUbicacion = Temp.IdUbicacion
                .IdUbicacion_nuevo = Temp.IdUbicacionDestino
                .Codigo = Temp.Codigo
                .Estado = Temp.EstadoOrigen
                .EstadoNuevo = Temp.EstadoDestino
                .Fecha_vence = Temp.FechaVence
                .Fecha_vence_stock = Temp.FechaVenceStock
                .IdUnidadMedida = Temp.IdUnidadMedida
                .IdPresentacion = Temp.IdPresentacion
                .IdProductoEstado = Temp.IdProductoEstado
                .IdProductoEst_nuevo = Temp.IdProductoEst_nuevo
                .lic_plate = Temp.Licencia
            End With

        Catch ex As Exception
            ' Aquí puedes registrar el error o lanzar una excepción más específica
            Throw New ApplicationException("Error al copiar datos de Temp a oBeTrans_inv_ciclico: " & ex.Message, ex)
        End Try
    End Sub


    ''' <summary>
    ''' #CKFK20250715 GetAll del inventario agrupado
    ''' </summary>
    ''' <param name="pIdInvEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_IdInventarioEncAgrupado(pIdInvEnc As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_ciclico)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_ciclico)

            Dim vSQL As String = "SELECT 0 IdInvCiclico, T.idinventarioenc, T.IdStock, T.IdProductoBodega, T.IdProductoEstado, T.IdPresentacion,
		                                    T.IdUbicacion, 0 EsNuevo, T.LoteOrigen lote_stock, 
		                                    T.LoteDestino lote, T.fecha_vence_stock,  T.fecha_vence,
                                            SUM(T.cantidad_stock) AS cant_stock, 
		                                    SUM(T.cantidad_conteo) AS cantidad, 
		                                    SUM(T.cantidad_reconteo) AS cant_reconteo, 
		                                    MAX(T.peso_stock) AS peso_stock, 
		                                    SUM(T.peso_conteo) AS peso, 
		                                    SUM(T.peso_reconteo) AS peso_reconteo, 
		                                    0 as IdOperador,0 as user_agr, GETDATE() fec_agr,
		                                    T.IdProductoEst_nuevo, 0 IdPresentacion_nuevo,T.idubicacion_nuevo,
		                                    0 EsPallet,T.Licencia lic_plate,T.IdUnidadMedida,
		                                    T.IdBodega,MAX(T.Fec_Mod) Fec_Mod, t.Regularizar, SUM(t.nuevo_stock)nuevo_stock,
                                            SUM(T.cantidad_reservada_umbas) AS cantidad_reservada_umbas
                                    FROM (
                                    SELECT  trans_inv_ciclico.idinventarioenc, 
                                            trans_inv_ciclico.IdStock,
                                            trans_inv_ciclico.IdProductoBodega,  
                                            MAX(trans_inv_ciclico.cant_stock) AS cantidad_stock, 
		                                    SUM(trans_inv_ciclico.cantidad) AS cantidad_conteo, 
		                                    SUM(trans_inv_ciclico.cant_reconteo) AS cantidad_reconteo, 
		                                    MAX(trans_inv_ciclico.peso_stock) AS peso_stock, 
		                                    SUM(trans_inv_ciclico.peso) AS peso_conteo, 
		                                    SUM(trans_inv_ciclico.peso_reconteo) AS peso_reconteo, 
		                                    producto.codigo AS Codigo, producto.nombre AS Producto, trans_inv_ciclico.lote_stock as LoteOrigen, 
		                                    trans_inv_ciclico.lote as LoteDestino,  trans_inv_ciclico.fecha_vence, trans_inv_ciclico.lic_plate AS Licencia, 
		                                    producto_estado_1.nombre AS Estado, producto_estado.nombre AS EstadoDestino,  
		                                    dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion,trans_inv_ciclico.IdBodega) as UbicacionOrigen,
		                                    ISNULL(dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion_nuevo,trans_inv_ciclico.IdBodega),
		                                    dbo.Nombre_Completo_Ubicacion(trans_inv_ciclico.IdUbicacion,trans_inv_ciclico.IdBodega)) as UbicacionDestino,
		                                    trans_inv_ciclico.IdUbicacion, MAX(trans_inv_ciclico.fec_mod) Fec_Mod,
		                                    trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdProductoEstado, 
		                                    trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.fecha_vence_stock, 
		                                    trans_inv_ciclico.IdUnidadMedida, trans_inv_ciclico.idubicacion_nuevo, trans_inv_ciclico.IdBodega,
		                                    trans_inv_ciclico.regularizar, MAX(trans_inv_ciclico.nuevo_stock) nuevo_stock,  
                                            MAX(trans_inv_ciclico.cantidad_reservada_umbas) AS cantidad_reservada_umbas
                                    FROM     trans_inv_ciclico INNER JOIN
                                                        trans_inv_enc ON trans_inv_ciclico.idinventarioenc = trans_inv_enc.idinventarioenc AND trans_inv_ciclico.idinventarioenc = trans_inv_enc.idinventarioenc INNER JOIN
                                                        producto_bodega ON trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega AND trans_inv_ciclico.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                                                        producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                                                        producto_estado AS producto_estado_1 ON trans_inv_ciclico.IdProductoEstado = producto_estado_1.IdEstado INNER JOIN
                                                        producto_estado ON trans_inv_ciclico.IdProductoEst_nuevo = producto_estado.IdEstado
                                    WHERE  (trans_inv_ciclico.idinventarioenc = @idinventarioenc )
                                    GROUP BY dbo.trans_inv_ciclico.idinventarioenc,dbo.trans_inv_ciclico.IdStock, trans_inv_ciclico.IdProductoBodega, producto.codigo, producto.nombre, 
                                                trans_inv_ciclico.lote, trans_inv_ciclico.fecha_vence, trans_inv_ciclico.lic_plate, 
                                                trans_inv_ciclico.IdProductoEstado, trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.IdUbicacion, 
		                                        trans_inv_ciclico.IdUbicacion_nuevo, producto_estado_1.nombre, producto_estado.nombre, 
		                                        trans_inv_ciclico.lote_stock,trans_inv_ciclico.IdStock,
		                                        trans_inv_ciclico.IdBodega,trans_inv_ciclico.IdUnidadMedida,
		                                    trans_inv_ciclico.IdPresentacion, trans_inv_ciclico.IdProductoEstado, 
		                                    trans_inv_ciclico.IdProductoEst_nuevo, trans_inv_ciclico.fecha_vence_stock, 
		                                    trans_inv_ciclico.idubicacion_nuevo, trans_inv_ciclico.IdBodega, trans_inv_ciclico.regularizar) T
                                    GROUP BY T.idinventarioenc, T.IdProductoBodega,
		                                    T.Codigo, t.Producto, T.LoteOrigen, 
		                                    T.LoteDestino,  T.fecha_vence, T.Licencia, 
		                                    T.Estado, T.EstadoDestino,  
		                                    T.UbicacionOrigen,
		                                    T.UbicacionDestino,
		                                    T.IdUbicacion,
		                                    T.IdUnidadMedida,
		                                    T.IdPresentacion, T.IdProductoEstado, 
		                                    T.IdProductoEst_nuevo, T.fecha_vence_stock, 
		                                    T.IdStock,T.IdUbicacion_nuevo,
		                                    T.IdBodega, T.regularizar Order by T.IdProductoBodega"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInvEnc)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                Dim vBeTrans_inv_detalle As New clsBeTrans_inv_ciclico

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_detalle = New clsBeTrans_inv_ciclico
                    Cargar(vBeTrans_inv_detalle, dr)
                    If vBeTrans_inv_detalle.Nuevo_Stock <> 0 Then
                        vBeTrans_inv_detalle.Cant_stock = vBeTrans_inv_detalle.Nuevo_Stock
                    End If
                    lReturnList.Add(vBeTrans_inv_detalle)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Regularizacion_Inventario(ByVal pIdInv As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_Regularizacion_Inventario = Nothing

        Try

            '#MA20260204 agregar columnas de presentacion y UMBas
            Dim vSQL As String = "SELECT ci.codigo AS Código,
                                         ci.producto AS Producto,
                                         ci.LoteOrigen,
                                         ci.Lote,
                                         ci.FechaVence,
                                         ci.Licencia, 
                                         ci.EstadoOrigen,
                                         ci.EstadoDestino,
                                         ci.UbicacionOrigen,
                                         ci.UbicacionDestino,
                                         CASE 
                                          WHEN ci.IdPresentacion > 0 
                                            THEN pp.nombre
                                          ELSE u.Nombre
                                        END AS Presentacion,
                                        CASE 
                                            WHEN ci.IdPresentacion > 0 
                                                THEN ci.Cantidad_Stock / NULLIF(pp.factor, 0)
                                            ELSE ci.Cantidad_Stock
                                        END AS CantidadStock_Pres,
                                       ci.Peso_Stock AS PesoStock,
                                       CASE 
                                         WHEN ci.IdPresentacion > 0 
                                           THEN ci.Cantidad / NULLIF(pp.factor, 0)
                                         ELSE ci.Cantidad
                                       END AS CantidadConteo_Pres,
                                       ci.Peso AS PesoConteo,
                                       CASE 
                                         WHEN ci.IdPresentacion > 0 
                                           THEN ci.Entradas / NULLIF(pp.factor, 0)
                                          ELSE ci.Entradas
                                       END AS Entradas_Pres,
                                        CASE 
                                            WHEN ci.IdPresentacion > 0 
                                                THEN ci.Salidas / NULLIF(pp.factor, 0)
                                            ELSE ci.Salidas
                                        END AS Salidas_Pres,
                                        CASE 
                                            WHEN ci.IdPresentacion > 0 
                                                THEN ci.Entradas_Salidas / NULLIF(pp.factor, 0)
                                            ELSE ci.Entradas_Salidas
                                        END AS Entradas_Salidas_Pres,
                                        CASE 
                                            WHEN ci.IdPresentacion > 0 
                                                THEN ci.Cantidad_Reservada_UmBas / NULLIF(pp.factor, 0)
                                            ELSE ci.Cantidad_Reservada_UmBas
                                        END AS Cantidad_Reservada_Pres,
                                        ci.Observacion,
                                        CASE 
                                          WHEN ci.Cantidad = (ci.Cantidad_Stock + ci.Entradas) AND ci.Salidas = 0  
                                            THEN ci.Cantidad
                                             WHEN ci.Salidas IS NOT NULL AND ci.Salidas < 0 
                                              THEN (ci.Cantidad_Stock + ci.Salidas)
                                             ELSE (ci.Cantidad + ci.Entradas_Salidas + ci.Salidas)
                                         END AS NuevoStock,
                                         CASE 
                                           WHEN ci.Cantidad = (ci.Cantidad_Stock + ci.Entradas) AND ci.Salidas = 0 
                                             THEN (ci.Cantidad_Stock + ci.Entradas - ci.Cantidad)
                                             WHEN ci.Salidas IS NOT NULL AND ci.Salidas < 0 
                                               THEN (ci.Cantidad_Stock + ci.Salidas - ci.Cantidad) * -1
                                            ELSE ((ci.Cantidad_Stock + ci.Entradas_Salidas) - ci.Cantidad) * -1
                                          END AS DiferenciaCantidad,
                                          (ci.Peso_Stock - ci.Peso) AS DiferenciaPeso,
                                          ci.TieneReservaYConteoInsuficiente
                                        FROM ComparacionInventario ci
                                        LEFT JOIN producto p 
                                            ON p.codigo = ci.codigo 
                                        LEFT JOIN unidad_medida u 
                                            ON p.IdUnidadMedidaBasica = u.IdUnidadMedida 
                                        LEFT JOIN producto_presentacion pp 
                                            ON ci.IdPresentacion = pp.IdPresentacion
                                        WHERE 
                                            ci.IdInventario = @idinventarioenc 
                                            AND ci.TieneReservaYConteoInsuficiente = 0;"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Return lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Comparacion_Inventario_No_Regularizar(ByVal pIdInv As Integer, lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_Comparacion_Inventario_No_Regularizar = Nothing

        Try

            '#MA20260204 Afregar columnas de presentacion
            Dim vSQL As String = "SELECT 
                    t.codigo AS Código,
                    t.producto AS Producto,
                    t.LoteOrigen AS LoteOrigen,
                    t.Lote,
                    t.FechaVence,
                    t.Licencia, 
                    t.EstadoOrigen,
                    t.EstadoDestino,
                    t.UbicacionOrigen,
                    t.UbicacionDestino,
                    CASE 
                       WHEN t.IdPresentacion > 0 
                         THEN pp.nombre
                            ELSE u.Nombre
                         END AS Presentacion,
                    t.Cantidad_Stock / ISNULL(pp.factor,1) AS CantidadStock_Pres,
                    t.Peso_Stock AS PesoStock,
                    t.Cantidad / ISNULL(pp.factor,1) AS CantidadConteo_Pres,
                    t.Peso AS PesoConteo,
                     CASE 
                        WHEN t.IdPresentacion > 0 
                          THEN t.Entradas / NULLIF(pp.factor, 0)
                            ELSE t.Entradas
                    END AS Entradas_Pres,
                     CASE 
                       WHEN t.IdPresentacion > 0 
                        THEN t.Salidas / NULLIF(pp.factor, 0)
                          ELSE t.Salidas
                     END AS Salidas_Pres,
                    CASE 
                     WHEN t.IdPresentacion > 0 
                       THEN t.Entradas_Salidas / NULLIF(pp.factor, 0)
                         ELSE t.Entradas_Salidas
                    END AS Entradas_Salidas_Pres,
                    -- NuevoStock con múltiples condiciones 
                    CASE 
                        WHEN t.Cantidad = (t.Cantidad_Stock + t.Entradas) AND t.Salidas = 0 THEN t.Cantidad
                        WHEN t.Salidas IS NOT NULL AND t.Salidas < 0 THEN (t.Cantidad_Stock + t.Salidas)
                        ELSE (t.Cantidad + t.Entradas_Salidas + t.Salidas)
                    END AS NuevoStock,

                    -- DiferenciaCantidad con múltiples condiciones 
                    CASE 
                        WHEN t.Cantidad = (t.Cantidad_Stock + t.Entradas) AND t.Salidas = 0 THEN (t.Cantidad_Stock + t.Entradas - t.Cantidad)
                        WHEN t.Salidas IS NOT NULL AND t.Salidas < 0 THEN (t.Cantidad_Stock + t.Salidas - t.Cantidad) * -1
                        ELSE ((t.Cantidad_Stock + t.Entradas_Salidas) - t.Cantidad) * -1
                    END AS DiferenciaCantidad,

                    -- DiferenciaPeso se mantiene igual 
                    (t.Peso_Stock - t.Peso) AS DiferenciaPeso,
                     CASE 
                      WHEN t.IdPresentacion > 0 
                      THEN t.Cantidad_Reservada_UmBas / NULLIF(pp.factor, 0)
                        ELSE t.Cantidad_Reservada_UmBas
                     END AS Cantidad_Reservada_Pres,
                   t.TieneReservaYConteoInsuficiente,  
                   t.Observacion
                FROM 
                   tempComparacionInventario t
               LEFT JOIN producto p ON p.codigo = t.codigo 
                                LEFT JOIN unidad_medida u ON p.IdUnidadMedidaBasica = u.IdUnidadMedida 
                                LEFT JOIN producto_presentacion pp ON t.IdPresentacion = pp.IdPresentacion
                WHERE 
                    t.IdInventario = @idinventarioenc
                    AND t.TieneReservaYConteoInsuficiente = 1;"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Return lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Comparacion_Inventario_A_Regularizar(ByVal pIdInv As Integer, lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_Comparacion_Inventario_A_Regularizar = Nothing

        Try

            '#MA20260204 agregar columnas de presentacion
            Dim vSQL As String = "SELECT 
                    t.codigo AS Código,
                    t.producto AS Producto,
                    t.LoteOrigen AS LoteOrigen,
                    t.Lote,
                    t.FechaVence,
                    t.Licencia, 
                    t.EstadoOrigen,
                    t.EstadoDestino,
                    t.UbicacionOrigen,
                    t.UbicacionDestino,
                    CASE
                    WHEN t.IdPresentacion > 0 THEN pp.nombre
                    ELSE u.Nombre
                    END AS Presentacion,
                    t.Cantidad_Stock / ISNULL(pp.factor,1) AS CantidadStock_Pres,
                    t.Peso_Stock AS PesoStock,
                    t.Cantidad / ISNULL(pp.factor,1) AS CantidadConteo_Pres,
                    t.Peso AS PesoConteo,
                    CASE
                    WHEN t.IdPresentacion > 0
                    THEN t.Entradas / NULLIF(pp.factor,0)
                    ELSE t.Entradas
                    END AS Entradas_Pres,
                    CASE
                    WHEN t.IdPresentacion > 0
                    THEN t.Salidas / NULLIF(pp.factor,0)
                    ELSE t.Salidas
                    END AS Salidas_Pres,
                    CASE
                    WHEN t.IdPresentacion > 0
                    THEN t.Entradas_Salidas / NULLIF(pp.factor,0)
                    ELSE t.Entradas_Salidas
                    END AS Entradas_Salidas_Pres,
                    -- NuevoStock con múltiples condiciones 
                    CASE 
                        WHEN t.Cantidad = (t.Cantidad_Stock + t.Entradas) AND t.Salidas = 0 THEN t.Cantidad
                        WHEN t.Salidas IS NOT NULL AND t.Salidas < 0 THEN (t.Cantidad_Stock + t.Salidas)
                        ELSE (t.Cantidad + t.Entradas_Salidas + t.Salidas)
                    END AS NuevoStock,

                    -- DiferenciaCantidad con múltiples condiciones 
                    CASE 
                       WHEN t.Cantidad = (t.Cantidad_Stock + t.Entradas) AND t.Salidas = 0 THEN (t.Cantidad_Stock + t.Entradas - t.Cantidad)
                       WHEN t.Salidas IS NOT NULL AND t.Salidas < 0 THEN (t.Cantidad_Stock + t.Salidas - t.Cantidad) * -1
                       ELSE ((t.Cantidad_Stock + t.Entradas_Salidas) - t.Cantidad) * -1
                    END AS DiferenciaCantidad,

                    -- DiferenciaPeso se mantiene igual
                    (t.Peso_Stock - t.Peso) AS DiferenciaPeso,
                    CASE
                    WHEN t.IdPresentacion > 0
                    THEN t.Cantidad_Reservada_UmBas / NULLIF(pp.factor,0)
                    ELSE t.Cantidad_Reservada_UmBas
                    END AS Cantidad_Reservada_Pres,  
                    t.TieneReservaYConteoInsuficiente,  
                    t.Observacion
                FROM 
                   tempComparacionInventario t
               LEFT JOIN producto p ON p.codigo = t.codigo 
                                LEFT JOIN unidad_medida u ON p.IdUnidadMedidaBasica = u.IdUnidadMedida 
                                LEFT JOIN producto_presentacion pp ON t.IdPresentacion = pp.IdPresentacion
                WHERE 
                    t.IdInventario = @idinventarioenc
                    AND t.TieneReservaYConteoInsuficiente = 0;"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    Return lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Existente_By_IdStock(ByVal IdStock As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeStock)

        Try

            Dim lReturnList As New List(Of clsBeStock)

            Dim vSQL As String = "SELECT * FROM stock WHERE IdStock=@IdStock "

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
                lDataAdapter.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                Dim vBeTrans_inv_detalle As New clsBeStock

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_inv_detalle = New clsBeStock
                    clsLnStock.Cargar(vBeTrans_inv_detalle, dr)
                    lReturnList.Add(vBeTrans_inv_detalle)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Regularizacion_Inventario_Consolidado(ByVal pIdInv As Integer,
                                                                            ByVal lConnection As SqlConnection,
                                                                            ByVal lTransaction As SqlTransaction) As DataTable

        Dim vSQL As String = "
                                SELECT 
                            ci.codigo AS Codigo,
                            ci.producto AS Producto,
                            ISNULL(pt.NombreTipoProducto, '') AS Categoria,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Cantidad / NULLIF(pp.factor, 0)
                                    ELSE ci.Cantidad
                                END
                            ) AS CantidadContada,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Cantidad_Stock / NULLIF(pp.factor, 0)
                                    ELSE ci.Cantidad_Stock
                                END
                            ) AS CantidadEsperada,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Entradas_Salidas / NULLIF(pp.factor, 0)
                                    ELSE ci.Entradas_Salidas
                                END
                            ) AS SalidasEntradas,

                            SUM(
                                CASE 
                                    WHEN ci.Cantidad = (ci.Cantidad_Stock + ci.Entradas) AND ci.Salidas = 0 
                                        THEN (ci.Cantidad_Stock + ci.Entradas - ci.Cantidad)
                                    WHEN ci.Salidas IS NOT NULL AND ci.Salidas < 0 
                                        THEN (ci.Cantidad_Stock + ci.Salidas - ci.Cantidad) * -1
                                    ELSE ((ci.Cantidad_Stock + ci.Entradas_Salidas) - ci.Cantidad) * -1
                                END
                            ) AS Diferencia

                        FROM ComparacionInventario ci
                        LEFT JOIN producto p 
                            ON p.codigo = ci.codigo
                        LEFT JOIN producto_tipo pt
                            ON pt.IdTipoProducto = p.IdTipoProducto
                        LEFT JOIN producto_presentacion pp 
                            ON ci.IdPresentacion = pp.IdPresentacion

                        WHERE 
                            ci.IdInventario = @idinventarioenc
                            AND ci.TieneReservaYConteoInsuficiente = 0

                        GROUP BY 
                            ci.codigo,
                            ci.producto,
                            pt.NombreTipoProducto

                        ORDER BY 
                            ci.codigo;

                            SELECT 
                            ci.codigo AS Codigo,
                            ci.producto AS Producto,
                            ISNULL(pt.NombreTipoProducto, '') AS Categoria,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Cantidad / NULLIF(pp.factor, 0)
                                    ELSE ci.Cantidad
                                END
                            ) AS CantidadContada,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Cantidad_Stock / NULLIF(pp.factor, 0)
                                    ELSE ci.Cantidad_Stock
                                END
                            ) AS CantidadEsperada,

                            SUM(
                                CASE 
                                    WHEN ci.IdPresentacion > 0 
                                        THEN ci.Entradas_Salidas / NULLIF(pp.factor, 0)
                                    ELSE ci.Entradas_Salidas
                                END
                            ) AS SalidasEntradas,

                            SUM(
                                CASE 
                                    WHEN ci.Cantidad = (ci.Cantidad_Stock + ci.Entradas) AND ci.Salidas = 0 
                                        THEN (ci.Cantidad_Stock + ci.Entradas - ci.Cantidad)
                                    WHEN ci.Salidas IS NOT NULL AND ci.Salidas < 0 
                                        THEN (ci.Cantidad_Stock + ci.Salidas - ci.Cantidad) * -1
                                    ELSE ((ci.Cantidad_Stock + ci.Entradas_Salidas) - ci.Cantidad) * -1
                                END
                            ) AS Diferencia

                        FROM ComparacionInventario ci
                        LEFT JOIN producto p 
                            ON p.codigo = ci.codigo
                        LEFT JOIN producto_tipo pt
                            ON pt.IdTipoProducto = p.IdTipoProducto
                        LEFT JOIN producto_presentacion pp 
                            ON ci.IdPresentacion = pp.IdPresentacion

                        WHERE 
                            ci.IdInventario = @idinventarioenc
                            AND ci.TieneReservaYConteoInsuficiente = 0

                        GROUP BY 
                            ci.codigo,
                            ci.producto,
                            pt.NombreTipoProducto

                        ORDER BY 
                            ci.codigo;"

        Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
            lDataAdapter.SelectCommand.CommandType = CommandType.Text
            lDataAdapter.SelectCommand.Transaction = lTransaction
            lDataAdapter.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInv)

            Dim lDataTable As New DataTable()
            lDataAdapter.Fill(lDataTable)

            Return lDataTable
        End Using

    End Function

    Public Shared Function Actualiza_Conteo_Ciclico_Caja_Master_HH(ByVal ListaTransInvCiclico As List(Of clsBeTrans_inv_ciclico)) As Integer

        Dim registrosProcesados As Integer = 0
        Dim lTransaction As SqlTransaction = Nothing

        If ListaTransInvCiclico Is Nothing OrElse ListaTransInvCiclico.Count = 0 Then
            Return 0
        End If

        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Try
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Dim pCampos(2) As clsBeProducto.ProdPropiedades

                pCampos(0) = clsBeProducto.ProdPropiedades.Control_lote
                pCampos(1) = clsBeProducto.ProdPropiedades.Control_vencimiento

                For Each BeTransInvCiclico As clsBeTrans_inv_ciclico In ListaTransInvCiclico

                    Dim rslt As Integer = 0
                    Dim pBeProducto As New clsBeProducto
                    Dim InvCiclico As clsBeTrans_inv_ciclico = Nothing

                    pBeProducto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeTransInvCiclico.IdProductoBodega,
                                                                                               lConnection,
                                                                                               lTransaction)

                    pBeProducto = clsLnProducto.GetSingle(pBeProducto.IdProducto,
                                                          pCampos,
                                                          lConnection,
                                                          lTransaction)

                    InvCiclico = Get_Inventario_Ciclico(BeTransInvCiclico,
                                                        pBeProducto,
                                                        lConnection,
                                                        lTransaction)

                    If InvCiclico Is Nothing Then
                        Throw New Exception(
                            "No se encontró información de inventario para el producto: " &
                            BeTransInvCiclico.IdProductoBodega.ToString())
                    End If

                    InvCiclico.Fec_Mod = Now
                    InvCiclico.Contado = True
                    InvCiclico.Cantidad = InvCiclico.Cant_stock

                    rslt = Act_Inventario_Ciclico_Caja_Master(InvCiclico,
                                                              lConnection,
                                                              lTransaction)



                    If rslt <= 0 Then
                        Throw New Exception(
                            "No fue posible actualizar el producto: " &
                            BeTransInvCiclico.IdProductoBodega.ToString())
                    End If

                    registrosProcesados += 1

                Next

                lTransaction.Commit()

                Return registrosProcesados

            Catch ex As Exception

                If lTransaction IsNot Nothing Then
                    lTransaction.Rollback()
                End If

                Throw

            End Try

        End Using

    End Function

    '#AT20260523 Actualizar inventario ciclico por idinvciclico caja master
    Public Shared Function Act_Inventario_Ciclico_Caja_Master(ByRef oBeTrans_inv_ciclico As clsBeTrans_inv_ciclico,
                                                              Optional ByVal pConection As SqlConnection = Nothing,
                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_inv_ciclico")

            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("contado", "@contado", DataType.Parametro)
            Upd.Where("idinvciclico = @idinvciclico")

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

            cmd.Parameters.Add(New SqlParameter("@IDINVCICLICO", oBeTrans_inv_ciclico.IdInvCiclico))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_inv_ciclico.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_inv_ciclico.Fec_Mod))
            cmd.Parameters.Add(New SqlParameter("@CONTADO", oBeTrans_inv_ciclico.Contado))

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

    Public Shared Function Cerrar_Inventario_RFID(ByVal BeTransInvEnc As clsBeTrans_inv_enc,
                                                  ByVal ListBeBarra_epc As List(Of clsBeTrans_inv_ciclico_rfid),
                                                  ByVal Usuario As clsBeUsuario,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As Boolean




        Dim ListStock As New List(Of clsBeI_nav_barras_rfid_stock)
        Dim objStockHist As New clsBeI_nav_barras_rfid_stock_his
        Dim vCantidadHist As Integer = 0
        Dim lIdStocksAEliminar As New List(Of Integer)

        Try


            BeTransInvEnc.Regularizado = True
            BeTransInvEnc.Estado = "Finalizado"
            BeTransInvEnc.Hora_fin = Now
            BeTransInvEnc.Activo = True
            BeTransInvEnc.User_mod = Usuario.IdUsuario

            clsLnTrans_inv_enc.Actualizar(BeTransInvEnc, lConnection, lTransaction)


            '#GT03062026: insertar historico y eliminar stock.

            If ListBeBarra_epc IsNot Nothing AndAlso ListBeBarra_epc.Count > 0 Then

                For Each pBeBarra_EPC As clsBeTrans_inv_ciclico_rfid In ListBeBarra_epc.OrderBy(Function(x) x.IdProductoBodega)

                    Dim BeStockRFID As New clsBeI_nav_barras_rfid_stock

                    BeStockRFID.Barra_epc = pBeBarra_EPC.SSCC

                    clsLnI_nav_barras_rfid_stock.GetSingle_By_Barra_Epc(BeStockRFID, lConnection, lTransaction)

                    If BeStockRFID IsNot Nothing Then

                        'clsPublic.CopyObject(pBeBarra_EPC, objStockHist)
                        '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                        
                        'objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                        'objStockHist.IdNuevoStock = pBeBarra_EPC.IdStock
                        'objStockHist.IdPedidoEnc = pBeStock.IdPedidoEnc
                        'objStockHist.IdPickingEnc = pBeStock.IdPickingEnc
                        'objStockHist.IdUbicacion_anterior = BeStock.IdUbicacion
                        'objStockHist.IdUbicacion = pBeStock.IdUbicacion

                        objStockHist.IdRfidStock = BeStockRFID.IdRfidStock
                        objStockHist.IdBodega = BeStockRFID.IdBodega
                        objStockHist.IdProductoBodega = BeStockRFID.IdProductoBodega
                        objStockHist.Barra_epc = BeStockRFID.Barra_epc
                        objStockHist.Lote = BeStockRFID.Lote
                        objStockHist.IdPedidoEnc = 0
                        objStockHist.IdOrdenCompraEnc = 0
                        objStockHist.User_agr = Usuario.IdUsuario
                        objStockHist.User_mod = Usuario.IdUsuario
                        objStockHist.Fec_agr = Now
                        objStockHist.Fec_mod = Now
                        objStockHist.Cantidad = 1
                        objStockHist.Motivo = "WMS-AJINRFID"

                        clsLnI_nav_barras_rfid_stock_his.Insertar(objStockHist, lConnection, lTransaction)
                        'clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                        '#GT04062026: solo elimina del stock la barra_epc
                        clsLnI_nav_barras_rfid_stock.Eliminar_Stock_Salida(BeStockRFID)

                    End If

                Next

            End If

            clsLnTarea_hh.Actualiza_Estado_Tarea(BeTransInvEnc.Idinventarioenc,
                                                 6,
                                                 4,
                                                 lConnection,
                                                 lTransaction)


            Return True

        Catch ex As Exception
            Throw
        End Try

    End Function


    '#AG27052026: Obtiene progreso de conteo ciclico por ubicacion.
    Public Shared Function Get_Progreso_Conteo_Ubicacion(ByVal pIdInventarioEnc As Integer,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As DataTable

        Dim lDataTable As New DataTable

        Try

            Dim vSQL As String =
                    "WITH StockBase AS ( " &
                    "    SELECT " &
                    "        c.IdUbicacion, " &
                    "        c.IdBodega, " &
                    "        dbo.Nombre_Completo_Ubicacion(c.IdUbicacion, c.IdBodega) AS Ubicacion, " &
                    "        c.IdStock, " &
                    "        c.IdProductoBodega, " &
                    "        MAX(ISNULL(c.cant_stock, 0)) AS CantidadAContar, " &
                    "        MAX(CASE WHEN ISNULL(c.contado, 0) = 1 THEN 1 ELSE 0 END) AS StockContado, " &
                    "        MAX(CASE WHEN ISNULL(c.contado, 0) = 1 THEN ISNULL(c.cantidad, 0) ELSE 0 END) AS CantidadContada " &
                    "    FROM trans_inv_ciclico c " &
                    "    WHERE c.idinventarioenc = @idinventarioenc " &
                    "      AND c.IdBodega = @IdBodega " &
                    "    GROUP BY " &
                    "        c.IdUbicacion, " &
                    "        c.IdBodega, " &
                    "        dbo.Nombre_Completo_Ubicacion(c.IdUbicacion, c.IdBodega), " &
                    "        c.IdStock, " &
                    "        c.IdProductoBodega " &
                    "), " &
                    "ProductoBase AS ( " &
                    "    SELECT " &
                    "        IdUbicacion, " &
                    "        IdBodega, " &
                    "        Ubicacion, " &
                    "        IdProductoBodega, " &
                    "        SUM(CantidadAContar) AS CantidadAContar, " &
                    "        SUM(CantidadContada) AS CantidadContada, " &
                    "        MIN(StockContado) AS ProductoContado " &
                    "    FROM StockBase " &
                    "    GROUP BY " &
                    "        IdUbicacion, " &
                    "        IdBodega, " &
                    "        Ubicacion, " &
                    "        IdProductoBodega " &
                    ") " &
                    "SELECT " &
                    "    Ubicacion, " &
                    "    COUNT(IdProductoBodega) AS ProductosAContar, " &
                    "    SUM(CASE WHEN ProductoContado = 1 THEN 1 ELSE 0 END) AS ProductosContados, " &
                    "    COUNT(IdProductoBodega) - SUM(CASE WHEN ProductoContado = 1 THEN 1 ELSE 0 END) AS DiferenciaProductos, " &
                    "    SUM(CantidadAContar) AS CantidadTotalAContar, " &
                    "    SUM(CantidadContada) AS CantidadTotalContada, " &
                    "    SUM(CantidadContada) - SUM(CantidadAContar) AS DiferenciaTotal " &
                    "FROM ProductoBase " &
                    "GROUP BY Ubicacion " &
                    "ORDER BY Ubicacion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                lDTA.Fill(lDataTable)

            End Using

            Return lDataTable

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#AG27052026: Obtiene progreso de conteo ciclico por tramo/rack.
    Public Shared Function Get_Progreso_Conteo_Tramo(ByVal pIdInventarioEnc As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As DataTable

        Dim lDataTable As New DataTable

        Try

            Dim vSQL As String =
                "WITH StockBase AS ( " &
                "    SELECT " &
                "        bu.IdTramo, " &
                "        bt.descripcion AS Tramo, " &
                "        c.IdUbicacion, " &
                "        c.IdStock, " &
                "        c.IdProductoBodega, " &
                "        MAX(CASE WHEN ISNULL(c.contado, 0) = 1 THEN 1 ELSE 0 END) AS StockContado " &
                "    FROM trans_inv_ciclico c " &
                "    INNER JOIN bodega_ubicacion bu " &
                "        ON bu.IdUbicacion = c.IdUbicacion " &
                "       AND bu.IdBodega = c.IdBodega " &
                "    LEFT JOIN bodega_tramo bt " &
                "        ON bt.IdTramo = bu.IdTramo " &
                "       AND bt.IdBodega = bu.IdBodega " &
                "    WHERE c.idinventarioenc = @idinventarioenc " &
                "      AND c.IdBodega = @IdBodega " &
                "    GROUP BY " &
                "        bu.IdTramo, " &
                "        bt.descripcion, " &
                "        c.IdUbicacion, " &
                "        c.IdStock, " &
                "        c.IdProductoBodega " &
                "), " &
                "ProductoBase AS ( " &
                "    SELECT " &
                "        Tramo, " &
                "        IdUbicacion, " &
                "        IdProductoBodega, " &
                "        MIN(StockContado) AS ProductoContado " &
                "    FROM StockBase " &
                "    GROUP BY " &
                "        Tramo, " &
                "        IdUbicacion, " &
                "        IdProductoBodega " &
                "), " &
                "Ubicaciones AS ( " &
                "    SELECT " &
                "        Tramo, " &
                "        IdUbicacion, " &
                "        COUNT(IdProductoBodega) AS ProductosAContar, " &
                "        SUM(CASE WHEN ProductoContado = 1 THEN 1 ELSE 0 END) AS ProductosContados " &
                "    FROM ProductoBase " &
                "    GROUP BY " &
                "        Tramo, " &
                "        IdUbicacion " &
                ") " &
                "SELECT " &
                "    ISNULL(Tramo, '') AS Tramo, " &
                "    COUNT(IdUbicacion) AS UbicacionesAContar, " &
                "    SUM(CASE WHEN ProductosAContar = ProductosContados THEN 1 ELSE 0 END) AS UbicacionesContadas, " &
                "    COUNT(IdUbicacion) - SUM(CASE WHEN ProductosAContar = ProductosContados THEN 1 ELSE 0 END) AS DiferenciaUbicaciones " &
                "FROM Ubicaciones " &
                "GROUP BY Tramo " &
                "ORDER BY Tramo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventarioenc", pIdInventarioEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                lDTA.Fill(lDataTable)

            End Using

            Return lDataTable

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


End Class
