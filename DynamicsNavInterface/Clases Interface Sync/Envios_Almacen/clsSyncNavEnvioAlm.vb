Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSCreaPicking
Imports TOMWMS.WSEnvioAlm
Imports TOMWMS.WSListaCliente
Imports TOMWMS.WSPedidoTransferencia
Imports TOMWMS.WSPicking

Public Class clsSyncNavEnvioAlm : Inherits clsInterfaceBase

    Implements IDisposable

    Property pBodega As String = ""

    Property pIdUsuario As Integer = 0

    Private fichaEnviosAlm() As Envio_alm

    Dim VContadorBitacoraTomims As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private wsEnviosAlmService As New Envio_alm_Service() With
    {
    .UseDefaultCredentials = UsarCredencialesPorDefecto,
    .Credentials = CredencialesConexion
    }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsEnviosAlmService IsNot Nothing Then
            wsEnviosAlmService.Dispose()
            wsEnviosAlmService = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Public Function Get_Envios_Alm_FromWS(ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction,
                                          Optional ByVal AplicarFiltros As Boolean = True,
                                          Optional ByVal pEnvioAlmacen As String = "") As List(Of Envio_alm)

        Try

            Dim lPedidosTraslado As New List(Of Envio_alm)
            Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
            Dim StartDate As String = "14032022.."

            lFiltros = clsLnI_nav_ent_filtros.Get_All_By_IdNavEnt(clsLnI_nav_ent_filtros.pEntidadesSycn.Pedido_Traslado,
                                                                  lConnection,
                                                                  lTransaction)

            Dim vCriteria As String = ""
            Dim vContador As Integer = 0

            For Each FiltroCategoria In lFiltros

                If FiltroCategoria.Tipo_Filtro = "" OrElse FiltroCategoria.Tipo_Filtro = "BODEGA" Then

                    If vContador = 0 Then
                        vCriteria = FiltroCategoria.Valor
                    Else
                        vCriteria += "|" & FiltroCategoria.Valor
                    End If

                ElseIf FiltroCategoria.Tipo_Filtro = "FECHA_INICIO" Then
                    StartDate = FiltroCategoria.Valor
                End If

                vContador += 1

            Next

            If vCriteria <> "" AndAlso pBodega <> "" Then
                If pBodega <> vCriteria Then
                    Throw New Exception(String.Format("La Bodega del filtro: {0} no se corresponde con la Bodega de la interface: {1}", vCriteria, pBodega))
                End If
            End If

            wsEnviosAlmService.Url = My.Settings.NavSync_WSEnvioAlm_Envio_alm_Service

            If AplicarFiltros Then

                '#EJC20180426: Cambmio transfer_to_code.
                Dim vFiltros As Envio_alm_Filter()
                Dim vFiltrosBodegasDestino As New Envio_alm_Filter With {.Field = Envio_alm_Fields.Location_Code, .Criteria = vCriteria}
                Dim vFiltroEstado As New Envio_alm_Filter With {.Field = Envio_alm_Fields.Status, .Criteria = "1"}
                Dim vFiltroEstadoDcoumento As New Envio_alm_Filter With {.Field = Envio_alm_Fields.Document_Status, .Criteria = ""}
                Dim vFiltroFechaDesde As New Envio_alm_Filter() With {.Field = Envio_alm_Fields.Posting_Date, .Criteria = StartDate}

                Dim vFiltroNumero As New Envio_alm_Filter()
                If pEnvioAlmacen <> "" Then
                    vFiltroNumero = New Envio_alm_Filter() With {.Field = Envio_alm_Fields.No, .Criteria = pEnvioAlmacen}
                End If

                'Importar cantidad enviada y si cantidad enviada > 0 no recibir
                If pEnvioAlmacen.Trim() <> "" Then
                    vFiltros = New Envio_alm_Filter() {vFiltrosBodegasDestino,
                                                       vFiltroEstado,
                                                       vFiltroEstadoDcoumento,
                                                       vFiltroFechaDesde,
                                                       vFiltroNumero}
                Else
                    vFiltros = New Envio_alm_Filter() {vFiltrosBodegasDestino,
                                                       vFiltroEstado,
                                                       vFiltroEstadoDcoumento,
                                                       vFiltroFechaDesde}
                End If

                fichaEnviosAlm = wsEnviosAlmService.ReadMultiple(vFiltros, Nothing, 1000)

                lPedidosTraslado.AddRange(fichaEnviosAlm)

            Else

                fichaEnviosAlm = wsEnviosAlmService.ReadMultiple(Nothing, Nothing, 1000)
                lPedidosTraslado.AddRange(fichaEnviosAlm)

            End If

            Return lPedidosTraslado

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Importar_Envio_Almacen_Desde_WSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                         ByRef prg As System.Windows.Forms.ProgressBar,
                                                                         ByRef cnnLog As SqlConnection,
                                                                         Optional pEnvioAlmacen As String = "") As Boolean

        Importar_Envio_Almacen_Desde_WSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsPublic.Actualizar_Progreso(lblprg, "")
            clsPublic.Actualizar_Progreso(lblprg, "Importando a tabla intermedia.")
            clsPublic.Actualizar_Progreso(lblprg, "")

            Dim lPedidosTraslado As New List(Of Envio_alm)

            lPedidosTraslado = Get_Envios_Alm_FromWS(lConnection, lTransaction, True, pEnvioAlmacen)

            BeNavEjecucionRes.Registros_ws = lPedidosTraslado.Count

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim BeI_Nav_PedidoTraslado As clsBeI_nav_ped_traslado_enc
            Dim BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim vTransferToCodeDefault As String = ""
            Dim vTipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Envío(s) de almacen en WS: {0} ", fichaEnviosAlm.Count))
            clsPublic.Actualizar_Progreso(lblprg, "")

            prg.Maximum = lPedidosTraslado.Count

            Dim vContador As Integer = 0

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)

            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If
            End If

            Dim BePropietario As New clsBePropietarios
            BePropietario = clsLnPropietarios.GetSingle(BeConfigEnc.IdPropietario)

            If BePropietario Is Nothing Then
                Throw New Exception("No éstá definido el código de propietario para el IdPropietario: " & BeConfigEnc.IdPropietario & " verifique que existe un propietario con ese Id")
            End If

            For Each EnvAlm As Envio_alm In lPedidosTraslado

                If Not clsLnI_nav_ped_traslado_enc.Exist(EnvAlm.No) Then

                    BeI_Nav_PedidoTraslado = New clsBeI_nav_ped_traslado_enc

                    CopyObject(EnvAlm, BeI_Nav_PedidoTraslado)

                    If Not EnvAlm.Posting_DateSpecified Then
                        EnvAlm.Posting_Date = Now.Date
                    ElseIf EnvAlm.Posting_Date.Year <= 1000 Then
                        EnvAlm.Posting_Date = Now.Date
                    End If

                    If Not EnvAlm.Shipment_DateSpecified Then
                        EnvAlm.Shipment_Date = Now.Date
                    ElseIf EnvAlm.Shipment_Date.Year <= 1000 Then
                        EnvAlm.Shipment_Date = Now.Date
                    End If

                    BeI_Nav_PedidoTraslado.No = EnvAlm.No
                    BeI_Nav_PedidoTraslado.Status = EnvAlm.Status

                    BeI_Nav_PedidoTraslado.Transfer_from_Code = BeConfigEnc.Codigo_proveedor_produccion
                    BeI_Nav_PedidoTraslado.Transfer_from_Name = "Producción"

                    If Not BePropietario Is Nothing Then
                        BeI_Nav_PedidoTraslado.Product_Owner_Code = BePropietario.Codigo
                    End If

                    If Not EnvAlm.Location_Code Is Nothing Then
                        BeI_Nav_PedidoTraslado.Transfer_to_Code = EnvAlm.Location_Code
                        BeI_Nav_PedidoTraslado.Transfer_to_Name = EnvAlm.Location_Code
                        vTransferToCodeDefault = EnvAlm.Location_Code
                    Else
                        BeI_Nav_PedidoTraslado.Transfer_to_Code = vTransferToCodeDefault
                    End If

                    If Not EnvAlm.Assigned_User_ID Is Nothing Then
                        BeI_Nav_PedidoTraslado.Transfer_to_Contact = EnvAlm.Assigned_User_ID
                        BeI_Nav_PedidoTraslado.Transfer_to_CodeField = EnvAlm.Assigned_User_ID
                    Else
                        BeI_Nav_PedidoTraslado.Transfer_to_Contact = "ND"
                        BeI_Nav_PedidoTraslado.Transfer_to_CodeField = "ND"
                    End If


                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Envío: {0} ", BeI_Nav_PedidoTraslado.No, vbNewLine))

                    Try

                        If Not EnvAlm.WhseShptLines Is Nothing Then
                            If EnvAlm.WhseShptLines.Count > 0 Then
                                BeI_Nav_PedidoTraslado.Receipt_Document_Reference = EnvAlm.WhseShptLines.FirstOrDefault.Source_No 'Número del pedido de venta.
                            End If
                        End If

                        '#EJC20220329:Log the External_Document_No from NAV.
                        If EnvAlm.External_Document_No IsNot Nothing Then
                            BeI_Nav_PedidoTraslado.External_Document_No = EnvAlm.External_Document_No
                        Else
                            BeI_Nav_PedidoTraslado.External_Document_No = ""
                        End If

                        'Insertar Encabezado
                        clsLnI_nav_ped_traslado_enc.Insertar(BeI_Nav_PedidoTraslado,
                                                             lConnection,
                                                             lTransaction)

                        VContadorBitacoraIntermedia += 1

                        prg.Value = vContador

                        vContador += 1

                        Application.DoEvents()

                        vTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente

                        If Not EnvAlm.WhseShptLines Is Nothing Then

                            For Each L As Whse_Shipment_Line In EnvAlm.WhseShptLines

                                BeI_Nav_PedidoTrasladoDet = New clsBeI_nav_ped_traslado_det

                                Try

                                    CopyObject(L, BeI_Nav_PedidoTrasladoDet)

                                    BeI_Nav_PedidoTrasladoDet.NoEnc = EnvAlm.No
                                    BeI_Nav_PedidoTrasladoDet.No = L.Item_No
                                    BeI_Nav_PedidoTrasladoDet.Transfer_to_CodeField = L.Destination_No 'Destination_No es el cliente.
                                    BeI_Nav_PedidoTrasladoDet.Line_No = L.Line_No
                                    BeI_Nav_PedidoTrasladoDet.Source_ID = L.Source_No

                                    If L.Source_Document = 0 Then
                                        vTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                    ElseIf L.Source_Document = 4 Then
                                        vTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS
                                    Else
                                        vTipoDocumento = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                    End If

                                    If Not L.Variant_Code Is Nothing Then
                                        'Debug.Print("Espera")
                                    End If

                                    BeI_Nav_PedidoTrasladoDet.Variant_Code = L.Variant_Code

                                    '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                                    If Not L.Item_No Is Nothing Then

                                        BeProductoBodega = clsLnProducto_bodega.Existe(L.Item_No,
                                                                                       BeConfigEnc.Idbodega,
                                                                                       lConnection,
                                                                                       lTransaction)

                                        'Existe el producto en el maestro?
                                        If Not BeProductoBodega Is Nothing Then

                                            'Si Cantidad enviada es 0 se importa
                                            If L.QtyCrossDockedAllUOMBase <> L.Quantity Then

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Importando producto desde NAV a tabla intermedia : {0}", L.Item_No))

                                                '#EJC20210614:Solicitar solo la cantidad pendiente de surtir al envío.
                                                BeI_Nav_PedidoTrasladoDet.Quantity = L.Qty_Outstanding

                                                '#EJC20210614:Cantidad en unidades (pendientes)
                                                Dim vCantidadUMBas As Double = L.Qty_Outstanding_Base
                                                Dim vCantidadUnidadesPorCaja As Double = L.Qty_Outstanding_Base / L.Qty_Outstanding

                                                If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet,
                                                                                     lConnection,
                                                                                     lTransaction) Then

                                                    clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet,
                                                                                                 lConnection,
                                                                                                 lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                Else
                                                    clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet,
                                                                                         lConnection,
                                                                                         lTransaction)
                                                    VContadorBitacoraIntermedia += 1
                                                End If

                                            End If

                                        Else

                                            Try

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log("Producto no existe en maestro",
                                                                                           L.Item_No,
                                                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                           BeConfigDet.Idnavconfigdet, cnnLog)

                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", L.Item_No, vbNewLine))

                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try

                                        End If 'FIn Existe el producto en el maestro?

                                    Else
                                        Debug.Print("_: " & L.Description)
                                    End If

                                Catch ex As Exception

                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               "Sin informacion",
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de traslado: {0}{1}{2}", BeI_Nav_PedidoTrasladoDet.No, vbNewLine, ex.Message))

                                End Try

                            Next

                            '#CKFK 20211118 Aquí validamos que el tipo de documento del encabezado sea distinto del detalle
                            'si eso llegara a pasar se coloca el del detalle
                            If vTipoDocumento <> BeI_Nav_PedidoTraslado.Document_Type Then
                                BeI_Nav_PedidoTraslado.Document_Type = vTipoDocumento
                                clsLnI_nav_ped_traslado_enc.Actualizar(BeI_Nav_PedidoTraslado, lConnection, lTransaction)
                            End If

                        End If

                    Catch ex As Exception

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                  BeI_Nav_PedidoTraslado.No,
                                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                                  BeConfigDet.Idnavconfigdet,
                                                                  cnnLog)

                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar encabezado EA desde ws a intermedia: {0}{1}{2}", BeI_Nav_PedidoTraslado.No, vbNewLine, ex.Message))

                    End Try

                End If

            Next

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            Importar_Envio_Almacen_Desde_WSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Envíos de almacén desde WS a interface de TOMWMS: {0}{1}", vbNewLine, ex.Message))
            clsPublic.Actualizar_Progreso(lblprg, "")
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            clsPublic.Actualizar_Progreso(lblprg, "Fin de Proceso en tabla intermedia.")
            clsPublic.Actualizar_Progreso(lblprg, "")
        End Try

    End Function

    Public Function Importar_Envio_Almacen_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                                 ByRef prg As System.Windows.Forms.ProgressBar,
                                                                                 Optional ByVal ForzarEjecucion As Boolean = False,
                                                                                 Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False,
                                                                                 Optional ByVal pEnvioAlmacen As String = "") As Boolean
        Importar_Envio_Almacen_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim lConectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim vContador As Integer = 0
        Dim vContadorProcesados As Integer = 0
        Dim vContadorLineasDet As Integer = 0

        Try

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Envio_Almacen") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 ' clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            BeNavEjecucionRes.IdEjecucionRes = 0 'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            lConectionInterface.Open() : lTransInterface = lConectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vURLParseada As String = ""
            vURLParseada = GetIpAndPortFromUrl(My.MySettings.Default.NavSync_WSEnvioAlm_Envio_alm_Service)

            clsPublic.Actualizar_Progreso(lblprg, "")
            clsPublic.Actualizar_Progreso(lblprg, "Consultando Envíos de Almacén en: " & vURLParseada & "...")

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Envio_Almacen_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog, pEnvioAlmacen) Then
                    Exit Function
                End If

            Else

                If XtraMessageBox.Show("¿Llenar tabla intermedia desde WS?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not Importar_Envio_Almacen_Desde_WSNav_A_TablaIntermedia(lblprg, prg, CnnLog, pEnvioAlmacen) Then
                        Exit Function
                    End If
                End If

            End If

            Dim lPedidoTrasladoEnc As New List(Of clsBeI_nav_ped_traslado_enc)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando envíos de almacén en tabla intermedia ")

            lPedidoTrasladoEnc = clsLnI_nav_ped_traslado_enc.GetAll_Envios_Almacen(lConectionInterface,
                                                                                   lTransInterface)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Envíos de almacén en tabla intermedia: {0}", lPedidoTrasladoEnc.Count))

            lTransInterface.Commit()

            If lPedidoTrasladoEnc.Count > 0 Then

                If pEnvioAlmacen <> "" Then

                    Dim vResult = lPedidoTrasladoEnc.FindAll(Function(x) x.No = pEnvioAlmacen)

                    If vResult.Count = 0 Then

                        clsPublic.Actualizar_Progreso(lblprg, "MSG_202312191335: El envío: " & pEnvioAlmacen & " no está en el listado de documentos de la tabla intermedia (verifique si el envío está en estado lanzado).")

                        For Each NAVEnvioAlm As clsBeI_nav_ped_traslado_enc In lPedidoTrasladoEnc
                            clsPublic.Actualizar_Progreso(lblprg, "Envío en tabla intermedia: " & NAVEnvioAlm.No)
                        Next

                    End If

                    For Each NAVEnvioAlm As clsBeI_nav_ped_traslado_enc In lPedidoTrasladoEnc.FindAll(Function(x) x.No = pEnvioAlmacen)

                        If Importar_Envio_Almacen2(NAVEnvioAlm,
                                                  lblprg,
                                                  prg,
                                                  CnnLog) Then
                            vContadorProcesados += 1
                        End If

                        vContador += 1

                    Next

                Else

                    If XtraMessageBox.Show("Se importarán todos los pedidos lanzados ¿Está seguro(a) de continuar?", "Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        For Each NAVEnvioAlm As clsBeI_nav_ped_traslado_enc In lPedidoTrasladoEnc

                            If Importar_Envio_Almacen2(NAVEnvioAlm,
                                                      lblprg,
                                                      prg,
                                                      CnnLog) Then
                                vContadorProcesados += 1
                            End If

                            vContador += 1

                        Next

                    End If

                End If

            End If

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Total de envíos de NAV {0}, procesados correctamente: {1}", vContador, vContadorProcesados))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes,
                                                CnnLog)

        Catch ex As Exception

            Try
                If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            Catch ex1 As Exception
                '#EJC20171105_1259AM_REF01: Agregar excepción a log.
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex1.Message,
                                                           "",
                                                           BeNavEjecucionEnc.IdEjecucionEnc,
                                                           BeConfigDet.Idnavconfigdet,
                                                           CnnLog)
            End Try

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla DE TOMWMS: {0} {1}", ex.Message, vbNewLine))

        Finally

            If lConectionInterface.State = ConnectionState.Open Then lConectionInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()

            prg.Value = 0

            clsPublic.Actualizar_Progreso(lblprg, vbCrLf)

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

        End Try

    End Function

