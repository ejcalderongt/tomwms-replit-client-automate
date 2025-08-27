Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Partial Public Class clsLnTrans_picking_det

    Public Shared Function Get_All_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                   ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_det)

        Dim lReturnList As New List(Of clsBeTrans_picking_det)

        Try

            Dim vSQL As String = "SELECT b.nombre AS Bodega, c.nombre_comercial AS Cliente, pr.nombre_comercial AS Propietario, penc.fecha_pedido, 
                                pcdet.*,p.IdProducto,pdet.IdPresentacion,pdet.idUnidadMedidaBasica,pdet.IdEstado
                                FROM trans_picking_det pcdet
                                INNER JOIN trans_pe_det AS pdet ON pcdet.IdPedidoDet = pdet.IdPedidoDet
                                INNER JOIN trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc
                                INNER JOIN propietario_bodega AS prb ON penc.IdPropietarioBodega = prb.IdPropietarioBodega
                                INNER JOIN propietarios AS pr ON prb.IdPropietario = pr.IdPropietario
                                INNER JOIN bodega AS b ON penc.IdBodega = b.IdBodega
                                INNER JOIN cliente AS c ON penc.IdCliente = c.IdCliente And prb.IdPropietario = c.IdPropietario
                                INNER JOIN producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega
                                INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                                WHERE pcdet.IdPickingEnc=@IdPickingEnc AND penc.IdBodega = @IdBodega "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeTransPickingDet As clsBeTrans_picking_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeTransPickingDet = New clsBeTrans_picking_det

                                BeTransPickingDet.Bodega = CType(lRow("Bodega"), String)
                                BeTransPickingDet.Cliente = CType(lRow("Cliente"), String)
                                BeTransPickingDet.Propietario = CType(lRow("Propietario"), String)
                                BeTransPickingDet.FechaPedido = CType(lRow("fecha_pedido"), System.DateTime)

                                Cargar(BeTransPickingDet, lRow)

                                BeTransPickingDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)

                                Dim pCampos(5) As clsBeProducto.ProdPropiedades
                                pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                                pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                                pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                                pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                                pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                                pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                                BeTransPickingDet.Producto = clsLnProducto.GetSingle(BeTransPickingDet.Producto.IdProducto, pCampos, lConnection, lTransaction)

                                '#EJC20190214_0113PM: Comentariado por rendimiento, cargar solo los campos necesarios..
                                'clsLnProducto.Obtener(Obj.Producto, lConnection, lTransaction)

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    BeTransPickingDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                    clsLnProducto_presentacion.Obtener(BeTransPickingDet.Presentacion, lConnection, lTransaction)
                                End If

                                If lRow("idUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("idUnidadMedidaBasica") IsNot Nothing Then
                                    BeTransPickingDet.UnidadMedida.IdUnidadMedida = CType(lRow("idUnidadMedidaBasica"), Integer)
                                    clsLnUnidad_medida.Obtener(BeTransPickingDet.UnidadMedida, lConnection, lTransaction)
                                End If

                                If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                                    BeTransPickingDet.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                                    clsLnProducto_estado.Obtener(BeTransPickingDet.ProductoEstado, lConnection, lTransaction)
                                End If
                                If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                                    BeTransPickingDet.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                                End If

                                BeTransPickingDet.IsNew = False

                                clsLnTrans_pe_det.Get_InfoPedido_By_IdPedidoDet(BeTransPickingDet.IdPedidoDet,
                                                                                BeTransPickingDet.IdPedidoEnc,
                                                                                BeTransPickingDet.Referencia,
                                                                                lConnection,
                                                                                lTransaction)

                                BeTransPickingDet.ListaDetalleParametro = clsLnTrans_picking_det_parametros.Get_All_By_IdPickingDet(BeTransPickingDet.IdPickingDet,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)

                                'Obj.ListaDetalleUbicacion = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingDet(Obj.IdPickingDet, lConnection, lTransaction)

                                lReturnList.Add(BeTransPickingDet)

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

    Public Shared Function Get_All_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                   ByVal pIdBodega As Integer,
                                                   ByVal pIdPedidoDet As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det)

        Dim lReturnList As New List(Of clsBeTrans_picking_det)

        Try

            Dim vSQL As String = "SELECT b.nombre AS Bodega, c.nombre_comercial AS Cliente, pr.nombre_comercial AS Propietario, penc.fecha_pedido, 
                                pcdet.*,p.IdProducto,pdet.IdPresentacion,pdet.idUnidadMedidaBasica,pdet.IdEstado
                                FROM trans_picking_det pcdet
                                INNER JOIN trans_pe_det AS pdet ON pcdet.IdPedidoDet = pdet.IdPedidoDet
                                INNER JOIN trans_pe_enc AS penc ON pdet.IdPedidoEnc = penc.IdPedidoEnc
                                INNER JOIN propietario_bodega AS prb ON penc.IdPropietarioBodega = prb.IdPropietarioBodega
                                INNER JOIN propietarios AS pr ON prb.IdPropietario = pr.IdPropietario
                                INNER JOIN bodega AS b ON penc.IdBodega = b.IdBodega
                                INNER JOIN cliente AS c ON penc.IdCliente = c.IdCliente And prb.IdPropietario = c.IdPropietario
                                INNER JOIN producto_bodega AS pb ON pdet.IdProductoBodega = pb.IdProductoBodega
                                INNER JOIN producto AS p ON pb.IdProducto = p.IdProducto 
                                WHERE pcdet.IdPickingEnc=@IdPickingEnc AND penc.IdBodega = @IdBodega AND pcdet.IdPedidoDet =@IdPedidoDet "


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_det

                        Obj.Bodega = CType(lRow("Bodega"), String)
                        Obj.Cliente = CType(lRow("Cliente"), String)
                        Obj.Propietario = CType(lRow("Propietario"), String)
                        Obj.FechaPedido = CType(lRow("fecha_pedido"), System.DateTime)

                        Cargar(Obj, lRow)

                        Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)

                        Dim pCampos(5) As clsBeProducto.ProdPropiedades
                        pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                        pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                        pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                        pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                        pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                        pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                        Obj.Producto = clsLnProducto.GetSingle(Obj.Producto.IdProducto, pCampos, lConnection, lTransaction)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            clsLnProducto_presentacion.Obtener(Obj.Presentacion, lConnection, lTransaction)
                        End If

                        If lRow("idUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("idUnidadMedidaBasica") IsNot Nothing Then
                            Obj.UnidadMedida.IdUnidadMedida = CType(lRow("idUnidadMedidaBasica"), Integer)
                            clsLnUnidad_medida.Obtener(Obj.UnidadMedida, lConnection, lTransaction)
                        End If

                        If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                            Obj.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                            clsLnProducto_estado.Obtener(Obj.ProductoEstado, lConnection, lTransaction)
                        End If
                        If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                            Obj.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                        End If

                        Obj.IsNew = False

                        Obj.IdPedidoEnc = clsLnTrans_pe_det.Get_IdPedidoEnc_By_IdPedidoDet(Obj.IdPedidoDet, lConnection, lTransaction)
                        Obj.Referencia = clsLnTrans_pe_det.Get_Referencias_By_IdPedidoDet(Obj.IdPedidoDet, lConnection, lTransaction)
                        Obj.ListaDetalleParametro = clsLnTrans_picking_det_parametros.Get_All_By_IdPickingDet(Obj.IdPickingDet, lConnection, lTransaction)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Picking_Det_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det)

        Dim lReturnList As New List(Of clsBeTrans_picking_det)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Picking_Det_By_IdPickingEnc WHERE IdPickingEnc=@IdPickingEnc  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransPickingDet As clsBeTrans_picking_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim ListaDetalleParametro = clsLnTrans_picking_det_parametros.Get_All_By_IdPickingEnc(pIdPickingEnc,
                                                                                                          lConnection,
                                                                                                          lTransaction)

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransPickingDet = New clsBeTrans_picking_det

                        BeTransPickingDet.Bodega = CType(lRow("Bodega"), String)
                        BeTransPickingDet.Cliente = CType(lRow("Cliente"), String)
                        BeTransPickingDet.Propietario = CType(lRow("Propietario"), String)
                        BeTransPickingDet.FechaPedido = CType(lRow("fecha_pedido"), System.DateTime)

                        Cargar(BeTransPickingDet, lRow)

                        BeTransPickingDet.Producto.IdProducto = CType(lRow("IdProducto"), Integer)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            BeTransPickingDet.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            BeTransPickingDet.Presentacion.Nombre = IIf(IsDBNull(lRow("Nombre_Presentacion")), "", lRow("Nombre_Presentacion"))
                        End If

                        If lRow("idUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("idUnidadMedidaBasica") IsNot Nothing Then
                            BeTransPickingDet.UnidadMedida.IdUnidadMedida = CType(lRow("idUnidadMedidaBasica"), Integer)
                            BeTransPickingDet.UnidadMedida.Nombre = IIf(IsDBNull(lRow("Nombre_Unidad_Medida")), "", lRow("Nombre_Unidad_Medida"))
                        End If

                        If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                            BeTransPickingDet.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                            BeTransPickingDet.ProductoEstado.Nombre = IIf(IsDBNull(lRow("Nombre_Estado")), "", lRow("Nombre_Estado"))
                        End If

                        If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                            BeTransPickingDet.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                        End If

                        If lRow("Bono") IsNot DBNull.Value AndAlso lRow("Bono") IsNot Nothing Then
                            BeTransPickingDet.Bono = CType(lRow("Bono"), String)
                        End If

                        BeTransPickingDet.IsNew = False
                        BeTransPickingDet.IdPedidoEnc = IIf(IsDBNull(lRow("IdPedidoEnc")), 0, lRow("IdPedidoEnc"))
                        BeTransPickingDet.ListaDetalleParametro = ListaDetalleParametro.FindAll(Function(x) x.IdPickingDet = BeTransPickingDet.IdPickingDet)

                        lReturnList.Add(BeTransPickingDet)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdPedidoDet As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_det

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM Trans_picking_det WHERE IdPedidoDet = @IdPedidoDet"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Obj = New clsBeTrans_picking_det
                    Cargar(Obj, lDataTable.Rows(0))
                    Obj.IsNew = False

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPickingDet),0) FROM trans_picking_det"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Function Get_Lista_PickingDet_Asociado_By_IdPedidoDet(ByVal IdPedidoDet As Integer,
                                                                        ByRef lConnection As SqlConnection,
                                                                        ByRef ltransaction As SqlTransaction) As List(Of clsBeTrans_picking_det)

        Get_Lista_PickingDet_Asociado_By_IdPedidoDet = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_picking_det
             Where(IdPedidoDet = @IdPedidoDet) "

            Dim cmd As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDODET", IdPedidoDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lPickingDet As New List(Of clsBeTrans_picking_det)
            Dim BePickingDet As clsBeTrans_picking_det

            If dt.Rows.Count = 1 Then

                BePickingDet = New clsBeTrans_picking_det

                Cargar(BePickingDet, dt.Rows(0))

                'BePickingDet.ListaDetalleUbicacion = clsLnTrans_picking_ubic.Get_All_IdStocks_PickingUbic_By_IdPickingUbic(BePickingDet.IdPickingDet, lConnection, ltransaction)

                lPickingDet.Add(BePickingDet)

                Return lPickingDet

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_IdPickingDet(ByVal IdPickingDet As Integer,
                                                    ByRef pConection As SqlConnection,
                                                    ByRef pTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from Trans_picking_det
                                   Where(IdPickingDet = @IdPickingDet)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdPickingDet", IdPickingDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_Params(ByVal IdPickingDet As Integer,
                                              ByVal IdPIckingEnc As Integer,
                                              ByVal IdPedidoEnc As Integer,
                                              ByVal IdPedidoDet As Integer,
                                              ByRef pConection As SqlConnection,
                                              ByRef pTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " DELETE FROM Trans_picking_det 
                                   WHERE(IdPickingDet = @IdPickingDet 
                                   AND IdPickingEnc =@IdPickingEnc 
                                   AND IdPedidoEnc = @IdPedidoEnc 
                                   AND IdPedidoDet = @IdPedidoDet)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IdPickingDet", IdPickingDet))
            cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPIckingEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDet", IdPedidoDet))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'Public Shared Function Liberar_Producto_No_Pickeado(ByVal IdPedidoDet As Integer,
    '                                                    ByVal IdPedidoEnc As Integer,
    '                                                    ByVal IdPickingEnc As Integer,
    '                                                    ByVal IdUsuario As Integer,
    '                                                    ByVal Referencia As String,
    '                                                    ByVal Observacion As String,
    '                                                    ByVal IdBodega As Integer) As Boolean

    '    Liberar_Producto_No_Pickeado = False

    '    Try


    '        Dim IdPickingUbic As Integer = 0
    '        Dim vCantidadDespachada As Double = 0
    '        Dim vCantidadSolicitada As Double = 0
    '        Dim vCantidadRecibida As Double = 0
    '        Dim vPesoDespachado As Double = 0
    '        Dim vIdStock As Double = 0
    '        Dim lBeStockPickeado As New List(Of clsBeStock)
    '        Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
    '        Dim BeStockPickeado As clsBeStock = Nothing
    '        Dim vIdUbicacionPickingTomarDe As Integer
    '        Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
    '        Dim objStockOrigen As New clsBeStock()
    '        Dim vActuarlizarUbicacion As Boolean = False
    '        Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
    '        Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
    '        Dim BeTransPickingDet As New clsBeTrans_picking_det()
    '        Dim vResultEliminacionStock As Integer = 0
    '        Dim vIdLogLiberacion As Integer = 0
    '        Dim vResultadoEliminacionPickingUbic As Integer = 0
    '        Dim vResultadoEliminacionPickingDet As Integer = 0
    '        Dim lStockReservado As New List(Of clsBeVW_stock_res)
    '        Dim BePickingEnc As New clsBeTrans_picking_enc

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            lConnection.Open()

    '            Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '                vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

    '                vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega, lConnection, lTransaction)

    '                If vIdUbicacionPickingDefectoPorBodega = 0 Then
    '                    Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
    '                End If

    '                lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)

    '                lStockReservado = lStockReservado.FindAll(Function(X) X.IdPedidoDet = IdPedidoDet)

    '                If lStockReservado IsNot Nothing Then

    '                    If lStockReservado.Count > 0 Then

    '                        BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

    '                        If Not BePickingEnc Is Nothing Then

    '                            lTransPickingDet = BePickingEnc.ListaPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

    '                            If Not lTransPickingDet Is Nothing Then

    '                                If Not BePickingEnc.ListaPickingUbic Is Nothing Then

    '                                    ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic.FindAll(Function(X) X.IdPedidoEnc = IdPedidoEnc AndAlso X.IdPedidoDet = IdPedidoDet)

    '                                    If Not lTransPickingDet Is Nothing Then

    '                                        lTransPickingDet = lTransPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

    '                                        If Not lTransPickingDet Is Nothing Then

    '                                            If lTransPickingDet.Count > 0 Then

    '                                                For Each PickingDet In lTransPickingDet

    '                                                    If Not ListaDetalleUbicacion Is Nothing Then

    '                                                        For Each PickingUbic In ListaDetalleUbicacion

    '                                                            vResultadoEliminacionPickingUbic = 0
    '                                                            vResultEliminacionStock = 0
    '                                                            vResultadoEliminacionPickingDet = 0

    '                                                            If PickingUbic.Cantidad_despachada = 0 Then

    '                                                                lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
    '                                                                                                                            IdPedidoEnc,
    '                                                                                                                            PickingUbic.IdStock,
    '                                                                                                                            PickingUbic.IdProductoBodega,
    '                                                                                                                            PickingUbic.IdPropietarioBodega,
    '                                                                                                                            IdPickingEnc,
    '                                                                                                                            lConnection,
    '                                                                                                                            lTransaction)

    '                                                                If Not lBeStockResByPickingUbic Is Nothing Then

    '                                                                    If lBeStockResByPickingUbic.Count > 0 Then

    '                                                                        vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_BePickingUbic_And_IdStock(PickingUbic,
    '                                                                                                                                                                      IdPedidoEnc,
    '                                                                                                                                                                      PickingUbic.IdStock,
    '                                                                                                                                                                      PickingUbic.IdBodega,
    '                                                                                                                                                                      lConnection,
    '                                                                                                                                                                      lTransaction)

    '                                                                        If vResultEliminacionStock > 0 Then



    '                                                                            vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_BePickingUbic(PickingUbic,
    '                                                                                                                                                                 lConnection,
    '                                                                                                                                                                 lTransaction)

    '                                                                            If vResultadoEliminacionPickingUbic > 0 Then

    '                                                                                Try

    '                                                                                    vResultadoEliminacionPickingDet = Eliminar_By_BePickingUbic(PickingUbic,
    '                                                                                                                                                lConnection,
    '                                                                                                                                                lTransaction)

    '                                                                                Catch ex As Exception
    '                                                                                    Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
    '                                                                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
    '                                                                                End Try

    '                                                                                Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
    '                                                                                Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
    '                                                                                Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
    '                                                                                Betrans_log_pedido_liberacion.Fecha = Now
    '                                                                                Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
    '                                                                                Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
    '                                                                                Betrans_log_pedido_liberacion.Referencia = Referencia
    '                                                                                Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
    '                                                                                Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
    '                                                                                Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
    '                                                                                Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
    '                                                                                Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock
    '                                                                                Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
    '                                                                                Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
    '                                                                                Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
    '                                                                                Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
    '                                                                                Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
    '                                                                                Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
    '                                                                                Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
    '                                                                                Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
    '                                                                                Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
    '                                                                                Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
    '                                                                                Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

    '                                                                                clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion, lConnection, lTransaction)
    '                                                                                vIdLogLiberacion += 1

    '                                                                            End If

    '                                                                        End If

    '                                                                    End If

    '                                                                End If

    '                                                            Else

    '                                                                lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
    '                                                                                                                            IdPedidoEnc,
    '                                                                                                                            PickingUbic.IdStock,
    '                                                                                                                            PickingUbic.IdProductoBodega,
    '                                                                                                                            PickingUbic.IdPropietarioBodega,
    '                                                                                                                            IdPickingEnc,
    '                                                                                                                            lConnection,
    '                                                                                                                            lTransaction)
    '                                                                If Not lBeStockResByPickingUbic Is Nothing Then

    '                                                                    If lBeStockResByPickingUbic.Count > 0 Then

    '                                                                        '#EJC20220108_1053AM: La cantidad despachada es > 0'
    '                                                                        If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
    '                                                                            '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
    '                                                                            'En este caso, el producto restante debe ser movido a la ubicación de picking

    '                                                                            objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
    '                                                                            vActuarlizarUbicacion = True
    '                                                                        Else
    '                                                                            '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
    '                                                                            'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
    '                                                                            objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
    '                                                                            vActuarlizarUbicacion = False
    '                                                                        End If

    '                                                                        vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
    '                                                                                                                                                                 IdPedidoEnc,
    '                                                                                                                                                                 PickingUbic.IdStock,
    '                                                                                                                                                                 PickingUbic.IdBodega,
    '                                                                                                                                                                 lConnection,
    '                                                                                                                                                                 lTransaction)

    '                                                                        If vResultEliminacionStock > 0 Then

    '                                                                            If vActuarlizarUbicacion Then

    '                                                                                '#CKFK20220717 Me parece que hay que agregar esto para que funcione
    '                                                                                'vIdStock = PickingUbic.IdStock
    '                                                                                objStockOrigen = New clsBeStock()
    '                                                                                objStockOrigen = clsLnStock.GetSingle(vIdStock,
    '                                                                                                                  lConnection,
    '                                                                                                                  lTransaction)

    '                                                                                If Not objStockOrigen Is Nothing Then

    '                                                                                    objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
    '                                                                                    objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
    '                                                                                    objStockOrigen.Fec_mod = Now
    '                                                                                    clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen, lConnection, lTransaction)

    '                                                                                Else
    '                                                                                    Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
    '                                                                                End If


    '                                                                            End If

    '                                                                        End If


    '                                                                    Else
    '                                                                        Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
    '                                                                    End If

    '                                                                Else
    '                                                                    Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
    '                                                                End If

    '                                                            End If

    '                                                        Next

    '                                                    Else
    '                                                        Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
    '                                                    End If

    '                                                Next

    '                                            Else
    '                                                'GT07042022: se muestra como aviso, porque la lista puede estar vacia por no existir, o porque ya fue liberada en un primer intento
    '                                                'Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
    '                                                Throw New Exception("AVISO_20220108_1027: No hay lineas de picking a liberar, la lista esta vacia.")
    '                                            End If

    '                                        Else
    '                                            Throw New Exception("Error_202212061120: El picking det por detalle de pedido es nothing.")
    '                                        End If

    '                                    Else

    '                                        Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

    '                                    End If

    '                                Else
    '                                    Throw New Exception("ERROR_202212061124: La lista de pickiubic por pickingdet es nothing.")
    '                                End If

    '                            Else
    '                                Throw New Exception("Error_202212061119: No se obtuvo detalle de picking_det.")

    '                            End If

    '                        Else
    '                            Throw New Exception("ERROR_202211022303: No se pudo obtener el picking: " & IdPickingEnc)
    '                        End If

    '                    End If

    '                End If

    '                lTransaction.Commit()

    '            End Using

    '            lConnection.Close()

    '        End Using

    '        Liberar_Producto_No_Pickeado = True

    '    Catch ex1 As SqlException
    '        Throw ex1
    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Liberar_Producto_No_Pickeado(ByVal IdPedidoDet As Integer,
                                                        ByVal IdPedidoEnc As Integer,
                                                        ByVal IdPickingEnc As Integer,
                                                        ByVal IdUsuario As Integer,
                                                        ByVal Referencia As String,
                                                        ByVal Observacion As String,
                                                        ByVal IdBodega As Integer,
                                                        ByVal pOpcion As clsDataContractDI.tOpcionLiberaStock) As Boolean

        Liberar_Producto_No_Pickeado = False

        Try

            Dim IdPickingUbic As Integer = 0
            Dim vCantidadDespachada As Double = 0
            Dim vCantidadSolicitada As Double = 0
            Dim vCantidadRecibida As Double = 0
            Dim vPesoDespachado As Double = 0
            Dim vIdStock As Double = 0
            Dim lBeStockPickeado As New List(Of clsBeStock)
            Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
            Dim BeStockPickeado As clsBeStock = Nothing
            Dim vIdUbicacionPickingTomarDe As Integer
            Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
            Dim objStockOrigen As New clsBeStock()
            Dim vActuarlizarUbicacion As Boolean = False
            Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
            Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
            Dim BeTransPickingDet As New clsBeTrans_picking_det()
            Dim vResultEliminacionStock As Integer = 0
            Dim vIdLogLiberacion As Integer = 0
            Dim vResultadoEliminacionPickingUbic As Integer = 0
            Dim vResultadoEliminacionPickingDet As Integer = 0
            Dim lStockReservado As New List(Of clsBeVW_stock_res)
            Dim BePickingEnc As New clsBeTrans_picking_enc
            Dim validacionCantidad As Boolean = False
            Dim ejecutarProceso As Boolean = True

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

                    vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega, lConnection, lTransaction)

                    If vIdUbicacionPickingDefectoPorBodega = 0 Then
                        Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
                    End If

                    lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)

                    lStockReservado = lStockReservado.FindAll(Function(X) X.IdPedidoDet = IdPedidoDet)

                    If lStockReservado IsNot Nothing Then

                        If lStockReservado.Count > 0 Then

                            If (pOpcion = clsDataContractDI.tOpcionLiberaStock.Despacho OrElse pOpcion = clsDataContractDI.tOpcionLiberaStock.StockReservado) Then

                                If XtraMessageBox.Show("¿Liberar stock no pickeado?", pOpcion.ToString(),
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then

                                    ejecutarProceso = False

                                End If

                            End If

                            If (ejecutarProceso) Then

                                BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

                                If Not BePickingEnc Is Nothing Then

                                    lTransPickingDet = BePickingEnc.ListaPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

                                    If Not lTransPickingDet Is Nothing Then

                                        If Not BePickingEnc.ListaPickingUbic Is Nothing Then

                                            ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic.FindAll(Function(X) X.IdPedidoEnc = IdPedidoEnc AndAlso X.IdPedidoDet = IdPedidoDet)

                                            If Not lTransPickingDet Is Nothing Then

                                                lTransPickingDet = lTransPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

                                                If Not lTransPickingDet Is Nothing Then

                                                    If lTransPickingDet.Count > 0 Then

                                                        For Each PickingDet In lTransPickingDet

                                                            If Not ListaDetalleUbicacion Is Nothing Then

                                                                For Each PickingUbic In ListaDetalleUbicacion

                                                                    vResultadoEliminacionPickingUbic = 0
                                                                    vResultEliminacionStock = 0
                                                                    vResultadoEliminacionPickingDet = 0

                                                                    If (pOpcion = clsDataContractDI.tOpcionLiberaStock.Pedido) Then
                                                                        validacionCantidad = (PickingUbic.Cantidad_despachada = 0 AndAlso PickingUbic.Cantidad_Recibida = 0)
                                                                    Else
                                                                        validacionCantidad = (PickingUbic.Cantidad_despachada = 0)
                                                                    End If

                                                                    If (validacionCantidad) Then

                                                                        lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
                                                                                                                                IdPedidoEnc,
                                                                                                                                PickingUbic.IdStock,
                                                                                                                                PickingUbic.IdProductoBodega,
                                                                                                                                PickingUbic.IdPropietarioBodega,
                                                                                                                                IdPickingEnc,
                                                                                                                                lConnection,
                                                                                                                                lTransaction)

                                                                        If Not lBeStockResByPickingUbic Is Nothing Then

                                                                            If lBeStockResByPickingUbic.Count > 0 Then

                                                                                vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_BePickingUbic_And_IdStock(PickingUbic,
                                                                                                                                                                               IdPedidoEnc,
                                                                                                                                                                               PickingUbic.IdStock,
                                                                                                                                                                               PickingUbic.IdBodega,
                                                                                                                                                                               lConnection,
                                                                                                                                                                               lTransaction)

                                                                                If vResultEliminacionStock > 0 Then



                                                                                    vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_BePickingUbic(PickingUbic,
                                                                                                                                                                        lConnection,
                                                                                                                                                                        lTransaction)

                                                                                    If vResultadoEliminacionPickingUbic > 0 Then

                                                                                        Try

                                                                                            vResultadoEliminacionPickingDet = Eliminar_By_BePickingUbic(PickingUbic,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)

                                                                                        Catch ex As Exception
                                                                                            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
                                                                                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                                                                                        End Try

                                                                                        Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
                                                                                        Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
                                                                                        Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
                                                                                        Betrans_log_pedido_liberacion.Fecha = Now
                                                                                        Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
                                                                                        Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
                                                                                        Betrans_log_pedido_liberacion.Referencia = Referencia
                                                                                        Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
                                                                                        Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
                                                                                        Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
                                                                                        Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
                                                                                        Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock
                                                                                        Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
                                                                                        Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
                                                                                        Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
                                                                                        Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
                                                                                        Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
                                                                                        Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
                                                                                        Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
                                                                                        Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
                                                                                        Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
                                                                                        Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
                                                                                        Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

                                                                                        clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion, lConnection, lTransaction)
                                                                                        vIdLogLiberacion += 1

                                                                                    Else
                                                                                        Throw New Exception("#CKFK20230516 No se eliminaron lo registros del PickingUbic")
                                                                                    End If

                                                                                End If

                                                                            End If

                                                                        End If

                                                                    Else

                                                                        '#CKFK20230514 Solo se libera el stock cuando no es pedido 
                                                                        If (pOpcion <> clsDataContractDI.tOpcionLiberaStock.Pedido) Then

                                                                            lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                                                                    IdPedidoEnc,
                                                                                                                                    PickingUbic.IdStock,
                                                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                                                    IdPickingEnc,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)
                                                                            If Not lBeStockResByPickingUbic Is Nothing Then

                                                                                If lBeStockResByPickingUbic.Count > 0 Then

                                                                                    '#EJC20220108_1053AM: La cantidad despachada es > 0'
                                                                                    If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
                                                                                        '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
                                                                                        'En este caso, el producto restante debe ser movido a la ubicación de picking

                                                                                        objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
                                                                                        vActuarlizarUbicacion = True
                                                                                    Else
                                                                                        '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
                                                                                        'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
                                                                                        objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
                                                                                        vActuarlizarUbicacion = False
                                                                                    End If

                                                                                    vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                                                             IdPedidoEnc,
                                                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                                                             lConnection,
                                                                                                                                                                             lTransaction)

                                                                                    If vResultEliminacionStock > 0 Then

                                                                                        If vActuarlizarUbicacion Then

                                                                                            '#CKFK20220717 Me parece que hay que agregar esto para que funcione
                                                                                            'vIdStock = PickingUbic.IdStock
                                                                                            objStockOrigen = New clsBeStock()
                                                                                            objStockOrigen = clsLnStock.GetSingle(vIdStock,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                                                                                            If Not objStockOrigen Is Nothing Then

                                                                                                objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
                                                                                                objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
                                                                                                objStockOrigen.Fec_mod = Now
                                                                                                clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen, lConnection, lTransaction)

                                                                                            Else
                                                                                                Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
                                                                                            End If


                                                                                        End If

                                                                                    End If


                                                                                Else
                                                                                    Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
                                                                                End If

                                                                            Else
                                                                                Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
                                                                            End If

                                                                        Else

                                                                            Debug.Print("No se va a liberar stock")

                                                                        End If

                                                                    End If

                                                                Next

                                                            Else
                                                                Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
                                                            End If

                                                        Next

                                                        '#CKFK20240820 Eliminar el detalle de trans_manufactura_det
                                                        clsLnTrans_manufactura_det.Eliminar_By_IdPedidoDet(IdPedidoDet)

                                                    Else
                                                        'GT07042022: se muestra como aviso, porque la lista puede estar vacia por no existir, o porque ya fue liberada en un primer intento
                                                        'Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
                                                        Throw New Exception("AVISO_20220108_1027: No hay lineas de picking a liberar, la lista esta vacia.")
                                                    End If

                                                Else
                                                    Throw New Exception("Error_202212061120: El picking det por detalle de pedido es nothing.")
                                                End If

                                            Else

                                                Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

                                            End If

                                        Else
                                            Throw New Exception("ERROR_202212061124: La lista de pickingubic por pickingdet es nothing.")
                                        End If

                                    Else
                                        Throw New Exception("Error_202212061119: No se obtuvo detalle de picking_det.")

                                    End If

                                Else
                                    Throw New Exception("ERROR_202211022303: No se pudo obtener el picking: " & IdPickingEnc)
                                End If

                            End If

                        End If

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Liberar_Producto_No_Pickeado = True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    'Public Shared Function Liberar_Producto_No_Pickeado_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
    '                                                                   ByVal IdPickingEnc As Integer,
    '                                                                   ByVal IdUsuario As Integer,
    '                                                                   ByVal Referencia As String,
    '                                                                   ByVal Observacion As String,
    '                                                                   ByVal IdBodega As Integer) As Boolean

    '    Liberar_Producto_No_Pickeado_By_IdPedidoEnc = False

    '    Try

    '        Dim IdPickingUbic As Integer = 0
    '        Dim vCantidadDespachada As Double = 0
    '        Dim vCantidadSolicitada As Double = 0
    '        Dim vCantidadRecibida As Double = 0
    '        Dim vPesoDespachado As Double = 0
    '        Dim vIdStock As Double = 0
    '        Dim lBeStockPickeado As New List(Of clsBeStock)
    '        Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
    '        Dim BeStockPickeado As clsBeStock = Nothing
    '        Dim vIdUbicacionPickingTomarDe As Integer
    '        Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
    '        Dim objStockOrigen As New clsBeStock()
    '        Dim vActuarlizarUbicacion As Boolean = False
    '        Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
    '        Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
    '        Dim BeTransPickingDet As New clsBeTrans_picking_det()
    '        Dim vResultEliminacionStock As Integer = 0
    '        Dim vIdLogLiberacion As Integer = 0
    '        Dim vResultadoEliminacionPickingUbic As Integer = 0
    '        Dim vResultadoEliminacionPickingDet As Integer = 0
    '        Dim lStockReservado As New List(Of clsBeVW_stock_res)
    '        Dim BePickingEnc As New clsBeTrans_picking_enc
    '        Dim validacionCantidad As Boolean = False
    '        Dim ejecutarProceso As Boolean = True

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            lConnection.Open()

    '            Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '                vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

    '                vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega, lConnection, lTransaction)

    '                If vIdUbicacionPickingDefectoPorBodega = 0 Then
    '                    Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
    '                End If

    '                lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)

    '                Dim lPedidoDet As List(Of Integer) = lStockReservado.Select(Function(x) x.IdPedidoDet).Distinct.ToList

    '                If lStockReservado IsNot Nothing Then

    '                    If lStockReservado.Count > 0 Then

    '                        BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

    '                        For Each IdPedidoDet In lPedidoDet

    '                            If Not BePickingEnc Is Nothing Then

    '                                lTransPickingDet = BePickingEnc.ListaPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

    '                                If Not lTransPickingDet Is Nothing Then

    '                                    If Not BePickingEnc.ListaPickingUbic Is Nothing Then

    '                                        ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic.FindAll(Function(X) X.IdPedidoEnc = IdPedidoEnc AndAlso X.IdPedidoDet = IdPedidoDet)

    '                                        If Not lTransPickingDet Is Nothing Then

    '                                            lTransPickingDet = lTransPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

    '                                            If Not lTransPickingDet Is Nothing Then

    '                                                If lTransPickingDet.Count > 0 Then

    '                                                    For Each PickingDet In lTransPickingDet

    '                                                        If Not ListaDetalleUbicacion Is Nothing Then

    '                                                            For Each PickingUbic In ListaDetalleUbicacion

    '                                                                vResultadoEliminacionPickingUbic = 0
    '                                                                vResultEliminacionStock = 0
    '                                                                vResultadoEliminacionPickingDet = 0

    '                                                                validacionCantidad = (PickingUbic.Cantidad_despachada = 0)

    '                                                                If (validacionCantidad) Then

    '                                                                    lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
    '                                                                                                                            IdPedidoEnc,
    '                                                                                                                            PickingUbic.IdStock,
    '                                                                                                                            PickingUbic.IdProductoBodega,
    '                                                                                                                            PickingUbic.IdPropietarioBodega,
    '                                                                                                                            IdPickingEnc,
    '                                                                                                                            lConnection,
    '                                                                                                                            lTransaction)

    '                                                                    If Not lBeStockResByPickingUbic Is Nothing Then

    '                                                                        If lBeStockResByPickingUbic.Count > 0 Then

    '                                                                            vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_BePickingUbic_And_IdStock(PickingUbic,
    '                                                                                                                                                                           IdPedidoEnc,
    '                                                                                                                                                                           PickingUbic.IdStock,
    '                                                                                                                                                                           PickingUbic.IdBodega,
    '                                                                                                                                                                           lConnection,
    '                                                                                                                                                                           lTransaction)

    '                                                                            If vResultEliminacionStock > 0 Then



    '                                                                                vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_BePickingUbic(PickingUbic,
    '                                                                                                                                                                    lConnection,
    '                                                                                                                                                                    lTransaction)

    '                                                                                If vResultadoEliminacionPickingUbic > 0 Then

    '                                                                                    Try

    '                                                                                        vResultadoEliminacionPickingDet = Eliminar_By_BePickingUbic(PickingUbic,
    '                                                                                                                                                lConnection,
    '                                                                                                                                                lTransaction)

    '                                                                                    Catch ex As Exception
    '                                                                                        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
    '                                                                                        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '                                                                                    End Try

    '                                                                                    Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
    '                                                                                    Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
    '                                                                                    Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
    '                                                                                    Betrans_log_pedido_liberacion.Fecha = Now
    '                                                                                    Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
    '                                                                                    Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
    '                                                                                    Betrans_log_pedido_liberacion.Referencia = Referencia
    '                                                                                    Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
    '                                                                                    Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
    '                                                                                    Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
    '                                                                                    Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
    '                                                                                    Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock
    '                                                                                    Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
    '                                                                                    Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
    '                                                                                    Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
    '                                                                                    Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
    '                                                                                    Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
    '                                                                                    Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
    '                                                                                    Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
    '                                                                                    Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
    '                                                                                    Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
    '                                                                                    Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
    '                                                                                    Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

    '                                                                                    clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion, lConnection, lTransaction)
    '                                                                                    vIdLogLiberacion += 1

    '                                                                                Else
    '                                                                                    Throw New Exception("#CKFK20230516 No se eliminaron lo registros del PickingUbic")
    '                                                                                End If

    '                                                                            End If

    '                                                                        End If

    '                                                                    End If

    '                                                                Else


    '                                                                    lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
    '                                                                                                                            IdPedidoEnc,
    '                                                                                                                            PickingUbic.IdStock,
    '                                                                                                                            PickingUbic.IdProductoBodega,
    '                                                                                                                            PickingUbic.IdPropietarioBodega,
    '                                                                                                                            IdPickingEnc,
    '                                                                                                                            lConnection,
    '                                                                                                                            lTransaction)
    '                                                                    If Not lBeStockResByPickingUbic Is Nothing Then

    '                                                                        If lBeStockResByPickingUbic.Count > 0 Then

    '                                                                            '#EJC20220108_1053AM: La cantidad despachada es > 0'
    '                                                                            If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
    '                                                                                '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
    '                                                                                'En este caso, el producto restante debe ser movido a la ubicación de picking

    '                                                                                objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
    '                                                                                vActuarlizarUbicacion = True
    '                                                                            Else
    '                                                                                '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
    '                                                                                'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
    '                                                                                objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
    '                                                                                vActuarlizarUbicacion = False
    '                                                                            End If

    '                                                                            vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
    '                                                                                                                                                                     IdPedidoEnc,
    '                                                                                                                                                                     PickingUbic.IdStock,
    '                                                                                                                                                                     PickingUbic.IdBodega,
    '                                                                                                                                                                     lConnection,
    '                                                                                                                                                                     lTransaction)

    '                                                                            If vResultEliminacionStock > 0 Then

    '                                                                                If vActuarlizarUbicacion Then

    '                                                                                    '#CKFK20220717 Me parece que hay que agregar esto para que funcione
    '                                                                                    vIdStock = PickingUbic.IdStock
    '                                                                                    objStockOrigen = New clsBeStock()
    '                                                                                    objStockOrigen = clsLnStock.GetSingle(vIdStock,
    '                                                                                                                      lConnection,
    '                                                                                                                      lTransaction)

    '                                                                                    If Not objStockOrigen Is Nothing Then

    '                                                                                        objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
    '                                                                                        objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
    '                                                                                        objStockOrigen.Fec_mod = Now
    '                                                                                        clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen, lConnection, lTransaction)

    '                                                                                    Else
    '                                                                                        Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
    '                                                                                    End If


    '                                                                                End If

    '                                                                            End If


    '                                                                        Else
    '                                                                            Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
    '                                                                        End If

    '                                                                    Else
    '                                                                        Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
    '                                                                    End If


    '                                                                End If

    '                                                            Next

    '                                                        Else
    '                                                            Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
    '                                                        End If

    '                                                    Next

    '                                                Else
    '                                                    'GT07042022: se muestra como aviso, porque la lista puede estar vacia por no existir, o porque ya fue liberada en un primer intento
    '                                                    'Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
    '                                                    Throw New Exception("AVISO_20220108_1027: No hay lineas de picking a liberar, la lista esta vacia.")
    '                                                End If

    '                                            Else
    '                                                Throw New Exception("Error_202212061120: El picking det por detalle de pedido es nothing.")
    '                                            End If

    '                                        Else

    '                                            Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

    '                                        End If

    '                                    Else
    '                                        Throw New Exception("ERROR_202212061124: La lista de pickiubic por pickingdet es nothing.")
    '                                    End If

    '                                Else
    '                                    Throw New Exception("Error_202212061119: No se obtuvo detalle de picking_det.")

    '                                End If

    '                            Else
    '                                Throw New Exception("ERROR_202211022303: No se pudo obtener el picking: " & IdPickingEnc)
    '                            End If

    '                        Next

    '                    End If

    '                End If

    '                lTransaction.Commit()

    '            End Using

    '            lConnection.Close()

    '        End Using

    '        Liberar_Producto_No_Pickeado_By_IdPedidoEnc = True

    '    Catch ex1 As SqlException
    '        Throw ex1
    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw ex
    '    End Try

    'End Function


    Public Shared Function Liberar_Producto_No_Pickeado(ByVal IdPedidoDet As Integer,
                                                        ByVal IdPedidoEnc As Integer,
                                                        ByVal IdPickingEnc As Integer,
                                                        ByVal IdUsuario As Integer,
                                                        ByVal Referencia As String,
                                                        ByVal Observacion As String,
                                                        ByVal IdBodega As Integer,
                                                        ByVal IdStockRes As Integer) As Boolean

        Liberar_Producto_No_Pickeado = False

        Try

            Dim IdPickingUbic As Integer = 0
            Dim vCantidadDespachada As Double = 0
            Dim vCantidadSolicitada As Double = 0
            Dim vCantidadRecibida As Double = 0
            Dim vPesoDespachado As Double = 0
            Dim vIdStock As Double = 0
            Dim lBeStockPickeado As New List(Of clsBeStock)
            Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
            Dim BeStockPickeado As clsBeStock = Nothing
            Dim vIdUbicacionPickingTomarDe As Integer
            Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
            Dim objStockOrigen As New clsBeStock()
            Dim vActuarlizarUbicacion As Boolean = False
            Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
            Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
            Dim BeTransPickingDet As New clsBeTrans_picking_det()
            Dim vResultEliminacionStock As Integer = 0
            Dim vIdLogLiberacion As Integer = 0
            Dim vResultadoEliminacionPickingUbic As Integer = 0
            Dim vResultadoEliminacionPickingDet As Integer = 0
            Dim lPickingUbic As New List(Of clsBeTrans_picking_ubic)
            Dim BePickingEnc As New clsBeTrans_picking_enc

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

                    vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega,
                                                                                                          lConnection,
                                                                                                          lTransaction)

                    If vIdUbicacionPickingDefectoPorBodega = 0 Then
                        Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
                    End If

                    BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

                    If Not BePickingEnc Is Nothing Then

                        lTransPickingDet = BePickingEnc.ListaPickingDet
                        ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic

                        If Not lTransPickingDet Is Nothing Then

                            If lTransPickingDet.Count > 0 Then

                                For Each PickingDet In lTransPickingDet

                                    If Not ListaDetalleUbicacion Is Nothing Then

                                        For Each PickingUbic In ListaDetalleUbicacion.Where(Function(x) x.IdStockRes = IdStockRes _
                                                                                            AndAlso x.IdPickingEnc = IdPickingEnc _
                                                                                            AndAlso x.IdPedidoDet = IdPedidoDet _
                                                                                            AndAlso x.IdPedidoEnc = IdPedidoEnc)

                                            vResultadoEliminacionPickingUbic = 0
                                            vResultEliminacionStock = 0
                                            vResultadoEliminacionPickingDet = 0

                                            If PickingUbic.Cantidad_despachada = 0 Then

                                                lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
                                                                                                            IdPedidoEnc,
                                                                                                            PickingUbic.IdStock,
                                                                                                            IdStockRes,
                                                                                                            PickingUbic.IdProductoBodega,
                                                                                                            PickingUbic.IdPropietarioBodega,
                                                                                                            IdPickingEnc,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                                                If Not lBeStockResByPickingUbic Is Nothing Then

                                                    If lBeStockResByPickingUbic.Count > 0 Then

                                                        vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(PickingUbic.IdPedidoDet,
                                                                                                                                                     IdPedidoEnc,
                                                                                                                                                     PickingUbic.IdStock,
                                                                                                                                                     IdStockRes,
                                                                                                                                                     PickingUbic.IdBodega,
                                                                                                                                                     lConnection,
                                                                                                                                                     lTransaction)

                                                        If vResultEliminacionStock > 0 Then

                                                            lPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingDet(PickingUbic.IdPickingDet,
                                                                                                                                       PickingUbic.IdPickingEnc,
                                                                                                                                       lConnection,
                                                                                                                                       lTransaction)

                                                            vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_Params(PickingUbic.IdPickingUbic,
                                                                                                                                          PickingUbic.IdPickingEnc,
                                                                                                                                          PickingUbic.IdPickingDet,
                                                                                                                                          PickingUbic.IdStock,
                                                                                                                                          IdStockRes,
                                                                                                                                          lConnection,
                                                                                                                                          lTransaction)

                                                            If vResultadoEliminacionPickingUbic > 0 Then

                                                                Try

                                                                    If lPickingUbic.Count = 1 Then

                                                                        vResultadoEliminacionPickingDet = Eliminar_By_Params(PickingUbic.IdPickingDet,
                                                                                                                             PickingUbic.IdPickingEnc,
                                                                                                                             IdPedidoEnc,
                                                                                                                             IdPedidoDet,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                                                    End If

                                                                Catch ex As Exception
                                                                    Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
                                                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                                                                End Try

                                                                Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
                                                                Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
                                                                Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
                                                                Betrans_log_pedido_liberacion.Fecha = Now
                                                                Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
                                                                Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
                                                                Betrans_log_pedido_liberacion.Referencia = Referencia
                                                                Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
                                                                Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
                                                                Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
                                                                Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
                                                                Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock
                                                                Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
                                                                Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
                                                                Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
                                                                Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
                                                                Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
                                                                Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
                                                                Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
                                                                Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
                                                                Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
                                                                Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
                                                                Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

                                                                clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion,
                                                                                                          lConnection,
                                                                                                          lTransaction)
                                                                vIdLogLiberacion += 1

                                                            End If

                                                        End If

                                                    End If

                                                End If

                                            Else

                                                lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                                            IdPedidoEnc,
                                                                                                            PickingUbic.IdStock,
                                                                                                            PickingUbic.IdProductoBodega,
                                                                                                            PickingUbic.IdPropietarioBodega,
                                                                                                            IdPickingEnc,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                                                If Not lBeStockResByPickingUbic Is Nothing Then

                                                    If lBeStockResByPickingUbic.Count > 0 Then

                                                        '#EJC20220108_1053AM: La cantidad despachada es > 0'
                                                        If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
                                                            '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
                                                            'En este caso, el producto restante debe ser movido a la ubicación de picking

                                                            objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
                                                            vActuarlizarUbicacion = True
                                                        Else
                                                            '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
                                                            'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
                                                            objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
                                                            vActuarlizarUbicacion = False
                                                        End If

                                                        vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                                     IdPedidoEnc,
                                                                                                                                                     PickingUbic.IdStock,
                                                                                                                                                     PickingUbic.IdBodega,
                                                                                                                                                     lConnection,
                                                                                                                                                     lTransaction)

                                                        If vResultEliminacionStock > 0 Then

                                                            If vActuarlizarUbicacion Then

                                                                '#CKFK20221104 Agregué esto para que funcione el eliminar
                                                                vIdStock = PickingUbic.IdStock
                                                                objStockOrigen = New clsBeStock()
                                                                objStockOrigen = clsLnStock.GetSingle(vIdStock,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                                                                If Not objStockOrigen Is Nothing Then

                                                                    objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
                                                                    objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
                                                                    objStockOrigen.Fec_mod = Now
                                                                    clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen,
                                                                                                               lConnection,
                                                                                                               lTransaction)

                                                                Else
                                                                    Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
                                                                End If


                                                            End If

                                                        End If


                                                    Else
                                                        Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
                                                    End If

                                                Else
                                                    Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
                                                End If

                                            End If

                                        Next

                                    Else
                                        Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
                                    End If

                                Next

                            Else
                                Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
                            End If

                        Else

                            Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

                        End If

                        Liberar_Producto_No_Pickeado = True

                    Else
                        Throw New Exception("ERROR_202211022305: No se pudo obtener el picking: " & IdPickingEnc)
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_PickingDet(ByRef pBePedidoDet As clsBeTrans_pe_det,
                                               ByVal pIdPickingEnc As Integer,
                                               ByRef pIdPickingDet As Integer,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As Boolean

        Insertar_PickingDet = False

        Try

            'Se tomará producto de un nuevo IdStock, insertar nuevo picking_ubic.
            Dim ObjBePickingDet As New clsBeTrans_picking_det
            Dim ObjProducto As New clsBeProducto
            ObjBePickingDet.Presentacion = New clsBeProducto_Presentacion
            ObjBePickingDet.UnidadMedida = New clsBeUnidad_medida
            ObjBePickingDet.ProductoEstado = New clsBeProducto_estado
            ObjBePickingDet.Producto = New clsBeProducto

            ObjProducto = clsLnProducto.GetSingle(pBePedidoDet.Producto.IdProducto, lConnection, lTransaction)

            pIdPickingDet = MaxID(lConnection, lTransaction) + 1
            ObjBePickingDet.IdPickingEnc = pIdPickingEnc
            ObjBePickingDet.IdPickingDet = pIdPickingDet
            ObjBePickingDet.IdPedidoEnc = pBePedidoDet.IdPedidoEnc
            ObjBePickingDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
            ObjBePickingDet.Cantidad = pBePedidoDet.Cantidad
            ObjBePickingDet.User_agr = pBePedidoDet.User_agr
            ObjBePickingDet.Fec_agr = Now
            ObjBePickingDet.User_mod = pBePedidoDet.User_agr
            ObjBePickingDet.Fec_mod = Now
            ObjBePickingDet.Activo = True
            ObjBePickingDet.IsNew = True
            ObjBePickingDet.Producto.Codigo = ObjProducto.Codigo
            ObjBePickingDet.Codigo = ObjProducto.Codigo
            ObjBePickingDet.NombreProducto = pBePedidoDet.Producto.Nombre
            ObjBePickingDet.Producto.Nombre = pBePedidoDet.Producto.Nombre
            ObjBePickingDet.Presentacion.IdPresentacion = pBePedidoDet.IdPresentacion
            ObjBePickingDet.Presentacion.Nombre = pBePedidoDet.Nom_presentacion
            ObjBePickingDet.UnidadMedida.IdUnidadMedida = pBePedidoDet.IdUnidadMedidaBasica
            ObjBePickingDet.UnidadMedida.Nombre = pBePedidoDet.Nom_unid_med
            ObjBePickingDet.ProductoEstado.IdEstado = pBePedidoDet.IdEstado
            ObjBePickingDet.ProductoEstado.Nombre = pBePedidoDet.Nom_estado
            ObjBePickingDet.Cantidad = pBePedidoDet.Cantidad
            ObjBePickingDet.Cantidad_recibida = 0
            ObjBePickingDet.Cliente_dias = 0

            Insertar(ObjBePickingDet, lConnection, lTransaction)

            Insertar_PickingDet = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Guarda_Trans_picking_det(ByVal IdPickingEnc As Integer,
                                               ByVal IdBodegaMuelle As Integer,
                                               ByVal pListObjPickingDet As List(Of clsBeTrans_picking_det),
                                               ByVal pListObjPickingUbic As List(Of clsBeTrans_picking_ubic),
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxIdDet As Integer = 0
            Dim lPedidosEnc As New List(Of Integer)
            Dim beListPickingUbic As New List(Of clsBeTrans_picking_ubic)
            Dim usuario As Integer = 0

            For Each BeTransPickingDet As clsBeTrans_picking_det In pListObjPickingDet

                If Not lPedidosEnc.Exists(Function(x) x = BeTransPickingDet.IdPedidoEnc) Then
                    lPedidosEnc.Add(BeTransPickingDet.IdPedidoEnc)
                End If

                If BeTransPickingDet.IsNew Then

                    lMaxIdDet = MaxID(lConnection, lTransaction) + 1

                    BeTransPickingDet.IdPickingEnc = IdPickingEnc

                    beListPickingUbic = pListObjPickingUbic.FindAll(Function(x) x.IdPickingDet = BeTransPickingDet.IdPickingDet _
                                                                        AndAlso x.IdPedidoDet = BeTransPickingDet.IdPedidoDet _
                                                                        AndAlso x.IdPedidoEnc = BeTransPickingDet.IdPedidoEnc)

                    '#GT07072025: obtener un usuario aunque no es la mejor manera.
                    If usuario = 0 Then
                        Dim registro = beListPickingUbic.FirstOrDefault()
                        If registro IsNot Nothing Then
                            usuario = registro.User_agr
                        End If
                    End If


                    For Each BePickingUbic In beListPickingUbic
                        BePickingUbic.IdPickingDet = lMaxIdDet
                    Next

                    BeTransPickingDet.IdPickingDet = lMaxIdDet

                    Insertar(BeTransPickingDet, lConnection, lTransaction)

                Else
                    Actualizar(BeTransPickingDet, lConnection, lTransaction)
                End If

            Next

            For Each IdPedidoEnc In lPedidosEnc

                clsLnTrans_pe_enc.ActualizarIdPickingEnc(IdPickingEnc,
                                                         IdPedidoEnc,
                                                         lConnection,
                                                         lTransaction)

                clsLnTrans_pe_enc.Actualizar_Estado_Pendiente(IdPedidoEnc,
                                                              usuario,
                                                              lConnection,
                                                              lTransaction)

                '#EJC202304111703: Actualizar el timestamp de inicio de preparación al crear el picking.
                clsLnTrans_pe_enc.Actualizar_Fecha_Inicio_Preparacion(IdPedidoEnc,
                                                                      lConnection,
                                                                      lTransaction)


                '#GT29042025: si el pedido no maneja muelle para ciertos clientes, no actualizar.
                If IdBodegaMuelle > 0 Then
                    '#EJC20240609: Actualizar el muelle del pedido.
                    clsLnTrans_pe_enc.Actualizar_IdMuelle_By_IdPedidoEnc(IdPedidoEnc,
                                                                         IdBodegaMuelle,
                                                                         lConnection,
                                                                         lTransaction)
                End If


            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_IdPicking_Enc_By_IdPedidoDet(ByVal pIdPedidoEnc As Integer, ByVal pIdPedidoDet As Integer) As Integer

        Get_IdPicking_Enc_By_IdPedidoDet = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Dim vSQL As String = "SELECT IdPickingEnc FROM trans_picking_det WHERE IdPedidoDet=@IdPedidoDet and IdPedidoEnc=@IdPedidoEnc"

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_IdPicking_Enc_By_IdPedidoDet = lDataTable.Rows(0).Item("IdPickingEnc")

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

    Public Shared Function Get_IdPicking_Enc_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                            ByVal pIdPedidoEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Get_IdPicking_Enc_By_IdPedidoDet = 0

        Try

            Dim vSQL As String = "SELECT IdPickingEnc FROM trans_picking_det WHERE IdPedidoDet=@IdPedidoDet and IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_IdPicking_Enc_By_IdPedidoDet = lDataTable.Rows(0).Item("IdPickingEnc")

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_By_BePickingUbic(ByVal BePickingUbic As clsBeTrans_picking_ubic,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As Integer

        Dim vDiferencia As Double = 0
        Dim rowsAffected As Integer = 0
        Dim lPickingUbic As New List(Of clsBeTrans_picking_ubic)

        Try

            '#EJC20220412: Si recibió menos de lo que solicitaron (aun tiene pendiente por pickear)
            If BePickingUbic.Cantidad_Solicitada > BePickingUbic.Cantidad_Recibida Then

                vDiferencia = BePickingUbic.Cantidad_Solicitada - BePickingUbic.Cantidad_Recibida

                lPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingDet(BePickingUbic.IdPickingDet,
                                                                                           BePickingUbic.IdPickingEnc,
                                                                                           lConnection,
                                                                                           lTransaction)

                '#EJC20220412: No estoy seguro de que esto se deba borrar...
                'Pero solo lo vamos a eliminar, si no se ha pickeado nada de lo solicitado.
                If BePickingUbic.Cantidad_Solicitada = vDiferencia Then

                    '#EJC20220412: Si no hay picking_ubic asociado al picking det, entonces podemos eliminar el pikcingdet
                    If lPickingUbic.Count = 0 Then

                        Const sp As String = " DELETE FROM Trans_picking_det 
                                               WHERE(IdPickingDet = @IdPickingDet 
                                               AND IdPickingEnc =@IdPickingEnc 
                                               AND IdPedidoEnc = @IdPedidoEnc 
                                               AND IdPedidoDet = @IdPedidoDet)"

                        Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        cmd.Parameters.Add(New SqlParameter("@IdPickingDet", BePickingUbic.IdPickingDet))
                        cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", BePickingUbic.IdPickingEnc))
                        cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", BePickingUbic.IdPedidoEnc))
                        cmd.Parameters.Add(New SqlParameter("@IdPedidoDet", BePickingUbic.IdPedidoDet))

                        rowsAffected = cmd.ExecuteNonQuery()

                        cmd.Dispose()

                    Else
                        '#EJC20220412: Aun hay registros de pickingubic asociados al pickingdet, no se puede eliminar.
                    End If

                Else
                    Debug.Write("Something is still missing")
                End If

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Picking_Det_Pendientes_For_HH_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                                                 ByRef lConnection As SqlConnection,
                                                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_det)

        Dim lReturnList As New List(Of clsBeTrans_picking_det)

        Try

            '#CKFKF Agregué los campos codigo y nombre en la vista VW_Picking_Det_By_IdPickingEnc para que se mostraran los datos en dgridDetallePicking porque salían vacíos
            Dim vSQL As String = "SELECT * FROM VW_Picking_Det_By_IdPickingEnc WHERE IdPickingEnc=@IdPickingEnc 
                                  AND IdPickingDet IN (select IdPickingDet FROM VW_PickingUbic_By_IdPickingEnc
                                  WHERE (IdPickingEnc = @IdPickingEnc  AND dañado_picking = 0 and no_encontrado = 0 and cantidad_recibida < cantidad_solicitada) ) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_det

                        Obj.Bodega = CType(lRow("Bodega"), String)
                        Obj.Cliente = CType(lRow("Cliente"), String)
                        Obj.Propietario = CType(lRow("Propietario"), String)
                        Obj.FechaPedido = CType(lRow("fecha_pedido"), System.DateTime)

                        Cargar(Obj, lRow)

                        Obj.Producto.IdProducto = CType(lRow("IdProducto"), Integer)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.Presentacion.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                            Obj.Presentacion.Nombre = IIf(IsDBNull(lRow("Nombre_Presentacion")), "", lRow("Nombre_Presentacion"))
                        End If

                        If lRow("idUnidadMedidaBasica") IsNot DBNull.Value AndAlso lRow("idUnidadMedidaBasica") IsNot Nothing Then
                            Obj.UnidadMedida.IdUnidadMedida = CType(lRow("idUnidadMedidaBasica"), Integer)
                            Obj.UnidadMedida.Nombre = IIf(IsDBNull(lRow("Nombre_Unidad_Medida")), "", lRow("Nombre_Unidad_Medida"))
                        End If

                        If lRow("IdEstado") IsNot DBNull.Value AndAlso lRow("IdEstado") IsNot Nothing Then
                            Obj.ProductoEstado.IdEstado = CType(lRow("IdEstado"), Integer)
                            Obj.ProductoEstado.Nombre = IIf(IsDBNull(lRow("Nombre_Estado")), "", lRow("Nombre_Estado"))
                        End If

                        If lRow("IdOperadorBodega") IsNot DBNull.Value AndAlso lRow("IdOperadorBodega") IsNot Nothing Then
                            Obj.IdOperadorBodega = CType(lRow("IdOperadorBodega"), Integer)
                        End If

                        Obj.IsNew = False

                        Obj.IdPedidoEnc = IIf(IsDBNull(lRow("IdPedidoEnc")), 0, lRow("IdPedidoEnc"))

                        Obj.ListaDetalleParametro = clsLnTrans_picking_det_parametros.Get_All_By_IdPickingDet(Obj.IdPickingDet,
                                                                                                              lConnection,
                                                                                                              lTransaction)

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Liberar_Producto_No_Pickeado_Multiple(ByVal IdPedidoDet As Integer,
                                                                 ByVal IdPedidoEnc As Integer,
                                                                 ByVal IdPickingEnc As Integer,
                                                                 ByVal IdUsuario As Integer,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdStockRes As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Boolean

        Liberar_Producto_No_Pickeado_Multiple = False

        Try

            Dim IdPickingUbic As Integer = 0
            Dim vCantidadDespachada As Double = 0
            Dim vCantidadSolicitada As Double = 0
            Dim vCantidadRecibida As Double = 0
            Dim vPesoDespachado As Double = 0
            Dim vIdStock As Double = 0
            Dim lBeStockPickeado As New List(Of clsBeStock)
            Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
            Dim BeStockPickeado As clsBeStock = Nothing
            Dim vIdUbicacionPickingTomarDe As Integer
            Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
            Dim objStockOrigen As New clsBeStock()
            Dim vActuarlizarUbicacion As Boolean = False
            Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
            Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
            Dim BeTransPickingDet As New clsBeTrans_picking_det()
            Dim vResultEliminacionStock As Integer = 0
            Dim vIdLogLiberacion As Integer = 0
            Dim vResultadoEliminacionPickingUbic As Integer = 0
            Dim vResultadoEliminacionPickingDet As Integer = 0
            Dim lPickingUbic As New List(Of clsBeTrans_picking_ubic)
            Dim BePedidoEnc As New clsBeTrans_pe_enc
            Dim BePickingEnc As New clsBeTrans_picking_enc

            BePedidoEnc = clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc_And_IdBodega(IdPedidoEnc,
                                                                                   IdBodega,
                                                                                   lConnection,
                                                                                   lTransaction)



            If Not BePedidoEnc Is Nothing Then

                vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

                vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                If vIdUbicacionPickingDefectoPorBodega = 0 Then
                    Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
                End If

                BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

                lTransPickingDet = BePickingEnc.ListaPickingDet
                ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic

                If Not lTransPickingDet Is Nothing Then

                    If lTransPickingDet.Count > 0 Then

                        For Each PickingDet In lTransPickingDet

                            If Not ListaDetalleUbicacion Is Nothing Then

                                For Each PickingUbic In ListaDetalleUbicacion.Where(Function(x) x.IdStockRes = IdStockRes _
                                                                                    AndAlso x.IdPickingEnc = IdPickingEnc _
                                                                                    AndAlso x.IdPedidoDet = IdPedidoDet _
                                                                                    AndAlso x.IdPedidoEnc = IdPedidoEnc _
                                                                                    AndAlso x.IdStockRes = IdStockRes)

                                    vResultadoEliminacionPickingUbic = 0
                                    vResultEliminacionStock = 0
                                    vResultadoEliminacionPickingDet = 0

                                    If PickingUbic.Cantidad_despachada = 0 Then

                                        lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
                                                                                                    IdPedidoEnc,
                                                                                                    PickingUbic.IdStock,
                                                                                                    IdStockRes,
                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                    IdPickingEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                        If Not lBeStockResByPickingUbic Is Nothing Then

                                            If lBeStockResByPickingUbic.Count > 0 Then

                                                vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(PickingUbic.IdPedidoDet,
                                                                                                                                             IdPedidoEnc,
                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                             IdStockRes,
                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                                                If vResultEliminacionStock > 0 Then

                                                    lPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingDet(PickingUbic.IdPickingDet,
                                                                                                                               PickingUbic.IdPickingEnc,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)

                                                    vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_Params(PickingUbic.IdPickingUbic,
                                                                                                                                  PickingUbic.IdPickingEnc,
                                                                                                                                  PickingUbic.IdPickingDet,
                                                                                                                                  PickingUbic.IdStock,
                                                                                                                                  IdStockRes,
                                                                                                                                  lConnection,
                                                                                                                                  lTransaction)

                                                    If vResultadoEliminacionPickingUbic > 0 Then

                                                        Try

                                                            If lPickingUbic.Count = 1 Then

                                                                vResultadoEliminacionPickingDet = Eliminar_By_Params(PickingUbic.IdPickingDet,
                                                                                                                     PickingUbic.IdPickingEnc,
                                                                                                                     IdPedidoEnc,
                                                                                                                     IdPedidoDet,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

                                                            End If

                                                        Catch ex As Exception
                                                            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
                                                            clsLnLog_error_wms.Agregar_Error(vMsgError)
                                                        End Try

                                                        Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
                                                        Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
                                                        Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
                                                        Betrans_log_pedido_liberacion.Fecha = Now
                                                        Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
                                                        Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
                                                        Betrans_log_pedido_liberacion.Referencia = BePedidoEnc.Referencia
                                                        Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
                                                        Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
                                                        Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
                                                        Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
                                                        Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock & " IdStockRes: " & IdStockRes
                                                        Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
                                                        Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
                                                        Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
                                                        Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
                                                        Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
                                                        Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
                                                        Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
                                                        Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
                                                        Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
                                                        Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
                                                        Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

                                                        clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                                                        vIdLogLiberacion += 1

                                                    End If

                                                End If

                                            End If

                                        End If

                                    Else

                                        lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                                    IdPedidoEnc,
                                                                                                    PickingUbic.IdStock,
                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                    IdPickingEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                                        If Not lBeStockResByPickingUbic Is Nothing Then

                                            If lBeStockResByPickingUbic.Count > 0 Then

                                                '#EJC20220108_1053AM: La cantidad despachada es > 0'
                                                If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
                                                    '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
                                                    'En este caso, el producto restante debe ser movido a la ubicación de picking

                                                    objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
                                                    vActuarlizarUbicacion = True
                                                Else
                                                    '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
                                                    'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
                                                    objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
                                                    vActuarlizarUbicacion = False
                                                End If

                                                vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                             IdPedidoEnc,
                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                             lConnection,
                                                                                                                                             lTransaction)

                                                If vResultEliminacionStock > 0 Then

                                                    If vActuarlizarUbicacion Then

                                                        '#CKFK20220717 Me parece que hay que agregar esto para que funcione
                                                        'vIdStock = PickingUbic.IdStock
                                                        objStockOrigen = New clsBeStock()
                                                        objStockOrigen = clsLnStock.GetSingle(vIdStock,
                                                                                              lConnection,
                                                                                              lTransaction)

                                                        If Not objStockOrigen Is Nothing Then

                                                            objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
                                                            objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
                                                            objStockOrigen.Fec_mod = Now
                                                            clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen,
                                                                                                       lConnection,
                                                                                                       lTransaction)

                                                        Else
                                                            Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
                                                        End If


                                                    End If

                                                End If


                                            Else
                                                Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
                                            End If

                                        Else
                                            Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
                                        End If

                                    End If

                                Next

                            Else
                                Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
                            End If

                        Next

                        Liberar_Producto_No_Pickeado_Multiple = True

                    Else
                        Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
                    End If

                Else

                    Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

                End If

            Else
                Throw New Exception("ERROR_20221006_1618: No se pudo obtener el objeto del pedido con el identificador: " & IdPedidoEnc)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Liberar_Producto_No_Pickeado(ByVal IdPedidoDet As Integer,
                                                        ByVal IdPedidoEnc As Integer,
                                                        ByVal IdPickingEnc As Integer,
                                                        ByVal IdUsuario As Integer,
                                                        ByVal Referencia As String,
                                                        ByVal Observacion As String,
                                                        ByVal IdBodega As Integer,
                                                        ByVal pOpcion As clsDataContractDI.tOpcionLiberaStock,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Boolean

        Liberar_Producto_No_Pickeado = False

        Try

            Dim IdPickingUbic As Integer = 0
            Dim vCantidadDespachada As Double = 0
            Dim vCantidadSolicitada As Double = 0
            Dim vCantidadRecibida As Double = 0
            Dim vPesoDespachado As Double = 0
            Dim vIdStock As Double = 0
            Dim lBeStockPickeado As New List(Of clsBeStock)
            Dim lBeStockResByPickingUbic As New List(Of clsBeStock_res)
            Dim BeStockPickeado As clsBeStock = Nothing
            Dim vIdUbicacionPickingTomarDe As Integer
            Dim vIdUbicacionPickingDefectoPorBodega As Integer = 0
            Dim objStockOrigen As New clsBeStock()
            Dim vActuarlizarUbicacion As Boolean = False
            Dim lTransPickingDet As New List(Of clsBeTrans_picking_det)
            Dim ListaDetalleUbicacion As New List(Of clsBeTrans_picking_ubic)
            Dim BeTransPickingDet As New clsBeTrans_picking_det()
            Dim vResultEliminacionStock As Integer = 0
            Dim vIdLogLiberacion As Integer = 0
            Dim vResultadoEliminacionPickingUbic As Integer = 0
            Dim vResultadoEliminacionPickingDet As Integer = 0
            Dim lStockReservado As New List(Of clsBeVW_stock_res)
            Dim BePickingEnc As New clsBeTrans_picking_enc
            Dim validacionCantidad As Boolean = False
            Dim ejecutarProceso As Boolean = True

            vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

            vIdUbicacionPickingDefectoPorBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega, lConnection, lTransaction)

            If vIdUbicacionPickingDefectoPorBodega = 0 Then
                Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
            End If

            lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)

            lStockReservado = lStockReservado.FindAll(Function(X) X.IdPedidoDet = IdPedidoDet)

            If lStockReservado IsNot Nothing Then

                If lStockReservado.Count > 0 Then

                    If (pOpcion = clsDataContractDI.tOpcionLiberaStock.Pedido) Then

                        If XtraMessageBox.Show("¿Liberar el stock de el producto no pickeado?", "Despacho",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then

                            ejecutarProceso = False

                        End If

                    End If

                    If (ejecutarProceso) Then

                        BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)

                        If Not BePickingEnc Is Nothing Then

                            lTransPickingDet = BePickingEnc.ListaPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

                            If Not lTransPickingDet Is Nothing Then

                                If Not BePickingEnc.ListaPickingUbic Is Nothing Then

                                    ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic.FindAll(Function(X) X.IdPedidoEnc = IdPedidoEnc AndAlso X.IdPedidoDet = IdPedidoDet)

                                    If Not lTransPickingDet Is Nothing Then

                                        lTransPickingDet = lTransPickingDet.FindAll(Function(x) x.IdPedidoDet = IdPedidoDet)

                                        If Not lTransPickingDet Is Nothing Then

                                            If lTransPickingDet.Count > 0 Then

                                                For Each PickingDet In lTransPickingDet

                                                    If Not ListaDetalleUbicacion Is Nothing Then

                                                        For Each PickingUbic In ListaDetalleUbicacion

                                                            vResultadoEliminacionPickingUbic = 0
                                                            vResultEliminacionStock = 0
                                                            vResultadoEliminacionPickingDet = 0

                                                            If (pOpcion = clsDataContractDI.tOpcionLiberaStock.Pedido) Then
                                                                validacionCantidad = (PickingUbic.Cantidad_despachada = 0 AndAlso PickingUbic.Cantidad_Recibida = 0)
                                                            Else
                                                                validacionCantidad = (PickingUbic.Cantidad_despachada = 0)
                                                            End If

                                                            If (validacionCantidad) Then

                                                                lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(PickingUbic.IdPedidoDet,
                                                                                                                            IdPedidoEnc,
                                                                                                                            PickingUbic.IdStock,
                                                                                                                            PickingUbic.IdProductoBodega,
                                                                                                                            PickingUbic.IdPropietarioBodega,
                                                                                                                            IdPickingEnc,
                                                                                                                            lConnection,
                                                                                                                            lTransaction)

                                                                If Not lBeStockResByPickingUbic Is Nothing Then

                                                                    If lBeStockResByPickingUbic.Count > 0 Then

                                                                        vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_BePickingUbic_And_IdStock(PickingUbic,
                                                                                                                                                                        IdPedidoEnc,
                                                                                                                                                                        PickingUbic.IdStock,
                                                                                                                                                                        PickingUbic.IdBodega,
                                                                                                                                                                        lConnection,
                                                                                                                                                                        lTransaction)

                                                                        If vResultEliminacionStock > 0 Then



                                                                            vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_BePickingUbic(PickingUbic,
                                                                                                                                                                 lConnection,
                                                                                                                                                                 lTransaction)

                                                                            If vResultadoEliminacionPickingUbic > 0 Then

                                                                                Try

                                                                                    vResultadoEliminacionPickingDet = Eliminar_By_BePickingUbic(PickingUbic,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)

                                                                                Catch ex As Exception
                                                                                    Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "Error_20220108_1148: Al eliminar el PickingDet: " & PickingUbic.IdPickingDet & " Probablemente ya tenga un despacho asociado. ")
                                                                                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                                                                                End Try

                                                                                Dim Betrans_log_pedido_liberacion As New clsBeTrans_log_pedido_liberacion
                                                                                Betrans_log_pedido_liberacion.IdLogLiberacionStock = vIdLogLiberacion
                                                                                Betrans_log_pedido_liberacion.IdUsuario = IdUsuario
                                                                                Betrans_log_pedido_liberacion.Fecha = Now
                                                                                Betrans_log_pedido_liberacion.IdPedidoEnc = IdPedidoEnc
                                                                                Betrans_log_pedido_liberacion.IdPedidoDet = IdPedidoDet
                                                                                Betrans_log_pedido_liberacion.Referencia = Referencia
                                                                                Betrans_log_pedido_liberacion.Codigo_Producto = PickingDet.Codigo
                                                                                Betrans_log_pedido_liberacion.Lote = PickingUbic.Lote
                                                                                Betrans_log_pedido_liberacion.Lic_Plate = PickingUbic.Lic_plate
                                                                                Betrans_log_pedido_liberacion.Fecha_Vence = PickingUbic.Fecha_Vence
                                                                                Betrans_log_pedido_liberacion.Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock
                                                                                Betrans_log_pedido_liberacion.Cantidad = PickingUbic.Cantidad_Solicitada
                                                                                Betrans_log_pedido_liberacion.IdPickingUbic = PickingUbic.IdPickingUbic
                                                                                Betrans_log_pedido_liberacion.IdPickingDet = PickingDet.IdPickingDet
                                                                                Betrans_log_pedido_liberacion.IdProductoBodega = PickingUbic.IdProductoBodega
                                                                                Betrans_log_pedido_liberacion.IdProductoEstado = PickingUbic.IdPickingDet
                                                                                Betrans_log_pedido_liberacion.IdPropietarioBodega = PickingUbic.IdPropietarioBodega
                                                                                Betrans_log_pedido_liberacion.IdUnidadMedida = PickingUbic.IdUnidadMedida
                                                                                Betrans_log_pedido_liberacion.IdPresentacion = PickingUbic.IdPresentacion
                                                                                Betrans_log_pedido_liberacion.IdUbicacion = PickingUbic.IdUbicacion
                                                                                Betrans_log_pedido_liberacion.IdStock = PickingUbic.IdStock
                                                                                Betrans_log_pedido_liberacion.IdBodega = PickingUbic.IdBodega

                                                                                clsLnTrans_log_pedido_liberacion.Insertar(Betrans_log_pedido_liberacion, lConnection, lTransaction)
                                                                                vIdLogLiberacion += 1

                                                                            Else
                                                                                Throw New Exception("#CKFK20230516 No se eliminaron lo registros del PickingUbic")
                                                                            End If

                                                                        End If

                                                                    End If

                                                                End If

                                                            Else

                                                                '#CKFK20230514 Solo se libera el stock cuando no es pedido 
                                                                If (pOpcion <> clsDataContractDI.tOpcionLiberaStock.Pedido) Then

                                                                    lBeStockResByPickingUbic = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                                                                    IdPedidoEnc,
                                                                                                                                    PickingUbic.IdStock,
                                                                                                                                    PickingUbic.IdProductoBodega,
                                                                                                                                    PickingUbic.IdPropietarioBodega,
                                                                                                                                    IdPickingEnc,
                                                                                                                                    lConnection,
                                                                                                                                    lTransaction)
                                                                    If Not lBeStockResByPickingUbic Is Nothing Then

                                                                        If lBeStockResByPickingUbic.Count > 0 Then

                                                                            '#EJC20220108_1053AM: La cantidad despachada es > 0'
                                                                            If (PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida) Then
                                                                                '#EJC20220107: El operador bajó todo el producto de la posición, pero hizo un despacho parcial
                                                                                'En este caso, el producto restante debe ser movido a la ubicación de picking

                                                                                objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega 'Nueva ubicación
                                                                                vActuarlizarUbicacion = True
                                                                            Else
                                                                                '#EJC20220107: El operador NO bajó todo el producto de la posición, pero hizo un despacho parcial
                                                                                'En este caso, el producto restante (No pickeado y no despachado) debe quedarse en la misma posición.
                                                                                objStockOrigen.IdUbicacion = PickingUbic.IdUbicacion
                                                                                vActuarlizarUbicacion = False
                                                                            End If

                                                                            vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                                                             IdPedidoEnc,
                                                                                                                                                                             PickingUbic.IdStock,
                                                                                                                                                                             PickingUbic.IdBodega,
                                                                                                                                                                             lConnection,
                                                                                                                                                                             lTransaction)

                                                                            If vResultEliminacionStock > 0 Then

                                                                                If vActuarlizarUbicacion Then

                                                                                    '#CKFK20220717 Me parece que hay que agregar esto para que funcione
                                                                                    vIdStock = PickingUbic.IdStock
                                                                                    objStockOrigen = New clsBeStock()
                                                                                    objStockOrigen = clsLnStock.GetSingle(vIdStock,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                                                                                    If Not objStockOrigen Is Nothing Then

                                                                                        objStockOrigen.IdUbicacion = vIdUbicacionPickingDefectoPorBodega
                                                                                        objStockOrigen.IdUbicacion_anterior = vIdUbicacionPickingTomarDe
                                                                                        objStockOrigen.Fec_mod = Now
                                                                                        clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen, lConnection, lTransaction)

                                                                                    Else
                                                                                        Throw New Exception("Error_20220701: No se pudo obtnener el IdStock asociado a la transacción.")
                                                                                    End If


                                                                                End If

                                                                            End If


                                                                        Else
                                                                            Debug.WriteLine("Error_202201081123: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " El count de la lista es 0")
                                                                        End If

                                                                    Else
                                                                        Debug.WriteLine("Error_202201081123A: No se obtuvo stock reservado para la línea de pickingubic: " & PickingUbic.IdPickingUbic & " la lista es nothig.")
                                                                    End If

                                                                Else

                                                                    Debug.Print("No se va a liberar stock")

                                                                End If

                                                            End If

                                                        Next

                                                    Else
                                                        Debug.WriteLine("La lista picking ubic está vacía para la línea de pickingdet: " & PickingDet.IdPickingDet)
                                                    End If

                                                Next

                                            Else
                                                'GT07042022: se muestra como aviso, porque la lista puede estar vacia por no existir, o porque ya fue liberada en un primer intento
                                                'Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")
                                                Throw New Exception("AVISO_20220108_1027: No hay lineas de picking a liberar, la lista esta vacia.")
                                            End If

                                        Else
                                            Throw New Exception("Error_202212061120: El picking det por detalle de pedido es nothing.")
                                        End If

                                    Else

                                        Throw New Exception("Error_20220108_1027: No se encontraron las líneas de picking asociadas al pedido, la lista está vacía.")

                                    End If

                                Else
                                    Throw New Exception("ERROR_202212061124: La lista de pickiubic por pickingdet es nothing.")
                                End If

                            Else
                                Throw New Exception("Error_202212061119: No se obtuvo detalle de picking_det.")

                            End If

                        Else
                            Throw New Exception("ERROR_202211022303: No se pudo obtener el picking: " & IdPickingEnc)
                        End If

                    End If

                End If

            End If

            Liberar_Producto_No_Pickeado = True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Liberar_Producto_No_Pickeado_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer,
                                                                       ByVal IdPickingEnc As Integer,
                                                                       ByVal IdUsuario As Integer,
                                                                       ByVal Referencia As String,
                                                                       ByVal Observacion As String,
                                                                       ByVal IdBodega As Integer) As Boolean
        Liberar_Producto_No_Pickeado_By_IdPedidoEnc = False

        Try
            Dim vIdLogLiberacion As Integer = 0
            Dim vIdUbicacionDefecto As Integer
            Dim vResultEliminacionStock As Integer
            Dim vResultadoEliminacionPickingUbic As Integer
            Dim vResultadoEliminacionPickingDet As Integer

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vIdLogLiberacion = clsLnTrans_log_pedido_liberacion.MaxID(lConnection, lTransaction) + 1

                    vIdUbicacionDefecto = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(IdBodega, lConnection, lTransaction)
                    If vIdUbicacionDefecto = 0 Then
                        Throw New Exception("No está definida la posición de picking por defecto en la bodega, para liberar el producto a esa posición")
                    End If

                    Dim lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(IdPedidoEnc, lConnection, lTransaction)
                    If lStockReservado Is Nothing OrElse lStockReservado.Count = 0 Then Return False

                    Dim lPedidoDet = lStockReservado.Select(Function(x) x.IdPedidoDet).Distinct().ToList()
                    Dim BePickingEnc = clsLnTrans_picking_enc.GetSingle(IdPickingEnc, lConnection, lTransaction)
                    If BePickingEnc Is Nothing Then Throw New Exception("ERROR_202211022303: No se pudo obtener el picking: " & IdPickingEnc)

                    For Each IdPedidoDet In lPedidoDet

                        Dim lTransPickingDet = BePickingEnc.ListaPickingDet?.Where(Function(x) x.IdPedidoDet = IdPedidoDet).ToList()
                        Dim ListaDetalleUbicacion = BePickingEnc.ListaPickingUbic?.Where(Function(x) x.IdPedidoDet = IdPedidoDet AndAlso x.IdPedidoEnc = IdPedidoEnc).ToList()

                        If lTransPickingDet Is Nothing OrElse lTransPickingDet.Count = 0 Then
                            Throw New Exception("AVISO_20220108_1027: No hay lineas de picking a liberar, la lista esta vacia.")
                        End If

                        If ListaDetalleUbicacion Is Nothing OrElse ListaDetalleUbicacion.Count = 0 Then
                            Throw New Exception("ERROR_202212061124: La lista de pickiubic por pickingdet es nothing.")
                        End If

                        For Each PickingDet In lTransPickingDet
                            For Each PickingUbic In ListaDetalleUbicacion
                                If PickingUbic Is Nothing Then Continue For

                                Dim esCantidadCero = (PickingUbic.Cantidad_despachada = 0)

                                Dim lStockRes = clsLnStock_res.Get_All_By_Params(IdPedidoDet,
                                                                                 IdPedidoEnc,
                                                                                 PickingUbic.IdStock,
                                                                                 PickingUbic.IdProductoBodega,
                                                                                 PickingUbic.IdPropietarioBodega,
                                                                                 IdPickingEnc,
                                                                                 lConnection,
                                                                                 lTransaction)

                                If lStockRes Is Nothing OrElse lStockRes.Count = 0 Then
                                    Debug.WriteLine($"Error: No se obtuvo stock reservado para PickingUbic: {PickingUbic.IdPickingUbic}")
                                    Continue For
                                End If

                                If esCantidadCero Then

                                    vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_BePickingUbic_And_IdStock(PickingUbic,
                                                                                                                                   IdPedidoEnc,
                                                                                                                                   PickingUbic.IdStock,
                                                                                                                                   PickingUbic.IdBodega,
                                                                                                                                   lConnection, lTransaction)

                                    If vResultEliminacionStock > 0 Then

                                        vResultadoEliminacionPickingUbic = clsLnTrans_picking_ubic.Eliminar_By_BePickingUbic(PickingUbic, lConnection, lTransaction)

                                        If vResultadoEliminacionPickingUbic > 0 Then
                                            Try
                                                vResultadoEliminacionPickingDet = Eliminar_By_BePickingUbic(PickingUbic, lConnection, lTransaction)
                                            Catch ex As Exception
                                                Dim msg = $"{MethodBase.GetCurrentMethod.Name()} Error_20220108_1148: Al eliminar PickingDet: {PickingUbic.IdPickingDet}"
                                                clsLnLog_error_wms.Agregar_Error(msg)
                                            End Try

                                            Dim log As New clsBeTrans_log_pedido_liberacion With {
                                            .IdLogLiberacionStock = vIdLogLiberacion,
                                            .IdUsuario = IdUsuario,
                                            .Fecha = Now,
                                            .IdPedidoEnc = IdPedidoEnc,
                                            .IdPedidoDet = IdPedidoDet,
                                            .Referencia = Referencia,
                                            .Codigo_Producto = PickingDet.Codigo,
                                            .Lote = PickingUbic.Lote,
                                            .Lic_Plate = PickingUbic.Lic_plate,
                                            .Fecha_Vence = PickingUbic.Fecha_Vence,
                                            .Observacion = "Se liberó producto del IdStock: " & PickingUbic.IdStock,
                                            .Cantidad = PickingUbic.Cantidad_Solicitada,
                                            .IdPickingUbic = PickingUbic.IdPickingUbic,
                                            .IdPickingDet = PickingDet.IdPickingDet,
                                            .IdProductoBodega = PickingUbic.IdProductoBodega,
                                            .IdProductoEstado = PickingUbic.IdPickingDet,
                                            .IdPropietarioBodega = PickingUbic.IdPropietarioBodega,
                                            .IdUnidadMedida = PickingUbic.IdUnidadMedida,
                                            .IdPresentacion = PickingUbic.IdPresentacion,
                                            .IdUbicacion = PickingUbic.IdUbicacion,
                                            .IdStock = PickingUbic.IdStock,
                                            .IdBodega = PickingUbic.IdBodega
                                            }

                                            clsLnTrans_log_pedido_liberacion.Insertar(log, lConnection, lTransaction)
                                            vIdLogLiberacion += 1
                                        Else
                                            Throw New Exception("#CKFK20230516 No se eliminaron lo registros del PickingUbic")
                                        End If
                                    End If
                                Else
                                    If PickingUbic.Cantidad_Solicitada = PickingUbic.Cantidad_Recibida Then
                                        ' Producto fue totalmente bajado, pero despacho parcial → mover a ubicación por defecto
                                        Dim objStockOrigen = clsLnStock.GetSingle(PickingUbic.IdStock, lConnection, lTransaction)
                                        If objStockOrigen Is Nothing Then Throw New Exception("Error_20220701: No se pudo obtener el IdStock asociado a la transacción.")

                                        objStockOrigen.IdUbicacion = vIdUbicacionDefecto
                                        objStockOrigen.IdUbicacion_anterior = vIdUbicacionDefecto
                                        objStockOrigen.Fec_mod = Now
                                        clsLnStock.Actualiza_Ubicacion_Por_Picking(objStockOrigen, lConnection, lTransaction)
                                    End If

                                    vResultEliminacionStock = clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_And_IdStock(IdPedidoDet,
                                                                                                                                 IdPedidoEnc,
                                                                                                                                 PickingUbic.IdStock,
                                                                                                                                 PickingUbic.IdBodega,
                                                                                                                                 lConnection,
                                                                                                                                 lTransaction)
                                End If
                            Next
                        Next
                    Next

                    lTransaction.Commit()
                End Using
                lConnection.Close()
            End Using

            Liberar_Producto_No_Pickeado_By_IdPedidoEnc = True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim msg = $"{MethodBase.GetCurrentMethod.Name()} {ex.Message}"
            clsLnLog_error_wms.Agregar_Error(msg)
            Throw ex
        End Try
    End Function

End Class