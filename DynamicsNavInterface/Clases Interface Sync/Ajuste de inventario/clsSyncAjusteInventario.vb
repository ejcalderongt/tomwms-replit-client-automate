Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS.WSAjusteInventario
Imports TOMWMS.WSDiarioAlmacen
Imports TOMWMS.WSFichaBodegas
Imports TOMWMS.WSSeries

Public Class clsSyncAjusteInventario : Inherits clsInterfaceBase
    Implements IDisposable

    Property pBodega As String = ""

    Private ReadOnly wsAjusteInventario As New Ajustes_Inventario() With
    {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
    }

    Private wsSeries As New Series_Service() With
    {
        .UseDefaultCredentials = UsarCredencialesPorDefecto,
        .Credentials = CredencialesConexion
    }

    Public Sub Sync_Ajustes(ByVal lblprg As RichTextBox,
                            ByRef prg As System.Windows.Forms.ProgressBar)

        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)

        Try

            CnnLog.Open()

            Actualizar_Progreso(lblprg, "Consultando ajustes pendientes de envío.")

            Dim lAjustesPendEnvio As New List(Of clsBeTrans_ajuste_enc)
            lAjustesPendEnvio = clsLnTrans_ajuste_enc.GetAll_Pendientes_Envio()

            If Not lAjustesPendEnvio Is Nothing Then

                Dim lVistaAjustesPendientesEnvio As New List(Of clsBe_vw_ajustes)
                Dim vDif As Double = 0
                Dim vNoDocumento As String = ""
                Dim vContador As Integer = 0
                Dim BeAjusteDet As New clsBeTrans_ajuste_det
                Dim DetallesEnviados As Integer = 0
                Dim BeFamilia As New clsBeProducto_familia
                Dim vSerieBodega As String = ""
                Dim BeCliente As New clsBeCliente
                Dim Cod_Variante As String = ""
                Dim AjusteDoc As New clsBeTrans_ajuste_det_doc
                Dim MaxIdAjusteDoc As Integer = 0
                Dim vCodigoBodega As String = ""
                Dim vSerieNav As Series
                Dim vNomenclaturaBase As String = ""
                Dim vCorrelativoActual As Integer = 0
                Dim vSerieAjusteNAV As String = ""
                Dim vSeccionAjusteNAV As String = ""
                Dim vCodigoDiario As String = "PRODUCTO" '#CKFK20220210 Dice Ricardo que se debe enviar siempre PRODUCTO
                Dim vCodigoSeccion As String = "GENERICO" '#CKFK20220210 Dice Ricardo que se debe enviar siempre GENERICO
                Dim vResultadoInsertLoteDiarioALM As String = ""
                Dim vResultadoRegistraAjustesAlmacen As String = ""
                Dim vResultadoCalculaAjusteAlmacen As String = ""
                Dim vResultadoRegistrasAjustesDiarios As String = ""
                Dim vNoDocumentoAjuste As String = ""
                Dim vUnidadMedida As String = ""
                Dim BeCentroCosto As New clsBeCentro_costo

                prg.Maximum = lAjustesPendEnvio.Count

                MaxIdAjusteDoc = clsLnTrans_ajuste_det_doc.MaxID() + 1

                Dim wsBodegaService As New Ficha_Bodegas_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

                wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

                Dim wsDiarioAlmacen As New Diario_Almacen_Service() With
                {
                .UseDefaultCredentials = UsarCredencialesPorDefecto,
                .Credentials = CredencialesConexion
                }

                wsDiarioAlmacen.Url = My.Settings.NavSync_WSDiarioAlmacen_Diario_Almacen_Service

                wsSeries.Url = My.Settings.NavSync_WSSeries_Series_Service

                '#EJC20210426: CodeUnit de NAV para WMS, agregado por la bodega de PT.
                Dim wsCUWMS As New CUWMS.CUWMS() With {.UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                       .Credentials = CredencialesConexion
                                                      }

                wsCUWMS.Url = My.MySettings.Default.NavSync_CUWMS_CUWMS

                Dim beBodega As Ficha_Bodegas
                Dim NAVDiario As New Diario_Almacen

                For Each AjEnc In lAjustesPendEnvio

                    vNoDocumento = Right("000000" & AjEnc.Idajusteenc, 6)
                    vNoDocumento = "WMS" + vNoDocumento

                    lVistaAjustesPendientesEnvio = clsLn_vw_ajustes.Get_All_Pendientes_Envio(AjEnc.Idajusteenc)

                    DetallesEnviados = 0

                    '#EJC20180711: Obtener la sección para NAV (Debe llenarse a traves del mantenimiento de familias y se selecciona en el encabezado del ajuste)
                    If AjEnc.IdProductoFamilia > 0 Then
                        BeFamilia = clsLnProducto_familia.GetSingle(AjEnc.IdProductoFamilia)
                    End If

                    If lVistaAjustesPendientesEnvio.Count > 0 Then

                        Actualizar_Progreso(lblprg, "Detalle de ajustes para transacción: " & AjEnc.Idajusteenc)

                        '#EJC20210928: Leer ficha de bodega para saber si es almacen avanzado en NAV o NO.
                        vCodigoBodega = clsLnBodega.Get_Codigo_By_IdBodega(lVistaAjustesPendientesEnvio.Item(0).IdBodegaERP)
                        beBodega = wsBodegaService.Read(vCodigoBodega)

                        For Each AjDet In lVistaAjustesPendientesEnvio
