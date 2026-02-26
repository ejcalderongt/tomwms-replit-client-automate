Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_picking_ubic

    '#CKFK 20180405 04:16 PM Se agregó este Sub para mostrar los picking ubic de un pedido despachado
    Public Shared Sub Cargar_For_Despacho(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, ByRef dr As DataRow)

        Try

            With oBeTrans_picking_ubic

                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdPickingUbic = IIf(IsDBNull(dr.Item("IdPickingUbic")), 0, dr.Item("IdPickingUbic"))
                .IdPickingDet = IIf(IsDBNull(dr.Item("IdPickingDet")), 0, dr.Item("IdPickingDet"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet")) '#CKFK 20180331 Agregué el IdPedidoDet en el cargar porque se estaba quedando con valor 0
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacionAnterior = IIf(IsDBNull(dr.Item("IdUbicacionAnterior")), 0, dr.Item("IdUbicacionAnterior"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Fecha_minima = IIf(IsDBNull(dr.Item("fecha_minima")), Date.Now, dr.Item("fecha_minima"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Acepto = IIf(IsDBNull(dr.Item("acepto")), False, dr.Item("acepto"))
                .Peso_solicitado = IIf(IsDBNull(dr.Item("peso_solicitado")), 0.0, dr.Item("peso_solicitado"))
                .Peso_recibido = IIf(IsDBNull(dr.Item("peso_recibido")), 0.0, dr.Item("peso_recibido"))
                .Peso_verificado = IIf(IsDBNull(dr.Item("peso_verificado")), 0.0, dr.Item("peso_verificado"))
                .Peso_despachado = IIf(IsDBNull(dr.Item("peso_despachado")), 0.0, dr.Item("peso_despachado"))
                .Cantidad_Solicitada = IIf(IsDBNull(dr.Item("cantidad_solicitada")), 0.0, dr.Item("cantidad_solicitada"))
                .Cantidad_Recibida = IIf(IsDBNull(dr.Item("cantidad_recibida")), 0.0, dr.Item("cantidad_recibida"))
                .Cantidad_Verificada = IIf(IsDBNull(dr.Item("cantidad_verificada")), 0.0, dr.Item("cantidad_verificada"))
                .Encontrado = IIf(IsDBNull(dr.Item("encontrado")), False, dr.Item("encontrado"))
                .Dañado_verificacion = IIf(IsDBNull(dr.Item("dañado_verificacion")), False, dr.Item("dañado_verificacion"))
                .Fecha_real_vence = IIf(IsDBNull(dr.Item("fecha_real_vence")), Date.Now, dr.Item("fecha_real_vence"))
                .No_packing = IIf(IsDBNull(dr.Item("no_packing")), "", dr.Item("no_packing"))
                .Fecha_picking = IIf(IsDBNull(dr.Item("fecha_picking")), Date.Now, dr.Item("fecha_picking"))
                .Fecha_verificado = IIf(IsDBNull(dr.Item("fecha_verificado")), Date.Now, dr.Item("fecha_verificado"))
                .Fecha_packing = IIf(IsDBNull(dr.Item("fecha_packing")), Date.Now, dr.Item("fecha_packing"))
                .Fecha_despachado = IIf(IsDBNull(dr.Item("fecha_despachado")), Date.Now, dr.Item("fecha_despachado"))
                .Cantidad_despachada = IIf(IsDBNull(dr.Item("cantidad_despachada")), 0.0, dr.Item("cantidad_despachada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Dañado_picking = IIf(IsDBNull(dr.Item("dañado_picking")), False, dr.Item("dañado_picking"))
                .IdUbicacionTemporal = IIf(IsDBNull(dr.Item("IdUbicacionTemporal")), 0, dr.Item("IdUbicacionTemporal"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPedidoDet:=oBeTrans_picking_ubic.IdPedidoDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_ubic.IdPedidoEnc,
                                                  pIdPickingEnc:=oBeTrans_picking_ubic.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_ubic.IdPickingDet,
                                                  pIdPickingUbic:=oBeTrans_picking_ubic.IdPickingUbic,
                                                  pCodigoProducto:=oBeTrans_picking_ubic.CodigoProducto,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Picking_Ubicacion(ByVal pActivo As Boolean,
                                               ByVal pFechaDel As Date,
                                               ByVal pFechaAl As Date,
                                               ByVal pIdPropietarioBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbicacion WHERE 1 > 0  "

            If pActivo = True Then
                vSQL += " AND activo=1"
            Else
                vSQL += " AND activo=0"
            End If

            If pIdPropietarioBodega > 0 Then
                vSQL += " AND IdPropietarioBodega=" & pIdPropietarioBodega
            End If

            vSQL += String.Format(" AND cast([Fecha Picking] AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
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

    Public Shared Function Get_Progreso_Picking(ByVal pFechaDel As Date,
                                                ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Progreso_Picking_By_Operador WHERE 1 > 0  "

            vSQL += String.Format(" AND CAST(Fecha_Agregado AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
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

    Public Shared Function Get_Picking_Ubic_By_IdPicking(ByVal pIdPicking As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim BeListPicking As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT * FROM trans_picking_ubic WHERE IdPickingEnc =" & pIdPicking & " AND fecha_picking > '19000101' "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        Dim lDT As New DataTable
                        lDataAdapter.Fill(lDT)

                        Dim BeTransPickingUbic As clsBeTrans_picking_ubic

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDT.Rows

                                BeTransPickingUbic = New clsBeTrans_picking_ubic
                                Cargar(BeTransPickingUbic, lRow)

                                BeTransPickingUbic.ProductoUnidadMedida = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(BeTransPickingUbic.IdUnidadMedida,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)
                                If BeTransPickingUbic.IdPresentacion <> 0 Then
                                    BeTransPickingUbic.ProductoPresentacion = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(BeTransPickingUbic.IdPresentacion,
                                                                                                                                                    lConnection,
                                                                                                                                                    lTransaction)
                                Else
                                    BeTransPickingUbic.ProductoPresentacion = ""
                                End If

                                BeTransPickingUbic.IdProducto = clsLnProducto_bodega.Get_BeProducto_By_IdProductoBodega(BeTransPickingUbic.IdProductoBodega,
                                                                                                                        lConnection,
                                                                                                                        lTransaction).IdProducto

                                BeTransPickingUbic.CodigoProducto = clsLnProducto.Get_CodigoProducto_By_IdProducto(BeTransPickingUbic.IdProducto,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)
                                BeListPicking.Add(BeTransPickingUbic)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return BeListPicking

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(ByVal pIdPicking As Integer,
                                                                              ByVal pIdPedidoEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")
        Dim lDataTable As New DataTable

        Try

            Dim vSQL As String = String.Empty

            If pIdPicking > 0 Then
                vSQL = "SELECT * FROM VW_UbicacionPicking WHERE IdPickingEnc=" & pIdPicking
            ElseIf pIdPedidoEnc > 0 Then
                vSQL = "SELECT * FROM VW_UbicacionPicking WHERE IdPedidoEnc=" & pIdPedidoEnc
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
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

    Public Shared Function Get_All_PickingUbic_Despachado_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                                         ByVal pIdDespachoEnc As Integer,
                                                                         ByVal pIdPickingEnc As Integer,
                                                                         ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_Despachado_By_IdPedidoDet = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BeBodega As New clsBeBodega

        Try

            '#CKFK20220719 Creé una nueva vista antes era esta VW_PickingUbic_Despachado_By_IdPedidoDet, porque generaba duplicados
            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_Desp_By_IdPedidoDet
                                  WHERE IdPedidoDet = @IdPedidoDet 
                                  AND dañado_picking=0 
                                  AND dañado_verificacion=0 
                                  AND no_encontrado = 0 "

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)

            If Not BeBodega Is Nothing Then
                '#CKFK20221205 Agregué el ordenamiento por tramo
                If BeBodega.Ordenar_Por_Nombre_Completo Then
                    vSQL += " ORDER BY Nombre_Ubicacion "
                    If BeBodega.Ordenar_Picking_Descendente Then
                        vSQL += " desc "
                    End If
                Else
                    vSQL += " ORDER BY IdPedidoEnc "
                End If
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", pIdPedidoDet))
                        'lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdDespachoEnc", pIdDespachoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            lReturnList = New List(Of clsBeTrans_picking_ubic)

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar_For_Despacho(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    .Ubicacion.IdBodega = pIdBodega
                                    'clsLnBodega_ubicacion.Obtener_For_Picking(.Ubicacion)
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("Nombre_Ubicacion")), 0, lRow.Item("Nombre_Ubicacion"))

                                    .IdPickingEnc = pIdPickingEnc
                                    .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                                    .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo")), "", lRow.Item("codigo"))
                                    .NombreProducto = IIf(IsDBNull(lRow.Item("nombre")), "", lRow.Item("nombre"))
                                    .ProductoPresentacion = IIf(IsDBNull(lRow.Item("Presentacion")), "", lRow.Item("Presentacion"))
                                    .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("UnidadMedida")), "", lRow.Item("UnidadMedida"))
                                    .ProductoEstado = IIf(IsDBNull(lRow.Item("NomEstado")), "", lRow.Item("NomEstado"))
                                    .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                                    .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                                    .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                                    .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                                    '.IdStockRes = Se pierde porque se elima el stock_res al realizar el despacho.
                                    .IdStock = IIf(IsDBNull(lRow.Item("IdStock")), 0, lRow.Item("IdStock"))
                                    .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                                    .IsNew = False

                                End With

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_Despachado_By_IdPedidoDet(ByVal pIdPedidoEnc As Integer,
                                                                        ByVal pIdPedidoDet As Integer,
                                                                         ByVal pIdDespachoEnc As Integer,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_Despachado_By_IdPedidoDet = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_Desp_By_IdPedidoDet
                                  WHERE IdPedidoDet = @IdPedidoDet 
                                  AND IdPedidoEnc = @IdPedidoEnc 
                                  AND dañado_picking=0 
                                  AND dañado_verificacion=0 
                                  AND no_encontrado = 0"
            vSQL += " ORDER BY IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", pIdPedidoDet))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar_For_Despacho(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("Nombre_Ubicacion")), 0, lRow.Item("Nombre_Ubicacion"))
                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo")), "", lRow.Item("codigo"))
                            .NombreProducto = IIf(IsDBNull(lRow.Item("nombre")), "", lRow.Item("nombre"))
                            .ProductoPresentacion = IIf(IsDBNull(lRow.Item("Presentacion")), "", lRow.Item("Presentacion"))
                            .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("UnidadMedida")), "", lRow.Item("UnidadMedida"))
                            .ProductoEstado = IIf(IsDBNull(lRow.Item("NomEstado")), "", lRow.Item("NomEstado"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
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

    Public Shared Function Get_All_PickingUbic_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                              ByVal pIdPedidoEnc As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPedidoDet = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BeBodega As New clsBeBodega

        Try

            Dim vSQL As String = " SELECT * FROM VW_PickingUbic_By_IdPedidoDet 
                                   WHERE IdPedidoDet = @IdPedidoDet AND  
                                         IdPedidoEnc = @IdPedidoEnc AND  
                                         dañado_picking = 0 AND dañado_verificacion = 0 AND no_encontrado = 0 AND IdStockRes is not null "

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, pConnection, pTransaction)

            If Not BeBodega Is Nothing Then
                '#CKFK20221205 Agregué el ordenamiento por tramo
                If BeBodega.Ordenar_Por_Nombre_Completo Then
                    vSQL += " ORDER BY Nombre_Ubicacion "
                    If BeBodega.Ordenar_Picking_Descendente Then
                        vSQL += " desc "
                    End If
                Else
                    vSQL += " ORDER BY IdPedidoEnc "
                End If
            End If

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", pIdPedidoDet))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
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

    Public Shared Function Get_All_PickingUbic_By_IdPickingDet(ByVal pIdPickingDet As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE IdPickingDet=@IdPickingDet AND no_encontrado = 0 and dañado_picking = 0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pIdPickingDet)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                               ByVal pDetalleOperador As Boolean,
                                                               ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                  WHERE (IdPickingEnc = @IdPickingEnc AND dañado_picking = 0 AND no_encontrado = 0) 
                                  AND CANTIDAD_RECIBIDA < CANTIDAD_SOLICITADA "

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega = @IdOperadorBodega"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                        If pDetalleOperador Then
                            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                                    .IdPickingEnc = pIdPickingEnc
                                    .IsNew = False

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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(ByVal pIdPickingEnc As Integer,
                                                                               ByVal pDetalleOperador As Boolean,
                                                                               ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                  WHERE (IdPickingEnc = @IdPickingEnc AND dañado_picking = 0 AND no_encontrado = 0 AND dañado_verificacion = 0) 
                                  AND CANTIDAD_VERIFICADA < CANTIDAD_RECIBIDA "

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega = @IdOperadorBodega"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                        If pDetalleOperador Then
                            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                                    .IdPickingEnc = pIdPickingEnc
                                    .IsNew = False

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

    '#AT20241206 Agregue el parametro pIdPedidoEnc
    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(ByVal pIdPickingEnc As Integer,
                                                                                ByVal pDetalleOperador As Boolean,
                                                                                ByVal pIdOperadorBodega As Integer,
                                                                                ByVal pIdPedidoEnc As Integer,
                                                                                ByRef lConnection As SqlConnection,
                                                                                ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                  WHERE (IdPickingEnc = @IdPickingEnc AND dañado_picking = 0 AND no_encontrado = 0 AND dañado_verificacion = 0) 
                                  AND CANTIDAD_VERIFICADA < CANTIDAD_RECIBIDA "

            If pIdPedidoEnc <> 0 Then
                vSQL += " AND IdPedidoEnc = @IdPedidoEnc"
            End If

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega = @IdOperadorBodega"
            End If


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pIdPedidoEnc <> 0 Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))
                End If

                If pDetalleOperador Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                            .IdPickingEnc = pIdPickingEnc
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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_Detallado(ByVal pIdPickingEnc As Integer,
                                                                         ByVal pDetalleOperador As Boolean,
                                                                         ByVal pIdOperadorBodega As Integer,
                                                                         ByRef lConnection As SqlConnection,
                                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim countReg As Integer = 0
        Dim vSQL As String = ""

        Try

            '#CKFK20250311 Agregué el dañado_verificacion = 0
            vSQL = "SELECT  * FROM VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado 
		                WHERE IdPickingEnc = @IdPickingEnc 
                        AND dañado_picking = 0 
                        AND cantidad_solicitada <> cantidad_recibida 
                        AND no_encontrado = 0 AND dañado_verificacion = 0 "

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega = @IdOperadorBodega"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pDetalleOperador Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        Obj.Ubicacion.Nivel = lRow.Item("nivel")
                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                            .IdPickingEnc = pIdPickingEnc
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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_Consolidado(ByVal pIdPickingEnc As Integer,
                                                                           ByVal pDetalleOperador As Boolean,
                                                                           ByVal pIdOperadorBodega As Integer,
                                                                           ByRef lConnection As SqlConnection,
                                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim lReturnListCons As New List(Of clsBeTrans_picking_ubic)
        Dim countReg As Integer = 0
        Dim vSQL As String = ""

        Try

            vSQL = "SELECT  * FROM VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado 
		                WHERE IdPickingEnc = @IdPickingEnc 
                        AND dañado_picking = 0 
                        AND cantidad_solicitada <> cantidad_recibida 
                        AND no_encontrado = 0"

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega_Pickeo = @IdOperadorBodega"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pDetalleOperador Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        Obj.Ubicacion.Nivel = lRow.Item("nivel")

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                            .IdPickingEnc = pIdPickingEnc
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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                               ByVal pIdBodega As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim BeBodega As New clsBeBodega

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                 WHERE (IdPickingEnc = @IdPickingEnc 
                                 AND dañado_picking = 0 and no_encontrado = 0 AND dañado_verificacion = 0) "

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, lConnection, lTransaction)

            If Not BeBodega Is Nothing Then
                '#CKFK20221205 Agregué el ordenamiento por tramo
                If BeBodega.Ordenar_Por_Nombre_Completo Then
                    vSQL += " ORDER BY NombreUbicacion "
                    If BeBodega.Ordenar_Picking_Descendente Then
                        vSQL += " desc "
                    End If
                Else
                    vSQL += " ORDER BY IdPedidoEnc "
                End If
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransPickingUbic As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransPickingUbic = New clsBeTrans_picking_ubic()

                        Cargar(BeTransPickingUbic, lRow)

                        BeTransPickingUbic.Ubicacion.IdBodega = lRow.Item("IdBodega")
                        BeTransPickingUbic.Ubicacion.IdUbicacion = lRow.Item("IdUbicacion")

                        With BeTransPickingUbic

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), 0, lRow.Item("NombreUbicacion")) 'Obj.Ubicacion.Descripcion
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
                            .IdPickingEnc = pIdPickingEnc
                            .IsNew = False
                            .No_encontrado = IIf(IsDBNull(lRow.Item("no_encontrado")), False, lRow.Item("no_encontrado"))
                            '#EJC20220304: Debe venir en la vista, join de producto - Clasificación.
                            .NombreClasificacion = IIf(IsDBNull(lRow.Item("Clasificacion")), "", lRow.Item("Clasificacion"))
                            .IdProductoTallaColor = IIf(IsDBNull(lRow.Item("IdProductoTallaColor")), 0, lRow.Item("IdProductoTallaColor"))
                            .Codigo_Talla = IIf(IsDBNull(lRow.Item("Talla")), "", lRow.Item("Talla"))
                            .Codigo_Color = IIf(IsDBNull(lRow.Item("Color")), "", lRow.Item("Color"))

                        End With

                        lReturnList.Add(BeTransPickingUbic)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc(ByVal pIdPickingEnc As Integer,
                                                                               ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '#CKFK 20180402 06:42 PM Agregué esta condición  " and pu.IdStockRes = sr.IdStockRes " para evitar la duplicidad de registros
                    '#CKFK 20210714 1337 Agregué la función dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion,penc.IdBodega) AS nom_ubicacion por error en la relacion
                    Dim vSQL As String = "SELECT pu.*,pdet.IdPedidoEnc,pdet.IdPedidoDet,pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, 
                         pdet.codigo_producto, pdet.nombre_producto,pdet.nom_presentacion, pdet.nom_unid_med,pdet.nom_estado,
                         pdet.IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes, sr.IdStock,
	                     dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion,penc.IdBodega) AS nom_ubicacion
                         FROM trans_picking_ubic pu  
                         INNER JOIN trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet 
                         INNER JOIN trans_pe_det As pdet On pdet.IdPedidoDet = pkdet.IdPedidoDet  
                         INNER JOIN trans_pe_enc As penc On pdet.IdPedidoEnc = penc.IdPedidoEnc 
                         INNER JOIN stock_res sr ON pkdet.IdPedidoDet = sr.IdPedidoDet AND pu.IdUbicacion = sr.IdUbicacion  
                                    and pu.IdStockRes = sr.IdStockRes  and sr.IdBodega = pu.IdBodega   
                         WHERE pkdet.IdPickingEnc = @IdPickingEnc And penc.IdPedidoEnc = @IdPedidoEnc and pu.dañado_verificacion=0 and pu.dañado_picking=0"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    'clsLnBodega_ubicacion.Obtener(.Ubicacion)
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("nom_ubicacion")), 0, lRow.Item("nom_ubicacion")) '.Ubicacion.NombreCompleto
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

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20211227 Creé función para obtener el listado de picking ubic que cumplen con determinados parametros
    Public Shared Function Get_All_PickingUbic_By_PickingUbic(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            '#CKFK20221205 Agregué el IdPickingEnc a los parámetros
            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE dañado_picking = 0 AND
							      cantidad_solicitada <> cantidad_recibida AND " &
                                  IIf(pPickingUbic.IdPickingDet = 0, "", " IdPickingDet=@IdPickingDet AND ") &
                                  " IdUnidadMedida=@IdUnidadMedida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND                                   
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                  IdUbicacion = @IdUbicacion AND
                                  IdProductoEstado = @IdProductoEstado AND 
                                  no_encontrado = 0 AND  
                                  IdPickingEnc = @IdPickingEnc "

            '#CKFK20221129 Quité la comparación con el serial porque este valor no se está enviando
            '(serial = @serial OR serial IS NULL)  AND  

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pPickingUbic.IdPickingDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPickingUbic.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPickingUbic.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@serial", pPickingUbic.Serial)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPickingUbic.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pPickingUbic.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPickingUbic.IdProductoEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    '#CKFK 20220816 Creé función para obtener el picking ubic de un IdDespachoDet
    Public Shared Function Get_PickingUbic_By_IdPickingUbic(ByVal pIdPickingUbic As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As clsBeTrans_picking_ubic

        Dim oReturn As clsBeTrans_picking_ubic = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM trans_picking_ubic
                                  WHERE IdPickingUbic=@IdPickingUbic "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingUbic", pIdPickingUbic)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Obj = New clsBeTrans_picking_ubic

                    Cargar(Obj, lDataTable.Rows(0))

                    Obj.IsNew = False

                    oReturn = Obj

                End If

            End Using

            Return oReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'AT20240521 Obtener picking ubic nuevo para reemplazo automatico

    Public Shared Function Get_PickingUbicRem_By_IdPickingUbic(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                               ByVal Licencia As String,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lPicking As New List(Of clsBeTrans_picking_ubic)
        Dim Obj As New clsBeTrans_picking_ubic


        Try
            Dim vSQL As String = "SELECT TOP 1 * FROM VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado
                                  WHERE dañado_picking = 0 AND
							      cantidad_solicitada <> cantidad_recibida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND                                   
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  cast(fecha_vence as date) = @FechaVence AND
                                  no_encontrado = 0 AND  
                                  IdPickingEnc = @IdPickingEnc AND IdProductoBodega = @IdProductoBodega ORDER BY fec_agr DESC"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaVence", pPickingUbic.Fecha_Vence.Date)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", Licencia)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPickingUbic.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeTrans_picking_ubic
                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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

                        lPicking.Add(Obj)
                    Next

                End If

            End Using

            Return lPicking

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'AT20240521 Obtener picking ubic nuevo para reemplazo automatico
    Public Shared Function Get_PickingUbicRemConso_By_IdPickingUbic(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                               ByVal Licencia As String,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lPicking As New List(Of clsBeTrans_picking_ubic)
        Dim Obj As New clsBeTrans_picking_ubic


        Try
            Dim vSQL As String = "SELECT * FROM VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado
                                  WHERE dañado_picking = 0 AND
							      cantidad_solicitada <> cantidad_recibida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND                                   
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  cast(fecha_vence as date) = @FechaVence AND
                                  no_encontrado = 0 AND  
                                  IdPickingEnc = @IdPickingEnc AND 
                                  IdProductoBodega = @IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaVence", pPickingUbic.Fecha_Vence.Date)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", Licencia)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPickingUbic.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeTrans_picking_ubic
                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                            .IdPickingEnc = pPickingUbic.IdPickingEnc
                            .IsNew = False

                        End With

                        lPicking.Add(Obj)
                    Next

                End If

            End Using

            Return lPicking

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'AT20220615 Obtiene picking para verificación
    Public Shared Function Get_All_PickingUbic_By_Verificacion(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            '#CKFK20221205 Agregué el IdPickingEnc a los parámetros
            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE dañado_picking = 0 AND
							      cantidad_verificada <> cantidad_recibida AND " &
                                  IIf(pPickingUbic.IdPickingDet = 0, "", " IdPickingDet=@IdPickingDet AND ") &
                                  " IdUnidadMedida=@IdUnidadMedida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND 
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                  IdUbicacion = @IdUbicacion AND
                                  IdProductoEstado = @IdProductoEstado AND 
                                  no_encontrado = 0  AND  
                                  IdPickingEnc = @IdPickingEnc "

            '#CKFK20221129 Quité la comparación con el serial porque este valor no se está enviando
            '(serial = @serial OR serial IS NULL)  AND  

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pPickingUbic.IdPickingDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPickingUbic.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPickingUbic.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@serial", pPickingUbic.Serial)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPickingUbic.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pPickingUbic.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPickingUbic.IdProductoEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    '#AT 20220310 Función para obtener lista de picking por medio de IdPickingEnc y ciertos parametros
    Public Shared Function Get_All_PickingUbic_By_BePickingEnc(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE dañado_picking = 0 AND
							      cantidad_solicitada <> cantidad_recibida AND
                                  IdPickingEnc=@IdPickingEnc AND
                                  IdUnidadMedida=@IdUnidadMedida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND 
                                  (serial = @serial OR serial IS NULL)  AND 
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                  IdUbicacion = @IdUbicacion AND
                                  IdProductoEstado = @IdProductoEstado AND 
                                  IdProductoBodega = @IdProductoBodega AND 
                                  no_encontrado = 0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPickingUbic.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPickingUbic.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@serial", pPickingUbic.Serial)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPickingUbic.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pPickingUbic.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPickingUbic.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPickingUbic.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    '#AT20220405 Se obtienen una lista de picking ubic por medio de  picking consolidado.
    Public Shared Function Get_All_PickingUbic_By_Consolidado(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE dañado_picking = 0 
							      AND (cantidad_solicitada <> cantidad_recibida OR cantidad_solicitada <> cantidad_verificada)
                                  AND IdPickingEnc=@IdPickingEnc AND
                                  IdUnidadMedida=@IdUnidadMedida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                  IdUbicacion = @IdUbicacion AND
                                  IdProductoEstado = @IdProductoEstado AND 
                                  IdProductoBodega = @IdProductoBodega AND 
                                  no_encontrado = 0 AND dañado_verificacion = 0 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPickingUbic.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPickingUbic.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPickingUbic.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pPickingUbic.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPickingUbic.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPickingUbic.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic
                        Cargar(Obj, lRow)
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

    Public Shared Function Get_All_PickingUbic_By_Consolidado_Cm(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE dañado_picking = 0 
							      AND (cantidad_solicitada <> cantidad_recibida OR cantidad_solicitada <> cantidad_verificada)
                                  AND IdPickingEnc=@IdPickingEnc AND
                                  lic_plate=@lic_plate "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPickingUbic.IdPickingEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPickingUbic.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPickingUbic.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPickingUbic.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPickingUbic.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPickingUbic.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pPickingUbic.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPickingUbic.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPickingUbic.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic
                        Cargar(Obj, lRow)
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

    Public Shared Function Get_All_IdStocks_PickingUbic_By_IdPickingUbic(ByVal pIdPickingDet As Integer,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_IdStocks_PickingUbic_By_IdPickingUbic = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            '#CKFK 20180402 06:42 PM Agregué esta condición  " and pu.IdStockRes = sr.IdStockRes " para evitar la duplicidad de registros
            Dim vSQL As String = "SELECT pu.*,pdet.IdPedidoEnc,pdet.IdPedidoDet,pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, " &
                " pdet.codigo_producto, " &
                " pdet.nombre_producto,pdet.nom_presentacion, pdet.nom_unid_med,pdet.nom_estado, " &
                " pdet.IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes, sr.IdStock " &
                " FROM trans_picking_ubic pu " &
                " INNER JOIN trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet " &
                " INNER JOIN trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet " &
                " INNER JOIN stock_res sr ON pdet.IdPedidoDet = sr.IdPedidoDet and pu.IdStockRes = sr.IdStockRes " &
                " WHERE pu.IdPickingDet = @IdPickingDet"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingDet", pIdPickingDet))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            clsLnBodega_ubicacion.Obtener(.Ubicacion, lConnection, lTransaction)
                            .NombreUbicacion = .Ubicacion.NombreCompleto

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

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdPickingUbic),0) FROM trans_picking_ubic "

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

    Public Shared Function Actualizar_Picking(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                              ByVal BeStockRes As clsBeStock_res,
                                              ByVal oBeTrans_picking_det As clsBeTrans_picking_det,
                                              ByVal IdBodega As Integer,
                                              ByVal pBeStockReemplazoPallet As clsBeProducto,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As String

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultado As String = ""
        Dim FilasAfectadas As Integer = 0

        Try

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            oBeTrans_picking_ubic.IdStock_reemplazo = oBeTrans_picking_ubic.IdStock
            oBeTrans_picking_ubic.Lic_plate_Reemplazo = oBeTrans_picking_ubic.Lic_plate
            oBeTrans_picking_ubic.IdUbicacion_reemplazo = oBeTrans_picking_ubic.IdUbicacion

            oBeTrans_picking_ubic.IdUbicacion = pBeStockReemplazoPallet.Stock.IdUbicacion
            oBeTrans_picking_ubic.IdStock = pBeStockReemplazoPallet.Stock.IdStock
            oBeTrans_picking_ubic.Lic_plate = pBeStockReemplazoPallet.Stock.Lic_plate

            FilasAfectadas = Actualizar(oBeTrans_picking_ubic, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeStockRes.IdStock = pBeStockReemplazoPallet.Stock.IdStock
            BeStockRes.Lic_plate = pBeStockReemplazoPallet.Stock.Lic_plate
            BeStockRes.IdUbicacion = pBeStockReemplazoPallet.Stock.IdUbicacion

            FilasAfectadas = clsLnStock_res.Actualizar(BeStockRes, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            resultado += String.Format(", actualizó {0} filas en stock_res ", FilasAfectadas.ToString)

            FilasAfectadas = clsLnTrans_picking_det.Actualizar(oBeTrans_picking_det, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            resultado += String.Format(", actualizó {0} filas en trans_picking_det ", FilasAfectadas.ToString)

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            resultado += ", terminó la actualizacion"

            Return resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Picking(ByVal pBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                              ByVal pBeStockRes As clsBeStock_res,
                                              ByVal pIdBodega As Integer,
                                              ByVal pCantidad As Double,
                                              ByVal pHost As String,
                                              Optional ByVal pConnection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As String

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultado As String = ""
        Dim FilasAfectadas As Integer = 0
        Dim bePickingUbicExistente As New clsBeTrans_picking_ubic
        Dim BeOnTablePickingUbic As New clsBeTrans_picking_ubic
        Dim BeTransPeDet As New clsBeTrans_pe_det
        Dim Factor As Integer = 1
        Dim BeTransPickingDet As New clsBeTrans_picking_det
        Dim BeTransPickingUbicStock As New clsBeTrans_picking_ubic_stock
        Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
        Dim BeStockActual As New clsBeStock
        Dim vCantidadARestarStock As Double = 0
        Dim Verificacion_Auto As Boolean = False

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            resultado += "Inicia la actualizacion"

            bePickingUbicExistente.IdPickingUbic = pBeTrans_picking_ubic.IdPickingUbic

            BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(bePickingUbicExistente.IdPickingUbic,
                                                                    If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                    If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeTransPickingUbicStockExistente = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBeTrans_picking_ubic.IdPickingEnc,
                                                                                                                     pBeTrans_picking_ubic.IdPickingUbic,
                                                                                                                     pBeTrans_picking_ubic.IdStock,
                                                                                                                     If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            FilasAfectadas = Actualizar_Cantidad_Recibida(pBeTrans_picking_ubic,
                                                          If(Es_Transaccion_Remota, pConnection, lConnection),
                                                          If(Es_Transaccion_Remota, pTransaction, lTransaction))

            Dim vIdUbicacionPickingByBodega As Integer = 0

            Dim vMoverProductoAMuelle As Boolean = clsLnTrans_pe_enc.Mover_Producto_A_Muelle_By_IdPedidoEnc(pBeTrans_picking_ubic.IdPedidoEnc,
                                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If vMoverProductoAMuelle Then

                Dim BeBodegaMuelle As New clsBeBodega_muelles
                BeBodegaMuelle = clsLnTrans_picking_enc.Get_BeBodegaMuelle_By_IdPickingEnc(pBeTrans_picking_ubic.IdPickingEnc,
                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                           If(Es_Transaccion_Remota, pTransaction, lTransaction))

                '#EJC20250427: definió un muelle para el picking y el muelle debe tener una ubicación válida para colocar allí el stock pickeado.
                If Not BeBodegaMuelle Is Nothing Then
                    If Not BeBodegaMuelle Is Nothing Then
                        vIdUbicacionPickingByBodega = BeBodegaMuelle.IdUbicacionDefecto

                        If vIdUbicacionPickingByBodega = 0 Then
                            Throw New Exception("Error_20250427: No se encontró la ubicación asociada al muelle")
                        End If
                Else
                    Throw New Exception("Error_20250710: La configuración del pedido indica que se debe mover el producto al muelle, pero el muelle no fue definido en el picking")
                    End If

                End If

            Else
                'El picking no tiene muelle, colocar el producto en la ubicación de picking por defecto.
                vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(pBeTrans_picking_ubic.IdBodega,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))
                If vIdUbicacionPickingByBodega = 0 Then
                    Throw New Exception("Error_20250427A: La ubicación de picking no está definida para la bodega")
                End If

            End If

            Dim vCantidadARestarStockUmBas As Double = 0
            Dim vCantidadARestarStockPres As Double = 0

            If (pCantidad) >= pBeTrans_picking_ubic.Cantidad_Solicitada Then
                vCantidadARestarStockUmBas = pBeTrans_picking_ubic.Cantidad_Solicitada
            Else
                vCantidadARestarStockUmBas = pCantidad
            End If

            If pBeTrans_picking_ubic.IdPresentacion > 0 Then

                If Not Es_Transaccion_Remota Then
                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBeTrans_picking_ubic.IdProductoBodega,
                                                                                       pBeTrans_picking_ubic.IdPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)
                Else
                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBeTrans_picking_ubic.IdProductoBodega,
                                                                                       pBeTrans_picking_ubic.IdPresentacion,
                                                                                       pConnection,
                                                                                       pTransaction)
                End If

                vCantidadARestarStockPres = vCantidadARestarStockUmBas
                vCantidadARestarStockUmBas = vCantidadARestarStockUmBas * Factor

            End If

            Verificacion_Auto = clsLnTrans_picking_enc.Get_Verificacion_Auto_By_IdPickingEnc(pBeTrans_picking_ubic.IdPickingEnc, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(pBeTrans_picking_ubic,
                                                                                              vIdUbicacionPickingByBodega,
                                                                                              vCantidadARestarStockUmBas,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

#Region "Inserta PickingUbicStock"

            BeTransPickingUbicStockExistente = New clsBeTrans_picking_ubic_stock
            BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
            BeTransPickingUbicStock.IdBodega = BeOnTablePickingUbic.IdBodega
            BeTransPickingUbicStock.IdPickingUbic = BeOnTablePickingUbic.IdPickingUbic
            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
            BeTransPickingUbicStock.IdUbicacion = BeOnTablePickingUbic.IdUbicacion
            BeTransPickingUbicStock.IdUbicacion = pBeTrans_picking_ubic.IdUbicacion
            BeTransPickingUbicStock.IdStock = BeOnTablePickingUbic.IdStock
            BeTransPickingUbicStock.IdStockRes = BeOnTablePickingUbic.IdStockRes
            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
            BeTransPickingUbicStock.IdProductoBodega = BeOnTablePickingUbic.IdProductoBodega
            BeTransPickingUbicStock.IdProductoEstado = pBeTrans_picking_ubic.IdProductoEstado
            BeTransPickingUbicStock.IdPresentacion = pBeTrans_picking_ubic.IdPresentacion
            BeTransPickingUbicStock.IdUnidadMedida = pBeTrans_picking_ubic.IdUnidadMedida
            BeTransPickingUbicStock.IdUbicacionAnterior = pBeTrans_picking_ubic.IdUbicacionAnterior
            BeTransPickingUbicStock.IdRecepcion = BeOnTablePickingUbic.IdRecepcion
            BeTransPickingUbicStock.IdPedidoEnc = BeOnTablePickingUbic.IdPedidoEnc
            BeTransPickingUbicStock.IdPedidoDet = BeOnTablePickingUbic.IdPedidoDet
            BeTransPickingUbicStock.IdPickingEnc = BeOnTablePickingUbic.IdPickingEnc
            BeTransPickingUbicStock.IdOperadorBodega = pBeTrans_picking_ubic.IdOperadorBodega_Pickeo
            BeTransPickingUbicStock.IdOperadorBodega_Pickeo = pBeTrans_picking_ubic.IdOperadorBodega_Pickeo
            BeTransPickingUbicStock.IdOperadorBodega_Verifico = pBeTrans_picking_ubic.IdOperadorBodega_Verifico
            BeTransPickingUbicStock.Lote = pBeTrans_picking_ubic.Lote
            BeTransPickingUbicStock.Lote = pBeTrans_picking_ubic.Lote
            BeTransPickingUbicStock.Fecha_vence = pBeTrans_picking_ubic.Fecha_Vence
            BeTransPickingUbicStock.Fecha_minima = pBeTrans_picking_ubic.Fecha_minima
            BeTransPickingUbicStock.Serial = pBeTrans_picking_ubic.Serial
            BeTransPickingUbicStock.Licencia = pBeTrans_picking_ubic.Lic_plate
            BeTransPickingUbicStock.Cantidad_recibida = pCantidad
            BeTransPickingUbicStock.Cantidad_verificada = pBeTrans_picking_ubic.Cantidad_Verificada
            BeTransPickingUbicStock.Fecha_picking = pBeTrans_picking_ubic.Fecha_picking
            BeTransPickingUbicStock.Fecha_verificado = pBeTrans_picking_ubic.Fecha_verificado
            BeTransPickingUbicStock.Fecha_verificado = pBeTrans_picking_ubic.Fecha_verificado
            BeTransPickingUbicStock.Fecha_despachado = pBeTrans_picking_ubic.Fecha_despachado
            BeTransPickingUbicStock.Cantidad_despachada = pBeTrans_picking_ubic.Cantidad_despachada
            BeTransPickingUbicStock.User_agr = BeOnTablePickingUbic.User_agr
            BeTransPickingUbicStock.Fec_agr = BeOnTablePickingUbic.Fec_agr
            BeTransPickingUbicStock.User_mod = BeOnTablePickingUbic.User_mod
            BeTransPickingUbicStock.Fec_mod = BeOnTablePickingUbic.Fec_mod
            BeTransPickingUbicStock.Activo = BeOnTablePickingUbic.Activo
            BeTransPickingUbicStock.IdUbicacionTemporal = BeOnTablePickingUbic.IdUbicacionTemporal
            BeTransPickingUbicStock.IdOperadorBodega_Asignado = BeOnTablePickingUbic.IdOperadorBodega_Asignado
            BeTransPickingUbicStock.Host = pHost
            BeTransPickingUbicStock.IdMovimiento = vIdMovimiento
            clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

#End Region

            '#CKFK20250505 Le quité a la cantidad recibida la cantidad despachada, porque si no se resta de más en el inventario
            'Restar la cantidad pickeada al stock original.
            vCantidadARestarStock = pBeTrans_picking_ubic.Cantidad_Recibida - pBeTrans_picking_ubic.Cantidad_despachada

            If pBeTrans_picking_ubic.IdPresentacion > 0 Then

                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBeTrans_picking_ubic.IdProductoBodega,
                                                                                   pBeTrans_picking_ubic.IdPresentacion,
                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))


                '#CKFK20250505 Le quité a la cantidad recibida la cantidad despachada, porque si no se resta de más en el inventario
                vCantidadARestarStock = (pBeTrans_picking_ubic.Cantidad_Recibida * Factor) - (pBeTrans_picking_ubic.Cantidad_despachada * Factor)

            End If

            BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            '#EJC20241005: Mover a ubicación de picking lo pickeado valga la rebusnancia.
            If BeOnTablePickingUbic.Cantidad_Solicitada = pBeTrans_picking_ubic.Cantidad_Recibida Then

                If BeStockActual.Cantidad = vCantidadARestarStock Then

                    FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
                                                                                  pBeTrans_picking_ubic.IdUbicacion,
                                                                                  pBeTrans_picking_ubic.IdBodega,
                                                                                  pBeTrans_picking_ubic.IdStock,
                                                                                  If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                  If(Es_Transaccion_Remota, pTransaction, lTransaction))

                Else

                    'Hacer una copia del stock actual e insertarlo con la cantidad pickeada en la ubicación de picking.
                    Dim BeNuevoStockPickeado As New clsBeStock
                    clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                    BeNuevoStockPickeado.Cantidad = vCantidadARestarStock
                    BeNuevoStockPickeado.Peso = pBeTrans_picking_ubic.Peso_recibido
                    BeNuevoStockPickeado.IdUbicacion_anterior = BeStockActual.IdUbicacion
                    BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega
                    BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
                    BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                    BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion
                    clsLnStock.Insertar(BeNuevoStockPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    BeStockActual.Cantidad = BeStockActual.Cantidad - vCantidadARestarStock
                    BeStockActual.Peso = BeStockActual.Peso - pBeTrans_picking_ubic.Peso_recibido

                    If BeStockActual.Cantidad = 0 Then
                        clsLnStock.Eliminar_By_IdStock(BeStockActual.IdStock, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                    Else
                        clsLnStock.Actualizar_Cantidad(BeStockActual, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                    End If

                    'Obtener el registro de la reserva actual del stock que apunta al idstock original que contiene todo el stock para la posición.
                    Dim BeStockResActual As New clsBeStock_res
                    BeStockResActual = clsLnStock_res.GetSingle_By_IdStockRes(pBeTrans_picking_ubic.IdBodega,
                                                                              pBeTrans_picking_ubic.IdStockRes,
                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    'Actualizar la reserva con el IdStock que contiene únicamente lo pickeado.lTransaction)
                    BeStockResActual.IdStock = BeNuevoStockPickeado.IdStock
                    clsLnStock_res.Actualizar_IdStock(BeStockResActual,
                                                      If(Es_Transaccion_Remota, pConnection, lConnection),
                                                      If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    'Actualizar el picking con el nuevo idstock.
                    pBeTrans_picking_ubic.IdStock = BeStockResActual.IdStock
                    Actualizar_IdStock(pBeTrans_picking_ubic,
                                       If(Es_Transaccion_Remota, pConnection, lConnection),
                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

                End If

            Else

                '#EJC20250409: Split the stock!

#Region "Insertar nuevo stock"

                'Hacer una copia del stock actual e insertarlo con la cantidad pickeada en la ubicación de picking.
                Dim BeNuevoStockPickeado As New clsBeStock
                clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                BeNuevoStockPickeado.Cantidad = vCantidadARestarStock
                BeNuevoStockPickeado.Peso = pBeTrans_picking_ubic.Peso_recibido
                BeNuevoStockPickeado.IdUbicacion_anterior = BeStockActual.IdUbicacion
                BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega
                BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
                BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion
                clsLnStock.Insertar(BeNuevoStockPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
#End Region

#Region "Actualizar cantidad en stock Actual"

                BeStockActual.Cantidad = BeStockActual.Cantidad - vCantidadARestarStock
                BeStockActual.Peso = BeStockActual.Peso - pBeTrans_picking_ubic.Peso_recibido

                If BeStockActual.Cantidad = 0 Then
                    clsLnStock.Eliminar_By_IdStock(BeStockActual.IdStock, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                Else
                    clsLnStock.Actualizar_Cantidad(BeStockActual, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                End If

#End Region

                'Obtener el registro de la reserva actual del stock que apunta al idstock original que contiene todo el stock para la posición.
                Dim BeStockResActual As New clsBeStock_res

                BeStockResActual = clsLnStock_res.GetSingle_By_IdStockRes(pBeTrans_picking_ubic.IdBodega,
                                                                             pBeTrans_picking_ubic.IdStockRes,
                                                                             If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))


                Dim BeNuevoStockResPickeado As New clsBeStock_res
                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStock
                BeNuevoStockPickeado.Peso = pBeTrans_picking_ubic.Peso_recibido
                'BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                BeNuevoStockResPickeado.Estado = "PICKEADO"
                clsLnStock_res.Insertar(BeNuevoStockResPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                clsPublic.CopyObject(pBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
                BeNuevoTransPickingUbic.Cantidad_Solicitada = pCantidad
                BeNuevoTransPickingUbic.Cantidad_Recibida = pCantidad
                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(pBeTrans_picking_ubic.Cantidad_Verificada > 0, pCantidad, 0)
                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                Insertar(BeNuevoTransPickingUbic, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

                BeStockResActual.Cantidad = BeStockResActual.Cantidad - vCantidadARestarStock
                BeStockResActual.Peso = BeStockActual.Peso - pBeTrans_picking_ubic.Peso_recibido

                If BeStockResActual.Cantidad = 0 Then
                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                Else
                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
                End If

                pBeTrans_picking_ubic.Cantidad_Recibida = 0
                pBeTrans_picking_ubic.Cantidad_Verificada = 0
                pBeTrans_picking_ubic.Encontrado = False
                pBeTrans_picking_ubic.Cantidad_Solicitada = pBeTrans_picking_ubic.Cantidad_Solicitada - pCantidad
                Actualizar_Cantidad_Recibida(pBeTrans_picking_ubic, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

                pBeStockRes.Estado = "UNCOMMITED"

            End If

            resultado += String.Format(", actualizó {0} filas en trans_picking_ubic ", FilasAfectadas.ToString)

            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(pBeStockRes, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            resultado += String.Format(", actualizó {0} filas en stock_res ", FilasAfectadas.ToString)

            BeTransPeDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(pBeTrans_picking_ubic.IdPedidoEnc,
                                                                                           pBeTrans_picking_ubic.IdPedidoDet,
                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                           If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeTransPickingDet = clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(pBeTrans_picking_ubic.IdPickingEnc,
                                                                                                       pBeTrans_picking_ubic.IdPickingDet,
                                                                                                       If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If BeTransPeDet.IdPresentacion <> pBeTrans_picking_ubic.IdPresentacion Then
                If BeTransPeDet.IdPresentacion = 0 AndAlso pBeTrans_picking_ubic.IdPresentacion <> 0 Then
                    If Factor <> 0 Then
                        Dim CantPedido As Double = pCantidad * Factor
                        BeTransPickingDet.Cantidad_recibida += CantPedido
                        pCantidad = pCantidad * Factor
                    End If
                End If

            Else
                Dim vCantPickeada As Double = 0
                vCantPickeada = Math.Round(BeTransPickingDet.Cantidad_recibida + pCantidad, 6)
                BeTransPickingDet.Cantidad_recibida = vCantPickeada
                pCantidad = pCantidad * Factor
            End If

            FilasAfectadas = clsLnTrans_picking_det.Actualizar(BeTransPickingDet,
                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If Verificacion_Auto Then

                clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(pBeTrans_picking_ubic,
                                                                        vIdUbicacionPickingByBodega,
                                                                        pCantidad,
                                                                        pBeTrans_picking_ubic.Peso_recibido,
                                                                        IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                        IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            End If

            resultado += String.Format(", actualizó {0} filas en trans_picking_det ", FilasAfectadas.ToString)

            resultado += ", terminó la actualizacion"

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    '    Public Shared Function Actualizar_Picking(ByVal pBeTrans_picking_ubic As clsBeTrans_picking_ubic,
    '                                              ByVal pBeStockRes As clsBeStock_res,
    '                                              ByVal pIdBodega As Integer,
    '                                              ByVal pCantidad As Double,
    '                                              ByVal pHost As String,
    '                                              Optional ByVal pConnection As SqlConnection = Nothing,
    '                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As String

    '        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
    '        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '        Dim lTransaction As SqlTransaction = Nothing
    '        Dim resultado As String = ""
    '        Dim FilasAfectadas As Integer = 0
    '        Dim bePickingUbicExistente As New clsBeTrans_picking_ubic
    '        Dim BeOnTablePickingUbic As New clsBeTrans_picking_ubic
    '        Dim BeTransPeDet As New clsBeTrans_pe_det
    '        Dim Factor As Integer = 1
    '        Dim BeTransPickingDet As New clsBeTrans_picking_det
    '        Dim BeTransPickingUbicStock As New clsBeTrans_picking_ubic_stock
    '        Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
    '        Dim BeStockActual As New clsBeStock
    '        Dim Verificacion_Auto As Boolean = False

    '        Try

    '            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '            resultado += "Inicia la actualizacion"

    '            bePickingUbicExistente.IdPickingUbic = pBeTrans_picking_ubic.IdPickingUbic

    '            BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(bePickingUbicExistente.IdPickingUbic,
    '                                                                    If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                    If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            BeTransPickingUbicStockExistente = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBeTrans_picking_ubic.IdPickingEnc,
    '                                                                                                                     pBeTrans_picking_ubic.IdPickingUbic,
    '                                                                                                                     pBeTrans_picking_ubic.IdStock,
    '                                                                                                                     If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                                                     If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            FilasAfectadas = Actualizar_Cantidad_Recibida(pBeTrans_picking_ubic,
    '                                                          If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                          If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            Dim vIdUbicacionPickingByBodega As Integer = 0

    '            vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(pBeTrans_picking_ubic.IdBodega, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '            Verificacion_Auto = clsLnTrans_picking_enc.Get_Verificacion_Auto_By_IdPickingEnc(pBeTrans_picking_ubic.IdPickingEnc, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            Dim vCantidadARestarStockUmBas As Double = 0
    '            Dim vCantidadARestarStockPres As Double = 0

    '            vCantidadARestarStockUmBas = pBeTrans_picking_ubic.Cantidad_Solicitada

    '            If pBeTrans_picking_ubic.IdPresentacion > 0 Then

    '                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBeTrans_picking_ubic.IdProductoBodega,
    '                                                                                   pBeTrans_picking_ubic.IdPresentacion,
    '                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                vCantidadARestarStockPres = vCantidadARestarStockUmBas
    '                vCantidadARestarStockUmBas = vCantidadARestarStockUmBas * Factor

    '            End If

    '#Region "Inserta PickingUbicStock"

    '            BeTransPickingUbicStockExistente = New clsBeTrans_picking_ubic_stock
    '            BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
    '            BeTransPickingUbicStock.IdBodega = BeOnTablePickingUbic.IdBodega
    '            BeTransPickingUbicStock.IdPickingUbic = BeOnTablePickingUbic.IdPickingUbic
    '            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
    '            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
    '            BeTransPickingUbicStock.IdUbicacion = BeOnTablePickingUbic.IdUbicacion
    '            BeTransPickingUbicStock.IdUbicacion = pBeTrans_picking_ubic.IdUbicacion
    '            BeTransPickingUbicStock.IdStock = BeOnTablePickingUbic.IdStock
    '            BeTransPickingUbicStock.IdStockRes = BeOnTablePickingUbic.IdStockRes
    '            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
    '            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
    '            BeTransPickingUbicStock.IdProductoBodega = BeOnTablePickingUbic.IdProductoBodega
    '            BeTransPickingUbicStock.IdProductoEstado = pBeTrans_picking_ubic.IdProductoEstado
    '            BeTransPickingUbicStock.IdPresentacion = pBeTrans_picking_ubic.IdPresentacion
    '            BeTransPickingUbicStock.IdUnidadMedida = pBeTrans_picking_ubic.IdUnidadMedida
    '            BeTransPickingUbicStock.IdUbicacionAnterior = pBeTrans_picking_ubic.IdUbicacionAnterior
    '            BeTransPickingUbicStock.IdRecepcion = BeOnTablePickingUbic.IdRecepcion
    '            BeTransPickingUbicStock.IdPedidoEnc = BeOnTablePickingUbic.IdPedidoEnc
    '            BeTransPickingUbicStock.IdPedidoDet = BeOnTablePickingUbic.IdPedidoDet
    '            BeTransPickingUbicStock.Idpickingenc = BeOnTablePickingUbic.IdPickingEnc
    '            BeTransPickingUbicStock.IdOperadorBodega = pBeTrans_picking_ubic.IdOperadorBodega_Pickeo
    '            BeTransPickingUbicStock.IdOperadorBodega_Pickeo = pBeTrans_picking_ubic.IdOperadorBodega_Pickeo
    '            BeTransPickingUbicStock.IdOperadorBodega_Verifico = pBeTrans_picking_ubic.IdOperadorBodega_Verifico
    '            BeTransPickingUbicStock.Lote = pBeTrans_picking_ubic.Lote
    '            BeTransPickingUbicStock.Lote = pBeTrans_picking_ubic.Lote
    '            BeTransPickingUbicStock.Fecha_vence = pBeTrans_picking_ubic.Fecha_Vence
    '            BeTransPickingUbicStock.Fecha_minima = pBeTrans_picking_ubic.Fecha_minima
    '            BeTransPickingUbicStock.Serial = pBeTrans_picking_ubic.Serial
    '            BeTransPickingUbicStock.Licencia = pBeTrans_picking_ubic.Lic_plate
    '            BeTransPickingUbicStock.Cantidad_recibida = pCantidad
    '            BeTransPickingUbicStock.Cantidad_verificada = pBeTrans_picking_ubic.Cantidad_Verificada
    '            BeTransPickingUbicStock.Fecha_picking = pBeTrans_picking_ubic.Fecha_picking
    '            BeTransPickingUbicStock.Fecha_verificado = pBeTrans_picking_ubic.Fecha_verificado
    '            BeTransPickingUbicStock.Fecha_verificado = pBeTrans_picking_ubic.Fecha_verificado
    '            BeTransPickingUbicStock.Fecha_despachado = pBeTrans_picking_ubic.Fecha_despachado
    '            BeTransPickingUbicStock.Cantidad_despachada = pBeTrans_picking_ubic.Cantidad_despachada
    '            BeTransPickingUbicStock.User_agr = BeOnTablePickingUbic.User_agr
    '            BeTransPickingUbicStock.Fec_agr = BeOnTablePickingUbic.Fec_agr
    '            BeTransPickingUbicStock.User_mod = BeOnTablePickingUbic.User_mod
    '            BeTransPickingUbicStock.Fec_mod = BeOnTablePickingUbic.Fec_mod
    '            BeTransPickingUbicStock.Activo = BeOnTablePickingUbic.Activo
    '            BeTransPickingUbicStock.IdUbicacionTemporal = BeOnTablePickingUbic.IdUbicacionTemporal
    '            BeTransPickingUbicStock.IdOperadorBodega_Asignado = BeOnTablePickingUbic.IdOperadorBodega_Asignado
    '            BeTransPickingUbicStock.Host = pHost
    '            clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock,
    '                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '#End Region

    '            BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock,
    '                                                             If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                             If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            If BeStockActual Is Nothing Then
    '                Throw New Exception("Error_20250419B: No se pudo obtener el BeStockOriginal")
    '            End If

    '            Dim BeStockResActual As clsBeStock_res = clsLnStock_res.GetSingle_By_IdStockRes(BeOnTablePickingUbic.IdBodega,
    '                                                                                            BeOnTablePickingUbic.IdStockRes,
    '                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            If BeStockResActual Is Nothing Then
    '                Throw New Exception("Error_20250419A: No se pudo obtener el BeStockResActual")
    '            End If

    '            '#EJC20241005: Mover a ubicación de pickinig lo pickeado valga la rebusnancia.
    '            If BeOnTablePickingUbic.Cantidad_Solicitada = pBeTrans_picking_ubic.Cantidad_Recibida Then

    '                If BeStockActual.Cantidad = vCantidadARestarStockUmBas Then

    '                    FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
    '                                                                                  pBeTrans_picking_ubic.IdUbicacion,
    '                                                                                  pBeTrans_picking_ubic.IdBodega,
    '                                                                                  pBeTrans_picking_ubic.IdStock,
    '                                                                                  If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                  If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                Else

    '                    'Hacer una copia del stock actual e insertarlo con la cantidad pickeada en la ubicación de picking.
    '                    Dim BeNuevoStockPickeado As New clsBeStock
    '                    clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
    '                    BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
    '                    BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega
    '                    BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
    '                    BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
    '                    BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion
    '                    clsLnStock.Insertar(BeNuevoStockPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                    BeStockActual.Cantidad = BeStockActual.Cantidad - vCantidadARestarStockUmBas
    '                    If BeStockActual.Cantidad = 0 Then
    '                        clsLnStock.Eliminar_By_IdStock(BeStockActual.IdStock,
    '                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                    Else
    '                        clsLnStock.Actualizar_Cantidad(BeStockActual,
    '                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                    End If

    '                    BeStockResActual = clsLnStock_res.GetSingle_By_IdStockRes(pBeTrans_picking_ubic.IdBodega,
    '                                                                                  pBeTrans_picking_ubic.IdStockRes,
    '                                                                                  If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                  If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                    'Actualizar la reserva con el IdStock que contiene únicamente lo pickeado.lTransaction)
    '                    BeStockResActual.IdStock = BeNuevoStockPickeado.IdStock
    '                    clsLnStock_res.Actualizar_IdStock(BeStockResActual,
    '                                                          If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                          If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                    'Actualizar el picking con el nuevo idstock.
    '                    pBeTrans_picking_ubic.IdStock = BeStockResActual.IdStock
    '                    Actualizar_IdStock(pBeTrans_picking_ubic,
    '                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                End If

    '            Else

    '                '#EJC20250409: Split the stock!

    '#Region "Insertar nuevo stock"

    '                'Hacer una copia del stock actual e insertarlo con la cantidad pickeada en la ubicación de picking.
    '                Dim BeNuevoStockPickeado As New clsBeStock
    '                clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
    '                BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
    '                BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega
    '                BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction)) + 1
    '                BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
    '                BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion
    '                clsLnStock.Insertar(BeNuevoStockPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '#End Region

    '#Region "Actualizar cantidad en stock Actual"

    '                BeStockActual.Cantidad = BeStockActual.Cantidad - vCantidadARestarStockUmBas
    '                If BeStockActual.Cantidad = 0 Then
    '                    clsLnStock.Eliminar_By_IdStock(BeStockActual.IdStock,
    '                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                Else
    '                    clsLnStock.Actualizar_Cantidad(BeStockActual,
    '                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                End If
    '#End Region

    '                Dim BeNuevoStockResPickeado As New clsBeStock_res
    '                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
    '                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
    '                BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1
    '                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
    '                BeNuevoStockResPickeado.Estado = "PICKEADO"
    '                clsLnStock_res.Insertar(BeNuevoStockResPickeado,
    '                                            If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
    '                clsPublic.CopyObject(pBeTrans_picking_ubic, BeNuevoTransPickingUbic)
    '                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
    '                BeNuevoTransPickingUbic.Cantidad_Solicitada = pCantidad
    '                BeNuevoTransPickingUbic.Cantidad_Recibida = pCantidad
    '                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(pBeTrans_picking_ubic.Cantidad_Verificada > 0, pCantidad, 0)
    '                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
    '                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
    '                Insertar(BeNuevoTransPickingUbic,
    '                         If(Es_Transaccion_Remota, pConnection, lConnection),
    '                         If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                BeStockResActual.Cantidad = BeStockResActual.Cantidad - vCantidadARestarStockUmBas

    '                If BeStockResActual.Cantidad = 0 Then
    '                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes,
    '                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                Else
    '                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual,
    '                                                                  If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                  If(Es_Transaccion_Remota, pTransaction, lTransaction))
    '                End If

    '                pBeTrans_picking_ubic.Cantidad_Recibida = 0
    '                pBeTrans_picking_ubic.Cantidad_Verificada = 0
    '                pBeTrans_picking_ubic.Encontrado = False
    '                pBeTrans_picking_ubic.Cantidad_Solicitada = pBeTrans_picking_ubic.Cantidad_Solicitada - pCantidad
    '                Actualizar_Cantidad_Recibida(pBeTrans_picking_ubic,
    '                                             If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                             If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '                pBeStockRes.Fec_mod = Now
    '                pBeStockRes.Estado = "UNCOMMITED"

    '            End If

    '            resultado += String.Format(", actualizó {0} filas en trans_picking_ubic ", FilasAfectadas.ToString)

    '            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(pBeStockRes,
    '                                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            resultado += String.Format(", actualizó {0} filas en stock_res ", FilasAfectadas.ToString)

    '            BeTransPeDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(pBeTrans_picking_ubic.IdPedidoEnc,
    '                                                                                       pBeTrans_picking_ubic.IdPedidoDet,
    '                                                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            BeTransPickingDet = clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(pBeTrans_picking_ubic.IdPickingEnc,
    '                                                                                                   pBeTrans_picking_ubic.IdPickingDet,
    '                                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            If BeTransPeDet.IdPresentacion <> pBeTrans_picking_ubic.IdPresentacion Then
    '                If BeTransPeDet.IdPresentacion = 0 AndAlso pBeTrans_picking_ubic.IdPresentacion <> 0 Then
    '                    If Factor <> 0 Then
    '                        Dim CantPedido As Double = pCantidad * Factor
    '                        BeTransPickingDet.Cantidad_recibida += CantPedido
    '                        pCantidad = pCantidad * Factor
    '                    End If
    '                End If

    '            Else
    '                Dim vCantPickeada As Double = 0
    '                vCantPickeada = Math.Round(BeTransPickingDet.Cantidad_recibida + pCantidad, 6)
    '                BeTransPickingDet.Cantidad_recibida = vCantPickeada
    '                pCantidad = pCantidad * Factor
    '            End If

    '            FilasAfectadas = clsLnTrans_picking_det.Actualizar(BeTransPickingDet,
    '                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            clsLnTrans_movimientos.Insertar_Movimiento_Picking(pBeTrans_picking_ubic,
    '                                                               vIdUbicacionPickingByBodega,
    '                                                               pCantidad,
    '                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))

    '            If Verificacion_Auto Then
    '                clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(pBeTrans_picking_ubic,
    '                                                                        pCantidad,
    '                                                                        pBeTrans_picking_ubic.Peso_recibido,
    '                                                                        IIf(Es_Transaccion_Remota, pConnection, lConnection),
    '                                                                        IIf(Es_Transaccion_Remota, pTransaction, lTransaction))
    '            End If


    '            resultado += String.Format(", actualizó {0} filas en trans_picking_det ", FilasAfectadas.ToString)

    '            resultado += ", terminó la actualizacion"

    '            If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '            Return resultado

    '        Catch ex As Exception
    '            If Not Es_Transaccion_Remota Then If Not lTransaction Is Nothing Then lTransaction.Rollback()
    '            Throw ex
    '        Finally
    '            If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '            If lConnection IsNot Nothing Then lConnection.Dispose()
    '        End Try

    '    End Function

    Public Shared Function Actualizar_Picking_Desde_Consolidado1(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                                 ByVal BeStockResActual As clsBeStock_res,
                                                                 ByVal oBeTrans_picking_det As clsBeTrans_picking_det,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal pHost As String,
                                                                 ByRef pCantidad As Double,
                                                                 Optional ByVal pConnection As SqlConnection = Nothing,
                                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As String

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultado As String = ""
        Dim FilasAfectadas As Integer = 0
        Dim bePickingUbicExistente As New clsBeTrans_picking_ubic
        Dim beTransPeDet As New clsBeTrans_pe_det
        Dim BeOnTablePickingUbic As New clsBeTrans_picking_ubic
        Dim Factor As Double = 1
        Dim vIdUbicacionPickingByBodega As Integer = 0
        Dim BeTransPickingUbicStock As New clsBeTrans_picking_ubic_stock
        Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
        Dim BeStockActual As New clsBeStock
        Dim BeNuevoStockPickeado As New clsBeStock

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CKFK20250428 Agregué la funcionalidad de la ubicación de muelle en el picking consolidado
            Dim vMoverProductoAMuelle As Boolean = clsLnTrans_pe_enc.Mover_Producto_A_Muelle_By_IdPedidoEnc(oBeTrans_picking_ubic.IdPedidoEnc,
                                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If vMoverProductoAMuelle Then

                Dim BeBodegaMuelle As New clsBeBodega_muelles
                BeBodegaMuelle = clsLnTrans_picking_enc.Get_BeBodegaMuelle_By_IdPickingEnc(oBeTrans_picking_ubic.IdPickingEnc,
                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                           If(Es_Transaccion_Remota, pTransaction, lTransaction))

                '#EJC20250427: definió un muelle para el picking y el muelle debe tener una ubicación válida para colocar allí el stock pickeado.
                If Not BeBodegaMuelle Is Nothing Then
                    If Not BeBodegaMuelle Is Nothing Then
                        vIdUbicacionPickingByBodega = BeBodegaMuelle.IdUbicacionDefecto

                        If vIdUbicacionPickingByBodega = 0 Then
                            Throw New Exception("Error_20250427: No se encontró la ubicación asociada al muelle")
                        End If
                    End If

                End If

            Else
                'El picking no tiene muelle, colocar el producto en la ubicación de picking por defecto.
                vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(oBeTrans_picking_ubic.IdBodega,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))
                If vIdUbicacionPickingByBodega = 0 Then
                    Throw New Exception("Error_20250427A: La ubicación de picking no está definida para la bodega")
                End If

            End If

            FilasAfectadas = clsLnTrans_picking_det.Actualizar(oBeTrans_picking_det, lConnection, lTransaction)

            If Not Es_Transaccion_Remota Then
                BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(oBeTrans_picking_ubic.IdPickingUbic, lConnection, lTransaction)
            Else
                BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(oBeTrans_picking_ubic.IdPickingUbic, pConnection, pTransaction)
            End If

            If Not Es_Transaccion_Remota Then
                BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(lConnection, lTransaction) + 1
            Else
                BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(pConnection, pTransaction) + 1
            End If

            Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(BeOnTablePickingUbic,
                                                                                              vIdUbicacionPickingByBodega,
                                                                                              pCantidad,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))

            Dim Verificacion_Auto As Boolean = clsLnTrans_picking_enc.Get_Verificacion_Auto_By_IdPickingEnc(BeOnTablePickingUbic.IdPickingEnc,
                                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If Verificacion_Auto Then

                clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(BeOnTablePickingUbic,
                                                                        vIdUbicacionPickingByBodega,
                                                                        pCantidad,
                                                                        BeOnTablePickingUbic.Peso_recibido,
                                                                        IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                        IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            End If

            BeTransPickingUbicStock.IdBodega = BeOnTablePickingUbic.IdBodega
            BeTransPickingUbicStock.IdPickingUbic = BeOnTablePickingUbic.IdPickingUbic
            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
            BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
            BeTransPickingUbicStock.IdUbicacion = BeOnTablePickingUbic.IdUbicacion
            BeTransPickingUbicStock.IdUbicacion = oBeTrans_picking_ubic.IdUbicacion
            BeTransPickingUbicStock.IdStock = BeOnTablePickingUbic.IdStock
            BeTransPickingUbicStock.IdStockRes = BeOnTablePickingUbic.IdStockRes
            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
            BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
            BeTransPickingUbicStock.IdProductoBodega = BeOnTablePickingUbic.IdProductoBodega
            BeTransPickingUbicStock.IdProductoEstado = oBeTrans_picking_ubic.IdProductoEstado
            BeTransPickingUbicStock.IdPresentacion = oBeTrans_picking_ubic.IdPresentacion
            BeTransPickingUbicStock.IdUnidadMedida = oBeTrans_picking_ubic.IdUnidadMedida
            BeTransPickingUbicStock.IdUbicacionAnterior = oBeTrans_picking_ubic.IdUbicacionAnterior
            BeTransPickingUbicStock.IdRecepcion = BeOnTablePickingUbic.IdRecepcion
            BeTransPickingUbicStock.IdPedidoEnc = BeOnTablePickingUbic.IdPedidoEnc
            BeTransPickingUbicStock.IdPedidoDet = BeOnTablePickingUbic.IdPedidoDet
            BeTransPickingUbicStock.IdPickingEnc = BeOnTablePickingUbic.IdPickingEnc
            BeTransPickingUbicStock.IdOperadorBodega = oBeTrans_picking_ubic.IdOperadorBodega_Pickeo
            BeTransPickingUbicStock.IdOperadorBodega_Pickeo = oBeTrans_picking_ubic.IdOperadorBodega_Pickeo
            BeTransPickingUbicStock.IdOperadorBodega_Verifico = oBeTrans_picking_ubic.IdOperadorBodega_Verifico
            BeTransPickingUbicStock.Lote = oBeTrans_picking_ubic.Lote
            BeTransPickingUbicStock.Lote = oBeTrans_picking_ubic.Lote
            BeTransPickingUbicStock.Fecha_vence = oBeTrans_picking_ubic.Fecha_Vence
            BeTransPickingUbicStock.Fecha_minima = oBeTrans_picking_ubic.Fecha_minima
            BeTransPickingUbicStock.Serial = oBeTrans_picking_ubic.Serial
            BeTransPickingUbicStock.Licencia = oBeTrans_picking_ubic.Lic_plate
            BeTransPickingUbicStock.Cantidad_recibida = pCantidad
            BeTransPickingUbicStock.Cantidad_verificada = pCantidad
            BeTransPickingUbicStock.Fecha_picking = oBeTrans_picking_ubic.Fecha_picking
            BeTransPickingUbicStock.Fecha_verificado = oBeTrans_picking_ubic.Fecha_verificado
            BeTransPickingUbicStock.Fecha_verificado = oBeTrans_picking_ubic.Fecha_verificado
            BeTransPickingUbicStock.Fecha_despachado = oBeTrans_picking_ubic.Fecha_despachado
            BeTransPickingUbicStock.Cantidad_despachada = oBeTrans_picking_ubic.Cantidad_despachada
            BeTransPickingUbicStock.User_agr = BeOnTablePickingUbic.User_agr
            BeTransPickingUbicStock.Fec_agr = BeOnTablePickingUbic.Fec_agr
            BeTransPickingUbicStock.User_mod = BeOnTablePickingUbic.User_mod
            BeTransPickingUbicStock.Fec_mod = BeOnTablePickingUbic.Fec_mod
            BeTransPickingUbicStock.Activo = BeOnTablePickingUbic.Activo
            BeTransPickingUbicStock.IdUbicacionTemporal = BeOnTablePickingUbic.IdUbicacionTemporal
            BeTransPickingUbicStock.IdOperadorBodega_Asignado = BeOnTablePickingUbic.IdOperadorBodega_Asignado
            BeTransPickingUbicStock.Host = pHost
            BeTransPickingUbicStock.IdMovimiento = vIdMovimiento

            If Not Es_Transaccion_Remota Then
                clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock, lConnection, lTransaction)
            Else
                clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock, pConnection, pTransaction)
            End If

            If Not Es_Transaccion_Remota Then
                BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock, lConnection, lTransaction)
            Else
                BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock, pConnection, pTransaction)
            End If

            Dim vCantidadARestarStockUmBas As Double = 0
            Dim vCantidadARestarStockPres As Double = 0

            If (pCantidad) > oBeTrans_picking_ubic.Cantidad_Solicitada Then
                vCantidadARestarStockUmBas = oBeTrans_picking_ubic.Cantidad_Solicitada
            Else
                vCantidadARestarStockUmBas = pCantidad
            End If

            If oBeTrans_picking_ubic.IdPresentacion > 0 Then

                If Not Es_Transaccion_Remota Then
                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                       oBeTrans_picking_ubic.IdPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)
                Else
                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                       oBeTrans_picking_ubic.IdPresentacion,
                                                                                       pConnection,
                                                                                       pTransaction)
                End If

                vCantidadARestarStockPres = vCantidadARestarStockUmBas
                vCantidadARestarStockUmBas = vCantidadARestarStockUmBas * Factor

            End If

            If BeOnTablePickingUbic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Recibida Then

                If BeStockActual IsNot Nothing Then

                    If BeStockActual.Cantidad = vCantidadARestarStockUmBas Then

                        If Not Es_Transaccion_Remota Then
                            FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
                                                                                      oBeTrans_picking_ubic.IdUbicacion,
                                                                                      oBeTrans_picking_ubic.IdBodega,
                                                                                      oBeTrans_picking_ubic.IdStock,
                                                                                      lConnection,
                                                                                      lTransaction)

                            BeStockResActual.Estado = "PICKEADO"
                            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                        Else
                            FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
                                                                                      oBeTrans_picking_ubic.IdUbicacion,
                                                                                      oBeTrans_picking_ubic.IdBodega,
                                                                                      oBeTrans_picking_ubic.IdStock,
                                                                                      pConnection,
                                                                                      pTransaction)

                            BeStockResActual.Estado = "PICKEADO"
                            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                        End If

                        If Not Es_Transaccion_Remota Then
                            FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, lConnection, lTransaction)
                        Else
                            FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                        End If

                    Else

                        BeNuevoStockPickeado = New clsBeStock
                        clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                        BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
                        BeNuevoStockPickeado.IdUbicacion_anterior = BeStockActual.IdUbicacion
                        BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega

                        If Not Es_Transaccion_Remota Then
                            BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1
                        Else
                            BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(pConnection, pTransaction) + 1
                        End If

                        BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                        BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion

                        If Not Es_Transaccion_Remota Then
                            clsLnStock.Insertar(BeNuevoStockPickeado, lConnection, lTransaction)
                        Else
                            clsLnStock.Insertar(BeNuevoStockPickeado, pConnection, pTransaction)
                        End If

                        BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad - vCantidadARestarStockUmBas, 6)
                        BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad, 6)

                        If Not Es_Transaccion_Remota Then
                            If BeStockActual.Cantidad = 0 Then
                                clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, lConnection, lTransaction)
                            Else
                                clsLnStock.Actualizar_Cantidad(BeStockActual, lConnection, lTransaction)
                            End If
                        Else
                            If BeStockActual.Cantidad = 0 Then
                                clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, pConnection, pTransaction)
                            Else
                                clsLnStock.Actualizar_Cantidad(BeStockActual, pConnection, pTransaction)
                            End If
                        End If

                        If Not Es_Transaccion_Remota Then

                            If Not BeStockResActual Is Nothing Then

                                If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                    BeStockResActual.Cantidad = 0
                                Else
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                End If

                                If BeStockResActual.Cantidad = 0 Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                                Else

                                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, lConnection, lTransaction)

                                    BeStockResActual.Estado = "UNCOMMITED"
                                    FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                                End If

                                Dim BeNuevoStockResPickeado As New clsBeStock_res
                                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1
                                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoStockResPickeado.Estado = "PICKEADO"
                                clsLnStock_res.Insertar(BeNuevoStockResPickeado, lConnection, lTransaction)

                                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                                BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                Insertar(BeNuevoTransPickingUbic, lConnection, lTransaction)

                            End If

                            oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                            oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                            oBeTrans_picking_ubic.Cantidad_Recibida = 0
                            oBeTrans_picking_ubic.Cantidad_Verificada = 0
                            oBeTrans_picking_ubic.Encontrado = False

                            If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                            Else
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                            End If

                        Else

                            If Not BeStockResActual Is Nothing Then

                                If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                    BeStockResActual.Cantidad = 0
                                Else
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                End If

                                If BeStockResActual.Cantidad = 0 Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, pConnection, pTransaction)
                                Else

                                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, pConnection, pTransaction)

                                    BeStockResActual.Estado = "UNCOMMITED"
                                    FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                                End If

                                Dim BeNuevoStockResPickeado As New clsBeStock_res
                                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(pConnection, pTransaction) + 1
                                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoStockResPickeado.Estado = "PICKEADO"
                                clsLnStock_res.Insertar(BeNuevoStockResPickeado, pConnection, pTransaction)

                                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(pConnection, pTransaction) + 1
                                BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                Insertar(BeNuevoTransPickingUbic, pConnection, pTransaction)

                            End If

                            oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                            oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                            oBeTrans_picking_ubic.Cantidad_Recibida = 0
                            oBeTrans_picking_ubic.Cantidad_Verificada = 0
                            oBeTrans_picking_ubic.Encontrado = False

                            If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                            Else
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                            End If

                        End If

                    End If

                Else
                    Throw New Exception("No existe el idstock " & oBeTrans_picking_ubic.IdStock)
                End If

            Else

                'Es un picking parcial?
                BeNuevoStockPickeado = New clsBeStock
                clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
                BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega

                If Not Es_Transaccion_Remota Then
                    BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1
                Else
                    BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(pConnection, pTransaction) + 1
                End If

                BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion

                If Not Es_Transaccion_Remota Then
                    clsLnStock.Insertar(BeNuevoStockPickeado, lConnection, lTransaction)
                Else
                    clsLnStock.Insertar(BeNuevoStockPickeado, pConnection, pTransaction)
                End If

                If BeStockActual IsNot Nothing Then

                    BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad - vCantidadARestarStockUmBas, 6)
                    BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad, 6)

                    If Not Es_Transaccion_Remota Then
                        If BeStockActual.Cantidad = 0 Then
                            clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, lConnection, lTransaction)
                        Else
                            clsLnStock.Actualizar_Cantidad(BeStockActual, lConnection, lTransaction)
                        End If
                    Else
                        If BeStockActual.Cantidad = 0 Then
                            clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, pConnection, pTransaction)
                        Else
                            clsLnStock.Actualizar_Cantidad(BeStockActual, pConnection, pTransaction)
                        End If
                    End If

                Else
                    Throw New Exception("No existe el idstock " & oBeTrans_picking_ubic.IdStock)
                End If

                If Not Es_Transaccion_Remota Then

                    If Not BeStockResActual Is Nothing Then

                        If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                            BeStockResActual.Cantidad = 0
                        Else
                            BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                            BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                        End If

                        If BeStockResActual.Cantidad = 0 Then
                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                        Else

                            clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, lConnection, lTransaction)

                            BeStockResActual.Estado = "UNCOMMITED"
                            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                        End If

                        Dim BeNuevoStockResPickeado As New clsBeStock_res
                        clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                        BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                        BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1
                        BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                        BeNuevoStockResPickeado.Estado = "PICKEADO"
                        clsLnStock_res.Insertar(BeNuevoStockResPickeado, lConnection, lTransaction)

                        Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                        clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                        BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                        BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                        BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                        BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                        BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                        BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                        Insertar(BeNuevoTransPickingUbic, lConnection, lTransaction)

                    End If

                    oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                    oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                    oBeTrans_picking_ubic.Cantidad_Recibida = 0
                    oBeTrans_picking_ubic.Cantidad_Verificada = 0
                    oBeTrans_picking_ubic.Encontrado = False

                    If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                        FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                    Else
                        FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                    End If

                Else

                    BeStockResActual = clsLnStock_res.GetSingle_By_IdStockRes(oBeTrans_picking_ubic.IdBodega,
                                                                              oBeTrans_picking_ubic.IdStockRes,
                                                                              pConnection,
                                                                              pTransaction)

                    If Not BeStockResActual Is Nothing Then

                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)

                        If BeStockResActual.Cantidad = 0 Then
                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                        Else

                            clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, pConnection, pTransaction)

                            BeStockResActual.Estado = "UNCOMMITED"
                            FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                        End If

                        Dim BeNuevoStockResPickeado As New clsBeStock_res
                        clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                        BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                        BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(pConnection, pTransaction) + 1
                        BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                        BeNuevoStockResPickeado.Estado = "PICKEADO"
                        clsLnStock_res.Insertar(BeNuevoStockResPickeado, pConnection, pTransaction)

                        Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                        clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                        BeNuevoTransPickingUbic.IdPickingUbic = MaxID(pConnection, pTransaction) + 1
                        BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                        BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                        BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                        BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                        BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                        Insertar(BeNuevoTransPickingUbic, pConnection, pTransaction)

                    End If

                    oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                    oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                    oBeTrans_picking_ubic.Cantidad_Recibida = 0
                    oBeTrans_picking_ubic.Cantidad_Verificada = 0
                    oBeTrans_picking_ubic.Encontrado = False

                    If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                        FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                    Else
                        FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                    End If

                End If

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            resultado += ", terminó la actualizacion"

            Return resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function


    Public Shared Function Actualizar_Picking_Desde_Consolidado(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                                ByVal BeStockResActual As clsBeStock_res,
                                                                ByVal oBeTrans_picking_det As clsBeTrans_picking_det,
                                                                ByVal IdBodega As Integer,
                                                                ByVal pHost As String,
                                                                ByRef pCantidad As Double,
                                                                Optional ByVal pConnection As SqlConnection = Nothing,
                                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As String

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultado As String = ""
        Dim FilasAfectadas As Integer = 0
        Dim bePickingUbicExistente As New clsBeTrans_picking_ubic
        Dim beTransPeDet As New clsBeTrans_pe_det
        Dim BeOnTablePickingUbic As New clsBeTrans_picking_ubic
        Dim Factor As Double = 1
        Dim vIdUbicacionPickingByBodega As Integer = 0
        Dim BeTransPickingUbicStock As New clsBeTrans_picking_ubic_stock
        Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
        Dim BeStockActual As New clsBeStock
        Dim BeNuevoStockPickeado As New clsBeStock

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CKFK20250428 Agregué la funcionalidad de la ubicación de muelle en el picking consolidado
            Dim vMoverProductoAMuelle As Boolean = clsLnTrans_pe_enc.Mover_Producto_A_Muelle_By_IdPedidoEnc(oBeTrans_picking_ubic.IdPedidoEnc,
                                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If vMoverProductoAMuelle Then

                Dim BeBodegaMuelle As New clsBeBodega_muelles
                BeBodegaMuelle = clsLnTrans_picking_enc.Get_BeBodegaMuelle_By_IdPickingEnc(oBeTrans_picking_ubic.IdPickingEnc,
                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                           If(Es_Transaccion_Remota, pTransaction, lTransaction))

                '#EJC20250427: definió un muelle para el picking y el muelle debe tener una ubicación válida para colocar allí el stock pickeado.
                If Not BeBodegaMuelle Is Nothing Then
                    If Not BeBodegaMuelle Is Nothing Then
                        vIdUbicacionPickingByBodega = BeBodegaMuelle.IdUbicacionDefecto

                        If vIdUbicacionPickingByBodega = 0 Then
                            Throw New Exception("Error_20250427: No se encontró la ubicación asociada al muelle")
                        End If
                    End If

                End If

            Else
                'El picking no tiene muelle, colocar el producto en la ubicación de picking por defecto.
                vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(oBeTrans_picking_ubic.IdBodega,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))
                If vIdUbicacionPickingByBodega = 0 Then
                    Throw New Exception("Error_20250427A: La ubicación de picking no está definida para la bodega")
                End If

            End If

            FilasAfectadas = clsLnTrans_picking_det.Actualizar(oBeTrans_picking_det, lConnection, lTransaction)

            If Not Es_Transaccion_Remota Then
                BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(oBeTrans_picking_ubic.IdPickingUbic, lConnection, lTransaction)
            Else
                BeOnTablePickingUbic = Get_PickingUbic_By_IdPickingUbic(oBeTrans_picking_ubic.IdPickingUbic, pConnection, pTransaction)
            End If

            If BeOnTablePickingUbic IsNot Nothing Then

                If Not Es_Transaccion_Remota Then
                    BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock, lConnection, lTransaction)
                Else
                    BeStockActual = clsLnStock.Get_Single_By_IdStock(BeOnTablePickingUbic.IdStock, pConnection, pTransaction)
                End If

                If BeStockActual IsNot Nothing Then

                    If Not Es_Transaccion_Remota Then
                        BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(lConnection, lTransaction) + 1
                    Else
                        BeTransPickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(pConnection, pTransaction) + 1
                    End If

                    Dim vCantidadARestarStockUmBas As Double = 0
                    Dim vCantidadARestarStockPres As Double = 0

                    If (pCantidad) > oBeTrans_picking_ubic.Cantidad_Solicitada Then
                        vCantidadARestarStockUmBas = oBeTrans_picking_ubic.Cantidad_Solicitada
                    Else
                        vCantidadARestarStockUmBas = pCantidad
                    End If

                    If oBeTrans_picking_ubic.IdPresentacion > 0 Then

                        If Not Es_Transaccion_Remota Then
                            Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                       oBeTrans_picking_ubic.IdPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)
                        Else
                            Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(oBeTrans_picking_ubic.IdProductoBodega,
                                                                                       oBeTrans_picking_ubic.IdPresentacion,
                                                                                       pConnection,
                                                                                       pTransaction)
                        End If

                        vCantidadARestarStockPres = vCantidadARestarStockUmBas
                        vCantidadARestarStockUmBas = vCantidadARestarStockUmBas * Factor

                    End If

                    Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(BeOnTablePickingUbic,
                                                                                                      vIdUbicacionPickingByBodega,
                                                                                                      vCantidadARestarStockUmBas,
                                                                                                      If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                      If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    BeTransPickingUbicStock.IdBodega = BeOnTablePickingUbic.IdBodega
                    BeTransPickingUbicStock.IdPickingUbic = BeOnTablePickingUbic.IdPickingUbic
                    BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
                    BeTransPickingUbicStock.IdPickingDet = BeOnTablePickingUbic.IdPickingDet
                    BeTransPickingUbicStock.IdUbicacion = BeOnTablePickingUbic.IdUbicacion
                    BeTransPickingUbicStock.IdUbicacion = oBeTrans_picking_ubic.IdUbicacion
                    BeTransPickingUbicStock.IdStock = BeOnTablePickingUbic.IdStock
                    BeTransPickingUbicStock.IdStockRes = BeOnTablePickingUbic.IdStockRes
                    BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
                    BeTransPickingUbicStock.IdPropietarioBodega = BeOnTablePickingUbic.IdPropietarioBodega
                    BeTransPickingUbicStock.IdProductoBodega = BeOnTablePickingUbic.IdProductoBodega
                    BeTransPickingUbicStock.IdProductoEstado = oBeTrans_picking_ubic.IdProductoEstado
                    BeTransPickingUbicStock.IdPresentacion = oBeTrans_picking_ubic.IdPresentacion
                    BeTransPickingUbicStock.IdUnidadMedida = oBeTrans_picking_ubic.IdUnidadMedida
                    BeTransPickingUbicStock.IdUbicacionAnterior = oBeTrans_picking_ubic.IdUbicacionAnterior
                    BeTransPickingUbicStock.IdRecepcion = BeOnTablePickingUbic.IdRecepcion
                    BeTransPickingUbicStock.IdPedidoEnc = BeOnTablePickingUbic.IdPedidoEnc
                    BeTransPickingUbicStock.IdPedidoDet = BeOnTablePickingUbic.IdPedidoDet
                    BeTransPickingUbicStock.IdPickingEnc = BeOnTablePickingUbic.IdPickingEnc
                    BeTransPickingUbicStock.IdOperadorBodega = oBeTrans_picking_ubic.IdOperadorBodega_Pickeo
                    BeTransPickingUbicStock.IdOperadorBodega_Pickeo = oBeTrans_picking_ubic.IdOperadorBodega_Pickeo
                    BeTransPickingUbicStock.IdOperadorBodega_Verifico = oBeTrans_picking_ubic.IdOperadorBodega_Verifico
                    BeTransPickingUbicStock.Lote = oBeTrans_picking_ubic.Lote
                    BeTransPickingUbicStock.Lote = oBeTrans_picking_ubic.Lote
                    BeTransPickingUbicStock.Fecha_vence = oBeTrans_picking_ubic.Fecha_Vence
                    BeTransPickingUbicStock.Fecha_minima = oBeTrans_picking_ubic.Fecha_minima
                    BeTransPickingUbicStock.Serial = oBeTrans_picking_ubic.Serial
                    BeTransPickingUbicStock.Licencia = oBeTrans_picking_ubic.Lic_plate
                    BeTransPickingUbicStock.Cantidad_recibida = pCantidad
                    BeTransPickingUbicStock.Cantidad_verificada = pCantidad
                    BeTransPickingUbicStock.Fecha_picking = oBeTrans_picking_ubic.Fecha_picking
                    BeTransPickingUbicStock.Fecha_verificado = oBeTrans_picking_ubic.Fecha_verificado
                    BeTransPickingUbicStock.Fecha_verificado = oBeTrans_picking_ubic.Fecha_verificado
                    BeTransPickingUbicStock.Fecha_despachado = oBeTrans_picking_ubic.Fecha_despachado
                    BeTransPickingUbicStock.Cantidad_despachada = oBeTrans_picking_ubic.Cantidad_despachada
                    BeTransPickingUbicStock.User_agr = BeOnTablePickingUbic.User_agr
                    BeTransPickingUbicStock.Fec_agr = BeOnTablePickingUbic.Fec_agr
                    BeTransPickingUbicStock.User_mod = BeOnTablePickingUbic.User_mod
                    BeTransPickingUbicStock.Fec_mod = BeOnTablePickingUbic.Fec_mod
                    BeTransPickingUbicStock.Activo = BeOnTablePickingUbic.Activo
                    BeTransPickingUbicStock.IdUbicacionTemporal = BeOnTablePickingUbic.IdUbicacionTemporal
                    BeTransPickingUbicStock.IdOperadorBodega_Asignado = BeOnTablePickingUbic.IdOperadorBodega_Asignado
                    BeTransPickingUbicStock.Host = pHost
                    BeTransPickingUbicStock.IdMovimiento = vIdMovimiento

                    If Not Es_Transaccion_Remota Then
                        clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock, lConnection, lTransaction)
                    Else
                        clsLnTrans_picking_ubic_stock.Insertar(BeTransPickingUbicStock, pConnection, pTransaction)
                    End If

                    If BeOnTablePickingUbic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Recibida Then

                        If BeStockActual.Cantidad = vCantidadARestarStockUmBas Then

                            If Not Es_Transaccion_Remota Then
                                FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
                                                                                              oBeTrans_picking_ubic.IdUbicacion,
                                                                                              oBeTrans_picking_ubic.IdBodega,
                                                                                              oBeTrans_picking_ubic.IdStock,
                                                                                              lConnection,
                                                                                              lTransaction)

                                BeStockResActual.Estado = "PICKEADO"
                                FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                            Else
                                FilasAfectadas = clsLnStock.Actualizar_IdUbicacion_By_IdStock(vIdUbicacionPickingByBodega,
                                                                                              oBeTrans_picking_ubic.IdUbicacion,
                                                                                              oBeTrans_picking_ubic.IdBodega,
                                                                                              oBeTrans_picking_ubic.IdStock,
                                                                                              pConnection,
                                                                                              pTransaction)

                                BeStockResActual.Estado = "PICKEADO"
                                FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                            End If

                            If Not Es_Transaccion_Remota Then
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, lConnection, lTransaction)
                            Else
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                            End If

                        Else

                            BeNuevoStockPickeado = New clsBeStock
                            clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                            BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
                            BeNuevoStockPickeado.IdUbicacion_anterior = BeStockActual.IdUbicacion
                            BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega

                            If Not Es_Transaccion_Remota Then
                                BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1
                            Else
                                BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(pConnection, pTransaction) + 1
                            End If

                            BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                            BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion

                            If Not Es_Transaccion_Remota Then
                                clsLnStock.Insertar(BeNuevoStockPickeado, lConnection, lTransaction)
                            Else
                                clsLnStock.Insertar(BeNuevoStockPickeado, pConnection, pTransaction)
                            End If

                            BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad - vCantidadARestarStockUmBas, 6)
                            BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad, 6)

                            If Not Es_Transaccion_Remota Then
                                If BeStockActual.Cantidad = 0 Then
                                    clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, lConnection, lTransaction)
                                Else
                                    clsLnStock.Actualizar_Cantidad(BeStockActual, lConnection, lTransaction)
                                End If
                            Else
                                If BeStockActual.Cantidad = 0 Then
                                    clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, pConnection, pTransaction)
                                Else
                                    clsLnStock.Actualizar_Cantidad(BeStockActual, pConnection, pTransaction)
                                End If
                            End If

                            If Not Es_Transaccion_Remota Then

                                If Not BeStockResActual Is Nothing Then

                                    If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                        BeStockResActual.Cantidad = 0
                                    Else
                                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                    End If

                                    If BeStockResActual.Cantidad = 0 Then
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                                    Else

                                        clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, lConnection, lTransaction)

                                        BeStockResActual.Estado = "UNCOMMITED"
                                        FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                                    End If

                                    Dim BeNuevoStockResPickeado As New clsBeStock_res
                                    clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                    BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                    BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1
                                    BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                    BeNuevoStockResPickeado.Estado = "PICKEADO"
                                    clsLnStock_res.Insertar(BeNuevoStockResPickeado, lConnection, lTransaction)

                                    Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                    clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                    BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                                    BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                    BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                    BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                    BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                    BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                    Insertar(BeNuevoTransPickingUbic, lConnection, lTransaction)

                                End If

                                oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                                oBeTrans_picking_ubic.Cantidad_Recibida = 0
                                oBeTrans_picking_ubic.Cantidad_Verificada = 0
                                oBeTrans_picking_ubic.Encontrado = False

                                If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                    FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                                Else
                                    FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                                End If

                            Else

                                If Not BeStockResActual Is Nothing Then

                                    If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                        BeStockResActual.Cantidad = 0
                                    Else
                                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                        BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                    End If

                                    If BeStockResActual.Cantidad = 0 Then
                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, pConnection, pTransaction)
                                    Else

                                        clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, pConnection, pTransaction)

                                        BeStockResActual.Estado = "UNCOMMITED"
                                        FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                                    End If

                                    Dim BeNuevoStockResPickeado As New clsBeStock_res
                                    clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                    BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                    BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(pConnection, pTransaction) + 1
                                    BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                    BeNuevoStockResPickeado.Estado = "PICKEADO"
                                    clsLnStock_res.Insertar(BeNuevoStockResPickeado, pConnection, pTransaction)

                                    Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                    clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                    BeNuevoTransPickingUbic.IdPickingUbic = MaxID(pConnection, pTransaction) + 1
                                    BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                    BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                    BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                    BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                    BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                    Insertar(BeNuevoTransPickingUbic, pConnection, pTransaction)

                                End If

                                oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                                oBeTrans_picking_ubic.Cantidad_Recibida = 0
                                oBeTrans_picking_ubic.Cantidad_Verificada = 0
                                oBeTrans_picking_ubic.Encontrado = False

                                If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                    FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                                Else
                                    FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                                End If

                            End If

                        End If

                    Else

                        'Es un picking parcial?
                        BeNuevoStockPickeado = New clsBeStock
                        clsPublic.CopyObject(BeStockActual, BeNuevoStockPickeado)
                        BeNuevoStockPickeado.Cantidad = vCantidadARestarStockUmBas
                        BeNuevoStockPickeado.IdUbicacion = vIdUbicacionPickingByBodega

                        If Not Es_Transaccion_Remota Then
                            BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(lConnection, lTransaction) + 1
                        Else
                            BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(pConnection, pTransaction) + 1
                        End If

                        BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockActual.IdProductoEstado
                        BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockActual.IdPresentacion

                        If Not Es_Transaccion_Remota Then
                            clsLnStock.Insertar(BeNuevoStockPickeado, lConnection, lTransaction)
                        Else
                            clsLnStock.Insertar(BeNuevoStockPickeado, pConnection, pTransaction)
                        End If

                        BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad - vCantidadARestarStockUmBas, 6)
                        BeStockActual.Cantidad = Math.Round(BeStockActual.Cantidad, 6)

                        If Not Es_Transaccion_Remota Then
                            If BeStockActual.Cantidad = 0 Then
                                clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, lConnection, lTransaction)
                            Else
                                clsLnStock.Actualizar_Cantidad(BeStockActual, lConnection, lTransaction)
                            End If
                        Else
                            If BeStockActual.Cantidad = 0 Then
                                clsLnStock.Eliminar_By_IdStock(BeStockResActual.IdStock, pConnection, pTransaction)
                            Else
                                clsLnStock.Actualizar_Cantidad(BeStockActual, pConnection, pTransaction)
                            End If
                        End If

                        If Not Es_Transaccion_Remota Then

                            If Not BeStockResActual Is Nothing Then

                                If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                    BeStockResActual.Cantidad = 0
                                Else
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                End If

                                If BeStockResActual.Cantidad = 0 Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                                Else

                                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, lConnection, lTransaction)

                                    BeStockResActual.Estado = "UNCOMMITED"
                                    FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, lConnection, lTransaction)

                                End If

                                Dim BeNuevoStockResPickeado As New clsBeStock_res
                                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction) + 1
                                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoStockResPickeado.Estado = "PICKEADO"
                                clsLnStock_res.Insertar(BeNuevoStockResPickeado, lConnection, lTransaction)

                                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                                BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                Insertar(BeNuevoTransPickingUbic, lConnection, lTransaction)

                            End If

                            oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                            oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                            oBeTrans_picking_ubic.Cantidad_Recibida = 0
                            oBeTrans_picking_ubic.Cantidad_Verificada = 0
                            oBeTrans_picking_ubic.Encontrado = False

                            If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                            Else
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                            End If

                        Else

                            BeStockResActual = clsLnStock_res.GetSingle_By_IdStockRes(oBeTrans_picking_ubic.IdBodega,
                                                                              oBeTrans_picking_ubic.IdStockRes,
                                                                              pConnection,
                                                                              pTransaction)

                            If Not BeStockResActual Is Nothing Then

                                BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)

                                If BeStockResActual.Cantidad = 0 Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes, lConnection, lTransaction)
                                Else

                                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual, pConnection, pTransaction)

                                    BeStockResActual.Estado = "UNCOMMITED"
                                    FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual, pConnection, pTransaction)

                                End If

                                Dim BeNuevoStockResPickeado As New clsBeStock_res
                                clsPublic.CopyObject(BeStockResActual, BeNuevoStockResPickeado)
                                BeNuevoStockResPickeado.Cantidad = vCantidadARestarStockUmBas
                                BeNuevoStockResPickeado.IdStockRes = clsLnStock_res.MaxID(pConnection, pTransaction) + 1
                                BeNuevoStockResPickeado.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoStockResPickeado.Estado = "PICKEADO"
                                clsLnStock_res.Insertar(BeNuevoStockResPickeado, pConnection, pTransaction)

                                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                clsPublic.CopyObject(oBeTrans_picking_ubic, BeNuevoTransPickingUbic)
                                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(pConnection, pTransaction) + 1
                                BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(oBeTrans_picking_ubic.Cantidad_Verificada > 0, IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                BeNuevoTransPickingUbic.IdStock = BeNuevoStockPickeado.IdStock
                                BeNuevoTransPickingUbic.IdStockRes = BeNuevoStockResPickeado.IdStockRes
                                Insertar(BeNuevoTransPickingUbic, pConnection, pTransaction)

                            End If

                            oBeTrans_picking_ubic.Cantidad_Solicitada = oBeTrans_picking_ubic.Cantidad_Solicitada - IIf(oBeTrans_picking_ubic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                            oBeTrans_picking_ubic.Cantidad_Solicitada = Math.Round(oBeTrans_picking_ubic.Cantidad_Solicitada, 6)
                            oBeTrans_picking_ubic.Cantidad_Recibida = 0
                            oBeTrans_picking_ubic.Cantidad_Verificada = 0
                            oBeTrans_picking_ubic.Encontrado = False

                            If oBeTrans_picking_ubic.Cantidad_Solicitada = 0 Then
                                FilasAfectadas = Eliminar_By_IdPickingUbic(oBeTrans_picking_ubic, pConnection, pTransaction)
                            Else
                                FilasAfectadas = Actualizar_Cantidad_Recibida(oBeTrans_picking_ubic, pConnection, pTransaction)
                            End If

                        End If

                    End If

                    Dim Verificacion_Auto As Boolean = clsLnTrans_picking_enc.Get_Verificacion_Auto_By_IdPickingEnc(BeOnTablePickingUbic.IdPickingEnc,
                                                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    If Verificacion_Auto Then

                        clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(BeOnTablePickingUbic,
                                                                                vIdUbicacionPickingByBodega,
                                                                                vCantidadARestarStockUmBas,
                                                                                BeOnTablePickingUbic.Peso_recibido,
                                                                                IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

                    End If

                Else
                    Throw New Exception("No existe el idstock " & oBeTrans_picking_ubic.IdStock)
                End If

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            resultado += ", terminó la actualizacion"

            Return resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_PickingUbic_Por_Verificacion(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                                   ByVal BeStockRes As clsBeStock_res,
                                                                   Optional ByRef pCantidad As Double = Nothing,
                                                                   Optional ByRef pPeso As Double = Nothing,
                                                                   Optional ByRef pConection As SqlConnection = Nothing,
                                                                   Optional ByRef pTransaction As SqlTransaction = Nothing) As String

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim resultado As String = ""
        Dim FilasAfectadas As Integer = 0

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If oBeTrans_picking_ubic.Cantidad_Verificada <> 0 Then

            resultado += "Inicia la actualizacion"

            FilasAfectadas = Actualizar(oBeTrans_picking_ubic,
                                        IIf(Es_Transaccion_Remota, pConection, lConnection),
                                        IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

                resultado += String.Format(", actualizó {0} filas en trans_picking_ubic, cantidad {1}, operador {2}, pedido {3}, pedidodet {4} IdPickingUbic {5} ",
                                           FilasAfectadas.ToString, oBeTrans_picking_ubic.Cantidad_Verificada,
                                           oBeTrans_picking_ubic.IdOperadorBodega_Verifico, oBeTrans_picking_ubic.IdPedidoEnc,
                                           oBeTrans_picking_ubic.IdPedidoDet, oBeTrans_picking_ubic.IdPickingUbic)

            FilasAfectadas = clsLnStock_res.Actualizar(BeStockRes,
                                                       IIf(Es_Transaccion_Remota, pConection, lConnection),
                                                       IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            resultado += String.Format(", actualizó {0} filas en stock_res ", FilasAfectadas.ToString)

            Dim BeStock As New clsBeStock
                BeStock = clsLnStock.Get_Single_By_IdStock(BeStockRes.IdStock,
                                                           IIf(Es_Transaccion_Remota, pConection, lConnection),
                                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            FilasAfectadas = clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(oBeTrans_picking_ubic,
                                                                                     BeStock.IdUbicacion,
                                                                                     pCantidad,
                                                                                     pPeso,
                                                                                     IIf(Es_Transaccion_Remota, pConection, lConnection),
                                                                                     IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            resultado += String.Format(", actualizó {0} filas en trans_movimientos ", FilasAfectadas.ToString)

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            resultado += ", terminó la actualizacion"

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return resultado

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_By_IdPickingDet(ByVal IdPickingDet As Integer,
                                                    ByVal pConection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = " Delete from Trans_picking_ubic
                                   Where(IdPickingDet = @IdPickingDet)"

            Dim cmd As New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdPickingDet", IdPickingDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pIdPickingDet:=IdPickingDet, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Sub Procesar_Picking_Desde_BOF(ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic),
                                                 ByVal Usuario As Integer,
                                                 ByVal pListBeTrans_picking_det As List(Of clsBeTrans_picking_det),
                                                 ByVal BePickingEnc As clsBeTrans_picking_enc,
                                                 ByVal pListBeStockRes As List(Of clsBeStock_res),
                                                 Optional ByVal pConnection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing
        Dim FilasAfectadas As Integer = 0
        Dim vIdUbicacionPickingByBodega As Integer = 0
        Dim vIdUbicacionMuelle As Integer = 0
        Dim vUsarCantidadUmBas As Double = 0

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim listaIdPedidos As List(Of Integer) = pListBePickingUbic _
                                                    .Select(Function(x) Convert.ToInt32(x.IdPedidoEnc)) _
                                                    .Distinct() _
                                                    .ToList()

            '#EJC20250427: Prioizar ubicación de muelle
            If Not BePickingEnc.IdBodegaMuelle = 0 Then

                Dim vMover_A_Muelle = clsLnTrans_picking_enc.Mover_Producto_A_Muelle_By_ListaPedidos(listaIdPedidos,
                                                                                                     If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                     If(Es_Transaccion_Remota, pTransaction, ltransaction))

                If vMover_A_Muelle Then
                    vIdUbicacionMuelle = clsLnBodega_muelles.Get_IdUbicacion_By_IdMuelle(BePickingEnc.IdBodega,
                                                                                                        BePickingEnc.IdBodegaMuelle,
                                                                                                        If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                        If(Es_Transaccion_Remota, pTransaction, ltransaction))
                    If vIdUbicacionMuelle = 0 Then
                        Throw New Exception("Error_20250419A: El muelle seleccionado para el picking no tiene ubicación definida (Definia la ubicación para el muelle en el mantenimiento)")
                    End If
                End If

            End If

            '#EJC20250427: Si no se definió muelle, tomar la ubicación de picking de la bodega.
            vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(BePickingEnc.IdBodega,
                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                              If(Es_Transaccion_Remota, pTransaction, ltransaction))
            If vIdUbicacionPickingByBodega = 0 Then
                Throw New Exception("Error_20250419: No se encontró la ubicación de picking por bodega")
            End If


            If pListBePickingUbic.Count > 0 Then

                For Each PickingUbic In pListBePickingUbic

                    Dim lPickingUbicConsolidado As List(Of clsBeTrans_picking_ubic) = Get_All_PickingUbic_By_Consolidado(PickingUbic,
                                                                                                                        If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                                        If(Es_Transaccion_Remota, pTransaction, ltransaction))

                    Dim CantidadPendiente As Double = PickingUbic.Cantidad_Recibida
                    Dim PesoPendiente As Double = PickingUbic.Peso_recibido

                For Each vBePickingUbic As clsBeTrans_picking_ubic In lPickingUbicConsolidado.FindAll(Function(x) x.IdPickingUbic = PickingUbic.IdPickingUbic)

                        Dim CantPendiente As Double = Math.Round(Math.Max(0, vBePickingUbic.Cantidad_Solicitada - vBePickingUbic.Cantidad_Recibida), 6)
                        Dim PesoPend As Double = Math.Round(Math.Max(0, vBePickingUbic.Peso_solicitado - vBePickingUbic.Peso_recibido), 6)

                        '#CKFK20250525 Agregué validación para saber si algo está pendiente de pickearse
                        If CantPendiente > 0 Then

                            Dim UsarCantidad As Double = Math.Max(CantPendiente, CantidadPendiente)
                            Dim UsarPeso As Double = Math.Max(PesoPend, PesoPendiente)

                            vBePickingUbic.Cantidad_Recibida += IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                            vBePickingUbic.Peso_recibido += IIf(UsarPeso = 0, PesoPend, UsarPeso)
                            vBePickingUbic.Cantidad_Recibida = Math.Round(vBePickingUbic.Cantidad_Recibida, 6)
                            vBePickingUbic.Cantidad_Verificada = IIf(BePickingEnc.verifica_auto, Math.Round(vBePickingUbic.Cantidad_Recibida, 6), 0)
                            vBePickingUbic.Peso_recibido = Math.Round(vBePickingUbic.Peso_recibido, 6)
                            vBePickingUbic.IdOperadorBodega_Pickeo = Usuario
                            vBePickingUbic.Fec_mod = Now
                            vBePickingUbic.Fecha_picking = Now
                            vBePickingUbic.IdProductoTallaColor = PickingUbic.IdProductoTallaColor

                            PickingUbic.Cantidad_Recibida = vBePickingUbic.Cantidad_Recibida
                            PickingUbic.Peso_recibido = vBePickingUbic.Peso_recibido

                            Dim BePickingDet As New clsBeTrans_picking_det
                            BePickingDet.IdPickingDet = vBePickingUbic.IdPickingDet
                            clsLnTrans_picking_det.Obtener(BePickingDet, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            If BePickingDet Is Nothing Then
                                Throw New Exception("Error_20250419: No se pudo obtener el picking_det")
                            End If

                            Dim vMoverProductoAMuelle As Boolean = clsLnTrans_pe_enc.Mover_Producto_A_Muelle_By_IdPedidoEnc(BePickingDet.IdPedidoEnc,
                                                                                                                        If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                                        If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            Dim BePedidoDet As clsBeTrans_pe_det = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(BePickingDet.IdPedidoEnc,
                                                                                                                           BePickingDet.IdPedidoDet,
                                                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                                           If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            Dim Factor As Integer = 1
                            Dim vCantidadARestarStockUmBas As Double = 0
                            Dim vCantidadARestarStockPres As Double = 0

                            If (UsarCantidad) > vBePickingUbic.Cantidad_Solicitada Then
                                vCantidadARestarStockUmBas = vBePickingUbic.Cantidad_Solicitada
                            Else
                                vCantidadARestarStockUmBas = UsarCantidad
                            End If

                            If vBePickingUbic.IdPresentacion > 0 Then

                                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBePickingUbic.IdProductoBodega,
                                                                                               vBePickingUbic.IdPresentacion,
                                                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                               If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                vCantidadARestarStockPres = vCantidadARestarStockUmBas
                                vCantidadARestarStockUmBas = vCantidadARestarStockUmBas * Factor
                                vUsarCantidadUmBas = UsarCantidad * Factor
                            Else
                                vUsarCantidadUmBas = UsarCantidad
                            End If

                            If BePedidoDet.IdPresentacion <> vBePickingUbic.IdPresentacion Then
                                If BePedidoDet.IdPresentacion = 0 AndAlso vBePickingUbic.IdPresentacion <> 0 Then
                                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBePickingUbic.IdProductoBodega,
                                                                                                   vBePickingUbic.IdPresentacion,
                                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                   If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                    BePickingDet.Cantidad_recibida += IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor
                                End If
                            Else
                                BePickingDet.Cantidad_recibida += IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                            End If

                            BePickingDet.Cantidad_recibida = Math.Round(BePickingDet.Cantidad_recibida, 6)
                            BePickingDet.User_mod = Usuario
                            BePickingDet.Fec_mod = Now

                            clsLnTrans_picking_det.Actualizar_Cantidad_Recibida(BePickingDet,
                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                            If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            Dim BeStockResActual As clsBeStock_res = clsLnStock_res.GetSingle_By_IdStockRes(vBePickingUbic.IdBodega,
                                                                                                        vBePickingUbic.IdStockRes,
                                                                                                        If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                        If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            If BeStockResActual Is Nothing Then
                                Throw New Exception("Error_20250419A: No se pudo obtener el BeStockResActual")
                            End If

                            Dim BeStockOriginal As clsBeStock = clsLnStock.Get_Single_By_IdStock(vBePickingUbic.IdStock, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            If BeStockOriginal Is Nothing Then
                                Throw New Exception("Error_20250419B: No se pudo obtener el BeStockOriginal")
                            End If

                            Dim TotalIgual = (Math.Round(BeStockOriginal.Cantidad, 6) = Math.Round(IIf(vUsarCantidadUmBas = 0, CantidadPendiente, vUsarCantidadUmBas), 6)) AndAlso
                                     (Math.Round(BeStockResActual.Cantidad, 6) = Math.Round(IIf(vUsarCantidadUmBas = 0, CantidadPendiente, vUsarCantidadUmBas), 6))

                            If TotalIgual Then

                                ' Solo actualizar estado y transiciones
                                BeStockResActual.Estado = If(BePickingEnc.verifica_auto, "VERIFICADO", "PICKEADO")
                                BeStockResActual.User_mod = Usuario
                                BeStockResActual.Fec_mod = Now
                                clsLnStock_res.Actualizar(BeStockResActual,
                                                      If(Es_Transaccion_Remota, pConnection, lConnection),
                                                      If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                vBePickingUbic.Fec_mod = Now
                                vBePickingUbic.User_mod = Usuario
                                Actualizar_Cantidad_Recibida(vBePickingUbic,
                                                         If(Es_Transaccion_Remota, pConnection, lConnection),
                                                         If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(PickingUbic,
                                                                                                              IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                                                                                                              IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor,
                                                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                              If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                clsLnStock.Actualizar_IdUbicacion_By_IdStock(IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                                                                         vBePickingUbic.IdUbicacion,
                                                                         vBePickingUbic.IdBodega,
                                                                         vBePickingUbic.IdStock,
                                                                         If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                         If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            'Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(PickingUbic,
                            '                                                                                  IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                            '                                                                                  IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor,
                            '                                                                                  If(Es_Transaccion_Remota, pConnection, lConnection),
                            '                                                                                  If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                ' Insertar picking_ubic_stock
                                Dim BePickingUbicStock As New clsBeTrans_picking_ubic_stock
                                clsPublic.CopyObject(vBePickingUbic, BePickingUbicStock)
                                BePickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction)) + 1
                                BePickingUbicStock.IdPickingUbic = vBePickingUbic.IdPickingUbic
                                BePickingUbicStock.IdPickingDet = vBePickingUbic.IdPickingDet
                                BePickingUbicStock.IdUbicacion = vBePickingUbic.IdUbicacion
                                BePickingUbicStock.IdStock = BeStockResActual.IdStock
                                BePickingUbicStock.Cantidad_recibida = IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                                BePickingUbicStock.Cantidad_verificada = If(BePickingEnc.verifica_auto, UsarCantidad, 0)
                                BePickingUbicStock.Fecha_picking = Now
                                BePickingUbicStock.User_agr = Usuario
                                BePickingUbicStock.Fec_agr = Now
                                BePickingUbicStock.Activo = True
                                BePickingUbicStock.IdMovimiento = vIdMovimiento
                                clsLnTrans_picking_ubic_stock.Insertar(BePickingUbicStock,
                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                   If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            Else

                                Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(PickingUbic,
                                                                                                              IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                                                                                                              IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor,
                                                                                                              IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                              IIf(Es_Transaccion_Remota, pTransaction, ltransaction))

                                ' Crear nuevo stock
                                Dim BeNuevoStockPickeado As New clsBeStock
                                clsPublic.CopyObject(BeStockOriginal, BeNuevoStockPickeado)
                                BeNuevoStockPickeado.Cantidad = IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor
                                BeNuevoStockPickeado.Peso = IIf(UsarPeso = 0, PesoPend, UsarPeso)
                                BeNuevoStockPickeado.IdUbicacion = IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega)
                                BeNuevoStockPickeado.IdStock = clsLnStock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction)) + 1
                                BeNuevoStockPickeado.ProductoEstado.IdEstado = BeStockOriginal.IdProductoEstado
                                BeNuevoStockPickeado.Presentacion.IdPresentacion = BeStockOriginal.IdPresentacion
                                BeNuevoStockPickeado.IdProductoTallaColor = PickingUbic.IdProductoTallaColor
                                clsLnStock.Insertar(BeNuevoStockPickeado, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                ' Actualizar stock original
                                BeStockOriginal.Cantidad = Math.Round(BeStockOriginal.Cantidad, 6) - Math.Round(BeNuevoStockPickeado.Cantidad, 6)
                                If BeStockOriginal.Cantidad = 0 Then
                                    clsLnStock.Eliminar_By_IdStock(BeStockOriginal.IdStock,
                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                               If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                Else
                                    clsLnStock.Actualizar_Cantidad(BeStockOriginal,
                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                               If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                End If

                                ' Crear nueva reserva
                                Dim BeStockResNuevo As New clsBeStock_res
                                clsPublic.CopyObject(BeStockResActual, BeStockResNuevo)
                                BeStockResNuevo.IdStockRes = clsLnStock_res.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction)) + 1
                                BeStockResNuevo.IdStock = BeNuevoStockPickeado.IdStock
                                BeStockResNuevo.Cantidad = IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                                BeStockResNuevo.Peso = IIf(UsarPeso = 0, PesoPend, UsarPeso)
                                BeStockResNuevo.Fec_agr = Now
                                BeStockResNuevo.User_agr = Usuario
                                BeStockResNuevo.Estado = If(BePickingEnc.verifica_auto, "VERIFICADO", "PICKEADO")
                                BeStockResNuevo.IdProductoTallaColor = PickingUbic.IdProductoTallaColor
                                clsLnStock_res.Insertar(BeStockResNuevo, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                If vCantidadARestarStockUmBas > BeStockResActual.Cantidad Then
                                    BeStockResActual.Cantidad = 0
                                Else
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad - vCantidadARestarStockUmBas, 6)
                                    BeStockResActual.Cantidad = Math.Round(BeStockResActual.Cantidad, 6)
                                End If

                                If BeStockResActual.Cantidad = 0 Then
                                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(BeStockResActual.IdStockRes,
                                                                                      If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                      If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                Else

                                    clsLnStock_res.Actualizar_Cantidad_By_IdStock(BeStockResActual,
                                                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                              If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                    BeStockResActual.Estado = "UNCOMMITED"
                                    FilasAfectadas = clsLnStock_res.Actualizar_Estado_Pickeado(BeStockResActual,
                                                                                           If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                           If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                End If

                            'Dim vIdMovimiento As Integer = clsLnTrans_movimientos.Insertar_Movimiento_Picking(PickingUbic,
                            '                                                                                  IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                            '                                                                                  IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor,
                            '                                                                                  IIf(Es_Transaccion_Remota, pConnection, lConnection),
                            '                                                                                  IIf(Es_Transaccion_Remota, pTransaction, ltransaction))

                                ' Insertar picking_ubic_stock
                                Dim BePickingUbicStock As New clsBeTrans_picking_ubic_stock
                                clsPublic.CopyObject(vBePickingUbic, BePickingUbicStock)
                                BePickingUbicStock.IdPickingUbicStock = clsLnTrans_picking_ubic_stock.MaxID(If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction)) + 1
                                BePickingUbicStock.IdPickingUbic = vBePickingUbic.IdPickingUbic
                                BePickingUbicStock.IdPickingDet = vBePickingUbic.IdPickingDet
                                BePickingUbicStock.IdUbicacion = vBePickingUbic.IdUbicacion
                                BePickingUbicStock.IdStock = BeNuevoStockPickeado.IdStock
                                BePickingUbicStock.Cantidad_recibida = IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                                BePickingUbicStock.Cantidad_verificada = If(BePickingEnc.verifica_auto, UsarCantidad, 0)
                                BePickingUbicStock.Fecha_picking = Now
                                BePickingUbicStock.User_agr = Usuario
                                BePickingUbicStock.Fec_agr = Now
                                BePickingUbicStock.Activo = True
                                BePickingUbicStock.IdMovimiento = vIdMovimiento
                                clsLnTrans_picking_ubic_stock.Insertar(BePickingUbicStock,
                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                   If(Es_Transaccion_Remota, pTransaction, ltransaction))

                                Dim BeNuevoTransPickingUbic As New clsBeTrans_picking_ubic
                                clsPublic.CopyObject(vBePickingUbic, BeNuevoTransPickingUbic)
                                BeNuevoTransPickingUbic.IdPickingUbic = MaxID(lConnection, ltransaction) + 1
                                BeNuevoTransPickingUbic.Cantidad_Solicitada = IIf(vBePickingUbic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Recibida = IIf(vBePickingUbic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                BeNuevoTransPickingUbic.Cantidad_Verificada = IIf(vBePickingUbic.Cantidad_Verificada > 0, IIf(vBePickingUbic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas), 0)
                                BeNuevoTransPickingUbic.Peso_recibido = IIf(UsarPeso = 0, PesoPend, UsarPeso)
                                BeNuevoTransPickingUbic.IdStock = BePickingUbicStock.IdStock
                                BeNuevoTransPickingUbic.IdStockRes = BeStockResNuevo.IdStockRes
                                BeNuevoTransPickingUbic.IdProductoTallaColor = PickingUbic.IdProductoTallaColor
                                Insertar(BeNuevoTransPickingUbic, lConnection, ltransaction)

                                vBePickingUbic.Cantidad_Solicitada = vBePickingUbic.Cantidad_Solicitada - IIf(vBePickingUbic.IdPresentacion > 0, vCantidadARestarStockPres, vCantidadARestarStockUmBas)
                                vBePickingUbic.Cantidad_Solicitada = Math.Round(vBePickingUbic.Cantidad_Solicitada, 6)
                                vBePickingUbic.Cantidad_Recibida = 0
                                vBePickingUbic.Cantidad_Verificada = 0
                                vBePickingUbic.Peso_recibido = 0
                                vBePickingUbic.Encontrado = False

                                If vBePickingUbic.Cantidad_Solicitada = 0 Then
                                    FilasAfectadas += Eliminar_By_IdPickingUbic(vBePickingUbic,
                                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                            If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                Else
                                    FilasAfectadas += Actualizar_Cantidad_Recibida(vBePickingUbic,
                                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                               If(Es_Transaccion_Remota, pTransaction, ltransaction))
                                End If

                            End If

                            If BePickingEnc.verifica_auto Then
                                clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(PickingUbic,
                                                                                    IIf(vMoverProductoAMuelle, vIdUbicacionMuelle, vIdUbicacionPickingByBodega),
                                                                                    IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad) * Factor,
                                                                                    IIf(UsarPeso = 0, PesoPend, UsarPeso),
                                                                                    IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                    IIf(Es_Transaccion_Remota, pTransaction, ltransaction))
                            End If

                            CantidadPendiente -= IIf(UsarCantidad = 0, CantidadPendiente, UsarCantidad)
                            PesoPendiente -= UsarPeso

                        End If

                        If (vBePickingUbic.Cantidad_Recibida > 0 AndAlso vBePickingUbic.Cantidad_Recibida <> vBePickingUbic.Cantidad_Verificada) AndAlso BePickingEnc.verifica_auto Then

                            Dim CantidadStockDestino As Double

                            PickingUbic.Dañado_verificacion = False
                            PickingUbic.Cantidad_Verificada = PickingUbic.Cantidad_Recibida
                            PickingUbic.Peso_verificado = PickingUbic.Peso_recibido
                            PickingUbic.User_mod = Usuario
                            PickingUbic.Fec_mod = Now

                            CantidadStockDestino = PickingUbic.Cantidad_Solicitada

                            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(PickingUbic.IdBodega, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))
                            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

                            Actualizar(PickingUbic,
                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                   If(Es_Transaccion_Remota, pTransaction, ltransaction))

                            Dim BeStock As New clsBeStock
                            BeStock = clsLnStock.Get_Single_By_IdStock(PickingUbic.IdStock, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltransaction))

                            clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(PickingUbic,
                                                                                    BeStock.IdUbicacion,
                                                                                    PickingUbic.Cantidad_Recibida,
                                                                                    PickingUbic.Peso_recibido,
                                                                                    If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                    If(Es_Transaccion_Remota, pTransaction, ltransaction))

                        End If

                        If CantidadPendiente <= 0 Then Exit For

                    Next

                Next

                ' Actualizar encabezado de picking
                BePickingEnc.Estado = If(BePickingEnc.verifica_auto, "Verificado", "Procesado")
                BePickingEnc.Fec_mod = Now
                BePickingEnc.User_mod = Usuario
                BePickingEnc.Hora_fin = Now

                clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc,
                                                         If(Es_Transaccion_Remota, pConnection, lConnection),
                                                         If(Es_Transaccion_Remota, pTransaction, ltransaction))

                '#EJC20250425: Actualizar el estado del pedido.
                For Each IdPedidoEnc In listaIdPedidos

                    clsLnTrans_pe_enc.Actualizar_Estado(IdPedidoEnc,
                                                        IIf(BePickingEnc.verifica_auto, "Verificado", "Pickeado"),
                                                        If(Es_Transaccion_Remota, pConnection, lConnection),
                                                        If(Es_Transaccion_Remota, pTransaction, ltransaction))

                Next

            Else
                Throw New Exception("No se pudo procesar el picking, la lista de ubicaciones es inconsistente.")
            End If
            '#EJC20250701: Finalizar la tarea de picking generada para la hh (Erik, arregla esto. att erik del pasado).
            'clsLnTarea_hh.Guardar_Tarea_Picking_HH(pBeTareaHH,
            '                                       lConnection,
            '                                       ltransaction)


            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso ltransaction IsNot Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If ltransaction IsNot Nothing Then ltransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Sub

    Public Shared Sub Procesar_Verificacion_Desde_BOF(ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic),
                                                      ByVal Usuario As Integer,
                                                      ByVal pListBeTrans_picking_det As List(Of clsBeTrans_picking_det),
                                                      ByVal BePickingEnc As clsBeTrans_picking_enc,
                                                      ByVal pListBeStockRes As List(Of clsBeStock_res),
                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim pListBeTransPeEnc As New List(Of clsBeTrans_pe_enc)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing
        Dim CantidadStockDestino As Double = 0

        Try
            If Not Es_Transaccion_Remota Then lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each PickingUbic In pListBePickingUbic

                If PickingUbic.Cantidad_Recibida > 0 Then

                    PickingUbic.Dañado_verificacion = False
                    PickingUbic.Cantidad_Verificada = PickingUbic.Cantidad_Recibida
                    PickingUbic.Peso_verificado = PickingUbic.Peso_recibido
                    PickingUbic.User_mod = Usuario
                    PickingUbic.Fec_mod = Now

                    CantidadStockDestino = PickingUbic.Cantidad_Solicitada

                    Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(PickingUbic.IdBodega, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))
                    clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

                    Actualizar(PickingUbic,
                               If(Es_Transaccion_Remota, pConection, lConnection),
                               If(Es_Transaccion_Remota, pTransaction, ltransaction))

                    Dim BeStock As New clsBeStock
                    BeStock = clsLnStock.Get_Single_By_IdStock(PickingUbic.IdStock, IIf(Es_Transaccion_Remota, pConection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, ltransaction))

                    If BeStock IsNot Nothing Then

                    clsLnTrans_movimientos.Insertar_Movimiento_Verificacion(PickingUbic,
                                                                            BeStock.IdUbicacion,
                                                                            PickingUbic.Cantidad_Recibida,
                                                                            PickingUbic.Peso_recibido,
                                                                            If(Es_Transaccion_Remota, pConection, lConnection),
                                                                            If(Es_Transaccion_Remota, pTransaction, ltransaction))

                End If

                End If

            Next

            For Each stock In pListBeStockRes
                stock.Estado = "VERIFICADO"
                stock.User_mod = Usuario
                stock.Fec_mod = Now
                clsLnStock_res.Actualizar(stock, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction))
            Next

            If (clsLnStock_res.Get_All_Pickeados_Verificados_By_IdPickingEnc(BePickingEnc.IdPickingEnc, If(Es_Transaccion_Remota, pConection, lConnection), If(Es_Transaccion_Remota, pTransaction, ltransaction)).Count = 0) Then
                BePickingEnc.Estado = "Verificado"
            Else
                BePickingEnc.Estado = "Procesado"
            End If

            BePickingEnc.Fec_mod = Now
            BePickingEnc.User_mod = Usuario
            BePickingEnc.Hora_fin = Now

            clsLnTrans_picking_enc.Set_Pedidos_Verificados(BePickingEnc,
                                                           Usuario,
                                                           If(Es_Transaccion_Remota, pConection, lConnection),
                                                           If(Es_Transaccion_Remota, pTransaction, ltransaction))

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso Not ltransaction Is Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try
    End Sub

    Public Shared Sub Marcar_Linea_No_Pickeada(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                               ByVal Usuario As Integer,
                                               Optional ByVal pConnection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim IdPedidoDet As Integer = 0
        Dim BeTransPeDet As New clsBeTrans_pe_det
        Dim BeTransPickingDet As New clsBeTrans_picking_det
        Dim Factor As Integer
        Dim tmpBePickingUbic As New clsBeTrans_picking_ubic
        Dim CantidadStockDestino As Double = 0
        Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
        Dim vPickingParcial As Boolean = False
        Dim Verificacion_Auto As Boolean = False

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Verificacion_Auto = clsLnTrans_picking_enc.Get_Verificacion_Auto_By_IdPickingEnc(pBePickingUbic.IdPickingEnc, If(Es_Transaccion_Remota, pConnection, lConnection), If(Es_Transaccion_Remota, pTransaction, lTransaction))

            '#EJC20250419: Se que es redundante pero trato de obtener el registro dentro de la transacción para confirmar que no se haya modificado en la HH.
            pBePickingUbic = Get_Single_By_IdStockRes_And_IdPickingEnc(pBePickingUbic.IdStockRes,
                                                                       pBePickingUbic.IdPickingEnc,
                                                                       pBePickingUbic.IdBodega,
                                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

            tmpBePickingUbic = pBePickingUbic.Clone()

            If (pBePickingUbic.Cantidad_despachada > 0) Then
                pBePickingUbic.Cantidad_Recibida = pBePickingUbic.Cantidad_despachada
                pBePickingUbic.Peso_recibido = pBePickingUbic.Peso_despachado
                pBePickingUbic.Cantidad_Verificada = pBePickingUbic.Cantidad_despachada
                pBePickingUbic.Peso_verificado = pBePickingUbic.Peso_despachado
            Else
                vPickingParcial = (pBePickingUbic.Cantidad_Solicitada <> pBePickingUbic.Cantidad_Recibida)
                pBePickingUbic.Cantidad_Recibida = 0
                pBePickingUbic.Peso_recibido = 0
                pBePickingUbic.Cantidad_Verificada = 0
                pBePickingUbic.Peso_verificado = 0
                pBePickingUbic.Fecha_picking = New Date(1900, 1, 1)
                pBePickingUbic.Fecha_verificado = New Date(1900, 1, 1)
                pBePickingUbic.Fecha_packing = New Date(1900, 1, 1)
                pBePickingUbic.IdOperadorBodega_Pickeo = 0
            End If

            pBePickingUbic.User_mod = Usuario
            pBePickingUbic.Fec_mod = Now

            CantidadStockDestino = pBePickingUbic.Cantidad_Solicitada

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(pBePickingUbic.IdBodega,
                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

            Actualizar(pBePickingUbic,
                       If(Es_Transaccion_Remota, pConnection, lConnection),
                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeTransPeDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(pBePickingUbic.IdPedidoEnc,
                                                                                       pBePickingUbic.IdPedidoDet,
                                                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeTransPickingDet = clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(pBePickingUbic.IdPickingEnc,
                                                                                                   pBePickingUbic.IdPickingDet,
                                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If BeTransPeDet.IdPresentacion <> pBePickingUbic.IdPresentacion Then
                If BeTransPeDet.IdPresentacion = 0 AndAlso pBePickingUbic.IdPresentacion <> 0 Then
                    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pBePickingUbic.IdProductoBodega,
                                                                                       pBePickingUbic.IdPresentacion,
                                                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

                    If Factor <> 0 Then
                        Dim CantPedido As Double = tmpBePickingUbic.Cantidad_Recibida * Factor
                        BeTransPickingDet.Cantidad_recibida -= CantPedido
                    End If
                End If
            Else
                BeTransPickingDet.Cantidad_recibida -= tmpBePickingUbic.Cantidad_Recibida
            End If

            clsLnTrans_picking_det.Actualizar(BeTransPickingDet,
                                              If(Es_Transaccion_Remota, pConnection, lConnection),
                                              If(Es_Transaccion_Remota, pTransaction, lTransaction))

            BeTransPickingUbicStockExistente = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBePickingUbic.IdPickingEnc,
                                                                                                                     pBePickingUbic.IdPickingUbic,
                                                                                                                     pBePickingUbic.IdStock,
                                                                                                                     If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                                                     If(Es_Transaccion_Remota, pTransaction, lTransaction))
            If Not BeTransPickingUbicStockExistente Is Nothing Then

                Dim BeStockActualPickeado As New clsBeStock
                BeStockActualPickeado = clsLnStock.Get_Single_Stock_By_IdStock(pBePickingUbic.IdStock,
                                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))

                If Not BeStockActualPickeado Is Nothing AndAlso Not vPickingParcial Then

                    clsLnStock.Actualizar_IdUbicacion_By_IdStock(BeStockActualPickeado.IdUbicacion_anterior,
                                                                 BeStockActualPickeado.IdUbicacion,
                                                                 BeStockActualPickeado.IdBodega,
                                                                 BeStockActualPickeado.IdStock,
                                                                 If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                 If(Es_Transaccion_Remota, pTransaction, lTransaction))
                End If

                clsLnTrans_picking_ubic_stock.Eliminar(BeTransPickingUbicStockExistente,
                                                       If(Es_Transaccion_Remota, pConnection, lConnection),
                                                       If(Es_Transaccion_Remota, pTransaction, lTransaction))

                Dim BeMovimientoPicking As New clsBeTrans_movimientos
                BeMovimientoPicking = clsLnTrans_movimientos.Get_Single_By_IdMovimiento(BeTransPickingUbicStockExistente.IdMovimiento,
                                                                                         If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                         If(Es_Transaccion_Remota, pTransaction, lTransaction))

                clsLnTrans_movimientos.Eliminar_Movimiento_Picking_By_IdMovimiento(BeTransPickingUbicStockExistente.IdMovimiento,
                                                                                   If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                   If(Es_Transaccion_Remota, pTransaction, lTransaction))

                If Not BeMovimientoPicking Is Nothing Then

                    clsLnTrans_movimientos.Eliminar_Movimiento_Verificacion_By_PickingUbic(BeMovimientoPicking,
                                                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))

                End If

            End If

            clsLnTrans_packing_enc.Marcar_Linea_No_Empacada(pBePickingUbic,
                                                            Usuario,
                                                            If(Es_Transaccion_Remota, pConnection, lConnection),
                                                            If(Es_Transaccion_Remota, pTransaction, lTransaction))

            clsLnTrans_picking_enc.Actualizar_Estado_Pendiente(pBePickingUbic.IdPickingEnc,
                                                               If(Es_Transaccion_Remota, pConnection, lConnection),
                                                               If(Es_Transaccion_Remota, pTransaction, lTransaction))


            clsLnTrans_pe_enc.Actualizar_Estado_Pendiente(pBePickingUbic.IdPedidoEnc,
                                                          Usuario,
                                                          If(Es_Transaccion_Remota, pConnection, lConnection),
                                                          If(Es_Transaccion_Remota, pTransaction, lTransaction))

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota AndAlso lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Sub Marcar_Linea_No_Verificada(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                                 ByVal Usuario As Integer,
                                                 Optional ByVal pConnection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing
        Dim CantidadStockDestino As Double = 0
        Dim Verificacion_Auto As Boolean = False

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : ltransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If (pBePickingUbic.Cantidad_despachada > 0) Then
                pBePickingUbic.Cantidad_Verificada = pBePickingUbic.Cantidad_despachada
                pBePickingUbic.Peso_verificado = pBePickingUbic.Peso_despachado
            Else
                pBePickingUbic.Cantidad_Verificada = 0
                pBePickingUbic.Peso_verificado = 0
            End If

            pBePickingUbic.User_mod = Usuario
            pBePickingUbic.Fec_mod = Now
            pBePickingUbic.IdOperadorBodega_Verifico = 0 '//Requerido por Erik 17072025

            CantidadStockDestino = pBePickingUbic.Cantidad_Solicitada

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(pBePickingUbic.IdBodega,
                                                                                   IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                   IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

            '#GT17072025: se agrega otro metodo, actualizar_no_verificado, que maneje solo los campos necesarios y no el que toca toda la tabla.
            'Actualizar(pBePickingUbic,
            '           IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
            '           IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

            Actualizar_No_Verificado(pBePickingUbic,
                       IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                       IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

            clsLnTrans_packing_enc.Marcar_Linea_No_Empacada(pBePickingUbic,
                                                            Usuario,
                                                            IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                            IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

            clsLnTrans_pe_enc.Actualizar_Estado_Pendiente(pBePickingUbic.IdPedidoEnc,
                                                          Usuario,
                                                          IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                          IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

            Dim BeTransPickingUbicStockExistente As New clsBeTrans_picking_ubic_stock
            BeTransPickingUbicStockExistente = clsLnTrans_picking_ubic_stock.Get_Single_By_IdPickingUbic_And_IdStock(pBePickingUbic.IdPickingEnc,
                                                                                                                     pBePickingUbic.IdPickingUbic,
                                                                                                                     pBePickingUbic.IdStock,
                                                                                                                     IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                                                     IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))



            If Not BeTransPickingUbicStockExistente Is Nothing Then

                Dim BeMovimientoPicking As New clsBeTrans_movimientos
                BeMovimientoPicking = clsLnTrans_movimientos.Get_Single_By_IdMovimiento(BeTransPickingUbicStockExistente.IdMovimiento,
                                                                                        IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                        IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

                clsLnTrans_picking_ubic_stock.Eliminar(BeTransPickingUbicStockExistente,
                                                       IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                       IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

                clsLnTrans_movimientos.Eliminar_Movimiento_Picking_By_IdMovimiento(BeTransPickingUbicStockExistente.IdMovimiento,
                                                                                   IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                   IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

                Verificacion_Auto = clsLnTrans_picking_enc.Get_VerificacionAuto_By_IdPickingEnc(pBePickingUbic.IdPickingEnc,
                                                                                                IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                                IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))
                If Verificacion_Auto Then

                    If Not BeMovimientoPicking Is Nothing Then

                        clsLnTrans_movimientos.Eliminar_Movimiento_Verificacion_By_PickingUbic(BeMovimientoPicking,
                                                                                               IIf(Not Es_Transaccion_Remota, lConnection, pConnection),
                                                                                               IIf(Not Es_Transaccion_Remota, ltransaction, pTransaction))

                    End If

                End If

            End If

            If Not Es_Transaccion_Remota Then ltransaction.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not ltransaction Is Nothing Then ltransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Sub Marcar_Picking_No_Verificado(ByVal pIdPickingEnc As Integer,
                                                   ByVal pUsuario As Integer,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim vResult As String = ""

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltrans As SqlTransaction = Nothing
        Dim IdPedidoDet As Integer = 0

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : ltrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "UPDATE trans_picking_ubic
                                SET Cantidad_Verificada = 0,
                                    Peso_verificado = 0,
                                    User_mod = @Usuario,
                                    Fec_mod  = @Fecha
                                 WHERE IdPickingEnc = @IdPickingEnc"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                cmd = New SqlCommand(sp, lConnection, ltrans)
            End If

            cmd.Parameters.AddWithValue("@Usuario", pUsuario)
            cmd.Parameters.AddWithValue("@Fecha", Now)
            cmd.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then ltrans.Commit()

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If Not ltrans Is Nothing Then ltrans.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Sub

    Public Shared Function Guarda_Trans_picking_ubic(ByVal IdPickingEnc As Integer,
                                                     ByRef pListObjPickingUbic As List(Of clsBeTrans_picking_ubic),
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction,
                                                     Optional EsPickig As Boolean = False) As String

        Dim vResult As String = ""

        Try

            'Dim lMaxIdDet As Integer = MaxID(lConnection, lTransaction)
            Dim registrosAct As Integer = 0
            Dim registrosInsertados As Integer = 0
            Dim vListaIdPedidoEnc As New List(Of Integer)
            Dim CantidadStockDestino As Double = 0

            For Each BeTransPickingUbic As clsBeTrans_picking_ubic In pListObjPickingUbic

                BeTransPickingUbic.IdPickingEnc = IdPickingEnc

                If BeTransPickingUbic.IsNew Then

                    BeTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1

                    CantidadStockDestino = BeTransPickingUbic.Cantidad_Solicitada

                    Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BeTransPickingUbic.IdBodega, lConnection, lTransaction)
                    clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

                    Insertar(BeTransPickingUbic,
                             lConnection,
                             lTransaction)

                    vResult += String.Format("MSG_202303031902A: Entré a insertar el Picking Ubic {0} ", BeTransPickingUbic.IdPickingUbic)

                    registrosInsertados += 1

                Else
                    registrosAct = Actualizar(BeTransPickingUbic,
                                              lConnection,
                                              lTransaction)

                    vResult += String.Format("Registros actualizados {0} ", registrosAct)
                    vResult += String.Format("Entré a actualizar el Picking Ubic {0} ", BeTransPickingUbic.IdPickingUbic)
                End If

                vResult += clsLnStock_res.Set_IdPicking_By_IdPedidoDet(IdPickingEnc,
                                                                       BeTransPickingUbic.IdPedidoDet,
                                                                       lConnection,
                                                                       lTransaction)

                If Not vListaIdPedidoEnc.Contains(BeTransPickingUbic.IdPedidoEnc) Then
                    vListaIdPedidoEnc.Add(BeTransPickingUbic.IdPedidoEnc)
                End If

            Next

            For Each Pedido In vListaIdPedidoEnc

                If Not clsLnStock_res.Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado(Pedido,
                                                                                         lConnection,
                                                                                         lTransaction) Then
                    Throw New Exception("ERROR_20220701_0658B: La asociación de picking para stock reservado falló y esto generaría un picking incompleto en base a la reserva para el pedido " & Pedido)
                End If

            Next

            Console.WriteLine("Registros insertados: " & registrosInsertados)

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            'clsLnLog_error_wms.Agregar_Error(String.Format("{0} {1}  Result {2} ", MethodBase.GetCurrentMethod.Name(), ex.Message, vResult))
            Dim vMsgError As String = String.Format("{0} {1}  Result {2} ", MethodBase.GetCurrentMethod.Name(), ex.Message, vResult)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pIdPickingEnc:=IdPickingEnc, pStackTrace:=ex.StackTrace)
            Throw New Exception(String.Format("{0} {1}  Result {2} ", MethodBase.GetCurrentMethod.Name(), ex.Message, vResult))
        End Try

        Return vResult

    End Function

    Public Shared Function Guarda_Trans_picking_ubic(ByVal IdPickingEnc As Integer,
                                                     ByRef pListBeTransPickingUbic As List(Of clsBeTrans_picking_ubic),
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As String

        Dim vResult As String = ""

        Try

            Dim registrosAct As Integer = 0
            Dim registrosInsertados As Integer = 0
            Dim vListaIdPedidoEnc As New List(Of Integer)
            Dim CantidadStockDestino As Double = 0

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(pListBeTransPickingUbic.FirstOrDefault.IdBodega, lConnection, lTransaction)

            Dim inconsistencias = pListBeTransPickingUbic.
            Where(Function(x) Not vPermitirDecimales AndAlso
                              x.Cantidad_Solicitada <> Math.Floor(x.Cantidad_Solicitada)).
            ToList()

            If inconsistencias.Any() Then
                For Each item In inconsistencias
                    Throw New Exception($"La bodega {item.IdBodega} no permite decimales, pero se encontró Cantidad_Solicitada = {item.Cantidad_Solicitada} en IdPedidoDet: {item.IdPedidoDet}")
                Next
            End If

            For Each BeTransPickingUbic As clsBeTrans_picking_ubic In pListBeTransPickingUbic

                BeTransPickingUbic.IdPickingEnc = IdPickingEnc

                If BeTransPickingUbic.IsNew Then

                    BeTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1

                    CantidadStockDestino = BeTransPickingUbic.Cantidad_Solicitada

                    '#EJC202309291619:Si existe concurrencia, volver a calcular el IdPIckingUbic.
                    If Existe_IdPickingUbic(BeTransPickingUbic.IdPickingUbic, lConnection, lTransaction) Then
                        BeTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                    End If

                    Try

                        Insertar(BeTransPickingUbic,
                                 lConnection,
                                 lTransaction)

                    Catch ex As Exception

                        If ex.Message.Contains("PRIMARY KEY constraint") Then
                            '#EJC202309291619:Si existe concurrencia, volver a calcular el IdPIckingUbic.
                            If Existe_IdPickingUbic(BeTransPickingUbic.IdPickingUbic, lConnection, lTransaction) Then

                                BeTransPickingUbic.IdPickingUbic = MaxID(lConnection, lTransaction) + 1

                                Insertar(BeTransPickingUbic,
                                         lConnection,
                                         lTransaction)
                            Else
                                Throw New Exception("Error_202303291717: En este universo, al parecer la causa no precede al efecto.")
                            End If

                        Else
                            Debug.Print(BeTransPickingUbic.IdPickingUbic)
                            Throw ex
                        End If

                    End Try

                    registrosInsertados += 1

                Else

                    registrosAct = Actualizar(BeTransPickingUbic, lConnection, lTransaction)

                End If

                vResult += clsLnStock_res.Set_IdPicking_By_IdPedidoDet(IdPickingEnc,
                                                                       BeTransPickingUbic.IdPedidoDet,
                                                                       lConnection,
                                                                       lTransaction)


                If Not vListaIdPedidoEnc.Contains(BeTransPickingUbic.IdPedidoEnc) Then
                    vListaIdPedidoEnc.Add(BeTransPickingUbic.IdPedidoEnc)
                End If

            Next

            For Each Pedido In vListaIdPedidoEnc

                If Not clsLnStock_res.Stock_Reservado_By_IdPedido_Tiene_Picking_Asociado(Pedido,
                                                                                         lConnection,
                                                                                         lTransaction) Then

                    Throw New Exception("ERROR_20220701_0658B: La asociación de picking para stock reservado falló y esto generaría un picking incompleto en base a la reserva.")

                End If

            Next

            Console.WriteLine("Registros insertados: " & registrosInsertados)

        Catch ex As Exception
            Throw ex
        End Try

        Return vResult

    End Function

    Public Shared Function Insertar_PickingUbic(ByRef pStockRes As clsBeStock_res,
                                                ByVal pIdPickingDet As Integer,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As Boolean

        Insertar_PickingUbic = False

        Dim CantidadStockDestino As Double = 0

        Try

            Dim BePickingUbic As New clsBeTrans_picking_ubic()

            With BePickingUbic
                .IdPickingUbic = MaxID(lConnection, lTransaction) + 1
                .IdPedidoDet = pStockRes.IdPedidoDet
                .IdStockRes = pStockRes.IdStockRes
                .IdPickingDet = pIdPickingDet
                .IdPickingEnc = pStockRes.IdPicking
                .IdPedidoEnc = pStockRes.IdPedido
                .IdStock = pStockRes.IdStock
                .IdPropietarioBodega = pStockRes.IdPropietarioBodega
                .IdProductoBodega = pStockRes.IdProductoBodega
                .IdProductoEstado = pStockRes.IdProductoEstado
                .IdPresentacion = pStockRes.IdPresentacion
                .IdUnidadMedida = pStockRes.IdUnidadMedida
                .IdUbicacionAnterior = Val(pStockRes.Ubicacion_ant)
                .IdRecepcion = pStockRes.IdRecepcion
                .IdUbicacion = pStockRes.IdUbicacion
                .Lote = pStockRes.Lote
                .Fecha_Vence = pStockRes.Fecha_vence
                .Serial = pStockRes.Serial
                .Lic_plate = pStockRes.Lic_plate
                .Peso_solicitado = pStockRes.Peso
                .Cantidad_Solicitada = pStockRes.Cantidad
                .Cantidad_Recibida = 0.0
                .Fecha_real_vence = pStockRes.Fecha_vence
                .User_agr = pStockRes.User_agr
                .Fec_agr = Now
                .User_mod = pStockRes.User_mod
                .Fec_mod = Now
                .Activo = True
                .IsNew = True
                .IdBodega = clsLnProducto_bodega.Get_IdBodega_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction)
                .IdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction)
            End With

            CantidadStockDestino = BePickingUbic.Cantidad_Solicitada

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BePickingUbic.IdBodega, lConnection, lTransaction)
            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

            Insertar(BePickingUbic, lConnection, lTransaction)

            '#EJC20220627:Actualizar el IdPicking en la reserva.
            pStockRes.IdPicking = pStockRes.IdPicking
            pStockRes.User_agr = pStockRes.User_agr
            clsLnStock_res.Actualizar_IdPickingEnc(pStockRes, lConnection, lTransaction)

            Insertar_PickingUbic = True

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pIdPickingDet:=pIdPickingDet, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    '#CKFK20180128 01:08AM: Cree la funcion ReemplazoProductoPicking para poder hacer todos los cambios por reemplazo de producto en el Picking
    '#AT 20220110 Le agregué la conexión y la transacción
    Public Shared Function Reemplazo_Producto_En_Picking(ByVal IdStockCambioEstado As Integer,
                                                         ByVal IdPickingEnc As Integer,
                                                         ByVal IdPickingDet As Integer,
                                                         ByVal CantSol As Double,
                                                         ByVal MaquinaQueSolicita As String,
                                                         ByVal UsuarioHH As Integer,
                                                         ByVal lBeStockAReservar As List(Of clsBeStock_res),
                                                         ByVal IdBodega As Integer,
                                                         ByVal IdEmpresa As Integer,
                                                         ByVal IdUbicDestino As Integer,
                                                         ByVal IdEstDestino As Integer,
                                                         ByVal IdStockResCambioEstado As Integer,
                                                         ByVal EsPicking As Boolean,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction,
                                                         Optional ByVal Tipo As Integer = 0) As Boolean

        Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)
        Dim Factor As Double = 0
        Dim CantidadPresentacion As Double = 0
        Dim resultado As String = ""

        Try

            If lBeStockAReservar IsNot Nothing AndAlso lBeStockAReservar.Count > 0 Then

                For Each BeStockRes As clsBeStock_res In lBeStockAReservar

                    If BeStockRes.IdPresentacion <> 0 Then
                        Factor = 0
                        CantidadPresentacion = 0
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(BeStockRes.IdProductoBodega,
                                                                                           BeStockRes.IdPresentacion,
                                                                                           lConnection,
                                                                                           lTransaction)
                        CantidadPresentacion = Math.Round(BeStockRes.Cantidad / Factor, 6)
                    End If

                    'Llena PickingUbic
                    Dim BeTransPickingUbic As New clsBeTrans_picking_ubic() With {
                        .IdPedidoDet = BeStockRes.IdPedidoDet,
                        .IdPedidoEnc = BeStockRes.IdPedido,
                        .IdStockRes = BeStockRes.IdStockRes,
                        .IdPickingDet = IdPickingDet,
                        .IdPickingEnc = IdPickingEnc,
                        .IdStock = BeStockRes.IdStock,
                        .IdPropietarioBodega = BeStockRes.IdPropietarioBodega,
                        .IdProductoBodega = BeStockRes.IdProductoBodega,
                        .IdProductoEstado = BeStockRes.IdProductoEstado,
                        .IdPresentacion = BeStockRes.IdPresentacion,
                        .IdUnidadMedida = BeStockRes.IdUnidadMedida,
                        .IdUbicacionAnterior = Val(BeStockRes.Ubicacion_ant),
                        .IdRecepcion = BeStockRes.IdRecepcion,
                        .IdUbicacion = BeStockRes.IdUbicacion,
                        .Lote = BeStockRes.Lote,
                        .Fecha_Vence = BeStockRes.Fecha_vence,
                        .Serial = BeStockRes.Serial,
                        .Lic_plate = BeStockRes.Lic_plate,
                        .Peso_solicitado = BeStockRes.Peso,
                        .Cantidad_Solicitada = IIf(BeStockRes.IdPresentacion <> 0, CantidadPresentacion, BeStockRes.Cantidad),
                        .Cantidad_Recibida = 0,
                        .Peso_recibido = 0,
                        .Cantidad_Verificada = 0,
                        .Peso_verificado = 0,
                        .Cantidad_despachada = 0,
                        .Peso_despachado = 0,
                        .Fecha_real_vence = BeStockRes.Fecha_vence,
                        .Encontrado = False,
                        .Acepto = False,
                        .Dañado_picking = False,
                        .Dañado_verificacion = False,
                        .User_agr = UsuarioHH,
                        .Fec_agr = Now,
                        .User_mod = UsuarioHH,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True,
                        .No_packing = 0,
                        .IdBodega = IdBodega}

                    pListBePickingUbic.Add(BeTransPickingUbic)

                    BeStockRes.Estado = "UNCOMMITED"
                    clsLnStock_res.Actualizar(BeStockRes, lConnection, lTransaction)

                Next

            End If

            ' Picking Detalle Ubicacion
            Guarda_Trans_picking_ubic(IdPickingEnc,
                                      pListBePickingUbic,
                                      lConnection,
                                      lTransaction,
                                      EsPicking)

            '#CKFK 20180202 Genera la tarea de cambio de estado
            '#AT En base a lo analizado creemos que aqui vamos a colocar el metodo Marcar_No_Encontrado cuando el tipo sea 2
            If Tipo = 1 Then
                clsLnTrans_ubic_hh_enc.Generar_Tarea_Cambio_Estado(IdBodega,
                                                                   IdEmpresa,
                                                                   IdStockCambioEstado,
                                                                   IdStockResCambioEstado,
                                                                   UsuarioHH,
                                                                   CantSol,
                                                                   IdUbicDestino,
                                                                   IdEstDestino,
                                                                   lBeStockAReservar(0).IdPropietarioBodega,
                                                                   pListBePickingUbic(0).IdPickingUbic,
                                                                   lConnection,
                                                                   lTransaction,
                                                                   MaquinaQueSolicita,
                                                                   EsPicking)
            Else
                Dim BePresentacion As New clsBeProducto_Presentacion

                If lBeStockAReservar(0).IdPresentacion <> 0 Then

                    BePresentacion.IdPresentacion = lBeStockAReservar(0).IdPresentacion

                    clsLnProducto_presentacion.GetSingle(BePresentacion, lConnection, lTransaction)

                    If BePresentacion.EsPallet Then

                        Dim vFactorPallet As Double = (BePresentacion.Factor * BePresentacion.CajasPorCama * BePresentacion.CamasPorTarima)

                        If vFactorPallet > 0 Then
                            CantSol = Math.Round(CantSol * vFactorPallet, 7)
                        Else
                            Throw New Exception("No se pudo reservar el stock para el tipo de producto pallet porque los factores de conversión dan un denominador = 0")
                        End If

                    Else
                        CantSol = Math.Round(CantSol * BePresentacion.Factor, 7)
                    End If

                End If

                resultado = Marcar_No_Encontrado(IdBodega,
                                                 IdEmpresa,
                                                 IdStockCambioEstado,
                                                 IdStockResCambioEstado,
                                                 UsuarioHH,
                                                 CantSol,
                                                 lBeStockAReservar(0).IdPropietarioBodega,
                                                 pListBePickingUbic(0).IdPickingUbic,
                                                 lConnection,
                                                 lTransaction,
                                                 lBeStockAReservar(0).IdProductoBodega,
                                                 MaquinaQueSolicita)
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Reemplazo_Producto_En_Picking(ByVal IdStockCambioEstado As Integer,
                                                         ByVal IdPickingEnc As Integer,
                                                         ByVal IdPickingDet As Integer,
                                                         ByVal CantSol As Double,
                                                         ByVal MaquinaQueSolicita As String,
                                                         ByVal UsuarioHH As Integer,
                                                         ByVal lBeStockAReservar As List(Of clsBeStock_res),
                                                         ByVal IdBodega As Integer,
                                                         ByVal IdEmpresa As Integer,
                                                         ByVal IdUbicDestino As Integer,
                                                         ByVal IdEstDestino As Integer,
                                                         ByVal IdStockResCambioEstado As Integer,
                                                         ByVal EsPicking As Boolean,
                                                         ByVal MarcarComoNE As Boolean,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Boolean

        Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)
        Dim Factor As Double = 0
        Dim CantidadPresentacion As Double = 0

        Try

            If lBeStockAReservar IsNot Nothing AndAlso lBeStockAReservar.Count > 0 Then

                For Each bo As clsBeStock_res In lBeStockAReservar

                    If bo.IdPresentacion <> 0 Then
                        Factor = 0
                        CantidadPresentacion = 0
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(bo.IdProductoBodega, bo.IdPresentacion, lConnection, lTransaction)
                        CantidadPresentacion = Math.Round(bo.Cantidad / Factor, 6)
                    End If

                    'Llena PickingUbic
                    Dim ObjU As New clsBeTrans_picking_ubic() With {
                        .IdPedidoDet = bo.IdPedidoDet,
                        .IdStockRes = bo.IdStockRes,
                        .IdPickingDet = IdPickingDet,
                        .IdPickingEnc = IdPickingEnc,
                        .IdStock = bo.IdStock,
                        .IdPropietarioBodega = bo.IdPropietarioBodega,
                        .IdProductoBodega = bo.IdProductoBodega,
                        .IdProductoEstado = bo.IdProductoEstado,
                        .IdPresentacion = bo.IdPresentacion,
                        .IdUnidadMedida = bo.IdUnidadMedida,
                        .IdUbicacionAnterior = Val(bo.Ubicacion_ant),
                        .IdRecepcion = bo.IdRecepcion,
                        .IdUbicacion = bo.IdUbicacion,
                        .Lote = bo.Lote,
                        .Fecha_Vence = bo.Fecha_vence,
                        .Serial = bo.Serial,
                        .Lic_plate = bo.Lic_plate,
                        .Peso_solicitado = bo.Peso,
                        .Cantidad_Solicitada = IIf(bo.IdPresentacion <> 0, CantidadPresentacion, bo.Cantidad),
                        .Cantidad_Recibida = 0,
                        .Peso_recibido = 0,
                        .Cantidad_Verificada = 0,
                        .Peso_verificado = 0,
                        .Cantidad_despachada = 0,
                        .Peso_despachado = 0,
                        .Fecha_real_vence = bo.Fecha_vence,
                        .Encontrado = False,
                        .Acepto = False,
                        .Dañado_picking = False,
                        .Dañado_verificacion = False,
                        .User_agr = UsuarioHH,
                        .Fec_agr = Now,
                        .User_mod = UsuarioHH,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True,
                        .IdBodega = IdBodega}

                    pListBePickingUbic.Add(ObjU)

                    'bo.Estado = "UNCOMMITED"
                    'clsLnStock_res.Actualizar(bo, lConnection, lTransaction)

                Next

            End If

            ' Picking Detalle Ubicacion
            Guarda_Trans_picking_ubic(IdPickingEnc,
                                      pListBePickingUbic,
                                      lConnection,
                                      lTransaction)

            '#CKFK 20180202 Genera la tarea de cambio de estado
            clsLnTrans_ubic_hh_enc.Generar_Tarea_Cambio_Estado(IdBodega,
                                                               IdEmpresa,
                                                               IdStockCambioEstado,
                                                               IdStockResCambioEstado,
                                                               UsuarioHH,
                                                               CantSol,
                                                               IdUbicDestino,
                                                               IdEstDestino,
                                                               lBeStockAReservar(0).IdPropietarioBodega,
                                                               pListBePickingUbic(0).IdPickingUbic,
                                                               MarcarComoNE,
                                                               lConnection,
                                                               lTransaction,
                                                               MaquinaQueSolicita)

            Return True

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPickingDet:=IdPickingDet,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    '#CKFK20180219 03:30PM: Cree la funcion ReemplazoProductoPickingTest para hacer pruebas en el WS de la función ReemplazoProductoPicking
    'Public Shared Function ReemplazoProductoPickingTest(ByVal IdStockCambioEstado As Integer,
    '                                                    ByVal IdPickingEnc As Integer,
    '                                                    ByVal IdPickingDet As Integer,
    '                                                    ByVal CantSol As Double,
    '                                                    ByVal MaquinaQueSolicita As String,
    '                                                    ByVal UsuarioHH As Integer,
    '                                                    ByVal IdBodega As Integer,
    '                                                    ByVal IdEmpresa As Integer,
    '                                                    ByVal IdUbicDestino As Integer,
    '                                                    ByVal IdEstDestino As Integer,
    '                                                    ByVal IdStockResCambioEstado As Integer,
    '                                                    ByVal IdPedidoDet As Integer,
    '                                                    ByVal IdPropietarioBodega As Integer,
    '                                                    ByVal IdPedidoEnc As Integer) As Boolean

    '    Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        Dim lBeStockAReservar As New List(Of clsBeStock_res)

    '        lBeStockAReservar = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(IdPedidoDet,
    '                                                                            IdPropietarioBodega,
    '                                                                            IdPickingEnc,
    '                                                                            IdPedidoEnc,
    '                                                                            lConnection,
    '                                                                            lTransaction)

    '        If lBeStockAReservar IsNot Nothing AndAlso lBeStockAReservar.Count > 0 Then

    '            For Each bo As clsBeStock_res In lBeStockAReservar

    '                'Llena PickingUbic
    '                Dim ObjU As New clsBeTrans_picking_ubic() With {
    '                    .IdPedidoDet = bo.IdPedidoDet,
    '                    .IdStockRes = bo.IdStockRes,
    '                    .IdPickingDet = IdPickingDet,
    '                    .IdPickingEnc = IdPickingEnc,
    '                    .IdStock = bo.IdStock,
    '                    .IdPropietarioBodega = bo.IdPropietarioBodega,
    '                    .IdProductoBodega = bo.IdProductoBodega,
    '                    .IdProductoEstado = bo.IdProductoEstado,
    '                    .IdPresentacion = bo.IdPresentacion,
    '                    .IdUnidadMedida = bo.IdUnidadMedida,
    '                    .IdUbicacionAnterior = Val(bo.Ubicacion_ant),
    '                    .IdRecepcion = bo.IdRecepcion,
    '                    .IdUbicacion = bo.IdUbicacion,
    '                    .Lote = bo.Lote,
    '                    .Fecha_Vence = bo.Fecha_vence,
    '                    .Serial = bo.Serial,
    '                    .Lic_plate = bo.Lic_plate,
    '                    .Peso_solicitado = bo.Peso,
    '                    .Cantidad_Solicitada = bo.Cantidad,
    '                    .Cantidad_Recibida = 0.0,
    '                    .Peso_recibido = 0,
    '                    .Cantidad_Verificada = 0,
    '                    .Peso_verificado = 0,
    '                    .Cantidad_despachada = 0,
    '                    .Peso_despachado = 0,
    '                    .Fecha_real_vence = bo.Fecha_vence,
    '                    .Encontrado = False,
    '                    .Dañado_picking = False,
    '                    .Dañado_verificacion = False,
    '                    .User_agr = UsuarioHH,
    '                    .Fec_agr = Now,
    '                    .User_mod = UsuarioHH,
    '                    .Fec_mod = Now,
    '                    .Activo = True,
    '                    .IsNew = True}

    '                pListBePickingUbic.Add(ObjU)

    '            Next

    '        End If

    '        ' Picking Detalle Ubicacion
    '        Guarda_Trans_picking_ubic(IdPickingEnc, pListBePickingUbic, lConnection, lTransaction)

    '        '#CKFK 20180202 Debo terminarlo
    '        clsLnTrans_ubic_hh_enc.Generar_Tarea_Cambio_Estado(IdBodega,
    '                                                           IdEmpresa,
    '                                                           IdStockCambioEstado,
    '                                                           IdStockResCambioEstado,
    '                                                           UsuarioHH,
    '                                                           CantSol,
    '                                                           IdUbicDestino,
    '                                                           IdEstDestino,
    '                                                           IdPropietarioBodega,
    '                                                           pListBePickingUbic(0).IdPickingUbic,
    '                                                           lConnection,
    '                                                           lTransaction,
    '                                                           MaquinaQueSolicita)

    '        lTransaction.Commit()

    '        Return True

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '    End Try
    'End Function

    '#CKFK 20180415 10:58 PM Procedimiento creado para marcar producto como no encontrado
    '#CKFK 20201124 09:30 AM Modifique el Sub a Function para saber que hace
    Public Shared Function Marcar_No_Encontrado(ByVal IdBodega As Integer,
                                                ByVal IdEmpresa As Integer,
                                                ByVal IdStock As Integer,
                                                ByVal IdStockRes As Integer,
                                                ByVal UsuarioHH As Integer,
                                                ByVal CantNoEncontrada As Double,
                                                ByVal IdPropietarioBodega As Integer,
                                                ByVal IdPickingUbic As Integer,
                                                Optional ByRef pConnection As SqlConnection = Nothing,
                                                Optional ByRef pTransaction As SqlTransaction = Nothing,
                                                Optional ByVal IdProductoBodega As Integer = 0,
                                                Optional ByVal pHostSolicita As String = Nothing) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Dim vResult As String = ""

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim MovResult As Boolean = False
            'Stock no encontrado, 
            Dim pBeStock As New clsBeStock 'pStock es el objeto donde voy a guardar el stock no encontrado para luego reservarlo

            'El IdStock del producto no encontrado
            'El IdStockRes del producto no encontrado 
            pBeStock = clsLnStock.GetSingle(IdStock,
                                            IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                            IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            '#AT 20220117 Se obtiene el IdProductoEstadoNE con IdBodega
            Dim beBodega As New clsBeBodega() With {.IdBodega = IdBodega}
            clsLnBodega.GetSingle(beBodega,
                                  IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                  IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            If beBodega.IdProductoEstadoNE > 0 AndAlso beBodega.ubic_producto_ne > 0 Then

                '#AT Se crea el movimiento para el producto no encontrado
                MovResult = Movimiento_Prod_No_Encontrado(pBeStock,
                                                          beBodega,
                                                          IdBodega,
                                                          IdPropietarioBodega,
                                                          IdEmpresa,
                                                          IdStockRes,
                                                          IdPickingUbic,
                                                          UsuarioHH,
                                                          CantNoEncontrada,
                                                          pHostSolicita,
                                                          pConnection,
                                                          pTransaction)

                If MovResult Then

                    If Not Es_Transaccion_Remota Then lTransaction.Commit()

                End If

            Else
                Throw New Exception("No existe el estado No Encontrado")
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} Result {2}", MethodBase.GetCurrentMethod.Name(), ex.Message, vResult))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

        Return vResult

    End Function

    '#CKFK 20180415 10:58 PM Procedimiento creado para marcar producto como no encontrado
    '#CKFK 20201124 09:30 AM Modifique el Sub a Function para saber que hace
    Public Shared Function Marcar_No_Encontrado_Original(ByVal IdBodega As Integer,
                                              ByVal IdEmpresa As Integer,
                                              ByVal IdStock As Integer,
                                              ByVal IdStockRes As Integer,
                                              ByVal UsuarioHH As Integer,
                                              ByVal CantNoEncontrada As Double,
                                              ByVal IdPropietarioBodega As Integer,
                                              ByVal IdPickingUbic As Integer,
                                              Optional ByRef pConnection As SqlConnection = Nothing,
                                              Optional ByRef pTransaction As SqlTransaction = Nothing,
                                              Optional ByVal IdProductoBodega As Integer = 0) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Dim vResult As String = ""

        Try

            If Not Es_Transaccion_Remota Then lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim pBePropietarioBodega As New clsBePropietario_bodega
            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            'Stock no encontrado, 
            Dim pStock As New clsBeStock 'pStock es el objeto donde voy a guardar el stock no encontrado para luego reservarlo
            Dim pListStock As New List(Of clsBeStock) 'Lista de los stock no encontrados

            '#AT 20220117 Obtener IdProductoEstadoNE con IdBodega
            Dim beBodega As New clsBeBodega() With {.IdBodega = IdBodega}
            clsLnBodega.GetSingle(beBodega)

            'El IdStock del producto no encontrado
            'El IdStockRes del producto no encontrado 
            pStock = clsLnStock.GetSingle(IdStock, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            'Modificar campos en el stock
            pStock.Cantidad = CantNoEncontrada

            If beBodega IsNot Nothing Then
                pStock.IdProductoEstado = beBodega.IdProductoEstadoNE
                pStock.ProductoEstado.IdEstado = beBodega.IdProductoEstadoNE
                pStock.IdUbicacion = beBodega.ubic_producto_ne
            Else
                pStock.IdProductoEstado = pStock.ProductoEstado.IdEstado ' 5 '#CKFK 20180422 Aquí debo colocar el estado del producto no encontrado
                pStock.ProductoEstado.IdEstado = pStock.ProductoEstado.IdEstado '5 '#CKFK 20180422 Aquí debo colocar el estado del producto no encontrado
                pStock.IdUbicacion = pStock.IdUbicacion '1053 '#CKFK 20180422 Aquí debo colocar la ubicación del producto no encontrado
            End If

            pListStock.Add(pStock)

            '#AT 20220117 Aquí se  reserva el stock no encontrado en una transacción de cambio de ubicación
            vResult += clsLnStock_res.Guardar_Stock_Res_NE(0, pListStock, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            Dim beStockRes As New clsBeStock_res() With {.IdStockRes = IdStockRes, .IdProductoBodega = IdProductoBodega}
            clsLnStock_res.GetSingle(beStockRes, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

            vResult += String.Format("La cantidad no encontrada es {0} ", CantNoEncontrada)

            If beStockRes.Cantidad = CantNoEncontrada Then

                clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

                vResult += String.Format("Eliminó el Stock_res {0} ", IdStockRes)

            ElseIf beStockRes.Cantidad > CantNoEncontrada Then
                clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes, CantNoEncontrada, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction))

                vResult += String.Format("beStockRes.Cantidad > CantNoEncontrada el Stock_res {0} ", IdStockRes)

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} Result {2}", MethodBase.GetCurrentMethod.Name(), ex.Message, vResult))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

        Return vResult

    End Function

    '#AT 20220118 Nueva función para guardar la transacción para producto no encontrado
    Public Shared Function Movimiento_Prod_No_Encontrado(ByVal pBeStock As clsBeStock,
                                                         ByVal beBodega As clsBeBodega,
                                                         ByVal IdBodega As Integer,
                                                         ByVal IdPropietarioBodega As Integer,
                                                         ByVal IdEmpresa As Integer,
                                                         ByVal IdStockRes As Integer,
                                                         ByVal IdPickingUbic As Integer,
                                                         ByVal UsuarioHH As Integer,
                                                         ByVal CantNoEncontrada As Double,
                                                         ByVal pHostSolicita As String,
                                                         Optional ByRef pConnection As SqlConnection = Nothing,
                                                         Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean
        Try

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega,
                                                                        pConnection,
                                                                        pTransaction)

            Dim pBePropietarioBodega As New clsBePropietario_bodega
            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega,
                                                                                       pConnection,
                                                                                       pTransaction)

            Dim pListStock As New List(Of clsBeStock) 'Lista de los stock no encontrados

            Dim pStockActual As New clsBeStock
            pStockActual = clsLnStock.GetSingle(pBeStock.IdStock,
                                                pConnection,
                                                pTransaction)

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc
            Dim beTareaHH As New clsBeTarea_hh()

            Dim beStockRes As New clsBeStock_res

            '#AT20230104 Analizando los datos, esta consulta devuelve la nueva linea insertada en trans_picking_ubic
            'disponible para ser pickeada; aca pude obtener la licencia para la bitacora de movimientos.
            Dim bePickingUbic As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            GetSingle(bePickingUbic,
                      pConnection,
                      pTransaction)

            beStockRes.IdStockRes = IdStockRes
            beStockRes.IdProductoBodega = bePickingUbic.IdProductoBodega
            clsLnStock_res.GetSingle(beStockRes,
                                     pConnection,
                                     pTransaction)

            If beStockRes.Cantidad = CantNoEncontrada Then

                'CM_21012019: Se elimina el IdStock_res porque ya se insertó uno nuevo con los valores correctos. 
                clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes,
                                                                      pConnection,
                                                                      pTransaction)

                '#CKFK 20180219 Cambié la condición porque es cuando la cantidad dañada es menor que se modifica la cantidad en el Stock
            ElseIf beStockRes.Cantidad > CantNoEncontrada Then

                clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes,
                                                                        CantNoEncontrada,
                                                                        pConnection,
                                                                        pTransaction)

            End If


            If Not UbicaAuto Then

                'Encabezado de ubicación con HH por cambio de estado
                Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega,
                                                                                                     pConnection,
                                                                                                     pTransaction)

                If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion por defecto para ubicación automática, no puede realizarse el reemplazo")

                beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                beUbicHHEnc.FechaInicio = Now
                beUbicHHEnc.HoraInicio = Now
                beUbicHHEnc.FechaFin = Now
                beUbicHHEnc.HoraFin = Now
                beUbicHHEnc.Activo = True
                beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                beUbicHHEnc.User_agr = UsuarioHH
                beUbicHHEnc.Fec_agr = Now
                beUbicHHEnc.User_mod = UsuarioHH
                beUbicHHEnc.Fec_mod = Now
                beUbicHHEnc.Operador_por_linea = UsuarioHH
                beUbicHHEnc.Ubicacion_con_hh = True
                beUbicHHEnc.Cambio_estado = True
                beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                beUbicHHEnc.IdTipoTarea = 3
                beUbicHHEnc.IdBodega = IdBodega
                beUbicHHEnc.Estado = "Nuevo"
                beUbicHHEnc.IsNew = True

                'Tarea de cambio de estado
                beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(pConnection, pTransaction) + 1
                beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                beTareaHH.IdBodega = IdBodega
                beTareaHH.IdEstado = 1
                beTareaHH.Tipo = 0
                beTareaHH.FechaInicio = Now
                beTareaHH.FechaFin = Now
                beTareaHH.DiaCompleto = False
                beTareaHH.Ubicacion = ""
                beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                beTareaHH.Recordatorio = ""
                beTareaHH.Asunto = ""
                beTareaHH.IdPrioridad = 0
                beTareaHH.IdTipoTarea = 3
                beTareaHH.IdMuelle = Nothing

            End If

            'Detalle de ubicación con HH por cambio de estado
            Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
            Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

            If Not UbicaAuto Then

                clsPublic.CopyObject(pBeStock, beUbicHHDet.Stock)

                beUbicHHDet.IdStock = pBeStock.IdStock
                beUbicHHDet.Producto = New clsBeProducto
                beUbicHHDet.Stock = New clsBeStock
                beUbicHHDet.IdTareaUbicacionDet = 0
                beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                beUbicHHDet.Producto.Nombre = pBeStock.Producto.Nombre
                beUbicHHDet.Producto.Codigo = pBeStock.Producto.Codigo
                beUbicHHDet.Stock.IdUbicacion_anterior = pBeStock.IdUbicacion
                beUbicHHDet.IdUbicacionOrigen = pBeStock.IdUbicacion 'IdUbicDest pHostSolicita
                beUbicHHDet.IdUbicacionDestino = beBodega.ubic_producto_ne 'IdUbicDest Preguntar
                beUbicHHDet.Stock.Fecha_vence = pBeStock.Fecha_vence
                beUbicHHDet.Stock.Serial = pBeStock.Serial
                beUbicHHDet.IdEstadoOrigen = pBeStock.IdProductoEstado
                beUbicHHDet.IdEstadoDestino = beBodega.IdProductoEstadoNE 'IdEstadoDest
                beUbicHHDet.Cantidad = CantNoEncontrada
                beUbicHHDet.Recibido = 0
                beUbicHHDet.Estado = "Pendiente"
                beUbicHHDet.Operador = New clsBeOperador
                beUbicHHDet.IdOperadorBodega = UsuarioHH
                beUbicHHDet.Activo = True
                beListUbicHHDet.Add(beUbicHHDet)

            End If


            'Inicio de movimiento de cambio de ubicacion
            Dim pListMov As New List(Of clsBeTrans_movimientos)
            Dim pBeMovimiento As New clsBeTrans_movimientos()

            pBeMovimiento.IdEmpresa = IdEmpresa
            pBeMovimiento.IdBodegaOrigen = IdBodega
            pBeMovimiento.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
            pBeMovimiento.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
            pBeMovimiento.IdProductoBodega = pBeStock.IdProductoBodega
            pBeMovimiento.IdUbicacionOrigen = pBeStock.IdUbicacion
            pBeMovimiento.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, beBodega.ubic_producto_ne)
            pBeMovimiento.IdPresentacion = pBeStock.IdPresentacion
            pBeMovimiento.IdEstadoOrigen = pBeStock.IdProductoEstado
            pBeMovimiento.IdEstadoDestino = beBodega.IdProductoEstadoNE 'IdEstadoDest Preguntar
            pBeMovimiento.IdUnidadMedida = pBeStock.IdUnidadMedida
            pBeMovimiento.IdTipoTarea = 27 'REEMP_NE_PICK
            pBeMovimiento.IdBodegaDestino = IdBodega
            pBeMovimiento.IdRecepcion = pBeStock.IdRecepcionEnc
            pBeMovimiento.IdRecepcionDet = pBeStock.IdRecepcionDet
            pBeMovimiento.Cantidad = IIf(Not UbicaAuto, beUbicHHDet.Cantidad, IIf(pBeStock.IdPresentacion <> 0, CantNoEncontrada, pBeStock.Cantidad))
            pBeMovimiento.Serie = pBeStock.Serial
            pBeMovimiento.Peso = pBeStock.Peso
            pBeMovimiento.Lote = pBeStock.Lote
            pBeMovimiento.Fecha_vence = pBeStock.Fecha_vence
            pBeMovimiento.Fecha = Now
            pBeMovimiento.Barra_pallet = pBeStock.Lic_plate
            pBeMovimiento.Hora_ini = Now
            pBeMovimiento.Hora_fin = Now
            pBeMovimiento.Fecha_agr = Now
            pBeMovimiento.Usuario_agr = UsuarioHH
            pBeMovimiento.Cantidad_hist = pBeStock.Cantidad
            pBeMovimiento.Peso_hist = pBeStock.Peso
            pBeMovimiento.IdOperadorBodega = UsuarioHH
            pBeMovimiento.Lic_plate = bePickingUbic.Lic_plate

            'Modificar campos en el stock
            pBeStock.Cantidad = CantNoEncontrada
            pBeStock.IdProductoEstado = beBodega.IdProductoEstadoNE
            pBeStock.ProductoEstado.IdEstado = beBodega.IdProductoEstadoNE
            pBeStock.IdUbicacion = beBodega.ubic_producto_ne

            pListMov.Add(pBeMovimiento)
            pListStock.Add(pBeStock)

            If Not UbicaAuto Then

                clsLnTrans_ubic_hh_enc.Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                                               beListUbicHHDet,
                                                                               Nothing,
                                                                               pListMov,
                                                                               True,
                                                                               pBePropietarioBodega.IdPropietario,
                                                                               pListStock,
                                                                               Nothing,
                                                                               Nothing,
                                                                               beTareaHH.IdTareahh,
                                                                               pConnection,
                                                                               pTransaction,
                                                                               pHostSolicita)

            Else

                Dim vStockRes As New clsBeVW_stock_res
                vStockRes.IdProductoBodega = pBeStock.IdProductoBodega
                vStockRes.IdUbicacion = pBeMovimiento.IdUbicacionOrigen
                vStockRes.Lote = pBeStock.Lote
                vStockRes.Fecha_Vence = pBeStock.Fecha_vence
                vStockRes.CantidadUmBas = IIf(pBeStock.IdPresentacion <> 0, CantNoEncontrada, pBeStock.Cantidad)
                vStockRes.Peso = pBeStock.Peso
                vStockRes.IdPresentacion = pBeStock.IdPresentacion 'pStock.IdPresentacion '#CKFK 20190208 Aquí estoy enviando la presentación del stock original
                vStockRes.IdProductoEstado = pBeMovimiento.IdEstadoOrigen
                vStockRes.Fecha_ingreso = Now
                vStockRes.ValorFecha = Now

                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pBeMovimiento,
                                                                            vStockRes,
                                                                            False,
                                                                            pConnection,
                                                                            pTransaction)


            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180422 11:08PM: Cree la funcion SustituirProductoNEPicking para poder hacer todos los cambios por los productos no encontrados en el Picking
    Public Shared Function Sustituir_Producto_NE_Picking(ByVal IdPickingEnc As Integer,
                                                         ByVal IdPickingDet As Integer,
                                                         ByVal CantSol As Double,
                                                         ByVal MaquinaQueSolicita As String,
                                                         ByVal UsuarioHH As Integer,
                                                         ByVal lBeStockAReservar As List(Of clsBeStock_res),
                                                         ByVal IdBodega As Integer,
                                                         ByVal IdEmpresa As Integer,
                                                         ByVal IdStockProductoNE As Integer,
                                                         ByVal IdStockResProductoNE As Integer,
                                                         ByRef resultado As String) As Boolean

        Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CantidadStockDestino As Double = 0

        Try

            resultado = ""

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If lBeStockAReservar IsNot Nothing AndAlso lBeStockAReservar.Count > 0 Then

                For Each BeStockRes As clsBeStock_res In lBeStockAReservar

                    'Llena PickingUbic
                    Dim BeNuevoPickingUbic As New clsBeTrans_picking_ubic() With {
                        .IdPedidoDet = BeStockRes.IdPedidoDet,
                        .IdStockRes = BeStockRes.IdStockRes,
                        .IdPickingDet = IdPickingDet,
                        .IdStock = BeStockRes.IdStock,
                        .IdPropietarioBodega = BeStockRes.IdPropietarioBodega,
                        .IdProductoBodega = BeStockRes.IdProductoBodega,
                        .IdProductoEstado = BeStockRes.IdProductoEstado,
                        .IdPresentacion = BeStockRes.IdPresentacion,
                        .IdUnidadMedida = BeStockRes.IdUnidadMedida,
                        .IdUbicacionAnterior = Val(BeStockRes.Ubicacion_ant),
                        .IdRecepcion = BeStockRes.IdRecepcion,
                        .IdUbicacion = BeStockRes.IdUbicacion,
                        .Lote = BeStockRes.Lote,
                        .Fecha_Vence = BeStockRes.Fecha_vence,
                        .Serial = BeStockRes.Serial,
                        .Lic_plate = BeStockRes.Lic_plate,
                        .Peso_solicitado = BeStockRes.Peso,
                        .Cantidad_Solicitada = BeStockRes.Cantidad,
                        .Cantidad_Recibida = 0.0,
                        .Peso_recibido = 0,
                        .Cantidad_Verificada = 0,
                        .Peso_verificado = 0,
                        .Cantidad_despachada = 0,
                        .Peso_despachado = 0,
                        .Fecha_real_vence = BeStockRes.Fecha_vence,
                        .Encontrado = False,
                        .Dañado_picking = False,
                        .Dañado_verificacion = False,
                        .User_agr = UsuarioHH,
                        .Fec_agr = Now,
                        .User_mod = UsuarioHH,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True,
                        .IdBodega = IdBodega}

                    pListBePickingUbic.Add(BeNuevoPickingUbic)

                    BeStockRes.Estado = "UNCOMMITED"

                    CantidadStockDestino = BeStockRes.Cantidad

                    Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BeStockRes.IdBodega, lConnection, lTransaction)
                    clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

                    'If Math.Abs(CantidadStockDestino - Fix(CantidadStockDestino)) Then
                    '    Throw New Exception("Error_202303101448T: El valor a insertar en stock_res sería un valor decimal no válido, se prevendrá continuar para evitar inconvenientes en reserva.")
                    'End If

                    clsLnStock_res.Actualizar(BeStockRes, lConnection, lTransaction)

                Next

            End If

            resultado += " Se reservó el nuevo stock"

            ' Picking Detalle Ubicacion
            Guarda_Trans_picking_ubic(IdPickingEnc, pListBePickingUbic, lConnection, lTransaction)

            resultado += " Se guardó el nuevo trans_picking_ubic"

            '#CKFK 20180422 12:00 AM Marca los productos no encontrador y modifica el StockRes
            resultado += Marcar_No_Encontrado(IdBodega,
                                              IdEmpresa,
                                              IdStockProductoNE,
                                              IdStockResProductoNE,
                                              UsuarioHH,
                                              CantSol,
                                              lBeStockAReservar(0).IdPropietarioBodega,
                                              pListBePickingUbic(0).IdPickingUbic,
                                              lConnection,
                                              lTransaction)

            resultado += " Se marcó el no encontrado"

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20180422 11:08PM: Cree la funcion SustituirProductoNEPicking para poder hacer todos los cambios por los productos no encontrados en el Picking
    Public Shared Function SustituirProductoNEPicking_Test(ByVal IdPickingEnc As Integer,
                                                           ByVal IdPickingDet As Integer,
                                                           ByVal CantSol As Double,
                                                           ByVal MaquinaQueSolicita As String,
                                                           ByVal UsuarioHH As Integer,
                                                           ByVal IdBodega As Integer,
                                                           ByVal IdEmpresa As Integer,
                                                           ByVal IdStockProductoNE As Integer,
                                                           ByVal IdStockResProductoNE As Integer,
                                                           ByVal IdPedidoDet As Integer,
                                                           ByVal IdPropietarioBodega As Integer,
                                                           ByVal IdPedidoEnc As Integer) As Boolean

        Dim pListBePickingUbic As New List(Of clsBeTrans_picking_ubic)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim vResult As String = ""

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lBeStockAReservar As New List(Of clsBeStock_res)

            lBeStockAReservar = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(IdPedidoDet, IdPropietarioBodega, IdPickingEnc, IdPedidoEnc, lConnection, lTransaction)

            If lBeStockAReservar IsNot Nothing AndAlso lBeStockAReservar.Count > 0 Then

                For Each bo As clsBeStock_res In lBeStockAReservar

                    'Llena PickingUbic
                    Dim ObjU As New clsBeTrans_picking_ubic() With {
                        .IdPedidoDet = bo.IdPedidoDet,
                        .IdStockRes = bo.IdStockRes,
                        .IdPickingDet = IdPickingDet,
                        .IdStock = bo.IdStock,
                        .IdPropietarioBodega = bo.IdPropietarioBodega,
                        .IdProductoBodega = bo.IdProductoBodega,
                        .IdProductoEstado = bo.IdProductoEstado,
                        .IdPresentacion = bo.IdPresentacion,
                        .IdUnidadMedida = bo.IdUnidadMedida,
                        .IdUbicacionAnterior = Val(bo.Ubicacion_ant),
                        .IdRecepcion = bo.IdRecepcion,
                        .IdUbicacion = bo.IdUbicacion,
                        .Lote = bo.Lote,
                        .Fecha_Vence = bo.Fecha_vence,
                        .Serial = bo.Serial,
                        .Lic_plate = bo.Lic_plate,
                        .Peso_solicitado = bo.Peso,
                        .Cantidad_Solicitada = bo.Cantidad,
                        .Cantidad_Recibida = 0.0,
                        .Peso_recibido = 0,
                        .Cantidad_Verificada = 0,
                        .Peso_verificado = 0,
                        .Cantidad_despachada = 0,
                        .Peso_despachado = 0,
                        .Fecha_real_vence = bo.Fecha_vence,
                        .Encontrado = False,
                        .Dañado_picking = False,
                        .Dañado_verificacion = False,
                        .User_agr = UsuarioHH,
                        .Fec_agr = Now,
                        .User_mod = UsuarioHH,
                        .Fec_mod = Now,
                        .Activo = True,
                        .IsNew = True}

                    pListBePickingUbic.Add(ObjU)

                    bo.Estado = "UNCOMMITED"
                    clsLnStock_res.Actualizar(bo, lConnection, lTransaction)

                Next

            End If

            ' Picking Detalle Ubicacion
            vResult += Guarda_Trans_picking_ubic(IdPickingEnc, pListBePickingUbic, lConnection, lTransaction)

            '#CKFK 20180422 12:00 AM Marca los productos no encontrador y modifica el StockRes
            Marcar_No_Encontrado(IdBodega, IdEmpresa, IdStockProductoNE, IdStockResProductoNE, UsuarioHH, CantSol, lBeStockAReservar(0).IdPropietarioBodega,
                               pListBePickingUbic(0).IdPickingUbic, lConnection, lTransaction)

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function ObtienIdPickingEnc_Y_Verifica_Estado_PickingEnc_ByPickingUbic(ByVal IdPickingUbic As Integer,
                                                                                         ByRef pConnection As SqlConnection,
                                                                                         ByRef pTransaction As SqlTransaction) As Integer

        Dim cmd As New SqlCommand

        ObtienIdPickingEnc_Y_Verifica_Estado_PickingEnc_ByPickingUbic = 0

        Try

            Const sp As String = "SELECT trans_picking_enc.IdPickingEnc,estado FROM trans_picking_enc inner join
                                    trans_picking_det ON trans_picking_det.IdPickingEnc = trans_picking_enc.IdPickingEnc inner join
                                    trans_picking_ubic ON trans_picking_det.IdPickingDet = trans_picking_ubic.IdPickingDet
                                     Where (trans_picking_ubic.IdPickingUbic = @IdPickingUbic)"

            cmd = New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingUbic", IdPickingUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                If dt.Rows(0).Item("estado") = "Procesado" Then
                    ObtienIdPickingEnc_Y_Verifica_Estado_PickingEnc_ByPickingUbic = dt.Rows(0).Item("IdPickingEnc")
                End If
            End If

        Catch ex As Exception
            '#MECR23102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pIdPickingUbic:=IdPickingUbic, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_Tipo(ByVal pIdPickingEnc As Integer,
                                                                    ByVal pDetalleOperador As Boolean,
                                                                    ByVal pIdOperadorBodega As Integer,
                                                                    ByVal Tipo As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_PickingUbic_By_IdPickingEnc_Tipo = Nothing

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#EJC202203403:Consolidado
            If Tipo = 1 Then
                lReturnList = Get_All_PickingUbic_By_IdPickingEnc_Consolidado_Reducido(pIdPickingEnc, pDetalleOperador, pIdOperadorBodega, lConnection, lTransaction)
            Else
                lReturnList = Get_All_PickingUbic_By_IdPickingEnc_Detallado(pIdPickingEnc, pDetalleOperador, pIdOperadorBodega, lConnection, lTransaction)
            End If

            Get_All_PickingUbic_By_IdPickingEnc_Tipo = lReturnList

            lTransaction.Commit()

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualiza_Picking_Consolidado_Cm(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                                         ByVal pIdOperador As Integer,
                                                         ByVal pHost As String) As Boolean

        Actualiza_Picking_Consolidado_Cm = False

        Dim CantPendiente As Double
        Dim PesoPendiente As Double
        Dim BeStockResActual As New clsBeStock_res
        Dim BePickingDet As New clsBeTrans_picking_det
        Dim pBePickingUbicList As New List(Of clsBeTrans_picking_ubic)
        Dim tmpPickingUbicList As New List(Of clsBeTrans_picking_ubic)
        Dim BePedidoDet As New clsBeTrans_pe_det
        Dim resultado As String = ""
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Factor As Integer = 0
        Dim vIdUbicacionPickingByBodega As Integer = 0
        Dim BeStockActual As New clsBeStock

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim watch As Stopwatch = Stopwatch.StartNew()

            pBePickingUbicList = Get_All_PickingUbic_By_Consolidado_Cm(pBePickingUbic, lConnection, lTransaction)

            For Each vBePickingUbic As clsBeTrans_picking_ubic In pBePickingUbicList

                CantPendiente = vBePickingUbic.Cantidad_Solicitada
                PesoPendiente = vBePickingUbic.Peso_solicitado

                vBePickingUbic.Cantidad_Recibida = CantPendiente
                vBePickingUbic.Cantidad_Recibida = Math.Round(vBePickingUbic.Cantidad_Recibida, 6)
                vBePickingUbic.Peso_recibido = PesoPendiente
                vBePickingUbic.IdOperadorBodega_Pickeo = pBePickingUbic.IdOperadorBodega_Pickeo
                vBePickingUbic.Fec_mod = Now
                vBePickingUbic.Fecha_picking = Now

                BePickingDet = New clsBeTrans_picking_det
                BePickingDet.IdPickingDet = vBePickingUbic.IdPickingDet
                clsLnTrans_picking_det.Obtener(BePickingDet, lConnection, lTransaction)

                BePedidoDet = New clsBeTrans_pe_det
                BePedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(BePickingDet.IdPedidoEnc,
                                                                                          BePickingDet.IdPedidoDet,
                                                                                          lConnection,
                                                                                          lTransaction)

                If BePedidoDet.IdPresentacion <> vBePickingUbic.IdPresentacion Then
                    If BePedidoDet.IdPresentacion = 0 AndAlso vBePickingUbic.IdPresentacion <> 0 Then
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBePickingUbic.IdProductoBodega,
                                                                                           vBePickingUbic.IdPresentacion,
                                                                                           lConnection,
                                                                                           lTransaction)
                        If Factor <> 0 Then
                            Dim CantPedido As Double = CantPendiente * Factor
                            BePickingDet.Cantidad_recibida += CantPedido
                            BePickingDet.Cantidad_recibida = Math.Round(BePickingDet.Cantidad_recibida, 6)
                        End If
                    End If
                Else
                    BePickingDet.Cantidad_recibida += CantPendiente
                    BePickingDet.Cantidad_recibida = Math.Round(BePickingDet.Cantidad_recibida, 6)
                End If

                BePickingDet.User_mod = pIdOperador
                BePickingDet.Fec_mod = Now

                BeStockResActual.IdStockRes = vBePickingUbic.IdStockRes
                BeStockResActual.IdProductoBodega = vBePickingUbic.IdProductoBodega
                clsLnStock_res.GetSingle(BeStockResActual, lConnection, lTransaction)

                BeStockResActual.Estado = "PICKEADO"
                BeStockResActual.User_mod = pIdOperador
                BeStockResActual.Fec_mod = Now


                Actualizar_Picking(vBePickingUbic,
                                   BeStockResActual,
                                   vBePickingUbic.IdBodega,
                                   vBePickingUbic.Cantidad_Solicitada,
                                   pHost,
                                   lConnection, lTransaction)

            Next

            lTransaction.Commit()

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK 2020508 Creé función para actualizar la cantidad en las verificaciones de la aplicación de Android
    '#AT20220509 Agregué pTipo para validar si se debe buscar por lote y fecha_vencimiento antes
    'de completar el proceso de actualizar la cantidad verificada, pTipo siempre vendré con valor = 0 
    '#AT20241206 Agregue el parametro pIdPedidoEnc
    Public Shared Function Actualiza_Cant_Peso_Verificacion(ByVal pBePickingUbicList As List(Of clsBeTrans_picking_ubic),
                                                            ByVal pIdOperador As Integer,
                                                            ByRef pCantidad As Double,
                                                            ByRef pPeso As Double,
                                                            ByVal pTipo As Integer,
                                                            ByVal pIdPedidoEnc As Integer,
                                                            ByRef pEtiqueta As clsBeTrans_verificacion_etiqueta) As Boolean

        Dim CantPendiente As Double
        Dim PesoPendiente As Double
        Dim BeStockRes As New clsBeStock_res
        Dim resultado As String = ""
        Dim tmpBeListPickingUbic As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BePickingUbic As New clsBeTrans_picking_ubic
        Dim clsTrans As New clsTransaccion

        Actualiza_Cant_Peso_Verificacion = False

        Try
            clsTrans.Open_Connection() : clsTrans.Begin_Transaction()
            BePickingUbic = pBePickingUbicList(0)
            pBePickingUbicList = Nothing

            tmpBeListPickingUbic = Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(BePickingUbic.IdPickingEnc,
                                                                                        False,
                                                                                        0,
                                                                                        pIdPedidoEnc,
                                                                                        clsTrans.lConnection,
                                                                                        clsTrans.lTransaction)

            Dim BeBodega = clsLnBodega.GetSingle_By_Idbodega(BePickingUbic.IdBodega)

            '#AT20220509 Si pTipo <> 0 No aplica buscar por Lote y Fecha Vencimiento
            If pTipo <> 0 Then
                pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.IdPresentacion = BePickingUbic.IdPresentacion And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()
            Else
                If BeBodega IsNot Nothing Then
                    If BeBodega.Agrupar_Sin_Lic_Veri_No_Cons Then
                        pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.Lote = BePickingUbic.Lote And x.Fecha_Vence = BePickingUbic.Fecha_Vence And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()
                    Else
                pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.Lote = BePickingUbic.Lote And x.Fecha_Vence = BePickingUbic.Fecha_Vence And x.Lic_plate = BePickingUbic.Lic_plate And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()

            End If
                Else
                    pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.Lote = BePickingUbic.Lote And x.Fecha_Vence = BePickingUbic.Fecha_Vence And x.Lic_plate = BePickingUbic.Lic_plate And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()
                End If
            End If

            For Each vBePickingUbic As clsBeTrans_picking_ubic In pBePickingUbicList

                If Math.Round(vBePickingUbic.Cantidad_Verificada + pCantidad, 6) > vBePickingUbic.Cantidad_Recibida Then
                    CantPendiente = vBePickingUbic.Cantidad_Recibida - vBePickingUbic.Cantidad_Verificada
                Else
                    CantPendiente = pCantidad
                End If


                If ((vBePickingUbic.Peso_verificado + pPeso) > vBePickingUbic.Peso_recibido) Then
                    PesoPendiente = vBePickingUbic.Peso_recibido - vBePickingUbic.Peso_verificado
                Else
                    PesoPendiente = pPeso
                End If

                vBePickingUbic.Cantidad_Verificada += CantPendiente
                vBePickingUbic.Cantidad_Verificada = Math.Round(vBePickingUbic.Cantidad_Verificada, 6)

                vBePickingUbic.Peso_verificado += PesoPendiente
                vBePickingUbic.Peso_verificado = Math.Round(vBePickingUbic.Peso_verificado, 6)

                vBePickingUbic.IdOperadorBodega_Verifico = pIdOperador
                vBePickingUbic.Fecha_verificado = Now

                BeStockRes.IdStockRes = vBePickingUbic.IdStockRes
                BeStockRes.IdProductoBodega = vBePickingUbic.IdProductoBodega

                clsLnStock_res.GetSingle(BeStockRes,
                                         clsTrans.lConnection,
                                         clsTrans.lTransaction)

                BeStockRes.Estado = "VERIFICADO"
                BeStockRes.User_mod = pIdOperador
                BeStockRes.Fec_mod = Now

                '#GT25042023: cantidad y peso son opcionales, porque el método se llama desde otro lugar, donde no son necesarios dichos valores.
                resultado += Actualizar_PickingUbic_Por_Verificacion(vBePickingUbic,
                                                                     BeStockRes,
                                                                     pCantidad,
                                                                     pPeso,
                                                                     clsTrans.lConnection,
                                                                     clsTrans.lTransaction)
                '#MA20251219
                If BeBodega.impresion_verificacion Then

                    pEtiqueta = New clsBeTrans_verificacion_etiqueta()
                    pEtiqueta =
                    clsLnTrans_verificacion_etiqueta.Guardar_Etiqueta_Verificacion(vBePickingUbic,
                                                                                    pIdOperador,
                                                                                    BeBodega.IdTipoEtiquetaVerificacion,
                                                                                    clsTrans.lConnection,
                                                                                    clsTrans.lTransaction)
                End If

                '#MECR11122025: Se agrego bitacora para logs de verificacion
                resultado += " Codigo " & vBePickingUbic.CodigoProducto & " Pedido parámetro " & pIdPedidoEnc
                clsLnLog_error_wms.Agregar_Error(resultado)
                clsLnLog_verificacion_bof_error.Agregar_Error(resultado,
                  pIdPedidoDet:=vBePickingUbic.IdPedidoDet,
                  pIdPedidoEnc:=vBePickingUbic.IdPedidoEnc,
                  pIdPickingEnc:=vBePickingUbic.IdPickingEnc,
                  pIdPickingDet:=vBePickingUbic.IdPickingDet,
                  pIdPickingUbic:=vBePickingUbic.IdPickingUbic,
                  pConection:=clsTrans.lConnection,
                  pTransaction:=clsTrans.lTransaction)

                If (Math.Round(pCantidad - CantPendiente, 6) = 0) Then
                    Exit For
                Else
                    pCantidad -= CantPendiente
                    pCantidad = Math.Round(pCantidad, 6)
                End If

            Next

            clsTrans.Commit_Transaction()

            Return True

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function
    Public Shared Function Actualiza_Picking_Consolidado(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                                         ByVal pIdOperador As Integer,
                                                         ByVal ReemplazoLP As Boolean,
                                                         ByRef pCantidad As Double,
                                                         ByRef pPeso As Double,
                                                         ByVal BeStockPallet As clsBeProducto,
                                                         ByVal pHost As String) As Boolean

        Actualiza_Picking_Consolidado = False

        Dim CantPendiente As Double
        Dim PesoPendiente As Double
        Dim BeStockResActual As New clsBeStock_res
        Dim BePickingDet As New clsBeTrans_picking_det
        Dim pBePickingUbicList As New List(Of clsBeTrans_picking_ubic)
        Dim tmpPickingUbicList As New List(Of clsBeTrans_picking_ubic)
        Dim BePedidoDet As New clsBeTrans_pe_det
        Dim resultado As String = ""
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Factor As Integer = 0
        Dim vIdUbicacionPickingByBodega As Integer = 0
        Dim BeStockActual As New clsBeStock

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim watch As Stopwatch = Stopwatch.StartNew()

            pBePickingUbicList = Get_All_PickingUbic_By_Consolidado(pBePickingUbic, lConnection, lTransaction)

            vIdUbicacionPickingByBodega = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(pBePickingUbicList.FirstOrDefault.IdBodega(),
                                                                                          lConnection,
                                                                                          lTransaction)

            For Each vBePickingUbic As clsBeTrans_picking_ubic In pBePickingUbicList

                If (vBePickingUbic.Cantidad_Recibida + pCantidad) > vBePickingUbic.Cantidad_Solicitada Then
                    CantPendiente = Math.Round(vBePickingUbic.Cantidad_Solicitada - vBePickingUbic.Cantidad_Recibida, 6)
                Else
                    CantPendiente = pCantidad
                End If

                If ((vBePickingUbic.Peso_recibido + pPeso) > vBePickingUbic.Peso_solicitado) Then
                    PesoPendiente = vBePickingUbic.Peso_solicitado - vBePickingUbic.Peso_recibido
                Else
                    PesoPendiente = pPeso
                End If

                vBePickingUbic.Cantidad_Recibida += CantPendiente
                vBePickingUbic.Cantidad_Recibida = Math.Round(vBePickingUbic.Cantidad_Recibida, 6)
                vBePickingUbic.Peso_recibido += PesoPendiente
                vBePickingUbic.IdOperadorBodega_Pickeo = pBePickingUbic.IdOperadorBodega_Pickeo
                vBePickingUbic.Fec_mod = Now
                vBePickingUbic.Fecha_picking = Now

                BePickingDet = New clsBeTrans_picking_det
                BePickingDet.IdPickingDet = vBePickingUbic.IdPickingDet
                clsLnTrans_picking_det.Obtener(BePickingDet, lConnection, lTransaction)

                BePedidoDet = New clsBeTrans_pe_det
                BePedidoDet = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(BePickingDet.IdPedidoEnc,
                                                                                          BePickingDet.IdPedidoDet,
                                                                                          lConnection,
                                                                                          lTransaction)

                If BePedidoDet.IdPresentacion <> vBePickingUbic.IdPresentacion Then
                    If BePedidoDet.IdPresentacion = 0 AndAlso vBePickingUbic.IdPresentacion <> 0 Then
                        Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(vBePickingUbic.IdProductoBodega,
                                                                                           vBePickingUbic.IdPresentacion,
                                                                                           lConnection,
                                                                                           lTransaction)
                        If Factor <> 0 Then
                            Dim CantPedido As Double = CantPendiente * Factor
                            BePickingDet.Cantidad_recibida += CantPedido
                            BePickingDet.Cantidad_recibida = Math.Round(BePickingDet.Cantidad_recibida, 6)
                        End If
                    End If
                Else
                    BePickingDet.Cantidad_recibida += CantPendiente
                    BePickingDet.Cantidad_recibida = Math.Round(BePickingDet.Cantidad_recibida, 6)
                End If

                BePickingDet.User_mod = pIdOperador
                BePickingDet.Fec_mod = Now

                BeStockResActual.IdStockRes = vBePickingUbic.IdStockRes
                BeStockResActual.IdProductoBodega = vBePickingUbic.IdProductoBodega
                clsLnStock_res.GetSingle(BeStockResActual, lConnection, lTransaction)

                BeStockResActual.Estado = "PICKEADO"
                BeStockResActual.User_mod = pIdOperador
                BeStockResActual.Fec_mod = Now

                If Not ReemplazoLP Then
                    Actualizar_Picking_Desde_Consolidado(vBePickingUbic,
                                                         BeStockResActual,
                                                         BePickingDet,
                                                         vBePickingUbic.IdBodega,
                                                         pHost,
                                                         pCantidad,
                                                         lConnection,
                                                         lTransaction)

                Else
                    Actualizar_Picking(vBePickingUbic,
                                       BeStockResActual,
                                       BePickingDet,
                                       vBePickingUbic.IdBodega,
                                       BeStockPallet,
                                       lConnection,
                                       lTransaction)
                End If

                If (pCantidad - CantPendiente = 0) Then
                    Exit For
                Else
                    pCantidad -= CantPendiente
                End If

            Next

            lTransaction.Commit()

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            Return True

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    '#CKFK 20200511 Creé esta función para reemplazo de inventario en las verificaciones
    Public Shared Function Reemplaza_Producto_Dannado_Menor(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                                            ByVal pIdStock As Integer,
                                                            ByVal pIdPickingEnc As Integer,
                                                            ByVal pIdOperador As Integer,
                                                            ByVal pHost As String,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdEmpresa As Integer,
                                                            ByVal pIdUbicDestino As Integer,
                                                            ByVal pIdEstadoDestino As Integer,
                                                            ByVal pCantLinea As Double,
                                                            ByRef pCantReemplazar As Double) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Index As Integer = 0
        Dim IdStockForIndex As Integer = 0
        Dim CantidadDañadaTotal As Double = 0
        Dim BeStockRes As New clsBeStock_res
        Dim resultado As String = ""
        Dim BePickingUbic As New clsBeTrans_picking_ubic

        Reemplaza_Producto_Dannado_Menor = False

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CM_20190204: Agregué una variable para no perder la cantidad dañada total y agregué validación para la cantidad a reemplazar.
            For Each BePicking As clsBeTrans_picking_ubic In plistPickingUbi

                CantidadDañadaTotal = BePicking.CantidadDañada

                IdStockForIndex = BePicking.IdStock

                Index = plistPickingUbi.FindIndex(Function(x) x.IdStock = IdStockForIndex)

                Dim CantidadPendiente As Double = 0
                BePickingUbic = New clsBeTrans_picking_ubic

                BePickingUbic.IdStock = pIdStock

                CantidadPendiente = pCantReemplazar - pCantLinea

                If CantidadPendiente > 0 Then
                    BePickingUbic.CantidadDañada = pCantLinea
                ElseIf CantidadPendiente < 0 Then
                    BePickingUbic.CantidadDañada = pCantReemplazar
                End If

                pCantReemplazar -= BePickingUbic.CantidadDañada
                CantidadDañadaTotal -= BePickingUbic.CantidadDañada

                If pCantReemplazar <> 0 Then

                    BePicking.CantidadDañada = BePickingUbic.CantidadDañada

                    Aplica_Cambio_Ubicacion(BePickingUbic, pHost, BePicking, pIdPickingEnc, pIdOperador, pIdBodega, pIdEmpresa, pIdUbicDestino, pIdEstadoDestino, lConnection, lTransaction)

                    'Asigna_Nuevo_Inventario_Disponible(DT_Completo, pIdStock)

                End If

                plistPickingUbi(Index).CantidadDañada = CantidadDañadaTotal
                Exit For

            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Sub Aplica_Cambio_Ubicacion(ByVal pBeTransPicking As clsBeTrans_picking_ubic,
                                              ByVal host As String,
                                              ByVal BePicking As clsBeTrans_picking_ubic,
                                              ByVal pIdPickingEnc As Integer,
                                              ByVal gOperador As Integer,
                                              ByVal pIdBodega As Integer,
                                              ByVal pIdEmpresa As Integer,
                                              ByVal pIdUbicDestino As Integer,
                                              ByVal pIdEstadoDestino As Integer,
                                              ByRef pConnection As SqlConnection,
                                              ByRef pTransaction As SqlTransaction)

        Try


            If pBeTransPicking.IdStock <> BePicking.IdStock Then ' Si el producto en estado no válido para pickear pertenece el mismo IdStock no se debe realizar esto, solo la tarea de cambio de estado


                'Revisar si ReservaStockByIdStock se puede reemplazar por alguna de las funciones que Erik ya tiene.
                'Esta reserva ya no va a ser por IdStock
                clsLnStock_res.Reservar_Stock_By_IdStock(pBeTransPicking.IdStock,
                                                         BePicking.CantidadDañada,
                                                         host,
                                                         pIdPickingEnc,
                                                         BePicking.IdPedidoEnc,
                                                         gOperador,
                                                         BePicking.IdPedidoDet,
                                                         BePicking.IdPickingUbic,
                                                         False,
                                                         pBeTransPicking.IdPresentacion,
                                                         1)


                Dim lBeStockRes As clsBeStock_res()
                Dim lBeStockResAux As New List(Of clsBeStock_res)
                Dim CantRes As Integer = 0

                lBeStockResAux = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(BePicking.IdPedidoDet,
                                                                                 BePicking.IdPropietarioBodega,
                                                                                 BePicking.IdPickingEnc,
                                                                                 BePicking.IdPedidoEnc,
                                                                                 pConnection,
                                                                                 pTransaction).ToList

                If lBeStockResAux IsNot Nothing Then

                    CantRes = lBeStockResAux.Count - 1

                    'Detalle Stock Reservado
                    Dim I As Integer = 0

                    ReDim lBeStockRes(CantRes)

                    For Each StockRes As clsBeStock_res In lBeStockResAux

                        lBeStockRes(I) = StockRes
                        I += 1

                    Next

                    Reemplazo_Producto_En_Picking(BePicking.IdStock,
                                                  pIdPickingEnc,
                                                  BePicking.IdPickingDet,
                                                  BePicking.CantidadDañada,
                                                  host,
                                                  gOperador,
                                                  lBeStockRes.ToList,
                                                  pIdBodega,
                                                  pIdEmpresa,
                                                  pIdUbicDestino,
                                                  pIdEstadoDestino,
                                                  BePicking.IdStockRes,
                                                  False,
                                                  pConnection,
                                                  pTransaction)

                End If

            Else

                clsLnTrans_ubic_hh_enc.Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(pIdBodega,
                                                                                      pIdEmpresa,
                                                                                      BePicking.IdStock,
                                                                                      BePicking.IdStockRes,
                                                                                      gOperador,
                                                                                      BePicking.CantidadDañada,
                                                                                      pIdUbicDestino,
                                                                                      pIdEstadoDestino,
                                                                                      BePicking.IdPropietarioBodega,
                                                                                      BePicking.IdPickingUbic,
                                                                                      False,
                                                                                      host,
                                                                                      pConnection,
                                                                                      pTransaction)

            End If


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        End Try

    End Sub

    Public Shared Sub Asigna_Nuevo_Inventario_Disponible(ByRef Inventario As DataTable, ByVal IdStock As Integer)

        Dim data As New Object

        Try

            data = Inventario.AsEnumerable().Select(Function(Z) New With
                                       {
                                       .Codigo = Z.Field(Of String)("Codigo"),
                                       .Nombre = Z.Field(Of String)("Nombre"),
                                       .Presentacion = IIf(String.IsNullOrEmpty(Z.Field(Of String)("Presentacion")), "N/A", Z.Field(Of String)("Presentacion")),
                                       .UnidadMedida = Z.Field(Of String)("UnidadMedida"),
                                       .Disponible_UMBas = Z.Field(Of Double)("Disponible_UMBas"),
                                       .IdUbicacion = Z.Field(Of Integer)("IdUbicacion"),
                                       .Vence = Z.Field(Of Date)("Vence"),
                                       .Lic_Plate = IIf(String.IsNullOrEmpty(Z.Field(Of String)("Lic_Plate")), "", Z.Field(Of String)("Lic_Plate")),
                                       .Lote = Z.Field(Of String)("Lote"),
                                       .IdProductoBodega = Z.Field(Of Integer)("IdProductoBodega"),
                                       .Peso = Z.Field(Of Double)("Peso"),
                                       .NomEstado = Z.Field(Of String)("NomEstado"),
                                       .IdStock = Z.Field(Of Integer)("IdStock"),
                                       .Aplica = Z.Field(Of String)("Aplica")
                                       }).Where(Function(x) x.IdStock <> IdStock)

            If Not data Is Nothing Then
                ' Dim table As DataTable = EQToDataTable(data)
                Inventario = data
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        End Try

    End Sub

    '#CKFK 20200511 Creé esta función para reemplazo de inventario en las verificaciones de la aplicación de Android
    Public Shared Function Reemplaza_Producto_Dannado_Mayor_Igual(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                                                  ByVal pIdStock As Integer,
                                                                  ByVal pIdPickingEnc As Integer,
                                                                  ByVal pIdOperador As Integer,
                                                                  ByVal pHost As String,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal pIdEmpresa As Integer,
                                                                  ByVal pIdUbicDestino As Integer,
                                                                  ByVal pIdEstadoDestino As Integer,
                                                                  ByVal pCantLinea As Double,
                                                                  ByRef pCantReemplazar As Double) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeStockRes As New clsBeStock_res

        Reemplaza_Producto_Dannado_Mayor_Igual = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each BePicking As clsBeTrans_picking_ubic In plistPickingUbi

                If BePicking.Cantidad_Recibida >= pCantReemplazar Then

                    BePicking.CantidadDañada = pCantReemplazar
                    pCantReemplazar = 0

                Else

                    BePicking.CantidadDañada = BePicking.Cantidad_Recibida
                    pCantReemplazar -= BePicking.Cantidad_Recibida

                End If

                If BePicking.CantidadDañada > 0 Then

                    If pIdStock <> BePicking.IdStock Then ' Si el producto en estado no válido para pickear pertenece el mismo IdStock no se debe realizar esto, solo la tarea de cambio de estado

                        'Esta reserva ya no va a ser por IdStock
                        clsLnStock_res.Reservar_Stock_By_IdStock(pIdStock, BePicking.CantidadDañada, pHost,
                                                      pIdPickingEnc, BePicking.IdPedidoEnc,
                                                      pIdOperador, BePicking.IdPedidoDet,
                                                      BePicking.IdPickingUbic, False, BePicking.IdPresentacion, 1)

                        Dim lBeStockRes As clsBeStock_res()
                        Dim lBeStockResAux As New List(Of clsBeStock_res)
                        Dim CantRes As Integer = 0

                        lBeStockResAux = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(BePicking.IdPedidoDet,
                                                                    BePicking.IdPropietarioBodega,
                                                                    BePicking.IdPickingEnc,
                                                                    BePicking.IdPedidoEnc).ToList

                        If lBeStockResAux IsNot Nothing Then

                            CantRes = lBeStockResAux.Count - 1

                            'Detalle Stock Reservado
                            Dim I As Integer = 0

                            ReDim lBeStockRes(CantRes)

                            For Each StockRes As clsBeStock_res In lBeStockResAux

                                lBeStockRes(I) = StockRes
                                I += 1

                            Next

                            Reemplazo_Producto_En_Picking(BePicking.IdStock,
                                                          pIdPickingEnc,
                                                          BePicking.IdPickingDet,
                                                          BePicking.CantidadDañada,
                                                          pHost,
                                                          pIdOperador,
                                                          lBeStockRes.ToList,
                                                          pIdBodega,
                                                          pIdEmpresa,
                                                          pIdUbicDestino,
                                                          pIdEstadoDestino,
                                                          BePicking.IdStockRes,
                                                          False,
                                                          lConnection,
                                                          lTransaction)

                        End If


                    Else

                        clsLnTrans_ubic_hh_enc.Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(pIdBodega,
                                                                                              pIdEmpresa,
                                                                                              BePicking.IdStock,
                                                                                              BePicking.IdStockRes,
                                                                                              pIdOperador,
                                                                                              BePicking.CantidadDañada,
                                                                                              pIdUbicDestino,
                                                                                              pIdEstadoDestino,
                                                                                              BePicking.IdPropietarioBodega,
                                                                                              BePicking.IdPickingUbic,
                                                                                              False,
                                                                                              pHost, lConnection, lTransaction)

                    End If

                Else
                    Exit For
                End If

            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK 20200514 Creé esta función para marcar dañados todos los picking ubic del pedido det cuando no haya existencias en la aplicación de Android
    Public Shared Function Marcar_Danado(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                         ByVal pCantidad As Double,
                                         ByVal pIdBodega As Integer,
                                         ByVal pIdEmpresa As Integer,
                                         ByVal pIdUbicDestino As Integer,
                                         ByVal pIdEstadoDestino As Integer,
                                         ByVal pIdOperador As Integer,
                                         ByVal pHostSolicita As String) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CantPendiente As Double = 0

        Marcar_Danado = False

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Recorrer los IdPickingUbic
            For Each ubi As clsBeTrans_picking_ubic In plistPickingUbi

                If ubi.Cantidad_Verificada + pCantidad > ubi.Cantidad_Recibida Then
                    CantPendiente = ubi.Cantidad_Recibida - ubi.Cantidad_Verificada
                Else
                    CantPendiente = pCantidad
                End If

                clsLnTrans_ubic_hh_enc.Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(pIdBodega,
                                                                                      pIdEmpresa,
                                                                                      ubi.IdStock,
                                                                                      ubi.IdStockRes,
                                                                                      pIdOperador,
                                                                                      CantPendiente,
                                                                                      pIdUbicDestino,
                                                                                      pIdEstadoDestino,
                                                                                      ubi.IdPropietarioBodega,
                                                                                      ubi.IdPickingUbic,
                                                                                      False,
                                                                                      pHostSolicita,
                                                                                      lConnection,
                                                                                      lTransaction)

                If pCantidad - CantPendiente = 0 Then
                    Exit For
                Else
                    pCantidad -= CantPendiente
                End If

            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                               ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try
            '#CKFK20250626 Agregué esto pu.cantidad_verificada<>pu.cantidad_despachada
            Dim vSQL As String = "SELECT  pu.IdPickingEnc, 0 IdPickingUbic, max(pu.IdPickingDet) IdPickingDet, max(pu.IdUbicacion) IdUbicacion, 
                                            max(pu.IdStock) IdStock, pu.IdPropietarioBodega, pu.IdProductoEstado, pu.IdUnidadMedida, 
		                                    max(pu.IdUbicacionAnterior) IdUbicacionAnterior, MAX(pu.IdRecepcion) IdRecepcion, pu.lote, pu.fecha_vence, pu.fecha_minima, pu.serial, 
		                                    pu.lic_plate, 0 acepto, SUM(pu.peso_solicitado) peso_solicitado, SUM(pu.peso_recibido) peso_recibido, 
		                                    SUM(pu.peso_verificado) peso_verificado, SUM(pu.peso_despachado) peso_despachado, 
                                            SUM(pu.cantidad_solicitada) cantidad_solicitada, SUM(pu.cantidad_recibida) cantidad_recibida, 
                                            SUM(pu.cantidad_verificada) cantidad_verificada, 0 encontrado, 0 dañado_verificacion,
                                            pu.fecha_real_vence, isnull(pu.no_packing,0) no_packing, max(CONVERT(DATE,pu.fecha_picking)) fecha_picking, 
		                                    max(CONVERT(DATE,pu.fecha_verificado))fecha_verificado, max(CONVERT(DATE,pu.fecha_packing)) fecha_packing,
		                                    max(CONVERT(DATE,pu.fecha_despachado))fecha_despachado, SUM(pu.cantidad_despachada) cantidad_despachada, 
		                                    '' user_agr, max(CONVERT(DATE,pu.fec_agr)) fec_agr, '' user_mod, max(CONVERT(DATE,pu.fec_mod)) fec_mod, 
		                                    pu.activo, pu.dañado_picking, max(pu.IdStockRes) IdStockRes, 
		                                    pu.lic_plate_reemplazo, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pdet.IdPedidoEnc, 
		                                    max(pdet.IdPedidoDet) IdPedidoDet, pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, 
		                                    pdet.codigo_producto, pdet.nombre_producto, pdet.nom_presentacion, pdet.nom_unid_med, 
                                            dbo.producto_estado.nombre AS nom_estado, pdet.IdEstado, max(pdet.Peso), max(pdet.Precio),pu.IdBodega, 
		                                    '' AS NombreUbicacion, pu.IdUbicacionTemporal, 
		                                    pu.IdPedidoEnc, 0 IdOperadorBodega_Pickeo, 0 IdOperadorBodega_Verifico,
                                            0 IdOperadorBodega_Asignado,pe.bodega_destino Referencia, pu.IdProductoTallaColor
                                    FROM    dbo.bodega_sector INNER JOIN
                                            dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
                                            dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                                            dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                                            dbo.trans_picking_ubic AS pu ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND pu.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                                            dbo.trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet AND pkdet.IdPickingEnc = pu.IdPickingEnc INNER JOIN
                                            dbo.trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
                                            dbo.stock_res AS sr ON pu.IdUbicacion = sr.IdUbicacion AND pu.IdStockRes = sr.IdStockRes AND pu.IdPropietarioBodega = sr.IdPropietarioBodega AND pu.IdProductoBodega = sr.IdProductoBodega AND 
                                            pu.IdPedidoDet = sr.IdPedidoDet INNER JOIN
                                            dbo.bodega ON dbo.bodega_area.IdBodega = dbo.bodega.IdBodega INNER JOIN
                                            dbo.trans_picking_enc ON pkdet.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc INNER JOIN
                                            dbo.producto_estado ON pu.IdProductoEstado = dbo.producto_estado.IdEstado INNER JOIN
		                                    dbo.trans_pe_enc pe ON pe.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc and
		                                    pdet.IdPedidoEnc = pe.IdPedidoEnc
                                    WHERE  (pu.IdPickingEnc=@IdPickingEnc AND pu.IdPedidoEnc = @IdPedidoEnc AND 
                                            pu.cantidad_verificada > 0 AND pu.dañado_picking = 0 AND 
                                            pu.no_encontrado = 0 AND pu.dañado_verificacion = 0 AND 
                                            pu.cantidad_verificada<>pu.cantidad_despachada)
									GROUP BY pu.IdPickingEnc,  pu.IdPropietarioBodega, pu.IdProductoEstado, pu.IdUnidadMedida, 
                                            pu.lote, pu.fecha_vence, pu.fecha_minima, pu.serial, 
                                            pu.lic_plate, 
                                            pu.fecha_real_vence, isnull(pu.no_packing,0),
                                            pu.activo, pu.dañado_picking, 
                                            pu.lic_plate_reemplazo, pu.IdUbicacion_reemplazo, pu.IdStock_reemplazo, pdet.IdPedidoEnc, 
                                            pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, 
                                            pdet.codigo_producto, pdet.nombre_producto, pdet.nom_presentacion, pdet.nom_unid_med, 
                                            dbo.producto_estado.nombre, pdet.IdEstado,pu.IdBodega, 
                                            pu.IdUbicacionTemporal, 
                                            pu.IdPedidoEnc,pe.bodega_destino, pu.IdProductoTallaColor "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                                    .IdPickingEnc = pIdPickingEnc
                                    .IsNew = False

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

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_Group(ByVal pIdPickingEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try


            Dim vSQL As String = "
                SELECT   pu.IdPickingEnc, 0 AS IdPickingUbic, 0 AS IdPickingDet, 0 AS IdUbicacion, 0 AS IdStock, 0 AS IdPropietarioBodega, pu.IdProductoBodega, pu.IdProductoEstado, pu.IdPresentacion, pu.IdUnidadMedida, 0 AS IdUbicacionAnterior, 
                         0 AS IdRecepcion, pu.lote, pu.fecha_vence, NULL AS fecha_minima, '' AS serial, '' AS lic_plate, 0 AS acepto, 0 AS peso_solicitado, 0 AS peso_recibido, 0 AS peso_verificado, 0 AS peso_despachado, SUM(pu.cantidad_solicitada) 
                         AS cantidad_solicitada, SUM(pu.cantidad_recibida) AS cantidad_recibida, 0 AS cantidad_verificada, 0 AS encontrado, 0 AS dañado_verificacion, NULL AS fecha_real_vence, 0 AS no_packing, NULL AS fecha_picking, NULL AS fecha_verificado, NULL 
                         AS fecha_packing, NULL AS fecha_despachado, SUM(pu.cantidad_despachada) AS cantidad_despachada, 0 AS user_agr, NULL AS fec_agr, 0 AS user_mod, NULL AS fec_mod, 0 AS activo, 0 AS IdPedidoDet, 0 AS dañado_picking,
                          0 AS IdStockRes, '' AS lic_plate_reemplazo, 0 AS IdUbicacion_reemplazo, 0 AS IdStock_reemplazo, 0 AS IdPedidoEnc, 0 AS Expr1, pdet.IdPresentacion AS Expr2, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega AS Expr3, 
                         pdet.codigo_producto, pdet.nombre_producto, pdet.nom_presentacion, pdet.nom_unid_med, dbo.producto_estado.nombre AS nom_estado, 0 AS IdEstado, 0 AS Peso, 0 AS Precio, 0 AS Sr_IdStockRes, 0 AS Sr_IdStock, 
                         0 AS IdBodega, 0 AS IdOperadorBodega_Pickeo, 0 AS IdOperadorBodega_Verifico, '' AS NombreUbicacion, pu.IdUbicacionTemporal, pu.IdOperadorBodega_Asignado
                FROM     dbo.bodega_sector INNER JOIN
                         dbo.bodega_area ON dbo.bodega_sector.IdArea = dbo.bodega_area.IdArea AND dbo.bodega_sector.IdBodega = dbo.bodega_area.IdBodega INNER JOIN
                         dbo.bodega_tramo ON dbo.bodega_sector.IdSector = dbo.bodega_tramo.IdSector AND dbo.bodega_sector.IdBodega = dbo.bodega_tramo.IdBodega INNER JOIN
                         dbo.bodega_ubicacion ON dbo.bodega_tramo.IdTramo = dbo.bodega_ubicacion.IdTramo AND dbo.bodega_tramo.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                         dbo.trans_picking_ubic AS pu ON pu.IdUbicacion = dbo.bodega_ubicacion.IdUbicacion AND pu.IdBodega = dbo.bodega_ubicacion.IdBodega INNER JOIN
                         dbo.trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet AND pkdet.IdPickingEnc = pu.IdPickingEnc INNER JOIN
                         dbo.trans_pe_det AS pdet ON pdet.IdPedidoDet = pkdet.IdPedidoDet INNER JOIN
                         dbo.bodega ON dbo.bodega_area.IdBodega = dbo.bodega.IdBodega INNER JOIN
                         dbo.trans_picking_enc ON pkdet.IdPickingEnc = dbo.trans_picking_enc.IdPickingEnc INNER JOIN
                         dbo.producto_estado ON pu.IdProductoEstado = dbo.producto_estado.IdEstado
                GROUP BY pu.IdPickingEnc, pu.IdProductoBodega, pu.IdPresentacion, pu.IdUnidadMedida, pu.lote, pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, pdet.IdProductoBodega, pdet.codigo_producto, pdet.nombre_producto, 
                         pdet.nom_presentacion, pdet.nom_unid_med, pu.fecha_vence, pu.IdProductoEstado, dbo.producto_estado.nombre, pu.IdUbicacionTemporal, pu.IdOperadorBodega_Asignado
                HAVING   (pu.IdPickingEnc=" & pIdPickingEnc & ")"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_picking_ubic

                                Cargar(Obj, lRow)

                                With Obj

                                    .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                                    .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                                    .IdPickingEnc = pIdPickingEnc
                                    .IsNew = False

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

    Public Shared Function Get_All_PickingUbic_By_IdPickingDet(ByVal pIdPickingDet As Integer,
                                                               ByVal pIdPickingEnc As Integer,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingDet
                                  WHERE IdPickingDet=@IdPickingDet 
                                  AND IdPickingEnc = @IdPickingEnc
                                  AND no_encontrado = 0 and dañado_picking = 0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingDet", pIdPickingDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    ''' <summary>
    ''' #EJC20220330: Obtener el registro de picking por IdStock y Bodega asociado para actualizar el IdUbicacionVirtual
    ''' </summary>
    ''' <param name="IdStock"></param>
    ''' <param name="IdBodega"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_IdStock(ByVal IdStock As Integer,
                                                 ByVal IdBodega As Integer,
                                                 Optional pConnection As SqlConnection = Nothing,
                                                 Optional pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_ubic

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Get_Single_By_IdStock = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic Where(IdStock = @Idstock AND IdBodega = @IdBodega)"

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            cmd = New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
                With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@Idstock", IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim vBeTrans_picking_ubic As New clsBeTrans_picking_ubic()
                Cargar(vBeTrans_picking_ubic, dt.Rows(0))
                Get_Single_By_IdStock = vBeTrans_picking_ubic
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_Pendientes_HH_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                                             ByVal pIdOperadorBodega As Integer,
                                                                             ByVal pIdBodega As Integer,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            '#EJC20190625: Agregué el nombre de la ubicación.
            '#CKFK 20180402 06:42 PM Agregué esta condición  " and pu.IdStockRes = sr.IdStockRes " para evitar la duplicidad de registros
            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                  WHERE (IdPickingEnc = @IdPickingEnc AND dañado_picking = 0 and no_encontrado = 0 
                                  AND cantidad_recibida < cantidad_solicitada) "

            '#AT20220611 Filtar detalle picking por IdOperadorBodega
            If pIdOperadorBodega > 0 Then
                vSQL += " AND IdOperadorBodega_Asignado = @IdOperadorBodega"
            End If

            Debug.WriteLine(pIdBodega)

            'Dim BeBodega As New clsBeBodega
            'BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)

            'If Not BeBodega Is Nothing Then
            '    If BeBodega.Mostrar_Area_En_HH Then
            '        '#CKFK20221205 Agregué el ordenamiento por tramo
            '        vSQL += " ORDER BY IdTramo, IdUbicacion, codigo_producto, fecha_vence, nom_estado"
            '    Else
            '        '#CKFK20221205 Agregué el ordenamiento por tramo
            '        If BeBodega.Ordenar_Picking_Descendente Then
            '            vSQL += " ORDER BY dbo.Nombre_Completo_Ubicacion(trans_picking_ubic.IdUbicacion, trans_picking_ubic.IdBodega) "
            '            If BeBodega.Ordenar_Picking_Descendente Then
            '                vSQL += " desc "
            '            End If
            '        Else
            '            vSQL += " ORDER BY Nombre_Tramo, IdUbicacion, codigo_producto, fecha_vence, nom_estado"
            '        End If
            '    End If
            'End If


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pIdOperadorBodega > 0 Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic()

                        Cargar(Obj, lRow)

                        Obj.Ubicacion.IdBodega = lRow.Item("IdBodega")
                        Obj.Ubicacion.IdUbicacion = lRow.Item("IdUbicacion")
                        Obj.Ubicacion.Nivel = lRow.Item("nivel")

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), 0, lRow.Item("NombreUbicacion")) 'Obj.Ubicacion.Descripcion
                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo_producto")), "", lRow.Item("codigo_producto"))
                            .NombreProducto = IIf(IsDBNull(lRow.Item("nombre_producto")), "", lRow.Item("nombre_producto"))
                            .ProductoPresentacion = IIf(IsDBNull(lRow.Item("nom_presentacion")), "", lRow.Item("nom_presentacion"))
                            .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("nom_unid_med")), "", lRow.Item("nom_unid_med"))
                            .ProductoEstado = IIf(IsDBNull(lRow.Item("nom_estado")), "", lRow.Item("nom_estado"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            '#CKFK20240806 Se enviaba en este campo el IdEstado y debía ser IdProductoEstado
                            .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedidaBasica")), 0, lRow.Item("IdUnidadMedidaBasica"))
                            .IdStockRes = IIf(IsDBNull(lRow.Item("IdStockRes")), 0, lRow.Item("IdStockRes"))
                            .IdStock = IIf(IsDBNull(lRow.Item("IdStock")), 0, lRow.Item("IdStock"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .IdPickingEnc = pIdPickingEnc '#CKFK 20180109 09:33 AM Agregué este campo para que se llene al llamar a la función
                            .IsNew = False
                            .No_encontrado = IIf(IsDBNull(lRow.Item("no_encontrado")), False, lRow.Item("no_encontrado"))
                            .NombreClasificacion = IIf(IsDBNull(lRow.Item("Clasificacion")), "", lRow.Item("Clasificacion"))

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

    Public Shared Function Get_Info_Ubicacion_By_ListaPickingUbic(ByRef pListaPickingUbic As List(Of clsBeTrans_picking_ubic)) As Boolean

        Get_Info_Ubicacion_By_ListaPickingUbic = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


            If Not pListaPickingUbic Is Nothing Then

                For Each Pu In pListaPickingUbic

                    Pu.Ubicacion = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Pu.IdUbicacion, Pu.IdBodega, lConnection, lTransaction)

                Next

                Get_Info_Ubicacion_By_ListaPickingUbic = True

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Reporte_Ubicaciones_Resumido(ByVal IdPickingEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Ubicaciones_Picking_Resumido WHERE IdPicking = @IdPickingEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", IdPickingEnc)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
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


    Public Shared Function Get_All_PickingUbic_By_IdStockRes_And_IdStock(ByVal pIdStockRes As Integer,
                                                                         ByVal pIdStock As Integer,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdStockRes_And_IdStock = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM trans_picking_ubic                                  
                                 WHERE IdStockRes = @IdStockRes 
                                 AND IdStock = @IdStock "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdStockRes", pIdStockRes))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", pIdStock))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic()
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                    Get_All_PickingUbic_By_IdStockRes_And_IdStock = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_PickingUbic_Despachado_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_Despachado_By_IdPedidoEnc = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_Desp_By_IdPedidoDet
                                  WHERE IdPedidoEnc = @IdPedidoEnc 
                                  AND dañado_picking=0 
                                  AND dañado_verificacion=0 
                                  AND no_encontrado = 0"
            vSQL += " ORDER BY IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar_For_Despacho(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("Nombre_Ubicacion")), 0, lRow.Item("Nombre_Ubicacion"))
                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo")), "", lRow.Item("codigo"))
                            .NombreProducto = IIf(IsDBNull(lRow.Item("nombre")), "", lRow.Item("nombre"))
                            .ProductoPresentacion = IIf(IsDBNull(lRow.Item("Presentacion")), "", lRow.Item("Presentacion"))
                            .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("UnidadMedida")), "", lRow.Item("UnidadMedida"))
                            .ProductoEstado = IIf(IsDBNull(lRow.Item("NomEstado")), "", lRow.Item("NomEstado"))
                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                            .IdStock = IIf(IsDBNull(lRow.Item("IdStock")), 0, lRow.Item("IdStock"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .Codigo_Talla = IIf(IsDBNull(lRow.Item("Talla")), 0, lRow.Item("Talla"))
                            .Codigo_Color = IIf(IsDBNull(lRow.Item("Color")), 0, lRow.Item("Color"))
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


    Public Shared Function InsertarTmp(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CantidadStockDestino As Double = 0

        Try

            Ins.Init("tmp_picking")
            Ins.Add("fecha_picking", "@FECHA_PICKING", DataType.Parametro)
            Ins.Add("idpickingubic", "@IDPICKINGUBIC", DataType.Parametro)
            Ins.Add("cantidad_solicitada", "@CANTIDAD_SOLICITADA", DataType.Parametro)
            Ins.Add("cantidad_recibida", "@CANTIDAD_RECIBIDA", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@FECHA_PICKING", Now))
            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_SOLICITADA", oBeTrans_picking_ubic.Cantidad_Solicitada))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_picking_ubic.Cantidad_Recibida))

            CantidadStockDestino = oBeTrans_picking_ubic.Cantidad_Solicitada

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(oBeTrans_picking_ubic.IdBodega, pConection, pTransaction)
            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

            'If Math.Abs(CantidadStockDestino - Fix(CantidadStockDestino)) Then
            '    Throw New Exception("Error_202303101448P: El valor a insertar en stock sería un valor decimal no válido, se prevendrá continuar para evitar inconvenientes en reserva.")
            'End If

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

    Public Shared Function Get_All_PickingUbic_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                              ByVal pIdPedidoEnc As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPedidoDet = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BeBodega As New clsBeBodega

        Try

            If pIdPedidoDet = 125710 Then
                Debug.Print("Hola")
            End If

            Dim vSQL As String = " SELECT * FROM VW_PickingUbic_By_IdPedidoDet WHERE IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.CommandTimeout = 100
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoDet", pIdPedidoDet))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    Public Shared Function Existe_IdPickingUbic(ByVal IdPickingUbic As Integer,
                                                Optional pConnection As SqlConnection = Nothing,
                                                Optional pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Existe_IdPickingUbic = False

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic Where(IdPickingUbic = @IdPickingUbic)"

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            cmd = New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
                With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingUbic", IdPickingUbic))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_IdPickingUbic = (dt.Rows.Count > 0)

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT04072024: traer lista de ubicaciones para saber a que área pertenecen en la prefacturacion.

    Public Shared Function Get_Picking_Ubic_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Dim BeListPicking As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT * FROM trans_picking_ubic WHERE IdPedidoEnc=@pIdPedidoEnc and IdBodega=@pIdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@pIdPedidoEnc", pIdPedidoEnc))
                        lDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@pIdBodega", pIdBodega))
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text


                        Dim lDT As New DataTable
                        lDataAdapter.Fill(lDT)

                        Dim Obj As clsBeTrans_picking_ubic

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDT.Rows

                                Obj = New clsBeTrans_picking_ubic
                                Cargar(Obj, lRow)

                                BeListPicking.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return BeListPicking

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Resumen_Pickings_By_Fechas(ByVal FechaDesde As Date, ByVal FechaHasta As Date) As DataTable

        Get_Reporte_Resumen_Pickings_By_Fechas = Nothing

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT 
                                  CAST(fecha_picking AS DATE) AS Fecha,
                                  DATEDIFF(HOUR, MIN(Inicio_Picking), MAX(Fin_Picking))  Horas_Picking_Total,
                                  COUNT(distinct IdPickingEnc) AS Cantidad_Pickings
                                FROM 
                                  (SELECT
                                     u.IdPickingEnc,
                                     CAST(u.fecha_picking AS DATE) AS fecha_picking,
                                     MIN(u.fecha_picking) OVER (PARTITION BY u.IdPickingEnc) AS Inicio_Picking,
                                     MAX(u.fecha_picking) OVER (PARTITION BY u.IdPickingEnc) AS Fin_Picking
                                   FROM [dbo].[trans_picking_enc] p INNER JOIN 
                                     [dbo].[trans_picking_ubic] u ON p.IdPickingEnc = u.IdPickingEnc
                                   WHERE 
                                     u.fecha_picking IS NOT NULL AND cantidad_recibida>0 AND u.activo = 1 AND encontrado = 1 
	                                 AND p.estado <> 'Anulado'
                                     AND CAST(u.fecha_picking AS DATE) <> '19000101'
                                  ) AS SubConsulta
                                WHERE CAST(fecha_picking AS DATE) BETWEEN @FECHA_DESDE AND @FECHA_HASTA                                
                                GROUP BY 
                                  CAST(fecha_picking AS DATE)
                                ORDER BY 
                                  Fecha "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", FechaDesde)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", FechaHasta)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                        Get_Reporte_Resumen_Pickings_By_Fechas = lTable
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

    Public Shared Function Get_Reporte_Detalle_Pedidos_By_Fechas(ByVal FechaDesde As Date, ByVal FechaHasta As Date) As DataTable

        Get_Reporte_Detalle_Pedidos_By_Fechas = Nothing

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT 
                                  subconsulta.IdPedidoEnc,
                                  cliente.nombre_comercial,
                                  CAST(fecha_picking AS DATE) AS Fecha,
                                  CAST(DATEDIFF(MINUTE, MIN(Inicio_Picking), MAX(Fin_Picking)) / 60 AS VARCHAR) + 'h ' 
                                  + CAST(DATEDIFF(MINUTE, MIN(Inicio_Picking), MAX(Fin_Picking)) % 60 AS VARCHAR) + 'm' AS Duracion,
                                  COUNT(distinct subconsulta.IdPedidoEnc) AS Cantidad_Pickings
                                FROM 
                                  (SELECT
                                     u.IdPedidoEnc,
	                                 u.IdBodega,
                                     CAST(u.fecha_picking AS DATE) AS fecha_picking,
                                     MIN(u.fecha_picking) OVER (PARTITION BY u.IdPickingEnc) AS Inicio_Picking,
                                     MAX(u.fecha_picking) OVER (PARTITION BY u.IdPickingEnc) AS Fin_Picking
                                   FROM [dbo].[trans_picking_enc] p INNER JOIN 
                                     [dbo].[trans_picking_ubic] u ON p.IdPickingEnc = u.IdPickingEnc
                                   WHERE 
                                     u.fecha_picking IS NOT NULL AND cantidad_recibida > 0 AND u.activo = 1 AND encontrado = 1 
                                     AND p.estado <> 'Anulado'
                                  ) AS SubConsulta
                                  INNER JOIN trans_pe_enc on SubConsulta.IdPedidoEnc = trans_pe_enc.IdPedidoEnc
                                  AND SubConsulta.IdBodega = trans_pe_enc.IdBodega
                                  INNER JOIN cliente ON trans_pe_enc.IdCliente = cliente.IdCliente
                                WHERE CAST(fecha_picking AS DATE) BETWEEN @FECHA_DESDE AND @FECHA_HASTA
                                GROUP BY 
                                  CAST(fecha_picking AS DATE), subconsulta.IdPedidoEnc,cliente.nombre_comercial
                                ORDER BY 
                                  Fecha;
                                "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FECHA_DESDE", FechaDesde)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FECHA_HASTA", FechaHasta)
                        lDataAdapter.SelectCommand.CommandTimeout = 100
                        lDataAdapter.Fill(lTable)
                        Get_Reporte_Detalle_Pedidos_By_Fechas = lTable
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

    Public Shared Function Actualizar_IdStock(ByRef oBeTrans_picking_ubic As clsBeTrans_picking_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_picking_ubic")
            Upd.Add("IdStock", "@IdStock", DataType.Parametro)
            Upd.Add("IdStockres", "@IdStockres", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic")

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

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGUBIC", oBeTrans_picking_ubic.IdPickingUbic))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_picking_ubic.IdStock))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKRES", oBeTrans_picking_ubic.IdStockRes))

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

    '#AT20250203 Get Picking detallado para Packing
    Public Shared Function Get_All_PickingUbic_For_Packing(ByVal pPackingEnc As clsBeTrans_packing_enc,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try

            Dim vSQL As String = "SELECT * FROM trans_picking_ubic
                                  WHERE dañado_picking = 0 AND
                                  dañado_verificacion = 0 AND 
                                  no_encontrado = 0 AND 
							      cantidad_verificada > 0 AND
                                  IdPickingEnc=@IdPickingEnc AND
                                  IdUnidadMedida=@IdUnidadMedida AND
                                  lic_plate=@lic_plate AND 
                                  ISNULL(IdPresentacion,0) = @IdPresentacion AND
                                  (Lote = @Lote OR Lote IS NULL)  AND 
                                  ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                  IdProductoEstado = @IdProductoEstado AND 
                                  IdProductoBodega = @IdProductoBodega"


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdPickingEnc", pPackingEnc.Idpickingenc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pPackingEnc.Idpresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pPackingEnc.Idunidadmedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", pPackingEnc.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pPackingEnc.Fecha_vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pPackingEnc.Lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pPackingEnc.Idproductoestado)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pPackingEnc.Idproductobodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

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

    Public Shared Function CantidadRegistros(ByVal pIdPickingEnc As Integer,
                                             ByVal pDetalleOperador As Boolean,
                                             ByVal pIdOperadorBodega As Integer,
                                             ByVal pConnection As SqlConnection,
                                             ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lCount As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Count(IdPickingUbic),0) 
                                  FROM trans_picking_ubic 
                                  WHERE IdPickingEnc = @IdPickingEnc 
                                        AND dañado_picking = 0 
                                        AND cantidad_solicitada <> cantidad_recibida 
                                        AND no_encontrado = 0"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                lCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pDetalleOperador Then
                    lCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lCount = CInt(lReturnValue)
                End If

            End Using

            Return lCount

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_Consolidado_Reducido(ByVal pIdPickingEnc As Integer,
                                                                                    ByVal pDetalleOperador As Boolean,
                                                                                    ByVal pIdOperadorBodega As Integer,
                                                                                    ByRef lConnection As SqlConnection,
                                                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim lReturnListCons As New List(Of clsBeTrans_picking_ubic)
        Dim countReg As Integer = 0
        Dim vSQL As String = ""

        Try

            '#CKFK20250311 Agregue el dañado_verificacion = 0
            vSQL = "SELECT  * FROM VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado 
		                WHERE IdPickingEnc = @IdPickingEnc 
                        AND dañado_picking = 0 
                        AND cantidad_solicitada <> cantidad_recibida 
                        AND no_encontrado = 0 AND dañado_verificacion = 0 "
            'End If

            If pDetalleOperador Then
                vSQL += " AND IdOperadorBodega_Pickeo = @IdOperadorBodega"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))

                If pDetalleOperador Then
                    lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdOperadorBodega", pIdOperadorBodega))
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        'Obj.Ubicacion.Nivel = lRow.Item("nivel")

                        With Obj
                            .Ubicacion = New clsBeBodega_ubicacion
                            .Ubicacion.Nivel = lRow.Item("nivel")
                            .IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), "", lRow.Item("NombreUbicacion"))
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
                            .IdPickingEnc = pIdPickingEnc
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

    Public Shared Function Get_All_PickingUbic_By_IdPicking_Pedido_Producto(ByVal pIdPickingEnc As Integer,
                                                                            ByVal pIdBodega As Integer,
                                                                            ByVal pIdPedidoEnc As Integer,
                                                                            ByVal pCodigoProducto As String,
                                                                            ByRef lConnection As SqlConnection,
                                                                            ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)
        Dim BeBodega As New clsBeBodega

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_By_IdPickingEnc
                                 WHERE (IdPickingEnc = @IdPickingEnc AND 
                                        IdPedidoEnc = @IdPedidoEnc AND  
                                        codigo_producto = @Codigo_Producto AND  
                                        dañado_picking = 0 AND  
                                        no_encontrado = 0 AND  
                                        dañado_verificacion = 0) "

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, lConnection, lTransaction)

            If Not BeBodega Is Nothing Then
                '#CKFK20221205 Agregué el ordenamiento por tramo
                If BeBodega.Ordenar_Por_Nombre_Completo Then
                    vSQL += " ORDER BY NombreUbicacion "
                    If BeBodega.Ordenar_Picking_Descendente Then
                        vSQL += " desc "
                    End If
                Else
                    vSQL += " ORDER BY IdPedidoEnc "
                End If
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@Codigo_Producto", pCodigoProducto))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTransPickingUbic As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTransPickingUbic = New clsBeTrans_picking_ubic()

                        Cargar(BeTransPickingUbic, lRow)

                        BeTransPickingUbic.Ubicacion.IdBodega = lRow.Item("IdBodega")
                        BeTransPickingUbic.Ubicacion.IdUbicacion = lRow.Item("IdUbicacion")

                        With BeTransPickingUbic

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("NombreUbicacion")), 0, lRow.Item("NombreUbicacion")) 'Obj.Ubicacion.Descripcion
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
                            .IdPickingEnc = pIdPickingEnc '#CKFK 20180109 09:33 AM Agregué este campo para que se llene al llamar a la función
                            .IsNew = False
                            .No_encontrado = IIf(IsDBNull(lRow.Item("no_encontrado")), False, lRow.Item("no_encontrado"))
                            '#EJC20220304: Debe venir en la vista, join de producto - Clasificación.
                            .NombreClasificacion = IIf(IsDBNull(lRow.Item("Clasificacion")), "", lRow.Item("Clasificacion"))

                        End With

                        lReturnList.Add(BeTransPickingUbic)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc(ByVal pIdPickingEnc As Integer,
                                                                               ByVal pIdPedidoEnc As Integer,
                                                                               ByVal pConnection As SqlConnection,
                                                                               ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim lReturnList As New List(Of clsBeTrans_picking_ubic)

        Try
            '#EJC20250724: Quité Join por ubicación en Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc
            '#CKFK 20180402 06:42 PM Agregué esta condición  " and pu.IdStockRes = sr.IdStockRes " para evitar la duplicidad de registros
            '#CKFK 20210714 1337 Agregué la función dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion,penc.IdBodega) AS nom_ubicacion por error en la relacion
            Dim vSQL As String = "SELECT pu.*,pdet.IdPedidoEnc,pdet.IdPedidoDet,pdet.IdPresentacion, pdet.IdUnidadMedidaBasica, 
                                  pdet.IdProductoBodega, 
                                  pdet.codigo_producto, pdet.nombre_producto,pdet.nom_presentacion, pdet.nom_unid_med,pdet.nom_estado,
                                  pdet.IdEstado, pdet.Peso, pdet.Precio, sr.IdStockRes, sr.IdStock,
	                              dbo.Nombre_Completo_Ubicacion(pu.IdUbicacion,penc.IdBodega) AS nom_ubicacion, pu.No_Linea
                                  FROM trans_picking_ubic pu  
                                      INNER JOIN trans_picking_det AS pkdet ON pkdet.IdPickingDet = pu.IdPickingDet 
                                      INNER JOIN trans_pe_det As pdet On pdet.IdPedidoDet = pkdet.IdPedidoDet  
                                      INNER JOIN trans_pe_enc As penc On pdet.IdPedidoEnc = penc.IdPedidoEnc 
                                      INNER JOIN stock_res sr ON pkdet.IdPedidoDet = sr.IdPedidoDet AND pu.IdUbicacion = sr.IdUbicacion  
                                                and pu.IdStockRes = sr.IdStockRes  and sr.IdBodega = pu.IdBodega   
                                  WHERE pkdet.IdPickingEnc = @IdPickingEnc And penc.IdPedidoEnc = @IdPedidoEnc 
                                       and pu.dañado_verificacion=0 and pu.dañado_picking=0"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", pIdPickingEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            'clsLnBodega_ubicacion.Obtener(.Ubicacion)
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("nom_ubicacion")), 0, lRow.Item("nom_ubicacion")) '.Ubicacion.NombreCompleto
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

    Public Shared Function Actualizar_IdStockRes(ByRef oBePickingUbic As clsBeTrans_picking_ubic,
                                                 Optional ByVal pConection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try
            Upd.Init("trans_picking_ubic")
            Upd.Add("IdStockRes", "@IdStockRes", DataType.Parametro)
            Upd.Where("IdPickingUbic = @IdPickingUbic")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdStockRes", oBePickingUbic.IdStockRes))
            cmd.Parameters.Add(New SqlParameter("@IdPickingUbic", oBePickingUbic.IdPickingUbic))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
                If lConnection IsNot Nothing Then lConnection.Dispose()
            End If
        End Try

    End Function

    Public Shared Function Get_Single_By_IdStockRes_And_IdPickingEnc(ByVal IdStockRes As Integer,
                                                                     ByVal IdPickingEnc As Integer,
                                                                     ByVal IdBodega As Integer,
                                                                     Optional pConnection As SqlConnection = Nothing,
                                                                     Optional pTransaction As SqlTransaction = Nothing) As clsBeTrans_picking_ubic

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Get_Single_By_IdStockRes_And_IdPickingEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_picking_ubic Where(IdStockRes = @IdStockRes AND IdBodega = @IdBodega AND IdPickingEnc = @IdPickingEnc)
                                  AND dañado_picking = 0 AND dañado_verificacion = 0 AND no_encontrado = 0 "

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            cmd = New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
                With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStockRes", IdStockRes))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPickingEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim vBeTrans_picking_ubic As New clsBeTrans_picking_ubic()
                Cargar(vBeTrans_picking_ubic, dt.Rows(0))
                Get_Single_By_IdStockRes_And_IdPickingEnc = vBeTrans_picking_ubic
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_Operador_Defecto_By_IdPickingEnc(ByVal IdPickingEnc As Integer,
                                                                Optional pConnection As SqlConnection = Nothing,
                                                                Optional pTransaction As SqlTransaction = Nothing) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Get_Operador_Defecto_By_IdPickingEnc = ""

        Try

            Const sp As String = "select top(1) concat(op.nombres, ' ', op.apellidos) as Operador from trans_picking_ubic pu
                                  join operador_bodega ob on pu.IdOperadorBodega_Pickeo = ob.IdOperadorBodega
                                  join operador op on ob.IdOperador = op.IdOperador Where(IdPickingEnc = @IdPickingEnc) "

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            cmd = New SqlCommand(sp, IIf(Es_Transaccion_Remota, pConnection, lConnection), IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
                With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPickingEnc", IdPickingEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Operador_Defecto_By_IdPickingEnc = IIf(IsDBNull(dt.Rows(0).Item("Operador")), "", dt.Rows(0).Item("Operador"))
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_PickingUbic_Despachado_By_IdDespachoEnc(ByVal pIdDespachoEnc As Integer,
                                                                           ByVal lConnection As SqlConnection,
                                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_Despachado_By_IdDespachoEnc = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_PickingUbic_Desp_By_IdDespachoEnc
                                  WHERE IdDespachoEnc = @IdDespachoEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdDespachoEnc", pIdDespachoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar_For_Despacho(Obj, lRow)

                        With Obj
                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
                            .NombreUbicacion = IIf(IsDBNull(lRow.Item("Nombre_Ubicacion")), 0, lRow.Item("Nombre_Ubicacion"))
                            .IdPedidoDet = IIf(IsDBNull(lRow.Item("IdPedidoDet")), 0, lRow.Item("IdPedidoDet"))
                            .CodigoProducto = IIf(IsDBNull(lRow.Item("codigo")), "", lRow.Item("codigo"))
                            .NombreProducto = IIf(IsDBNull(lRow.Item("nombre")), "", lRow.Item("nombre"))
                            If lDataTable.Columns.Contains("Presentacion") Then
                                .ProductoPresentacion = IIf(IsDBNull(lRow.Item("Presentacion")), "", lRow.Item("Presentacion"))
                            End If
                            If lDataTable.Columns.Contains("UnidadMedida") Then
                                .ProductoUnidadMedida = IIf(IsDBNull(lRow.Item("UnidadMedida")), "", lRow.Item("UnidadMedida"))
                            End If
                            If lDataTable.Columns.Contains("NomEstado") Then
                                .ProductoEstado = IIf(IsDBNull(lRow.Item("NomEstado")), "", lRow.Item("NomEstado"))
                            End If

                            .IdProductoBodega = IIf(IsDBNull(lRow.Item("IdProductoBodega")), 0, lRow.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                            .IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                            .IdStock = IIf(IsDBNull(lRow.Item("IdStock")), 0, lRow.Item("IdStock"))
                            .IdPedidoEnc = IIf(IsDBNull(lRow.Item("IdPedidoEnc")), 0, lRow.Item("IdPedidoEnc"))
                            .Codigo_Talla = IIf(IsDBNull(lRow.Item("Talla")), 0, lRow.Item("Talla"))
                            .Codigo_Color = IIf(IsDBNull(lRow.Item("Color")), 0, lRow.Item("Color"))
                            .No_Linea = IIf(IsDBNull(lRow.Item("No_Linea")), 0, lRow.Item("No_Linea"))
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

    Public Shared Function Get_All_PickingUbic_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pConnection As SqlConnection,
                                                              ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPedidoEnc = Nothing

        Dim lReturnList As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BeBodega As New clsBeBodega

        Try

            Dim vSQL As String = " SELECT * FROM VW_PickingUbic_By_IdPedidoDet 
                                   WHERE IdPedidoEnc = @IdPedidoEnc AND  
                                         dañado_picking = 0 AND dañado_verificacion = 0 AND no_encontrado = 0 "

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, pConnection, pTransaction)

            If Not BeBodega Is Nothing Then
                '#CKFK20221205 Agregué el ordenamiento por tramo
                If BeBodega.Ordenar_Por_Nombre_Completo Then
                    vSQL += " ORDER BY Nombre_Ubicacion "
                    If BeBodega.Ordenar_Picking_Descendente Then
                        vSQL += " desc "
                    End If
                Else
                    vSQL += " ORDER BY IdPedidoEnc "
                End If
            End If

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", pIdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeTrans_picking_ubic

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lReturnList = New List(Of clsBeTrans_picking_ubic)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeTrans_picking_ubic

                        Cargar(Obj, lRow)

                        With Obj

                            .Ubicacion.IdUbicacion = IIf(IsDBNull(lRow.Item("IdUbicacion")), 0, lRow.Item("IdUbicacion"))
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

    '#GT28112025: metodo sobrecargado para recibir transaccion remota y no tocar el proceso de verificacion por HH
    Public Shared Function Actualiza_Cant_Peso_Verificacion(ByVal pBePickingUbicList As List(Of clsBeTrans_picking_ubic),
                                                            ByVal pIdOperador As Integer,
                                                            ByRef pCantidad As Double,
                                                            ByRef pPeso As Double,
                                                            ByVal pTipo As Integer,
                                                            ByVal pIdPedidoEnc As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As Boolean

        Dim CantPendiente As Double
        Dim PesoPendiente As Double
        Dim BeStockRes As New clsBeStock_res
        Dim resultado As String = ""
        Dim tmpBeListPickingUbic As List(Of clsBeTrans_picking_ubic) = Nothing
        Dim BePickingUbic As New clsBeTrans_picking_ubic
        Actualiza_Cant_Peso_Verificacion = False

        Try

            BePickingUbic = pBePickingUbicList(0)
            pBePickingUbicList = Nothing

            tmpBeListPickingUbic = Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(BePickingUbic.IdPickingEnc,
                                                                                        False,
                                                                                        0,
                                                                                        pIdPedidoEnc,
                                                                                        lConnection,
                                                                                        lTransaction)

            '#AT20220509 Si pTipo <> 0 No aplica buscar por Lote y Fecha Vencimiento
            If pTipo <> 0 Then
                pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.IdPresentacion = BePickingUbic.IdPresentacion And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()
            Else
                pBePickingUbicList = tmpBeListPickingUbic.Where(Function(x) x.CodigoProducto = BePickingUbic.CodigoProducto And x.Lote = BePickingUbic.Lote And x.Fecha_Vence = BePickingUbic.Fecha_Vence And x.Lic_plate = BePickingUbic.Lic_plate And ((x.Cantidad_Recibida - x.Cantidad_Verificada) <> 0.0)).ToList()

            End If

            For Each vBePickingUbic As clsBeTrans_picking_ubic In pBePickingUbicList

                If Math.Round(vBePickingUbic.Cantidad_Verificada + pCantidad, 6) > vBePickingUbic.Cantidad_Recibida Then
                    CantPendiente = vBePickingUbic.Cantidad_Recibida - vBePickingUbic.Cantidad_Verificada
                Else
                    CantPendiente = pCantidad
                End If


                If ((vBePickingUbic.Peso_verificado + pPeso) > vBePickingUbic.Peso_recibido) Then
                    PesoPendiente = vBePickingUbic.Peso_recibido - vBePickingUbic.Peso_verificado
                Else
                    PesoPendiente = pPeso
                End If

                vBePickingUbic.Cantidad_Verificada += CantPendiente
                vBePickingUbic.Cantidad_Verificada = Math.Round(vBePickingUbic.Cantidad_Verificada, 6)

                vBePickingUbic.Peso_verificado += PesoPendiente
                vBePickingUbic.Peso_verificado = Math.Round(vBePickingUbic.Peso_verificado, 6)

                vBePickingUbic.IdOperadorBodega_Verifico = pIdOperador
                vBePickingUbic.Fecha_verificado = Now

                BeStockRes.IdStockRes = vBePickingUbic.IdStockRes
                BeStockRes.IdProductoBodega = vBePickingUbic.IdProductoBodega

                clsLnStock_res.GetSingle(BeStockRes,
                                         lConnection,
                                         lTransaction)

                BeStockRes.Estado = "VERIFICADO"
                BeStockRes.User_mod = pIdOperador
                BeStockRes.Fec_mod = Now

                '#GT25042023: cantidad y peso son opcionales, porque el método se llama desde otro lugar, donde no son necesarios dichos valores.
                resultado += Actualizar_PickingUbic_Por_Verificacion(vBePickingUbic,
                                                                     BeStockRes,
                                                                     pCantidad,
                                                                     pPeso,
                                                                     lConnection,
                                                                     lTransaction)


                If (Math.Round(pCantidad - CantPendiente, 6) = 0) Then
                    Exit For
                Else
                    pCantidad -= CantPendiente
                    pCantidad = Math.Round(pCantidad, 6)
                End If

            Next

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        End Try

    End Function

End Class