#Region "Métodos Privados"

    Private Shared Sub Crear_Pedido_Enc(ByRef pBePedidoEnc As clsBeTrans_pe_enc,
                                        ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                        ByVal vIdPropietarioBodega As Integer,
                                        ByVal BeCliente As clsBeCliente,
                                        ByVal lConnection As SqlConnection,
                                        ByVal lTransaction As SqlTransaction)

        Try

#Region "Inserta encabezado pedido"

            pBePedidoEnc.IdBodega = BeConfigEnc.Idbodega
            pBePedidoEnc.Cliente = New clsBeCliente()
            pBePedidoEnc.Cliente = BeCliente
            pBePedidoEnc.IdCliente = BeCliente.IdCliente
            pBePedidoEnc.Cliente = clsLnCliente.GetSingle(pBePedidoEnc.Cliente.IdCliente,
                                                      lConnection,
                                                      lTransaction)
            pBePedidoEnc.IdMuelle = 0
            pBePedidoEnc.PropietarioBodega = New clsBePropietario_bodega()
            pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = vIdPropietarioBodega
            pBePedidoEnc.IdPropietarioBodega = vIdPropietarioBodega
            pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo()
            pBePedidoEnc.TipoPedido.IdTipoPedido = NAVEnvioAlm.Document_Type
            pBePedidoEnc.Fecha_Pedido = NAVEnvioAlm.Posting_Date
            pBePedidoEnc.Hora_ini = Now
            pBePedidoEnc.Hora_fin = Now.AddHours(1)
            pBePedidoEnc.HoraEntregaDesde = Now
            pBePedidoEnc.HoraEntregaHasta = Now.AddHours(1)
            pBePedidoEnc.Ubicacion = 1
            pBePedidoEnc.Estado = "Nuevo"
            pBePedidoEnc.No_despacho = 0
            pBePedidoEnc.Activo = True
            If pBePedidoEnc.User_agr = 0 Then
                pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario
            End If
            pBePedidoEnc.Fec_agr = Now
            If pBePedidoEnc.User_mod = 0 Then
                pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario
            End If
            pBePedidoEnc.Fec_mod = Now
            pBePedidoEnc.Local = True
            pBePedidoEnc.Pallet_primero = True
            pBePedidoEnc.Dias_cliente = 0
            pBePedidoEnc.Anulado = False
            pBePedidoEnc.IdPickingEnc = 0
            pBePedidoEnc.RoadKilometraje = 0
            pBePedidoEnc.RoadFechaEntr = NAVEnvioAlm.Shipment_Date
            pBePedidoEnc.RoadDirEntrega = ""
            pBePedidoEnc.RoadTotal = 0
            pBePedidoEnc.RoadDesMonto = 0
            pBePedidoEnc.RoadImpMonto = 0
            pBePedidoEnc.RoadPeso = 0
            pBePedidoEnc.RoadBandera = 0
            pBePedidoEnc.RoadStatCom = ""
            pBePedidoEnc.RoadCalcoBJ = 0
            pBePedidoEnc.RoadImpres = 0
            pBePedidoEnc.RoadADD1 = ""
            pBePedidoEnc.RoadADD2 = ""
            pBePedidoEnc.RoadADD3 = ""
            pBePedidoEnc.RoadStatProc = 0
            pBePedidoEnc.RoadRechazado = 0
            pBePedidoEnc.RoadRazon_Rechazado = 0
            pBePedidoEnc.RoadInformado = 0
            pBePedidoEnc.RoadSucursal = ""
            pBePedidoEnc.RoadIdDespacho = 0
            pBePedidoEnc.RoadIdFacturacion = 0
            pBePedidoEnc.RoadIdRuta = 0
            pBePedidoEnc.RoadIdVendedor = 0
            pBePedidoEnc.RoadIdRutaDespacho = 0
            pBePedidoEnc.RoadIdVendedorDespacho = 0
            pBePedidoEnc.Enviado_A_ERP = False
            pBePedidoEnc.No_Documento_Externo = NAVEnvioAlm.External_Document_No

            clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc, lConnection, lTransaction)

