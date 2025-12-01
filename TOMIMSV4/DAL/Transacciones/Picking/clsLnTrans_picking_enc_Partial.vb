Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_picking_enc

    Public Shared Function Get_IdUbicacion_Picking(ByVal pIdPickingEnc As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As Integer

        '#EJC20171022_0432PM: Quitar cable, se debería obtener una ubicación de piso por defecto,
        'pero puedo tener N de momento no sé como resolverlo y
        'Tengo prisa por hacer el rollback del stock en el pedido cuando tiene un picking asociado,
        'así que no tengo tiempo para pensar en esa banalidad.
        Get_IdUbicacion_Picking = 4

        Try

            Dim vSQL As String = "SELECT IdUbicacionPicking 
                                  FROM trans_picking_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc AND estado <> 'Anulado' AND activo = 1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                        Get_IdUbicacion_Picking = CType(lRow("IdUbicacionPicking"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPickingEnc As Integer) As clsBeTrans_picking_enc

        GetSingle = Nothing

        Try

            Dim sSQL As String = ""

            sSQL = "SELECT b.descripcion AS UbicacionPicking, enc.* 
                    FROM trans_picking_enc AS enc 
                    INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                    AND enc.IdBodega = b.IdBodega
                    WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                            Cargar(BeTransPickingEnc, lRow)

                            If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                                BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                                BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)

                            End If

                            BeTransPickingEnc.IsNew = False

                            BeTransPickingEnc.ListaPickingDet = clsLnTrans_picking_det.Get_All_Picking_Det_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                           lConnection,
                                                                                                                           lTransaction)

                            BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                             BeTransPickingEnc.IdBodega,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                            GetSingle = BeTransPickingEnc

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return GetSingle

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPickingEnc As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionPicking, enc.*, c.nombre as nombre_bodega
                                  FROM trans_picking_enc AS enc 
                                  INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                                  INNER JOIN bodega as c on c.idbodega = enc.IdBodega
                                  WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                    Cargar(BeTransPickingEnc, lRow)

                    If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                        BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                        BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)
                        BeTransPickingEnc.NombreBodega = CType(lRow("nombre_bodega"), String)

                    End If

                    BeTransPickingEnc.IsNew = False

                    BeTransPickingEnc.ListaPickingDet = clsLnTrans_picking_det.Get_All_Picking_Det_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                    BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                     BeTransPickingEnc.IdBodega,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                    Return BeTransPickingEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20220624 Copia de GetSingle para la HH por que no vamos a llenar el detalle de pdet ni el picking ubic del pdet
    Public Shared Function GetSingleHH(ByVal pIdPickingEnc As Integer,
                                       ByRef lConnection As SqlConnection,
                                       ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_enc

        GetSingleHH = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionPicking, enc.* 
                                  FROM trans_picking_enc AS enc 
                                  INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                                  WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                    Cargar(BeTransPickingEnc, lRow)

                    If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                        BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                        BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)

                    End If

                    BeTransPickingEnc.IsNew = False

                    BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                     BeTransPickingEnc.IdBodega,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                    Return BeTransPickingEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Picking WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " And activo=1"
            Else
                vSQL += " And activo=0"
            End If

            vSQL += String.Format(" And cast([Fecha Picking] AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.Fill(lTable)
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

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdBodega As Integer) As DataTable

        Get_All_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Picking WHERE IdBodega = @IdBodega  "

            If pActivo = True Then
                vSQL += " And activo=1"
            Else
                vSQL += " And activo=0"
            End If

            vSQL += String.Format(" And cast([Fecha Picking] AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("Result")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.Fill(lTable)
                        Get_All_By_IdBodega = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Pedido(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_enc)

        Get_All_By_Pedido = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_enc)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT trans_picking_enc.IdPickingEnc, trans_pe_enc.IdPedidoEnc, propietarios.nombre_comercial, trans_picking_enc.IdUbicacionPicking, 
                                         bodega_ubicacion.descripcion AS UbicacionPicking, trans_picking_enc.fecha_picking, trans_picking_enc.hora_ini, trans_picking_enc.hora_fin, 
                                         trans_picking_enc.estado, trans_picking_enc.detalle_operador, trans_picking_enc.activo, bodega.nombre AS Bodega,bodega.IdBodega
                                         FROM  trans_picking_enc INNER JOIN
                                         trans_picking_det ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc INNER JOIN
                                         trans_pe_det ON trans_picking_det.IdPedidoDet = trans_pe_det.IdPedidoDet INNER JOIN
                                         trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                                         bodega ON trans_picking_enc.IdBodega = bodega.IdBodega AND trans_pe_enc.IdBodega = bodega.IdBodega INNER JOIN
                                         propietario_bodega ON trans_picking_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND 
                                         trans_pe_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND bodega.IdBodega = propietario_bodega.IdBodega INNER JOIN
                                         propietarios ON propietario_bodega.IdPropietario = propietarios.IdPropietario INNER JOIN
                                         bodega_ubicacion ON trans_picking_enc.IdUbicacionPicking = bodega_ubicacion.IdUbicacion
                                         WHERE trans_pe_enc.IdPedidoEnc=@IdPedidoEnc AND trans_picking_enc.activo=1
                                         GROUP BY trans_picking_enc.IdPickingEnc, trans_pe_enc.IdPedidoEnc, propietarios.nombre_comercial, bodega_ubicacion.descripcion, 
                                         trans_picking_enc.IdUbicacionPicking, trans_picking_enc.fecha_picking, trans_picking_enc.hora_ini, trans_picking_enc.hora_fin, 
                                         trans_picking_enc.estado, trans_picking_enc.detalle_operador, trans_picking_enc.activo, bodega.nombre,bodega.IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_enc

                                If lRow("IdPedidoEnc") IsNot DBNull.Value AndAlso lRow("IdPedidoEnc") IsNot Nothing Then
                                    Obj.IdPedidoEnc = CType(lRow("IdPedidoEnc"), Integer)
                                End If

                                If lRow("IdPickingEnc") IsNot DBNull.Value AndAlso lRow("IdPickingEnc") IsNot Nothing Then
                                    Obj.IdPickingEnc = CType(lRow("IdPickingEnc"), Integer)
                                End If

                                If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                    Obj.NombreBodega = CType(lRow("Bodega"), String)
                                End If

                                If lRow("nombre_comercial") IsNot DBNull.Value AndAlso lRow("nombre_comercial") IsNot Nothing Then
                                    Obj.NombrePropietarioPicking = CType(lRow("nombre_comercial"), String)
                                End If

                                If lRow("UbicacionPicking") IsNot DBNull.Value AndAlso lRow("UbicacionPicking") IsNot Nothing Then
                                    Obj.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                                    Obj.UbicacionPicking.IdUbicacion = Obj.IdUbicacionPicking
                                    Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                                    Obj.UbicacionPicking = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacionPicking, Obj.IdBodega, lConnection, lTransaction)
                                    Obj.NombreUbicacionPicking = Obj.UbicacionPicking.NombreCompleto
                                End If

                                If lRow("fecha_picking") IsNot DBNull.Value AndAlso lRow("fecha_picking") IsNot Nothing Then
                                    Obj.Fecha_picking = CType(lRow("fecha_picking"), Date)
                                End If

                                If lRow("hora_ini") IsNot DBNull.Value AndAlso lRow("hora_ini") IsNot Nothing Then
                                    Obj.Hora_ini = CType(lRow("hora_ini"), Date)
                                End If

                                If lRow("hora_fin") IsNot DBNull.Value AndAlso lRow("hora_fin") IsNot Nothing Then
                                    Obj.Hora_fin = CType(lRow("hora_fin"), Date)
                                End If

                                If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("estado"), String)
                                End If

                                If lRow("detalle_operador") IsNot DBNull.Value AndAlso lRow("detalle_operador") IsNot Nothing Then
                                    Obj.Detalle_operador = CType(lRow("detalle_operador"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_All_By_Pedido = lReturnList

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

    Public Shared Function Get_All_By_Pedido(ByVal pIdPedidoEnc As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_enc)

        Get_All_By_Pedido = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_enc)

            Dim vSQL As String = "SELECT trans_picking_enc.IdPickingEnc, trans_pe_enc.IdPedidoEnc, propietarios.nombre_comercial, trans_picking_enc.IdUbicacionPicking, 
                                         bodega_ubicacion.descripcion AS UbicacionPicking, trans_picking_enc.fecha_picking, trans_picking_enc.hora_ini, trans_picking_enc.hora_fin, 
                                         trans_picking_enc.estado, trans_picking_enc.detalle_operador, trans_picking_enc.activo, bodega.nombre AS Bodega,bodega.IdBodega
                                         FROM  trans_picking_enc INNER JOIN
                                         trans_picking_det ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc INNER JOIN
                                         trans_pe_det ON trans_picking_det.IdPedidoDet = trans_pe_det.IdPedidoDet INNER JOIN
                                         trans_pe_enc ON trans_pe_det.IdPedidoEnc = trans_pe_enc.IdPedidoEnc INNER JOIN
                                         bodega ON trans_picking_enc.IdBodega = bodega.IdBodega AND trans_pe_enc.IdBodega = bodega.IdBodega INNER JOIN
                                         propietario_bodega ON trans_picking_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND 
                                         trans_pe_enc.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND bodega.IdBodega = propietario_bodega.IdBodega INNER JOIN
                                         propietarios ON propietario_bodega.IdPropietario = propietarios.IdPropietario INNER JOIN
                                         bodega_ubicacion ON trans_picking_enc.IdUbicacionPicking = bodega_ubicacion.IdUbicacion
                                         WHERE trans_pe_enc.IdPedidoEnc=@IdPedidoEnc AND trans_picking_enc.activo=1
                                         GROUP BY trans_picking_enc.IdPickingEnc, trans_pe_enc.IdPedidoEnc, propietarios.nombre_comercial, bodega_ubicacion.descripcion, 
                                         trans_picking_enc.IdUbicacionPicking, trans_picking_enc.fecha_picking, trans_picking_enc.hora_ini, trans_picking_enc.hora_fin, 
                                         trans_picking_enc.estado, trans_picking_enc.detalle_operador, trans_picking_enc.activo, bodega.nombre,bodega.IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_enc

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_enc

                        If lRow("IdPedidoEnc") IsNot DBNull.Value AndAlso lRow("IdPedidoEnc") IsNot Nothing Then
                            Obj.IdPedidoEnc = CType(lRow("IdPedidoEnc"), Integer)
                        End If

                        If lRow("IdPickingEnc") IsNot DBNull.Value AndAlso lRow("IdPickingEnc") IsNot Nothing Then
                            Obj.IdPickingEnc = CType(lRow("IdPickingEnc"), Integer)
                        End If

                        If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                            Obj.NombreBodega = CType(lRow("Bodega"), String)
                        End If

                        If lRow("nombre_comercial") IsNot DBNull.Value AndAlso lRow("nombre_comercial") IsNot Nothing Then
                            Obj.NombrePropietarioPicking = CType(lRow("nombre_comercial"), String)
                        End If

                        If lRow("UbicacionPicking") IsNot DBNull.Value AndAlso lRow("UbicacionPicking") IsNot Nothing Then
                            Obj.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                            Obj.UbicacionPicking.IdUbicacion = Obj.IdUbicacionPicking
                            Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                            Obj.UbicacionPicking = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacionPicking, Obj.IdBodega, lConnection, lTransaction)
                            Obj.NombreUbicacionPicking = Obj.UbicacionPicking.NombreCompleto
                        End If

                        If lRow("fecha_picking") IsNot DBNull.Value AndAlso lRow("fecha_picking") IsNot Nothing Then
                            Obj.Fecha_picking = CType(lRow("fecha_picking"), Date)
                        End If

                        If lRow("hora_ini") IsNot DBNull.Value AndAlso lRow("hora_ini") IsNot Nothing Then
                            Obj.Hora_ini = CType(lRow("hora_ini"), Date)
                        End If

                        If lRow("hora_fin") IsNot DBNull.Value AndAlso lRow("hora_fin") IsNot Nothing Then
                            Obj.Hora_fin = CType(lRow("hora_fin"), Date)
                        End If

                        If lRow("estado") IsNot DBNull.Value AndAlso lRow("estado") IsNot Nothing Then
                            Obj.Estado = CType(lRow("estado"), String)
                        End If

                        If lRow("detalle_operador") IsNot DBNull.Value AndAlso lRow("detalle_operador") IsNot Nothing Then
                            Obj.Detalle_operador = CType(lRow("detalle_operador"), String)
                        End If

                        lReturnList.Add(Obj)

                    Next

                    Get_All_By_Pedido = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Detalle_By_Pedido(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_det)

        Get_All_Detalle_By_Pedido = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_det)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT dbo.trans_picking_ubic.IdPickingUbic, 
                                            res.IdPicking AS IdPickingEnc, 
                                            res.IdPedido, 
                                            p.codigo, 
                                            p.nombre, ISNULL(pp.nombre, '') AS Presentación, pp.IdPresentacion, ISNULL(pp.factor, 0) AS Factor, pe.nombre AS Estado, um.Nombre AS UMBas, 
                                            pr.nombre_comercial AS Propietario, dbo.Nombre_Completo_Ubicacion(bu.IdBodega, bu.IdUbicacion) AS Ubicación, ISNULL(s.cantidad / pp.factor, 0) AS Cantidad, res.IdPedidoDet, res.IdUbicacion, 
                                            trans_picking_ubic.cantidad_solicitada AS CantidadReservada, 
                                            trans_picking_ubic.cantidad_recibida AS Cantidad_Pickeada, 
                                            trans_picking_ubic.cantidad_verificada AS Cantidad_Verificada, trans_picking_ubic.cantidad_despachada,
                                            ISNULL(s.cantidad, 0) 
                                            AS Cantidad_Stock, res.fecha_ingreso, res.fecha_vence, res.lote, res.lic_plate
                                            FROM stock_res AS res INNER JOIN
                                            propietario_bodega AS prb ON res.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                                            producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN
                                            producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                                            bodega_ubicacion AS bu ON res.IdUbicacion = bu.IdUbicacion INNER JOIN
                                            bodega_tramo ON bu.IdTramo = bodega_tramo.IdTramo AND bu.IdSector = bodega_tramo.IdSector AND bu.IdArea = bodega_tramo.IdArea AND bu.IdBodega = bodega_tramo.IdBodega INNER JOIN
                                            producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado INNER JOIN
                                            unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
                                            propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                                            trans_picking_ubic ON bu.IdUbicacion = trans_picking_ubic.IdUbicacion AND trans_picking_ubic.IdStockRes = res.IdStockRes INNER JOIN
                                            trans_picking_det ON trans_picking_ubic.IdPickingDet = trans_picking_det.IdPickingDet AND res.IdPedidoDet = trans_picking_det.IdPedidoDet INNER JOIN
                                            trans_picking_enc ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc INNER JOIN
                                            trans_pe_enc ON trans_pe_enc.IdPickingEnc = trans_picking_det.IdPickingEnc LEFT OUTER JOIN
                                            stock AS s ON res.IdStock = s.IdStock AND res.IDBODEGA = s.idbodega LEFT OUTER JOIN
                                            producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
                                            WHERE (res.IdPedido = @IdPedido) and trans_picking_ubic.dañado_picking = 0 and trans_picking_ubic.dañado_verificacion = 0
                                            UNION
                                            SELECT trans_picking_ubic.IdPickingUbic, 
                                            trans_picking_enc.IdPickingEnc, 
                                            0 AS IdPedido, 
                                            p.codigo, 
                                            p.nombre, 
                                            ISNULL(pp.nombre, '') AS Presentación, pp.IdPresentacion, ISNULL(pp.factor, 0) AS Factor, pe.nombre AS Estado, 
                                            um.Nombre AS UMBas, pr.nombre_comercial AS Propietario, dbo.Nombre_Completo_Ubicacion(bu.IdBodega, bu.IdUbicacion) AS Ubicación, ISNULL(s.cantidad / pp.factor, 0) AS Cantidad, trans_picking_det.IdPedidoDet, 
                                            trans_picking_ubic.IdUbicacion, trans_picking_det.cantidad AS CantidadReservada, trans_picking_ubic.cantidad_recibida AS Cantidad_Pickeada, 
                                            trans_picking_ubic.cantidad_verificada AS Cantidad_Verificada, trans_picking_ubic.cantidad_despachada,
                                            s.cantidad AS Cantidad_Stock, trans_picking_ubic.fecha_picking, trans_picking_ubic.fecha_vence, trans_picking_ubic.lote, trans_picking_ubic.lic_plate,
                                            trans_picking_ubic.no_encontrado
                                            FROM trans_picking_ubic INNER JOIN
                                            trans_picking_det ON trans_picking_ubic.IdPickingDet = trans_picking_det.IdPickingDet INNER JOIN
                                            trans_picking_enc ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc INNER JOIN
                                            trans_pe_det ON trans_pe_det.IdPedidoDet = trans_picking_det.IdPedidoDet INNER JOIN
                                            propietario_bodega AS prb ON prb.IdPropietarioBodega = trans_picking_ubic.IdPropietarioBodega INNER JOIN
                                            producto_bodega AS pb ON pb.IdProductoBodega = trans_picking_ubic.IdProductoBodega INNER JOIN
                                            producto AS p ON pb.IdProducto = p.IdProducto INNER JOIN
                                            bodega_ubicacion AS bu ON trans_picking_ubic.IdUbicacion = bu.IdUbicacion INNER JOIN
                                            bodega_tramo ON bu.IdTramo = bodega_tramo.IdTramo AND bu.IdSector = bodega_tramo.IdSector AND bu.IdArea = bodega_tramo.IdArea AND bu.IdBodega = bodega_tramo.IdBodega INNER JOIN
                                            producto_estado AS pe ON trans_picking_ubic.IdProductoEstado = pe.IdEstado INNER JOIN
                                            unidad_medida AS um ON trans_picking_ubic.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
                                            stock AS s ON trans_picking_ubic.IdStock = s.IdStock INNER JOIN
                                            propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                                            producto_presentacion AS pp ON trans_picking_ubic.IdPresentacion = pp.IdPresentacion
						                    WHERE  (trans_pe_det.IdPedidoEnc = @IdPedido) AND trans_picking_enc.IdPickingEnc 
                                            NOT IN (SELECT IdPickingEnc FROM trans_pe_Enc WHERE IdPedidoEnc = @IdPedido) and 
                                            trans_picking_ubic.dañado_picking = 0 and trans_picking_ubic.dañado_verificacion = 0
                                            and trans_picking_ubic.no_encontrado = 0"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedido", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_det

                                If lRow("IdPedido") IsNot DBNull.Value AndAlso lRow("IdPedido") IsNot Nothing Then
                                    Obj.IdPedidoEnc = CType(lRow("IdPedido"), Integer)
                                End If

                                If lRow("IdPickingEnc") IsNot DBNull.Value AndAlso lRow("IdPickingEnc") IsNot Nothing Then
                                    Obj.IdPickingEnc = CType(lRow("IdPickingEnc"), Integer)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("codigo"), String)
                                End If

                                If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                    Obj.NombreProducto = CType(lRow("nombre"), String)
                                End If

                                If lRow("fecha_ingreso") IsNot DBNull.Value AndAlso lRow("fecha_ingreso") IsNot Nothing Then
                                    Obj.Fecha_Ingreso = CType(lRow("fecha_ingreso"), Date)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("Estado"), String)
                                End If

                                If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                                    Obj.Factorx = CType(lRow("Factor"), Double)
                                End If

                                If lRow("UMBas") IsNot DBNull.Value AndAlso lRow("UMBas") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("UMBas"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Ubicación") IsNot DBNull.Value AndAlso lRow("Ubicación") IsNot Nothing Then
                                    Obj.UbicacionPicking = CType(lRow("Ubicación"), String)
                                End If

                                If lRow("Presentación") IsNot DBNull.Value AndAlso lRow("Presentación") IsNot Nothing Then
                                    Obj.Presentacion.Nombre = CType(lRow("Presentación"), String)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad"), Double)
                                End If

                                If lRow("IdPedidoDet") IsNot DBNull.Value AndAlso lRow("IdPedidoDet") IsNot Nothing Then
                                    Obj.IdPedidoDet = CType(lRow("IdPedidoDet"), Integer)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                End If

                                If lRow("CantidadReservada") IsNot DBNull.Value AndAlso lRow("CantidadReservada") IsNot Nothing Then
                                    Obj.CantidadReservada = CType(lRow("CantidadReservada"), Double)
                                End If

                                If lRow("Cantidad_Pickeada") IsNot DBNull.Value AndAlso lRow("Cantidad_Pickeada") IsNot Nothing Then
                                    Obj.Cantidad_Pickeada = CType(lRow("Cantidad_Pickeada"), Double)
                                End If

                                If lRow("Cantidad_Verificada") IsNot DBNull.Value AndAlso lRow("Cantidad_Verificada") IsNot Nothing Then
                                    Obj.Cantidad_Verificada = CType(lRow("Cantidad_Verificada"), Double)
                                End If

                                If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                                    Obj.Cantidad_Stock = CType(lRow("Cantidad_Stock"), Double)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), String)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.Lic_Plate = CType(lRow("lic_plate"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                            Get_All_Detalle_By_Pedido = lReturnList

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

    ''' <summary>
    ''' #EJC20221005_1722: Corrección, optimización y mejora en Query Para obtener los picking activos por pedido.
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Detalle_By_Pedido(ByVal pIdPedidoEnc As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det)

        Get_All_Detalle_By_Pedido = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_picking_det)

            Dim vSQL As String = "SELECT trans_picking_ubic.IdPickingUbic, 
                                  res.IdPicking AS IdPickingEnc, 
                                  res.IdPedido, 
                                  p.codigo, 
                                  p.nombre, ISNULL(pp.nombre, '') AS Presentación, pp.IdPresentacion, ISNULL(pp.factor, 0) AS Factor, pe.nombre AS Estado, um.Nombre AS UMBas, 
                                  pr.nombre_comercial AS Propietario, dbo.Nombre_Completo_Ubicacion(bu.IdBodega, bu.IdUbicacion) AS Ubicación, ISNULL(s.cantidad / pp.factor, 0) AS Cantidad, res.IdPedidoDet, res.IdUbicacion, 
                                  trans_picking_ubic.cantidad_solicitada AS CantidadReservada, 
                                  trans_picking_ubic.cantidad_recibida AS Cantidad_Pickeada, 
                                  trans_picking_ubic.cantidad_verificada AS Cantidad_Verificada, trans_picking_ubic.cantidad_despachada,
                                  ISNULL(s.cantidad, 0) 
                                  AS Cantidad_Stock, res.fecha_ingreso, res.fecha_vence, res.lote, res.lic_plate
                                  FROM stock_res AS res 
                                  INNER JOIN propietario_bodega AS prb ON res.IdPropietarioBodega = prb.IdPropietarioBodega 
                                  INNER JOIN producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega 
                                  INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                                  INNER JOIN bodega_ubicacion AS bu ON res.IdUbicacion = bu.IdUbicacion                                   
                                  INNER JOIN producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado 
                                  INNER JOIN unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida 
                                  INNER JOIN propietarios AS pr ON prb.IdPropietario = pr.IdPropietario 
                                  INNER JOIN trans_picking_ubic ON bu.IdUbicacion = trans_picking_ubic.IdUbicacion 
                                  AND bu.IdBodega = trans_picking_ubic.IdBodega   
                                  AND trans_picking_ubic.IdStockRes = res.IdStockRes 
                                  INNER JOIN trans_picking_det ON trans_picking_ubic.IdPickingDet = trans_picking_det.IdPickingDet AND res.IdPedidoDet = trans_picking_det.IdPedidoDet 
                                  INNER JOIN trans_picking_enc ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc 
                                  INNER JOIN trans_pe_enc ON trans_pe_enc.IdPickingEnc = trans_picking_det.IdPickingEnc 
                                  LEFT OUTER JOIN stock AS s ON res.IdStock = s.IdStock AND res.IDBODEGA = s.idbodega 
                                  LEFT OUTER JOIN producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
                                  WHERE (res.IdPedido = @IdPedido) and trans_picking_ubic.dañado_picking = 0 
                                  AND trans_picking_ubic.dañado_verificacion = 0
                                  AND trans_picking_ubic.no_encontrado = 0
                                  AND trans_picking_enc.estado <> 'Anulado' 
                                  AND trans_picking_enc.activo =1  
                                  UNION
                                  SELECT trans_picking_ubic.IdPickingUbic, 
                                  trans_picking_enc.IdPickingEnc, 
                                  0 AS IdPedido, 
                                  p.codigo, 
                                  p.nombre, 
                                  ISNULL(pp.nombre, '') AS Presentación, pp.IdPresentacion, ISNULL(pp.factor, 0) AS Factor, pe.nombre AS Estado, 
                                  um.Nombre AS UMBas, pr.nombre_comercial AS Propietario, dbo.Nombre_Completo_Ubicacion(bu.IdBodega, bu.IdUbicacion) AS Ubicación, ISNULL(s.cantidad / pp.factor, 0) AS Cantidad, trans_picking_det.IdPedidoDet, 
                                  trans_picking_ubic.IdUbicacion, trans_picking_det.cantidad AS CantidadReservada, trans_picking_ubic.cantidad_recibida AS Cantidad_Pickeada, 
                                  trans_picking_ubic.cantidad_verificada AS Cantidad_Verificada, trans_picking_ubic.cantidad_despachada,
                                  s.cantidad AS Cantidad_Stock, trans_picking_ubic.fecha_picking, trans_picking_ubic.fecha_vence, trans_picking_ubic.lote, trans_picking_ubic.lic_plate
                                  FROM trans_picking_ubic INNER JOIN
                                  trans_picking_det ON trans_picking_ubic.IdPickingDet = trans_picking_det.IdPickingDet 
                                  INNER JOIN trans_picking_enc ON trans_picking_enc.IdPickingEnc = trans_picking_det.IdPickingEnc 
                                  INNER JOIN trans_pe_det ON trans_pe_det.IdPedidoDet = trans_picking_det.IdPedidoDet 
                                  INNER JOIN propietario_bodega AS prb ON prb.IdPropietarioBodega = trans_picking_ubic.IdPropietarioBodega 
                                  INNER JOIN producto_bodega AS pb ON pb.IdProductoBodega = trans_picking_ubic.IdProductoBodega 
                                  INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                                  INNER JOIN bodega_ubicacion AS bu ON trans_picking_ubic.IdUbicacion = bu.IdUbicacion                                   
                                  AND bu.IdBodega = trans_picking_ubic.IdBodega 
                                  INNER JOIN producto_estado AS pe ON trans_picking_ubic.IdProductoEstado = pe.IdEstado 
                                  INNER JOIN unidad_medida AS um ON trans_picking_ubic.IdUnidadMedida = um.IdUnidadMedida 
                                  LEFT OUTER JOIN stock AS s ON trans_picking_ubic.IdStock = s.IdStock 
                                  INNER JOIN propietarios AS pr ON prb.IdPropietario = pr.IdPropietario 
                                  LEFT OUTER JOIN producto_presentacion AS pp ON trans_picking_ubic.IdPresentacion = pp.IdPresentacion
						          WHERE  (trans_pe_det.IdPedidoEnc = @IdPedido) AND trans_picking_enc.IdPickingEnc 
                                  NOT IN (SELECT IdPickingEnc FROM trans_pe_Enc WHERE IdPedidoEnc = @IdPedido) 
                                  AND trans_picking_ubic.dañado_picking = 0 and trans_picking_ubic.dañado_verificacion = 0 
                                  AND trans_picking_ubic.no_encontrado = 0 
                                  AND trans_picking_enc.estado <> 'Anulado' 
                                  AND trans_picking_enc.activo =1  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedido", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_det

                        If lRow("IdPedido") IsNot DBNull.Value AndAlso lRow("IdPedido") IsNot Nothing Then
                            Obj.IdPedidoEnc = CType(lRow("IdPedido"), Integer)
                        End If

                        If lRow("IdPickingEnc") IsNot DBNull.Value AndAlso lRow("IdPickingEnc") IsNot Nothing Then
                            Obj.IdPickingEnc = CType(lRow("IdPickingEnc"), Integer)
                        End If

                        If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                            Obj.Codigo = CType(lRow("codigo"), String)
                        End If

                        If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                            Obj.NombreProducto = CType(lRow("nombre"), String)
                        End If

                        If lRow("fecha_ingreso") IsNot DBNull.Value AndAlso lRow("fecha_ingreso") IsNot Nothing Then
                            Obj.Fecha_Ingreso = CType(lRow("fecha_ingreso"), Date)
                        End If

                        If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                            Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                        End If

                        If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                            Obj.Estado = CType(lRow("Estado"), String)
                        End If

                        If lRow("Factor") IsNot DBNull.Value AndAlso lRow("Factor") IsNot Nothing Then
                            Obj.Factorx = CType(lRow("Factor"), Double)
                        End If

                        If lRow("UMBas") IsNot DBNull.Value AndAlso lRow("UMBas") IsNot Nothing Then
                            Obj.UMBas = CType(lRow("UMBas"), String)
                        End If

                        If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                            Obj.Propietario = CType(lRow("Propietario"), String)
                        End If

                        If lRow("Ubicación") IsNot DBNull.Value AndAlso lRow("Ubicación") IsNot Nothing Then
                            Obj.UbicacionPicking = CType(lRow("Ubicación"), String)
                        End If

                        If lRow("Presentación") IsNot DBNull.Value AndAlso lRow("Presentación") IsNot Nothing Then
                            Obj.Presentacion.Nombre = CType(lRow("Presentación"), String)
                        End If

                        If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                            Obj.Cantidad = CType(lRow("Cantidad"), Double)
                        End If

                        If lRow("IdPedidoDet") IsNot DBNull.Value AndAlso lRow("IdPedidoDet") IsNot Nothing Then
                            Obj.IdPedidoDet = CType(lRow("IdPedidoDet"), Integer)
                        End If

                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                            Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                        End If

                        If lRow("CantidadReservada") IsNot DBNull.Value AndAlso lRow("CantidadReservada") IsNot Nothing Then
                            Obj.CantidadReservada = CType(lRow("CantidadReservada"), Double)
                        End If

                        If lRow("Cantidad_Pickeada") IsNot DBNull.Value AndAlso lRow("Cantidad_Pickeada") IsNot Nothing Then
                            Obj.Cantidad_Pickeada = CType(lRow("Cantidad_Pickeada"), Double)
                        End If

                        If lRow("Cantidad_Verificada") IsNot DBNull.Value AndAlso lRow("Cantidad_Verificada") IsNot Nothing Then
                            Obj.Cantidad_Verificada = CType(lRow("Cantidad_Verificada"), Double)
                        End If

                        If lRow("Cantidad_Stock") IsNot DBNull.Value AndAlso lRow("Cantidad_Stock") IsNot Nothing Then
                            Obj.Cantidad_Stock = CType(lRow("Cantidad_Stock"), Double)
                        End If

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), String)
                        End If

                        If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                            Obj.Lote = CType(lRow("lote"), String)
                        End If

                        If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                            Obj.Lic_Plate = CType(lRow("lic_plate"), String)
                        End If

                        lReturnList.Add(Obj)

                    Next

                    Get_All_Detalle_By_Pedido = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Referencias_By_IdPicking(ByVal pIdPickingEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As String

        Get_All_Referencias_By_IdPicking = ""

        Try

            Dim lReturn As String = ""

            Dim vSQL As String = "SELECT DISTINCT p.referencia 
                                  FROM trans_picking_det k INNER JOIN 
                                       trans_pe_enc p on k.IdPedidoEnc = p.IdPedidoEnc
                                  WHERE k.IdPickingEnc = @IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        lReturn = lRow("referencia") & ", "

                    Next

                    Get_All_Referencias_By_IdPicking = lReturn.Substring(1, lReturn.Length - 2)

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPickingEnc),0) FROM trans_picking_enc "

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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

    Private Shared Sub Anular_Encabezado(ByVal pIdPickingEnc As Integer,
                                         ByVal pIdUsuarioAnulo As Integer,
                                         ByVal pConnection As SqlConnection,
                                         ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE trans_picking_enc 
                                  SET Estado='Anulado',
                                  user_mod = @user_mod,
                                  fec_mod = @fec_mod  
                                  WHERE IdPickingEnc=@IdPickingEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.Transaction = pTransaction
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                lCommand.Parameters.AddWithValue("@user_mod", pIdUsuarioAnulo)
                lCommand.Parameters.AddWithValue("@fec_mod", Now)
                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Guardar(ByVal pBeTrans_picking_enc As clsBeTrans_picking_enc,
                                   ByVal pBeTareaHH As clsBeTarea_hh,
                                   ByVal pListBePickingDet As List(Of clsBeTrans_picking_det),
                                   ByVal pListBePickingDetParametros As List(Of clsBeTrans_picking_det_parametros),
                                   ByVal pListBePickingOpe As List(Of clsBeTrans_picking_op),
                                   ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic)) As Boolean

        Guardar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#GT01022023: Obtener el IdPickingEnc
            Dim vBePickingEncOriginal As New clsBeTrans_picking_enc
            vBePickingEncOriginal.IdPickingEnc = pBeTrans_picking_enc.IdPickingEnc

            '#EJC202301301011:Determinar si se cambió la bandera de procesado BOF después de que el operador pickeara con HH.
            If Not pBeTrans_picking_enc.IsNew Then
                If GetSingle(vBePickingEncOriginal, lConnection, lTransaction) Then
                    If Not vBePickingEncOriginal Is Nothing Then
                        If Not vBePickingEncOriginal.procesado_bof Then
                            If pBeTrans_picking_enc.procesado_bof Then

                                Dim vMensajeLog As String = "Advertencia_202303032146: Se actualizó a procesado_bof el picking_enc: " & pBeTrans_picking_enc.IdPickingEnc

                                '#MECR23102025: Se agrego bitacora para logs de picking
                                'clsLnLog_error_wms.Agregar_Error(vMensajeLog)
                                clsLnLog_error_wms_pick.Agregar_Error(vMensajeLog,
                                                                      pIdPedidoEnc:=pBeTrans_picking_enc.IdPedidoEnc,
                                                                      pIdPickingEnc:=pBeTrans_picking_enc.IdPickingEnc,
                                                                      pUserAgr:=pBeTrans_picking_enc.User_mod,
                                                                      pIdBodega:=pBeTrans_picking_enc.IdBodega,
                                                                      pConection:=lConnection,
                                                                      pTransaction:=lTransaction)
                            End If
                        End If
                    End If
                End If
            End If

            ' Picking Encabezado
            Guarda_Trans_picking_enc(pBeTrans_picking_enc,
                                     lConnection,
                                     lTransaction)

            pBeTareaHH.IdTransaccion = pBeTrans_picking_enc.IdPickingEnc

            'Tarea de picking.
            clsLnTarea_hh.Guardar_Tarea_Picking_HH(pBeTareaHH,
                                                   lConnection,
                                                   lTransaction)

            ' Picking Detalle
            clsLnTrans_picking_det.Guarda_Trans_picking_det(pBeTrans_picking_enc.IdPickingEnc,
                                                            pBeTrans_picking_enc.IdBodegaMuelle,
                                                            pListBePickingDet,
                                                            pListBePickingUbic,
                                                            lConnection,
                                                            lTransaction)

            ' Picking Detalle Parametros
            clsLnTrans_picking_det_parametros.Guarda_Trans_picking_parametros(pListBePickingDetParametros,
                                                                              lConnection,
                                                                              lTransaction)

            ' Picking Detalle Ubicacion
            clsLnTrans_picking_ubic.Guarda_Trans_picking_ubic(pBeTrans_picking_enc.IdPickingEnc,
                                                              pListBePickingUbic,
                                                              lConnection,
                                                              lTransaction)


            ' Picking Detalle Operador
            clsLnTrans_picking_op.Guarda_Trans_picking_operador(pBeTrans_picking_enc.IdPickingEnc,
                                                                pListBePickingOpe,
                                                                lConnection,
                                                                lTransaction)

            Guardar = True

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_Picking(ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                                              ByVal IdBodegaMuelle As Integer,
                                              ByVal pBePedidoDetList As List(Of clsBeTrans_pe_det)) As Boolean

        Actualizar_Picking = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)

            Dim pListBePickingDet As New List(Of clsBeTrans_picking_det)

            For Each BeTransPeDet As clsBeTrans_pe_det In pBePedidoDetList

                If BeTransPeDet.IsNew Then

                    BeTransPeDet.ListaStockRes = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoDet(BeTransPeDet.IdPedidoDet, lConnection, lTransaction)

                    Dim BePickingDet As New clsBeTrans_picking_det()
                    BePickingDet.IdPickingDet = clsLnTrans_picking_det.MaxID(lConnection, lTransaction) + 1
                    BePickingDet.IdPickingEnc = pBePedidoEnc.IdPickingEnc
                    BePickingDet.IdPedidoDet = BeTransPeDet.IdPedidoDet
                    BePickingDet.Cantidad = BeTransPeDet.Cantidad
                    BePickingDet.Cliente_dias = 0
                    BePickingDet.User_agr = BeTransPeDet.User_agr
                    BePickingDet.Fec_agr = Date.Now
                    BePickingDet.User_mod = BeTransPeDet.User_agr
                    BePickingDet.Fec_mod = Date.Now
                    pBePedidoEnc.Activo = True
                    BePickingDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc

                    For Each BeStockRes As clsBeStock_res In BeTransPeDet.ListaStockRes

                        Dim BeTransPickingUbic As New clsBeTrans_picking_ubic()

                        Dim BePresentacion As New clsBeProducto_Presentacion

                        With BeTransPickingUbic

                            If Val(BeStockRes.Cantidad) = 0 Then

                                Dim vMensaje As String = String.Format("Una línea del picking es inconsistente, 
                                éste es un error poco usual y estamos trabajando para resolverlo. 
                                El IdStock: {0} reporta una cantidad: {1} aunque ésto puede no afectar
                                se considera una línea no válida para el sistema, 
                                de forma preventiva se ha restringido el picking de este documento,
                                lo sentimos, EJC.", BeStockRes.IdStock, BeStockRes.Cantidad)

                                Throw New Exception(vMensaje)

                            Else

                                .IdPedidoEnc = BeStockRes.IdPedido
                                .IdPedidoDet = BeStockRes.IdPedidoDet
                                .IdStockRes = BeStockRes.IdStockRes
                                .IdPickingDet = BePickingDet.IdPickingDet
                                .IdStock = BeStockRes.IdStock
                                .IdPropietarioBodega = BeStockRes.IdPropietarioBodega
                                .IdProductoBodega = BeStockRes.IdProductoBodega
                                .IdProductoEstado = BeStockRes.IdProductoEstado
                                .IdPresentacion = BeStockRes.IdPresentacion
                                .IdUnidadMedida = BeStockRes.IdUnidadMedida
                                .IdUbicacionAnterior = Val(BeStockRes.Ubicacion_ant)
                                .IdRecepcion = BeStockRes.IdRecepcion
                                .IdUbicacion = BeStockRes.IdUbicacion
                                .Lote = BeStockRes.Lote
                                .Fecha_Vence = BeStockRes.Fecha_vence
                                .Serial = BeStockRes.Serial
                                .Lic_plate = BeStockRes.Lic_plate
                                .Peso_solicitado = BeStockRes.Peso

                                ''#EJC20180926: Insertar cantidad de presentación en picking_ubic 
                                If BeStockRes.IdPresentacion = 0 Then
                                    .Cantidad_Solicitada = BeStockRes.Cantidad
                                Else
                                    BePresentacion = clsLnProducto_presentacion.GetSingle(BeStockRes.IdPresentacion)
                                    If Not BePresentacion Is Nothing Then
                                        If BePresentacion.Factor = 0 Then BePresentacion.Factor = 1
                                        '#EJC20191122: Quité redondeo para obtener exactitud en la conversión.
                                        '.Cantidad_Solicitada = Math.Round(bo.Cantidad / BePresentacion.Factor, 6)
                                        .Cantidad_Solicitada = BeStockRes.Cantidad / BePresentacion.Factor
                                    Else
                                        Throw New Exception("No se obtuvo el detalle de la presentación para el código de presentación: " & BeStockRes.IdPresentacion)
                                    End If
                                End If

                                .Cantidad_Recibida = 0.0
                                .Fecha_real_vence = BeStockRes.Fecha_vence
                                .User_agr = BeTransPeDet.User_agr
                                .Fec_agr = Now
                                .User_mod = BeTransPeDet.User_agr
                                .Fec_mod = Now
                                .Activo = True
                                .IsNew = True

                            End If

                        End With

                        pListBePickingUbic.Add(BeTransPickingUbic)

                    Next

                    pListBePickingDet.Add(BePickingDet)

                End If

            Next

            ' Picking Detalle
            clsLnTrans_picking_det.Guarda_Trans_picking_det(pBePedidoEnc.IdPickingEnc,
                                                            IdBodegaMuelle,
                                                            pListBePickingDet,
                                                            pListBePickingUbic,
                                                            lConnection,
                                                            lTransaction)

            ' Picking Detalle Ubicacion
            clsLnTrans_picking_ubic.Guarda_Trans_picking_ubic(pBePedidoEnc.IdPickingEnc,
                                                              pListBePickingUbic,
                                                              lConnection,
                                                              lTransaction)

            lTransaction.Commit()

            Actualizar_Picking = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Procesar(ByVal pBeTransPickingEnc As clsBeTrans_picking_enc,
                                    ByVal pBeTareaHH As clsBeTarea_hh,
                                    ByVal pListBePickingDet As List(Of clsBeTrans_picking_det),
                                    ByVal pListBePickingDetParametros As List(Of clsBeTrans_picking_det_parametros),
                                    ByVal pListBePickingOpe As List(Of clsBeTrans_picking_op),
                                    ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        '#EJC20171022_1801: Refactorización en función Procesar de clase clsLnTrans_picking_enc

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Picking Encabezado
            Guarda_Trans_picking_enc(pBeTransPickingEnc,
                                     lConnection,
                                     lTransaction)

            ' Picking Detalle
            clsLnTrans_picking_det.Guarda_Trans_picking_det(pBeTransPickingEnc.IdPickingEnc,
                                                            pBeTransPickingEnc.IdBodegaMuelle,
                                                            pListBePickingDet,
                                                            pListBePickingUbic,
                                                            lConnection,
                                                            lTransaction)

            'Tarea de picking.
            clsLnTarea_hh.Guardar_Tarea_Picking_HH(pBeTareaHH,
                                                   lConnection,
                                                   lTransaction)

            ' Picking Detalle Parametros
            clsLnTrans_picking_det_parametros.Guarda_Trans_picking_parametros(pListBePickingDetParametros,
                                                                              lConnection,
                                                                              lTransaction)

            ' Picking Detalle Operador
            clsLnTrans_picking_op.Guarda_Trans_picking_operador(pBeTransPickingEnc.IdPickingEnc,
                                                                pListBePickingOpe,
                                                                lConnection,
                                                                lTransaction)

            ' Picking Detalle Ubicacion
            clsLnTrans_picking_ubic.Guarda_Trans_picking_ubic(pBeTransPickingEnc.IdPickingEnc,
                                                              pListBePickingUbic,
                                                              lConnection,
                                                              lTransaction)

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Guarda_Trans_picking_enc(ByVal pBeTransPickingEnc As clsBeTrans_picking_enc,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)
        Try

            If pBeTransPickingEnc.IsNew Then
                pBeTransPickingEnc.IdPickingEnc = MaxID(lConnection, lTransaction)
                Insertar(pBeTransPickingEnc, lConnection, lTransaction)
            Else
                Actualizar(pBeTransPickingEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoEnc:=pBeTransPickingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=pBeTransPickingEnc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    '#EJC20171022_0849PM: Anular picking, liberar producto asociado de los pedidos, repartir stock pickeado. Rev I.
    Public Shared Function Anular_Picking(ByVal pBePickingEnc As clsBeTrans_picking_enc,
                                          ByVal pListBeTransPickingDet As List(Of clsBeTrans_picking_det),
                                          ByVal pListBeTransPickingOp As List(Of clsBeTrans_picking_op),
                                          ByVal pIdEmpresa As Integer,
                                          ByVal pLiberarStock As Boolean) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lBeStockPickeado As New List(Of clsBeStock)
        Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Picking Detalle Operador
            For Each BeOperador As clsBeTrans_picking_op In pListBeTransPickingOp
                clsLnTrans_picking_op.Eliminar(BeOperador, lConnection, lTransaction)
            Next

            Dim vListPickingDetParam As New List(Of clsBeTrans_picking_det_parametros)

            'Picking Detalle y Parámetros por detalle.
            For Each BePickingDet As clsBeTrans_picking_det In pListBeTransPickingDet

                vListPickingDetParam = BePickingDet.ListaDetalleParametro.FindAll(Function(b) b.IdPickingDet = BePickingDet.IdPickingDet)

                If Not vListPickingDetParam Is Nothing Then

                    For Each BeTransPickingDetParametro In vListPickingDetParam
                        clsLnTrans_picking_det_parametros.Eliminar(BeTransPickingDetParametro, lConnection, lTransaction)
                    Next

                End If

                ListaDetalleUbicacion = pBePickingEnc.ListaPickingUbic

                If BePickingDet.Cantidad_recibida > 0 Then

                    If Not ListaDetalleUbicacion Is Nothing Then

                        Dim BeStockPickeado As clsBeStock = Nothing

                        For Each BeTransPickingUbic In ListaDetalleUbicacion.Where(Function(x) x.IdPickingDet = BePickingDet.IdPickingDet)

                            If BeTransPickingUbic.Cantidad_Recibida > 0 Then

                                Console.WriteLine(BeTransPickingUbic.IdStock)

                                BeStockPickeado = New clsBeStock
                                BeStockPickeado.IdStock = BeTransPickingUbic.IdStock
                                BeStockPickeado = clsLnStock.GetSingle(BeStockPickeado.IdStock,
                                                                       lConnection,
                                                                       lTransaction)
                                BeStockPickeado.Cantidad = BeTransPickingUbic.Cantidad_Recibida
                                BeStockPickeado.Peso = BeTransPickingUbic.Peso_recibido
                                BeStockPickeado.IdPedidoEnc = 0
                                BeStockPickeado.IdPickingEnc = 0
                                BeStockPickeado.IdUbicacion_anterior = BeStockPickeado.IdUbicacion
                                BeStockPickeado.IdUbicacion = pBePickingEnc.IdUbicacionPicking
                                lBeStockPickeado.Add(BeStockPickeado)

                                clsLnTrans_picking_ubic.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                 lConnection,
                                                                                 lTransaction)

                                clsLnTrans_picking_det.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                lConnection,
                                                                                lTransaction)

                                If pLiberarStock Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(BePickingDet.IdPedidoDet,
                                                                                           lConnection,
                                                                                           lTransaction)
                                End If


                            Else

                                clsLnTrans_picking_ubic.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                 lConnection,
                                                                                 lTransaction)

                                clsLnTrans_picking_det.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                                lConnection,
                                                                                lTransaction)

                                If pLiberarStock Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(BePickingDet.IdPedidoDet,
                                                                                           lConnection,
                                                                                           lTransaction)
                                End If

                            End If

                        Next

                    End If

                Else

                    clsLnTrans_picking_ubic.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                     lConnection,
                                                                     lTransaction)

                    clsLnTrans_picking_det.Eliminar_By_IdPickingDet(BePickingDet.IdPickingDet,
                                                                    lConnection,
                                                                    lTransaction)

                    If pLiberarStock Then
                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(BePickingDet.IdPedidoDet,
                                                                               lConnection,
                                                                               lTransaction)
                    End If

                End If

            Next

            clsLnStock.Actualizar_Stock_Por_Productos_Pickeados(lBeStockPickeado,
                                                                lConnection,
                                                                lTransaction)

            ''#EJC20171025_0238PM: Obtener listado de pedidos asociados al picking en proceso de anulación
            Dim listaPedidoPorPicking = From P In pBePickingEnc.ListaPickingDet Select New With {Key P.IdPedidoEnc} Distinct.ToList()

            '#EJC20220708:Agregué el usuario que anuló en user_mod de pedido_enc
            For Each Ped In listaPedidoPorPicking

                clsLnTrans_pe_enc.Liberar_De_Picking(Ped.IdPedidoEnc,
                                                     pBePickingEnc.IdPickingEnc,
                                                     pBePickingEnc.User_mod,
                                                     lConnection,
                                                     lTransaction)

                '#EJC20220708:Agregar log de error de anulación.
                'clsLnLog_error_wms.Agregar_Error(pIdEmpresa,
                '                                 pBePickingEnc.IdBodega,
                '                                 "Picking_Anulado",
                '                                 Ped.IdPedidoEnc,
                '                                 pBePickingEnc.IdPickingEnc,
                '                                 0,
                '                                 pBePickingEnc.User_mod)

                clsLnLog_error_wms_pick.Agregar_Error($"Picking anulado {pBePickingEnc.IdPickingEnc}",
                                                      pIdPedidoEnc:=pBePickingEnc.IdPedidoEnc,
                                                      pIdPickingEnc:=pBePickingEnc.IdPickingEnc,
                                                      pIdEmpresa:=pIdEmpresa,
                                                      pIdBodega:=pBePickingEnc.IdBodega,
                                                      pUserAgr:=pBePickingEnc.User_mod,
                                                      pTransaction:=lTransaction,
                                                      pConection:=lConnection)

            Next

            '#EJC20220708:Agregué el usuario que anuló en user_mod de picking_enc
            ' Picking Encabezado
            Anular_Encabezado(pBePickingEnc.IdPickingEnc,
                              pBePickingEnc.User_mod,
                              lConnection,
                              lTransaction)




            'Cambiar estado de la tarea de la HH a anulada
            clsLnTarea_hh.Actualiza_Estado_Tarea(pBePickingEnc.IdPickingEnc,
                                                 clsDataContractDI.tTipoTarea.PIK,
                                                 3,
                                                 lConnection,
                                                 lTransaction) 'El IdEstado 3 es Anulado

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_PickingEnc_Procesado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc,
                                                           Optional ByRef pConection As SqlConnection = Nothing,
                                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim beTareaHH As New clsBeTarea_hh
        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            If Es_Transaccion_Remota Then

                beTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(oBeTrans_picking_enc.IdPickingEnc, 8, pConection, pTransaction)
                clsLnTarea_hh.GetSingle(beTareaHH, pConection, pTransaction)

                beTareaHH.IdEstado = 4
                beTareaHH.FechaFin = Now '#CKFK 20180726 10:03 Se actualizó el campo fecha fin 

                clsLnTarea_hh.Actualizar(beTareaHH, pConection, pTransaction)

                '#CKFK 20180430 10:10 AM Crear tarea de verificación
                beTareaHH.Asunto = "Picking de Productos"
                beTareaHH.IdTipoTarea = 11
                beTareaHH.IdEstado = 1
                clsLnTarea_hh.Guardar_Tarea_Verificacion_HH(beTareaHH,
                                                            pConection,
                                                            pTransaction)

                pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc,
                                                                                     pConection,
                                                                                     pTransaction)

                For Each enc In pListBeTransPeEnc

                    '#CKFK 20200430 Se modificó porque el estado debe ser Verificado cuando es automática la Verificacion 
                    'y de lo contrario debe quedar el pedido en estado Pendiente
                    'enc.Estado = "Procesado"

                    If (oBeTrans_picking_enc.verifica_auto) Then
                        enc.Estado = "Verificado"
                        clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)
                    End If

                Next

                '#CKFK 20211206 Le vamos a colocar al picking enc la fecha del BOF por si la HH tiene la fecha incorrecta
                oBeTrans_picking_enc.Fec_mod = Now
                oBeTrans_picking_enc.Hora_fin = Now

                Actualizar(oBeTrans_picking_enc, pConection, pTransaction)

            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                beTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(oBeTrans_picking_enc.IdPickingEnc,
                                                               clsDataContractDI.tTipoTarea.PIK,
                                                               lConnection,
                                                               lTransaction)

                clsLnTarea_hh.GetSingle(beTareaHH, lConnection, lTransaction)

                beTareaHH.IdEstado = 4

                clsLnTarea_hh.Actualizar(beTareaHH, lConnection, lTransaction)

                pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc,
                                                                                     lConnection,
                                                                                     lTransaction)

                For Each enc In pListBeTransPeEnc

                    '#CKFK 20200430 Se modificó porque el estado debe ser Verificado cuando es automática la Verificacion
                    'y de lo contrario debe quedar el pedido en estado Pendiente
                    'enc.Estado = "Procesado"

                    If (oBeTrans_picking_enc.verifica_auto) Then
                        enc.Estado = "Verificado"
                        clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)
                    End If

                Next

                oBeTrans_picking_enc.Fec_mod = Now
                oBeTrans_picking_enc.Hora_fin = Now

                Actualizar(oBeTrans_picking_enc, lConnection, lTransaction)

                lTransaction.Commit()

            End If

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_PickingEnc_Verificado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc,
                                                           Optional ByRef pConection As SqlConnection = Nothing,
                                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc,
                                                                                 IIf(Es_Transaccion_Remota, pConection, lConnection),
                                                                                 IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            For Each enc In pListBeTransPeEnc

                clsLnTrans_pe_enc.Actualizar_Estado_Verificado(enc,
                                                    IIf(Es_Transaccion_Remota, pConection, lConnection),
                                                    IIf(Es_Transaccion_Remota, pTransaction, lTransaction))
            Next

            '#CKFK 20211206 Le vamos a colocar al picking enc la fecha del BOF por si la HH tiene la fecha incorrecta
            oBeTrans_picking_enc.Fec_mod = Now
            oBeTrans_picking_enc.Hora_fin = Now

            Actualizar_Estado_Verificado(oBeTrans_picking_enc,
                              IIf(Es_Transaccion_Remota, pConection, lConnection),
                              IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_Estado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_picking_enc.Estado))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTrans_picking_enc.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTrans_picking_enc.User_mod))

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

    Public Shared Function Actualizar_Estado_Verificado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Verificado"))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", Now))

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

    Public Shared Function ActualizarEstadoByRemplazo(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", oBeTrans_picking_enc.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeTrans_picking_enc.Estado))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoEnc:=oBeTrans_picking_enc.IdPedidoEnc,
                                                  pIdPickingEnc:=oBeTrans_picking_enc.IdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Set_PickingEnc_Procesado_Verificado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc,
                                                               Optional ByRef pConection As SqlConnection = Nothing,
                                                               Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim BeTareaHH As New clsBeTarea_hh
        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            If Es_Transaccion_Remota Then

                BeTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(oBeTrans_picking_enc.IdPickingEnc, 8, pConection, pTransaction)
                clsLnTarea_hh.GetSingle(BeTareaHH, pConection, pTransaction)

                BeTareaHH.IdEstado = 4

                clsLnTarea_hh.Actualizar(BeTareaHH, pConection, pTransaction)

                pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc, pConection, pTransaction)

                For Each enc In pListBeTransPeEnc
                    If oBeTrans_picking_enc.verifica_auto Then
                        enc.Estado = "Verificado"
                    Else
                        enc.Estado = "Pendiente"
                    End If
                    clsLnTrans_pe_enc.Actualizar_Estado(enc, pConection, pTransaction)
                Next

                Actualizar(oBeTrans_picking_enc, pConection, pTransaction)

            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                BeTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(oBeTrans_picking_enc.IdPickingEnc, 8, lConnection, lTransaction)
                clsLnTarea_hh.GetSingle(BeTareaHH, lConnection, pTransaction)

                BeTareaHH.IdEstado = 4

                clsLnTarea_hh.Actualizar(BeTareaHH, lConnection, lTransaction)

                pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc, lConnection, lTransaction)

                For Each enc In pListBeTransPeEnc
                    If oBeTrans_picking_enc.verifica_auto Then
                        enc.Estado = "Verificado"
                    Else
                        enc.Estado = "Procesado"
                    End If
                    clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)
                Next

                Actualizar(oBeTrans_picking_enc, lConnection, lTransaction)

                lTransaction.Commit()

            End If

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Set_Pedidos_Verificados(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc,
                                                   ByVal Usuario As Integer,
                                                   Optional ByRef pConection As SqlConnection = Nothing,
                                                   Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim BeTareaHH As New clsBeTarea_hh
        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)
        Dim lConnection As SqlConnection = If(pConection, New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST")))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim conn As SqlConnection = If(Es_Transaccion_Remota, pConection, lConnection)
            Dim trans As SqlTransaction = If(Es_Transaccion_Remota, pTransaction, lTransaction)

            BeTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(oBeTrans_picking_enc.IdPickingEnc, clsDataContractDI.tTipoTarea.PIK, conn, trans)
            clsLnTarea_hh.GetSingle(BeTareaHH, conn, trans)

            BeTareaHH.IdEstado = 4
            clsLnTarea_hh.Actualizar(BeTareaHH, conn, trans)

            pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(oBeTrans_picking_enc.IdPickingEnc, conn, trans)

            For Each enc In pListBeTransPeEnc

                enc.Estado = "Verificado"
                clsLnTrans_pe_enc.Actualizar_Estado(enc, conn, trans)

                '#MECR23102025: Se agrego bitacora para logs de picking
                Dim BeUsuario = clsLnUsuario.GetSingle(Usuario, conn, trans)
                'clsLnLog_error_wms.Agregar_Error("El usuario" & Usuario & " - " & BeUsuario.Nombres & " verifico el pedido " & enc.IdPedidoEnc)
                clsLnLog_error_wms_pick.Agregar_Error("El usuario" & Usuario & " - " & BeUsuario.Nombres & " verifico el pedido " & enc.IdPedidoEnc,
                                                      pIdPedidoEnc:=oBeTrans_picking_enc.IdPedidoEnc,
                                                      pIdPickingEnc:=oBeTrans_picking_enc.IdPickingEnc,
                                                      pIdBodega:=oBeTrans_picking_enc.IdBodega,
                                                      pUserAgr:=Usuario,
                                                      pConection:=lConnection,
                                                      pTransaction:=lTransaction)

            Next

            Actualizar(oBeTrans_picking_enc, conn, trans)

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return True

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not Es_Transaccion_Remota AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not Es_Transaccion_Remota Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Tiene_Despachos_Pendientes(ByVal pIdPickingEnc As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Boolean

        Tiene_Despachos_Pendientes = True

        Try

            Dim lCount As Integer = 0

            Dim vSQL As String = "SELECT Count(pu.IdPickingUbic) As PickingPendiente 
                        FROM  trans_picking_ubic AS pu INNER JOIN
                        trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet INNER JOIN
                        trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
                        stock_res AS sr ON pdet.IdPedidoDet = sr.IdPedidoDet
                        WHERE (pkdet.IdPickingEnc = @IdPickingEnc)
                        AND  (pu.cantidad_solicitada - pu.cantidad_despachada) <> 0 "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCount = CInt(lReturnValue)
                    Tiene_Despachos_Pendientes = (lCount <> 0)
                End If

            End Using

            Return lCount

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc(ByVal pIdPickingEnc As Integer) As Boolean

        Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc = False

        Try

            Dim lTienePedidos As Boolean = False

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT DISTINCT pdet.IdPedidoEnc
                    FROM trans_picking_ubic pu  
                    INNER JOIN trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet 
                    INNER JOIN trans_pe_det As pdet On pdet.IdPedidoDet = pkdet.IdPedidoDet  
                    INNER JOIN trans_pe_enc As penc On pdet.IdPedidoEnc = penc.IdPedidoEnc  
                    WHERE pkdet.IdPickingEnc = @IdPickingEnc And penc.estado NOT IN ('Verificado','Despachado')"

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If Not dt Is Nothing Then
                        lTienePedidos = dt.Rows.Count > 0
                    End If

                    Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc = lTienePedidos

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Dañados_Verificacion_Picking_ByPickingEnc(ByVal IdPickingEnc As Integer) As DataTable

        Get_Dañados_Verificacion_Picking_ByPickingEnc = Nothing

        Try

            '#CKFK 20200514 Correccion de este query, le hacían falta los parentesis en el or del Where
            'GT01022022: se agrega lic_plateyo 
            Dim vSQL As String = "SELECT ped.IdPedidoEnc, ped.IdPedidoDet,pubic.IdPickingUbic,pubic.IdPickingDet,p.Codigo as Código,ped.nombre_producto as Producto,
                                  ped.nom_unid_med as UMBas, pp.Nombre as Presentación,ped.nom_estado as EstadoProducto,pubic.lote as Lote, pubic.fecha_vence as Vence,
                                  pubic.cantidad_solicitada AS CantidadDañada,pubic.dañado_picking as DañadoPicking,pubic.dañado_verificacion as DañadoVerificación, 
                                  pubic.no_encontrado as NoEncontrado,pubic.lic_plate
                                  FROM trans_pe_det ped inner join trans_picking_det pkdet ON  ped.IdPedidoDet = pkdet.IdPedidoDet
                                  inner JOIN trans_picking_ubic pubic ON pkdet.IdPickingDet = pubic.IdPickingDet
                                  INNER JOIN producto_bodega pb ON pb.IdProductoBodega = ped.IdProductoBodega
                                  INNER JOIN producto P ON p.IdProducto = pb.IdProducto
                                  LEFT OUTER JOIN producto_presentacion pp ON p.IdProducto = pp.IdProducto AND pp.Idpresentacion = pubic.idPresentacion 
                                  WHERE pkdet.IdPickingEnc = @IdPickingEnc  and (pubic.dañado_verificacion = 1 or pubic.dañado_picking = 1 or no_encontrado = 1)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lTable As New DataTable("Result")

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)
                        lDataAdapter.Fill(lTable)
                        Get_Dañados_Verificacion_Picking_ByPickingEnc = lTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Set_Estado_Finalizado_Packing(ByVal IdPickingEnc As Integer,
                                                         ByVal IdPedidoEnc As Integer,
                                                         Optional ByRef pConnection As SqlConnection = Nothing,
                                                         Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Set_Estado_Finalizado_Packing = 0

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Dim sp As String
            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand
            Dim rowsAffected As Integer = 0
            Dim vCantPed As Integer = 0
            Dim vEstadoPedido As String = ""

            If Es_Transaccion_Remota Then
                vEstadoPedido = clsLnTrans_pe_enc.Get_Estado_Pedido_By_IdPedidoEnc(IdPedidoEnc, pConnection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                vEstadoPedido = clsLnTrans_pe_enc.Get_Estado_Pedido_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)
            End If

            If vEstadoPedido = "Verificado" Then

                Upd.Init("trans_packing_enc")
                Upd.Add("finalizado", "@finalizado", DataType.Parametro)
                Upd.Where("IdPickingEnc = @IdPickingEnc AND IdPedidoEnc = @IdPedidoEnc ")
                sp = Upd.SQL()

                If Es_Transaccion_Remota Then
                    cmd = New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                    vCantPed = clsLnTrans_packing_enc.Get_Pedidos_Sin_Procesar_By_IdPicking(IdPickingEnc, IdPedidoEnc, pConnection, pTransaction)

                Else
                    cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                    vCantPed = clsLnTrans_packing_enc.Get_Pedidos_Sin_Procesar_By_IdPicking(IdPickingEnc, IdPedidoEnc, lConnection, lTransaction)

                End If

                cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
                cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
                cmd.Parameters.Add(New SqlParameter("@finalizado", True))

                rowsAffected += cmd.ExecuteNonQuery()

                cmd.Dispose()

                If vCantPed = 0 Then

                    Upd.Init("trans_picking_enc")
                    Upd.Add("estado_preparacion", "@estado", DataType.Parametro)
                    Upd.Add("fecha_fin_preparacion", "@fecha_fin_preparacion", DataType.Parametro)
                    Upd.Where("IdPickingEnc = @IdPickingEnc AND IdPedidoEnc = @IdPedidoEnc ")

                    sp = Upd.SQL()

                    If Es_Transaccion_Remota Then
                        cmd = New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}
                    Else
                        cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    End If

                    cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
                    cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))
                    cmd.Parameters.Add(New SqlParameter("@ESTADO", "Finalizado"))
                    cmd.Parameters.Add(New SqlParameter("@FECHA_FIN_PREPARACION", Now))

                    rowsAffected += cmd.ExecuteNonQuery()

                    cmd.Dispose()

                End If

                If Not Es_Transaccion_Remota Then lTransaction.Commit()

                Return rowsAffected

            Else

                Throw New Exception("El pedido no tiene estado Verificado no se puede finalizar el packing")

            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Set_Estado_Pendiente_Packing(ByVal IdPickingEnc As Integer,
                                                        Optional ByRef pConection As SqlConnection = Nothing,
                                                        Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vFechaInicioDefecto As Date = New Date(1900, 1, 1)
        Dim vFechaInicioActual As Date = Now

        Try

            vFechaInicioActual = Get_Fecha_Inicio_Packing(IdPickingEnc, lConnection, lTransaction)

            Upd.Init("trans_picking_enc")
            Upd.Add("estado_preparacion", "@estado", DataType.Parametro)
            If vFechaInicioActual = vFechaInicioDefecto Then
                Upd.Add("fecha_inicio_preparacion", "@fecha_inicio_preparacion", DataType.Parametro)
            End If
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Pendiente"))

            If vFechaInicioActual = vFechaInicioDefecto Then
                cmd.Parameters.Add(New SqlParameter("@FECHA_INICIO_PREPARACION", Now))
            End If

            Dim rowsAffected As Integer = 0

            rowsAffected = cmd.ExecuteNonQuery()

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

    Public Shared Function Get_Fecha_Inicio_Packing(ByVal pIdPickingEnc As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As Date

        Get_Fecha_Inicio_Packing = New Date(1900, 1, 1)

        Try

            Dim vSQL As String = "SELECT fecha_inicio_preparacion FROM trans_picking_enc WHERE IdPickingEnc=@IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("fecha_inicio_preparacion") IsNot DBNull.Value AndAlso lRow("fecha_inicio_preparacion") IsNot Nothing Then

                        Get_Fecha_Inicio_Packing = IIf(IsDBNull(lRow("fecha_inicio_preparacion")), New Date(1900, 1, 1), lRow("fecha_inicio_preparacion"))

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Picking_Asociado(ByRef IdPickingEnc As Integer,
                                        Optional ByRef pConnection As SqlConnection = Nothing,
                                        Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing
        Dim Resultado As String = ""

        Try

            Dim lCommand As New SqlCommand
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim Idpic As String = (" select isnull(sum(cantidad_recibida),0) cantidad_recibida 
                                            from trans_picking_enc pickin_enc inner join trans_picking_det pickin_det
                                            on pickin_enc.IdPickingEnc=pickin_det.IdPickingEnc
                                            where pickin_enc.estado<>'Anulado' ")

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : ltrans = lConnection.BeginTransaction
                lCommand = New SqlCommand(Idpic, lConnection, ltrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPickingEnc))

            Resultado = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return Resultado <> ""

        Catch ex As Exception
            If ltrans IsNot Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPickingEnc_For_HH(ByVal pIdPickingEnc As Integer,
                                                             ByVal pIdOperadorBodega As Integer) As clsBeTrans_picking_enc

        Get_Single_By_IdPickingEnc_For_HH = Nothing

        Try

            Dim sSQL As String = ""

            sSQL = "SELECT b.descripcion AS UbicacionPicking, enc.* 
                    FROM trans_picking_enc AS enc 
                    INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                    AND enc.IdBodega = b.IdBodega
                    WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                            Cargar(BeTransPickingEnc, lRow)

                            If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then
                                BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                                BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)
                            End If

                            BeTransPickingEnc.IsNew = False

                            BeTransPickingEnc.ListaPickingDet = clsLnTrans_picking_det.Get_All_Picking_Det_Pendientes_For_HH_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                            '#EJC20171021_0458PM: Se agregó funcionalidad para llenar el detalle de picking_ubic por IdPickingEnc
                            '#GT22122022_0200PM: Se agrega la bodega del picking para reordenamiento en la vista
                            BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_Pendientes_HH_By_IdPickingEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                                           pIdOperadorBodega,
                                                                                                                                           BeTransPickingEnc.IdBodega,
                                                                                                                                           lConnection,
                                                                                                                                           lTransaction)


                            Get_Single_By_IdPickingEnc_For_HH = BeTransPickingEnc

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

    Public Shared Function Actualizar_Estado_Andr(ByVal pEstadoPicking As String,
                                                  ByVal pIdPickingEnc As Integer,
                                                  Optional ByRef pConection As SqlConnection = Nothing,
                                                  Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", pIdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", pEstadoPicking))

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

    ''' <summary>
    ''' #EJC20220614: Simplificación de método para Android.
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_PickingEnc_Procesado_Andr(ByVal pIdPickingEnc As Integer,
                                                                ByVal pIdOperadorBodegaCerro As Integer,
                                                                ByVal pHostCerro As String,
                                                                Optional ByRef pConnection As SqlConnection = Nothing,
                                                                Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim beTareaHH As New clsBeTarea_hh
        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim verifica_auto As Boolean = False
        Dim pEstado As String = ""
        Dim pEstadoPicking As String = ""


        Try

            '#CKFK20240229 Agregué esta validación para que no se actualicen los pedidos con picking 0
            If pIdPickingEnc <> 0 Then

                If Es_Transaccion_Remota Then

                    verifica_auto = Get_VerificacionAuto_By_IdPickingEnc(pIdPickingEnc,
                                                                         pConnection,
                                                                         pTransaction)

                    beTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(pIdPickingEnc, 8, pConnection, pTransaction)
                    clsLnTarea_hh.GetSingle(beTareaHH, pConnection, pTransaction)

                    beTareaHH.IdEstado = 4
                    beTareaHH.FechaFin = Now '#CKFK 20180726 10:03 Se actualizó el campo fecha fin 

                    clsLnTarea_hh.Actualizar(beTareaHH, pConnection, pTransaction)

                    '#CKFK 20180430 10:10 AM Crear tarea de verificación
                    beTareaHH.Asunto = "Picking de Productos"
                    beTareaHH.IdTipoTarea = 11
                    beTareaHH.IdEstado = 1
                    clsLnTarea_hh.Guardar_Tarea_Verificacion_HH(beTareaHH,
                                                                pConnection,
                                                                pTransaction)

                    pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(pIdPickingEnc,
                                                                                         pConnection,
                                                                                         pTransaction)


                    '#GT21032025: infiero que, si verificacion es auto, aplica para cada iteracion pListBeTransPeEnc
                    If (verifica_auto) Then
                        pEstado = "Verificado"
                    Else
                        pEstado = "Pickeado"
                    End If

                    For Each enc In pListBeTransPeEnc

                        '#CKFK 20200430 Se modificó porque el estado debe ser Verificado cuando es automática la Verificacion 
                        'y de lo contrario debe quedar el pedido en estado Pendiente
                        'enc.Estado = "Procesado"

                        'If (verifica_auto) Then
                        '    enc.Estado = "Verificado"
                        '    clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)
                        'Else
                        '    '#GT10012023: Estado Pickeado para el pedido sino tiene verifica_auto
                        '    enc.Estado = "Pickeado"
                        '    clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)
                        'End If

                        enc.Estado = pEstado

                        clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)

                    Next

                    '#GT21032025: validamos el estado no solo en el pedido, tambien el picking
                    If (verifica_auto) Then
                        pEstadoPicking = "Verificado"
                    Else
                        pEstadoPicking = "Procesado"
                    End If
                    Actualizar_FecMod_And_HoraFin(pIdPickingEnc, pEstadoPicking, lConnection, lTransaction)

                Else

                    lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    verifica_auto = Get_VerificacionAuto_By_IdPickingEnc(pIdPickingEnc, lConnection, lTransaction)

                    beTareaHH.IdTareahh = clsLnTarea_hh.GetIdTarea(pIdPickingEnc,
                                                                   8,
                                                                   lConnection,
                                                                   lTransaction)

                    clsLnTarea_hh.GetSingle(beTareaHH, lConnection, lTransaction)

                    beTareaHH.IdEstado = 4
                    beTareaHH.IdOperadorBodega_Cerro = pIdOperadorBodegaCerro
                    beTareaHH.Host_Cerro = pHostCerro

                    '#AT20250103 Agregar log cuando de cierre un picking
                    If clsLnTarea_hh.Actualizar(beTareaHH, lConnection, lTransaction) > 0 Then
                        Dim BeOperador As New clsBeOperador
                        BeOperador = clsLnOperador.Get_Single_By_IdOperadorBodega(pIdOperadorBodegaCerro, lConnection, lTransaction)

                        If Not BeOperador Is Nothing Then
                            '#MECR23102025: Se agrego bitacora para logs de picking
                            Dim vNombre As String = BeOperador.Nombres & " " & BeOperador.Apellidos
                            'clsLnLog_error_wms.Agregar_Error(BeOperador.IdEmpresa, beTareaHH.IdBodega, "El IdOperadorBodega: " & pIdOperadorBodegaCerro & " " & vNombre & " cerró el picking " & pIdPickingEnc)
                            clsLnLog_error_wms_pick.Agregar_Error("El IdOperadorBodega: " & pIdOperadorBodegaCerro & " " & vNombre & " cerró el picking " & pIdPickingEnc,
                                                                  pIdPedidoEnc:=pIdPickingEnc,
                                                                  pConection:=lConnection,
                                                                  pTransaction:=lTransaction)
                        End If

                    End If

                    pListBeTransPeEnc = clsLnTrans_pe_enc.Get_All_Pedido_By_IdPickingEnc(pIdPickingEnc,
                                                                                         lConnection,
                                                                                         lTransaction)


                    '#GT21032025: infiero que, si verificacion es auto, aplica para cada iteracion pListBeTransPeEnc
                    If (verifica_auto) Then
                        pEstado = "Verificado"
                    Else
                        pEstado = "Pickeado"
                    End If

                    For Each enc In pListBeTransPeEnc

                        enc.Estado = pEstado
                        clsLnTrans_pe_enc.Actualizar_Estado(enc, lConnection, lTransaction)

                    Next

                    '#GT21032025: validamos el estado no solo para el pedido, tambien picking
                    If (verifica_auto) Then
                        pEstadoPicking = "Verificado"
                    Else
                        pEstadoPicking = "Procesado"
                    End If

                    Actualizar_FecMod_And_HoraFin(pIdPickingEnc, pEstadoPicking, lConnection, lTransaction)

                    lTransaction.Commit()

                End If

                Return True

            Else

                Throw New Exception("El IdPickingEnc es 0, no se puede actualizar el estado de los pedidos")

            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_VerificacionAuto_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As Boolean

        Get_VerificacionAuto_By_IdPickingEnc = False

        Try

            Dim vSQL As String = "SELECT verifica_auto FROM trans_picking_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("verifica_auto") IsNot DBNull.Value AndAlso lRow("verifica_auto") IsNot Nothing Then

                        Get_VerificacionAuto_By_IdPickingEnc = CType(lRow("verifica_auto"), Boolean)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPrioridadPicking(ByVal pIdPickingEnc As Integer,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPrioridadPicking = 0

        Try

            Dim vSQL As String = "SELECT IdPrioridadPicking 
                                  FROM trans_picking_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdPrioridadPicking") IsNot DBNull.Value AndAlso lRow("IdPrioridadPicking") IsNot Nothing Then

                        Get_IdPrioridadPicking = CType(lRow("IdPrioridadPicking"), Integer)

                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPickingEnc_For_HH_Reducido(ByVal pIdPickingEnc As Integer,
                                                                      ByVal pIdOperadorBodega As Integer) As clsBeTrans_picking_enc

        Get_Single_By_IdPickingEnc_For_HH_Reducido = Nothing

        Try

            Dim sSQL As String = ""

            sSQL = "SELECT b.descripcion AS UbicacionPicking,  c.nombre as NombreMuelle, enc.* 
                    FROM trans_picking_enc AS enc 
                    INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                    LEFT JOIN bodega_muelles AS c ON c.IdMuelle = enc.IdBodegaMuelle
                    AND enc.IdBodega = b.IdBodega
                    WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                            Cargar(BeTransPickingEnc, lRow)

                            If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then
                                BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                                BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)
                            End If

                            If lRow("NombreMuelle") IsNot DBNull.Value AndAlso lRow("NombreMuelle") IsNot Nothing Then
                                BeTransPickingEnc.NombreMuelle = CType(lRow("NombreMuelle"), String)
                            End If

                            Get_Single_By_IdPickingEnc_For_HH_Reducido = BeTransPickingEnc

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

    '#CKFK20250225 Hice una copia de esta función para que solo cargue lo del producto y el pedido
    Public Shared Function GetSingleHH_By_IdPicking_IdPedido_CodProd(ByVal pIdPickingEnc As Integer,
                                                                     ByVal pIdPedidoEnc As Integer,
                                                                     ByVal pCodProd As String,
                                                                     ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_enc

        GetSingleHH_By_IdPicking_IdPedido_CodProd = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionPicking, enc.* 
                                  FROM trans_picking_enc AS enc 
                                  INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                                  WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                    Cargar(BeTransPickingEnc, lRow)

                    If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                        BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                        BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)

                    End If

                    BeTransPickingEnc.IsNew = False

                    BeTransPickingEnc.ListaPickingDet = Nothing

                    BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPicking_Pedido_Producto(BeTransPickingEnc.IdPickingEnc,
                                                                                                                                  BeTransPickingEnc.IdBodega,
                                                                                                                                  pIdPedidoEnc,
                                                                                                                                  pCodProd,
                                                                                                                                  lConnection,
                                                                                                                                  lTransaction)

                    Return BeTransPickingEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20220624 Copia de GetSingle para la HH por que no vamos a llenar el detalle de pdet ni el picking ubic del pdet
    Public Shared Function GetSingleHH_By_IdPedidoEnc(ByVal pIdPickingEnc As Integer,
                                                      ByVal pIdPedidoEnc As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_enc

        GetSingleHH_By_IdPedidoEnc = Nothing

        Try

            Dim vSQL As String = "SELECT b.descripcion AS UbicacionPicking, enc.* 
                                  FROM trans_picking_enc AS enc 
                                  INNER JOIN bodega_ubicacion AS b ON enc.IdUbicacionPicking = b.IdUbicacion 
                                  WHERE enc.IdPickingEnc=@IdPickingEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTransPickingEnc As New clsBeTrans_picking_enc()

                    Cargar(BeTransPickingEnc, lRow)

                    If lRow("IdUbicacionPicking") IsNot DBNull.Value AndAlso lRow("IdUbicacionPicking") IsNot Nothing Then

                        BeTransPickingEnc.IdUbicacionPicking = CType(lRow("IdUbicacionPicking"), Integer)
                        BeTransPickingEnc.NombreUbicacionPicking = CType(lRow("UbicacionPicking"), String)

                    End If

                    BeTransPickingEnc.IsNew = False

                    '#CKFK20250303 Cambie esta funcion Get_All_PickingUbic_By_IdPickingEnc por esta Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc
                    BeTransPickingEnc.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc(BeTransPickingEnc.IdPickingEnc,
                                                                                                                                     pIdPedidoEnc,
                                                                                                                                     lConnection,
                                                                                                                                     lTransaction)

                    Return BeTransPickingEnc

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20250313: Productividad picking creado para la cumbre.
    ''' </summary>
    ''' <param name="pFechaDel"></param>
    ''' <param name="pFechaAl"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_Rpt_Productividad_Picking_By_IdBodega(ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdBodega As Integer) As DataTable
        Get_Rpt_Productividad_Picking_By_IdBodega = Nothing

        Try
            Dim vSQL As String = "SELECT * FROM VW_Productividad_Picking WHERE CAST(Fecha_Por_Línea AS DATE) BETWEEN @FechaDel AND @FechaAl AND IdBodega = @IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        Dim lTable As New DataTable("PickingResult")
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaDel", pFechaDel.Date)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaAl", pFechaAl.Date)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.Fill(lTable)
                        Get_Rpt_Productividad_Picking_By_IdBodega = lTable
                    End Using
                    lTransaction.Commit()
                End Using

                lConnection.Close()
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name, ex.Message))
        End Try
    End Function

    Public Shared Function Actualizar_Estado_Pendiente(ByVal IdPickingEnc As Integer, Optional ByRef pConection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_enc")
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Where("IdPickingEnc = @IdPickingEnc")

            Dim sp As String = Upd.SQL()
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", "Pendiente"))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", New Date(1900, 1, 1)))

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

    Public Shared Function Get_Verificacion_Auto_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction) As Integer

        Get_Verificacion_Auto_By_IdPickingEnc = False

        Try

            Dim vSQL As String = "SELECT verifica_auto 
                                  FROM trans_picking_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    If lRow("verifica_auto") IsNot DBNull.Value AndAlso lRow("verifica_auto") IsNot Nothing Then
                        Get_Verificacion_Auto_By_IdPickingEnc = CType(lRow("verifica_auto"), Boolean)
                    End If
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BeBodegaMuelle_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As clsBeBodega_muelles

        Get_BeBodegaMuelle_By_IdPickingEnc = Nothing

        Try

            Dim vSQL As String = "SELECT IdBodegaMuelle 
                                  FROM trans_picking_enc 
                                  WHERE IdPickingEnc=@IdPickingEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDT As New DataTable()

                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    If lRow("IdBodegaMuelle") IsNot DBNull.Value AndAlso lRow("IdBodegaMuelle") IsNot Nothing Then

                        Dim vIdBodegaMuelle As Integer = lRow("IdBodegaMuelle")
                        Dim vBodegaMuelle As clsBeBodega_muelles = clsLnBodega_muelles.Get_Single_By_IdBodegaMuelle(vIdBodegaMuelle,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                        Get_BeBodegaMuelle_By_IdPickingEnc = vBodegaMuelle
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Mover_Producto_A_Muelle_By_ListaPedidos(ByVal lPedidos As List(Of Integer),
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As Boolean
        Dim vMover As Boolean = False

        Mover_Producto_A_Muelle_By_ListaPedidos = False

        Try

            For Each ped In lPedidos

                vMover = clsLnTrans_pe_enc.Mover_Producto_A_Muelle_By_IdPedidoEnc(ped, lConnection, lTransaction)
                If vMover Then
                    Return vMover
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_CantidadPedidos_By_IdPickingEnc(ByVal IdPickingEnc As Integer) As Integer
        Get_CantidadPedidos_By_IdPickingEnc = 0

        Try
            Dim sSQL As String = "SELECT COUNT(DISTINCT IdPedidoEnc) AS pedidos_diferentes
                                 FROM trans_picking_ubic
                                 WHERE IdPickingEnc = @IdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("pedidos_diferentes") IsNot DBNull.Value Then
                                Get_CantidadPedidos_By_IdPickingEnc = lRow("pedidos_diferentes")
                            End If
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

    Public Shared Function Get_Estado_By_IdPickingEnc(ByVal IdPickingEnc As Integer) As String
        Dim estado As String = ""

        Try
            Dim sSQL As String = "SELECT Estado FROM trans_picking_enc WHERE IdPickingEnc = @IdPickingEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sSQL, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)

                            If lRow("Estado") IsNot DBNull.Value Then
                                estado = lRow("Estado").ToString()
                            End If
                        End If
                    End Using

                    lTransaction.Commit()
                End Using

                lConnection.Close()
            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return estado
    End Function

End Class
