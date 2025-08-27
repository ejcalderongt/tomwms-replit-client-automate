Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_enc_reconteo

    Public Shared Function Lista_Reconteos_Activos(pIdenc As Integer) As List(Of clsBeTrans_inv_enc_reconteo)
        Dim lReturnList As List(Of clsBeTrans_inv_enc_reconteo)

        Try
            lReturnList = GetAll().FindAll(Function(x) x.Idinventarioenc = pIdenc).ToList
            lReturnList = lReturnList.FindAll(Function(x) x.Estado <> "Finalizado").OrderByDescending(Function(Y) Y.Reconteo).ToList

            Return lReturnList
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInventario As Integer) As List(Of clsBeTrans_inv_enc_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_enc_reconteo)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT trans_inv_enc_reconteo.idinvencreconteo, producto.codigo AS Codigo, producto.nombre AS Producto, 
                               producto_presentacion.nombre AS Presentacion, producto_estado.nombre AS Estado, trans_inv_reconteo.IdStock, trans_inv_reconteo.lote, 
                               trans_inv_reconteo.cantidadAnterior, SUM(trans_inv_reconteo.cantidad) AS Cantidad, trans_inv_reconteo.pesoAnterior, 
                               SUM(trans_inv_reconteo.peso) AS Peso, trans_inv_enc_reconteo.idinventarioenc, trans_inv_reconteo.fecha_vence, operador.nombres AS Operador, 
                               trans_inv_enc_reconteo.estado AS Estado_reconteo, trans_inv_enc_reconteo.hora_ini, trans_inv_enc_reconteo.hora_fin,trans_inv_reconteo.idUbicacionAnterior,trans_inv_reconteo.idinvreconteo,
                               trans_inv_enc_reconteo.reconteo
                        FROM   trans_inv_enc_reconteo INNER JOIN
                               trans_inv_reconteo ON trans_inv_enc_reconteo.idinvencreconteo = trans_inv_reconteo.idreconteo INNER JOIN
                               producto_bodega ON trans_inv_reconteo.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                               producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                               producto_estado ON trans_inv_reconteo.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                               operador ON trans_inv_reconteo.IdOperador = operador.IdOperador LEFT OUTER JOIN
                               producto_presentacion ON trans_inv_reconteo.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE  trans_inv_enc_reconteo.idinventarioenc=@idinventario
                      GROUP BY trans_inv_enc_reconteo.idinvencreconteo, producto.codigo, producto.nombre, producto_presentacion.nombre, producto_estado.nombre, 
                               trans_inv_reconteo.IdStock, trans_inv_reconteo.lote, trans_inv_reconteo.cantidadAnterior, trans_inv_reconteo.pesoAnterior, 
                               trans_inv_enc_reconteo.idinventarioenc, trans_inv_reconteo.fecha_vence, operador.nombres, trans_inv_enc_reconteo.estado, 
                               trans_inv_enc_reconteo.hora_ini, trans_inv_enc_reconteo.hora_fin,trans_inv_reconteo.idUbicacionAnterior,trans_inv_reconteo.idinvreconteo,trans_inv_enc_reconteo.reconteo"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_inv_enc_reconteo

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_inv_enc_reconteo()

                                If lRow("idUbicacionAnterior") IsNot DBNull.Value AndAlso lRow("idUbicacionAnterior") IsNot Nothing Then
                                    Dim Ubic As New clsBeBodega_ubicacion
                                    Ubic.IdUbicacion = CType(lRow("idUbicacionAnterior"), Integer)
                                    clsLnBodega_ubicacion.Obtener(Ubic, lConnection, lTransaction)
                                    Obj.Ubicacion = Ubic.NombreCompleto
                                End If

                                If lRow("idinvencreconteo") IsNot DBNull.Value AndAlso lRow("idinvencreconteo") IsNot Nothing Then
                                    Obj.Idinvencreconteo = CType(lRow("idinvencreconteo"), Integer)
                                End If

                                If lRow("reconteo") IsNot DBNull.Value AndAlso lRow("reconteo") IsNot Nothing Then
                                    Obj.Reconteo = CType(lRow("reconteo"), Integer)
                                End If

                                If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                                    Obj.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                                End If

                                If lRow("idinvreconteo") IsNot DBNull.Value AndAlso lRow("idinvreconteo") IsNot Nothing Then
                                    Obj.IdInvReconteo = CType(lRow("idinvreconteo"), Integer)
                                End If

                                If lRow("Operador") IsNot DBNull.Value AndAlso lRow("Operador") IsNot Nothing Then
                                    Obj.Operador = CType(lRow("Operador"), String)
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

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.EstadoProd = CType(lRow("Estado"), String)
                                End If

                                If lRow("cantidadAnterior") IsNot DBNull.Value AndAlso lRow("cantidadAnterior") IsNot Nothing Then
                                    Obj.cantidadAnterior = CType(lRow("cantidadAnterior"), Double)
                                End If

                                If lRow("pesoAnterior") IsNot DBNull.Value AndAlso lRow("pesoAnterior") IsNot Nothing Then
                                    Obj.pesoAnterior = CType(lRow("pesoAnterior"), Double)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad"), Double)
                                End If

                                If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("Peso"), Double)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("Estado_reconteo") IsNot DBNull.Value AndAlso lRow("Estado_reconteo") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("Estado_reconteo"), String)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("hora_ini") IsNot DBNull.Value AndAlso lRow("hora_ini") IsNot Nothing Then
                                    Obj.Hora_ini = CType(lRow("hora_ini"), Date)
                                End If

                                If lRow("hora_fin") IsNot DBNull.Value AndAlso lRow("hora_fin") IsNot Nothing Then
                                    Obj.Hora_fin = CType(lRow("hora_fin"), Date)
                                End If

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

    Public Shared Function MaxReconteo(ByVal IdInventarioEnc As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(reconteo),0) FROM Trans_inv_enc_reconteo WHERE IdInventarioEnc=@IdInventarioEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@IdInventarioEnc", IdInventarioEnc)
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

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInventario As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_inv_enc_reconteo)

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_enc_reconteo)

            Dim vSQL As String = "SELECT trans_inv_enc_reconteo.idinvencreconteo, producto.codigo AS Codigo, producto.nombre AS Producto, 
                               producto_presentacion.nombre AS Presentacion, producto_estado.nombre AS Estado, trans_inv_reconteo.IdStock, trans_inv_reconteo.lote, 
                               trans_inv_reconteo.cantidadAnterior, SUM(trans_inv_reconteo.cantidad) AS Cantidad, trans_inv_reconteo.pesoAnterior, 
                               SUM(trans_inv_reconteo.peso) AS Peso, trans_inv_enc_reconteo.idinventarioenc, trans_inv_reconteo.fecha_vence, operador.nombres AS Operador, 
                               trans_inv_enc_reconteo.estado AS Estado_reconteo, trans_inv_enc_reconteo.hora_ini, trans_inv_enc_reconteo.hora_fin,trans_inv_reconteo.idUbicacionAnterior,trans_inv_reconteo.idinvreconteo,
                               trans_inv_enc_reconteo.reconteo
                        FROM   trans_inv_enc_reconteo INNER JOIN
                               trans_inv_reconteo ON trans_inv_enc_reconteo.idinvencreconteo = trans_inv_reconteo.idreconteo INNER JOIN
                               producto_bodega ON trans_inv_reconteo.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                               producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                               producto_estado ON trans_inv_reconteo.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                               operador ON trans_inv_reconteo.IdOperador = operador.IdOperador LEFT OUTER JOIN
                               producto_presentacion ON trans_inv_reconteo.IdPresentacion = producto_presentacion.IdPresentacion
                        WHERE  trans_inv_enc_reconteo.idinventarioenc=@idinventario
                      GROUP BY trans_inv_enc_reconteo.idinvencreconteo, producto.codigo, producto.nombre, producto_presentacion.nombre, producto_estado.nombre, 
                               trans_inv_reconteo.IdStock, trans_inv_reconteo.lote, trans_inv_reconteo.cantidadAnterior, trans_inv_reconteo.pesoAnterior, 
                               trans_inv_enc_reconteo.idinventarioenc, trans_inv_reconteo.fecha_vence, operador.nombres, trans_inv_enc_reconteo.estado, 
                               trans_inv_enc_reconteo.hora_ini, trans_inv_enc_reconteo.hora_fin,trans_inv_reconteo.idUbicacionAnterior,trans_inv_reconteo.idinvreconteo,trans_inv_enc_reconteo.reconteo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@idinventario", pIdInventario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransInvEnc As clsBeTrans_inv_enc_reconteo

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransInvEnc = New clsBeTrans_inv_enc_reconteo()

                        If lRow("idUbicacionAnterior") IsNot DBNull.Value AndAlso lRow("idUbicacionAnterior") IsNot Nothing Then
                            Dim Ubic As New clsBeBodega_ubicacion
                            Ubic.IdUbicacion = CType(lRow("idUbicacionAnterior"), Integer)
                            clsLnBodega_ubicacion.Obtener(Ubic, lConnection, lTransaction)
                            BeTransInvEnc.Ubicacion = Ubic.NombreCompleto
                        End If

                        If lRow("idinvencreconteo") IsNot DBNull.Value AndAlso lRow("idinvencreconteo") IsNot Nothing Then
                            BeTransInvEnc.Idinvencreconteo = CType(lRow("idinvencreconteo"), Integer)
                        End If

                        If lRow("reconteo") IsNot DBNull.Value AndAlso lRow("reconteo") IsNot Nothing Then
                            BeTransInvEnc.Reconteo = CType(lRow("reconteo"), Integer)
                        End If

                        If lRow("idinventarioenc") IsNot DBNull.Value AndAlso lRow("idinventarioenc") IsNot Nothing Then
                            BeTransInvEnc.Idinventarioenc = CType(lRow("idinventarioenc"), Integer)
                        End If

                        If lRow("idinvreconteo") IsNot DBNull.Value AndAlso lRow("idinvreconteo") IsNot Nothing Then
                            BeTransInvEnc.IdInvReconteo = CType(lRow("idinvreconteo"), Integer)
                        End If

                        If lRow("Operador") IsNot DBNull.Value AndAlso lRow("Operador") IsNot Nothing Then
                            BeTransInvEnc.Operador = CType(lRow("Operador"), String)
                        End If

                        If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                            BeTransInvEnc.Producto = CType(lRow("Producto"), String)
                        End If

                        If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                            BeTransInvEnc.Codigo = CType(lRow("Codigo"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            BeTransInvEnc.Presentacion = CType(lRow("Presentacion"), String)
                        End If

                        If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                            BeTransInvEnc.Lote = CType(lRow("lote"), String)
                        End If

                        If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                            BeTransInvEnc.EstadoProd = CType(lRow("Estado"), String)
                        End If

                        If lRow("cantidadAnterior") IsNot DBNull.Value AndAlso lRow("cantidadAnterior") IsNot Nothing Then
                            BeTransInvEnc.cantidadAnterior = CType(lRow("cantidadAnterior"), Double)
                        End If

                        If lRow("pesoAnterior") IsNot DBNull.Value AndAlso lRow("pesoAnterior") IsNot Nothing Then
                            BeTransInvEnc.pesoAnterior = CType(lRow("pesoAnterior"), Double)
                        End If

                        If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                            BeTransInvEnc.Cantidad = CType(lRow("Cantidad"), Double)
                        End If

                        If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                            BeTransInvEnc.Peso = CType(lRow("Peso"), Double)
                        End If

                        If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                            BeTransInvEnc.IdStock = CType(lRow("IdStock"), Integer)
                        End If

                        If lRow("Estado_reconteo") IsNot DBNull.Value AndAlso lRow("Estado_reconteo") IsNot Nothing Then
                            BeTransInvEnc.Estado = CType(lRow("Estado_reconteo"), String)
                        End If

                        If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                            BeTransInvEnc.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                        End If

                        If lRow("hora_ini") IsNot DBNull.Value AndAlso lRow("hora_ini") IsNot Nothing Then
                            BeTransInvEnc.Hora_ini = CType(lRow("hora_ini"), Date)
                        End If

                        If lRow("hora_fin") IsNot DBNull.Value AndAlso lRow("hora_fin") IsNot Nothing Then
                            BeTransInvEnc.Hora_fin = CType(lRow("hora_fin"), Date)
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

End Class
