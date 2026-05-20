Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports SAPbobsCOM

Public Class clsSyncSAPAjustes : Inherits clsInterfaceBase

    Implements IDisposable
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""
    Private oCompany As Company
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Public Function Procesar_Ajustes_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                         ByVal lblprg As RichTextBox,
                                         ByRef prg As Windows.Forms.ProgressBar,
                                         ByRef cnnLog As SqlConnection) As Boolean
        Procesar_Ajustes_SAP = False

        Dim Resultado As String = ""

        Try

            Dim lPedidosTraslado As New List(Of clsBeI_nav_ped_traslado_enc)
            Dim BeClienteWMS As New clsBeCliente
            Dim vCodigoBodegaOrigen As String = ""
            Dim vCodigoBodegaDestino As String = ""
            Dim vBodegaDestinoEsWMS As Boolean = False
            Dim vBodegaOrigenEsWMS As Boolean = False
            Dim BeBodega As New clsBeBodega()
            Dim BeBodegaDestino As New clsBeBodega

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega)

            If Not BeBodega Is Nothing Then

                lPedidosTraslado = Get_Ajustes_SAP(BeBodega.Codigo)

                For Each SolicitudTraslado In lPedidosTraslado

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando solicitud de traslado SAP (OWTQ) : {0}/{1}{2}", SolicitudTraslado.No, SolicitudTraslado.Receipt_Document_Reference, vbNewLine))

                    vCodigoBodegaOrigen = SolicitudTraslado.Transfer_from_Code
                    vCodigoBodegaDestino = SolicitudTraslado.Transfer_to_Code

                    BeClienteWMS = clsLnCliente.Existe(SolicitudTraslado.Transfer_to_Code)

                    If BeClienteWMS Is Nothing Then
                        Inserta_Cliente_SAP(SolicitudTraslado.Transfer_to_Code)
                        clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & SolicitudTraslado.Transfer_to_Code & " No existía en WMS y fue insertado.")
                    End If

                    vBodegaOrigenEsWMS = clsLnBodega_area.Existe_Codigo_By_IdBodega(vCodigoBodegaOrigen, BeConfigEnc.Idbodega)
                    vBodegaDestinoEsWMS = clsLnBodega_area.Existe_Codigo_By_IdBodega(vCodigoBodegaDestino, BeConfigEnc.Idbodega)

                    If vBodegaOrigenEsWMS AndAlso (BeBodega.Codigo = vCodigoBodegaOrigen) AndAlso Not vBodegaDestinoEsWMS Then
                        clsPublic.Actualizar_Progreso(lblprg, $"El traslado es de almacén general hacia la bodega: {SolicitudTraslado.Transfer_to_Code} - {SolicitudTraslado.Transfer_to_Name} que no está en wms, (pero no afectará el inventario en WMS).")
                    Else

                        clsPublic.Actualizar_Progreso(lblprg, $"El traslado es de almacén general hacia la bodega: {SolicitudTraslado.Transfer_to_Code} - {SolicitudTraslado.Transfer_to_Name} que existe en wms, (se generará la tarea de cambio de ubicación).")
                        '#CKFK20240222 Vamos a agregar la funcionalidad del cambio de ubicación
                        If clsLnI_nav_ped_traslado_enc.Importar_Traslado_A_Tabla_Intermedia(SolicitudTraslado, Resultado) = 1 Then
                            If Marcar_PI_Sincronizado_SAP(SolicitudTraslado.No) Then
                                Procesar_Ajustes_SAP = True
                            End If
                        End If
                    End If

                    'Cuando es pedido.
                    If Not vBodegaDestinoEsWMS Then
                        'Generar salida (Tomar en consideración el área de origen)
                        Dim BePedidoEnc As New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(SolicitudTraslado, lblprg, Nothing, Nothing)

                        If BePedidoEnc Is Nothing Then
                            If Marcar_PI_Sincronizado_SAP(SolicitudTraslado.No) Then
                                Procesar_Ajustes_SAP = True
                            End If
                        End If
                    End If

                    'Cuando es ingreso.
                    If Not vBodegaOrigenEsWMS Then

                        Dim BePedCompraEnc As New clsBeI_nav_ped_compra_enc
                        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
                        Dim vResult As String = ""

                        'Crear documento de ingreso.
                        If Not clsLnProveedor.Existe_Proveedor(SolicitudTraslado.Transfer_from_Code) Then

                            BeConfigEnc = BeConfigEnc

                            If clsSyncSAPPedidoCompra.Inserta_Proveedor_Desde_SAP(SolicitudTraslado.Transfer_from_Code, cnnLog) Then
                                clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & SolicitudTraslado.Transfer_from_Code & " No existía en WMS y fue insertado.")
                            End If

                        End If

                        '#CKFK20240222 Debo llenar los objetos necesarios a partir del objeto SolicitudTraslado
                        If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BePedCompraEnc,
                                                                                BePedidoCompraEnc,
                                                                                vResult) Then
                            Marcar_PI_Sincronizado_SAP(SolicitudTraslado.No)

                        End If
                    End If

                    'Cuando es un cambio de estado dirigido con cambio de ubicación. (Traslado entre bodegas de SAP, que son estados.)
                    If vBodegaOrigenEsWMS AndAlso vBodegaDestinoEsWMS Then
                        'Generar cambio de estado dirigido.
                        Procesar_Ajustes_SAP = True
                    End If

                    'Validar si la bodega destino es una bodega de WMS.
                    BeBodegaDestino = clsLnBodega.GetSingle_By_Codigo(SolicitudTraslado.Transfer_to_Code)

                    '#CKFK20240222 
                    If BeBodega IsNot Nothing AndAlso BeBodegaDestino IsNot Nothing Then
                        'Es una transferencia interbodegas WMS.
                        Dim BePedidoEnc As New clsBeTrans_pe_enc
                        BePedidoEnc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(SolicitudTraslado, lblprg, Nothing, Nothing)

                        If Not BePedidoEnc Is Nothing Then
                            If Marcar_PI_Sincronizado_SAP(SolicitudTraslado.No) Then
                                Procesar_Ajustes_SAP = True
                            End If
                        End If
                    End If

                    clsPublic.Actualizar_Progreso(lblprg, Resultado)

                Next

            Else

                Throw New Exception("ERROR_202311271751: Error no se pudo obtener el objeto de bodega asociado a la configuraciòn de interface: " & BeI_nav_config_enc.Idbodega)

            End If

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function
    Public Function Marcar_PI_Sincronizado_SAP(ByVal pNoDocumento As String) As Boolean

        Marcar_PI_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim oTransferenciaStock As StockTransfer
                oTransferenciaStock = oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest)

                If oTransferenciaStock.GetByKey(pNoDocumento) Then

                    Try

                        oTransferenciaStock.UserFields.Fields.Item("U_EnviadoWMS").Value = "1"
                        oTransferenciaStock.Update()

                    Catch e As Exception
                        Throw e
                    End Try

                End If

            End If

            oCompany.Disconnect()

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Private Function Get_Ajustes_SAP(ByVal pCodigoBodegaInterface As String) As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Ajustes_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim BePedidoDetLotesWMS As New clsBeI_nav_ped_traslado_det_lote
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim vIdMaxDet As Integer = 0

        Try

            '#EJC20240314: Prueba de commit desde beco laptop
            '#CKFK20240314 Aqui va mi commit de prueba desde mi compu
            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                If BeConfigEnc Is Nothing Then
                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
                End If

                If BeConfigEnc Is Nothing Then
                    Throw New Exception("#ERROR_202401011209: No se pudo obtener la configuración de interface para el Id:  " & BD.Instancia.IdConfiguracionInterface)
                End If

                BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

                If BePropietario Is Nothing Then
                    Throw New Exception("#ERROR_202401011209: No se pudo obtener el propietario asociado a la configuración de interface para el IdPropietario:  " & BeConfigEnc.IdPropietario)
                End If

                Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Traslado_SAP)

                Dim StartDate As String = "12142022"
                Dim vCriteria As String = ""
                Dim isFirstCriterion As Boolean = True

                For Each FiltroCategoria In lFiltros
                    If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then
                        If Not isFirstCriterion Then
                            vCriteria += ", "
                        End If
                        vCriteria += "'" & FiltroCategoria.Valor & "'"
                        isFirstCriterion = False
                    ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                        StartDate = FiltroCategoria.Valor
                    End If
                Next

                Dim SAP_Ajustes As String = "SELECT DISTINCT  
                                                T0.DocEntry,
                                                T0.DocNum, 
                                                T0.DocDate, 
                                                T0.CardName, 
                                                T0.Comments AS JRNLMEMO, 
                                                T0.Canceled, 
                                                T0.DocStatus, 
                                                'AJCANTP' AS Tipo_Documento, 
                                                (SELECT TOP 1 T1.WhsCode FROM IGN1 T1 WHERE T1.DocEntry = T0.DocEntry) AS CODIGO_BODEGA_DESTINO, 
                                                (SELECT TOP 1 OWHS.WhsName FROM IGN1 T1 INNER JOIN OWHS ON T1.WhsCode = OWHS.WhsCode WHERE T1.DocEntry = T0.DocEntry) AS NOMBRE_BODEGA_DESTINO 
                                            FROM 
                                                OIGN T0 
                                            WHERE 
                                                T0.DocDate BETWEEN '2024-01-01' AND '2024-01-31' 
                                                AND T0.CANCELED = 'N' 
                                                AND T0.DocStatus = 'O' 
	                                            AND T0.U_EnviadoWMS = 2; "

                'If Not String.IsNullOrEmpty(vCriteria) Then
                '    SAP_Ajustes += " AND (T1.FromWhsCod IN (" & vCriteria & ")
                '               OR T1.WhsCode IN (" & vCriteria & "))
                '          ORDER BY T0.DocEntry DESC"
                'Else
                '    ' Asumiendo que pCodigoBodegaInterface es una variable ya definida en otro lugar del código
                '    SAP_Ajustes += " AND (T1.FromWhsCod = " & pCodigoBodegaInterface &
                '            " OR T1.WhsCode = " & pCodigoBodegaInterface & ")
                '          ORDER BY T0.DocEntry DESC "
                'End If

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

                RsEnc.DoQuery(SAP_Ajustes)

                Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

                While RsEnc.EoF = False

                    BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
                    BePedidoWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BePedidoWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Status = 1
                    BePedidoWMS.Transfer_from_Code = RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value
                    BePedidoWMS.Transfer_from_Contact = RsEnc.Fields.Item("JRNLMEMO").Value
                    BePedidoWMS.Transfer_from_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value
                    BePedidoWMS.Transfer_to_Code = RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value
                    BePedidoWMS.Transfer_to_Contact = IIf(IsDBNull(RsEnc.Fields.Item("CARDNAME").Value), "", RsEnc.Fields.Item("CARDNAME").Value)
                    BePedidoWMS.Transfer_to_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value
                    BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
                    BePedidoWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT 
                                    T0.LineNum AS LINENUM,
                                    T0.ItemCode AS ITEMCODE,
                                    T0.Dscription AS DSCRIPTION,
                                    T0.Quantity AS QUANTITY,
                                    T0.Price AS PRICE,
                                    T0.LineTotal AS LINETOTAL,
                                    T0.VatSum AS VATSUM,
                                    T0.DocEntry AS DOCENTRY,
                                    T0.WhsCode AS WHSCODE,
                                    T1.U_Um_Prod AS UNIDAD_MEDIDA  
                                    FROM IGN1 T0 
                                    INNER JOIN OITM T1 ON T1.ItemCode = T0.ItemCode   
                                    WHERE T0.DOCENTRY = '" & BePedidoWMS.No & "' 
                                    AND T0.FROMWHSCOD = '" & BePedidoWMS.Transfer_from_Code & "' 
                                    AND T0.WHSCODE = '" & BePedidoWMS.Transfer_to_Code & "'"

                    RsDet.DoQuery(query_det)

                    BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                    Dim lineNo As Integer = 0

                    vIdMaxDet = clsLnI_nav_ped_traslado_det.MaxID() + 1

                    While Not RsDet.EoF

                        BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()

                        With BePedidoDetWMS
                            .NoEnc = BePedidoWMS.No
                            .No = vIdMaxDet + 1
                            .Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                            .Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
                            .Shipment_Date = Date.Now
                            .Quantity = Convert.ToInt32(RsDet.Fields.Item("QUANTITY").Value)
                            .Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
                            .Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                            .Status = 1
                            .Variant_Code = Nothing
                            .Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
                            .Transfer_From_CodeField = RsDet.Fields.Item("FROMWHSCOD").Value.ToString()
                            .Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                        End With

                        Dim vsql_lotes_ajustes As String = "SELECT 
                                                                IBT1.BatchNum, 
                                                                IBT1.ItemCode, 
                                                                IBT1.Quantity, 
                                                                IBT1.WhsCode, 
                                                                OBTN.ExpDate,
                                                                IBT1.LineNum
                                                            FROM 
                                                                IBT1 
                                                            INNER JOIN 
                                                                OBTN ON IBT1.BatchNum = OBTN.DistNumber AND IBT1.ItemCode = OBTN.ItemCode
                                                            WHERE 
                                                                IBT1.BaseType = 59  
                                                             AND IBT1.BaseEntry = '" & BePedidoWMS.No & "' 
                                                             AND IBT1.WHSCODE = '" & BePedidoDetWMS.Transfer_to_CodeField & "'
                                                             AND IBT1.ItemCode = '" & BePedidoDetWMS.Item_No & "'"

                        'AND IBT1.LINENUM = '" & BePedidoDetWMS.Line_No & "' 

                        Dim RsDetLotes As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                        RsDetLotes.DoQuery(vsql_lotes_ajustes)

                        While Not RsDetLotes.EoF

                            BePedidoDetLotesWMS = New clsBeI_nav_ped_traslado_det_lote

                            With BePedidoDetLotesWMS
                                .NoEnc = BePedidoWMS.No
                                .No = RsDetLotes.Fields.Item("ITEMCODE").Value.ToString()
                                .Line_No = RsDetLotes.Fields.Item("LINENUM").Value.ToString()
                                .Batch_No = RsDetLotes.Fields.Item("BatchNum").Value.ToString()
                                .Serial_No = ""
                                .Quantity_Base = Convert.ToInt32(RsDetLotes.Fields.Item("QUANTITY").Value)
                                .Expiration_Date = IIf(IsDBNull(RsDetLotes.Fields.Item("ExpDate").Value), New Date(1900, 1, 1), RsDetLotes.Fields.Item("ExpDate").Value)
                                .WhsFrom = BePedidoDetWMS.Transfer_From_CodeField
                                .WhsTo = RsDetLotes.Fields.Item("WhsCode").Value.ToString()
                                .Fec_Agr = Date.Now
                            End With

                            BePedidoDetWMS.Lotes_Detalle.Add(BePedidoDetLotesWMS)

                            RsDetLotes.MoveNext()

                        End While

                        BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

                        lineNo += 1 : vIdMaxDet += 1

                        RsDet.MoveNext()

                    End While

                    lPedidosCliente.Add(BePedidoWMS)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCliente

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function Importar_Ajustes_SAP(ByRef lblprg As RichTextBox,
                                             ByRef prg As Windows.Forms.ProgressBar,
                                             Optional ByVal ForzarEjecucion As Boolean = False,
                                             Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean
        Importar_Ajustes_SAP = False

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Resultado As String = ""

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Pedido traslado") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

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

            BeNavEjecRes = BeNavEjecucionRes

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Ajustes_SAP(BeConfigEnc, lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Ajustes_SAP(BeConfigEnc, lblprg, prg, CnnLog) Then
                        Exit Function
                    End If
                End If

            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw ex

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function
    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
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

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio()

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

                        If Enviar_Entrega_Mercancia_Traslado_SAP(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet,
                                                                           CnnLog)

                            End Try

                        End If

                    End If

                Next

            End If

        Catch ex As Exception
            Throw ex
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub
    Public Function Enviar_Entrega_Mercancia_Traslado_SAP(ByVal _Docentry As Integer,
                                                          ByVal lINav_Transaccioens_Out As List(Of clsBeI_nav_transacciones_out),
                                                          ByRef lblprg As RichTextBox,
                                                          ByRef prg As Windows.Forms.ProgressBar) As Boolean


        prg.Maximum = lINav_Transaccioens_Out.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)

        Try

            Dim lRetCode As Integer = 0
            Dim errMsg As String = ""
            Dim ErrNo As Integer = 0

            Conectar_A_SAP(oCompany, lRetCode, errMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then
                If errMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & errMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & errMsg)
                End If
            Else

                Dim oEntrega As Documents
                Dim oInventarioTransferRequest As Documents
                Dim BaseLine As Integer = 0

                oEntrega = CType(oCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes), Documents)
                oInventarioTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), Documents)

                If oInventarioTransferRequest.GetByKey(_Docentry) Then

                    Console.WriteLine(oInventarioTransferRequest.DocumentStatus)

                    oEntrega.CardCode = oInventarioTransferRequest.CardCode
                    oEntrega.DocDate = Date.Today
                    oEntrega.DocObjectCode = BoObjectTypes.oDeliveryNotes

                    For j As Integer = 0 To oInventarioTransferRequest.Lines.Count - 1

                        oInventarioTransferRequest.Lines.SetCurrentLine(j)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", oInventarioTransferRequest.Lines.ItemCode.ToString()))

                        Dim BeInavTransaccioensOut As clsBeI_nav_transacciones_out = New clsBeI_nav_transacciones_out()
                        BeInavTransaccioensOut = lINav_Transaccioens_Out.Find(Function(x) x.Codigo_producto = oInventarioTransferRequest.Lines.ItemCode.ToString())

                        If Not BeInavTransaccioensOut Is Nothing Then

                            If Not oInventarioTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then

                                If BeInavTransaccioensOut.Cantidad <= oInventarioTransferRequest.Lines.Quantity Then

                                    Dim vTipoImpuesto As String = oInventarioTransferRequest.Lines.TaxCode

                                    oEntrega.Lines.BaseType = Convert.ToInt32(BoAPARDocumentTypes.bodt_Order)
                                    oEntrega.Lines.ItemCode = oInventarioTransferRequest.Lines.ItemCode
                                    oEntrega.Lines.BaseEntry = _Docentry
                                    oEntrega.Lines.BaseLine = BaseLine
                                    oEntrega.Lines.TaxCode = vTipoImpuesto
                                    oEntrega.Lines.UserFioEntregaelds.Fields.Item("U_Tipo").Value = "B"
                                    oEntrega.Lines.Quantity = BeInavTransaccioensOut.Cantidad
                                    oEntrega.Lines.Add()

                                Else
                                    Throw New Exception("WMS está intentando generar una entrega por: " & BeInavTransaccioensOut.Cantidad & " en una línea de SAP para el material: " & oInventarioTransferRequest.Lines.ItemCode & " que refleja una cantidad de: " & BeInavTransaccioensOut.Cantidad & " probablemente esto no sea posible.")
                                End If

                            Else
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oInventarioTransferRequest.Lines.ItemCode.ToString()))
                            End If

                            lINav_Transaccioens_Out_Enviadas.Add(BeInavTransaccioensOut)

                            BaseLine += 1

                        End If

                    Next

                    Dim oResultado As Integer
                    oResultado = oEntrega.Add()

                    If oResultado <> 0 Then
                        Throw New Exception(oCompany.GetLastErrorDescription())
                    Else

                        Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(lINav_Transaccioens_Out_Enviadas)

                        If IResult = 0 Then
                            Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                        End If

                    End If

                End If

            End If

            Return True

        Catch errMsg As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw errMsg

        Finally
            If oCompany.Connected Then
                oCompany.Disconnect()
            End If

        End Try

    End Function
    Private Function Inserta_Cliente_SAP(ByVal pCodigo As String) As Boolean

        Inserta_Cliente_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim cTrans As New clsTransaccion

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                Dim query_sap As String

                query_sap = "SELECT top 1 T0.CARDCODE AS CODIGO,
                             T0.CARDNAME AS NOMBRE_COMERCIAL,
                             T0.Phone1, 'TEST' AS CONTACTO ,
                             T0.u_nit AS NIT, 
                             T0.Address AS DIRECCION, 
                             T0.E_Mail FROM OCRD T0 
                             WHERE T0.CARDTYPE = 'C'  
                             AND (t0.CARDCODE)= '" & pCodigo & "'"


                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                cTrans.Begin_Transaction()

                Dim BeCliente As New clsBeCliente

                While Not rs.EoF

                    BeCliente.IdCliente = clsLnCliente.MaxID(cTrans.lConnection, cTrans.lTransaction) + 1
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

                    clsLnCliente.Insertar(BeCliente, cTrans.lConnection, cTrans.lTransaction)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(cTrans.lConnection, cTrans.lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                cTrans.lConnection,
                                                                cTrans.lTransaction)

                    cTrans.Commit_Transaction()

                    Dim oBusinessPartnerSBO As BusinessPartners = CType(oCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners), SAPbobsCOM.BusinessPartners)

                    If oBusinessPartnerSBO.GetByKey(pCodigo) Then
                        oBusinessPartnerSBO.UserFields.Fields.Item("U_Enviado_WMS").Value = "1"
                        oBusinessPartnerSBO.Update()
                    End If

                    Inserta_Cliente_SAP = True

                    rs.MoveNext()

                End While

            End If

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            cTrans.Close_Conection()
        End Try

    End Function

End Class
