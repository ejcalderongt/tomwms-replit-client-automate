Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSapDevolucionProveedor : Inherits clsInterfaceBase

    Public Shared Function Get_Devolucion_Proveedor_From_SAP(Optional ByVal AplicarFiltros As Boolean = True,
                                                             Optional ByVal pDocEntrySolicitudDevolucion As String = "",
                                                             Optional ByVal pCompany As pEmpresa = pEmpresa.Killios) As List(Of clsBeI_nav_ped_traslado_enc)

        Dim lSolicitudesDevolucion As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            Dim BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
            Dim BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim SAP_OV As String = "SELECT T0.DocEntry, T0.DocNum, T0.DocDate, T0.CardCode, T0.CardName, T0.DocCur, T0.DocTotal, " &
                              "T0.JrnlMemo, T0.Canceled, T0.DocStatus, " &
                              "CASE WHEN T0.DocType = 'I' THEN 'ARTICULO' ELSE 'SERVICIO' END AS TIPO_ORDEN_VENTA, " &
                              "(SELECT TOP 1 D0.WhsCode FROM PRR1 D0 WHERE D0.DocEntry = T0.DocEntry) AS BODEGA, " &
                              "ISNULL(T0.Comments,'') + ISNULL(T0.Address,'') AS Comments, T0.NumAtCard " &
                              "FROM OPRR T0 WHERE T0.DocStatus = 'O' AND T0.CreateDate >= '2024-01-01' " &
                              "AND T0.U_Enviado_WMS = 2 AND T0.Canceled = 'N'" &
                              If(pDocEntrySolicitudDevolucion <> "", " AND T0.DocEntry = " & pDocEntrySolicitudDevolucion, "") &
                              " ORDER BY T0.DocEntry DESC"

            RsEnc.DoQuery(SAP_OV)

            While Not RsEnc.EoF
                Dim BeEnc As New clsBeI_nav_ped_traslado_enc With {
                .No = RsEnc.Fields.Item("DOCENTRY").Value,
                .Posting_Date = RsEnc.Fields.Item("DOCDATE").Value,
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
                .Document_Type = tTipoDocumentoSalida.Devolucion_Proveedor,
                .Company_Code = pCompany.ToString(),
                .Lineas_Detalle = Get_Devolucion_Proveedor_Det(RsEnc.Fields.Item("DOCENTRY").Value, oCompany)
            }
                lSolicitudesDevolucion.Add(BeEnc)
                RsEnc.MoveNext()
            End While

            Return lSolicitudesDevolucion

        Catch ex As Exception
            Throw
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function
    Private Shared Function Get_Devolucion_Proveedor_Det(ByVal docEntry As Integer, oCompany As Company) As List(Of clsBeI_nav_ped_traslado_det)

        Get_Devolucion_Proveedor_Det = Nothing

        Try

            Dim detalles As New List(Of clsBeI_nav_ped_traslado_det)
            Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            Dim query As String = "SELECT T0.LineNum, T1.U_CodWMS AS ITEMCODE, T0.DSCRIPTION, T0.QUANTITY, T0.PRICE, " &
                              "T0.LINETOTAL, T0.VATSUM, T0.DOCENTRY, T0.WHSCODE, T2.UomCode AS UNIDAD_MEDIDA, T2.UomName " &
                              "FROM PRR1 T0 INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode " &
                              "INNER JOIN OUOM T2 ON T1.IUoMEntry = T2.UomEntry WHERE T0.DOCENTRY = '" & docEntry & "'"

            RsDet.DoQuery(query)

            While Not RsDet.EoF
                Dim det As New clsBeI_nav_ped_traslado_det With {
                .NoEnc = docEntry,
                .No = clsLnTrans_pe_det.MaxID() + 1,
                .Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString(),
                .Line_No = RsDet.Fields.Item("LINENUM").Value.ToString(),
                .Shipment_Date = Date.Now,
                .Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value),
                .Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString(),
                .Unit_of_Measure_Code = "UN",
                .Status = 1,
                .Variant_Code = If(RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString().ToUpper() = "UNIDAD", Nothing, RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()),
                .Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString(),
                .Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
            }
                detalles.Add(det)
                RsDet.MoveNext()

            End While

            Return detalles

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Function Procesar_Devolucion_Mercancia_SAP(ByVal lblprg As RichTextBox,
                                                              ByRef prg As ProgressBar,
                                                              Optional pPedidoCliente As String = "") As Boolean
        Dim Resultado As String = ""
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim empresas() As pEmpresa = {pEmpresa.Killios, pEmpresa.Garesa}

        Try

            For Each empresa In empresas

                Dim devoluciones As List(Of clsBeI_nav_ped_traslado_enc) = Get_Devolucion_Proveedor_From_SAP(True, pPedidoCliente, empresa)

                If devoluciones Is Nothing OrElse devoluciones.Count = 0 Then
                    Dim mensaje As String = If(pPedidoCliente <> "",
                    $"No hay devoluciones a proveedor pendientes de importar con el No.: {pPedidoCliente}{vbNewLine}",
                    $"No hay devoluciones a proveedor pendientes de importar {vbNewLine}")
                    clsPublic.Actualizar_Progreso(lblprg, mensaje)
                    Continue For
                End If

                clsPublic.Actualizar_Progreso(lblprg, $"Documento pertenece a: {empresa}")

                If CnnLog.State <> ConnectionState.Open Then
                    CnnLog.Open()
                End If

                For Each PedidoClienteSAP In devoluciones

                    clsPublic.Actualizar_Progreso(lblprg, $"Procesando devolución a proveedor (OPRR): {PedidoClienteSAP.No}/{PedidoClienteSAP.Receipt_Document_Reference}{vbNewLine}")

                    Dim BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                    If BeClienteWMS Is Nothing Then
                        If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code, empresa) Then
                            clsPublic.Actualizar_Progreso(lblprg, $"El cliente: {PedidoClienteSAP.Transfer_to_Code} no existía en WMS y fue insertado.")
                        Else
                            clsPublic.Actualizar_Progreso(lblprg, $"El cliente: {PedidoClienteSAP.Transfer_to_Code} no se pudo insertar en WMS.")
                            Return False
                        End If
                    End If

                    Dim BePedidoEncResult = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP, lblprg, Nothing, Nothing)

                    If BePedidoEncResult IsNot Nothing Then
                        BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc)
                        Marcar_Solicitud_Devolucion_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg, empresa)
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, Resultado)
                Next
            Next

            Return True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, CnnLog)
            Throw
        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try
    End Function
    Private Shared Function Inserta_Cliente_SAP(ByVal pCodigo As String, ByVal pCompany As pEmpresa) As Boolean

        Inserta_Cliente_SAP = False

        Dim clsTransaccion As New clsTransaccion
        Dim lFamilias As List(Of clsBeProducto_familia) = Nothing
        Dim lClasificacion As List(Of clsBeProducto_clasificacion) = Nothing
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            Dim query_sap As String = $"SELECT TOP 1 T0.CARDCODE AS CODIGO, T0.CARDNAME AS NOMBRE_COMERCIAL, T0.Phone1, 'TEST' AS CONTACTO, T0.LicTradNum AS NIT, T0.Address AS DIRECCION, T0.E_Mail FROM OCRD T0 WHERE T0.CARDTYPE = 'S' AND T0.CARDCODE = '{pCodigo}'"

            Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rs.DoQuery(query_sap)

            clsTransaccion.Open_Connection()

            Dim BeCliente As New clsBeCliente

            While Not rs.EoF
                BeCliente.IdCliente = clsLnCliente.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                BeCliente.Codigo = rs.Fields.Item("CODIGO").Value.ToString()
                BeCliente.Nombre_comercial = rs.Fields.Item("NOMBRE_COMERCIAL").Value.ToString()
                BeCliente.Sistema = True
                BeCliente.Activo = True
                BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                BeCliente.Nit = rs.Fields.Item("NIT").Value.ToString()
                BeCliente.IdTipoCliente = 1
                BeCliente.Es_bodega_recepcion = False
                BeCliente.Es_Bodega_Traslado = False
                BeCliente.User_agr = BeConfigEnc.IdUsuario
                BeCliente.Fec_agr = Now
                BeCliente.User_mod = "MI3"
                BeCliente.Fec_mod = Now
                BeCliente.IdUbicacionAbastecerCon = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(BeConfigEnc.Idbodega, clsTransaccion.lConnection, clsTransaccion.lTransaction)

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

                If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                    lFamilias = clsLnProducto_familia.Get_All_Filtro(True, BeConfigEnc.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True, BeConfigEnc.IdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If lFamilias IsNot Nothing AndAlso lClasificacion IsNot Nothing Then
                        For Each F In lFamilias
                            For Each C In lClasificacion
                                Dim BeTiempoCliente As New clsBeCliente_tiempos With {
                                .IdTiempoCliente = clsLnCliente_tiempos.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                                .IdCliente = BeCliente.IdCliente,
                                .IdFamilia = F.IdFamilia,
                                .IdClasificacion = C.IdClasificacion,
                                .Dias_Local = BeConfigEnc.Dias_Vida_Defecto_Perecederos,
                                .Dias_Exterior = BeConfigEnc.Dias_Vida_Defecto_Perecederos,
                                .User_agr = BeConfigEnc.IdUsuario,
                                .User_mod = BeConfigEnc.IdUsuario,
                                .Fec_agr = Now,
                                .Fec_mod = Now,
                                .Activo = True
                            }
                                clsLnCliente_tiempos.Insertar(BeTiempoCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                            Next
                        Next
                    End If
                End If

                clsTransaccion.Commit_Transaction()

                Dim updateQuery As String = $"UPDATE OCRD SET U_Enviado_WMS = '1' WHERE CardCode = '{pCodigo}'"
                Dim updateRS As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                updateRS.DoQuery(updateQuery)

                Inserta_Cliente_SAP = True

                rs.MoveNext()

            End While

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Private Shared Function Marcar_Solicitud_Devolucion_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                                 ByVal EstadoEnvio As Estado_Enviado_SAP,
                                                                 ByVal lblprg As RichTextBox,
                                                                 ByVal pCompany As pEmpresa) As Boolean

        Marcar_Solicitud_Devolucion_Sincronizado_SAP = False
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(pCompany)
            Dim oCompany As Company = conn.Company

            clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio & " para permitir importación nuevamente en pedido de cliente: " & pNoDocumento)

            Dim updateQuery As String = $"UPDATE OPRR SET U_Enviado_WMS = '{EstadoEnvio}' WHERE DocEntry = '{pNoDocumento}'"
            Dim rsUpdate As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            rsUpdate.DoQuery(updateQuery)

            clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")

            Marcar_Solicitud_Devolucion_Sincronizado_SAP = True

        Catch ex As Exception
            Throw ex
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
        End Try

    End Function

    Public Shared Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar,
                                                     ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)
        Dim BePedidoEnc As New clsBeTrans_pe_enc

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documentos a enviar: {0}", ListaPedidosTransf.Count))

                For Each PT In ListaPedidosTransf

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Devolución: {0}-{1}", PT.Idpedidoenc, PT.No_pedido))

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        BePedidoEnc = clsLnTrans_pe_enc.GetSingle(PT.Idpedidoenc)

                        If Crear_Devolucion_Desde_Solicitud_Aprobada(PT.No_pedido,
                                                                     lTransaccionesSalidaSingle,
                                                                     BePedidoEnc,
                                                                     lblprg,
                                                                     prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        End If

                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Private Shared Function Crear_Devolucion_Desde_Solicitud_Aprobada(ByVal _Docentry As Integer,
                                                                      ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                      ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                                      ByRef lblprg As RichTextBox,
                                                                      ByRef prg As ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaEntrega As Integer = 0
        Dim BeProducto As New clsBeProducto
        Dim clsTransaccion As New clsTransaccion
        Dim vEmpresa As pEmpresa = CType([Enum].Parse(GetType(pEmpresa), BePedidoEnc.Codigo_Empresa_ERP), pEmpresa)
        Dim conn As SapConnectionWrapper = Nothing

        Try

            conn = sapPool.GetConnection(vEmpresa)
            Dim oCompany As Company = conn.Company

            Dim oSolicitudDevolucion As Documents = CType(oCompany.GetBusinessObject(234000032), Documents)
            Dim oDevolucion As Documents = CType(oCompany.GetBusinessObject(BoObjectTypes.oPurchaseReturns), Documents)

            If Not oSolicitudDevolucion.GetByKey(_Docentry) Then Return False

            If oSolicitudDevolucion.AuthorizationStatus = DocumentAuthorizationStatusEnum.dasPending Then
                Throw New Exception("El documento está pendiente de aprobación en SAP.")
            ElseIf oSolicitudDevolucion.AuthorizationStatus = DocumentAuthorizationStatusEnum.dasRejected Then
                Throw New Exception("El documento fue rechazado en SAP.")
            End If

            oDevolucion.CardCode = oSolicitudDevolucion.CardCode
            oDevolucion.DocDate = Date.Today
            oDevolucion.DocObjectCode = BoObjectTypes.oPurchaseDeliveryNotes
            oDevolucion.NumAtCard = BePedidoEnc.Cliente.Codigo

            clsTransaccion.Open_Connection()

            For i As Integer = 0 To oSolicitudDevolucion.Lines.Count - 1

                oSolicitudDevolucion.Lines.SetCurrentLine(i)

                Dim vCodigoProductoSAP As String = oSolicitudDevolucion.Lines.ItemCode
                Dim vNoLineaOCSAP As Integer = oSolicitudDevolucion.Lines.LineNum

                clsPublic.Actualizar_Progreso(lblprg, $"Procesando Producto: {vCodigoProductoSAP}")

                BeProducto = If(vEmpresa = pEmpresa.Killios,
                                clsLnProducto.Get_Single_By_NoParte(vCodigoProductoSAP, clsTransaccion.lConnection, clsTransaccion.lTransaction),
                                clsLnProducto.Get_Single_By_NoSerie(vCodigoProductoSAP, clsTransaccion.lConnection, clsTransaccion.lTransaction))

                Dim productosSalida = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = BeProducto.Codigo AndAlso x.No_linea = vNoLineaOCSAP).
                                    GroupBy(Function(x) New With {x.Codigo_producto, x.No_linea}).
                                    Select(Function(g) New With {.Codigo_producto = g.Key.Codigo_producto, .No_linea = g.Key.No_linea, .Cantidad_Total = g.Sum(Function(x) x.Cantidad)}).
                                    ToList()

                For Each producto In productosSalida

                    If oSolicitudDevolucion.Lines.LineStatus = BoStatus.bost_Close Then
                        clsPublic.Actualizar_Progreso(lblprg, $"El Producto: {vCodigoProductoSAP} ya fue completado.")
                        Continue For
                    End If

                    If producto.Cantidad_Total > oSolicitudDevolucion.Lines.Quantity Then
                        Throw New Exception($"WMS intenta generar una entrega de {producto.Cantidad_Total} mayor a {oSolicitudDevolucion.Lines.Quantity} en SAP para el material {vCodigoProductoSAP}.")
                    End If

                    If vCodigoAnterior <> producto.Codigo_producto Then
                        oDevolucion.Lines.SetCurrentLine(NoLineaEntrega)
                        oDevolucion.Lines.BaseType = 234000032
                        oDevolucion.Lines.ItemCode = vCodigoProductoSAP
                        oDevolucion.Lines.BaseEntry = _Docentry
                        oDevolucion.Lines.BaseLine = vNoLineaOCSAP
                        oDevolucion.Lines.Quantity = producto.Cantidad_Total
                        oDevolucion.Lines.Add()
                        NoLineaEntrega += 1
                        vCodigoAnterior = producto.Codigo_producto

                        Lista_A_Actualizar.AddRange(lINavTransaccionesOut.Where(Function(x) x.No_pedido = _Docentry AndAlso x.No_linea = vNoLineaOCSAP AndAlso x.Codigo_producto = producto.Codigo_producto AndAlso Not x.Enviado))
                    End If

                Next

            Next

            If oDevolucion.CardCode <> oSolicitudDevolucion.CardCode Then
                Throw New Exception("CardCode origen y destino no coinciden.")
            End If

            oDevolucion.Comments = $"Documento generado por WMS Pedido: {BePedidoEnc.IdPedidoEnc} Despacho: {BePedidoEnc.No_despacho} Solicitud SAP: {_Docentry}"

            Dim resultado As Integer = oDevolucion.Add()
            If resultado <> 0 Then
                Throw New Exception("#ERROR_SAP_" & resultado & ": " & oCompany.GetLastErrorDescription())
            End If

            Dim actualizado = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)
            If actualizado = 0 Then
                Throw New Exception("La devolución se envió a SAP pero no se marcaron como enviados en WMS.")
            End If

            clsTransaccion.Commit_Transaction()

            Return True

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error al enviar devolución a SAP: {MethodBase.GetCurrentMethod.Name()} {ex.Message}", "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al enviar devolución a SAP: {vbNewLine}{ex.Message}")
            Return False
        Finally
            If conn IsNot Nothing Then
                sapPool.ReleaseConnection(conn)
            End If
            clsTransaccion.Close_Conection()
        End Try

    End Function

End Class