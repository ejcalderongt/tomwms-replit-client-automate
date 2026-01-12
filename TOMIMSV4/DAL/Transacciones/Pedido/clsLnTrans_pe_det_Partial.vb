Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_pe_det

    Private Shared lProductosInMemory As New List(Of clsBeProducto)
    Private Shared lPresentaciones As New List(Of clsBeProducto_Presentacion)
    Private Shared lBeConfigInMemory As New List(Of clsBeI_nav_config_enc)

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vSQL As String = ""
        Dim vIdxProducto As Integer = -1

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vSQL = "SELECT b.nombre AS Bodega, c.nombre_comercial AS Cliente, p.nombre_comercial AS Propietario, enc.fecha_pedido AS 'Fecha Pedido', det.*, pb.IdProducto " &
                       " FROM trans_pe_det AS det " &
                       " INNER JOIN trans_pe_enc AS enc ON det.IdPedidoEnc = enc.IdPedidoEnc " &
                       " INNER JOIN propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega " &
                       " INNER JOIN propietarios AS p ON prb.IdPropietario = p.IdPropietario " &
                       " INNER JOIN bodega AS b ON enc.IdBodega = b.IdBodega " &
                       " INNER JOIN cliente AS c ON enc.IdCliente = c.IdCliente AND prb.IdPropietario = c.IdPropietario " &
                       " INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega " &
                       " WHERE det.IdPedidoEnc={0}"

                    vSQL = String.Format(vSQL, pIdPedidoEnc)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_pe_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_pe_det

                                With Obj

                                    'Obj.Bodega = IIf(IsDBNull(lRow.Item("Bodega")), String.Empty, lRow.Item("Bodega"))
                                    'Obj.Cliente = IIf(IsDBNull(lRow.Item("Cliente")), String.Empty, lRow.Item("Cliente"))
                                    'Obj.Propietario = IIf(IsDBNull(lRow.Item("Propietario")), String.Empty, lRow.Item("Propietario"))
                                    'Obj.FechaPedido = IIf(IsDBNull(lRow.Item("Fecha Pedido")), Date.Now, lRow.Item("Fecha Pedido"))

                                    .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                                    .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                                    .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                                    .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                                    vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = .Producto.IdProducto)

                                    If vIdxProducto = -1 Then

                                        .Producto = New clsBeProducto()
                                        .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))
                                        clsLnProducto.Obtener(.Producto, lConnection, lTransaction)
                                        lProductosInMemory.Add(.Producto.Clone())

                                    Else
                                        .Producto = lProductosInMemory(vIdxProducto).Clone()
                                    End If


                                    .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                                    .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                                    .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                                    .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                                    .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                                    .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                                    .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                                    .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                                    .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                                    .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                                    .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                                    .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                                    .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                                    .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                                    .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                                    .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                                    .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                                    .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                                    .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                                    .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                                    .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                                    .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                                    .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                                    .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

                                End With

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

    Public Shared Function Get_Detalle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vIdxProducto As Integer = 0

        Try

            Dim vSQL As String = " SELECT det.*, pb.IdProducto 
                                   FROM trans_pe_det det
                                    INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                    WHERE det.IdPedidoEnc= @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransPeDet As clsBeTrans_pe_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransPeDet = New clsBeTrans_pe_det

                        With BeTransPeDet

                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))

                            .Producto = New clsBeProducto()
                            .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                            Dim vIdProducto As Integer = .Producto.IdProducto

                            vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = vIdProducto)

                            If vIdxProducto = -1 Then

                                Dim pCampos(6) As clsBeProducto.ProdPropiedades
                                pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                                pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                                pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                                pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                                pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                                pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                                .Producto = clsLnProducto.GetSingle(.Producto.IdProducto, pCampos, lConnection, lTransaction)
                                lProductosInMemory.Add(.Producto.Clone())

                            Else
                                .Producto = lProductosInMemory(vIdxProducto).Clone()
                            End If

                            '#EJC20190214_0116PM: Comentariado por rendimiento, obtener solo lo necesario..
                            'clsLnProducto.Obtener(.Producto, lConnection, lTransaction)

                            .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                            .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                            .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                            .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                            .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                            .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                            .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                            .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                            .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                            .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                            .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                            .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                            .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                            .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                            .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

                            '#EJC20180114: Agruegué No_Linea y Atributo_Variante_1 en GetByPedidoEnc
                            .No_linea = IIf(IsDBNull(lRow.Item("No_linea")), 0.0, lRow.Item("No_linea"))
                            .Atributo_Variante_1 = IIf(IsDBNull(lRow.Item("Atributo_Variante_1")), 0.0, lRow.Item("Atributo_Variante_1"))
                            '#CM_17092019_453PM: Agruegué IdStockEspecifico en GetByPedidoEnc
                            .IdStockEspecifico = IIf(IsDBNull(lRow.Item("IdStockEspecifico")), 0, lRow.Item("IdStockEspecifico"))
                            .EsPadre = IIf(IsDBNull(lRow.Item("EsPadre")), False, lRow.Item("EsPadre"))
                            .IdPedidoDetPadre = IIf(IsDBNull(lRow.Item("IdPedidoDetPadre")), 0, lRow.Item("IdPedidoDetPadre"))
                            .IdCliente = IIf(IsDBNull(lRow.Item("IdCliente")), 0, lRow.Item("IdCliente"))
                            .Talla = IIf(IsDBNull(lRow.Item("talla")), "", lRow.Item("talla"))
                            .Color = IIf(IsDBNull(lRow.Item("color")), "", lRow.Item("color"))
                            .IdProductoTallaColor = IIf(IsDBNull(lRow.Item("IdProductoTallaColor")), 0, lRow.Item("IdProductoTallaColor"))

                        End With

                        lReturnList.Add(BeTransPeDet)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_By_IdPedidoEnc_HH(ByVal pIdPedidoEnc As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vIdxProducto As Integer = 0

        Try

            'Dim vSQL As String = " SELECT det.*, pb.IdProducto FROM trans_pe_det det
            '                        INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
            '                        WHERE det.IdPedidoEnc= @IdPedidoEnc "

            Dim vSQL As String = " SELECT * FROM VW_Get_Pedido_Det
                                    WHERE IdPedidoEnc= @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_pe_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_pe_det

                        With Obj

                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))

                            .Producto = New clsBeProducto()
                            .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                            'Dim vIdProducto As Integer = .Producto.IdProducto

                            'vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = vIdProducto)

                            'If vIdxProducto = -1 Then

                            '    Dim pCampos(6) As clsBeProducto.ProdPropiedades
                            '    pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                            '    pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                            '    pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                            '    pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                            '    pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                            '    pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                            '    .Producto = clsLnProducto.GetSingle(.Producto.IdProducto, pCampos, lConnection, lTransaction)
                            '    lProductosInMemory.Add(.Producto.Clone())

                            'Else
                            '    .Producto = lProductosInMemory(vIdxProducto).Clone()
                            'End If

                            '#EJC20190214_0116PM: Comentariado por rendimiento, obtener solo lo necesario..
                            'clsLnProducto.Obtener(.Producto, lConnection, lTransaction)

                            .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                            .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                            .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                            .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                            .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                            .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                            .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                            .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                            .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                            .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                            .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                            .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                            .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                            .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                            .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

                            '#EJC20180114: Agruegué No_Linea y Atributo_Variante_1 en GetByPedidoEnc
                            .No_linea = IIf(IsDBNull(lRow.Item("No_linea")), 0.0, lRow.Item("No_linea"))
                            .Atributo_Variante_1 = IIf(IsDBNull(lRow.Item("Atributo_Variante_1")), 0.0, lRow.Item("Atributo_Variante_1"))
                            '#CM_17092019_453PM: Agruegué IdStockEspecifico en GetByPedidoEnc
                            .IdStockEspecifico = IIf(IsDBNull(lRow.Item("IdStockEspecifico")), 0, lRow.Item("IdStockEspecifico"))
                            .EsPadre = IIf(IsDBNull(lRow.Item("EsPadre")), False, lRow.Item("EsPadre"))
                            .IdPedidoDetPadre = IIf(IsDBNull(lRow.Item("IdPedidoDetPadre")), 0, lRow.Item("IdPedidoDetPadre"))
                            .IdCliente = IIf(IsDBNull(lRow.Item("IdCliente")), 0, lRow.Item("IdCliente"))

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

    Public Shared Function Get_Detalle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                      ByVal pEstadoPedido As String,
                                                      ByRef pIdDespachoEnc As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vIdxProducto As Integer = -1
        Dim lSubListaProductosInMemory As New List(Of clsBeProducto)
        Dim vlSubListaProductosInMemory As New List(Of clsBeProducto)

        Try

            Dim vSQL As String = " SELECT det.*, pb.IdProducto FROM trans_pe_det det
                                    INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                    WHERE det.IdPedidoEnc= @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BePedidoDet As clsBeTrans_pe_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lSubListaProductosInMemory = clsLnProducto.Get_All_By_IdPedidoEnc(pIdPedidoEnc)

                    If Not lSubListaProductosInMemory Is Nothing Then

                        If Not lProductosInMemory Is Nothing Then
                            If lProductosInMemory.Count > 0 Then
                                vlSubListaProductosInMemory = lSubListaProductosInMemory.Except(lProductosInMemory).ToList()
                            End If
                        End If

                        If Not vlSubListaProductosInMemory Is Nothing Then
                            lProductosInMemory.AddRange(vlSubListaProductosInMemory)
                        End If

                    End If

                    For Each lRow As DataRow In lDataTable.Rows

                        BePedidoDet = New clsBeTrans_pe_det

                        With BePedidoDet

                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                            Dim vIdProducto As Integer = .Producto.IdProducto
                            vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = vIdProducto)

                            If vIdxProducto = -1 Then

                                Dim BeProducto = New clsBeProducto()
                                Dim pCampos(6) As clsBeProducto.ProdPropiedades
                                pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                                pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                                pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                                pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                                pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                                pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                                BeProducto.IdProducto = vIdProducto
                                BeProducto = clsLnProducto.GetSingle(vIdProducto,
                                                                     pCampos,
                                                                     lConnection,
                                                                     lTransaction)
                                lProductosInMemory.Add(BeProducto.Clone())
                                .Producto = BeProducto

                            Else
                                Dim BeProducto = New clsBeProducto()
                                BeProducto = lProductosInMemory(vIdxProducto).Clone()
                                .Producto = BeProducto
                            End If

                            .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                            .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                            .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                            .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                            .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                            .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                            .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                            .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                            .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                            .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                            .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                            .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                            .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                            .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                            .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

                            '#EJC20180114: Agruegué No_Linea y Atributo_Variante_1 en GetByPedidoEnc
                            .No_linea = IIf(IsDBNull(lRow.Item("No_linea")), 0.0, lRow.Item("No_linea"))
                            .Atributo_Variante_1 = IIf(IsDBNull(lRow.Item("Atributo_Variante_1")), 0.0, lRow.Item("Atributo_Variante_1"))
                            .IdStockEspecifico = IIf(IsDBNull(lRow.Item("IdStockEspecifico")), 0, lRow.Item("IdStockEspecifico"))
                            .Talla = IIf(IsDBNull(lRow.Item("Talla")), "", lRow.Item("Talla"))
                            .Color = IIf(IsDBNull(lRow.Item("Color")), "", lRow.Item("Color"))
                            .IdProductoTallaColor = IIf(IsDBNull(lRow.Item("IdProductoTallaColor")), 0, lRow.Item("IdProductoTallaColor"))

                            If pEstadoPedido = "Despachado" Then

                                Dim lSubListaPickingUbic As New List(Of clsBeTrans_picking_ubic)

                                lSubListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_Despachado_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)
                                If Not lSubListaPickingUbic Is Nothing Then

                                    .ListaPickingUbic = lSubListaPickingUbic.FindAll(Function(x) x.IdPedidoEnc = pIdPedidoEnc AndAlso x.IdPedidoDet = .IdPedidoDet)

                                End If

                            End If

                        End With

                        lReturnList.Add(BePedidoDet)

                        Application.DoEvents()

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vSQL As String = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vSQL = "SELECT * FROM trans_pe_det WHERE IdPedidoEnc=@IdPedidoEnc "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_pe_det

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_pe_det
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

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

    Public Shared Function Get_All_Stock_Res_By_IdPedidoDet(ByVal pIdPedidoDet As Integer, ByVal IdPedidoEnc As Integer) As List(Of clsBeStock_res)

        Dim lReturnList As New List(Of clsBeStock_res)

        Try

            Dim vSQL As String = "SELECT * FROM stock_res WHERE IdPedidoDet = @IdPedidoDet AND IdPedido = @IdPedidoEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeStock_res
                                clsLnStock_res.Cargar(Obj, lRow)
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

    Public Shared Function Get_Referencias_By_IdPedidoDet(ByVal pIdPedidoDet As Integer) As String

        Try

            Dim Referencia As String = ""

            Dim vSQL As String = String.Format("Select referencia 
                                                From trans_pe_enc inner join 
                                                trans_pe_det on trans_pe_enc.IdPedidoEnc = trans_pe_det.IdPedidoEnc
                                                Where trans_pe_det.IdPedidoDet={0}", pIdPedidoDet)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Referencia = lReturnValue
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Referencia

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Referencias_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As String

        Try

            Dim Referencia As String = ""

            Dim vSQL As String = String.Format("Select referencia 
                                                From trans_pe_enc inner join 
                                                trans_pe_det on trans_pe_enc.IdPedidoEnc = trans_pe_det.IdPedidoEnc
                                                WHERE trans_pe_det.IdPedidoDet={0}", pIdPedidoDet)

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Referencia = lReturnValue
                End If

            End Using

            Return Referencia

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdPedidoEnc_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim IdPedidoEnc As Integer = 0

            Dim sp As String = String.Format("SELECT IdPedidoEnc FROM trans_pe_det WHERE IdPedidoDet={0}", pIdPedidoDet)

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    IdPedidoEnc = CInt(lReturnValue)
                End If

            End Using

            Return IdPedidoEnc

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Res_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                            ByVal PendientesDeDespacho As Boolean,
                                                            ByVal EsPickingNuevo As Boolean,
                                                            ByVal EsPicking As Boolean) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim vSQL As String = ""

        Try

            vSQL = "SELECT p.codigo, p.nombre, pp.nombre AS presentacion, 
                    pe.nombre AS NomEstado, 
                    um.Nombre AS unidadmedida, 
                    pr.nombre_comercial AS propietario, 
                    bu.descripcion AS bodegaubicacion, 
                    ISNULL(s.cantidad,0) AS CantidadSF, 
                    pp.factor, ISNULL(ISNULL(s.cantidad,0) / pp.factor,0) AS Cantidad, 
                    res.IdStockRes, 
                    res.IdTransaccion, 
                    res.Indicador, 
                    res.IdPedidoDet, 
                    res.IdStock, 
                    res.IdPropietarioBodega, 
                    res.IdProductoBodega, 
                    res.IdUbicacion, 
                    res.IdProductoEstado, 
                    res.IdPresentacion, 
                    res.IdUnidadMedida, 
                    res.lote, 
                    res.lic_plate, 
                    res.serial, "

            If EsPickingNuevo Then
                vSQL += "Res.Cantidad As CantidadReservada, "
            Else
                vSQL += "SUM(ISNULL(trans_picking_ubic.cantidad_solicitada, 0)) As CantidadReservada, "
            End If

            vSQL += " res.peso, 
                    res.estado, 
                    res.fecha_vence, 
                    res.uds_lic_plate, 
                    res.ubicacion_ant AS IdUbicacion_anterior, 
                    res.no_bulto, 
                    res.IdRecepcion AS IdRecepcionEnc, 
                    res.IdPicking, 
                    res.IdPedido, 
                    res.IdDespacho, 
                    res.añada, 
                    res.fecha_manufactura, 
                    SUM(ISNULL(dbo.trans_picking_ubic.peso_recibido, 0)) AS Peso_Recibido, 
                    SUM(ISNULL(dbo.trans_picking_ubic.peso_verificado, 0)) AS Peso_Verificado, 
                    ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0)) AS cantidad_recibida, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0)) AS cantidad_verificada, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_despachada, 
                    0)) AS Cantidad_Despachada, 
                    ISNULL(dbo.trans_picking_ubic.encontrado, 0) AS Encontrado, 
                    dbo.Nombre_Completo_Ubicacion(res.IdUbicacion, res.IDBODEGA) AS NomUbic,
                    s.IdRecepcionEnc, s.IdRecepcionDet,
                    col.Codigo AS Color,
                    tal.Codigo AS Talla,
                    ptc.IdProductoTallaColor
                    FROM  stock_res AS res INNER JOIN
                    propietario_bodega AS prb ON res.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                    producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN
                    producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado INNER JOIN
                    unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
                    propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                    producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                    bodega_ubicacion AS bu RIGHT OUTER JOIN
                    trans_picking_det INNER JOIN
                    trans_picking_ubic ON trans_picking_det.IdPickingDet = trans_picking_ubic.IdPickingDet ON bu.IdBodega = trans_picking_ubic.IdBodega AND bu.IdUbicacion = trans_picking_ubic.IdUbicacion ON 
                    res.IDBODEGA = trans_picking_ubic.IdBodega AND res.IdPedidoDet = trans_picking_det.IdPedidoDet AND res.IdStock = trans_picking_ubic.IdStock AND 
                    res.IdStockRes = trans_picking_ubic.IdStockRes LEFT OUTER JOIN
                    stock AS s ON res.IdStock = s.IdStock LEFT OUTER JOIN
                    producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
                    LEFT JOIN producto_talla_color AS ptc
                        ON ptc.IdProductoTallaColor = res.IdProductoTallaColor   
                    LEFT JOIN color AS col
                        ON col.IdColor = ptc.IdColor
                    LEFT JOIN talla AS tal
                        ON tal.IdTalla = ptc.IdTalla
                    WHERE(Res.IdPedido = @IdPedido) AND (ISNULL(trans_picking_ubic.dañado_verificacion, 0) = 0) AND (ISNULL(trans_picking_ubic.dañado_picking, 0) = 0) AND (ISNULL(trans_picking_ubic.no_encontrado, 0) = 0) "

            If PendientesDeDespacho Then
                vSQL += " AND (trans_picking_ubic.cantidad_despachada < trans_picking_ubic.cantidad_verificada) "
            End If

            If Not EsPicking Then
                vSQL += " AND (trans_picking_ubic.cantidad_verificada > 0) "
            End If

            vSQL += "GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial, bu.descripcion, s.cantidad, pp.factor, s.cantidad / pp.factor, res.IdStockRes, 
                        Res.IdTransaccion, Res.Indicador, Res.IdPedidoDet, Res.IdStock, Res.IdPropietarioBodega, Res.IdProductoBodega, Res.IdUbicacion, Res.IdProductoEstado, 
                        res.IdPresentacion, res.IdUnidadMedida, res.lote, res.lic_plate, res.serial, res.cantidad, res.peso, res.estado, res.fecha_vence, res.uds_lic_plate, 
                        Res.ubicacion_ant, Res.no_bulto, Res.IdRecepcion, Res.IdPicking, Res.IdPedido, Res.IdDespacho,
                        res.añada, res.fecha_manufactura,
                        ISNULL(trans_picking_ubic.acepto, 0), 
                        ISNULL(trans_picking_ubic.encontrado, 0),bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion, res.IdBodega, s.IdRecepcionEnc, s.IdRecepcionDet,col.Codigo, tal.Codigo,ptc.IdProductoTallaColor "

            vSQL += " ORDER BY bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedido", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                '#CKFK 20180419 09:59 PM Agregué el obtener de la ubicación para poder tener el nombre completo en GetAllStockResByPedido
                                'Obj.UbicacionActual.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                'clsLnBodega_ubicacion.Get_For_Picking(Obj.UbicacionActual, lConnection, lTransaction)
                                'Obj.Ubicacion_Nombre = Obj.UbicacionActual.NombreCompleto() '#CKFK 20180603 Agregué esta línea para que se cargara el nombre de la ubicación
                                Obj.Ubicacion_Nombre = IIf(IsDBNull(lRow("NomUbic")), "", lRow("NomUbic"))
                                Obj.UbicacionActual.NombreCompleto = Obj.Ubicacion_Nombre

                                If lRow("encontrado") IsNot DBNull.Value AndAlso lRow("encontrado") IsNot Nothing Then
                                    Obj.encontrado = CType(lRow("encontrado"), Boolean)
                                End If

                                If lRow("acepto") IsNot DBNull.Value AndAlso lRow("acepto") IsNot Nothing Then
                                    Obj.acepto = CType(lRow("acepto"), Boolean)
                                End If

                                If lRow("peso_recibido") IsNot DBNull.Value AndAlso lRow("peso_recibido") IsNot Nothing Then
                                    Obj.peso_pickeado = CType(lRow("peso_recibido"), Double)
                                End If

                                If lRow("peso_verificado") IsNot DBNull.Value AndAlso lRow("peso_verificado") IsNot Nothing Then
                                    Obj.peso_verificado = CType(lRow("peso_verificado"), Double)
                                End If

                                If lRow("cantidad_recibida") IsNot DBNull.Value AndAlso lRow("cantidad_recibida") IsNot Nothing Then
                                    Obj.Cantidad_Pickeada = CType(lRow("cantidad_recibida"), Double)
                                End If

                                If lRow("cantidad_verificada") IsNot DBNull.Value AndAlso lRow("cantidad_verificada") IsNot Nothing Then
                                    Obj.Cantidad_Verificada = CType(lRow("cantidad_verificada"), Double)
                                End If

                                If lRow("cantidad_despachada") IsNot DBNull.Value AndAlso lRow("cantidad_despachada") IsNot Nothing Then
                                    Obj.Cantidad_Despachada = CType(lRow("cantidad_despachada"), Double)
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

    ''' <summary>
    ''' #EJC202508111059: Este query se debe unificar.
    ''' </summary>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <param name="PendientesDeDespacho"></param>
    ''' <param name="EsPickingNuevo"></param>
    ''' <param name="EsPicking"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Stock_Res_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                            ByVal PendientesDeDespacho As Boolean,
                                                            ByVal EsPickingNuevo As Boolean,
                                                            ByVal EsPicking As Boolean,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim vSQL As String = ""

        Try


            vSQL = "SELECT p.codigo, p.nombre, pp.nombre AS presentacion, pe.nombre AS NomEstado, um.Nombre AS unidadmedida, pr.nombre_comercial AS propietario,
                        bu.descripcion As bodegaubicacion, s.cantidad As CantidadSF, pp.factor, s.cantidad / pp.factor As Cantidad, Res.IdStockRes, Res.IdTransaccion, Res.Indicador, 
                        res.IdPedidoDet, res.IdStock, res.IdPropietarioBodega, res.IdProductoBodega, res.IdUbicacion, res.IdProductoEstado, res.IdPresentacion, res.IdUnidadMedida,
                        Res.lote, Res.lic_plate, Res.serial, s.IdRecepcionEnc, s.IdRecepcionDet, "

            If EsPickingNuevo Then
                vSQL += "Res.Cantidad As CantidadReservada, "
            Else
                vSQL += "SUM(ISNULL(trans_picking_ubic.cantidad_solicitada, 0)) As CantidadReservada, "
            End If

            vSQL += " Res.peso, Res.estado, Res.fecha_vence, Res.uds_lic_plate, 
                        res.ubicacion_ant as IdUbicacion_anterior, res.no_bulto, res.IdRecepcion as IdRecepcionEnc, 
                        res.IdPicking, res.IdPedido, res.IdDespacho, 
                        Res.añada, Res.fecha_manufactura, 
                        SUM(ISNULL(trans_picking_ubic.peso_recibido, 0)) As Peso_Recibido, 
                        SUM(ISNULL(trans_picking_ubic.peso_verificado, 0))AS Peso_Verificado, 
                        ISNULL(trans_picking_ubic.acepto, 0) AS Acepto, 
                        SUM(ISNULL(trans_picking_ubic.cantidad_recibida, 0)) AS cantidad_recibida,
                        SUM(ISNULL(trans_picking_ubic.cantidad_verificada, 0)) As cantidad_verificada,
                        SUM(ISNULL(trans_picking_ubic.cantidad_despachada, 0)) as Cantidad_Despachada, 
                        ISNULL(trans_picking_ubic.encontrado, 0) As Encontrado, trans_picking_ubic.IdProductoTallaColor
                        ,res.color as Codigo_Color,res.talla as Codigo_Talla
                        FROM trans_picking_det INNER JOIN 
                        trans_picking_ubic ON trans_picking_det.IdPickingDet = trans_picking_ubic.IdPickingDet RIGHT OUTER JOIN 
                        stock_res AS res INNER JOIN 
                        propietario_bodega AS prb ON res.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN 
                        producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN 
                        producto AS p ON pb.IdProducto = p.IdProducto And prb.IdPropietario = p.IdPropietario INNER JOIN 
                        bodega_ubicacion AS bu ON res.IdUbicacion = bu.IdUbicacion AND res.IdBodega = bu.IdBodega INNER JOIN 
                        producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado And prb.IdPropietario = pe.IdPropietario INNER JOIN 
                        unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida AND prb.IdPropietario = um.IdPropietario INNER JOIN 
                        stock AS s ON res.IdStock = s.IdStock INNER JOIN 
                        propietarios AS pr ON prb.IdPropietario = pr.IdPropietario ON trans_picking_det.IdPedidoDet = res.IdPedidoDet AND 
                        trans_picking_ubic.IdUbicacion = bu.IdUbicacion AND res.IdStock = trans_picking_ubic.IdStock 
                        and res.IdStockres = trans_picking_ubic.IdStockres LEFT OUTER JOIN 
                        producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto 
                        WHERE(Res.IdPedido = @IdPedido) AND (ISNULL(trans_picking_ubic.dañado_verificacion, 0) = 0) AND (ISNULL(trans_picking_ubic.dañado_picking, 0) = 0) "

            If PendientesDeDespacho Then
                vSQL += " AND (trans_picking_ubic.cantidad_despachada = 0) "
            End If

            If Not EsPicking Then
                vSQL += " AND (trans_picking_ubic.cantidad_verificada > 0) "
            End If

            vSQL += "GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial, bu.descripcion, s.cantidad, pp.factor, s.cantidad / pp.factor, res.IdStockRes, 
                        Res.IdTransaccion, Res.Indicador, Res.IdPedidoDet, Res.IdStock, Res.IdPropietarioBodega, Res.IdProductoBodega, Res.IdUbicacion, Res.IdProductoEstado, 
                        res.IdPresentacion, res.IdUnidadMedida, res.lote, res.lic_plate, res.serial, res.cantidad, res.peso, res.estado, res.fecha_vence, res.uds_lic_plate, 
                        Res.ubicacion_ant, Res.no_bulto, Res.IdRecepcion, Res.IdPicking, Res.IdPedido, Res.IdDespacho,
                        res.añada, res.fecha_manufactura,
                        ISNULL(trans_picking_ubic.acepto, 0), 
                        ISNULL(trans_picking_ubic.encontrado, 0),
                        bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion, 
                        s.IdRecepcionEnc, s.IdRecepcionDet,trans_picking_ubic.IdProductoTallaColor,
                        res.color ,res.talla "

            vSQL += " ORDER BY bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedido", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res

                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                        '#CKFK 20180419 09:59 PM Agregué el obtener de la ubicación para poder tener el nombre completo en GetAllStockResByPedido
                        Obj.UbicacionActual.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                        clsLnBodega_ubicacion.Get_For_Picking(Obj.UbicacionActual, lConnection, lTransaction)
                        Obj.Ubicacion_Nombre = Obj.UbicacionActual.NombreCompleto '#CKFK 20180603 Agregué esta línea para que se cargara el nombre de la ubicación

                        If lRow("encontrado") IsNot DBNull.Value AndAlso lRow("encontrado") IsNot Nothing Then
                            Obj.encontrado = CType(lRow("encontrado"), Boolean)
                        End If

                        If lRow("acepto") IsNot DBNull.Value AndAlso lRow("acepto") IsNot Nothing Then
                            Obj.acepto = CType(lRow("acepto"), Boolean)
                        End If

                        If lRow("peso_recibido") IsNot DBNull.Value AndAlso lRow("peso_recibido") IsNot Nothing Then
                            Obj.peso_pickeado = CType(lRow("peso_recibido"), Double)
                        End If

                        If lRow("peso_verificado") IsNot DBNull.Value AndAlso lRow("peso_verificado") IsNot Nothing Then
                            Obj.peso_verificado = CType(lRow("peso_verificado"), Double)
                        End If

                        If lRow("cantidad_recibida") IsNot DBNull.Value AndAlso lRow("cantidad_recibida") IsNot Nothing Then
                            Obj.Cantidad_Pickeada = CType(lRow("cantidad_recibida"), Double)
                        End If

                        If lRow("cantidad_verificada") IsNot DBNull.Value AndAlso lRow("cantidad_verificada") IsNot Nothing Then
                            Obj.Cantidad_Verificada = CType(lRow("cantidad_verificada"), Double)
                        End If

                        If lRow("cantidad_despachada") IsNot DBNull.Value AndAlso lRow("cantidad_despachada") IsNot Nothing Then
                            Obj.Cantidad_Despachada = CType(lRow("cantidad_despachada"), Double)
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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef ltransaction As SqlTransaction) As Integer

        Dim lMax As Integer = 0

        Try

            Dim vSQL As String = "SELECT ISNULL(Max(IdPedidoDet),0) FROM trans_pe_det"

            Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Eliminar_Detalle_By_IdPedidoDet(ByVal pIdPedidoEnc As Integer,
                                                           ByVal pIdPedidoDet As Integer,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_det" &
             "  Where(IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", pIdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", pIdPedidoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Reservar_Stock_Por_Linea(ByVal vDiasVencimientoCliente As Double,
                                                    ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                    ByRef pBeStockResSol As clsBeStock_res,
                                                    ByVal pIdPickingEnc As Integer,
                                                    ByVal MaquinaQueSolicita As String,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByVal pIdPropietario As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vIdxPresentacion As Integer = 0
        Reservar_Stock_Por_Linea = False

        Dim BeConfigEnc As New clsBeI_nav_config_enc With {.Idnavconfigenc = 1, .IdPropietario = pIdPropietario}

        Try

            Dim ResultadoInsert As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lStockReservo As New List(Of clsBeStock_res)
            Dim vCantidadDisponible As Double = 0

            Dim vIdPropietarioBodega As Integer = 0
            vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(pIdPropietario,
                                                                                                                 pIdBodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)
            Dim pEmpresa As clsBeEmpresa
            pEmpresa = New clsBeEmpresa()
            pEmpresa.IdEmpresa = pIdEmpresa
            pEmpresa = clsLnEmpresa.GetSingle(pEmpresa,
                                              lConnection,
                                              lTransaction)

            If Not pEmpresa.Operador_logistico Then
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(pIdBodega,
                                                                                            pIdPropietario,
                                                                                            lConnection,
                                                                                            lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgError As String = "No se obtuvo la configuración de interface Operador_logistico = true IdBodega: " & pIdBodega & " IdPropietario: " & pIdPropietario
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                End If

            Else
                BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega,
                                                                                         pIdEmpresa,
                                                                                         lConnection,
                                                                                         lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgError As String = "No se obtuvo la configuración de interface Operador_logistico = false IdEmpresa: " & pIdEmpresa & " IdPropietario: " & pIdPropietario
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                End If

            End If

            '#EJC202211091834: Parámetro?
            If BeConfigEnc Is Nothing Then
                Throw New Exception("ERROR_202211091833: No se pudo obtener la configuración de la interface.")
            Else
                'MsgBox("Parámetro conservar zona picking: " & BeConfigEnc.Conservar_Zona_Picking_Clavaud)
            End If

            If pBePedidoDet.IsNew Then

                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)

            Else
                '#EJC20171023_0222PM: No me gusta esta chapusería pero se agregó por cuando modifican una línea existente en el pedido.
                'Ver ref -> '#EJC20171024_1245PM_REF en forma de pedido
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else

                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection,
                                                                                       lTransaction)

                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction)

                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidad_Reservada_By_IdStock As Double = 0
                Dim vCantidadPendienteDeReservar As Double = 0
                Dim vDifPedidoVrsReservadoUMBas As Double = 0
                Dim BePres As New clsBeProducto_Presentacion
                Dim vDifTotalPedidoVrsReservado As Double = 0
                Dim vCantidadSolicitadaUMBas As Double = 0

                Dim vCantReservadaActual As Double = (From p In pBePedidoDet.ListaStockRes).Sum(Function(x) x.Cantidad)

                If pBePedidoDet.IdPresentacion <> 0 Then

                    Dim vIdPresentacion As Integer = pBePedidoDet.IdPresentacion

                    vIdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If vIdxPresentacion = -1 Then

                        BePres = New clsBeProducto_Presentacion
                        BePres.IdPresentacion = vIdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)
                        lPresentaciones.Add(BePres.Clone())

                    Else
                        BePres = New clsBeProducto_Presentacion
                        BePres = lPresentaciones(vIdxPresentacion).Clone()
                    End If

                    pBePedidoDet.Presentacion = BePres

                    If Not BePres Is Nothing Then
                        vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor
                    End If

                Else

                    vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad

                End If

                vDifTotalPedidoVrsReservado = vCantidadSolicitadaUMBas - vCantReservadaActual

                '#EJC202308281221B.
                'El pedido, puede venir en dos variantes:
                '1. Variante 1: El pedido viene en UMBas.
                '2. Variante 2: El pedido viene en Presentación.
                'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion > 0.

                '2. Variante 2 + Caso de uso #2
                If pBePedidoDet.IdPresentacion > 0 AndAlso pBeStockResSol.IdPresentacion > 0 Then
                    If Not BePres Is Nothing Then
                        pBeStockResSol.Cantidad = Math.Round(vDifTotalPedidoVrsReservado / BePres.Factor, 6)
                    End If
                End If

                Select Case vDifTotalPedidoVrsReservado

                        '#EJC20191114:
                        'Se aumentó la cantidad en el pedido, 
                        'Por lo tanto se debe aumentar la cantidad reservada 
                        'y la cantidad en picking (si tiene)
                        'Solo se reversa el excendente (pej. si tenía 4 reservadas y la cantidad cambió a 5, 
                        'Solo se reserva +1
                    Case Is > 0

                        If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockResSol,
                                                                 vDiasVencimientoCliente,
                                                                 MaquinaQueSolicita,
                                                                 BeConfigEnc,
                                                                 vCantidadDisponible,
                                                                 vIdPropietarioBodega,
                                                                 lStockReservo,
                                                                 lConnection,
                                                                 lTransaction) Then

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                If Actualiza_Picking_Existente(pBeStockResSol,
                                                               pBePedidoDet,
                                                               pIdPickingEnc,
                                                               lConnection,
                                                               lTransaction) Then

                                    Reservar_Stock_Por_Linea = True

                                End If

                            Else
                                Reservar_Stock_Por_Linea = True
                            End If

                        End If

                    Case Is < 0 'Se disminuyó la cantidad en el pedido

                        vDifTotalPedidoVrsReservado = Math.Abs(vDifTotalPedidoVrsReservado)

                        '#EJC20200125: Buscar si ya existe un solo IdStockRes que contenga la cantidad que se solicita...
                        Dim BeStockRes As clsBeStock_res = pBePedidoDet.ListaStockRes.Find(Function(X) X.Cantidad = vCantidadSolicitadaUMBas)

                        If Not BeStockRes Is Nothing Then
                            'Eliminar todos los idstock res diferentes del que tiene la cantidad que quiero.
                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_Excepto_IdStockRes(pBePedidoDet.IdPedidoDet, BeStockRes.IdStockRes, lConnection, lTransaction)
                            Reservar_Stock_Por_Linea = True
                        Else

                            For Each Sr In pBePedidoDet.ListaStockRes.OrderByDescending(Function(x) x.Cantidad)

                                If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                    Throw New Exception("Error de usuario en el proceso: No se puede reducir la cantidad de la línea de pedido porque ya fue asignada a un proceso de picking.")

                                    'For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                    '    Debug.Print(Pu.IdPickingUbic)
                                    'Next

                                Else

                                    vCantidad_Reservada_By_IdStock = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(Sr.IdStock,
                                                                                                                       False,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)


                                    If vDifTotalPedidoVrsReservado > 0 Then

                                        '#EJC20191114:
                                        'Si la cantidad a restar es menor que la cantidad reservada
                                        'en un IdStockRes (Ej: reservado es 5 y la cantidad se cambió a 4)
                                        'Se debe restar 1 de ese IdStockRes.
                                        If vDifTotalPedidoVrsReservado < Sr.Cantidad Then
                                            'vDifTotalPedidoVrsReservado = Sr.Cantidad - vDifTotalPedidoVrsReservado
                                            Sr.Cantidad -= Math.Abs(vDifTotalPedidoVrsReservado)
                                            clsLnStock_res.Actualizar(Sr, lConnection, lTransaction)
                                        ElseIf vDifTotalPedidoVrsReservado = Sr.Cantidad Then
                                            vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                        ElseIf vDifTotalPedidoVrsReservado > Sr.Cantidad Then
                                            vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                        End If

                                    End If

                                End If

                            Next

                        End If

                    Case Else
                        '#EJC20230912: La cantidad no varió la reserva se mantiene sin modificación.
                        Reservar_Stock_Por_Linea = True
                        Exit Select

                End Select

            Else

                If pBePedidoDet.IdPresentacion <> 0 Then

                    Dim vIdPresentacion As Integer = pBePedidoDet.IdPresentacion

                    vIdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If vIdxPresentacion = -1 Then

                        pBePedidoDet.Presentacion = New clsBeProducto_Presentacion
                        pBePedidoDet.Presentacion.IdPresentacion = vIdPresentacion
                        clsLnProducto_presentacion.GetSingle(pBePedidoDet.Presentacion, lConnection, lTransaction)
                        lPresentaciones.Add(pBePedidoDet.Presentacion.Clone())

                    Else
                        pBePedidoDet.Presentacion = New clsBeProducto_Presentacion
                        pBePedidoDet.Presentacion = lPresentaciones(vIdxPresentacion).Clone()
                    End If

                End If

                If clsLnStock_res.Tiene_StockRes(pBeStockResSol.IdPedido,
                                                 pBeStockResSol.IdPedidoDet,
                                                 lConnection,
                                                 lTransaction) Then

                    If Not clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(pBeStockResSol.IdPedido,
                                                                                                  pBeStockResSol.IdPedidoDet,
                                                                                                  lConnection,
                                                                                                  lTransaction) Then

                        Throw New Exception("No se pudo eliminar el stock reservado antes de reservar de nuevo el stock")

                    End If

                End If

                If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockResSol,
                                                         vDiasVencimientoCliente,
                                                         MaquinaQueSolicita,
                                                         BeConfigEnc,
                                                         vCantidadDisponible,
                                                         vIdPropietarioBodega,
                                                         lStockReservo,
                                                         lConnection,
                                                         lTransaction,
                                                         pBePedidoDet.No_linea) Then

                    If pIdPickingEnc > 0 Then

                        If Not lStockReservo Is Nothing Then

                            If Actualiza_Picking_Existente(lStockReservo,
                                                           pBePedidoDet,
                                                           pIdPickingEnc,
                                                           lConnection,
                                                           lTransaction) Then

                                Reservar_Stock_Por_Linea = True

                            End If

                        Else
                            Throw New Exception("Error 20180702: No se pudo actualizar el picking asociado al pedido")
                        End If

                    Else
                        Reservar_Stock_Por_Linea = True
                    End If

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Reservar_Stock_Por_Linea(ByVal vDiasVencimientoCliente As Double,
                                                    ByRef pListBePedidoDet As List(Of clsBeTrans_pe_det),
                                                    ByRef pListBeStock_res As List(Of clsBeStock_res),
                                                    ByVal pIdPickingEnc As Integer,
                                                    ByVal MaquinaQueSolicita As String,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByRef pIdPedidoDetPadre As Integer,
                                                    ByVal pIdPropietario As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Reservar_Stock_Por_Linea = False

        Try

            Dim ResultadoInsert As Integer = 0
            Dim pBeStockResSol As clsBeStock_res = Nothing


            lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each pBePedidoDet In pListBePedidoDet

                pBeStockResSol = pListBeStock_res.Where(Function(x) x.IdPedidoDet = pBePedidoDet.IdPedidoDet).FirstOrDefault()

                If Not pBeStockResSol Is Nothing Then

                    If pBePedidoDet.IsNew Then

                        pBePedidoDet.IdPedidoDet = MaxID(lConnection, ltransaction) + 1
                        pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                        If Not pBePedidoDet.EsPadre Then pBePedidoDet.IdPedidoDetPadre = pIdPedidoDetPadre
                        ResultadoInsert = Insertar(pBePedidoDet, lConnection, ltransaction)
                        pBePedidoDet.IsNew = False

                    Else
                        '#EJC20171023_0222PM: No me gusta esta chapusería pero se agregó por cuando modifican una línea existente en el pedido.
                        'Ver ref -> '#EJC20171024_1245PM_REF en forma de pedido
                        If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, ltransaction) Then
                            pBePedidoDet.IdPedidoDet = MaxID(lConnection, ltransaction) + 1
                            pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                            ResultadoInsert = Insertar(pBePedidoDet, lConnection, ltransaction)
                        Else

                            pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                               lConnection,
                                                                                               ltransaction)

                            pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                                                       lConnection,
                                                                                                                       ltransaction)
                            ResultadoInsert = Actualizar(pBePedidoDet, lConnection, ltransaction)

                        End If

                    End If

                    If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                        Dim vCantidadReservada As Double = 0
                        Dim vDifPedidoVrsReservado As Double = 0

                        Dim vCantidadSolicitadaUMBas As Double = 0
                        Dim vIdxPresentacion As Integer = 0
                        Dim BePres As New clsBeProducto_Presentacion

                        If pBePedidoDet.IdPresentacion <> 0 Then

                            Dim vIdPresentacion As Integer = pBePedidoDet.IdPresentacion

                            vIdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                            If vIdxPresentacion = -1 Then

                                BePres = New clsBeProducto_Presentacion
                                BePres.IdPresentacion = vIdPresentacion
                                clsLnProducto_presentacion.GetSingle(BePres, lConnection, ltransaction)
                                lPresentaciones.Add(BePres.Clone())

                            Else
                                BePres = New clsBeProducto_Presentacion
                                BePres = lPresentaciones(vIdxPresentacion).Clone()
                            End If

                            pBePedidoDet.Presentacion = BePres

                            If Not BePres Is Nothing Then
                                vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor
                            End If

                        Else

                            vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad

                        End If

                        For Each Sr In pBePedidoDet.ListaStockRes

                            'Get_Cantidad_Reservada_By_IdStock
                            vCantidadReservada = clsLnStock_res.Get_Cantidad_Reservada_By_IdStock(Sr.IdStock,
                                                                                                  lConnection,
                                                                                                  ltransaction)


                            vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservada

                            'El pedido, puede venir en dos variantes:
                            '1. Variante 1: El pedido viene en UMBas.
                            '2. Variante 2: El pedido viene en Presentación.

                            'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                            'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion > 0.

                            '2. Variante 2 + Caso de uso #2
                            If pBePedidoDet.IdPresentacion > 0 AndAlso pBeStockResSol.IdPresentacion > 0 Then
                                If Not BePres Is Nothing Then
                                    pBeStockResSol.Cantidad = Math.Round(vDifPedidoVrsReservado / BePres.Factor, 6)
                                End If
                            End If

                            Select Case vDifPedidoVrsReservado

                                Case Is > 0 'Se aumentó la cantidad en el pedido, 'Por lo tanto se debe aumentar la cantidad en picking.

                                    pBeStockResSol.Cantidad -= vCantidadReservada

                                    If clsLnStock_res.Reserva_Stock(pBeStockResSol,
                                                                    pIdPropietario,
                                                                    vDiasVencimientoCliente,
                                                                    MaquinaQueSolicita,
                                                                    lConnection,
                                                                    ltransaction) Then

                                        If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                            If Actualiza_Picking_Existente(pBeStockResSol,
                                                                           pBePedidoDet,
                                                                           pIdPickingEnc,
                                                                           lConnection,
                                                                           ltransaction) Then

                                                Reservar_Stock_Por_Linea = True

                                            End If

                                        Else
                                            Reservar_Stock_Por_Linea = True
                                        End If

                                    End If

                                Case Is < 0 'Se disminuyó la cantidad en el pedido

                                    For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)

                                        Debug.Print(Pu.IdPickingUbic)

                                    Next

                                Case Else
                                    Exit Select

                            End Select

                        Next

                    Else

                        If clsLnStock_res.Tiene_StockRes(pBeStockResSol.IdPedido,
                                                         pBeStockResSol.IdPedidoDet,
                                                        lConnection,
                                                        ltransaction) Then

                            If Not clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(pBeStockResSol.IdPedido,
                                                                                                           pBeStockResSol.IdPedidoDet,
                                                                                                           lConnection,
                                                                                                           ltransaction) Then

                                Throw New Exception("No se pudo eliminar el stock reservado antes de reservar de nuevo el stock")

                            End If

                        End If

                        Dim lStockReservo As New List(Of clsBeStock_res)

                        If clsLnStock_res.Reserva_Stock_NAV_BYB(pBeStockResSol,
                                                                vDiasVencimientoCliente,
                                                                MaquinaQueSolicita,
                                                                lStockReservo,
                                                                pIdEmpresa,
                                                                pIdBodega,
                                                                pIdPropietario,
                                                                lConnection,
                                                                ltransaction) Then

                            If pIdPickingEnc > 0 Then

                                If Not lStockReservo Is Nothing Then

                                    If Actualiza_Picking_Existente(lStockReservo,
                                       pBePedidoDet,
                                       pIdPickingEnc,
                                       lConnection,
                                       ltransaction) Then

                                        Reservar_Stock_Por_Linea = True

                                    End If

                                Else
                                    Throw New Exception("Error 20180702: No se pudo actualizar el picking asociado al pedido")
                                End If

                            Else
                                Reservar_Stock_Por_Linea = True
                            End If

                        End If

                    End If

                Else

                    '#EJC20201018:FIX Encontrado en CLC MP:
                    'La linea ya se había insertado y se volvía a insertar... un recuerdo encontrado después de la partida de Tzirin.
                    If pBePedidoDet.IsNew Then
                        Dim vIdPedidoDet As Integer = MaxID(lConnection, ltransaction) + 1
                        pBePedidoDet.IdPedidoDet = vIdPedidoDet
                        If pBePedidoDet.EsPadre Then pIdPedidoDetPadre = pBePedidoDet.IdPedidoDet
                        ResultadoInsert = Insertar(pBePedidoDet, lConnection, ltransaction)
                        pBePedidoDet.IsNew = False

                    End If

                End If

            Next

            ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
            'MessageBox.Show("Error al reservar stock:" & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Reservar_Stock_Especifico_Por_Linea(ByVal vDiasVencimientoCliente As Double,
                                                               ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                               ByRef pBeStockResSol As clsBeStock_res,
                                                               ByRef pBeStockEspecifico As clsBeStock,
                                                               ByVal pIdPickingEnc As Integer,
                                                               ByVal MaquinaQueSolicita As String,
                                                               ByVal pIdPropietario As Integer) As Boolean

        Dim lconection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Reservar_Stock_Especifico_Por_Linea = False

        Try

            Dim ResultadoInsert As Integer = 0

            lconection.Open() : ltransaction = lconection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBePedidoDet.IsNew Then
                pBePedidoDet.IdPedidoDet = MaxID(lconection, ltransaction) + 1
                pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lconection, ltransaction)
            Else
                '#EJC20171023_0222PM: No me gusta esta chapusería pero se agregó por cuando modifican una línea existente en el pedido.
                'Ver ref -> '#EJC20171024_1245PM_REF en forma de pedido
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lconection, ltransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lconection, ltransaction) + 1
                    pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lconection, ltransaction)
                Else
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lconection, ltransaction)
                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lconection,
                                                                                                               ltransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lconection, ltransaction)
                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidadReservada As Double = 0
                Dim vDifPedidoVrsReservado As Double = 0

                Dim vCantidad_Reservada_By_IdStock As Double = 0
                Dim vCantidadPendienteDeReservar As Double = 0
                Dim vDifPedidoVrsReservadoUMBas As Double = 0
                Dim BePres As New clsBeProducto_Presentacion
                Dim vDifTotalPedidoVrsReservado As Double = 0
                Dim vCantidadSolicitadaUMBas As Double = 0
                Dim vDiferenciaStockALiberar As Double = 0
                Dim vCantReservadaActual As Double = (From p In pBePedidoDet.ListaStockRes).Sum(Function(x) x.Cantidad)
                'segui copiando lo demas creo que ya solo so nlas variables y probas, ya se me va a pagar la maquinas :(
                'De acuerdo
                If pBePedidoDet.IdPresentacion <> 0 Then

                    BePres.IdPresentacion = pBePedidoDet.IdPresentacion
                    clsLnProducto_presentacion.GetSingle(BePres, lconection, ltransaction)

                    pBePedidoDet.Presentacion = BePres

                    If Not BePres Is Nothing Then
                        vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor
                    End If

                Else

                    vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad

                End If

                vDifTotalPedidoVrsReservado = vCantidadSolicitadaUMBas - vCantReservadaActual

                '#EJC202308281221A.
                'El pedido, puede venir en dos variantes:
                '1. Variante 1: El pedido viene en UMBas.
                '2. Variante 2: El pedido viene en Presentación.
                'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion > 0.

                '2. Variante 2 + Caso de uso #2
                If pBePedidoDet.IdPresentacion > 0 AndAlso pBeStockResSol.IdPresentacion > 0 Then
                    If Not BePres Is Nothing Then
                        pBeStockResSol.Cantidad = Math.Round(vDifTotalPedidoVrsReservado / BePres.Factor, 6)
                    End If
                End If

                Select Case vDifTotalPedidoVrsReservado

                        '#EJC20191114:
                        'Se aumentó la cantidad en el pedido, 
                        'Por lo tanto se debe aumentar la cantidad reservada 
                        'y la cantidad en picking (si tiene)
                        'Solo se reversa el excendente (pej. si tenía 4 reservadas y la cantidad cambió a 5, 
                        'Solo se reserva +1
                    Case Is > 0

                        pBeStockResSol.Cantidad = vDifTotalPedidoVrsReservado

                        If clsLnStock_res.Reserva_Stock(pBeStockResSol,
                                                        pIdPropietario,
                                                        vDiasVencimientoCliente,
                                                        MaquinaQueSolicita,
                                                        lconection,
                                                        ltransaction) Then

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                If Actualiza_Picking_Existente(pBeStockResSol,
                                                               pBePedidoDet,
                                                               pIdPickingEnc,
                                                               lconection,
                                                               ltransaction) Then

                                    Reservar_Stock_Especifico_Por_Linea = True

                                End If

                            Else
                                Reservar_Stock_Especifico_Por_Linea = True
                            End If

                        End If

                    Case Is < 0 'Se disminuyó la cantidad en el pedido

                        vDifTotalPedidoVrsReservado = Math.Abs(vDifTotalPedidoVrsReservado)

                        For Each Sr In pBePedidoDet.ListaStockRes

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                Throw New Exception("Error de usuario en el proceso: No se puede reducir la cantidad de la línea de pedido porque ya fue asignada a un proceso de picking.")

                                'For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                '    Debug.Print(Pu.IdPickingUbic)
                                'Next

                            Else

                                vCantidad_Reservada_By_IdStock = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(Sr.IdStock,
                                                                                                                       False,
                                                                                                                       lconection,
                                                                                                                       ltransaction)


                                If vDifTotalPedidoVrsReservado > 0 Then

                                    '#EJC20191114:
                                    'Si la cantidad a restar es menor que la cantidad reservada
                                    'en un IdStockRes (Ej: reservado es 5 y la cantidad se cambió a 4)
                                    'Se debe restar 1 de ese IdStockRes.
                                    If vDifTotalPedidoVrsReservado < Sr.Cantidad Then
                                        'vDiferenciaStockALiberar = Sr.Cantidad - vDifTotalPedidoVrsReservado
                                        Sr.Cantidad -= Math.Abs(vDifTotalPedidoVrsReservado)
                                        clsLnStock_res.Actualizar(Sr, lconection, ltransaction)
                                    ElseIf vDifTotalPedidoVrsReservado = Sr.Cantidad Then
                                        vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lconection, ltransaction)
                                    ElseIf vDifTotalPedidoVrsReservado > Sr.Cantidad Then
                                        vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lconection, ltransaction)
                                    End If

                                End If

                            End If

                        Next

                    Case Else
                        Exit Select

                End Select

            Else

                If pBePedidoDet.IdPresentacion <> 0 Then
                    pBePedidoDet.Presentacion.IdPresentacion = pBePedidoDet.IdPresentacion
                    clsLnProducto_presentacion.GetSingle(pBePedidoDet.Presentacion, lconection, ltransaction)
                End If

                If clsLnStock_res.Tiene_StockRes(pBeStockResSol.IdPedido,
                                                 pBeStockResSol.IdPedidoDet,
                                                 lconection,
                                                 ltransaction) Then

                    If Not clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(pBeStockResSol.IdPedido,
                                                                                                  pBeStockResSol.IdPedidoDet,
                                                                                                  lconection,
                                                                                                  ltransaction) Then

                        Throw New Exception("No se pudo eliminar el stock reservado antes de reservar de nuevo el stock")

                    End If

                ElseIf Not pBeStockEspecifico Is Nothing Then

                    Debug.Write("Aquí debería eliminar algo")

                End If

                Dim lStockReservo As New List(Of clsBeStock_res)

                If clsLnStock_res.Reserva_Stock_Especifico(pBeStockResSol,
                                                            vDiasVencimientoCliente,
                                                            MaquinaQueSolicita,
                                                            lStockReservo,
                                                            pBeStockEspecifico,
                                                            lconection,
                                                            ltransaction) Then

                    If pIdPickingEnc > 0 Then

                        If Not lStockReservo Is Nothing Then

                            If Actualiza_Picking_Existente(lStockReservo,
                                                           pBePedidoDet,
                                                           pIdPickingEnc,
                                                           lconection,
                                                           ltransaction) Then

                                Reservar_Stock_Especifico_Por_Linea = True

                            End If

                        Else
                            Throw New Exception("Error 20180702: No se pudo actualizar el picking asociado al pedido")
                        End If

                    Else
                        Reservar_Stock_Especifico_Por_Linea = True
                    End If

                End If

            End If

            ltransaction.Commit()

        Catch ex As Exception
            If ltransaction IsNot Nothing Then ltransaction.Rollback()
            'MessageBox.Show("Error al reservar stock:" & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Throw ex
        Finally
            If lconection.State = ConnectionState.Open Then lconection.Close()
        End Try

    End Function

    Public Shared Function Reservar_Stock_Por_Linea_Interface(ByVal vDiasVencimientoCliente As Double,
                                                              ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                              ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                              ByRef pBeStockRes As clsBeStock_res,
                                                              ByVal MaquinaQueSolicita As String,
                                                              ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                              ByVal IdPropietarioBodega As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As Boolean

        Reservar_Stock_Por_Linea_Interface = False

        lTransaction.Save("Init_Stock")

        Try

            Dim ResultadoInsert As Integer = 0

            If pBePedidoDet.IsNew Then
                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
            Else
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection,
                                                                                       lTransaction)

                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet,
                                                 lConnection,
                                                 lTransaction)
                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidadReservadaEnUMDocumento As Double = 0
                Dim vDifPedidoVrsReservado As Double = 0
                Dim vIdPickingEnc As Integer = 0
                Dim pListStockResOUT As New List(Of clsBeStock_res)

                For Each Sr In pBePedidoDet.ListaStockRes

                    vCantidadReservadaEnUMDocumento = clsLnStock_res.Get_Cantidad_ReservadaEnUMDocumento_By_IdStock(Sr.IdStock,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)

                    'El pedido, puede venir en dos variantes:
                    '1. Variante 1: El pedido viene en UMBas.
                    '2. Variante 2: El pedido viene en Presentación.
                    '
                    'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                    'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion <> 0.

                    vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservadaEnUMDocumento

                    Select Case vDifPedidoVrsReservado

                        Case Is > 0 'Se aumentó la cantidad en el pedido, 'Por lo tanto se debe aumentar la cantidad en picking.                            

                            pBeStockRes.Cantidad -= vCantidadReservadaEnUMDocumento

                            If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockRes,
                                                                     vDiasVencimientoCliente,
                                                                     MaquinaQueSolicita,
                                                                     pBeConfigEnc,
                                                                     pBeTrasladoDet.Qty_to_Receive,
                                                                     IdPropietarioBodega,
                                                                     pListStockResOUT,
                                                                     lConnection,
                                                                     lTransaction) Then

                                If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                    vIdPickingEnc = pBePedidoDet.ListaPickingUbic.FirstOrDefault.IdPickingEnc

                                    If Actualiza_Picking_Existente(pBeStockRes,
                                                                   pBePedidoDet,
                                                                   vIdPickingEnc,
                                                                   lConnection,
                                                                   lTransaction) Then

                                        Reservar_Stock_Por_Linea_Interface = True

                                    End If

                                End If

                            Else
                                Reservar_Stock_Por_Linea_Interface = False
                            End If

                        Case Is < 0 'Se disminuyó la cantidad en el pedido

                            For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                Debug.Print(Pu.IdPickingUbic)
                            Next

                        Case Else
                            Exit Select

                    End Select

                Next

            Else

                Dim pListStockResOUT As New List(Of clsBeStock_res)

                If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockRes,
                                                        vDiasVencimientoCliente,
                                                        MaquinaQueSolicita,
                                                        pBeConfigEnc,
                                                        pBeTrasladoDet.Qty_to_Receive,
                                                        IdPropietarioBodega,
                                                        pListStockResOUT,
                                                        lConnection,
                                                        lTransaction, 0,
                                                        False,
                                                        pBeTrasladoDet) Then

                    Reservar_Stock_Por_Linea_Interface = True
                Else
                    Reservar_Stock_Por_Linea_Interface = False
                End If

            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback("Init_Stock")
            'Throw ex
            Throw ex
        End Try

    End Function

    '#CKFK20220628 Agregué esta función para poder recibir la lista de stock reservada
    Public Shared Function Reservar_Stock_Por_Linea_Interface(ByVal vDiasVencimientoCliente As Double,
                                                              ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                              ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                              ByRef pBeStockRes As clsBeStock_res,
                                                              ByVal MaquinaQueSolicita As String,
                                                              ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                              ByVal IdPropietarioBodega As Integer,
                                                              ByRef pListStockResOUT As List(Of clsBeStock_res),
                                                              ByRef plblprg As RichTextBox,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As Boolean

        Reservar_Stock_Por_Linea_Interface = False

        lTransaction.Save("Init_Stock")

        Try

            Dim ResultadoInsert As Integer = 0
            Dim pBeTrasladoTemp As New clsBeI_nav_ped_traslado_det

            pBeTrasladoTemp.Item_No = pBeTrasladoDet.No
            pBeTrasladoTemp.Line_No = pBeTrasladoDet.Line_No
            pBeTrasladoTemp.NoEnc = pBeTrasladoDet.NoEnc
            clsLnI_nav_ped_traslado_det.GetSingle(lConnection, lTransaction, pBeTrasladoTemp)

            If pBePedidoDet.IsNew Then

                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet

                '#CKFK20221117 Agregué esto para actualizar el pedido con la cantidad solicitada y la unidad de medida correctos
                If pBeTrasladoDet.Variant_Code <> pBeTrasladoTemp.Variant_Code And pBeTrasladoTemp.Unit_of_Measure_Code <> pBePedidoDet.Nom_presentacion Then
                    If Not pBeTrasladoDet Is Nothing Then
                        If pBePedidoDet.IdPresentacion <> 0 Then
                            pBePedidoDet.Cantidad = Math.Ceiling(Math.Round(pBeTrasladoDet.Quantity * pBePedidoDet.Factor, 2))
                            pBePedidoDet.Nom_presentacion = ""
                            pBePedidoDet.IdPresentacion = 0
                        End If
                    End If
                End If

                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
            Else
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection, lTransaction)
                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction)
                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidadReservada As Double = 0
                Dim vDifPedidoVrsReservado As Double = 0
                Dim vIdPickingEnc As Integer = 0

                For Each Sr In pBePedidoDet.ListaStockRes

                    vCantidadReservada = clsLnStock_res.Get_Cantidad_Reservada_By_IdStock(Sr.IdStock,
                                                                                          lConnection,
                                                                                          lTransaction)
                    vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservada

                    Select Case vDifPedidoVrsReservado

                        Case Is > 0 'Se aumentó la cantidad en el pedido, 'Por lo tanto se debe aumentar la cantidad en picking.                            

                            pBeStockRes.Cantidad -= vCantidadReservada

                            If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockRes,
                                                                    vDiasVencimientoCliente,
                                                                    MaquinaQueSolicita,
                                                                    pBeConfigEnc,
                                                                    pBeTrasladoDet.Qty_to_Receive,
                                                                    IdPropietarioBodega,
                                                                    pListStockResOUT,
                                                                    lConnection,
                                                                    lTransaction,
                                                                    pBeTrasladoDet.Line_No,
                                                                    False,
                                                                    pBeTrasladoDet) Then

                                If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                    vIdPickingEnc = pBePedidoDet.ListaPickingUbic.FirstOrDefault.IdPickingEnc

                                    If Actualiza_Picking_Existente(pBeStockRes,
                                                                   pBePedidoDet,
                                                                   vIdPickingEnc,
                                                                   lConnection,
                                                                   lTransaction) Then

                                        Reservar_Stock_Por_Linea_Interface = True

                                    End If

                                End If

                            Else
                                Reservar_Stock_Por_Linea_Interface = False
                            End If

                        Case Is < 0 'Se disminuyó la cantidad en el pedido

                            For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                Debug.Print(Pu.IdPickingUbic)
                            Next

                        Case Else
                            Exit Select

                    End Select

                Next

            Else

                'Dim pListStockResOUT As New List(Of clsBeStock_res)

                If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockRes,
                                                         vDiasVencimientoCliente,
                                                         MaquinaQueSolicita,
                                                         pBeConfigEnc,
                                                         pBeTrasladoDet.Qty_to_Receive,
                                                         IdPropietarioBodega,
                                                         pListStockResOUT,
                                                         lConnection,
                                                         lTransaction,
                                                         pBeTrasladoDet.Line_No,
                                                         False,
                                                         pBeTrasladoDet) Then

                    Reservar_Stock_Por_Linea_Interface = True
                Else
                    Reservar_Stock_Por_Linea_Interface = False
                End If

            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback("Init_Stock")
            Throw New Exception(String.Format(vbNewLine & "{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            'Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_Reservado_Por_Linea_Interface(ByVal vDiasVencimientoCliente As Double,
                                                                   ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                                   ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                                   ByRef pBeStockRes As clsBeStock_res,
                                                                   ByVal MaquinaQueSolicita As String,
                                                                   ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                                   ByVal pIdPropietario As Integer,
                                                                   ByVal Conmutar_Umbas_A_Presentacion As Boolean,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeStock_res)

        Get_Stock_Reservado_Por_Linea_Interface = Nothing

        lTransaction.Save("Init_Stock")

        Dim BeConfigEnc As New clsBeI_nav_config_enc With {.Idnavconfigenc = 1, .IdPropietario = pIdPropietario}
        Dim vIdxConfig As Integer = -1

        Try

            If vIdxConfig = -1 Then

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(pBeStockRes.IdBodega,
                                                                                            pIdPropietario,
                                                                                            lConnection,
                                                                                            lTransaction)
                lBeConfigInMemory.Add(BeConfigEnc.Clone())

            Else
                BeConfigEnc = New clsBeI_nav_config_enc With {.Idnavconfigenc = 1}
                BeConfigEnc = lBeConfigInMemory(vIdxConfig).Clone()
            End If

            Dim ResultadoInsert As Integer = 0
            Dim lStockReservadoLotes As New List(Of clsBeStock_res)

            If pBePedidoDet.IsNew Then
                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
            Else
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection, lTransaction)
                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection, lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction)
                End If
            End If

            pBeTrasladoDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
            clsLnI_nav_ped_traslado_det.Actualizar_IdPedidoDet(pBeTrasladoDet, lConnection, lTransaction)

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidadReservada As Double = 0
                Dim vDifPedidoVrsReservado As Double = 0
                Dim vIdPickingEnc As Integer = 0


                For Each Sr In pBePedidoDet.ListaStockRes

                    vCantidadReservada = clsLnStock_res.Get_Cantidad_Reservada_By_IdStock(Sr.IdStock,
                                                                                          lConnection,
                                                                                          lTransaction)
                    vDifPedidoVrsReservado = pBePedidoDet.Cantidad - vCantidadReservada

                    Select Case vDifPedidoVrsReservado

                        Case Is > 0 'Se aumentó la cantidad en el pedido, 'Por lo tanto se debe aumentar la cantidad en picking.                            

                            pBeStockRes.Cantidad -= vCantidadReservada

                            lStockReservadoLotes = clsLnStock_res.Reserva_Stock_Lista_Result(pBeStockRes,
                                                                                            vDiasVencimientoCliente,
                                                                                            MaquinaQueSolicita,
                                                                                            pBeConfigEnc,
                                                                                            pBeTrasladoDet,
                                                                                            pIdPropietario,
                                                                                            Conmutar_Umbas_A_Presentacion,
                                                                                            lConnection,
                                                                                            lTransaction)

                            If Not lStockReservadoLotes Is Nothing Then

                                If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                    vIdPickingEnc = pBePedidoDet.ListaPickingUbic.FirstOrDefault.IdPickingEnc

                                    If Actualiza_Picking_Existente(pBeStockRes,
                                                                   pBePedidoDet,
                                                                   vIdPickingEnc,
                                                                   lConnection,
                                                                   lTransaction) Then

                                        Get_Stock_Reservado_Por_Linea_Interface = lStockReservadoLotes

                                    End If

                                End If

                            End If

                        Case Is < 0 'Se disminuyó la cantidad en el pedido

                            For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                Debug.Print(Pu.IdPickingUbic)
                            Next

                        Case Else
                            Exit Select

                    End Select

                Next

            Else

                lStockReservadoLotes = clsLnStock_res.Reserva_Stock_Lista_Result(pBeStockRes,
                                                                                vDiasVencimientoCliente,
                                                                                MaquinaQueSolicita,
                                                                                pBeConfigEnc,
                                                                                pBeTrasladoDet,
                                                                                pIdPropietario,
                                                                                Conmutar_Umbas_A_Presentacion,
                                                                                lConnection,
                                                                                lTransaction)

                Get_Stock_Reservado_Por_Linea_Interface = lStockReservadoLotes

            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback("Init_Stock")
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Picking_Existente(ByRef lStockRes As List(Of clsBeStock_res),
                                                       ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                       ByVal pIdPickingEnc As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Boolean

        Actualiza_Picking_Existente = False

        Dim vDetalleActualizadoCorrectamente As Boolean = False
        Dim vIdPickingDet As Integer = 0
        Dim BeTransPickingEnc As New clsBeTrans_picking_enc
        Dim BeTransPeEnc As New clsBeTrans_pe_enc

        Try

            If pIdPickingEnc > 0 Then

                If lStockRes.Count > 0 Then

                    BeTransPickingEnc = clsLnTrans_picking_enc.GetSingle(pIdPickingEnc, lConnection, lTransaction)
                    BeTransPeEnc = clsLnTrans_pe_enc.GetSingle(pBePedidoDet.IdPedidoEnc, lConnection, lTransaction)

                    If Not BeTransPickingEnc Is Nothing Then

                        'Se tomará producto de un nuevo IdStock, insertar nuevo picking_ubic.
                        If clsLnTrans_picking_det.Insertar_PickingDet(pBePedidoDet,
                                                                      pIdPickingEnc,
                                                                      vIdPickingDet,
                                                                      lConnection,
                                                                      lTransaction) Then


                            For Each Sr As clsBeStock_res In lStockRes

                                Sr.IdPicking = pIdPickingEnc

                                '#EJC20191218: Corrección a raiz de que el picking ubic si tiene pres, 
                                'La cantidad va en pres.
                                If pBePedidoDet.IdPresentacion <> 0 Then
                                    Sr.Cantidad = Sr.Cantidad / pBePedidoDet.Presentacion.Factor
                                End If

                                If clsLnTrans_picking_ubic.Insertar_PickingUbic(Sr,
                                                                                vIdPickingDet,
                                                                                lConnection,
                                                                                lTransaction) Then

                                    If clsLnTrans_picking_det_parametros.Insertar_Parametros_Stock_Para_Picking(Sr.IdStock,
                                                                                                            vIdPickingDet,
                                                                                                            lConnection,
                                                                                                            lTransaction) Then
                                        vDetalleActualizadoCorrectamente = True

                                    End If

                                End If

                            Next


                        End If

                        If BeTransPickingEnc.Estado = "Despachado" Then

                            BeTransPickingEnc.Estado = "Pendiente"

                            clsLnTrans_picking_enc.Actualizar_Estado(BeTransPickingEnc,
                                                                     lConnection,
                                                                     lTransaction)

                        End If

                        If Not BeTransPeEnc Is Nothing Then

                            If BeTransPeEnc.Enviado_A_ERP Then

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc_Single(BeTransPeEnc.IdPedidoEnc,
                                                                                                        False,
                                                                                                        BeTransPeEnc.User_agr,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                            End If

                        End If

                    End If

                End If

            End If

            Actualiza_Picking_Existente = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualiza_Picking_Existente(ByRef SR As clsBeStock_res,
                                                       ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                       ByVal pIdPickingEnc As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Boolean

        Actualiza_Picking_Existente = False

        Dim vDetalleActualizadoCorrectamente As Boolean = False
        Dim BePickingUbicActual As clsBeTrans_picking_ubic
        Dim BePickingDetActual As New clsBeTrans_picking_det
        Dim vIdPickingEnc As Integer = pIdPickingEnc 'pBePedidoDet.ListaPickingUbic.FirstOrDefault.IdPickingEnc
        Dim vIdPickingDet As Integer
        Dim vIdStock As Integer = SR.IdStock
        Dim vIdPedidoDet As Integer = pBePedidoDet.IdPedidoDet

        Try

            If vIdPickingEnc > 0 Then

                BePickingUbicActual = pBePedidoDet.ListaPickingUbic.
                        Find(Function(x) x.IdStock = vIdStock _
                        AndAlso x.IdPedidoDet = vIdPedidoDet)

                If Not BePickingUbicActual Is Nothing Then

                    BePickingUbicActual.Cantidad_Solicitada = pBePedidoDet.Cantidad

                    If clsLnTrans_picking_ubic.Actualizar(BePickingUbicActual, lConnection, lTransaction) > 0 Then

                        BePickingDetActual = clsLnTrans_picking_det.GetSingle(pBePedidoDet.IdPedidoDet, lConnection, lTransaction)
                        BePickingDetActual.Cantidad = pBePedidoDet.Cantidad

                        If clsLnTrans_picking_det.Actualizar(BePickingDetActual, lConnection, lTransaction) > 0 Then
                            vDetalleActualizadoCorrectamente = True
                        End If

                    End If

                Else

                    'Se tomará producto de un nuevo IdStock, insertar nuevo picking_ubic.
                    If clsLnTrans_picking_det.Insertar_PickingDet(pBePedidoDet, vIdPickingEnc, vIdPickingDet, lConnection, lTransaction) Then

                        If clsLnTrans_picking_ubic.Insertar_PickingUbic(SR, vIdPickingDet, lConnection, lTransaction) Then

                            If clsLnTrans_picking_det_parametros.Insertar_Parametros_Stock_Para_Picking(SR.IdStock, vIdPickingDet, lConnection, lTransaction) Then
                                vDetalleActualizadoCorrectamente = True
                            End If

                        End If

                    End If

                End If

            End If

            Actualiza_Picking_Existente = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pIdPedidoDet As Integer,
                                  ByVal pIdPedidoEnc As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As Boolean

        Existe = False

        Try

            Dim vSQL As String = "SELECT * FROM trans_pe_det WHERE IdPedidoDet=@IdPedidoDet AND IdPedidoEnc=@IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Existe = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pIdPedidoEnc As Integer,
                                  ByVal pNoLinea As Integer,
                                  ByRef pBeTrans_pe_det As clsBeTrans_pe_det,
                                  ByVal CodigoProducto As String,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As Boolean

        Existe = False

        Try

            Dim vSQL As String = "SELECT * FROM trans_pe_det 
                    WHERE (IdPedidoEnc=@IdPedidoEnc
                    AND No_Linea = @No_Linea
                    AND codigo_producto = @codigo_producto)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@No_Linea", pNoLinea)
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo_producto", CodigoProducto)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    pBeTrans_pe_det = New clsBeTrans_pe_det

                    If lDT.Rows.Count = 1 Then
                        Cargar(pBeTrans_pe_det, lDT.Rows(0), lConnection, lTransaction)
                    End If

                    Existe = True

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180502 04:15 PM Agregué el campo ndias porque me hace falta para poder listar el inventario disponible al realizar reemplazos
    Public Shared Function Get_Detalle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_By_IdPedidoEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeDetallePedidoAVerificar)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    'AT20220607 Quite la condición <> 0 , para que se muestren siempre las lineas de verificación
                    Dim vSQL As String = "SELECT * FROM VW_VERIFICACION WHERE IdPedidoEnc = @IdPedidoEnc"
                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeDetallePedidoAVerificar

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                        Return lReturnList

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180502 04:15 PM Agregué el campo ndias porque me hace falta para poder listar el inventario disponible al realizar reemplazos
    Public Shared Function Get_Detalle_By_IdPedidoEnc_And_IdProducto(ByVal pIdPedidoEnc As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_By_IdPedidoEnc_And_IdProducto = Nothing

        Try

            Dim lReturnList As New List(Of clsBeDetallePedidoAVerificar)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    'AT20220607 Quite la condición <> 0 , para que se muestren siempre las lineas de verificación
                    Dim vSQL As String = "SELECT * FROM VW_VERIFICACION WHERE IdPedidoEnc = @IdPedidoEnc"
                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeDetallePedidoAVerificar

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                        Return lReturnList

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20220509 Obtener detalle de verificación sin agrupar por lote y fecha_ven
    Public Shared Function Get_Detalle_By_IdPedidoEnc_Consolidado(ByVal pIdPedidoEnc As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_By_IdPedidoEnc_Consolidado = Nothing

        Try

            Dim lReturnList As New List(Of clsBeDetallePedidoAVerificar)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    'AT20220607 Quite la condición <> 0 , para que se muestren siempre las lineas de verificación
                    Dim vSQL As String = "SELECT * FROM VW_Verificacion_Consolidada WHERE IdPedidoEnc = @IdPedidoEnc"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeDetallePedidoAVerificar

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                        Return lReturnList

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20230221 Obtener detalle de verificación agrupando por lote y fecha_ven
    Public Shared Function Get_Detalle_By_IdPedidoEnc_Consolidado_LFV(pIdPedidoEnc As Integer,
                                                                      pIdProductoBodega As Integer,
                                                                      pIdPresentacion As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_By_IdPedidoEnc_Consolidado_LFV = Nothing

        Try

            Dim lReturnList As New List(Of clsBeDetallePedidoAVerificar)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Dim vSQL As String = "SELECT * FROM VW_Verificacion_Consolidada_LFV
                                          WHERE IdPedidoEnc = @IdPedidoEnc 
                                          AND cantidad_recibida > 0
                                          AND cantidad_recibida > cantidad_verificada 
                                          AND IdProductoBodega = @IdProductoBodega "

                    If pIdPresentacion <> 0 Then
                        vSQL += " AND IdPresentacionPicking = @IdPresentacion"
                    Else
                        vSQL += " AND (IdPresentacionPicking = 0 or IdPresentacionPicking is null) "
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        If pIdPresentacion <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeDetallePedidoAVerificar

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

                        Return lReturnList

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Cantidad_Y_Peso_Despachado(ByRef oBeTrans_pe_det As clsBeTrans_pe_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim vSQL As String = "update trans_pe_det 
                    set cant_despachada = @cant_despachada,
                    peso_despachado += @peso_despachado
                    Where(IdPedidoDet = @IdPedidoDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(vSQL, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(vSQL, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@CANT_DESPACHADA", oBeTrans_pe_det.Cant_despachada))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_pe_det.Peso_despachado))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#CKFK 20180502 07:21 AM Creé la funcion GetReemplazosPedido para obtener todos los reemplazos que se realizaron en un pedidos
    '#CKFK 20180502 04:15 PM Agregué el campo ndias porque me hace falta para poder listar el inventario disponible al realizar reemplazos
    Public Shared Function Get_Reemplazo_Producto_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Reemplazo_Producto_By_IdPedidoEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeDetallePedidoAVerificar)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '#CKFK 20180930 Agregué esto al Group By porque daba error la consulta , pubic.dañado_verificacion
                    '#CKFK 20200512 Agregué el campo pubic.lic_plate porque este campo se agregó a la clase clsBeDetallePedidoAVerificar y daba error esta función
                    '#CKFK 20201008 Agregué el campo , pubic.IdProductoEstado porque este campo se agregó a la clase clsBeDetallePedidoAVerificar
                    Dim vSQL As String = "SELECT ped.IdPedidoEnc, ped.IdPedidoDet, ped.IdProductoBodega, pubic.lote, pubic.fecha_vence,
                        ped.nom_presentacion, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado,
                        SUM(pubic.cantidad_solicitada) AS cantidad_solicitada, SUM(pubic.cantidad_recibida) AS cantidad_recibida,
                        SUM(pubic.cantidad_verificada) AS cantidad_verificada, 
                        ped.IdPresentacion, ped.IdUnidadMedidaBasica, p.Codigo, ped.ndias, pubic.lic_plate, pubic.IdProductoEstado
                        FROM trans_pe_det ped inner join trans_picking_det pkdet ON  ped.IdPedidoDet = pkdet.IdPedidoDet
                        INNER JOIN trans_picking_ubic pubic ON pkdet.IdPickingDet = pubic.IdPickingDet
                        INNER JOIN producto_bodega pb ON pb.IdProductoBodega = ped.IdProductoBodega
                        INNER JOIN producto P ON p.IdProducto = pb.IdProducto
                        WHERE ped.IdPedidoEnc = @IdPedidoEnc  
                        AND pkdet.idpickingenc NOT IN (SELECT idpickingenc FROM trans_picking_enc WHERE estado = 'Anulado' ) 
                        GROUP BY ped.IdPedidoEnc, ped.IdPedidoDet, ped.IdProductoBodega, pubic.lote, pubic.fecha_vence,
                        ped.nom_presentacion, ped.nom_unid_med, ped.nombre_producto, ped.nom_estado,
                        ped.IdPresentacion, ped.IdUnidadMedidaBasica, p.Codigo, ped.ndias, pubic.dañado_verificacion, pubic.lic_plate, pubic.IdProductoEstado
                        HAVING pubic.dañado_verificacion = 1"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeDetallePedidoAVerificar

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                            Return lReturnList

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

    Public Shared Function Get_All_Stock_Res_By_IdPedidoEnc_And_IdPickingEnc(ByVal pIdPedidoEnc As Integer,
                                                                             ByVal pIdPickingEnc As Integer,
                                                                             ByVal PendientesDeDespacho As Boolean,
                                                                             ByVal EsPickingNuevo As Boolean,
                                                                             ByVal EsPicking As Boolean) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim vSQL As String = ""

        Try

            vSQL = "SELECT p.codigo, p.nombre, pp.nombre AS presentacion, 
                    pe.nombre AS NomEstado, 
                    um.Nombre AS unidadmedida, 
                    pr.nombre_comercial AS propietario, 
                    bu.descripcion AS bodegaubicacion, 
                    ISNULL(s.cantidad,0) AS CantidadSF, 
                    pp.factor, ISNULL(ISNULL(s.cantidad,0) / pp.factor,0) AS Cantidad, 
                    res.IdStockRes, 
                    res.IdTransaccion, 
                    res.Indicador, 
                    res.IdPedidoDet, 
                    res.IdStock, 
                    res.IdPropietarioBodega, 
                    res.IdProductoBodega, 
                    res.IdUbicacion, 
                    res.IdProductoEstado, 
                    res.IdPresentacion, 
                    res.IdUnidadMedida, 
                    res.lote, 
                    res.lic_plate, 
                    res.serial, "

            If EsPickingNuevo Then
                vSQL += "Res.Cantidad As CantidadReservada, "
            Else
                vSQL += "SUM(ISNULL(trans_picking_ubic.cantidad_solicitada, 0)) As CantidadReservada, "
            End If

            vSQL += " res.peso, 
                    res.estado, 
                    res.fecha_vence, 
                    res.uds_lic_plate, 
                    res.ubicacion_ant AS IdUbicacion_anterior, 
                    res.no_bulto, 
                    res.IdRecepcion AS IdRecepcionEnc, 
                    res.IdPicking, 
                    res.IdPedido, 
                    res.IdDespacho, 
                    res.añada, 
                    res.fecha_manufactura, 
                    SUM(ISNULL(dbo.trans_picking_ubic.peso_recibido, 0)) AS Peso_Recibido, 
                    SUM(ISNULL(dbo.trans_picking_ubic.peso_verificado, 0)) AS Peso_Verificado, 
                    ISNULL(dbo.trans_picking_ubic.acepto, 0) AS Acepto, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_recibida, 0)) AS cantidad_recibida, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_verificada, 0)) AS cantidad_verificada, 
                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_despachada, 
                    0)) AS Cantidad_Despachada, 
                    ISNULL(dbo.trans_picking_ubic.encontrado, 0) AS Encontrado, 
                    dbo.Nombre_Completo_Ubicacion(res.IdUbicacion, res.IDBODEGA) AS NomUbic,
                    res.IDBODEGA,
                    res.fecha_ingreso, s.IdRecepcionEnc, s.IdRecepcionDet,
                    res.IdProductoTallaColor,
                    col.Codigo AS Color,
                    tal.Codigo AS Talla
                    FROM  stock_res AS res INNER JOIN
                    propietario_bodega AS prb ON res.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                    producto_bodega AS pb ON pb.IdProductoBodega = res.IdProductoBodega INNER JOIN
                    producto_estado AS pe ON res.IdProductoEstado = pe.IdEstado INNER JOIN
                    unidad_medida AS um ON res.IdUnidadMedida = um.IdUnidadMedida INNER JOIN
                    propietarios AS pr ON prb.IdPropietario = pr.IdPropietario INNER JOIN
                    producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                    bodega_ubicacion AS bu RIGHT OUTER JOIN
                    trans_picking_det INNER JOIN
                    trans_picking_ubic ON trans_picking_det.IdPickingDet = trans_picking_ubic.IdPickingDet ON bu.IdBodega = trans_picking_ubic.IdBodega AND bu.IdUbicacion = trans_picking_ubic.IdUbicacion ON 
                    res.IDBODEGA = trans_picking_ubic.IdBodega AND res.IdPedidoDet = trans_picking_det.IdPedidoDet AND res.IdStock = trans_picking_ubic.IdStock AND 
                    res.IdStockRes = trans_picking_ubic.IdStockRes LEFT OUTER JOIN
                    stock AS s ON res.IdStock = s.IdStock LEFT OUTER JOIN
                    producto_presentacion AS pp ON res.IdPresentacion = pp.IdPresentacion
                    LEFT JOIN producto_talla_color AS ptc
                        ON ptc.IdProductoTallaColor = res.IdProductoTallaColor   
                    LEFT JOIN color AS col
                        ON col.IdColor = ptc.IdColor
                    LEFT JOIN talla AS tal
                        ON tal.IdTalla = ptc.IdTalla
                    WHERE(Res.IdPedido = @IdPedido) 
                    AND (ISNULL(trans_picking_ubic.dañado_verificacion, 0) = 0) 
                    AND (ISNULL(trans_picking_ubic.dañado_picking, 0) = 0) 
                    AND (ISNULL(trans_picking_ubic.no_encontrado, 0) = 0) "

            If PendientesDeDespacho Then
                vSQL += " AND (trans_picking_ubic.cantidad_despachada < trans_picking_ubic.cantidad_verificada) "
            End If

            If Not EsPicking Then
                vSQL += " AND (trans_picking_ubic.cantidad_verificada > 0) "
            End If

            If pIdPickingEnc <> 0 Then
                vSQL += "AND (trans_picking_ubic.IdPickingEnc = @IdPickingEnc) "
            End If

            vSQL += "GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial, bu.descripcion, s.cantidad, pp.factor, s.cantidad / pp.factor, res.IdStockRes, 
                        Res.IdTransaccion, Res.Indicador, Res.IdPedidoDet, Res.IdStock, Res.IdPropietarioBodega, Res.IdProductoBodega, Res.IdUbicacion, Res.IdProductoEstado, 
                        res.IdPresentacion, res.IdUnidadMedida, res.lote, res.lic_plate, res.serial, res.cantidad, res.peso, res.estado, res.fecha_vence, res.uds_lic_plate, 
                        Res.ubicacion_ant, Res.no_bulto, Res.IdRecepcion, Res.IdPicking, Res.IdPedido, Res.IdDespacho,
                        res.añada, res.fecha_manufactura,
                        ISNULL(trans_picking_ubic.acepto, 0), 
                        ISNULL(trans_picking_ubic.encontrado, 0),bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion, res.IdBodega, res.fecha_ingreso, s.IdRecepcionEnc, s.IdRecepcionDet, 
                        res.IdProductoTallaColor,col.Codigo, tal.Codigo "

            vSQL += " ORDER BY bu.IdTramo,bu.Indice_x,bu.Nivel,bu.IdUbicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedido", pIdPedidoEnc)

                        If pIdPickingEnc <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                        End If


                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.Ubicacion_Nombre = IIf(IsDBNull(lRow("NomUbic")), "", lRow("NomUbic"))
                                Obj.UbicacionActual.NombreCompleto = Obj.Ubicacion_Nombre

                                If lRow("encontrado") IsNot DBNull.Value AndAlso lRow("encontrado") IsNot Nothing Then
                                    Obj.encontrado = CType(lRow("encontrado"), Boolean)
                                End If

                                If lRow("acepto") IsNot DBNull.Value AndAlso lRow("acepto") IsNot Nothing Then
                                    Obj.acepto = CType(lRow("acepto"), Boolean)
                                End If

                                If lRow("peso_recibido") IsNot DBNull.Value AndAlso lRow("peso_recibido") IsNot Nothing Then
                                    Obj.peso_pickeado = CType(lRow("peso_recibido"), Double)
                                End If

                                If lRow("peso_verificado") IsNot DBNull.Value AndAlso lRow("peso_verificado") IsNot Nothing Then
                                    Obj.peso_verificado = CType(lRow("peso_verificado"), Double)
                                End If

                                If lRow("cantidad_recibida") IsNot DBNull.Value AndAlso lRow("cantidad_recibida") IsNot Nothing Then
                                    Obj.Cantidad_Pickeada = CType(lRow("cantidad_recibida"), Double)
                                End If

                                If lRow("cantidad_verificada") IsNot DBNull.Value AndAlso lRow("cantidad_verificada") IsNot Nothing Then
                                    Obj.Cantidad_Verificada = CType(lRow("cantidad_verificada"), Double)
                                End If

                                If lRow("cantidad_despachada") IsNot DBNull.Value AndAlso lRow("cantidad_despachada") IsNot Nothing Then
                                    Obj.Cantidad_Despachada = CType(lRow("cantidad_despachada"), Double)
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


    Public Shared Function Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega(ByVal pIdPedidoEnc As Integer,
                                                                               ByVal pIdBodega As Integer) As DataTable

        Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdPedidoEnc as Correlativo, 
                                          CodigoProducto, NombreProducto, Clasificacion, UMBas, 
                                          Presentacion, cantidad_solicitada as Cantidad_Solicitada, 
                                          cantidad_recibida as Cantidad_Picking, cantidad_verificada as Cantidad_Verificada, 
                                          cantidad_despachada as Cantidad_Despachada, 
                                          Operador_Picking, Operador_Verifico, 
                                          Fecha_Picking, Fecha_Verificacion 
                                          FROM VW_Progreso_Picking_By_Operador 
                                          WHERE IdPedidoEnc = @IdPedidoEnc AND IdBodega= @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega = lDataTable

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_InfoPedido_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                         ByRef IdPedidoEnc As Integer,
                                                         ByRef Referencia As String,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Boolean

        Get_InfoPedido_By_IdPedidoDet = False

        Try

            IdPedidoEnc = 0 : Referencia = ""

            Dim sp As String = "SELECT IdPedidoEnc, Referencia FROM trans_pe_det WHERE IdPedidoDet=@IdPedidoDet"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If Not lDataTable Is Nothing Then

                    If lDataTable.Rows.Count > 0 Then

                        IdPedidoEnc = lDataTable.Rows(0).Item("IdPedidoEnc")
                        Referencia = IIf(IsDBNull(lDataTable.Rows(0).Item("Referencia")), "", lDataTable.Rows(0).Item("Referencia"))

                    End If

                End If


            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Res_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_res)

        Dim lReturnList As New List(Of clsBeStock_res)

        Try

            Dim vSQL As String = "SELECT * FROM stock_res WHERE IdPedidoDet = @IdPedidoDet"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeStockRes As clsBeStock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeStockRes = New clsBeStock_res
                        clsLnStock_res.Cargar(BeStockRes, lRow)
                        lReturnList.Add(BeStockRes)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Detalle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                           Optional ByVal pConection As SqlConnection = Nothing,
                                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Trans_pe_det WHERE(IdPedidoEnc = @IdPedidoEnc) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", pIdPedidoEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Tiene_Manufactura_Asociada(ByVal IdPedidoEnc As Integer,
                                                      ByVal IdPedidoDet As Integer,
                                                      Optional ByRef pConnection As SqlConnection = Nothing,
                                                      Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing
        Dim Resultado As Integer = 0

        Try

            Dim lCommand As New SqlCommand
            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            Dim Idpic As String = "SELECT count(d.IdPedidoDet) cant
                                   FROM trans_manufactura_enc e INNER JOIN
                                        trans_manufactura_det d on e.IdManufacturaEnc = d.IdManufacturaEnc
                                   WHERE e.IdPedidoEnc = @IdPedidoEnc AND
                                         d.IdPedidoDet = @IdPedidoDet AND
                                         estado NOT IN ('Anulado') "

            If Es_Transaccion_Remota Then
                lCommand = New SqlCommand(Idpic, pConnection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : ltrans = lConnection.BeginTransaction
                lCommand = New SqlCommand(Idpic, lConnection, ltrans) With {.CommandType = CommandType.Text}
            End If

            lCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
            lCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", IdPedidoDet))

            Resultado = lCommand.ExecuteScalar()

            If Not Es_Transaccion_Remota Then ltrans.Commit()

            Return Resultado > 0

        Catch ex As Exception
            If ltrans IsNot Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection IsNot Nothing Then lConnection.Close()
            lConnection.Dispose()
            If ltrans IsNot Nothing Then ltrans.Dispose()
        End Try

    End Function

    Public Shared Function Get_Count_Lines_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lCount As Integer = 0

            Const sp As String = "SELECT count(IdPedidoDet) cant FROM trans_pe_det WHERE IdPedidoEnc=@IdPedidoEnc"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCount = CInt(lReturnValue)
                End If

            End Using

            Return lCount

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_Lines_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As Integer

        Try

            Dim lCount As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Const sp As String = "SELECT count(IdPedidoDet) cant FROM trans_pe_det WHERE IdPedidoEnc=@IdPedidoEnc"

                    Using lCommand As New SqlCommand(sp, lConnection, ltransaction) With {.CommandType = CommandType.Text}
                        lCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lCount = CInt(lReturnValue)
                        End If

                    End Using


                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lCount

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Reservar_Stock_Especifico_Por_Linea(ByVal vDiasVencimientoCliente As Double,
                                                               ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                               ByRef pBeStockResSol As clsBeStock_res,
                                                               ByRef pBeStockEspecifico As clsBeStock,
                                                               ByVal pIdPickingEnc As Integer,
                                                               ByVal MaquinaQueSolicita As String,
                                                               ByVal pIdPropietario As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As Boolean

        Reservar_Stock_Especifico_Por_Linea = False

        Try

            Dim ResultadoInsert As Integer = 0

            If pBePedidoDet.IsNew Then
                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
            Else
                '#EJC20171023_0222PM: No me gusta esta chapusería pero se agregó por cuando modifican una línea existente en el pedido.
                'Ver ref -> '#EJC20171024_1245PM_REF en forma de pedido
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else
                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection, lTransaction)
                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction)
                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidadReservada As Double = 0
                Dim vDifPedidoVrsReservado As Double = 0

                Dim vCantidad_Reservada_By_IdStock As Double = 0
                Dim vCantidadPendienteDeReservar As Double = 0
                Dim vDifPedidoVrsReservadoUMBas As Double = 0
                Dim BePres As New clsBeProducto_Presentacion
                Dim vDifTotalPedidoVrsReservado As Double = 0
                Dim vCantidadSolicitadaUMBas As Double = 0
                Dim vDiferenciaStockALiberar As Double = 0
                Dim vCantReservadaActual As Double = (From p In pBePedidoDet.ListaStockRes).Sum(Function(x) x.Cantidad)
                'segui copiando lo demas creo que ya solo so nlas variables y probas, ya se me va a pagar la maquinas :(
                'De acuerdo
                If pBePedidoDet.IdPresentacion <> 0 Then

                    BePres.IdPresentacion = pBePedidoDet.IdPresentacion
                    clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)

                    pBePedidoDet.Presentacion = BePres

                    If Not BePres Is Nothing Then
                        vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor
                    End If

                Else

                    vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad

                End If

                vDifTotalPedidoVrsReservado = vCantidadSolicitadaUMBas - vCantReservadaActual

                '#EJC202308281221A.
                'El pedido, puede venir en dos variantes:
                '1. Variante 1: El pedido viene en UMBas.
                '2. Variante 2: El pedido viene en Presentación.
                'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion > 0.

                '2. Variante 2 + Caso de uso #2
                If pBePedidoDet.IdPresentacion > 0 AndAlso pBeStockResSol.IdPresentacion > 0 Then
                    If Not BePres Is Nothing Then
                        pBeStockResSol.Cantidad = Math.Round(vDifTotalPedidoVrsReservado / BePres.Factor, 6)
                    End If
                End If

                Select Case vDifTotalPedidoVrsReservado

                        '#EJC20191114:
                        'Se aumentó la cantidad en el pedido, 
                        'Por lo tanto se debe aumentar la cantidad reservada 
                        'y la cantidad en picking (si tiene)
                        'Solo se reversa el excendente (pej. si tenía 4 reservadas y la cantidad cambió a 5, 
                        'Solo se reserva +1
                    Case Is > 0

                        pBeStockResSol.Cantidad = vDifTotalPedidoVrsReservado

                        If clsLnStock_res.Reserva_Stock(pBeStockResSol,
                                                        pIdPropietario,
                                                        vDiasVencimientoCliente,
                                                        MaquinaQueSolicita,
                                                        lConnection,
                                                        lTransaction) Then

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                If Actualiza_Picking_Existente(pBeStockResSol,
                                                               pBePedidoDet,
                                                               pIdPickingEnc,
                                                               lConnection,
                                                               lTransaction) Then

                                    Reservar_Stock_Especifico_Por_Linea = True

                                End If

                            Else
                                Reservar_Stock_Especifico_Por_Linea = True
                            End If

                        End If

                    Case Is < 0 'Se disminuyó la cantidad en el pedido

                        vDifTotalPedidoVrsReservado = Math.Abs(vDifTotalPedidoVrsReservado)

                        For Each Sr In pBePedidoDet.ListaStockRes

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                Throw New Exception("Error de usuario en el proceso: No se puede reducir la cantidad de la línea de pedido porque ya fue asignada a un proceso de picking.")

                                'For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                '    Debug.Print(Pu.IdPickingUbic)
                                'Next

                            Else

                                vCantidad_Reservada_By_IdStock = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(Sr.IdStock,
                                                                                                                       False,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)


                                If vDifTotalPedidoVrsReservado > 0 Then

                                    '#EJC20191114:
                                    'Si la cantidad a restar es menor que la cantidad reservada
                                    'en un IdStockRes (Ej: reservado es 5 y la cantidad se cambió a 4)
                                    'Se debe restar 1 de ese IdStockRes.
                                    If vDifTotalPedidoVrsReservado < Sr.Cantidad Then
                                        'vDiferenciaStockALiberar = Sr.Cantidad - vDifTotalPedidoVrsReservado
                                        Sr.Cantidad -= Math.Abs(vDifTotalPedidoVrsReservado)
                                        clsLnStock_res.Actualizar(Sr, lConnection, lTransaction)
                                    ElseIf vDifTotalPedidoVrsReservado = Sr.Cantidad Then
                                        vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                    ElseIf vDifTotalPedidoVrsReservado > Sr.Cantidad Then
                                        vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                    End If

                                End If

                            End If

                        Next

                    Case Else
                        Exit Select

                End Select

            Else

                If pBePedidoDet.IdPresentacion <> 0 Then
                    pBePedidoDet.Presentacion.IdPresentacion = pBePedidoDet.IdPresentacion
                    clsLnProducto_presentacion.GetSingle(pBePedidoDet.Presentacion, lConnection, lTransaction)
                End If

                If clsLnStock_res.Tiene_StockRes(pBeStockResSol.IdPedido,
                                                 pBeStockResSol.IdPedidoDet,
                                                 lConnection,
                                                 lTransaction) Then

                    If Not clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(pBeStockResSol.IdPedido,
                                                                                                  pBeStockResSol.IdPedidoDet,
                                                                                                  lConnection,
                                                                                                  lTransaction) Then

                        Throw New Exception("No se pudo eliminar el stock reservado antes de reservar de nuevo el stock")

                    End If

                ElseIf Not pBeStockEspecifico Is Nothing Then

                    Debug.Write("Aquí debería eliminar algo")

                End If

                Dim lStockReservo As New List(Of clsBeStock_res)

                If clsLnStock_res.Reserva_Stock_Especifico(pBeStockResSol,
                                                            vDiasVencimientoCliente,
                                                            MaquinaQueSolicita,
                                                            lStockReservo,
                                                            pBeStockEspecifico,
                                                            lConnection,
                                                            lTransaction) Then

                    If pIdPickingEnc > 0 Then

                        If Not lStockReservo Is Nothing Then

                            If Actualiza_Picking_Existente(lStockReservo,
                               pBePedidoDet,
                               pIdPickingEnc,
                               lConnection,
                               lTransaction) Then

                                Reservar_Stock_Especifico_Por_Linea = True

                            End If

                        Else
                            Throw New Exception("Error 20180702: No se pudo actualizar el picking asociado al pedido")
                        End If

                    Else
                        Reservar_Stock_Especifico_Por_Linea = True
                    End If

                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Reservar_Stock_Por_Linea(ByVal vDiasVencimientoCliente As Double,
                                                    ByRef pBePedidoDet As clsBeTrans_pe_det,
                                                    ByRef pBeStockResSol As clsBeStock_res,
                                                    ByVal pIdPickingEnc As Integer,
                                                    ByVal MaquinaQueSolicita As String,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As Boolean

        Dim vIdxPresentacion As Integer = 0
        Reservar_Stock_Por_Linea = False

        Dim BeConfigEnc As New clsBeI_nav_config_enc With {.Idnavconfigenc = 1, .IdPropietario = pIdPropietario}

        Try

            Dim ResultadoInsert As Integer = 0

            Dim lStockReservo As New List(Of clsBeStock_res)
            Dim vCantidadDisponible As Double = 0

            Dim vIdPropietarioBodega As Integer = 0
            vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(pIdPropietario,
                                                                                                                 pIdBodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)
            Dim pEmpresa As clsBeEmpresa
            pEmpresa = New clsBeEmpresa()
            pEmpresa.IdEmpresa = pIdEmpresa
            pEmpresa = clsLnEmpresa.GetSingle(pEmpresa,
                                              lConnection,
                                              lTransaction)

            If Not pEmpresa.Operador_logistico Then
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(pIdBodega,
                                                                                            pIdPropietario,
                                                                                            lConnection,
                                                                                            lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgError As String = "No se obtuvo la configuración de interface Operador_logistico = true IdBodega: " & pIdBodega & " IdPropietario: " & pIdPropietario
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                End If

            Else
                BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega,
                                                                                         pIdEmpresa,
                                                                                         lConnection,
                                                                                         lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgError As String = "No se obtuvo la configuración de interface Operador_logistico = false IdEmpresa: " & pIdEmpresa & " IdPropietario: " & pIdPropietario
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                End If

            End If

            '#EJC202211091834: Parámetro?
            If BeConfigEnc Is Nothing Then
                Throw New Exception("ERROR_202211091833: No se pudo obtener la configuración de la interface.")
            Else
                'MsgBox("Parámetro conservar zona picking: " & BeConfigEnc.Conservar_Zona_Picking_Clavaud)
            End If

            If pBePedidoDet.IsNew Then

                pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)

            Else
                '#EJC20171023_0222PM: No me gusta esta chapusería pero se agregó por cuando modifican una línea existente en el pedido.
                'Ver ref -> '#EJC20171024_1245PM_REF en forma de pedido
                If Not Existe(pBePedidoDet.IdPedidoDet, pBePedidoDet.IdPedidoEnc, lConnection, lTransaction) Then
                    pBePedidoDet.IdPedidoDet = MaxID(lConnection, lTransaction) + 1
                    pBeStockResSol.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    ResultadoInsert = Insertar(pBePedidoDet, lConnection, lTransaction)
                Else

                    pBePedidoDet.ListaStockRes = clsLnStock_res.Get_All_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                       pBePedidoDet.IdPedidoEnc,
                                                                                       lConnection,
                                                                                       lTransaction)

                    pBePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(pBePedidoDet.IdPedidoDet,
                                                                                                               pBePedidoDet.IdPedidoEnc,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                    ResultadoInsert = Actualizar(pBePedidoDet, lConnection, lTransaction)

                End If
            End If

            If Not pBePedidoDet.ListaStockRes Is Nothing AndAlso pBePedidoDet.ListaStockRes.Count > 0 Then

                Dim vCantidad_Reservada_By_IdStock As Double = 0
                Dim vCantidadPendienteDeReservar As Double = 0
                Dim vDifPedidoVrsReservadoUMBas As Double = 0
                Dim BePres As New clsBeProducto_Presentacion
                Dim vDifTotalPedidoVrsReservado As Double = 0
                Dim vCantidadSolicitadaUMBas As Double = 0

                Dim vCantReservadaActual As Double = (From p In pBePedidoDet.ListaStockRes).Sum(Function(x) x.Cantidad)

                If pBePedidoDet.IdPresentacion <> 0 Then

                    Dim vIdPresentacion As Integer = pBePedidoDet.IdPresentacion

                    vIdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If vIdxPresentacion = -1 Then

                        BePres = New clsBeProducto_Presentacion
                        BePres.IdPresentacion = vIdPresentacion
                        clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)
                        lPresentaciones.Add(BePres.Clone())

                    Else
                        BePres = New clsBeProducto_Presentacion
                        BePres = lPresentaciones(vIdxPresentacion).Clone()
                    End If

                    pBePedidoDet.Presentacion = BePres

                    If Not BePres Is Nothing Then
                        vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor
                    End If

                Else

                    vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad

                End If

                vDifTotalPedidoVrsReservado = vCantidadSolicitadaUMBas - vCantReservadaActual

                '#EJC202308281221B.
                'El pedido, puede venir en dos variantes:
                '1. Variante 1: El pedido viene en UMBas.
                '2. Variante 2: El pedido viene en Presentación.
                'Caso de uso #1: El pedido viene en umBas, el objeto pBeStockRes.Idpresentacion = 0.
                'Caso de uso #2: El pedido viene en Presentación, el objeto pBeStockRes.Idpresentacion > 0.

                '2. Variante 2 + Caso de uso #2
                If pBePedidoDet.IdPresentacion > 0 AndAlso pBeStockResSol.IdPresentacion > 0 Then
                    If Not BePres Is Nothing Then
                        pBeStockResSol.Cantidad = Math.Round(vDifTotalPedidoVrsReservado / BePres.Factor, 6)
                    End If
                Else
                    pBeStockResSol.Cantidad = Math.Round(vDifTotalPedidoVrsReservado, 6)
                End If

                Select Case vDifTotalPedidoVrsReservado

                        '#EJC20191114:
                        'Se aumentó la cantidad en el pedido, 
                        'Por lo tanto se debe aumentar la cantidad reservada 
                        'y la cantidad en picking (si tiene)
                        'Solo se reversa el excendente (pej. si tenía 4 reservadas y la cantidad cambió a 5, 
                        'Solo se reserva +1
                    Case Is > 0

                        If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockResSol,
                                                                 vDiasVencimientoCliente,
                                                                 MaquinaQueSolicita,
                                                                 BeConfigEnc,
                                                                 vCantidadDisponible,
                                                                 vIdPropietarioBodega,
                                                                 lStockReservo,
                                                                 lConnection,
                                                                 lTransaction) Then

                            If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                If Actualiza_Picking_Existente(pBeStockResSol,
                                                               pBePedidoDet,
                                                               pIdPickingEnc,
                                                               lConnection,
                                                               lTransaction) Then

                                    Reservar_Stock_Por_Linea = True

                                End If

                            Else
                                Reservar_Stock_Por_Linea = True
                            End If

                        End If

                    Case Is < 0 'Se disminuyó la cantidad en el pedido

                        vDifTotalPedidoVrsReservado = Math.Abs(vDifTotalPedidoVrsReservado)

                        '#EJC20200125: Buscar si ya existe un solo IdStockRes que contenga la cantidad que se solicita...
                        Dim BeStockRes As clsBeStock_res = pBePedidoDet.ListaStockRes.Find(Function(X) X.Cantidad = vCantidadSolicitadaUMBas)

                        If Not BeStockRes Is Nothing Then
                            'Eliminar todos los idstock res diferentes del que tiene la cantidad que quiero.
                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet_Excepto_IdStockRes(pBePedidoDet.IdPedidoDet,
                                                                                                      BeStockRes.IdStockRes,
                                                                                                      lConnection,
                                                                                                      lTransaction)
                            Reservar_Stock_Por_Linea = True
                        Else

                            For Each Sr In pBePedidoDet.ListaStockRes.OrderByDescending(Function(x) x.Cantidad)

                                If Not pBePedidoDet.ListaPickingUbic Is Nothing Then

                                    Throw New Exception("Error de usuario en el proceso: No se puede reducir la cantidad de la línea de pedido porque ya fue asignada a un proceso de picking.")

                                    'For Each Pu In pBePedidoDet.ListaPickingUbic.Where(Function(x) (x.Cantidad_Verificada - x.Cantidad_despachada) > 0)
                                    '    Debug.Print(Pu.IdPickingUbic)
                                    'Next

                                Else

                                    vCantidad_Reservada_By_IdStock = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(Sr.IdStock,
                                                                                                                       False,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)


                                    If vDifTotalPedidoVrsReservado > 0 Then

                                        '#EJC20191114:
                                        'Si la cantidad a restar es menor que la cantidad reservada
                                        'en un IdStockRes (Ej: reservado es 5 y la cantidad se cambió a 4)
                                        'Se debe restar 1 de ese IdStockRes.
                                        If vDifTotalPedidoVrsReservado < Sr.Cantidad Then
                                            'vDifTotalPedidoVrsReservado = Sr.Cantidad - vDifTotalPedidoVrsReservado
                                            Sr.Cantidad -= Math.Abs(vDifTotalPedidoVrsReservado)
                                            clsLnStock_res.Actualizar(Sr, lConnection, lTransaction)
                                        ElseIf vDifTotalPedidoVrsReservado = Sr.Cantidad Then
                                            vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                        ElseIf vDifTotalPedidoVrsReservado > Sr.Cantidad Then
                                            vDifTotalPedidoVrsReservado -= Sr.Cantidad
                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(Sr.IdStockRes, lConnection, lTransaction)
                                        End If

                                    End If

                                End If

                            Next

                        End If

                    Case Else
                        '#EJC20230912: La cantidad no varió la reserva se mantiene sin modificación.
                        Reservar_Stock_Por_Linea = True
                        Exit Select

                End Select

            Else

                If pBePedidoDet.IdPresentacion <> 0 Then

                    Dim vIdPresentacion As Integer = pBePedidoDet.IdPresentacion

                    vIdxPresentacion = lPresentaciones.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If vIdxPresentacion = -1 Then

                        pBePedidoDet.Presentacion = New clsBeProducto_Presentacion
                        pBePedidoDet.Presentacion.IdPresentacion = vIdPresentacion
                        clsLnProducto_presentacion.GetSingle(pBePedidoDet.Presentacion, lConnection, lTransaction)
                        lPresentaciones.Add(pBePedidoDet.Presentacion.Clone())

                    Else
                        pBePedidoDet.Presentacion = New clsBeProducto_Presentacion
                        pBePedidoDet.Presentacion = lPresentaciones(vIdxPresentacion).Clone()
                    End If

                End If

                If clsLnStock_res.Tiene_StockRes(pBeStockResSol.IdPedido,
                                                 pBeStockResSol.IdPedidoDet,
                                                 lConnection,
                                                 lTransaction) Then

                    If Not clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoEnc_And_IdPedidoDet(pBeStockResSol.IdPedido,
                                                                                                  pBeStockResSol.IdPedidoDet,
                                                                                                  lConnection,
                                                                                                  lTransaction) Then

                        Throw New Exception("No se pudo eliminar el stock reservado antes de reservar de nuevo el stock")

                    End If

                End If

                If clsLnStock_res.Reserva_Stock_From_MI3(pBeStockResSol,
                                                         vDiasVencimientoCliente,
                                                         MaquinaQueSolicita,
                                                         BeConfigEnc,
                                                         vCantidadDisponible,
                                                         vIdPropietarioBodega,
                                                         lStockReservo,
                                                         lConnection,
                                                         lTransaction,
                                                         pBePedidoDet.No_linea) Then

                    If pIdPickingEnc > 0 Then

                        If Not lStockReservo Is Nothing Then

                            If Actualiza_Picking_Existente(lStockReservo,
                                                           pBePedidoDet,
                                                           pIdPickingEnc,
                                                           lConnection,
                                                           lTransaction) Then

                                Reservar_Stock_Por_Linea = True

                            End If

                        Else
                            Throw New Exception("Error 20180702: No se pudo actualizar el picking asociado al pedido")
                        End If

                    Else
                        Reservar_Stock_Por_Linea = True
                    End If

                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vSQL As String = ""
        Dim vIdxProducto As Integer = -1

        Try


            vSQL = "SELECT b.nombre AS Bodega, c.nombre_comercial AS Cliente, p.nombre_comercial AS Propietario, enc.fecha_pedido AS 'Fecha Pedido', det.*, pb.IdProducto " &
                       " FROM trans_pe_det AS det " &
                       " INNER JOIN trans_pe_enc AS enc ON det.IdPedidoEnc = enc.IdPedidoEnc " &
                       " INNER JOIN propietario_bodega AS prb ON enc.IdPropietarioBodega = prb.IdPropietarioBodega " &
                       " INNER JOIN propietarios AS p ON prb.IdPropietario = p.IdPropietario " &
                       " INNER JOIN bodega AS b ON enc.IdBodega = b.IdBodega " &
                       " INNER JOIN cliente AS c ON enc.IdCliente = c.IdCliente AND prb.IdPropietario = c.IdPropietario " &
                       " INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega " &
                       " WHERE det.IdPedidoEnc={0}"

            vSQL = String.Format(vSQL, pIdPedidoEnc)

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_pe_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_pe_det

                        With Obj

                            'Obj.Bodega = IIf(IsDBNull(lRow.Item("Bodega")), String.Empty, lRow.Item("Bodega"))
                            'Obj.Cliente = IIf(IsDBNull(lRow.Item("Cliente")), String.Empty, lRow.Item("Cliente"))
                            'Obj.Propietario = IIf(IsDBNull(lRow.Item("Propietario")), String.Empty, lRow.Item("Propietario"))
                            'Obj.FechaPedido = IIf(IsDBNull(lRow.Item("Fecha Pedido")), Date.Now, lRow.Item("Fecha Pedido"))

                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                            vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = .Producto.IdProducto)

                            If vIdxProducto = -1 Then

                                .Producto = New clsBeProducto()
                                .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))
                                clsLnProducto.Obtener(.Producto, lConnection, lTransaction)
                                lProductosInMemory.Add(.Producto.Clone())

                            Else
                                .Producto = lProductosInMemory(vIdxProducto).Clone()
                            End If


                            .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                            .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                            .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                            .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                            .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                            .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                            .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                            .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                            .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                            .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                            .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                            .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                            .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                            .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                            .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

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

    Public Shared Function Get_All_Despacho_Det_By_IdPedidoEnc(ByVal pIdDespachoEnc As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim vSQL As String = ""

        Try

            vSQL = "SELECT p.codigo, p.nombre, pp.nombre AS presentacion, 
                    pe.nombre AS NomEstado, 
                    um.Nombre AS unidadmedida, 
                    pr.nombre_comercial AS propietario, 
                    bu.descripcion AS bodegaubicacion, 
                    ISNULL(s.cantidad,0) AS CantidadSF, 
                    pp.factor, ISNULL(ISNULL(s.cantidad,0) / pp.factor,0) AS Cantidad, 
                    ubic.IdStockRes, 
                    det.IdPedidoEnc IdTransaccion, 
                    'PED' Indicador, 
                    det.IdPedidoDet, 
                    ubic.IdStock, 
                    ubic.IdPropietarioBodega, 
                    det.IdProductoBodega, 
                    ubic.IdUbicacion, 
                    det.IdProductoEstado, 
                    det.IdPresentacion, 
                    ubic.IdUnidadMedida, 
                    ubic.lote, 
                    ubic.lic_plate, 
                    ubic.serial, 
                    SUM(ISNULL(ubic.cantidad_solicitada, 0)) As CantidadReservada,  
                    ubic.peso_solicitado peso, 
                    det.IdProductoEstado estado, 
                    ubic.fecha_vence, 
                    Null uds_lic_plate, 
                    ubic.IdUbicacionAnterior AS IdUbicacion_anterior, 
                    Null no_bulto, 
                    ubic.IdRecepcion AS IdRecepcionEnc, 
                    ubic.IdPickingEnc IdPicking, 
                    det.IdPedidoEnc IdPedido, 
                    det.IdDespachoEnc IdDespacho, 
                    s.añada, 
                    s.fecha_manufactura, 
                    SUM(ISNULL(ubic.peso_recibido, 0)) AS Peso_Recibido, 
                    SUM(ISNULL(ubic.peso_verificado, 0)) AS Peso_Verificado, 
                    ISNULL(ubic.acepto, 0) AS Acepto, 
                    SUM(ISNULL(ubic.cantidad_recibida, 0)) AS cantidad_recibida, 
                    SUM(ISNULL(ubic.cantidad_verificada, 0)) AS cantidad_verificada, 
                    SUM(ISNULL(det.CantidadDespachada,0)) AS Cantidad_Despachada, 
                    ISNULL(ubic.encontrado, 0) AS Encontrado, 
                    dbo.Nombre_Completo_Ubicacion(ubic.IdUbicacion, ubic.IDBODEGA) AS NomUbic,
                    s.IdRecepcionEnc, s.IdRecepcionDet
                    FROM trans_despacho_det det INNER JOIN
                         trans_picking_ubic ubic ON det.IdPedidoEnc = ubic.IdPedidoEnc AND 
	                     det.IdPedidoDet = ubic.IdPedidoDet AND
	                     det.IdPickingUbic = ubic.IdPickingUbic INNER JOIN
	                     producto_bodega pb ON pb.IdProductoBodega = det.IdProductoBodega INNER JOIN
	                     producto p ON p.IdProducto = pb.IdProducto INNER JOIN
 	                     producto_estado pe ON det.IdProductoEstado = pe.IdEstado INNER JOIN
	                     unidad_medida um ON um.IdUnidadMedida = det.IdUnidadMedidaBasica INNER JOIN
	                     propietarios pr ON pr.IdPropietario = p.IdPropietario INNER JOIN 
	                     bodega_ubicacion bu ON bu.IdUbicacion = ubic.IdUbicacion AND bu.IdBodega = ubic.IdBodega LEFT JOIN
	                     producto_presentacion AS pp ON det.IdPresentacion = pp.IdPresentacion LEFT JOIN
	                     stock s ON s.IdStock = ubic.IdStock
                    WHERE(det.IdDespachoEnc = @IdDespachoEnc) 
                    GROUP BY p.codigo, p.nombre, pp.nombre, pe.nombre, um.Nombre, pr.nombre_comercial,
                             s.cantidad, pp.factor, s.cantidad / pp.factor, ubic.IdStockRes, 
                             det.IdPedidoDet, ubic.IdStock, ubic.IdPropietarioBodega, det.IdProductoBodega, ubic.IdUbicacion,
		                     ubic.IdProductoEstado, det.IdPresentacion, ubic.IdUnidadMedida, ubic.lote, ubic.lic_plate, 
		                     ubic.serial, ubic.cantidad_solicitada,ubic.peso_solicitado,det.IdProductoEstado, ubic.fecha_vence,
		                     ubic.IdUbicacionAnterior, ubic.IdRecepcion, ubic.IdPickingEnc, det.IdPedidoEnc, det.IdDespachoEnc,
		                     ubic.acepto, ubic.encontrado, ubic.IdUbicacion, ubic.IdBodega, s.IdRecepcionEnc, bu.descripcion,
		                     s.añada,s.fecha_manufactura, s.IdRecepcionDet"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdDespachoEnc", pIdDespachoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.Ubicacion_Nombre = IIf(IsDBNull(lRow("NomUbic")), "", lRow("NomUbic"))
                                Obj.UbicacionActual.NombreCompleto = Obj.Ubicacion_Nombre

                                If lRow("encontrado") IsNot DBNull.Value AndAlso lRow("encontrado") IsNot Nothing Then
                                    Obj.encontrado = CType(lRow("encontrado"), Boolean)
                                End If

                                If lRow("acepto") IsNot DBNull.Value AndAlso lRow("acepto") IsNot Nothing Then
                                    Obj.acepto = CType(lRow("acepto"), Boolean)
                                End If

                                If lRow("peso_recibido") IsNot DBNull.Value AndAlso lRow("peso_recibido") IsNot Nothing Then
                                    Obj.peso_pickeado = CType(lRow("peso_recibido"), Double)
                                End If

                                If lRow("peso_verificado") IsNot DBNull.Value AndAlso lRow("peso_verificado") IsNot Nothing Then
                                    Obj.peso_verificado = CType(lRow("peso_verificado"), Double)
                                End If

                                If lRow("cantidad_recibida") IsNot DBNull.Value AndAlso lRow("cantidad_recibida") IsNot Nothing Then
                                    Obj.Cantidad_Pickeada = CType(lRow("cantidad_recibida"), Double)
                                End If

                                If lRow("cantidad_verificada") IsNot DBNull.Value AndAlso lRow("cantidad_verificada") IsNot Nothing Then
                                    Obj.Cantidad_Verificada = CType(lRow("cantidad_verificada"), Double)
                                End If

                                If lRow("cantidad_despachada") IsNot DBNull.Value AndAlso lRow("cantidad_despachada") IsNot Nothing Then
                                    Obj.Cantidad_Despachada = CType(lRow("cantidad_despachada"), Double)
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

    '#AT20250219 Funcion para obtener el detalle por codigo y lista de pedidos
    Public Shared Function Get_Detalle_By_IdPedidoEnc_And_Producto_Consolidado(ByVal pProducto As String, ByVal pListaPedidos As String) As clsBeDetallePedidoAVerificar
        Get_Detalle_By_IdPedidoEnc_And_Producto_Consolidado = Nothing

        Try
            Dim Obj As clsBeDetallePedidoAVerificar = Nothing

            If pListaPedidos.StartsWith("[") AndAlso pListaPedidos.EndsWith("]") Then
                pListaPedidos = pListaPedidos.Trim("["c, "]"c)
            End If

            Dim listaIds As List(Of String) = pListaPedidos.Split(","c).Select(Function(s) s.Trim()).ToList()
            Dim parametros As String = String.Join(",", listaIds.Select(Function(id, index) "@Pedidos" & index))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Dim vSQL As String = "SELECT TOP(1) * FROM VW_Verificacion_Consolidada 
                                          WHERE IdPedidoEnc IN (" & parametros & ") AND 
                                                codigo = @Producto AND 
                                                cantidad_verificada <> cantidad_recibida AND 
                                                cantidad_recibida <> 0"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Producto", pProducto)

                        For i As Integer = 0 To listaIds.Count - 1
                            lDTA.SelectCommand.Parameters.AddWithValue("@Pedidos" & i, listaIds(i))
                        Next

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeDetallePedidoAVerificar
                                clsLnDetallePedidoAVerificar.Cargar(Obj, lRow)
                            Next
                        End If
                    End Using

                    ltransaction.Commit()
                End Using

                lConnection.Close()
            End Using

            Return Obj

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '#CKFK20250227 Creé esta función para cargar los pedidos sin picking
    Public Shared Function Get_Detalle_By_IdPedidoEnc_Sin_Picking(ByVal pIdPedidoEnc As Integer,
                                                                  ByVal pEstadoPedido As String,
                                                                  ByRef pIdDespachoEnc As Integer,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_pe_det)

        Dim lReturnList As New List(Of clsBeTrans_pe_det)
        Dim vIdxProducto As Integer = -1
        Dim lSubListaProductosInMemory As New List(Of clsBeProducto)
        Dim vlSubListaProductosInMemory As New List(Of clsBeProducto)

        Try

            Dim vSQL As String = " SELECT det.*, pb.IdProducto FROM trans_pe_det det
                                    INNER JOIN producto_bodega AS pb ON det.IdProductoBodega = pb.IdProductoBodega
                                    WHERE det.IdPedidoEnc= @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BePedidoDet As clsBeTrans_pe_det

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lSubListaProductosInMemory = clsLnProducto.Get_All_By_IdPedidoEnc(pIdPedidoEnc)

                    If Not lSubListaProductosInMemory Is Nothing Then

                        If Not lProductosInMemory Is Nothing Then
                            If lProductosInMemory.Count > 0 Then
                                vlSubListaProductosInMemory = lSubListaProductosInMemory.Except(lProductosInMemory).ToList()
                            End If
                        End If

                        If Not vlSubListaProductosInMemory Is Nothing Then
                            lProductosInMemory.AddRange(vlSubListaProductosInMemory)
                        End If

                    End If

                    For Each lRow As DataRow In lDataTable.Rows

                        BePedidoDet = New clsBeTrans_pe_det

                        With BePedidoDet

                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .ProductoBodega.IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .Producto.IdProducto = IIf(IsDBNull(lRow.Item("IdProducto")), 0, lRow.Item("IdProducto"))

                            Dim vIdProducto As Integer = .Producto.IdProducto
                            vIdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = vIdProducto)

                            If vIdxProducto = -1 Then

                                Dim BeProducto = New clsBeProducto()
                                Dim pCampos(6) As clsBeProducto.ProdPropiedades
                                pCampos(0) = clsBeProducto.ProdPropiedades.Codigo
                                pCampos(1) = clsBeProducto.ProdPropiedades.Nombre
                                pCampos(2) = clsBeProducto.ProdPropiedades.Control_lote
                                pCampos(3) = clsBeProducto.ProdPropiedades.Control_Peso
                                pCampos(4) = clsBeProducto.ProdPropiedades.Control_vencimiento
                                pCampos(5) = clsBeProducto.ProdPropiedades.Codigos_Barra
                                BeProducto.IdProducto = vIdProducto
                                BeProducto = clsLnProducto.GetSingle(vIdProducto,
                                                                     pCampos,
                                                                     lConnection,
                                                                     lTransaction)
                                lProductosInMemory.Add(BeProducto.Clone())
                                .Producto = BeProducto

                            Else
                                Dim BeProducto = New clsBeProducto()
                                BeProducto = lProductosInMemory(vIdxProducto).Clone()
                                .Producto = BeProducto
                            End If

                            .IdEstado = IIf(IsDBNull(lRow.Item("IdEstado")), 0, lRow.Item("IdEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedidaBasica = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .Cantidad = IIf(IsDBNull(lRow.Item("Cantidad")), 0.0, lRow.Item("Cantidad"))
                            .Peso = IIf(IsDBNull(lRow.Item("Peso")), 0.0, lRow.Item("Peso"))
                            .Precio = IIf(IsDBNull(lRow.Item("Precio")), 0.0, lRow.Item("Precio"))
                            .No_recepcion = IIf(IsDBNull(lRow.Item("no_recepcion")), 0, lRow.Item("no_recepcion"))
                            .Ndias = IIf(IsDBNull(lRow.Item("ndias")), 0, lRow.Item("ndias"))
                            .Cant_despachada = IIf(IsDBNull(lRow.Item("cant_despachada")), 0.0, lRow.Item("cant_despachada"))
                            .Codigo_Producto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .Nombre_producto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .Nom_presentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .Nom_unid_med = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .Nom_estado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .User_agr = IIf(IsDBNull(lRow.Item("user_agr")), "", lRow.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(lRow.Item("fec_agr")), Date.Now, lRow.Item("fec_agr"))
                            .Fecha_especifica = IIf(IsDBNull(lRow.Item("fecha_especifica")), False, lRow.Item("fecha_especifica"))
                            .RoadDes = IIf(IsDBNull(lRow.Item("RoadDes")), 0.0, lRow.Item("RoadDes"))
                            .RoadDesMon = IIf(IsDBNull(lRow.Item("RoadDesMon")), 0.0, lRow.Item("RoadDesMon"))
                            .RoadTotal = IIf(IsDBNull(lRow.Item("RoadTotal")), 0.0, lRow.Item("RoadTotal"))
                            .RoadPrecioDoc = IIf(IsDBNull(lRow.Item("RoadPrecioDoc")), 0.0, lRow.Item("RoadPrecioDoc"))
                            .RoadVAL1 = IIf(IsDBNull(lRow.Item("RoadVAL1")), 0.0, lRow.Item("RoadVAL1"))
                            .RoadVAL2 = IIf(IsDBNull(lRow.Item("RoadVAL2")), "", lRow.Item("RoadVAL2"))
                            .RoadCantProc = IIf(IsDBNull(lRow.Item("RoadCantProc")), 0.0, lRow.Item("RoadCantProc"))

                            '#EJC20180114: Agruegué No_Linea y Atributo_Variante_1 en GetByPedidoEnc
                            .No_linea = IIf(IsDBNull(lRow.Item("No_linea")), 0.0, lRow.Item("No_linea"))
                            .Atributo_Variante_1 = IIf(IsDBNull(lRow.Item("Atributo_Variante_1")), 0.0, lRow.Item("Atributo_Variante_1"))
                            .IdStockEspecifico = IIf(IsDBNull(lRow.Item("IdStockEspecifico")), 0, lRow.Item("IdStockEspecifico"))

                            'If pEstadoPedido = "Despachado" Then

                            '    Dim lSubListaPickingUbic As New List(Of clsBeTrans_picking_ubic)

                            '    lSubListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_Despachado_By_IdPedidoEnc(pIdPedidoEnc,
                            '                                                                                                 lConnection,
                            '                                                                                                 lTransaction)
                            '    If Not lSubListaPickingUbic Is Nothing Then

                            '        .ListaPickingUbic = lSubListaPickingUbic.FindAll(Function(x) x.IdPedidoEnc = pIdPedidoEnc AndAlso x.IdPedidoDet = .IdPedidoDet)

                            '    End If

                            'End If

                        End With

                        lReturnList.Add(BePedidoDet)

                        Application.DoEvents()

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_BePresentacion_By_NoLinea(No_Linea As Integer,
                                                         IdPedidoEnc As Integer,
                                                         lConnection As SqlConnection,
                                                         lTransaction As SqlTransaction) As clsBeProducto_Presentacion

        Get_BePresentacion_By_NoLinea = Nothing

        Try

            Const sp As String = "SELECT pp.*  
                                  FROM trans_pe_det p INNER JOIN 
                                       producto_presentacion pp ON p.IdPresentacion = pp.IdPresentacion  
                                  WHERE(p.No_Linea = @No_Linea AND p.IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO_LINEA", No_Linea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDOENC", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim vPresentacion As New clsBeProducto_Presentacion
                clsLnProducto_presentacion.Cargar(vPresentacion, dt.Rows(0))
                Get_BePresentacion_By_NoLinea = vPresentacion
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Res_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                            ByVal IdPedidoEnc As Integer,
                                                            ByVal pConnection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction) As List(Of clsBeStock_res)

        Dim lReturnList As New List(Of clsBeStock_res)

        Try

            Dim vSQL As String = "SELECT * FROM stock_res WHERE IdPedidoDet = @IdPedidoDet AND IdPedido = @IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoDet", pIdPedidoDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeStock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeStock_res
                        clsLnStock_res.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class