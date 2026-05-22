Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI
Public Class clsSyncSAPSPedidoCliente : Inherits clsInterfaceBase

    Private Shared Function Get_Pedidos_Cliente_SAP(Optional ByVal pPedidoCliente As String = "",
                                                    Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePropietario As New clsBePropietarios
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            If BeConfigEnc Is Nothing Then
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
            End If

            BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim queryEnc As String = "SELECT T0.DocEntry, T0.DocNum, T0.DocDate, T0.CardCode AS CARDCODE, T0.CardName AS CARDNAME, " &
                                  "T0.DocCur, T0.DocTotal, T0.JrnlMemo, T0.Canceled, T0.DocStatus, " &
                                  "CASE WHEN T0.DocType = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA, " &
                                  "(SELECT TOP 1 D0.WhsCode FROM RDR1 D0 WHERE D0.DocEntry = T0.DocEntry) AS BODEGA, " &
                                  "T0.Comments, T0.NumAtCard, " &
                                  "ISNULL(T1.Street,'') + ' ' + ISNULL(T1.City,'') + ' ' + ISNULL(T1.U_DiaEntrega,'') + ' ' + ISNULL(T1.U_HorarioEntrega,'') AS Direccion, " &
                                  "T0.U_EsExport FROM ORDR T0 LEFT JOIN CRD1 T1 ON T0.CardCode = T1.CardCode " &
                                  "WHERE T0.DocStatus = 'O' AND T0.CreateDate >= '2024-01-01 00:00:00.000' AND T0.U_Enviado_WMS = 2 AND T1.Address = 'Entrega' "

            If Not String.IsNullOrEmpty(pPedidoCliente) Then
                queryEnc &= " AND T0.DocNum = " & pPedidoCliente
            End If

            queryEnc &= " ORDER BY T0.DocEntry DESC"
            RsEnc.DoQuery(queryEnc)

            While Not RsEnc.EoF
                Dim BePedidoWMS As New clsBeI_nav_ped_traslado_enc With {
                .No = RsEnc.Fields.Item("DOCENTRY").Value,
                .Posting_Date = Now,
                .Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value,
                .Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value,
                .Status = 1,
                .Transfer_from_Code = RsEnc.Fields.Item("BODEGA").Value,
                .Transfer_from_Contact = "MI3_NAME",
                .Transfer_from_Name = "MI3_NAME",
                .Transfer_to_Code = RsEnc.Fields.Item("CARDCODE").Value,
                .Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value,
                .Transfer_to_Name = RsEnc.Fields.Item("CARDNAME").Value,
                .Product_Owner_Code = BePropietario.Codigo,
                .Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value,
                .Company_Code = pCompany.ToString(),
                .Document_Type = tTipoDocumentoSalida.Pedido_De_Venta_NAV,
                .Address = RsEnc.Fields.Item("DIRECCION").Value,
                .Comments = RsEnc.Fields.Item("COMMENTS").Value,
                .IsExport = IIf(RsEnc.Fields.Item("U_EsExport").Value.ToString() = "Y" OrElse RsEnc.Fields.Item("U_EsExport").Value.ToString() = "S", True, False)
            }

                BePedidoWMS.Lineas_Detalle = ObtenerDetallePedido(oCompany, BePedidoWMS.No)
                lPedidosCliente.Add(BePedidoWMS)
                RsEnc.MoveNext()
            End While

            Return lPedidosCliente

        Catch ex As Exception
            'Throw            
            Get_Pedidos_Cliente_SAP = Nothing
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function

    Private Shared Function ObtenerDetallePedido(ByVal oCompany As Company, ByVal docEntry As Integer) As List(Of clsBeI_nav_ped_traslado_det)

        Dim detalles As New List(Of clsBeI_nav_ped_traslado_det)
        Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

        Dim queryDet As String = "SELECT T0.LineNum, T1.U_CodWMS AS ItemCode, T0.DSCRIPTION, T0.QUANTITY, T0.PRICE, T0.LINETOTAL, T0.VATSUM, " &
                              "T0.DOCENTRY, T0.WHSCODE, T0.UomCode AS UNIDAD_MEDIDA FROM RDR1 T0 " &
                              "INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode WHERE T0.DOCENTRY = '" & docEntry & "'"
        RsDet.DoQuery(queryDet)

        While Not RsDet.EoF
            Dim detalle As New clsBeI_nav_ped_traslado_det With {
            .NoEnc = docEntry,
            .No = clsLnI_nav_ped_traslado_det.MaxID() + 1,
            .Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString(),
            .Line_No = RsDet.Fields.Item("LINENUM").Value.ToString(),
            .Shipment_Date = Date.Now,
            .Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value),
            .Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString(),
            .Unit_of_Measure_Code = "UN",
            .Status = 1,
            .Variant_Code = If(RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString() = "Unidad", Nothing, RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()),
            .Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString(),
            .Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
        }
            detalles.Add(detalle)
            RsDet.MoveNext()
        End While

        Return detalles

    End Function

    Public Shared Function Importar_Pedido_Cliente_SAP(ByRef lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar,
                                                        Optional ByVal ForzarEjecucion As Boolean = False,
                                                        Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                        Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Pedido_Cliente_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

        Try

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0'clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente) Then
                        Exit Function
                    End If
                End If

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

    Public Shared Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(tTipoDocumentoSalida.Pedido_De_Venta_NAV)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documentos a enviar: {0}", ListaPedidosTransf.Count))

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc)

                    Dim vCodEmpresaPed As String = clsLnTrans_pe_enc.Get_Empresa_By_IdPedidoEnc(PT.Idpedidoenc)

                    If vCodEmpresaPed = "" Then
                        clsPublic.Actualizar_Progreso(lblprg, "No se pudo determinar la sociedad a la que pertenece el documento :" & PT.No_pedido & " verifique que la sociedad esté definida en el documento. (O verifique si este documento debe ser enviado a SAP)")
                        Continue For
                    End If

                    If PT.No_pedido = "" Then
                        clsPublic.Actualizar_Progreso(lblprg, "No se puede enviar un documento a SAP sin número de referencia de documento base IdPedidoEnc: " & PT.Idpedidoenc)
                        Continue For
                    End If

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        Dim vvEmpresa As pEmpresa = [Enum].Parse(GetType(pEmpresa), vCodEmpresaPed)

                        Dim vNoPedidoSap As String = If(PT.No_pedido.StartsWith("K") OrElse PT.No_pedido.StartsWith("G"), PT.No_pedido.Substring(1), PT.No_pedido)

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x)
                                                                                      Dim pedido = If(x.No_pedido.StartsWith("K") OrElse x.No_pedido.StartsWith("G"), x.No_pedido.Substring(1), x.No_pedido)
                                                                                      Return pedido.Trim() = vNoPedidoSap.Trim()
                                                                                  End Function)


                        lTransaccionesSalidaSingle.ForEach(Sub(t)
                                                               If t.No_pedido.StartsWith("K") OrElse t.No_pedido.StartsWith("G") Then
                                                                   t.No_pedido = t.No_pedido.Substring(1)
                                                               End If
                                                           End Sub)

                        If Enviar_Entrega_Mercancia_OV_SAP3(vNoPedidoSap, lTransaccionesSalidaSingle, vvEmpresa, lblprg, prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalidaSingle.Count))

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "El documento: " & PT.No_pedido & " IdPedidoEncWMS: " & PT.Idpedidoenc & " se encuentra enviado a ERP (Cabecera) pero sus registros de interface aparecen pendientes de envío (no se enviarán), verifique el estado de los registros de interface.")
                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

        Catch ex As Exception
            Throw
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub
    Private Shared Function Inserta_Cliente_SAP(ByVal pCodigo As String,
                                                Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As Boolean

        Inserta_Cliente_SAP = False

        Dim conn As SapConnectionWrapper = Nothing
        Dim clsTransaccion As New clsTransaccion

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            clsTransaccion.Open_Connection()

            Dim query_sap As String = "SELECT TOP 1 
                                     T0.CARDCODE AS CODIGO,
                                     T0.CARDNAME AS NOMBRE_COMERCIAL,
                                     T0.Phone1,
                                     'TEST' AS CONTACTO,
                                     T0.AddId AS NIT,
                                     ISNULL(T1.Street, '') + ' ' + ISNULL(T1.City, '') AS DIR1,
                                     T1.Address AS DIRECCION,
                                     T0.E_Mail
                                   FROM OCRD T0
                                   LEFT JOIN CRD1 T1 ON T1.CardCode = T0.CardCode AND T1.Address = 'ENTREGA'
                                   WHERE T0.CardType = 'C'
                                   AND T0.CARDCODE = '" & pCodigo & "'"

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query_sap)

            While Not rs.EoF

                Dim BeCliente As New clsBeCliente With {
                .IdCliente = clsLnCliente.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdPropietario = BeConfigEnc.IdPropietario,
                .Codigo = pCompany.ToString().Substring(0, 1) & rs.Fields.Item("CODIGO").Value.ToString(),
                .Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString(),
                .Sistema = True,
                .Activo = True,
                .IdEmpresa = BeConfigEnc.Idempresa,
                .Nit = rs.Fields.Item("NIT").Value.ToString(),
                .IdTipoCliente = 1,
                .Es_bodega_recepcion = False,
                .Es_Bodega_Traslado = False,
                .Direccion = rs.Fields.Item("DIR1").Value.ToString()
            }

                clsLnCliente.Insertar(BeCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Dim lBodegas As New List(Of clsBeBodega)
                lBodegas = clsLnBodega.GetAll()

                For Each Bod In lBodegas

                    Dim BeClienteBodega As New clsBeCliente_bodega With {
                .IdClienteBodega = clsLnCliente_bodega.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdCliente = BeCliente.IdCliente,
                .IdBodega = Bod.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Cliente = BeCliente
                }

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Next

                Dim oBP As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), BusinessPartners)
                If oBP.GetByKey(pCodigo) Then
                    oBP.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                    'oBP.UserFields.Fields.Item("U_Observaciones").Value = BeCliente.IdCliente.ToString()
                    If oBP.Update() <> 0 Then
                        Throw New Exception("Error al actualizar UDFs del cliente en SAP: " & oCompany.GetLastErrorDescription())
                    End If
                End If

                clsTransaccion.Commit_Transaction()

                Inserta_Cliente_SAP = True

                rs.MoveNext()

            End While

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente proveniente de SAP: " & ex.Message)
        Finally
            If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
            clsTransaccion.Close_Conection()
        End Try

    End Function
    Private Shared Function Get_Codigos_Bodegas(ByVal IdPedidoEnc As Integer) As clsBeInfoBodegaByIdPedidoEnc

        Get_Codigos_Bodegas = Nothing

        Dim BeInfoBodega As New clsBeInfoBodegaByIdPedidoEnc

        Try

            Dim BePedidoEnc As New clsBeTrans_pe_enc
            clsLnTrans_pe_enc.GetBodegas_By_IdPedidoEnc(IdPedidoEnc,
                                                        BeInfoBodega.Codigo_Bodega_Origen,
                                                        BeInfoBodega.Codigo_Bodega_Destino)

            Get_Codigos_Bodegas = BeInfoBodega

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Enviar_Entrega_Mercancia_OV_SAP3(ByVal _Docentry As Integer,
                                                            ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                            ByVal pEmpresa As pEmpresa,
                                                            ByRef lblprg As RichTextBox,
                                                            ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim conn As SapConnectionWrapper = Nothing
        Dim oCompany As Company = Nothing

        Dim clsTransaccion As New clsTransaccion
        Dim clsSapTransaction As clsSapTransaction = Nothing

        Dim BePedido As clsBeTrans_pe_enc = Nothing
        Dim vEmpresa As pEmpresa
        Dim BeConfigEnc As clsBeI_nav_config_enc = Nothing

        Dim vGeneroTransferencia As Boolean = False

        Enviar_Entrega_Mercancia_OV_SAP3 = False

        Try
            ' Inicia transacción local
            clsTransaccion.Begin_Transaction()

            ' Obtiene datos del pedido WMS
            BePedido = clsLnTrans_pe_enc.GetSingle(lINavTransaccionesOut.FirstOrDefault.Idpedidoenc,
                                                   clsTransaccion.lConnection,
                                                   clsTransaccion.lTransaction)

            If BePedido Is Nothing Then
                Throw New Exception("No se encontró el pedido de cliente con IdPedidoEnc: " & lINavTransaccionesOut.FirstOrDefault.Idpedidoenc)
            End If

            vEmpresa = CType([Enum].Parse(GetType(pEmpresa), BePedido.Codigo_Empresa_ERP), pEmpresa)

            ' Conexión a SAP via pool
            conn = sapPool.GetConnection(vEmpresa)
            oCompany = conn.Company

            ' Transacción SAP
            clsSapTransaction = New clsSapTransaction(oCompany)
            clsSapTransaction.BeginTransaction()

            clsPublic.Actualizar_Progreso(lblprg, "Procesando documento: " & BePedido.Referencia_Documento_Ingreso_Bodega_Destino &
                                                   " IdPedidoEncWMS: " & BePedido.IdPedidoEnc)

            ' Configuración de interface
            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                      clsTransaccion.lConnection,
                                                      clsTransaccion.lTransaction)

            If BeConfigEnc Is Nothing Then
                Throw New Exception("No está definida la configuración de interface para el identificador: " & BD.Instancia.IdConfiguracionInterface)
            End If

            Dim lResultProductosTransferSap As List(Of ProductoTransferSAP) = Nothing
            Dim transferenciaSAP As String = clsLnTrans_pe_enc.Get_No_Picking_ERP(BePedido.IdPedidoEnc, BePedido.No_despacho)

            If (transferenciaSAP = "") Then

                ' Genera transferencia de stock
                lResultProductosTransferSap = Enviar_Transferencia_Stock_SAP(_Docentry,
                                           lINavTransaccionesOut,
                                           lblprg,
                                           prg,
                                           BePedido,
                                           clsTransaccion,
                                           oCompany,
                                           BeConfigEnc,
                                           vEmpresa)

            Else

                lResultProductosTransferSap = Preparar_Productos_Transferencia(lINavTransaccionesOut,
                                                                               BePedido,
                                                                               clsTransaccion,
                                                                               oCompany,
                                                                               pEmpresa)

            End If

            vGeneroTransferencia = (lResultProductosTransferSap.Count > 0)

            ' Genera la entrega si fue exitosa la transferencia
            If lResultProductosTransferSap IsNot Nothing AndAlso vGeneroTransferencia Then

                Dim entrega As String = clsLnTrans_pe_enc.Existe_Entrega_By_IdDespachoEnc(BePedido.No_despacho)
                Dim generoentregaOV As Boolean = False

                generoentregaOV = Generar_Entrega_OV(_Docentry,
                                   BePedido,
                                   clsTransaccion,
                                   lblprg,
                                   lINavTransaccionesOut,
                                   lResultProductosTransferSap,
                                   vEmpresa,
                                   oCompany,
                                   BeConfigEnc)
            End If

            ' Confirmar transacciones
            clsTransaccion.Commit_Transaction()
            clsSapTransaction.CommitTransaction()

            Enviar_Entrega_Mercancia_OV_SAP3 = True

        Catch errMsg As Exception
            Dim vMensaje As String = String.Format("{1} {0} {1} DocumentoSAP {2} PedidoWMS {3}",
                                               errMsg.Message, vbNewLine, _Docentry, If(BePedido IsNot Nothing, BePedido.IdPedidoEnc.ToString(), "N/D"))

            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensaje,
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

            clsTransaccion.RollBack_Transaction()
            clsSapTransaction?.RollbackTransaction()

        Finally
            If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
            clsTransaccion.Close_Conection()
        End Try

    End Function
    Public Shared Function Generar_Entrega_OV(ByVal _DocEntry As Integer,
                                              ByVal BePedido As clsBeTrans_pe_enc,
                                              ByVal clsTrans As clsTransaccion,
                                              ByVal lblprg As RichTextBox,
                                              ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                              ByVal lProductosTransferidos As List(Of ProductoTransferSAP),
                                              ByVal vEmpresa As pEmpresa,
                                              ByVal oCompany As Company,
                                              ByVal BeConfigEnc As clsBeI_nav_config_enc) As Boolean

        Dim conn As SapConnectionWrapper = Nothing
        Generar_Entrega_OV = False

        Try

            If BePedido.IdTipoPedido <> tTipoDocumentoSalida.Pedido_De_Venta_NAV Then Return False

            Dim oOrderSales As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
            If Not oOrderSales.GetByKey(_DocEntry) Then Throw New Exception("No se encontró la orden de venta.")

            Dim oEntrega As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
            InicializarCabeceraEntrega(oEntrega, oOrderSales, BePedido)

            Dim listaActualizar As List(Of clsBeI_nav_transacciones_out) = ProcesarLineasEntrega(oEntrega,
                                                                                                 oOrderSales,
                                                                                                 lINavTransaccionesOut,
                                                                                                 lProductosTransferidos,
                                                                                                 clsTrans,
                                                                                                 BePedido,
                                                                                                 _DocEntry,
                                                                                                 lblprg,
                                                                                                 BeConfigEnc,
                                                                                                 oCompany)

            If listaActualizar.Count > 0 Then

                Dim res As Integer = oEntrega.Add()
                If res <> 0 Then Throw New Exception($"#ERROR_SAP_{res}: {oCompany.GetLastErrorDescription()}")

                FinalizarEntregaSAP(oCompany, BePedido, clsTrans, lblprg, listaActualizar)

                Generar_Entrega_OV = True

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
        End Try

    End Function
    Private Shared Sub InicializarCabeceraEntrega(oEntrega As Documents, oOrderSales As Documents, BePedido As clsBeTrans_pe_enc)
        oEntrega.CardCode = oOrderSales.CardCode
        oEntrega.DocDate = Date.Today
        oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes
        ' Concatenar comentarios y asegurar que no sean Nothing
        Dim comentarios As String = (If(oOrderSales.Comments, String.Empty) &
                             $" IdPedidoEncWMS: {BePedido.IdPedidoEnc}").Trim()

        ' Limitar a un máximo de 254 caracteres
        If comentarios.Length > 254 Then
            comentarios = comentarios.Substring(0, 254)
        End If
        oEntrega.Comments = comentarios
    End Sub

    Private Shared Function ProcesarLineasEntrega(oEntrega As Documents,
                                                  oOrderSales As Documents,
                                                  transacciones As List(Of clsBeI_nav_transacciones_out),
                                                  productosTransferidos As List(Of ProductoTransferSAP),
                                                  clsTrans As clsTransaccion,
                                                  BePedido As clsBeTrans_pe_enc,
                                                  docEntry As Integer,
                                                  lblprg As RichTextBox,
                                                  BeConfigEnc As clsBeI_nav_config_enc,
                                                  oCompany As Company) As List(Of clsBeI_nav_transacciones_out)

        Dim listaActualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaEntrega As Integer = 0
        Dim vNoLineaAnterior As Integer = -1

        For j As Integer = 0 To oOrderSales.Lines.Count - 1

            oOrderSales.Lines.SetCurrentLine(j)

            Dim itemSAP = oOrderSales.Lines.ItemCode
            Dim lineaSAP = oOrderSales.Lines.LineNum
            Dim uomEntry = oOrderSales.Lines.UoMEntry
            Dim uomCode = oOrderSales.Lines.UoMCode
            Dim orderQuantity = oOrderSales.Lines.Quantity

            Dim productoWMS = productosTransferidos.FirstOrDefault(Function(x) x.CodigoProductoSAP = itemSAP)?.CodigoProductoWMS

            If String.IsNullOrEmpty(productoWMS) Then
                If Not BePedido.EsExportacion Then
                    clsPublic.Actualizar_Progreso(lblprg, $"Adv: El Producto: {itemSAP} no fue pickeado en WMS.")
                Else
                    '#CKFK20250909 Considero que aquí no va un continue for sino un Throw exception, ya que no se debe enviar a SAP
                    'el pedido parcial si es de exportación porque debe ser un producto sin equivalente
                    Throw New Exception($"Error de configuración: El Producto: {itemSAP} de WMS no tiene equivalente en la lista de SAP.")
                End If
            Else

                Dim agrupadas = AgruparTransaccionesPorLinea(transacciones, productoWMS, lineaSAP)

                agrupadas = UnificarTransaccionesPorLineaSAP(agrupadas, productosTransferidos, uomEntry, uomCode, clsTrans)

                For Each producto In agrupadas

                    If oOrderSales.Lines.LineStatus = BoStatus.bost_Close Then
                        clsLnLog_error_wms.Agregar_Error(1, BePedido.IdBodega, $"El Producto: {itemSAP} ya fue completado.")
                        Continue For
                    End If

                    If vCodigoAnterior <> itemSAP OrElse vNoLineaAnterior <> lineaSAP Then

                        oEntrega.Lines.SetCurrentLine(NoLineaEntrega)
                        oEntrega.Lines.BaseType = BoObjectTypes.oOrders
                        oEntrega.Lines.ItemCode = itemSAP
                        oEntrega.Lines.BaseEntry = docEntry
                        oEntrega.Lines.BaseLine = lineaSAP
                        oEntrega.Lines.Quantity = producto.Cantidad_Total

                        If uomEntry > 0 Then
                            oEntrega.Lines.UoMEntry = uomEntry
                        End If

                        oEntrega.Lines.WarehouseCode = BeConfigEnc.Bodega_Facturacion
                        oEntrega.Lines.Add()

                        NoLineaEntrega += 1

                        Dim transActual = transacciones.
                    Where(Function(x) x.No_pedido = oOrderSales.DocEntry AndAlso
                                      x.No_linea = lineaSAP AndAlso
                                      x.Codigo_producto = producto.Codigo_producto AndAlso
                                      Not x.Enviado).ToList()

                        listaActualizar.AddRange(transActual)

                        vCodigoAnterior = itemSAP
                        vNoLineaAnterior = lineaSAP

                    End If

                Next

            End If

        Next

        Return listaActualizar

    End Function

    Private Shared Function UnificarTransaccionesPorLineaSAP(transacciones As List(Of clsBeI_nav_transacciones_out_agrupado),
                                                             productoWMS As List(Of ProductoTransferSAP),
                                                             uomEntry As Integer,
                                                             uomCode As String,
                                                             tx As clsTransaccion) As List(Of clsBeI_nav_transacciones_out_agrupado)

        Dim agrupadas As New List(Of clsBeI_nav_transacciones_out_agrupado)

        For Each trans In transacciones

            ' Buscar producto asociado
            Dim producto = productoWMS.FirstOrDefault(Function(p) p.CodigoProductoWMS = trans.Codigo_producto)
            If producto Is Nothing Then Continue For ' Puedes lanzar excepción si lo prefieres

            ' Calcular cantidad ajustada para esta transacción
            Dim cantidadAjustada As Decimal = AjustarCantidadPorPresentacion(trans, uomEntry, uomCode, tx)

            ' Buscar si ya existe un agrupado con misma clave
            Dim existente = agrupadas.FirstOrDefault(Function(x) x.Codigo_producto = trans.Codigo_producto AndAlso
                                                            x.No_linea = trans.No_linea AndAlso
                                                            x.Idpedidoenc = trans.Idpedidoenc)

            If existente IsNot Nothing Then
                ' Sumar cantidades y anexar transacción
                existente.Cantidad_Total += cantidadAjustada
            Else
                ' Crear nuevo agrupado
                Dim nuevo As New clsBeI_nav_transacciones_out_agrupado With {
                .Codigo_producto = trans.Codigo_producto,
                .No_linea = trans.No_linea,
                .Idpedidoenc = trans.Idpedidoenc,
                .Cantidad_Total = cantidadAjustada}
                agrupadas.Add(nuevo)
            End If
        Next

        Return agrupadas

    End Function

    Private Shared Function AgruparTransaccionesPorLinea(transacciones As List(Of clsBeI_nav_transacciones_out),
                                                         codigoProductoWMS As String,
                                                         lineaSAP As Integer) As List(Of clsBeI_nav_transacciones_out_agrupado)

        Return transacciones.
        Where(Function(x) x.Codigo_producto = codigoProductoWMS AndAlso x.No_linea = lineaSAP).
        GroupBy(Function(x) New With {x.Idpedidoenc, x.Codigo_producto, x.No_linea, x.Idpresentacion, x.IdDespachoDet}).
        Select(Function(g) New clsBeI_nav_transacciones_out_agrupado With {
            .Codigo_producto = g.Key.Codigo_producto,
            .No_linea = g.Key.No_linea,
            .IdDespachoDet = g.Key.IdDespachoDet,
            .IdPresentacion = g.Key.Idpresentacion,
            .Idpedidoenc = g.Key.Idpedidoenc,
            .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
        }).ToList()
    End Function
    Private Shared Function AjustarCantidadPorPresentacion(prod As clsBeI_nav_transacciones_out_agrupado,
                                                           uomEntry As Integer,
                                                           uomCode As String,
                                                           clsTrans As clsTransaccion) As Decimal

        Dim presentacion = clsLnTrans_despacho_det.Get_BePresentacion_By_IdDespachoDet(prod.IdDespachoDet, prod.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

        If prod.IdPresentacion = 0 AndAlso uomEntry > 0 AndAlso uomCode <> "Unidad" AndAlso presentacion IsNot Nothing Then
            Return Math.Round(prod.Cantidad_Total / presentacion.Factor, 6)
        End If

        If prod.IdPresentacion <> 0 AndAlso (uomEntry = 0 OrElse uomCode = "Unidad") AndAlso
            presentacion IsNot Nothing Then
            Return Math.Round(prod.Cantidad_Total * presentacion.Factor, 6)
        End If

        If presentacion Is Nothing AndAlso uomEntry > 0 And uomCode <> "Unidad" Then
            presentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Nombre_Presentacion(prod.Codigo_producto, uomCode, clsTrans.lConnection, clsTrans.lTransaction)
            If presentacion IsNot Nothing Then
                Return Math.Round(prod.Cantidad_Total / presentacion.Factor, 6)
            End If
        End If

        Return prod.Cantidad_Total

    End Function
    Private Shared Sub FinalizarEntregaSAP(oCompany As Company,
                                           BePedido As clsBeTrans_pe_enc,
                                           clsTrans As clsTransaccion,
                                           lblprg As RichTextBox,
                                           listaActualizar As List(Of clsBeI_nav_transacciones_out))

        Try

            Dim newObjCode As String = ""
            oCompany.GetNewObjectCode(newObjCode)

            Dim vDocEntryEntrega As Integer
            If Not Integer.TryParse(newObjCode, vDocEntryEntrega) Then
                Throw New Exception("No se pudo obtener el DocEntry de la entrega.")
            End If

            Dim BeDespachoEnc = clsLnTrans_despacho_enc.Get_Single_By_IdPedidoEnc(BePedido.IdPedidoEnc, clsTrans.lConnection, clsTrans.lTransaction)
            Dim oEntrega As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
            If oEntrega.GetByKey(vDocEntryEntrega) Then
                BeDespachoEnc.No_pase = oEntrega.DocNum
                clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespachoEnc, clsTrans.lConnection, clsTrans.lTransaction)
            End If

            clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(BePedido.IdPedidoEnc, True, BeConfigEnc.IdUsuario, clsTrans.lConnection, clsTrans.lTransaction)

            If listaActualizar.Any() Then
                Dim ok = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(listaActualizar, clsTrans.lConnection, clsTrans.lTransaction)
                If ok = 0 Then
                    Throw New Exception("No se pudieron marcar como enviadas las líneas en WMS.")
                End If
            End If

            Dim msg = $"Se creó la entrega de mercancía para el pedido de cliente: {BePedido.Referencia_Documento_Ingreso_Bodega_Destino} con DocNum: {vDocEntryEntrega} en SAP. Para el IdPedidoEnc WMS: {BePedido.IdPedidoEnc} - {BePedido.Codigo_Empresa_ERP}"
            clsPublic.Actualizar_Progreso(lblprg, msg)
            clsLnLog_error_wms.Agregar_Error(1, BePedido.IdBodega, msg, BePedido.IdPedidoEnc, BePedido.IdPickingEnc, 0, IdUsuario)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Public Shared Function Marcar_Documento_Sincronizado_SAP(ByVal pNoDocumento As Integer,
    '                                                         ByVal IdPedidoEnc As Integer,
    '                                                         Optional ByVal pCompany As Integer = pEmpresa.Killios) As Boolean
    '    Dim conn As SapConnectionWrapper = Nothing
    '    Marcar_Documento_Sincronizado_SAP = False

    '    Try

    '        conn = sapPool.GetConnection(pCompany)
    '        Dim oCompany As Company = conn.Company

    '        Dim sQuery As String = "UPDATE ORDR SET U_Enviado_WMS = '1' WHERE DocEntry = " & pNoDocumento
    '        Dim oRecordset As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

    '        oRecordset.DoQuery(sQuery)
    '        Marcar_Documento_Sincronizado_SAP = True

    '    Catch ex As Exception
    '        Throw New Exception("Error al marcar PI como sincronizado en SAP: " & ex.Message)
    '    Finally
    '        If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
    '    End Try

    'End Function

    Public Shared Function Marcar_Documento_Sincronizado_SAP(ByVal docEntry As Integer,
                                                             ByVal IdPedidoEnc As Integer,
                                                             Optional ByVal pCompany As Integer = pEmpresa.Killios) As Boolean
        Dim conn As SapConnectionWrapper = Nothing
        Dim intentos As Integer = 10
        Dim esperaMs As Integer = 300
        Marcar_Documento_Sincronizado_SAP = False

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            For i As Integer = 1 To intentos
                Dim oOrder As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
                If Not oOrder.GetByKey(docEntry) Then
                    Throw New Exception($"No existe ORDR con DocEntry={docEntry}.")
                End If

                ' Idempotencia: ya está marcado
                Dim curr As String = CStr(oOrder.UserFields.Fields.Item("U_Enviado_WMS").Value)
                If curr = "1" Then
                    Marcar_Documento_Sincronizado_SAP = True
                    Exit For
                End If

                oOrder.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"

                Dim ret As Integer = oOrder.Update()
                If ret = 0 Then
                    ' Verificación post-update
                    Dim check As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
                    If check.GetByKey(docEntry) AndAlso CStr(check.UserFields.Fields.Item("U_Enviado_WMS").Value) = "1" Then
                        Marcar_Documento_Sincronizado_SAP = True
                        Exit For
                    Else
                        ' algo externo revirtió o no se guardó
                        Throw New Exception("Verificación fallida: el UDF no quedó en '1'.")
                    End If
                Else
                    Dim code As Integer : Dim msg As String = ""
                    oCompany.GetLastError(code, msg)

                    ' Para deadlock/lock/timeout reintenta; para permisos o nombre UDF mal, no sirve reintentar
                    Dim reintentar As Boolean =
                    (code = -5002) OrElse msg.ToLowerInvariant().Contains("locked") OrElse msg.ToLowerInvariant().Contains("timeout")
                    If i = intentos OrElse Not reintentar Then
                        Throw New Exception($"DI API Update() falló (intento {i}/{intentos}): {code} - {msg}")
                    End If
                    Threading.Thread.Sleep(esperaMs)
                    esperaMs *= 2 ' backoff exponencial
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Error al marcar pedido como sincronizado en SAP: " & ex.Message)
        Finally
            If conn IsNot Nothing Then sapPool.ReleaseConnection(conn)
        End Try
    End Function
    Public Shared Function Enviar_Transferencia_Stock_SAP(ByVal _DocEntry As Integer,
                                                           ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                           ByRef lblprg As RichTextBox,
                                                           ByRef prg As Windows.Forms.ProgressBar,
                                                           ByRef BePedido As clsBeTrans_pe_enc,
                                                           ByVal clsTrans As clsTransaccion,
                                                           ByVal oCompany As Company,
                                                           BeConfigEnc As clsBeI_nav_config_enc,
                                                           Optional ByVal pCompany As Integer = pEmpresa.Killios) As List(Of ProductoTransferSAP)

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Try

            Dim productosPreparados = Preparar_Productos_Transferencia(lINavTransaccionesOut, BePedido, clsTrans, oCompany, pCompany)

            Dim oOrderSales As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
            If Not oOrderSales.GetByKey(_DocEntry) Then
                Throw New Exception("No se pudo obtener el documento de ventas con DocEntry: " & _DocEntry)
            End If

            Dim resultado = Procesar_Transferencias_SAP(_DocEntry,
                                                        productosPreparados,
                                                        BePedido,
                                                        clsTrans,
                                                        oCompany,
                                                        BeConfigEnc,
                                                        lblprg,
                                                        pCompany,
                                                        oOrderSales)

            Return resultado

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log("Error al enviar traslado entre almacenes a SAP: " & ex.Message,
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)
            Throw New Exception("Error al enviar traslado entre almacenes a SAP: " & vbNewLine & ex.Message)
        End Try

    End Function

    Private Shared Function Preparar_Productos_Transferencia(lTransacciones As List(Of clsBeI_nav_transacciones_out),
                                                              BePedido As clsBeTrans_pe_enc,
                                                              clsTrans As clsTransaccion,
                                                              oCompany As Company,
                                                              empresa As pEmpresa) As List(Of ProductoTransferSAP)

        Dim productos As New List(Of ProductoTransferSAP)

        Dim agrupados = lTransacciones.
        Where(Function(x) (x.IdTipoDocumento = tTipoDocumentoSalida.Pedido_De_Cliente OrElse
                           x.IdTipoDocumento = tTipoDocumentoSalida.Pedido_De_Venta_NAV) AndAlso
                           Not x.Enviado).
        GroupBy(Function(x) New With {x.Idpedidoenc, x.No_pedido, x.IdDespachoDet, x.Idpresentacion, x.No_linea})

        For Each grupo In agrupados
            For Each trans In grupo
                Dim prod As clsBeProducto = clsLnProducto.Get_Single_By_Codigo(trans.Codigo_producto, clsTrans.lConnection, clsTrans.lTransaction)
                Dim codigoSAP = If(empresa = pEmpresa.Killios, prod.Noparte, prod.Noserie)

                If BePedido.EsExportacion Then
                    codigoSAP = SapHelper.Obtener_Codigo_Exportacion_SAP(codigoSAP, prod.Codigo, oCompany)
                End If
                If codigoSAP = "425" Then
                    Debug.Print("Aqui")
                End If
                Dim cantidad = Ajustar_Cantidad_Presentacion(trans, clsTrans)

                Dim existente = productos.FirstOrDefault(Function(p) p.IdPedidoEnc = trans.Idpedidoenc AndAlso
                                                             p.CodigoProductoSAP = codigoSAP AndAlso p.CodigoPresentacion = trans.Codigo_variante)
                If existente IsNot Nothing Then
                    existente.Cantidad_Total += cantidad
                Else

                    If trans.Idpresentacion > 0 Then
                        If trans.Codigo_variante = String.Empty Then
                            trans.Codigo_variante = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_IdPresentacion(trans.Idpresentacion, clsTrans.lConnection, clsTrans.lTransaction)
                        End If
                    End If

                    productos.Add(New ProductoTransferSAP With {.IdPedidoEnc = trans.Idpedidoenc,
                    .CodigoProductoSAP = codigoSAP,
                    .CodigoProductoWMS = prod.Codigo,
                    .Cantidad_Total = cantidad,
                                                                .CodigoPresentacion = trans.Codigo_variante,
                    .No_Pedido = trans.No_pedido,
                    .No_Linea = trans.No_linea
                })

                End If
            Next
        Next

        Return productos

    End Function
    Private Shared Function Ajustar_Cantidad_Presentacion(ByRef trans As clsBeI_nav_transacciones_out,
                                                          clsTrans As clsTransaccion) As Decimal
        Dim factor As Decimal = 1D
        Dim presentacionDesp As clsBeProducto_Presentacion = clsLnTrans_despacho_det.Get_BePresentacion_By_IdDespachoDet(trans.IdDespachoDet, trans.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)
        Dim presentacionPed As clsBeProducto_Presentacion = clsLnTrans_pe_det.Get_BePresentacion_By_NoLinea(trans.No_linea, trans.Idpedidoenc, clsTrans.lConnection, clsTrans.lTransaction)

        If presentacionDesp IsNot Nothing AndAlso presentacionPed Is Nothing Then
            factor = presentacionDesp.Factor
            Return Math.Round(trans.Cantidad * factor, 6)
        End If

        If presentacionDesp Is Nothing AndAlso presentacionPed IsNot Nothing Then
            factor = presentacionPed.Factor
            '#EJC20251217: Con esto facilito que se agrupen bien cajas y unidades en el siguiente procedimiento.
            trans.Codigo_variante = presentacionPed.Codigo
            Return Math.Round(trans.Cantidad / factor, 6)
        End If

        Return trans.Cantidad
    End Function

    Private Shared Function Procesar_Transferencias_SAP(docEntry As Integer,
                                                         productos As List(Of ProductoTransferSAP),
                                                        ByRef BePedido As clsBeTrans_pe_enc,
                                                         clsTrans As clsTransaccion,
                                                         oCompany As Company,
                                                         BeConfigEnc As clsBeI_nav_config_enc,
                                                         lblprg As RichTextBox,
                                                         pCompany As pEmpresa,
                                                         oOrderSales As Documents) As List(Of ProductoTransferSAP)

        Dim oOrdenVenta As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)
        If Not oOrdenVenta.GetByKey(docEntry) Then Throw New Exception("No se encontró la orden de venta: " & docEntry)
        Dim vLineasAgregadas As Integer = 0

        Dim agrupadoPorPedido = productos.GroupBy(Function(p) p.IdPedidoEnc)

        For Each pedido In agrupadoPorPedido

            Dim oTransfer As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
            Dim infoBodega = Get_Codigos_Bodegas(pedido.Key)

            Dim origen = infoBodega.Codigo_Bodega_Origen
            Dim destino = If(BePedido.IdTipoPedido = tTipoDocumentoSalida.Pedido_De_Venta_NAV,
                             BeConfigEnc.Bodega_Facturacion,
                             BePedido.Cliente.Codigo.Substring(1))

            oTransfer.FromWarehouse = origen
            oTransfer.ToWarehouse = destino
            oTransfer.Comments = $"Transferencia SAP - Pedido WMS: {pedido.Key} - Orden de Venta SAP: {BePedido.Referencia_Documento_Ingreso_Bodega_Destino}"
            oTransfer.JournalMemo = oTransfer.Comments

            Dim rs As Recordset
            rs = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            Dim proderror As String = 0

            For Each linea In pedido

                ' Verificar existencia y costo antes de agregar la línea
                Dim itemCode As String
                itemCode = linea.CodigoProductoSAP

                ' Si pasa las validaciones, se agrega la línea
                oTransfer.Lines.ItemCode = linea.CodigoProductoSAP
                oTransfer.Lines.Quantity = linea.Cantidad_Total
                oTransfer.Lines.FromWarehouseCode = origen
                oTransfer.Lines.WarehouseCode = destino

                Dim resultado = SapHelper.Obtener_UoMEntry_De_InventoryUOM(linea.CodigoProductoSAP, oCompany)
                Dim umEntryOrdenVenta As Integer = Buscar_UoMEntry_OrdenVenta(oOrdenVenta, linea.CodigoProductoSAP, linea.No_Linea)

                If resultado.UoMEntry > 0 Then
                    '"EJC20250902: Cuando el pedido es de exportación la umEntryOrdenVenta es -1 por la comparación por código,
                    'no enviar la cantidad dividida.
                    If (resultado.UoMEntry <> umEntryOrdenVenta) AndAlso (umEntryOrdenVenta <> -1) Then
                        oTransfer.Lines.Quantity = Math.Round(linea.Cantidad_Total / resultado.Factor, 6)
                    End If
                    oTransfer.Lines.UoMEntry = resultado.UoMEntry
                Else
                    Debug.Print(resultado.UoMEntry)
                End If

                oTransfer.Lines.Add() : vLineasAgregadas += 1

            Next

            If vLineasAgregadas > 0 Then
                If oTransfer.Add() <> 0 Then
                    Dim err = oCompany.GetLastErrorDescription()
                    Throw New Exception("Error al crear la transferencia SAP: " & err)
                Else
                    Dim newObjCode As String = ""
                    oCompany.GetNewObjectCode(newObjCode)
                    BePedido.No_Picking_ERP = CInt(newObjCode)
                    Dim vResult As Integer = clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(BePedido, clsTrans.lConnection, clsTrans.lTransaction)
                    clsPublic.Actualizar_Progreso(lblprg, $"Transferencia generada correctamente. DocNum: {newObjCode}-{BePedido.Codigo_Empresa_ERP}")
                    clsLnLog_error_wms.Agregar_Error(1, BePedido.IdBodega, $"Transferencia generada correctamente. DocNum: {newObjCode}-{BePedido.Codigo_Empresa_ERP}", BePedido.IdPedidoEnc, BePedido.IdPickingEnc, 0, IdUsuario)
                End If
            Else
                clsPublic.Actualizar_Progreso(lblprg, $"Advertencia > Transferencia SAP - Pedido WMS: {pedido.Key} - Orden de Venta SAP: {BePedido.Referencia_Documento_Ingreso_Bodega_Destino} (No se generaron líneas, ¿es un despacho parcial?)")
            End If

        Next

        Return productos

    End Function
    Private Shared Function Buscar_UoMEntry_OrdenVenta(oOrdenVenta As Documents, itemCode As String, lineNum As Integer) As Integer
        For i As Integer = 0 To oOrdenVenta.Lines.Count - 1
            oOrdenVenta.Lines.SetCurrentLine(i)
            If oOrdenVenta.Lines.ItemCode = itemCode AndAlso oOrdenVenta.Lines.LineNum = lineNum Then
                Return oOrdenVenta.Lines.UoMEntry
            End If
        Next
        Return -1 ' No encontrado
    End Function

    Public Shared Function Procesar_Pedidos_Cliente_SAP(ByRef lblprg As RichTextBox,
                                                         ByRef prg As Windows.Forms.ProgressBar,
                                                         ByRef cnnLog As SqlConnection,
                                                         Optional pPedidoCliente As String = "") As Boolean
        Procesar_Pedidos_Cliente_SAP = False
        Dim resultado As String = ""

        Try

            Procesar_Pedidos_Por_Empresa(pEmpresa.Killios, "K", pPedidoCliente, lblprg, resultado)
            Procesar_Pedidos_Por_Empresa(pEmpresa.Garesa, "G", pPedidoCliente, lblprg, resultado)

            Procesar_Pedidos_Cliente_SAP = True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            Throw
        Finally
            clsPublic.Actualizar_Progreso(lblprg, "Se finalizó el proceso de sincronización: " & Now)
        End Try

    End Function

    Private Shared Sub Procesar_Pedidos_Por_Empresa(emp As pEmpresa,
                                                    prefijoCliente As String,
                                                    pPedidoCliente As String,
                                                    lblprg As RichTextBox,
                                                    ByRef resultado As String)

        Dim pedidos As List(Of clsBeI_nav_ped_traslado_enc) = Get_Pedidos_Cliente_SAP(pPedidoCliente, emp)

        If pedidos Is Nothing OrElse pedidos.Count = 0 Then
            clsPublic.Actualizar_Progreso(lblprg, $"No hay pedidos en {emp}.")
            Exit Sub
        End If

        clsPublic.Actualizar_Progreso(lblprg, $"Documentos encontrados en {emp}: {pedidos.Count}")

        For Each pedido In pedidos

            clsPublic.Actualizar_Progreso(lblprg, $"Procesando Pedido Cliente (ORDR): {pedido.Receipt_Document_Reference}-{pedido.Company_Code}")

            Dim codigoClienteWMS As String = prefijoCliente & pedido.Transfer_to_Code
            Dim clienteWMS As clsBeCliente = Validar_o_Insertar_Cliente(codigoClienteWMS, pedido.Transfer_to_Code, pedido.Company_Code, emp, lblprg)

            Try

                Dim pedidoImportado As clsBeTrans_pe_enc =
                clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(pedido, lblprg, Nothing, Nothing)

                If pedidoImportado IsNot Nothing Then
                    If pedido.No.StartsWith("K") OrElse pedido.No.StartsWith("G") Then
                        pedido.No = pedido.No.Substring(1)
                        Marcar_Documento_Sincronizado_SAP(pedido.No, pedidoImportado.IdPedidoEnc, emp)
                    Else
                        Marcar_Documento_Sincronizado_SAP(pedido.No, pedidoImportado.IdPedidoEnc, emp)
                    End If
                End If

                clsPublic.Actualizar_Progreso(lblprg, resultado)

            Catch ex As Exception
                clsPublic.Actualizar_Progreso(lblprg, ex.Message)
            End Try

        Next

    End Sub
    Private Shared Function Validar_o_Insertar_Cliente(codigoWMS As String,
                                                       codigoSAP As String,
                                                       companyCode As String,
                                                       emp As pEmpresa,
                                                       lblprg As RichTextBox) As clsBeCliente

        Dim cliente As clsBeCliente = clsLnCliente.Existe_By_Codigo_And_Company(codigoWMS, companyCode)

        If cliente IsNot Nothing Then Return cliente

        cliente = clsLnCliente.Existe(codigoWMS)

        If cliente IsNot Nothing Then
            cliente.Codigo = codigoWMS
            clsLnCliente.Actualizar(cliente)
            Return cliente
        End If

        cliente = clsLnCliente.Existe(codigoSAP)

        If cliente IsNot Nothing Then
            cliente.Codigo = codigoWMS
            cliente.Codigo_Empresa_ERP = emp.ToString()
            clsLnCliente.Actualizar(cliente)
            Return cliente
        End If

        If Inserta_Cliente_SAP(codigoSAP, emp) Then
            clsPublic.Actualizar_Progreso(lblprg, $"El cliente: {codigoSAP} no existía en WMS y fue insertado.")
        End If

        Return clsLnCliente.Existe_By_Codigo_And_Company(codigoWMS, companyCode)

    End Function
    Private Class clsBeI_nav_transacciones_out_agrupado
        Public Property Idpedidoenc As Integer
        Public Property Codigo_producto As String
        Public Property No_linea As Integer
        Public Property IdPresentacion As Integer
        Public Property IdDespachoDet As Integer
        Public Property Cantidad_Total As Decimal
    End Class
    Public Class clsBeInfoBodegaByIdPedidoEnc
        Public Property Codigo_Bodega_Origen As String = ""
        Public Property Codigo_Bodega_Destino As String = ""
    End Class

    Public Shared Function IsDecimal(ByVal value As String) As Boolean
        Dim result As Decimal
        Return Decimal.TryParse(value, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, result)
    End Function

End Class