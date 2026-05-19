Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPPedidoCompra : Inherits clsInterfaceBase

    Private VContadorBitacoraTOMWMS As Integer = 0
    Private VContadorBitacoraIntermedia As Integer = 0

    Private Shared Function Inserta_Proveedor_Desde_SAP(ByVal pCodigo As String,
                                                         ByVal cnnLog As SqlConnection,
                                                         ByVal pCompany As pEmpresa,
                                                         ByVal oCompany As Company) As Boolean

        Inserta_Proveedor_Desde_SAP = False


        Dim BeProveedor As New clsBeProveedor
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeINavProveedor As New clsBeI_nav_proveedor
        Dim clsTrans As New clsTransaccion

        Try

            BeINavProveedor = clsSyncSAPProveedor.Get_Proveedor_SAP(pCodigo, pCompany, oCompany)

            clsTrans.Begin_Transaction()

            If Not BeINavProveedor Is Nothing Then

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                BeProveedor.Codigo = pCompany.ToString().Substring(0, 1) & BeINavProveedor.No
                BeProveedor.Nombre = BeINavProveedor.Name
                BeProveedor.Telefono = BeINavProveedor.Phone_No
                BeProveedor.Nit = BeINavProveedor.VAT_Registratrion_No
                BeProveedor.Direccion = BeINavProveedor.Adress
                BeProveedor.Contacto = BeINavProveedor.Contact
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow
                BeProveedor.Codigo_Empresa_ERP = BeINavProveedor.Search_Name

                Try

                    clsLnProveedor.Insertar(BeProveedor, clsTrans.lConnection, clsTrans.lTransaction)

                    Dim lBodegas As New List(Of clsBeBodega)
                    lBodegas = clsLnBodega.GetAll()

                    For Each Bod In lBodegas

                        BeProveedorBodega = New clsBeProveedor_bodega
                        BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                        BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                        BeProveedorBodega.IdBodega = Bod.IdBodega
                        BeProveedorBodega.Activo = True
                        BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                        BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                        BeProveedorBodega.Fec_agr = Now
                        BeProveedorBodega.Fec_mod = Now

                        clsLnProveedor_bodega.Insertar(BeProveedorBodega, clsTrans.lConnection, clsTrans.lTransaction)

                    Next

                    clsSyncSAPProveedor.Marcar_Proveedor_Sincronizado_SAP(BeProveedor.Codigo)

                    Inserta_Proveedor_Desde_SAP = True

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet,
                                                               cnnLog)

                    Throw

                End Try

            End If

            clsTrans.Commit_Transaction()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            Throw
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function

    Public Shared Function Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByRef lblprg As RichTextBox,
                                                                                           ByRef prg As ProgressBar,
                                                                                           Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                           Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                           Optional ByVal pNoDocumentoSAP As String = "") As Boolean

        Insertar_Pedidosdecompra_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim DifCant As Double = 0
        Dim gBeOrdenCompra As clsBeTrans_oc_enc = Nothing
        Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
        Dim vContador As Integer = 0
        Dim vContadorLineasDet As Integer = 0
        Dim BeProveedorBodega As New clsBeProveedor_bodega
        Dim BeProductoBodega As New clsBeProducto_bodega
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim vDocumentosIngresoPendientesImportacion As Boolean = False

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

            CnnInterface.Open() : lTransInterface = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog, pNoDocumentoSAP) Then
                Exit Function
            End If

            lTransInterface.Commit()

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)

            clsPublic.Actualizar_Progreso(lblprg, vbTab & String.Format(" -> Fin de proceso, tiempo transcurrido: {0} segundo(s)", difSegundos))

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            prg.Value = 0

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                              "",
                                              BeNavEjecucionEnc.IdEjecucionEnc,
                                              BeConfigDet.Idnavconfigdet, CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de compra a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            Throw
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function
    Private Shared Function Get_Objetos_Documento_Ingreso(ByVal IdOrdenCompraEnc As Integer,
                                                  ByVal IdRecepcionEnc As Integer,
                                                  ByVal IdBodega As Integer,
                                                  ByRef BeTransOCEnc As clsBeTrans_oc_enc,
                                                  ByRef BeReOC As clsBeTrans_re_oc,
                                                  ByRef vCodigoBodegaDestino As String) As Boolean

        Get_Objetos_Documento_Ingreso = False

        Try

            BeTransOCEnc = clsLnTrans_oc_enc.Get_BeTransOcEnc_By_IdOrdenCompraEnc(IdOrdenCompraEnc)

            If BeTransOCEnc Is Nothing Then
                Throw New Exception("ERROR_202310310531: No se obtuvo el objeto de la orden de compra para el IdOrdenCompraEnc: " & IdOrdenCompraEnc)
            End If

            BeReOC = clsLnTrans_re_oc.Get_Single_By_IdOrdenCompraEnc_And_IdRecepcionEnc(IdOrdenCompraEnc,
                                                                                        IdRecepcionEnc)

            If BeReOC Is Nothing Then
                Throw New Exception("ERROR_202310310532: No se obtuvo el objeto de la recepción de compra para el IdRecepcionEnc: " & IdRecepcionEnc)
            End If

            vCodigoBodegaDestino = clsLnBodega.Get_Codigo_By_IdBodega(IdBodega)


            If vCodigoBodegaDestino = "" Then
                Throw New Exception("ERROR_202310310533: No se obtuvo el código de bodega destino sap para el IdBodega: " & IdBodega & " Bodega.Codigo=(vacio/no válido)")
            End If

            Get_Objetos_Documento_Ingreso = True

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function Configuracion_Interface_Correcta(ByVal BeINavConfigEnc As clsBeI_nav_config_enc) As Boolean

        Configuracion_Interface_Correcta = False

        Dim BeEstadoProductoBueno As New clsBeProducto_estado
        Dim BeBodegaUbicacion As New clsBeBodega_ubicacion

        Try

            If BeINavConfigEnc Is Nothing Then
                Throw New Exception("ERROR_20231031: No está definida la configuración de interface")
            Else

                If BeINavConfigEnc.IdProductoEstado = 0 Then
                    Throw New Exception("ERROR_20231031A: El IdProductoEstado (que define el producto en buen estado) no está configurado en la interface")
                Else

                    BeEstadoProductoBueno = clsLnProducto_estado.Get_Single_By_IdEstado(BeINavConfigEnc.IdProductoEstado)

                    If BeEstadoProductoBueno Is Nothing Then
                        Throw New Exception("ERROR_20231031B: No se obtuvo el objeto de estado para el BeConfigEnc.IdProductoEstado: " & BeConfigEnc.IdProductoEstado)
                    End If

                End If

            End If

            If BeINavConfigEnc.IdProductoEstado_NC <> 0 Then

                BeEstadoProductoBueno = clsLnProducto_estado.Get_Single_By_IdEstado(BeINavConfigEnc.IdProductoEstado_NC)

                If BeEstadoProductoBueno Is Nothing Then
                    Throw New Exception("ERROR_CONFIGURACION_20231025: No se encontró el estado de producto por defecto para la gestión de inventario teórico en N.C.")
                End If

            Else
                Throw New Exception("ERROR_CONFIGURACION_20231025A: No se encontró el estado de producto por defecto para la gestión de inventario teórico en N.C.")
            End If

            Configuracion_Interface_Correcta = True

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Function Enviar_Entrada_Mercancia_Teorica_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                             ByVal BeProductoEstado_NC As clsBeProducto_estado,
                                                             ByVal _Docentry As Integer,
                                                             ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                             ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                             ByVal vCodigoBodegaDestino As String,
                                                             ByRef lblprg As RichTextBox,
                                                             ByRef prg As ProgressBar,
                                                             ByVal oCompany As Company,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As Boolean


        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim BeProducto As New clsBeProducto()
        Dim vControlVencimiento As Boolean = False
        Dim BeLineaDetalleOC As New clsBeTrans_oc_det
        Dim lDetalleOC As New List(Of clsBeTrans_oc_det)
        Dim vContadorLote As Integer = 0
        Dim vCorrelativoInternalSerialNumber As String = 0
        Dim vAgregarEntrega As Boolean = False

        Try

            Dim oEntrega As Documents
            Dim oOrderPurchase As Documents

            oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes), Documents)
            oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)

            If oOrderPurchase.GetByKey(_Docentry) Then

                oEntrega.CardCode = oOrderPurchase.CardCode
                oEntrega.DocDate = Date.Today
                oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
                oEntrega.AgentCode = "WMS"

                Dim n As Integer = 0

                lDetalleOC = BeTransOCEnc.DetalleOC

                Dim ListaResumen = (From i In lINav_Transaccioens_Out.Where(Function(x) x.No_pedido = _Docentry)
                                    Group i By Keys = New With {Key i.No_pedido, Key i.No_linea,
                                                                    Key i.Codigo_producto,
                                                                    Key i.Enviado} Into Group
                                    Select New With {Keys.No_pedido,
                                                         Keys.No_linea,
                                                         Keys.Codigo_producto,
                                                         Keys.Enviado,
                                                        .Cantidad = Group.Sum(Function(x) x.Cantidad)})

                For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                    oOrderPurchase.Lines.SetCurrentLine(j)

                    If oOrderPurchase.Lines.LineStatus = BoStatus.bost_Open Then

                        Dim lLotesPorProductoYLinea As New List(Of clsBeI_nav_transacciones_out)
                        lLotesPorProductoYLinea = lINav_Transaccioens_Out.FindAll(Function(x) x.Codigo_producto = oOrderPurchase.Lines.ItemCode.ToString() AndAlso x.No_linea = oOrderPurchase.Lines.LineNum)

                        BeLineaDetalleOC = lDetalleOC.Find(Function(x) x.Codigo_Producto = oOrderPurchase.Lines.ItemCode.ToString() _
                                                               AndAlso x.No_Linea = oOrderPurchase.Lines.LineNum)

                        If Not BeLineaDetalleOC Is Nothing Then

                            If Not lLotesPorProductoYLinea Is Nothing Then

                                Dim vItemCode As String = ""
                                Dim vBaseLineOC As Integer = oOrderPurchase.Lines.LineNum

                                For Each LineaIngresoResumida In ListaResumen

                                    vItemCode = LineaIngresoResumida.Codigo_producto

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format(vbTab & "Procesando Producto: {0} - Cantidad: {1}.",
                                                                        vItemCode,
                                                                        LineaIngresoResumida.Cantidad))

                                    Dim vDiferenciaPorLineaIngreso As Double = Math.Round(BeLineaDetalleOC.Cantidad - BeLineaDetalleOC.Cantidad_recibida, 6)

                                    If vDiferenciaPorLineaIngreso > 0 Then

                                        oEntrega.Lines.SetCurrentLine(n)
                                        oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                                        oEntrega.Lines.BaseEntry = _Docentry
                                        oEntrega.Lines.ItemCode = vItemCode
                                        oEntrega.Lines.BaseLine = vBaseLineOC
                                        oEntrega.Lines.TaxCode = "IVA"
                                        oEntrega.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                                        oEntrega.Lines.Quantity = vDiferenciaPorLineaIngreso
                                        oEntrega.Lines.WarehouseCode = BeINavConfigEnc.Codigo_Bodega_ERP_NC

                                        oEntrega.Lines.Add()
                                        n += 1

                                        Dim BeStockPushAlmacenaje As New clsBeStock
                                        BeStockPushAlmacenaje = New clsBeStock()
                                        BeStockPushAlmacenaje.IdBodega = BeINavConfigEnc.Idbodega
                                        BeStockPushAlmacenaje.IdStock = 0
                                        BeProducto = clsLnProducto.Get_Single_By_Codigo(vItemCode, lConnection, lTransaction)
                                        BeStockPushAlmacenaje.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto, BeINavConfigEnc.Idbodega, lConnection, lTransaction)
                                        BeStockPushAlmacenaje.IdPropietarioBodega = BeINavConfigEnc.IdPropietario
                                        BeStockPushAlmacenaje.IdProductoEstado = BeINavConfigEnc.IdProductoEstado_NC
                                        BeStockPushAlmacenaje.ProductoEstado.IdEstado = BeINavConfigEnc.IdProductoEstado_NC
                                        BeStockPushAlmacenaje.Presentacion.IdPresentacion = 0
                                        BeStockPushAlmacenaje.IdUnidadMedida = BeProducto.IdUnidadMedidaBasica
                                        BeStockPushAlmacenaje.IdUbicacion = BeProductoEstado_NC.IdUbicacionDefecto
                                        BeStockPushAlmacenaje.IdUbicacion_anterior = BeStockPushAlmacenaje.IdUbicacion
                                        If BeProducto.Control_lote Then BeStockPushAlmacenaje.Lote = BeINavConfigEnc.Lote_Defecto_Entrada_NC
                                        If BeProducto.Control_vencimiento Then BeStockPushAlmacenaje.Fecha_vence = New Date(2026, 7, 1)
                                        BeStockPushAlmacenaje.Fecha_Ingreso = Now
                                        BeStockPushAlmacenaje.Cantidad = 1
                                        BeStockPushAlmacenaje.Activo = True
                                        BeStockPushAlmacenaje.Peso = 0
                                        BeStockPushAlmacenaje.Temperatura = 0
                                        BeStockPushAlmacenaje.Fec_agr = Now
                                        BeStockPushAlmacenaje.Fec_mod = Now
                                        BeStockPushAlmacenaje.User_agr = BeINavConfigEnc.IdUsuario
                                        BeStockPushAlmacenaje.User_mod = BeINavConfigEnc.IdUsuario
                                        BeStockPushAlmacenaje.Pallet_No_Estandar = False
                                        BeStockPushAlmacenaje.Lic_plate = FormatoFechas.tFechaHora(Now)
                                        clsLnStock.Insertar(BeStockPushAlmacenaje, lConnection, lTransaction)

                                        vAgregarEntrega = True

                                    End If

                                Next

                            End If

                        Else
                            Throw New Exception("ERROR_202310310600: No se encontrò el objeto relacionado con la lìnea.")
                        End If

                    End If

                Next

                If vAgregarEntrega Then

                    If oEntrega.Lines.Count > 0 Then

                        Dim oResultado As Integer
                        oResultado = oEntrega.Add()

                        If oResultado <> 0 Then
                            Dim sErrMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception("#ERROR_SAP_202309270131: " & sErrMsg)
                        Else

                            Dim vTrasladoDocEntry As Integer = 0
                            Dim newObjectCode As String = ""
                            oCompany.GetNewObjectCode(newObjectCode)

                            If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                                Throw New Exception("No se pudo obtener el DocEntry del documento generado.")
                            End If

                            Dim vDocNum As String = ""

                            Try
                                Dim vCreatedoEntrega As Documents = Nothing
                                vCreatedoEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)

                                If vCreatedoEntrega.GetByKey(vTrasladoDocEntry) Then
                                    vDocNum = vCreatedoEntrega.DocNum
                                    clsPublic.Actualizar_Progreso(lblprg, "Se generó la entrega de mercancía teórica: " & vDocNum & " en SAP.")
                                End If
                            Catch ex As Exception
                                clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                            End Try

                            Enviar_Entrada_Mercancia_Teorica_SAP = True

                        End If

                    Else

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay líneas pendientes para generar entrega de documento: {0}", _Docentry))

                    End If

                End If

            End If

            Return True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                          "",
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet)
            Throw

        End Try

    End Function
    Private Shared Sub Configurar_Entrega_Compra(oCompany As Company, ByRef oEntrega As Documents, oOrderPurchase As Documents, pFechaRecepcionWMS As Date)
        oEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)
        oEntrega.CardCode = oOrderPurchase.CardCode
        oEntrega.DocDate = pFechaRecepcionWMS
        oEntrega.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
        oEntrega.DocCurrency = oOrderPurchase.DocCurrency
    End Sub

    Private Shared Function Procesar_Detalle_Ingreso(ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                      ByVal _Docentry As Integer,
                                                      ByVal oOrderPurchase As Documents,
                                                      ByVal pTipoDocumento As tTipoDocumentoIngreso,
                                                      ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                      ByRef lblPrg As RichTextBox,
                                                      ByVal pCompany As pEmpresa,
                                                      ByRef oCompany As Company,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Boolean

        Dim oResultado As Integer = 0
        Dim oEntrega As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim esIngresoImportacion As Boolean = False
        Dim BeProducto As New clsBeProducto
        Dim vCodigoProductoWMS As String = ""

        Procesar_Detalle_Ingreso = False

        Try

            esIngresoImportacion = (pTipoDocumento = tTipoDocumentoIngreso.Ingreso_importación)

            If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
               pTipoDocumento = tTipoDocumentoIngreso.Ingreso_importación Then
                Configurar_Entrega_Compra(oCompany,
                                          oEntrega,
                                          oOrderPurchase,
                                          lINavTransaccionesOut.FirstOrDefault.Fecha_recepcion())
            End If

            If oEntrega Is Nothing Then
                Throw New Exception("No se pudo crear el objeto de entrega.")
            End If

            NoLineaEntrega = 0 : NoLineaEntregaLote = 0

            For j As Integer = 0 To oOrderPurchase.Lines.Count - 1

                oOrderPurchase.Lines.SetCurrentLine(j)

                Dim vCodigoProductoSAP As String = oOrderPurchase.Lines.ItemCode.ToString()
                Dim vNoLineaOCSAP As Integer = oOrderPurchase.Lines.LineNum

                Select Case pCompany

                    Case pEmpresa.Killios
                        BeProducto = clsLnProducto.Get_Single_By_NoParte(vCodigoProductoSAP,
                                                                        lConnection,
                                                                        lTransaction)

                        If Not BeProducto Is Nothing Then
                            vCodigoProductoWMS = BeProducto.Codigo
                        Else
                            Throw New Exception("No se obtuvo el producto WMS para el NoParte (Killios): " & vCodigoProductoSAP)
                        End If

                    Case pEmpresa.Garesa
                        BeProducto = clsLnProducto.Get_Single_By_NoSerie(vCodigoProductoSAP,
                                                                        lConnection,
                                                                        lTransaction)

                        If Not BeProducto Is Nothing Then
                            vCodigoProductoWMS = BeProducto.Codigo
                        Else
                            Throw New Exception("No se obtuvo el producto WMS para el NoSerie (Garesa): " & vCodigoProductoSAP)
                        End If

                End Select

                Dim DistinctProductosLineas = lINavTransaccionesOut.
                Where(Function(x) x.Codigo_producto = vCodigoProductoWMS AndAlso x.No_linea = vNoLineaOCSAP).
                GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                Select(Function(g) New With {
                    g.Key.Codigo_producto,
                    g.Key.No_linea,
                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                }).ToList()

                If DistinctProductosLineas.Any() Then

                    Dim nuevaLineaEntrega As Boolean = True

                    For Each ProductoIngreso In DistinctProductosLineas

                        nuevaLineaEntrega = (vCodigoAnterior <> ProductoIngreso.Codigo_producto OrElse vNoLineaAnterior <> ProductoIngreso.No_linea)

                        If nuevaLineaEntrega Then

                            Dim vTipoImpuesto As String = oOrderPurchase.Lines.TaxCode

                            oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                            oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_PurchaseOrder)
                            oEntrega.Lines.BaseEntry = oOrderPurchase.DocEntry
                            oEntrega.Lines.ItemCode = vCodigoProductoSAP
                            oEntrega.Lines.BaseLine = vNoLineaOCSAP
                            oEntrega.Lines.TaxCode = vTipoImpuesto
                            oEntrega.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                            oEntrega.Lines.Quantity = ProductoIngreso.Cantidad_Total

                            If esIngresoImportacion Then

                                If pCompany = pEmpresa.Killios Then
                                    If BeConfigEnc.Bodega_Prorrateo1 <> "" Then
                                        oEntrega.Lines.WarehouseCode = BeConfigEnc.Bodega_Prorrateo1
                                    Else
                                        Throw New Exception("No está definida la bodega de prorrateo para la bodega " & BeConfigEnc.Idbodega & " no se puedo continuar con el envío del documento ")
                                    End If
                                ElseIf pCompany = pEmpresa.Garesa Then
                                    If BeConfigEnc.Bodega_Prorrateo <> "" Then
                                        oEntrega.Lines.WarehouseCode = BeConfigEnc.Bodega_Prorrateo
                                    Else
                                        Throw New Exception("No está definida la bodega de prorrateo para la bodega " & BeConfigEnc.Idbodega & " no se puedo continuar con el envío del documento ")
                                    End If
                                End If

                                '#EJC20250616: Copiar gastos (expenses) desde el documento base
                                For k As Integer = 0 To oOrderPurchase.Expenses.Count - 1
                                    oOrderPurchase.Expenses.SetCurrentLine(k)
                                    oEntrega.Expenses.ExpenseCode = oOrderPurchase.Expenses.ExpenseCode
                                    oEntrega.Expenses.LineTotal = oOrderPurchase.Expenses.LineTotal
                                    oEntrega.Expenses.Add()
                                Next

                            End If

                            vCodigoAnterior = oEntrega.Lines.ItemCode
                            vNoLineaAnterior = oEntrega.Lines.LineNum

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oOrderPurchase.DocEntry _
                                                                                  AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                  AndAlso x.Codigo_producto = vCodigoProductoWMS _
                                                                                  AndAlso x.Enviado = False)

                            If Sublista_A_Actualizar IsNot Nothing AndAlso Sublista_A_Actualizar.Count > 0 Then
                                Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                            End If

                            oEntrega.Lines.Add() : NoLineaEntrega += 1

                        End If

                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True
                Else
                    clsPublic.Actualizar_Progreso(lblPrg, "No se obtuverion registros de ingreso para el código de SAP: " & vCodigoProductoSAP)
                End If

            Next

            If vAgregarEntrega Then

                Dim vMensaje As String = "Entrada de mercancía basada en pedido de compra SAP: " & oOrderPurchase.DocNum & " Documento WMS: " & beTransOCEnc.IdOrdenCompraEnc
                oEntrega.Comments = vMensaje

                oResultado = oEntrega.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}: Documento: {oOrderPurchase.DocNum}")
                Else

                    Dim vTrasladoDocEntry As Integer = 0
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    Dim vDocNum As String = ""

                    Try
                        Dim vCreatedoEntrega As Documents = Nothing
                        vCreatedoEntrega = oCompany.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes)

                        If vCreatedoEntrega.GetByKey(vTrasladoDocEntry) Then
                            vDocNum = vCreatedoEntrega.DocNum
                        End If
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, ex.Message)
                    End Try

                    vRegistroEntregaPorLote = True

                    If Lista_A_Actualizar IsNot Nothing AndAlso Lista_A_Actualizar.Count > 0 Then
                        For Each T In Lista_A_Actualizar
                            T.Enviado = True
                            T.no_documento_salida_ref_devol = vTrasladoDocEntry
                            T.Fec_mod = Now
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T, lConnection, lTransaction)
                        Next
                    End If

                    Procesar_Detalle_Ingreso = True

                    clsLnTrans_oc_enc.Actualizar_No_Documento_Recepcion_ERP(vDocNum,
                                                                            Lista_A_Actualizar.FirstOrDefault.Idordencompra,
                                                                            lConnection,
                                                                            lTransaction)

                    clsPublic.Actualizar_Progreso(lblPrg, vbTab & vbTab & " ✔️ Se generó la entrega: " & vDocNum & " Documento SAP: " & oOrderPurchase.DocNum & " IdOrdenCompraEnc_WMS: " & beTransOCEnc.IdOrdenCompraEnc & " - " & beTransOCEnc.Codigo_Empresa_ERP.ToString())

                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function Procesar_Detalle_Ingreso_Devolucion(ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                                 ByVal oReturnRequest As Documents,
                                                                 ByVal pTipoDocumento As tTipoDocumentoIngreso,
                                                                 ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                 ByVal _Docentry As Integer,
                                                                 ByRef lblPrg As RichTextBox,
                                                                 ByVal pCompany As pEmpresa,
                                                                 ByVal oCompany As Company,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Boolean

        Dim oResultado As Integer = 0
        Dim oReturn As Documents = Nothing
        Dim vRegistroEntregaPorLote As Boolean = False
        Dim NoLineaEntrega As Integer = 0 : Dim NoLineaEntregaLote As Integer = 0
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim vAgregarEntrega As Boolean = False

        Procesar_Detalle_Ingreso_Devolucion = False

        Try

            Dim vDocEntrySolicitud As Integer = lINavTransaccionesOut.FirstOrDefault.No_pedido
            Dim BeProducto As New clsBeProducto

            Configurar_Devolucion_Cliente(oCompany, oReturn, oReturnRequest, vDocEntrySolicitud)

            If oReturn Is Nothing Then
                Throw New Exception("No se pudo crear el objeto de entrega.")
            End If

            NoLineaEntrega = 0 : NoLineaEntregaLote = 0

            Dim vCodigoProductoWMS As String = ""

            For j As Integer = 0 To oReturnRequest.Lines.Count - 1

                oReturnRequest.Lines.SetCurrentLine(j)

                Dim vCodigoProductoSAP As String = oReturnRequest.Lines.ItemCode.ToString()
                Dim vNoLineaOCSAP As Integer = oReturnRequest.Lines.LineNum
                Dim nuevaLineaEntrega As Boolean = False

                Select Case pCompany

                    Case pEmpresa.Killios

                        BeProducto = clsLnProducto.Get_Single_By_NoParte(vCodigoProductoSAP,
                                                                        lConnection,
                                                                        lTransaction)

                    Case pEmpresa.Garesa

                        BeProducto = clsLnProducto.Get_Single_By_NoSerie(vCodigoProductoSAP,
                                                                        lConnection,
                                                                        lTransaction)

                End Select

                If Not BeProducto Is Nothing Then

                    vCodigoProductoWMS = BeProducto.Codigo

                    Dim DistinctProductosLineas = lINavTransaccionesOut.
                    Where(Function(x) x.Codigo_producto = vCodigoProductoWMS AndAlso x.No_linea = vNoLineaOCSAP).
                    GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                    Select(Function(g) New With {
                        g.Key.Codigo_producto,
                        g.Key.No_linea,
                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                    }).ToList()

                    If DistinctProductosLineas.Any() Then

                        For Each ProductoIngreso In DistinctProductosLineas

                            nuevaLineaEntrega = (vCodigoAnterior <> ProductoIngreso.Codigo_producto OrElse vNoLineaAnterior <> ProductoIngreso.No_linea)

                            If nuevaLineaEntrega Then

                                Dim vTipoImpuesto As String = oReturnRequest.Lines.TaxCode
                                oReturn.Lines.SetCurrentLine(NoLineaEntrega)
                                oReturn.Lines.BaseType = BoObjectTypes.oReturnRequest
                                oReturn.Lines.BaseEntry = oReturnRequest.DocEntry
                                oReturn.Lines.BaseLine = vNoLineaOCSAP
                                oReturn.Lines.ItemCode = vCodigoProductoSAP
                                oReturn.Lines.TaxCode = vTipoImpuesto
                                oReturn.Lines.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                                oReturn.Lines.Quantity = ProductoIngreso.Cantidad_Total

                                vCodigoAnterior = oReturn.Lines.ItemCode
                                vNoLineaAnterior = oReturn.Lines.LineNum

                                Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = oReturnRequest.DocEntry _
                                                                                      AndAlso x.No_linea = vNoLineaOCSAP _
                                                                                      AndAlso x.Codigo_producto = vCodigoProductoWMS _
                                                                                      AndAlso x.Enviado = False)

                                If Sublista_A_Actualizar IsNot Nothing AndAlso Sublista_A_Actualizar.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                End If

                                oReturn.Lines.Add() : NoLineaEntrega += 1

                            End If

                        Next 'DistinctProductosLineas

                        vAgregarEntrega = True

                    Else
                        clsPublic.Actualizar_Progreso(lblPrg, "No se encontraron registros de recepción para el código de producto SAP: " & vCodigoProductoSAP)
                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblPrg, "#ERROR_20250408: No se pudo obtener el código de producto para la empresa: " & pCompany.ToString() & " con el identificador: " & vCodigoProductoSAP)
                End If

            Next

            If vAgregarEntrega Then

                oResultado = oReturn.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else
                    vRegistroEntregaPorLote = True

                    If Lista_A_Actualizar IsNot Nothing AndAlso Lista_A_Actualizar.Count > 0 Then
                        For Each T In Lista_A_Actualizar
                            T.Enviado = True
                            T.Fec_mod = Now
                            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(T, lConnection, lTransaction)
                        Next
                    End If

                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)
                    Dim vTrasladoDocEntry As Integer = 0

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    Dim vDocNum As String = ""

                    Try
                        Dim vCreatedoEntrega As Documents = Nothing
                        vCreatedoEntrega = oCompany.GetBusinessObject(BoObjectTypes.oReturns)

                        If vCreatedoEntrega.GetByKey(vTrasladoDocEntry) Then
                            vDocNum = vCreatedoEntrega.DocNum
                            clsPublic.Actualizar_Progreso(lblPrg, vbTab & vbTab & " ✔️ Se creó la devolución con DocNum: " & vDocNum & " para documento SAP: " & beTransOCEnc.No_Documento & " IdOrdenCompraEnc: " & beTransOCEnc.IdOrdenCompraEnc)
                        End If
                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblPrg, ex.Message)
                    End Try

                    Procesar_Detalle_Ingreso_Devolucion = True

                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Sub Configurar_Devolucion_Cliente(ByVal oCompany As Company, ByRef oReturn As Documents, oReturnRequest As Documents, _Docentry As Integer)
        oReturn = oCompany.GetBusinessObject(BoObjectTypes.oReturns)
        oReturn.CardCode = oReturnRequest.CardCode
        oReturn.DocDate = Date.Today
        oReturn.DocCurrency = oReturnRequest.DocCurrency
        oReturn.DocObjectCode = BoObjectTypes.oReturns
    End Sub

    Private Shared Function Enviar_Entrada_Mercancia_Sin_Estado_SAP_B1(ByVal beINavConfigEnc As clsBeI_nav_config_enc,
                                                                       ByVal _Docentry As Integer,
                                                                       ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                       ByVal beTransOCEnc As clsBeTrans_oc_enc,
                                                                       ByVal codigoBodegaDestino As String,
                                                                       ByRef lblPrg As RichTextBox,
                                                                       ByRef prg As Windows.Forms.ProgressBar,
                                                                       ByVal oCompany As Company,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Value = 0
        prg.Visible = True

        Dim oOrderPurchase As Documents = Nothing
        Dim result As Boolean = False
        Dim NoLineaSAPEntrega As Integer = 0
        Dim vEmpresa As pEmpresa

        Try

            vEmpresa = CType([Enum].Parse(GetType(pEmpresa), beTransOCEnc.Codigo_Empresa_ERP), pEmpresa)

            Select Case beTransOCEnc.TipoIngreso.IdTipoIngresoOC

                Case tTipoDocumentoIngreso.Ingreso, tTipoDocumentoIngreso.Ingreso_importación

                    oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)
                    If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then

                        result = Procesar_Detalle_Ingreso(beTransOCEnc,
                                                          _Docentry,
                                                          oOrderPurchase,
                                                          beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                          lINavTransaccionesOut,
                                                          lblPrg,
                                                          vEmpresa,
                                                          oCompany,
                                                          lConnection,
                                                          lTransaction)

                    Else
                        clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                    End If

                Case tTipoDocumentoIngreso.Devolucion

                    oOrderPurchase = DirectCast(oCompany.GetBusinessObject(BoObjectTypes.oReturnRequest), Documents)

                    If oOrderPurchase IsNot Nothing AndAlso oOrderPurchase.GetByKey(_Docentry) Then
                        result = Procesar_Detalle_Ingreso_Devolucion(beTransOCEnc,
                                                                     oOrderPurchase,
                                                                    beTransOCEnc.TipoIngreso.IdTipoIngresoOC,
                                                                    lINavTransaccionesOut,
                                                                    _Docentry,
                                                                    lblPrg,
                                                                    vEmpresa,
                                                                    oCompany,
                                                                    lConnection,
                                                                    lTransaction)
                    Else
                        clsPublic.Actualizar_Progreso(lblPrg, "ERROR_202311212019: No se pudo obtener el documento de devolución de sap para el _Docentry: " & _Docentry)
                    End If

            End Select

        Catch ex As Exception
            Throw
        Finally
            prg.Value = 0
            prg.Visible = False
        End Try

        Return result

    End Function

    Private Shared Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                       ByVal oCompany As Company) As Boolean

        Dim conn As SapConnectionWrapper = Nothing
        Marcar_PI_Sincronizado_SAP = False

        Try

            ' Escapar el número de documento por seguridad
            Dim safeNoDoc As String = pNoDocumento.Replace("'", "''")
            Dim sQuery As String = $"UPDATE OPOR SET U_Enviado_WMS = '1' WHERE DocEntry = '{safeNoDoc}'"

            ' Ejecutar consulta
            Dim oRecordset As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            oRecordset.DoQuery(sQuery)

            Marcar_PI_Sincronizado_SAP = True

        Catch ex As Exception
            Throw New Exception("Error al marcar PI como sincronizado en SAP: " & ex.Message, ex)
        End Try

    End Function

    Public Shared Function Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                                ByRef prg As ProgressBar,
                                                                                ByRef cnnLog As SqlConnection,
                                                                                Optional ByVal pNoDocumentoSAP As String = "") As Boolean
        Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResult As String = ""
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
        Dim connK As SapConnectionWrapper = Nothing
        Dim connG As SapConnectionWrapper = Nothing

        Try

            connK = sapPool.GetConnection(pEmpresa.Killios)
            Dim oCompanyK As Company = connK.Company

            connG = sapPool.GetConnection(pEmpresa.Garesa)
            Dim oCompanyG As Company = connG.Company

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento en tabla intermedia TOMWMS.")

            ' Obtener pedidos de ambas empresas
            Dim pedidosKillios = Get_Pedidos_Compra_From_SAP(oCompanyK, True, pNoDocumentoSAP, pEmpresa.Killios)
            Dim pedidosGaresa = Get_Pedidos_Compra_From_SAP(oCompanyG, True, pNoDocumentoSAP, pEmpresa.Garesa)

            BeNavEjecucionRes.Registros_ws = pedidosKillios.Count + pedidosGaresa.Count
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            clsPublic.Actualizar_Progreso(lblprg, vbTab & $"Pedidos de compra en relación con SAP (OPOR): {pedidosKillios.Count}")
            prg.Maximum = pedidosKillios.Count + pedidosGaresa.Count

            ' Iniciar transacción
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ' Obtener configuración
            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

            Try
                Procesar_Pedidos_Compra(lblprg, pedidosKillios, pEmpresa.Killios, cnnLog, lTransaction, vResult, BePedidoCompraEnc, oCompanyK)
            Catch ex As Exception
                clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            End Try

            Try
                Procesar_Pedidos_Compra(lblprg, pedidosGaresa, pEmpresa.Garesa, cnnLog, lTransaction, vResult, BePedidoCompraEnc, oCompanyG)
            Catch ex As Exception
                clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            End Try

            lTransaction.Commit()

            Importar_Pedidos_Compra_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al insertar Ordenes de Compra desde SAP a intermedia:{vbNewLine}{ex.Message}")
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If connK IsNot Nothing Then sapPool.ReleaseConnection(connK)
            If connG IsNot Nothing Then sapPool.ReleaseConnection(connG)
            prg.Value = 0
        End Try

    End Function

    Private Shared Sub Procesar_Pedidos_Compra(ByVal lblprg As RichTextBox,
                                                ByVal pedidos As List(Of clsBeI_nav_ped_compra_enc),
                                                ByVal empresa As pEmpresa,
                                                ByVal cnnLog As SqlConnection,
                                                ByVal lTransaction As SqlTransaction,
                                                ByRef vResult As String,
                                                ByRef BePedidoCompraEnc As clsBeTrans_oc_enc,
                                                ByVal oCompany As Company)

        For Each pedido In pedidos

            clsPublic.Actualizar_Progreso(lblprg, vbTab & $"Procesando Pedido Compra: {pedido.No} - {pedido.Vendor_Invoice_No} - {empresa.ToString()}")

            Dim codProveedorWMS As String = empresa.ToString().Substring(0, 1) & pedido.Buy_From_Vendor_No

            ' Verificar si el proveedor ya existe
            If Not clsLnProveedor.Existe_By_Codigo_And_Company(codProveedorWMS, pedido.Company_Code) Then
                If Inserta_Proveedor_Desde_SAP(pedido.Buy_From_Vendor_No, cnnLog, empresa, oCompany) Then
                    clsPublic.Actualizar_Progreso(lblprg, vbTab & $"El proveedor: {codProveedorWMS} no existía en WMS y fue insertado.")
                End If
            End If

            ' Procesar pedido y marcar como sincronizado
            If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(pedido, BePedidoCompraEnc, vResult) Then
                Marcar_PI_Sincronizado_SAP(pedido.No, oCompany)
            End If

            clsPublic.Actualizar_Progreso(lblprg, vResult)

        Next

    End Sub

    Private Shared Function Get_Pedidos_Compra_From_SAP(ByVal oCompany As Company,
                                                        Optional ByVal AplicarFiltros As Boolean = True,
                                                        Optional ByVal pNoDocumentoSAP As String = "",
                                                        Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_ped_compra_enc)

        Dim lPedidosCompra As New List(Of clsBeI_nav_ped_compra_enc)

        Try

            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim queryEnc As String = Construir_Query_Pedidos_Compra(pNoDocumentoSAP)
            RsEnc.DoQuery(queryEnc)

            While Not RsEnc.EoF
                Dim pedido = Construir_Encabezado_Pedido(RsEnc, pCompany)
                pedido.Lineas_Detalle = Obtener_Lineas_Pedido(oCompany, pedido.No)
                lPedidosCompra.Add(pedido)
                RsEnc.MoveNext()
            End While

        Catch ex As Exception
            Throw
        End Try

        Return lPedidosCompra
    End Function

    Private Shared Function Construir_Query_Pedidos_Compra(ByVal pNoDocumentoSAP As String) As String
        Return " SELECT T0.DOCENTRY, T0.DOCNUM, T0.DOCDATE, T0.CARDCODE, T0.CARDNAME, T0.DOCCUR, " &
           "T0.DOCTOTAL, T0.JRNLMEMO, T0.CANCELED, T0.DOCSTATUS, " &
           "CASE WHEN T0.DOCTYPE = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA, " &
           "(SELECT TOP 1 D0.WhsCode FROM POR1 D0 INNER JOIN OWHS D1 ON D1.WhsCode = D0.WhsCode WHERE D0.DocEntry = T0.DOCENTRY) AS BODEGA, " &
           "T0.COMMENTS, T0.NumAtCard, T0.Series, T1.MailCountr, T0.U_NoContenedor " &
           "FROM OPOR T0 INNER JOIN OCRD T1 ON T0.CARDCODE = T1.CARDCODE " &
           "WHERE T0.DOCSTATUS = 'O' AND T0.CANCELED = 'N' AND T0.CreateDate >= '2020-10-09 00:00:00.000' " &
           "AND T0.U_Enviado_WMS = 2 " &
           IIf(pNoDocumentoSAP <> "", " And T0.DocNum = " & pNoDocumentoSAP, "") & " ORDER BY T0.DocEntry DESC"
    End Function

    Private Shared Function Construir_Encabezado_Pedido(Rs As Recordset, pCompany As pEmpresa) As clsBeI_nav_ped_compra_enc
        Dim BePedido As New clsBeI_nav_ped_compra_enc()
        With BePedido
            .No = Rs.Fields.Item("DOCENTRY").Value
            .Posting_Date = CDate(Rs.Fields.Item("DOCDATE").Value)
            .Order_Date = .Posting_Date
            .Document_Date = .Posting_Date
            .Expected_Receipt_Date = .Posting_Date
            .Status = 1
            .Buy_From_Vendor_No = Rs.Fields.Item("CARDCODE").Value.ToString()
            .Buy_From_Vendor_Name = Rs.Fields.Item("CARDNAME").Value.ToString()
            .Is_Internal_Transfer = False
            .Location_Code = Rs.Fields.Item("BODEGA").Value.ToString()
            .Vendor_Invoice_No = If(Rs.Fields.Item("DOCNUM").Value.ToString() <> "", Rs.Fields.Item("DOCNUM").Value.ToString(), .No.ToString())
            .Posting_Description = Rs.Fields.Item("COMMENTS").Value.ToString()
            .Product_Owner_Code = BeConfigEnc.IdPropietario
            .Company_Code = pCompany.ToString()
            .Ship_To_Contact = Rs.Fields.Item("U_NoContenedor").Value.ToString()
            Dim pais As String = Rs.Fields.Item("MailCountr").Value.ToString()
            .Document_Type = If(pais <> "GT", tTipoDocumentoIngreso.Ingreso_importación, tTipoDocumentoIngreso.Ingreso)
        End With
        Return BePedido
    End Function

    Private Shared Function Obtener_Lineas_Pedido(oCompany As Company, docEntry As Integer) As List(Of clsBeI_nav_ped_compra_det)
        Dim lista As New List(Of clsBeI_nav_ped_compra_det)
        Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

        Dim query As String = "SELECT T1.U_CodWMS ItemCode, T0.DSCRIPTION, T0.QUANTITY, T0.PRICE, T0.LINETOTAL, " &
                          "T0.VATSUM, T0.DOCENTRY, T0.WHSCODE, T0.OPENCREQTY As CANTIDAD_PENDIENTE, T0.BASELINE, T0.LINENUM, " &
                          "T0.UomCode as UNIDAD_MEDIDA, T2.UomName " &
                          "FROM POR1 T0 " &
                          "INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode " &
                          "INNER JOIN UGP1 U1 ON T1.IUoMEntry = U1.UomEntry " &
                          "INNER JOIN OUGP U0 ON U1.UgpEntry = U0.UgpEntry " &
                          "INNER JOIN OUOM T2 ON U1.UomEntry = T2.UomEntry " &
                          "WHERE T0.DOCENTRY = '" & docEntry & "' AND T0.LINESTATUS = 'O'"

        RsDet.DoQuery(query)

        While Not RsDet.EoF
            Dim det As New clsBeI_nav_ped_compra_det()
            det.NoEnc = RsDet.Fields.Item("DOCENTRY").Value.ToString()
            det.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
            det.Line_No = CInt(RsDet.Fields.Item("LINENUM").Value)
            det.Planed_Receipt_Date = Now
            det.Quantity = CDec(RsDet.Fields.Item("CANTIDAD_PENDIENTE").Value)
            det.Quantity_Received = 0
            det.Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
            det.Unit_of_Measure_Code = "UN"
            det.Type = 2
            det.Variant_Code = If(RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString() = "Unidad", Nothing, RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString())
            det.Location_Code = RsDet.Fields.Item("WHSCODE").Value.ToString()
            lista.Add(det)
            RsDet.MoveNext()
        End While

        Return lista
    End Function

    Public Shared Sub Enviar_Transacciones_De_Ingreso_SAP(ByRef lblprg As RichTextBox,
                                                           ByRef prg As ProgressBar)

        Dim transaccionesPendientes = clsLnI_nav_transacciones_out.Get_Lotes_Ingreso_Pendientes_Envio()
        If transaccionesPendientes Is Nothing OrElse transaccionesPendientes.Count = 0 Then
            clsPublic.Actualizar_Progreso(lblprg, "🚫 No hay registros de ingreso para envío a SAP.")
            Exit Sub
        End If

        Dim pedidosPorDocumento = Agrupar_Transacciones_Por_Pedido(transaccionesPendientes)
        clsPublic.Actualizar_Progreso(lblprg, $"Documentos a enviar: {pedidosPorDocumento.Count}")

        Dim beConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

        If Not Configuracion_Interface_Correcta(beConfigEnc) Then Exit Sub

        prg.Visible = True

        For Each docIngreso In pedidosPorDocumento
            Procesar_Documento_De_Ingreso(docIngreso, transaccionesPendientes, beConfigEnc, lblprg, prg)
        Next

        clsPublic.Actualizar_Progreso(lblprg, $"Fin del proceso: {Now}")

        prg.Value = 0 : prg.Visible = False

    End Sub

    Private Shared Function Agrupar_Transacciones_Por_Pedido(transacciones As List(Of clsBeI_nav_transacciones_out)) _
        As IEnumerable(Of Object)

        Return From i In transacciones
               Group i By Keys = New With {
               Key i.No_pedido,
               Key i.Idordencompra,
               Key i.Idrecepcionenc,
               Key i.Idbodega
           } Into Group
               Select Keys
    End Function

    Private Shared Sub Procesar_Documento_De_Ingreso(doc As Object,
                                                      transacciones As List(Of clsBeI_nav_transacciones_out),
                                                      config As clsBeI_nav_config_enc,
                                                      lblprg As RichTextBox,
                                                      ByRef prg As ProgressBar)

        Dim beOCEnc As New clsBeTrans_oc_enc
        Dim beREOC As New clsBeTrans_re_oc
        Dim vBodegaDestino As String = ""
        Dim clsTrans As New clsTransaccion
        Dim clsTransSAP As clsSapTransaction = Nothing
        Dim conn As SapConnectionWrapper = Nothing
        Dim oCompany As Company = Nothing

        Try

            If Not Get_Objetos_Documento_Ingreso(doc.Idordencompra,
                                                 doc.Idrecepcionenc,
                                                 doc.Idbodega,
                                                 beOCEnc,
                                                 beREOC,
                                                 vBodegaDestino) Then Exit Sub

            If String.IsNullOrEmpty(beOCEnc.Codigo_Empresa_ERP) Then
                clsPublic.Actualizar_Progreso(lblprg, $"⚠️ Documento sin sociedad definida. IdOC: {beOCEnc.IdOrdenCompraEnc}")
                Marcar_Como_Enviado_Local_Si_Aplica(transacciones, doc.No_pedido, clsTrans)
                Exit Sub
            End If

            Dim empresa = CType([Enum].Parse(GetType(pEmpresa), beOCEnc.Codigo_Empresa_ERP), pEmpresa)
            conn = sapPool.GetConnection(empresa)
            oCompany = conn.Company

            clsTransSAP = New clsSapTransaction(oCompany)
            clsTransSAP.BeginTransaction()
            clsTrans.Begin_Transaction()

            clsPublic.Actualizar_Progreso(lblprg, $"Procesando documento: {beOCEnc.No_Documento} ERP: {beOCEnc.Codigo_Empresa_ERP}")

            Dim transacDoc = transacciones.Where(Function(x) x.No_pedido = doc.No_pedido).ToList()

            Select Case beOCEnc.IdTipoIngresoOC
                Case tTipoDocumentoIngreso.Ingreso, tTipoDocumentoIngreso.Devolucion, tTipoDocumentoIngreso.Ingreso_importación
                    Procesar_Entrada_Mercancia(transacDoc, doc, beOCEnc, beREOC, config, lblprg, prg, oCompany, clsTrans)

                Case tTipoDocumentoIngreso.Transferencia_de_Ingreso
                    Procesar_Traslado_Entre_Almacenes(transacDoc, doc, beOCEnc, beREOC, config, lblprg, prg, empresa, oCompany, clsTrans)
            End Select

            clsTransSAP.CommitTransaction()
            clsTrans.Commit_Transaction()

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            If clsTransSAP IsNot Nothing Then clsTransSAP.RollbackTransaction()
            clsPublic.Actualizar_Progreso(lblprg, "❌ " & ex.Message)
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
            clsTrans.Close_Conection()
        End Try

    End Sub

    Private Shared Sub Procesar_Entrada_Mercancia(transacDoc As List(Of clsBeI_nav_transacciones_out),
                                                   doc As Object,
                                                   beOCEnc As clsBeTrans_oc_enc,
                                                   beREOC As clsBeTrans_re_oc,
                                                   config As clsBeI_nav_config_enc,
                                                   lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar,
                                                   oCompany As Company,
                                                   clsTrans As clsTransaccion)

        If Not Enviar_Entrada_Mercancia_OC_SAP(config, doc.No_pedido, transacDoc, beOCEnc, "", lblprg, prg, oCompany, clsTrans.lConnection, clsTrans.lTransaction) Then
            clsPublic.Actualizar_Progreso(lblprg, $"⚠️ No se pudo enviar el pedido {doc.No_pedido} a SAP.")
            Return
        End If

        beREOC.No_docto = "ENV-WMS" & FormatoFechas.tFecha(Now)
        clsLnTrans_re_oc.Actualizar_No_Docto(beREOC, clsTrans.lConnection, clsTrans.lTransaction)
        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(doc.Idordencompra, True, clsTrans.lConnection, clsTrans.lTransaction)
    End Sub
    Private Shared Sub Procesar_Traslado_Entre_Almacenes(transacDoc As List(Of clsBeI_nav_transacciones_out),
                                                          doc As Object,
                                                          beOCEnc As clsBeTrans_oc_enc,
                                                          beREOC As clsBeTrans_re_oc,
                                                          config As clsBeI_nav_config_enc,
                                                          lblprg As RichTextBox,
                                                          ByRef prg As ProgressBar,
                                                          empresa As pEmpresa,
                                                          oCompany As Company,
                                                          clsTrans As clsTransaccion)

        If Not clsSyncSAPSSolicitudTraslado.Enviar_Traslado_Entre_Almacenes_SAP(doc.No_pedido, transacDoc, beOCEnc, lblprg, prg, empresa, oCompany, clsTrans.lConnection, clsTrans.lTransaction) Then
            clsPublic.Actualizar_Progreso(lblprg, $"⚠️ No se pudo enviar traslado {doc.No_pedido}")
            Return
        End If

        beREOC.No_docto = "ENV-WMS" & FormatoFechas.tFecha(Now)
        clsLnTrans_re_oc.Actualizar_No_Docto(beREOC, clsTrans.lConnection, clsTrans.lTransaction)
        clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(doc.Idordencompra, True, clsTrans.lConnection, clsTrans.lTransaction)
    End Sub

    Private Shared Sub Marcar_Como_Enviado_Local_Si_Aplica(transacciones As List(Of clsBeI_nav_transacciones_out),
                                                            noPedido As String,
                                                            clsTrans As clsTransaccion)

        Dim pendientes = transacciones.FindAll(Function(x) x.No_pedido = noPedido)

        For Each t In pendientes
            t.Enviado = True
            t.no_documento_salida_ref_devol = 20250610
            t.Fec_mod = Now
            clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(t, clsTrans.lConnection, clsTrans.lTransaction)
        Next
    End Sub
    Private Shared Function Enviar_Entrada_Mercancia_OC_SAP(ByVal BeINavConfigEnc As clsBeI_nav_config_enc,
                                                            ByVal _Docentry As Integer,
                                                            ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                            ByVal BeTransOCEnc As clsBeTrans_oc_enc,
                                                            ByVal vCodigoBodegaDestino As String,
                                                            ByRef lblprg As RichTextBox,
                                                            ByRef prg As Windows.Forms.ProgressBar,
                                                            ByVal oCompany As Company,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Boolean

        Enviar_Entrada_Mercancia_OC_SAP = False

        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim BeProductoEstado_NC As New clsBeProducto_estado

        Try

            ' Enviar entrada de mercancía en estado bueno
            Dim EnvioEntradaBuenEstado As Boolean = Enviar_Entrada_Mercancia_Sin_Estado_SAP_B1(BeINavConfigEnc,
                                                                                            _Docentry,
                                                                                            lINav_Transaccioens_Out,
                                                                                            BeTransOCEnc,
                                                                                            vCodigoBodegaDestino,
                                                                                            lblprg,
                                                                                            prg,
                                                                                            oCompany,
                                                                                            lConnection,
                                                                                            lTransaction)

            ' Enviar entrada teórica si aplica
            Dim EnvioEntradaTeorico As Boolean = False
            If EnvioEntradaBuenEstado AndAlso BeTransOCEnc.TipoIngreso IsNot Nothing AndAlso BeTransOCEnc.TipoIngreso.Es_Importacion Then

                If BeINavConfigEnc.IdProductoEstado_NC > 0 Then
                    BeProductoEstado_NC = clsLnProducto_estado.GetSingle(BeINavConfigEnc.IdProductoEstado_NC, lConnection, lTransaction)
                End If

                EnvioEntradaTeorico = Enviar_Entrada_Mercancia_Teorica_SAP(BeINavConfigEnc,
                                                                            BeProductoEstado_NC,
                                                                            _Docentry,
                                                                            lINav_Transaccioens_Out,
                                                                            BeTransOCEnc,
                                                                            vCodigoBodegaDestino,
                                                                            lblprg,
                                                                            prg,
                                                                            oCompany,
                                                                            lConnection,
                                                                            lTransaction)
            End If

            ' Actualizar estado en SAP si fue exitoso
            Dim oOrderPurchase As Documents = Nothing

            Select Case BeTransOCEnc.TipoIngreso.IdTipoIngresoOC
                Case tTipoDocumentoIngreso.Ingreso, tTipoDocumentoIngreso.Ingreso_importación
                    oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseOrders), Documents)
                Case tTipoDocumentoIngreso.Devolucion
                    oOrderPurchase = CType(oCompany.GetBusinessObject(BoObjectTypes.oReturnRequest), Documents)
            End Select

            If oOrderPurchase.GetByKey(_Docentry) Then
                If EnvioEntradaBuenEstado OrElse EnvioEntradaTeorico Then
                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(BeTransOCEnc.IdOrdenCompraEnc, True, lConnection, lTransaction)
                    Enviar_Entrada_Mercancia_OC_SAP = True
                Else
                    Throw New Exception("No se pudo determinar el estado del envío de documentos a SAP.")
                End If
            End If

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, ex.Message)
        End Try

    End Function

End Class