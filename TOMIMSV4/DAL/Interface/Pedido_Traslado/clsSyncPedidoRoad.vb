Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsSyncPedidoRoad

    Public Shared Function Procesar_Pedidos_Road(ByVal pIdPropietario As Integer,
                                                 ByVal pCodigoBodegaOrigen As String,
                                                 ByVal pIdEmpresa As Integer,
                                                 ByVal pIdBodega As Integer,
                                                 ByVal lblprg As RichTextBox,
                                                 ByRef prg As Windows.Forms.ProgressBar) As Boolean
        Procesar_Pedidos_Road = False

        Dim Resultado As String = ""

        Dim cnnLog As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

        Try

            cnnLog.Open()

            Dim lPedidosTraslado As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTraslado = Get_Pedidos_Road(pIdPropietario, pCodigoBodegaOrigen)

            Dim BeClienteWMS As New clsBeCliente

            Dim BePedidoRoad As New clsBeD_PEDIDO

            For Each PC In lPedidosTraslado

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Cliente: {0}/{1}{2}", PC.No, PC.Receipt_Document_Reference, vbNewLine))

                BeClienteWMS = clsLnCliente.Existe(PC.Transfer_to_Code)

                If BeClienteWMS Is Nothing Then
                    Inserta_Cliente_Road(PC.Transfer_to_Code, pIdPropietario, pIdEmpresa, pIdBodega)
                    clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PC.Transfer_to_Code & " No existía en WMS y fue insertado.")
                End If

                Dim BePedidoEnc As New clsBeTrans_pe_enc
                BePedidoEnc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(PC, lblprg)

                If Not BePedidoEnc Is Nothing Then

                    BePedidoRoad.COREL = PC.No
                    clsLnD_PEDIDO.GetSingle(BePedidoRoad)

                    BePedidoRoad.STATCOM = "P"
                    clsLnD_PEDIDO.Actualizar_Estado(BePedidoRoad)

                End If

                clsPublic.Actualizar_Progreso(lblprg, Resultado)

            Next

            Procesar_Pedidos_Road = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                           "",
                                                           0,
                                                           0)

            Throw ex

        Finally
            If cnnLog.State = ConnectionState.Open Then cnnLog.Close()
        End Try

    End Function

    Private Shared Function Get_Pedidos_Road(ByVal pIdPropietario As Integer,
                                             ByVal pCodigoBodegaOrigen As String) As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Pedidos_Road = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios


        Try

            Dim lPedidosRoad As New List(Of clsBeD_PEDIDO)

            lPedidosRoad = clsLnD_PEDIDO.Get_All_Pendientes_Procesar_WMS()

            Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()
            Dim BeProductoRoad As New clsBeP_PRODUCTO

            For Each P In lPedidosRoad

                If (P.IMPRES <> 0) Then
                    BePropietario = clsLnPropietarios.GetSingle(P.IMPRES)
                Else
                    BePropietario = clsLnPropietarios.GetSingle(pIdPropietario)
                End If

                BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
                BePedidoWMS.No = P.COREL
                BePedidoWMS.Posting_Date = Now
                BePedidoWMS.Receipt_Date = Now
                BePedidoWMS.Shipment_Date = Now
                BePedidoWMS.Status = 1
                BePedidoWMS.Transfer_from_Code = pCodigoBodegaOrigen
                BePedidoWMS.Transfer_from_Contact = P.VENDEDOR
                BePedidoWMS.Transfer_from_Name = "MI3_NAME"
                BePedidoWMS.Transfer_to_Code = P.CLIENTE
                BePedidoWMS.Transfer_to_Contact = P.VENDEDOR
                BePedidoWMS.Transfer_to_CodeField = P.RUTA
                BePedidoWMS.Transfer_to_Name = P.SUCURSAL
                BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
                BePedidoWMS.Receipt_Document_Reference = P.ADD3
                BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                For Each D In P.Detalle

                    BeProductoRoad = clsLnP_PRODUCTO.Get_Producto_By_Codigo(D.PRODUCTO)
                    BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()
                    BePedidoDetWMS.NoEnc = BePedidoWMS.No
                    BePedidoDetWMS.No = clsLnTrans_pe_det.MaxID() + 1
                    BePedidoDetWMS.Item_No = D.PRODUCTO
                    BePedidoDetWMS.Description = BeProductoRoad.DESCCORTA
                    BePedidoDetWMS.Line_No = NoLinea
                    BePedidoDetWMS.Shipment_Date = Date.Now
                    BePedidoDetWMS.Quantity = D.CANT
                    BePedidoDetWMS.Unit_of_Measure_Code = D.UMVENTA
                    BePedidoDetWMS.Status = 1
                    BePedidoDetWMS.Variant_Code = Nothing
                    BePedidoDetWMS.Transfer_to_CodeField = pCodigoBodegaOrigen
                    BePedidoDetWMS.Price = D.PRECIO
                    BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

                Next

                lPedidosCliente.Add(BePedidoWMS)

            Next

            Return lPedidosCliente

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Inserta_Linea_Detalle_Pedido(ByVal pIdPedidoEnc As Integer,
                                                  ByVal PDet As clsBeI_nav_ped_traslado_det,
                                                  ByVal BeProducto As clsBeProducto,
                                                  ByVal vDiasVencimientoCliente As Integer,
                                                  ByVal BeUnidadMedida As clsBeUnidad_medida,
                                                  ByVal IdUsuario As Integer,
                                                  ByVal IdBodega As Integer,
                                                  ByVal IdPropietario As Integer,
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef lConnectionInterface As SqlConnection,
                                                  ByRef CnnLog As SqlConnection,
                                                  ByRef lTransInterface As SqlTransaction) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim BeNavConfigEnc As New clsBeI_nav_config_enc

        Try

            '#EJC2023020201147
            'BeNavConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
            '                                                 lConnectionInterface,
            '                                                 lTransInterface)

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = clsLnTrans_pe_det.MaxID(lConnectionInterface, lTransInterface) + 1
            pBePedidoDet.No_linea = PDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = PDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = pIdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeProducto.IdProductoBodega, lConnectionInterface, lTransInterface)
            pBePedidoDet.Producto.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.Producto.Codigo = PDet.Item_No
            pBePedidoDet.IdPresentacion = 0
            pBePedidoDet.IdUnidadMedidaBasica = BeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = PDet.Quantity
            pBePedidoDet.Peso = 0 'PDet.Quantity
            pBePedidoDet.Precio = 0
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = 1 'pBeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = 0
            pBePedidoDet.RoadTotal = 0
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Codigo_Producto = PDet.No
            pBePedidoDet.Nombre_producto = PDet.Description
            pBePedidoDet.Nom_presentacion = ""
            pBePedidoDet.Nom_unid_med = PDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = PDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBeStockRes.User_agr = IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = "Interface"
            pBeStockRes.IdPresentacion = 0 'De momemento.
            pBeStockRes.IdProductoEstado = 1 'Por defecto.
            pBeStockRes.IdPedido = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = BeProducto.IdProductoBodega
            pBeStockRes.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(IdBodega, IdPropietario, lConnectionInterface, lTransInterface)
            pBeStockRes.IdUnidadMedida = BeUnidadMedida.IdUnidadMedida
            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.Atributo_Variante_1 <> "" Then

                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(pBePedidoDet.Producto.IdProducto,
                                                                                            pBePedidoDet.Atributo_Variante_1,
                                                                                            lConnectionInterface,
                                                                                            lTransInterface)

                If Not BePresentacion Is Nothing Then
                    pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                Else
                    pBeStockRes.IdPresentacion = -1 'No se encontró la presentación solicitada
                End If

            End If

            Try

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(vDiasVencimientoCliente,
                                                                            PDet,
                                                                            pBePedidoDet,
                                                                            pBeStockRes,
                                                                            "Interface",
                                                                            BeNavConfigEnc,
                                                                            IdPropietario,
                                                                            lConnectionInterface,
                                                                            lTransInterface) Then
                    Inserta_Linea_Detalle_Pedido = True
                End If

            Catch ex As Exception

                'clsLnTrans_pe_det.Eliminar(pBePedidoDet,CnnInterface,lTransInterface)

                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error en 
                                                            Reservar_Stock_Por_Linea 
                                                            para el pedido: {0} 
                                                            línea: {1} 
                                                            Código_Producto: {3}
                                                            U.M.: {4}
                                                            V.C.: {5}
                                                            Descripción del error: {2} ", PDet.NoEnc,
                                                                             PDet.Line_No,
                                                                             ex.Message,
                                                                             PDet.Item_No,
                                                                             PDet.Unit_of_Measure_Code,
                                                                             PDet.Variant_Code),
                                                            PDet.No,
                                                            0,
                                                            0)

                lblprg.AppendText(String.Format("Error en
                                                Reservar_Stock_Por_Linea 
                                                para el pedido: {0} 
                                                línea: {1} 
                                                Código_Producto: {4}
                                                U.M.: {5}
                                                V.C.: {6}
                                                Descripción del error: {2}{3} ",
                                                    PDet.NoEnc,
                                                    PDet.Line_No,
                                                    ex.Message,
                                                    vbNewLine,
                                                    PDet.Item_No,
                                                    PDet.Unit_of_Measure_Code,
                                                    PDet.Variant_Code))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End Try

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Inserta_Cliente_Road(ByVal pCodigo As String,
                                          ByVal pIdPropietario As Integer,
                                          ByVal pIdEmpresa As Integer,
                                          ByVal pIdBodega As Integer) As Boolean

        Inserta_Cliente_Road = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim BeClienteWMS As New clsBeCliente
        Dim BeClienteBodega As New clsBeCliente_bodega

        Try

            Dim BeClienteRoad As New clsBeP_CLIENTE

            BeClienteRoad = clsLnP_CLIENTE.Get_Single_By_Codigo(pCodigo)

            BeClienteWMS.IdCliente = clsLnCliente.MaxID() + 1
            BeClienteWMS.IdPropietario = pIdPropietario
            BeClienteWMS.Codigo = BeClienteRoad.CODIGO
            BeClienteWMS.Nombre_comercial = BeClienteRoad.NOMBRE
            BeClienteWMS.Sistema = True
            BeClienteWMS.Activo = True
            BeClienteWMS.IdEmpresa = pIdEmpresa
            BeClienteWMS.Nit = BeClienteRoad.NIT
            BeClienteWMS.IdTipoCliente = 1
            BeClienteWMS.Es_bodega_recepcion = False
            BeClienteWMS.Es_Bodega_Traslado = False

            clsLnCliente.Insertar(BeClienteWMS)

            BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID() + 1
            BeClienteBodega.IdBodega = pIdBodega
            BeClienteBodega.IdCliente = BeClienteWMS.IdCliente
            BeClienteBodega.User_agr = "Mi3"
            BeClienteBodega.User_mod = "Mi3"
            BeClienteBodega.Activo = True
            BeClienteBodega.Fec_agr = Now
            BeClienteBodega.Fec_mod = Now

            clsLnCliente_bodega.Insertar(BeClienteBodega)

            Inserta_Cliente_Road = True

        Catch ex As Exception
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        End Try

    End Function


End Class
