Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraPrinting.BarCode.Native
Imports DevExpress.XtraSplashScreen
Imports DevExpress.XtraTab

Public Class frmOrdenCompra

    Private DTS As New DataTable("DetalleRec")
    Private DsReOc As New DSRecOC
    Private lOCDet As New List(Of clsBeTrans_oc_det)
    Private lOCDetLn As New List(Of clsBeTrans_oc_det)
    Private lOCImg As New List(Of clsBeTrans_oc_imagen)
    Private ReadOnly pListObjPR As New List(Of clsProducto)
    Public gBeOrdenCompra As New clsBeTrans_oc_enc
    Public Obj As clsBeTipo_etiqueta
    Public listaOC As New List(Of clsBeTrans_re_enc)
    Public Delegate Sub Listar_Pedidos_Compra()
    Public Delegate Sub Cargar_Orden_Compra()
    Public Property InvokeListarPedidosCompra As Listar_Pedidos_Compra
    Private lProductoKitComposicion As New List(Of clsBeProducto_kit_composicion)
    Public Property InvokeCargarPedidoCompra As Action(Of Integer)
    '#EJC20260602_DELEGADOS_OC_RECEPCION: puente seguro para refrescar listado OC desde recepción hija.
    Private Sub Refrescar_Lista_OC_Desde_Recepcion()
        Try
            If InvokeListarPedidosCompra IsNot Nothing Then
                InvokeListarPedidosCompra.Invoke()
            End If
        Catch ex As Exception
            ' #EJC20260602_DELEGADOS_OC_RECEPCION: best-effort, no bloquear recepcion.
        End Try
    End Sub

    'GT 26012021 gestión del IdTicket para cargar Duca desde TMS_TICKET
    Public gBeTmsTicket_pol As New clsBeTms_ticket_pol
    Public Mostrar_Duca_Tms As Boolean
    Public Estado_Ticket As String

    Private IsClosing As Boolean = False
    Private IsLoading As Boolean = False
    '#EJC20260522_OC_TRACE: traza local de carga de frmOrdenCompra sin sumar roundtrips a la BD.
    Private Const TAG_OC_TRACE As String = "#EJC20260522_OC_TRACE"
    Private Const TAG_OC_UI As String = "#EJC20260522_OC_UI"
    Private Const TAG_OC_PRODUCT_LOOKUP_LAZY As String = "#EJC20260522_OC_PRODUCT_LOOKUP_LAZY"
    Private mOCTraceTotal As System.Diagnostics.Stopwatch
    Private mOCTracePaso As System.Diagnostics.Stopwatch
    Private mOCTraceSesion As String = String.Empty
    Private mOCCargandoDetalle As Boolean = False
    Private ReadOnly mOCProductoLookupCache As New Dictionary(Of String, DataTable)
    Public Property TraceCargaGetSingleMs As Long = 0

    Private BeBodega As New clsBeBodega
    Private BeConfigBodega As New clsBeI_nav_config_enc

    Private vBeProducto As New clsBeProducto

    '#GT10102023: bandera para saber si guardamos por correccion o no
    Dim pCorreccionPoliza As Boolean

    '#GT28052024: bandera para obtener propietario del combo
    Private pIdPropietario As Integer = 0

    '#GT10062024: bandera para saber que hay varios acuerdos activos
    Private pListaAcuerdos As New List(Of clsBeTrans_Acuerdoscomerciales_enc)

    '#GT28052024:uso para servicios asociados a un acuerdo comercial.
    Private ServicioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private DescripcionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private txtFechaServicioGrid As New RepositoryItemDateEdit

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Enum eTipoTrans
        Simple = 1
        Consolidado = 2
    End Enum

    Public Property Modo As ModoTrans
    Public Property TipoTrans As eTipoTrans = 1
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Function OCTrace_Activo() As Boolean

        Dim vConfig As String = Environment.GetEnvironmentVariable("TOMWMS_OC_TRACE")

        If Not String.IsNullOrWhiteSpace(vConfig) Then
            vConfig = vConfig.Trim().ToUpperInvariant()
            If vConfig = "0" OrElse vConfig = "NO" OrElse vConfig = "FALSE" Then Return False
        End If

        Return True

    End Function

    Private Function OCTrace_Ruta() As String

        Return Path.Combine(Path.GetTempPath(), "TOMWMS", "ordencompra-load-trace.log")

    End Function

    Private Sub OCTrace_Iniciar(ByVal pContexto As String)

        If Not OCTrace_Activo() Then Return

        mOCTraceSesion = Guid.NewGuid().ToString("N").Substring(0, 8)
        mOCTraceTotal = System.Diagnostics.Stopwatch.StartNew()
        mOCTracePaso = System.Diagnostics.Stopwatch.StartNew()

        OCTrace_Escribir("START", 0, 0, pContexto)

    End Sub

    Private Sub OCTrace_Marca(ByVal pPaso As String, Optional ByVal pExtra As String = "")

        If Not OCTrace_Activo() Then Return
        If mOCTraceTotal Is Nothing OrElse Not mOCTraceTotal.IsRunning Then Return

        Dim vTotalMs As Long = mOCTraceTotal.ElapsedMilliseconds
        Dim vDeltaMs As Long = 0

        If Not mOCTracePaso Is Nothing Then
            vDeltaMs = mOCTracePaso.ElapsedMilliseconds
            mOCTracePaso.Restart()
        End If

        OCTrace_Escribir(pPaso, vTotalMs, vDeltaMs, pExtra)

    End Sub

    Private Sub OCTrace_Finalizar(Optional ByVal pExtra As String = "")

        If Not OCTrace_Activo() Then Return
        If mOCTraceTotal Is Nothing OrElse Not mOCTraceTotal.IsRunning Then Return

        OCTrace_Marca("END", pExtra)
        mOCTraceTotal.Stop()

    End Sub

    Private Sub OCTrace_Escribir(ByVal pPaso As String, ByVal pTotalMs As Long, ByVal pDeltaMs As Long, ByVal pExtra As String)

        Try
            Dim vRuta As String = OCTrace_Ruta()
            Dim vDir As String = Path.GetDirectoryName(vRuta)
            If Not Directory.Exists(vDir) Then Directory.CreateDirectory(vDir)

            Dim vLinea As String = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}|session={1}|form=frmOrdenCompra|step={2}|totalMs={3}|deltaMs={4}|{5}{6}",
                                                 Date.Now,
                                                 mOCTraceSesion,
                                                 pPaso,
                                                 pTotalMs,
                                                 pDeltaMs,
                                                 pExtra,
                                                 Environment.NewLine)
            File.AppendAllText(vRuta, vLinea)
        Catch
        End Try

    End Sub

    Private Function Carga_Datos_PedidoERP() As Boolean

        Carga_Datos_PedidoERP = False

        Try

            grdDetERP.DataSource = Nothing

            Dim DT As New DataTable

            DT = clsLnI_nav_ped_compra_enc.Get_Detalle_Pedido_Traslado_By_Referencia(gBeOrdenCompra.Referencia)

            If DT.Rows.Count > 0 Then

                grdDetERP.DataSource = DT

                GridView5.OptionsView.ColumnAutoWidth = False
                GridView5.BestFitColumns(True)

                GridView5.OptionsView.ShowFooter = True

                GridView5.Columns("No").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                GridView5.Columns("No").SummaryItem.DisplayFormat = "Registros: {0}"

                GridView5.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
                GridView5.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView5.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView5.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                Carga_Datos_PedidoERP = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Bloquea_Objetos()

        dtmFechaOrdenCompra.ReadOnly = True
        cmbBodega.ReadOnly = True
        lcmbPropietario.ReadOnly = True
        txtIdProveedor.ReadOnly = True
        cmbTipoIngreso.ReadOnly = True
        cmbEstado.ReadOnly = True
        txtNoDocumento.ReadOnly = True
        txtReferencia.ReadOnly = True
        txtProcedencia.ReadOnly = True
        txtObservacion.ReadOnly = True
        txtNombreProveedor.ReadOnly = True
        cmbMotivoDevolucion.ReadOnly = True
        chkControlPoliza.ReadOnly = True
        lnkProveedor.Enabled = False
        chkActivo.Enabled = False
        cbCBM.Enabled = False

    End Sub

    Private Sub Cargar_Datos()

        Try

            OCTrace_Iniciar("idOrdenCompraEnc=" & gBeOrdenCompra.IdOrdenCompraEnc &
                            ";modo=" & Modo &
                            ";getSingleMs=" & TraceCargaGetSingleMs)

            Dim vTraceReloj As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()
            Application.DoEvents()
            OCTrace_Marca("doevents_inicio", "ms=" & vTraceReloj.ElapsedMilliseconds)

            lblC.Text = gBeOrdenCompra.IdOrdenCompraEnc

            dtmFechaOrdenCompra.EditValue = gBeOrdenCompra.Fecha_Creacion

            cmbBodega.EditValue = gBeOrdenCompra.IdBodega
            cmbBodega.Enabled = False

            txtIdProveedor.Tag = gBeOrdenCompra.IdProveedorBodega

            gBeOrdenCompra.ProveedorBodega.Proveedor = clsLnProveedor.Get_Single_By_IdProveedorBodega(gBeOrdenCompra.IdProveedorBodega)

            If gBeOrdenCompra.ProveedorBodega.Proveedor IsNot Nothing Then
                txtIdProveedor.Text = gBeOrdenCompra.ProveedorBodega.Proveedor.IdProveedor
                txtIdProveedor_Validated(Nothing, Nothing)
            End If

            lcmbPropietario.EditValue = gBeOrdenCompra.IdPropietarioBodega

            lcmbPropietario.Enabled = False
            lnkProveedor.Enabled = False
            txtIdProveedor.Enabled = False

            cmbOperadorDefecto.EditValue = gBeOrdenCompra.IdOperadorBodegaDefecto
            cmbOperadorDefecto.Enabled = False

            cmbTipoIngreso.EditValue = gBeOrdenCompra.IdTipoIngresoOC
            cmbTipoIngreso.Enabled = False

            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Application.DoEvents()
            OCTrace_Marca("doevents_encabezado", "ms=" & vTraceReloj.ElapsedMilliseconds)

            'Pedido de transferencia para B&B
            If gBeOrdenCompra.IdTipoIngresoOC = 2 Then
                tabLotes.Visible = True
            Else
                tabLotes.Visible = False
            End If

            cmbEstado.EditValue = gBeOrdenCompra.IdEstadoOC
            cmbEstado.Enabled = False

            If gBeOrdenCompra.IdMotivoDevolucion <> 0 Then
                cmbMotivoDevolucion.Visible = True
                cmbMotivoDevolucion.EditValue = gBeOrdenCompra.IdMotivoDevolucion
                cmbMotivoDevolucion.Enabled = False
            Else
                cmbMotivoDevolucion.Visible = False
            End If

            If gBeOrdenCompra.IdNoDocumentoRef <> 0 Then
                Llena_Documentos_Referencia_Asignado(gBeOrdenCompra.IdNoDocumentoRef)
                cmbDocumentoRef.EditValue = gBeOrdenCompra.IdNoDocumentoRef
            End If

            txtNoTicketTMS.Text = gBeOrdenCompra.No_Ticket_TMS

            If gBeOrdenCompra.No_Ticket_TMS <> String.Empty AndAlso gBeOrdenCompra.No_Ticket_TMS <> "0" Then
                Procesar_No_Ticket_TMS(gBeOrdenCompra.No_Ticket_TMS, False)
            End If

            chkControlPoliza.Checked = gBeOrdenCompra.Control_Poliza
            txtNoDocumento.Text = gBeOrdenCompra.No_Documento
            txtReferencia.Text = gBeOrdenCompra.Referencia
            txtProcedencia.Text = gBeOrdenCompra.Procedencia
            txtObservacion.Text = gBeOrdenCompra.Observacion

            User_agrTextEdit.Text = gBeOrdenCompra.User_Agr
            Fec_agrDateEdit.Text = gBeOrdenCompra.Fec_Agr
            User_modTextEdit.Text = gBeOrdenCompra.User_Mod
            Fec_modDateEdit.Text = gBeOrdenCompra.Fec_Mod

            lOCDet = gBeOrdenCompra.DetalleOC.ToList()

            '#EJC20210404: Igualar ambas listas...
            lOCDetLn = gBeOrdenCompra.DetalleOC.ToList()

            lOCImg = gBeOrdenCompra.ListaImg.ToList()

            cmbOperadorDefecto.EditValue = gBeOrdenCompra.IdOperadorBodegaDefecto

            If BeTipoDocumento.Es_devolucion Then
                If Not pBePedidoEncDevolRef Is Nothing Then
                    txtNombPedido.Text = gBeOrdenCompra.No_Documento_Devolucion
                    txtIdPedidoDevolucionEnc.Text = gBeOrdenCompra.IdPedidoEncDevolucion
                    pBePedidoEncDevolRef = New clsBeTrans_pe_enc()
                    pBePedidoEncDevolRef.Referencia = gBeOrdenCompra.No_Documento_Devolucion
                    pBePedidoEncDevolRef.IdPedidoEnc = gBeOrdenCompra.IdPedidoEncDevolucion
                End If
            End If

            txtDocumentoUbicacion.Text = gBeOrdenCompra.No_Documento_Ubicacion_ERP
            txtNoDocumentoRecepcion.Text = gBeOrdenCompra.No_Documento_Recepcion_ERP
            txtCodigoEmpresaERP.Text = gBeOrdenCompra.Codigo_Empresa_ERP
            txtIdCampaña.Text = gBeOrdenCompra.IdCampaña
            txtIdCampaña.Enabled = False : txtNomCampaña.Enabled = False

            Set_Campaña()

            OCTrace_Marca("antes_detalle", "lineas=" & If(gBeOrdenCompra.DetalleOC Is Nothing, 0, gBeOrdenCompra.DetalleOC.Count))
            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Cargar_Detalle_OC()
            OCTrace_Marca("detalle_oc", "ms=" & vTraceReloj.ElapsedMilliseconds & ";rows=" & DTGridDetalleDocIngresos.Rows.Count)
            OCProductoLookup_ResolverDisplayDesdeDetalle()

            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Cargar_Detalle_Lotes_OC()
            OCTrace_Marca("detalle_lotes", "ms=" & vTraceReloj.ElapsedMilliseconds)

            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Cargar_Imagenes()
            OCTrace_Marca("imagenes", "ms=" & vTraceReloj.ElapsedMilliseconds & ";imagenes=" & If(lOCImg Is Nothing, 0, lOCImg.Count))

            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Cargar_Poliza()
            OCTrace_Marca("poliza", "ms=" & vTraceReloj.ElapsedMilliseconds)

            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            Cargar_Detalle_Rec()
            OCTrace_Marca("detalle_rec", "ms=" & vTraceReloj.ElapsedMilliseconds)

            Set_Estado_Envio_A_ERP()

            If gBeOrdenCompra.IdCampaña = 0 Then
                tabTallaColor.Visible = False
                tabTallaColor.PageVisible = False
            Else

                vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                Cargar_Talla_Color(gBeOrdenCompra.IdCampaña)
                OCTrace_Marca("talla_color", "ms=" & vTraceReloj.ElapsedMilliseconds)

            End If

            '#EJC20210707:Activar o desactivar botón para crear tarea en documento de ingreso.
            If (gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA OrElse gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.BACK_ORDER) AndAlso
                Not clsLnTrans_re_enc.OC_Tiene_Recepciones_Activas(gBeOrdenCompra.IdOrdenCompraEnc) Then
                mnuTareaRecepcion.Enabled = True
                txtIdRecepcion.Text = "0"
            Else
                mnuTareaRecepcion.Enabled = False
                txtIdRecepcion.Text = clsLnTrans_re_enc.Get_Recepcion_Activa_By_IdOrdenCompraEnc(gBeOrdenCompra.IdOrdenCompraEnc)
            End If

            OCTrace_Finalizar("rows=" & DTGridDetalleDocIngresos.Rows.Count)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Set_Campaña()

        Try

            Dim BeCampaña As New clsBeCampaña
            If gBeOrdenCompra.IdCampaña > 0 Then
                BeCampaña = clsLnCampaña.Get_Single_By_IdCampaña(gBeOrdenCompra.IdCampaña)
                txtNomCampaña.Text = BeCampaña.Nombre
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub Set_Estado_Envio_A_ERP()

        Try

            If gBeOrdenCompra.Enviado_A_ERP Then
                mnuEstadoEnviadoAERP.Caption = "Enviado"
                mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.green_ball
            Else
                mnuEstadoEnviadoAERP.Caption = "No Enviado"
                mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.red_ball
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.UsuarioAp.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub Cargar_Detalle_OC()

        Dim vGridEnUpdate As Boolean = False
        Dim vDataEnCarga As Boolean = False

        Try

            '#EJC20260522_OC_UI: evita validaciones/repintado mientras se cargan filas programaticamente.
            mOCCargandoDetalle = True
            gvDetalleDocIngreso.BeginDataUpdate()
            vGridEnUpdate = True
            DTGridDetalleDocIngresos.BeginLoadData()
            vDataEnCarga = True

            DTGridDetalleDocIngresos.Clear()

            lOCDet = gBeOrdenCompra.DetalleOC

            Dim i As Integer = -1

            For Each BeTransOCDet As clsBeTrans_oc_det In lOCDet.OrderBy(Function(x) x.No_Linea)

                Dim vCantidadPendiente As Double = Math.Round(BeTransOCDet.Cantidad_recibida - BeTransOCDet.Cantidad, 6)
                Dim vTalla As String = ""
                Dim vColor As String = ""
                Dim vSKU As String = ""
                Dim vCodigoProducto As String = BeTransOCDet.Codigo_Producto

                If (BeTransOCDet.IdProductoTallaColor <> 0) Then

                    vTalla = BeTransOCDet.Talla.IdTalla
                    vColor = BeTransOCDet.Color.IdColor
                    vSKU = BeTransOCDet.Codigo_Producto

                End If

                ' #EJC20260602_OC_FIX_CODIGO: fallback para documentos viejos/mixtos donde el código no vino en detalle.
                If String.IsNullOrWhiteSpace(vCodigoProducto) AndAlso BeTransOCDet.IdProductoBodega > 0 Then
                    vCodigoProducto = clsLnProducto.Get_Codigo_By_IdProductoBodega(BeTransOCDet.IdProductoBodega)
                End If

                Dim commonData As Object() = {
                    BeTransOCDet.IdPropietarioBodega,
                BeTransOCDet.Nombre_Propietario,
                BeTransOCDet.No_Linea,
                BeTransOCDet.IdProductoBodega,
                vCodigoProducto,
                BeTransOCDet.Nombre_producto,
                BeTransOCDet.Nombre_unidad_medida_basica,
                BeTransOCDet.IdUnidadMedidaBasica,
                BeTransOCDet.IdPresentacion,
                BeTransOCDet.Arancel.IdArancel,
                BeTransOCDet.IdMotivoDevolucion,
                BeTransOCDet.Cantidad,
                BeTransOCDet.Cantidad_recibida,
                vCantidadPendiente,
                BeTransOCDet.Peso_Bruto,
                BeTransOCDet.Peso_Neto,
                BeTransOCDet.Costo,
                BeTransOCDet.valor_aduana,
                BeTransOCDet.valor_fob,
                BeTransOCDet.valor_iva,
                BeTransOCDet.valor_dai,
                BeTransOCDet.valor_seguro,
                BeTransOCDet.valor_flete,
                BeTransOCDet.Total_linea,
                BeTransOCDet.Producto.IdProducto,
                BeTransOCDet.IsNew,
                BeTransOCDet.IdOrdenCompraEnc,
                BeTransOCDet.IdOrdenCompraDet,
                False,
                BeTransOCDet.Atributo_variante_1,
                BeTransOCDet.Producto.Kit,
                BeTransOCDet.IdPedidoCompraDet,
                BeTransOCDet.IdOrdenCompraDetPadre,
                BeTransOCDet.Producto.Control_peso,
                BeTransOCDet.Producto.Peso_referencia,
                BeTransOCDet.Nombre_Embarcador,
                    BeTransOCDet.Producto.Clasificacion.Nombre
                }

                Dim finalData As Object()
                If BeBodega.Control_Talla_Color Then
                    finalData = commonData.Concat({vTalla, vColor, vSKU}).ToArray()
                Else
                    finalData = commonData
                End If

                DTGridDetalleDocIngresos.Rows.Add(finalData)

                If BeTransOCDet.lProductosHijosKit.Count > 0 Then

                    For Each Hijo As clsBeTrans_oc_det In BeTransOCDet.lProductosHijosKit

                        vCantidadPendiente = Math.Round(Hijo.Cantidad_recibida - Hijo.Cantidad, 6)
                        Dim vCodigoProductoHijo As String = Hijo.Codigo_Producto
                        If String.IsNullOrWhiteSpace(vCodigoProductoHijo) AndAlso Hijo.IdProductoBodega > 0 Then
                            vCodigoProductoHijo = clsLnProducto.Get_Codigo_By_IdProductoBodega(Hijo.IdProductoBodega)
                        End If

                        DTGridDetalleDocIngresos.Rows.Add(Hijo.IdPropietarioBodega,
                                                          Hijo.Nombre_Propietario,
                                                          Hijo.No_Linea,
                                                          Hijo.IdProductoBodega,
                                                          vCodigoProductoHijo,
                                                          Hijo.Nombre_producto,
                                                          Hijo.Nombre_unidad_medida_basica,
                                                          Hijo.IdUnidadMedidaBasica,
                                                          Hijo.IdPresentacion,
                                                          Hijo.Arancel.IdArancel,
                                                          Hijo.IdMotivoDevolucion,
                                                          Hijo.Cantidad,
                                                          Hijo.Cantidad_recibida,
                                                          vCantidadPendiente,
                                                          Hijo.Peso_Bruto,
                                                          Hijo.Peso_Neto,
                                                          Hijo.Costo,
                                                          Hijo.valor_aduana,
                                                          Hijo.valor_fob,
                                                          Hijo.valor_iva,
                                                          Hijo.valor_dai,
                                                          Hijo.valor_seguro,
                                                          Hijo.valor_flete,
                                                          Hijo.Total_linea,
                                                          Hijo.Producto.IdProducto,
                                                          Hijo.IsNew,
                                                          Hijo.IdOrdenCompraEnc,
                                                          Hijo.IdOrdenCompraDet,
                                                          False,
                                                          Hijo.Atributo_variante_1,
                                                          Hijo.Producto.Kit,
                                                          Hijo.IdPedidoCompraDet,
                                                          Hijo.IdOrdenCompraDetPadre,
                                                          Hijo.Producto.Control_peso,
                                                          Hijo.Producto.Peso_referencia)
                    Next

                End If

            Next

            gvDetalleDocIngreso.BestFitColumns()

            Set_LayOut_Grid_Detalle_Documento_Ingreso()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            If vDataEnCarga Then DTGridDetalleDocIngresos.EndLoadData()
            If vGridEnUpdate Then gvDetalleDocIngreso.EndDataUpdate()
            mOCCargandoDetalle = False
        End Try

    End Sub

    Private Sub Cargar_Detalle_OC_HH()

        Try
            'GT Limpia el grid, porque al refrescar, duplica los valores
            DTGridDetalleDocIngresos.Clear()
            gBeOrdenCompra.DetalleOC.Clear()


            gBeOrdenCompra.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(gBeOrdenCompra.IdOrdenCompraEnc)
            lOCDet = gBeOrdenCompra.DetalleOC

            Dim i As Integer = -1

            For Each BeTransOcDet As clsBeTrans_oc_det In lOCDet

                Dim vCantidadPendiente As Double = Math.Round(BeTransOcDet.Cantidad_recibida - BeTransOcDet.Cantidad, 6)

                DTGridDetalleDocIngresos.Rows.Add(BeTransOcDet.IdPropietarioBodega,
                                                   BeTransOcDet.Nombre_Propietario,
                                                   BeTransOcDet.No_Linea,
                                                   BeTransOcDet.IdProductoBodega,
                                                   BeTransOcDet.Codigo_Producto,
                                                   BeTransOcDet.Nombre_producto,
                                                   BeTransOcDet.Nombre_unidad_medida_basica,
                                                   BeTransOcDet.IdUnidadMedidaBasica,
                                                   BeTransOcDet.IdPresentacion,
                                                   BeTransOcDet.Arancel.IdArancel,
                                                   BeTransOcDet.IdMotivoDevolucion,
                                                   BeTransOcDet.Cantidad,
                                                   BeTransOcDet.Cantidad_recibida,
                                                   vCantidadPendiente,
                                                   BeTransOcDet.Peso_Bruto,
                                                   BeTransOcDet.Peso_Neto,
                                                   BeTransOcDet.Costo,
                                                   BeTransOcDet.valor_aduana,
                                                   BeTransOcDet.valor_fob,
                                                   BeTransOcDet.valor_iva,
                                                   BeTransOcDet.valor_dai,
                                                   BeTransOcDet.valor_seguro,
                                                   BeTransOcDet.valor_flete,
                                                   BeTransOcDet.Total_linea,
                                                   BeTransOcDet.Producto.IdProducto,
                                                   BeTransOcDet.IsNew,
                                                   BeTransOcDet.IdOrdenCompraEnc,
                                                   BeTransOcDet.IdOrdenCompraDet,
                                                   False,
                                                   BeTransOcDet.Atributo_variante_1,
                                                   BeTransOcDet.Producto.Kit,
                                                   BeTransOcDet.IdPedidoCompraDet,
                                                   BeTransOcDet.IdOrdenCompraDetPadre,
                                                   BeTransOcDet.Producto.Control_peso,
                                                   BeTransOcDet.Producto.Peso_referencia,
                                                   BeTransOcDet.Nombre_Embarcador,
                                                   BeTransOcDet.Producto.Clasificacion.Nombre)

                'GT0903: agregue embarcador y clasificacion

                If BeTransOcDet.lProductosHijosKit.Count > 0 Then

                    For Each Hijo As clsBeTrans_oc_det In BeTransOcDet.lProductosHijosKit

                        vCantidadPendiente = Math.Round(Hijo.Cantidad_recibida - Hijo.Cantidad, 6)

                        DTGridDetalleDocIngresos.Rows.Add(Hijo.IdPropietarioBodega,
                                                          Hijo.Nombre_Propietario,
                                                          Hijo.No_Linea,
                                                          Hijo.IdProductoBodega,
                                                          Hijo.Codigo_Producto,
                                                          Hijo.Nombre_producto,
                                                          Hijo.Nombre_unidad_medida_basica,
                                                          Hijo.IdUnidadMedidaBasica,
                                                          Hijo.IdPresentacion,
                                                          Hijo.Arancel.IdArancel,
                                                          Hijo.IdMotivoDevolucion,
                                                          Hijo.Cantidad,
                                                          Hijo.Cantidad_recibida,
                                                          vCantidadPendiente,
                                                          Hijo.Peso_Bruto,
                                                          Hijo.Peso_Neto,
                                                          Hijo.Costo,
                                                          Hijo.valor_aduana,
                                                          Hijo.valor_fob,
                                                          Hijo.valor_iva,
                                                          Hijo.valor_dai,
                                                          Hijo.valor_seguro,
                                                          Hijo.valor_flete,
                                                          Hijo.Total_linea,
                                                          Hijo.Producto.IdProducto,
                                                          Hijo.IsNew,
                                                          Hijo.IdOrdenCompraEnc,
                                                          Hijo.IdOrdenCompraDet,
                                                          False,
                                                          Hijo.Atributo_variante_1,
                                                          Hijo.Producto.Kit,
                                                          Hijo.IdPedidoCompraDet,
                                                          Hijo.IdOrdenCompraDetPadre,
                                                          Hijo.Producto.Control_peso,
                                                          Hijo.Producto.Peso_referencia)
                    Next

                End If

            Next

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try



    End Sub

    Private Sub Cargar_Detalle_Lotes_OC()

        Try

            Dim DT As New Object
            'GT21022022: agrego el tolist() porque el objeto en si no setea al datasource
            DT = (From datos In gBeOrdenCompra.DetalleLotes Select datos.No_linea,
                                                                   datos.Codigo_producto,
                                                                   Cantidad_UMBAS = datos.Cantidad,
                                                                   Cantidad_Recibida_UMBAS = datos.Cantidad_recibida,
                                                                   Unidad_Medida = datos.UnidadMedida.Nombre,
                                                                   Cantidad_Presentacion = IIf(datos.IdPresentacion <> 0, datos.Cantidad / datos.Presentacion.Factor, 0),
                                                                   Cantidad_Recibida_Presentacion = IIf(datos.IdPresentacion <> 0, datos.Cantidad_recibida / datos.Presentacion.Factor, 0),
                                                                   datos.Peso_Licencia,
                                                                   Presentacion = datos.Presentacion.Nombre,
                                                                   datos.Presentacion.Factor,
                                                                   datos.Lote,
                                                                   Licencia = datos.Lic_Plate,
                                                                   datos.Fecha_vence,
                                                                   datos.Ubicacion,
                                                                   datos.IdUnidadMedidaBasica,
                                                                   datos.IdPresentacion,
                                                                   datos.Reclasificar,
                                                                   datos.IdOrdenCompraEnc,
                                                                   datos.IdOrdenCompraDet,
                                                                   datos.IdOrdenCompraDetLote,
                                                                   datos.IdProductoBodega).ToList()



            DgridLotes.DataSource = DT

            If gridviewLotes.Columns.Count > 0 Then
                gridviewLotes.Columns("IdOrdenCompraEnc").Visible = False
                gridviewLotes.Columns("IdOrdenCompraDet").Visible = False
                gridviewLotes.Columns("IdOrdenCompraDetLote").Visible = False
                gridviewLotes.Columns("IdProductoBodega").Visible = False

                ' Cantidades
                gridviewLotes.Columns("Cantidad_UMBAS").DisplayFormat.FormatType = FormatType.Numeric
                gridviewLotes.Columns("Cantidad_UMBAS").DisplayFormat.FormatString = "{0:n6}"
                gridviewLotes.Columns("Cantidad_UMBAS").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridviewLotes.Columns("Cantidad_UMBAS").SummaryItem.DisplayFormat = "Total: {0:n6}"

                gridviewLotes.Columns("Cantidad_Recibida_UMBAS").Visible = True
                gridviewLotes.Columns("Cantidad_Recibida_UMBAS").DisplayFormat.FormatType = FormatType.Numeric
                gridviewLotes.Columns("Cantidad_Recibida_UMBAS").DisplayFormat.FormatString = "{0:n6}"
                gridviewLotes.Columns("Cantidad_Recibida_UMBAS").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridviewLotes.Columns("Cantidad_Recibida_UMBAS").SummaryItem.DisplayFormat = "Total: {0:n6}"

                gridviewLotes.Columns("Cantidad_Presentacion").DisplayFormat.FormatType = FormatType.Numeric
                gridviewLotes.Columns("Cantidad_Presentacion").DisplayFormat.FormatString = "{0:n6}"
                gridviewLotes.Columns("Cantidad_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridviewLotes.Columns("Cantidad_Presentacion").SummaryItem.DisplayFormat = "Total: {0:n6}"

                gridviewLotes.Columns("Cantidad_Recibida_Presentacion").DisplayFormat.FormatType = FormatType.Numeric
                gridviewLotes.Columns("Cantidad_Recibida_Presentacion").DisplayFormat.FormatString = "{0:n6}"
                gridviewLotes.Columns("Cantidad_Recibida_Presentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridviewLotes.Columns("Cantidad_Recibida_Presentacion").SummaryItem.DisplayFormat = "Total: {0:n6}"

                ' Factor
                gridviewLotes.Columns("Factor").DisplayFormat.FormatType = FormatType.Numeric
                gridviewLotes.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                ' Peso
                If gridviewLotes.Columns.ColumnByFieldName("peso_licencia") IsNot Nothing Then
                    gridviewLotes.Columns("peso_licencia").DisplayFormat.FormatType = FormatType.Numeric
                    gridviewLotes.Columns("peso_licencia").DisplayFormat.FormatString = "{0:n2}"
                    gridviewLotes.Columns("peso_licencia").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    gridviewLotes.Columns("peso_licencia").SummaryItem.DisplayFormat = "Total: {0:n2}"
                End If
            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Poliza()

        Try

            If Mostrar_Duca_Tms Then

                Dim BeRegimen As New clsBeRegimen_fiscal

                If gBeTmsTicket_pol IsNot Nothing Then

                    'GT 14022021 con el idregimen se obtiene el codigo_regimen para setear el cb
                    BeRegimen = clsLnRegimen_fiscal.GetSingle_By_IdRegimen(gBeTmsTicket_pol.IdRegimen)
                    '#EJC20210222: Validar que no sea nothing el objeto
                    If Not BeRegimen Is Nothing Then
                        cmbRegimen.EditValue = BeRegimen.Codigo_regimen
                    End If

                    '#GT25032022_1220: set de la TO, que existe en la OC pero no en el ticket
                    If Not gBeOrdenCompra.ObjPoliza Is Nothing Then
                        'txtNoPoliza.Text = gBeTmsTicket_pol.NoPoliza
                        txtNoPoliza.Text = gBeOrdenCompra.ObjPoliza.NoPoliza
                    End If

                    txtCodigoPoliza.Text = gBeTmsTicket_pol.NoPoliza
                    txtPaisProcedencia.Text = gBeTmsTicket_pol.Pais_procede
                    txtValorAduana.Value = gBeTmsTicket_pol.Total_valoraduana
                    txtValorFlete.Value = gBeTmsTicket_pol.Total_flete
                    txtTotalFOBUSD.Value = gBeTmsTicket_pol.Total_usd
                    txtNumeroDUA.Text = gBeOrdenCompra.ObjPoliza.Dua
                    dtFechaPoliza.EditValue = gBeTmsTicket_pol.Fecha_poliza
                    txtTipoCambio.Value = gBeTmsTicket_pol.Tipo_cambio
                    txtValorSeguro.Value = gBeTmsTicket_pol.Total_seguro
                    txtNumeroOrden.Text = gBeTmsTicket_pol.Dua
                    txtClaveAduana.Text = gBeTmsTicket_pol.Clave_aduana
                    txtNitImpExp.Text = gBeTmsTicket_pol.Nit_imp_exp
                    txtClase.Text = gBeTmsTicket_pol.Clase
                    txtMod_transporte.Text = gBeTmsTicket_pol.Mod_transporte
                    txtTotal_liquidar.Text = gBeTmsTicket_pol.Total_liquidar
                    txtTotal_general.Text = gBeTmsTicket_pol.Total_general

                ElseIf gBeOrdenCompra.ObjPoliza IsNot Nothing Then

                    BeRegimen = clsLnRegimen_fiscal.GetSingle_By_IdRegimen(gBeOrdenCompra.ObjPoliza.IdRegimen)

                    '#EJC20210222: Validar que no sea nothing el objeto
                    If Not BeRegimen Is Nothing Then
                        cmbRegimen.EditValue = BeRegimen.Codigo_regimen
                    End If

                    BLNo.Text = gBeOrdenCompra.ObjPoliza.Bl_No
                    txtPuertaDescarga.Text = gBeOrdenCompra.ObjPoliza.Pto_Descarga
                    txtRemitente.Text = gBeOrdenCompra.ObjPoliza.Remitente
                    dtFechaAbordaje.EditValue = gBeOrdenCompra.ObjPoliza.Fecha_abordaje
                    Descripcion.Text = gBeOrdenCompra.ObjPoliza.Descripcion
                    txtCantidad.Value = gBeOrdenCompra.ObjPoliza.Cantidad
                    txtPesoKgs.Value = gBeOrdenCompra.ObjPoliza.Total_kgs
                    txtViaje.Text = gBeOrdenCompra.ObjPoliza.Viaje_no
                    txtBuque.Text = gBeOrdenCompra.ObjPoliza.Buque_no
                    txtDestinatario.Text = gBeOrdenCompra.ObjPoliza.Destino
                    txtDireccion.Text = gBeOrdenCompra.ObjPoliza.Dir_destino
                    txtPONumber.Text = gBeOrdenCompra.ObjPoliza.Po_number
                    txtPiezas.Value = gBeOrdenCompra.ObjPoliza.Piezas

                    ' POLIZA
                    txtNoPoliza.Text = gBeOrdenCompra.ObjPoliza.NoPoliza
                    txtCodigoPoliza.Text = gBeOrdenCompra.ObjPoliza.codigo_poliza
                    txtPaisProcedencia.Text = gBeOrdenCompra.ObjPoliza.Pais_procede
                    txtValorAduana.Value = gBeOrdenCompra.ObjPoliza.Total_valoraduana
                    txtValorFlete.Value = gBeOrdenCompra.ObjPoliza.Total_flete
                    txtTotalFOBUSD.Value = gBeOrdenCompra.ObjPoliza.Total_usd
                    txtNumeroDUA.Text = gBeOrdenCompra.ObjPoliza.Dua
                    dtFechaPoliza.EditValue = gBeOrdenCompra.ObjPoliza.Fecha_poliza
                    'GT29112021: se agregan sets para los inputs de fecha_llegada y fecha_aceptacion
                    dtpFechaAceptacion.EditValue = gBeOrdenCompra.ObjPoliza.fecha_aceptacion
                    dtpFechaLlegada.EditValue = gBeOrdenCompra.ObjPoliza.fecha_llegada
                    txtTipoCambio.Value = gBeOrdenCompra.ObjPoliza.Tipo_cambio
                    txtValorSeguro.Value = gBeOrdenCompra.ObjPoliza.Total_seguro
                    txtNumeroOrden.Text = gBeOrdenCompra.ObjPoliza.numero_orden
                    txtClaveAduana.Text = gBeOrdenCompra.ObjPoliza.clave_aduana
                    txtNitImpExp.Text = gBeOrdenCompra.ObjPoliza.nit_imp_exp
                    txtClase.Text = gBeOrdenCompra.ObjPoliza.clase
                    txtMod_transporte.Text = gBeOrdenCompra.ObjPoliza.mod_transporte
                    txtTotal_liquidar.Text = gBeOrdenCompra.ObjPoliza.total_liquidar
                    txtTotal_general.Text = gBeOrdenCompra.ObjPoliza.total_general
                    txtScanPoliza.Text = gBeOrdenCompra.ObjPoliza.Codigo_Barra
                    cbCBM.Value = gBeOrdenCompra.ObjPoliza.Cbm

                    '#GT12062024: campos para validar peso bruto y neto
                    txtTotalPesoBruto.Value = gBeOrdenCompra.ObjPoliza.Total_bultos_Peso_Bruto
                    txtTotalPesoNeto.Value = gBeOrdenCompra.ObjPoliza.Total_bultos_Peso_Neto

                    If gBeOrdenCompra.ObjPoliza.Codigo_Barra.Length > 0 Then
                        txtScanPoliza.ReadOnly = True
                    End If

                End If

            End If

            Bloquear_Inputs_Poliza()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            Dim vDetalleCorrecto As Boolean = False

            If Modo = ModoTrans.Nuevo Then
                vDetalleCorrecto = (lOCDetLn.Count <> 0 OrElse gvDetalleDocIngreso.RowCount <> 0)
            Else
                vDetalleCorrecto = (lOCDet.Count <> 0)
            End If

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbBodega.Focus()
            ElseIf String.IsNullOrEmpty(txtIdProveedor.Text) Then
                XtraMessageBox.Show("Seleccione Proveedor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf chkControlPoliza.Checked And txtNumeroDUA.Text = String.Empty And txtCodigoPoliza.Text = String.Empty Then
                XtraMessageBox.Show("Faltan los datos de la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not vDetalleCorrecto Then
                XtraMessageBox.Show("El documento parece no tener líneas válidas.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Grid_Tiene_Error Then
                XtraMessageBox.Show("El detalle del documento contiene errores, debe corregirlos antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf BeTipoDocumento.Exigir_Campo_Referencia AndAlso txtReferencia.Text.Trim = String.Empty Then
                XtraMessageBox.Show("El campo referencia es obligatorio para el tipo de documento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtReferencia.Focus()
            ElseIf IsNumeric(txtIdProveedor.Text) Then

                Dim o As New clsBeProveedor
                o = clsLnProveedor.GetSingle(txtIdProveedor.Text)

                If o Is Nothing Then
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show(String.Format("El Id {0} del Proveedor no existe.", txtIdProveedor.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Datos_Correctos = True
                End If

            Else

                Datos_Correctos = True

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            mnuGuardar.Enabled = False

            Grabar()

            mnuGuardar.Enabled = True

        Catch ex As Exception
            mnuGuardar.Enabled = True
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Grabar()

        If Datos_Correctos() Then

            SplashScreenManager.CloseForm(False)

            If XtraMessageBox.Show("¿Guardar Documento de Ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                If Guardar(False) Then

                    SplashScreenManager.CloseForm(False)

                    Dim GeneraRec As Boolean = True

                    If BeTipoDocumento.Requerir_Documento_Ref Then
                        If (gBeOrdenCompra.IdNoDocumentoRef = 0 AndAlso gBeOrdenCompra.IdPedidoEncDevolucion = 0 AndAlso gBeOrdenCompra.No_Documento_Devolucion = String.Empty) Then
                            GeneraRec = False
                        End If
                    End If

                    If GeneraRec Then

                        If cmbOperadorDefecto.EditValue Is Nothing Then

                            If XtraMessageBox.Show("Se guardó el documento. ¿Desea crear la Recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                If NuevaRecepcion(0) Then
                                    InvokeListarPedidosCompra.Invoke()
                                    DialogResult = DialogResult.OK
                                    Close()
                                Else
                                    InvokeListarPedidosCompra.Invoke()
                                    Close()
                                End If
                            Else
                                InvokeListarPedidosCompra.Invoke()
                                Close()
                            End If

                        Else
                            If NuevaRecepcion(cmbOperadorDefecto.EditValue) Then
                                InvokeListarPedidosCompra.Invoke()
                                DialogResult = DialogResult.OK
                                Close()
                            Else
                                InvokeListarPedidosCompra.Invoke()
                                Close()
                            End If
                        End If

                    Else
                        XtraMessageBox.Show("Se guardó el documento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                        Close()
                    End If

                End If

            End If

        End If

    End Sub

    Private Function NuevaRecepcion(ByVal IdOperadorDefecto As Integer) As Boolean

        NuevaRecepcion = False

        Try

            Dim Rec As New frmRecepcion(frmRecepcion.TipoTrans.Nuevo) With
                {.pRecepcionInmediata = True,
                .pIdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc,
                .IdBodega = cmbBodega.EditValue,
                .pIdPropietarioBodega = lcmbPropietario.EditValue,
                .IdOperadorBodegaDefecto = IdOperadorDefecto,
                .Listar = AddressOf Refrescar_Lista_OC_Desde_Recepcion}

            '#EJC20210613: Compartir el objeto a priori.
            Rec.gBeOrdenCompra = gBeOrdenCompra.Clone()
            Rec.txtNoDocumento.Text = txtNoDocumento.Text
            '#EJC202405090304AM: No embarque Cumbre.
            Rec.txtNoContenedor.Text = txtNoDocumentoRecepcion.Text

            '#EJC20220330: Wrap of the parameter..
            Rec.chkEscanearUbicacionRec.Checked = BeTipoDocumento.Requerir_Ubic_Rec_Ingreso

            If Rec.ShowDialog() = DialogResult.OK Then
                InvokeListarPedidosCompra.Invoke()
                NuevaRecepcion = True
            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Guardar(ByVal EsActualizacion As Boolean) As Boolean

        Guardar = False

        Try

            Dim vContador As Integer = 0

            If gBeOrdenCompra Is Nothing Then
                gBeOrdenCompra = New clsBeTrans_oc_enc() With {.IsNew = True}
            End If

            vContador += 1

            If gBeOrdenCompra.IsNew Then

                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega

                If Not lcmbPropietario.EditValue Is Nothing Then
                    gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = lcmbPropietario.EditValue
                End If

                gBeOrdenCompra.IdEstadoOC = cmbEstado.EditValue
                gBeOrdenCompra.Hora_Creacion = Now
                gBeOrdenCompra.User_Agr = AP.UsuarioAp.IdUsuario
                gBeOrdenCompra.Fec_Agr = Now
            End If

            gBeOrdenCompra.Fecha_Creacion = dtmFechaOrdenCompra.EditValue
            gBeOrdenCompra.Activo = chkActivo.Checked

            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega
            End If

            '1
            vContador += 1

            gBeOrdenCompra.IdProveedorBodega = Val(txtIdProveedor.Tag)
            gBeOrdenCompra.IdTipoIngresoOC = cmbTipoIngreso.EditValue
            gBeOrdenCompra.IdAcuerdoComercial = cmbAcuerdoComercial.EditValue
            gBeOrdenCompra.No_Documento = txtNoDocumento.Text.Trim
            gBeOrdenCompra.User_Mod = AP.UsuarioAp.IdUsuario
            gBeOrdenCompra.Fec_Mod = Now
            gBeOrdenCompra.Procedencia = txtProcedencia.Text.Trim
            gBeOrdenCompra.Referencia = txtReferencia.Text.Trim
            gBeOrdenCompra.Observacion = txtObservacion.Text.Trim
            gBeOrdenCompra.Comentarios = txtComentarios.Text.Trim
            gBeOrdenCompra.Usr_Documento = txtUsuarioERP.Text.Trim
            gBeOrdenCompra.Control_Poliza = chkControlPoliza.Checked
            gBeOrdenCompra.No_Ticket_TMS = txtNoTicketTMS.EditValue

            If BeTipoDocumento.Requerir_Documento_Ref Then
                If Not cmbDocumentoRef.EditValue Is Nothing Then
                    gBeOrdenCompra.IdNoDocumentoRef = cmbDocumentoRef.EditValue
                End If
            End If

            If BeTipoDocumento.Es_devolucion Then
                If Not pBePedidoEncDevolRef Is Nothing Then
                    gBeOrdenCompra.No_Documento_Devolucion = pBePedidoEncDevolRef.Referencia
                    gBeOrdenCompra.IdPedidoEncDevolucion = pBePedidoEncDevolRef.IdPedidoEnc
                End If
            End If

            '2
            vContador += 1

            'GT 28012021: Si no se valida poliza, se envia ObjPoliza con valores vacios
            If chkControlPoliza.Checked Then

                'GT 08022021 se obtiene el IdRegimen del combo
                Dim fila As Object = cmbRegimen.GetSelectedDataRow
                Dim bePolizaAux As clsBeTrans_oc_pol = Nothing
                Dim IdRegimen_ As Integer

                'GT08022022
                If fila Is Nothing Then
                    Throw New Exception("Error_20220208_1204: el régimen en la póliza no es valido.")
                Else
                    IdRegimen_ = fila.Item("IdRegimen")
                End If

                'GT11022022: revisamos si, la OC ya tenia registrada la poliza, para no leerla de los inputs
                bePolizaAux = clsLnTrans_oc_pol.GetSingle(gBeOrdenCompra.IdOrdenCompraEnc)

                If Not bePolizaAux Is Nothing Then

                    gBeOrdenCompra.ObjPoliza = bePolizaAux
                    '#EJC20220222: Actualizar el número de T.O.
                    gBeOrdenCompra.ObjPoliza.NoPoliza = txtNoPoliza.Text.Trim
                    gBeOrdenCompra.ObjPoliza.IsNew = False
                    '#GT27092023: este valor si actualiza cuando no es nueva la póliza
                    gBeOrdenCompra.ObjPoliza.Cbm = cbCBM.Value

                Else

                    gBeOrdenCompra.ObjPoliza = New clsBeTrans_oc_pol
                    gBeOrdenCompra.ObjPoliza.User_agr = AP.UsuarioAp.IdUsuario
                    gBeOrdenCompra.ObjPoliza.Fec_agr = Now
                    'Embarque
                    gBeOrdenCompra.ObjPoliza.Codigo_Barra = txtScanPoliza.Text
                    gBeOrdenCompra.ObjPoliza.Bl_No = BLNo.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Pto_Descarga = txtPuertaDescarga.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Remitente = txtRemitente.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Fecha_abordaje = dtFechaAbordaje.EditValue
                    gBeOrdenCompra.ObjPoliza.Descripcion = Descripcion.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Cantidad = CInt(txtCantidad.Value)
                    gBeOrdenCompra.ObjPoliza.Total_kgs = txtPesoKgs.Value
                    gBeOrdenCompra.ObjPoliza.Viaje_no = txtViaje.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Buque_no = txtBuque.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Destino = txtDestinatario.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Dir_destino = txtDireccion.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Po_number = txtPONumber.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Piezas = CInt(txtPiezas.Value)
                    'gBeOrdenCompra.ObjPoliza.Cbm = txtCBM.Value
                    gBeOrdenCompra.ObjPoliza.Cbm = cbCBM.Value
                    'GT 23012021 obtener el id del regimen en lugar del código regimen
                    gBeOrdenCompra.ObjPoliza.IdRegimen = IdRegimen_
                    gBeOrdenCompra.ObjPoliza.NoPoliza = txtNoPoliza.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Pais_procede = txtPaisProcedencia.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Total_valoraduana = txtValorAduana.Value
                    gBeOrdenCompra.ObjPoliza.Total_bultos_Peso_Bruto = txtTotalPesoBruto.Value
                    gBeOrdenCompra.ObjPoliza.Total_bultos_Peso_Neto = txtTotalPesoNeto.Value
                    gBeOrdenCompra.ObjPoliza.Total_flete = txtValorFlete.Value
                    gBeOrdenCompra.ObjPoliza.Total_usd = txtTotalFOBUSD.Value
                    gBeOrdenCompra.ObjPoliza.Dua = txtNumeroDUA.Text.Trim
                    gBeOrdenCompra.ObjPoliza.Fecha_poliza = dtFechaPoliza.EditValue
                    gBeOrdenCompra.ObjPoliza.Tipo_cambio = txtTipoCambio.Value
                    gBeOrdenCompra.ObjPoliza.Total_lineas = CInt(txtTotalLineas.Value)
                    gBeOrdenCompra.ObjPoliza.Total_bultos = CInt(txtTotalBulto.Value)
                    gBeOrdenCompra.ObjPoliza.Total_seguro = txtValorSeguro.Value
                    gBeOrdenCompra.ObjPoliza.User_mod = AP.UsuarioAp.IdUsuario
                    gBeOrdenCompra.ObjPoliza.Fec_mod = Now
                    gBeOrdenCompra.Enviado_A_ERP = False
                    gBeOrdenCompra.ObjPoliza.codigo_poliza = txtCodigoPoliza.Text.Trim
                    gBeOrdenCompra.ObjPoliza.ticket = Val(txtTicket.Text.Trim)
                    gBeOrdenCompra.ObjPoliza.numero_orden = txtNumeroOrden.Text.Trim
                    gBeOrdenCompra.ObjPoliza.fecha_aceptacion = dtpFechaAceptacion.EditValue
                    gBeOrdenCompra.ObjPoliza.fecha_llegada = dtpFechaLlegada.EditValue
                    gBeOrdenCompra.ObjPoliza.total_otros = Val(txtTotalOtros.Value)
                    'GT 22012021
                    gBeOrdenCompra.ObjPoliza.clave_aduana = txtClaveAduana.Text.Trim
                    gBeOrdenCompra.ObjPoliza.nit_imp_exp = txtNitImpExp.Text.Trim
                    gBeOrdenCompra.ObjPoliza.clase = txtClase.Text.Trim
                    gBeOrdenCompra.ObjPoliza.mod_transporte = txtMod_transporte.Text.Trim
                    gBeOrdenCompra.ObjPoliza.total_liquidar = Val(txtTotal_liquidar.EditValue)
                    gBeOrdenCompra.ObjPoliza.total_general = Val(txtTotal_general.EditValue)

                    '#GT27092023: CBM no viene en el código barras, se toma del input
                    gBeOrdenCompra.ObjPoliza.Cbm = cbCBM.Value
                    gBeOrdenCompra.ObjPoliza.activo = True

                    '#GT16092024: validar bodega de la poliza (en la transferencia se podria duplicar el mismo numero de orden)
                    gBeOrdenCompra.ObjPoliza.IdBodega = AP.Bodega.IdBodega

                End If

            Else
                gBeOrdenCompra.ObjPoliza = Nothing
            End If

            vContador += 1

            If cmbMotivoDevolucion.Visible AndAlso cmbMotivoDevolucion.Text <> String.Empty Then
                gBeOrdenCompra.IdMotivoDevolucion = CInt(cmbMotivoDevolucion.EditValue)
            End If

            Dim lBeTransOcDet As New List(Of clsBeTrans_oc_det)
            Dim listaPR As New List(Of clsBeProducto)
            Dim listaServ As New List(Of clsBeTrans_oc_servicios)

            If gvDetalleDocIngreso.RowCount > 0 Then

                Dim tpa As New clsBeTipo_actualizacion_costo()
                Dim lActualizo As Boolean = False
                Dim lTipoActualizacion As Integer = 0

                '#EJC20210315: La política de actualización de costos, solo válida si definió proveedor y propietario.
                If Not lcmbPropietario.EditValue Is Nothing Then

                    If Val(txtIdProveedor.Text) <> 0 Then

                        '4
                        vContador += 1

                        tpa = clsLnTipo_actualizacion_costo.GetParametro(lcmbPropietario.EditValue, txtIdProveedor.Text)

                        If tpa IsNot Nothing AndAlso tpa.ObjPropietario IsNot Nothing AndAlso tpa.ObjProveedor IsNot Nothing Then

                            If tpa.ObjPropietario.Actualiza_costo_oc AndAlso tpa.ObjProveedor.Actualiza_costo_oc Then
                                lActualizo = True
                                lTipoActualizacion = tpa.ObjPropietario.IdTipoActualizacionCosto
                            End If

                        End If

                    End If

                End If

                '5
                vContador += 1

                For i As Integer = 0 To gvDetalleDocIngreso.RowCount - 1

                    '6
                    vContador += 1

                    If gvDetalleDocIngreso.GetRowCellValue(i, "IdProductoBodega") IsNot Nothing Then

                        Dim BeTransOcDet As New clsBeTrans_oc_det

                        If gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraEnc") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraEnc") IsNot Nothing Then
                            BeTransOcDet.IdOrdenCompraEnc = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraEnc"))
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDet") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDet") IsNot Nothing Then
                            BeTransOcDet.IdOrdenCompraDet = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDet"))
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDetPadre") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDetPadre") IsNot Nothing Then
                            BeTransOcDet.IdOrdenCompraDetPadre = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDetPadre"))
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea") IsNot Nothing Then
                            BeTransOcDet.No_Linea = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea"))
                        End If

                        '7
                        vContador += 1

#Region "Valores Poliza"

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorAduana") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorAduana") IsNot Nothing Then
                            BeTransOcDet.valor_aduana = gvDetalleDocIngreso.GetRowCellValue(i, "ValorAduana")
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorFOB") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorFOB") IsNot Nothing Then
                            BeTransOcDet.valor_fob = gvDetalleDocIngreso.GetRowCellValue(i, "ValorFOB")
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorIVA") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorIVA") IsNot Nothing Then
                            BeTransOcDet.valor_iva = gvDetalleDocIngreso.GetRowCellValue(i, "ValorIVA")
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorDAI") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorDAI") IsNot Nothing Then
                            BeTransOcDet.valor_dai = gvDetalleDocIngreso.GetRowCellValue(i, "ValorDAI")
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorSeguro") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorSeguro") IsNot Nothing Then
                            BeTransOcDet.valor_seguro = gvDetalleDocIngreso.GetRowCellValue(i, "ValorSeguro")
                        End If

                        If gvDetalleDocIngreso.GetRowCellValue(i, "ValorFlete") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "ValorFlete") IsNot Nothing Then
                            BeTransOcDet.valor_flete = gvDetalleDocIngreso.GetRowCellValue(i, "ValorFlete")
                        End If
#End Region

                        '8
                        vContador += 1

                        BeTransOcDet.IdProductoBodega = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "IdProductoBodega"))
                        BeTransOcDet.Nombre_producto = CStr(gvDetalleDocIngreso.GetRowCellValue(i, "NombreProducto"))
                        BeTransOcDet.Codigo_Producto = CStr(gvDetalleDocIngreso.GetRowCellValue(i, "CodigoProducto"))

                        Dim lCapturarArancel As Boolean = gvDetalleDocIngreso.GetRowCellValue(i, "CapturarArancel")

                        If gvDetalleDocIngreso.GetRowCellValue(i, "CapturarArancel") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "CapturarArancel") IsNot Nothing Then

                            If lCapturarArancel AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "Arancel") IsNot DBNull.Value AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "Arancel") IsNot Nothing Then

                                BeTransOcDet.Arancel = New clsBeArancel
                                BeTransOcDet.Arancel.IdArancel = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "Arancel"))

                            ElseIf gvDetalleDocIngreso.GetRowCellValue(i, "Arancel") Is DBNull.Value OrElse gvDetalleDocIngreso.GetRowCellValue(i, "Arancel") Is Nothing AndAlso lCapturarArancel Then

                                SplashScreenManager.CloseForm(False)
                                Throw New Exception(String.Format("Debe existir Arancel en Producto {0}", BeTransOcDet.Codigo_Producto))

                            End If

                        End If

                        '9
                        vContador += 1

                        BeTransOcDet.Presentacion = New clsBeProducto_Presentacion
                        BeTransOcDet.UnidadMedida = New clsBeUnidad_medida

                        If gvDetalleDocIngreso.GetRowCellValue(i, "IdPresentacion") IsNot Nothing AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdPresentacion") IsNot DBNull.Value Then
                            BeTransOcDet.Presentacion.IdPresentacion = CInt(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdPresentacion")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "IdPresentacion")))
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = CInt(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas")))
                        ElseIf gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas") IsNot Nothing AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas") > 0 Then
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = CInt(gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas"))
                        ElseIf gvDetalleDocIngreso.GetRowCellValue(i, "IdPresentacion") Is Nothing AndAlso gvDetalleDocIngreso.GetRowCellValue(i, "IdUmBas") = 0 Then
                            Throw New Exception(String.Format("Seleccione unidad de medida o presentación de producto {0}", BeTransOcDet.Codigo_Producto))
                        End If

                        If gvDetalleDocIngreso.Columns("IdMotivoDevolucion").Visible Then
                            If gvDetalleDocIngreso.GetRowCellValue(i, "IdMotivoDevolucion") IsNot Nothing Then
                                BeTransOcDet.IdMotivoDevolucion = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdMotivoDevolucion")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "IdMotivoDevolucion"))
                            Else
                                Throw New Exception(String.Format("Seleccione motivo de devolución en producto {0}", BeTransOcDet.Codigo_Producto))
                            End If
                        End If

                        vContador += 1

                        BeTransOcDet.Cantidad = Math.Round(CDec(gvDetalleDocIngreso.GetRowCellValue(i, "Cantidad")), 6)
                        BeTransOcDet.Peso = CDec(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "PesoNeto")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "PesoNeto")))
                        BeTransOcDet.Peso_Neto = CDec(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "PesoNeto")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "PesoNeto")))
                        BeTransOcDet.Peso_Bruto = CDec(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "PesoBruto")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "PesoBruto")))

                        vContador += 1

                        'GT cuando el ingreso no maneja poliza, se asigna el valor total a valor_iva porque no se pide iva en el grid.
                        If gBeOrdenCompra.ObjPoliza Is Nothing Then
                            'BeTransOcDet.valor_iva = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Total")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "Total"))
                            'GT12072022: ajuste según correo de Otto para dejar IVA =0 para general
                            If Not BeBodega.Es_Bodega_Fiscal Then
                                BeTransOcDet.valor_iva = 0
                            End If
                        End If

                        vContador += 1

                        BeTransOcDet.Costo = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Costo")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "Costo"))
                        BeTransOcDet.Total_linea = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Total")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "Total"))
                        BeTransOcDet.Cantidad_recibida = Math.Round(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Cantidad_Recibida")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "Cantidad_Recibida")), 6)
                        BeTransOcDet.Nombre_unidad_medida_basica = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "UMBas")), String.Empty, gvDetalleDocIngreso.GetRowCellValue(i, "UMBas"))
                        BeTransOcDet.IsNew = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IsNew")), True, gvDetalleDocIngreso.GetRowCellValue(i, "IsNew"))
                        BeTransOcDet.User_agr = AP.UsuarioAp.IdUsuario
                        BeTransOcDet.Fec_agr = Now
                        BeTransOcDet.User_mod = AP.UsuarioAp.IdUsuario
                        BeTransOcDet.Fec_mod = Now
                        BeTransOcDet.Activo = True

                        If TipoTrans = eTipoTrans.Consolidado Then
                            BeTransOcDet.IdPropietarioBodega = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdPropietarioBodega")), "0", gvDetalleDocIngreso.GetRowCellValue(i, "IdPropietarioBodega"))
                            BeTransOcDet.Nombre_Propietario = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "NombrePropietario")), "0", gvDetalleDocIngreso.GetRowCellValue(i, "NombrePropietario"))
                        Else
                            BeTransOcDet.IdPropietarioBodega = lcmbPropietario.EditValue
                            BeTransOcDet.Nombre_Propietario = lcmbPropietario.Text
                        End If

                        '#GT21012025: movi aca la validación, ya que cantidad_recibida se asigna del gvdetalledocingreso y aca se confirma si mantiene o no dicho valor.
                        '#EJC20171011_0219PM: Observación de carol, no actualizar estos valores si ya la O.C. existe.
                        If Not EsActualizacion Then
                            BeTransOcDet.Peso_Recibido = 0
                            BeTransOcDet.Cantidad_recibida = 0
                        End If


                        vContador += 1

                        '#GT12082025: agregar talla y color a los valores obtenidos.
                        If BeBodega.Control_Talla_Color Then

                            BeTransOcDet.Talla = New clsBeTalla()
                            BeTransOcDet.Talla.IsNew = True
                            BeTransOcDet.Talla.IdTalla = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Talla")), "0", gvDetalleDocIngreso.GetRowCellValue(i, "Talla"))
                            BeTransOcDet.Color = New clsBeColor()
                            BeTransOcDet.Color.IsNew = True
                            BeTransOcDet.Color.IdColor = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "Color")), "0", gvDetalleDocIngreso.GetRowCellValue(i, "Color"))

                        End If

                        vContador += 1

                        lBeTransOcDet.Add(BeTransOcDet)

                        If lActualizo Then

                            Dim lIndex As Integer = -1

                            lIndex = pListObjPR.FindIndex(Function(p) p.pIdProducto = BeTransOcDet.IdProductoBodega)

                            If lIndex > -1 Then

                                Dim ObjPR As New clsBeProducto

                                If lTipoActualizacion = 1 Then

                                    If BeTransOcDet.Costo > pListObjPR(lIndex).pCosto Then
                                        ObjPR.IdProducto = BeTransOcDet.IdProductoBodega
                                        ObjPR.Costo = BeTransOcDet.Costo
                                    End If

                                ElseIf lTipoActualizacion = 2 Then

                                    If BeTransOcDet.Costo < pListObjPR(lIndex).pCosto Then
                                        ObjPR.IdProducto = BeTransOcDet.IdProductoBodega
                                        ObjPR.Costo = BeTransOcDet.Costo
                                    End If

                                ElseIf lTipoActualizacion = 3 Then

                                    If BeTransOcDet.Costo <> pListObjPR(lIndex).pCosto Then
                                        ObjPR.IdProducto = BeTransOcDet.IdProductoBodega
                                        ObjPR.Costo = BeTransOcDet.Costo
                                    End If

                                End If

                                listaPR.Add(ObjPR)

                            End If

                        End If

                    End If

                Next

            End If


            vContador += 1

            '#GT28052024: aqui se guardan los servicios asociados a un acuerdo comercial
            If gvDetalleServicios.DataRowCount > 0 Then

                Dim servicio As New clsBeTrans_oc_servicios()
                'Dim BeAcuerdoDet As New clsBeI_nav_acuerdo_det()
                Dim BeAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det()
                Dim vIdAcuerdo As Integer = 0
                Dim vIdAcuerdoDet As Integer = 0
                Dim vCorrelativo As Integer = 0
                Dim vIdPropietarioBodega As Integer = 0
                Dim vIdOrdenCompraServicio As Integer = 0
                Dim vCantidad As Double = 0
                Dim vIsNewRow As Boolean = False

                For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1

                    vIdAcuerdo = gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoEnc")
                    vIdAcuerdoDet = gvDetalleServicios.GetRowCellValue(i, "IdAcuerdoDet")
                    vCorrelativo = gvDetalleServicios.GetRowCellValue(i, "correlativo_detalleacuerdo")
                    vIdOrdenCompraServicio = gvDetalleServicios.GetRowCellValue(i, "IdOrdenCompraServicio")
                    vCantidad = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "cantidad")), 0, gvDetalleServicios.GetRowCellValue(i, "cantidad"))
                    vIdPropietarioBodega = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "IdPropietarioBodega")), 0, gvDetalleServicios.GetRowCellValue(i, "IdPropietarioBodega"))
                    vIsNewRow = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(i, "IsNewR")), False, gvDetalleServicios.GetRowCellValue(i, "IsNewR"))

                    '#GT28052024: obtener el acuerdo comercial según correlativo
                    BeAcuerdoDet.Correlativo_detalleacuerdo = vCorrelativo
                    clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(BeAcuerdoDet)

                    If BeAcuerdoDet IsNot Nothing Then '#EJC20210420: Es un servicio válido para el acuerdo.

                        '#GT30052024: validar que la linea sea nueva para guardar
                        If vIsNewRow Then

                            servicio = New clsBeTrans_oc_servicios()
                            CopyObject(BeAcuerdoDet, servicio)
                            servicio.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                            servicio.IdOrdenCompraServicio = vIdOrdenCompraServicio
                            servicio.IdPropietarioBodega = vIdPropietarioBodega
                            servicio.IdAcuerdo = BeAcuerdoDet.IdAcuerdoEnc
                            servicio.IdAcuerdoDet = BeAcuerdoDet.IdAcuerdoDet
                            servicio.Corre_detalleacuerdo = vCorrelativo
                            servicio.Cantidad = vCantidad
                            servicio.Nombre_servicio = BeAcuerdoDet.Servicio
                            servicio.User_agr = AP.UsuarioAp.IdUsuario
                            servicio.User_mod = AP.UsuarioAp.IdUsuario
                            servicio.Fec_mod = Now
                            servicio.IsNew = (vIdOrdenCompraServicio = 0)
                            listaServ.Add(servicio)

                        End If

                    End If

                Next

            End If


            vContador += 1

            If lBeTransOcDet.Count = 0 Then Throw New Exception("Ingrese Productos.")

            If clsLnTrans_oc_enc.Es_Devolucion(cmbTipoIngreso.EditValue) Then
                gBeOrdenCompra.IdMotivoDevolucion = cmbMotivoDevolucion.EditValue
            End If

            gBeOrdenCompra.IdOperadorBodegaDefecto = cmbOperadorDefecto.EditValue
            gBeOrdenCompra.IdBodega = cmbBodega.EditValue
            gBeOrdenCompra.IdPropietarioBodega = lcmbPropietario.EditValue
            If String.IsNullOrEmpty(txtIdCampaña.Text) Then
                gBeOrdenCompra.IdCampaña = 0
            Else
                gBeOrdenCompra.IdCampaña = txtIdCampaña.Text
            End If


            vContador += 1

            gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.Actualizar_Datos(gBeOrdenCompra,
                                                                                lBeTransOcDet,
                                                                                lOCImg,
                                                                                listaPR,
                                                                                listaServ,
                                                                                gBeOrdenCompra.ObjPoliza)


            If Not EsActualizacion Then
                '#MECR03102025: Se agrego nueva bitacora de logs para OC
                'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                Dim msgAdvertencia As String = "ADVERTENCIA_202302231656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc
                clsLnLog_error_wms_oc.Agregar_Error(msgAdvertencia, AP.UsuarioAp.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)
            Else
                '#MECR03102025: Se agrego nueva bitacora de logs para OC
                'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                Dim msgAdvertencia As String = "ADVERTENCIA_202302231656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc
                clsLnLog_error_wms_oc.Agregar_Error(msgAdvertencia, AP.UsuarioAp.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)
            End If


            Guardar = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Function Guardar_Enc_OC() As Boolean

        Guardar_Enc_OC = False

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbBodega.Focus()
                Exit Function
            End If

            If lcmbPropietario.EditValue Is Nothing Then
                XtraMessageBox.Show("Seleccione propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                lcmbPropietario.Focus()
                Exit Function
            End If

            If gBeOrdenCompra Is Nothing Then
                gBeOrdenCompra = New clsBeTrans_oc_enc() With {.IsNew = True}
                gBeOrdenCompra.IdBodega = cmbBodega.EditValue
            End If

            If gBeOrdenCompra.IsNew Then
                gBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                gBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = lcmbPropietario.EditValue
                gBeOrdenCompra.IdEstadoOC = cmbEstado.EditValue
                gBeOrdenCompra.Hora_Creacion = Now
                gBeOrdenCompra.User_Agr = AP.UsuarioAp.IdUsuario
                gBeOrdenCompra.Fec_Agr = Now
            End If

            gBeOrdenCompra.Fecha_Creacion = dtmFechaOrdenCompra.EditValue
            gBeOrdenCompra.Activo = chkActivo.Checked

            If gBeOrdenCompra.ProveedorBodega Is Nothing Then
                gBeOrdenCompra.ProveedorBodega = New clsBeProveedor_bodega()
            End If

            '#EJC20180926: En el tag se guarda el IdProveedorBodega y en el text se muestra el IdProveedor
            gBeOrdenCompra.IdProveedorBodega = Val(txtIdProveedor.Tag)
            gBeOrdenCompra.IdTipoIngresoOC = cmbTipoIngreso.EditValue
            gBeOrdenCompra.No_Documento = txtNoDocumento.Text.Trim
            gBeOrdenCompra.User_Mod = AP.UsuarioAp.IdUsuario
            gBeOrdenCompra.Fec_Mod = Now
            gBeOrdenCompra.Procedencia = txtProcedencia.Text.Trim
            'gBeOrdenCompra.No_Marchamo = txtNoMarchamo.Text.Trim
            gBeOrdenCompra.Referencia = txtReferencia.Text.Trim
            gBeOrdenCompra.Observacion = txtObservacion.Text.Trim
            gBeOrdenCompra.Comentarios = txtComentarios.Text.Trim
            gBeOrdenCompra.Usr_Documento = txtUsuarioERP.Text.Trim
            gBeOrdenCompra.Control_Poliza = chkControlPoliza.Checked

            If cmbMotivoDevolucion.Visible AndAlso cmbMotivoDevolucion.Text <> String.Empty Then
                gBeOrdenCompra.IdMotivoDevolucion = CInt(cmbMotivoDevolucion.EditValue)
            End If

            If clsLnTrans_oc_enc.Es_Devolucion(cmbTipoIngreso.EditValue) Then
                gBeOrdenCompra.IdMotivoDevolucion = cmbMotivoDevolucion.EditValue
            End If

            '#EJC20210407: Evitar generar encabezados innecesarios si falla la carga de archivo o se cancela.
            'Pasar el objeto a la forma de carga de excel y que la forma lo guarde dentro de la transacción.
            'gBeOrdenCompra.IdOrdenCompraEnc = clsLnTrans_oc_enc.Actualizar_Datos_Enc(gBeOrdenCompra)

            Guardar_Enc_OC = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then Actualizar = Guardar(True)

            If Actualizar AndAlso gBeOrdenCompra.IdEstadoOC = 1 AndAlso Not clsLnTrans_re_enc.OC_Tiene_Recepciones_Activas(gBeOrdenCompra.IdOrdenCompraEnc) Then

                SplashScreenManager.CloseForm(False)

                Dim GeneraRec As Boolean = True

                If BeTipoDocumento.Requerir_Documento_Ref Then
                    If (gBeOrdenCompra.IdNoDocumentoRef = 0 AndAlso gBeOrdenCompra.IdPedidoEncDevolucion = 0 AndAlso gBeOrdenCompra.No_Documento_Devolucion = String.Empty) Then
                        GeneraRec = False
                    End If
                End If

                If GeneraRec Then

                    If cmbOperadorDefecto.EditValue Is Nothing Then

                        If XtraMessageBox.Show("Se actualizó el registro. ¿Crear tarea de recepción?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If NuevaRecepcion(0) Then
                                If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                                DialogResult = DialogResult.OK
                                Close()
                            Else
                                If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                                Close()
                            End If
                        Else
                            If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                            Close()
                        End If

                    Else
                        If NuevaRecepcion(cmbOperadorDefecto.EditValue) Then
                            If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                            DialogResult = DialogResult.OK
                            Close()
                        Else
                            If Not InvokeListarPedidosCompra Is Nothing Then InvokeListarPedidosCompra.Invoke()
                            Close()
                        End If
                    End If

                    Actualizar = True

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")


        Try

            If Actualizar() Then
                SplashScreenManager.CloseForm(False)
                Close()
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txtPiezas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPiezas.KeyPress
        If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
            e.Handled = True
        End If

        If e.KeyChar = "." Then
            e.Handled = True
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        PicImg.Visible = False

        Try

            Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
            gFile.ShowDialog()

            If gFile.FileName.Length <> 0 Then

                Dim iNombreDoc As String = InputBox("Ingrese una descripción:", Text, String.Empty)


                If String.IsNullOrEmpty(iNombreDoc) = False Then

                    PicImg.ImageLocation = gFile.FileName
                    PicImg.Tag = gFile.FileName

                    Dim ObjI As New clsBeTrans_oc_imagen

                    If lOCImg IsNot Nothing AndAlso lOCImg.Count > 0 Then
                        ObjI.IdOrdenCompraImg = (From b In lOCImg.AsEnumerable Select b.IdOrdenCompraImg).Max + 1
                    Else
                        ObjI.IdOrdenCompraImg = 1
                    End If

                    ObjI.Descripcion = iNombreDoc

                    PicImg.ImageLocation.LastIndexOf("\")
                    ObjI.Imagen = ReadBinaryFile(PicImg.ImageLocation)
                    ObjI.IsNew = True
                    lOCImg.Add(ObjI)
                    Cargar_Imagenes()

                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No selecciono ninguna imagen.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Shared Function ReadBinaryFile(ByVal fileName As String) As Byte()

        If Not File.Exists(fileName) Then Return Nothing

        Try
            Dim fs As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim data() As Byte = New Byte(Convert.ToInt32(fs.Length)) {}
            fs.Read(data, 0, Convert.ToInt32(fs.Length))
            fs.Close()
            Return data
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub Cargar_Imagenes()

        Try

            Dim DT As New DataTable("Imagen")
            DT.Columns.Add("Código", GetType(Integer))
            DT.Columns.Add("Descripción", GetType(String))

            If lOCImg IsNot Nothing AndAlso lOCImg.Count > 0 Then
                For Each Obj As clsBeTrans_oc_imagen In lOCImg
                    DT.Rows.Add(Obj.IdOrdenCompraImg, Obj.Descripcion)
                Next
            End If

            GrdImagen.DataSource = DT

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub GrdImagen_Click(sender As Object, e As EventArgs) Handles GrdImagen.Click

        Try

            If GridViewImg.RowCount > 0 Then
                Dim Dr As DataRowView = GridViewImg.GetFocusedRow
                Dim Obj As New clsBeTrans_oc_imagen()
                Obj = lOCImg.Find(Function(b) b.IdOrdenCompraImg = CInt(Dr.Item("Código")))
                Dim ms As MemoryStream = New MemoryStream(Obj.Imagen)
                Dim bm As Bitmap = New Bitmap(ms)
                PicImg.Image = bm
                PicImg.Visible = True
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click

        Try

            If GridViewImg.RowCount > 0 Then
                Dim Dr As DataRow = GridViewImg.GetFocusedDataRow
                SplashScreenManager.CloseForm(False)
                If XtraMessageBox.Show(String.Format("¿Eliminar la imagen {0}", Dr.Item("Descripción")),
                                       Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim lIndex As Integer = -1
                    lIndex = lOCImg.FindIndex(Function(i) i.IdOrdenCompraImg = CInt(Dr.Item("Código")))
                    If lIndex > -1 Then
                        clsLnTrans_oc_imagen.Delete(lOCImg(lIndex).IdOrdenCompraEnc, lOCImg(lIndex).IdOrdenCompraImg)
                        lOCImg.RemoveAt(lIndex)
                        Cargar_Imagenes()
                        PicImg.Image = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lnkProveedor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkProveedor.LinkClicked

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Proveedores")

        Try

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                Dim Proveedor As New frmProveedor_List() With {.IdBodega = cmbBodega.EditValue,
                    .IdPropietario = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, lcmbPropietario.EditValue),
                    .Modo = frmProveedor_List.pModo.Seleccion,
                    .StartPosition = FormStartPosition.CenterParent,
                    .WindowState = FormWindowState.Maximized}
                Proveedor.Requerir_Proveedor_Es_Bodega_WMS = BeTipoDocumento.Requerir_Proveedor_Es_Bodega_WMS
                Proveedor.chkRequerir_Proveedor_Es_Bodega_WMS.Checked = BeTipoDocumento.Requerir_Proveedor_Es_Bodega_WMS
                SplashScreenManager.CloseForm(False)

                Proveedor.ShowDialog()

                If Proveedor.pBeProveedor IsNot Nothing AndAlso Proveedor.pBeProveedor.IdProveedor <> 0 Then
                    txtIdProveedor.Tag = Proveedor.pBeProveedor.IdAsignacion
                    txtIdProveedor.Text = Proveedor.pBeProveedor.IdProveedor
                    If Not Proveedor.pBeProveedor.Proveedor Is Nothing Then
                        txtNombreProveedor.Text = Proveedor.pBeProveedor.Proveedor.Nombre
                    End If
                End If

                Proveedor.Close()
                Proveedor.Dispose()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdProveedor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdProveedor.KeyPress

        Try

            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If

            If e.KeyChar = "." Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdProveedor.Text.Length = 1 Then
                txtNombreProveedor.Text = String.Empty
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmOrdenCompra22_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try

            If e.KeyCode = Keys.F2 Then
                If xtraOrdenCompra.SelectedTabPageIndex = 1 AndAlso DgridDetalleOC.ContainsFocus Then
                    prgp.Visible = True
                    Application.DoEvents()
                    '#EJC20210307: PENDIENTE POR CAMBIO DE GRID
                    'Clipboard.SetDataObject(DgridDetalleOC.CurrentCell.Value)
                    Thread.Sleep(1000)
                    prgp.Visible = False
                End If
            ElseIf e.KeyCode = Keys.Escape Then

                '#EJC20240326: Validar al salir.
                Dim vMensaje As String = ""

                If Modo = ModoTrans.Nuevo Then
                    vMensaje = "¿Al parecer no se ha guardado la recepción, salir de todas formas?"
                Else
                    vMensaje = "¿Salir de la recepción?"
                End If

                If XtraMessageBox.Show(vMensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Close()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.UsuarioAp.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Function ImagenCodigoBarra(ByVal pCodigoBarra As String, ByVal pIdSimbologia As Integer) As Byte()

        Dim Ret As Byte()

        Try

            Dim Bcc As New BarCodeControl() With {.Text = pCodigoBarra.Trim}
            Dim symb As New QRCodeGenerator()
            Bcc.Symbology = New QRCodeGenerator()
            symb.CompactionMode = QRCodeCompactionMode.AlphaNumeric
            symb.ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H
            symb.Version = QRCodeVersion.AutoVersion
            Bcc.Symbology = BarCodeGeneratorFactory.Create(pIdSimbologia)
            'NuevaRecepcion()
            Bcc.Height = 55
            Bcc.Width = 275

            Dim bitmap As New Bitmap(Bcc.Width, Bcc.Height)
            Bcc.DrawToBitmap(bitmap, Bcc.ClientRectangle)

            PicImg.Image = bitmap

            Ret = ImageToByteArray(bitmap)
            Return Ret

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Sub ImprimirPreIngreso()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try

            Using r As New frmReporte

                Dim rpt As New rptOrden_Compra
                Dim lDS As New DsOrden_Compra()
                Dim DT As DataTable = clsLnTrans_oc_enc.GetImpresionByOC(gBeOrdenCompra.IdOrdenCompraEnc)

                Dim d As New DataTable("OC")
                d.Columns.Add("Fecha_Creacion", GetType(DateTime))
                d.Columns.Add("Proveedor", GetType(String))
                d.Columns.Add("No_Documento", GetType(String))
                d.Columns.Add("No_Linea", GetType(String))
                d.Columns.Add("Codigo", GetType(String))
                d.Columns.Add("Producto", GetType(String))
                d.Columns.Add("Cantidad", GetType(Double))
                d.Columns.Add("CodigoBarra", GetType(Byte()))
                d.Columns.Add("IdOrdenCompraDet", GetType(Integer))
                d.Columns.Add("MotivoDevolucion", GetType(String))

                For Each lRow As DataRow In DT.Rows
                    Dim row As DataRow = d.NewRow
                    row(0) = lRow(0) : row(1) = lRow(1)
                    row(2) = lRow(2) : row(3) = lRow(3)
                    row(4) = lRow(4) : row(5) = lRow(5)
                    row(6) = lRow(6)
                    Dim Cod As String = lRow(7).ToString
                    Dim lId As Integer = IIf(IsDBNull(lRow(8)), 0, lRow(8))
                    If lId <> 0 Then row(7) = ImagenCodigoBarra(Cod, lId)
                    row(8) = lRow(10)
                    row(9) = lRow(12)
                    d.Rows.Add(row)
                Next

                lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                lDS.Tables("Data").Merge(d)

                For Each thisFormulaField In rpt.DataDefinition.FormulaFields
                    Select Case thisFormulaField.FormulaName
                        Case "{@NombreEmpresa}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Empresa", "IdEmpresa = " & AP.IdEmpresa))
                        Case "{@Bodega}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Bodega", "IdEmpresa = " & AP.IdBodega))
                        Case "{@Usuario}"
                            thisFormulaField.Text = String.Format("'{0}'", AP.UsuarioAp.Nombres)
                        Case Else
                            Exit Select
                    End Select
                Next

                r.Text = "Orden de Compra Pre Ingreso"
                rpt.SetDataSource(lDS)

                r.rptView.ReportSource = rpt
                SplashScreenManager.CloseForm(False)
                r.ShowDialog()

            End Using

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub ImprimirCostoArancel()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Imprimiendo...")

        Try

            Using r As New frmReporte

                Dim rpt As New rptOCCostoArancel

                'rpt.Load("C:\Users\Desarrollo7\Source\Repos\TOMIMSV42\TOMIMSV4\TOMIMSV4\Reportes\Orden_Compra\CostoArancel\rptOCCostoArancel.rpt")

                Dim lDS As New DsOCCostoArancel
                Dim DT As DataTable = clsLnTrans_oc_enc.GetImpresionByOC(gBeOrdenCompra.IdOrdenCompraEnc)

                lDS.Tables("Imagen").Merge(ObtenerImagen("Imagen", "Empresa", "IdEmpresa=" & AP.IdEmpresa))
                lDS.Tables("Data").Merge(DT)

                For Each thisFormulaField In rpt.DataDefinition.FormulaFields
                    Select Case thisFormulaField.FormulaName
                        Case "{@NombreEmpresa}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Empresa", "IdEmpresa = " & AP.IdEmpresa))
                        Case "{@Bodega}"
                            thisFormulaField.Text = String.Format("'{0}'", GetDBValue("Nombre", "Bodega", "IdEmpresa = " & AP.IdBodega))
                        Case "{@Usuario}"
                            thisFormulaField.Text = String.Format("'{0}'", AP.UsuarioAp.Nombres)
                        Case Else
                            Exit Select
                    End Select
                Next

                r.Text = "Ingreso a Bodega"

                rpt.SetDataSource(lDS)

                r.rptView.ReportSource = rpt
                SplashScreenManager.CloseForm(False)
                r.ShowDialog()

            End Using

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmdPreIngreso_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPreIngreso.ItemClick
        ImprimirPreIngreso()
    End Sub

    Private Sub cmdCostoArancel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCostoArancel.ItemClick
        ImprimirCostoArancel()
    End Sub

    Private BeTipoDocumento As New clsBeTrans_oc_ti

    Private Sub Set_Tipo_Documento()

        Try

            'If Llenando_Datos_Maestros Then Exit Sub

            If cmbBodega.Text <> String.Empty AndAlso cmbTipoIngreso.EditValue <> 0 Then

                BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(cmbTipoIngreso.EditValue)

                If BeTipoDocumento Is Nothing Then
                    Throw New Exception("No se encontró la definición del tipo de documento para: " & cmbTipoIngreso.EditValue)
                End If

                Set_Columnas_Grid_Detalle_Documento_Ingreso()

                If (gvDetalleDocIngreso.Columns.Count > 0) Then
                    gvDetalleDocIngreso.Columns("ValorFOB").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("ValorIVA").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("ValorDAI").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("ValorSeguro").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("ValorFlete").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("ValorAduana").Visible = BeTipoDocumento.Control_Poliza
                    gvDetalleDocIngreso.Columns("IdMotivoDevolucion").Visible = BeTipoDocumento.Es_devolucion
                End If

                lblMotivoDevolucion.Visible = BeTipoDocumento.Es_devolucion
                cmbMotivoDevolucion.Visible = BeTipoDocumento.Es_devolucion

                If BeTipoDocumento.Es_devolucion Then
                    grpMotivoDevolucion.Visible = True
                    Llena_Motivos_Devolucion()
                    lblMotivoDevolucion.ForeColor = Color.Red
                    tabPedidosDevolucion.Visible = True
                    tabPedidosDevolucion.PageVisible = True
                Else
                    grpMotivoDevolucion.Visible = False
                    cmbMotivoDevolucion.Properties.DataSource = Nothing
                    lblMotivoDevolucion.ForeColor = Color.Gray
                    tabPedidosDevolucion.PageVisible = False
                End If

                'GT27012022_2212: solo para bodega fiscal se debe mostrar tab poliza, en general no es necesario
                If AP.Bodega.Es_Bodega_Fiscal Then

                    chkControlPoliza.Checked = BeTipoDocumento.Control_Poliza
                    grpScanPoliza.Visible = BeTipoDocumento.Control_Poliza
                    Poliza.Visible = BeTipoDocumento.Control_Poliza
                    Poliza.PageVisible = BeTipoDocumento.Control_Poliza
                    tabPolizaCorregida.Visible = BeTipoDocumento.Control_Poliza
                    tabPolizaCorregida.PageVisible = BeTipoDocumento.Control_Poliza
                    cmdCorreccionPoliza.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    chkControlPoliza.Checked = False
                    grpScanPoliza.Visible = False
                    Poliza.Visible = False
                    Poliza.PageVisible = False
                    tabPolizaCorregida.Visible = False
                    tabPolizaCorregida.PageVisible = False
                    cmdCorreccionPoliza.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If

                lblDocumentoRef.Enabled = BeTipoDocumento.Requerir_Documento_Ref
                cmbDocumentoRef.Enabled = BeTipoDocumento.Requerir_Documento_Ref

                '#EJC20210617: Ref. a pedido de cliente para devolución.
                lnkPedido.Visible = BeTipoDocumento.Es_devolucion
                txtIdPedidoDevolucionEnc.Visible = BeTipoDocumento.Es_devolucion
                txtNombPedido.Visible = BeTipoDocumento.Es_devolucion

                If Not BeTipoDocumento.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Liquidacion_De_Ruta_Devolucion Then '#EJC20210617:Devolución idealsa.
                    If BeTipoDocumento.Requerir_Documento_Ref Then
                        If (gBeOrdenCompra.IdNoDocumentoRef = 0 AndAlso gBeOrdenCompra.IdPedidoEncDevolucion = 0 AndAlso gBeOrdenCompra.No_Documento_Devolucion = String.Empty) Then
                            Llena_Documentos_Referencia_Pendientes_De_Asingar()
                            lblDocumentoRef.ForeColor = Color.Red
                        Else
                            lblDocumentoRef.ForeColor = Color.Green
                        End If
                    Else
                        lblDocumentoRef.ForeColor = Color.Gray
                    End If
                End If

                '#EJC20220504:Set de colores para indicar al usuario que ingrese referencia.
                If BeTipoDocumento.Exigir_Campo_Referencia Then
                    lblReferencia.ForeColor = Color.Red
                    txtReferencia.BackColor = Color.MistyRose
                Else
                    lblReferencia.ForeColor = Color.Black
                    txtReferencia.BackColor = Color.White
                End If

            Else

                lblMotivoDevolucion.Visible = False
                cmbMotivoDevolucion.Visible = False

                If gvDetalleDocIngreso.Columns("MotivoDevolucion") IsNot Nothing Then
                    gvDetalleDocIngreso.Columns("MotivoDevolucion").Visible = False
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Llena_Motivos_Devolucion()

        Try

            cmbMotivoDevolucion.Properties.DataSource = Nothing

            Dim l As New DataTable

            l = clsLnMotivo_devolucion.Get_All_By_IdPropietario_And_Bodega_DT(lcmbPropietario.Tag, cmbBodega.EditValue)

            If l.Rows.Count > 0 Then
                lblMotivoDevolucion.Visible = True
                cmbMotivoDevolucion.Visible = True
                cmbMotivoDevolucion.Properties.ValueMember = "IdMotivoDevolucion"
                cmbMotivoDevolucion.Properties.DisplayMember = "Nombre"
                cmbMotivoDevolucion.Properties.DataSource = l
                'GT 13042021 Columna da error de instancia
                'gvDetalleDocIngreso.Columns("MotivoDevolucion").Visible = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub Llena_Documentos_Referencia_Pendientes_De_Asingar()

        Try

            cmbDocumentoRef.Properties.DataSource = Nothing

            Dim l As New DataTable

            l = clsLnTrans_oc_docu_ref.Get_All_Activos_Pendientes_De_Asignar()

            If l.Rows.Count > 0 Then
                cmbDocumentoRef.Visible = True
                cmbDocumentoRef.Properties.ValueMember = "IdDocumentoRef"
                cmbDocumentoRef.Properties.DisplayMember = "Codigo"
                cmbDocumentoRef.Properties.DataSource = l
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub Llena_Documentos_Referencia_Asignado(ByVal pIdNoDocumentoRef As Integer)

        Try

            cmbDocumentoRef.Properties.DataSource = Nothing

            Dim l As New DataTable

            l = clsLnTrans_oc_docu_ref.Get_Single_By_IdNoDocumento(pIdNoDocumentoRef)

            If l.Rows.Count > 0 Then
                cmbDocumentoRef.Visible = True
                cmbDocumentoRef.Properties.ValueMember = "IdDocumentoRef"
                cmbDocumentoRef.Properties.DisplayMember = "Codigo"
                cmbDocumentoRef.Properties.DataSource = l
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Function Llena_Servicios_By_Acuerdo_For_Combo() As Boolean

        Llena_Servicios_By_Acuerdo_For_Combo = False

        Try

            cmbAcuerdoComercial.Properties.DataSource = Nothing

            Dim DTAcuerdoEnc As New DataTable
            DTAcuerdoEnc.Clear()

            DTAcuerdoEnc = clsLnTrans_acuerdoscomerciales_enc.Get_AcuerdosEnc_By_IdCliente_And_IdBodega(pIdPropietario, cmbBodega.EditValue)

            If DTAcuerdoEnc IsNot Nothing AndAlso DTAcuerdoEnc.Rows.Count > 0 Then

                If DTAcuerdoEnc.Rows.Count > 0 Then

                    cmbAcuerdoComercial.Visible = True

                    cmbAcuerdoComercial.Properties.Columns.Clear()
                    cmbAcuerdoComercial.Properties.Columns.Add(New LookUpColumnInfo("IdAcuerdoEnc", "IdAcuerdoEnc"))
                    cmbAcuerdoComercial.Properties.Columns.Add(New LookUpColumnInfo("codigo", "Codigo"))
                    cmbAcuerdoComercial.Properties.Columns.Add(New LookUpColumnInfo("acuerdo", "Acuerdo"))
                    cmbAcuerdoComercial.Properties.Columns.Add(New LookUpColumnInfo("moneda", "Moneda"))
                    cmbAcuerdoComercial.Properties.Columns.Add(New LookUpColumnInfo("tipo_cobro", "Tipo_cobro"))

                    cmbAcuerdoComercial.Properties.DataSource = DTAcuerdoEnc
                    cmbAcuerdoComercial.Properties.ValueMember = "IdAcuerdoEnc"
                    cmbAcuerdoComercial.Properties.DisplayMember = "acuerdo"

                    'cmbAcuerdoComercial.Properties.PopulateColumns()

                    If cmbAcuerdoComercial.Properties.Columns.Count > 0 Then
                        cmbAcuerdoComercial.Properties.Columns("IdAcuerdoEnc").Visible = False
                    End If

                    Select Case Modo

                        Case ModoTrans.Nuevo

                            cmbAcuerdoComercial.Enabled = True
                            ServicioGridLookUpEdit.DataSource = Nothing

                        Case ModoTrans.Editar

                            If gBeOrdenCompra.IdAcuerdoComercial > 0 Then
                                cmbAcuerdoComercial.EditValue = gBeOrdenCompra.IdAcuerdoComercial
                                cmbAcuerdoComercial.Enabled = False
                            Else
                                cmbAcuerdoComercial.Enabled = True
                            End If

                    End Select

                    Llena_Servicios_By_Acuerdo_For_Combo = True

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Function

    Private Sub GridViewImg_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridViewImg.RowStyle

        Try

            GridViewImg.OptionsBehavior.Editable = False
            GridViewImg.OptionsSelection.EnableAppearanceFocusedCell = False

            GridViewImg.FocusRectStyle = DrawFocusRectStyle.RowFocus

            GridViewImg.OptionsSelection.EnableAppearanceFocusedRow = True
            GridViewImg.OptionsSelection.EnableAppearanceHideSelection = True
            GridViewImg.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridViewImg.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridViewImg.Appearance.FocusedRow.ForeColor = Color.White
            GridViewImg.Appearance.SelectedRow.ForeColor = Color.White

            GridViewImg.Appearance.SelectedRow.Options.UseBackColor = True
            GridViewImg.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try
            cmdEliminar.Enabled = False

            SplashScreenManager.CloseForm(False)

            If Not clsLnTrans_re_oc.OrdenCompra_Tiene_Recepciones_Finalizadas(gBeOrdenCompra.IdOrdenCompraEnc) Then

                If XtraMessageBox.Show("¿Anular Documento de Ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    Using MA As New frmMotivo_AnulacionList()

                        With MA

                            .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                            .BeMotivoAnulacionBodega.IdBodega = cmbBodega.EditValue

                            If OpcionesMenu IsNot Nothing Then
                                .OpcionesMenu = OpcionesMenu
                            End If

                            '#CM20171108:Obtener IdMotivoAnulacionBodega por IdBodega y MotivoAnulacion
                            'Ya no es necesario, by ejc.
                            'clsLnMotivo_anulacion_bodega.GetIdMotivoAnulacionBodega(.BeMotivoAnulacionBodega.IdBodega, .BeMotivoAnulacionBodega.IdMotivoAnulacion)

                            If .ShowDialog() = DialogResult.OK Then

                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                If clsLnTrans_oc_enc.Anular_OC(gBeOrdenCompra.IdOrdenCompraEnc, gBeOrdenCompra.ObjPoliza, .BeMotivoAnulacionBodega.IdMotivoAnulacionBodega, AP.IdBodega) Then

                                    '#MECR03102025: Se agrego nueva bitacora de logs para OC
                                    'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " anuló el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                                    Dim msgAdvertencia As String = "ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " anuló el IdOrdenCompraEnc: " & gBeOrdenCompra.IdOrdenCompraEnc
                                    clsLnLog_error_wms_oc.Agregar_Error(msgAdvertencia, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)

                                    SplashScreenManager.CloseForm(False)

                                    XtraMessageBox.Show("Documento de ingreso anulado correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                    Close()

                                    InvokeListarPedidosCompra.Invoke()

                                Else
                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show("No se pudo anular el documento de ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If

                            End If

                        End With

                    End Using

                End If

            Else
                XtraMessageBox.Show("No se puede anular el documento de ingreso, tiene recepciones finalizadas.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            cmdEliminar.Enabled = True

        Catch ex As Exception

            cmdEliminar.Enabled = True
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdProveedor_Validated(sender As Object, e As EventArgs) Handles txtIdProveedor.Validated
        Valida_Proveedor()
    End Sub

    Private Function Valida_Proveedor() As Boolean

        Valida_Proveedor = False

        Try

            If String.IsNullOrEmpty(txtIdProveedor.Text.Trim()) = False AndAlso txtIdProveedor.Text > "0" Then

                Dim pBeProvBod As New clsBeProveedor_bodega() With {.IdProveedor = txtIdProveedor.Text.Trim(), .IdBodega = cmbBodega.EditValue}
                clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(pBeProvBod)

                If pBeProvBod IsNot Nothing AndAlso pBeProvBod.Proveedor.IdProveedor > 0 Then
                    txtIdProveedor.Tag = pBeProvBod.IdAsignacion '#CKFK 20181005 Se estaba asignando el IdProveedor y debía ser el IdProveedorBodega
                    txtNombreProveedor.Text = pBeProvBod.Proveedor.Nombre
                    Valida_Proveedor = True
                Else

                    If Modo = ModoTrans.Nuevo Then

                        SplashScreenManager.CloseForm(False)

                        txtIdProveedor.Tag = 0

                        If vMensajeDevolucionRutaExcepcionProveedor = String.Empty Then
                            XtraMessageBox.Show(String.Format("No existe Proveedor con código: ""{0}"" asociado a la bodega: ""{1}""", txtIdProveedor.Text, cmbBodega.Text), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            XtraMessageBox.Show(String.Format("No existe Proveedor con código: ""{0}"" asociado a la bodega: ""{1}"" - {2} ", txtIdProveedor.Text,
                                                              cmbBodega.Text,
                                                              vbNewLine & vMensajeDevolucionRutaExcepcionProveedor), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        txtIdProveedor.Focus()
                        txtIdProveedor.SelectAll()

                    Else
                        '#EJC20171012:0435AM: Si el proveedor fue desactivado y se abre una O.C. existente, no mostrar excepción y desplegar proveedor como histórico no editable.
                        txtIdProveedor.Enabled = False
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub cmdEliminarFila_Click(sender As Object, e As EventArgs) Handles cmdEliminarFila.Click
        EliminarFila(Nothing)
    End Sub

    Private Sub EliminarFila(e As KeyEventArgs)

        Try

            Dim currentView As GridView = DgridDetalleOC.FocusedView

            '#GT11102022_1500: validar que es una fila con datos
            If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                Dim vCodigoProducto As String = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdProductoBodega")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdProductoBodega"))

                If XtraMessageBox.Show(String.Format("¿Eliminar el Producto: ""{0}""?", vCodigoProducto) _
                                          , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    Dim gridControl As GridControl = DgridDetalleOC
                    Eliminar_Fila(gridControl)

                End If


            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdAgregarProducto_Click(sender As Object, e As EventArgs) Handles cmdAgregarProducto.Click

        Try

            Using Producto As New frmProductoList(True)

                Producto.cmdImportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Producto.chkActivos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Producto.pIdBodega = cmbBodega.EditValue
                Producto.pIdPropietarioBodega = lcmbPropietario.EditValue
                Producto.Modo = frmProductoList.pModo.Seleccion
                Producto.WindowState = FormWindowState.Maximized
                Producto.ShowDialog()

                If Producto.pObjProducto IsNot Nothing AndAlso Producto.pObjProducto.IdProducto <> 0 Then

                    'GT24022022: set del producto seleccionado de la lista
                    vBeProducto = Producto.pObjProducto

                    Llena_ProductosLookUp_Grid(lcmbPropietario.EditValue)

                    Dim NoLinea As Integer = 0

                    If lOCDetLn.Count = 0 Then
                        NoLinea += 1
                    Else
                        NoLinea = lOCDetLn.Max(Function(x) x.No_Linea) + 1
                    End If

                    'add a new row
                    gvDetalleDocIngreso.AddNewRow()
                    'set a new row cell value. The static GridControl.NewItemRowHandle field allows you to retrieve the added row
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdPropietarioBodega", lcmbPropietario.EditValue)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "NombrePropietario", lcmbPropietario.Text)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "NoLinea", NoLinea)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdProductoBodega", Producto.pObjProducto.IdProductoBodega)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "CodigoProducto", Producto.pObjProducto.Codigo)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "NombreProducto", Producto.pObjProducto.Nombre)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "UMBas", Producto.pObjProducto.UnidadMedida.Nombre)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdPresentacion", Producto.pObjProducto.IdPresentacionOrigen)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Arancel", Producto.pObjProducto.Arancel.IdArancel)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdMotivoDevolucion", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Cantidad", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Cantidad_Recibida", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Cantidad_Pendiente", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "PesoBruto", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "PesoNeto", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Costo", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorAduana", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorFOB", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorIVA", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorDAI", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorSeguro", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ValorFlete", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "Total", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdProducto", Producto.pObjProducto.IdProducto)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IsNew", True)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdOrdenCompraEnc", gBeOrdenCompra.IdOrdenCompraEnc)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdOrdenCompraDet", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "CapturaArancel", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "EsKit", Producto.pObjProducto.Kit)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdPedidoCompraDet", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "IdOrdenCompraDetPadre", 0)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "ControlPeso", Producto.pObjProducto.Control_peso)
                    gvDetalleDocIngreso.SetRowCellValue(GridControl.NewItemRowHandle, "PesoReferenciaUMBas", Producto.pObjProducto.Peso_referencia)
                    gvDetalleDocIngreso.FocusedColumn = gvDetalleDocIngreso.Columns("Cantidad")
                    gvDetalleDocIngreso.RefreshRow(GridControl.NewItemRowHandle)
                    gvDetalleDocIngreso.ShowEditor()

                    Application.DoEvents()

                End If

            End Using

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdImprimeBarras_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimeBarras.ItemClick

        Try

            If gvDetalleDocIngreso.RowCount > 0 Then

                Dim lProductosOC As New List(Of clsBeProducto)
                Dim BeProducto As New clsBeProducto

                For Each Obj As clsBeTrans_oc_det In lOCDet
                    BeProducto = New clsBeProducto
                    BeProducto.Codigo = Obj.Producto.Codigo
                    BeProducto.Nombre = Obj.Producto.Nombre
                    BeProducto.Codigo_barra = Obj.Producto.Codigo_barra
                    lProductosOC.Add(BeProducto)
                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub xtraOrdenCompra_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles xtraOrdenCompra.SelectedPageChanged

        '#EJC20210602: Force the tabpage to refresh.
        xtraOrdenCompra.ResumeLayout(True)
        Application.DoEvents()

    End Sub

    Private Sub Cargar_Detalle_Rec()

        Try

            Dim pIdOrdenCompraEnc As Integer = gBeOrdenCompra.IdOrdenCompraEnc

            grdEncRec.BeginUpdate()

            Cargar_Encabezado_Rec()

            Cargar_Detalle_Rec(pIdOrdenCompraEnc)

            grdEncRec.EndUpdate()

            grdEncRec.ForceInitialize()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Encabezado_Rec()

        Dim lRow As DataRow

        Try

            Dim pIdOrdenCompraEnc As Integer = gBeOrdenCompra.IdOrdenCompraEnc

            listaOC = clsLnTrans_re_enc.Get_All_By_IdOrdenRecEnc(pIdOrdenCompraEnc)

            If Not listaOC Is Nothing Then

                Dim ListarOrdenCompra = From i In listaOC Group i By Keys = New With {Key i.IdRecepcionEnc, Key i.Bodega,
                                                          Key i.NoOrdencompra, Key i.NoDocumentoOC, Key i.Fecha_recepcion, Key i.Estado, Key i.IdTipoTransaccion, Key i.Descripcion, Key i.MuelleRec} Into Group
                                        Select New With {.ID = Keys.IdRecepcionEnc, .Bod = Keys.Bodega, .NoOC = Keys.NoOrdencompra, .DocOC = Keys.NoDocumentoOC,
                                                              .FeRec = Keys.Fecha_recepcion, .Est = Keys.Estado, .TiTrans = Keys.IdTipoTransaccion, .Desc = Keys.Descripcion, .Mue = Keys.MuelleRec}

                If ListarOrdenCompra IsNot Nothing AndAlso ListarOrdenCompra.Count > 0 Then

                    DsOC.Encabezado.Clear()

                    For Each Objs In ListarOrdenCompra

                        lRow = DsOC.Encabezado.NewRow
                        lRow.Item("Código") = Objs.ID
                        lRow.Item("Bodega") = Objs.Bod
                        lRow.Item("NoDocIngreso") = Objs.NoOC
                        lRow.Item("ReferenciaDI") = Objs.DocOC
                        lRow.Item("Fecha") = Objs.FeRec
                        lRow.Item("Estado") = Objs.Est
                        lRow.Item("Tipo_Transacción") = Objs.TiTrans
                        lRow.Item("Descripción") = Objs.Desc
                        lRow.Item("Muelle") = Objs.Mue

                        DsOC.Encabezado.AddEncabezadoRow(lRow)

                    Next

                End If

                If GridView6.RowCount > 0 Then
                    lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)
                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Detalle_Rec(ByVal IdOrdenCompraEnc As Integer)

        Dim lRow As DataRow
        Dim List As New List(Of clsBeTrans_re_det)

        Try

            List = clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(IdOrdenCompraEnc)

            If List IsNot Nothing AndAlso List.Count > 0 Then

                DsOC.Detalle.Clear()

                For Each Objs In List

                    lRow = DsOC.Detalle.NewRow
                    lRow.Item("IdProductoBodega") = Objs.IdProductoBodega
                    lRow.Item("No_Linea") = Objs.No_Linea
                    lRow.Item("nombre_producto") = Objs.Nombre_producto
                    lRow.Item("codigo_producto") = Objs.Codigo_Producto
                    lRow.Item("nombre_presentacion") = Objs.Nombre_presentacion
                    lRow.Item("nombre_unidad_medida") = Objs.Nombre_unidad_medida
                    lRow.Item("nombre_producto_estado") = Objs.Nombre_producto_estado
                    lRow.Item("lote") = Objs.Lote
                    lRow.Item("fecha_vence") = Objs.Fecha_vence.ToShortDateString()
                    lRow.Item("observacion") = Objs.Observacion
                    lRow.Item("IdRecepcionEnc") = Objs.IdRecepcionEnc
                    lRow.Item("CantidadRecibida") = Objs.cantidad_recibida
                    lRow.Item("IdPresentacion") = Objs.IdPresentacion
                    lRow.Item("peso") = Objs.Peso
                    lRow.Item("costo") = Objs.Costo
                    lRow.Item("costo_oc") = Objs.Costo_Oc
                    lRow.Item("costo_estadistico") = Objs.Costo_Estadistico
                    lRow.Item("IdRecepcionDet") = Objs.IdRecepcionDet
                    lRow.Item("Licencia") = Objs.Lic_plate
                    lRow.Item("Talla") = Objs.Talla.Codigo
                    lRow.Item("Color") = Objs.Color.Codigo

                    DsOC.Detalle.AddDetalleRow(lRow)

                Next

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdProveedor_EditValueChanged(sender As Object, e As EventArgs) Handles txtIdProveedor.EditValueChanged
        txtNombreProveedor.Text = String.Empty
    End Sub

    Private Sub grdEncRec_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdEncRec.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            gridView.OptionsView.ColumnAutoWidth = False
            gridView.BestFitColumns()

            If gridView.IsDetailView Then

                gridView.Columns("IdProductoBodega").Visible = False
                gridView.Columns("IdPresentacion").Visible = False
                gridView.Columns("No_Linea").Caption = "No. Línea"
                gridView.Columns("nombre_producto").Caption = "Producto"
                gridView.Columns("nombre_presentacion").Caption = "Presentación"
                gridView.Columns("nombre_unidad_medida").Caption = "Unidad Medida"
                gridView.Columns("nombre_producto_estado").Caption = "Estado"
                gridView.Columns("codigo_producto").Caption = "Código Producto"
                gridView.Columns("lote").Caption = "Lote"
                gridView.Columns("fecha_vence").Caption = "Fecha Vence"
                gridView.Columns("peso").Caption = "Peso"
                gridView.Columns("observacion").Caption = "Observación"
                gridView.Columns("costo").Caption = "Costo"
                gridView.Columns("costo_oc").Caption = "Costo OC"
                gridView.Columns("costo_estadistico").Caption = "Costo Estadistico"
                gridView.Columns("IdRecepcionEnc").Caption = "Id Recepcion"
                gridView.Columns("CantidadRecibida").Caption = "Cantidad Recibida"

            End If

            If gridView.Columns.Count > 0 Then

                gridView.OptionsView.ShowFooter = True

                gridView.Columns("costo").DisplayFormat.FormatType = FormatType.Numeric
                gridView.Columns("costo").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("costo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("costo").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("peso").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("peso").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("costo_oc").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("costo_oc").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("costo_estadistico").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("costo_estadistico").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("CantidadRecibida").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                gridView.Columns("CantidadRecibida").DisplayFormat.FormatString = "{0:n6}"

                gridView.Columns("CantidadRecibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gridView.Columns("CantidadRecibida").SummaryItem.DisplayFormat = "{0:n6}"

                gridView.Columns("No_Linea").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                gridView.Columns("No_Linea").SummaryItem.DisplayFormat = "Registros: " & "{0:0}"

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try
            clsUiPrintHelper.PrintGridPreview(grdEncRec, AP.UsuarioAp.Nombres, AddressOf PrintableComponentLink_CreateReportHeaderArea, True)
            Exit Sub
            GridView6.OptionsPrint.ExpandAllDetails = True
            GridView6.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            RemoveHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea
            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, String.Empty, rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdEncRec
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de recepción"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    'Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs)

    '    Try

    '        If cmbBodega.EditValue <> 0 Then

    '            BeBodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue)
    '            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue, AP.IdEmpresa)

    '            Listar_Propietarios()

    '            IMS.Listar_TipoIngresoOC(cmbTipoIngreso, BeBodega.Es_Bodega_Fiscal)

    '            '#EJC20210922: En teoría esto no debería ser necesario porque al listar y hacer el set, se ejecuta, éste método.
    '            'Set_Tipo_Documento()

    '        End If

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
    '        Text,
    '        MessageBoxButtons.OK,
    '        MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            lcmbPropietario.Properties.DataSource = DT1
            lcmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            lcmbPropietario.Properties.DisplayMember = "Nombre"
            lcmbPropietario.Properties.PopupWidth = 700
            lcmbPropietario.Properties.BestFit()
            lcmbPropietario.Properties.PopulateColumns()

            If lcmbPropietario.Properties.Columns.Count > 0 Then
                lcmbPropietario.Properties.Columns(0).Visible = False
                lcmbPropietario.Properties.Columns(1).Visible = False
            End If

            lcmbPropietario.Properties.NullText = String.Empty

            If Not DT1 Is Nothing Then
                If DT1.Rows.Count = 1 Then
                    lcmbPropietario.Text = DT1.Rows(0).Item("Nombre").ToString
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private DTGridDetalleDocIngresos As New DataTable("DetalleIngreso")

    Private Sub Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

        DTGridDetalleDocIngresos.Columns.Clear()
        DTGridDetalleDocIngresos.Columns.Add("IdPropietarioBodega", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("NombrePropietario", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("NoLinea", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdProductoBodega", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("CodigoProducto", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("NombreProducto", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("UMBas", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("IdUmBas", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdPresentacion", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("Arancel", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdMotivoDevolucion", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad_Recibida", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Cantidad_Pendiente", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("PesoBruto", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("PesoNeto", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Costo", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorAduana", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorFOB", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorIVA", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorDAI", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorSeguro", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("ValorFlete", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Total", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("IdProducto", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IsNew", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraEnc", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraDet", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("CapturaArancel", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("Variant_Code", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("EsKit", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("IdPedidoCompraDet", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("IdOrdenCompraDetPadre", GetType(Integer))
        DTGridDetalleDocIngresos.Columns.Add("ControlPeso", GetType(Boolean))
        DTGridDetalleDocIngresos.Columns.Add("PesoReferenciaUMBas", GetType(Double))
        DTGridDetalleDocIngresos.Columns.Add("Embarcador", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("Clasificacion", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("Talla", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("Color", GetType(String))
        DTGridDetalleDocIngresos.Columns.Add("SKU", GetType(String))

    End Sub

    Private Sub Set_Columnas_Grid_Detalle_Documento_Ingreso()

        Try

            DgridDetalleOC.DataSource = DTGridDetalleDocIngresos

            Dim ColIndexAux As Integer = 0

            gvDetalleDocIngreso.OptionsView.ShowFooter = True
            gvDetalleDocIngreso.OptionsView.ShowGroupPanel = False

            gvDetalleDocIngreso.OptionsView.ColumnAutoWidth = False

            gvDetalleDocIngreso.Columns.Clear()

#Region "Columna - Propietario"

            PropietarioGridLookUpEdit.View.Columns.Clear()

            PropietarioGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPropietarioBodega", .Caption = "IdPropietario", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PropietarioGridLookUpEdit.ValueMember = "IdPropietarioBodega"
            PropietarioGridLookUpEdit.DisplayMember = "Nombre"
            PropietarioGridLookUpEdit.NullText = String.Empty
            PropietarioGridLookUpEdit.DataSource = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            PropietarioGridLookUpEdit.PopupFormWidth = 700
            PropietarioGridLookUpEdit.View.BestFitColumns()

            RemoveHandler PropietarioGridLookUpEdit.Leave, AddressOf PropietarioGridLookUpEditDetalleIngreso_Leave
            AddHandler PropietarioGridLookUpEdit.Leave, AddressOf PropietarioGridLookUpEditDetalleIngreso_Leave

            PropietarioGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPropieario As New GridColumn With {
                .FieldName = "IdPropietarioBodega",
                .Caption = "Propietario",
                .Visible = True,
                .VisibleIndex = 0,
                .ColumnEdit = PropietarioGridLookUpEdit
            }
            ColPropieario.Width = 300
            ColPropieario.OptionsColumn.AllowEdit = True
            ColPropieario.Visible = (TipoTrans = eTipoTrans.Consolidado)
            gvDetalleDocIngreso.Columns.Add(ColPropieario)

#End Region

#Region "Columna - No_Linea"

            Dim ColNoLinea As New GridColumn With {
                .FieldName = "NoLinea",
                .Caption = "No. Linea",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = 1
            }

            ColNoLinea.OptionsColumn.AllowEdit = True

            gvDetalleDocIngreso.Columns.Add(ColNoLinea)


#End Region

#Region "Columna - IdProductoBodega"

            ProductoGridLookUpEdit.View.Columns.Clear()

            ProductoGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdProductoBodega", .Caption = "IdProductoBodega", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "CodigoBarra", .Caption = "CodigoBarra", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True},
                New GridColumn With {.FieldName = "UMBas", .Caption = "UMBas", .Visible = True},
                New GridColumn With {.FieldName = "IdUmBas", .Caption = "IdUmBas", .Visible = False},
                New GridColumn With {.FieldName = "ControlPeso", .Caption = "ControlPeso", .Visible = False},
                New GridColumn With {.FieldName = "Familia", .Caption = "Familia", .Visible = True},
                New GridColumn With {.FieldName = "Clasificacion", .Caption = "Clasificacion", .Visible = True},
                New GridColumn With {.FieldName = "TipoProducto", .Caption = "TipoProducto", .Visible = True}
               })

            ProductoGridLookUpEdit.ValueMember = "IdProductoBodega"
            ProductoGridLookUpEdit.DisplayMember = "Codigo"
            ProductoGridLookUpEdit.NullText = "-> Producto"
            ProductoGridLookUpEdit.PopupFormWidth = 700
            ProductoGridLookUpEdit.ImmediatePopup = True
            '#EJC20260522_OC_PRODUCT_LOOKUP_LAZY: no cargar todos los productos al abrir; se asigna al editar IdProductoBodega.
            ProductoGridLookUpEdit.DataSource = OCProductoLookup_DataSourceVacio()
            ProductoGridLookUpEdit.View.BestFitColumns()

            '#EJC20240326: Agregar esto + evento keydown para escanear en celda.
            ProductoGridLookUpEdit.TextEditStyle = TextEditStyles.Standard
            ProductoGridLookUpEdit.SearchMode = SearchMode.AutoSuggest
            ProductoGridLookUpEdit.View.OptionsFind.AlwaysVisible = True
            ProductoGridLookUpEdit.View.OptionsFind.FindMode = FindMode.Always
            ProductoGridLookUpEdit.View.OptionsFind.SearchInPreview = False
            ProductoGridLookUpEdit.View.OptionsFind.FindFilterColumns = "*"
            ProductoGridLookUpEdit.View.BestFitColumns()

            RemoveHandler ProductoGridLookUpEdit.KeyDown, AddressOf ProductoGridLookUpEdit_KeyDown
            AddHandler ProductoGridLookUpEdit.KeyDown, AddressOf ProductoGridLookUpEdit_KeyDown

            RemoveHandler ProductoGridLookUpEdit.Leave, AddressOf ProductoGridLookUpEdit_Leave
            AddHandler ProductoGridLookUpEdit.Leave, AddressOf ProductoGridLookUpEdit_Leave

            RemoveHandler ProductoGridLookUpEdit.ProcessNewValue, AddressOf ProductoGridLookUpEdit_ProcessNewValue
            AddHandler ProductoGridLookUpEdit.ProcessNewValue, AddressOf ProductoGridLookUpEdit_ProcessNewValue

            ProductoGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColIdProductoBodega As New GridColumn With {
                .FieldName = "IdProductoBodega",
                .Caption = "Código",
                .Visible = True,
                .VisibleIndex = 2,
                .ColumnEdit = ProductoGridLookUpEdit
            }

            '#EJC20210306: Permitir el ingreso de valores que no estén en la lista.
            ProductoGridLookUpEdit.AcceptEditorTextAsNewValue = DefaultBoolean.True

            '#GT03102023: cuando no sea nuevo, evitar que cambien el producto
            Select Case Modo
                Case ModoTrans.Nuevo
                    ColIdProductoBodega.OptionsColumn.AllowEdit = True
                Case ModoTrans.Editar
                    ColIdProductoBodega.OptionsColumn.AllowEdit = False
            End Select

            'ColIdProductoBodega.OptionsColumn.AllowEdit = True
            ColIdProductoBodega.Width = 150
            gvDetalleDocIngreso.Columns.Add(ColIdProductoBodega)

#End Region

#Region "Columna - CodigoProducto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "CodigoProducto",
                .Caption = "CodigoProducto",
                .Width = 100,
                .VisibleIndex = 3
            }

            ColCodigoProducto.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColCodigoProducto)


#End Region

#Region "Columna - Nombre_Producto"

            Dim ColNombreProducto As New GridColumn With {
                .FieldName = "NombreProducto",
                .Caption = "Nombre",
                .Visible = True,
                .VisibleIndex = 4,
                .Width = 200
            }

            ColNombreProducto.OptionsColumn.AllowEdit = False

            gvDetalleDocIngreso.Columns.Add(ColNombreProducto)

#End Region

#Region "Columna - NomUMBas"


            Dim ColUMBas As New GridColumn With {
                .FieldName = "UMBas",
                .Caption = "UMBas",
                .Visible = True,
                .VisibleIndex = 4,
                .Width = 75
            }

            ColUMBas.OptionsColumn.AllowEdit = False

            gvDetalleDocIngreso.Columns.Add(ColUMBas)

#End Region

#Region "Columna - IdUMBas"

            Dim ColIdUmBas As New GridColumn With {
                .FieldName = "IdUmBas",
                .Caption = "IdUmBas",
                .VisibleIndex = 5,
                .Width = 75
            }

            ColIdUmBas.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdUmBas)

#End Region

#Region "Columna - Presentacion"

            PresentacionGridLookUpEdit.View.Columns.Clear()

            PresentacionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdPresentacion", .Caption = "IdPresentacion", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            PresentacionGridLookUpEdit.ValueMember = "IdPresentacion"
            PresentacionGridLookUpEdit.DisplayMember = "Nombre"
            PresentacionGridLookUpEdit.NullText = String.Empty
            PresentacionGridLookUpEdit.DataSource = clsLnProducto_presentacion.Get_All_By_IdBodega(cmbBodega.EditValue)
            PresentacionGridLookUpEdit.View.BestFitColumns()
            PresentacionGridLookUpEdit.PopupFormWidth = 700

            'RemoveHandler PresentacionGridLookUpEdit.Leave, AddressOf PresentacionGridLookUpEditDetalleServicio_Leave
            'AddHandler PresentacionGridLookUpEdit.Leave, AddressOf PresentacionGridLookUpEditDetalleServicio_Leave

            RemoveHandler PresentacionGridLookUpEdit.KeyDown, AddressOf PresentacionGridLookUpEditDetalleServicio_KeyDown
            AddHandler PresentacionGridLookUpEdit.KeyDown, AddressOf PresentacionGridLookUpEditDetalleServicio_KeyDown

            PresentacionGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColPresentacion As New GridColumn With {
                .FieldName = "IdPresentacion",
                .Caption = "Presentacion",
                .Visible = True,
                .VisibleIndex = 6,
                .ColumnEdit = PresentacionGridLookUpEdit
            }

            ColPresentacion.Width = 100
            ColPresentacion.OptionsColumn.AllowEdit = True
            gvDetalleDocIngreso.Columns.Add(ColPresentacion)

#End Region

#Region "Columna - Motivo_Devolución"

            '#EJC20210307: Agregar siempre la columna y ocultar después.
            'If EsDevolucion Then
            'End If

            MotivoDevolcuionGridLookUpEdit.View.Columns.Clear()

            MotivoDevolcuionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdMotivoDevolucion", .Caption = "Código", .Visible = False},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            MotivoDevolcuionGridLookUpEdit.DataSource = clsLnMotivo_devolucion.Get_All_By_IdPropietario_And_Bodega_DT(lcmbPropietario.Tag, cmbBodega.EditValue)
            MotivoDevolcuionGridLookUpEdit.ValueMember = "IdMotivoDevolucion"
            MotivoDevolcuionGridLookUpEdit.DisplayMember = "Nombre"
            MotivoDevolcuionGridLookUpEdit.NullText = String.Empty
            MotivoDevolcuionGridLookUpEdit.View.BestFitColumns()

            Dim ColMotivoDevolucion As New GridColumn With {
                  .FieldName = "IdMotivoDevolucion",
                  .Caption = "Motivo Devolución",
                  .Visible = True,
                  .Width = 150,
                  .VisibleIndex = 7,
                  .ColumnEdit = MotivoDevolcuionGridLookUpEdit
              }

            ColMotivoDevolucion.OptionsColumn.AllowEdit = True

            gvDetalleDocIngreso.Columns.Add(ColMotivoDevolucion)

#End Region

#Region "Columna - Cantidad"

            ColIndexAux = 8

            Dim ColCantidad As New GridColumn With {
                .FieldName = "Cantidad",
                .Caption = "Cantidad",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidad.OptionsColumn.AllowEdit = True
            ColCantidad.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidad.DisplayFormat.FormatString = "{0:n6}"

            'GT 10062021 No funciona en este momento.
            RemoveHandler txtCantidadGrid.Leave, AddressOf txtCantidadGrid_Leave
            AddHandler txtCantidadGrid.Leave, AddressOf txtCantidadGrid_Leave

            gvDetalleDocIngreso.Columns.Add(ColCantidad)

            gvDetalleDocIngreso.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad").SummaryItem.DisplayFormat = "Cantidad: {0:n6}"

            gvDetalleDocIngreso.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - Cantidad_Recibida"

            ColIndexAux = 8

            Dim ColCantidadRecibida As New GridColumn With {
                .FieldName = "Cantidad_Recibida",
                .Caption = "Cantidad Recibida",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadRecibida.OptionsColumn.AllowEdit = False
            ColCantidadRecibida.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadRecibida.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColCantidadRecibida)

            gvDetalleDocIngreso.Columns("Cantidad_Recibida").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").SummaryItem.DisplayFormat = "Recibido: {0:n6}"
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad_Recibida").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - Cantidad_Pendiente"

            ColIndexAux = 8

            Dim ColCantidadPendiente As New GridColumn With {
                .FieldName = "Cantidad_Pendiente",
                .Caption = "Cantidad Pendiente",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = ColIndexAux
            }

            ColCantidadPendiente.OptionsColumn.AllowEdit = False
            ColCantidadPendiente.DisplayFormat.FormatType = FormatType.Numeric
            ColCantidadPendiente.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColCantidadPendiente)

            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").SummaryItem.DisplayFormat = "Pendiente: {0:n6}"
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Cantidad_Pendiente").DisplayFormat.FormatString = "{0:n6}"

#End Region

#Region "Columna - PesoBruto"

            Dim ColPesoBruto As New GridColumn With {
                .FieldName = "PesoBruto",
                .Caption = "Peso Bruto",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux
            }

            ColPesoBruto.OptionsColumn.AllowEdit = True
            ColPesoBruto.DisplayFormat.FormatType = FormatType.Numeric
            ColPesoBruto.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColPesoBruto)

            gvDetalleDocIngreso.Columns("PesoBruto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("PesoBruto").SummaryItem.DisplayFormat = "Peso bruto: {0:n6}"

            gvDetalleDocIngreso.Columns("PesoBruto").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("PesoBruto").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - PesoNeto"

            Dim ColPesoNeto As New GridColumn With {
                .FieldName = "PesoNeto",
                .Caption = "Peso Neto",
                .Visible = True,
                .Width = 100,
                .VisibleIndex = ColIndexAux
            }

            ColPesoNeto.OptionsColumn.AllowEdit = True
            ColPesoNeto.DisplayFormat.FormatType = FormatType.Numeric
            ColPesoNeto.DisplayFormat.FormatString = "{0:n6}"

            gvDetalleDocIngreso.Columns.Add(ColPesoNeto)

            gvDetalleDocIngreso.Columns("PesoNeto").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("PesoNeto").SummaryItem.DisplayFormat = "Peso neto: {0:n6}"

            gvDetalleDocIngreso.Columns("PesoNeto").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("PesoNeto").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - Costo"

            Dim ColCosto As New GridColumn With {
                .FieldName = "Costo",
                .Caption = "Costo_Unitario",
                .Visible = True,
                .Width = 100,
                 .ColumnEdit = txtCostoGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            'GT Se remueve, costo se hace por otro método
            RemoveHandler txtCostoGrid.Leave, AddressOf txtCostoGrid_Leave
            AddHandler txtCostoGrid.Leave, AddressOf txtCostoGrid_Leave

            ColCosto.OptionsColumn.AllowEdit = True
            ColCosto.DisplayFormat.FormatType = FormatType.Numeric
            ColCosto.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColCosto)

            gvDetalleDocIngreso.Columns("Costo").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Costo").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorAduana"

            Dim ColValorAduana As New GridColumn With {
                .FieldName = "ValorAduana",
                .Caption = "Valor Aduana",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtValorADUANAGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorAduana.OptionsColumn.AllowEdit = True
            ColValorAduana.DisplayFormat.FormatType = FormatType.Numeric
            ColValorAduana.DisplayFormat.FormatString = "{0:n6}"


            'GT08092022_1650: se calcula IVA sobre esta columna y sobre DAI
            RemoveHandler txtValorADUANAGrid.Leave, AddressOf txtValorAduana_Leave
            AddHandler txtValorADUANAGrid.Leave, AddressOf txtValorAduana_Leave

            gvDetalleDocIngreso.Columns.Add(ColValorAduana)

            gvDetalleDocIngreso.Columns("ValorAduana").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorAduana").SummaryItem.DisplayFormat = "Aduana: {0:n6}"
            gvDetalleDocIngreso.Columns("ValorAduana").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorAduana").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorFOB"

            Dim ColValorFOB As New GridColumn With {
                .FieldName = "ValorFOB",
                .Caption = "Valor FOB",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtValorFOBGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorFOB.OptionsColumn.AllowEdit = True
            ColValorFOB.DisplayFormat.FormatType = FormatType.Numeric
            ColValorFOB.DisplayFormat.FormatString = "{0:n6}"

            'GT 09062021 Esta columna suma al valor calculado del IVA
            RemoveHandler txtValorFOBGrid.Leave, AddressOf txtValorFOB_Leave
            AddHandler txtValorFOBGrid.Leave, AddressOf txtValorFOB_Leave

            gvDetalleDocIngreso.Columns.Add(ColValorFOB)

            gvDetalleDocIngreso.Columns("ValorFOB").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorFOB").SummaryItem.DisplayFormat = "FOB: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorFOB").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorFOB").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorDAI"

            Dim ColValorDAI As New GridColumn With {
                .FieldName = "ValorDAI",
                .Caption = "Valor DAI",
                .Visible = True,
                .Width = 100,
                  .ColumnEdit = txtValorDAIGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorDAI.OptionsColumn.AllowEdit = True
            ColValorDAI.DisplayFormat.FormatType = FormatType.Numeric
            ColValorDAI.DisplayFormat.FormatString = "{0:n6}"

            'GT 09062021 esta columna suma al valor total
            RemoveHandler txtValorDAIGrid.Leave, AddressOf txtValorDAI_Leave
            AddHandler txtValorDAIGrid.Leave, AddressOf txtValorDAI_Leave

            gvDetalleDocIngreso.Columns.Add(ColValorDAI)

            gvDetalleDocIngreso.Columns("ValorDAI").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorDAI").SummaryItem.DisplayFormat = "DAI: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorDAI").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorDAI").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorIVA"

            Dim ColValorIVA As New GridColumn With {
                .FieldName = "ValorIVA",
                .Caption = "Valor IVA",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtValorIVAGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            '#CKFK 20210825 No se debe permitir modificar el IVA, es un valor calculado
            ColValorIVA.OptionsColumn.AllowEdit = False
            ColValorIVA.DisplayFormat.FormatType = FormatType.Numeric
            ColValorIVA.DisplayFormat.FormatString = "{0:n6}"
            'ColValorIVA.OptionsColumn.AllowEdit = True

            gvDetalleDocIngreso.Columns.Add(ColValorIVA)

            gvDetalleDocIngreso.Columns("ValorIVA").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorIVA").SummaryItem.DisplayFormat = "IVA: {0:n6}"
            gvDetalleDocIngreso.Columns("ValorIVA").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorIVA").DisplayFormat.FormatString = "{0:n6}"


            'GT 09062021 Esta columna suma al valor calculado del IVA
            RemoveHandler txtValorIVAGrid.Leave, AddressOf txtValorIVA_Leave
            AddHandler txtValorIVAGrid.Leave, AddressOf txtValorIVA_Leave

            ColIndexAux += 1

#End Region

#Region "Columna - ValorSeguro"

            Dim ColValorSeguro As New GridColumn With {
                .FieldName = "ValorSeguro",
                .Caption = "Valor Seguro",
                .Visible = True,
                .Width = 100,
                   .ColumnEdit = txtvalorSeguroGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            'GT 09062021 Esta columna suma al valor calculado del IVA
            RemoveHandler txtvalorSeguroGrid.Leave, AddressOf txtValorSeguro_Leave
            AddHandler txtvalorSeguroGrid.Leave, AddressOf txtValorSeguro_Leave

            ColValorSeguro.OptionsColumn.AllowEdit = True
            ColValorSeguro.DisplayFormat.FormatType = FormatType.Numeric
            ColValorSeguro.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorSeguro)

            gvDetalleDocIngreso.Columns("ValorSeguro").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorSeguro").SummaryItem.DisplayFormat = "Seguro: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorSeguro").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorSeguro").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorFlete"

            Dim ColValorFlete As New GridColumn With {
                .FieldName = "ValorFlete",
                .Caption = "Valor Flete",
                .Visible = True,
                .Width = 100,
                   .ColumnEdit = txtValorFleteGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            'GT 09062021 esta columna suma al valor calculado para IVA
            RemoveHandler txtValorFleteGrid.Leave, AddressOf txtValorFlete_Leave
            AddHandler txtValorFleteGrid.Leave, AddressOf txtValorFlete_Leave

            ColValorFlete.OptionsColumn.AllowEdit = True
            ColValorFlete.DisplayFormat.FormatType = FormatType.Numeric
            ColValorFlete.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorFlete)

            gvDetalleDocIngreso.Columns("ValorFlete").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("ValorFlete").SummaryItem.DisplayFormat = "Flete: {0:n6}"

            gvDetalleDocIngreso.Columns("ValorFlete").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("ValorFlete").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - ValorTotal"

            Dim ColValorTotal As New GridColumn With {
                .FieldName = "Total",
                .Caption = "Valor Total",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtTotalGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColValorTotal.OptionsColumn.AllowEdit = False
            ColValorTotal.DisplayFormat.FormatType = FormatType.Numeric
            ColValorTotal.DisplayFormat.FormatString = "{0:n6}"
            gvDetalleDocIngreso.Columns.Add(ColValorTotal)

            gvDetalleDocIngreso.Columns("Total").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gvDetalleDocIngreso.Columns("Total").SummaryItem.DisplayFormat = "Total: {0:n6}"
            gvDetalleDocIngreso.Columns("Total").DisplayFormat.FormatType = FormatType.Numeric
            gvDetalleDocIngreso.Columns("Total").DisplayFormat.FormatString = "{0:n6}"

            ColIndexAux += 1

#End Region

#Region "Columna - IsNew"

            Dim ColIsNew As New GridColumn With {
                .FieldName = "IsNew",
                .Caption = "IsNew",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIsNew.Visible = False
            ColIsNew.OptionsColumn.AllowEdit = True
            ColIsNew.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColIsNew)

            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraDetPadre"

            Dim ColIdOrdenCompraDetPadre As New GridColumn With {
                .FieldName = "IdOrdenCompraDetPadre",
                .Caption = "IdOrdenCompraDetPadre",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIdOrdenCompraDetPadre.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdOrdenCompraDetPadre)

            ColIndexAux += 1

#End Region

#Region "Columna - ControlPeso"

            Dim ColControlPeso As New GridColumn With {
                .FieldName = "ControlPeso",
                .Caption = "ControlPeso",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColControlPeso.Visible = False
            ColControlPeso.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColControlPeso)

            ColIndexAux += 1

#End Region

#Region "Columna - PesoReferenciaUMBas"

            Dim ColPesoRefUMBas As New GridColumn With {
                .FieldName = "PesoReferenciaUMBas",
                .Caption = "PesoReferenciaUMBas",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColControlPeso.Visible = False
            ColControlPeso.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleDocIngreso.Columns.Add(ColPesoRefUMBas)

            ColIndexAux += 1

#End Region

#Region "Columna - IdOrdenCompraDet"

            Dim ColIdOrdenCompraDet As New GridColumn With {
                .FieldName = "ColIdOrdenCompraDet",
                .Caption = "ColIdOrdenCompraDet",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIdOrdenCompraDet.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColIdOrdenCompraDet)

            ColIndexAux += 1

#End Region

#Region "Columna - NombrePropietario"

            Dim ColNombrePropietario As New GridColumn With {
                .FieldName = "NombrePropietario",
                .Caption = "Propietario",
                .Visible = False,
                .Width = 100,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColNombrePropietario.OptionsColumn.AllowEdit = False
            ColNombrePropietario.Visible = False
            gvDetalleDocIngreso.Columns.Add(ColNombrePropietario)

            ColIndexAux += 1
#End Region

#Region "Columna - Shipper/Embarcador"

            Dim ColNombreEmbarcador As New GridColumn With {
                .FieldName = "Embarcador",
                .Caption = "Embarcador",
                .Visible = False,
                .Width = 150,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColNombreEmbarcador.OptionsColumn.AllowEdit = False
            ColNombreEmbarcador.Visible = True
            gvDetalleDocIngreso.Columns.Add(ColNombreEmbarcador)

            ColIndexAux += 1
#End Region

#Region "Columna - Clasificacion"

            Dim ColClasificacion As New GridColumn With {
                .FieldName = "Clasificacion",
                .Caption = "Clasificación",
                .Visible = False,
                .Width = 150,
                .ColumnEdit = txtNoLineaGrid,
                .VisibleIndex = ColIndexAux + 1
            }

            ColClasificacion.OptionsColumn.AllowEdit = False
            ColClasificacion.Visible = True
            gvDetalleDocIngreso.Columns.Add(ColClasificacion)

            ColIndexAux += 1
#End Region

#Region "Talla_Color"

            If BeBodega.Control_Talla_Color Then

                TallaGridLookUpEdit.View.Columns.Clear()
                TallaGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                                                          New GridColumn With {.FieldName = "IdTalla", .Caption = "IdTalla", .Visible = False},
                                                          New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True}
                })

                TallaGridLookUpEdit.ValueMember = "IdTalla"
                TallaGridLookUpEdit.DisplayMember = "Codigo"
                TallaGridLookUpEdit.NullText = String.Empty
                TallaGridLookUpEdit.DataSource = clsLnTalla.Listar()

                TallaGridLookUpEdit.TextEditStyle = TextEditStyles.Standard
                TallaGridLookUpEdit.SearchMode = SearchMode.AutoSuggest
                TallaGridLookUpEdit.View.OptionsFind.AlwaysVisible = True
                TallaGridLookUpEdit.View.OptionsFind.FindMode = FindMode.Always
                TallaGridLookUpEdit.View.OptionsFind.SearchInPreview = False
                TallaGridLookUpEdit.View.OptionsFind.FindFilterColumns = "*"

                TallaGridLookUpEdit.View.BestFitColumns()

                TallaGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

#Region "Columna - Talla"

                Dim ColTalla As New GridColumn With {
                .FieldName = "Talla",
                .Caption = "Talla",
                .Width = 150,
                .ColumnEdit = TallaGridLookUpEdit,
                .VisibleIndex = ColIndexAux + 1
            }

                ColTalla.OptionsColumn.AllowEdit = True
                ColTalla.Visible = True
                gvDetalleDocIngreso.Columns.Add(ColTalla)

                ColIndexAux += 1
#End Region

#Region "Columna - Color"

                ColorGridLookUpEdit.View.Columns.Clear()
                ColorGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                                                          New GridColumn With {.FieldName = "IdColor", .Caption = "IdColor", .Visible = False},
                                                          New GridColumn With {.FieldName = "Codigo", .Caption = "Codigo", .Visible = True},
                                                          New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
                })

                ColorGridLookUpEdit.ValueMember = "IdColor"
                ColorGridLookUpEdit.DisplayMember = "Codigo"
                ColorGridLookUpEdit.NullText = String.Empty
                ColorGridLookUpEdit.DataSource = clsLnColor.Listar()

                ColorGridLookUpEdit.TextEditStyle = TextEditStyles.Standard
                ColorGridLookUpEdit.SearchMode = SearchMode.AutoSuggest
                ColorGridLookUpEdit.View.OptionsFind.AlwaysVisible = True
                ColorGridLookUpEdit.View.OptionsFind.FindMode = FindMode.Always
                ColorGridLookUpEdit.View.OptionsFind.SearchInPreview = False
                ColorGridLookUpEdit.View.OptionsFind.FindFilterColumns = "*"

                ColorGridLookUpEdit.View.BestFitColumns()

                ColorGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True


                Dim ColColor As New GridColumn With {
                .FieldName = "Color",
                .Caption = "Color",
                .Width = 150,
                .ColumnEdit = ColorGridLookUpEdit,
                .VisibleIndex = ColIndexAux + 1
            }

                ColColor.OptionsColumn.AllowEdit = True
                ColColor.Visible = True
                gvDetalleDocIngreso.Columns.Add(ColColor)

                ColIndexAux += 1
#End Region

#Region "Columna - SKU"

                Dim ColSKU As New GridColumn With {
                .FieldName = "SKU",
                .Caption = "SKU",
                .Visible = True,
                .Width = 150,
                .ColumnEdit = txtSKUGrid,
                .VisibleIndex = ColIndexAux + 1
            }

                ColSKU.OptionsColumn.AllowEdit = False
                ColSKU.Visible = True
                gvDetalleDocIngreso.Columns.Add(ColSKU)

                ColIndexAux += 1
#End Region

            End If

#End Region

            gvDetalleDocIngreso.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorFlete_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorSeguro_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            '#GT08092022_1645: no se requiere calculo, todo se va sobre VALOR ADUANA Y DAI
            'Calcula_Iva()
            'GT 21062021 se recalcula total porque IVA afecta el total
            'Recalcula_Valor_Total()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorFOB_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            '#GT08092022_1645: No se requieren calculos
            'Calcula_Iva()
            'Recalcula_Valor_Total()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorDAI_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Calcula_Iva()
            Recalcula_Valor_Total()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorAduana_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Calcula_Iva()
            Recalcula_Valor_Total()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Recalcula_Valor_Total()

        Try

            '#GT08092022_1640: El valor total se obtiene de VALOR ADUANA+DAI 
            'Dim ValorFOB As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFOB")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFOB"))
            'Dim ValorFlete As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFlete")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFlete"))
            'Dim ValorSeguro As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorSeguro")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorSeguro"))

            Dim ValorIVA As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorIVA")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorIVA"))
            Dim ValorDAI As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorDAI")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorDAI"))
            Dim ValorAduana As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana"))
            Dim Total As Double

            If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
                    cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA OrElse
                     cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then

                '#GT07092022_1818: el valor total se calcula sobre el valor aduana + dai + iva
                'Total = Math.Round(ValorFOB + ValorFlete + ValorSeguro + ValorDAI + ValorIVA, 2)
                Total = Math.Round(ValorAduana + ValorDAI + ValorIVA, 2)
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)

            Else
                Debug.WriteLine("aqui no toca")
            End If

            gvDetalleDocIngreso.PostEditor()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    ''' <summary>
    ''' #EJC20210607:Calcular iva si tiene valor definido por bodega.
    ''' </summary>
    Private Sub Calcula_Iva()

        Try

            'GT 18062021 el DAI no suma al FOB, suma al VALOR ADUANA (CIF-Q)
            'Dim ValorFOB As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFOB")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFOB"))
            'Dim ValorFlete As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFlete")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorFlete"))
            'Dim ValorSeguro As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorSeguro")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorSeguro"))
            Dim ValorDAI As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorDAI")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorDAI"))
            Dim ValorAduana As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana"))

            '#GT10052022_1026: cambie a BeBodega, porque se actualiza si cambia el valor en el combo.
            'GT12072022: con bodega fiscal y % de iva, se hace calculo
            If BeBodega.Es_Bodega_Fiscal And BeBodega.valor_porcentaje_iva <> 0 Then
                '#GT08092022_1630: La nueva base imponible es sobre Valor Aduana y Dai
                'Dim vBaseImponible As Double = (ValorFOB + ValorFlete + ValorSeguro)
                Dim vBaseImponible As Double = (ValorAduana + ValorDAI)
                Dim vValorIVA As Double = Math.Round((vBaseImponible) * (AP.Bodega.valor_porcentaje_iva / 100), 2)
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorIVA", vValorIVA)
                gvDetalleDocIngreso.PostEditor()

            ElseIf BeBodega.Es_Bodega_Fiscal And BeBodega.valor_porcentaje_iva = 0 Then

                '#GT02112022_1640: sino es fiscal y no tiene iva no pasa nada, para cealsa.
                Throw New Exception("#20220204_1820A: No hay % de iva registrado para la bodega" & BeBodega.Nombre)
            Else
                '#GT21072023: no pasa nada si no hay iva para bodega general
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCantidadGrid_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Dim Costo As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo"))
            Dim Total As Double = Math.Round(lista.EditValue * Costo, 2)
            Dim ControlPeso As Boolean = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ControlPeso")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ControlPeso"))
            Dim PesoReferenciaUMBas As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoReferenciaUMBas")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoReferenciaUMBas"))
            Dim IdPresentacion As Double = CType(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), Integer)
            Dim Cantidad As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad"))
            Dim vValorCostoUnitario As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo"))
            Dim vPesoTotal As Double = 0

            If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
                cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana", Total)
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)
            Else
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)
            End If

            If ControlPeso Then

                vPesoTotal = Math.Round(Val(vBeProducto.Peso_referencia) * Val(Cantidad), 6)

                If IdPresentacion = 0 Then

                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoBruto", vPesoTotal)
                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoNeto", vPesoTotal)

                Else

                    Dim BePresentacion As New clsBeProducto_Presentacion()
                    BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacion)

                    If Not BePresentacion Is Nothing Then

                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoBruto", BePresentacion.Peso * Cantidad)
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoNeto", BePresentacion.Peso * Cantidad)

                        Total = Math.Round(Cantidad * BePresentacion.Factor * Costo, 2)

                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana", Total)
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)

                    End If

                End If

            End If

            If IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion()
                BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacion)

                If Not BePresentacion Is Nothing Then

                    Total = Math.Round(Cantidad * BePresentacion.Factor * Costo, 2)

                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana", Total)
                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)

                End If

            End If

            '#EJC20210609: Corrección de calculos por tipo de documento.
            If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
                cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then

                Dim ColCostoUnitario As GridColumn = gvDetalleDocIngreso.Columns("Costo")

                If vValorCostoUnitario = 0 Then
                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo", "0.01")
                    gvDetalleDocIngreso.SetColumnError(ColCostoUnitario, "")
                End If

            End If

            gvDetalleServicios.PostEditor()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtCostoGrid_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Dim Costo As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo"))
            Dim Cantidad As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad"))
            Dim IdPresentacion As Double = CType(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), Integer)
            Dim Total As Double = Math.Round(Cantidad * Costo, 2)

            '#EJC20210609: Validar por tipo de documento el cálculo.
            If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
                    cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana", Total)
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)
            Else
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)
            End If

            If IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion()
                BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacion)

                If Not BePresentacion Is Nothing Then

                    Total = Math.Round(Cantidad * BePresentacion.Factor * Costo, 2)

                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ValorAduana", Total)
                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Total", Total)

                End If

            End If

            gvDetalleServicios.PostEditor()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtValorIVA_Leave(sender As Object, e As EventArgs)

        Try

            Dim lista As SpinEdit = TryCast(sender, SpinEdit)
            If lista.EditValue Is Nothing Then Return

            Recalcula_Valor_Total()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private DTGridDetalleServicios As New DataTable("DetalleServicios")

    Private Sub Set_Datata_Table_Grid_Detalle_Servicios()

        DTGridDetalleServicios.Columns.Add("IdAcuerdoEnc", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("IdAcuerdoDet", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("servicio", GetType(String))
        DTGridDetalleServicios.Columns.Add("codigo_producto", GetType(String))
        DTGridDetalleServicios.Columns.Add("descripcion", GetType(String))
        DTGridDetalleServicios.Columns.Add("correlativo_detalleacuerdo", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("cantidad", GetType(Double))
        DTGridDetalleServicios.Columns.Add("IdOrdenCompraServicio", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("FechaServicio", GetType(Date))
        DTGridDetalleServicios.Columns.Add("IsNewR", GetType(Boolean))

    End Sub

    Private Sub Set_Columnas_Grid_Detalle_Servicios()

        Dim VisibleIndex As Integer = 1

        Try

            Dim ColIndexAux As Integer = 0

            dgridServiciosAsociados.DataSource = DTGridDetalleServicios

            gvDetalleServicios.OptionsView.ShowFooter = True
            gvDetalleServicios.OptionsView.ShowGroupPanel = False
            gvDetalleServicios.Columns.Clear()

#Region "Columna - Encabezado Acuerdo"

            Dim ColAcuerdoEnc As New GridColumn With {
                .FieldName = "IdAcuerdoEnc",
                .Caption = "Acuerdo Enc",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 200
            }

            ColAcuerdoEnc.Visible = False
            ColAcuerdoEnc.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColAcuerdoEnc)
            VisibleIndex += 1

#End Region

#Region "Columna - Detalle Acuerdo"

            Dim ColAcuerdoDet As New GridColumn With {
                .FieldName = "IdAcuerdoDet",
                .Caption = "Acuerdo Det",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 200
            }

            ColAcuerdoDet.Visible = False
            ColAcuerdoDet.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColAcuerdoDet)
            VisibleIndex += 1

#End Region

#Region "Columna - Servicio"


            ServicioGridLookUpEdit.View.Columns.Clear()

            ServicioGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdAcuerdoEnc", .Caption = "IdAcuerdoEnc", .Visible = False},
                New GridColumn With {.FieldName = "IdAcuerdoDet", .Caption = "IdAcuerdoDet", .Visible = False},
                New GridColumn With {.FieldName = "correlativo_detalleacuerdo", .Caption = "Correlativo", .Visible = True, .Width = 200},
                New GridColumn With {.FieldName = "codigo_producto", .Caption = "Codigo Producto", .Visible = True, .Width = 300},
                New GridColumn With {.FieldName = "servicio", .Caption = "Servicio", .Visible = True, .Width = 600},
                New GridColumn With {.FieldName = "descripcion", .Caption = "Descripcion", .Visible = True, .Width = 250}
             })

            ServicioGridLookUpEdit.ValueMember = "correlativo_detalleacuerdo"
            ServicioGridLookUpEdit.DisplayMember = "servicio"
            ServicioGridLookUpEdit.NullText = String.Empty

            Dim idPropietario As Integer = 0
            Dim fila As Object = lcmbPropietario.GetSelectedDataRow
            If fila IsNot Nothing Then
                idPropietario = fila.Item("IdPropietario")
            End If


            '#GT10062024: si existen varios acuerdos debe seleccionar uno para cargar el detalle
            If cmbAcuerdoComercial.EditValue > 0 Then

                Select Case Modo

                    Case ModoTrans.Nuevo
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, AP.Bodega.IdBodega)
                    Case ModoTrans.Editar
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, gBeOrdenCompra.IdBodega)

                End Select



            End If

            RemoveHandler ServicioGridLookUpEdit.Leave, AddressOf servicioGridLookUpEditDetalleServicio_Leave
            AddHandler ServicioGridLookUpEdit.Leave, AddressOf servicioGridLookUpEditDetalleServicio_Leave

            RemoveHandler ServicioGridLookUpEdit.KeyDown, AddressOf servicioGridLookUpEdit_KeyDown
            AddHandler ServicioGridLookUpEdit.KeyDown, AddressOf servicioGridLookUpEdit_KeyDown

            ServicioGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            Dim ColCodigoServicio As New GridColumn With {
                .FieldName = "correlativo_detalleacuerdo",
                .Caption = "servicio",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .ColumnEdit = ServicioGridLookUpEdit
            }

            ColCodigoServicio.Width = 600
            ServicioGridLookUpEdit.NullText = String.Empty
            ColCodigoServicio.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCodigoServicio)
            VisibleIndex += 1

#End Region

#Region "Columna - Codigo Producto"

            Dim ColCodigoProducto As New GridColumn With {
                .FieldName = "codigo_producto",
                .Caption = "codigo producto",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 200
            }

            ColCodigoProducto.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColCodigoProducto)
            VisibleIndex += 1

#End Region

#Region "Columna - Descripcion "

            Dim ColDescripcion As New GridColumn With {
                .FieldName = "descripcion",
                .Caption = "descripcion",
                .Visible = True,
                .VisibleIndex = VisibleIndex,
                .Width = 350
            }

            ColDescripcion.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColDescripcion)
            VisibleIndex += 1

#End Region

#Region "Columna - Codigo Acuerdo"

            'Dim ColCodigoAcuerdo As New GridColumn With {
            '    .FieldName = "codigo_acuerdo",
            '    .Caption = "codigo acuerdo",
            '    .Visible = True,
            '    .VisibleIndex = VisibleIndex,
            '    .Width = 200
            '}

            'ColCodigoAcuerdo.OptionsColumn.AllowEdit = False
            'gvDetalleServicios.Columns.Add(ColCodigoAcuerdo)
            'VisibleIndex += 1

#End Region

#Region "Columna - correlativo_detalleacuerdo"


            Dim Colcorre_detalleacuerdo As New GridColumn With {
                .FieldName = "correlativo_detalleacuerdo",
                .Caption = "correlativo",
                .Visible = False,
                .VisibleIndex = VisibleIndex,
                .Width = 100
            }

            Colcorre_detalleacuerdo.Visible = False
            Colcorre_detalleacuerdo.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(Colcorre_detalleacuerdo)

#End Region

#Region "Columna - Cantidad"

            Dim ColCantidad As New GridColumn With {
                .FieldName = "cantidad",
                .Caption = "cantidad",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = VisibleIndex
            }

            ColCantidad.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColCantidad)
            VisibleIndex += 1

#End Region

#Region "Columna - IdOrdenCompraServicio"

            Dim ColIdCompraServicio As New GridColumn With {
                .FieldName = "IdOrdenCompraServicio",
                .Caption = "IdOrdenCompraServicio",
                .Visible = True,
                .Width = 100,
                .ColumnEdit = txtCantidadGrid,
                .VisibleIndex = VisibleIndex
            }

            ColIdCompraServicio.Visible = False
            ColIdCompraServicio.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColIdCompraServicio)
            VisibleIndex += 1

#End Region

#Region "Columna - FechaServicio"

            Dim ColFechaVencimiento As New GridColumn With {
                .FieldName = "FechaServicio",
                .Caption = "FechaServicio",
                .Visible = True,
                .Width = 130,
                .ColumnEdit = txtFechaServicioGrid,
                .VisibleIndex = VisibleIndex
            }

            ColFechaVencimiento.OptionsColumn.AllowEdit = True
            gvDetalleServicios.Columns.Add(ColFechaVencimiento)
            VisibleIndex += 1

#End Region

#Region "Columna - IsNewR"

            Dim ColIsNew As New GridColumn With {
                .FieldName = "IsNewR",
                .Caption = "IsNewR",
                .Width = 100,
                .VisibleIndex = ColIndexAux + 1
            }

            ColIsNew.Visible = False
            ColIsNew.OptionsColumn.AllowEdit = False
            ColIsNew.DisplayFormat.FormatType = FormatType.Custom
            gvDetalleServicios.Columns.Add(ColIsNew)

            ColIndexAux += 1

#End Region

            gvDetalleServicios.OptionsNavigation.AutoFocusNewRow = True
            gvDetalleServicios.ClearSorting()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    '#GT29052024: se cargan los acuerdos si es OC nueva
    Private Sub Llena_Servicios_By_Propietario()

        Try

            Select Case Modo

                Case ModoTrans.Nuevo
                    ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(pIdPropietario, AP.Bodega.IdBodega)

                Case ModoTrans.Editar
                    ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(pIdPropietario, gBeOrdenCompra.IdBodega)
            End Select

            ServicioGridLookUpEdit.ValueMember = "correlativo_detalleacuerdo"
            ServicioGridLookUpEdit.DisplayMember = "servicio"
            ServicioGridLookUpEdit.NullText = String.Empty

            ServicioGridLookUpEdit.BestFitMode = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub servicioGridLookUpEditDetalleServicio_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim UnaOvejota As Object = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView))

            Dim View As GridView = CType(dgridServiciosAsociados.DefaultView, GridView)
            Dim EsFilaNueva As Boolean = IIf(IsDBNull(gvDetalleServicios.GetRowCellValue(gvDetalleServicios.FocusedRowHandle, "IsNewR")), 1, gvDetalleServicios.GetRowCellValue(gvDetalleServicios.FocusedRowHandle, "IsNewR"))


            If EsFilaNueva Then


                If Not UnaOvejota Is Nothing Then
                    Dim drArticulo As DataRow = UnaOvejota.Row
                    If drArticulo Is Nothing Then Return

                    drLineaRequisicion("IdAcuerdoEnc") = drArticulo("IdAcuerdoEnc")
                    drLineaRequisicion("IdAcuerdoDet") = drArticulo("IdAcuerdoDet")
                    drLineaRequisicion("codigo_producto") = drArticulo("codigo_producto")
                    drLineaRequisicion("servicio") = drArticulo("servicio")
                    drLineaRequisicion("descripcion") = drArticulo("descripcion")
                    drLineaRequisicion("correlativo_detalleacuerdo") = drArticulo("correlativo_detalleacuerdo")
                    drLineaRequisicion("cantidad") = 0
                    drLineaRequisicion("IdOrdenCompraServicio") = 0
                    'drLineaRequisicion("FechaServicio") = drArticulo("FechaServicio")
                    drLineaRequisicion("IsNewR") = True

                    gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("cantidad")
                    gvDetalleServicios.PostEditor()
                End If

            End If


        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub servicioGridLookUpEdit_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then

            Try

                Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
                If lista.EditValue Is Nothing Then Return
                Dim drLineaRequisicion As DataRow = gvDetalleServicios.GetFocusedDataRow()
                If drLineaRequisicion Is Nothing Then Return

                Dim UnaOvejota As Object = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView))

                If Not UnaOvejota Is Nothing Then
                    Dim drArticulo As DataRow = UnaOvejota.Row
                    If drArticulo Is Nothing Then Return

                    drLineaRequisicion("IdAcuerdoEnc") = drArticulo("IdAcuerdoEnc")
                    drLineaRequisicion("IdAcuerdoDet") = drArticulo("IdAcuerdoDet")
                    drLineaRequisicion("codigo_producto") = drArticulo("codigo_producto")
                    drLineaRequisicion("servicio") = drArticulo("servicio")
                    drLineaRequisicion("descripcion") = drArticulo("descripcion")
                    drLineaRequisicion("correlativo_detalleacuerdo") = drArticulo("correlativo_detalleacuerdo")
                    drLineaRequisicion("cantidad") = 0
                    drLineaRequisicion("IdOrdenCompraServicio") = 0
                    'drLineaRequisicion("FechaServicio") = drArticulo("FechaServicio")
                    drLineaRequisicion("IsNewR") = True

                    gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("cantidad")
                    gvDetalleServicios.PostEditor()

                End If

            Catch ex As Exception
                '#MECR03102025: Se agrego nueva bitacora de logs para OC
                Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
                'clsLnLog_error_wms.Agregar_Error(vMsgError)
                clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
            End Try

        End If

    End Sub


    Private Sub PropietarioGridLookUpEditDetalleIngreso_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then
                If lcmbPropietario.EditValue Is Nothing Then
                    Return
                Else
                    '#EJC20210307:Como le ponemos el propietario al 
                    gvDetalleDocIngreso.SetFocusedRowCellValue(gvDetalleDocIngreso.Columns("IdPropietarioBodega"), lcmbPropietario.EditValue)
                End If
            End If

            Dim drLineaRequisicion As DataRow = gvDetalleDocIngreso.GetFocusedDataRow()
            If drLineaRequisicion Is Nothing Then Return

            Dim vObjProp As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProp Is Nothing Then

                Dim drPropietario As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drPropietario Is Nothing Then Return
                Dim vIdPropietario As Integer = 0
                vIdPropietario = drPropietario("IdPropietarioBodega")

                Llena_ProductosLookUp_Grid(vIdPropietario)

                Dim vNoLinea As Integer = Val(IIf(IsDBNull(drLineaRequisicion("NoLinea")), 0, drLineaRequisicion("NoLinea")))
                If vNoLinea = 0 Then
                    drLineaRequisicion("NoLinea") = Get_No_Linea_Grid_Detalle()
                End If

                drLineaRequisicion("NombrePropietario") = lista.Text

                gvDetalleDocIngreso.FocusedColumn = gvDetalleDocIngreso.Columns("NoLinea")
                gvDetalleDocIngreso.PostEditor()


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PresentacionGridLookUpEditDetalleServicio_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            If e.KeyCode = Keys.Back Then
                gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion", Nothing)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Valida_No_Ticket()

        Dim no_ticket As Integer = 0

        Try

            '#EJC20210428:
            If gBeOrdenCompra Is Nothing Then Exit Sub

            'GT 26012021 se valida el control de poliza 
            If gBeOrdenCompra.Control_Poliza Then
                '#CKFK 20210214 Agregué la validación para cuando el ticket es vacio y no es numérico
                If gBeOrdenCompra.No_Ticket_TMS <> String.Empty Then
                    If IsNumeric(gBeOrdenCompra.No_Ticket_TMS) Then
                        no_ticket = Val(gBeOrdenCompra.No_Ticket_TMS)
                    End If
                End If

                gBeTmsTicket_pol = clsLnTms_ticket_pol.GetSingle(no_ticket)

                If gBeTmsTicket_pol IsNot Nothing Then
                    Mostrar_Duca_Tms = True
                ElseIf gBeOrdenCompra.ObjPoliza IsNot Nothing Then
                    Mostrar_Duca_Tms = True
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)
        End Try

    End Sub
    Private Sub frmOrdenCompra2_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        clsUiGridCopyHelper.AttachToForm(Me, "Copiar")
        Dim watch As Stopwatch = Stopwatch.StartNew()

        Try
            DTGridDetalleServicios.Clear()

            pCorreccionPoliza = False

            vNombreArchivoLayOutGridgvDetalleDocIngreso = "GridgvDetalleDocIngreso.xml"

            IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Documento de Ingreso...")

            If OpcionesMenu IsNot Nothing Then
                cmdActualizar.Enabled = OpcionesMenu.Modificar
                cmdEliminar.Enabled = OpcionesMenu.Eliminar
                cmdEliminarFila.Enabled = OpcionesMenu.Eliminar
                cmdAgregarProducto.Enabled = OpcionesMenu.Modificar
            End If

            Set_Datata_Table_Grid_Detalle_Servicios() : Set_Datata_Table_Grid_Detalle_Documento_Ingreso()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)

            Set_Columnas_Grid_Detalle_Servicios()

            dgridServiciosAsociados.DataSource = DTGridDetalleServicios
            gvDetalleServicios.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

            'Set_Columnas_Grid_Detalle_Documento_Ingreso()

            IMS.Listar_EstadosOC(cmbEstado)

            Listar_Operador_Por_Defecto()

            '#EJC20210317: Get_IdTipoDocumento_Consolidado_Defecto

            '#CKFK 20210418: Saqué esta opción fuera del else,ya que siempre se debe llenar el combo
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue)

            '#GT12122023: solo para fiscal, llenar los regimenes de ducas.
            If BeBodega.Es_Bodega_Fiscal Then
                IMS.Listar_Regimen_Fiscal(cmbRegimen)
            End If

            '#CKFK20240528 Puse ReadOnly la referencia basado en que tenga interface con SAP
            txtReferencia.ReadOnly = BeBodega.Interface_SAP = True

            cmdDuplicar.Visibility = IIf(clsLnMenu_rol.Permiso_Funcionalidad("2.1.1.2", AP.IdRol), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)

            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue, AP.IdEmpresa)

            IMS.Listar_TipoIngresoOC(cmbTipoIngreso, BeBodega.Es_Bodega_Fiscal)

            If TipoTrans = eTipoTrans.Consolidado Then
                Dim IdTipoDocConsolidadoDefecto As Integer = clsLnTrans_oc_ti.Get_IdTipoDocumento_Consolidado_Defecto()
                If IdTipoDocConsolidadoDefecto <> 0 Then
                    cmbTipoIngreso.EditValue = IdTipoDocConsolidadoDefecto
                End If
                txtIdProveedor.ReadOnly = True
                Me.Text = "Documento de Ingreso consolidado"
                RibbonPage1.Text = "Documento de Ingreso consolidado"
            Else
                '  IMS.Listar_TipoIngresoOC(cmbTipoIngreso)
                txtIdProveedor.ReadOnly = False
                RibbonPage1.Text = "Documento de Ingreso"
                Me.Text = "Documento de Ingreso"
            End If

            Me.Refresh()

            Application.DoEvents()

            '#EJC20210322: No ocultar al inicio porque luego no se puede activar el tab.
            'Poliza.Visible = False
            'Poliza.PageVisible = False

            If Not AP.Bodega.Control_Tarifa_Servicios Then
                tabDetalleServicios.Visible = False
                tabDetalleServicios.PageVisible = False
            End If

            Mostrar_Duca_Tms = False

            '#EJC20210222: Simplificar a método.
            Valida_No_Ticket()

            Select Case Modo

                Case ModoTrans.Nuevo

                    'Llenando_Datos_Maestros = True

                    lblC.Text = clsLnTrans_oc_enc.MaxID() + 1
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now
                    mnuGuardar.Enabled = True
                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    mnuAsignacion.Enabled = False
                    SubImprimir.Enabled = False
                    cmbBodega.Enabled = False
                    cmbEstado.ItemIndex = 0
                    cmdDuplicar.Enabled = False
                    dtmFechaOrdenCompra.DateTime = Today
                    dtFechaAbordaje.DateTime = Today
                    dtFechaPoliza.DateTime = Today
                    txtNoDocumento.Text = clsLnTrans_oc_enc.Genera_Correlativo_OC()
                    mnuTareaRecepcion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    grpUltRec.Visible = False

                    gBeOrdenCompra = New clsBeTrans_oc_enc With {.IsNew = True}

                    '#EJC20220301: Botón para registrar en NAV BYB.
                    mnuRegistrarEnNAV.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                    Set_Tipo_Documento()

                    xtraOrdenCompra.SelectedTabPageIndex = 0

                    chkActivo.Checked = True
                    chkActivo.Enabled = False

                    Application.DoEvents()

                    '#GT11062024: validar si aplicamos acuerdos y servicios al documento.
                    If AP.Bodega.Control_Tarifa_Servicios Then

                        If Llena_Servicios_By_Acuerdo_For_Combo() Then
                            Cargar_Servicios_Registrados()
                        End If

                    End If

                    Set_LayOut_Grid_Detalle_Documento_Ingreso()

                Case ModoTrans.Editar

                    Cargar_Datos()

                    'GT27012022_2212: solo para bodega fiscal se debe mostrar tab poliza, en general no es necesario
                    If AP.Bodega.Es_Bodega_Fiscal Then

                        chkControlPoliza.Checked = BeTipoDocumento.Control_Poliza
                        grpScanPoliza.Visible = BeTipoDocumento.Control_Poliza
                        Poliza.Visible = BeTipoDocumento.Control_Poliza
                        Poliza.PageVisible = BeTipoDocumento.Control_Poliza
                        tabPolizaCorregida.Visible = BeTipoDocumento.Control_Poliza
                        tabPolizaCorregida.PageVisible = BeTipoDocumento.Control_Poliza
                        cmdCorreccionPoliza.Enabled = True
                        txtScanPoliza.Enabled = False
                        lbScanPoliza.Enabled = False

                        Polizas_Corregidas(gBeOrdenCompra.IdOrdenCompraEnc)

                        If gridViewPolizas.RowCount > 1 Then
                            txtScanPoliza.BackColor = Color.IndianRed
                        End If

                    Else

                        chkControlPoliza.Checked = False
                        grpScanPoliza.Visible = False
                        Poliza.Visible = False
                        Poliza.PageVisible = False
                        tabPolizaCorregida.Visible = False
                        tabPolizaCorregida.PageVisible = False
                        cmdCorreccionPoliza.Enabled = False

                    End If

                    If Carga_Datos_PedidoERP() Then
                        xtraOrdenCompra.TabPages.Item(5).PageVisible = True
                    End If

                    mnuGuardar.Enabled = False
                    cmdActualizar.Enabled = True
                    cmdEliminar.Enabled = True
                    mnuAsignacion.Enabled = True
                    SubImprimir.Enabled = True
                    cmdDuplicar.Enabled = True

                    If Not gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.BACK_ORDER Then
                        cmdBackorder.Enabled = True
                    Else
                        cmdBackorder.Enabled = False
                    End If

                    'Si el estado de la Orden de Compra es 5 es porque esta anulada, 4 = Cerrada
                    If gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.ANULADA OrElse
                        gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.CERRADA Then
                        Bloquea_Objetos()
                        'gvDetalleDocIngreso.OptionsBehavior.ReadOnly = True
                        gvDetalleDocIngreso.OptionsBehavior.Editable = False
                        GrpImagen.Enabled = False
                        cmdActualizar.Enabled = False
                        cmdEliminar.Enabled = False
                        txtScanPoliza.ReadOnly = True
                        cmdDuplicar.Enabled = True

                        '#GT27052024: grid con detalle no debe permitir agregar o eliminar mas lineas.
                        cmdEliminarFila.Enabled = False
                        cmdAgregarProducto.Enabled = False
                        OCTrace_EstadoEdicionGrid("modo_editar_estado_cerrada_anulada")

                    Else
                        GrpEnc.Enabled = True
                        GrpDetalle.Enabled = True
                        GrpImagen.Enabled = True
                        cmdActualizar.Enabled = True
                        cmdEliminar.Enabled = True
                        cmdCorreccionPoliza.Enabled = True
                        txtScanPoliza.Enabled = False
                        cmdDuplicar.Enabled = True

                        '#GT29112024: si esta en backorder o abierta, permitir editar ciertas columnas controladas por validateRow
                        gvDetalleDocIngreso.OptionsBehavior.Editable = True

                        '#EJC20210613:Habilitar botón para crear recepción de forma inmediatia.
                        If Not clsLnTrans_re_enc.OC_Tiene_Recepciones_Activas(gBeOrdenCompra.IdOrdenCompraEnc) Then
                            mnuTareaRecepcion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            grpUltRec.Visible = True
                        End If
                        OCTrace_EstadoEdicionGrid("modo_editar_estado_abierta_o_backorder")

                    End If

                    If gBeOrdenCompra.Push_To_NAV Then

                        If Not gBeOrdenCompra.PutAway_Registrado Then

                            mnuRegistrarEnNAV.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                        End If

                    Else
                        mnuRegistrarEnNAV.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    End If

                    '#GT11062024: validar si aplicamos acuerdos y servicios al documento.
                    If AP.Bodega.Control_Tarifa_Servicios Then

                        If Llena_Servicios_By_Acuerdo_For_Combo() Then
                            Cargar_Servicios_Registrados()
                        End If

                    End If

            End Select

            '#EJC20220113_0302AM: Mostrar tab de servicios según el parámetro.
            xtraOrdenCompra.TabPages.Item(3).PageVisible = AP.Bodega.Control_Tarifa_Servicios
            tabDetalleServicios.Visible = AP.Bodega.Control_Tarifa_Servicios

            txtTotalLineas.Enabled = False
            txtTotalBulto.Enabled = False

            xtraOrdenCompra.SelectedTabPageIndex = 0

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            Focus()
            lcmbPropietario.Focus()
            IsLoading = False
            '#EJC20210428: Si se realizó una interrupción para cerrar mientras se estaba cargando, cerrar la forma al final.
            If IsClosing Then Close()

            watch.Stop()

            TiempoTranscurrido = watch.Elapsed.TotalSeconds

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            GrpEnc.Text = "E.S: " & Math.Round(TiempoTranscurrido, 2)

        End Try

    End Sub

    Private Sub Cargar_Servicios_Registrados()
        Try

            Dim listServicios As New List(Of clsBeTrans_oc_servicios)

            listServicios = clsLnTrans_oc_servicios.Get_All_By_IdOrdenCompraEnc(gBeOrdenCompra.IdOrdenCompraEnc)

            If listServicios IsNot Nothing Then

                DTGridDetalleServicios.Clear()

                For Each Servicio As clsBeTrans_oc_servicios In listServicios

                    Dim pAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det
                    pAcuerdoDet.IdAcuerdoDet = Servicio.IdAcuerdoDet
                    clsLnTrans_acuerdoscomerciales_det.GetSingle(pAcuerdoDet)

                    If pAcuerdoDet IsNot Nothing Then

                        DTGridDetalleServicios.Rows.Add(Servicio.IdAcuerdo,
                                                    Servicio.IdAcuerdoDet,
                                                    Servicio.Nombre_servicio,
                                                    Servicio.Codigo_producto,
                                                    pAcuerdoDet.Descripcion,
                                                    Servicio.Corre_detalleacuerdo,
                                                    Servicio.Cantidad,
                                                    Servicio.IdOrdenCompraServicio,
                                                    Servicio.Fecha_Servicio,
                                                    False)

                    End If



                Next

                dgridServiciosAsociados.DataSource = DTGridDetalleServicios

                If DTGridDetalleServicios.Rows.Count > 0 Then

                    gvDetalleServicios.OptionsBehavior.Editable = True

                    gvDetalleServicios.Columns("IdAcuerdoEnc").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("IdAcuerdoDet").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("correlativo_detalleacuerdo").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("codigo_producto").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("descripcion").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("IsNewR").OptionsColumn.AllowEdit = False
                    gvDetalleServicios.Columns("cantidad").OptionsColumn.AllowEdit = True

                    'ServicioGridLookUpEdit.ReadOnly = True

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private TiempoTranscurrido As Double = 0

    Private Sub Carga_Datos_RecepcionesPorOC()

        Try

            Dim rpt As New rptRecsOc

            Cargar_Encabezado_RecOc()

            Cargar_Detalle_RecOc(gBeOrdenCompra.IdOrdenCompraEnc)

            rpt.SetDataSource(DsReOc)
            frmReporte.rptView.ReportSource = rpt
            frmReporte.ShowDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Detalle_RecOc(ByVal IdOrdenCompraEnc As Integer)

        Dim lRow As DataRow
        Dim List As New List(Of clsBeTrans_re_det)

        Try

            List = clsLnTrans_re_det.Get_Recepciones_By_IdOrdenCompraEnc(IdOrdenCompraEnc, AP.IdBodega)

            If List IsNot Nothing AndAlso List.Count > 0 Then

                DsReOc.Detalle.Clear()

                For Each Objs In List

                    lRow = DsReOc.Detalle.NewRow
                    lRow.Item("IdProductoBodega") = Objs.IdProductoBodega
                    lRow.Item("IdPresentacion") = Objs.IdPresentacion
                    lRow.Item("IdUnidadMedida") = Objs.IdUnidadMedida
                    lRow.Item("IdProductoEstado") = Objs.IdProductoEstado
                    lRow.Item("IdOperadorBodega") = Objs.IdOperadorBodega
                    lRow.Item("IdMotivoDevolucion") = Objs.IdMotivoDevolucion
                    lRow.Item("No_Linea") = Objs.No_Linea
                    lRow.Item("CantidadRecibida") = Objs.cantidad_recibida
                    lRow.Item("Codigo") = Objs.Codigo_Producto
                    lRow.Item("Producto") = Objs.Nombre_producto
                    lRow.Item("Presentacion") = Objs.Nombre_presentacion
                    lRow.Item("UMBas") = Objs.Nombre_unidad_medida
                    lRow.Item("Estado") = Objs.Nombre_producto_estado
                    lRow.Item("Lote") = Objs.Lote
                    lRow.Item("Lp") = Objs.Lic_plate
                    lRow.Item("Fecha_Vence") = Objs.Fecha_vence.Date
                    lRow.Item("Fecha_Ingreso") = Objs.Fecha_ingreso
                    lRow.Item("Peso") = Objs.Peso
                    lRow.Item("IdOCEnc") = Objs.IdOrdenCompraEnc
                    lRow.Item("IdReceEnc") = Objs.IdRecepcionEnc
                    lRow.Item("Fecha_Recepcion") = Objs.Fecha_Rec
                    lRow.Item("hora_ini") = Objs.Hora_ini
                    lRow.Item("hora_fin") = Objs.Hora_Fin
                    lRow.Item("Estado_rec") = Objs.Estado_Rec
                    lRow.Item("fecha_tarea") = Objs.Fecha_tarea
                    lRow.Item("IdUbicacionRecepcion") = Objs.UbicacionCompleta
                    lRow.Item("IdMuelle") = 1

                    DsReOc.Detalle.AddDetalleRow(lRow)

                Next

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Encabezado_RecOc()

        Dim lRow As DataRow
        Dim Oc As New clsBeTrans_oc_enc

        Try

            Dim pIdOrdenCompraEnc As Integer = gBeOrdenCompra.IdOrdenCompraEnc

            Oc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc(pIdOrdenCompraEnc)

            DsReOc.Encabezado.Clear()

            lRow = DsReOc.Encabezado.NewRow
            lRow.Item("IdOCEnc") = Oc.IdOrdenCompraEnc
            lRow.Item("Fecha_creacion") = Oc.Fecha_Creacion
            lRow.Item("No_Documento") = Oc.No_Documento
            lRow.Item("Referencia") = Oc.Referencia
            lRow.Item("Observacion") = Oc.Observacion
            lRow.Item("Fecha_recepcion") = Oc.Fecha_Recepcion

            DsReOc.Encabezado.AddEncabezadoRow(lRow)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdRecepcionesAsociadas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRecepcionesAsociadas.ItemClick
        Carga_Datos_RecepcionesPorOC()
    End Sub

    Private Sub mnuCerrarPedidoCompra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCerrarPedidoCompra.ItemClick

        Try
            mnuCerrarPedidoCompra.Enabled = False

            If gBeOrdenCompra.Enviado_A_ERP Then
                If gBeOrdenCompra.IdEstadoOC = 6 Then 'BackOrder
                    Dim result As DialogResult = XtraMessageBox.Show("¿Éste documento ya se 
                    ha registrado en el ERP, Cambiar estado en WMS a cerrado?", "Confirmation", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        gBeOrdenCompra.IdEstadoOC = 4 'Cerrada
                        clsLnTrans_oc_enc.Actualizar_Estado_Cerrada(gBeOrdenCompra.IdOrdenCompraEnc)
                        XtraMessageBox.Show("Se ha cerrado el documento de ingreso",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                        Close()
                    End If
                End If
            Else
                XtraMessageBox.Show("Aun no se ha marcado el documento como enviado a ERP.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation)
            End If

            mnuCerrarPedidoCompra.Enabled = True
        Catch ex As Exception
            mnuCerrarPedidoCompra.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEstadoEnviadoAERP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEstadoEnviadoAERP.ItemClick

        Try
            mnuEstadoEnviadoAERP.Enabled = False

            If gBeOrdenCompra.Enviado_A_ERP Then

                Dim result As DialogResult = XtraMessageBox.Show("¿Éste documento ya se 
                    ha registrado en NAV acorde a los registros de WMS, Cambiar estado en WMS a no registrado?", "Confirmation", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    gBeOrdenCompra.Enviado_A_ERP = False 'No enviado a ERP
                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(gBeOrdenCompra.IdOrdenCompraEnc, gBeOrdenCompra.Enviado_A_ERP)
                    XtraMessageBox.Show("Se ha actualizado el estado del documento",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                    Close()
                End If

            Else

                Dim result As DialogResult = XtraMessageBox.Show("¿Éste documento no se ha registrado en el ERP acorde a los registros de WMS, Cambiar estado en WMS a registrado?", "Confirmation", MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then

                    gBeOrdenCompra.Enviado_A_ERP = True 'No enviado a ERP

                    clsLnTrans_oc_enc.Actualizar_Estado_Enviado_A_ERP(gBeOrdenCompra.IdOrdenCompraEnc, gBeOrdenCompra.Enviado_A_ERP)
                    XtraMessageBox.Show("Se ha actualizado el estado del documento",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                    If Not InvokeListarPedidosCompra Is Nothing Then
                        InvokeListarPedidosCompra.Invoke()
                    End If

                    Close()

                End If

            End If

            mnuEstadoEnviadoAERP.Enabled = True

        Catch ex As Exception
            mnuEstadoEnviadoAERP.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbTipoIngreso_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoIngreso.EditValueChanged
        Set_Tipo_Documento()
    End Sub

    Private Sub cmdBackorder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdBackorder.ItemClick

        Try

            If XtraMessageBox.Show(String.Format("Cambiar el estado del documento a backorder?", lblC.Text),
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.BACK_ORDER

                clsLnTrans_oc_enc.Actualizar_Estado_BackOrder(gBeOrdenCompra.IdOrdenCompraEnc)

                If Not InvokeListarPedidosCompra Is Nothing Then
                    InvokeListarPedidosCompra.Invoke()
                End If

                If clsLnTrans_oc_enc.Tiene_Pendientes(gBeOrdenCompra.IdOrdenCompraEnc) Then

                    mnuTareaRecepcion.Enabled = True

                    If NuevaRecepcion(0) Then
                        DialogResult = DialogResult.OK
                    End If

                Else
                    Close()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)

        End Try

    End Sub

    Private Sub cmdActualizarDetalle_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizarDetalle.ItemClick

        cmdActualizarDetalle.Enabled = False
        Cargar_Encabezado_Rec()
        Cargar_Detalle_Rec()
        'Cargar_Detalle_OC()
        'GT 02062021 Se recarga el detalle OC desde la bd, porque se esta actualizando para mostrar el conteo desde la HH.
        Cargar_Detalle_OC_HH()
        cmdActualizarDetalle.Enabled = True
    End Sub

    Private Sub GridView5_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView5.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub

        Try

            Dim Existe As Boolean = False

            Dim View As GridView = sender
            Dim CodigoPrd As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Código"))

            If e.Column.FieldName = "Código" Then

                If lOCDet.Exists(Function(x) x.Codigo_Producto = CodigoPrd) Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                Else
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PropietarioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PropietarioGridServiciosLookUpEdit As New RepositoryItemGridLookUpEdit
    Private PresentacionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private MotivoDevolcuionGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtNoLineaGrid As New RepositoryItemTextEdit

    Private txtValorFOBGrid As New RepositoryItemSpinEdit
    Private txtValorDAIGrid As New RepositoryItemSpinEdit
    Private txtTotalGrid As New RepositoryItemSpinEdit
    Private txtCostoGrid As New RepositoryItemSpinEdit
    Private txtvalorSeguroGrid As New RepositoryItemSpinEdit
    Private txtValorFleteGrid As New RepositoryItemSpinEdit
    Private txtValorIVAGrid As New RepositoryItemSpinEdit
    Private txtValorADUANAGrid As New RepositoryItemSpinEdit
    Private txtTallaGrid As New RepositoryItemTextEdit
    Private txtColorGrid As New RepositoryItemTextEdit
    Private txtSKUGrid As New RepositoryItemTextEdit

    '#GT12082025: combos talla/color para producto
    Private TallaGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private ColorGridLookUpEdit As New RepositoryItemGridLookUpEdit

    '#EJC20260522_OC_PRODUCT_LOOKUP_LAZY: evita cargar 39k productos al abrir la OC; se cargan al editar la columna.
    Private Function OCProductoLookup_Key(ByVal pIdPropietarioBodega As Integer, ByVal pIdBodega As Integer) As String

        Return pIdPropietarioBodega & "|" & pIdBodega

    End Function

    Private Function OCProductoLookup_DataSourceVacio() As DataTable

        Dim vDT As New DataTable("ProductoLookupVacio")
        vDT.Columns.Add("IdProductoBodega", GetType(Integer))
        vDT.Columns.Add("Codigo", GetType(String))
        vDT.Columns.Add("CodigoBarra", GetType(String))
        vDT.Columns.Add("Nombre", GetType(String))
        vDT.Columns.Add("UMBas", GetType(String))
        vDT.Columns.Add("IdUmBas", GetType(Integer))
        vDT.Columns.Add("Costo", GetType(Double))
        vDT.Columns.Add("Kit", GetType(Boolean))
        vDT.Columns.Add("IdProducto", GetType(Integer))
        vDT.Columns.Add("ControlPeso", GetType(Boolean))
        vDT.Columns.Add("Familia", GetType(String))
        vDT.Columns.Add("Clasificacion", GetType(String))
        vDT.Columns.Add("TipoProducto", GetType(String))
        Return vDT

    End Function

    Private Function OCProductoLookup_Obtener(ByVal pIdPropietarioBodega As Integer,
                                              ByVal pIdBodega As Integer,
                                              Optional ByVal pForzarRefresh As Boolean = False) As DataTable

        If pIdBodega = 0 Then
            pIdBodega = Val(cmbBodega.EditValue)
            If pIdBodega = 0 AndAlso gBeOrdenCompra IsNot Nothing Then pIdBodega = gBeOrdenCompra.IdBodega
            If pIdBodega = 0 Then pIdBodega = AP.IdBodega
        End If

        If pIdBodega = 0 Then Return OCProductoLookup_DataSourceVacio()

        Dim vKey As String = OCProductoLookup_Key(pIdPropietarioBodega, pIdBodega)

        If pForzarRefresh OrElse Not mOCProductoLookupCache.ContainsKey(vKey) Then
            Dim vReloj As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()
            Dim vDT As DataTable

            If pIdPropietarioBodega <> 0 Then
                vDT = clsLnProducto.Get_Lista_For_Grid_By_IdPropietario_And_IdBodega(pIdPropietarioBodega, pIdBodega)
            Else
                vDT = clsLnProducto.Get_Lista_For_Grid_By_IdBodega(pIdBodega)
            End If

            If vDT Is Nothing Then vDT = OCProductoLookup_DataSourceVacio()
            mOCProductoLookupCache(vKey) = vDT
            OCTrace_Marca("producto_lookup_load",
                          "idPropietarioBodega=" & pIdPropietarioBodega &
                          ";idBodega=" & pIdBodega &
                          ";rows=" & vDT.Rows.Count &
                          ";ms=" & vReloj.ElapsedMilliseconds)
        End If

        Return mOCProductoLookupCache(vKey)

    End Function

    '#EJC20260603_OC_LOOKUP_DISPLAY: en edición, mantener lookup liviano para que la columna Código resuelva DisplayMember sin cargar todo el catálogo.
    Private Sub OCProductoLookup_ResolverDisplayDesdeDetalle()

        Try

            If Modo <> ModoTrans.Editar Then Return
            If DTGridDetalleDocIngresos Is Nothing OrElse DTGridDetalleDocIngresos.Rows.Count = 0 Then Return

            Dim vDT As DataTable = OCProductoLookup_DataSourceVacio()
            Dim vKeys As New HashSet(Of Integer)

            For Each vRow As DataRow In DTGridDetalleDocIngresos.Rows

                If vRow Is Nothing Then Continue For
                If vRow.RowState = DataRowState.Deleted Then Continue For

                Dim vIdProductoBodega As Integer = 0
                If Not IsDBNull(vRow("IdProductoBodega")) Then vIdProductoBodega = CInt(vRow("IdProductoBodega"))
                If vIdProductoBodega <= 0 Then Continue For
                If vKeys.Contains(vIdProductoBodega) Then Continue For

                Dim vCodigo As String = String.Empty
                If Not IsDBNull(vRow("CodigoProducto")) Then vCodigo = CStr(vRow("CodigoProducto"))

                Dim vNombre As String = String.Empty
                If Not IsDBNull(vRow("NombreProducto")) Then vNombre = CStr(vRow("NombreProducto"))

                Dim vUMBas As String = String.Empty
                If Not IsDBNull(vRow("UMBas")) Then vUMBas = CStr(vRow("UMBas"))

                Dim vIdUmBas As Integer = 0
                If Not IsDBNull(vRow("IdUmBas")) Then vIdUmBas = CInt(vRow("IdUmBas"))

                Dim vCosto As Double = 0
                If vRow.Table.Columns.Contains("Costo") AndAlso Not IsDBNull(vRow("Costo")) Then
                    vCosto = CDbl(vRow("Costo"))
                End If

                Dim vKit As Boolean = False
                If vRow.Table.Columns.Contains("EsKit") AndAlso Not IsDBNull(vRow("EsKit")) Then
                    vKit = CBool(vRow("EsKit"))
                End If

                Dim vControlPeso As Boolean = False
                If vRow.Table.Columns.Contains("ControlPeso") AndAlso Not IsDBNull(vRow("ControlPeso")) Then
                    vControlPeso = CBool(vRow("ControlPeso"))
                End If

                vDT.Rows.Add(vIdProductoBodega,
                             vCodigo,
                             String.Empty,
                             vNombre,
                             vUMBas,
                             vIdUmBas,
                             vCosto,
                             vKit,
                             0,
                             vControlPeso,
                             String.Empty,
                             String.Empty,
                             String.Empty)
                vKeys.Add(vIdProductoBodega)

            Next

            If vDT.Rows.Count > 0 Then
                ProductoGridLookUpEdit.DataSource = vDT
                Debug.WriteLine("OC_LOOKUP_DISPLAY_DETALLE rows=" & vDT.Rows.Count &
                                ";idOrdenCompraEnc=" & gBeOrdenCompra.IdOrdenCompraEnc)
                OCTrace_Marca("producto_lookup_display_detalle",
                              "rows=" & vDT.Rows.Count &
                              ";idOrdenCompraEnc=" & gBeOrdenCompra.IdOrdenCompraEnc)
            Else
                Debug.WriteLine("OC_LOOKUP_DISPLAY_DETALLE rows=0;idOrdenCompraEnc=" & gBeOrdenCompra.IdOrdenCompraEnc)
                OCTrace_Marca("producto_lookup_display_detalle",
                              "rows=0;idOrdenCompraEnc=" & gBeOrdenCompra.IdOrdenCompraEnc)
            End If

        Catch ex As Exception
            Debug.WriteLine("OC_LOOKUP_DISPLAY_DETALLE error=" & ex.Message)
            OCTrace_Marca("producto_lookup_display_detalle_error", ex.Message)
        End Try

    End Sub

    '#EJC20260603_OC_EDIT_RULES: traza centralizada para entender por qué el grid queda editable/bloqueado.
    Private Sub OCTrace_EstadoEdicionGrid(ByVal pContexto As String)

        Try
            Dim vEstado As Integer = 0
            Dim vEnviadoErp As Boolean = False
            Dim vPushToNav As Boolean = False

            If gBeOrdenCompra IsNot Nothing Then
                vEstado = gBeOrdenCompra.IdEstadoOC
                vEnviadoErp = gBeOrdenCompra.Enviado_A_ERP
                vPushToNav = gBeOrdenCompra.Push_To_NAV
            End If

            Dim vMsg As String = "OC_EDIT_RULES contexto=" & pContexto &
                                 ";modo=" & Modo &
                                 ";estadoOC=" & vEstado &
                                 ";editable=" & gvDetalleDocIngreso.OptionsBehavior.Editable &
                                 ";readOnly=" & gvDetalleDocIngreso.OptionsBehavior.ReadOnly &
                                 ";cmdAgregarProducto=" & cmdAgregarProducto.Enabled &
                                 ";cmdEliminarFila=" & cmdEliminarFila.Enabled &
                                 ";cmdActualizar=" & cmdActualizar.Enabled &
                                 ";enviadoERP=" & vEnviadoErp &
                                 ";pushToNAV=" & vPushToNav

            Debug.WriteLine(vMsg)
            OCTrace_Marca("oc_edit_rules", vMsg)
        Catch ex As Exception
            Debug.WriteLine("OC_EDIT_RULES error=" & ex.Message)
        End Try

    End Sub

    Private Sub OCProductoLookup_Asignar(ByVal pEditor As GridLookUpEdit,
                                         ByVal pIdPropietarioBodega As Integer,
                                         Optional ByVal pForzarRefresh As Boolean = False)

        If pEditor Is Nothing Then Return
        pEditor.Properties.DataSource = OCProductoLookup_Obtener(pIdPropietarioBodega,
                                                                 Val(cmbBodega.EditValue),
                                                                 pForzarRefresh)
        If pEditor.Properties.DataSource IsNot Nothing Then pEditor.Properties.View.BestFitColumns()

    End Sub

    Private Sub OCProductoLookup_Asignar(ByVal pEditor As RepositoryItemGridLookUpEdit,
                                         ByVal pIdPropietarioBodega As Integer,
                                         Optional ByVal pForzarRefresh As Boolean = False)

        If pEditor Is Nothing Then Return
        pEditor.DataSource = OCProductoLookup_Obtener(pIdPropietarioBodega,
                                                      Val(cmbBodega.EditValue),
                                                      pForzarRefresh)
        If pEditor.DataSource IsNot Nothing Then pEditor.View.BestFitColumns()

    End Sub

    Private Sub ProductoGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try



            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvDetalleDocIngreso.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim View As GridView = CType(DgridDetalleOC.DefaultView, GridView)
            Dim ColTalla As GridColumn = View.Columns("Talla")
            Dim ColColor As GridColumn = View.Columns("Color")

            Dim vObjProducto As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProducto Is Nothing Then

                Dim drArticulo As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drArticulo Is Nothing Then Return

                'GT01032022_0834: traemos las propiedades del producto, al igual que en Agregar Producto (F2), para usar peso_referencia
                vBeProducto = clsLnProducto.Get_Single_By_CodigoProducto(drArticulo("Codigo"))

                drLineaGrid("CodigoProducto") = drArticulo("Codigo")
                drLineaGrid("NombreProducto") = drArticulo("Nombre")
                drLineaGrid("IdUmBas") = drArticulo("IdUmBas")
                If drArticulo.Table.Columns.Contains("UMBas") Then
                    drLineaGrid("UMBas") = drArticulo("UMBas")
                ElseIf drArticulo.Table.Columns.Contains("UmBas") Then
                    drLineaGrid("UMBas") = drArticulo("UmBas")
                Else
                    drLineaGrid("UMBas") = String.Empty
                End If
                drLineaGrid("Costo") = drArticulo("Costo")
                drLineaGrid("EsKit") = drArticulo("Kit")
                'drLineaGrid("IdProducto") = drArticulo("IdProducto")
                drLineaGrid("ControlPeso") = drArticulo("ControlPeso")
                'drLineaGrid("PesoReferenciaUMBas") = drArticulo("PesoReferenciaUMBas")

                'Dim vTalla As String = IIf(IsDBNull(drArticulo("Talla")), "", drArticulo("Talla"))
                'Dim vColor As String = IIf(IsDBNull(drArticulo("Color")), "", drArticulo("Color"))

                Dim vIdProductoBodega As Integer = drArticulo("IdProductoBodega")

                'Llena_PresentacionLookUp_Grid(vIdProductoBodega)

                Dim vIdProducto As Integer = drArticulo("IdProducto")
                Dim l As List(Of clsBeProducto_Presentacion) = clsLnProducto_presentacion.Get_All_By_IdProducto(vIdProducto).ToList

                If l.Count = 1 Then
                    drLineaGrid("IdPresentacion") = l(0).IdPresentacion
                Else
                    gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion", Nothing)
                End If

                Me.DgridDetalleOC.BeginInvoke(New MethodInvoker(Sub()
                                                                    gvDetalleDocIngreso.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                    gvDetalleDocIngreso.FocusedColumn = gvDetalleDocIngreso.Columns("Cantidad")
                                                                    gvDetalleDocIngreso.ShowEditor()
                                                                End Sub))

                'Dim View As ColumnView = DgridDetalleOC.FocusedView
                'Dim column As GridColumn = View.Columns.FirstOrDefault(Function(x) x.FieldName = "Cantidad")
                'If Not column Is Nothing Then
                '    View.FocusedColumn = column
                '    View.ShowEditor()
                '    Application.DoEvents()
                'End If

                'gvDetalleDocIngreso.FocusedColumn = gvDetalleDocIngreso.Columns("Cantidad")
                'gvDetalleDocIngreso.PostEditor()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub Llena_ProductosLookUp_Grid(ByVal pIdPropietarioBodega As Integer)

        Try

            'ProductoGridLookUpEdit.View.Columns.Clear()
            OCProductoLookup_Asignar(ProductoGridLookUpEdit, pIdPropietarioBodega)
            'ProductoGridLookUpEdit.PopupFormWidth = 1000
            'ProductoGridLookUpEdit.View.BestFitColumns()
            'ProductoGridLookUpEdit.TextEditStyle = TextEditStyles.Standard
            'ProductoGridLookUpEdit.AcceptEditorTextAsNewValue = DefaultBoolean.True

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub Llena_Motivos_Devolucion_LookUp_Grid()

        Try

            MotivoDevolcuionGridLookUpEdit.View.Columns.Clear()

            MotivoDevolcuionGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdMotivoDevolucion", .Caption = "Código", .Visible = False},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
            })

            MotivoDevolcuionGridLookUpEdit.DataSource = clsLnMotivo_devolucion.Get_All_By_IdPropietario_And_Bodega_DT(lcmbPropietario.Tag, cmbBodega.EditValue)
            MotivoDevolcuionGridLookUpEdit.ValueMember = "IdMotivoDevolucion"
            MotivoDevolcuionGridLookUpEdit.DisplayMember = "Nombre"
            MotivoDevolcuionGridLookUpEdit.NullText = String.Empty
            MotivoDevolcuionGridLookUpEdit.View.BestFitColumns()
            MotivoDevolcuionGridLookUpEdit.PopupFormWidth = 700

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub ProductoGridLookUpEdit_ProcessNewValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs)

        Try
            'Add new values to GridLookUpEdit control's DataSource.
            Dim gridLookup As GridLookUpEdit = TryCast(sender, GridLookUpEdit)

            If e.DisplayValue Is Nothing Then
                Return
            End If

            Dim newValue As String = e.DisplayValue.ToString()

            If newValue = String.Empty Then
                Return
            End If

            If Not clsLnProducto.Existe_Codigo(newValue) Then

                If MessageBox.Show(Me, "El código de material no existe,'" & newValue & "' ¿Crear el código?", "Confirmar", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                    Dim IdPropietarioBodega As Integer = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPropietarioBodega")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPropietarioBodega"))
                    Dim IdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(cmbBodega.EditValue, IdPropietarioBodega)

                    Dim frmProductomnt As New frmProducto
                    frmProductomnt.Modo = frmProducto.TipoTrans.Nuevo
                    frmProductomnt.IdPropietarioNuevo = IdPropietario
                    frmProductomnt.CodigoNuevoProducto = newValue
                    frmProductomnt.IdBodegaNuevoProducto = cmbBodega.EditValue

                    If frmProductomnt.ShowDialog() = DialogResult.OK Then
                        OCProductoLookup_Asignar(gridLookup, IdPropietarioBodega, True)
                        OCProductoLookup_Asignar(ProductoGridLookUpEdit, IdPropietarioBodega)
                        gridLookup.EditValue = frmProductomnt.IdProductoBodegaReturn
                        ProductoGridLookUpEdit_Leave(sender, Nothing)
                        e.Handled = True
                    End If

                End If

            End If


            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles lbScanPoliza.Click
        lbScanPoliza.Enabled = False
        Scan_Poliza()
        lbScanPoliza.Enabled = True
    End Sub


    Private Sub Scan_Poliza()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim encabezado_duca As New clsBeCEALSA_DUCA_ENC
            Dim barra_poliza As String = txtScanPoliza.Text
            Dim pPolizaExiste As New clsBeTrans_oc_pol
            Dim pNumero_OrdenOriginal As String = String.Empty

            pNumero_OrdenOriginal = txtNumeroOrden.Text.Trim

            If String.IsNullOrEmpty(barra_poliza) Then
                If chkControlPoliza.Checked Then
                    XtraMessageBox.Show("No hay póliza para leer", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else

                If Not BeBodega.Es_Bodega_Fiscal Then
                    cmbTipoIngreso.EditValue = 10
                Else

                End If

                '#MECR03102025: Se agrego nueva bitacora de logs para OC
                '#GT31082023: por seguridad, guardamos el scan de poliza
                Dim pPoliza = "AVISO: se guarda duca de ingreso " & txtScanPoliza.Text
                'clsLnLog_error_wms.Agregar_Error(pPoliza)
                Dim clsError As New clsBeLog_error_wms_oc
                clsError.MensajeError = pPoliza
                clsError.Fecha = Now
                clsError.IdBodega = AP.IdBodega
                clsError.IdEmpresa = AP.IdEmpresa
                clsError.IdUsuarioAgr = AP.UsuarioAp.IdUsuario
                clsError.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc

                'clsLnLog_error_wms.Agregar_Error(vMsgError)
                clsLnLog_error_wms_oc.Insertar(clsError, lConnection, lTransaction)


                Dim Nuevo_Formato_Duca = Formato_Nuevo_Duca(barra_poliza)

                If Nuevo_Formato_Duca Is Nothing Then

                    '#GT31082023: Es nueva cuando no se hace por importación.
                    'Si es nueva se pregunta, sino es nueva, ya se preguntó en el proceso de importación.
                    If gBeOrdenCompra.IsNew Then

                        encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)

                        '#GT31082023: Se valida si exite ingreso con misma póliza, según el numero_orden
                        pPolizaExiste = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden(encabezado_duca.Numero_Orden)
                        If Not pPolizaExiste Is Nothing Then

                            If XtraMessageBox.Show("¿La póliza ingresada " & pPolizaExiste.numero_orden & " ya existe, asociar el ingreso a póliza existente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                '#MECR03102025: Se agrego nueva bitacora de logs para OC
                                Dim pLog = "ADVERTENCIA_31082023_A: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza existente " & pPolizaExiste.numero_orden & " a mas de un ingreso."
                                Dim clsBeLogError As New clsBeLog_error_wms_oc
                                clsBeLogError.MensajeError = pLog
                                clsBeLogError.Fecha = Now
                                clsBeLogError.IdBodega = AP.IdBodega
                                clsBeLogError.IdEmpresa = AP.IdEmpresa
                                clsBeLogError.IdUsuarioAgr = AP.UsuarioAp.IdUsuario
                                clsBeLogError.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc

                                'clsLnLog_error_wms.Agregar_Error(pLog)
                                clsLnLog_error_wms_oc.Insertar(clsBeLogError, lConnection, lTransaction)
                            Else
                                txtScanPoliza.Text = String.Empty
                                encabezado_duca = Nothing
                                Exit Sub
                            End If

                        End If

                    End If

                    encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20)
                    '#GT: formatear fecha
                    Dim Fecha_string = barra_poliza.Substring(30, 8)
                    Dim comodin As String = "/"
                    Dim dd = Fecha_string.ToString.Substring(0, 2)
                    Dim mm = Fecha_string.ToString.Substring(2, 2)
                    Dim anio = Fecha_string.ToString.Substring(4, 4)
                    'encabezado_duca.Fecha_Aceptacion = dd & comodin & mm & comodin & anio
                    encabezado_duca.Fecha_Aceptacion = New Date(anio, mm, dd)

                    encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                    encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                    'GT 29042021 se convierte a mayuscula el regimen.
                    encabezado_duca.Regimen = barra_poliza.Substring(70, 5).ToUpper()
                    encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                    encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2).ToUpper()
                    encabezado_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                    encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                    encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                    encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                    encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                    encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                    encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                    encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                    encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                    encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                    encabezado_duca.Codigo_Poliza = barra_poliza.Substring(210, 9)

                Else

                    pPolizaExiste = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden(Nuevo_Formato_Duca.Numero_Orden)
                    If Not pPolizaExiste Is Nothing Then

                        If XtraMessageBox.Show("¿La póliza ingresada " & pPolizaExiste.numero_orden & " ya existe, asociar el ingreso a póliza existente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '#MECR03102025: Se agrego nueva bitacora de logs para OC
                            Dim pLog = "ADVERTENCIA_31082023_A: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza existente " & pPolizaExiste.numero_orden & " a mas de un ingreso."
                            Dim clsBeLogError As New clsBeLog_error_wms_oc
                            clsBeLogError.MensajeError = pLog
                            clsBeLogError.Fecha = Now
                            clsBeLogError.IdBodega = AP.IdBodega
                            clsBeLogError.IdEmpresa = AP.IdEmpresa
                            clsBeLogError.IdUsuarioAgr = AP.UsuarioAp.IdUsuario
                            clsBeLogError.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc

                            'clsLnLog_error_wms.Agregar_Error(pLog)
                            clsLnLog_error_wms_oc.Insertar(clsBeLogError, lConnection, lTransaction)
                        Else
                            txtScanPoliza.Text = String.Empty
                            encabezado_duca = Nothing
                            Nuevo_Formato_Duca = Nothing
                            Exit Sub
                        End If

                    End If

                    encabezado_duca = Nuevo_Formato_Duca

                End If

                'encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)

                '#GT31082023: Es nueva cuando no se hace por importación.
                'Si es nueva se pregunta, sino es nueva, ya se preguntó en el proceso de importación.
                'If gBeOrdenCompra.IsNew Then

                '    '#GT31082023: Se valida si exite ingreso con misma póliza, según el numero_orden
                '    pPolizaExiste = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden(encabezado_duca.Numero_Orden)
                '    If Not pPolizaExiste Is Nothing Then

                '        If XtraMessageBox.Show("¿La póliza ingresada " & pPolizaExiste.numero_orden & " ya existe, asociar el ingreso a póliza existente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            Dim pLog = "ADVERTENCIA_31082023_A: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza existente " & pPolizaExiste.numero_orden & " a mas de un ingreso."
                '            clsLnLog_error_wms.Agregar_Error(pLog)
                '        Else
                '            txtScanPoliza.Text = String.Empty
                '            encabezado_duca = Nothing
                '            Exit Sub
                '        End If

                '    End If

                'End If

                '/***********************************************************************************************/
                '#GT14082025: proceso actual de leer una poliza con 219 caracteres
                'encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)
                'encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20)
                'Dim Fecha_string = barra_poliza.Substring(30, 8)
                'encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                'encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                'encabezado_duca.Regimen = barra_poliza.Substring(70, 5).ToUpper()
                'encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                'encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2).ToUpper()
                'encabezado_duca.Modo_transporte = barra_poliza.Substring(80, 1)
                'encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                'encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                'encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                'encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                'encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                'encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                'encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                'encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                'encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                'encabezado_duca.Codigo_Poliza = barra_poliza.Substring(210, 9)

                '/***********************************************************************************************/
                '#GT12082025: numero de orden maneja 15 caracteres
                'encabezado_duca.Numero_DUCA = barra_poliza.Substring(15, 20)
                'Dim Fecha_string = barra_poliza.Substring(35, 8)
                'encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(43, 7)
                'encabezado_duca.NIT_Importador = barra_poliza.Substring(50, 25).Trim()
                '' GT 29042021 se convierte a mayuscula el regimen.
                'encabezado_duca.Regimen = barra_poliza.Substring(75, 5).ToUpper()
                'encabezado_duca.Clase = barra_poliza.Substring(80, 3).Trim()
                'encabezado_duca.Pais_procedencia = barra_poliza.Substring(83, 2).ToUpper()
                'encabezado_duca.Modo_transporte = barra_poliza.Substring(85, 1)
                'encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(86, 7))

                'encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(94, 15))
                'encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(109, 16))
                'encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(125, 15))
                'encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(140, 15))
                'encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(155, 15))
                'encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(170, 15))
                'encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(185, 15))
                'encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(200, 15))
                'encabezado_duca.Codigo_Poliza = barra_poliza.Substring(215, 9)

                'concatenación para fecha dd/mm/yyyy
                'Dim comodin As String = "/"
                'Dim dd As String = String.Empty
                'Dim mm As String = String.Empty
                'Dim anio As String = String.Empty
                'dd = Fecha_string.ToString.Substring(0, 2)
                'mm = Fecha_string.ToString.Substring(2, 2)
                'anio = Fecha_string.ToString.Substring(4, 4)
                'encabezado_duca.Fecha_Aceptacion = dd & comodin & mm & comodin & anio




                'GT 22012021 Set de los inputs en el formulario desde la clase encabezado_duca
                txtNumeroOrden.Text = encabezado_duca.Numero_Orden
                txtNumeroDUA.Text = encabezado_duca.Numero_DUCA.ToUpper()
                dtpFechaAceptacion.Text = encabezado_duca.Fecha_Aceptacion
                '4 Clave de aduana despacho/destino no definido
                txtClaveAduana.Text = encabezado_duca.Clave_aduana_despacho_destino.Trim
                '5 NIT de importador/exportador
                txtNitImpExp.Text = encabezado_duca.NIT_Importador
                Dim BeRegimen As New clsBeRegimen_fiscal()
                BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim)
                'GT18022022: si el regimen de la cadena no es legible puede afectar el tamaño de los demas valores de la póliza
                If Not BeRegimen Is Nothing Then
                    cmbRegimen.EditValue = BeRegimen.Codigo_regimen
                Else
                    txtScanPoliza.Text = String.Empty
                    Throw New Exception("El régimen: " & encabezado_duca.Regimen & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación, re-intente!.")
                End If
                '7 Clase
                txtClase.Text = encabezado_duca.Clase.Trim
                txtPaisProcedencia.Text = encabezado_duca.Pais_procedencia.Trim
                '9 Modo de transporte
                txtMod_transporte.Text = encabezado_duca.Modo_transporte.Trim
                txtTipoCambio.Value = encabezado_duca.Tipo_cambio
                txtValorAduana.Value = encabezado_duca.Total_valor_aduana
                txtTotalPesoBruto.Value = encabezado_duca.Total_bultos_Peso_Bruto
                txtTotalFOBUSD.Value = encabezado_duca.TotalFOBUSD
                txtValorFlete.Value = encabezado_duca.Total_Flete_USD
                txtValorSeguro.Value = encabezado_duca.Total_Seguro_USD
                txtTotalOtros.Value = encabezado_duca.TotalOtrosgastosUSD
                '17 Total liquidar
                txtTotal_liquidar.Text = encabezado_duca.Total_Liquidar
                '18 Totalgeneral
                txtTotal_general.Text = encabezado_duca.Total_General
                dtpFechaLlegada.Text = Now
                txtCodigoPoliza.Text = encabezado_duca.Codigo_Poliza
                '#GT27092023: CBM no viene en el código barras, corresponde buscarlo si fue registrado
                Dim tmpPoliza As New clsBeTrans_oc_pol
                tmpPoliza = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden(encabezado_duca.Numero_Orden)
                If Not tmpPoliza Is Nothing Then
                    cbCBM.Value = tmpPoliza.Cbm
                End If

                '#GT10102023: Guardar la nueva DUCA por corrección desde boton Scan Póliza
                If pCorreccionPoliza Then

                    '#GT31082023: Validar que la DUCA leída no exista.
                    If Not tmpPoliza Is Nothing Then
                        txtScanPoliza.Text = String.Empty
                        encabezado_duca = Nothing
                        Throw New Exception("ERROR_10102023: La póliza leída ya existe, no puede ser utilizada para corrección, reintente!.")

                    Else

                        '#GT10102023: TRAN para deshabilitar la duca previa, y guardar la nueva
                        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                        '#GT10102023: desactivamos la duca registrada anteriormente
                        Dim pPolizaOriginal As New clsBeTrans_oc_pol
                        pPolizaOriginal.numero_orden = pNumero_OrdenOriginal
                        pPolizaOriginal.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                        pPolizaOriginal.activo = False
                        clsLnTrans_oc_pol.Desactivar_Pol_By_Numero_Orden_and_OC(pPolizaOriginal, lConnection, lTransaction)

                        '#GT10102023: iniciamos la nueva DUCA
                        Dim pPolizaCorreccion As New clsBeTrans_oc_pol
                        pPolizaCorreccion.IsNew = True
                        pPolizaCorreccion.User_agr = AP.UsuarioAp.IdUsuario
                        pPolizaCorreccion.Fec_agr = Now
                        pPolizaCorreccion.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                        pPolizaCorreccion.Codigo_Barra = txtScanPoliza.Text
                        pPolizaCorreccion.Bl_No = BLNo.Text.Trim
                        pPolizaCorreccion.Pto_Descarga = txtPuertaDescarga.Text.Trim
                        pPolizaCorreccion.Remitente = txtRemitente.Text.Trim
                        pPolizaCorreccion.Fecha_abordaje = dtFechaAbordaje.EditValue
                        pPolizaCorreccion.Descripcion = "BY CORRECCION"
                        pPolizaCorreccion.Cantidad = CInt(txtCantidad.Value)
                        pPolizaCorreccion.Total_kgs = txtPesoKgs.Value
                        pPolizaCorreccion.Viaje_no = txtViaje.Text.Trim
                        pPolizaCorreccion.Buque_no = txtBuque.Text.Trim
                        pPolizaCorreccion.Destino = txtDestinatario.Text.Trim
                        pPolizaCorreccion.Dir_destino = txtDireccion.Text.Trim
                        pPolizaCorreccion.Po_number = txtPONumber.Text.Trim
                        pPolizaCorreccion.Piezas = CInt(txtPiezas.Value)
                        '#GT10102023: al inicio se valida el regimen, aca solo lo asignamos
                        pPolizaCorreccion.IdRegimen = BeRegimen.IdRegimen
                        pPolizaCorreccion.NoPoliza = txtNoPoliza.Text.Trim
                        pPolizaCorreccion.Pais_procede = txtPaisProcedencia.Text.Trim
                        pPolizaCorreccion.Total_valoraduana = txtValorAduana.Value
                        pPolizaCorreccion.Total_bultos_Peso_Bruto = txtTotalPesoBruto.Value
                        pPolizaCorreccion.Total_bultos_Peso_Neto = txtTotalPesoNeto.Value
                        pPolizaCorreccion.Total_flete = txtValorFlete.Value
                        pPolizaCorreccion.Total_usd = txtTotalFOBUSD.Value
                        pPolizaCorreccion.Dua = txtNumeroDUA.Text.Trim
                        pPolizaCorreccion.Fecha_poliza = dtFechaPoliza.EditValue
                        pPolizaCorreccion.Tipo_cambio = txtTipoCambio.Value
                        pPolizaCorreccion.Total_lineas = CInt(txtTotalLineas.Value)
                        pPolizaCorreccion.Total_bultos = CInt(txtTotalBulto.Value)
                        pPolizaCorreccion.Total_seguro = txtValorSeguro.Value
                        pPolizaCorreccion.User_mod = AP.UsuarioAp.IdUsuario
                        pPolizaCorreccion.Fec_mod = Now
                        'pPolizaCorreccion.Enviado_A_ERP = False
                        pPolizaCorreccion.codigo_poliza = txtCodigoPoliza.Text.Trim
                        pPolizaCorreccion.ticket = Val(txtTicket.Text.Trim)
                        pPolizaCorreccion.numero_orden = txtNumeroOrden.Text.Trim
                        pPolizaCorreccion.fecha_aceptacion = dtpFechaAceptacion.EditValue
                        pPolizaCorreccion.fecha_llegada = dtpFechaLlegada.EditValue
                        pPolizaCorreccion.total_otros = Val(txtTotalOtros.Value)
                        pPolizaCorreccion.clave_aduana = txtClaveAduana.Text.Trim
                        pPolizaCorreccion.nit_imp_exp = txtNitImpExp.Text.Trim
                        pPolizaCorreccion.clase = txtClase.Text.Trim
                        pPolizaCorreccion.mod_transporte = txtMod_transporte.Text.Trim
                        pPolizaCorreccion.total_liquidar = Val(txtTotal_liquidar.EditValue)
                        pPolizaCorreccion.total_general = Val(txtTotal_general.EditValue)
                        pPolizaCorreccion.Cbm = cbCBM.Value
                        pPolizaCorreccion.activo = True
                        pPolizaCorreccion.IdBodega = AP.Bodega.IdBodega

                        clsLnTrans_oc_enc.Guarda_Trans_oc_pol(gBeOrdenCompra.IdOrdenCompraEnc, pPolizaCorreccion,
                                                                                               lConnection,
                                                                                               lTransaction)

                        lTransaction.Commit()

                        XtraMessageBox.Show("Se actualizó el ingreso: " & gBeOrdenCompra.IdOrdenCompraEnc & " con el nuevo numero de orden: " & pPolizaCorreccion.numero_orden, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Polizas_Corregidas(gBeOrdenCompra.IdOrdenCompraEnc)

                    End If

                End If

                '#GT21062024: a futuro, se debe requerir permiso para autorizar el modificar campos de la póliza.
                'Bloquear_Inputs_Poliza()

            End If

            Set_Tipo_Documento()

            If pCorreccionPoliza Then
                xtraOrdenCompra.SelectedTabPageIndex = 9
            Else
                xtraOrdenCompra.SelectedTabPageIndex = 1
            End If

        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub


    Public Function Formato_Nuevo_Duca(barra_poliza As String) As clsBeCEALSA_DUCA_ENC

        Formato_Nuevo_Duca = Nothing
        Dim EsFecha As Boolean = False
        Dim EsRegimen As Boolean = False

        Try
            Dim encabezado_duca = New clsBeCEALSA_DUCA_ENC()
            encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 15)
            encabezado_duca.Numero_DUCA = barra_poliza.Substring(15, 20)
            Dim Fecha_string = barra_poliza.Substring(35, 8)
            encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(43, 7)
            encabezado_duca.NIT_Importador = barra_poliza.Substring(50, 25).Trim()
            encabezado_duca.Regimen = barra_poliza.Substring(75, 5).ToUpper()

            If EsFechaValida(Fecha_string) Then
                Dim comodin As String = "/"
                Dim dd = Fecha_string.ToString.Substring(0, 2)
                Dim mm = Fecha_string.ToString.Substring(2, 2)
                Dim anio = Fecha_string.ToString.Substring(4, 4)
                encabezado_duca.Fecha_Aceptacion = dd & comodin & mm & comodin & anio
                EsFecha = True
            End If

            Dim BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(encabezado_duca.Regimen.Trim)
            If BeRegimen IsNot Nothing Then
                cmbRegimen.EditValue = BeRegimen.Codigo_regimen
                EsRegimen = True
            End If

            If EsFecha AndAlso EsRegimen Then

                encabezado_duca.Clase = barra_poliza.Substring(80, 3).Trim()
                encabezado_duca.Pais_procedencia = barra_poliza.Substring(83, 2).ToUpper()
                encabezado_duca.Modo_transporte = barra_poliza.Substring(85, 1)
                encabezado_duca.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(86, 7))
                encabezado_duca.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(94, 15))
                encabezado_duca.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(109, 16))
                encabezado_duca.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(125, 15))
                encabezado_duca.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(140, 15))
                encabezado_duca.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(155, 15))
                encabezado_duca.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(170, 15))
                encabezado_duca.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(185, 15))
                encabezado_duca.Total_General = Convert.ToDouble(barra_poliza.Substring(200, 15))
                encabezado_duca.Codigo_Poliza = barra_poliza.Substring(215, 9)
                Formato_Nuevo_Duca = encabezado_duca

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try
    End Function

    Private Function EsFechaValida(fechaStr As String) As Boolean

        Dim comodin As String = "/"
        Dim dd = fechaStr.ToString.Substring(0, 2)
        Dim mm = fechaStr.ToString.Substring(2, 2)
        Dim anio = fechaStr.ToString.Substring(4, 4)
        Dim Fecha_Aceptacion = dd & comodin & mm & comodin & anio

        Dim fecha As Date
        Return Date.TryParseExact(Fecha_Aceptacion, "dd/MM/yyyy", Nothing, Globalization.DateTimeStyles.None, fecha)
    End Function


    Private Sub Bloquear_Inputs_Poliza()
        Try
            txtCodigoPoliza.Enabled = False
            txtNumeroOrden.Enabled = False
            txtNoPoliza.Enabled = False
            txtTicket.Enabled = False
            txtNumeroDUA.Enabled = False
            dtFechaPoliza.Enabled = False
            dtpFechaAceptacion.Enabled = False
            dtpFechaLlegada.Enabled = False
            txtValorSeguro.Enabled = False
            txtValorAduana.Enabled = False
            txtTotalOtros.Enabled = False
            txtValorFlete.Enabled = False
            'txtTotalPesoBruto.Enabled = False
            'txtTotalPesoNeto.Enabled = False
            txtTipoCambio.Enabled = False
            txtClaveAduana.Enabled = False
            txtPaisProcedencia.Enabled = False
            cmbRegimen.Enabled = False
            txtTotalFOBUSD.Enabled = False
            txtNitImpExp.Enabled = False
            txtClase.Enabled = False
            txtMod_transporte.Enabled = False
            txtTotal_liquidar.Enabled = False
            txtTotal_general.Enabled = False
            'cbCBM.Enabled = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Polizas_Corregidas(idOrdenCompraEnc As Integer)

        Dim DT As New DataTable

        Try

            DT.Clear()

            DT = clsLnTrans_oc_pol.Get_All_By_IdOrdenCompraEnc(idOrdenCompraEnc)

            If DT IsNot Nothing Then

                If DT.Rows.Count > 0 Then
                    DgridPolizas.DataSource = DT
                    gridViewPolizas.BestFitColumns()

                    tabPolizaCorregida.Text = "Pólizas Corregidas (" & DT.Rows.Count & ")"
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try
    End Sub

    Private Sub cmdImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImportarExcel.ItemClick

        Try

            If lcmbPropietario.EditValue Is Nothing Then
                XtraMessageBox.Show("Debe seleccionar el propietario antes de iniciar el proceso de importación ", "Documento de Ingreso")
                Return
            End If

            If txtIdProveedor.EditValue = String.Empty Then
                XtraMessageBox.Show("Debe seleccionar el proveedor antes de iniciar el proceso de importación ", "Documento de Ingreso")
                Return
            End If
            cmdImportarExcel.Enabled = False
            If XtraMessageBox.Show(String.Format("¿Importar Documento de Ingreso {0} desde Excel?", lblC.Text),
Text,
MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                If Modo = ModoTrans.Nuevo Then

                    If Not Guardar_Enc_OC() Then Return

                    'Si el estado de la Orden de Compra es 5 es porque esta anulada, 4 = Cerrada
                    If gBeOrdenCompra.IdEstadoOC = 5 OrElse gBeOrdenCompra.IdEstadoOC = 4 Then
                        Bloquea_Objetos()
                        gvDetalleDocIngreso.OptionsBehavior.ReadOnly = True
                        GrpImagen.Enabled = False
                        cmdActualizar.Enabled = False
                        cmdEliminar.Enabled = False
                        cmdDuplicar.Enabled = True
                        OCTrace_EstadoEdicionGrid("import_excel_post_guardado_estado_cerrada_anulada")
                    Else
                        mnuGuardar.Enabled = False
                        GrpEnc.Enabled = True
                        GrpDetalle.Enabled = True
                        GrpImagen.Enabled = True
                        cmdActualizar.Enabled = True
                        cmdEliminar.Enabled = True
                        cmdDuplicar.Enabled = True
                        OCTrace_EstadoEdicionGrid("import_excel_post_guardado_estado_abierta")
                    End If

                Else
                    Actualizar()
                End If

                Dim Carga As New frmCargaExcel_DI() With {.pNombreMantenimiento = "Detalle Documento Ingreso",
                .pTipoMantenimiento = "Documento Ingreso",
                .IdPropietarioBodega = lcmbPropietario.EditValue,
                .Listar = Nothing,
                .IdOrdenCompraEnc = lblC.Text,
                .IdBodega = cmbBodega.EditValue,
                .gRefBeOrdenCompra = gBeOrdenCompra.Clone(),
                .pTipoDocumento = BeTipoDocumento.IdTipoIngresoOC}

                If cmbMotivoDevolucion.Visible Then
                    Carga.IdMotivoDevolucion = cmbMotivoDevolucion.EditValue
                End If

                If Carga.ShowDialog() = DialogResult.OK Then

                    lOCDet.Clear()
                    lOCDetLn.Clear()

                    gBeOrdenCompra = Carga.gRefBeOrdenCompra
                    gBeOrdenCompra.DetalleOC = clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc(gBeOrdenCompra.IdOrdenCompraEnc)
                    lOCDet = gBeOrdenCompra.DetalleOC
                    lOCDetLn = gBeOrdenCompra.DetalleOC

                    txtNoTicketTMS.Text = Carga.gScanTicket
                    Scan_No_Ticket_TMS()

                    txtScanPoliza.Text = Carga.gScanCodigoBarraPoliza

                    Scan_Poliza()

                    Cargar_Detalle_OC()

                    cmbOperadorDefecto.EditValue = gBeOrdenCompra.IdOperadorBodegaDefecto

                    DgridDetalleOC.Refresh()

                    cmbTipoIngreso.Enabled = False

                    'tabPolizaCorregida
                    If chkControlPoliza.Checked Then
                        xtraOrdenCompra.SelectedTabPageIndex = 1
                    Else
                        xtraOrdenCompra.SelectedTabPageIndex = 0
                    End If
                Else
                    Debug.WriteLine("IsNew is: " & gBeOrdenCompra.IsNew)
                End If

                Carga.Dispose()

            End If

            cmdImportarExcel.Enabled = True



        Catch ex As Exception
            cmdImportarExcel.Enabled = True
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lcmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles lcmbPropietario.EditValueChanged

        Try

            If lcmbPropietario.EditValue <> 0 Then

                '#GT28052024: obtener el propietario para usarlo globalmente.
                Dim fila As Object = lcmbPropietario.GetSelectedDataRow
                If fila IsNot Nothing Then
                    pIdPropietario = fila.Item("IdPropietario")
                End If

                If cmbBodega.Text <> String.Empty Then

                    lcmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

                    If lcmbPropietario.Tag <> 0 Then

                        '#EJC20210404: llenar productos de propietario.
                        If TipoTrans = eTipoTrans.Simple Then

                            Llena_ProductosLookUp_Grid(lcmbPropietario.EditValue)

                            Dim BeBodega As New clsBeBodega
                            BeBodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue)
                            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue, AP.IdEmpresa)

                            If BeBodega.habilitar_ingreso_consolidado Then

                                If Modo = ModoTrans.Nuevo Then

                                    '#EJC20210317: Colocar el proveedor por defecto asociado al código de propietario.
                                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                                    'BeProveedorBodega.IdProveedor = lcmbPropietario.Tag
                                    '#CKFK20230512 Al parecer el error está en la línea de arriba
                                    'porque se asigna un propietario en lugar de un proveedor, ahora lo acabo de corregir
                                    '#GT18052023: antes de asignar el tag, validar que no este vacio
                                    If lcmbPropietario.Tag <> 0 Then
                                        BeProveedorBodega.IdProveedor = lcmbPropietario.Tag
                                        BeProveedorBodega.IdBodega = cmbBodega.EditValue
                                        clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega)
                                    End If

                                    If BeProveedorBodega IsNot Nothing Then
                                        txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion
                                        txtIdProveedor.Text = BeProveedorBodega.IdProveedor
                                        txtNombreProveedor.Text = BeProveedorBodega.Proveedor.Nombre
                                    Else

                                        'txtIdProveedor.Text = ""
                                        'txtIdProveedor.Tag = ""
                                        'txtNombreProveedor.Text = ""

                                        'GT21082022: Se agrega validación si es ingreso_consolidado (creo que todos lo son para cealsa según el parametro de la bodega)
                                        Dim BePropietario As New clsBePropietarios
                                        BePropietario = clsLnPropietarios.GetSingle(lcmbPropietario.Tag)

                                        If Not BePropietario Is Nothing Then

                                            Dim pBeProveedorBodega As New clsBeProveedor_bodega
                                            pBeProveedorBodega = clsLnProveedor_bodega.Get_Single_By_NIT_AND_IdBodega(BePropietario.NIT, cmbBodega.EditValue)

                                            If pBeProveedorBodega IsNot Nothing Then
                                                txtIdProveedor.Tag = pBeProveedorBodega.IdAsignacion
                                                txtIdProveedor.Text = pBeProveedorBodega.IdProveedor
                                                txtNombreProveedor.Text = pBeProveedorBodega.Proveedor.Nombre
                                            Else
                                                '#EJC20220818: No se encontró por NIT
                                                txtIdProveedor.Tag = String.Empty
                                                txtIdProveedor.Text = String.Empty
                                                txtNombreProveedor.Text = String.Empty
                                            End If

                                        Else
                                            '#EJC20220818: No se obtuvo el propietario.
                                            txtIdProveedor.Tag = String.Empty
                                            txtIdProveedor.Text = String.Empty
                                            txtNombreProveedor.Text = String.Empty
                                        End If

                                    End If


                                Else


                                End If

                            Else

                                Dim listaProveedorBodega As New List(Of clsBeProveedor_bodega)
                                listaProveedorBodega = clsLnProveedor_bodega.Get_All_By_IdBodega_And_IdPropietario(cmbBodega.EditValue,
                                                                                                                   lcmbPropietario.Tag,
                                                                                                                   BeTipoDocumento.Requerir_Proveedor_Es_Bodega_WMS)

                                If listaProveedorBodega.Count = 1 Then
                                    txtIdProveedor.Tag = listaProveedorBodega.Item(0).IdAsignacion
                                    txtIdProveedor.Text = listaProveedorBodega.Item(0).IdProveedor
                                    txtNombreProveedor.Text = listaProveedorBodega.Item(0).Proveedor.Nombre
                                Else

                                    Dim BePropietario As New clsBePropietarios
                                    BePropietario = clsLnPropietarios.GetSingle(lcmbPropietario.Tag)

                                    If Not BePropietario Is Nothing Then

                                        Dim BeProveedorBodega As New clsBeProveedor_bodega
                                        BeProveedorBodega = clsLnProveedor_bodega.Get_Single_By_NIT_AND_IdBodega(BePropietario.NIT, cmbBodega.EditValue)

                                        If BeProveedorBodega IsNot Nothing Then
                                            txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion
                                            txtIdProveedor.Text = BeProveedorBodega.IdProveedor
                                            txtNombreProveedor.Text = BeProveedorBodega.Proveedor.Nombre
                                        Else
                                            '#EJC20220818: No se encontró por NIT
                                            txtIdProveedor.Tag = String.Empty
                                            txtIdProveedor.Text = String.Empty
                                            txtNombreProveedor.Text = String.Empty
                                        End If

                                    Else
                                        '#EJC20220818: No se obtuvo el propietario.
                                        txtIdProveedor.Tag = String.Empty
                                        txtIdProveedor.Text = String.Empty
                                        txtNombreProveedor.Text = String.Empty
                                    End If

                                End If

                            End If

                        Else
                            '#EJC20210317: Colocar el proveedor por defecto asociado al código de propietario.
                            Dim BeProveedorBodega As New clsBeProveedor_bodega()

                            '#GT18052023: antes de asignar el tag, validar que no este vacio
                            If lcmbPropietario.Tag <> String.Empty Then
                                BeProveedorBodega.IdProveedor = lcmbPropietario.Tag
                                BeProveedorBodega.IdBodega = cmbBodega.EditValue
                                clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega)
                            End If


                            If BeProveedorBodega IsNot Nothing Then
                                txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion
                                txtIdProveedor.Text = BeProveedorBodega.IdProveedor
                                txtNombreProveedor.Text = BeProveedorBodega.Proveedor.Nombre
                            Else
                                txtIdProveedor.Text = String.Empty
                                txtIdProveedor.Tag = String.Empty
                                txtNombreProveedor.Text = String.Empty
                            End If

                        End If

                    End If

                    If AP.Bodega.Control_Tarifa_Servicios Then

                        Select Case Modo

                            Case ModoTrans.Nuevo
                                Llena_Servicios_By_Acuerdo_For_Combo()
                        End Select

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub txtNoTicketTMS_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoTicketTMS.KeyDown

        If e.KeyCode = Keys.Enter Then
            Scan_No_Ticket_TMS()
        End If

    End Sub

    '#EJC20210222
    Private Sub Scan_No_Ticket_TMS()

        If Not txtNoTicketTMS.EditValue Is Nothing Then
            If txtNoTicketTMS.Text.Trim() <> String.Empty Then
                If IsNumeric(txtNoTicketTMS.Text.Trim()) Then
                    If Procesar_No_Ticket_TMS(txtNoTicketTMS.EditValue, IIf(Modo = ModoTrans.Nuevo, True, False)) Then
                        If chkControlPoliza.Checked Then
                            If txtScanPoliza.Text.Trim = String.Empty Then
                                txtScanPoliza.Focus()
                            Else
                                xtraOrdenCompra.SelectedTabPageIndex = 2
                                DgridDetalleOC.Focus()
                            End If
                        Else
                            xtraOrdenCompra.SelectedTabPageIndex = 1
                            DgridDetalleOC.Focus()
                        End If
                    End If
                Else
                    XtraMessageBox.Show("Solo se puede ingresar valores de tipo número", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End If

    End Sub

    Private Function Procesar_No_Ticket_TMS(ByVal pNoTicket As String, ByVal EsNuevaAsignacion As Boolean) As Boolean

        Procesar_No_Ticket_TMS = False

        Try

            Dim BeTicketIngreso As New clsBeTms_ticket
            BeTicketIngreso = clsLnTms_ticket.Get_Ticket_By_Id(pNoTicket)

            If Not BeTicketIngreso Is Nothing Then

                If EsNuevaAsignacion AndAlso Not BeTicketIngreso.Estado = "Abierto" Then
                    XtraMessageBox.Show("El número de ticket: " & txtNoTicketTMS.Text.Trim & " ya fue asignado en otra transacción, no se puede asociar nuevamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNoTicketTMS.Text = String.Empty
                    Procesar_No_Ticket_TMS = False
                Else

                    txtFechaIngresoTMS.Text = BeTicketIngreso.Fecha_Ingreso

                    Dim BePiloto As New clsBeEmpresa_transporte_pilotos
                    BePiloto = clsLnEmpresa_transporte_pilotos.Get_By_IdPiloto(BeTicketIngreso.IdPiloto)

                    If Not BePiloto Is Nothing Then
                        txtNombresPilotoTMS.Text = BePiloto.Nombres & " " & BePiloto.Apellidos
                    End If

                    Dim BeVehiculo As New clsBeEmpresa_transporte_vehiculos
                    BeVehiculo = clsLnEmpresa_transporte_vehiculos.Get_Single_By_IdVehiculo(BeTicketIngreso.IdVehiculo)

                    If Not BeVehiculo Is Nothing Then
                        txtNoPlacaTMS.Text = BeVehiculo.Placa
                    End If

                    Dim vTiempoEspera As Double = DateDiff(DateInterval.Minute, BeTicketIngreso.Fecha_Ingreso, Now)

                    Dim hora As String = String.Format("{0:N0}:{1:N0} hrs", vTiempoEspera / 60, vTiempoEspera Mod 60)
                    txtTiempoEsperaTMS.Text = hora

                    If Not BeTicketIngreso.ObjPoliza Is Nothing Then
                        If txtScanPoliza.Text.Trim = String.Empty Then
                            txtScanPoliza.Text = BeTicketIngreso.ObjPoliza.Codigo_Barra
                            Scan_Poliza()
                        End If
                    End If

                    Procesar_No_Ticket_TMS = True

                End If

            Else
                XtraMessageBox.Show("No se encontró información relacionada al ticket ingresado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'grpTMS.Visible = False
                txtNoTicketTMS.Text = String.Empty
                txtFechaIngresoTMS.Text = String.Empty
                txtNombresPilotoTMS.Text = String.Empty
                txtNoPlacaTMS.Text = String.Empty
                txtTiempoEsperaTMS.Text = String.Empty
                Procesar_No_Ticket_TMS = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Function

    Private DTOperadores As DataTable

    Private Sub Listar_Operador_Por_Defecto()

        Try

            DTOperadores = clsLnOperador_bodega.Get_All_By_IdBodega_DT_For_Combo(cmbBodega.EditValue)
            cmbOperadorDefecto.Properties.DataSource = DTOperadores
            cmbOperadorDefecto.Properties.DisplayMember = "Nombres"
            cmbOperadorDefecto.Properties.ValueMember = "Id"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub txtScanPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanPoliza.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtScanPoliza.Text.Trim() <> String.Empty Then
                Scan_Poliza()
            End If
        End If
    End Sub

    Private Sub txtNoPoliza_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If chkControlPoliza.Checked Then
                If txtScanPoliza.Text.Trim <> String.Empty Then
                    xtraOrdenCompra.SelectedTabPageIndex = 2
                    DgridDetalleOC.Focus()
                End If
            End If
        End If

    End Sub

    Private Sub gvDetalleDocIngreso_ValidatingEditor(sender As Object, e As BaseContainerValidateEditorEventArgs) Handles gvDetalleDocIngreso.ValidatingEditor

        Try

            Dim vView As GridView = sender
            Dim vValor = e.Value

            '#EJC20210307: Esto sirve para que si se definió un propietario a nivel de cabecera
            ''ese propietario se asigne automáticamente al propietario del grid (en caso de que no se defina)
            If (vView.FocusedColumn.FieldName = "IdPropietarioBodega") Then

                If vValor Is Nothing Then

                    If Not lcmbPropietario.EditValue Is Nothing Then
                        e.Value = lcmbPropietario.EditValue
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Grid_Tiene_Error As Boolean = False
    Private Sub gvDetalleDocIngreso_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvDetalleDocIngreso.ValidateRow

        Try

            '#EJC20260522_OC_UI: la carga masiva no debe ejecutar validaciones de edicion por fila.
            If mOCCargandoDetalle Then
                e.Valid = True
                Exit Sub
            End If

            Dim View As GridView = CType(sender, GridView)
            Dim ColCantidad As GridColumn = View.Columns("Cantidad")
            Dim ColProducto As GridColumn = View.Columns("IdProductoBodega")
            Dim ColLinea As GridColumn = View.Columns("NoLinea")
            Dim ColCodigoProducto As GridColumn = View.Columns("CodigoProducto")
            Dim ColPropietario As GridColumn = View.Columns("IdPropietarioBodega")
            Dim ColValorAduana As GridColumn = View.Columns("ValorAduana")
            Dim ColValorFOB As GridColumn = View.Columns("ValorFOB")
            Dim ColValorIVA As GridColumn = View.Columns("ValorIVA")
            Dim ColValorDAI As GridColumn = View.Columns("ValorDAI")
            Dim ColValorSeguro As GridColumn = View.Columns("ValorSeguro")
            Dim ColValorFlete As GridColumn = View.Columns("ValorFlete")
            Dim ColTotal As GridColumn = View.Columns("Total")
            Dim ColCostoUnitario As GridColumn = View.Columns("Costo")
            Dim ColIdMotivoDevolucion As GridColumn = View.Columns("IdMotivoDevolucion")
            Dim ColPesoBruto As GridColumn = View.Columns("PesoBruto")
            Dim ColPesoNeto As GridColumn = View.Columns("PesoNeto")

            Dim Cantidad As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "Cantidad")), 0, View.GetRowCellValue(e.RowHandle, "Cantidad"))
            Dim Peso As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "Peso")), 0, View.GetRowCellValue(e.RowHandle, "Peso"))
            Dim IdProductoBodega As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProductoBodega")), 0, View.GetRowCellValue(e.RowHandle, "IdProductoBodega"))
            Dim NoLinea As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "NoLinea")), 0, View.GetRowCellValue(e.RowHandle, "NoLinea"))
            Dim IdPropietarioBodega As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdPropietarioBodega")), 0, View.GetRowCellValue(e.RowHandle, "IdPropietarioBodega"))
            Dim ValorAduana As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorAduana")), 0, View.GetRowCellValue(e.RowHandle, "ValorAduana"))
            Dim ValorFOB As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorFOB")), 0, View.GetRowCellValue(e.RowHandle, "ValorFOB"))
            Dim ValorIVA As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorIVA")), 0, View.GetRowCellValue(e.RowHandle, "ValorIVA"))
            Dim ValorDAI As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorDAI")), 0, View.GetRowCellValue(e.RowHandle, "ValorDAI"))
            Dim ValorSeguro As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorSeguro")), 0, View.GetRowCellValue(e.RowHandle, "ValorSeguro"))
            Dim ValorFlete As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ValorFlete")), 0, View.GetRowCellValue(e.RowHandle, "ValorFlete"))
            Dim ValorTotal As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "Total")), 0, View.GetRowCellValue(e.RowHandle, "Total"))
            Dim vValorCostoUnitario As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "Costo")), 0, View.GetRowCellValue(e.RowHandle, "Costo"))
            Dim CodigoProducto As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "CodigoProducto")), 0, View.GetRowCellValue(e.RowHandle, "CodigoProducto"))
            Dim Nombre_Producto As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "NombreProducto")), 0, View.GetRowCellValue(e.RowHandle, "NombreProducto"))
            Dim IdUmBas As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdUmBas")), 0, View.GetRowCellValue(e.RowHandle, "IdUmBas"))
            Dim EsKit As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "EsKit")), 0, View.GetRowCellValue(e.RowHandle, "EsKit"))
            Dim IdProducto As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdProducto")), 0, View.GetRowCellValue(e.RowHandle, "IdProducto"))
            Dim IdOrdenCompraDetPadre As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDetPadre")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDetPadre"))
            Dim IdOrdenCompraDet As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenCompraDet"))
            Dim NombrePropietario As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "NombrePropietario")), 0, View.GetRowCellValue(e.RowHandle, "NombrePropietario"))
            Dim vIdMotivoDevolucion As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion")), 0, View.GetRowCellValue(e.RowHandle, "IdMotivoDevolucion"))

            '#GT29112024: validar si peso neto/bruto se deben actualizar cuando OC esta en BackOrder
            'ControlPeso
            Dim vControlPeso As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "ControlPeso")), 0, View.GetRowCellValue(e.RowHandle, "ControlPeso"))
            Dim vPesoBruto = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "PesoBruto")), 0, View.GetRowCellValue(e.RowHandle, "PesoBruto"))
            Dim vPesoNeto = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "PesoNeto")), 0, View.GetRowCellValue(e.RowHandle, "PesoNeto"))

            Dim vExiste_Linea As Boolean = Existe_Linea(NoLinea)
            Dim NoLineaGrid As Integer = 0
            Dim Etapa_Uno_Correcta As Boolean = False
            Dim Etapa_Dos_Correcta As Boolean = False
            Dim Etapa_Tres_Correcta As Boolean = False

            '#EJC20210313: Es culpable hasta que se demuestre lo contrario.
            Grid_Tiene_Error = True

            If NoLinea = 0 Then
                If lOCDetLn.Count = 0 Then
                    NoLinea += 1
                Else
                    NoLinea = lOCDetLn.Max(Function(x) x.No_Linea) + 1
                End If
                View.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "NoLinea", NoLinea)
            End If

            If Cantidad = 0 Then
                e.Valid = False
                View.SetColumnError(ColCantidad, "Ingrese cantidad > 0")
            ElseIf IdProductoBodega = 0 Then
                e.Valid = False
                View.SetColumnError(ColProducto, "Ingrese un código de producto válido")
            ElseIf Not IsNumeric(NoLinea) OrElse vExiste_Linea Then
                e.Valid = False
                If vExiste_Linea Then
                    View.SetColumnError(ColLinea, "El número de línea ya existe")
                Else
                    View.SetColumnError(ColLinea, "Ingrese un valor numérico")
                End If
            ElseIf IdPropietarioBodega = 0 AndAlso TipoTrans = eTipoTrans.Consolidado Then
                e.Valid = False
                View.SetColumnError(ColPropietario, "Seleccione el propietario de la mercancía.")
            ElseIf vControlPeso AndAlso vPesoBruto = 0 Then
                e.Valid = False
                View.SetColumnError(ColPesoBruto, "Ingrese un peso bruto.")
            ElseIf vControlPeso AndAlso vPesoNeto = 0 Then
                e.Valid = False
                View.SetColumnError(ColPesoNeto, "Ingrese un peso neto.")
            Else
                e.Valid = True : Etapa_Uno_Correcta = True
            End If

            '#EJC20210609: Corrección de calculos por tipo de documento.
            If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse
                cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then

                If vValorCostoUnitario = 0 Then
                    e.Valid = False
                    View.SetColumnError(ColCostoUnitario, "Ingrese costo unitario > 0")
                    'View.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Costo", 0.01)
                ElseIf ValorTotal = 0 Then
                    e.Valid = False
                    View.SetColumnError(ColTotal, "Total de la línea debe ser > 0")
                Else
                    Etapa_Dos_Correcta = True
                End If

            ElseIf cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA OrElse
                clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then

                'GT08092022: valor obligatorio para VALOR_ADUANA/DAI,Costo,Cantidad. Los demas son opcionales
                If BeTipoDocumento.Control_Poliza Then
                    'GT 26082021: columna costo unitario aplica dentro de consolidado/duda
                    If ValorAduana = 0 Then
                        e.Valid = False
                        View.SetColumnError(ColValorAduana, "Ingrese Valor Aduana > 0")
                    ElseIf vValorCostoUnitario = 0 Then
                        e.Valid = False
                        View.SetColumnError(ColCostoUnitario, "Ingrese Costo Unitario > 0")
                    ElseIf ValorIVA = 0 Then
                        e.Valid = False
                        View.SetColumnError(ColValorIVA, "Ingrese Valor IVA > 0")
                    ElseIf ValorTotal = 0 Then
                        e.Valid = False
                        View.SetColumnError(ColTotal, "Ingrese Total > 0")
                    Else
                        Etapa_Dos_Correcta = True
                    End If
                Else
                    Etapa_Dos_Correcta = True
                End If

            End If

            If BeTipoDocumento.Es_devolucion Then
                If vIdMotivoDevolucion = 0 Then
                    e.Valid = False
                    View.SetColumnError(ColIdMotivoDevolucion, "Seleccione motivo de devolución por línea")
                    Etapa_Dos_Correcta = False
                End If
            End If

            If Etapa_Uno_Correcta AndAlso Etapa_Dos_Correcta Then

                Dim vIndiceFila As Integer = e.RowHandle

                If vIndiceFila = -2147483647 Then
                    vIndiceFila = gvDetalleDocIngreso.DataRowCount
                End If

                Dim vIndiceExistente As Integer = lOCDetLn.FindIndex(Function(x) x.RowIndex = vIndiceFila)

                If vIndiceExistente = -1 Then
                    vIndiceExistente = lOCDetLn.FindIndex(Function(x) x.IdProductoBodega = IdProductoBodega)
                    vIndiceFila = vIndiceExistente
                    If vIndiceFila <> -1 AndAlso vIndiceFila = -2147483647 Then
                        e.Valid = False
                        View.SetColumnError(ColProducto, "El material ya existe!")
                        Exit Sub
                    End If
                End If

                Dim BeTransOCDet As New clsBeTrans_oc_det
                Dim ObjDetHijo As New clsBeTrans_oc_det

                If vIndiceExistente = -1 Then

                    BeTransOCDet.No_Linea = NoLinea
                    BeTransOCDet.IdProductoBodega = IdProductoBodega
                    BeTransOCDet.Codigo_Producto = CodigoProducto
                    BeTransOCDet.Nombre_producto = Nombre_Producto
                    BeTransOCDet.IdUnidadMedidaBasica = IdUmBas
                    BeTransOCDet.IdPresentacion = 0
                    BeTransOCDet.RowIndex = vIndiceFila
                    BeTransOCDet.valor_aduana = ValorAduana
                    BeTransOCDet.valor_fob = ValorFOB
                    BeTransOCDet.valor_iva = ValorIVA
                    BeTransOCDet.valor_dai = ValorDAI
                    BeTransOCDet.valor_seguro = ValorSeguro
                    BeTransOCDet.valor_flete = ValorFlete
                    BeTransOCDet.Total_linea = ValorTotal
                    BeTransOCDet.Cantidad = Cantidad
                    lOCDetLn.Add(BeTransOCDet)

                Else

                    BeTransOCDet = lOCDetLn(vIndiceFila)
                    BeTransOCDet.No_Linea = NoLinea
                    BeTransOCDet.IdProductoBodega = IdProductoBodega
                    BeTransOCDet.Codigo_Producto = CodigoProducto
                    BeTransOCDet.Nombre_producto = Nombre_Producto
                    BeTransOCDet.IdUnidadMedidaBasica = IdUmBas
                    BeTransOCDet.RowIndex = vIndiceFila
                    BeTransOCDet.valor_aduana = ValorAduana
                    BeTransOCDet.valor_fob = ValorFOB
                    BeTransOCDet.valor_iva = ValorIVA
                    BeTransOCDet.valor_dai = ValorDAI
                    BeTransOCDet.valor_seguro = ValorSeguro
                    BeTransOCDet.valor_flete = ValorFlete
                    BeTransOCDet.Total_linea = ValorTotal
                    BeTransOCDet.Cantidad = Cantidad

                End If

                '#EJC20200309:
                If EsKit Then

                    If BeTransOCDet.lProductoComposicionKit.Count = 0 Then
                        Dim vIdProductoBodegaPadre As Integer = IdProductoBodega
                        lProductoKitComposicion = clsLnProducto_kit_composicion.Get_All_By_IdProducto_And_IdBodega(IdProducto, cmbBodega.EditValue)
                        BeTransOCDet.lProductoComposicionKit = lProductoKitComposicion
                    Else
                        lProductoKitComposicion = BeTransOCDet.lProductoComposicionKit
                    End If

                    If Not BeTransOCDet.lProductoComposicionKit Is Nothing Then

                        '#EJC20210404: Si no se han agregado aún los hijos...
                        If BeTransOCDet.lProductosHijosKit.Count = 0 Then

#Region "LLENAR_COMPOSICION"

                            Dim vIndiceHijo As Integer = -1
                            Dim vIndiceLista As Integer = -1
                            Dim vNoLineaHijo As Integer = NoLinea + 1

                            For Each PKC In lProductoKitComposicion

                                'IdOrdenCompraDetPadre

                                vIndiceHijo = lOCDetLn.FindIndex(Function(x) x.IdProductoBodega = PKC.Producto.Codigo)

                                If vIndiceHijo = -1 Then

                                    ObjDetHijo = New clsBeTrans_oc_det()
                                    ObjDetHijo.No_Linea = NoLinea
                                    ObjDetHijo.IdProductoBodega = PKC.Producto.IdProductoBodega
                                    ObjDetHijo.Codigo_Producto = PKC.Producto.Codigo
                                    ObjDetHijo.Nombre_producto = PKC.Producto.Nombre
                                    ObjDetHijo.IdUnidadMedidaBasica = PKC.IdUnidadMedidaBasicaHijo
                                    ObjDetHijo.IdPresentacion = 0
                                    ObjDetHijo.RowIndex = vIndiceHijo
                                    ObjDetHijo.valor_aduana = 0
                                    ObjDetHijo.valor_fob = 0
                                    ObjDetHijo.valor_iva = 0
                                    ObjDetHijo.valor_dai = 0
                                    ObjDetHijo.valor_seguro = 0
                                    ObjDetHijo.valor_flete = 0
                                    ObjDetHijo.Total_linea = 0
                                    ObjDetHijo.Cantidad = PKC.Cantidad * Cantidad
                                    ObjDetHijo.Peso = PKC.Producto.Peso_referencia
                                    BeTransOCDet.lProductosHijosKit.Add(ObjDetHijo)

                                    View.AddNewRow()
                                    Dim newRowHandle As Integer = View.FocusedRowHandle
                                    Dim newRow As Object = View.GetRow(newRowHandle)

                                    View.SetRowCellValue(newRowHandle, "IdPropietarioBodega", IdPropietarioBodega)
                                    View.SetRowCellValue(newRowHandle, "NombrePropietario", NombrePropietario)
                                    View.SetRowCellValue(newRowHandle, "NoLinea", vNoLineaHijo)
                                    View.SetRowCellValue(newRowHandle, "IdProductoBodega", PKC.Producto.IdProductoBodega)
                                    View.SetRowCellValue(newRowHandle, "CodigoProducto", PKC.Producto.Codigo)
                                    View.SetRowCellValue(newRowHandle, "NombreProducto", PKC.Producto.Nombre)
                                    View.SetRowCellValue(newRowHandle, "IdUmBas", PKC.Producto.IdUnidadMedidaBasica)
                                    View.SetRowCellValue(newRowHandle, "UMBas", PKC.Producto.UnidadMedida.Nombre)
                                    View.SetRowCellValue(newRowHandle, "Cantidad", PKC.Cantidad * Cantidad)
                                    View.SetRowCellValue(newRowHandle, "EsKit", False)
                                    View.SetRowCellValue(newRowHandle, "IsNew", True)
                                    View.SetRowCellValue(newRowHandle, "IdOrdenCompraDetPadre", IdProductoBodega)

                                    If PKC.Producto.Control_peso Then
                                        View.SetRowCellValue(newRowHandle, "Peso", IdProductoBodega)
                                    End If

                                    vNoLineaHijo += 1

                                Else

                                    BeTransOCDet.lProductosHijosKit.Find(Function(x) x.IdProductoBodega = PKC.Producto.IdProductoBodega).Cantidad = PKC.Cantidad * Cantidad

                                    vIndiceHijo = Get_Indice_Producto_Hijo(PKC.Producto.IdProductoBodega, IdProductoBodega)

                                    If vIndiceHijo <> -1 Then
                                        View.SetRowCellValue(vIndiceHijo, "Cantidad", PKC.Cantidad * Cantidad)
                                    End If

                                End If

                            Next
#End Region

                        End If

                    End If

                End If

                Etapa_Tres_Correcta = True

            End If

            If Etapa_Uno_Correcta AndAlso Etapa_Dos_Correcta AndAlso Etapa_Tres_Correcta Then
                Grid_Tiene_Error = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Get_Indice_Producto_Hijo(ByVal pIdProductoBodega As Integer, ByVal pIdProductoPadre As Integer) As Integer

        Get_Indice_Producto_Hijo = -1

        Try

            If pIdProductoBodega <> 0 Then

                Dim gridIdProductoBodega As Integer = 0
                Dim gridIdProductoPadre As Integer = 0

                For i As Integer = 0 To gvDetalleDocIngreso.DataRowCount - 1

                    gridIdProductoBodega = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdProductoBodega")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "IdProductoBodega"))
                    gridIdProductoPadre = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDetPadre")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "IdOrdenCompraDetPadre"))

                    If (Val(gridIdProductoBodega) = pIdProductoBodega) AndAlso (gridIdProductoPadre = pIdProductoPadre) Then
                        Return i
                    End If

                Next i

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Existe_Linea(ByVal pNoLInea As Integer) As Boolean

        Existe_Linea = False

        Try

            Dim ExisteLinea As Boolean
            If pNoLInea <> 0 Then
                Dim NoLineaGrid As Integer = 0
                For i As Integer = 0 To gvDetalleDocIngreso.DataRowCount - 1
                    NoLineaGrid = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea"))
                    If Val(NoLineaGrid) = pNoLInea Then
                        ExisteLinea = True
                        Exit For
                    End If
                Next i
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub gvDetalleDocIngreso_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles gvDetalleDocIngreso.InvalidRowException

        Try

            '#EJC20210307: Evita que salte mensaje indicando si se quiere corregir la fila.
            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Function Get_No_Linea_Grid_Detalle() As Integer

        Get_No_Linea_Grid_Detalle = 1

        Try

            Dim vNoLineaGrid As Integer = 0
            Dim vNoLineaSiguiente As Integer = 0

            For i As Integer = 0 To gvDetalleDocIngreso.DataRowCount - 1
                vNoLineaGrid = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea")), 0, gvDetalleDocIngreso.GetRowCellValue(i, "NoLinea"))
            Next i

            Get_No_Linea_Grid_Detalle = vNoLineaGrid + 1

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Function

    Private Sub gvDetalleDocIngreso_ShowingEditor(sender As Object, e As CancelEventArgs) Handles gvDetalleDocIngreso.ShowingEditor

        Try

            Dim IdOrdenCompraDetPadre As Integer = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdOrdenCompraDetPadre")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdOrdenCompraDetPadre"))
            Dim ControlPeso As Boolean = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ControlPeso")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "ControlPeso"))
            Dim PesoReferenciaUMBas As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoReferenciaUMBas")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoReferenciaUMBas"))
            Dim IdPresentacion As Double = CType(IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "IdPresentacion")), Integer)
            Dim Cantidad As Double = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad")), 0, gvDetalleDocIngreso.GetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "Cantidad"))

            If Not IdOrdenCompraDetPadre = 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If

            If ControlPeso Then

                If IdPresentacion = 0 Then
                    If vBeProducto.IdProducto > 0 AndAlso vBeProducto.Peso_referencia > 0 Then
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoBruto", vBeProducto.Peso_referencia * Cantidad)
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoNeto", vBeProducto.Peso_referencia * Cantidad)
                    End If
                Else
                    Dim BePresentacion As New clsBeProducto_Presentacion()
                    BePresentacion = clsLnProducto_presentacion.GetSingle(IdPresentacion)

                    If Not BePresentacion Is Nothing Then
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoBruto", BePresentacion.Peso * Cantidad)
                        gvDetalleDocIngreso.SetRowCellValue(gvDetalleDocIngreso.FocusedRowHandle, "PesoNeto", BePresentacion.Peso * Cantidad)
                    End If

                End If

            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub gvDetalleDocIngreso_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvDetalleDocIngreso.RowStyle

        Try

            Dim IdOrdenCompraDetPadre As Integer = IIf(IsDBNull(gvDetalleDocIngreso.GetRowCellValue(e.RowHandle, "IdOrdenCompraDetPadre")), 0, gvDetalleDocIngreso.GetRowCellValue(e.RowHandle, "IdOrdenCompraDetPadre"))

            If IdOrdenCompraDetPadre = 0 Then
                e.Appearance.BackColor = Color.White
            Else
                e.Appearance.BackColor = Color.Salmon
            End If

            e.HighPriority = True   'override any other formatting  

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub Eliminar_Fila(ByVal pGridControl As GridControl)

        Dim currentView As GridView = CType(pGridControl.FocusedView, GridView)
        Dim BeTransOcDet As clsBeTrans_oc_det

        Try

            Dim IdProductoBodega As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoBodega")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProductoBodega"))
            Dim EsKit As Boolean = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "EsKit")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "EsKit"))
            Dim IdProducto As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProducto")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdProducto"))
            Dim IdOrdenCompraDetPadre As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdOrdenCompraDetPadre")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdOrdenCompraDetPadre"))
            Dim IdOrdenCompraDet As Integer = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdOrdenCompraDet")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "IdOrdenCompraDet"))

            Dim lProductosHijosKitAEliminar As New List(Of clsBeTrans_oc_det)

            If IdOrdenCompraDetPadre = 0 Then

                BeTransOcDet = lOCDetLn.Find(Function(x) x.IdProductoBodega = IdProductoBodega AndAlso x.IdOrdenCompraDet = IdOrdenCompraDet)

                If EsKit Then

                    If BeTransOcDet.Cantidad_recibida = 0 Then

                        For Each H In BeTransOcDet.lProductosHijosKit
                            Dim vIndiceHijo As Integer = Get_Indice_Producto_Hijo(H.IdProductoBodega, IdProductoBodega)
                            lProductosHijosKitAEliminar.Add(H)
                            currentView.DeleteRow(vIndiceHijo)
                        Next

                        For Each H2D In lProductosHijosKitAEliminar
                            BeTransOcDet.lProductosHijosKit.Remove(H2D)
                            If Not BeTransOcDet.IsNew Then
                                clsLnTrans_oc_det.Delete(BeTransOcDet.IdOrdenCompraEnc, H2D.IdOrdenCompraDet)
                            End If
                        Next

                    Else
                        XtraMessageBox.Show("Se registró ingreso para la línea, no se puede eliminar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Else

                    If BeTransOcDet IsNot Nothing Then

                        If BeTransOcDet.Cantidad_recibida = 0 Then

                            lOCDetLn.Remove(BeTransOcDet)
                            currentView.DeleteRow(currentView.FocusedRowHandle)

                            If Not BeTransOcDet.IsNew Then
                                clsLnTrans_oc_det.Delete(BeTransOcDet.IdOrdenCompraEnc, IdOrdenCompraDet)
                            End If

                        Else
                            XtraMessageBox.Show("Se registró ingreso para la línea, no se puede eliminar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Else
                        '#EJC20210630: Is not a commited row.
                        currentView.DeleteRow(currentView.FocusedRowHandle)
                    End If

                End If

            Else

                XtraMessageBox.Show("El producto pertenece a un producto kit, no se puede eliminar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '#EJC20210309: Es el hijo de un producto kit, no eliminar, eliminar el padre.

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub



    Private Sub frmOrdenCompra2_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Try

            '#EJC20210428: Ocurre que si se cierra la forma cuando aún está cargando, suceden errores.
            'Entonces capturo si se está cerrando para evitar que continue haciendo cosas en el load.

            If IsLoading Then
                IsClosing = True
                e.Cancel = True
            Else
                IsClosing = False
                If Not InvokeListarPedidosCompra Is Nothing Then
                    InvokeListarPedidosCompra.Invoke()
                End If
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub gvDetalleDocIngreso_ShownEditor(sender As Object, e As EventArgs) Handles gvDetalleDocIngreso.ShownEditor

        Try

            Dim view As ColumnView = DirectCast(sender, ColumnView)


            If view.FocusedColumn.FieldName = "IdPresentacion" Then

                Dim editor As GridLookUpEdit = CType(view.ActiveEditor, GridLookUpEdit)
                Dim pIdProductoBodega As String = Convert.ToString(view.GetFocusedRowCellValue("IdProductoBodega"))

                If Not Val(pIdProductoBodega) = 0 Then
                    editor.Properties.DataSource = clsLnProducto_presentacion.Get_All_By_IdProductoBodega(Val(pIdProductoBodega))
                End If


            ElseIf view.FocusedColumn.FieldName = "IdProductoBodega" Then

                Dim editor As GridLookUpEdit = CType(view.ActiveEditor, GridLookUpEdit)
                Dim pIdPropietarioBodega As String = Convert.ToString(view.GetFocusedRowCellValue("IdPropietarioBodega"))

                If pIdPropietarioBodega.Trim = String.Empty Then pIdPropietarioBodega = lcmbPropietario.EditValue
                If Val(pIdPropietarioBodega) = 0 AndAlso gBeOrdenCompra IsNot Nothing Then
                    pIdPropietarioBodega = gBeOrdenCompra.IdPropietarioBodega.ToString()
                End If

                If Not Val(pIdPropietarioBodega) = 0 Then
                    OCProductoLookup_Asignar(editor, Val(pIdPropietarioBodega))
                    '#EJC20260603_OC_LOOKUP_SYNC: sincronizar también el repository para que DisplayMember no se pierda al salir del editor.
                    OCProductoLookup_Asignar(ProductoGridLookUpEdit, Val(pIdPropietarioBodega))
                Else
                    OCProductoLookup_Asignar(editor, 0)
                    OCProductoLookup_Asignar(ProductoGridLookUpEdit, 0)
                End If

                Debug.WriteLine("OC_LOOKUP_SYNC shownEditor;idPropietarioBodega=" & pIdPropietarioBodega &
                                ";editorRows=" & If(editor.Properties.DataSource Is Nothing, -1, CType(editor.Properties.DataSource, DataTable).Rows.Count) &
                                ";repoRows=" & If(ProductoGridLookUpEdit.DataSource Is Nothing, -1, CType(ProductoGridLookUpEdit.DataSource, DataTable).Rows.Count))
                OCTrace_Marca("producto_lookup_sync",
                              "idPropietarioBodega=" & pIdPropietarioBodega)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuTareaRecepcion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTareaRecepcion.ItemClick
        Crear_Nueva_Recepcion_Desde_OC()
    End Sub

    Private Sub Crear_Nueva_Recepcion_Desde_OC()

        Try

            If NuevaRecepcion(0) Then
                InvokeListarPedidosCompra.Invoke()
                DialogResult = DialogResult.OK
                Close()
            Else
                InvokeListarPedidosCompra.Invoke()
                Close()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public pListaPedidos As New List(Of Integer)
    Public pBePedidoEncDevolRef As New clsBeTrans_pe_enc
    Private vMensajeDevolucionRutaExcepcionProveedor As String = String.Empty

    Private Sub lnkPedido_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPedido.LinkClicked

        Dim ObjDet As New clsBeTrans_oc_det
        Dim BeProveedorBodega As New clsBeProveedor_bodega()

        Try

            pBePedidoEncDevolRef = Nothing

            '#EJC20210617: Le envío a la forma los pedidos ya incluidos en el Picking
            Dim bo As New frmPedidoDetalleBuscador() With {.pListaPedidos = pListaPedidos,
                                                           .IdBodega = cmbBodega.EditValue,
                                                           .EstadoDespachado = True}

            bo.ShowDialog()

            If Not bo.pBePedidoEnc Is Nothing Then

                If bo.pBePedidoEnc.IdCliente <> 0 AndAlso bo.pBePedidoEnc.IdPropietarioBodega <> 0 Then

                    If bo.pBePedidoEnc.Detalle IsNot Nothing AndAlso bo.pBePedidoEnc.Detalle.Count > 0 Then

                        pListaPedidos.Clear()
                        dgridPedidos.Rows.Clear()
                        DTGridDetalleDocIngresos.Rows.Clear()

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Cargando Pedido")

                        Cursor = Cursors.WaitCursor

                        Application.DoEvents()

                        Dim i As Integer = dgridPedidos.Rows.Add()

                        dgridPedidos.Rows(i).Cells("IdPedido").Value = bo.pBePedidoEnc.IdPedidoEnc
                        dgridPedidos.Rows(i).Cells("Referencia").Value = bo.pBePedidoEnc.Referencia
                        dgridPedidos.Rows(i).Cells("Bodega").Value = bo.pBePedidoEnc.IdBodega
                        dgridPedidos.Rows(i).Cells("Cliente").Value = bo.pBePedidoEnc.Cliente.Nombre_comercial
                        dgridPedidos.Rows(i).Cells("Propietario").Value = bo.pBePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial
                        dgridPedidos.Rows(i).Cells("FechaPedido").Value = bo.pBePedidoEnc.Fecha_Pedido
                        dgridPedidos.Rows(i).Cells("EstadoP").Value = bo.pBePedidoEnc.Estado

                        dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        dgridPedidos.EndEdit()

                        '#EJC20210908
                        pListaPedidos.Add(bo.pBePedidoEnc.IdPedidoEnc)
                        pBePedidoEncDevolRef = bo.pBePedidoEnc.Clone()

                        txtIdPedidoDevolucionEnc.Text = pBePedidoEncDevolRef.IdPedidoEnc
                        txtNombPedido.Text = pBePedidoEncDevolRef.Referencia
                        txtReferencia.Text = pBePedidoEncDevolRef.Referencia

                        vMensajeDevolucionRutaExcepcionProveedor = String.Empty

                        Dim BeProveedor As New clsBeProveedor
                        BeProveedor = clsLnProveedor.Existe(pBePedidoEncDevolRef.Cliente.Codigo)

                        If Not BeProveedor Is Nothing Then

                            BeProveedorBodega = New clsBeProveedor_bodega()
                            BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                            BeProveedorBodega.IdBodega = cmbBodega.EditValue

                            '#EJC20211223: Tropicalización específica para MERCOPAN, porque la interface desactiva al proveedor porque no existe en aurora.
                            If Not BeProveedor.Activo Then
                                If cmbTipoIngreso.EditValue = clsDataContractDI.tTipoDocumentoIngreso.Liquidacion_De_Ruta_Devolucion Then
                                    BeProveedor.Activo = True
                                    clsLnProveedor.Actualizar_Activo(BeProveedor)
                                End If
                            End If

                            If clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega) Then

                                vMensajeDevolucionRutaExcepcionProveedor = "El cliente: " & pBePedidoEncDevolRef.Cliente.Codigo & ". Existe en WMS con código de proveedor: " & BeProveedor.Codigo & " pero no se encontró su asoicación con la bodega: " & cmbBodega.EditValue

                                txtIdProveedor.Text = BeProveedor.IdProveedor
                                txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion

                                If Valida_Proveedor() Then
                                    'All good, Erik  20211125
                                End If

                            Else

                                BeProveedorBodega = New clsBeProveedor_bodega()
                                BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID() + 1
                                BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                                BeProveedorBodega.IdBodega = pBePedidoEncDevolRef.IdBodega
                                BeProveedorBodega.User_agr = AP.UsuarioAp.IdUsuario
                                BeProveedorBodega.User_mod = AP.UsuarioAp.IdUsuario
                                BeProveedorBodega.Fec_agr = Now
                                BeProveedorBodega.Fec_mod = Now
                                BeProveedorBodega.Activo = True
                                clsLnProveedor_bodega.Insertar(BeProveedorBodega)

                                txtIdProveedor.Text = BeProveedor.IdProveedor
                                txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion

                                If Valida_Proveedor() Then

                                    XtraMessageBox.Show("Inyección de cliente a proveedor exitosa, note el proveedor del docuemtno.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                End If

                            End If

                        Else

                            BeProveedor = New clsBeProveedor()
                            BeProveedor.IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(pBePedidoEncDevolRef.IdBodega)
                            BeProveedor.IdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pBePedidoEncDevolRef.IdBodega, pBePedidoEncDevolRef.IdPropietarioBodega)

                            If BeProveedor.IdPropietario = 0 Then
                                Throw New Exception("No se pudo obtener el identificador de propietario asociado a la bodega: " & pBePedidoEncDevolRef.IdBodega & " IdPropietarioBodega =" & pBePedidoEncDevolRef.IdPropietarioBodega)
                            End If

                            BeProveedor.IdProveedor = clsLnProveedor.MaxID() + 1
                            BeProveedor.Codigo = pBePedidoEncDevolRef.Cliente.Codigo
                            BeProveedor.Nombre = pBePedidoEncDevolRef.Cliente.Nombre_comercial
                            BeProveedor.Telefono = pBePedidoEncDevolRef.Cliente.Telefono
                            BeProveedor.Nit = pBePedidoEncDevolRef.Cliente.Nit
                            BeProveedor.Direccion = pBePedidoEncDevolRef.Cliente.Direccion
                            BeProveedor.Email = pBePedidoEncDevolRef.Cliente.Correo_electronico
                            BeProveedor.Activo = True
                            BeProveedor.Muestra_precio = False
                            BeProveedor.User_agr = AP.UsuarioAp.IdUsuario
                            BeProveedor.Fec_agr = Now
                            BeProveedor.User_mod = AP.UsuarioAp.IdUsuario
                            BeProveedor.Fec_mod = Now
                            BeProveedor.Actualiza_costo_oc = False
                            BeProveedor.IdUbicacionVirtual = 0
                            BeProveedor.Es_Bodega_Recepcion = False
                            BeProveedor.Es_Bodega_Recepcion = False
                            BeProveedor.Referencia = "DEV" & pBePedidoEncDevolRef.Cliente.Codigo
                            BeProveedor.Sistema = False
                            BeProveedor.IdConfiguracionBarraPallet = 0
                            clsLnProveedor.Insertar(BeProveedor)

                            BeProveedorBodega = New clsBeProveedor_bodega()
                            BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID() + 1
                            BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                            BeProveedorBodega.IdBodega = pBePedidoEncDevolRef.IdBodega
                            BeProveedorBodega.User_agr = AP.UsuarioAp.IdUsuario
                            BeProveedorBodega.User_mod = AP.UsuarioAp.IdUsuario
                            BeProveedorBodega.Fec_agr = Now
                            BeProveedorBodega.Fec_mod = Now
                            BeProveedorBodega.Activo = True
                            clsLnProveedor_bodega.Insertar(BeProveedorBodega)

                            txtIdProveedor.Text = BeProveedor.IdProveedor
                            txtIdProveedor.Tag = BeProveedorBodega.IdAsignacion

                            If Valida_Proveedor() Then

                                XtraMessageBox.Show("Inyección de cliente a proveedor exitosa, note el proveedor del docuemtno.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            End If

                        End If

                        Dim vCantidadPendiente As Double = 0
                        Dim IdOrdenCompraDet As Integer = clsLnTrans_oc_det.MaxID(gBeOrdenCompra.IdOrdenCompraEnc) + 1
                        Dim IdMotivoDevolucion As Integer = 0
                        Dim IdArancel As Integer = 0
                        Dim BeMotivoDevolucion As New clsBeMotivo_devolucion()
                        BeMotivoDevolucion = clsLnMotivo_devolucion.Get_Motivo_Devolucion_Por_Defecto(cmbBodega.EditValue, lcmbPropietario.Tag)

                        If Not BeMotivoDevolucion Is Nothing Then
                            IdMotivoDevolucion = BeMotivoDevolucion.IdMotivoDevolucion
                        Else
                            XtraMessageBox.Show("No existe el dato maestro para motivo de devolución, deberá registralo manualmente en cada línea.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Llenando detalle...")

                        Llena_Motivos_Devolucion_LookUp_Grid()

                        Llena_ProductosLookUp_Grid(bo.pBePedidoEnc.IdPropietarioBodega)

                        For Each obj As clsBeTrans_pe_det In bo.pBePedidoEnc.Detalle

                            Debug.WriteLine("err")
                            vCantidadPendiente = Math.Round(obj.Cantidad - obj.Cant_despachada, 6)

                            DTGridDetalleDocIngresos.Rows.Add(bo.pBePedidoEnc.IdPropietarioBodega,
                                                              bo.pBePedidoEnc.PropietarioBodega.Propietario.Nombre_comercial,
                                                              obj.No_linea,
                                                              obj.IdProductoBodega,
                                                              obj.Codigo_Producto,
                                                              obj.Nombre_producto,
                                                              obj.Nom_unid_med,
                                                              obj.IdUnidadMedidaBasica,
                                                              obj.IdPresentacion,
                                                              IdArancel,
                                                              IdMotivoDevolucion,
                                                              obj.Cantidad,
                                                              0,
                                                              vCantidadPendiente,
                                                              obj.Peso_Bruto,
                                                              obj.Peso_Neto,
                                                              obj.Costo,
                                                              obj.valor_aduana,
                                                              obj.valor_fob,
                                                              obj.valor_iva,
                                                              obj.valor_dai,
                                                              obj.valor_seguro,
                                                              obj.valor_flete,
                                                              obj.Total_linea,
                                                              obj.Producto.IdProducto,
                                                              True,
                                                              gBeOrdenCompra.IdOrdenCompraEnc,
                                                              IdOrdenCompraDet,
                                                              False,
                                                              obj.Atributo_Variante_1,
                                                              obj.Producto.Kit,
                                                              0,
                                                              0,
                                                              obj.Producto.Control_peso,
                                                              obj.Producto.Peso_referencia)
                            IdOrdenCompraDet += 1

                        Next

                        Dim iColumna As Integer = 0
                        For Each column As GridColumn In gvDetalleDocIngreso.Columns
                            For ifila As Integer = 0 To gvDetalleDocIngreso.DataRowCount - 1
                                'Llena_ObjBeDet_Devol(ifila, column.AbsoluteIndex)
                            Next
                        Next

                    End If

                Else
                    XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("El pedido no se creó correctamente: No tiene asociado cliente o propietario, corríjalo desde el mantenimiento de pedidos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            Exportar_Grid_A_Excel(DgridDetalleOC, "WMS_DetalleDocumentoIngreso_" & txtNoDocumento.Text & ".xlsx")

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    '#EJC20210716:Guardar LayoutGrid en LotesPorUbi.
    Private vNombreArchivoLayOutGridgvDetalleDocIngreso As String = String.Empty

    Private Sub gvDetalleDocIngreso_Layout(sender As Object, e As EventArgs) Handles gvDetalleDocIngreso.Layout

        Try

            Dim Ms As New MemoryStream
            gvDetalleDocIngreso.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridgvDetalleDocIngreso,
                                                          LayoutToText)

            'gvDetalleDocIngreso.SaveLayoutToXml(vNombreArchivoLayOutGridgvDetalleDocIngreso)

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub mnuEliminarDiseñoGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridgvDetalleDocIngreso)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                clsLnConfiguracion_usuario_det.Eliminar(BeConfiguracionUsuarioDet)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If

        Catch ex As Exception
            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
        End Try

    End Sub

    Private Sub Set_LayOut_Grid_Detalle_Documento_Ingreso()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridgvDetalleDocIngreso)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gvDetalleDocIngreso.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuRegistrarEnNAV_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRegistrarEnNAV.ItemClick

        Try

            If Not gBeOrdenCompra Is Nothing Then

                If gBeOrdenCompra.Push_To_NAV Then

                    If (Not gBeOrdenCompra.No_Documento_Recepcion_ERP = String.Empty AndAlso Not gBeOrdenCompra.Referencia = String.Empty) Then

                        If Not clsLnTrans_oc_enc.Registros_Pendientes_Push(gBeOrdenCompra.IdOrdenCompraEnc) Then

                            If gBeOrdenCompra.No_Documento_Ubicacion_ERP = String.Empty Then

                                If XtraMessageBox.Show("¿Registrar documento en NAV-ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    Dim ArchHeader As New wsTOMHH.clsArchHeader
                                    ArchHeader.Tipo = "WM"

                                    Dim vResultPutAway As Boolean = wsTOMHHInstance.Registrar_Pedido_Compra_To_NAV_For_BYB(ArchHeader,
                                                                                                                           gBeOrdenCompra.IdOrdenCompraEnc,
                                                                                                                           gBeOrdenCompra.Referencia,
                                                                                                                           gBeOrdenCompra.No_Documento_Recepcion_ERP)

                                    If Not vResultPutAway Then
                                        Throw New Exception("Error NAV - No se pudo realizar el registro.")
                                    Else
                                        XtraMessageBox.Show("El registro se realizó correctamente",
                                                            Text,
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Information)
                                    End If

                                End If

                            Else

                                XtraMessageBox.Show("El documento ya fue registrado anteriormente en la ubicación " & gBeOrdenCompra.No_Documento_Ubicacion_ERP,
                                                       Text,
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information)

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)

            Try
                'clsLnLog_error_wms.Agregar_Error(vMsgError)
                clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)
            Catch ex1 As Exception
                Debug.Print(ex1.Message)
            End Try

            XtraMessageBox.Show(ex.Message,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

        End Try

    End Sub

    Private Function cmdCorreccionPoliza_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCorreccionPoliza.ItemClick


        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String
        Dim BeMoticoCorreccionBodega As New clsBeTrans_oc_pol_motivo_correccion_bodega

        Try

            BeMoticoCorreccionBodega.IdBodega = cmbBodega.EditValue

            ms.IdMenu = e.Link.KeyTip
            clsLnMenu_sistema.GetSingle(ms)

            If (ms.Solicitar_clave_autorizacion) Then

                us.IdUsuario = AP.UsuarioAp.IdUsuario
                clsLnUsuario.GetSingle(us)

                Try

                    clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                    If (clave = String.Empty) Then Throw New Exception

                Catch ex As Exception
                    MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Return False
                End Try

                Dim frmlog As New frmAjusteLogin() With {.clave = clave}

                If frmlog.ShowDialog() <> DialogResult.Yes Then
                    frmlog.Dispose() : Return False
                Else
                    pCorreccionPoliza = True
                End If

                frmlog.Dispose()

                Using MA As New frmMotivo_CorrecionList()

                    With MA

                        .Modo = frmMotivo_CorrecionList.pModo.Seleccion
                        .BeMotivoCorreccion.IdBodega = BeMoticoCorreccionBodega.IdBodega

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                        End If

                        If .ShowDialog() = DialogResult.OK Then
                            '#MECR03102025: Se agrego nueva bitacora de logs para OC
                            Dim msgAdvertencia As String = "ADVERTENCIA_202310091900: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " corrige póliza con el idmotivo: " & MA.BeMotivoCorreccion.IdMotivoCorreccion & " para la OC: " & gBeOrdenCompra.IdOrdenCompraEnc
                            'clsLnLog_error_wms.Agregar_Error(vMsgError)
                            clsLnLog_error_wms_oc.Agregar_Error(msgAdvertencia, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario)

                            txtScanPoliza.Enabled = True
                            txtScanPoliza.ReadOnly = False
                            txtScanPoliza.EditValue = String.Empty
                            lbScanPoliza.Enabled = True
                            txtScanPoliza.Focus()
                        Else
                            txtScanPoliza.Enabled = False
                            txtScanPoliza.ReadOnly = True
                            lbScanPoliza.Enabled = False
                        End If

                    End With

                End Using

                SplashScreenManager.CloseForm(False)

                Return True

            Else
                Return True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            If cmbBodega.EditValue <> 0 Then

                BeBodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue)
                BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue, AP.IdEmpresa)

                Listar_Propietarios()

                IMS.Listar_TipoIngresoOC(cmbTipoIngreso, BeBodega.Es_Bodega_Fiscal)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdImprimirEtiquetasRecepcion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimirEtiquetasRecepcion.ItemClick

        Try

            Dim pd As PrintDialog = New PrintDialog()
            pd.PrinterSettings = New PrinterSettings()

            Dim lBeTransReDet As New List(Of clsBeTrans_re_det)

            lBeTransReDet = clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(lblC.Text)

            If DialogResult.OK = pd.ShowDialog(Me) Then

                For Each redet As clsBeTrans_re_det In lBeTransReDet
                    Imprimir_Etiqueta(redet, pd.PrinterSettings.PrinterName)
                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    Private Sub Imprimir_Etiqueta(ByVal pReDet As clsBeTrans_re_det,
                                  ByVal PrinterName As String)

        Dim ZPLString As String = ""
        Dim pTipoEtiqueta As Integer = 8
        Dim vEmpresa As String = AP.Empresa.Nombre
        Dim vCodigoBarra As String = "$" & pReDet.Lic_plate.Substring(0, IIf(pReDet.Lic_plate.Length < 10, pReDet.Lic_plate.Length, 9)) ' "20240123"
        Dim vCodigoProducto As String = pReDet.Codigo_Producto ' "COD123456"
        Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 47, pReDet.Nombre_producto.Length, 46)) '"PRAZOLEN 20MG CAJA X 15 CAPSULAS MUY LARGO PARA"
        Dim vLote As String = pReDet.Lote '"LOT240123"
        Dim vFechaVence As String = pReDet.Fecha_vence.ToString("dd/MM/yyyy") '"23/01/2025"


        If pTipoEtiqueta = 8 Then

            ZPLString = String.Format(" ^XA
                                        ^PW784
                                        ^LL200
                                        ^LH0,0
                                        ^FO30,15^A0N,15,15^FDTOMWMS - {0}^FS
                                        ^FO30,40^BXN,3,200,,C^FD{1}^FS
                                        ^FO80,30^A0N,13,13^FD{2}^FS
                                        ^FO80,50^A0N,13,13^FB200,2,,^FD{3}^FS
                                        ^FO80,80^A0N,13,13^FDLote: {4}^FS
                                        ^FO80,95^A0N,13,13^FDFecha: {5}^FS
                                        ^FO280,15^A0N,15,15^FDTOMWMS - {0}^FS
                                        ^FO280,40^BXN,3,200,,C^FD{1}^FS
                                        ^FO330,30^A0N,13,13^FD{2}^FS
                                        ^FO330,50^A0N,13,13^FB200,2,,^FD{3}^FS
                                        ^FO330,80^A0N,13,13^FDLote: {4}^FS
                                        ^FO330,95^A0N,13,13^FDFecha: {5}^FS
                                        ^FO530,15^A0N,15,15^FDTOMWMS - {0}^FS
                                        ^FO530,40^BXN,3,200,,C^FD{1}^FS
                                        ^FO580,30^A0N,13,13^FD{2}^FS
                                        ^FO580,50^A0N,13,13^FB200,2,,^FD{3}^FS
                                        ^FO580,80^A0N,13,13^FDLote: {4}^FS
                                        ^FO580,95^A0N,13,13^FDFecha: {5}^FS
                                        ^XZ", vEmpresa, vCodigoBarra, vCodigoProducto, vNombreProducto, vLote, vFechaVence)


        End If

        Try

            If ZPLString <> "" Then

                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
            Else
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el tipo de etiqueta"),
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error)
            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Impresión de ubicaciones",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cmdDuplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdDuplicar.ItemClick

        Try

            If XtraMessageBox.Show("¿Duplicar documento de ingreso?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Dim vIdOrdenCompraNueva As Integer = clsLnTrans_oc_enc.Duplicar(lblC.Text, AP.UsuarioAp.IdUsuario)

                If vIdOrdenCompraNueva > 0 Then
                    XtraMessageBox.Show("Se generó correctamente la copia del documento base, nuevo correlativo:" & vIdOrdenCompraNueva, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not InvokeCargarPedidoCompra Is Nothing Then InvokeCargarPedidoCompra.Invoke(vIdOrdenCompraNueva)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtIdRecepcion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles txtIdRecepcion.LinkClicked

        Dim gBeRecepcion As New clsBeTrans_re_enc

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Procesando Recepción...")

            If Val(txtIdRecepcion.Text) = 0 Then
                Crear_Nueva_Recepcion_Desde_OC()
                Exit Sub
            Else
                gBeRecepcion = clsLnTrans_re_enc.GetSingle(txtIdRecepcion.Text)

                Cierra_Instancia_Previa(frmRecepcion)

                With frmRecepcion
                    .Modo = frmRecepcion.TipoTrans.Editar
                    .MdiParent = MdiParent
                    .gBeRecepcionEnc = gBeRecepcion
                    .Listar = AddressOf Refrescar_Lista_OC_Desde_Recepcion

                    If OpcionesMenu IsNot Nothing Then
                        .OpcionesMenu = OpcionesMenu
                    End If

                    .Show()
                    .Focus()
                End With

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, ex.StackTrace)

        End Try

    End Sub

    ''' <summary>
    ''' #EJC20240326: Permite escanear en la celda del grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProductoGridLookUpEdit_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then

                Dim View As GridView = CType(DgridDetalleOC.DefaultView, GridView)
                Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
                Dim ColProducto As GridColumn = View.Columns("IdProductoBodega")
                Dim vCodigoBarra As String = IIf(IsDBNull(lista.Text), "", lista.Text)

                If lista.EditValue Is Nothing AndAlso vCodigoBarra.Trim = "" Then Return

                Dim drLineaGrid As DataRow = gvDetalleDocIngreso.GetFocusedDataRow()
                If drLineaGrid Is Nothing AndAlso Not View.IsNewItemRow(View.FocusedRowHandle) Then Return

                Dim codigoIngresadoString As String = ""

                If lista.EditValue Is Nothing OrElse String.IsNullOrEmpty(lista.EditValue.ToString()) Then
                    codigoIngresadoString = vCodigoBarra.Trim()
                Else
                    codigoIngresadoString = lista.EditValue
                End If

                If String.IsNullOrEmpty(codigoIngresadoString) Then
                    ' El código ingresado es nulo o vacío
                    If Not View Is Nothing AndAlso Not ColProducto Is Nothing Then
                        'Marca celda de código de producto con error.
                        View.SetColumnError(ColProducto, "Código no válido.")
                    End If
                Else

                    Dim vIdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_Codigo(codigoIngresadoString, AP.IdBodega)

                    If Not vIdProductoBodega = 0 Then

                        lista.EditValue = vIdProductoBodega

                        Dim NoLinea As Integer = IIf(IsDBNull(View.GetRowCellValue(View.FocusedRowHandle, "NoLinea")), 0, View.GetRowCellValue(View.FocusedRowHandle, "NoLinea"))

                        If NoLinea = 0 Then
                            If lOCDetLn.Count = 0 Then
                                NoLinea += 1
                            Else
                                NoLinea = lOCDetLn.Max(Function(x) x.No_Linea) + 1
                            End If
                            View.SetRowCellValue(View.FocusedRowHandle, "NoLinea", NoLinea)
                        End If

                        DgridDetalleOC.BeginInvoke(New MethodInvoker(Sub()
                                                                         gvDetalleDocIngreso.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                         gvDetalleDocIngreso.FocusedColumn = gvDetalleDocIngreso.Columns("cantidad")
                                                                         gvDetalleDocIngreso.ShowEditor()
                                                                     End Sub))

                    Else
                        If Not View Is Nothing AndAlso Not ColProducto Is Nothing Then
                            'Marca celda de código de producto con error.
                            View.SetColumnError(ColProducto, "Código no válido.")
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleRec2_KeyDown(sender As Object, e As KeyEventArgs) Handles gvDetalleDocIngreso.KeyDown

        If e.KeyCode = Keys.Escape Then
            If gvDetalleDocIngreso.IsNewItemRow(gvDetalleDocIngreso.FocusedRowHandle) Then
                If XtraMessageBox.Show("¿Cancelar la edición de la fila?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Grid_Tiene_Error = False
                    gvDetalleDocIngreso.CancelUpdateCurrentRow()
                End If
            End If
        End If

    End Sub

    Private Sub cmbAcuerdoComercial_EditValueChanged(sender As Object, e As EventArgs) Handles cmbAcuerdoComercial.EditValueChanged

        Try

            If cmbAcuerdoComercial.EditValue > 0 Then

                Select Case Modo

                    Case ModoTrans.Nuevo
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, AP.Bodega.IdBodega)
                    Case ModoTrans.Editar
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, gBeOrdenCompra.IdBodega)

                End Select

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Grid_Servicios_Tiene_Error As Boolean = False

    Private Sub gvDetalleServicios_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvDetalleServicios.ValidateRow

        Dim clsTransaccion As New clsTransaccion

        Try
            Dim servicio As New clsBeTrans_oc_servicios()
            Dim acuerdoDet As New clsBeTrans_acuerdoscomerciales_det()
            Dim gMaxIdOrdenCompraServicio As Integer = 0

            Dim View As GridView = CType(sender, GridView)
            Dim ColCantidad As GridColumn = View.Columns("Cantidad")
            Dim ColServicio As GridColumn = View.Columns("servicio")
            Dim ColCodigoProudcto As GridColumn = View.Columns("codigo_producto")

            Dim pServicio As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "servicio")), "", View.GetRowCellValue(e.RowHandle, "servicio"))
            Dim pCantidad As Double = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "cantidad")), 0, View.GetRowCellValue(e.RowHandle, "cantidad"))
            Dim pIdAcuerdoDet As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdAcuerdoDet")), 0, View.GetRowCellValue(e.RowHandle, "IdAcuerdoDet"))
            Dim pCorrelativo = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "correlativo_detalleacuerdo")), 0, View.GetRowCellValue(e.RowHandle, "correlativo_detalleacuerdo"))
            Dim pIdOrdenCompraServicio As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenCompraServicio")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenCompraServicio"))
            Dim pFechaServicio As Date = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "FechaServicio")), Now, View.GetRowCellValue(e.RowHandle, "FechaServicio"))
            Dim vIsNewRow As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IsNewR")), False, View.GetRowCellValue(e.RowHandle, "IsNewR"))

            Dim Etapa_Uno_Correcta As Boolean = False
            Dim Etapa_Dos_Correcta As Boolean = False
            Dim vExisteRegla As Boolean = False

            '#EJC20210313: Es culpable hasta que se demuestre lo contrario.
            Grid_Servicios_Tiene_Error = True
            Dim isValidCantidad As Boolean = True
            Dim IsValidAcuerdoDet As Boolean = True
            Dim isValidCodigoProducto As Boolean = True
            Dim isValidCorrelativo As Boolean = True

            '#GT29052024: guardamos cada registro inmediatamente.
            clsTransaccion.Begin_Transaction()

            If pCorrelativo > 0 AndAlso pIdAcuerdoDet > 0 Then

                servicio = New clsBeTrans_oc_servicios

                If pServicio = "" Then
                    IsValidAcuerdoDet = False
                    View.SetColumnError(ColServicio, "Seleccione un servicio.")
                End If

                If pCantidad = 0 Then
                    isValidCantidad = False
                    View.SetColumnError(ColCantidad, "Ingrese cantidad > 0")
                Else
                    servicio.Cantidad = pCantidad
                End If

                e.Valid = isValidCantidad AndAlso isValidCorrelativo AndAlso IsValidAcuerdoDet
                Etapa_Uno_Correcta = e.Valid

            End If


            If Etapa_Uno_Correcta Then

                If gBeOrdenCompra.IdOrdenCompraEnc > 0 Then

                    '#GT29052024: si es linea nueva, se agrega a la lista.
                    If vIsNewRow Then
                        acuerdoDet = New clsBeTrans_acuerdoscomerciales_det
                        acuerdoDet.Correlativo_detalleacuerdo = pCorrelativo
                        clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(acuerdoDet, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        servicio.IsNew = vIsNewRow
                        servicio.IdOrdenCompraServicio = clsLnTrans_oc_servicios.MaxID(clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction) + 1

                        servicio.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                        servicio.IdAcuerdo = acuerdoDet.IdAcuerdoEnc
                        servicio.IdAcuerdoDet = acuerdoDet.IdAcuerdoDet
                        servicio.Codigo_producto = acuerdoDet.Codigo_producto
                        servicio.Nombre_servicio = acuerdoDet.Servicio
                        servicio.Corre_detalleacuerdo = acuerdoDet.Correlativo_detalleacuerdo
                        servicio.Corre_catalogoproductos = 0
                        'servicio.Cantidad   = pCantidad 'ya se asigno en la primera fase
                        servicio.User_agr = AP.UsuarioAp.IdUsuario
                        servicio.User_mod = AP.UsuarioAp.IdUsuario
                        servicio.Fec_mod = Now
                        servicio.IdPropietarioBodega = 0
                        servicio.Fecha_Servicio = pFechaServicio

                        For indice As Integer = 0 To gvDetalleServicios.RowCount - 1

                            Dim pServicioExiste = gvDetalleServicios.GetRowCellValue(indice, "servicio")
                            Dim pCorrelativoExiste = CInt(gvDetalleServicios.GetRowCellValue(indice, "correlativo_detalleacuerdo"))
                            Dim pFecha = CDate(gvDetalleServicios.GetRowCellValue(indice, "FechaServicio"))

                            '#GT21022025: si existe un servicio igual, pero con fecha distinta es permitido
                            If pServicioExiste = servicio.Nombre_servicio AndAlso pCorrelativoExiste = servicio.Corre_detalleacuerdo AndAlso servicio.Fecha_Servicio.Date = pFecha.Date Then
                                vExisteRegla = True
                                Exit For
                            End If

                        Next

                        '#GT14082024: antes de consultar datos, validamos que sea nuevo servicio y no uno existente.
                        If vExisteRegla Then
                            View.SetColumnError(ColServicio, "No se puede registrar el mismo servicio mas de una vez.")
                        Else

                            '#GT14082024: asociar el acuerdo con el doc de ingreso.
                            If gBeOrdenCompra.IdAcuerdoComercial = 0 Then
                                gBeOrdenCompra.IdAcuerdoComercial = acuerdoDet.IdAcuerdoEnc
                                gBeOrdenCompra.User_Mod = AP.UsuarioAp.IdUsuario
                                gBeOrdenCompra.Fec_Mod = Now
                                clsLnTrans_oc_enc.Actualizar_AcuerdoComercial_By_IdOrdenCompraEnc(gBeOrdenCompra,
                                                                                                  clsTransaccion.lConnection,
                                                                                                  clsTransaccion.lTransaction)
                            End If

                            '#GT29052024: guardar inmediatamente el servicio.
                            clsLnTrans_oc_servicios.Insertar(servicio,
                                                             clsTransaccion.lConnection,
                                                             clsTransaccion.lTransaction)
                        End If

                    Else

                        '#GT14112024: si servicio ya esta asociado a una prefactura, no permitir cambios
                        acuerdoDet = New clsBeTrans_acuerdoscomerciales_det
                        acuerdoDet.Correlativo_detalleacuerdo = pCorrelativo
                        clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(acuerdoDet, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        Dim pAcuerdoComercialDet = New clsBeTrans_acuerdoscomerciales_det
                        pAcuerdoComercialDet.IdAcuerdoEnc = acuerdoDet.IdAcuerdoEnc
                        pAcuerdoComercialDet.IdAcuerdoDet = acuerdoDet.IdAcuerdoDet

                        If clsLnTrans_prefactura_enc.Exist_By_IdOrdenCompraEnc_and_ServicioDet(gBeOrdenCompra.IdOrdenCompraEnc, pAcuerdoComercialDet,
                                                                                                                                clsTransaccion.lConnection,
                                                                                                                                clsTransaccion.lTransaction) Then
                            Throw New Exception("Servicio asociado a una prefactura, no se puede eliminar o modificar.")
                        End If


                        If clsLnTrans_prefactura_enc.Exist_By_IdOrdenCompraEnc_and_Movimiento(gBeOrdenCompra.IdOrdenCompraEnc, pAcuerdoComercialDet,
                                                                                                                               clsTransaccion.lConnection,
                                                                                                                               clsTransaccion.lTransaction) Then
                            Throw New Exception("Servicio asociado a una prefactura, no se puede eliminar o modificar.")
                        End If


                        '#GT: actualizar cantidad
                        servicio.IdOrdenCompraServicio = pIdOrdenCompraServicio
                        servicio.IdOrdenCompraEnc = gBeOrdenCompra.IdOrdenCompraEnc
                        servicio.User_mod = AP.UsuarioAp.IdUsuario
                        servicio.Fec_mod = Now
                        servicio.Fecha_Servicio = pFechaServicio
                        clsLnTrans_oc_servicios.Actualizar_Servicio_By_IdServicio(servicio, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    End If


                    e.Valid = (servicio.IdOrdenCompraServicio > 0) AndAlso Not vExisteRegla


                    If e.Valid Then
                        If servicio.IdOrdenCompraServicio > 0 Then
                            View.SetRowCellValue(e.RowHandle, "IdOrdenCompraServicio", servicio.IdOrdenCompraServicio)
                            View.SetRowCellValue(e.RowHandle, "IsNewR", False)
                        End If


                    Else
                        Grid_Servicios_Tiene_Error = True
                    End If

                End If

            End If

            clsTransaccion.Commit_Transaction()
            clsTransaccion.Close_Conection()


            If Etapa_Uno_Correcta And Not Grid_Servicios_Tiene_Error Then
                dgridServiciosAsociados.BeginInvoke(New MethodInvoker(Sub()
                                                                          gvDetalleServicios.FocusedRowHandle = GridControl.NewItemRowHandle
                                                                          gvDetalleServicios.FocusedColumn = ColServicio
                                                                          'gvDetalleServicios.MakeColumnVisible(ColServicio)
                                                                          gvDetalleServicios.ActiveFilter.Clear()
                                                                          If gvDetalleServicios.FocusedColumn IsNot Nothing Then
                                                                              gvDetalleServicios.ClearColumnsFilter()
                                                                              gvDetalleServicios.ShowEditor()
                                                                          End If
                                                                      End Sub))


                Application.DoEvents()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub gvDetalleServicios_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles gvDetalleServicios.InvalidRowException

        Try

            '#EJC20210307: Evita que salte mensaje indicando si se quiere corregir la fila.
            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdEliminarServicio_Click(sender As Object, e As EventArgs) Handles cmdEliminarServicio.Click
        Try

            Dim currentView As GridView = dgridServiciosAsociados.FocusedView
            Dim pAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det

            '#GT11102022_1500: validar que es una fila con datos
            If currentView IsNot Nothing AndAlso currentView.SelectedRowsCount = 1 Then

                Dim IdOrdenCompraServicio As Integer = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdOrdenCompraServicio")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "IdOrdenCompraServicio"))
                Dim vCorrelativo_DetalleAcuerdo As Integer = IIf(IsDBNull(currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "correlativo_detalleacuerdo")), 0, currentView.GetRowCellDisplayText(currentView.FocusedRowHandle, "correlativo_detalleacuerdo"))
                pAcuerdoDet.Correlativo_detalleacuerdo = vCorrelativo_DetalleAcuerdo

                clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(pAcuerdoDet)

                If clsLnTrans_prefactura_enc.Exist_By_IdOrdenCompraEnc_and_ServicioDet(gBeOrdenCompra.IdOrdenCompraEnc, pAcuerdoDet) Then
                    Throw New Exception("Servicio asociado a una prefactura, no se puede eliminar o modificar.")
                End If


                If clsLnTrans_prefactura_enc.Exist_By_IdOrdenCompraEnc_and_Movimiento(gBeOrdenCompra.IdOrdenCompraEnc, pAcuerdoDet) Then
                    Throw New Exception("Servicio asociado a una prefactura, no se puede eliminar o modificar.")
                End If


                If Not IdOrdenCompraServicio = 0 Then
                    If XtraMessageBox.Show(String.Format("¿Eliminar el servicio: ""{0}""?", pAcuerdoDet.Servicio) _
                                          , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                        clsLnTrans_oc_servicios.Eliminar_By_IdOrdenCompraServicio(IdOrdenCompraServicio)
                        currentView.DeleteRow(currentView.FocusedRowHandle)

                        XtraMessageBox.Show("Servicio eliminado!")

                    End If
                Else
                    currentView.DeleteRow(currentView.FocusedRowHandle)
                End If


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dgridServiciosAsociados_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles dgridServiciosAsociados.ProcessGridKey


        Dim pAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det

        Dim view As ColumnView = TryCast(TryCast(sender, GridControl).FocusedView, ColumnView)
        If view Is Nothing Then
            Return
        End If
        If e.KeyCode = Keys.Delete AndAlso view.Editable AndAlso view.SelectedRowsCount > 0 Then
            'Prevent record deletion when an in-place editor is invoked:
            If view.ActiveEditor IsNot Nothing Then
                Return
            End If
            e.Handled = True

            'Dim IdServicio As Integer = IIf(IsDBNull(view.GetRowCellValue(view.FocusedRowHandle, "IdAcuerdoDet")), 0, view.GetRowCellValue(view.FocusedRowHandle, "IdAcuerdoDet"))
            Dim IdOrdenCompraServicio As Integer = IIf(IsDBNull(view.GetRowCellValue(view.FocusedRowHandle, "IdOrdenCompraServicio")), 0, view.GetRowCellValue(view.FocusedRowHandle, "IdOrdenCompraServicio"))
            Dim vCorrelativo_DetalleAcuerdo As Integer = IIf(IsDBNull(view.GetRowCellValue(view.FocusedRowHandle, "correlativo_detalleacuerdo")), 0, view.GetRowCellValue(view.FocusedRowHandle, "correlativo_detalleacuerdo"))

            pAcuerdoDet.Correlativo_detalleacuerdo = vCorrelativo_DetalleAcuerdo

            clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(pAcuerdoDet)


            If Not IdOrdenCompraServicio = 0 Then

                If XtraMessageBox.Show(String.Format("¿Eliminar el servicio: ""{0}""?", pAcuerdoDet.Servicio) _
                                        , Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    clsLnTrans_oc_servicios.Eliminar_By_IdOrdenCompraServicio(IdOrdenCompraServicio)
                    view.DeleteSelectedRows()

                    XtraMessageBox.Show("Servicio eliminado!")

                End If
            Else
                view.DeleteSelectedRows()
            End If

        End If

    End Sub

    Private Sub Cargar_Talla_Color(ByVal IdCampaña As Integer)

        Try

            GridView7.Columns.Clear()

            dgridTallaColor.DataSource = Nothing
            dgridTallaColor.RefreshDataSource()

            SplashScreenManager.CloseForm(False)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando...")

            Dim Dt As New DataTable
            Dt = clsLnProducto_talla_color.Get_All_Dt_By_IdCampaña_And_IdOrdenCompraEnc(IdCampaña, lblC.Text)

            dgridTallaColor.DataSource = Dt

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Talla_Color_Con_Imagen(IdCampaña As Integer, vRutaCDN As String)

        Try

            GridView7.Columns.Clear()

            dgridTallaColor.DataSource = Nothing
            dgridTallaColor.RefreshDataSource()

            SplashScreenManager.CloseForm(False)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando...")

            Dim dt As New DataTable
            dt = clsLnProducto_talla_color.Get_All_Dt_By_IdCampaña_And_IdOrdenCompraEnc(IdCampaña, lblC.Text)
            ' Agregar columna de imagen
            If Not dt.Columns.Contains("Imagen") Then
                dt.Columns.Add("Imagen", GetType(Image))
            End If
            If Not dt.Columns.Contains("RutaImagen") Then
                dt.Columns.Add("RutaImagen", GetType(String))
            End If
            ' Mostrar datos primero, luego cargar imágenes en background
            dgridTallaColor.DataSource = dt
            dgridTallaColor.RefreshDataSource()

            For Each row As DataRow In dt.Rows
                Dim codigoSKU As String = row("CodigoSKU").ToString()
                Dim productoBase As String = codigoSKU
                Dim talla As String = ""
                Dim color As String = ""

                If codigoSKU.Length >= 13 Then
                    productoBase = codigoSKU.Substring(0, 10)
                    talla = codigoSKU.Substring(10, 3)
                    If codigoSKU.Length > 13 Then
                        color = codigoSKU.Substring(13)
                    End If
                ElseIf codigoSKU.Length >= 10 Then
                    productoBase = codigoSKU.Substring(0, 10)
                End If

                Dim patrones As New List(Of String)

                If talla <> "" AndAlso color <> "" Then
                    patrones.Add("._" & productoBase & "-" & talla & "-" & color & "*.png")
                    patrones.Add(productoBase & "-" & talla & "-" & color & "*.png")
                End If

                If talla <> "" Then
                    patrones.Add("._" & productoBase & "-" & talla & "*.png")
                    patrones.Add(productoBase & "-" & talla & "*.png")
                End If

                patrones.Add("._" & productoBase & "*.png")
                patrones.Add(productoBase & "*.png")

                Dim archivoEncontrado As String = Nothing
                For Each patron In patrones
                    Dim archivos() As String = Directory.GetFiles(vRutaCDN, patron)
                    If archivos.Length > 0 Then
                        archivoEncontrado = archivos(0)
                        Exit For
                    End If
                Next

                Try
                    If Not String.IsNullOrEmpty(archivoEncontrado) Then
                        Using fs As New FileStream(archivoEncontrado, FileMode.Open, FileAccess.Read)
                            Dim img As Image = Image.FromStream(fs)
                            row("Imagen") = CType(img.Clone(), Image)
                            row("RutaImagen") = archivoEncontrado ' Para doble clic
                        End Using
                    Else
                        row("Imagen") = Nothing
                        row("RutaImagen") = Nothing
                    End If
                Catch ex As Exception
                    row("Imagen") = Nothing
                    row("RutaImagen") = Nothing
                End Try
            Next

            dgridTallaColor.RefreshDataSource()
            SplashScreenManager.CloseForm(False)

            Application.DoEvents()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgridTallaColor_DoubleClick(sender As Object, e As EventArgs) Handles dgridTallaColor.DoubleClick
        Dim view As ColumnView = CType(dgridTallaColor.FocusedView, ColumnView)
        Dim rowHandle As Integer = view.FocusedRowHandle
        If rowHandle < 0 Then Exit Sub

        Dim rutaImagen As Object = view.GetRowCellValue(rowHandle, "RutaImagen")
        If rutaImagen IsNot Nothing AndAlso File.Exists(rutaImagen.ToString()) Then
            Dim previewForm As New DevExpress.XtraEditors.XtraForm()
            previewForm.Text = "Vista previa de imagen"
            previewForm.StartPosition = FormStartPosition.CenterParent
            previewForm.Size = New Size(700, 700)

            Dim pictureEdit As New DevExpress.XtraEditors.PictureEdit()
            pictureEdit.Dock = DockStyle.Fill
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
            pictureEdit.Image = Image.FromFile(rutaImagen.ToString())

            previewForm.Controls.Add(pictureEdit)
            previewForm.ShowDialog()
        End If
    End Sub

    Private Sub cmdEliminarDocumento_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminarDocumento.ItemClick
        Try

            If clsLnTrans_re_oc.Tiene_Recepciones(gBeOrdenCompra.IdOrdenCompraEnc) Then

                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se puede eliminar, recepción asociada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                If gBeOrdenCompra.IdEstadoOC = 1 Then

                    SplashScreenManager.CloseForm(False)

                    If XtraMessageBox.Show("¿Eliminar documento de ingreso:" & gBeOrdenCompra.Referencia & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                        If clsLnTrans_oc_enc.Eliminar_OC(gBeOrdenCompra, AP.UsuarioAp) Then

                            '#MECR03102025: Se agrego nueva bitacora de logs para OC
                            Dim vMsgAdvertencia As String = "ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " eliminó la Orden Compra Enc: " & gBeOrdenCompra.IdOrdenCompraEnc
                            'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231700: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " eliminó la Orden Compra Enc: " & gBeOrdenCompra.IdOrdenCompraEnc)
                            clsLnLog_error_wms_oc.Agregar_Error(vMsgAdvertencia, AP.IdEmpresa, AP.IdBodega, AP.UsuarioAp.IdUsuario, pIdOCEnc:=gBeOrdenCompra.IdOrdenCompraEnc)

                            SplashScreenManager.CloseForm(False)

                            XtraMessageBox.Show("Documento de ingreso eliminado correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Close()

                        Else
                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("No se pudo eliminar el documento de ingreso.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

        End Try
    End Sub

    Private Sub tabTallaColor_VisibleChanged(sender As Object, e As EventArgs) Handles tabTallaColor.VisibleChanged
        Try

            If tabTallaColor.Visible Then
                Dim vRutaCDN As String = clsLnBodega.GetRutaCDN_By_Idbodega(AP.IdBodega)

                If Not String.IsNullOrEmpty(vRutaCDN) Then
                    If MessageBox.Show(String.Format("¿Mostrar imágenes de los productos?", Me.Text), "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Cargar_Talla_Color_Con_Imagen(gBeOrdenCompra.IdCampaña, vRutaCDN)
                    Else
                        Cargar_Talla_Color(gBeOrdenCompra.IdCampaña)
                    End If
                Else
                    Cargar_Talla_Color(gBeOrdenCompra.IdCampaña)
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdPreImpresionOC_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPreImpresionOC.ItemClick
        Try
            With frmImpresionRecepcion_OC
                .pTransOC_Enc = gBeOrdenCompra
                .Show()
                .Focus()
            End With
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdPreImpresionRFID_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPreImpresionRFID.ItemClick
        Try
            With frmImpresion_OC_RFID
                .pTransOC_Enc = gBeOrdenCompra
                .Show()
                .Focus()
            End With
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

End Class




