Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports SAPbobsCOM
Imports TOMWMS.clsDataContractDI

Public Class clsSyncSAPSSolicitudTraslado : Inherits clsInterfaceBase

    Implements IDisposable

    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0
    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Get_Solicitudes_Traslado_SAP(ByVal pCodigoBodegaInterface As String,
                                                  Optional ByVal pPedidoCliente As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Solicitudes_Traslado_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lErrCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

            If BeConfigEnc Is Nothing Then
                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
            End If

            BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

            Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
            Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

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

            Dim SAP_Traslados As String = "SELECT DISTINCT T0.DocEntry,
                                                               T0.DocNum,
                                                               T0.DocDate, 
                                                               T0.CardName, 
                                                               T1.FromWhsCod AS 'CODIGO_BODEGA_ORIGEN',
                                                               OW1.WhsName AS 'NOMBRE_BODEGA_ORIGEN',
                                                               T1.WhsCode AS 'CODIGO_BODEGA_DESTINO',
                                                               OW2.WhsName AS 'NOMBRE_BODEGA_DESTINO',
                                                               T0.Comments AS JRNLMEMO,
                                                               T0.Canceled,
                                                               T0.DocStatus,
                                                               'TRANSFERENCIA' AS Tipo_Transferencia,
                                                               T0.U_ALMDEST
                                               FROM OWTQ T0 INNER JOIN 
                                                    WTQ1 T1 ON T0.docentry = T1.docentry INNER JOIN 
	                                                OWHS OW1 ON T1.FromWhsCod = OW1.WhsCode INNER JOIN 
	                                                OWHS OW2 ON T1.WhsCode = OW2.WhsCode
                                               WHERE T0.DOCSTATUS = 'O' AND 
                                                     T0.U_Enviado_WMS = 2 "

            If pPedidoCliente <> "" Then
                SAP_Traslados += " AND T0.docnum = " & pPedidoCliente
            End If

            If Not String.IsNullOrEmpty(vCriteria) Then
                SAP_Traslados += " AND (T1.FromWhsCod IN (" & vCriteria & ")
                               OR T1.WhsCode IN (" & vCriteria & ")) 
                          ORDER BY T0.DocEntry DESC"
            Else
                ' Asumiendo que pCodigoBodegaInterface es una variable ya definida en otro lugar del código
                SAP_Traslados += " AND (T1.FromWhsCod = " & pCodigoBodegaInterface &
                        " OR T1.WhsCode = " & pCodigoBodegaInterface & ") 
                          ORDER BY T0.DocEntry DESC "
            End If

            RsEnc.DoQuery(SAP_Traslados)

            Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

            While RsEnc.EoF = False

                BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
                BePedidoWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
                BePedidoWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                BePedidoWMS.Status = 1
                BePedidoWMS.Transfer_from_Code = RsEnc.Fields.Item("CODIGO_BODEGA_ORIGEN").Value
                BePedidoWMS.Transfer_from_Contact = "MI3_NAME"
                BePedidoWMS.Transfer_from_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_ORIGEN").Value
                BePedidoWMS.Transfer_to_Code = RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value
                BePedidoWMS.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
                BePedidoWMS.Transfer_to_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value
                BePedidoWMS.Transfer_to_CodeField = RsEnc.Fields.Item("U_ALMDEST").Value
                BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
                BePedidoWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value
                BePedidoWMS.Manufacturing_Process = 0
                BePedidoWMS.Document_Type = tTipoDocumentoSalida.Traslado_Por_Estados_SAP

                Dim n As Integer = 1
                Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim query_det As String

                query_det = "SELECT 
                                 T0.LineNum, 
                                 T0.ITEMCODE, 
                                 T0.DSCRIPTION, 
                                 T0.QUANTITY, 
                                 T0.PRICE, 
                                 T0.LINETOTAL, 
                                 T0.VATSUM, 
                                 T0.DOCENTRY,  
                                 T0.WHSCODE, 
                                 T1.SalUnitMsr AS UNIDAD_MEDIDA   
                                 FROM WTQ1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode    
                                 WHERE T0.DOCENTRY = '" & BePedidoWMS.No & "'"

                RsDet.DoQuery(query_det)

                BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                While RsDet.EoF = False

                    BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()
                    BePedidoDetWMS.NoEnc = BePedidoWMS.No
                    BePedidoDetWMS.No = clsLnTrans_pe_det.MaxID() + 1
                    BePedidoDetWMS.Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                    BePedidoDetWMS.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
                    BePedidoDetWMS.Shipment_Date = Date.Now
                    BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
                    BePedidoDetWMS.Description = RsDet.Fields.Item("DSCRIPTION").Value.ToString()
                    BePedidoDetWMS.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                    BePedidoDetWMS.Status = 1
                    BePedidoDetWMS.Variant_Code = Nothing
                    BePedidoDetWMS.Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
                    BePedidoDetWMS.Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                    BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

                    n += 1

                    RsDet.MoveNext()

                End While

                lPedidosCliente.Add(BePedidoWMS)

                RsEnc.MoveNext()

            End While

            End If


            Return lPedidosCliente

        Catch ex As Exception
            If ex.Message.Contains("El addon ya inicio sesion") Then

            End If
            Throw New Exception(ex.Message)
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Procesar_Solicitudes_Traslado_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                                      ByRef lblprg As RichTextBox,
                                                      ByRef prg As Windows.Forms.ProgressBar,
                                                      ByRef cnnLog As SqlConnection,
                                                      Optional pPedidoCliente As String = "") As Boolean
        Procesar_Solicitudes_Traslado_SAP = False

        Dim Resultado As String = ""
        Dim BeBodega As New clsBeBodega
        Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

        Try

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega)

            If Not BeBodega Is Nothing Then

                lPedidosTrasladoSAP = Get_Solicitudes_Traslado_SAP(BeBodega.Codigo, pPedidoCliente)

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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Traslado (OWTQ) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

                            BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                            If BeClienteWMS Is Nothing Then

                                If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code, lblprg) Then
                                    clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                                End If

                            End If

                            Dim BePedidoEncResult As New clsBeTrans_pe_enc
                            BePedidoEncResult = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP,
                                                                                                                       lblprg,
                                                                                                                       Nothing,
                                                                                                                       Nothing)

                            If Not BePedidoEncResult Is Nothing Then

                                Marcar_Solicitud_Trasladado_Sincronizado_SAP(PedidoClienteSAP.No,
                                                                             Estado_Enviado_SAP.Enviado,
                                                                             lblprg)

                                clsLnLog_error_wms.Agregar_Error(String.Format("#IF_SAP_SOL_TRAS: Se importó el pedido de cliente con Solicitud de Traslado (OWTQ):{0}/{1}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference))

                            End If

                            clsPublic.Actualizar_Progreso(lblprg, Resultado)

                        Next

                    End If

                End If

                Procesar_Solicitudes_Traslado_SAP = True

            Else
                clsPublic.Actualizar_Progreso(lblprg, "ERROR_202405210317: No se obtuvo la bodega para la configuración de la interface.")
            End If

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Public Function Importar_Solicitud_Traslado_SAP(ByRef lblprg As RichTextBox,
                                                    ByRef prg As Windows.Forms.ProgressBar,
                                                    ByVal FlujoEntrada As Boolean,
                                                    Optional ByVal ForzarEjecucion As Boolean = False,
                                                    Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                    Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Solicitud_Traslado_SAP = False

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

            lblprg.Text = ""

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Solicitudes_Traslado_SAP(BeConfigEnc,
                                                         lblprg,
                                                         prg,
                                                         CnnLog,
                                                         pPedidoCliente) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Solicitudes_Traslado_SAP(BeConfigEnc,
                                                                     lblprg,
                                                                     prg,
                                                                     CnnLog,
                                                                     pPedidoCliente) Then
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
                                                      BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw ex

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function

    Public Sub Enviar_Transacciones_De_Salida(ByRef lblprg As RichTextBox,
                                              ByRef prg As Windows.Forms.ProgressBar,
                                              ByVal pTipo As tTipoDocumentoSalida)

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    If PT.No_pedido = "" Then
                        Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc)
                    Else
                        Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_Referencia_TipoDoc(PT.No_pedido, pTipo)
                    End If

                    If Not Enviado_A_Erp Then

                        If PT.No_pedido = "" Then
                            lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.Idpedidoenc = PT.Idpedidoenc)
                        Else
                            lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)
                        End If

                        If Enviar_Transferencia_Stock_SAP(lTransaccionesSalidaSingle, lblprg, prg) Then

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

    ''' <summary>
    ''' Valida que la línea de detalle de la OV en SAP esté abierta para generar la entrega.
    ''' </summary>
    ''' <param name="itemcode"></param>
    ''' <returns></returns>
    Private Function Estado_Linea(ByVal itemcode As String) As String

        Estado_Linea = "O"

        Try

            Dim oOrderSales As Documents
            oOrderSales = CType(oCompany.GetBusinessObject(BoObjectTypes.oOrders), Documents)

            oOrderSales = oCompany.GetBusinessObject(BoObjectTypes.oOrders)

            If oOrderSales.Lines.GetByKey(itemcode) Then
                Estado_Linea = oOrderSales.Lines.LineStatus
            End If

            MsgBox(Estado_Linea)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Inserta_Cliente_SAP(ByVal pCodigo As String, ByVal lblprg As RichTextBox) As Boolean

        Inserta_Cliente_SAP = False

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios
        Dim clsTransaccion As New clsTransaccion
        Dim lFamilias As New List(Of clsBeProducto_familia)
        Dim lClasificacion As New List(Of clsBeProducto_clasificacion)

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

                    clsLnCliente.Insertar(BeCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                clsTransaccion.lConnection,
                                                                clsTransaccion.lTransaction)


                    clsPublic.Actualizar_Progreso(lblprg, "Días vida por defecto clientes perecederos: " & BeConfigEnc.Dias_Vida_Defecto_Perecederos)

                    '#EJC202303031646: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         clsTransaccion.lConnection,
                                                                         clsTransaccion.lTransaction)

                        clsPublic.Actualizar_Progreso(lblprg, "Lista de familias: " & lFamilias.Count)


                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    clsTransaccion.lConnection,
                                                                                    clsTransaccion.lTransaction)

                        clsPublic.Actualizar_Progreso(lblprg, "Lista de clasificaciones: " & lClasificacion.Count)

                        If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                            Dim BeTiempoCliente As New clsBeCliente_tiempos

                            For Each F In lFamilias

                                For Each C In lClasificacion

                                    BeTiempoCliente = New clsBeCliente_tiempos()
                                    BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction) + 1
                                    BeTiempoCliente.IdCliente = BeCliente.IdCliente
                                    BeTiempoCliente.IdFamilia = F.IdFamilia
                                    BeTiempoCliente.IdClasificacion = C.IdClasificacion
                                    BeTiempoCliente.Dias_Local = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.Dias_Exterior = BeConfigEnc.Dias_Vida_Defecto_Perecederos
                                    BeTiempoCliente.User_agr = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.User_mod = BeConfigEnc.IdUsuario
                                    BeTiempoCliente.Fec_agr = Now
                                    BeTiempoCliente.Fec_mod = Now
                                    BeTiempoCliente.Activo = True
                                    clsLnCliente_tiempos.Insertar(BeTiempoCliente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                                Next

                            Next

                        End If

                    End If


                    clsTransaccion.Commit_Transaction()

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
            clsTransaccion.RollBack_Transaction()
            Throw New Exception("No se pudo insertar el cliente nuevo proviniente de SAP: " & ex.Message)
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Enviar_Traslado_Entre_Almacenes_SAP(ByVal _Docentry As Integer,
                                                        ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                        ByRef lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transaccioens_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vCodigoBodegaERP As String = ""
        Dim vAgregarEntrega As Boolean = False
        Dim NoLineaEntrega As Integer = 0
        Dim NoLineaEntregaLote As Integer = 0
        Dim clsTransaccion As New clsTransaccion

        Try
            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then
                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If
            Else
                Dim oTransfer As StockTransfer
                Dim oTransferRequest As StockTransfer
                Dim BaseLine As Integer = 0

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If oTransferRequest.GetByKey(_Docentry) Then

                    oTransfer.CardCode = oTransferRequest.CardCode
                    oTransfer.DocDate = Date.Today
                    oTransfer.DocObjectCode = BoObjectTypes.oStockTransfer

                    NoLineaEntrega = 0
                    NoLineaEntregaLote = 0

                    clsTransaccion.Open_Connection()

                    For j As Integer = 0 To oTransferRequest.Lines.Count - 1

                        oTransferRequest.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oTransferRequest.Lines.ItemCode.ToString()
                        Dim vNoLineaOCSAP As Integer = oTransferRequest.Lines.LineNum

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", vCodigoProductoSAP))

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP _
                                                                              AndAlso x.No_linea = vNoLineaOCSAP).
                                                                        GroupBy(Function(x) New With {Key x.Codigo_producto, Key x.No_linea}).
                                                                        Select(Function(g) New With {
                                                                            g.Key.Codigo_producto,
                                                                            g.Key.No_linea,
                                                                            .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                        }).ToList()

                        If DistinctProductosLineas.Any() Then

                            For Each ProductoSalida In DistinctProductosLineas

                                If Not oTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oTransferRequest.Lines.Quantity Then

                                        Dim nuevaLineaEntrega As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto)

                                        If nuevaLineaEntrega Then

                                            oTransfer.Lines.ItemCode = oTransferRequest.Lines.ItemCode
                                            oTransfer.Lines.BaseEntry = _Docentry
                                            oTransfer.Lines.BaseLine = vNoLineaOCSAP
                                            oTransfer.Lines.BaseType = BoObjectTypes.oInventoryTransferRequest
                                            oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                                            vCodigoAnterior = oTransfer.Lines.ItemCode

                                            oTransfer.Lines.Add()

                                            NoLineaEntrega += 1

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

                            vAgregarEntrega = True
                        End If
                    Next

                    Dim oResultado As Integer
                    oResultado = oTransfer.Add()

                    If oResultado <> 0 Then
                        Dim errMsg = oCompany.GetLastErrorDescription()
                        Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                    Else

                        Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                        If IResult = 0 Then
                            Throw New Exception("Se envió la entrada de mercancía a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                        End If

                        ' Obtener el DocEntry del traslado generado
                        Dim newObjectCode As String = ""
                        oCompany.GetNewObjectCode(newObjectCode)

                        Dim vTrasladoDocEntry As Integer = 0

                        If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                            Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                        End If

                        Dim vDocNumTraslado As Integer = Obtener_Doc_Num_Traslado(oCompany, vTrasladoDocEntry)

                        clsPublic.Actualizar_Progreso(lblprg, "#MSG2407152241: Se creó el traslado con DocNum: " & vDocNumTraslado)

                    End If

                    clsTransaccion.Commit_Transaction()
                End If
            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar entrada de mercancía a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar entrada de mercancía a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))
        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try
    End Function

    Private Function Asigna_Unidad_De_Medida(ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                             ByRef navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                             ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                             ByRef BeProductoBodega As clsBeProducto_bodega,
                                             ByRef lblprg As RichTextBox,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnectionLog As SqlConnection) As Boolean

        Asigna_Unidad_De_Medida = False

        Try

            'Existe el producto con U.M.Bas = U.M. de pedido de compra.
            If Not clsLnProducto.Existe(navPedidoCompraDet.No,
                                        BeUnidadMedidaPedCompra.IdUnidadMedida,
                                        lConnection,
                                        lTransaction) Then

                Dim BePresentacion As New clsBeProducto_Presentacion

                BePresentacion = clsLnProducto_presentacion.
                Existe_By_IdProducto_And_NombrePresentacion(BeProductoBodega.IdProducto,
                                                            navPedidoCompraDet.Unit_of_Measure_Code,
                                                            lConnection,
                                                            lTransaction)

                If Not BePresentacion Is Nothing Then
                    'La presentación ya existe
                    BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre
                Else

                    Dim vFactorConv As Double = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                   BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                   lConnection,
                                                                   lTransaction)

                    'Existe factor para crear la presentación con la unidad de medida del pedido de compra.
                    If vFactorConv > 0 Then

                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1
                        BePresentacion.IdProducto = BeProductoBodega.IdProducto
                        BePresentacion.Codigo_barra = BeProductoBodega.Producto.Codigo_barra + navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Nombre = navPedidoCompraDet.Unit_of_Measure_Code
                        BePresentacion.Imprime_barra = True
                        BePresentacion.Peso = 0
                        BePresentacion.Alto = 0
                        BePresentacion.Largo = 0
                        BePresentacion.Ancho = 0
                        BePresentacion.Factor = vFactorConv
                        BePresentacion.MinimoExistencia = 0
                        BePresentacion.MaximoExistencia = 0
                        BePresentacion.User_agr = BeConfigEnc.IdUsuario
                        BePresentacion.User_mod = BeConfigEnc.IdUsuario
                        BePresentacion.Fec_agr = Now
                        BePresentacion.Fec_mod = Now
                        BePresentacion.Activo = True
                        BePresentacion.EsPallet = False
                        BePresentacion.Precio = 0
                        BePresentacion.MinimoPeso = 0
                        BePresentacion.MaximoPeso = 0
                        BePresentacion.Costo = 0
                        BePresentacion.CamasPorTarima = 0
                        BePresentacion.CajasPorCama = 0
                        BePresentacion.Genera_lp_auto = False
                        BePresentacion.Permitir_paletizar = False
                        BePresentacion.Sistema = True
                        BePresentacion.Codigo = BeProductoBodega.Producto.Codigo

                        Try

                            clsLnProducto_presentacion.Insertar(BePresentacion, lConnection, lTransaction)

                            BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                            BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                            BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BePedidoCompraDet.Codigo_Producto,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       lConnectionLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar presentación: {0}{1}", ex.Message, vbNewLine))

                        End Try

                    Else

                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion 
                                               para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                              navPedidoCompraDet.No,
                                              BeProductoBodega.Producto.UnidadMedida.Nombre,
                                              navPedidoCompraDet.Unit_of_Measure_Code))

                    End If 'Fin Sí: 'Existe factor para crear la presentación con la unidad de medida del pedido de compra.                   

                End If 'Fin sí: Existe presentación.              

            Else
                'La unidad de medida básica del producto es = a la unidad de medida del pedido de compra.
                'Se utiliza la UM del pedido de compra aunque la básica del maestro sea otra porque existe factor de conversión
                BePedidoCompraDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
            End If

            Asigna_Unidad_De_Medida = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Marcar_Solicitud_Trasladado_Sincronizado_SAP(ByVal pNoDocumento As String, ByVal EstadoEnvio As Estado_Enviado_SAP, ByVal lblprg As RichTextBox) As Boolean

        Marcar_Solicitud_Trasladado_Sincronizado_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                clsPublic.Actualizar_Progreso(lblprg, "Actualizando el estado enviado = " & EstadoEnvio)

                Dim osalidaMercancia As StockTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If osalidaMercancia.GetByKey(pNoDocumento) Then

                    Try

                        osalidaMercancia.UserFields.Fields.Item("U_ENVIADO_WMS").Value = EstadoEnvio
                        osalidaMercancia.Update()

                        clsPublic.Actualizar_Progreso(lblprg, "Se actualizó el estado del documento.")

                        Marcar_Solicitud_Trasladado_Sincronizado_SAP = True

                    Catch e As Exception
                        Throw e
                    End Try

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No se pudo obtener el documento de SAP con DocEntry: " & pNoDocumento)
                End If

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Get_Codigos_Bodegas(ByVal IdPedidoEnc As Integer) As clsBeInfoBodegaByIdPedidoEnc

        Get_Codigos_Bodegas = Nothing

        Dim BeInfoBodega As New clsBeInfoBodegaByIdPedidoEnc

        Try

            Dim BePedidoEnc As New clsBeTrans_pe_enc
            clsLnTrans_pe_enc.GetBodegas_By_IdPedidoEnc(IdPedidoEnc, BeInfoBodega.Codigo_Bodega_Origen, BeInfoBodega.Codigo_Bodega_Destino)

            Get_Codigos_Bodegas = BeInfoBodega

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Class clsBeInfoBodegaByIdPedidoEnc

        Public Property Codigo_Bodega_Origen As String = ""
        Public Property Codigo_Bodega_Destino As String = ""


    End Class

    Public Function Enviar_Transferencia_Stock_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                   ByRef lblprg As RichTextBox,
                                                   ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim BodegasByPedido As New Dictionary(Of Integer, clsBeInfoBodegaByIdPedidoEnc)
        Dim vMarcadorWMS_SAP As String = ""
        Dim vDocNum As String = ""

        Enviar_Transferencia_Stock_SAP = False

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                Dim oTransfer As StockTransfer
                Dim BaseLine As Integer = 0
                Dim vDocEntrySolicitud As Integer = 0

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)

                NoLineaTransfer = 0
                NoLineaTransferLote = 0

                clsTransaccion.Open_Connection()


                Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Transferencia_Directa AndAlso x.Enviado = False).
                                                                GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto, Key x.No_pedido}).
                                                                Select(Function(g) New With {
                                                                    .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                    .Codigo_Producto = g.Key.Codigo_producto,
                                                                    .No_Pedido = g.Key.No_pedido,
                                                                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                }).ToList()

                If DistinctIdPedidoEncByTraslado.Any() Then

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

                    For Each ProductoSalida In DistinctIdPedidoEncByTraslado

                        Dim IdPedidoEnc As Integer = ProductoSalida.IdPedidoEnc
                        Dim IdOrdenCompraEnc As Integer = clsLnTrans_oc_enc.Get_IdOrdenCompraEnc_By_IdPedidoEnc(IdPedidoEnc)
                        Dim infoBodega As clsBeInfoBodegaByIdPedidoEnc = Nothing
                        Dim FromWarehouse As String = ""
                        Dim ToWarehouse As String = ""

                        clsPublic.Actualizar_Progreso(lblprg, "Adicionando al pedido " & IdPedidoEnc & " el producto: " & ProductoSalida.Codigo_Producto)

                        If Not BodegasByPedido.TryGetValue(IdPedidoEnc, infoBodega) Then
                            infoBodega = Get_Codigos_Bodegas(IdPedidoEnc)
                        End If

                        vDocEntrySolicitud = Val(ProductoSalida.No_Pedido)

                        FromWarehouse = infoBodega.Codigo_Bodega_Origen
                        ToWarehouse = infoBodega.Codigo_Bodega_Destino

                        vMarcadorWMS_SAP = "Pedido WMS: " & IdPedidoEnc & " Ingreso WMS: " & IdOrdenCompraEnc

                        oTransfer.FromWarehouse = FromWarehouse
                        oTransfer.ToWarehouse = ToWarehouse
                        oTransfer.Comments = vMarcadorWMS_SAP
                        oTransfer.UserFields.Fields.Item("U_FIS").Value = "N"
                        oTransfer.JournalMemo = vMarcadorWMS_SAP

                        Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto)

                        If nuevaLineaTransfer Then

                            oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
                            oTransfer.Lines.ItemCode = ProductoSalida.Codigo_Producto
                            oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                            oTransfer.Lines.FromWarehouseCode = FromWarehouse
                            oTransfer.Lines.WarehouseCode = ToWarehouse
                            vCodigoAnterior = oTransfer.Lines.ItemCode

                            oTransfer.Lines.Add() : NoLineaTransfer += 1

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_linea = NoLineaTransfer _
                                                                                  AndAlso x.Codigo_producto = ProductoSalida.Codigo_Producto _
                                                                                  AndAlso x.Enviado = False)

                            If Not Sublista_A_Actualizar Is Nothing Then
                                If Sublista_A_Actualizar.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                End If
                            End If
                        End If

                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                End If

                Dim oResultado As Integer
                oResultado = oTransfer.Add()

                If oResultado <> 0 Then

                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")

                Else

                    Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                    If IResult = 0 Then
                        Throw New Exception("Se envió el traslado entre almacenes a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                    End If

                    ' Obtener el DocEntry del traslado generado
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    Dim vTrasladoDocEntry As Integer = 0

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    vDocNum = Obtener_Doc_Num_Traslado(oCompany, vTrasladoDocEntry)

                    clsPublic.Actualizar_Progreso(lblprg, "#MSG2407152241: Se creó el traslado con DocNum: " & vDocNum)

                    Dim oTransferRequest As StockTransfer
                    oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                    If Not (vDocEntrySolicitud = 0) Then
                        If oTransferRequest.GetByKey(vDocEntrySolicitud) Then
                            oTransferRequest.Close()
                        End If
                    End If

                End If

                clsTransaccion.Commit_Transaction()

                vMarcadorWMS_SAP = "#IF_SAP_ENV_TRANS_STOCK: Se envió a SAP la transferencia con DocNum" & vDocNum & " para el " & vMarcadorWMS_SAP
                clsLnLog_error_wms.Agregar_Error(vMarcadorWMS_SAP)

            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            clsPublic.Actualizar_Progreso(lblprg, errMsg.Message)

        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Enviar_Traslados_Desde_Solicitud(ByRef lblprg As RichTextBox,
                                                     ByRef prg As Windows.Forms.ProgressBar,
                                                     ByVal pTipo As tTipoDocumentoSalida) As Boolean

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)

        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Enviar_Traslados_Desde_Solicitud = False

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})

                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Pedido: {0}-{1}", PT.Idpedidoenc, PT.No_pedido))

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP(PT.No_pedido)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalidaSingle = lTransaccionesSalida.FindAll(Function(x) x.No_pedido = PT.No_pedido)

                        Dim vResult As Boolean = Enviar_Traslado_Desde_Solicitud_SAP(PT.No_pedido, lTransaccionesSalidaSingle, lblprg, prg)

                        If vResult Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslado {0} enviado correctamente", PT.No_pedido))

                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)

                                Enviar_Traslados_Desde_Solicitud = True

                            Catch ex As Exception

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message))

                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al registrar el pedido:{0} en el ERP. Error: {1}", PT.No_pedido, ex.Message),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                            End Try

                        Else

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslado {0} no se envió correctamente", PT.No_pedido))
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Traslado {0} no se envió correctamente al ERP.", PT.No_pedido),
                                                                           PT.No_pedido,
                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                           BeConfigDet.Idnavconfigdet, CnnLog)

                        End If

                    Else
                        clsPublic.Actualizar_Progreso(lblprg, "Se están intentando enviar registros de un pedido que ya fue marcado como enviado a ERP, por favor valide la integridad de los datos manualmente.")
                    End If

                Next

            Else

                clsPublic.Actualizar_Progreso(lblprg, "MSG_240117: No hay transacciones para enviar.")

            End If

            watch.Stop()

            clsPublic.Actualizar_Progreso(lblprg, "Tiempo transcurrido: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Function Enviar_Traslado_Desde_Solicitud_SAP(ByVal _DocEntry As Integer,
                                                        ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                        ByRef lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar) As Boolean

        Enviar_Traslado_Desde_Solicitud_SAP = False

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim IdPedidoEnc As Integer = 0
        Dim IdDespachoEnc As Integer = 0
        Dim BeDespacho As New clsBeTrans_despacho_enc
        Dim oTransfer As StockTransfer
        Dim oTransferRequest As StockTransfer
        Dim BaseLine As Integer = 0
        Dim vTrasladoDocEntry1 As Double = 0
        Dim vTrasladoDocEntry2 As Double = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                If oTransferRequest.GetByKey(_DocEntry) Then

                    oTransfer.CardCode = oTransferRequest.CardCode
                    oTransfer.DocDate = Date.Today
                    oTransfer.FromWarehouse = oTransferRequest.FromWarehouse
                    oTransfer.ToWarehouse = oTransferRequest.ToWarehouse

                    NoLineaTransfer = 0
                    NoLineaTransferLote = 0

                    clsTransaccion.Open_Connection() ': oCompany.StartTransaction()

                    Dim vBodega_Destino As String = ""
                    Dim BePedidoEnc As New clsBeTrans_pe_enc

                    IdPedidoEnc = lINavTransaccionesOut.FirstOrDefault().Idpedidoenc
                    IdDespachoEnc = lINavTransaccionesOut.FirstOrDefault().Iddespachoenc

                    Dim lProductoAValidarEnSAP As List(Of Object) = Get_Distinct_Product_Codes(lINavTransaccionesOut).ToList

                    BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    BeDespacho = clsLnTrans_despacho_enc.GetSingle(IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    If BePedidoEnc.Bodega_Destino <> "" Then
                        vBodega_Destino = BePedidoEnc.Bodega_Destino.Substring(0, 2)
                    Else
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No está definida la bodega destino para el traslado: {0} ", BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino))
                        Return False
                    End If

                    '#EJC202412061544: EndPoint validación Mario Tabora.
                    Dim result As String = Validata_Productos_EndPointCumbre(lProductoAValidarEnSAP, vBodega_Destino)

                    If result.Contains("Error") Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("No se puede enviar el traslado: {0} ", result))
                        Return False
                    End If

                    For j As Integer = 0 To oTransferRequest.Lines.Count - 1

                        oTransferRequest.Lines.SetCurrentLine(j)

                        Dim vCodigoProductoSAP As String = oTransferRequest.Lines.ItemCode.ToString()
                        Dim vNoLineaTRSAP As Integer = oTransferRequest.Lines.LineNum

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", vCodigoProductoSAP))

                        Dim DistinctProductosLineas = lINavTransaccionesOut.Where(Function(x) x.Codigo_producto = vCodigoProductoSAP _
                                                                          AndAlso x.No_linea = vNoLineaTRSAP _
                                                                          AndAlso x.Idpedidoenc = IdPedidoEnc).
                                                                    GroupBy(Function(x) New With {Key x.Codigo_producto, x.No_linea}).
                                                                    Select(Function(g) New With {
                                                                        g.Key.Codigo_producto,
                                                                        g.Key.No_linea,
                                                                        .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                    }).ToList()

                        If DistinctProductosLineas.Any() Then

                            For Each ProductoSalida In DistinctProductosLineas

                                If Not oTransferRequest.Lines.LineStatus = BoStatus.bost_Close Then

                                    If ProductoSalida.Cantidad_Total <= oTransferRequest.Lines.Quantity Then

                                        Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_producto OrElse
                                            vNoLineaAnterior <> ProductoSalida.No_linea)

                                        If nuevaLineaTransfer Then

                                            oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
                                            oTransfer.Lines.BaseType = InvBaseDocTypeEnum.InventoryTransferRequest
                                            oTransfer.Lines.BaseEntry = _DocEntry
                                            oTransfer.Lines.BaseLine = vNoLineaTRSAP
                                            oTransfer.Lines.ItemCode = oTransferRequest.Lines.ItemCode
                                            oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                                            oTransfer.Lines.FromWarehouseCode = oTransferRequest.FromWarehouse
                                            oTransfer.Lines.WarehouseCode = oTransferRequest.ToWarehouse

                                            vCodigoAnterior = oTransfer.Lines.ItemCode
                                            vNoLineaAnterior = oTransfer.Lines.LineNum

                                            oTransfer.Lines.Add() : NoLineaTransfer += 1

                                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.No_pedido = _DocEntry _
                                                                                                  AndAlso x.No_linea = vNoLineaTRSAP _
                                                                                                  AndAlso x.Codigo_producto = vCodigoProductoSAP _
                                                                                                  AndAlso x.Enviado = False)

                                            If Not Sublista_A_Actualizar Is Nothing Then
                                                If Sublista_A_Actualizar.Count > 0 Then
                                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                                End If
                                            End If

                                        End If
                                    Else
                                        Throw New Exception("WMS está intentando generar un traslado por: " & ProductoSalida.Cantidad_Total &
                                                        " en una línea de SAP para el material: " & oTransferRequest.Lines.ItemCode &
                                                        " que refleja una cantidad de: " & oTransferRequest.Lines.Quantity & " probablemente esto no sea posible.")
                                    End If
                                Else
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El Producto: {0} ya fue completado. ", oTransferRequest.Lines.ItemCode.ToString()))
                                End If
                            Next 'DistinctProductosLineas

                            vAgregarEntrega = True

                        End If

                    Next

                    If NoLineaTransfer > 0 AndAlso vAgregarEntrega Then

                        Dim BePiloto As New clsBeEmpresa_transporte_pilotos
                        Dim BeVehiculo As New clsBeEmpresa_transporte_vehiculos

                        BePiloto = clsLnEmpresa_transporte_pilotos.Get_By_IdPiloto(BeDespacho.IdPiloto)
                        BeVehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_IdVehiculo(BeDespacho.IdVehiculo)

                        If Not BeVehiculo Is Nothing Then
                            oTransfer.UserFields.Fields.Item("U_vehiculo").Value = BeVehiculo.Placa
                        End If

                        If Not BeDespacho.NombrePiloto Is Nothing Then
                            oTransfer.UserFields.Fields.Item("U_nomcondu").Value = BeDespacho.NombrePiloto
                        End If

                        oTransfer.UserFields.Fields.Item("U_motras").Value = "Traslado"
                        oTransfer.UserFields.Fields.Item("U_rtncondu").Value = BeDespacho.Numero.ToString()

                        If BePedidoEnc.Cliente.Codigo = "05" Then

                            oTransfer.UserFields.Fields.Item("U_FIS").Value = "S"
                            '#CKFK20250116 Se cambió el IdPedidoEnc por el despacho
                            oTransfer.Comments = "Traslado fiscal generado por TOMWMS Despacho WMS: " & BeDespacho.IdDespachoEnc
                            oTransfer.Series = 115

                            vBodega_Destino = BePedidoEnc.Bodega_Destino.Substring(0, 2)

                        Else
                            oTransfer.UserFields.Fields.Item("U_FIS").Value = "N"
                            '#CKFK20250116 Se cambió el IdPedidoEnc por el despacho
                            oTransfer.Comments = "Traslado generado por TOMWMS Despacho WMS: " & BeDespacho.IdDespachoEnc
                        End If

                        oTransfer.JournalMemo = IdPedidoEnc

                        Dim oResultado As Integer
                        oResultado = oTransfer.Add()

                        If oResultado <> 0 Then
                            Dim errMsg = oCompany.GetLastErrorDescription()
                            Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                        Else

                            oTransfer.Close()

                            ' Obtener el DocEntry del traslado generado
                            Dim newObjectCode As String = ""
                            oCompany.GetNewObjectCode(newObjectCode)

                            If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry1) Then
                                Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                            End If

                            '#CKFK20250125 Obtener el docnum del documento 
                            Dim vTrasladoDocNum As Integer = 0 ' Aquí almacenaremos el DocNum     
                            Dim vOtroDocNum As String = ""

                            Try

                                vOtroDocNum = Obtener_Doc_Num_Traslado(oCompany, vTrasladoDocEntry1)

                                If Not lblprg.IsDisposed Then
                                    clsPublic.Actualizar_Progreso(lblprg, "Se creó la transferencia con DocNum: " & vOtroDocNum & " en SAP.")
                                    clsLnLog_error_wms.Agregar_Error("#IF_SAP_ENV_TRAS_DESDE_SOL: Se creó la transferencia para el documento: " & _DocEntry & " en SAP.")
                                End If

                            Catch ex As Exception
                                If Not lblprg.IsDisposed Then
                                    clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                                End If
                            End Try

                            BePedidoEnc.No_Documento_Externo = vTrasladoDocEntry1
                            clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(BePedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            BeDespacho.No_Documento_Externo = vTrasladoDocEntry1
                            clsLnTrans_despacho_enc.Actualizar_No_Documento_Externo(BeDespacho, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                            '05 A colonial.
                            If vBodega_Destino <> "" Then
                                vTrasladoDocEntry2 = Enviar_Solicitud_Traslado_SAP(lINavTransaccionesOut,
                                                                                  BePedidoEnc.Cliente.Codigo,
                                                                                  vBodega_Destino,
                                                                                  vTrasladoDocEntry1,
                                                                                  IdPedidoEnc,
                                                                                  oCompany,
                                                                                  lblprg,
                                                                                  prg)
                            End If

                            Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                            If IResult = 0 Then
                                Throw New Exception("Se envió el traslado entre almacenes a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                            End If

                        End If

                    End If

                    clsTransaccion.Commit_Transaction()

                End If

            End If

            Return True

        Catch errMsg As Exception

            clsTransaccion.RollBack_Transaction()

            clsPublic.Actualizar_Progreso(lblprg, errMsg.Message)

            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Enviar_Solicitud_Traslado_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                  ByVal FromWhs As String,
                                                  ByVal ToWhs As String,
                                                  ByVal vTrasladoDocEntry As String,
                                                  ByVal IdPedidoEnc As Integer,
                                                  oCompany As Company,
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef prg As Windows.Forms.ProgressBar) As Integer

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vNoLineaAnterior As Integer = -1
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim BodegasByPedido As New Dictionary(Of Integer, clsBeInfoBodegaByIdPedidoEnc)

        Enviar_Solicitud_Traslado_SAP = 0

        Try

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                Dim oTransfer As StockTransfer
                Dim BaseLine As Integer = 0
                Dim IdDespachoEnc As Integer = 0

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                NoLineaTransfer = 0
                NoLineaTransferLote = 0

                clsTransaccion.Open_Connection()

                Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Traslado_Por_Estados_SAP AndAlso x.Enviado = False).
                                                                GroupBy(Function(x) New With {Key x.Iddespachoenc, Key x.Idpedidoenc, Key x.Codigo_producto, Key x.No_linea}).
                                                                Select(Function(g) New With {
                                                                    .IdDespachoEnc = g.Key.Iddespachoenc,
                                                                    .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                    .Codigo_Producto = g.Key.Codigo_producto,
                                                                    .No_Linea = g.Key.No_linea,
                                                                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                }).ToList()

                If DistinctIdPedidoEncByTraslado.Any() Then

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

                    For Each ProductoSalida In DistinctIdPedidoEncByTraslado

                        IdDespachoEnc = ProductoSalida.IdDespachoEnc
                        oTransfer.FromWarehouse = FromWhs
                        oTransfer.ToWarehouse = ToWhs.Substring(0, 2)
                        oTransfer.UserFields.Fields.Item("U_FIS").Value = "N"
                        oTransfer.JournalMemo = IdPedidoEnc
                        oTransfer.Series = 113

                        Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto OrElse vNoLineaAnterior <> ProductoSalida.No_Linea)

                        If nuevaLineaTransfer Then

                            oTransfer.Lines.SetCurrentLine(NoLineaTransfer)
                            oTransfer.Lines.ItemCode = ProductoSalida.Codigo_Producto
                            oTransfer.Lines.Quantity = ProductoSalida.Cantidad_Total
                            oTransfer.Lines.FromWarehouseCode = FromWhs
                            oTransfer.Lines.WarehouseCode = ToWhs
                            vCodigoAnterior = oTransfer.Lines.ItemCode
                            vNoLineaAnterior = ProductoSalida.No_Linea

                            oTransfer.Lines.Add() : NoLineaTransfer += 1

                        End If

                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                End If

                '#EJC20240629: DocEntry en solicitud intermedia.
                oTransfer.Comments = vTrasladoDocEntry

                Dim oResultado As Integer
                oResultado = oTransfer.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else

                    ' Obtener el DocEntry del traslado generado
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    Enviar_Solicitud_Traslado_SAP = vTrasladoDocEntry

                    Dim vMensaje As String = ""

                    Try

                        Dim vDocNum As Integer = Obtener_Doc_Num_StockTransfer(oCompany, vTrasladoDocEntry)
                        vMensaje = "Se creó la solicitud de traslado con DocNum: " & vDocNum & " en SAP."

                        Dim BeDespacho As New clsBeTrans_despacho_enc
                        BeDespacho.IdDespachoEnc = IdDespachoEnc
                        BeDespacho.No_pase = vDocNum

                        clsLnTrans_despacho_enc.Actualizar_No_Pase(BeDespacho, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                        clsLnLog_error_wms.Agregar_Error("#IF_SAP_ENV_SOL_TRAS: Se creó la solicitud de traslado con DocNum: " & vDocNum & " en SAP para el IdDespachoEnc: " & IdDespachoEnc & " Documento: " & vTrasladoDocEntry)

                    Catch ex As Exception
                        clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                    End Try

                End If

                clsTransaccion.Commit_Transaction()

            End If

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("#EJC_20241125: Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Importar_Solicitud_Traslado_Entrada_SAP(ByRef lblprg As RichTextBox,
                                                            ByRef prg As Windows.Forms.ProgressBar,
                                                            Optional ByVal ForzarEjecucion As Boolean = False,
                                                            Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                            Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Solicitud_Traslado_Entrada_SAP = False

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

            lblprg.Text = ""

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Solicitudes_Traslado_Entrada_SAP(BeConfigEnc, lblprg, True, prg, CnnLog, pPedidoCliente) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Solicitudes_Traslado_Entrada_SAP(BeConfigEnc,
                                                                     lblprg,
                                                                     True,
                                                                     prg,
                                                                     CnnLog,
                                                                     pPedidoCliente) Then
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

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw ex

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Procesar_Solicitudes_Traslado_Entrada_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                                              ByVal lblprg As RichTextBox,
                                                              ByVal FlujoEntrada As Boolean,
                                                              ByRef prg As Windows.Forms.ProgressBar,
                                                              ByRef cnnLog As SqlConnection,
                                                              Optional pPedidoCliente As String = "") As Boolean
        Procesar_Solicitudes_Traslado_Entrada_SAP = False

        Dim Resultado As String = ""
        Dim BeBodega As New clsBeBodega
        Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_compra_enc)

        Try

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega)

            If Not BeBodega Is Nothing Then

                lPedidosTrasladoSAP = Get_Solicitudes_Traslado_Entrada_SAP(BeBodega.Codigo, FlujoEntrada, pPedidoCliente)

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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Traslado (OWTQ) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Vendor_Invoice_No, vbNewLine))

                            If Not clsLnProveedor.Existe_Proveedor(PedidoClienteSAP.Buy_From_Vendor_No) Then

                                BeConfigEnc = BeConfigEnc

                                If Inserta_Proveedor_Desde_SAP(PedidoClienteSAP.Buy_From_Vendor_No,
                                                               PedidoClienteSAP.Buy_From_Vendor_Name,
                                                               cnnLog) Then
                                    clsPublic.Actualizar_Progreso(lblprg, vbTab & "El proveedor: " & PedidoClienteSAP.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                                    clsLnLog_error_wms.Agregar_Error("#IF_SAP_SOL_TRAS_ENT: El proveedor: " & PedidoClienteSAP.Buy_From_Vendor_No & " No existía en WMS y fue insertado.")
                                End If

                            End If


                            Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
                            Dim vResult As String = ""
                            If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(PedidoClienteSAP,
                                                                                    BePedidoCompraEnc,
                                                                                    vResult) Then

                                clsPublic.Actualizar_Progreso(lblprg, "Se creó el documento de ingreso: " & vResult)
                                clsLnLog_error_wms.Agregar_Error("#IF_SAP_SOL_TRAS_ENT: Se importó el documento de ingreso: " & vResult)
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "No se pudo crear el documento de ingreso: " & vResult)
                            End If

                            If Not BePedidoCompraEnc Is Nothing Then
                                Marcar_Solicitud_Trasladado_Sincronizado_SAP(PedidoClienteSAP.No,
                                                                             Estado_Enviado_SAP.Enviado,
                                                                             lblprg)
                            End If

                            clsPublic.Actualizar_Progreso(lblprg, Resultado)

                        Next

                    End If

                End If

                Procesar_Solicitudes_Traslado_Entrada_SAP = True

            Else
                clsPublic.Actualizar_Progreso(lblprg, "ERROR_202405210317: No se obtuvo la bodega para la configuración de la interface.")
            End If

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Private Function Get_Solicitudes_Traslado_Entrada_SAP(ByVal pCodigoBodegaInterface As String,
                                                          ByVal FlujoEntrada As Boolean,
                                                          Optional ByVal pPedidoCliente As String = "") As List(Of clsBeI_nav_ped_compra_enc)

        Get_Solicitudes_Traslado_Entrada_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_compra_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_compra_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                If BeConfigEnc Is Nothing Then
                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
                End If

                BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

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

                Dim SAP_Traslados As String = "SELECT DISTINCT T0.DocEntry,
                                                               T0.DocNum,
                                                               T0.DocDueDate DocDate, 
                                                               T0.CardName, 
                                                               T1.FromWhsCod AS 'CODIGO_BODEGA_ORIGEN',
                                                               OW1.WhsName AS 'NOMBRE_BODEGA_ORIGEN',
                                                               T1.WhsCode AS 'CODIGO_BODEGA_DESTINO',
                                                               OW2.WhsName AS 'NOMBRE_BODEGA_DESTINO',
                                                               T0.Comments AS JRNLMEMO,
                                                               T0.Canceled,
                                                               T0.DocStatus,
                                                               'TRANSFERENCIA' AS Tipo_Transferencia,
                                                               T0.U_ALMDEST
                                               FROM OWTQ T0 INNER JOIN 
                                                    WTQ1 T1 ON T0.docentry = T1.docentry INNER JOIN 
	                                                OWHS OW1 ON T1.FromWhsCod = OW1.WhsCode INNER JOIN 
	                                                OWHS OW2 ON T1.WhsCode = OW2.WhsCode
                                               WHERE T0.DOCSTATUS = 'O' AND 
                                                     T0.U_Enviado_WMS = 2 "

                If pPedidoCliente <> "" Then
                    SAP_Traslados += " AND T0.docnum = " & pPedidoCliente
                End If
                If Not FlujoEntrada Then

                    If Not String.IsNullOrEmpty(vCriteria) Then
                        SAP_Traslados += " AND (T1.FromWhsCod IN (" & vCriteria & ")
                               OR T1.WhsCode IN (" & vCriteria & ")) 
                          ORDER BY T0.DocEntry DESC"
                    Else
                        ' Asumiendo que pCodigoBodegaInterface es una variable ya definida en otro lugar del código
                        SAP_Traslados += " AND (T1.FromWhsCod = " & pCodigoBodegaInterface &
                        " OR T1.WhsCode = " & pCodigoBodegaInterface & ") 
                          ORDER BY T0.DocEntry DESC "
                    End If

                Else

                    ' Asumiendo que pCodigoBodegaInterface es una variable ya definida en otro lugar del código
                    SAP_Traslados += " AND (T1.WhsCode = " & pCodigoBodegaInterface & ") 
                              ORDER BY T0.DocEntry DESC "
                End If

                RsEnc.DoQuery(SAP_Traslados)

                Dim BeINAVPedidoCompra As clsBeI_nav_ped_compra_enc = New clsBeI_nav_ped_compra_enc()

                While RsEnc.EoF = False

                    BeINAVPedidoCompra = New clsBeI_nav_ped_compra_enc()
                    BeINAVPedidoCompra.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BeINAVPedidoCompra.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeINAVPedidoCompra.Order_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeINAVPedidoCompra.Document_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BeINAVPedidoCompra.Expected_Receipt_Date = Convert.ToDateTime(RsEnc.Fields.Item("DOCDATE").Value)
                    BeINAVPedidoCompra.Status = 1
                    BeINAVPedidoCompra.Buy_From_Vendor_No = RsEnc.Fields.Item("CODIGO_BODEGA_ORIGEN").Value.ToString()
                    BeINAVPedidoCompra.Buy_From_Vendor_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_ORIGEN").Value.ToString()
                    BeINAVPedidoCompra.Is_Internal_Transfer = False
                    BeINAVPedidoCompra.Location_Code = Convert.ToString(RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value)
                    BeINAVPedidoCompra.Vendor_Invoice_No = Convert.ToString(RsEnc.Fields.Item("DOCNUM").Value).ToString()
                    BeINAVPedidoCompra.Posting_Description = RsEnc.Fields.Item("JRNLMEMO").Value.ToString()
                    BeINAVPedidoCompra.Product_Owner_Code = BeConfigEnc.IdPropietario
                    BeINAVPedidoCompra.Ship_To_Contact = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value.ToString()
                    BeINAVPedidoCompra.Document_Type = tTipoDocumentoIngreso.Transferencia_de_Ingreso

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT 
                                 T0.LineNum, 
                                 T0.ITEMCODE, 
                                 T0.DSCRIPTION, 
                                 T0.QUANTITY, 
                                 T0.PRICE, 
                                 T0.LINETOTAL, 
                                 T0.VATSUM, 
                                 T0.DOCENTRY,  
                                 T0.WHSCODE, 
                                 T1.SalUnitMsr AS UNIDAD_MEDIDA   
                                 FROM WTQ1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode    
                                 WHERE T0.DOCENTRY = '" & BeINAVPedidoCompra.No & "'"

                    RsDet.DoQuery(query_det)

                    BeINAVPedidoCompra.Lineas_Detalle = New List(Of clsBeI_nav_ped_compra_det)

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = BeINAVPedidoCompra.No
                        BePedidoDetWMS.No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                        BePedidoDetWMS.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
                        BePedidoDetWMS.Description = RsDet.Fields.Item("dscription").Value.ToString()
                        BePedidoDetWMS.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                        BePedidoDetWMS.Variant_Code = Nothing
                        BeINAVPedidoCompra.Lineas_Detalle.Add(BePedidoDetWMS)
                        n += 1
                        RsDet.MoveNext()
                    End While

                    lPedidosCliente.Add(BeINAVPedidoCompra)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
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

                Throw ex

            End Try


        Catch ex As Exception
            Throw ex
        Finally
            clstrans.Close_Conection()
        End Try

    End Function

    Public Function Importar_Solicitud_Devolucion_Proveedor_SAP(ByRef lblprg As RichTextBox,
                                                                ByRef prg As Windows.Forms.ProgressBar,
                                                                ByVal FlujoEntrada As Boolean,
                                                                Optional ByVal ForzarEjecucion As Boolean = False,
                                                                Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                Optional ByVal pPedidoCliente As String = "") As Boolean
        Importar_Solicitud_Devolucion_Proveedor_SAP = False

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

            lblprg.Text = ""

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Procesar_Solicitudes_Devolucion_Proveedor_SAP(BeConfigEnc,
                                                                     lblprg,
                                                                     prg,
                                                                     CnnLog,
                                                                     pPedidoCliente) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde SAP?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Procesar_Solicitudes_Devolucion_Proveedor_SAP(BeConfigEnc,
                                                                         lblprg,
                                                                         prg,
                                                                         CnnLog,
                                                                         pPedidoCliente) Then
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

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de cliente a tabla de TOMWMS: {1} {0} {1}", ex.Message, vbNewLine))

            Throw ex

        Finally
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Procesar_Solicitudes_Devolucion_Proveedor_SAP(ByVal BeI_nav_config_enc As clsBeI_nav_config_enc,
                                                                  ByVal lblprg As RichTextBox,
                                                                  ByRef prg As Windows.Forms.ProgressBar,
                                                                  ByRef cnnLog As SqlConnection,
                                                                  Optional pPedidoCliente As String = "") As Boolean
        Procesar_Solicitudes_Devolucion_Proveedor_SAP = False

        Dim Resultado As String = ""
        Dim BeBodega As New clsBeBodega
        Dim lPedidosTrasladoSAP As New List(Of clsBeI_nav_ped_traslado_enc)

        Try

            BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeI_nav_config_enc.Idbodega)

            If Not BeBodega Is Nothing Then

                lPedidosTrasladoSAP = Get_Solicitudes_Devolucion_Proveedor_SAP(BeBodega.Codigo)

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

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Solicitud de Traslado (OWTQ) : {0}/{1}{2}", PedidoClienteSAP.No, PedidoClienteSAP.Receipt_Document_Reference, vbNewLine))

                            BeClienteWMS = clsLnCliente.Existe(PedidoClienteSAP.Transfer_to_Code)

                            If BeClienteWMS Is Nothing Then

                                If Inserta_Cliente_SAP(PedidoClienteSAP.Transfer_to_Code, lblprg) Then
                                    clsPublic.Actualizar_Progreso(lblprg, "El cliente: " & PedidoClienteSAP.Transfer_to_Code & " No existía en WMS y fue insertado.")
                                End If

                            End If

                            Dim BePedidoEncResult As New clsBeTrans_pe_enc
                            BePedidoEncResult = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia(PedidoClienteSAP,
                                                                                                                       lblprg, Nothing, Nothing)

                            If Not BePedidoEncResult Is Nothing Then

                                BePedidoEncResult.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEncResult.IdPedidoEnc)

                                Marcar_Solicitud_Trasladado_Sincronizado_SAP(PedidoClienteSAP.No, Estado_Enviado_SAP.Enviado, lblprg)

                                clsLnLog_error_wms.Agregar_Error("#IF_SAP_SOL_DEVOL_PROV: Se importó el documento: " & PedidoClienteSAP.No)

                            End If

                            clsPublic.Actualizar_Progreso(lblprg, Resultado)

                        Next

                    End If

                End If

                Procesar_Solicitudes_Devolucion_Proveedor_SAP = True

            Else
                clsPublic.Actualizar_Progreso(lblprg, "ERROR_202405210317: No se obtuvo la bodega para la configuración de la interface.")
            End If

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            Throw ex

        End Try

    End Function

    Private Function Get_Solicitudes_Devolucion_Proveedor_SAP(ByVal pCodigoBodegaInterface As String,
                                                              Optional ByVal pPedidoCliente As String = "") As List(Of clsBeI_nav_ped_traslado_enc)

        Get_Solicitudes_Devolucion_Proveedor_SAP = Nothing

        Dim lPedidosCliente As New List(Of clsBeI_nav_ped_traslado_enc)
        Dim BePedidoCliente As New clsBeI_nav_ped_traslado_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_traslado_det
        Dim NoLinea As Integer = 1
        Dim BePropietario As New clsBePropietarios

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            If lRetCode <> 0 Then
                oCompany.GetLastError(lErrCode, sErrMsg)
                Throw New Exception(sErrMsg)
            Else

                If BeConfigEnc Is Nothing Then
                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface)
                End If

                BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

                Dim oRecSet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                Dim RsEnc As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)

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

                Dim SAP_Traslados As String = "SELECT DISTINCT T0.DocEntry,
                                                               T0.DocNum,
                                                               T0.DocDate, 
                                                               T0.CardName, 
                                                               T1.FromWhsCod AS 'CODIGO_BODEGA_ORIGEN',
                                                               OW1.WhsName AS 'NOMBRE_BODEGA_ORIGEN',
                                                               T1.WhsCode AS 'CODIGO_BODEGA_DESTINO',
                                                               OW2.WhsName AS 'NOMBRE_BODEGA_DESTINO',
                                                               T0.Comments AS JRNLMEMO,
                                                               T0.Canceled,
                                                               T0.DocStatus,
                                                               'TRANSFERENCIA' AS Tipo_Transferencia,
                                                               T0.U_ALMDEST
                                               FROM OPRR T0 INNER JOIN 
                                                    PRR1 T1 ON T0.docentry = T1.docentry INNER JOIN 
	                                                OWHS OW1 ON T1.FromWhsCod = OW1.WhsCode INNER JOIN 
	                                                OWHS OW2 ON T1.WhsCode = OW2.WhsCode
                                               WHERE T0.DOCSTATUS = 'O' AND 
                                                     T0.U_Enviado_WMS = 2 "

                If Not String.IsNullOrEmpty(vCriteria) Then
                    SAP_Traslados += " AND (T1.FromWhsCod IN (" & vCriteria & ")
                               OR T1.WhsCode IN (" & vCriteria & ")) 
                          ORDER BY T0.DocEntry DESC"
                Else
                    ' Asumiendo que pCodigoBodegaInterface es una variable ya definida en otro lugar del código
                    SAP_Traslados += " AND (T1.FromWhsCod = " & pCodigoBodegaInterface &
                            " OR T1.WhsCode = " & pCodigoBodegaInterface & ") 
                          ORDER BY T0.DocEntry DESC "
                End If

                RsEnc.DoQuery(SAP_Traslados)

                Dim BePedidoWMS As clsBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc()

                While RsEnc.EoF = False

                    BePedidoWMS = New clsBeI_nav_ped_traslado_enc()
                    BePedidoWMS.No = RsEnc.Fields.Item("DOCENTRY").Value
                    BePedidoWMS.Posting_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Receipt_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Shipment_Date = RsEnc.Fields.Item("DOCDATE").Value
                    BePedidoWMS.Status = 1
                    BePedidoWMS.Transfer_from_Code = RsEnc.Fields.Item("CODIGO_BODEGA_ORIGEN").Value
                    BePedidoWMS.Transfer_from_Contact = "MI3_NAME"
                    BePedidoWMS.Transfer_from_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_ORIGEN").Value
                    BePedidoWMS.Transfer_to_Code = RsEnc.Fields.Item("CODIGO_BODEGA_DESTINO").Value
                    BePedidoWMS.Transfer_to_Contact = RsEnc.Fields.Item("CARDNAME").Value
                    BePedidoWMS.Transfer_to_Name = RsEnc.Fields.Item("NOMBRE_BODEGA_DESTINO").Value
                    BePedidoWMS.Transfer_to_CodeField = RsEnc.Fields.Item("U_ALMDEST").Value
                    BePedidoWMS.Product_Owner_Code = BePropietario.Codigo
                    BePedidoWMS.Receipt_Document_Reference = RsEnc.Fields.Item("DOCNUM").Value
                    BePedidoWMS.Manufacturing_Process = 0
                    BePedidoWMS.Document_Type = tTipoDocumentoSalida.Traslado_Por_Estados_SAP

                    Dim n As Integer = 1
                    Dim RsDet As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                    Dim query_det As String

                    query_det = "SELECT 
                                 T0.LineNum, 
                                 T0.ITEMCODE, 
                                 T0.DSCRIPTION, 
                                 T0.QUANTITY, 
                                 T0.PRICE, 
                                 T0.LINETOTAL, 
                                 T0.VATSUM, 
                                 T0.DOCENTRY,  
                                 T0.WHSCODE, 
                                 T1.SalUnitMsr AS UNIDAD_MEDIDA   
                                 FROM PRR1 T0 INNER JOIN OITM T1 ON T1.ItemCode= T0.ItemCode    
                                 WHERE T0.DOCENTRY = '" & BePedidoWMS.No & "'"

                    RsDet.DoQuery(query_det)

                    BePedidoWMS.Lineas_Detalle = New List(Of clsBeI_nav_ped_traslado_det)

                    While RsDet.EoF = False

                        BePedidoDetWMS = New clsBeI_nav_ped_traslado_det()
                        BePedidoDetWMS.NoEnc = BePedidoWMS.No
                        BePedidoDetWMS.No = clsLnTrans_pe_det.MaxID() + 1
                        BePedidoDetWMS.Item_No = RsDet.Fields.Item("ITEMCODE").Value.ToString()
                        BePedidoDetWMS.Line_No = RsDet.Fields.Item("LINENUM").Value.ToString()
                        BePedidoDetWMS.Shipment_Date = Date.Now
                        BePedidoDetWMS.Quantity = Convert.ToDecimal(RsDet.Fields.Item("QUANTITY").Value)
                        BePedidoDetWMS.Description = RsDet.Fields.Item("dscription").Value.ToString()
                        BePedidoDetWMS.Unit_of_Measure_Code = RsDet.Fields.Item("UNIDAD_MEDIDA").Value.ToString()
                        BePedidoDetWMS.Status = 1
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Transfer_to_CodeField = RsDet.Fields.Item("WHSCODE").Value.ToString()
                        BePedidoDetWMS.Price = Convert.ToDouble(RsDet.Fields.Item("PRICE").Value)
                        BePedidoWMS.Lineas_Detalle.Add(BePedidoDetWMS)

                        n += 1

                        RsDet.MoveNext()

                    End While

                    lPedidosCliente.Add(BePedidoWMS)

                    RsEnc.MoveNext()

                End While

            End If

            Return lPedidosCliente

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Public Function Enviar_Solicitud_Devolucion_Proveedor(ByRef lblprg As RichTextBox,
                                                        ByRef prg As Windows.Forms.ProgressBar,
                                                        ByVal pTipo As tTipoDocumentoSalida) As Boolean

        Dim lTransaccionesSalida As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaSingle As New List(Of clsBeI_nav_transacciones_out)
        Dim lTransaccionesSalidaReproceso As New List(Of clsBeI_nav_transacciones_out)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim Sl As New clsSyncLotes()
        Dim vContadorReproceso As Integer = 0
        Dim lTransPtPendienteRegistroEnNav As New List(Of clsBeTrans_pe_enc)

        Enviar_Solicitud_Devolucion_Proveedor = False

        Try

            CnnLog.Open()

            lTransaccionesSalida = clsLnI_nav_transacciones_out.Get_Lotes_Salida_Pendientes_Envio(pTipo)

            If Not lTransaccionesSalida Is Nothing AndAlso lTransaccionesSalida.Count > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones a enviar: {0}", lTransaccionesSalida.Count))

                Dim ListaPedidosTransf = (From i In lTransaccionesSalida
                                          Group i By Keys = New With {Key i.No_pedido, Key i.Idpedidoenc} Into Group
                                          Select New With {Key Keys.No_pedido, Key Keys.Idpedidoenc})
                Dim Enviado_A_Erp As Boolean = False '#CKFK 20180518 10:20 PM Agregué la validación de si el registro ya fue enviado o no a NAV

                For Each PT In ListaPedidosTransf

                    Enviado_A_Erp = clsLnTrans_pe_enc.Get_Estado_Enviado_A_ERP_By_IdPedidoEnc(PT.Idpedidoenc)

                    If Not Enviado_A_Erp Then

                        lTransaccionesSalida = lTransaccionesSalida.FindAll(Function(x) x.Idpedidoenc = PT.Idpedidoenc).ToList()

                        If Procesar_Solicitudes_Devolucion_Proveedor_SAP(lTransaccionesSalida, lblprg, prg) Then

                            Try

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Transacciones de salida enviadas correctamente: {0}", lTransaccionesSalida.Count))
                                clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(PT.Idpedidoenc, True, BeConfigEnc.IdUsuario)
                                Enviar_Solicitud_Devolucion_Proveedor = True

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

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            prg.Visible = False
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Function Procesar_Solicitudes_Devolucion_Proveedor_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                                  ByRef lblprg As RichTextBox,
                                                                  ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim vNoLineaAnterior As Integer = -1
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim IdPedidoEnc As Integer = 0
        Dim IdDespachoEnc As Integer = 0
        Dim BeDespacho As New clsBeTrans_despacho_enc
        Dim oTransfer As StockTransfer
        Dim oTransferRequest As StockTransfer
        Dim BaseLine As Integer = 0

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                oTransfer = CType(oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer), StockTransfer)
                oTransferRequest = CType(oCompany.GetBusinessObject(BoObjectTypes.oInventoryTransferRequest), StockTransfer)

                Dim vCodigoProductoSAP As String = oTransferRequest.Lines.ItemCode.ToString()

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Producto: {0} ", vCodigoProductoSAP))

                IdPedidoEnc = lINavTransaccionesOut.FirstOrDefault().Idpedidoenc
                IdDespachoEnc = lINavTransaccionesOut.FirstOrDefault().Iddespachoenc

                Dim BePedidoEnc As New clsBeTrans_pe_enc
                Dim vBodega_Destino As String = ""

                BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                BeDespacho = clsLnTrans_despacho_enc.GetSingle(IdDespachoEnc, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                vBodega_Destino = BePedidoEnc.Bodega_Destino

                Enviar_Solicitud_Devolucion_Proveedor_SAP(lINavTransaccionesOut,
                                                          BePedidoEnc,
                                                          lblprg,
                                                          prg)

                clsTransaccion.Commit_Transaction()

            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction() : oCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            Desconectar_SAP(oCompany)
            clsTransaccion.Close_Conection()
        End Try

    End Function

    Public Function Enviar_Solicitud_Devolucion_Proveedor_SAP(ByVal lINavTransaccionesOut As List(Of clsBeI_nav_transacciones_out),
                                                              ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                              ByRef lblprg As RichTextBox,
                                                              ByRef prg As Windows.Forms.ProgressBar) As Boolean

        prg.Maximum = lINavTransaccionesOut.Count
        prg.Visible = True

        Dim lINav_Transacciones_Out_Enviadas As New List(Of clsBeI_nav_transacciones_out)
        Dim Sublista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim Lista_A_Actualizar As New List(Of clsBeI_nav_transacciones_out)
        Dim vCodigoAnterior As String = ""
        Dim NoLineaTransfer As Integer = 0
        Dim NoLineaTransferLote As Integer = 0
        Dim vNoLineaAnterior As Integer = -1
        Dim vAgregarEntrega As Boolean = False
        Dim clsTransaccion As New clsTransaccion
        Dim BodegasByPedido As New Dictionary(Of Integer, clsBeInfoBodegaByIdPedidoEnc)

        Try

            Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg)

            Application.DoEvents()

            If lRetCode <> 0 Then

                If sErrMsg = " - The specified resource name cannot be found in the image file." Then
                    Throw New Exception("El servidor de SAP no respondió la solicitud de conexión: " & sErrMsg)
                Else
                    Throw New Exception("Error al conectar a SAP: " & sErrMsg)
                End If

            Else

                Dim oReturnRequestProvider As Documents
                Dim BaseLine As Integer = 0

                oReturnRequestProvider = CType(oCompany.GetBusinessObject(BoObjectTypes.oGoodsReturnRequest), Documents)

                NoLineaTransfer = 0
                NoLineaTransferLote = 0

                clsTransaccion.Open_Connection()


                Dim DistinctIdPedidoEncByTraslado = lINavTransaccionesOut.Where(Function(x) x.IdTipoDocumento = tTipoDocumentoSalida.Devolucion_Proveedor).
                                                                GroupBy(Function(x) New With {Key x.Idpedidoenc, Key x.Codigo_producto}).
                                                                Select(Function(g) New With {
                                                                    .IdPedidoEnc = g.Key.Idpedidoenc,
                                                                    .Codigo_Producto = g.Key.Codigo_producto,
                                                                    .Cantidad_Total = g.Sum(Function(x) x.Cantidad)
                                                                }).ToList()

                If DistinctIdPedidoEncByTraslado.Any() Then

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Traslados encontrados: {0} ", DistinctIdPedidoEncByTraslado.Count))

                    For Each ProductoSalida In DistinctIdPedidoEncByTraslado

                        oReturnRequestProvider.CardCode = BePedidoEnc.Cliente.Codigo
                        oReturnRequestProvider.Comments = BePedidoEnc.IdPedidoEnc
                        oReturnRequestProvider.UserFields.Fields.Item("U_FIS").Value = "N"
                        oReturnRequestProvider.JournalMemo = BePedidoEnc.IdPedidoEnc

                        Dim nuevaLineaTransfer As Boolean = (vCodigoAnterior <> ProductoSalida.Codigo_Producto)

                        If nuevaLineaTransfer Then

                            oReturnRequestProvider.Lines.SetCurrentLine(NoLineaTransfer)
                            oReturnRequestProvider.Lines.ItemCode = ProductoSalida.Codigo_Producto
                            oReturnRequestProvider.Lines.Quantity = ProductoSalida.Cantidad_Total
                            vCodigoAnterior = oReturnRequestProvider.Lines.ItemCode

                            oReturnRequestProvider.Lines.Add() : NoLineaTransfer += 1

                            Sublista_A_Actualizar = lINavTransaccionesOut.FindAll(Function(x) x.Codigo_producto = ProductoSalida.Codigo_Producto _
                                                                                  AndAlso x.Enviado = False)

                            If Not Sublista_A_Actualizar Is Nothing Then
                                If Sublista_A_Actualizar.Count > 0 Then
                                    Lista_A_Actualizar.AddRange(Sublista_A_Actualizar)
                                End If
                            End If

                        End If


                    Next 'DistinctProductosLineas

                    vAgregarEntrega = True

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "No hay líneas para enviar.")
                End If

                oReturnRequestProvider.UserFields.Fields.Item("U_tiedest").Value = BePedidoEnc.IdBodega.ToString()
                oReturnRequestProvider.UserFields.Fields.Item("U_Causas_dev").Value = BePedidoEnc.IdMotivoDevolucion.ToString()

                Dim oResultado As Integer
                oResultado = oReturnRequestProvider.Add()

                If oResultado <> 0 Then
                    Dim errMsg = oCompany.GetLastErrorDescription()
                    Throw New Exception($"#ERROR_SAP_{oResultado}: {errMsg}")
                Else

                    Dim IResult As Integer = clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(Lista_A_Actualizar)

                    If IResult = 0 Then
                        Throw New Exception("Se envió la solicitud de devolución a proveedor a SAP pero no se pudieron marcar los registros como enviados en WMS.")
                    End If

                    ' Obtener el DocEntry del traslado generado
                    Dim newObjectCode As String = ""
                    oCompany.GetNewObjectCode(newObjectCode)

                    Dim vTrasladoDocEntry As Integer = 0

                    If Not Integer.TryParse(newObjectCode, vTrasladoDocEntry) Then
                        Throw New Exception("No se pudo obtener el DocEntry del traslado generado.")
                    End If

                    Try

                        Dim vDocNumTraslado As Integer = Obtener_Doc_Num_Traslado(oCompany, vTrasladoDocEntry)
                        Dim vMensaje As String = "Se creó la solicitud de devolución a proveedor con número: " & vDocNumTraslado & " en SAP."

                        BePedidoEnc.No_Documento_Externo = vTrasladoDocEntry
                        clsLnTrans_pe_enc.Actualizar_No_Documento_Externo(BePedidoEnc)
                        clsPublic.Actualizar_Progreso(lblprg, vMensaje)
                        clsLnLog_error_wms.Agregar_Error(BeConfigEnc.Idempresa,
                                                         BeConfigEnc.Idbodega,
                                                         vMensaje,
                                                         BePedidoEnc.IdPedidoEnc,
                                                         0,
                                                         0,
                                                         BeConfigEnc.IdUsuario)

                        clsLnLog_error_wms.Agregar_Error("#IF_SAP_ENV_DEVOL_PROV: Se envió a SAP la solicitud de devolución a proveedor con número: " & vDocNumTraslado & " Documento: " & vTrasladoDocEntry)

                    Catch ex As Exception
                    End Try

                End If

                clsTransaccion.Commit_Transaction()

            End If

            Return True

        Catch errMsg As Exception
            clsTransaccion.RollBack_Transaction()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error al enviar traslado entre almacenes a SAP: {0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message),
                                                   "",
                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                   BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al enviar traslado entre almacenes a SAP: {1} {0} {1}", errMsg.Message, vbNewLine))

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), errMsg.Message))

        Finally
            'Desconectar_SAP(oCompany) No cerrar porque viene en transacción SAP Desde el traslado.
            clsTransaccion.Close_Conection()
        End Try
    End Function

    Private Function Obtener_Doc_Num_Traslado(oCompany As Company, docEntry As Integer) As Integer
        ' Inicializar el valor de retorno en 0
        Dim docNum As Integer = 0

        Try
            ' Crear un objeto Recordset
            Dim oRecordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            ' Definir la consulta SQL para obtener el DocNum basándose en el DocEntry
            Dim query As String = $"SELECT DocNum FROM OWTR WHERE DocEntry = {docEntry}"

            ' Ejecutar la consulta
            oRecordset.DoQuery(query)

            ' Verificar si se encontraron resultados 
            If oRecordset.RecordCount > 0 Then
                ' Moverse al primer registro y obtener el valor del campo DocNum
                oRecordset.MoveFirst()
                docNum = oRecordset.Fields.Item("DocNum").Value
            Else
                ' Lanzar una excepción si no se encuentra el DocNum
                Throw New Exception("No se pudo obtener el DocNum del traslado generado.")
            End If

        Catch ex As Exception
            ' Re-lanzar la excepción capturada
            Throw
        End Try

        ' Retornar el valor de DocNum
        Return docNum
    End Function

    Private Function Obtener_Doc_Num_StockTransfer(oCompany As Company, docEntry As Integer) As Double
        ' Inicializar el valor de retorno en 0
        Dim docNum As Integer = 0

        Try
            ' Crear un objeto Recordset
            Dim oRecordset As Recordset = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            ' Definir la consulta SQL para obtener el DocNum basándose en el DocEntry
            Dim query As String = $"SELECT DocNum FROM OWTQ WHERE DocEntry = {docEntry}"

            ' Ejecutar la consulta
            oRecordset.DoQuery(query)

            ' Verificar si se encontraron resultados 
            If oRecordset.RecordCount > 0 Then
                ' Moverse al primer registro y obtener el valor del campo DocNum
                oRecordset.MoveFirst()
                docNum = oRecordset.Fields.Item("DocNum").Value
            Else
                ' Lanzar una excepción si no se encuentra el DocNum
                Throw New Exception("No se pudo obtener el DocNum del traslado generado.")
            End If

        Catch ex As Exception
            ' Re-lanzar la excepción capturada
            Throw
        End Try

        ' Retornar el valor de DocNum
        Return docNum
    End Function

    Public Function Validata_Productos_EndPointCumbre(products As List(Of Object), whsCode As String) As String

        Dim apiUrl As String = "http://10.10.1.12:8088/api/Item/Validator"

        Try

            apiUrl = BD.Instancia.URL_VALIDA_PRODUCTOS_POST

            ' Crear el cliente HTTP
            Using client As New HttpClient()
                ' Configurar el encabezado
                client.DefaultRequestHeaders.Add("WhsCode", whsCode)

                ' Serializar el arreglo de productos a JSON
                Dim jsonContent As String = JsonConvert.SerializeObject(products)
                Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")

                ' Enviar la solicitud POST0
                Dim response As HttpResponseMessage = client.PostAsync(apiUrl, content).Result
                Dim jsonResponse As String = response.Content.ReadAsStringAsync().Result

                ' Deserializar el JSON si es necesario
                Dim jsonObject As Object = JsonConvert.DeserializeObject(jsonResponse)

                ' Verificar la respuesta
                If response.IsSuccessStatusCode Then

                    If String.IsNullOrWhiteSpace(jsonResponse) OrElse jsonResponse.Trim() = "[]" Then
                        Return jsonResponse
                    Else
                        Return $"Error: {response.StatusCode} - {response.ReasonPhrase} -  {jsonResponse}"
                    End If

                Else
                    ' Manejar errores
                    Return $"Error: {response.StatusCode} - {response.ReasonPhrase}"
                End If
            End Using

        Catch ex As Exception
            ' Manejar excepciones
            Return $"Exception: {ex.Message}"
        End Try
    End Function

    Public Function Get_Distinct_Product_Codes(lINavTransaccionesOut As IEnumerable(Of Object)) As List(Of Object)

        Get_Distinct_Product_Codes = Nothing

        Try

            Dim distinctProducts = lINavTransaccionesOut.GroupBy(Function(x) x.Codigo_producto).
                                                                Select(Function(g) g.Key).
                                                                Distinct().ToList()

            Dim products As New List(Of Object)

            For Each product In distinctProducts
                products.Add(New With {.itemcode = product})
            Next

            Get_Distinct_Product_Codes = products

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class