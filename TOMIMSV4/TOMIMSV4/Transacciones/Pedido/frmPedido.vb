Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class frmPedido

    Public pBePedidoEnc As New clsBeTrans_pe_enc
    Private pBeCliente As clsBeCliente
    Private pBeProducto As New clsBeProducto
    Private pBeStock As New clsBeStock
    Private pBeStockRes As New clsBeStock_res
    Private pClienteTiemposList As New List(Of clsBeCliente_tiempos)
    Private pBePedidoDet As New clsBeTrans_pe_det
    Private pBePedidoDetList As New List(Of clsBeTrans_pe_det)
    Private Propietario As New clsBePropietarios
    Public listarPicking As New List(Of clsBeTrans_picking_enc)
    Private EsKit As Boolean = False
    Private PedidoGuardadoPorUsuario As Boolean = False
    Public Delegate Sub ListarPedidos()
    Public Property InvokeListarPedidos As ListarPedidos
    Private DTStockRes As New DataTable("StockRes")
    Private Property Preguntar_Al_Salir As Boolean = True

    Dim IdxLineaEditando As Integer = -1

    Private UnaExtrañaCondicionDeEdicionEnLInea As Boolean = False

    Private BeListProductoKit As New List(Of clsBeProducto_kit_composicion)
    Private pBeListStockRes As New List(Of clsBeStock_res)

    Private mgr As New SplashScreenManager(Me, GetType(WaitForm), True, True, False)

    'GT 01022021 variable global para set checkbox y validar si aplica control poliza
    Public vControlPoliza As Boolean

    Private BeBodega As New clsBeBodega()
    'Private BeConfigBodega As New clsBeI_nav_config_enc

    Private BeConfigBodega As New clsBeI_nav_config_enc()

    '#CKFK20220325 Agregué estas dos variables para cuando el cliente se maneje en el detalle del pedido
    Private Cliente_Detalle_Ultimo_Lote As Integer
    Private Cliente_Detalle_Control_Calidad As Integer

    Private txtCodigoProductoGrid As TextBox
    Private LastEventHandlerPres As EventHandler = AddressOf cmbPresentacion_SelectedIndexChanged
    Private LastEventHandlerEstado As EventHandler = AddressOf cmbEstado_SelectedIndexChanged

    Dim DgComboPresentacion As New DataGridViewComboBoxCell()
    Dim DgComboEstado As New DataGridViewComboBoxCell()
    Dim ContadorFocus As Integer = 0
    Dim DgComboCliente As New DataGridViewComboBoxCell()

    'Instancias de las celdas del grid
    Dim NoLineaCell As DataGridViewCell
    Dim IdProductoCell As DataGridViewCell
    Dim IdProductoBodegaCell As DataGridViewCell
    Dim CodProductoCell As DataGridViewCell
    Dim NomProductoCell As DataGridViewCell
    Dim CantidadCell As DataGridViewCell
    Dim PesoCell As DataGridViewCell
    Dim PrecioCell As DataGridViewCell
    Dim TotalCell As DataGridViewCell
    Dim CantDisp As DataGridViewCell
    Dim EstadoCell As DataGridViewCell
    Dim PresentacionCell As DataGridViewCell
    Dim UnidadMedidaCell As DataGridViewCell
    Dim IdPedidoDetCell As DataGridViewCell
    Dim NoDiasVencimientoCell As DataGridViewCell
    Dim SerieProductoCell As DataGridViewCell
    Dim ClienteCell As DataGridViewCell

    'Valor obtenido de las celdas del grid
    Dim vNoLinea As String = ""
    Dim vNomPresentacion As String = ""
    Dim vCodigoProducto As String = ""
    Dim vNomProducto As String = ""
    Dim vCantidad As Double = 0
    Dim vPeso As Double = 0
    Dim vPrecio As Double = 0
    Dim vTotal As Double = 0
    Dim vCantidadDisponible As Double = 0
    Dim vIdPresentacion As Integer = 0
    Dim vIdProductoBodega As Integer = 0
    Dim vIdEstado As Integer = 0
    Dim vNomUnidadMedida As String = ""
    Dim vNomEstado As String = ""
    Dim vIdPedidoDet As Integer = 0
    Dim vIdPedidoDetPadre As Integer = 0
    Dim vNoDiasVencimiento As Integer = 0
    Dim vIdProducto As Integer = 0
    Dim vNoSerie As String = ""
    Private IsGettingValoresGrid As Boolean = False

    Private frmSelStock As frmStock_Especifico_List
    Public pListBeTrans_ubic_hh_det As New List(Of clsBeTrans_ubic_hh_det)
    Private pObjVWStock As New clsBeVW_stock_res
    Private DTProductoKitComposicion As New DataTable("ProductoKitComposicion")
    Private DTGridDetalleServicios As New DataTable("DetalleServicios")
    Private ServicioGridLookUpEdit As New RepositoryItemGridLookUpEdit
    Private txtCantidadGrid As New RepositoryItemSpinEdit
    Private BeTipoDoc As New clsBeTrans_pe_tipo()

    Private lBeTransPickImagen As New List(Of clsBeTrans_picking_img)

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Delegate Sub Cargar_Datos_Pedido()
    ReadOnly Property InvokeCargarPedido As Cargar_Datos_Pedido()

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub Set_Columnas_DT_StockRes()
        DTStockRes.Columns.Add("IdStockRes", GetType(Integer))
        DTStockRes.Columns.Add("IdStock", GetType(Integer))
        DTStockRes.Columns.Add("Código", GetType(String))
        DTStockRes.Columns.Add("Nombre", GetType(String))
        DTStockRes.Columns.Add("Estado", GetType(String))
        DTStockRes.Columns.Add("Lote", GetType(String))
        DTStockRes.Columns.Add("Licencia", GetType(String))
        DTStockRes.Columns.Add("Vence", GetType(Date))
        DTStockRes.Columns.Add("Cantidad_UMBas", GetType(Double))
        DTStockRes.Columns.Add("UMBas", GetType(String))
        DTStockRes.Columns.Add("Cantidad_Pres", GetType(Double))
        DTStockRes.Columns.Add("Presentación", GetType(String))
        DTStockRes.Columns.Add("Peso", GetType(Double))
        DTStockRes.Columns.Add("Ubicacion", GetType(String))
        DTStockRes.Columns.Add("Host", GetType(String))
        DTStockRes.Columns.Add("Linea", GetType(String))
        DTStockRes.Columns.Add("Referencia", GetType(String))
    End Sub

    Private Sub frmPedido_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try

            If Not PedidoTieneDetalle() AndAlso pBePedidoEnc.Estado.ToUpper = "NUEVO" Then

                EliminaEncabezadoPedidoSinDetalle()

                If Not InvokeListarPedidos Is Nothing Then
                    InvokeListarPedidos.Invoke()
                End If

            Else

                If Not PedidoGuardadoPorUsuario Then

                    If Preguntar_Al_Salir Then

                        If XtraMessageBox.Show(String.Format("El pedido tiene: {0} líneas de detalle, ¿salir sin guardar?",
                                                             dgrid.RowCount - 1),
                                                             Text,
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                            If (dgrid.RowCount - 1) = 0 Then

                                If clsLnTrans_pe_enc.Existe_Documento_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc) Then

                                    If XtraMessageBox.Show(String.Format("¿Eliminar el pedido?", dgrid.RowCount - 1), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                        If Elimina_Pedido_Con_Detalle() Then
                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            If Not InvokeListarPedidos Is Nothing Then
                                                InvokeListarPedidos.Invoke()
                                            End If
                                            e.Cancel = False
                                        End If
                                    End If

                                End If

                            ElseIf (dgrid.RowCount - 1) > 0 Then
                                If XtraMessageBox.Show(String.Format("¿Eliminar el pedido?", dgrid.RowCount - 1), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    If Elimina_Pedido_Con_Detalle() Then
                                        XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        If Not InvokeListarPedidos Is Nothing Then
                                            InvokeListarPedidos.Invoke
                                        End If
                                        e.Cancel = False
                                    End If
                                End If
                            End If
                        Else
                            e.Cancel = True
                        End If

                    End If

                End If

            End If

            If Not InvokeListarPedidos Is Nothing Then
                InvokeListarPedidos.Invoke
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Carga_Datos_PedidoERP(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Carga_Datos_PedidoERP = False

        Try

            grdPedTras.DataSource = Nothing

            Dim DT As New DataTable

            If pBePedidoEnc Is Nothing Then Exit Function

            DT = clsLnI_nav_ped_traslado_enc.Get_Detalle_Pedido_Traslado_By_Referencia(pBePedidoEnc.Referencia,
                                                                                       lConnection,
                                                                                       lTransaction)

            If DT.Rows.Count > 0 Then

                grdPedTras.DataSource = DT

                GridView8.OptionsView.ColumnAutoWidth = False
                GridView8.BestFitColumns(True)

                GridView8.OptionsView.ShowFooter = True

                GridView8.Columns("No").SummaryItem.SummaryType = SummaryItemType.Count
                GridView8.Columns("No").SummaryItem.DisplayFormat = "Registros: {0}"

                GridView8.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
                GridView8.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView8.Columns("Cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView8.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView8.Columns("Cantidad_Reservada").DisplayFormat.FormatType = FormatType.Numeric
                GridView8.Columns("Cantidad_Reservada").DisplayFormat.FormatString = "{0:n6}"
                GridView8.Columns("Cantidad_Reservada").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView8.Columns("Cantidad_Reservada").SummaryItem.DisplayFormat = "{0:n6}"

                Carga_Datos_PedidoERP = True

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Carga_Datos_PedidoERP() As Boolean

        Carga_Datos_PedidoERP = False

        Try

            grdPedTras.DataSource = Nothing

            Dim DT As New DataTable

            DT = clsLnI_nav_ped_traslado_enc.Get_Detalle_Pedido_Traslado_By_Referencia(pBePedidoEnc.Referencia)

            If DT.Rows.Count > 0 Then

                grdPedTras.DataSource = DT

                GridView8.OptionsView.ColumnAutoWidth = False
                GridView8.BestFitColumns(True)

                GridView8.OptionsView.ShowFooter = True

                GridView8.Columns("No").SummaryItem.SummaryType = SummaryItemType.Count
                GridView8.Columns("No").SummaryItem.DisplayFormat = "Registros: {0}"

                GridView8.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
                GridView8.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView8.Columns("Cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView8.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                Carga_Datos_PedidoERP = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Inserta_Encabezado(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Inserta_Encabezado = False

        Try

            pBePedidoEnc = New clsBeTrans_pe_enc()
            pBePedidoEnc.IdBodega = AP.IdBodega
            pBePedidoEnc.Fec_agr = Now
            pBePedidoEnc.Fec_mod = Now
            pBePedidoEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBePedidoEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBePedidoEnc.Fecha_Pedido = Now
            pBePedidoEnc.Hora_ini = New Date(1900, 1, 1, 0, 0, 0)
            pBePedidoEnc.Hora_fin = New Date(1900, 1, 1, 0, 0, 0)
            pBePedidoEnc.Fecha_Preparacion = New Date(1900, 1, 1)
            pBePedidoEnc.RoadFechaEntr = New Date(1900, 1, 1)
            pBePedidoEnc.Ubicacion = "TMP"
            pBePedidoEnc.Estado = "NUEVO"
            pBePedidoEnc.RoadDirEntrega = ""
            pBePedidoEnc.RoadBandera = ""
            pBePedidoEnc.RoadStatCom = ""
            pBePedidoEnc.RoadCalcoBJ = ""
            pBePedidoEnc.RoadADD1 = ""
            pBePedidoEnc.RoadADD2 = ""
            pBePedidoEnc.RoadADD3 = ""
            pBePedidoEnc.RoadStatProc = ""
            pBePedidoEnc.RoadSucursal = ""
            pBePedidoEnc.RoadTotal = 0
            pBePedidoEnc.RoadRazon_Rechazado = ""
            pBePedidoEnc.HoraEntregaDesde = Now
            pBePedidoEnc.HoraEntregaHasta = Now
            pBePedidoEnc.Observacion = clsPublic.Quitar_Caracteres_No_Permitidos(txtObservacion.Text.Trim)
            pBePedidoEnc.Enviado_A_ERP = False
            pBePedidoEnc.Activo = True
            '#GT11082023: Se carga vacia porque es el insert del encabezado sin detalle de nada.
            pBePedidoEnc.ObjPoliza = Nothing

            clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc,
                                                 lConnection,
                                                 lTransaction)

            Inserta_Encabezado = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Set_Estado_Envio_A_ERP()

        Try

            If Not pBePedidoEnc Is Nothing Then
                If pBePedidoEnc.Enviado_A_ERP Then
                    mnuEstadoEnviadoAERP.Caption = "Enviado"
                    mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.green_ball
                Else
                    mnuEstadoEnviadoAERP.Caption = "No Enviado"
                    mnuEstadoEnviadoAERP.LargeGlyph = My.Resources.red_ball
                End If
            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Function Elimina_Pedido_Con_Detalle() As Boolean

        Elimina_Pedido_Con_Detalle = False

        Try

            If clsLnTrans_pe_enc.Existe_Documento_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc) Then

                Elimina_Pedido_Con_Detalle = clsLnTrans_pe_enc.Eliminar_Pedido(pBePedidoEnc.IdPedidoEnc)

                '#EJC20220718:Dejar log de eliminación de pedido.
                Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(pBePedidoEnc.IdBodega)

                'clsLnLog_error_wms.Agregar_Error(vIdEmpresa, pBePedidoEnc.IdBodega, "PED_DEL: Se eliminó el IdPedido con det: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia)
                Dim vMsgDelete As String = "PED_DEL: Se eliminó el IdPedido con det: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia
                clsLnLog_error_wms_pe.Agregar_Error(vMsgDelete,
                                                    pIdEmpresa:=AP.IdEmpresa,
                                                    pIdBodega:=AP.IdBodega,
                                                    pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                    pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

            End If

            If Elimina_Pedido_Con_Detalle = 0 Then
                Throw New Exception("ERROR_20220616_1506: No se pudo eliminar el pedido en su totalidad, probablemente deba liberar el stock manualmente, esto muy probablmente se debe a un objeto VW_Get_Single_Pedido que no está actualizado que permite liberar el stock de pedidos que se han creado de forma incorrecta, con cariño EJC. ")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function EliminaEncabezadoPedidoSinDetalle() As Boolean

        EliminaEncabezadoPedidoSinDetalle = False

        Try

            EliminaEncabezadoPedidoSinDetalle = (clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc) > 0)

            '#EJC20220718:Dejar log de eliminación de pedido.
            Dim vIdEmpresa As Integer = clsLnBodega.Get_IdEmpresa_By_IdBodega(pBePedidoEnc.IdBodega)

            'clsLnLog_error_wms.Agregar_Error(vIdEmpresa, pBePedidoEnc.IdBodega, "PED_DEL: Se eliminó sin det, el IdPedido: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia)
            Dim msgEliminacion As String = "PED_DEL: Se eliminó sin det, el IdPedido: " & pBePedidoEnc.IdPedidoEnc & " con referencia: " & pBePedidoEnc.Referencia
            clsLnLog_error_wms_pe.Agregar_Error(msgEliminacion,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function PedidoTieneDetalle() As Boolean

        PedidoTieneDetalle = False

        Try

            If Not pBePedidoEnc Is Nothing Then
                PedidoTieneDetalle = clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Cargar_Datos(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If Not pBePedidoEnc Is Nothing Then

                pBePedidoEnc.IsNew = False

                lblIdPedidoEnc.Text = pBePedidoEnc.IdPedidoEnc

                cmbBodega.EditValue = pBePedidoEnc.IdBodega
                cmbBodega.Enabled = False

                If cmbBodega.EditValue <> 0 Then
                    IMS.Listar_Propietarios_By_IdBodega(lcmbPropietario,
                                                        cmbBodega.EditValue,
                                                        lConnection,
                                                        lTransaction,
                                                        True)
                End If

                cmbMuelle.EditValue = pBePedidoEnc.IdMuelle
                cmbMuelle.Enabled = False

                If pBePedidoEnc.PropietarioBodega.IdPropietarioBodega <> 0 Then
                    lcmbPropietario.EditValue = pBePedidoEnc.PropietarioBodega.IdPropietarioBodega

                    '#CKFK20240527 Agregué esta línea
                    Propietario.IdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

                    lcmbPropietario.Enabled = False
                End If

                cmbRoadRutaPedido.EditValue = pBePedidoEnc.RoadIdRuta
                cmbRoadVendedorPedido.EditValue = pBePedidoEnc.RoadIdVendedor

                cmbRoadRutaDespacho.EditValue = pBePedidoEnc.RoadIdRutaDespacho
                cmbRoadVendedorDespacho.EditValue = pBePedidoEnc.RoadIdVendedor

                dtpFechaPedido.EditValue = pBePedidoEnc.Fecha_Pedido
                dtpHoraInicioPreparacion.Value = pBePedidoEnc.Hora_ini
                dtpHoraFinPreparacion.Value = pBePedidoEnc.Hora_ini

                dtpFechaEntrega.EditValue = pBePedidoEnc.RoadFechaEntr
                dtpHoraEntregaDesde.Value = pBePedidoEnc.HoraEntregaDesde
                dtpHoraEntregaHasta.Value = pBePedidoEnc.HoraEntregaHasta

                If Not pBePedidoEnc.RoadDirEntrega = "" Then
                    txtDireccionEntrega.Text = pBePedidoEnc.RoadDirEntrega
                ElseIf Not pBePedidoEnc.Cliente.Direccion = "" Then
                    txtDireccionEntrega.Text = pBePedidoEnc.Cliente.Direccion
                End If

                lblEstado.Text = pBePedidoEnc.Estado
                chkActivo.Checked = pBePedidoEnc.Activo

                User_agrTextEdit1.Text = pBePedidoEnc.User_agr
                Fec_agrDateEdit1.Text = pBePedidoEnc.Fec_agr
                User_modTextEdit1.Text = pBePedidoEnc.User_mod
                Fec_modDateEdit1.Text = pBePedidoEnc.Fec_mod

                txtNoDocumento.Text = pBePedidoEnc.No_documento

                txtReferencia.ReadOnly = (pBePedidoEnc.Referencia <> "" AndAlso AP.Bodega.Interface_SAP)
                txtReferencia.Text = pBePedidoEnc.Referencia
                txtNoPickingERP.Text = pBePedidoEnc.No_Picking_ERP
                txtReferencia2.ReadOnly = (pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino <> "" AndAlso AP.Bodega.Interface_SAP)
                txtReferencia2.Text = pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino

                chkPedidoLocal.Checked = pBePedidoEnc.Local
                chkPalletPrimero.Checked = pBePedidoEnc.Pallet_primero
                txtDiasVencimiento.Value = pBePedidoEnc.Dias_cliente

                txtObservacion.Text = pBePedidoEnc.Observacion

                cmbTipoPedido.EditValue = pBePedidoEnc.IdTipoPedido
                cmbTipoPedido.Enabled = False

                Set_Tipo_Documento()

                cmbMotivoDevolucion.EditValue = pBePedidoEnc.IdMotivoDevolucion

                If Modo = TipoTrans.Nuevo Then
                    '#EJC20220327: Cambio por lookupedit. (antex textbox)
                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)
                Else

                    IMS.Listar_Clientes_By_IdCliente(txtIdCliente,
                                                     lcmbPropietario.GetColumnValue("IdPropietario"),
                                                     cmbBodega.EditValue,
                                                     pBePedidoEnc.IdCliente)
                End If

                '#EJC20220327: Cambio por lookupedit.
                txtIdCliente.EditValue = pBePedidoEnc.Cliente.IdCliente
                txtIdCliente.Enabled = False

                chkAnulado.Checked = pBePedidoEnc.Anulado
                RoadKilometrajeSpinEdit.Text = pBePedidoEnc.RoadKilometraje
                RoadTotalSpinEdit.Value = pBePedidoEnc.RoadTotal
                RoadDesMontoSpinEdit.Value = pBePedidoEnc.RoadDesMonto
                RoadImpMontoSpinEdit.Value = pBePedidoEnc.RoadImpMonto
                RoadPesoSpinEdit.Value = pBePedidoEnc.RoadPeso
                RoadBanderaTextEdit.Text = pBePedidoEnc.RoadBandera
                RoadStatComTextEdit.Text = pBePedidoEnc.RoadStatCom
                RoadCalcoBJTextEdit.Text = pBePedidoEnc.RoadCalcoBJ
                RoadImpresSpinEdit.Value = pBePedidoEnc.RoadImpres
                RoadADD1TextEdit.Text = pBePedidoEnc.RoadADD1
                RoadADD2TextEdit.Text = pBePedidoEnc.RoadADD2
                RoadADD3TextEdit.Text = pBePedidoEnc.RoadADD3
                RoadStatProcTextEdit.Text = pBePedidoEnc.RoadStatProc
                RoadRechazadoCheckEdit.Checked = pBePedidoEnc.RoadRechazado
                RoadRazon_RechazadoTextEdit.Text = pBePedidoEnc.RoadRazon_Rechazado
                RoadInformadoCheckEdit.Checked = pBePedidoEnc.RoadInformado
                RoadSucursalTextEdit.Text = pBePedidoEnc.RoadSucursal
                RoadIdDespachoSpinEdit.Value = pBePedidoEnc.RoadIdDespacho
                RoadIdFacturacionSpinEdit.Text = pBePedidoEnc.RoadIdFacturacion
                chkRequiereTarimas.Checked = pBePedidoEnc.Requiere_Tarimas
                dtpFechaPreparacion.EditValue = pBePedidoEnc.Fecha_Preparacion
                cmbManufacturaLigera.EditValue = pBePedidoEnc.IdTipoManufactura

                txtIdPicking.Text = pBePedidoEnc.IdPickingEnc

                lblBodegaDestino.Visible = pBePedidoEnc.Bodega_Destino.Trim <> ""
                txtBodegaDestino.Visible = pBePedidoEnc.Bodega_Destino.Trim <> ""

                lblBodegaOrigen.Visible = pBePedidoEnc.Bodega_Origen.Trim <> ""
                txtBodegaOrigen.Visible = pBePedidoEnc.Bodega_Origen.Trim <> ""

                txtBodegaOrigen.Text = pBePedidoEnc.Bodega_Origen
                txtBodegaDestino.Text = pBePedidoEnc.Bodega_Destino

                txtSociedadSAP.Text = pBePedidoEnc.Codigo_Empresa_ERP

                lblSociedadSAP.Visible = BeConfigBodega.Interface_SAP
                txtSociedadSAP.Visible = BeConfigBodega.Interface_SAP

                txtEsExportacion.Text = IIf(pBePedidoEnc.EsExportacion, "Si", "No")

                If txtEsExportacion.Text = "Si" Then
                    txtEsExportacion.BackColor = Color.PaleGreen
                Else
                    txtEsExportacion.BackColor = Color.LightPink
                End If

                Cargar_Detalle_Pedido(lConnection,
                                      lTransaction)

                Cargar_Log_MI3(lConnection,
                               lTransaction)

                Get_Log_Reserva(lConnection, lTransaction)

                '#EJC202403281018:Esto estaba en comentario pero no se porque ni porquien, lo revertí.
                Cargar_Imagenes()

                Set_Estado_Envio_A_ERP()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub Cargar_Log_MI3(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If Not pBePedidoEnc Is Nothing Then

                Dim lErroresInterface As New List(Of clsBeI_nav_transacciones_out_error)
                lErroresInterface = clsLnI_nav_transacciones_out_error.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                              lConnection,
                                                                                              lTransaction)

                If Not lErroresInterface Is Nothing Then

                    If lErroresInterface.Count > 0 Then

                        dgridLogMI3.DataSource = lErroresInterface
                        tabLogMI3.PageVisible = True
                        tabLogMI3.Visible = True

                        If GridView5.Columns.Count > 0 Then

                            GridView5.BestFitColumns()

                            GridView5.Columns("Fecha").DisplayFormat.FormatType = FormatType.DateTime
                            GridView5.Columns("Fecha").DisplayFormat.FormatString = "G"

                        End If


                    Else
                        tabLogMI3.PageVisible = False
                        tabLogMI3.Visible = False
                    End If

                Else
                    tabLogMI3.PageVisible = False
                    tabLogMI3.Visible = False
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub Cargar_Datos()

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            pBePedidoEnc.IsNew = False

            lblIdPedidoEnc.Text = pBePedidoEnc.IdPedidoEnc

            cmbBodega.EditValue = pBePedidoEnc.IdBodega
            cmbBodega.Enabled = False

            If cmbBodega.EditValue <> 0 Then
                IMS.Listar_Propietarios_By_IdBodega(lcmbPropietario,
                                                    cmbBodega.EditValue,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction,
                                                    True)
            End If

            txtIdCliente.EditValue = pBePedidoEnc.Cliente.IdCliente
            txtIdCliente.Enabled = False

            cmbMuelle.EditValue = pBePedidoEnc.IdMuelle
            cmbMuelle.Enabled = False

            If pBePedidoEnc.PropietarioBodega.IdPropietarioBodega <> 0 Then
                lcmbPropietario.EditValue = pBePedidoEnc.PropietarioBodega.IdPropietarioBodega
                lcmbPropietario.Enabled = False
            End If

            cmbRoadRutaPedido.EditValue = pBePedidoEnc.RoadIdRuta
            cmbRoadVendedorPedido.EditValue = pBePedidoEnc.RoadIdVendedor

            cmbRoadRutaDespacho.EditValue = pBePedidoEnc.RoadIdDespacho
            cmbRoadVendedorDespacho.EditValue = pBePedidoEnc.RoadIdVendedor

            dtpFechaPedido.EditValue = pBePedidoEnc.Fecha_Pedido
            dtpHoraInicioPreparacion.Value = pBePedidoEnc.Hora_ini
            dtpHoraFinPreparacion.Value = pBePedidoEnc.Hora_ini

            lblEstado.Text = pBePedidoEnc.Estado
            chkActivo.Checked = pBePedidoEnc.Activo

            User_agrTextEdit1.Text = pBePedidoEnc.User_agr
            Fec_agrDateEdit1.Text = pBePedidoEnc.Fec_agr
            User_modTextEdit1.Text = pBePedidoEnc.User_mod
            Fec_modDateEdit1.Text = pBePedidoEnc.Fec_mod

            txtNoDocumento.Text = pBePedidoEnc.No_documento

            txtReferencia.ReadOnly = (pBePedidoEnc.Referencia <> "" AndAlso AP.Bodega.Interface_SAP)
            txtReferencia.Text = pBePedidoEnc.Referencia
            txtNoPickingERP.Text = pBePedidoEnc.No_Picking_ERP
            txtReferencia2.ReadOnly = (pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino <> "" AndAlso AP.Bodega.Interface_SAP)
            txtReferencia2.Text = pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino

            chkPedidoLocal.Checked = pBePedidoEnc.Local
            chkPalletPrimero.Checked = pBePedidoEnc.Pallet_primero
            txtDiasVencimiento.Value = pBePedidoEnc.Dias_cliente

            cmbTipoPedido.EditValue = pBePedidoEnc.IdTipoPedido
            cmbTipoPedido.Enabled = False

            chkAnulado.Checked = pBePedidoEnc.Anulado
            RoadKilometrajeSpinEdit.Text = pBePedidoEnc.RoadKilometraje
            RoadTotalSpinEdit.Value = pBePedidoEnc.RoadTotal
            RoadDesMontoSpinEdit.Value = pBePedidoEnc.RoadDesMonto
            RoadImpMontoSpinEdit.Value = pBePedidoEnc.RoadImpMonto
            RoadPesoSpinEdit.Value = pBePedidoEnc.RoadPeso
            RoadBanderaTextEdit.Text = pBePedidoEnc.RoadBandera
            RoadStatComTextEdit.Text = pBePedidoEnc.RoadStatCom
            RoadCalcoBJTextEdit.Text = pBePedidoEnc.RoadCalcoBJ
            RoadImpresSpinEdit.Value = pBePedidoEnc.RoadImpres
            RoadADD1TextEdit.Text = pBePedidoEnc.RoadADD1
            RoadADD2TextEdit.Text = pBePedidoEnc.RoadADD2
            RoadADD3TextEdit.Text = pBePedidoEnc.RoadADD3
            RoadStatProcTextEdit.Text = pBePedidoEnc.RoadStatProc
            RoadRechazadoCheckEdit.Checked = pBePedidoEnc.RoadRechazado
            RoadRazon_RechazadoTextEdit.Text = pBePedidoEnc.RoadRazon_Rechazado
            RoadInformadoCheckEdit.Checked = pBePedidoEnc.RoadInformado
            RoadSucursalTextEdit.Text = pBePedidoEnc.RoadSucursal
            RoadIdDespachoSpinEdit.Text = pBePedidoEnc.RoadIdDespacho
            RoadIdFacturacionSpinEdit.Text = pBePedidoEnc.RoadIdFacturacion

            chkRequiereTarimas.Checked = pBePedidoEnc.Requiere_Tarimas

            dtpFechaPreparacion.EditValue = pBePedidoEnc.Fecha_Preparacion

            txtIdPicking.Text = pBePedidoEnc.IdPickingEnc

            txtObservacion.Text = pBePedidoEnc.Observacion

            '#EJC20220510: Fix
            PedidoGuardadoPorUsuario = True

            Cargar_Detalle_Pedido(clsTransaccion.lConnection,
                                  clsTransaccion.lTransaction)

            'Cargar_Imagenes()

            Set_Estado_Envio_A_ERP()

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        Finally
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub Cargar_Detalle_Pedido(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            dgrid.Rows.Clear()

            Dim i As Integer = -1
            Dim vCantidadPickeada As Double = 0
            Dim vCantidadVerificada As Double = 0
            Dim IndicePadre As Integer = -1
            Dim vCodigoPadre As String = ""
            Dim vClienteTiempo As New clsBeCliente_tiempos

            If Not pClienteTiemposList Is Nothing Then
                vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                And x.IdFamilia = pBeProducto.Familia.IdFamilia)
            End If

            Dim vDiasVencimientoCliente As Integer = 0

            If Not vClienteTiempo Is Nothing Then
                If chkPedidoLocal.Checked Then
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                Else
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                End If
            End If

            'ltrans.Begin_Transaction()

            Cliente_Detalle_Ultimo_Lote = 0
            Cliente_Detalle_Control_Calidad = 0


            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            Application.DoEvents()

            If Not pBePedidoEnc Is Nothing Then

                Dim lProductosBodega = clsLnProducto_bodega.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, lConnection, lTransaction)
                Dim lPresentacionesByPedido = clsLnProducto_presentacion.Get_All_Presentacion_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, lConnection, lTransaction)
                Dim lClientes = clsLnCliente.Get_All(lConnection, lTransaction)
                Dim lEstados = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                            lConnection,
                                                                                            lTransaction)

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Detalle de documento")

                For Each pDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle.OrderBy(Function(x) x.No_linea)

                    pBeStock = New clsBeStock
                    pBeProducto = New clsBeProducto
                    pBeProducto.IdProducto = pDet.Producto.IdProducto

                    If SplashScreenManager.Default Is Nothing Then
                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Obteniendo código: " & pDet.Codigo_Producto)
                    Else
                        SplashScreenManager.Default.SetWaitFormDescription("Obteniendo código: " & pDet.Codigo_Producto)
                    End If

                    If Not pDet.EsPadre AndAlso Not pDet.IdPedidoDetPadre > 0 Then

                        i = dgrid.Rows.Add(pDet.No_linea,
                                           pDet.Producto.IdProducto,
                                           pDet.IsNew,
                                           pDet.Codigo_Producto,
                                           pDet.Nombre_producto)

                        pBeProducto.IdProductoBodega = pDet.IdProductoBodega

                        Llena_Presentacion_Grid(i, lPresentacionesByPedido, pDet.IdPresentacion)

                        If BeTipoDoc.Generar_pedido_ingreso_bodega_destino Then
                            Llena_Cliente_Grid(i, lClientes, pDet.IdCliente)
                        End If

                        If pDet.IdCliente <> 0 Then

                            If Cliente_Control_Calidad(pDet.IdCliente, lClientes) Then
                                Cliente_Detalle_Control_Calidad += 1
                            End If

                            If Cliente_Control_Ultimo_Lote(pDet.IdCliente, lClientes) Then
                                Cliente_Detalle_Ultimo_Lote += 1
                            End If

                        End If

                        dgrid.Rows(i).Cells("colUnidadMedida").Value = pDet.Nom_unid_med

                        '#EJC20180614: Se agregó validación para que cuando no haya existencia, no se trate de desplegar el estado
                        'Llena_Estados_Grid(i,
                        '                   lConnection,
                        '                   lTransaction,
                        '                   pDet.IdEstado)

                        Llena_Estados_Grid(i,
                                           lEstados,
                                           pDet.IdEstado)

                        dgrid.Rows(i).Cells("colUnidadMedida").Value = pDet.Nom_unid_med

                        '#EJC20180114: Agregu? No_Linea y Atributo_Variante_1 en Cargar_Detalle_Pedido
                        dgrid.Rows(i).Cells("ColNo_Linea").Value = pDet.No_linea
                        dgrid.Rows(i).Cells("Atributo_Variante_1").Value = pDet.Atributo_Variante_1

                        '#EJC20180606: Para reservar stock a posteriori.
                        dgrid.Rows(i).Cells("colIdProductoBodega").Value = pDet.ProductoBodega.IdProductoBodega

                        pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
                        pBeStock.ProductoEstado.IdEstado = pDet.IdEstado
                        pBeStock.Presentacion.IdPresentacion = pDet.IdPresentacion
                        pBeStock.IdPresentacion = pDet.IdPresentacion
                        pBeStock.IdBodega = cmbBodega.EditValue

                        ''#EJC20171025_0217PM: Si no se manda la unidad de medida no devuelve el stock disponible en el pedido.
                        pBeStock.IdUnidadMedida = pDet.IdUnidadMedidaBasica

                        If pBeStock.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                            pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega
                            pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                            '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                            pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                            'Obtiene la cantidad disponible restando la cantidad reservada.
                            clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                         cmbBodega.EditValue,
                                                                         True,
                                                                         False,
                                                                         vDiasVencimientoCliente,
                                                                         True,
                                                                         lConnection,
                                                                         lTransaction)

                            pDet.CantidadReservada = clsLnStock.Get_Cantidad_Reservada_By_IdPedidoDet(pBeStock,
                                                                                                      pDet.IdPedidoDet,
                                                                                                      lConnection,
                                                                                                      lTransaction,
                                                                                                      True)
                            'GT 270720210843: para un pedido, si se edita, es porque ya se guardo, y no se debe sumar lo reservado más la existencia
                            If Modo = TipoTrans.Editar Then

                            Else
                                '#EJC20171021_1108AM: Obtiene la cantidad reservada por detalle de pedido para considerarla como disponible.
                                pBeStock.Cantidad += pDet.CantidadReservada
                            End If

                            pDet.PesoReservado = clsLnStock.Get_Peso_Reservado(pBeStock,
                                                                               pDet.IdPedidoDet,
                                                                               lConnection,
                                                                               lTransaction,
                                                                               True)

                            '#EJC20171021_1108AM: Obtiene el peso reservado por detalle de pedido para considerarlo como disponible.
                            pBeStock.Peso += pDet.PesoReservado

                            ''#EJC20171025_0221PM: Desplegar cantidad disponible en base a presentación cuando se edita un pedido.
                            If Not pBeStock.Presentacion Is Nothing Then

                                If pBeStock.Presentacion.IdPresentacion <> 0 Then

                                    dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                    dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

                                Else

                                    DgComboPresentacion = TryCast(dgrid.Rows(i).Cells("colPresentacion"), DataGridViewComboBoxCell)
                                    DgComboPresentacion.Value = Nothing

                                    dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                    dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

                                End If

                            Else

                                dgrid.Rows(i).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(i).Cells("colPesoExistencia").Value = pBeStock.Peso

                            End If

                            If pBeStock.Cantidad > 0 Then
                                dgrid.Rows(i).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            ElseIf pBeStock.Peso > 0 Then
                                dgrid.Rows(i).Cells("colPesoUnitario").Value = pBeStock.Peso
                            Else
                                dgrid.Rows(i).Cells("colPesoUnitario").Value = 0
                            End If

                            '#EJC20171021_0527PM: Obtener la cantidad pickeada.
                            If Not pDet.ListaPickingUbic Is Nothing Then

                                Try

                                    Dim vCantidadRecUMBas As Double = 0
                                    Dim vCantidadVerUMBas As Double = 0
                                    vCantidadPickeada = 0
                                    vCantidadVerificada = 0

                                    If pDet.IdPresentacion > 0 Then
                                        vCantidadRecUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
                                        vCantidadVerUMBas = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)
                                    End If

                                    If vCantidadRecUMBas > 0 OrElse vCantidadVerUMBas > 0 Then
                                        '#CM_20191128: Busco de primero la cantidad total con presentación

                                        vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
                                        vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

                                        '#CM_20191128: Busco el factor de la presentación
                                        Dim BePresProd = lPresentacionesByPedido.Find(Function(x) x.IdProducto = pDet.Producto.IdProducto)
                                        If Not BePresProd Is Nothing Then
                                            pDet.Factor = BePresProd.Factor
                                        End If

                                        '#CM_20191128: Divido la cantidad UMBas entre el factor
                                        vCantidadRecUMBas = Math.Round(vCantidadRecUMBas / pDet.Factor, 6)
                                        vCantidadVerUMBas = Math.Round(vCantidadVerUMBas / pDet.Factor, 6)

                                        '#CM_20191128: Sumo las cantidades.
                                        vCantidadPickeada = Math.Round(vCantidadPickeada + vCantidadRecUMBas, 6)
                                        vCantidadVerificada = Math.Round(vCantidadVerificada + vCantidadVerUMBas, 6)
                                    Else


                                        If pDet.IdPresentacion = 0 Then

                                            Dim vFactor As Integer = 0

                                            For Each ubic In pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet).ToList
                                                If ubic.IdPresentacion <> 0 Then

                                                    '#CM_20191128: Busco el factor de la presentación
                                                    Dim BePresProd = lPresentacionesByPedido.Find(Function(x) x.IdProducto = pDet.Producto.IdProducto)
                                                    If Not BePresProd Is Nothing Then
                                                        vFactor = BePresProd.Factor
                                                    End If

                                                    vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida) * vFactor
                                                    vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = ubic.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada) * vFactor

                                                Else

                                                    vCantidadPickeada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Recibida)
                                                    vCantidadVerificada += pDet.ListaPickingUbic.FindAll(Function(x) x.IdPickingUbic = ubic.IdPickingUbic And x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = 0).Sum(Function(y) y.Cantidad_Verificada)

                                                End If
                                            Next

                                        Else

                                            vCantidadPickeada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Recibida)
                                            vCantidadVerificada = pDet.ListaPickingUbic.FindAll(Function(x) x.IdPedidoDet = pDet.IdPedidoDet AndAlso x.IdPresentacion = pDet.IdPresentacion).Sum(Function(y) y.Cantidad_Verificada)

                                        End If

                                    End If


                                Catch ex As Exception
                                    '#EJC201710210531PM: No se pudo obtener la cantidad pickeada de la lista, podr?a pasar pero aun no se porqu? ;) 
                                End Try

                                dgrid.Rows(i).Cells("CantidadPickeada").Value = vCantidadPickeada
                                dgrid.Rows(i).Cells("CantidadVerificada").Value = vCantidadVerificada

                                Dim vDif As Double = (pDet.Cantidad - Math.Round(vCantidadPickeada, 6))
                                '#EJC20171021_0534OM: Formateo condicional de color, cantidad pedido vrs. cantidad_picking
                                Select Case vDif

                                    Case pDet.Cantidad

                                        'No se ha pickeado nada.
                                        dgrid.Rows(i).DefaultCellStyle.BackColor = Color.White

                                    Case Is > 0

                                        'Falta pickear producto 
                                        dgrid.Rows(i).DefaultCellStyle.BackColor = Color.MistyRose

                                    Case Is < 0

                                        'Sobra producto en el picking, esto no debería pasar nunca.
                                        dgrid.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow

                                    Case 0

                                        'Se pickeó completa la cantidad solicitada en el pedido.
                                        dgrid.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen

                                    Case Else
                                        Exit Select

                                End Select

                            End If

                        End If

                        dgrid.Rows(i).Cells("colCantidad").Value = pDet.Cantidad
                        If Modo = TipoTrans.Editar Then
                            dgrid.Rows(i).Cells("colCantidadExistencia").Value = pDet.Cantidad + dgrid.Rows(i).Cells("colCantidadExistencia").Value
                        End If
                        dgrid.Rows(i).Cells("colPeso").Value = pDet.Peso
                        dgrid.Rows(i).Cells("colPrecio").Value = pDet.Precio
                        dgrid.Rows(i).Cells("colTotal").Value = pDet.RoadTotal
                        dgrid.Rows(i).Cells("colIdPedidoDet").Value = pDet.IdPedidoDet
                        dgrid.Rows(i).Cells("colNoDias").Value = pDet.Ndias
                        dgrid.Rows(i).Cells("ColFechaEspecifica").Value = pDet.Fecha_especifica

                        If pDet.IdStockEspecifico > 0 Then
                            dgrid.Rows(i).Cells("IdStockEspecifico").Value = pDet.IdStockEspecifico
                        End If

                    Else
                        If pDet.EsPadre Then

                            i = dgrid.Rows.Add(pDet.No_linea,
                                               pDet.Producto.IdProducto,
                                               pDet.IsNew,
                                               pDet.Codigo_Producto,
                                               pDet.Nombre_producto)

                            IndicePadre = i
                            vCodigoPadre = pDet.Codigo_Producto

                            Set_Producto_Padre_Kit(pBeProducto,
                                                   i,
                                                   lConnection,
                                                   lTransaction)

                            dgrid.Rows(i).Cells("colCantidad").Value = pDet.Cantidad
                            If Modo = TipoTrans.Editar Then
                                dgrid.Rows(i).Cells("colCantidadExistencia").Value = pDet.Cantidad + dgrid.Rows(i).Cells("colCantidadExistencia").Value
                            End If
                            dgrid.Rows(i).Cells("colPeso").Value = pDet.Peso
                            dgrid.Rows(i).Cells("colPrecio").Value = pDet.Precio
                            dgrid.Rows(i).Cells("colTotal").Value = pDet.RoadTotal
                            dgrid.Rows(i).Cells("colIdPedidoDet").Value = pDet.IdPedidoDet
                            dgrid.Rows(i).Cells("colNoDias").Value = pDet.Ndias
                            dgrid.Rows(i).Cells("ColFechaEspecifica").Value = pDet.Fecha_especifica

                            If pDet.IdStockEspecifico > 0 Then
                                dgrid.Rows(i).Cells("IdStockEspecifico").Value = pDet.IdStockEspecifico
                            End If

                        End If

                        pDet.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                        If pDet.IdPedidoDetPadre <> 0 Then

                            pBeStock.IdProductoBodega = pDet.ProductoBodega.IdProductoBodega

                            Set_Productos_Hijos_Kit(pBeProducto,
                                                    IndicePadre,
                                                    vCodigoPadre)

                        End If

                    End If

                    i += 1

                    pBePedidoDetList.Add(pDet)

                    Application.DoEvents()

                Next

            End If

            txtControlUltimoLote.Text = IIf(Cliente_Detalle_Ultimo_Lote > 0, "Si", "No")

            If txtControlUltimoLote.Text = "Si" Then
                txtControlUltimoLote.BackColor = Color.PaleGreen
            Else
                txtControlUltimoLote.BackColor = Color.Firebrick
            End If

            txtCertificadoCalidad.Text = IIf(Cliente_Detalle_Control_Calidad > 0, "Si", "No")

            If txtCertificadoCalidad.Text = "Si" Then
                txtCertificadoCalidad.BackColor = Color.PaleGreen
            Else
                txtCertificadoCalidad.BackColor = Color.Firebrick
            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try


            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf lcmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbTipoPedido.ItemIndex < 0 Then '#CKFK20231002 Cambié el <1 por < 0 porque se puede seleccionar el itemindeex 0
                XtraMessageBox.Show("Seleccione un tipo documento.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtIdCliente.EditValue) Then
                XtraMessageBox.Show("Seleccione cliente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf IsNumeric(txtIdCliente.EditValue) AndAlso pBeCliente Is Nothing Then
                XtraMessageBox.Show(String.Format("El Código {0} del cliente no existe.", txtIdCliente.EditValue), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not Detalle_Ok() Then
                xtrPedido.SelectedTabPage = tpDetalleProducto
                dgrid.Focus()
            ElseIf chkControlPoliza.Checked And txtCodigoPoliza.Text = "" And txtNumeroDUA.Text = "" Then
                XtraMessageBox.Show("Faltan los datos de la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf BeTipoDoc.Es_Devolucion And cmbMotivoDevolucion.Text = "" Then
                XtraMessageBox.Show("Debe ingresar el motivo de devolución.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Not Integridad_Cantidades_Correcta() Then
                xtrPedido.SelectedTabPage = tpDetalleProducto
                dgrid.Focus()
            ElseIf Modo = TipoTrans.Nuevo AndAlso Pedido_Requiere_Muelle() AndAlso cmbMuelle.Text = "" Then
                XtraMessageBox.Show("La configuración del pedido requiere muelle y no fue definido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Integridad_Cantidades_Correcta() As Boolean

        Integridad_Cantidades_Correcta = True

        Dim vCantidad_Reservada_By_IdPedidoDet As Double = 0
        Dim lStockRes As New List(Of clsBeStock_res)
        Dim BePresentacion As New clsBeProducto_Presentacion

        Try

            For Each PedDet In pBePedidoDetList

                If Not PedDet.EsPadre Then

                    If PedDet.IdPresentacion <> 0 Then

                        lStockRes = clsLnStock_res.Get_All_Stock_Res_By_IdPedidoDet(PedDet.IdPedidoDet, PedDet.IdPedidoEnc)

                        vCantidad_Reservada_By_IdPedidoDet = 0

                        If Not lStockRes Is Nothing Then

                            For Each StockRes In lStockRes

                                If StockRes.IdPresentacion <> 0 Then

                                    BePresentacion.IdPresentacion = StockRes.IdPresentacion

                                    If clsLnProducto_presentacion.GetSingle(BePresentacion) Then

                                        If BePresentacion.Factor <> 0 Then
                                            vCantidad_Reservada_By_IdPedidoDet += Math.Round(StockRes.Cantidad / BePresentacion.Factor, 6)

                                        End If

                                    Else
                                        Throw New Exception("No se pudo obtener el factor de la presentación para el stock reservado, IdPresentacion: " & BePresentacion.IdPresentacion)
                                    End If

                                    '#CKFK 20210726 Puse esto en comentario ya que si es por presentación ya hizo la suma arriba.
                                    'vCantidad_Reservada_By_IdPedidoDet += StockRes.Cantidad

                                Else
                                    vCantidad_Reservada_By_IdPedidoDet += StockRes.Cantidad
                                End If

                            Next

                        Else
                            vCantidad_Reservada_By_IdPedidoDet = 0
                        End If

                    Else

                        vCantidad_Reservada_By_IdPedidoDet = PedDet.Cantidad


                    End If

                    If Not (vCantidad_Reservada_By_IdPedidoDet = PedDet.Cantidad) AndAlso (vCantidad_Reservada_By_IdPedidoDet > 0) Then

                        If XtraMessageBox.Show("Error #EJC20200113: La línea del pedido: " & PedDet.No_linea & " Código: " & PedDet.Codigo_Producto & " Refleja una cantidad de: " & PedDet.Cantidad &
                                                " que no coincide con la cantidad reservada: " & vCantidad_Reservada_By_IdPedidoDet & ", si está realizando un despacho parcial este podría ser un comportamiento correcto, continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then

                            Integridad_Cantidades_Correcta = False

                            Exit Function

                        End If

                    End If

                End If

            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            mnuGuardar.Enabled = False

            If dgrid.IsCurrentRowDirty Then

                XtraMessageBox.Show("Un valor se ha modificado en el grid y no se han " &
                       " confirmado los cambios, presione enter en la celda " &
                       " que está modificando antes de guardar el registro",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
                Exit Sub

            End If

            Guardar()

            mnuGuardar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            mnuGuardar.Enabled = True
        End Try

    End Sub

    Private Sub Guardar()


        If Datos_Correctos() Then

            If XtraMessageBox.Show("Guardar Pedido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

                PedidoGuardadoPorUsuario = Guardar_Pedido()

                SplashScreenManager.CloseForm(False)

                If PedidoGuardadoPorUsuario Then

                    ' #CKFK20171116 11:07PM: Al crear el pedido se sugiere la creación del picking automático
                    SplashScreenManager.CloseForm(False)

                    If XtraMessageBox.Show("Se guardo el pedido. Crear  picking?",
                                           Text,
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If Nuevo_Picking() Then
                            DialogResult = DialogResult.OK
                        Else
                            InvokeListarPedidos.Invoke
                            Close()
                        End If
                    Else
                        InvokeListarPedidos.Invoke
                        Close()
                    End If

                End If

            End If

        End If

    End Sub

    Private Function Detalle_Ok() As Boolean

        Detalle_Ok = True

        Try

            Dim vMensajeAnterior As Boolean = False
            Dim LineasPedido As Integer = 0

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            'SplashScreenManager.Default.SetWaitFormDescription("Guardando...")

            For I As Integer = 0 To dgrid.RowCount - 1

                If Not dgrid.Rows(I).IsNewRow Then

                    Get_ValoresGrid(I)

                    SplashScreenManager.Default.SetWaitFormDescription("Verificando Código: " & vCodigoProducto)

                    pBeProducto = New clsBeProducto
                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigoProducto,
                                                                         cmbBodega.EditValue)


                    Application.DoEvents()

                    If vIdProducto.ToString <> "" AndAlso vIdProducto <> Nothing Then

                        If vNomProducto.Trim = "" Then

                            XtraMessageBox.Show(String.Format("El Codigo de producto: {0} parece ser un Codigo no valido, por favor verifique la descripcion del producto", vCodigoProducto),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            vMensajeAnterior = True
                            Detalle_Ok = False
                            Exit For
                        ElseIf (pBeProducto.Control_peso) AndAlso vPeso = 0 Then

                            XtraMessageBox.Show(String.Format("No esta definido el peso para el producto: {0} y tiene parametro control peso", vCodigoProducto),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            Exit For
                            Detalle_Ok = False
                        ElseIf Val(vCantidad) = 0 Then

                            XtraMessageBox.Show(String.Format("No se puede procesar el producto: {0} con cantidad 0", vIdProducto),
                                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            Detalle_Ok = False
                            vMensajeAnterior = True
                            Exit For

                        ElseIf Math.Round(Val(vCantidad), 6) > Math.Round(Val(vCantidadDisponible), 6) Then
                            XtraMessageBox.Show(String.Format("Cantidad > Existencia producto: {0}", vCodigoProducto), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Detalle_Ok = False
                            vMensajeAnterior = True
                            Exit For
                        ElseIf vIdPedidoDet = 0 Then
                            LineasPedido += 1
                        Else
                            LineasPedido += 1
                        End If

                    Else
                        XtraMessageBox.Show(String.Format("Debe registrar el Código de producto en la línea: {0}", I + 1),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Detalle_Ok = False
                        vMensajeAnterior = True

                        Exit For

                    End If

                End If

            Next

            If LineasPedido > 0 AndAlso Not vMensajeAnterior Then
                Detalle_Ok = True
            Else
                If Modo = TipoTrans.Nuevo Then

                    If Not vMensajeAnterior Then
                        XtraMessageBox.Show("Ingrese el detalle del documento", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Detalle_Ok = False
                    End If

                Else

                    '#CKFK 20210528 Agregué este else porque cuando el pedido no es nuevo también se deben de validar las líneas del pedido
                    XtraMessageBox.Show("Ingrese el detalle del documento", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Detalle_Ok = False

                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Function Guardar_Pedido() As Boolean

        Guardar_Pedido = False
        Dim IdRegimen_ As Integer
        Dim hora_server As DateTime

        Try

            hora_server = clsServidor.Get_Fecha_Servidor()
            pBePedidoEnc.IdBodega = cmbBodega.EditValue
            pBePedidoEnc.Cliente = New clsBeCliente()
            pBePedidoEnc.Cliente.IdCliente = txtIdCliente.EditValue
            '#EJC20190314: Por seguridad! dejar registro en el pedido si el cliente aplicó proceso de control último lote al momento de la reserva
            'si después al cliente se le quita la bandera, podría verse como una inconsistencia en el despacho que el sistema despache un lote mas reciente.
            pBePedidoEnc.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
            pBePedidoEnc.IdMuelle = cmbMuelle.EditValue
            pBePedidoEnc.PropietarioBodega = New clsBePropietario_bodega()
            pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = lcmbPropietario.EditValue
            pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo()
            pBePedidoEnc.TipoPedido.IdTipoPedido = cmbTipoPedido.EditValue
            pBePedidoEnc.IdTipoPedido = cmbTipoPedido.EditValue
            pBePedidoEnc.Fecha_Pedido = dtpFechaPedido.EditValue
            '#GT11042023: se maneja hora_server para evitar desfases en horario
            pBePedidoEnc.Fecha_Preparacion = dtpFechaPreparacion.EditValue
            pBePedidoEnc.Hora_ini = dtpHoraInicioPreparacion.Value
            pBePedidoEnc.Hora_fin = dtpHoraFinPreparacion.Value

            pBePedidoEnc.Ubicacion = txtDireccionEntrega.Text
            pBePedidoEnc.Observacion = txtObservacion.Text.Trim

            If pBePedidoEnc.Estado.ToUpper <> "NUEVO" Then
                pBePedidoEnc.Estado = pBePedidoEnc.Estado
            Else
                pBePedidoEnc.Estado = "Nuevo"
            End If

            pBePedidoEnc.No_despacho = 0
            pBePedidoEnc.Activo = True
            pBePedidoEnc.User_agr = AP.UsuarioAp.IdUsuario
            pBePedidoEnc.Fec_agr = hora_server
            pBePedidoEnc.User_mod = AP.UsuarioAp.IdUsuario
            pBePedidoEnc.Fec_mod = hora_server
            pBePedidoEnc.No_documento = txtNoDocumento.Text
            pBePedidoEnc.Local = chkPedidoLocal.Checked
            pBePedidoEnc.Pallet_primero = chkPalletPrimero.Checked
            pBePedidoEnc.Dias_cliente = txtDiasVencimiento.Value
            pBePedidoEnc.Anulado = False

            '#EJC20171016_0239AM: EL huevo hoy si tengo sueño!.
            If pBePedidoEnc.IdPickingEnc > 0 Then
                pBePedidoEnc.IdPickingEnc = pBePedidoEnc.IdPickingEnc
            Else
                pBePedidoEnc.IdPickingEnc = 0
            End If

            pBePedidoEnc.RoadKilometraje = RoadKilometrajeSpinEdit.Value

            pBePedidoEnc.RoadFechaEntr = dtpFechaEntrega.EditValue
            pBePedidoEnc.HoraEntregaDesde = dtpHoraEntregaDesde.Value
            pBePedidoEnc.HoraEntregaHasta = dtpHoraEntregaHasta.Value

            pBePedidoEnc.RoadDirEntrega = txtDireccionEntrega.Text
            pBePedidoEnc.RoadTotal = RoadTotalSpinEdit.Value
            pBePedidoEnc.RoadDesMonto = RoadDesMontoSpinEdit.Value
            pBePedidoEnc.RoadImpMonto = RoadImpMontoSpinEdit.Value
            pBePedidoEnc.RoadPeso = RoadPesoSpinEdit.Value
            pBePedidoEnc.RoadBandera = RoadBanderaTextEdit.Text
            pBePedidoEnc.RoadStatCom = RoadStatComTextEdit.Text
            pBePedidoEnc.RoadCalcoBJ = RoadCalcoBJTextEdit.Text
            pBePedidoEnc.RoadImpres = RoadImpresSpinEdit.Value
            pBePedidoEnc.RoadADD1 = RoadADD1TextEdit.Text
            pBePedidoEnc.RoadADD2 = RoadADD2TextEdit.Text
            pBePedidoEnc.RoadADD3 = RoadADD3TextEdit.Text
            pBePedidoEnc.RoadStatProc = RoadStatProcTextEdit.Text
            pBePedidoEnc.RoadRechazado = RoadRechazadoCheckEdit.Checked
            pBePedidoEnc.RoadRazon_Rechazado = RoadRazon_RechazadoTextEdit.Text
            pBePedidoEnc.RoadInformado = RoadInformadoCheckEdit.Checked
            pBePedidoEnc.RoadSucursal = RoadSucursalTextEdit.Text
            pBePedidoEnc.RoadIdDespacho = RoadIdDespachoSpinEdit.Text
            pBePedidoEnc.RoadIdFacturacion = RoadIdFacturacionSpinEdit.Text
            pBePedidoEnc.RoadIdRuta = cmbRoadRutaPedido.EditValue
            pBePedidoEnc.RoadIdVendedor = cmbRoadVendedorPedido.EditValue
            pBePedidoEnc.RoadIdRutaDespacho = cmbRoadRutaDespacho.EditValue
            pBePedidoEnc.RoadIdVendedorDespacho = cmbRoadVendedorDespacho.EditValue
            pBePedidoEnc.Referencia = txtReferencia.Text
            pBePedidoEnc.No_Picking_ERP = txtNoPickingERP.Text
            pBePedidoEnc.Sync_MI3 = chkSyncMI3.Checked
            pBePedidoEnc.Requiere_Tarimas = chkRequiereTarimas.Checked
            pBePedidoEnc.Fecha_Preparacion = dtpFechaPreparacion.EditValue
            pBePedidoEnc.IdTipoManufactura = cmbManufacturaLigera.EditValue
            '#GT13062024: Guardar el acuerdo comercial
            pBePedidoEnc.IdAcuerdoComercial = cmbAcuerdoComercial.EditValue

            If BeTipoDoc.Es_Devolucion Then
                pBePedidoEnc.IdMotivoDevolucion = Val(cmbMotivoDevolucion.EditValue)
            Else
                pBePedidoEnc.IdMotivoDevolucion = 0
            End If

            If chkControlPoliza.Checked Then

                'GT 170820211743: Se obtiene el regimen, pero se valida que este seteado o avisar que la lectura de la poliza no lo asigno correctamente
                Dim fila As Object = cmbRegimen.GetSelectedDataRow
                If fila IsNot Nothing Then
                    IdRegimen_ = fila.Item("IdRegimen")
                Else
                    XtraMessageBox.Show("No se encontró el regimen de la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                'GT 19022021
                If pBePedidoEnc.ObjPoliza Is Nothing OrElse pBePedidoEnc.IsNew Then
                    pBePedidoEnc.ObjPoliza = New clsBeTrans_pe_pol
                    pBePedidoEnc.ObjPoliza.IsNew = True
                    pBePedidoEnc.ObjPoliza.User_agr = AP.UsuarioAp.IdUsuario
                    pBePedidoEnc.ObjPoliza.Fec_agr = Now
                Else

                    Dim BePolizaExistente As New clsBeTrans_pe_pol()
                    BePolizaExistente = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)

                    If Not BePolizaExistente Is Nothing Then
                        pBePedidoEnc.ObjPoliza.IsNew = False
                    Else
                        pBePedidoEnc.ObjPoliza.IsNew = True
                    End If

                End If

                'GT 19012021 Poliza 
                pBePedidoEnc.ObjPoliza.NoPoliza = txtCodigoPoliza.Text.Trim
                pBePedidoEnc.ObjPoliza.Pais_procede = txtPaisProcedencia.Text.Trim
                pBePedidoEnc.ObjPoliza.Total_valoraduana = txtValorAduana.Value
                '#GT13062024: validar peso bruto para efectos de prefacturacion
                pBePedidoEnc.ObjPoliza.Total_bultos_Peso = txtTotalPesoBruto.Value
                pBePedidoEnc.ObjPoliza.Total_bultos_Peso_Neto = txtTotalPesoNeto.Value

                pBePedidoEnc.ObjPoliza.Total_flete = txtValorFlete.Value
                pBePedidoEnc.ObjPoliza.Total_usd = txtTotalFOBUSD.Value
                pBePedidoEnc.ObjPoliza.Dua = txtNumeroDUA.Text.Trim
                pBePedidoEnc.ObjPoliza.Fecha_poliza = dtFechaPoliza.EditValue
                pBePedidoEnc.ObjPoliza.Tipo_cambio = txtTipoCambio.Value
                pBePedidoEnc.ObjPoliza.Total_lineas = Val(txtTotalLineas.Value)
                pBePedidoEnc.ObjPoliza.Total_bultos = Val(txtTotalBulto.Value)
                pBePedidoEnc.ObjPoliza.Total_seguro = txtValorSeguro.Value
                pBePedidoEnc.ObjPoliza.User_mod = AP.UsuarioAp.IdUsuario
                pBePedidoEnc.ObjPoliza.Fec_mod = Now
                pBePedidoEnc.Enviado_A_ERP = False

                'GT 08022021 se agrega el id regimen del combobox
                pBePedidoEnc.ObjPoliza.IdRegimen = IdRegimen_
                pBePedidoEnc.ObjPoliza.codigo_poliza = txtCodigoPoliza.Text.Trim
                pBePedidoEnc.ObjPoliza.ticket = Val(txtTicket.Text.Trim)
                pBePedidoEnc.ObjPoliza.numero_orden = txtNumeroOrden.Text.Trim
                pBePedidoEnc.ObjPoliza.fecha_aceptacion = dtpFechaAceptacion.EditValue
                pBePedidoEnc.ObjPoliza.fecha_llegada = dtpFechaLlegada.EditValue
                pBePedidoEnc.ObjPoliza.total_otros = Val(txtTotalOtros.Value)

                '#EJC202308211828: Evitar perder el número de orden de la poliza.
                If txtReferencia.Text.Trim = "" AndAlso txtNumeroOrden.Text.Trim() <> "" Then
                    txtReferencia.Text = txtNumeroOrden.Text.Trim()
                End If

                'GT 25012021
                pBePedidoEnc.ObjPoliza.clave_aduana = txtClaveAduana.Text.Trim
                pBePedidoEnc.ObjPoliza.nit_imp_exp = txtNitImpExp.Text.Trim
                pBePedidoEnc.ObjPoliza.clase = txtClase.Text.Trim
                pBePedidoEnc.ObjPoliza.mod_transporte = txtMod_transporte.Text.Trim
                pBePedidoEnc.ObjPoliza.total_liquidar = Val(txtTotal_liquidar.EditValue)
                pBePedidoEnc.ObjPoliza.total_general = Val(txtTotal_general.EditValue)
                pBePedidoEnc.ObjPoliza.activo = True

            Else

                pBePedidoEnc.ObjPoliza = Nothing

            End If

            'GT 25012001 servicios asociados
            Dim listaServ As New List(Of clsBeTrans_pe_servicios)

            If gvDetalleServicios.DataRowCount > 0 Then

                Dim servicio As New clsBeTrans_pe_servicios()

                For i As Integer = 0 To gvDetalleServicios.DataRowCount - 1

                    servicio = New clsBeTrans_pe_servicios()
                    servicio.IdOrdenPedidoEnc = pBePedidoEnc.IdPedidoEnc
                    servicio.IdOrdenPedidoServicio = 0
                    servicio.IdServicio = gvDetalleServicios.GetRowCellValue(i, "IdServicio")
                    servicio.Cantidad = gvDetalleServicios.GetRowCellValue(i, "Cantidad")
                    servicio.User_agr = AP.UsuarioAp.IdUsuario
                    servicio.User_mod = AP.UsuarioAp.IdUsuario

                    listaServ.Add(servicio)

                Next

            End If

            If BeConfigBodega.Interface_SAP Then
                If Not pBePedidoEnc.TipoPedido.Genera_Guia_Remision Then
                    If txtReferencia.Text = "" OrElse txtSociedadSAP.Text = "" Then
                        pBePedidoEnc.Sync_MI3 = False
                        pBePedidoEnc.Enviado_A_ERP = True
                    End If
                End If
            End If

            clsLnTrans_pe_enc.Actualizar_Datos(pBePedidoEnc,
                                               pBePedidoDetList,
                                               pBePedidoEnc.ObjPoliza,
                                               listaServ)

            Guardar_Pedido = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub lnkAgregarProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAgregarProducto.LinkClicked

        Try

            Dim Producto As New frmProductoList(True)
            Producto.cmdImportarExcel.Visibility = BarItemVisibility.Never
            Producto.chkActivos.Visibility = BarItemVisibility.Never
            Producto.pIdBodega = cmbBodega.EditValue
            Producto.pIdPropietarioBodega = lcmbPropietario.EditValue
            Producto.Modo = frmProductoList.pModo.Seleccion
            Producto.WindowState = FormWindowState.Maximized
            Producto.ShowDialog()

            Dim vBeProducto As New clsBeProducto
            vBeProducto = Producto.pObjProducto

            'línea nueva
            Dim i As Integer = dgrid.Rows.Count - 1

            If vBeProducto IsNot Nothing AndAlso vBeProducto.IdProducto <> 0 Then

                If vBeProducto.Kit Then
                    Set_Producto_Padre_Kit(vBeProducto, i)
                    Set_Productos_Hijos_Kit(vBeProducto, i, vBeProducto.Codigo)
                Else
                    Set_Producto(vBeProducto, i)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Set_Producto(ByRef ObjP As clsBeProducto, ByVal IndiceFila As Integer)

        Try

            If ObjP.IdProducto > 0 Then

                Dim vClienteTiempo As New clsBeCliente_tiempos

                '#EJC0220524: Marcelo lo reportó en Mercosal, corrección.|
                If pBeProducto Is Nothing Then pBeProducto = ObjP

                If Not pClienteTiemposList Is Nothing Then
                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                          x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                                          And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                End If

                Dim vDiasVencimientoCliente As Integer = 0

                If Not vClienteTiempo Is Nothing Then
                    If chkPedidoLocal.Checked Then
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                    Else
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                    End If
                End If

                Get_ValoresGrid(IndiceFila)

                vCodigoProducto = ObjP.Codigo

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colUnidadMedida").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colPrecio").Index)

                CodProductoCell.Value = vCodigoProducto

                If vCodigoProducto <> "" Then

                    Dim vIndiceDetalleExistente As Integer = 0

                    If vNoLinea Is Nothing Then vNoLinea = ""

                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                        If Modo = TipoTrans.Nuevo Then

                            If pBePedidoDetList.Count = 0 Then
                                NoLineaCell.Value = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                            Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                NoLineaCell.Value = pBePedidoDet.No_linea
                            End If

                        End If

                    End If

                    If vIdPedidoDet <> 0 Then

                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                        If pBeProducto.Codigo <> vCodigoProducto Then
                            clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                  pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                  pBePedidoEnc.IdPickingEnc)
                            'Limpiar el campo cantidad por si ten?a algún valor anteriormente
                            CantidadCell.Value = 0

                            'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                        End If

                    End If

                    pBeProducto = New clsBeProducto
                    '#CKFK 20210728 Agregué que le asigne a la clase pBeProducto el codigo del producto y no de error
                    pBeProducto.Codigo = vCodigoProducto
                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue)

                    dgrid.Item("ColPeso", IndiceFila).ReadOnly = Not pBeProducto.Control_peso

                    dgrid.Item("ColIdProducto", IndiceFila).Value = pBeProducto.IdProducto
                    dgrid.Item("ColNomProducto", IndiceFila).Value = pBeProducto.Nombre

                    Llena_Presentacion_Grid(IndiceFila)

                    dgrid.Rows(IndiceFila).Cells("colUnidadMedida").Value = pBeProducto.UnidadMedida.Nombre
                    dgrid.Rows(IndiceFila).Cells("colPrecio").Value = pBeProducto.Precio

                    Llena_Estados_Grid(IndiceFila)

                    pBeStock.ProductoEstado = New clsBeProducto_estado
                    pBeStock.ProductoEstado.IdEstado = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colEstadoProducto").Value)
                    pBeStock.Presentacion = New clsBeProducto_Presentacion
                    pBeStock.Presentacion.IdPresentacion = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colPresentacion").Value)
                    pBeStock.IdPresentacion = pBeStock.Presentacion.IdPresentacion
                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion)

                    If Not IsNothing(pBeProducto.UnidadMedida) Then
                        pBeStock.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida
                    End If

                    If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                        pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega

                        '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                        pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                        clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                 pBePedidoEnc.IdBodega,
                                                                 True,
                                                                 False,
                                                                 vDiasVencimientoCliente)

                        If Not pBeStock.Presentacion Is Nothing Then

                            If pBeStock.Presentacion.IdPresentacion <> 0 Then

                                If pBeStock.Presentacion.EsPallet Then
                                    Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPallets) * vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPallets)
                                Else
                                    Dim vCantidadPresentacion As Double = pBeStock.Cantidad '/ pBeStock.Presentacion.Factor
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPresentacion)
                                End If

                            Else
                                dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            End If

                        Else
                            dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                            dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso

                            If pBeStock.Cantidad > 0 Then
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            Else
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = 0
                            End If

                        End If

                        dgrid.Rows(IndiceFila).Cells("colIdProductoBodega").Value = pBeStock.IdProductoBodega

                    Else
                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = 0
                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = 0
                    End If

                    ContadorFocus = 0

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Producto_Padre_Kit(ByRef ObjP As clsBeProducto,
                                       ByVal IndiceFila As Integer)

        Try

            Dim lPres As New List(Of clsBeProducto_Presentacion)
            Dim lEstado As New List(Of clsBeProducto_estado)

            If ObjP.IdProducto > 0 Then

                Dim vClienteTiempo As New clsBeCliente_tiempos

                If Not pClienteTiemposList Is Nothing Then
                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                          x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                                          AndAlso x.IdFamilia = pBeProducto.Familia.IdFamilia)
                End If

                Dim vDiasVencimientoCliente As Integer = 0

                If Not vClienteTiempo Is Nothing Then
                    If chkPedidoLocal.Checked Then
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                    Else
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                    End If
                End If

                Get_ValoresGrid(IndiceFila)

                vCodigoProducto = ObjP.Codigo

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colUnidadMedida").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colPrecio").Index)

                CodProductoCell.Value = vCodigoProducto

                If vCodigoProducto <> "" Then

                    Dim vIndiceDetalleExistente As Integer = 0

                    If vNoLinea Is Nothing Then vNoLinea = ""

                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                        If Modo = TipoTrans.Nuevo Then

                            If pBePedidoDetList.Count = 0 Then
                                NoLineaCell.Value = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                            Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                NoLineaCell.Value = pBePedidoDet.No_linea
                            End If

                        End If

                    End If

                    If vIdPedidoDet <> 0 Then

                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                        If pBeProducto.Codigo <> vCodigoProducto Then
                            clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                pBePedidoEnc.IdPickingEnc)
                            'Limpiar el campo cantidad por si ten?a algún valor anteriormente
                            CantidadCell.Value = 0

                            'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                        End If

                    End If

                    pBeProducto = New clsBeProducto
                    pBeProducto.Codigo = CodProductoCell.Value
                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue)

                    clsLnProducto_kit_composicion.Get_Disp_And_All_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                            AP.IdBodega,
                                                                                            BeListProductoKit,
                                                                                            pBeStock,
                                                                                            lEstado,
                                                                                            lPres,
                                                                                            NoLineaCell.Value)
                    'End If

                    dgrid.Item("ColPeso", IndiceFila).ReadOnly = Not pBeProducto.Control_peso
                    dgrid.Item("ColIdProducto", IndiceFila).Value = pBeProducto.IdProducto
                    dgrid.Item("ColNomProducto", IndiceFila).Value = pBeProducto.Nombre

                    Llena_Presentacion_Grid_ProductoKit(IndiceFila, lPres)

                    dgrid.Rows(IndiceFila).Cells("colUnidadMedida").Value = pBeProducto.UnidadMedida.Nombre
                    dgrid.Rows(IndiceFila).Cells("colPrecio").Value = pBeProducto.Precio

                    If pBeStock.ProductoEstado.IdEstado = 0 Then

                        Llena_Estados_Grid_ProductoKit(IndiceFila,
                                                    lEstado,
                                                    pBeStock.ProductoEstado.IdEstado)

                        pBeStock.ProductoEstado = New clsBeProducto_estado
                        pBeStock.ProductoEstado.IdEstado = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colEstadoProducto").Value)

                    End If

                    pBeStock.Presentacion = New clsBeProducto_Presentacion
                    pBeStock.Presentacion.IdPresentacion = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colPresentacion").Value)
                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion)

                    If Not IsNothing(pBeProducto.UnidadMedida) Then
                        pBeStock.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida
                    End If

                    If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                        pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega

                        If Not pBeStock.Presentacion Is Nothing Then

                            If pBeStock.Presentacion.IdPresentacion <> 0 Then

                                If pBeStock.Presentacion.EsPallet Then
                                    Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPallets) * vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPallets)
                                Else
                                    Dim vCantidadPresentacion As Double = pBeStock.Cantidad / pBeStock.Presentacion.Factor
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPresentacion)
                                End If

                            Else
                                dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            End If

                        Else
                            dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                            dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso

                            If pBeStock.Cantidad > 0 Then
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            Else
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = 0
                            End If

                        End If

                        dgrid.Rows(IndiceFila).Cells("colIdProductoBodega").Value = pBeStock.IdProductoBodega

                    Else
                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = 0
                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = 0
                    End If

                    ContadorFocus = 0

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Producto_Padre_Kit(ByRef ObjP As clsBeProducto,
                                       ByVal IndiceFila As Integer,
                                       ByVal lConnection As SqlConnection,
                                       ByVal lTransaction As SqlTransaction)

        Try

            Dim lPres As New List(Of clsBeProducto_Presentacion)
            Dim lEstado As New List(Of clsBeProducto_estado)

            If ObjP.IdProducto > 0 Then

                Dim vClienteTiempo As New clsBeCliente_tiempos

                If Not pClienteTiemposList Is Nothing Then
                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                          x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                                          And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                End If

                Dim vDiasVencimientoCliente As Integer = 0

                If Not vClienteTiempo Is Nothing Then
                    If chkPedidoLocal.Checked Then
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                    Else
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                    End If
                End If

                Get_ValoresGrid(IndiceFila)

                vCodigoProducto = ObjP.Codigo

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colUnidadMedida").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colPrecio").Index)

                CodProductoCell.Value = vCodigoProducto

                If vCodigoProducto <> "" Then

                    Dim vIndiceDetalleExistente As Integer = 0

                    If vNoLinea Is Nothing Then vNoLinea = ""

                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                        If Modo = TipoTrans.Nuevo Then

                            If pBePedidoDetList.Count = 0 Then
                                NoLineaCell.Value = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                            Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                NoLineaCell.Value = pBePedidoDet.No_linea
                            End If

                        End If

                    End If

                    If vIdPedidoDet <> 0 Then

                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                        If pBeProducto.Codigo <> vCodigoProducto Then
                            clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                    pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                    pBePedidoEnc.IdPickingEnc,
                                                                    lConnection,
                                                                    lTransaction)
                            'Limpiar el campo cantidad por si ten?a algún valor anteriormente
                            CantidadCell.Value = 0

                            'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                        End If

                    End If

                    pBeProducto = New clsBeProducto()
                    pBeProducto.Codigo = CodProductoCell.Value
                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo,
                                                                         cmbBodega.EditValue,
                                                                         lConnection,
                                                                         lTransaction)

                    clsLnProducto_kit_composicion.Get_Disp_And_All_By_IdProducto_And_IdBodega(pBeProducto.IdProducto,
                                                                                              AP.IdBodega,
                                                                                              BeListProductoKit,
                                                                                              pBeStock,
                                                                                              lEstado,
                                                                                              lPres,
                                                                                              NoLineaCell.Value,
                                                                                              lConnection,
                                                                                              lTransaction)

                    dgrid.Item("ColPeso", IndiceFila).ReadOnly = Not pBeProducto.Control_peso
                    dgrid.Item("ColIdProducto", IndiceFila).Value = pBeProducto.IdProducto
                    dgrid.Item("ColNomProducto", IndiceFila).Value = pBeProducto.Nombre

                    Llena_Presentacion_Grid_ProductoKit(IndiceFila, lPres)

                    dgrid.Rows(IndiceFila).Cells("colUnidadMedida").Value = pBeProducto.UnidadMedida.Nombre
                    dgrid.Rows(IndiceFila).Cells("colPrecio").Value = pBeProducto.Precio

                    If pBeStock.ProductoEstado.IdEstado = 0 Then

                        Llena_Estados_Grid_ProductoKit(IndiceFila, lEstado, pBeStock.ProductoEstado.IdEstado)

                        pBeStock.ProductoEstado = New clsBeProducto_estado
                        pBeStock.ProductoEstado.IdEstado = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colEstadoProducto").Value)

                    End If

                    pBeStock.Presentacion = New clsBeProducto_Presentacion()
                    pBeStock.Presentacion.IdPresentacion = Convert.ToInt32(dgrid.Rows(IndiceFila).Cells("colPresentacion").Value)
                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion, lConnection, lTransaction)

                    If Not IsNothing(pBeProducto.UnidadMedida) Then
                        pBeStock.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida
                    End If

                    If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                        pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega

                        If Not pBeStock.Presentacion Is Nothing Then

                            If pBeStock.Presentacion.IdPresentacion <> 0 Then

                                If pBeStock.Presentacion.EsPallet Then
                                    Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPallets) * vCantidadPallets
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPallets)
                                Else
                                    Dim vCantidadPresentacion As Double = pBeStock.Cantidad / pBeStock.Presentacion.Factor
                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPresentacion)
                                End If

                            Else
                                dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            End If

                        Else
                            dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                            dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso

                            If pBeStock.Cantidad > 0 Then
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                            Else
                                dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = 0
                            End If

                        End If

                        dgrid.Rows(IndiceFila).Cells("colIdProductoBodega").Value = pBeStock.IdProductoBodega

                    Else
                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = 0
                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = 0
                    End If

                    ContadorFocus = 0

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Productos_Hijos_Kit(ByRef ObjP As clsBeProducto,
                                        ByVal IndiceFilaPadre As Integer,
                                        ByVal pCodigoPadre As String)

        Try

            If dgrid.Rows.Count >= IndiceFilaPadre AndAlso IndiceFilaPadre <> -1 Then

                Dim NoLinea As Integer = dgrid.Rows(IndiceFilaPadre).Cells("ColNo_Linea").Value

                If ObjP.IdProducto > 0 Then

                    Dim vCodigoProductoHijo As String = ObjP.IdProductoBodega

                    If Not BeListProductoKit Is Nothing AndAlso BeListProductoKit.Count > 0 Then

                        For Each ObjPK As clsBeProducto_kit_composicion In BeListProductoKit.Where(Function(x) x.IdProductoHijo = vCodigoProductoHijo)

                            grdComposicion.DataSource = Nothing

                            DTProductoKitComposicion.Rows.Add(ObjPK.No_Linea,
                                                              pCodigoPadre,
                                                              ObjPK.Producto.Codigo,
                                                              ObjPK.Producto.Nombre,
                                                              ObjPK.Producto.UnidadMedida.Nombre,
                                                              ObjPK.Cantidad,
                                                              ObjPK.BeStock.Cantidad)

                        Next

                        grdComposicion.DataSource = DTProductoKitComposicion

                        grdVComp.OptionsView.ColumnAutoWidth = False
                        grdVComp.OptionsView.ShowFooter = True

                        lblRegs.Caption = String.Format("Registros: {0}", grdVComp.RowCount)

                        grdVComp.Columns("NoLinea").GroupIndex = 0

                        grdVComp.Columns("Cantidad_Kit").DisplayFormat.FormatType = FormatType.Numeric
                        grdVComp.Columns("Cantidad_Kit").DisplayFormat.FormatString = "{0:n6}"

                        grdVComp.Columns("Cantidad_Kit").SummaryItem.SummaryType = SummaryItemType.Sum
                        grdVComp.Columns("Cantidad_Kit").SummaryItem.DisplayFormat = "{0:n6}"

                        grdVComp.Columns("Cantidad_Disp").DisplayFormat.FormatType = FormatType.Numeric
                        grdVComp.Columns("Cantidad_Disp").DisplayFormat.FormatString = "{0:n6}"

                        grdVComp.Columns("Cantidad_Disp").SummaryItem.SummaryType = SummaryItemType.Sum
                        grdVComp.Columns("Cantidad_Disp").SummaryItem.DisplayFormat = "{0:n6}"

                        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                          With {.FieldName = "Cantidad",
                          .SummaryType = SummaryItemType.Sum,
                          .DisplayFormat = "{0:n6}",
                          .ShowInGroupColumnFooter = grdVComp.Columns("Cantidad")}
                        grdVComp.GroupSummary.Add(item)

                        grdVComp.BestFitColumns(True)

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Set_Producto_Stock_Especifico(ByRef ObjStockEspec As clsBeVW_stock_res,
                                              ByVal IndiceFila As Integer)

        Try

            If ObjStockEspec.IdStock > 0 Then

                Get_ValoresGrid(IndiceFila)

                vCodigoProducto = ObjStockEspec.Codigo_Producto

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colUnidadMedida").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colPrecio").Index)
                Dim vValorUltimaLinea As Double = 0

                CodProductoCell.Value = vCodigoProducto

                If vCodigoProducto.ToString <> "" Then

                    Dim vIndiceDetalleExistente As Integer = 0

                    If vNoLinea Is Nothing Then vNoLinea = ""

                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                        If Modo = TipoTrans.Nuevo Then

                            If pBePedidoDetList.Count = 0 Then
                                NoLineaCell.Value = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                            Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                NoLineaCell.Value = pBePedidoDet.No_linea
                            End If

                        Else

                            '#ejc20220712:  get last linea value
                            For i As Integer = 0 To dgrid.RowCount - 2
                                vValorUltimaLinea = Val(dgrid.Rows(i).Cells(dgrid.Columns("ColNo_Linea").Index).Value)
                            Next i

                            NoLineaCell.Value = vValorUltimaLinea + 1

                        End If

                    End If

                    If vIdPedidoDet <> 0 Then

                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                        If pBeProducto.Codigo <> vCodigoProducto Then
                            clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                    pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                    pBePedidoEnc.IdPickingEnc)
                            'Limpiar el campo cantidad por si ten?a algún valor anteriormente
                            CantidadCell.Value = 0

                            'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                        End If

                    End If


                    pBeProducto = New clsBeProducto
                    pBeProducto.Codigo = CodProductoCell.Value
                    pBeProducto = clsLnProducto.Get_BeProducto_By_IdProducto(ObjStockEspec.IdProducto, cmbBodega.EditValue)

                    If Not pBeProducto Is Nothing Then

                        Dim nombre, umbas_nombre As String
                        Dim vPrecio As Double = 0

                        nombre = pBeProducto.Nombre
                        umbas_nombre = pBeProducto.UnidadMedida.Nombre

                        '#GT05112025: si el producto no esta costeado, validar si lo esta en su ingreso
                        If pBeProducto.Precio > 0 Then
                            vPrecio = pBeProducto.Precio
                        Else
                            vPrecio = ObjStockEspec.Costo
                        End If


                        dgrid.Item("ColPeso", IndiceFila).ReadOnly = Not pBeProducto.Control_peso
                        dgrid.Item("ColIdProducto", IndiceFila).Value = pBeProducto.IdProducto
                        dgrid.Item("ColNomProducto", IndiceFila).Value = nombre
                        dgrid.Rows(IndiceFila).Cells("colUnidadMedida").Value = umbas_nombre
                        dgrid.Rows(IndiceFila).Cells("colPrecio").Value = vPrecio

                        pBeStock = clsLnStock.GetSingle(ObjStockEspec.IdStock)

                        If pBeStock.IdPresentacion <> 0 Then
                            Llena_Presentacion_Grid(IndiceFila, pBeStock.IdPresentacion)
                        Else
                            pBeStock.Presentacion = Nothing
                        End If

                        Llena_Estados_Grid(IndiceFila, pBeStock.IdProductoEstado)

                        If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.IdProductoEstado <> 0 Then

                            If ObjStockEspec.CantidadReservadaUMBas <> 0 Then
                                pBeStock.Cantidad -= Math.Round(ObjStockEspec.CantidadReservadaUMBas, 6)
                            End If

                            If Not pBeStock.Presentacion Is Nothing Then

                                If pBeStock.IdPresentacion <> 0 Then
                                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.IdPresentacion)
                                End If

                                If pBeStock.Presentacion.IdPresentacion <> 0 OrElse pBeStock.IdPresentacion <> 0 Then

                                    If pBeStock.Presentacion.EsPallet Then
                                        Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPallets
                                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = Math.Round((pBeStock.Peso / vCantidadPallets) * vCantidadPallets, 6)
                                        dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = Math.Round((pBeStock.Peso / vCantidadPallets), 6)
                                    Else
                                        Dim vCantidadPresentacion As Double = pBeStock.Cantidad / pBeStock.Presentacion.Factor
                                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = Math.Round(vCantidadPresentacion, 6)
                                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = Math.Round((pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion, 6)
                                        dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = Math.Round((pBeStock.Peso / vCantidadPresentacion), 6)
                                    End If

                                Else

                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad

                                End If

                            Else

                                dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso

                                If pBeStock.Cantidad > 0 Then
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                                Else
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = 0
                                End If

                            End If

                        Else
                            dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = 0
                            dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = 0
                        End If

                        ContadorFocus = 0

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Is_Seting_Stock_Especifico As Boolean = False
    Private Sub Set_Producto_Stock_Especifico_Con_Reserva_Completa(ByRef ObjStockEspec As clsBeVW_stock_res,
                                                                   ByVal IndiceFila As Integer)

        Try

            If ObjStockEspec.IdStock > 0 Then

                Get_ValoresGrid(IndiceFila)

                Is_Seting_Stock_Especifico = True

                vCodigoProducto = ObjStockEspec.Codigo_Producto

                Dim IdProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colIdProducto").Index)
                Dim CodProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColCodProducto").Index)
                Dim NomProductoCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNomProducto").Index)
                Dim UniMedCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colUnidadMedida").Index)
                Dim colPrecio As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("colPrecio").Index)
                Dim NoLineaCell As DataGridViewCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNo_Linea").Index)

                NoDiasVencimientoCell = dgrid.Rows(IndiceFila).Cells(dgrid.Columns("ColNo_Linea").Index)

                CodProductoCell.Value = vCodigoProducto

                If vCodigoProducto.ToString <> "" Then

                    Dim vIndiceDetalleExistente As Integer = 0

                    If (vNoLinea Is Nothing) OrElse (NoLineaCell.Value Is Nothing) Then vNoLinea = ""

                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                        If Modo = TipoTrans.Nuevo Then

                            If pBePedidoDetList.Count = 0 Then
                                NoLineaCell.Value = 1 : vNoLinea = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                                vNoLinea = NoLineaCell.Value
                            Else 'Se movió hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!                                
                                NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                                vNoLinea = NoLineaCell.Value
                            End If

                        End If

                    End If

                    If vIdPedidoDet <> 0 Then

                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                        If Not pBeProducto Is Nothing Then

                            If pBeProducto.Codigo <> vCodigoProducto Then
                                clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                        pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                        pBePedidoEnc.IdPickingEnc)
                                'Limpiar el campo cantidad por si tenía algún valor anteriormente
                                CantidadCell.Value = 0

                                'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                                pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                            End If

                        End If

                    End If


                    pBeProducto = New clsBeProducto()
                    pBeProducto.Codigo = CodProductoCell.Value
                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue)

                    If Not pBeProducto Is Nothing Then

                        Dim nombre, umbas_nombre As String
                        Dim vPrecio As Double = 0

                        nombre = pBeProducto.Nombre
                        vNomProducto = pBeProducto.Nombre
                        umbas_nombre = pBeProducto.UnidadMedida.Nombre
                        vNomUnidadMedida = umbas_nombre
                        vPrecio = pBeProducto.Precio

                        dgrid.Item("ColPeso", IndiceFila).ReadOnly = IIf(pBeProducto.Control_peso, 1, 0)
                        dgrid.Item("ColIdProducto", IndiceFila).Value = pBeProducto.IdProducto
                        dgrid.Item("ColNomProducto", IndiceFila).Value = nombre
                        dgrid.Rows(IndiceFila).Cells("colUnidadMedida").Value = umbas_nombre
                        dgrid.Rows(IndiceFila).Cells("colPrecio").Value = vPrecio
                        dgrid.Rows(IndiceFila).Cells("colNo_Linea").Value = vNoLinea

                        pBeStock = clsLnStock.GetSingle(ObjStockEspec.IdStock)

                        Llena_Presentacion_Grid(IndiceFila, pBeStock.IdPresentacion)
                        Llena_Estados_Grid(IndiceFila, pBeStock.IdProductoEstado)
                        If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.IdProductoEstado <> 0 Then

                            If ObjStockEspec.CantidadReservadaUMBas <> 0 Then
                                pBeStock.Cantidad -= Math.Round(ObjStockEspec.CantidadReservadaUMBas, 6)
                            End If

                            If Not pBeStock.Presentacion Is Nothing Then

                                If pBeStock.IdPresentacion <> 0 Then
                                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.IdPresentacion)
                                End If

                                If pBeStock.Presentacion.IdPresentacion <> 0 OrElse pBeStock.IdPresentacion <> 0 Then

                                    If pBeStock.Presentacion.EsPallet Then
                                        Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPallets
                                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = Math.Round((pBeStock.Peso / vCantidadPallets) * vCantidadPallets, 6)
                                        dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = Math.Round((pBeStock.Peso / vCantidadPallets), 6)
                                    Else

                                        Dim vCantidadPresentacion As Double = Math.Round(pBeStock.Cantidad / pBeStock.Presentacion.Factor, 6)
                                        Dim vPesoPresentacion As Double = Math.Round((pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion, 6)

                                        dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = vCantidadPresentacion
                                        dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = vPesoPresentacion
                                        dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = Math.Round((pBeStock.Peso / vCantidadPresentacion), 6)
                                        dgrid.Rows(IndiceFila).Cells("colCantidad").Value = Math.Round(vCantidadPresentacion, 6)
                                        dgrid.Rows(IndiceFila).Cells("colPeso").Value = vPesoPresentacion

                                        vCantidad = vCantidadPresentacion


                                    End If

                                Else

                                    dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                    dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad

                                    dgrid.Rows(IndiceFila).Cells("colCantidad").Value = pBeStock.Cantidad
                                    dgrid.Rows(IndiceFila).Cells("colPeso").Value = pBeStock.Peso

                                    vCantidad = pBeStock.Cantidad

                                End If

                            Else

                                dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = pBeStock.Peso

                                If pBeStock.Cantidad > 0 Then
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                                Else
                                    dgrid.Rows(IndiceFila).Cells("colPesoUnitario").Value = 0
                                End If

                            End If

                        Else
                            dgrid.Rows(IndiceFila).Cells("colCantidadExistencia").Value = 0
                            dgrid.Rows(IndiceFila).Cells("colPesoExistencia").Value = 0
                        End If

                        ContadorFocus = 0

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Is_Seting_Stock_Especifico = False
        End Try

    End Sub

    Private Sub Actualiza_grid_Composicion()

        Try

            If Not BeListProductoKit Is Nothing AndAlso BeListProductoKit.Count > 0 Then

                Dim DT As New DataTable("ProductoKitComposicion")
                DT.Columns.Add("NoLinea", GetType(String))
                DT.Columns.Add("Padre", GetType(String))
                DT.Columns.Add("Código", GetType(String))
                DT.Columns.Add("Nombre", GetType(String))
                DT.Columns.Add("U.M.Bas", GetType(String))
                DT.Columns.Add("Cantidad_Kit", GetType(Double))
                DT.Columns.Add("Cantidad_Disp", GetType(Double))

                For Each ObjPK As clsBeProducto_kit_composicion In BeListProductoKit

                    grdComposicion.DataSource = Nothing

                    DT.Rows.Add(ObjPK.No_Linea, ObjPK.Producto.Codigo, ObjPK.Producto.Codigo,
                                        ObjPK.Producto.Nombre,
                                        ObjPK.Producto.UnidadMedida.Nombre,
                                        ObjPK.Cantidad, ObjPK.BeStock.Cantidad)

                Next

                grdComposicion.DataSource = DT

                grdVComp.OptionsView.ColumnAutoWidth = False
                grdVComp.OptionsView.ShowFooter = True

                lblRegs.Caption = String.Format("Registros: {0}", grdVComp.RowCount)

                grdVComp.Columns("NoLinea").GroupIndex = 0

                grdVComp.Columns("Cantidad_Kit").DisplayFormat.FormatType = FormatType.Numeric
                grdVComp.Columns("Cantidad_Kit").DisplayFormat.FormatString = "{0:n6}"
                grdVComp.Columns("Cantidad_Kit").SummaryItem.SummaryType = SummaryItemType.Sum
                grdVComp.Columns("Cantidad_Kit").SummaryItem.DisplayFormat = "{0:n6}"

                grdVComp.Columns("Cantidad_Disp").DisplayFormat.FormatType = FormatType.Numeric
                grdVComp.Columns("Cantidad_Disp").DisplayFormat.FormatString = "{0:n6}"
                grdVComp.Columns("Cantidad_Disp").SummaryItem.SummaryType = SummaryItemType.Sum
                grdVComp.Columns("Cantidad_Disp").SummaryItem.DisplayFormat = "{0:n6}"

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                  With {.FieldName = "Cantidad",
                  .SummaryType = SummaryItemType.Sum,
                  .DisplayFormat = "{0:n6}",
                  .ShowInGroupColumnFooter = grdVComp.Columns("Cantidad")}
                grdVComp.GroupSummary.Add(item)

                grdVComp.BestFitColumns(True)


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmPedido_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try

            If e.KeyCode = Keys.F2 AndAlso xtrPedido.SelectedTabPageIndex = 1 Then
                lnkAgregarProducto_LinkClicked(Nothing, Nothing)
            End If

            '#CKFK 20210723 Cambié esto xtrPedido.SelectedTabPageIndex = 1 por =2
            If e.KeyCode = Keys.Delete AndAlso xtrPedido.SelectedTabPageIndex = 2 Then

                dgrid.EndEdit()

                If (dgrid.SelectedRows.Count > 0) AndAlso Not dgrid.SelectedRows.Item(0).IsNewRow Then

                    Dim Indice As Integer = dgrid.CurrentCell.RowIndex
                    Dim BeDetallePedidoHijos As New List(Of clsBeTrans_pe_det)
                    Dim EsPadre As Boolean = False
                    Dim vIdPedidoDet As String = IIf(IsDBNull(dgrid.Item("ColIdPedidoDet", Indice).Value), "", dgrid.Item("ColIdPedidoDet", Indice).Value)
                    Dim vIdStockEspecifico As Integer = IIf(IsDBNull(dgrid.Item("IdStockEspecifico", Indice).Value), 0, dgrid.Item("IdStockEspecifico", Indice).Value)

                    If XtraMessageBox.Show("¿Eliminar línea?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If Not Val(vIdPedidoDet = 0) Then

                            If Modo = TipoTrans.Editar Then

                                Dim Idx As Integer = pBePedidoEnc.Detalle.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                                If Idx <> -1 Then EsPadre = pBePedidoEnc.Detalle(Idx).EsPadre

                                If EsPadre Then
                                    BeDetallePedidoHijos = pBePedidoEnc.Detalle.FindAll(Function(x) x.IdPedidoDetPadre = vIdPedidoDet)
                                End If
                            Else

                                '#EJC20201018: Fix Object references.
                                Dim Obj = pBePedidoDetList.Find(Function(x) x.IdPedidoDet = vIdPedidoDet)

                                If Not Obj Is Nothing Then
                                    EsPadre = Obj.EsPadre
                                    If EsPadre Then
                                        BeDetallePedidoHijos = pBePedidoDetList.FindAll(Function(x) x.IdPedidoDetPadre = vIdPedidoDet)
                                    End If
                                End If

                            End If

                            If EsPadre Then

                                If Not BeDetallePedidoHijos Is Nothing AndAlso BeDetallePedidoHijos.Count > 0 Then

                                    For Each objDetH In BeDetallePedidoHijos

                                        If (clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                      objDetH.IdPedidoDet,
                                                                                      pBePedidoEnc.IdPickingEnc) > 0) Then

                                            Dim IdxListal As Integer = pBePedidoEnc.Detalle.FindIndex(Function(x) x.IdPedidoDet = objDetH.IdPedidoDet)

                                            If IdxListal <> -1 Then
                                                pBePedidoEnc.Detalle.RemoveAt(IdxListal)
                                            End If

                                            Dim IdxListalN As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = objDetH.IdPedidoDet)

                                            If IdxListalN <> -1 Then
                                                pBePedidoDetList.RemoveAt(IdxListalN)
                                            End If

                                            Dim IdxListHijos As Integer = BeListProductoKit.FindIndex(Function(x) x.No_Linea = objDetH.No_linea AndAlso x.Producto.Codigo = objDetH.Codigo_Producto)

                                            If IdxListHijos <> -1 Then
                                                BeListProductoKit.RemoveAt(IdxListHijos)
                                            End If

                                            'dgrid.Rows.Remove(dgrid.SelectedRows.Item(0))

                                        Else
                                            XtraMessageBox.Show("No se pudo elimminar la línea del pedido en la bd", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If

                                    Next

                                    Actualiza_grid_Composicion()

                                End If

                            End If

                            If Not (clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                              vIdPedidoDet,
                                                                              pBePedidoEnc.IdPickingEnc) > 0) Then

                                XtraMessageBox.Show("No se pudo eliminar la línea del pedido en la bd", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            Else

                                '#EJC20180619: Si se elimina una línea de pedido actualizar tab/grid de stock reservado
                                Carga_Stock_Reservado()

                                Cargar_Picking()

                                'Cargar_Detalle_Pedido()

                                '#GT31032025: si es edicion el detalle del pedido existe, si es nuevo, detalle=nothing
                                Dim IdxLista As Integer = -1
                                If Modo = TipoTrans.Editar Then
                                    IdxLista = pBePedidoEnc.Detalle.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)
                                End If

                                Dim IdxIdStockEspeficico As Integer = pListBeTrans_ubic_hh_det.FindIndex(Function(x) x.IdStock = vIdStockEspecifico)

                                Dim IdxListalN As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                                If IdxListalN <> -1 Then
                                    pBePedidoDetList.RemoveAt(IdxListalN)
                                End If

                                If IdxLista <> -1 Then
                                    pBePedidoEnc.Detalle.RemoveAt(IdxLista)
                                End If

                                If IdxIdStockEspeficico <> -1 Then
                                    pListBeTrans_ubic_hh_det.RemoveAt(IdxIdStockEspeficico)
                                End If

                                dgrid.Rows.Remove(dgrid.SelectedRows.Item(0))

                                If Not PedidoTieneDetalle() Then

                                    dgrid.EndEdit(DataGridViewDataErrorContexts.Commit)

                                    If XtraMessageBox.Show(String.Format("El pedido tiene :{0} líneas de detalle, ¿Eliminar el pedido?", dgrid.RowCount - 1),
                                                           Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                        If EliminaEncabezadoPedidoSinDetalle() Then

                                            XtraMessageBox.Show("Se eliminó el pedido",
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information)

                                            Preguntar_Al_Salir = False

                                            Close()

                                        End If

                                    End If

                                End If

                            End If

                        Else
                            If vIdPedidoDet Is Nothing Then
                                dgrid.Rows.Remove(dgrid.SelectedRows.Item(0))
                                '#CM_CK_20180917_12:59PM: Inicializamos las clases pBeProducto y pBeStock porque se quedaba con información anterior 
                                pBeProducto = Nothing
                                pBeStock = New clsBeStock
                            End If
                        End If

                        e.Handled = True

                    Else
                        e.Handled = False
                    End If

                End If

            End If

            'Configuración
            If e.KeyCode = Keys.F3 Then

                If dgrid.Focused Then

                    If XtraMessageBox.Show("¿Abrir configuración de producto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim frm As New frmProducto(frmProducto.TipoTrans.Consulta)
                        frm.pBeProducto.IdProducto = pBeProducto.IdProducto
                        frm.ShowDialog(Me)
                        frm.Dispose()

                    End If

                End If

            End If

            'Parámetros de producto
            If e.KeyCode = Keys.F4 And Not e.Alt Then
                If dgrid.CurrentCell.ColumnIndex <> 1 Then Exit Sub
            End If

            'Stock específico
            If e.KeyCode = Keys.F5 AndAlso xtrPedido.SelectedTabPageIndex = 2 Then
                lnkAgregarStockEspecifico_LinkClicked(Nothing, Nothing)
            End If

            'Cierra Forma con Escape
            If e.KeyCode = Keys.Escape Then Close()

            If e.KeyCode = Keys.Delete Then Exit Sub

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub lnkCliente_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCliente.LinkClicked

        Set_Cliente()

    End Sub

    Private Sub Set_Cliente()

        Try

            Dim CliList As New frmCliente_List() With {.Modo = frmCliente_List.pModo.Seleccion,
                                                       .EsProveedor = IIf(BeTipoDoc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor, True, False)}

            If OpcionesMenu IsNot Nothing Then
                CliList.OpcionesMenu = OpcionesMenu
                CliList.mnuActualizar.Enabled = OpcionesMenu.Leer
                CliList.mnuNuevo.Enabled = OpcionesMenu.Modificar
            End If

            CliList.Propietario.Nombre_comercial = lcmbPropietario.Text
            CliList.Propietario = clsLnPropietarios.GetSingle(clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, lcmbPropietario.EditValue))
            CliList.Requerir_Cliente_Es_Bodega_WMS = BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS

            If Not CliList.Propietario() Is Nothing Then

                CliList.ShowDialog()

                ''GT21082022_2100: deje esto aca para para que cargue el list de clientes si hacen clic en lnkCliente
                'IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                '                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                '                                         0,
                '                                         cmbBodega.EditValue,
                '                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                '#CKFK20241107 Listar los clientes tomando en cuenta el tipo de documento
                If BeTipoDoc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor Then

                    IMS.Listar_Clientes_By_IdPropietario_By_EsProveedor(txtIdCliente,
                                                                        lcmbPropietario.GetColumnValue("IdPropietario"),
                                                                        cmbBodega.EditValue,
                                                                        BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS,
                                                                        True)

                Else

                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                End If

                pBeCliente = CliList.gCliente

                If pBeCliente IsNot Nothing AndAlso pBeCliente.IdCliente <> 0 Then

                    txtIdCliente.EditValue = pBeCliente.IdCliente

                End If

                CliList.Close()
                CliList.Dispose()

            Else

                XtraMessageBox.Show("Debe definir el propietario como filtro base para la selección de cliente", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub RemoveValueChangedHandlerPres(ByVal sender As Object, ByVal e As EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerPres
        End If

    End Sub

    Private Sub RemoveValueChangedHandlerEstado(ByVal sender As Object, ByVal e As EventArgs)

        If TypeOf (sender) Is DataGridViewComboBoxEditingControl Then
            Dim cboThisCombobox = DirectCast(sender, DataGridViewComboBoxEditingControl)

            RemoveHandler cboThisCombobox.SelectedValueChanged, LastEventHandlerEstado
        End If

    End Sub

    Private Sub dgrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid.CellValueChanged

        Try

            Dim row As DataGridViewRow = dgrid.CurrentRow

            If row Is Nothing OrElse (row.IsNewRow) OrElse Is_Seting_Stock_Especifico Then Exit Sub

            Get_ValoresGrid(e.RowIndex)

            If vCodigoProducto Is Nothing OrElse vCodigoProducto = "" Then Exit Sub

            row.Cells("colTotal").Value = Math.Round(vCantidad * vPrecio, 2)

            If dgrid.Columns(e.ColumnIndex).Name() = "ColCantidad" Or dgrid.Columns(e.ColumnIndex).Name() = "ColPrecio" Then
                setTotal(e.RowIndex)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Function GetCodigosSugeridos() As List(Of String)

        GetCodigosSugeridos = Nothing

        Try

            Dim vIdPropietario As Integer = 0

            Try

                vIdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

            Catch ex As Exception
                Throw ex
            End Try

            If vIdPropietario = 0 Then Exit Function

            Return clsLnProducto.Get_Codigos_Sugeridos_By_IdPropietario_And_IdBodega(vIdPropietario, cmbBodega.EditValue)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Function

    Private Sub dgvDetalleFactura_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) Handles dgrid.EditingControlShowing

        Dim Dt As New List(Of String)

        Try

            dgrid.EndEdit()

            If txtCodigoProductoGrid IsNot Nothing Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()

            Dim vProducto As String = ""

            If dgrid.CurrentCell.ColumnIndex = 0 Then
                txtCodigoProductoGrid = TryCast(e.Control, TextBox)
                txtCodigoProductoGrid.BackColor = Color.White
            End If

            vProducto = IIf(IsDBNull(dgrid.Item(0, dgrid.CurrentCell.RowIndex).Value), "", dgrid.Item(0, dgrid.CurrentCell.RowIndex).Value)

            txtCodigoProductoGrid = TryCast(e.Control, TextBox)

            Select Case dgrid.CurrentCell.OwningColumn.Name

                Case "ColCodProducto"

                    If txtCodigoProductoGrid IsNot Nothing Then

                        txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                        txtCodigoProductoGrid.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                        txtCodigoProductoGrid.AutoCompleteSource = AutoCompleteSource.CustomSource

                        Dt = GetCodigosSugeridos()

                        If Not Dt Is Nothing Then
                            txtCodigoProductoGrid.AutoCompleteCustomSource.AddRange(Dt.ToArray())
                        End If

                    Else
                        If Not IsNothing(txtCodigoProductoGrid) Then txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()
                    End If

                Case "colPresentacion"

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerPres

                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerPres

                        AddHandler cboThisComboBox.KeyDown, AddressOf cmbPresentacion_KeyDown

                    End If

                Case "colEstadoProducto"

                    If TypeOf (e.Control) Is DataGridViewComboBoxEditingControl Then

                        Dim cboThisComboBox = DirectCast(e.Control, DataGridViewComboBoxEditingControl)

                        AddHandler cboThisComboBox.SelectedValueChanged, LastEventHandlerEstado

                        AddHandler cboThisComboBox.Leave, AddressOf RemoveValueChangedHandlerEstado

                    End If

                Case Else

                    txtCodigoProductoGrid.AutoCompleteCustomSource.Clear()

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub dgrid_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles dgrid.CellValidating

        Try

            Dim vClienteTiempo As New clsBeCliente_tiempos

            Dim vDiasVencimientoCliente As Integer = 0

            If ContadorFocus = 1 Then
                ContadorFocus = 0
                Exit Sub
            End If

            If Not dgrid.Rows(e.RowIndex) Is Nothing AndAlso Not dgrid.Rows(e.RowIndex).IsNewRow AndAlso dgrid.IsCurrentRowDirty Then

                dgrid.EndEdit()

                Get_ValoresGrid(e.RowIndex)

                If Not IsNothing(pBeProducto) Then
                    If Not IsNothing(pBeProducto.UnidadMedida) Then
                        If Not IsNothing(pBeStock) Then
                            pBeStock.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida
                        End If
                    End If
                End If

                'If pBeProducto Is Nothing Then Exit Sub
                If vCodigoProducto Is Nothing Then vCodigoProducto = ""

                Dim vIndiceDetalleExistente As Integer = 0

                If vNoLinea Is Nothing Then vNoLinea = ""

                Select Case dgrid.Columns(e.ColumnIndex).Name

                    Case "colNo_Linea"

                        If vNoLinea.ToString = "" Then

                            If Modo = TipoTrans.Nuevo Then

                                If pBePedidoDetList.Count = 0 Then
                                    NoLineaCell.Value = 1
                                ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                    NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                                Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                    NoLineaCell.Value = pBePedidoDet.No_linea
                                End If

                            End If

                        End If

                    Case "ColCodProducto"

                        If vCodigoProducto.ToString <> "" Then

                            If Not pBeProducto Is Nothing Then

                                If Not pBeProducto.Codigo Is Nothing Then

                                    vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                                    If pBeProducto.Codigo <> vCodigoProducto Then

                                        If vIndiceDetalleExistente <> -1 Then

                                            '#EJC20171024_1245PM_REF:
                                            '#EJC20171024_1245PM_REF: Si entra aquí significa que cambió el Código del producto después de que ya existía uno antes en la misma línea.
                                            clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                                        pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                                        pBePedidoEnc.IdPickingEnc)

                                            'Limpiar el campo cantidad por si tenía algún valor anteriormente
                                            CantidadCell.Value = 0

                                            'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True
                                            pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                                        End If

                                    End If

                                End If

                            End If

                            If vIdPedidoDet <> 0 Then

                                If Not pBeProducto Is Nothing Then

                                    If Not pBeProducto.Codigo Is Nothing Then

                                        vIndiceDetalleExistente = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = vIdPedidoDet)

                                        If pBeProducto.Codigo <> vCodigoProducto Then

                                            If vIndiceDetalleExistente <> -1 Then

                                                '#EJC20171024_1245PM_REF:
                                                '#EJC20171024_1245PM_REF: Si entra aquí significa que cambió el código del producto después de que ya existía uno antes en la misma línea.
                                                clsLnTrans_pe_enc.Eliminar_Detalle_Pedido(pBePedidoDetList(vIndiceDetalleExistente).IdPedidoEnc,
                                                                                        pBePedidoDetList(vIndiceDetalleExistente).IdPedidoDet,
                                                                                        pBePedidoEnc.IdPickingEnc)

                                                'Limpiar el campo cantidad por si ten?a algún valor anteriormente
                                                CantidadCell.Value = 0

                                                'Eliminar de la lista de detalle para evitar que no se inserte el stock_res
                                                pBePedidoDetList(vIndiceDetalleExistente).IsNew = True

                                            End If

                                        End If

                                    End If

                                Else

                                    Dim vCodigo As String = dgrid.Rows(e.RowIndex).Cells("Codigo").Value
                                    Dim ObjP As clsBeProducto = Nothing

                                    If Not vCodigo Is Nothing Then

                                        ObjP = clsLnProducto.Get_BeProducto_By_Codigo(vCodigo, cmbBodega.EditValue, lcmbPropietario.Tag)

                                        If ObjP Is Nothing Then

                                            dgrid.Rows(e.RowIndex).Cells("Codigo").ErrorText = String.Format("El Código de Producto {0} no existe en Propietario {1} y Bodega {2}.", vCodigo, lcmbPropietario.Text, cmbBodega.Text)

                                        End If

                                    End If

                                End If


                            End If

                            If Not IsNothing(pBeProducto) Then
                                pBeProducto.Codigo = CodProductoCell.Value
                            End If

                            Dim vIdStockEspecifico As Integer = 0

                            If vIndiceDetalleExistente <> -1 Then
                                Try
                                    If Not pBePedidoDetList(vIndiceDetalleExistente) Is Nothing Then
                                        vIdStockEspecifico = pBePedidoDetList(vIndiceDetalleExistente).IdStockEspecifico
                                        'vIdStockEspecifico = IIf(IsDBNull(dgrid.Item("IdStockEspecifico", vIndiceDetalleExistente).Value), 0, dgrid.Item("IdStockEspecifico", vIndiceDetalleExistente).Value)
                                    End If
                                Catch ex As Exception

                                End Try
                            End If

                            If vIdStockEspecifico > 0 Then
                                Exit Sub
                            End If

                            If Not IsNothing(pBeProducto) Then
                                '#CKFK 20210226 Modifiqué esta línea para llamar a la que busca el producto también por propietario
                                'pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue)
                                pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue, clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, lcmbPropietario.EditValue))

                            Else
                                pBeProducto = New clsBeProducto
                                pBeProducto.Codigo = CodProductoCell.Value
                                '#CKFK 20210226 Modifiqué esta línea para llamar a la que busca el producto también por propietario
                                'pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue)
                                pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pBeProducto.Codigo, cmbBodega.EditValue, clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, lcmbPropietario.EditValue))

                            End If

                            If Not IsNothing(pBeProducto) Then

                                If Not pBeProducto.Kit Then

                                    If Not pClienteTiemposList Is Nothing Then

                                        vClienteTiempo = New clsBeCliente_tiempos

                                        If Not pClienteTiemposList Is Nothing Then
                                            vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                                                x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                                                                And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                                        End If

                                        vDiasVencimientoCliente = 0

                                        If Not vClienteTiempo Is Nothing Then
                                            If chkPedidoLocal.Checked Then
                                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                                            Else
                                                vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                                            End If
                                        End If
                                    End If


                                    dgrid.Item("colIdProductoBodega", e.RowIndex).Value = pBeProducto.IdProductoBodega

                                    dgrid.Item("ColPeso", e.RowIndex).ReadOnly = Not pBeProducto.Control_peso
                                    dgrid.Item("ColIdProducto", e.RowIndex).Value = pBeProducto.IdProducto
                                    dgrid.Item("ColNomProducto", e.RowIndex).Value = pBeProducto.Nombre

                                    Llena_Presentacion_Grid(e.RowIndex)

                                    If Not DgComboPresentacion.Value Is Nothing Then
                                        dgrid.Rows(e.RowIndex).Cells("colPresentacion").Value = DgComboPresentacion.Value
                                    End If

                                    dgrid.Item("colUnidadMedida", e.RowIndex).Value = pBeProducto.UnidadMedida.Nombre
                                    dgrid.Item("colPrecio", e.RowIndex).Value = pBeProducto.Precio


                                    '#GT08042025: enviar el estado del documento si tuviera uno por default que coincida con el mismo propietario
                                    Dim pEstadoProductoDefault As Integer = -1
                                    If lcmbPropietario.EditValue = BeTipoDoc.IdPropietario AndAlso BeTipoDoc.IdProductoEstado Then
                                        pEstadoProductoDefault = BeTipoDoc.IdProductoEstado
                                    End If

                                    '#GT09042025: validar que si el tipo documento tiene un estado, filtrar cuando se digita un codigo de producto.
                                    'podria darse el caso que no exista dicho estado pero si los demas valores (del stock consultado)
                                    Llena_Estados_Grid(e.RowIndex, pEstadoProductoDefault)

                                    If vNoLinea Is Nothing Then vNoLinea = ""

                                    If vNoLinea.ToString = "" OrElse Val(vNoLinea) = 0 Then

                                        'If Modo = TipoTrans.Nuevo Then

                                        If pBePedidoDetList.Count = 0 Then
                                            NoLineaCell.Value = 1
                                        ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                            NoLineaCell.Value = pBePedidoDetList.Max(Function(x) x.No_linea) + 1
                                        Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                            NoLineaCell.Value = pBePedidoDet.No_linea
                                        End If

                                        'End If

                                    End If

                                    '#GT10042025: validar que hay un estado en el grid, porque si se maneja Idestadodefault del tipo pedido, podria afectar.
                                    pBeStock.ProductoEstado = New clsBeProducto_estado
                                    Dim vEstadoProducto As Integer = -1

                                    vEstadoProducto = Convert.ToInt32(dgrid.Rows(e.RowIndex).Cells("colEstadoProducto").Value)
                                    If vEstadoProducto = -1 Then
                                        dgrid.Rows(e.RowIndex).Cells("colEstadoProducto").ErrorText = String.Format("No se encontro producto con el estado {0}, no existe en Propietario  {1} y Bodega {2}.", pEstadoProductoDefault, lcmbPropietario.Text, cmbBodega.Text)
                                    Else
                                        pBeStock.ProductoEstado.IdEstado = vEstadoProducto
                                        pBeStock.IdProductoEstado = pBeStock.ProductoEstado.IdEstado
                                    End If

                                    pBeStock.Presentacion = New clsBeProducto_Presentacion
                                    pBeStock.IdPresentacion = Convert.ToInt32(dgrid.Rows(e.RowIndex).Cells("colPresentacion").Value)
                                    pBeStock.Presentacion.IdPresentacion = pBeStock.IdPresentacion
                                    pBeStock.Presentacion = clsLnProducto_presentacion.GetSingle(pBeStock.Presentacion.IdPresentacion)

                                    If Not IsNothing(pBeProducto.UnidadMedida) Then
                                        pBeStock.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida
                                    End If

                                    If pBeProducto.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                                        pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega

                                        '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                                        pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                                        clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                                 cmbBodega.EditValue,
                                                                                 True,
                                                                                 False,
                                                                                 vDiasVencimientoCliente)

                                        If Not pBeStock.Presentacion Is Nothing Then

                                            dgrid.Rows(e.RowIndex).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                            dgrid.Rows(e.RowIndex).Cells("colCantidadExistencia").Tag = pBeStock.Cantidad
                                            dgrid.Rows(e.RowIndex).Cells("colPesoExistencia").Value = pBeStock.Peso
                                            dgrid.Rows(e.RowIndex).Cells("colPesoUnitario").Value = IIf(pBeStock.Cantidad > 0, pBeStock.Peso / pBeStock.Cantidad, 0)

                                        Else

                                            dgrid.Rows(e.RowIndex).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                            dgrid.Rows(e.RowIndex).Cells("colPesoExistencia").Value = pBeStock.Peso

                                            If pBeStock.Cantidad > 0 Then
                                                dgrid.Rows(e.RowIndex).Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                                            Else
                                                dgrid.Rows(e.RowIndex).Cells("colPesoUnitario").Value = 0
                                            End If

                                        End If

                                    Else
                                        dgrid.Rows(e.RowIndex).Cells("colCantidadExistencia").Value = 0
                                        dgrid.Rows(e.RowIndex).Cells("colPesoExistencia").Value = 0
                                    End If
                                Else

                                    Set_Producto_Padre_Kit(pBeProducto, e.RowIndex)
                                    Set_Productos_Hijos_Kit(pBeProducto, e.RowIndex, pBeProducto.Codigo)

                                End If

                            Else

                                Dim vCodigo As String = dgrid.Rows(e.RowIndex).Cells("ColCodProducto").Value

                                dgrid.Rows(e.RowIndex).Cells("ColCodProducto").ErrorText = String.Format("El Código de Producto {0} no existe en Propietario  {1} y Bodega {2}.", vCodigo, lcmbPropietario.Text, cmbBodega.Text)


                            End If

                            'MsgBox("Validatin: " & TiempoS)

                            ContadorFocus = 0

                            'dgrid.CurrentCell = dgrid.Item("ColCantidad", e.RowIndex)

                        End If

                    Case "ColCantidad" 'Cantidad

                        Dim vPeso As Double = IIf(IsDBNull(dgrid.Rows(e.RowIndex).Cells("colPesoUnitario").Value), 0, Val(dgrid.Rows(e.RowIndex).Cells("colPesoUnitario").Value))
                        Dim vCantidad As Double = IIf(IsDBNull(dgrid.Rows(e.RowIndex).Cells("colCantidad").Value), 0, Val(dgrid.Rows(e.RowIndex).Cells("colCantidad").Value))

                        dgrid.Rows(e.RowIndex).Cells("colPeso").Value = vPeso * vCantidad

                    Case "ColPeso" 'Peso de producto en factura manual

                    Case "colEstadoProducto"

                        If Not pClienteTiemposList Is Nothing Then
                            vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                        x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                                        And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                        End If

                        vDiasVencimientoCliente = 0

                        If Not vClienteTiempo Is Nothing Then
                            If chkPedidoLocal.Checked Then
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                            Else
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                            End If
                        End If

                        If pBeProducto Is Nothing Then
                            pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(dgrid.Rows(e.RowIndex).Cells("ColCodProducto").Value, AP.IdBodega)
                        End If

                        If Not pBeProducto.Kit Then
                            'CM_20180919_1106AM: Al seleccionar el estado del producto muestra la cantidad en existencia del producto.
                            pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega
                            pBeStock.ProductoEstado.IdEstado = Convert.ToInt32(dgrid.Rows(e.RowIndex).Cells("colEstadoProducto").Value)
                            If pBeStock.Presentacion Is Nothing Then pBeStock.Presentacion = New clsBeProducto_Presentacion
                            pBeStock.Presentacion.IdPresentacion = Convert.ToInt32(dgrid.Rows(e.RowIndex).Cells("colPresentacion").Value)
                            pBeStock.IdPresentacion = pBeStock.Presentacion.IdPresentacion

                            '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                            pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                            clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                         cmbBodega.EditValue,
                                                                         True,
                                                                         False,
                                                                         vDiasVencimientoCliente)

                            dgrid.Rows(e.RowIndex).Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                        End If

                    Case 4 'Precio de producto en factura manual

                    Case 5 'Total de línea

                    Case Else
                        Exit Select
                End Select

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Presentacion_Grid(ByVal pIndex As Integer,
                                        Optional ByVal pIdPresentacion As Integer = 0)

        Try

            DgComboPresentacion = TryCast(dgrid.Rows(pIndex).Cells("colPresentacion"), DataGridViewComboBoxCell)
            DgComboPresentacion.DropDownWidth = 200

            Dim lPres As New List(Of clsBeProducto_Presentacion)

            If Modo = TipoTrans.Nuevo Then
                'GT07022022: por razón que no conozco ahora, el producto importado del stock especifico a veces viene vacio! 
                If pBeProducto Is Nothing Then
                    Throw New Exception(String.Format("Error_07022022: No se puede cargar la presentación, el producto viene vacio"))
                End If

                lPres = clsLnProducto_presentacion.Get_All_Presentacion_By_IdProductoBodega(pBeProducto.IdProductoBodega).ToList()
            Else
                lPres = clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProductoBodega(pBeProducto.IdProductoBodega, True).ToList()
            End If

            DgComboPresentacion.DataSource = lPres
            DgComboPresentacion.ValueMember = "IdPresentacion"
            DgComboPresentacion.DisplayMember = "Nombre"

            If DgComboPresentacion.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
                Dim vIdPresentacion As Integer = lPres(0).IdPresentacion
                DgComboPresentacion.Value = vIdPresentacion
                If Not DgComboPresentacion.Value = lPres(0).IdPresentacion Then
                    DgComboPresentacion.Value = Nothing
                End If
                '#CKFK20221029 puse en comentario este exit sub
                'Exit Sub
            Else
                DgComboPresentacion.Value = Nothing
            End If

            If pIdPresentacion <> 0 AndAlso DgComboPresentacion.Items.Count > 0 Then
                DgComboPresentacion.Value = pIdPresentacion
            Else
                DgComboPresentacion.Value = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Presentacion_Grid(ByVal pIndex As Integer,
                                        lPresentacionesByPedido As List(Of clsBeProducto_Presentacion),
                                        Optional ByVal pIdPresentacion As Integer = 0)

        Try

            DgComboPresentacion = TryCast(dgrid.Rows(pIndex).Cells("colPresentacion"), DataGridViewComboBoxCell)
            DgComboPresentacion.DropDownWidth = 200

            Dim lPres = lPresentacionesByPedido.FindAll(Function(x) x.IdProducto = pBeProducto.IdProducto AndAlso x.Activo = True)

            DgComboPresentacion.DataSource = lPres
            DgComboPresentacion.ValueMember = "IdPresentacion"
            DgComboPresentacion.DisplayMember = "Nombre"

            If DgComboPresentacion.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
                Dim vIdPresentacion As Integer = lPres(0).IdPresentacion
                DgComboPresentacion.Value = vIdPresentacion
                If Not DgComboPresentacion.Value = lPres(0).IdPresentacion Then
                    DgComboPresentacion.Value = Nothing
                End If
                Exit Sub
            Else
                DgComboPresentacion.Value = Nothing
            End If

            If pIdPresentacion <> 0 AndAlso DgComboPresentacion.Items.Count > 0 Then
                DgComboPresentacion.Value = pIdPresentacion
            Else
                DgComboPresentacion.Value = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Cliente_Grid(ByVal pIndex As Integer,
                                   lClientes As List(Of clsBeCliente),
                                   Optional ByVal pIdCliente As Integer = 0)

        Try

            DgComboCliente = TryCast(dgrid.Rows(pIndex).Cells("IdCliente"), DataGridViewComboBoxCell)
            DgComboCliente.DropDownWidth = 200

            Dim lCliente As New List(Of clsBeCliente)

            lCliente = New List(Of clsBeCliente)

            If Modo = TipoTrans.Nuevo Then
                lCliente = lClientes
            Else
                lCliente = lCliente.FindAll(Function(x) x.IdCliente = pIdCliente)
            End If

            DgComboCliente.DataSource = lCliente
            DgComboCliente.ValueMember = "IdCliente"
            DgComboCliente.DisplayMember = "nombre_comercial"

            If DgComboCliente.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
                Dim vIdCliente As Integer = lCliente(0).IdCliente
                DgComboCliente.Value = vIdCliente
                If Not DgComboCliente.Value = lCliente(0).IdCliente Then
                    DgComboCliente.Value = Nothing
                End If
                Exit Sub
            Else
                DgComboCliente.Value = Nothing
            End If

            If pIdCliente <> 0 AndAlso DgComboCliente.Items.Count > 0 Then
                DgComboCliente.Value = pIdCliente
            Else
                DgComboCliente.Value = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Presentacion_Grid_ProductoKit(ByVal pIndex As Integer, ByVal lPres As List(Of clsBeProducto_Presentacion), Optional ByVal pIdPresentacion As Integer = 0)

        Try

            DgComboPresentacion = TryCast(dgrid.Rows(pIndex).Cells("colPresentacion"), DataGridViewComboBoxCell)
            DgComboPresentacion.DropDownWidth = 200

            DgComboPresentacion.DataSource = lPres
            DgComboPresentacion.ValueMember = "IdPresentacion"
            DgComboPresentacion.DisplayMember = "Nombre"

            If DgComboPresentacion.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
                DgComboPresentacion.Value = lPres(0).IdPresentacion
                Exit Sub
            Else
                DgComboPresentacion.Value = Nothing
            End If

            If pIdPresentacion <> 0 AndAlso DgComboPresentacion.Items.Count > 0 Then
                DgComboPresentacion.Value = pIdPresentacion
            Else
                DgComboPresentacion.Value = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Estados_Grid(ByVal pIndex As Integer, Optional ByVal pIdEstado As Integer = 0)

        Try

            DgComboEstado = TryCast(dgrid.Rows(pIndex).Cells("colEstadoProducto"), DataGridViewComboBoxCell)

            DgComboEstado.DropDownWidth = 200

            If Not pBeProducto Is Nothing Then

                Dim lEstado As New List(Of clsBeProducto_estado)

                lEstado = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProductoBodega(pBeProducto.IdProductoBodega, pIdEstado)

                '#GT10042025: si la lista tiene registros...
                If lEstado IsNot Nothing AndAlso lEstado.Count > 0 Then

                    '#GT24042025: si el idEstado existe se filtra, de lo contrario, la lista se retorna completa
                    If pIdEstado > 0 Then
                        lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
                    End If

                    'lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
                    DgComboEstado.DataSource = lEstado
                    DgComboEstado.ValueMember = "IdEstado"
                    DgComboEstado.DisplayMember = "Nombre"

                    If DgComboEstado.Items.Count > 0 Then
                        DgComboEstado.Value = lEstado(0).IdEstado
                    Else
                        '#EJC20171024_1136PM:Corrección para cuando se cambia a un Código de producto que no tiene stock y por lo tanto no tiene estado.
                        DgComboEstado.Value = Nothing
                    End If

                End If

                'If pIdEstado <> 0 Then
                '    lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
                'End If

                'DgComboEstado.DataSource = lEstado
                'DgComboEstado.ValueMember = "IdEstado"
                'DgComboEstado.DisplayMember = "Nombre"

                'If DgComboEstado.Items.Count > 0 Then
                '    DgComboEstado.Value = lEstado(0).IdEstado
                'Else
                '    '#EJC20171024_1136PM:Corrección para cuando se cambia a un Código de producto que no tiene stock y por lo tanto no tiene estado.
                '    DgComboEstado.Value = Nothing
                'End If

                'If pIdEstado <> 0 Then
                '    If DgComboEstado.Items.Count > 0 Then
                '        DgComboEstado.Value = pIdEstado
                '    End If
                'End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Estados_Grid_ProductoKit(ByVal pIndex As Integer, ByVal lEstado As List(Of clsBeProducto_estado), Optional ByVal pIdEstado As Integer = 0)

        Try

            DgComboEstado = TryCast(dgrid.Rows(pIndex).Cells("colEstadoProducto"), DataGridViewComboBoxCell)

            DgComboEstado.DropDownWidth = 200

            If pIdEstado <> 0 Then
                lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
            End If

            DgComboEstado.DataSource = lEstado
            DgComboEstado.ValueMember = "IdEstado"
            DgComboEstado.DisplayMember = "Nombre"

            If DgComboEstado.Items.Count > 0 Then
                DgComboEstado.Value = lEstado(0).IdEstado
            Else
                '#EJC20171024_1136PM:Corrección para cuando se cambia a un Código de producto que no tiene stock y por lo tanto no tiene estado.
                DgComboEstado.Value = Nothing
            End If

            If pIdEstado <> 0 Then
                If DgComboEstado.Items.Count > 0 Then
                    DgComboEstado.Value = pIdEstado
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPresentacion_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            Dim vClienteTiempo As New clsBeCliente_tiempos

            If e.KeyCode = Keys.Back Then

                If Not pClienteTiemposList Is Nothing Then
                    vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                        x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                        And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                End If

                Dim vDiasVencimientoCliente As Integer = 0

                If Not vClienteTiempo Is Nothing Then
                    If chkPedidoLocal.Checked Then
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                    Else
                        vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                    End If
                End If

                Dim DgComboPres As New DataGridViewComboBoxCell
                DgComboPres = TryCast(dgrid.CurrentRow.Cells("colPresentacion"), DataGridViewComboBoxCell)
                DgComboPres.Value = Nothing
                dgrid.Refresh()

                Llena_Estados_Grid(dgrid.CurrentRow.Index)

                Dim DgComboEstado As New DataGridViewComboBoxCell
                DgComboEstado = TryCast(dgrid.CurrentRow.Cells("colEstadoProducto"), DataGridViewComboBoxCell)
                pBeStock.IdProductoEstado = DgComboEstado.Value


                pBeStock.ProductoEstado.IdEstado = pBeStock.IdProductoEstado
                pBeStock.IdPresentacion = 0
                pBeStock.Presentacion.IdPresentacion = 0

                XtraMessageBox.Show("Se quitó la presentación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                '#EJC20180410: Mostrar existencia en UMBas si se quitó presentación
                clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                             pBePedidoEnc.IdBodega,
                                                             True,
                                                             False,
                                                             vDiasVencimientoCliente)

                dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                dgrid.CurrentRow.Cells("colPesoExistencia").Value = pBeStock.Peso

                If pBeStock.Cantidad > 0 Then
                    dgrid.CurrentRow.Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                Else
                    dgrid.CurrentRow.Cells("colPesoUnitario").Value = 0
                End If

                'dgrid.EndEdit()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmbPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim combo As DataGridViewComboBoxEditingControl = TryCast(sender, DataGridViewComboBoxEditingControl)

            Dim vClienteTiempo As New clsBeCliente_tiempos

            If Not pClienteTiemposList Is Nothing Then
                vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                And x.IdFamilia = pBeProducto.Familia.IdFamilia)
            End If

            Dim vDiasVencimientoCliente As Integer = 0

            If Not vClienteTiempo Is Nothing Then
                If chkPedidoLocal.Checked Then
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                Else
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                End If
            End If

            Dim IdPresentacion As Integer

            If (combo.SelectedItem IsNot Nothing) Then

                If Integer.TryParse(combo.SelectedValue, IdPresentacion) Then

                    If pBeStock.Presentacion Is Nothing Then
                        pBeStock.Presentacion = New clsBeProducto_Presentacion
                    End If

                    pBeStock.Presentacion.IdPresentacion = IdPresentacion
                    pBeStock.IdPresentacion = IdPresentacion

                    If pBeStock.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                        pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega
                        '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                        pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                        clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                     cmbBodega.EditValue,
                                                                     True,
                                                                     False,
                                                                     vDiasVencimientoCliente)

                        dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                        dgrid.CurrentRow.Cells("colPesoExistencia").Value = pBeStock.Peso

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim combo As DataGridViewComboBoxEditingControl = TryCast(sender, DataGridViewComboBoxEditingControl)

            Dim IdEstado As Integer

            If (combo.SelectedItem IsNot Nothing) Then

                If Integer.TryParse(combo.SelectedValue, IdEstado) Then

                    If Not pBeProducto Is Nothing Then
                        pBeStockRes.IdProductoBodega = pBeProducto.IdProductoBodega
                    Else
                        pBeStockRes.IdProductoBodega = dgrid.CurrentRow.Cells("colIdProductoBodega").Value
                    End If

                    pBeStock.ProductoEstado.IdEstado = IdEstado
                    pBeStock.IdProductoEstado = IdEstado

                    If pBeStock.IdProductoBodega <> 0 AndAlso pBeStock.ProductoEstado.IdEstado <> 0 Then

                        If Not pBeProducto Is Nothing Then
                            pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega
                        Else
                            pBeStock.IdProductoBodega = dgrid.CurrentRow.Cells("colIdProductoBodega").Value
                        End If

                        'pBeStock.IdProductoBodega = pBeProducto.IdProductoBodega

                        If Not pBeProducto.Kit Then

                            '#EJC20220720_1357:Abastecer desde ubicación específica de cliente.
                            pBeStock.IdUbicacion = Val(txtIdUbicacionAbastecimiento.Text)

                            clsLnStock.Get_Existencia_Disp_By_IdProducto(pBeStock,
                                                                         pBePedidoEnc.IdBodega,
                                                                         True,
                                                                         False,
                                                                         vNoDiasVencimiento)

                            If Not pBeStock.Presentacion Is Nothing Then

                                If pBeStock.Presentacion.IdPresentacion <> 0 Then

                                    If pBeStock.Presentacion.EsPallet Then
                                        Dim vCantidadPallets As Double = pBeStock.Cantidad / (pBeStock.Presentacion.Factor * pBeStock.Presentacion.CajasPorCama * pBeStock.Presentacion.CamasPorTarima)
                                        dgrid.CurrentRow.Cells("colCantidadExistencia").Value = vCantidadPallets
                                        dgrid.CurrentRow.Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPallets) * vCantidadPallets
                                        dgrid.CurrentRow.Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPallets)
                                    Else
                                        Dim vCantidadPresentacion As Double = pBeStock.Cantidad / pBeStock.Presentacion.Factor
                                        dgrid.CurrentRow.Cells("colCantidadExistencia").Value = vCantidadPresentacion
                                        dgrid.CurrentRow.Cells("colPesoExistencia").Value = (pBeStock.Peso / vCantidadPresentacion) * vCantidadPresentacion
                                        dgrid.CurrentRow.Cells("colPesoUnitario").Value = (pBeStock.Peso / vCantidadPresentacion)
                                    End If

                                Else
                                    dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                    dgrid.CurrentRow.Cells("colPesoExistencia").Value = pBeStock.Peso
                                    dgrid.CurrentRow.Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                                End If

                            Else

                                dgrid.CurrentRow.Cells("colCantidadExistencia").Value = pBeStock.Cantidad
                                dgrid.CurrentRow.Cells("colPesoExistencia").Value = pBeStock.Peso

                                dgrid.CurrentRow.Cells("colIdProductoBodega").Value = pBeStock.IdProductoBodega

                                If pBeStock.Cantidad > 0 Then
                                    dgrid.CurrentRow.Cells("colPesoUnitario").Value = pBeStock.Peso / pBeStock.Cantidad
                                Else
                                    dgrid.CurrentRow.Cells("colPesoUnitario").Value = 0
                                End If

                            End If

                        Else

                            Set_Producto_Padre_Kit(pBeProducto, dgrid.CurrentRow.Index)
                            Set_Productos_Hijos_Kit(pBeProducto, dgrid.CurrentRow.Index, pBeProducto.Codigo)

                        End If

                        dgrid.EndEdit()

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Get_ValoresGrid(ByVal IndiceFila As Integer)

        If IsGettingValoresGrid Then Exit Sub

        IsGettingValoresGrid = True

        UnaExtrañaCondicionDeEdicionEnLInea = False


        Try

            dgrid.EndEdit()

            Dim row As DataGridViewRow = dgrid.Rows(IndiceFila)
            NoLineaCell = row.Cells(dgrid.Columns("ColNo_Linea").Index)
            CodProductoCell = row.Cells(dgrid.Columns("colCodProducto").Index)
            NomProductoCell = row.Cells(dgrid.Columns("ColNomProducto").Index)
            CantidadCell = row.Cells(dgrid.Columns("ColCantidad").Index)
            PesoCell = row.Cells(dgrid.Columns("ColPeso").Index)
            PrecioCell = row.Cells(dgrid.Columns("ColPrecio").Index)
            TotalCell = row.Cells(dgrid.Columns("ColTotal").Index)
            CantDisp = row.Cells(dgrid.Columns("colCantidadExistencia").Index)
            EstadoCell = row.Cells(dgrid.Columns("colEstadoProducto").Index)
            PresentacionCell = row.Cells(dgrid.Columns("colPresentacion").Index)
            ClienteCell = row.Cells(dgrid.Columns("IdCliente").Index)
            IdProductoBodegaCell = row.Cells(dgrid.Columns("colIdProductoBodega").Index)

            DgComboPresentacion = TryCast(PresentacionCell, DataGridViewComboBoxCell)
            DgComboCliente = TryCast(ClienteCell, DataGridViewComboBoxCell)

            UnidadMedidaCell = row.Cells(dgrid.Columns("colUnidadMedida").Index)
            IdPedidoDetCell = row.Cells(dgrid.Columns("colIdPedidoDet").Index)
            NoDiasVencimientoCell = row.Cells(dgrid.Columns("colNoDias").Index)
            IdProductoCell = row.Cells(dgrid.Columns("colIdProducto").Index)
            SerieProductoCell = row.Cells(dgrid.Columns("colNoSerie").Index)

            vNoLinea = (IIf(IsDBNull(NoLineaCell.Value), "0", NoLineaCell.Value))
            vCodigoProducto = (IIf(IsDBNull(CodProductoCell.Value), "", CodProductoCell.Value))
            vNomProducto = IIf(IsDBNull(NomProductoCell.Value), "", NomProductoCell.Value)
            vCantidad = Val(IIf(IsDBNull(CantidadCell.Value), "0", CantidadCell.Value))
            vPeso = Val(IIf(IsDBNull(PesoCell.Value), "0", PesoCell.Value))
            vPrecio = Val(IIf(IsDBNull(PrecioCell.Value), "0", PrecioCell.Value))
            vTotal = Val(IIf(IsDBNull(TotalCell.Value), "0", TotalCell.Value))
            vCantidadDisponible = Val(IIf(IsDBNull(CantDisp.Value), "0", CantDisp.Value))
            vIdPresentacion = Val(IIf(IsDBNull(PresentacionCell.Value), "0", PresentacionCell.Value))
            vIdEstado = Val(IIf(IsDBNull(EstadoCell.Value), "0", EstadoCell.Value))
            vIdProductoBodega = Val(IIf(IsDBNull(IdProductoBodegaCell.Value), "0", IdProductoBodegaCell.Value))

            Try
                vNomPresentacion = PresentacionCell.FormattedValue
            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            vNomUnidadMedida = UnidadMedidaCell.FormattedValue

            Try
                vNomEstado = EstadoCell.FormattedValue
            Catch ex As Exception
            End Try

            vIdPedidoDet = IIf(IsDBNull(IdPedidoDetCell.Value), 0, IdPedidoDetCell.Value)
            vNoDiasVencimiento = IIf(IsDBNull(NoDiasVencimientoCell.Value), 0, NoDiasVencimientoCell.Value)
            vIdProducto = IIf(IsDBNull(IdProductoCell.Value), 0, IdProductoCell.Value)
            vNoSerie = IIf(IsDBNull(SerieProductoCell.Value), 0, SerieProductoCell.Value)

            '#CK_CM_20191709_1:03PM: Agregamos validación a pBeStock para cuando sea nothing
            If pBeStock IsNot Nothing Then
                pBeStock.IdProductoBodega = vIdProductoBodega
            End If

            If Not pBeProducto Is Nothing AndAlso vIdProductoBodega <> 0 Then
                pBeProducto.IdProductoBodega = vIdProductoBodega
            Else

                pBeProducto = New clsBeProducto

                If Not vCodigoProducto Is Nothing Then

                    pBeProducto.Codigo = vCodigoProducto

                    Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue,
                                                                                        lcmbPropietario.EditValue)

                    pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo_And_IdPropietario(pBeProducto.Codigo,
                                                                                           cmbBodega.EditValue,
                                                                                           vIdPropietario)


                    If Not pBeProducto Is Nothing Then
                        '#EJC20191205: Corrección por problema de rendimiento en grid de pedido.
                        IdProductoBodegaCell.Value = pBeProducto.IdProductoBodega
                    End If

                Else

                    '#EJC20191111
                    Debug.Print("Hit not nothing in Get_BeProducto_By_Codigo")
                    Exit Sub

                End If

            End If

            If vNoLinea Is Nothing AndAlso Not (vIdPedidoDet = 0) Then
                vNoLinea = vIdPedidoDet
            End If

            If Not pBeProducto Is Nothing Then

                If Not pBeProducto.Kit Then
                    '#EJC20200109: Cuando se regresa a una línea y se modificaba la cantidad se quedaba el idpedidodet anterior.
                    If Not (vIdPedidoDet = 0) Then

                        If pBePedidoDet.IdPedidoDet <> vIdPedidoDet Then

                            'MsgBox("Movimiento sospechoso detectado", MsgBoxStyle.Exclamation, "Verifique el stock reservado")
                            'pBePedidoDet.IdPedidoDet = vIdPedidoDet

                            IdxLineaEditando = pBePedidoDetList.FindIndex(Function(x) x.Codigo_Producto = vCodigoProducto AndAlso x.No_linea = vNoLinea)

                            If IdxLineaEditando <> -1 Then
                                UnaExtrañaCondicionDeEdicionEnLInea = False
                                'vIdPedidoDet = pBePedidoDetList.Find(Function(x) x.Codigo_Producto = vCodigoProducto AndAlso x.No_linea = vNoLinea).IdPedidoDet
                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsGettingValoresGrid = False
        End Try

    End Sub

    Private Sub dgrid_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgrid.RowValidating

        'válido
        Dim ControlPeso As Boolean = False

        Try

            If Not dgrid.Rows(e.RowIndex) Is Nothing AndAlso Not dgrid.Rows(e.RowIndex).IsNewRow AndAlso dgrid.IsCurrentRowDirty Then

                Get_ValoresGrid(e.RowIndex)

                Propietario.IdPropietario = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

                If vCodigoProducto <> "" Then

                    Dim vControPeso As Boolean = False

                    If Not pBeProducto Is Nothing Then
                        vControPeso = pBeProducto.Control_peso
                        EsKit = pBeProducto.Kit
                    End If

                    If vNomProducto = "" Then
                        CodProductoCell.ErrorText = "Código de producto no válido"
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = "Código de producto no válido"
                        e.Cancel = True
                    ElseIf vCantidad <= 0 Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = "Ingrese cantidad > 0"
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = "Ingrese cantidad > 0"
                        e.Cancel = True
                    ElseIf vPeso = 0 AndAlso vControPeso Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        PesoCell.ErrorText = "Ingrese peso > 0"
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = "Ingrese peso > 0"
                        e.Cancel = True
                    ElseIf Not IsNumeric(vCantidad) AndAlso (dgrid.Item(0, e.RowIndex).Value <> "") Then
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = "Ingrese valor numerico"
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = "Ingrese valor numerico"
                        e.Cancel = True
                    ElseIf Val(vCantidad) > Math.Round(vCantidadDisponible, 6) Then 'Se vende por lote
                        Dim result As String = String.Format("La cantidad ingresada: {0} es mayor que la cantidad disponible: {1} ", vCantidad, vCantidadDisponible)
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = result
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = result
                        e.Cancel = True
                    ElseIf EsKit AndAlso vCantidad < 1 Then
                        Dim result As String = String.Format("La cantidad ingresada: {0} debe ser unitaria no se puede reservar número decimales: {1} ", vCantidad, vCantidadDisponible)
                        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                        CantidadCell.ErrorText = result
                        dgrid.Rows(CantidadCell.RowIndex).ErrorText = result
                        e.Cancel = True
                    Else

                        LimpiarMensajesErrorCeldas()

                        If Not EsKit Then

                            If Modo = TipoTrans.Nuevo Then

                                If pBePedidoDetList.Count = 0 Then
                                    vIdPedidoDet = 1
                                ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                                    '#EJC20210209: Lo cambio a IdPedidoDet antes NoLinea, evaluar....
                                    vIdPedidoDet = pBePedidoDetList.Max(Function(x) x.IdPedidoDet) + 1
                                Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!

                                    vIdPedidoDet = pBePedidoDet.IdPedidoDet
                                End If

                            End If

                            If Reservar_Stock_Por_Linea(e.RowIndex, Propietario.IdPropietario) Then

                                '#EJC20171021_0308PM: Si se reserva el stock correctamente asociar el IdPedidoDet al grid para evitar insertar nuevamente.
                                dgrid.Rows(e.RowIndex).Cells("colIdPedidoDet").Value = pBePedidoDet.IdPedidoDet
                                '#CM_CK_20180917_12:59PM: Inicializamos las clases pBeProducto y pBeStock porque se quedaba con información anterior 
                                pBeStock = New clsBeStock
                                pBeProducto = Nothing
                                vIdPedidoDet = pBePedidoDet.IdPedidoDet

                            Else
                                Dim result As String = String.Format("Error_202302222218: La cantidad ingresada: {0} es mayor que la cantidad disponible: {1} ", vCantidad, vCantidadDisponible)
                                dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
                                dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
                                dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
                                dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
                                CantidadCell.ErrorText = result
                                dgrid.Rows(CantidadCell.RowIndex).ErrorText = result

                                If pBePedidoEnc.IsNew Then
                                    e.Cancel = True
                                Else
                                    e.Cancel = False
                                End If

                                XtraMessageBox.Show("No se pudo reservar el stock por línea", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            End If

                        Else

                            If Agrega_Producto_Kit_Padre_A_Detalle_Pedido(e.RowIndex) Then

                                If Reservar_Stock_Producto_Kit(e.RowIndex, vIdPedidoDetPadre, Propietario.IdPropietario) Then

                                    '#EJC20171021_0308PM: Si se reserva el stock correctamente asociar el IdPedidoDet al grid para evitar insertar nuevamente.
                                    dgrid.Rows(e.RowIndex).Cells("colIdPedidoDet").Value = vIdPedidoDetPadre
                                    pBeStock = New clsBeStock
                                    pBeProducto = Nothing
                                    vIdPedidoDet = vIdPedidoDetPadre

                                End If

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub LimpiarMensajesErrorCeldas()

        dgrid.Rows(CodProductoCell.RowIndex).ErrorText = ""
        CodProductoCell.ErrorText = ""

        dgrid.Rows(CantidadCell.RowIndex).ErrorText = ""
        CantidadCell.ErrorText = ""

        dgrid.Rows(PesoCell.RowIndex).ErrorText = ""
        PesoCell.ErrorText = ""

        dgrid.Rows(PrecioCell.RowIndex).ErrorText = ""
        PrecioCell.ErrorText = ""

        dgrid.Rows(TotalCell.RowIndex).ErrorText = ""
        TotalCell.ErrorText = ""

    End Sub

    Private Function Reservar_Stock_Por_Linea(ByVal IndiceLinea As Integer,
                                              ByVal pIdPropietario As Integer,
                                              Optional ByRef MostrarMensajeReserva As Boolean = True) As Boolean

        Reservar_Stock_Por_Linea = False

        Dim clsTrans As New clsTransaccion

        Try

            If UnaExtrañaCondicionDeEdicionEnLInea Then
                vIdPedidoDet = pBePedidoDetList(IdxLineaEditando).IdPedidoDet
            End If

            'CM_20200121: Puse esta condición para exitar que cuando sea eliminada una línea por stock especifico no vuelva a reservar la misma cantidad.
            If Not dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value Is Nothing Then
                '#EJC20200125: Te falto validar si la cantidad es igual, porque si se modific? se debe entrar a reservar_stock again.
                If pBePedidoDetList.Exists(Function(x) x.IdPedidoDet = vIdPedidoDet AndAlso x.Cantidad = vCantidad AndAlso x.Cant_despachada = 0) Then
                    Exit Function
                End If
            End If

            Dim vClienteTiempo As New clsBeCliente_tiempos

            If Not pClienteTiemposList Is Nothing Then
                vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                And x.IdFamilia = pBeProducto.Familia.IdFamilia)
            End If

            Dim vDiasVencimientoCliente As Integer = 0

            If Not vClienteTiempo Is Nothing Then
                If chkPedidoLocal.Checked Then
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                Else
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                End If
            End If

            If Not NoDiasVencimientoCell Is Nothing Then
                NoDiasVencimientoCell.Value = vDiasVencimientoCliente
            End If

            Dim BePedidoDetExistente As New clsBeTrans_pe_det
            BePedidoDetExistente = clsLnTrans_pe_det.Get_Single_By_IdPedidoEnc_And_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                               vIdPedidoDet,
                                                                                               clsTrans.lConnection,
                                                                                               clsTrans.lTransaction)

            Dim vCantidadPendiente As Double = 0

            If Not BePedidoDetExistente Is Nothing Then
                vCantidadPendiente = BePedidoDetExistente.Cantidad - BePedidoDetExistente.Cant_despachada
                If vCantidadPendiente = 0 Then
                    Throw New Exception("No se puede despachar más de lo solicitado en el pedido originalmente. Cantidad solicitada originalmente: " & BePedidoDetExistente.Cantidad.ToString() &
                            ", Cantidad pendiente: " & vCantidadPendiente.ToString() & ", Cantidad nueva solicitada: " & vCantidad.ToString())
                Else
                    If vCantidadPendiente > BePedidoDetExistente.Cantidad Then
                        Throw New Exception("No se puede despachar más de lo solicitado en el pedido originalmente. Cantidad solicitada originalmente: " & BePedidoDetExistente.Cantidad.ToString() &
                                ", Cantidad pendiente: " & vCantidadPendiente.ToString() & ", Cantidad nueva solicitada: " & vCantidad.ToString())
                    End If
                End If
            End If


            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.No_linea = vNoLinea
            pBePedidoDet.IdPedidoDet = vIdPedidoDet
            pBePedidoDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProductoBodega = pBeStock.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pBeStock.IdProductoBodega
            pBePedidoDet.Producto.Codigo = vCodigoProducto
            pBePedidoDet.IdPresentacion = pBeStock.IdPresentacion
            pBePedidoDet.IdUnidadMedidaBasica = pBeStock.IdUnidadMedida
            pBePedidoDet.Cantidad = vCantidad
            pBePedidoDet.Peso = vPeso
            pBePedidoDet.Precio = vPrecio
            pBePedidoDet.No_recepcion = pBeStock.IdRecepcionEnc
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdPresentacion = vIdPresentacion
            pBePedidoDet.IdEstado = vIdEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Nom_estado = vNomEstado
            pBePedidoDet.IsNew = IIf(vIdPedidoDet = 0, True, False)
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = AP.UsuarioAp.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.Precio = vPrecio
            pBePedidoDet.RoadPrecioDoc = vPrecio
            pBePedidoDet.RoadTotal = vTotal
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Codigo_Producto = vCodigoProducto
            pBePedidoDet.Nombre_producto = vNomProducto
            pBePedidoDet.Nom_presentacion = vNomPresentacion
            pBePedidoDet.Nom_unid_med = vNomUnidadMedida
            pBePedidoDet.Nom_estado = vNomEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Producto.IdProducto = vIdProducto
            pBePedidoDet.Producto.Nombre = vNomProducto
            pBePedidoDet.IdProductoBodega = pBeProducto.IdProductoBodega
            pBePedidoDet.IdStockEspecifico = pBeStock.IdStock

            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pBePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED" 'Pedido #CKFK20220704 Se quito PE y se puso PED
            pBeStockRes.añada = pBeStock.Añada

            '#RESERVAR EL DIFERENCIAL, EJC 202410080141AM.
            If vCantidadPendiente > 0 Then
                pBeStockRes.Cantidad = vCantidadPendiente
            Else
                pBeStockRes.Cantidad = vCantidad
            End If

            pBeStockRes.Peso = vPeso
            pBeStockRes.Estado = "PPC" 'Producto pedido por cliente
            pBeStockRes.User_agr = AP.UsuarioAp.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = AP.UsuarioAp.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = AP.HostName
            pBeStockRes.IdProductoEstado = 0
            pBeStockRes.IdPresentacion = vIdPresentacion
            pBeStockRes.IdProductoEstado = vIdEstado
            pBeStockRes.IdPedido = pBePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = vIdPedidoDet
            pBeStockRes.IdProductoBodega = pBeProducto.IdProductoBodega
            pBeStockRes.IdPropietarioBodega = lcmbPropietario.EditValue
            pBeStockRes.IdUnidadMedida = pBeStock.IdUnidadMedida
            pBeStockRes.Ubicacion_ant = pBeStock.IdUbicacion_anterior
            pBeStockRes.IdBodega = cmbBodega.EditValue
            '#EJC20220718:  IdUbicacionAbastecerCon
            pBeStockRes.IdUbicacionAbastecerCon = Val(txtIdUbicacionAbastecimiento.Text)

            If Not pBeCliente Is Nothing Then
                '#EJC20190313_0718PM: Se agregó para considerar en proceso de clsLnTrans_pe_det.Reservar_Stock_Por_Linea()
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
            End If

            pBeStock.Peso = vPeso

            clsTrans.Open_Connection() : clsTrans.Begin_Transaction()

            If Not pBeCliente Is Nothing Then
                '#EJC20250713: Agregué la actualización del IdCliente para definir políticas de reserva (Killios)
                If pBeCliente.IdCliente <> 0 AndAlso pBePedidoEnc.IdCliente = 0 Then
                    clsLnTrans_pe_enc.Actualizar_IdCliente_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, pBeCliente.IdCliente, clsTrans.lConnection, clsTrans.lTransaction)
                End If
            End If

            If pBeStockRes.Control_Ultimo_Lote Then
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pBeStock.IdProductoBodega, clsTrans.lConnection, clsTrans.lTransaction)
            End If

            If Not dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value Is Nothing Then

                Dim IdStock As Integer = 0
                IdStock = dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value

                Dim BeStockEspecifico As New clsBeStock
                BeStockEspecifico = clsLnStock.GetSingle(IdStock, clsTrans.lConnection, clsTrans.lTransaction)

                clsLnTrans_pe_det.Reservar_Stock_Especifico_Por_Linea(vDiasVencimientoCliente,
                                                                       pBePedidoDet,
                                                                       pBeStockRes,
                                                                       BeStockEspecifico,
                                                                       pBePedidoEnc.IdPickingEnc,
                                                                       AP.HostName,
                                                                       pIdPropietario,
                                                                       clsTrans.lConnection,
                                                                       clsTrans.lTransaction)

                '#CMT20180913: Erik dice que hay que agregar a la lista de 
                'trans_ubic_hh_det el stock reservado para que se reste en memoria en la pantalla de stock
                Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                BeTransUbicHHDet.IdStock = IdStock
                BeTransUbicHHDet.Cantidad = pBeStockRes.Cantidad
                BeTransUbicHHDet.Activo = True
                pListBeTrans_ubic_hh_det.Add(BeTransUbicHHDet)
                pBePedidoDet.IsNew = False

            Else

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea(vDiasVencimientoCliente,
                                                              pBePedidoDet,
                                                              pBeStockRes,
                                                              pBePedidoEnc.IdPickingEnc,
                                                              AP.HostName,
                                                              AP.IdEmpresa,
                                                              cmbBodega.EditValue,
                                                              pIdPropietario,
                                                              clsTrans.lConnection,
                                                              clsTrans.lTransaction) Then

                    pBePedidoDet.IsNew = False

                Else
                    Throw New Exception("No hay existencias suficientes para el código de producto: " & vCodigoProducto)
                End If

            End If

            Dim vIndiceDetalle As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = pBePedidoDet.IdPedidoDet)

            If vIndiceDetalle = -1 Then
                pBePedidoDetList.Add(pBePedidoDet)
            Else
                CopyObject(pBePedidoDet, pBePedidoDetList(vIndiceDetalle))
            End If

            Reservar_Stock_Por_Linea = True

            If MostrarMensajeReserva Then

                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                '#CKFK20240820 Insertar la manufactura, si el pedido tiene manufactura
                If pBePedidoEnc.IdTipoManufactura <> 0 Then

                    If pBePedidoEnc.IdTipoManufactura = clsDataContractDI.Manufacturing_Process.Pegar_Stickers Then

                        If pBePedidoDet.Producto.IdTipoManufactura = clsDataContractDI.Manufacturing_Process.Pegar_Stickers Then

                            Dim BeManufacturaDet As New clsBeTrans_manufactura_det
                            Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc

                            BeManufacturaEnc.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                            clsLnTrans_manufactura_enc.GetSingle_By_IdPedidoEnc(BeManufacturaEnc,
                                                                                clsTrans.lConnection,
                                                                                clsTrans.lTransaction)

                            BeManufacturaDet = New clsBeTrans_manufactura_det
                            BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                            BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                            BeManufacturaDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
                            BeManufacturaDet.IdPropietarioBodega = pBePedidoEnc.IdPropietarioBodega
                            BeManufacturaDet.IdProductoBodega = pBePedidoDet.IdProductoBodega
                            BeManufacturaDet.Codigo_producto = pBePedidoDet.Codigo_Producto
                            BeManufacturaDet.Nombre_producto = pBePedidoDet.Nombre_producto
                            BeManufacturaDet.Cantidad_esperada = pBePedidoDet.Cantidad
                            BeManufacturaDet.Fec_agr = Now
                            BeManufacturaDet.Fec_mod = Now
                            BeManufacturaDet.User_agr = AP.UsuarioAp.IdUsuario
                            BeManufacturaDet.User_mod = AP.UsuarioAp.IdUsuario

                            clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, clsTrans.lConnection, clsTrans.lTransaction)

                        End If

                    Else

                        Dim BeManufacturaDet As New clsBeTrans_manufactura_det
                        Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc

                        BeManufacturaEnc.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                        clsLnTrans_manufactura_enc.GetSingle_By_IdPedidoEnc(BeManufacturaEnc,
                                                                            clsTrans.lConnection,
                                                                            clsTrans.lTransaction)

                        BeManufacturaDet = New clsBeTrans_manufactura_det
                        BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                        BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                        BeManufacturaDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
                        BeManufacturaDet.IdPropietarioBodega = pBePedidoEnc.IdPropietarioBodega
                        BeManufacturaDet.IdProductoBodega = pBePedidoDet.IdProductoBodega
                        BeManufacturaDet.Codigo_producto = pBePedidoDet.Codigo_Producto
                        BeManufacturaDet.Nombre_producto = pBePedidoDet.Nombre_producto
                        BeManufacturaDet.Cantidad_esperada = pBePedidoDet.Cantidad
                        BeManufacturaDet.Fec_agr = Now
                        BeManufacturaDet.Fec_mod = Now
                        BeManufacturaDet.User_agr = AP.UsuarioAp.IdUsuario
                        BeManufacturaDet.User_mod = AP.UsuarioAp.IdUsuario

                        clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, clsTrans.lConnection, clsTrans.lTransaction)

                    End If

                End If

                Carga_Stock_Reservado(clsTrans.lConnection, clsTrans.lTransaction)

                Cargar_Picking(clsTrans.lConnection, clsTrans.lTransaction)

                clsTrans.Commit_Transaction()

            End If

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            Throw
        Finally
            prg.Visible = False
            clsTrans.Close_Conection()
        End Try

    End Function

    Private Function Reservar_Stock_Producto_Kit(ByVal pIndiceFilaPadre As Integer,
                                                ByRef pIdPedidoDetPadre As Integer,
                                                ByVal pIdPropietario As Integer) As Boolean

        Reservar_Stock_Producto_Kit = False

        Dim vDiasVencimientoCliente As Integer = 0
        pBeListStockRes = New List(Of clsBeStock_res)

        Try


            If UnaExtrañaCondicionDeEdicionEnLInea Then
                vIdPedidoDet = pBePedidoDetList(IdxLineaEditando).IdPedidoDet
            End If

            Dim BeListProductosHijos As New List(Of clsBeProducto_kit_composicion)
            Dim NoLinea As Integer = dgrid.Rows(pIndiceFilaPadre).Cells("ColNo_Linea").Value
            Dim CantPadre As Integer = dgrid.Rows(pIndiceFilaPadre).Cells("ColCantidad").Value
            Dim IdPedidoDetPadre As Integer = dgrid.Rows(pIndiceFilaPadre).Cells("colIdPedidoDet").Value
            Dim IdxPadre As Integer = -1
            Dim vIdPedidoDetHijo As Integer = -1

            If Not IdPedidoDetPadre = 0 Then
                '#EJC20191111:Validar si ya existe el padre.
                IdxPadre = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = IdPedidoDetPadre)

                If IdxPadre <> -1 Then
                    '#EJC2019111: Debo eliminar, el stock_res, el detalle del pedido y los registros 
                    'de la lista para volver a insertar.
                    'clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(IdPedidoDet)
                    'clsLnStock_res.Eliminar_Stock_Reservado(pBePedidoEnc.IdPedidoEnc, IdPedidoDet)
                End If

            End If

            BeListProductosHijos = BeListProductoKit.FindAll(Function(x) x.No_Linea = NoLinea)

            If Not BeListProductosHijos Is Nothing OrElse BeListProductosHijos.Count > 0 Then

                For Each ObjPH In BeListProductosHijos

                    Dim vClienteTiempo As New clsBeCliente_tiempos

                    If Not pClienteTiemposList Is Nothing Then
                        vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                        x.IdClasificacion = ObjPH.Producto.Clasificacion.IdClasificacion _
                        And x.IdFamilia = ObjPH.Producto.Familia.IdFamilia)
                    End If

                    If Not vClienteTiempo Is Nothing Then
                        If chkPedidoLocal.Checked Then
                            vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                        Else
                            vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                        End If
                    End If

                    NoDiasVencimientoCell.Value = vDiasVencimientoCliente

                    pBePedidoDet = New clsBeTrans_pe_det
                    pBePedidoDet.No_linea = vNoLinea
                    pBePedidoDet.IdPedidoDet = vIdPedidoDetHijo
                    pBePedidoDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                    pBePedidoDet.Producto = New clsBeProducto
                    pBePedidoDet.Producto.IdProductoBodega = ObjPH.BeStock.IdProductoBodega
                    pBePedidoDet.IdProductoBodega = ObjPH.BeStock.IdProductoBodega
                    pBePedidoDet.Producto.Codigo = ObjPH.Producto.Codigo
                    pBePedidoDet.IdPresentacion = ObjPH.BeStock.IdPresentacion
                    pBePedidoDet.IdUnidadMedidaBasica = ObjPH.BeStock.IdUnidadMedida
                    pBePedidoDet.Cantidad = Math.Round(vCantidad * ObjPH.Cantidad, 6)
                    pBePedidoDet.Peso = vPeso
                    pBePedidoDet.Precio = vPrecio
                    pBePedidoDet.No_recepcion = ObjPH.BeStock.IdRecepcionEnc
                    pBePedidoDet.Cant_despachada = 0
                    pBePedidoDet.IdPresentacion = vIdPresentacion
                    pBePedidoDet.IdEstado = vIdEstado
                    pBePedidoDet.Ndias = vDiasVencimientoCliente
                    pBePedidoDet.Nom_estado = vIdEstado
                    pBePedidoDet.IsNew = IIf(IdPedidoDetPadre = 0, True, False)
                    'pBePedidoDet.IsNew = True
                    pBePedidoDet.Fec_agr = Now
                    pBePedidoDet.User_agr = AP.UsuarioAp.IdUsuario
                    pBePedidoDet.RoadDes = 0
                    pBePedidoDet.RoadDesMon = 0
                    pBePedidoDet.Precio = vPrecio
                    pBePedidoDet.RoadPrecioDoc = vPrecio
                    pBePedidoDet.RoadTotal = vTotal
                    pBePedidoDet.RoadVAL1 = 0
                    pBePedidoDet.RoadVAL2 = 0
                    pBePedidoDet.Codigo_Producto = ObjPH.Producto.Codigo
                    pBePedidoDet.Nombre_producto = ObjPH.Producto.Nombre
                    pBePedidoDet.Nom_presentacion = ObjPH.BeStock.Presentacion.Nombre
                    pBePedidoDet.Nom_unid_med = vNomUnidadMedida
                    pBePedidoDet.Nom_estado = vNomEstado
                    pBePedidoDet.Ndias = vDiasVencimientoCliente
                    pBePedidoDet.Producto.IdProducto = ObjPH.Producto.IdProducto
                    pBePedidoDet.Producto.Nombre = ObjPH.Producto.Nombre
                    pBePedidoDet.IdProductoBodega = ObjPH.Producto.IdProductoBodega
                    pBePedidoDet.IdStockEspecifico = ObjPH.BeStock.IdStock
                    pBePedidoDet.IdPedidoDetPadre = pIdPedidoDetPadre

                    pBeStockRes = New clsBeStock_res()
                    pBeStockRes.IdStockRes = 0
                    pBeStockRes.IdTransaccion = pBePedidoEnc.IdPedidoEnc
                    pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
                    pBeStockRes.Indicador = "PED" 'Pedido #CKFK20220704 Se quito PE y se puso PED
                    pBeStockRes.añada = pBeStock.Añada
                    pBeStockRes.Cantidad = Math.Round(vCantidad * ObjPH.Cantidad, 6)
                    pBeStockRes.Estado = "PPC" 'Producto pedido por cliente
                    pBeStockRes.User_agr = AP.UsuarioAp.IdUsuario
                    pBeStockRes.Fec_agr = Now
                    pBeStockRes.User_mod = AP.UsuarioAp.IdUsuario
                    pBeStockRes.Fec_mod = Now
                    pBeStockRes.Host = AP.HostName
                    pBeStockRes.IdProductoEstado = 0
                    pBeStockRes.IdPresentacion = vIdPresentacion
                    pBeStockRes.IdProductoEstado = vIdEstado
                    pBeStockRes.IdPedido = pBePedidoEnc.IdPedidoEnc
                    pBeStockRes.IdProductoBodega = ObjPH.BeStock.IdProductoBodega
                    pBeStockRes.IdPropietarioBodega = lcmbPropietario.EditValue
                    pBeStockRes.IdUnidadMedida = ObjPH.BeStock.IdUnidadMedida
                    pBeStockRes.Ubicacion_ant = ObjPH.BeStock.IdProductoBodega

                    If Not pBeCliente Is Nothing Then
                        '#EJC20190313_0718PM: Se agregó para considerar en proceso de clsLnTrans_pe_det.Reservar_Stock_Por_Linea()
                        pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
                    End If

                    pBeStock.Peso = vPeso

                    If pBeStockRes.Control_Ultimo_Lote Then
                        pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente, vIdProducto)
                    End If

                    If Not dgrid.Rows(dgrid.CurrentRow.Index).Cells("IdStockEspecifico").Value Is Nothing Then

                        Dim IdStock As Integer = 0
                        IdStock = dgrid.Rows(dgrid.CurrentRow.Index).Cells("IdStockEspecifico").Value

                        Dim BeStockEspecifico As New clsBeStock

                        BeStockEspecifico = clsLnStock.GetSingle(IdStock)

                        clsLnTrans_pe_det.Reservar_Stock_Especifico_Por_Linea(vDiasVencimientoCliente,
                                                                               pBePedidoDet,
                                                                               pBeStockRes,
                                                                               BeStockEspecifico,
                                                                               pBePedidoEnc.IdPickingEnc,
                                                                               AP.HostName,
                                                                               pIdPropietario)

                        '#CMT20180913: Erik dice que hay que agregar a la lista de 
                        'trans_ubic_hh_det el stock reservado para que se reste en memoria en la pantalla de stock
                        Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                        BeTransUbicHHDet.IdStock = IdStock
                        BeTransUbicHHDet.Cantidad = pBeStockRes.Cantidad
                        BeTransUbicHHDet.Activo = True
                        pListBeTrans_ubic_hh_det.Add(BeTransUbicHHDet)

                    Else

                        vIdPedidoDetHijo = pBePedidoDetList.Max(Function(x) x.IdPedidoDet) + 1
                        pBePedidoDet.IdPedidoDet = vIdPedidoDetHijo
                        pBeStockRes.IdPedidoDet = vIdPedidoDetHijo

                    End If

                    Dim vIndiceDetalle As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = pBePedidoDet.IdPedidoDet)

                    If vIndiceDetalle = -1 Then
                        pBePedidoDet.IsNew = True
                        pBeListStockRes.Add(pBeStockRes)
                        pBePedidoDetList.Add(pBePedidoDet)
                    Else
                        CopyObject(pBePedidoDet, pBePedidoDetList(vIndiceDetalle))
                    End If

                Next

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea(vDiasVencimientoCliente,
                                                               pBePedidoDetList,
                                                               pBeListStockRes,
                                                               pBePedidoEnc.IdPickingEnc,
                                                               AP.HostName,
                                                               AP.IdEmpresa,
                                                               cmbBodega.EditValue,
                                                               pIdPedidoDetPadre,
                                                               pIdPropietario) Then

                    Reservar_Stock_Producto_Kit = True

                    XtraMessageBox.Show("Stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Carga_Stock_Reservado()

                    Cargar_Picking()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error al reservar stock:" & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
        End Try

    End Function

    Private Function Agrega_Producto_Kit_Padre_A_Detalle_Pedido(ByVal IndiceFila As Integer) As Boolean

        Agrega_Producto_Kit_Padre_A_Detalle_Pedido = False

        vIdPedidoDetPadre = 0

        Dim vLocalIdPedidoDet As Integer = vIdPedidoDet

        Try

            Dim NoLinea As Integer = dgrid.Rows(IndiceFila).Cells("ColNo_Linea").Value
            Dim IdPedidoDet As Integer = dgrid.Rows(IndiceFila).Cells("colIdPedidoDet").Value

            If Not pBeProducto Is Nothing Then

                If pBeProducto.IdProducto > 0 Then

                    Dim vClienteTiempo As New clsBeCliente_tiempos

                    If Not pClienteTiemposList Is Nothing Then
                        vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                            x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                            And x.IdFamilia = pBeProducto.Familia.IdFamilia)
                    End If

                    Dim vDiasVencimientoCliente As Integer = 0

                    If Not vClienteTiempo Is Nothing Then
                        If chkPedidoLocal.Checked Then
                            vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                        Else
                            vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                        End If
                    End If

                    If Not pBePedidoDetList.Exists(Function(x) x.No_linea = NoLinea) Then

                        If pBePedidoDetList.Count = 0 Then
                            vIdPedidoDet = 1
                        ElseIf vIdPedidoDet = 0 Then 'Es una nueva línea
                            vIdPedidoDet = pBePedidoDetList.Max(Function(x) x.IdPedidoDet) + 1
                        Else 'Se movi? hacia una línea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                            vIdPedidoDet = pBePedidoDet.No_linea
                        End If

                        NoDiasVencimientoCell.Value = vDiasVencimientoCliente

                        pBePedidoDet = New clsBeTrans_pe_det
                        pBePedidoDet.No_linea = vNoLinea
                        pBePedidoDet.IdPedidoDet = vIdPedidoDet
                        pBePedidoDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                        pBePedidoDet.Producto = New clsBeProducto
                        pBePedidoDet.Producto.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.Producto.Codigo = pBeProducto.Codigo
                        pBePedidoDet.IdPresentacion = vIdPresentacion
                        pBePedidoDet.IdUnidadMedidaBasica = pBeProducto.IdUnidadMedidaBasica
                        pBePedidoDet.Cantidad = vCantidad
                        pBePedidoDet.Peso = vPeso
                        pBePedidoDet.Precio = vPrecio
                        pBePedidoDet.No_recepcion = 0
                        pBePedidoDet.Cant_despachada = 0
                        pBePedidoDet.IdPresentacion = vIdPresentacion
                        pBePedidoDet.IdEstado = vIdEstado
                        pBePedidoDet.Ndias = vDiasVencimientoCliente
                        pBePedidoDet.Nom_estado = vIdEstado
                        pBePedidoDet.IsNew = IIf(vIdPedidoDet = 0, True, False)
                        pBePedidoDet.Fec_agr = Now
                        pBePedidoDet.User_agr = AP.UsuarioAp.IdUsuario
                        pBePedidoDet.RoadDes = 0
                        pBePedidoDet.RoadDesMon = 0
                        pBePedidoDet.Precio = vPrecio
                        pBePedidoDet.RoadPrecioDoc = vPrecio
                        pBePedidoDet.RoadTotal = vTotal
                        pBePedidoDet.RoadVAL1 = 0
                        pBePedidoDet.RoadVAL2 = 0
                        pBePedidoDet.Codigo_Producto = pBeProducto.Codigo
                        pBePedidoDet.Nombre_producto = pBeProducto.Nombre
                        pBePedidoDet.Nom_presentacion = pBeProducto.Presentacion.Nombre
                        pBePedidoDet.Nom_unid_med = vNomUnidadMedida
                        pBePedidoDet.Nom_estado = vNomEstado
                        pBePedidoDet.Ndias = vDiasVencimientoCliente
                        pBePedidoDet.Producto.IdProducto = pBeProducto.IdProducto
                        pBePedidoDet.Producto.Nombre = pBeProducto.Nombre
                        pBePedidoDet.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.IdStockEspecifico = 0
                        pBePedidoDet.EsPadre = pBeProducto.Kit

                        If pBePedidoDet.IsNew Then

                            If pBePedidoDetList.Count = 0 Then
                                vLocalIdPedidoDet = 1
                            ElseIf vIdPedidoDet = 0 Then 'Es una nueva linea
                                vLocalIdPedidoDet = pBePedidoDetList.Max(Function(x) x.IdPedidoDet) + 1
                            Else 'Se movio hacia una linea existente (Que probablemente ya tiene stock reservado) #EJC20180710: Descubierto!
                                vLocalIdPedidoDet = pBePedidoDet.No_linea
                            End If

                            vIdPedidoDet = vLocalIdPedidoDet

                            pBePedidoDet.IdPedidoDet = vLocalIdPedidoDet

                            'vIdPedidoDetPadre = pBePedidoDet.IdPedidoDet
                            'pBePedidoDet.IdPedidoDet = clsLnTrans_pe_det.MaxID() + 1
                            'clsLnTrans_pe_det.Insertar(pBePedidoDet)
                        End If

                        Dim vIndiceDetalle As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = pBePedidoDet.IdPedidoDet)

                        If vIndiceDetalle = -1 Then
                            pBePedidoDetList.Add(pBePedidoDet)
                        Else
                            CopyObject(pBePedidoDet, pBePedidoDetList(vIndiceDetalle))
                        End If

                        Agrega_Producto_Kit_Padre_A_Detalle_Pedido = True

                    Else

                        Dim vIndiceLineaPedido As Integer = pBePedidoDetList.FindIndex(Function(x) x.No_linea = NoLinea)

                        'está editando una línea que ya existe.
                        pBePedidoDet = pBePedidoDetList(vIndiceLineaPedido)
                        pBePedidoDet.No_linea = vNoLinea
                        pBePedidoDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                        pBePedidoDet.Producto = New clsBeProducto
                        pBePedidoDet.Producto.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.Producto.Codigo = pBeProducto.Codigo
                        pBePedidoDet.IdPresentacion = vIdPresentacion
                        pBePedidoDet.IdUnidadMedidaBasica = pBeProducto.IdUnidadMedidaBasica
                        pBePedidoDet.Cantidad = vCantidad
                        pBePedidoDet.Peso = vPeso
                        pBePedidoDet.Precio = vPrecio
                        pBePedidoDet.No_recepcion = 0
                        pBePedidoDet.Cant_despachada = 0
                        pBePedidoDet.IdPresentacion = vIdPresentacion
                        pBePedidoDet.IdEstado = vIdEstado
                        pBePedidoDet.Ndias = vDiasVencimientoCliente
                        pBePedidoDet.Nom_estado = vIdEstado
                        pBePedidoDet.IsNew = IIf(vIdPedidoDet = 0, True, False)
                        pBePedidoDet.Fec_agr = Now
                        pBePedidoDet.User_agr = AP.UsuarioAp.IdUsuario
                        pBePedidoDet.RoadDes = 0
                        pBePedidoDet.RoadDesMon = 0
                        pBePedidoDet.Precio = vPrecio
                        pBePedidoDet.RoadPrecioDoc = vPrecio
                        pBePedidoDet.RoadTotal = vTotal
                        pBePedidoDet.RoadVAL1 = 0
                        pBePedidoDet.RoadVAL2 = 0
                        pBePedidoDet.Codigo_Producto = pBeProducto.Codigo
                        pBePedidoDet.Nombre_producto = pBeProducto.Nombre
                        pBePedidoDet.Nom_presentacion = pBeProducto.Presentacion.Nombre
                        pBePedidoDet.Nom_unid_med = vNomUnidadMedida
                        pBePedidoDet.Nom_estado = vNomEstado
                        pBePedidoDet.Ndias = vDiasVencimientoCliente
                        pBePedidoDet.Producto.IdProducto = pBeProducto.IdProducto
                        pBePedidoDet.Producto.Nombre = pBeProducto.Nombre
                        pBePedidoDet.IdProductoBodega = pBeProducto.IdProductoBodega
                        pBePedidoDet.IdStockEspecifico = 0
                        pBePedidoDet.EsPadre = pBeProducto.Kit
                        Agrega_Producto_Kit_Padre_A_Detalle_Pedido = True
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error al reservar stock:" & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
        End Try

    End Function

    '#CM20172310_0153PM: Configuración de producto en pedido.
    Private Sub lnkVerConfiguracionProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkVerConfiguracionProducto.LinkClicked

        Try

            If dgrid.CurrentCell Is Nothing Then Return

            Dim Idx As Integer = dgrid.CurrentCell.RowIndex

            If Not dgrid.Rows(Idx) Is Nothing AndAlso Not dgrid.Rows(Idx).IsNewRow AndAlso Not dgrid.IsCurrentRowDirty Then

                Dim row As DataGridViewRow = dgrid.Rows(Idx)
                Dim IdProductoCell As DataGridViewCell = row.Cells(dgrid.Columns("colIdProducto").Index)

                Dim vIdProducto As String = ""

                vIdProducto = (IIf(IsDBNull(IdProductoCell.Value), "", IdProductoCell.Value))

                If vIdProducto = "" Then

                    XtraMessageBox.Show("Ingrese un Código de producto valido", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Else
                    Dim vParamProd As New frmProducto(frmProducto.TipoTrans.Consulta)
                    vParamProd.pBeProducto.IdProducto = vIdProducto
                    vParamProd.ShowDialog()
                    vParamProd.Dispose()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try

            cmdEliminar.Enabled = False

            If clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

                XtraMessageBox.Show("No se puede anular, Picking asociado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                If pBePedidoEnc.Estado = "Nuevo" _
                    OrElse pBePedidoEnc.Estado = "Incompleto" _
                    OrElse pBePedidoEnc.Estado = "Pendiente" _
                    OrElse pBePedidoEnc.Estado = "Pickeado" _
                    OrElse pBePedidoEnc.Estado = "Verificado" Then

                    If XtraMessageBox.Show("¿Anular Pedido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Using MA As New frmMotivo_AnulacionList()

                            With MA

                                .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                                .BeMotivoAnulacionBodega.IdBodega = cmbBodega.EditValue

                                If .ShowDialog() = DialogResult.OK Then

                                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                    SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                    If clsLnTrans_pe_enc.Anular_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                       .BeMotivoAnulacionBodega.IdMotivoAnulacionBodega) Then

                                        '#GT27062024: si anulamos pedido, se anula la póliza asociada)
                                        If AP.Bodega.Es_Bodega_Fiscal Then
                                            If pBePedidoEnc.ObjPoliza IsNot Nothing Then
                                                pBePedidoEnc.ObjPoliza.activo = 0
                                                clsLnTrans_pe_pol.Anular_poliza(pBePedidoEnc.ObjPoliza)
                                            End If
                                        End If

                                        '#MECR15102025: Se agrego bitacora de logs para pedidos
                                        'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231703A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " Anuló el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)
                                        Dim msgAdvertencia As String = "ADVERTENCIA_202302231703A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " Anuló el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc
                                        clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia,
                                                                            pIdEmpresa:=AP.IdEmpresa,
                                                                            pIdBodega:=AP.IdBodega,
                                                                            pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                            pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                                        SplashScreenManager.CloseForm(False)

                                        XtraMessageBox.Show("Se ha anulado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                        If Not InvokeListarPedidos Is Nothing Then
                                            InvokeListarPedidos.Invoke()
                                        End If

                                        Close()

                                    Else
                                        SplashScreenManager.CloseForm(False)
                                        XtraMessageBox.Show("No se pudo anular el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    End If

                                End If

                            End With

                        End Using

                    End If

                Else

                    XtraMessageBox.Show("No es posible anular el pedido en estado " & pBePedidoEnc.Estado, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            End If

            cmdEliminar.Enabled = True

        Catch ex As Exception
            cmdEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick

        Try

            cmdActualizar.Enabled = False

            If dgrid.IsCurrentRowDirty Then
                MsgBox("Un valor se ha modificado en el grid y no se han " &
                       " confirmado los cambios, presione enter en la celda " &
                       " que está modificando antes de guardar el registro", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If Datos_Correctos() Then

                If XtraMessageBox.Show("¿Actualizar Pedido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

                    PedidoGuardadoPorUsuario = Guardar_Pedido()

                    '#MECR15102025: Se agrego bitacora de logs para pedidos
                    'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231703: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)
                    Dim msgAdvertencia As String = "ADVERTENCIA_202302231703: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc
                    clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia,
                                                        pIdEmpresa:=AP.IdEmpresa,
                                                        pIdBodega:=AP.IdBodega,
                                                        pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                        pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                    SplashScreenManager.CloseForm(False)

                    If PedidoGuardadoPorUsuario Then

                        '#CKFK 20210528 Agregué validación de que el grid tenga registros
                        If dgrid.Rows.Count > 0 Then

                            ' #CKFK20171116 11:07PM: Al crear el pedido se sugiere la creación del picking automático
                            SplashScreenManager.CloseForm(False)

                            If Not clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

                                If XtraMessageBox.Show("Se guardó el pedido. ¿Crear  picking?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    If Nuevo_Picking() Then
                                        DialogResult = DialogResult.OK
                                        If Not InvokeListarPedidos Is Nothing Then InvokeListarPedidos.Invoke
                                        Close()
                                    Else
                                        If Not InvokeListarPedidos Is Nothing Then InvokeListarPedidos.Invoke
                                        Close()
                                    End If
                                Else
                                    If Not InvokeListarPedidos Is Nothing Then InvokeListarPedidos.Invoke
                                    Close()
                                End If

                            Else

                                If Modo = TipoTrans.Nuevo Then
                                    pBePedidoEnc.Detalle = pBePedidoDetList
                                Else
                                    '#EJC20180620: En edición la lista de detalle del BeEnc no está actualizada desde la BD por lo que se asigna la lista en memoria.
                                    pBePedidoEnc.Detalle = pBePedidoDetList
                                End If

                                '#MECR15102025: Se agrego bitacora de logs para pedidos
                                'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)
                                Dim msgAdvertencia1 As String = "ADVERTENCIA_202302271656A: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " actualizó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc
                                clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia1,
                                                                    pIdEmpresa:=AP.IdEmpresa,
                                                                    pIdBodega:=AP.IdBodega,
                                                                    pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                    pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                                XtraMessageBox.Show(String.Format("Se actualizó el pedido#:{0}  y el picking#:{1} asociado", pBePedidoEnc.IdPedidoEnc, pBePedidoEnc.IdPickingEnc), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                If Not InvokeListarPedidos Is Nothing Then
                                    InvokeListarPedidos.Invoke
                                End If

                                DialogResult = DialogResult.OK

                                Close()

                            End If

                        Else

                            XtraMessageBox.Show("No hay productos asociados al pedido", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    Else
                        InvokeListarPedidos.Invoke
                        Close()
                    End If

                End If

            End If


            cmdActualizar.Enabled = True


        Catch ex As Exception
            cmdActualizar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdListaUbicacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdListaUbicacion.ItemClick
        'Imprimir()

        Try

            Genera_Reporte_Lista_Ubicaciones()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Genera_Reporte_Lista_Ubicaciones()

        Try

            Dim Rep As New rptListaUbicaciones
            Dim pDatatable As New DataTable

            pDatatable = clsLnTrans_picking_ubic.Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(0, pBePedidoEnc.IdPedidoEnc)
            'Rep.DataSource = clsLnTrans_picking_ubic.Get_Ubicacion_Picking_By_IdPicking_And_IdPedidoEnc(0, pBePedidoEnc.IdPedidoEnc)

            If pDatatable IsNot Nothing AndAlso pDatatable.Rows.Count > 0 Then

                Rep.DataSource = pDatatable

                'Rep.DataMember = "Result"

                Rep.Parameters("Tipo_Documento").Value = cmbTipoPedido.Text
                Rep.Parameters("Tipo_Documento").Visible = False
                Rep.Parameters("Observacion").Value = txtObservacion.Text
                Rep.Parameters("Observacion").Visible = False
                'Rep.Parameters("No_Pedido_ERP").Value = txtReferencia.Text
                'Rep.Parameters("No_Pedido_ERP").Visible = False
                'Rep.Parameters("Direcion_Entrega").Value = txtDireccionEntrega.Text
                'Rep.Parameters("Direcion_Entrega").Visible = False

                'Rep.RequestParameters = False

                Rep.ShowPreview()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub xtrPedido_TabIndexChanged(sender As Object, e As EventArgs) Handles xtrPedido.TabIndexChanged

        If (txtIdCliente.EditValue Is Nothing OrElse txtIdCliente.Text = "") AndAlso xtrPedido.SelectedTabPageIndex = 1 Then
            XtraMessageBox.Show("Debe seleccionar el cliente", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            dgrid.Enabled = False
        Else
            dgrid.Enabled = True
        End If

    End Sub

    Private Function SetTotal() As Double

        Dim c As Double

        Try

            For x = 0 To dgrid.Rows.Count - 1
                If String.IsNullOrEmpty(dgrid.Rows(x).Cells("ColNomProducto").Value) = False Then
                    Dim str As String = dgrid.Rows(x).Cells("ColTotal").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(dgrid.Item("ColTotal".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Sub setTotal(ByVal pIndex As Integer)

        Try

            If IsNumeric(dgrid.Rows(pIndex).Cells("ColCantidad").Value) OrElse
                IsNumeric(dgrid.Rows(pIndex).Cells("ColPrecio").Value) Then

                Dim cantidad As Decimal = dgrid.Rows(pIndex).Cells("ColCantidad").Value
                Dim costo As Decimal = dgrid.Rows(pIndex).Cells("ColPrecio").Value
                Dim total As Decimal = Math.Round(cantidad * costo, 2)

                dgrid.Rows(pIndex).Cells("ColTotal").Value = total
                dgrid.Rows(pIndex).Cells("ColPrecio").Value = costo

                lblCantidad.Text = "Cant: " & Format(SetCantidad(), "N2")
                lblPeso.Text = "Peso: " & Format(SetPeso(), "c")
                lblTotal.Text = "Total: " & Format(SetTotal(), "c")
                lblRegs.Caption = String.Format("Registros: {0}", dgrid.RowCount)

            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Sub

    Private Function SetCantidad() As Double

        Dim c As Double

        Try

            For x = 0 To dgrid.Rows.Count - 1
                If String.IsNullOrEmpty(dgrid.Rows(x).Cells("ColNomProducto").Value) = False Then
                    Dim str As String = dgrid.Rows(x).Cells("ColCantidad").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(dgrid.Item("ColCantidad".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Function SetPeso() As Double

        Dim c As Double

        Try

            For x = 0 To dgrid.Rows.Count - 1
                If String.IsNullOrEmpty(dgrid.Rows(x).Cells("ColNomProducto").Value) = False Then
                    Dim str As String = dgrid.Rows(x).Cells("ColPeso").Value
                    If String.IsNullOrEmpty(str) = False Then
                        c += CDbl(dgrid.Item("ColPeso".ToLower, x).Value)
                    End If
                End If
            Next

            Return c

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Sub dgrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgrid.CellFormatting

        Try

            If e.ColumnIndex >= 7 AndAlso e.ColumnIndex <= 9 Then
                If e.Value IsNot Nothing AndAlso IsNumeric(e.Value.ToString()) Then
                    Dim Valor As Double = Double.Parse(e.Value.ToString())
                    e.Value = Valor.ToString("N6")
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Datos_Picking(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim lRow As DataRow
        Dim pIdPedidoEnc As Integer

        Try

            pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

            listarPicking = clsLnTrans_picking_enc.Get_All_By_Pedido(pIdPedidoEnc,
                                                                     lConnection,
                                                                     lTransaction)

            If Not listarPicking Is Nothing Then

                Dim ListarPickingDet = From i In listarPicking Group i By Keys = New With {Key i.IdPedidoEnc, Key i.IdPickingEnc, Key i.NombreBodega, Key i.NombrePropietarioPicking,
                                                               Key i.NombreUbicacionPicking, Key i.Estado, Key i.Detalle_operador, Key i.Hora_ini, Key i.Hora_fin, Key i.Fecha_picking} Into Group
                                       Select New With {.IdPedido = Keys.IdPedidoEnc, .IdPicking = Keys.IdPickingEnc, .Bodega = Keys.NombreBodega, .Propietario = Keys.NombrePropietarioPicking,
                                                        .Ubicacion = Keys.NombreUbicacionPicking, .Estado = Keys.Estado, .Operador = Keys.Detalle_operador, .Inicio = Keys.Hora_ini, .Fin = Keys.Hora_fin, .Fecha = Keys.Fecha_picking}

                DsPicking.Encabezado.Clear()

                If ListarPickingDet IsNot Nothing AndAlso ListarPickingDet.Count > 0 Then

                    For Each Objs In ListarPickingDet

                        lRow = DsPicking.Encabezado.NewRow
                        lRow.Item("IdPedido") = Objs.IdPedido
                        lRow.Item("Código") = Objs.IdPicking
                        lRow.Item("Bodega") = Objs.Bodega
                        lRow.Item("Propietario") = Objs.Propietario
                        lRow.Item("Ubicación Picking") = Objs.Ubicacion
                        lRow.Item("Estado") = Objs.Estado
                        lRow.Item("Detalle Operador") = Objs.Operador
                        lRow.Item("Hora Inicial") = Objs.Inicio
                        lRow.Item("Hora Final") = Objs.Fin
                        lRow.Item("Fecha Picking") = Objs.Fecha

                        DsPicking.Encabezado.AddEncabezadoRow(lRow)

                    Next

                End If

            End If

            pIdPedidoEnc = 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Cargar_Datos_Detalle_Picking(ByVal IdPedido As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim lRow As DataRow

        Try

            Dim List As New List(Of clsBeTrans_picking_det)
            Dim BeProductoPresentacion As New clsBeProducto_Presentacion
            Dim BePedidoDet As New clsBeTrans_pe_det
            Dim CantidadPres As Double = 0

            List = clsLnTrans_picking_enc.Get_All_Detalle_By_Pedido(IdPedido,
                                                                    lConnection,
                                                                    lTransaction)

            If List IsNot Nothing AndAlso List.Count > 0 Then

                DsPicking.Detalle.Clear()

                For Each Objs In List

                    CantidadPres = 0

                    BePedidoDet = pBePedidoEnc.Detalle.Find(Function(x) x.IdPedidoDet = Objs.IdPedidoDet)

                    If Not BePedidoDet Is Nothing Then

                        If Objs.Presentacion.IdPresentacion <> 0 Then

                            BeProductoPresentacion.IdPresentacion = Objs.Presentacion.IdPresentacion
                            clsLnProducto_presentacion.GetSingle(BeProductoPresentacion, lConnection, lTransaction)

                            If Not BeProductoPresentacion Is Nothing Then

                                If BePedidoDet.IdStockEspecifico = 0 Then
                                    CantidadPres = Objs.CantidadReservada
                                    Objs.CantidadReservada = Math.Round(Objs.CantidadReservada * BeProductoPresentacion.Factor, 6)
                                    Objs.Cantidad_Pickeada = Objs.Cantidad_Pickeada
                                    Objs.Cantidad_Verificada = Objs.Cantidad_Verificada
                                    Objs.Cantidad_Stock = Math.Round(Objs.Cantidad_Stock / BeProductoPresentacion.Factor, 6)
                                Else
                                    Objs.CantidadReservada = Math.Round(Objs.CantidadReservada * BeProductoPresentacion.Factor, 6)
                                    CantidadPres = Objs.CantidadReservada
                                    Objs.Cantidad_Pickeada = Objs.Cantidad_Pickeada
                                    Objs.Cantidad_Verificada = Objs.Cantidad_Verificada
                                    Objs.Cantidad_Stock = Math.Round(Objs.Cantidad_Stock / BeProductoPresentacion.Factor, 6)
                                End If

                            End If

                        End If

                    End If

                    lRow = DsPicking.Detalle.NewRow
                    lRow.Item("IdPedido") = Objs.IdPedidoEnc
                    lRow.Item("IdPicking") = Objs.IdPickingEnc
                    lRow.Item("codigo") = Objs.Codigo
                    lRow.Item("nombre") = Objs.NombreProducto
                    lRow.Item("Presentacion") = Objs.Presentacion.Nombre
                    lRow.Item("Estado") = Objs.Estado
                    lRow.Item("UMBas") = Objs.UMBas
                    lRow.Item("Propietario") = Objs.Propietario
                    lRow.Item("Ubicacion") = Objs.UbicacionPicking
                    lRow.Item("Cantidad") = Objs.Cantidad
                    lRow.Item("IdPedidoDet") = Objs.IdPedidoDet
                    lRow.Item("IdUbicacion") = Objs.IdUbicacion
                    lRow.Item("CantidadReservada") = Objs.CantidadReservada
                    lRow.Item("fecha_ingreso") = Objs.Fecha_Ingreso
                    lRow.Item("fecha_vence") = Objs.Fecha_Vence
                    lRow.Item("Cantidad_Pickeada") = Objs.Cantidad_Pickeada
                    lRow.Item("Cantidad_Verificada") = Objs.Cantidad_Verificada
                    lRow.Item("Cantidad_Stock") = Objs.Cantidad_Stock
                    lRow.Item("Factor") = Objs.Factorx
                    lRow.Item("Lote") = Objs.Lote
                    lRow.Item("Lic_Plate") = Objs.Lic_Plate
                    lRow.Item("CantidadPresentacion") = CantidadPres

                    DsPicking.Detalle.AddDetalleRow(lRow)

                Next

                If gvDetallePicking.Columns.Count > 0 Then

                    gvDetallePicking.BestFitColumns()

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Cargar_Picking(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            If pBePedidoEnc.IdPickingEnc <> 0 Then

                mnuLiberarNoPickeado.Visibility = BarItemVisibility.Always

                grdPicking.BeginUpdate()

                Cargar_Datos_Picking(lConnection, lTransaction)

                Dim IdPedido As Integer = pBePedidoEnc.IdPedidoEnc

                Cargar_Datos_Detalle_Picking(IdPedido, lConnection, lTransaction)

                'DetalleTableAdapter.Fill(DsPicking.Detalle, IdPedido)

                DsPicking.Detalle.codigoColumn.ReadOnly = True

                grdPicking.EndUpdate()

                grdPicking.DefaultView.PopulateColumns()

                gvDetallePicking.Columns("Código").Caption = "No. Picking"
                gvDetallePicking.Columns("IdPedido").Caption = "No. Pedido"

                grdPicking.ForceInitialize()

                '#EJC20181906: Desplegar condicionalmente el tab de picking, solo si tiene picking                
                tpPicking.PageVisible = True
                mnuCrearPicking.Enabled = False
            Else
                tpPicking.PageVisible = False
                mnuCrearPicking.Enabled = True
            End If


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Cargar_Picking()

        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            If pBePedidoEnc.IdPickingEnc <> 0 Then

                mnuLiberarNoPickeado.Visibility = BarItemVisibility.Always

                grdPicking.BeginUpdate()

                Cargar_Datos_Picking(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                Dim IdPedido As Integer = pBePedidoEnc.IdPedidoEnc

                Cargar_Datos_Detalle_Picking(IdPedido, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                DsPicking.Detalle.codigoColumn.ReadOnly = True

                grdPicking.EndUpdate()

                grdPicking.DefaultView.PopulateColumns()

                gvDetallePicking.Columns("Código").Caption = "No. Picking"
                gvDetallePicking.Columns("IdPedido").Caption = "No. Pedido"

                grdPicking.ForceInitialize()

                tpPicking.PageVisible = True
            Else
                If tpPicking.Visible Then
                    tpPicking.PageVisible = False
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub grdPicking_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdPicking.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            gridView.BestFitColumns()

            gridView.Columns("Estado").VisibleIndex = 7
            gridView.Columns("Ubicacion").VisibleIndex = 8
            gridView.Columns("CantidadReservada").VisibleIndex = 9
            gridView.Columns("UMBas").VisibleIndex = 10
            gridView.Columns("CantidadPresentacion").VisibleIndex = 11
            gridView.Columns("Presentacion").VisibleIndex = 12
            gridView.Columns("Cantidad_Pickeada").VisibleIndex = 13
            gridView.Columns("Cantidad_Verificada").VisibleIndex = 14

            gridView.Columns("IdPicking").Visible = False
            gridView.Columns("IdPedido").Visible = False
            gridView.Columns("IdPedidoDet").Visible = False
            gridView.Columns("Cantidad_Stock").Visible = False
            gridView.Columns("Propietario").Visible = False
            gridView.Columns("Factor").Visible = False
            gridView.Columns("Cantidad").Visible = False
            gridView.Columns("IdUbicacion").Visible = False

            gridView.Columns("codigo").Caption = "Codigo"
            gridView.Columns("nombre").Caption = "Producto"
            gridView.Columns("CantidadReservada").Caption = "Cant_Pedido U.M.Bas"
            gridView.Columns("CantidadPresentacion").Caption = "Cant_Pedido Pres"
            gridView.Columns("Cantidad_Pickeada").Caption = "Cant_Pick"
            gridView.Columns("Cantidad_Verificada").Caption = "Cant_Veri"

            gridView.OptionsView.ShowFooter = True

            gridView.Columns("fecha_vence").DisplayFormat.FormatType = FormatType.DateTime
            gridView.Columns("fecha_vence").DisplayFormat.FormatString = "dd/MM/yyyy"

            gridView.Columns("CantidadReservada").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("CantidadReservada").DisplayFormat.FormatString = "n2"
            gridView.Columns("CantidadReservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("CantidadReservada").SummaryItem.DisplayFormat = "{0:n6}"

            gridView.Columns("CantidadPresentacion").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("CantidadPresentacion").DisplayFormat.FormatString = "n2"
            gridView.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"

            gridView.Columns("Cantidad_Pickeada").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("Cantidad_Pickeada").DisplayFormat.FormatString = "n2"

            gridView.Columns("Cantidad_Pickeada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("Cantidad_Pickeada").SummaryItem.DisplayFormat = "{0:n6}"

            gridView.Columns("Cantidad_Verificada").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("Cantidad_Verificada").DisplayFormat.FormatString = "n2"

            gridView.Columns("Cantidad_Verificada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

            gridView.BestFitColumns()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Function Nuevo_Picking() As Boolean

        Nuevo_Picking = False

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Generando picking...")

            Dim Picking As New frmPicking(frmPicking.TipoTrans.Nuevo, True) With
                {.BePickingEnc = New clsBeTrans_picking_enc() With {.IsNew = True}}

            Picking.SetDatataTable()

            AP.Listar_Bodegas_By_Usuario(Picking.cmbBodegas)

            Picking.IdBodega = cmbBodega.EditValue
            Picking.cmbBodegas.EditValue = cmbBodega.EditValue
            Picking.cmbBodegas.Enabled = False
            Picking.cmbPropietario.EditValue = lcmbPropietario.EditValue
            Picking.cmbPropietario.Enabled = False
            Picking.pReferencia = txtReferencia.Text
            Picking.Set_Stock_Res(pBePedidoEnc.IdPedidoEnc)

            If pBePedidoEnc.IdPickingEnc = 0 Then
                Picking.pListaPedidos.Add(pBePedidoEnc.IdPedidoEnc)
            Else
                Picking.pListaPedidos.Clear()
            End If

            Dim i As Integer = Picking.dgridPedidos.Rows.Add()

            Dim pBeCliente As New clsBeCliente
            pBeCliente.IdCliente = pBePedidoEnc.Cliente.IdCliente
            pBeCliente = clsLnCliente.GetSingle(pBeCliente.IdCliente)

            If Not pBeCliente Is Nothing Then

                Dim pBePropietarioBodega As New clsBePropietario_bodega
                pBePropietarioBodega.IdPropietarioBodega = pBePedidoEnc.PropietarioBodega.IdPropietarioBodega
                pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario_By_IdPropietarioBodega(pBePropietarioBodega.IdPropietarioBodega)

                Picking.dgridPedidos.Rows(i).Cells("IdPedido").Value = pBePedidoEnc.IdPedidoEnc
                Picking.dgridPedidos.Rows(i).Cells("Referencia").Value = pBePedidoEnc.Referencia
                Picking.dgridPedidos.Rows(i).Cells("Bodega").Value = pBePedidoEnc.IdBodega
                Picking.dgridPedidos.Rows(i).Cells("Cliente").Value = pBeCliente.Nombre_comercial
                Picking.dgridPedidos.Rows(i).Cells("Propietario").Value = pBePropietarioBodega.Propietario.Nombre_comercial
                Picking.dgridPedidos.Rows(i).Cells("FechaPedido").Value = pBePedidoEnc.Fecha_Pedido
                Picking.dgridPedidos.Rows(i).Cells("EstadoP").Value = pBePedidoEnc.Estado

                Picking.dgridPedidos.CommitEdit(DataGridViewDataErrorContexts.Commit)
                Picking.dgridPedidos.EndEdit()

                If Modo = TipoTrans.Nuevo Then
                    pBePedidoEnc.Detalle = pBePedidoDetList
                Else
                    '#EJC20180620: En edición la lista de detalle del BeEnc no está actualizada desde la BD por lo que se asigna la lista en memoria.
                    pBePedidoEnc.Detalle = pBePedidoDetList
                End If

                For Each BePedidoDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle
                    BePedidoDet.ListaStockRes = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoDet(BePedidoDet.IdPedidoDet, BePedidoDet.IdPedidoEnc)
                    Picking.SetProducto(BePedidoDet, pBePedidoEnc)
                    Application.DoEvents()
                Next

                '#EJC20210826: Inidicar que el picking se llamó desde el pedido.
                Picking.Llamado_Desde_Pedido = True

                '#EJC20210826: Inidicar si se verifica o no de forma automática.
                Picking.chkverifica_auto.Checked = chkVerificar.Checked

                '#EJC20210826: Inidicar si se toma o no fotografía en la verificacion.
                Picking.chkFotografiaVerificacion.Checked = chkFotografiaVerificacion.Checked

                '#EJC20210826: Inidicar si se requiere empaque por tarima
                Picking.chkEmpaquePorTarima.Checked = pBePedidoEnc.TipoPedido.Empaque_Tarima

                SplashScreenManager.CloseForm(False)

                If Picking.ShowDialog() = DialogResult.OK Then
                    Nuevo_Picking = True
                    Actualizar_Info_Picking()
                End If

                If Not InvokeListarPedidos Is Nothing Then InvokeListarPedidos.Invoke()

                Close()

            Else
                XtraMessageBox.Show("Falta definir el cliente o el pedido no se ha guardado aún.", "Generar Pedido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub lnkAgregarStockEspecifico_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkAgregarStockEspecifico.LinkClicked

        Try

            Dim IdCliente As Integer = 0
            Dim vLinea As DataGridViewCell = Nothing
            Dim vValorUltimaLinea As Double = 0
            Dim vMostrarMensaje As Boolean = False

            If Modo = TipoTrans.Nuevo Then

                If Not dgrid.CurrentRow Is Nothing Then

                    For i As Integer = 0 To dgrid.RowCount - 1

                        Dim vCantidad As DataGridViewCell = dgrid.Rows(i).Cells(dgrid.Columns("ColCantidad").Index)
                        vLinea = dgrid.Rows(i).Cells(dgrid.Columns("colNo_Linea").Index)

                        If dgrid.Rows(i).IsNewRow Then

                            If Not vLinea.Value Is Nothing OrElse Not vCantidad.Value Is Nothing Then
                                XtraMessageBox.Show("Falta confirmar stock en la linea: " & i + 1, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If

                        Else
                            If vLinea.Value Is Nothing OrElse vCantidad.Value Is Nothing Then
                                XtraMessageBox.Show("Falta confirmar stock en la linea: " & i + 1, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If
                        End If
                    Next i

                End If

                If Not pBeCliente Is Nothing Then
                    IdCliente = pBeCliente.IdCliente
                End If

            ElseIf Modo = TipoTrans.Editar Then

                IdCliente = pBePedidoEnc.Cliente.IdCliente

            End If

            Dim fila2 As Object = cmbTipoPedido.GetSelectedDataRow
            If Not fila2 Is Nothing Then
                vControlPoliza = IIf(IsDBNull(fila2.Item("control_poliza")), False, fila2.Item("control_poliza"))
            End If

            If frmSelStock Is Nothing Then
                frmSelStock = New frmStock_Especifico_List With {.Modo = frmStock_Especifico_List.pModo.Seleccion,
                .WindowState = FormWindowState.Maximized, .IdCliente = IdCliente, .BuscarPoliza = vControlPoliza, .IdBodega = cmbBodega.EditValue}
            Else
                frmSelStock.IdCliente = IdCliente
                frmSelStock.IdBodega = cmbBodega.EditValue
            End If

            If Not lcmbPropietario.EditValue Is Nothing Then
                frmSelStock.pIdPropietarioBodega = lcmbPropietario.EditValue
            Else
                frmSelStock.pIdPropietarioBodega = 0
            End If

            frmSelStock.Modo = frmStock_Especifico_List.pModo.Seleccion
            frmSelStock.pListObjDet = pListBeTrans_ubic_hh_det
            frmSelStock.WindowState = FormWindowState.Maximized
            frmSelStock.chkFiltroPolizaActivo.Checked = chkControlPoliza.Checked

            '#GT08042025: enviar el estado del documento si tuviera uno por default que coincida con el mismo propietario
            If lcmbPropietario.EditValue = BeTipoDoc.IdPropietario Then
                frmSelStock.IdProductoEstadoDefault = BeTipoDoc.IdProductoEstado
            Else
                frmSelStock.IdProductoEstadoDefault = 0
            End If



            If frmSelStock.ShowDialog() = DialogResult.OK Then

                If (frmSelStock.pObjStock IsNot Nothing AndAlso frmSelStock.pObjStock.IdStock <> 0) Then

                    If Not frmSelStock.SeleccionMultiple Then

                        pObjVWStock = frmSelStock.pObjStock

                        'GT 260720211832: Si tiene control poliza, el stock especifico se filtrara por una póliza
                        If vControlPoliza Then

                            lcmbPropietario.EditValue = pObjVWStock.IdPropietarioBodega

                            'GT 230820211837: evita tener que setear los campos y consultar cliente por cada iteración
                            If txtIdCliente.EditValue Is Nothing Then

                                Dim Obj As New clsBeCliente()
                                Dim vPropietario As New clsBePropietarios
                                vPropietario = clsLnPropietarios.GetSingle(clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue,
                                                                                                               lcmbPropietario.EditValue))

                                Dim lista = clsLnCliente.Get_All_Clientes_By_IdPropietario_And_IdBodega(True,
                                                                                                        vPropietario.IdPropietario,
                                                                                                        AP.IdBodega,
                                                                                                        BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                                'GT17022022: Set del idcliente como cliente_bodega_wms
                                If lista.Rows.Count > 0 Then
                                    txtIdCliente.EditValue = lista.Rows(0).Item("Correlativo")
                                End If

                            End If

                        End If

                        Dim Idx As Integer = 0

                        If Not dgrid.CurrentRow Is Nothing Then

                            'GT 23082021: si el grid ya tiene registros filtrados por poliza el index no es 0 pero tampoco esta seleccionada la siguiente fila
                            If dgrid.RowCount > 0 Then
                                Dim registros = dgrid.RowCount - 1
                                If dgrid.Rows(registros).IsNewRow Then
                                    Idx = dgrid.RowCount - 1
                                Else
                                    Idx = dgrid.CurrentRow.Index
                                End If
                            Else
                                Idx = dgrid.CurrentRow.Index
                            End If

                        Else
                            dgrid.Rows.Add()
                        End If

                        Set_Producto_Stock_Especifico(pObjVWStock, Idx)

                        'IdStockEspecifico
                        dgrid.Rows(Idx).Cells("IdStockEspecifico").Value = pObjVWStock.IdStock

                    Else

                        vMostrarMensaje = frmSelStock.mnuMostrarMensajePorCadaReserva.Checked

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        'SplashScreenManager.Default.SetWaitFormDescription("Agregando registros...")

                        Dim Idx As Integer = 0

                        Dim vCantidadRegistrosAAgregar As Integer = frmSelStock.listaStockSeleccionado.Count

                        For Each StockEspecificoSeleccionado In frmSelStock.listaStockSeleccionado

                            pObjVWStock = New clsBeVW_stock_res()
                            pObjVWStock = StockEspecificoSeleccionado

                            'GT07022022: faltó esta validación si es seleccion multiple.
                            If vControlPoliza Then

                                lcmbPropietario.EditValue = pObjVWStock.IdPropietarioBodega

                                'GT17022022:si no es selección multiple, igual setear cliente para validar transferencia o pedido
                                If txtIdCliente.EditValue Is Nothing Then

                                    Dim Obj As New clsBeCliente
                                    Dim vPropietario As New clsBePropietarios
                                    vPropietario = clsLnPropietarios.GetSingle(clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue,
                                                                                                                   lcmbPropietario.EditValue))

                                    Dim lista = clsLnCliente.Get_All_Clientes_By_IdPropietario_And_IdBodega(True,
                                                                                                            vPropietario.IdPropietario,
                                                                                                            AP.IdBodega,
                                                                                                            BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                                    'GT17022022: Set del idcliente como cliente_bodega_wms
                                    If lista.Rows.Count > 0 Then
                                        txtIdCliente.EditValue = lista.Rows(0).Item("Correlativo")
                                    End If

                                End If

                            End If

                            If Not dgrid.CurrentRow Is Nothing Then

                                If dgrid.RowCount > 0 Then

                                    If Idx = 0 Then
                                        Idx = dgrid.RowCount - 1
                                        vCantidadRegistrosAAgregar += (dgrid.RowCount - 1)
                                        dgrid.Rows.Add()
                                    Else
                                        If Not (vCantidadRegistrosAAgregar) = (Idx + 1) Then
                                            dgrid.Rows.Add()
                                        End If
                                        Idx += 1
                                    End If

                                End If

                            Else
                                Idx = dgrid.Rows.Add()
                            End If

                            Set_Producto_Stock_Especifico_Con_Reserva_Completa(pObjVWStock, Idx)

                            'IdStockEspecifico
                            dgrid.Rows(Idx).Cells("IdStockEspecifico").Value = pObjVWStock.IdStock

                            vIdEstado = pObjVWStock.IdProductoEstado

                            vIdPedidoDet = 0

                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                            'SplashScreenManager.Default.SetWaitFormDescription("Agregando registros...")

                            If Reservar_Stock_Por_Linea_Seleccion(Idx, False, pObjVWStock, vMostrarMensaje) Then

                                SplashScreenManager.Default.SetWaitFormDescription("Stock Reservado: " & pObjVWStock.Codigo_Producto)

                                '#EJC20220422:Actualizar totales.
                                setTotal(Idx)

                                '#EJC20171021_0308PM: Si se reserva el stock correctamente asociar el IdPedidoDet al grid para evitar insertar nuevamente.
                                dgrid.Rows(Idx).Cells("colIdPedidoDet").Value = pBePedidoDet.IdPedidoDet
                                dgrid.Rows(Idx).Cells("colNo_Linea").Value = vNoLinea

                                '#CKFK20240820 Insertar la manufactura, si el pedido tiene manufactura
                                If pBePedidoEnc.IdTipoManufactura <> 0 Then

                                    If pBePedidoEnc.IdTipoManufactura = clsDataContractDI.Manufacturing_Process.Pegar_Stickers Then

                                        If pBePedidoDet.Producto.IdTipoManufactura = clsDataContractDI.Manufacturing_Process.Pegar_Stickers Then

                                            Dim BeManufacturaDet As New clsBeTrans_manufactura_det
                                            Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc
                                            Dim clsTrans As New clsTransaccion

                                            Try
                                                clsTrans.Begin_Transaction()

                                                BeManufacturaEnc.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                                                clsLnTrans_manufactura_enc.GetSingle_By_IdPedidoEnc(BeManufacturaEnc,
                                                                                                clsTrans.lConnection,
                                                                                                clsTrans.lTransaction)

                                                BeManufacturaDet = New clsBeTrans_manufactura_det
                                                BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                                                BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                                                BeManufacturaDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
                                                BeManufacturaDet.IdPropietarioBodega = pBePedidoEnc.IdPropietarioBodega
                                                BeManufacturaDet.IdProductoBodega = pBePedidoDet.IdProductoBodega
                                                BeManufacturaDet.Codigo_producto = pBePedidoDet.Codigo_Producto
                                                BeManufacturaDet.Nombre_producto = pBePedidoDet.Nombre_producto
                                                BeManufacturaDet.Cantidad_esperada = pBePedidoDet.Cantidad
                                                BeManufacturaDet.Fec_agr = Now
                                                BeManufacturaDet.Fec_mod = Now
                                                BeManufacturaDet.User_agr = AP.UsuarioAp.IdUsuario
                                                BeManufacturaDet.User_mod = AP.UsuarioAp.IdUsuario

                                                clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, clsTrans.lConnection, clsTrans.lTransaction)

                                                clsTrans.Commit_Transaction()

                                            Catch ex As Exception
                                                If Not clsTrans Is Nothing Then
                                                    clsTrans.RollBack_Transaction()
                                                End If
                                            Finally
                                                clsTrans.Close_Conection()
                                            End Try

                                        End If

                                    Else

                                        Dim BeManufacturaDet As New clsBeTrans_manufactura_det
                                        Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc
                                        Dim clsTrans As New clsTransaccion

                                        Try
                                            clsTrans.Begin_Transaction()

                                            BeManufacturaEnc.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
                                            clsLnTrans_manufactura_enc.GetSingle_By_IdPedidoEnc(BeManufacturaEnc,
                                                                                                clsTrans.lConnection,
                                                                                                clsTrans.lTransaction)

                                            BeManufacturaDet = New clsBeTrans_manufactura_det
                                            BeManufacturaDet.IdManufacturaDet = clsLnTrans_manufactura_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                                            BeManufacturaDet.IdManufacturaEnc = BeManufacturaEnc.IdManufacturaEnc
                                            BeManufacturaDet.IdPedidoDet = pBePedidoDet.IdPedidoDet
                                            BeManufacturaDet.IdPropietarioBodega = pBePedidoEnc.IdPropietarioBodega
                                            BeManufacturaDet.IdProductoBodega = pBePedidoDet.IdProductoBodega
                                            BeManufacturaDet.Codigo_producto = pBePedidoDet.Codigo_Producto
                                            BeManufacturaDet.Nombre_producto = pBePedidoDet.Nombre_producto
                                            BeManufacturaDet.Cantidad_esperada = pBePedidoDet.Cantidad
                                            BeManufacturaDet.Fec_agr = Now
                                            BeManufacturaDet.Fec_mod = Now
                                            BeManufacturaDet.User_agr = AP.UsuarioAp.IdUsuario
                                            BeManufacturaDet.User_mod = AP.UsuarioAp.IdUsuario

                                            clsLnTrans_manufactura_det.Insertar(BeManufacturaDet, clsTrans.lConnection, clsTrans.lTransaction)

                                            clsTrans.Commit_Transaction()

                                        Catch ex As Exception
                                            If Not clsTrans Is Nothing Then
                                                clsTrans.RollBack_Transaction()
                                            End If
                                        Finally
                                            clsTrans.Close_Conection()
                                        End Try

                                    End If
                                End If

                                '#CM_CK_20180917_12:59PM: Inicializamos las clases pBeProducto y pBeStock porque se quedaba con información anterior 
                                pBeStock = New clsBeStock
                                pBeProducto = Nothing
                                'vIdPedidoDet = pBePedidoDet.IdPedidoDet

                                Debug.WriteLine("Idx: " & Idx & " Cant. filas grid: " & dgrid.Rows.Count)

                                dgrid.Refresh()

                                Application.DoEvents()

                                dgrid.EndEdit(DataGridViewDataErrorContexts.Commit)

                            Else
                                Throw New Exception("Error_20220203_1115: No se pudo realizar la reserva de stock para el código:" + pObjVWStock.Codigo_Producto)
                            End If

                        Next

                        Carga_Stock_Reservado() : Cargar_Picking()

                        SplashScreenManager.CloseForm(False)

                        'XtraMessageBox.Show("Se realizó la reserva de stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                    '#GT31102025: bloquear propietario cuando reserva stock, para evitar que cambie y tomen stock de otro distinto.
                    lcmbPropietario.Enabled = False

                End If

            End If

            frmSelStock.Hide()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Carga_Stock_Reservado(ByVal lConnection As SqlConnection,
                                      ByVal lTransaction As SqlTransaction)

        Try

            Dim ListStockRes As New List(Of clsBeVW_stock_res)

            ListStockRes = clsLnStock_res.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                 pBePedidoEnc.IdBodega,
                                                                 lConnection,
                                                                 lTransaction)

            DTStockRes.Rows.Clear()

            For Each BeVW_stock_res As clsBeVW_stock_res In ListStockRes

                DTStockRes.Rows.Add(BeVW_stock_res.IdStockRes,
                                    BeVW_stock_res.IdStock,
                                    BeVW_stock_res.Codigo_Producto,
                                    BeVW_stock_res.Nombre_Producto,
                                    BeVW_stock_res.NomEstado,
                                    BeVW_stock_res.Lote,
                                    BeVW_stock_res.Lic_plate,
                                    BeVW_stock_res.Fecha_Vence,
                                    BeVW_stock_res.Cantidad_Res,
                                    BeVW_stock_res.UMBas,
                                    BeVW_stock_res.CantidadPresentacion,
                                    BeVW_stock_res.Nombre_Presentacion,
                                    BeVW_stock_res.Peso,
                                    BeVW_stock_res.Ubicacion_Nombre,
                                    BeVW_stock_res.Host,
                                    BeVW_stock_res.no_linea,
                                    pBePedidoEnc.Referencia)
            Next

            grdStockReservado.DataSource = DTStockRes

            GridView6.OptionsView.ColumnAutoWidth = False
            GridView6.OptionsView.ShowFooter = True

            lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)

            GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
            GridView6.Columns("Cantidad_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

            GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"
            GridView6.Columns("Cantidad_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Cantidad_Pres").SummaryItem.DisplayFormat = "{0:n6}"

            GridView6.Columns("Peso").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"
            GridView6.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

            Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
              With {.FieldName = "Cantidad",
              .SummaryType = SummaryItemType.Sum,
              .DisplayFormat = "{0:n6}",
              .ShowInGroupColumnFooter = GridView6.Columns("Cantidad")}
            GridView6.GroupSummary.Add(item)

            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Peso",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Peso")}
            GridView6.GroupSummary.Add(item1)

            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Cantidad_UMBas",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_UMBas")}
            GridView6.GroupSummary.Add(item2)


            Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Cantidad_Pres",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_Pres")}
            GridView6.GroupSummary.Add(item3)

            GridView6.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Carga_Stock_Reservado()

        Try

            Dim ListStockRes As New List(Of clsBeVW_stock_res)

            ListStockRes = clsLnStock_res.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)

            DTStockRes.Rows.Clear()

            For Each BeVWStockRes As clsBeVW_stock_res In ListStockRes

                DTStockRes.Rows.Add(BeVWStockRes.IdStockRes,
                                    BeVWStockRes.IdStock,
                                    BeVWStockRes.Codigo_Producto,
                                    BeVWStockRes.Nombre_Producto,
                                    BeVWStockRes.NomEstado,
                                    BeVWStockRes.Lote,
                                    BeVWStockRes.Lic_plate,
                                    BeVWStockRes.Fecha_Vence,
                                    BeVWStockRes.Cantidad_Res,
                                    BeVWStockRes.UMBas,
                                    BeVWStockRes.CantidadPresentacion,
                                    BeVWStockRes.Nombre_Presentacion,
                                    BeVWStockRes.Peso,
                                    BeVWStockRes.Ubicacion_Nombre,
                                    BeVWStockRes.Host,
                                    BeVWStockRes.no_linea,
                                    pBePedidoEnc.Referencia)
            Next

            grdStockReservado.DataSource = DTStockRes

            GridView6.OptionsView.ColumnAutoWidth = False
            GridView6.OptionsView.ShowFooter = True

            lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)

            GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"

            GridView6.Columns("Cantidad_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

            GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"

            GridView6.Columns("Cantidad_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Cantidad_Pres").SummaryItem.DisplayFormat = "{0:n6}"

            GridView6.Columns("Peso").DisplayFormat.FormatType = FormatType.Numeric
            GridView6.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

            GridView6.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
            GridView6.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

            Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
              With {.FieldName = "Cantidad",
              .SummaryType = SummaryItemType.Sum,
              .DisplayFormat = "{0:n6}",
              .ShowInGroupColumnFooter = GridView6.Columns("Cantidad")}
            GridView6.GroupSummary.Add(item)

            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Peso",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Peso")}
            GridView6.GroupSummary.Add(item1)

            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Cantidad_UMBas",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_UMBas")}
            GridView6.GroupSummary.Add(item2)

            Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Cantidad_Pres",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n6}",
            .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_Pres")}
            GridView6.GroupSummary.Add(item3)

            GridView6.BestFitColumns()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Log_Reserva(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim DTLogReserva As New DataTable
        Dim vRegistros As Integer = 0

        Try

            If Not pBePedidoEnc Is Nothing Then

                DTLogReserva = clsLnTrans_pe_det_log_reserva.Get_All_By_IdPedidoEnc_And_Idbodedga(pBePedidoEnc.IdPedidoEnc,
                                                                                                  pBePedidoEnc.IdBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                GridView10.BeginUpdate()

                If Not DTLogReserva Is Nothing Then
                    dgrdLogReserva.DataSource = DTLogReserva

                    GridView10.OptionsView.ColumnAutoWidth = False
                    GridView10.OptionsView.ShowFooter = True

                    GridView10.BestFitColumns(True)

                    vRegistros = DTLogReserva.Rows.Count
                End If

                GridView10.EndUpdate()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private ReleaseRowPicking As Integer = -1
    Private Sub mnuLiberarNoPickea_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuLiberarNoPickeado.ItemClick

        Try

            mnuLiberarNoPickeado.Enabled = False

            'GT06042022: se debe hacer clic en el grid para que la fila a liberar sea la requerida y no la primera de la lista.
            'If Not Val(vIdPedidoDet = 0) Then
            If ReleaseRowPicking > -1 Then

                'Estoy validando...
                Get_ValoresGrid(ReleaseRowPicking)
                'Get_ValoresGrid(dgrid.CurrentRow.Index)

                If Val(pBePedidoEnc.IdPickingEnc > 0) Then

                    pBePedidoEnc.Picking = clsLnTrans_picking_enc.GetSingle(pBePedidoEnc.IdPickingEnc)

                    Dim vRowToRelease = pBePedidoEnc.Picking.ListaPickingUbic.Find(Function(x) x.CodigoProducto = vCodigoProducto _
                                                                                   AndAlso x.IdPickingEnc = pBePedidoEnc.IdPickingEnc _
                                                                                   AndAlso x.IdPedidoDet = vIdPedidoDet)


                    If vRowToRelease IsNot Nothing Then

                        If vRowToRelease.Cantidad_Recibida = vRowToRelease.Cantidad_Solicitada OrElse
                       vRowToRelease.Cantidad_Verificada = vRowToRelease.Cantidad_Solicitada Then

                            XtraMessageBox.Show("El producto " & vCodigoProducto & " tiene la cantidad pickeada completa, no se puede liberar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else

                            If XtraMessageBox.Show("¿Liberar producto no pickeado para la línea:" & vNoLinea & " Código:" & vCodigoProducto & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                If Not (clsLnTrans_picking_det.Liberar_Producto_No_Pickeado(vIdPedidoDet,
                                                                                            pBePedidoEnc.IdPedidoEnc,
                                                                                            pBePedidoEnc.IdPickingEnc,
                                                                                            AP.UsuarioAp.IdUsuario,
                                                                                            pBePedidoEnc.Referencia,
                                                                                            "mnuLiberarNoPickea_ItemClick",
                                                                                            cmbBodega.EditValue,
                                                                                            clsDataContractDI.tOpcionLiberaStock.Pedido)) Then

                                    '#MECR15102025: Se agrego bitacora de logs para pedidos
                                    Dim vMsgError As String = "No se pudo liberar_Producto_No_Pickeado para el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia
                                    'clsLnLog_error_wms.Agregar_Error(vMsgError)
                                    clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                                        pIdEmpresa:=AP.IdEmpresa,
                                                                        pIdBodega:=AP.IdBodega,
                                                                        pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                        pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                                    XtraMessageBox.Show("No se pudo liberar el producto del picking, valide que la línea:" & vNoLinea & " del código:" & vCodigoProducto & " no tenga despacho asociado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                Else
                                    XtraMessageBox.Show("Se ha liberado el producto del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Carga_Stock_Reservado() : Cargar_Picking()
                                End If

                            End If

                        End If
                    Else
                        XtraMessageBox.Show("La fila ya fue liberada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Else
                    XtraMessageBox.Show("El pedido no tiene picking activo asociado, no se puede liberar producto en picking.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else
                XtraMessageBox.Show("Seleccione una línea del pedido para liberar el producto no pickeado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            mnuLiberarNoPickeado.Enabled = True

        Catch ex As Exception
            mnuLiberarNoPickeado.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuReservaStockManual_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuReservaStockManual.ItemClick

        Try

            mnuReservaStockManual.Enabled = False

            Get_ValoresGrid(dgrid.CurrentRow.Index)

            If XtraMessageBox.Show("¿Reservar manualmente el stock?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Reservar_Stock_Por_Linea(dgrid.CurrentRow.Index, Propietario.IdPropietario) Then
                    Carga_Stock_Reservado()
                Else
                    XtraMessageBox.Show("No se pudo completar la reserva, revise existencias.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            mnuReservaStockManual.Enabled = True

        Catch ex As Exception
            mnuReservaStockManual.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Despacho(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim pIdPedidoEnc As Integer
        Dim lDespachos As New List(Of clsBeTrans_despacho_enc)

        Try
            ' Verificar si existe un pedido
            If Not pBePedidoEnc Is Nothing Then
                ' Obtener el ID del pedido
                pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

                ' Obtener todos los despachos relacionados con el pedido
                lDespachos = clsLnTrans_despacho_enc.Get_All_By_IdPedidoEnc(pIdPedidoEnc, lConnection, lTransaction)

                ' Limpiar los DataSets antes de cargar nuevos datos
                DsDespacho.Encabezado.Clear()
                DsDespacho.Detalle.Clear()

                ' Iterar sobre la lista de despachos
                For Each Despacho As clsBeTrans_despacho_enc In lDespachos

                    ' Asegurarse de que el despacho no sea nulo
                    If Despacho IsNot Nothing Then
                        ' Crear una nueva fila para el encabezado
                        Dim lRow As DataRow = DsDespacho.Encabezado.NewRow()

                        ' Asignar los valores del despacho a la fila
                        lRow.Item("IdDespachoEnc") = Despacho.IdDespachoEnc
                        lRow.Item("Fecha_Desp") = Despacho.Fecha
                        lRow.Item("Hora_ini") = Despacho.Hora_ini
                        lRow.Item("Hora_fin") = Despacho.Hora_fin
                        lRow.Item("Estado") = Despacho.Estado
                        lRow.Item("IdPedidoEnc") = pIdPedidoEnc

                        ' Agregar la fila al DataSet
                        DsDespacho.Encabezado.AddEncabezadoRow(lRow)

                        ' Cargar el detalle del despacho
                        Cargar_Detalle_Despacho(Despacho.IdDespachoEnc, DsDespacho, lConnection, lTransaction)

                        ' Desplegar el ID del último despacho en la interfaz
                        txtIdDespacho.Text = Despacho.IdDespachoEnc

                        ' Verificar si se debe mostrar el botón de revertir despacho
                        If Not clsLnTrans_oc_enc.Existe_Devolucion_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc) Then
                            mnuRevertirDespacho.Visibility = BarItemVisibility.Always
                        End If

                        ' Mostrar la pestaña de despachos
                        tpDespachos.PageVisible = True
                    Else
                        ' Si no hay despachos, ocultar la pestaña de despachos
                        tpDespachos.PageVisible = False
                        mnuRevertirDespacho.Visibility = BarItemVisibility.Never
                    End If
                Next
            End If

        Catch ex As Exception
            ' Mostrar un mensaje de error en caso de excepción
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            ' Registrar el error en el log
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub Cargar_Despacho()

        Dim pIdPedidoEnc As Integer
        Dim lDespachos As New List(Of clsBeTrans_despacho_enc)
        Dim cTrans As New clsTransaccion

        Try

            ' Verificar si existe un pedido
            If Not pBePedidoEnc Is Nothing Then
                ' Obtener el ID del pedido
                pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

                cTrans.Open_Connection() : cTrans.Begin_Transaction()
                ' Obtener todos los despachos relacionados con el pedido
                lDespachos = clsLnTrans_despacho_enc.Get_All_By_IdPedidoEnc(pIdPedidoEnc, cTrans.lConnection, cTrans.lTransaction)

                ' Limpiar los DataSets antes de cargar nuevos datos
                Try
                    DsDespacho.Clear()
                    DsDespacho.Encabezado.Clear()
                    DsDespacho.Detalle.Clear()
                Catch ex As Exception

                End Try

                ' Iterar sobre la lista de despachos
                For Each Despacho As clsBeTrans_despacho_enc In lDespachos

                    ' Asegurarse de que el despacho no sea nulo
                    If Despacho IsNot Nothing Then
                        ' Crear una nueva fila para el encabezado
                        Dim lRow As DataRow = DsDespacho.Encabezado.NewRow()

                        ' Asignar los valores del despacho a la fila
                        lRow.Item("IdDespachoEnc") = Despacho.IdDespachoEnc
                        lRow.Item("Fecha_Desp") = Despacho.Fecha
                        lRow.Item("Hora_ini") = Despacho.Hora_ini
                        lRow.Item("Hora_fin") = Despacho.Hora_fin
                        lRow.Item("Estado") = Despacho.Estado
                        lRow.Item("IdPedidoEnc") = pIdPedidoEnc

                        ' Agregar la fila al DataSet
                        DsDespacho.Encabezado.AddEncabezadoRow(lRow)

                        ' Cargar el detalle del despacho
                        Cargar_Detalle_Despacho(Despacho.IdDespachoEnc, DsDespacho, cTrans.lConnection, cTrans.lTransaction)

                        ' Desplegar el ID del último despacho en la interfaz
                        txtIdDespacho.Text = Despacho.IdDespachoEnc

                        ' Verificar si se debe mostrar el botón de revertir despacho
                        If Not clsLnTrans_oc_enc.Existe_Devolucion_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc, cTrans.lConnection, cTrans.lTransaction) Then
                            mnuRevertirDespacho.Visibility = BarItemVisibility.Always
                        End If

                        ' Mostrar la pestaña de despachos
                        tpDespachos.PageVisible = True
                    Else
                        ' Si no hay despachos, ocultar la pestaña de despachos
                        tpDespachos.PageVisible = False
                        mnuRevertirDespacho.Visibility = BarItemVisibility.Never
                    End If
                Next
            End If

            cTrans.Commit_Transaction()

        Catch ex As Exception
            cTrans.RollBack_Transaction()
            ' Mostrar un mensaje de error en caso de excepción
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            ' Registrar el error en el log
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        Finally
            cTrans.Close_Conection()
        End Try

    End Sub
    Private Sub Cargar_Ultimo_Despacho()

        Dim lRow As DataRow
        Dim pIdPedidoEnc As Integer
        Dim Despacho As New clsBeTrans_despacho_enc

        Try

            pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

            Despacho = clsLnTrans_despacho_enc.Get_Single_By_IdPedidoEnc(pIdPedidoEnc)

            DsDespacho.Detalle.Clear() : DsDespacho.Encabezado.Clear()

            If Despacho IsNot Nothing Then

                lRow = DsDespacho.Encabezado.NewRow
                lRow.Item("IdDespachoEnc") = Despacho.IdDespachoEnc
                lRow.Item("Fecha_Desp") = Despacho.Fecha
                lRow.Item("Hora_ini") = Despacho.Hora_ini
                lRow.Item("Hora_fin") = Despacho.Hora_fin
                lRow.Item("Estado") = Despacho.Estado
                lRow.Item("IdPedidoEnc") = pIdPedidoEnc
                DsDespacho.Encabezado.AddEncabezadoRow(lRow)

                Cargar_Detalle_Despacho(Despacho.IdDespachoEnc)

                '#EJC20181906: Desplegar último despacho en tab de datos generales del pedido
                txtIdDespacho.Text = Despacho.IdDespachoEnc

                '#EJC20181906: Desplegar condicionalmente el tab de despachos, solo si tiene despacho
                tpDespachos.PageVisible = True
            Else
                tpDespachos.PageVisible = False
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Cargar_Detalle_Despacho(ByVal IdDespachoEnc As Integer)

        Dim lRow As DataRow
        Dim pIdPedidoEnc As Integer
        Dim ListDespacho As New List(Of clsBeTrans_despacho_det)

        Try

            pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

            ListDespacho = clsLnTrans_despacho_det.Get_All_By_IdPedidoEnc(pIdPedidoEnc)

            If ListDespacho.Count > 0 Then

                DsDespacho.Detalle.Clear()

                For Each Obj As clsBeTrans_despacho_det In ListDespacho.Where(Function(x) x.IdDespachoEnc = IdDespachoEnc)

                    lRow = DsDespacho.Detalle.NewRow
                    lRow.Item("IdDespachoEnc") = Obj.IdDespachoEnc
                    lRow.Item("IdPickingUbic") = Obj.IdPickingUbic
                    lRow.Item("IdPedidoEnc") = Obj.IdPedidoEnc
                    lRow.Item("Codigo") = Obj.Codigo
                    lRow.Item("Producto") = Obj.NombreProducto
                    lRow.Item("Estado") = Obj.NombreEstado
                    lRow.Item("Fecha_Despacho") = Obj.Fecha
                    lRow.Item("CantidadDespachada") = Obj.CantidadDespachada
                    lRow.Item("PesoDespachado") = Obj.PesoDespachado
                    lRow.Item("Lote") = Obj.Lote
                    lRow.Item("Lic_plate") = Obj.Lic_plate

                    DsDespacho.Detalle.AddDetalleRow(lRow)

                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Cargar_Detalle_Despacho(ByVal IdDespachoEnc As Integer, ByRef DsDespacho As DsDespacho, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim lRow As DataRow
        Dim pIdPedidoEnc As Integer
        Dim ListDespacho As New List(Of clsBeTrans_despacho_det)

        Try

            pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

            ListDespacho = clsLnTrans_despacho_det.Get_All_By_IdPedidoEnc(pIdPedidoEnc,
                                                                          IdDespachoEnc,
                                                                          lConnection,
                                                                          lTransaction)

            If ListDespacho.Count > 0 Then

                For Each Obj As clsBeTrans_despacho_det In ListDespacho

                    lRow = DsDespacho.Detalle.NewRow()
                    lRow.Item("IdDespachoEnc") = Obj.IdDespachoEnc
                    lRow.Item("IdPickingUbic") = Obj.IdPickingUbic
                    lRow.Item("IdPedidoEnc") = Obj.IdPedidoEnc
                    lRow.Item("Codigo") = Obj.Codigo
                    lRow.Item("Producto") = Obj.NombreProducto
                    lRow.Item("Estado") = Obj.NombreEstado
                    lRow.Item("Fecha_Despacho") = Obj.Fecha
                    lRow.Item("CantidadDespachada") = Obj.CantidadDespachada
                    lRow.Item("PesoDespachado") = Obj.PesoDespachado
                    lRow.Item("Lote") = Obj.Lote
                    lRow.Item("Lic_plate") = Obj.Lic_plate
                    lRow.Item("Presentacion") = Obj.ProductoPresentacion

                    DsDespacho.Detalle.AddDetalleRow(lRow)

                Next

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtIdPicking_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles txtIdPicking.LinkClicked
        Process_Picking()
    End Sub

    Public Sub Actualizar_Info_Picking()

        Try

            pBePedidoEnc.IdPickingEnc = clsLnTrans_pe_enc.Get_IdPicking_By_IdPedido(pBePedidoEnc.IdPedidoEnc)

            txtIdPicking.Text = pBePedidoEnc.IdPickingEnc
            txtReferencia.Text = pBePedidoEnc.Referencia

            Carga_Stock_Reservado()

            Cargar_Picking()

            If Val(txtIdPicking.Text) = 0 Then
                Debug.Print("Se anul? el picking de este pedido :)")
                txtIdPicking.LinkColor = Color.Red
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

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

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuPendiente_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuPendiente.ItemClick

        Try

            mnuPendiente.Enabled = False

            Dim vEstadoPedido As String = clsLnTrans_pe_enc.Get_Estado_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)

            If Not vEstadoPedido = "Despachado" Then

                If XtraMessageBox.Show("¿Modificar el pedido a pendiente de verificar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    '#EJC20210830: Estatus pendientes de pickear.
                    pBePedidoEnc.Estado = "Pickeado"

                    If clsLnTrans_pe_enc.Actualizar_Estado(pBePedidoEnc) > 0 Then

                        '#MECR15102025: Se agrego bitacora de logs para pedidos
                        Dim vMsgError As String = "Se actualizó a estado pickeado el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia
                        'clsLnLog_error_wms.Agregar_Error(vMsgError)
                        clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                            pIdEmpresa:=AP.IdEmpresa,
                                                            pIdBodega:=AP.IdBodega,
                                                            pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                            pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                        Cargar_Datos()

                        XtraMessageBox.Show("Se actualizó el pedido a estado pickeado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                End If

            Else

                XtraMessageBox.Show("El pedido fue despachado no se puede cambiar a estado pendiente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            mnuPendiente.Enabled = True

        Catch ex As Exception
            mnuPendiente.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuEstadoEnviadoAERP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEstadoEnviadoAERP.ItemClick

        Dim MarcarRegistrosNoEnviados As Boolean = True

        Try

            If pBePedidoEnc.Enviado_A_ERP Then

                If XtraMessageBox.Show("¿Cambiar el estado de envío del pedido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    pBePedidoEnc.Enviado_A_ERP = False

                    If XtraMessageBox.Show("¿Conservar estado (Enviado) en registros de interface?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        MarcarRegistrosNoEnviados = False
                    End If

                    If clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(pBePedidoEnc.IdPedidoEnc,
                                                                         pBePedidoEnc.Enviado_A_ERP,
                                                                         AP.UsuarioAp.IdUsuario,
                                                                         MarcarRegistrosNoEnviados) > 0 Then

                        Cargar_Datos()

                        XtraMessageBox.Show("Se actualizó el pedido a pendiente de envío", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                End If

            Else

                If XtraMessageBox.Show("¿Está seguro de cambiar el estado del pedido a enviado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    pBePedidoEnc.Enviado_A_ERP = True

                    If XtraMessageBox.Show("¿Conservar estado (No Enviado) en registros de interface?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        MarcarRegistrosNoEnviados = False
                    End If

                    If clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP(pBePedidoEnc.IdPedidoEnc, pBePedidoEnc.Enviado_A_ERP, AP.UsuarioAp.IdUsuario, MarcarRegistrosNoEnviados) > 0 Then

                        Cargar_Datos()

                        XtraMessageBox.Show("El pedido ha cambiado a estatus enviado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try

            IMS.Listar_Propietarios_By_IdBodega(lcmbPropietario, cmbBodega.EditValue)
            IMS.Listar_Muelles(cmbMuelle, cmbBodega.EditValue)

            '#EJC2019: Tzirin utiliza esta variable para consultar la existencia por bodega, pero
            'No llena la variable, entonces el IdBodega quedaba en 0, mostrando siempre existencia 0.
            pBePedidoEnc.IdBodega = cmbBodega.EditValue

        Catch ex As Exception

            SplashScreenManager.Default.CloseWaitForm()

            XtraMessageBox.Show(String.Format("Método: {0} Error: {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub GridView8_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView8.RowCellStyle

        Try

            Dim Existe As Boolean = False
            Dim View As GridView = sender
            Dim vCodigo As String = ""
            vCodigo = IIf(IsDBNull(View.Columns("Código")), "", View.Columns("Código"))

            Dim vCantidad As Object = IIf(IsDBNull(View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad"))), 0, View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad")))

            Dim vCantRes As Object = IIf(IsDBNull(View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad_Reservada"))), 0, View.GetRowCellDisplayText(e.RowHandle, View.Columns("Cantidad_Reservada")))

            If Not vCodigo Is Nothing Then

                If vCodigo <> "" Then

                    Dim CodigoPrd As Object = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Código"))

                    If e.Column.FieldName = "Código" Then

                        If pBePedidoEnc.Detalle.Exists(Function(x) x.Codigo_Producto = CodigoPrd) Then
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

                End If

            End If

            If vCantidad <> vCantRes Then
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.LightCoral
                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            Else
                e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub grdDespacho_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles grdDespacho.ViewRegistered

        Try

            Dim gridView As GridView = e.View

            gridView.BestFitColumns()

            gridView.OptionsView.ColumnAutoWidth = False
            gridView.BestFitColumns(True)

            gridView.OptionsView.ShowFooter = True

            gridView.Columns("Lote").VisibleIndex = 6
            gridView.Columns("Lic_plate").VisibleIndex = 7

            gridView.Columns("CantidadDespachada").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("CantidadDespachada").DisplayFormat.FormatString = "n2"

            gridView.Columns("CantidadDespachada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("CantidadDespachada").SummaryItem.DisplayFormat = "{0:n6}"

            gridView.Columns("CantidadDespachada").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("CantidadDespachada").DisplayFormat.FormatString = "{0:n6}"

            gridView.Columns("PesoDespachado").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("PesoDespachado").DisplayFormat.FormatString = "n2"

            gridView.Columns("PesoDespachado").DisplayFormat.FormatType = FormatType.Numeric
            gridView.Columns("PesoDespachado").DisplayFormat.FormatString = "{0:n6}"

            gridView.Columns("PesoDespachado").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            gridView.Columns("PesoDespachado").SummaryItem.DisplayFormat = "{0:n6}"



            gridView.BestFitColumns(True)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Set_Columnas_DT_ProductoComposicion()

        DTProductoKitComposicion.Columns.Add("NoLinea", GetType(String))
        DTProductoKitComposicion.Columns.Add("Padre", GetType(String))
        DTProductoKitComposicion.Columns.Add("Código", GetType(String))
        DTProductoKitComposicion.Columns.Add("Nombre", GetType(String))
        DTProductoKitComposicion.Columns.Add("U.M.Bas", GetType(String))
        DTProductoKitComposicion.Columns.Add("Cantidad_Kit", GetType(Double))
        DTProductoKitComposicion.Columns.Add("Cantidad_Disp", GetType(Double))


    End Sub

    Private Sub frmPedido_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Dim clsTransaccion As New clsTransaccion()
        IsLoading = True
        Dim hora_server As DateTime

        Try

            clsTransaccion.Begin_Transaction()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando datos...")

            pBeProducto = New clsBeProducto

            Set_Columnas_DT_StockRes()

            Set_Columnas_DT_ProductoComposicion()

            AP.Listar_Bodegas_By_Usuario(cmbBodega,
                                         clsTransaccion.lConnection,
                                         clsTransaccion.lTransaction)

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue,
                                                                        clsTransaccion.lConnection,
                                                                        clsTransaccion.lTransaction)

            '#EJC20210826: Si no hay propietarios por bodega, no continuar.
            If Not DT1 Is Nothing Then

                '#CKFK20181001: Colocar bodega por defecto.
                cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
                cmbBodega.RefreshEditValue()

                IMS.Listar_Propietarios_By_IdBodega(lcmbPropietario,
                                                    cmbBodega.EditValue,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)

                IMS.Listar_Muelles(cmbMuelle,
                                   cmbBodega.EditValue,
                                   clsTransaccion.lConnection,
                                   clsTransaccion.lTransaction)

                IMS.Listar_RoadRutas(cmbRoadRutaPedido,
                                     clsTransaccion.lConnection,
                                     clsTransaccion.lTransaction)

                IMS.Listar_RoadRutas(cmbRoadRutaDespacho,
                                     clsTransaccion.lConnection,
                                     clsTransaccion.lTransaction)

                IMS.Listar_VendedoresByRuta(cmbRoadVendedorPedido,
                                            cmbRoadRutaPedido.EditValue,
                                            clsTransaccion.lConnection,
                                            clsTransaccion.lTransaction)

                IMS.Listar_VendedoresByRuta(cmbRoadVendedorDespacho,
                                            cmbRoadRutaDespacho.EditValue,
                                            clsTransaccion.lConnection,
                                            clsTransaccion.lTransaction)

                IMS.Listar_VendedoresByRuta(cmbRoadVendedorDespacho,
                                            cmbRoadRutaDespacho.EditValue,
                                            clsTransaccion.lConnection,
                                            clsTransaccion.lTransaction)

                Application.DoEvents()

                IMS.Listar_Tipos_Manufactura_Ligera(cmbManufacturaLigera,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)

                'GT 090820211654: agregue esto para filtrar el tipo de doc según la bodega (fiscal o general)
                BeBodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue,
                                                             clsTransaccion.lConnection,
                                                             clsTransaccion.lTransaction)

                Application.DoEvents()

                If Not IMS.Listar_TiposPedido(cmbTipoPedido,
                                              BeBodega.Es_Bodega_Fiscal,
                                              clsTransaccion.lConnection,
                                              clsTransaccion.lTransaction) Then
                    Throw New Exception("No hay documentos definidos para la bodega " & AP.IdBodega & ", no se puede crear el pedido")
                End If

                Application.DoEvents()

                'GT 22012021 no esta incluido para pedido
                IMS.Listar_Regimen_Fiscal(cmbRegimen,
                                          clsTransaccion.lConnection,
                                          clsTransaccion.lTransaction)

                IsLoading = False

                dgrid.Columns("ColCantidad").DefaultCellStyle.BackColor = Color.OldLace
                dgrid.Columns("ColCantidad").DefaultCellStyle.ForeColor = Color.Black

                '#EJC20220327: Cambio por lookupedit. (antex textbox)
                If BeTipoDoc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor Then

                    IMS.Listar_Clientes_By_IdPropietario_By_EsProveedor(txtIdCliente,
                                                                        lcmbPropietario.GetColumnValue("IdPropietario"),
                                                                        cmbBodega.EditValue,
                                                                        BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS,
                                                                        True)

                Else

                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                End If

                IsLoading = True

                Application.DoEvents()

                '#EJC20210409:Servicios CEALSA.
                If AP.Bodega.Control_Tarifa_Servicios Then
                    Set_Datata_Table_Grid_Detalle_Servicios()
                    Set_Columnas_Grid_Detalle_Servicios()
                End If

                'GT 25012021 Grid Servicios
                dgridServiciosAsociados.DataSource = DTGridDetalleServicios
                gvDetalleServicios.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom

                pBeStock.ProductoEstado = New clsBeProducto_estado
                pBeStock.Presentacion = New clsBeProducto_Presentacion
                pBePedidoDetList = New List(Of clsBeTrans_pe_det)

                dgrid.Columns("ColCantidad").DefaultCellStyle.Format = "N2"
                dgrid.Columns("ColPrecio").DefaultCellStyle.Format = "N2"
                dgrid.Columns("ColTotal").DefaultCellStyle.Format = "N2"

                WindowState = FormWindowState.Maximized

                If AP.IdRol = 1 Then
                    mnuLiberarNoPickeado.Enabled = True
                    mnuReservaStockManual.Enabled = True
                Else
                    mnuLiberarNoPickeado.Enabled = False
                    mnuReservaStockManual.Enabled = False
                End If

                'Application.DoEvents()

                If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                    mnuEliminarPedido.Enabled = True
                Else
                    mnuEliminarPedido.Enabled = False
                End If

                AP.Bodega = clsLnBodega.GetSingle_By_Idbodega(cmbBodega.EditValue,
                                                              clsTransaccion.lConnection,
                                                              clsTransaccion.lTransaction)

                '#EJC20220223: Set TipoDocumentoSalida defecto, configurado por bodega.
                If (AP.Bodega.IdTipoTransaccionSalida > 0) Then
                    cmbTipoPedido.EditValue = AP.Bodega.IdTipoTransaccionSalida
                End If

                Set_Tipo_Documento()

                If cmbTipoPedido.ItemIndex = -1 Then
                    Preguntar_Al_Salir = False
                    Close()
                    Exit Sub
                End If

                'GT 210720211443: Si Pedido tiene tipo doc transferencia fiscal a general, se habilita el tab de poliza.
                Dim fila As Object = cmbTipoPedido.GetSelectedDataRow

                If Not fila Is Nothing Then
                    vControlPoliza = fila.Item("control_poliza")
                End If

                If vControlPoliza Then
                    chkControlPoliza.Checked = True
                    chkControlPoliza.Enabled = False
                    tabPoliza.Visible = True
                    tabPoliza.PageVisible = True
                    grpScanPoliza.Visible = True

                Else
                    txtScanPoliza.Visible = False
                    LabelControl2.Visible = False
                    grpScanPoliza.Visible = False
                    tabPoliza.Visible = False
                    tabPoliza.PageVisible = False
                    grpScanPoliza.Visible = False
                End If

                Application.DoEvents()

                Select Case Modo

                    Case TipoTrans.Nuevo

                        hora_server = clsServidor.Get_Fecha_Servidor(clsTransaccion.lConnection,
                                                                     clsTransaccion.lTransaction)
                        pBePedidoEnc.IsNew = True

                        lblIdPedidoEnc.Text = "-"
                        User_agrTextEdit1.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                        Fec_agrDateEdit1.Text = hora_server
                        User_modTextEdit1.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                        Fec_modDateEdit1.Text = hora_server
                        mnuGuardar.Enabled = True
                        cmdActualizar.Enabled = False
                        mnuEliminar.Enabled = False
                        cmdImprimir.Enabled = False
                        mnuAsignacion.Enabled = False
                        cmbBodega.Enabled = True
                        cmdEliminar.Enabled = False
                        dtpFechaPedido.DateTime = hora_server
                        dtpFechaEntrega.DateTime = hora_server

                        dtpFechaPreparacion.DateTime = New Date(1900, 1, 1)
                        dtpHoraInicioPreparacion.Value = New Date(1900, 1, 1, 0, 0, 0)
                        dtpHoraFinPreparacion.Value = New Date(1900, 1, 1, 0, 0, 0)

                        dtpFechaPreparacion.ReadOnly = True
                        dtpHoraInicioPreparacion.Enabled = False
                        dtpHoraFinPreparacion.Enabled = False

                        dtpHoraEntregaDesde.Value = hora_server
                        dtpHoraEntregaHasta.Value = hora_server

                        mnuDespachado.Visibility = BarItemVisibility.Never
                        mnuEliminarPedido.Visibility = BarItemVisibility.Never
                        mnuCrearPicking.Enabled = False

                        If Inserta_Encabezado(clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                            pBePedidoEnc.IsNew = False

                            '#MECR15102025: Se agrego bitacora de logs para pedidos
                            'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302271656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)
                            Dim msgAdvertencia As String = "ADVERTENCIA_202302271656: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " guardó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc
                            clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia,
                                                                pIdEmpresa:=AP.IdEmpresa,
                                                                pIdBodega:=AP.IdBodega,
                                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                                pConection:=clsTransaccion.lConnection,
                                                                pTransaction:=clsTransaccion.lTransaction)
                        End If

                        txtNoDocumento.Text = pBePedidoEnc.No_documento
                        lblIdPedidoEnc.Text = pBePedidoEnc.IdPedidoEnc

                        xtrPedido.TabPages.Item(3).Visible = False
                        xtrPedido.TabPages.Item(5).Visible = False

                        tabLogMI3.PageVisible = False
                        tabLogMI3.Visible = False

                        tabExistencias.PageVisible = False
                        tabExistencias.Visible = False

                        '#EJC20220819111: Requerimiento de Axel para CEALSA.
                        cmbBodega.EditValue = AP.IdBodega
                        cmbBodega.ReadOnly = True

                    Case TipoTrans.Editar

                        mnuGuardar.Enabled = False
                        cmdActualizar.Enabled = True
                        mnuEliminar.Enabled = True
                        mnuAsignacion.Enabled = True
                        cmdImprimir.Enabled = True

                        '#EJC20220509id
                        PedidoGuardadoPorUsuario = True

                        If pBePedidoEnc Is Nothing Then Exit Sub

                        Cargar_Datos(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        Application.DoEvents()

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormDescription("Cargando despacho. ")

                        Cargar_Despacho(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        Application.DoEvents()

                        SplashScreenManager.Default.SetWaitFormDescription("Manufactura...")

                        Cargar_Manufactura(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                        Application.DoEvents()

                        SplashScreenManager.Default.SetWaitFormDescription("Datos ERP. ")

                        If Carga_Datos_PedidoERP(clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                            xtrPedido.TabPages.Item(7).PageVisible = True
                            mnuEliminarPedidoTablaIntermedia.Enabled = True
                        Else
                            mnuEliminarPedidoTablaIntermedia.Enabled = False
                        End If

                        If pBePedidoEnc Is Nothing Then Exit Sub

                        mnuLiberarNoPickeado.Enabled = (pBePedidoEnc.IdPickingEnc > 0)

                        mnuEliminarPedido.Visibility = IIf(AP.Bodega.Permitir_Eliminar_Documento_Salida, BarItemVisibility.Always, BarItemVisibility.Never)

                        If pBePedidoEnc.Estado.ToUpper() = "DESPACHADO" Then

                            '#GT27052024: bloquear al editar un pedido despachado
                            lnkAgregarStockEspecifico.Enabled = False
                            lnkAgregarProducto.Enabled = False
                            lnkVerConfiguracionProducto.Enabled = False
                            lnkParametrosProducto.Enabled = False
                            txtScanPoliza.Enabled = False

                            mnuGuardar.Enabled = False
                            cmdActualizar.Enabled = False
                            mnuEliminar.Enabled = False
                            cmdEliminar.Enabled = False
                            mnuAsignacion.Enabled = False
                            dgrid.ReadOnly = True
                            '#EJC202308111207: Permitir liberar por despachos parciales.
                            mnuLiberarNoPickeado.Enabled = True
                            mnuReservaStockManual.Enabled = False
                            mnuDespachado.Visibility = BarItemVisibility.Never
                            mnuEliminarPedido.Visibility = BarItemVisibility.Never
                            '#EJC202308111207: Generar picking por despachos parciales.
                            mnuCrearPicking.Enabled = True

                        ElseIf pBePedidoEnc.Estado.ToUpper() = "ANULADO" Then

                            '#GT27052024: bloquear al abrir un pedido anulado
                            lnkAgregarStockEspecifico.Enabled = False
                            lnkAgregarProducto.Enabled = False
                            lnkVerConfiguracionProducto.Enabled = False
                            lnkParametrosProducto.Enabled = False
                            txtScanPoliza.Enabled = False

                            mnuGuardar.Enabled = False
                            cmdActualizar.Enabled = False
                            mnuEliminar.Enabled = False
                            cmdEliminar.Enabled = False
                            mnuAsignacion.Enabled = False
                            cmdImprimir.Enabled = False
                            dgrid.ReadOnly = True
                            RibbonPageGroup2.Enabled = False
                            RibbonPageGroup4.Enabled = False
                            RibbonPageGroup5.Enabled = False
                            RibbonPageGroup6.Enabled = False
                            mnuEliminarPedido.Visibility = BarItemVisibility.Always
                            mnuCrearPicking.Enabled = False

                        Else
                            mnuDespachado.Visibility = BarItemVisibility.Always
                        End If

                        SplashScreenManager.Default.SetWaitFormDescription("Picking. ")

                        'Cargar_Picking(clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        Dim task1 As Task = Task.Run(Sub()
                                                         Cargar_Picking()
                                                     End Sub)

                        SplashScreenManager.Default.SetWaitFormDescription("Reserva. ")

                        'Carga_Stock_Reservado(clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        Dim task2 As Task = Task.Run(Sub()
                                                         Carga_Stock_Reservado()
                                                     End Sub)

                        SplashScreenManager.Default.SetWaitFormDescription("Stock liberado. ")

                        'Cargar_Stock_Liberado(clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        Dim task3 As Task = Task.Run(Sub()
                                                         Cargar_Stock_Liberado()
                                                     End Sub)

                        SplashScreenManager.Default.SetWaitFormDescription("Hoja de verificación. ")

                        If Carga_Hoja_Verificacion(clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                            xtrPedido.TabPages.Item(12).PageVisible = True
                            tabHojaVerificacion.PageVisible = True
                        Else
                            xtrPedido.TabPages.Item(12).PageVisible = False
                            tabHojaVerificacion.PageVisible = False
                        End If

                        SplashScreenManager.Default.SetWaitFormDescription("Poliza. ")

                        '#EJC20210215: validar antes que no sea nothing
                        If Not pBePedidoEnc.ObjPoliza Is Nothing Then
                            'GT 15022021 carga datos de poliza si el registro tiene una asignada
                            If pBePedidoEnc.ObjPoliza.Dua <> "" Then
                                Dim task4a As Task = Task.Run(Sub()
                                                                  Cargar_Poliza()
                                                              End Sub)
                            End If
                        End If

                        '#GT11042023: set de la hora inicio y fin 
                        dtpHoraInicioPreparacion.Value = pBePedidoEnc.Hora_ini
                        dtpHoraFinPreparacion.Value = pBePedidoEnc.Hora_fin

                        dtpHoraInicioPreparacion.Enabled = False
                        dtpHoraFinPreparacion.Enabled = False

                        '#GT25042023: faltaba esta asignación al abrir un pedido para editar (porque inicialmente setea en tipo_documento() )
                        chkVerificar.Checked = pBePedidoEnc.Picking.verifica_auto

                        '#CKFK20231027 Asignar al pedido el valor del campo requerir_fotografia_verificacion del documento
                        chkFotografiaVerificacion.Checked = pBePedidoEnc.Picking.Fotografia_Verificacion

                        '#GT28052024: aqui probamos recargar acuerdos previamente registrados.
                        If AP.Bodega.Control_Tarifa_Servicios Then

                            If Llena_Servicios_By_Acuerdo_For_Combo() Then
                                Cargar_Servicios_Registrados()
                            End If

                        End If

                        SplashScreenManager.Default.SetWaitFormDescription("Existencias. ")

                        Dim task4 As Task = Task.Run(Sub()
                                                         Cargar_Existencias_Pedido()
                                                     End Sub)

                End Select

                Dim task5 As Task = Task.Run(Sub()
                                                 Equiparar_Cliente_Con_Propietario()
                                             End Sub)


                '#EJC20220113_0302AM: Mostrar tab de servicios según parametro.
                xtrPedido.TabPages.Item(9).PageVisible = AP.Bodega.Control_Tarifa_Servicios
                TabServiciosAsociados.Visible = AP.Bodega.Control_Tarifa_Servicios

                xtrPedido.SelectedTabPageIndex = 0
                txtTotalLineas.Enabled = False
                txtTotalBulto.Enabled = False

                txtNoDocumento.Focus()

            End If

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception

            clsTransaccion.RollBack_Transaction()

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

            If ex.Message.Contains("No hay documentos definidos para la bodega " & AP.IdBodega) Then
                Close()
            End If

        Finally
            SplashScreenManager.CloseForm(False)
            clsTransaccion.Close_Conection()
            IsLoading = False
        End Try

    End Sub

    Public Class clsBeHojaVerificacion

        Public Property CodigoProducto As String = ""
        Public Property Nombre As String = ""
        Public Property NombrePresentacion As String = ""
        Public Property CantidadPresentacion As Double = 0
        Public Property CantidadUMBas As Double = 0
        Public Property Peso As Double = 0
        Public Property Factor As Double = 0
        Public Property Referencia As String = ""

        Public Property Observaciones As String = ""
        Public Property RequiereTarimas As Boolean = False

        Public Property TieneBono As Boolean = False

    End Class

    Private Function Carga_Hoja_Verificacion(ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As Boolean

        Carga_Hoja_Verificacion = False

        Try

            Dim lHojaVerificacion As New List(Of clsBeHojaVerificacion)
            Dim BeHojaVerificacion As New clsBeHojaVerificacion
            Dim vDeltaFactorPresentacion As Double = 0
            Dim BePresentacionDefecto As New clsBeProducto_Presentacion()
            Dim vCantidadEnteraPresentacion As Double = 0
            Dim vCantidadSobranteUnidades As Double = 0
            Dim pesoUnidad As Double = 0
            Dim vFactorQuintal As Double = 45.359237
            Dim vListaPickingHojaVerif As New List(Of clsBeTrans_picking_ubic)

            'Where(Function(x) x.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc).

            If pBePedidoEnc.Picking.ListaPickingUbic.Count = 0 Then

                Dim vReservaPedidoDT As DataTable
                Dim BePickingUbic As New clsBeTrans_picking_ubic

                vReservaPedidoDT = clsLnStock_res.Get_All_Reserva_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)

                If vReservaPedidoDT IsNot Nothing AndAlso vReservaPedidoDT.Rows.Count > 0 Then

                    For Each lRow As DataRow In vReservaPedidoDT.Rows

                        BePickingUbic = New clsBeTrans_picking_ubic
                        clsLnTrans_picking_ubic.Cargar(BePickingUbic, lRow)
                        BePickingUbic.CodigoProducto = lRow("CodigoProducto")
                        BePickingUbic.NombreProducto = lRow("NombreProducto")
                        vListaPickingHojaVerif.Add(BePickingUbic)

                    Next

                End If


            Else
                vListaPickingHojaVerif = pBePedidoEnc.Picking.ListaPickingUbic
            End If

            For Each Det In vListaPickingHojaVerif.Where(Function(x) x.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc).OrderBy(Function(x) x.CodigoProducto)

                BeHojaVerificacion = New clsBeHojaVerificacion()
                BeHojaVerificacion.CodigoProducto = Det.CodigoProducto
                BeHojaVerificacion.Nombre = Det.NombreProducto
                BeHojaVerificacion.Observaciones = txtObservacion.Text
                BeHojaVerificacion.RequiereTarimas = chkRequiereTarimas.Checked

                If Det.IdProducto = 0 Then
                    Det.IdProducto = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(Det.IdProductoBodega,
                                                                                             lConnection,
                                                                                             lTransaction)
                End If

                BePresentacionDefecto = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(Det.IdProducto,
                                                                                                          lConnection,
                                                                                                          lTransaction)

                If Det.IdPresentacion = 0 Then

                    If Not BePresentacionDefecto Is Nothing Then
                        '#EJC20220406
                        'BeHojaVerificacion.Factor = BePresentacionDefecto.Factor

                        If Det.Cantidad_Solicitada > BePresentacionDefecto.Factor Then

                            vDeltaFactorPresentacion = Math.Round(Det.Cantidad_Solicitada / BePresentacionDefecto.Factor, 6)

                            vCantidadEnteraPresentacion = Math.Truncate(vDeltaFactorPresentacion)

                            vCantidadSobranteUnidades = Math.Round(Math.Abs((vCantidadEnteraPresentacion - vDeltaFactorPresentacion) * BePresentacionDefecto.Factor))

                            Dim vFactorDeRelacionUnidades As Double = 0

                            If vCantidadSobranteUnidades = 0 Then
                                BeHojaVerificacion.CantidadPresentacion = vCantidadEnteraPresentacion
                                BeHojaVerificacion.CantidadUMBas = 0
                                BeHojaVerificacion.Peso = Math.Round(vCantidadEnteraPresentacion * BePresentacionDefecto.Peso, 6)
                            Else
                                BeHojaVerificacion.CantidadPresentacion = vCantidadEnteraPresentacion
                                BeHojaVerificacion.CantidadUMBas = vCantidadSobranteUnidades
                                BeHojaVerificacion.Peso = Math.Round(vCantidadEnteraPresentacion * BePresentacionDefecto.Peso, 6)
                                Dim vPesoReferencia As Double = clsLnProducto.get_Peso_Referencia(Det.IdProducto, lConnection, lTransaction)
                                BeHojaVerificacion.Peso += Math.Round(vCantidadSobranteUnidades * vPesoReferencia, 6)
                            End If

                            BeHojaVerificacion.NombrePresentacion = BePresentacionDefecto.Nombre
                            BeHojaVerificacion.Factor = BePresentacionDefecto.Factor

                        Else
                            pesoUnidad = BePresentacionDefecto.Peso / BePresentacionDefecto.Factor
                            BeHojaVerificacion.CantidadUMBas = Det.Cantidad_Solicitada
                            BeHojaVerificacion.Factor = BePresentacionDefecto.Factor
                            BeHojaVerificacion.Peso = Det.Cantidad_Solicitada * pesoUnidad
                        End If

                    Else
                        '#CKFK20230307 Aqui no se se debe buscar el peso unitarios
                        BeHojaVerificacion.CantidadUMBas = Det.Cantidad_Solicitada
                        BeHojaVerificacion.Peso = Det.Peso_solicitado
                    End If

                Else

                    Dim vPresentacion As New clsBeProducto_Presentacion
                    vPresentacion = clsLnProducto_presentacion.GetSingle(Det.IdPresentacion,
                                                                         lConnection,
                                                                         lTransaction)

                    BeHojaVerificacion.CantidadPresentacion = Det.Cantidad_Solicitada
                    BeHojaVerificacion.CantidadUMBas = 0

                    If Not vPresentacion Is Nothing Then
                        BeHojaVerificacion.NombrePresentacion = vPresentacion.Nombre
                        BeHojaVerificacion.Peso = Math.Round(Det.Cantidad_Solicitada * vPresentacion.Peso, 6)
                        BeHojaVerificacion.Factor = vPresentacion.Factor
                    Else
                        BeHojaVerificacion.NombrePresentacion = ""
                        BeHojaVerificacion.Peso = 0
                        BeHojaVerificacion.Factor = 0
                    End If

                End If

                '#EJC20220406: Convertir a quintales... BYB, parametrizar en el futuro.
                BeHojaVerificacion.Peso = Math.Round((BeHojaVerificacion.Peso / 1000) / vFactorQuintal, 6)
                BeHojaVerificacion.Referencia = pBePedidoEnc.Referencia
                BeHojaVerificacion.TieneBono = clsLnProducto.get_Tiene_Bono(Det.IdProducto, lConnection, lTransaction)
                lHojaVerificacion.Add(BeHojaVerificacion)

            Next

            Dim Lista = From i In lHojaVerificacion Group i By Keys = New With {Key i.CodigoProducto,
                                                                                Key i.Nombre,
                                                                                Key i.NombrePresentacion,
                                                                                Key i.Factor,
                                                                                Key i.Referencia,
                                                                                Key i.RequiereTarimas,
                                                                                Key i.Observaciones,
                                                                                Key i.TieneBono} Into Group
                        Select New With {Keys.CodigoProducto,
                                        Keys.Nombre,
                                        Keys.NombrePresentacion,
                                        Keys.Factor,
                                        .CantidadPresentacion = Group.Sum(Function(x) x.CantidadPresentacion),
                                        .CantidadUMBas = Group.Sum(Function(x) x.CantidadUMBas),
                                        .Peso = Group.Sum(Function(x) x.Peso),
                                        Keys.Referencia,
                                        Keys.RequiereTarimas,
                                        Keys.Observaciones,
                                        Keys.TieneBono}

            dgridVerificacion.DataSource = Lista

            GridView9.OptionsView.ColumnAutoWidth = False
            GridView9.OptionsView.ShowFooter = True

            lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)

            If GridView9.Columns.Count > 0 Then

                GridView9.Columns("CodigoProducto").Caption = "Código"
                GridView9.Columns("NombrePresentacion").Caption = "Presentación"
                GridView9.Columns("RequiereTarimas").Caption = "Tarimas"
                GridView9.Columns("RequiereTarimas").Visible = False
                GridView9.Columns("Observaciones").Visible = False
                GridView9.Columns("Referencia").Visible = False

                GridView9.Columns("CantidadUMBas").Caption = "Unidades"
                GridView9.Columns("CantidadUMBas").DisplayFormat.FormatType = FormatType.Numeric
                GridView9.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView9.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n2}"
                GridView9.Columns("CantidadUMBas").AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center

                GridView9.Columns("CantidadPresentacion").Caption = "Pres"
                GridView9.Columns("CantidadPresentacion").DisplayFormat.FormatType = FormatType.Numeric
                GridView9.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView9.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n2}"

                GridView9.Columns("Peso").DisplayFormat.FormatType = FormatType.Numeric
                GridView9.Columns("Peso").DisplayFormat.FormatString = "{0:n2}"
                GridView9.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView9.Columns("Peso").SummaryItem.DisplayFormat = "{0:n2}"

                GridView9.Columns("Factor").DisplayFormat.FormatType = FormatType.Numeric
                GridView9.Columns("Factor").SummaryItem.SummaryType = SummaryItemType.Sum

                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
              With {.FieldName = "CantidadUMBas",
              .SummaryType = SummaryItemType.Sum,
              .DisplayFormat = "{0:n2}",
              .ShowInGroupColumnFooter = GridView6.Columns("CantidadUMBas")}
                GridView9.GroupSummary.Add(item)

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Peso",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n2}",
            .ShowInGroupColumnFooter = GridView6.Columns("Peso")}
                GridView9.GroupSummary.Add(item1)

                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "CantidadUMBas",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n2}",
            .ShowInGroupColumnFooter = GridView6.Columns("CantidadUMBas")}
                GridView9.GroupSummary.Add(item2)


                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
            With {.FieldName = "Cantidad_Presentacion",
            .SummaryType = SummaryItemType.Sum,
            .DisplayFormat = "{0:n2}",
            .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_Presentacion")}
                GridView9.GroupSummary.Add(item3)


                GridView9.BestFitColumns()

            End If

            Carga_Hoja_Verificacion = (lHojaVerificacion.Count > 0)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub Equiparar_Cliente_Con_Propietario()

        Try


            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue,
                                                                                        AP.IdEmpresa)

            If Not BeConfigBodega Is Nothing Then

                If BeConfigBodega.equiparar_cliente_con_propietario_en_doc_salida Then


                    lcmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

                    Dim vNIT As String = ""

                    If Not lcmbPropietario.Tag Is Nothing Then

                        Dim BePropietario As New clsBePropietarios
                        BePropietario = clsLnPropietarios.GetSingle(lcmbPropietario.Tag)

                        If Not BePropietario Is Nothing Then
                            vNIT = BePropietario.NIT
                        End If

                    End If

                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         vNIT,
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                    txtIdCliente.EditValue = pBePedidoEnc.IdCliente


                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Stock_Liberado(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Try

            Dim lStockLiberado As New List(Of clsBeTrans_log_pedido_liberacion)
            lStockLiberado = clsLnTrans_log_pedido_liberacion.Get_All_By_IdPedidoEnc_And_IdBodega(pBePedidoEnc.IdPedidoEnc,
                                                                                                  pBePedidoEnc.IdBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)

            xtrPedido.TabPages.Item(10).PageVisible = False

            If Not lStockLiberado Is Nothing Then
                If lStockLiberado.Count > 0 Then
                    dgridStockLiberado.DataSource = lStockLiberado
                    xtrPedido.TabPages.Item(10).PageVisible = True
                    gvLogStockLiberado.BestFitColumns()
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Stock_Liberado()

        Try
            Dim lStockLiberado As List(Of clsBeTrans_log_pedido_liberacion) = Nothing

            ' Carga en segundo plano (solo datos)
            Task.Run(Sub()

                         lStockLiberado = clsLnTrans_log_pedido_liberacion.Get_All_By_IdPedidoEnc_And_IdBodega(pBePedidoEnc.IdPedidoEnc,
                                                                                                           pBePedidoEnc.IdBodega)

                         ' Volver al hilo de UI para manipular controles
                         Me.Invoke(Sub()

                                       xtrPedido.TabPages.Item(10).PageVisible = False

                                       If lStockLiberado IsNot Nothing AndAlso lStockLiberado.Count > 0 Then
                                           dgridStockLiberado.DataSource = lStockLiberado
                                           xtrPedido.TabPages.Item(10).PageVisible = True
                                           gvLogStockLiberado.BestFitColumns()
                                       End If

                                   End Sub)

                     End Sub)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    Private Sub Equiparar_Cliente_Con_Propietario(ByVal pNIT As String, ByVal Requerir_Cliente_Es_Bodega_WMS As Boolean)

        Dim vIdCorrelativoClientePorPropietario As Integer = 0

        Try

            Dim DT As DataTable = clsLnCliente.Get_All_Clientes_By_IdPropietario_And_IdBodega(True,
                                                                                              lcmbPropietario.Tag,
                                                                                              pNIT,
                                                                                              cmbBodega.EditValue,
                                                                                              Requerir_Cliente_Es_Bodega_WMS)

            If Not DT Is Nothing Then
                If DT.Rows.Count > 0 Then
                    vIdCorrelativoClientePorPropietario = IIf(IsDBNull(DT.Rows(0).Item("Correlativo")), 0, DT.Rows(0).Item("Correlativo"))
                End If
            End If

            If vIdCorrelativoClientePorPropietario <> 0 Then
                txtIdCliente.EditValue = vIdCorrelativoClientePorPropietario
            Else
                '#GT13082023: sino exite cliente para equiparar, dejar combo vacio, porque no se limpiaba y dejaba registro previo.
                txtIdCliente.Properties.DataSource = Nothing

                If Requerir_Cliente_Es_Bodega_WMS Then
                    Throw New Exception("ERROR_13082023: No existe un cliente asociado, para transferir de Fiscal a otra bodega. Cierre este pedido y verifique catálogo de clientes.")
                End If
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Poliza()

        Try

            Dim BeRegimen As New clsBeRegimen_fiscal

            If pBePedidoEnc.ObjPoliza IsNot Nothing Then

                BeRegimen = clsLnRegimen_fiscal.GetSingle_By_IdRegimen(pBePedidoEnc.ObjPoliza.IdRegimen)

                If Not BeRegimen Is Nothing Then
                    cmbRegimen.EditValue = BeRegimen.Codigo_regimen
                End If

                txtCodigoPoliza.Text = pBePedidoEnc.ObjPoliza.NoPoliza
                txtPaisProcedencia.Text = pBePedidoEnc.ObjPoliza.Pais_procede
                txtValorAduana.Value = pBePedidoEnc.ObjPoliza.Total_valoraduana
                txtValorFlete.Value = pBePedidoEnc.ObjPoliza.Total_flete
                txtTotalFOBUSD.Value = pBePedidoEnc.ObjPoliza.Total_usd
                txtNumeroDUA.Text = pBePedidoEnc.ObjPoliza.Dua
                dtFechaPoliza.EditValue = pBePedidoEnc.ObjPoliza.Fecha_poliza
                txtTipoCambio.Value = pBePedidoEnc.ObjPoliza.Tipo_cambio
                txtValorSeguro.Value = pBePedidoEnc.ObjPoliza.Total_seguro
                txtNumeroOrden.Text = pBePedidoEnc.ObjPoliza.numero_orden
                txtClaveAduana.Text = pBePedidoEnc.ObjPoliza.clave_aduana
                txtNitImpExp.Text = pBePedidoEnc.ObjPoliza.nit_imp_exp
                txtClase.Text = pBePedidoEnc.ObjPoliza.clase
                txtMod_transporte.Text = pBePedidoEnc.ObjPoliza.mod_transporte
                txtTotal_liquidar.Text = pBePedidoEnc.ObjPoliza.total_liquidar
                txtTotal_general.Text = pBePedidoEnc.ObjPoliza.total_general
                txtTotalPesoBruto.Value = pBePedidoEnc.ObjPoliza.Total_bultos_Peso
                txtTotalPesoNeto.Value = pBePedidoEnc.ObjPoliza.Total_bultos_Peso_Neto

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Scan_Poliza()

        Try

            Dim encabezado_duca As New clsBeCEALSA_DUCA_ENC
            Dim barra_poliza As String = txtScanPoliza.Text
            Dim vPolizaValida As Boolean = False

            If String.IsNullOrEmpty(barra_poliza) Then

                XtraMessageBox.Show("No hay póliza para leer", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else

                '#MECR15102025: Se agrego bitacora de logs para pedidos
                '#EJC202308211833:Loguear la barra escaneada para que no se pierda.
                'clsLnLog_error_wms.Agregar_Error(AP.IdEmpresa,
                '                                 cmbBodega.EditValue,
                '                                 "SCAN_POL " & barra_poliza,
                '                                 pBePedidoEnc.IdPedidoEnc,
                '                                 pBePedidoEnc.IdPickingEnc,
                '                                 0,
                '                                 AP.UsuarioAp.IdUsuario)

                Dim msgAdvertencia As String = "SCAN_POL " & barra_poliza
                clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia,
                                                    pIdEmpresa:=AP.IdEmpresa,
                                                    pIdBodega:=AP.IdBodega,
                                                    pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                    pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                Dim Nuevo_Formato_Duca = Formato_Nuevo_Duca(barra_poliza)

                If Nuevo_Formato_Duca Is Nothing Then

                    '#GT: si no es el nuevo formato, utilizar el viejo :)
                    encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)

                    Dim pPolizaExiste As New clsBeTrans_pe_pol
                    pPolizaExiste = clsLnTrans_pe_pol.Get_Single_By_No_Orden(encabezado_duca.Numero_Orden)

                    If Not pPolizaExiste Is Nothing Then

                        If XtraMessageBox.Show("¿La póliza de salida " & pPolizaExiste.numero_orden & " ya existe en otro documento, desea asociar de todas formas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '#MECR15102025: Se agrego bitacora de logs para pedidos
                            Dim pLog = "ADVERTENCIA_PEDIDO_31082023: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza registrada " & pPolizaExiste.numero_orden & " al pedido " & pBePedidoEnc.IdPedidoEnc
                            'clsLnLog_error_wms.Agregar_Error(pLog)
                            clsLnLog_error_wms_pe.Agregar_Error(pLog,
                                                                pIdEmpresa:=AP.IdEmpresa,
                                                                pIdBodega:=AP.IdBodega,
                                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)
                        Else
                            txtScanPoliza.Text = ""
                            encabezado_duca = Nothing
                            Exit Sub
                        End If
                    End If

                    encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20)
                    Dim Fecha_string = barra_poliza.Substring(30, 8)
                    encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                    encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                    'GT 29042021 se convierte a mayuscula el regimen.
                    Dim upper_regimen As String = barra_poliza.Substring(70, 5)
                    encabezado_duca.Regimen = upper_regimen.ToUpper()
                    encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                    encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2)
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

                    ''concatenación para fecha dd/mm/yyyy
                    Dim comodin As String = "/"
                    Dim dd As String = ""
                    Dim mm As String = ""
                    Dim anio As String = ""
                    dd = Fecha_string.ToString.Substring(0, 2)
                    mm = Fecha_string.ToString.Substring(2, 2)
                    anio = Fecha_string.ToString.Substring(4, 4)
                    '#CKFK20220215 Cambie la forma de generar la fecha
                    encabezado_duca.Fecha_Aceptacion = New Date(anio, mm, dd)

                Else

                    Dim pPolizaExiste As New clsBeTrans_pe_pol
                    pPolizaExiste = clsLnTrans_pe_pol.Get_Single_By_No_Orden(Nuevo_Formato_Duca.Numero_Orden)

                    If Not pPolizaExiste Is Nothing Then

                        If XtraMessageBox.Show("¿La póliza de salida " & pPolizaExiste.numero_orden & " ya existe en otro documento, desea asociar de todas formas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '#MECR15102025: Se agrego bitacora de logs para pedidos
                            Dim pLog = "ADVERTENCIA_PEDIDO_31082023: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza registrada " & pPolizaExiste.numero_orden & " al pedido " & pBePedidoEnc.IdPedidoEnc
                            'clsLnLog_error_wms.Agregar_Error(pLog)
                            clsLnLog_error_wms_pe.Agregar_Error(pLog,
                                                                pIdEmpresa:=AP.IdEmpresa,
                                                                pIdBodega:=AP.IdBodega,
                                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)
                        Else
                            txtScanPoliza.Text = ""
                            encabezado_duca = Nothing
                            Exit Sub
                        End If
                    End If

                    encabezado_duca = Nuevo_Formato_Duca

                End If



                'encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 10)

                'encabezado_duca.Numero_Orden = barra_poliza.Substring(0, 15)
                ''#GT31082023: Se valida si exite pedido con misma póliza, según el numero_orden
                'Dim pPolizaExiste As New clsBeTrans_pe_pol
                'pPolizaExiste = clsLnTrans_pe_pol.Get_Single_By_No_Orden(encabezado_duca.Numero_Orden)

                'If Not pPolizaExiste Is Nothing Then

                '    If XtraMessageBox.Show("¿La póliza de salida " & pPolizaExiste.numero_orden & " ya existe en otro documento, desea asociar de todas formas?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '        Dim pLog = "ADVERTENCIA_PEDIDO_31082023: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la póliza registrada " & pPolizaExiste.numero_orden & " al pedido " & pBePedidoEnc.IdPedidoEnc
                '        clsLnLog_error_wms.Agregar_Error(pLog)
                '    Else
                '        txtScanPoliza.Text = ""
                '        encabezado_duca = Nothing
                '        Exit Sub
                '    End If
                'End If


                '#GT. METODO ORIGINAL
                'encabezado_duca.Numero_DUCA = barra_poliza.Substring(10, 20)
                'Dim Fecha_string = barra_poliza.Substring(30, 8)
                'encabezado_duca.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                'encabezado_duca.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                ''GT 29042021 se convierte a mayuscula el regimen.
                'Dim upper_regimen As String = barra_poliza.Substring(70, 5)
                'encabezado_duca.Regimen = upper_regimen.ToUpper()
                'encabezado_duca.Clase = barra_poliza.Substring(75, 3).Trim()
                'encabezado_duca.Pais_procedencia = barra_poliza.Substring(78, 2)
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

                ''#GT. METODO NUEVO
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

                ''concatenación para fecha dd/mm/yyyy
                'Dim comodin As String = "/"
                'Dim dd As String = ""
                'Dim mm As String = ""
                'Dim anio As String = ""
                'dd = Fecha_string.ToString.Substring(0, 2)
                'mm = Fecha_string.ToString.Substring(2, 2)
                'anio = Fecha_string.ToString.Substring(4, 4)
                ''#CKFK20220215 Cambie la forma de generar la fecha
                'encabezado_duca.Fecha_Aceptacion = New Date(anio, mm, dd)





                'GT 22012021 Set de los inputs en el formulario desde la clase encabezado_duca
                txtNumeroOrden.Text = encabezado_duca.Numero_Orden
                txtNumeroDUA.Text = encabezado_duca.Numero_DUCA
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
                    Throw New Exception("El régimen: " & encabezado_duca.Regimen & " no esta registrado en Régimen Fiscal, o no es legible desde el archivo de importación")
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

                txtCodigoPoliza.Text = encabezado_duca.Codigo_Poliza

                xtrPedido.SelectedTabPageIndex = 1
                txtNoPoliza.Focus()

                If Not BeTipoDoc Is Nothing Then

                    If BeTipoDoc.Control_Poliza AndAlso BeBodega.Es_Bodega_Fiscal Then

                        Dim BePolizaExistente As New clsBeTrans_pe_pol()
                        BePolizaExistente = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)

                        If BePolizaExistente Is Nothing Then

                            pBePedidoEnc.ObjPoliza = New clsBeTrans_pe_pol()
                            pBePedidoEnc.ObjPoliza.IdOrdenPedidoPol = clsLnTrans_pe_pol.MaxID(pBePedidoEnc.IdPedidoEnc) + 1
                            pBePedidoEnc.ObjPoliza.IdOrdenPedidoEnc = pBePedidoEnc.IdPedidoEnc
                            pBePedidoEnc.ObjPoliza.NoPoliza = txtCodigoPoliza.Text.Trim
                            pBePedidoEnc.ObjPoliza.Pais_procede = txtPaisProcedencia.Text.Trim
                            pBePedidoEnc.ObjPoliza.Total_valoraduana = txtValorAduana.Value
                            pBePedidoEnc.ObjPoliza.Total_bultos_Peso = txtTotalPesoNeto.Value
                            pBePedidoEnc.ObjPoliza.Total_flete = txtValorFlete.Value
                            pBePedidoEnc.ObjPoliza.Total_usd = txtTotalFOBUSD.Value
                            pBePedidoEnc.ObjPoliza.Dua = txtNumeroDUA.Text.Trim
                            pBePedidoEnc.ObjPoliza.Fecha_poliza = dtFechaPoliza.EditValue
                            pBePedidoEnc.ObjPoliza.Tipo_cambio = txtTipoCambio.Value
                            pBePedidoEnc.ObjPoliza.Total_lineas = Val(txtTotalLineas.Value)
                            pBePedidoEnc.ObjPoliza.Total_bultos = Val(txtTotalBulto.Value)
                            pBePedidoEnc.ObjPoliza.Total_seguro = txtValorSeguro.Value
                            pBePedidoEnc.ObjPoliza.User_mod = AP.UsuarioAp.IdUsuario
                            pBePedidoEnc.ObjPoliza.Fec_mod = Now
                            pBePedidoEnc.Enviado_A_ERP = False

                            'GT 170820211743: Se obtiene el regimen, pero se valida que este seteado o avisar que la lectura de la poliza no lo asigno correctamente
                            Dim fila As Object = cmbRegimen.GetSelectedDataRow
                            Dim IdRegimen_ As Integer
                            If fila IsNot Nothing Then
                                IdRegimen_ = fila.Item("IdRegimen")
                            Else
                                XtraMessageBox.Show("No se encontró el regimen de la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                            'GT 08022021 se agrega el id regimen del combobox
                            pBePedidoEnc.ObjPoliza.IdRegimen = IdRegimen_
                            pBePedidoEnc.ObjPoliza.codigo_poliza = txtCodigoPoliza.Text.Trim
                            pBePedidoEnc.ObjPoliza.ticket = Val(txtTicket.Text.Trim)
                            pBePedidoEnc.ObjPoliza.numero_orden = txtNumeroOrden.Text.Trim
                            pBePedidoEnc.ObjPoliza.fecha_aceptacion = dtpFechaAceptacion.EditValue
                            pBePedidoEnc.ObjPoliza.fecha_llegada = dtpFechaLlegada.EditValue
                            pBePedidoEnc.ObjPoliza.total_otros = Val(txtTotalOtros.Value)
                            pBePedidoEnc.ObjPoliza.clave_aduana = txtClaveAduana.Text.Trim
                            pBePedidoEnc.ObjPoliza.nit_imp_exp = txtNitImpExp.Text.Trim
                            pBePedidoEnc.ObjPoliza.clase = txtClase.Text.Trim
                            pBePedidoEnc.ObjPoliza.mod_transporte = txtMod_transporte.Text.Trim
                            pBePedidoEnc.ObjPoliza.total_liquidar = Val(txtTotal_liquidar.EditValue)
                            pBePedidoEnc.ObjPoliza.total_general = Val(txtTotal_general.EditValue)

                            If Not pBePedidoEnc.ObjPoliza Is Nothing Then

                                clsLnTrans_pe_pol.Insertar(pBePedidoEnc.ObjPoliza)

                                XtraMessageBox.Show("Pedido actualizado con la poliza (A): " & pBePedidoEnc.ObjPoliza.numero_orden,
                                                    Text,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information)

                                vPolizaValida = True

                            Else
                                Throw New Exception("ERROR202308180940: La poliza está vacía.")
                            End If

                        Else

                            pBePedidoEnc.ObjPoliza = New clsBeTrans_pe_pol()
                            'pBePedidoEnc.ObjPoliza.IdOrdenPedidoPol = clsLnTrans_pe_pol.MaxID(pBePedidoEnc.IdPedidoEnc) + 1
                            pBePedidoEnc.ObjPoliza.IdOrdenPedidoPol = 1
                            pBePedidoEnc.ObjPoliza.IdOrdenPedidoEnc = pBePedidoEnc.IdPedidoEnc
                            pBePedidoEnc.ObjPoliza.NoPoliza = txtCodigoPoliza.Text.Trim
                            pBePedidoEnc.ObjPoliza.Pais_procede = txtPaisProcedencia.Text.Trim
                            pBePedidoEnc.ObjPoliza.Total_valoraduana = txtValorAduana.Value
                            pBePedidoEnc.ObjPoliza.Total_bultos_Peso = txtTotalPesoNeto.Value
                            pBePedidoEnc.ObjPoliza.Total_flete = txtValorFlete.Value
                            pBePedidoEnc.ObjPoliza.Total_usd = txtTotalFOBUSD.Value
                            pBePedidoEnc.ObjPoliza.Dua = txtNumeroDUA.Text.Trim
                            pBePedidoEnc.ObjPoliza.Fecha_poliza = dtFechaPoliza.EditValue
                            pBePedidoEnc.ObjPoliza.Tipo_cambio = txtTipoCambio.Value
                            pBePedidoEnc.ObjPoliza.Total_lineas = Val(txtTotalLineas.Value)
                            pBePedidoEnc.ObjPoliza.Total_bultos = Val(txtTotalBulto.Value)
                            pBePedidoEnc.ObjPoliza.Total_seguro = txtValorSeguro.Value
                            pBePedidoEnc.ObjPoliza.User_mod = AP.UsuarioAp.IdUsuario
                            pBePedidoEnc.ObjPoliza.Fec_mod = Now
                            pBePedidoEnc.Enviado_A_ERP = False

                            'GT 170820211743: Se obtiene el regimen, pero se valida que este seteado o avisar que la lectura de la poliza no lo asigno correctamente
                            Dim fila As Object = cmbRegimen.GetSelectedDataRow
                            Dim IdRegimen_ As Integer
                            If fila IsNot Nothing Then
                                IdRegimen_ = fila.Item("IdRegimen")
                            Else
                                XtraMessageBox.Show("No se encontró el regimen de la póliza.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If

                            'GT 08022021 se agrega el id regimen del combobox
                            pBePedidoEnc.ObjPoliza.IdRegimen = IdRegimen_
                            pBePedidoEnc.ObjPoliza.codigo_poliza = txtCodigoPoliza.Text.Trim
                            pBePedidoEnc.ObjPoliza.ticket = Val(txtTicket.Text.Trim)
                            pBePedidoEnc.ObjPoliza.numero_orden = txtNumeroOrden.Text.Trim
                            pBePedidoEnc.ObjPoliza.fecha_aceptacion = dtpFechaAceptacion.EditValue
                            pBePedidoEnc.ObjPoliza.fecha_llegada = dtpFechaLlegada.EditValue
                            pBePedidoEnc.ObjPoliza.total_otros = Val(txtTotalOtros.Value)
                            pBePedidoEnc.ObjPoliza.clave_aduana = txtClaveAduana.Text.Trim
                            pBePedidoEnc.ObjPoliza.nit_imp_exp = txtNitImpExp.Text.Trim
                            pBePedidoEnc.ObjPoliza.clase = txtClase.Text.Trim
                            pBePedidoEnc.ObjPoliza.mod_transporte = txtMod_transporte.Text.Trim
                            pBePedidoEnc.ObjPoliza.total_liquidar = Val(txtTotal_liquidar.EditValue)
                            pBePedidoEnc.ObjPoliza.total_general = Val(txtTotal_general.EditValue)

                            clsLnTrans_pe_pol.Actualizar(pBePedidoEnc.ObjPoliza)

                            XtraMessageBox.Show("Pedido actualizado con la poliza (B): " & pBePedidoEnc.ObjPoliza.numero_orden,
                                                 Text,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Information)

                            vPolizaValida = True

                        End If

                    End If

                End If

                '#EJC202308211828: Evitar perder el número de orden de la poliza.
                If vPolizaValida AndAlso txtReferencia.Text.Trim = "" Then
                    txtReferencia.Text = txtNumeroOrden.Text.Trim()
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub servicioGridLookUpEditDetalleServicio_Leave(ByVal sender As Object, ByVal e As EventArgs)

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
                drLineaRequisicion("IdOrdenPedidoServicio") = 0
                drLineaRequisicion("IsNewR") = True

                gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("cantidad")
                gvDetalleServicios.PostEditor()
            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub


    Private Sub servicioGridLookUpEdit_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then

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
                    drLineaRequisicion("IdOrdenPedidoServicio") = 0
                    drLineaRequisicion("IsNewR") = True

                    gvDetalleServicios.FocusedColumn = gvDetalleServicios.Columns("cantidad")
                    gvDetalleServicios.PostEditor()
                End If

            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub
    Private Sub Set_Datata_Table_Grid_Detalle_Servicios()

        DTGridDetalleServicios.Columns.Clear()


        DTGridDetalleServicios.Columns.Add("IdAcuerdoEnc", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("IdAcuerdoDet", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("servicio", GetType(String))
        DTGridDetalleServicios.Columns.Add("codigo_producto", GetType(String))
        DTGridDetalleServicios.Columns.Add("descripcion", GetType(String))
        DTGridDetalleServicios.Columns.Add("correlativo_detalleacuerdo", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("cantidad", GetType(Double))
        DTGridDetalleServicios.Columns.Add("IdOrdenPedidoServicio", GetType(Integer))
        DTGridDetalleServicios.Columns.Add("IsNewR", GetType(Boolean))

        'DTGridDetalleServicios.Columns.Add("IdAcuerdoDet", GetType(Integer))
        'DTGridDetalleServicios.Columns.Add("nombre_servicio", GetType(String))
        'DTGridDetalleServicios.Columns.Add("nombre_unidad", GetType(String))
        'DTGridDetalleServicios.Columns.Add("cantidad", GetType(Double))
        'DTGridDetalleServicios.Columns.Add("corre_detalleacuerdo", GetType(Integer))
        'DTGridDetalleServicios.Columns.Add("corre_catalogoproductos", GetType(Integer))

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

            Select Case Modo

                Case TipoTrans.Nuevo
                    ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(idPropietario,
                                                                                                                                            AP.Bodega.IdBodega)
                Case TipoTrans.Editar
                    ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(pBePedidoEnc.PropietarioBodega.Propietario.IdPropietario,
                                                                                                                                            pBePedidoEnc.IdBodega)
            End Select

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

            ColCodigoServicio.Width = 400
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
                .Width = 150
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
                .Width = 200
            }

            ColDescripcion.OptionsColumn.AllowEdit = False
            gvDetalleServicios.Columns.Add(ColDescripcion)
            VisibleIndex += 1

#End Region

#Region "Columna - Codigo Acuerdo"

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

    Private Sub cmbTipoPedido_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoPedido.EditValueChanged

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Cargando clientes...")

        Try

            Set_Tipo_Documento()

            If cmbTipoPedido.ItemIndex <> -1 Then

                Set_Tipo_Pedido()

                If BeTipoDoc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor Then
                    IMS.Listar_Clientes_By_IdPropietario_By_EsProveedor(txtIdCliente,
                                                                        lcmbPropietario.GetColumnValue("IdPropietario"),
                                                                        cmbBodega.EditValue,
                                                                        BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS,
                                                                        True)
                Else
                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub Set_Tipo_Documento()

        Try

            If cmbBodega.Text <> "" AndAlso cmbTipoPedido.EditValue <> 0 Then

                'GT 210720211443: Si Tipo Doc tiene  transferencia fiscal a general, se habilita el tab de poliza.
                Dim fila As Object = cmbTipoPedido.GetSelectedDataRow

                If fila IsNot Nothing Then

                    Dim vControl_Poliza As Boolean = fila.Item("control_poliza")
                    Dim vVerificar As Boolean = fila.Item("Verificar")
                    Dim vFotografiaVerificacion As Boolean = fila.Item("Fotografia_Verificacion")
                    Dim vEs_Devolucion As Boolean = fila.Item("es_devolucion")

                    If vControl_Poliza Then
                        chkControlPoliza.Checked = True
                        chkControlPoliza.Enabled = False
                    Else
                        chkControlPoliza.Checked = False
                        chkControlPoliza.Enabled = False
                    End If

                    cmbMotivoDevolucion.Visible = vEs_Devolucion
                    lblMotivoDevolucion.Visible = vEs_Devolucion

                    If vEs_Devolucion Then
                        Llena_Motivos_Devolucion()
                    End If

                    grpScanPoliza.Visible = vControl_Poliza
                    tabPoliza.Visible = vControl_Poliza
                    tabPoliza.PageVisible = vControl_Poliza
                    chkVerificar.Checked = vVerificar
                    chkFotografiaVerificacion.Checked = vFotografiaVerificacion
                Else
                    Throw New Exception("El tipo de documento no es válido, revise la configuración de la bodega, no se puede crear el pedido")
                End If

            End If

            xtrPedido.SelectedTabPageIndex = 0

            If cmbBodega.Text <> "" AndAlso cmbTipoPedido.EditValue <> 0 Then

                BeTipoDoc.IdTipoPedido = cmbTipoPedido.EditValue

                '#EJC20210716:Obtener parámetro control poliza y verificación auto en frmpedido.
                'El parametro verificación, se envía al picking, para determinar si hay o no verificación.
                If clsLnTrans_pe_tipo.GetSingle(BeTipoDoc) Then

                    If BeTipoDoc.Requerir_Documento_Ref Then
                        txtReferencia.BackColor = Color.LightPink
                        txtReferencia.ReadOnly = True
                        Llena_Documentos_Referencia_By_Bodega()
                    Else
                        txtReferencia.BackColor = Color.White

                        If (pBePedidoEnc.Referencia <> "" AndAlso AP.Bodega.Interface_SAP) Then
                            txtReferencia.ReadOnly = True
                        Else
                            txtReferencia.ReadOnly = False
                        End If
                    End If

                    lblDocumentoRef.Visible = BeTipoDoc.Requerir_Documento_Ref
                    cmbDocumentoRef.Visible = BeTipoDoc.Requerir_Documento_Ref

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub


    Private IsLoading As Boolean = False
    '#GT28052024: bandera para obtener propietario del combo
    Dim pIdPropietario As Integer = 0
    Private Sub lcmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles lcmbPropietario.EditValueChanged

        If lcmbPropietario.EditValue > 0 Then

            '#GT28052024: obtener el propietario para usarlo globalmente.
            Dim fila As Object = lcmbPropietario.GetSelectedDataRow
            If fila IsNot Nothing Then
                pIdPropietario = fila.Item("IdPropietario")
            End If

            If AP.Bodega.Control_Tarifa_Servicios Then

                Select Case Modo

                    Case TipoTrans.Nuevo

                        '#GT10062024: valida si hay varios acuerdos activos
                        If Not Llena_Servicios_By_Acuerdo_For_Combo() Then
                            '#GT13062024: Se debe alertar que no hay acuerdos comerciales??
                        End If

                End Select

            End If


        End If

        Set_Tipo_Pedido()

    End Sub

    Private Sub Set_Tipo_Pedido()

        Try

            If IsLoading Then Exit Sub

            'GT16082021: Se obtiene la configuración para validar si el id propietario es seteado a cliente
            BeConfigBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(cmbBodega.EditValue,
                                                                                        AP.IdEmpresa)
            If Not BeConfigBodega Is Nothing Then

                If BeConfigBodega.equiparar_cliente_con_propietario_en_doc_salida Then

                    '#GT03062024: ya no se cargan acuerdos en un combo, sino en el grid
                    'Llena_Acuerdos_Comerciales()

                    lcmbPropietario.Tag = IMS.Get_IdPropietario_By_IdBodega(cmbBodega.EditValue, lcmbPropietario.EditValue)

                    Dim vNIT As String = ""

                    If Not lcmbPropietario.Tag Is Nothing Then

                        Dim BePropietario As New clsBePropietarios
                        BePropietario = clsLnPropietarios.GetSingle(lcmbPropietario.Tag)

                        If Not BePropietario Is Nothing Then
                            vNIT = BePropietario.NIT
                        End If

                    End If

                    'GT2172022_1700: deje esto aca para que cargue el cliente en la modal o cuando agregue una linea de stock
                    IMS.Listar_Clientes_By_IdPropietario(txtIdCliente,
                                                         lcmbPropietario.GetColumnValue("IdPropietario"),
                                                         vNIT,
                                                         cmbBodega.EditValue,
                                                         BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                    Equiparar_Cliente_Con_Propietario(vNIT, BeTipoDoc.Requerir_Cliente_Es_Bodega_WMS)

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub


    ''#GT29052024: se cargan los acuerdos si es OC nueva
    'Private Sub Llena_Servicios_By_Propietario()

    '    Try

    '        ServicioGridLookUpEdit.ValueMember = "correlativo_detalleacuerdo"
    '        ServicioGridLookUpEdit.DisplayMember = "servicio"
    '        ServicioGridLookUpEdit.NullText = String.Empty
    '        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_AcuerdoDetalle_By_IdPropietario_And_IdBodega(pIdPropietario, AP.Bodega.IdBodega)
    '        ServicioGridLookUpEdit.BestFitMode = True

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub

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

                    If cmbAcuerdoComercial.Properties.Columns.Count > 0 Then
                        cmbAcuerdoComercial.Properties.Columns("IdAcuerdoEnc").Visible = False
                    End If

                    Select Case Modo

                        Case TipoTrans.Nuevo

                            cmbAcuerdoComercial.Enabled = True
                            ServicioGridLookUpEdit.DataSource = Nothing

                        Case TipoTrans.Editar

                            If pBePedidoEnc.IdAcuerdoComercial > 0 Then
                                cmbAcuerdoComercial.EditValue = pBePedidoEnc.IdAcuerdoComercial
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

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Function


    Private Sub LabelControl2_Click(sender As Object, e As EventArgs) 
        LabelControl2.Enabled = False
        Scan_Poliza()
        LabelControl2.Enabled = True
    End Sub

    Private Sub txtNoPoliza_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub txtScanPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtScanPoliza.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtScanPoliza.Text.Trim() <> "" Then
                Scan_Poliza()
            End If
        End If
    End Sub

    Private Sub Llena_Documentos_Referencia_By_Bodega()

        Try

            cmbDocumentoRef.Properties.DataSource = Nothing

            Dim l As New DataTable
            Dim vCodigoBodega As String = BeBodega.Codigo

            If Not pBeCliente Is Nothing Then

                Dim vCodigoCliente As String = pBeCliente.Codigo

                l = clsLnTrans_pe_docu_ref.Get_All_By_Codigo_Bodega_DT(vCodigoBodega, vCodigoCliente)

                If l.Rows.Count > 0 Then
                    cmbDocumentoRef.Visible = True
                    cmbDocumentoRef.Properties.ValueMember = "Codigo"
                    cmbDocumentoRef.Properties.DisplayMember = "Referencia_ERP"
                    cmbDocumentoRef.Properties.DataSource = l
                    cmbDocumentoRef.Enabled = True
                    cmbDocumentoRef.Properties.PopupWidth = 700
                    cmbDocumentoRef.Properties.PopulateColumns()
                    cmbDocumentoRef.Properties.BestFit()
                    cmbDocumentoRef.Properties.NullText = "Seleccione documento Ref."
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmbDocumentoRef_EditValueChanged(sender As Object, e As EventArgs) Handles cmbDocumentoRef.EditValueChanged

        Try

            Dim fila As Object = cmbDocumentoRef.GetSelectedDataRow
            Dim vReferencia As String = IIf(IsDBNull(fila.Item("referencia")), "", fila.Item("referencia"))

            txtReferencia.Text = vReferencia

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lnkUltDespacho_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Despacho_Link()
    End Sub

    Private Sub Despacho_Link()

        txtIdDespacho.Enabled = False

        Dim DT As New DataTable
        Dim BePicking As New clsBeTrans_picking_enc

        Try

            DT = clsLnTrans_pe_enc.Get_Single_Pedido_For_Despacho(pBePedidoEnc.IdPedidoEnc,
                                                                  pBePedidoEnc.IdPickingEnc,
                                                                  pBePedidoEnc.IdBodega)

            If Val(txtIdPicking.Text) <> 0 Then
                BePicking = clsLnTrans_picking_enc.GetSingle(pBePedidoEnc.IdPickingEnc)
            End If


            'Tiene registros pendientes de despahco.
            If DT.Rows.Count > 0 Then

                If Val(txtIdPicking.Text) <> 0 Then

                    If Not BePicking Is Nothing Then

                        If BePicking.Requiere_Preparacion Then

                            Dim lPacking As New List(Of clsBeTrans_packing_enc)
                            lPacking = clsLnTrans_packing_enc.Get_All_By_IdPicking(BePicking.IdPickingEnc, False, pBePedidoEnc.IdPedidoEnc)

                            If lPacking.Count = 0 Then
                                SplashScreenManager.CloseForm(False)
                                XtraMessageBox.Show("El picking asociado al pedido requiere preparación o packing y no se ha realizado, ingrese a tomwms en la handheld y seleccione Packing.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        Else

                            If mgr IsNot Nothing AndAlso mgr.IsSplashFormVisible Then
                                mgr.CloseWaitForm()
                            End If

                            Dim vDespachos As Integer = clsLnTrans_despacho_enc.Get_Count_Despacho_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)

                            If vDespachos > 0 Then

                                If pBePedidoEnc.Estado = "Verificado" Then

                                    If XtraMessageBox.Show("El pedido tiene registros pendientes de despacho, ¿generar nuevo despacho?",
                                                             Text,
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                        Nuevo_Despacho()
                                        Exit Sub

                                    End If

                                End If

                            End If

                        End If

                    End If

                End If

            End If

            If txtIdDespacho.Text.Trim <> "0" Then

                If mgr IsNot Nothing AndAlso Not mgr.IsSplashFormVisible Then
                    mgr.ShowWaitForm()
                    mgr.SetWaitFormDescription("Cargando Despacho...")
                End If

                Cierra_Instancia_Previa(frmDespacho)

                Abrir_Despacho(Val(txtIdDespacho.Text))

            Else

                If mgr IsNot Nothing AndAlso Not mgr.IsSplashFormVisible Then
                    mgr.ShowWaitForm()
                    mgr.SetWaitFormDescription("Generando Despacho...")
                End If

                If Val(txtIdPicking.Text) <> 0 Then

                    If Not pBePedidoEnc Is Nothing Then

                        '#GT10072025: refactorizado para validar ambas cosas en una linea
                        If Not DT Is Nothing AndAlso DT.Rows.Count > 0 Then

                            If Not BePicking Is Nothing Then

                                If BePicking.Requiere_Preparacion Then

                                    Dim lPacking As New List(Of clsBeTrans_packing_enc)
                                    lPacking = clsLnTrans_packing_enc.Get_All_By_IdPicking(BePicking.IdPickingEnc, False, pBePedidoEnc.IdPedidoEnc)

                                    If lPacking.Count = 0 Then
                                        SplashScreenManager.CloseForm(False)
                                        XtraMessageBox.Show("El picking asociado al pedido requiere preparación o packing y no se ha realizado, ingrese a tomwms en la handheld y seleccione Packing.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Return
                                    End If

                                End If

                            End If

                            If pBePedidoEnc.Estado = "Pendiente" Then

                                If XtraMessageBox.Show("Realizar despacho parcial?",
                                                             Text,
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    Nuevo_Despacho()
                                Else
                                    Exit Sub

                                End If

                            Else
                                Nuevo_Despacho()
                            End If

                            'Nuevo_Despacho()

                        Else
                            XtraMessageBox.Show("El picking o verificación no se han completado, no puede generarse el despacho", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    End If

                Else
                    XtraMessageBox.Show("El pedido no tiene picking asociado, no puede despacharse", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If mgr IsNot Nothing AndAlso mgr.IsSplashFormVisible Then
                mgr.CloseWaitForm()
            End If
            txtIdDespacho.Enabled = True
        End Try

    End Sub

    Private Sub Abrir_Despacho(ByVal IdDespacho As Integer)

        Try


            Dim Dr As DataRowView = GridView1.GetFocusedRow
            Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

            Cierra_Instancia_Previa(frmDespacho)

            With frmDespacho
                .Modo = frmDespacho.TipoTrans.Editar
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Despacho")
                .BeDespachoEnc = clsLnTrans_despacho_enc.GetSingle(IdDespacho)
                SplashScreenManager.CloseForm(False)
                .WindowState = FormWindowState.Maximized
                .InvokeCargarObjetoPedido = AddressOf Recargar_Objeto_Pedido
                .InvokeCargarPedido = AddressOf Cargar_Datos
                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Nuevo_Despacho()

        Try

            Cierra_Instancia_Previa(frmDespacho)

            With frmDespacho
                .Modo = frmDespacho.TipoTrans.Nuevo
                .WindowState = FormWindowState.Maximized
                .Activate()
                .Show()
                .BePedidoEnc = pBePedidoEnc
                .InvokeGetDespachoEnPedido = AddressOf Cargar_Despacho
                .InvokeActualizarStockReservadoEnPedido = AddressOf Carga_Stock_Reservado
                .InvokeCargarObjetoPedido = AddressOf Recargar_Objeto_Pedido
                .InvokeCargarPedido = AddressOf Cargar_Datos
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Actualizar_Pedido_Estado_Despachado_Manual() As Boolean

        Actualizar_Pedido_Estado_Despachado_Manual = False

        Try

            '#EJC20210830: Estutus pendientes.
            pBePedidoEnc.Estado = "Despachado"

            If clsLnTrans_pe_enc.Actualizar_Estado(pBePedidoEnc) > 0 Then

                Cargar_Datos()

                '#MECR15102025: Se agrego bitacora de logs para pedidos
                Dim vMsgError As String = "Se actualizó a despachado manualmente el pedido: " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia
                'clsLnLog_error_wms.Agregar_Error(vMsgError)
                clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                    pIdEmpresa:=AP.IdEmpresa,
                                                    pIdBodega:=AP.IdBodega,
                                                    pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                    pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                XtraMessageBox.Show("Se actualizó el pedido a despachado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Actualizar_Pedido_Estado_Despachado_Manual = True

            End If

        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuDespachado_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuDespachado.ItemClick

        Dim vMensaje1 As String = "Cambiar a estado despachado, esta acción liberará el stock no pickeado, ¿Continuar?"

        Try

            If solicitarClave() Then

                mnuDespachado.Enabled = False

                Dim stockres As Integer = clsLnTrans_pe_enc.Get_StockRes_By_IdPedido(pBePedidoEnc.IdPedidoEnc)

                If pBePedidoEnc.Estado = "Verificado" OrElse pBePedidoEnc.Estado = "Pendiente" OrElse
                    ((pBePedidoEnc.Estado = "Despachado" OrElse pBePedidoEnc.Estado = "Pickeado") AndAlso stockres = 0) Then

                    'Estoy validando...
                    Get_ValoresGrid(dgrid.CurrentRow.Index)

                    If Not BeBodega.Liberar_Stock_Despachos_Parciales Then
                        vMensaje1 = "¿Modificar el pedido a despachado?"
                    End If

                    If XtraMessageBox.Show(vMensaje1, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Dim lStockReservado As New List(Of clsBeVW_stock_res)
                        lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc)

                        If Not lStockReservado Is Nothing Then

                            If lStockReservado.Count > 0 Then

                                '#EJC202308111241: En bodega se agregó un parámetro (liberar_stock_despachos_parciales ) que definie, si al momento de cambiar a estado despachado
                                'El pedido, desde la pantalla de pedido, el stock no pickeado se libera automáticamente o se pregunta, si se quiere quedar con la reserva.
                                'OBS: Es útil, para una especie de Pedido de Cliente en Backorder
                                'Debería estar relacionado en el futuro con un cambio en el estado del pedido a Despachado Parcialmente.
                                If Not BeBodega.Liberar_Stock_Despachos_Parciales Then

                                    If XtraMessageBox.Show("¿Liberar producto no pickeado/Despachado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                        If Not (clsLnTrans_picking_det.Liberar_Producto_No_Pickeado_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                                   pBePedidoEnc.IdPickingEnc,
                                                                                                                   AP.UsuarioAp.IdUsuario,
                                                                                                                   pBePedidoEnc.Referencia,
                                                                                                                   "mnuDespachado_ItemClick",
                                                                                                                   cmbBodega.EditValue)) Then


                                            XtraMessageBox.Show("No se pudo liberar el producto del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                        Else
                                            Cerrar_Documento_SAP()
                                            If Actualizar_Pedido_Estado_Despachado_Manual() Then
                                                If Not InvokeListarPedidos Is Nothing Then
                                                    InvokeListarPedidos.Invoke
                                                End If
                                                Close()
                                            End If
                                        End If

                                    Else
                                        Cerrar_Documento_SAP()
                                        If Actualizar_Pedido_Estado_Despachado_Manual() Then
                                            If Not InvokeListarPedidos Is Nothing Then
                                                InvokeListarPedidos.Invoke
                                            End If
                                            Close()
                                        End If
                                    End If

                                Else

                                    If Not (clsLnTrans_picking_det.Liberar_Producto_No_Pickeado_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                               pBePedidoEnc.IdPickingEnc,
                                                                                                               AP.UsuarioAp.IdUsuario,
                                                                                                               pBePedidoEnc.Referencia,
                                                                                                               "mnuDespachado_ItemClick",
                                                                                                               cmbBodega.EditValue)) Then

                                        XtraMessageBox.Show("No se pudo liberar el producto del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    Else
                                        Cerrar_Documento_SAP()
                                        If Actualizar_Pedido_Estado_Despachado_Manual() Then
                                            If Not InvokeListarPedidos Is Nothing Then
                                                InvokeListarPedidos.Invoke
                                            End If
                                            Close()
                                        End If
                                    End If
                                End If

                            Else
                                Cerrar_Documento_SAP()
                                If Actualizar_Pedido_Estado_Despachado_Manual() Then
                                    If Not InvokeListarPedidos Is Nothing Then
                                        InvokeListarPedidos.Invoke
                                    End If
                                    Close()
                                End If

                            End If

                        End If

                    End If

                Else

                    XtraMessageBox.Show("#Error_20211201_1429: Pedido en estado " & pBePedidoEnc.Estado & ", no se puede actualizar a estado Despachado, valide", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            mnuDespachado.Enabled = True
        End Try

    End Sub

    Private Sub Cerrar_Documento_SAP()

        Try

            Dim BeConfigEnc As New clsBeI_nav_config_enc
            BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(pBePedidoEnc.IdBodega)

            If Not BeConfigEnc Is Nothing Then
                If BeConfigEnc.Interface_SAP Then
                    If Ejecutar_Interface("21-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then

                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Sub mnuLiberarStockProductoEspecifico_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuLiberarStockProductoEspecifico.ItemClick

        Dim BETrans_Log_Pedido_Liberacion As New clsBeTrans_log_pedido_liberacion()

        Try

            mnuLiberarStockTodoElPedido.Enabled = False

            If Val(pBePedidoEnc.IdPedidoEnc > 0) Then

                If Val(pBePedidoEnc.IdPickingEnc > 0) Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Liberando stock...")

                    If Not (clsLnTrans_picking_det.Liberar_Producto_No_Pickeado(vIdPedidoDet,
                                                                                pBePedidoEnc.IdPedidoEnc,
                                                                                pBePedidoEnc.IdPickingEnc,
                                                                                AP.UsuarioAp.IdUsuario,
                                                                                pBePedidoEnc.Referencia,
                                                                                "mnuLiberarStockTodoElPedido",
                                                                                cmbBodega.EditValue,
                                                                                clsDataContractDI.tOpcionLiberaStock.StockReservado) > 0) Then


                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("No se pudo liberar el producto del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If

                Else

                    Dim lStockReservado As New List(Of clsBeVW_stock_res)
                    lStockReservado = clsLnStock_res.Get_All_By_IdPedidoEnc_And_IdPedidoDet(pBePedidoEnc.IdPedidoEnc, vIdPedidoDet)

                    If Not lStockReservado Is Nothing Then

                        If lStockReservado.Count > 0 Then

                            If XtraMessageBox.Show(String.Format("¿Liberar stock reservado para el código:" & pBePedidoDet.Codigo_Producto & "?", pBeStockRes.IdStockRes), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                SplashScreenManager.Default.SetWaitFormDescription("Liberando stock...")

                                If clsLnStock_res.Eliminar_Stock_Res_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                    pBePedidoEnc.Referencia,
                                                                                    AP.UsuarioAp.IdUsuario,
                                                                                    "mnuLiberarStockTodoElPedido_ItemClick: No Picking asociado") Then
                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show("Se liberó el stock reservado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Carga_Stock_Reservado() : Cargar_Stock_Liberado()
                                Else
                                    SplashScreenManager.CloseForm(False)
                                    XtraMessageBox.Show("#202112271011: No se pudo liberar el stock reservado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If

                            End If

                        Else
                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("#202112271007A: El pedido no tiene stock reservado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    Else
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("#202112271007: El pedido no tiene stock reservado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If


                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se obtuvo el número de pedido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            mnuLiberarStockTodoElPedido.Enabled = True
        End Try

    End Sub

    Private Sub Llena_Estados_Grid(ByVal pIndex As Integer,
                                   lEstados As List(Of clsBeProducto_Estado_Cmb),
                                   Optional ByVal pIdEstado As Integer = 0)

        Try

            DgComboEstado = TryCast(dgrid.Rows(pIndex).Cells("colEstadoProducto"), DataGridViewComboBoxCell)

            DgComboEstado.DropDownWidth = 200

            If Not pBeProducto Is Nothing Then

                Dim lEstado = lEstados.FindAll(Function(x) x.IdProductoBodega = pBeProducto.IdProductoBodega)

                If pIdEstado <> 0 Then
                    lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
                End If

                DgComboEstado.DataSource = lEstado
                DgComboEstado.ValueMember = "IdEstado"
                DgComboEstado.DisplayMember = "Nombre"

                If DgComboEstado.Items.Count > 0 Then
                    DgComboEstado.Value = lEstado(0).IdEstado
                Else
                    '#EJC20171024_1136PM:Corrección para cuando se cambia a un Código de producto que no tiene stock y por lo tanto no tiene estado.
                    DgComboEstado.Value = Nothing
                End If

                If pIdEstado <> 0 Then
                    If DgComboEstado.Items.Count > 0 Then
                        DgComboEstado.Value = pIdEstado
                    End If
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEliminarPedidoTablaIntermedia_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarPedidoTablaIntermedia.ItemClick

        Try

            If XtraMessageBox.Show(String.Format("¿Eliminar pedido: " & pBePedidoEnc.Referencia & " de tabla intermedia MI3?", pBeStockRes.IdStockRes), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim vResult As Integer = 0

                vResult = clsLnI_nav_ped_traslado_enc.Eliminar_Pedido_By_NoEnc(pBePedidoEnc.Referencia)

                If vResult <> 0 Then

                    '#MECR15102025: Se agrego bitacora de logs para pedidos
                    Dim vMsgError As String = "El usuario: " & AP.UsuarioAp.Nombres & " Eliminó el pedido: " & pBePedidoEnc.Referencia & " de la tabla intermedia"
                    'clsLnLog_error_wms.Agregar_Error(AP.IdEmpresa, cmbBodega.EditValue, vMsgError)
                    clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                        pIdEmpresa:=AP.IdEmpresa,
                                                        pIdBodega:=AP.IdBodega,
                                                        pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                        pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                    XtraMessageBox.Show("Se eliminó el pedido de la tabla intermedia", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If Carga_Datos_PedidoERP() Then
                        xtrPedido.TabPages.Item(7).PageVisible = True
                        mnuEliminarPedidoTablaIntermedia.Enabled = True
                    Else
                        mnuEliminarPedidoTablaIntermedia.Enabled = False
                    End If

                End If

            End If

        Catch ex As Exception

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

        End Try

    End Sub

    '''#EJC20211227:
    '''San Sebastián asaetado reza por tus pecados,
    '''Llora por ti, no olvida
    '''Al que sufre en silencio
    '''A su oveja perdida.
    Public Function Verso() As Boolean
        Return Nothing
    End Function

    Private Sub mnuEliminarPedido_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarPedido.ItemClick

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            cmdEliminar.Enabled = False

            If clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

                XtraMessageBox.Show("Error_de_Proceso_003061653: No se puede anular, Picking asociado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else

                If pBePedidoEnc.Estado = "Nuevo" _
                    OrElse pBePedidoEnc.Estado = "Incompleto" _
                    OrElse pBePedidoEnc.Estado = "Pendiente" _
                    OrElse pBePedidoEnc.Estado = "Verificado" _
                    OrElse pBePedidoEnc.Estado = "Pickeado" _
                    OrElse pBePedidoEnc.Estado = "Anulado" Then

                    If XtraMessageBox.Show("¿Eliminar Pedido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Using MA As New frmMotivo_AnulacionList()

                            With MA

                                .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                                .BeMotivoAnulacionBodega.IdBodega = cmbBodega.EditValue

                                If .ShowDialog() = DialogResult.OK Then

                                    '#CKFK20220324 Modifiqué esto para que cuando esté habilitado el Sync_MI3 y se elimine el documento de salida
                                    'también se elimine en NAV
                                    If pBePedidoEnc.Sync_MI3 Then

                                        If XtraMessageBox.Show("¿Eliminar documento de ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                            SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                            If wsTOMHHInstance Is Nothing Then
                                                clsPublic.Actualizar_Progreso(lblprg, "No está definida la configuración de interface (WS Interno de TOMWMS)")
                                                lblprg.BackColor = Color.Firebrick
                                            Else
                                                lblprg.BackColor = Color.LightBlue
                                            End If

                                            '#EJC202211221049: Validar que la instancia no sea nothing para eliminar desde WS
                                            If Not wsTOMHHInstance Is Nothing Then

                                                clsPublic.Actualizar_Progreso(lblprg, "Conectando a wsTOMHHInstance: " & wsTOMHHInstance.Endpoint.Address.Uri.AbsoluteUri.ToString())

                                                Dim ArchHeader As New wsTOMHH.clsArchHeader()
                                                ArchHeader.Tipo = "WM"

                                                'Si no existe picking no debo borrar

                                                clsPublic.Actualizar_Progreso(lblprg, "Borrar_Picking: " & pBePedidoEnc.Referencia)

                                                Dim vResultBorraPicking As Boolean = wsTOMHHInstance.Borrar_Picking(ArchHeader,
                                                                                                                    pBePedidoEnc.Referencia)

                                                If Not vResultBorraPicking Then
                                                    SplashScreenManager.CloseForm(False)
                                                    XtraMessageBox.Show("No se pudo eliminar el envío de almacén, ni el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Else

                                                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                    SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                                    If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                       AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                       AP.UsuarioAp.IdUsuario) Then

                                                        '#MECR15102025: Se agrego bitacora de logs para pedidos
                                                        'clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231703B: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " Eliminó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)
                                                        Dim msgAdvertencia As String = "ADVERTENCIA_202302231703B: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " Eliminó el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc
                                                        clsLnLog_error_wms_pe.Agregar_Error(msgAdvertencia,
                                                                                            pIdEmpresa:=AP.IdEmpresa,
                                                                                            pIdBodega:=AP.IdBodega,
                                                                                            pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                                                            pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc)

                                                        SplashScreenManager.CloseForm(False)

                                                        XtraMessageBox.Show("Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                        If Not InvokeListarPedidos Is Nothing Then
                                                            InvokeListarPedidos.Invoke()
                                                        End If

                                                        Close()

                                                    Else
                                                        SplashScreenManager.CloseForm(False)
                                                        XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    End If

                                                End If

                                            Else

                                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                                                If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                   AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                   AP.UsuarioAp.IdUsuario) Then

                                                    SplashScreenManager.CloseForm(False)

                                                    '#GT27062024: si anulamos pedido, se anula la póliza asociada)
                                                    If AP.Bodega.Es_Bodega_Fiscal Then
                                                        If pBePedidoEnc.ObjPoliza IsNot Nothing Then
                                                            pBePedidoEnc.ObjPoliza.activo = 0
                                                            clsLnTrans_pe_pol.Anular_poliza(pBePedidoEnc.ObjPoliza)
                                                        End If
                                                    End If

                                                    Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                                    Try

                                                        If vInterfaceSAP Then

                                                            Dim vArgumentosAEnviarAInterface As String = ""
                                                            Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                                            tipoDocumento = pBePedidoEnc.IdTipoPedido

                                                            Select Case tipoDocumento
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                                                    vArgumentosAEnviarAInterface = "12-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                                                    vArgumentosAEnviarAInterface = "8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                                                    vArgumentosAEnviarAInterface = "7-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                            End Select

                                                            'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                            If Ejecutar_Interface(vArgumentosAEnviarAInterface, Me) Then
                                                                XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                            End If

                                                        Else

                                                            XtraMessageBox.Show("Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                        End If

                                                    Catch ex As Exception
                                                        XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado, sin embargo no se pudo actualizar el estado en SAP", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End Try

                                                    If Not InvokeListarPedidos Is Nothing Then
                                                        InvokeListarPedidos.Invoke()
                                                    End If

                                                    Close()

                                                Else
                                                    SplashScreenManager.CloseForm(False)
                                                    XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                End If

                                            End If

                                        Else

                                            If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                               AP.Bodega.Eliminar_Documento_Salida,
                                                                                                               AP.UsuarioAp.IdUsuario) Then

                                                SplashScreenManager.CloseForm(False)

                                                XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                If Not InvokeListarPedidos Is Nothing Then
                                                    InvokeListarPedidos.Invoke()
                                                End If

                                                Close()

                                            Else
                                                SplashScreenManager.CloseForm(False)
                                                XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            End If

                                        End If

                                    Else

                                        If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                           AP.Bodega.Eliminar_Documento_Salida,
                                                                                                           AP.UsuarioAp.IdUsuario) Then

                                            SplashScreenManager.CloseForm(False)

                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                            If Not InvokeListarPedidos Is Nothing Then
                                                InvokeListarPedidos.Invoke()
                                            End If

                                            Close()

                                        Else
                                            SplashScreenManager.CloseForm(False)
                                            XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If

                                    End If

                                End If

                            End With

                        End Using

                    End If

                End If

            End If

            cmdEliminar.Enabled = True

        Catch ex As Exception
            cmdEliminar.Enabled = True
            clsPublic.Actualizar_Progreso(lblprg, "Error_202312121133: " & ex.Message())
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            lblprg.Text = "" : lblprg.Visible = False
        End Try

    End Sub

    'Private Function permiteMenu(menu As String) As Boolean

    '    Dim us As New clsBeUsuario
    '    Dim ms As New clsBeMenu_sistema
    '    Dim clave As String

    '    Try

    '        ms.IdMenu = menu
    '        'MsgBox(link.KeyTip)
    '        clsLnMenu_sistema.GetSingle(ms)

    '        If (ms.Solicitar_clave_autorizacion) Then

    '            us.IdUsuario = AP.UsuarioAp.IdUsuario
    '            clsLnUsuario.GetSingle(us)

    '            Try

    '                clave = clsPublic.Desencriptar(us.Clave_autorizacion)

    '                If (clave = "") Then Throw New Exception("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.")

    '            Catch ex As Exception
    '                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Return False
    '            End Try

    '            Dim frmlog As New frmAjusteLogin() With {.clave = clave}

    '            If frmlog.ShowDialog() <> DialogResult.Yes Then
    '                frmlog.Dispose() : Return False
    '            End If

    '            frmlog.Dispose()

    '            Return True

    '        Else
    '            Return True
    '        End If

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return False
    '    End Try

    'End Function

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As ItemClickEventArgs) Handles tsmiImprimirStockRes.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

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

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdStockReservado
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOMWMS - Lista de reserva documento de salida"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    '#CKFK20240909 se perdió este handles, ya se lo puse Handles txtIdCliente.EditValueChanged
    Private Sub txtIdCliente_EditValueChanged(sender As Object, e As EventArgs) Handles txtIdCliente.EditValueChanged

        Try

            If Not txtIdCliente.EditValue Is Nothing Then

                pBeCliente = clsLnCliente.GetSingle(txtIdCliente.EditValue)

                If Not pBeCliente Is Nothing Then

                    pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBeCliente.IdCliente)

                    txtControlUltimoLote.Text = IIf(pBeCliente.Control_Ultimo_Lote, "Si", "No")

                    If txtControlUltimoLote.Text = "Si" Then
                        txtControlUltimoLote.BackColor = Color.PaleGreen
                    Else
                        txtControlUltimoLote.BackColor = Color.Firebrick
                    End If

                    txtCertificadoCalidad.Text = IIf(pBeCliente.Control_Calidad, "Si", "No")

                    If txtCertificadoCalidad.Text = "Si" Then
                        txtCertificadoCalidad.BackColor = Color.PaleGreen
                    Else
                        txtCertificadoCalidad.BackColor = Color.Firebrick
                    End If

                    If pBeCliente.IdUbicacionVirtual <> 0 Then

                        txtUbicacionTransito.Visible = True
                        lblUbicTransito.Visible = True

                        Dim vIdUbicacionTransito As Integer = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(pBeCliente.IdUbicacionVirtual)

                        If vIdUbicacionTransito <> 0 Then
                            txtUbicacionTransito.Text = clsLnBodega.Get_IdUbicDespacho_By_IdBodega(pBeCliente.IdUbicacionVirtual)
                        Else
                            Throw New Exception("No esta definida la Ubicacion de tránsito para la bodega :" & pBeCliente.Nombre_comercial)
                        End If

                    Else
                        txtUbicacionTransito.Visible = False
                        lblUbicTransito.Visible = False
                    End If

                    If pBeCliente.IdUbicacionAbastecerCon <> 0 Then

                        txtIdUbicacionAbastecimiento.Visible = True
                        lblUbicacionAbastecimiento.Visible = True

                        Dim vIdUbicacionTransito As Integer = pBeCliente.IdUbicacionAbastecerCon

                        If vIdUbicacionTransito <> 0 Then
                            txtIdUbicacionAbastecimiento.Text = vIdUbicacionTransito
                        End If

                    Else
                        txtIdUbicacionAbastecimiento.Visible = False
                        lblUbicacionAbastecimiento.Visible = False
                    End If

                End If

                Llena_Documentos_Referencia_By_Bodega()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Sub mnuImprimirHojaVerificacion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuImprimirHojaVerificacion.ItemClick

        Try

            Imprimir_Vista_Hoja_Verificacion()

            'If clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

            '    Imprimir_Vista_Hoja_Verificacion()

            'Else

            '    XtraMessageBox.Show("Error_de_Proceso_202303061658: Debe generar el picking del pedido antes de imprimir la hoja de verificación",
            '                        Text,
            '                        MessageBoxButtons.OK,
            '                        MessageBoxIcon.Error)

            'End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub Imprimir_Vista_Hoja_Verificacion()

        Try

            GridView1.OptionsPrint.ExpandAllDetails = True
            GridView1.OptionsPrint.PrintDetails = True

            Dim printingSystem1 As New PrintingSystem()
            Dim printLink As New PrintableComponentLink()
            ' Aumentar el margen superior en 100 píxeles
            'printLink.Margins.Top += 80

            '#MECR12092025: Se agrego formato y margenes al diseño.
            printLink.PaperKind = System.Drawing.Printing.PaperKind.Letter
            printLink.Landscape = False
            printLink.Margins = New System.Drawing.Printing.Margins(30, 30, 80, 40)

            AddHandler printLink.CreateMarginalHeaderArea, AddressOf PrintableComponentLinkHojaVeri_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As PageHeaderFooter = TryCast(printLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = BrickAlignment.Far

            If GridView9.Columns.Count > 0 Then

                GridView9.Columns("Factor").Visible = False
                GridView9.BestFitColumns()

            End If

            printingSystem1.PageSettings.Landscape = False
            printingSystem1.PageMargins().Right = 10
            printingSystem1.PageMargins().Left = 10
            printLink.Component = dgridVerificacion
            printLink.Landscape = False
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub


    Private Sub PrintableComponentLinkHojaVeri_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As CreateAreaEventArgs)

        Try

            ' Encabezado principal centrado
            Dim reportHeader As String = AP.Empresa.Nombre &
                                          vbNewLine & "Hoja de verificación, TOMWMS." &
                                          vbNewLine & "Cliente: " & pBeCliente.Codigo & " - " & pBeCliente.Nombre_comercial

            e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
            e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)
            Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 100)
            e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

            ' Encabezado de detalles alineado a la izquierda
            Dim detailHeader As String = String.Empty

            detailHeader += "Fecha: " & pBePedidoEnc.Fecha_Pedido

            If Not pBePedidoEnc.IdPickingEnc = 0 Then
                detailHeader += "Picking WMS#: " & pBePedidoEnc.IdPickingEnc
            End If

            If Not pBePedidoEnc.Referencia = "" Then
                detailHeader += vbNewLine & "Referencia#: " & pBePedidoEnc.Referencia
            End If

            If Not pBePedidoEnc.IdTipoManufactura = 0 Then
                detailHeader += vbNewLine & "Manufactura Ligera: " & cmbManufacturaLigera.Text
            End If

            If Not pBePedidoEnc.Observacion = "" Or Not pBePedidoEnc.RoadDirEntrega = "" Then
                detailHeader += vbNewLine & "Observación: " & IIf(pBePedidoEnc.Observacion <> "", pBePedidoEnc.Observacion & " ", "") &
                            IIf(pBePedidoEnc.RoadDirEntrega <> "", pBePedidoEnc.RoadDirEntrega, "")
            End If

            e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Near)
            e.Graph.Font = New Font("Tahoma", 10, FontStyle.Regular)
            Dim detailRec As RectangleF = New RectangleF(0, 100, e.Graph.ClientPageSize.Width, 275)
            e.Graph.DrawString(detailHeader, Color.Black, detailRec, DevExpress.XtraPrinting.BorderSide.None)

        Catch ex As Exception
            ' Manejo de errores
        End Try

    End Sub

    'Private Sub PrintableComponentLinkHojaVeri_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

    '    Dim reportHeader As String = vbNewLine & "TOMWMS - Hoja de verificación."
    '    Dim RutaDespacho As String = vbNewLine & "Ruta#: " & cmbRoadRutaPedido.Text

    '    If Not pBePedidoEnc.IdPickingEnc = 0 Then
    '        reportHeader += vbNewLine & " Picking WMS#: " & pBePedidoEnc.IdPickingEnc
    '    End If

    '    If Not pBePedidoEnc.Referencia = "" Then
    '        reportHeader += vbNewLine & "Referencia#: " & pBePedidoEnc.Referencia
    '    End If

    '    If Not pBePedidoEnc.Observacion = "" Or Not pBePedidoEnc.RoadDirEntrega = "" Then
    '        reportHeader += vbNewLine & "Observación: " & IIf(pBePedidoEnc.Observacion <> "", pBePedidoEnc.Observacion & " ", "") &
    '                        IIf(pBePedidoEnc.RoadDirEntrega <> "", pBePedidoEnc.RoadDirEntrega, "")

    '    End If

    '    e.Graph.StringFormat = New BrickStringFormat(StringAlignment.Center)
    '    e.Graph.Font = New Font("Tahoma", 8, FontStyle.Bold)

    '    Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 150)
    '    e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None)


    'End Sub

    Private Sub dgrid_MouseClick(sender As Object, e As MouseEventArgs) Handles dgrid.MouseClick

        Try

            If Not dgrid.CurrentCell Is Nothing Then
                ReleaseRowPicking = dgrid.CurrentCell.RowIndex
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)
        End Try

    End Sub

    Private Function Reservar_Stock_Por_Linea_Seleccion(ByVal IndiceLinea As Integer,
                                                        ByVal pIdPropietario As Integer,
                                                        ByVal pObjVWStock As clsBeVW_stock_res,
                                                        Optional ByRef MostrarMensajeReserva As Boolean = True) As Boolean

        Reservar_Stock_Por_Linea_Seleccion = False

        Try

            'CM_20200121: Puse esta condición para exitar que cuando sea eliminada una línea por stock especifico no vuelva a reservar la misma cantidad.
            If Not dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value Is Nothing Then
                '#EJC20200125: Te falto validar si la cantidad es igual, porque si se modific? se debe entrar a reservar_stock again.
                If pBePedidoDetList.Exists(Function(x) x.IdPedidoDet = vIdPedidoDet AndAlso x.Cantidad = vCantidad) Then
                    Exit Function
                End If
            End If

            Dim vClienteTiempo As New clsBeCliente_tiempos

            If pClienteTiemposList IsNot Nothing Then
                vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                x.IdClasificacion = pBeProducto.Clasificacion.IdClasificacion _
                                And x.IdFamilia = pBeProducto.Familia.IdFamilia)
            End If

            Dim vDiasVencimientoCliente As Integer = 0

            If Not vClienteTiempo Is Nothing Then
                If chkPedidoLocal.Checked Then
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                Else
                    vDiasVencimientoCliente = vClienteTiempo.Dias_Exterior
                End If
            End If

            If Not NoDiasVencimientoCell Is Nothing Then
                NoDiasVencimientoCell.Value = vDiasVencimientoCliente
            End If

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.No_linea = vNoLinea
            pBePedidoDet.IdPedidoDet = vIdPedidoDet
            pBePedidoDet.IdPedidoEnc = pBePedidoEnc.IdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProductoBodega = pObjVWStock.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pObjVWStock.IdProductoBodega
            pBePedidoDet.Producto.Codigo = pObjVWStock.Codigo_Producto
            pBePedidoDet.IdPresentacion = pObjVWStock.IdPresentacion
            pBePedidoDet.IdUnidadMedidaBasica = pObjVWStock.IdUnidadMedida

            If pObjVWStock.IdPresentacion = 0 Then
                pBePedidoDet.Cantidad = pObjVWStock.CantidadUmBas - pObjVWStock.CantidadReservadaUMBas
            Else
                If pObjVWStock.Factor = 0 Then
                    Throw New Exception(String.Format("La presentación del producto {0} no tiene factor definido ", pObjVWStock.Codigo_Producto))
                End If
                pBePedidoDet.Cantidad = pObjVWStock.CantidadPresentacion - (pObjVWStock.CantidadReservadaUMBas / pObjVWStock.Factor)
            End If

            pBePedidoDet.Peso = vPeso
            pBePedidoDet.Precio = vPrecio
            pBePedidoDet.No_recepcion = pObjVWStock.IdRecepcionEnc
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdPresentacion = pObjVWStock.IdPresentacion
            pBePedidoDet.IdEstado = pObjVWStock.IdProductoEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Nom_estado = pObjVWStock.NomEstado
            pBePedidoDet.IsNew = IIf(vIdPedidoDet = 0, True, False)
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = AP.UsuarioAp.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.Precio = vPrecio
            pBePedidoDet.RoadPrecioDoc = vPrecio
            pBePedidoDet.RoadTotal = vTotal
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Codigo_Producto = pObjVWStock.Codigo_Producto
            pBePedidoDet.Nombre_producto = pObjVWStock.Nombre_Producto
            pBePedidoDet.Nom_presentacion = pObjVWStock.Nombre_Presentacion
            pBePedidoDet.Nom_unid_med = pBeProducto.UnidadMedida.Nombre
            pBePedidoDet.Nom_estado = pObjVWStock.NomEstado
            pBePedidoDet.Ndias = vDiasVencimientoCliente
            pBePedidoDet.Producto.IdProducto = vIdProducto
            pBePedidoDet.Producto.Nombre = pObjVWStock.Nombre_Producto
            pBePedidoDet.IdProductoBodega = pObjVWStock.IdProductoBodega
            pBePedidoDet.IdStockEspecifico = pObjVWStock.IdStock

            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = pBePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED" 'Pedido #CKFK20220704 Se quito PE y se puso PED
            pBeStockRes.añada = pObjVWStock.Añada

            If pObjVWStock.IdPresentacion = 0 Then
                pBeStockRes.Cantidad = pObjVWStock.CantidadUmBas
            Else
                pBeStockRes.Cantidad = Math.Round(pObjVWStock.CantidadPresentacion * pObjVWStock.Factor, 6)
            End If

            pBeStockRes.Peso = vPeso
            pBeStockRes.Estado = "PPC" 'Producto pedido por cliente
            pBeStockRes.User_agr = AP.UsuarioAp.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = AP.UsuarioAp.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = AP.HostName
            pBeStockRes.IdProductoEstado = 0
            pBeStockRes.IdPresentacion = pObjVWStock.IdPresentacion
            pBeStockRes.IdProductoEstado = pObjVWStock.IdProductoEstado
            pBeStockRes.IdPedido = pBePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = vIdPedidoDet
            pBeStockRes.IdProductoBodega = pObjVWStock.IdProductoBodega
            pBeStockRes.IdPropietarioBodega = lcmbPropietario.EditValue
            pBeStockRes.IdUnidadMedida = pObjVWStock.IdUnidadMedida
            pBeStockRes.Ubicacion_ant = pObjVWStock.IdUbicacion_Anterior
            pBeStockRes.IdBodega = cmbBodega.EditValue
            '#EJC20220718:  IdUbicacionAbastecerCon
            pBeStockRes.IdUbicacionAbastecerCon = Val(txtIdUbicacionAbastecimiento.Text)

            If Not pBeCliente Is Nothing Then
                '#EJC20190313_0718PM: Se agregó para considerar en proceso de clsLnTrans_pe_det.Reservar_Stock_Por_Linea()
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote
            End If

            pObjVWStock.Peso = vPeso

            If pBeStockRes.Control_Ultimo_Lote Then
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pObjVWStock.IdProductoBodega)
            End If

            If Not dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value Is Nothing Then

                Dim IdStock As Integer = 0
                IdStock = dgrid.Rows(IndiceLinea).Cells("IdStockEspecifico").Value

                Dim BeStockEspecifico As New clsBeStock
                BeStockEspecifico = clsLnStock.GetSingle(IdStock)

                clsLnTrans_pe_det.Reservar_Stock_Especifico_Por_Linea(vDiasVencimientoCliente,
                                                                      pBePedidoDet,
                                                                      pBeStockRes,
                                                                      BeStockEspecifico,
                                                                      pBePedidoEnc.IdPickingEnc,
                                                                      AP.HostName,
                                                                      pIdPropietario)

                '#CMT20180913: Erik dice que hay que agregar a la lista de 
                'trans_ubic_hh_det el stock reservado para que se reste en memoria en la pantalla de stock
                Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                BeTransUbicHHDet.IdStock = IdStock
                BeTransUbicHHDet.Cantidad = pBeStockRes.Cantidad
                BeTransUbicHHDet.Activo = True
                pListBeTrans_ubic_hh_det.Add(BeTransUbicHHDet)
                pBePedidoDet.IsNew = False

            Else

                clsLnTrans_pe_det.Reservar_Stock_Por_Linea(vDiasVencimientoCliente,
                                                           pBePedidoDet,
                                                           pBeStockRes,
                                                           pBePedidoEnc.IdPickingEnc,
                                                           AP.HostName,
                                                           AP.IdEmpresa,
                                                           cmbBodega.EditValue,
                                                           pIdPropietario)
                pBePedidoDet.IsNew = False

            End If

            Dim vIndiceDetalle As Integer = pBePedidoDetList.FindIndex(Function(x) x.IdPedidoDet = pBePedidoDet.IdPedidoDet)

            If vIndiceDetalle = -1 Then
                pBePedidoDetList.Add(pBePedidoDet)
            Else
                CopyObject(pBePedidoDet, pBePedidoDetList(vIndiceDetalle))
            End If

            Reservar_Stock_Por_Linea_Seleccion = True

            If MostrarMensajeReserva Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Carga_Stock_Reservado()
                Cargar_Picking()
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error al reservar stock:" & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            prg.Visible = False
        End Try

    End Function

    Private Sub mnuCrearPicking_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuCrearPicking.ItemClick
        Process_Picking()
    End Sub

    Private Sub Process_Picking()

        txtIdPicking.Enabled = False

        Try

            If txtIdPicking.Text.Trim <> "0" Then

                If mgr IsNot Nothing AndAlso Not mgr.IsSplashFormVisible Then
                    mgr.ShowWaitForm()
                    mgr.SetWaitFormDescription("Cargando picking...")
                End If

                Cierra_Instancia_Previa(frmPicking)

                With frmPicking
                    .Text = "Picking " & txtIdPicking.Text
                    .chkEmpaquePorTarima.Checked = clsLnTrans_pe_enc.Tiene_Empaque_Tarima_By_IdPedidoEnc(lblIdPedidoEnc.Text)
                    .pReferencia = txtReferencia.Text
                    .Modo = frmPicking.TipoTrans.Editar
                    .BePickingEnc = clsLnTrans_picking_enc.GetSingle(txtIdPicking.Text)
                    .WindowState = FormWindowState.Normal
                    .MdiParent = MdiParent
                    .InvokeActualizarIdPicking = AddressOf Actualizar_Info_Picking
                    .InvokeCargarPedido = AddressOf Cargar_Datos
                    .InvokeCargarObjetoPedido = AddressOf Recargar_Objeto_Pedido
                    .Show()
                    .Focus()
                End With

            Else
                Nuevo_Picking()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If mgr IsNot Nothing AndAlso mgr.IsSplashFormVisible Then
                mgr.CloseWaitForm()
            End If
            txtIdPicking.Enabled = True
        End Try

    End Sub

    Private Sub txtIdDespacho_Click(sender As Object, e As EventArgs) Handles txtIdDespacho.LinkClicked

        Despacho_Link()

    End Sub

    Private Function Cliente_Control_Calidad(pIdCliente As Integer, lClientes As List(Of clsBeCliente))

        Cliente_Control_Calidad = False

        Try

            Dim pCliente = lClientes.Find(Function(x) x.IdCliente = pIdCliente)

            If pCliente IsNot Nothing Then
                Return pCliente.Control_Calidad
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Cliente_Control_Ultimo_Lote(pIdCliente As Integer, lClientes As List(Of clsBeCliente))

        Cliente_Control_Ultimo_Lote = False

        Try

            Dim pCliente As New clsBeCliente With {.IdCliente = pIdCliente}
            pCliente = lClientes.Find(Function(x) x.IdCliente = pIdCliente)

            If pCliente IsNot Nothing Then
                Return pCliente.Control_Ultimo_Lote
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click

        PicImg.Visible = False

        Try

            Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
            gFile.ShowDialog()

            If gFile.FileName.Length <> 0 Then

                Dim iNombreDoc As String = InputBox("Ingrese una descripción:", Text, "")


                If String.IsNullOrEmpty(iNombreDoc) = False Then

                    PicImg.ImageLocation = gFile.FileName
                    PicImg.Tag = gFile.FileName

                    Dim ObjI As New clsBeTrans_picking_img

                    If lBeTransPickImagen IsNot Nothing AndAlso lBeTransPickImagen.Count > 0 Then
                        ObjI.IdImagen = (From b In lBeTransPickImagen.AsEnumerable Select b.IdImagen).Max + 1
                    Else
                        ObjI.IdImagen = 1
                    End If

                    ObjI.Observacion = iNombreDoc

                    PicImg.ImageLocation.LastIndexOf("\")
                    ObjI.Imagen = ReadBinaryFile(PicImg.ImageLocation)
                    ObjI.IdPickingDet = 0
                    ObjI.IdPickingEnc = 0
                    ObjI.IdPedidoEnc = 0
                    ObjI.IdPedidoDet = 0
                    ObjI.IsNew = True
                    lBeTransPickImagen.Add(ObjI)
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

            If lBeTransPickImagen IsNot Nothing AndAlso lBeTransPickImagen.Count > 0 Then
                For Each BeTransPickingIMg As clsBeTrans_picking_img In lBeTransPickImagen
                    DT.Rows.Add(BeTransPickingIMg.IdImagen, BeTransPickingIMg.Observacion)
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
                Dim Obj As New clsBeTrans_picking_img
                Obj = lBeTransPickImagen.Find(Function(b) b.IdImagen = CInt(Dr.Item("Código")))
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
                    lIndex = lBeTransPickImagen.FindIndex(Function(i) i.IdImagen = CInt(Dr.Item("Código")))
                    If lIndex > -1 Then
                        clsLnTrans_picking_img.Eliminar(lBeTransPickImagen(lIndex))
                        lBeTransPickImagen.RemoveAt(lIndex)
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
    Private Sub Cargar_Manufactura(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction)

        Dim pIdPedidoEnc As Integer = 0
        Dim pIdManufactura As Integer = 0

        Try

            If Not pBePedidoEnc Is Nothing Then

                pIdPedidoEnc = pBePedidoEnc.IdPedidoEnc

                pIdManufactura = clsLnTrans_manufactura_enc.Get_Manufactura_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                          lConnection,
                                                                                          lTransaction)

                If pIdManufactura > 0 Then
                    txtIdManufactura.Text = pIdManufactura
                    grpUltTareaManufactura.Visible = True
                Else
                    grpUltTareaManufactura.Visible = False
                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub txtIdManufactura_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles txtIdManufactura.LinkClicked
        Process_Manufactura()
    End Sub

    Private Sub Process_Manufactura()

        Try

            If txtIdManufactura.Text.Trim <> "0" Then

                If mgr IsNot Nothing AndAlso Not mgr.IsSplashFormVisible Then
                    mgr.ShowWaitForm()
                    mgr.SetWaitFormDescription("Cargando manufactura...")
                End If

                Cierra_Instancia_Previa(frmManufactura)

                Dim BeManufacturaEnc As New clsBeTrans_manufactura_enc
                BeManufacturaEnc.IdManufacturaEnc = Val(txtIdManufactura.Text)
                clsLnTrans_manufactura_enc.GetSingle(BeManufacturaEnc)

                With frmManufactura
                    .Modo = frmPicking.TipoTrans.Editar
                    .BeManufacturaEnc = BeManufacturaEnc
                    .WindowState = FormWindowState.Normal
                    .MdiParent = MdiParent
                    .Show()
                    .Focus()
                End With

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If mgr IsNot Nothing AndAlso mgr.IsSplashFormVisible Then
                mgr.CloseWaitForm()
            End If
        End Try

    End Sub

    Private Sub GridView9_CustomDrawFooterCell(sender As Object, e As FooterCellCustomDrawEventArgs) Handles GridView9.CustomDrawFooterCell
        If e.Column.FieldName = "CantidadUMBas" Then
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        End If
    End Sub

    Private Sub Cargar_Servicios_Registrados()
        Try

            Dim listServicios As New List(Of clsBeTrans_pe_servicios)

            listServicios = clsLnTrans_pe_servicios.Get_All_By_IdOrdenPedidoEnc(pBePedidoEnc.IdPedidoEnc)

            If listServicios IsNot Nothing Then

                For Each Servicio As clsBeTrans_pe_servicios In listServicios

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
                                                    Servicio.IdOrdenPedidoServicio,
                                                    False)

                    End If





                Next

                dgridServiciosAsociados.DataSource = DTGridDetalleServicios

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Grid_Servicios_Tiene_Error As Boolean = False
    Private Sub gvDetalleServicios_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles gvDetalleServicios.ValidateRow

        Dim clsTransaccion As New clsTransaccion

        Try
            Dim servicio As New clsBeTrans_pe_servicios()
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
            Dim IdOrdenPedidoServicio As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "IdOrdenPedidoServicio")), 0, View.GetRowCellValue(e.RowHandle, "IdOrdenPedidoServicio"))
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

                servicio = New clsBeTrans_pe_servicios

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

                If pBePedidoEnc.IdPedidoEnc > 0 Then

                    '#GT29052024: si es linea nueva, se agrega a la lista.
                    If vIsNewRow Then
                        acuerdoDet.Correlativo_detalleacuerdo = pCorrelativo
                        clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(acuerdoDet)
                        servicio.IsNew = vIsNewRow
                        servicio.IdOrdenPedidoServicio = clsLnTrans_pe_servicios.MaxID(clsTransaccion.lConnection,
                                                                                       clsTransaccion.lTransaction) + 1
                        servicio.IdOrdenPedidoEnc = pBePedidoEnc.IdPedidoEnc
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

                        For indice As Integer = 0 To gvDetalleServicios.RowCount - 1

                            Dim pServicioExiste = gvDetalleServicios.GetRowCellValue(indice, "servicio")
                            Dim pCorrelativoExiste = CInt(gvDetalleServicios.GetRowCellValue(indice, "correlativo_detalleacuerdo"))

                            If pServicioExiste = servicio.Nombre_servicio AndAlso pCorrelativoExiste = servicio.Corre_detalleacuerdo Then

                                vExisteRegla = True
                                Exit For
                            End If

                        Next

                        If vExisteRegla Then
                            View.SetColumnError(ColServicio, "No se puede registrar el mismo servicio mas de una vez.")
                        Else

                            If pBePedidoEnc.IdAcuerdoComercial = 0 Then
                                pBePedidoEnc.IdAcuerdoComercial = acuerdoDet.IdAcuerdoEnc
                                pBePedidoEnc.User_mod = AP.UsuarioAp.IdUsuario
                                pBePedidoEnc.Fec_mod = Now
                                clsLnTrans_pe_enc.Actualizar_AcuerdoComercial_By_IdPedidoEnc(pBePedidoEnc,
                                                                                             clsTransaccion.lConnection,
                                                                                             clsTransaccion.lTransaction)
                            End If

                            '#GT29052024: guardar inmediatamente el servicio.
                            clsLnTrans_pe_servicios.Insertar(servicio,
                                                             clsTransaccion.lConnection,
                                                             clsTransaccion.lTransaction)
                        End If


                    Else
                        '#GT: actualizar cantidad
                        servicio.IdOrdenPedidoEnc = pBePedidoEnc.IdPedidoEnc
                        servicio.IdOrdenPedidoServicio = IdOrdenPedidoServicio
                        servicio.User_mod = AP.UsuarioAp.IdUsuario
                        servicio.Fec_mod = Now
                        clsLnTrans_pe_servicios.Actualizar_Servicio_By_IdServicio(servicio, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                    End If

                    e.Valid = (servicio.IdOrdenPedidoServicio > 0) AndAlso Not vExisteRegla

                    If e.Valid Then
                        If servicio.IdOrdenPedidoServicio > 0 Then
                            View.SetRowCellValue(e.RowHandle, "IdOrdenPedidoServicio", servicio.IdOrdenPedidoServicio)
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

    Public Sub Recargar_Objeto_Pedido()

        Try
            pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(pBePedidoEnc.IdPedidoEnc)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbAcuerdoComercial_EditValueChanged(sender As Object, e As EventArgs) Handles cmbAcuerdoComercial.EditValueChanged

        Try

            If cmbAcuerdoComercial.EditValue > 0 Then

                Select Case Modo

                    Case TipoTrans.Nuevo
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, AP.Bodega.IdBodega)
                    Case TipoTrans.Editar
                        ServicioGridLookUpEdit.DataSource = clsLnTrans_acuerdoscomerciales_det.Get_Detalle_By_Codigo_Acuerdo(cmbAcuerdoComercial.EditValue, pBePedidoEnc.IdBodega)

                End Select

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Llena_Motivos_Devolucion()

        Try

            cmbMotivoDevolucion.Properties.DataSource = Nothing

            Dim l As New DataTable

            Propietario = clsLnPropietarios.GetSingle(clsLnPropietarios.Get_IdPropietario(cmbBodega.EditValue, lcmbPropietario.EditValue))

            l = clsLnMotivo_devolucion.Get_All_By_IdPropietario_And_Bodega_DT(Propietario.IdPropietario, cmbBodega.EditValue)

            If l.Rows.Count > 0 Then
                lblMotivoDevolucion.Visible = True
                cmbMotivoDevolucion.Visible = True
                cmbMotivoDevolucion.Properties.ValueMember = "IdMotivoDevolucion"
                cmbMotivoDevolucion.Properties.DisplayMember = "Nombre"
                cmbMotivoDevolucion.Properties.DataSource = l
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmbTipoIngreso_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoPedido.EditValueChanged
        Set_Tipo_Documento()
    End Sub

    Private Sub Cargar_Existencias_Pedido()

        Try

            Dim DT As New DataTable
            DT = clsLnStock.Get_All_Stock_Consolidado_DT_By_Referencia(pBePedidoEnc.Referencia)

            dgridExistencias.DataSource = DT

            If GridView11.Columns.Count > 0 Then
                GridView11.Columns("Cantidad").DisplayFormat.FormatType = FormatType.Numeric
                GridView11.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView11.Columns("Cantidad").SummaryItem.SummaryType = SummaryItemType.Sum
                GridView11.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"
                GridView11.BestFitColumns()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub mnuRevertirDespacho_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuRevertirDespacho.ItemClick

        Dim us As New clsBeUsuario
        Dim ms As New clsBeMenu_sistema
        Dim clave As String

        Try

            us.IdUsuario = AP.UsuarioAp.IdUsuario
            clsLnUsuario.GetSingle(us)

            Try

                clave = clsPublic.Desencriptar(us.Clave_autorizacion)

                If (clave = "") Then Throw New Exception

            Catch ex As Exception
                MsgBox("No se ha registrado la clave de autorización para el usuario y esta transacción necesita clave de supervisor.") : Exit Sub
            End Try

            Dim frmlog As New frmAjusteLogin() With {.clave = clave}

            If frmlog.ShowDialog() <> DialogResult.Yes Then
                frmlog.Dispose()
            Else

                frmlog.Dispose()

                If XtraMessageBox.Show(String.Format("¿Revertir despacho de pedido?", dgrid.RowCount - 1), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    If clsLnTrans_despacho_enc.Revertir_Despacho_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc) Then

                        XtraMessageBox.Show("Se ha generado el documento de ingreso para el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If Not InvokeListarPedidos Is Nothing Then
                            InvokeListarPedidos.Invoke()
                        End If

                    End If

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

        End Try

    End Sub

    Private Sub cmbMuelle_EditValueChanged(sender As Object, e As EventArgs) Handles cmbMuelle.EditValueChanged

        Try

            Dim fila As Object = cmbMuelle.GetSelectedDataRow

            If fila IsNot Nothing Then

                Dim vIdUbicacion As Integer = fila.Item("IdUbicacionDefecto")
                txtIdUbicacionMuelle.Text = vIdUbicacion

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function Pedido_Requiere_Muelle() As Boolean

        Pedido_Requiere_Muelle = False

        Try

            If cmbTipoPedido.EditValue <> 0 Then
                '#AT20250710 Cambie de escanear_muelle_picking a Mover_Producto_Zona_Muelle
                'Porque escanear_muelle_picking se utiliza para saber si se debe o no escanear el muelle en la HH
                Return clsLnTrans_pe_tipo.GetSingle(cmbTipoPedido.EditValue)?.Mover_Producto_Zona_Muelle

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuImprimirGridDocumentoERP_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles mnuImprimirGridDocumentoERP.ItemClick
        Imprimir_DocumentoERP()
    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderDocumentoERP(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Documento ERP"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Imprimir_DocumentoERP()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderDocumentoERP

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdPedTras
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Llena_Cliente_Grid(ByVal pIndex As Integer,
                                   Optional ByVal pIdCliente As Integer = 0)

        Try

            DgComboCliente = TryCast(dgrid.Rows(pIndex).Cells("IdCliente"), DataGridViewComboBoxCell)
            DgComboCliente.DropDownWidth = 200

            Dim lCliente As New List(Of clsBeCliente)

            lCliente = New List(Of clsBeCliente)

            If Modo = TipoTrans.Nuevo Then
                lCliente = clsLnCliente.Get_All()
            Else
                lCliente = clsLnCliente.Get_All_By_IdCliente(pIdCliente)
            End If

            DgComboCliente.DataSource = lCliente
            DgComboCliente.ValueMember = "IdCliente"
            DgComboCliente.DisplayMember = "nombre_comercial"

            If DgComboCliente.Items.Count > 0 AndAlso (Modo = TipoTrans.Nuevo Or Modo = TipoTrans.Editar) Then
                Dim vIdCliente As Integer = lCliente(0).IdCliente
                DgComboCliente.Value = vIdCliente
                If Not DgComboCliente.Value = lCliente(0).IdCliente Then
                    DgComboCliente.Value = Nothing
                End If
                Exit Sub
            Else
                DgComboCliente.Value = Nothing
            End If

            If pIdCliente <> 0 AndAlso DgComboCliente.Items.Count > 0 Then
                DgComboCliente.Value = pIdCliente
            Else
                DgComboCliente.Value = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    'Private Sub Carga_Stock_Reservado()

    '    Dim clsTrans As New clsTransaccion

    '    Try

    '        clsTrans.Begin_Transaction()

    '        Dim ListStockRes As New List(Of clsBeVW_stock_res)

    '        ListStockRes = clsLnStock_res.Get_All_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
    '                                                             pBePedidoEnc.IdBodega,
    '                                                             clsTrans.lConnection,
    '                                                             clsTrans.lTransaction)

    '        DTStockRes.Rows.Clear()

    '        For Each BeVW_stock_res As clsBeVW_stock_res In ListStockRes

    '            DTStockRes.Rows.Add(BeVW_stock_res.IdStockRes,
    '                                BeVW_stock_res.IdStock,
    '                                BeVW_stock_res.Codigo_Producto,
    '                                BeVW_stock_res.Nombre_Producto,
    '                                BeVW_stock_res.NomEstado,
    '                                BeVW_stock_res.Lote,
    '                                BeVW_stock_res.Lic_plate,
    '                                BeVW_stock_res.Fecha_Vence,
    '                                BeVW_stock_res.Cantidad_Res,
    '                                BeVW_stock_res.UMBas,
    '                                BeVW_stock_res.CantidadPresentacion,
    '                                BeVW_stock_res.Nombre_Presentacion,
    '                                BeVW_stock_res.Peso,
    '                                BeVW_stock_res.Ubicacion_Nombre,
    '                                BeVW_stock_res.Host,
    '                                BeVW_stock_res.no_linea,
    '                                pBePedidoEnc.Referencia)
    '        Next

    '        grdStockReservado.DataSource = DTStockRes

    '        GridView6.OptionsView.ColumnAutoWidth = False
    '        GridView6.OptionsView.ShowFooter = True

    '        lblRegs.Caption = String.Format("Registros: {0}", GridView6.RowCount)

    '        GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatType = FormatType.Numeric
    '        GridView6.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
    '        GridView6.Columns("Cantidad_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
    '        GridView6.Columns("Cantidad_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

    '        GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatType = FormatType.Numeric
    '        GridView6.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"
    '        GridView6.Columns("Cantidad_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
    '        GridView6.Columns("Cantidad_Pres").SummaryItem.DisplayFormat = "{0:n6}"

    '        GridView6.Columns("Peso").DisplayFormat.FormatType = FormatType.Numeric
    '        GridView6.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"
    '        GridView6.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
    '        GridView6.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

    '        Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
    '          With {.FieldName = "Cantidad",
    '          .SummaryType = SummaryItemType.Sum,
    '          .DisplayFormat = "{0:n6}",
    '          .ShowInGroupColumnFooter = GridView6.Columns("Cantidad")}
    '        GridView6.GroupSummary.Add(item)

    '        Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
    '        With {.FieldName = "Peso",
    '        .SummaryType = SummaryItemType.Sum,
    '        .DisplayFormat = "{0:n6}",
    '        .ShowInGroupColumnFooter = GridView6.Columns("Peso")}
    '        GridView6.GroupSummary.Add(item1)

    '        Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
    '        With {.FieldName = "Cantidad_UMBas",
    '        .SummaryType = SummaryItemType.Sum,
    '        .DisplayFormat = "{0:n6}",
    '        .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_UMBas")}
    '        GridView6.GroupSummary.Add(item2)


    '        Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
    '        With {.FieldName = "Cantidad_Pres",
    '        .SummaryType = SummaryItemType.Sum,
    '        .DisplayFormat = "{0:n6}",
    '        .ShowInGroupColumnFooter = GridView6.Columns("Cantidad_Pres")}
    '        GridView6.GroupSummary.Add(item3)

    '        GridView6.BestFitColumns()

    '        clsTrans.Commit_Transaction()

    '    Catch ex As Exception
    '        clsTrans.RollBack_Transaction()
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Finally
    '        clsTrans.Close_Conection()
    '    End Try

    'End Sub
    Private Sub Llena_Estados_Grid(ByVal pIndex As Integer,
                                   ByVal lConnection As SqlConnection,
                                   ByVal lTransaction As SqlTransaction,
                                   Optional ByVal pIdEstado As Integer = 0)

        Try

            DgComboEstado = TryCast(dgrid.Rows(pIndex).Cells("colEstadoProducto"), DataGridViewComboBoxCell)

            DgComboEstado.DropDownWidth = 200

            If Not pBeProducto Is Nothing Then

                Dim lEstado As New List(Of clsBeProducto_estado)
                lEstado = clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProductoBodega(pBeProducto.IdProductoBodega,
                                                                                            lConnection,
                                                                                            lTransaction).ToList()

                If pIdEstado <> 0 Then
                    lEstado = lEstado.FindAll(Function(x) x.IdEstado = pIdEstado)
                End If

                DgComboEstado.DataSource = lEstado
                DgComboEstado.ValueMember = "IdEstado"
                DgComboEstado.DisplayMember = "Nombre"

                If DgComboEstado.Items.Count > 0 Then
                    DgComboEstado.Value = lEstado(0).IdEstado
                Else
                    '#EJC20171024_1136PM:Corrección para cuando se cambia a un Código de producto que no tiene stock y por lo tanto no tiene estado.
                    DgComboEstado.Value = Nothing
                End If

                If pIdEstado <> 0 Then
                    If DgComboEstado.Items.Count > 0 Then
                        DgComboEstado.Value = pIdEstado
                    End If
                End If


            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = ex.Message
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pIdEmpresa:=AP.IdEmpresa,
                                                pIdBodega:=AP.IdBodega,
                                                pUsrAgr:=AP.UsuarioAp.IdUsuario,
                                                pIdPedidoEnc:=pBePedidoEnc.IdPedidoEnc,
                                                pStackTrace:=ex.StackTrace)

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

End Class