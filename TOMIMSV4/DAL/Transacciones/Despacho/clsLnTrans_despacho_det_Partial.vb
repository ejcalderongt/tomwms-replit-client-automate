
Imports System.Data.SqlClient

Partial Public Class clsLnTrans_despacho_det


    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdDespachoDet),0) FROM trans_despacho_det"

            '#HS 07112017 Quité query dentro de SqlCommand. 
            Using lCommand As New SqlClient.SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pIdDespachoEnc"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_By_Despacho(ByVal pIdDespachoEnc As Integer) As List(Of clsBeTrans_despacho_det)

        Dim lReturnList As New List(Of clsBeTrans_despacho_det)

        Dim vSQL As String = "SELECT * FROM VW_Despacho_Detalle WHERE IdDespachoEnc=@IdDespachoEnc"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim Obj As clsBeTrans_despacho_det

                        For Each lRow As DataRow In lDT.Rows

                            Obj = New clsBeTrans_despacho_det
                            Obj.IdDespachoDet = CType(lRow("IdDespachoDet"), Integer)
                            Obj.IdDespachoEnc = CType(lRow("IdDespachoEnc"), Integer)
                            Obj.IdPedidoEnc = CType(lRow("IdPedidoEnc"), Integer)
                            Obj.IdPedidoDet = CType(lRow("IdPedidoDet"), Integer)

                            If lRow("IdPickingUbic") IsNot DBNull.Value AndAlso lRow("IdPickingUbic") IsNot Nothing Then
                                Obj.IdPickingUbic = CType(lRow("IdPickingUbic"), Integer)
                            End If

                            If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                Obj.Codigo = CType(lRow("Codigo"), String)
                            End If

                            If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                Obj.NombreProducto = CType(lRow("Producto"), String)
                            End If

                            If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                Obj.ProductoPresentacion = CType(lRow("Presentacion"), String)
                            End If

                            If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                Obj.ProductoEstado = CType(lRow("Estado"), String)
                            End If

                            If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                Obj.ProductoUnidadMedida = CType(lRow("UnidadMedida"), String)
                            End If

                            If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                Obj.NombreUbicacion = CType(lRow("Ubicacion"), String)
                            End If

                            If lRow("Fecha") IsNot DBNull.Value AndAlso lRow("Fecha") IsNot Nothing Then
                                Obj.Fecha = CType(lRow("Fecha"), Date)
                            End If

                            If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                Obj.User_agr = CType(lRow("user_agr"), String)
                            End If

                            If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                Obj.Fec_agr = CType(lRow("fec_agr"), Date)
                            End If

                            If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                Obj.User_mod = CType(lRow("user_mod"), String)
                            End If

                            If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                Obj.Fec_mod = CType(lRow("fec_mod"), Date)
                            End If

                            If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                                Obj.Activo = CType(lRow("Activo"), Boolean)
                            End If

                            Obj.IsNew = False

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

    Public Shared Function Get_All_By_Despacho(ByVal pIdDespachoEnc As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_despacho_det)

        Dim lReturnList As New List(Of clsBeTrans_despacho_det)

        Dim vSQL As String = "SELECT * FROM VW_Despacho_Detalle WHERE IdDespachoEnc=@IdDespachoEnc "

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim Obj As clsBeTrans_despacho_det

                    For Each lRow As DataRow In lDT.Rows

                        Obj = New clsBeTrans_despacho_det
                        Obj.IdDespachoDet = CType(lRow("IdDespachoDet"), Integer)
                        Obj.IdDespachoEnc = CType(lRow("IdDespachoEnc"), Integer)
                        Obj.IdPedidoEnc = CType(lRow("IdPedidoEnc"), Integer)
                        Obj.IdPedidoDet = CType(lRow("IdPedidoDet"), Integer)

                        If lRow("IdPickingUbic") IsNot DBNull.Value AndAlso lRow("IdPickingUbic") IsNot Nothing Then
                            Obj.IdPickingUbic = CType(lRow("IdPickingUbic"), Integer)
                        End If

                        If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                            Obj.Codigo = CType(lRow("Codigo"), String)
                        End If

                        If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                            Obj.NombreProducto = CType(lRow("Producto"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            Obj.ProductoPresentacion = CType(lRow("Presentacion"), String)
                        End If

                        If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                            Obj.ProductoEstado = CType(lRow("Estado"), String)
                        End If

                        If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                            Obj.ProductoUnidadMedida = CType(lRow("UnidadMedida"), String)
                        End If

                        If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                            Obj.NombreUbicacion = CType(lRow("Ubicacion"), String)
                        End If

                        If lRow("Fecha") IsNot DBNull.Value AndAlso lRow("Fecha") IsNot Nothing Then
                            Obj.Fecha = CType(lRow("Fecha"), Date)
                        End If

                        If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                            Obj.User_agr = CType(lRow("user_agr"), String)
                        End If

                        If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                            Obj.Fec_agr = CType(lRow("fec_agr"), Date)
                        End If

                        If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                            Obj.User_mod = CType(lRow("user_mod"), String)
                        End If

                        If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                            Obj.Fec_mod = CType(lRow("fec_mod"), Date)
                        End If

                        If lRow("Activo") IsNot DBNull.Value AndAlso lRow("Activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("Activo"), Boolean)
                        End If

                        Obj.IsNew = False

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As List(Of clsBeTrans_despacho_det)

        Get_All_By_IdPedidoEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_despacho_det)

        Dim vSQL As String = "SELECT trans_despacho_det.IdDespachoDet, trans_despacho_det.IdDespachoEnc, trans_despacho_det.IdPickingUbic, trans_despacho_det.Fecha, 
                trans_despacho_det.user_agr, trans_despacho_det.fec_agr, trans_despacho_det.user_mod, trans_despacho_det.fec_mod, 
                trans_despacho_det.activo, trans_despacho_det.IdPedidoEnc, trans_despacho_det.IdPedidoDet, trans_despacho_det.IdProductoBodega, 
                trans_despacho_det.IdUnidadMedidaBasica, trans_despacho_det.IdPresentacion, trans_despacho_det.Codigo, 
                trans_despacho_det.NombreProducto, trans_despacho_det.NombreEstado, trans_despacho_det.CantidadDespachada, 
                trans_despacho_det.PesoDespachado, trans_despacho_det.IdProductoEstado, trans_picking_ubic.lote, trans_picking_ubic.lic_plate
                FROM            trans_pe_det INNER JOIN
                trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc INNER JOIN
                trans_picking_ubic ON trans_despacho_det.IdPickingUbic = trans_picking_ubic.IdPickingUbic
                WHERE        (trans_pe_det.IdPedidoEnc = @IdPedidoEnc) AND (trans_despacho_det.CantidadDespachada > 0) "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim Obj As clsBeTrans_despacho_det

                            For Each lRow As DataRow In lDT.Rows

                                Obj = New clsBeTrans_despacho_det
                                Cargar(Obj, lRow)

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.Lic_plate = CType(lRow("lic_plate"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_IdPedidoEnc = lReturnList

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

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_despacho_det)

        Get_All_By_IdPedidoEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_despacho_det)

        Dim vSQL As String = "SELECT trans_despacho_det.IdDespachoDet, trans_despacho_det.IdDespachoEnc, trans_despacho_det.IdPickingUbic, trans_despacho_det.Fecha, 
                              trans_despacho_det.user_agr, trans_despacho_det.fec_agr, trans_despacho_det.user_mod, trans_despacho_det.fec_mod, 
                              trans_despacho_det.activo, trans_despacho_det.IdPedidoEnc, trans_despacho_det.IdPedidoDet, trans_despacho_det.IdProductoBodega, 
                              trans_despacho_det.IdUnidadMedidaBasica, trans_despacho_det.IdPresentacion, trans_despacho_det.Codigo, 
                              trans_despacho_det.NombreProducto, trans_despacho_det.NombreEstado, trans_despacho_det.CantidadDespachada, 
                              trans_despacho_det.PesoDespachado, trans_despacho_det.IdProductoEstado, trans_picking_ubic.lote, trans_picking_ubic.lic_plate,
                              producto_presentacion.Nombre as Presentacion  
                              FROM  trans_pe_det INNER JOIN
                              trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                              trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                              trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc INNER JOIN
                              trans_picking_ubic ON trans_despacho_det.IdPickingUbic = trans_picking_ubic.IdPickingUbic LEFT JOIN
                              producto_presentacion ON trans_picking_ubic.IdPresentacion = producto_presentacion.IdPresentacion                             
                              WHERE (trans_pe_det.IdPedidoEnc = @IdPedidoEnc) AND (trans_despacho_det.CantidadDespachada > 0) "

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim BeTransDespachoDet As clsBeTrans_despacho_det

                    For Each lRow As DataRow In lDT.Rows

                        BeTransDespachoDet = New clsBeTrans_despacho_det

                        Cargar(BeTransDespachoDet, lRow)

                        If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                            BeTransDespachoDet.Lote = CType(lRow("lote"), String)
                        End If

                        If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                            BeTransDespachoDet.Lic_plate = CType(lRow("lic_plate"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            BeTransDespachoDet.ProductoPresentacion = CType(lRow("Presentacion"), String)
                        End If

                        lReturnList.Add(BeTransDespachoDet)

                    Next

                    Get_All_By_IdPedidoEnc = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Tiempos_Despacho_By_Fechas(ByVal pFechaDel As Date,
                                                                  ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Tiempos_Despacho_By_IdPedidoEnc WHERE 1 > 0  "

            vSQL += String.Format(" AND CAST(Fecha_Pedido AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)
                        Get_Reporte_Tiempos_Despacho_By_Fechas = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                  ByVal IdDespachoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_despacho_det)

        Get_All_By_IdPedidoEnc = Nothing

        Dim lReturnList As New List(Of clsBeTrans_despacho_det)

        Dim vSQL As String = "SELECT trans_despacho_det.IdDespachoDet, trans_despacho_det.IdDespachoEnc, trans_despacho_det.IdPickingUbic, trans_despacho_det.Fecha, 
                              trans_despacho_det.user_agr, trans_despacho_det.fec_agr, trans_despacho_det.user_mod, trans_despacho_det.fec_mod, 
                              trans_despacho_det.activo, trans_despacho_det.IdPedidoEnc, trans_despacho_det.IdPedidoDet, trans_despacho_det.IdProductoBodega, 
                              trans_despacho_det.IdUnidadMedidaBasica, trans_despacho_det.IdPresentacion, trans_despacho_det.Codigo, 
                              trans_despacho_det.NombreProducto, trans_despacho_det.NombreEstado, trans_despacho_det.CantidadDespachada, 
                              trans_despacho_det.PesoDespachado, trans_despacho_det.IdProductoEstado, trans_picking_ubic.lote, trans_picking_ubic.lic_plate,
                              producto_presentacion.Nombre as Presentacion  
                              FROM  trans_pe_det INNER JOIN
                              trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                              trans_despacho_det ON trans_pe_det.IdPedidoDet = trans_despacho_det.IdPedidoDet INNER JOIN
                              trans_despacho_enc ON trans_despacho_det.IdDespachoEnc = trans_despacho_enc.IdDespachoEnc INNER JOIN
                              trans_picking_ubic ON trans_despacho_det.IdPickingUbic = trans_picking_ubic.IdPickingUbic LEFT JOIN
                              producto_presentacion ON trans_picking_ubic.IdPresentacion = producto_presentacion.IdPresentacion                             
                              WHERE (trans_pe_det.IdPedidoEnc = @IdPedidoEnc AND trans_despacho_det.IdDespachoEnc =@IdDespachoEnc) AND (trans_despacho_det.CantidadDespachada > 0) "

        Try

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim BeTransDespachoDet As clsBeTrans_despacho_det

                    For Each lRow As DataRow In lDT.Rows

                        BeTransDespachoDet = New clsBeTrans_despacho_det

                        Cargar(BeTransDespachoDet, lRow)

                        If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                            BeTransDespachoDet.Lote = CType(lRow("lote"), String)
                        End If

                        If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                            BeTransDespachoDet.Lic_plate = CType(lRow("lic_plate"), String)
                        End If

                        If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                            BeTransDespachoDet.ProductoPresentacion = CType(lRow("Presentacion"), String)
                        End If

                        lReturnList.Add(BeTransDespachoDet)

                    Next

                    Get_All_By_IdPedidoEnc = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                              ByVal pIdPedidoEnc As Integer,
                                                              ByVal pIdDespachoEnc As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPedidoDet = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BeBodega As New clsBeBodega

        Try

            Dim vSQL As String = "  SELECT d.IdPickingUbic, IdPickingEnc, IdPickingDet, pu.IdUbicacion, IdStock, pu.IdPropietarioBodega, 
		                                    pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, IdUbicacionAnterior, 
		                                    IdRecepcion, lote, fecha_vence, fecha_minima, serial, lic_plate, acepto, peso_solicitado, peso_recibido, 
		                                    peso_verificado, pu.peso_despachado, d.PesoDespachado, cantidad_solicitada, cantidad_recibida, d.CantidadDespachada cantidad_verificada, 
		                                    encontrado, dañado_verificacion, fecha_real_vence, no_packing, fecha_picking, fecha_verificado, 
		                                    fecha_packing, fecha_despachado, d.CantidadDespachada cantidad_despachada, d.user_agr, d.fec_agr, d.user_mod, d.fec_mod, 
		                                    pu.activo, pu.IdPedidoDet, dañado_picking, IdStockRes, lic_plate_reemplazo, IdUbicacion_reemplazo, 
		                                    IdStock_reemplazo, pu.IdBodega, IdOperadorBodega_Pickeo, IdOperadorBodega_Verifico, pu.IdPedidoEnc, 
		                                    no_encontrado, IdUbicacionTemporal, IdOperadorBodega_Asignado,
		                                    dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion, pu.IdBodega) Nombre_Ubicacion,producto.codigo Codigo_Producto,
		                                    producto.nombre Nombre_Producto, dbo.producto_presentacion.nombre nom_presentacion,
		                                    dbo.unidad_medida.Nombre nom_unid_med, dbo.producto_estado.nombre nom_Estado, pu.IdUnidadMedida IdUnidadMedidaBasica
                                    FROM dbo.trans_pe_det INNER JOIN
	                                     dbo.producto INNER JOIN
	                                     dbo.producto_bodega ON dbo.producto.IdProducto = dbo.producto_bodega.IdProducto 
	                                                         ON dbo.trans_pe_det.IdProductoBodega = dbo.producto_bodega.IdProductoBodega INNER JOIN
	                                     dbo.producto_estado INNER JOIN
	                                     dbo.trans_picking_ubic pu ON dbo.producto_estado.IdEstado = pu.IdProductoEstado INNER JOIN
	                                     dbo.propietario_bodega ON pu.IdPropietarioBodega = dbo.propietario_bodega.IdPropietarioBodega INNER JOIN
	                                     dbo.propietarios ON dbo.propietario_bodega.IdPropietario = dbo.propietarios.IdPropietario INNER JOIN
	                                     dbo.trans_despacho_det d ON pu.IdPickingUbic = d.IdPickingUbic AND 
								                                    pu.IdPedidoDet = d.IdPedidoDet AND
								                                    pu.IdProductoBodega = d.IdProductoBodega AND 
								                                    pu.IdUnidadMedida = d.IdUnidadMedidaBasica AND
	                                                                pu.IdPresentacion = d.IdPresentacion 
							                                    ON dbo.trans_pe_det.IdPedidoDet = pu.IdPedidoDet AND
	                                                               dbo.trans_pe_det.IdProductoBodega = pu.IdProductoBodega AND 
							                                       dbo.trans_pe_det.IdUnidadMedidaBasica = pu.IdUnidadMedida AND
							                                       dbo.trans_pe_det.IdEstado = pu.IdProductoEstado INNER JOIN
	                                     dbo.unidad_medida ON dbo.trans_pe_det.IdUnidadMedidaBasica = dbo.unidad_medida.IdUnidadMedida INNER JOIN
	                                     dbo.bodega_ubicacion ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND pu.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
	                                     dbo.bodega_tramo ON dbo.bodega_ubicacion.IdTramo = dbo.bodega_tramo.IdTramo AND dbo.bodega_ubicacion.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
	                                     dbo.bodega_sector ON dbo.bodega_tramo.IdSector = dbo.bodega_sector.IdSector AND dbo.bodega_tramo.IdBodega = dbo.bodega_sector.IdBodega INNER JOIN
	                                     dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega LEFT OUTER JOIN
	                                     dbo.producto_presentacion ON dbo.trans_pe_det.IdPresentacion = dbo.producto_presentacion.IdPresentacion 
                                   WHERE d.IdPedidoDet = @IdPedidoDet AND d.IdPedidoEnc = @IdPedidoEnc AND d.IdDespachoEnc = @IdDespachoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.CommandTimeout = 100
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", pIdPedidoDet))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdDespachoEnc", pIdDespachoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        clsLnTrans_picking_ubic.Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            'clsLnBodega_ubicacion.Obtener(.Ubicacion, pConnection, pTransaction)
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("Nombre_Ubicacion")), 0, lRow.Item("Nombre_Ubicacion"))

                            .IdPickingEnc = IIf(IsDBNull(lRow.Item("IdPickingEnc")), 0, lRow.Item("IdPickingEnc"))
                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .NombreProducto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .ProductoPresentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .ProductoEstado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .IdStockRes = IIf(IsDBNull(lRow.Item("IdStockRes")), 0, lRow.Item("IdStockRes"))
                            .IdStock = IIf(IsDBNull(lRow.Item("IdStock")), 0, lRow.Item("IdStock"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .IsNew = False

                        End With

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Despachada(ByVal pIdDespachoEnc As Integer,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As Double


        Try

            Dim lCantidadDespachada As Double = 0

            Const sp As String = "SELECT ISNULL(SUM(cantidaddespachada),0) 
                                  FROM trans_despacho_det 
                                  WHERE IdDespachoEnc = @IdDespachoEnc  "

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction
                lCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCantidadDespachada = CDbl(lReturnValue)
                End If

            End Using

            Return lCantidadDespachada

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
