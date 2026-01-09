Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ServiceModel
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar
Imports DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering
Imports DevExpress.Office.Services
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI
Imports TOMWMS.SapHelper

Public Class clsSyncSAPSSolicitudTraslado : Inherits clsInterfaceBase
    Public Shared Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar,
                                                     ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            lTransaccionesSalida.ForEach(Sub(t)
                                             If t.No_pedido.StartsWith("K") OrElse t.No_pedido.StartsWith("G") Then
                                                 t.No_pedido = t.No_pedido.Substring(1)
                                             End If
                                         End Sub)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                Dim cantidadPedidosDiferentes As Integer = ListaPedidosTransf.Select(Function(x) x.Idpedidoenc).Distinct().Count()

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documentos a enviar: {0}", cantidadPedidosDiferentes))

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc)

                    clsPublic.Actualizar_Progreso(lblprg, "Enviando documento: " & PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        Dim vEmpresaStr As String = clsLnTrans_pe_enc.Get_Company_SAP_By_IdPedidoEnc(PT.Idpedidoenc)
                        Dim vEmpresa As pEmpresa = [Enum].Parse(GetType(pEmpresa), vEmpresaStr)
                        Dim vNoPedido As Integer = 0

                        If PT.No_pedido.StartsWith("K") OrElse PT.No_pedido.StartsWith("G") Then
                            vNoPedido = Val(PT.No_pedido.Substring(1))
                        Else
                            vNoPedido = PT.No_pedido
                        End If

                        If Enviar_Transferencia_Stock_SAP(vNoPedido, lTransaccionesSalidaSingle, vEmpresa, lblprg, prg) Then

                            Try

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", vNoPedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", vNoPedido, ex.Message),
                                                                          vNoPedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "El Documento: " & PT.No_pedido & " ya se encuentra enviado, pero sus registros de interface no, verifique estado de registros.")
                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("ERR_TR_PRO_20250608: " & ex.Message)
            Throw
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            clsPublic.Actualizar_Progreso(lblprg, "Fin del proceso de sincronización: " & Now)
        End Try

    End Sub
    Public Shared Function Enviar_Traslado_Entre_Almacenes_SAP(ByVal _Docentry As Integer,
                                                               ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                               ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                               ByRef lblprg As RichTextBox,
                                                               ByRef prg As ProgressBar,
                                                               ByVal vEmpresa As pEmpresa,
                                                               ByVal oCompany As Company,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = 0
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0
        Dim NoLineaEntregaLote As Integer = 0
        Dim CodigoPresentacion As String = ""
        Dim vFactor As Double = 1

        Try

            Dim oTransfer As StockTransfer
            Dim oTransferRequest As StockTransfer
            Dim BaseLine As Integer = 0
            Dim BeProducto As New clsBeProducto()
            Dim vCodigoProductoSAP As String = ""
            Dim vCodigoProductoWMS As String = ""
            Dim BePresentacion As New clsBeProducto_Presentacion
            Dim ProductosParaSAP As New List(Of ProductoTransferSAP)
            Dim vUnidadesEncontradas As Boolean = False

            oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
            oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

#Region "Agrupar por ID del pedido y No_Pedido, y recoger los productos"

            Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.GroupBy(Function(x) New With {Key x.Idordencompra, Key x.No_pedido, Key x.No_linea}).
                    Select(Function(g) New With {
                        .Idordencompra = g.Key.Idordencompra,
                        .No_Pedido = g.Key.No_pedido,
                        .Productos = g.Select(Function(p) New With {
                            .Codigo_Producto = p.Codigo_producto,
                            .Cantidad_Total = p.Cantidad,
                            .IdPresentacion = p.Idpresentacion,
                            .Idordencompra = p.Idordencompra,
                            .No_Linea = g.Key.No_linea
                        }).ToList()
                    }).ToList()

            If DistinctIdPedidoEncByTraslado.Any() Then

                For Each Pedido In DistinctIdPedidoEncByTraslado

                    For Each producto In Pedido.Productos

                        vCodigoProductoSAP = producto.Codigo_Producto

                        BeProducto = clsLnProducto.Get_Single_By_Codigo(vCodigoProductoSAP,
                                                                        lConnection,
                                                                        lTransaction)

                        If Not BeProducto Is Nothing Then

                            Select Case vEmpresa
                                Case pEmpresa.Killios
                                    vCodigoProductoSAP = BeProducto.Noparte
                                Case pEmpresa.Garesa
                                    vCodigoProductoSAP = BeProducto.Noserie
                            End Select

                            '#CKFK20251016 ´Modificamos esta funcion para enviar correctamente las unidades a SAP,
                            'cuando se envían decimales en el documento de ingreso y en WMS se reciben unidades
                            Dim info As UoMInfo = Nothing

                            Dim ok As Boolean = GetUoMFromTransferRequest(oCompany, _Docentry, vCodigoProductoSAP, producto.No_Linea, info)

                            Dim IdPresentacionOC As Integer = 0

                            If ok Then
                                IdPresentacionOC = clsLnProducto_presentacion.Get_IdPresentacion_By_Codigo(info.UoMCode,
                                                                                                           BeProducto.IdProducto,
                                                                                                           lConnection,
                                                                                                           lTransaction)
                            Else
                                Console.WriteLine("No se encontró la línea solicitada.")
                            End If

                            Dim vIdPresentacion As Integer = producto.IdPresentacion

                            If IdPresentacionOC <> producto.IdPresentacion Then

                                ' Calcular cantidad ajustada para esta transacción
                                Dim ProductoAgrupado As New clsBeI_nav_transacciones_out_agrupado
                                ProductoAgrupado.Idpedidoenc = 0
                                ProductoAgrupado.Codigo_producto = producto.Codigo_Producto
                                ProductoAgrupado.No_linea = producto.No_Linea
                                ProductoAgrupado.IdPresentacion = producto.IdPresentacion
                                ProductoAgrupado.IdDespachoDet = 0
                                ProductoAgrupado.Cantidad_Total = producto.Cantidad_Total

                                Dim cantidadAjustada As Decimal
                                cantidadAjustada = AjustarCantidadPorPresentacion(ProductoAgrupado,
                                                                                  IdPresentacionOC,
                                                                                  info.UoMEntry,
                                                                                  info.UoMCode,
                                                                                  lConnection,
                                                                                  lTransaction)
                                producto.Cantidad_Total = cantidadAjustada
                                vIdPresentacion = IdPresentacionOC

                            End If

                            BePresentacion = Nothing
                            CodigoPresentacion = ""

                            BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacionOC,
                                                                                  lConnection,
                                                                                  lTransaction)

                            If Not BePresentacion Is Nothing Then
                                CodigoPresentacion = BePresentacion.Codigo
                                vFactor = BePresentacion.Factor
                                vUnidadesEncontradas = False 
                            End If

                            '#CKFK20251226 Agregué al linq el número de línea 
                            Dim existente = ProductosParaSAP.FirstOrDefault(Function(p) p.IdPedidoEnc = producto.Idordencompra AndAlso
                                                                                     p.CodigoProductoSAP = vCodigoProductoSAP AndAlso
                                                                                     p.CodigoPresentacion = CodigoPresentacion AndAlso
                                                                                     p.No_Linea = producto.No_Linea)

                            If vIdPresentacion = 0 Then

                                '#CKFK20251226 Agregué al linq el número de línea 
                                Dim existente1 = ProductosParaSAP.FirstOrDefault(Function(p) p.IdPedidoEnc = producto.Idordencompra AndAlso
                                                                                             p.CodigoProductoSAP = vCodigoProductoSAP AndAlso
                                                                                             p.CodigoPresentacion <> CodigoPresentacion AndAlso
                                                                                             p.No_Linea = producto.No_Linea)

                                If existente1 IsNot Nothing Then
                                    existente1.Cantidad_Total += Math.Round(producto.Cantidad_Total / existente1.Factor, 4)
                                    vUnidadesEncontradas = True
                                End If

                            End If

                            If existente IsNot Nothing Then
                                ' Sumar cantidad                                
                                existente.Cantidad_Total += producto.Cantidad_Total
                            ElseIf Not vUnidadesEncontradas Then
                                ' Agregar nuevo
                                Dim prodSap As New ProductoTransferSAP With {
                                                .IdPedidoEnc = producto.Idordencompra,
                                                .CodigoProductoSAP = vCodigoProductoSAP,
                                                .CodigoProductoWMS = BeProducto.Codigo,
                                                .Cantidad_Total = producto.Cantidad_Total,
                                                .CodigoPresentacion = CodigoPresentacion,
                                                .No_Pedido = Pedido.No_Pedido,
                                                .No_Linea = producto.No_Linea,
                                                .Factor = vFactor}

                                ProductosParaSAP.Add(prodSap)
                            End If

                        End If

                    Next

                Next

            End If