#End Region

        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' '#EJC202312132332
    ''' </summary>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Private Shared Sub Configuracion_Interface_Correcta(ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction)

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                          lConnection,
                                                          lTransaction)
            If BeConfigEnc Is Nothing Then
                If BD.Instancia.IdConfiguracionInterface = 0 Then
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                Else
                    Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                End If
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

    Private Shadows Function Get_Cliente(ByRef Transfer_to_Code As String,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction) As clsBeCliente

        Get_Cliente = Nothing

        Try

            Dim BeCliente As New clsBeCliente
            BeCliente = clsLnCliente.Get_Single_By_Codigo(Transfer_to_Code,
                                                         lConnection,
                                                         lTransaction)

            If BeCliente Is Nothing Then
                Throw New Exception(String.Format("{0} No existe el cliente {1} en maestro para pedido de tralsado ", MethodBase.GetCurrentMethod.Name(), Transfer_to_Code))
            Else
                Get_Cliente = BeCliente
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Sub Muestra_Progreso_Inicio(ByRef lblprg As RichTextBox, ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc)
        clsPublic.Actualizar_Progreso(lblprg, "Iniciando importación de envío de almacén: " & NAVEnvioAlm.No, True)
        clsPublic.Actualizar_Progreso(lblprg, "Trasladando documento a TOMWMS.", True)
    End Sub

    Private Shared Sub Valida_Transferencia_Interna(ByRef NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction)

        Try

            If NAVEnvioAlm.Document_Type = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then

                Dim pSyncNavPedidoTraslado As New clsSyncNavPedidoTraslado
                Dim resultPedidoTransferencia As New Pedidos_Transferencia

                resultPedidoTransferencia = pSyncNavPedidoTraslado.Get_Pedido_Transferencia_Envio_FromWS(lConnection,
                                                                                                         lTransaction,
                                                                                                         NAVEnvioAlm.Receipt_Document_Reference,
                                                                                                         True)

                If Not resultPedidoTransferencia Is Nothing Then
                    NAVEnvioAlm.Transfer_to_Code = resultPedidoTransferencia.Transfer_to_Code
                Else
                    Throw New Exception("Error_202302021324: No se obtuvo resultado del pedido de transferencia de NAV es posible que no se haya ")
                End If

            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Shared lProductos As New List(Of clsBeProducto)
    Private Shared lUnidadesMedida As New List(Of clsBeUnidad_medida)
    Private Shared lPresentaciones As New List(Of clsBeProducto_Presentacion)
    Private Shared lClientes As New List(Of clsBeCliente)

    Public Shared Function Get_Producto(ByVal Item_No As String,
                                        ByVal lConnection As SqlConnection,
                                        ByVal lTransaction As SqlTransaction) As clsBeProducto


        Dim vIndiceListaProducto As Integer = -1
        Dim vIndiceListaPresentacion As Integer = -1
        Dim vIndiceListaUnidadMedida As Integer = -1
        Dim BeProducto As New clsBeProducto


        Try

            vIndiceListaProducto = lProductos.FindIndex(Function(x) x.Codigo = Item_No)

            If vIndiceListaProducto > -1 Then
                BeProducto = New clsBeProducto()
                BeProducto = lProductos(vIndiceListaProducto).Clone()
                Get_Producto = BeProducto
            Else

                BeProducto = New clsBeProducto()
                BeProducto.Codigo = Item_No
                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(Item_No,
                                                                    BeConfigEnc.Idbodega,
                                                                    lConnection,
                                                                    lTransaction)

                If BeProducto Is Nothing Then
                    Throw New Exception("ERROR_202312130419: " & String.Format("El producto: {0} NO existe en el catálogo o no está asociado a la bodega: {1}", Item_No, BeConfigEnc.Idbodega))
                End If

                lProductos.Add(BeProducto)
                Get_Producto = BeProducto

            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Class Presentaciones
        Public Property Defecto As clsBeProducto_Presentacion
        Public Property Pedido As clsBeProducto_Presentacion
    End Class

    Private Shared Function Get_Presentacion(ByRef BeProducto As clsBeProducto,
                                             ByVal PDet As clsBeI_nav_ped_traslado_det,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As Presentaciones

        Dim vIndiceListaPresentacion As Integer = -1
        Dim BeProductoPresentacionPedido As New clsBeProducto_Presentacion
        Dim BeProductoPresentacionDefecto As New clsBeProducto_Presentacion
        Dim resultado As New Presentaciones()
        Get_Presentacion = Nothing

        Try

            If Not (BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code) Then

                If PDet.Variant_Code = "" Then

                    If Not (PDet.Unit_of_Measure_Code.Trim = "") Then

                        vIndiceListaPresentacion = lPresentaciones.FindIndex(Function(x) x.Codigo_barra = PDet.Unit_of_Measure_Code.Trim)

                        If vIndiceListaPresentacion > -1 Then
                            BeProductoPresentacionPedido = New clsBeProducto_Presentacion()
                            BeProductoPresentacionPedido = lPresentaciones(vIndiceListaPresentacion).Clone()
                            resultado.Pedido = New clsBeProducto_Presentacion()
                            resultado.Pedido = BeProductoPresentacionPedido
                        Else

                            BeProductoPresentacionPedido = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(PDet.No,
                                                                                                                        PDet.Unit_of_Measure_Code.Trim,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                            If Not BeProductoPresentacionPedido Is Nothing Then
                                PDet.Variant_Code = PDet.Unit_of_Measure_Code
                                lPresentaciones.Add(BeProductoPresentacionPedido)
                                resultado.Pedido = New clsBeProducto_Presentacion()
                                resultado.Pedido = BeProductoPresentacionPedido
                            End If

                        End If

                    End If

                End If

            Else

                resultado.Pedido = Nothing

                If vIndiceListaPresentacion > -1 Then
                    BeProductoPresentacionDefecto = New clsBeProducto_Presentacion()
                    BeProductoPresentacionDefecto = lPresentaciones(vIndiceListaPresentacion).Clone()
                    resultado.Defecto = BeProductoPresentacionDefecto
                Else

                    BeProductoPresentacionDefecto = clsLnProducto_presentacion.Get_Single_By_Codigo_Producto(PDet.Item_No,
                                                                                                              lConnection,
                                                                                                              lTransaction)

                    If Not BeProductoPresentacionDefecto Is Nothing Then
                        'PDet.Variant_Code = PDet.Unit_of_Measure_Code
                        '#CKFK20240126 Esto antes estaba afuera del if 
                        lPresentaciones.Add(BeProductoPresentacionDefecto)
                        resultado.Defecto = BeProductoPresentacionDefecto
                    End If

                End If

            End If

        Catch ex As Exception
            Throw
        End Try

        Return resultado

    End Function

    Private Shared Function Get_Unidad_Medida_Basica(ByVal Unit_of_Measure_Code As String,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As clsBeUnidad_medida

        Dim BeUnidadMedida As New clsBeUnidad_medida
        Get_Unidad_Medida_Basica = Nothing

        Dim vIndiceUnidadMedida As Integer = -1

        Try

            vIndiceUnidadMedida = lUnidadesMedida.FindIndex(Function(x) x.Codigo = Unit_of_Measure_Code)

            If vIndiceUnidadMedida > -1 Then
                BeUnidadMedida = New clsBeUnidad_medida()
                BeUnidadMedida = lUnidadesMedida(vIndiceUnidadMedida).Clone()
            Else

                BeUnidadMedida = New clsBeUnidad_medida
                BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(Unit_of_Measure_Code,
                                                                                lConnection,
                                                                                lTransaction)

                If Not BeUnidadMedida Is Nothing Then
                    lUnidadesMedida.Add(BeUnidadMedida)
                End If

            End If

            If BeUnidadMedida Is Nothing Then
                Throw New Exception(String.Format("{0} No existe la U.M. {1} en el maestro de WMS. ", MethodBase.GetCurrentMethod.Name(), Unit_of_Measure_Code))
            Else
                Get_Unidad_Medida_Basica = BeUnidadMedida
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function Importar_Envio_Almacen(ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                           ByRef lblprg As RichTextBox,
                                           ByRef prg As Windows.Forms.ProgressBar,
                                           ByRef CnnLog As SqlConnection) As Boolean

        Importar_Envio_Almacen = False

#Region "Variables"

        Dim lConnectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim Source_ID As String = ""
        Dim vCodigoProducto As String = ""
        Dim vCantidad As Double = 0
        Dim vNoLinea As Integer = 0
        Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
        Dim TrasladoExistente As clsBeTrans_pe_enc = Nothing
        Dim Lineas_Detalle_Procesadas As New List(Of clsBeI_nav_ped_traslado_det)
        Dim BeCliente As New clsBeCliente()
        Dim BeClienteDetalle As New clsBeCliente()
        Dim vContadorLineasDet As Integer = 0
        Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeProducto As New clsBeProducto()
        Dim BeStockResPedido As New clsBeStock_res()
        Dim pBePedidoDet As New clsBeTrans_pe_det()
        Dim vClienteTiempo As New clsBeCliente_tiempos()
        Dim vDiasVencimientoCliente As Integer = 0
        Dim BeUnidadMedida As New clsBeUnidad_medida
        Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0
        Dim vIdPropietarioBodega As Integer = 0
        Dim vCodigoBodega As String = ""
        Dim vCodeUnitNavError As Boolean = False
        Dim vInsertoLineaDetalle As Boolean = False
        'Debería indicar si se creo correctamente y cuál es el número de referencia.                
        Dim vRespuestaSetWarehouseDocuments As String = ""
        Dim vRespuestaInsertLotNo As String = ""
        Dim vRespuestaNoPicking As String = ""
        Dim CurrentWkshName As String = "GENERICO" '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim CurrentSortingMethod As Integer = 1 '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim TheWorkSheetNav() As Crea_picking
        Dim vFechaVenceNav As String = ""
        Dim vCrearPickingNav As Boolean = False
        Dim lDocumentosHojaDeTrabajo As New List(Of String)
        Dim cantLineasHojaTrabajo As Integer = 0
        Dim lBeStockResPedido As New List(Of clsBeStock_res)
        Dim lBeStockResPedidoFilter As New List(Of clsBeStock_res)
        Dim ThePickingNAV As New Picking()
        Dim lProductoPresentacion As New List(Of clsBeProducto_Presentacion)
        Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
        Dim BeProductoPresentacionDefecto As New clsBeProducto_Presentacion()
        Dim vDeltaFactorPresentacion As Double = 0
        Dim vCantidadEnteraPresentacion As Double = 0
        Dim vCantidadSobranteUnidades As Double = 0
        Dim vExplosionAutomatica As Boolean = False
        Dim vClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeClienteBodegaDetalle As New clsBeCliente_bodega
        Dim Presentaciones As New Presentaciones
        Dim srvWorkSheet As New Crea_picking_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

        srvWorkSheet.Url = My.Settings.NavSync_WSCreaPicking_Crea_picking_Service

        '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
        Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                .Credentials = CredencialesConexion
                                               }

        wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

        '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
        Dim wsSrvPickingNAV As New Picking_Service() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                           .Credentials = CredencialesConexion
                                                          }

        wsSrvPickingNAV.Url = My.MySettings.Default.NavSync_WSPicking_Picking_Service
        Dim BePresentacion As New clsBeProducto_Presentacion()
        Dim vNombrePresentacion As String = ""
        Dim vEnviarEnPresentacion As Boolean = False
        Dim vCantidadAEnviar As Double = 0

#End Region

        Try

            lConnectionInterface.Open() : lTransInterface = lConnectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            Muestra_Progreso_Inicio(lblprg, NAVEnvioAlm)

            VContadorBitacoraTomims = 0

            Configuracion_Interface_Correcta(lConnectionInterface, lTransInterface)

            vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(BeConfigEnc.IdPropietario,
                                                                                                                 BeConfigEnc.Idbodega,
                                                                                                                 lConnectionInterface,
                                                                                                                 lTransInterface)

            vCodigoBodega = clsLnBodega.Get_Codigo_By_IdBodega(BeConfigEnc.Idbodega,
                                                               lConnectionInterface,
                                                               lTransInterface)

            lPresentaciones.Clear()

            If NAVEnvioAlm.Status > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Envío No.: {0} ", NAVEnvioAlm.No, vbNewLine), True)

                If NAVEnvioAlm.Lineas_Detalle.Count > 0 Then

                    pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = NAVEnvioAlm.No,
                                                                 .IdTipoPedido = NAVEnvioAlm.Document_Type}

                    Lineas_Detalle_Procesadas = New List(Of clsBeI_nav_ped_traslado_det)

                    TrasladoExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                   lConnectionInterface,
                                                                                   lTransInterface)

                    vContadorLineasDet = 0

                    Valida_Transferencia_Interna(NAVEnvioAlm,
                                                 lConnectionInterface,
                                                 lTransInterface)

                    BeCliente = Get_Cliente(NAVEnvioAlm.Transfer_to_Code,
                                           lConnectionInterface,
                                           lTransInterface)

                    If Not TrasladoExistente Is Nothing Then
                        pBePedidoEnc.Activo = True
                    Else

                        pBePedidoEnc.Fecha_Pedido = NAVEnvioAlm.Posting_Date
                        pBePedidoEnc.Referencia = NAVEnvioAlm.No
                        pBePedidoEnc.User_agr = pIdUsuario
                        pBePedidoEnc.User_mod = pIdUsuario

                        If BeConfigEnc Is Nothing Then
                            BeConfigEnc = New clsBeI_nav_config_enc
                            BeConfigEnc.Idbodega = 1
                        End If

                        Crear_Pedido_Enc(pBePedidoEnc,
                                         NAVEnvioAlm,
                                         vIdPropietarioBodega,
                                         BeCliente,
                                         lConnectionInterface,
                                         lTransInterface)

                        vContador_Lineas_Detalle_Pedido_Insertadas = 0

#Region "Inserta Detalle del pedido"

                        pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                        For Each PDet In NAVEnvioAlm.Lineas_Detalle.OrderBy(Function(x) x.Line_No)

                            BeProductoPresentacion = Nothing
                            vDiasVencimientoCliente = 0
                            If Not lBeStockResPedido Is Nothing Then lBeStockResPedido.Clear()

                            BeProducto = Get_Producto(PDet.Item_No,
                                                      lConnectionInterface,
                                                      lTransInterface)

                            Presentaciones = New Presentaciones()
                            Presentaciones = Get_Presentacion(BeProducto,
                                                              PDet,
                                                              lConnectionInterface,
                                                              lTransInterface)

                            BeProductoPresentacion = Presentaciones.Pedido
                            BeProductoPresentacionDefecto = Presentaciones.Defecto

                            If BeProductoPresentacion Is Nothing Then

                                BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(PDet.Unit_of_Measure_Code,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                                If BeUnidadMedida Is Nothing Then
                                    Throw New Exception(String.Format("{0} No existe la U.M. {1} en el maestro de WMS. ", MethodBase.GetCurrentMethod.Name(), PDet.Unit_of_Measure_Code))
                                End If
                            Else
                                '#CKFK20240109 Le asigné al objeto BeUnidadMedida el del producto
                                BeUnidadMedida = BeProducto.UnidadMedida
                            End If

                            vDiasVencimientoCliente = 0

                            BeClienteDetalle = Get_Cliente_Detalle(PDet,
                                                                   NAVEnvioAlm,
                                                                   BeProducto,
                                                                   pBePedidoEnc,
                                                                   lConnectionInterface,
                                                                   lTransInterface,
                                                                   CnnLog,
                                                                   lblprg,
                                                                   prg,
                                                                   pClienteTiemposList,
                                                                   BeClienteBodegaDetalle,
                                                                   vClienteTiempo)

                            If Not vClienteTiempo Is Nothing Then
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                            Else
                                vDiasVencimientoCliente = 0
                            End If

                            If Not BeClienteDetalle Is Nothing Then
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Cliente: {0} - {1}", BeClienteDetalle.Codigo, BeClienteDetalle.Nombre_comercial))
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Tiempo vida (Días):{1}", vbTab, vDiasVencimientoCliente))
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "Tiempos de cliente: 0 (no definidos).")
                            End If

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Source_ID:{1}", vbTab, PDet.Source_ID))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Line_No:{1}", vbTab, PDet.Line_No))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Código:{1}", vbTab, BeProducto.Codigo))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Nombre:{1}", vbTab, BeProducto.Nombre))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Solicitado:{1}-{2}", vbTab, PDet.Quantity, PDet.Unit_of_Measure_Code))

                            Dim vCodigosDebug As String() = {"00174459", "00193250", "00170440", "00194250"}

                            If vCodigosDebug.Contains(BeProducto.Codigo) Then
                                Debug.WriteLine("espere aquí por favor, señor compilador.")
                            End If

                            Application.DoEvents()

                            vInsertoLineaDetalle = False

                            '#EJC20220224_0123AM: El producto viene en UMBAS.
                            If BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code Then 'And Not PDet.Unit_of_Measure_Code.StartsWith("CJ")

                                '#EJC20220224_0124: Si la implosión automática está activa en la configuración de la interface.
                                If BeConfigEnc.Explosion_Automatica Then

                                    '#EJC20220224_0125: Buscar la presentación por "defecto", buscar la primera que deberían ser las cajas.
                                    lProductoPresentacion = clsLnProducto_presentacion.Get_All_By_IdProducto_By_IdBodega(BeProducto.IdProducto,
                                                                                                                         True,
                                                                                                                         BeConfigEnc.Idbodega,
                                                                                                                         lConnectionInterface,
                                                                                                                         lTransInterface)

                                    If Not lProductoPresentacion Is Nothing Then

                                        '#EJC20220224: Trabajar con la primera presentación, obtener el factor y determinar si debe 
                                        'ocurrir o no la implosión.
                                        If lProductoPresentacion.Count = 1 Then

                                            BeProductoPresentacion = lProductoPresentacion(0)

                                            If Not BeProductoPresentacion Is Nothing Then

                                                '#EJC20220224_0126: Si la cantidad solicitada es mayor que el factor por presentación
                                                'es decir: la cantidad excede las unidades por caja...
                                                If PDet.Quantity >= BeProductoPresentacion.Factor Then

                                                    vDeltaFactorPresentacion = Math.Round(PDet.Quantity / BeProductoPresentacion.Factor, 6)
                                                    vCantidadEnteraPresentacion = Math.Truncate(vDeltaFactorPresentacion)
                                                    vCantidadSobranteUnidades = Math.Round(Math.Abs((vCantidadEnteraPresentacion - vDeltaFactorPresentacion) * BeProductoPresentacion.Factor))

                                                    Dim vFactorDeRelacionUnidades As Double = 0

                                                    If vCantidadSobranteUnidades = 0 Then
                                                        PDet.Quantity = vCantidadEnteraPresentacion
                                                        PDet.Quantity_In_UMBas = 0
                                                        PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                    Else
                                                        PDet.Quantity = vDeltaFactorPresentacion
                                                        PDet.Quantity_In_UMBas = vCantidadSobranteUnidades
                                                        PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                    End If

                                                    vExplosionAutomatica = True

                                                Else
                                                    'La cantidad es menor que una caja, solicitar unidades a WMS.
                                                    vExplosionAutomatica = False
                                                End If

                                            End If

                                        Else
                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error_20220224_0121: La implosión automática está activa, pero se encontró más de una presentación para el producto: {0}, el sistema no puede determinar el factor de implosión.",
                                                                                                     BeProducto.Codigo),
                                                                                                     NAVEnvioAlm.No,
                                                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                     BeConfigDet.Idnavconfigdet,
                                                                                                     CnnLog)
                                        End If

                                    End If

                                End If

                            End If

                            If TrasladoExistente Is Nothing Then

                                Try

                                    If BeClienteDetalle Is Nothing Then

                                        vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                        'Primera llamada (No tiene Cliente en el detalle)
                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            BeClienteDetalle,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                            '#EJC2312260941:
                                            'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                        End If

                                        If lBeStockResPedido Is Nothing Then

                                            If lBeStockResPedido.Count > 0 Then

                                                '#EJC20220314:La solicitud se hizo en UMBAS y no se encontró stock.
                                                If PDet.Variant_Code = "" Then

                                                    '#EJC20220314: Existe una presentación con el codigo de producto proporcionado.
                                                    If Not BeProductoPresentacionDefecto Is Nothing Then

                                                        '#EJC20220314:Intentar reservar el stock solicitado en UMBAS a partir del stock en presentación.
                                                        PDet.Variant_Code = PDet.Unit_of_Measure_Code
                                                        PDet.Quantity = 0

                                                        'Segunda llamada
                                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                            PDet,
                                                                                            BeProducto,
                                                                                            vDiasVencimientoCliente,
                                                                                            BeUnidadMedida,
                                                                                            BeProductoPresentacion,
                                                                                            BeClienteDetalle,
                                                                                            BeConfigEnc,
                                                                                            BeConfigEnc.Idbodega,
                                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                                            0,
                                                                                            lblprg,
                                                                                            lBeStockResPedido,
                                                                                            lConnectionInterface,
                                                                                            CnnLog,
                                                                                            lTransInterface) Then
                                                            '#EJC202312260939:

                                                        End If

                                                    End If

                                                End If

                                            End If

                                        End If

                                    Else
                                        'Tercera llamada (tiene cliente en el detalle)
                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                        PDet,
                                                                        BeProducto,
                                                                        vDiasVencimientoCliente,
                                                                        BeUnidadMedida,
                                                                        BeProductoPresentacion,
                                                                        BeClienteDetalle,
                                                                        BeConfigEnc,
                                                                        BeConfigEnc.Idbodega,
                                                                        pBePedidoEnc.IdPropietarioBodega,
                                                                        0,
                                                                        lblprg,
                                                                        lBeStockResPedido,
                                                                        lConnectionInterface,
                                                                        CnnLog,
                                                                        lTransInterface) Then

                                            '#EJC2312260941:
                                            'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                        End If

                                    End If

                                    If Not lBeStockResPedido Is Nothing Then

                                        If lBeStockResPedido.Count > 0 Then

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            vInsertoLineaDetalle = True

                                            Dim vCantidadReservadaWMS As Double = 0
                                            vCantidadReservadaWMS = lBeStockResPedido.Sum(Function(x) x.Cantidad)

                                            If Not BeProductoPresentacion Is Nothing Then
                                                vCantidadReservadaWMS = IIf(PDet.Variant_Code = "", vCantidadReservadaWMS, vCantidadReservadaWMS / IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))
                                                If vExplosionAutomatica Then
                                                    If Not PDet.Variant_Code.StartsWith("CJ") Then
                                                        vCantidadReservadaWMS = vCantidadReservadaWMS * BeProductoPresentacion.Factor
                                                    End If
                                                End If
                                            Else
                                                Debug.WriteLine("#EJC202312191300: Es valido?")
                                            End If

                                            PDet.Qty_to_Ship = vCantidadReservadaWMS

                                            If PDet.Is_Partially_Processed Then
                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea procesada parcialmente, Línea:{0}  Código:{1}  Solicitado: {2}{3} Abastecido:{4}  ",
                                                                                          PDet.Line_No,
                                                                                          PDet.Item_No,
                                                                                          PDet.Quantity,
                                                                                          IIf(PDet.Variant_Code = "", PDet.Unit_of_Measure_Code, PDet.Unit_of_Measure_Code),
                                                                                          IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Qty_to_Ship /
                                                                                                  IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))))
                                                clsPublic.Actualizar_Progreso_CR(lblprg)
                                            Else


                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("{2}{2}Línea procesada -> Abastecido:{0}{1}  ",
                                                                                          IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Quantity_Reserved_WMS),
                                                                                          IIf(PDet.Variant_Code = "", PDet.Unit_of_Measure_Code, PDet.Unit_of_Measure_Code),
                                                                                          vbTab))

                                                clsPublic.Actualizar_Progreso_CR(lblprg)

                                            End If

                                            Lineas_Detalle_Procesadas.Add(PDet)

                                        Else

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea procesada -> Abastecido:{0}{1}  ", 0, vbNewLine))
                                            clsPublic.Actualizar_Progreso_CR(lblprg)

                                        End If

                                    End If

                                Catch ex As Exception
                                    Throw
                                End Try

                            Else 'es un pedido existente.

                                'Si la línea de detalle no existe
                                If Not clsLnTrans_pe_det.Existe(TrasladoExistente.IdPedidoEnc,
                                                                PDet.Line_No,
                                                                pBePedidoDet,
                                                                PDet.No,
                                                                lConnectionInterface,
                                                                lTransInterface) Then

                                    Try


                                        If BeClienteDetalle Is Nothing Then

                                            'Cuarta llamada
                                            If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            Nothing,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                                '#EJC2312260941:
                                                'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                            End If

                                        Else

                                            'Quinta llamada
                                            If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            BeClienteDetalle,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                                '#EJC2312260941:
                                                'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                            End If

                                        End If

                                        If Not lBeStockResPedido Is Nothing Then

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            vInsertoLineaDetalle = True

                                            Dim vCantidadReservadaWMS As Double = 0
                                            vCantidadReservadaWMS = lBeStockResPedido.Sum(Function(x) x.Cantidad)
                                            PDet.Qty_to_Ship = vCantidadReservadaWMS
                                            vCantidadReservadaWMS = IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Qty_to_Ship / IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))

                                            Lineas_Detalle_Procesadas.Add(PDet)

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc),
                                                                                                PDet.No,
                                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                BeConfigDet.Idnavconfigdet,
                                                                                                CnnLog)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc))

                                        End If

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   pBePedidoEnc.IdPedidoEnc,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        Throw

                                    End Try

                                Else

                                    If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                        Dim vMensajeError As String = String.Format("El pedido: {0} existe,
                                                                                     la línea de detalle: {1} existe, 
                                                                                     cantidad_origen <> cantidad_destino
                                                                                     no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No)

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeError,
                                                                                   PDet.No,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        clsPublic.Actualizar_Progreso(lblprg, vMensajeError)
                                        clsPublic.Actualizar_Progreso_CR(lblprg)

                                    Else

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} existe,
                                                                                   la línea de detalle: {1} existe, 
                                                                                   cantidad_origen = cantidad_destino
                                                                                   no se actualizará", PDet.NoEnc, PDet.Line_No))
                                        clsPublic.Actualizar_Progreso_CR(lblprg)

                                    End If

                                End If

                            End If 'fin TrasladoExistente                                    

                        Next

                        Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
#End Region

                    End If 'fin TrasladoExistente 1

#Region "Si hay lineas reservadas se crea hoja de trabajo"

                    Try

                        If vContador_Lineas_Detalle_Pedido_Insertadas = 0 Then

                            If (pBePedidoEnc.IdPedidoEnc <> 0) Then
                                If Not clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                                       lConnectionInterface,
                                                                       lTransInterface) Then

                                    clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc, lConnectionInterface, lTransInterface)
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", NAVEnvioAlm.No, vbNewLine))
                                    clsPublic.Actualizar_Progreso_CR(lblprg)

                                Else
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} no insertó líneas nuevas, pero ya tiene detalle en WMS; no se elimina la cabecera: {1}", NAVEnvioAlm.No, vbNewLine))
                                    clsPublic.Actualizar_Progreso_CR(lblprg)
                                End If
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} ya existe en el WMS {1}", NAVEnvioAlm.No, vbNewLine))
                                clsPublic.Actualizar_Progreso_CR(lblprg)
                            End If

                        Else

                            vCodeUnitNavError = False

                            Try

                                '#EJC20210709: Si ya se registró en la hoja de trabajo NAV no registra nuevamente.
                                If Not lDocumentosHojaDeTrabajo.Contains(NAVEnvioAlm.No) Then

                                    vRespuestaSetWarehouseDocuments = ""

                                    '#EJC20210614: Envía a traer las líneas o cantidades pendientes de envío.
                                    wsCUWMS.SetWarehouseDocuments(CurrentWkshName,
                                                                  NAVEnvioAlm.No,
                                                                  vRespuestaSetWarehouseDocuments)

                                    lDocumentosHojaDeTrabajo.Add(NAVEnvioAlm.No)

                                End If

                                '#EJC202301242112: Actualizar a estado importado...
                                NAVEnvioAlm.Status = 3

                                clsLnI_nav_ped_traslado_enc.Actualizar_Estado(NAVEnvioAlm, lConnectionInterface, lTransInterface)

                            Catch ex As Exception

                                '#EJC20210707: es probable que el envío al que se hace referencia ya no esté disponible o que ya no tenga lineas pendientes.
                                If ex.Message.Contains("There are no Warehouse Worksheet Lines created.") Then

                                    vCodeUnitNavError = True

                                    clsPublic.Actualizar_Progreso(lblprg, "Error en Code Unit - SetWarehouseDocuments " & ex.Message)
                                    clsPublic.Actualizar_Progreso_CR(lblprg)
                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               pBePedidoEnc.IdPedidoEnc,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                Else
                                    Throw
                                End If

                            End Try

                            If Not vCodeUnitNavError Then

                                '#EJC2021116: Leer el picking_nav
                                'Llena la hoja de trabajo con lo que tiene el documento
                                TheWorkSheetNav = srvWorkSheet.ReadMultiple(CurrentWkshName,
                                                                            vCodigoBodega,
                                                                            CurrentSortingMethod,
                                                                            Nothing,
                                                                            Nothing,
                                                                            0)

                                If Not TheWorkSheetNav Is Nothing Then

                                    For Each PDet In Lineas_Detalle_Procesadas.OrderBy(Function(x) x.Line_No)

                                        Source_ID = PDet.Source_ID
                                        vCantidad = PDet.Quantity
                                        vCodigoProducto = PDet.Item_No
                                        vNoLinea = PDet.Line_No

                                        BePresentacion = New clsBeProducto_Presentacion()

                                        If PDet.Variant_Code <> "" Then

                                            BePresentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(PDet.Item_No,
                                                                                                                                PDet.Variant_Code,
                                                                                                                                lConnectionInterface,
                                                                                                                                lTransInterface)

                                            If Not BePresentacion Is Nothing Then
                                                vNombrePresentacion = BePresentacion.Nombre
                                                vEnviarEnPresentacion = True
                                            Else
                                                vEnviarEnPresentacion = False
                                            End If

                                        Else
                                            vEnviarEnPresentacion = False
                                            BePresentacion = Nothing
                                            vNombrePresentacion = ""
                                        End If

                                        '#EJC20250423: Así estaba antes, pienso que el proceso de lectura puede mejorarse o ser más rápido si colocamos filtros (veamos).
                                        TheWorkSheetNav = srvWorkSheet.ReadMultiple(CurrentWkshName,
                                                                                    vCodigoBodega,
                                                                                    CurrentSortingMethod,
                                                                                    Nothing,
                                                                                    Nothing,
                                                                                    0)


                                        Dim lItemOnWorkSheet = TheWorkSheetNav.Where(Function(x) x.Item_No = vCodigoProducto _
                                                                                     AndAlso x.Whse_Document_No = NAVEnvioAlm.No _
                                                                                     AndAlso x.Source_No = Source_ID _
                                                                                     AndAlso x.Whse_Document_Line_No = vNoLinea)

                                        If Not lItemOnWorkSheet Is Nothing Then

                                            If lItemOnWorkSheet.Count = 0 Then
                                                clsPublic.Actualizar_Progreso(lblprg, "#ERROR_20231218: No se encontró el objeto en la hoja de trabajo de NAV para este registro.")
                                            End If

                                        End If

                                        For Each ItemOnWorkSheet In TheWorkSheetNav.Where(Function(x) x.Item_No = vCodigoProducto _
                                                                                          AndAlso x.Whse_Document_No = NAVEnvioAlm.No _
                                                                                          AndAlso x.Source_No = Source_ID _
                                                                                          AndAlso x.Whse_Document_Line_No = vNoLinea)


                                            If ItemOnWorkSheet.Qty_to_Handle = 0 Then

                                                vInsertoLineaDetalle = True

                                                If PDet.Unit_of_Measure_Code = ItemOnWorkSheet.Unit_of_Measure_Code Then

                                                    clsPublic.Actualizar_Progreso(lblprg, "Actualizando - Qty_to_Handle de Source_ID" & PDet.Source_ID & " Codigo: " & PDet.Item_No & " Línea: " & vNoLinea)

                                                    '#EJC20220316:Está en UMBAS
                                                    If (PDet.Unit_of_Measure_Code = ItemOnWorkSheet.Unit_of_Measure_Code) AndAlso
                                                        (PDet.Unit_of_Measure_Code = BeProducto.UnidadMedida.Codigo) Then

                                                        vCantidadAEnviar = IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Quantity_Reserved_WMS)

                                                        If vEnviarEnPresentacion Then
                                                            ItemOnWorkSheet.Qty_to_Handle += vCantidadAEnviar
                                                        Else
                                                            ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                        End If

                                                    Else

                                                        '#EJC202312191414:Aqui generalmente entra cuando son UNI.
                                                        '(No se para que se valida la presentacion)
                                                        If Not BePresentacion Is Nothing Then

                                                            If BePresentacion.Factor > 0 Then
                                                                If vInsertoLineaDetalle Then
                                                                    ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                                Else
                                                                    ItemOnWorkSheet.Qty_to_Handle = 0
                                                                End If
                                                            Else
                                                                Throw New Exception("Factor incorrecto para la presentación de producto al actualizar la cantidad a manejar en el codeunit.")
                                                            End If

                                                        Else
                                                            ItemOnWorkSheet.Qty_to_Handle += (PDet.Qty_to_Ship)
                                                        End If

                                                    End If

                                                Else

                                                    If vInsertoLineaDetalle Then
                                                        ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                    Else
                                                        ItemOnWorkSheet.Qty_to_Handle = 0
                                                    End If
                                                End If

                                                '#CKFK20220201 Metí el try dentro del if 
                                                If vInsertoLineaDetalle Then

                                                    Try

                                                        '#EJC20211116: Actualizar picking con la cantidad que tiene WMS.
                                                        srvWorkSheet.UpdateAsync(CurrentWkshName,
                                                                            vCodigoBodega,
                                                                            CurrentSortingMethod,
                                                                            ItemOnWorkSheet)

                                                        Dim vContadorLotesProcesadosCorrectamente As Integer = 0

                                                        lBeStockResPedido = clsLnStock_res.Get_All_By_Params_For_BYB(pBePedidoEnc.IdPedidoEnc,
                                                                                                                     vCodigoProducto,
                                                                                                                     vNoLinea,
                                                                                                                     Source_ID,
                                                                                                                     lConnectionInterface,
                                                                                                                     lTransInterface)


                                                        Dim ListaStockReservadoConsolidado = Nothing
                                                        Dim vCantidadLote As Double = 0

                                                        If Not lBeStockResPedido Is Nothing Then

                                                            ListaStockReservadoConsolidado = From i In lBeStockResPedido Group i By Keys = New With {Key i.IdProductoBodega,
                                                                                Key i.Lote,
                                                                                Key i.Fecha_vence,
                                                                                Key i.IdUnidadMedida} Into Group
                                                                                             Select New With {Keys.IdProductoBodega,
                                                                                                              Keys.Lote,
                                                                                                              Keys.Fecha_vence,
                                                                                                              Keys.IdUnidadMedida,
                                                                                                              .Cantidad = Group.Sum(Function(x) x.Cantidad)}

                                                            If Not ListaStockReservadoConsolidado Is Nothing Then

                                                                For Each StockReservado In ListaStockReservadoConsolidado

                                                                    vFechaVenceNav = StockReservado.Fecha_vence.Date.Year & "-" & Right("00" & StockReservado.Fecha_vence.Date.Month, 2) & "-" & Right("00" & StockReservado.Fecha_vence.Day, 2)

                                                                    clsPublic.Actualizar_Progreso(lblprg, vbTab & "Registrando lote: " & StockReservado.Lote & " Cantidad: " & StockReservado.Cantidad & " Código: " & PDet.Item_No)

                                                                    vCantidadLote = 0

                                                                    '#EJC20211116: Indicarle a NAV, que lote se va a utilizar para el proceso de picking (el lote que reservó WMS).
                                                                    wsCUWMS.InsertLotNoAsync(CurrentWkshName,
                                                                                            vRespuestaInsertLotNo,
                                                                                            PDet.Item_No,
                                                                                            PDet.Source_ID,
                                                                                            StockReservado.Lote,
                                                                                            vFechaVenceNav,
                                                                                            StockReservado.Cantidad,
                                                                                            ItemOnWorkSheet.Whse_Document_Line_No)

                                                                    If vRespuestaInsertLotNo = "Lote ingresado correctamente" Then

                                                                        vContadorLotesProcesadosCorrectamente += 1
                                                                        clsPublic.Actualizar_Progreso(lblprg, vbTab & vbTab & vRespuestaInsertLotNo)
                                                                        vRespuestaInsertLotNo = ""

                                                                    End If

                                                                    vContadorLotesProcesadosCorrectamente += 1
                                                                    clsPublic.Actualizar_Progreso(lblprg, vbTab & vbTab & vRespuestaInsertLotNo)
                                                                    vRespuestaInsertLotNo = ""


                                                                Next

                                                                vCrearPickingNav = (vContadorLotesProcesadosCorrectamente > 0) OrElse vCrearPickingNav

                                                            End If

                                                        End If

                                                    Catch ex As Exception

                                                        '#EJC20210707: es probable que el envío al que se hace referencia ya no esté disponible o que ya no tenga lineas pendientes.
                                                        If ex.Message.Contains("You cannot handle more than the outstanding") Then

                                                            vCodeUnitNavError = True

                                                            clsPublic.Actualizar_Progreso(lblprg, "Error en Code Unit - SetWarehouseDocuments " & ex.Message)

                                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                                       pBePedidoEnc.IdPedidoEnc,
                                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                                                        Else
                                                            Throw New Exception(String.Format("{2} {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message, vbNewLine))
                                                        End If

                                                    End Try

                                                End If

                                            End If

                                        Next

                                    Next

                                    If vCrearPickingNav Then

                                        Try

                                            vCrearPickingNav = False

                                            If Lineas_Detalle_Procesadas.Count > 0 Then

                                                wsCUWMS.CreatePick(CurrentWkshName, vRespuestaNoPicking)

                                                If vRespuestaNoPicking.Contains("has been created.") Then

                                                    '#CKFK 20211124 Que Erik me confirme si así está correcto el proceso
                                                    pBePedidoEnc.No_Picking_ERP = vRespuestaNoPicking.Replace("Pick activity no. ", "").Replace(" has been created.", "")

                                                    ThePickingNAV = wsSrvPickingNAV.Read(pBePedidoEnc.No_Picking_ERP)
                                                    Dim BeStockResEncontradoEnPickingNav As New clsBeStock_res()

                                                    wsSrvPickingNAV.Update(ThePickingNAV)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Picking {0} creado correctamente para el envío {1} {2}", pBePedidoEnc.No_Picking_ERP, pBePedidoEnc.Referencia, vbNewLine))

                                                    clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(pBePedidoEnc,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                                                    Importar_Envio_Almacen = True

                                                Else
                                                    Throw New Exception(String.Format("No pudo ser creado el Picking para el envío {0}", pBePedidoEnc.Referencia))
                                                End If

                                            Else
                                                clsPublic.Actualizar_Progreso(lblprg, "No hay líneas en la hoja de trabajo para generar picking")
                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay líneas en la hoja de trabajo para generar picking para el envío {0} {1}", pBePedidoEnc.Referencia, vbNewLine))
                                            End If

                                        Catch ex As Exception

                                            Dim vMsgError As String = String.Format("Error al crear el picking para el Envío: {0} {1} {2}", NAVEnvioAlm.No, ex.Message, vbNewLine)

                                            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgError,
                                                                                       NAVEnvioAlm.No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                                        End Try

                                    End If

                                End If

                            End If

                        End If

                    Catch ex As Exception

                        Dim vMsgError As String = String.Format("Error al insertar el lote para el envío : {0} y P.V. {1} y producto {2}. {4} Error: {3} {4} ",
                                                                        NAVEnvioAlm.No,
                                                                        Source_ID,
                                                                        vCodigoProducto,
                                                                        ex.Message,
                                                                        vbNewLine)

                        clsPublic.Actualizar_Progreso(lblprg, vMsgError)
                        clsPublic.Actualizar_Progreso_CR(lblprg)

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgError.Trim,
                                                                   NAVEnvioAlm.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet, CnnLog)

                    End Try

#End Region

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Evío Inactivo {0} ", NAVEnvioAlm.No, vbNewLine))
                clsPublic.Actualizar_Progreso_CR(lblprg)
            End If

            lTransInterface.Commit()

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            'Si no existe picking no debo borrar
            Try

                wsCUWMS.BorraPicking(NAVEnvioAlm.No)

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Se eliminó correctamente el envío de almacén: {0} {1}", NAVEnvioAlm.No, vbNewLine))

            Catch ex1 As Exception
                clsPublic.Actualizar_Progreso(lblprg, String.Format("No se pudo eliminar el envío de almacén: {0} {1} {2}", NAVEnvioAlm.No, ex.Message, vbNewLine))
            End Try

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       NAVEnvioAlm.No,
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

        Finally
            If lConnectionInterface.State = ConnectionState.Open Then lConnectionInterface.Close()
        End Try

        Return Importar_Envio_Almacen

    End Function

    Private Shared pClienteTiemposList As New List(Of clsBeCliente_tiempos)

    Private Function Inserta_Linea_Detalle_Pedido(ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                  ByVal PDet As clsBeI_nav_ped_traslado_det,
                                                  ByVal BeProducto As clsBeProducto,
                                                  ByVal vDiasVencimientoCliente As Integer,
                                                  ByVal BeUnidadMedida As clsBeUnidad_medida,
                                                  ByVal pIdClienteDetalle As Integer,
                                                  ByVal Conmutar_Umbas_A_Presentacion As Boolean,
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef lConnectionInterface As SqlConnection,
                                                  ByRef CnnLog As SqlConnection,
                                                  ByRef lTransInterface As SqlTransaction) As List(Of clsBeStock_res)

        Inserta_Linea_Detalle_Pedido = Nothing

        Dim pBePedidoDet As New clsBeTrans_pe_det()
        Dim vBeStockResSolicitud As New clsBeStock_res()
        Dim lReturnList As New List(Of clsBeStock_res)
        Dim lReturnListUMBas As New List(Of clsBeStock_res)

        Dim BeNavConfigEnc As New clsBeI_nav_config_enc

        Try

            If BeConfigEnc Is Nothing Then

                BeNavConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                 lConnectionInterface,
                                                                 lTransInterface)

                BeConfigEnc = BeNavConfigEnc

            Else

                If BeConfigEnc.Idnavconfigenc = -1 Then

                    BeNavConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface,
                                                                     lConnectionInterface,
                                                                     lTransInterface)

                    BeConfigEnc = BeNavConfigEnc

                End If

            End If

            pBePedidoDet = New clsBeTrans_pe_det()
            pBePedidoDet.IdPedidoDet = 0
            pBePedidoDet.IdCliente = pIdClienteDetalle
            pBePedidoDet.No_linea = PDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = PDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = BePedidoEnc.IdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto()
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(BeProducto.IdProductoBodega,
                                                                                                 lConnectionInterface,
                                                                                                 lTransInterface)
            pBePedidoDet.Producto.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = BeProducto.IdProductoBodega
            pBePedidoDet.Producto.Codigo = PDet.Item_No
            pBePedidoDet.IdPresentacion = 0
            pBePedidoDet.IdUnidadMedidaBasica = BeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = PDet.Quantity
            pBePedidoDet.Peso = 0
            pBePedidoDet.Precio = PDet.Price
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = BeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = BeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = 0
            pBePedidoDet.RoadTotal = 0
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Codigo_Producto = PDet.No
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(PDet.Description)
            pBePedidoDet.Nom_presentacion = ""
            pBePedidoDet.Nom_unid_med = BeUnidadMedida.Nombre

            vBeStockResSolicitud.IdStockRes = 0
            vBeStockResSolicitud.IdTransaccion = BePedidoEnc.IdPedidoEnc
            vBeStockResSolicitud.IdPedidoDet = pBePedidoDet.IdPedidoDet
            vBeStockResSolicitud.Indicador = "PED"
            vBeStockResSolicitud.añada = 0
            vBeStockResSolicitud.Cantidad = PDet.Quantity
            vBeStockResSolicitud.Estado = "PPC"
            vBeStockResSolicitud.User_agr = BeConfigEnc.IdUsuario
            vBeStockResSolicitud.Fec_agr = Now
            vBeStockResSolicitud.User_mod = BeConfigEnc.IdUsuario
            vBeStockResSolicitud.Fec_mod = Now
            vBeStockResSolicitud.Host = PDet.Source_ID
            vBeStockResSolicitud.IdPresentacion = 0 'De momemento.
            vBeStockResSolicitud.IdProductoEstado = 1 'Por defecto.
            vBeStockResSolicitud.IdPedido = BePedidoEnc.IdPedidoEnc
            vBeStockResSolicitud.IdPedidoDet = pBePedidoDet.IdPedidoDet
            vBeStockResSolicitud.IdProductoBodega = BeProducto.IdProductoBodega
            vBeStockResSolicitud.IdUnidadMedida = BeUnidadMedida.IdUnidadMedida
            vBeStockResSolicitud.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1
            vBeStockResSolicitud.Codigo_Producto = PDet.Item_No
            vBeStockResSolicitud.No_Pedido = PDet.Source_ID
            vBeStockResSolicitud.Serial = PDet.Line_No
            vBeStockResSolicitud.IdBodega = BeConfigEnc.Idbodega

            '#EJC20220329: Wrap the request of the location for supply.
            If Not BePedidoEnc.Cliente Is Nothing Then

                If Not BePedidoEnc.Cliente.IdUbicacionAbastecerCon = 0 Then

                    '#CKFK20220331 Cambié la condicion para saber si es correcta la configuración del cliente
                    If BePedidoEnc.No_Documento_Externo.Trim() = BePedidoEnc.Cliente.IdCliente.ToString Then
                        If clsLnBodega_ubicacion.Exists(BePedidoEnc.Cliente.IdUbicacionAbastecerCon,
                                                        BeConfigEnc.Idbodega,
                                                        lConnectionInterface,
                                                        lTransInterface) Then
                            vBeStockResSolicitud.IdUbicacionAbastecerCon = BePedidoEnc.Cliente.IdUbicacionAbastecerCon
                        Else
                            Dim vMsg1 As String = String.Format("#ERR_ON_CONFIG_20220331: El número de documento externo ({0}) tiene un cliente que no tiene definida una ubicación de abastecimiento válida ({1}).", BePedidoEnc.No_Documento_Externo, BePedidoEnc.Cliente.IdUbicacionAbastecerCon)
                            Throw New Exception(vMsg1)
                        End If
                    ElseIf BePedidoEnc.No_Documento_Externo = "" Then
                        vBeStockResSolicitud.IdUbicacionAbastecerCon = 0
                    ElseIf Not BePedidoEnc.No_Documento_Externo.Trim() = "" AndAlso BePedidoEnc.Cliente.IdUbicacionAbastecerCon = 0 Then
                        Dim vMsg1 As String = String.Format("#ERR_ON_CONFIG_20220330: El envío tiene definido un número de documento externo ({0}) pero el cliente no tiene definida la ubicación de abastecimiento.", BePedidoEnc.No_Documento_Externo)
                        Throw New Exception(vMsg1)
                    ElseIf Not BePedidoEnc.No_Documento_Externo.Trim() <> BePedidoEnc.Cliente.IdUbicacionAbastecerCon Then
                        Dim vMsg2 As String = String.Format("#ERR_ON_CONFIG_20220330A: El envío tiene defino un número de documento externo ({0}) pero el cliente tiene definido el valor ({1}) que es diferente.", BePedidoEnc.No_Documento_Externo, BePedidoEnc.Cliente.IdUbicacionAbastecerCon)
                        Throw New Exception(vMsg2)
                    End If

                End If

            End If

            vBeStockResSolicitud.IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(BeConfigEnc.Idbodega,
                                                                                                                               BeConfigEnc.IdPropietario,
                                                                                                                               lConnectionInterface,
                                                                                                                               lTransInterface)


            Dim BePresentacion As New clsBeProducto_Presentacion()

            If pBePedidoDet.Atributo_Variante_1 <> "" Then

                BePresentacion = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(pBePedidoDet.Producto.IdProducto,
                                                                                                        pBePedidoDet.Atributo_Variante_1,
                                                                                                        lConnectionInterface,
                                                                                                        lTransInterface)

                If Not BePresentacion Is Nothing Then
                    vBeStockResSolicitud.IdPresentacion = BePresentacion.IdPresentacion
                    pBePedidoDet.IdPresentacion = BePresentacion.IdPresentacion
                Else
                    vBeStockResSolicitud.IdPresentacion = -1 'No se encontró la presentación solicitada
                End If

            End If

            Try

                If PDet.Item_No = "00191650" Then
                    Debug.Print("Es el código")
                End If

                lReturnList = clsLnTrans_pe_det.Get_Stock_Reservado_Por_Linea_Interface(vDiasVencimientoCliente,
                                                                                        PDet,
                                                                                        pBePedidoDet,
                                                                                        vBeStockResSolicitud,
                                                                                        "Interface",
                                                                                        BeConfigEnc,
                                                                                        BeConfigEnc.IdPropietario,
                                                                                        Conmutar_Umbas_A_Presentacion,
                                                                                        lConnectionInterface,
                                                                                        lTransInterface)
                If Not lReturnList Is Nothing Then

                    '#EJC20220315: Holds the reserved stock from the presentation.
                    Inserta_Linea_Detalle_Pedido = lReturnList

                    '#EJC20220315: If the line has required quantity on basic units, call recursively this function
                    'Whitout the variant_code with has the presentation_code.
                    If PDet.Quantity_In_UMBas > 0 Then

                        PDet.Quantity = PDet.Quantity_In_UMBas
                        PDet.Variant_Code = ""
                        PDet.Quantity_In_UMBas = 0

                        lReturnListUMBas = Inserta_Linea_Detalle_Pedido(BePedidoEnc,
                                                                        PDet,
                                                                        BeProducto,
                                                                        vDiasVencimientoCliente,
                                                                        BeUnidadMedida,
                                                                        0,
                                                                        False,
                                                                        lblprg,
                                                                        lConnectionInterface,
                                                                        CnnLog,
                                                                        lTransInterface)

                        '#EJC20220315: If success, the wms was able to make the reservation in basic unit, so.
                        'lets add the resulted list to the main list.
                        If Not lReturnListUMBas Is Nothing Then
                            '#EJC20220315: As the function has a pointer to the list lReturnlist
                            'I will expect that the result of the function gives me back the entire list of reserved stock
                            'of both packaging, lets see...?
                            lReturnList.AddRange(lReturnListUMBas)
                        End If

                    End If

                End If

            Catch ex As Exception

                'clsLnTrans_pe_det.Eliminar(pBePedidoDet,CnnInterface,lTransInterface)

                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format(vbNewLine & "Error en Reservar_Stock_Por_Linea para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2}. ", PDet.NoEnc,
                                                                         PDet.Line_No,
                                                                         ex.Message,
                                                                         PDet.Item_No,
                                                                         PDet.Unit_of_Measure_Code,
                                                                         PDet.Variant_Code),
                                                                         PDet.No,
                                                                         BeNavEjecucionEnc.IdEjecucionEnc,
                                                                         BeConfigDet.Idnavconfigdet,
                                                                         CnnLog,
                                                                         PDet.Line_No,
                                                                         PDet.Item_No,
                                                                         PDet.Unit_of_Measure_Code,
                                                                         PDet.Variant_Code)


                clsPublic.Actualizar_Progreso(lblprg, String.Format(vbNewLine & "Error en
                                                Reservar_Stock_Por_Linea 
                                                para el pedido: {0} 
                                                línea: {1} 
                                                Código_Producto: {4}
                                                U.M.: {5}
                                                V.C.: {6}
                                                Descripción del error: {2}{3}. ",
                                                PDet.NoEnc,
                                                PDet.Line_No,
                                                ex.Message,
                                                vbNewLine,
                                                PDet.Item_No,
                                                PDet.Unit_of_Measure_Code,
                                                PDet.Variant_Code))

            End Try

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK20221012 Renombré la función de arriba que es la que originalmente se utilizaba y creé esta que es una copia de la que esta en la clase clsLnI_nav_ped_traslado_enc
    'y que es la función que llama a clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface que a su vez llama a clsLnStock_res.Reserva_Stock_From_MI3 
    'solo le agregué el stockres para que lo devuelva
    Private Shared Function Get_Process_Result_Actual_EnvioAlm(ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                               ByRef lConectionInterface As SqlConnection,
                                                               ByRef lTransactionInterface As SqlTransaction) As String

        Try

            Using Cmd As New SqlCommand()
                Cmd.Connection = lConectionInterface
                Cmd.Transaction = lTransactionInterface
                Cmd.CommandText = "SELECT TOP 1 Process_Result FROM i_nav_ped_traslado_det WHERE NoEnc = @NoEnc AND Line_No = @Line_No AND Item_No = @Item_No"
                Cmd.Parameters.AddWithValue("@NoEnc", pBeTrasladoDet.NoEnc)
                Cmd.Parameters.AddWithValue("@Line_No", pBeTrasladoDet.Line_No)
                Cmd.Parameters.AddWithValue("@Item_No", pBeTrasladoDet.Item_No)

                Dim vResultado As Object = Cmd.ExecuteScalar()

                If vResultado Is Nothing OrElse IsDBNull(vResultado) Then
                    Return ""
                End If

                Return vResultado.ToString.Trim
            End Using

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(String.Format("{0} Get_Process_Result_Actual_EnvioAlm {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            Return ""
        End Try

    End Function

    Private Shared Sub Actualizar_Process_Result_EnvioAlm(ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                          ByRef lConectionInterface As SqlConnection,
                                                          ByRef lTransactionInterface As SqlTransaction)

        Using Cmd As New SqlCommand()
            Cmd.Connection = lConectionInterface
            Cmd.Transaction = lTransactionInterface
            Cmd.CommandText = "UPDATE i_nav_ped_traslado_det SET Process_Result = @Process_Result WHERE NoEnc = @NoEnc AND Line_No = @Line_No AND Item_No = @Item_No"
            Cmd.Parameters.AddWithValue("@Process_Result", pBeTrasladoDet.Process_Result)
            Cmd.Parameters.AddWithValue("@NoEnc", pBeTrasladoDet.NoEnc)
            Cmd.Parameters.AddWithValue("@Line_No", pBeTrasladoDet.Line_No)
            Cmd.Parameters.AddWithValue("@Item_No", pBeTrasladoDet.Item_No)

            Dim vFilasAfectadas As Integer = Cmd.ExecuteNonQuery()

            If vFilasAfectadas = 0 Then
                clsLnLog_error_wms.Agregar_Error(String.Format("{0} Actualizar_Process_Result_EnvioAlm sin filas. Documento: {1} Linea: {2} Item: {3} Resultado: {4}",
                                                               MethodBase.GetCurrentMethod.Name,
                                                               pBeTrasladoDet.NoEnc,
                                                               pBeTrasladoDet.Line_No,
                                                               pBeTrasladoDet.Item_No,
                                                               pBeTrasladoDet.Process_Result))
            End If
        End Using

    End Sub

    Private Shared Function Limpiar_Process_Result_Generico_Reserva(ByVal pProcessResult As String) As String

        Dim vProcessResult As String = ""

        If Not pProcessResult Is Nothing Then
            vProcessResult = pProcessResult.Trim
        End If

        If vProcessResult = "" Then
            Return ""
        End If

        If String.Equals(vProcessResult, "Ok", StringComparison.OrdinalIgnoreCase) Then
            Return ""
        End If

        If String.Equals(vProcessResult, "No se pudo completar la reserva.", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(vProcessResult, "No se pudo completar la reserva", StringComparison.OrdinalIgnoreCase) Then
            Return ""
        End If

        Return vProcessResult

    End Function

    Private Shared Function Formatear_Process_Result_No_Reserva_EnvioAlm(ByVal pProcessResultActual As String,
                                                                         ByVal pMotivoDefecto As String) As String

        Dim vMotivo As String = Limpiar_Process_Result_Generico_Reserva(pProcessResultActual)

        If vMotivo = "" Then
            vMotivo = Limpiar_Process_Result_Generico_Reserva(pMotivoDefecto)
        End If

        If vMotivo = "" Then
            vMotivo = "TIPO_NO_RESERVA=RESERVA_NO_COMPLETADA | No hay existencia aplicable valida para la solicitud. Revise stock disponible, ubicacion de picking/almacenamiento, vencimiento, presentacion y reservas vigentes."
        End If

        If vMotivo.StartsWith("No se pudo completar la reserva", StringComparison.OrdinalIgnoreCase) Then
            Return vMotivo
        End If

        Return "No se pudo completar la reserva: " & vMotivo

    End Function

    Private Shared Function Inserta_Linea_Detalle_Pedido(ByVal pIdPedidoEnc As Integer,
                                                         ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                         ByVal pBePoducto As clsBeProducto,
                                                         ByVal pDiasVencimientoCliente As Integer,
                                                         ByVal pBeUnidadMedida As clsBeUnidad_medida,
                                                         ByVal pBePresentacion As clsBeProducto_Presentacion,
                                                         ByVal pBeCliente As clsBeCliente,
                                                         ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                         ByVal pIdBodegaOrigen As Integer,
                                                         ByVal pIdPropietarioBodega As Integer,
                                                         ByVal pIdejecucionenc As Integer,
                                                         ByRef plblprg As RichTextBox,
                                                         ByRef pListStockResOUT As List(Of clsBeStock_res),
                                                         ByRef lConectionInterface As SqlConnection,
                                                         ByRef CnnLog As SqlConnection,
                                                         ByRef lTransactionInterface As SqlTransaction) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim IdxPresentacion As Integer = -1

        Try

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = 0
            pBePedidoDet.No_linea = pBeTrasladoDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = pBeTrasladoDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = pIdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                 lConectionInterface,
                                                                                                 lTransactionInterface)
            pBePedidoDet.Producto.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.Codigo_Producto = pBeTrasladoDet.Item_No
            pBePedidoDet.Producto.Codigo = pBeTrasladoDet.Item_No
            '#EJC20220622:Quitar caractéres no permitidos.
            pBePedidoDet.Producto.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.IdUnidadMedidaBasica = pBeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity
            pBePedidoDet.Peso = 0
            pBePedidoDet.Precio = pBeTrasladoDet.Price
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = pBeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = pBeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = pBeTrasladoDet.Price
            pBePedidoDet.RoadTotal = Math.Round(pBeTrasladoDet.Price * pBeTrasladoDet.Quantity, 6)
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0

            If Not pBeTrasladoDet.Variant_Code = "" Then
                If Not pBePresentacion Is Nothing Then
                    If pBePresentacion.IdPresentacion <> 0 Then
                        pBePedidoDet.Nom_presentacion = pBePresentacion.Nombre
                        pBePedidoDet.IdPresentacion = pBePresentacion.IdPresentacion
                        pBePedidoDet.Factor = pBePresentacion.Factor
                    Else
                        pBePedidoDet.Nom_presentacion = ""
                    End If
                End If
            Else
                pBePedidoDet.Nom_presentacion = ""
            End If

            pBePedidoDet.Nom_unid_med = pBeTrasladoDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"

            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = pBeTrasladoDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBeStockRes.User_agr = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = pBeTrasladoDet.Source_ID
            pBeStockRes.Serial = pBeTrasladoDet.Line_No

            Dim BeProductoEstadoList As New List(Of clsBeProducto_estado)

            Dim vIdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pIdBodegaOrigen,
                                                                                                                      pIdPropietarioBodega,
                                                                                                                      lConectionInterface,
                                                                                                                      lTransactionInterface)

            Try

                '#EJC202220620:Buscar el estado de producto de la interface.
                Dim vIdEstadoProductoInterface As Integer = pBeConfigEnc.IdProductoEstado

                BeProductoEstadoList = clsLnProducto_estado.Existe_IdEstado_By_IdPropietario(vIdPropietario,
                                                                                             vIdEstadoProductoInterface,
                                                                                             lConectionInterface,
                                                                                             lTransactionInterface)

                If Not BeProductoEstadoList Is Nothing Then

                    If Not BeProductoEstadoList.FirstOrDefault() Is Nothing Then
                        pBeStockRes.IdProductoEstado = BeProductoEstadoList.FirstOrDefault.IdEstado()
                    Else
                        Throw New Exception("ERR_202205121200A: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                    End If

                Else
                    Throw New Exception("ERR_202205121200B: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                End If

            Catch ex As Exception
                Throw New Exception("ERES_TU: " & ex.Message)
            End Try

            pBeStockRes.IdPedido = pIdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = pBePoducto.IdProductoBodega
            '#EJC20220512: 'clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pIdBodega,pIdPropietarioBodega,lConectionInterface,lTransactionInterface)
            pBeStockRes.IdPropietarioBodega = pIdPropietarioBodega
            pBeStockRes.IdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(pBePedidoDet.Producto.Codigo,
                                                                                      lConectionInterface,
                                                                                      lTransactionInterface)
            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            '#EJC20190314: Asignar control ultimo lote a objeto de reserva.
            If Not pBeCliente Is Nothing Then
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
            Else
                pBeStockRes.Control_Ultimo_Lote = False
            End If

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.IdPresentacion <> 0 Then

                If Not pBePedidoDet.Atributo_Variante_1 Is Nothing Then

                    If Not pBePedidoDet.Atributo_Variante_1 = "" Then

                        BePresentacion = New clsBeProducto_Presentacion
                        BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(pBePedidoDet.Producto.IdProducto,
                                                                                                  pBePedidoDet.Atributo_Variante_1,
                                                                                                  lConectionInterface,
                                                                                                  lTransactionInterface)

                        If Not BePresentacion Is Nothing Then
                            pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                        Else

                            '#EJC202210181952: Buscar por código de barra (BYB)
                            BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_IdProducto_And_CodigoBarra(pBePedidoDet.Producto.IdProducto,
                                                                                                                          pBePedidoDet.Codigo_Producto,
                                                                                                                          lConectionInterface,
                                                                                                                          lTransactionInterface)

                            If Not BePresentacion Is Nothing Then
                                pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                            Else
                                pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                            End If

                        End If

                    Else
                        pBeStockRes.IdPresentacion = 0 'La solicitud es en UMBAS.
                    End If


                Else
                    pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                End If

            End If

            If pBeStockRes.Control_Ultimo_Lote Then
                '#EJC20190314: Capturar último lote despachado para evitar enviar el mismo.
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pBePedidoDet.Producto.IdProducto)
            End If

            If Not pBeCliente Is Nothing Then
                '#EJC20220712_0853:Asignar la ubicación con la que se va a abastecer el pedido de un determinado cliente.
                'MI3: (Quedaría pendiente validar si la ubicación que trae es válida, pero eso que lo haga otro... que esté viendo mi pantalla.
                If Val(pBeCliente.IdUbicacionAbastecerCon) <> 0 Then
                    pBeStockRes.IdUbicacionAbastecerCon = pBeCliente.IdUbicacionAbastecerCon
                Else
                    pBeStockRes.IdUbicacionAbastecerCon = 0
                End If
            Else
                pBeStockRes.IdUbicacionAbastecerCon = 0
            End If


            Try
                '#CKFK20221012 Agregué la función que devuelve el stock reservado
                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(pDiasVencimientoCliente,
                                                                        pBeTrasladoDet,
                                                                        pBePedidoDet,
                                                                        pBeStockRes,
                                                                        pBeTrasladoDet.Source_ID,
                                                                        pBeConfigEnc,
                                                                        pIdPropietarioBodega,
                                                                        pListStockResOUT,
                                                                        plblprg,
                                                                        lConectionInterface,
                                                                        lTransactionInterface) Then
                    Inserta_Linea_Detalle_Pedido = True
                    pBeTrasladoDet.Process_Result = "Ok"
                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)
                Else

                    Dim vProcessResultActual As String = Get_Process_Result_Actual_EnvioAlm(pBeTrasladoDet,
                                                                                           lConectionInterface,
                                                                                           lTransactionInterface)
                    Dim vMotivoDefecto As String = ""

                    If pBeStockRes.IdUbicacionAbastecerCon > 0 Then

                        Dim vNombreUbicacion As String = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(pBeStockRes.IdUbicacionAbastecerCon, pIdBodegaOrigen)

                        vMotivoDefecto = "TIPO_NO_RESERVA=UBICACION_ABASTECER_SIN_STOCK | MSG_231214: No se pudo completar la reserva, valide disponibilidad de inventario en ubicacion (IdUbicacionAbastecerCon): " & vNombreUbicacion

                    Else

                        vMotivoDefecto = "TIPO_NO_RESERVA=RESERVA_NO_COMPLETADA | No hay existencia aplicable valida para la solicitud despues de evaluar stock disponible, ubicacion de picking/almacenamiento, vencimiento, presentacion y reservas vigentes."

                    End If

                    pBeTrasladoDet.Process_Result = Formatear_Process_Result_No_Reserva_EnvioAlm(vProcessResultActual,
                                                                                                  vMotivoDefecto)
                    Actualizar_Process_Result_EnvioAlm(pBeTrasladoDet,
                                                       lConectionInterface,
                                                       lTransactionInterface)

                End If

            Catch ex As Exception

                Dim vMensajeEx As String = String.Format(vbNewLine & "Error en Reservar_Stock_Por_Linea para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", pBeTrasladoDet.NoEnc,
                                                        pBeTrasladoDet.Line_No,
                                                        ex.Message,
                                                        pBeTrasladoDet.Item_No,
                                                        pBeTrasladoDet.Unit_of_Measure_Code,
                                                        pBeTrasladoDet.Variant_Code,
                                                        pBeTrasladoDet.Quantity)

                pBeTrasladoDet.Process_Result = vMensajeEx

                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                      lConectionInterface,
                                                                      lTransactionInterface)

                clsPublic.Actualizar_Progreso(plblprg, vMensajeEx)

                If pBeConfigEnc.Rechazar_pedido_incompleto Then
                    Throw New Exception(vMensajeEx)
                End If

            End Try

        Catch ex As Exception
            Dim st As New StackTrace(True)
            st = New StackTrace(ex, True)
            Dim vMsgError As String = String.Format("ERR_RELAY_202303011324B: {0} {1} - Línea {2}", MethodBase.GetCurrentMethod.Name(), ex.Message, st.GetFrame(0).GetFileLineNumber().ToString)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Cliente_Single(ByVal CodigoClienteNAV As String,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction,
                                                   ByRef lConnectionLog As SqlConnection,
                                                   ByRef lblprg As RichTextBox,
                                                   ByRef prg As Windows.Forms.ProgressBar) As clsBeCliente_bodega

        Insertar_Cliente_Single = Nothing

        Try
            Dim lFamilias As New List(Of clsBeProducto_familia)
            Dim lClasificacion As New List(Of clsBeProducto_clasificacion)
            Dim navCliente As New Lista_clientes()

            Dim wsClienteService As New Lista_clientes_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

            wsClienteService.Url = My.MySettings.Default.NavSync_WSListaClientes_Lista_clientes_Service
            navCliente = wsClienteService.Read(CodigoClienteNAV)

            Dim BeCliente As New clsBeCliente()
            Dim BeClienteBodega As New clsBeCliente_bodega()

            If Not navCliente Is Nothing Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Cliente: {0} ", navCliente.No, vbNewLine))

                BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.IdTipoCliente = 1
                BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                BeCliente.Codigo = CodigoClienteNAV
                BeCliente.Nombre_comercial = navCliente.Name
                BeCliente.Nombre_contacto = IIf(navCliente.Contact Is Nothing, navCliente.Name, navCliente.Contact)
                BeCliente.Direccion = navCliente.Address & " " & navCliente.City
                BeCliente.Sistema = True
                BeCliente.Activo = True
                BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                BeCliente.Nit = IIf(navCliente.VAT_Registration_No Is Nothing, CodigoClienteNAV, navCliente.VAT_Registration_No)
                BeCliente.IdTipoCliente = 1
                BeCliente.Es_bodega_recepcion = False
                BeCliente.Es_Bodega_Traslado = False

                Try

                    clsLnCliente.Insertar(BeCliente,
                                          lConnection,
                                          lTransaction)

                    BeClienteBodega = New clsBeCliente_bodega()
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now
                    BeClienteBodega.Cliente = BeCliente

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega,
                                                                lConnection,
                                                                lTransaction)


                    '#CKFK20240424: Insertar días por defecto para clientes.
                    If BeConfigEnc.Dias_Vida_Defecto_Perecederos > 0 Then

                        lFamilias = clsLnProducto_familia.Get_All_Filtro(True,
                                                                         BeConfigEnc.IdPropietario,
                                                                         lConnection,
                                                                         lTransaction)

                        lClasificacion = clsLnProducto_clasificacion.Get_All_Filtro(True,
                                                                                    BeConfigEnc.IdPropietario,
                                                                                    lConnection,
                                                                                    lTransaction)

                        If Not lFamilias Is Nothing AndAlso Not lClasificacion Is Nothing Then

                            Dim BeTiempoCliente As New clsBeCliente_tiempos

                            For Each F In lFamilias

                                For Each C In lClasificacion

                                    BeTiempoCliente = New clsBeCliente_tiempos()
                                    BeTiempoCliente.IdTiempoCliente = clsLnCliente_tiempos.MaxID(lConnection, lTransaction) + 1
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
                                    clsLnCliente_tiempos.Insertar(BeTiempoCliente, lConnection, lTransaction)

                                Next

                            Next

                        End If

                    End If

                    Return BeClienteBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeCliente.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar proveedor: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                End Try

            End If

        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Cliente_Detalle(ByVal PDet As clsBeI_nav_ped_traslado_det,
                                               ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                               ByVal BeProducto As clsBeProducto,
                                               ByVal pBePedidoEnc As clsBeTrans_pe_enc,
                                               ByVal lConnectionInterface As SqlConnection,
                                               ByVal lTransInterface As SqlTransaction,
                                               ByVal CnnLog As SqlConnection,
                                               ByRef lblprg As RichTextBox,
                                               ByRef prg As Windows.Forms.ProgressBar,
                                               ByRef pClienteTiemposList As List(Of clsBeCliente_tiempos),
                                               ByRef BeClienteBodegaDetalle As clsBeCliente_bodega,
                                               ByRef vClienteTiempo As clsBeCliente_tiempos) As clsBeCliente

        Dim BeClienteDetalle As clsBeCliente = Nothing

        Try
            ' Verifica el código de transferencia
            If Not String.IsNullOrEmpty(PDet.Transfer_to_CodeField) AndAlso (PDet.Transfer_to_CodeField <> NAVEnvioAlm.Transfer_to_Code) Then
                ' Busca el cliente en la lista compartida primero
                Dim vIndiceListaCliente As Integer = lClientes.FindIndex(Function(x) x.Codigo = PDet.Transfer_to_CodeField)

                If vIndiceListaCliente > -1 Then
                    ' Si lo encuentra, clona el objeto para evitar modificaciones por referencia
                    BeClienteDetalle = lClientes(vIndiceListaCliente).Clone()
                Else
                    ' Si no está en la lista, busca en la base de datos
                    BeClienteDetalle = clsLnCliente.Get_Single_By_Codigo(PDet.Transfer_to_CodeField,
                                                                         lConnectionInterface,
                                                                         lTransInterface)

                    ' Si no encuentra el cliente, intenta insertarlo
                    If BeClienteDetalle Is Nothing Then
                        BeClienteBodegaDetalle = Insertar_Cliente_Single(PDet.Transfer_to_CodeField,
                                                                        lConnectionInterface,
                                                                        lTransInterface,
                                                                        CnnLog,
                                                                        lblprg,
                                                                        prg)
                        If Not BeClienteBodegaDetalle Is Nothing Then
                            BeClienteDetalle = BeClienteBodegaDetalle.Cliente
                            lClientes.Add(BeClienteDetalle) ' Agrega el cliente a la lista en memoria

                        End If
                    Else
                        lClientes.Add(BeClienteDetalle) ' Agrega el cliente encontrado a la lista en memoria
                    End If
                End If

                ' Actualiza la lista de tiempos del cliente si es necesario
                If BeClienteDetalle IsNot Nothing AndAlso pClienteTiemposList IsNot Nothing AndAlso pClienteTiemposList.Count > 0 Then
                    Dim vClienteTiemposList As List(Of clsBeCliente_tiempos) = pClienteTiemposList.FindAll(Function(x) x.IdCliente = BeClienteDetalle.IdCliente)
                    If vClienteTiemposList Is Nothing OrElse vClienteTiemposList.Count = 0 Then
                        vClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(BeClienteDetalle.IdCliente,
                                                                                               lConnectionInterface,
                                                                                               lTransInterface)
                        If vClienteTiemposList IsNot Nothing AndAlso vClienteTiemposList.Count > 0 Then
                            pClienteTiemposList.AddRange(vClienteTiemposList)
                        End If
                    End If
                End If

            Else
                '#EJC20220314: El cliente en el detalle es el mismo que en el encabezado.
                BeClienteDetalle = Nothing
            End If

            ' Verifica el código de transferencia
            If Not String.IsNullOrEmpty(PDet.Transfer_to_CodeField) AndAlso (PDet.Transfer_to_CodeField <> NAVEnvioAlm.Transfer_to_Code) Then
                ' [Código existente aquí para obtener BeClienteDetalle y actualizar BeClienteBodegaDetalle]

                ' Actualiza la lista de tiempos del cliente si es necesario
                If BeClienteDetalle IsNot Nothing AndAlso pClienteTiemposList IsNot Nothing AndAlso pClienteTiemposList.Count > 0 Then
                    ' [Código existente aquí para actualizar vClienteTiemposList]

                    ' Asigna vClienteTiempo basado en la existencia de BeClienteDetalle
                    vClienteTiempo = pClienteTiemposList.Find(Function(x) x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                                          AndAlso x.IdFamilia = BeProducto.Familia.IdFamilia _
                                                          AndAlso x.IdCliente = BeClienteDetalle.IdCliente)
                End If
            Else
                '#EJC20220314: El cliente en el detalle es el mismo que en el encabezado.
                BeClienteDetalle = Nothing
                ' Asigna vClienteTiempo basado en el cliente del pedido
                vClienteTiempo = pClienteTiemposList.Find(Function(x) x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                                      AndAlso x.IdFamilia = BeProducto.Familia.IdFamilia _
                                                      AndAlso x.IdCliente = pBePedidoEnc.IdCliente)
            End If

            Return BeClienteDetalle

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function Implosion_Automatica(ByRef PDet As clsBeI_nav_ped_traslado_det,
                                         ByVal BeProducto As clsBeProducto,
                                         ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                         ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                         ByVal lConnectionInterface As SqlConnection,
                                         ByVal lTransInterface As SqlTransaction,
                                         ByVal CnnLog As SqlConnection) As Boolean

        ' Inicializar la variable de resultado
        Dim vExplosionAutomatica As Boolean = False

        ' Comprobar si la unidad de medida coincide
        If BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code Then

            ' Comprobar si la implosión automática está activa
            If BeConfigEnc.Explosion_Automatica Then

                ' Obtener las presentaciones del producto
                Dim lProductoPresentacion As List(Of clsBeProducto_Presentacion)
                lProductoPresentacion = clsLnProducto_presentacion.Get_All_By_IdProducto_By_IdBodega(BeProducto.IdProducto,
                                                                                                     True,
                                                                                                     BeConfigEnc.Idbodega,
                                                                                                     lConnectionInterface,
                                                                                                     lTransInterface)

                ' Comprobar si se obtuvo alguna presentación
                If Not lProductoPresentacion Is Nothing Then

                    ' Trabajar con la primera presentación si solo hay una
                    If lProductoPresentacion.Count = 1 Then

                        Dim BeProductoPresentacion As clsBeProducto_Presentacion = lProductoPresentacion(0)

                        ' Comprobar si no es nulo
                        If Not BeProductoPresentacion Is Nothing Then

                            ' Realizar cálculos si la cantidad solicitada es mayor que el factor de presentación
                            If PDet.Quantity >= BeProductoPresentacion.Factor Then

                                ' Cálculos de implosión automática
                                Dim vDeltaFactorPresentacion As Double = Math.Round(PDet.Quantity / BeProductoPresentacion.Factor, 6)
                                Dim vCantidadEnteraPresentacion As Integer = Math.Truncate(vDeltaFactorPresentacion)
                                Dim vCantidadSobranteUnidades As Integer = Math.Round(Math.Abs((vCantidadEnteraPresentacion - vDeltaFactorPresentacion) * BeProductoPresentacion.Factor))

                                ' Determinar cantidades y variantes
                                If vCantidadSobranteUnidades = 0 Then
                                    PDet.Quantity = vCantidadEnteraPresentacion
                                    PDet.Quantity_In_UMBas = 0
                                    PDet.Variant_Code = BeProductoPresentacion.Nombre
                                Else
                                    PDet.Quantity = vDeltaFactorPresentacion
                                    PDet.Quantity_In_UMBas = vCantidadSobranteUnidades
                                    PDet.Variant_Code = BeProductoPresentacion.Nombre
                                End If

                                vExplosionAutomatica = True

                            Else
                                ' Si la cantidad es menor que una caja, no se realiza la implosión
                                vExplosionAutomatica = False
                            End If

                        End If

                    Else
                        ' Error si hay más de una presentación
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error_20220224_0121: La implosión automática está activa, pero se encontró más de una presentación para el producto: {0}, el sistema no puede determinar el factor de implosión.",
                                                                             BeProducto.Codigo),
                                                                             NAVEnvioAlm.No,
                                                                             BeNavEjecucionEnc.IdEjecucionEnc,
                                                                             BeConfigDet.Idnavconfigdet,
                                                                             CnnLog)
                    End If

                End If

            End If

        End If

        Return vExplosionAutomatica

    End Function

    Public Shared Function GetIpAndPortFromUrl(url As String) As String
        Try
            Dim uri As New Uri(url)
            Return $"{uri.Host}:{uri.Port}"
        Catch ex As UriFormatException
            ' Manejo del error en caso de formato incorrecto de la URL
            Return url
        End Try
    End Function

    Private TotalLotesEnviados As Integer = 0
    Private LotesInsertadosCorrectamente As Integer = 0
    Private LotesInsertadosConError As Integer = 0
    Private LockObject As New Object()

    Public Function Importar_Envio_Almacen2(ByVal NAVEnvioAlm As clsBeI_nav_ped_traslado_enc,
                                           ByRef lblprg As RichTextBox,
                                           ByRef prg As Windows.Forms.ProgressBar,
                                           ByRef CnnLog As SqlConnection) As Boolean

        Importar_Envio_Almacen2 = False

#Region "Variables"

        Dim lConnectionInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing
        Dim Source_ID As String = ""
        Dim vCodigoProducto As String = ""
        Dim vCantidad As Double = 0
        Dim vNoLinea As Integer = 0
        Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
        Dim TrasladoExistente As clsBeTrans_pe_enc = Nothing
        Dim Lineas_Detalle_Procesadas As New List(Of clsBeI_nav_ped_traslado_det)
        Dim BeCliente As New clsBeCliente()
        Dim BeClienteDetalle As New clsBeCliente()
        Dim vContadorLineasDet As Integer = 0
        Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeProducto As New clsBeProducto()
        Dim BeStockResPedido As New clsBeStock_res()
        Dim pBePedidoDet As New clsBeTrans_pe_det()
        Dim vClienteTiempo As New clsBeCliente_tiempos()
        Dim vDiasVencimientoCliente As Integer = 0
        Dim BeUnidadMedida As New clsBeUnidad_medida
        Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0
        Dim vIdPropietarioBodega As Integer = 0
        Dim vCodigoBodega As String = ""
        Dim vCodeUnitNavError As Boolean = False
        Dim vInsertoLineaDetalle As Boolean = False
        'Debería indicar si se creo correctamente y cuál es el número de referencia.                
        Dim vRespuestaSetWarehouseDocuments As String = ""
        Dim vRespuestaInsertLotNo As String = ""
        Dim vRespuestaNoPicking As String = ""
        Dim CurrentWkshName As String = "GENERICO" '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim CurrentSortingMethod As Integer = 1 '#EJC20210614: Según Ricardo, este valor se envía fijo.
        Dim TheWorkSheetNav() As Crea_picking
        Dim vFechaVenceNav As String = ""
        Dim vCrearPickingNav As Boolean = False
        Dim lDocumentosHojaDeTrabajo As New List(Of String)
        Dim cantLineasHojaTrabajo As Integer = 0
        Dim lBeStockResPedido As New List(Of clsBeStock_res)
        Dim lBeStockResPedidoFilter As New List(Of clsBeStock_res)
        Dim ThePickingNAV As New Picking()
        Dim lProductoPresentacion As New List(Of clsBeProducto_Presentacion)
        Dim BeProductoPresentacion As New clsBeProducto_Presentacion()
        Dim BeProductoPresentacionDefecto As New clsBeProducto_Presentacion()
        Dim vDeltaFactorPresentacion As Double = 0
        Dim vCantidadEnteraPresentacion As Double = 0
        Dim vCantidadSobranteUnidades As Double = 0
        Dim vExplosionAutomatica As Boolean = False
        Dim vClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeClienteBodegaDetalle As New clsBeCliente_bodega
        Dim Presentaciones As New Presentaciones
        Dim srvWorkSheet As New Crea_picking_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

        srvWorkSheet.Url = My.Settings.NavSync_WSCreaPicking_Crea_picking_Service

        '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
        Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                .Credentials = CredencialesConexion
                                               }

        wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

        '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
        Dim wsSrvPickingNAV As New Picking_Service() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                           .Credentials = CredencialesConexion
                                                          }

        wsSrvPickingNAV.Url = My.MySettings.Default.NavSync_WSPicking_Picking_Service
        Dim BePresentacion As New clsBeProducto_Presentacion()
        Dim vNombrePresentacion As String = ""
        Dim vEnviarEnPresentacion As Boolean = False
        Dim vCantidadAEnviar As Double = 0
        Dim lCreaPicking As New List(Of Crea_picking)

#End Region

        Try

            lConnectionInterface.Open() : lTransInterface = lConnectionInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            Muestra_Progreso_Inicio(lblprg, NAVEnvioAlm)

            VContadorBitacoraTomims = 0

            Configuracion_Interface_Correcta(lConnectionInterface, lTransInterface)

            vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(BeConfigEnc.IdPropietario,
                                                                                                                 BeConfigEnc.Idbodega,
                                                                                                                 lConnectionInterface,
                                                                                                                 lTransInterface)

            vCodigoBodega = clsLnBodega.Get_Codigo_By_IdBodega(BeConfigEnc.Idbodega,
                                                               lConnectionInterface,
                                                               lTransInterface)

            lPresentaciones.Clear()

            If NAVEnvioAlm.Status > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Envío No.: {0} ", NAVEnvioAlm.No, vbNewLine), True)

                If NAVEnvioAlm.Lineas_Detalle.Count > 0 Then

                    pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = NAVEnvioAlm.No,
                                                                 .IdTipoPedido = NAVEnvioAlm.Document_Type}

                    Lineas_Detalle_Procesadas = New List(Of clsBeI_nav_ped_traslado_det)

                    TrasladoExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                   lConnectionInterface,
                                                                                   lTransInterface)

                    vContadorLineasDet = 0

                    Valida_Transferencia_Interna(NAVEnvioAlm,
                                                 lConnectionInterface,
                                                 lTransInterface)

                    BeCliente = Get_Cliente(NAVEnvioAlm.Transfer_to_Code,
                                           lConnectionInterface,
                                           lTransInterface)

                    If Not TrasladoExistente Is Nothing Then
                        pBePedidoEnc.Activo = True
                    Else

                        pBePedidoEnc.Fecha_Pedido = NAVEnvioAlm.Posting_Date
                        pBePedidoEnc.Referencia = NAVEnvioAlm.No
                        pBePedidoEnc.User_agr = pIdUsuario
                        pBePedidoEnc.User_mod = pIdUsuario

                        If BeConfigEnc Is Nothing Then
                            BeConfigEnc = New clsBeI_nav_config_enc
                            BeConfigEnc.Idbodega = 1
                        End If

                        Crear_Pedido_Enc(pBePedidoEnc,
                                         NAVEnvioAlm,
                                         vIdPropietarioBodega,
                                         BeCliente,
                                         lConnectionInterface,
                                         lTransInterface)

                        vContador_Lineas_Detalle_Pedido_Insertadas = 0

#Region "Inserta Detalle del pedido"

                        pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                        For Each PDet In NAVEnvioAlm.Lineas_Detalle.OrderBy(Function(x) x.Line_No)

                            BeProductoPresentacion = Nothing
                            vDiasVencimientoCliente = 0
                            If Not lBeStockResPedido Is Nothing Then lBeStockResPedido.Clear()

                            BeProducto = Get_Producto(PDet.Item_No,
                                                      lConnectionInterface,
                                                      lTransInterface)

                            Presentaciones = New Presentaciones()
                            Presentaciones = Get_Presentacion(BeProducto,
                                                              PDet,
                                                              lConnectionInterface,
                                                              lTransInterface)

                            BeProductoPresentacion = Presentaciones.Pedido
                            BeProductoPresentacionDefecto = Presentaciones.Defecto

                            If BeProductoPresentacion Is Nothing Then

                                BeUnidadMedida = clsLnUnidad_medida.Get_Unidad_Medida_By_Codigo(PDet.Unit_of_Measure_Code,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                                If BeUnidadMedida Is Nothing Then
                                    Throw New Exception(String.Format("{0} No existe la U.M. {1} en el maestro de WMS. ", MethodBase.GetCurrentMethod.Name(), PDet.Unit_of_Measure_Code))
                                End If

                            Else
                                '#CKFK20240109 Le asigné al objeto BeUnidadMedida el del producto
                                BeUnidadMedida = BeProducto.UnidadMedida
                            End If

                            vDiasVencimientoCliente = 0

                            BeClienteDetalle = Get_Cliente_Detalle(PDet,
                                                                   NAVEnvioAlm,
                                                                   BeProducto,
                                                                   pBePedidoEnc,
                                                                   lConnectionInterface,
                                                                   lTransInterface,
                                                                   CnnLog,
                                                                   lblprg,
                                                                   prg,
                                                                   pClienteTiemposList,
                                                                   BeClienteBodegaDetalle,
                                                                   vClienteTiempo)

                            If Not vClienteTiempo Is Nothing Then
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                            Else
                                vDiasVencimientoCliente = 0
                            End If

                            If Not BeClienteDetalle Is Nothing Then
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Cliente: {0} - {1}", BeClienteDetalle.Codigo, BeClienteDetalle.Nombre_comercial))
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Tiempo vida (Días):{1}", vbTab, vDiasVencimientoCliente))
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, "Tiempos de cliente: 0 (no definidos).")
                            End If

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Source_ID:{1}", vbTab, PDet.Source_ID))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Line_No:{1}", vbTab, PDet.Line_No))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Código:{1}", vbTab, BeProducto.Codigo))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Nombre:{1}", vbTab, BeProducto.Nombre))
                            clsPublic.Actualizar_Progreso(lblprg, String.Format("{0}  Solicitado:{1}-{2}", vbTab, PDet.Quantity, PDet.Unit_of_Measure_Code))

                            Dim vCodigosDebug As String() = {"00174459", "00193250", "00170440", "00194250"}

                            If vCodigosDebug.Contains(BeProducto.Codigo) Then
                                Debug.WriteLine("espere aquí por favor, señor compilador.")
                            End If

                            Application.DoEvents()

                            vInsertoLineaDetalle = False

                            '#EJC20220224_0123AM: El producto viene en UMBAS.
                            If BeProducto.UnidadMedida.Codigo = PDet.Unit_of_Measure_Code Then 'And Not PDet.Unit_of_Measure_Code.StartsWith("CJ")

                                '#EJC20220224_0124: Si la implosión automática está activa en la configuración de la interface.
                                If BeConfigEnc.Explosion_Automatica Then

                                    '#EJC20220224_0125: Buscar la presentación por "defecto", buscar la primera que deberían ser las cajas.
                                    lProductoPresentacion = clsLnProducto_presentacion.Get_All_By_IdProducto_By_IdBodega(BeProducto.IdProducto,
                                                                                                                         True,
                                                                                                                         BeConfigEnc.Idbodega,
                                                                                                                         lConnectionInterface,
                                                                                                                         lTransInterface)

                                    If Not lProductoPresentacion Is Nothing Then

                                        '#EJC20220224: Trabajar con la primera presentación, obtener el factor y determinar si debe 
                                        'ocurrir o no la implosión.
                                        If lProductoPresentacion.Count = 1 Then

                                            BeProductoPresentacion = lProductoPresentacion(0)

                                            If Not BeProductoPresentacion Is Nothing Then

                                                '#EJC20220224_0126: Si la cantidad solicitada es mayor que el factor por presentación
                                                'es decir: la cantidad excede las unidades por caja...
                                                If PDet.Quantity >= BeProductoPresentacion.Factor Then

                                                    vDeltaFactorPresentacion = Math.Round(PDet.Quantity / BeProductoPresentacion.Factor, 6)
                                                    vCantidadEnteraPresentacion = Math.Truncate(vDeltaFactorPresentacion)
                                                    vCantidadSobranteUnidades = Math.Round(Math.Abs((vCantidadEnteraPresentacion - vDeltaFactorPresentacion) * BeProductoPresentacion.Factor))

                                                    Dim vFactorDeRelacionUnidades As Double = 0

                                                    If vCantidadSobranteUnidades = 0 Then
                                                        PDet.Quantity = vCantidadEnteraPresentacion
                                                        PDet.Quantity_In_UMBas = 0
                                                        PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                    Else
                                                        PDet.Quantity = vDeltaFactorPresentacion
                                                        PDet.Quantity_In_UMBas = vCantidadSobranteUnidades
                                                        PDet.Variant_Code = BeProductoPresentacion.Nombre
                                                    End If

                                                    vExplosionAutomatica = True

                                                Else
                                                    'La cantidad es menor que una caja, solicitar unidades a WMS.
                                                    vExplosionAutomatica = False
                                                End If

                                            End If

                                        Else
                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Error_20220224_0121: La implosión automática está activa, pero se encontró más de una presentación para el producto: {0}, el sistema no puede determinar el factor de implosión.",
                                                                                                     BeProducto.Codigo),
                                                                                                     NAVEnvioAlm.No,
                                                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                     BeConfigDet.Idnavconfigdet,
                                                                                                     CnnLog)
                                        End If

                                    End If

                                End If

                            End If

                            If TrasladoExistente Is Nothing Then

                                Try

                                    If BeClienteDetalle Is Nothing Then

                                        vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                        'Primera llamada (No tiene Cliente en el detalle)
                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            BeClienteDetalle,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                            '#EJC2312260941:
                                            'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                        End If

                                        If lBeStockResPedido Is Nothing Then

                                            If lBeStockResPedido.Count > 0 Then

                                                '#EJC20220314:La solicitud se hizo en UMBAS y no se encontró stock.
                                                If PDet.Variant_Code = "" Then

                                                    '#EJC20220314: Existe una presentación con el codigo de producto proporcionado.
                                                    If Not BeProductoPresentacionDefecto Is Nothing Then

                                                        '#EJC20220314:Intentar reservar el stock solicitado en UMBAS a partir del stock en presentación.
                                                        PDet.Variant_Code = PDet.Unit_of_Measure_Code
                                                        PDet.Quantity = 0

                                                        'Segunda llamada
                                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                            PDet,
                                                                                            BeProducto,
                                                                                            vDiasVencimientoCliente,
                                                                                            BeUnidadMedida,
                                                                                            BeProductoPresentacion,
                                                                                            BeClienteDetalle,
                                                                                            BeConfigEnc,
                                                                                            BeConfigEnc.Idbodega,
                                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                                            0,
                                                                                            lblprg,
                                                                                            lBeStockResPedido,
                                                                                            lConnectionInterface,
                                                                                            CnnLog,
                                                                                            lTransInterface) Then
                                                            '#EJC202312260939:

                                                        End If

                                                    End If

                                                End If

                                            End If

                                        End If

                                    Else
                                        'Tercera llamada (tiene cliente en el detalle)
                                        If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                        PDet,
                                                                        BeProducto,
                                                                        vDiasVencimientoCliente,
                                                                        BeUnidadMedida,
                                                                        BeProductoPresentacion,
                                                                        BeClienteDetalle,
                                                                        BeConfigEnc,
                                                                        BeConfigEnc.Idbodega,
                                                                        pBePedidoEnc.IdPropietarioBodega,
                                                                        0,
                                                                        lblprg,
                                                                        lBeStockResPedido,
                                                                        lConnectionInterface,
                                                                        CnnLog,
                                                                        lTransInterface) Then

                                            '#EJC2312260941:
                                            'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                        End If

                                    End If

                                    If Not lBeStockResPedido Is Nothing Then

                                        If lBeStockResPedido.Count > 0 Then

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            vInsertoLineaDetalle = True

                                            Dim vCantidadReservadaWMS As Double = 0
                                            vCantidadReservadaWMS = lBeStockResPedido.Sum(Function(x) x.Cantidad)

                                            If Not BeProductoPresentacion Is Nothing Then
                                                vCantidadReservadaWMS = IIf(PDet.Variant_Code = "", vCantidadReservadaWMS, vCantidadReservadaWMS / IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))
                                                If vExplosionAutomatica Then
                                                    If Not PDet.Variant_Code.StartsWith("CJ") Then
                                                        vCantidadReservadaWMS = vCantidadReservadaWMS * BeProductoPresentacion.Factor
                                                    End If
                                                End If
                                            Else
                                                Debug.WriteLine("#EJC202312191300: Es valido?")
                                            End If

                                            PDet.Qty_to_Ship = vCantidadReservadaWMS

                                            If PDet.Is_Partially_Processed Then
                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea procesada parcialmente, Línea:{0}  Código:{1}  Solicitado: {2}{3} Abastecido:{4}  ",
                                                                                          PDet.Line_No,
                                                                                          PDet.Item_No,
                                                                                          PDet.Quantity,
                                                                                          IIf(PDet.Variant_Code = "", PDet.Unit_of_Measure_Code, PDet.Unit_of_Measure_Code),
                                                                                          IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Qty_to_Ship /
                                                                                                  IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))))
                                                clsPublic.Actualizar_Progreso_CR(lblprg)
                                            Else


                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("{2}{2}Línea procesada -> Abastecido:{0}{1}  ",
                                                                                          IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Quantity_Reserved_WMS),
                                                                                          IIf(PDet.Variant_Code = "", PDet.Unit_of_Measure_Code, PDet.Unit_of_Measure_Code),
                                                                                          vbTab))

                                                clsPublic.Actualizar_Progreso_CR(lblprg)

                                            End If

                                            Lineas_Detalle_Procesadas.Add(PDet)

                                        Else

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea procesada -> Abastecido:{0}{1}  ", 0, vbNewLine))
                                            clsPublic.Actualizar_Progreso_CR(lblprg)

                                        End If

                                    End If

                                Catch ex As Exception
                                    Throw
                                End Try

                            Else 'es un pedido existente.

                                'Si la línea de detalle no existe
                                If Not clsLnTrans_pe_det.Existe(TrasladoExistente.IdPedidoEnc,
                                                                PDet.Line_No,
                                                                pBePedidoDet,
                                                                PDet.No,
                                                                lConnectionInterface,
                                                                lTransInterface) Then

                                    Try


                                        If BeClienteDetalle Is Nothing Then

                                            'Cuarta llamada
                                            If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            Nothing,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                                '#EJC2312260941:
                                                'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                            End If

                                        Else

                                            'Quinta llamada
                                            If Not Inserta_Linea_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                            PDet,
                                                                            BeProducto,
                                                                            vDiasVencimientoCliente,
                                                                            BeUnidadMedida,
                                                                            BeProductoPresentacion,
                                                                            BeClienteDetalle,
                                                                            BeConfigEnc,
                                                                            BeConfigEnc.Idbodega,
                                                                            pBePedidoEnc.IdPropietarioBodega,
                                                                            0,
                                                                            lblprg,
                                                                            lBeStockResPedido,
                                                                            lConnectionInterface,
                                                                            CnnLog,
                                                                            lTransInterface) Then

                                                '#EJC2312260941:
                                                'clsPublic.Actualizar_Progreso(lblprg, "No fue posible completar la reserva.")

                                            End If

                                        End If

                                        If Not lBeStockResPedido Is Nothing Then

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            vInsertoLineaDetalle = True

                                            Dim vCantidadReservadaWMS As Double = 0
                                            vCantidadReservadaWMS = lBeStockResPedido.Sum(Function(x) x.Cantidad)
                                            PDet.Qty_to_Ship = vCantidadReservadaWMS
                                            vCantidadReservadaWMS = IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Qty_to_Ship / IIf(BeProductoPresentacion IsNot Nothing, BeProductoPresentacion.Factor, 1))

                                            Lineas_Detalle_Procesadas.Add(PDet)

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc),
                                                                                                PDet.No,
                                                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                BeConfigDet.Idnavconfigdet,
                                                                                                CnnLog)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc))

                                        End If

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   pBePedidoEnc.IdPedidoEnc,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        Throw

                                    End Try

                                Else

                                    If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                        Dim vMensajeError As String = String.Format("El pedido: {0} existe,
                                                                                     la línea de detalle: {1} existe, 
                                                                                     cantidad_origen <> cantidad_destino
                                                                                     no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No)

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeError,
                                                                                   PDet.No,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        clsPublic.Actualizar_Progreso(lblprg, vMensajeError)
                                        clsPublic.Actualizar_Progreso_CR(lblprg)

                                    Else

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} existe,
                                                                                   la línea de detalle: {1} existe, 
                                                                                   cantidad_origen = cantidad_destino
                                                                                   no se actualizará", PDet.NoEnc, PDet.Line_No))
                                        clsPublic.Actualizar_Progreso_CR(lblprg)

                                    End If

                                End If

                            End If 'fin TrasladoExistente                                    

                        Next

                        Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
#End Region

                    End If 'fin TrasladoExistente 1

#Region "Si hay lineas reservadas se crea hoja de trabajo"

                    Try

                        If vContador_Lineas_Detalle_Pedido_Insertadas = 0 Then

                            If (pBePedidoEnc.IdPedidoEnc <> 0) Then
                                If Not clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                                       lConnectionInterface,
                                                                       lTransInterface) Then

                                    clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc, lConnectionInterface, lTransInterface)
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", NAVEnvioAlm.No, vbNewLine))
                                    clsPublic.Actualizar_Progreso_CR(lblprg)

                                Else
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} no insertó líneas nuevas, pero ya tiene detalle en WMS; no se elimina la cabecera: {1}", NAVEnvioAlm.No, vbNewLine))
                                    clsPublic.Actualizar_Progreso_CR(lblprg)
                                End If
                            Else
                                clsPublic.Actualizar_Progreso(lblprg, String.Format("El envío {0} ya existe en el WMS {1}", NAVEnvioAlm.No, vbNewLine))
                                clsPublic.Actualizar_Progreso_CR(lblprg)
                            End If

                        Else

                            vCodeUnitNavError = False

                            Try

                                '#EJC20210709: Si ya se registró en la hoja de trabajo NAV no registra nuevamente.
                                If Not lDocumentosHojaDeTrabajo.Contains(NAVEnvioAlm.No) Then

                                    vRespuestaSetWarehouseDocuments = ""

                                    '#EJC20210614: Envía a traer las líneas o cantidades pendientes de envío.
                                    wsCUWMS.SetWarehouseDocuments(CurrentWkshName,
                                                                  NAVEnvioAlm.No,
                                                                  vRespuestaSetWarehouseDocuments)

                                    lDocumentosHojaDeTrabajo.Add(NAVEnvioAlm.No)

                                End If

                                '#EJC202301242112: Actualizar a estado importado...
                                NAVEnvioAlm.Status = 3

                                clsLnI_nav_ped_traslado_enc.Actualizar_Estado(NAVEnvioAlm, lConnectionInterface, lTransInterface)

                            Catch ex As Exception

                                '#EJC20210707: es probable que el envío al que se hace referencia ya no esté disponible o que ya no tenga lineas pendientes.
                                If ex.Message.Contains("There are no Warehouse Worksheet Lines created.") Then

                                    vCodeUnitNavError = True

                                    clsPublic.Actualizar_Progreso(lblprg, "Error en Code Unit - SetWarehouseDocuments " & ex.Message)
                                    clsPublic.Actualizar_Progreso_CR(lblprg)
                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                               pBePedidoEnc.IdPedidoEnc,
                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                               BeConfigDet.Idnavconfigdet,
                                                                               CnnLog)

                                Else
                                    Throw
                                End If

                            End Try

                            If Not vCodeUnitNavError Then

                                TheWorkSheetNav = srvWorkSheet.ReadMultiple(CurrentWkshName,
                                                                            vCodigoBodega,
                                                                            CurrentSortingMethod,
                                                                            Nothing,
                                                                            Nothing,
                                                                            0)

                                If Not TheWorkSheetNav Is Nothing Then

                                    For Each PDet In Lineas_Detalle_Procesadas.OrderBy(Function(x) x.Line_No)

                                        Source_ID = PDet.Source_ID
                                        vCantidad = PDet.Quantity
                                        vCodigoProducto = PDet.Item_No
                                        vNoLinea = PDet.Line_No

                                        BePresentacion = New clsBeProducto_Presentacion()

                                        If PDet.Variant_Code <> "" Then

                                            BePresentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(PDet.Item_No,
                                                                                                                                PDet.Variant_Code,
                                                                                                                                lConnectionInterface,
                                                                                                                                lTransInterface)
                                            If Not BePresentacion Is Nothing Then
                                                vNombrePresentacion = BePresentacion.Nombre
                                                vEnviarEnPresentacion = True
                                            Else
                                                vEnviarEnPresentacion = False
                                            End If

                                        Else
                                            vEnviarEnPresentacion = False
                                            BePresentacion = Nothing
                                            vNombrePresentacion = ""
                                        End If

                                        Dim lItemOnWorkSheet = TheWorkSheetNav.Where(Function(x) x.Item_No = vCodigoProducto _
                                                                                     AndAlso x.Whse_Document_No = NAVEnvioAlm.No _
                                                                                     AndAlso x.Source_No = Source_ID _
                                                                                     AndAlso x.Whse_Document_Line_No = vNoLinea)

                                        If Not lItemOnWorkSheet Is Nothing Then

                                            If lItemOnWorkSheet.Count = 0 Then
                                                clsPublic.Actualizar_Progreso(lblprg, "#ERROR_20231218: No se encontró el objeto en la hoja de trabajo de NAV para este registro.")
                                            End If

                                        End If

                                        For Each ItemOnWorkSheet In lItemOnWorkSheet

                                            If ItemOnWorkSheet.Qty_to_Handle = 0 Then

                                                vInsertoLineaDetalle = True

                                                If PDet.Unit_of_Measure_Code = ItemOnWorkSheet.Unit_of_Measure_Code Then

                                                    clsPublic.Actualizar_Progreso(lblprg, "Actualizando - Qty_to_Handle de Source_ID" & PDet.Source_ID & " Codigo: " & PDet.Item_No & " Línea: " & vNoLinea)

                                                    If (PDet.Unit_of_Measure_Code = ItemOnWorkSheet.Unit_of_Measure_Code) AndAlso
                                                        (PDet.Unit_of_Measure_Code = BeProducto.UnidadMedida.Codigo) Then

                                                        vCantidadAEnviar = IIf(PDet.Variant_Code = "", PDet.Qty_to_Ship, PDet.Quantity_Reserved_WMS)

                                                        If vEnviarEnPresentacion Then
                                                            ItemOnWorkSheet.Qty_to_Handle += vCantidadAEnviar
                                                        Else
                                                            ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                        End If

                                                    Else

                                                        If Not BePresentacion Is Nothing Then

                                                            If BePresentacion.Factor > 0 Then
                                                                If vInsertoLineaDetalle Then
                                                                    ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                                Else
                                                                    ItemOnWorkSheet.Qty_to_Handle = 0
                                                                End If
                                                            Else
                                                                Throw New Exception("Factor incorrecto para la presentación de producto al actualizar la cantidad a manejar en el codeunit.")
                                                            End If

                                                        Else
                                                            ItemOnWorkSheet.Qty_to_Handle += (PDet.Qty_to_Ship)
                                                        End If

                                                    End If

                                                Else
                                                    If vInsertoLineaDetalle Then
                                                        ItemOnWorkSheet.Qty_to_Handle += PDet.Qty_to_Ship
                                                    Else
                                                        ItemOnWorkSheet.Qty_to_Handle = 0
                                                    End If
                                                End If

                                                lCreaPicking.Add(ItemOnWorkSheet)

                                            End If

                                        Next

                                    Next

                                    If vInsertoLineaDetalle Then

                                        Try

                                            srvWorkSheet.UpdateMultiple(CurrentWkshName,
                                                                            vCodigoBodega,
                                                                            CurrentSortingMethod,
                                                                            lCreaPicking.ToArray())

                                        Catch ex As Exception

                                            '#EJC20210707: es probable que el envío al que se hace referencia ya no esté disponible o que ya no tenga lineas pendientes.
                                            If ex.Message.Contains("You cannot handle more than the outstanding") Then

                                                vCodeUnitNavError = True

                                                clsPublic.Actualizar_Progreso(lblprg, "Error en Code Unit - SetWarehouseDocuments " & ex.Message)

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               pBePedidoEnc.IdPedidoEnc,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                                            Else
                                                Throw New Exception(String.Format("{2} {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message, vbNewLine))
                                            End If

                                        End Try

                                    End If

                                    For Each PDet In Lineas_Detalle_Procesadas.OrderBy(Function(x) x.Line_No)

                                        Source_ID = PDet.Source_ID
                                        vCantidad = PDet.Quantity
                                        vCodigoProducto = PDet.Item_No
                                        vNoLinea = PDet.Line_No

                                        BePresentacion = New clsBeProducto_Presentacion()

                                        If PDet.Variant_Code <> "" Then

                                            BePresentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(PDet.Item_No,
                                                                                                                                PDet.Variant_Code,
                                                                                                                                lConnectionInterface,
                                                                                                                                lTransInterface)
                                            If Not BePresentacion Is Nothing Then
                                                vNombrePresentacion = BePresentacion.Nombre
                                                vEnviarEnPresentacion = True
                                            Else
                                                vEnviarEnPresentacion = False
                                            End If

                                        Else
                                            vEnviarEnPresentacion = False
                                            BePresentacion = Nothing
                                            vNombrePresentacion = ""
                                        End If

                                        Try

                                            Dim vContadorLotesProcesadosCorrectamente As Integer = 0

                                            lBeStockResPedido = clsLnStock_res.Get_All_By_Params_For_BYB(pBePedidoEnc.IdPedidoEnc,
                                                                                                             vCodigoProducto,
                                                                                                             vNoLinea,
                                                                                                             Source_ID,
                                                                                                             lConnectionInterface,
                                                                                                             lTransInterface)


                                            Dim ListaStockReservadoConsolidado = Nothing
                                            Dim vCantidadLote As Double = 0

                                            If Not lBeStockResPedido Is Nothing Then

                                                ListaStockReservadoConsolidado = From i In lBeStockResPedido Group i By Keys = New With {Key i.IdProductoBodega,
                                                                                Key i.Lote,
                                                                                Key i.Fecha_vence,
                                                                                Key i.IdUnidadMedida} Into Group
                                                                                 Select New With {Keys.IdProductoBodega,
                                                                                                              Keys.Lote,
                                                                                                              Keys.Fecha_vence,
                                                                                                              Keys.IdUnidadMedida,
                                                                                                              .Cantidad = Group.Sum(Function(x) x.Cantidad)}

                                                If Not ListaStockReservadoConsolidado Is Nothing Then

                                                    For Each StockReservado In ListaStockReservadoConsolidado

                                                        vFechaVenceNav = StockReservado.Fecha_vence.Date.Year & "-" & Right("00" & StockReservado.Fecha_vence.Date.Month, 2) & "-" & Right("00" & StockReservado.Fecha_vence.Day, 2)

                                                        clsPublic.Actualizar_Progreso(lblprg, vbTab & "Registrando lote: " & StockReservado.Lote & " Cantidad: " & StockReservado.Cantidad & " Código: " & PDet.Item_No)

                                                        vCantidadLote = 0

                                                        wsCUWMS.InsertLotNo(CurrentWkshName,
                                                                            vRespuestaInsertLotNo,
                                                                            PDet.Item_No,
                                                                            PDet.Source_ID,
                                                                            StockReservado.Lote,
                                                                            vFechaVenceNav,
                                                                            StockReservado.Cantidad,
                                                                            vNoLinea)

                                                        TotalLotesEnviados += 1

                                                        If vRespuestaInsertLotNo = "Lote ingresado correctamente" Then

                                                            vContadorLotesProcesadosCorrectamente += 1
                                                            clsPublic.Actualizar_Progreso(lblprg, vbTab & vbTab & vRespuestaInsertLotNo)
                                                            vRespuestaInsertLotNo = ""

                                                        End If

                                                    Next

                                                    vCrearPickingNav = (vContadorLotesProcesadosCorrectamente > 0) OrElse vCrearPickingNav

                                                End If

                                            End If

                                        Catch ex As Exception

                                            '#EJC20210707: es probable que el envío al que se hace referencia ya no esté disponible o que ya no tenga lineas pendientes.
                                            If ex.Message.Contains("You cannot handle more than the outstanding") Then

                                                vCodeUnitNavError = True

                                                clsPublic.Actualizar_Progreso(lblprg, "Error en Code Unit - SetWarehouseDocuments " & ex.Message)

                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                                       pBePedidoEnc.IdPedidoEnc,
                                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                                            Else
                                                Throw New Exception(String.Format("{2} {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message, vbNewLine))
                                            End If

                                        End Try

                                    Next

                                    If vCrearPickingNav Then

                                        Try

                                            vCrearPickingNav = False

                                            If Lineas_Detalle_Procesadas.Count > 0 Then

                                                wsCUWMS.CreatePick(CurrentWkshName, vRespuestaNoPicking)

                                                If vRespuestaNoPicking.Contains("has been created.") Then

                                                    '#CKFK 20211124 Que Erik me confirme si así está correcto el proceso
                                                    pBePedidoEnc.No_Picking_ERP = vRespuestaNoPicking.Replace("Pick activity no. ", "").Replace(" has been created.", "")

                                                    ThePickingNAV = wsSrvPickingNAV.Read(pBePedidoEnc.No_Picking_ERP)
                                                    Dim BeStockResEncontradoEnPickingNav As New clsBeStock_res()

                                                    wsSrvPickingNAV.Update(ThePickingNAV)

                                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Picking {0} creado correctamente para el envío {1} {2}", pBePedidoEnc.No_Picking_ERP, pBePedidoEnc.Referencia, vbNewLine))

                                                    clsLnTrans_pe_enc.Actualizar_No_Picking_ERP(pBePedidoEnc,
                                                                                                lConnectionInterface,
                                                                                                lTransInterface)

                                                    Importar_Envio_Almacen2 = True

                                                Else
                                                    Throw New Exception(String.Format("No pudo ser creado el Picking para el envío {0}", pBePedidoEnc.Referencia))
                                                End If

                                            Else
                                                clsPublic.Actualizar_Progreso(lblprg, "No hay líneas en la hoja de trabajo para generar picking")
                                                clsPublic.Actualizar_Progreso(lblprg, String.Format("No hay líneas en la hoja de trabajo para generar picking para el envío {0} {1}", pBePedidoEnc.Referencia, vbNewLine))
                                            End If

                                        Catch ex As Exception

                                            Dim vMsgError As String = String.Format("Error al crear el picking para el Envío: {0} {1} {2}", NAVEnvioAlm.No, ex.Message, vbNewLine)

                                            clsPublic.Actualizar_Progreso(lblprg, vMsgError)

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgError,
                                                                                       NAVEnvioAlm.No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                                        End Try

                                    End If

                                End If

                            End If

                        End If

                    Catch ex As Exception

                        Dim vMsgError As String = String.Format("Error al insertar el lote para el envío : {0} y P.V. {1} y producto {2}. {4} Error: {3} {4} ",
                                                                        NAVEnvioAlm.No,
                                                                        Source_ID,
                                                                        vCodigoProducto,
                                                                        ex.Message,
                                                                        vbNewLine)

                        clsPublic.Actualizar_Progreso(lblprg, vMsgError)
                        clsPublic.Actualizar_Progreso_CR(lblprg)

                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgError.Trim,
                                                                   NAVEnvioAlm.No,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet, CnnLog)

                    End Try

#End Region

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Evío Inactivo {0} ", NAVEnvioAlm.No, vbNewLine))
                clsPublic.Actualizar_Progreso_CR(lblprg)
            End If

            lTransInterface.Commit()

        Catch ex As Exception

            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar pedido de traslado a tabla de TOMWMS: {0} {1}", ex.Message, vbNewLine))

            'Si no existe picking no debo borrar
            Try

                wsCUWMS.BorraPicking(NAVEnvioAlm.No)

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Se eliminó correctamente el envío de almacén: {0} {1}", NAVEnvioAlm.No, vbNewLine))

            Catch ex1 As Exception
                clsPublic.Actualizar_Progreso(lblprg, String.Format("No se pudo eliminar el envío de almacén: {0} {1} {2}", NAVEnvioAlm.No, ex.Message, vbNewLine))
            End Try

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       NAVEnvioAlm.No,
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

        Finally
            If lConnectionInterface.State = ConnectionState.Open Then lConnectionInterface.Close()
        End Try

        Return Importar_Envio_Almacen2

    End Function

End Class