#Region "SERIE_AJUSTE"

                            If beBodega.Require_Pick AndAlso beBodega.Require_Receive Then

                                If AjDet.Tipo_Ajuste = "Ajuste Positivo" Then
                                    vSerieAjusteNAV = "DIAP-AJDEV"
                                ElseIf AjDet.Tipo_Ajuste = "Ajuste Negativo" Then
                                    vSerieAjusteNAV = "DIAP-VENT"
                                Else
                                    vSerieAjusteNAV = "DPROM"
                                End If

                                vSerieBodega = vSerieAjusteNAV

                            Else

                                '#CKFK20231214 Cambié esto porque no se estaba obteniendo el código correcto
                                'BeCliente = clsLnCliente.Get_Single(AjEnc.IdBodega)
                                BeCliente = clsLnCliente.Get_Single_By_Codigo(AjDet.Codigo_Bodega)

                                If BeCliente Is Nothing Then

                                    XtraMessageBox.Show("No existe el cliente con identificador: " & AjEnc.IdBodega & " para la bodega, esto evita tener una serie definida",
                                                        "Ajuste",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error)
                                Else
                                    vSerieBodega = BeCliente.Referencia
                                End If

                            End If

                            '#EJC20180711: Aplicar filtro de serie por bodega para obtener el correlativo del ajuste de NAV.
                            Dim vFiltro2 As New Series_Filter() With {.Field = Series_Fields.Series_Code, .Criteria = vSerieBodega}

                            If vSerieBodega.Trim = "" Then
                                Throw New Exception(String.Format("No está definida la serie de ajustes para la bodega:{0}", BeCliente.Codigo))
                            End If

                            '#EJC20180711: Leer únicamente la serie correspondiente para la bodega del ajuste.
                            vSerieNav = wsSeries.Read(vSerieBodega)

