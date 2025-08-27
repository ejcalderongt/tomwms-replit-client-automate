Imports System.Reflection
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPSPedidoCliente : Inherits clsInterfaceBase

    Implements IDisposable

    Shared VContadorBitacoraTOMWMS As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0
    Shared lRetCode, lErrCode As Long
    Shared sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Shared BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Shared Function Get_Pedidos_Cliente_SAP(Optional ByVal pPedidoCliente As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Pedidos_Cliente_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            'Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            'If lRetCode <> 0 Then
            '    oCompany.GetLastError(lErrCode, sErrMsg)
            '    Throw New Exception(sErrMsg)
            'Else

            '    If BeConfigEnc Is Nothing Then
            '        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
            '    End If

            '    BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

            '    Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            '    Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

            '    Dim SAP_OV As String = "SELECT 
            '                            T0.DocEntry,
            '                            T0.DocNum,
            '                            T0.DocDate,
            '                            T0.CardCode AS CARDCODE,
            '                            T0.CardName AS CARDNAME,
            '                            T0.DocCur,
            '                            T0.DocTotal,
            '                            T0.JrnlMemo,
            '                            T0.Canceled,
            '                            T0.DocStatus,
            '                            CASE 
            '                                WHEN T0.DocType = 'I' THEN 'ARTICULO'    
            '                                ELSE 'SERVICIO'    
            '                            END AS TIPO_ORDEN_VENTA,
            '                            (SELECT TOP 1 D0.WhsCode 
            '                                FROM RDR1 D0 
            '                                WHERE D0.DocEntry = T0.DocEntry) AS BODEGA,
            '                            T0.Comments,
            '                            T0.NumAtCard                                        
            '                        FROM ORDR T0 
            '                        WHERE T0.DocStatus = 'O' 
            '                        AND T0.CreateDate >= '2024-01-01 00:00:00.000' 
            '                        AND T0.U_Enviado_WMS = 2 " &
            '                        IIf(pPedidoCliente <> "", " AND T0.DocNum  = " & pPedidoCliente, "") & "                                    
            '                        ORDER BY T0.DocEntry DESC"

            '    RsEnc.DoQuery(SAP_OV)

            '    Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

            '    While RsEnc.EoF = False

            '        '#CKFK20240528 Cambiando DOCENTRY por DOCNUM 
            '        BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
            '        BePedidoWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
            '        BePedidoWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
            '        BePedidoWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
            '        BePedidoWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
            '        BePedidoWMS.Status = 1
            '        BePedidoWMS.Transfer_from_Code = RsEnc.Fields.Item("BODEGA").Value
            '        BePedidoWMS.Transfer_from_Contact = "MI3_NAME"
            '        BePedidoWMS.Transfer_from_Name = "MI3_NAME"
            '        BePedidoWMS.Transfer_to_Code = RsEnc.Fields.Item("CARDCODE").Value
            '        BePedidoWMS.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
            '        BePedidoWMS.Transfer_to_Name = RsEnc.Fields.Item("CARDNAME").Value
            '        BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
            '        BePedidoWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value
            '        BePedidoWMS.Manufacturing_Process = 0

            '        Dim n As Integer = 1
            '        Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            '        Dim query_det As String

            '        '#CKFK20240528 Cambiando DOCENTRY por DOCNUM 
            '        query_det = "SELECT 
            '                     T0.LineNum, 
            '                     T0.ITEMCODE, 
            '                     T0.DSCRIPTION, 
            '                     T0.QUANTITY, 
            '                     T0.PRICE, 
            '                     T0.LINETOTAL, 
            '                     T0.VATSUM, 
            '                     T0.DOCENTRY,  
            '                     T0.WHSCODE, 
            '                     T1.U_Um_Prod AS UNIDAD_MEDIDA   
            '                     FROM RDR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode    
            '                     WHERE T0.DOCENTRY = '" & BePedidoWMS.No & "'"

            '        RsDet.DoQuery(query_det)

            '        BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

            '        While RsDet.EoF = False

            '            BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()
            '            BePedidoDetWMS.NoEnc = BePedidoWMS.No
            '            BePedidoDetWMS.No = clsLnTrans_pe_det.MaxID() + 1
            '            BePedidoDetWMS.Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
            '            BePedidoDetWMS.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
            '            BePedidoDetWMS.Shipment_Date = Date.Now
            '            BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
            '            BePedidoDetWMS.Description = RsDet.Fields.Item("dscription").Value.ToString()
            '            BePedidoDetWMS.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
            '            BePedidoDetWMS.Status = 1
            '            BePedidoDetWMS.Variant_Code = Nothing
            '            BePedidoDetWMS.Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
            '            BePedidoDetWMS.Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
            '            BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

            '            n += 1

            '            RsDet.MoveNext()

            '        End While

            '        lPedidosCliente.Add(BePedidoWMS)

            '        RsEnc.MoveNext()

            '    End While

            'End If

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Async Function Procesar_Pedidos_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                             ByVal prg As Windows.Forms.ProgressBar,
                                                             ByVal cnnLog As SqlConnection,
                                                             Optional pPedidoCliente As String = "") As Task(Of Boolean)

        Dim Resultado As String = ""

        Try

            Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

            lPedidosTrasladoSAP = Get_Pedidos_Cliente_SAP(pPedidoCliente)

            Dim BeClienteWMS As New clsBeCliente

            If lPedidosTrasladoSAP IsNot Nothing Then

                If lPedidosTrasladoSAP.Count = 0 Then

                    If pPedidoCliente <> "" Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar con el No.: {0} {1}", pPedidoCliente, vbNewLine))
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay pedidos de cliente pendientes de importar {0}", vbNewLine))
                    End If

                Else

                    For Each PedidoClienteSAP In lPedidosTrasladoSAP

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido de Cliente (ORDR) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

                        BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                        If BeClienteWMS Is Nothing Then

                            If Await Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code) Then
                                clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                            End If

                        End If

                        Dim BePedidoEncResult As clsBeTrans_pe_enc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(PedidoClienteSAP,
                                                                                                                                               Resultado)

                        If Not BePedidoEncResult Is Nothing Then

                            Marcar_Pedido_Cliente_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                            If PedidoClienteSAP.Manufacturing_Process Then
                                '#EJC202404011228: Insertar tarea de manufactura.
                                clsLnTrans_manufactura_enc.Insertar_Manufactura_Por_Defecto(PedidoClienteSAP,
                                                                                            BePedidoEncResult,
                                                                                            BeConfigEnc)
                            End If

                        End If

                        clsPublic.Actualizar_Progreso(lblprg, Resultado)

                    Next

                End If

            End If

            Return True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Public Shared Async Function Importar_Pedido_Cliente_SAP(ByVal lblprg As RichTextBox,
                                                         ByVal prg As Windows.Forms.ProgressBar,
                                                         Optional ByVal ForzarEjecucion As Boolean = False,
                                                         Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                         Optional ByVal pPedidoCliente As String = "") As Task(Of Boolean)

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim resultadoFinal As Boolean = False

        Try
            If Not ForzarEjecucion Then
                If Not Ejecutar_Interfaz("Pedido traslado") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento.")
                    Return False
                End If
            End If

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
            BeNavEjecRes = BeNavEjecucionRes

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            Dim ejecucionExitosa As Boolean = False

            If Not Pregunta_Si_LLena_Intermedia Then
                ejecucionExitosa = Await Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente)
            Else
                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    ejecucionExitosa = Await Procesar_Pedidos_Cliente_SAP(lblprg, prg, CnnLog, pPedidoCliente)
                End If
            End If

            If Not ejecucionExitosa Then
                Return False
            End If

            BeNavEjecucionRes.Exitosa = True
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)
            resultadoFinal = True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, CnnLog)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))
            resultadoFinal = False
        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

        Return resultadoFinal
    End Function


    Public Shared Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(0)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        If Enviar_Entrega_Mercancia_OV_SAP2(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

    Public Shared Function Marcar_Pedido_Cliente_Sincronizado_SAP(ByVal pNoDocumento As String,
                                                                   ByVal EstadoEnvio As Estado_Enviado_SAP,
                                                                   ByVal lblprg As RichTextBox) As Boolean

        Marcar_Pedido_Cliente_Sincronizado_SAP = False

        Try

            clsPublic.Actualizar_Progreso(lblprg, $"Actualizando el estado enviado = {EstadoEnvio} para permitir importación nuevamente en pedido de cliente: {pNoDocumento}")

            Dim updateQuery As String = $"UPDATE ORDR 
                                        SET ""U_EnviadoWMS"" = '{CInt(EstadoEnvio)}'
                                        WHERE ""DocEntry"" = '{pNoDocumento}'"

            Dim filasAfectadas As Integer = HanaHelper.Xcute(updateQuery)

            If filasAfectadas > 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")
                Marcar_Pedido_Cliente_Sincronizado_SAP = True
            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se encontró ningún documento con el DocEntry proporcionado.")
            End If

        Catch ex As Exception
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {ex.Message}")
        End Try

    End Function

    Public Function Cerrar_Lineas_Documento_Salida(ByVal _Docentry As Integer,
                                                ByRef lblprg As RichTextBox) As Boolean

        Cerrar_Lineas_Documento_Salida = False

        Try
            clsPublic.Actualizar_Progreso(lblprg, $"Cerrando documento de traslado SAP con DocEntry = {_Docentry}...")

            Dim updateQuery As String = $"
            UPDATE WTQ1
            SET ""LineStatus"" = 'C'
            WHERE ""DocEntry"" = {_Docentry}"

            Dim filasAfectadas As Integer = HanaHelper.Xcute(updateQuery)

            If filasAfectadas > 0 Then
                clsPublic.Actualizar_Progreso(lblprg, "Documento de traslado cerrado exitosamente.")
                Cerrar_Lineas_Documento_Salida = True
            Else
                clsPublic.Actualizar_Progreso(lblprg, $"No se encontró el documento SAP con DocEntry: {_Docentry}")
            End If

        Catch errMsg As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error al cerrar líneas de traslado en SAP HANA: {MethodBase.GetCurrentMethod.Name()} {errMsg.Message}",
                                                  "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lblprg, $"Error al cerrar líneas de traslado SAP: {vbNewLine}{errMsg.Message}{vbNewLine}")
            Throw New Exception($"{MethodBase.GetCurrentMethod.Name()} {errMsg.Message}")
        End Try

    End Function

    Public Shared Function Enviar_Entrega_Mercancia_OV_SAP2(ByVal _Docentry As Integer,
                                                            ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                            ByRef lblprg As RichTextBox,
                                                            ByRef prg As Windows.Forms.ProgressBar) As Boolean

        Enviar_Entrega_Mercancia_OV_SAP2 = False
        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Try

            Application.DoEvents()

            clsPublic.Actualizar_Progreso(lblprg, $"Generando entrega mediante Service Layer para OV DocEntry = {_Docentry}...")

            Dim lineas = lINavTransaccionesOut.
                GroupBy(Function(x) New With {x.Codigo_producto, x.No_linea}).
                Select(Function(g) New With {
                    .ItemCode = g.Key.Codigo_producto,
                    .BaseLine = g.Key.No_linea,
                    .Quantity = g.Sum(Function(x) x.Cantidad),
                    .BaseEntry = _Docentry,
                    .BaseType = 17
                }).ToList()

            Dim dtOV As DataTable = HanaHelper.OpenDT($"SELECT ""CardCode"" FROM ORDR WHERE ""DocEntry"" = {_Docentry}")
            If dtOV.Rows.Count = 0 Then
                Throw New Exception("No se encontró la orden de venta para generar la entrega.")
            End If

            Dim cardCode As String = dtOV.Rows(0)("CardCode").ToString()

            Dim payload As New With {
                .CardCode = cardCode,
                .DocDate = DateTime.Today.ToString("yyyy-MM-dd"),
                .DocDueDate = DateTime.Today.ToString("yyyy-MM-dd"),
                .DocumentLines = lineas
            }

            Dim client As New SapServiceLayerClient()
            'Dim result = client.PostDeliveryAsync(payload)
            clsPublic.Actualizar_Progreso(lblprg, "Entrega generada exitosamente en SAP a través de Service Layer.")

            Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(
                lINavTransaccionesOut.Where(Function(x) Not x.Enviado).ToList())
            If IResult = 0 Then
                Throw New Exception("Se generó la entrega pero no se actualizaron los registros como enviados.")
            End If

            Return True

        Catch ex As Exception
            clsLnI_nav_ejecucion_det_error.Inserta_Log($"Error en Service Layer: {MethodBase.GetCurrentMethod.Name()} {ex.Message}",
                                                      "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet)
            clsPublic.Actualizar_Progreso(lblprg, $"Error Service Layer: {vbNewLine}{ex.Message}{vbNewLine}")
            Throw
        End Try

    End Function

    Public Shared Async Function Inserta_Cliente_SAP(ByVal pCodigo As String) As Task(Of Boolean)

        Dim clsTransaccion As New clsTransaccion

        Try
            Dim client = New SapServiceLayerClient()
            Dim bp As BusinessPartnerDto = Await client.GetBusinessPartnerAsync(pCodigo)

            If bp Is Nothing Then
                Throw New Exception("No se encontró el cliente en SAP HANA con CardCode: " & pCodigo)
            End If

            clsTransaccion.Open_Connection()

            Dim BeCliente As New clsBeCliente With {
                .IdCliente = clsLnCliente.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdPropietario = BeConfigEnc.IdPropietario,
                .Codigo = bp.CardCode,
                .Nombre_comercial = bp.CardName,
                .Sistema = True,
                .Activo = True,
                .IdEmpresa = BeConfigEnc.Idempresa,
                .Nit = bp.CardCode,
                .IdTipoCliente = 1,
                .Es_bodega_recepcion = False,
                .Es_Bodega_Traslado = False
            }

            clsLnCliente.Insertar(BeCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            Dim BeClienteBodega As New clsBeCliente_bodega With {
                .IdClienteBodega = clsLnCliente_bodega.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1,
                .IdCliente = BeCliente.IdCliente,
                .IdBodega = BeConfigEnc.Idbodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now,
                .Cliente = BeCliente
            }

            clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                        clsTransaccion.lConnection,
                                                        clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

            Return True

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proveniente de SAP: " & ex.Message)
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Function

End Class