#End Region

            If oTransferRequest.GetByKey(_Docentry) Then

                Dim FromWhs As String = clsLnProveedor.Get_Codigo_By_IdProveedorBodega(BeTransOCEnc.IdProveedorBodega, lConnection, lTransaction)
                Dim ToWhs As String = clsLnBodega.Get_Codigo_By_IdBodega(BeTransOCEnc.IdBodega, lConnection, lTransaction)

                oTransfer.CardCode = oTransferRequest.CardCode
                oTransfer.DocDate = Date.Today
                oTransfer.DocObjectCode = BoObjectTypes.oStockTransfer
                oTransfer.FromWarehouse = FromWhs
                oTransfer.ToWarehouse = ToWhs
                oTransfer.UserFields.Fields.Item("U_ToWhsCode").Value = ToWhs
                oTransfer.ShipToCode = oTransferRequest.ShipToCode
                oTransfer.Address = oTransferRequest.Address
                oTransfer.SalesPersonCode = oTransferRequest.SalesPersonCode
                oTransfer.PriceList = oTransferRequest.PriceList
                oTransfer.JournalMemo = $"Traslado de mercancía generado por WMS, en base a solicitud SAP{_Docentry} / IdDocumentoIngresoWMS {BeTransOCEnc.IdOrdenCompraEnc}"
                oTransfer.Comments = $"Traslado de mercancía generado por WMS, en base a solicitud SAP{_Docentry} / IdDocumentoIngresoWMS {BeTransOCEnc.IdOrdenCompraEnc}"

                NoLineaEntrega = 0
                NoLineaEntregaLote = 0

                For j As Integer = 0 To oTransferRequest.Lines.Count - 1

                    oTransferRequest.Lines.SetCurrentLine(j)

                    vCodigoProductoSAP = oTransferRequest.Lines.ItemCode.ToString()
                    Dim vNoLineaOCSAP As Integer = oTransferRequest.Lines.LineNum
                    Dim vUoMEntry As Integer = oTransferRequest.Lines.UoMEntry
                    Dim vUoMCode As String = oTransferRequest.Lines.UoMCode

                    Dim DistinctProductosLineasPres = ProductosParaSAP.Where(Function(x) x.CodigoProductoSAP = vCodigoProductoSAP _
                                                                              AndAlso x.No_Linea = vNoLineaOCSAP).
                                                                        GroupBy(Function(x) New With {Key x.CodigoProductoSAP, Key x.No_Linea, Key x.CodigoPresentacion}).
                                                                        Select(Function(g) New With {
                                                                            g.Key.CodigoProductoSAP,
                                                                            g.Key.No_Linea,
                                                                            g.Key.CodigoPresentacion,
                                                                            .Cantidad_Total = g.Sum(Function(x) x.Cantidad_Total)
                                                                        }).ToList()

                    If DistinctProductosLineasPres.Any() Then

                        For Each ProductoSalida In DistinctProductosLineasPres

                            If Not oTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then

                                If ProductoSalida.Cantidad_Total <= oTransferRequest.Lines.Quantity Then

                                    Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.CodigoProductoSAP OrElse
                                                                         vNoLineaAnterior <> ProductoSalida.No_Linea)

                                    If nuevaLineaEntrega Then

                                        oTransfer.Lines.ItemCode = oTransferRequest.Lines.ItemCode
                                        oTransfer.Lines.BaseEntry = _Docentry
                                        oTransfer.Lines.BaseLine = vNoLineaOCSAP
                                        oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
                                        oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                                        oTransfer.Lines.FromWarehouseCode = FromWhs
                                        oTransfer.Lines.WarehouseCode = ToWhs

                                        If vUoMEntry > 0 AndAlso vUoMCode <> "Unidad" Then
                                            oTransfer.Lines.UoMEntry = vUoMEntry
                                        End If

                                        vCodigoAnterior = oTransfer.Lines.ItemCode
                                        vNoLineaAnterior = oTransfer.Lines.LineNum

                                        oTransfer.Lines.Add()

                                        vAgregarEntrega = True

                                        NoLineaEntrega += 1

                                        vCodigoProductoWMS = ProductosParaSAP.Find(Function(x) x.CodigoProductoSAP = vCodigoProductoSAP).CodigoProductoWMS

                                        Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = _Docentry _
                                                                                                  AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                                  AndAlso x.Codigo_producto = vCodigoProductoWMS _
                                                                                                  AndAlso x.Enviado = False)

                                        If Not Sublista_A_Actualizar Is Nothing Then
                                            If Sublista_A_Actualizar.Count > 0 Then
                                                Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                            End If
                                        End If

                                    End If

                                Else
                                    Throw New Exception("WMS está intentando generar una entrega por: " & ProductoSalida.Cantidad_Total &
                                                        " en una línea de SAP para el material: " & oTransferRequest.Lines.ItemCode &
                                                        " que refleja una cantidad de: " & oTransferRequest.Lines.Quantity & " probablemente esto no sea posible.")
                                End If
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oTransferRequest.Lines.ItemCode.ToString()))
                            End If

                        Next 'DistinctProductosLineas                        


                    End If

                Next

                Dim oResultado As Integer = -1

                If vAgregarEntrega Then

                    oResultado = oTransfer.Add()

                    If oResultado <> 0 Then
                        Dim errMsg = oCompany.GetLastErrorDescription()
                        Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                    Else

                        Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar, lConnection, lTransaction)

                        If IResult = 0 Then
                            Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                        End If

                        Dim newObjectCode As String = ""
                        oCompany.GetNewObjectCode(newObjectCode)

                        Dim vTrasladoDocEntry As Integer = 0

                        If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                            Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                        End If

                        Dim vDocNumTraslado As Integer = 0

                        Try

                            Dim vCreatedoTransfer As StockTransfer
                            vCreatedoTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)

                            If vCreatedoTransfer.GetByKey(vTrasladoDocEntry) Then
                                vDocNumTraslado = vCreatedoTransfer.DocNum
                                clsPublic.Actualizar_Progreso(lblprg, vbTab & vbTab & "✔️ Se creó el traslado con DocNum: " & vDocNumTraslado)
                            End If
                        Catch ex As Exception
                            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                        End Try

                    End If

                Else
                    Throw New Exception("No se pudo crear la entrega para el documento SAP: " & _Docentry & " Sociedad: " & BeTransOCEnc.Codigo_Empresa_ERP & " IdOrdenCompraEnc: " & BeTransOCEnc.IdOrdenCompraEnc)
                End If

            End If

            Return True

        Catch errMsg As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            Throw

        End Try

    End Function
    Private Shared Function Marcar_Solicitud_Trasladado_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                                         ByVal EstadoEnvio As Estado_Enviado_SAP,
                                                                         ByVal lblprg As RichTextBox,
                                                                         Optional ByVal pCompany As Integer = 1) As Boolean
        Marcar_Solicitud_Trasladado_Sincronizado_SAP = False

        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            Dim query As String = $"UPDATE OWTQ SET U_Enviado_WMS = '{CInt(EstadoEnvio)}' WHERE DocEntry = '{pNoDocumento}'"

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query)

            clsPublic.Actualizar_Progreso(lblprg, $"✔ Se actualizó correctamente el estado del documento: {pNoDocumento} a estado Enviado")

            Marcar_Solicitud_Trasladado_Sincronizado_SAP = True

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod().Name} - Error: {ex.Message}", ex)
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function
    Public Function Get_Codigos_Bodegas(ByVal IdPedidoEnc As Integer,
                                        lConnection As SqlConnection,
                                        lTransaction As SqlTransaction,
                                        ByRef pBePedidoEnc As clsBeTrans_pe_enc) As clsBeInfoBodegaByIdPedidoEnc

        Get_Codigos_Bodegas = Nothing

        Dim BeInfoBodega As New clsBeInfoBodegaByIdPedidoEnc

        Try

            Dim BePedidoEnc As New clsBeTrans_pe_enc
            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, lConnection, lTransaction)

            If Not BePedidoEnc Is Nothing Then

                BeInfoBodega.Codigo_Bodega_Origen = clsLnBodega.Get_Codigo_By_IdBodega(BePedidoEnc.IdBodega, lConnection, lTransaction)
                BeInfoBodega.Codigo_Bodega_Destino = BePedidoEnc.Cliente.Codigo
                Get_Codigos_Bodegas = BeInfoBodega
                pBePedidoEnc = BePedidoEnc

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Function Enviar_Traslados_Desde_Solicitud(ByRef lblprg As RichTextBox,
                                                            ByRef prg As ProgressBar,
                                                            ByVal pTipo As tTipoDocumentoSalida) As Boolean

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim clsTrans As New clsTransaccion

        Enviar_Traslados_Desde_Solicitud = False

        Try

            CnnLog.Open() : clsTrans.Begin_Transaction()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo, clsTrans.lConnection, clsTrans.lTransaction)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documentos a enviar: {0}", ListaPedidosTransf.Count))

                For Each PT In ListaPedidosTransf

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido: {0}-{1}", PT.Idpedidoenc, PT.No_pedido))

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)
                    Dim vCodEmpresaPed As String = clsLnTrans_pe_enc.Get_Empresa_By_IdPedidoEnc(PT.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

                    If Not Enviado_A_Erp Then

                        Dim vvEmpresa As pEmpresa = [Enum].Parse(GetType(pEmpresa), vCodEmpresaPed)
                        Dim vDocEntrySAP As Integer = 0

                        vDocEntrySAP = If(PT.No_pedido.StartsWith("K") OrElse PT.No_pedido.StartsWith("G"), PT.No_pedido.Substring(1), PT.No_pedido)

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x)
                                                                                      Dim pedido = If(x.No_pedido.StartsWith("K") OrElse x.No_pedido.StartsWith("G"), x.No_pedido.Substring(1), x.No_pedido)
                                                                                      Return pedido.Trim() = vDocEntrySAP
                                                                                  End Function)


                        lTransaccionesSalidaSingle.ForEach(Sub(t)
                                                               If t.No_pedido.StartsWith("K") OrElse t.No_pedido.StartsWith("G") Then
                                                                   t.No_pedido = t.No_pedido.Substring(1)
                                                               End If
                                                           End Sub)


                        Dim vResult As Boolean = Enviar_Traslado_Desde_Solicitud_SAP(vDocEntrySAP,
                                                                                     PT.No_pedido,
                                                                                     lTransaccionesSalidaSingle,
                                                                                     clsTrans,
                                                                                     vvEmpresa,
                                                                                     lblprg,
                                                                                     prg)

                        If vResult Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", vResult))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)

                                Enviar_Traslados_Desde_Solicitud = True

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Se están intentando enviar registros de un pedido que ya fue marcado como enviado a ERP, por favor valide la integridad de los datos manualmente.")
                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

            clsTrans.Commit_Transaction()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            Throw
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            clsTrans.Close_Conection()
        End Try

    End Function
    Private Shared Function Enviar_Solicitud_Traslado_SAP(ByVal lINavTransaccionesOut As List(Of ProductoTransferSAP),
                                                          ByVal FromWhs As String,
                                                          ByVal ToWhs As String,
                                                          ByRef vTrasladoDocEntry As String,
                                                          ByVal IdPedidoEnc As Integer,
                                                          ByVal solicitudTransfer As StockTransfer,
                                                          ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                          ByVal oCompany As Company,
                                                          ByVal pEmpresa As pEmpresa,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction,
                                                          ByRef lblprg As RichTextBox,
                                                          ByRef prg As ProgressBar) As Boolean

        Enviar_Solicitud_Traslado_SAP = False

        Dim vLineasAgregadas As Integer = 0

        Try
            If lINavTransaccionesOut Is Nothing OrElse Not lINavTransaccionesOut.Any() Then
                Throw New Exception("Error_20250519: No se encontraron líneas válidas para generar la solicitud de traslado.")
            End If

            prg.Maximum = lINavTransaccionesOut.Count
            prg.Visible = True

            Application.DoEvents()

            Dim oTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

            ' Cabecera única
            Dim vMsg As String = $"Documento generado en base a Pedido de WMS No. {IdPedidoEnc} Despacho: {BeDespachoEnc.IdDespachoEnc} Bod. Origen: {FromWhs} Bod. Destino: {ToWhs}"
            oTransfer.FromWarehouse = FromWhs
            oTransfer.ToWarehouse = ToWhs
            oTransfer.JournalMemo = vMsg
            oTransfer.Comments = vMsg
            oTransfer.CardCode = solicitudTransfer.CardCode
            oTransfer.CardName = solicitudTransfer.CardName
            oTransfer.DocDate = Date.Today
            oTransfer.UserFields.Fields.Item("U_ToWhsCode").Value = ToWhs
            oTransfer.UserFields.Fields.Item("U_Enviado_WMS").Value = 2
            oTransfer.ShipToCode = solicitudTransfer.ShipToCode
            oTransfer.Address = solicitudTransfer.Address
            oTransfer.SalesPersonCode = solicitudTransfer.SalesPersonCode
            oTransfer.PriceList = solicitudTransfer.PriceList

            ' Detalle
            Dim vCodigoAnterior As String = ""
            Dim vNoLineaAnterior As Integer = -1

            Dim ProductosAgrupadosParaSolicitud = lINavTransaccionesOut.Where(Function(x) x.IdPedidoEnc = IdPedidoEnc).
            GroupBy(Function(p) New With {Key p.CodigoProductoSAP, Key p.CodigoProductoWMS, Key p.No_Linea, Key p.CodigoPresentacion}).
                                Select(Function(g) New With {
                                    g.Key.No_Linea,
                                    g.Key.CodigoProductoSAP,
                                    g.Key.CodigoProductoWMS,
                                    g.Key.CodigoPresentacion,
                                    .Cantidad = g.Sum(Function(x) x.Cantidad_Total)
                                }).ToList()

            For i As Integer = 0 To solicitudTransfer.Lines.Count - 1

                solicitudTransfer.Lines.SetCurrentLine(i)

                Dim codigoProductoSAP As String = solicitudTransfer.Lines.ItemCode
                Dim lineaSAP As Integer = solicitudTransfer.Lines.LineNum
                Dim productosLinea = ProductosAgrupadosParaSolicitud.FindAll(Function(x) x.CodigoProductoSAP = codigoProductoSAP AndAlso x.No_Linea = lineaSAP).ToList()

                If productosLinea.Any() Then

                    For Each prod In productosLinea

                        '#EJC20251226 Erik quitóla validación de línea cerrada
                        If prod.Cantidad <= solicitudTransfer.Lines.Quantity Then

                            ' Solo agregamos línea si es un nuevo producto
                            If vCodigoAnterior <> prod.CodigoProductoSAP Then

                                oTransfer.Lines.ItemCode = prod.CodigoProductoSAP
                                oTransfer.Lines.Quantity = prod.Cantidad
                                oTransfer.Lines.FromWarehouseCode = FromWhs
                                oTransfer.Lines.WarehouseCode = ToWhs

                                Dim resultado = SapHelper.Obtener_UoMEntry_De_InventoryUOM(prod.CodigoProductoSAP, oCompany)
                                Dim umEntryOrdenVenta As Integer = Buscar_UoMEntry_SolTraslado(solicitudTransfer, prod.CodigoProductoSAP, prod.No_Linea)
                                If resultado.UoMEntry > 0 Then
                                    If resultado.UoMEntry <> umEntryOrdenVenta Then
                                        oTransfer.Lines.Quantity = Math.Round(prod.Cantidad / resultado.Factor, 6)
                                    End If
                                    oTransfer.Lines.UoMEntry = resultado.UoMEntry
                                End If

                                vCodigoAnterior = prod.CodigoProductoSAP
                                vNoLineaAnterior = prod.No_Linea
                                oTransfer.Lines.Add() : vLineasAgregadas += 1

                            End If

                        Else
                            Throw New Exception($"Cantidad WMS ({prod.Cantidad}) excede cantidad SAP ({solicitudTransfer.Lines.Quantity}) para producto: {codigoProductoSAP}")
                        End If

                    Next

                Else
                    clsPublic.Actualizar_Progreso(lblprg, ($"Producto {codigoProductoSAP} línea {lineaSAP} no encontrado en agrupación."))
                End If

            Next

            If vLineasAgregadas > 0 Then

                ' Intentar agregar en SAP
                Dim oResultado As Integer = oTransfer.Add()
                If oResultado <> 0 Then
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {oCompany.GetLastErrorDescription()}")
                End If

                ' Obtener el nuevo DocEntry
                Dim newObjectCode As String = ""
                oCompany.GetNewObjectCode(newObjectCode)

                Dim vSolTrasDocEntry As Integer = 0

                If Not Integer.TryParse(newObjectCode, vSolTrasDocEntry) Then
                    Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                End If

                ' Obtener y actualizar datos relacionados
                Dim tmpTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If tmpTransfer.GetByKey(CInt(vSolTrasDocEntry)) Then
                    Dim vMsgSol = "Solicitud de Traslado generada por WMS en base a Transferencia SAP No. " & vTrasladoDocEntry
                    clsPublic.Actualizar_Progreso(lblprg, vMsgSol)
                    clsLnLog_error_wms.Agregar_Error("#TRPSAP20250610A: " & vMsgSol)
                    BeDespachoEnc.No_pase = tmpTransfer.DocNum
                    clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespachoEnc, lConnection, lTransaction)
                    Enviar_Solicitud_Traslado_SAP = True
                End If
            Else
                Throw New Exception("Error2025510101216: El documento no tiene líneas válidas para SAP.")
            End If

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error al enviar traslado entre almacenes a SAP: {MethodBase.GetCurrentMethod.Name()} {ex.Message}",
                                                        "",
                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                        BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, $"Error al enviar solicitud de traslado entre almacenes a SAP:{vbNewLine}{ex.Message}")
            Throw New Exception($"Error al enviar solicitud de traslado entre almacenes a SAP:{vbNewLine}{ex.Message}")
        End Try

    End Function

    'For Each producto In lINavTransaccionesOut

    '    Dim productosLinea = lINavTransaccionesOut.
    '    Where(Function(p) p.CodigoProductoSAP = producto.CodigoProductoSAP AndAlso
    '                      p.No_Linea = producto.No_Linea AndAlso
    '                      p.IdPedidoEnc = producto.IdPedidoEnc).
    '    GroupBy(Function(p) New With {Key p.CodigoProductoSAP, Key p.CodigoProductoWMS}).
    '    Select(Function(g) New With {
    '        g.Key.CodigoProductoSAP,
    '        g.Key.CodigoProductoWMS,
    '        .Cantidad = g.Sum(Function(x) x.CantidadBase)
    '    }).ToList()

    '    ' Solo agregar línea si cambia el producto o línea
    '    Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> producto.CodigoProductoSAP OrElse vNoLineaAnterior <> producto.No_Linea)
    '    If nuevaLineaTransfer Then
    '        oTransfer.Lines.ItemCode = producto.CodigoProductoSAP
    '        oTransfer.Lines.Quantity = producto.CantidadBase
    '        oTransfer.Lines.FromWarehouseCode = FromWhs
    '        oTransfer.Lines.WarehouseCode = ToWhs

    '        Dim vIdUMEntrySAP As Integer = SapHelper.Obtener_UoMEntry_Por_Codigo_Con_Recordset(producto.CodigoPresentacion, oCompany)
    '        If vIdUMEntrySAP > 0 Then
    '            oTransfer.Lines.UoMEntry = vIdUMEntrySAP
    '        End If

    '        vCodigoAnterior = producto.CodigoProductoSAP
    '        vNoLineaAnterior = producto.No_Linea
    '        oTransfer.Lines.Add()
    '    End If
    'Next
    Public Shared Function Importar_Solicitud_Traslado_Entrada_SAP(ByRef lblprg As RichTextBox,
                                                                    ByRef prg As Windows.Forms.ProgressBar,
                                                                    Optional ByVal ForzarEjecucion As Boolean = False,
                                                                    Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                    Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Solicitud_Traslado_Entrada_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

        Try

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            lblprg.Text = ""

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Procesar_Solicitudes_Traslado_Entrada_SAP(BeConfigEnc, lblprg, True, prg, CnnLog, pPedidoCliente) Then
                Exit Function
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      "",
                                                      BeNavEjecucionEnc.IdEjecucionEnc,
                                                      BeConfigDet.Idnavconfigdet,
                                                      CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function
    Public Shared Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                       ByVal pNombre As String,
                                                       ByVal cnnLog As SqlConnection) As Boolean

        Inserta_Proveedor_Desde_SAP = False


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim clstrans As New clsTransaccion

        Try

            clstrans.Begin_Transaction()

            BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
            BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
            BeProveedor.IdProveedor = clsLnProveedor.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
            BeProveedor.Codigo = pCodigo
            BeProveedor.Nombre = pNombre
            BeProveedor.Telefono = ""
            BeProveedor.Nit = pCodigo
            BeProveedor.Direccion = ""
            BeProveedor.Contacto = ""
            BeProveedor.Activo = True
            BeProveedor.User_agr = BeConfigEnc.IdUsuario
            BeProveedor.Fec_agr = Date.UtcNow
            BeProveedor.User_mod = BeConfigEnc.IdUsuario
            BeProveedor.Fec_mod = Date.UtcNow

            Try

                clsLnProveedor.Insertar(BeProveedor,
                                            clstrans.lConnection,
                                            clstrans.lTransaction)

                BeProveedorBodega = New clsBeProveedor_bodega
                BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(clstrans.lConnection, clstrans.lTransaction) + 1
                BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                BeProveedorBodega.Activo = True
                BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                BeProveedorBodega.Fec_agr = Now
                BeProveedorBodega.Fec_mod = Now

                clsLnProveedor_bodega.Insertar(BeProveedorBodega,
                                               clstrans.lConnection,
                                               clstrans.lTransaction)

                Inserta_Proveedor_Desde_SAP = True

                clstrans.lTransaction.Commit()

            Catch ex As Exception

                clstrans.RollBack_Transaction()

                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                Throw

            End Try


        Catch ex As Exception
            Throw
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Private Shared Function Procesar_Solicitudes_Traslado_Entrada_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                                                      ByVal lblprg As RichTextBox,
                                                                      ByVal FlujoEntrada As Boolean,
                                                                      ByRef prg As Windows.Forms.ProgressBar,
                                                                      ByRef cnnLog As SqlConnection,
                                                                      Optional pPedidoCliente As String = "") As Boolean

        Procesar_Solicitudes_Traslado_Entrada_SAP = False

        Try

            Dim BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega)
            If BeBodega Is Nothing Then
                clsPublic.Actualizar_Progreso(lblprg, "ERROR_202405210317: No se obtuvo la bodega para la configuración de la interface.")
                Return False
            End If

            ProcesarTrasladosEmpresa(lblprg, cnnLog, BeBodega.Codigo, FlujoEntrada, pPedidoCliente, pEmpresa.Killios, "Killios")
            ProcesarTrasladosEmpresa(lblprg, cnnLog, BeBodega.Codigo, FlujoEntrada, pPedidoCliente, pEmpresa.Garesa, "Garesa")

            Procesar_Solicitudes_Traslado_Entrada_SAP = True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            Throw
        End Try

    End Function
    Private Shared Sub ProcesarTrasladosEmpresa(ByRef lblprg As RichTextBox,
                                                ByRef cnnLog As SqlConnection,
                                                ByVal CodigoBodega As String,
                                                ByVal FlujoEntrada As Boolean,
                                                ByVal pPedidoCliente As String,
                                                ByVal Empresa As pEmpresa,
                                                ByVal NombreEmpresa As String)

        Dim pedidosTraslado = Get_Solicitudes_Traslado_Entrada_SAP(CodigoBodega, FlujoEntrada, pPedidoCliente, Empresa)

        If pedidosTraslado Is Nothing OrElse pedidosTraslado.Count = 0 Then
            Dim mensaje = If(pPedidoCliente <> "", $"No hay traslados de {NombreEmpresa} pendientes de importar con el No.: {pPedidoCliente}", $"No hay traslados de {NombreEmpresa} pendientes de importar")
            clsPublic.Actualizar_Progreso(lblprg, mensaje & vbNewLine)
            Exit Sub
        End If

        Try

            For Each Pedido In pedidosTraslado

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando Solicitud de Traslado (OWTQ) de {NombreEmpresa}: {Pedido.No}/{Pedido.Vendor_Invoice_No}{vbNewLine}")

                If Not clsLnProveedor.Existe_Proveedor(Pedido.Buy_From_Vendor_No) Then
                    If Inserta_Proveedor_Desde_SAP(Pedido.Buy_From_Vendor_No, Pedido.Company_Code, cnnLog) Then
                        clsPublic.Actualizar_Progreso(lblprg, vbTab & $"El proveedor: {Pedido.Buy_From_Vendor_No} No existía en WMS y fue insertado.")
                    End If
                End If

                Try

                    Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
                    Dim vResult As String = ""

                    If Pedido.Company_Code <> "" Then
                        Pedido.No = Pedido.Company_Code.Substring(0, 1) & Pedido.No
                    End If

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(Pedido, BePedidoCompraEnc, vResult) Then
                        clsPublic.Actualizar_Progreso(lblprg, $"Se creó el documento de traslado de {NombreEmpresa}: {vResult}")
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, $"No se pudo crear el documento de traslado de {NombreEmpresa}: {vResult}")
                    End If

                    If BePedidoCompraEnc IsNot Nothing Then

                        Dim vNoPedido As String = Pedido.No

                        If Pedido.No.Substring(0, 1) = "K" OrElse Pedido.No.Substring(0, 1) = "G" Then
                            vNoPedido = vNoPedido.Substring(1)
                        End If

                        Marcar_Solicitud_Trasladado_Sincronizado_SAP(vNoPedido, Estado_Enviado_SAP.Enviado, lblprg, Empresa)

                    End If

                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                End Try

            Next

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        End Try

    End Sub
    Private Shared Function Get_Solicitudes_Traslado_Entrada_SAP(ByVal pCodigoBodegaInterface As String,
                                                                 ByVal FlujoEntrada As Boolean,
                                                                 Optional ByVal pPedidoCliente As String = "",
                                                                 Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_compra_enc)
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            Dim filtros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Traslado_SAP)

            ' Obtener fecha y filtros
            Dim startDate As String = "", criterioBodegas As String = ""
            ObtenerFechaInicioYFiltroBodegas(startDate, criterioBodegas)

            Dim queryEncabezado As String = ConstruirQueryEncabezado(pPedidoCliente,
                                                                    FlujoEntrada,
                                                                    pCodigoBodegaInterface,
                                                                    criterioBodegas,
                                                                    startDate)

            Dim RsEnc As Recordset = EjecutarConsulta(oCompany, queryEncabezado)

            While Not RsEnc.EoF

                Dim pedido = MapearEncabezadoPedido(RsEnc, BeConfigEnc, pCompany)
                Dim queryDet = ConstruirQueryDetalle(pedido.No)
                Dim rsDet = EjecutarConsulta(oCompany, queryDet)

                While Not rsDet.EoF
                    Dim detalle = MapearDetallePedido(rsDet, pedido.No)
                    pedido.Lineas_Detalle.Add(detalle)
                    rsDet.MoveNext()
                End While

                lPedidosCliente.Add(pedido)

                RsEnc.MoveNext()

            End While

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod().Name} {ex.Message}")
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function
    Private Shared Sub ObtenerFechaInicioYFiltroBodegas(ByRef startDate As String, ByRef criterioBodegas As String)
        startDate = "20221214" ' Valor por defecto
        criterioBodegas = ""

        Dim lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Traslado_SAP)
        Dim criterios As New List(Of String)

        For Each filtro In lFiltros
            If filtro.Tipo_Filtro = "FECHA_INICIO" Then
                startDate = filtro.Valor
            ElseIf String.IsNullOrEmpty(filtro.Tipo_Filtro) OrElse filtro.Tipo_Filtro = "BODEGA" Then
                criterios.Add("'" & filtro.Valor & "'")
            End If
        Next

        criterioBodegas = String.Join(",", criterios)
    End Sub
    Private Shared Function ConstruirQueryEncabezado(pPedidoCliente As String,
                                          FlujoEntrada As Boolean,
                                          pCodigoBodegaInterface As String,
                                          criterioBodegas As String,
                                          startDate As String) As String

        Dim baseQuery As String = "
                            SELECT DISTINCT 
                                   T0.DocEntry, 
                                   T0.DocNum, 
                                   T0.DocDueDate DOCDATE, 
                                   T0.CardName,
                                   T1.FromWhsCod AS CODIGO_BODEGA_ORIGEN, 
                                   OW1.WhsName AS NOMBRE_BODEGA_ORIGEN,
                                   T1.WhsCode AS CODIGO_BODEGA_DESTINO, 
                                   OW2.WhsName AS NOMBRE_BODEGA_DESTINO,
                                   T0.Comments AS JRNLMEMO, 
                                   T0.Canceled, 
                                   T0.DocStatus, 
                                   'TRANSFERENCIA' AS Tipo_Transferencia,
                                   T1.WhsCode AS U_ALMDEST
                            FROM OWTQ T0
                            INNER JOIN WTQ1 T1 ON T0.DocEntry = T1.DocEntry
                            INNER JOIN OWHS OW1 ON T1.FromWhsCod = OW1.WhsCode
                            INNER JOIN OWHS OW2 ON T1.WhsCode = OW2.WhsCode
                            WHERE T0.DocStatus = 'O' 
                              AND T0.U_Enviado_WMS = 2"

        ' Filtro por documento específico (si aplica)
        If Not String.IsNullOrEmpty(pPedidoCliente) Then
            baseQuery &= " AND T0.DocNum = " & pPedidoCliente
        End If

        ' Filtro por almacenes según flujo
        If Not FlujoEntrada Then
            If Not String.IsNullOrEmpty(criterioBodegas) Then
                baseQuery &= " AND (T1.FromWhsCod IN (" & criterioBodegas & ") OR T1.WhsCode IN (" & criterioBodegas & "))"
            Else
                baseQuery &= " AND (T1.FromWhsCod = '" & pCodigoBodegaInterface & "' OR T1.WhsCode = '" & pCodigoBodegaInterface & "')"
            End If
        Else
            baseQuery &= " AND T1.WhsCode = '" & pCodigoBodegaInterface & "'"
        End If

        ' Ordenamiento final
        baseQuery &= " ORDER BY T0.DocEntry DESC"

        Return baseQuery
    End Function
    Private Shared Function ConstruirQueryDetalle(docEntry As String) As String
        Return $"
        SELECT T0.LineNum, T1.U_CodWMS AS ITEMCODE, T0.DSCRIPTION, T0.QUANTITY, T0.PRICE,
               T0.LINETOTAL, T0.VATSUM, T0.DOCENTRY, T0.WHSCODE, T0.UomCode AS UNIDAD_MEDIDA
        FROM WTQ1 T0
        INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode
        WHERE T0.DocEntry = '{docEntry}'"
    End Function
    Private Shared Function EjecutarConsulta(oCompany As Company, query As String) As Recordset
        Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        rs.DoQuery(query)
        Return rs
    End Function
    Private Shared Function MapearEncabezadoPedido(rs As Recordset,
                                        config As clsBeI_nav_config_enc,
                                        empresa As pEmpresa) As clsBeI_nav_ped_compra_enc
        Dim p As New clsBeI_nav_ped_compra_enc With {
            .No = rs.Fields.Item("DOCENTRY").Value,
            .Posting_Date = rs.Fields.Item("DOCDATE").Value,
            .Order_Date = rs.Fields.Item("DOCDATE").Value,
            .Document_Date = rs.Fields.Item("DOCDATE").Value,
            .Expected_Receipt_Date = rs.Fields.Item("DOCDATE").Value,
            .Status = 1,
            .Buy_From_Vendor_No = rs.Fields.Item("CODIGO_BODEGA_ORIGEN").Value.ToString(),
            .Buy_From_Vendor_Name = rs.Fields.Item("NOMBRE_BODEGA_ORIGEN").Value.ToString(),
            .Is_Internal_Transfer = False,
            .Location_Code = rs.Fields.Item("CODIGO_BODEGA_DESTINO").Value.ToString(),
            .Vendor_Invoice_No = rs.Fields.Item("DOCNUM").Value.ToString(),
            .Posting_Description = rs.Fields.Item("JRNLMEMO").Value.ToString(),
            .Product_Owner_Code = config.IdPropietario,
            .Ship_To_Contact = rs.Fields.Item("NOMBRE_BODEGA_DESTINO").Value.ToString(),
            .Document_Type = tTipoDocumentoIngreso.Transferencia_de_Ingreso,
            .Company_Code = empresa.ToString(),
            .Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)
        }
        Return p
    End Function
    Private Shared Function MapearDetallePedido(rs As Recordset, docEntry As String) As clsBeI_nav_ped_compra_det
        Return New clsBeI_nav_ped_compra_det With {
            .NoEnc = docEntry,
            .No = rs.Fields.Item("ITEMCODE").Value.ToString(),
            .Line_No = rs.Fields.Item("LINENUM").Value.ToString(),
            .Quantity = Convert.ToDecimal(rs.Fields.Item("QUANTITY").Value),
            .Description = rs.Fields.Item("DSCRIPTION").Value.ToString(),
            .Unit_of_Measure_Code = rs.Fields.Item("UNIDAD_MEDIDA").Value.ToString(),
            .Variant_Code = Nothing
        }
    End Function
    Public Shared Function Enviar_Transferencia_Stock_SAP(ByVal _DocEntry As Integer,
                                                          ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                          ByVal pCompany As pEmpresa,
                                                          ByRef lblprg As RichTextBox,
                                                          ByRef prg As ProgressBar) As Boolean

        Enviar_Transferencia_Stock_SAP = False
        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim ProductosParaSAP As New List(Of ProductoTransferSAP)
        Dim clsTrans As New clsTransaccion
        Dim clsSapTrans As clsSapTransaction = Nothing
        Dim oCompany As Company = Nothing

        Try

            oCompany = sapPool.GetConnection(pCompany).Company
            clsTrans.Begin_Transaction()

            clsSapTrans = New clsSapTransaction(oCompany)
            clsSapTrans.BeginTransaction()

            Dim IdPedidoEnc = lINavTransaccionesOut.First().Idpedidoenc
            Dim IdDespachoEnc = lINavTransaccionesOut.First().Iddespachoenc

            Dim BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, clsTrans.lConnection, clsTrans.lTransaction)
            Dim BeDespacho = clsLnTrans_despacho_enc.GetSingle(IdDespachoEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ProductosParaSAP = AgruparProductosParaTraslado(lINavTransaccionesOut, pCompany, clsTrans)

            If ProductosParaSAP.Count = 0 Then
                Throw New Exception("No se pudo realizar la agrupación de registros previo a su envío.")
            End If

            Dim oTransferRequest As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)
            If Not oTransferRequest.GetByKey(_DocEntry) Then
                Throw New Exception("No se encontró la solicitud de traslado con DocEntry: " & _DocEntry)
            End If

            Dim BeBodega As New clsBeBodega
            BeBodega = clsLnBodega.GetSingle_By_Codigo(BePedidoEnc.Cliente.Codigo)

            If Not BeBodega Is Nothing Then
                Dim BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BePedidoEnc.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)
                'If BeConfigEnc IsNot Nothing Then
                '    If Not BeConfigEnc.Bodega_Prorrateo.Equals(BePedidoEnc.Bodega_Destino) OrElse Not BeConfigEnc.Bodega_Prorrateo1.Equals(BePedidoEnc.Bodega_Destino) Then
                '        clsPublic.Actualizar_Progreso(lblprg, "❌ La bodega de prorrateo no coincide con la bodega del cliente en el pedido.")
                '        Exit Function
                '    End If
                'End If
            End If

            Dim oTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
            Configurar_Encabezado_Transferencia(oTransfer, oTransferRequest, BePedidoEnc, BeDespacho)

            Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
            Dim NoLineaTransfer As Integer = 0
            Dim vAgregarEntrega As Boolean = False

            For j As Integer = 0 To oTransferRequest.Lines.Count - 1

                oTransferRequest.Lines.SetCurrentLine(j)

                Dim codigoSAP = oTransferRequest.Lines.ItemCode.ToString()
                Dim lineaSAP = oTransferRequest.Lines.LineNum

                Dim productosLinea = ProductosParaSAP.Where(Function(x) x.CodigoProductoSAP = codigoSAP AndAlso x.No_Linea = lineaSAP AndAlso x.IdPedidoEnc = IdPedidoEnc).ToList()

                For Each prod In productosLinea

                    If oTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then
                        clsPublic.Actualizar_Progreso(lblprg, $"Producto {codigoSAP} ya fue completado.")
                        Continue For
                    End If

                    If prod.Cantidad_Total > oTransferRequest.Lines.Quantity Then
                        Throw New Exception($"WMS intenta generar un traslado por {prod.Cantidad_Total} de {codigoSAP}, pero SAP muestra {oTransferRequest.Lines.Quantity}.")
                    End If

                    oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
                    oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
                    oTransfer.Lines.BaseEntry = _DocEntry
                    oTransfer.Lines.BaseLine = lineaSAP
                    oTransfer.Lines.ItemCode = codigoSAP

                    Dim resultado = SapHelper.Obtener_UoMEntry_De_InventoryUOM(prod.CodigoProductoSAP, oCompany)
                    Dim umEntryOrdenVenta As Integer = Buscar_UoMEntry_SolTraslado(oTransferRequest, prod.CodigoProductoSAP, prod.No_Linea)
                    If resultado.UoMEntry > 0 Then
                        If resultado.UoMEntry <> umEntryOrdenVenta Then
                            oTransfer.Lines.Quantity = Math.Round(prod.Cantidad_Total / resultado.Factor, 6)
                        End If
                        oTransfer.Lines.UoMEntry = resultado.UoMEntry
                    End If

                    oTransfer.Lines.Quantity = prod.Cantidad_Total
                    oTransfer.Lines.FromWarehouseCode = oTransferRequest.FromWarehouse
                    oTransfer.Lines.WarehouseCode = BePedidoEnc.Cliente.Codigo
                    oTransfer.Lines.Add()

                    NoLineaTransfer += 1
                    vAgregarEntrega = True

                    Dim subLista = lINavTransaccionesOut.Where(Function(x) x.No_pedido = _DocEntry AndAlso x.No_linea = lineaSAP AndAlso x.Codigo_producto = prod.CodigoProductoWMS AndAlso Not x.Enviado).ToList()
                    Lista_A_Actualizar.AddRange(subLista)

                Next

            Next

            If vAgregarEntrega Then

                oTransfer.Comments = $"Traslado generado por TOMWMS Despacho WMS: {IdDespachoEnc} IdPedidoEnc: {IdPedidoEnc} Solicitud: {_DocEntry}"
                oTransfer.JournalMemo = oTransfer.Comments

                If oTransfer.Add() <> 0 Then
                    Throw New Exception($"#ERROR_SAP: {oCompany.GetLastErrorDescription()}")
                End If

                Dim newDocEntry As Integer
                Dim newObjCode As String = ""
                oCompany.GetNewObjectCode(newObjCode)
                Integer.TryParse(newObjCode, newDocEntry)

                BePedidoEnc.No_Picking_ERP = newDocEntry
                clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(BePedidoEnc, clsTrans.lConnection, clsTrans.lTransaction)

                Try
                    Dim oTempTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                    If oTempTransfer.GetByKey(newDocEntry) Then
                        Dim docNum As Integer = oTempTransfer.DocNum
                        clsPublic.Actualizar_Progreso(lblprg, "Se creó la transferencia con DocNum: " & docNum & " en SAP. para el IdPedidoEncWMS: " & IdPedidoEnc & " Solicitud de Traslado SAP: " & oTransferRequest.DocNum)
                    End If
                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                End Try

                Dim actualizados = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar, clsTrans.lConnection, clsTrans.lTransaction)

                If actualizados = 0 Then
                    Throw New Exception("No se pudieron marcar los registros como enviados.")
                End If

            End If

            clsSapTrans.CommitTransaction()
            clsTrans.Commit_Transaction()

            Enviar_Transferencia_Stock_SAP = True

        Catch ex As Exception
            clsSapTrans?.RollbackTransaction()
            clsTrans.RollBack_Transaction()
            clsPublic.Actualizar_Progreso(lblprg, $"❌ {ex.Message}")
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error al enviar traslado SAP: {MethodBase.GetCurrentMethod.Name()} {ex.Message}", "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
        End Try

    End Function
    Private Shared Function AgruparProductosParaTraslado(lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                         pEmpresa As pEmpresa,
                                                         clsTrans As clsTransaccion) As List(Of ProductoTransferSAP)

        Dim ProductosParaSAP As New List(Of ProductoTransferSAP)

        ' Agrupar por ID del pedido, línea y presentación
        Dim agrupados = lINavTransaccionesOut.
        Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Transferencia_Interna_WMS AndAlso x.Enviado = False).
        GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.No_pedido, Key x.IdDespachoDet, Key x.Idpresentacion, Key x.No_linea}).
        Select(Function(g) New With {
            g.Key.Idpedidoenc,
            g.Key.No_pedido,
            g.Key.No_linea,
            g.Key.IdDespachoDet,
            g.Key.Idpresentacion,
            .CodigoProducto = g.First().Codigo_producto,
            .Cantidad = g.Sum(Function(x) x.Cantidad)
        }).ToList()

        For Each item In agrupados

            Dim BeProducto = clsLnProducto.Get_Single_By_Codigo(item.CodigoProducto, clsTrans.lConnection, clsTrans.lTransaction)

            ' Obtener código SAP según empresa
            Dim CodigoSAP As String = item.CodigoProducto
            Select Case pEmpresa
                Case pEmpresa.Killios
                    CodigoSAP = BeProducto.Noparte
                Case pEmpresa.Garesa
                    CodigoSAP = BeProducto.Noserie
            End Select

            '#CKFK20251226 Cambié la función, porque hay que obtener presentación de la línea del pedido no del despacho
            Dim presentacionDesp As clsBeProducto_Presentacion = clsLnTrans_despacho_det.Get_BePresentacion_By_IdDespachoDet(item.IdDespachoDet,
                                                                                                                         item.Idpedidoenc,
                                                                                                                         clsTrans.lConnection,
                                                                                                                         clsTrans.lTransaction)

            Dim presentacionPed As clsBeProducto_Presentacion = clsLnTrans_pe_det.Get_BePresentacion_By_NoLinea(item.No_linea,
                                                                                                               item.Idpedidoenc,
                                                                                                               clsTrans.lConnection,
                                                                                                               clsTrans.lTransaction)
            Dim CodigoPresentacion As String = "" ' = If(BePresentacion IsNot Nothing, BePresentacion.Codigo, "")
            Dim CantidadBase As Decimal = item.Cantidad
            Dim factor As Double = 0

            If presentacionDesp IsNot Nothing AndAlso presentacionPed Is Nothing Then
                CodigoPresentacion = presentacionDesp.Codigo
            ElseIf presentacionDesp Is Nothing AndAlso presentacionPed IsNot Nothing Then
                factor = presentacionPed.Factor
                '#EJC20251217: Con esto facilito que se agrupen bien cajas y unidades en el siguiente procedimiento.
                CodigoPresentacion = presentacionPed.Codigo
                CantidadBase = Math.Round(CantidadBase / factor, 4)
            End If

            'If BePresentacion IsNot Nothing AndAlso item.Idpresentacion = 0 Then
            '    CantidadBase = Math.Round(CantidadBase / BePresentacion.Factor, 4)
            'End If

            ' Buscar si ya existe un agrupado con misma clave
            Dim existente = ProductosParaSAP.FirstOrDefault(Function(x) x.CodigoProductoSAP = CodigoSAP AndAlso
                                                            x.No_Linea = item.No_linea AndAlso
                                                            x.IdPedidoEnc = item.Idpedidoenc)

            If existente IsNot Nothing Then
                ' Sumar cantidades y anexar transacción
                existente.Cantidad_Total += CantidadBase
            Else
                ' Crear nuevo agrupado
                ProductosParaSAP.Add(New ProductoTransferSAP With {
                    .IdPedidoEnc = item.Idpedidoenc,
                    .CodigoProductoSAP = CodigoSAP,
                    .CodigoProductoWMS = item.CodigoProducto,
                    .Cantidad_Total = CantidadBase,
                    .CodigoPresentacion = CodigoPresentacion,
                    .No_Pedido = item.No_pedido,
                    .No_Linea = item.No_linea
                })
            End If


        Next

        Return ProductosParaSAP

    End Function
    Private Shared Sub Configurar_Encabezado_Transferencia(ByRef oTransfer As StockTransfer,
                                                           ByVal oTransferRequest As StockTransfer,
                                                           ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                           ByVal BeDespacho As clsBeTrans_despacho_enc)

        oTransfer.CardCode = oTransferRequest.CardCode
        oTransfer.CardName = oTransferRequest.CardName
        oTransfer.DocDate = Date.Today
        oTransfer.FromWarehouse = oTransferRequest.FromWarehouse
        oTransfer.ToWarehouse = BePedidoEnc.Cliente.Codigo
        oTransfer.UserFields.Fields.Item("U_ToWhsCode").Value = BePedidoEnc.Bodega_Destino
        oTransfer.ShipToCode = oTransferRequest.ShipToCode
        oTransfer.Address = oTransferRequest.Address
        oTransfer.SalesPersonCode = oTransferRequest.SalesPersonCode
        oTransfer.PriceList = oTransferRequest.PriceList

        oTransfer.Comments = $"Traslado generado por TOMWMS Despacho WMS: {BeDespacho.IdDespachoEnc} IdPedidoEnc: {BePedidoEnc.IdPedidoEnc} Solicitud Traslado SAP: {oTransferRequest.DocEntry}"
        oTransfer.JournalMemo = $"Traslado generado por TOMWMS Despacho WMS: {BeDespacho.IdDespachoEnc} IdPedidoEnc: {BePedidoEnc.IdPedidoEnc} Solicitud Traslado SAP: {oTransferRequest.DocEntry}"

    End Sub
    Private Shared Function Enviar_Traslado_Desde_Solicitud_SAP(ByVal _DocEntry As Integer,
                                                                ByVal _NoReferenciaWMS As String,
                                                                ByVal transaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                ByVal clsTrans As clsTransaccion,
                                                                ByVal empresa As pEmpresa,
                                                                ByRef lblprg As RichTextBox,
                                                                ByRef prg As ProgressBar) As Boolean

        Enviar_Traslado_Desde_Solicitud_SAP = False
        prg.Maximum = transaccionesOut.Count
        prg.Visible = True

        ' Agrupar productos a transferir
        Dim productosAgrupados = AgruparProductosParaTraslado(transaccionesOut, empresa, clsTrans)
        If productosAgrupados Is Nothing OrElse productosAgrupados.Count = 0 Then
            Throw New Exception("No se pudo agrupar los productos para envío a SAP.")
        End If

        ' Obtener encabezados
        Dim pedidoEnc = clsLnTrans_pe_enc.GetSingle(transaccionesOut.FirstOrDefault.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)
        Dim despachoEnc = clsLnTrans_despacho_enc.GetSingle(transaccionesOut.FirstOrDefault.Iddespachoenc, clsTrans.lConnection, clsTrans.lTransaction)

        ' Definir bodegas
        Dim bodegaDestinoFinal = If(pedidoEnc.Bodega_Destino = pedidoEnc.Cliente.Codigo, "", pedidoEnc.Bodega_Destino)
        Dim bodegaIntermedia = pedidoEnc.Cliente.Codigo

        ' Obtener conexión SAP
        Dim conn As SapConnectionWrapper = Nothing
        Dim sapTx As clsSapTransaction = Nothing

        Try

            conn = sapPool.GetConnection(empresa)
            Dim oCompany As Company = conn.Company
            sapTx = New clsSapTransaction(oCompany)
            sapTx.BeginTransaction()

            ' Obtener solicitud de transferencia (OWTQ)
            Dim solicitudTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)
            If Not solicitudTransfer.GetByKey(_DocEntry) Then
                Throw New Exception($"No se encontró la solicitud de traslado en SAP con DocEntry {_DocEntry}")
            End If

            ' Configurar encabezado de traslado
            Dim transferencia As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
            Configurar_Encabezado_Transferencia(transferencia, solicitudTransfer, pedidoEnc, despachoEnc)

            ' Agregar líneas al traslado
            Dim lineasAgregadas = AgregarLineasTransferencia(transferencia,
                                                             solicitudTransfer,
                                                             productosAgrupados,
                                                             pedidoEnc,
                                                             _NoReferenciaWMS,
                                                             transaccionesOut,
                                                             lblprg)
            If Not lineasAgregadas Then
                Throw New Exception("No se generaron líneas válidas de transferencia.")
            End If

            ' Comentarios para trazabilidad
            Dim mensaje = $"Traslado generado por TOMWMS Despacho WMS: {despachoEnc.IdDespachoEnc} IdPedidoEnc: {pedidoEnc.IdPedidoEnc} Sol. Transf. SAP: {_DocEntry}"
            transferencia.Comments = mensaje
            transferencia.JournalMemo = mensaje

            ' Enviar traslado a SAP
            If transferencia.Add() <> 0 Then
                Dim ErrorSap As String = $"#ERROR_SAP: {oCompany.GetLastErrorDescription()}"
                clsPublic.Actualizar_Progreso(lblprg, ErrorSap)
                Throw New Exception(ErrorSap)
            End If

            ' Obtener DocEntry generado
            Dim newDocEntryStr As String = ""
            oCompany.GetNewObjectCode(newDocEntryStr)

            Dim nuevoDocEntry As Integer
            If Not Integer.TryParse(newDocEntryStr, nuevoDocEntry) Then
                Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
            End If

            ' Actualizar encabezado WMS con referencias SAP
            pedidoEnc.No_Documento_Externo = nuevoDocEntry

            Dim tempTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
            If tempTransfer.GetByKey(nuevoDocEntry) Then
                pedidoEnc.No_Picking_ERP = tempTransfer.DocNum
                clsPublic.Actualizar_Progreso(lblprg, "Se creó la transferencia con DocNum: " & tempTransfer.DocNum & " en SAP.")
            End If

            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(pedidoEnc, clsTrans.lConnection, clsTrans.lTransaction)
            clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(pedidoEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ' Log del proceso
            clsPublic.Actualizar_Progreso(lblprg, mensaje)
            clsLnLog_error_wms.Agregar_Error("#TRSAPSOL20250610: " & mensaje)

            Dim solicitudTraslado As Boolean = False

            ' Enviar traslado final si aplica
            If Not String.IsNullOrEmpty(bodegaDestinoFinal) Then
                solicitudTraslado = Enviar_Solicitud_Traslado_SAP(productosAgrupados,
                                                                  bodegaIntermedia,
                                                                  bodegaDestinoFinal,
                                                                  nuevoDocEntry,
                                                                  pedidoEnc.IdPedidoEnc,
                                                                  solicitudTransfer,
                                                                  despachoEnc,
                                                                  oCompany,
                                                                  empresa,
                                                                  clsTrans.lConnection,
                                                                  clsTrans.lTransaction,
                                                                  lblprg,
                                                                  prg)
            End If

            If Not String.IsNullOrEmpty(bodegaDestinoFinal) Then

                If Not solicitudTraslado Then
                    Dim vSolicitud As Integer = clsLnTrans_despacho_enc.Get_No_Pase_By_IdDespachoEnc(despachoEnc.IdDespachoEnc, clsTrans.lConnection, clsTrans.lTransaction)
                    If vSolicitud = 0 Then
                        Throw New Exception("No se pudo generar la solicitud de traslado.")
                    End If
                End If

            End If

            ' Marcar registros como enviados            
            If clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(transaccionesOut, clsTrans.lConnection, clsTrans.lTransaction) = 0 Then
                Throw New Exception("No se pudieron marcar los registros como enviados.")
            End If

            ' Confirmar en SAP y SQL
            sapTx.CommitTransaction()

            Enviar_Traslado_Desde_Solicitud_SAP = True

        Catch ex As Exception
            sapTx?.RollbackTransaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error en {MethodBase.GetCurrentMethod.Name}: {ex.Message}", "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al enviar traslado: {vbNewLine}{ex.Message}")
            Throw
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function
    Private Shared Function AgregarLineasTransferencia(ByRef transferencia As StockTransfer,
                                                       ByVal solicitudTransfer As StockTransfer,
                                                       ByVal productosAgrupados As List(Of ProductoTransferSAP),
                                                       ByVal pedidoEnc As clsBeTrans_pe_enc,
                                                       ByVal noReferenciaWMS As String,
                                                       ByRef transaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                       ByRef lblprg As RichTextBox) As Boolean

        Dim vCodigoAnterior As String = ""
        Dim lineasAgregadas As Boolean = False
        Dim noLineaTransfer As Integer = 0

        noReferenciaWMS = productosAgrupados.FirstOrDefault.No_Pedido

        For i As Integer = 0 To solicitudTransfer.Lines.Count - 1

            solicitudTransfer.Lines.SetCurrentLine(i)

            Dim codigoProductoSAP As String = solicitudTransfer.Lines.ItemCode
            Dim lineaSAP As Integer = solicitudTransfer.Lines.LineNum
            Dim estadoLinea As BoStatus = solicitudTransfer.Lines.LineStatus
            If estadoLinea = BoStatus.bost_Close Then
                clsPublic.Actualizar_Progreso(lblprg, $"La línea {codigoProductoSAP} aparece estar cerrada en sap.")
                clsLnLog_error_wms.Agregar_Error(1, pedidoEnc.IdPedidoEnc, $"La línea {codigoProductoSAP} aparece estar cerrada en sap (se enviará de todas formas)")
            End If

            Dim productosLinea = productosAgrupados.
            Where(Function(p) p.CodigoProductoSAP = codigoProductoSAP AndAlso
                              p.No_Linea = lineaSAP AndAlso
                              p.IdPedidoEnc = pedidoEnc.IdPedidoEnc).
            GroupBy(Function(p) New With {Key p.CodigoProductoSAP, Key p.CodigoProductoWMS}).
            Select(Function(g) New With {
                g.Key.CodigoProductoSAP,
                g.Key.CodigoProductoWMS,
                .Cantidad = g.Sum(Function(x) x.Cantidad_Total)
            }).ToList()

            If productosLinea.Any() Then

                For Each prod In productosLinea

                    If prod.Cantidad <= solicitudTransfer.Lines.Quantity Then

                        ' Solo agregamos línea si es un nuevo producto
                        If vCodigoAnterior <> prod.CodigoProductoSAP Then
                            transferencia.Lines.SetCurrentLine(noLineaTransfer)
                            transferencia.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
                            transferencia.Lines.BaseEntry = solicitudTransfer.DocEntry
                            transferencia.Lines.BaseLine = lineaSAP
                            transferencia.Lines.ItemCode = codigoProductoSAP
                            transferencia.Lines.Quantity = prod.Cantidad
                            transferencia.Lines.FromWarehouseCode = solicitudTransfer.FromWarehouse
                            transferencia.Lines.WarehouseCode = pedidoEnc.Cliente.Codigo

                            transferencia.Lines.Add()
                            noLineaTransfer += 1

                            vCodigoAnterior = prod.CodigoProductoSAP
                            lineasAgregadas = True
                        End If

                        ' Marcar transacciones relacionadas para actualizar
                        Dim transaccionesActualizar = transaccionesOut.
                            Where(Function(x) x.Codigo_producto = prod.CodigoProductoWMS AndAlso
                                              x.No_pedido = noReferenciaWMS AndAlso
                                              x.No_linea = lineaSAP AndAlso
                                              x.Enviado = False).ToList()

                        If transaccionesActualizar.Any() Then
                            For Each tx In transaccionesActualizar
                                tx.Enviado = True
                            Next
                        End If

                    Else
                        Throw New Exception($"Cantidad WMS ({prod.Cantidad}) excede cantidad SAP ({solicitudTransfer.Lines.Quantity}) para producto: {codigoProductoSAP}")
                    End If

                Next

            Else
                clsPublic.Actualizar_Progreso(lblprg, ($"Producto {codigoProductoSAP} línea {lineaSAP} no encontrado en agrupación."))
            End If

        Next

        Return lineasAgregadas

    End Function

    Private Shared Function Buscar_UoMEntry_SolTraslado(oTransferRequest As StockTransfer, itemCode As String, lineNum As Integer) As Integer
        For i As Integer = 0 To oTransferRequest.Lines.Count - 1
            oTransferRequest.Lines.SetCurrentLine(i)
            If oTransferRequest.Lines.ItemCode = itemCode AndAlso oTransferRequest.Lines.LineNum = lineNum Then
                Return oTransferRequest.Lines.UoMEntry
            End If
        Next
        Return -1 ' No encontrado
    End Function

    Private Shared Function AjustarCantidadPorPresentacion(prod As clsBeI_nav_transacciones_out_agrupado,
                                                          IdPresentacionOC As Integer,
                                                          uomEntry As Integer,
                                                          uomCode As String,
                                                          lconnection As SqlConnection,
                                                          ltransaction As SqlTransaction) As Decimal

        Dim presentacion = clsLnProducto_presentacion.GetSingle(IdPresentacionOC, lconnection, ltransaction)

        If prod.IdPresentacion = 0 AndAlso uomEntry > 0 AndAlso uomCode <> "Unidad" AndAlso presentacion IsNot Nothing Then
            Return Math.Round(prod.Cantidad_Total / presentacion.Factor, 6)
        End If

        If prod.IdPresentacion <> 0 AndAlso (uomEntry = 0 OrElse uomCode = "Unidad") AndAlso
            presentacion IsNot Nothing Then
            Return Math.Round(prod.Cantidad_Total * presentacion.Factor, 6)
        End If

        Return prod.Cantidad_Total
    End Function

End Class