#End Region
                            AjusteDoc = New clsBeTrans_ajuste_det_doc

                            If Not vSerieNav Is Nothing Then

                                vNoDocumento = vSerieNav.Last_No_Used

                                If vNoDocumento Is Nothing Then vNoDocumento = vSerieNav.Ending_No

                                vNomenclaturaBase = vNoDocumento.Substring(0, 4)

                                If vCorrelativoActual = 0 Then

                                    If beBodega.Require_Pick AndAlso beBodega.Require_Receive Then
                                        vCorrelativoActual = Val(vNoDocumento.Substring(5, 8))
                                    Else
                                        vCorrelativoActual = Val(vNoDocumento.Substring(4, 6))
                                    End If

                                End If

                            Else
                                '#EJC20180711_1203PM
                                Throw New Exception(String.Format("No se pudo obtener el correlativo para la serie:{0}", vSerieBodega))
                            End If

                            If vCorrelativoActual = 0 Then

                                vCorrelativoActual = Val(vNoDocumento.Substring(4, 6))
                                vCorrelativoActual += 1

                            ElseIf vCorrelativoActual = 999999 Then '#CKFK 20180927 0953PM Agregué esta condición porque cuando la serie es nueva el correlativo actual va a ser igual a 999999

                                vCorrelativoActual = 1

                            Else
                                vCorrelativoActual += 1
                            End If

                            vNoDocumento = vNomenclaturaBase + Right("000000" & vCorrelativoActual, 6)

                            Actualizar_Progreso(lblprg, "Procesando ajuste número de documento: " & vNoDocumento)

                            BeAjusteDet.IdAjusteDet = AjDet.IdAjusteDet
                            BeAjusteDet.IdAjusteEnc = AjEnc.Idajusteenc
                            clsLnTrans_ajuste_det.GetSingle(BeAjusteDet)

                            If Not BeFamilia Is Nothing Then
                                AjDet.Seccion = BeFamilia.Nombre
                            End If

                            '#CM_20180828_2:54PM: Llena datos para el documento de ajustes. 
                            AjusteDoc.Idajustedoc = MaxIdAjusteDoc
                            AjusteDoc.Idajusteenc = AjEnc.Idajusteenc
                            AjDet.UMBas = BeAjusteDet.UmBas

                            vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                            If BeAjusteDet.IdPresentacion <> 0 Then

                            End If

                            If AjDet.Modifica_Cantidad Then

                                If AjDet.Tipo_Ajuste = "Ajuste Positivo" Then
                                    vSeccionAjusteNAV = "AJPOSDEV."
                                ElseIf AjDet.Tipo_Ajuste = "Ajuste Negativo" Then
                                    vSeccionAjusteNAV = "AJNEGVENT"
                                Else
                                    vSeccionAjusteNAV = "DPROM"
                                End If

                                If AjDet.Cantidad_original > AjDet.Cantidad_nueva Then 'Es un ajuste negativo

                                    vDif = Math.Round(AjDet.Cantidad_original - AjDet.Cantidad_nueva, 6)

                                    Try

                                        '#EJC20210928: Si es almacén avanzado en NAV, hacer de esta forma.
                                        If beBodega.Require_Pick AndAlso beBodega.Require_Receive Then

                                            NAVDiario.Registering_Date = AjDet.Fecha
                                            NAVDiario.Whse_Document_No = vNoDocumento
                                            NAVDiario.Item_No = AjDet.Codigo_Producto
                                            NAVDiario.Variant_Code = Nothing
                                            NAVDiario.Description = AjDet.Observacion
                                            NAVDiario.Zone_Code = "ALM"
                                            NAVDiario.Bin_Code = "ALM"
                                            NAVDiario.Quantity = -vDif
                                            ''#EJC20211019: Enviar cantidad con signo.
                                            'If AjDet.IdPresentacion <> 0 Then
                                            '    NAVDiario.Quantity = -(AjDet.Cantidad_original - AjDet.Cantidad_nueva) / AjDet.Factor
                                            'Else
                                            '    NAVDiario.Quantity = -(AjDet.Cantidad_original - AjDet.Cantidad_nueva)
                                            'End If
                                            NAVDiario.Unit_of_Measure_Code = AjDet.UMBas
                                            NAVDiario.Reason_Code = AjDet.Motivo_Ajuste

                                            If AjDet.Nombre_Presentacion = "" Then
                                                vUnidadMedida = AjDet.UMBas
                                            Else
                                                vUnidadMedida = AjDet.Nombre_Presentacion
                                            End If

                                            BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen

                                            If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                '#CKFK20220331 Cambié el vCodigoSeccionAjuste por vCodigoSeccion
                                                'y vNoDocumentoAjuste por vNoDocumento
                                                wsCUWMS.AjustesDiarioAlmacen(vCodigoSeccion,
                                                                             vCodigoBodega,
                                                                             vResultadoInsertLoteDiarioALM,
                                                                             vNoDocumento,
                                                                             AjDet.Codigo_Producto,
                                                                             "",
                                                                             NAVDiario.Quantity,
                                                                             AjDet.UMBas,
                                                                             AjDet.Motivo_Ajuste,
                                                                             AjDet.Lote_Nuevo,
                                                                             AjDet.Fecha_vence_nueva)
                                            Else
                                                vResultadoInsertLoteDiarioALM = "Successfully Created."
                                            End If

                                            If vResultadoInsertLoteDiarioALM = "The journal lines were successfully registered." Or
                                               vResultadoInsertLoteDiarioALM = "Successfully Created." Then

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen
                                                BeAjusteDet.estado_ajuste_erp = 1
                                                clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.CalculaAjusteAlmacen

                                                If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                    BeCentroCosto = New clsBeCentro_costo
                                                    BeCentroCosto.IdCentroCosto = AjEnc.IdCentroCosto

                                                    clsLnCentro_costo.GetSingle(BeCentroCosto)

                                                    wsCUWMS.CalculaAjusteAlmacen(vCodigoDiario,
                                                                                 vCodigoSeccion,
                                                                                 AjDet.Codigo_Producto,
                                                                                 vNoDocumento,
                                                                                 vResultadoCalculaAjusteAlmacen,
                                                                                 AjDet.Observacion,
                                                                                 BeCentroCosto.Codigo)

                                                Else
                                                    vResultadoCalculaAjusteAlmacen = "Successfully Created"
                                                End If

                                                If vResultadoCalculaAjusteAlmacen = "Successfully Created" Then

                                                    Try

                                                        BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.RegistrasAjustesDiarios

                                                        If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                            'wsAjusteInventario.RegistrasAjustesDiarios(vNoDocumento,
                                                            '                                           vCodigoSeccion,
                                                            '                                           vCodigoDiario)

                                                            BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.RegistrasAjustesDiarios
                                                            BeAjusteDet.estado_ajuste_erp = 1
                                                            clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)

                                                        End If

                                                    Catch ex As Exception

                                                        BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.RegistrasAjustesDiarios
                                                        BeAjusteDet.estado_ajuste_erp = 0
                                                        clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                        Throw New Exception("Ocurrió un error al realizar el registro del ajuste del diario ")

                                                    End Try

                                                Else
                                                    BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.CalculaAjusteAlmacen
                                                    BeAjusteDet.estado_ajuste_erp = 0
                                                    clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                    Throw New Exception("Ocurrió un error al realizar el cálculo en el ajuste de almacén " & vResultadoCalculaAjusteAlmacen)
                                                End If


                                            Else

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen
                                                BeAjusteDet.estado_ajuste_erp = 0
                                                clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                Throw New Exception("Ocurrió un error al realizar el ajuste diario de almacén " & vResultadoInsertLoteDiarioALM)

                                            End If

                                        Else

                                            wsAjusteInventario.ProcesaAjusteNegativo(vNoDocumento,
                                                                                     AjDet.Codigo_Producto,
                                                                                     Cod_Variante,
                                                                                     IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo),
                                                                                     AjDet.UMBas,
                                                                                     vDif,
                                                                                     AjDet.Lote_Nuevo,
                                                                                     AjDet.Motivo_Ajuste,
                                                                                     AjDet.Observacion,
                                                                                     AjDet.Seccion)


                                        End If


                                        lblprg.AppendText("Procesando ajuste negativo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto & vbNewLine)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        lblprg.Refresh()

                                        BeAjusteDet.Enviado = True
                                        clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            If Not AjEnc.Referencia.Contains(vNoDocumento) Then
                                                AjEnc.Referencia = AjEnc.Referencia
                                            End If
                                        End If

                                        '#CM_20180828_2:57PM: Llena la referencia para el documento e inserta en la tabla.  
                                        'AjusteDoc.Documento = AjEnc.Referencia.
                                        '#EJC20180924: Cambio de Referencia a Documento.
                                        AjusteDoc.Documento = vNoDocumento
                                        clsLnTrans_ajuste_det_doc.Insertar(AjusteDoc)

                                        '#CM_20180828_2:57PM: Se puso en comentario porque ahora se insertan las referencias en una nueva tabla.
                                        'clsLnTrans_ajuste_enc.Actualizar_Referencia(AjEnc.Idajusteenc, AjEnc.Referencia)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                    AjEnc.Idajusteenc,
                                                                                    BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                    BeConfigDet.Idnavconfigdet, CnnLog)

                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("Error en NAV al procesar el ajuste #: " & AjEnc.Idajusteenc & vbNewLine)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText(ex.Message)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.SelectionStart = lblprg.TextLength
                                        vCorrelativoActual -= 1
                                        lblprg.ScrollToCaret()
                                        lblprg.Refresh()

                                    End Try

                                ElseIf AjDet.Cantidad_original < AjDet.Cantidad_nueva Then 'Es un ajuste positivo

                                    vDif = Math.Round(AjDet.Cantidad_nueva - AjDet.Cantidad_original, 6)

                                    Try

                                        '#EJC20210928: Si es almacén avanzado en NAV, hacer de esta forma.
                                        If beBodega.Require_Pick AndAlso beBodega.Require_Receive Then

                                            NAVDiario.Item_No = AjDet.Codigo_Producto
                                            NAVDiario.Quantity = AjDet.Cantidad_nueva
                                            NAVDiario.Zone_Code = "ALM"
                                            NAVDiario.Bin_Code = "ALM"
                                            NAVDiario.Unit_of_Measure_Code = AjDet.UMBas
                                            NAVDiario.Description = AjDet.Motivo_Ajuste
                                            NAVDiario.Whse_Document_No = vNoDocumento
                                            'wsDiarioAlmacen.Create("GENERICO", vCodigoBodega, NAVDiario)

                                            If AjDet.Nombre_Presentacion = "" Then
                                                vUnidadMedida = AjDet.UMBas
                                            Else
                                                vUnidadMedida = AjDet.Nombre_Presentacion
                                            End If

                                            NAVDiario.Quantity = vDif

                                            BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen

                                            If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                wsCUWMS.AjustesDiarioAlmacen(vCodigoSeccion,
                                                                            vCodigoBodega,
                                                                            vResultadoInsertLoteDiarioALM,
                                                                            vNoDocumento,
                                                                            AjDet.Codigo_Producto,
                                                                            "",
                                                                            NAVDiario.Quantity,
                                                                            AjDet.UMBas,
                                                                            AjDet.Motivo_Ajuste,
                                                                            AjDet.Lote_Nuevo,
                                                                            AjDet.Fecha_vence_nueva)

                                            Else

                                                vResultadoInsertLoteDiarioALM = "Successfully Created."

                                            End If

                                            '#EJC20210908: Pendiente definir código de departamentos.
                                            'wsCUWMS.RegistraAjustesAlmacen(vNoDocumento, "GENERICO", "PRODUCTO", vCodigoBodega, vResultadoRegistraAjustesAlmacen)

                                            If vResultadoInsertLoteDiarioALM = "The journal lines were successfully registered." Or
                                                vResultadoInsertLoteDiarioALM = "Successfully Created." Then

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen
                                                BeAjusteDet.estado_ajuste_erp = 1
                                                clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.CalculaAjusteAlmacen

                                                If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                    BeCentroCosto = New clsBeCentro_costo
                                                    BeCentroCosto.IdCentroCosto = AjEnc.IdCentroCosto

                                                    clsLnCentro_costo.GetSingle(BeCentroCosto)

                                                    wsCUWMS.CalculaAjusteAlmacen(vCodigoDiario,
                                                                                 vCodigoSeccion,
                                                                                 AjDet.Codigo_Producto,
                                                                                 vNoDocumento,
                                                                                 vResultadoCalculaAjusteAlmacen,
                                                                                 AjDet.Observacion,
                                                                                 BeCentroCosto.Codigo)


                                                Else
                                                    vResultadoCalculaAjusteAlmacen = "Successfully Created"
                                                End If

                                                If vResultadoCalculaAjusteAlmacen = "Successfully Created" Then

                                                    Try

                                                        BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.CalculaAjusteAlmacen

                                                        If Not clsLnTrans_ajuste_det.Referencia_Realizada(BeAjusteDet) Then

                                                        End If

                                                        BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.RegistrasAjustesDiarios
                                                        BeAjusteDet.estado_ajuste_erp = 1
                                                        clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)

                                                    Catch ex As Exception

                                                        BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.RegistrasAjustesDiarios
                                                        BeAjusteDet.estado_ajuste_erp = 0
                                                        clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                        Throw New Exception("Ocurrió un error al realizar el registro del ajuste del diario ")

                                                    End Try

                                                Else
                                                    BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.CalculaAjusteAlmacen
                                                    BeAjusteDet.estado_ajuste_erp = 0
                                                    clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                    Throw New Exception("Ocurrió un error al realizar el cálculo en el ajuste de almacén " & vResultadoCalculaAjusteAlmacen)
                                                End If

                                            Else

                                                BeAjusteDet.referencia_ajuste_erp = clsDataContractDI.tReferenciaAjusteERP.AjustesDiarioAlmacen
                                                BeAjusteDet.estado_ajuste_erp = 0
                                                clsLnTrans_ajuste_det.Actualizar_Estado_And_Referencia_Ajuste_ERP(BeAjusteDet)
                                                Throw New Exception("Ocurrió un error al realizar el ajuste diario de almacén " & vResultadoInsertLoteDiarioALM)

                                            End If

                                        Else

                                            wsAjusteInventario.ProcesaAjustePositivo(vNoDocumento,
                                                                                 AjDet.Codigo_Producto,
                                                                                 Cod_Variante,
                                                                                 IIf(AjEnc.Ajuste_Por_Inventario > 0, AjDet.Codigo_Bodega, BeCliente.Codigo),
                                                                                 AjDet.UMBas,
                                                                                 vDif,
                                                                                 AjDet.Lote_Original,
                                                                                 AjDet.Fecha_vence_nueva,
                                                                                 AjDet.Motivo_Ajuste,
                                                                                 AjDet.Observacion,
                                                                                 AjDet.Seccion)

                                        End If

                                        Actualizar_Progreso(lblprg, "Procesando ajuste positivo para: " & AjDet.Codigo_Producto & " " & AjDet.Nombre_Producto & vbNewLine)

                                        BeAjusteDet.Enviado = True

                                        If AjEnc.Referencia.Trim = "" Then
                                            AjEnc.Referencia = vNoDocumento
                                        Else
                                            AjEnc.Referencia = AjEnc.Referencia
                                        End If

                                        AjusteDoc.Documento = vNoDocumento
                                        clsLnTrans_ajuste_det_doc.Insertar(AjusteDoc)

                                        clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)

                                        DetallesEnviados += 1

                                    Catch ex As Exception

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                   AjEnc.Idajusteenc,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   BeConfigDet.Idnavconfigdet,
                                                                                   CnnLog)

                                        Actualizar_Progreso(lblprg, "Error en NAV al procesar el ajuste #: " & AjEnc.Idajusteenc)

                                    End Try

                                End If


                            Else

                                '#EJC20180615: Es un tipo de ajuste que no se puede enviar a NAV, pej. cambio de lote
                                BeAjusteDet.Enviado = True
                                clsLnTrans_ajuste_det.Actualizar(BeAjusteDet)
                                DetallesEnviados += 1

                            End If

                            MaxIdAjusteDoc += 1

                        Next

                        vCorrelativoActual = 0

                        If DetallesEnviados = lVistaAjustesPendientesEnvio.Count Then
                            clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(AjEnc.Idajusteenc, True)
                            Actualizar_Progreso(lblprg, "Ajuste: " & AjEnc.Idajusteenc & " procesado correctamente [det = count] :)")
                        Else
                            Actualizar_Progreso(lblprg, String.Format("Incertidumbre de ajuste: " & AjEnc.Idajusteenc & " det = {0} count = {1}", DetallesEnviados, lVistaAjustesPendientesEnvio.Count))
                        End If

                    Else

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc,
                                                                   AjEnc.Idajusteenc,
                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                   BeConfigDet.Idnavconfigdet,
                                                                   CnnLog)

                        Actualizar_Progreso(lblprg, "No hay detalle de ajustes válidos para el Id de ajuste #: " & AjEnc.Idajusteenc)

                    End If

                    prg.Value = vContador : vContador += 1

                Next

            Else
                Actualizar_Progreso(lblprg, "No hay ajustes pendientes de envío.")
            End If

            Actualizar_Progreso(lblprg, "Fin de sincronización de ajustes.")

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "Sync_Ajustes",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       CnnLog)

            Actualizar_Progreso(lblprg, String.Format("Error al enviar ajustes a NAV: {0}{1}", vbNewLine, ex.Message))

            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If Not CnnLog Is Nothing AndAlso CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
        If wsAjusteInventario IsNot Nothing Then
            wsAjusteInventario.Dispose()
        End If
        If wsSeries IsNot Nothing Then
            wsSeries.Dispose()
            wsSeries = Nothing
        End If
    End Sub
#End Region

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Public Shared Sub Actualizar_Progreso_CR(ByRef lblPrg As RichTextBox)
        lblPrg.AppendText(vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String, ByVal CRAntesYDespues As Boolean)
        If CRAntesYDespues Then Actualizar_Progreso_CR(lblPrg)
        lblPrg.AppendText(mensaje & vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
        Actualizar_Progreso_CR(lblPrg)
    End Sub

End Class